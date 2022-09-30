@ModelType List(Of EuromaWeb.ODPProduzioneViewModel)
@Code
    ViewData("Title") = "Schedulatore"
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

<h2 style="margin-top: 1rem; margin-bottom: 1rem;">Gestione Produzione</h2>
        <div>
            <Table id="mainDataTableSchedulatore" Class="stripe" style="width:100%;">
                <thead>
                    <tr>
                        <th>
                            Ordine di produzione
                        </th>
                        <th>
                            Articolo
                        </th>
                        <th>
                            Descrizione
                        </th>
                        <th>
                            Attività
                        </th>
                    </tr>
                </thead>
                <tbody>
                    @For Each item In Model
                        @<tr>
                            <td>
                                @Html.DisplayFor(Function(modelItem) item.ODP) (@Html.DisplayFor(Function(modelItem) item.Cliente))
                            </td>

                            <td>
                                @Html.DisplayFor(Function(modelItem) item.Articolo)
                            </td>
                            <td>
                                @Html.DisplayFor(Function(modelItem) item.Desc_Art)
                            </td>
                            <td>
                                @Html.DisplayFor(Function(modelItem) item.ListaAttivita)
                            </td>
                        </tr>
                    Next
                </tbody>
            </Table>
        </div>




