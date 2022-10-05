@ModelType EuromaWeb.Macchine
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Macchine</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Macchina)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Macchina)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Descrizione_Macchina)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Descrizione_Macchina)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Path_3d)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Path_3d)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
