Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Overview
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Overview",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Matricola = c.String(),
                        .Macchina = c.String(),
                        .Zona = c.Byte(nullable := False),
                        .Data = c.DateTime(nullable := False),
                        .Totale_Ore_Uomo = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Overview")
        End Sub
    End Class
End Namespace
