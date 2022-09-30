@ModelType IEnumerable(Of EuromaWeb.Progetto)
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
            @Html.DisplayNameFor(Function(model) model.OC)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.StartDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.EndDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Cliente)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Brand)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Codice)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Note_Pezzo)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Id_Last_Storico_Progetto)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.OC)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.StartDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.EndDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Cliente)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Brand)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Codice)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Note_Pezzo)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Id_Last_Storico_Progetto)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.Id }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.Id }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.Id })
        </td>
    </tr>
Next

</table>
