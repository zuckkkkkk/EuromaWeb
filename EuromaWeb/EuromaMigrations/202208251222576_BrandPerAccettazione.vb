Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class BrandPerAccettazione
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AccettazioneUC", "Brand", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.AccettazioneUC", "Brand")
        End Sub
    End Class
End Namespace
