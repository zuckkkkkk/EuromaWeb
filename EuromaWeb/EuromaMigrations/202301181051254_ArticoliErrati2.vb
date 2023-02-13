Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class ArticoliErrati2
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ArticoliErrati", "OperatoreCompletamento", Function(c) c.String())
            AddColumn("dbo.ArticoliErrati", "UltimaModifica_OperatoreID", Function(c) c.String())
            AddColumn("dbo.ArticoliErrati", "UltimaModifica_Operatore", Function(c) c.String())
            AddColumn("dbo.ArticoliErrati", "UltimaModifica_Data", Function(c) c.DateTime())
            AlterColumn("dbo.ArticoliErrati", "RichiestaCompletata", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.ArticoliErrati", "RichiestaCompletata", Function(c) c.String())
            DropColumn("dbo.ArticoliErrati", "UltimaModifica_Data")
            DropColumn("dbo.ArticoliErrati", "UltimaModifica_Operatore")
            DropColumn("dbo.ArticoliErrati", "UltimaModifica_OperatoreID")
            DropColumn("dbo.ArticoliErrati", "OperatoreCompletamento")
        End Sub
    End Class
End Namespace
