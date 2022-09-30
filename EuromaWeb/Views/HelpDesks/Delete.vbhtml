@ModelType EuromaWeb.HelpDesk
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>HelpDesk</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.RequestUser)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RequestUser)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.RequestDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RequestDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Title)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Title)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Body)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Body)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Solved)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Solved)
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
