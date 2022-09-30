Imports System.Web.Mvc
Imports System.Data.SqlClient
Imports EuromaWeb
Namespace Controllers
    Public Class ClientiController
        Inherits Controller

        Private Const ConnectionString As String = "Persist Security Info=True;Password=Irlandese6!;User ID=MattiaZucchini;Initial Catalog=ALNEUMA;Data Source=192.168.100.50"
        Private myConn As SqlConnection
        Private myCmd As SqlCommand
        Private myReader As SqlDataReader
        Private results As String

        ' GET: Clienti
        <Authorize>
        Function Index() As ActionResult
            'Create ADO.NET objects.
            Dim results = ""
            Dim listOfClienti As New List(Of ClienteViewModel)
            Dim listOfClientiFinal As New List(Of ClienteViewModel)
            Try
                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                myCmd.CommandText = "
              SELECT CL.CLFCOD, CLFNMG , FI.NAZCOD, FI.CLFPLC,  FI.CLFLC1, CL.CLFEMA, CL.CLFTEL, CL.CLFFAX,DW.DWCDA2,DW.DWCCA1, DW.DWCDRE,DW.DWCCR2,DW.DWCPK0 FROM CLIENTI AS CL
                INNER JOIN CLFANA AS AN ON CLFCOD=CLFCO1 AND AN.CLFTIP=CL.CLFTIP
                INNER JOIN CLFIND AS FI ON CL.CLFCOD=FI.CLFCOD AND CL.CLFTIP=FI.CLFTIP
                INNER JOIN DWDCLI00 AS DW ON CL.CLFCOD=DW.DWCCLI
                GROUP BY  CL.CLFCOD, CLFNMG , FI.NAZCOD, FI.CLFPLC,  FI.CLFLC1, CL.CLFEMA, CL.CLFTEL, CL.CLFFAX, DW.DWCDA2,DW.DWCCA1, DW.DWCDRE,DW.DWCCR2,DW.DWCPK0
			"
                myConn.Open()
            Catch ex As Exception

            End Try
            Try
                myReader = myCmd.ExecuteReader
                Do While myReader.Read()
                    Dim cliente As New ClienteViewModel With {
                        .Id = myReader.GetString(0),
                        .Nome1 = myReader.GetString(1),
                    .Nome2 = myReader.GetString(2),
                    .Provincia = myReader.GetString(3),
                    .Citta = myReader.GetString(4),
                    .Email = myReader.GetString(5),
                    .Tel = myReader.GetString(6),
                    .Fax = myReader.GetString(7),
                    .Divisione = myReader.GetString(8),
                    .Agente = myReader.GetString(9),
                    .HaFatturato = False,
                    .Regione = myReader.GetString(10),
                    .RaggruppamentoDue = myReader.GetString(11),
                    .AlnusCode = myReader.GetDecimal(12).ToString
                    }
                    listOfClienti.Add(cliente)
                    '    results = results & myReader.GetString(0) & vbTab &
                    'myReader.GetString(1) & vbLf
                Loop
                myConn.Close()

            Catch ex As Exception

            End Try
            Try

                myConn = New SqlConnection(ConnectionString)
                myCmd = myConn.CreateCommand
                'Per gli ordini eseguiti dal primo gennaio 2022 in poi
                myCmd.CommandText = "SELECT DISTINCT DWFCK0 FROM DWFFA100 WHERE DWFDTCREV > 20210101"

                'Per tutti gli ordini indistintamente dalla data
                'myCmd.CommandText = "SELECT DWFCK0 FROM DWFFA100" 
                myConn.Open()
                myReader = myCmd.ExecuteReader
                Do While myReader.Read()
                    Dim codcli = myReader.GetDecimal(0).ToString
                    'If codcli.Length < 6 Then
                    '    For i = 1 To 6 - codcli.Length
                    '        codcli = "0" & codcli
                    '    Next
                    'End If
                    Dim cli = listOfClienti.Where(Function(x) x.AlnusCode.ToString = codcli).FirstOrDefault
                    If Not IsNothing(cli) Then
                        listOfClientiFinal.Add(cli)
                        cli.HaFatturato = True
                    End If

                Loop
                myConn.Close()
            Catch ex As Exception

            End Try
            Return View(listOfClientiFinal)
        End Function
    End Class
End Namespace