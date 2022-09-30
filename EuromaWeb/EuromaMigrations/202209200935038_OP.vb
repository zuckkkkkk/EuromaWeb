Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class OP
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Overview", "OP", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Overview", "OP")
        End Sub
    End Class
End Namespace
