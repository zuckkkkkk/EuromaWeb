Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class TempiAttivita
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.TempiAttivita",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Cod_Macchina = c.String(),
                        .TempoGiorni = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.TempiAttivita")
        End Sub
    End Class
End Namespace
