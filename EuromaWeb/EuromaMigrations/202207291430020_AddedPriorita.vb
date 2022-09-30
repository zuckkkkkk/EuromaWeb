Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class AddedPriorita
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AccettazioneUC", "Priorita", Function(c) c.Byte(nullable := False))
            AddColumn("dbo.AccettazioneUC", "DataRichiestaConsegna", Function(c) c.DateTime())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.AccettazioneUC", "DataRichiestaConsegna")
            DropColumn("dbo.AccettazioneUC", "Priorita")
        End Sub
    End Class
End Namespace
