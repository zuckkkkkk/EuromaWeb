@ModelType IEnumerable(Of EuromaWeb.ProgettiProd)
@Code
    ViewData("Title") = "Produzione"
End Code

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
                <Button type="button" id="Send_Btn" class="btn btn-primary Send ModalSubmit">Invia</Button>
                <Button type="button" class="btn btn-danger Delete ModalSubmit">Elimina</Button>
                <Button type="button" class="btn btn-primary Save ModalSubmit">Salva Modifiche</Button>
                <Button type="button" class="btn btn-secondary SaveClose ModalSubmit">Salva e Chiudi</Button>
            </div>
        </div>
    </div>
</div>

<h2 style="margin-top: 1rem; margin-bottom: 1rem;">Produzione</h2>
@If User.IsInRole("Admin") Or User.IsInRole("ProduzioneController") Then
    @<ul Class="nav nav-pills mb-3" id="pills-tab" role="tablist">
        <li Class="nav-item" role="presentation">
            <Button Class="nav-link active" id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">In Corso</Button>
        </li>
        <li Class="nav-item" role="presentation">
            <Button Class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Completati</Button>
        </li>
    </ul>
Else
    @<ul Class="nav nav-pills mb-3" id="pills-tab" role="tablist">
        <li Class="nav-item" role="presentation">
            <Button Class="nav-link active" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="true">Completati</Button>
        </li>
    </ul>
End If
@If User.IsInRole("Admin") Or User.IsInRole("ProduzioneController") Then
    @<div Class="tab-content" id="pills-tabContent">
    <div Class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
        <Table id="mainDataTableProduzione" Class="stripe">
            <thead>
                <tr>
                    <th>

                    </th>
                    <th>
                        Commessa
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.Operatore)
                    </th>
                    <th>
                        Stato progetto
                    </th>
                </tr>
            </thead>

            <tbody>
                @For Each item In Model
                    @<tr>
                        <td>
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.OC_Riferimento)
                        </td>

                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Operatore)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.StatoProgetto)

                        </td>

                    </tr>
                Next
            </tbody>

        </Table>

    </div>
    <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
        <table id="mainDataTableProduzioneCompletati" class="stripe" style="width:100%!important;">
            <thead>
                <tr>
                    <th>

                    </th>
                    <th>
                        Commessa
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.Operatore)
                    </th>
                    <th>
                        Stato progetto
                    </th>
                </tr>
            </thead>

            <tbody>
                @For Each item In Model
                    @<tr>
                        <td>
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.OC_Riferimento)
                        </td>

                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Operatore)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.StatoProgetto)

                        </td>

                    </tr>
                Next
            </tbody>

        </table>
    </div>
</div>
Else
    @<div Class="tab-content" id="pills-tabContent">
    <div class="tab-pane fade show active" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
        <table id="mainDataTableProduzioneCompletati" class="stripe" style="width:100%!important;">
            <thead>
                <tr>
                    <th>

                    </th>
                    <th>
                        Commessa
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.Operatore)
                    </th>
                    <th>
                        Stato progetto
                    </th>
                </tr>
            </thead>

            <tbody>
                @For Each item In Model
                    @<tr>
                        <td>
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.OC_Riferimento)
                        </td>

                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Operatore)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.StatoProgetto)

                        </td>

                    </tr>
                Next
            </tbody>

        </table>
    </div>
</div>
End If



