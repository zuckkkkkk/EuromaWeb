@ModelType EuromaWeb.HelpDesk
@Code
    ViewData("Title") = "Create"
End Code
@Using (Html.BeginForm("Create", "HelpDesks", Nothing, FormMethod.Post, New With {.class = "ModalForm", .onsubmit = "Codifica();"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

    <div class="row">
        <div class="col-md-12">
            <label class="control-label col-md-2" for="Title">Titolo richiesta:</label>
            @Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control", .required = "required"}})
            @Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <label class="control-label col-md-4" for="Request_Type">Tipologia richiesta:</label>
            @Html.EnumDropDownListFor(Function(model) model.Request_Type, htmlAttributes:=New With {.class = "form-control", .style = "max-width: none;", .required = "required"})
            @Html.ValidationMessageFor(Function(model) model.Request_Type, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <label class="control-label col-md-2" for="Body">Corpo:</label>
            @Html.TextAreaFor(Function(model) model.Body, htmlAttributes:=New With {.id = "summernote", .class = "form-control", .style = "max-width: none;", .required = "required"})
            @Html.ValidationMessageFor(Function(model) model.Body, "", New With {.class = "text-danger"})
        </div>
    </div>


</div>
End Using
<script>
    $('#summernote').summernote({
        toolbar: [
            // [groupName, [list of button]]
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['font', ['strikethrough', 'superscript', 'subscript']],
            ['fontsize', ['fontsize']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['height', ['height']]
        ],
        dialogsInBody: true,
        height: 300
    });
    $('.dropdown-toggle').dropdown();
</script>
