Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class AggiuntaNoteEStati
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ArticoliPerOC",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Cod_Art = c.String(),
                        .Descrizione = c.String(),
                        .OC = c.String(),
                        .DistintaBase = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.NotePerOC",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .OC = c.String(),
                        .Contenuto_Nota = c.String(),
                        .Data_Nota = c.DateTime(nullable := False),
                        .Operatore_Id = c.String(),
                        .Operatore_Nome = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.NotePerOC")
            DropTable("dbo.ArticoliPerOC")
        End Sub
    End Class
End Namespace
