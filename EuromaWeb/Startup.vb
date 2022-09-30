Imports Owin
Imports Microsoft.Owin
Imports Hangfire
Imports Hangfire.SqlServer
Imports System.Data.SqlClient
Imports Hangfire.Dashboard
Imports System.Security.Cryptography
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports EuromaWeb
Imports Microsoft.Ajax.Utilities

<Assembly: OwinStartupAttribute(GetType(Startup))>

Partial Public Class Startup
    Private db As New EuromaModels
    Private Const ConnectionString As String = "Persist Security Info=True;Password=ALNUSAD;User ID=ALNUSAD;Initial Catalog=ALNEUMA;Data Source=192.168.100.50"
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String
#If DEBUG Then
    Private Iterator Function GetHangfireServers() As IEnumerable(Of IDisposable) '127.0.0.1 
        GlobalConfiguration.Configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170).UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage("Server= 127.0.0.1; Database=HangFire; Integrated Security=True;", New SqlServerStorageOptions With { '     (localdb)\MSSQLLocalDB
            .CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            .SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            .QueuePollInterval = TimeSpan.Zero,
            .UseRecommendedIsolationLevel = True,
            .DisableGlobalLocks = True
        })
        Yield New BackgroundJobServer()
    End Function
#Else
        Private Iterator Function GetHangfireServers() As IEnumerable(Of IDisposable) '127.0.0.1 
        GlobalConfiguration.Configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170).UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage("Server=; Database=HangFire; Integrated Security=True;", New SqlServerStorageOptions With {
            .CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            .SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            .QueuePollInterval = TimeSpan.Zero,
            .UseRecommendedIsolationLevel = True,
            .DisableGlobalLocks = True
        })
        Yield New BackgroundJobServer()
    End Function
