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
    Public Class LavoriController
        Inherits System.Web.Http.ApiController

        Private db As New EuromaAPI

        ' GET: api/Lavori
        Function GetLavori() As IQueryable(Of Lavori)
            Return db.Lavori
        End Function

        ' GET: api/Lavori/5
        <ResponseType(GetType(Lavori))>
        Function GetLavori(ByVal id As Integer) As IHttpActionResult
            Dim lavori As Lavori = db.Lavori.Find(id)
            If IsNothing(lavori) Then
                Return NotFound()
            End If

            Return Ok(lavori)
        End Function

        ' PUT: api/Lavori/5
        <ResponseType(GetType(Void))>
        Function PutLavori(ByVal id As Integer, ByVal lavori As Lavori) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = lavori.Id Then
                Return BadRequest()
            End If

            db.Entry(lavori).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (LavoriExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/Lavori
        <ResponseType(GetType(Lavori))>
        Function PostLavori(ByVal lavori As Lavori) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.Lavori.Add(lavori)
            db.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.id = lavori.Id}, lavori)
        End Function

        ' DELETE: api/Lavori/5
        <ResponseType(GetType(Lavori))>
        Function DeleteLavori(ByVal id As Integer) As IHttpActionResult
            Dim lavori As Lavori = db.Lavori.Find(id)
            If IsNothing(lavori) Then
                Return NotFound()
            End If

            db.Lavori.Remove(lavori)
            db.SaveChanges()

            Return Ok(lavori)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function LavoriExists(ByVal id As Integer) As Boolean
            Return db.Lavori.Count(Function(e) e.Id = id) > 0
        End Function
    End Class
End Namespace