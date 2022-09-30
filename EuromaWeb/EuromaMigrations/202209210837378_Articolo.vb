Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Articolo
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.OrdiniDiProduzione", "Articolo", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.OrdiniDiProduzione", "Articolo")
        End Sub
    End Class
End Namespace
