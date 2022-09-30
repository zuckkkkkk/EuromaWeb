Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class StoricoOC1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.StoricoOC", "Ufficio", Function(c) c.Byte())
            AddColumn("dbo.StoricoOC", "Titolo", Function(c) c.String())
            AddColumn("dbo.StoricoOC", "Descrizione", Function(c) c.String())
            DropColumn("dbo.StoricoOC", "Livello")
            DropColumn("dbo.StoricoOC", "Indirizzo")
            DropColumn("dbo.StoricoOC", "Messaggio")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.StoricoOC", "Messaggio", Function(c) c.String())
            AddColumn("dbo.StoricoOC", "Indirizzo", Function(c) c.String())
            AddColumn("dbo.StoricoOC", "Livello", Function(c) c.Byte())
            DropColumn("dbo.StoricoOC", "Descrizione")
            DropColumn("dbo.StoricoOC", "Titolo")
            DropColumn("dbo.StoricoOC", "Ufficio")
        End Sub
    End Class
End Namespace
