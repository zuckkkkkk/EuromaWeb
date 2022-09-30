Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Overviewv22
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.Overview", "Totale_Ore_Uomo", Function(c) c.DateTime(nullable:=False))
        End Sub

        Public Overrides Sub Down()
            AlterColumn("dbo.Overview", "Totale_Ore_Uomo", Function(c) c.Int(nullable:=False))
        End Sub
    End Class
End Namespace
