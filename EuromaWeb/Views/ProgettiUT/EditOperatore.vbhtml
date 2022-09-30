@ModelType EuromaWeb.ProgettiUT_Operatore

@Using (Html.BeginForm("EditOperatore", "ProgettiUT", Nothing, FormMethod.Post, New With {.class = "ModalForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    @Html.HiddenFor(Function(model) model.Id)

    <div class="form-group">
        <div class="col-md-12">
            @* @Html.LabelFor(Function(model) model.OC_Riferimento, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
            <label for="OC_Riferimento">Doc. Riferimento</label>
            @Html.EditorFor(Function(model) model.OC_Riferimento, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly", .style = "width: 100%;"}})
            @Html.ValidationMessageFor(Function(model) model.OC_Riferimento, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-12">
            @Html.LabelFor(Function(model) model.Operatore, htmlAttributes:=New With {.class = "control-label col-md-2"})
            @Html.DropDownList("Operatore", Nothing, htmlAttributes:=New With {.class = "form-control", .required = "required", .style = "max-width:none!important;"})
            @Html.ValidationMessageFor(Function(model) model.Operatore, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-12">
            <label for="StatoProgetto">Data Retroattiva (se presente)</label>
            @Html.EditorFor(Function(model) model.DataRetroattiva, New With {.htmlAttributes = New With {.class = "form-control", .id = "DateRetroattiva", .style = "width: 100%;"}})
            @Html.ValidationMessageFor(Function(model) model.DataRetroattiva, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-12">
            @*@Html.LabelFor(Function(model) model.StatoProgetto, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
            <label for="StatoProgetto">Stato Progetto</label>
            @Html.EnumDropDownListFor(Function(model) model.StatoProgetto, htmlAttributes:=New With {.class = "form-control", .style = "max-width: none!important;"})
            @Html.ValidationMessageFor(Function(model) model.StatoProgetto, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-12">
            @Html.LabelFor(Function(model) model.Note, htmlAttributes:=New With {.class = "control-label col-md-2"})
            @Html.TextAreaFor(Function(model) model.Note, htmlAttributes:=New With {.rows = "5", .class = "form-control", .style = "max-width: none;"})
            @Html.ValidationMessageFor(Function(model) model.Note, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="container EditOperatoreTecnico">
        <h5 style="margin-top: 5px; margin-bottom: 16px; "> Distinta Base</h5>
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
                @For Each l In ViewBag.ListaArt
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

        <h5 style="margin-top: 16px; margin-bottom: 16px;"> Lista </h5>
        <div Class="row ">
            <div Class="col-md-6">
                <div Class="row" style=" display: flex; align-items: center;">
                    <div Class="col-md-6">
                        <Label for="Flag_4">Figurino</Label>
                    </div>
                    <div class="col-md-6">
                        @Html.EnumDropDownListFor(Function(model) model.Flag_4, htmlAttributes:=New With {.class = "form-control", .style = "max-width: none!important;"})
                        @Html.ValidationMessageFor(Function(model) model.Flag_4, "", New With {.class = "text-danger"})

                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row" style=" display: flex; align-items: center;">
                    <div class="col-md-6">
                        <label for="Flag_2">Anticipo Materiali</label>
                    </div>
                    <div class="col-md-6">
                        @Html.EnumDropDownListFor(Function(model) model.Flag_2, htmlAttributes:=New With {.class = "form-control", .style = "max-width: none!important;"})
                        @Html.ValidationMessageFor(Function(model) model.Flag_2, "", New With {.class = "text-danger"})

                    </div>
                </div>
            </div>
        </div>
        <div Class="row mt-2">
            <div class="col-md-6">
                <div class="row" style=" display: flex; align-items: center;">
                    <div class="col-md-6">
                        <label for="Flag_1">Controllo DB</label>

                    </div>
                    <div class="col-md-6">
                        @Html.EditorFor(Function(model) model.Flag_1)
                        @Html.ValidationMessageFor(Function(model) model.Flag_1, "", New With {.class = "text-danger"})

                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row" style=" display: flex; align-items: center;">
                    <div class="col-md-6">
                        <label for="Flag_3">Conformità disegno e DB</label>

                    </div>
                    <div class="col-md-6">
                        @Html.EditorFor(Function(model) model.Flag_3)
                        @Html.ValidationMessageFor(Function(model) model.Flag_3, "", New With {.class = "text-danger"})

                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <h5 style="margin-top: 16px; margin-bottom: 16px;"> Progetto PDF: </h5>
            <div class="dropzone" id="ProgettoFileUpload"></div>
        </div>
    </div>


</div>
End Using

<script>

    $('#StatoProgetto').on('change', function () {
        var val = this.value;
        if (val == '90') {
            console.log("here");
            $(".EditOperatoreTecnico").show();
        } else {
            console.log("there");
            $(".EditOperatoreTecnico").hide();

        }
    });
    $('#StatoProgetto').on('load', function () {
        var val = this.value;
        if (val == '90') {
            console.log("here");
            $(".EditOperatoreTecnico").show();
        } else {
            console.log("there");
            $(".EditOperatoreTecnico").hide();

        }
    });
    $('.dropzone').dropzone();
    $("#DateRetroattiva").flatpickr({
        enableTime: true,
        minTime: "09:00",
        maxTime: '18:00',
        minDate: "2020-01"
    });
</script>