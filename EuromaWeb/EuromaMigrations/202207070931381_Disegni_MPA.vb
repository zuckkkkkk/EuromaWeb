Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Disegni_MPA
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Disegni_MPA",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Code_Disegno = c.String(),
                        .User = c.String(),
                        .Cliente = c.String(),
                        .Path_File = c.String(),
                        .Descrizione = c.String(),
                        .Triple_Code = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Disegni_MPA")
        End Sub
    End Class
End Namespace
