@ModelType EuromaWeb.RichiestaOLviewModel
@Code
    ViewData("Title") = "Lavorazioni Esterne"
End Code


<h2 style="margin-top: 1rem; margin-bottom: 1rem;">Lista OL</h2>
<table id="mainDataTable" class="stripe">
    <thead>
        <tr>
            <th>
                Status
            </th>
            <th>
                OL
            </th>
            <th>
                Email
            </th>
            <th>
                Operatore
            </th>
            <th>
                Data Inserimento
            </th>
            <th>
                Azioni
            </th>
        </tr>
    </thead>

    <tbody>
                @For Each item In Model.List
                    @<tr>
                         <td>
                             @Select item.Inviato
                                 Case 1
                                      @<span class="badge bg-success">Testato</span>
                                 Case 2
                                      @<span class="badge bg-success">Inviato</span>
                                 Case Else
                                      @<span class="badge bg-info text-dark">In attesa</span>
                             End Select
                         </td>
                         <td>

                             @Html.DisplayFor(Function(modelItem) item.OL)
                         </td>
                         <td>
                             @Html.DisplayFor(Function(modelItem) item.Email)
                         </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Operatore)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Data_Inserimento)
            </td>
            <td>
                @If Not IsNothing(item.Path_Doc) Then
                    @<a id="DOC_@item.Id" href="@Url.Action("DownloadFile", "Acquisti", New With {.id = item.Id})" target="_blank" data-toggle="tooltip" data-placement="top" title="Scarica bolla"><i Class="fa-solid fa-file-invoice"></i></a>
                End If
                @If Not IsNothing(item.Path_DDT) Then
                    @<a id="DDT_@item.Id" href="@Url.Action("DownloadFileDDT", "Acquisti", New With {.id = item.Id})" target="_blank" data-toggle="tooltip" data-placement="top" title="Scarica DDT"><i Class="fa-solid fa-truck"></i></a>
                End If
                @If item.Inviato = Enum_Bolla.In_attesa Then
                    @<btn class="btn-primary btn" data-value="@item.Id" id="sendOLtoFornitore" data-toggle="tooltip" data-placement="top" title="Invia a fornitore"><i class="fa-solid fa-paper-plane"></i></btn>
                    '<btn class="btn-primary btn" data-value="@item.Id" id="sendOLtoInterno" data-toggle="tooltip" data-placement="top" title="Invia a te stesso"><i class="fa-solid fa-circle-check"></i></btn>
                    @<div id="loading_@item.Id" style="display: none; align-content: center; justify-content: center; overflow: hidden; height: 64px; width: 64px; ">
                        <img src="/Asset/loading.gif" />
                    </div>
                End If
                @If item.Inviato = Enum_Bolla.Test_interno Then
                    @<btn class="btn-primary btn" data-value="@item.Id" id="sendOLtoFornitore" data-toggle="tooltip" data-placement="top" title="Invia a fornitore"><i class="fa-solid fa-paper-plane"></i></btn>
                    @<div id="loading_@item.Id" style="display: none; align-content: center; justify-content: center; overflow: hidden; height: 64px; width: 64px; ">
                        <img src="/Asset/loading.gif" />
                    </div>
                End If
                @If item.Inviato = Enum_Bolla.Inviato Then
                    @*@<btn class="btn-primary btn" data-value="@item.Id" id="sendOLtoInterno" data-toggle="tooltip" data-placement="top" title="Invia a te stesso"><i class="fa-solid fa-circle-check"></i></btn>
            @<div id="loading_@item.Id" style="display: none; align-content: center; justify-content: center; overflow: hidden; height: 64px; width: 64px; ">
                <img src="/Asset/loading.gif" />
            </div>*@
                End If
                <btn class="btn-primary btn"onclick="DeleteOL(@item.Id)" data-toggle="tooltip" data-placement="top" title="cancella"><i class="fa-solid fa-trash"></i></btn>


            </td>
        
        </tr>
                Next
    </tbody>

</table>
<div class="accordion mt-3" id="accordionExample">
    <div class="accordion-item">
        <h2 class="accordion-header" id="headingOne">
            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne"  aria-controls="collapseOne">
                Inserimento Lav.Est. manuale
            </button>
        </h2>
        <div id="collapseOne" class="accordion-collapse collapse" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
            <div class="accordion-body">
                @Using (Html.BeginForm("LavorazioniEsterne", "Acquisti", Nothing, FormMethod.Post, New With {.class = "LavEst"})) ', New With {.class = "ModalForm", .onsubmit = "Codifica();"}
                    @Html.AntiForgeryToken()

                    @<div class="form-horizontal">
                        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                        <h2 id="Titolo" style="margin-top: 1rem; margin-bottom: 1rem;">Lavorazioni Esterne </h2> @*<btn class="btn btn-secondary" id="btn_lavorazioni_esterne"> ? </btn>*@
                        <div class="row w-100 ">
                            <div class="col-md-4">
                                <label class="control-label col-md-2" for="Title">Esercizio:</label>
                                @Html.EditorFor(Function(model) model.Esecod, New With {.htmlAttributes = New With {.class = "form-control", .required = "required"}})
                                @Html.ValidationMessageFor(Function(model) model.Esecod, "", New With {.class = "text-danger"})
                            </div>
                            <div class="col-md-4">
                                <label class="control-label col-md-2" for="Title">OL:</label>
                                @Html.EditorFor(Function(model) model.OL, New With {.htmlAttributes = New With {.class = "form-control", .required = "required"}})
                                @Html.ValidationMessageFor(Function(model) model.OL, "", New With {.class = "text-danger"})
                            </div>
                            <div class="col-md-4">
                                <label class="control-label col-md-2" for="Title">Num:</label>
                                @Html.EditorFor(Function(model) model.Num, New With {.htmlAttributes = New With {.class = "form-control", .required = "required"}})
                                @Html.ValidationMessageFor(Function(model) model.Num, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                        <div class="row w-100">
                            <div class="col-md-10">
                                <label class="control-label col-md-2" for="Title">Email:</label>
                                @Html.EditorFor(Function(model) model.email, New With {.htmlAttributes = New With {.class = "form-control", .required = "required"}})
                                @Html.ValidationMessageFor(Function(model) model.email, "", New With {.class = "text-danger"})
                            </div>
                            <div class="col-md-1">
                                <label class="control-label col-md-12" for="FlagMailAuto">S.a.m:</label>
                                <div class="checkbox">
                                    @Html.CheckBoxFor(Function(model) model.FlagMailAuto, New With {.style = "height:38px; width:38px; border: 1px solid #ced4da!important;"})
                                    @Html.ValidationMessageFor(Function(model) model.FlagMailAuto, "", New With {.class = "text-danger"})
                                </div>
                            </div>
                            <div class="col-md-1">
                                <label class="control-label col-md-12" for="FlagBoth">DDT</label>
                                <div class="checkbox">
                                    @Html.CheckBoxFor(Function(model) model.FlagBoth, New With {.style = "height:38px; width:38px; border: 1px solid #ced4da!important;"})
                                    @Html.ValidationMessageFor(Function(model) model.FlagBoth, "", New With {.class = "text-danger"})
                                </div>
                            </div>
                        </div>
                        <button class="mt-2 btn btn-primary">Invia a fornitore</button>
                    </div>

                End Using
            </div>
        </div>
    </div>
</div>
