Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class Priorita_Data_Consegna_Prod_UT
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.ProgettiProd", "Priorita", Function(c) c.Byte(nullable := False))
            AddColumn("dbo.ProgettiProd", "DataRichiestaConsegna", Function(c) c.DateTime())
            AddColumn("dbo.ProgettiUT", "Priorita", Function(c) c.Byte(nullable := False))
            AddColumn("dbo.ProgettiUT", "DataRichiestaConsegna", Function(c) c.DateTime())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.ProgettiUT", "DataRichiestaConsegna")
            DropColumn("dbo.ProgettiUT", "Priorita")
            DropColumn("dbo.ProgettiProd", "DataRichiestaConsegna")
            DropColumn("dbo.ProgettiProd", "Priorita")
        End Sub
    End Class
End Namespace
