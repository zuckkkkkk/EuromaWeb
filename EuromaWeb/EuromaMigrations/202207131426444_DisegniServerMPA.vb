Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class DisegniServerMPA
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Disegni_MPA_server",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Code_Disegno = c.String(),
                        .Path_File = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Disegni_MPA_server")
        End Sub
    End Class
End Namespace
