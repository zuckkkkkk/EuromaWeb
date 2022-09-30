@ModelType EuromaWeb.Licenze
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
