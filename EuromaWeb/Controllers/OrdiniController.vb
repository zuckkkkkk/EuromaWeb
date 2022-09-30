Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports EuromaWeb
Imports Newtonsoft.Json
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel

Namespace Controllers
    Public Class OrdiniController
        Inherits System.Web.Mvc.Controller

        Private db As New EuromaModels

        ' GET: Ordini
        Function Index(ByVal id As Integer) As ActionResult
            ViewBag.Year = id
            ViewBag.TotITA = 0
            ViewBag.TotEST = 0
            'Dim lst As List(Of Entity) = New List(Of Entity)()
            If id = DateTime.Now.Year Then
                Dim data = db.Ordini.Where(Function(s) s.Anno = Date.Now.Year).GroupBy(Function(x) New With {x.Anno, x.Mese, x.Tipo_Ordine}, Function(key, group) New With {Key .yr = key.Anno, Key .mnth = key.Mese, Key .type = key.Tipo_Ordine, Key .tCharge = group.Sum(Function(k) k.Valore_Netto)}).OrderBy(Function(y) y.yr).ThenBy(Function(z) z.mnth).ToList()
                Dim list As New List(Of OrdineViewModel)
                For Each d In data
                    Dim ord As New OrdineViewModel With {
                        .Anno = d.yr,
                        .Mese = d.mnth,
                        .tCharge = d.tCharge,
                        .Tipo_Ordine = d.type
                        }
                    list.Add(ord)
                Next
                Dim ret As New OrdineListaViewmodel With {.Lista = list}
                ViewBag.OrdiniMese = JsonConvert.SerializeObject(db.Ordini.Where(Function(s) s.Anno = Date.Now.Year).GroupBy(Function(x) New With {x.Mese}, Function(key, group) New With {Key .mnth = key.Mese, Key .tCharge = group.Sum(Function(k) k.Valore_Totale)}).OrderBy(Function(z) z.mnth).ToArray())
                ViewBag.OrdiniMeseAnnoPrima1 = JsonConvert.SerializeObject(db.Ordini.Where(Function(s) s.Anno = Date.Now.Year - 1).GroupBy(Function(x) New With {x.Mese}, Function(key, group) New With {Key .mnth = key.Mese, Key .tCharge = group.Sum(Function(k) k.Valore_Totale)}).OrderBy(Function(z) z.mnth).ToArray())
                ViewBag.OrdiniMeseAnnoPrima2 = JsonConvert.SerializeObject(db.Ordini.Where(Function(s) s.Anno = Date.Now.Year - 2).GroupBy(Function(x) New With {x.Mese}, Function(key, group) New With {Key .mnth = key.Mese, Key .tCharge = group.Sum(Function(k) k.Valore_Totale)}).OrderBy(Function(z) z.mnth).ToArray())
                ViewBag.OrdiniMarca = JsonConvert.SerializeObject(db.Ordini.Where(Function(s) s.Anno = Date.Now.Year).GroupBy(Function(x) New With {x.Tipo_Ordine}, Function(key, group) New With {Key .mnth = key.Tipo_Ordine, Key .tCharge = group.Sum(Function(k) k.Valore_Netto)}).OrderBy(Function(z) z.mnth).ToArray())
                Dim paesi = db.Ordini.Where(Function(s) s.Anno = id).GroupBy(Function(x) New With {x.Stato}, Function(key, group) New With {Key .mnth = key.Stato, Key .tCharge = group.Count}).OrderBy(Function(z) z.mnth).ToArray()
                Dim dict As New Dictionary(Of String, Short)
                For Each p In paesi
                    Try
                        Dim v As String = p.mnth.Split("  - ")(0)
                        dict.Add(p.mnth.Split("  - ")(0), p.tCharge)
                    Catch ex As Exception

                    End Try
                Next
                ViewBag.OrdiniPaese = JsonConvert.SerializeObject(paesi)
                ViewBag.JSON = JsonConvert.SerializeObject(ret)
                For Each a In db.Ordini.Where(Function(x) x.Anno = DateTime.Now.Year)
                    If a.Stato = "IT  - ITALIA" Then
                        ViewBag.TotITA = ViewBag.TotITA + a.Valore_Totale
                    Else
                        ViewBag.TotEST = ViewBag.TotEST + a.Valore_Totale
                    End If
                Next
                Return View(ret)
            Else
                Dim data = db.Ordini.Where(Function(s) s.Anno = id).GroupBy(Function(x) New With {x.Anno, x.Mese, x.Tipo_Ordine}, Function(key, group) New With {Key .yr = key.Anno, Key .mnth = key.Mese, Key .type = key.Tipo_Ordine, Key .tCharge = group.Sum(Function(k) k.Valore_Netto)}).OrderBy(Function(y) y.yr).ThenBy(Function(z) z.mnth).ToList()
                Dim list As New List(Of OrdineViewModel)
                For Each d In data
                    Dim ord As New OrdineViewModel With {
                        .Anno = d.yr,
                        .Mese = d.mnth,
                        .tCharge = d.tCharge,
                        .Tipo_Ordine = d.type
                        }
                    list.Add(ord)
                Next
                Dim ret As New OrdineListaViewmodel With {.Lista = list}
                Dim t = db.Ordini.Where(Function(s) s.Anno = id).GroupBy(Function(x) New With {x.Stato}, Function(key, group) New With {Key .mnth = key.Stato, Key .tCharge = group.Count.ToString}).OrderBy(Function(z) z.mnth).ToList()
                'Dim head = New String() {"mnth", "tCharge"}
                't.Insert(0, head)
                ViewBag.OrdiniMese = JsonConvert.SerializeObject(db.Ordini.Where(Function(s) s.Anno = id).GroupBy(Function(x) New With {x.Mese}, Function(key, group) New With {Key .mnth = key.Mese, Key .tCharge = group.Sum(Function(k) k.Valore_Totale)}).OrderBy(Function(z) z.mnth).ToArray())
                ViewBag.OrdiniMeseAnnoPrima1 = JsonConvert.SerializeObject(db.Ordini.Where(Function(s) s.Anno = id - 1).GroupBy(Function(x) New With {x.Mese}, Function(key, group) New With {Key .mnth = key.Mese, Key .tCharge = group.Sum(Function(k) k.Valore_Totale)}).OrderBy(Function(z) z.mnth).ToArray())
                ViewBag.OrdiniMeseAnnoPrima2 = JsonConvert.SerializeObject(db.Ordini.Where(Function(s) s.Anno = id - 2).GroupBy(Function(x) New With {x.Mese}, Function(key, group) New With {Key .mnth = key.Mese, Key .tCharge = group.Sum(Function(k) k.Valore_Totale)}).OrderBy(Function(z) z.mnth).ToArray())
                ViewBag.OrdiniMarca = JsonConvert.SerializeObject(db.Ordini.Where(Function(s) s.Anno = id).GroupBy(Function(x) New With {x.Tipo_Ordine}, Function(key, group) New With {Key .mnth = key.Tipo_Ordine, Key .tCharge = group.Sum(Function(k) k.Valore_Netto)}).OrderBy(Function(z) z.mnth).ToArray())
                ViewBag.OrdiniPaese = JsonConvert.SerializeObject(t)
                ViewBag.JSON = JsonConvert.SerializeObject(ret)
                For Each a In db.Ordini.Where(Function(x) x.Anno = id)
                    If a.Stato = "IT  - ITALIA" Then
                        ViewBag.TotITA = ViewBag.TotITA + a.Valore_Totale
                    Else
                        ViewBag.TotEST = ViewBag.TotEST + a.Valore_Totale
                    End If
                Next
                Return View(ret)
            End If

        End Function

        ' GET: Ordini/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim ordine As Ordine = db.Ordini.Find(id)
            If IsNothing(ordine) Then
                Return HttpNotFound()
            End If
            Return View(ordine)
        End Function

        ' GET: Ordini/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Ordini/Create
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,Mese,Anno,CodCliente,NomeCliente,Stato,Regione_uno,Regione_due,Tipo_Ordine,Valore_Netto,Valore_Netto,Provenienza")> ByVal ordine As Ordine) As ActionResult
            If ModelState.IsValid Then
                db.Ordini.Add(ordine)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(ordine)
        End Function
        ' POST: Ordini/Create
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function CreateExcel() As ActionResult
            For Each UploadedFileName In Request.Files
                Try
                    Dim UploadedFile As HttpPostedFileBase = Request.Files(UploadedFileName)

                    If UploadedFile IsNot Nothing AndAlso UploadedFile.ContentLength > 0 Then
                        Dim workbook As XSSFWorkbook = New XSSFWorkbook(UploadedFile.InputStream)
                        ' =================================================== ANAGRAFICA
                        Try
                            Try
                                For c = 1 To workbook.NumberOfSheets
                                    Dim ws As XSSFSheet = workbook.GetSheetAt(c)
                                    Dim anno = ws.SheetName.Split(" ")(1)
                                    With ws
                                        For i = 1 To .LastRowNum
                                            Try
                                                Dim o As New Ordine
                                                Try
                                                    Select Case GetCellValue(.GetRow(i), 0)
                                                        Case "1"
                                                            o.Mese = Mese.Gennaio
                                                        Case "2"
                                                            o.Mese = Mese.Febbraio
                                                        Case "3"
                                                            o.Mese = Mese.Marzo
                                                        Case "4"
                                                            o.Mese = Mese.Aprile
                                                        Case "5"
                                                            o.Mese = Mese.Maggio
                                                        Case "6"
                                                            o.Mese = Mese.Giugno
                                                        Case "7"
                                                            o.Mese = Mese.Luglio
                                                        Case "8"
                                                            o.Mese = Mese.Agosto
                                                        Case "9"
                                                            o.Mese = Mese.Settembre
                                                        Case "10"
                                                            o.Mese = Mese.Ottobre
                                                        Case "11"
                                                            o.Mese = Mese.Novembre
                                                        Case "12"
                                                            o.Mese = Mese.Dicembre
                                                    End Select
                                                Catch ex As Exception

                                                End Try

                                                Try
                                                    Dim Main_Cli() = GetCellValue(.GetRow(i), 1).ToString.Split(" - ")
                                                    o.CodCliente = Main_Cli(0)
                                                    o.NomeCliente = Main_Cli(1)
                                                Catch ex As Exception

                                                End Try
                                                Try
                                                    o.Anno = Convert.ToInt32(anno)
                                                    o.Stato = GetCellValue(ws.GetRow(i), 2).ToString
                                                    o.Regione_uno = GetCellValue(ws.GetRow(i), 3).ToString
                                                    o.Regione_due = GetCellValue(ws.GetRow(i), 4).ToString
                                                Catch ex As Exception

                                                End Try

                                                Try
                                                    Select Case GetCellValue(.GetRow(i), 5)
                                                        Case "A/R   - Accessori e ricambi"
                                                            o.Tipo_Ordine = Tipo_Ordine.AccessoriERicambi
                                                        Case "CMT   - Materiale CMT"
                                                            o.Tipo_Ordine = Tipo_Ordine.CMT
                                                        Case "DRILL - Materiale Drillmatic"
                                                            o.Tipo_Ordine = Tipo_Ordine.Drillmatic
                                                        Case "ISA   - Materiale ISA"
                                                            o.Tipo_Ordine = Tipo_Ordine.ISA
                                                        Case "MPA   - Materiale MPA"
                                                            o.Tipo_Ordine = Tipo_Ordine.MPA
                                                        Case "UNI   - Materiali unistand"
                                                            o.Tipo_Ordine = Tipo_Ordine.Unistand
                                                        Case Else
                                                            o.Tipo_Ordine = Tipo_Ordine.None
                                                    End Select
                                                Catch ex As Exception

                                                End Try
                                                Try
                                                    o.Valore_Netto = GetCellValue(ws.GetRow(i), 6)
                                                    o.Valore_Netto = GetCellValue(ws.GetRow(i), 7)
                                                Catch ex As Exception

                                                End Try
                                                If o.Stato = "IT - ITALIA" Then
                                                    o.Provenienza = 1
                                                Else
                                                    o.Provenienza = 0
                                                End If
                                                Dim ord_old = db.Ordini.Where(Function(x) x.CodCliente = o.CodCliente And x.Valore_Netto = o.Valore_Netto And x.Tipo_Ordine = o.Tipo_Ordine And x.Valore_Netto = o.Valore_Netto).FirstOrDefault
                                                If ord_old Is Nothing Then
                                                    db.Ordini.Add(o)
                                                    db.SaveChanges()
                                                End If
                                            Catch ex As Exception

                                            End Try
                                        Next
                                    End With
                                Next


                            Catch ex As Exception

                            End Try

                        Catch ex As Exception
                        End Try
                    End If

                Catch ex As Exception

                End Try

            Next

        End Function
        ' GET: Ordini/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim ordine As Ordine = db.Ordini.Find(id)
            If IsNothing(ordine) Then
                Return HttpNotFound()
            End If
            Return View(ordine)
        End Function

        ' POST: Ordini/Edit/5
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,Mese,Anno,CodCliente,NomeCliente,Stato,Regione_uno,Regione_due,Tipo_Ordine,Valore_Netto,Valore_Netto,Provenienza")> ByVal ordine As Ordine) As ActionResult
            If ModelState.IsValid Then
                db.Entry(ordine).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(ordine)
        End Function

        ' GET: Ordini/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim ordine As Ordine = db.Ordini.Find(id)
            If IsNothing(ordine) Then
                Return HttpNotFound()
            End If
            Return View(ordine)
        End Function

        ' POST: Ordini/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim ordine As Ordine = db.Ordini.Find(id)
            db.Ordini.Remove(ordine)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function
        Private Function GetCellValue(row As IRow, col As Integer, Optional OpID As String = vbNullString, Optional OpName As String = vbNullString) As Object
            Dim result As String = vbNullString

            Try

                If Not IsNothing(row) Then
                    Dim cell As ICell = row.GetCell(col)
                    If Not IsNothing(cell) Then
                        Select Case cell.CellType
                            Case CellType.Numeric
                                If DateUtil.IsCellDateFormatted(cell) Then
                                    Return cell.DateCellValue
                                Else
                                    Return cell.NumericCellValue
                                End If
                            Case CellType.String
                                Return cell.StringCellValue.Trim
                            Case CellType.Boolean
                                Return cell.BooleanCellValue
                            Case CellType.Formula
                                Return cell.NumericCellValue
                            Case CellType.Blank, CellType.Error, CellType.Unknown
                                Return vbNullString
                        End Select
                    End If
                End If
            Catch ex As Exception

            End Try
            db.SaveChanges()

            Return result
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
