@ModelType EuromaWeb.RichiesteViewModel
<div class="row text-center">
    <div class="col">
        @If User.IsInRole("Tecnico") Or User.IsInRole("TecnicoAdmin") Or User.IsInRole("Admin") Then
            @<button type="button" data-type="edit_tecnico_richiesta" data-value="@Model.Id" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                Modifica stato
                <i class="fa-solid fa-file-pen"></i>
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
                                @Html.DisplayNameFor(Function(model) model.Cod_OC)
                                @Html.EditorFor(Function(model) model.Cod_OC, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>
                            <div class="col-md-6">
                                @Html.DisplayNameFor(Function(model) model.Cod_OP)
                                @Html.EditorFor(Function(model) model.Cod_OP, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                @Html.DisplayNameFor(Function(model) model.Articolo)
                                @Html.EditorFor(Function(model) model.Articolo, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>
                            <div class="col-md-6">
                                @Html.DisplayNameFor(Function(model) model.UltimaModifica.Operatore)
                                @Html.EditorFor(Function(model) model.UltimaModifica.Operatore, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                @Html.DisplayNameFor(Function(model) model.UltimaModifica.Data)
                                @Html.EditorFor(Function(model) model.UltimaModifica.Data, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

                            </div>
                        </div>
                        <dd>
                            @Html.DisplayNameFor(Function(model) model.Note_Produzione)
                            @Html.TextAreaFor(Function(model) model.Note_Produzione, htmlAttributes:=New With {.class = "form-control", .style = "max-width: none;", .readonly = "readonly", .rows = "5"})
                        </dd>
                        <dd>
                            @Html.DisplayNameFor(Function(model) model.Note_UT)
                            @Html.TextAreaFor(Function(model) model.Note_UT, htmlAttributes:=New With {.class = "form-control", .style = "max-width: none;", .readonly = "readonly", .rows = "5"})
                        </dd>
                    </dl>
                </div>
            </div>
        </div>
    </div>



</div>