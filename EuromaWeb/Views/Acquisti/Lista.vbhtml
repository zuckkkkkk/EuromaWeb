@ModelType IEnumerable(Of EuromaWeb.LavorazioniEsterne)
@Code
    ViewData("Title") = "Index"
End Code

<h2 style="margin-top: 1rem; margin-bottom: 1rem;">Lista OL</h2>
<table id="mainDataTable" class="stripe">
    <thead>
        <tr>
            <th>
                OL
            </th>
            <th>
                Operatore
            </th>
            <th>
               Data Inserimento
            </th>
            <th>
                Azioni
            </th>
        </tr>
    </thead>

    <tbody>
        @For Each item In Model.ToList
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.OL)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Operatore)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Data_Inserimento)
                </td>
               @If item.Inviato = False Then
                   @<btn class="btn-primary btn">Invia</btn>
               End If
            </tr>
        Next
    </tbody>

</table>
