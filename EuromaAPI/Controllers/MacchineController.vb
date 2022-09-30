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
    Public Class MacchineController
        Inherits System.Web.Http.ApiController

        Private db As New EuromaAPI

        ' GET: api/Macchine
        Function GetMacchine() As IQueryable(Of Macchine)
            Return db.Macchine
        End Function

        ' GET: api/Macchine/5
        <ResponseType(GetType(Macchine))>
        Function GetMacchine(ByVal id As Integer) As IHttpActionResult
            Dim macchine As Macchine = db.Macchine.Find(id)
            If IsNothing(macchine) Then
                Return NotFound()
            End If

            Return Ok(macchine)
        End Function

        ' PUT: api/Macchine/5
        <ResponseType(GetType(Void))>
        Function PutMacchine(ByVal id As Integer, ByVal macchine As Macchine) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = macchine.Id Then
                Return BadRequest()
            End If

            db.Entry(macchine).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (MacchineExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/Macchine
        <ResponseType(GetType(Macchine))>
        Function PostMacchine(ByVal macchine As Macchine) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.Macchine.Add(macchine)
            db.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.id = macchine.Id}, macchine)
        End Function

        ' DELETE: api/Macchine/5
        <ResponseType(GetType(Macchine))>
        Function DeleteMacchine(ByVal id As Integer) As IHttpActionResult
            Dim macchine As Macchine = db.Macchine.Find(id)
            If IsNothing(macchine) Then
                Return NotFound()
            End If

            db.Macchine.Remove(macchine)
            db.SaveChanges()

            Return Ok(macchine)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function MacchineExists(ByVal id As Integer) As Boolean
            Return db.Macchine.Count(Function(e) e.Id = id) > 0
        End Function
    End Class
End Namespace