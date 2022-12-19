Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class StoricoGrezzi_2
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.StoricoGrezzi",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .IdArticolo = c.Int(nullable := False),
                        .QtaPrecedente = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .QtaAggiornata = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .UltimaModifica = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.StoricoGrezzi")
        End Sub
    End Class
End Namespace
