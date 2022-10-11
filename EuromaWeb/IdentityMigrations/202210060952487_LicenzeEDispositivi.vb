Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace IdentityMigrations
    Public Partial Class LicenzeEDispositivi
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.AspNetUserDevice",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .IdRuolo = c.Int(nullable := False),
                        .NomeDispositivo = c.String(),
                        .DescrizioneDispositivo = c.String(),
                        .IPDispositivo = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.AspNetUserLicenze",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .IdEsterno = c.Int(nullable := False),
                        .TypeLicenza = c.Boolean(nullable := False),
                        .NomeLicenza = c.String(),
                        .DescrizioneLicenza = c.String(),
                        .DataInizio = c.DateTime(nullable := False),
                        .DataRinnovo = c.DateTime(nullable := False),
                        .DurataLicenza = c.Int(nullable := False),
                        .CostoLicenza = c.Double(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.AspNetUserLicenze")
            DropTable("dbo.AspNetUserDevice")
        End Sub
    End Class
End Namespace
