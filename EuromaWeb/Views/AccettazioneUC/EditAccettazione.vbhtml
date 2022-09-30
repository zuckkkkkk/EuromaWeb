@ModelType EuromaWeb.AccettazioneUC
<btn class="btn btn-secondary" id="btn_Edit_Accettazione_Guide"> ? </btn>
@Using (Html.BeginForm("EditAccettazione", "AccettazioneUC", Nothing, FormMethod.Post, New With {.class = "ModalForm"}))

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
                 @Html.EditorFor(Function(model) model.Cartella, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
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
        <div class="row">
            <div class="col-md-12">
                    <label class="control-label col-md-11" for="Accettato">Stato accettazione</label>
                    @Html.EnumDropDownListFor(Function(model) model.Accettato, htmlAttributes:=New With {.class = "form-select form-control", .style = "min-width:100%;"})
                    @Html.ValidationMessageFor(Function(model) model.Accettato, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
            @Html.LabelFor(Function(model) model.Note, htmlAttributes:=New With {.class = "control-label col-md-2"})
                @Html.TextAreaFor(Function(model) model.Note, htmlAttributes:=New With {.rows = "5", .class = "form-control", .style = "max-width: none;"})
                @Html.ValidationMessageFor(Function(model) model.Note, "", New With {.class = "text-danger"})
            </div>
        </div>

      
    </div>
End Using

<script>
    $('#btn_Edit_Accettazione_Guide').guides({
        guides: [{
            element: $('#OC'),
            html: 'Questo è il codice della Conferma/Offerta!'
        }, {
            element: $('#Cartella'),
            html: 'Questo è il percorso della cartella legata a un dato OC.'
        }, {
            element: $('#OperatoreInsert'),
            html: "Nome dell'operatore."
        }, {
            element: $('#DataCreazione'),
            html: 'Data di creazione su questo portale di un dato OC!'
            }, {
                element: $('#Accettato'),
                html: "E' stato accettato oppure no?"
            }, {
                element: $('#Note'),
                html: 'Inserisci qui le note da far vedere agli operatori!'
            }]
    });
</script>