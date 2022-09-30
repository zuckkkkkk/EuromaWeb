@ModelType IEnumerable(Of EuromaWeb.TicketViewModel)
@Code
    ViewData("Title") = "Index"
End Code

<h2 style="margin-top: 1rem; margin-bottom: 1rem;">Sezione Ticket</h2>
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



<table id="mainDataTable" class="stripe">
    <thead>
        <tr>
            <th>
                Utente richiesta
            </th>
            <th>
                Data richiesta
            </th>
            <th>
               Titolo
            </th>
            <th>
                Tipologia richiesta
            </th>
            <th>
                Stato Progetto
            </th>
            @*<th>
                @Html.DisplayNameFor(Function(model) model.Body)
            </th>*@

            <th></th>
        </tr>
    </thead>

    <tbody>
        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.RequestUser)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.RequestDate)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Title)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Request_Type)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Stato_Ticket)
                </td>
                <td>

                    <button type="button" data-type="edit_ticket" data-value="@item.Id" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                        <img src="~/Content/icons/Modifica.png" width="16" height="16" />
                    </button>
                    <button type="button" data-type="details_ticket" data-value="@item.Id" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                        <img src="~/Content/icons/Cerca.png" width="16" height="16" />
                    </button>
                </td>
            </tr>
        Next
    </tbody>


</table>
<div class="row text-center">
    <div class="col">
        <button type="button" data-type="add_ticket" class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
            Aggiungi Ticket
        </button>
    </div>
</div>
