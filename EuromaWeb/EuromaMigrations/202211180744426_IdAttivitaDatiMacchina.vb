Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class IdAttivitaDatiMacchina
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.DatiMacchina", "idAttività", Function(c) c.Int(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.DatiMacchina", "idAttività")
        End Sub
    End Class
End Namespace
