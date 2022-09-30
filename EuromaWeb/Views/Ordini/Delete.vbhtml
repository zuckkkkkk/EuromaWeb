@ModelType EuromaWeb.Ordine
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>Ordine</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Mese)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Mese)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Anno)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Anno)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.CodCliente)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.CodCliente)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.NomeCliente)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.NomeCliente)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Stato)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Stato)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Regione_uno)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Regione_uno)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Regione_due)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Regione_due)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Tipo_Ordine)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Tipo_Ordine)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Valore_Netto)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Valore_Netto)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Valore_Totale)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Valore_Totale)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Provenienza)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Provenienza)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
