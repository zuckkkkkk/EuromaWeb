Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class AccensioneSpegnimento
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Computer", "Ora_Spegnimento", Function(c) c.DateTime(nullable := False))
            AddColumn("dbo.Computer", "Ora_Accensione", Function(c) c.DateTime(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Computer", "Ora_Accensione")
            DropColumn("dbo.Computer", "Ora_Spegnimento")
        End Sub
    End Class
End Namespace
