Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class DocumentiPerLicenze
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.DocumentiPerLicenze",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Nome_File = c.String(),
                        .Percorso_File = c.String(),
                        .DataCreazioneFile = c.DateTime(nullable := False),
                        .Operatore_Id = c.String(),
                        .Operatore_Nome = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.DocumentiPerLicenze")
        End Sub
    End Class
End Namespace
