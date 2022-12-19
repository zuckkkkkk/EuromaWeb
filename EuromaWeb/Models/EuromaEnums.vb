Imports System.ComponentModel.DataAnnotations

Public Enum Mese As Byte
    Vuoto = Costanti.EnumVuotoValore

    <Display(Name:="Gennaio")>
    Gennaio = 1
    <Display(Name:="Febbraio")>
    Febbraio = 2
    <Display(Name:="Marzo")>
    Marzo = 3
    <Display(Name:="Aprile")>
    Aprile = 4
    <Display(Name:="Maggio")>
    Maggio = 5
    <Display(Name:="Giugno")>
    Giugno = 6
    <Display(Name:="Luglio")>
    Luglio = 7
    <Display(Name:="Agosto")>
    Agosto = 8
    <Display(Name:="Settembre")>
    Settembre = 9
    <Display(Name:="Ottobre")>
    Ottobre = 10
    <Display(Name:="Novembre")>
    Novembre = 11
    <Display(Name:="Dicembre")>
    Dicembre = 12
End Enum
Public Enum TipoVisualizzazione As Byte
    File = 1
    Nota = 2
End Enum
Public Enum Tipo_Ordine As Byte
    Vuoto = Costanti.EnumVuotoValore

    <Display(Name:="Accessori e Ricambi")>
    AccessoriERicambi = 1
    <Display(Name:="Materiale CMT")>
    CMT = 2
    <Display(Name:="Materiale Drillmatic")>
    Drillmatic = 3
    <Display(Name:="Materiale ISA")>
    ISA = 4
    <Display(Name:="Materiale MPA")>
    MPA = 5
    <Display(Name:="Materiale Unistand")>
    Unistand = 6
    <Display(Name:="-")>
    None = 7
End Enum
Public Enum Stato_Ordine_Di_Produzione_Esterno As Byte
    <Display(Name:="In attesa documenti interni")>
    In_attesa_int = 0
    <Display(Name:="In attesa documenti esterni")>
    In_attesa_est = 1
    <Display(Name:="In lavorazione")>
    In_Lavorazione = 2
    <Display(Name:="Completato esterno")>
    Completato_Esterno = 3
    <Display(Name:="Conclusa fase macchina")>
    Completato = 5
    <Display(Name:="Finito")>
    Finito = 99
End Enum
Public Enum Stato_UC As Byte
    Vuoto = Costanti.EnumVuotoValore
    <Display(Name:="In attesa")>
    In_attesa = 0
    <Display(Name:="Accettato")>
    Accettato = 1
    <Display(Name:="Non accettato")>
    Non_Accettato = 2
    <Display(Name:="Inviato")>
    Inviato = 3
    <Display(Name:="Inviato a UC")>
    Inviato_UC = 4
    <Display(Name:="Inviato a Prod")>
    Inviato_Prod = 5
    <Display(Name:="Inviato a UT")>
    Inviato_UT = 6
    <Display(Name:="Ritorno da UT")>
    Ritorno_da_UT = 7
    <Display(Name:="Chiuso")>
    Chiuso = 99
End Enum
Public Enum StatoCheck As Byte
    <Display(Name:="Selezione")>
    Selezione = 0
    <Display(Name:="Si")>
    Si = 1
    <Display(Name:="Non Necessario")>
    Non_Necessario = 2

End Enum
Public Enum Stato_Ticket As Byte
    <Display(Name:="In Attesa")>
    In_attesa = 0
    <Display(Name:="Presa in carico")>
    Smistato = 10
    <Display(Name:="In Lavorazione")>
    In_Lavorazione = 50
    <Display(Name:="Completato")>
    Completato = 90
    <Display(Name:="Inviato")>
    Inviato = 100
    <Display(Name:="Errore")>
    Errore = 101
End Enum
Public Enum Stato_UT As Byte
    <Display(Name:="In Attesa Admin")>
    In_attesa_Admin = 0
    <Display(Name:="Ritorno da Produzione")>
    Ritorno_Da_Prod = 5
    <Display(Name:="In Attesa Operatore")>
    In_Attesa_Operatore = 10
    <Display(Name:="In attesa maggiori informazioni")>
    Maggiori_Info_Necessarie = 25
    <Display(Name:="Sviluppo per approvazione cliente")>
    Sviluppo_Approvazione_Cliente = 35
    <Display(Name:="Attesa approvazione cliente")>
    Attesa_Approvazione_Cliente = 45
    <Display(Name:="Approvazione avvenuta")>
    Approvazione_Cliente = 50
    <Display(Name:="Disegno Particolari")>
    Disegno_Particolari = 75
    <Display(Name:="Completato")>
    Completato = 90
    <Display(Name:="Inviato in produzione")>
    Inviato = 100
    <Display(Name:="Chiuso")>
    Chiuso = 255
End Enum
Public Enum Stato_Priorita As Byte
    <Display(Name:="Nessuna Priorita")>
    No_Priorita = 0
    <Display(Name:="Priorita massima 1")>
    Priorita_Max_1 = 1
    <Display(Name:="Priorita massima 2")>
    Priorita_Max_2 = 2
    <Display(Name:="Priorita massima 3")>
    Priorita_Max_3 = 3
    <Display(Name:="Priorita media 1")>
    Priorita_Media_1 = 4
    <Display(Name:="Priorita media 2")>
    Priorita_Media_2 = 5
    <Display(Name:="Priorita media 3")>
    Priorita_Media_3 = 6
    <Display(Name:="Priorita minima 1")>
    Priorita_Minima_1 = 7
    <Display(Name:="Priorita minima 2")>
    Priorita_Minima_2 = 8
    <Display(Name:="Priorita minima 3")>
    Priorita_Minima_3 = 9
