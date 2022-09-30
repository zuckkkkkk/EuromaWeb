@ModelType EuromaWeb.Overview
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
