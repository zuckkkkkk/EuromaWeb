@ModelType EuromaWeb.Overview
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Overview</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Matricola)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Matricola)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Macchina)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Macchina)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Zona)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Zona)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Data)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Data)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Totale_Ore_Uomo)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Totale_Ore_Uomo)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
