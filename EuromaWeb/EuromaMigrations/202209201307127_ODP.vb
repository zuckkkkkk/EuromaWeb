Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class ODP
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.OrdiniDiProduzione",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .OP = c.String(),
                        .Cartella = c.String(),
                        .OperatoreInsert = c.String(),
                        .IdOperatoreInsert = c.String(),
                        .DataCreazione = c.DateTime(nullable := False),
                        .Accettato = c.Byte(nullable := False),
                        .Priorita = c.Byte(nullable := False),
                        .DataRichiestaConsegna = c.DateTime(),
                        .tre_dim_Presente = c.Boolean(nullable := False),
                        .due_dim_Presente = c.Boolean(nullable := False),
                        .note_presenti = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.OrdiniDiProduzione")
        End Sub
    End Class
End Namespace
