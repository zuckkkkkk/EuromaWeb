@ModelType Computer
@Using Html.BeginForm("AddComputer", "Manage", Nothing, FormMethod.Post, New With {.class = "ModalForm"})
@Html.AntiForgeryToken()

@<div class="form-horizontal">
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
     <div class="form-row">
         <div class="row">
             <div class="col-md-4">
                 <label for="NomePC">Nome PC:</label>
                 @Html.EditorFor(Function(model) model.NomePC, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.NomePC, "", New With {.class = "text-danger"})
             </div>
             <div class="col-md-4">
                 <label for="IP">IP:</label>
                 @Html.EditorFor(Function(model) model.IP, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.IP, "", New With {.class = "text-danger"})
             </div>
             <div class="col-md-4">
                 <label for="MAC">MAC:</label>
                 @Html.EditorFor(Function(model) model.MAC, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.MAC, "", New With {.class = "text-danger"})
             </div>
         </div>
         <div class="row">
             <div class="col-md-6">
                 <label for="NomeOperatore">Nome Operatore:</label>
                 @Html.EditorFor(Function(model) model.NomeOperatore, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.NomeOperatore, "", New With {.class = "text-danger"})
             </div>
             <div class="col-md-6">
                 <label for="DescrizionePC">Descrizione PC:</label>
                 @Html.EditorFor(Function(model) model.DescrizionePC, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.DescrizionePC, "", New With {.class = "text-danger"})
             </div>
         </div>
     </div>
</div>
End Using