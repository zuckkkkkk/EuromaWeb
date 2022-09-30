@ModelType EuromaWeb.AccettazioneUC

@Using (Html.BeginForm("Edit", "AccettazioneUC", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data", .class = "ModalForm"}))

    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        @Html.HiddenFor(Function(model) model.Id)

         <div class="row">
             <div class="col-md-6">
                 @Html.LabelFor(Function(model) model.OC, htmlAttributes:=New With {.class = "control-label col-md-2"})
                 @Html.EditorFor(Function(model) model.OC, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
                 @Html.ValidationMessageFor(Function(model) model.OC, "", New With {.class = "text-danger"})
             </div>
             <div class="col-md-6">
                 @Html.LabelFor(Function(model) model.Cartella, htmlAttributes:=New With {.class = "control-label col-md-2"})
                 @Html.EditorFor(Function(model) model.Cartella, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.Cartella, "", New With {.class = "text-danger"})
             </div>
         </div>
         <div class="row">
             <div class="col-md-6">
                 <label class="control-label col-md-2" for="OperatoreInsert">Operatore</label>
                 @Html.EditorFor(Function(model) model.OperatoreInsert, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
                 @Html.ValidationMessageFor(Function(model) model.OperatoreInsert, "", New With {.class = "text-danger"})
             </div>
             <div class="col-md-6">
                 <label class="control-label col-md-3" for="DataCreazione">Creato il:</label>
                 @Html.EditorFor(Function(model) model.DataCreazione, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
                 @Html.ValidationMessageFor(Function(model) model.DataCreazione, "", New With {.class = "text-danger"})
             </div>
         </div>
        <div class="form-group d-none">
            <div class="col-md-10">
            @Html.LabelFor(Function(model) model.Accettato, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="checkbox">
                    @Html.EditorFor(Function(model) model.Accettato, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
                    @Html.ValidationMessageFor(Function(model) model.Accettato, "", New With {.class = "text-danger"})
                </div>
            </div>
        </div>
        <div class="form-group">
            <div>

            </div>
            <label class="control-label" for="newFileUpload">Revisione file precedente:</label>
            <div class="dropzone" id="newFileUpload"></div>
        </div>
        <div class="row d-none">
            <div class="col-md-12">
            @Html.LabelFor(Function(model) model.Note, htmlAttributes:=New With {.class = "control-label col-md-2"})
                @Html.TextAreaFor(Function(model) model.Note, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly", .style = "max-width: none!important;"}})
                @Html.ValidationMessageFor(Function(model) model.Note, "", New With {.class = "text-danger"})
            </div>
        </div>

      
    </div>
End Using

<script>
    $('.dropzone').dropzone();
</script>