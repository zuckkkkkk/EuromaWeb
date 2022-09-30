Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class OperatoreProgettiUT
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ProgettiUT", "OperatoreSmistamentoId", Function(c) c.String())
            AddColumn("dbo.ProgettiUT", "OperatoreId", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.ProgettiUT", "OperatoreId")
            DropColumn("dbo.ProgettiUT", "OperatoreSmistamentoId")
        End Sub
    End Class
End Namespace
