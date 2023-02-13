﻿Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Web.Mvc
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel

Namespace Controllers
    Public Class PezziController
        Inherits Controller

        Private db As New EuromaModels

        Private Const ConnectionString As String = "Persist Security Info=True;Password=ALNUSAD;User ID=ALNUSAD;Initial Catalog=ALNEUMA;Data Source=192.168.100.50"
        Private Const ConnectionStringBI As String = "Persist Security Info=True;Password=ALNUSAD;User ID=ALNUSAD;Initial Catalog=DWAlnus;Data Source=192.168.100.50"
        Private myConn As SqlConnection
        Private myCmd As SqlCommand
        Private myReader As SqlDataReader

        Private myConnBI As SqlConnection
        Private myCmdBI As SqlCommand
        Private myReaderBI As SqlDataReader

        Private results As String
        <Authorize(Roles:="Commerciale_Admin, Admin")>
        <HttpGet>
        Function Fatturato(dateTime As String) As FileResult
            Dim datetimeCalc = dateTime.Split("-")
            Dim ListFatture As New List(Of Fattura)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "  select ESECOD, FATSEZ, FATNRP, FATZON,FATMOV, DVSCOD,FATDREREV from FATTES00 where (FATDCOREV >= " + datetimeCalc(0) + " AND FATDCOREV <= " + datetimeCalc(1) + ") and (FATTIP = 'FA' or FATTIP = 'FV' or FATTIP = 'AF' or FATTIP = 'NA') "
                myConn.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Dim f As New Fattura With {
                        .Anno = myReader.GetString(0),
                        .codFat = myReader.GetString(1),
                        .NumFat = myReader.GetDecimal(2),
                        .Zone = myReader.GetString(3),
                        .TipoFat = myReader.GetString(4),
                        .Div = myReader.GetString(5),
                        .Mese = myReader.GetDecimal(6).ToString.Substring(4, 2)
                    }
                    ListFatture.Add(f)

                Loop
                myConn.Close()


            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Dim finalCosts As New Fatturato With {
               .Unistand = New Dictionary(Of String, Divisione),
                .ISA = New Dictionary(Of String, Divisione),
                .MPA = New Dictionary(Of String, Divisione),
                .Drill = New Dictionary(Of String, Divisione),
                .CMT = New Dictionary(Of String, Divisione),
                .RicambiERevisioni = New Dictionary(Of String, Divisione),
                .ImballiEtrasporti = New Dictionary(Of String, Divisione),
                .Altro = 0
            }
            Dim tmpF As New List(Of Fattura)
            Dim codList As New List(Of String)
            Dim totalMissing = 0
            Dim altro = 0
            Dim missingFA As New List(Of Fattura)
            Dim totalRevenue = 0
            Dim accessori = 0
            Dim listOfAccessori As New List(Of String)
            Dim listOfRevisioni As New List(Of String)
            Dim listOfAltro As New List(Of String)
            Dim revisioni = 0
            Dim dict As New Dictionary(Of String, Decimal)
            Dim anticipi As New Dictionary(Of String, Decimal)
            Dim fattureDrill As New List(Of Fattura)
            Dim contiDrill As New List(Of String)
            Dim fatture As New Fatturato_Fatture_list With {
                .Prestiti = New List(Of Fattura),
                .MPA = New List(Of Fattura),
                .Unistand = New List(Of Fattura),
                .ISA = New List(Of Fattura),
                .CMT = New List(Of Fattura),
                .DrillMatic = New List(Of Fattura),
                .Ricambi_Rev = New List(Of Fattura)
            }
            For Each F In ListFatture
                Try
                    myConn = New SqlConnection(ConnectionString)
                    myCmd = myConn.CreateCommand
                    myCmd.CommandText = "SELECT FT.PDCCOD, FT.CPRIMP, CONCAT(CONVERT(VARCHAR(50),VE.ESECOD), 'VE',CONVERT(VARCHAR(50),VE.FATNPR)), FT.FATINV FROM FATCPR00 AS FT, FATTES00 AS VE where FT.ESECOD = VE.ESECOD AND FT.FATSEZ = VE.FATSEZ AND FT.FATNRP = VE.FATNRP AND FT.ESECOD =  " + F.Anno.ToString + " AND FT.FATSEZ = '" + F.codFat.ToString + "' AND FT.FATNRP = " + F.NumFat.ToString + ""
                    myConn.Open()
                Catch ex As Exception
                    'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                End Try
                Try
                    myReader = myCmd.ExecuteReader

                    Do While myReader.Read()
                        Select Case myReader.GetString(0).ToString
                            Case "501020      "
                                If Not finalCosts.Drill.ContainsKey(F.Mese) Then
                                    finalCosts.Drill.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.Drill(F.Mese).Estero = finalCosts.Drill(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.Drill(F.Mese).Italia = finalCosts.Drill(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.Drill(F.Mese).Estero = finalCosts.Drill(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.Drill(F.Mese).Italia = finalCosts.Drill(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                contiDrill.Add(F.codFat.ToString + " - " + F.Zone.ToString + " - " + myReader.GetString(0).ToString + " - " + myReader.GetDecimal(1).ToString)
                                fatture.DrillMatic.Add(F)
                            Case "501001      "
                                If Not finalCosts.Drill.ContainsKey(F.Mese) Then
                                    finalCosts.Drill.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.Drill(F.Mese).Estero = finalCosts.Drill(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.Drill(F.Mese).Italia = finalCosts.Drill(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.Drill(F.Mese).Estero = finalCosts.Drill(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.Drill(F.Mese).Italia = finalCosts.Drill(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                contiDrill.Add(F.codFat.ToString + " - " + F.Zone.ToString + " - " + myReader.GetString(0).ToString + " - " + myReader.GetDecimal(1).ToString)
                                fatture.DrillMatic.Add(F)

                            Case "501002      "
                                If Not finalCosts.Drill.ContainsKey(F.Mese) Then
                                    finalCosts.Drill.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.Drill(F.Mese).Estero = finalCosts.Drill(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.Drill(F.Mese).Italia = finalCosts.Drill(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.Drill(F.Mese).Estero = finalCosts.Drill(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.Drill(F.Mese).Italia = finalCosts.Drill(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                contiDrill.Add(F.codFat.ToString + " - " + F.Zone.ToString + " - " + myReader.GetString(0).ToString + " - " + myReader.GetDecimal(1).ToString)
                                fatture.DrillMatic.Add(F)

                            Case "501061      "
                                If Not finalCosts.CMT.ContainsKey(F.Mese) Then
                                    finalCosts.CMT.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                fatture.CMT.Add(F)
                            Case "501021      "
                                If Not finalCosts.CMT.ContainsKey(F.Mese) Then
                                    finalCosts.CMT.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                fatture.CMT.Add(F)

                            Case "501069      "
                                If Not finalCosts.CMT.ContainsKey(F.Mese) Then
                                    finalCosts.CMT.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                fatture.CMT.Add(F)
                            Case "501063      "
                                If Not finalCosts.CMT.ContainsKey(F.Mese) Then
                                    finalCosts.CMT.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                fatture.CMT.Add(F)
                            Case "501112      " 'revisioni 
                                If Not finalCosts.CMT.ContainsKey(F.Mese) Then
                                    finalCosts.CMT.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                fatture.CMT.Add(F)
                            Case "501022      "
                                If Not finalCosts.ISA.ContainsKey(F.Mese) Then
                                    finalCosts.ISA.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.ISA(F.Mese).Estero = finalCosts.ISA(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.ISA(F.Mese).Italia = finalCosts.ISA(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.ISA(F.Mese).Estero = finalCosts.ISA(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.ISA(F.Mese).Italia = finalCosts.ISA(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                fatture.ISA.Add(F)
                            Case "501025      "
                                If Not finalCosts.MPA.ContainsKey(F.Mese) Then
                                    finalCosts.MPA.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.MPA(F.Mese).Estero = finalCosts.MPA(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.MPA(F.Mese).Italia = finalCosts.MPA(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.MPA(F.Mese).Estero = finalCosts.MPA(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.MPA(F.Mese).Italia = finalCosts.MPA(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If

                                fatture.MPA.Add(F)
                            Case "501024      "
                                If Not finalCosts.Unistand.ContainsKey(F.Mese) Then
                                    finalCosts.Unistand.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.Zone.Contains("Z") Then
                                    finalCosts.Unistand(F.Mese).Estero = finalCosts.Unistand(F.Mese).Estero + myReader.GetDecimal(1)
                                Else
                                    finalCosts.Unistand(F.Mese).Italia = finalCosts.Unistand(F.Mese).Italia + myReader.GetDecimal(1)
                                End If
                                fatture.Unistand.Add(F)
                            Case "502001      "
                                If Not finalCosts.ImballiEtrasporti.ContainsKey(F.Mese) Then
                                    finalCosts.ImballiEtrasporti.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.ImballiEtrasporti(F.Mese).Estero = finalCosts.ImballiEtrasporti(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.ImballiEtrasporti(F.Mese).Italia = finalCosts.ImballiEtrasporti(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.ImballiEtrasporti(F.Mese).Estero = finalCosts.ImballiEtrasporti(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.ImballiEtrasporti(F.Mese).Italia = finalCosts.ImballiEtrasporti(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                            Case "502002      "
                                If Not finalCosts.ImballiEtrasporti.ContainsKey(F.Mese) Then
                                    finalCosts.ImballiEtrasporti.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.ImballiEtrasporti(F.Mese).Estero = finalCosts.ImballiEtrasporti(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.ImballiEtrasporti(F.Mese).Italia = finalCosts.ImballiEtrasporti(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.ImballiEtrasporti(F.Mese).Estero = finalCosts.ImballiEtrasporti(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.ImballiEtrasporti(F.Mese).Italia = finalCosts.ImballiEtrasporti(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                            Case "501102      "
                                revisioni = revisioni + myReader.GetDecimal(1)
                                listOfRevisioni.Add(myReader.GetString(2))
                                If Not finalCosts.RicambiERevisioni.ContainsKey(F.Mese) Then
                                    finalCosts.RicambiERevisioni.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                fatture.Ricambi_Rev.Add(F)
                            Case "501010      "
                                accessori = accessori + myReader.GetDecimal(1)
                                listOfAccessori.Add(myReader.GetString(2))
                                If Not finalCosts.RicambiERevisioni.ContainsKey(F.Mese) Then
                                    finalCosts.RicambiERevisioni.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If

                                fatture.Ricambi_Rev.Add(F)
                            Case "501005      "
                                If Not finalCosts.RicambiERevisioni.ContainsKey(F.Mese) Then
                                    finalCosts.RicambiERevisioni.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If

                                fatture.Ricambi_Rev.Add(F)
                            Case "501101      " 'Altro
                                If Not finalCosts.RicambiERevisioni.ContainsKey(F.Mese) Then
                                    finalCosts.RicambiERevisioni.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If

                                fatture.Ricambi_Rev.Add(F)
                            Case "501104      " 'Altro
                                If Not finalCosts.RicambiERevisioni.ContainsKey(F.Mese) Then
                                    finalCosts.RicambiERevisioni.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                fatture.Ricambi_Rev.Add(F)
                            Case "501113      " 'Altro
                                If Not finalCosts.RicambiERevisioni.ContainsKey(F.Mese) Then
                                    finalCosts.RicambiERevisioni.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                End If
                                If F.codFat = "NA" Then
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero - myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia - myReader.GetDecimal(1)
                                    End If
                                Else
                                    If F.Zone.Contains("Z") Then
                                        finalCosts.RicambiERevisioni(F.Mese).Estero = finalCosts.RicambiERevisioni(F.Mese).Estero + myReader.GetDecimal(1)
                                    Else
                                        finalCosts.RicambiERevisioni(F.Mese).Italia = finalCosts.RicambiERevisioni(F.Mese).Italia + myReader.GetDecimal(1)
                                    End If
                                End If
                                fatture.Ricambi_Rev.Add(F)
                            Case "261004      "
                                Select Case F.Div
                                    Case "01"
                                        If Not finalCosts.Drill.ContainsKey(F.Mese) Then
                                            finalCosts.Drill.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                        End If
                                        If F.Zone.Contains("Z") Then
                                            finalCosts.Drill(F.Mese).Estero = finalCosts.Drill(F.Mese).Estero + myReader.GetDecimal(1)
                                        Else
                                            finalCosts.Drill(F.Mese).Italia = finalCosts.Drill(F.Mese).Italia + myReader.GetDecimal(1)
                                        End If
                                        fatture.DrillMatic.Add(F)
                                    Case "02"
                                        If Not finalCosts.CMT.ContainsKey(F.Mese) Then
                                            finalCosts.CMT.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                        End If
                                        If F.Zone.Contains("Z") Then
                                            finalCosts.CMT(F.Mese).Estero = finalCosts.CMT(F.Mese).Estero + myReader.GetDecimal(1)
                                        Else
                                            finalCosts.CMT(F.Mese).Italia = finalCosts.CMT(F.Mese).Italia + myReader.GetDecimal(1)
                                        End If
                                        fatture.CMT.Add(F)
                                    Case "03"
                                        If Not finalCosts.ISA.ContainsKey(F.Mese) Then
                                            finalCosts.ISA.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                        End If
                                        If F.Zone.Contains("Z") Then
                                            finalCosts.ISA(F.Mese).Estero = finalCosts.ISA(F.Mese).Estero + myReader.GetDecimal(1)
                                        Else
                                            finalCosts.ISA(F.Mese).Italia = finalCosts.ISA(F.Mese).Italia + myReader.GetDecimal(1)
                                        End If
                                        fatture.ISA.Add(F)
                                    Case "04"
                                        If Not finalCosts.Unistand.ContainsKey(F.Mese) Then
                                            finalCosts.Unistand.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                        End If
                                        If F.Zone.Contains("Z") Then
                                            finalCosts.Unistand(F.Mese).Estero = finalCosts.Unistand(F.Mese).Estero + myReader.GetDecimal(1)
                                        Else
                                            finalCosts.Unistand(F.Mese).Italia = finalCosts.Unistand(F.Mese).Italia + myReader.GetDecimal(1)
                                        End If
                                        fatture.Unistand.Add(F)
                                    Case "05"
                                        If Not finalCosts.MPA.ContainsKey(F.Mese) Then
                                            finalCosts.MPA.Add(F.Mese, New Divisione With {.Italia = 0, .Estero = 0})
                                        End If
                                        If F.Zone.Contains("Z") Then
                                            finalCosts.MPA(F.Mese).Estero = finalCosts.MPA(F.Mese).Estero + myReader.GetDecimal(1)
                                        Else
                                            finalCosts.MPA(F.Mese).Italia = finalCosts.MPA(F.Mese).Italia + myReader.GetDecimal(1)
                                        End If
                                        fatture.MPA.Add(F)
                                End Select
                            Case "261007      "
                                Dim segno = myReader.GetString(3)
                                If Not anticipi.ContainsKey(F.Mese) Then
                                    If segno = "N" Then
                                        anticipi(F.Mese) = myReader.GetDecimal(1)
                                    Else
                                        anticipi(F.Mese) = -myReader.GetDecimal(1)
                                    End If
                                Else
                                    If segno = "N" Then
                                        anticipi(F.Mese) = anticipi(F.Mese) + myReader.GetDecimal(1)
                                    Else
                                        anticipi(F.Mese) = anticipi(F.Mese) - myReader.GetDecimal(1)
                                    End If
                                End If
                                fatture.Prestiti.Add(F)

                            Case Else
                                If Not dict.ContainsKey(myReader.GetString(0).ToString) Then
                                    dict.Add(myReader.GetString(0).ToString, myReader.GetDecimal(1))
                                Else
                                    dict.Item(myReader.GetString(0).ToString) = dict.Item(myReader.GetString(0).ToString) + myReader.GetDecimal(1)
                                End If
                                tmpF.Add(F)
                                codList.Add(myReader.GetString(0).ToString)
                                totalMissing = totalMissing + myReader.GetDecimal(1)
                                finalCosts.Altro = finalCosts.Altro + myReader.GetDecimal(1)
                                missingFA.Add(F)
                                listOfAltro.Add(myReader.GetString(2))
                        End Select
                        totalRevenue = totalRevenue + myReader.GetDecimal(1)
                    Loop
                    myConn.Close()
                Catch ex As Exception
                    myConn.Close()
                    'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})

                End Try
            Next
            'Apertura file
            Dim fs As New FileStream(Server.MapPath("\Content\Template\Euroma_Fatturato.xlsx"), FileMode.Open, FileAccess.Read)
            Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)

            Dim ws As XSSFSheet = workbook.GetSheetAt(0)
            'Dim titoli As XSSFCellStyle = workbook.CreateCellStyle()
            'titoli.FillBackgroundColor = IndexedColors.DarkBlue.Index

            'Dim styleCurrency = workbook.CreateCellStyle()
            'styleCurrency.DataFormat = workbook.CreateDataFormat().GetFormat(String.Format("""{0}""#,##0.00", item.CurrencySymbol))
            'cell5.CellStyle = styleCurrency
            'Dim cont As XSSFCellStyle = workbook.CreateCellStyle()
            'cont.FillBackgroundColor = IndexedColors.LightBlue.Index
            'Base data
            Try
                ws.GetRow(1).GetCell(6).SetCellValue(datetimeCalc(0).ToString.Substring(0, 4) + "/" + datetimeCalc(0).ToString.Substring(4, 2) + "/" + datetimeCalc(0).ToString.Substring(6, 2))
                ws.GetRow(1).GetCell(9).SetCellValue(datetimeCalc(1).ToString.Substring(0, 4) + "/" + datetimeCalc(1).ToString.Substring(4, 2) + "/" + datetimeCalc(1).ToString.Substring(6, 2))
            Catch ex As Exception

            End Try
            Dim r As IRow = ws.CreateRow(2)
            For j = 0 To 26
                r.CreateCell(j)
            Next
            'Intestazione
            Dim c = 1
            Dim countKey = finalCosts.RicambiERevisioni.Keys.Count
            ws.GetRow(2).GetCell(0).SetCellValue("Mese")
            For Each k In finalCosts.RicambiERevisioni.Keys
                Try
                    Select Case k.ToString
                        Case "01"
                            ws.GetRow(2).GetCell(c).SetCellValue("01 - Gennaio")
                        Case "02"
                            ws.GetRow(2).GetCell(c).SetCellValue("02 - Febbraio")
                        Case "03"
                            ws.GetRow(2).GetCell(c).SetCellValue("03 - Marzo")
                        Case "04"
                            ws.GetRow(2).GetCell(c).SetCellValue("04 - Aprile")
                        Case "05"
                            ws.GetRow(2).GetCell(c).SetCellValue("05 - Maggio")
                        Case "06"
                            ws.GetRow(2).GetCell(c).SetCellValue("06 - Giugno")
                        Case "07"
                            ws.GetRow(2).GetCell(c).SetCellValue("07 - Luglio")
                        Case "08"
                            ws.GetRow(2).GetCell(c).SetCellValue("08 - Agosto")
                        Case "09"
                            ws.GetRow(2).GetCell(c).SetCellValue("09 - Settembre")
                        Case "10"
                            ws.GetRow(2).GetCell(c).SetCellValue("10 - Ottobre")
                        Case "11"
                            ws.GetRow(2).GetCell(c).SetCellValue("11 - Novembre")
                        Case "12"
                            ws.GetRow(2).GetCell(c).SetCellValue("12 - Dicembre")
                    End Select
                    c = c + 2
                Catch ex As Exception

                End Try

            Next
            'Start Pop
            Dim baserow As IRow = ws.GetRow(0)
            'Dim baserow As IRow = ws.GetRow(2)
            Dim ms As New MemoryStream
            Dim ms1 As New MemoryStream
            Dim ms2 As New MemoryStream
            'Riga Intestazione
            Try
                Dim i As Integer = 1
                'Drillmatic
                i = 1
                For i = 1 To countKey * 2
                    Try
                        If finalCosts.Drill.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                            Try
                                ws.GetRow(4).GetCell(i).SetCellValue(finalCosts.Drill(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia)
                                i = i + 1
                                ws.GetRow(4).GetCell(i).SetCellValue(finalCosts.Drill(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero)
                            Catch ex As Exception

                            End Try
                        Else
                            i = i + 1
                        End If
                    Catch ex As Exception

                    End Try

                Next
                'Isa
                i = 1
                For i = 1 To countKey * 2
                    Try
                        If finalCosts.ISA.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                            Try
                                ws.GetRow(5).GetCell(i).SetCellValue(finalCosts.ISA(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia)
                                i = i + 1
                                ws.GetRow(5).GetCell(i).SetCellValue(finalCosts.ISA(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero)
                            Catch ex As Exception

                            End Try
                        Else
                            i = i + 1
                        End If
                    Catch ex As Exception

                    End Try

                Next

                'CMT
                i = 1
                For i = 1 To countKey * 2
                    Try
                        If finalCosts.CMT.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                            Try
                                ws.GetRow(6).GetCell(i).SetCellValue(finalCosts.CMT(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia)
                                i = i + 1
                                ws.GetRow(6).GetCell(i).SetCellValue(finalCosts.CMT(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero)
                            Catch ex As Exception

                            End Try
                        Else
                            i = i + 1
                        End If
                    Catch ex As Exception

                    End Try

                Next
                'Unistand
                i = 1
                For i = 1 To countKey * 2
                    Try
                        If finalCosts.Unistand.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                            Try
                                ws.GetRow(7).GetCell(i).SetCellValue(finalCosts.Unistand(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia)
                                i = i + 1
                                ws.GetRow(7).GetCell(i).SetCellValue(finalCosts.Unistand(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero)
                            Catch ex As Exception

                            End Try
                        Else
                            i = i + 1
                        End If
                    Catch ex As Exception

                    End Try

                Next

                'MPA
                i = 1
                For i = 1 To countKey * 2
                    Try
                        If finalCosts.MPA.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                            Try
                                ws.GetRow(8).GetCell(i).SetCellValue(finalCosts.MPA(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia)
                                i = i + 1
                                ws.GetRow(8).GetCell(i).SetCellValue(finalCosts.MPA(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero)
                            Catch ex As Exception

                            End Try
                        Else
                            i = i + 1
                        End If
                    Catch ex As Exception

                    End Try

                Next
                'Ricambi
                i = 1
                For Each k In finalCosts.RicambiERevisioni.Keys
                    Try
                        If k = GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0) Then
                            Try
                                ws.GetRow(9).GetCell(i).SetCellValue(finalCosts.RicambiERevisioni(k).Italia)
                                i = i + 1
                                ws.GetRow(9).GetCell(i).SetCellValue(finalCosts.RicambiERevisioni(k).Estero)
                                i = i + 1
                            Catch ex As Exception

                            End Try
                        End If
                    Catch ex As Exception

                    End Try

                Next
                'Ricambi
                i = 1
                For Each k In finalCosts.ImballiEtrasporti.Keys
                    Try
                        If k = GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0) Then
                            Try
                                ws.GetRow(10).GetCell(i).SetCellValue(finalCosts.ImballiEtrasporti(k).Italia)
                                i = i + 1
                                ws.GetRow(10).GetCell(i).SetCellValue(finalCosts.ImballiEtrasporti(k).Estero)
                                i = i + 1
                            Catch ex As Exception

                            End Try
                        End If
                    Catch ex As Exception

                    End Try

                Next
                Dim counterAnticipi = 1
                Try
                    For Each k In anticipi
                        ws.GetRow(11).GetCell(counterAnticipi).SetCellValue(k.Value)
                        counterAnticipi = counterAnticipi + 2
                    Next
                Catch ex As Exception

                End Try


                ws.GetRow(42).GetCell(1).SetCellValue(finalCosts.Altro)

                'Evaluation totale
            Catch ex As Exception

            End Try
            workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll()
            workbook.Write(ms1)
            'ms1 = ms
            Return File(ms1.ToArray, "application/vnd.ms-excel", "FATTURATO_BRAND_" & dateTime.ToString & ".xlsx")

            'Return Json(New With {.tot = finalCosts, .anticipi = anticipi, .miss = totalMissing, .totRev = totalRevenue}, JsonRequestBehavior.AllowGet) '.tmpf = tmpF, .codList = codList, .tmp = dict,, .FD = fattureDrill, .CD = contiDrill
        End Function
        <Authorize(Roles:="Commerciale_Admin, Admin, Produzione")>
        <HttpGet>
        Function FatturatoFornitori(datetime As String) As FileResult
            Dim datetimeCalc = datetime.Split("-")
            Dim m1 = datetimeCalc(0).ToString.Substring(4, 2)
            Dim m2 = datetimeCalc(1).ToString.Substring(4, 2)
            Dim listSpM As New List(Of SpeseMensili)
            Dim listPNVarie As New List(Of PrimeNote)
            Dim listPN As New List(Of PrimeNote)
            Dim ff As New List(Of FatturatoFornitoriViewModel)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "select 
                                        PNCOCF + ' - '+ CLFNMG as CodCliFor,
                                        SUM(IVIMBE) as Importo,
										SUBSTRING(cast(PNDTDOREV as nvarchar), 5, 2)
                                        from CGMPNO00,
                                        CLFANA,
										CGMIVA00
										where PNDTDOREV >= '" + datetimeCalc(0) + "'
                                        AND PNDTDOREV <= '" + datetimeCalc(1) + "'
                                        And PNSNUM = 'FP'
										AND  PNCLFO = 'F'
                                        AND (PNCCAU = 'FF1' or PNCCAU = 'FF2')
										AND CLFTIP = 'F'
										AND CLFCO1 = PNCOCF
										and IVESER = PNESEC
										and IVSNUM = PNSNUM
										AND IVNRRE = PNNRRE
									group by PNCOCF, CLFNMG,SUBSTRING(cast(PNDTDOREV as nvarchar), 5, 2)
"
                myConn.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Dim fornitore = myReader.GetString(0)

                    If ff.Where(Function(x) x.Fornitore = fornitore).Count = 0 Then
                        ff.Add(New FatturatoFornitoriViewModel With {
                        .Fornitore = myReader.GetString(0),
                        .TypeFornitore = "S",
                        .Fatturato = New MesiFatturatoViewModel With {
                            .Fatturato_Gennaio = 0,
                            .Fatturato_Febbraio = 0,
                            .Fatturato_Marzo = 0,
                            .Fatturato_Aprile = 0,
                            .Fatturato_Maggio = 0,
                            .Fatturato_Giugno = 0,
                            .Fatturato_Luglio = 0,
                            .Fatturato_Agosto = 0,
                            .Fatturato_Settembre = 0,
                            .Fatturato_Ottobre = 0,
                            .Fatturato_Novembre = 0,
                            .Fatturato_Dicembre = 0
                        }
                    })
                    End If
                    Dim f = ff.Where(Function(x) x.Fornitore = myReader.GetString(0)).First
                        Select Case myReader.GetString(2)
                            Case "01"
                                f.Fatturato.Fatturato_Gennaio = f.Fatturato.Fatturato_Gennaio + myReader.GetDecimal(1)
                            Case "02"
                                f.Fatturato.Fatturato_Febbraio = f.Fatturato.Fatturato_Febbraio + myReader.GetDecimal(1)
                            Case "03"
                                f.Fatturato.Fatturato_Marzo = f.Fatturato.Fatturato_Marzo + myReader.GetDecimal(1)
                            Case "04"
                                f.Fatturato.Fatturato_Aprile = f.Fatturato.Fatturato_Aprile + myReader.GetDecimal(1)
                            Case "05"
                                f.Fatturato.Fatturato_Maggio = f.Fatturato.Fatturato_Maggio + myReader.GetDecimal(1)
                            Case "06"
                                f.Fatturato.Fatturato_Giugno = f.Fatturato.Fatturato_Giugno + myReader.GetDecimal(1)
                            Case "07"
                                f.Fatturato.Fatturato_Luglio = f.Fatturato.Fatturato_Luglio + myReader.GetDecimal(1)
                            Case "08"
                                f.Fatturato.Fatturato_Agosto = f.Fatturato.Fatturato_Agosto + myReader.GetDecimal(1)
                            Case "09"
                                f.Fatturato.Fatturato_Settembre = f.Fatturato.Fatturato_Settembre + myReader.GetDecimal(1)
                            Case "10"
                                f.Fatturato.Fatturato_Ottobre = f.Fatturato.Fatturato_Ottobre + myReader.GetDecimal(1)
                            Case "11"
                                f.Fatturato.Fatturato_Novembre = f.Fatturato.Fatturato_Novembre + myReader.GetDecimal(1)
                            Case "12"
                                f.Fatturato.Fatturato_Dicembre = f.Fatturato.Fatturato_Dicembre + myReader.GetDecimal(1)
                        End Select

                    '.DataCompetenza = Convert.ToDateTime(myReader.GetDecimal(3).ToString),
                Loop
                myConn.Close()

            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try

            Dim fs As New FileStream(Server.MapPath("\Content\Template\Supplier_Turnover_Template.xlsx"), FileMode.Open, FileAccess.ReadWrite)
            Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
            Dim ws As XSSFSheet = workbook.GetSheetAt(1)
            Dim fontIntestazione As XSSFFont = CType(workbook.CreateFont(), XSSFFont)
            fontIntestazione.FontHeightInPoints = CShort(12)
            fontIntestazione.FontName = "Arial"
            fontIntestazione.IsBold = True
            fontIntestazione.IsItalic = False
            fontIntestazione.FontHeightInPoints = 11
            Dim styleIntestazione As XSSFCellStyle = CType(workbook.CreateCellStyle(), XSSFCellStyle)
            styleIntestazione.FillBackgroundColor = FillPattern.SolidForeground
            styleIntestazione.SetFont(fontIntestazione)
            Dim nomefile = "Periodo_" + datetimeCalc(0) + "-" + datetimeCalc(1) + "_Supplier_Turnover.xlsx"
            'For p As Integer = 0 To 12

            'Base data
            Dim ms As New MemoryStream
            Dim ms1 As New MemoryStream
            Dim counter = 2
            Dim wsCli As XSSFSheet = workbook.GetSheetAt(1)
            Try
                wsCli.GetRow(1).GetCell(1).SetCellValue("Fatturato Fornitori dal '" + datetimeCalc(0) + "' al '" + datetimeCalc(1) + "'")
                'Date 
                wsCli.GetRow(2).GetCell(3).SetCellValue("01")
                wsCli.GetRow(2).GetCell(4).SetCellValue("02")
                wsCli.GetRow(2).GetCell(5).SetCellValue("03")
                wsCli.GetRow(2).GetCell(6).SetCellValue("04")
                wsCli.GetRow(2).GetCell(6).SetCellValue("05")
                wsCli.GetRow(2).GetCell(8).SetCellValue("06")
                wsCli.GetRow(2).GetCell(9).SetCellValue("07")
                wsCli.GetRow(2).GetCell(10).SetCellValue("08")
                wsCli.GetRow(2).GetCell(11).SetCellValue("09")
                wsCli.GetRow(2).GetCell(12).SetCellValue("10")
                wsCli.GetRow(2).GetCell(13).SetCellValue("11")
                wsCli.GetRow(2).GetCell(14).SetCellValue("12")
                Dim i = 3
                For Each f In ff
                    Try
                        wsCli.GetRow(i).GetCell(2).SetCellValue(f.Fornitore)
                        wsCli.GetRow(i).GetCell(3).SetCellValue(f.Fatturato.Fatturato_Gennaio)
                        wsCli.GetRow(i).GetCell(4).SetCellValue(f.Fatturato.Fatturato_Febbraio)
                        wsCli.GetRow(i).GetCell(5).SetCellValue(f.Fatturato.Fatturato_Marzo)
                        wsCli.GetRow(i).GetCell(6).SetCellValue(f.Fatturato.Fatturato_Aprile)
                        wsCli.GetRow(i).GetCell(7).SetCellValue(f.Fatturato.Fatturato_Maggio)
                        wsCli.GetRow(i).GetCell(8).SetCellValue(f.Fatturato.Fatturato_Giugno)
                        wsCli.GetRow(i).GetCell(9).SetCellValue(f.Fatturato.Fatturato_Luglio)
                        wsCli.GetRow(i).GetCell(10).SetCellValue(f.Fatturato.Fatturato_Agosto)
                        wsCli.GetRow(i).GetCell(11).SetCellValue(f.Fatturato.Fatturato_Settembre)
                        wsCli.GetRow(i).GetCell(12).SetCellValue(f.Fatturato.Fatturato_Ottobre)
                        wsCli.GetRow(i).GetCell(13).SetCellValue(f.Fatturato.Fatturato_Novembre)
                        wsCli.GetRow(i).GetCell(14).SetCellValue(f.Fatturato.Fatturato_Dicembre)
                        i = i + 1
                    Catch ex As Exception

                    End Try
                Next

            Catch ex As Exception

            End Try

            workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll()
            workbook.Write(ms1)
            Return File(ms1.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomefile)

        End Function

        <Authorize(Roles:="Commerciale_Admin, Admin")>
        <HttpGet>
        Function PrimaNota(datetime As String) As FileResult
            Dim datetimeCalc = datetime.Split("-")
            Dim m1 = datetimeCalc(0).ToString.Substring(4, 2)
            Dim m2 = datetimeCalc(1).ToString.Substring(4, 2)
            Dim listSpM As New List(Of SpeseMensili)
            Dim listPNVarie As New List(Of PrimeNote)
            If m1 = m2 Then
                Dim listPN As New List(Of PrimeNote)
                Dim SpM As New SpeseMensili With {
                    .OneriBanc = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Bonifici = New CostiPN With {.Avere = 0, .Dare = 0},
                    .SBF = New CostiPN With {.Avere = 0, .Dare = 0},
                    .AF = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Finanziamento = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Affitto = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Assicurazioni = New CostiPN With {.Avere = 0, .Dare = 0},
                    .CartaCredito = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Finanziamenti = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Fornitori = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Insoluti = New CostiPN With {.Avere = 0, .Dare = 0},
                    .INAIL = New CostiPN With {.Avere = 0, .Dare = 0},
                    .INPS = New CostiPN With {.Avere = 0, .Dare = 0},
                    .IRPEF = New CostiPN With {.Avere = 0, .Dare = 0},
                    .IVA = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Leasing = New CostiPN With {.Avere = 0, .Dare = 0},
                    .RA = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Stipendi = New CostiPN With {.Avere = 0, .Dare = 0},
                    .EntiDIP = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Varie = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Tasse = New CostiPN With {.Avere = 0, .Dare = 0},
                    .IntPass = New CostiPN With {.Avere = 0, .Dare = 0},
                    .EntiDipendenti = New CostiPN With {.Avere = 0, .Dare = 0},
                    .Mese = Convert.ToInt32(m1)
                }
                Try
                    myConn = New SqlConnection(ConnectionString)
                    myCmd = myConn.CreateCommand
                    myCmd.CommandText = "select PNESER as Esercizio, 
                                        PNSNUM As PrimaNota, 
                                        PNNRRE as Numero_PN, 
                                        PNDTCOREV as DataCompetenza, 
                                        PNIMOE as Importo, 
                                        PNCON1 as Conto, 
                                        PNCCAU as Causale,
                                        PNSEGN as Segno,
                                        PNCLFO as FlagCF,
                                        PNCOCF as CodCliFor
                                        from CGMPNO00
                                        where PNDTREREV >= '" + datetimeCalc(0) + "'
                                        AND PNDTREREV <= '" + datetimeCalc(1) + "'
                                        And PNSNUM = 'PN'
                                        AND (PNCCAU = 'ACC' OR PNCCAU = 'ADD' OR PNCCAU = 'PAG' OR PNCCAU = 'GC' OR PNCCAU = 'INS'OR PNCCAU = 'INC' OR PNCCAU = 'RIC' OR PNCCAU = 'DEF' OR PNCCAU = 'ANT' OR PNCCAU = 'EST')
                                    "
                    myConn.Open()
                Catch ex As Exception
                    'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                End Try
                Try
                    myReader = myCmd.ExecuteReader

                    Do While myReader.Read()
                        listPN.Add(New EuromaWeb.PrimeNote With {
                            .Anno = myReader.GetString(0),
                            .CodPN = myReader.GetString(1),
                            .NumPN = myReader.GetDecimal(2),
                            .Importo = myReader.GetDecimal(4),
                            .Conto = myReader.GetString(5),
                            .Causale = myReader.GetString(6),
                            .Segno = myReader.GetString(7),
                            .FlagCF = myReader.GetString(8),
                            .CodCLIFOR = myReader.GetString(9)
                        })

                        '.DataCompetenza = Convert.ToDateTime(myReader.GetDecimal(3).ToString),
                    Loop
                    myConn.Close()

                Catch ex As Exception
                    'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                End Try
                Dim sum = 0
                'Calcolo  PrimeNote
                For Each p In listPN
                    Select Case p.Conto.Replace(" ", "")
                        Case “122000” 'Incassi ->Qui ci sono gli insoluti che sono “INS” e “RIC”
                            sum = sum + p.Importo
                            If p.FlagCF = "C" Then
                                Select Case p.Causale
                                    Case "INS"
                                        SpM.Insoluti.Dare = SpM.Insoluti.Dare + p.Importo
                                    Case "RIC"
                                        SpM.Insoluti.Dare = SpM.Insoluti.Dare + p.Importo
                                    Case "INC"
                                        SpM.Bonifici.Avere = SpM.Bonifici.Avere + p.Importo
                                    Case Else
                                        listPNVarie.Add(p)
                                        SpM.Bonifici.Avere = SpM.Bonifici.Avere + p.Importo
                                End Select
                            Else
                                listPNVarie.Add(p)
                                SpM.Bonifici.Avere = SpM.Bonifici.Avere + p.Importo
                            End If
                        Case "122011", "122012", "122013", "122014", "122015", "122016", "122017", "122018", "122019", "122020"
                            If p.Causale = "DEF" Then
                                SpM.SBF.Avere = SpM.SBF.Avere + p.Importo
                            End If
                        Case “251311”, “251312”, “251313”, “251314”, “251315”, “251316”, “251317”, “251318”, “251319”, “251320”, “251321”, “251322”, “251323”, “251324”, “251325”, “251326”, “251327”, “251328” ''ANT EST
                            If p.Segno = "D" Then
                                SpM.AF.Avere = SpM.AF.Avere - p.Importo
                            Else
                                SpM.AF.Avere = SpM.AF.Avere + p.Importo
                            End If
                        Case “251330”, “251331”, “251332”, “251333”, “251334”, “251335”, “251336”, “251337”, “251338”, “251339”, “251340”, “251341”, “251342”, “251343”, “251344”, “251345”, “251346”, “251347”, “251348”, “251349”, “251350”, “251351”, “251352”, “251353”, “251354”, “251355”, “251356”, “251357”, “251358”, “251359”, “251360”, “251361”, “251362”, “251363”  'Finanziamenti
                            If p.Causale = "ACC" Then
                                SpM.Finanziamenti.Avere = SpM.Finanziamenti.Avere + p.Importo
                            Else
                                SpM.Finanziamenti.Dare = SpM.Finanziamenti.Dare + p.Importo
                            End If
                        Case “512505” 'Assicurazioni
                            SpM.Assicurazioni.Dare = SpM.Assicurazioni.Dare + p.Importo
                        Case “251501” 'Carte
                            If p.Causale = "ADD" Then
                                SpM.CartaCredito.Dare = SpM.CartaCredito.Dare + p.Importo
                            Else
                                SpM.Varie.Dare = SpM.Varie.Dare + p.Importo
                            End If
                        Case “271000” 'Fornitori -> Qui ci sono due fornitori 001995 e 003327 che vanno in “Fornitori dell’affitto”, mentre ci sono i fornitori 002696, 000108, 003369 che vanno in “Fornitori Leasing”
                            Select Case p.CodCLIFOR
                                Case "001995", "003327"
                                    SpM.Affitto.Dare = SpM.Affitto.Dare + p.Importo
                                Case "002696", "000108", "003369"
                                    SpM.Leasing.Dare = SpM.Leasing.Dare = p.Importo
                                Case Else
                                    SpM.Fornitori.Dare = SpM.Fornitori.Dare + p.Importo
                            End Select
                        Case “303002” 'INAIL
                            If p.Causale = "PAG" Then
                                SpM.INAIL.Dare = SpM.INAIL.Dare + p.Importo
                            Else
                                SpM.Varie.Dare = SpM.Varie.Dare + p.Importo
                            End If
                        Case “303001” 'INPS
                            If p.Causale = "PAG" Then
                                SpM.INPS.Dare = SpM.INPS.Dare + p.Importo
                            Else
                                SpM.Varie.Dare = SpM.Varie.Dare + p.Importo
                            End If
                        Case “301010”, “301090” 'IRPEF
                            If p.Causale = "PAG" Then
                                SpM.IRPEF.Dare = SpM.IRPEF.Dare + p.Importo
                            Else
                                SpM.Varie.Dare = SpM.Varie.Dare + p.Importo
                            End If
                        Case “301021” 'IVA
                            SpM.IVA.Dare = SpM.IVA.Dare + p.Importo
                        Case “301011” 'Ritenuta d’acconto
                            SpM.RA.Dare = SpM.RA.Dare + p.Importo
                        Case “302001”, “302002”, “302030” 'Stipendi
                            SpM.Stipendi.Dare = SpM.Stipendi.Dare + p.Importo
                        Case “303013”, “303006”, “303004”, “303016”, “303005”, “303014”, “303003”, “512306”, "302022", "303007", "301012" 'Enti Dipendenti
                            SpM.EntiDipendenti.Dare = SpM.EntiDipendenti.Dare + p.Importo
                        Case “301023”, “301022” 'Tasse
                            SpM.Tasse.Dare = SpM.Tasse.Dare + p.Importo
                        Case “523001”, “523007”, "523002", "523003" 'Interessi Passivi
                            SpM.IntPass.Dare = SpM.IntPass.Dare + p.Importo
                        Case “523004”, “523005” 'Oneri Bancari
                            SpM.OneriBanc.Dare = SpM.OneriBanc.Dare + p.Importo
                        Case "506001", "506002", "506003", "506004", "506005", "506006"
                            SpM.Varie.Dare = SpM.Varie.Dare + p.Importo
                        Case "122010", "311001"
                        Case Else
                            If p.Causale = "ACC" Then
                                listPNVarie.Add(p)
                            End If
                    End Select
                Next
                listSpM.Add(SpM)
            Else
                Dim mese1 = Convert.ToInt32(m1)
                Dim mese2 = Convert.ToInt32(m2)
                For i = mese1 To mese2
                    Dim listPN As New List(Of PrimeNote)
                    Dim SpM As New SpeseMensili With {
                        .OneriBanc = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Bonifici = New CostiPN With {.Avere = 0, .Dare = 0},
                        .SBF = New CostiPN With {.Avere = 0, .Dare = 0},
                        .AF = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Finanziamento = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Affitto = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Assicurazioni = New CostiPN With {.Avere = 0, .Dare = 0},
                        .CartaCredito = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Finanziamenti = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Fornitori = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Insoluti = New CostiPN With {.Avere = 0, .Dare = 0},
                        .INAIL = New CostiPN With {.Avere = 0, .Dare = 0},
                        .INPS = New CostiPN With {.Avere = 0, .Dare = 0},
                        .IRPEF = New CostiPN With {.Avere = 0, .Dare = 0},
                        .IVA = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Leasing = New CostiPN With {.Avere = 0, .Dare = 0},
                        .RA = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Stipendi = New CostiPN With {.Avere = 0, .Dare = 0},
                        .EntiDIP = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Varie = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Tasse = New CostiPN With {.Avere = 0, .Dare = 0},
                        .IntPass = New CostiPN With {.Avere = 0, .Dare = 0},
                        .EntiDipendenti = New CostiPN With {.Avere = 0, .Dare = 0},
                        .Mese = i
                    }
                    Try
                        Dim mese = i.ToString
                        If mese.Length = 1 Then
                            mese = "0" + mese
                        End If
                        myConn = New SqlConnection(ConnectionString)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "select PNESER as Esercizio, 
                                        PNSNUM As PrimaNota, 
                                        PNNRRE as Numero_PN, 
                                        PNDTCOREV as DataCompetenza, 
                                        PNIMOE as Importo, 
                                        PNCON1 as Conto, 
                                        PNCCAU as Causale,
                                        PNSEGN as Segno,
                                        PNCLFO as FlagCF,
                                        PNCOCF as CodCliFor
                                        from CGMPNO00
                                        where PNDTREREV like '" + datetimeCalc(0).Substring(0, 4) + mese + "%'
                                        And PNSNUM = 'PN'
                                        AND (PNCCAU = 'ACC' OR PNCCAU = 'ADD' OR PNCCAU = 'PAG' OR PNCCAU = 'GC' OR PNCCAU = 'INS'OR PNCCAU = 'INC' OR PNCCAU = 'RIC' OR PNCCAU = 'DEF' OR PNCCAU = 'ANT' OR PNCCAU = 'EST')
                                    "
                        myConn.Open()
                    Catch ex As Exception
                        'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader

                        Do While myReader.Read()
                            listPN.Add(New EuromaWeb.PrimeNote With {
                                .Anno = myReader.GetString(0),
                                .CodPN = myReader.GetString(1),
                                .NumPN = myReader.GetDecimal(2),
                                .Importo = myReader.GetDecimal(4),
                                .Conto = myReader.GetString(5),
                                .Causale = myReader.GetString(6),
                                .Segno = myReader.GetString(7),
                                .FlagCF = myReader.GetString(8),
                                .CodCLIFOR = myReader.GetString(9)
                            })

                            '.DataCompetenza = Convert.ToDateTime(myReader.GetDecimal(3).ToString),
                        Loop
                        myConn.Close()

                    Catch ex As Exception
                        'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                    End Try
                    Dim sum = 0
                    'Calcolo  PrimeNote
                    For Each p In listPN
                        Select Case p.Conto.Replace(" ", "")
                            Case “122000” 'Incassi ->Qui ci sono gli insoluti che sono “INS” e “RIC”
                                sum = sum + p.Importo
                                If p.FlagCF = "C" Then
                                    Select Case p.Causale
                                        Case "INS"
                                            SpM.Insoluti.Dare = SpM.Insoluti.Dare + p.Importo
                                        Case "RIC"
                                            SpM.Insoluti.Dare = SpM.Insoluti.Dare + p.Importo
                                        Case "INC"
                                            SpM.Bonifici.Avere = SpM.Bonifici.Avere + p.Importo
                                        Case Else
                                            listPNVarie.Add(p)
                                            SpM.Bonifici.Avere = SpM.Bonifici.Avere + p.Importo
                                    End Select
                                Else
                                    listPNVarie.Add(p)
                                    SpM.Bonifici.Avere = SpM.Bonifici.Avere + p.Importo
                                End If
                            Case "122011", "122012", "122013", "122014", "122015", "122016", "122017", "122018", "122019", "122020"
                                If p.Causale = "DEF" Then
                                    SpM.SBF.Avere = SpM.SBF.Avere + p.Importo
                                End If
                            Case “251311”, “251312”, “251313”, “251314”, “251315”, “251316”, “251317”, “251318”, “251319”, “251320”, “251321”, “251322”, “251323”, “251324”, “251325”, “251326”, “251327”, “251328” ''ANT EST
                                If p.Segno = "D" Then
                                    SpM.AF.Avere = SpM.AF.Avere - p.Importo
                                Else
                                    SpM.AF.Avere = SpM.AF.Avere + p.Importo
                                End If
                            Case “251330”, “251331”, “251332”, “251333”, “251334”, “251335”, “251336”, “251337”, “251338”, “251339”, “251340”, “251341”, “251342”, “251343”, “251344”, “251345”, “251346”, “251347”, “251348”, “251349”, “251350”, “251351”, “251352”, “251353”, “251354”, “251355”, “251356”, “251357”, “251358”, “251359”, “251360”, “251361”, “251362”, “251363”  'Finanziamenti
                                If p.Causale = "ACC" Then
                                    SpM.Finanziamenti.Avere = SpM.Finanziamenti.Avere + p.Importo
                                Else
                                    SpM.Finanziamenti.Dare = SpM.Finanziamenti.Dare + p.Importo
                                End If
                            Case “512505” 'Assicurazioni
                                SpM.Assicurazioni.Dare = SpM.Assicurazioni.Dare + p.Importo
                            Case “251501” 'Carte
                                If p.Causale = "ADD" Then
                                    SpM.CartaCredito.Dare = SpM.CartaCredito.Dare + p.Importo
                                Else
                                    SpM.Varie.Dare = SpM.Varie.Dare + p.Importo
                                End If
                            Case “271000” 'Fornitori -> Qui ci sono due fornitori 001995 e 003327 che vanno in “Fornitori dell’affitto”, mentre ci sono i fornitori 002696, 000108, 003369 che vanno in “Fornitori Leasing”
                                Select Case p.CodCLIFOR
                                    Case "001995", "003327"
                                        SpM.Affitto.Dare = SpM.Affitto.Dare + p.Importo
                                    Case "002696", "000108", "003369"
                                        SpM.Leasing.Dare = SpM.Leasing.Dare = p.Importo
                                    Case Else
                                        SpM.Fornitori.Dare = SpM.Fornitori.Dare + p.Importo
                                End Select
                            Case “303002” 'INAIL
                                If p.Causale = "PAG" Then
                                    SpM.INAIL.Dare = SpM.INAIL.Dare + p.Importo
                                Else
                                    SpM.Varie.Dare = SpM.Varie.Dare + p.Importo
                                End If
                            Case “303001” 'INPS
                                If p.Causale = "PAG" Then
                                    SpM.INPS.Dare = SpM.INPS.Dare + p.Importo
                                Else
                                    SpM.Varie.Dare = SpM.Varie.Dare + p.Importo
                                End If
                            Case “301010”, “301090” 'IRPEF
                                If p.Causale = "PAG" Then
                                    SpM.IRPEF.Dare = SpM.IRPEF.Dare + p.Importo
                                Else
                                    SpM.Varie.Dare = SpM.Varie.Dare + p.Importo
                                End If
                            Case “301021” 'IVA
                                SpM.IVA.Dare = SpM.IVA.Dare + p.Importo
                            Case “301011” 'Ritenuta d’acconto
                                SpM.RA.Dare = SpM.RA.Dare + p.Importo
                            Case “302001”, “302002”, “302030” 'Stipendi
                                SpM.Stipendi.Dare = SpM.Stipendi.Dare + p.Importo
                            Case “303013”, “303006”, “303004”, “303016”, “303005”, “303014”, “303003”, “512306”, "302022", "303007", "301012" 'Enti Dipendenti
                                SpM.EntiDipendenti.Dare = SpM.EntiDipendenti.Dare + p.Importo
                            Case “301023”, “301022” 'Tasse
                                SpM.Tasse.Dare = SpM.Tasse.Dare + p.Importo
                            Case “523001”, “523007”, "523002", "523003" 'Interessi Passivi
                                SpM.IntPass.Dare = SpM.IntPass.Dare + p.Importo
                            Case “523004”, “523005” 'Oneri Bancari
                                SpM.OneriBanc.Dare = SpM.OneriBanc.Dare + p.Importo
                            Case "506001", "506002", "506003", "506004", "506005", "506006"
                                SpM.Varie.Dare = SpM.Varie.Dare + p.Importo
                            Case "122010", "311001"
                            Case Else
                                If p.Causale = "ACC" Then
                                    listPNVarie.Add(p)
                                End If
                        End Select
                    Next
                    listSpM.Add(SpM)
                Next
            End If



            Dim fs As New FileStream(Server.MapPath("\Content\Template\PrimeNote.xlsx"), FileMode.Open, FileAccess.ReadWrite)
            Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
            Dim ws As XSSFSheet = workbook.GetSheetAt(0)
            Dim fontIntestazione As XSSFFont = CType(workbook.CreateFont(), XSSFFont)
            fontIntestazione.FontHeightInPoints = CShort(12)
            fontIntestazione.FontName = "Arial"
            fontIntestazione.IsBold = True
            fontIntestazione.IsItalic = False
            fontIntestazione.FontHeightInPoints = 11
            Dim styleIntestazione As XSSFCellStyle = CType(workbook.CreateCellStyle(), XSSFCellStyle)
            styleIntestazione.FillBackgroundColor = FillPattern.SolidForeground
            styleIntestazione.SetFont(fontIntestazione)
            Dim nomefile = "CostiEuroma_Periodo_" + datetimeCalc(0) + "_" + datetimeCalc(1) + ".xlsx"
            'For p As Integer = 0 To 12

            'Base data
            Dim ms As New MemoryStream
            Dim ms1 As New MemoryStream
            Dim counter = 2
            Dim wsCli As XSSFSheet = workbook.GetSheetAt(0)
            Try
                'Date 
                wsCli.GetRow(5).GetCell(4).SetCellValue(datetimeCalc(0).Substring(6, 2) + "/" + datetimeCalc(0).Substring(4, 2) + "/" + datetimeCalc(0).Substring(0, 4))
                wsCli.GetRow(6).GetCell(4).SetCellValue(datetimeCalc(1).Substring(6, 2) + "/" + datetimeCalc(1).Substring(4, 2) + "/" + datetimeCalc(1).Substring(0, 4))

                For i = 1 To 12
                    If listSpM.Where(Function(x) x.Mese = i).Count > 0 Then
                        Dim SpMOutPut = listSpM.Where(Function(x) x.Mese = i).First
                        Dim colDare = 8 + ((i - 1) * 4) + 1
                        Dim colAvere = 8 + ((i - 1) * 4) + 2
                        'Bonifici
                        wsCli.GetRow(4).GetCell(colDare).SetCellValue(SpMOutPut.Bonifici.Dare)
                        wsCli.GetRow(4).GetCell(colAvere).SetCellValue(SpMOutPut.Bonifici.Avere)

                        'SBF
                        wsCli.GetRow(5).GetCell(colDare).SetCellValue(SpMOutPut.SBF.Dare)
                        wsCli.GetRow(5).GetCell(colAvere).SetCellValue(SpMOutPut.SBF.Avere)

                        'AF
                        wsCli.GetRow(6).GetCell(colDare).SetCellValue(SpMOutPut.AF.Dare)
                        wsCli.GetRow(6).GetCell(colAvere).SetCellValue(SpMOutPut.AF.Avere)

                        'Fin
                        wsCli.GetRow(7).GetCell(colDare).SetCellValue(SpMOutPut.Finanziamento.Dare)
                        wsCli.GetRow(7).GetCell(colAvere).SetCellValue(SpMOutPut.Finanziamento.Avere)

                        'Affitto
                        wsCli.GetRow(8).GetCell(colDare).SetCellValue(SpMOutPut.Affitto.Dare)
                        wsCli.GetRow(8).GetCell(colAvere).SetCellValue(SpMOutPut.Affitto.Avere)

                        'Ass
                        wsCli.GetRow(9).GetCell(colDare).SetCellValue(SpMOutPut.Assicurazioni.Dare)
                        wsCli.GetRow(9).GetCell(colAvere).SetCellValue(SpMOutPut.Assicurazioni.Avere)

                        'Carta
                        wsCli.GetRow(10).GetCell(colDare).SetCellValue(SpMOutPut.CartaCredito.Dare)
                        wsCli.GetRow(10).GetCell(colAvere).SetCellValue(SpMOutPut.CartaCredito.Avere)

                        'Finanz
                        wsCli.GetRow(11).GetCell(colDare).SetCellValue(SpMOutPut.Finanziamenti.Dare)
                        wsCli.GetRow(11).GetCell(colAvere).SetCellValue(SpMOutPut.Finanziamenti.Avere)

                        'Forn
                        wsCli.GetRow(12).GetCell(colDare).SetCellValue(SpMOutPut.Fornitori.Dare)
                        wsCli.GetRow(12).GetCell(colAvere).SetCellValue(SpMOutPut.Fornitori.Avere)

                        'Insoluti
                        wsCli.GetRow(13).GetCell(colDare).SetCellValue(SpMOutPut.Insoluti.Dare)
                        wsCli.GetRow(13).GetCell(colAvere).SetCellValue(SpMOutPut.Insoluti.Avere)

                        'INAIL
                        wsCli.GetRow(14).GetCell(colDare).SetCellValue(SpMOutPut.INAIL.Dare)
                        wsCli.GetRow(14).GetCell(colAvere).SetCellValue(SpMOutPut.INAIL.Avere)

                        'INSP
                        wsCli.GetRow(15).GetCell(colDare).SetCellValue(SpMOutPut.INPS.Dare)
                        wsCli.GetRow(15).GetCell(colAvere).SetCellValue(SpMOutPut.INPS.Avere)

                        'IRPEF
                        wsCli.GetRow(16).GetCell(colDare).SetCellValue(SpMOutPut.IRPEF.Dare)
                        wsCli.GetRow(16).GetCell(colAvere).SetCellValue(SpMOutPut.IRPEF.Avere)

                        'IVA
                        wsCli.GetRow(17).GetCell(colDare).SetCellValue(SpMOutPut.IVA.Dare)
                        wsCli.GetRow(17).GetCell(colAvere).SetCellValue(SpMOutPut.IVA.Avere)

                        'Leasing
                        wsCli.GetRow(18).GetCell(colDare).SetCellValue(SpMOutPut.Leasing.Dare)
                        wsCli.GetRow(18).GetCell(colAvere).SetCellValue(SpMOutPut.Leasing.Avere)

                        'RA
                        wsCli.GetRow(19).GetCell(colDare).SetCellValue(SpMOutPut.RA.Dare)
                        wsCli.GetRow(19).GetCell(colAvere).SetCellValue(SpMOutPut.RA.Avere)

                        'Stipendi
                        wsCli.GetRow(20).GetCell(colDare).SetCellValue(SpMOutPut.Stipendi.Dare)
                        wsCli.GetRow(20).GetCell(colAvere).SetCellValue(SpMOutPut.Stipendi.Avere)

                        'EntiDip
                        wsCli.GetRow(21).GetCell(colDare).SetCellValue(SpMOutPut.EntiDipendenti.Dare)
                        wsCli.GetRow(21).GetCell(colAvere).SetCellValue(SpMOutPut.EntiDipendenti.Avere)

                        'Varie
                        wsCli.GetRow(22).GetCell(colDare).SetCellValue(SpMOutPut.Varie.Dare)
                        wsCli.GetRow(22).GetCell(colAvere).SetCellValue(SpMOutPut.Varie.Avere)

                        'Tasse
                        wsCli.GetRow(23).GetCell(colDare).SetCellValue(SpMOutPut.Tasse.Dare)
                        wsCli.GetRow(23).GetCell(colAvere).SetCellValue(SpMOutPut.Tasse.Avere)
                        'Last
                        'IntPass
                        wsCli.GetRow(27).GetCell(colDare).SetCellValue(SpMOutPut.IntPass.Dare)
                        wsCli.GetRow(27).GetCell(colAvere).SetCellValue(SpMOutPut.IntPass.Avere)

                        'Oneri
                        wsCli.GetRow(28).GetCell(colDare).SetCellValue(SpMOutPut.OneriBanc.Dare)
                        wsCli.GetRow(28).GetCell(colAvere).SetCellValue(SpMOutPut.OneriBanc.Avere)
                    End If
                Next


                Dim arrListaPN = listPNVarie.ToArray
                For i = 38 To listPNVarie.Count + 38
                    Dim r As IRow = wsCli.CreateRow(i)
                    For j = 0 To 6
                        r.CreateCell(j)
                    Next
                    wsCli.GetRow(i).GetCell(1).SetCellValue(arrListaPN(i - 38).NumPN)
                    wsCli.GetRow(i).GetCell(2).SetCellValue(arrListaPN(i - 38).Importo)
                    wsCli.GetRow(i).GetCell(3).SetCellValue(arrListaPN(i - 38).CodPN)
                    wsCli.GetRow(i).GetCell(4).SetCellValue(arrListaPN(i - 38).Causale)
                    wsCli.GetRow(i).GetCell(5).SetCellValue(arrListaPN(i - 38).Conto)
                Next
            Catch ex As Exception

            End Try

            workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll()
            workbook.Write(ms1)
            Return File(ms1.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomefile)

        End Function
        <Authorize(Roles:="Commerciale_Admin, Admin")>
        <HttpGet>
        Function Offerto(dateTime As String, agente As String, cliente As String, downloadClienti As Boolean) As FileResult
            Dim datetimeCalc = dateTime.Split("-")
            Dim ListOrdini As New List(Of OrdineOrdinato)
            Dim ListSpese As New List(Of SpeseSecondarieViewModel)
            Dim ListOrdiniFallati As New Dictionary(Of OrdineOrdinato, String)
            Dim dictClienti As New Dictionary(Of String, String)
            Dim dictAgenti As New Dictionary(Of String, String)
            Try
                myConnBI = New SqlConnection(ConnectionStringBI)
                myCmdBI = myConnBI.CreateCommand
                myCmdBI.CommandText = ""
                If agente = "" And cliente = "" Then
                    myCmdBI.CommandText = "USE DWAlnus SELECT AL8.DWOESE, AL8.DWOSEZ, AL8.DWONUM, '', AL2.DWACR1, AL6.DWCCDC, AL1.DWCCNA, AL8.DWOTOT + AL8.DWORAS  + AL8.DWORST + AL8.DWORSB, AL4.DWDMES, AL3.DWTCCM, AL1.DWCCLI, AL7.DWVCA1, AL1.DWCRSC, AL7.DWVDA1 FROM DWAlnus.dbo.DWDCLI00 AL1, DWAlnus.dbo.DWDART00 AL2, DWAlnus.dbo.DWDTPI00 AL3, DWAlnus.dbo.DWDDAT00 AL4, DWAlnus.dbo.DWDJCO00 AL5, DWAlnus.dbo.DWDCLI00 AL6, DWAlnus.dbo.DWDFDV00 AL7, DWAlnus.dbo.DWFOC100 AL8 WHERE (AL8.DWOSEZ = 'PR' AND AL1.DWCSOC=AL8.DWOSOC AND AL6.DWCSOC=AL8.DWOSOC AND AL3.DWTSOC=AL8.DWOSOC AND AL2.DWASOC=AL8.DWOSOC AND AL5.DWJSOC=AL8.DWOSOC AND AL7.DWVSOC=AL8.DWOSOC AND AL8.DWOAK0=AL2.DWAPK0 AND AL5.DWJPK0=AL8.DWOJK0 AND AL8.DWOVK0=AL7.DWVPK0 AND AL3.DWTPK0=AL8.DWOTK0 AND AL8.DWOCK1=AL6.DWCPK0 AND AL8.DWOCK0=AL1.DWCPK0 AND AL8.DWODTDREV=AL4.DWDDATREV) AND (((AL4.DWDDATREV BETWEEN 0 AND 0 OR AL4.DWDDATREV BETWEEN '" + datetimeCalc(0) + "' AND '" + datetimeCalc(1) + "') AND (NOT AL8.DWOSTA='A') AND AL3.DWTARC='1'))"
                Else
                    If agente = "" Then
                        myCmdBI.CommandText = "USE DWAlnus SELECT AL8.DWOESE, AL8.DWOSEZ, AL8.DWONUM, '', AL2.DWACR1, AL6.DWCCDC, AL1.DWCCNA, AL8.DWOTOT + AL8.DWORAS  + AL8.DWORST + AL8.DWORSB, AL4.DWDMES, AL3.DWTCCM, AL1.DWCCLI, AL7.DWVCA1, AL1.DWCRSC, AL7.DWVDA1 FROM DWAlnus.dbo.DWDCLI00 AL1, DWAlnus.dbo.DWDART00 AL2, DWAlnus.dbo.DWDTPI00 AL3, DWAlnus.dbo.DWDDAT00 AL4, DWAlnus.dbo.DWDJCO00 AL5, DWAlnus.dbo.DWDCLI00 AL6, DWAlnus.dbo.DWDFDV00 AL7, DWAlnus.dbo.DWFOC100 AL8 WHERE (AL8.DWOSEZ = 'PR' AND AL1.DWCSOC=AL8.DWOSOC AND AL6.DWCSOC=AL8.DWOSOC AND AL3.DWTSOC=AL8.DWOSOC AND AL2.DWASOC=AL8.DWOSOC AND AL5.DWJSOC=AL8.DWOSOC AND AL7.DWVSOC=AL8.DWOSOC AND AL8.DWOAK0=AL2.DWAPK0 AND AL5.DWJPK0=AL8.DWOJK0 AND AL8.DWOVK0=AL7.DWVPK0 AND AL3.DWTPK0=AL8.DWOTK0 AND AL8.DWOCK1=AL6.DWCPK0 AND AL8.DWOCK0=AL1.DWCPK0 AND AL8.DWODTDREV=AL4.DWDDATREV) AND (((AL4.DWDDATREV BETWEEN 0 AND 0 OR AL4.DWDDATREV BETWEEN '" + datetimeCalc(0) + "' AND '" + datetimeCalc(1) + "') AND (NOT AL8.DWOSTA='A') AND AL3.DWTARC='1')) AND (AL1.DWCCLI = '" + cliente + "')"
                    Else
                        myCmdBI.CommandText = "USE DWAlnus SELECT AL8.DWOESE, AL8.DWOSEZ, AL8.DWONUM, '', AL2.DWACR1, AL6.DWCCDC, AL1.DWCCNA, AL8.DWOTOT + AL8.DWORAS  + AL8.DWORST + AL8.DWORSB, AL4.DWDMES, AL3.DWTCCM, AL1.DWCCLI, AL7.DWVCA1, AL1.DWCRSC, AL7.DWVDA1 FROM DWAlnus.dbo.DWDCLI00 AL1, DWAlnus.dbo.DWDART00 AL2, DWAlnus.dbo.DWDTPI00 AL3, DWAlnus.dbo.DWDDAT00 AL4, DWAlnus.dbo.DWDJCO00 AL5, DWAlnus.dbo.DWDCLI00 AL6, DWAlnus.dbo.DWDFDV00 AL7, DWAlnus.dbo.DWFOC100 AL8 WHERE (AL8.DWOSEZ = 'PR' AND AL1.DWCSOC=AL8.DWOSOC AND AL6.DWCSOC=AL8.DWOSOC AND AL3.DWTSOC=AL8.DWOSOC AND AL2.DWASOC=AL8.DWOSOC AND AL5.DWJSOC=AL8.DWOSOC AND AL7.DWVSOC=AL8.DWOSOC AND AL8.DWOAK0=AL2.DWAPK0 AND AL5.DWJPK0=AL8.DWOJK0 AND AL8.DWOVK0=AL7.DWVPK0 AND AL3.DWTPK0=AL8.DWOTK0 AND AL8.DWOCK1=AL6.DWCPK0 AND AL8.DWOCK0=AL1.DWCPK0 AND AL8.DWODTDREV=AL4.DWDDATREV) AND (((AL4.DWDDATREV BETWEEN 0 AND 0 OR AL4.DWDDATREV BETWEEN '" + datetimeCalc(0) + "' AND '" + datetimeCalc(1) + "') AND (NOT AL8.DWOSTA='A') AND AL3.DWTARC='1')) AND (AL7.DWVCA1 = '" + agente + "')"
                    End If
                End If
                myConnBI.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReaderBI = myCmdBI.ExecuteReader

                Do While myReaderBI.Read()
                    Dim imp = ""
                    Dim o As New OrdineOrdinato With {
                        .Anno = myReaderBI.GetString(0),
                        .codOrd = myReaderBI.GetString(1),
                        .NumOrd = myReaderBI.GetDecimal(2),
                        .Zone = myReaderBI.GetString(6),
                        .TipoOrd = myReaderBI.GetString(4),
                        .Div = myReaderBI.GetString(5),
                        .Mese = myReaderBI.GetString(8).ToString,
                        .TipoOrd2 = myReaderBI.GetString(9),
                        .Cliente = myReaderBI.GetString(10),
                        .Agente = myReaderBI.GetString(11)
                    }
                    If Not dictClienti.ContainsKey(myReaderBI.GetString(10)) Then
                        dictClienti.Add(myReaderBI.GetString(10), myReaderBI.GetString(12))
                    End If
                    If Not dictAgenti.ContainsKey(myReaderBI.GetString(11)) Then
                        dictAgenti.Add(myReaderBI.GetString(11), myReaderBI.GetString(13))
                    End If
                    imp = myReaderBI.GetDecimal(7)
                    o.ImportoOrd = imp
                    ListOrdini.Add(o)

                Loop
                myConn.Close()

            Catch ex As Exception

            End Try
            Dim finalCosts As New Ordinato With {
               .Unistand = New Dictionary(Of String, DivisioneOrdinato),
                .ISA = New Dictionary(Of String, DivisioneOrdinato),
                .MPA = New Dictionary(Of String, DivisioneOrdinato),
                .Drill = New Dictionary(Of String, DivisioneOrdinato),
                .CMT = New Dictionary(Of String, DivisioneOrdinato),
                .Euroma = New Dictionary(Of String, DivisioneOrdinato),
                .ClientiNuovi = New Dictionary(Of String, Integer)
            }
            Dim totalRevenue = 0
            Dim missingImport = 0
            Dim addedSpeseDict As New List(Of String)

            'Next
            Dim listaClientiGiaControllati As New List(Of String)
            For Each o In ListOrdini
                If Not listaClientiGiaControllati.Contains(o.Cliente) Then
                    ' query per vedere quanti ordini sono
                    Try
                        myConn = New SqlConnection(ConnectionString)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "select ORCCLI, COUNT(*) from ORCTES00 where ORCDUMREV < '" + datetimeCalc(0) + "'  AND ORCCLI = '" + o.Cliente + "'group by ORCCLI "
                        myConn.Open()
                    Catch ex As Exception
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader
                        Do While myReader.Read()
                            listaClientiGiaControllati.Add(o.Cliente)
                        Loop
                        myConn.Close()
                    Catch ex As Exception
                    End Try
                End If
            Next
            For Each o In ListOrdini
                If Not listaClientiGiaControllati.Contains(o.Cliente) And Not finalCosts.ClientiNuovi.ContainsKey(o.Cliente) Then
                    finalCosts.ClientiNuovi.Add(o.Cliente, o.Mese)
                End If
            Next
            For a = 0 To ListOrdini.Count - 1
                Console.WriteLine(ListOrdini(a))
            Next
            For Each o In ListOrdini
                Try
                    Select Case o.Div
                        Case "01"
                            If Not finalCosts.Drill.ContainsKey(o.Mese) Then
                                finalCosts.Drill.Add(o.Mese, New DivisioneOrdinato With {.Italia_Nuovo = 0, .Estero_Nuovo = 0, .Italia_Ricambio = 0, .Estero_Ricambio = 0})
                            End If
                            If o.Zone.Contains("SM") Or o.Zone.Contains("IT") Then
                                finalCosts.Drill(o.Mese).Italia_Nuovo = finalCosts.Drill(o.Mese).Italia_Nuovo + o.ImportoOrd
                            Else
                                finalCosts.Drill(o.Mese).Estero_Nuovo = finalCosts.Drill(o.Mese).Estero_Nuovo + o.ImportoOrd
                            End If
                        Case "02"
                            If Not finalCosts.CMT.ContainsKey(o.Mese) Then
                                finalCosts.CMT.Add(o.Mese, New DivisioneOrdinato With {.Italia_Nuovo = 0, .Estero_Nuovo = 0, .Italia_Ricambio = 0, .Estero_Ricambio = 0})
                            End If
                            If o.Zone.Contains("SM") Or o.Zone.Contains("IT") Then
                                finalCosts.CMT(o.Mese).Italia_Nuovo = finalCosts.CMT(o.Mese).Italia_Nuovo + o.ImportoOrd
                            Else
                                finalCosts.CMT(o.Mese).Estero_Nuovo = finalCosts.CMT(o.Mese).Estero_Nuovo + o.ImportoOrd
                            End If
                        Case "03"
                            If Not finalCosts.ISA.ContainsKey(o.Mese) Then
                                finalCosts.ISA.Add(o.Mese, New DivisioneOrdinato With {.Italia_Nuovo = 0, .Estero_Nuovo = 0, .Italia_Ricambio = 0, .Estero_Ricambio = 0})
                            End If
                            If o.Zone.Contains("SM") Or o.Zone.Contains("IT") Then
                                finalCosts.ISA(o.Mese).Italia_Nuovo = finalCosts.ISA(o.Mese).Italia_Nuovo + o.ImportoOrd
                            Else
                                finalCosts.ISA(o.Mese).Estero_Nuovo = finalCosts.ISA(o.Mese).Estero_Nuovo + o.ImportoOrd
                            End If
                        Case "04"
                            If Not finalCosts.Unistand.ContainsKey(o.Mese) Then
                                finalCosts.Unistand.Add(o.Mese, New DivisioneOrdinato With {.Italia_Nuovo = 0, .Estero_Nuovo = 0, .Italia_Ricambio = 0, .Estero_Ricambio = 0})
                            End If
                            If o.Zone.Contains("SM") Or o.Zone.Contains("IT") Then
                                finalCosts.Unistand(o.Mese).Italia_Nuovo = finalCosts.Unistand(o.Mese).Italia_Nuovo + o.ImportoOrd
                            Else
                                finalCosts.Unistand(o.Mese).Estero_Nuovo = finalCosts.Unistand(o.Mese).Estero_Nuovo + o.ImportoOrd
                            End If
                        Case "05"
                            If Not finalCosts.MPA.ContainsKey(o.Mese) Then
                                finalCosts.MPA.Add(o.Mese, New DivisioneOrdinato With {.Italia_Nuovo = 0, .Estero_Nuovo = 0, .Italia_Ricambio = 0, .Estero_Ricambio = 0})
                            End If
                            If o.Zone.Contains("SM") Or o.Zone.Contains("IT") Then
                                finalCosts.MPA(o.Mese).Italia_Nuovo = finalCosts.MPA(o.Mese).Italia_Nuovo + o.ImportoOrd
                            Else
                                finalCosts.MPA(o.Mese).Estero_Nuovo = finalCosts.MPA(o.Mese).Estero_Nuovo + o.ImportoOrd
                            End If
                        Case Else
                            ListOrdiniFallati.Add(o, "Divisione errata")
                    End Select
                    totalRevenue = totalRevenue + o.ImportoOrd
                    myConn.Close()
                Catch ex As Exception
                    myConn.Close()
                    'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                End Try
                'End If

            Next
            'Apertura file
            If Not downloadClienti Then
                Dim fs As New FileStream(Server.MapPath("\Content\Template\Euroma_Offerto.xlsx"), FileMode.Open, FileAccess.ReadWrite)
                Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
                Dim ws As XSSFSheet = workbook.GetSheetAt(0)
                Dim fontIntestazione As XSSFFont = CType(workbook.CreateFont(), XSSFFont)
                fontIntestazione.FontHeightInPoints = CShort(12)
                fontIntestazione.FontName = "Arial"
                fontIntestazione.IsBold = True
                fontIntestazione.IsItalic = False
                fontIntestazione.FontHeightInPoints = 11
                Dim styleIntestazione As XSSFCellStyle = CType(workbook.CreateCellStyle(), XSSFCellStyle)
                styleIntestazione.FillBackgroundColor = FillPattern.SolidForeground
                styleIntestazione.SetFont(fontIntestazione)
                'For p As Integer = 0 To 12
                '    Row.Cells(p).CellStyle = yourStyle
                'Next
                'Base data
                Try
                    ws.GetRow(1).GetCell(4).CellStyle = styleIntestazione
                    ws.GetRow(1).GetCell(4).SetCellValue(datetimeCalc(0).ToString.Substring(0, 4) + "/" + datetimeCalc(0).ToString.Substring(4, 2) + "/" + datetimeCalc(0).ToString.Substring(6, 2))
                    ws.GetRow(1).GetCell(7).CellStyle = styleIntestazione
                    ws.GetRow(1).GetCell(7).SetCellValue(datetimeCalc(1).ToString.Substring(0, 4) + "/" + datetimeCalc(1).ToString.Substring(4, 2) + "/" + datetimeCalc(1).ToString.Substring(6, 2))
                Catch ex As Exception

                End Try
                Dim r As IRow = ws.CreateRow(2)
                For j = 0 To 48
                    r.CreateCell(j)
                Next
                'Intestazione
                Dim c = 1
                Dim countKey = 12
                If agente = "" Then
                    ws.GetRow(1).GetCell(12).SetCellValue("Analisi cliente " + cliente)
                Else
                    ws.GetRow(1).GetCell(12).SetCellValue("Analisi agente " + agente)
                End If
                ws.GetRow(2).GetCell(0).SetCellValue("Mese")
                For i = 1 To 12
                    Try
                        Select Case i.ToString
                            Case "1"
                                ws.GetRow(2).GetCell(c).SetCellValue("01 - Gennaio")
                            Case "2"
                                ws.GetRow(2).GetCell(c).SetCellValue("02 - Febbraio")
                            Case "3"
                                ws.GetRow(2).GetCell(c).SetCellValue("03 - Marzo")
                            Case "4"
                                ws.GetRow(2).GetCell(c).SetCellValue("04 - Aprile")
                            Case "5"
                                ws.GetRow(2).GetCell(c).SetCellValue("05 - Maggio")
                            Case "6"
                                ws.GetRow(2).GetCell(c).SetCellValue("06 - Giugno")
                            Case "7"
                                ws.GetRow(2).GetCell(c).SetCellValue("07 - Luglio")
                            Case "8"
                                ws.GetRow(2).GetCell(c).SetCellValue("08 - Agosto")
                            Case "9"
                                ws.GetRow(2).GetCell(c).SetCellValue("09 - Settembre")
                            Case "10"
                                ws.GetRow(2).GetCell(c).SetCellValue("10 - Ottobre")
                            Case "11"
                                ws.GetRow(2).GetCell(c).SetCellValue("11 - Novembre")
                            Case "12"
                                ws.GetRow(2).GetCell(c).SetCellValue("12 - Dicembre")
                        End Select
                        c = c + 2
                    Catch ex As Exception

                    End Try

                Next
                'Start Pop
                Dim baserow As IRow = ws.GetRow(0)
                'Dim baserow As IRow = ws.GetRow(2)
                Dim ms As New MemoryStream
                Dim ms1 As New MemoryStream
                'Riga Intestazione
                Try
                    Dim i As Integer = 1
                    'Drillmatic
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.Drill.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(5).GetCell(i).SetCellValue(finalCosts.Drill(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(5).GetCell(i).SetCellValue(finalCosts.Drill(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    'Isa
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.ISA.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(6).GetCell(i).SetCellValue(finalCosts.ISA(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(6).GetCell(i).SetCellValue(finalCosts.ISA(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    'cmt
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.CMT.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(7).GetCell(i).SetCellValue(finalCosts.CMT(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(7).GetCell(i).SetCellValue(finalCosts.CMT(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    'UNISTAND
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.Unistand.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(8).GetCell(i).SetCellValue(finalCosts.Unistand(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(8).GetCell(i).SetCellValue(finalCosts.Unistand(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    'MPA
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.MPA.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(9).GetCell(i).SetCellValue(finalCosts.MPA(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(9).GetCell(i).SetCellValue(finalCosts.MPA(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try

                    Next
                    'MPA
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.Euroma.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(10).GetCell(i).SetCellValue(finalCosts.Euroma(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(10).GetCell(i).SetCellValue(finalCosts.Euroma(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try

                    Next
                    ws.GetRow(36).GetCell(1).SetCellValue("Nr. " & finalCosts.ClientiNuovi.Keys.Count.ToString)
                    ws.GetRow(37).GetCell(1).SetCellValue(totalRevenue)
                    'Evaluation totale
                Catch ex As Exception

                End Try

                'ms1 = ms
                Dim nomeFile = "def.xlsx"
                If agente = "" Then
                    nomeFile = "OFFERTO_BRAND_" & cliente & "_" & dateTime.ToString & ".xlsx"
                Else
                    nomeFile = "OFFERTO_BRAND_" & agente & "_" & dateTime.ToString & ".xlsx"
                End If

                Dim wsOC As XSSFSheet = workbook.GetSheetAt(1)
                Dim counter = 2
                For Each i In ListOrdini
                    Dim rOrdini As IRow = wsOC.CreateRow(counter)
                    For jOrdini = 0 To 8
                        rOrdini.CreateCell(jOrdini)
                    Next
                    wsOC.GetRow(counter).GetCell(0).SetCellValue(i.Cliente)
                    wsOC.GetRow(counter).GetCell(1).SetCellValue(i.Anno.ToString + "" + i.codOrd.ToString + "" + i.NumOrd.ToString)
                    wsOC.GetRow(counter).GetCell(2).SetCellValue(i.Zone)
                    wsOC.GetRow(counter).GetCell(3).SetCellValue(i.TipoOrd)
                    wsOC.GetRow(counter).GetCell(4).SetCellValue(i.ImportoOrd.ToString)
                    wsOC.GetRow(counter).GetCell(5).SetCellValue(i.UtenteOrd)
                    wsOC.GetRow(counter).GetCell(6).SetCellValue(i.Div)
                    wsOC.GetRow(counter).GetCell(7).SetCellValue(i.Mese)
                    counter = counter + 1
                Next
                counter = counter + 2
                Dim TitoloFallato As IRow = wsOC.CreateRow(counter)
                TitoloFallato.CreateCell(0).SetCellValue("Offerte fallate")
                counter = counter + 2
                For Each i In ListOrdiniFallati
                    Dim rOrdini As IRow = wsOC.CreateRow(counter)
                    For jOrdini = 0 To 9
                        rOrdini.CreateCell(jOrdini)
                    Next
                    wsOC.GetRow(counter).GetCell(0).SetCellValue(i.Key.Cliente)
                    wsOC.GetRow(counter).GetCell(1).SetCellValue(i.Key.Anno.ToString + "" + i.Key.codOrd.ToString + "" + i.Key.NumOrd.ToString)
                    wsOC.GetRow(counter).GetCell(2).SetCellValue(i.Key.Zone)
                    wsOC.GetRow(counter).GetCell(3).SetCellValue(i.Key.TipoOrd)
                    wsOC.GetRow(counter).GetCell(4).SetCellValue(i.Key.ImportoOrd.ToString)
                    wsOC.GetRow(counter).GetCell(5).SetCellValue(i.Key.UtenteOrd)
                    wsOC.GetRow(counter).GetCell(6).SetCellValue(i.Key.Div)
                    wsOC.GetRow(counter).GetCell(7).SetCellValue(i.Key.Mese)
                    wsOC.GetRow(counter).GetCell(7).SetCellValue(i.Value)
                    counter = counter + 1
                Next
                Dim wsCli As XSSFSheet = workbook.GetSheetAt(2)
                counter = 3
                For Each c In finalCosts.ClientiNuovi.Keys
                    Dim rClienti As IRow = wsCli.CreateRow(counter)
                    For jClienti = 0 To 4
                        rClienti.CreateCell(jClienti)
                    Next
                    wsCli.GetRow(counter).GetCell(0).SetCellValue(c)
                    Dim ordineCliNuovo = ListOrdini.Where(Function(x) x.Cliente = c).First
                    wsCli.GetRow(counter).GetCell(1).SetCellValue(ordineCliNuovo.Anno + "-" + ordineCliNuovo.codOrd + "-" + ordineCliNuovo.NumOrd.ToString)
                    wsCli.GetRow(counter).GetCell(2).SetCellValue(ordineCliNuovo.ImportoOrd)
                    wsCli.GetRow(counter).GetCell(3).SetCellValue(ordineCliNuovo.Mese)
                    counter = counter + 1
                Next
                workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll()
                workbook.Write(ms1)
                Return File(ms1.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeFile)
            Else
                Dim fs As New FileStream(Server.MapPath("\Content\Template\Clienti_Nuovi.xlsx"), FileMode.Open, FileAccess.ReadWrite)
                Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
                Dim ws As XSSFSheet = workbook.GetSheetAt(0)
                Dim fontIntestazione As XSSFFont = CType(workbook.CreateFont(), XSSFFont)
                fontIntestazione.FontHeightInPoints = CShort(12)
                fontIntestazione.FontName = "Arial"
                fontIntestazione.IsBold = True
                fontIntestazione.IsItalic = False
                fontIntestazione.FontHeightInPoints = 11
                Dim styleIntestazione As XSSFCellStyle = CType(workbook.CreateCellStyle(), XSSFCellStyle)
                styleIntestazione.FillBackgroundColor = FillPattern.SolidForeground
                styleIntestazione.SetFont(fontIntestazione)
                Dim nomefile = "ClientiNuoviPeriodo_" + datetimeCalc(0) + "_" + datetimeCalc(1) + ".xlsx"
                'For p As Integer = 0 To 12
                '    Row.Cells(p).CellStyle = yourStyle
                'Next
                'Base data
                Dim ms As New MemoryStream
                Dim ms1 As New MemoryStream
                Dim counter = 2
                Dim wsCli As XSSFSheet = workbook.GetSheetAt(0)
                counter = 3
                For Each c In finalCosts.ClientiNuovi.Keys
                    Dim rClienti As IRow = wsCli.CreateRow(counter)
                    For jClienti = 0 To 4
                        rClienti.CreateCell(jClienti)
                    Next
                    wsCli.GetRow(counter).GetCell(0).SetCellValue(c + " - " + dictClienti.Where(Function(x) x.Key = c).First.Value)
                    Dim ordineCliNuovo = ListOrdini.Where(Function(x) x.Cliente = c).First
                    wsCli.GetRow(counter).GetCell(1).SetCellValue(ordineCliNuovo.Anno + "-" + ordineCliNuovo.codOrd + "-" + ordineCliNuovo.NumOrd.ToString)
                    wsCli.GetRow(counter).GetCell(2).SetCellValue(ordineCliNuovo.ImportoOrd)
                    wsCli.GetRow(counter).GetCell(3).SetCellValue(ordineCliNuovo.Mese)
                    wsCli.GetRow(counter).GetCell(4).SetCellValue(ordineCliNuovo.Agente + " - " + dictAgenti.Where(Function(x) x.Key = ordineCliNuovo.Agente).First.Value)
                    counter = counter + 1
                Next
                workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll()
                workbook.Write(ms1)
                Return File(ms1.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomefile)
            End If
            'Return Json(New With {.tot = finalCosts, .anticipi = anticipi, .miss = totalMissing, .totRev = totalRevenue}, JsonRequestBehavior.AllowGet) '.tmpf = tmpF, .codList = codList, .tmp = dict,, .FD = fattureDrill, .CD = contiDrill
        End Function
        <Authorize(Roles:="Commerciale_Admin, Admin")>
        <HttpGet>
        Function Ordinato(dateTime As String, agente As String, cliente As String, downloadClienti As Boolean) As FileResult
            Dim datetimeCalc = dateTime.Split("-")
            Dim ListOrdini As New List(Of OrdineOrdinato)
            Dim ListSpese As New List(Of SpeseSecondarieViewModel)
            Dim ListOrdiniFallati As New Dictionary(Of OrdineOrdinato, String)
            Dim dictClienti As New Dictionary(Of String, String)
            Dim dictAgenti As New Dictionary(Of String, String)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "select ESECOD, ORCTSZ, ORCTNR, SUM(ORCSPC) from ORCSPE00 WHERE ESECOD > '2021' GROUP BY ESECOD, ORCTSZ, ORCTNR"
                myConn.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Dim o As New SpeseSecondarieViewModel With {
                        .Anno = myReader.GetString(0),
                        .codOrd = myReader.GetString(1),
                        .NumOrd = myReader.GetDecimal(2),
                        .Value = myReader.GetDecimal(3)
                    }
                    ListSpese.Add(o)

                Loop
                myConn.Close()

            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myConnBI = New SqlConnection(ConnectionStringBI)
                myCmdBI = myConnBI.CreateCommand
                myCmdBI.CommandText = ""
                If agente = "" And cliente = "" Then
                    myCmdBI.CommandText = "USE DWAlnus SELECT AL8.DWOESE, AL8.DWOSEZ, AL8.DWONUM, '', AL2.DWACR1, AL6.DWCCDC, AL1.DWCCNA, AL8.DWOTOT + AL8.DWORAS  + AL8.DWORST + AL8.DWORSB  , AL4.DWDMES, AL3.DWTCCM, AL1.DWCCLI, AL7.DWVCA1,AL1.DWCRSC,  AL7.DWVDA1 FROM DWAlnus.dbo.DWDCLI00 AL1, DWAlnus.dbo.DWDART00 AL2, DWAlnus.dbo.DWDTPI00 AL3, DWAlnus.dbo.DWDDAT00 AL4, DWAlnus.dbo.DWDJCO00 AL5, DWAlnus.dbo.DWDCLI00 AL6, DWAlnus.dbo.DWDFDV00 AL7, DWAlnus.dbo.DWFOC100 AL8 WHERE (AL1.DWCSOC=AL8.DWOSOC AND AL6.DWCSOC=AL8.DWOSOC AND AL3.DWTSOC=AL8.DWOSOC AND AL2.DWASOC=AL8.DWOSOC AND AL5.DWJSOC=AL8.DWOSOC AND AL7.DWVSOC=AL8.DWOSOC AND AL8.DWOAK0=AL2.DWAPK0 AND AL5.DWJPK0=AL8.DWOJK0 AND AL8.DWOVK0=AL7.DWVPK0 AND AL3.DWTPK0=AL8.DWOTK0 AND AL8.DWOCK1=AL6.DWCPK0 AND AL8.DWOCK0=AL1.DWCPK0 AND AL8.DWODTDREV=AL4.DWDDATREV) AND (((AL4.DWDDATREV BETWEEN 0 AND 0 OR AL4.DWDDATREV BETWEEN '" + datetimeCalc(0) + "' AND '" + datetimeCalc(1) + "') AND (NOT AL8.DWOSTA='A') AND AL3.DWTARC='2'))"
                Else
                    If agente = "" Then
                        myCmdBI.CommandText = "USE DWAlnus SELECT AL8.DWOESE, AL8.DWOSEZ, AL8.DWONUM, '', AL2.DWACR1, AL6.DWCCDC, AL1.DWCCNA, AL8.DWOTOT + AL8.DWORAS  + AL8.DWORST + AL8.DWORSB  , AL4.DWDMES, AL3.DWTCCM, AL1.DWCCLI, AL7.DWVCA1,AL1.DWCRSC,  AL7.DWVDA1 FROM DWAlnus.dbo.DWDCLI00 AL1, DWAlnus.dbo.DWDART00 AL2, DWAlnus.dbo.DWDTPI00 AL3, DWAlnus.dbo.DWDDAT00 AL4, DWAlnus.dbo.DWDJCO00 AL5, DWAlnus.dbo.DWDCLI00 AL6, DWAlnus.dbo.DWDFDV00 AL7, DWAlnus.dbo.DWFOC100 AL8 WHERE (AL1.DWCSOC=AL8.DWOSOC AND AL6.DWCSOC=AL8.DWOSOC AND AL3.DWTSOC=AL8.DWOSOC AND AL2.DWASOC=AL8.DWOSOC AND AL5.DWJSOC=AL8.DWOSOC AND AL7.DWVSOC=AL8.DWOSOC AND AL8.DWOAK0=AL2.DWAPK0 AND AL5.DWJPK0=AL8.DWOJK0 AND AL8.DWOVK0=AL7.DWVPK0 AND AL3.DWTPK0=AL8.DWOTK0 AND AL8.DWOCK1=AL6.DWCPK0 AND AL8.DWOCK0=AL1.DWCPK0 AND AL8.DWODTDREV=AL4.DWDDATREV) AND (((AL4.DWDDATREV BETWEEN 0 AND 0 OR AL4.DWDDATREV BETWEEN '" + datetimeCalc(0) + "' AND '" + datetimeCalc(1) + "')  AND (NOT AL8.DWOSTA='A') AND AL3.DWTARC='2')) AND (AL1.DWCCLI = '" + cliente + "')"
                    Else
                        myCmdBI.CommandText = "USE DWAlnus SELECT AL8.DWOESE, AL8.DWOSEZ, AL8.DWONUM, '', AL2.DWACR1, AL6.DWCCDC, AL1.DWCCNA, AL8.DWOTOT + AL8.DWORAS  + AL8.DWORST + AL8.DWORSB  , AL4.DWDMES, AL3.DWTCCM, AL1.DWCCLI, AL7.DWVCA1,AL1.DWCRSC,  AL7.DWVDA1 FROM DWAlnus.dbo.DWDCLI00 AL1, DWAlnus.dbo.DWDART00 AL2, DWAlnus.dbo.DWDTPI00 AL3, DWAlnus.dbo.DWDDAT00 AL4, DWAlnus.dbo.DWDJCO00 AL5, DWAlnus.dbo.DWDCLI00 AL6, DWAlnus.dbo.DWDFDV00 AL7, DWAlnus.dbo.DWFOC100 AL8 WHERE (AL1.DWCSOC=AL8.DWOSOC AND AL6.DWCSOC=AL8.DWOSOC AND AL3.DWTSOC=AL8.DWOSOC AND AL2.DWASOC=AL8.DWOSOC AND AL5.DWJSOC=AL8.DWOSOC AND AL7.DWVSOC=AL8.DWOSOC AND AL8.DWOAK0=AL2.DWAPK0 AND AL5.DWJPK0=AL8.DWOJK0 AND AL8.DWOVK0=AL7.DWVPK0 AND AL3.DWTPK0=AL8.DWOTK0 AND AL8.DWOCK1=AL6.DWCPK0 AND AL8.DWOCK0=AL1.DWCPK0 AND AL8.DWODTDREV=AL4.DWDDATREV) AND (((AL4.DWDDATREV BETWEEN 0 AND 0 OR AL4.DWDDATREV BETWEEN '" + datetimeCalc(0) + "' AND '" + datetimeCalc(1) + "') AND (NOT AL8.DWOSTA='A') AND AL3.DWTARC='2')) AND (AL7.DWVCA1 = '" + agente + "')"
                    End If
                End If
                myConnBI.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReaderBI = myCmdBI.ExecuteReader

                Do While myReaderBI.Read()
                    Dim imp = ""
                    Dim o As New OrdineOrdinato With {
                        .Anno = myReaderBI.GetString(0),
                        .codOrd = myReaderBI.GetString(1),
                        .NumOrd = myReaderBI.GetDecimal(2),
                        .Zone = myReaderBI.GetString(6),
                        .TipoOrd = myReaderBI.GetString(4),
                        .Div = myReaderBI.GetString(5),
                        .Mese = myReaderBI.GetString(8).ToString,
                        .TipoOrd2 = myReaderBI.GetString(9),
                        .Cliente = myReaderBI.GetString(10),
                        .Agente = myReaderBI.GetString(11)
                    }
                    If Not dictClienti.ContainsKey(myReaderBI.GetString(10)) Then
                        dictClienti.Add(myReaderBI.GetString(10), myReaderBI.GetString(12))
                    End If
                    If Not dictAgenti.ContainsKey(myReaderBI.GetString(11)) Then
                        dictAgenti.Add(myReaderBI.GetString(11), myReaderBI.GetString(13))
                    End If
                    imp = myReaderBI.GetDecimal(7)
                    o.ImportoOrd = imp
                    ListOrdini.Add(o)

                Loop
                myConn.Close()

            Catch ex As Exception

            End Try
            Dim finalCosts As New Ordinato With {
               .Unistand = New Dictionary(Of String, DivisioneOrdinato),
                .ISA = New Dictionary(Of String, DivisioneOrdinato),
                .MPA = New Dictionary(Of String, DivisioneOrdinato),
                .Drill = New Dictionary(Of String, DivisioneOrdinato),
                .CMT = New Dictionary(Of String, DivisioneOrdinato),
                .Euroma = New Dictionary(Of String, DivisioneOrdinato),
                .ClientiNuovi = New Dictionary(Of String, Integer)
            }
            Dim totalRevenue = 0
            Dim missingImport = 0
            Dim addedSpeseDict As New List(Of String)

            'Next
            Dim listaClientiGiaControllati As New List(Of String)
            For Each o In ListOrdini
                If Not listaClientiGiaControllati.Contains(o.Cliente) Then
                    ' query per vedere quanti ordini sono
                    Try
                        myConn = New SqlConnection(ConnectionString)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "select ORCCLI, COUNT(*) from ORCTES00 where ORCDUMREV < '" + datetimeCalc(0) + "'  AND ORCCLI = '" + o.Cliente + "'group by ORCCLI "
                        myConn.Open()
                    Catch ex As Exception
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader
                        Do While myReader.Read()
                            listaClientiGiaControllati.Add(o.Cliente)
                        Loop
                        myConn.Close()
                    Catch ex As Exception
                    End Try
                End If
            Next
            For Each o In ListOrdini
                If Not listaClientiGiaControllati.Contains(o.Cliente) And Not finalCosts.ClientiNuovi.ContainsKey(o.Cliente) Then
                    finalCosts.ClientiNuovi.Add(o.Cliente, o.Mese)
                End If
            Next
            For a = 0 To ListOrdini.Count - 1
                Console.WriteLine(ListOrdini(a))
            Next
            For Each o In ListOrdini
                Try
                    Select Case o.Div
                        Case "01"
                            If Not finalCosts.Drill.ContainsKey(o.Mese) Then
                                finalCosts.Drill.Add(o.Mese, New DivisioneOrdinato With {.Italia_Nuovo = 0, .Estero_Nuovo = 0, .Italia_Ricambio = 0, .Estero_Ricambio = 0})
                            End If
                            If o.Zone.Contains("SM") Or o.Zone.Contains("IT") Then
                                If Not o.TipoOrd.Contains("DRILL") And Not o.TipoOrd.Contains("MPA") And Not o.TipoOrd.Contains("A/R") Then
                                    ListOrdiniFallati.Add(o, "Raggruppamento 1 errato")
                                Else
                                    If o.TipoOrd2 = "UC1" Or o.TipoOrd = "UC9" Then
                                        finalCosts.Drill(o.Mese).Italia_Nuovo = finalCosts.Drill(o.Mese).Italia_Nuovo + o.ImportoOrd
                                    Else
                                        finalCosts.Drill(o.Mese).Italia_Ricambio = finalCosts.Drill(o.Mese).Italia_Ricambio + o.ImportoOrd
                                    End If
                                End If
                            Else
                                If Not o.TipoOrd.Contains("DRILL") And Not o.TipoOrd.Contains("MPA") And Not o.TipoOrd.Contains("A/R") Then
                                    ListOrdiniFallati.Add(o, "Raggruppamento 1 errato")
                                Else
                                    If o.TipoOrd2 = "UC1" Or o.TipoOrd = "UC9" Then
                                        finalCosts.Drill(o.Mese).Estero_Nuovo = finalCosts.Drill(o.Mese).Estero_Nuovo + o.ImportoOrd
                                    Else
                                        finalCosts.Drill(o.Mese).Estero_Ricambio = finalCosts.Drill(o.Mese).Estero_Ricambio + o.ImportoOrd
                                    End If
                                End If
                            End If
                        Case "02"
                            If Not finalCosts.CMT.ContainsKey(o.Mese) Then
                                finalCosts.CMT.Add(o.Mese, New DivisioneOrdinato With {.Italia_Nuovo = 0, .Estero_Nuovo = 0, .Italia_Ricambio = 0, .Estero_Ricambio = 0})
                            End If
                            If o.Zone.Contains("SM") Or o.Zone.Contains("IT") Then
                                If Not o.TipoOrd.Contains("CMT") And Not o.TipoOrd.Contains("A/R") Then
                                    ListOrdiniFallati.Add(o, "Raggruppamento 1 errato")
                                Else
                                    If o.TipoOrd2 = "UC1" Or o.TipoOrd = "UC9" Then
                                        finalCosts.CMT(o.Mese).Italia_Nuovo = finalCosts.CMT(o.Mese).Italia_Nuovo + o.ImportoOrd
                                    Else
                                        finalCosts.CMT(o.Mese).Italia_Ricambio = finalCosts.CMT(o.Mese).Italia_Ricambio + o.ImportoOrd
                                    End If
                                End If
                            Else
                                If Not o.TipoOrd.Contains("CMT") And Not o.TipoOrd.Contains("A/R") Then
                                    ListOrdiniFallati.Add(o, "Raggruppamento 1 errato")
                                Else
                                    If o.TipoOrd2 = "UC1" Or o.TipoOrd = "UC9" Then
                                        finalCosts.CMT(o.Mese).Estero_Nuovo = finalCosts.CMT(o.Mese).Estero_Nuovo + o.ImportoOrd
                                    Else
                                        finalCosts.CMT(o.Mese).Estero_Ricambio = finalCosts.CMT(o.Mese).Estero_Ricambio + o.ImportoOrd
                                    End If
                                End If
                            End If
                        Case "03"
                            If Not finalCosts.ISA.ContainsKey(o.Mese) Then
                                finalCosts.ISA.Add(o.Mese, New DivisioneOrdinato With {.Italia_Nuovo = 0, .Estero_Nuovo = 0, .Italia_Ricambio = 0, .Estero_Ricambio = 0})
                            End If
                            If o.Zone.Contains("SM") Or o.Zone.Contains("IT") Then
                                If Not o.TipoOrd.Contains("ISA") And Not o.TipoOrd.Contains("A/R") Then
                                    ListOrdiniFallati.Add(o, "Raggruppamento 1 errato")
                                Else
                                    If o.TipoOrd2 = "UC1" Or o.TipoOrd = "UC9" Then
                                        finalCosts.ISA(o.Mese).Italia_Nuovo = finalCosts.ISA(o.Mese).Italia_Nuovo + o.ImportoOrd
                                    Else
                                        finalCosts.ISA(o.Mese).Italia_Ricambio = finalCosts.ISA(o.Mese).Italia_Ricambio + o.ImportoOrd
                                    End If
                                End If
                            Else
                                If Not o.TipoOrd.Contains("ISA") And Not o.TipoOrd.Contains("A/R") Then
                                    ListOrdiniFallati.Add(o, "Raggruppamento 1 errato")
                                Else
                                    If o.TipoOrd2 = "UC1" Or o.TipoOrd = "UC9" Then
                                        finalCosts.ISA(o.Mese).Estero_Nuovo = finalCosts.ISA(o.Mese).Estero_Nuovo + o.ImportoOrd
                                    Else
                                        finalCosts.ISA(o.Mese).Estero_Ricambio = finalCosts.ISA(o.Mese).Estero_Ricambio + o.ImportoOrd
                                    End If
                                End If
                            End If
                        Case "04"
                            If Not finalCosts.Unistand.ContainsKey(o.Mese) Then
                                finalCosts.Unistand.Add(o.Mese, New DivisioneOrdinato With {.Italia_Nuovo = 0, .Estero_Nuovo = 0, .Italia_Ricambio = 0, .Estero_Ricambio = 0})
                            End If
                            If o.Zone.Contains("SM") Or o.Zone.Contains("IT") Then
                                If Not o.TipoOrd.Contains("UNI") And Not o.TipoOrd.Contains("A/R") Then
                                    ListOrdiniFallati.Add(o, "Raggruppamento 1 errato")
                                Else
                                    If o.TipoOrd2 = "UC1" Or o.TipoOrd = "UC9" Then
                                        finalCosts.Unistand(o.Mese).Italia_Nuovo = finalCosts.Unistand(o.Mese).Italia_Nuovo + o.ImportoOrd
                                    Else
                                        finalCosts.Unistand(o.Mese).Italia_Ricambio = finalCosts.Unistand(o.Mese).Italia_Ricambio + o.ImportoOrd
                                    End If
                                End If
                            Else
                                If Not o.TipoOrd.Contains("UNI") And Not o.TipoOrd.Contains("A/R") Then
                                    ListOrdiniFallati.Add(o, "Raggruppamento 1 errato")
                                Else
                                    If o.TipoOrd2 = "UC1" Or o.TipoOrd = "UC9" Then
                                        finalCosts.Unistand(o.Mese).Estero_Nuovo = finalCosts.Unistand(o.Mese).Estero_Nuovo + o.ImportoOrd
                                    Else
                                        finalCosts.Unistand(o.Mese).Estero_Ricambio = finalCosts.Unistand(o.Mese).Estero_Ricambio + o.ImportoOrd
                                    End If
                                End If
                            End If
                        Case "05"
                            If Not finalCosts.MPA.ContainsKey(o.Mese) Then
                                finalCosts.MPA.Add(o.Mese, New DivisioneOrdinato With {.Italia_Nuovo = 0, .Estero_Nuovo = 0, .Italia_Ricambio = 0, .Estero_Ricambio = 0})
                            End If
                            If o.Zone.Contains("SM") Or o.Zone.Contains("IT") Then
                                If Not o.TipoOrd.Contains("MPA") And Not o.TipoOrd.Contains("A/R") Then
                                    ListOrdiniFallati.Add(o, "Raggruppamento 1 errato")
                                Else
                                    If o.TipoOrd2 = "UC1" Or o.TipoOrd = "UC9" Then
                                        finalCosts.MPA(o.Mese).Italia_Nuovo = finalCosts.MPA(o.Mese).Italia_Nuovo + o.ImportoOrd
                                    Else
                                        finalCosts.MPA(o.Mese).Italia_Ricambio = finalCosts.MPA(o.Mese).Italia_Ricambio + o.ImportoOrd
                                    End If
                                End If
                            Else
                                If Not o.TipoOrd.Contains("MPA") And Not o.TipoOrd.Contains("A/R") Then
                                    ListOrdiniFallati.Add(o, "Raggruppamento 1 errato")
                                Else
                                    If o.TipoOrd2 = "UC1" Or o.TipoOrd = "UC9" Then
                                        finalCosts.MPA(o.Mese).Estero_Nuovo = finalCosts.MPA(o.Mese).Estero_Nuovo + o.ImportoOrd
                                    Else
                                        finalCosts.MPA(o.Mese).Estero_Ricambio = finalCosts.MPA(o.Mese).Estero_Ricambio + o.ImportoOrd
                                    End If
                                End If
                            End If
                        Case Else
                            ListOrdiniFallati.Add(o, "Divisione errata")
                    End Select
                    totalRevenue = totalRevenue + o.ImportoOrd
                    myConn.Close()
                Catch ex As Exception
                    myConn.Close()
                    'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                End Try
                'End If

            Next
            'Apertura file
            If Not downloadClienti Then
                Dim fs As New FileStream(Server.MapPath("\Content\Template\Euroma_Ordinato.xlsx"), FileMode.Open, FileAccess.ReadWrite)
                Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
                Dim ws As XSSFSheet = workbook.GetSheetAt(0)
                Dim fontIntestazione As XSSFFont = CType(workbook.CreateFont(), XSSFFont)
                fontIntestazione.FontHeightInPoints = CShort(12)
                fontIntestazione.FontName = "Arial"
                fontIntestazione.IsBold = True
                fontIntestazione.IsItalic = False
                fontIntestazione.FontHeightInPoints = 11
                Dim styleIntestazione As XSSFCellStyle = CType(workbook.CreateCellStyle(), XSSFCellStyle)
                styleIntestazione.FillBackgroundColor = FillPattern.SolidForeground
                styleIntestazione.SetFont(fontIntestazione)
                'For p As Integer = 0 To 12
                '    Row.Cells(p).CellStyle = yourStyle
                'Next
                'Base data
                Try
                    ws.GetRow(1).GetCell(6).CellStyle = styleIntestazione
                    ws.GetRow(1).GetCell(6).SetCellValue(datetimeCalc(0).ToString.Substring(0, 4) + "/" + datetimeCalc(0).ToString.Substring(4, 2) + "/" + datetimeCalc(0).ToString.Substring(6, 2))
                    ws.GetRow(1).GetCell(9).CellStyle = styleIntestazione
                    ws.GetRow(1).GetCell(9).SetCellValue(datetimeCalc(1).ToString.Substring(0, 4) + "/" + datetimeCalc(1).ToString.Substring(4, 2) + "/" + datetimeCalc(1).ToString.Substring(6, 2))
                Catch ex As Exception

                End Try
                Dim r As IRow = ws.CreateRow(2)
                For j = 0 To 48
                    r.CreateCell(j)
                Next
                'Intestazione
                Dim c = 1
                Dim countKey = 12
                If agente = "" Then
                    ws.GetRow(1).GetCell(12).SetCellValue("Analisi cliente " + cliente)
                Else
                    ws.GetRow(1).GetCell(12).SetCellValue("Analisi agente " + agente)
                End If
                ws.GetRow(2).GetCell(0).SetCellValue("Mese")
                For i = 1 To 12
                    Try
                        Select Case i.ToString
                            Case "1"
                                ws.GetRow(2).GetCell(c).SetCellValue("01 - Gennaio")
                            Case "2"
                                ws.GetRow(2).GetCell(c).SetCellValue("02 - Febbraio")
                            Case "3"
                                ws.GetRow(2).GetCell(c).SetCellValue("03 - Marzo")
                            Case "4"
                                ws.GetRow(2).GetCell(c).SetCellValue("04 - Aprile")
                            Case "5"
                                ws.GetRow(2).GetCell(c).SetCellValue("05 - Maggio")
                            Case "6"
                                ws.GetRow(2).GetCell(c).SetCellValue("06 - Giugno")
                            Case "7"
                                ws.GetRow(2).GetCell(c).SetCellValue("07 - Luglio")
                            Case "8"
                                ws.GetRow(2).GetCell(c).SetCellValue("08 - Agosto")
                            Case "9"
                                ws.GetRow(2).GetCell(c).SetCellValue("09 - Settembre")
                            Case "10"
                                ws.GetRow(2).GetCell(c).SetCellValue("10 - Ottobre")
                            Case "11"
                                ws.GetRow(2).GetCell(c).SetCellValue("11 - Novembre")
                            Case "12"
                                ws.GetRow(2).GetCell(c).SetCellValue("12 - Dicembre")
                        End Select
                        c = c + 4
                    Catch ex As Exception

                    End Try

                Next
                'Start Pop
                Dim baserow As IRow = ws.GetRow(0)
                'Dim baserow As IRow = ws.GetRow(2)
                Dim ms As New MemoryStream
                Dim ms1 As New MemoryStream
                'Riga Intestazione
                Try
                    Dim i As Integer = 1
                    'Drillmatic
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.Drill.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(5).GetCell(i).SetCellValue(finalCosts.Drill(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(5).GetCell(i).SetCellValue(finalCosts.Drill(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                    i = i + 1
                                    ws.GetRow(5).GetCell(i).SetCellValue(finalCosts.Drill(GetCellValue(ws.GetRow(2), i - 2).ToString.Split(" ")(0)).Italia_Ricambio)
                                    i = i + 1
                                    ws.GetRow(5).GetCell(i).SetCellValue(finalCosts.Drill(GetCellValue(ws.GetRow(2), i - 3).ToString.Split(" ")(0)).Estero_Ricambio)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    'Isa
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.ISA.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(6).GetCell(i).SetCellValue(finalCosts.ISA(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(6).GetCell(i).SetCellValue(finalCosts.ISA(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                    i = i + 1
                                    ws.GetRow(6).GetCell(i).SetCellValue(finalCosts.ISA(GetCellValue(ws.GetRow(2), i - 2).ToString.Split(" ")(0)).Italia_Ricambio)
                                    i = i + 1
                                    ws.GetRow(6).GetCell(i).SetCellValue(finalCosts.ISA(GetCellValue(ws.GetRow(2), i - 3).ToString.Split(" ")(0)).Estero_Ricambio)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    'cmt
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.CMT.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(7).GetCell(i).SetCellValue(finalCosts.CMT(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(7).GetCell(i).SetCellValue(finalCosts.CMT(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                    i = i + 1
                                    ws.GetRow(7).GetCell(i).SetCellValue(finalCosts.CMT(GetCellValue(ws.GetRow(2), i - 2).ToString.Split(" ")(0)).Italia_Ricambio)
                                    i = i + 1
                                    ws.GetRow(7).GetCell(i).SetCellValue(finalCosts.CMT(GetCellValue(ws.GetRow(2), i - 3).ToString.Split(" ")(0)).Estero_Ricambio)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    'UNISTAND
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.Unistand.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(8).GetCell(i).SetCellValue(finalCosts.Unistand(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(8).GetCell(i).SetCellValue(finalCosts.Unistand(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                    i = i + 1
                                    ws.GetRow(8).GetCell(i).SetCellValue(finalCosts.Unistand(GetCellValue(ws.GetRow(2), i - 2).ToString.Split(" ")(0)).Italia_Ricambio)
                                    i = i + 1
                                    ws.GetRow(8).GetCell(i).SetCellValue(finalCosts.Unistand(GetCellValue(ws.GetRow(2), i - 3).ToString.Split(" ")(0)).Estero_Ricambio)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                    'MPA
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.MPA.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(9).GetCell(i).SetCellValue(finalCosts.MPA(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(9).GetCell(i).SetCellValue(finalCosts.MPA(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                    i = i + 1
                                    ws.GetRow(9).GetCell(i).SetCellValue(finalCosts.MPA(GetCellValue(ws.GetRow(2), i - 2).ToString.Split(" ")(0)).Italia_Ricambio)
                                    i = i + 1
                                    ws.GetRow(9).GetCell(i).SetCellValue(finalCosts.MPA(GetCellValue(ws.GetRow(2), i - 3).ToString.Split(" ")(0)).Estero_Ricambio)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try

                    Next
                    'MPA
                    i = 1
                    For i = 1 To countKey * 4
                        Try
                            If finalCosts.Euroma.ContainsKey(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)) Then
                                Try
                                    ws.GetRow(10).GetCell(i).SetCellValue(finalCosts.Euroma(GetCellValue(ws.GetRow(2), i).ToString.Split(" ")(0)).Italia_Nuovo)
                                    i = i + 1
                                    ws.GetRow(10).GetCell(i).SetCellValue(finalCosts.Euroma(GetCellValue(ws.GetRow(2), i - 1).ToString.Split(" ")(0)).Estero_Nuovo)
                                    i = i + 1
                                    ws.GetRow(10).GetCell(i).SetCellValue(finalCosts.Euroma(GetCellValue(ws.GetRow(2), i - 2).ToString.Split(" ")(0)).Italia_Ricambio)
                                    i = i + 1
                                    ws.GetRow(10).GetCell(i).SetCellValue(finalCosts.Euroma(GetCellValue(ws.GetRow(2), i - 3).ToString.Split(" ")(0)).Estero_Ricambio)
                                Catch ex As Exception

                                End Try
                            Else
                                i = i + 1
                            End If
                        Catch ex As Exception

                        End Try

                    Next
                    ws.GetRow(36).GetCell(1).SetCellValue("Nr. " & finalCosts.ClientiNuovi.Keys.Count.ToString)
                    ws.GetRow(37).GetCell(1).SetCellValue(totalRevenue)
                    'Evaluation totale
                Catch ex As Exception

                End Try

                'ms1 = ms
                Dim nomeFile = "def.xlsx"
                If agente = "" Then
                    nomeFile = "ORDINATO_BRAND_" & cliente & "_" & dateTime.ToString & ".xlsx"
                Else
                    nomeFile = "ORDINATO_BRAND_" & agente & "_" & dateTime.ToString & ".xlsx"
                End If

                Dim wsOC As XSSFSheet = workbook.GetSheetAt(1)
                Dim counter = 2
                For Each i In ListOrdini
                    Dim rOrdini As IRow = wsOC.CreateRow(counter)
                    For jOrdini = 0 To 8
                        rOrdini.CreateCell(jOrdini)
                    Next
                    wsOC.GetRow(counter).GetCell(0).SetCellValue(i.Cliente)
                    wsOC.GetRow(counter).GetCell(1).SetCellValue(i.Anno.ToString + "" + i.codOrd.ToString + "" + i.NumOrd.ToString)
                    wsOC.GetRow(counter).GetCell(2).SetCellValue(i.Zone)
                    wsOC.GetRow(counter).GetCell(3).SetCellValue(i.TipoOrd)
                    wsOC.GetRow(counter).GetCell(4).SetCellValue(i.ImportoOrd.ToString)
                    wsOC.GetRow(counter).GetCell(5).SetCellValue(i.UtenteOrd)
                    wsOC.GetRow(counter).GetCell(6).SetCellValue(i.Div)
                    wsOC.GetRow(counter).GetCell(7).SetCellValue(i.Mese)
                    counter = counter + 1
                Next
                counter = counter + 2
                Dim TitoloFallato As IRow = wsOC.CreateRow(counter)
                TitoloFallato.CreateCell(0).SetCellValue("Ordini fallati")
                counter = counter + 2
                For Each i In ListOrdiniFallati
                    Dim rOrdini As IRow = wsOC.CreateRow(counter)
                    For jOrdini = 0 To 9
                        rOrdini.CreateCell(jOrdini)
                    Next
                    wsOC.GetRow(counter).GetCell(0).SetCellValue(i.Key.Cliente)
                    wsOC.GetRow(counter).GetCell(1).SetCellValue(i.Key.Anno.ToString + "" + i.Key.codOrd.ToString + "" + i.Key.NumOrd.ToString)
                    wsOC.GetRow(counter).GetCell(2).SetCellValue(i.Key.Zone)
                    wsOC.GetRow(counter).GetCell(3).SetCellValue(i.Key.TipoOrd)
                    wsOC.GetRow(counter).GetCell(4).SetCellValue(i.Key.ImportoOrd.ToString)
                    wsOC.GetRow(counter).GetCell(5).SetCellValue(i.Key.UtenteOrd)
                    wsOC.GetRow(counter).GetCell(6).SetCellValue(i.Key.Div)
                    wsOC.GetRow(counter).GetCell(7).SetCellValue(i.Key.Mese)
                    wsOC.GetRow(counter).GetCell(7).SetCellValue(i.Value)
                    counter = counter + 1
                Next
                Dim wsCli As XSSFSheet = workbook.GetSheetAt(2)
                counter = 3
                For Each c In finalCosts.ClientiNuovi.Keys
                    Dim rClienti As IRow = wsCli.CreateRow(counter)
                    For jClienti = 0 To 4
                        rClienti.CreateCell(jClienti)
                    Next
                    wsCli.GetRow(counter).GetCell(0).SetCellValue(c)
                    Dim ordineCliNuovo = ListOrdini.Where(Function(x) x.Cliente = c).First
                    wsCli.GetRow(counter).GetCell(1).SetCellValue(ordineCliNuovo.Anno + "-" + ordineCliNuovo.codOrd + "-" + ordineCliNuovo.NumOrd.ToString)
                    wsCli.GetRow(counter).GetCell(2).SetCellValue(ordineCliNuovo.ImportoOrd)
                    wsCli.GetRow(counter).GetCell(3).SetCellValue(ordineCliNuovo.Mese)
                    counter = counter + 1
                Next
                workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll()
                workbook.Write(ms1)
                Return File(ms1.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeFile)
            Else
                Dim fs As New FileStream(Server.MapPath("\Content\Template\Clienti_Nuovi.xlsx"), FileMode.Open, FileAccess.ReadWrite)
                Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)
                Dim ws As XSSFSheet = workbook.GetSheetAt(0)
                Dim fontIntestazione As XSSFFont = CType(workbook.CreateFont(), XSSFFont)
                fontIntestazione.FontHeightInPoints = CShort(12)
                fontIntestazione.FontName = "Arial"
                fontIntestazione.IsBold = True
                fontIntestazione.IsItalic = False
                fontIntestazione.FontHeightInPoints = 11
                Dim styleIntestazione As XSSFCellStyle = CType(workbook.CreateCellStyle(), XSSFCellStyle)
                styleIntestazione.FillBackgroundColor = FillPattern.SolidForeground
                styleIntestazione.SetFont(fontIntestazione)
                Dim nomefile = "ClientiNuoviPeriodo_" + datetimeCalc(0) + "_" + datetimeCalc(1) + ".xlsx"
                'For p As Integer = 0 To 12
                '    Row.Cells(p).CellStyle = yourStyle
                'Next
                'Base data
                Dim ms As New MemoryStream
                Dim ms1 As New MemoryStream
                Dim counter = 2
                Dim wsCli As XSSFSheet = workbook.GetSheetAt(0)
                counter = 3
                For Each c In finalCosts.ClientiNuovi.Keys
                    Dim rClienti As IRow = wsCli.CreateRow(counter)
                    For jClienti = 0 To 4
                        rClienti.CreateCell(jClienti)
                    Next
                    wsCli.GetRow(counter).GetCell(0).SetCellValue(c + " - " + dictClienti.Where(Function(x) x.Key = c).First.Value)
                    Dim ordineCliNuovo = ListOrdini.Where(Function(x) x.Cliente = c).First
                    wsCli.GetRow(counter).GetCell(1).SetCellValue(ordineCliNuovo.Anno + "-" + ordineCliNuovo.codOrd + "-" + ordineCliNuovo.NumOrd.ToString)
                    wsCli.GetRow(counter).GetCell(2).SetCellValue(ordineCliNuovo.ImportoOrd)
                    wsCli.GetRow(counter).GetCell(3).SetCellValue(ordineCliNuovo.Mese)
                    wsCli.GetRow(counter).GetCell(4).SetCellValue(ordineCliNuovo.Agente + " - " + dictAgenti.Where(Function(x) x.Key = ordineCliNuovo.Agente).First.Value)
                    counter = counter + 1
                Next
                workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll()
                workbook.Write(ms1)
                Return File(ms1.ToArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeFile)
            End If
            'Return Json(New With {.tot = finalCosts, .anticipi = anticipi, .miss = totalMissing, .totRev = totalRevenue}, JsonRequestBehavior.AllowGet) '.tmpf = tmpF, .codList = codList, .tmp = dict,, .FD = fattureDrill, .CD = contiDrill
        End Function
        <Authorize(Roles:="Commerciale_Admin, Admin")>
        <HttpGet>
        Function OrdiniPerImportanza(dateTime As String) As FileResult
            Dim datetimeCalc = dateTime.Split("-")
            Dim OC As New List(Of OCPrioritaViewModel)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "Select ESECOD,ORCTSZ,ORCTNR,ORCCRP,ORCTSSREV,ORCDDCREV,CLFNMG from ORCTES00,CLFANA WHERE ORCSTC = '050' AND ORCTSZ = 'OC' AND ORCDDCREV >= '" + datetimeCalc(0) + "' AND ORCDDCREV <= '" + datetimeCalc(1) + "'  AND ORCTES00.ORCCLI = CLFANA.CLFCO1 AND CLFANA.CLFTIP = 'C'"
                myConn.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Dim o As New OCPrioritaViewModel With {
                        .Anno = myReader.GetString(0),
                        .codOrd = myReader.GetString(1),
                        .NumOrd = myReader.GetDecimal(2).ToString,
                        .Priorita = myReader.GetDecimal(3).ToString,
                        .DataRichConsegna = myReader.GetDecimal(4).ToString,
                        .DataInserimento = myReader.GetDecimal(5).ToString,
                        .Cliente = myReader.GetString(6)
                    }
                    OC.Add(o)

                Loop
                myConn.Close()

            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            'Apertura file
            Dim fs As New FileStream(Server.MapPath("\Content\Template\Template_Priorita.xlsx"), FileMode.Open, FileAccess.Read)
            Dim workbook As XSSFWorkbook = New XSSFWorkbook(fs)

            Dim ws As XSSFSheet = workbook.GetSheetAt(0)
            'Base data


            'Intestazione
            Dim c = 3
            For Each k In OC
                Try
                    Dim r As IRow = ws.CreateRow(c)
                    For j = 1 To 7
                        r.CreateCell(j)
                    Next
                    ws.GetRow(c).GetCell(1).SetCellValue(k.Anno)
                    ws.GetRow(c).GetCell(2).SetCellValue(k.codOrd)
                    ws.GetRow(c).GetCell(3).SetCellValue(k.NumOrd)
                    Select Case k.Priorita
                        Case "0"
                            ws.GetRow(c).GetCell(4).SetCellValue("Nessuna Priorita")
                        Case "1"
                            ws.GetRow(c).GetCell(4).SetCellValue("Priorita massima 1")
                        Case "2"
                            ws.GetRow(c).GetCell(4).SetCellValue("Priorita massima 2")
                        Case "3"
                            ws.GetRow(c).GetCell(4).SetCellValue("Priorita massima 3")
                        Case "4"
                            ws.GetRow(c).GetCell(4).SetCellValue("Priorita media 1")
                        Case "5"
                            ws.GetRow(c).GetCell(4).SetCellValue("Priorita media 2")
                        Case "6"
                            ws.GetRow(c).GetCell(4).SetCellValue("Priorita media 3")
                        Case "7"
                            ws.GetRow(c).GetCell(4).SetCellValue("Priorita minima 1")
                        Case "8"
                            ws.GetRow(c).GetCell(4).SetCellValue("Priorita minima 2")
                        Case "9"
                            ws.GetRow(c).GetCell(4).SetCellValue("Priorita minima 3")

                    End Select
                    ws.GetRow(c).GetCell(5).SetCellValue(k.DataRichConsegna.ToString.Substring(0, 4) + "/" + k.DataRichConsegna.ToString.Substring(4, 2) + "/" + k.DataRichConsegna.ToString.Substring(6, 2))
                    ws.GetRow(c).GetCell(6).SetCellValue(k.DataInserimento.ToString.Substring(0, 4) + "/" + k.DataInserimento.ToString.Substring(4, 2) + "/" + k.DataInserimento.ToString.Substring(6, 2))
                    ws.GetRow(c).GetCell(7).SetCellValue(k.cliente)
                    c = c + 1
                Catch ex As Exception

                End Try

            Next
            'Start Pop
            Dim baserow As IRow = ws.GetRow(0)
            'Dim baserow As IRow = ws.GetRow(2)
            Dim ms As New MemoryStream
            Dim ms1 As New MemoryStream
            'Riga Intestazione
            'ms1 = ms
            Dim nomeFile = "OCPriorita" + dateTime + ".xlsx"
            workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll()
            workbook.Write(ms1)
            Return File(ms1.ToArray, "application/vnd.ms-excel", nomeFile)

            'Return Json(New With {.tot = finalCosts, .anticipi = anticipi, .miss = totalMissing, .totRev = totalRevenue}, JsonRequestBehavior.AllowGet) '.tmpf = tmpF, .codList = codList, .tmp = dict,, .FD = fattureDrill, .CD = contiDrill
        End Function
        <Authorize(Roles:="Commerciale_Admin, Admin")>
        <HttpGet>
        Function Par(id) As JsonResult
            Dim year = Convert.ToInt32(DateTime.Now.Year.ToString)
            Dim yrAgo = year - 1
            Dim yrsAgo = year - 2
            Dim costiOggi As New StoricoViewModel With {.Year = year}
            Dim costiAnnoScorso As New StoricoViewModel With {.Year = yrAgo}
            Dim costiDueAnniFa As New StoricoViewModel With {.Year = yrsAgo}
            Dim div1 = 0
            Dim div2 = 0
            Dim div3 = 0
            Dim costiOggi_0 = 0
            Dim costiOggi_1 = 0
            Dim costiOggi_2 = 0
            Dim costiOggi_3 = 0
            Dim costiOggi_4 = 0
            Dim costiOggi_5 = 0
            Dim costiAnnoScorso_0 = 0
            Dim costiAnnoScorso_1 = 0
            Dim costiAnnoScorso_2 = 0
            Dim costiAnnoScorso_3 = 0
            Dim costiAnnoScorso_4 = 0
            Dim costiAnnoScorso_5 = 0
            Dim costiDueAnniFa_0 = 0
            Dim costiDueAnniFa_1 = 0
            Dim costiDueAnniFa_2 = 0
            Dim costiDueAnniFa_3 = 0
            Dim costiDueAnniFa_4 = 0
            Dim costiDueAnniFa_5 = 0

            Dim Labels As New List(Of String)
            Dim DataTot As New List(Of Double)
            Dim DataMat As New List(Of Double)
            Dim DataMacchina As New List(Of Double)
            Dim DataManoEst As New List(Of Double)
            Dim DataAtt As New List(Of Double)
            Dim DataManoInt As New List(Of Double)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "SELECT COSDATREV,MAX(COSTOT),MAX(COSD01),MAX(COSD02),MAX(COSD03),MAX(COSD04),MAX(COSD05) from COSART00 
                                     WHERE  COSDATREV >= '" + yrsAgo.ToString + "0101' AND COSDATREV <= '" + year.ToString + "1231' AND ARTCOD = '" + id(0).ToString + "' AND COSTCS = 'U' GROUP BY COSDATREV"
                myConn.Open()
            Catch ex As Exception

            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Dim y = myReader.GetDecimal(0).ToString.Substring(0, 4)
                    Select Case y
                        Case year.ToString
                            If myReader.GetDecimal(1) <> 0 Then
                                costiOggi.Costo_Globale = costiOggi.Costo_Globale + myReader.GetDecimal(1)
                                costiOggi_0 = costiOggi_0 + 1
                            End If
                            If myReader.GetDecimal(2) <> 0 Then
                                costiOggi.Costo_Materiali = costiOggi.Costo_Materiali + myReader.GetDecimal(2)
                                costiOggi_1 = costiOggi_1 + 1
                            End If
                            If myReader.GetDecimal(3) <> 0 Then
                                costiOggi.Costo_Macchina = costiOggi.Costo_Macchina + myReader.GetDecimal(3)
                                costiOggi_2 = costiOggi_2 + 1
                            End If
                            If myReader.GetDecimal(4) <> 0 Then
                                costiOggi.Costo_Manodopera_Est = costiOggi.Costo_Manodopera_Est + myReader.GetDecimal(4)
                                costiOggi_3 = costiOggi_3 + 1

                            End If
                            If myReader.GetDecimal(5) <> 0 Then
                                costiOggi.Costo_Attrezzaggio = costiOggi.Costo_Attrezzaggio + myReader.GetDecimal(5)
                                costiOggi_4 = costiOggi_4 + 1

                            End If
                            If myReader.GetDecimal(6) <> 0 Then
                                costiOggi.Costo_Manodopera_Int = costiOggi.Costo_Manodopera_Int + myReader.GetDecimal(6)
                                costiOggi_5 = costiOggi_5 + 1

                            End If
                            div1 = div1 + 1
                        Case yrAgo.ToString
                            If myReader.GetDecimal(1) <> 0 Then
                                costiAnnoScorso.Costo_Globale = costiAnnoScorso.Costo_Globale + myReader.GetDecimal(1)
                                costiAnnoScorso_0 = costiAnnoScorso_0 + 1
                            End If
                            If myReader.GetDecimal(2) <> 0 Then
                                costiAnnoScorso.Costo_Materiali = costiAnnoScorso.Costo_Materiali + myReader.GetDecimal(2)
                                costiAnnoScorso_1 = costiAnnoScorso_1 + 1
                            End If
                            If myReader.GetDecimal(3) <> 0 Then
                                costiAnnoScorso.Costo_Macchina = costiAnnoScorso.Costo_Macchina + myReader.GetDecimal(3)
                                costiAnnoScorso_2 = costiAnnoScorso_2 + 1

                            End If
                            If myReader.GetDecimal(4) <> 0 Then
                                costiAnnoScorso.Costo_Manodopera_Est = costiAnnoScorso.Costo_Manodopera_Est + myReader.GetDecimal(4)
                                costiAnnoScorso_3 = costiAnnoScorso_3 + 1

                            End If
                            If myReader.GetDecimal(5) <> 0 Then
                                costiAnnoScorso.Costo_Attrezzaggio = costiAnnoScorso.Costo_Attrezzaggio + myReader.GetDecimal(5)
                                costiAnnoScorso_4 = costiAnnoScorso_4 + 1

                            End If
                            If myReader.GetDecimal(6) <> 0 Then
                                costiAnnoScorso.Costo_Manodopera_Int = costiAnnoScorso.Costo_Manodopera_Int + myReader.GetDecimal(6)
                                costiAnnoScorso_5 = costiAnnoScorso_5 + 1

                            End If
                            div2 = div2 + 1

                        Case yrsAgo.ToString
                            If myReader.GetDecimal(1) <> 0 Then
                                costiDueAnniFa.Costo_Globale = costiDueAnniFa.Costo_Globale + myReader.GetDecimal(1)
                                costiDueAnniFa_0 = costiDueAnniFa_0 + 1
                            End If
                            If myReader.GetDecimal(2) <> 0 Then
                                costiDueAnniFa.Costo_Materiali = costiDueAnniFa.Costo_Materiali + myReader.GetDecimal(2)
                                costiDueAnniFa_1 = costiDueAnniFa_1 + 1
                            End If
                            If myReader.GetDecimal(3) <> 0 Then
                                costiDueAnniFa.Costo_Macchina = costiDueAnniFa.Costo_Macchina + myReader.GetDecimal(3)
                                costiDueAnniFa_2 = costiDueAnniFa_2 + 1

                            End If
                            If myReader.GetDecimal(4) <> 0 Then
                                costiDueAnniFa.Costo_Manodopera_Est = costiDueAnniFa.Costo_Manodopera_Est + myReader.GetDecimal(4)
                                costiDueAnniFa_3 = costiDueAnniFa_3 + 1

                            End If
                            If myReader.GetDecimal(5) <> 0 Then
                                costiDueAnniFa.Costo_Attrezzaggio = costiDueAnniFa.Costo_Attrezzaggio + myReader.GetDecimal(5)
                                costiDueAnniFa_4 = costiDueAnniFa_4 + 1

                            End If
                            If myReader.GetDecimal(6) <> 0 Then
                                costiDueAnniFa.Costo_Manodopera_Int = costiDueAnniFa.Costo_Manodopera_Int + myReader.GetDecimal(6)
                                costiDueAnniFa_5 = costiDueAnniFa_5 + 1

                            End If
                            div3 = div3 + 1

                    End Select
                Loop
                myConn.Close()
                If div1 <> 0 Then
                    If costiOggi.Costo_Globale <> 0 Then
                        costiOggi.Costo_Globale = Math.Round(Val(costiOggi.Costo_Globale / costiOggi_0), 2)
                    End If
                    If costiOggi.Costo_Materiali <> 0 Then
                        costiOggi.Costo_Materiali = Math.Round(Val(costiOggi.Costo_Materiali / costiOggi_1), 2)
                    End If
                    If costiOggi.Costo_Macchina <> 0 Then
                        costiOggi.Costo_Macchina = Math.Round(Val(costiOggi.Costo_Macchina / costiOggi_2), 2)
                    End If
                    If costiOggi.Costo_Manodopera_Est <> 0 Then
                        costiOggi.Costo_Manodopera_Est = Math.Round(Val(costiOggi.Costo_Manodopera_Est / costiOggi_3), 2)
                    End If
                    If costiOggi.Costo_Attrezzaggio <> 0 Then
                        costiOggi.Costo_Attrezzaggio = Math.Round(Val(costiOggi.Costo_Attrezzaggio / costiOggi_4), 2)
                    End If
                    If costiOggi.Costo_Manodopera_Int <> 0 Then
                        costiOggi.Costo_Manodopera_Int = Math.Round(Val(costiOggi.Costo_Manodopera_Int / costiOggi_5), 2)
                    End If
                End If
                If div2 <> 0 Then
                    If costiAnnoScorso.Costo_Globale <> 0 Then
                        costiAnnoScorso.Costo_Globale = Math.Round(Val(costiAnnoScorso.Costo_Globale / costiAnnoScorso_0), 2)
                    End If
                    If costiAnnoScorso.Costo_Materiali <> 0 Then
                        costiAnnoScorso.Costo_Materiali = Math.Round(Val(costiAnnoScorso.Costo_Materiali / costiAnnoScorso_1), 2)
                    End If
                    If costiAnnoScorso.Costo_Macchina <> 0 Then
                        costiAnnoScorso.Costo_Macchina = Math.Round(Val(costiAnnoScorso.Costo_Macchina / costiAnnoScorso_2), 2)
                    End If
                    If costiAnnoScorso.Costo_Manodopera_Est <> 0 Then
                        costiAnnoScorso.Costo_Manodopera_Est = Math.Round(Val(costiAnnoScorso.Costo_Manodopera_Est / costiAnnoScorso_3), 2)
                    End If
                    If costiAnnoScorso.Costo_Attrezzaggio <> 0 Then
                        costiAnnoScorso.Costo_Attrezzaggio = Math.Round(Val(costiAnnoScorso.Costo_Attrezzaggio / costiAnnoScorso_4), 2)
                    End If
                    If costiAnnoScorso.Costo_Manodopera_Int <> 0 Then
                        costiAnnoScorso.Costo_Manodopera_Int = Math.Round(Val(costiAnnoScorso.Costo_Manodopera_Int / costiAnnoScorso_5), 2)
                    End If
                End If


                If div3 <> 0 Then
                    If costiDueAnniFa.Costo_Globale <> 0 Then
                        costiDueAnniFa.Costo_Globale = Math.Round(Val(costiDueAnniFa.Costo_Globale / costiDueAnniFa_0), 2)
                    End If
                    If costiDueAnniFa.Costo_Materiali <> 0 Then
                        costiDueAnniFa.Costo_Materiali = Math.Round(Val(costiDueAnniFa.Costo_Materiali / costiDueAnniFa_1), 2)
                    End If
                    If costiDueAnniFa.Costo_Macchina <> 0 Then
                        costiDueAnniFa.Costo_Macchina = Math.Round(Val(costiDueAnniFa.Costo_Macchina / costiDueAnniFa_2), 2)
                    End If
                    If costiDueAnniFa.Costo_Manodopera_Est <> 0 Then
                        costiDueAnniFa.Costo_Manodopera_Est = Math.Round(Val(costiDueAnniFa.Costo_Manodopera_Est / costiDueAnniFa_3), 2)
                    End If
                    If costiDueAnniFa.Costo_Attrezzaggio <> 0 Then
                        costiDueAnniFa.Costo_Attrezzaggio = Math.Round(Val(costiDueAnniFa.Costo_Attrezzaggio / costiDueAnniFa_4), 2)
                    End If
                    If costiDueAnniFa.Costo_Manodopera_Int <> 0 Then
                        costiDueAnniFa.Costo_Manodopera_Int = Math.Round(Val(costiDueAnniFa.Costo_Manodopera_Int / costiDueAnniFa_5), 2)
                    End If
                End If

            Catch ex As Exception

            End Try

            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "SELECT DISTINCT COSDATREV, MAX(ARTCOD),MAX(COSTOT),MAX(COSD01),MAX(COSD02),MAX(COSD03),MAX(COSD04),MAX(COSD05) from COSART00
                                     WHERE  COSDATREV >= '" + yrsAgo.ToString + "0101' AND COSDATREV <= '" + year.ToString + "1231' AND ARTCOD = '" + id(0).ToString + "' AND COSTCS = 'U'  GROUP BY COSDATREV"
                myConn.Open()
            Catch ex As Exception

            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Labels.Add(DateTime.ParseExact(myReader.GetDecimal(0).ToString, "yyyyMMdd", Nothing).ToString.Split(" ")(0))
                    DataTot.Add(myReader.GetDecimal(2))
                    DataMat.Add(myReader.GetDecimal(3))
                    DataMacchina.Add(myReader.GetDecimal(4))
                    DataManoEst.Add(myReader.GetDecimal(5))
                    DataAtt.Add(myReader.GetDecimal(6))
                    DataManoInt.Add(myReader.GetDecimal(7))
                Loop
                myConn.Close()
            Catch ex As Exception

            End Try
            costiOggi.Manodopera = costiOggi.Costo_Manodopera_Int + costiOggi.Costo_Macchina + costiOggi.Costo_Attrezzaggio
            costiAnnoScorso.Manodopera = costiAnnoScorso.Costo_Manodopera_Int + costiAnnoScorso.Costo_Macchina + costiAnnoScorso.Costo_Attrezzaggio
            costiDueAnniFa.Manodopera = costiDueAnniFa.Costo_Manodopera_Int + costiDueAnniFa.Costo_Macchina + costiDueAnniFa.Costo_Attrezzaggio
            Return Json(New With {.id = id, .dataOggi = costiOggi, .dataAnnoScorso = costiAnnoScorso, .dataDueAnniFa = costiDueAnniFa, .Labels = Labels.ToArray, .DataTot = DataTot.ToArray, .DataMat = DataMat.ToArray, .DataMacchina = DataMacchina.ToArray, .DataManoEst = DataManoEst.ToArray, .DataAtt = DataAtt.ToArray, .DataManoInt = DataManoInt.ToArray}, JsonRequestBehavior.AllowGet)
        End Function
        Function TestRicorsivo(ByVal id As String) As ActionResult
            Dim stringProva = "111802-100"
            Dim s = New MasterCosti With {.CostoContoLavoro = 0, .CostoInterno_Macchina = 0, .CostoInterno_Mdo = 0, .CostoMateriali = 0}
            Dim a = RecursiveDistinta(New DBViewModel With {.Codice = id, .ListaArt = New List(Of DBViewModel)}, s)
            System.Console.WriteLine("await here")
            Return View("TestRicorsivo", a)
        End Function
        Function RecursiveDistinta(ByVal listaArticoliDaDistinta As DBViewModel, ByVal costiMaster As MasterCosti)
            Try
                myConn = New SqlConnection(ConnectionString)
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
                        myConn = New SqlConnection(ConnectionString)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "select CLFCOD,ORFMOV,AVG(FAQIFA) from FFODET00 where ARTCOD = '" + l.Codice + "' GROUP BY CLFCOD,ORFMOV"
                        myConn.Open()
                    Catch ex As Exception
                        Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
                    End Try
                    Try
                        myReader = myCmd.ExecuteReader

                        Do While myReader.Read()
                            Select Case myReader.GetString(1)
                                Case "EL1"
                                    Dim CO = myReader.GetDecimal(2)
                                    l.CostoContoLavoro = l.CostoContoLavoro + Math.Round(CO, 2)
                                Case "EA1"
                                    l.CostoMateriali = Math.Round(myReader.GetDecimal(2), 2)
                            End Select
                        Loop
                        myConn.Close()

                    Catch ex As Exception

                    End Try
                    '-----------------------------------------------------------------END CONTOLAVORO
                    '-----------------------------------------------------------------START LAV INTERNO
                    Try
                        myConn = New SqlConnection(ConnectionString)
                        myCmd = myConn.CreateCommand
                        myCmd.CommandText = "SELECT AVG(ODLLVR),AVG(ODLLVM) FROM ODLTES00,ODLMOP00 WHERE ODLALP = '" + l.Codice + "' AND ODLMOP00.ODLNMR =ODLTES00.ODLNMR AND ODLMOP00.ODLANN = ODLTES00.ODLANN AND ODLTES00.ODLANN = '2022' AND (ODLLVR > 0 OR ODLLVM > 0) GROUP BY ODLALP"
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
                        Try
                            l.CostoInterno_Mdo = Math.Round(l.CostoInterno_Mdo / 60, 2) * 30
                        Catch ex As Exception

                        End Try
                        Try
                            l.CostoInterno_Macchina = Math.Round(l.CostoInterno_Macchina / 60, 2) * 30
                        Catch ex As Exception

                        End Try
                        myConn.Close()

                    Catch ex As Exception

                    End Try
                    '-----------------------------------------------------------------END LAV INTERNO
                    '-----------------------------------------------------------------START LAV INTERNO
                    Try
                        myConn = New SqlConnection(ConnectionString)
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
                    costiMaster.CostoMateriali = costiMaster.CostoMateriali + l.CostoMateriali
                    costiMaster.CostoInterno_Mdo = costiMaster.CostoInterno_Mdo + l.CostoInterno_Mdo
                    costiMaster.CostoInterno_Macchina = costiMaster.CostoInterno_Macchina + l.CostoInterno_Macchina
                    costiMaster.CostoContoLavoro = costiMaster.CostoContoLavoro + l.CostoContoLavoro
                    l = RecursiveDistinta(l, costiMaster)
                Next
            Catch ex As Exception

            End Try
            Return listaArticoliDaDistinta
        End Function
        ' GET: Clienti
        <Authorize>
        <HttpGet>
        Function Commessa(OP As String) As JsonResult
            Dim opSplit = OP.Split("-")
            Dim CommRighe As New List(Of CommessaViewModel)
            Dim startDate As New DateTime(1970, 1, 1)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "SELECT TDMDATE, TDMEVEN, TDMUSER, TDMRIG,TDMQTY, ARTCOD FROM TABTDM00LG (NOLOCK)
                    WHERE ESECOD = '" + opSplit(0).ToString + "'
                    AND TDMSEZ = '" + opSplit(1).ToString + "'
                    AND TDMNRR = " + opSplit(2).ToString + "
                    ORDER BY 1, 4, 5 "
                myConn.Open()
            Catch ex As Exception
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader
                Do While myReader.Read()
                    Dim f As New CommessaViewModel With {
                        .Data = myReader.GetDateTime(0),
                        .DataString = myReader.GetDateTime(0).ToString().Split(" ")(0),
                        .Action = myReader.GetString(1).ToString.Replace("""", ""),
                        .Utente = myReader.GetString(2).ToString.Replace("""", ""),
                        .Qty = myReader.GetDecimal(4).ToString.Replace("""", ""),
                        .Art = myReader.GetString(5).ToString.Replace("""", ""),
                        .Timestamp = (.Data - startDate).TotalSeconds.ToString
                    }
                    CommRighe.Add(f)
                Loop
                myConn.Close()
            Catch ex As Exception
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Return Json(New With {.list = CommRighe, .OP = OP}, JsonRequestBehavior.AllowGet) '.tmpf = tmpF, .codList = codList, .tmp = dict,, .FD = fattureDrill, .CD = contiDrill

        End Function
        ' GET: OC To Produzione
        <Authorize>
        <HttpGet>
        Function TempiPerOC() As JsonResult
            Dim listOC As New List(Of OCTempiViewModel)
            Dim listOC_Final As New List(Of OCTempiViewModel)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "SELECT ESECOD, ORDSEZ, ORDNRR, ORDDATE,ORDSTC FROM ORCDET00LG (NOLOCK) WHERE (ORDSTC = '050' or ORDSTC = '010') AND ORDSEZ= 'OC' "
                myConn.Open()
            Catch ex As Exception
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    Dim f As New OCTempiViewModel With {
                        .OCOT = myReader.GetString(0) + myReader.GetString(1) + myReader.GetDecimal(2).ToString,
                        .Data_Inizio = myReader.GetDateTime(3),
                        .Stato = myReader.GetString(4)
                    }
                    listOC.Add(f)
                Loop
                myConn.Close()

                For Each oc In listOC
                    If listOC_Final.Where(Function(x) x.OCOT = oc.OCOT).Count > 0 Then
                        Dim l = listOC_Final.Where(Function(x) x.OCOT = oc.OCOT).First
                        If l.Stato = "050" And l.Data_Fine < l.Data_Inizio Then
                            l.Data_Fine = oc.Data_Inizio
                            l.Data_Diff = Weekdays(l.Data_Inizio, l.Data_Fine)
                        End If
                    Else
                        listOC_Final.Add(oc)
                    End If
                Next
                Dim a = listOC_Final.Where(Function(x) x.Data_Inizio < x.Data_Fine).ToList
                Console.WriteLine(a)
            Catch ex As Exception
                Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            'Return Json(New With {.list = CommRighe, .OP = OP}, JsonRequestBehavior.AllowGet) '.tmpf = tmpF, .codList = codList, .tmp = dict,, .FD = fattureDrill, .CD = contiDrill
        End Function
        <Authorize>
        Function StampaEtichetta(id As String, Optional fromMagazzino As Boolean = False) As ActionResult
            Dim art = ""
            Dim desc = ""
            If fromMagazzino Then
                Dim artMag = db.ArticoliMagazzino.Where(Function(x) x.codArticolo = id).First
                ViewBag.art = artMag.codArticolo
                ViewBag.desc = artMag.noteArticolo
                ViewBag.Qta = "Q.tà: " + artMag.qta.ToString
            Else
                Try
                    myConn = New SqlConnection(ConnectionString)
                    myCmd = myConn.CreateCommand
                    myCmd.CommandText = "SELECT AR.ARTCOD ARTICOLO , TP.ARTDES DESCRIZIONE FROM ARTDMA AS AR
                    INNER JOIN ARTANA AS TP ON TP.ARTCO1=AR.ARTCOD
                    WHERE ARTCOD= '" + id.ToString + "'
                    GROUP BY AR.ARTCOD, TP.ARTDES"
                    myConn.Open()
                Catch ex As Exception

                End Try
                Try
                    myReader = myCmd.ExecuteReader
                    Do While myReader.Read()
                        ViewBag.art = myReader.GetString(0).ToString()
                        ViewBag.desc = myReader.GetString(1).ToString()

                        '    results = results & myReader.GetString(0) & vbTab &
                        'myReader.GetString(1) & vbLf
                    Loop
                Catch ex As Exception

                End Try
            End If


            Return PartialView()
        End Function
        <Authorize>
        Function ComplessivoOrdinatoCalcolo(datetime As String) As JsonResult
            Dim datetimeCalc = datetime.Split("-")
            Dim dataInizio = datetimeCalc(0)
            Dim dataFine = datetimeCalc(1)
            Dim expenddt As Date = Date.ParseExact(dataInizio, "yyyyMMdd",
            System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Dim expenddtEnd As Date = Date.ParseExact(dataFine, "yyyyMMdd",
            System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Dim totale = 0
            Dim ListaPrev As New List(Of PrevOrdinatoViewModel)
            Dim conteggio = 0
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "
                  select DATEPART(week, cast(cast(ORDDCOREV as char(8)) as datetime)) AS Settimana,
                    SUM(ORDQOR*ORDPVA),
                    COUNT(*),
                    CONVERT(varchar(50), (DATEADD(dd, @@DATEFIRST - DATEPART(dw, cast(cast(ORDDCOREV as char(8)) as datetime)) + 7, cast(cast(ORDDCOREV as char(8)) as datetime))), 101),
                    CONVERT(varchar(50), (DATEADD(dd, @@DATEFIRST - DATEPART(dw, cast(cast(ORDDCOREV as char(8)) as datetime)) + 13, cast(cast(ORDDCOREV as char(8)) as datetime))), 101)
                    as Importo from ORCDET00 where ORDDCOREV >= '" + dataInizio + "' AND ORDDCOREV <= '" + dataFine + "' AND (ORDSEZ = 'OC' or  ORDSEZ = 'OT') and ORDSTC < '080'
                    GROUP BY DATEPART(week, cast(cast(ORDDCOREV as char(8)) as datetime)),cast(cast(ORDDCOREV as char(8)) as datetime) 
                    ORDER BY DATEPART(week, cast(cast(ORDDCOREV as char(8)) as datetime))"
                myConn.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    If ListaPrev.Where(Function(x) x.nSettimana = myReader.GetInt32(0)).Count > 0 Then
                        Dim LP = ListaPrev.Where(Function(x) x.nSettimana = myReader.GetInt32(0)).First
                        LP.Totale = LP.Totale + myReader.GetDecimal(1)
                        LP.Conteggio = LP.Conteggio + myReader.GetInt32(2)
                        ListaPrev.Remove(ListaPrev.Where(Function(x) x.nSettimana = myReader.GetInt32(0)).First)
                        ListaPrev.Add(LP)
                    Else
                        ListaPrev.Add(New PrevOrdinatoViewModel With {
                       .nSettimana = myReader.GetInt32(0),
                       .Totale = myReader.GetDecimal(1),
                       .Conteggio = myReader.GetInt32(2),
                       .DataInizio = myReader.GetString(3).Split("/")(1),
                       .DataFine = myReader.GetString(4).Split("/")(1) + "/" + myReader.GetString(4).Split("/")(0)
                   })
                    End If

                Loop
                myConn.Close()

            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Return Json(New With {.ok = True, .lista = ListaPrev}, JsonRequestBehavior.AllowGet)
        End Function
        <Authorize>
        Function FatturatoPage() As ActionResult
            Return PartialView()
        End Function
        <Authorize>
        Function FatturatoFornitoriPage() As ActionResult
            Return PartialView()
        End Function
        <Authorize>
        Function PrimaNotaPage() As ActionResult
            Return PartialView()
        End Function
        <Authorize>
        Function OrdinatoPage() As ActionResult
            Dim selectSlotList As New List(Of ClienteSmallViewModel)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "Select CLFCO1, CLFRSC from CLFANA where CLFTIP = 'C' order by CLFRSC"
                myConn.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    selectSlotList.Add(New ClienteSmallViewModel With {
                        .CodCliente = myReader.GetString(0),
                        .NomeCliente = myReader.GetString(1)
                    })

                Loop
                myConn.Close()

            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            ViewBag.clienti = New SelectList(selectSlotList, "CodCliente", "NomeCliente")

            Dim selectSlotListAgenti As New List(Of ClienteSmallViewModel)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "select PROAGE, PRORSC from PROANA00"
                myConn.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    selectSlotListAgenti.Add(New ClienteSmallViewModel With {
                        .CodCliente = myReader.GetString(0),
                        .NomeCliente = myReader.GetString(1)
                    })

                Loop
                myConn.Close()

            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            ViewBag.clienti = New SelectList(selectSlotList, "CodCliente", "NomeCliente")
            ViewBag.agenti = New SelectList(selectSlotListAgenti, "CodCliente", "NomeCliente")

            Return PartialView()
        End Function
        <Authorize>
        Function OffertoPage() As ActionResult
            Dim selectSlotList As New List(Of ClienteSmallViewModel)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "Select CLFCO1, CLFRSC from CLFANA where CLFTIP = 'C' order by CLFRSC"
                myConn.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    selectSlotList.Add(New ClienteSmallViewModel With {
                        .CodCliente = myReader.GetString(0),
                        .NomeCliente = myReader.GetString(1)
                    })

                Loop
                myConn.Close()

            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            ViewBag.clienti = New SelectList(selectSlotList, "CodCliente", "NomeCliente")

            Dim selectSlotListAgenti As New List(Of ClienteSmallViewModel)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "select PROAGE, PRORSC from PROANA00"
                myConn.Open()
            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            Try
                myReader = myCmd.ExecuteReader

                Do While myReader.Read()
                    selectSlotListAgenti.Add(New ClienteSmallViewModel With {
                        .CodCliente = myReader.GetString(0),
                        .NomeCliente = myReader.GetString(1)
                    })

                Loop
                myConn.Close()

            Catch ex As Exception
                'Return Json(New With {.ok = False, .message = "Errore: " + ex.Message + "."})
            End Try
            ViewBag.clienti = New SelectList(selectSlotList, "CodCliente", "NomeCliente")
            ViewBag.agenti = New SelectList(selectSlotListAgenti, "CodCliente", "NomeCliente")

            Return PartialView()
        End Function
        <Authorize>
        Function Gleason(AQ, MQ, Z2Q, Z1Q, S, Pos) As JsonResult
            Try
                Dim U1
                Dim K1
                Dim K2
                Dim TAB
                Dim A1
                Dim A2
                Dim A3
                Dim A4
                Dim A5
                Dim A6
                Dim A7
                Dim A8
                Dim Pr
                Dim H1
                Dim H2
                Dim D1
                Dim D2
                Dim D3
                Dim D4
                Dim B
                Dim C1
                Dim C2
                Dim E1
                Dim E2
                Dim F1
                Dim F2
                Dim S1
                Dim S2
                Dim S3
                Dim S4
                Dim R1
                Dim R2
                Dim R3
                Dim R4
                Dim T1
                Dim T2
                Dim T3
                Dim T4
                Dim T5
                Dim T6
                Dim T7
                Dim T8
                Dim Q1
                Dim Q2
                Dim Q3
                Dim Q4
                Dim V1
                Dim V2
                Dim V3
                Dim V4
                Dim EsempioCalcolo
                TAB = S
                Dim A = Convert.ToDecimal(AQ(0))
                Dim M = Convert.ToDecimal(MQ(0))
                Dim Z1 = Convert.ToDecimal(Z1Q(0))
                Dim Z2 = Convert.ToDecimal(Z2Q(0))
                If A > 90 Then
                    A = A - 90
                End If
                Dim ARad = (A * Math.PI) / 180
                Dim ARad180 = ((180 - A) * Math.PI) / 180
                Dim cosA = Math.Cos(ARad)
                Dim cosA180 = Math.Cos(180 - A)
                Dim sinA = Math.Sin(ARad)
                Dim sinA180 = Math.Sin(180 - A)
                sinA180 = (sinA180 * 180) / Math.PI
                cosA180 = (cosA180 * 180) / Math.PI
                Dim A2Rad
                Dim A1Rad
                If S(0) = "DX" Then
                    K1 = "DX"
                    K2 = "SX"
                ElseIf S(0) = "SX" Then
                    K1 = "SX"
                    K2 = "DX"
                End If

                If A = 90 Then
                    A1 = Math.Round((Int(Math.Atan(Z1 / Z2) * 10000000) / 10000000), 6)
                    A1Rad = (A1 * 180) / Math.PI
                    A2Rad = (A2 * 180) / Math.PI
                    A2 = (90 - A1Rad)
                ElseIf (A < 90) Then
                    Dim div = Z2 / Z1
                    If Pos(0) = "meno" Then
                        A1 = Math.Round((Int(Math.Atan(sinA / (div - cosA)) * 10000000) / 10000000), 6)
                    ElseIf Pos(0) = "piu" Then
                        A1 = Math.Round((Int(Math.Atan(sinA / (div + cosA)) * 10000000) / 10000000), 6)
                    End If
                    A1Rad = (A1 * 180) / Math.PI
                    A2Rad = (A2 * 180) / Math.PI
                    A2 = (A - A1Rad)
                ElseIf (A > 90) Then
                    Dim div = Z2 / Z1
                    A1 = Math.Round((Int(Math.Atan(sinA180 / (div - cosA180)) * 10000000) / 10000000), 6)
                    A1Rad = (A1 * 180) / Math.PI
                    A2Rad = (A2 * 180) / Math.PI
                    A2 = (A - A1Rad)
                End If
                H1 = (1.7 * M)
                H2 = (1.888 * M)
                D1 = (Z1 * M)
                D2 = (Z2 * M)
                B = (Int((D2 / (2 * Math.Sin(A2)) * 1000)) / 1000)
                C2 = Math.Round((Int(M * (0.46 + 0.39 * (Z1 / Z2) ^ 2) * 10000000) / 10000000), 6)
                C1 = (H1 - C2)
                E1 = (H2 - C1)
                E2 = (H2 - C2)
                'sT7 = ((T7 * 180) / Math.PI)
                Dim temp = A1 / Math.PI
                A1 = temp * 180
                Dim gradTMP = (Math.Round(A1, 3))
                Dim TMP = (gradTMP / 180) * Math.PI
                Dim TMPUpper = D1 / 2
                Try
                    EsempioCalcolo = TMPUpper / (Math.Sin(TMP))
                    Console.WriteLine(EsempioCalcolo)
                Catch ex As Exception

                End Try
                A3 = (Math.Atan(E1 / EsempioCalcolo))
                A4 = (Math.Atan(E2 / EsempioCalcolo))
                A5 = (A1 + A4)
                A6 = (A2 + A3)
                A7 = (A1 - A3)
                A8 = (A2 - A4)
                D3 = Math.Round((Int((D1 + 2 * C1 * Math.Cos((A1 * Math.PI) / 180)) * 10000000) / 10000000), 6)
                D4 = Math.Round((Int((D2 + 2 * C2 * Math.Cos((A2 * Math.PI) / 180)) * 10000000) / 10000000), 6)


                If A = 90 Then
                    F1 = Math.Round((Int((D2 / 2 - C1 * Math.Sin(A1Rad)) * 10000000) / 10000000), 6)
                    F2 = Math.Round((Int((D1 / 2 - C2 * Math.Sin(A2Rad)) * 10000000) / 10000000), 6)
                ElseIf A < 90 Then
                    F1 = Math.Round((Int((EsempioCalcolo * Math.Cos(A1Rad) - C1 * Math.Sin(A1Rad)) * 10000000) / 10000000), 6)
                    F2 = Math.Round((Int((EsempioCalcolo * Math.Cos(A2Rad) - C2 * Math.Sin(A2Rad)) * 10000000) / 10000000), 6)
                ElseIf (A > 90) Then
                    F1 = Math.Round((Int((EsempioCalcolo * Math.Cos(A1Rad) - C1 * Math.Sin(A1Rad)) * 10000000) / 10000000), 6)
                    F2 = Math.Round((Int((EsempioCalcolo * Math.Cos(A2Rad) - C2 * Math.Sin(A2Rad)) * 10000000) / 10000000), 6)
                End If
                S1 = ((A7 - Int(A7)) * 60)
                S2 = (Int((S1 - Int(S1)) * 60))
                S3 = (Int(1000 * A7) / 1000)
                S4 = (Int(S1))
                R1 = ((A4 - Int(A4)) * 60)
                R2 = (Int((R1 - Int(R1)) * 60))
                R3 = (Int(1000 * A4) / 1000)
                R4 = (Int(R1))
                T1 = ((A3 - Int(A3)) * 60)
                T2 = (Int((T1 - Int(T1)) * 60))
                T3 = (Int(1000 * A3) / 1000)
                T4 = (Int(T1))
                T5 = ((A5 - Int(A5)) * 60)
                T6 = (Int((T5 - Int(T5)) * 60))
                T7 = (Int(1000 * A5) / 1000)
                T8 = (Int(T5))
                Q1 = ((A6 - Int(A6)) * 60)
                Q2 = (Int((Q1 - Int(Q1)) * 60))
                Q3 = (Int(1000 * A6) / 1000)
                Q4 = (Int(Q1))
                V1 = ((A8 - Int(A8)) * 60)
                V2 = (Int((V1 - Int(V1)) * 60))
                V3 = (Int(1000 * A8) / 1000)
                V4 = (Int(V1))

                'A1 = ((A1 * 180) / Math.PI)
                Dim t3tmp = T3 * 180
                T3 = t3tmp / 3.14
                Dim t7tmp = T7 * 180
                T7 = t7tmp / 3.14
                Dim arctandedendum = Math.Atan(E1 / EsempioCalcolo)
                Dim angoloDedendum = (arctandedendum / Math.PI) * 180
                Dim arctandedendumEsterno = Math.Atan(E2 / EsempioCalcolo)
                Dim angoloDedendumEsterno = (arctandedendumEsterno / Math.PI) * 180

                Dim arctandedendumEsternoCorona = Math.Atan(E2 / EsempioCalcolo)
                Dim angoloDedendumEsternoCorona = (arctandedendumEsternoCorona / Math.PI) * 180
                Dim tablePign = "<table class='table'><thead><tr><th>Nome Campo</th><th>Valore</th></tr></thead><tbody>"
                tablePign = tablePign + "<tr><td>" + "Angolo Fra Gli assi" + "</td><td>" + A.ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Angolo di Pressione" + "</td><td>20°</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Angolo Elica" + "</td><td>35°</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Senso elica" + "</td><td>" + K1.ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Lunghezza generatrice" + "</td><td>" + Math.Round(EsempioCalcolo, 3).ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Modulo" + "</td><td>" + M.ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Nr denti" + "</td><td>" + Z1.ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Diam. Primitivo" + "</td><td>" + D1.ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Diam. Esterno" + "</td><td>" + Math.Round(D3, 3).ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Addendum" + "</td><td>" + Math.Round(C1, 3).ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Dedendum" + "</td><td>" + Math.Round(E1, 3).ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Angolo Prim." + "</td><td>" + Math.Round(A1, 3).ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Angolo Est." + "</td><td>" + Math.Round(angoloDedendumEsterno + A1, 3).ToString + "</td></tr>"
                tablePign = tablePign + "<tr><td>" + "Angolo Dedendum" + "</td><td>" + Math.Round(angoloDedendum, 3).ToString + "</td></tr>"
                tablePign = tablePign + "</tbody></table>"
                Dim tableCor = "<table class='table'><thead><tr><th>Nome Campo</th><th>Valore</th></tr></thead><tbody>"
                tableCor = tableCor + "<tr><td>" + "Angolo Fra Gli assi" + "</td><td>" + A.ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Angolo di Pressione" + "</td><td>20°</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Angolo Elica" + "</td><td>35°</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Senso elica" + "</td><td>" + K2.ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Lunghezza generatrice" + "</td><td>" + Math.Round(EsempioCalcolo, 3).ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Modulo" + "</td><td>" + M.ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Nr denti" + "</td><td>" + Z2.ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Diam. Primitivo" + "</td><td>" + D2.ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Diam. Esterno" + "</td><td>" + Math.Round(D4, 3).ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Addendum" + "</td><td>" + Math.Round(C2, 3).ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Dedendum" + "</td><td>" + Math.Round(E2, 3).ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Angolo Prim." + "</td><td>" + Math.Round(A2, 3).ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Angolo Est." + "</td><td>" + Math.Round(angoloDedendum + A2, 3).ToString + "</td></tr>"
                tableCor = tableCor + "<tr><td>" + "Angolo Dedendum" + "</td><td>" + Math.Round(angoloDedendumEsternoCorona, 3).ToString + "</td></tr>"
                tableCor = tableCor + "</tbody></table>"
                Dim objPign As New With {
                  .AngoloFraGliAssi = A.ToString,
                  .AngoloDiPressione = "20°",
                  .AngoloElica = "35°",
                  .SensoDellElica = K1.ToString,
                  .LunghezzaGenEst = Math.Round(EsempioCalcolo, 3).ToString,
                  .Modulo = M,
                  .NDenti = Z1,
                  .DiametroPrim = D1.ToString,
                  .DiametroEst = D3.ToString,
                  .Add = Math.Round(C1, 3).ToString,
                  .Ded = Math.Round(E1, 3).ToString,
                  .AngoloPrim = Math.Round(A1, 3).ToString,
                  .AngoloEst = Math.Round(angoloDedendumEsterno + A1, 3).ToString,
                  .AngoloDed = Math.Round(angoloDedendum, 3).ToString
                }
                Dim objCoron As New With {
                  .AngoloFraGliAssi = A.ToString,
                  .AngoloDiPressione = "20°",
                  .AngoloElica = "35°",
                  .SensoDellElica = K2.ToString,
                  .LunghezzaGenEst = Math.Round(EsempioCalcolo, 3).ToString,
                  .Modulo = M,
                  .NDenti = Z2,
                  .DiametroPrim = D2.ToString,
                  .DiametroEst = D4.ToString,
                  .Add = Math.Round(C2, 3).ToString,
                  .Ded = Math.Round(E2, 3).ToString,
                  .AngoloPrim = Math.Round(A2, 3).ToString,
                  .AngoloEst = Math.Round(angoloDedendum + A2, 3).ToString,
                  .AngoloDed = Math.Round(angoloDedendumEsternoCorona, 3).ToString
                }
                Return Json(New With {.ok = True, .message = "Calcolo Gleason effettuato correttamente", .pignone = objPign, .corona = objCoron, .tablePignone = tablePign, .tableCoron = tableCor}, JsonRequestBehavior.AllowGet)
            Catch ex As Exception
                Return Json(New With {.ok = False, .message = "Errore calcolo Gleason"}, JsonRequestBehavior.AllowGet)
            End Try
        End Function
        <Authorize>
        Function AnalisiGleason() As ActionResult
            Return PartialView()
        End Function
        <Authorize>
        Function ComplessivoOrdinato() As ActionResult
            Return PartialView()
        End Function
        <Authorize>
        Function PrioritaPage() As ActionResult
            Return PartialView()
        End Function
        <Authorize>
        Function CommessaPage() As ActionResult
            Return View()
        End Function
        <Authorize>
        Function Tempi() As ActionResult
            Return View()
        End Function
        <Authorize>
        Function Index() As ActionResult
            'Create ADO.NET objects.
            Dim results = ""
            Dim listOfPezzi As New List(Of PezzoViewModel)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                If User.IsInRole("Produzione") Then
                    myCmd.CommandText = "SELECT AR.ARTCOD ARTICOLO , TP.ARTDES DESCRIZIONE , AR.ARTGIA GIACENZA, TP.TPRCOD TP, AC.ARTCUL ULTIMO,  AC.ARTSTD STANDARD,  AC.ARTCUL - AC.ARTSTD AS DIFFERENZA FROM ARTDMA AS AR
                    INNER JOIN ARTCOS AS AC ON AR.ARTCOD=AC.ARTCOD
                    INNER JOIN ARTANA AS TP ON TP.ARTCO1=AR.ARTCOD
                    GROUP BY AR.ARTCOD, TP.ARTDES, AR.ARTGIA, TP.TPRCOD, AC.ARTCUL, AC.ARTSTD
                    ORDER BY GIACENZA DESC"
                Else
                    myCmd.CommandText = "SELECT AR.ARTCOD ARTICOLO , TP.ARTDES DESCRIZIONE , AR.ARTGIA GIACENZA, TP.TPRCOD TP, AC.ARTCUL ULTIMO,  AC.ARTSTD STANDARD,  AC.ARTCUL - AC.ARTSTD AS DIFFERENZA FROM ARTDMA AS AR
                    INNER JOIN ARTCOS AS AC ON AR.ARTCOD=AC.ARTCOD
                    INNER JOIN ARTANA AS TP ON TP.ARTCO1=AR.ARTCOD
                    WHERE TPRCOD='TS' OR TPRCOD='TF'
                    GROUP BY AR.ARTCOD, TP.ARTDES, AR.ARTGIA, TP.TPRCOD, AC.ARTCUL, AC.ARTSTD
                    ORDER BY GIACENZA DESC"
                End If

                myConn.Open()
            Catch ex As Exception

            End Try
            Try
                myReader = myCmd.ExecuteReader
                Do While myReader.Read()
                    Dim cliente As New PezzoViewModel With {
                        .CodArt = myReader.GetString(0).ToString,
                        .Descrizione = myReader.GetString(1).ToString,
                        .Giacenza = myReader.GetDecimal(2).ToString,
                        .TPRCOD = myReader.GetString(3).ToString,
                        .ArtUltimo = myReader.GetDecimal(4).ToString,
                        .ArtStandard = myReader.GetDecimal(5).ToString,
                        .Diff = myReader.GetDecimal(6).ToString
                    }
                    listOfPezzi.Add(cliente)
                Loop
            Catch ex As Exception

            End Try

            Return View(listOfPezzi)
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
        Public Shared Function Weekdays(ByRef startDate As Date, ByRef endDate As Date) As Integer
            Dim numWeekdays As Integer
            Dim totalDays As Integer
            Dim WeekendDays As Integer
            numWeekdays = 0
            WeekendDays = 0
            totalDays = DateDiff(DateInterval.Day, startDate, endDate) + 1
            For i As Integer = 1 To totalDays
                If DatePart(DateInterval.Weekday, startDate) = 1 Then
                    WeekendDays = WeekendDays + 7
                End If
                If DatePart(DateInterval.Weekday, startDate) = 7 Then
                    WeekendDays = WeekendDays + 1
                End If
                startDate = DateAdd("d", 1, startDate)
            Next
            numWeekdays = totalDays - WeekendDays
            Return numWeekdays
        End Function
    End Class
End Namespace