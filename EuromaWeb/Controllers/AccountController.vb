Imports System.Data.Entity
Imports System.Globalization
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

<Authorize>
Public Class AccountController
    Inherits Controller

    Private appctx As New ApplicationDbContext
    Private db As New EuromaModels

    Private _signInManager As ApplicationSignInManager
    Private _userManager As ApplicationUserManager

    Public Sub New()
    End Sub

    Public Sub New(appUserMan As ApplicationUserManager, signInMan As ApplicationSignInManager)
        UserManager = appUserMan
        SignInManager = signInMan
    End Sub

    Public Property SignInManager() As ApplicationSignInManager
        Get
            Return If(_signInManager, HttpContext.GetOwinContext().[Get](Of ApplicationSignInManager)())
        End Get
        Private Set
            _signInManager = Value
        End Set
    End Property

    Public Property UserManager() As ApplicationUserManager
        Get
            Return If(_userManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
        End Get
        Private Set
            _userManager = Value
        End Set
    End Property

    <Authorize(Roles:="Commerciale_Admin, Admin")>
    Function _NuovoUtente() As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpID = User.Identity.GetUserId()
            OpName = User.Identity.GetUserName()
            ViewBag.Ruolo = New SelectList(appctx.Roles, "Name", "Name")
            Return PartialView()
        Catch ex As Exception

            db.SaveChanges()
        End Try
        Return New EmptyResult
    End Function
    Function _EditUtente() As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpID = User.Identity.GetUserId()
            OpName = User.Identity.GetUserName()
            Dim store = New UserStore(Of ApplicationUser)(appctx)
            Dim manager = New UserManager(Of ApplicationUser)(store)
            Dim currentuser As ApplicationUser = manager.FindById(User.Identity.GetUserId())
            If IsNothing(currentuser.Profile) Then
                Return PartialView(New ProfileViewModel With {.Soprannome = "", .PWD_Email = "", .Firma = "", .Percorso_Ricerca = ""})
            Else
                Dim Password = Decrypter(currentuser.Profile.PWD_Email)
                Dim Firma = currentuser.Profile.Firma
                Dim Soprannome = currentuser.Profile.Soprannome
                Dim Percorso_Ricerca = currentuser.Profile.Percorso_Ricerca
                Dim Email = currentuser.Profile.NotificheViaMail
                Return PartialView(New ProfileViewModel With {.Soprannome = Soprannome, .PWD_Email = Password, .Firma = Firma, .Percorso_Ricerca = Percorso_Ricerca, .Email = Email})
            End If

        Catch ex As Exception
            db.Log.Add(New Log With {
                                 .Livello = TipoLogLivello.Warning,
                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                 .Messaggio = "Errore edit utente " & vbNewLine & ex.Message,
                                 .Dati = "",
                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}})
            db.SaveChanges()
        End Try
        Return New EmptyResult
    End Function

    <HttpPost()>
    <ValidateAntiForgeryToken()>
    Function _EditUtente(Soprannome As String, Password As String, Firma As String, Percorso_Ricerca As String, Email As Boolean) As JsonResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpID = User.Identity.GetUserId()
            OpName = User.Identity.GetUserName()
            Firma = Firma.Replace("&lt;", "<").Replace("&gt;", ">")

            Dim store = New UserStore(Of ApplicationUser)(appctx)
            Dim manager = New UserManager(Of ApplicationUser)(store)
            Dim currentuser As ApplicationUser = manager.FindById(User.Identity.GetUserId())
            If IsNothing(currentuser.Profile) Then
                currentuser.Profile = New Profile
            End If
            If Not IsNothing(Soprannome) Then
                If Not currentuser.Profile.Soprannome = Soprannome Then
                    currentuser.Profile.Soprannome = Soprannome
                End If
            End If
            If Not IsNothing(Email) Then
                If Not currentuser.Profile.NotificheViaMail = Email Then
                    currentuser.Profile.NotificheViaMail = Email
                End If
            End If
            If Not IsNothing(Percorso_Ricerca) Then
                If Not currentuser.Profile.Percorso_Ricerca = Percorso_Ricerca Then
                    currentuser.Profile.Percorso_Ricerca = Percorso_Ricerca
                End If
            End If
            If Not IsNothing(Password) Then
                If Not currentuser.Profile.PWD_Email = TextEncrypt(Password) Then
                    currentuser.Profile.PWD_Email = TextEncrypt(Password)
                End If
            End If
            If Not IsNothing(Firma) Then
                If Not currentuser.Profile.Firma = Firma Then
                    currentuser.Profile.Firma = Firma
                End If
            End If
            manager.Update(currentuser)
            db.SaveChanges()
            Return Json(New With {.ok = True, .message = "Modifiche eseguite correttamente!"})
        Catch ex As Exception
            For Each e In db.ChangeTracker.Entries().Where(Function(x) x.State = EntityState.Modified)
                e.State = EntityState.Unchanged
            Next
            db.Log.Add(New Log With {
                                   .Livello = TipoLogLivello.Warning,
                                   .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                   .Messaggio = "Errore Edit utente " & vbNewLine & ex.Message,
                                   .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.nome = OpName}),
                                   .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}})
            db.SaveChanges()
            Return Json(New With {.ok = False, .message = "Errore: Impossibile effettuare le modifiche!"})
        End Try
        Return Json(New With {.ok = False, .message = "Errore: Impossibile effettuare le modifiche!"})
    End Function

    <Authorize(Roles:="Commerciale_Admin, Admin")>
    <HttpPost()>
    <ValidateAntiForgeryToken()>
    Function _NuovoUtente(Nome As String, Email As String, Password As String, Ruolo As String) As JsonResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpID = User.Identity.GetUserId()
            OpName = User.Identity.GetUserName()

            If IsNothing(Ruolo) Then
                Return Json(New With {.ok = False, .message = "Impostare un ruolo per l'utente, Operazione Annullata!"})
            End If
            If IsNothing(Password) OrElse Password.Length < 6 Then
                Return Json(New With {.ok = False, .message = "La Password deve essere almeno di 6 caratteri, Operazione Annullata!"})
            End If


            Dim s = New UserStore(Of ApplicationUser)(appctx)
            Dim m = New ApplicationUserManager(s)
            m.UserValidator = New UserValidator(Of ApplicationUser)(m) With {
                        .AllowOnlyAlphanumericUserNames = False,
                        .RequireUniqueEmail = False
                    }

            ' Configurare la logica di convalida per le password
            m.PasswordValidator = New PasswordValidator With {
                        .RequiredLength = 6,
                        .RequireNonLetterOrDigit = False,
                        .RequireDigit = False,
                        .RequireLowercase = False,
                        .RequireUppercase = False
                    }

            ' Configurare le impostazioni predefinite per il blocco dell'utente
            m.UserLockoutEnabledByDefault = True
            m.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
            m.MaxFailedAccessAttemptsBeforeLockout = 10
            Dim p = New Profile With {.Soprannome = "", .Firma = "", .NotificheViaMail = False, .Percorso_Ricerca = "", .PWD_Email = "", .CMT = False, .Drill = False, .ISA = False, .MPA = True, .UNI = False}
            Dim u = New ApplicationUser With {.UserName = Nome, .Email = Email, .Profile = p}
            Dim result As IdentityResult = m.Create(u, Password)

            If Not result.Succeeded Then
                Return Json(New With {.ok = False, .message = "Errore Aggiunta Utente! " & String.Join(", ", result.Errors)})
            End If

            result = m.AddToRole(u.Id, Ruolo)
            If Not result.Succeeded Then
                Return Json(New With {.ok = False, .message = "Errore Aggiunta Ruolo! " & String.Join(", ", result.Errors)})
            End If

            Return Json(New With {.ok = True, .message = "Utente Aggiunto Correttamente!"})
        Catch ex As Exception
            db.SaveChanges()
        End Try

        Return Json(New With {.ok = False, .message = "Errore: Aggiunta Utente Annullata!"})
    End Function

    '
    ' GET: /Account/Login
    <AllowAnonymous>
    Public Function Login(returnUrl As String) As ActionResult
        If User.Identity.IsAuthenticated() Then
            Return RedirectToAction("Index", "Home")
        End If
        ViewData!ReturnUrl = returnUrl
        Return View()
    End Function

    '
    ' POST: /Account/Login
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function Login(model As LoginViewModel, returnUrl As String) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If

        ' Questa opzione non calcola il numero di tentativi di accesso non riusciti per il blocco dell'account
        ' Per abilitare il conteggio degli errori di password per attivare il blocco, impostare shouldLockout := True
        Dim result = Await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout:=False)
        Select Case result
            Case SignInStatus.Success
                Return RedirectToLocal(returnUrl)
            Case SignInStatus.LockedOut
                Return View("Lockout")
            Case SignInStatus.RequiresVerification
                Return RedirectToAction("SendCode", New With {
                    returnUrl,
                    model.RememberMe
                })
            Case Else
                ModelState.AddModelError("", "Tentativo di accesso non valido.")
                Return View(model)
        End Select
    End Function

    '
    ' GET: /Account/VerifyCode
    <AllowAnonymous>
    Public Async Function VerifyCode(provider As String, returnUrl As String, rememberMe As Boolean) As Task(Of ActionResult)
        ' Impostare come condizione che l'utente abbia già eseguito l'accesso con nome utente/password o account di accesso esterno
        If Not Await SignInManager.HasBeenVerifiedAsync() Then
            Return View("Error")
        End If
        Return View(New VerifyCodeViewModel() With {
            .Provider = provider,
            .ReturnUrl = returnUrl,
            .RememberMe = rememberMe
        })
    End Function

    '
    ' POST: /Account/VerifyCode
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function VerifyCode(model As VerifyCodeViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If

        ' La parte di codice seguente protegge i codici di autenticazione a due fattori dagli attacchi di forza bruta. 
        ' Se un utente immette codici non corretti in un intervallo di tempo specificato, l'account dell'utente 
        ' viene bloccato per un intervallo di tempo specificato. 
        ' Si possono configurare le impostazioni per il blocco dell'account in IdentityConfig
        Dim result = Await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:=model.RememberMe, rememberBrowser:=model.RememberBrowser)
        Select Case result
            Case SignInStatus.Success
                Return RedirectToLocal(model.ReturnUrl)
            Case SignInStatus.LockedOut
                Return View("Lockout")
            Case Else
                ModelState.AddModelError("", "Codice non valido.")
                Return View(model)
        End Select
    End Function

    '
    ' GET: /Account/Register
    <AllowAnonymous>
    Public Function Register() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/Register
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function Register(model As RegisterViewModel) As Task(Of ActionResult)
        If ModelState.IsValid Then
            Dim user = New ApplicationUser() With {
                .UserName = model.Email,
                .Email = model.Email
            }
            Dim result = Await UserManager.CreateAsync(user, model.Password)
            If result.Succeeded Then
                Await SignInManager.SignInAsync(user, isPersistent:=False, rememberBrowser:=False)

                ' Per altre informazioni su come abilitare la conferma dell'account e la reimpostazione della password, vedere https://go.microsoft.com/fwlink/?LinkID=320771
                ' Inviare un messaggio di posta elettronica con questo collegamento
                ' Dim code = Await UserManager.GenerateEmailConfirmationTokenAsync(user.Id)
                ' Dim callbackUrl = Url.Action("ConfirmEmail", "Account", New With { .userId = user.Id, code }, protocol := Request.Url.Scheme)
                ' Await UserManager.SendEmailAsync(user.Id, "Conferma account", "Per confermare l'account, fare clic <a href=""" & callbackUrl & """>qui</a>")

                Return RedirectToAction("Index", "Home")
            End If
            AddErrors(result)
        End If

        ' Se si è arrivati a questo punto, significa che si è verificato un errore, rivisualizzare il form
        Return View(model)
    End Function

    '
    ' GET: /Account/ConfirmEmail
    <AllowAnonymous>
    Public Async Function ConfirmEmail(userId As String, code As String) As Task(Of ActionResult)
        If userId Is Nothing OrElse code Is Nothing Then
            Return View("Error")
        End If
        Dim result = Await UserManager.ConfirmEmailAsync(userId, code)
        Return View(If(result.Succeeded, "ConfirmEmail", "Error"))
    End Function

    '
    ' GET: /Account/ForgotPassword
    <AllowAnonymous>
    Public Function ForgotPassword() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/ForgotPassword
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function ForgotPassword(model As ForgotPasswordViewModel) As Task(Of ActionResult)
        If ModelState.IsValid Then
            Dim user = Await UserManager.FindByNameAsync(model.Email)
            If user Is Nothing OrElse Not (Await UserManager.IsEmailConfirmedAsync(user.Id)) Then
                ' Non rivelare che l'utente non esiste o non è confermato
                Return View("ForgotPasswordConfirmation")
            End If
            ' Per altre informazioni su come abilitare la conferma dell'account e la reimpostazione della password, vedere https://go.microsoft.com/fwlink/?LinkID=320771
            ' Inviare un messaggio di posta elettronica con questo collegamento
            ' Dim code = Await UserManager.GeneratePasswordResetTokenAsync(user.Id)
            ' Dim callbackUrl = Url.Action("ResetPassword", "Account", New With { .userId = user.Id, code }, protocol := Request.Url.Scheme)
            ' Await UserManager.SendEmailAsync(user.Id, "Reimposta password", "Per reimpostare la password, fare clic <a href=""" & callbackUrl & """>qui</a>")
            ' Return RedirectToAction("ForgotPasswordConfirmation", "Account")
        End If

        ' Se si è arrivati a questo punto, significa che si è verificato un errore, rivisualizzare il form
        Return View(model)
    End Function

    '
    ' GET: /Account/ForgotPasswordConfirmation
    <AllowAnonymous>
    Public Function ForgotPasswordConfirmation() As ActionResult
        Return View()
    End Function

    '
    ' GET: /Account/ResetPassword
    <AllowAnonymous>
    Public Function ResetPassword(code As String) As ActionResult
        Return If(code Is Nothing, View("Error"), View())
    End Function

    '
    ' POST: /Account/ResetPassword
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function ResetPassword(model As ResetPasswordViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View(model)
        End If
        Dim user = Await UserManager.FindByNameAsync(model.Email)
        If user Is Nothing Then
            ' Non rivelare che l'utente non esiste
            Return RedirectToAction("ResetPasswordConfirmation", "Account")
        End If
        Dim result = Await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password)
        If result.Succeeded Then
            Return RedirectToAction("ResetPasswordConfirmation", "Account")
        End If
        AddErrors(result)
        Return View()
    End Function

    '
    ' GET: /Account/ResetPasswordConfirmation
    <AllowAnonymous>
    Public Function ResetPasswordConfirmation() As ActionResult
        Return View()
    End Function

    '
    ' POST: /Account/ExternalLogin
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Function ExternalLogin(provider As String, returnUrl As String) As ActionResult
        ' Richiedere un reindirizzamento al provider di accesso esterno
        Return New ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", New With {
            returnUrl
        }))
    End Function

    '
    ' GET: /Account/SendCode
    <AllowAnonymous>
    Public Async Function SendCode(returnUrl As String, rememberMe As Boolean) As Task(Of ActionResult)
        Dim userId = Await SignInManager.GetVerifiedUserIdAsync()
        If userId Is Nothing Then
            Return View("Error")
        End If
        Dim userFactors = Await UserManager.GetValidTwoFactorProvidersAsync(userId)
        Dim factorOptions = userFactors.[Select](Function(purpose) New SelectListItem() With {
            .Text = purpose,
            .Value = purpose
        }).ToList()
        Return View(New SendCodeViewModel() With {
            .Providers = factorOptions,
            .ReturnUrl = returnUrl,
            .RememberMe = rememberMe
        })
    End Function

    '
    ' POST: /Account/SendCode
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function SendCode(model As SendCodeViewModel) As Task(Of ActionResult)
        If Not ModelState.IsValid Then
            Return View()
        End If

        ' Generare il token e inviarlo
        If Not Await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider) Then
            Return View("Error")
        End If
        Return RedirectToAction("VerifyCode", New With {
            .Provider = model.SelectedProvider,
            model.ReturnUrl,
            model.RememberMe
        })
    End Function

    '
    ' GET: /Account/ExternalLoginCallback
    <AllowAnonymous>
    Public Async Function ExternalLoginCallback(returnUrl As String) As Task(Of ActionResult)
        Dim loginInfo = Await AuthenticationManager.GetExternalLoginInfoAsync()
        If loginInfo Is Nothing Then
            Return RedirectToAction("Login")
        End If

        ' Se l'utente ha già un account, consentire l'accesso dell'utente a questo provider di accesso esterno
        Dim result = Await SignInManager.ExternalSignInAsync(loginInfo, isPersistent:=False)
        Select Case result
            Case SignInStatus.Success
                Return RedirectToLocal(returnUrl)
            Case SignInStatus.LockedOut
                Return View("Lockout")
            Case SignInStatus.RequiresVerification
                Return RedirectToAction("SendCode", New With {
                    returnUrl,
                    .RememberMe = False
                })
            Case Else
                ' Se l'utente non ha un account, chiedere all'utente di crearne uno
                ViewData!ReturnUrl = returnUrl
                ViewData!LoginProvider = loginInfo.Login.LoginProvider
                Return View("ExternalLoginConfirmation", New ExternalLoginConfirmationViewModel() With {
                    .Email = loginInfo.Email
                })
        End Select
    End Function

    '
    ' POST: /Account/ExternalLoginConfirmation
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function ExternalLoginConfirmation(model As ExternalLoginConfirmationViewModel, returnUrl As String) As Task(Of ActionResult)
        If User.Identity.IsAuthenticated Then
            Return RedirectToAction("Index", "Manage")
        End If

        If ModelState.IsValid Then
            ' Recuperare le informazioni sull'utente dal provider di accesso esterno
            Dim info = Await AuthenticationManager.GetExternalLoginInfoAsync()
            If info Is Nothing Then
                Return View("ExternalLoginFailure")
            End If
            Dim userInfo = New ApplicationUser() With {
                .UserName = model.Email,
                .Email = model.Email
            }
            Dim result = Await UserManager.CreateAsync(userInfo)
            If result.Succeeded Then
                result = Await UserManager.AddLoginAsync(userInfo.Id, info.Login)
                If result.Succeeded Then
                    Await SignInManager.SignInAsync(userInfo, isPersistent:=False, rememberBrowser:=False)
                    Return RedirectToLocal(returnUrl)
                End If
            End If
            AddErrors(result)
        End If

        ViewData!ReturnUrl = returnUrl
        Return View(model)
    End Function

    '
    ' POST: /Account/LogOff
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function LogOff() As ActionResult
        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
        Return RedirectToAction("Index", "Home")
    End Function

    '
    ' GET: /Account/ExternalLoginFailure
    <AllowAnonymous>
    Public Function ExternalLoginFailure() As ActionResult
        Return View()
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If _userManager IsNot Nothing Then
                _userManager.Dispose()
                _userManager = Nothing
            End If
            If _signInManager IsNot Nothing Then
                _signInManager.Dispose()
                _signInManager = Nothing
            End If
        End If

        MyBase.Dispose(disposing)
    End Sub
    Public Shared Function Decrypter(ByVal Text As String) As String
        Try
            Dim bytesBuff As Byte() = Convert.FromBase64String(Text)
            Using aes__1 As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()
                Dim crypto As New System.Security.Cryptography.Rfc2898DeriveBytes("11 23 91 b7 51 b5 ee b5 86 fd e9 1e 44 20 3a 2a", {&H45, &H77, &H89, &H4E, &H23, &H2D, &H45, &H44, &H86, &H55, &H84, &H95, &H36})
                aes__1.Key = crypto.GetBytes(32)
                aes__1.IV = crypto.GetBytes(16)
                aes__1.Padding = System.Security.Cryptography.PaddingMode.None
                Using mStream As New System.IO.MemoryStream()
                    Using cStream As New System.Security.Cryptography.CryptoStream(mStream, aes__1.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
                        cStream.Write(bytesBuff, 0, bytesBuff.Length)
                        cStream.Close()
                    End Using
                    Text = Encoding.Unicode.GetString(mStream.ToArray())
                End Using
            End Using
        Catch ex As Exception
        End Try


        Return Text
    End Function


    Public Shared Function TextEncrypt(ByVal Text As String) As String
        Try
            Dim bytesBuff As Byte() = Encoding.Unicode.GetBytes(Text)
            Using aes__1 As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()
                Dim crypto As New System.Security.Cryptography.Rfc2898DeriveBytes("dskjlsfkojhsfkhkjoieewfiduhpuih4785.reiuyt", {&H45, &H77, &H89, &H4E, &H23, &H2D, &H45, &H44, &H86, &H55, &H84, &H95, &H36})
                aes__1.Key = crypto.GetBytes(32)
                aes__1.Padding = System.Security.Cryptography.PaddingMode.None
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

    Private Function RedirectToLocal(returnUrl As String) As ActionResult
        If Url.IsLocalUrl(returnUrl) Then
            Return Redirect(returnUrl)
        End If
        Return RedirectToAction("Index", "Home")
    End Function

    Friend Class ChallengeResult
        Inherits HttpUnauthorizedResult
        Public Sub New(provider As String, redirectUri As String)
            Me.New(provider, redirectUri, Nothing)
        End Sub

        Public Sub New(provider As String, redirect As String, user As String)
            LoginProvider = provider
            RedirectUri = redirect
            UserId = user
        End Sub

        Public Property LoginProvider As String
        Public Property RedirectUri As String
        Public Property UserId As String

        Public Overrides Sub ExecuteResult(context As ControllerContext)
            Dim properties = New AuthenticationProperties() With {
                .RedirectUri = RedirectUri
            }
            If UserId IsNot Nothing Then
                properties.Dictionary(XsrfKey) = UserId
            End If
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider)
        End Sub
    End Class
#End Region
End Class
