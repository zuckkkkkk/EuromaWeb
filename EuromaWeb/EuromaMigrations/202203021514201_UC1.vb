Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class UC1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.AccettazioneUC", "DataAccettazione", Function(c) c.DateTime())
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.AccettazioneUC", "DataAccettazione", Function(c) c.DateTime(nullable := False))
        End Sub
    End Class
End Namespace
