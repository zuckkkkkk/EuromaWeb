Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class TempiCAD
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.TempiCAD",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .UserID = c.Int(nullable := False),
                        .UserName = c.String(),
                        .DatetimeInizio = c.DateTime(nullable := False),
                        .DatetimeFine = c.DateTime(nullable := False),
                        .ElapsedTime = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .OL = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.TempiCAD")
        End Sub
    End Class
End Namespace
