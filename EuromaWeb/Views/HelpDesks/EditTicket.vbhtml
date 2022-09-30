@ModelType EuromaWeb.HelpDesk


@Using (Html.BeginForm("EditTicket", "HelpDesks", Nothing, FormMethod.Post, New With {.class = "ModalForm", .onsubmit = "Codifica();"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">

    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    @Html.HiddenFor(Function(model) model.Id)


    <div class="row">
        <div class="col-md-12">
            <label class="control-label col-md-4" for="Title">Titolo:</label>
            @Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <label class="control-label col-md-4" for="Request_Type">Tipologia Richiesta:</label>
            <div class="checkbox">
                @Html.EnumDropDownListFor(Function(model) model.Request_Type, htmlAttributes:=New With {.class = "form-control", .style = "max-width: none;"})
                @Html.ValidationMessageFor(Function(model) model.Request_Type, "", New With {.class = "text-danger"})
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <label class="control-label col-md-4" for="Stato_Ticket">Stato Ticket:</label>
            <div class="checkbox">
                @Html.EnumDropDownListFor(Function(model) model.Stato_Ticket, htmlAttributes:=New With {.class = "form-control", .style = "max-width: none;"})
                @Html.ValidationMessageFor(Function(model) model.Stato_Ticket, "", New With {.class = "text-danger"})
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <label class="control-label col-md-4" for="Body">Domanda:</label>
            @Html.TextAreaFor(Function(model) model.Body, htmlAttributes:=New With {.id = "summernote", .class = "form-control", .style = "max-width: none;"})
            @Html.ValidationMessageFor(Function(model) model.Body, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <label class="control-label col-md-4" for="Body">Risposta:</label>
            @Html.TextAreaFor(Function(model) model.Body_Risposta, htmlAttributes:=New With {.id = "summernote_risposta", .class = "form-control", .style = "max-width: none;"})
            @Html.ValidationMessageFor(Function(model) model.Body_Risposta, "", New With {.class = "text-danger"})
        </div>
    </div>

</div>
End Using

<script>
    $('#summernote').summernote({
        dialogsInBody: true,
        height: 300
    });
    $('#summernote').summernote('disable')
    $('#summernote_risposta').summernote({
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