Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class ComputerUtenti
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Computer",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .NomePC = c.String(),
                        .NomeOperatore = c.String(),
                        .DescrizionePC = c.String(),
                        .IP = c.String(),
                        .MAC = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Computer")
        End Sub
    End Class
End Namespace
