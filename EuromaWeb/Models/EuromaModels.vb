Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class EuromaModels
    Inherits DbContext

    'Il contesto è stato configurato per utilizzare una stringa di connessione 'EbsModels' dal file di configurazione 
    ' dell'applicazione (App.config o Web.config). Per impostazione predefinita, la stringa di connessione è destinata al 
    ' database 'EbsWebBackOffice.EbsModels' nell'istanza di LocalDb. 
    ' 
    ' Per destinarla a un database o un provider di database differente, modificare la stringa di connessione 'EbsModels' 
    ' nel file di configurazione dell'applicazione.
    Public Sub New()
        MyBase.New("name=EuromaModels")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)

        'modelBuilder.Entity(Of Contabilizzatore)().[Property](Function(p) p.FattoreK).HasPrecision(12, 6)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    ' Aggiungere DbSet per ogni tipo di entità che si desidera includere nel modello. Per ulteriori informazioni 
    ' sulla configurazione e sull'utilizzo di un modello Code, vedere http://go.microsoft.com/fwlink/?LinkId=390109.
    ' Public Overridable Property MyEntities() As DbSet(Of MyEntity)

    Public Overridable Property Ordini() As DbSet(Of Ordine)
    Public Overridable Property Progetti() As DbSet(Of Progetto)
    Public Overridable Property Storico_Progetti() As DbSet(Of Storico_Progetto)
    Public Overridable Property Overview() As DbSet(Of Overview)
    Public Overridable Property TempiCAD() As DbSet(Of TempoCAD)
    Public Overridable Property Tempi_Macchina() As DbSet(Of Tempo_Macchina)
    Public Overridable Property AccettazioneUC() As DbSet(Of AccettazioneUC)
    Public Overridable Property ProgettiUT() As DbSet(Of ProgettiUT)
    Public Overridable Property ProgettiProd() As DbSet(Of ProgettiProd)
    Public Overridable Property UserEmail() As DbSet(Of UserEmail)
    Public Overridable Property HelpDesk() As DbSet(Of HelpDesk)
    Public Overridable Property Log() As DbSet(Of Log)
    Public Overridable Property Audit() As DbSet(Of Audit)
    Public Overridable Property Licenze() As DbSet(Of Licenze)
    Public Overridable Property Disegni_MPA() As DbSet(Of Disegni_MPA)
    Public Overridable Property Disegni_MPA_server() As DbSet(Of Disegni_MPA_server)
    Public Overridable Property LavorazioniEsterne() As DbSet(Of LavorazioniEsterne)
    Public Overridable Property ChangeLog() As DbSet(Of ChangeLog)
    Public Overridable Property VisualizzazioneChangeLog() As DbSet(Of VisualizzazioneChangeLog)
    Public Overridable Property ArticoliPerOC() As DbSet(Of ArticoliPerOC)
    Public Overridable Property NotePerOC() As DbSet(Of NotePerOC)
    Public Overridable Property DocumentiPerOC() As DbSet(Of DocumentiPerOC)
    Public Overridable Property DocumentiPerLicenze() As DbSet(Of DocumentiPerLicenze)
    Public Overridable Property StoricoOC() As DbSet(Of StoricoOC)
    Public Overridable Property FasiOC() As DbSet(Of FasiOC)
    Public Overridable Property TempiAttivita() As DbSet(Of TempiAttivita)
    Public Overridable Property ArticoliMagazzino() As DbSet(Of ArticoliMagazzino)
    Public Overridable Property Magazzino() As DbSet(Of Magazzino)
    Public Overridable Property ScaffaliMagazzino() As DbSet(Of ScaffaliMagazzino)
    Public Overridable Property SlotScaffale() As DbSet(Of SlotScaffale)
    Public Overridable Property OrdiniDiProduzione() As DbSet(Of OrdiniDiProduzione)
    Public Overridable Property VisualizzazioneFileNota() As DbSet(Of VisualizzazioneFileNota)
    Public Overridable Property DatiMacchina() As DbSet(Of DatiMacchina)
    Public Overridable Property Macchine() As DbSet(Of Macchine)
End Class
Public Class Disegno_Server_ViewModel
    Public Property Id As Integer
    Public Property Code_Disegno As String
    Public Property Path_File As String
End Class
Public Class OTViewModel
    Public Property OT As String
