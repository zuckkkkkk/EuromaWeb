Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class AddedMacchinaFasi1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.TempiAttivita", "Descrizione_Attivita", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.TempiAttivita", "Descrizione_Attivita")
        End Sub
    End Class
End Namespace
