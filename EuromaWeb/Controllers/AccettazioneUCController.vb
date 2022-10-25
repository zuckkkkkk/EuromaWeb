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
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports Microsoft.AspNet.Identity
Imports System.Reflection
Imports Outlook = Microsoft.Office.Interop.Outlook
Imports Microsoft.Office.Interop.Outlook
Imports Microsoft.Exchange.WebServices.Data
Imports Microsoft.Exchange.WebServices.Autodiscover
Imports Tesseract
Imports TikaOnDotNet.TextExtraction
Imports System.Data.SqlClient
Imports Newtonsoft.Json
'Imports PdfSharp.Pdf.IO

Namespace Controllers
    Public Class AccettazioneUCController
        Inherits System.Web.Mvc.Controller
        Private Const ConnectionString As String = "Persist Security Info=True;Password=ALNUSAD;User ID=ALNUSAD;Initial Catalog=ALNEUMA;Data Source=192.168.100.50"
        Private myConn As SqlConnection
        Private myCmd As SqlCommand
        Private myReader As SqlDataReader
        Private results As String

        Private db As New EuromaModels
        Private appctx As New ApplicationDbContext

        ' GET: AccettazioneUC
        <Authorize>
        Function Index(ByVal id As Integer?) As ActionResult
            Dim Chiave = ""
            Dim metrica = 0
            Select Case id
                Case 1
                    Return View(db.AccettazioneUC.Where(Function(x) x.EmailInviata = False And x.Accettato = Stato_UC.In_attesa).ToList())
                Case 2
                    Return View(db.AccettazioneUC.Where(Function(x) x.EmailInviata = False And x.Accettato = Stato_UC.Accettato).ToList())
                Case 3
                    Return View(db.AccettazioneUC.Where(Function(x) x.EmailInviata = False And x.Accettato = Stato_UC.Non_Accettato).ToList())
                Case 4
                    Return View(db.AccettazioneUC.ToList())
            End Select

            Return HttpNotFound()
        End Function
        Function CaricoUT() As ActionResult
            Dim carico = db.ProgettiUT.Where(Function(w) w.StatoProgetto < Stato_UT.Completato).GroupBy(Function(p) p.Operatore).Select(Function(p) New With {.Nome = p.Key, .Number = p.Count}).ToList
            ViewBag.CaricoOrdiniUT = JsonConvert.SerializeObject(carico)
            Return PartialView()
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
                Dim data As IQueryable(Of AccettazioneUC)
                If Not IsNothing(PostedData.columns(1).search.value) Then
                    Dim Stato_Accettazione = 0
                    Select Case PostedData.columns(1).search.value
                        Case "In attesa"
                            Stato_Accettazione = 0

                        Case "Accettato"
                            Stato_Accettazione = 1

                        Case "Non accettato"
                            Stato_Accettazione = 2

                        Case "Inviato"
                            Stato_Accettazione = 3

                        Case "Inviato a UT"
                            Stato_Accettazione = 4

                        Case "Inviato a Prod"
                            Stato_Accettazione = 5

                        Case "Ritorno da UT"
                            Stato_Accettazione = 7

                        Case Else
                            Stato_Accettazione = 0
                    End Select

                    data = db.AccettazioneUC.Where(Function(y) y.Accettato = Stato_Accettazione).OrderBy(Function(x) x.DataCreazione)
                Else
                    data = db.AccettazioneUC.OrderBy(Function(x) x.DataCreazione)
                End If


                'ricerca
                Try
                    If Not IsNothing(PostedData.search.value) Then
                        If Not PostedData.search.value.Contains(" ") Then 'singola parola
                            Dim search As String = PostedData.search.value
                            Dim w As Expressions.Expression(Of Func(Of AccettazioneUC, Boolean)) = MakeWhereExpression(search)
                            w.Compile()
                            data = data.Where(w)
                        Else 'multiple
                            For Each term As String In PostedData.search.value.Split(" ")
                                Dim wpartial As Expressions.Expression(Of Func(Of AccettazioneUC, Boolean)) = MakeWhereExpression(term)
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

                Try
                    'If Not IsNothing(PostedData.columns(1).search.value) Then
                    '    Dim Stato_Accettazione = 0
                    '    Select Case PostedData.columns(1).search.value
                    '        Case "<Span Class='badge bg-warning text-dark'><i class='fa-solid fa-clock'></i>In attesa</span>"
                    '            Stato_Accettazione = 0

                    '        Case "<Span Class='badge bg-primary'><i class='fa-solid fa-check-double'></i>Accettato</span>"
                    '            Stato_Accettazione = 1

                    '        Case "<Span Class='badge bg-danger'><i class='fa-solid fa-circle-exclamation'></i>Non Accettato</span>"
                    '            Stato_Accettazione = 2

                    '        Case "<Span Class='badge bg-success'><i class='fa-solid fa-envelope-circle-check'></i>Inviato</span>"
                    '            Stato_Accettazione = 3

                    '    End Select
                    '    Dim search As String = PostedData.columns(1).search.value
                    '    Dim w As Expressions.Expression(Of Func(Of AccettazioneUC, Boolean)) = MakeWhereExpression(search)
                    '    w.Compile()
                    '    data = data.Where(w)
                    'End If
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
                'ordinamento
                Try
                    If PostedData.order.Count = 0 Then
                        Dim o As Expressions.Expression(Of Func(Of AccettazioneUC, String))
                        o = MakeOrderExpression(Nothing) 'default
                        o.Compile()
                        data = data.OrderBy(o)
                        'data = data.OrderBy(CreateExpression(Of Amministratore)("Studio"))
                        'data = OrderByDynamic(data, "Studio", False)
                    ElseIf PostedData.order.Count = 1 Then 'singolo
                        Dim o As Expressions.Expression(Of Func(Of AccettazioneUC, String)) = MakeOrderExpression(PostedData.order(0).column)
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
                        Dim o As Expressions.Expression(Of Func(Of AccettazioneUC, String)) = MakeOrderExpression(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of AccettazioneUC, String)) = MakeOrderExpression(PostedData.order(1).column)
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
                        Dim o As Expressions.Expression(Of Func(Of AccettazioneUC, String)) = MakeOrderExpression(PostedData.order(0).column)
                        o.Compile()

                        Dim o2 As Expressions.Expression(Of Func(Of AccettazioneUC, String)) = MakeOrderExpression(PostedData.order(1).column)
                        o2.Compile()

                        Dim o3 As Expressions.Expression(Of Func(Of AccettazioneUC, String)) = MakeOrderExpression(PostedData.order(2).column)
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
                    'Messo in pausa logging ordinamento causa errore 
                    'db.Log.Add(New Log With {
                    ' .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                    ' .Livello = TipoLogLivello.Errors,
                    ' .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                    ' .Messaggio = "Errore Ordinamento -> " & ex.Message,
                    ' .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                    ' })
                    'db.SaveChanges()
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
                For Each Acc As AccettazioneUC In data
                    Try
                        Dim finalButtons = ""
                        Dim Stato_Accettazione = ""
                        Select Case Acc.Accettato
                            Case 0
                                Stato_Accettazione = "<Span Class='badge bg-warning text-dark'><i class='fa-solid fa-clock'></i>In attesa</span>"

                            Case 1
                                Stato_Accettazione = "<Span Class='badge bg-primary'><i class='fa-solid fa-check-double'></i>Accettato</span>"

                            Case 2
                                Stato_Accettazione = "<Span Class='badge bg-danger'><i class='fa-solid fa-circle-exclamation'></i>Non Accettato</span>"

                            Case 3
                                Stato_Accettazione = "<Span Class='badge bg-success'><i class='fa-solid fa-envelope-circle-check'></i>Inviato</span>"

                            Case 4
                                Stato_Accettazione = "<Span Class='badge bg-success'><i class='fa-solid fa-envelope-circle-check'></i>Inviato a UT</span>"

                            Case 5
                                Stato_Accettazione = "<Span Class='badge bg-success'><i class='fa-solid fa-envelope-circle-check'></i>Inviato a PROD</span>"

                            Case 7
                                Stato_Accettazione = "<Span Class='badge bg-warning text-dark'><i class='fa-solid fa-clock'></i>Ritorno da UT</span>"

                        End Select
                        Dim countNote = db.NotePerOC.Where(Function(x) x.OC = Acc.OC).Count
                        Dim countFile = db.DocumentiPerOC.Where(Function(x) x.OC = Acc.OC).Count
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
                                .File = Acc.File,
                                .OperatoreInsert = Acc.OperatoreInsert,
                                .OperatoreAccettazione = Acc.OperatoreAccettazione,
                                .OC = "<a style='text-decoration:none!important; margin-right:8px;'href='/Overviews/Ordine/" + Acc.OC.ToString + "' Target='_blank'>" + Acc.OC + "</a>" + notificheFile + notificheNote,
                                .Note = Acc.Note,
                                .EmailOperatoreInsert = Acc.EmailOperatoreInsert,
                                .Accettato = Stato_Accettazione,
                                .Cartella = Acc.Cartella,
                                .DataAccettazione = Acc.DataAccettazione.ToString,
                                .DataCreazione = Acc.DataCreazione.ToString,
                                .EmailInviata = Acc.EmailInviata
                           })


                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                             .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                             .Livello = TipoLogLivello.Errors,
                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                             .Messaggio = "Errore Creazione Lista Impianto (" & Acc.Id & ") -> " & ex.Message & " [" & ex.InnerException.Message & "]",
                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                        })
                        db.SaveChanges()
                    End Try
                Next

                Return Json(New With {PostedData.draw, .recordsTotal = db.AccettazioneUC.Count, .recordsFiltered = filtered, .data = result})
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
            Return Json(New With {PostedData.draw, .recordsTotalUC = db.AccettazioneUC.Count, .recordsFiltered = 0})
        End Function



        ' GET: AccettazioneUC/Details/5
        <HttpPost()>
        <Authorize>
        Function Details(ByVal id As Integer?) As JsonResult
            If IsNothing(id) Then
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Accettazione -> " & id & ". Impossibile recuperare i dati."})
            End If
            Dim accettazioneUC As AccettazioneUC = db.AccettazioneUC.Find(id)
            Dim conteggioInUT = db.ProgettiUT.Where(Function(x) x.OC_Riferimento = accettazioneUC.OC).Count
            Dim conteggioInPROD = db.ProgettiProd.Where(Function(x) x.OC_Riferimento = accettazioneUC.OC).Count
            Dim listArticoli = db.ArticoliPerOC.Where(Function(x) x.OC = accettazioneUC.OC).ToList
            Dim listNote = db.NotePerOC.Where(Function(x) x.OC = accettazioneUC.OC).ToList
            Dim listDocumenti = db.DocumentiPerOC.Where(Function(x) x.OC = accettazioneUC.OC).ToList
            Dim fAccettazioneUC As New AccettazioneUCViewModel With {
                .Accettato = accettazioneUC.Accettato,
                .Cartella = accettazioneUC.Cartella,
                .DataAccettazione = accettazioneUC.DataAccettazione,
                .DataCreazione = accettazioneUC.DataCreazione,
                .EmailInviata = accettazioneUC.EmailInviata,
                .EmailOperatoreInsert = accettazioneUC.EmailOperatoreInsert,
                .File = accettazioneUC.File,
                .Id = accettazioneUC.Id,
                .Note = accettazioneUC.Note,
                .OC = accettazioneUC.OC,
                .OperatoreAccettazione = accettazioneUC.OperatoreAccettazione,
                .OperatoreInsert = accettazioneUC.OperatoreInsert,
                .SenttoUC = IIf(conteggioInPROD + conteggioInUT > 0 And Not accettazioneUC.Accettato = Stato_UC.Ritorno_da_UT, True, False),
                .ListaArt = listArticoli,
                .ListOfNote = listNote,
                .ListOfDocumenti = listDocumenti,
                .DataPrevistaConsegna = accettazioneUC.DataRichiestaConsegna,
                .Priorita = accettazioneUC.Priorita,
                .Brand = accettazioneUC.Brand,
                .PrezzoMaggiorato = accettazioneUC.CostoMaggiorato
            }
            If IsNothing(accettazioneUC) Then
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Accettazione -> " & id & ". Impossibile recuperare i dati."})
            End If
            Return Json(New With {.ok = True, .message = PartialToString("Details", fAccettazioneUC)})
        End Function
        ' GET: AccettazioneUC/Details/5
        <HttpPost()>
        <Authorize>
        Function AddNota(Id As String, Nota As String) As JsonResult '
            If IsNothing(Id) Then
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Accettazione -> " & Id & ". Impossibile recuperare l'OC."})
            End If
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim id_Nota = ""
                If Id.Contains("OP") Then
                    id_Nota = Id
                Else
                    Dim accettazioneUC As AccettazioneUC = db.AccettazioneUC.Where(Function(x) x.OC = Id).First
                    id_Nota = accettazioneUC.OC
                End If
                db.NotePerOC.Add(New NotePerOC With {
                    .OC = id_Nota,
                    .Data_Nota = DateTime.Now,
                    .Contenuto_Nota = Nota,
                    .Operatore_Id = OpID,
                    .Operatore_Nome = OpName
                })
                db.SaveChanges()
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Aggiunta nota",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = Id, .OC = id_Nota, .nota = Nota}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "Nota correttamente aggiunta -> " & Id & ".", .id = Id}, JsonRequestBehavior.AllowGet)
            Catch ex As SystemException
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore aggiunta nota -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = Id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Aggiunta note -> " & Id & ";" & ex.Message})
            End Try


        End Function
        ' GET: AccettazioneUC/Details/5
        <HttpPost()>
        <Authorize>
        Function AddFile(id As String, File As HttpPostedFileBase) As JsonResult '
            If IsNothing(id) Then
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Accettazione -> " & id & ". Impossibile recuperare l'OC."})
            End If
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim id_file = ""
                If id.Contains("OP") Then
                    id_file = id
                Else
                    Dim accettazioneUC As AccettazioneUC = db.AccettazioneUC.Where(Function(x) x.OC = id).First
                    id_file = accettazioneUC.OC
                End If
                Try
                    If Not IsNothing(File) Then
                        If id_file.Contains("OP") Then
                            Dim UploadedFile As HttpPostedFileBase = File
                            If UploadedFile IsNot Nothing AndAlso UploadedFile.ContentLength > 0 Then
                                Dim pathTMP = Path.Combine(Server.MapPath("~/Content/upload_esterno"), DateTime.Now.ToString.Split(" ")(0).Replace("/", "-") + "_" + id_file + "_" + UploadedFile.FileName.ToString.Replace(" ", String.Empty))
                                UploadedFile.SaveAs(pathTMP)
                                db.DocumentiPerOC.Add(New DocumentiPerOC With {
                                    .DataCreazioneFile = DateTime.Now,
                                    .Nome_File = UploadedFile.FileName,
                                    .OC = id_file,
                                    .Operatore_Id = OpID,
                                    .Operatore_Nome = OpName,
                                    .Percorso_File = pathTMP
                                })
                                db.SaveChanges()
                                If User.IsInRole("ProgrammazioneEsterno") Then
                                    Dim odp = db.OrdiniDiProduzione.Where(Function(x) x.OP = id_file).First
                                    odp.Accettato = Stato_Ordine_Di_Produzione_Esterno.In_Lavorazione
                                    db.SaveChanges()
                                End If
                            End If
                        Else
                            Dim UploadedFile As HttpPostedFileBase = File
                            If UploadedFile IsNot Nothing AndAlso UploadedFile.ContentLength > 0 Then
                                Dim pathTMP = Path.Combine(Server.MapPath("~/Content/upload_utenti"), UploadedFile.FileName.ToString.Replace(" ", String.Empty))
                                UploadedFile.SaveAs(pathTMP)
                                db.DocumentiPerOC.Add(New DocumentiPerOC With {
                                    .DataCreazioneFile = DateTime.Now,
                                    .Nome_File = UploadedFile.FileName,
                                    .OC = id_file,
                                    .Operatore_Id = OpID,
                                    .Operatore_Nome = OpName,
                                    .Percorso_File = pathTMP
                                })
                                db.SaveChanges()
                            End If
                        End If

                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                    .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                    .Livello = TipoLogLivello.Errors,
                    .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                    .Messaggio = "Errore lettura file -> " & ex.Message,
                    .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                    })
                    db.SaveChanges()
                    Return Json(New With {.ok = False, .message = "Errore: lettura file -> " & id & ";" & ex.Message})
                End Try
                db.SaveChanges()
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Aggiunto File",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id, .OC = id_file, .file = id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "File correttamente aggiunto -> " & id & ".", .id = id}, JsonRequestBehavior.AllowGet)
            Catch ex As SystemException
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore aggiunta file -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: Dettagli Aggiunta file -> " & id & ";" & ex.Message})
            End Try


        End Function
        ' GET: AccettazioneUC/Create
        <Authorize>
        Function Create() As ActionResult
            Dim OTList = db.AccettazioneUC.Where(Function(x) x.OC.Contains("OT")).ToList
            Dim optionsOtList = ""
            For Each OT In OTList
                optionsOtList = optionsOtList + "<option>" + OT.OC + "</option>"
            Next
            ViewBag.ListaOT = "<datalist id='OTList'>" + optionsOtList + "</datalist>"
            Return PartialView()
        End Function

        ' POST: AccettazioneUC/Create
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <Authorize>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(ByVal file As HttpPostedFileBase, ByVal checkCosto As Boolean, ByVal ListOT As String) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim pathTMP = Path.Combine(Server.MapPath("~/Content/upload_UC"), file.FileName.ToString.Replace(" ", String.Empty))
            Dim pathTMPOldREV = Path.Combine(Server.MapPath("~/Content/upload_UC"), DateTime.Now.Ticks.ToString + "_OLD_REV_" + file.FileName.ToString.Replace(" ", String.Empty))
            Dim em = ""
            Dim text = ""
            Dim filename()
            Dim OC
            Dim priorita = 0
            Dim data
            Dim brand = ""
            Dim typeOfPagamento = ""
            Dim cliente = ""
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                If IO.File.Exists(pathTMP) Then
                    IO.File.Copy(pathTMP, pathTMPOldREV)
                    IO.File.Delete(pathTMP)
                    Dim f = db.AccettazioneUC.Where(Function(x) x.File = pathTMP).FirstOrDefault
                    file.SaveAs(pathTMP)
                    Try
                        db.DocumentiPerOC.Add(New DocumentiPerOC With {
                        .OC = f.OC,
                        .DataCreazioneFile = DateTime.Now,
                        .Nome_File = f.OC + "_REV.pdf",
                        .Operatore_Id = OpID,
                        .Operatore_Nome = OpName,
                        .Percorso_File = pathTMPOldREV
                    })
                        db.SaveChanges()
                        db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Aggiunta nuova revisione all'OC",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = f.Id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                        db.StoricoOC.Add(New StoricoOC With {
                        .Descrizione = "Aggiunto nuova rev all'OC",
                        .OC = OC,
                        .Titolo = "Aggiunta nuova rev all'OC",
                        .Ufficio = TipoUfficio.UfficioCommerciale,
                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                    })
                        db.SaveChanges()
                        text = New TextExtractor().Extract(pathTMP).Text
                        Dim tempString = text.Split(ControlChars.CrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        'For Each line In tempString.Where(Function(x) x.Contains("Nr "))
                        For Each line In tempString
                            If line.Contains("Nr ") Then
                                Dim subLine = line.Split({" "c}, 2)
                                Dim codeArticolo = subLine(0)
                                Dim descrizione = subLine(1).Split("Nr")(0)
                                Dim filenameTMP = file.FileName.ToString.Split("_")
                                Dim OCName = filenameTMP(2).ToString + "-" + filenameTMP(3).ToString + "-" + filenameTMP(4).ToString.Split(".")(0)
                                Dim conteggio = db.ArticoliPerOC.Where(Function(x) x.Cod_Art = codeArticolo And x.Descrizione = descrizione And x.OC = OCName).Count
                                If Not conteggio > 0 Then
                                    myConn = New SqlConnection(ConnectionString)
                                    myCmd = myConn.CreateCommand
                                    myCmd.CommandText = "select count(*) from DIBDCO00 where ARTCOD = '" + codeArticolo + "'"
                                    myConn.Open()
                                    Try
                                        myReader = myCmd.ExecuteReader
                                        Dim countDB = 0
                                        Do While myReader.Read()
                                            countDB = myReader.GetInt32(0)
                                        Loop
                                        myConn.Close()
                                        db.ArticoliPerOC.Add(New ArticoliPerOC With {
                                           .Cod_Art = codeArticolo,
                                           .Descrizione = descrizione,
                                           .DistintaBase = IIf(countDB > 0, 1, 0),
                                           .OC = OCName
                                       })
                                        db.SaveChanges()
                                    Catch ex As SystemException

                                    End Try

                                End If
                            End If
                        Next
                        'Ricerca priorita e data consegna prevista
                        If text.ToUpper.Contains("REVISIONE") Then
                            f.IsRevisione = True
                        Else
                            f.IsRevisione = False
                        End If
                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                          .Livello = TipoLogLivello.Warning,
                          .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                          .Messaggio = "Errore inserimento aggiornamento: " & vbNewLine & ex.Message,
                          .Dati = "",
                          .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}})
                        db.SaveChanges()

                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message})
                    End Try
                    f.EmailInviata = 0
                    db.SaveChanges()
                    db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Aggiornata accettazione",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = f.Id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                    db.SaveChanges()
                    Return Json(New With {.ok = False, .message = "Aggiornamento accettazione."})
                ElseIf Not IsNothing(ListOT) And ListOT <> "" Then
                    If db.AccettazioneUC.Where(Function(x) x.OC = ListOT).Count < 0 Then
                        Return Json(New With {.ok = False, .message = "Impossibile trovare l'OT richiesta."})
                    End If
                    file.SaveAs(pathTMP)
                    Dim oldOT = db.AccettazioneUC.Where(Function(x) x.OC = ListOT).First
                    Dim oldOT_Prog = db.ProgettiUT.Where(Function(x) x.OC_Riferimento = ListOT).First
                    filename = file.FileName.ToString.Split("_")
                    OC = filename(2).ToString + "-" + filename(3).ToString + "-" + filename(4).ToString.Split(".")(0)
                    db.DocumentiPerOC.Add(New DocumentiPerOC With {
                        .OC = OC,
                        .DataCreazioneFile = DateTime.Now,
                        .Nome_File = oldOT.OC + ".pdf",
                        .Operatore_Id = OpID,
                        .Operatore_Nome = OpName,
                        .Percorso_File = oldOT.File
                    })
                    db.SaveChanges()
                    db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Convertita OT in OC - da" + oldOT.OC + " a " + OC,
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = oldOT.Id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                    db.StoricoOC.Add(New StoricoOC With {
                        .Descrizione = "Convertita correttamente OT a OC",
                        .OC = OC,
                        .Titolo = "Conversione OT/OC",
                        .Ufficio = TipoUfficio.UfficioCommerciale,
                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                    })
                    db.SaveChanges()
                    For Each OT In db.StoricoOC.Where(Function(x) x.OC = oldOT.OC)
                        OT.OC = OC
                    Next
                    db.SaveChanges()
                    For Each OT In db.DocumentiPerOC.Where(Function(x) x.OC = oldOT.OC)
                        OT.OC = OC
                    Next
                    db.SaveChanges()
                    For Each OT In db.NotePerOC.Where(Function(x) x.OC = oldOT.OC)
                        OT.OC = OC
                    Next
                    db.SaveChanges()
                    For Each OT In db.ArticoliPerOC.Where(Function(x) x.OC = oldOT.OC)
                        OT.OC = OC
                    Next
                    db.SaveChanges()
                    oldOT.OC = OC
                    oldOT_Prog.OC_Riferimento = OC
                    db.SaveChanges()
                    Return Json(New With {.ok = True, .message = "OT convertita correttamente."})
                Else
                    file.SaveAs(pathTMP)
                    Try
                        text = New TextExtractor().Extract(pathTMP).Text

                    Catch ex As SystemException

                    End Try
                End If
            Catch ex As SystemException
                db.Log.Add(New Log With {
                                 .Livello = TipoLogLivello.Warning,
                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                 .Messaggio = "Errore inserimento aggiornamento: " & vbNewLine & ex.Message,
                                 .Dati = "",
                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}})
                db.SaveChanges()

                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message})
            End Try

            Try
                filename = file.FileName.ToString.Split("_")
                OC = filename(2).ToString + "-" + filename(3).ToString + "-" + filename(4).ToString.Split(".")(0)
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "Select ORCCRP,ORCTSSREV,PAGCOD,DVSCOD, CLFNMG from ORCTES00,CLFANA WHERE ESECOD = '" + filename(2).ToString + "' AND ORCTSZ = '" + filename(3).ToString + "' AND ORCTNR = '" + filename(4).ToString.Split(".")(0) + "' AND ORCTES00.ORCCLI = CLFANA.CLFCO1 AND CLFANA.CLFTIP = 'C'"
                myConn.Open()
                Try
                    myReader = myCmd.ExecuteReader
                    Do While myReader.Read()
                        priorita = myReader.GetDecimal(0)
                        Data = myReader.GetDecimal(1)
                        typeOfPagamento = myReader.GetString(2)
                        Select Case myReader.GetString(3)
                            Case "01"
                                brand = "Drillmatic"
                            Case "02"
                                brand = "CMT"
                            Case "03"
                                brand = "ISA"
                            Case "04"
                                brand = "Unistand"
                            Case "05"
                                brand = "MPA"
                            Case "06"
                                brand = "Euroma"
                        End Select
                        cliente = myReader.GetString(4)
                    Loop
                    myConn.Close()

                    db.SaveChanges()
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                                 .Livello = TipoLogLivello.Warning,
                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                 .Messaggio = "Errore ricerca priorita OC: " & vbNewLine & ex.Message,
                                 .Dati = "",
                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}})
                    db.SaveChanges()
                End Try
            Catch ex As SystemException
                db.Log.Add(New Log With {
                                 .Livello = TipoLogLivello.Warning,
                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                 .Messaggio = "Errore inserimento OC: " & vbNewLine & ex.Message,
                                 .Dati = "",
                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}})
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: Nome errato, OffertaCliente_Nr_XXXX_XX_XX."})

            End Try
            If ModelState.IsValid Then
                Try
                    If typeOfPagamento.Contains("SP") Then
                        Dim mySmtp As New SmtpClient
                        Dim myMail As New MailMessage()
                        mySmtp.UseDefaultCredentials = False
                        mySmtp.Credentials = New System.Net.NetworkCredential("no-reply@euromagroup.com", "yp@4d%p2AFa;")
                        mySmtp.Host = "oberon.dnshigh.com"
                        myMail = New MailMessage()
                        myMail.From = New MailAddress("no-reply@euromagroup.com")
                        myMail.Attachments.Add(New System.Net.Mail.Attachment(pathTMP))
                        myMail.To.Add("amministrazione@euromagroup.com")
                        myMail.Subject = "Acconto per " + OC
                        Dim StrContent = ""
                        Using reader = New StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Views/Shared/Email_Euroma_Amm.vbhtml")
                            Dim readFile As String = reader.ReadToEnd()
                            StrContent = readFile
                            StrContent = StrContent.Replace("[Username]", "Claudia")
                            StrContent = StrContent.Replace("[Motivo]", "E' stato inserito a sistema un nuovo ordine con acconto.")
                        End Using
                        myMail.Body = StrContent.ToString
                        myMail.IsBodyHtml = True
                        mySmtp.Send(myMail)
                        db.Audit.Add(New Audit With {
                                               .Livello = TipoAuditLivello.Info,
                                               .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                               .Messaggio = "Invio mail Amministrazione causa anticipo",
                                               .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.OC = OC}),
                                              .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                 })
                        db.SaveChanges()
                    End If
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                                .Livello = TipoLogLivello.Warning,
                                .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                .Messaggio = "Errore invio anticipo amministrazione: " & vbNewLine & ex.Message,
                                .Dati = "",
                                .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}})
                    db.SaveChanges()
                End Try
                Try
                    Dim rev = False
                    If text.ToUpper.Contains("REVISIONE") Then
                        rev = True
                    Else
                        rev = False
                    End If
                    Try
                        text = New TextExtractor().Extract(pathTMP).Text
                        Dim tempString = text.Split(ControlChars.CrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        'For Each line In tempString.Where(Function(x) x.Contains("Nr "))
                        For Each line In tempString
                            If line.Contains("Nr ") Then
                                Dim subLine = line.Split({" "c}, 2)
                                Dim codeArticolo = subLine(0)
                                Dim descrizione = subLine(1).Split("Nr")(0)
                                Dim filenameTMP = file.FileName.ToString.Split("_")
                                Dim OCName = filenameTMP(2).ToString + "-" + filenameTMP(3).ToString + "-" + filenameTMP(4).ToString.Split(".")(0)
                                Dim conteggio = db.ArticoliPerOC.Where(Function(x) x.Cod_Art = codeArticolo And x.Descrizione = descrizione And x.OC = OCName).Count
                                If Not conteggio > 0 Then
                                    myConn = New SqlConnection(ConnectionString)
                                    myCmd = myConn.CreateCommand
                                    myCmd.CommandText = "select count(*) from DIBDCO00 where ARTCOD = '" + codeArticolo + "'"
                                    myConn.Open()
                                    Try
                                        myReader = myCmd.ExecuteReader
                                        Dim countDB = 0
                                        Do While myReader.Read()
                                            countDB = myReader.GetInt32(0)
                                        Loop
                                        myConn.Close()
                                        db.ArticoliPerOC.Add(New ArticoliPerOC With {
                                           .Cod_Art = codeArticolo,
                                           .Descrizione = descrizione,
                                           .DistintaBase = IIf(countDB > 0, 1, 0),
                                           .OC = OCName
                                       })
                                        db.SaveChanges()
                                    Catch ex As SystemException
                                        db.Log.Add(New Log With {
                                         .Livello = TipoLogLivello.Warning,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Errore lettura file: " & vbNewLine & ex.Message,
                                         .Dati = "",
                                         .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}})
                                        db.SaveChanges()
                                    End Try

                                End If
                            End If
                        Next
                        'Ricerca priorita e data consegna prevista

                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                          .Livello = TipoLogLivello.Warning,
                          .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                          .Messaggio = "Errore inserimento aggiornamento: " & vbNewLine & ex.Message,
                          .Dati = "",
                          .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}})
                        db.SaveChanges()

                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message})
                    End Try
                    db.AccettazioneUC.Add(New AccettazioneUC With {
                         .EmailOperatoreInsert = em,
                         .OperatoreInsert = OpName,
                         .DataCreazione = DateTime.Now,
                         .OC = OC,
                         .File = pathTMP,
                         .Cartella = "",
                         .Accettato = 0,
                         .EmailInviata = False,
                         .IsRevisione = rev,
                         .Priorita = priorita,
                         .DataRichiestaConsegna = Convert.ToDateTime(data.ToString.Insert(6, "/").Insert(4, "/")),
                         .Brand = brand,
                         .CostoMaggiorato = checkCosto,
                         .Cliente = cliente
                })
                    db.SaveChanges()
                    db.StoricoOC.Add(New StoricoOC With {
                        .OC = OC,
                        .Descrizione = "Creata sul sistema nuova OC",
                        .Titolo = "Creazione OC",
                        .Ufficio = TipoUfficio.UfficioCommerciale,
                        .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                    })
                    db.SaveChanges()
                    db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Creata Accettazione",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.OC = OC}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                    db.SaveChanges()
                    Dim mySmtpT As New SmtpClient
                    Dim myMailT As New MailMessage()
                    mySmtpT.UseDefaultCredentials = False
                    mySmtpT.Credentials = New System.Net.NetworkCredential("no-reply@euromagroup.com", "yp@4d%p2AFa;")
                    mySmtpT.Host = "oberon.dnshigh.com"
                    myMailT = New MailMessage()
                    myMailT.From = New MailAddress("no-reply@euromagroup.com")
                    myMailT.Attachments.Add(New System.Net.Mail.Attachment(pathTMP))
                    'myMailT.To.Add("m.zucchini@euromagroup.com")
                    myMailT.To.Add("t.marchioni@euromagroup.com")
                    myMailT.To.Add("s.botti@euromagroup.com")
                    myMailT.To.Add("s.carboni@euromagroup.com")
                    myMailT.Subject = "Inserito " + OC + " - " + cliente
                    Dim StrContentT = ""
                    Using reader = New StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Views/Shared/Email_Euroma_Amm.vbhtml")
                        Dim readFile As String = reader.ReadToEnd()
                        StrContentT = readFile
                        StrContentT = StrContentT.Replace("[Username]", "")
                        StrContentT = StrContentT.Replace("[Motivo]", "E' stato inserita una nuova conferma d'ordine.")
                    End Using
                    myMailT.Body = StrContentT.ToString
                    myMailT.IsBodyHtml = True
                    mySmtpT.Send(myMailT)
                    db.Audit.Add(New Audit With {
                                               .Livello = TipoAuditLivello.Info,
                                               .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                               .Messaggio = "Invio mail Tiziano, Sebastiano e Stefano per conoscenza",
                                               .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.OC = OC}),
                                              .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                 })
                    db.SaveChanges()
                    Dim role = appctx.Roles.SingleOrDefault(Function(m) m.Name = "Commerciale_Admin")
                    Dim usersInRole = appctx.Users.Where(Function(m) m.Roles.Any(Function(r) r.RoleId = role.Id))

                    If db.AccettazioneUC.Where(Function(x) x.Accettato = Stato_UC.In_attesa).Count > 5 And usersInRole.Where(Function(x) x.Profile.NotificheViaMail = True).Count > 0 Then
                        Try
                            Dim mySmtp As New SmtpClient
                            Dim myMail As New MailMessage()
                            mySmtp.UseDefaultCredentials = False
                            mySmtp.Credentials = New System.Net.NetworkCredential("no-reply@euromagroup.com", "yp@4d%p2AFa;")
                            mySmtp.Host = "oberon.dnshigh.com"
                            myMail = New MailMessage()
                            myMail.From = New MailAddress("no-reply@euromagroup.com")
                            myMail.Attachments.Add(New System.Net.Mail.Attachment(pathTMP))
                            For Each a In usersInRole.Where(Function(x) x.Profile.NotificheViaMail = True)
                                If a.Profile.NotificheViaMail Then
                                    myMail.To.Add(a.Email)
                                End If
                            Next
                            myMail.Subject = "Inseriti nuovi file da accettare"
                            Dim StrContent = ""
                            Using reader = New StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Views/Shared/Email_Euroma.vbhtml")
                                Dim readFile As String = reader.ReadToEnd()
                                StrContent = readFile
                                StrContent = StrContent.Replace("[Username]", "Amministratori")
                                StrContent = StrContent.Replace("[Motivo]", "Sono stati inseriti nuovi file da accettare all'interno del portale Web Euroma.")
                            End Using
                            myMail.Body = StrContent.ToString
                            myMail.IsBodyHtml = True
                            mySmtp.Send(myMail)
                            db.Audit.Add(New Audit With {
                                                            .Livello = TipoLogLivello.Info,
                                                            .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                            .Messaggio = "Mail inviata",
                                                            .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Subject = myMail.Subject, .body = myMail.Body, .cc = myMail.CC, .to = myMail.To}),
                                                           .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                              })


                        Catch ex As SystemException
                            db.Log.Add(New Log With {
                                 .Livello = TipoLogLivello.Warning,
                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                 .Messaggio = "Errore inserimento OC: " & vbNewLine & ex.Message,
                                 .Dati = "",
                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}})
                            db.SaveChanges()
                        End Try

                    End If
                    Return Json(New With {.ok = True, .message = "Richiesta Inviata correttamente!"})
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                                 .Livello = TipoLogLivello.Warning,
                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                 .Messaggio = "Errore inserimento OC: " & vbNewLine & ex.Message,
                                 .Dati = "",
                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}})
                    db.SaveChanges()
                    Return Json(New With {.ok = False, .message = "Errore: " + ex.Message})

                End Try

            End If
            Return Json(New With {.ok = False, .message = "Errore: Impossibile aggiungere il ticket!"})
        End Function
        Private Function MakeWhereExpression(Search As String) As Expressions.Expression(Of Func(Of AccettazioneUC, Boolean))
            Return Function(x) x.Cartella.Contains(Search) Or
                               x.EmailOperatoreInsert.Contains(Search) Or
                               x.Note.Contains(Search) Or
                               x.OC.Contains(Search) Or
                               x.OperatoreAccettazione.Contains(Search) Or
                               x.OperatoreInsert.Contains(Search) Or
                               x.File.Contains(Search)

        End Function
        Private Function MakeOrderExpression(Column As Integer) As Expressions.Expression(Of Func(Of AccettazioneUC, String))
            Select Case Column
                Case Nothing : Return Function(x) x.OC
                Case 1 : Return Function(x) x.Accettato
                Case 2 : Return Function(x) x.OC
                Case 4 : Return Function(x) x.Cartella
                Case 5 : Return Function(x) x.OperatoreInsert
                Case 6 : Return Function(x) x.DataCreazione
                Case Else : Return Function(x) x.OC
            End Select
        End Function
        ' GET: AccettazioneUC/Edit/5
        <Authorize>
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim accettazioneUC As AccettazioneUC = db.AccettazioneUC.Find(id)
            If IsNothing(accettazioneUC) Then
                Return HttpNotFound()
            End If
            Return PartialView(accettazioneUC)
        End Function
        <Authorize>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function CreaEmail(<Bind(Include:="Id,Privacy_Percorso,File_Percorso,Mittente,Destinatario,Oggetto,Corpo,CC")> ByVal email As EmailViewModel, file As HttpPostedFileBase) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim fileSalvati As New List(Of String)
            If ModelState.IsValid Then
                For i = 0 To Request.Files.Count - 1
                    Try
                        Dim UploadedFile As HttpPostedFileBase = Request.Files(i)
                        If UploadedFile IsNot Nothing AndAlso UploadedFile.ContentLength > 0 Then
                            Dim pathTMP = Path.Combine(Server.MapPath("~/Content/upload_UC"), UploadedFile.FileName.ToString.Replace(" ", String.Empty))

                            If System.IO.File.Exists(pathTMP) Then
                                fileSalvati.Add(pathTMP)
                            Else
                                UploadedFile.SaveAs(pathTMP)
                                fileSalvati.Add(pathTMP)
                            End If

                        End If
                    Catch ex As SystemException
                        Return Json(New With {.ok = False, .message = "Errore nell'importazione dei file aggiuntivi."})
                    End Try


                Next
                Try
                    OpID = User.Identity.GetUserId()
                    OpName = User.Identity.GetUserName()
                    Dim OC = db.AccettazioneUC.Where(Function(x) x.Id = email.Id).First
                    Dim u = appctx.Users.Where(Function(x) x.UserName = OpName.ToString).First 'db.UserEmail.Where(Function(x) x.Uid = OC.OperatoreInsert).First
                    Dim arr_CC()
                    Dim arr_Destinatari()
                    If Not IsNothing(email.Destinatario) Then
                        arr_Destinatari = email.Destinatario.Split("""")
                    Else
                        Return Json(New With {.ok = False, .message = "Nessun destinatario impostato."})
                    End If
                    If Not IsNothing(email.CC) Then
                        arr_CC = email.CC.Split("""")
                    End If

                    If IsNothing(u.Profile) Then
                        Return Json(New With {.ok = False, .message = "Errore: Profilo non configurato."})
                    End If
                    Dim password = Decrypter(u.Profile.PWD_Email)
                    Dim firma = u.Profile.Firma
                    Try

                        email.Corpo = email.Corpo.Replace("&lt;", "<").Replace("&gt;", ">")
                        Dim mySmtp As New SmtpClient
                        Dim myMail As New MailMessage()
                        mySmtp.UseDefaultCredentials = False
                        mySmtp.Credentials = New System.Net.NetworkCredential(email.Mittente, password)
                        mySmtp.Host = "oberon.dnshigh.com"
                        myMail = New MailMessage()
                        myMail.From = New MailAddress(email.Mittente)
                        For Each a In arr_Destinatari
                            If a.Contains("@") Then
                                myMail.To.Add(a)
                            End If
                        Next
                        If Not IsNothing(arr_CC) Then
                            For Each a In arr_CC
                                If a.Contains("@") Then
                                    myMail.CC.Add(a)
                                End If
                            Next
                        End If
                        myMail.Bcc.Add(email.Mittente)
                        myMail.Subject = email.Oggetto
                        myMail.IsBodyHtml = True
                        myMail.Body = "<html> <body>" + email.Corpo + "<br>" + firma + "</body></html>"
                        myMail.Attachments.Add(New System.Net.Mail.Attachment(email.File_Percorso.ToString))
                        myMail.Attachments.Add(New System.Net.Mail.Attachment(email.Privacy_Percorso.ToString))
                        If Not IsNothing(fileSalvati) Then
                            For Each f In fileSalvati
                                myMail.Attachments.Add(New System.Net.Mail.Attachment(f.ToString))
                            Next
                        End If
                        mySmtp.Send(myMail)

                        db.Log.Add(New Log With {
                                                    .Livello = TipoLogLivello.Info,
                                                    .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                    .Messaggio = "Mail inviata",
                                                    .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Subject = myMail.Subject, .body = myMail.Body, .cc = myMail.CC, .to = myMail.To}),
                                                   .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                      })
                        db.SaveChanges()
                        OC.Accettato = Stato_UC.Inviato
                        OC.EmailInviata = True
                        db.SaveChanges()
                        Return Json(New With {.ok = True, .message = "Email correttamente inviata!"}, JsonRequestBehavior.AllowGet)
                    Catch ex As SystemException
                        db.Log.Add(New Log With {
                                                     .Livello = TipoLogLivello.Errors,
                                                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                     .Messaggio = "Errore: " + ex.Message,
                                                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = "errore"}),
                                                    .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                       })
                        db.SaveChanges()
                        Return Json(New With {.ok = False, .message = "Errore: !" + ex.Message})
                    End Try

                Catch ex As SystemException
                    db.Log.Add(New Log With {
                                                     .Livello = TipoLogLivello.Errors,
                                                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                     .Messaggio = "Errore: " + ex.Message,
                                                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = "errore"}),
                                                    .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                       })
                    db.SaveChanges()
                    Return Json(New With {.ok = False, .message = "Errore: !" + ex.Message})
                End Try
            End If


        End Function
        <Authorize>
        Function CreaEmail(ByVal id As Integer) As ActionResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString

            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC = db.AccettazioneUC.Where(Function(x) x.Id = id).First
                Dim emaMittente = appctx.Users.Where(Function(x) x.UserName = OC.OperatoreInsert).First.Email 'db.UserEmail.Where(Function(x) x.Uid = OC.OperatoreInsert).First
                Dim filename = ""
                If OC.OC.Contains("OC") Then
                    filename = OC.File.Split("\").Last

                ElseIf OC.OC.Contains("PR") Then
                    filename = OC.File.Split("\").Last
                Else
                    filename = OC.File.Split("\").Last
                End If
                Dim pathCondizioni = Server.MapPath("~\Content\File_Base_UC\Condizioni_Generali_Fornitura_ITL.pdf")
                If OC.OperatoreInsert = "silvia" Then
                    pathCondizioni = Server.MapPath("~\Content\File_Base_UC\Condizioni_Generali_Fornitura_ENG.pdf")
                End If
                Dim pathCondizioniFolder = Server.MapPath("~\Content\File_Base_UC\")
                Dim pathNEW = Server.MapPath("~\Content\upload_UC\ConFirma\" + filename)
                Dim OC_Code() = OC.OC.Split("-")
                Dim resString = ""
                Try
                    resString = "Offerta servizio " + OC.OC.ToString + ";" + "" + ";" + pathCondizioni.ToString + ";" + pathNEW.ToString
                    Return PartialView(New EmailViewModel With {
                     .Corpo = "Buongiorno, le alleghiamo qui sotto i file per l'ordine da lei effettuato.",
                     .File_Percorso = pathNEW.ToString,
                     .Privacy_Percorso = pathCondizioni.ToString,
                     .Mittente = emaMittente,
                     .Oggetto = "Offerta servizio " + OC.OC.ToString,
                     .Id = OC.Id,
                     .Destinatario = ""
                    })
                Catch ex As SystemException
                    db.Log.Add(New Log With {
                                                     .Livello = TipoLogLivello.Errors,
                                                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                     .Messaggio = "Errore: " + ex.Message,
                                                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = "errore"}),
                                                    .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                       })
                    db.SaveChanges()
                    Return View()

                End Try

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

        End Function
        Function SendTo(ByVal id As Integer) As ActionResult

            Return PartialView()
        End Function
        Function SendToProd(ByVal id As Integer) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC = db.AccettazioneUC.Where(Function(x) x.Id = id).First
                Dim exist = db.ProgettiProd.Where(Function(x) x.OC_Riferimento = OC.OC).Count
                Dim existTec = db.ProgettiUT.Where(Function(x) x.OC_Riferimento = OC.OC).Count
                If exist > 0 Then
                    Return Json(New With {.ok = False, .message = "Progetto già esistente."}, JsonRequestBehavior.AllowGet)
                End If
                If existTec > 0 And Not OC.Accettato = Stato_UC.Ritorno_da_UT Then
                    Return Json(New With {.ok = False, .message = "Progetto già inviato al Tecnico."}, JsonRequestBehavior.AllowGet)
                End If
                Dim Progetto As New ProgettiProd With {
                    .OC_Riferimento = OC.OC,
                    .DataCreazione = DateTime.Now,
                    .StatoProgetto = Stato_Prod.In_attesa,
                    .Operatore = "Tiziano",
                    .Priorita = OC.Priorita,
                    .DataRichiestaConsegna = OC.DataRichiestaConsegna
                    }
                db.ProgettiProd.Add(Progetto)
                db.SaveChanges()
                OC.Accettato = Stato_UC.Inviato_Prod
                db.SaveChanges()
                db.StoricoOC.Add(New StoricoOC With {
                        .OC = OC.OC,
                        .Descrizione = "Documento condiviso correttamente con l'ufficio Produzione",
                        .Titolo = "Inviata a Produzione",
                        .Ufficio = TipoUfficio.UfficioCommerciale,
                        .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                    })
                db.SaveChanges()
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Inviata accettazione a Produzione",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id, .OC = OC.OC}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
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
                Return Json(New With {.ok = False, .message = "Errore invio in prod."}, JsonRequestBehavior.AllowGet)

            End Try
            Return Json(New With {.ok = True, .message = "Inviato a produzione correttamente!"}, JsonRequestBehavior.AllowGet)
        End Function
        Function SendToUT(ByVal id As Integer) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC = db.AccettazioneUC.Where(Function(x) x.Id = id).First
                Dim exist = db.ProgettiUT.Where(Function(x) x.OC_Riferimento = OC.OC).Count
                Dim existProd = db.ProgettiProd.Where(Function(x) x.OC_Riferimento = OC.OC).Count
                If existProd > 0 Then
                    Return Json(New With {.ok = False, .message = "Progetto già in produzione."}, JsonRequestBehavior.AllowGet)
                End If
                If exist > 0 Then
                    Return Json(New With {.ok = False, .message = "Progetto già esistente."}, JsonRequestBehavior.AllowGet)
                End If
                If OC.IsRevisione Then
                    Dim roleId = appctx.Roles.Where(Function(m) m.Name = "Tecnico").[Select](Function(m) m.Id).SingleOrDefault()
                    Dim user = appctx.Users.Where(Function(u) u.Email = "a.zambelli@euromagroup.com").First
                    Dim Progetto As New ProgettiUT With {
                    .OC_Riferimento = OC.OC,
                    .DataCreazione = DateTime.Now,
                    .StatoProgetto = Stato_UT.In_Attesa_Operatore,
                    .OperatoreSmistamento = "Sistema",
                    .Operatore = user.UserName,
                    .OperatoreId = user.Id,
                    .DataRichiestaConsegna = OC.DataRichiestaConsegna,
                    .Priorita = OC.Priorita
                }
                    db.ProgettiUT.Add(Progetto)
                    db.SaveChanges()
                    db.StoricoOC.Add(New StoricoOC With {
                       .OC = OC.OC,
                       .Descrizione = "Smistamento automatico da sistema causa REVISIONE",
                       .Titolo = "Inviata a Tecnico",
                       .Ufficio = TipoUfficio.UfficioCommerciale,
                       .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                   })
                    db.SaveChanges()
                Else
                    Dim Progetto As New ProgettiUT With {
                    .OC_Riferimento = OC.OC,
                    .DataCreazione = DateTime.Now,
                    .StatoProgetto = Stato_UT.In_attesa_Admin,
                    .DataRichiestaConsegna = OC.DataRichiestaConsegna,
                    .Priorita = OC.Priorita
                }
                    db.ProgettiUT.Add(Progetto)
                    db.SaveChanges()
                End If
                OC.Accettato = Stato_UC.Inviato_UC
                db.SaveChanges()
                db.StoricoOC.Add(New StoricoOC With {
                        .OC = OC.OC,
                        .Descrizione = "Documento condiviso correttamente con l'ufficio Tecnico",
                        .Titolo = "Inviata a Tecnico",
                        .Ufficio = TipoUfficio.UfficioCommerciale,
                        .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .Operatore = OpName, .OperatoreID = OpID}
                    })
                db.SaveChanges()
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Inviata accettazione a UT",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                db.SaveChanges()
            Catch ex As System.Exception
                db.Log.Add(New Log With {
                                                  .Livello = TipoLogLivello.Errors,
                                                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                  .Messaggio = "Errore: " + ex.Message + " - Id OC: " + id,
                                                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.SendToUT = "errore"}),
                                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                    })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore nel salvataggio del file."}, JsonRequestBehavior.AllowGet)

            End Try
            Return Json(New With {.ok = True, .message = "Salvato correttamente!"}, JsonRequestBehavior.AllowGet)
        End Function
        Function DownloadFile(ByVal id As Integer) As FileResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim fileDoc = db.DocumentiPerOC.Where(Function(x) x.Id = id).First
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Scaricato File da OC",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                db.SaveChanges()
                Return File(IO.File.ReadAllBytes(fileDoc.Percorso_File), "application/octet-stream", fileDoc.Nome_File)

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
            Return Nothing
        End Function
        Function ScaricaFile(ByVal id As Integer) As FileResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC = db.AccettazioneUC.Where(Function(x) x.Id = id).First
                Dim filename = ""
                If OC.OC.Contains("OC") Then
                    filename = OC.File.Split("\").Last

                ElseIf OC.OC.Contains("PR") Then
                    filename = OC.File.Split("\").Last
                Else
                    filename = OC.File.Split("\").Last
                End If
                Dim pathNEW = Server.MapPath("~\Content\upload_UC\ConFirma\" + filename)
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Scaricato File",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                db.SaveChanges()
                Return File(IO.File.ReadAllBytes(pathNEW), "application/octet-stream", OC.OC.ToString + ".pdf")

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


            Return Nothing
        End Function
        Function ScaricaFileRaw(ByVal id As Integer) As FileResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC = db.AccettazioneUC.Where(Function(x) x.Id = id).First
                Dim filename = ""
                If OC.OC.Contains("OC") Then
                    filename = OC.File.Split("\").Last

                ElseIf OC.OC.Contains("PR") Then
                    filename = OC.File.Split("\").Last
                Else
                    filename = OC.File.Split("\").Last
                End If
                Dim pathNEW = Server.MapPath("~\Content\upload_UC\" + filename)
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Scaricato File Raw",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = OC.Id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                db.SaveChanges()
                Return File(IO.File.ReadAllBytes(pathNEW), "application/octet-stream", OC.OC.ToString + ".pdf")

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


            Return Nothing
        End Function
        ' POST: AccettazioneUC/Edit/5
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,OC,Cartella,OperatoreInsert,DataCreazione,Accettato,Note")> ByVal accettazioneUC As AccettazioneUC, file As HttpPostedFileBase) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim fileSalvati As New List(Of String)
            Dim pathTMP = ""
            Try
                For i = 0 To Request.Files.Count - 1
                    Try
                        Dim UploadedFile As HttpPostedFileBase = Request.Files(i)
                        If UploadedFile IsNot Nothing AndAlso UploadedFile.ContentLength > 0 Then
                            pathTMP = Path.Combine(Server.MapPath("~/Content/upload_UC"), UploadedFile.FileName.ToString.Replace(" ", String.Empty))

                            If System.IO.File.Exists(pathTMP) Then
                                fileSalvati.Add(pathTMP)
                            Else
                                UploadedFile.SaveAs(pathTMP)
                                fileSalvati.Add(pathTMP)
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
                If fileSalvati.Count > 0 Then
                    OpID = User.Identity.GetUserId()
                    OpName = User.Identity.GetUserName()
                    Dim OC = db.AccettazioneUC.Where(Function(x) x.Id = accettazioneUC.Id).First
                    OC.Cartella = accettazioneUC.Cartella
                    OC.File = pathTMP
                    db.SaveChanges()
                    Dim mySmtp As New SmtpClient
                    Dim myMail As New MailMessage()
                    mySmtp.UseDefaultCredentials = False
                    mySmtp.Credentials = New System.Net.NetworkCredential("no-reply@euromagroup.com", "yp@4d%p2AFa;")
                    mySmtp.Host = "oberon.dnshigh.com"
                    myMail = New MailMessage()
                    myMail.From = New MailAddress("no-reply@euromagroup.com")
                    myMail.Attachments.Add(New System.Net.Mail.Attachment(pathTMP))
                    Dim role = appctx.Roles.SingleOrDefault(Function(m) m.Name = "Commerciale_Admin")
                    Dim usersInRole = appctx.Users.Where(Function(m) m.Roles.Any(Function(r) r.RoleId = role.Id))
                    For Each a In usersInRole
                        If a.Profile.NotificheViaMail Then
                            myMail.To.Add(a.Email)
                        End If
                    Next
                    myMail.Subject = "Aggiornamento file per " + OC.OC.ToString
                    Dim StrContent = ""
                    Using reader = New StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Views/Shared/Email_Euroma.vbhtml")
                        Dim readFile As String = reader.ReadToEnd()
                        StrContent = readFile
                        StrContent = StrContent.Replace("[Username]", "Amministratori")
                        StrContent = StrContent.Replace("[Motivo]", "E' stato aggiornato un file all'interno del portale web Euroma.")
                    End Using
                    myMail.Body = StrContent.ToString
                    myMail.IsBodyHtml = True
                    ' myMail.Body = "E' stato inserito un nuovo file da accettare all'interno del portale Web Euroma. <br> <a href='http://euromaweb.com/'>Clicca qui</a>"
                    mySmtp.Send(myMail)
                Else
                    OpID = User.Identity.GetUserId()
                    OpName = User.Identity.GetUserName()
                    Dim OC = db.AccettazioneUC.Where(Function(x) x.Id = accettazioneUC.Id).First
                    OC.Cartella = accettazioneUC.Cartella
                    db.SaveChanges()
                End If


                Return Json(New With {.ok = True, .message = "Modifiche salvate correttamente!"})
            Catch ex As SystemException
                db.Log.Add(New Log With {
                                                     .Livello = TipoLogLivello.Errors,
                                                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                     .Messaggio = "Errore: " + ex.Message,
                                                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = "errore"}),
                                                    .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                       })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message})
            End Try
        End Function

        Function EditAccettazione(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim accettazioneUC As AccettazioneUC = db.AccettazioneUC.Find(id)
            If IsNothing(accettazioneUC) Then
                Return HttpNotFound()
            End If
            Return PartialView(accettazioneUC)
        End Function

        ' POST: AccettazioneUC/Edit/5
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function EditAccettazione(<Bind(Include:="Id,OC,Cartella,OperatoreInsert,DataCreazione,Accettato,Note")> ByVal accettazioneUC As AccettazioneUC) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim OC = db.AccettazioneUC.Where(Function(x) x.Id = accettazioneUC.Id).First
                If accettazioneUC.Accettato = Stato_UC.Accettato And OC.Accettato = Stato_UC.In_attesa Then
                    OC.OperatoreAccettazione = OpName
                    OC.DataAccettazione = DateTime.Now
                    OC.Note = accettazioneUC.Note

                    OC.Accettato = accettazioneUC.Accettato
                    db.SaveChanges()
                    Try
                        Dim filename = ""
                        If OC.OC.Contains("OC") Then
                            filename = OC.File.Split("\").Last

                        ElseIf OC.OC.Contains("PR") Then
                            filename = OC.File.Split("\").Last
                        Else
                            filename = OC.File.Split("\").Last
                        End If
                        If Not System.IO.File.Exists(Server.MapPath("~\Content\upload_UC\ConFirma\" + filename)) Then
                            Dim pathOLD = Server.MapPath("~\Content\upload_UC\" + filename)
                            Dim pathNEW = Server.MapPath("~\Content\upload_UC\ConFirma\" + filename)
                            Dim reader As PdfReader = New PdfReader(pathOLD)
                            Dim fs As FileStream = New FileStream(pathNEW, FileMode.Create)
                            Dim stamper = New PdfStamper(reader, fs)
                            Dim pdfContentByte = stamper.GetOverContent(reader.NumberOfPages)
                            Dim sigimage As Image = iTextSharp.text.Image.GetInstance(Server.MapPath("~\Content\Firme\Firma.png"))
                            sigimage.SetAbsolutePosition(150, 80)
                            sigimage.ScaleAbsolute(80.0F, 40.0F)
                            sigimage.Alignment = iTextSharp.text.Image.UNDERLYING
                            pdfContentByte.AddImage(sigimage)
                            stamper.FormFlattening = True
                            stamper.Writer.CloseStream = True
                            stamper.Close()
                            reader.Close()
                            fs.Close()
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
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message})
                    End Try
                    Dim em = appctx.Users.Where(Function(x) x.UserName = OC.OperatoreInsert).First 'db.UserEmail.Where(Function(x) x.Uid = OC.OperatoreInsert).First
                    If em.Profile.NotificheViaMail Then
                        Dim mySmtp As New SmtpClient
                        Dim myMail As New MailMessage()
                        mySmtp.UseDefaultCredentials = False
                        mySmtp.Credentials = New System.Net.NetworkCredential("no-reply@euromagroup.com", "yp@4d%p2AFa;")
                        mySmtp.Host = "oberon.dnshigh.com"
                        myMail = New MailMessage()
                        myMail.From = New MailAddress("no-reply@euromagroup.com")
                        If em.Profile.NotificheViaMail Then
                            myMail.To.Add(em.Email)
                        End If

                        myMail.Subject = "Nuovo file accettato - " + OC.OC.ToString
                        Dim StrContent = ""
                        Dim note = ""
                        If Not IsNothing(OC.Note) Then
                            note = OC.Note
                        End If
                        Using reader = New StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Views/Shared/Email_Euroma.vbhtml")
                            Dim readFile As String = reader.ReadToEnd()
                            StrContent = readFile
                            StrContent = StrContent.Replace("[Username]", em.Profile.Soprannome)
                            StrContent = StrContent.Replace("[Motivo]", "E' stato accettato un file all'interno del portale Web Euroma. <br>Note: " + note + "")
                        End Using
                        myMail.Body = StrContent.ToString
                        myMail.IsBodyHtml = True
                        mySmtp.Send(myMail)
                    End If
                ElseIf accettazioneUC.Accettato = Stato_UC.Non_Accettato And OC.Accettato = Stato_UC.In_attesa Then
                    OC.OperatoreAccettazione = OpName
                    OC.DataAccettazione = DateTime.Now
                    OC.Note = accettazioneUC.Note
                    OC.Accettato = Stato_UC.Non_Accettato
                    db.SaveChanges()
                    Dim note = ""
                    If Not IsNothing(OC.Note) Then
                        note = OC.Note
                    End If
                    Try
                        Dim filename = ""
                        If OC.OC.Contains("OC") Then
                            filename = OC.File.Split("\").Last

                        ElseIf OC.OC.Contains("PR") Then
                            filename = OC.File.Split("\").Last
                        Else
                            filename = OC.File.Split("\").Last
                        End If
                        If Not System.IO.File.Exists(Server.MapPath("~\Content\upload_UC\ConFirma\" + filename)) Then
                            Dim pathOLD = Server.MapPath("~\Content\upload_UC\" + filename)
                            Dim pathNEW = Server.MapPath("~\Content\upload_UC\ConFirma\" + filename)
                            Dim reader As PdfReader = New PdfReader(pathOLD)
                            Dim fs As FileStream = New FileStream(pathNEW, FileMode.Create)
                            Dim stamper = New PdfStamper(reader, fs)
                            Dim pdfContentByte = stamper.GetOverContent(reader.NumberOfPages)
                            Dim sigimage As Image = iTextSharp.text.Image.GetInstance(Server.MapPath("~\Content\Firme\Firma.png"))
                            sigimage.SetAbsolutePosition(150, 80)
                            sigimage.ScaleAbsolute(80.0F, 40.0F)
                            sigimage.Alignment = iTextSharp.text.Image.UNDERLYING
                            pdfContentByte.AddImage(sigimage)
                            stamper.FormFlattening = True
                            stamper.Writer.CloseStream = True
                            stamper.Close()
                            reader.Close()
                            fs.Close()
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
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message})
                    End Try
                    Dim StrContent = ""
                    Dim em = appctx.Users.Where(Function(x) x.UserName = OC.OperatoreInsert).First 'db.UserEmail.Where(Function(x) x.Uid = OC.OperatoreInsert).First
                    If em.Profile.NotificheViaMail Then
                        Dim mySmtp As New SmtpClient
                        Dim myMail As New MailMessage()
                        mySmtp.UseDefaultCredentials = False
                        mySmtp.Credentials = New System.Net.NetworkCredential("no-reply@euromagroup.com", "yp@4d%p2AFa;")
                        mySmtp.Host = "oberon.dnshigh.com"
                        myMail = New MailMessage()
                        myMail.From = New MailAddress("no-reply@euromagroup.com")
                        If em.Profile.NotificheViaMail Then
                            myMail.To.Add(em.Email)
                        End If
                        myMail.Subject = "File non accettato - " + OC.OC.ToString
                        myMail.IsBodyHtml = True
                        Using reader = New StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Views/Shared/Email_Euroma.vbhtml")
                            Dim readFile As String = reader.ReadToEnd()
                            StrContent = readFile
                            StrContent = StrContent.Replace("[Username]", em.Profile.Soprannome)
                            StrContent = StrContent.Replace("[Motivo]", "L'amministratore non ha accettato il file. <br> Causa: " + note + "  ")
                        End Using
                        myMail.Body = StrContent.ToString
                        'myMail.Body = "Ciao " + OC.OperatoreInsert + ", <br> l'amministratore non ha accettato il file. <br> Causa: " + OC.Note.ToString + " <br><a href='http://euromaweb.com/'>Clicca qui</a>"
                        mySmtp.Send(myMail)
                    End If
                ElseIf accettazioneUC.Accettato <> OC.Accettato Then
                    OC.Accettato = accettazioneUC.Accettato
                    db.SaveChanges()
                    Dim StrContent = ""
                    Dim note = ""
                    If Not IsNothing(OC.Note) Then
                        note = OC.Note
                    End If
                    Dim em = appctx.Users.Where(Function(x) x.UserName = OC.OperatoreInsert).First 'db.UserEmail.Where(Function(x) x.Uid = OC.OperatoreInsert).First
                    If em.Profile.NotificheViaMail Then
                        Dim mySmtp As New SmtpClient
                        Dim myMail As New MailMessage()
                        mySmtp.UseDefaultCredentials = False
                        mySmtp.Credentials = New System.Net.NetworkCredential("no-reply@euromagroup.com", "yp@4d%p2AFa;")
                        mySmtp.Host = "oberon.dnshigh.com"
                        myMail = New MailMessage()
                        myMail.From = New MailAddress("no-reply@euromagroup.com")
                        myMail.To.Add(em.Email)
                        myMail.Subject = "File non accettato - " + OC.OC.ToString
                        myMail.IsBodyHtml = True
                        Using reader = New StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Views/Shared/Email_Euroma.vbhtml")
                            Dim readFile As String = reader.ReadToEnd()
                            StrContent = readFile
                            StrContent = StrContent.Replace("[Username]", em.Profile.Soprannome)
                            StrContent = StrContent.Replace("[Motivo]", "E' stato modificato lo stato in un'accettazione. <br> Causa: " + note + "  ")
                        End Using
                        myMail.Body = StrContent.ToString
                        'myMail.Body = "Ciao " + OC.OperatoreInsert + ", <br> l'amministratore non ha accettato il file. <br> Causa: " + OC.Note.ToString + " <br><a href='http://euromaweb.com/'>Clicca qui</a>"
                        mySmtp.Send(myMail)
                    End If

                End If
                Return Json(New With {.ok = True, .message = "Richiesta modifica inviata!"})
            Catch ex As SystemException
                db.Log.Add(New Log With {
                                                     .Livello = TipoLogLivello.Errors,
                                                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                     .Messaggio = "Errore: " + ex.Message,
                                                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = "errore"}),
                                                    .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                       })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message})
            End Try

        End Function
        Public Shared Function Decrypter(ByVal Text As String) As String
            Try
                Dim bytesBuff As Byte() = Convert.FromBase64String(Text)
                Using aes__1 As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()
                    Dim crypto As New System.Security.Cryptography.Rfc2898DeriveBytes("11 23 91 b7 51 b5 ee b5 86 fd e9 1e 44 20 3a 2a", {&H45, &H77, &H89, &H4E, &H23, &H2D, &H45, &H44, &H86, &H55, &H84, &H95, &H36})
                    aes__1.Key = crypto.GetBytes(32)
                    aes__1.IV = crypto.GetBytes(16)
                    Using mStream As New System.IO.MemoryStream()
                        Using cStream As New System.Security.Cryptography.CryptoStream(mStream, aes__1.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
                            cStream.Write(bytesBuff, 0, bytesBuff.Length)
                            cStream.Close()
                        End Using
                        Text = Encoding.Unicode.GetString(mStream.ToArray())
                    End Using
                End Using
            Catch ex As SystemException

            End Try


            Return Text
        End Function


        Public Shared Function TextEncrypt(ByVal Text As String) As String
            Try
                Dim bytesBuff As Byte() = Encoding.Unicode.GetBytes(Text)
                Using aes__1 As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()
                    Dim crypto As New System.Security.Cryptography.Rfc2898DeriveBytes("dskjlsfkojhsfkhkjoieewfiduhpuih4785.reiuyt", {&H45, &H77, &H89, &H4E, &H23, &H2D, &H45, &H44, &H86, &H55, &H84, &H95, &H36})
                    aes__1.Key = crypto.GetBytes(32)
                    aes__1.IV = crypto.GetBytes(16)
                    Using mStream As New System.IO.MemoryStream()
                        Using cStream As New System.Security.Cryptography.CryptoStream(mStream, aes__1.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
                            cStream.Write(bytesBuff, 0, bytesBuff.Length)
                            cStream.Close()
                        End Using
                        Text = Convert.ToBase64String(mStream.ToArray())
                    End Using
                End Using
            Catch
            End Try
            Return Text
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
        'Private Function GetNomeOperatore(ByVal id As String) As String
        '    Try
        '        If Not IsNothing(id) Then
        '            Return db..Find(id).UserName
        '        End If
        '    Catch ex As SystemException

        '    End Try
        '    Return vbNullString

        'End Function

    End Class
End Namespace
