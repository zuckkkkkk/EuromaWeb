@ModelType IEnumerable(Of EuromaWeb.DisegnoServerViewModel)
@Code
    ViewData("Title") = "View"
End Code
<div class="modal fade " id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                ...
            </div>
            <div class="modal-footer" style="border-top: none!important;">
                <Button type="button" class="btn btn-primary Add ModalSubmit">Aggiungi</Button>
                <Button type="button" id="Send_Btn" class="btn btn-primary Send ModalSubmit">Invia</Button>
                <Button type="button" class="btn btn-danger Delete ModalSubmit">Elimina</Button>
                <Button type="button" class="btn btn-primary Save ModalSubmit">Salva Modifiche</Button>
                <Button type="button" class="btn btn-secondary SaveClose ModalSubmit">Salva e Chiudi</Button>
            </div>
        </div>
    </div>
</div>
<div id="myOverlay" class="overlay">
    <span class="closebtn" onclick="closeSearch()" title="Close Overlay">×</span>
    <div class="overlay-content">
            <input id="inputRicerca"type="text" placeholder="Cerca disegno..." name="search">
            <button id="overlayBtnMPA"data-value="06 - MPA" ><i class="fa fa-search"></i></button>
    </div>
</div>


<h2 style="margin-top: 1rem; margin-bottom: 1rem;">Risultato Ricerca "<span class="nomeArticolo">@ViewBag.Art.ToString</span>" <button class="openBtn btn btn-primary" onclick="openSearch()"><i class="fas fa-search"></i></button></h2>
<div id="loader" style="overflow:hidden;position:absolute; left: 0; top: 132px; width: 100vw; height:92vh; background-color: white;z-index: 1001; display: flex; align-items: center; justify-content:center;">
    <img src="~/Asset/loading.gif" style="max-height:100px;"/>
</div>
<table id="mainDataTableDisegni" class="stripe">
    <thead>
        <tr>
            <th></th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Name)
            </th>
            <th>
                Desc_Alnus
            </th>
            <th>
                Path_file
            </th>
            
        </tr>

    </thead>
    <tbody>
        @For Each item In Model
            @<tr>
                 <td></td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Name)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Path)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.ext)
                </td>
               
            </tr>
        Next

    </tbody>

</table>
