@ModelType EuromaWeb.OrdiniDiProduzione

@Using (Html.BeginForm("EditEsterno", "ProgettiUT", Nothing, FormMethod.Post, New With {.class = "ModalForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    @Html.HiddenFor(Function(model) model.Id)

         <div Class="row mt-2">
             <div class="col-md-4">
                 <div class="row" style=" display: flex; align-items: center;">
                     <div class="col-md-6">
                         <label for="due_dim_Presente">2d Presente</label>
                     </div>
                     <div class="col-md-6">
                         @Html.EditorFor(Function(model) model.due_dim_Presente)
                         @Html.ValidationMessageFor(Function(model) model.due_dim_Presente, "", New With {.class = "text-danger"})

                     </div>
                 </div>
             </div>
             <div class="col-md-4">
                 <div class="row" style=" display: flex; align-items: center;">
                     <div class="col-md-6">
                         <label for="tre_dim_Presente">3d Presente</label>
                     </div>
                     <div class="col-md-6">
                         @Html.EditorFor(Function(model) model.tre_dim_Presente)
                         @Html.ValidationMessageFor(Function(model) model.tre_dim_Presente, "", New With {.class = "text-danger"})

                     </div>
                 </div>
             </div>
             <div class="col-md-4">
                 <div class="row" style=" display: flex; align-items: center;">
                     <div class="col-md-6">
                         <label for="note_presenti">Note presenti</label>
                     </div>
                     <div class="col-md-6">
                         @Html.EditorFor(Function(model) model.note_presenti)
                         @Html.ValidationMessageFor(Function(model) model.note_presenti, "", New With {.class = "text-danger"})

                     </div>
                 </div>
             </div>
         </div>
   
</div>
End Using

