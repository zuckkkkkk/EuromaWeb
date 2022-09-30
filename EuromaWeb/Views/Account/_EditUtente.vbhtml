@ModelType ProfileViewModel
@Using (Html.BeginForm("_EditUtente", "Account", FormMethod.Post, New With {.class = "ModalForm", .onsubmit = "Codifica();"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <div class="row">
            <div class="col-md-8">
                @Html.Label("Soprannome", htmlAttributes:=New With {.class = "control-label col"})
                @Html.TextBox("Soprannome", Model.Soprannome, htmlAttributes:=New With {.class = "form-control", .required = "required"})
            </div>
            <div class="col-md-4">
                @Html.Label("Email", htmlAttributes:=New With {.class = "control-label col"})
                <div class="checkbox">
                    @Html.EditorFor(Function(model) model.Email, New With {.htmlAttributes = New With {.style = "width: 30px; height: 30px;"}})
                </div>
            </div>
    </div>
    <div class="form-row">
        <div class="form-group col">
            @Html.Label("Password Email", htmlAttributes:=New With {.class = "control-label col"})
            <div class="col-md-12">
                @Html.TextBox("Password", Model.PWD_Email, htmlAttributes:=New With {.class = "form-control", .required = "required"})
            </div>
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col">
            @Html.Label("Firma", htmlAttributes:=New With {.class = "control-label col"})
            <div class="col-md-12">
                @Html.TextArea("Firma", Model.Firma, htmlAttributes:=New With {.id = "summernote", .class = "form-control", .required = "required"})
            </div>
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col">
            @Html.Label("Percorso_Ricerca", htmlAttributes:=New With {.class = "control-label col"})
            <div class="col-md-12">
                @Html.TextBox("Percorso_Ricerca", Model.Percorso_Ricerca, htmlAttributes:=New With {.class = "form-control", .required = "required"})
            </div>
        </div>
    </div>
</div>      End Using

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