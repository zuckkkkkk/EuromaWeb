Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace IdentityMigrations
    Public Partial Class Path_ricerca
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AspNetProfiles", "Percorso_Ricerca", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.AspNetProfiles", "Percorso_Ricerca")
        End Sub
    End Class
End Namespace