End Enum
Public Enum Stato_Prod As Byte
    <Display(Name:="In Attesa")>
    In_attesa = 0
    <Display(Name:="Ritorno da UT")>
    Ritorno_Da_UT = 5
    <Display(Name:="In attesa maggiori informazioni")>
    Maggiori_Info_Necessarie = 25
    <Display(Name:="In Lavorazione")>
    In_Lavorazione = 50
    <Display(Name:="Completato")>
    Completato = 90
    <Display(Name:="Rilasciato")>
    Rilasciato = 100
    <Display(Name:="Chiuso")>
    Chiuso = 255
End Enum
Public Enum Stato_UT_Operatore As Byte
    <Display(Name:="In Attesa")>
    In_Attesa_Operatore = 10
    <Display(Name:="In attesa maggiori informazioni")>
    Maggiori_Info_Necessarie = 25
    <Display(Name:="Sviluppo per approvazione cliente")>
    Sviluppo_Approvazione_Cliente = 35
    <Display(Name:="Attesa approvazione cliente")>
    Attesa_Approvazione_Cliente = 45
    <Display(Name:="Approvazione avvenuta")>
    Approvazione_Cliente = 50
    <Display(Name:="Disegno Particolari")>
    Disegno_Particolari = 75
    <Display(Name:="Completato")>
    Completato = 90
    <Display(Name:="Chiuso")>
    Chiuso = 255
End Enum
Public Enum TipoBrand As Byte
    Vuoto = Costanti.EnumVuotoValore
    <Display(Name:="CMT")>
    CMT = 2
    <Display(Name:="Drillmatic")>
    Drillmatic = 3
    <Display(Name:="ISA")>
    ISA = 4
    <Display(Name:="MPA")>
    MPA = 5
    <Display(Name:="Unistand")>
    Unistand = 6
End Enum
Public Enum TipoLogLivello As Byte
    Errors = 0
    Warning = 1
    Info = 2
    Debug = 3
End Enum
Public Enum TipoUfficio As Byte
    UfficioCommerciale = 0
    UfficioTecnico = 1
    Produzione = 2
End Enum
Public Enum TipoAuditLivello As Byte
    Errors = 0
    Warning = 1
    Info = 2
    Debug = 3
End Enum
Public Enum Tipo_Licenza As Byte
    Microsoft = 0
    CDM = 1
End Enum
Public Enum Enum_Bolla As Byte
    In_attesa = 0
    Test_interno = 1
    Inviato = 2
    Errore = 99
End Enum
Public Enum Tipo_Rinnovo_Licenza As Byte
    Mensile = 0
    Trimestrale = 1
    Semestrale = 2
    Annuale = 3
End Enum
Public Enum RQ_Type As Byte
    Vuoto = Costanti.EnumVuotoValore
    <Display(Name:="Alnus")>
    Alnus = 1
    <Display(Name:="Opera")>
    Opera = 2
    <Display(Name:="Anomalia")>
    Anomalia = 3
    <Display(Name:="Server")>
    Server = 4
    <Display(Name:="Modula")>
    Modula = 5
    <Display(Name:="Varie")>
    Varie = 6
End Enum
Public Enum Macchina_CNF As Byte
    Vuoto = Costanti.EnumVuotoValore
    <Display(Name:="CNF")>
    CNF = 1
    <Display(Name:="CNF2")>
    CNF2 = 2
    <Display(Name:="CNF4")>
    CNF4 = 4
    <Display(Name:="CNF5")>
    CNF5 = 5
    <Display(Name:="CNF6")>
    CNF6 = 6
    <Display(Name:="CNF7")>
    CNF7 = 7
    <Display(Name:="CNF8")>
    CNF8 = 8
    <Display(Name:="CNF9")>
    CNF9 = 9
    <Display(Name:="CNF10")>
    CNF10 = 10
End Enum
Public Enum TipoUtente As Byte
    Vuoto = Costanti.EnumVuotoValore
    <Display(Name:="Utente 1")>
    Utente_1 = 1
    <Display(Name:="Utente 2")>
    Utente_2 = 2
    <Display(Name:="Utente 3")>
    Utente_3 = 3
    <Display(Name:="Utente 4")>
    Utente_4 = 4
    <Display(Name:="Utente 5")>
    Utente_5 = 5
End Enum
Public Enum TipoZona As Byte
    Vuoto = Costanti.EnumVuotoValore
    <Display(Name:="Montaggio")>
    Montaggio = 1
    <Display(Name:="Torni")>
    Torni = 2
    <Display(Name:="Frese")>
    Frese = 3
    <Display(Name:="Magazzino")>
    Magazzino = 4
    <Display(Name:="Rettifiche")>
    Rettifiche = 5
End Enum
Public Enum TipoStatoPezzo As Byte
    Vuoto = Costanti.EnumVuotoValore
    <Display(Name:="In attesa")>
    In_Attesa = 1
    <Display(Name:="In lavorazione")>
    In_Lavorazione = 2
    <Display(Name:="In attesa di accettazione")>
    In_Accettazione = 3
    <Display(Name:="Completato")>
    Completato = 4
End Enum