@*@ModelType EuromaWeb.OrdineListaViewmodel
@Code
    ViewData("Title") = "Home Page"
End Code
<table class="table" id="mainDataTable">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.Lista.First.Anno)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Lista.First.Mese)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Lista.First.Tipo_Ordine)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Lista.First.tCharge)
            </th>
            <th></th>
        </tr>
    </thead>

    <tbody>
        @For Each item In Model.Lista
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Anno)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Mese)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Tipo_Ordine)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.tCharge)
                </td>
            </tr>
        Next

    </tbody>

</table>*@