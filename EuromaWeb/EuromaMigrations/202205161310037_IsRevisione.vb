Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class IsRevisione
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AccettazioneUC", "IsRevisione", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.AccettazioneUC", "IsRevisione")
        End Sub
    End Class
End Namespace
