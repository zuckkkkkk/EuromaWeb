Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class DDT
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.LavorazioniEsterne", "Path_Doc", Function(c) c.String())
            AddColumn("dbo.LavorazioniEsterne", "Path_DDT", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.LavorazioniEsterne", "Path_DDT")
            DropColumn("dbo.LavorazioniEsterne", "Path_Doc")
        End Sub
    End Class
End Namespace
