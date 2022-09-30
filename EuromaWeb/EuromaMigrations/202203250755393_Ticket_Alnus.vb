Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Ticket_Alnus
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.HelpDesk",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .RequestUser = c.String(),
                        .RequestDate = c.DateTime(nullable := False),
                        .Title = c.String(),
                        .Body = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.HelpDesk")
        End Sub
    End Class
End Namespace
