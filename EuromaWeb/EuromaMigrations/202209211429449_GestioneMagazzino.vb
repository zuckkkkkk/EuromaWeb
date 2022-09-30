Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class GestioneMagazzino
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ArticoliMagazzino",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .codArticolo = c.String(),
                        .qta = c.Double(nullable := False),
                        .idSlot = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Magazzino",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .CodMagazzino = c.Int(nullable := False),
                        .DescrizioneMagazzino = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.ScaffaliMagazzino",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .numScaffale = c.Int(nullable := False),
                        .idesternoMagazzino = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.SlotScaffale",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .idEsternoScaffale = c.Int(nullable := False),
                        .nomeSlot = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.SlotScaffale")
            DropTable("dbo.ScaffaliMagazzino")
            DropTable("dbo.Magazzino")
            DropTable("dbo.ArticoliMagazzino")
        End Sub
    End Class
End Namespace
