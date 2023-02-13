@ModelType List(Of EuromaWeb.ScadenzaViewModel)
<style>
    #Parent_Card_Menu {
        margin: 12px 0px;
    }

    #Card_Menu:hover {
        box-shadow: rgb(0 0 0 / 15%) 0px 15px 25px, rgb(0 0 0 / 5%) 0px 5px 10px;
        transition: .2s ease-in-out;
        cursor: pointer;
    }

    #Card_Menu {
        border: none !important;
        box-shadow: rgb(0 0 0 / 18%) 0px 2px 4px;
        min-height: 150px;
        display: flex;
        justify-content: center;
        align-items: center;
        transition: .2s ease-in-out;
    }

    .card {
        position: relative;
        display: flex;
        flex-direction: column;
        min-width: 0;
        word-wrap: break-word;
        background-color: #fff;
        background-clip: border-box;
        border: 1px solid rgba(0,0,0,.125);
        border-radius: 0.25rem;
    }

    #Nome_Menu {
        font-weight: 700;
        font-size: 18px;
    }

    #Stato_Menu {
        font-weight: 500;
        font-size: 13px;
        position: absolute;
        top: 8px;
        left: 16px;
    }

    .green {
        color: green;
        -webkit-animation-name: greenAnimation;
        -webkit-animation-iteration-count: infinite;
        -webkit-animation-duration: 2s;
    }

    #Arrow {
        position: absolute;
        right: 16px;
        bottom: 8px;
    }

    #Parent_Card_Menu a {
        text-decoration: none !important;
        color: black;
    }

    a:hover {
        color: #0a58ca;
    }

    a {
        color: #0d6efd;
        text-decoration: underline;
    }

    .EditMenuBtn {
        border-radius: 0px .25rem 0px 0px !important;
        position: absolute;
        bottom: 0;
        left: 0;
        background-color: #ff6c2b !important;
        border: none !important;
        z-index: 1000;
    }

    .btn-primary {
        color: #fff;
        background-color: #0d6efd;
        border-color: #0d6efd;
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
<!-- Button trigger modal -->
<div id="myOverlay" class="overlay" style="background-color: rgba(0, 0, 0, 0.55)!important; z-index:1005!important;">
    <span class="closebtn" onclick="closeSearch()" title="Close Overlay">×</span>
    <div class="overlay-content">
        <input id="inputRicerca" type="text" placeholder="Cerca disegno..." name="search" style="border-radius: 16px 0px 0px 16px !important;">
        <button id="overlayBtnMPA" data-value="06 - MPA"><i class="fa fa-search"></i></button>
    </div>
</div>

@If Not ViewBag.local Then
    @<div Class="text-center" style="position: absolute; bottom:16px; left:0; width: 100vw;">
        <a Class="btn btn-info" href="https://euroma.eu.ngrok.io">Ambiente di prova <i class="fa-solid fa-code-compare"></i></a>
    </div>
End If

<div Class="row">
    <div Class="col-md-12">
        <h2 style="margin-top: 1rem;"> Ciao, @User.Identity.Name</h2>
        <h5> Ricerca disegni</h5>
    </div>

</div>
<div Class="">

    @*<div Class="row mt-3">
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
        </div>*@

    <div Class="row mt-2">
        <div Class="col-md-3">

        </div>
        <div Class="col-md-6">
            @*<div Class="row">
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
                </div>*@
            @*@If ViewBag.divPermessi_MPA Then
                        @<div class="row mt-2">
                            <div class="col-md-12">
                                <Button Class="btn btn-success w-100" type="button" id="BtnDownloadDirettoEuroma"><p style="margin: 0!important" ;>Ricerca disegni Server Euroma</p></Button>
                            </div>
                        </div>
                    End if
                </div>
                <div class="col-md-3">

                </div>*@
        </div>
    </div><div class="row">
    @*<div class="col-md-12">
        <div class="row">
            <div class="col-md-9">
                <input type="text" name="Disegno" id="Disegno" class="Disegno"/>
            </div>
            <div class="col-md-3">
                <btn class="btn btn-primary" id="SearchDraw">Cerca</btn>
            </div>
        </div>
    </div>*@
    <div class="col-md-6">
        <div class="row">
            <div class="col-md-12" id="Parent_Card_Menu">
                <div class="card" id="Card_Menu">
                    <p id="Nome_Menu">Storico MPA</p>
                    <i id="Arrow" class="fa-solid fa-arrow-right-long fa-2x"></i>
                    <btn class="btn btn-primary EditMenuBtn" onclick="helpIndex(1);"><i class="fas fa-info"></i></btn>
                    <a href="http://192.168.100.12:120/" Target="_blank" class="stretched-link"></a>
                </div>
            </div>
            <div class="col-md-12" id="Parent_Card_Menu">
                <div class="card" id="Card_Menu">
                    <p id="Nome_Menu">Storico EUROMA</p>
                    <i id="Arrow" class="fa-solid fa-arrow-right-long fa-2x"></i>
                    <btn class="btn btn-primary EditMenuBtn" onclick="helpIndex(2);"><i class="fas fa-info"></i></btn>
                    <a href="http://192.168.100.50:120/" Target="_blank" class="stretched-link"></a>
                </div>
            </div>
            <div class="col-md-12" id="Parent_Card_Menu">
                <div class="card CardDettaglio" id="Card_Menu">
                    <p id="Nome_Menu">Dettaglio MPA</p>
                    <i id="Arrow" class="fa-solid fa-arrow-right-long fa-2x"> </i>
                    <btn class="btn btn-primary EditMenuBtn" onclick="helpIndex(3);" style="z-index:1001!important;"><i class="fas fa-info"></i></btn>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        @If ViewBag.divPermessi_MPA Then
            If Not User.IsInRole("Tecnico") Then
                @<section id="counter" class="sec-padding" style="padding:0!important;">
                    <div class="container" style="padding: 0 !important; margin: 12px !important;">
                        <div class="row">
                            <div class="col-md-12 ">
                                <div class="count" style="margin: 12px !important;">
                                    <i class="fa-solid fa-database fa-4x"></i>
                                    <p class="number">@ViewBag.countDB1</p>
                                    <h4> Disegni MPA DB 1</h4>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="count" style="margin: 12px !important;">
                                    <i class="fa-solid fa-database fa-4x"></i>
                                    <p class="number">@ViewBag.countDB2</p>
                                    <h4> Disegni MPA DB 2</h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            Else
                @<section id="counter" class="sec-padding" style="padding:0!important;">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="count" style="margin: 12px !important;">
                                    <i class="@ViewBag.RandomIcon1 fa-4x"></i>
                                    <p class="number">@ViewBag.countDB1</p>
                                    <h4> Disegni MPA DB 1</h4>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="count" style="margin: 12px !important;">
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
    </div>

</div>
</div>
<script>
    function helpIndex(num) {
        switch(num) {
            case 1:
            Swal.fire(
                'Storico MPA',
                'Permette la ricerca di tutti i disegni presenti sul server MPA (SRVD01), perciò tutto lo storico fino a giugno 2021.',
                'info'
            )
            break;
            case 2:
            Swal.fire(
                'Storico Euroma',
                'Permette la ricerca di tutti i disegni presenti sul server EUROMA (SRV2K16), perciò tutti i disegni di EUROMA (CMT - Drill - MPA nuovo - Unistand - ISA - ecc.).',
                'info'
            )
            break;
            case 3:
            Swal.fire(
                'Dettaglio MPA',
                'Recupero del database dettagliato di Eurocad. Permette la ricerca per cliente, operatore e descrizione articolo nello storico MPA.',
                'info'
            )
                break;
                Default:
                break;
                // code block
            }
    };
    $(".CardDettaglio").click(function (e) {
        e.stopPropagation();
        openSearch();
    });
</script>


