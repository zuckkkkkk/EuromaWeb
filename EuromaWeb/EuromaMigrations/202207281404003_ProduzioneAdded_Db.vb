Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class ProduzioneAdded_Db
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ProgettiProd",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .OC_Riferimento = c.String(),
                        .OperatoreSmistamento = c.String(),
                        .OperatoreSmistamentoId = c.String(),
                        .Operatore = c.String(),
                        .OperatoreId = c.String(),
                        .DataCreazione = c.DateTime(nullable := False),
                        .DataCompletamento = c.DateTime(),
                        .Note = c.String(),
                        .File = c.String(),
                        .StatoProgetto = c.Byte(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.ProgettiProd")
        End Sub
    End Class
End Namespace
