Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Init4
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.LavorazioniEsterne",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Id_Operatore = c.String(),
                        .Operatore = c.String(),
                        .OL = c.String(),
                        .Data_Inserimento = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.LavorazioniEsterne")
        End Sub
    End Class
End Namespace
