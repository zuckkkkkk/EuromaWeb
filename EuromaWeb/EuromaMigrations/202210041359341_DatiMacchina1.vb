Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class DatiMacchina1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.DatiMacchina", "Data", Function(c) c.DateTime(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.DatiMacchina", "Data")
        End Sub
    End Class
End Namespace
