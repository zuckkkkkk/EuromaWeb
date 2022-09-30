@ModelType IEnumerable(Of EuromaWeb.Licenze)
@Code
ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Nome_Licenza)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Tipologia_Licenza)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.StartDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.StartDate_Month)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.StartDate_Day)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.StartDate_Year)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Tipologia_Rinnovo)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Active)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Nome_Licenza)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Tipologia_Licenza)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.StartDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.StartDate_Month)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.StartDate_Day)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.StartDate_Year)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Tipologia_Rinnovo)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Active)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.Id }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.Id }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.Id })
        </td>
    </tr>
Next

</table>
