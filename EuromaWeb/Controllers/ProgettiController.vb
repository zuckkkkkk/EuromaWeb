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
    Public Class ProgettiController
        Inherits System.Web.Mvc.Controller

        Private db As New EuromaModels

        ' GET: Progetti
        Function Index() As ActionResult
            Return View(db.Progetti.ToList())
        End Function

        ' GET: Progetti/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim progetto As Progetto = db.Progetti.Find(id)
            If IsNothing(progetto) Then
                Return HttpNotFound()
            End If
            Return View(progetto)
        End Function

        ' GET: Progetti/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Progetti/Create
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,OC,StartDate,EndDate,Cliente,Brand,Codice,Note_Pezzo,Id_Last_Storico_Progetto")> ByVal progetto As Progetto) As ActionResult
            If ModelState.IsValid Then
                db.Progetti.Add(progetto)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(progetto)
        End Function

        ' GET: Progetti/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim progetto As Progetto = db.Progetti.Find(id)
            If IsNothing(progetto) Then
                Return HttpNotFound()
            End If
            Return View(progetto)
        End Function

        ' POST: Progetti/Edit/5
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,OC,StartDate,EndDate,Cliente,Brand,Codice,Note_Pezzo,Id_Last_Storico_Progetto")> ByVal progetto As Progetto) As ActionResult
            If ModelState.IsValid Then
                db.Entry(progetto).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(progetto)
        End Function

        ' GET: Progetti/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim progetto As Progetto = db.Progetti.Find(id)
            If IsNothing(progetto) Then
                Return HttpNotFound()
            End If
            Return View(progetto)
        End Function

        ' POST: Progetti/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim progetto As Progetto = db.Progetti.Find(id)
            db.Progetti.Remove(progetto)
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