#End If

    Public Sub Configuration(app As IAppBuilder)
        ConfigureAuth(app)
        app.UseHangfireAspNet(AddressOf GetHangfireServers)
        app.UseHangfireDashboard("/jobs") ', options)
        RecurringJob.AddOrUpdate(Sub() LoadOLFromAlnus(), Cron.Hourly)
        RecurringJob.AddOrUpdate(Sub() LoadFasiOP(), Cron.MinuteInterval(4))
        RecurringJob.AddOrUpdate(Sub() UpdateTempiOpera(), Cron.HourInterval(2))
    End Sub
    Public Function LoadOLFromAlnus()
        Dim newOlList As New List(Of LavorazioniEsterne)
        Try
            Dim day = DateTime.Now.Day.ToString
            Dim month = DateTime.Now.Month.ToString
            If day.Length = 1 Then
                day = "0" + day
            End If
            If month.Length = 1 Then
                month = "0" + month
            End If
            Dim dateNow = DateTime.Now.Year.ToString + month + day
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            'myCmd.CommandText = "select TES.ESECOD,TES.ORFSEZ,TES.ORFNUM, IND.CLFEMA from ORFTES00 as TES, CLFIND as IND WHERE ORFSEZ = 'OL' AND ORFDATREV = '" + dateNow + "' AND TES.ORFCLF = IND.CLFCOD AND CLFTIP = 'F' AND CLFEMA <> ''"
            myCmd.CommandText = "select TES.ESECOD,TES.ORFSEZ,TES.ORFNUM, IND.CLFEMA from ORFTES00 as TES, CLFIND as IND WHERE ORFSEZ = 'OL' AND ORFDATREV = '" + dateNow + "' AND TES.ORFCLF = IND.CLFCOD AND CLFTIP = 'F' AND CLFEMA <> ''"
            myConn.Open()
        Catch Ex As Exception

        End Try
        Try
            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                Dim OL As New LavorazioniEsterne With {
                    .Data_Inserimento = DateTime.Now,
                    .OL = myReader.GetString(0) + "_" + myReader.GetString(1) + "_" + myReader.GetDecimal(2).ToString,
                    .Inviato = Enum_Bolla.In_attesa,
                    .Email = myReader.GetString(3)
                }
                newOlList.Add(OL)
            Loop
            myConn.Close()
            For Each O In newOlList
                If db.LavorazioniEsterne.Where(Function(x) x.OL = O.OL).Count = 0 Then
                    db.LavorazioniEsterne.Add(O)
                    db.SaveChanges()
                End If
            Next
        Catch ex As Exception
        End Try
    End Function
    'Public Function UpdateFasiOP()
    '    For Each fase In db.FasiOC.Where(Function(x) x.Completata = False)
    '        Dim prog = db.ProgettiProd.Where(Function(x) x.OC_Riferimento = fase.OC).First
    '        Dim OCAlnusCode = prog.OC_Riferimento.Substring(2, 2) + "OC"
    '        Dim incOC = prog.OC_Riferimento.Split("-").Last
    '        For i = 1 To 6 - incOC.Count
    '            OCAlnusCode = OCAlnusCode + "0"
    '        Next
    '        OCAlnusCode = OCAlnusCode + incOC
    '        myConn = New SqlConnection(ConnectionString)
    '        myCmd = myConn.CreateCommand
    '        myCmd.CommandText = "select ODLMOP00.ODLANN, ODLMOP00.ODLSEZ, ODLMOP00.ODLNMR, ODLTES00.ODLALP, ODLPDP, ODLPPR, ODLFSE, OPRDES,ODLCMC from ODLMOP00,TABOPR00,ODLTES00 where ODLMOP00.ODLOPR =  TABOPR00.OPRCO1  AND ODLMOP00.ODLCMO = '" + OCAlnusCode + "' AND ODLMOP00.ODLANN = ODLTES00.ODLANN AND ODLMOP00.ODLSEZ = ODLTES00.ODLSEZ AND ODLMOP00.ODLNMR = ODLTES00.ODLNMR"
    '        myConn.Open()
    '        Try
    '            myReader = myCmd.ExecuteReader
    '            Do While myReader.Read()
    '                Dim OP = myReader.GetString(0).ToString + "-" + myReader.GetString(1).ToString + "-" + myReader.GetDecimal(2).ToString
    '                Dim Fa = myReader.GetDecimal(6)
    '                Dim Art = myReader.GetString(3)
    '                Dim count = db.FasiOC.Where(Function(x) x.OP = OP And x.Fase = Fa And x.Articolo = Art).Count
    '                If count = 0 Then
    '                    db.FasiOC.Add(New FasiOC With {
    '                                     .OP = OP,
    '                                     .Articolo = myReader.GetString(3),
    '                                     .Qta_Da_Produrre = myReader.GetDecimal(4),
    '                                     .Qta_Prodotta = myReader.GetDecimal(5),
    '                                     .Fase = myReader.GetDecimal(6),
    '                                     .Descrizione_Fase = myReader.GetString(7),
    '                                     .OC = prog.OC_Riferimento,
    '                                     .Completata = IIf(myReader.GetDecimal(4) = myReader.GetDecimal(5), True, False),
    '                                     .Macchina = myReader.GetString(8)
    '                                   })
    '                    db.SaveChanges()
    '                    db.StoricoOC.Add(New StoricoOC With {
    '                         .Descrizione = "Inserita nuova fase per l'ODP " + myReader.GetValue(0).ToString + "-" + myReader.GetValue(1).ToString + "-" + myReader.GetValue(2).ToString,
    '                         .OC = prog.OC_Riferimento,
    '                         .Titolo = "Aggiunta fase al sistema",
    '                         .Ufficio = TipoUfficio.Produzione,
    '                         .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = "", .Operatore = "Sistema", .Data = DateTime.Now}
    '                     })
    '                    db.SaveChanges()
    '                Else
    '                    Dim f = db.FasiOC.Where(Function(x) x.OP = OP And x.Fase = Fa And x.Articolo = Art).First
    '                    If f.Qta_Da_Produrre <> myReader.GetDecimal(4) Then
    '                        f.Qta_Da_Produrre = myReader.GetDecimal(4)
    '                        db.SaveChanges()
    '                        db.StoricoOC.Add(New StoricoOC With {
    '                            .Descrizione = "Aggiornata fase per l'ODP " + OP,
    '                            .OC = prog.OC_Riferimento,
    '                            .Titolo = "Aggiornata Fase (Qta da produrre)",
    '                            .Ufficio = TipoUfficio.Produzione,
    '                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = "", .Operatore = "Sistema", .Data = DateTime.Now}
    '                        })
    '                        db.SaveChanges()
    '                    End If
    '                    If f.Qta_Prodotta <> myReader.GetDecimal(5) Then
    '                        f.Qta_Prodotta = myReader.GetDecimal(5)
    '                        db.SaveChanges()
    '                        db.StoricoOC.Add(New StoricoOC With {
    '                            .Descrizione = "Aggiornata fase per l'ODP " + OP,
    '                            .OC = prog.OC_Riferimento,
    '                            .Titolo = "Aggiornata Fase (Qta Prodotta)",
    '                            .Ufficio = TipoUfficio.Produzione,
    '                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = "", .Operatore = "Sistema", .Data = DateTime.Now}
    '                        })
    '                        db.SaveChanges()
    '                    End If
    '                    If myReader.GetDecimal(4) = myReader.GetDecimal(5) Then
    '                        f.Completata = IIf(myReader.GetDecimal(4) = myReader.GetDecimal(5), True, False)
    '                        db.SaveChanges()
    '                        db.StoricoOC.Add(New StoricoOC With {
    '                            .Descrizione = "Completata fase per l'ODP " + OP,
    '                            .OC = prog.OC_Riferimento,
    '                            .Titolo = "Fase Completata",
    '                            .Ufficio = TipoUfficio.Produzione,
    '                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = "", .Operatore = "Sistema", .Data = DateTime.Now}
    '                        })
    '                        db.SaveChanges()
    '                    End If

    '                End If
    '            Loop
    '            myConn.Close()

    '            db.SaveChanges()
    '        Catch ex As Exception

    '        End Try
    '    Next
    'End Function
    Public Function LoadFasiOP()
        Dim progList = db.ProgettiProd.Where(Function(x) x.StatoProgetto = Stato_Prod.Rilasciato).ToList
        For Each prog In progList
            Dim OCAlnusCode = prog.OC_Riferimento.Substring(2, 2) + "OC"
            Dim incOC = prog.OC_Riferimento.Split("-").Last
            For i = 1 To 6 - incOC.Count
                OCAlnusCode = OCAlnusCode + "0"
            Next
            OCAlnusCode = OCAlnusCode + incOC
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "select ODLMOP00.ODLANN, ODLMOP00.ODLSEZ, ODLMOP00.ODLNMR, ODLTES00.ODLALP, ODLPDP, ODLPPR, ODLFSE, OPRDES,ODLCMC from ODLMOP00,TABOPR00,ODLTES00 where ODLMOP00.ODLOPR =  TABOPR00.OPRCO1  AND ODLMOP00.ODLCMO = '" + OCAlnusCode + "' AND ODLMOP00.ODLANN = ODLTES00.ODLANN AND ODLMOP00.ODLSEZ = ODLTES00.ODLSEZ AND ODLMOP00.ODLNMR = ODLTES00.ODLNMR"
            myConn.Open()
            Try
                myReader = myCmd.ExecuteReader
                Do While myReader.Read()
                    Dim OP = myReader.GetString(0).ToString + "-" + myReader.GetString(1).ToString + "-" + myReader.GetDecimal(2).ToString
                    Dim Fa = myReader.GetDecimal(6)
                    Dim Art = myReader.GetString(3)
                    Dim count = db.FasiOC.Where(Function(x) x.OP = OP And x.Fase = Fa And x.Articolo = Art).Count
                    If count = 0 Then
                        db.FasiOC.Add(New FasiOC With {
                                         .OP = OP,
                                         .Articolo = myReader.GetString(3),
                                         .Qta_Da_Produrre = myReader.GetDecimal(4),
                                         .Qta_Prodotta = myReader.GetDecimal(5),
                                         .Fase = myReader.GetDecimal(6),
                                         .Descrizione_Fase = myReader.GetString(7),
                                         .OC = prog.OC_Riferimento,
                                         .Completata = IIf(myReader.GetDecimal(4) = myReader.GetDecimal(5), True, False),
                                         .Macchina = myReader.GetString(8)
                                       })
                        db.SaveChanges()
                        db.StoricoOC.Add(New StoricoOC With {
                             .Descrizione = "Inserita nuova fase per l'ODP " + myReader.GetValue(0).ToString + "-" + myReader.GetValue(1).ToString + "-" + myReader.GetValue(2).ToString,
                             .OC = prog.OC_Riferimento,
                             .Titolo = "Aggiunta fase al sistema",
                             .Ufficio = TipoUfficio.Produzione,
                             .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = "", .Operatore = "Sistema", .Data = DateTime.Now}
                         })
                        db.SaveChanges()
                    Else
                        Dim f = db.FasiOC.Where(Function(x) x.OP = OP And x.Fase = Fa And x.Articolo = Art).First
                        If f.Qta_Da_Produrre <> myReader.GetDecimal(4) Then
                            f.Qta_Da_Produrre = myReader.GetDecimal(4)
                            db.SaveChanges()
                            db.StoricoOC.Add(New StoricoOC With {
                                .Descrizione = "Aggiornata fase per l'ODP " + OP,
                                .OC = prog.OC_Riferimento,
                                .Titolo = "Aggiornata Fase (Qta da produrre)",
                                .Ufficio = TipoUfficio.Produzione,
                                .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = "", .Operatore = "Sistema", .Data = DateTime.Now}
                            })
                            db.SaveChanges()
                        End If
                        If f.Qta_Prodotta <> myReader.GetDecimal(5) Then
                            f.Qta_Prodotta = myReader.GetDecimal(5)
                            db.SaveChanges()
                            db.StoricoOC.Add(New StoricoOC With {
                                .Descrizione = "Aggiornata fase per l'ODP " + OP,
                                .OC = prog.OC_Riferimento,
                                .Titolo = "Aggiornata Fase (Qta Prodotta)",
                                .Ufficio = TipoUfficio.Produzione,
                                .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = "", .Operatore = "Sistema", .Data = DateTime.Now}
                            })
                            db.SaveChanges()
                        End If
                        If myReader.GetDecimal(4) = myReader.GetDecimal(5) Then
                            f.Completata = IIf(myReader.GetDecimal(4) = myReader.GetDecimal(5), True, False)
                            db.SaveChanges()
                        End If

                    End If
                Loop
                myConn.Close()

                db.SaveChanges()
            Catch ex As Exception

            End Try
        Next
    End Function
    Function UpdateTempiOpera() As JsonResult
        Dim listOfFasi As New List(Of OverviewViewModel)
        Dim date1 = DateTime.Today.AddDays(-2)
        Dim date2 = DateTime.Today.AddDays(-3)
        Try
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "
                         SELECT Produzione.di_matrico,ma_codice,dp_datain, dp_datafi, dp_oreuomo, dp_autoinc, di_nomina,F.pb_codice 
                         FROM [Opera6010].[dbo].[Produzione], [Opera6010].[dbo].[Dipendenti] as D, [Opera6010].[dbo].[Fasi]  as F                
                         WHERE Produzione.fa_id = F.fa_id and Produzione.di_matrico = D.di_matrico AND dp_datain < '" & date1.Day.ToString.PadLeft(2, "0"c) + "/" + date1.Month.ToString.PadLeft(2, "0"c) + "/" + date1.Year.ToString & "' AND dp_datain > '" & date2.Day.ToString.PadLeft(2, "0"c) + "/" + date2.Month.ToString.PadLeft(2, "0"c) + "/" + date2.Year.ToString & "'
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
            db.Log.Add(New Log With {
                   .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = "", .Operatore = "Sistema"},
                   .Livello = TipoLogLivello.Errors,
                   .Indirizzo = "Startup.vb",
                   .Messaggio = "Errore Query tempi opera -> " & ex.Message,
                   .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = DateTime.Now.Ticks.ToString})
                   })
            db.SaveChanges()
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
                Dim idFase = fase.OP
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
                                    .Nomina = d.Nomina
                                }
                Dim alreadyExist = db.Overview.Where(Function(x) x.Id_Opera = DatasObj.Id_Opera).FirstOrDefault
                If IsNothing(alreadyExist) Then
                    db.Overview.Add(DatasObj)
                    db.SaveChanges()
                End If
            Catch ex As Exception
                db.Log.Add(New Log With {
                 .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = "", .Operatore = "Sistema"},
                 .Livello = TipoLogLivello.Errors,
                 .Indirizzo = "Startup.vb",
                 .Messaggio = "Errore Salvataggio tempi opera -> " & ex.Message,
                 .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = d.OP})
                 })
                db.SaveChanges()
            End Try

            Console.WriteLine(totalMinutes)
        Next
    End Function
End Class
