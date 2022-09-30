Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class ChangeLog
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ChangeLog",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Title = c.String(),
                        .Descrizione = c.String(),
                        .Release_Date = c.DateTime(nullable := False),
                        .StartDate = c.DateTime(nullable := False),
                        .EndDate = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.ChangeLog")
        End Sub
    End Class
End Namespace
