Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

Namespace IdentityMigrations

    Friend NotInheritable Class Configuration 
        Inherits DbMigrationsConfiguration(Of ApplicationDbContext)

        Public Sub New()
            AutomaticMigrationsEnabled = False
            MigrationsDirectory = "IdentityMigrations"
        End Sub

        Protected Overrides Sub Seed(context As ApplicationDbContext)
            '  This method will be called after migrating to the latest version.

            '  You can use the DbSet(Of T).AddOrUpdate() helper extension method 
            '  to avoid creating duplicate seed data.

            '=========================================================================COMMERCIALE
            If Not context.Roles.Any(Function(r) r.Name = "Commerciale_Admin") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "Commerciale_Admin"}
                manager.Create(role)
            End If
            If Not context.Roles.Any(Function(r) r.Name = "Commerciale_Utente") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "Commerciale_Utente"}
                manager.Create(role)
            End If
            '=========================================================================TECNICO
            If Not context.Roles.Any(Function(r) r.Name = "Tecnico") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "Tecnico"}
                manager.Create(role)
            End If
            If Not context.Roles.Any(Function(r) r.Name = "TecnicoAdmin") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "TecnicoAdmin"}
                manager.Create(role)
            End If
            If Not context.Roles.Any(Function(r) r.Name = "TecnicoRevisione") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "TecnicoRevisione"}
                manager.Create(role)
            End If
            If Not context.Roles.Any(Function(r) r.Name = "TecnicoVisualizzazione") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "TecnicoVisualizzazione"}
                manager.Create(role)
            End If
            '=========================================================================ALTRI
            If Not context.Roles.Any(Function(r) r.Name = "Admin") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "Admin"}
                manager.Create(role)
            End If
            If Not context.Roles.Any(Function(r) r.Name = "Magazzino") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "Magazzino"}
                manager.Create(role)
            End If
            If Not context.Roles.Any(Function(r) r.Name = "Produzione") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "Produzione"}
                manager.Create(role)
            End If
            If Not context.Roles.Any(Function(r) r.Name = "ProduzioneController") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "ProduzioneController"}
                manager.Create(role)
            End If
            If Not context.Roles.Any(Function(r) r.Name = "ProgrammazioneEsterno") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "ProgrammazioneEsterno"}
                manager.Create(role)
            End If
            If Not context.Roles.Any(Function(r) r.Name = "ProgrammazioneInterno") Then
                Dim store = New RoleStore(Of IdentityRole)(context)
                Dim manager = New RoleManager(Of IdentityRole)(store)
                Dim role = New IdentityRole With {.Name = "ProgrammazioneInterno"}
                manager.Create(role)
            End If
            'User Creation

            If Not context.Users.Any(Function(u) u.UserName = "mattia") Then
                Dim store = New UserStore(Of ApplicationUser)(context)
                Dim manager = New UserManager(Of ApplicationUser)(store)
                Dim user = New ApplicationUser With {.UserName = "mattia"}
                manager.Create(user, "Euroma.1")
                manager.AddToRole(user.Id, "Admin")
            End If
            If Not context.Users.Any(Function(u) u.UserName = "ProgrammazioneEsterno") Then
                Dim store = New UserStore(Of ApplicationUser)(context)
                Dim manager = New UserManager(Of ApplicationUser)(store)
                Dim user = New ApplicationUser With {.UserName = "ProgrammazioneEsterno"}
                manager.Create(user, "Euroma.1")
                manager.AddToRole(user.Id, "ProgrammazioneEsterno")
            End If
            If Not context.Users.Any(Function(u) u.UserName = "ProgrammazioneInterno") Then
                Dim store = New UserStore(Of ApplicationUser)(context)
                Dim manager = New UserManager(Of ApplicationUser)(store)
                Dim user = New ApplicationUser With {.UserName = "ProgrammazioneInterno"}
                manager.Create(user, "Euroma.1")
                manager.AddToRole(user.Id, "ProgrammazioneInterno")
            End If
            If Not context.Users.Any(Function(u) u.UserName = "valerio") Then
                Dim store = New UserStore(Of ApplicationUser)(context)
                Dim manager = New UserManager(Of ApplicationUser)(store)
                Dim user = New ApplicationUser With {.UserName = "valerio"}
                manager.Create(user, "Euroma.1")
                manager.AddToRole(user.Id, "Admin")
            End If
            If Not context.Users.Any(Function(u) u.UserName = "lorenzo") Then
                Dim store = New UserStore(Of ApplicationUser)(context)
                Dim manager = New UserManager(Of ApplicationUser)(store)
                Dim user = New ApplicationUser With {.UserName = "lorenzo"}
                manager.Create(user, "Euroma.1")
                manager.AddToRole(user.Id, "Admin")
            End If
            If Not context.Users.Any(Function(u) u.UserName = "maurizio") Then
                Dim store = New UserStore(Of ApplicationUser)(context)
                Dim manager = New UserManager(Of ApplicationUser)(store)
                Dim user = New ApplicationUser With {.UserName = "maurizio"}
                manager.Create(user, "Euroma.1")
                manager.AddToRole(user.Id, "Commerciale_Admin")
            End If
            If Not context.Users.Any(Function(u) u.UserName = "barbara") Then
                Dim store = New UserStore(Of ApplicationUser)(context)
                Dim manager = New UserManager(Of ApplicationUser)(store)
                Dim user = New ApplicationUser With {.UserName = "barbara"}
                manager.Create(user, "Euroma.1")
                manager.AddToRole(user.Id, "Commerciale_Utente")
            End If
            If Not context.Users.Any(Function(u) u.UserName = "sebastiano") Then
                Dim store = New UserStore(Of ApplicationUser)(context)
                Dim manager = New UserManager(Of ApplicationUser)(store)
                Dim user = New ApplicationUser With {.UserName = "sebastiano"}
                manager.Create(user, "Euroma.1")
                manager.AddToRole(user.Id, "Produzione")
            End If
        End Sub

    End Class

End Namespace
