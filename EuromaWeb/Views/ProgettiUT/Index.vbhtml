@ModelType IEnumerable(Of EuromaWeb.Progetti_UTViewModel)
@Code
    ViewData("Title") = "Ufficio Tecnico"
End Code

<style>
    input[type='checkbox'] {
        width: 37px !important;
        height: 37px !important;
    }

    .EditOperatoreTecnico {
        display: none;
    }

    @@media only screen and (max-width: 768px) {
        .dataTables_info, .dt-buttons, .dataTables_paginate {
            display: none !important;
        }

        #mainDataTableProgettiEsterniInAttesa_filter, #mainDataTableRichieste_filter {
            position: fixed;
            z-index: 1000;
            background-color: white;
            bottom: 0px;
            width: 100vw;
            left: 0;
            padding: 8px;
            padding-bottom: 16px !important;
        }

        #pills-esterni-in-attesa, #pills-esterni, #pills-richieste {
            overflow-x: scroll;
        }
    }
</style>
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

<h2 style="margin-top: 1rem; margin-bottom: 1rem;">Progetti</h2>
<ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
    @If User.IsInRole("TecnicoVisualizzazione") Or User.IsInRole("Tecnico") Or User.IsInRole("TecnicoAdmin") Or User.IsInRole("TecnicoRevisione") Or User.IsInRole("Admin") Then
        @<li Class="nav-item" role="presentation">
            <Button Class="nav-link active" id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">In Corso</Button>
        </li>
        @<li Class="nav-item" role="presentation">
            <Button Class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Completati</Button>
        </li>

    End If
    @If User.IsInRole("ProgrammazioneEsterno") Or User.IsInRole("ProgrammazioneInterno") Then
        @<li Class="nav-item" role="presentation">
            <Button Class="nav-link active" id="pills-esterni-in-attesa-tab" data-bs-toggle="pill" data-bs-target="#pills-esterni-in-attesa" type="button" role="tab" aria-controls="pills-esterni-in-attesa" aria-selected="false">Esterni in attesa</Button>
        </li>
    ElseIf User.IsInRole("Tecnico") Or User.IsInRole("TecnicoAdmin") Or User.IsInRole("TecnicoRevisione") Or User.IsInRole("Admin") Then
        @<li Class="nav-item" role="presentation">
            <Button Class="nav-link" id="pills-esterni-in-attesa-tab" data-bs-toggle="pill" data-bs-target="#pills-esterni-in-attesa" type="button" role="tab" aria-controls="pills-esterni-in-attesa" aria-selected="false">Esterni in attesa</Button>
        </li>
    End if
    @If User.IsInRole("ProgrammazioneEsterno") Or User.IsInRole("ProgrammazioneInterno") Or User.IsInRole("Tecnico") Or User.IsInRole("TecnicoAdmin") Or User.IsInRole("TecnicoRevisione") Or User.IsInRole("Admin") Then
        @<li Class="nav-item" role="presentation">
            <Button Class="nav-link" id="pills-esterni-tab" data-bs-toggle="pill" data-bs-target="#pills-esterni" type="button" role="tab" aria-controls="pills-esterni" aria-selected="false">Esterni Completati</Button>
        </li>
    End If
    @If User.IsInRole("Tecnico") Or User.IsInRole("Produzione") Or User.IsInRole("Admin") Or User.IsInRole("TecnicoAdmin") Then
        If User.IsInRole("Produzione") Then
            @<li Class="nav-item" role="presentation">
                <Button Class="nav-link active" id="pills-richieste-tab" data-bs-toggle="pill" data-bs-target="#pills-richieste" type="button" role="tab" aria-controls="pills-richieste" aria-selected="true">Richieste Modifica</Button>
            </li>
        Else
            @<li Class="nav-item" role="presentation">
                <Button Class="nav-link" id="pills-richieste-tab" data-bs-toggle="pill" data-bs-target="#pills-richieste" type="button" role="tab" aria-controls="pills-richieste" aria-selected="false">Richieste Modifica</Button>
            </li>
        End If

    End If
