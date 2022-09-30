Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Overviewv2
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Overview", "Id_Opera", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Overview", "Id_Opera")
        End Sub
    End Class
End Namespace
