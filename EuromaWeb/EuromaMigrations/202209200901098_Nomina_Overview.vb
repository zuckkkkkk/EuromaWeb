Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Nomina_Overview
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Overview", "Nomina", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Overview", "Nomina")
        End Sub
    End Class
End Namespace
