Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Net
Imports Microsoft.AspNet.Identity

Namespace Controllers
    Public Class ProgettiProdController
        Inherits System.Web.Mvc.Controller
        Private Const ConnectionString As String = "Persist Security Info=True;Password=ALNUSAD;User ID=ALNUSAD;Initial Catalog=ALNEUMA;Data Source=192.168.100.50"
        Private myConn As SqlConnection
        Private myCmd As SqlCommand
        Private myReader As SqlDataReader
        Private results As String

        Private db As New EuromaModels
        Private appctx As New ApplicationDbContext

        ' GET: ProgettiProd
        <Authorize>
        Function Index() As ActionResult
            Return View(db.ProgettiProd.ToList())
        End Function
        Function Schedulatore() As ActionResult
            Return View()
        End Function
        ' GET: ProgettiProd/Details/5
        <Authorize>
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Progetto -> " & id & ". Impossibile recuperare i dati."})
            End If
            Dim ProgettiProd As ProgettiProd = db.ProgettiProd.Find(id)
            Dim listArticoli = db.ArticoliPerOC.Where(Function(x) x.OC = ProgettiProd.OC_Riferimento).ToList
            Dim listNote = db.NotePerOC.Where(Function(x) x.OC = ProgettiProd.OC_Riferimento).ToList
            Dim listDocumenti = db.DocumentiPerOC.Where(Function(x) x.OC = ProgettiProd.OC_Riferimento).ToList
            Dim accettazioneID = db.AccettazioneUC.Where(Function(x) x.OC = ProgettiProd.OC_Riferimento).First.Id
            Dim putVM As New ProgettiProdViewModel With {
                .StatoProgetto = ProgettiProd.StatoProgetto,
                .DataCompletamento = ProgettiProd.DataCompletamento,
                .DataCreazione = ProgettiProd.DataCreazione,
                .File = ProgettiProd.File,
                .Id = ProgettiProd.Id,
                .ListaArt = listArticoli,
                .ListOfNote = listNote,
                .ListOfDocumenti = listDocumenti,
                .Note = ProgettiProd.Note,
                .OC_Riferimento = ProgettiProd.OC_Riferimento,
                .Id_OC = accettazioneID,
                .Operatore = ProgettiProd.Operatore,
                .OperatoreId = ProgettiProd.Operatore,
                .OperatoreSmistamento = ProgettiProd.OperatoreSmistamento,
                .OperatoreSmistamentoId = ProgettiProd.OperatoreSmistamentoId
            }
            If IsNothing(ProgettiProd) Then
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Progetto -> " & id & ". Impossibile recuperare i dati."})
            End If
            Dim Id_OC = db.AccettazioneUC.Where(Function(x) x.OC = ProgettiProd.OC_Riferimento).First.Id
            ViewBag.Id_OC = Id_OC
            Return Json(New With {.ok = True, .message = PartialToString("Details", putVM)}, JsonRequestBehavior.AllowGet)
        End Function

        ' GET: ProgettiProd/Create
        <Authorize>
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: ProgettiProd/Create
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,OC_Riferimento,OperatoreSmistamento,OperatoreSmistamentoId,Operatore,OperatoreId,DataCreazione,DataCompletamento,Note,File,StatoProgetto")> ByVal progettiProd As ProgettiProd) As ActionResult
            If ModelState.IsValid Then
                db.ProgettiProd.Add(progettiProd)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(progettiProd)
        End Function
        Function DirectSendToUT(ByVal id As Integer) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC_Proj = db.ProgettiProd.Where(Function(x) x.Id = id).First
                Dim OC = db.AccettazioneUC.Where(Function(x) x.OC = OC_Proj.OC_Riferimento).First
                Dim exist = db.ProgettiUT.Where(Function(x) x.OC_Riferimento = OC.OC).Count
                If exist > 0 Then
                    db.ProgettiUT.Where(Function(x) x.OC_Riferimento = OC.OC).First.StatoProgetto = Stato_UT.Ritorno_Da_Prod
                    db.SaveChanges()
                    Return Json(New With {.ok = True, .message = "Progetto già esistente. Aggiornamento stato"}, JsonRequestBehavior.AllowGet)
                End If
                Dim Progetto As New ProgettiUT With {
                    .OC_Riferimento = OC.OC,
                    .DataCreazione = DateTime.Now,
                    .StatoProgetto = Stato_Prod.Ritorno_Da_UT,
                    .DataRichiestaConsegna = OC.DataRichiestaConsegna,
                    .Priorita = OC.Priorita
                    }
                db.ProgettiUT.Add(Progetto)
                db.SaveChanges()
                OC.Accettato = Stato_UC.Inviato_UT
                OC_Proj.StatoProgetto = Stato_Prod.Ritorno_Da_UT
                db.Audit.Add(New Audit With {
                                      .Livello = TipoAuditLivello.Info,
                                      .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                      .Messaggio = "Invio diretto progetto a Tecnico",
                                      .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id, .OC = OC.OC}),
                                     .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                        })
                db.SaveChanges()
                db.StoricoOC.Add(New StoricoOC With {
                          .OC = OC.OC,
                          .Descrizione = "Progetto correttamente inviato all'Ufficio Tecnico per erroneo smistamento",
                          .Titolo = "Progetto inviato all'Ufficio Tecnico",
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
                Return Json(New With {.ok = False, .message = "Errore invio in UT diretto"}, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(New With {.ok = True, .message = "Inviato a UT correttamente!"}, JsonRequestBehavior.AllowGet)
        End Function
        ' GET: ProgettiProd/Edit/5
        Function EditAdmin(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim progettiProd As ProgettiProd = db.ProgettiProd.Find(id)
            If IsNothing(progettiProd) Then
                Return HttpNotFound()
            End If
            Return PartialView(progettiProd)
        End Function
        <HttpPost()>
        <ValidateInput(False)>
        Function ServerProcessingSchedulatore(PostedData As DataTableAjaxPostModel) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()

                Dim result As New List(Of Object)
                Dim dataTMP As New List(Of ODPProduzioneViewModel)
                Dim data As IQueryable(Of ODPProduzioneViewModel)
                Dim listaParallelaTMP = db.FasiOC.Where(Function(x) x.Completata = 0).GroupBy(Function(x) x.OP).Select(Function(x) x.Key).ToList
                Dim listaParallela = listaParallelaTMP.AsQueryable
                'paginazione
                Dim filtered As Integer = 0
                Try
                    filtered = listaParallela.Count
                    If PostedData.length > 0 Then
                        listaParallela = listaParallela.Skip(PostedData.start).Take(PostedData.length * 5)
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
                For Each elem In listaParallela.ToList
                    Dim l = db.FasiOC.Where(Function(x) x.OP = elem).FirstOrDefault
                    Dim stato = False
                    Try
                        myConn = New SqlConnection(ConnectionString)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "SELECT COUNT(*) FROM ODLTES00 where ODLSTS < '080' and ODLANN = '" + l.OP.Split("-")(0) + "' AND ODLSEZ = 'OP' AND ODLNMR = '" + l.OP.Split("-")(2) + "'"
                        myConn.Open()
                    Catch ex As Exception
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader
                        Do While myReader.Read()
                            Dim Conteggio = myReader.GetInt32(0)
                            If Conteggio > 0 Then
                                stato = True
                            End If
                        Loop
                        myConn.Close()
                    Catch ex As Exception

                    End Try
                    If stato Then
                        Try
                            If Not dataTMP.Where(Function(x) x.Articolo = l.Articolo).Any Then
                                Dim artdes = ""
                                myConn = New SqlConnection(ConnectionString)
                                myCmd = myConn.CreateCommand
                                myCmd.CommandText = "select ARTDES from ARTANA where ARTCO1 = '" + l.Articolo + "'"
                                myConn.Open()
                                Try
                                    myReader = myCmd.ExecuteReader
                                    Dim countDB = 0
                                    Do While myReader.Read()
                                        artdes = myReader.GetString(0)
                                    Loop
                                    myConn.Close()
                                Catch ex As Exception
                                End Try
                                Dim cliente = ""
                                Dim name = db.AccettazioneUC.Where(Function(x) x.OC = l.OC).First.Cliente
                                If name.Length > 0 Then
                                    cliente = name
                                End If
                                Dim listFasiPerOP = db.FasiOC.Where(Function(X) X.OP = l.OP).ToList
                                Dim OC = db.FasiOC.Where(Function(X) X.OP = l.OP).First.OC
                                dataTMP.Add(New ODPProduzioneViewModel With {
                                .Articolo = l.Articolo.Trim,
                                .OC = OC,
                                .Cliente = IIf(IsNothing(cliente), "", cliente.Trim),
                                .Completato = 0,
                                .Desc_Art = artdes,
                                .ODP = l.OP,
                                .Data_Inizio_Attività = DateTime.Now,
                                .ListaAttivita = listFasiPerOP
                            })
                            End If
                        Catch ex As Exception

                        End Try

                    Else
                        l.Completata = True
                        db.SaveChanges()
                    End If

                Next
                Try
                    Do While dataTMP.Count > (PostedData.length)
                        ' Last node and the previous one
                        Dim dLast = dataTMP.Last
                        dataTMP.Remove(dLast)
                    Loop
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
                'ricerca

                Try
                    If Not IsNothing(PostedData.search.value) Then
                        If Not PostedData.search.value.Contains(" ") Then 'singola parola
                            Dim search As String = PostedData.search.value
                            'Dim w As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, Boolean)) = MakeWhereExpressionSchedulatore(search)
                            'w.Compile()
                            'data = data.Where(w)
                            dataTMP = dataTMP.Where(Function(X) X.ODP.Contains(search) Or X.Articolo.Contains(search) Or X.Desc_Art.Contains(search) Or X.OC.Contains(search)).ToList
                            Dim es = ""
                        Else 'multiple
                            'For Each term As String In PostedData.search.value.Split(" ")
                            '    Dim wpartial As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, Boolean)) = MakeWhereExpressionSchedulatore(term)
                            '    wpartial.Compile()
                            '    data = data.Where(wpartial)
                            'Next
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
                data = dataTMP.AsQueryable
                Try
                    If PostedData.order.Count = 0 Then
                        Dim o As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, String))
                        o = MakeOrderExpressionSchedulatore(Nothing) 'default
                        o.Compile()
                        data = data.OrderBy(o)
                        'data = data.OrderBy(CreateExpression(Of Amministratore)("Studio"))
                        'data = OrderByDynamic(data, "Studio", False)
                    ElseIf PostedData.order.Count = 1 Then 'singolo
                        Dim o As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, String)) = MakeOrderExpressionSchedulatore(PostedData.order(0).column)
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
                        Dim o As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, String)) = MakeOrderExpressionSchedulatore(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, String)) = MakeOrderExpressionSchedulatore(PostedData.order(1).column)
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
                        Dim o As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, String)) = MakeOrderExpressionSchedulatore(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, String)) = MakeOrderExpressionSchedulatore(PostedData.order(1).column)
                        o2.Compile()

                        Dim o3 As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, String)) = MakeOrderExpressionSchedulatore(PostedData.order(2).column)
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
                Catch ex As Exception

                End Try
                'esecuzione (spero)
                For Each Acc As ODPProduzioneViewModel In data

                    Try
                        Dim ListaMancanti As New Dictionary(Of String, String)
                        Dim listMancanti = RecursiveDistinta(New DBMancanteViewModel With {.Codice = Acc.Articolo, .ListaArt = New List(Of DBMancanteViewModel)}, ListaMancanti)
                        Dim countTotFasi = db.FasiOC.Where(Function(x) x.OP = Acc.ODP And x.Articolo = Acc.Articolo).Count
                        Dim countComplFasi = db.FasiOC.Where(Function(x) x.OP = Acc.ODP And x.Articolo = Acc.Articolo And x.Completata = True).Count
                        Dim StatoProgetto = ""
                        If countComplFasi = 0 Then
                            StatoProgetto = "<div Class='progress-bar bg-danger progress-bar-striped progress-bar-animated' role='progressbar' style='width:  10%;border-radius: 8px;' aria-valuenow='0' aria-valuemin='0' aria-valuemax='100'>0%</div>"
                        Else
                            Dim valFasi = (countComplFasi * 100) / countTotFasi
                            Dim colore = ""
                            Select Case valFasi ' now integer is the target type
                                Case 0 To 9
                                    colore = "danger"
                                Case 10 To 75
                                    colore = "warning"
                                Case 76 To 99
                                    colore = "info"
                                Case 100
                                    colore = "success"
                                Case Else
                                    colore = "danger"
                            End Select
                            StatoProgetto = "<div Class='progress-bar bg-" + colore + " progress-bar-striped progress-bar-animated' role='progressbar' style='width:  " + valFasi.ToString.Split(",")(0) + "%;border-radius: 8px;' aria-valuenow='" + valFasi.ToString.Split(",")(0) + "' aria-valuemin='0' aria-valuemax='100'>" + valFasi.ToString.Split(",")(0) + "%</div>"
                        End If
                        Dim title = "title='"
                        Dim typeMancanti = "<i style='color:#228B22;'class='fa-solid fa-check' data-toggle='tooltip' data-placement='top'></i>"

                        For Each m In ListaMancanti
                            title = title + m.Key.Trim() + " - " + m.Value + " | "
                        Next
                        title = title + "'"
                        For Each m In ListaMancanti
                            If typeMancanti = "<i style='color:#228B22;'class='fa-solid fa-check' data-toggle='tooltip' data-placement='top'></i>" And m.Value = "Mancante" Then
                                typeMancanti = "<i style='color:#FF0000;'class='fa-solid fa-circle-exclamation' data-toggle='tooltip' data-placement='top'" + title + "></i>"
                            End If
                            If typeMancanti = "<i style='color:#228B22;'class='fa-solid fa-check' data-toggle='tooltip' data-placement='top'></i>" And m.Value = "In Attesa" Then
                                typeMancanti = "<i style='color:#4682B4;'class='fa-solid fa-clock' data-toggle='tooltip' data-placement='top'" + title + "></i>"
                            End If
                        Next
                        result.Add(New With {
                                .DT_RowData = New With {.value = Acc.ODP},
                                .DT_RowId = "row_" & Acc.ODP,
                                .Id = Acc.ODP,
                                .OC = Acc.OC,
                                .ODP = "<a style='margin-right:8px!important;text-decoration:none!important;'href='/Overviews/Ordine/" + Acc.ODP.ToString + "' Target='_blank'>" + Acc.ODP + "(" + Acc.Cliente + ")" + "</a>" + typeMancanti,
                                .Articolo = Acc.Articolo,
                                .Descrizione = Acc.Desc_Art,
                                .StatoProgetto = StatoProgetto,
                                .ListaAttivita = Acc.ListaAttivita,
                                .Azioni = "<btn class='btn btn-primary' data-value='" + Acc.ODP + "' data-bs-toggle='modal' data-bs-target='#exampleModal' data-type='show_gantt'> <i class='fa-solid fa-chart-simple'></i></btn>"
                           })
                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                             .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                             .Livello = TipoLogLivello.Errors,
                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                             .Messaggio = "Errore Creazione Lista Progetti in ODP Produzione (" & Acc.ODP & ") -> " & ex.Message & " [" & ex.InnerException.Message & "]",
                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                        })
                        db.SaveChanges()
                    End Try
                Next

                Return Json(New With {PostedData.draw, .recordsTotal = db.ProgettiProd.Count, .recordsFiltered = filtered, .data = result})
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
            Return Json(New With {PostedData.draw, .recordsTotal = db.ProgettiProd.Count, .recordsFiltered = 0})
        End Function
        Function RecursiveDistinta(ByVal listaArticoliDaDistinta As DBMancanteViewModel, ByRef ListaMancanti As Dictionary(Of String, String))
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "select DIBCOM,DIBQTC from DIBDCO00 where ARTCOD = '" + listaArticoliDaDistinta.Codice + "'"
                myConn.Open()
            Catch ex As Exception
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    listaArticoliDaDistinta.ListaArt.Add(New DBMancanteViewModel With {
                        .Codice = myReader.GetString(0),
                        .ListaArt = New List(Of DBMancanteViewModel),
                        .Qta = myReader.GetDecimal(1)
                    })
                Loop
                myConn.Close()
                For Each l In listaArticoliDaDistinta.ListaArt
                    l = RecursiveDistinta(l, ListaMancanti)
                Next
                Try
                    myConn = New SqlConnection(ConnectionString)
                    myCmd = myConn.CreateCommand
                    myCmd.CommandText = "select ARTGIA as Giacenza, ARTQIP as Impegni, ARTQPS as Ordini from ARTDMA where ARTCOD = '" + listaArticoliDaDistinta.Codice.ToString + "'"
                    myConn.Open()
                Catch ex As Exception
                    Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                End Try
                Try
                    myReader = myCmd.ExecuteReader
                    Do While myReader.Read()
                        Dim Giacenza = myReader.GetDecimal(0)
                        Dim Impegni = myReader.GetDecimal(1)
                        Dim Ordini = myReader.GetDecimal(2)
                        'Giacenza > Impegni - nero
                        'Impegni > Giacenza e ordini = 0 - rosso
                        'Impegni > Giacenza e ordini > 0 - blu
                        If Giacenza >= Impegni Then
                            listaArticoliDaDistinta.Colore = "#000000"
                        ElseIf Impegni > Giacenza And Ordini = 0 Then
                            listaArticoliDaDistinta.Colore = "#BD1206"
                            ListaMancanti.Add(listaArticoliDaDistinta.Codice.ToString, "Mancante")
                        ElseIf Impegni > Giacenza And Ordini > 0 Then
                            listaArticoliDaDistinta.Colore = "#1F02D9"
                            ListaMancanti.Add(listaArticoliDaDistinta.Codice.ToString, "In attesa")
                        End If
                    Loop
                    myConn.Close()
                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try
            Return listaArticoliDaDistinta
        End Function
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
                Dim data As IQueryable(Of ProgettiProd)
                data = db.ProgettiProd.Where(Function(x) x.StatoProgetto < Stato_Prod.Rilasciato).OrderBy(Function(x) x.DataCreazione)
                'ricerca
                Try
                    If Not IsNothing(PostedData.search.value) Then
                        If Not PostedData.search.value.Contains(" ") Then 'singola parola
                            Dim search As String = PostedData.search.value
                            Dim w As Expressions.Expression(Of Func(Of ProgettiProd, Boolean)) = MakeWhereExpression(search)
                            w.Compile()
                            data = data.Where(w)
                        Else 'multiple
                            For Each term As String In PostedData.search.value.Split(" ")
                                Dim wpartial As Expressions.Expression(Of Func(Of ProgettiProd, Boolean)) = MakeWhereExpression(term)
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

                'esecuzione (spero)
                For Each Acc As ProgettiProd In data
                    Try
                        Dim finalButtons = ""
                        Dim StatoProgetto = ""
                        Dim flagInvio = ""
                        Select Case Acc.StatoProgetto
                            Case 0
                                StatoProgetto = "<div Class='progress-bar bg-danger progress-bar-striped progress-bar-animated' role='progressbar' style='width:  10%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>0%</div>"
                            Case 5
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  15%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>5%</div>"
                            Case 25
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  25%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>25%</div>"
                            Case 50
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  50%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>50%</div>"
                            Case 90
                                StatoProgetto = "<div Class='progress-bar bg-info progress-bar-striped progress-bar-animated' role='progressbar' style='width:  90%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>90%</div>"
                            Case 100
                                StatoProgetto = "<div Class='progress-bar bg-success progress-bar-striped progress-bar-animated' role='progressbar' style='width:  100%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>100%</div>"

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
                                .OC = "<a style='margin-right:8px!important;text-decoration:none!important;'href='/Overviews/Ordine/" + Acc.OC_Riferimento.ToString + "' Target='_blank'>" + Acc.OC_Riferimento + "(" + ")" + "</a>" + notificheNote + notificheFile,
                                .Operatore = Acc.Operatore,
                                .StatoProgetto = StatoProgetto
                           })

                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                             .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                             .Livello = TipoLogLivello.Errors,
                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                             .Messaggio = "Errore Creazione Lista Progetti in Produzione (" & Acc.Id & ") -> " & ex.Message & " [" & ex.InnerException.Message & "]",
                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                        })
                        db.SaveChanges()
                    End Try
                Next

                Return Json(New With {PostedData.draw, .recordsTotal = db.ProgettiProd.Count, .recordsFiltered = filtered, .data = result})
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
            Return Json(New With {PostedData.draw, .recordsTotalPROD = db.ProgettiProd.Count, .recordsFiltered = 0})
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
                Dim data As IQueryable(Of ProgettiProd)
                data = db.ProgettiProd.Where(Function(x) x.StatoProgetto = Stato_Prod.Rilasciato).OrderBy(Function(x) x.DataCreazione)
                'ricerca
                Try
                    If Not IsNothing(PostedData.search.value) Then
                        If Not PostedData.search.value.Contains(" ") Then 'singola parola
                            Dim search As String = PostedData.search.value
                            Dim w As Expressions.Expression(Of Func(Of ProgettiProd, Boolean)) = MakeWhereExpression(search)
                            w.Compile()
                            data = data.Where(w)
                        Else 'multiple
                            For Each term As String In PostedData.search.value.Split(" ")
                                Dim wpartial As Expressions.Expression(Of Func(Of ProgettiProd, Boolean)) = MakeWhereExpression(term)
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

                'esecuzione (spero)
                For Each Acc As ProgettiProd In data
                    Try
                        Dim finalButtons = ""
                        Dim StatoProgetto = ""
                        Dim flagInvio = ""
                        Select Case Acc.StatoProgetto
                            Case 0
                                StatoProgetto = "<div Class='progress-bar bg-danger progress-bar-striped progress-bar-animated' role='progressbar' style='width:  10%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>0%</div>"
                            Case 5
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  15%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>5%</div>"
                            Case 25
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  25%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>25%</div>"
                            Case 50
                                StatoProgetto = "<div Class='progress-bar bg-warning progress-bar-striped progress-bar-animated' role='progressbar' style='width:  50%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>50%</div>"
                            Case 90
                                StatoProgetto = "<div Class='progress-bar bg-info progress-bar-striped progress-bar-animated' role='progressbar' style='width:  90%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>90%</div>"
                            Case 100
                                StatoProgetto = "<div Class='progress-bar bg-success progress-bar-striped progress-bar-animated' role='progressbar' style='width:  100%;border-radius: 8px;' aria-valuenow='@item.StatoProgetto' aria-valuemin='0' aria-valuemax='100'>100%</div>"

                        End Select
                        result.Add(New With {
                                .DT_RowData = New With {.value = Acc.Id},
                                .DT_RowId = "row_" & Acc.Id,
                                .Id = Acc.Id,
                                .OC = "<a style='text-decoration:none!important;'href='/Overviews/Ordine/" + Acc.OC_Riferimento.ToString + "' Target='_blank'>" + Acc.OC_Riferimento + "(" + ")" + "</a>",
                                .Operatore = Acc.Operatore,
                                .StatoProgetto = StatoProgetto
                           })

                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                             .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                             .Livello = TipoLogLivello.Errors,
                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                             .Messaggio = "Errore Creazione Lista Progetti in Produzione (" & Acc.Id & ") -> " & ex.Message & " [" & ex.InnerException.Message & "]",
                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                        })
                        db.SaveChanges()
                    End Try
                Next

                Return Json(New With {PostedData.draw, .recordsTotal = db.ProgettiProd.Count, .recordsFiltered = filtered, .data = result})
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
            Return Json(New With {PostedData.draw, .recordsTotalPROD = db.ProgettiProd.Count, .recordsFiltered = 0})
        End Function
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function EditAdmin(<Bind(Include:="Id,OC_Riferimento,Note,StatoProgetto")> ByVal progProd As ProgettiProd) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                If ModelState.IsValid Then
                    'Ricerca username
                    Dim prog = db.ProgettiProd.Where(Function(x) x.Id = progProd.Id).First
                    prog.OperatoreId = User.Identity.GetUserId
                    prog.Operatore = User.Identity.Name
                    prog.Note = progProd.Note
                    prog.OperatoreSmistamento = User.Identity.Name
                    prog.OperatoreSmistamentoId = User.Identity.GetUserId
                    prog.StatoProgetto = progProd.StatoProgetto
                    db.SaveChanges()
                    db.StoricoOC.Add(New StoricoOC With {
                                .OC = prog.OC_Riferimento,
                                .Descrizione = "Aggiornamento stato in Produzione: " + prog.StatoProgetto.ToString,
                                .Titolo = "Aggiornamento Stato Produzione",
                                .Ufficio = TipoUfficio.Produzione,
                                .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                            })
                    db.SaveChanges()
                    If prog.StatoProgetto = Stato_Prod.Rilasciato Then
                        Try
                            Dim OCAlnusCode = prog.OC_Riferimento.Substring(2, 2) + "OC"
                            Dim incOC = prog.OC_Riferimento.Split("-").Last
                            For i = 1 To 6 - incOC.Count
                                OCAlnusCode = OCAlnusCode + "0"
                            Next
                            OCAlnusCode = OCAlnusCode + incOC
                            myConn = New SqlConnection(ConnectionString)
                            myCmd = myConn.CreateCommand
                            myCmd.CommandText = "select ODLMOP00.ODLANN, ODLMOP00.ODLSEZ, ODLMOP00.ODLNMR, ODLTES00.ODLALP, ODLPDP, ODLPPR, ODLFSE, OPRDES,ODLCMC from ODLMOP00,TABOPR00,ODLTES00 where ODLMOP00.ODLOPR =  TABOPR00.OPRCO1  AND ODLMOP00.ODLCMO = '" + OCAlnusCode + "' AND ODLMOP00.ODLANN = ODLTES00.ODLANN AND ODLMOP00.ODLSEZ = ODLTES00.ODLSEZ AND ODLMOP00.ODLNMR = ODLTES00.ODLNMR"
                            myConn.Open()
                            Try
                                myReader = myCmd.ExecuteReader
                                Do While myReader.Read()
                                    Dim OP = myReader.GetString(0).ToString + "-" + myReader.GetString(1).ToString + "-" + myReader.GetDecimal(2).ToString
                                    Dim Fa = myReader.GetDecimal(6)
                                    Dim Art = myReader.GetString(3)
                                    Dim count = db.FasiOC.Where(Function(x) x.OP = OP And x.Fase = Fa And x.Articolo = Art).Count

                                    If count = 0 Then
                                        db.FasiOC.Add(New FasiOC With {
                                         .OP = OP,
                                         .Articolo = myReader.GetString(3),
                                         .Qta_Da_Produrre = myReader.GetDecimal(4),
                                         .Qta_Prodotta = myReader.GetDecimal(5),
                                         .Fase = myReader.GetDecimal(6),
                                         .Descrizione_Fase = myReader.GetString(7),
                                         .OC = prog.OC_Riferimento,
                                         .Completata = False,
                                         .Macchina = myReader.GetString(8)
                                       })
                                        db.SaveChanges()
                                    End If
                                Loop
                                myConn.Close()

                                db.SaveChanges()
                            Catch ex As Exception

                            End Try
                        Catch ex As Exception
                        End Try
                    End If
                    Return Json(New With {.ok = True, .message = "Progetto correttamente aggiornato."})
                End If
                Return Json(New With {.ok = False, .message = "Errore nell'aggiornamento del progetto."})
            Catch ex As Exception
                db.Log.Add(New Log With {
                    .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                    .Livello = TipoLogLivello.Errors,
                    .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                    .Messaggio = "Errore aggiornamento Prod -> " & ex.Message,
                    .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = progProd.Id})
                    })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore nell'aggiornamento del progetto."})
            End Try
            Return Json(New With {.ok = False, .message = "Errore nell'aggiornamento del progetto."})


        End Function
        ' GET: ProgettiProd/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim progettiProd As ProgettiProd = db.ProgettiProd.Find(id)
            If IsNothing(progettiProd) Then
                Return HttpNotFound()
            End If
            Return View(progettiProd)
        End Function

        ' POST: ProgettiProd/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim progettiProd As ProgettiProd = db.ProgettiProd.Find(id)
            db.ProgettiProd.Remove(progettiProd)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function
        Private Function MakeWhereExpression(Search As String) As Expressions.Expression(Of Func(Of ProgettiProd, Boolean))
            Return Function(x) x.OC_Riferimento.Contains(Search) Or
                        x.Operatore.Contains(Search)
        End Function
        Private Function MakeWhereExpressionSchedulatore(Search As String) As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, Boolean))
            Return Function(x) x.ODP.Contains(Search) Or
                               x.Articolo.Contains(Search) Or
                               x.OC.Contains(Search) Or
                               x.Cliente.Contains(Search) Or
                               x.Desc_Art.Contains(Search)
        End Function
        Private Function MakeOrderExpression(Column As Integer) As Expressions.Expression(Of Func(Of ProgettiProd, String))
            Select Case Column
                Case Nothing : Return Function(x) x.OC_Riferimento
                Case 1 : Return Function(x) x.OC_Riferimento
                Case 2 : Return Function(x) x.Operatore
                Case Else : Return Function(x) x.OC_Riferimento
            End Select
        End Function
        Private Function MakeOrderExpressionSchedulatore(Column As Integer) As Expressions.Expression(Of Func(Of ODPProduzioneViewModel, String))
            Select Case Column
                Case Nothing : Return Function(x) x.ODP
                Case 0 : Return Function(x) x.ODP
                Case 1 : Return Function(x) x.OC
                Case 2 : Return Function(x) x.Articolo
                Case 3 : Return Function(x) x.Desc_Art
                Case Else : Return Function(x) x.ODP
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
