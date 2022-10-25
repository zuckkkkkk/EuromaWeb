@ModelType GestioneUtentiViewModel
<h2 style="margin-top:1rem;margin-bottom:1rem;">Gestione Utenti</h2>
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
<div>
    <div class="row">
        <div class="col-md-12">
            <div class="tab-pane fade show active" id="pills-lista" role="tabpanel" aria-labelledby="pills-lista-tab">
                <table class="table table-striped" id="mainDataTableLicenzeAssociazioni" style="width:100%;">
                    <thead>
                        <tr>
                            <th>
                                Utente
                            </th>
                            <th>
                                Licenza
                            </th>
                            <th>
                                Descrizione
                            </th>
                            <th>
                                Data Scadenza
                            </th>
                            <th>
                                Qta
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>

                    </tbody>
                </table>
                <div class="row text-center">
                    <div class="col">
                        <button type="button" data-type="createAss" id="Add_Ass" class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                            Aggiungi Associazione
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row mt-2">
        <div class="col-md-12">
            <div class="accordion" id="accordionExample">
                <div class="accordion-item">
                    <h2 class="accordion-header" id="headingOne">
                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                            Lista Utenti
                        </button>
                    </h2>
                    <div id="collapseOne" class="accordion-collapse collapse" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                        <div class="accordion-body">
                            <h4 style="margin-left:16px; margin-top:16px;">
                                Sezione Account
                                @If User.IsInRole("Admin") Then
                                    @<button type="button" data-type="createAcc" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                        Crea Account
                                    </button>
                                End If
                            </h4>
                            <hr Class="my-4">
                            <Table id="mainDataTableUser" Class="table table-striped" style="width:100%;">
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
                                    @For Each u In Model.ListaUtenti
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
                            </Table>
                        </div>
                    </div>
                </div>
                <div class="accordion-item">
                    <h2 class="accordion-header" id="headingTwo">
                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                            Lista Licenze
                        </button>
                    </h2>
                    <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                        <div class="accordion-body">
                            <h4 style="margin-left:16px; margin-top:16px;">
                                Sezione Licenze
                                <button type="button" data-type="createLic" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                    Crea Licenza
                                </button>
                            </h4>
                            <table id="mainDataTableLicenze" class="table table-striped " style="width:100%;">
                                <thead>
                                    <tr>
                                        <td>
                                            Nome Licenza
                                        </td>
                                        <td>
                                            Descrizione
                                        </td>
                                        <td>
                                            Tipo
                                        </td>
                                        <td>
                                            Data Rinnovo
                                        </td>
                                        <td>
                                            Quantità
                                        </td>
                                        <td>
                                            Azioni
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    @For Each u In Model.ListaLicenze
                                        @<tr>
                                            <td>
                                                @u.NomeLicenza
                                            </td>
                                            <td>
                                                @u.DescrizioneLicenza
                                            </td>
                                            <td>
                                                @If Not u.TypeLicenza Then
                                                    @Html.Raw("Software")
                                                Else
                                                    @Html.Raw("Hardware")
                                                End If
                                            </td>
                                            <td>
                                                @u.DataRinnovo
                                            </td>
                                            <td>
                                                @u.QtaLicenze
                                            </td>
                                            <td>
                                                <btn class='btn btn-primary' data-value="@u.Id" data-type='editLic' id='Edit_Lic' data-bs-toggle='modal' data-bs-target='#exampleModal'><i class='fa-solid fa-pen-to-square'></i></btn>
                                            </td>
                                        </tr>
                                    Next
                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>
                <div class="accordion-item">
                    <h2 class="accordion-header" id="headingThree">
                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                            Lista Hardware
                        </button>
                    </h2>
                    <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                        <div class="accordion-body">
                            @*<strong> This is the third item's accordion body.</strong> It is hidden by default, until the collapse plugin adds the appropriate classes that we use to style each element. These classes control the overall appearance, as well as the showing and hiding via CSS transitions. You can modify any of this with custom CSS or overriding our default variables. It's also worth noting that just about any HTML can go within the <code>.accordion-body</code>, though the transition does limit overflow.*@
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <div class="tab-pane fade" id="pills-associazioni" role="tabpanel" aria-labelledby="pills-associazioni-tab">

    </div>
</div>