End Class
Public Class GanttViewModel
    Public Property id As String
    Public Property nome As String
    Public Property inizio As DateTime
    Public Property fine As DateTime
    Public Property completa As Boolean
End Class
Public Class Fatturato_Fatture_list
    Public Property DrillMatic As List(Of Fattura)
    Public Property MPA As List(Of Fattura)
    Public Property Unistand As List(Of Fattura)
    Public Property ISA As List(Of Fattura)
    Public Property CMT As List(Of Fattura)
    Public Property Prestiti As List(Of Fattura)
    Public Property Ricambi_Rev As List(Of Fattura)
End Class
Public Class GestioneMagazzinoViewModel
    Public Property IdMag As Integer
    Public Property CodMag As Integer
    Public Property DescMag As String
    Public Property ListaScaffali As New List(Of GestioneScaffaliViewModel)
End Class
Public Class GestioneScaffaliViewModel
    Public Property NumScaffale As String
    Public Property ListaSlot As New List(Of GestioneSlotViewModel)
End Class
Public Class GestioneSlotViewModel
    Public Property idSlot As Integer
    Public Property CodSlot As String
    Public Property Count As Integer
End Class
Public Class CreazioneArticoloMagazzinoViewModel
    Public Property codArt As String
    Public Property qta As Double
    Public Property Scaffale As Integer
    Public Property Slot As String
End Class
Public Class ODPEstViewModel
    Public Property Id As Integer
    Public Property OP As String
    Public Property Cartella As String
    Public Property OperatoreInsert As String
    Public Property IdOperatoreInsert As String
    Public Property DataCreazione As DateTime
    Public Property Accettato As Stato_Ordine_Di_Produzione_Esterno
    Public Property Priorita As Stato_Priorita
    Public Property DataRichiestaConsegna As Nullable(Of DateTime)
    Public Property tre_dim_Presente As Boolean
    Public Property due_dim_Presente As Boolean
    Public Property note_presenti As Boolean
    Public Property ListOfNote As List(Of NotePerOC)
    Public Property ListOfDocumenti As List(Of DocumentiPerOC)
End Class
Public Class Tempi_Macchina_Viewmodel
    Public Property Id As Integer
    Public Property RequestUser As String
    Public Property Macchina As Macchina_CNF
    Public Property T_Tot As Decimal
End Class
Public Class Progetti_UTViewModel
    Public Property Id As Integer
    Public Property Id_OC As Integer
    Public Property OC_Riferimento As String
    Public Property OperatoreSmistamento As String
    Public Property OperatoreSmistamentoId As String
    Public Property Operatore As String
    Public Property OperatoreId As String
    Public Property DataCreazione As DateTime
    Public Property DataCompletamento As Nullable(Of DateTime)
    Public Property Note As String
    Public Property File As String
    Public Property StatoProgetto As Stato_UT
    Public Property StatoProgettoVal As Integer
    Public Property Flag_Invio_Materiali As Boolean
    Public Property Flag_1 As Boolean
    Public Property Flag_2 As Boolean
    Public Property Flag_3 As Boolean
    Public Property Priorita As Stato_Priorita
    Public Property DataRichiestaConsegna As Nullable(Of DateTime)
End Class
Public Class ProgettiUT_Operatore
    <Key>
    Public Property Id As Integer
    Public Property OC_Riferimento As String
    Public Property OperatoreSmistamento As String
    Public Property OperatoreSmistamentoId As String
    Public Property Operatore As String
    Public Property OperatoreId As String
    Public Property DataCreazione As DateTime
    Public Property DataCompletamento As Nullable(Of DateTime)
    Public Property Note As String
    Public Property File As String
    Public Property StatoProgetto As Stato_UT_Operatore
    Public Property Flag_Invio_Materiali As Boolean
    Public Property Flag_1 As Boolean
    Public Property Flag_2 As StatoCheck
    Public Property Flag_3 As Boolean
    Public Property Flag_4 As StatoCheck
    Public Property DataRetroattiva As DateTime
End Class
Public Class LicenzaViewModel
    Public Property Id As Integer
    Public Property Nome_Licenza As String
    Public Property Tipologia_Licenza As Tipo_Licenza
    Public Property StartDate As DateTime
    Public Property Tipologia_Rinnovo As Tipo_Rinnovo_Licenza
    Public Property Utente_Nome As String
    Public Property Active As Boolean
