@ModelType EuromaWeb.ArticoliErrati

@Using (Html.BeginForm("EditRichiesta", "ProgettiUT", Nothing, FormMethod.Post, New With {.class = "ModalForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    @Html.HiddenFor(Function(model) model.Id)

         <div class="row">
             <div class="col-md-5">
                 @Html.LabelFor(Function(model) model.Cod_OP, htmlAttributes:=New With {.class = "control-label col-md-2"})
                 @Html.EditorFor(Function(model) model.Cod_OP, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.Cod_OP, "", New With {.class = "text-danger"})
             </div>
             <div class="col-md-5">
                 @Html.LabelFor(Function(model) model.Cod_OC, htmlAttributes:=New With {.class = "control-label col-md-2"})
                 @Html.EditorFor(Function(model) model.Cod_OC, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.Cod_OC, "", New With {.class = "text-danger"})
             </div>
             <div class="col-md-2">
                    <label for="RichiestaCompletata">Completa</label>
                 @Html.CheckBoxFor(Function(model) model.RichiestaCompletata, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.RichiestaCompletata, "", New With {.class = "text-danger"})
             </div>
         </div>
    <div class="row">
        <div class="col-md-12">
            @Html.LabelFor(Function(model) model.Note_Produzione, htmlAttributes:=New With {.class = "control-label col-md-2"})
            @Html.TextAreaFor(Function(model) model.Note_Produzione, htmlAttributes:=New With {.rows = "5", .class = "form-control", .style = "max-width: none;", .disabled = "disabled"})
            @Html.ValidationMessageFor(Function(model) model.Note_Produzione, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            @Html.LabelFor(Function(model) model.Note_UT, htmlAttributes:=New With {.class = "control-label col-md-2"})
            @Html.TextAreaFor(Function(model) model.Note_UT, htmlAttributes:=New With {.rows = "5", .class = "form-control", .style = "max-width: none;"})
            @Html.ValidationMessageFor(Function(model) model.Note_UT, "", New With {.class = "text-danger"})
        </div>
    </div>
</div>  
End Using

