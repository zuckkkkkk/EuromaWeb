<script src='@Html.Raw("https://cdn.jsdelivr.net/npm/fullcalendar@6.1.4/index.global.min.js")'></script>
<script src=@Html.Raw("https://cdn.jsdelivr.net/npm/@fullcalendar/core@6.1.4/locales/it.global.js")></script>
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

    #calendar {
        color: black !important;
        text-decoration: none !important;
    }

    .mainContainerCalendar {
        width: 100%;
        display: flex;
        justify-content: center !important;
        align-items: center !important;
        align-content: center !important;
        box-shadow: rgb(0 0 0 / 18%) 0px 2px 4px;
        padding-top: 16px;
        padding-bottom: 16px;
    }

    .containerCalendario {
        width:  600px;
    }

    a {
        color: black;
        text-decoration: none;
    }
    .NotForYou{
        height:100%;
        display:flex;
        justify-content: center;
        align-items: center;
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
<div class="row">
    <div class="col-md-12">
        <h2 style="margin-top: 1rem;"> Ciao, @User.Identity.Name</h2>
    </div>
</div>
<div Class="row">
    <div Class="col-md-6">
        <h5> Ricerca disegni</h5>
    </div>
    <div class="col-md-6">
        @If User.IsInRole("TecnicoAdmin") Or User.IsInRole("Admin") Or User.IsInRole("CommercialeAdmin") Then
            @<h5> Calendario(Progetti UT)</h5>
        End If

    </div>
</div>
<div Class="">
    <div Class="row mt-2">
        <div Class="col-md-3">

        </div>
        <div Class="col-md-6">

        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-12" id="Parent_Card_Menu" style="margin-top:0px!important;">
                    <div class="card" id="Card_Menu">
                        <p id="Nome_Menu"> Storico MPA</p>
                        <i id="Arrow" class="fa-solid fa-arrow-right-long fa-2x"></i>
                        <btn class="btn btn-primary EditMenuBtn" onclick="helpIndex(1);"><i class="fas fa-info"></i></btn>
                        <a href="http://192.168.100.12:120/" Target="_blank" class="stretched-link"></a>
                    </div>
                </div>
                <div class="col-md-12" id="Parent_Card_Menu">
                    <div class="card" id="Card_Menu">
                        <p id="Nome_Menu"> Storico EUROMA</p>
                        <i id="Arrow" class="fa-solid fa-arrow-right-long fa-2x"></i>
                        <btn class="btn btn-primary EditMenuBtn" onclick="helpIndex(2);"><i class="fas fa-info"></i></btn>
                        <a href="http://192.168.100.50:120/" Target="_blank" class="stretched-link"></a>
                    </div>
                </div>
                <div class="col-md-12" id="Parent_Card_Menu">
                    <div class="card CardDettaglio" id="Card_Menu">
                        <p id="Nome_Menu"> Dettaglio MPA</p>
                        <i id="Arrow" class="fa-solid fa-arrow-right-long fa-2x"> </i>
                        <btn class="btn btn-primary EditMenuBtn" onclick="helpIndex(3);" style="z-index:1001!important;"><i class="fas fa-info"></i></btn>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            @If User.IsInRole("TecnicoAdmin") Or User.IsInRole("Admin") Or User.IsInRole("CommercialeAdmin") Then
                @<div Class="Calendario">
                    <div Class="mainContainerCalendar">
                        <div Class="containerCalendario">
                            <div id='calendar'></div>
                        </div>
                    </div>
                </div>
            Else
                @<div class="NotForYou" style="height:100%">
                     <div class="DivCenter">
                         <div class="row">
                             <div class="col-md-12"style="justify-content: center;display: flex;">
                                 <i class="fa-solid fa-lock fa-3x"></i>
                             </div>
                         </div>
                         <div class="row">
                             <div class="col-md-12"style="display: flex;justify-content: center;">
                                 <p style="width: 50%;text-align: center;">La sezione calendario è disponibile solo agli amministratori del sistema</p>
                             </div>
                         </div>
                     </div>
                </div>
            End If
        </div>

    </div>
</div>
<script>
                                                                            document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');
             var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
aspectRatio: 2,
                locale: 'it',
headerToolbar: {
                    left: 'prev,next today',
center: 'title',
right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                weekends: false,
                 events: @Html.Raw(ViewBag.result)
                        });
                        calendar.setOption('aspectRatio', 1)
                        calendar.render();
                    });
                    function helpIndex(num) {
                        switch(num) {
                            case 1 :
                                                                                            Swal.fire(
                                'Storico MPA',
                                'Permette la ricerca di tutti i disegni presenti sul server MPA (SRVD01), perciò tutto lo storico fino a giugno 2021.',
                                'info'
                            )
                            break;
                            case 2 :
                                                                                            Swal.fire(
                                'Storico Euroma',
                                'Permette la ricerca di tutti i disegni presenti sul server EUROMA (SRV2K16), perciò tutti i disegni di EUROMA (CMT - Drill - MPA nuovo - Unistand - ISA - ecc.).',
                                'info'
                            )
                            break;
                            case 3 :
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