End Class
Public Class OrdineViewModel
    Public Property Anno As Integer
    Public Property Mese As Mese
    Public Property Tipo_Ordine As Tipo_Ordine
    Public Property tCharge As Decimal
End Class
Public Class OrdineListaViewmodel
    Public Property Lista As List(Of OrdineViewModel)
End Class
Public Class MagazzinoViewModel
    Public Property CodMagazzino As String
End Class
Public Class DisegnoViewModel
    Public Property CodDisegno As String
End Class
Public Class DisegnoServerViewModel
    Public Property Id As Integer
    Public Property Name As String
    Public Property Path As String
    Public Property ext As String
End Class
Public Class DisegnoMPAViewModel
    Public Property Id As Integer
    Public Property Cod_art As String
    Public Property Campo1 As String
    Public Property Campo2 As String
    Public Property Campo3 As String
    Public Property Campo4 As String
    Public Property Campo5 As String
    Public Property Desc_Alnus As String
End Class
Public Class ArticoloMagazzinoViewModel
    Public Property Posizione As String
    Public Property Articolo As String
    Public Property Desc_Articolo As String
    Public Property Quantita As String
End Class
Public Class ArtViewModel
    Public Property Articolo As String
    Public Property Quantita As String
End Class
Public Class AlnusdescViewModel
    Public Property Articolo As String
    Public Property Descrizione As String
End Class
Public Class ClienteAnagraficaViewModel
    Public Property CodCli As String
    Public Property ragSoc As String
    Public Property Nazione As String
    Public Property Regione As String
    Public Property Provincia As String
    Public Property Tipo As String
    Public Property Mail As String
    Public Property Drillmatic As String
    Public Property MPA As String
    Public Property ISA As String
    Public Property CMT As String
    Public Property Unistand As String
    Public Property Euroma As String
End Class
Public Class EmailViewModel
    Public Property Id As String
    Public Property Privacy_Percorso As String
    Public Property File_Percorso As String
    Public Property Mittente As String
    Public Property Destinatario As String
    Public Property CC As String
    Public Property Oggetto As String
    Public Property Corpo As String
    Public Property file As HttpPostedFile
End Class
'Public Class String
'    Public Property Agente As String
'End Class
Public Class ProgettoViewModel
    Public Property Id As Integer
    Public Property OC As String
    Public Property StartDate As Date
    Public Property EndDate As Date
    Public Property Cliente As String
    Public Property Brand As TipoBrand
    Public Property Codice As String
    Public Property Note_Pezzo As String
    Public Property Affidato As TipoUtente
    Public Property Previsione As Date
    Public Property Note_Generiche As Date
    Public Property Stato_Pezzo As TipoStatoPezzo
End Class
Public Class TicketViewModel
    Public Property Id As Integer
    Public Property RequestUser As String
    Public Property RequestDate As DateTime
    Public Property Request_Type As RQ_Type
    Public Property Title As String
    Public Property Body As String
    Public Property Stato_Ticket As String
    Public Property Solved As String
End Class
Public Class Fattura
    Public Property Anno As String
    Public Property codFat As String
    Public Property NumFat As Decimal
    Public Property Zone As String
    Public Property TipoFat As String
    Public Property Div As String
    Public Property Mese As String
    Public Property VE As String
End Class
Public Class OrdineOrdinato
    Public Property Anno As String
    Public Property codOrd As String
    Public Property NumOrd As Decimal
    Public Property Zone As String
    Public Property TipoOrd As String
    Public Property TipoOrd2 As String
    Public Property ImportoOrd As Decimal
    Public Property UtenteOrd As String
    Public Property Div As String
    Public Property Mese As String
    Public Property Cliente As String
End Class
Public Class SpeseSecondarieViewModel
    Public Property Anno As String
    Public Property codOrd As String
    Public Property NumOrd As String
    Public Property Value As Decimal
End Class
Public Class OCPrioritaViewModel
    Public Property Anno As String
    Public Property codOrd As String
    Public Property NumOrd As String
    Public Property Priorita As String
    Public Property DataRichConsegna As String
    Public Property DataInserimento As String
    Public Property Cliente As String
