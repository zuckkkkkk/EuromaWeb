Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class CampoDescAlnus
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Disegni_MPA", "Desc_Alnus", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Disegni_MPA", "Desc_Alnus")
        End Sub
    End Class
End Namespace