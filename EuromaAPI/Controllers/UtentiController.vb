Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Description
Imports EuromaAPI

Namespace Controllers
    Public Class UtentiController
        Inherits System.Web.Http.ApiController

        Private db As New EuromaAPI

        ' GET: api/Utenti
        Function GetUtenti() As IQueryable(Of Utenti)
            Return db.Utenti
        End Function

        ' GET: api/Utenti/5
        <ResponseType(GetType(Utenti))>
        Function GetUtenti(ByVal id As Integer) As IHttpActionResult
            Dim utenti As Utenti = db.Utenti.Find(id)
            If IsNothing(utenti) Then
                Return NotFound()
            End If

            Return Ok(utenti)
        End Function

        ' PUT: api/Utenti/5
        <ResponseType(GetType(Void))>
        Function PutUtenti(ByVal id As Integer, ByVal utenti As Utenti) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = utenti.Id Then
                Return BadRequest()
            End If

            db.Entry(utenti).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (UtentiExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/Utenti
        <ResponseType(GetType(Utenti))>
        Function PostUtenti(ByVal utenti As Utenti) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.Utenti.Add(utenti)
            db.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.id = utenti.Id}, utenti)
        End Function

        ' DELETE: api/Utenti/5
        <ResponseType(GetType(Utenti))>
        Function DeleteUtenti(ByVal id As Integer) As IHttpActionResult
            Dim utenti As Utenti = db.Utenti.Find(id)
            If IsNothing(utenti) Then
                Return NotFound()
            End If

            db.Utenti.Remove(utenti)
            db.SaveChanges()

            Return Ok(utenti)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function UtentiExists(ByVal id As Integer) As Boolean
            Return db.Utenti.Count(Function(e) e.Id = id) > 0
        End Function
    End Class
End Namespace