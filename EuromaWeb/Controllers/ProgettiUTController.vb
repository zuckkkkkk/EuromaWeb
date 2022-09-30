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
Imports Microsoft.AspNetCore.Identity
Imports System.Web.Security
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.IO
Imports System.Data.SqlClient
Imports System.Net.Mail

Namespace Controllers
    Public Class ProgettiUTController
        Inherits System.Web.Mvc.Controller

        Private Const ConnectionString As String = "Persist Security Info=True;Password=ALNUSAD;User ID=ALNUSAD;Initial Catalog=ALNEUMA;Data Source=192.168.100.50"
        Private myConn As SqlConnection
        Private myCmd As SqlCommand
        Private myReader As SqlDataReader
        Private results As String

        Private db As New EuromaModels
        Private appctx As New ApplicationDbContext
        <HttpPost()>
        <ValidateInput(False)>
        Function ServerProcessingEsterni(PostedData As DataTableAjaxPostModel) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()

                Dim result As New List(Of Object)
                Dim data As IQueryable(Of OrdiniDiProduzione)
                If User.IsInRole("ProgrammazioneEsterno") Then
                    data = db.OrdiniDiProduzione.Where(Function(y) y.Accettato = Stato_Ordine_Di_Produzione_Esterno.In_attesa_est).OrderBy(Function(x) x.Priorita).OrderBy(Function(y) y.DataRichiestaConsegna)
                Else
                    data = db.OrdiniDiProduzione.Where(Function(y) y.Accettato = Stato_Ordine_Di_Produzione_Esterno.In_attesa_int Or y.Accettato = Stato_Ordine_Di_Produzione_Esterno.In_attesa_est).OrderBy(Function(x) x.Priorita).OrderBy(Function(y) y.DataRichiestaConsegna)
                End If

                'ricerca
                Try
                    If Not IsNothing(PostedData.search.value) Then
                        If Not PostedData.search.value.Contains(" ") Then 'singola parola
                            Dim search As String = PostedData.search.value
                            Dim w As Expressions.Expression(Of Func(Of OrdiniDiProduzione, Boolean)) = MakeWhereExpressionEsterno(search)
                            w.Compile()
                            data = data.Where(w)
                        Else 'multiple
                            For Each term As String In PostedData.search.value.Split(" ")
                                Dim wpartial As Expressions.Expression(Of Func(Of OrdiniDiProduzione, Boolean)) = MakeWhereExpressionEsterno(term)
                                wpartial.Compile()
                                data = data.Where(wpartial)
                            Next
                        End If
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                      .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                      .Livello = TipoLogLivello.Errors,
                      .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                      .Messaggio = "Errore Ricerca -> " & ex.Message,
                      .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                      })
                    db.SaveChanges()
                End Try
                'paginazione
                Dim filtered As Integer = 0
                Try
                    filtered = data.Count
                    If PostedData.length > 0 Then
                        data = data.Skip(PostedData.start).Take(PostedData.length)
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                                         .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                                         .Livello = TipoLogLivello.Errors,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Errore Paginazione -> " & ex.Message,
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                                         })
                    db.SaveChanges()
                End Try
                Try
                    If PostedData.order.Count = 0 Then
                        Dim o As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String))
                        o = MakeOrderExpressionEsterno(Nothing) 'default
                        o.Compile()
                        data = data.OrderBy(o)
                    ElseIf PostedData.order.Count = 1 Then 'singolo
                        Dim o As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(0).column)
                        o.Compile()
                        Select Case PostedData.order(0).dir
                            Case "asc"
                                data = data.OrderBy(o)
                            Case "desc"
                                data = data.OrderByDescending(o)
                        End Select
                    ElseIf PostedData.order.Count = 2 Then 'doppio
                        Dim o As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(1).column)
                        o2.Compile()

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        data = data.OrderBy(o).ThenBy(o2)
                                    Case "desc"
                                        data = data.OrderBy(o).ThenByDescending(o2)
                                End Select

                            Case "desc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        data = data.OrderByDescending(o).ThenBy(o2)
                                    Case "desc"
                                        data = data.OrderByDescending(o).ThenByDescending(o2)
                                End Select
                        End Select
                    Else 'solo i primi tre
                        Dim o As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(1).column)
                        o2.Compile()

                        Dim o3 As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(2).column)
                        o3.Compile()

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderBy(o).ThenBy(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderBy(o).ThenBy(o2).ThenByDescending(o3)
                                        End Select
                                    Case "desc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderBy(o).ThenByDescending(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderBy(o).ThenByDescending(o2).ThenByDescending(o3)
                                        End Select
                                End Select

                            Case "desc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderByDescending(o).ThenBy(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderByDescending(o).ThenBy(o2).ThenByDescending(o3)
                                        End Select
                                    Case "desc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderByDescending(o).ThenByDescending(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderByDescending(o).ThenByDescending(o2).ThenByDescending(o3)
                                        End Select
                                End Select
                        End Select
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Ordinamento -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                     })
                    db.SaveChanges()
                End Try
                'esecuzione (spero)
                For Each Acc As OrdiniDiProduzione In data
                    Try
                        Dim finalButtons = ""
                        Dim StatoProgetto = ""
                        Dim flagInvio = ""
                        Dim Priorita = ""
                        Select Case Acc.Accettato
                            Case 0
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  20%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>10%</div>"
                            Case 1
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  50%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>50%</div>"
                            Case 2
                                StatoProgetto = "<div Class='progress-bar bg-success progress-bar-striped progress-bar-animated' role='progressbar' style='width:  100%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>100%</div>"

                        End Select
                        Select Case Acc.Priorita
                            Case Stato_Priorita.No_Priorita
                                Priorita = ""
                            Case Stato_Priorita.Priorita_Max_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 1'></i>"
                            Case Stato_Priorita.Priorita_Max_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 2'></i>"
                            Case Stato_Priorita.Priorita_Max_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 3'></i>"
                            Case Stato_Priorita.Priorita_Media_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 1'></i>"
                            Case Stato_Priorita.Priorita_Media_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation' style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 2'></i>"
                            Case Stato_Priorita.Priorita_Media_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 3'></i>"
                            Case Stato_Priorita.Priorita_Minima_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 1'></i>"
                            Case Stato_Priorita.Priorita_Minima_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 2'></i>"
                            Case Stato_Priorita.Priorita_Minima_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 3'></i>"
                        End Select
                        Dim countNote = db.NotePerOC.Where(Function(x) x.OC = Acc.OP).ToList
                        Dim countFile = db.DocumentiPerOC.Where(Function(x) x.OC = Acc.OP).ToList
                        Dim notificheNote = ""
                        Dim notificheFile = ""
                        Dim daLeggereNote = False
                        Dim daLeggereDoc = False
                        If countNote.Count > 0 Then
                            For Each n In countNote
                                If db.VisualizzazioneFileNota.Where(Function(x) x.type = TipoVisualizzazione.Nota And x.User = OpID And x.id_filenota = n.Id).Count = 0 Then
                                    daLeggereNote = True
                                End If
                            Next
                            If daLeggereNote Then
                                notificheNote = "<i class='mx-1 fa-solid fa-comment fa-beat-fade' style='color:red!important;--fa-beat-fade-opacity: 0.67; --fa-beat-fade-scale: 1.05;'></i>"
                            Else
                                notificheNote = "<i class='mx-1 fa-solid fa-comment' style='color:black!important;'></i>"
                            End If
                        End If
                        If countFile.Count > 0 Then
                            For Each n In countFile
                                If db.VisualizzazioneFileNota.Where(Function(x) x.type = TipoVisualizzazione.File And x.User = OpID And x.id_filenota = n.Id).Count = 0 Then
                                    daLeggereDoc = True
                                End If
                            Next
                            If daLeggereDoc Then
                                notificheFile = "<i class='mx-1 fa-solid fa-file fa-beat-fade' style='color:red!important;--fa-beat-fade-opacity: 0.67; --fa-beat-fade-scale: 1.05;'></i>"
                            Else
                                notificheFile = "<i class='mx-1 fa-solid fa-file' style='color:black!important;'></i>"
                            End If
                        End If
                        result.Add(New With {
                                .DT_RowData = New With {.value = Acc.Id},
                                .DT_RowId = "row_" & Acc.Id,
                                .Id = Acc.Id,
                                .OC = Acc.OP.ToString + "(" + Acc.Articolo + ")" + notificheFile + notificheNote,
                                .Operatore = Acc.OperatoreInsert,
                                .StatoProgetto = StatoProgetto,
                                .Priorita = Priorita,
                                .Flag_Invio_Materiali = flagInvio,
                                .DataRichConsegna = Acc.DataRichiestaConsegna.ToString.Split(" ")(0)
                           })

                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                             .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                             .Livello = TipoLogLivello.Errors,
                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                             .Messaggio = "Errore Creazione Lista Progetti Esterni (" & Acc.Id & ") -> " & ex.Message & " [" & ex.InnerException.Message & "]",
                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                        })
                        db.SaveChanges()
                    End Try
                Next

                Return Json(New With {PostedData.draw, .recordsTotal = db.ProgettiUT.Count, .recordsFiltered = filtered, .data = result})
            Catch ex As SystemException
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Generico -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                     })
                db.SaveChanges()
            End Try
            Return Json(New With {PostedData.draw, .recordsTotal = db.OrdiniDiProduzione.Count, .recordsFiltered = 0})
        End Function
        <HttpPost()>
        <ValidateInput(False)>
        Function ServerProcessingEsterniCompletati(PostedData As DataTableAjaxPostModel) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()

                Dim result As New List(Of Object)
                Dim data As IQueryable(Of OrdiniDiProduzione)
                data = db.OrdiniDiProduzione.Where(Function(y) y.Accettato = Stato_Ordine_Di_Produzione_Esterno.Completato).OrderBy(Function(x) x.Priorita).OrderBy(Function(y) y.DataRichiestaConsegna)


                'ricerca
                Try
                    If Not IsNothing(PostedData.search.value) Then
                        If Not PostedData.search.value.Contains(" ") Then 'singola parola
                            Dim search As String = PostedData.search.value
                            Dim w As Expressions.Expression(Of Func(Of OrdiniDiProduzione, Boolean)) = MakeWhereExpressionEsterno(search)
                            w.Compile()
                            data = data.Where(w)
                        Else 'multiple
                            For Each term As String In PostedData.search.value.Split(" ")
                                Dim wpartial As Expressions.Expression(Of Func(Of OrdiniDiProduzione, Boolean)) = MakeWhereExpressionEsterno(term)
                                wpartial.Compile()
                                data = data.Where(wpartial)
                            Next
                        End If
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                      .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                      .Livello = TipoLogLivello.Errors,
                      .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                      .Messaggio = "Errore Ricerca -> " & ex.Message,
                      .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                      })
                    db.SaveChanges()
                End Try
                'paginazione
                Dim filtered As Integer = 0
                Try
                    filtered = data.Count
                    If PostedData.length > 0 Then
                        data = data.Skip(PostedData.start).Take(PostedData.length)
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                                         .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                                         .Livello = TipoLogLivello.Errors,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Errore Paginazione -> " & ex.Message,
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                                         })
                    db.SaveChanges()
                End Try
                Try
                    If PostedData.order.Count = 0 Then
                        Dim o As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String))
                        o = MakeOrderExpressionEsterno(Nothing) 'default
                        o.Compile()
                        data = data.OrderBy(o)
                        'data = data.OrderBy(CreateExpression(Of Amministratore)("Studio"))
                        'data = OrderByDynamic(data, "Studio", False)
                    ElseIf PostedData.order.Count = 1 Then 'singolo
                        Dim o As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(0).column)
                        o.Compile()
                        'If IsNothing(PostedData.columns(PostedData.order(0).column).data) Then
                        '    data = OrderByDynamic(data, "Studio", False)
                        'Else
                        '    data = OrderByDynamic(data, PostedData.columns(PostedData.order(0).column).data, PostedData.order(0).dir = "desc")
                        'End If

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                data = data.OrderBy(o)
                            Case "desc"
                                data = data.OrderByDescending(o)
                        End Select
                    ElseIf PostedData.order.Count = 2 Then 'doppio
                        Dim o As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(1).column)
                        o2.Compile()

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        data = data.OrderBy(o).ThenBy(o2)
                                    Case "desc"
                                        data = data.OrderBy(o).ThenByDescending(o2)
                                End Select

                            Case "desc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        data = data.OrderByDescending(o).ThenBy(o2)
                                    Case "desc"
                                        data = data.OrderByDescending(o).ThenByDescending(o2)
                                End Select
                        End Select
                    Else 'solo i primi tre
                        Dim o As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(1).column)
                        o2.Compile()

                        Dim o3 As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String)) = MakeOrderExpressionEsterno(PostedData.order(2).column)
                        o3.Compile()

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderBy(o).ThenBy(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderBy(o).ThenBy(o2).ThenByDescending(o3)
                                        End Select
                                    Case "desc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderBy(o).ThenByDescending(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderBy(o).ThenByDescending(o2).ThenByDescending(o3)
                                        End Select
                                End Select

                            Case "desc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderByDescending(o).ThenBy(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderByDescending(o).ThenBy(o2).ThenByDescending(o3)
                                        End Select
                                    Case "desc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderByDescending(o).ThenByDescending(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderByDescending(o).ThenByDescending(o2).ThenByDescending(o3)
                                        End Select
                                End Select
                        End Select
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Ordinamento -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                     })
                    db.SaveChanges()
                End Try
                'esecuzione (spero)
                For Each Acc As OrdiniDiProduzione In data
                    Try
                        Dim finalButtons = ""
                        Dim StatoProgetto = ""
                        Dim flagInvio = ""
                        Dim Priorita = ""
                        Select Case Acc.Accettato
                            Case 0
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  20%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>10%</div>"
                            Case 1
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  50%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>50%</div>"
                            Case 2
                                StatoProgetto = "<div Class='progress-bar bg-success progress-bar-striped progress-bar-animated' role='progressbar' style='width:  100%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>100%</div>"

                        End Select
                        Select Case Acc.Priorita
                            Case Stato_Priorita.No_Priorita
                                Priorita = ""
                            Case Stato_Priorita.Priorita_Max_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 1'></i>"
                            Case Stato_Priorita.Priorita_Max_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 2'></i>"
                            Case Stato_Priorita.Priorita_Max_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 3'></i>"
                            Case Stato_Priorita.Priorita_Media_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 1'></i>"
                            Case Stato_Priorita.Priorita_Media_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation' style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 2'></i>"
                            Case Stato_Priorita.Priorita_Media_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 3'></i>"
                            Case Stato_Priorita.Priorita_Minima_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 1'></i>"
                            Case Stato_Priorita.Priorita_Minima_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 2'></i>"
                            Case Stato_Priorita.Priorita_Minima_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 3'></i>"
                        End Select
                        Dim countNote = db.NotePerOC.Where(Function(x) x.OC = Acc.OP).ToList
                        Dim countFile = db.DocumentiPerOC.Where(Function(x) x.OC = Acc.OP).ToList
                        Dim daLeggereNote = False
                        Dim daLeggereDoc = False
                        Dim notificheNote = ""
                        Dim notificheFile = ""
                        If countNote.Count > 0 Then
                            For Each n In countNote
                                If db.VisualizzazioneFileNota.Where(Function(x) x.type = TipoVisualizzazione.Nota And x.User = OpID And x.id_filenota = n.Id).Count = 0 Then
                                    daLeggereNote = True
                                End If
                            Next
                            If daLeggereNote Then
                                notificheNote = "<i class='mx-1 fa-solid fa-comment fa-beat-fade' style='color:red!important;--fa-beat-fade-opacity: 0.67; --fa-beat-fade-scale: 1.05;'></i>"
                            Else
                                notificheNote = "<i class='mx-1 fa-solid fa-comment' style='color:black!important;'></i>"
                            End If
                        End If
                        If countFile.Count > 0 Then
                            For Each n In countFile
                                If db.VisualizzazioneFileNota.Where(Function(x) x.type = TipoVisualizzazione.File And x.User = OpID And x.id_filenota = n.Id).Count = 0 Then
                                    daLeggereDoc = True
                                End If
                            Next
                            If daLeggereDoc Then
                                notificheFile = "<i class='mx-1 fa-solid fa-file fa-beat-fade' style='color:red!important;--fa-beat-fade-opacity: 0.67; --fa-beat-fade-scale: 1.05;'></i>"
                            Else
                                notificheFile = "<i class='mx-1 fa-solid fa-file' style='color:black!important;'></i>"
                            End If
                        End If
                        result.Add(New With {
                                .DT_RowData = New With {.value = Acc.Id},
                                .DT_RowId = "row_" & Acc.Id,
                                .Id = Acc.Id,
                                .OC = Acc.OP.ToString + notificheFile + notificheNote,
                                .Operatore = Acc.OperatoreInsert,
                                .StatoProgetto = StatoProgetto,
                                .Priorita = Priorita,
                                .Flag_Invio_Materiali = flagInvio,
                                .DataRichConsegna = Acc.DataRichiestaConsegna.ToString.Split(" ")(0)
                           })

                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                             .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                             .Livello = TipoLogLivello.Errors,
                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                             .Messaggio = "Errore Creazione Lista Progetti Esterni (" & Acc.Id & ") -> " & ex.Message & " [" & ex.InnerException.Message & "]",
                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                        })
                        db.SaveChanges()
                    End Try
                Next

                Return Json(New With {PostedData.draw, .recordsTotal = db.ProgettiUT.Count, .recordsFiltered = filtered, .data = result})
            Catch ex As SystemException
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Generico -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                     })
                db.SaveChanges()
            End Try
            Return Json(New With {PostedData.draw, .recordsTotalEst = db.OrdiniDiProduzione.Count, .recordsFiltered = 0})
        End Function

        ' GET: ProgettiUT
        'DATATABLES
        <HttpPost()>
        <ValidateInput(False)>
        Function ServerProcessing(PostedData As DataTableAjaxPostModel) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()

                Dim result As New List(Of Object)
                Dim data As IQueryable(Of ProgettiUT)
                If User.IsInRole("Tecnico") Or User.IsInRole("TecnicoRevisione") Then
                    data = db.ProgettiUT.Where(Function(y) y.OperatoreId = OpID And y.StatoProgetto < Stato_UT.Inviato).OrderBy(Function(x) x.Priorita).OrderBy(Function(y) y.DataRichiestaConsegna)
                Else
                    data = db.ProgettiUT.Where(Function(x) x.StatoProgetto < Stato_UT.Inviato).OrderBy(Function(x) x.Priorita).OrderBy(Function(y) y.DataRichiestaConsegna)
                End If

                'ricerca
                Try
                    If Not IsNothing(PostedData.search.value) Then
                        If Not PostedData.search.value.Contains(" ") Then 'singola parola
                            Dim search As String = PostedData.search.value
                            Dim w As Expressions.Expression(Of Func(Of ProgettiUT, Boolean)) = MakeWhereExpression(search)
                            w.Compile()
                            data = data.Where(w)
                        Else 'multiple
                            For Each term As String In PostedData.search.value.Split(" ")
                                Dim wpartial As Expressions.Expression(Of Func(Of ProgettiUT, Boolean)) = MakeWhereExpression(term)
                                wpartial.Compile()
                                data = data.Where(wpartial)
                            Next
                        End If
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                      .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                      .Livello = TipoLogLivello.Errors,
                      .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                      .Messaggio = "Errore Ricerca -> " & ex.Message,
                      .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                      })
                    db.SaveChanges()
                End Try
                'paginazione
                Dim filtered As Integer = 0
                Try
                    filtered = data.Count
                    If PostedData.length > 0 Then
                        data = data.Skip(PostedData.start).Take(PostedData.length)
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                                         .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                                         .Livello = TipoLogLivello.Errors,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Errore Paginazione -> " & ex.Message,
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                                         })
                    db.SaveChanges()
                End Try
                Try
                    If PostedData.order.Count = 0 Then
                        Dim o As Expressions.Expression(Of Func(Of ProgettiUT, String))
                        o = MakeOrderExpression(Nothing) 'default
                        o.Compile()
                        data = data.OrderBy(o)
                        'data = data.OrderBy(CreateExpression(Of Amministratore)("Studio"))
                        'data = OrderByDynamic(data, "Studio", False)
                    ElseIf PostedData.order.Count = 1 Then 'singolo
                        Dim o As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(0).column)
                        o.Compile()
                        'If IsNothing(PostedData.columns(PostedData.order(0).column).data) Then
                        '    data = OrderByDynamic(data, "Studio", False)
                        'Else
                        '    data = OrderByDynamic(data, PostedData.columns(PostedData.order(0).column).data, PostedData.order(0).dir = "desc")
                        'End If

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                data = data.OrderBy(o)
                            Case "desc"
                                data = data.OrderByDescending(o)
                        End Select
                    ElseIf PostedData.order.Count = 2 Then 'doppio
                        Dim o As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(1).column)
                        o2.Compile()

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        data = data.OrderBy(o).ThenBy(o2)
                                    Case "desc"
                                        data = data.OrderBy(o).ThenByDescending(o2)
                                End Select

                            Case "desc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        data = data.OrderByDescending(o).ThenBy(o2)
                                    Case "desc"
                                        data = data.OrderByDescending(o).ThenByDescending(o2)
                                End Select
                        End Select
                    Else 'solo i primi tre
                        Dim o As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(1).column)
                        o2.Compile()

                        Dim o3 As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(2).column)
                        o3.Compile()

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderBy(o).ThenBy(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderBy(o).ThenBy(o2).ThenByDescending(o3)
                                        End Select
                                    Case "desc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderBy(o).ThenByDescending(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderBy(o).ThenByDescending(o2).ThenByDescending(o3)
                                        End Select
                                End Select

                            Case "desc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderByDescending(o).ThenBy(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderByDescending(o).ThenBy(o2).ThenByDescending(o3)
                                        End Select
                                    Case "desc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderByDescending(o).ThenByDescending(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderByDescending(o).ThenByDescending(o2).ThenByDescending(o3)
                                        End Select
                                End Select
                        End Select
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Ordinamento -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                     })
                    db.SaveChanges()
                End Try
                'esecuzione (spero)
                For Each Acc As ProgettiUT In data
                    Try
                        Dim finalButtons = ""
                        Dim StatoProgetto = ""
                        Dim flagInvio = ""
                        Dim Priorita = ""
                        Dim cliente = db.AccettazioneUC.Where(Function(x) x.OC = Acc.OC_Riferimento).First.Cliente
                        Select Case Acc.StatoProgetto
                            Case 0
                                StatoProgetto = "<div Class='progress-bar bg-danger progress-bar-striped progress-bar-animated' role='progressbar' style='width:  20%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>0%</div>"
                            Case 5
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  20%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>5%</div>"
                            Case 10
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  20%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>10%</div>"
                            Case 25
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  25%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>25%</div>"
                            Case 35
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  35%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>35%</div>"
                            Case 45
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  45%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>45%</div>"
                            Case 50
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  50%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>50%</div>"
                            Case 75
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  50%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>75%</div>"

                            Case 90
                                StatoProgetto = "<div Class='progress-bar bg-info progress-bar-striped progress-bar-animated' role='progressbar' style='width:  90%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>90%</div>"

                            Case 100
                                StatoProgetto = "<div Class='progress-bar bg-success progress-bar-striped progress-bar-animated' role='progressbar' style='width:  100%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>100%</div>"

                        End Select
                        If Acc.Flag_Invio_Materiali Then
                            flagInvio = "<input class='check-box' data-val='true' id='Flag_Invio_Materiali' name='Flag_Invio_Materiali' style='width: 30px; height: 30px;' type='checkbox' value='true' checked disabled>"
                        Else
                            flagInvio = "<input class='check-box' data-val='false'  id='Flag_Invio_Materiali' name='Flag_Invio_Materiali' style='width: 30px; height: 30px;' type='checkbox' value='false' disabled>"
                        End If
                        Select Case Acc.Priorita
                            Case Stato_Priorita.No_Priorita
                                Priorita = ""
                            Case Stato_Priorita.Priorita_Max_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 1'></i>"
                            Case Stato_Priorita.Priorita_Max_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 2'></i>"
                            Case Stato_Priorita.Priorita_Max_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 3'></i>"
                            Case Stato_Priorita.Priorita_Media_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 1'></i>"
                            Case Stato_Priorita.Priorita_Media_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation' style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 2'></i>"
                            Case Stato_Priorita.Priorita_Media_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 3'></i>"
                            Case Stato_Priorita.Priorita_Minima_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 1'></i>"
                            Case Stato_Priorita.Priorita_Minima_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 2'></i>"
                            Case Stato_Priorita.Priorita_Minima_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 3'></i>"
                        End Select
                        Dim countNote = db.NotePerOC.Where(Function(x) x.OC = Acc.OC_Riferimento).Count
                        Dim countFile = db.DocumentiPerOC.Where(Function(x) x.OC = Acc.OC_Riferimento).Count
                        Dim notificheNote = ""
                        Dim notificheFile = ""
                        If countNote > 0 Then
                            notificheNote = "<i class='mx-1 fa-solid fa-comment fa-beat-fade' style='color:red!important;--fa-beat-fade-opacity: 0.67; --fa-beat-fade-scale: 1.05;'></i>"
                        End If
                        If countFile > 0 Then
                            notificheFile = "<i class='mx-1 fa-solid fa-file fa-beat-fade' style='color:red!important;--fa-beat-fade-opacity: 0.67; --fa-beat-fade-scale: 1.05;'></i>"
                        End If
                        result.Add(New With {
                                .DT_RowData = New With {.value = Acc.Id},
                                .DT_RowId = "row_" & Acc.Id,
                                .Id = Acc.Id,
                                .OC = "<a style='text-decoration:none!important; margin-right:8px;'href='/Overviews/Ordine/" + Acc.OC_Riferimento.ToString + "' Target='_blank'>" + Acc.OC_Riferimento + "(" + cliente + ")" + "</a>" + notificheFile + notificheNote,
                                .Operatore = Acc.Operatore,
                                .StatoProgetto = StatoProgetto,
                                .Priorita = Priorita,
                                .Flag_Invio_Materiali = flagInvio,
                                .DataRichConsegna = Acc.DataRichiestaConsegna.ToString.Split(" ")(0)
                           })

                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                             .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                             .Livello = TipoLogLivello.Errors,
                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                             .Messaggio = "Errore Creazione Lista Progetti (" & Acc.Id & ") -> " & ex.Message & " [" & ex.InnerException.Message & "]",
                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                        })
                        db.SaveChanges()
                    End Try
                Next

                Return Json(New With {PostedData.draw, .recordsTotal = db.ProgettiUT.Count, .recordsFiltered = filtered, .data = result})
            Catch ex As SystemException
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Generico -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                     })
                db.SaveChanges()
            End Try
            Return Json(New With {PostedData.draw, .recordsTotalUT = db.ProgettiUT.Count, .recordsFiltered = 0})
        End Function

        <HttpPost()>
        <ValidateInput(False)>
        Function ServerProcessingCompletati(PostedData As DataTableAjaxPostModel) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()

                Dim result As New List(Of Object)
                Dim data As IQueryable(Of ProgettiUT)
                If User.IsInRole("Tecnico") Or User.IsInRole("TecnicoRevisione") Then
                    data = db.ProgettiUT.Where(Function(y) y.OperatoreId = OpID And y.StatoProgetto = Stato_UT.Inviato).OrderBy(Function(x) x.Priorita).OrderBy(Function(y) y.DataRichiestaConsegna)
                Else
                    data = db.ProgettiUT.Where(Function(x) x.StatoProgetto = Stato_UT.Inviato).OrderBy(Function(x) x.Priorita).OrderBy(Function(y) y.DataRichiestaConsegna)
                End If

                'ricerca
                Try
                    If Not IsNothing(PostedData.search.value) Then
                        If Not PostedData.search.value.Contains(" ") Then 'singola parola
                            Dim search As String = PostedData.search.value
                            Dim w As Expressions.Expression(Of Func(Of ProgettiUT, Boolean)) = MakeWhereExpression(search)
                            w.Compile()
                            data = data.Where(w)
                        Else 'multiple
                            For Each term As String In PostedData.search.value.Split(" ")
                                Dim wpartial As Expressions.Expression(Of Func(Of ProgettiUT, Boolean)) = MakeWhereExpression(term)
                                wpartial.Compile()
                                data = data.Where(wpartial)
                            Next
                        End If
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                      .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                      .Livello = TipoLogLivello.Errors,
                      .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                      .Messaggio = "Errore Ricerca -> " & ex.Message,
                      .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                      })
                    db.SaveChanges()
                End Try
                'paginazione
                Dim filtered As Integer = 0
                Try
                    filtered = data.Count
                    If PostedData.length > 0 Then
                        data = data.Skip(PostedData.start).Take(PostedData.length)
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                                         .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                                         .Livello = TipoLogLivello.Errors,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Errore Paginazione -> " & ex.Message,
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                                         })
                    db.SaveChanges()
                End Try
                Try
                    If PostedData.order.Count = 0 Then
                        Dim o As Expressions.Expression(Of Func(Of ProgettiUT, String))
                        o = MakeOrderExpression(Nothing) 'default
                        o.Compile()
                        data = data.OrderBy(o)
                        'data = data.OrderBy(CreateExpression(Of Amministratore)("Studio"))
                        'data = OrderByDynamic(data, "Studio", False)
                    ElseIf PostedData.order.Count = 1 Then 'singolo
                        Dim o As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(0).column)
                        o.Compile()
                        'If IsNothing(PostedData.columns(PostedData.order(0).column).data) Then
                        '    data = OrderByDynamic(data, "Studio", False)
                        'Else
                        '    data = OrderByDynamic(data, PostedData.columns(PostedData.order(0).column).data, PostedData.order(0).dir = "desc")
                        'End If

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                data = data.OrderBy(o)
                            Case "desc"
                                data = data.OrderByDescending(o)
                        End Select
                    ElseIf PostedData.order.Count = 2 Then 'doppio
                        Dim o As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(1).column)
                        o2.Compile()

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        data = data.OrderBy(o).ThenBy(o2)
                                    Case "desc"
                                        data = data.OrderBy(o).ThenByDescending(o2)
                                End Select

                            Case "desc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        data = data.OrderByDescending(o).ThenBy(o2)
                                    Case "desc"
                                        data = data.OrderByDescending(o).ThenByDescending(o2)
                                End Select
                        End Select
                    Else 'solo i primi tre
                        Dim o As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(1).column)
                        o2.Compile()

                        Dim o3 As Expressions.Expression(Of Func(Of ProgettiUT, String)) = MakeOrderExpression(PostedData.order(2).column)
                        o3.Compile()

                        Select Case PostedData.order(0).dir
                            Case "asc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderBy(o).ThenBy(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderBy(o).ThenBy(o2).ThenByDescending(o3)
                                        End Select
                                    Case "desc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderBy(o).ThenByDescending(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderBy(o).ThenByDescending(o2).ThenByDescending(o3)
                                        End Select
                                End Select

                            Case "desc"
                                Select Case PostedData.order(1).dir
                                    Case "asc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderByDescending(o).ThenBy(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderByDescending(o).ThenBy(o2).ThenByDescending(o3)
                                        End Select
                                    Case "desc"
                                        Select Case PostedData.order(2).dir
                                            Case "asc"
                                                data = data.OrderByDescending(o).ThenByDescending(o2).ThenBy(o3)
                                            Case "desc"
                                                data = data.OrderByDescending(o).ThenByDescending(o2).ThenByDescending(o3)
                                        End Select
                                End Select
                        End Select
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Ordinamento -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                     })
                    db.SaveChanges()
                End Try
                'esecuzione (spero)
                For Each Acc As ProgettiUT In data
                    Try
                        Dim finalButtons = ""
                        Dim StatoProgetto = ""
                        Dim flagInvio = ""
                        Dim Priorita = ""
                        Dim cliente = db.AccettazioneUC.Where(Function(x) x.OC = Acc.OC_Riferimento).First.Cliente
                        Select Case Acc.StatoProgetto
                            Case 0
                                StatoProgetto = "<div Class='progress-bar bg-danger progress-bar-striped progress-bar-animated' role='progressbar' style='width:  20%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>0%</div>"
                            Case 5
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  20%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>5%</div>"
                            Case 10
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  20%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>10%</div>"
                            Case 25
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  25%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>25%</div>"
                            Case 35
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  35%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>35%</div>"
                            Case 45
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  45%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>45%</div>"
                            Case 50
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  50%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>50%</div>"
                            Case 75
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  50%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>75%</div>"

                            Case 90
                                StatoProgetto = "<div Class='progress-bar bg-info progress-bar-striped progress-bar-animated' role='progressbar' style='width:  90%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>90%</div>"

                            Case 100
                                StatoProgetto = "<div Class='progress-bar bg-success progress-bar-striped progress-bar-animated' role='progressbar' style='width:  100%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>100%</div>"

                        End Select
                        If Acc.Flag_Invio_Materiali Then
                            flagInvio = "<input class='check-box' data-val='true' id='Flag_Invio_Materiali' name='Flag_Invio_Materiali' style='width: 30px; height: 30px;' type='checkbox' value='true' checked disabled>"
                        Else
                            flagInvio = "<input class='check-box' data-val='false'  id='Flag_Invio_Materiali' name='Flag_Invio_Materiali' style='width: 30px; height: 30px;' type='checkbox' value='false' disabled>"
                        End If
                        Select Case Acc.Priorita
                            Case Stato_Priorita.No_Priorita
                                Priorita = ""
                            Case Stato_Priorita.Priorita_Max_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 1'></i>"
                            Case Stato_Priorita.Priorita_Max_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 2'></i>"
                            Case Stato_Priorita.Priorita_Max_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ff4b2b!important;' data-toggle='tooltip' data-placement='left' title='Priorita Massima 3'></i>"
                            Case Stato_Priorita.Priorita_Media_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 1'></i>"
                            Case Stato_Priorita.Priorita_Media_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation' style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 2'></i>"
                            Case Stato_Priorita.Priorita_Media_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #ffc32b!important;' data-toggle='tooltip' data-placement='left' title='Priorita media 3'></i>"
                            Case Stato_Priorita.Priorita_Minima_1
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 1'></i>"
                            Case Stato_Priorita.Priorita_Minima_2
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 2'></i>"
                            Case Stato_Priorita.Priorita_Minima_3
                                Priorita = "<i class='fa-solid fa-triangle-exclamation'  style = 'color: #50c416!important;' data-toggle='tooltip' data-placement='left' title='Priorita Minima 3'></i>"
                        End Select
                        result.Add(New With {
                                .DT_RowData = New With {.value = Acc.Id},
                                .DT_RowId = "row_" & Acc.Id,
                                .Id = Acc.Id,
                                .OC = "<a style='text-decoration:none!important;'href='/Overviews/Ordine/" + Acc.OC_Riferimento.ToString + "' Target='_blank'>" + Acc.OC_Riferimento + "(" + cliente + ")" + "</a>",
                                .Operatore = Acc.Operatore,
                                .StatoProgetto = StatoProgetto,
                                .Priorita = Priorita,
                                .Flag_Invio_Materiali = flagInvio,
                                .DataRichConsegna = Acc.DataRichiestaConsegna.ToString.Split(" ")(0)
                           })

                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                             .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                             .Livello = TipoLogLivello.Errors,
                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                             .Messaggio = "Errore Creazione Lista Progetti (" & Acc.Id & ") -> " & ex.Message & " [" & ex.InnerException.Message & "]",
                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                        })
                        db.SaveChanges()
                    End Try
                Next

                Return Json(New With {PostedData.draw, .recordsTotalUT = db.ProgettiUT.Count, .recordsFiltered = filtered, .data = result})
            Catch ex As SystemException
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Generico -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                     })
                db.SaveChanges()
            End Try
            Return Json(New With {PostedData.draw, .recordsTotalUT = db.ProgettiUT.Count, .recordsFiltered = 0})
        End Function
        ' GET: ProgettiUT
        <Authorize>
        Function Index() As ActionResult
            Dim PList As New List(Of Progetti_UTViewModel)
            For Each p In db.ProgettiUT
                Dim id_OC = db.AccettazioneUC.Where(Function(x) x.OC = p.OC_Riferimento).First.Id
                Dim pNew = New Progetti_UTViewModel With {
                    .OC_Riferimento = p.OC_Riferimento,
                    .Operatore = p.Operatore,
                    .Id = p.Id,
                    .Flag_Invio_Materiali = p.Flag_Invio_Materiali,
                    .StatoProgetto = p.StatoProgetto,
                    .Id_OC = id_OC
                }
                Select Case (p.StatoProgetto)
                    Case Stato_UT.In_attesa_Admin
                        pNew.StatoProgettoVal = 0
                    Case Stato_UT.In_Attesa_Operatore
                        pNew.StatoProgettoVal = 10
                    Case Stato_UT.Maggiori_Info_Necessarie
                        pNew.StatoProgettoVal = 25
                    Case Stato_UT.Sviluppo_Approvazione_Cliente
                        pNew.StatoProgettoVal = 35
                    Case Stato_UT.Approvazione_Cliente
                        pNew.StatoProgettoVal = 50
                    Case Stato_UT.Disegno_Particolari
                        pNew.StatoProgettoVal = 75
                    Case Stato_UT.Completato
                        pNew.StatoProgettoVal = 90

                End Select
                PList.Add(pNew)

            Next
            Return View(PList)
        End Function
        <Authorize>
        Function DeleteOrdineDiProduzioneEsterno(ByVal id As Integer) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim odp = db.OrdiniDiProduzione.Find(id)
                db.OrdiniDiProduzione.Remove(odp)
                db.SaveChanges()
                db.Audit.Add(New Audit With {
                                            .Livello = TipoAuditLivello.Info,
                                            .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                            .Messaggio = "ODP Esterno cancellato correttamente",
                                            .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id, .op = odp.OP}),
                                           .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                              })
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "OP correttamente eliminato!"}, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore cancellazione OP -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Impossibile cancellare l'OP!"}, JsonRequestBehavior.AllowGet)
            End Try
        End Function
        ' GET: ProgettiUT/Details/5
        Function Details(ByVal id As Integer?) As JsonResult
            If IsNothing(id) Then
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Progetto -> " & id & ". Impossibile recuperare i dati."})
            End If
            Dim progettiUT As ProgettiUT = db.ProgettiUT.Find(id)
            Dim listArticoli = db.ArticoliPerOC.Where(Function(x) x.OC = progettiUT.OC_Riferimento).ToList
            Dim listNote = db.NotePerOC.Where(Function(x) x.OC = progettiUT.OC_Riferimento).ToList
            Dim accettazioneID = db.AccettazioneUC.Where(Function(x) x.OC = progettiUT.OC_Riferimento).First.Id
            Dim conteggioInProd = db.ProgettiProd.Where(Function(x) x.OC_Riferimento = progettiUT.OC_Riferimento).Count
            Dim listDocumenti = db.DocumentiPerOC.Where(Function(x) x.OC = progettiUT.OC_Riferimento).ToList
            Dim putVM As New ProgettiUTViewModel With {
                .StatoProgetto = progettiUT.StatoProgetto,
                .DataCompletamento = progettiUT.DataCompletamento,
                .DataCreazione = progettiUT.DataCreazione,
                .File = progettiUT.File,
                .Flag_Invio_Materiali = progettiUT.Flag_Invio_Materiali,
                .Id = progettiUT.Id,
                .ListaArt = listArticoli,
                .ListOfNote = listNote,
                .ListOfDocumenti = listDocumenti,
                .Note = progettiUT.Note,
                .OC_Riferimento = progettiUT.OC_Riferimento,
                .Id_OC = accettazioneID,
                .Operatore = progettiUT.Operatore,
                .OperatoreId = progettiUT.Operatore,
                .OperatoreSmistamento = progettiUT.OperatoreSmistamento,
                .OperatoreSmistamentoId = progettiUT.OperatoreSmistamentoId,
                .SenttoUC = IIf(conteggioInProd > 0, 1, 0)
            }
            If IsNothing(progettiUT) Then
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Progetto -> " & id & ". Impossibile recuperare i dati."})
            End If
            Dim Id_OC = db.AccettazioneUC.Where(Function(x) x.OC = progettiUT.OC_Riferimento).First.Id
            ViewBag.Id_OC = Id_OC
            Return Json(New With {.ok = True, .message = PartialToString("Details", putVM)})
        End Function
        Function DetailsEst(ByVal id As Integer?) As JsonResult
            If IsNothing(id) Then
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Progetto -> " & id & ". Impossibile recuperare i dati."})
            End If
            Dim OrdiniDiProduzione As OrdiniDiProduzione = db.OrdiniDiProduzione.Find(id)
            Dim listNote = db.NotePerOC.Where(Function(x) x.OC = OrdiniDiProduzione.OP).ToList
            Dim listDocumenti = db.DocumentiPerOC.Where(Function(x) x.OC = OrdiniDiProduzione.OP).ToList
            Dim OpName = User.Identity.GetUserId()
            For Each nota In listNote
                If db.VisualizzazioneFileNota.Where(Function(x) x.type = TipoVisualizzazione.Nota And x.User = OpName And x.id_filenota = nota.Id).Count = 0 Then
                    db.VisualizzazioneFileNota.Add(New VisualizzazioneFileNota With {
                     .id_filenota = nota.Id,
                     .ReaedingDate = DateTime.Now,
                     .User = OpName,
                     .type = TipoVisualizzazione.Nota
                         })
                    db.SaveChanges()
                End If

            Next
            For Each f In listDocumenti
                If db.VisualizzazioneFileNota.Where(Function(x) x.type = TipoVisualizzazione.File And x.User = OpName And x.id_filenota = f.Id).Count = 0 Then
                    db.VisualizzazioneFileNota.Add(New VisualizzazioneFileNota With {
                     .id_filenota = f.Id,
                     .ReaedingDate = DateTime.Now,
                     .User = OpName,
                     .type = TipoVisualizzazione.File
                         })
                    db.SaveChanges()
                End If
            Next
            Dim putVM As New ODPEstViewModel With {
                .Accettato = OrdiniDiProduzione.Accettato,
                .DataCreazione = OrdiniDiProduzione.DataCreazione,
                .Id = OrdiniDiProduzione.Id,
                .ListOfNote = listNote,
                .ListOfDocumenti = listDocumenti,
                .OP = OrdiniDiProduzione.OP,
                .OperatoreInsert = OrdiniDiProduzione.OperatoreInsert
            }
            If IsNothing(OrdiniDiProduzione) Then
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Progetto -> " & id & ". Impossibile recuperare i dati."})
            End If
            Return Json(New With {.ok = True, .message = PartialToString("DetailsEst", putVM)})
        End Function
        ' GET: ProgettiUT/Create
        <Authorize>
        Function Create() As ActionResult
            Return View()
        End Function
        Function CreateODPEst() As ActionResult
            Return PartialView()
        End Function
        ' POST: ProgettiUT/Create
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,OC_Riferimento,OperatoreSmistamento,Operatore,DataCreazione,DataCompletamento,Note,File,StatoProgetto,Flag_Invio_Materiali,Flag_1,Flag_2,Flag_3")> ByVal progettiUT As ProgettiUT) As ActionResult
            If ModelState.IsValid Then
                db.ProgettiUT.Add(progettiUT)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(progettiUT)
        End Function

        ' GET: ProgettiUT/Edit/5
        <Authorize>
        Function EditOperatore(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim progettiUT As ProgettiUT = db.ProgettiUT.Find(id)
            ViewBag.ListaArt = db.ArticoliPerOC.Where(Function(x) x.OC = progettiUT.OC_Riferimento).ToList
            Dim roleId = appctx.Roles.Where(Function(m) m.Name = "Tecnico").[Select](Function(m) m.Id).SingleOrDefault()
            Dim roleIdAdmin = appctx.Roles.Where(Function(m) m.Name = "TecnicoAdmin").[Select](Function(m) m.Id).SingleOrDefault()
            Dim list = appctx.Users.Where(Function(u) u.Roles.Any(Function(r) r.RoleId = roleId Or r.RoleId = roleIdAdmin)).ToList()
            list.Insert(0, New ApplicationUser With {.Id = "0", .UserName = "Selezionare Voce..."})
            ViewBag.Operatore = New SelectList(list, "Id", "Username")
            Dim ProgettiUTOperatore As New ProgettiUT_Operatore With {
                .DataCompletamento = progettiUT.DataCompletamento,
                .DataCreazione = progettiUT.DataCreazione,
                .File = progettiUT.File,
                .Flag_1 = progettiUT.Flag_1,
                .Flag_2 = progettiUT.Flag_2,
                .Flag_3 = progettiUT.Flag_3,
                .Flag_Invio_Materiali = progettiUT.Flag_Invio_Materiali,
                .Id = progettiUT.Id,
                .Note = progettiUT.Note,
                .OC_Riferimento = progettiUT.OC_Riferimento,
                .Operatore = progettiUT.Operatore,
                .OperatoreId = progettiUT.OperatoreId,
                .OperatoreSmistamento = progettiUT.OperatoreSmistamento,
                .OperatoreSmistamentoId = progettiUT.OperatoreSmistamentoId,
                .StatoProgetto = progettiUT.StatoProgetto
            }
            If IsNothing(ProgettiUTOperatore) Then
                Return HttpNotFound()
            End If
            Return PartialView(ProgettiUTOperatore)
        End Function

        ' POST: ProgettiUT/Edit/5
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function EditOperatore(<Bind(Include:="Id,OC_Riferimento,Note,Operatore,StatoProgetto,Flag_1,Flag_2,Flag_3,Flag_4,DataRetroattiva")> ByVal ProgettiUTOperatore As ProgettiUT_Operatore, file As HttpPostedFileBase) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim fileSalvati As New List(Of String)
            Dim pathTMP = ""
            For i = 0 To Request.Files.Count - 1
                Try
                    Dim UploadedFile As HttpPostedFileBase = Request.Files(i)
                    If UploadedFile IsNot Nothing AndAlso UploadedFile.ContentLength > 0 Then
                        pathTMP = Path.Combine(Server.MapPath("~/Content/upload_UC_Progetti"), UploadedFile.FileName)
                        If System.IO.File.Exists(pathTMP) Then
                            db.DocumentiPerOC.Add(New DocumentiPerOC With {
                                .DataCreazioneFile = DateTime.Now,
                                .Nome_File = UploadedFile.FileName,
                                .OC = ProgettiUTOperatore.OC_Riferimento,
                                .Operatore_Id = OpID,
                                .Operatore_Nome = OpName,
                                .Percorso_File = pathTMP
                            })
                            db.SaveChanges()
                        Else
                            UploadedFile.SaveAs(pathTMP)
                            db.DocumentiPerOC.Add(New DocumentiPerOC With {
                                .DataCreazioneFile = DateTime.Now,
                                .Nome_File = UploadedFile.FileName,
                                .OC = ProgettiUTOperatore.OC_Riferimento,
                                .Operatore_Id = OpID,
                                .Operatore_Nome = OpName,
                                .Percorso_File = pathTMP
                            })
                        End If

                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                                                     .Livello = TipoLogLivello.Errors,
                                                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                     .Messaggio = "Errore: " + ex.Message,
                                                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = "errore"}),
                                                    .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                       })
                    db.SaveChanges()
                End Try


            Next
            If ModelState.IsValid Then
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim prog = db.ProgettiUT.Where(Function(x) x.Id = ProgettiUTOperatore.Id).First
                Dim Cliente = db.AccettazioneUC.Where(Function(x) x.OC = ProgettiUTOperatore.OC_Riferimento).First
                Dim listArticoli = db.ArticoliPerOC.Where(Function(x) x.OC = ProgettiUTOperatore.OC_Riferimento).ToList
                'Inizio ricerca nuove distinte basi per aggiornamento stato
                For Each art In listArticoli
                    myConn = New SqlConnection(ConnectionString)
                    myCmd = myConn.CreateCommand
                    myCmd.CommandText = "select count(*) from DIBDCO00 where ARTCOD = '" + art.Cod_Art + "'"
                    myConn.Open()
                    Try
                        myReader = myCmd.ExecuteReader
                        Dim countDB = 0
                        Do While myReader.Read()
                            countDB = myReader.GetInt32(0)
                        Loop
                        myConn.Close()
                        If countDB > 0 Then
                            Try
                                db.ArticoliPerOC.Where(Function(x) x.Cod_Art = art.Cod_Art And x.OC = prog.OC_Riferimento).First.DistintaBase = True
                                db.SaveChanges()
                                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Aggiornamento stato Distinta Base per articolo correttamente aggiornata.",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = prog.Id, .OC = prog.OC_Riferimento}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                                db.SaveChanges()
                            Catch ex As Exception
                                db.Log.Add(New Log With {
                                                  .Livello = TipoLogLivello.Errors,
                                                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                  .Messaggio = "Errore: " + ex.Message,
                                                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                    })
                                db.SaveChanges()
                            End Try

                        End If
                        db.SaveChanges()
                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                                                  .Livello = TipoLogLivello.Errors,
                                                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                  .Messaggio = "Errore: " + ex.Message,
                                                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                    })
                        db.SaveChanges()
                    End Try
                Next
                'End aggiornamento distinta base
                If Not ProgettiUTOperatore.Operatore = "0" Then
                    Dim userNameOperatoreScelto = appctx.Users.Where(Function(x) x.Id = ProgettiUTOperatore.Operatore).First.UserName
                    If userNameOperatoreScelto <> prog.Operatore Then
                        prog.Operatore = userNameOperatoreScelto
                        prog.OperatoreId = ProgettiUTOperatore.Operatore
                        db.SaveChanges()
                        db.StoricoOC.Add(New StoricoOC With {
                              .OC = prog.OC_Riferimento,
                              .Descrizione = "Aggiornato operatore progetto",
                              .Titolo = "Modificato Operatore in Progetto da " + ProgettiUTOperatore.Operatore + " a " + prog.Operatore,
                              .Ufficio = TipoUfficio.UfficioTecnico,
                              .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                          })
                        db.SaveChanges()
                    End If
                End If
                If ProgettiUTOperatore.StatoProgetto = Stato_UT.Completato Then
                    prog.File = pathTMP
                    If ProgettiUTOperatore.Flag_1 And ProgettiUTOperatore.Flag_2 > StatoCheck.Selezione And ProgettiUTOperatore.Flag_3 And ProgettiUTOperatore.Flag_4 > StatoCheck.Selezione Then
                        prog.Note = ProgettiUTOperatore.Note
                        prog.StatoProgetto = ProgettiUTOperatore.StatoProgetto
                        Dim datacomp = DateTime.Now
                        If ProgettiUTOperatore.DataRetroattiva <> "1/1/0001 12:00:00 AM" Then
                            datacomp = ProgettiUTOperatore.DataRetroattiva
                        End If
                        prog.DataCompletamento = datacomp
                        prog.Flag_1 = ProgettiUTOperatore.Flag_1
                        prog.Flag_2 = ProgettiUTOperatore.Flag_2
                        prog.Flag_3 = ProgettiUTOperatore.Flag_3
                        prog.Flag_4 = ProgettiUTOperatore.Flag_4
                        db.SaveChanges()
                        db.StoricoOC.Add(New StoricoOC With {
                            .OC = prog.OC_Riferimento,
                            .Descrizione = "Completato correttamente progetto in Ufficio Tecnico",
                            .Titolo = "Progetto completato Ufficio tecnico",
                            .Ufficio = TipoUfficio.UfficioTecnico,
                            .UltimaModifica = New TipoUltimaModifica With {.Data = datacomp, .Operatore = OpName, .OperatoreID = OpID}
                        })
                        db.SaveChanges()
                        Dim mySmtpT As New SmtpClient
                        Dim myMailT As New MailMessage()
                        mySmtpT.UseDefaultCredentials = False
                        mySmtpT.Credentials = New System.Net.NetworkCredential("no-reply@euromagroup.com", "yp@4d%p2AFa;")
                        mySmtpT.Host = "oberon.dnshigh.com"
                        myMailT = New MailMessage()
                        myMailT.From = New MailAddress("no-reply@euromagroup.com")
                        'myMailT.Attachments.Add(New System.Net.Mail.Attachment(Cliente.File))
                        Try
                            If Not IsNothing(prog.File) Then
                                myMailT.Attachments.Add(New System.Net.Mail.Attachment(prog.File))
                            End If
                        Catch ex As Exception

                        End Try

                        myMailT.To.Add("t.marchioni@euromagroup.com")
                        'myMailT.To.Add("m.zucchini@euromagroup.com")
                        myMailT.Subject = "Completato - " + ProgettiUTOperatore.OC_Riferimento.ToString + " - " + Cliente.Cliente.ToString
                        Dim StrContentT = ""
                        Using reader = New StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Views/Shared/Email_Euroma_Amm.vbhtml")
                            Dim readFile As String = reader.ReadToEnd()
                            StrContentT = readFile
                            StrContentT = StrContentT.Replace("[Username]", "")
                            StrContentT = StrContentT.Replace("[Motivo]", "E' stato completato un progetto.")
                        End Using
                        myMailT.Body = StrContentT.ToString
                        myMailT.IsBodyHtml = True
                        mySmtpT.Send(myMailT)
                        db.Audit.Add(New Audit With {
                                               .Livello = TipoAuditLivello.Info,
                                               .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                               .Messaggio = "Invio mail Tiziano per conoscenza (completamento prog)",
                                               .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.OC = ProgettiUTOperatore.OC_Riferimento}),
                                              .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                 })
                        db.SaveChanges()
                        Return Json(New With {.ok = True, .message = "Progetto correttamente completato."})
                    Else
                        Return Json(New With {.ok = False, .message = "Non tutti i campi sono stati correttamente salvati."})
                    End If
                Else
                    prog.Note = ProgettiUTOperatore.Note
                    prog.StatoProgetto = ProgettiUTOperatore.StatoProgetto
                    Dim datacomp = DateTime.Now
                    If ProgettiUTOperatore.DataRetroattiva <> "1/1/0001 12:00:00 AM" Then
                        datacomp = ProgettiUTOperatore.DataRetroattiva
                    End If
                    prog.DataCompletamento = datacomp
                    db.SaveChanges()
                    db.StoricoOC.Add(New StoricoOC With {
                           .OC = prog.OC_Riferimento,
                           .Descrizione = "Aggiornato stato progetto in Ufficio Tecnico, stato attuale: " + prog.StatoProgetto.ToString,
                           .Titolo = "Aggiornamento stato Ufficio tecnico",
                           .Ufficio = TipoUfficio.UfficioTecnico,
                           .UltimaModifica = New TipoUltimaModifica With {.Data = datacomp, .Operatore = OpName, .OperatoreID = OpID}
                       })
                    db.SaveChanges()
                    End If
                    Return Json(New With {.ok = True, .message = "Progetto correttamente aggiornato."})
            End If
            Return Json(New With {.ok = False, .message = "Errore nell'aggiornamento del progetto."})
        End Function
        Function DirectSendToProd(ByVal id As Integer) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC_Proj = db.ProgettiUT.Where(Function(x) x.Id = id).First
                Dim OC = db.AccettazioneUC.Where(Function(x) x.OC = OC_Proj.OC_Riferimento).First
                Dim exist = db.ProgettiProd.Where(Function(x) x.OC_Riferimento = OC.OC).Count
                If exist > 0 Then
                    db.ProgettiProd.Where(Function(x) x.OC_Riferimento = OC.OC).First.StatoProgetto = Stato_Prod.Ritorno_Da_UT
                    db.SaveChanges()
                    Return Json(New With {.ok = True, .message = "Progetto già esistente. Aggiornamento stato"}, JsonRequestBehavior.AllowGet)
                End If
                Dim Progetto As New ProgettiProd With {
                    .OC_Riferimento = OC.OC,
                    .DataCreazione = DateTime.Now,
                    .StatoProgetto = Stato_Prod.Ritorno_Da_UT,
                    .Operatore = "Tiziano"
                    }
                db.ProgettiProd.Add(Progetto)
                db.SaveChanges()
                OC.Accettato = Stato_UC.Inviato_Prod
                OC_Proj.StatoProgetto = Stato_UT.Completato
                db.Audit.Add(New Audit With {
                                      .Livello = TipoAuditLivello.Info,
                                      .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                      .Messaggio = "Invio diretto progetto a Produzione",
                                      .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id, .OC = OC.OC}),
                                     .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                        })
                db.SaveChanges()
                db.StoricoOC.Add(New StoricoOC With {
                          .OC = OC.OC,
                          .Descrizione = "Progetto correttamente inviato all'Ufficio Produzione per erroneo smistamento",
                          .Titolo = "Progetto inviato all'Ufficio Produzione",
                          .Ufficio = TipoUfficio.UfficioTecnico,
                          .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                      })
                db.SaveChanges()
            Catch ex As Exception
                db.Log.Add(New Log With {
                                                  .Livello = TipoLogLivello.Errors,
                                                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                  .Messaggio = "Errore: " + ex.Message,
                                                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                    })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore invio in prod. diretto"}, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(New With {.ok = True, .message = "Inviato a produzione correttamente!"}, JsonRequestBehavior.AllowGet)
        End Function
        Function CreateODPEsterno(ByVal id As String) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC_Codice = db.FasiOC.Where(Function(x) x.OP = id).FirstOrDefault
                If IsNothing(OC_Codice) Then
                    Dim ODPEstFinal As New OrdiniDiProduzione
                    myConn = New SqlConnection(ConnectionString)
                    myCmd = myConn.CreateCommand
                    myCmd.CommandText = "SELECT ODLDIUREV,ODLALP FROM ODLTES00 where ODLANN = '" + id.Split("-")(0).ToString + "' AND ODLSEZ = 'OP' AND ODLNMR = " + id.Split("-")(2).ToString + ""
                    myConn.Open()
                    Try
                        myReader = myCmd.ExecuteReader
                        Do While myReader.Read()
                            Dim dateDecimal = myReader.GetDecimal(0).ToString
                            Dim newDate = myReader.GetDecimal(0).ToString.Substring(6, 2) + "/" + myReader.GetDecimal(0).ToString.Substring(4, 2) + "/" + myReader.GetDecimal(0).ToString.Substring(0, 4)
                            Dim ODPEst As New OrdiniDiProduzione With {
                            .Accettato = 0,
                            .Cartella = "",
                            .DataCreazione = DateTime.Now,
                            .DataRichiestaConsegna = Convert.ToDateTime(newDate),
                            .due_dim_Presente = False,
                            .IdOperatoreInsert = OpID,
                            .OperatoreInsert = OpName,
                            .note_presenti = False,
                            .tre_dim_Presente = False,
                            .OP = id,
                            .Articolo = myReader.GetString(1)
                        }
                            ODPEstFinal = ODPEst
                        Loop
                        myConn.Close()
                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                                                 .Livello = TipoLogLivello.Errors,
                                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                 .Messaggio = "Errore: " + ex.Message,
                                                 .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                   })
                        db.SaveChanges()
                    End Try
                    myConn = New SqlConnection(ConnectionString)
                    myCmd = myConn.CreateCommand
                    myCmd.CommandText = "SELECT ODLDIUREV,ORCCRP,ODLALP FROM ODLTES00,ORCTES00 where ODLTES00.ODLCOM = ORCTES00.ORCCOM AND ODLANN = '" + id.Split("-")(0).ToString + "' AND ODLSEZ = 'OP' AND ODLNMR = " + id.Split("-")(2).ToString + " AND ORCTSZ = 'OC' and ESECOD = '" + id.Split("-")(0).ToString + "'"
                    myConn.Open()
                    Try
                        myReader = myCmd.ExecuteReader
                        Do While myReader.Read()
                            ODPEstFinal.Priorita = myReader.GetDecimal(1)

                        Loop
                        myConn.Close()
                        db.OrdiniDiProduzione.Add(ODPEstFinal)
                        db.SaveChanges()
                        Return Json(New With {.ok = True, .message = "ODP correttamente creato."}, JsonRequestBehavior.AllowGet)
                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                                                 .Livello = TipoLogLivello.Errors,
                                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                 .Messaggio = "Errore: " + ex.Message,
                                                 .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                   })
                        db.SaveChanges()
                    End Try
                    Return Json(New With {.ok = False, .message = "Inesistente."}, JsonRequestBehavior.AllowGet)

                Else
                    Dim OC = db.AccettazioneUC.Where(Function(x) x.OC = OC_Codice.OC).First
                    Dim ODPEst As New OrdiniDiProduzione With {
                        .Accettato = 0,
                        .Cartella = "",
                        .DataCreazione = DateTime.Now,
                        .DataRichiestaConsegna = OC.DataRichiestaConsegna,
                        .due_dim_Presente = False,
                        .IdOperatoreInsert = OpID,
                        .OperatoreInsert = OpName,
                        .Priorita = OC.Priorita,
                        .note_presenti = False,
                        .tre_dim_Presente = False,
                        .OP = id,
                        .Articolo = OC_Codice.Articolo
                    }
                    db.OrdiniDiProduzione.Add(ODPEst)
                    db.SaveChanges()
                    db.Audit.Add(New Audit With {
                                                       .Livello = TipoAuditLivello.Info,
                                                       .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                       .Messaggio = "Creato ordine di produzione esterno",
                                                       .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id, .OC = OC.OC, .idOP = ODPEst.OP}),
                                                      .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                         })
                    db.SaveChanges()
                    Return Json(New With {.ok = True, .message = "ODP Esterno correttamente creato."}, JsonRequestBehavior.AllowGet)
                End If
            Catch ex As Exception
                db.Log.Add(New Log With {
                                                 .Livello = TipoLogLivello.Errors,
                                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                 .Messaggio = "Errore: " + ex.Message,
                                                 .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                   })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Impossibile creare ODP Esterno."}, JsonRequestBehavior.AllowGet)
            End Try
        End Function
        Function SendToProd(ByVal id As Integer) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC_Proj = db.ProgettiUT.Where(Function(x) x.Id = id).First
                Dim OC = db.AccettazioneUC.Where(Function(x) x.OC = OC_Proj.OC_Riferimento).First
                Dim exist = db.ProgettiProd.Where(Function(x) x.OC_Riferimento = OC.OC And x.StatoProgetto <> Stato_Prod.Ritorno_Da_UT).Count
                If exist > 0 Then
                    Return Json(New With {.ok = False, .message = "Progetto già esistente."}, JsonRequestBehavior.AllowGet)
                End If
                If db.ProgettiProd.Where(Function(x) x.OC_Riferimento = OC.OC).Count > 0 Then
                    If db.ProgettiProd.Where(Function(x) x.OC_Riferimento = OC.OC).First.StatoProgetto = Stato_Prod.Ritorno_Da_UT Then
                        Dim prog = db.ProgettiProd.Where(Function(x) x.OC_Riferimento = OC.OC).First
                        prog.StatoProgetto = Stato_Prod.In_attesa
                        db.SaveChanges()
                        OC.Accettato = Stato_UC.Inviato_Prod
                        db.SaveChanges()
                        OC_Proj.StatoProgetto = Stato_UT.Inviato
                        db.SaveChanges()
                        db.Audit.Add(New Audit With {
                                                    .Livello = TipoAuditLivello.Info,
                                                    .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                    .Messaggio = "Inviato progetto a Produzione",
                                                    .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id, .OC = OC.OC}),
                                                   .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                      })
                        db.SaveChanges()
                        db.StoricoOC.Add(New StoricoOC With {
                                  .OC = OC.OC,
                                  .Descrizione = "Progetto correttamente inviato all'Ufficio Produzione",
                                  .Titolo = "Progetto inviato all'Ufficio Produzione",
                                  .Ufficio = TipoUfficio.UfficioTecnico,
                                  .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                              })
                        db.SaveChanges()
                    Else
                        Dim Progetto As New ProgettiProd With {
                        .OC_Riferimento = OC.OC,
                        .DataCreazione = DateTime.Now,
                        .StatoProgetto = Stato_Prod.In_attesa,
                        .Operatore = "Tiziano"
                        }
                        db.ProgettiProd.Add(Progetto)
                        db.SaveChanges()
                        OC.Accettato = Stato_UC.Inviato_Prod
                        OC_Proj.StatoProgetto = Stato_UT.Inviato
                        db.SaveChanges()
                        db.Audit.Add(New Audit With {
                                                     .Livello = TipoAuditLivello.Info,
                                                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                     .Messaggio = "Inviato progetto a Produzione",
                                                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id, .OC = OC.OC}),
                                                    .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                       })
                        db.SaveChanges()
                        db.StoricoOC.Add(New StoricoOC With {
                                  .OC = OC.OC,
                                  .Descrizione = "Progetto correttamente inviato all'Ufficio Produzione",
                                  .Titolo = "Progetto inviato all'Ufficio Produzione",
                                  .Ufficio = TipoUfficio.UfficioTecnico,
                                  .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                              })
                        db.SaveChanges()
                    End If
                Else
                    Dim Progetto As New ProgettiProd With {
                      .OC_Riferimento = OC.OC,
                      .DataCreazione = DateTime.Now,
                      .StatoProgetto = Stato_Prod.In_attesa,
                      .Operatore = "Tiziano"
                      }
                    db.ProgettiProd.Add(Progetto)
                    db.SaveChanges()
                    OC.Accettato = Stato_UC.Inviato_Prod
                    OC_Proj.StatoProgetto = Stato_UT.Inviato
                    db.SaveChanges()
                    db.Audit.Add(New Audit With {
                                                 .Livello = TipoAuditLivello.Info,
                                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                 .Messaggio = "Inviato progetto a Produzione",
                                                 .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id, .OC = OC.OC}),
                                                .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                   })
                    db.SaveChanges()
                    db.StoricoOC.Add(New StoricoOC With {
                              .OC = OC.OC,
                              .Descrizione = "Progetto correttamente inviato all'Ufficio Produzione",
                              .Titolo = "Progetto inviato all'Ufficio Produzione",
                              .Ufficio = TipoUfficio.UfficioTecnico,
                              .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                          })
                    db.SaveChanges()
                End If


            Catch ex As System.Exception
                db.Log.Add(New Log With {
                                                  .Livello = TipoLogLivello.Errors,
                                                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                  .Messaggio = "Errore: " + ex.Message,
                                                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                    })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore invio in prod."}, JsonRequestBehavior.AllowGet)

            End Try
            Return Json(New With {.ok = True, .message = "Inviato a produzione correttamente!"}, JsonRequestBehavior.AllowGet)
        End Function
        Function SendToComm(ByVal id As Integer) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC_Proj = db.ProgettiUT.Where(Function(x) x.Id = id).First
                Dim exist = db.AccettazioneUC.Where(Function(x) x.OC = OC_Proj.OC_Riferimento).Count
                If exist = 0 Then
                    Return Json(New With {.ok = False, .message = "Non esiste questo progetto."}, JsonRequestBehavior.AllowGet)
                End If
                Dim OC = db.AccettazioneUC.Where(Function(x) x.OC = OC_Proj.OC_Riferimento).First

                OC.Accettato = Stato_UC.Ritorno_da_UT
                OC_Proj.StatoProgetto = Stato_UT.Inviato
                db.SaveChanges()
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Reinviato OT a Commerciale",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id, .OC = OC.OC}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                db.SaveChanges()
                db.StoricoOC.Add(New StoricoOC With {
                          .OC = OC.OC,
                          .Descrizione = "Progetto correttamente reinviato all'Ufficio Commerciale",
                          .Titolo = "Progetto reinviato all'Ufficio Commerciale",
                          .Ufficio = TipoUfficio.UfficioTecnico,
                          .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                      })
                db.SaveChanges()
            Catch ex As System.Exception
                db.Log.Add(New Log With {
                                                  .Livello = TipoLogLivello.Errors,
                                                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                  .Messaggio = "Errore: " + ex.Message,
                                                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                    })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore invio a Uff. Comm."}, JsonRequestBehavior.AllowGet)

            End Try
            Return Json(New With {.ok = True, .message = "Inviato a Commerciale correttamente!"}, JsonRequestBehavior.AllowGet)
        End Function
        Function EditAdmin(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim progettiUT As ProgettiUT = db.ProgettiUT.Find(id)
            Dim roleId = appctx.Roles.Where(Function(m) m.Name = "Tecnico").[Select](Function(m) m.Id).SingleOrDefault()
            Dim roleIdAdmin = appctx.Roles.Where(Function(m) m.Name = "TecnicoAdmin").[Select](Function(m) m.Id).SingleOrDefault()
            Dim list = appctx.Users.Where(Function(u) u.Roles.Any(Function(r) r.RoleId = roleId Or r.RoleId = roleIdAdmin)).ToList()
            ViewBag.Operatore = New SelectList(list, "Id", "Username")

            If IsNothing(progettiUT) Then
                Return HttpNotFound()
            End If
            Return PartialView(progettiUT)
        End Function
        Function EditEsterno(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim OrdiniDiProduzione As OrdiniDiProduzione = db.OrdiniDiProduzione.Find(id)
            If IsNothing(OrdiniDiProduzione) Then
                Return HttpNotFound()
            End If
            Return PartialView(OrdiniDiProduzione)
        End Function
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function EditEsterno(<Bind(Include:="Id,tre_dim_Presente,due_dim_Presente,note_presenti")> ByVal OrdiniDiProduzione As OrdiniDiProduzione) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                If ModelState.IsValid Then
                    'Ricerca username
                    Dim prog = db.OrdiniDiProduzione.Where(Function(x) x.Id = OrdiniDiProduzione.Id).First
                    If prog.tre_dim_Presente <> OrdiniDiProduzione.tre_dim_Presente Then
                        prog.tre_dim_Presente = OrdiniDiProduzione.tre_dim_Presente
                    End If
                    If prog.due_dim_Presente <> OrdiniDiProduzione.due_dim_Presente Then
                        prog.due_dim_Presente = OrdiniDiProduzione.due_dim_Presente
                    End If
                    If prog.note_presenti <> OrdiniDiProduzione.note_presenti Then
                        prog.note_presenti = OrdiniDiProduzione.note_presenti
                    End If
                    If prog.tre_dim_Presente = True And prog.due_dim_Presente = True And prog.note_presenti = True Then
                        prog.Accettato = Stato_Ordine_Di_Produzione_Esterno.In_attesa_est
                        db.SaveChanges()
                        db.StoricoOC.Add(New StoricoOC With {
                            .OC = prog.OP,
                            .Descrizione = "Completato progetto internamente, ora inviato all'esterno",
                            .Titolo = "Caricamento dati completato",
                            .Ufficio = TipoUfficio.UfficioTecnico,
                            .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                        })
                        db.SaveChanges()
                        Return Json(New With {.ok = True, .message = "Progetto correttamente completato."})
                    Else
                        db.StoricoOC.Add(New StoricoOC With {
                           .OC = prog.OP,
                           .Descrizione = "Modifica stati ordine di produzione",
                           .Titolo = "Modifica stati ODP",
                           .Ufficio = TipoUfficio.UfficioTecnico,
                           .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                       })
                        db.SaveChanges()
                        Return Json(New With {.ok = True, .message = "Progetto correttamente aggiornato."})
                    End If
                End If
                Return Json(New With {.ok = False, .message = "Errore nell'aggiornamento del progetto."})
            Catch ex As Exception
                db.Log.Add(New Log With {
                                                  .Livello = TipoLogLivello.Errors,
                                                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                  .Messaggio = "Errore: " + ex.Message,
                                                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                    })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore nell'aggiornamento del file."})
            End Try
            Return Json(New With {.ok = False, .message = "Errore nell'aggiornamento del file."})
        End Function
        ' POST: ProgettiUT/Edit/5
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function EditAdmin(<Bind(Include:="Id,Operatore,Note,Flag_Invio_Materiali")> ByVal progettiUT As ProgettiUT) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                If ModelState.IsValid Then
                    'Ricerca username
                    Dim u = appctx.Users.Where(Function(x) x.Id = progettiUT.Operatore).First
                    Dim prog = db.ProgettiUT.Where(Function(x) x.Id = progettiUT.Id).First
                    prog.OperatoreId = progettiUT.Operatore
                    prog.Operatore = u.UserName
                    prog.Note = progettiUT.Note
                    prog.Flag_Invio_Materiali = progettiUT.Flag_Invio_Materiali
                    prog.OperatoreSmistamento = User.Identity.Name
                    prog.OperatoreSmistamentoId = User.Identity.GetUserId
                    prog.StatoProgetto = Stato_UT.In_Attesa_Operatore
                    db.SaveChanges()
                    db.StoricoOC.Add(New StoricoOC With {
                            .OC = prog.OC_Riferimento,
                            .Descrizione = "Documento aggiornato con Operatore di riferimento",
                            .Titolo = "Assegnato Operatore",
                            .Ufficio = TipoUfficio.UfficioTecnico,
                            .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                        })
                    db.SaveChanges()
                    Return Json(New With {.ok = True, .message = "Progetto correttamente aggiornato."})
                End If
                Return Json(New With {.ok = False, .message = "Errore nell'aggiornamento del file."})
            Catch ex As Exception
                db.Log.Add(New Log With {
                                                  .Livello = TipoLogLivello.Errors,
                                                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                  .Messaggio = "Errore: " + ex.Message,
                                                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                    })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore nell'aggiornamento del file."})
            End Try
            Return Json(New With {.ok = False, .message = "Errore nell'aggiornamento del file."})
        End Function

        ' GET: ProgettiUT/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim progettiUT As ProgettiUT = db.ProgettiUT.Find(id)
            If IsNothing(progettiUT) Then
                Return HttpNotFound()
            End If
            Return View(progettiUT)
        End Function

        ' POST: ProgettiUT/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim progettiUT As ProgettiUT = db.ProgettiUT.Find(id)
            db.ProgettiUT.Remove(progettiUT)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function
        Private Function MakeWhereExpression(Search As String) As Expressions.Expression(Of Func(Of ProgettiUT, Boolean))
            Return Function(x) x.OC_Riferimento.Contains(Search) Or
                        x.Operatore.Contains(Search)
        End Function
        Private Function MakeOrderExpression(Column As Integer) As Expressions.Expression(Of Func(Of ProgettiUT, String))
            Select Case Column
                Case Nothing : Return Function(x) x.Priorita
                Case 1 : Return Function(x) x.Priorita
                Case 2 : Return Function(x) x.DataRichiestaConsegna
                Case 3 : Return Function(x) x.OC_Riferimento
                Case 4 : Return Function(x) x.Operatore
                Case 5 : Return Function(x) x.StatoProgetto
                Case Else : Return Function(x) x.DataRichiestaConsegna
            End Select
        End Function
        Private Function MakeWhereExpressionEsterno(Search As String) As Expressions.Expression(Of Func(Of OrdiniDiProduzione, Boolean))
            Return Function(x) x.OP.Contains(Search) Or
                        x.OperatoreInsert.Contains(Search)
        End Function
        Private Function MakeOrderExpressionEsterno(Column As Integer) As Expressions.Expression(Of Func(Of OrdiniDiProduzione, String))
            Select Case Column
                Case Nothing : Return Function(x) x.Priorita
                Case 1 : Return Function(x) x.Priorita
                Case 2 : Return Function(x) x.DataRichiestaConsegna
                Case 3 : Return Function(x) x.OP
                Case 4 : Return Function(x) x.OperatoreInsert
                Case 5 : Return Function(x) x.Accettato
                Case Else : Return Function(x) x.DataRichiestaConsegna
            End Select
        End Function
        Private Function PartialToString(ByVal viewName As String, ByVal model As Object) As String
            ViewData.Model = model

            Using writer As IO.StringWriter = New IO.StringWriter()
                Dim vResult As ViewEngineResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName)
                Dim vContext As ViewContext = New ViewContext(Me.ControllerContext, vResult.View, ViewData, New TempDataDictionary(), writer)
                vResult.View.Render(vContext, writer)
                Return writer.ToString()
            End Using
        End Function
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
