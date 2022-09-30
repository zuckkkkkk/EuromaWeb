@ModelType EuromaWeb.Progetto
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Progetto</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.OC)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OC)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.StartDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.StartDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EndDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EndDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Cliente)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Cliente)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Brand)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Brand)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Codice)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Codice)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Note_Pezzo)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Note_Pezzo)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Id_Last_Storico_Progetto)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Id_Last_Storico_Progetto)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