End Class
Public Class ProgettiUTViewModel
    Public Property Id As Integer
    Public Property OC_Riferimento As String
    Public Property Id_OC As Integer
    Public Property OperatoreSmistamento As String
    Public Property OperatoreSmistamentoId As String
    Public Property Operatore As String
    Public Property OperatoreId As String
    Public Property DataCreazione As DateTime
    Public Property DataCompletamento As Nullable(Of DateTime)
    Public Property Note As String
    Public Property File As String
    Public Property StatoProgetto As Stato_UT
    Public Property Flag_Invio_Materiali As Boolean
    Public Property ListaArt As List(Of ArticoliPerOC)
    Public Property ListOfNote As List(Of NotePerOC)
    Public Property ListOfDocumenti As List(Of DocumentiPerOC)
    Public Property SenttoUC As Boolean
End Class
Public Class ProgettiProdViewModel
    Public Property Id As Integer
    Public Property OC_Riferimento As String
    Public Property Id_OC As Integer
    Public Property OperatoreSmistamento As String
    Public Property OperatoreSmistamentoId As String
    Public Property Operatore As String
    Public Property OperatoreId As String
    Public Property DataCreazione As DateTime
    Public Property DataCompletamento As Nullable(Of DateTime)
    Public Property Note As String
    Public Property File As String
    Public Property StatoProgetto As Stato_UT
    Public Property Flag_Invio_Materiali As Boolean
    Public Property ListaArt As List(Of ArticoliPerOC)
    Public Property ListOfDocumenti As List(Of DocumentiPerOC)
    Public Property ListOfNote As List(Of NotePerOC)
End Class
Public Class CommessaViewModel
    Public Property Data As DateTime
    Public Property DataString As String
    Public Property Action As String
    Public Property Utente As String
    Public Property Qty As String
    Public Property Art As String
    Public Property Timestamp As String
End Class
Public Class DBMancanteViewModel
    Public Property Codice As String
    Public Property ListaArt As New List(Of DBMancanteViewModel)
    Public Property Tipoart As String
    Public Property Qta As Decimal
    Public Property Colore As String
End Class
Public Class DBViewModel
    Public Property Codice As String
    Public Property ListaArt As New List(Of DBViewModel)
    Public Property Qta As Decimal
    Public Property CostoContoLavoro As Decimal
    Public Property CostoMateriali As Decimal
    Public Property CostoInterno_Mdo As Decimal
    Public Property CostoInterno_Macchina As Decimal
    Public Property Tipoart As String
    Public Property Colore As String
End Class
Public Class MasterCosti
    Public Property CostoContoLavoro As Decimal
    Public Property CostoMateriali As Decimal
    Public Property CostoInterno_Mdo As Decimal
    Public Property CostoInterno_Macchina As Decimal
End Class
Public Class OCTempiViewModel
    Public Property OCOT As String
    Public Property Data_Inizio As DateTime
    Public Property Data_Fine As DateTime
    Public Property Data_Diff As Integer
    Public Property Stato As String
End Class
Public Class OlviewModel
    Public Property OL As String
    Public Property Data As String
    Public Property Cliente As String
    Public Property Cod_Fornitore As String
    Public Property PIVA As String
    Public Property Consegna As String
    Public Property Pagamento As String
    Public Property Banca_Dappoggio As String
    Public Property Abi As String
    Public Property Cab As String
    Public Property CC As String
    Public Property Cod_Bolla As String
    Public Property Cod_Prebolla As String
    Public Property Documento_Type As String
    Public Property Operatore As String
    Public Property Cod_Fiscale As String
    Public Property Totale As String
    Public Property listOfArticoli As List(Of ArticoliOL)
End Class
Public Class RichiestaOLviewModel
    Public Property Esecod As String
    Public Property OL As String
    Public Property Num As String
    Public Property email As String
    Public Property FlagMailAuto As Boolean
    Public Property FlagBoth As Boolean
    Public Property List As List(Of LavorazioniEsterne)
End Class
Public Class ArticoliOL
    Public Property cod_articolo As String
    Public Property desc_articolo As String
    Public Property UM As String
    Public Property qta As String
    Public Property importo As String
    Public Property Consegna As String
End Class
<Table("Ordini")>
Public Class Ordine
    <Key>
    Public Property Id As Integer
    Public Property Mese As Mese
    Public Property Anno As Integer
    Public Property CodCliente As String
    Public Property NomeCliente As String
    Public Property Stato As String
    Public Property Regione_uno As String
    Public Property Regione_due As String
    Public Property Tipo_Ordine As Tipo_Ordine
    Public Property Valore_Netto As Decimal
    Public Property Valore_Totale As Decimal
    Public Property Provenienza As Boolean
