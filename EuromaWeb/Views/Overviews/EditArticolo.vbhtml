@ModelType EuromaWeb.ArticoliMagazzino

<h2 style="margin-top: 1rem;">Modifica articolo "@Model.codArticolo"</h2>

@Using (Html.BeginForm("EditArticolo", "Overviews", Nothing, FormMethod.Post))
    @Html.AntiForgeryToken()
    @Html.Hidden(Model.Id)
    @<div class="form-horizontal">
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <div class="form-row">
        <div class="row">
            <div class="col-md-6">
                <label for="codArticolo">Codice:</label>
                @Html.EditorFor(Function(model) model.codArticolo, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.codArticolo, "", New With {.class = "text-danger"})
            </div>
            <div class="col-md-6">
                <label for="qta">Quantità:</label>
                @Html.EditorFor(Function(model) model.qta, New With {.htmlAttributes = New With {.class = "form-control", .type = "number"}})
                @Html.ValidationMessageFor(Function(model) model.qta, "", New With {.class = "text-danger"})
            </div>
        </div>
    </div>
    <div class="form-row">
        <div class="row">
            <div class="col-md-12">
                <label for="noteArticolo">Descrizione Articolo:</label>
                @Html.TextAreaFor(Function(model) model.noteArticolo, htmlAttributes:=New With {.rows = "5", .class = "form-control", .style = "max-width: none;"})
                @Html.ValidationMessageFor(Function(model) model.noteArticolo, "", New With {.class = "text-danger"})
            </div>

        </div>
    </div>
    <input type="submit" value="Modifica" class="mt-2 btn btn-primary">

</div> 

End Using
