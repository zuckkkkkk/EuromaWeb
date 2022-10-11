Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace IdentityMigrations
    Public Partial Class LicenzeEDispositivi_2
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.AspNetUserExchangeLicenseTable",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .IdEsternoUtente = c.String(),
                        .IdEsternoLicenza = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            AddColumn("dbo.AspNetUserLicenze", "QtaLicenze", Function(c) c.Int(nullable := False))
            DropColumn("dbo.AspNetUserLicenze", "IdEsterno")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.AspNetUserLicenze", "IdEsterno", Function(c) c.Int(nullable := False))
            DropColumn("dbo.AspNetUserLicenze", "QtaLicenze")
            DropTable("dbo.AspNetUserExchangeLicenseTable")
        End Sub
    End Class
End Namespace
