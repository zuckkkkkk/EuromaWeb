@ModelType EuromaWeb.ProgettiProdViewModel
<div id="childDatatableProduzione">
    <div class="row text-center">
        <div class="col">
            <button onclick="AddNota('@Model.OC_Riferimento');" type="button" data-value="@Model.OC_Riferimento" Class="btn btn-primary w-auto">
                Aggiungi Nota...
                <i class="fa-solid fa-message-lines"></i>
            </button>
            <button onclick="AddFile('@Model.OC_Riferimento');" type="button" data-value="@Model.OC_Riferimento" Class="btn btn-info w-auto">
                Aggiungi File...
                <i class="fa-solid fa-message-lines"></i>
            </button>
            @If User.IsInRole("ProduzioneController") Or User.IsInRole("Admin") Then
                @<button type="button" data-type="edit_controller_produzione" data-value="@Model.Id" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                    Modifica
                    <i class="fa-solid fa-file-pen"></i>
                </button>
                @<button onclick="SendDirectToUTPrompt(@Model.Id);" type="button" data-value="@Model.Id" Class="btn btn-primary w-auto">
                    Invia a UT
                    <i class="fa-solid fa-paper-plane"></i>
                </button>
            @<button type = "button" onclick="DownloadFirmaFileRaw(@ViewBag.Id_OC);" Class="btn btn-dark w-auto">
                Scarica OC
                <i Class="fa-solid fa-file-arrow-down"></i>
            </button>
            End If
        </div>
    </div>
    <div style="margin-top:16px;">
        <div class="accordion " id="accordionFlushExample">
            <div class="accordion-item">
                <h2 class="accordion-header" id="flush-headingOne_@Html.Raw(Model.Id)">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseOne_@Html.Raw(Model.Id)" aria-expanded="false" aria-controls="flush-collapseOne_@Html.Raw(Model.Id)">
                        Dettagli
                    </button>
                </h2>
                <div id="flush-collapseOne_@Html.Raw(Model.Id)" class="accordion-collapse collapse" aria-labelledby="flush-headingOne_@Html.Raw(Model.Id)" data-bs-parent="#accordionFlushExample">
                    <div class="accordion-body">

                        <dl Class="dl-horizontal">
                            <div class="row">
                                <div class="col-md-6">
                                    @Html.DisplayNameFor(Function(model) model.OC_Riferimento)
                                    @Html.EditorFor(Function(model) model.OC_Riferimento, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                                </div>
                                <div class="col-md-6">
                                    @Html.DisplayNameFor(Function(model) model.DataCreazione)
                                    @Html.EditorFor(Function(model) model.DataCreazione, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    Creato il :
                                    @Html.EditorFor(Function(model) model.DataCreazione, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                                </div>
                                <div class="col-md-6">
                                    Ultima modifica progetto:
                                    @Html.EditorFor(Function(model) model.DataCompletamento, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
                                </div>
                            </div>
                            <dt>
                                @Html.DisplayNameFor(Function(model) model.Note)
                            </dt>

                            <dd>
                                @Html.TextAreaFor(Function(model) model.Note, htmlAttributes:=New With {.class = "form-control", .style = "max-width: none;", .readonly = "readonly", .rows = "5"})
                            </dd>

                        </dl>
                    </div>
                </div>
            </div>
            <div class="accordion-item">
                <h2 class="accordion-header" id="flush-headingTwo_@Html.Raw(Model.Id)">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseTwo_@Html.Raw(Model.Id)" aria-expanded="false" aria-controls="flush-collapseTwo_@Html.Raw(Model.Id)">
                        Distinte basi
                    </button>
                </h2>
                <div id="flush-collapseTwo_@Html.Raw(Model.Id)" class="accordion-collapse collapse" aria-labelledby="flush-headingTwo_@Html.Raw(Model.Id)" data-bs-parent="#accordionFlushExample">
                    <div class="accordion-body">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <td>
                                        Codice Articolo
                                    </td>
                                    <td>
                                        Descrizione
                                    </td>
                                    <td>
                                        Distinta Presente
                                    </td>
                                </tr>
                            </thead>
                            <tbody>
                                @For Each l In Model.ListaArt
                                    @<tr>
                                        <td>
                                            @l.Cod_Art
                                        </td>
                                        <td>
                                            @l.Descrizione
                                        </td>
                                        <td>
                                            @If l.DistintaBase Then
                                                @<i class="fa-solid fa-check"></i>
                                            Else
                                                @<i class="fa-solid fa-xmark"></i>
                                            End If
                                        </td>
                                    </tr>
                                Next

                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
            <div class="accordion-item">
                <h2 class="accordion-header" id="flush-headingThree_@Html.Raw(Model.Id)">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseThree_@Html.Raw(Model.Id)" aria-expanded="false" aria-controls="flush-collapseThree_@Html.Raw(Model.Id)">
                        Note   <span class="badge bg-danger" style="border-radius:32px; margin-left:8px;">@Model.ListOfNote.Count</span>
                    </button>
                </h2>
                <div id="flush-collapseThree_@Html.Raw(Model.Id)" class="accordion-collapse collapse" aria-labelledby="flush-headingThree_@Html.Raw(Model.Id)" data-bs-parent="#accordionFlushExample">
                    <div class="accordion-body">
                        @For Each mess In Model.ListOfNote
                            @<div Class="flex-shrink-1 bg-light rounded py-2 px-3 ml-3 mb-2 mt-1 ">
                                <div Class="font-weight-bold mb-1">@StrConv(mess.Operatore_Nome, VbStrConv.ProperCase), il @mess.Data_Nota.ToString.Split(" ")(0) alle @mess.Data_Nota.ToString.Split(" ")(1).Split(":")(0):@mess.Data_Nota.ToString.Split(" ")(1).Split(":")(1):</div>
                                @mess.Contenuto_Nota
                            </div>
                        Next

                    </div>
                </div>
            </div>
            <div class="accordion-item">
                <h2 class="accordion-header" id="flush-headingFour_@Html.Raw(Model.Id)">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseFour_@Html.Raw(Model.Id)" aria-expanded="false" aria-controls="flush-collapseFour_@Html.Raw(Model.Id)">
                        Documenti   <span class="badge bg-danger" style="border-radius:32px; margin-left:8px;">@Model.ListOfDocumenti.Count</span>
                    </button>
                </h2>
                <div id="flush-collapseFour_@Html.Raw(Model.Id)" class="accordion-collapse collapse" aria-labelledby="flush-headingFour_@Html.Raw(Model.Id)" data-bs-parent="#accordionFlushExample">
                    <div class="accordion-body">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <td> Nome File</td>
                                    <td> Proprietario</td>
                                    <td> Data Caricamento</td>
                                    <td> Download</td>
                                </tr>
                            </thead>
                            <tbody>
                                @For Each file In Model.ListOfDocumenti
                                    @<tr>
                                        <td>
                                            @file.Nome_File
                                        </td>
                                        <td>
                                            @file.Operatore_Nome
                                        </td>
                                        <td>
                                            @file.DataCreazioneFile
                                        </td>
                                        <td>
                                            <a href="@Url.Action("DownloadFile", "AccettazioneUC", New With {.id = file.Id})"> <i class="fa-solid fa-download"></i></a>
                                        </td>
                                    </tr>
                                Next

                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>



    </div>
</div>
