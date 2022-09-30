@ModelType IEnumerable(Of EuromaWeb.ChangeLog)
@Code
ViewData("Title") = "Index"
End Code

<h2 class="mt-3">Lista Changelog</h2>
@If User.IsInRole("Admin") Then
@<p>
    @Html.ActionLink("Create New", "Create")
</p>
End If

<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Title)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Descrizione)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Release_Date)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.StartDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.EndDate)
        </th>
        @If User.IsInRole("Admin") Then
            @<th></th>
        End If
        
    </tr>

    @For Each item In Model
    @<tr>
    <td>
        @Html.DisplayFor(Function(modelItem) item.Title)
    </td>
    <td>
        @Html.DisplayFor(Function(modelItem) item.Descrizione)
    </td>
    <td>
        @Html.DisplayFor(Function(modelItem) item.Release_Date)
    </td>
    <td>
        @Html.DisplayFor(Function(modelItem) item.StartDate)
    </td>
    <td>
        @Html.DisplayFor(Function(modelItem) item.EndDate)
    </td>
    @If User.IsInRole("Admin") Then
        @<td>
            @Html.ActionLink("Modifica", "Edit", New With {.id = item.Id}) |
            @Html.ActionLink("Dettagli", "Details", New With {.id = item.Id}) |
            @Html.ActionLink("Elimina", "Delete", New With {.id = item.Id})
         </td>
    End If

</tr>
    Next

</table>
