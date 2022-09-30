Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class FasiOC
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.FasiOC",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .OC = c.String(),
                        .OP = c.String(),
                        .Fase = c.String(),
                        .Descrizione_Fase = c.String(),
                        .Qta_Da_Produrre = c.Int(nullable := False),
                        .Qta_Prodotta = c.Int(nullable := False),
                        .Tempo_Attivita = c.Double(nullable := False),
                        .Completata = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.FasiOC")
        End Sub
    End Class
End Namespace
