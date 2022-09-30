Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Licenze
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Licenze",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Nome_Licenza = c.String(),
                        .Tipologia_Licenza = c.Byte(nullable := False),
                        .StartDate = c.DateTime(nullable := False),
                        .StartDate_Month = c.Int(nullable := False),
                        .StartDate_Day = c.Int(nullable := False),
                        .StartDate_Year = c.Int(nullable := False),
                        .Tipologia_Rinnovo = c.Byte(nullable := False),
                        .Active = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Licenze")
        End Sub
    End Class
End Namespace
