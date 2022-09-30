Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class ChangeLog_Visualizzazione
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.VisualizzazioneChangeLog",
                Function(c) New With
                    {
                        .id = c.Int(nullable:=False, identity:=True),
                        .changeLog_id = c.Int(nullable:=False),
                        .ReaedingDate = c.DateTime(nullable:=False),
                        .User = c.String()
                    }) _
                .PrimaryKey(Function(t) t.id) _
                .ForeignKey("dbo.ChangeLog", Function(t) t.changeLog_id) _
                .Index(Function(t) t.changeLog_id)

        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.VisualizzazioneChangeLog", "ChangeLog_Id", "dbo.ChangeLog")
            DropIndex("dbo.VisualizzazioneChangeLog", New String() { "ChangeLog_Id" })
            DropTable("dbo.VisualizzazioneChangeLog")
        End Sub
    End Class
End Namespace
