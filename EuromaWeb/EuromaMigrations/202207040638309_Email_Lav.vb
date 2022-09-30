Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Email_Lav
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.LavorazioniEsterne", "Email", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.LavorazioniEsterne", "Email")
        End Sub
    End Class
End Namespace
