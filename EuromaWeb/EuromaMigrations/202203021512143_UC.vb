Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class UC
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AccettazioneUC", "File", Function(c) c.String())
            DropColumn("dbo.AccettazioneUC", "PercorsoFileNonConfermato")
            DropColumn("dbo.AccettazioneUC", "PercorsoFileConfermato")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.AccettazioneUC", "PercorsoFileConfermato", Function(c) c.String())
            AddColumn("dbo.AccettazioneUC", "PercorsoFileNonConfermato", Function(c) c.String())
            DropColumn("dbo.AccettazioneUC", "File")
        End Sub
    End Class
End Namespace
