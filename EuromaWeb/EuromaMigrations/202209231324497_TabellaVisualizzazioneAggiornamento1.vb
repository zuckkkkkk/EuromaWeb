Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class TabellaVisualizzazioneAggiornamento1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.VisualizzazioneFileNota", "id_filenota", Function(c) c.Int(nullable := False))
            DropColumn("dbo.VisualizzazioneFileNota", "changeLog_id")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.VisualizzazioneFileNota", "changeLog_id", Function(c) c.Int(nullable := False))
            DropColumn("dbo.VisualizzazioneFileNota", "id_filenota")
        End Sub
    End Class
End Namespace
