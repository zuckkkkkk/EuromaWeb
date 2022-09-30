@ModelType IEnumerable(Of EuromaWeb.AccettazioneUC)
@Code
    ViewData("Title") = "Commerciale"
    Dim DataEsportazione = DateTime.Now
End Code

<h2 style="margin-top: 1rem; margin-bottom: 1rem;">Sezione Accettazione <btn style="margin-left: 16px;"button type="button" data-type="Show_Carico" id="Show_Carico" class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal"> Carico UT <i class="fas fa-chart-bar"></i> </btn></h2>
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

    <table id="mainDataTableAccettazione" class="stripe">
        <thead>
            <tr>
                <th>

                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.Accettato)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.OC)
                </th>

                <th>
                    @Html.DisplayNameFor(Function(model) model.Cartella)
                </th>
                <th>
                    Operatore
                </th>
                <th>
                    Creato il
                </th>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </thead>

        <tbody>
            @For Each item In Model
                @<tr>
                    <td>
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.OC)
                    </td>

                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Cartella)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.OperatoreInsert)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.DataCreazione)
                    </td>

                </tr>
            Next
        </tbody>

    </table>
    <div class="row text-center">
        <div class="col">
            <button type="button" data-type="add" id="Add_Acc" class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                Aggiungi accettazione
            </button>
        </div>
    </div>
