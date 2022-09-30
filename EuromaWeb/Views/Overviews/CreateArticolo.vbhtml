@ModelType EuromaWeb.ArticoliMagazzino

@Using (Html.BeginForm("CreateArticolo", "Overviews", Nothing, FormMethod.Post, New With {.class = "ModalForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <div class="form-row">
        <div class="row">
            <div class="col-md-4">
                <label for="codArticolo">Codice:</label>
                @Html.EditorFor(Function(model) model.codArticolo, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.codArticolo, "", New With {.class = "text-danger"})
            </div>
            <div class="col-md-4">
                <label for="qta">Quantità:</label>
                @Html.EditorFor(Function(model) model.qta, New With {.htmlAttributes = New With {.class = "form-control", .type = "number"}})
                @Html.ValidationMessageFor(Function(model) model.qta, "", New With {.class = "text-danger"})
            </div>
            <div class="col-md-4">
                <label for="idSlot">Slot:</label>
                @Html.DropDownList("idSlot", Nothing, "Non Impostato", htmlAttributes:=New With {.class = "form-control"})
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
</div>
End Using
