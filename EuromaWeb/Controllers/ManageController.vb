﻿Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security

<Authorize>
Public Class ManageController
    Inherits Controller
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