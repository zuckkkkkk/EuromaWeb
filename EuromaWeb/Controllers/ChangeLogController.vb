Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports EuromaWeb
Imports Microsoft.AspNet.Identity

Namespace Controllers
    Public Class ChangeLogController
        Inherits System.Web.Mvc.Controller

        Private db As New EuromaModels

        ' GET: ChangeLog
        Function Index() As ActionResult
            Return View(db.ChangeLog.ToList())
        End Function

        ' GET: ChangeLog/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim changeLog As ChangeLog = db.ChangeLog.Find(id)
            If IsNothing(changeLog) Then
                Return HttpNotFound()
            End If
            Return View(changeLog)
        End Function

        ' GET: ChangeLog/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: ChangeLog/Create
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,Title,Descrizione,Release_Date,StartDate,EndDate")> ByVal changeLog As ChangeLog) As ActionResult
            If ModelState.IsValid Then
                db.ChangeLog.Add(changeLog)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(changeLog)
        End Function

        ' GET: ChangeLog/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim changeLog As ChangeLog = db.ChangeLog.Find(id)
            If IsNothing(changeLog) Then
                Return HttpNotFound()
            End If
            Return View(changeLog)
        End Function
        Function AggiornaUtente(ByVal id As Integer) As JsonResult
            Try
                Dim changeLog = db.ChangeLog.Where(Function(x) x.Id = id).First
                Dim u = User.Identity.GetUserId
                Dim v_cl As New VisualizzazioneChangeLog With {
                    .changeLog_id = changeLog.Id,
                    .ReaedingDate = DateTime.Now,
                    .User = u
                }
                db.VisualizzazioneChangeLog.Add(v_cl)
                db.SaveChanges()
                Return Json(New With {.ok = True}, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                Return Json(New With {.ok = False}, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(New With {.ok = False}, JsonRequestBehavior.AllowGet)
        End Function
        Function Aggiornamenti() As JsonResult
            Dim adesso = DateTime.Now
            Dim u = User.Identity.GetUserId
            If Not IsNothing(u) Then
                If db.ChangeLog.Where(Function(x) x.StartDate < adesso And x.EndDate > adesso).Count > 0 Then
                    Dim changeLog = db.ChangeLog.Where(Function(x) x.StartDate < adesso And x.EndDate > adesso).First
                    Dim clear = 1
                    If db.VisualizzazioneChangeLog.Where(Function(x) x.changeLog_id = changeLog.Id And x.User = u).Count = 0 Then
                        Dim result As New Object
                        result = New With {
                            .Titolo = changeLog.Title,
                            .Descrizione = changeLog.Descrizione,
                            .Release = changeLog.Release_Date,
                            .id = changeLog.Id
                        }
                        Return Json(New With {.esiste = True, .data = result}, JsonRequestBehavior.AllowGet)
                    Else
                        Return Json(New With {.esiste = False}, JsonRequestBehavior.AllowGet)
                    End If
                End If
            Else
                Return Json(New With {.esiste = False}, JsonRequestBehavior.AllowGet)
            End If
            Return Json(New With {.esiste = False}, JsonRequestBehavior.AllowGet)
        End Function
        ' POST: ChangeLog/Edit/5
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,Title,Descrizione,Release_Date,StartDate,EndDate")> ByVal changeLog As ChangeLog) As ActionResult
            If ModelState.IsValid Then
                db.Entry(changeLog).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(changeLog)
        End Function

        ' GET: ChangeLog/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim changeLog As ChangeLog = db.ChangeLog.Find(id)
            If IsNothing(changeLog) Then
                Return HttpNotFound()
            End If
            Return View(changeLog)
        End Function

        ' POST: ChangeLog/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim changeLog As ChangeLog = db.ChangeLog.Find(id)
            db.ChangeLog.Remove(changeLog)
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
