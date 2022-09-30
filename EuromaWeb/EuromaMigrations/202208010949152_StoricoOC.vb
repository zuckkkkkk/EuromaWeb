Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class StoricoOC
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.StoricoOC",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .OC = c.String(),
                        .Livello = c.Byte(),
                        .Indirizzo = c.String(),
                        .Messaggio = c.String(),
                        .UltimaModifica_OperatoreID = c.String(),
                        .UltimaModifica_Operatore = c.String(),
                        .UltimaModifica_Data = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.StoricoOC")
        End Sub
    End Class
End Namespace
