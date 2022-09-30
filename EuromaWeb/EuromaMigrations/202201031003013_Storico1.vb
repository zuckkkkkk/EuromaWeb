Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Storico1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Progetti",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .OC = c.String(),
                        .StartDate = c.DateTime(nullable := False),
                        .EndDate = c.DateTime(nullable := False),
                        .Cliente = c.String(),
                        .Brand = c.Byte(nullable := False),
                        .Codice = c.String(),
                        .Note_Pezzo = c.String(),
                        .Id_Last_Storico_Progetto = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Storico_Progetti",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Id_Progetto = c.Int(nullable := False),
                        .Affidato = c.Byte(nullable := False),
                        .Previsione = c.DateTime(nullable := False),
                        .Note_Generiche = c.DateTime(nullable := False),
                        .Stato_Pezzo = c.Byte(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Storico_Progetti")
            DropTable("dbo.Progetti")
        End Sub
    End Class
End Namespace
