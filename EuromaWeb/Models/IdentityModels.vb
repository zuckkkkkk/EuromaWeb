Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin

' È possibile aggiungere dati del profilo per l'utente aggiungendo altre proprietà alla classe ApplicationUser. Per altre informazioni, vedere https://go.microsoft.com/fwlink/?LinkID=317594.
Public Class ApplicationUser
    Inherits IdentityUser
    Public Async Function GenerateUserIdentityAsync(manager As UserManager(Of ApplicationUser)) As Task(Of ClaimsIdentity)
        ' Tenere presente che il valore di authenticationType deve corrispondere a quello definito in CookieAuthenticationOptions.AuthenticationType
        Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
        ' Aggiungere qui i reclami utente personalizzati
        Return userIdentity
    End Function

    Public Overridable Property Profile As Profile

End Class


Public Class ApplicationDbContext
    Inherits IdentityDbContext(Of ApplicationUser)
    Public Sub New()
        MyBase.New("DefaultConnection", throwIfV1Schema:=False)
    End Sub

    Public Shared Function Create() As ApplicationDbContext
        Return New ApplicationDbContext()
    End Function

    Public Overridable Property Profiles As DbSet(Of Profile)
    Public Overridable Property Device As DbSet(Of AspNetUserDevice)
    Public Overridable Property UserLicenze As DbSet(Of UserLicenze)
    Public Overridable Property AspNetUserExchangeLicenseTable As DbSet(Of AspNetUserExchangeLicenseTable)
End Class

<Table("AspNetProfiles")>
Public Class Profile
    <Key>
    Public Property Id As Integer
    Public Property PWD_Email As String
    Public Property Firma As String
    Public Property Percorso_Ricerca As String
    Public Property Soprannome As String
    Public Property NotificheViaMail As Boolean
    Public Property MPA As Boolean
    Public Property Drill As Boolean
    Public Property CMT As Boolean
    Public Property ISA As Boolean
    Public Property UNI As Boolean
End Class
<Table("AspNetUserDevice")>
Public Class AspNetUserDevice
    <Key>
    Public Property Id As Integer
    Public Property IdRuolo As Integer 'Se a zero utente se 1 computer
    Public Property NomeDispositivo As String
    Public Property DescrizioneDispositivo As String
    Public Property IPDispositivo As String
End Class
<Table("AspNetUserExchangeLicenseTable")>
Public Class AspNetUserExchangeLicenseTable
    <Key>
    Public Property Id As Integer
    Public Property IdEsternoUtente As String
    Public Property IdEsternoLicenza As Integer
End Class
<Table("AspNetUserLicenze")>
Public Class UserLicenze
    <Key>
    Public Property Id As Integer
    Public Property TypeLicenza As Boolean
    Public Property NomeLicenza As String
    Public Property DescrizioneLicenza As String
    Public Property DataInizio As DateTime
    Public Property DataRinnovo As DateTime
    Public Property DurataLicenza As Integer
    Public Property CostoLicenza As Double
    Public Property QtaLicenze As Integer
End Class
