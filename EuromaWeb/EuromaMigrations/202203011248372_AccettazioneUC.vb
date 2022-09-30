Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class AccettazioneUC
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.AccettazioneUC",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .OC = c.String(),
                        .PercorsoFileNonConfermato = c.String(),
                        .PercorsoFileConfermato = c.String(),
                        .Cartella = c.String(),
                        .OperatoreInsert = c.String(),
                        .OperatoreAccettazione = c.String(),
                        .DataCreazione = c.DateTime(nullable := False),
                        .DataAccettazione = c.DateTime(nullable := False),
                        .Accettato = c.Boolean(nullable := False),
                        .Note = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.AccettazioneUC")
        End Sub
    End Class
End Namespace
