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
            Return View(db.Macchine.ToList())
        End Function

        ' GET: Macchine/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim macchine As Macchine = db.Macchine.Find(id)
            If IsNothing(macchine) Then
                Return HttpNotFound()
            End If
            Return View(macchine)
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
