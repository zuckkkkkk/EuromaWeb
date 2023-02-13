Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class ArticoliErrati1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ArticoliErrati", "Note_Produzione", Function(c) c.String())
            AddColumn("dbo.ArticoliErrati", "Note_UT", Function(c) c.String())
            AddColumn("dbo.ArticoliErrati", "RichiestaCompletata", Function(c) c.String())
            DropColumn("dbo.ArticoliErrati", "Note")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.ArticoliErrati", "Note", Function(c) c.String())
            DropColumn("dbo.ArticoliErrati", "RichiestaCompletata")
            DropColumn("dbo.ArticoliErrati", "Note_UT")
            DropColumn("dbo.ArticoliErrati", "Note_Produzione")
        End Sub
    End Class
End Namespace
