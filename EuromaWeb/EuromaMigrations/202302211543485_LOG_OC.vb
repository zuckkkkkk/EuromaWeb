Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class LOG_OC
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.OC_LOG",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True),
                        .ESECOD = c.String(),
                        .ORCTSZ = c.String(),
                        .ORCTNR = c.String(),
                        .ORCMOV = c.String(),
                        .DVSCOD = c.String(),
                        .ORCCLI = c.String(),
                        .ORCIND = c.String(),
                        .ORCCLS = c.String(),
                        .ORCINS = c.String(),
                        .PAGCOD = c.String(),
                        .ORCDCOREV = c.String(),
                        .ORCTSSREV = c.String(),
                        .ORCST5 = c.String(),
                        .CORCOD = c.String()
                    }) _
                .PrimaryKey(Function(t) t.id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.OC_LOG")
        End Sub
    End Class
End Namespace
