@ModelType EuromaWeb.ProgettiUT

@Using (Html.BeginForm("EditAdmin", "ProgettiUT", Nothing, FormMethod.Post, New With {.class = "ModalForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        @Html.HiddenFor(Function(model) model.Id)

        <div class="row">
            <div class="col-md-12">
                @Html.LabelFor(Function(model) model.Operatore, htmlAttributes:=New With {.class = "control-label col-md-2"})
                @Html.DropDownList("Operatore", Nothing, htmlAttributes:=New With {.class = "form-control", .required = "required", .style = "max-width:none!important;"})
                @Html.ValidationMessageFor(Function(model) model.Operatore, "", New With {.class = "text-danger"})
            </div>
            @*<div class="col-md-6 d-none">
                <label for="Flag_Invio_Materiali">Invio Materiali</label>
                <div class="checkbox">
                    @Html.EditorFor(Function(model) model.Flag_Invio_Materiali)
                    @Html.ValidationMessageFor(Function(model) model.Flag_Invio_Materiali, "", New With {.class = "text-danger"})
                </div>
            </div>*@
        </div>

        <div class="row">
            <div class="col-md-12">
                @Html.LabelFor(Function(model) model.Note, htmlAttributes:=New With {.class = "control-label col-md-2"})
                @Html.TextAreaFor(Function(model) model.Note, htmlAttributes:=New With {.rows = "5", .class = "form-control", .style = "max-width: none;"})
                @Html.ValidationMessageFor(Function(model) model.Note, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="row">
            <div class="col-md-11">
               
            </div>
        </div>
    </div>
End Using

