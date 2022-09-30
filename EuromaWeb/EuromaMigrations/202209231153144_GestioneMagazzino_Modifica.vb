Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class GestioneMagazzino_Modifica
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.ScaffaliMagazzino", "numScaffale", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.ScaffaliMagazzino", "numScaffale", Function(c) c.Int(nullable := False))
        End Sub
    End Class
End Namespace
