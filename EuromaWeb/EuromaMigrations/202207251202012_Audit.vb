Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Partial Public Class Audit
        Inherits DbMigration

        Public Overrides Sub Up()
            CreateTable(
                "dbo.Audit",
                Function(c) New With
                    {
                        .Id = c.Int(nullable:=False, identity:=True),
                        .Livello = c.Byte(),
                        .Indirizzo = c.String(),
                        .Messaggio = c.String(),
                        .Dati = c.String(),
                        .UltimaModifica_OperatoreID = c.String(),
                        .UltimaModifica_Operatore = c.String(),
                        .UltimaModifica_Data = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.Id)

        End Sub

        Public Overrides Sub Down()
            DropTable("dbo.Audit")
        End Sub
    End Class
End Namespace
