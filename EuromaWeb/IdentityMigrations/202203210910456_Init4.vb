Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace IdentityMigrations
    Public Partial Class Init4
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.AspNetUsers", "Profile_Id", Function(c) c.Int())
            CreateIndex("dbo.AspNetUsers", "Profile_Id")
            AddForeignKey("dbo.AspNetUsers", "Profile_Id", "dbo.AspNetProfiles", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.AspNetUsers", "Profile_Id", "dbo.AspNetProfiles")
            DropIndex("dbo.AspNetUsers", New String() { "Profile_Id" })
            DropColumn("dbo.AspNetUsers", "Profile_Id")
        End Sub
    End Class
End Namespace
