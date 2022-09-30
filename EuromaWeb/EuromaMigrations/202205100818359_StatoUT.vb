Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class StatoUT
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.ProgettiUT", "StatoProgetto", Function(c) c.Byte(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.ProgettiUT", "StatoProgetto", Function(c) c.Int(nullable := False))
        End Sub
    End Class
End Namespace
