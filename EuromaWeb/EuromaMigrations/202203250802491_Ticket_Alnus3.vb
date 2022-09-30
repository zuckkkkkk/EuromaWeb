Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Ticket_Alnus3
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.HelpDesk", "Request_Type", Function(c) c.Byte(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.HelpDesk", "Request_Type")
        End Sub
    End Class
End Namespace
