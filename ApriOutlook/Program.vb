Imports System
Imports Microsoft.Office.Interop

Module Program
    Sub Main(args As String())
        Dim Outlook As Outlook.Application
        Dim Mail As Outlook.MailItem
        Dim Acc As Outlook.Account

        Outlook = New Outlook.Application()
        Mail = Outlook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem)
        Mail.To = "test@test.com"
        Mail.Subject = "Hello World!"

        'If you have multiple accounts you could change it in sender:
        For Each Acc In Outlook.Session.Accounts
            'Select first pop3 for instance.
            If Acc.AccountType = Microsoft.Office.Interop.Outlook.OlAccountType.olPop3 Then
                Mail.Sender = Acc
            End If
        Next

        'Take default account if no sender...
        'Attach files
        Mail.Attachments.Add("C:\Path\To\File.pdf")
        Mail.Attachments.Add("C:\Path\To\File1.pdf")

        'Append some text:
        Mail.HTMLBody &= "Hello World!"

        Mail.Display()

    End Sub
End Module
