Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class FasiOC_Articolo
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.FasiOC", "Articolo", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.FasiOC", "Articolo")
        End Sub
    End Class
End Namespace
