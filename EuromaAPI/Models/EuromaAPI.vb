Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class EuromaAPI
    Inherits DbContext
    Public Sub New()
        MyBase.New("name=EuromaAPI")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)

        'modelBuilder.Entity(Of Contabilizzatore)().[Property](Function(p) p.FattoreK).HasPrecision(12, 6)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Overridable Property Utenti() As DbSet(Of Utenti)
    Public Overridable Property Macchine() As DbSet(Of Macchine)
    Public Overridable Property Lavori() As DbSet(Of Lavori)

End Class

<Table("Utenti")>
Public Class Utenti
    <Key>
    Public Property Id As Integer
    Public Property Nome_Utente As String
    Public Property Password As String
    Public Property Email As String
    Public Property Soprannome As String

End Class

<Table("Macchine")>
Public Class Macchine
    <Key>
    Public Property Id As Integer
    Public Property Cod_Macchina As String
    Public Property Desc_Macchina As String
End Class

<Table("Lavori")>
Public Class Lavori
    <Key>
    Public Property Id As Integer
    Public Property Cod_Macchina As String
    Public Property Cod_Lavorazione As String
    Public Property Desc_Lavorazione As String
    Public Property DateTime_Inizio As DateTime
    Public Property DateTime_Fine As DateTime
    Public Property Totale_Time As Decimal
End Class
