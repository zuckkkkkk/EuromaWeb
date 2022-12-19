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
<!-- Button trigger modal -->
<div id="myOverlay" class="overlay">
    <span class="closebtn" onclick="closeSearch()" title="Close Overlay">×</span>
    <div class="overlay-content">
        <input id="inputRicerca" type="text" placeholder="Cerca disegno..." name="search">
        <button id="overlayBtnMPA" data-value="06 - MPA"><i class="fa fa-search"></i></button>
    </div>
</div>

@If Not ViewBag.local Then
    @<div Class="text-center" style="position: absolute; bottom:16px; left:0; width: 100vw;">
        <a Class="btn btn-info" href="https://euroma.eu.ngrok.io">Ambiente di prova <i class="fa-solid fa-code-compare"></i></a>
    </div>
End If

<div Class="row">
    <div Class="col-md-3">

    </div>
    <div Class="col-md-6">
        <h2 style="margin-top: 1rem;"> Ricerca disegni</h2>
    </div>
    <div Class="col-md-3">

    </div>
</div>
<div Class="">

    <div Class="row mt-3">
        <div Class="col-md-3">

        </div>
        <div Class="col-md-6">
            @If ViewBag.divPermessi_MPA Then
                @<div Class="input-box"> <input type="text" class="form-control" id="NomeDisegno" placeholder="Cerca disegni..."></div>
            Else
                @<div Class="input-box"> <input type="text" class="form-control" id="NomeDisegno" placeholder="Cerca disegni..." disabled></div>
            End If
        </div>
        <div Class="col-md-3">

        </div>
    </div>
    <div Class="row mt-2">
        <div Class="col-md-3">

        </div>
        <div Class="col-md-6">
            <div Class="row">
                @If ViewBag.divPermessi_MPA Then
                    @<div Class="col">
                        <Button data-value="06 - MPA" Class="btn btn-primary w-100" type="button" id="BtnDisegni"><p style="margin: 0!important" ;>Cerca disegni MPA</p></Button>
                    </div>
                    @<div class="col">
                        <Button Class="btn btn-primary w-100" type="button" id="BtnDownloadDiretto"><p style="margin: 0!important" ;>Ricerca storico MPA</p></Button>
                    </div>
                Else
                    @<div Class="col">
                        <Button data-value="06 - MPA" Class="btn btn-primary w-100" type="button" id="BtnDisegni" disabled><p style="margin: 0!important" ;>Cerca disegni MPA</p></Button>
                    </div>
                    @<div class="col">
                        <Button Class="btn btn-primary w-100" type="button" id="BtnDownloadDiretto" disabled><p style="margin: 0!important" ;>Download Diretto</p></Button>
                    </div>
                End If
                @If ViewBag.divPermessi_Drill Then
                    @<div class="col">
                        <button data-value="02 - Drill Matic" class="btn btn-primary w-100" type="button" id="BtnDisegni"><p style="margin: 0!important" ;>DRILL</p></button>
                    </div>
                End If
                @If ViewBag.divPermessi_CMT Then
                    @<div Class="col">
                        <Button data-value="03 - CMT" Class="btn btn-primary w-100" type="button" id="BtnDisegni"><p style="margin: 0!important" ;>CMT</p></Button>
                    </div>
                End If
                @If ViewBag.divPermessi_ISA Then
                    @<div Class="col">
                        <Button data-value="04 - ISA" Class="btn btn-primary w-100" type="button" id="BtnDisegni"><p style="margin: 0!important" ;>ISA</p></Button>
                    </div>
                End If
                @If ViewBag.divPermessi_UNI Then
                    @<div class="col">
                        <button data-value="05 - Unistand" class="btn btn-primary w-100" type="button" id="BtnDisegni"><p style="margin: 0!important" ;>UNI</p></button>
                    </div>
                End If
            </div>
            @If ViewBag.divPermessi_MPA Then
                @<div class="row mt-2">
                     <div class="col-md-12">
                         <Button Class="btn btn-success w-100" type="button" id="BtnDownloadDirettoEuroma"><p style="margin: 0!important" ;>Ricerca disegni Server Euroma</p></Button>
                     </div>
                </div>
            End if
        </div>
        <div class="col-md-3">

        </div>
    </div>
</div>
        @If ViewBag.divPermessi_MPA Then
                    If Not User.IsInRole("Tecnico") Then
        @<section id="counter" class="sec-padding">
            <div class="container">
                <div class="row">
                    <div class="col-md-3">

                    </div>
                    <div class="col-md-3 ">
                        <div class="count">
                            <i class="fa-solid fa-database fa-4x"></i>
                            <p class="number">@ViewBag.countDB1</p>
                            <h4> Disegni MPA DB 1</h4>
                        </div>
                    </div>
                    <div class="col-md-3 ">
                        <div class="count">
                            <i class="fa-solid fa-database fa-4x"></i>
                            <p class="number">@ViewBag.countDB2</p>
                            <h4> Disegni MPA DB 2</h4>
                        </div>
                    </div>
                </div>
            </div>
        </section>

            Else
        @<section id="counter" class="sec-padding">
            <div class="container">
                <div class="row">
                    <div class="col-md-3">

                    </div>
                    <div class="col-md-3 ">
                        <div class="count">
                            <i class="@ViewBag.RandomIcon1 fa-4x"></i>
                            <p class="number">@ViewBag.countDB1</p>
                            <h4> Disegni MPA DB 1</h4>
                        </div>
                    </div>
                    <div class="col-md-3 ">
                        <div class="count">
                            <i class="@ViewBag.RandomIcon2 fa-4x"></i>
                            <p class="number">@ViewBag.countDB2</p>
                            <h4> Disegni MPA DB 2</h4>
                        </div>
                    </div>
                </div>
            </div>
        </section>

            End If

                    End If
        <div Class="input-group" style="margin-right: 32px;">

</div>
        @*<div class="container" style="display: flex; align-content: center; justify-content:center;">
                <img src = "/Asset/img/logo-nero.png" />
    </div>*@


