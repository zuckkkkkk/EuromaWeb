Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class TempiMacchina
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Tempi_Macchina",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .RequestUser = c.String(),
                        .Macchina = c.String(),
                        .T_Tot = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .T_Start = c.DateTime(nullable := False),
                        .T_End = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Tempi_Macchina")
        End Sub
    End Class
End Namespace
