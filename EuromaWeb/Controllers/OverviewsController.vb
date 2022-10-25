Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports System.Xml
Imports EuromaWeb
Imports Microsoft.Ajax.Utilities
Imports Microsoft.AspNet.Identity
Imports Newtonsoft.Json

Namespace Controllers
    Public Class OverviewsController
        Inherits System.Web.Mvc.Controller
        Private Const ConnectionString As String = "Persist Security Info=True;Password=ALNUSAD;User ID=ALNUSAD;Initial Catalog=Opera6010;Data Source=192.168.100.50"
        Private Const ConnectionStringAlnus As String = "Persist Security Info=True;Password=ALNUSAD;User ID=ALNUSAD;Initial Catalog=ALNEUMA;Data Source=192.168.100.50"
        Private myConn As SqlConnection
        Private myCmd As SqlCommand
        Private myReader As SqlDataReader
        Private results As String
        Private db As New EuromaModels

        ' GET: Overviews
        <Authorize>
        Function Index() As ActionResult
            If Not User.IsInRole("Admin") Then
                Return RedirectToAction("Index", "Home")
            End If
            Dim yesterday = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 12, 0, 0, 0)
            'Dim yesterday = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0, 0)
            'yesterday = yesterday.AddDays(-1)
            If yesterday.DayOfWeek = DayOfWeek.Saturday Then
                yesterday = yesterday.AddDays(-1)
            End If
            If yesterday.DayOfWeek = DayOfWeek.Sunday Then
                yesterday = yesterday.AddDays(-2)
            End If
            Dim lastWeekDate = yesterday
            lastWeekDate = lastWeekDate.AddDays(-7)
            Dim lastWeekDateComparisonEnd = lastWeekDate.AddDays(-1)
            Dim lastWeekDateComparisonStart = lastWeekDateComparisonEnd.AddDays(-7)
            Dim YesterdayWork = db.Overview.Where(Function(x) x.Data < yesterday And x.Data > lastWeekDate).ToList
            Dim ComparisonWork = db.Overview.Where(Function(x) x.Data < lastWeekDateComparisonEnd And x.Data > lastWeekDateComparisonStart).ToList

            'Blocco ricerca dati di questa settimana
            ViewBag.StartDate = lastWeekDate
            ViewBag.EndDate = yesterday
            ViewBag.Montaggio = 0
            ViewBag.Rettifiche = 0
            ViewBag.Magazzino = 0
            ViewBag.Torni = 0
            ViewBag.Frese = 0
            Dim mon = 0
            Dim ret = 0
            Dim mag = 0
            Dim Fre = 0
            Dim Tor = 0
            For Each T In YesterdayWork
                Select Case T.Zona
                    Case 1
                        ViewBag.Montaggio = ViewBag.Montaggio + T.Totale_Minuti_Uomo
                        mon = mon + 540
                    Case 2
                        ViewBag.Torni = ViewBag.Torni + T.Totale_Minuti_Uomo
                        Tor = Tor + 540
                    Case 3
                        ViewBag.Frese = ViewBag.Frese + T.Totale_Minuti_Uomo
                        Fre = Fre + 540
                    Case 4
                        ViewBag.Magazzino = ViewBag.Magazzino + T.Totale_Minuti_Uomo
                        mag = mag + 540
                    Case 5
                        ViewBag.Rettifiche = ViewBag.Rettifiche + T.Totale_Minuti_Uomo
                        ret = ret + 540
                    Case Else
                End Select
            Next
            If mon <> 0 Then
                ViewBag.Montaggio = (ViewBag.Montaggio / mon) * 100
            End If
            If ret <> 0 Then
                ViewBag.Rettifiche = (ViewBag.Rettifiche / ret) * 100
            End If
            If mag <> 0 Then
                ViewBag.Magazzino = (ViewBag.Magazzino / mag) * 100
            End If
            If Tor <> 0 Then
                ViewBag.Torni = (ViewBag.Torni / Tor) * 100
            End If
            If Fre <> 0 Then
                ViewBag.Frese = (ViewBag.Frese / Fre) * 100
            End If
            ViewBag.Montaggio = Convert.ToInt32(ViewBag.Montaggio.ToString().Split(",")(0))
            ViewBag.Rettifiche = Convert.ToInt32(ViewBag.Rettifiche.ToString().Split(",")(0))
            ViewBag.Magazzino = Convert.ToInt32(ViewBag.Magazzino.ToString().Split(",")(0))
            ViewBag.Torni = Convert.ToInt32(ViewBag.Torni.ToString().Split(",")(0))
            ViewBag.Frese = Convert.ToInt32(ViewBag.Frese.ToString().Split(",")(0))
            'Blocco ricerca dati di settimana precedente
            ViewBag.MontaggioComparison = 0
            ViewBag.RettificheComparison = 0
            ViewBag.MagazzinoComparison = 0
            ViewBag.TorniComparison = 0
            ViewBag.FreseComparison = 0
            Dim monComparison = 0
            Dim retComparison = 0
            Dim magComparison = 0
            Dim FreComparison = 0
            Dim TorComparison = 0
            For Each T In YesterdayWork
                Select Case T.Zona
                    Case 1
                        ViewBag.MontaggioComparison = ViewBag.MontaggiComparisono + T.Totale_Minuti_Uomo
                        monComparison = monComparison + 540
                    Case 2
                        ViewBag.TorniComparison = ViewBag.TorniComparison + T.Totale_Minuti_Uomo
                        TorComparison = TorComparison + 540
                    Case 3
                        ViewBag.FreseComparison = ViewBag.FreseComparison + T.Totale_Minuti_Uomo
                        FreComparison = FreComparison + 540
                    Case 4
                        ViewBag.MagazzinoComparison = ViewBag.MagazzinoComparison + T.Totale_Minuti_Uomo
                        magComparison = magComparison + 540
                    Case 5
                        ViewBag.RettificheComparison = ViewBag.RettificheComparison + T.Totale_Minuti_Uomo
                        retComparison = retComparison + 540
                    Case Else
                End Select
            Next
            If monComparison <> 0 Then
                ViewBag.MontaggioComparison = (ViewBag.Montaggio * 100) / ViewBag.MontaggioComparison
            Else
                ViewBag.MontaggioComparison = 100
            End If
            If retComparison <> 0 Then
                ViewBag.RettificheComparison = (ViewBag.Rettifiche * 100) / ViewBag.RettificheComparison
            Else
                ViewBag.RettificheComparison = 100
            End If
            If magComparison <> 0 Then
                ViewBag.MagazzinoComparison = (ViewBag.Magazzino * 100) / ViewBag.MagazzinoComparison
            ElseIf mag <> 0 Then
                ViewBag.MagazzinoComparison = 0
            ElseIf mag <> 0 And magComparison = 0 Then
                ViewBag.MagazzinoComparison = 100
            End If
            If TorComparison <> 0 Then
                ViewBag.TorniComparison = (ViewBag.Torni * 100) / ViewBag.TorniComparison
            Else
                ViewBag.TorniComparison = 100
            End If
            If FreComparison <> 0 Then
                ViewBag.FreseComparison = (ViewBag.Frese * 100) / ViewBag.FreseComparison
            Else
                ViewBag.FreseComparison = 100
            End If
            ViewBag.MontaggioComparison = Convert.ToInt32(ViewBag.MontaggioComparison.ToString().Split(",")(0))
            ViewBag.RettificheComparison = Convert.ToInt32(ViewBag.RettificheComparison.ToString().Split(",")(0))
            ViewBag.MagazzinoComparison = Convert.ToInt32(ViewBag.MagazzinoComparison.ToString().Split(",")(0))
            ViewBag.TorniComparison = Convert.ToInt32(ViewBag.TorniComparison.ToString().Split(",")(0))
            ViewBag.FreseComparison = Convert.ToInt32(ViewBag.FreseComparison.ToString().Split(",")(0))
            Return View()
        End Function
        <Authorize>
        <HttpPost()>
        Function OrdineExist(ByVal id As String) As JsonResult
            Dim countOP = db.FasiOC.Where(Function(x) x.OP = id).Count
            Dim countOC = db.AccettazioneUC.Where(Function(x) x.OC = id).Count
            Dim countOPgiaEst = db.OrdiniDiProduzione.Where(Function(x) x.OP = id).Count
            If countOP > 0 Then
                Return Json(New With {.ok = True})
            End If
            If countOC > 0 Then
                Return Json(New With {.ok = True})
            End If
            If countOPgiaEst > 0 And countOC = 0 Then
                Return Json(New With {.ok = False, .type = 3})
            End If
            If id.Contains("OP") Then
                Return Json(New With {.ok = False, .type = 1})
            End If
            If id.Contains("OC") Then
                Return Json(New With {.ok = False, .type = 2})
            End If
        End Function
        Function Ordine(ByVal id As String) As ActionResult
            If id.Contains("OP") Then
                ViewBag.OP = id
                Dim OC = db.FasiOC.Where(Function(X) X.OP = id).Count
                If OC = 0 Then
                    Return Nothing
                Else
                    id = db.FasiOC.Where(Function(x) x.OP = id).First.OC
                End If
            End If
            If db.StoricoOC.Where(Function(x) x.OC = id).Count > 0 Then
                Dim accettazione = db.AccettazioneUC.Where(Function(x) x.OC = id).First
                Dim ListaStorico = db.StoricoOC.Where(Function(x) x.OC = id).OrderBy(Function(y) y.UltimaModifica.Data).ToList
                Dim ListaArticoli = db.ArticoliPerOC.Where(Function(x) x.OC = id).ToList
                Dim ListaDocumenti = db.DocumentiPerOC.Where(Function(x) x.OC = id).ToList
                Dim ListaNote = db.NotePerOC.Where(Function(x) x.OC = id).ToList
                Dim StatoGenerale = 1
                For Each s In ListaStorico
                    If s.Descrizione = "Documento condiviso correttamente con l'ufficio Tecnico" And StatoGenerale <= 2 Then
                        StatoGenerale = 2
                    End If
                    If s.Descrizione = "Aggiornato stato progetto in Ufficio Tecnico, stato attuale: Approvazione_Cliente" And StatoGenerale <= 3 Then
                        StatoGenerale = 3
                    End If
                    If s.Descrizione = "Progetto correttamente inviato all'Ufficio Produzione" And StatoGenerale <= 4 Then
                        StatoGenerale = 4
                    End If
                    If s.Descrizione = "Documento condiviso correttamente con l'ufficio Produzione" And StatoGenerale <= 4 Then
                        StatoGenerale = 4
                    End If
                    If s.Descrizione = "Aggiornamento stato in Produzione: Rilasciato" And StatoGenerale <= 5 Then
                        StatoGenerale = 5
                    End If
                Next
                Dim ListaOPTemporary As New List(Of OrdineDiProduzioneViewModel)
                If db.FasiOC.Where(Function(x) x.OC = accettazione.OC).Count > 0 Then
                    Dim ListaOP = db.FasiOC.Where(Function(x) x.OC = accettazione.OC).ToList
                    For Each l In ListaOP
                        If ListaOPTemporary.Where(Function(x) x.OP = l.OP).Count > 0 Then
                            Dim lT = ListaOPTemporary.Where(Function(x) x.OP = l.OP).First
                            lT.ListaFasi.Add(New FaseViewModel With {
                                .NumFase = l.Fase,
                                .DescFase = l.Descrizione_Fase,
                                .Completato = l.Completata
                            })
                        Else
                            Dim count = db.OrdiniDiProduzione.Where(Function(x) x.OP = l.OP).Count
                            Dim elem = New OrdineDiProduzioneViewModel With {
                                .Articolo = l.Articolo,
                                .ListaFasi = New List(Of FaseViewModel),
                                .OP = l.OP,
                                .PresenteInDbEsterno = IIf(count > 0, 1, 0)
                            }
                            elem.ListaFasi.Add(New FaseViewModel With {
                                               .Completato = l.Completata,
                                               .DescFase = l.Descrizione_Fase,
                                               .NumFase = l.Fase
                                               })
                            ListaOPTemporary.Add(elem)
                        End If
                    Next
                End If

                Return View(New OverviewOrdineViewModel With {
                    .ListArt = ListaArticoli,
                    .Timeline = ListaStorico,
                    .Stato_Generale = StatoGenerale,
                    .OC = id,
                    .id = accettazione.Id,
                    .Documenti = ListaDocumenti,
                    .NoteList = ListaNote,
                    .ListaOP = ListaOPTemporary
                })
            End If


        End Function
        Function Mancanti(ByVal id As String) As ActionResult
            Dim a = RecursiveDistinta(New DBViewModel With {.Codice = id, .ListaArt = New List(Of DBViewModel)})
            Return PartialView(a)
        End Function
        Function Gantt(ByVal id As String) As ActionResult
            ViewBag.idOP = id
            Return PartialView()
        End Function
        Function PostGantt(ByVal id As String) As JsonResult
            Dim listFasi = db.FasiOC.Where(Function(x) x.OP = id).ToList '.Where(Function(x) x.OP = id)
            Dim listaF As New List(Of GanttViewModel)
            Dim dateNow = DateTime.Now.ToString.Split(" ")(0)
            Dim listaObj As New List(Of Object)
            Dim listaGantt As New List(Of Object)
            For Each l In listFasi
                If listaGantt.Where(Function(x) x.OP = l.OP).Any Then
                    Dim elem = listaGantt.Where(Function(x) x.OP = l.OP).Last
                    Dim StartingDate = DateTime.ParseExact(elem.end, “yyyy-MM-dd”, System.Globalization.CultureInfo.InvariantCulture)
                    Dim dateLater = StartingDate.AddDays(getDays(l.Macchina, l.Descrizione_Fase)).ToString.Split(" ")(0)
                    Dim progress = 0
                    If l.Completata Then
                        progress = 100
                    Else
                        progress = 0
                    End If
                    Dim ojb As New List(Of Object)
                    listaGantt.Add(New With {
                        .id = l.Id,
                        .start = StartingDate.ToString.Substring(6, 4) + "-" + StartingDate.ToString.Substring(3, 2) + "-" + StartingDate.ToString.Substring(0, 2),
                        .end = dateLater.ToString.Substring(6, 4) + "-" + dateLater.ToString.Substring(3, 2) + "-" + dateLater.ToString.Substring(0, 2),
                        .name = l.Descrizione_Fase,
                        .progress = progress,
                        .OP = l.OP
                    })
                Else
                    Dim dateLater = DateTime.Now.AddDays(getDays(l.Macchina, l.Descrizione_Fase)).ToString.Split(" ")(0)
                    Dim progress = 0
                    If l.Completata Then
                        progress = 100
                    Else
                        progress = 0
                    End If
                    Dim ojb As New List(Of Object)
                    listaGantt.Add(New With {
                        .id = l.Id,
                        .start = dateNow.ToString.Substring(6, 4) + "-" + dateNow.ToString.Substring(3, 2) + "-" + dateNow.ToString.Substring(0, 2),
                        .end = dateLater.ToString.Substring(6, 4) + "-" + dateLater.ToString.Substring(3, 2) + "-" + dateLater.ToString.Substring(0, 2),
                        .name = l.Descrizione_Fase,
                        .progress = progress,
                        .OP = l.OP
                    })
                End If
            Next
            Return Json(New With {.ok = True, .lista = listaGantt}, JsonRequestBehavior.AllowGet)
        End Function

        ' GET: Overviews/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim overview As Overview = db.Overview.Find(id)
            If IsNothing(overview) Then
                Return HttpNotFound()
            End If
            Return View(overview)
        End Function
        Function getDays(ByVal macchina As String, ByVal descrizione As String)
            Dim tempo = 0
            Dim checkEx = db.TempiAttivita.Where(Function(x) x.Cod_Macchina = macchina And x.Descrizione_Attivita = descrizione).Count
            Dim checkEx1 = db.TempiAttivita.Where(Function(x) x.Cod_Macchina = macchina).Count
            Dim checkEx2 = db.TempiAttivita.Where(Function(x) x.Descrizione_Attivita = descrizione).Count
            If checkEx > 0 Then
                tempo = db.TempiAttivita.Where(Function(x) x.Cod_Macchina = macchina And x.Descrizione_Attivita = descrizione).First.TempoGiorni
            ElseIf checkEx1 > 0 Then
                tempo = db.TempiAttivita.Where(Function(x) x.Cod_Macchina = macchina).First.TempoGiorni
            ElseIf checkEx2 > 0 Then
                tempo = db.TempiAttivita.Where(Function(x) x.Descrizione_Attivita = descrizione).First.TempoGiorni
            Else
            End If
            Return tempo
        End Function

        ' GET: Overviews/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Overviews/Create
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,Matricola,Macchina,Zona,Data,Totale_Ore_Uomo")> ByVal overview As Overview) As ActionResult
            If ModelState.IsValid Then
                db.Overview.Add(overview)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(overview)
        End Function
        Function Update() As JsonResult
            Dim listOfFasi As New List(Of OverviewViewModel)
            Dim date1 = DateTime.Today.AddDays(+1)
            Dim date2 = DateTime.Today.AddDays(0)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "
                         SELECT Produzione.di_matrico,ma_codice,dp_datain, dp_datafi, dp_oreuomo, dp_autoinc, di_nomina,Fasi.pb_codice 
                         FROM [Opera6010].[dbo].[Produzione], Dipendenti as D, Fasi                  
                         WHERE Produzione.fa_id = fasi.fa_id and Produzione.di_matrico = D.di_matrico AND dp_datain < '" & date1.Day.ToString.PadLeft(2, "0"c) + "/" + date1.Month.ToString.PadLeft(2, "0"c) + "/" + date1.Year.ToString & "' AND dp_datain > '" & date2.Day.ToString.PadLeft(2, "0"c) + "/" + date2.Month.ToString.PadLeft(2, "0"c) + "/" + date2.Year.ToString & "'
                "
                myConn.Open()
            Catch ex As Exception

            End Try
            'Parse dei dati da SQL
            Try
                myReader = myCmd.ExecuteReader
                Do While myReader.Read()
                    Dim OP_Code = myReader.GetString(7).Substring(1, 4) + "-" + "OP" + "-" + Replace(LTrim(Replace(myReader.GetString(7).Substring(7, 7), "0", " ")), " ", "0") + "_" + Replace(LTrim(Replace(myReader.GetString(7).Substring(14, 5), "0", " ")), " ", "0")
                    Dim Fase As New OverviewViewModel With {
                   .Matricola = myReader.GetDecimal(0),
                   .Macchina = myReader.GetString(1),
                   .Data_Inizio = myReader.GetDateTime(2),
                   .Data_Fine = myReader.GetDateTime(3),
                   .OreUomo = myReader.GetDecimal(4),
                   .AutoInc = myReader.GetDecimal(5),
                   .Nomina = myReader.GetString(6),
                   .OP = OP_Code
               }
                    listOfFasi.Add(Fase)
                Loop
                myConn.Close()

            Catch ex As Exception

            End Try
            Dim distinctValue = listOfFasi.DistinctBy(Function(X) X.Matricola).ToList
            For Each d In distinctValue
                'Definizione del periodo di lavoro
                Dim time(540)
                'Inserimento a zero del periodo di lavoro
                For i = 0 To 540
                    time(i) = 0
                Next
                'Tempo base start alle 7:30
                For Each fase In listOfFasi.Where(Function(x) x.Matricola = d.Matricola)
                    Dim floorTime = New DateTime(fase.Data_Inizio.Year, fase.Data_Inizio.Month, fase.Data_Inizio.Day, 7, 30, 0, 0)
                    Dim endTime = New DateTime(fase.Data_Inizio.Year, fase.Data_Inizio.Month, fase.Data_Inizio.Day, 16, 30, 0, 0)
                    'se aperta fase prima falla partire dalle 7:30
                    If fase.Data_Inizio < floorTime Then
                        fase.Data_Inizio = floorTime
                    End If
                    If fase.Data_Fine > endTime Then
                        fase.Data_Fine = endTime
                    End If
                    'Differenza tra fine e inizio
                    Dim spanAsDate As TimeSpan = fase.Data_Fine.Subtract(fase.Data_Inizio)
                    'Ricerca posizione per cominciare nell'array
                    Dim startPosAsDate As TimeSpan = fase.Data_Inizio.Subtract(floorTime)
                    Dim intStartPos = 0
                    Dim intToPos = 0
                    'Convert le ore e i minuti da data a numero per accesso ad array
                    If startPosAsDate.Hours <> 0 Then
                        For i = 1 To startPosAsDate.Hours
                            intStartPos = intStartPos + 60
                        Next
                    End If
                    intStartPos = intStartPos + startPosAsDate.Minutes
                    If spanAsDate.Hours <> 0 Then
                        For i = 1 To spanAsDate.Hours
                            intToPos = intToPos + 60
                        Next
                    End If
                    intToPos = intToPos + spanAsDate.Minutes
                    For i = intStartPos To intToPos + intStartPos
                        time(i) = 1
                    Next
                Next
                Dim totalMinutes = 0
                For i = 0 To time.Count - 1
                    If time(i) = 1 Then
                        totalMinutes = totalMinutes + 1
                    End If
                Next
                Dim zona As TipoZona
                Select Case listOfFasi.Where(Function(x) x.Matricola = d.Matricola).First.Macchina
                    Case "BANCO"
                        zona = TipoZona.Montaggio
                    Case "CNF", "CNF2", "CNF3", "CNF4", "CNF5", "CNF6", "CNF7", "CNF8", "CNF9", "CNF10"
                        zona = TipoZona.Frese
                    Case "CNT", "CNT2", "CNT3", "CNT4", "CNT5", "CNT6", "STOX", "T,NI1", "T,NI2"
                        zona = TipoZona.Torni
                    Case "0020"
                        zona = TipoZona.Magazzino
                    Case "DENT", "DENTA", "FAV R", "FAVMB", "LAPID1", "LAPID2", "M,ARA", "PARAT", "RADDR", "RETTI", "SALDA", "TANG", "TS"
                        zona = TipoZona.Rettifiche
                End Select
                Try
                    Dim DatasObj As New Overview With {
                                        .Data = New DateTime(listOfFasi.First.Data_Inizio.Year, listOfFasi.First.Data_Inizio.Month, listOfFasi.First.Data_Inizio.Day, 12, 0, 0, 0),
                                        .Totale_Ore_Uomo = New DateTime(listOfFasi.First.Data_Inizio.Year, listOfFasi.First.Data_Inizio.Month, listOfFasi.First.Data_Inizio.Day, 12, 0, 0, 0),
                                        .Id_Opera = listOfFasi.Where(Function(x) x.Matricola = d.Matricola).First.AutoInc.ToString,
                                        .Macchina = listOfFasi.Where(Function(x) x.Matricola = d.Matricola).First.Macchina.ToString,
                                        .Matricola = listOfFasi.Where(Function(x) x.Matricola = d.Matricola).First.Matricola.ToString,
                                        .Totale_Minuti_Uomo = totalMinutes,
                                        .Zona = zona,
                                        .Nomina = d.Nomina,
                                        .OP = d.OP
                                    }
                    Dim alreadyExist = db.Overview.Where(Function(x) x.Id_Opera = DatasObj.Id_Opera).FirstOrDefault
                    If IsNothing(alreadyExist) Then
                        db.Overview.Add(DatasObj)
                        db.SaveChanges()
                    End If
                Catch ex As Exception

                End Try

                Console.WriteLine(totalMinutes)
            Next
        End Function
        'SEZIONE GESTIONE MAGAZZINO
        <Authorize>
        Function GestioneMagazzino(ByVal id As Integer) As ActionResult
            Dim magazzino = db.Magazzino.Find(id)

            Dim scaffali = db.ScaffaliMagazzino.Where(Function(x) x.idesternoMagazzino = magazzino.Id).ToList
            Dim ListaScaffali As New List(Of GestioneScaffaliViewModel)
            For Each s In scaffali
                Dim ListaSlotNorm = db.SlotScaffale.Where(Function(x) x.idEsternoScaffale = s.Id).ToList
                Dim listSlotViewModel As New List(Of GestioneSlotViewModel)
                For Each l In ListaSlotNorm
                    Dim count = db.ArticoliMagazzino.Where(Function(x) x.idSlot = l.Id).Count
                    listSlotViewModel.Add(New GestioneSlotViewModel With {
                        .idSlot = l.Id,
                        .CodSlot = l.nomeSlot,
                        .Count = count
                    })
                Next
                ListaScaffali.Add(New GestioneScaffaliViewModel With {
                    .NumScaffale = s.numScaffale,
                    .ListaSlot = listSlotViewModel
                })
            Next
            Return View(New GestioneMagazzinoViewModel With {
                .IdMag = magazzino.Id,
                .CodMag = magazzino.CodMagazzino,
                .DescMag = magazzino.DescrizioneMagazzino,
                .ListaScaffali = ListaScaffali
            })
        End Function
        'END SEZIONE GESTIONE MAGAZZINO
        ' GET: Overviews/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim overview As Overview = db.Overview.Find(id)
            If IsNothing(overview) Then
                Return HttpNotFound()
            End If
            Return View(overview)
        End Function
        Function CreateArticolo(ByVal id As Integer) As ActionResult
            Dim scaffali = db.ScaffaliMagazzino.Where(Function(x) x.idesternoMagazzino = id).ToList
            Dim selectSlotList As New List(Of SlotListViewModel)
            For Each s In scaffali
                Dim slotList = db.SlotScaffale.Where(Function(x) x.idEsternoScaffale = s.Id).ToList
                For Each l In slotList
                    Dim scaffale = db.ScaffaliMagazzino.Where(Function(x) x.Id = l.idEsternoScaffale).First.numScaffale
                    selectSlotList.Add(New SlotListViewModel With {
                        .Id = l.Id,
                        .Scaffale_Slot = scaffale.ToString + "-" + l.nomeSlot
                    })
                Next
            Next
            ViewBag.idSlot = New SelectList(selectSlotList, "Id", "Scaffale_Slot")
            Return PartialView()
        End Function
        Function EditArticolo(ByVal id As Integer) As ActionResult
            Dim art = db.ArticoliMagazzino.Find(id)
            Dim slot = db.SlotScaffale.Find(art.idSlot)
            Dim scaff = db.ScaffaliMagazzino.Find(slot.idEsternoScaffale)
            Dim scaffali = db.ScaffaliMagazzino.Where(Function(x) x.idesternoMagazzino = scaff.idesternoMagazzino).ToList
            Dim selectSlotList As New List(Of SlotListViewModel)
            For Each s In scaffali
                Dim slotList = db.SlotScaffale.Where(Function(x) x.idEsternoScaffale = s.Id).ToList
                For Each l In slotList
                    Dim scaffale = db.ScaffaliMagazzino.Where(Function(x) x.Id = l.idEsternoScaffale).First.numScaffale
                    selectSlotList.Add(New SlotListViewModel With {
                        .Id = l.Id,
                        .Scaffale_Slot = scaffale.ToString + "-" + l.nomeSlot
                    })
                Next
            Next
            ViewBag.idSlot = New SelectList(selectSlotList, "Id", "Scaffale_Slot")
            Return PartialView(New ArticoliMagazzino With {
                .codArticolo = art.codArticolo,
                .Id = art.Id,
                .idSlot = art.idSlot,
                .noteArticolo = art.noteArticolo,
                .qta = art.qta
            })
        End Function
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function CreateArticolo(<Bind(Include:="codArticolo,qta,noteArticolo,idSlot")> ByVal ArticoliMagazzino As ArticoliMagazzino) As JsonResult
            If ModelState.IsValid Then
                Dim OpID As String = vbNullString
                Dim OpName As String = vbNullString
                Dim CurrentDate As DateTime = Now
                Try
                    OpID = User.Identity.GetUserId()
                    OpName = User.Identity.GetUserName()
                    db.ArticoliMagazzino.Add(ArticoliMagazzino)
                    db.SaveChanges()
                    db.Audit.Add(New Audit With {
                                            .Livello = TipoAuditLivello.Info,
                                            .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                            .Messaggio = "Articolo creato correttamente nel mag. 60",
                                            .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = ArticoliMagazzino.codArticolo}),
                                           .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                              })
                    db.SaveChanges()
                    Return Json(New With {.ok = True, .message = "Articolo aggiunto correttamente."})
                Catch ex As Exception
                    db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Creazione Articolo -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.art = ArticoliMagazzino})
                     })
                    db.SaveChanges()
                    Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                End Try
            End If
            Return Json(New With {.ok = False, .message = "Errore generico"})
        End Function
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function EditArticolo(<Bind(Include:="Id,codArticolo,qta,noteArticolo,idSlot")> ByVal ArticoliMagazzino As ArticoliMagazzino) As JsonResult
            If ModelState.IsValid Then
                Dim OpID As String = vbNullString
                Dim OpName As String = vbNullString
                Dim CurrentDate As DateTime = Now
                Try
                    OpID = User.Identity.GetUserId()
                    OpName = User.Identity.GetUserName()
                    Dim art = db.ArticoliMagazzino.Find(ArticoliMagazzino.Id)
                    If art.idSlot <> ArticoliMagazzino.idSlot Then
                        art.idSlot = ArticoliMagazzino.idSlot
                    End If
                    If art.qta <> ArticoliMagazzino.qta Then
                        art.qta = ArticoliMagazzino.qta
                    End If
                    If art.noteArticolo <> ArticoliMagazzino.noteArticolo Then
                        art.noteArticolo = ArticoliMagazzino.noteArticolo
                    End If
                    If art.codArticolo <> ArticoliMagazzino.codArticolo Then
                        art.codArticolo = ArticoliMagazzino.codArticolo
                    End If
                    db.Audit.Add(New Audit With {
                                            .Livello = TipoAuditLivello.Info,
                                            .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                            .Messaggio = "Articolo aggiornato correttamente",
                                            .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = ArticoliMagazzino.codArticolo}),
                                           .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                              })
                    db.SaveChanges()
                    Return Json(New With {.ok = True, .message = "Articolo aggiornato correttamente."})
                Catch ex As Exception
                    db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore aggiornamento Articolo -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.art = ArticoliMagazzino})
                     })
                    db.SaveChanges()
                    Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                End Try
            End If
            Return Json(New With {.ok = False, .message = "Errore generico"})
        End Function
        Function RicercaArticolo(ByVal stringa As String) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim art = db.ArticoliMagazzino.Where(Function(x) x.codArticolo.Contains(stringa)).FirstOrDefault
                Dim slot = db.SlotScaffale.Find(art.idSlot)
                Dim scaffale = db.ScaffaliMagazzino.Find(slot.idEsternoScaffale)
                db.Audit.Add(New Audit With {
                                            .Livello = TipoAuditLivello.Info,
                                            .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                            .Messaggio = "Articolo trovato correttamente",
                                            .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = art.Id}),
                                           .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                              })
                db.SaveChanges()
                Return Json(New With {.ok = True, .id = art.Id, .codart = art.codArticolo, .slot = slot.nomeSlot, .scaffale = scaffale.numScaffale}, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore ricerca articolo -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.art = stringa})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore:" + ex.Message}, JsonRequestBehavior.AllowGet)
            End Try

        End Function
        Function POSTEdit(ByVal id As Integer, ByVal nuovaPos As Integer) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim elem = db.ArticoliMagazzino.Find(id)
                elem.idSlot = nuovaPos
                db.SaveChanges()
                db.Audit.Add(New Audit With {
                                            .Livello = TipoAuditLivello.Info,
                                            .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                            .Messaggio = "Articolo spostato correttamente",
                                            .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                           .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                              })
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "Modificata correttamente posizione", .slot = nuovaPos, .id = id}, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore spostamento articolo -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.art = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "Errore:" + ex.Message}, JsonRequestBehavior.AllowGet)

            End Try
            Return Json(New With {.ok = True, .message = "Errore generico"}, JsonRequestBehavior.AllowGet)

        End Function
        Function EditArticoli(ByVal id As Integer) As JsonResult
            Dim scaffali = db.ScaffaliMagazzino.Where(Function(x) x.idesternoMagazzino = id).ToList
            Dim selectSlotList As New List(Of SlotListViewModel)
            For Each s In scaffali
                Dim slotList = db.SlotScaffale.Where(Function(x) x.idEsternoScaffale = s.Id).ToList
                For Each l In slotList
                    Dim scaffale = db.ScaffaliMagazzino.Where(Function(x) x.Id = l.idEsternoScaffale).First.numScaffale
                    selectSlotList.Add(New SlotListViewModel With {
                        .Id = l.Id,
                        .Scaffale_Slot = scaffale.ToString + "-" + l.nomeSlot
                    })
                Next
            Next
            ViewBag.idSlot = New SelectList(selectSlotList, "Id", "Scaffale_Slot")
            Return Json(New With {.ok = True, .message = "Errore generico", .slot = ViewBag.idSlot}, JsonRequestBehavior.AllowGet)
        End Function
        Function ListaArticoli(ByVal id As Integer) As ActionResult
            Dim listaArt = db.ArticoliMagazzino.Where(Function(x) x.idSlot = id).ToList
            Dim idSlot = listaArt.First.idSlot
            Dim slot = db.SlotScaffale.Where(Function(x) x.Id = idSlot).First
            Dim idScaffale = slot.idEsternoScaffale
            Dim scaffale = db.ScaffaliMagazzino.Where(Function(x) x.Id = idScaffale).First
            ViewBag.idMagazzino = scaffale.idesternoMagazzino
            For Each l In listaArt
                If Not IsNothing(l.noteArticolo) Then
                    If l.noteArticolo.Length > 40 Then
                        l.noteArticolo = l.noteArticolo.Substring(0, 40) + "..."
                    End If
                End If
            Next
            Return PartialView(listaArt)
        End Function
        ' POST: Overviews/Edit/5
        'Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        'Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
                                           <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,Matricola,Macchina,Zona,Data,Totale_Ore_Uomo")> ByVal overview As Overview) As ActionResult
            If ModelState.IsValid Then
                db.Entry(overview).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(overview)
        End Function
        Function VisualizzaArticolo(ByVal id As Integer) As JsonResult
            Dim art = db.ArticoliMagazzino.Find(id)
            If IsNothing(art) Then
                Return Json(New With {.ok = False, .message = "Impossibile trovare l'articolo richiesto."})
            End If
            Return Json(New With {.ok = True, .codart = art.codArticolo, .descart = art.noteArticolo, .qtaart = art.qta}, JsonRequestBehavior.AllowGet)
        End Function
        ' GET: Overviews/Delete/5
        Function DeleteArticolo(ByVal id As Integer?) As JsonResult
            If IsNothing(id) Then
                Return Json(New With {.ok = False, .message = "Impossibile trovare l'articolo richiesto."}, JsonRequestBehavior.AllowGet)
            End If
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim art = db.ArticoliMagazzino.Where(Function(x) x.Id = id).First
                db.ArticoliMagazzino.Remove(art)
                db.SaveChanges()
                db.Audit.Add(New Audit With {
                                            .Livello = TipoAuditLivello.Info,
                                            .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                            .Messaggio = "Articolo cancellato correttamente",
                                            .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                           .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                              })
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "Articolo cancellato correttamente."}, JsonRequestBehavior.AllowGet)

            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Creazione Articolo -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.art = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try

        End Function
        Function DeleteArticoloParziale(ByVal id As Integer, ByVal qta As Integer) As JsonResult
            If IsNothing(id) Then
                Return Json(New With {.ok = False, .message = "Impossibile trovare l'articolo richiesto."}, JsonRequestBehavior.AllowGet)
            End If
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim art = db.ArticoliMagazzino.Where(Function(x) x.Id = id).First
                art.qta = art.qta - qta
                db.SaveChanges()
                If art.qta <= 0 Then
                    db.ArticoliMagazzino.Remove(art)
                    db.SaveChanges()
                    db.Audit.Add(New Audit With {
                                            .Livello = TipoAuditLivello.Info,
                                            .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                            .Messaggio = "Articolo Eliminato correttamente",
                                            .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                           .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                              })
                    db.SaveChanges()
                    Return Json(New With {.ok = True, .message = "Articolo eliminato correttamente."}, JsonRequestBehavior.AllowGet)

                End If
                db.Audit.Add(New Audit With {
                                            .Livello = TipoAuditLivello.Info,
                                            .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                            .Messaggio = "Articolo scaricato parzialmente correttamente",
                                            .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                           .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                              })
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "Articolo scaricato parzialmente correttamente."}, JsonRequestBehavior.AllowGet)

            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Scarico parziale Articolo -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.art = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try

        End Function

        ' POST: Overviews/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim overview As Overview = db.Overview.Find(id)
            db.Overview.Remove(overview)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function
        Function RecursiveDistinta(ByVal listaArticoliDaDistinta As DBViewModel)
            Try
                myConn = New SqlConnection(ConnectionStringAlnus)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "select DIBCOM,DIBQTC from DIBDCO00 where ARTCOD = '" + listaArticoliDaDistinta.Codice + "'"
                myConn.Open()
            Catch ex As Exception
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    listaArticoliDaDistinta.ListaArt.Add(New DBViewModel With {
                        .Codice = myReader.GetString(0),
                        .ListaArt = New List(Of DBViewModel),
                        .Qta = myReader.GetDecimal(1)
                    })
                Loop
                myConn.Close()
                For Each l In listaArticoliDaDistinta.ListaArt
                    '-----------------------------------------------------------------START CONTOLAVORO / MATERIALI
                    Try
                        myConn = New SqlConnection(ConnectionStringAlnus)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "select ORFMOV,FAQIFA from FFODET00 where ARTCOD = '" + l.Codice + "'"
                        myConn.Open()
                    Catch ex As Exception
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader

                        Do While myReader.Read()
                            Select Case myReader.GetString(0)
                                Case "EL1"
                                    Dim CO = myReader.GetDecimal(1)
                                    l.CostoContoLavoro = l.CostoContoLavoro + Math.Round(CO, 2)
                                Case "EA1"
                                    l.CostoMateriali = Math.Round(myReader.GetDecimal(1), 2)
                            End Select
                        Loop
                        myConn.Close()

                    Catch ex As Exception

                    End Try
                    '-----------------------------------------------------------------END CONTOLAVORO
                    '-----------------------------------------------------------------START LAV INTERNO
                    Try
                        myConn = New SqlConnection(ConnectionStringAlnus)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "SELECT ODLLVR,ODLLVM FROM ODLTES00,ODLMOP00 WHERE ODLALP = '" + l.Codice + "' AND ODLMOP00.ODLNMR =ODLTES00.ODLNMR AND ODLMOP00.ODLANN = ODLTES00.ODLANN"
                        myConn.Open()
                    Catch ex As Exception
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader

                        Do While myReader.Read()
                            Dim CostoMdo = myReader.GetDecimal(0)
                            l.CostoInterno_Mdo = l.CostoInterno_Mdo + Math.Round(CostoMdo, 2)
                            Dim CostoMacc = myReader.GetDecimal(1)
                            l.CostoInterno_Macchina = l.CostoInterno_Macchina + Math.Round(CostoMacc, 2)
                        Loop
                        myConn.Close()

                    Catch ex As Exception

                    End Try
                    '-----------------------------------------------------------------END LAV INTERNO
                    '-----------------------------------------------------------------START LAV INTERNO
                    Try
                        myConn = New SqlConnection(ConnectionStringAlnus)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "select TPRCOD from ARTANA where ARTCO1 = '" + l.Codice + "'"
                        myConn.Open()
                    Catch ex As Exception
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader

                        Do While myReader.Read()
                            l.Tipoart = myReader.GetString(0)
                        Loop
                        myConn.Close()

                    Catch ex As Exception

                    End Try
                    '-----------------------------------------------------------------END LAV INTERNO
                    '-----------------------------------------------------------------Ricorsivo
                    l = RecursiveDistinta(l)
                Next
                Try
                    myConn = New SqlConnection(ConnectionStringAlnus)
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
                        ElseIf Impegni > Giacenza And Ordini > 0 Then
                            listaArticoliDaDistinta.Colore = "#1F02D9"
                        End If
                    Loop
                    myConn.Close()
                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try
            Return listaArticoliDaDistinta
        End Function
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
