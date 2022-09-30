Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class EmailInviataUC
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AccettazioneUC", "EmailInviata", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.AccettazioneUC", "EmailInviata")
        End Sub
    End Class
End Namespace
