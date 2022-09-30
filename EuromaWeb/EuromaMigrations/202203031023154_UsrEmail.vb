Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class UsrEmail
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.UserEmail",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Uid = c.String(),
                        .Email = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.UserEmail")
        End Sub
    End Class
End Namespace
