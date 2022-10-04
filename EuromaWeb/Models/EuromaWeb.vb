' È possibile aggiungere dati del profilo per l'utente aggiungendo altre proprietà alla classe ApplicationUser. Per altre informazioni, vedere https://go.microsoft.com/fwlink/?LinkID=317594.
Imports System.ComponentModel.DataAnnotations

Public Class ClienteViewModel
    Public Property Id() As String
    Public Property Nome1 As String
    Public Property Nome2 As String
    Public Property Via As String
    Public Property Citta As String
    Public Property Nazione As String
    Public Property CAP As String
    Public Property Tel As String
    Public Property Fax As String
    Public Property Email As String
    Public Property Divisione As String
    Public Property Agente As String
    Public Property Provincia As String
    Public Property HaFatturato As Boolean
    Public Property Regione As String
    Public Property RaggruppamentoDue As String
    Public Property AlnusCode As String
End Class
Public Class SlotListViewModel
    Public Property Id As Integer
    Public Property Scaffale_Slot As String
End Class
Public Class GanttObjViewModel
    Public Property name As String
    Public Property desc As String
    Public Property values As GanttValueViewModel
End Class
Public Class GanttValueViewModel
    Public Property da As DateTime
    Public Property a As DateTime
    Public Property label As String
End Class
Public Class ODPProduzioneViewModel
    Public Property ODP As String
    Public Property OC As String
    Public Property Cliente As String
    Public Property Articolo As String
    Public Property Desc_Art As String
    Public Property Data_Inizio_Attività As Date
    Public Property Completato As Boolean
    Public Property ListaAttivita As List(Of FasiOC)
End Class
        Public Class PrevOrdinatoViewModel
    Public Property DataInizio As String
    Public Property DataFine As String
    Public Property nSettimana As String
    Public Property Totale As Decimal
    Public Property Conteggio As Integer
End Class
Public Class PezzoViewModel
    Public Property CodArt As String
    Public Property Descrizione As String
    Public Property TPRCOD As String
    Public Property Giacenza As Integer
    Public Property ArtUltimo As Double
    Public Property ArtStandard As Integer
    Public Property Diff As String
End Class
Public Class StoricoViewModel
    Public Property Year As Integer
    Public Property Costo_Globale As Decimal = 0.00D
    Public Property Costo_Materiali As Decimal = 0.00D
    Public Property Costo_Macchina As Decimal = 0.00D
    Public Property Costo_Manodopera_Est As Decimal = 0.00D
    Public Property Costo_Attrezzaggio As Decimal = 0.00D
    Public Property Costo_Manodopera_Int As Decimal = 0.00D
    Public Property Manodopera As Decimal = 0.00D
End Class
Public Class Fatturato
    Public Property Drill As New Dictionary(Of String, Divisione)
    Public Property MPA As New Dictionary(Of String, Divisione)
    Public Property Unistand As New Dictionary(Of String, Divisione)
    Public Property CMT As New Dictionary(Of String, Divisione)
    Public Property ISA As New Dictionary(Of String, Divisione)
    Public Property RicambiERevisioni As New Dictionary(Of String, Divisione)
    Public Property ImballiEtrasporti As New Dictionary(Of String, Divisione)
    Public Property Altro As Decimal
End Class
Public Class Ordinato
    Public Property Drill As New Dictionary(Of String, DivisioneOrdinato)
    Public Property MPA As New Dictionary(Of String, DivisioneOrdinato)
    Public Property Unistand As New Dictionary(Of String, DivisioneOrdinato)
    Public Property CMT As New Dictionary(Of String, DivisioneOrdinato)
    Public Property ISA As New Dictionary(Of String, DivisioneOrdinato)
    Public Property Euroma As New Dictionary(Of String, DivisioneOrdinato)
End Class
Public Class OverviewOrdineViewModel
    Public Property id As Integer
    Public Property OC As String
    Public Property Stato_Generale As Integer
    Public Property ListArt As List(Of ArticoliPerOC)
    Public Property Timeline As List(Of StoricoOC)
    Public Property NoteList As List(Of NotePerOC)
    Public Property Documenti As List(Of DocumentiPerOC)
    Public Property ListaOP As List(Of OrdineDiProduzioneViewModel)
End Class
Public Class OrdineDiProduzioneViewModel
    Public Property Articolo As String
    Public Property OP As String
    Public Property ListaFasi As List(Of FaseViewModel)
    Public Property PresenteInDbEsterno As Boolean
End Class
Public Class FaseViewModel
    Public Property NumFase As String
    Public Property DescFase As String
    Public Property Completato As Boolean
End Class
Public Class Divisione
    Public Property Italia As Integer
    Public Property Estero As Integer
End Class
Public Class DivisioneOrdinato
    Public Property Italia_Nuovo As Integer
    Public Property Estero_Nuovo As Integer

    Public Property Italia_Ricambio As Integer
    Public Property Estero_Ricambio As Integer
End Class
Public Class OverviewViewModel
    Public Property Matricola As Integer
    Public Property Macchina As String
    Public Property Data_Inizio As DateTime
    Public Property Data_Fine As DateTime
    Public Property OreUomo As Double
    Public Property AutoInc As Integer
    Public Property Nomina As String
    Public Property OP As String
End Class
Public Class OverviewProduzioneViewModel
    'Public Property Dipendente As String
    'Public Property ListaLavori As List(Of String)
    'Public Property 
End Class
Public Class NotaViewModel
    Public Property Id As Integer
    Public Property nota As String
End Class
Public Class AccettazioneUCViewModel
    <Key>
    Public Property Id As Integer
    Public Property OC As String
    Public Property File As String
    Public Property Cartella As String
    Public Property OperatoreInsert As String
    Public Property EmailOperatoreInsert As String
    Public Property OperatoreAccettazione As String
    Public Property DataCreazione As DateTime
    Public Property DataAccettazione As Nullable(Of DateTime)
    Public Property Accettato As Stato_UC
    Public Property Note As String
    Public Property EmailInviata As Boolean
    Public Property SenttoUC As Boolean
    Public Property ListaArt As List(Of ArticoliPerOC)
    Public Property ListOfNote As List(Of NotePerOC)
    Public Property ListOfDocumenti As List(Of DocumentiPerOC)
    Public Property Priorita As Stato_Priorita
    Public Property DataPrevistaConsegna As DateTime
    Public Property Brand As String
    Public Property PrezzoMaggiorato As Boolean
End Class