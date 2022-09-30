Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class ChangeLog_Visualizzazione1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.VisualizzazioneChangeLog", "ChangeLog_Id", "dbo.ChangeLog")
            DropIndex("dbo.VisualizzazioneChangeLog", New String() { "ChangeLog_Id" })
            AlterColumn("dbo.VisualizzazioneChangeLog", "changeLog_id", Function(c) c.Int(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.VisualizzazioneChangeLog", "changeLog_id", Function(c) c.Int())
            CreateIndex("dbo.VisualizzazioneChangeLog", "ChangeLog_Id")
            AddForeignKey("dbo.VisualizzazioneChangeLog", "ChangeLog_Id", "dbo.ChangeLog", "Id")
        End Sub
    End Class
End Namespace
