Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class AddedMacchinaFasi
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.FasiOC", "Macchina", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.FasiOC", "Macchina")
        End Sub
    End Class
End Namespace
