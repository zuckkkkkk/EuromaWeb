@ModelType EuromaWeb.ProgettoViewModel
@Code
    ViewData("Title") = "Crea Progetto"
End Code

<h2>Creazione Progetto</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <div class="row form-group">
        <div class="col-md-6">
            <div class="col-md-12 p-0" style="padding: 0!important;">
                @Html.LabelFor(Function(model) model.OC, htmlAttributes:=New With {.class = "control-label "})
            </div>
            @Html.EditorFor(Function(model) model.OC, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.OC, "", New With {.class = "text-danger"})
        </div>
        <div class="col-md-6">
            <div class="col-md-12 p-0" style="padding: 0!important;">
                @Html.LabelFor(Function(model) model.EndDate, htmlAttributes:=New With {.class = "control-label"})
            </div>
            @Html.EditorFor(Function(model) model.EndDate, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.EndDate, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="row form-group">
        <div class="col-md-6">
            <div class="col-md-12 p-0" style="padding: 0!important;">
                @Html.LabelFor(Function(model) model.Cliente, htmlAttributes:=New With {.class = "control-label"})
            </div>
            @Html.EditorFor(Function(model) model.Cliente, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.Cliente, "", New With {.class = "text-danger"})
        </div>
        <div class="col-md-6">
            <div class="col-md-12 p-0" style="padding: 0!important;">
                @Html.LabelFor(Function(model) model.Brand, htmlAttributes:=New With {.class = "control-label"})
            </div>
            @Html.EnumDropDownListFor(Function(model) model.Brand, htmlAttributes:=New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(model) model.Brand, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-6">
            <div class="col-md-12 p-0" style="padding: 0!important;">
            @Html.LabelFor(Function(model) model.Codice, htmlAttributes:=New With {.class = "control-label p-0"})
            </div>
            @Html.EditorFor(Function(model) model.Codice, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.Codice, "", New With {.class = "text-danger"})
        </div>
        <div class="col-md-6">
            <div class="col-md-12 p-0" style="padding: 0!important;">
                @Html.LabelFor(Function(model) model.Stato_Pezzo, htmlAttributes:=New With {.class = "control-label"})
            </div>
            @Html.EnumDropDownListFor(Function(model) model.Stato_Pezzo, htmlAttributes:=New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(model) model.Stato_Pezzo, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-6">
            <div class="col-md-12 p-0" style="padding: 0!important;">
                @Html.LabelFor(Function(model) model.Affidato, htmlAttributes:=New With {.class = "control-label"})
            </div>
            @Html.EnumDropDownListFor(Function(model) model.Affidato, htmlAttributes:=New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(model) model.Affidato, "", New With {.class = "text-danger"})
        </div>
        <div class="col-md-6">
            <div class="col-md-12 p-0" style="padding: 0!important;">
                @Html.LabelFor(Function(model) model.Previsione, htmlAttributes:=New With {.class = "control-label"})
            </div>
            @Html.EditorFor(Function(model) model.Previsione, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.Previsione, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-6">
            <div class="col-md-12 p-0" style="padding: 0!important;">
                @Html.LabelFor(Function(model) model.Note_Generiche, htmlAttributes:=New With {.class = "control-label"})
            </div>
            @Html.EditorFor(Function(model) model.Note_Generiche, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.Note_Generiche, "", New With {.class = "text-danger"})
        </div>
        <div class="col-md-6">
            <div class="col-md-12 p-0" style="padding: 0!important;">
                @Html.LabelFor(Function(model) model.Note_Pezzo, htmlAttributes:=New With {.class = "control-label"})
            </div>
            @Html.EditorFor(Function(model) model.Note_Pezzo, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.Note_Pezzo, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-12">
            <input type="submit" value="Create" class="btn btn-default" />
        </div>
    </div>
</div>
End Using