</ul>
<div Class="tab-content" id="pills-tabContent">
    @If User.IsInRole("TecnicoVisualizzazione") Or User.IsInRole("Tecnico") Or User.IsInRole("TecnicoAdmin") Or User.IsInRole("TecnicoRevisione") Or User.IsInRole("Admin") Then
        @<div Class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
            <Table id="mainDataTableProgetti" Class="stripe">
                <thead>
                    <tr>
                        <th>

                        </th>
                        <th>
                            Priorita
                        </th>
                        <th>
                            Data Ins.
                        </th>
                        <th>
                            Data Rich. Cons.
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
                        @*<th>
                                Richiesta Materiali
                            </th>*@

                    </tr>
                </thead>
                <tbody>
                </tbody>
            </Table>

        </div>
        @<div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
            <table id="mainDataTableProgettiCompletati" class="stripe" style="width:100%!important;">
                <thead>
                    <tr>
                        <th>

                        </th>
                        <th>
                            Priorita
                        </th>
                        <th>
                            Data Ins.
                        </th>
                        <th>
                            Data Rich. Cons.
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
                </tbody>
            </table>
        </div>
    End If
    @If User.IsInRole("ProgrammazioneEsterno") Or User.IsInRole("ProgrammazioneInterno") Then
        @<div Class="tab-pane fade show active" id="pills-esterni-in-attesa" role="tabpanel" aria-labelledby="pills-esterni-in-attesa-tab" style="overflow-x:scroll;">
            <Table id="mainDataTableProgettiEsterniInAttesa" Class="stripe" style="width:100%!important;">
                <thead>
                    <tr>
                        <th>

                        </th>
                        <th>
                            Priorita
                        </th>

                        <th>
                            Data Rich. Cons.
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
                </tbody>
            </Table>
        </div>
    ElseIf User.IsInRole("Tecnico") Or User.IsInRole("TecnicoAdmin") Or User.IsInRole("TecnicoRevisione") Or User.IsInRole("Admin") Then
        @<div Class="tab-pane fade" id="pills-esterni-in-attesa" role="tabpanel" aria-labelledby="pills-esterni-in-attesa-tab">
            <Table id="mainDataTableProgettiEsterniInAttesa" Class="stripe" style="width:100%!important;">
                <thead>
                    <tr>
                        <th>

                        </th>
                        <th>
                            Priorita
                        </th>
                        <th>
                            Data Rich. Cons.
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
                </tbody>
            </Table>
        </div>
    End If
    @If User.IsInRole("ProgrammazioneEsterno") Or User.IsInRole("ProgrammazioneInterno") Or User.IsInRole("Tecnico") Or User.IsInRole("TecnicoAdmin") Or User.IsInRole("TecnicoRevisione") Or User.IsInRole("Admin") Then
        @<div Class="tab-pane fade" id="pills-esterni" role="tabpanel" aria-labelledby="pills-esterni-tab">
            <Table id="mainDataTableProgettiEsterniCompletati" Class="stripe" style="width:100%!important;">
                <thead>
                    <tr>
                        <th>

                        </th>
                        <th>
                            Priorita
                        </th>
                        <th>
                            Data Rich. Cons.
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
                </tbody>
            </Table>
        </div>
    End If
    @If User.IsInRole("Tecnico") Or User.IsInRole("Produzione") Or User.IsInRole("Admin") Or User.IsInRole("TecnicoAdmin") Then
        @If User.IsInRole("Produzione") Then
            @<div Class="tab-pane fade show active" id="pills-richieste" role="tabpanel" aria-labelledby="pills-richieste-tab">
                <Table id="mainDataTableRichieste" Class="stripe" style="width:100%!important;">
                    <thead>
                        <tr>
                            <th>

                            </th>
                            <th>
                                Data Inserimento
                            </th>
                            <th>
                                Codice OC
                            </th>
                            <th>
                                Codice OP
                            </th>
                            <th>
                                Articolo
                            </th>
                            <th>
                                Completata
                            </th>

                        </tr>
                    </thead>

                    <tbody>
                    </tbody>
                </Table>
                <div class="row text-center">
                    <div class="col">
                        <button type="button" data-type="add_ric" id="add_ric" class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                            Aggiungi richiesta
                        </button>
                    </div>
                </div>

            </div>
        Else
            @<div Class="tab-pane fade" id="pills-richieste" role="tabpanel" aria-labelledby="pills-richieste-tab">
                <Table id="mainDataTableRichieste" Class="stripe" style="width:100%!important;">
                    <thead>
                        <tr>
                            <th>

                            </th>
                            <th>
                                Data Inserimento
                            </th>
                            <th>
                                Codice OC
                            </th>
                            <th>
                                Codice OP
                            </th>
                            <th>
                                Articolo
                            </th>
                            <th>
                                Completata
                            </th>

                        </tr>
                    </thead>

                    <tbody>
                    </tbody>
                </Table>
                <div class="row text-center">
                    <div class="col">
                        <button type="button" data-type="add_ric" id="add_ric" class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                            Aggiungi richiesta
                        </button>
                    </div>
                </div>

            </div>
        End If
    End If
</div>
