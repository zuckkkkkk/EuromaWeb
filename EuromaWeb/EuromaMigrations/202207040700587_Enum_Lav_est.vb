Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Enum_Lav_est
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.LavorazioniEsterne", "Inviato", Function(c) c.Byte(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.LavorazioniEsterne", "Inviato", Function(c) c.Boolean(nullable := False))
        End Sub
    End Class
End Namespace
