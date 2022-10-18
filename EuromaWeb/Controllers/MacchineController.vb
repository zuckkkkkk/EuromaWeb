Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports EuromaWeb
Imports Microsoft.AspNet.Identity
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel

Namespace Controllers
    Public Class MacchineController
        Inherits System.Web.Mvc.Controller

        Private db As New EuromaModels

        ' GET: Macchine
        <Authorize>
        Function Index(id As String) As ActionResult
            If IsNothing(id) Then
                id = db.Macchine.First.Macchina
            End If
            ViewBag.idMacchina = id
            Dim lista = db.Macchine.ToList
            ViewBag.listaMacchine = New SelectList(lista, "Macchina", "Macchina")
            Return View()
        End Function

        ' GET: Macchine/Details/5
        <HttpPost>
        <Authorize>
        Function DetailsPost(ByVal id As String) As JsonResult
            If IsNothing(id) Then
                Return Json(New With {.ok = False})
            End If
            Dim macchine As Macchine = db.Macchine.Where(Function(x) x.Macchina = id).First
            If IsNothing(macchine) Then
                Return Json(New With {.ok = False})
            End If
            Dim lastActivity = db.DatiMacchina.Where(Function(X) X.Macchina = id).OrderByDescending(Function(x) x.id).First '.Reverse().First
            Dim DicitonaryMacchina As New Dictionary(Of String, Integer)
            Dim DictionaryOperatore As New Dictionary(Of String, Integer)
            Dim DictionaryStato As New Dictionary(Of String, Integer)
            Dim ListaDisegni As New List(Of ListaDisegniViewModel)
            Dim ListaRunningTime As New Dictionary(Of String, Integer)
            Dim dataOld = DateTime.Now.AddDays(-7)
            Dim Lista = db.DatiMacchina.Where(Function(x) x.Macchina = id And x.Data < DateTime.Now And x.Data > dataOld).ToList
            For Each disegno In Lista
                'Lista Disegni
                If ListaDisegni.Where(Function(x) x.CodDisegno = disegno.Programma).Count = 0 Then
                    Dim lastDis = db.DatiMacchina.Where(Function(x) x.Programma = disegno.Programma).OrderByDescending(Function(x) x.id).First
                    ListaDisegni.Add(New ListaDisegniViewModel With {
                        .CodDisegno = disegno.Programma,
                        .DescDisegno = disegno.ProgrammaDesc,
                        .FirstStart = disegno.Data.ToString.Split(" ")(0),
                        .LastStart = lastDis.Data.ToString.Split(" ")(0)
                    })
                End If
                'End Disegni
                'Pie chart attività
                If DicitonaryMacchina.ContainsKey(disegno.ModalitaMacchina) Then
                    DicitonaryMacchina.Item(disegno.ModalitaMacchina) = DicitonaryMacchina.Item(disegno.ModalitaMacchina) + 2
                Else
                    DicitonaryMacchina.Add(disegno.ModalitaMacchina, 2)
                End If
                If DictionaryOperatore.ContainsKey(disegno.ModalitaControllo) Then
                    DictionaryOperatore.Item(disegno.ModalitaControllo) = DictionaryOperatore.Item(disegno.ModalitaControllo) + 2
                Else
                    DictionaryOperatore.Add(disegno.ModalitaControllo, 2)
                End If
                If DictionaryStato.ContainsKey(disegno.EsecuzioneProgramma) Then
                    DictionaryStato.Item(disegno.EsecuzioneProgramma) = DictionaryStato.Item(disegno.EsecuzioneProgramma) + 2
                Else
                    DictionaryStato.Add(disegno.EsecuzioneProgramma, 2)
                End If
                'End Pie chart attività
                If Not ListaRunningTime.ContainsKey(disegno.Data.ToString.Split(" ")(1)) Then
                    ListaRunningTime.Add(disegno.Data.ToString.Split(" ")(1), disegno.LpSpindleRunTime / 60)
                End If
            Next
            Try
                Dim macchina As New MacchinaViewModel With {
                .LastUpdate = lastActivity.Data.ToString,
                .ActualProgram = lastActivity.Programma,
                .ActualProgramDesc = lastActivity.ProgrammaDesc,
                .ActualState = lastActivity.EsecuzioneProgramma,
                .CodMacchina = id,
                .DescMacchina = macchine.Descrizione_Macchina,
                .DicitonaryMacchina = DicitonaryMacchina,
                .DictionaryOperatore = DictionaryOperatore,
                .DictionaryStato = DictionaryStato,
                .ListaDisegni = ListaDisegni,
                .Path3d = macchine.Path_3d,
                .TempoComplessivo = ListaRunningTime
            }
                Return Json(New With {.ok = True, .data = macchina})
            Catch ex As Exception

            End Try

        End Function
        <HttpPost>
        <Authorize>
        Function DetailsPostComplessivo(ByVal id As String, ByVal macchina As String) As JsonResult
            If IsNothing(id) Then
                Return Json(New With {.ok = False})
            End If
            Dim macchine As Macchine = db.Macchine.Where(Function(x) x.Macchina = macchina).First
            If IsNothing(macchine) Then
                Return Json(New With {.ok = False})
            End If
            Dim lastActivity = db.DatiMacchina.Where(Function(X) X.Macchina = macchina).OrderByDescending(Function(x) x.id).First '.Reverse().First
            Dim DicitonaryMacchina As New Dictionary(Of String, Integer)
            Dim DictionaryOperatore As New Dictionary(Of String, Integer)
            Dim DictionaryStato As New Dictionary(Of String, Integer)
            Dim ListaDisegni As New List(Of ListaDisegniViewModel)
            Dim ListaRunningTime As New Dictionary(Of String, Integer)
            Dim dataOld = DateTime.Now.AddDays(-7)
            Dim Lista = db.DatiMacchina.Where(Function(x) x.Macchina = macchina And x.Data < DateTime.Now And x.Data > dataOld).ToList
            For Each disegno In Lista
                If Not ListaRunningTime.ContainsKey(disegno.Data.ToString.Split(" ")(1)) Then
                    Select Case id
                        Case "1"
                            ListaRunningTime.Add(disegno.Data.ToString.Split(" ")(1), disegno.LpTotalCuttinTime / 60)
                        Case "2"
                            ListaRunningTime.Add(disegno.Data.ToString.Split(" ")(1), disegno.LpTotalSpindleRuntime / 60)
                        Case "3"
                            ListaRunningTime.Add(disegno.Data.ToString.Split(" ")(1), disegno.LpTotalOperatingTime / 60)
                        Case "4"
                            ListaRunningTime.Add(disegno.Data.ToString.Split(" ")(1), disegno.LpTotalRunningTime / 60)
                    End Select
                End If
            Next
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()

                db.Audit.Add(New Audit With {
                                         .Livello = TipoAuditLivello.Info,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Aggiornamento chart complessivo machcina",
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                           })
                db.SaveChanges()
                Return Json(New With {.ok = True, .data = ListaRunningTime})
            Catch ex As Exception

            End Try

        End Function
        Function DownloadModalitaMacchina(ByVal id As String) As FileResult
            Dim fs As New FileStream(Server.MapPath("\Content\Macchine\Template_Macchine.xlsx"), FileMode.Open, FileAccess.Read)
            Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
            Dim ws As XSSFSheet = workbook.GetSheetAt(0)
            'Start Pop
            Dim i As Integer = 3
            Dim baserow As IRow = ws.GetRow(2)
            'Dim baserow As IRow = ws.GetRow(2)
            Dim ms As New MemoryStream
            Dim ms1 As New MemoryStream
            Dim dataOggi = DateTime.Now
            Dim dataweekago = dataOggi.AddDays(-7)
            Dim datiMacchina = db.DatiMacchina.Where(Function(x) x.Macchina = id And x.Data >= dataweekago And x.Data <= dataOggi).ToList
            ws.GetRow(0).GetCell(0).SetCellValue("Dati dal " + dataweekago.ToString.Split(" ")(0) + " al " + dataOggi.ToString.Split(" ")(0))
            ws.GetRow(1).GetCell(0).SetCellValue("Modalità Macchina")
            'Riga Intestazione
            Try
                Try
                    For Each l In datiMacchina
                        Dim r As IRow = ws.CreateRow(i)
                        For j = 0 To 2
                            r.CreateCell(j).CellStyle = baserow.GetCell(j).CellStyle
                        Next
                        Try
                            r.GetCell(0).SetCellValue(l.Macchina)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(1).SetCellValue(l.Data.ToString)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(2).SetCellValue(l.ModalitaMacchina.ToString)
                        Catch ex As Exception

                        End Try

                        i = i + 1
                    Next
                Catch ex As Exception

                End Try
                'Intestazione

                'Dati rilevati

                workbook.Write(ms)

                Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - MODALITA_MACCHINA_" & id & ".xlsx")
            Catch ex As Exception

            End Try


        End Function
        Function DownloadModalitaOperatore(ByVal id As String) As FileResult
            Dim fs As New FileStream(Server.MapPath("\Content\Macchine\Template_Macchine.xlsx"), FileMode.Open, FileAccess.Read)
            Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
            Dim ws As XSSFSheet = workbook.GetSheetAt(0)
            'Start Pop
            Dim i As Integer = 3
            Dim baserow As IRow = ws.GetRow(2)
            'Dim baserow As IRow = ws.GetRow(2)
            Dim ms As New MemoryStream
            Dim ms1 As New MemoryStream
            Dim dataOggi = DateTime.Now
            Dim dataweekago = dataOggi.AddDays(-7)
            Dim datiMacchina = db.DatiMacchina.Where(Function(x) x.Macchina = id And x.Data >= dataweekago And x.Data <= dataOggi).ToList
            ws.GetRow(0).GetCell(0).SetCellValue("Dati dal " + dataweekago.ToString.Split(" ")(0) + " al " + dataOggi.ToString.Split(" ")(0))
            ws.GetRow(1).GetCell(0).SetCellValue("Stato Programmi")
            'Riga Intestazione
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()

                Try
                    For Each l In datiMacchina
                        Dim r As IRow = ws.CreateRow(i)
                        For j = 0 To 2
                            r.CreateCell(j).CellStyle = baserow.GetCell(j).CellStyle
                        Next
                        Try
                            r.GetCell(0).SetCellValue(l.Macchina)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(1).SetCellValue(l.Data.ToString)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(2).SetCellValue(l.EsecuzioneProgramma.ToString)
                        Catch ex As Exception

                        End Try

                        i = i + 1
                    Next
                Catch ex As Exception

                End Try
                'Intestazione

                'Dati rilevati

                workbook.Write(ms)
                db.Audit.Add(New Audit With {
                                         .Livello = TipoAuditLivello.Info,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Download dettagli modalita operatore",
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                           })
                db.SaveChanges()
                Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - MODALITA_OPERATORE_" & id & ".xlsx")
            Catch ex As Exception

            End Try


        End Function
        Function DownloadModalitaStato(ByVal id As String) As FileResult
            Dim fs As New FileStream(Server.MapPath("\Content\Macchine\Template_Macchine.xlsx"), FileMode.Open, FileAccess.Read)
            Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
            Dim ws As XSSFSheet = workbook.GetSheetAt(0)
            'Start Pop
            Dim i As Integer = 3
            Dim baserow As IRow = ws.GetRow(2)
            'Dim baserow As IRow = ws.GetRow(2)
            Dim ms As New MemoryStream
            Dim ms1 As New MemoryStream
            Dim dataOggi = DateTime.Now
            Dim dataweekago = dataOggi.AddDays(-7)
            Dim datiMacchina = db.DatiMacchina.Where(Function(x) x.Macchina = id And x.Data >= dataweekago And x.Data <= dataOggi).ToList
            ws.GetRow(0).GetCell(0).SetCellValue("Dati dal " + dataweekago.ToString.Split(" ")(0) + " al " + dataOggi.ToString.Split(" ")(0))
            ws.GetRow(1).GetCell(0).SetCellValue("Modalità Operatore")
            'Riga Intestazione
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()

                Try
                    For Each l In datiMacchina
                        Dim r As IRow = ws.CreateRow(i)
                        For j = 0 To 2
                            r.CreateCell(j).CellStyle = baserow.GetCell(j).CellStyle
                        Next
                        Try
                            r.GetCell(0).SetCellValue(l.Macchina)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(1).SetCellValue(l.Data.ToString)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(2).SetCellValue(l.ModalitaControllo.ToString)
                        Catch ex As Exception

                        End Try

                        i = i + 1
                    Next
                Catch ex As Exception

                End Try
                'Intestazione

                'Dati rilevati

                workbook.Write(ms)
                db.Audit.Add(New Audit With {
                                         .Livello = TipoAuditLivello.Info,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Download dettagli stato macchina",
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                           })
                db.SaveChanges()
                Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - STATO_MACCHINA_" & id & ".xlsx")
            Catch ex As Exception

            End Try
        End Function
        Function DownloadComplessivoDati(ByVal macchina As String) As FileResult
            Dim fs As New FileStream(Server.MapPath("\Content\Macchine\Template_Macchine_Complessivo.xlsx"), FileMode.Open, FileAccess.Read)
            Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
            Dim ws As XSSFSheet = workbook.GetSheetAt(0)
            'Start Pop
            Dim i As Integer = 3
            Dim baserow As IRow = ws.GetRow(2)
            'Dim baserow As IRow = ws.GetRow(2)
            Dim ms As New MemoryStream
            Dim ms1 As New MemoryStream
            Dim dataOggi = DateTime.Now
            Dim dataweekago = dataOggi.AddDays(-7)
            Dim datiMacchina = db.DatiMacchina.Where(Function(x) x.Macchina = macchina And x.Data >= dataweekago And x.Data <= dataOggi).ToList
            ws.GetRow(0).GetCell(0).SetCellValue("Dati dal " + dataweekago.ToString.Split(" ")(0) + " al " + dataOggi.ToString.Split(" ")(0))
            ws.GetRow(1).GetCell(0).SetCellValue("Tempi Complessivi")
            'Riga Intestazione
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()

                Try
                    For Each l In datiMacchina
                        Dim r As IRow = ws.CreateRow(i)
                        For j = 0 To 5
                            r.CreateCell(j).CellStyle = baserow.GetCell(j).CellStyle
                        Next
                        Try
                            r.GetCell(0).SetCellValue(l.Macchina)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(1).SetCellValue(l.Data.ToString)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(2).SetCellValue(l.LpTotalCuttinTime.ToString)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(3).SetCellValue(l.LpTotalSpindleRuntime.ToString)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(4).SetCellValue(l.LpTotalOperatingTime.ToString)
                        Catch ex As Exception

                        End Try
                        Try
                            r.GetCell(5).SetCellValue(l.LpTotalRunningTime.ToString)
                        Catch ex As Exception

                        End Try
                        i = i + 1
                    Next
                Catch ex As Exception

                End Try
                workbook.Write(ms)
                db.Audit.Add(New Audit With {
                                         .Livello = TipoAuditLivello.Info,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Download dettagli complessivi macchina",
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = macchina}),
                                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                           })
                db.SaveChanges()
                Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - TEMPI_COMPLESSIVI_" & macchina & ".xlsx")
            Catch ex As Exception

            End Try
        End Function
        ' GET: Macchine/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Macchine/Create
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="id,Macchina,Descrizione_Macchina,Path_3d")> ByVal macchine As Macchine) As ActionResult
            If ModelState.IsValid Then
                db.Macchine.Add(macchine)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(macchine)
        End Function

        ' GET: Macchine/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim macchine As Macchine = db.Macchine.Find(id)
            If IsNothing(macchine) Then
                Return HttpNotFound()
            End If
            Return View(macchine)
        End Function

        ' POST: Macchine/Edit/5
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="id,Macchina,Descrizione_Macchina,Path_3d")> ByVal macchine As Macchine) As ActionResult
            If ModelState.IsValid Then
                db.Entry(macchine).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(macchine)
        End Function

        ' GET: Macchine/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim macchine As Macchine = db.Macchine.Find(id)
            If IsNothing(macchine) Then
                Return HttpNotFound()
            End If
            Return View(macchine)
        End Function

        ' POST: Macchine/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim macchine As Macchine = db.Macchine.Find(id)
            db.Macchine.Remove(macchine)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
