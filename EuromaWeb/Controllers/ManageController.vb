Imports System.IO
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.VisualBasic.Devices

<Authorize>
Public Class ManageController
    Inherits Controller

    Private db As New EuromaModels
    Private appctx As New ApplicationDbContext

    Public Sub New()
    End Sub

    Private _signInManager As ApplicationSignInManager
    Private _userManager As ApplicationUserManager

    Public Sub New(appUserManager As ApplicationUserManager, appSignInManager As ApplicationSignInManager)
        UserManager = appUserManager
        SignInManager = appSignInManager
    End Sub

    Public Property SignInManager() As ApplicationSignInManager
        Get
            Return If(_signInManager, HttpContext.GetOwinContext().Get(Of ApplicationSignInManager)())
        End Get
        Private Set(value As ApplicationSignInManager)
            _signInManager = value
        End Set
    End Property

    Public Property UserManager() As ApplicationUserManager
        Get
            Return If(_userManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
        End Get
        Private Set(value As ApplicationUserManager)
            _userManager = value
        End Set
    End Property

    '
    ' GET: /Manage/Index
    Public Async Function Index(message As System.Nullable(Of ManageMessageId)) As Task(Of ActionResult)
        ViewData!StatusMessage = If(message = ManageMessageId.ChangePasswordSuccess, "La password è stata cambiata.", If(message = ManageMessageId.SetPasswordSuccess, "La password è stata impostata.", If(message = ManageMessageId.SetTwoFactorSuccess, "Il provider di autenticazione a due fattori è stato impostato.", If(message = ManageMessageId.[Error], "Si è verificato un errore.", If(message = ManageMessageId.AddPhoneSuccess, "Il numero di telefono è stato aggiunto.", If(message = ManageMessageId.RemovePhoneSuccess, "Il numero di telefono è stato rimosso.", ""))))))
        ViewBag.utenti = appctx.Users.ToList
        Dim userId = User.Identity.GetUserId()
        Dim model = New IndexViewModel() With {
            .HasPassword = HasPassword(),
            .PhoneNumber = Await UserManager.GetPhoneNumberAsync(userId),
            .TwoFactor = Await UserManager.GetTwoFactorEnabledAsync(userId),
            .Logins = Await UserManager.GetLoginsAsync(userId),
            .BrowserRemembered = Await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
        }
        If User.IsInRole("Admin") Then
            Dim utenti = appctx.Users.ToList
            Dim listUtenti As New List(Of UserListViewModel)
            For Each u In utenti
                listUtenti.Add(New UserListViewModel With {
                    .Email = u.Email,
                    .Username = u.UserName
                })
            Next
            ViewBag.Users = listUtenti
        End If
        Return View(model)
    End Function
    Public Function AggiungiLicenza() As ActionResult
        Dim utenti = appctx.Users.ToList
        ViewBag.IdEsterno = New SelectList(utenti, "Id", "Username")
        Return PartialView()
    End Function
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function AggiungiLicenza(<Bind(Include:="NomeLicenza,DescrizioneLicenza,DataInizio,DataRinnovo,TypeLicenza,CostoLicenza,QtaLicenze")> ByVal Licenza As UserLicenze) As JsonResult
        If ModelState.IsValid Then
            Try
                Dim NewLic = New UserLicenze With {
                .CostoLicenza = Licenza.CostoLicenza,
                .DataInizio = Licenza.DataInizio,
                .DataRinnovo = Licenza.DataRinnovo,
                .DescrizioneLicenza = Licenza.DescrizioneLicenza,
                .DurataLicenza = Licenza.DurataLicenza,
                .NomeLicenza = Licenza.NomeLicenza,
                .TypeLicenza = Licenza.TypeLicenza,
                .QtaLicenze = Licenza.QtaLicenze
            }
                appctx.UserLicenze.Add(NewLic)
                appctx.SaveChanges()
                Return Json(New With {.ok = True, .message = "Licenza correttamente aggiunta, q.tà: " + Licenza.QtaLicenze.ToString})
            Catch ex As Exception
                Return Json(New With {.ok = False, .message = "Errore:" & ex.Message & "."})
            End Try
        Else
            Return Json(New With {.ok = False, .message = "Errore: Modello dati non valido."})
        End If
    End Function
    Public Function ModificaLicenza(ByVal id As Integer) As ActionResult
        Dim licenza = appctx.UserLicenze.Find(id)
        Return PartialView(licenza)
    End Function
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function ModificaLicenza(<Bind(Include:="Id,NomeLicenza,DescrizioneLicenza,DataInizio,DataRinnovo,TypeLicenza,CostoLicenza,QtaLicenze")> ByVal Licenza As UserLicenze, file As HttpPostedFileBase) As JsonResult
        If ModelState.IsValid Then
            For i = 0 To Request.Files.Count - 1
                Dim OpID As String = vbNullString
                Dim OpName As String = vbNullString
                Dim CurrentDate As DateTime = Now
                Try
                    OpID = User.Identity.GetUserId()
                    OpName = User.Identity.GetUserName()
                    Dim UploadedFile As HttpPostedFileBase = Request.Files(i)
                    If UploadedFile IsNot Nothing AndAlso UploadedFile.ContentLength > 0 Then
                        Dim pathTMP = Path.Combine(Server.MapPath("~/Content/upload_doc_licenze"), UploadedFile.FileName)
                        If System.IO.File.Exists(pathTMP) Then
                            db.DocumentiPerLicenze.Add(New DocumentiPerLicenze With {
                                .DataCreazioneFile = DateTime.Now,
                                .Nome_File = UploadedFile.FileName,
                                .Id = Licenza.Id,
                                .Operatore_Id = OpID,
                                .Operatore_Nome = OpName,
                                .Percorso_File = pathTMP
                            })
                            db.SaveChanges()
                        Else
                            UploadedFile.SaveAs(pathTMP)
                            db.DocumentiPerLicenze.Add(New DocumentiPerLicenze With {
                                .DataCreazioneFile = DateTime.Now,
                                .Nome_File = UploadedFile.FileName,
                                .Id = Licenza.Id,
                                .Operatore_Id = OpID,
                                .Operatore_Nome = OpName,
                                .Percorso_File = pathTMP
                            })
                            db.SaveChanges()
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
            Try
                Dim lic = appctx.UserLicenze.Where(Function(x) x.Id = Licenza.Id).First
                If lic.NomeLicenza <> Licenza.NomeLicenza Then
                    lic.NomeLicenza = Licenza.NomeLicenza
                End If
                If lic.DescrizioneLicenza <> Licenza.DescrizioneLicenza Then
                    lic.DescrizioneLicenza = Licenza.DescrizioneLicenza
                End If
                If lic.DataInizio <> Licenza.DataInizio Then
                    lic.DataInizio = Licenza.DataInizio
                End If
                If lic.DataRinnovo <> Licenza.DataRinnovo Then
                    lic.DataRinnovo = Licenza.DataRinnovo
                End If
                If lic.TypeLicenza <> Licenza.TypeLicenza Then
                    lic.TypeLicenza = Licenza.TypeLicenza
                End If
                If lic.CostoLicenza <> Licenza.CostoLicenza Then
                    lic.CostoLicenza = Licenza.CostoLicenza
                End If
                If lic.QtaLicenze <> Licenza.QtaLicenze Then
                    lic.QtaLicenze = Licenza.QtaLicenze
                End If
                appctx.SaveChanges()
                Return Json(New With {.ok = True, .message = "Licenza modificata correttamente"})
            Catch ex As Exception
                Return Json(New With {.ok = False, .message = "Errore:" & ex.Message & "."})
            End Try
        Else
            Return Json(New With {.ok = False, .message = "Errore: Modello dati non valido."})
        End If
    End Function
    <Authorize>
    Function Computer() As ActionResult
        If User.IsInRole("Admin") Then
            Dim listPC = db.Computer.ToList
            Dim FinalListPc As New List(Of ComputerViewModel)
            For Each pc In listPC
                Dim attivo = IIf(My.Computer.Network.Ping(pc.IP), 1, 0)
                Dim pcvm = New ComputerViewModel With {
                .DescrizionePC = pc.DescrizionePC,
                .IP = pc.IP,
                .MAC = pc.MAC,
                .NomeOperatore = pc.NomeOperatore,
                .NomePC = pc.NomePC,
                .Id = pc.Id,
                .Attivo = attivo
            }
                FinalListPc.Add(pcvm)
            Next
            Return View(FinalListPc)
        End If
        Return View("Index", "Home")
    End Function
    Function AddComputer() As ActionResult
        Return PartialView()
    End Function
    Function TurnOffPC(ByVal id As Integer) As JsonResult
        Try
            Dim pc = db.Computer.Find(id)
            Dim myProcess = New System.Diagnostics.Process()
            myProcess.StartInfo.FileName = "CMD"
            myProcess.StartInfo.UseShellExecute = False
            myProcess.StartInfo.RedirectStandardInput = True
            myProcess.Start()
            Dim myStreamWriter = myProcess.StandardInput
            myStreamWriter.WriteLine("shutdown -m \\" + pc.IP + " -s -f")
            Return Json(New With {.ok = True, .message = "Pc spento correttamente."}, JsonRequestBehavior.AllowGet)
        Catch ex As Exception
            Return Json(New With {.ok = False, .message = "Errore: impossibile spegnere il PC."}, JsonRequestBehavior.AllowGet)
        End Try
        Return Json(New With {.ok = False, .message = "Errore: impossibile spegnere il PC."}, JsonRequestBehavior.AllowGet)
    End Function
    Function TurnOnPC(ByVal id As Integer) As JsonResult
        Try
            Dim pc = db.Computer.Find(id)
            Dim myProcess = New System.Diagnostics.Process()
            myProcess.StartInfo.FileName = "CMD"
            myProcess.StartInfo.UseShellExecute = False
            myProcess.StartInfo.RedirectStandardInput = True
            myProcess.Start()
            Dim myStreamWriter = myProcess.StandardInput
            myStreamWriter.WriteLine("\\srv2k16\D\Azienda\Utenti\Installer_Programmi\WOL\wolcmd.exe " + pc.MAC + " " + pc.IP + " 255.255.255.0")
            Return Json(New With {.ok = True, .message = "Pc acceso correttamente."}, JsonRequestBehavior.AllowGet)
        Catch ex As Exception
            Return Json(New With {.ok = False, .message = "Errore: impossibile accendere il PC."}, JsonRequestBehavior.AllowGet)
        End Try
        Return Json(New With {.ok = False, .message = "Errore: impossibile accendere il PC."}, JsonRequestBehavior.AllowGet)
    End Function
    <HttpPost()>
    Function AddComputer(<Bind(Include:="NomePC,MAC,IP,NomeOperatore,DescrizionePC")> ByVal pc As Computer) As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpID = User.Identity.GetUserId()
            OpName = User.Identity.GetUserName()
            If ModelState.IsValid Then
                db.Computer.Add(New Computer With {
                    .IP = pc.IP,
                    .DescrizionePC = pc.DescrizionePC,
                    .MAC = pc.MAC,
                    .NomeOperatore = pc.NomeOperatore,
                    .NomePC = pc.NomePC
                })
                db.SaveChanges()
                db.Audit.Add(New Audit With {
                    .Livello = TipoAuditLivello.Info,
                    .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                    .Messaggio = "PC aggiunto correttamente",
                    .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.PC = pc.NomePC, .OP = pc.NomeOperatore}),
                    .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "Pc correttamente aggiunto."})
            End If
            Return Json(New With {.ok = False, .message = "Errore: Modello dati non valido."})
        Catch ex As Exception
            db.Log.Add(New Log With {
                   .Livello = TipoLogLivello.Errors,
                   .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                   .Messaggio = "Impossibile creare PC",
                   .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Subject = pc.NomePC}),
                   .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
             })
            db.SaveChanges()
            Return Json(New With {.ok = False, .message = "Errore: """})
        End Try
        Return PartialView()
    End Function
    <HttpPost()>
    <ValidateInput(False)>
    Function ServerProcessingLicenze(PostedData As DataTableAjaxPostModel) As JsonResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpID = User.Identity.GetUserId()
            OpName = User.Identity.GetUserName()

            Dim result As New List(Of Object)
            Dim data As IQueryable(Of AspNetUserExchangeLicenseTable)
            Dim UsersList = appctx.Users.ToList
            Dim UsersLicenze = appctx.UserLicenze.ToList
            Dim exchange = appctx.AspNetUserExchangeLicenseTable.ToList
            data = appctx.AspNetUserExchangeLicenseTable
            'paginazione
            Dim filtered As Integer = 0
            Try
                filtered = data.Count
                'If PostedData.length > 0 Then
                '    data = data.Skip(PostedData.start).Take(PostedData.length)
                'End If
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
            For Each Acc As AspNetUserExchangeLicenseTable In data
                If Not result.Any(Function(x) x.id = Acc.IdEsternoLicenza) Then
                    Try
                        Dim userString = "Utenti: "
                        Dim count = 0
                        For Each u In exchange.Where(Function(x) x.IdEsternoLicenza = Acc.IdEsternoLicenza).ToList
                            Dim utente = UsersList.Where(Function(x) x.Id = u.IdEsternoUtente).First.UserName
                            userString = userString + utente
                            If Not u Is exchange.Where(Function(x) x.IdEsternoLicenza = Acc.IdEsternoLicenza).ToList.Last Then
                                userString = userString + ","
                            End If
                            count = count + 1
                        Next
                        Dim lic = UsersLicenze.Where(Function(x) x.Id = Acc.IdEsternoLicenza).First
                        result.Add(New With {
                                    .DT_RowData = New With {.value = Acc.Id},
                                    .DT_RowId = "row_" & Acc.Id,
                                    .Id = Acc.IdEsternoLicenza,
                                    .Utente = userString,
                                    .Licenza = lic.NomeLicenza,
                                    .Descrizione = lic.DescrizioneLicenza,
                                    .DataScadenza = lic.DataRinnovo.ToString.Split(" ")(0),
                                    .Qta = "Usata/e " + count.ToString + " su " + lic.QtaLicenze.ToString
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
                End If

            Next

            Return Json(New With {PostedData.draw, .recordsTotal = db.Licenze.Count, .recordsFiltered = filtered, .data = result})
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

    Public Function GestioneUtenti() As ActionResult
        Dim utenti = appctx.Users.ToList
        Dim listUtenti As New List(Of UserListViewModel)
        For Each u In utenti
            listUtenti.Add(New UserListViewModel With {
                .Email = u.Email,
                .Username = u.UserName
            })
        Next
        Dim listLicenze As New List(Of UserLicenze)
        listLicenze = appctx.UserLicenze.ToList
        Return View(New GestioneUtentiViewModel With {
            .ListaUtenti = listUtenti,
            .ListaLicenze = listLicenze
        })
    End Function
    Function CreateAssociazione() As ActionResult
        Dim utenti = appctx.Users.ToList
        Dim listaLicenze = appctx.UserLicenze.ToList
        Dim FinalLista As New List(Of UserLicenze)
        For Each l In listaLicenze
            If appctx.AspNetUserExchangeLicenseTable.Where(Function(x) x.IdEsternoLicenza = l.Id).Count < l.QtaLicenze Then
                FinalLista.Add(New UserLicenze With {
                        .Id = l.Id,
                        .NomeLicenza = l.NomeLicenza
                 })
            End If
        Next
        ViewBag.IdEsternoUtente = New SelectList(utenti, "Id", "Email")
        ViewBag.IdEsternoLicenza = New SelectList(FinalLista, "Id", "NomeLicenza")
        Return PartialView()
    End Function

    ' POST: HelpDesks/Create
    'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
    'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
    <HttpPost()>
    <ValidateAntiForgeryToken()>
    Function CreateAssociazione(<Bind(Include:="IdEsternoUtente, IdEsternoLicenza")> ByVal associazione As AspNetUserExchangeLicenseTable) As JsonResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        If ModelState.IsValid Then
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                appctx.AspNetUserExchangeLicenseTable.Add(New AspNetUserExchangeLicenseTable With {
                    .IdEsternoLicenza = associazione.IdEsternoLicenza,
                    .IdEsternoUtente = associazione.IdEsternoUtente
                })
                appctx.SaveChanges()
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Associata Licenza correttamnete",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.idUtente = associazione.IdEsternoUtente, .idEsterno = associazione.IdEsternoLicenza}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                               })
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "Licenza associata correttamente."})
            Catch ex As Exception
                db.Log.Add(New Log With {
                                                 .Livello = TipoLogLivello.Errors,
                                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                 .Messaggio = "Errore:  " + ex.Message,
                                                 .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.licenza = "errore"}),
                                                .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                                   })
                db.SaveChanges()

                Return Json(New With {.ok = False, .message = "Errore: Impossibile inserire associazione."})
            End Try
        End If
        Return Json(New With {.ok = False, .message = "Errore: Impossibile inserire associazione."})
    End Function

    '
    ' POST: /Manage/RemoveLogin
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function RemoveLogin(loginProvider As String, providerKey As String) As Task(Of ActionResult)
        Dim message As System.Nullable(Of ManageMessageId)
        Dim result = Await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), New UserLoginInfo(loginProvider, providerKey))
        If result.Succeeded Then
            Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
            If userInfo IsNot Nothing Then
                Await SignInManager.SignInAsync(userInfo, isPersistent:=False, rememberBrowser:=False)
            End If
            message = ManageMessageId.RemoveLoginSuccess
        Else
            message = ManageMessageId.[Error]
        End If
        Return RedirectToAction("ManageLogins", New With {
            message
        })
    End Function

    '
    ' GET: /Manage/AddPhoneNumber
    Public Function AddPhoneNumber() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Manage/AddPhoneNumber
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function AddPhoneNumber(model As AddPhoneNumberViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If
        ' Generare il token e inviarlo
        Dim code = Await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number)
        If UserManager.SmsService IsNot Nothing Then
            Dim message = New IdentityMessage() With {
                .Destination = model.Number,
                .Body = "Il codice di sicurezza è: " & Convert.ToString(code)
            }
            Await UserManager.SmsService.SendAsync(message)
        End If
        Return RedirectToAction("VerifyPhoneNumber", New With {
              .PhoneNumber = model.Number
        })
    End Function

    '
    ' POST: /Manage/EnableTwoFactorAuthentication
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function EnableTwoFactorAuthentication() As Task(Of ActionResult)
        Await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), True)
        Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
        If userInfo IsNot Nothing Then
            Await SignInManager.SignInAsync(userInfo, isPersistent:=False, rememberBrowser:=False)
        End If
        Return RedirectToAction("Index", "Manage")
    End Function

    '
    ' POST: /Manage/DisableTwoFactorAuthentication
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function DisableTwoFactorAuthentication() As Task(Of ActionResult)
        Await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), False)
        Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
        If userInfo IsNot Nothing Then
            Await SignInManager.SignInAsync(userInfo, isPersistent:=False, rememberBrowser:=False)
        End If
        Return RedirectToAction("Index", "Manage")
    End Function

    '
    ' GET: /Manage/VerifyPhoneNumber
    Public Async Function VerifyPhoneNumber(phoneNumber As String) As Task(Of ActionResult)
        Dim code = Await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber)
        ' Inviare un SMS tramite il provider SMS per verificare il numero di telefono
        Return If(phoneNumber Is Nothing, View("Error"), View(New VerifyPhoneNumberViewModel() With {
            .PhoneNumber = phoneNumber
        }))
    End Function

    '
    ' POST: /Manage/VerifyPhoneNumber
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function VerifyPhoneNumber(model As VerifyPhoneNumberViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If
        Dim result = Await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code)
        If result.Succeeded Then
            Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
            If userInfo IsNot Nothing Then
                Await SignInManager.SignInAsync(userInfo, isPersistent:=False, rememberBrowser:=False)
            End If
            Return RedirectToAction("Index", New With {
                .Message = ManageMessageId.AddPhoneSuccess
            })
        End If
        ' Se si è arrivati a questo punto, significa che si è verificato un errore, rivisualizzare il form
        ModelState.AddModelError("", "Verifica del numero di telefono non riuscita")
        Return View(model)
    End Function

    '
    ' POST: /Manage/RemovePhoneNumber
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function RemovePhoneNumber() As Task(Of ActionResult)
        Dim result = Await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), Nothing)
        If Not result.Succeeded Then
            Return RedirectToAction("Index", New With {
                .Message = ManageMessageId.[Error]
            })
        End If
        Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
        If userInfo IsNot Nothing Then
            Await SignInManager.SignInAsync(userInfo, isPersistent:=False, rememberBrowser:=False)
        End If
        Return RedirectToAction("Index", New With {
            .Message = ManageMessageId.RemovePhoneSuccess
        })
    End Function

    '
    ' GET: /Manage/ChangePassword
    Public Function ChangePassword() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Manage/ChangePassword
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function ChangePassword(model As ChangePasswordViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If
        Dim result = Await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword)
        If result.Succeeded Then
            Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
            If userInfo IsNot Nothing Then
                Await SignInManager.SignInAsync(userInfo, isPersistent:=False, rememberBrowser:=False)
            End If
            Return RedirectToAction("Index", New With {
                .Message = ManageMessageId.ChangePasswordSuccess
            })
        End If
        AddErrors(result)
        Return View(model)
    End Function

    '
    ' GET: /Manage/SetPassword
    Public Function SetPassword() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Manage/SetPassword
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function SetPassword(model As SetPasswordViewModel) As Task(Of ActionResult)
        If ModelState.IsValid Then
            Dim result = Await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword)
            If result.Succeeded Then
                Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
                If userInfo IsNot Nothing Then
                    Await SignInManager.SignInAsync(userInfo, isPersistent:=False, rememberBrowser:=False)
                End If
                Return RedirectToAction("Index", New With {
                    .Message = ManageMessageId.SetPasswordSuccess
                })
            End If
            AddErrors(result)
        End If

        ' Se si è arrivati a questo punto, significa che si è verificato un errore, rivisualizzare il form
        Return View(model)
    End Function
    '
    ' GET: /Manage/ManageLogins
    Public Async Function ManageLogins(message As System.Nullable(Of ManageMessageId)) As Task(Of ActionResult)
        ViewData!StatusMessage = If(message = ManageMessageId.RemoveLoginSuccess, "L'account di accesso esterno è stato rimosso.", If(message = ManageMessageId.[Error], "Si è verificato un errore.", ""))
        Dim userInfo = Await UserManager.FindByIdAsync(User.Identity.GetUserId())
        If userInfo Is Nothing Then
            Return View("Error")
        End If
        Dim userLogins = Await UserManager.GetLoginsAsync(User.Identity.GetUserId())
        Dim otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(Function(auth) userLogins.All(Function(ul) auth.AuthenticationType <> ul.LoginProvider)).ToList()
        ViewData!ShowRemoveButton = userInfo.PasswordHash IsNot Nothing OrElse userLogins.Count > 1
        Return View(New ManageLoginsViewModel() With {
            .CurrentLogins = userLogins,
            .OtherLogins = otherLogins
        })
    End Function

    '
    ' POST: /Manage/LinkLogin
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function LinkLogin(provider As String) As ActionResult
        ' Richiedere un reindirizzamento al provider di accesso esterno per collegare un account di accesso per l'utente corrente
        Return New AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId())
    End Function

    '
    ' GET: /Manage/LinkLoginCallback
    Public Async Function LinkLoginCallback() As Task(Of ActionResult)
        Dim loginInfo = Await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId())
        If loginInfo Is Nothing Then
            Return RedirectToAction("ManageLogins", New With {
                .Message = ManageMessageId.[Error]
            })
        End If
        Dim result = Await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login)
        Return If(result.Succeeded, RedirectToAction("ManageLogins"), RedirectToAction("ManageLogins", New With {
            .Message = ManageMessageId.[Error]
        }))
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso _userManager IsNot Nothing Then
            _userManager.Dispose()
            _userManager = Nothing
        End If

        MyBase.Dispose(disposing)
    End Sub

