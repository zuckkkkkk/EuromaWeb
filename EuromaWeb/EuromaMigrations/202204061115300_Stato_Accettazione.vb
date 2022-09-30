Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Stato_Accettazione
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.AccettazioneUC", "Accettato", Function(c) c.Byte(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.AccettazioneUC", "Accettato", Function(c) c.Boolean(nullable := False))
        End Sub
    End Class
End Namespace
