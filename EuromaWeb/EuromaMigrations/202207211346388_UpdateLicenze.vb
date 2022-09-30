Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class UpdateLicenze
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Licenze", "Utente_Collegato", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Licenze", "Utente_Collegato")
        End Sub
    End Class
End Namespace