#Region "Helper"
    ' Usato per la protezione XSRF durante l'aggiunta di account di accesso esterni
    Private Const XsrfKey As String = "XsrfId"

    Private ReadOnly Property AuthenticationManager() As IAuthenticationManager
        Get
            Return HttpContext.GetOwinContext().Authentication
        End Get
    End Property

    Private Sub AddErrors(result As IdentityResult)
        For Each [error] In result.Errors
            ModelState.AddModelError("", [error])
        Next
    End Sub

    Private Function HasPassword() As Boolean
        Dim userInfo = UserManager.FindById(User.Identity.GetUserId())
        If userInfo IsNot Nothing Then
            Return userInfo.PasswordHash IsNot Nothing
        End If
        Return False
    End Function

    Private Function HasPhoneNumber() As Boolean
        Dim userInfo = UserManager.FindById(User.Identity.GetUserId())
        If userInfo IsNot Nothing Then
            Return userInfo.PhoneNumber IsNot Nothing
        End If
        Return False
    End Function

    Public Enum ManageMessageId
        AddPhoneSuccess
        ChangePasswordSuccess
        SetTwoFactorSuccess
        SetPasswordSuccess
        RemoveLoginSuccess
        RemovePhoneSuccess
        [Error]
    End Enum

#End Region
End Class
