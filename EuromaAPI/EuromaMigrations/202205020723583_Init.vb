Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Init
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Lavori",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Cod_Macchina = c.String(),
                        .Cod_Lavorazione = c.String(),
                        .Desc_Lavorazione = c.String(),
                        .DateTime_Inizio = c.DateTime(nullable := False),
                        .DateTime_Fine = c.DateTime(nullable := False),
                        .Totale_Time = c.Decimal(nullable := False, precision := 18, scale := 2)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Macchine",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Cod_Macchina = c.String(),
                        .Desc_Macchina = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Utenti",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Nome_Utente = c.String(),
                        .Password = c.String(),
                        .Email = c.String(),
                        .Soprannome = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Utenti")
            DropTable("dbo.Macchine")
            DropTable("dbo.Lavori")
        End Sub
    End Class
End Namespace
