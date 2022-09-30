Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace IdentityMigrations
    Public Partial Class Init5
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AspNetProfiles", "NotificheViaMail", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.AspNetProfiles", "NotificheViaMail")
        End Sub
    End Class
End Namespace
