Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class ArticoliErrati
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ArticoliErrati",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Cod_OP = c.String(),
                        .Fase_OP = c.String(),
                        .Cod_OC = c.String(),
                        .Articolo = c.String(),
                        .Note = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.ArticoliErrati")
        End Sub
    End Class
End Namespace