End Class
<Table("TempiAttivita")>
Public Class TempiAttivita
    <Key>
    Public Property ID As Integer
    Public Property Cod_Macchina As String
    Public Property Descrizione_Attivita As String
    Public Property TempoGiorni As Integer
End Class
<Table("ArticoliPerOC")>
Public Class ArticoliPerOC
    <Key>
    Public Property Id As Integer
    Public Property Cod_Art As String
    Public Property Descrizione As String
    Public Property OC As String
    Public Property DistintaBase As Boolean
End Class
<Table("Macchine")>
Public Class Macchine
    Public Property id As Integer
    Public Property Macchina As String
    Public Property Descrizione_Macchina As String
    Public Property Path_3d As String
End Class
<Table("DatiMacchina")>
Public Class DatiMacchina
    Public Property id As Integer
    Public Property Macchina As String
    Public Property ModalitaMacchina As String
    Public Property FungoPremuto As Boolean
    Public Property ModalitaControllo As String
    Public Property Programma As String
    Public Property EsecuzioneProgramma As String
    Public Property AvanzamanetoProgramma As Integer
    Public Property Data As New DateTime
    Public Property ProgrammaDesc As String
    Public Property LpCuttingTime As String
    Public Property LpOperatingTime As String
    Public Property LpRunningTime As String
    Public Property LpSpindleRunTime As String
    Public Property LpTotalCuttinTime As String
    Public Property LpTotalOperatingTime As String
    Public Property LpTotalRunningTime As String
    Public Property LpTotalSpindleRuntime As String
End Class
<Table("FasiOC")>
Public Class FasiOC
    <Key>
    Public Property Id As Integer
    Public Property OC As String
    Public Property OP As String
    Public Property Articolo As String
    Public Property Macchina As String
    Public Property Fase As String
    Public Property Descrizione_Fase As String
    Public Property Qta_Da_Produrre As Integer
    Public Property Qta_Prodotta As Integer
    Public Property Tempo_Attivita As Double
    Public Property Completata As Boolean
End Class
<Table("NotePerOC")>
Public Class NotePerOC
    <Key>
    Public Property Id As Integer
    Public Property OC As String
    Public Property Contenuto_Nota As String
    Public Property Data_Nota As DateTime
    Public Property Operatore_Id As String
    Public Property Operatore_Nome As String
End Class
<Table("DocumentiPerOC")>
Public Class DocumentiPerOC
    <Key>
    Public Property Id As Integer
    Public Property OC As String
    Public Property Nome_File As String
    Public Property Percorso_File As String
    Public Property DataCreazioneFile As DateTime
    Public Property Operatore_Id As String
    Public Property Operatore_Nome As String
End Class
<Table("DocumentiPerLicenze")>
Public Class DocumentiPerLicenze
    <Key>
    Public Property Id As Integer
    Public Property Nome_File As String
    Public Property Percorso_File As String
    Public Property DataCreazioneFile As DateTime
    Public Property Operatore_Id As String
    Public Property Operatore_Nome As String
End Class
<Table("Licenze")>
Public Class Licenze
    <Key>
    Public Property Id As Integer
    Public Property Nome_Licenza As String
    Public Property Tipologia_Licenza As Tipo_Licenza
    Public Property StartDate As DateTime
    Public Property StartDate_Month As Integer
    Public Property StartDate_Day As Integer
    Public Property StartDate_Year As Integer
    Public Property Tipologia_Rinnovo As Tipo_Rinnovo_Licenza
    Public Property Utente_Collegato As String
    Public Property Active As Boolean
End Class

<Table("Disegni_MPA")>
Public Class Disegni_MPA
    <Key>
    Public Property Id As Integer
    Public Property Code_Disegno As String
    Public Property User As String
    Public Property Cliente As String
    Public Property Path_File As String
    Public Property Descrizione As String
    Public Property Triple_Code As String
    Public Property Desc_Alnus As String
End Class
<Table("Disegni_MPA_server")>
Public Class Disegni_MPA_server
    <Key>
    Public Property Id As Integer
    Public Property Code_Disegno As String
    Public Property Path_File As String
