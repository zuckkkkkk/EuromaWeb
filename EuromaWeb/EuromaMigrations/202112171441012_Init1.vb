Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Init1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Ordini",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Mese = c.Byte(nullable := False),
                        .Anno = c.Int(nullable := False),
                        .CodCliente = c.String(),
                        .NomeCliente = c.String(),
                        .Stato = c.String(),
                        .Regione_uno = c.String(),
                        .Regione_due = c.String(),
                        .Tipo_Ordine = c.Byte(nullable := False),
                        .Valore_Netto = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .Valore_Totale = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .Provenienza = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Ordini")
        End Sub
    End Class
End Namespace
