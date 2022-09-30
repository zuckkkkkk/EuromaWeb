Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class ProgettiUT1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ProgettiUT",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .OC_Riferimento = c.String(),
                        .OperatoreSmistamento = c.String(),
                        .Operatore = c.String(),
                        .DataCreazione = c.DateTime(nullable := False),
                        .DataCompletamento = c.DateTime(),
                        .Note = c.String(),
                        .File = c.String(),
                        .StatoProgetto = c.Byte(nullable := False),
                        .Flag_Invio_Materiali = c.Boolean(nullable := False),
                        .Flag_1 = c.Boolean(nullable := False),
                        .Flag_2 = c.Boolean(nullable := False),
                        .Flag_3 = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.ProgettiUT")
        End Sub
    End Class
End Namespace
