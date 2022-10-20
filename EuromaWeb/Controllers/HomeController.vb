Imports System.Data.SqlClient
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports System.IO.Directory
Imports Microsoft.AspNet.Identity

Public Class HomeController
    Inherits System.Web.Mvc.Controller
    Private Const ConnectionString As String = "Persist Security Info=True;Password=ALNUSAD;User ID=ALNUSAD;Initial Catalog=ALNEUMA;Data Source=192.168.100.50"
    Private Const ConnectionStringLocal As String = "Persist Security Info=True;Initial Catalog=Prova;Data Source=(localdb)\MSSQLLocalDB"
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Private db As New EuromaModels
    Private appctx As New ApplicationDbContext
    Private firstCall = 0
    <Authorize>
    Function Index() As ActionResult
        If Not User.Identity.IsAuthenticated Then
            Return RedirectToAction("Login", "Account")
        End If
        ViewBag.divPermessi_CMT = appctx.Users.Where(Function(x) x.UserName = User.Identity.Name).First.Profile.CMT
        ViewBag.divPermessi_ISA = appctx.Users.Where(Function(x) x.UserName = User.Identity.Name).First.Profile.ISA
        ViewBag.divPermessi_UNI = appctx.Users.Where(Function(x) x.UserName = User.Identity.Name).First.Profile.UNI
        ViewBag.divPermessi_MPA = appctx.Users.Where(Function(x) x.UserName = User.Identity.Name).First.Profile.MPA
        ViewBag.divPermessi_Drill = appctx.Users.Where(Function(x) x.UserName = User.Identity.Name).First.Profile.Drill

        ViewBag.Local = Request.IsLocal
        ViewBag.countDB1 = db.Disegni_MPA.Where(Function(x) x.Cliente.Contains("E:\") Or x.Descrizione.Contains("E:\") Or x.Desc_Alnus.Contains("E:\") Or x.Path_File.Contains("E:\") Or x.Triple_Code.Contains("E:\") Or x.User.Contains("E:\")).Count
        ViewBag.countDB2 = db.Disegni_MPA_server.Count
        Dim Generator As System.Random = New System.Random()
        Select Case Generator.Next(0, 9)
            Case 1
                ViewBag.RandomIcon1 = "fa-solid fa-book-journal-whills"
            Case 2
                ViewBag.RandomIcon1 = "fa-brands fa-galactic-republic"
            Case 3
                ViewBag.RandomIcon1 = "fa-solid fa-jedi"
            Case 4
                ViewBag.RandomIcon1 = "fa-brands fa-galactic-senate"
            Case 5
                ViewBag.RandomIcon1 = "fa-brands fa-jedi-order"
            Case 6
                ViewBag.RandomIcon1 = "fa-brands fa-old-republic"
            Case 7
                ViewBag.RandomIcon1 = "fa-solid fa-wand-sparkles"
            Case 8
                ViewBag.RandomIcon1 = "fa-solid fa-user-astronaut"
            Case Else
                ViewBag.RandomIcon1 = "fa-solid fa-biohazard"
        End Select
        Select Case Generator.Next(0, 9)
            Case 1
                ViewBag.RandomIcon2 = "fa-solid fa-book-journal-whills"
            Case 2
                ViewBag.RandomIcon2 = "fa-brands fa-galactic-republic"
            Case 3
                ViewBag.RandomIcon2 = "fa-solid fa-jedi"
            Case 4
                ViewBag.RandomIcon2 = "fa-brands fa-galactic-senate"
            Case 5
                ViewBag.RandomIcon2 = "fa-brands fa-jedi-order"
            Case 6
                ViewBag.RandomIcon2 = "fa-brands fa-old-republic"
            Case 7
                ViewBag.RandomIcon2 = "fa-solid fa-wand-sparkles"
            Case 8
                ViewBag.RandomIcon2 = "fa-solid fa-user-astronaut"
            Case Else
                ViewBag.RandomIcon2 = "fa-solid fa-biohazard"
        End Select
        Return View()
    End Function
    Function Notifiche() As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpID = User.Identity.GetUserId()
            OpName = User.Identity.GetUserName()
            Dim countNote = db.NotePerOC.Where(Function(x) x.OC.Contains("OC") And x.Operatore_Id <> OpID And x.Data_Nota > "2022/10/17").ToList
            Dim countFile = db.DocumentiPerOC.Where(Function(x) x.OC.Contains("OC") And x.Operatore_Id <> OpID And x.DataCreazioneFile > "2022/10/17").ToList
            Dim retList As New List(Of NotificheViewModel)
            If countNote.Count > 0 Then
                For Each n In countNote
                    If db.VisualizzazioneFileNota.Where(Function(x) x.type = TipoVisualizzazione.Nota And x.User = OpID And x.id_filenota = n.Id).Count = 0 Then
                        Dim tempo = ""
                        If (CurrentDate - n.Data_Nota).Days > 0 Then
                            tempo = (CurrentDate - n.Data_Nota).Days.ToString + " giorno/i fa"
                        ElseIf (CurrentDate - n.Data_Nota).hours > 0 Then
                            tempo = (CurrentDate - n.Data_Nota).Hours.ToString + " ora/e fa"
                        Else
                            tempo = (CurrentDate - n.Data_Nota).Minutes.ToString + " minuto/i fa"
                        End If
                        retList.Add(New NotificheViewModel With {
                            .Descrizione = n.Operatore_Nome + " ha aggiunto una nuova nota all'" + n.OC,
                            .TipologiaNotifica = "fa-comment",
                            .Link = "/Home/NotificaLetta?id=" + n.Id.ToString + "&Type=2",
                            .ElapsedTime = tempo,
                            .DataAzione = n.Data_Nota
                        })
                    End If
                Next
            End If
            If countFile.Count > 0 Then
                For Each n In countFile
                    If db.VisualizzazioneFileNota.Where(Function(x) x.type = TipoVisualizzazione.File And x.User = OpID And x.id_filenota = n.Id).Count = 0 Then
                        Dim tempo = ""
                        If (CurrentDate - n.DataCreazioneFile).Days > 0 Then
                            tempo = (CurrentDate - n.DataCreazioneFile).Days.ToString + " giorno/i fa"
                        ElseIf (CurrentDate - n.DataCreazioneFile).Hours > 0 Then
                            tempo = (CurrentDate - n.DataCreazioneFile).Hours.ToString + " ora/e fa"
                        Else
                            tempo = (CurrentDate - n.DataCreazioneFile).Minutes.ToString + " minuto/i fa"
                        End If
                        retList.Add(New NotificheViewModel With {
                            .Descrizione = n.Operatore_Nome + " ha aggiunto un nuovo file all'" + n.OC,
                            .TipologiaNotifica = "fa-file",
                            .Link = "/Home/NotificaLetta?id=" + n.Id.ToString + "&Type=1",
                            .ElapsedTime = tempo,
                            .DataAzione = n.DataCreazioneFile
                        })
                    End If
                Next
            End If
            retList = retList.OrderByDescending(Function(x) x.DataAzione.Ticks).ToList
            Return PartialView(retList)
        Catch ex As Exception

        End Try

    End Function
    Function NotificaLetta(ByVal id As Integer, ByVal Type As Integer) As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpID = User.Identity.GetUserId()
            OpName = User.Identity.GetUserName()
            db.VisualizzazioneFileNota.Add(New VisualizzazioneFileNota With {
                  .id_filenota = id,
                  .type = Type,
                  .ReaedingDate = DateTime.Now,
                  .User = OpID
              })
            db.SaveChanges()
            db.Audit.Add(New Audit With {
                                  .Livello = TipoAuditLivello.Info,
                                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                  .Messaggio = "Notifica correttamente visualizzata",
                                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                 .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                    })
            db.SaveChanges()
            Dim OC = ""
            If Type = 2 Then
                OC = db.NotePerOC.Find(id).OC
            Else
                OC = db.DocumentiPerOC.Find(id).OC
            End If
            Return RedirectToAction("Ordine", "Overviews", New With {.id = OC})
        Catch ex As Exception
            db.Log.Add(New Log With {
                                       .Livello = TipoAuditLivello.Info,
                                       .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                       .Messaggio = "Errore aggiunta visualizzazione Notifica",
                                       .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                      .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                         })
            db.SaveChanges()
        End Try

    End Function
    Function About() As ActionResult
        ViewData("Message") = "Your application description page."

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function
    Function InventarioPerMagazzino() As ActionResult
        Return View()
    End Function
    <Authorize>
    <HttpPost()>
    <ValidateAntiForgeryToken()>
    Function InventarioPerMagazzino(<Bind(Include:="CodMagazzino")> ByVal PostedVM As MagazzinoViewModel) As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        OpName = User.Identity.Name
        If IsNothing(PostedVM.CodMagazzino) Then
            Return Nothing
        End If
        Dim listArticoli As New List(Of ArticoloMagazzinoViewModel)
        'Ricerca Articoli per condominio
        Try
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            If Not PostedVM.CodMagazzino.ToString = "0" Then
                myCmd.CommandText = "SELECT ARTDMG.MAGCOD as Magazzino, ARTDMG.ARTCOD as Articolo,
                        DMGVER as Versione, ARTDMG.ARTGIA as Quantita,
                        ARTDMA.MAZONA as Zona, ARTDMA.MACORS as Corsia,
                        ARTDMA.MABAYY as Campata, ARTDMA.MACAMP as Posizione,
                        MATTRA as Tipologia, MATRAC as Matricola, MACOMM as Commessa,
                        convert(varchar, GETDATE(), 112) as Data, ' ' as Cartellino,
                        ' ' as EAN, ARTDES
                        FROM ARTDMG (NOLOCK),
                        ARTDMA (NOLOCK),
                        ARTANA (NOLOCK)
                        WHERE ARTDMG.ARTCOD = ARTDMA.ARTCOD
                        AND ARTDMG.DMGVER = ARTVER
                        AND ARTDMG.MAGCOD = ARTDMA.MAGCOD
                        AND ARTDMG.ARTCOD = ARTCO1
                        -- AND ARTDMG.ARTGIA <> 0
                        -- AND ARTDMA.MABAYY = ' '
                        -- AND ARTANA.TPRCOD = 'MIN'
                        AND ARTDMA.MABAYY = '" & PostedVM.CodMagazzino.ToString & "'
                        union
                        SELECT ARTDMA.MAGCOD as Magazzino, ARTPIA.ARTCOD as Articolo,
                        isnull(DIBVER, ' ') as Versione, 0 as Quantita, MAZONA as Zona,
                        MACORS as Corsia, MABAYY as Campata, MACAMP as Posizione,
                        case when ARTGAR = 'S' then 'M' when ARTGLO = 'S' then 'L' else ' ' end
                        as Tipologia, ' ' as Matricola, ' ' as Commessa,
                        convert(varchar, GETDATE(), 112) as Data, ' ' as Cartellino,
                        ' ' as EAN, ARTDES
                        FROM ARTPIA (NOLOCK)
                        INNER JOIN ARTMAG (NOLOCK) ON ARTMAG.ARTCOD = ARTPIA.ARTCOD
                        INNER JOIN ARTDMA (NOLOCK) ON ARTDMA.ARTCOD = ARTPIA.ARTCOD
                        INNER JOIN ARTANA (NOLOCK) ON ARTCO1 = ARTPIA.ARTCOD
                        LEFT JOIN DIBTES00 (NOLOCK) ON ARTCO1 = DIBTES00.ARTCOD
                        AND DIBVDF = 'S'
                        WHERE ARTCO1 NOT IN
                        (SELECT ARTDMG.ARTCOD FROM ARTDMG (NOLOCK))
                        --AND ARTANA.TPRCOD = 'MIN'
                        AND ARTDMA.MABAYY = '" & PostedVM.CodMagazzino.ToString & "'
                        order by ARTDMG.MAGCOD, ARTDMA.MAZONA, ARTDMA.MACORS,
                        ARTDMA.MABAYY, ARTDMA.MACAMP, ARTDMG.ARTCOD, ARTDMG.DMGVER
 "
            Else
                myCmd.CommandText = "
             SELECT  ARTDMA.MACAMP as Posizione,ARTDMG.ARTCOD as Articolo, ARTDMG.ARTGIA as Quantita,  ARTDES
                FROM ARTDMG (NOLOCK),
                ARTDMA (NOLOCK),
                ARTANA (NOLOCK)
                WHERE ARTDMG.ARTCOD = ARTDMA.ARTCOD
                AND ARTDMG.DMGVER = ARTVER
                AND ARTDMG.MAGCOD = ARTDMA.MAGCOD
                AND ARTDMG.ARTCOD = ARTCO1
                --AND ARTDMA.MABAYY = '" & PostedVM.CodMagazzino.ToString & "'
                order by  ARTDMA.MACAMP,ARTDMG.ARTCOD,ARTDMG.ARTGIA "
            End If

            myConn.Open()
        Catch ex As Exception

        End Try
        'Parse dei dati da SQL
        Try
            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                If PostedVM.CodMagazzino <> "0" Then
                    Dim articolo As New ArticoloMagazzinoViewModel With {
                   .Posizione = myReader.GetString(7),
                   .Articolo = myReader.GetString(1),
                   .Quantita = myReader.GetDecimal(3).ToString,
                   .Desc_Articolo = myReader.GetString(14)
               }
                    listArticoli.Add(articolo)
                Else
                    Dim articolo As New ArticoloMagazzinoViewModel With {
                   .Posizione = myReader.GetString(0),
                   .Articolo = myReader.GetString(1),
                   .Quantita = myReader.GetDecimal(2).ToString,
                   .Desc_Articolo = myReader.GetString(3)
               }
                    listArticoli.Add(articolo)
                End If

                'results = results & myReader.GetString(0) & vbTab &
                'myReader.GetString(1) & vbLf
            Loop
            myConn.Close()

        Catch ex As Exception

        End Try
        'Apertura file
        Dim fs As New FileStream(Server.MapPath("\Content\Template\Magazzini_Template.xlsx"), FileMode.Open, FileAccess.Read)
        Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
        Dim ws As XSSFSheet = workbook.GetSheet("Magazzino")
        'Start Pop
        Dim i As Integer = 1
        Dim baserow As IRow = ws.GetRow(0)
        'Dim baserow As IRow = ws.GetRow(2)
        Dim ms As New MemoryStream
        Dim ms1 As New MemoryStream
        Dim base_value = listArticoli.First.Posizione
        'Riga Intestazione
        Try
            Try
                For Each l In listArticoli

                    Dim r As IRow = ws.CreateRow(i)
                    For j = 0 To 4
                        r.CreateCell(j).CellStyle = baserow.GetCell(j).CellStyle
                    Next

                    Try
                        If Not IsNothing(l.Posizione) Then
                            r.GetCell(0).SetCellValue(l.Posizione)
                        Else
                            r.GetCell(0).SetCellValue(" ")
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(1).SetCellValue(l.Articolo)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(2).SetCellValue(l.Desc_Articolo)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(3).SetCellValue(l.Quantita)
                    Catch ex As Exception

                    End Try
                    If Not base_value = l.Posizione Then
                        ws.SetRowBreak(i - 1)
                        base_value = l.Posizione
                    End If
                    i = i + 1
                Next
            Catch ex As Exception

            End Try
            'Intestazione

            'Dati rilevati

            workbook.Write(ms)
            If PostedVM.CodMagazzino = 0 Then
                Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - UBICAZIONE MANCANTE.xlsx")
            Else
                Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - MAGAZZINO " & PostedVM.CodMagazzino & ".xlsx")
            End If
        Catch ex As Exception

        End Try


    End Function
    <Authorize>
    <HttpPost>
    Function Lotti(ByVal stringa As String) As JsonResult
        stringa = stringa.ToUpper
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        OpName = User.Identity.Name

        Dim lotto As New LottiViewModel
        'Ricerca Articoli per condominio
        Try
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "
             select DISTINCT ARTCO1,ARTDES,CAST(ARTLOM as INTEGER),CAST(ARTSCM as INTEGER), CAST(ARTGIA as INTEGER),TPRCOD from ARTANA, ARTPIA,ARTDMA
                WHERE ARTANA.ARTCO1 = '" + stringa + "' AND ARTANA.ARTCO1 = ARTPIA.ARTCOD AND ARTANA.ARTCO1 = ARTDMA.ARTCOD"
            myConn.Open()
        Catch ex As Exception

        End Try
        'Parse dei dati da SQL
        Try
            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                lotto.Art = myReader.GetString(0).Trim
                lotto.Descr = myReader.GetString(1).Trim
                lotto.LottoMin = myReader.GetInt32(2)
                lotto.Scorta = myReader.GetInt32(3)
                lotto.CurrentGiacenza = myReader.GetInt32(4)
                lotto.TipoParte = myReader.GetString(5).Trim
            Loop
            myConn.Close()

        Catch ex As Exception

        End Try

        Try
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "select DISTINCT ODLALP, CAST(SUM(ODLQTP) as INTEGER) from ODLTES00 where ODLALP = '" + stringa + "' AND ODLDIUREV > '20220101' AND ODLDIUREV < '20221231' AND ODLSTS = '080' group by ODLALP"
            myConn.Open()
        Catch ex As Exception

        End Try
        'Parse dei dati da SQL
        Try
            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                If Not IsDBNull(myReader.GetValue(1)) Then
                    lotto.AnnoCurrent = myReader.GetInt32(1)
                End If
            Loop
            myConn.Close()

        Catch ex As Exception

        End Try
        Try
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "select DISTINCT ODLALP, CAST(SUM(ODLQTP) as INTEGER) from ODLTES00 where ODLALP = '" + stringa + "' AND ODLDIUREV > '20220101' AND ODLDIUREV < '20221231' AND ODLSTS < '080' group by ODLALP"
            myConn.Open()
        Catch ex As Exception

        End Try
        'Parse dei dati da SQL
        Try
            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                If Not IsDBNull(myReader.GetValue(1)) Then
                    lotto.AnnoCurrentInAttesa = myReader.GetInt32(1)
                End If
            Loop
            myConn.Close()

        Catch ex As Exception

        End Try
        Try
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "select DISTINCT ARTCOD, CAST(SUM(MVMCVT) as INTEGER)  from MVMDET00 where ARTCOD = '" + stringa + "' AND MVMDREREV > '20210101' AND MVMDREREV < '20211231' AND MVMCAU = 'UP1' GROUP BY ARTCOD"
            myConn.Open()
        Catch ex As Exception

        End Try
        'Parse dei dati da SQL
        Try
            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                lotto.ConsumoAnnoPrec = myReader.GetInt32(1)
            Loop
            myConn.Close()

        Catch ex As Exception

        End Try
        Try
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "select DISTINCT ARTCOD, CAST(SUM(MVMCVT) as INTEGER) from MVMDET00 where ARTCOD='" + stringa + "' AND MVMDREREV > '20220101' AND MVMDREREV < '20221231' AND MVMCAU = 'UP1' GROUP BY ARTCOD"
            myConn.Open()
        Catch ex As Exception

        End Try
        'Parse dei dati da SQL
        Try
            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                lotto.ConsumoAnnoCurrent = myReader.GetInt32(1)
            Loop
            myConn.Close()

        Catch ex As Exception

        End Try
        Try
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "select DISTINCT ODLALP, CAST(SUM(ODLQTP) as INTEGER) from ODLTES00 where ODLALP = '" + stringa + "' AND ODLDIUREV > '20210101' AND ODLDIUREV < '20211231' group by ODLALP"
            myConn.Open()
        Catch ex As Exception

        End Try
        'Parse dei dati da SQL
        Try
            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                If Not IsDBNull(myReader.GetValue(1)) Then
                    lotto.AnnoPrec = myReader.GetInt32(1)
                End If
            Loop
            myConn.Close()

        Catch ex As Exception

        End Try
        db.Audit.Add(New Audit With {
                                         .Livello = TipoAuditLivello.Info,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Ricercato articolo per lotto",
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.art = stringa}),
                                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                           })
        db.SaveChanges()
        'Apertura file
        'Dim fs As New FileStream(Server.MapPath("\Content\Template\Lotti_template.xlsx"), FileMode.Open, FileAccess.Read)
        'Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
        'Dim ws As XSSFSheet = workbook.GetSheetAt(0)
        ''Start Pop
        'Dim i As Integer = 1
        'Dim baserow As IRow = ws.GetRow(0)
        ''Dim baserow As IRow = ws.GetRow(2)
        'Dim ms As New MemoryStream
        'Dim ms1 As New MemoryStream
        ''Riga Intestazione
        'Try
        '    Try
        '        For Each l In ListLotti

        '            Dim r As IRow = ws.CreateRow(i)
        '            For j = 0 To 8
        '                r.CreateCell(j).CellStyle = baserow.GetCell(j).CellStyle
        '            Next

        '            Try
        '                r.GetCell(0).SetCellValue(l.Art)
        '            Catch ex As Exception

        '            End Try
        '            Try
        '                r.GetCell(1).SetCellValue(l.Descr)
        '            Catch ex As Exception

        '            End Try
        '            Try
        '                r.GetCell(2).SetCellValue(l.LottoMin)
        '            Catch ex As Exception

        '            End Try
        '            Try
        '                r.GetCell(3).SetCellValue(l.Scorta)
        '            Catch ex As Exception

        '            End Try
        '            Try
        '                r.GetCell(4).SetCellValue(l.TipoParte)
        '            Catch ex As Exception

        '            End Try
        '            'Try
        '            '    r.GetCell(4).SetCellValue(l.AnnoPrec)
        '            'Catch ex As Exception

        '            'End Try
        '            Try
        '                r.GetCell(5).SetCellValue(l.ConsumoAnnoPrec)
        '            Catch ex As Exception

        '            End Try
        '            'Try
        '            '    r.GetCell(6).SetCellValue(l.AnnoCurrent)
        '            'Catch ex As Exception

        '            'End Try
        '            Try
        '                r.GetCell(6).SetCellValue(l.ConsumoAnnoCurrent)
        '            Catch ex As Exception

        '            End Try
        '            Try
        '                r.GetCell(7).SetCellValue(l.AnnoCurrentInAttesa)
        '            Catch ex As Exception

        '            End Try
        '            Try
        '                r.GetCell(8).SetCellValue(l.CurrentGiacenza)
        '            Catch ex As Exception

        '            End Try
        '            i = i + 1
        '        Next
        '    Catch ex As Exception

        '    End Try
        '    'Intestazione

        '    'Dati rilevati

        '    workbook.Write(ms)
        '    Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - LISTA LOTTI.xlsx")
        'Catch ex As Exception

        'End Try

        Return Json(New With {.ok = True, .data = lotto}, JsonRequestBehavior.AllowGet)
    End Function
    <Authorize>
    Function ValorizzazioneMagazzino() As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Dim voc As New Dictionary(Of String, Decimal)
        Dim Tutti_Articoli_Con_Giacenza As New Dictionary(Of String, Decimal)
        OpName = User.Identity.Name
        'Ricerca Articoli per condominio
        Try
            myConn = New SqlConnection(ConnectionStringLocal)
            myCmd = myConn.CreateCommand
            myCmd.CommandText =
                "
                SELECT [COD_ART]
                      ,[COD_GIA]
                FROM [Prova].[dbo].[Tab_Gia_Final]
                 "

            myConn.Open()
        Catch ex As Exception

        End Try
        'Parse dei dati da SQL
        Try

            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                If voc.ContainsKey(myReader.GetString(0).Replace(" ", "").ToString) Then
                    voc.Item(myReader.GetString(0).Replace(" ", "").ToString) = voc.Item(myReader.GetString(0).Replace(" ", "").ToString) + myReader.GetDecimal(1)
                Else
                    voc.Add(myReader.GetString(0).Replace(" ", "").ToString, myReader.GetDecimal(1))
                End If
                'results = results & myReader.GetString(0) & vbTab &
                'myReader.GetString(1) & vbLf
            Loop
            myConn.Close()

        Catch ex As Exception

        End Try
        'Ricerca tot articoli con giacenza
        Try
            myConn = New SqlConnection(ConnectionStringLocal)
            myCmd = myConn.CreateCommand
            myCmd.CommandText =
                "
                SELECT [COD_ART]
                      ,[COD_GIA]
                FROM [Prova].[dbo].[Tab_Gia_Final]
				where cod_gia > 0 
                 "

            myConn.Open()
        Catch ex As Exception

        End Try
        'Parse dei dati da SQL
        Try

            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                If Tutti_Articoli_Con_Giacenza.ContainsKey(myReader.GetString(0).Replace(" ", "").ToString) Then
                    Tutti_Articoli_Con_Giacenza.Item(myReader.GetString(0).Replace(" ", "").ToString) = Tutti_Articoli_Con_Giacenza.Item(myReader.GetString(0).Replace(" ", "").ToString) + myReader.GetDecimal(1)
                Else
                    Tutti_Articoli_Con_Giacenza.Add(myReader.GetString(0).Replace(" ", "").ToString, myReader.GetDecimal(1))
                End If
                'results = results & myReader.GetString(0) & vbTab &
                'myReader.GetString(1) & vbLf
            Loop
            myConn.Close()

        Catch ex As Exception

        End Try
        'Apertura file
        Dim fs As New FileStream(Server.MapPath("\Content\Template\INVENTARIO_VALORIZZAZIONE_TEMPLATE.xlsx"), FileMode.Open, FileAccess.Read)
        Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
        'Start Pop
        Dim i As Integer = 1
        Dim ms As New MemoryStream
        Dim ms1 As New MemoryStream
        Dim existing_Art As New List(Of String)
        'Riga Intestazione
        Try
            Try
                For i = 1 To 5
                    Try
                        Dim ws As XSSFSheet = workbook.GetSheetAt(i)
                        With ws
                            For a = 1 To .LastRowNum
                                Try
                                    Dim cod = GetCellValue(.GetRow(a), 0).ToString.Replace(" ", "").ToString
                                    Dim giacenza = voc.Item(cod)
                                    existing_Art.Add(cod)
                                    If Tutti_Articoli_Con_Giacenza.Where(Function(x) x.Key = cod).Count > 0 Then
                                        Tutti_Articoli_Con_Giacenza.Remove(cod)
                                    End If
                                    If Not IsNothing(giacenza) Then
                                        Dim r As IRow = ws.GetRow(a)
                                        r.GetCell(2).SetCellValue(giacenza)
                                    Else
                                        Dim r As IRow = .GetRow(a)
                                        r.GetCell(2).SetCellValue("0")
                                    End If
                                Catch ex As Exception
                                    Dim r As IRow = .GetRow(a)
                                    r.GetCell(2).SetCellValue(" NULL ")
                                End Try
                            Next
                        End With

                    Catch ex As Exception

                    End Try



                Next
                Dim ws2 = workbook.GetSheetAt(6)
                Dim result = existing_Art.Where(Function(p) Tutti_Articoli_Con_Giacenza.All(Function(p2) p2.Key <> p)).ToList
                With ws2
                    Dim a = 1
                    For Each b In Tutti_Articoli_Con_Giacenza
                        Try
                            If Not existing_Art.Contains(b.Key.ToString) Then
                                Dim val = ""
                                Try
                                    myConn = New SqlConnection(ConnectionString)
                                    myCmd = myConn.CreateCommand
                                    myCmd.CommandText =
                                        "
                                            Select DISTINCT ARTCOD, REPLACE(MAX(COSGIA), '.',','), 
                                            Replace(MAX(COSTOT), '.',','), 
                                            Replace(MAX(COSGIA) * MAX(COSTOT), '.',',') 
                                            From COSART00 Where ARTCOD = '" + b.Key.ToString + "' GROUP BY ARTCOD 
                                         "
                                    myConn.Open()
                                Catch ex As Exception

                                End Try
                                'Parse dei dati da SQL
                                Try

                                    myReader = myCmd.ExecuteReader
                                    Do While myReader.Read()
                                        val = myReader.GetString(2)
                                    Loop
                                    myConn.Close()

                                Catch ex As Exception

                                End Try

                                Dim r As IRow = ws2.CreateRow(a)
                                For j = 0 To 3
                                    r.CreateCell(j)
                                Next
                                r.GetCell(0).SetCellValue(b.Key)
                                r.GetCell(1).SetCellValue(b.Value.ToString)
                                r.GetCell(2).SetCellValue(val.ToString)
                                a = a + 1
                            End If

                        Catch ex As Exception
                            Dim r As IRow = ws2.CreateRow(a)
                                r.GetCell(0).SetCellValue(" NULL ")
                                r.GetCell(1).SetCellValue(" 0.00 ")
                                a = a + 1
                            End Try
                    Next
                End With
            Catch ex As Exception

            End Try

            workbook.Write(ms)
            Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - VALORIZZAZIONE_MAGAZZINO.xlsx")
        Catch ex As Exception

        End Try


    End Function
    '<ConfigurationProperty("validateRequest", DefaultValue:=False)>
    Function FileSearcher(ByVal id As String) As FileResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        OpName = User.Identity.Name
        Try
            Dim s = Convert.FromBase64String(id)
            Dim search_path = System.Text.Encoding.UTF8.GetString(s)
            'Dim search_path = HttpUtility.UrlDecode(id.ToString.Replace("+", "%")) '.Replace("-", ".")
            Dim file = New FileInfo(search_path)

            If (file.Exists) Then
                Response.ClearHeaders()
                Response.ClearContent()
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", file.Name))
                Response.AddHeader("Content-Length", file.Length.ToString)
                Response.ContentType = "application/x-download"
                If file.FullName.Contains(".mi") Then
                    Response.TransmitFile(file.FullName)
                Else
                    Response.TransmitFile(file.FullName + ".mi")
                End If
                Response.End()
            Else

            End If
        Catch ex As Exception
            db.Log.Add(New Log With {
                                         .Livello = TipoLogLivello.Errors,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Errore: " + ex.Message,
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = id}),
                                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                           })
            db.SaveChanges()
        End Try

    End Function
    'Function MagazzinoInventario() As ActionResult
    '    Dim OpID As String = vbNullString
    '    Dim OpName As String = vbNullString
    '    Dim CurrentDate As DateTime = Now
    '    OpName = User.Identity.Name

    '    Dim listArticoli As New List(Of ArticoloMagazzinoViewModel)
    '    'Ricerca Articoli per condominio
    '    Try
    '        myConn = New SqlConnection(ConnectionString)
    '        myCmd = myConn.CreateCommand

    '        myCmd.CommandText = "
    '         SELECT  ARTDMA.MACAMP as Posizione,ARTDMG.ARTCOD as Articolo, ARTDMG.ARTGIA as Quantita,  ARTDES
    '            FROM ARTDMG (NOLOCK),
    '            ARTDMA (NOLOCK),
    '            ARTANA (NOLOCK)
    '            WHERE ARTDMG.ARTCOD = ARTDMA.ARTCOD
    '            AND ARTDMG.DMGVER = ARTVER
    '            AND ARTDMG.MAGCOD = ARTDMA.MAGCOD
    '            AND ARTDMG.ARTCOD = ARTCO1
    '            --AND ARTDMA.MABAYY = ''
    '            order by  ARTDMA.MACAMP,ARTDMG.ARTCOD,ARTDMG.ARTGIA "

    '        myConn.Open()
    '    Catch ex As Exception

    '    End Try
    '    'Parse dei dati da SQL
    '    Try
    '        myReader = myCmd.ExecuteReader
    '        Do While myReader.Read()
    '            If PostedVM.CodMagazzino <> "0" Then
    '                Dim articolo As New ArticoloMagazzinoViewModel With {
    '               .Posizione = myReader.GetString(7),
    '               .Articolo = myReader.GetString(1),
    '               .Quantita = myReader.GetDecimal(3).ToString,
    '               .Desc_Articolo = myReader.GetString(14)
    '           }
    '                listArticoli.Add(articolo)
    '            Else
    '                Dim articolo As New ArticoloMagazzinoViewModel With {
    '               .Posizione = myReader.GetString(0),
    '               .Articolo = myReader.GetString(1),
    '               .Quantita = myReader.GetDecimal(2).ToString,
    '               .Desc_Articolo = myReader.GetString(3)
    '           }
    '                listArticoli.Add(articolo)
    '            End If

    '            'results = results & myReader.GetString(0) & vbTab &
    '            'myReader.GetString(1) & vbLf
    '        Loop
    '        myConn.Close()

    '    Catch ex As Exception

    '    End Try
    '    'Apertura file
    '    Dim fs As New FileStream(Server.MapPath("\Content\Template\Magazzini_Template.xlsx"), FileMode.Open, FileAccess.Read)
    '    Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
    '    Dim ws As XSSFSheet = workbook.GetSheet("Magazzino")
    '    'Start Pop
    '    Dim i As Integer = 1
    '    Dim baserow As IRow = ws.GetRow(0)
    '    'Dim baserow As IRow = ws.GetRow(2)
    '    Dim ms As New MemoryStream
    '    Dim ms1 As New MemoryStream
    '    Dim base_value = listArticoli.First.Posizione
    '    'Riga Intestazione
    '    Try
    '        Try
    '            For Each l In listArticoli

    '                Dim r As IRow = ws.CreateRow(i)
    '                For j = 0 To 4
    '                    r.CreateCell(j).CellStyle = baserow.GetCell(j).CellStyle
    '                Next

    '                Try
    '                    If Not IsNothing(l.Posizione) Then
    '                        r.GetCell(0).SetCellValue(l.Posizione)
    '                    Else
    '                        r.GetCell(0).SetCellValue(" ")
    '                    End If
    '                Catch ex As Exception

    '                End Try
    '                Try
    '                    r.GetCell(1).SetCellValue(l.Articolo)
    '                Catch ex As Exception

    '                End Try
    '                Try
    '                    r.GetCell(2).SetCellValue(l.Desc_Articolo)
    '                Catch ex As Exception

    '                End Try
    '                Try
    '                    r.GetCell(3).SetCellValue(l.Quantita)
    '                Catch ex As Exception

    '                End Try
    '                If Not base_value = l.Posizione Then
    '                    ws.SetRowBreak(i - 1)
    '                    base_value = l.Posizione
    '                End If
    '                i = i + 1
    '            Next
    '        Catch ex As Exception

    '        End Try
    '        'Intestazione

    '        'Dati rilevati

    '        workbook.Write(ms)
    '        If PostedVM.CodMagazzino = 0 Then
    '            Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - UBICAZIONE MANCANTE.xlsx")
    '        Else
    '            Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - MAGAZZINO " & PostedVM.CodMagazzino & ".xlsx")
    '        End If
    '    Catch ex As Exception

    '    End Try


    'End Function
    Function AnagraficaClienti() As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        OpName = User.Identity.Name
        Dim listClienti As New List(Of ClienteAnagraficaViewModel)
        'Ricerca Articoli per condominio
        Try
            myConn = New SqlConnection(ConnectionString)
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "
            SELECT DISTINCT AL1.DWCCLI,
                AL1.DWCRSC,
                AL1.DWCCNA+' - '+AL1.DWCDNA,
                AL1.DWCCRE+' - '+AL1.DWCDRE,
                AL1.DWCCPR+' - '+AL1.DWCDPR,
                AL1.DWCDDC,
                AL7.DWVCA1+' - '+AL7.DWVDA1,
                AL1.DWCCR2+' - '+AL1.DWCDR2,
                ALN.CLFEMA FROM DWAlnus.dbo.DWDCLI00 AL1,
                DWAlnus.dbo.DWDART00 AL2,
                DWAlnus.dbo.DWDTPI00 AL3,
                DWAlnus.dbo.DWDDAT00 AL4,
                DWAlnus.dbo.DWDJCO00 AL5,
                DWAlnus.dbo.DWDCLI00 AL6,
                DWAlnus.dbo.DWDFDV00 AL7,
                DWAlnus.dbo.DWFOC100 AL8,
                ALNEUMA.dbo.CLFIND ALN
                WHERE (AL1.DWCSOC=AL8.DWOSOC AND AL6.DWCSOC=AL8.DWOSOC AND AL3.DWTSOC=AL8.DWOSOC AND AL2.DWASOC=AL8.DWOSOC AND AL5.DWJSOC=AL8.DWOSOC AND AL7.DWVSOC=AL8.DWOSOC AND AL8.DWOAK0=AL2.DWAPK0 AND AL5.DWJPK0=AL8.DWOJK0 AND AL8.DWOVK0=AL7.DWVPK0 AND AL3.DWTPK0=AL8.DWOTK0 AND AL8.DWOCK1=AL6.DWCPK0 AND AL8.DWOCK0=AL1.DWCPK0 AND AL8.DWODTDREV=AL4.DWDDATREV)
                --AND (((AL4.DWDDATREV BETWEEN 20220101 AND 20220217 OR AL4.DWDDATREV BETWEEN 20210101 AND 20211231) 
                AND (NOT AL8.DWOSTA='A') AND AL3.DWTARC='2'--)
                AND AL1.DWCCLI = ALN.CLFCOD--)"


            myConn.Open()
        Catch ex As Exception

        End Try
        'Parse dei dati da SQL
        Try
            myReader = myCmd.ExecuteReader
            Do While myReader.Read()
                If IsNothing(listClienti.Where(Function(x) x.ragSoc = myReader.GetString(1)).FirstOrDefault) Then
                    Dim Cli As New ClienteAnagraficaViewModel With {
                                       .CodCli = myReader.GetString(0),
                                       .ragSoc = myReader.GetString(1),
                                       .Nazione = myReader.GetString(2),
                                       .Regione = myReader.GetString(3),
                                       .Provincia = myReader.GetString(4),
                                       .Tipo = myReader.GetString(7),
                                       .Mail = myReader.GetString(8),
                                       .Euroma = "",
                                       .MPA = "",
                                       .Unistand = "",
                                       .CMT = "",
                                       .Drillmatic = "",
                                       .ISA = ""
                                  }



                    listClienti.Add(Cli)
                End If


                'results = results & myReader.GetString(0) & vbTab &
                'myReader.GetString(1) & vbLf
            Loop
            myConn.Close()

        Catch ex As Exception

        End Try
        For Each c In listClienti
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "
                    select D.CLFCOD, D.DVSCOD, D.CLFCA1, PROSCD, C.CLFEMA FROM CLFDIV00 as D,PROSOC00 as P, CLFIND as C where D.CLFCOD = '" + c.CodCli.ToString + "' AND D.CLFCA1 = P.PROAGE AND D.CLFCOD = C.CLFCOD"
                myConn.Open()
            Catch ex As Exception

            End Try
            'Parse dei dati da SQL
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Select Case myReader.GetString(1)
                        Case "01"
                            c.Drillmatic = myReader.GetString(2).ToString + " - " + myReader.GetString(3).ToString
                        Case "02"
                            c.CMT = myReader.GetString(2).ToString + " - " + myReader.GetString(3).ToString
                        Case "03"
                            c.ISA = myReader.GetString(2).ToString + " - " + myReader.GetString(3).ToString
                        Case "04"
                            c.Unistand = myReader.GetString(2).ToString + " - " + myReader.GetString(3).ToString
                        Case "05"
                            c.MPA = myReader.GetString(2).ToString + " - " + myReader.GetString(3).ToString
                        Case "06"
                            c.Euroma = myReader.GetString(2).ToString + " - " + myReader.GetString(3).ToString
                    End Select
                    c.Mail = myReader.GetString(4).ToString

                Loop
                myConn.Close()
            Catch ex As Exception

            End Try
        Next

        'Apertura file
        Dim fs As New FileStream(Server.MapPath("\Content\Template\Anagrafica.xlsx"), FileMode.Open, FileAccess.Read)
        Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
        Dim ws As XSSFSheet = workbook.GetSheet("Magazzino")
        'Start Pop
        Dim i As Integer = 1
        Dim baserow As IRow = ws.GetRow(0)
        'Dim baserow As IRow = ws.GetRow(2)
        Dim ms As New MemoryStream
        Dim ms1 As New MemoryStream
        'Dim base_value = listArticoli.First.Posizione
        'Riga Intestazione
        Try
            Try
                For Each l In listClienti

                    Dim r As IRow = ws.CreateRow(i)
                    For j = 0 To 10
                        r.CreateCell(j).CellStyle = baserow.GetCell(j).CellStyle
                    Next

                    Try
                        If Not IsNothing(l.ragSoc) Then
                            r.GetCell(0).SetCellValue(l.ragSoc)
                        Else
                            r.GetCell(0).SetCellValue(" ")
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(1).SetCellValue(l.Nazione)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(2).SetCellValue(l.Regione)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(3).SetCellValue(l.Provincia)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(4).SetCellValue(l.Drillmatic)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(5).SetCellValue(l.CMT)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(6).SetCellValue(l.ISA)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(7).SetCellValue(l.Unistand)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(8).SetCellValue(l.MPA)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(9).SetCellValue(l.Euroma)
                    Catch ex As Exception

                    End Try
                    Try
                        r.GetCell(10).SetCellValue(l.Mail)
                    Catch ex As Exception

                    End Try
                    i = i + 1
                Next
            Catch ex As Exception

            End Try
            'Intestazione

            'Dati rilevati

            workbook.Write(ms)
            Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Lista_Clienti.xlsx")

        Catch ex As Exception

        End Try


    End Function

    Function DownloadDiretto(ByVal id As String) As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate = DateTime.Now
        Try
            Dim Code = id.Replace("_-_", ".")
            Dim dis = db.Disegni_MPA.Where(Function(x) x.Code_Disegno.Contains(Code)).First
            DownloadMPA(dis.Id)
        Catch ex As Exception
            db.Log.Add(New Log With {
                                             .Livello = TipoLogLivello.Errors,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Errore: " + ex.Message,
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                               })
            db.SaveChanges()
        End Try
    End Function
    Function DettagliMPA(ByVal id As Integer?) As JsonResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpName = User.Identity.Name
            OpID = User.Identity.GetUserId()
        Catch ex As Exception

        End Try

        If IsNothing(id) Then
            Return Json(New With {.ok = False, .message = "Errore: Dettagli Disegno -> " & id & ". Impossibile recuperare i dati."})
        End If
        Dim Disegno_MPA As Disegni_MPA = db.Disegni_MPA.Find(id)
        If IsNothing(Disegno_MPA) Then
            Return Json(New With {.ok = False, .message = "Errore: Dettagli Disegno -> " & id & ". Impossibile recuperare i dati."})
        End If
        Dim path = ""
        If Not IsNothing(Disegno_MPA.Triple_Code) Then
            If Disegno_MPA.Triple_Code.ToString.Contains("E:\") Then
                path = Disegno_MPA.Triple_Code.ToString
            End If
        End If

        If Not IsNothing(Disegno_MPA.Cliente) Then
            If Disegno_MPA.Cliente.ToString.Contains("E:\") Then
                path = Disegno_MPA.Cliente.ToString
            End If
        End If

        If Not IsNothing(Disegno_MPA.Descrizione) Then
            If Disegno_MPA.Descrizione.ToString.Contains("E:\") Then
                path = Disegno_MPA.Descrizione.ToString
            End If
        End If

        If Not IsNothing(Disegno_MPA.User) Then
            If Disegno_MPA.User.ToString.Contains("E:\") Then
                path = Disegno_MPA.User.ToString
            End If
        End If
        If Not IsNothing(Disegno_MPA.Path_File) Then
            If Disegno_MPA.Path_File.ToString.Contains("E:\") Then
                path = Disegno_MPA.Path_File.ToString
            End If
        End If
        If Not IsNothing(Disegno_MPA.Path_File) Then
            If Disegno_MPA.Path_File.Contains("D:\Azienda\Tecnico\Progetti") Then
                path = Disegno_MPA.Path_File.ToString
            End If
        End If

        If path = "" Then
            Dim Disegno_MPAegno = db.Disegni_MPA_server.Where(Function(x) x.Code_Disegno.Contains(Disegno_MPA.Code_Disegno) And x.Path_File <> "").FirstOrDefault
            If Not IsNothing(Disegno_MPAegno) Then
                path = Disegno_MPAegno.Path_File + "\" + Disegno_MPAegno.Code_Disegno
            End If
        End If
        If path <> "" Then
            ViewBag.exist = True
        Else
            ViewBag.exist = False
        End If
        Dim DisegnoVM As New DisegnoMPAViewModel With {
            .Id = Disegno_MPA.Id,
            .Cod_art = Disegno_MPA.Code_Disegno,
            .Campo1 = IIf(Not IsNothing(Disegno_MPA.Descrizione), Disegno_MPA.Descrizione, ""),
            .Campo2 = IIf(Not IsNothing(Disegno_MPA.Triple_Code), Disegno_MPA.Triple_Code, ""),
            .Campo3 = IIf(Not IsNothing(Disegno_MPA.User), Disegno_MPA.User, ""),
            .Campo4 = IIf(Not IsNothing(Disegno_MPA.Cliente), Disegno_MPA.Cliente, ""),
            .Desc_Alnus = IIf(Not IsNothing(Disegno_MPA.Desc_Alnus), Disegno_MPA.Desc_Alnus, "")
        }
        db.Audit.Add(New Audit With {
                                         .Livello = TipoAuditLivello.Info,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Dettagli disegno MPA",
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                           })
        db.SaveChanges()
        Return Json(New With {.ok = True, .message = PartialToString("DettagliMPA", DisegnoVM)})
    End Function
    Function RicercaDisegni(ByVal id As String) As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Dim dict As New Dictionary(Of String, String)
        Try
            OpName = User.Identity.Name
            OpID = User.Identity.GetUserId()
            db.Audit.Add(New Audit With {
                                        .Livello = TipoAuditLivello.Info,
                                        .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                        .Messaggio = "Ricerca disegno main",
                                        .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.searchString = id}),
                                       .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                          })
            db.SaveChanges()
        Catch ex As Exception

        End Try
        Try
            Dim tmpPATH = id.Split("+")
            Dim val = "*" + tmpPATH(1).ToString + "*.*"
            Dim us = User.Identity.GetUserName()
            Dim u = appctx.Users.Where(Function(x) x.UserName = us).First 'db.UserEmail.Where(Function(x) x.Uid = OC.OperatoreInsert).First
            Dim path_basic = u.Profile.Percorso_Ricerca + "\" + tmpPATH(0)
            Dim nomeArt = tmpPATH(1).ToString.Replace("_-_", ".").ToUpper
            Dim l As New List(Of DisegnoServerViewModel)
            'If tmpPATH(0) = "06 - MPA" Then
            '    Dim listMPA As New List(Of Disegni_MPA)
            ViewBag.Art = nomeArt
            Return View(l)
        Catch ex As Exception
            db.Log.Add(New Log With {
                                          .Livello = TipoLogLivello.Errors,
                                          .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                          .Messaggio = "Errore: " + ex.Message,
                                          .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = id}),
                                         .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                            })
            db.SaveChanges()
            Return View(New List(Of DisegnoServerViewModel))
        End Try

    End Function
    Function ListDownloadMPA(ByVal id As Integer) As PartialViewResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpName = User.Identity.Name
            OpID = User.Identity.GetUserId()
        Catch ex As Exception

        End Try

        Dim l As New List(Of Disegno_Server_ViewModel)
        Dim acc = db.Disegni_MPA.Where(Function(x) x.Id = id).First
        Dim path = ""
        If Not IsNothing(acc.Triple_Code) Then
            If acc.Triple_Code.ToString.Contains("E:\") Then
                path = acc.Triple_Code.ToString
            End If
        End If

        If Not IsNothing(acc.Cliente) Then
            If acc.Cliente.ToString.Contains("E:\") Then
                path = acc.Cliente.ToString
            End If
        End If

        If Not IsNothing(acc.Descrizione) Then
            If acc.Descrizione.ToString.Contains("E:\") Then
                path = acc.Descrizione.ToString
            End If
        End If

        If Not IsNothing(acc.User) Then
            If acc.User.ToString.Contains("E:\") Then
                path = acc.User.ToString
            End If
        End If
        If Not IsNothing(acc.Path_File) Then
            If acc.Path_File.ToString.Contains("E:\") Then
                path = acc.Path_File.ToString
            End If
        End If
        If Not IsNothing(acc.Path_File) Then
            If acc.Path_File.Contains("D:\Azienda\Tecnico\Progetti") Then
                path = acc.Path_File.ToString
            End If
        End If

        If path = "" Then
            Dim dis = db.Disegni_MPA_server.Where(Function(x) x.Code_Disegno.Contains(acc.Code_Disegno) And x.Path_File <> "").FirstOrDefault
            If Not IsNothing(dis) Then
                path = dis.Path_File
            End If
        End If
        If path <> "" Then
            l.Add(New Disegno_Server_ViewModel With {
            .Code_Disegno = acc.Code_Disegno,
            .Path_File = path,
            .Id = id
        })
        End If
        db.Audit.Add(New Audit With {
                                         .Livello = TipoAuditLivello.Info,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Ricerca lista download MPA",
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                           })
        db.SaveChanges()
        Return PartialView(l)
    End Function
    Function DownloadMPA(ByVal id As Integer) As FileResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now

        Try
            OpName = User.Identity.Name
            OpID = User.Identity.GetUserId()

            Dim Acc = db.Disegni_MPA.Where(Function(x) x.Id = id).First
            Dim path = ""
            If Not IsNothing(Acc.Triple_Code) Then
                If Acc.Triple_Code.ToString.Contains("E:\") Then
                    path = Acc.Triple_Code.ToString
                End If
            End If

            If Not IsNothing(Acc.Cliente) Then
                If Acc.Cliente.ToString.Contains("E:\") Then
                    path = Acc.Cliente.ToString
                End If
            End If

            If Not IsNothing(Acc.Descrizione) Then
                If Acc.Descrizione.ToString.Contains("E:\") Then
                    path = Acc.Descrizione.ToString
                End If
            End If

            If Not IsNothing(Acc.User) Then
                If Acc.User.ToString.Contains("E:\") Then
                    path = Acc.User.ToString
                End If
            End If
            If Not IsNothing(Acc.Path_File) Then
                If Acc.Path_File.ToString.Contains("E:\") Then
                    path = Acc.Path_File.ToString
                End If
            End If
            If Not IsNothing(Acc.Path_File) Then
                If Acc.Path_File.Contains("D:\Azienda\Tecnico\Progetti") Then
                    path = Acc.Path_File + "\" + Acc.Code_Disegno
                End If
            End If

            If path = "" Then
                Dim disegno = db.Disegni_MPA_server.Where(Function(x) x.Code_Disegno.Contains(Acc.Code_Disegno) And x.Path_File <> "").FirstOrDefault
                If Not IsNothing(disegno) Then
                    path = disegno.Path_File + "\" + disegno.Code_Disegno
                End If
            End If
            If path <> "" Then
                path = path.Replace("E:\dati", "\\srvd01")
                path = path.Replace("E:\Dati", "\\srvd01")

            End If
            Try
                'Dim search_path = HttpUtility.UrlDecode(id.ToString.Replace("+", "%")) '.Replace("-", ".")
                If path.Contains(".mi") Then
                    If path.Split(".mi").Length > 1 Then
                        path = path.Split(".mi")(0) + ".mi"
                    End If
                ElseIf path.Substring(path.Length - 3, 3).Contains(".") Then
                    path = path + ".mi"
                End If
                Dim file = New FileInfo(path)

                If (file.Exists) Then
                    db.Audit.Add(New Audit With {
                                         .Livello = TipoAuditLivello.Info,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Download disegno MPA",
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id, .disegno = Acc.Code_Disegno}),
                                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                           })
                    db.SaveChanges()
                    Response.ClearHeaders()
                    Response.ClearContent()
                    Dim name = ""
                    If file.Name.Contains(".mi") Then
                        If file.Name.Split(".mi").Length > 1 Then
                            name = file.Name.Split(".mi")(0) + ".mi"
                        End If
                    ElseIf Not file.Name.Substring(file.Name.Length - 3, 3).Contains(".") Then
                        name = file.Name + ".mi"
                    End If
       
                    Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", name))
                    Response.AddHeader("Content-Length", file.Length.ToString)
                    Response.ContentType = "application/x-download"
                    Response.TransmitFile(file.FullName)
                    Response.End()
                Else

                End If
            Catch ex As Exception
                db.Log.Add(New Log With {
                                                 .Livello = TipoLogLivello.Errors,
                                                 .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                 .Messaggio = "Errore: " + ex.Message,
                                                 .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = id}),
                                                .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                                   })
                db.SaveChanges()
            End Try
        Catch ex As Exception
            db.Log.Add(New Log With {
                                                .Livello = TipoLogLivello.Errors,
                                                .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                                .Messaggio = "Errore: " + ex.Message,
                                                .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = id}),
                                               .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                                  })
                db.SaveChanges()
            End Try


    End Function
    Function HiddenGetAllQuantitas() As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        OpName = User.Identity.Name


        'Apertura file
        Dim fs As New FileStream(Server.MapPath("\Content\Template\per_mattia.xlsx"), FileMode.Open, FileAccess.Read)
        Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
        Dim ws As XSSFSheet = workbook.GetSheetAt(0)
        'Start Pop
        Dim baserow As IRow = ws.GetRow(0)
        'Dim baserow As IRow = ws.GetRow(2)
        Dim ms As New MemoryStream
        Dim ms1 As New MemoryStream
        Dim qt = ""
        Dim qtMat = ""
        'Riga Intestazione
        Try
            Try
                With ws
                    For i = 1 To .LastRowNum
                        Try
                            Dim mat = GetCellValue(.GetRow(i), 11)
                            Dim codArt = GetCellValue(.GetRow(i), 1)
                            Try
                                myConn = New SqlConnection(ConnectionString)
                                myCmd = myConn.CreateCommand
                                myCmd.CommandText = "
	SELECT ARTDMG.ARTCOD as Articolo,ARTDMG.MATRAC as Matricola,ARTDMA.ARTGIA, ARTDMG.ARTGIA
	FROM ARTDMG (NOLOCK),
	ARTDMA (NOLOCK),
	ARTANA (NOLOCK)
	WHERE ARTDMG.ARTCOD = '" & codArt.ToString & "' 
	AND MATRAC = '" & mat.ToString & "'
	AND ARTDMG.ARTCOD = ARTDMA.ARTCOD
	AND ARTDMG.DMGVER = ARTVER
	AND ARTDMG.MAGCOD = ARTDMA.MAGCOD
	AND ARTDMG.ARTCOD = ARTCO1"
                                myConn.Open()
                            Catch ex As Exception

                            End Try
                        Catch ex As Exception

                        End Try
                        'Parse dei dati da SQL
                        Try
                            myReader = myCmd.ExecuteReader
                            Do While myReader.Read()
                                qt = myReader.GetDecimal(2).ToString
                                qtMat = myReader.GetDecimal(3).ToString

                            Loop
                            myConn.Close()

                        Catch ex As Exception

                        End Try
                        Dim r As IRow = ws.GetRow(i)
                        r.GetCell(4).SetCellValue(qt)
                        r.GetCell(5).SetCellValue(qtMat)
                    Next
                End With

            Catch ex As Exception

            End Try
            'Intestazione

            'Dati rilevati

            workbook.Write(ms)

            Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - ARTICOLI CORETTI.xlsx")
        Catch ex As Exception

        End Try


    End Function
    Function HiddenGetAllRimanenze() As ActionResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        OpName = User.Identity.Name
        'Apertura file
        Dim fs As New FileStream(Server.MapPath("\Content\Template\INVENTARIO_UFFICIALE.xlsx"), FileMode.Open, FileAccess.Read)
        Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
        Dim ws As XSSFSheet = workbook.GetSheetAt(0)
        'Start Pop
        Dim baserow As IRow = ws.GetRow(0)
        'Dim baserow As IRow = ws.GetRow(2)
        Dim ms As New MemoryStream
        Dim ms1 As New MemoryStream
        Dim qt = ""
        Dim qtMat = ""
        Dim list As New Dictionary(Of String, String)
        'Riga Intestazione
        Try
            Try
                Try
                    myConn = New SqlConnection(ConnectionString)
                    myCmd = myConn.CreateCommand
                    myCmd.CommandText = "SELECT ARTDMM00.MAGCOD as Magazzino, ARTDMM00.ARTCOD as Articolo,
                                        DMMVER as Versione, ARTDMM00.QTARIM as Quantita,
                                        ARTDMA.MAZONA as Zona, ARTDMA.MACORS as Corsia,
                                        ARTDMA.MABAYY as Campata, ARTDMA.MACAMP as Posizione,
                                        DMMTRA as Tipologia, DMMRAC as Matricola, DMMCOM as Commessa,
                                        convert(varchar, GETDATE(), 112) as Data, ' ' as Cartellino,
                                        ' ' as EAN, ARTDES
                                        FROM ARTDMM00 (NOLOCK),
                                        ARTDMA (NOLOCK),
                                        ARTANA (NOLOCK)
                                        WHERE ARTDMM00.ARTCOD = ARTDMA.ARTCOD
                                        AND ARTDMM00.DMMVER = ARTVER
                                        AND ARTDMM00.MAGCOD = ARTDMA.MAGCOD
                                        AND ARTDMM00.ARTCOD = ARTCO1
                                        AND DMCMANREV = '202112'
                                        -- AND ARTDMG.ARTGIA <> 0
                                        -- AND ARTDMA.MABAYY = ' '
                                         --AND ARTANA.TPRCOD = 'MIN'
                                        --AND ARTDMA.MABAYY = '05'
                                        union
                                        SELECT ARTDMA.MAGCOD as Magazzino, ARTPIA.ARTCOD as Articolo,
                                        isnull(DIBVER, ' ') as Versione, 0 as Quantita, MAZONA as Zona,
                                        MACORS as Corsia, MABAYY as Campata, MACAMP as Posizione,
                                        case when ARTGAR = 'S' then 'M' when ARTGLO = 'S' then 'L' else ' ' end
                                        as Tipologia, ' ' as Matricola, ' ' as Commessa,
                                        convert(varchar, GETDATE(), 112) as Data, ' ' as Cartellino,
                                        ' ' as EAN, ARTDES
                                        FROM ARTPIA (NOLOCK)
                                        INNER JOIN ARTMAG (NOLOCK) ON ARTMAG.ARTCOD = ARTPIA.ARTCOD
                                        INNER JOIN ARTDMA (NOLOCK) ON ARTDMA.ARTCOD = ARTPIA.ARTCOD
                                        INNER JOIN ARTANA (NOLOCK) ON ARTCO1 = ARTPIA.ARTCOD
                                        LEFT JOIN DIBTES00 (NOLOCK) ON ARTCO1 = DIBTES00.ARTCOD
                                        AND DIBVDF = 'S'
                                        AND ARTGAR = 'S'
                                        WHERE ARTCO1 NOT IN
                                        (SELECT ARTDMM00.ARTCOD FROM ARTDMM00 (NOLOCK)
                                        WHERE DMCMANREV = '202112')
                                        -- AND ARTANA.TPRCOD = 'MIN'
                                        --AND ARTDMA.MABAYY = '40'
                                        order by ARTDMM00.ARTCOD,ARTDMM00.MAGCOD, ARTDMA.MAZONA, ARTDMA.MACORS,
                                        ARTDMA.MABAYY, ARTDMA.MACAMP, ARTDMM00.DMMVER
                                        "
                    myConn.Open()
                Catch ex As Exception

                End Try
                Try
                    Try
                        myReader = myCmd.ExecuteReader
                        Do While myReader.Read()
                            Try
                                list.Add(myReader.GetString(1).ToString.Replace(" ", "") + "-" + myReader.GetString(9).ToString.Replace(" ", ""), myReader.GetDecimal(3).ToString)
                            Catch ex As Exception

                            End Try
                        Loop
                        myConn.Close()

                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try
                With ws
                    For i = 1 To .LastRowNum
                        Try
                            If IsNothing(GetCellValue(.GetRow(i), 16)) Then
                                Dim mat = GetCellValue(.GetRow(i), 11)
                                Dim codArt = GetCellValue(.GetRow(i), 1).ToString.Replace(" ", "")
                                Dim codMat = GetCellValue(.GetRow(i), 10).ToString.Replace(" ", "")
                                Dim tmpArt As New ArtViewModel With {
                                    .Articolo = codArt}
                                Dim r As IRow = ws.GetRow(i)
                                Dim res As String = list.Where(Function(x) x.Key = codArt + "-" + codMat).FirstOrDefault.Value
                                If Not IsNothing(res) Then
                                    r.GetCell(16).SetCellValue(res)
                                Else
                                    r.GetCell(16).SetCellValue("0,000")
                                End If
                            End If

                        Catch ex As Exception

                        End Try
                        'Parse dei dati da SQL


                    Next
                End With

            Catch ex As Exception

            End Try
            'Intestazione

            'Dati rilevati

            workbook.Write(ms)

            Return File(ms.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Now.Year & "_" & Now.Month & "_" & Now.Day & " - ARTICOLI CORETTI.xlsx")
        Catch ex As Exception

        End Try


    End Function
    Private Function GetCellValue(row As IRow, col As Integer, Optional OpID As String = vbNullString, Optional OpName As String = vbNullString) As Object
        Dim result As String = vbNullString

        Try

            If Not IsNothing(row) Then
                Dim cell As ICell = row.GetCell(col)
                If Not IsNothing(cell) Then
                    Select Case cell.CellType
                        Case CellType.Numeric
                            If DateUtil.IsCellDateFormatted(cell) Then
                                Return cell.DateCellValue
                            Else
                                Return cell.NumericCellValue
                            End If
                        Case CellType.String
                            Return cell.StringCellValue.Trim
                        Case CellType.Boolean
                            Return cell.BooleanCellValue
                        Case CellType.Formula
                            Return cell.NumericCellValue
                        Case CellType.Blank, CellType.Error, CellType.Unknown
                            Return vbNullString
                    End Select
                End If
            End If
        Catch ex As Exception

        End Try

        Return result
    End Function
    Function SearchForSimilarFiles(ByVal RootFolder As String, ByVal FileFilter() As String) As List(Of String)
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        OpName = User.Identity.Name
        Try
            Dim ReturnedData As New List(Of String)
            Dim FolderStack As New Stack(Of String)
            FolderStack.Push(RootFolder)
            Do While FolderStack.Count > 0
                Dim ThisFolder As String = FolderStack.Pop
                Try
                    For Each SubFolder In GetDirectories(ThisFolder)
                        FolderStack.Push(SubFolder)
                    Next
                    For Each FileExt In FileFilter
                        ReturnedData.AddRange(GetFiles(ThisFolder, FileExt))
                    Next
                Catch ex As Exception
                End Try
            Loop
            Return ReturnedData
        Catch ex As Exception
            db.Log.Add(New Log With {
                                          .Livello = TipoLogLivello.Errors,
                                          .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                          .Messaggio = "Errore: " + ex.Message,
                                          .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.Disegno = "SearchForSimilarFiles"}),
                                         .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                            })
            db.SaveChanges()

        End Try

    End Function
    <HttpPost()>
    <ValidateInput(False)>
    Function ServerProcessing(PostedData As DataTableAjaxPostModel) As JsonResult
        Dim OpID As String = vbNullString
        Dim OpName As String = vbNullString
        Dim CurrentDate As DateTime = Now
        Try
            OpID = User.Identity.GetUserId()
            OpName = User.Identity.GetUserName()
            db.Audit.Add(New Audit With {
                                         .Livello = TipoAuditLivello.Info,
                                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                         .Messaggio = "Ricerca disegno con parametri",
                                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.mainSearchParam = PostedData.art, .secondarySearchParam = PostedData.search.value}),
                                        .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                           })
            db.SaveChanges()
            Dim result As New List(Of Object)
            Dim data = db.Disegni_MPA.Where(Function(x) x.Code_Disegno.Contains(PostedData.art) Or x.Descrizione.Contains(PostedData.art.ToUpper) Or x.Cliente.Contains(PostedData.art.ToUpper) Or x.Path_File.Contains(PostedData.art.ToUpper) Or x.Cliente.Contains(PostedData.art.ToUpper) Or x.Triple_Code.Contains(PostedData.art.ToUpper) Or x.Desc_Alnus.Contains(PostedData.art.ToString.ToUpper))
            'ricerca
            Try
                If Not IsNothing(PostedData.search.value) Then
                    If Not PostedData.search.value.Contains(" ") Then 'singola parola
                        Dim search As String = PostedData.search.value
                        Dim w As Expressions.Expression(Of Func(Of Disegni_MPA, Boolean)) = MakeWhereExpression(search)
                        w.Compile()
                        data = data.Where(w)
                    Else 'multiple
                        For Each term As String In PostedData.search.value.Split(" ")
                            Dim wpartial As Expressions.Expression(Of Func(Of Disegni_MPA, Boolean)) = MakeWhereExpression(term)
                            wpartial.Compile()
                            data = data.Where(wpartial)
                        Next
                    End If
                End If
            Catch ex As SystemException
                db.Log.Add(New Log With {
                      .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                      .Livello = TipoLogLivello.Errors,
                      .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                      .Messaggio = "Errore Ricerca -> " & ex.Message,
                      .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                      })
                db.SaveChanges()
            End Try

            'ordinamento
            Try
                If PostedData.order.Count = 0 Then
                    Dim o As Expressions.Expression(Of Func(Of Disegni_MPA, String))
                    o = MakeOrderExpression(Nothing) 'default
                    o.Compile()
                    data = data.OrderBy(o)
                    'data = data.OrderBy(CreateExpression(Of Amministratore)("Studio"))
                    'data = OrderByDynamic(data, "Studio", False)
                ElseIf PostedData.order.Count = 1 Then 'singolo
                    Dim o As Expressions.Expression(Of Func(Of Disegni_MPA, String)) = MakeOrderExpression(PostedData.order(0).column)
                    o.Compile()
                    'If IsNothing(PostedData.columns(PostedData.order(0).column).data) Then
                    '    data = OrderByDynamic(data, "Studio", False)
                    'Else
                    '    data = OrderByDynamic(data, PostedData.columns(PostedData.order(0).column).data, PostedData.order(0).dir = "desc")
                    'End If

                    Select Case PostedData.order(0).dir
                        Case "asc"
                            data = data.OrderBy(o)
                        Case "desc"
                            data = data.OrderByDescending(o)
                    End Select
                ElseIf PostedData.order.Count = 2 Then 'doppio
                    Dim o As Expressions.Expression(Of Func(Of Disegni_MPA, String)) = MakeOrderExpression(PostedData.order(0).column)
                    o.Compile()

                    Dim o2 As Expressions.Expression(Of Func(Of Disegni_MPA, String)) = MakeOrderExpression(PostedData.order(1).column)
                    o2.Compile()

                    Select Case PostedData.order(0).dir
                        Case "asc"
                            Select Case PostedData.order(1).dir
                                Case "asc"
                                    data = data.OrderBy(o).ThenBy(o2)
                                Case "desc"
                                    data = data.OrderBy(o).ThenByDescending(o2)
                            End Select

                        Case "desc"
                            Select Case PostedData.order(1).dir
                                Case "asc"
                                    data = data.OrderByDescending(o).ThenBy(o2)
                                Case "desc"
                                    data = data.OrderByDescending(o).ThenByDescending(o2)
                            End Select
                    End Select
                Else 'solo i primi tre
                    Dim o As Expressions.Expression(Of Func(Of Disegni_MPA, String)) = MakeOrderExpression(PostedData.order(0).column)
                    o.Compile()

                    Dim o2 As Expressions.Expression(Of Func(Of Disegni_MPA, String)) = MakeOrderExpression(PostedData.order(1).column)
                    o2.Compile()

                    Dim o3 As Expressions.Expression(Of Func(Of Disegni_MPA, String)) = MakeOrderExpression(PostedData.order(2).column)
                    o3.Compile()

                    Select Case PostedData.order(0).dir
                        Case "asc"
                            Select Case PostedData.order(1).dir
                                Case "asc"
                                    Select Case PostedData.order(2).dir
                                        Case "asc"
                                            data = data.OrderBy(o).ThenBy(o2).ThenBy(o3)
                                        Case "desc"
                                            data = data.OrderBy(o).ThenBy(o2).ThenByDescending(o3)
                                    End Select
                                Case "desc"
                                    Select Case PostedData.order(2).dir
                                        Case "asc"
                                            data = data.OrderBy(o).ThenByDescending(o2).ThenBy(o3)
                                        Case "desc"
                                            data = data.OrderBy(o).ThenByDescending(o2).ThenByDescending(o3)
                                    End Select
                            End Select

                        Case "desc"
                            Select Case PostedData.order(1).dir
                                Case "asc"
                                    Select Case PostedData.order(2).dir
                                        Case "asc"
                                            data = data.OrderByDescending(o).ThenBy(o2).ThenBy(o3)
                                        Case "desc"
                                            data = data.OrderByDescending(o).ThenBy(o2).ThenByDescending(o3)
                                    End Select
                                Case "desc"
                                    Select Case PostedData.order(2).dir
                                        Case "asc"
                                            data = data.OrderByDescending(o).ThenByDescending(o2).ThenBy(o3)
                                        Case "desc"
                                            data = data.OrderByDescending(o).ThenByDescending(o2).ThenByDescending(o3)
                                    End Select
                            End Select
                    End Select
                End If
            Catch ex As SystemException
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Ordinamento -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                     })
                db.SaveChanges()
            End Try

            'paginazione
            Dim filtered As Integer = 0
            Try
                filtered = data.Count
                If PostedData.length > 0 Then
                    data = data.Skip(PostedData.start).Take(PostedData.length)
                End If
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
            For Each Acc As Disegni_MPA In data
                Try
                    Dim path = ""
                    If Not IsNothing(Acc.Triple_Code) Then
                        If Acc.Triple_Code.ToString.Contains("E:\") Then
                            path = Acc.Triple_Code.ToString
                        End If
                    End If

                    If Not IsNothing(Acc.Cliente) Then
                        If Acc.Cliente.ToString.Contains("E:\") Then
                            path = Acc.Cliente.ToString
                        End If
                    End If

                    If Not IsNothing(Acc.Descrizione) Then
                        If Acc.Descrizione.ToString.Contains("E:\") Then
                            path = Acc.Descrizione.ToString
                        End If
                    End If

                    If Not IsNothing(Acc.User) Then
                        If Acc.User.ToString.Contains("E:\") Then
                            path = Acc.User.ToString
                        End If
                    End If
                    If Not IsNothing(Acc.Path_File) Then
                        If Acc.Path_File.ToString.Contains("E:\") Then
                            path = Acc.Path_File.ToString
                        End If
                    End If
                    If Not IsNothing(Acc.Path_File) Then
                        If Acc.Path_File.Contains("D:\Azienda\Tecnico\Progetti") Then
                            path = Acc.Path_File.ToString
                        End If
                    End If

                    If path = "" Then
                        Dim dis = db.Disegni_MPA_server.Where(Function(x) x.Code_Disegno.Contains(Acc.Code_Disegno) And x.Path_File <> "").FirstOrDefault
                        If Not IsNothing(dis) Then
                            path = dis.Path_File
                        End If
                    End If

                    Dim finalButtons = ""
                    Dim Stato_Accettazione = ""
                    result.Add(New With {
                                .DT_RowData = New With {.value = Acc.Id},
                                .DT_RowId = "row_" & Acc.Id,
                                .Id = Acc.Id,
                                .Code_Disegno = Acc.Code_Disegno,
                                .Desc = IIf(Not IsNothing(Acc.Desc_Alnus), Acc.Desc_Alnus, "") + " - " + IIf(Not IsNothing(Acc.Descrizione), Acc.Descrizione, ""),
                                .Path = path
                           })


                Catch ex As SystemException
                    db.Log.Add(New Log With {
                             .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                             .Livello = TipoLogLivello.Errors,
                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                             .Messaggio = "Errore Creazione Lista Impianto (" & Acc.Id & ") -> " & ex.Message & " [" & ex.InnerException.Message & "]",
                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {PostedData})
                        })
                    db.SaveChanges()
                End Try
            Next

            Return Json(New With {PostedData.draw, .recordsTotal = db.Disegni_MPA.Count, .recordsFiltered = filtered, .data = result})
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
        Return Json(New With {PostedData.draw, .recordsTotal = db.Disegni_MPA.Count, .recordsFiltered = 0})
    End Function
    Private Function MakeWhereExpression(Search As String) As Expressions.Expression(Of Func(Of Disegni_MPA, Boolean))
        Return Function(x) x.Code_Disegno.Contains(Search) Or
                           x.Cliente.Contains(Search) Or
                           x.User.Contains(Search) Or
                           x.Descrizione.Contains(Search) Or
                           x.Triple_Code.Contains(Search) Or
                           x.Path_File.Contains(Search) Or
                           x.Desc_Alnus.Contains(Search)

    End Function
    Private Function MakeOrderExpression(Column As Integer) As Expressions.Expression(Of Func(Of Disegni_MPA, String))
        Select Case Column
            Case Nothing : Return Function(x) x.Code_Disegno
            Case 1 : Return Function(x) x.Desc_Alnus
            Case 2 : Return Function(x) x.Descrizione
            Case 4 : Return Function(x) x.Cliente
            Case 5 : Return Function(x) x.User
            Case 6 : Return Function(x) x.Triple_Code
            Case 7 : Return Function(x) x.Path_File
            Case Else : Return Function(x) x.Code_Disegno
        End Select
    End Function
    Private Function PartialToString(ByVal viewName As String, ByVal model As Object) As String
        ViewData.Model = model

        Using writer As IO.StringWriter = New IO.StringWriter()
            Dim vResult As ViewEngineResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName)
            Dim vContext As ViewContext = New ViewContext(Me.ControllerContext, vResult.View, ViewData, New TempDataDictionary(), writer)
            vResult.View.Render(vContext, writer)
            Return writer.ToString()
        End Using
    End Function

End Class
