@ModelType EuromaWeb.Ordine
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

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
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
