Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace EuromaMigrations
    Public Partial Class DatiMacchina
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.DatiMacchina",
                Function(c) New With
                    {
                        .id = c.Int(nullable := False, identity := True),
                        .Macchina = c.String(),
                        .ModalitaMacchina = c.String(),
                        .FungoPremuto = c.Boolean(nullable := False),
                        .ModalitaControllo = c.String(),
                        .Programma = c.String(),
                        .EsecuzioneProgramma = c.String(),
                        .AvanzamanetoProgramma = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.DatiMacchina")
        End Sub
    End Class
End Namespace