End Class

<Table("ChangeLog")>
Public Class ChangeLog
    <Key>
    Public Property Id As Integer
    Public Property Title As String
    Public Property Descrizione As String
    Public Property Release_Date As DateTime
    Public Property StartDate As DateTime
    Public Property EndDate As DateTime
End Class
<Table("VisualizzazioneChangeLog")>
Public Class VisualizzazioneChangeLog
    <Key>
    Public Property id As Integer
    Public Property changeLog_id As Integer
    Public Property ReaedingDate As DateTime
    Public Property User As String
End Class
<Table("VisualizzazioneFileNota")>
Public Class VisualizzazioneFileNota
    <Key>
    Public Property id As Integer
    Public Property id_filenota As Integer
    Public Property type As TipoVisualizzazione
    Public Property ReaedingDate As DateTime
    Public Property User As String
End Class
<Table("LavorazioniEsterne")>
    Public Class LavorazioniEsterne
    <Key>
    Public Property Id As Integer
    Public Property Id_Alnus As Integer
    Public Property Id_Operatore As String
    Public Property Operatore As String
    Public Property OL As String
    Public Property Data_Inserimento As String
    Public Property Path_Doc As String
    Public Property Path_DDT As String
    Public Property Inviato As Enum_Bolla
    Public Property Email As String
End Class
<Table("HelpDesk")>
Public Class HelpDesk
    <Key>
    Public Property Id As Integer
    Public Property RequestUser As String
    Public Property RequestDate As DateTime
    Public Property Request_Type As RQ_Type
    Public Property Title As String
    Public Property Body As String
    Public Property Body_Risposta As String
    Public Property Stato_Ticket As Stato_Ticket
    Public Property Solved As Boolean
End Class
<Table("Tempi_Macchina")>
Public Class Tempo_Macchina
    <Key>
    Public Property Id As Integer
    Public Property RequestUser As String
    Public Property Macchina As String
    Public Property T_Tot As Decimal
    Public Property T_Start As DateTime
    Public Property T_End As DateTime
End Class
<Table("Log")>
Public Class Log
    <Key>
    Public Property Id As Integer
    Public Property Livello As TipoLogLivello?
    Public Property Indirizzo As String
    Public Property Messaggio As String
    Public Property Dati As String

    <Display(Name:="Ultima Modifica")>
    Public Property UltimaModifica As TipoUltimaModifica

End Class
<Table("Audit")>
Public Class Audit
    <Key>
    Public Property Id As Integer
    Public Property Livello As TipoLogLivello?
    Public Property Indirizzo As String
    Public Property Messaggio As String
    Public Property Dati As String
    <Display(Name:="Ultima Modifica")>
    Public Property UltimaModifica As TipoUltimaModifica
End Class
<Table("StoricoOC")>
Public Class StoricoOC
    <Key>
    Public Property Id As Integer
    Public Property OC As String
    Public Property Ufficio As TipoUfficio?
    Public Property Titolo As String
    Public Property Descrizione As String
    <Display(Name:="Ultima Modifica")>
    Public Property UltimaModifica As TipoUltimaModifica
End Class

<ComplexType>
Public Class TipoUltimaModifica
    Public Property OperatoreID As String
    Public Property Operatore As String

    <DataType(DataType.DateTime)>
    <DisplayFormat(DataFormatString:=Costanti.DisplayFormatDateStandard)>
    Public Property Data As DateTime?
End Class
<Table("Progetti")>
Public Class Progetto
    <Key>
    Public Property Id As Integer
    Public Property OC As String
    Public Property StartDate As Date
    Public Property EndDate As Date
    Public Property Cliente As String
    Public Property Brand As TipoBrand
    Public Property Codice As String
    Public Property Note_Pezzo As String
    Public Property Id_Last_Storico_Progetto As Integer
End Class
<Table("AccettazioneUC")>
Public Class AccettazioneUC
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
    Public Property IsRevisione As Boolean
    Public Property Priorita As Stato_Priorita
    Public Property DataRichiestaConsegna As Nullable(Of DateTime)
    Public Property Brand As String
    Public Property CostoMaggiorato As Boolean
    Public Property Cliente As String
