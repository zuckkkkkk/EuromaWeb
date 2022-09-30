@ModelType EuromaWeb.Tempi_Macchina_Viewmodel
@Code
    ViewData("Title") = "Commesse"
End Code


<div class="container">
    <h2 style="margin-top: 1rem; margin-bottom: 1rem;">Aggiungi tempo</h2>

    <div class="loader"></div>
  <div style="display: flex; align-content: center; justify-content: center; ">
      <div class="row card" style="width: 50%;">
          <div class="form-group  mt-1 mb-1">
              @Html.LabelFor(Function(model) model.Macchina, htmlAttributes:=New With {.class = "control-label col-md-2"})
              @Html.EnumDropDownListFor(Function(model) model.Macchina, htmlAttributes:=New With {.class = "form-control", .style = "max-width:none!important;"})
          </div>
          <div class="form-group  mt-1 mb-1">
              <label class="control-label col-md-2" for="RequestUser">Operatore</label>
              @Html.EditorFor(Function(model) model.RequestUser, New With {.htmlAttributes = New With {.class = "form-control"}})
          </div>
          <div class="form-group  mt-1 mb-1">
              <label class="control-label col-md-2" for="T_Tot">Tempo</label>
              @Html.EditorFor(Function(model) model.T_Tot, New With {.htmlAttributes = New With {.class = "form-control"}})
          </div>
          <div class="text-left mt-1 mb-3">
            <button class="btn btn-primary">Aggiungi tempo</button>
          </div>

      </div>

  </div>
</div>
@*</div>*@
