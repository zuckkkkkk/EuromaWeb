Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Ticket_Alnus1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.HelpDesk", "Solved", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.HelpDesk", "Solved")
        End Sub
    End Class
End Namespace
