Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class DatiMacchina_Agg
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.DatiMacchina", "ProgrammaDesc", Function(c) c.String())
            AddColumn("dbo.DatiMacchina", "LpCuttingTime", Function(c) c.String())
            AddColumn("dbo.DatiMacchina", "LpOperatingTime", Function(c) c.String())
            AddColumn("dbo.DatiMacchina", "LpRunningTime", Function(c) c.String())
            AddColumn("dbo.DatiMacchina", "LpSpindleRunTime", Function(c) c.String())
            AddColumn("dbo.DatiMacchina", "LpTotalCuttinTime", Function(c) c.String())
            AddColumn("dbo.DatiMacchina", "LpTotalOperatingTime", Function(c) c.String())
            AddColumn("dbo.DatiMacchina", "LpTotalRunningTime", Function(c) c.String())
            AddColumn("dbo.DatiMacchina", "LpTotalSpindleRuntime", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.DatiMacchina", "LpTotalSpindleRuntime")
            DropColumn("dbo.DatiMacchina", "LpTotalRunningTime")
            DropColumn("dbo.DatiMacchina", "LpTotalOperatingTime")
            DropColumn("dbo.DatiMacchina", "LpTotalCuttinTime")
            DropColumn("dbo.DatiMacchina", "LpSpindleRunTime")
            DropColumn("dbo.DatiMacchina", "LpRunningTime")
            DropColumn("dbo.DatiMacchina", "LpOperatingTime")
            DropColumn("dbo.DatiMacchina", "LpCuttingTime")
            DropColumn("dbo.DatiMacchina", "ProgrammaDesc")
        End Sub
    End Class
End Namespace
