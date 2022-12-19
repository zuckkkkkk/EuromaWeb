Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Mail
Imports System.Web
Imports System.Web.Mvc
Imports EuromaWeb
Imports Microsoft.AspNet.Identity

Namespace Controllers
    Public Class HelpDesksController
        Inherits System.Web.Mvc.Controller

        Private db As New EuromaModels

        ' GET: HelpDesks
        Function Index() As ActionResult
            Dim u = User.Identity.GetUserName()
            If User.IsInRole("Admin") And u = "mattia" Then
                Dim listTicket = db.HelpDesk.ToList()
                Dim listTicketFinal As New List(Of TicketViewModel)
                For Each l In listTicket
                    Try
                        Dim Stato_Ticket = ""
                        Select Case l.Stato_Ticket
                            Case 0
                                Stato_Ticket = "In Attesa" '"<div Class='progress-bar bg-danger progress-bar-striped progress-bar-animated' role='progressbar' style='width:  10%;border-radius: 8px;' aria-valuenow='@item.Stato_Ticket' aria-valuemin='0' aria-valuemax='100'>0%</div>"

                            Case 10
                                Stato_Ticket = "Presa in Carico"'"<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  15%;border-radius: 8px;' aria-valuenow='@item.Stato_Ticket' aria-valuemin='0' aria-valuemax='100'>10%</div>"

                            Case 50
                                Stato_Ticket = "In lavorazione"'"<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  50%;border-radius: 8px;' aria-valuenow='@item.Stato_Ticket' aria-valuemin='0' aria-valuemax='100'>50%</div>"

                            Case 90
                                Stato_Ticket = "Concluso"'"<div Class='progress-bar bg-info progress-bar-striped progress-bar-animated' role='progressbar' style='width:  90%;border-radius: 8px;' aria-valuenow='@item.Stato_Ticket' aria-valuemin='0' aria-valuemax='100'>90%</div>"

                            Case 100
                                Stato_Ticket = "Chiuso" '"<div Class='progress-bar bg-success progress-bar-striped progress-bar-animated' role='progressbar' style='width:  100%;border-radius: 8px;' aria-valuenow='@item.Stato_Ticket' aria-valuemin='0' aria-valuemax='100'>100%</div>"

                        End Select
                        listTicketFinal.Add(New TicketViewModel With {
                            .Body = l.Body,
                            .Id = l.Id,
                            .RequestDate = l.RequestDate,
                            .RequestUser = l.RequestUser,
                            .Request_Type = l.Request_Type,
                            .Solved = l.Solved,
                            .Stato_Ticket = Stato_Ticket,
                            .Title = l.Title
                        })
                    Catch ex As Exception

                    End Try
                Next


                Return View(listTicketFinal)
            Else
                Dim listTicket = db.HelpDesk.Where(Function(x) x.RequestUser = u).ToList()
                Dim listTicketFinal As New List(Of TicketViewModel)
                For Each l In listTicket
                    Try
                        Dim Stato_Ticket = ""
                        Select Case l.Stato_Ticket
                            Case 0
                                Stato_Ticket = "In Attesa" '"<div Class='progress-bar bg-danger progress-bar-striped progress-bar-animated' role='progressbar' style='width:  10%;border-radius: 8px;' aria-valuenow='@item.Stato_Ticket' aria-valuemin='0' aria-valuemax='100'>0%</div>"

                            Case 10
                                Stato_Ticket = "Presa in Carico"'"<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  15%;border-radius: 8px;' aria-valuenow='@item.Stato_Ticket' aria-valuemin='0' aria-valuemax='100'>10%</div>"

                            Case 50
                                Stato_Ticket = "In lavorazione"'"<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  50%;border-radius: 8px;' aria-valuenow='@item.Stato_Ticket' aria-valuemin='0' aria-valuemax='100'>50%</div>"

                            Case 90
                                Stato_Ticket = "Concluso"'"<div Class='progress-bar bg-info progress-bar-striped progress-bar-animated' role='progressbar' style='width:  90%;border-radius: 8px;' aria-valuenow='@item.Stato_Ticket' aria-valuemin='0' aria-valuemax='100'>90%</div>"

                            Case 100
                                Stato_Ticket = "Chiuso" '"<div Class='progress-bar bg-success progress-bar-striped progress-bar-animated' role='progressbar' style='width:  100%;border-radius: 8px;' aria-valuenow='@item.Stato_Ticket' aria-valuemin='0' aria-valuemax='100'>100%</div>"

                        End Select
                        listTicketFinal.Add(New TicketViewModel With {
                            .Body = l.Body,
                            .Id = l.Id,
                            .RequestDate = l.RequestDate,
                            .RequestUser = l.RequestUser,
                            .Request_Type = l.Request_Type,
                            .Solved = l.Solved,
                            .Stato_Ticket = Stato_Ticket,
                            .Title = l.Title
                        })
                    Catch ex As Exception

                    End Try
                Next


                Return View(listTicketFinal)
            End If
        End Function

        ' GET: HelpDesks/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim helpDesk As HelpDesk = db.HelpDesk.Find(id)
            If IsNothing(helpDesk) Then
                Return HttpNotFound()
            End If
            Return PartialView(helpDesk)
        End Function

        ' GET: HelpDesks/Create
        Function Create() As ActionResult
            Return PartialView()
        End Function

        ' POST: HelpDesks/Create
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Title,Body,Request_Type")> ByVal helpDesk As HelpDesk) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            If ModelState.IsValid Then
                Try
                    OpID = User.Identity.GetUserId()
                    OpName = User.Identity.GetUserName()
                    helpDesk.Body = helpDesk.Body.Replace("&lt;", "<").Replace("&gt;", ">")
                    db.HelpDesk.Add(New HelpDesk With {
                        .Body = helpDesk.Body,
                        .Solved = False,
                        .Title = helpDesk.Title,
                        .Request_Type = helpDesk.Request_Type,
                        .RequestDate = DateTime.Now,
                        .RequestUser = User.Identity.GetUserName(),
                        .Stato_Ticket = Stato_Ticket.In_attesa
                    })
                    db.SaveChanges()
                    Dim mySmtp As New SmtpClient
                    Dim myMail As New MailMessage()
                    mySmtp.UseDefaultCredentials = False
                    mySmtp.Credentials = New System.Net.NetworkCredential("no-reply@euromagroup.com", "yp@4d%p2AFa;")
                    mySmtp.Host = "squirtle.dnshigh.com"
                    myMail = New MailMessage()
                    myMail.From = New MailAddress("no-reply@euromagroup.com")
                    myMail.To.Add("m.zucchini@euromagroup.com")
                    myMail.Subject = "[" + helpDesk.Request_Type.ToString + "] - Nuovo Ticket - "
                    Dim StrContent = ""
                    Dim note = helpDesk.Body.ToString
                    Using reader = New StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Views/Shared/Email_Euroma.vbhtml")
                        Dim readFile As String = reader.ReadToEnd()
                        StrContent = readFile
                        StrContent = StrContent.Replace("[Username]", "Mattia")
                        StrContent = StrContent.Replace("[Motivo]", "E' stato inserito un nuovo ticket da " + User.Identity.GetUserName() + "all'interno del portale. <br>Descrizione: " + note + "")
                    End Using
                    myMail.Body = StrContent.ToString
                    myMail.IsBodyHtml = True
                    mySmtp.Send(myMail)
                    Return Json(New With {.ok = True, .message = "Ticket inserito correttamente."})
                Catch ex As Exception
                    db.Log.Add(New Log With {
                                                 .Livello = TipoLogLivello.Errors,
                                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                 .Messaggio = "Errore: " + ex.Message,
                                                 .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = "errore"}),
                                                .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                   })
                    db.SaveChanges()

                    Return Json(New With {.ok = False, .message = "Errore: Impossibile inserire ticket."})
                End Try
            End If
            Return Json(New With {.ok = False, .message = "Errore: Impossibile inserire ticket."})
        End Function

        ' GET: HelpDesks/Edit/5
        Function EditTicket(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim helpDesk As HelpDesk = db.HelpDesk.Find(id)
            If IsNothing(helpDesk) Then
                Return HttpNotFound()
            End If
            Return PartialView(helpDesk)
        End Function

        ' POST: HelpDesks/Edit/5
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function EditTicket(<Bind(Include:="Id,Title,Body,Body_Risposta,Request_Type,Stato_Ticket")> ByVal helpDesk As HelpDesk) As JsonResult
            If ModelState.IsValid Then
                Dim t = db.HelpDesk.Where(Function(x) x.Id = helpDesk.Id).FirstOrDefault
                If Not IsNothing(helpDesk.Title) Then
                    t.Title = helpDesk.Title
                End If
                If Not IsNothing(helpDesk.Body_Risposta) Then
                    helpDesk.Body_Risposta = helpDesk.Body_Risposta.Replace("&lt;", "<").Replace("&gt;", ">")
                    t.Body_Risposta = helpDesk.Body_Risposta
                End If
                If Not IsNothing(helpDesk.Request_Type) Then
                    t.Request_Type = helpDesk.Request_Type
                End If
                If Not IsNothing(helpDesk.Stato_Ticket) Then
                    t.Stato_Ticket = helpDesk.Stato_Ticket
                End If
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "Ticket modificato correttamente."})
            End If
            Return Json(New With {.ok = False, .message = "Impossibile modificare ticket."})
        End Function

        ' GET: HelpDesks/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim helpDesk As HelpDesk = db.HelpDesk.Find(id)
            If IsNothing(helpDesk) Then
                Return HttpNotFound()
            End If
            Return View(helpDesk)
        End Function

        ' POST: HelpDesks/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim helpDesk As HelpDesk = db.HelpDesk.Find(id)
            db.HelpDesk.Remove(helpDesk)
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

