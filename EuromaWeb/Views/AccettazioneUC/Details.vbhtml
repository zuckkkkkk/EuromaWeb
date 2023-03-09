@ModelType EuromaWeb.AccettazioneUCViewModel



<div class="row text-center">
    <div class="col">
        <btn class="btn btn-secondary" id="btn_Details_Accettazione_Guide" style="height: auto; width: auto; padding: auto;">Aiuto <i class="fa-solid fa-circle-question"></i> </btn>
        <button onclick="AddNota('@Model.OC');" type="button" data-value="@Model.OC" Class="btn btn-primary w-auto">
            Aggiungi Nota...
            <i class="fa-solid fa-message-lines"></i>        
        </button>
         <button onclick="AddFile('@Model.OC');" type="button" data-value="@Model.OC" Class="btn btn-info w-auto">
            Aggiungi File...
            <i class="fa-solid fa-message-lines"></i>        
        </button>
        @If Model.Accettato = Stato_UC.Accettato Then
            @<button type="button" onclick="DownloadFirmaFile(@Model.Id);" Class="btn btn-dark w-auto">
                Download
                <i class="fa-solid fa-file-arrow-down"></i>
            </button>
        Else
            @<button type="button" onclick="DownloadFirmaFileRaw(@Model.Id);" Class="btn btn-dark w-auto">
                Download
                <i class="fa-solid fa-file-arrow-down"></i>
            </button>
        End If
        @If (User.IsInRole("Commerciale_Admin") Or User.IsInRole("Commerciale_Utente") Or User.IsInRole("Admin")) Then 'And (Model.Accettato = Stato_UC.Accettato Or Model.Accettato = Stato_UC.Inviato)

            @If not Model.SenttoUC Then
                @<button onclick="SendToUTPrompt(@Model.Id);" type="button" data-value="@Model.Id" Class="btn btn-primary w-auto">
                    Invia
                    <i class="fa-solid fa-paper-plane"></i>
                </button>
            Else
                @<button Class="btn btn-primary w-auto disabled " data-toggle="tooltip" data-placement="top" title="Documento già inviato!">
                    Invia
                    <i class="fa-solid fa-paper-plane"></i>
                </button>

            End If
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
                                @Html.DisplayNameFor(Function(model) model.OC)
                                @Html.EditorFor(Function(model) model.OC, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>
                            <div class="col-md-6">
                                @Html.DisplayNameFor(Function(model) model.Cartella)
                                @Html.EditorFor(Function(model) model.Cartella, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                Inserito da :
                                @Html.EditorFor(Function(model) model.OperatoreInsert, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>
                            <div class="col-md-6">
                                Accettato da :
                                @Html.EditorFor(Function(model) model.OperatoreAccettazione, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                Brand:
                                @Html.EditorFor(Function(model) model.Brand, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>
                            <div class="col-md-6">
                                Costo Maggiorato:

                                @If Model.PrezzoMaggiorato Then
                                    @<input Class="form-control text-box single-line" id="PrezzoMaggiorato" name="PrezzoMaggiorato" readonly="readonly" type="text" value="Si">
                                Else
                                    @<input Class="form-control text-box single-line" id="PrezzoMaggiorato" name="PrezzoMaggiorato" readonly="readonly" type="text" value="No">
                                End If
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">

                                Creato il :
                                @Html.EditorFor(Function(model) model.DataCreazione, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>
                            <div class="col-md-6">
                                Ultima modifica accettazione:
                                @Html.EditorFor(Function(model) model.DataAccettazione, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                Priorita :
                                @Html.EditorFor(Function(model) model.Priorita, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
                            </div>
                            <div class="col-md-6">
                                Data richiesta consegna:
                                @Html.EditorFor(Function(model) model.DataPrevistaConsegna, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

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
<script>
    $('#btn_Details_Accettazione_Guide').guides({
        guides: [{
            element: $('#OC'),
            html: 'Questo è il codice della Conferma/Offerta!'
        }, {
            element: $('#Cartella'),
            html: 'Questo è il percorso della cartella legata a un dato OC.'
        },
        {
            element: $('#OperatoreInsert'),
            html: "Nome dell'operatore che ha inserito l'OC nel portale."
        }, {
            element: $('#OperatoreAccettazione'),
            html: "Nome dell'operatore che ha accettato l'OC nel portale."
        }, {
            element: $('#DataCreazione'),
            html: 'Data di creazione su questo portale di un dato OC!'
        }, {
            element: $('#DataAccettazione'),
            html: 'Data di accettazione su questo portale di un dato OC!'
        }, {
            element: $('#Accettato'),
            html: "E' stato accettato oppure no?"
        }, {
            element: $('#Note'),
            html: 'Note inserite dal capo!'
        }]
    });
</script>