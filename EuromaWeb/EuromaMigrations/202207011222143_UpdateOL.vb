Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class UpdateOL
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.LavorazioniEsterne", "Id_Alnus", Function(c) c.Int(nullable := False))
            AddColumn("dbo.LavorazioniEsterne", "Inviato", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.LavorazioniEsterne", "Inviato")
            DropColumn("dbo.LavorazioniEsterne", "Id_Alnus")
        End Sub
    End Class
End Namespace