End Class
<Table("ArticoliMagazzino")>
Public Class ArticoliMagazzino
    <Key>
    Public Property Id As Integer
    Public Property codArticolo As String
    Public Property qta As Double
    Public Property noteArticolo As String
    Public Property idSlot As Integer
End Class
<Table("Magazzino")>
Public Class Magazzino
    <Key>
    Public Property Id As Integer
    Public Property CodMagazzino As Integer
    Public Property DescrizioneMagazzino As String
End Class
<Table("ScaffaliMagazzino")>
Public Class ScaffaliMagazzino
    <Key>
    Public Property Id As Integer
    Public Property numScaffale As String
    Public Property idesternoMagazzino As Integer
End Class
<Table("SlotScaffale")>
Public Class SlotScaffale
    <Key>
    Public Property Id As Integer
    Public Property idEsternoScaffale As Integer
    Public Property nomeSlot As String
End Class
<Table("OrdiniDiProduzione")>
Public Class OrdiniDiProduzione
    <Key>
    Public Property Id As Integer
    Public Property OP As String
    Public Property Cartella As String
    Public Property OperatoreInsert As String
    Public Property IdOperatoreInsert As String
    Public Property DataCreazione As DateTime
    Public Property Accettato As Stato_Ordine_Di_Produzione_Esterno
    Public Property Priorita As Stato_Priorita
    Public Property DataRichiestaConsegna As Nullable(Of DateTime)
    Public Property tre_dim_Presente As Boolean
    Public Property due_dim_Presente As Boolean
    Public Property note_presenti As Boolean
    Public Property Articolo As String
End Class

<Table("ProgettiUT")>
Public Class ProgettiUT
    <Key>
    Public Property Id As Integer
    Public Property OC_Riferimento As String
    Public Property OperatoreSmistamento As String
    Public Property OperatoreSmistamentoId As String
    Public Property Operatore As String
    Public Property OperatoreId As String
    Public Property DataCreazione As DateTime
    Public Property DataCompletamento As Nullable(Of DateTime)
    Public Property Note As String
    Public Property File As String
    Public Property StatoProgetto As Stato_UT
    Public Property Flag_Invio_Materiali As Boolean
    Public Property Flag_1 As Boolean
    Public Property Flag_2 As StatoCheck
    Public Property Flag_3 As Boolean
    Public Property Flag_4 As StatoCheck
    Public Property Priorita As Stato_Priorita
    Public Property DataRichiestaConsegna As Nullable(Of DateTime)
End Class
<Table("ProgettiProd")>
Public Class ProgettiProd
    <Key>
    Public Property Id As Integer
    Public Property OC_Riferimento As String
    Public Property OperatoreSmistamento As String
    Public Property OperatoreSmistamentoId As String
    Public Property Operatore As String
    Public Property OperatoreId As String
    Public Property DataCreazione As DateTime
    Public Property DataCompletamento As Nullable(Of DateTime)
    Public Property Note As String
    Public Property File As String
    Public Property StatoProgetto As Stato_Prod
    Public Property Priorita As Stato_Priorita
    Public Property DataRichiestaConsegna As Nullable(Of DateTime)
End Class

<Table("UserEmail")>
Public Class UserEmail
    <Key>
    Public Property Id As Integer
    Public Property Uid As String
    Public Property Email As String
End Class


<Table("Storico_Progetti")>
Public Class Storico_Progetto
    <Key>
    Public Property Id As Integer
    Public Property Id_Progetto As Integer
    Public Property Affidato As TipoUtente
    Public Property Previsione As Date
    Public Property Note_Generiche As Date
    Public Property Stato_Pezzo As TipoStatoPezzo
End Class
<Table("Overview")>
Public Class Overview
    <Key>
    Public Property Id As Integer
    Public Property Matricola As String
    Public Property Macchina As String
    Public Property Zona As TipoZona
    Public Property Data As DateTime
    Public Property Totale_Ore_Uomo As DateTime
    Public Property Totale_Minuti_Uomo As Integer
    Public Property Id_Opera As String
    Public Property Nomina As String
    Public Property OP As String
End Class
<Table("TempiCAD")>
Public Class TempoCAD
    <Key>
    Public Property Id As Integer
    Public Property UserID As Integer
    Public Property UserName As String
    Public Property DatetimeInizio As DateTime
    Public Property DatetimeFine As DateTime
    Public Property ElapsedTime As Decimal
    Public Property OL As String
End Class