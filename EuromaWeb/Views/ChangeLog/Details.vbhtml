@ModelType EuromaWeb.ChangeLog
@Code
    ViewData("Title") = "Details"
End Code

<h2 class="mt-3">Dettagli "@Model.Title"</h2>

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
</div>
<p>
    @If User.IsInRole("Admin") Then
        @Html.ActionLink("Edit", "Edit", New With {.id = Model.Id}) 
    End If
    @Html.ActionLink("Torna alla lista", "Index")
</p>
