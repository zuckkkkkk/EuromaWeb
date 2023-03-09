@ModelType IEnumerable(Of EuromaWeb.AccettazioneUC)
@Code
    ViewData("Title") = "Commerciale"
    Dim DataEsportazione = DateTime.Now

End Code

<h2 style="margin-top: 1rem; margin-bottom: 1rem;">
    Sezione Accettazione
    <btn style="margin-left: 16px;" button type="button" data-type="Show_Carico" id="Show_Carico" class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal"> Carico UT <i class="fas fa-chart-bar"></i> </btn>
    @If ViewBag.tipo = 1 Then
        @<a href="@Url.Action("Index", "AccettazioneUC", New With {.id = 2})" class="btn btn-primary w-auto">Vis. tutti</a>
    Else
        @<a href="@Url.Action("Index", "AccettazioneUC", New With {.id = 1})" class="btn btn-primary w-auto">Vis. in attesa</a>
    End If
</h2>
<div Class="modal fade " id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div Class="modal-dialog modal-lg">
        <div Class="modal-content">
            <div Class="modal-header">
                <h5 Class="modal-title" id="exampleModalLabel">Modal title</h5>
                <Button type="button" Class="btn-close" data-bs-dismiss="modal" aria-label="Close"></Button>
            </div>
            <div Class="modal-body">
                ...
            </div>
            <div Class="modal-footer" style="border-top: none!important;">
                <Button type="button" Class="btn btn-primary Add ModalSubmit">Aggiungi</Button>
                <Button type="button" id="Send_Btn" Class="btn btn-primary Send ModalSubmit">Invia</Button>
                <Button type="button" Class="btn btn-danger Delete ModalSubmit">Elimina</Button>
                <Button type="button" Class="btn btn-primary Save ModalSubmit">Salva Modifiche</Button>
                <Button type="button" Class="btn btn-secondary SaveClose ModalSubmit">Salva e Chiudi</Button>
            </div>
        </div>
    </div>
</div>
@If ViewBag.tipo = 1 Then
    @<table id="mainDataTableAccettazione" Class="stripe">
        <thead>
            <tr>
                <th>

                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.Accettato)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.OC)
                </th>

                <th>
                    @Html.DisplayNameFor(Function(model) model.Cartella)
                </th>
                <th>
                    Operatore
                </th>
                <th>
                    Creato il
                </th>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </thead>

        <tbody>
        </tbody>

    </table>

Else
    @<table id="mainDataTableAccettazioneInAttesa" Class="stripe">
        <thead>
            <tr>
                <th>

                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.Accettato)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.OC)
                </th>

                <th>
                    @Html.DisplayNameFor(Function(model) model.Cartella)
                </th>
                <th>
                    Operatore
                </th>
                <th>
                    Creato il
                </th>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </thead>

        <tbody>
        </tbody>

    </table>

End If
@If User.IsInRole("Admin") Or User.IsInRole("CommercialeAdmin") Then
    @<div Class="row text-center">
        <div Class="col">
            <Button type="button" data-type="add" id="Add_Acc" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                Aggiungi accettazione
            </Button>
        </div>
    </div>
Else
    @<div Class="row text-center">
        <div Class="col">
            <Button type="button" data-type="add" id="Add_Acc" Class="btn btn-primary w-auto"disabled>
                Aggiungi accettazione
            </Button>
        </div>
    </div>
End If
