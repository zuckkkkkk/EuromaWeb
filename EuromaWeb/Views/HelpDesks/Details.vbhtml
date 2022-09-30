@ModelType EuromaWeb.HelpDesk
@Code
    ViewData("Title") = "Details"
End Code


<div>
    <dl class="dl-horizontal">

        <dt>
            Inserito il:
        </dt>

        <dd>
            @Html.EditorFor(Function(model) model.RequestDate, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
        </dd>

        <dt>
            Titolo:
        </dt>

        <dd>
            @Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})

        </dd>
        <dt>
            Tipologia richiesta:
        </dt>

        <dd>
            @Html.EnumDropDownListFor(Function(model) model.Request_Type, htmlAttributes:=New With {.class = "form-control", .style = "max-width: none;", .readonly = "readonly"})

        </dd>
        <dt>
            Domanda:
        </dt>

        <dd>
            @Html.TextAreaFor(Function(model) model.Body, htmlAttributes:=New With {.id = "summernote", .class = "form-control", .style = "max-width: none;", .readonly = "readonly"})
        </dd>
        <dt>
            Risposta:
        </dt>
        <dd>
            @Html.TextAreaFor(Function(model) model.Body_Risposta, htmlAttributes:=New With {.id = "summernote_risposta", .class = "form-control", .style = "max-width: none;", .readonly = "readonly"})
        </dd>

    </dl>
</div>

<script>
    $('#summernote').summernote({
        toolbar: []
    });
    $('#summernote').summernote('disable')
    $('#summernote_risposta').summernote({
        toolbar: []
    });
    $('#summernote_risposta').summernote('disable')
</script>