@ModelType EuromaWeb.MagazzinoViewModel
@Code
    ViewData("Title") = "Inventario"
End Code

<h2>Inventario per Magazzino</h2>
<div class="row">
    <div class="col-md-6">
        @Using (Html.BeginForm("InventarioPerMagazzino", "Home", Nothing, FormMethod.Post))
            @Html.AntiForgeryToken()
            @<div class="form-horizontal">
                <hr />
                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.CodMagazzino, htmlAttributes:=New With {.class = "control-label col-md-2", .style = "text-align: left!important;"})
                    <div class="col-md-12">
                        @Html.EditorFor(Function(model) model.CodMagazzino, New With {.htmlAttributes = New With {.class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.CodMagazzino, "", New With {.class = "text-danger"})
                    </div>
                </div>
            </div>
            @<div Class="form-group" style="padding:0!important;">
                <div Class="col-md-12" style="padding:0!important;">
                    <input type="submit" value="Download" Class="btn btn-default" />
                </div>
            </div>
        End Using
    </div>
    <div class="col-md-6">
        <p>Per scaricare un singolo magazzino inserire il codice di esso (es. 10 per il magazzino n°10). Oppure, per scaricare tutte gli artiocli con giacenza ma senza Campata inserire il numero 0. </p>
    </div>
</div>

