Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class CostoMaggiorato
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AccettazioneUC", "CostoMaggiorato", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.AccettazioneUC", "CostoMaggiorato")
        End Sub
    End Class
End Namespace
