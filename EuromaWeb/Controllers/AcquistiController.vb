Imports System.IO
Imports System.IO.Compression
Imports System.Web.Mvc
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Microsoft.AspNet.Identity
Imports ICSharpCode.SharpZipLib.Zip

Namespace Controllers
    Public Class AcquistiController
        Inherits Controller
        Private db As New EuromaModels
        Private appctx As New ApplicationDbContext

        Private Const ConnectionString As String = "Persist Security Info=True;Password=ALNUSAD;User ID=ALNUSAD;Initial Catalog=ALNEUMA;Data Source=192.168.100.50"
        Private myConn As SqlConnection
        Private myCmd As SqlCommand
        Private myReader As SqlDataReader
        Private results As String

        ' GET: Acquisti
        Function Lista() As ActionResult
            Return PartialView(db.LavorazioniEsterne.ToList())
        End Function
        Function LavorazioniEsterne(ByVal id As Integer) As ActionResult
            Dim lista As New List(Of LavorazioniEsterne)
            If id = 1 Then
                lista = db.LavorazioniEsterne.Where(Function(x) x.Inviato = Enum_Bolla.In_attesa).ToList()
            Else
                lista = db.LavorazioniEsterne.Where(Function(x) x.Inviato = Enum_Bolla.Inviato).ToList()
            End If
            Return View(New RichiestaOLviewModel With {
                .Esecod = "2022",
                .OL = "OL",
                .List = lista,
                .idPagina = id
            })
        End Function
        Function DownloadFileDDT(id As Integer) As FileResult
            Dim bolla = db.LavorazioniEsterne.Where(Function(x) x.Id = id).First
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Download DDT",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                               })
                db.SaveChanges()

            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Download DDT -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
            End Try

            Return File(bolla.Path_DDT, "application/octet-stream", bolla.OL + ".pdf") 'System.IO.File.ReadAllBytes(newFile)
        End Function
        Function DownloadFile(id As Integer) As FileResult
            Dim bolla = db.LavorazioniEsterne.Where(Function(x) x.Id = id).First
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Download Bolla",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                               })
                db.SaveChanges()

            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Download Bolla -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
            End Try
            Return File(bolla.Path_Doc, "application/octet-stream", bolla.OL + ".pdf") 'System.IO.File.ReadAllBytes(newFile)
        End Function
        <Authorize>
        Function DeleteLavorazione(ByVal id As Integer) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName()
                Dim lav = db.LavorazioniEsterne.Find(id)
                db.LavorazioniEsterne.Remove(lav)
                db.SaveChanges()
                db.Audit.Add(New Audit With {
                                            .Livello = TipoAuditLivello.Info,
                                            .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                            .Messaggio = "Bolla cancellata correttamente",
                                            .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id, .ol = lav.OL}),
                                           .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = DateTime.Now}
                              })
                db.SaveChanges()
                Return Json(New With {.ok = True, .message = "OL correttamente eliminata!"}, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = DateTime.Now, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Impossibile cancellare l'OL!"}, JsonRequestBehavior.AllowGet)
            End Try
        End Function
        <Authorize>
        Function LavorazioniEsterneDirect(ByVal id As Integer) As JsonResult
            Dim tmp_Lav = db.LavorazioniEsterne.Where(Function(x) x.Id = id).First.OL.Split("_")
            Dim ema_lav = db.LavorazioniEsterne.Where(Function(x) x.Id = id).First
            Dim lav As New RichiestaOLviewModel With {
                .Esecod = tmp_Lav(0),
                .OL = tmp_Lav(1),
                .Num = tmp_Lav(2),
                .email = ema_lav.Email,
                .FlagBoth = True,
                .FlagMailAuto = True
            }
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Dim fornitore As New OlviewModel
            Dim listOfArticoli As New List(Of ArticoliOL)
            'Ricerca se già stata inviata
            If db.LavorazioniEsterne.Where(Function(x) x.Id = id).First.Inviato = Enum_Bolla.Inviato Then
                Return Json(New With {.ok = False, .message = "OL già inviata!"}, JsonRequestBehavior.AllowGet)
            End If
            'Creazione modello
            'Ricerca dati
            Try
                Try
                    OpID = User.Identity.GetUserId()
                    OpName = User.Identity.GetUserName()
                    db.Audit.Add(New Audit With {
                                             .Livello = TipoAuditLivello.Info,
                                             .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                             .Messaggio = "Invio Diretto Bolla e DDT",
                                             .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id}),
                                            .UltimaModifica = New TipoUltimaModifica With {.OperatoreID = OpID, .Operatore = OpName, .Data = CurrentDate}
                               })
                    db.SaveChanges()

                Catch ex As Exception
                    db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                    db.SaveChanges()
                End Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = " select ORDFOR.ORFCLF, CLIDATA.CLFNMG + CLIIND.CLFIN1 + CLIIND.CLFLC1 + CLIIND.CLFCAP + CLIIND.CLFPLC + ' ' +   CLIIND.NAZCOD, CLIDATA.CLFCPI, CLIDATA.CLFNCC, CLIDATA.CLFABA, CLIDATA.CLFCAA, ABI.ABIDES  + ' ' + CAB.CABDES, RES.RESDES, PAG.PAGDES,CLIDATA.CLFCFS,ORDFOR.ORFTIM from ORFTES00 AS ORDFOR, CLFANA AS CLIDATA, CLFIND AS CLIIND, TABCAB00 AS CAB, TABABI00 AS ABI, TABRES00 AS RES, PAGTAB00 AS PAG where CLIIND.CLFTIP = 'F' AND CLIDATA.CLFTIP = 'F' AND ORDFOR.ESECOD = '" + lav.Esecod + "' and ORDFOR.ORFSEZ = '" + lav.OL + "' AND ORDFOR.ORFNUM = '" + lav.Num + "' AND CLIIND.CLFCOD = ORDFOR.ORFCLF AND CLIDATA.CLFCO1 = ORDFOR.ORFCLF AND CLIDATA.CLFABA = ABI.ABICO1 AND CLIDATA.CLFCAA = CAB.CABCO1 AND CLIDATA.CLFABA = CAB.ABICO1 AND ORDFOR.RESCOD = RES.RESCO1 AND ORDFOR.PAGCOD = PAG.PAGCO1"
                myConn.Open()
            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla (Ricerca dati da query) -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Dim fornitoreTMP As New OlviewModel With {
                        .Cod_Fornitore = myReader.GetString(0),
                        .Cliente = myReader.GetString(1),
                        .PIVA = myReader.GetString(2),
                        .CC = myReader.GetString(3),
                        .Abi = myReader.GetString(4),
                        .Cab = myReader.GetString(5),
                        .Banca_Dappoggio = myReader.GetString(6),
                        .Consegna = myReader.GetString(7),
                        .Pagamento = myReader.GetString(8),
                        .Data = DateTime.Now.ToString.Split(" ")(0),
                        .OL = lav.OL + "/" + lav.Num,
                        .Cod_Fiscale = myReader.GetString(9),
                        .Totale = myReader.GetDecimal(10).ToString,
                        .listOfArticoli = New List(Of ArticoliOL)
                    }
                    fornitore = fornitoreTMP
                Loop
                myConn.Close()
                If fornitore.Cod_Fornitore = "" And fornitore.Cliente = "" Then
                    Try
                        OpID = User.Identity.GetUserId()
                        OpName = User.Identity.GetUserName
                        myConn = New SqlConnection(ConnectionString)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "select ORDFOR.ORFCLF, CLIDATA.CLFNMG + CLIIND.CLFIN1 + CLIIND.CLFLC1 + CLIIND.CLFCAP + CLIIND.CLFPLC + ' ' +   CLIIND.NAZCOD, CLIDATA.CLFCPI, BAN.BAABI, BAN.BACAB, ABI.ABIDES  + ' ' + CAB.CABDES, PAG.PAGDES, CLIDATA.CLFCFS,ORDFOR.ORFTIM from ORFTES00 AS ORDFOR, PAGTAB00 AS PAG, CLFANA AS CLIDATA, CLFIND AS CLIIND, CGTBAN00 AS BAN, TABCAB00 AS CAB, TABABI00 AS ABI where CLIIND.CLFTIP = 'F' AND CLIDATA.CLFTIP = 'F' AND ORDFOR.ESECOD = '" + lav.Esecod + "' and ORDFOR.ORFSEZ = '" + lav.OL + "' AND ORDFOR.ORFNUM = '" + lav.Num + "' AND CLIIND.CLFCOD = ORDFOR.ORFCLF AND CLIDATA.CLFCO1 = ORDFOR.ORFCLF AND ORDFOR.PAGCOD = PAG.PAGCO1 AND BAN.BABAN = CLIDATA.CLFABP AND BAN.BAABI = ABI.ABICO1 AND BAN.BACAB = CAB.CABCO1 AND BAN.BAABI = CAB.ABICO1"
                        myConn.Open()
                    Catch ex As Exception
                        db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla (Ricerca dati w/o cod_Fornitore e Cliente) -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                        db.SaveChanges()
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader

                        Do While myReader.Read()
                            Dim fornitoreTMP As New OlviewModel With {
                                .Cod_Fornitore = myReader.GetString(0),
                                .Cliente = myReader.GetString(1),
                                .PIVA = myReader.GetString(2),
                                .CC = "",
                                .Abi = myReader.GetString(3),
                                .Cab = myReader.GetString(4),
                                .Banca_Dappoggio = myReader.GetString(5),
                                .Consegna = "",
                                .Pagamento = myReader.GetString(6),
                                .Data = DateTime.Now.ToString.Split(" ")(0),
                                .OL = lav.OL + "/" + lav.Num,
                                .Cod_Fiscale = myReader.GetString(7),
                                .Totale = myReader.GetDecimal(8).ToString,
                                .listOfArticoli = New List(Of ArticoliOL)
                            }
                            fornitore = fornitoreTMP
                        Loop
                        myConn.Close()
                    Catch ex As Exception
                        db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla (Creazione Bolla)-> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                        db.SaveChanges()
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
                    End Try
                End If
            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            'Ricerca articoli
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "select ARTCOD,ORFCONREV,ORFDAN, ORFQTY, TUMCOD,ORFPVA from ORFDET00 where ESECOD = '" + lav.Esecod + "' and ORFSEZ = '" + lav.OL + "' AND ORFNUM = '" + lav.Num + "'"
                myConn.Open()
            Catch ex As Exception
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Try
                myReader = myCmd.ExecuteReader
                Do While myReader.Read()
                    Dim consegna = myReader.GetDecimal(1).ToString
                    Dim art As New ArticoliOL With {
                        .cod_articolo = myReader.GetString(0),
                        .Consegna = consegna.ToString.Insert(6, "/").Insert(4, "/"),
                        .desc_articolo = myReader.GetString(2),
                        .importo = IIf(IsNothing(myReader.GetDecimal(5)), "Da Concordare", myReader.GetDecimal(5).ToString),
                        .qta = myReader.GetDecimal(3).ToString,
                        .UM = myReader.GetString(4)
                    }

                    listOfArticoli.Add(art)

                Loop
                myConn.Close()
            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla (Ricerca lavorazione)-> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Try
                Dim artListString = "AND ( "
                Dim r = 0
                For Each a In listOfArticoli
                    If r = 1 Then
                        artListString = artListString + " OR PREB.ARTCOD = '" + a.cod_articolo + "'"
                    Else
                        artListString = artListString + " PREB.ARTCOD = '" + a.cod_articolo + "'"
                        r = 1
                    End If
                Next
                artListString = artListString + ")"
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "SELECT DISTINCT PREB.BAMNRR, BAMTES.BOLNRR, BAMTES.BAMCTR, BAMTES.BAMUTE FROM BAMDET00 AS PREB, ORFDET00 AS ORDIN, BAMTES00 as BAMTES WHERE PREB.BAMCCD = ORDIN.COMCOD AND BAMTES.BAMNRR = PREB.BAMNRR AND PREB.ORCCLI = ORDIN.ORFCLF AND ORDIN.ORFSEZ = 'OL' AND ORDIN.ESECOD = '2023' AND PREB.ESECOD = '2023' AND BAMCTR = 'Trasferimento c/l fase (No mov.mag)' AND BAMTES.ESECOD = '2023' " + artListString
                myConn.Open()
            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla (Ricerca Prebolla)-> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    fornitore.Cod_Bolla = "PL / " + myReader.GetDecimal(0).ToString
                    fornitore.Cod_Prebolla = "BO / " + myReader.GetDecimal(1).ToString
                    fornitore.Documento_Type = myReader.GetString(2)
                    fornitore.Operatore = myReader.GetString(3)
                Loop
                myConn.Close()
            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla (Costruzione codici BO PL)-> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Dim path_File = ""
            Dim path_File_DDT = ""
            'End Ricerca Articoli
            If Not System.IO.Directory.Exists(Server.MapPath("~\Content\OfferteFornitori\" + lav.Num)) Then
                System.IO.Directory.CreateDirectory(Server.MapPath("~\Content\OfferteFornitori\" + lav.Num))
            End If
            Dim listPathPDF As New List(Of String)
            Dim listPathPDFDDT As New List(Of String)
            Try
                Dim count = 0
                Dim startIndex = 0
                Dim EndIndex = 12
                If EndIndex > listOfArticoli.Count - 1 Then
                    EndIndex = listOfArticoli.Count - 1
                End If
                Dim listArt = 0
                Dim countArt = 0
                Dim arrayArticoli = listOfArticoli.ToArray
                While countArt < listOfArticoli.Count
                    fornitore.listOfArticoli = listOfArticoli
                    Dim oldFile = Server.MapPath("~\Content\Template\OffertaFornitore.pdf")
                    Dim newFile = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\" + count.ToString + "_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf")
                    listPathPDF.Add(newFile)
                    Dim reader As PdfReader = New PdfReader(oldFile)
                    Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED)
                    Dim tablefont As Font = New Font(bf, 8)
                    Dim ms As New MemoryStream
                    Dim fs As FileStream = System.IO.File.Create(newFile)
                    Dim stamper As New PdfStamper(reader, fs)
                    'stamper.InsertPage(2, reader.GetPageSize(1))
                    Using stamper
                        Dim canvas As PdfContentByte = stamper.GetOverContent(1)
                        canvas.SetColorFill(BaseColor.BLACK)
                        canvas.SetFontAndSize(bf, 35)
                        Dim ct As ColumnText = New ColumnText(canvas)
                        Dim cli As Phrase = New Phrase(fornitore.Cliente, tablefont)
                        ct.SetSimpleColumn(cli, 335, 620, 525, 680, 15, Element.ALIGN_LEFT)
                        ct.Go()
                        'OL
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.OL, tablefont), 36, 701, 0)
                        'DATA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Data, tablefont), 95, 701, 0)
                        'CodFornitore
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Fornitore, tablefont), 36, 675, 0)
                        'PIVA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.PIVA, tablefont), 135, 675, 0)
                        'spedizioniere
                        'consegna
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Consegna, tablefont), 36, 620, 0)
                        'pagamento
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Pagamento, tablefont), 36, 595, 0)
                        'banca
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Banca_Dappoggio, tablefont), 33, 565, 0)
                        'abi
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Abi, tablefont), 50, 556, 0)
                        'Cab
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cab, tablefont), 140, 556, 0)
                        'CC
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.CC, tablefont), 230, 556, 0)
                        If EndIndex = listOfArticoli.Count - 1 And fornitore.Totale <> "0,000" Then
                            Dim ctImp As ColumnText = New ColumnText(canvas)
                            ctImp.SetSimpleColumn(New Phrase(fornitore.Totale.Substring(0, fornitore.Totale.Length - 1).Insert(fornitore.Totale.Length - 1, "€"), New Font(bf, 7)), 478, 96, 535, 96 + 15, 15, Element.ALIGN_LEFT)
                            ctImp.Go()
                        ElseIf EndIndex = listOfArticoli.Count - 1 And fornitore.Totale = "0,000" Then

                        Else
                            Dim ctImp As ColumnText = New ColumnText(canvas)
                            ctImp.SetSimpleColumn(New Phrase("Segue Pagina", New Font(bf, 7)), 478, 96, 535, 96 + 15, 15, Element.ALIGN_LEFT)
                            ctImp.Go()
                        End If
                        Dim startingPoint = 500
                        For c = startIndex To EndIndex
                            Try
                                'CODART
                                Dim ctart As ColumnText = New ColumnText(canvas)
                                ctart.SetSimpleColumn(New Phrase(arrayArticoli(c).cod_articolo, tablefont), 27, startingPoint, 650, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctart.Go()
                                'DESC
                                Dim ctdesc As ColumnText = New ColumnText(canvas)
                                ctdesc.SetSimpleColumn(New Phrase(arrayArticoli(c).desc_articolo, tablefont), 105, startingPoint, 325, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctdesc.Go()
                                'UM
                                Dim ctum As ColumnText = New ColumnText(canvas)
                                ctum.SetSimpleColumn(New Phrase(arrayArticoli(c).UM, tablefont), 320, startingPoint, 335, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctum.Go()
                                'QTA
                                Dim ctqta As ColumnText = New ColumnText(canvas)
                                ctqta.SetSimpleColumn(New Phrase(arrayArticoli(c).qta, tablefont), 350, startingPoint, 365, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctqta.Go()
                                'Importo
                                Dim ctImp As ColumnText = New ColumnText(canvas)
                                ctImp.SetSimpleColumn(New Phrase(arrayArticoli(c).importo, New Font(bf, 7)), 478, startingPoint, 535, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctImp.Go()
                                'Consegna
                                Dim ctCons As ColumnText = New ColumnText(canvas)
                                ctCons.SetSimpleColumn(New Phrase(arrayArticoli(c).Consegna, New Font(bf, 7)), 540, startingPoint, 675, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctCons.Go()
                                startingPoint = startingPoint - 25
                            Catch ex As Exception
                                db.Log.Add(New Log With {
                                   .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                                   .Livello = TipoLogLivello.Errors,
                                   .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                   .Messaggio = "Errore Invio Bolla (Impostazione dati)-> " & ex.Message,
                                   .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                                   })
                                db.SaveChanges()
                            End Try
                        Next
                    End Using
                    count = count + 1
                    If countArt + 10 > listOfArticoli.Count Then
                        countArt = listOfArticoli.Count
                    Else
                        countArt = countArt + 10
                    End If
                    startIndex = startIndex + 10
                    If EndIndex + 10 > listOfArticoli.Count Then
                        EndIndex = listOfArticoli.Count - 1
                    Else
                        EndIndex = EndIndex + 10
                    End If
                    fs.Close()
                    reader.Close()



                End While

            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla (Creazione PDF) -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Dim document = New Document()
            Dim outFile = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\FIN_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf") ' The outPutPDF varable is passed from another sub this is the output path
            Dim writer = New PdfCopy(document, New FileStream(outFile, FileMode.Create))
            Try
                document.Open()
                For Each file As String In listPathPDF.ToArray
                    Dim reader = New PdfReader(file)
                    For i As Integer = 1 To reader.NumberOfPages
                        Dim page = writer.GetImportedPage(reader, i)
                        writer.AddPage(page)
                    Next i
                    reader.Close()
                Next
                writer.Close()
                document.Close()
            Catch ex As Exception
                'catch a Exception if needed

            Finally
                path_File = outFile
                writer.Close()
                document.Close()

            End Try
            Try
                Dim count = 0
                Dim startIndex = 0
                Dim EndIndex = 12
                If EndIndex > listOfArticoli.Count - 1 Then
                    EndIndex = listOfArticoli.Count - 1
                End If
                Dim listArt = 0
                Dim countArt = 0
                Dim arrayArticoli = listOfArticoli.ToArray

                While countArt < listOfArticoli.Count
                    fornitore.listOfArticoli = listOfArticoli
                    Dim oldFile = Server.MapPath("~\Content\Template\OffertaFornitoreDDT.pdf")
                    Dim newfile = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\ddt_" + count.ToString + "_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf")
                    listPathPDFDDT.Add(newfile)
                    Dim reader As PdfReader = New PdfReader(oldFile)
                    Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED)
                    Dim tablefont As Font = New Font(bf, 8)
                    Dim tablefontSmall As Font = New Font(bf, 5)
                    Dim FontColour As BaseColor = New BaseColor(128, 128, 128)
                    Dim Calibri8 As Font = FontFactory.GetFont("COURIER_OBLIQUE", 8, FontColour)
                    Dim ms As New MemoryStream
                    Dim fs As FileStream = System.IO.File.Create(newfile)
                    Dim stamper As New PdfStamper(reader, fs)
                    'stamper.InsertPage(2, reader.GetPageSize(1))
                    Using stamper
                        Dim canvas As PdfContentByte = stamper.GetOverContent(1)
                        canvas.SetColorFill(BaseColor.BLACK)
                        canvas.SetFontAndSize(bf, 35)
                        Dim ct As ColumnText = New ColumnText(canvas)
                        Dim cli As Phrase = New Phrase(fornitore.Cliente, tablefont)
                        ct.SetSimpleColumn(cli, 385, 660, 625, 715, 15, Element.ALIGN_LEFT)
                        ct.Go()
                        'preb
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Bolla, Calibri8), 98, 734, 0)
                        'OL
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Prebolla, tablefont), 36, 665, 0)
                        'DATA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Data, tablefont), 125, 665, 0)
                        'causatrasp
                        ' ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Data, tablefont), 125, 680, 0)
                        'CodFornitore
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Fornitore, tablefont), 80, 646, 0)
                        'PIVA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.PIVA, tablefont), 80, 636, 0)
                        'cf
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Fiscale, tablefont), 285, 636, 0)
                        'doctype
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Documento_Type, tablefont), 125, 625, 0)
                        'spedizioniere
                        'consegna
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Consegna, tablefont), 75, 602, 0)
                        'clavoro
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase("Bolla di C/lavoro", tablefont), 100, 580, 0)
                        'pagamento
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Pagamento, tablefont), 100, 570, 0)
                        'oepratore
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Operatore, tablefont), 100, 560, 0)
                        Dim startingPoint = 515
                        For c = startIndex To EndIndex
                            'CODART
                            Dim ctart As ColumnText = New ColumnText(canvas)
                            ctart.SetSimpleColumn(New Phrase(arrayArticoli(c).cod_articolo, tablefont), 25, startingPoint, 650, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctart.Go()
                            'DESC
                            Dim ctdesc As ColumnText = New ColumnText(canvas)
                            ctdesc.SetSimpleColumn(New Phrase(arrayArticoli(c).desc_articolo, tablefont), 125, startingPoint, 325, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctdesc.Go()
                            'UM
                            Dim ctImp As ColumnText = New ColumnText(canvas)
                            ctImp.SetSimpleColumn(New Phrase(arrayArticoli(c).UM, New Font(bf, 7)), 485, startingPoint, 535, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctImp.Go()
                            'QTA
                            Dim ctCons As ColumnText = New ColumnText(canvas)
                            ctCons.SetSimpleColumn(New Phrase(arrayArticoli(c).qta, New Font(bf, 7)), 540, startingPoint, 675, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctCons.Go()
                            startingPoint = startingPoint - 15
                        Next
                    End Using
                    count = count + 1
                    startIndex = startIndex + 10

                    If countArt + 10 > listOfArticoli.Count Then
                        countArt = listOfArticoli.Count
                    Else
                        countArt = countArt + 10
                    End If

                    If EndIndex + 10 > listOfArticoli.Count Then
                        EndIndex = listOfArticoli.Count - 1
                    Else
                        EndIndex = EndIndex + 10
                    End If
                    fs.Close()
                    reader.Close()
                End While

            Catch ex As Exception
                db.Log.Add(New Log With {
                     .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                     .Livello = TipoLogLivello.Errors,
                     .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                     .Messaggio = "Errore Invio Bolla (Creazione PDF 2) -> " & ex.Message,
                     .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                     })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Dim documentDDT = New Document()
            Dim outFileDDT = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\FIN_DDT_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf") ' The outPutPDF varable is passed from another sub this is the output path
            Dim writerDDT = New PdfCopy(documentDDT, New FileStream(outFileDDT, FileMode.Create))
            Try
                documentDDT.Open()
                For Each file As String In listPathPDFDDT.ToArray
                    Dim reader = New PdfReader(file)
                    For i As Integer = 1 To reader.NumberOfPages
                        Dim page = writerDDT.GetImportedPage(reader, i)
                        writerDDT.AddPage(page)
                    Next i
                    reader.Close()
                Next
                writer.Close()
                document.Close()
            Catch ex As Exception
                db.Log.Add(New Log With {
                                   .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                                   .Livello = TipoLogLivello.Errors,
                                   .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                   .Messaggio = "Errore Invio Bolla (Salvatggio PDF) -> " & ex.Message,
                                   .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                                   })
                db.SaveChanges()
            Finally
                path_File_DDT = outFileDDT
                writerDDT.Close()
                documentDDT.Close()

            End Try
            Try
                Dim u = appctx.Users.Where(Function(x) x.UserName = OpName.ToString).First 'db.UserEmail.Where(Function(x) x.Uid = OC.OperatoreInsert).First
                Dim mySmtp As New SmtpClient
                Dim myMail As New MailMessage()
                Dim password = Decrypter(u.Profile.PWD_Email)
                mySmtp.UseDefaultCredentials = False
                mySmtp.Credentials = New System.Net.NetworkCredential(u.Email, password)
                mySmtp.Host = "squirtle.dnshigh.com"
                myMail = New MailMessage()
                myMail.From = New MailAddress(u.Email)
                If Not lav.FlagMailAuto Then
                    myMail.To.Add(lav.email)
                Else
                    myMail.To.Add(lav.email) 'Change this con quello da database
                End If
                Dim firma = "Euroma Group"
                If u.Profile.Firma.Length > 0 Then
                    firma = u.Profile.Firma
                End If
                myMail.Subject = "Lavorazione esterna - " + lav.Num
                myMail.IsBodyHtml = True
                myMail.Body = "<html> <body>Buongiorno, <br>ad inviare ordine. <br> Attendiamo i prezzi dove mancanti o non concordati.<br> " + firma + " </html> </body> "
                myMail.Attachments.Add(New System.Net.Mail.Attachment(path_File))
                If lav.FlagBoth Then
                    myMail.Attachments.Add(New System.Net.Mail.Attachment(path_File_DDT))
                End If
                mySmtp.Send(myMail)
                Dim lavF = db.LavorazioniEsterne.Where(Function(x) x.Id = id).First
                lavF.Inviato = Enum_Bolla.Inviato
                db.SaveChanges()
                lavF.Path_DDT = path_File_DDT
                lavF.Path_Doc = path_File
                lavF.Id_Operatore = OpID
                lavF.Operatore = OpName
                db.SaveChanges()
            Catch ex As Exception
                db.Log.Add(New Log With {
                   .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                   .Livello = TipoLogLivello.Errors,
                   .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                   .Messaggio = "Errore Invio Bolla (Stato Finale) -> " & ex.Message,
                   .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {.id = id})
                   })
                db.SaveChanges()
            End Try

            Return Json(New With {.ok = True, .message = "File inviati correttamente!"}, JsonRequestBehavior.AllowGet)
        End Function
        <Authorize>
        Function LavorazioniEsterneTest(ByVal id As Integer) As JsonResult
            Dim tmp_Lav = db.LavorazioniEsterne.Where(Function(x) x.Id = id).First.OL.Split("_")
            Dim lav As New RichiestaOLviewModel With {
                .Esecod = tmp_Lav(0),
                .OL = tmp_Lav(1),
                .Num = tmp_Lav(2),
                .email = "m.zucchini@euromagroup.com",
                .FlagBoth = True,
                .FlagMailAuto = True
            }
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Dim fornitore As New OlviewModel
            Dim listOfArticoli As New List(Of ArticoliOL)
            'Ricerca se già stata inviata
            If db.LavorazioniEsterne.Where(Function(x) x.Id = id).First.Inviato = Enum_Bolla.Test_interno Then
                Return Json(New With {.ok = False, .message = "Test già effettuato!"}, JsonRequestBehavior.AllowGet)
            End If
            'Creazione modello
            'Ricerca dati
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = " select ORDFOR.ORFCLF, CLIDATA.CLFNMG + CLIIND.CLFIN1 + CLIIND.CLFLC1 + CLIIND.CLFCAP + CLIIND.CLFPLC + ' ' +   CLIIND.NAZCOD, CLIDATA.CLFCPI, CLIDATA.CLFNCC, CLIDATA.CLFABA, CLIDATA.CLFCAA, ABI.ABIDES  + ' ' + CAB.CABDES, RES.RESDES, PAG.PAGDES,CLIDATA.CLFCFS,ORDFOR.ORFTIM from ORFTES00 AS ORDFOR, CLFANA AS CLIDATA, CLFIND AS CLIIND, TABCAB00 AS CAB, TABABI00 AS ABI, TABRES00 AS RES, PAGTAB00 AS PAG where CLIIND.CLFTIP = 'F' AND CLIDATA.CLFTIP = 'F' AND ORDFOR.ESECOD = '" + lav.Esecod + "' and ORDFOR.ORFSEZ = '" + lav.OL + "' AND ORDFOR.ORFNUM = '" + lav.Num + "' AND CLIIND.CLFCOD = ORDFOR.ORFCLF AND CLIDATA.CLFCO1 = ORDFOR.ORFCLF AND CLIDATA.CLFABA = ABI.ABICO1 AND CLIDATA.CLFCAA = CAB.CABCO1 AND CLIDATA.CLFABA = CAB.ABICO1 AND ORDFOR.RESCOD = RES.RESCO1 AND ORDFOR.PAGCOD = PAG.PAGCO1"
                myConn.Open()
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Dim fornitoreTMP As New OlviewModel With {
                        .Cod_Fornitore = myReader.GetString(0),
                        .Cliente = myReader.GetString(1),
                        .PIVA = myReader.GetString(2),
                        .CC = myReader.GetString(3),
                        .Abi = myReader.GetString(4),
                        .Cab = myReader.GetString(5),
                        .Banca_Dappoggio = myReader.GetString(6),
                        .Consegna = myReader.GetString(7),
                        .Pagamento = myReader.GetString(8),
                        .Data = DateTime.Now.ToString.Split(" ")(0),
                        .OL = lav.OL + "/" + lav.Num,
                        .Cod_Fiscale = myReader.GetString(9),
                        .Totale = myReader.GetDecimal(10).ToString,
                        .listOfArticoli = New List(Of ArticoliOL)
                    }
                    fornitore = fornitoreTMP
                Loop
                myConn.Close()
                If fornitore.Cod_Fornitore = "" And fornitore.Cliente = "" Then
                    Try
                        OpID = User.Identity.GetUserId()
                        OpName = User.Identity.GetUserName
                        myConn = New SqlConnection(ConnectionString)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "select ORDFOR.ORFCLF, CLIDATA.CLFNMG + CLIIND.CLFIN1 + CLIIND.CLFLC1 + CLIIND.CLFCAP + CLIIND.CLFPLC + ' ' +   CLIIND.NAZCOD, CLIDATA.CLFCPI, BAN.BAABI, BAN.BACAB, ABI.ABIDES  + ' ' + CAB.CABDES, PAG.PAGDES, CLIDATA.CLFCFS,ORDFOR.ORFTIM from ORFTES00 AS ORDFOR, PAGTAB00 AS PAG, CLFANA AS CLIDATA, CLFIND AS CLIIND, CGTBAN00 AS BAN, TABCAB00 AS CAB, TABABI00 AS ABI where CLIIND.CLFTIP = 'F' AND CLIDATA.CLFTIP = 'F' AND ORDFOR.ESECOD = '" + lav.Esecod + "' and ORDFOR.ORFSEZ = '" + lav.OL + "' AND ORDFOR.ORFNUM = '" + lav.Num + "' AND CLIIND.CLFCOD = ORDFOR.ORFCLF AND CLIDATA.CLFCO1 = ORDFOR.ORFCLF AND ORDFOR.PAGCOD = PAG.PAGCO1 AND BAN.BABAN = CLIDATA.CLFABP AND BAN.BAABI = ABI.ABICO1 AND BAN.BACAB = CAB.CABCO1 AND BAN.BAABI = CAB.ABICO1"
                        myConn.Open()
                    Catch ex As Exception
                        db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                        db.SaveChanges()
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader

                        Do While myReader.Read()
                            Dim fornitoreTMP As New OlviewModel With {
                                .Cod_Fornitore = myReader.GetString(0),
                                .Cliente = myReader.GetString(1),
                                .PIVA = myReader.GetString(2),
                                .CC = "",
                                .Abi = myReader.GetString(3),
                                .Cab = myReader.GetString(4),
                                .Banca_Dappoggio = myReader.GetString(5),
                                .Consegna = "",
                                .Pagamento = myReader.GetString(6),
                                .Data = DateTime.Now.ToString.Split(" ")(0),
                                .OL = lav.OL + "/" + lav.Num,
                                .Cod_Fiscale = myReader.GetString(7),
                                .Totale = myReader.GetDecimal(8).ToString,
                                .listOfArticoli = New List(Of ArticoliOL)
                            }
                            fornitore = fornitoreTMP
                        Loop
                        myConn.Close()
                    Catch ex As Exception
                        db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                        db.SaveChanges()
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
                    End Try
                End If
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            'Ricerca articoli
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "select ARTCOD,ORFCONREV,ORFDAN, ORFQTY, TUMCOD,ORFPVA from ORFDET00 where ESECOD = '" + lav.Esecod + "' and ORFSEZ = '" + lav.OL + "' AND ORFNUM = '" + lav.Num + "'"
                myConn.Open()
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Try
                myReader = myCmd.ExecuteReader
                Do While myReader.Read()
                    Dim consegna = myReader.GetDecimal(1).ToString
                    Dim art As New ArticoliOL With {
                        .cod_articolo = myReader.GetString(0),
                        .Consegna = consegna.ToString.Insert(6, "/").Insert(4, "/"),
                        .desc_articolo = myReader.GetString(2),
                        .importo = IIf(IsNothing(myReader.GetDecimal(5)), "Da Concordare", myReader.GetDecimal(5).ToString),
                        .qta = myReader.GetDecimal(3).ToString,
                        .UM = myReader.GetString(4)
                    }

                    listOfArticoli.Add(art)

                Loop
                myConn.Close()
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Try
                Dim artListString = "AND ( "
                Dim r = 0
                For Each a In listOfArticoli
                    If r = 1 Then
                        artListString = artListString + " OR PREB.ARTCOD = '" + a.cod_articolo + "'"
                    Else
                        artListString = artListString + " PREB.ARTCOD = '" + a.cod_articolo + "'"
                        r = 1
                    End If
                Next
                artListString = artListString + ")"
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "SELECT DISTINCT PREB.BAMNRR, BAMTES.BOLNRR, BAMTES.BAMCTR, BAMTES.BAMUTE FROM BAMDET00 AS PREB, ORFDET00 AS ORDIN, BAMTES00 as BAMTES WHERE PREB.BAMCCD = ORDIN.COMCOD AND BAMTES.BAMNRR = PREB.BAMNRR AND PREB.ORCCLI = ORDIN.ORFCLF AND ORDIN.ORFSEZ = 'OL' AND ORDIN.ESECOD = '2022' AND PREB.ESECOD = '2022' " + artListString
                myConn.Open()
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    fornitore.Cod_Bolla = "PL / " + myReader.GetDecimal(0).ToString
                    fornitore.Cod_Prebolla = "BO /" + myReader.GetDecimal(1).ToString
                    fornitore.Documento_Type = myReader.GetString(2)
                    fornitore.Operatore = myReader.GetString(3)
                Loop
                myConn.Close()
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Dim path_File = ""
            Dim path_File_DDT = ""
            'End Ricerca Articoli
            If Not System.IO.Directory.Exists(Server.MapPath("~\Content\OfferteFornitori\" + lav.Num)) Then
                System.IO.Directory.CreateDirectory(Server.MapPath("~\Content\OfferteFornitori\" + lav.Num))
            End If
            Dim listPathPDF As New List(Of String)
            Dim listPathPDFDDT As New List(Of String)
            Try
                Dim count = 0
                Dim startIndex = 0
                Dim EndIndex = 12
                If EndIndex > listOfArticoli.Count - 1 Then
                    EndIndex = listOfArticoli.Count - 1
                End If
                Dim listArt = 0
                Dim countArt = 0
                Dim arrayArticoli = listOfArticoli.ToArray
                While countArt < listOfArticoli.Count
                    fornitore.listOfArticoli = listOfArticoli
                    Dim oldFile = Server.MapPath("~\Content\Template\OffertaFornitore.pdf")
                    Dim newFile = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\" + count.ToString + "_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf")
                    listPathPDF.Add(newFile)
                    Dim reader As PdfReader = New PdfReader(oldFile)
                    Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED)
                    Dim tablefont As Font = New Font(bf, 8)
                    Dim ms As New MemoryStream
                    Dim fs As FileStream = System.IO.File.Create(newFile)
                    Dim stamper As New PdfStamper(reader, fs)
                    'stamper.InsertPage(2, reader.GetPageSize(1))
                    Using stamper
                        Dim canvas As PdfContentByte = stamper.GetOverContent(1)
                        canvas.SetColorFill(BaseColor.BLACK)
                        canvas.SetFontAndSize(bf, 35)
                        Dim ct As ColumnText = New ColumnText(canvas)
                        Dim cli As Phrase = New Phrase(fornitore.Cliente, tablefont)
                        ct.SetSimpleColumn(cli, 335, 620, 525, 680, 15, Element.ALIGN_LEFT)
                        ct.Go()
                        'OL
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.OL, tablefont), 36, 701, 0)
                        'DATA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Data, tablefont), 95, 701, 0)
                        'CodFornitore
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Fornitore, tablefont), 36, 675, 0)
                        'PIVA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.PIVA, tablefont), 135, 675, 0)
                        'spedizioniere
                        'consegna
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Consegna, tablefont), 36, 620, 0)
                        'pagamento
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Pagamento, tablefont), 36, 595, 0)
                        'banca
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Banca_Dappoggio, tablefont), 33, 565, 0)
                        'abi
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Abi, tablefont), 50, 556, 0)
                        'Cab
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cab, tablefont), 140, 556, 0)
                        'CC
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.CC, tablefont), 230, 556, 0)
                        If EndIndex = listOfArticoli.Count - 1 And fornitore.Totale <> "0,000" Then
                            Dim ctImp As ColumnText = New ColumnText(canvas)
                            ctImp.SetSimpleColumn(New Phrase(fornitore.Totale.Substring(0, fornitore.Totale.Length - 1).Insert(fornitore.Totale.Length - 1, "€"), New Font(bf, 7)), 478, 96, 535, 96 + 15, 15, Element.ALIGN_LEFT)
                            ctImp.Go()
                        ElseIf EndIndex = listOfArticoli.Count - 1 And fornitore.Totale = "0,000" Then

                        Else
                            Dim ctImp As ColumnText = New ColumnText(canvas)
                            ctImp.SetSimpleColumn(New Phrase("Segue Pagina", New Font(bf, 7)), 478, 96, 535, 96 + 15, 15, Element.ALIGN_LEFT)
                            ctImp.Go()
                        End If
                        Dim startingPoint = 500
                        For c = startIndex To EndIndex
                            Try
                                'CODART
                                Dim ctart As ColumnText = New ColumnText(canvas)
                                ctart.SetSimpleColumn(New Phrase(arrayArticoli(c).cod_articolo, tablefont), 27, startingPoint, 650, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctart.Go()
                                'DESC
                                Dim ctdesc As ColumnText = New ColumnText(canvas)
                                ctdesc.SetSimpleColumn(New Phrase(arrayArticoli(c).desc_articolo, tablefont), 105, startingPoint, 325, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctdesc.Go()
                                'UM
                                Dim ctum As ColumnText = New ColumnText(canvas)
                                ctum.SetSimpleColumn(New Phrase(arrayArticoli(c).UM, tablefont), 320, startingPoint, 335, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctum.Go()
                                'QTA
                                Dim ctqta As ColumnText = New ColumnText(canvas)
                                ctqta.SetSimpleColumn(New Phrase(arrayArticoli(c).qta, tablefont), 350, startingPoint, 365, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctqta.Go()
                                'Importo
                                Dim ctImp As ColumnText = New ColumnText(canvas)
                                ctImp.SetSimpleColumn(New Phrase(arrayArticoli(c).importo, New Font(bf, 7)), 478, startingPoint, 535, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctImp.Go()
                                'Consegna
                                Dim ctCons As ColumnText = New ColumnText(canvas)
                                ctCons.SetSimpleColumn(New Phrase(arrayArticoli(c).Consegna, New Font(bf, 7)), 540, startingPoint, 675, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctCons.Go()
                                startingPoint = startingPoint - 25
                            Catch ex As Exception
                                db.Log.Add(New Log With {
                                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                                  .Livello = TipoLogLivello.Errors,
                                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                                  .Messaggio = "Err invio a cliente ",
                                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                                  })
                                db.SaveChanges()
                            End Try
                        Next
                    End Using
                    count = count + 1
                    If countArt + 10 > listOfArticoli.Count Then
                        countArt = listOfArticoli.Count
                    Else
                        countArt = countArt + 10
                    End If
                    startIndex = startIndex + 10
                    If EndIndex + 10 > listOfArticoli.Count Then
                        EndIndex = listOfArticoli.Count - 1
                    Else
                        EndIndex = EndIndex + 10
                    End If
                    fs.Close()
                    reader.Close()



                End While

            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Dim document = New Document()
            Dim outFile = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\FIN_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf") ' The outPutPDF varable is passed from another sub this is the output path
            Dim writer = New PdfCopy(document, New FileStream(outFile, FileMode.Create))
            Try
                document.Open()
                For Each file As String In listPathPDF.ToArray
                    Dim reader = New PdfReader(file)
                    For i As Integer = 1 To reader.NumberOfPages
                        Dim page = writer.GetImportedPage(reader, i)
                        writer.AddPage(page)
                    Next i
                    reader.Close()
                Next
                writer.Close()
                document.Close()
            Catch ex As Exception
                'catch a Exception if needed
                db.Log.Add(New Log With {
          .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
          .Livello = TipoLogLivello.Errors,
          .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
          .Messaggio = "Err invio a cliente ",
          .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
          })
                db.SaveChanges()
            Finally
                path_File = outFile
                writer.Close()
                document.Close()

            End Try
            Try
                Dim count = 0
                Dim startIndex = 0
                Dim EndIndex = 12
                If EndIndex > listOfArticoli.Count - 1 Then
                    EndIndex = listOfArticoli.Count - 1
                End If
                Dim listArt = 0
                Dim countArt = 0
                Dim arrayArticoli = listOfArticoli.ToArray

                While countArt < listOfArticoli.Count
                    fornitore.listOfArticoli = listOfArticoli
                    Dim oldFile = Server.MapPath("~\Content\Template\OffertaFornitoreDDT.pdf")
                    Dim newfile = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\ddt_" + count.ToString + "_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf")
                    listPathPDFDDT.Add(newfile)
                    Dim reader As PdfReader = New PdfReader(oldFile)
                    Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED)
                    Dim tablefont As Font = New Font(bf, 8)
                    Dim tablefontSmall As Font = New Font(bf, 5)
                    Dim FontColour As BaseColor = New BaseColor(128, 128, 128)
                    Dim Calibri8 As Font = FontFactory.GetFont("COURIER_OBLIQUE", 8, FontColour)
                    Dim ms As New MemoryStream
                    Dim fs As FileStream = System.IO.File.Create(newfile)
                    Dim stamper As New PdfStamper(reader, fs)
                    'stamper.InsertPage(2, reader.GetPageSize(1))
                    Using stamper
                        Dim canvas As PdfContentByte = stamper.GetOverContent(1)
                        canvas.SetColorFill(BaseColor.BLACK)
                        canvas.SetFontAndSize(bf, 35)
                        Dim ct As ColumnText = New ColumnText(canvas)
                        Dim cli As Phrase = New Phrase(fornitore.Cliente, tablefont)
                        ct.SetSimpleColumn(cli, 385, 660, 625, 715, 15, Element.ALIGN_LEFT)
                        ct.Go()
                        'preb
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Bolla, Calibri8), 98, 734, 0)
                        'OL
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Prebolla, tablefont), 36, 665, 0)
                        'DATA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Data, tablefont), 125, 665, 0)
                        'causatrasp
                        ' ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Data, tablefont), 125, 680, 0)
                        'CodFornitore
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Fornitore, tablefont), 80, 646, 0)
                        'PIVA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.PIVA, tablefont), 80, 636, 0)
                        'cf
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Fiscale, tablefont), 285, 636, 0)
                        'doctype
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Documento_Type, tablefont), 125, 625, 0)
                        'spedizioniere
                        'consegna
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Consegna, tablefont), 75, 602, 0)
                        'clavoro
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase("Bolla di C/lavoro", tablefont), 100, 580, 0)
                        'pagamento
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Pagamento, tablefont), 100, 570, 0)
                        'oepratore
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Operatore, tablefont), 100, 560, 0)
                        Dim startingPoint = 515
                        For c = startIndex To EndIndex
                            'CODART
                            Dim ctart As ColumnText = New ColumnText(canvas)
                            ctart.SetSimpleColumn(New Phrase(arrayArticoli(c).cod_articolo, tablefont), 25, startingPoint, 650, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctart.Go()
                            'DESC
                            Dim ctdesc As ColumnText = New ColumnText(canvas)
                            ctdesc.SetSimpleColumn(New Phrase(arrayArticoli(c).desc_articolo, tablefont), 125, startingPoint, 325, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctdesc.Go()
                            'UM
                            Dim ctImp As ColumnText = New ColumnText(canvas)
                            ctImp.SetSimpleColumn(New Phrase(arrayArticoli(c).UM, New Font(bf, 7)), 485, startingPoint, 535, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctImp.Go()
                            'QTA
                            Dim ctCons As ColumnText = New ColumnText(canvas)
                            ctCons.SetSimpleColumn(New Phrase(arrayArticoli(c).qta, New Font(bf, 7)), 540, startingPoint, 675, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctCons.Go()
                            startingPoint = startingPoint - 15
                        Next
                    End Using
                    count = count + 1
                    startIndex = startIndex + 10

                    If countArt + 10 > listOfArticoli.Count Then
                        countArt = listOfArticoli.Count
                    Else
                        countArt = countArt + 10
                    End If

                    If EndIndex + 10 > listOfArticoli.Count Then
                        EndIndex = listOfArticoli.Count - 1
                    Else
                        EndIndex = EndIndex + 10
                    End If
                    fs.Close()
                    reader.Close()
                End While

            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."}, JsonRequestBehavior.AllowGet)
            End Try
            Dim documentDDT = New Document()
            Dim outFileDDT = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\FIN_DDT_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf") ' The outPutPDF varable is passed from another sub this is the output path
            Dim writerDDT = New PdfCopy(documentDDT, New FileStream(outFileDDT, FileMode.Create))
            Try
                documentDDT.Open()
                For Each file As String In listPathPDFDDT.ToArray
                    Dim reader = New PdfReader(file)
                    For i As Integer = 1 To reader.NumberOfPages
                        Dim page = writerDDT.GetImportedPage(reader, i)
                        writerDDT.AddPage(page)
                    Next i
                    reader.Close()
                Next
                writer.Close()
                document.Close()
            Catch ex As Exception
                'catch a Exception if needed
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
            Finally
                path_File_DDT = outFileDDT
                writerDDT.Close()
                documentDDT.Close()

            End Try
            Dim u = appctx.Users.Where(Function(x) x.UserName = OpName.ToString).First 'db.UserEmail.Where(Function(x) x.Uid = OC.OperatoreInsert).First
            Dim mySmtp As New SmtpClient
            Dim myMail As New MailMessage()
            Dim password = Decrypter(u.Profile.PWD_Email)
            mySmtp.UseDefaultCredentials = False
            mySmtp.Credentials = New System.Net.NetworkCredential(u.Email, password)
            mySmtp.Host = "squirtle.dnshigh.com"
            myMail = New MailMessage()
            myMail.From = New MailAddress(u.Email)
            If Not lav.FlagMailAuto Then
                myMail.To.Add(u.Email)
            Else
                myMail.To.Add(u.Email) 'Change this con quello da database
            End If
            Dim firma = "Euroma Group"
            If u.Profile.Firma.Length > 0 Then
                firma = u.Profile.Firma
            End If
            myMail.Subject = "Lavorazione esterna - " + lav.Num
            myMail.IsBodyHtml = True
            myMail.Body = "<html> <body>Buongiorno, <br>ad inviare ordine. <br> Attendiamo i prezzi dove mancanti o non concordati.<br> " + firma + " </html> </body> "
            myMail.Attachments.Add(New System.Net.Mail.Attachment(path_File))
            If lav.FlagBoth Then
                myMail.Attachments.Add(New System.Net.Mail.Attachment(path_File_DDT))
            End If
            mySmtp.Send(myMail)
            Dim lavF = db.LavorazioniEsterne.Where(Function(x) x.Id = id).First
            lavF.Inviato = Enum_Bolla.Test_interno
            lavF.Path_DDT = path_File_DDT
            lavF.Path_Doc = path_File
            lavF.Id_Operatore = OpID
            lavF.Operatore = OpName
            db.SaveChanges()
            db.Log.Add(New Log With {
                         .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                         .Livello = TipoLogLivello.Errors,
                         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                         .Messaggio = "File correttamente inviati al cliente",
                         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {lav})
                         })
            db.SaveChanges()
            Return Json(New With {.ok = True, .message = "File inviati correttamente!"}, JsonRequestBehavior.AllowGet)
        End Function

        <Authorize>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function LavorazioniEsterne(<Bind(Include:="Esecod,OL,Num,Email,FlagMailAuto,FlagBoth")> ByVal lav As RichiestaOLviewModel) As JsonResult
            Dim OpID As String = vbNullString
            Dim OpName As String = vbNullString
            Dim CurrentDate As DateTime = Now
            Dim fornitore As New OlviewModel
            Dim listOfArticoli As New List(Of ArticoliOL)
            'Ricerca se già stata inviata
            If db.LavorazioniEsterne.Where(Function(x) x.OL = lav.Esecod + "_" + lav.OL + "_" + lav.Num).Count >= 1 Then
                Return Json(New With {.ok = False, .message = "OL già inviata!"})
            End If
            'Creazione modello
            'Ricerca dati
            Try
                OpID = User.Identity.GetUserId()
                OpName = User.Identity.GetUserName
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = " select ORDFOR.ORFCLF, CLIDATA.CLFNMG + CLIIND.CLFIN1 + CLIIND.CLFLC1 + CLIIND.CLFCAP + CLIIND.CLFPLC + ' ' +   CLIIND.NAZCOD, CLIDATA.CLFCPI, CLIDATA.CLFNCC, CLIDATA.CLFABA, CLIDATA.CLFCAA, ABI.ABIDES  + ' ' + CAB.CABDES, RES.RESDES, PAG.PAGDES,CLIDATA.CLFCFS,ORDFOR.ORFTIM from ORFTES00 AS ORDFOR, CLFANA AS CLIDATA, CLFIND AS CLIIND, TABCAB00 AS CAB, TABABI00 AS ABI, TABRES00 AS RES, PAGTAB00 AS PAG where CLIIND.CLFTIP = 'F' AND CLIDATA.CLFTIP = 'F' AND ORDFOR.ESECOD = '" + lav.Esecod + "' and ORDFOR.ORFSEZ = '" + lav.OL + "' AND ORDFOR.ORFNUM = '" + lav.Num + "' AND CLIIND.CLFCOD = ORDFOR.ORFCLF AND CLIDATA.CLFCO1 = ORDFOR.ORFCLF AND CLIDATA.CLFABA = ABI.ABICO1 AND CLIDATA.CLFCAA = CAB.CABCO1 AND CLIDATA.CLFABA = CAB.ABICO1 AND ORDFOR.RESCOD = RES.RESCO1 AND ORDFOR.PAGCOD = PAG.PAGCO1"
                myConn.Open()
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Dim fornitoreTMP As New OlviewModel With {
                        .Cod_Fornitore = myReader.GetString(0),
                        .Cliente = myReader.GetString(1),
                        .PIVA = myReader.GetString(2),
                        .CC = myReader.GetString(3),
                        .Abi = myReader.GetString(4),
                        .Cab = myReader.GetString(5),
                        .Banca_Dappoggio = myReader.GetString(6),
                        .Consegna = myReader.GetString(7),
                        .Pagamento = myReader.GetString(8),
                        .Data = DateTime.Now.ToString.Split(" ")(0),
                        .OL = lav.OL + "/" + lav.Num,
                        .Cod_Fiscale = myReader.GetString(9),
                        .Totale = myReader.GetDecimal(10).ToString,
                        .listOfArticoli = New List(Of ArticoliOL)
                    }
                    fornitore = fornitoreTMP
                    If IsNothing(fornitore.Totale) Then
                        fornitore.Totale = "0,000"
                    End If
                Loop
                myConn.Close()
                If fornitore.Cod_Fornitore = "" And fornitore.Cliente = "" Then
                    Try
                        OpID = User.Identity.GetUserId()
                        OpName = User.Identity.GetUserName
                        myConn = New SqlConnection(ConnectionString)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "select ORDFOR.ORFCLF, CLIDATA.CLFNMG + CLIIND.CLFIN1 + CLIIND.CLFLC1 + CLIIND.CLFCAP + CLIIND.CLFPLC + ' ' +   CLIIND.NAZCOD, CLIDATA.CLFCPI, BAN.BAABI, BAN.BACAB, ABI.ABIDES  + ' ' + CAB.CABDES, PAG.PAGDES, CLIDATA.CLFCFS,ORDFOR.ORFTIM from ORFTES00 AS ORDFOR, PAGTAB00 AS PAG, CLFANA AS CLIDATA, CLFIND AS CLIIND, CGTBAN00 AS BAN, TABCAB00 AS CAB, TABABI00 AS ABI where CLIIND.CLFTIP = 'F' AND CLIDATA.CLFTIP = 'F' AND ORDFOR.ESECOD = '" + lav.Esecod + "' and ORDFOR.ORFSEZ = '" + lav.OL + "' AND ORDFOR.ORFNUM = '" + lav.Num + "' AND CLIIND.CLFCOD = ORDFOR.ORFCLF AND CLIDATA.CLFCO1 = ORDFOR.ORFCLF AND ORDFOR.PAGCOD = PAG.PAGCO1 AND BAN.BABAN = CLIDATA.CLFABP AND BAN.BAABI = ABI.ABICO1 AND BAN.BACAB = CAB.CABCO1 AND BAN.BAABI = CAB.ABICO1"
                        myConn.Open()
                    Catch ex As Exception
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader

                        Do While myReader.Read()
                            Dim fornitoreTMP As New OlviewModel With {
                                .Cod_Fornitore = myReader.GetString(0),
                                .Cliente = myReader.GetString(1),
                                .PIVA = myReader.GetString(2),
                                .CC = "",
                                .Abi = myReader.GetString(3),
                                .Cab = myReader.GetString(4),
                                .Banca_Dappoggio = myReader.GetString(5),
                                .Consegna = "",
                                .Pagamento = myReader.GetString(6),
                                .Data = DateTime.Now.ToString.Split(" ")(0),
                                .OL = lav.OL + "/" + lav.Num,
                                .Cod_Fiscale = myReader.GetString(7),
                                .Totale = myReader.GetDecimal(8).ToString,
                                .listOfArticoli = New List(Of ArticoliOL)
                            }
                            fornitore = fornitoreTMP
                        Loop
                        myConn.Close()
                    Catch ex As Exception
                        db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                        db.SaveChanges()
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                    End Try
                End If
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            'Ricerca articoli
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "select ARTCOD,ORFCONREV,ORFDAN, ORFQTY, TUMCOD,ORFPVA from ORFDET00 where ESECOD = '" + lav.Esecod + "' and ORFSEZ = '" + lav.OL + "' AND ORFNUM = '" + lav.Num + "'"
                myConn.Open()
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader
                Do While myReader.Read()
                    Dim consegna = myReader.GetDecimal(1).ToString
                    Dim art As New ArticoliOL With {
                        .cod_articolo = myReader.GetString(0),
                        .Consegna = consegna.ToString.Insert(6, "/").Insert(4, "/"),
                        .desc_articolo = myReader.GetString(2),
                        .importo = IIf(IsNothing(myReader.GetString(5)), "Da Concordare", myReader.GetString(5)),
                        .qta = myReader.GetDecimal(3).ToString,
                        .UM = myReader.GetString(4)
                    }

                    listOfArticoli.Add(art)

                Loop
                myConn.Close()
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                Dim artListString = "AND ( "
                Dim r = 0
                For Each a In listOfArticoli
                    If r = 1 Then
                        artListString = artListString + " OR PREB.ARTCOD = '" + a.cod_articolo + "'"
                    Else
                        artListString = artListString + " PREB.ARTCOD = '" + a.cod_articolo + "'"
                        r = 1
                    End If
                Next
                artListString = artListString + ")"
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "SELECT DISTINCT PREB.BAMNRR, BAMTES.BOLNRR, BAMTES.BAMCTR, BAMTES.BAMUTE FROM BAMDET00 AS PREB, ORFDET00 AS ORDIN, BAMTES00 as BAMTES WHERE PREB.BAMCCD = ORDIN.COMCOD AND BAMTES.BAMNRR = PREB.BAMNRR AND PREB.ORCCLI = ORDIN.ORFCLF AND ORDIN.ORFSEZ = 'OL' AND ORDIN.ESECOD = '2022' AND PREB.ESECOD = '2022' " + artListString
                myConn.Open()
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    fornitore.Cod_Bolla = "BO / " + myReader.GetDecimal(0).ToString
                    fornitore.Cod_Prebolla = "PL / " + myReader.GetDecimal(1).ToString
                    fornitore.Documento_Type = myReader.GetString(2)
                    fornitore.Operatore = myReader.GetString(3)
                Loop
                myConn.Close()
            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Dim path_File = ""
            Dim path_File_DDT = ""
            'End Ricerca Articoli
            If Not System.IO.Directory.Exists(Server.MapPath("~\Content\OfferteFornitori\" + lav.Num)) Then
                System.IO.Directory.CreateDirectory(Server.MapPath("~\Content\OfferteFornitori\" + lav.Num))
            End If
            Dim listPathPDF As New List(Of String)
            Dim listPathPDFDDT As New List(Of String)
            Try
                Dim count = 0
                Dim startIndex = 0
                Dim EndIndex = 12
                If EndIndex > listOfArticoli.Count - 1 Then
                    EndIndex = listOfArticoli.Count - 1
                End If
                Dim listArt = 0
                Dim countArt = 0
                Dim arrayArticoli = listOfArticoli.ToArray
                While countArt < listOfArticoli.Count
                    fornitore.listOfArticoli = listOfArticoli
                    Dim oldFile = Server.MapPath("~\Content\Template\OffertaFornitore.pdf")
                    Dim newFile = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\" + count.ToString + "_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf")
                    listPathPDF.Add(newFile)
                    Dim reader As PdfReader = New PdfReader(oldFile)
                    Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED)
                    Dim tablefont As Font = New Font(bf, 8)
                    Dim ms As New MemoryStream
                    Dim fs As FileStream = System.IO.File.Create(newFile)
                    Dim stamper As New PdfStamper(reader, fs)
                    'stamper.InsertPage(2, reader.GetPageSize(1))
                    Using stamper
                        Dim canvas As PdfContentByte = stamper.GetOverContent(1)
                        canvas.SetColorFill(BaseColor.BLACK)
                        canvas.SetFontAndSize(bf, 35)
                        Dim ct As ColumnText = New ColumnText(canvas)
                        Dim cli As Phrase = New Phrase(fornitore.Cliente, tablefont)
                        ct.SetSimpleColumn(cli, 335, 620, 525, 680, 15, Element.ALIGN_LEFT)
                        ct.Go()
                        'OL
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.OL, tablefont), 36, 701, 0)
                        'DATA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Data, tablefont), 95, 701, 0)
                        'CodFornitore
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Fornitore, tablefont), 36, 675, 0)
                        'PIVA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.PIVA, tablefont), 135, 675, 0)
                        'spedizioniere
                        'consegna
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Consegna, tablefont), 36, 620, 0)
                        'pagamento
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Pagamento, tablefont), 36, 595, 0)
                        'banca
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Banca_Dappoggio, tablefont), 33, 565, 0)
                        'abi
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Abi, tablefont), 50, 556, 0)
                        'Cab
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cab, tablefont), 140, 556, 0)
                        'CC
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.CC, tablefont), 230, 556, 0)
                        If EndIndex = listOfArticoli.Count - 1 And fornitore.Totale <> "0,000" Then
                            Dim ctImp As ColumnText = New ColumnText(canvas)
                            ctImp.SetSimpleColumn(New Phrase(fornitore.Totale.Substring(0, fornitore.Totale.Length - 1).Insert(fornitore.Totale.Length - 1, "€"), New Font(bf, 7)), 478, 96, 535, 96 + 15, 15, Element.ALIGN_LEFT)
                            ctImp.Go()
                        ElseIf EndIndex = listOfArticoli.Count - 1 And fornitore.Totale = "0,000" Then

                        Else
                            Dim ctImp As ColumnText = New ColumnText(canvas)
                            ctImp.SetSimpleColumn(New Phrase("Segue Pagina", New Font(bf, 7)), 478, 96, 535, 96 + 15, 15, Element.ALIGN_LEFT)
                            ctImp.Go()
                        End If
                        Dim startingPoint = 500
                        For c = startIndex To EndIndex
                            Try
                                'CODART
                                Dim ctart As ColumnText = New ColumnText(canvas)
                                ctart.SetSimpleColumn(New Phrase(arrayArticoli(c).cod_articolo, tablefont), 27, startingPoint, 650, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctart.Go()
                                'DESC
                                Dim ctdesc As ColumnText = New ColumnText(canvas)
                                ctdesc.SetSimpleColumn(New Phrase(arrayArticoli(c).desc_articolo, tablefont), 105, startingPoint, 325, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctdesc.Go()
                                'UM
                                Dim ctum As ColumnText = New ColumnText(canvas)
                                ctum.SetSimpleColumn(New Phrase(arrayArticoli(c).UM, tablefont), 320, startingPoint, 335, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctum.Go()
                                'QTA
                                Dim ctqta As ColumnText = New ColumnText(canvas)
                                ctqta.SetSimpleColumn(New Phrase(arrayArticoli(c).qta, tablefont), 350, startingPoint, 365, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctqta.Go()
                                'Importo
                                Dim ctImp As ColumnText = New ColumnText(canvas)
                                ctImp.SetSimpleColumn(New Phrase(arrayArticoli(c).importo, New Font(bf, 7)), 478, startingPoint, 535, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctImp.Go()
                                'Consegna
                                Dim ctCons As ColumnText = New ColumnText(canvas)
                                ctCons.SetSimpleColumn(New Phrase(arrayArticoli(c).Consegna, New Font(bf, 7)), 540, startingPoint, 675, startingPoint + 15, 15, Element.ALIGN_LEFT)
                                ctCons.Go()
                                startingPoint = startingPoint - 25
                            Catch ex As Exception

                            End Try
                        Next
                    End Using
                    count = count + 1
                    If countArt + 10 > listOfArticoli.Count Then
                        countArt = listOfArticoli.Count
                    Else
                        countArt = countArt + 10
                    End If
                    startIndex = startIndex + 10
                    If EndIndex + 10 > listOfArticoli.Count Then
                        EndIndex = listOfArticoli.Count - 1
                    Else
                        EndIndex = EndIndex + 10
                    End If
                    fs.Close()
                    reader.Close()



                End While

            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Dim document = New Document()
            Dim outFile = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\FIN_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf") ' The outPutPDF varable is passed from another sub this is the output path
            Dim writer = New PdfCopy(document, New FileStream(outFile, FileMode.Create))
            Try
                document.Open()
                For Each file As String In listPathPDF.ToArray
                    Dim reader = New PdfReader(file)
                    For i As Integer = 1 To reader.NumberOfPages
                        Dim page = writer.GetImportedPage(reader, i)
                        writer.AddPage(page)
                    Next i
                    reader.Close()
                Next
                writer.Close()
                document.Close()
            Catch ex As Exception
                db.Log.Add(New Log With {
         .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
         .Livello = TipoLogLivello.Errors,
         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
         .Messaggio = "Err invio a cliente ",
         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
         })
                db.SaveChanges()
                'catch a Exception if needed

            Finally
                path_File = outFile
                writer.Close()
                document.Close()

            End Try
            Try
                Dim count = 0
                Dim startIndex = 0
                Dim EndIndex = 12
                If EndIndex > listOfArticoli.Count - 1 Then
                    EndIndex = listOfArticoli.Count - 1
                End If
                Dim listArt = 0
                Dim countArt = 0
                Dim arrayArticoli = listOfArticoli.ToArray

                While countArt < listOfArticoli.Count
                    fornitore.listOfArticoli = listOfArticoli
                    Dim oldFile = Server.MapPath("~\Content\Template\OffertaFornitoreDDT.pdf")
                    Dim newfile = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\ddt_" + count.ToString + "_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf")
                    listPathPDFDDT.Add(newfile)
                    Dim reader As PdfReader = New PdfReader(oldFile)
                    Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1250, BaseFont.EMBEDDED)
                    Dim tablefont As Font = New Font(bf, 8)
                    Dim tablefontSmall As Font = New Font(bf, 5)
                    Dim FontColour As BaseColor = New BaseColor(128, 128, 128)
                    Dim Calibri8 As Font = FontFactory.GetFont("COURIER_OBLIQUE", 8, FontColour)
                    Dim ms As New MemoryStream
                    Dim fs As FileStream = System.IO.File.Create(newfile)
                    Dim stamper As New PdfStamper(reader, fs)
                    'stamper.InsertPage(2, reader.GetPageSize(1))
                    Using stamper
                        Dim canvas As PdfContentByte = stamper.GetOverContent(1)
                        canvas.SetColorFill(BaseColor.BLACK)
                        canvas.SetFontAndSize(bf, 35)
                        Dim ct As ColumnText = New ColumnText(canvas)
                        Dim cli As Phrase = New Phrase(fornitore.Cliente, tablefont)
                        ct.SetSimpleColumn(cli, 385, 660, 625, 715, 15, Element.ALIGN_LEFT)
                        ct.Go()
                        'preb
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Prebolla, Calibri8), 98, 734, 0)
                        'OL
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Bolla, tablefont), 36, 665, 0)
                        'DATA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Data, tablefont), 125, 665, 0)
                        'causatrasp
                        ' ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Data, tablefont), 125, 680, 0)
                        'CodFornitore
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Fornitore, tablefont), 80, 646, 0)
                        'PIVA
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.PIVA, tablefont), 80, 636, 0)
                        'cf
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Cod_Fiscale, tablefont), 285, 636, 0)
                        'doctype
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Documento_Type, tablefont), 125, 625, 0)
                        'spedizioniere
                        'consegna
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Consegna, tablefont), 75, 602, 0)
                        'clavoro
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase("Bolla di C/lavoro", tablefont), 100, 580, 0)
                        'pagamento
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Pagamento, tablefont), 100, 570, 0)
                        'oepratore
                        ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(fornitore.Operatore, tablefont), 100, 560, 0)
                        Dim startingPoint = 515
                        For c = startIndex To EndIndex
                            'CODART
                            Dim ctart As ColumnText = New ColumnText(canvas)
                            ctart.SetSimpleColumn(New Phrase(arrayArticoli(c).cod_articolo, tablefont), 25, startingPoint, 650, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctart.Go()
                            'DESC
                            Dim ctdesc As ColumnText = New ColumnText(canvas)
                            ctdesc.SetSimpleColumn(New Phrase(arrayArticoli(c).desc_articolo, tablefont), 125, startingPoint, 325, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctdesc.Go()
                            'UM
                            Dim ctImp As ColumnText = New ColumnText(canvas)
                            ctImp.SetSimpleColumn(New Phrase(arrayArticoli(c).UM, New Font(bf, 7)), 485, startingPoint, 535, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctImp.Go()
                            'QTA
                            Dim ctCons As ColumnText = New ColumnText(canvas)
                            ctCons.SetSimpleColumn(New Phrase(arrayArticoli(c).qta, New Font(bf, 7)), 540, startingPoint, 675, startingPoint + 18, 15, Element.ALIGN_LEFT)
                            ctCons.Go()
                            startingPoint = startingPoint - 15
                        Next
                    End Using
                    count = count + 1
                    startIndex = startIndex + 10

                    If countArt + 10 > listOfArticoli.Count Then
                        countArt = listOfArticoli.Count
                    Else
                        countArt = countArt + 10
                    End If

                    If EndIndex + 10 > listOfArticoli.Count Then
                        EndIndex = listOfArticoli.Count - 1
                    Else
                        EndIndex = EndIndex + 10
                    End If
                    fs.Close()
                    reader.Close()
                End While

            Catch ex As Exception
                db.Log.Add(New Log With {
                  .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
                  .Livello = TipoLogLivello.Errors,
                  .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
                  .Messaggio = "Err invio a cliente ",
                  .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
                  })
                db.SaveChanges()
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Dim documentDDT = New Document()
            Dim outFileDDT = Server.MapPath("~\Content\OfferteFornitori\" + lav.Num + "\FIN_DDT_" + lav.Num + "_" + CLng(DateTime.UtcNow.Subtract(New DateTime(1970, 1, 1)).TotalMilliseconds).ToString + ".pdf") ' The outPutPDF varable is passed from another sub this is the output path
            Dim writerDDT = New PdfCopy(documentDDT, New FileStream(outFileDDT, FileMode.Create))
            Try
                documentDDT.Open()
                For Each file As String In listPathPDFDDT.ToArray
                    Dim reader = New PdfReader(file)
                    For i As Integer = 1 To reader.NumberOfPages
                        Dim page = writerDDT.GetImportedPage(reader, i)
                        writerDDT.AddPage(page)
                    Next i
                    reader.Close()
                Next
                writer.Close()
                document.Close()
            Catch ex As Exception
                db.Log.Add(New Log With {
         .UltimaModifica = New TipoUltimaModifica With {.Data = CurrentDate, .OperatoreID = OpID, .Operatore = OpName},
         .Livello = TipoLogLivello.Errors,
         .Indirizzo = ControllerContext.RouteData.Values("controller") & "/" & ControllerContext.RouteData.Values("action"),
         .Messaggio = "Err invio a cliente ",
         .Dati = Newtonsoft.Json.JsonConvert.SerializeObject(New With {ex})
         })
                db.SaveChanges()
                'catch a Exception if needed

            Finally
                path_File_DDT = outFileDDT
                writerDDT.Close()
                documentDDT.Close()

            End Try
            Dim u = appctx.Users.Where(Function(x) x.UserName = OpName.ToString).First 'db.UserEmail.Where(Function(x) x.Uid = OC.OperatoreInsert).First
            Dim mySmtp As New SmtpClient
            Dim myMail As New MailMessage()
            Dim password = Decrypter(u.Profile.PWD_Email)
            mySmtp.UseDefaultCredentials = False
            mySmtp.Credentials = New System.Net.NetworkCredential(u.Email, password)
            mySmtp.Host = "squirtle.dnshigh.com"
            myMail = New MailMessage()
            myMail.From = New MailAddress(u.Email)
            If Not lav.FlagMailAuto Then
                myMail.To.Add(lav.email)
            Else
                myMail.To.Add(lav.email) 'Change this con quello da database
            End If

            myMail.Subject = "Lavorazione esterna - " + lav.Num
            myMail.IsBodyHtml = True
            myMail.Body = "<html> <body>Ad inviare ordine. <br>  Attendiamo i prezzi dove mancanti <br> Euroma Group </html> </body> "
            myMail.Attachments.Add(New System.Net.Mail.Attachment(path_File))
            If lav.FlagBoth Then
                myMail.Attachments.Add(New System.Net.Mail.Attachment(path_File_DDT))
            End If
            mySmtp.Send(myMail)
            db.LavorazioniEsterne.Add(New LavorazioniEsterne With {
                        .Data_Inserimento = DateTime.Now.ToString().Split(" ")(0),
                        .Id_Operatore = OpID,
                        .Operatore = OpName,
                        .OL = lav.Esecod + "_" + lav.OL + "_" + lav.Num,
                        .Path_DDT = path_File_DDT,
                        .Path_Doc = path_File
                    })
            db.SaveChanges()
            Return Json(New With {.ok = True, .message = "File inviati correttamente!"})
        End Function
        Public Shared Function Decrypter(ByVal Text As String) As String
            Try
                Dim bytesBuff As Byte() = Convert.FromBase64String(Text)
                Using aes__1 As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()
                    Dim crypto As New System.Security.Cryptography.Rfc2898DeriveBytes("11 23 91 b7 51 b5 ee b5 86 fd e9 1e 44 20 3a 2a", {&H45, &H77, &H89, &H4E, &H23, &H2D, &H45, &H44, &H86, &H55, &H84, &H95, &H36})
                    aes__1.Key = crypto.GetBytes(32)
                    aes__1.IV = crypto.GetBytes(16)
                    Using mStream As New System.IO.MemoryStream()
                        Using cStream As New System.Security.Cryptography.CryptoStream(mStream, aes__1.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
                            cStream.Write(bytesBuff, 0, bytesBuff.Length)
                            cStream.Close()
                        End Using
                        Text = Encoding.Unicode.GetString(mStream.ToArray())
                    End Using
                End Using
            Catch ex As SystemException

            End Try


            Return Text
        End Function
    End Class
End Namespace


