Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace IdentityMigrations
    Public Partial Class Identity_Div
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AspNetProfiles", "MPA", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.AspNetProfiles", "Drill", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.AspNetProfiles", "CMT", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.AspNetProfiles", "ISA", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.AspNetProfiles", "UNI", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.AspNetProfiles", "UNI")
            DropColumn("dbo.AspNetProfiles", "ISA")
            DropColumn("dbo.AspNetProfiles", "CMT")
            DropColumn("dbo.AspNetProfiles", "Drill")
            DropColumn("dbo.AspNetProfiles", "MPA")
        End Sub
    End Class
End Namespace
