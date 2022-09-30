Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class PrioritaUpdate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AccettazioneUC", "Priorità", Function(c) c.Byte(nullable := False))
            AddColumn("dbo.ProgettiUT", "Priorità", Function(c) c.Byte(nullable := False))
            DropColumn("dbo.AccettazioneUC", "Priorita")
            DropColumn("dbo.ProgettiUT", "Priorita")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.ProgettiUT", "Priorita", Function(c) c.Byte(nullable := False))
            AddColumn("dbo.AccettazioneUC", "Priorita", Function(c) c.Byte(nullable := False))
            DropColumn("dbo.ProgettiUT", "Priorità")
            DropColumn("dbo.AccettazioneUC", "Priorità")
        End Sub
    End Class
End Namespace
