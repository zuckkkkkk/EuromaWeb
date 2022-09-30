@ModelType EuromaWeb.Licenze
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Licenze</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Nome_Licenza)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Nome_Licenza)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Tipologia_Licenza)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Tipologia_Licenza)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.StartDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.StartDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.StartDate_Month)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.StartDate_Month)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.StartDate_Day)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.StartDate_Day)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.StartDate_Year)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.StartDate_Year)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Tipologia_Rinnovo)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Tipologia_Rinnovo)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Active)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Active)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
