Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports EuromaWeb

Namespace Controllers
    Public Class MacchineController
        Inherits System.Web.Mvc.Controller

        Private db As New EuromaModels

        ' GET: Macchine
        Function Index() As ActionResult
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
