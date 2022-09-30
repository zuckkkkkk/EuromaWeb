@ModelType IndexViewModel
@Code
    ViewBag.Title = "Gestisci"
End Code
<h2 style="margin-top:1rem;margin-bottom:1rem;">@ViewBag.Title.</h2>
<!-- Modal -->
<div class="modal fade " id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                ...
            </div>
            <div class="modal-footer" style="border-top: none!important;">
                <Button type="button" class="btn btn-primary Add ModalSubmit">Aggiungi</Button>
                <Button type="button" class="btn btn-primary Send ModalSubmit">Invia</Button>
                <Button type="button" class="btn btn-danger Delete ModalSubmit">Elimina</Button>
                <Button type="button" class="btn btn-primary Save ModalSubmit">Salva Modifiche</Button>
                <Button type="button" class="btn btn-secondary SaveClose ModalSubmit">Salva e Chiudi</Button>
            </div>
        </div>
    </div>
</div>
<p class="text-success">@ViewBag.StatusMessage</p>
<div>
    <h4>Cambiare le impostazioni dell'account</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>Password:</dt>
        <dd>
            [
            @If Model.HasPassword Then
                @Html.ActionLink("Cambia password", "ChangePassword")
            Else
                @Html.ActionLink("Crea", "SetPassword")
            End If
            ]
        </dd>
        @*<dt>Account di accesso esterni:</dt>
            <dd>
                @Model.Logins.Count [
                @Html.ActionLink("Gestisci", "ManageLogins") ]
            </dd>*@
        @*
            È possibile usare i numeri di telefono come secondo fattore di verifica in un sistema di autenticazione a due fattori.

             Vedere <a href="https://go.microsoft.com/fwlink/?LinkId=403804">questo articolo</a>
                per informazioni su come configurare l'applicazione ASP.NET per il supporto dell'autenticazione a due fattori usando gli SMS.

             Rimuovere i commenti dal blocco seguente dopo aver configurato l'autenticazione a due fattori
        *@
        @*
            <dt>Numero di telefono:</dt>
            <dd>
                @(If(Model.PhoneNumber, "None"))
                @If (Model.PhoneNumber <> Nothing) Then
                    @<br />
                    @<text>[&nbsp;&nbsp;@Html.ActionLink("Change", "AddPhoneNumber")&nbsp;&nbsp;]</text>
                    @Using Html.BeginForm("RemovePhoneNumber", "Manage", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                        @Html.AntiForgeryToken
                        @<text>[<input type="submit" value="Rimuovi" class="btn-link" />]</text>
                    End Using
                Else
                    @<text>[&nbsp;&nbsp;@Html.ActionLink("Add", "AddPhoneNumber") &nbsp;&nbsp;]</text>
                End If
            </dd>
        *@
        @*<dt>Autenticazione a due fattori:</dt>
        <dd>
            <p>
                Non sono configurati provider di autenticazione a due fattori. Vedere <a href="https://go.microsoft.com/fwlink/?LinkId=403804">questo articolo</a>
                per informazioni su come configurare l'applicazione ASP.NET per il supporto dell'autenticazione a due fattori.
            </p>*@
            @*
                @If Model.TwoFactor Then
                    @Using Html.BeginForm("DisableTwoFactorAuthentication", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
                      @Html.AntiForgeryToken()
                      @<text>
                      Attivato
                      <input type="submit" value="Disabilita" class="btn btn-link" />
                      </text>
                    End Using
                Else
                    @Using Html.BeginForm("EnableTwoFactorAuthentication", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
                      @Html.AntiForgeryToken()
                      @<text>
                      Disabilitata
                      <input type="submit" value="Abilita" class="btn btn-link" />
                      </text>
                    End Using
                End If
            *@
        @*</dd>*@
    </dl>
    @If User.IsInRole("Admin") Then
        @<hr />
        @<div class="bg-light p-5 rounded-lg m-3">
        <h1 class="display-4">Sezione Account
            <button type="button" data-type="createAcc" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                Crea Account
            </button>
    </h1>
        <hr class="my-4">
        <table id="mainDataTableUser" class="table table-striped">
            <thead>
                <tr>
                    <td>
                        Username
                    </td>
                    <td>
                        Email
                    </td>
                    <td>
                        Azioni
                    </td>
                </tr>
            </thead>
            <tbody>
                @For each u In ViewBag.Users
                    @<tr>
                        <td>
                            @u.Username
                        </td>
                        <td>
                            @u.Email
                        </td>
                        <td>
                        </td>
                    </tr>
                Next
            </tbody>
        </table>
    </div>

    End If
    <button type = "button" data-type="editUser" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
        Modifica Account
    </button>
</div>
