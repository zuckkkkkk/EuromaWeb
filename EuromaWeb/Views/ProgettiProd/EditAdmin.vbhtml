@ModelType EuromaWeb.ProgettiProd

@Using (Html.BeginForm("EditAdmin", "ProgettiProd", Nothing, FormMethod.Post, New With {.class = "ModalForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        @Html.HiddenFor(Function(model) model.Id)

        <div class="form-group">
            <div class="col-md-12">
                <label for="OC_Riferimento">Doc. Riferimento</label>
                @Html.EditorFor(Function(model) model.OC_Riferimento, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly", .style = "width: 100%;"}})
                @Html.ValidationMessageFor(Function(model) model.OC_Riferimento, "", New With {.class = "text-danger"})
            </div>
        </div>



        <div class="form-group">
            <div class="col-md-12">
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

    </div>  End Using
