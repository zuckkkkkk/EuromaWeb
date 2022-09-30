Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.ModelBinding
Imports System.Web.Http.OData
Imports System.Web.Http.OData.Routing
Imports EuromaWeb
Imports Microsoft.AspNet.OData

Namespace Controllers

    'Per aggiungere una route relativa a questo controller, può essere necessario apportare altre modifiche alla classe WebApiConfig. Unire queste istruzioni nel metodo Register della classe WebApiConfig. Tenere presente che per gli URL OData viene fatta distinzione tra maiuscole e minuscole.

    'Imports System.Web.Http.OData.Builder
    'Imports System.Web.Http.OData.Extensions
    'Dim builder As New ODataConventionModelBuilder
    'builder.EntitySet(Of Ordine)("Ordines")
    'config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel())

    Public Class OrdinesController
        Inherits ODataController

        Private db As New EuromaModels

        ' GET: odata/Ordines
        <EnableQuery>
        Function GetOrdines() As IQueryable(Of Ordine)
            Return db.Ordini
        End Function

        ' GET: odata/Ordines(5)
        <EnableQuery>
        Function GetOrdine(<FromODataUri> key As Integer) As SingleResult(Of Ordine)
            Return SingleResult.Create(db.Ordini.Where(Function(ordine) ordine.Id = key))
        End Function

        ' PUT: odata/Ordines(5)
        Function Put(<FromODataUri> ByVal key As Integer, ByVal patchValue As Delta(Of Ordine)) As IHttpActionResult
            'Validate(patchValue.GetEntity())

            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            Dim ordine As Ordine = db.Ordini.Find(key)
            If IsNothing(ordine) Then
                Return NotFound()
            End If

            patchValue.Put(ordine)

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (OrdineExists(key)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return Updated(ordine)
        End Function

        ' POST: odata/Ordines
        Function Post(ByVal ordine As Ordine) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.Ordini.Add(ordine)
            db.SaveChanges()

            Return Created(ordine)
        End Function

        ' PATCH: odata/Ordines(5)
        <AcceptVerbs("PATCH", "MERGE")>
        Function Patch(<FromODataUri> ByVal key As Integer, ByVal patchValue As Delta(Of Ordine)) As IHttpActionResult
            'Validate(patchValue.GetEntity())

            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            Dim ordine As Ordine = db.Ordini.Find(key)
            If IsNothing(ordine) Then
                Return NotFound()
            End If

            patchValue.Patch(ordine)

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (OrdineExists(key)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return Updated(ordine)
        End Function

        ' DELETE: odata/Ordines(5)
        Function Delete(<FromODataUri> ByVal key As Integer) As IHttpActionResult
            Dim ordine As Ordine = db.Ordini.Find(key)
            If IsNothing(ordine) Then
                Return NotFound()
            End If

            db.Ordini.Remove(ordine)
            db.SaveChanges()

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function OrdineExists(ByVal key As Integer) As Boolean
            Return db.Ordini.Count(Function(e) e.Id = key) > 0
        End Function
    End Class
End Namespace
