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
    </dl>
    <button type="button" data-type="editUser" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
        Modifica Account
    </button>
</div>
