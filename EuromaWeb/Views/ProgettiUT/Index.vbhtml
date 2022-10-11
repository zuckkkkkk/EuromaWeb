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

        #mainDataTableProgettiEsterniInAttesa_filter {
            position: fixed;
            z-index: 1000;
            background-color: white;
            bottom: 0px;
            width: 100vw;
            left: 0;
            padding: 8px;
        }

        #pills-esterni-in-attesa, #pills-esterni {
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
            @For Each item In Model
            @<tr>
                <td>
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Priorita)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.DataRichiestaConsegna)
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
                @*<td>
                        @Html.DisplayFor(Function(modelItem) item.Flag_Invio_Materiali, New With {.htmlAttributes = New With {.style = "width: 30px; height: 30px;"}})
                    </td>*@

            </tr>
                                Next
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
            @For Each item In Model
            @<tr>
                <td>
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Priorita)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.DataRichiestaConsegna)
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
            @For Each item In Model
            @<tr>
                <td>
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Priorita)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.DataRichiestaConsegna)
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
            @For Each item In Model
            @<tr>
                <td>
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Priorita)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.DataRichiestaConsegna)
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
            @For Each item In Model
            @<tr>
                <td>
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Priorita)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.DataRichiestaConsegna)
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
End If

</div>
