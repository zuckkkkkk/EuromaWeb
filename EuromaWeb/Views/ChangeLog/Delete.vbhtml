@ModelType EuromaWeb.ChangeLog
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>ChangeLog</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Title)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Title)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Descrizione)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Descrizione)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Release_Date)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Release_Date)
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

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
