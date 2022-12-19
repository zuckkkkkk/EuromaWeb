Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class StoricoGrezzi_3
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.StoricoGrezzi", "ODLANN", Function(c) c.String())
            AddColumn("dbo.StoricoGrezzi", "ODLSEZ", Function(c) c.String())
            AddColumn("dbo.StoricoGrezzi", "ODLNMR", Function(c) c.Decimal(nullable := False, precision := 18, scale := 2))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.StoricoGrezzi", "ODLNMR")
            DropColumn("dbo.StoricoGrezzi", "ODLSEZ")
            DropColumn("dbo.StoricoGrezzi", "ODLANN")
        End Sub
    End Class
End Namespace
