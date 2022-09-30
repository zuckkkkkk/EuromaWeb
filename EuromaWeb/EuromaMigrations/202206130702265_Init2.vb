Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Init2
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.HelpDesk", "Stato_Ticket", Function(c) c.Byte(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.HelpDesk", "Stato_Ticket")
        End Sub
    End Class
End Namespace
