@ModelType IEnumerable(Of EuromaWeb.Disegno_Server_ViewModel)
@Code
    ViewData("Title") = "View"
End Code
@If Model.Count = 0 Then
    @<div class="text-center">
    <a href="http://192.168.100.12:120/?search=@ViewBag.CodeDisegno" class="btn btn-primary">
        <i class="fa fa-download" aria-hidden="true"></i> Download MPA
    </a>
    <a href="http://192.168.100.50:120/?search=@ViewBag.CodeDisegno" class="btn btn-primary">
        <i class="fa fa-download" aria-hidden="true"></i> Download EUROMA
    </a>
</div>
End If
<table id = "mainDataTable" Class="stripe w-100" >
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
                    @If item.Path_File.Contains("E:\Dati") Then
                        @<a href="http://192.168.100.12:120/?search=@item.Code_Disegno">
                            <i class="fa fa-download" aria-hidden="true"></i>
                        </a>
                    Else
                        @<a href="http://192.168.100.50:120/?search=@item.Code_Disegno">
                            <i class="fa fa-download" aria-hidden="true"></i>
                        </a>
                    End If

                </td>
               
            </tr>
        Next

    </tbody>

</table>
