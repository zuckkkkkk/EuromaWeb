Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Init5
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ProgettiUT", "Data", Function(c) c.DateTime())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.ProgettiUT", "Data")
        End Sub
    End Class
End Namespace
