@ModelType IEnumerable(Of EuromaWeb.Disegno_Server_ViewModel)
@Code
    ViewData("Title") = "View"
End Code

<table id="mainDataTable" class="stripe w-100" >
    <thead>
        <tr>
            <th></th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Code_Disegno)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Path_File)

            </th>
            <th>
                Download
            </th>
            
        </tr>

    </thead>
    <tbody>
        @For Each item In Model
            @<tr>
                 <td></td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Code_Disegno)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Path_File)
                </td>
                <td>
                    <a href="@Url.Action("DownloadMPA", "Home", New With {.id = item.Id})">
                        <i class="fa fa-download" aria-hidden="true"></i>
                    </a>
                </td>
               
            </tr>
        Next

    </tbody>

</table>
