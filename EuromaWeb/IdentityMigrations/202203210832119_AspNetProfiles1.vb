Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace IdentityMigrations
    Public Partial Class AspNetProfiles1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.AspNetProfiles",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Impostazioni_Impianti_Dimensione = c.Int(nullable := False),
                        .Impostazioni_Impianti_Acapo = c.Boolean(nullable := False),
                        .Impostazioni_Impianti_Troncamento = c.Boolean(nullable := False),
                        .Impostazioni_Impianti_BarraScorrimentoH = c.Boolean(nullable := False),
                        .Impostazioni_Impianti_Zebratura = c.Boolean(nullable := False),
                        .Impostazioni_Impianti_NuovaFinestra = c.Boolean(nullable := False),
                        .Impostazioni_Amministratori_Dimensione = c.Int(nullable := False),
                        .Impostazioni_Amministratori_Acapo = c.Boolean(nullable := False),
                        .Impostazioni_Amministratori_Troncamento = c.Boolean(nullable := False),
                        .Impostazioni_Amministratori_BarraScorrimentoH = c.Boolean(nullable := False),
                        .Impostazioni_Amministratori_Zebratura = c.Boolean(nullable := False),
                        .Impostazioni_Amministratori_NuovaFinestra = c.Boolean(nullable := False),
                        .Impostazioni_Termotecnici_Dimensione = c.Int(nullable := False),
                        .Impostazioni_Termotecnici_Acapo = c.Boolean(nullable := False),
                        .Impostazioni_Termotecnici_Troncamento = c.Boolean(nullable := False),
                        .Impostazioni_Termotecnici_BarraScorrimentoH = c.Boolean(nullable := False),
                        .Impostazioni_Termotecnici_Zebratura = c.Boolean(nullable := False),
                        .Impostazioni_Termotecnici_NuovaFinestra = c.Boolean(nullable := False),
                        .Impostazioni_Documenti_Dimensione = c.Int(nullable := False),
                        .Impostazioni_Documenti_Acapo = c.Boolean(nullable := False),
                        .Impostazioni_Documenti_Troncamento = c.Boolean(nullable := False),
                        .Impostazioni_Documenti_BarraScorrimentoH = c.Boolean(nullable := False),
                        .Impostazioni_Documenti_Zebratura = c.Boolean(nullable := False),
                        .Impostazioni_Documenti_NuovaFinestra = c.Boolean(nullable := False),
                        .Impostazioni_Utenti_Dimensione = c.Int(nullable := False),
                        .Impostazioni_Utenti_Acapo = c.Boolean(nullable := False),
                        .Impostazioni_Utenti_Troncamento = c.Boolean(nullable := False),
                        .Impostazioni_Utenti_BarraScorrimentoH = c.Boolean(nullable := False),
                        .Impostazioni_Utenti_Zebratura = c.Boolean(nullable := False),
                        .Impostazioni_Utenti_NuovaFinestra = c.Boolean(nullable := False),
                        .Impostazioni_Ruoli_Dimensione = c.Int(nullable := False),
                        .Impostazioni_Ruoli_Acapo = c.Boolean(nullable := False),
                        .Impostazioni_Ruoli_Troncamento = c.Boolean(nullable := False),
                        .Impostazioni_Ruoli_BarraScorrimentoH = c.Boolean(nullable := False),
                        .Impostazioni_Ruoli_Zebratura = c.Boolean(nullable := False),
                        .Impostazioni_Ruoli_NuovaFinestra = c.Boolean(nullable := False),
                        .Preferenze_Impianti_Amministrazione = c.Boolean(nullable := False),
                        .Preferenze_Impianti_Anagrafica = c.Boolean(nullable := False),
                        .Preferenze_Impianti_CentraleTermica = c.Boolean(nullable := False),
                        .Preferenze_Impianti_Scadenze = c.Boolean(nullable := False),
                        .Preferenze_Impianti_Conformita = c.Boolean(nullable := False),
                        .Preferenze_Impianti_SoloDocumentiAttivi = c.Boolean(nullable := False),
                        .Preferenze_Impianti_BoxAmministratore = c.Boolean(nullable := False),
                        .Preferenze_Impianti_BoxTermotecnico = c.Boolean(nullable := False),
                        .Preferenze_Impianti_BoxProprietario = c.Boolean(nullable := False),
                        .Preferenze_Impianti_BoxContratto = c.Boolean(nullable := False),
                        .Preferenze_Impianti_BoxContabilizzazione = c.Boolean(nullable := False),
                        .Preferenze_Impianti_BoxCentraleTermica = c.Boolean(nullable := False),
                        .Preferenze_Impianti_BoxScadenze = c.Boolean(nullable := False),
                        .Preferenze_Impianti_BoxVerifiche = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.AspNetProfiles")
        End Sub
    End Class
End Namespace
