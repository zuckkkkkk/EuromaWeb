Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Overviewv21
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Overview", "Totale_Minuti_Uomo", Function(c) c.Int())
        End Sub

        Public Overrides Sub Down()
            DropColumn("dbo.Overview", "Totale_Minuti_Uomo", Function(c) c.Int())
        End Sub
    End Class
End Namespace
