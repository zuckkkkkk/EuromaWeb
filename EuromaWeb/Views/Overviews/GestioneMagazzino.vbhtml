@ModelType EuromaWeb.GestioneMagazzinoViewModel
@Code
    ViewData("Title") = "Gestione Magazzino"
End Code
<style>
    .card {
        border-radius: 16px;
        /*box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;*/
        display: flex;
        align-items: center;
        justify-content: center;
        height: 100px !important;
        background-color: #0d6efd;
        width: 100% !important;
    }

        .card:hover {
            cursor: pointer;
        }

    .scaffale {
        box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
        border-radius: 8px;
        padding: 16px;
        margin-left: 4px;
        margin-right: 4px;
    }

    p {
        margin: 0 !important;
    }
</style>
<!-- Modal -->
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
<h2 style="margin-top: 1rem;">Gestione magazzino @Model.CodMag</h2>
<h4 style=" margin-bottom: 1rem;"> @Model.DescMag</h4>

<div class="magazzino">
    <div class="row">
        @For i = 0 To Model.ListaScaffali.Count - 1
            @<div Class="mr-2 col-md-4">
                <div class="row">
                    <div class="col-md-12" style="margin-top: 8px !important;">
                        <p>Scaffale @Model.ListaScaffali(i).NumScaffale</p>
                        <div class="row scaffale">
                            @For Each slot In Model.ListaScaffali(i).ListaSlot
                                @<div class="col-md-4 my-2">
                                    <button type="button" data-type="visualizza_scaffale" data-value="@slot.idSlot" Class="btn btn-primary w-auto card" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                        Slot @slot.CodSlot <br /> @IIf(slot.Count > 0, "(" + slot.Count.ToString + " articoli)", "")
                                    </button>
                                </div>
                            Next
                        </div>
                    </div>
                </div>
            </div>
            i = i + 1
            If Model.ListaScaffali.Count - 1 >= i Then
                @<div Class="mr-2 col-md-4">
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 8px !important;">
                            <p>Scaffale @Model.ListaScaffali(i).NumScaffale</p>
                            <div class="row scaffale">
                                @For Each slot In Model.ListaScaffali(i).ListaSlot
                                    @<div class="col-md-4 my-2">
                                        <button type="button" data-type="visualizza_scaffale" data-value="@slot.idSlot" Class="btn btn-primary w-auto card" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                            Slot @slot.CodSlot <br /> @IIf(slot.Count > 0, "(" + slot.Count.ToString + "articoli)", "")
                                        </button>
                                    </div>
                                Next
                            </div>
                        </div>
                    </div>
                </div>
                i = i + 1
                @<div Class="mr-2 col-md-4">
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 8px !important;">
                            <p>Scaffale @Model.ListaScaffali(i).NumScaffale</p>
                            <div class="row scaffale">
                                @For Each slot In Model.ListaScaffali(i).ListaSlot
                                    @<div class="col-md-4 my-2">
                                        <button type="button" data-type="visualizza_scaffale" data-value="@slot.idSlot" Class="btn btn-primary w-auto card" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                            Slot @slot.CodSlot <br /> @IIf(slot.Count > 0, "(" + slot.Count.ToString + "articoli)", "")
                                        </button>
                                    </div>
                                Next
                            </div>
                        </div>
                    </div>
                </div>
            End If
        Next
    </div>
    <div class="row text-center mt-3">
        <div class="col">
            @If User.IsInRole("Magazzino") Then
                @<button type = "button" data-type="add_articolo_magazzion" data-value="@Model.IdMag"id="Add_Art" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                    Aggiungi Articolo
                </button>
            End If
            <button type = "button"  onclick="SearchArticolo()" Class="btn btn-success w-auto">
                Ricerca Articolo
            </button>
        </div>
    </div>
</div>
