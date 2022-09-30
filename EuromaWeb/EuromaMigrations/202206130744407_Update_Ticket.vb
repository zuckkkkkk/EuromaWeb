Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Update_Ticket
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.HelpDesk", "Body_Risposta", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.HelpDesk", "Body_Risposta")
        End Sub
    End Class
End Namespace
