Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Check_4
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ProgettiUT", "Flag_4", Function(c) c.Byte(nullable := False))
            AlterColumn("dbo.ProgettiUT", "Flag_2", Function(c) c.Byte(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.ProgettiUT", "Flag_2", Function(c) c.Boolean(nullable := False))
            DropColumn("dbo.ProgettiUT", "Flag_4")
        End Sub
    End Class
End Namespace
