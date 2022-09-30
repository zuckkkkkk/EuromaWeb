Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class TabellaVisualizzazioneAggiornamento
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.VisualizzazioneFileNota",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True),
                        .type = c.Byte(nullable := False),
                        .changeLog_id = c.Int(nullable := False),
                        .ReaedingDate = c.DateTime(nullable := False),
                        .User = c.String()
                    }) _
                .PrimaryKey(Function(t) t.id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.VisualizzazioneFileNota")
        End Sub
    End Class
End Namespace
