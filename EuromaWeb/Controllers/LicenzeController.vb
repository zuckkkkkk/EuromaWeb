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
    Public Class LicenzeController
        Inherits System.Web.Mvc.Controller

        Private db As New EuromaModels
        Private appctx As New ApplicationDbContext

        ' GET: Licenze
        Function Index() As ActionResult
            Return View(db.Licenze.Where(Function(x) x.Active = True).ToList())
        End Function

        ' GET: Licenze/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim licenze As Licenze = db.Licenze.Find(id)
            If IsNothing(licenze) Then
                Return HttpNotFound()
            End If
            Return View(licenze)
        End Function

        ' GET: Licenze/Create
        Function Create() As ActionResult
            ViewBag.UserList = New SelectList(appctx.Users.ToList, "Id", "UserName")
            Return View()
        End Function
        Function Utenti() As ActionResult
            Dim list = db.Licenze.Where(Function(x) x.Active = True)
            Dim listLicUser As New List(Of LicenzaViewModel)
            For Each l In list
                Try
                    Dim username = appctx.Users.Where(Function(x) x.Id = l.Utente_Collegato).First.UserName
                    listLicUser.Add(New LicenzaViewModel With {
                        .Active = l.Active,
                        .Id = l.Id,
                        .Nome_Licenza = l.Nome_Licenza,
                        .StartDate = l.StartDate,
                        .Tipologia_Licenza = l.Tipologia_Licenza,
                        .Tipologia_Rinnovo = l.Tipologia_Rinnovo,
                        .Utente_Nome = username
                    })
                Catch ex As Exception

                End Try

            Next

            Return View(listLicUser)
        End Function
        ' POST: Licenze/Create
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,Nome_Licenza,Tipologia_Licenza,StartDate,StartDate_Month,StartDate_Day,StartDate_Year,Tipologia_Rinnovo,Active,Utente_Collegato")> ByVal licenze As Licenze) As ActionResult
            If ModelState.IsValid Then
                db.Licenze.Add(licenze)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(licenze)
        End Function
        Function CalculateCosts(id As String) As JsonResult
            Dim data = id.Split("_")
            Dim month = data(0)
            Dim year = data(1)
            Dim listOfLicenze As New Dictionary(Of String, Integer)
            For Each l In db.Licenze
                If l.StartDate_Month.ToString = month And l.StartDate_Year.ToString = year Then
                    listOfLicenze.Add(l.Nome_Licenza, 0)
                Else
                    Dim count_adder_month = 0
                    Select Case l.Tipologia_Rinnovo
                        Case Tipo_Rinnovo_Licenza.Mensile
                            count_adder_month = 1
                        Case Tipo_Rinnovo_Licenza.Trimestrale
                            count_adder_month = 3
                        Case Tipo_Rinnovo_Licenza.Semestrale
                            count_adder_month = 6
                        Case Tipo_Rinnovo_Licenza.Annuale
                            count_adder_month = 12
                        Case Else
                            count_adder_month = 0
                    End Select
                    Dim lic_month = l.StartDate_Month
                    Dim lic_year = l.StartDate_Year
                    Try
                        While lic_month.ToString <> month And lic_year.ToString <> year
                            lic_month = lic_month + count_adder_month

                            If lic_month > 12 Then
                                lic_month = lic_month - 12
                                year = year + 1
                            End If

                            If lic_month.ToString = month And lic_year.ToString = year Then
                                listOfLicenze.Add(l.Nome_Licenza, 0)
                            End If
                        End While
                    Catch ex As Exception

                    End Try

                End If
            Next
            Return Json(New With {.data = listOfLicenze})
        End Function
        ' GET: Licenze/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim licenze As Licenze = db.Licenze.Find(id)
            If IsNothing(licenze) Then
                Return HttpNotFound()
            End If
            Return View(licenze)
        End Function

        ' POST: Licenze/Edit/5
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,Nome_Licenza,Tipologia_Licenza,StartDate,StartDate_Month,StartDate_Day,StartDate_Year,Tipologia_Rinnovo,Active")> ByVal licenze As Licenze) As ActionResult
            If ModelState.IsValid Then
                db.Entry(licenze).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(licenze)
        End Function

        ' GET: Licenze/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim licenze As Licenze = db.Licenze.Find(id)
            If IsNothing(licenze) Then
                Return HttpNotFound()
            End If
            Return View(licenze)
        End Function

        ' POST: Licenze/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim licenze As Licenze = db.Licenze.Find(id)
            db.Licenze.Remove(licenze)
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
