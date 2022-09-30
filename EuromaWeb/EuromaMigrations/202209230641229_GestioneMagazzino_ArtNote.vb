Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class GestioneMagazzino_ArtNote
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ArticoliMagazzino", "noteArticolo", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.ArticoliMagazzino", "noteArticolo")
        End Sub
    End Class
End Namespace
