Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class TempiUT
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ProgettiUT", "DataPrevistaInizio", Function(c) c.DateTime())
            AddColumn("dbo.ProgettiUT", "DataPrevistaFine", Function(c) c.DateTime())
            AddColumn("dbo.ProgettiUT", "TempoTotale", Function(c) c.Int(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.ProgettiUT", "TempoTotale")
            DropColumn("dbo.ProgettiUT", "DataPrevistaFine")
            DropColumn("dbo.ProgettiUT", "DataPrevistaInizio")
        End Sub
    End Class
End Namespace
