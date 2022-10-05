Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Macchine
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Macchine",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True),
                        .Macchina = c.String(),
                        .Descrizione_Macchina = c.String(),
                        .Path_3d = c.String()
                    }) _
                .PrimaryKey(Function(t) t.id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Macchine")
        End Sub
    End Class
End Namespace
