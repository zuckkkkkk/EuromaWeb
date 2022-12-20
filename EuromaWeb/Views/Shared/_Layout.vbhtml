<!DOCTYPE html lang="it">
<html style="overflow-x: hidden; max-width: 100%;">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Euromaweb</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" integrity="sha512-5A8nwdMOWrSz20fDsjczgUidUBR8liPYU+WymTZP1lmY9G6Oc7HlZv156XqnsgNUzTyMefFTcsFH/tnJE/+xBg==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jstree/3.2.1/themes/default/style.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jstree/3.2.1/jstree.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.11.3/af-2.3.7/b-2.0.1/cr-1.5.5/date-1.1.1/fc-4.0.1/fh-3.2.0/sb-1.3.0/datatables.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/darkmode-js@1.5.7/lib/darkmode-js.min.js"></script>
    <link rel="stylesheet" type="text/css" href="~/Content/guides.css" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/2.11.4/umd/popper.min.js" integrity="sha512-+Tn2V/oN9O/kiaJg/1o5bETqyS35pMDJzkhkf8uvCzxmRox69AsWkSpBSMEGEe4EZp07Nunw712J3Xvh5Tti0w==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://unpkg.com/bootstrap-darkmode@0.9.1/css/darktheme.css" />
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="~/Scripts/css/DatePicker_A.css">
    <link rel="stylesheet" href="~/Content/jtimeline.css">
    <script src="https://kit.fontawesome.com/880250b3f4.js" crossorigin="anonymous"></script>
    <script type="module" src="~/Scripts/Gradient.js" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="https://cdn.jsdelivr.net/npm/particles.js@2.0.0/particles.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.4/moment.min.js"></script>
    <script src="https://cdn.jsdelivr.net/gh/fcmam5/nightly.js@v1.0/dist/nightly.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/selectize.js/0.15.2/js/selectize.min.js" integrity="sha512-IOebNkvA/HZjMM7MxL0NYeLYEalloZ8ckak+NDtOViP7oiYzG5vn6WVXyrJDiJPhl4yRdmNAG49iuLmhkUdVsQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    @*<script src="~/Scripts/jquery.fn.gantt.js"></script>*@
    <script src="https://cdnjs.cloudflare.com/ajax/libs/frappe-gantt/0.6.1/frappe-gantt.min.js" integrity="sha512-HyGTvFEibBWxuZkDsE2wmy0VQ0JRirYgGieHp0pUmmwyrcFkAbn55kZrSXzCgKga04SIti5jZQVjbTSzFpzMlg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/frappe-gantt/0.6.1/frappe-gantt.css" integrity="sha512-57KPd8WI3U+HC1LxsxWPL2NKbW82g0BH+0PuktNNSgY1E50mnIc0F0cmWxdnvrWx09l8+PU2Kj+Vz33I+0WApw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
</head>
<style>
    ::-webkit-scrollbar {
        display: none;
    }
    .NotificaLink, .NotificaChild {
        text-decoration: none !important;
        transition: .2s linear;
        color: black;
    }
    .NotificaLink:hover, .NotificaChild:hover {
        background-color: lightgray;
        transition: .2s linear;
    }
    .darkmode--activated * {
        color: white !important;
    }
    .darkmode-layer, .darkmode-toggle {
        z-index: 500;
    }
    .darkmode-layer {
        background-color: #e3e3e3!important;
    }
    .darkmode--activated input[type=checkbox]:checked:before,.darkmode--activated .nav-link, .darkmode--activated .nav-link, .darkmode--activated .btn, .darkmode--activated .badge, .darkmode--activated .container-fluid, .darkmode--activated .progress-bar, .darkmode--activated a {
        mix-blend-mode: difference
    }
    .darkmode--activated .form-control:focus {
        mix-blend-mode: difference;
        background-color: #000;
    }
    .darkmode--activated .container-fluid {
        background-color: #ffffff !important;
    }
    .darkmode--activated #RicercaOC {
        border: 1px solid black!important;
    }
    .darkmode--activated .navbar{
        border-radius:0!important;
    }
    .darkmode--activated mx-1 {
        color: white !important;
        mix-blend-mode: difference !important;
    }
    .modal-content {
        border-radius: 14px;
        border: none !Important;
    }

    .modal-header {
        border: none;
    }

    .btn {
        border-radius: 0px 8px !important;
        box-shadow: rgb(0 0 0 / 10%) 0px 4px 12px 1px;
    }

    input, textarea,select {
        border-radius: 0px 8px !important;
    }

    .progress-bar {
        padding: 4px;
        border-radius: 4px;
    }

    .navbar {
        border-radius: 0px 0px 8px 8px;
    }

    #RicercaOC {
        border: none !important;
    }
    #counter {
        background-color: #fff;
        color: #fff;
        display: block;
        overflow: hidden;
        text-align: center;
        padding: 100px 0;
    }
        #counter .count {
            padding: 50px;
            background: #f9f9f9;
            color: #000;
            text-align: center;
        }

    .count h4 {
        color: #000;
        font-size: 16px;
        margin-top: 0;
    }

    #counter .count .fa {
        font-size: 40px;
        display: block;
        color: #000;
    }

    #counter .number {
        font-size: 30px;
        font-weight: 700;
        margin: 0;
    }
    .openBtn {
        border: none;
        padding: 10px 15px;
        font-size: 20px;
        cursor: pointer;
    }
    .dataTables_length{
        display: block!important;
    }
    .openBtn:hover {
        background: #bbb;
    }
    #agenti-selectized:focus-visible, #clienti-selectized:focus-visible {
        outline: none !important;
    }
    .selectize-dropdown{
        border: none!important; 
    }
    #agenti-selectized, #clienti-selectized {
        border: none !important;
    }
    .selectize-dropdown{
        height:250px;
        overflow: scroll!important;
    }
    .overlay {
        height: 100%;
        width: 100%;
        display: none;
        position: fixed;
        z-index: 1;
        top: 0;
        left: 0;
    }
    #swal2-input {
        max-width: 90%!important;
    }
    .overlay-content {
        position: relative;
        top: 46%;
        width: 80%;
        text-align: center;
        margin-top: 30px;
        margin: auto;
    }

    .overlay .closebtn {
        position: absolute;
        top: 20px;
        right: 45px;
        font-size: 65px;
        cursor: pointer;
        color: white;
        background-color: #0d6efd;
        border-radius: 75px;
        height: 64px;
        width: 64px;
        display: flex;
        justify-content: center;
        align-items: center;
        box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;
    }

        .overlay .closebtn:hover {
            color: #ccc;
        }

    .overlay input[type=text] {
        padding: 15px;
        font-size: 17px;
        border: none;
        float: left;
        width: 80%;
        background: white;
        height: 64px;
        border-radius: 16px 0px 0px 16px;
        box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;
    }
        .overlay input[type=text]:focus-visible{
            outline: none!important;

        }
        .overlay input[type=text]:hover {
            background: #f1f1f1;
        }

    .overlay button {
        float: left;
        width: 20%;
        padding: 15px;
        background: #0d6efd;
        font-size: 17px;
        border: none;
        cursor: pointer;
        height: 64px;
        border-radius: 0px 16px 16px 0px;
        color: white;
        box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;
    }

        .overlay button:hover {
            background: #bbb;
        }
    .BordersChild {
        border: 1px solid #f1f1f1;
        border-radius: 0px 0px 15px 15px;
    }
    #Oggetto {
        padding-top: 10px !important;
        padding-bottom: 10px !important;
    }

    .dropzone {
        border: 1px solid #ced4da !important;
        border-radius: .25rem !important;
    }

    .autocomplete-items {
        position: absolute;
        border: 1px solid #d4d4d4;
        border-bottom: none;
        border-top: none;
        z-index: 99;
        /*position the autocomplete items to be the same width as the container:*/
        top: 100%;
        left: 0;
        right: 0;
    }

        .autocomplete-items div {
            padding: 10px;
            cursor: pointer;
            background-color: #fff;
            border-bottom: 1px solid #d4d4d4;
        }

            .autocomplete-items div:hover {
                /*when hovering an item:*/
                background-color: #e9e9e9;
            }
    .swal2-overflow {
        overflow-x: visible;
        overflow-y: visible;
    }
    .swal2-html-container{
        overflow: visible!important;
        z-index:100!important;
    }
    .autocomplete {
        /*the container must be positioned relative:*/
        position: relative;
        display: inline-block;
        width: 100%;
    }
    #Change_Date {
        border-radius: 100px !important;
        height: 37px !important;
        width: 37px !important;
        position: fixed;
        right: 32px;
        bottom: 60px;
        overflow: hidden;
        padding: 6px !important;
    }
    #Hide_Navbar {
        border-radius: 100px !important;
        height: 37px !important;
        width: 37px !important;
        position: fixed;
        right: 32px;
        bottom: 16px;
        overflow: hidden;
        padding: 6px !important;
    }
    .autocomplete-active {
        /*when navigating through the items using the arrow keys:*/
        background-color: DodgerBlue !important;
        color: #ffffff;
    }

    #Infos:hover {
        transition: 0.9s;
        transform: rotateZ(270deg);
    }

    .dt-buttons {
        background-color: white !important;
        box-shadow: none !important;
    }

    body {
        padding: 0 !important;
    }

    input {
        max-width: 100% !important;
    }

    .alert .close {
        display: none;
        /*border: none!important;*/
        /*   background-color: transparent;*/
    }

    .tagify__input {
        min-height: 32px !important;
    }

    .loader {
        display: none;
        position: fixed;
        left: 0px;
        top: 0px;
        width: 100%;
        height: 100%;
        z-index: 9999;
        background: url('/Content/svg/Magnify.gif') 50% 50% no-repeat;
    }
</style>
<body class="d-flex flex-column min-vh-100" style="overflow-x:hidden;">
    <script src="https://unpkg.com/bootstrap-darkmode@0.9.1/bundles/bootstrap-darkmode.umd.js"></script>
    <script>
        //const bootstrapDarkmode = window['bootstrap-darkmode'];
        //const ThemeConfig = bootstrapDarkmode.ThemeConfig;
        //const writeDarkSwitch = bootstrapDarkmode.writeDarkSwitch;
        //const themeConfig = new ThemeConfig();
        //// place customizations here
        //themeConfig.initTheme();
        //const darkSwitch = writeDarkSwitch(themeConfig);
    </script>
    @If User.Identity.IsAuthenticated And Not User.IsInRole("MagazzinoTablet") Then
        @<nav class="navbar navbar-expand-lg navbar-dark bg-dark" style="padding: 0!important;">
            <div class="container-fluid">
                <a class="navbar-brand" href="@Url.Action("Index", "Home")"><img src="~/Asset/img/euromagroup-logo-negativo.svg" height="50" /></a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <!-- Ufficio Commerciale -->
                        @If User.IsInRole("Admin") Or User.IsInRole("Commerciale_Admin") Or User.IsInRole("Commerciale_Utente") Or User.IsInRole("Commerciale") Then
                            @<li Class="nav-item">
                                <a Class="nav-link" href="@Url.Action("Index", "AccettazioneUC", New With {.id = 1})"> Commerciale</a>
                            </li>
                        End If
                        <!-- Ufficio Tecnico -->
                        @If User.IsInRole("TecnicoVisualizzazione") Or User.IsInRole("Admin") Or User.IsInRole("TecnicoAdmin") Or User.IsInRole("Tecnico") Or User.IsInRole("ProgrammazioneEsterno") Or User.IsInRole("ProgrammazioneInterno") Then
                            @<li Class="nav-item">
                                <a Class="nav-link" href="@Url.Action("Index", "ProgettiUT", New With {.id = 1})"> Tecnico</a>
                            </li>
                        End If
                        @If User.IsInRole("Admin") Or User.IsInRole("ProduzioneController") Or User.IsInRole("Produzione") Then
                            @<li Class="nav-item">
                                <a Class="nav-link" href="@Url.Action("Index", "ProgettiProd", New With {.id = 1})"> Produzione</a>
                            </li>
                        End If
                        @If User.IsInRole("Admin") Or User.IsInRole("Magazzino") Then
                            @<li Class="nav-item">
                                <a Class="nav-link" href="@Url.Action("Schedulatore", "ProgettiProd")"> Gestione</a>
                            </li>
                        End If
                        @If User.IsInRole("Admin") Then
                            @<li Class="nav-item">
                                <a Class="nav-link" href="@Url.Action("Index", "Macchine")"> Macchine</a>
                            </li>
                        End If
                        <!-- Analisi Costi -->
                        @If User.IsInRole("Admin") Or User.IsInRole("Commerciale_Admin") Or User.IsInRole("ProduzioneController") Or User.IsInRole("Produzione") Or User.IsInRole("Tecnico") Or User.IsInRole("TecnicoAdmin") Then
                            @<li Class="nav-item dropdown">
                                <a href="@Url.Action("Index", "Pezzi")" Class="nav-link dropdown-toggle" data-bs-toggle="dropdown">Utilities</a>

                                <ul Class="dropdown-menu">
                                    <li>
                                        @If User.IsInRole("Produzione") Or User.IsInRole("ProduzioneController") Then
                                            @<button Class="btn  w-auto" onclick="SearchLotto()" style="box-shadow:none!important;">
                                                Ricerca Lotti
                                            </button>
                                            @<a href="@Url.Action("Index", "Pezzi")" Class="dropdown-item">Articoli</a>
                                            @<a href="@Url.Action("Index", "ChangeLog")" Class="dropdown-item">Changelog</a>
                                            @<a href="@Url.Action("LavorazioniEsterne", "Acquisti", New With {.id = 1})" Class="dropdown-item">C/O</a>
                                            @<a href="@Url.Action("Opera", "Overviews")" Class="dropdown-item">Analisi Opera</a>
                                            @<a href="@Url.Action("GestioneMagazzino", "Overviews", New With {.id = 1})" Class="dropdown-item">Magazzino 60</a>
                                            @<a href="@Url.Action("GestioneMagazzino", "Overviews", New With {.id = 2})" Class="dropdown-item">Magazzino 70</a>

                                        ElseIf User.IsInRole("Admin") Or User.IsInRole("TecnicoAdmin") Then
                                            @<a href="@Url.Action("Index", "Pezzi")" Class="dropdown-item">Costi</a>
                                            @<a href="@Url.Action("GestioneMagazzino", "Overviews", New With {.id = 1})" Class="dropdown-item">Magazzino 60</a>
                                            @<a href="@Url.Action("GestioneMagazzino", "Overviews", New With {.id = 2})" Class="dropdown-item">Magazzino 70</a>
                                        ElseIf User.IsInRole("GestioneLicenze") Or User.IsInRole("Admin") Then
                                            @<a href="@Url.Action("GestioneUtenti", "Manage")" Class="dropdown-item">Utenti</a>
                                        End If
                                    </li>
                                    <li>
                                        @If User.IsInRole("Admin") Then
                                            @<a href="@Url.Action("LavorazioniEsterne", "Acquisti", New With {.id = 1})" Class="dropdown-item">C/O</a>
                                            @<a href="@Url.Action("Index", "ChangeLog")" Class="dropdown-item">Changelog</a>
                                            @<button data-type="downloadFatturato" Class="btn  w-auto " data-bs-toggle="modal" data-bs-target="#exampleModal" style="box-shadow:none!important;">
                                                Fatturato
                                            </button>
                                            @<button data-type="downloadOrdinato" Class="btn  w-auto " data-bs-toggle="modal" data-bs-target="#exampleModal" style="box-shadow:none!important;">
                                                Ordinato
                                            </button>
                                            @<button data-type="downloadCompOrdinato" Class="btn  w-auto " data-bs-toggle="modal" data-bs-target="#exampleModal" style="box-shadow:none!important;">
                                                Prev. Ordinato
                                            </button>
                                        End If
                                    </li>
                                    <li>
                                        @If User.IsInRole("Tecnico") Then
                                            @<button data-type="analisiGleason" Class="btn  w-auto " data-bs-toggle="modal" data-bs-target="#exampleModal" style="box-shadow:none!important;">
                                                Gleason
                                            </button>
                                        End If
                                    </li>
                                    <li>
                                        @If User.IsInRole("Commerciale_Admin") Then
                                            @<button data-type="downloadOrdinato" Class="btn  w-auto " data-bs-toggle="modal" data-bs-target="#exampleModal" style="box-shadow:none!important;">
                                                Ordinato
                                            </button>
                                            @<button data-type="downloadPriorita" Class="btn  w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal" style="box-shadow:none!important;">
                                                OC Priorita
                                            </button>
                                            @<button data-type="downloadCompOrdinato" Class="btn  w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal" style="box-shadow:none!important;">
                                                Prev. Ordinato
                                            </button>
                                            @<a href="@Url.Action("Index", "ChangeLog")" Class="dropdown-item">Changelog</a>
                                        End If
                                        @If User.IsInRole("Tecnico") Or User.IsInRole("TecnicoAdmin") Then
                                            @<a href="@Url.Action("Index", "ChangeLog")" Class="dropdown-item">Changelog</a>
                                        End If
                                    </li>

                                </ul>
                            </li>
                        End If

                        <li Class="nav-item">
                            <a Class="nav-link" href="@Url.Action("Index", "HelpDesks")">Ticket</a>
                        </li>
                        <li Class="nav-item">

                        </li>
                    </ul>
                    @If Not User.IsInRole("ProgrammazioneEsterno") Then
                        @<div Class="d-flex" style="margin: 0!important;">
                            <div id="btn_notifiche" style="position: relative;">
                                <btn Class="nav-link" style="display: flex; height: 100%; align-items: center;" href="@Url.Action("Index", "HelpDesks")">
                                    <button type="button" class="btn position-relative" style="color: white;">
                                        <i class="fa-solid fa-bell">
                                        </i>
                                        <span id="conteggioNotifiche" class="position-absolute top-0 start-100 translate-middle badge rounded-pill">
                                        </span>
                                    </button>
                                </btn>

                            </div>

                            <input id="RicercaOC" Class="form-control me-2" type="search" placeholder="2022-OC-1234" aria-label="Search">
                            <Button id="btnRicercaOC" Class="btn btn-outline-success" type="submit">Cerca</Button>
                        </div>
                    End If

                    <Span Class="navbar-text">

                        @Html.Partial("_LoginPartial")
                    </Span>
                </div>

            </div>
        </nav>

    End If
    <div class="container body-content ">
        @RenderBody()
        <p style="display:none!important;z-index: 10000;text-align: center; position: fixed; left: 16px; width: 15vw; bottom: 0; color: white; padding: 8px; background-color: black; margin: 0 !important; border-radius: 16px 16px 0px 0px; box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px; ">
            DEBUG 0.9.0a
        </p>
    </div>

    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.11.3/af-2.3.7/b-2.0.1/cr-1.5.5/date-1.1.1/fc-4.0.1/fh-3.2.0/sb-1.3.0/datatables.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://editor.datatables.net/extensions/Editor/js/dataTables.editor.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.4.0/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.4.0/js/buttons.flash.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.4.0/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.4.0/js/buttons.print.min.js"></script>
    <script src="~/Scripts/HotKeys.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/dropzone/5.8.0/dropzone.min.js" integrity="sha512-jhptvhQFnQKI2G5D/BKyGZ/Dx57CMM/l11+tJ15PgTMcYi/594SE+MQClNRZQtGFfJjaMrd2/F4w6V3TxeFz8A==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/dropzone/5.8.0/min/dropzone.min.css" integrity="sha512-WvVX1YO12zmsvTpUQV8s7ZU98DnkaAokcciMZJfnNWyNzm7//QRV61t4aEr0WdIa4pe854QHLTV302vH92FSMw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <!-- include summernote css/js -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/2.6.0/umd/popper.min.js" integrity="sha512-BmM0/BQlqh02wuK5Gz9yrbe7VyIVwOzD1o40yi1IsTjriX/NGF37NyXHfmFzIlMmoSIBXgqDiG1VNU6kB5dBbA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.20/summernote-bs5.min.css" integrity="sha512-ngQ4IGzHQ3s/Hh8kMyG4FC74wzitukRMIcTOoKT3EyzFZCILOPF0twiXOQn75eDINUfKBYmzYn2AA8DkAk8veQ==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.20/summernote-bs5.min.js" integrity="sha512-6F1RVfnxCprKJmfulcxxym1Dar5FsT/V2jiEUvABiaEiFWoQ8yHvqRM/Slf0qJKiwin6IDQucjXuolCfCKnaJQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>    @Scripts.Render("~/bundles/notify")
    <script src="~/Scripts/guides.min.js"></script>
    <script src="~/Scripts/Tagify/tagify.min.js"></script>
    <script src="~/Scripts/Tagify/tagify.polyfills.min.js"></script>
    <link href="~/Scripts/Tagify/tagify.css" rel="stylesheet" type="text/css" />
    <script src="~/Scripts/datepicker.js"></script>
    <script src="~/Scripts/jtimeline.js"></script>
    <script src="~/Scripts/filterDropDown.js"></script>
    <script src="~/Scripts/Timepicker/timepicker-ui.umd.js"></script>
    <script src="~/Scripts/Cleave.js"></script>

    @RenderSection("scripts", required:=False)
    <script defer>
    $('body').on('click', function (e) {
        if ($(".notifiche").length > 0) {
            $(".notifiche").remove();
        }
    });
    updateNots();
        setInterval(updateNots, 5000);
        function updateNots() {
            $.ajax({
                method: "GET",
                url: "/ChangeLog/Aggiornamenti",
                success: function (data) {
                    if (data.totalnot > 0) {
                        $("#conteggioNotifiche").text(data.totalnot);
                        $("#conteggioNotifiche").addClass("bg-danger");
                    } else {
                        $("#conteggioNotifiche").text("");
                        $("#conteggioNotifiche").removeClass("bg-danger");
                    }
                    if (data.esiste && !Swal.isVisible() ) {
                        Swal.fire({
                            icon: 'warning',
                            title: data.data.Titolo,
                            text: data.data.Descrizione,
                            confirmButtonText: 'Capito!',
                            footer: '<a href="/ChangeLog/Details/'+data.data.id+'">Vai alla pagina del changelog</a>'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $.ajax({
                                    method: "GET",
                                    url: "/ChangeLog/AggiornaUtente/"+ data.data.id,
                                    success: function (data) {
                                        if (data.ok) {

                                        };
                                    },
                                    error: function (error_data) {
                                        console.log("Endpoint request error");
                                    }
                                });
                            }
                        })
                    }
                },
                error: function (error_data) {
                    console.log("Endpoint GET request error");
                    window.location.href = "http://192.168.100.50:83/";
                }
            });
        }

    function ToggleNavbar() {
        $(".navbar").toggle();
        $("#ToggleEye").toggleClass("fa-eye fa-eye-slash")
    }

    function autocomplete(inp, arr) {
        /*the autocomplete function takes two arguments,
        the text field element and an array of possible autocompleted values:*/
        var currentFocus;
        /*execute a function when someone writes in the text field:*/
        inp.addEventListener("input", function (e) {
            var a, b, i, val = this.value;
            /*close any already open lists of autocompleted values*/
            closeAllLists();
            if (!val) { return false; }
            currentFocus = -1;
            /*create a DIV element that will contain the items (values):*/
            a = document.createElement("DIV");
            a.setAttribute("id", this.id + "autocomplete-list");
            a.setAttribute("class", "autocomplete-items");
            /*append the DIV element as a child of the autocomplete container:*/
            this.parentNode.appendChild(a);
            /*for each item in the array...*/
            for (i = 0; i < arr.length; i++) {
                /*check if the item starts with the same letters as the text field value:*/
                if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                    /*create a DIV element for each matching element:*/
                    b = document.createElement("DIV");
                    /*make the matching letters bold:*/
                    b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                    b.innerHTML += arr[i].substr(val.length);
                    /*insert a input field that will hold the current array item's value:*/
                    b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                    /*execute a function when someone clicks on the item value (DIV element):*/
                    b.addEventListener("click", function (e) {
                        /*insert the value for the autocomplete text field:*/
                        inp.value = this.getElementsByTagName("input")[0].value;
                        /*close the list of autocompleted values,
                        (or any other open lists of autocompleted values:*/
                        closeAllLists();
                    });
                    a.appendChild(b);
                }
            }
        });
        /*execute a function presses a key on the keyboard:*/
        inp.addEventListener("keydown", function (e) {
            var x = document.getElementById(this.id + "autocomplete-list");
            if (x) x = x.getElementsByTagName("div");
            if (e.keyCode == 40) {
                /*If the arrow DOWN key is pressed,
                increase the currentFocus variable:*/
                currentFocus++;
                /*and and make the current item more visible:*/
                addActive(x);
            } else if (e.keyCode == 38) { //up
                /*If the arrow UP key is pressed,
                decrease the currentFocus variable:*/
                currentFocus--;
                /*and and make the current item more visible:*/
                addActive(x);
            } else if (e.keyCode == 13) {
                /*If the ENTER key is pressed, prevent the form from being submitted,*/
                e.preventDefault();
                if (currentFocus > -1) {
                    /*and simulate a click on the "active" item:*/
                    if (x) x[currentFocus].click();
                }
            }
        });
        function addActive(x) {
            /*a function to classify an item as "active":*/
            if (!x) return false;
            /*start by removing the "active" class on all items:*/
            removeActive(x);
            if (currentFocus >= x.length) currentFocus = 0;
            if (currentFocus < 0) currentFocus = (x.length - 1);
            /*add class "autocomplete-active":*/
            x[currentFocus].classList.add("autocomplete-active");
        }
        function removeActive(x) {
            /*a function to remove the "active" class from all autocomplete items:*/
            for (var i = 0; i < x.length; i++) {
                x[i].classList.remove("autocomplete-active");
            }
        }
        function closeAllLists(elmnt) {
            /*close all autocomplete lists in the document,
            except the one passed as an argument:*/
            var x = document.getElementsByClassName("autocomplete-items");
            for (var i = 0; i < x.length; i++) {
                if (elmnt != x[i] && elmnt != inp) {
                    x[i].parentNode.removeChild(x[i]);
                }
            }
        }
        /*execute a function when someone clicks in the document:*/
        document.addEventListener("click", function (e) {
            closeAllLists(e.target);
        });
    }
        var endpoint = '/Pezzi/Fatturato?datetime=';
        var endpointComplessivoOrdinato = '/Pezzi/ComplessivoOrdinatoCalcolo?datetime=';
        var endpointOrdinato = '/Pezzi/Ordinato?datetime=';
        var endpointGleason= '/Pezzi/Gleason?';
        var endpointCommessa = '/Pezzi/Commessa?OP=';
        var endpointPriorita = '/Pezzi/OrdiniPerImportanza?datetime=';
        var isChildOpen = false;

        function openSearch() {
            document.getElementById("myOverlay").style.display = "block";
            $("#inputRicerca").focus();
        }
        $('#inputRicerca').keyup(function (e) {
            e.preventDefault();
            if (e.keyCode == 13) {
                $("#overlayBtnMPA").click();
            }
        });
        function closeSearch() {
            document.getElementById("myOverlay").style.display = "none";
        }
        $(document).ready(function () {
            //const options = {
            //    bottom: '32px', // default: '32px'
            //    right: '32px', // default: '32px'
            //    left: 'unset', // default: 'unset'
            //    time: '0.5s', // default: '0.3s'
            //    mixColor: '#fff', // default: '#fff'
            //    backgroundColor: '#fff',  // default: '#fff'
            //    buttonColorDark: '#100f2c',  // default: '#100f2c'
            //    buttonColorLight: '#fff', // default: '#fff'
            //    saveInCookies: true, // default: true,
            //    label: '🌓', // default: ''
            //    autoMatchOsTheme: true // default: true
            //}

            //const darkmode = new Darkmode(options);
            //darkmode.showWidget();
        //function addDarkmodeWidget() {
        //    new Darkmode().showWidget();
        //}
        //window.addEventListener('load', addDarkmodeWidget);
        $('#jstree_demo_div').jstree({
            "core" : {
            "animation": 0,
            "check_callback": true,
            "themes": { "stripes": true }
            },
            "types": {
                "root": {
                    "icon": "fa-solid fa-gear"
                }
            },
            plugins: ["search", "themes", "types"]
        });
        //$('#jstree_demo_div').on("select_node.jstree", function (e, data) { console.log(data);alert("node_id: " + data.node.id); });
            var cleave = new Cleave('#RicercaOC', {
                delimiter: '-',
                blocks: [4, 2, 7],
                uppercase: true
            });
            hotkeys('ctrl+a', function (event, handler) {
            switch (handler.key) {
                case 'ctrl+a': openSearch();
                    break;
                default: alert(event);
            }
        });
            $.notifyDefaults({
                type: 'success',
                z_index: '5001',
                globalPosition: 'top right',
                style: "metro",
                animate: {
                    enter: 'animated fadeInUp',
                    exit: 'animated fadeOutDown'
                }
            });
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })

    });
    $('body').on('click', '#BtnDownloadDiretto', function (e) {
        var d = $('#NomeDisegno').val();
        d = d.replace(".", "_-_");
        if (d == "") {
            Swal.fire({
                icon: 'error',
                title: 'Attento!',
                text: 'Per effettuare la ricerca di un disegno, inserisci un criterio di ricerca (che sia il codice del disegno o il cliente).'
            });
        } else {
            var type = $(this).data('value');
            window.location = '/Home/DownloadDiretto/' + d.toString();
        }
    });
    $('body').on('click', '#BtnDownloadDirettoEuroma', function (e) {
        var d = $('#NomeDisegno').val();
        d = d.replace(".", "_-_");
        if (d == "") {
            Swal.fire({
                icon: 'error',
                title: 'Attento!',
                text: 'Per effettuare la ricerca di un disegno, inserisci un criterio di ricerca (che sia il codice del disegno o il cliente).'
            });
        } else {
            var type = $(this).data('value');

            window.open('http://192.168.100.50:120/?search=' + d.toString());
        }
    });

    async function SearchLotto() {
        const { value: text } = await Swal.fire({
            input: 'text',
            inputLabel: 'Ricerca Articolo per lotto',
            inputPlaceholder: 'Inserisci qui il codice...',
            inputAttributes: {
                'aria-label': 'Inserisci qui il codice'
            },
            showCancelButton: true,
            confirmButtonText: 'Cerca',
            cancelButtonText: 'Cancella',
        })

        if (text) {
            var id = 0;
            $.ajax({
                url: '/Home/Lotti',
                type: 'POST',
                data: { stringa: text },
                dataType: 'json',
                success: function (result) {
                    if (result.ok) {
                        id = result.id;
                        console.log(result);
                        Swal.fire({
                            title: 'Articolo ' + result.data.Art + "(" + result.data.TipoParte + " - " + result.data.Descr+")",
                            html: "L'articolo ha un <b>lotto minimo di " + result.data.LottoMin + " pezzi </b>e una <b>scorta di sicurezza di " + result.data.Scorta + " pezzi</b>.  Ne sono stati <b>consumati " + result.data.ConsumoAnnoPrec + " pezzi lo scorso </b>anno e <b>" + result.data.ConsumoAnnoCurrent + " quest'anno</b>. Ha una <b>giacenza attuale di " + result.data.CurrentGiacenza + " e " + result.data.AnnoCurrentInAttesa +" in attesa</b> di essere prodotti.",
                            icon: 'info',
                            showCancelButton: false,
                            showDenyButton: false,
                            confirmButtonText: 'Ok!',
                            confirmButtonColor: '#26a360',
                            cancelButtonColor: '#d33'
                        })
                    }
                    else {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                },
                error: function (result) {
                    console.log(result);
                    $.notify({ message: result.message }, { type: 'danger' });
                }
            });
        }
    }
    hotkeys('ctrl+b', function (event, handler) {
        switch (handler.key) {
            case 'ctrl+b': SearchLotto();
                break;
        }
    });
    $('#btnRicercaOC').on('click', function (e) {
        console.log("active");
        var d = $('#RicercaOC').val();
        var formData = new FormData();
        formData.append("id", d);
        $.ajax({
            url: "/Overviews/OrdineExist",
            method: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            async: false,
            success: function (result) {
                if (result.ok) {
                    window.location = '/Overviews/Ordine/' + d.toString();
                }
                else {
                    if (result.type == 1) {
                        Swal.fire({
                            title: 'Creazione',
                            text: "Non esiste nessun ODP esterno a sistema, vuoi crearlo?",
                            icon: 'question',
                            showCancelButton: true,
                            showDenyButton: false,
                            confirmButtonText: 'Si!',
                            cancelButtonText: 'No',
                            confirmButtonColor: '#26a360',
                            cancelButtonColor: '#d33'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $.ajax({
                                    url: "/ProgettiUT/CreateODPEsterno/" + d,
                                    success: function (result) {
                                        if (result.ok) {
                                            $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                                            $('#mainDataTableProgettiEsterniInAttesa').DataTable().ajax.reload();
                                        }
                                        else {
                                            console.log(result);
                                            $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                        }
                                    },
                                    error: function (result) {
                                        console.log(result);
                                        $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                    }
                                });
                            }
                        })
                    }
                    if (result.type == 2) {
                        Swal.fire({
                            title: 'Attenzione',
                            text: "Nessun OC trovato",
                            icon: 'error',
                            showCancelButton: true,
                            showDenyButton: false,
                            confirmButtonText: 'Ok',
                            cancelButtonText: 'No',
                            confirmButtonColor: '#26a360',
                            cancelButtonColor: '#d33'
                        })
                    }
                    if (result.type == 3) {
                        Swal.fire({
                            title: 'Attenzione',
                            text: "ODP esistente esternamente ma nessuna OC trovata a sistema",
                            icon: 'error',
                            showCancelButton: true,
                            showDenyButton: false,
                            confirmButtonText: 'Ok',
                            cancelButtonText: 'No',
                            confirmButtonColor: '#26a360',
                            cancelButtonColor: '#d33'
                        })
                    }
                }
            },
            error: function (result) {
                console.log(result);
                $.notify({ message: result.message }, { type: 'danger' });
            }
        });

    });
        var TableProgettiCompletati = $('#mainDataTableProgettiCompletati').DataTable({
            stateSave: true,
            "ordering": true,
                processing: true,
                serverSide: true,
                ajax: { url: '@Url.Content("~/ProgettiUT/ServerProcessingCompletati")', type: 'POST' },
               "deferRender": true,
                dom: '<"row  align-items-center"<"col col-auto"f><"col"i><"col col-auto"B>>rt<"row align-items-center"<"col"p><"col col-auto"l>>',
                 buttons: [
                    {
                        extend: 'excel',
                        text:'<i class="fas fa-download"></i>',
                        filename: 'Lista Progetti Completati •  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                     {
                         extend: 'print',
                         text: '<i class="fas fa-print"></i>'
                     }],
                columns: [
                    { data: null, orderable: true, class: "details-control", defaultContent: '<i class="fas fa-plus-circle"></i>' },
                    { data: "Priorita", orderable: true },
                    { data: "DataInserimento", orderable: true },
                    { data: "DataRichConsegna", orderable: true },
                    { data: "OC", orderable: true },
                    { data: "Operatore", orderable: true},
                    { data: "StatoProgetto", orderable: true}
               ],
               "columnDefs": [
                   {
                       "targets": [0,3],
                       "searchable": true
                   }
            ],
            "columnDefs": [{
                "targets": [2, 3],    // column index, 0 is the first column
                "type": "date",
                "render": function (data) {
                    // US English uses month-day-year order
                    if (data != null) {
                        console.log(data)
                        var cc = data.replace("/", "").replace("(", "").replace(")", "").replace("/", "").replace("Date", "");
                        var date = new Date(parseInt(cc));
                        console.log(date);
                        return date.toLocaleDateString('it-IT'); // 4/25/2018
                    }
                    return '';
                }
            }],
               select: {
                   targets: 0,
                   data: null,
                   defaultContent: '',
                   orderable: true,
                   className: 'select-checkbox'
                },
                lengthMenu: [[5, 10, 15, 20, 30, 50, 75, 100, -1], [5, 10, 15, 20, 30, 50, 75, 100, "Tutti"]],
                 pageLength: 10,
                 language: {
                     "decimal": ",",
                     "emptyTable": "Nessun Dato Disponibile",
                     "info": "Visualizzazione da _START_ a _END_ di _TOTAL_ Progetti",
                     "infoEmpty": "Visualizzazione da 0 a 0 di 0 Progetti",
                     "infoFiltered": "(Filtrati su _MAX_ Progetti Totali)",
                     "infoPostFix": "",
                     "thousands": ".",
                     "lengthMenu": "Mostra _MENU_",
                     "loadingRecords": "Caricamento...",
                     "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                     "search": "<i class='fas fa-search'></i>",
                     "zeroRecords": "Nessun Progetto",
                     "paginate": {
                         "first": "Prima",
                         "last": "Ultima",
                         "next": "Prossima",
                         "previous": "Precedente"
                     },
                     "aria": {
                         "sortAscending": ": ordina in modo ascendente A-Z",
                         "sortDescending": ": ordina in modo discendente Z-A"
                     }
                },

        });

    var TableLicenze = $('#mainDataTableLicenzeAssociazioni').DataTable({
            stateSave: true,
            "ordering": false,
                processing: true,
                serverSide: true,
                ajax: { url: '@Url.Content("~/Manage/ServerProcessingLicenze")', type: 'POST' },
               "deferRender": true,
                dom: '<"row  align-items-center"<"col col-auto"f><"col"i><"col col-auto"B>>rt<"row align-items-center"<"col"p><"col col-auto"l>>',
                 buttons: [
                    {
                        extend: 'excel',
                        text:'<i class="fas fa-download"></i>',
                        filename: 'Lista Licenze •  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                     {
                         extend: 'print',
                         text: '<i class="fas fa-print"></i>'
                     }],
                columns: [
                    { data: "Utente", orderable: false },
                    { data: "Licenza", orderable: false },
                    { data: "Descrizione", orderable: false },
                    { data: "DataScadenza", orderable: false },
                    { data: "Qta", orderable: false }
               ],
               "columnDefs": [
                   {
                       "targets": [0,2],
                       "searchable": true
                   }
            ],
               select: {
                   targets: 0,
                   data: null,
                   defaultContent: '',
                   orderable: true,
                   className: 'select-checkbox'
                },
                lengthMenu: [[5, 10, 15, 20, 30, 50, 75, 100, -1], [5, 10, 15, 20, 30, 50, 75, 100, "Tutti"]],
                 pageLength: 10,
                 language: {
                     "decimal": ",",
                     "emptyTable": "Nessun Dato Disponibile",
                     "info": "Visualizzazione da _START_ a _END_ di _TOTAL_ Licenze",
                     "infoEmpty": "Visualizzazione da 0 a 0 di 0 Licenze",
                     "infoFiltered": "(Filtrati su _MAX_ Licenze Totali)",
                     "infoPostFix": "",
                     "thousands": ".",
                     "lengthMenu": "Mostra _MENU_",
                     "loadingRecords": "Caricamento...",
                     "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                     "search": "<i class='fas fa-search'></i>",
                     "zeroRecords": "Nessun Progetto",
                     "paginate": {
                         "first": "Prima",
                         "last": "Ultima",
                         "next": "Prossima",
                         "previous": "Precedente"
                     },
                     "aria": {
                         "sortAscending": ": ordina in modo ascendente A-Z",
                         "sortDescending": ": ordina in modo discendente Z-A"
                     }
                },

        });
        var TableProgettiEsterniInAttesa = $('#mainDataTableProgettiEsterniInAttesa').DataTable({
            stateSave: true,
            "ordering": true,
                processing: true,
                serverSide: true,
                ajax: { url: '@Url.Content("~/ProgettiUT/ServerProcessingEsterni")', type: 'POST' },
               "deferRender": true,
                dom: '<"row  align-items-center"<"col col-auto"f><"col"i><"col col-auto"B>>rt<"row align-items-center"<"col"p><"col col-auto"l>>',
                 buttons: [
                    {
                        extend: 'excel',
                        text:'<i class="fas fa-download"></i>',
                        filename: 'Lista Progetti Esterni •  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                     {
                         extend: 'print',
                         text: '<i class="fas fa-print"></i>'
                     }],
                columns: [
                    { data: null, orderable: true, class: "details-control", defaultContent: '<i class="fas fa-plus-circle"></i>' },
                    { data: "Priorita", orderable: true },
                    { data: "DataRichConsegna", orderable: true },
                    { data: "OC", orderable: true },
                    { data: "Operatore", orderable: true},
                    { data: "StatoProgetto", orderable: true}
               ],
               "columnDefs": [
                   {
                       "targets": [0,3],
                       "searchable": true
                   }
            ],
               select: {
                   targets: 0,
                   data: null,
                   defaultContent: '',
                   orderable: true,
                   className: 'select-checkbox'
                },
                lengthMenu: [[5, 10, 15, 20, 30, 50, 75, 100, -1], [5, 10, 15, 20, 30, 50, 75, 100, "Tutti"]],
                 pageLength: 10,
                 language: {
                     "decimal": ",",
                     "emptyTable": "Nessun Dato Disponibile",
                     "info": "Visualizzazione da _START_ a _END_ di _TOTAL_ Progetti",
                     "infoEmpty": "Visualizzazione da 0 a 0 di 0 Progetti",
                     "infoFiltered": "(Filtrati su _MAX_ Progetti Totali)",
                     "infoPostFix": "",
                     "thousands": ".",
                     "lengthMenu": "Mostra _MENU_",
                     "loadingRecords": "Caricamento...",
                     "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                     "search": "<i class='fas fa-search'></i>",
                     "zeroRecords": "Nessun Progetto",
                     "paginate": {
                         "first": "Prima",
                         "last": "Ultima",
                         "next": "Prossima",
                         "previous": "Precedente"
                     },
                     "aria": {
                         "sortAscending": ": ordina in modo ascendente A-Z",
                         "sortDescending": ": ordina in modo discendente Z-A"
                     }
                },

        });
        var TableProgettiEsterniCompletati = $('#mainDataTableProgettiEsterniCompletati').DataTable({
            stateSave: true,
            "ordering": true,
                processing: true,
                serverSide: true,
                ajax: { url: '@Url.Content("~/ProgettiUT/ServerProcessingEsterniCompletati")', type: 'POST' },
               "deferRender": true,
                dom: '<"row  align-items-center"<"col col-auto"f><"col"i><"col col-auto"B>>rt<"row align-items-center"<"col"p><"col col-auto"l>>',
                 buttons: [
                    {
                        extend: 'excel',
                        text:'<i class="fas fa-download"></i>',
                        filename: 'Lista Progetti Esterni Completati•  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                     {
                         extend: 'print',
                         text: '<i class="fas fa-print"></i>'
                     }],
                columns: [
                    { data: null, orderable: true, class: "details-control", defaultContent: '<i class="fas fa-plus-circle"></i>' },
                    { data: "Priorita", orderable: true },
                    { data: "DataRichConsegna", orderable: true },
                    { data: "OC", orderable: true },
                    { data: "Operatore", orderable: true},
                    { data: "StatoProgetto", orderable: true}
               ],
               "columnDefs": [
                   {
                       "targets": [0,3],
                       "searchable": true
                   }
            ],
               select: {
                   targets: 0,
                   data: null,
                   defaultContent: '',
                   orderable: true,
                   className: 'select-checkbox'
                },
                lengthMenu: [[5, 10, 15, 20, 30, 50, 75, 100, -1], [5, 10, 15, 20, 30, 50, 75, 100, "Tutti"]],
                 pageLength: 10,
                 language: {
                     "decimal": ",",
                     "emptyTable": "Nessun Dato Disponibile",
                     "info": "Visualizzazione da _START_ a _END_ di _TOTAL_ Progetti",
                     "infoEmpty": "Visualizzazione da 0 a 0 di 0 Progetti",
                     "infoFiltered": "(Filtrati su _MAX_ Progetti Totali)",
                     "infoPostFix": "",
                     "thousands": ".",
                     "lengthMenu": "Mostra _MENU_",
                     "loadingRecords": "Caricamento...",
                     "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                     "search": "<i class='fas fa-search'></i>",
                     "zeroRecords": "Nessun Progetto",
                     "paginate": {
                         "first": "Prima",
                         "last": "Ultima",
                         "next": "Prossima",
                         "previous": "Precedente"
                     },
                     "aria": {
                         "sortAscending": ": ordina in modo ascendente A-Z",
                         "sortDescending": ": ordina in modo discendente Z-A"
                     }
                },

        });

        $('body').on('click', '#overlayBtnMPA', function (e) {
            var d = $('#inputRicerca').val();
            d = d.replace(".", "_-_");
            console.log($(this).data('value'));
            var type = $(this).data('value');
            window.location = '/Home/RicercaDisegni/' + type + "+" + d.toString();
        });
    $('body').on('click', '#BtnDisegni', function (e) {
        var d = $('#NomeDisegno').val();
        if (d == "") {
            Swal.fire({
                icon: 'error',
                title: 'Attento!',
                text: 'Per effettuare la ricerca di un disegno, inserisci un criterio di ricerca (che sia il codice del disegno o il cliente).'
            });
        } else {
            d = d.replace(".", "_-_");
            console.log($(this).data('value'));
            var type = $(this).data('value');
            window.location = '/Home/RicercaDisegni/' + type + "+" + d.toString();
        }
    });
    function ChangeComplessivo(id, macchina) {
        $.ajax({
        url: '/Macchine/DetailsPostComplessivo?id=' + id + "&macchina=" + macchina,
        type: 'POST',
        success: function (result) {
            if (result.ok) {
                console.log(result);
                switch (id) {
                    case "1":
                        $(".Complessivo").text("Tempo Taglio Compl.")
                        break;
                    case "2":
                        $(".Complessivo").text("Tempo Mandrino Compl.")
                        break;
                    case "3":
                        $(".Complessivo").text("Operatività Compl.")
                        break;
                    case "4":
                        $(".Complessivo").text("Esecuzione Compl.")
                        break;
                }
                var kmkLabelComplessivo = Object.keys(result.data)
                var kmkDataComplessivo = Object.values(result.data)
                $("#ChartComplessivo").remove();
                $('#ContainerComplessivo').append('<canvas id="ChartComplessivo"></canvas>');
                window.ChartComplessivo = new Chart(document.getElementById("ChartComplessivo"), {
                    type: 'line',
                    data: {
                        labels: kmkLabelComplessivo,
                        datasets: [
                            {
                                label: "# Tempo",
                                backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                                data: kmkDataComplessivo
                            }
                        ]
                    },
                    options: {
                        bezierCurve: true,
                        legend: { display: false },
                        title: {
                            display: true,
                            text: 'Predicted world population (millions) in 2050'
                        },
                    }
                });
            }
            else {
                console.log(result);
                $.notify({ message: result.message }, { type: 'danger' });
            }
        },
        error: function (result) {
            console.log(result);
            $.notify({ message: result.message }, { type: 'danger' });
        }
    });

    }
    $('#NomeDisegno').keyup(function (e) {
        e.preventDefault();
        if (e.keyCode == 13) {
            $("#BtnDisegni").click();
        }
    });
        $('#RicercaOC').keyup(function (e) {
            e.preventDefault();
            if (e.keyCode == 13) {
                $("#btnRicercaOC").click();
            }
        });
    $('body').on('click', '.ModalSubmit', function (e) {
            e.preventDefault();
            $('body .modal').modal('hide');
            $("body .ModalForm").submit();
    });
    $('body').on('submit', '.LavEst', function (e) {
            e.preventDefault();
            var form = $(this);
            var formData = new FormData($(this)[0]);
                $.ajax({
                    url: form.attr('action'),
                    method: form.attr('method'),
                    data: formData,
                    processData: false,
                    contentType: false,
                    async: false,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ message: result.message }, { type: 'success' });
                        }
                        else {
                            console.log(result);
                            $.notify({ message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                });
            return false;
        });
    $('body').on('submit', '.ModalForm', function (e) {
            e.preventDefault();
            var form = $(this);
            var formData = new FormData($(this)[0]);
            if (form.attr('action').includes("CreaEmail")) {
                var files = $('#attachmentsUpload').get(0).dropzone.getAcceptedFiles();
                for (let x = 0; x < files.length; x++) {
                    console.log(files[x]);
                    formData.append('files', files[x]);
                }
                $.ajax({
                    url: form.attr('action'),
                    method: form.attr('method'),
                    data: formData,
                    processData: false,
                    contentType: false,
                    async: false,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ message: result.message }, { type: 'success' });
                            window.location.reload();
                        }
                        else {
                            console.log(result);
                            $.notify({ message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                });
            } else if (form.attr('action').includes("/Edit/")) {
                var files = $('#newFileUpload').get(0).dropzone.getAcceptedFiles();
                for (let x = 0; x < files.length; x++) {
                    console.log(files[x]);
                    formData.append('files', files[x]);
                }
                $.ajax({
                    url: form.attr('action'),
                    method: form.attr('method'),
                    data: formData,
                    processData: false,
                    contentType: false,
                    async: false,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ message: result.message }, { type: 'success' });
                            window.location.reload();
                        }
                            console.log(result);
                            $.notify({ message: result.message }, { type: 'danger' });
                        },
                    error: function (result) {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                });
            } else if (form.attr('action').includes("/EditOperatore/")) {
                var files = $('#ProgettoFileUpload').get(0).dropzone.getAcceptedFiles();
                for (let x = 0; x < files.length; x++) {
                    console.log(files[x]);
                    formData.append('files', files[x]);
                }
                $.ajax({
                    url: form.attr('action'),
                    method: form.attr('method'),
                    data: formData,
                    processData: false,
                    contentType: false,
                    async: false,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ message: result.message }, { type: 'success' });
                            window.location.reload();
                        }
                        else {
                            console.log(result);
                            $.notify({ message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                });
            } else if (form.attr('action').includes("/ModificaLicenza/")) {
                var files = $('#LicenzaFileUpload').get(0).dropzone.getAcceptedFiles();
                for (let x = 0; x < files.length; x++) {
                    console.log(files[x]);
                    formData.append('files', files[x]);
                }
                $.ajax({
                    url: form.attr('action'),
                    method: form.attr('method'),
                    data: formData,
                    processData: false,
                    contentType: false,
                    async: false,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ message: result.message }, { type: 'success' });
                            window.location.reload();
                        }
                        else {
                            console.log(result);
                            $.notify({ message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                });
            }else {
                $.ajax({
                    url: form.attr('action'),
                    method: form.attr('method'),
                    data: formData,
                    processData: false,
                    contentType: false,
                    async: false,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ message: result.message }, { type: 'success' });
                            window.location.reload();
                        }
                        else {
                            console.log(result);
                            $.notify({ message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                });
            }

            ////};

            return false;
    });
        $('body').on('click', '#FirstLvl', function (e) {
            Swal.fire(
                'Accettazione OC',
                'La OC/OT è stata accettata e smistata correttamente dall\'ufficio commerciale',
            );
        })
        $('body').on('click', '#SecondLvl', function (e) {
            Swal.fire(
                'Accettazione OC',
                'La OC/OT è stata accettata e smistata correttamente dall\'ufficio commerciale',
            );
        })
        $('body').on('click', '#ThirdLvl', function (e) {
            Swal.fire(
                'Accettazione OC',
                'La OC/OT è stata accettata e smistata correttamente dall\'ufficio commerciale',
            );
        })
        $('body').on('click', '#FourthLvl', function (e) {
            Swal.fire(
                'Accettazione OC',
                'La OC/OT è stata accettata e smistata correttamente dall\'ufficio commerciale',
            );
        })
        $('body').on('click', '#FifthLvl', function (e) {
            Swal.fire(
                'Accettazione OC',
                'La OC/OT è stata accettata e smistata correttamente dall\'ufficio commerciale',
            );
        })
    $('#btn_notifiche').on('click', function (e) {
        if ($(".notifiche").length == 0) {
            $('#btn_notifiche').append($('<div id="ContainerNotifiche">').load('/Home/Notifiche/'));
        }
        else {
            $('#ContainerNotifiche').remove();
        }
    })
    $('body').on('show.bs.modal', '.modal', function (e) {
            var button = $(e.relatedTarget); // Button that triggered the modal
            var recipient = button.data('value');
            var type = button.data('type');
            switch (type) {
                case 'view':
                    $(this).find('.modal-title').removeClass('text-danger').html('Visualizza Accettazione');
                    $(this).data('reload', false);
                    $(this).find('.Send').hide();
                    $(this).find('.Add').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/AccettazioneUC/Details/' + recipient);
                    break;
                case 'downloadFatturato':
                    $(this).find('.modal-title').removeClass('text-danger').html('Scarica Fatturato');
                    $(this).data('reload', false);
                    $(this).find('.Send').hide();
                    $(this).find('.Add').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Pezzi/FatturatoPage/');
                    break;
                case 'downloadPriorita':
                    $(this).find('.modal-title').removeClass('text-danger').html('Scarica OC per Priorita');
                    $(this).data('reload', false);
                    $(this).find('.Send').hide();
                    $(this).find('.Add').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Pezzi/PrioritaPage/');
                    break;
                    
                case 'analisiGleason':
                    $(this).find('.modal-title').removeClass('text-danger').html('Analisi Gleason');
                    $(this).data('reload', false);
                    $(this).find('.Send').hide();
                    $(this).find('.Add').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Pezzi/AnalisiGleason/');
                    break;
                case 'downloadOrdinato':
                    $(this).find('.modal-title').removeClass('text-danger').html('Scarica Ordinato');
                    $(this).data('reload', false);
                    $(this).find('.Send').hide();
                    $(this).find('.Add').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Pezzi/OrdinatoPage/');
                    break;
                case 'downloadCompOrdinato':
                    $(this).find('.modal-title').removeClass('text-danger').html('Previsione Ordinato');
                    $(this).data('reload', false);
                    $(this).find('.Send').hide();
                    $(this).find('.Add').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Pezzi/ComplessivoOrdinato/');
                    break;
                case 'add':
                    $(this).find('.modal-title').removeClass('text-danger').html('Aggiungi PDF');
                    $(this).data('reload', true);
                    $(this).find('.Send').hide();
                    $(this).find('.Add').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/AccettazioneUC/Create', function () {
                    });
                    break;
                case 'add_computer':
                    $(this).find('.modal-title').removeClass('text-danger').html('Aggiungi PC');
                    $(this).data('reload', true);
                    $(this).find('.Send').hide();
                    $(this).find('.Add').show();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Manage/AddComputer/');
                    break;
                case 'add_articolo_magazzion':
                    $(this).find('.modal-title').removeClass('text-danger').html('Aggiungi Articolo');
                    $(this).data('reload', true);
                    $(this).find('.Send').hide();
                    $(this).find('.Add').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Overviews/CreateArticolo/' + recipient, function () {
                    });
                    break;
                case 'edit':
                    $(this).find('.modal-title').removeClass('text-danger').html('Modifica Accettazione');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/AccettazioneUC/Edit/' + recipient, function () {
                    });
                    break;

                case 'edit_controller_produzione':
                    $(this).find('.modal-title').removeClass('text-danger').html('Modifica Progetto');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/ProgettiProd/EditAdmin/' + recipient, function () {
                    });
                    break;
                case 'aggiungi_articolo':
                    $(this).find('.modal-title').removeClass('text-danger').html('Crea articolo su magazzino');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Overviews/CreateArticolo/', function () {
                    });
                    break;
                case 'visualizza_scaffale':
                    $(this).find('.modal-title').removeClass('text-danger').html('Articoli presenti nello slot: ' + recipient);
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Overviews/ListaArticoli/' + recipient, function () {
                    });
                    break;
                case 'edit_articolo_magazzino':
                    $(this).find('.modal-title').removeClass('text-danger').html('Modifica Articolo');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Overviews/EditArticolo/' + recipient, function () {
                    });
                    break;
                case 'edit_tecnico_admin':
                    $(this).find('.modal-title').removeClass('text-danger').html('Modifica Progetto');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/ProgettiUT/EditAdmin/' + recipient, function () {
                    });
                    break;
                case 'edit_tecnico_operatore':
                    $(this).find('.modal-title').removeClass('text-danger').html('Modifica Progetto');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/ProgettiUT/EditOperatore/' + recipient, function () {
                    });
                    break;
                case 'edit_tecnico_esterno':
                    $(this).find('.modal-title').removeClass('text-danger').html('Modifica ODP');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/ProgettiUT/EditEsterno/' + recipient, function () {
                    });
                    break;
                case 'show_gantt':
                    $(this).find('.modal-title').removeClass('text-danger').html('Gantt ODP ' + recipient);
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Overviews/Gantt/' + recipient, function () {
                    });
                    break;
                case 'show_mancanti':
                    $(this).find('.modal-title').removeClass('text-danger').html('Lista mancanti '+ recipient);
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Overviews/Mancanti/' + recipient, function () {
                    });
                    break;
                case 'Show_Carico':
                    $(this).find('.modal-title').removeClass('text-danger').html('Carico Ufficio Tecnico');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/AccettazioneUC/CaricoUT/', function () {
                    });
                    break;
                case 'editUser':
                    $(this).find('.modal-title').removeClass('text-danger').html('Modifica Utente');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Account/_EditUtente/', function () {
                    });

                    break;
                case 'editAcc':
                    $(this).find('.modal-title').removeClass('text-danger').html('Modifica Accettazione');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/AccettazioneUC/EditAccettazione/' + recipient, function () {
                    });
                    break;
                case 'MPA_Details':
                    $(this).find('.modal-title').removeClass('text-danger').html('Dettagli Articolo MPA');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Home/DettagliMPA/' + recipient, function () {
                    });
                    break;
                case 'add_ticket':
                    $(this).find('.modal-title').removeClass('text-danger').html('Aggiungi Ticket');
                    $(this).data('reload', true);
                    $(this).find('.Add').show();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/HelpDesks/Create/', function () {
                    });
                    break;
                case 'edit_ticket':
                    $(this).find('.modal-title').removeClass('text-danger').html('Modifica Ticket');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/HelpDesks/EditTicket/' + recipient, function () {
                    });
                    break;
                case 'details_ticket':
                    $(this).find('.modal-title').removeClass('text-danger').html('Visualizza Ticket');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/HelpDesks/Details/' + recipient, function () {
                    });
                    break;
                case 'email':
                    $(this).find('.modal-title').removeClass('text-danger').html('Nuova Email');
                    $(this).data('reload', true);
                    $(this).find('.Send').show();
                    $(this).find('.Add').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/AccettazioneUC/CreaEmail/' + recipient, function () {
                    });
                    break;
                case 'createAcc':
                    $(this).find('.modal-title').removeClass('text-danger').html('Crea Account');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Account/_NuovoUtente/', function () {
                    });
                    break;
                case 'createLic':
                    $(this).find('.modal-title').removeClass('text-danger').html('Crea Licenza');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Manage/AggiungiLicenza/', function () {
                    });
                    break;
                case 'editLic':
                    $(this).find('.modal-title').removeClass('text-danger').html('Modifica Licenza');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Manage/ModificaLicenza/' + recipient, function () {
                    });
                    break;
                case 'createAss':
                    $(this).find('.modal-title').removeClass('text-danger').html('Crea Associazione utente-licenza');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').hide();
                    $(this).find('.Save').show();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Manage/CreateAssociazione/', function () {
                    });
                    break;
                case 'delete':
                    $(this).find('.modal-title').addClass('text-danger').html('Elimina Unità Immobiliare');
                    $(this).data('reload', true);
                    $(this).find('.Add').hide();
                    $(this).find('.Send').hide();
                    $(this).find('.Delete').show();
                    $(this).find('.Save').hide();
                    $(this).find('.SaveClose').hide();
                    $(this).find('.modal-body').html('').load('/Condomini/_UnitaImmobiliareElimina/' + recipient);
                    break;

            };
    });
        function showCarico(ctx, json) {
            console.log(Object.keys(json).map(key => json[key]));
            var labels = json.map(function (e) {
                return e.Nome;
            });
            var data = json.map(function (e) {
                return e.Number;
            });;
            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Progetti in corso',
                        data: data,
                        backgroundColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderColor: [
                            'rgba(255,99,132,1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            gridLines: {
                                drawBorder: false,
                            },
                            ticks: {
                                beginAtZero: true,
                                precision: 0
                            }
                        }]
                    }
                }
            });

        }
    function getdetails(id, url, token) {
        console.log(id + " - " + url);
        var div = $('<div/>').addClass('loading').text('Caricamento Dettagli...');
        console.log(div);
        $.ajax({
            url: url,
            method: 'post',
            data: {
                __RequestVerificationToken: token
            },
            success: function (result) {
                if (result.ok) {
                    div.html(result.message).removeClass('loading');
                    $('[data-toggle="tooltip"]').tooltip();
                }
                else {
                    $.notify({ icon: 'fas fa-exclamation-triangle', message: result.message }, { type: 'danger' });
                }
            },
            error: function (result) {
                $.notify({ icon: 'fas fa-exclamation-triangle', message: result.message }, { type: 'danger' });
            }
        });
        return div;
    }
    $('#mainDataTableAccettazione').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = $('#mainDataTableAccettazione').DataTable().row(tr);
        var id = $(this).parent().data('value');
        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
            $(this).html('<i class="fas fa-plus-circle"></i>');
            isChildOpen = false;
        }
        else {
            // Open this row
            //alert($(this).parent().data('value'));
            row.child(getdetails(id, '/AccettazioneUC/Details/' + id, $('body[name = "__RequestVerificationToken"]').val())).show();
            tr.addClass('shown');
            $(this).html('<i class="fas fa-minus-circle"></i>');
            isChildOpen = true;
        }
    });
    $('#mainDataTableProgetti').on('click', 'td.details-control', function () {
            var tr = $(this).closest('tr');
            var row = $('#mainDataTableProgetti').DataTable().row(tr);
            var id = $(this).parent().data('value');
            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
                tr.removeClass('shown');
                $(this).html('<i class="fas fa-plus-circle"></i>');
                isChildOpen = false;
            }
            else {
                // Open this row
                //alert($(this).parent().data('value'));
                row.child(getdetails(id, '/ProgettiUT/Details/' + id, $('body[name = "__RequestVerificationToken"]').val())).show();
                tr.addClass('shown');
                $(this).html('<i class="fas fa-minus-circle"></i>');
                isChildOpen = true;
            }
    });
    $('#mainDataTableProgettiEsterniInAttesa').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = $('#mainDataTableProgettiEsterniInAttesa').DataTable().row(tr);
        var id = $(this).parent().data('value');
        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
            $(this).html('<i class="fas fa-plus-circle"></i>');
            isChildOpen = false;
        }
        else {
            // Open this row
            //alert($(this).parent().data('value'));
            row.child(getdetails(id, '/ProgettiUT/DetailsEst/' + id, $('body[name = "__RequestVerificationToken"]').val())).show();
            tr.addClass('shown');
            $(this).html('<i class="fas fa-minus-circle"></i>');
            isChildOpen = true;
        }
    });
    $('#mainDataTableProgettiEsterniCompletati').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = $('#mainDataTableProgettiEsterniCompletati').DataTable().row(tr);
        var id = $(this).parent().data('value');
        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
            $(this).html('<i class="fas fa-plus-circle"></i>');
            isChildOpen = false;
        }
        else {
            // Open this row
            //alert($(this).parent().data('value'));
            row.child(getdetails(id, '/ProgettiUT/DetailsEst/' + id, $('body[name = "__RequestVerificationToken"]').val())).show();
            tr.addClass('shown');
            $(this).html('<i class="fas fa-minus-circle"></i>');
            isChildOpen = true;
        }
    });
        $('#mainDataTableProgettiCompletati').on('click', 'td.details-control', function () {
            var tr = $(this).closest('tr');
            var row = $('#mainDataTableProgettiCompletati').DataTable().row(tr);
            var id = $(this).parent().data('value');
            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
                tr.removeClass('shown');
                $(this).html('<i class="fas fa-plus-circle"></i>');
                isChildOpen = false;
            }
            else {
                // Open this row
                //alert($(this).parent().data('value'));
                row.child(getdetails(id, '/ProgettiUT/Details/' + id, $('body[name = "__RequestVerificationToken"]').val())).show();
                tr.addClass('shown');
                $(this).html('<i class="fas fa-minus-circle"></i>');
                isChildOpen = true;
            }
        });
    $('#mainDataTableProduzione').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = $('#mainDataTableProduzione').DataTable().row(tr);
        var id = $(this).parent().data('value');
        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
            $(this).html('<i class="fas fa-plus-circle"></i>');
            isChildOpen = false;
        }
        else {
            // Open this row
            //alert($(this).parent().data('value'));
            row.child(getdetails(id, '/ProgettiProd/Details/' + id, $('body[name = "__RequestVerificationToken"]').val())).show();
            tr.addClass('shown');
            $(this).html('<i class="fas fa-minus-circle"></i>');
            isChildOpen = true;
        }
    });
    $('#mainDataTableProduzioneCompletati').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = $('#mainDataTableProduzioneCompletati').DataTable().row(tr);
        var id = $(this).parent().data('value');
        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
            $(this).html('<i class="fas fa-plus-circle"></i>');
            isChildOpen = false;
        }
        else {
            // Open this row
            //alert($(this).parent().data('value'));
            row.child(getdetails(id, '/ProgettiProd/Details/' + id, $('body[name = "__RequestVerificationToken"]').val())).show();
            tr.addClass('shown');
            $(this).html('<i class="fas fa-minus-circle"></i>');
            isChildOpen = true;
        }
    });
    $('#mainDataTableDisegni').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = $('#mainDataTableDisegni').DataTable().row(tr);
        var id = $(this).parent().data('value');
        if (row.child.isShown()) {
            //  This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
            $(this).html('<i class="fas fa-plus-circle"></i>');
            isChildOpen = false;
        }
        else {
            row.child(getdetails(id, '/Home/DettagliMPA/' + id, $('body[name = "__RequestVerificationToken"]').val())).show();
            tr.addClass('shown');
            $(this).html('<i class="fas fa-minus-circle"></i>');
            isChildOpen = true;
        }
    });
      var table = $('#mainDataTableDisegni').DataTable({
                stateSave: true,
                order: [[3, 'desc']],
                processing: true,
                serverSide: true,
                ajax: {
                    url: '@Url.Content("~/Home/ServerProcessing")',
                    type: 'POST',
                    "data": function (d) {
                        d.art = $('.nomeArticolo').text();
                    }
                },
               "deferRender": true,
                      dom: 'Blfrtip',
                      language: {
                    "decimal": ",",
                    "emptyTable": "Nessun Dato Disponibile",
                    "info": "Visualizzazione da _START_ a _END_ di _TOTAL_ disegni",
                    "infoEmpty": "Visualizzazione da 0 a 0 di 0 disegni",
                    "infoFiltered": "(Filtrati su _MAX_ Disegni Totali)",
                    "infoPostFix": "",
                    "thousands": ".",
                    "lengthMenu": "Mostra _MENU_",
                    "loadingRecords": "Caricamento...",
                    "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                    "search": "<i class='fas fa-search'></i>",
                    "zeroRecords": "Nessun Disegno",
                    "paginate": {
                        "first": "Prima",
                        "last": "Ultima",
                        "next": "Prossima",
                        "previous": "Precedente"
                    },
                    "aria": {
                        "sortAscending": ": ordina in modo ascendente A-Z",
                        "sortDescending": ": ordina in modo discendente Z-A"
                    }
                },
                buttons: [
                    {
                        extend: 'excel',
                        text:'Excel',
                        filename: 'Lista Disegni •  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                    {
                extend: 'pdfHtml5',
                orientation: 'landscape',
                        pageSize: 'LEGAL',
                customize: function (doc) {
                                            doc.content[1].table.widths =
                                                Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                                              doc.content[1].margin = [ 100, 0, 100, 0 ] //left, top, right, bottom

                                        }
                                    },
                    {
                        extend: 'print',
                        text: 'Stampa',
                        orientation: 'landscape'
                     }
                ],
                columns: [
                    { data: null, orderable: false, class: "details-control", defaultContent: '<i class="fas fa-plus-circle"></i>' },
                    { data: "Code_Disegno", orderable: false, },
                    { data: "Desc", searchable: false },
                    { data: "Path", searchable: false }
                ],
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],

                                "initComplete": function (settings, json) {
                                    $('#loader').remove();
                                    $('.main-table').show();
                                    $('body').css('overflow', '');
                                    $(this).show()
                                }
            });
    $(document).ready(function () {
     
            /* Plugin API method to determine is a column is sortable */
            $.fn.dataTable.Api.register('column().searchable()', function () {
                var ctx = this.context[0];
                return ctx.aoColumns[this[0]].bSearchable;
            });

        var TableAccettazione = $('#mainDataTableAccettazione').DataTable({
            stateSave: true,
            "ordering": false,
                processing: true,
                serverSide: true,
                ajax: { url: '@Url.Content("~/AccettazioneUC/ServerProcessing")', type: 'POST' },
               "deferRender": true,
            dom: '<"row  align-items-center"<"col col-auto"f><"col"i><"col col-auto"B>>rt<"row align-items-center"<"col"p><"col col-auto"l>>',
                 buttons: [
                    {
                        extend: 'excel',
                        text:'<i class="fas fa-download"></i>',
                        filename: 'Lista Accettazioni •  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                     {
                         extend: 'print',
                         text: '<i class="fas fa-print"></i>'
                     }],
                columns: [
                   { data: null, orderable: false, class: "details-control", defaultContent: '<i class="fas fa-plus-circle"></i>' },
                    { data: "Accettato", orderable: false,},
                    { data: "OC", searchable: false},
                    { data: "Cartella", searchable: false},
                    { data: "OperatoreInsert", searchable: false},
                    { data: "DataCreazione", searchable: false},
               ],
               "columnDefs": [
                   {
                       "targets": [0,4],
                       "searchable": false
                   }
            ],
               select: {
                   targets: 0,
                   data: null,
                   defaultContent: '',
                   orderable: false,
                   className: 'select-checkbox'
            },
            initComplete: function () {
                createDropdowns(this.api());
            },
                lengthMenu: [[5, 10, 15, 20, 30, 50, 75, 100, -1], [5, 10, 15, 20, 30, 50, 75, 100, "Tutti"]],
                 pageLength: 10,
                 language: {
                     "decimal": ",",
                     "emptyTable": "Nessun Dato Disponibile",
                     "info": "Visualizzazione da _START_ a _END_ di _TOTAL_ Accettazioni",
                     "infoEmpty": "Visualizzazione da 0 a 0 di 0 Accettazion1",
                     "infoFiltered": "(Filtrati su _MAX_ Accettazioni Totali)",
                     "infoPostFix": "",
                     "thousands": ".",
                     "lengthMenu": "Mostra _MENU_",
                     "loadingRecords": "Caricamento...",
                     "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                     "search": "<i class='fas fa-search'></i>",
                     "zeroRecords": "Nessuna Accettazione",
                     "paginate": {
                         "first": "Prima",
                         "last": "Ultima",
                         "next": "Prossima",
                         "previous": "Precedente"
                     },
                     "aria": {
                         "sortAscending": ": ordina in modo ascendente A-Z",
                         "sortDescending": ": ordina in modo discendente Z-A"
                     }
                },

        });
        var TableProgetti = $('#mainDataTableProgetti').DataTable({
            stateSave: true,
            "ordering": true,
                processing: true,
                serverSide: true,
                ajax: { url: '@Url.Content("~/ProgettiUT/ServerProcessing")', type: 'POST' },
               "deferRender": true,
                dom: '<"row  align-items-center"<"col col-auto"f><"col"i><"col col-auto"B>>rt<"row align-items-center"<"col"p><"col col-auto"l>>',
                 buttons: [
                    {
                        extend: 'excel',
                        text:'<i class="fas fa-download"></i>',
                        filename: 'Lista Progetti •  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                     {
                         extend: 'print',
                         text: '<i class="fas fa-print"></i>'
                     }],
                columns: [
                    { data: null, orderable: false, class: "details-control", defaultContent: '<i class="fas fa-plus-circle"></i>' },
                    { data: "Priorita", orderable: true },
                    { data: "DataInserimento", orderable: true },
                    { data: "DataRichConsegna", orderable: true },
                    { data: "OC", orderable: true },
                    { data: "Operatore", orderable: true},
                    { data: "StatoProgetto", searchable: true}
            ],
               "columnDefs": [
                   {
                       "targets": [0,3],
                       "searchable": true,
                       "type": "num-html"
                   }
            ],
            "columnDefs": [{
                "targets": [2,3],    // column index, 0 is the first column
                "type": "date",
                "render": function (data) {
                    // US English uses month-day-year order
                    var cc = data.replace("/", "").replace("(", "").replace(")", "").replace("/", "").replace("Date", "");
                    var date = new Date(parseInt(cc));
                    console.log(date);
                    return date.toLocaleDateString('it-IT'); // 4/25/2018
                }
            }],
               select: {
                   targets: 0,
                   data: null,
                   defaultContent: '',
                   orderable: true,
                   className: 'select-checkbox'
            },
                lengthMenu: [[5, 10, 15, 20, 30, 50, 75, 100, -1], [5, 10, 15, 20, 30, 50, 75, 100, "Tutti"]],
                 pageLength: 10,
                 language: {
                     "decimal": ",",
                     "emptyTable": "Nessun Dato Disponibile",
                     "info": "Visualizzazione da _START_ a _END_ di _TOTAL_ Progetti",
                     "infoEmpty": "Visualizzazione da 0 a 0 di 0 Progetti",
                     "infoFiltered": "(Filtrati su _MAX_ Progetti Totali)",
                     "infoPostFix": "",
                     "thousands": ".",
                     "lengthMenu": "Mostra _MENU_",
                     "loadingRecords": "Caricamento...",
                     "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                     "search": "<i class='fas fa-search'></i>",
                     "zeroRecords": "Nessun Progetto",
                     "paginate": {
                         "first": "Prima",
                         "last": "Ultima",
                         "next": "Prossima",
                         "previous": "Precedente"
                     },
                     "aria": {
                         "sortAscending": ": ordina in modo ascendente A-Z",
                         "sortDescending": ": ordina in modo discendente Z-A"
                     }
                },

        });

        var UserTable = $('#mainDataTableUser').DataTable();
        var UserLicenze = $('#mainDataTableLicenze').DataTable();
        var TableSchedulatore = $('#mainDataTableSchedulatore').DataTable({
            stateSave: true,
            "ordering": true,
            processing: true,
                serverSide: true,
                drawCallback: function (settings, json) {
                    $('[data-toggle="tooltip"]').tooltip('update');
                    //$("#list-of-product tbody tr > td").tooltip('hide');
                },
                ajax: { url: '@Url.Content("~/ProgettiProd/ServerProcessingSchedulatore")', type: 'POST' },
               "deferRender": true,
                 dom: '<"row  align-items-center"<"col col-auto"f><"col"i><"col col-auto"B>>rt<"row align-items-center"<"col"p><"col col-auto"l>>',
                 buttons: [
                    {
                        extend: 'excel',
                        text:'<i class="fas fa-download"></i>',
                        filename: 'Lista ODP •  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                     {
                         extend: 'print',
                         text: '<i class="fas fa-print"></i>'
                     }],
                columns: [
                    { data: "ODP", orderable: true, },
                    { data: "OC", orderable: true, },
                    { data: "Articolo", orderable: true, },
                    { data: "Descrizione", orderable: true, "width": "20%"  },
                    { data: "Azioni", searchable: true, "width": "5%"  },
                    { data: "StatoProgetto", searchable: true, "width": "20%" }
               ],
               "columnDefs": [
                   {
                       "targets": [0,3],
                       "searchable": true
                   }
            ],
               select: {
                   targets: 0,
                   data: null,
                   defaultContent: '',
                   orderable: true,
                   className: 'select-checkbox'
            },
            initComplete: function () {
                createDropdowns(this.api());
            },
                lengthMenu: [[5, 10, 15, 20, 30, 50, 75, 100, -1], [5, 10, 15, 20, 30, 50, 75, 100, "Tutti"]],
                 pageLength: 10,
                 language: {
                     "decimal": ",",
                     "emptyTable": "Nessun Dato Disponibile",
                     "info": "Visualizzazione da _START_ a _END_ ",
                     "infoEmpty": "Visualizzazione da 0 a 0 di 0 ODP",
                     "infoFiltered": "(Filtrati su _MAX_ ODP)",
                     "infoPostFix": "",
                     "thousands": ".",
                     "lengthMenu": "Mostra _MENU_",
                     "loadingRecords": "Caricamento...",
                     "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                     "search": "<i class='fas fa-search'></i>",
                     "zeroRecords": "Nessun         ",
                     "paginate": {
                         "first": "Prima",
                         "last": "Ultima",
                         "next": "Prossima",
                         "previous": "Precedente"
                     },
                     "aria": {
                         "sortAscending": ": ordina in modo ascendente A-Z",
                         "sortDescending": ": ordina in modo discendente Z-A"
                     }
                },

        });

        var TableProduzione = $('#mainDataTableProduzione').DataTable({
            stateSave: true,
            "ordering": false,
                processing: true,
                serverSide: true,
                ajax: { url: '@Url.Content("~/ProgettiProd/ServerProcessing")', type: 'POST' },
               "deferRender": true,
            dom: '<"row  align-items-center"<"col col-auto"f><"col"i><"col col-auto"B>>rt<"row align-items-center"<"col"p><"col col-auto"l>>',
                 buttons: [
                    {
                        extend: 'excel',
                        text:'<i class="fas fa-download"></i>',
                        filename: 'Lista Produzione •  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                     {
                         extend: 'print',
                         text: '<i class="fas fa-print"></i>'
                     }],
                columns: [
                    { data: null, orderable: false, class: "details-control", defaultContent: '<i class="fas fa-plus-circle"></i>' },
                    { data: "OC", orderable: false, },
                    { data: "Operatore", orderable: false,},
                    { data: "StatoProgetto", searchable: false}
               ],
               "columnDefs": [
                   {
                       "targets": [0,3],
                       "searchable": false
                   }
            ],
               select: {
                   targets: 0,
                   data: null,
                   defaultContent: '',
                   orderable: false,
                   className: 'select-checkbox'
            },
            initComplete: function () {
                createDropdowns(this.api());
            },
                lengthMenu: [[5, 10, 15, 20, 30, 50, 75, 100, -1], [5, 10, 15, 20, 30, 50, 75, 100, "Tutti"]],
                 pageLength: 10,
                 language: {
                     "decimal": ",",
                     "emptyTable": "Nessun Dato Disponibile",
                     "info": "Visualizzazione da _START_ a _END_ ",
                     "infoEmpty": "Visualizzazione da 0 a 0 di 0 Progetti",
                     "infoFiltered": "(Filtrati su _MAX_ Progetti Totali)",
                     "infoPostFix": "",
                     "thousands": ".",
                     "lengthMenu": "Mostra _MENU_",
                     "loadingRecords": "Caricamento...",
                     "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                     "search": "<i class='fas fa-search'></i>",
                     "zeroRecords": "Nessun Progetto",
                     "paginate": {
                         "first": "Prima",
                         "last": "Ultima",
                         "next": "Prossima",
                         "previous": "Precedente"
                     },
                     "aria": {
                         "sortAscending": ": ordina in modo ascendente A-Z",
                         "sortDescending": ": ordina in modo discendente Z-A"
                     }
                },

        });
            var TableProduzioneCompletati = $('#mainDataTableProduzioneCompletati').DataTable({
            stateSave: true,
            "ordering": false,
                processing: true,
                serverSide: true,
                ajax: { url: '@Url.Content("~/ProgettiProd/ServerProcessingCompletati")', type: 'POST' },
               "deferRender": true,
            dom: '<"row  align-items-center"<"col col-auto"f><"col"i><"col col-auto"B>>rt<"row align-items-center"<"col"p><"col col-auto"l>>',
                 buttons: [
                    {
                        extend: 'excel',
                        text:'<i class="fas fa-download"></i>',
                        filename: 'Lista Produzione Completati•  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                     {
                         extend: 'print',
                         text: '<i class="fas fa-print"></i>'
                     }],
                columns: [
                    { data: null, orderable: false, class: "details-control", defaultContent: '<i class="fas fa-plus-circle"></i>' },
                    { data: "OC", orderable: false, },
                    { data: "Operatore", orderable: false,},
                    { data: "StatoProgetto", searchable: false}
               ],
               "columnDefs": [
                   {
                       "targets": [0,3],
                       "searchable": false
                   }
            ],
               select: {
                   targets: 0,
                   data: null,
                   defaultContent: '',
                   orderable: false,
                   className: 'select-checkbox'
            },
            initComplete: function () {
                createDropdowns(this.api());
            },
                lengthMenu: [[5, 10, 15, 20, 30, 50, 75, 100, -1], [5, 10, 15, 20, 30, 50, 75, 100, "Tutti"]],
                 pageLength: 10,
                 language: {
                     "decimal": ",",
                     "emptyTable": "Nessun Dato Disponibile",
                     "info": "Visualizzazione da _START_ a _END_ ",
                     "infoEmpty": "Visualizzazione da 0 a 0 di 0 Progetti",
                     "infoFiltered": "(Filtrati su _MAX_ Progetti Totali)",
                     "infoPostFix": "",
                     "thousands": ".",
                     "lengthMenu": "Mostra _MENU_",
                     "loadingRecords": "Caricamento...",
                     "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                     "search": "<i class='fas fa-search'></i>",
                     "zeroRecords": "Nessun Progetto",
                     "paginate": {
                         "first": "Prima",
                         "last": "Ultima",
                         "next": "Prossima",
                         "previous": "Precedente"
                     },
                     "aria": {
                         "sortAscending": ": ordina in modo ascendente A-Z",
                         "sortDescending": ": ordina in modo discendente Z-A"
                     }
                },

        });
            if (window.location.href.indexOf("/AccettazioneUC/Index") != -1)
            {
            var interval = setInterval(reloadTable, 10000);
            }
            if (window.location.href.indexOf("Accettazione") != -1) {
                var interval = setInterval(reloadTable, 10000);
            }
            if (window.location.href.indexOf("Accettazione") != -1) {
                var interval = setInterval(reloadTable, 10000);
            }
            var intervalProgetti = setInterval(reloadTableProgetti, 10000);
            var intervalProduzione = setInterval(reloadTableProduzione, 10000);
            var countRecordsProg = 0;
            var countRecordsProgEstInAtt = 0;
            var countRecordsProgEstCompl = 0;
            var countRecordsProd = 0;
            var countRecordsComm = 0;
            function createDropdowns(api) {
            api.columns().every(function () {
                if (this.searchable()) {
                    var that = this;
                    var col = this.index();

                    // Only create if not there or blank
                    var selected = $('thead tr:eq(1) td:eq(' + col + ') select').val();
                    if (selected === undefined || selected === '') {
                        // Create the `select` element
                        $('thead tr:eq(1) td')
                            .eq(col)
                            .empty();
                        var select = $('<select class="form-select"><option value=""></option><option value="In Attesa">In Attesa</option><option value="Inviato a PROD">Inviato a PROD</option><option value="Inviato a UT">Inviato a UT</option>')
                            .appendTo($('thead tr:eq(1) td').eq(col))
                            .on('change', function () {
                                console.log($(this).val());
                                that.search($(this).val()).draw();
                                createDropdowns(api);
                            });
                    }
                }
            });
        }
            function reloadTable() {
                if (!isChildOpen) {
                    //countRecords = $('#mainDataTableAccettazione').DataTable().data().count();
                    $('#mainDataTableAccettazione').DataTable().ajax.reload(function (json) {
                        console.log(json);
                        if (countRecordsComm < json.recordsTotalUC) {
                            countRecordsComm = json.recordsTotalUC;
                            var audio = new Audio('/Content/mp3/notification_operatore.mp3');
                            audio.play();
                        }
                    });

                }
            }
            function reloadTableProgetti() {
                if (!isChildOpen) {
                    //countRecords = $('#mainDataTableProgetti').DataTable().data().count();
                    $('#mainDataTableProgetti').DataTable().ajax.reload(function (json) {
                        console.log(json);
                        if (countRecordsProg < json.recordsTotalUT && window.location.href.indexOf("ProgettiUT") != -1) {
                            countRecordsProg = json.recordsTotalUT;
                            var audio = new Audio('/Content/mp3/notification_admin_UT.mp3');
                            audio.play();
                        }
                    });
                    $('#mainDataTableProgettiEsterniCompletati').DataTable().ajax.reload(function (json) {
                        if (countRecordsProg < json.recordsTotalEst && window.location.href.indexOf("ProgettiUT") != -1) {
                            countRecordsProg = json.recordsTotalEst;
                            var audio = new Audio('/Content/mp3/notification_admin_UT.mp3');
                            audio.play();
                        }
                    });
                    $('#mainDataTableProgettiEsterniInAttesa').DataTable().ajax.reload(function (json) {
                        if (countRecordsProg < json.recordsTotalEst && window.location.href.indexOf("ProgettiUT") != -1) {
                            countRecordsProg = json.recordsTotalEst;
                            var audio = new Audio('/Content/mp3/notification_admin_UT.mp3');
                            audio.play();
                        }
                    });
                }
            }
            function reloadTableProduzione() {
                if (!isChildOpen) {
                    //countRecords = $('#mainDataTableProduzione').DataTable().data().count();
                    $('#mainDataTableProduzione').DataTable().ajax.reload(function (json) {
                        console.log(json);
                        if (countRecordsProd < json.recordsTotalPROD && window.location.href.indexOf("ProgettiProd") != -1) {
                            countRecordsProd = json.recordsTotalPROD;
                            var audio = new Audio('/Content/mp3/notification_operatore.mp3');
                            audio.play();
                        }
                    });

                }
            }
            function format(d) {
                var div = $('<div/>')
                    .addClass('loading')
                    .text('Caricamento...');
                $.ajax({
                    url: '@Url.Action("Par", "Pezzi")',
                data: {
                                        Id: d[1]
                                    },
                                    'type': 'GET',
                                    dataType: 'json',
                success: function (json) {
                                        console.log(json);

                                        div
                                                                .html('<div class="jumbotron" style="max-width:100%;"> <div class=""> <p>Storico media costi</p> <table class="table-striped" style=" width: 100%;border-radius: 15px;  border-collapse: collapse;  border-radius: 1em;box-shadow: rgba(50, 50, 93, 0.25) 0px 6px 12px -2px, rgba(0, 0, 0, 0.3) 0px 3px 7px -3px; overflow: hidden;"> <thead> <tr> <td> Anno </td> <td> Costo Globale </td> <td> Costi materiali </td> <td> Costi Manodopera </td> <td> Costi Conto Lavoro </td> </tr> </thead> <tbody> <tr> <td>' + json.dataDueAnniFa.Year + '</td> <td>' + json.dataDueAnniFa.Costo_Globale + '</td> <td>' + json.dataDueAnniFa.Costo_Materiali + '</td> <td>' + json.dataDueAnniFa.Manodopera + '</td> <td>' + json.dataDueAnniFa.Costo_Manodopera_Est + '</td> </tr> <tr> <td>' + json.dataAnnoScorso.Year + '</td> <td>' + json.dataAnnoScorso.Costo_Globale + '</td> <td>' + json.dataAnnoScorso.Costo_Materiali + '</td> <td>' + json.dataAnnoScorso.Manodopera + '</td> <td>' + json.dataAnnoScorso.Costo_Manodopera_Est + '</td> </tr> <tr> <td>' + json.dataOggi.Year + '</td> <td>' + json.dataOggi.Costo_Globale + '</td> <td>' + json.dataOggi.Costo_Materiali + '</td> <td>' + json.dataOggi.Manodopera + '</td> <td>' + json.dataOggi.Costo_Manodopera_Est + '</td> </tr> </tbody> </table> </div> </div>')
                                                                .removeClass('loading');
                                        const ctx = document.getElementById('Chart_Costi_'+json.id+'').getContext('2d');
                                        const myChart = new Chart(ctx, {
                                            type: 'bar',
                data: {
                                                labels: json.Labels,
                                                datasets:  [{
                                                    label: 'Costo Globale',
                data: json.DataTot,
                backgroundColor:  [
                                                        'rgba(255, 99, 132, 1)'
                                                    ],
                                                    borderWidth: 1
                                                },
                                                    {
                                                        label: 'Costo Materiale',
                data: json.DataMat,
                backgroundColor:  [
                                                            'rgba(14, 99, 132, 1)'
                                                        ],
                                                        borderWidth: 1
                                                    },
                                                    {
                                                        label: 'Costo Macchina',
                data: json.DataMacchina,
                backgroundColor:  [
                                                            'rgba(28, 99, 132, 1)'
                                                        ],
                                                        borderWidth: 1
                                                    },
                                                    {
                                                        label: 'Costo Manodopera Esterna',
                data: json.DataManoEst,
                backgroundColor:  [
                                                            'rgba(56, 99, 132, 1)'
                                                        ],
                                                        borderWidth: 1
                                                    },
                                                    {
                                                        label: 'Costo Attrezzaggio',
                data: json.DataAtt,
                backgroundColor:  [
                                                            'rgba(120, 99, 132, 1)'
                                                        ],
                                                        borderWidth: 1
                                                    },
                                                    {
                                                        label: 'Costo Manodopera Interna',
                data: json.DataManoInt,
                backgroundColor:  [
                                                            'rgba(14, 33, 132, 1)'
                                                        ],
                                                        borderWidth: 1
                                                    }]
                                            },
                                            options: {
                                                bezierCurve: true,
                                                scales: {
                                                    y: {
                                                        beginAtZero: true
                                                    }
                                                }
                                            }
                                        });
                                    }
                                });

                                return div;
                            }
            var table = $('#mainDataTable').DataTable({
                stateSave: true,
                order: [[3, 'desc']],
               "deferRender": true,
                dom: '<"row  align-items-center"<"col col-auto"f><"col"i><"col col-auto"B>>rt<"row align-items-center"<"col"p><"col col-auto"l>>',
                language: {
                    "decimal": ",",
                    "emptyTable": "Nessun Dato Disponibile",
                    "info": "Visualizzazione da _START_ a _END_ di _TOTAL_ disegni",
                    "infoEmpty": "Visualizzazione da 0 a 0 di 0 disegni",
                    "infoFiltered": "(Filtrati su _MAX_ Ticket Totali)",
                    "infoPostFix": "",
                    "thousands": ".",
                    "lengthMenu": "Mostra _MENU_",
                    "loadingRecords": "Caricamento...",
                    "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
                    "search": "<i class='fas fa-search'></i>",
                    "zeroRecords": "Nessun Disegno",
                    "paginate": {
                        "first": "Prima",
                        "last": "Ultima",
                        "next": "Prossima",
                        "previous": "Precedente"
                    },
                    "aria": {
                        "sortAscending": ": ordina in modo ascendente A-Z",
                        "sortDescending": ": ordina in modo discendente Z-A"
                    }
                },
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'excel',
                        text:'Excel',
                        filename: 'Lista Ticket •  @DateTime.Now • EuromaGroup',
                        sheetName: '@DateTime.Now'
                     },
                             {extend: 'pdfHtml5',
                orientation: 'landscape',
                pageSize: 'LEGAL',
                customize: function (doc) {
                                            doc.content[1].table.widths =
                                                Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                                              doc.content[1].margin = [ 100, 0, 100, 0 ] //left, top, right, bottom

                                        }
                                    },
                    {
                        extend: 'print',
                        text: 'Stampa',
                        orientation: 'landscape'
                     }
                ],
                "initComplete": function (settings, json) {
                    $('.loading').remove();
                },
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],

                                "initComplete": function (settings, json) {
                                    $('#loader').remove();
                                    $('.main-table').show();
                                    $('body').css('overflow', '');
                                    $(this).show()
                                }
            });
            $("input[type='text']").last().keyup(function () {
                $(".nomeArticolo").text($("input[type='text']").last().val());
            });

            Dropzone.options.importupload = {
                url: '/AccettazioneUC/Create',
                init: function () {
                    this.on("sending", function (data, xhr, formData) {
                        formData.append("checkCosto", $("#checkCosto").is(":checked"));
                        formData.append("ListOT", $("#ListOT").val());
                        formData.append("__RequestVerificationToken", jQuery("[name='__RequestVerificationToken'").val());
                        console.log(formData);
                    });
                    this.on('success', function (file, result) {
                        if (result.ok) {
                            $.notify({ icon: ' fa-check-circle-o', message: result.message }, { type: 'success' });
                            window.location.reload();
                        } else {
                            $.notify({ icon: 'fas fa-exclamation-triangle', message: result.message }, { Type: 'danger' });
                        }
                    });
                    this.on("error", function (file, result) {
                        $.notify({ icon: 'fas fa-exclamation-triangle', message: result.message }, { Type: 'danger' });
                    });

                },
                maxFilesize: 100,
                acceptedFiles: ".pdf",
                dictDefaultMessage: "<strong>Trascinare i file pdf o cliccare per sfogliare</strong><br/>sono ammessi solo .pdf fino a 100 MB",
                dictFallbackMessage: "Il browser non supporta il trascinamento di files, usare: chrome, ie, opera, safari, firefox o edge",
                dictFileTooBig: "File Troppo Grande ({{filesize}}). Il limite è fissato a {{maxFilesize}}.",
                dictInvalidFileType: "Il tipo di file non è fra quelli permessi. sono ammessi solo file PDF",
                dictResponseError: "Server non disponibile. codice errore {{statusCode}}"
            };
            Dropzone.options.attachmentsUpload = {
                url: '/AccettazioneUC/CreaEmail',
                addRemoveLinks: true,
                autoDiscover: false,
                autoProcessQueue: false,
                maxFilesize: 100,
                dictDefaultMessage: "<strong>Trascinare i file o cliccare per sfogliare</strong><br/>sono ammessi solo file fino a 100 MB",
                dictFallbackMessage: "Il browser non supporta il trascinamento di files, usare: chrome, ie, opera, safari, firefox o edge",
                dictFileTooBig: "File Troppo Grande ({{filesize}}). Il limite è fissato a {{maxFilesize}}.",
                dictInvalidFileType: "Il tipo di file non è fra quelli permessi.",
                dictResponseError: "Server non disponibile. codice errore {{statusCode}}",
                init: function () {
                    dzClosure = this; // Makes sure that 'this' is understood inside the functions below.
                    $("#Send").click(function () {
                        e.preventDefault();
                        e.stopPropagation();
                        dzClosure.processQueue();
                    });

                    //send all the form data along with the files:
                    this.on("sendingmultiple", function (data, xhr, formData) {
                        formData.append("firstname", $("#firstname").val());
                        formData.append("lastname", $("#lastname").val());
                    });
                }
            };
            Dropzone.options.newFileUpload = {
                url: '/AccettazioneUC/Edit',
                acceptedFiles: ".pdf",
                addRemoveLinks: true,
                autoDiscover: false,
                autoProcessQueue: false,
                maxFilesize: 100,
                maxFiles: 1,
                maxfilesexceeded: function (file) {
                    this.removeAllFiles();
                    this.addFile(file);
                },
                dictDefaultMessage: "<strong>Trascinare la revisione del file qui all'interno.</strong><br/>",
                dictFallbackMessage: "Il browser non supporta il trascinamento di files, usare: chrome, ie, opera, safari, firefox o edge",
                dictFileTooBig: "File Troppo Grande ({{filesize}}). Il limite è fissato a {{maxFilesize}}.",
                dictInvalidFileType: "Il tipo di file non è fra quelli permessi.",
                dictResponseError: "Server non disponibile. codice errore {{statusCode}}",
                init: function () {
                    dzClosure = this; // Makes sure that 'this' is understood inside the functions below.
                    $("#Send").click(function () {
                        e.preventDefault();
                        e.stopPropagation();
                        dzClosure.processQueue();
                    });

                    //send all the form data along with the files:
                    this.on("sendingmultiple", function (data, xhr, formData) {
                        formData.append("firstname", $("#firstname").val());
                        formData.append("lastname", $("#lastname").val());
                    });
                }
            };
            Dropzone.options.ProgettoFileUpload = {
                url: '/ProgettiUT/EditOperatore',
                acceptedFiles: ".pdf",
                addRemoveLinks: true,
                autoDiscover: false,
                autoProcessQueue: false,
                maxFilesize: 30,
                maxFiles: 5,
                maxfilesexceeded: function (file) {
                    this.removeAllFiles();
                    this.addFile(file);
                },
                dictDefaultMessage: "<strong>Trascinare i documenti all'interno, oppure cliccare per caricarli.</strong><br/>",
                dictFallbackMessage: "Il browser non supporta il trascinamento di files, usare: chrome, ie, opera, safari, firefox o edge",
                dictFileTooBig: "File Troppo Grande ({{filesize}}). Il limite è fissato a {{maxFilesize}} MB.",
                dictInvalidFileType: "Il tipo di file non è fra quelli permessi.",
                dictResponseError: "Server non disponibile. codice errore {{statusCode}}",
                init: function () {
                    dzClosure = this; // Makes sure that 'this' is understood inside the functions below.
                    $("#Send").click(function () {
                        e.preventDefault();
                        e.stopPropagation();
                        dzClosure.processQueue();
                    });

                    //send all the form data along with the files:
                    this.on("sendingmultiple", function (data, xhr, formData) {
                        formData.append("firstname", $("#firstname").val());
                        formData.append("lastname", $("#lastname").val());
                    });
                }
            };


            $('#mainDataTable tbody').on('click', 'td.dt-control', function () {
                var tr = $(this).closest('tr');
                var row = table.row(tr);

                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    tr.removeClass('shown');
                }
                else {
                    row.child(format(row.data())).show();
                    tr.addClass('shown');
                    // Open this row
                }
            });
        });
    $('#btn_Accettazione_Guide').guides({
            guides: [{
                element: $('.navbar'),
                html: 'Benvenuto su EuromaWeb!'
            }, {
                element: $('.buttons-html5'),
                html: 'Clicca sui bottoni per scaricare il contenuto della tabella nel formato che più ti piace.'
            }, {
                element: $('#Add_Acc'),
                html: 'Clicca su questo bottone per importare un nuovo file da far confermare al caporeparto!'
                }, {
                    element: $('.btn-primary'),
                    html: 'Clicca sui bottoni azzuri nella lista per performare azioni.'
                }]
    });
    $('#btn_lavorazioni_esterne').guides({
            guides: [{
                element: $('.navbar'),
                html: 'Sezione lavorazioni esterne'
            }, {
                element: $('#Num'),
                html: 'Inserisci qui il numero della OL.'
            }, {
                element: $('#FlagMailAuto'),
                html: 'Spunta questo flag per lasciare inserire al sistema la mail adatta.'
                }, {
                element: $('#email'),
                    html: 'Altrimenti inseriscila manualmente.'
                 }, {
                element: $('#FlagBoth'),
                     html: 'Vuoi inviare anche il documento di trasporto? Flagga questa spunta.'
                 }, {
                element: $('#mainDataTable'),
                     html: 'Per ultimo: guarda tutte gli ordini fornitore inviati all\'esterno: da chi e quando. Puoi anche scaricare l\'ol o il ddt in uno secondo momento con le tue icone blu.'
                 }]
        });
    $('body').on('click', '#Search', function (e) {
        $(".loader").show();
        var start = $("#StartDate")[0].value;
        var end = $("#EndDate")[0].value;
        console.log(endpoint + start + "-" + end);
        window.open(endpoint + start + "-" + end, '_blank').focus();
        $(".loader").hide();

    });
        function turnoff(id) {
            $.ajax({
                method: "GET",
                url: "/Manage/TurnOffPC/"+id,
                success: function (data) {
                    $.notify({ message: data.message }, { type: 'success' });
                },
                error: function (error_data) {
                    $.notify({ message: error_data.message }, { type: 'error' });
                }
            });
        }
        function turnon(id) {
            $.ajax({
                method: "GET",
                url: "/Manage/TurnOnPC/" + id,
                success: function (data) {
                    $.notify({ message: data.message }, { type: 'success' });
                },
                error: function (error_data) {
                    $.notify({ message: error_data.message }, { type: 'error' });
                }
            });
        }
    $('body').on('click', '#SearchComplessivoOrdinato', function (e) {
        $(".loader").show();
        var start = $("#StartDate")[0].value;
        var end = $("#EndDate")[0].value;
        $.ajax({
            method: "GET",
            url: endpointComplessivoOrdinato + start + "-" + end,
            success: function (data) {
                console.log(data);
                var kmkLabel = data.lista.map(c =>  c.DataInizio + "-" + c.DataFine);
                var kmkData = data.lista.map(c => c.Totale);
                $("#ChartComplessivo").remove();
                $('#ComplessivoChartContainer').append('<canvas id="ChartComplessivo"></canvas>');
                window.ChartComplessivo = new Chart(document.getElementById("ChartComplessivo"), {
                    type: 'bar',
                    data: {
                        labels: kmkLabel,
                        datasets: [
                            {
                                label: "# Euro di fatturato",
                                backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                                data: kmkData
                            }
                        ]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: 'Predicted world population (millions) in 2050'
                        },
                    }
                });
                //data.lista.forEach(function (x) {
                //    $("#PSettimana").append("<li> Settimana: " + x.nSettimana + "(" + x.DataInizio + ")-((" + x.DataFine +")</li>");
                //    $("#PTotale").append("<li> Totale: " + x.Totale + "</li>");
                //    $("#PConteggio").append("<li> Conteggio Ordini: " + x.Conteggio + "</li>");
                //})

            },
            error: function (error_data) {
                console.log(error_data);
            }
        });
        $(".loader").hide();

    });
    $('body').on('click', '#SearchOrdinato', function (e) {
            $(".loader").show();
            var start = $("#StartDate")[0].value;
            var end = $("#EndDate")[0].value;
            var agente = $('#agenti :selected').val();
            var cliente = $('#clienti :selected').val();
            window.open(endpointOrdinato + start + "-" + end + '&agente=' + agente + '&cliente=' + cliente, '_blank').focus();
            $(".loader").hide();

    });
        $('body').on('click', '#CalcolaGleason', function (e) {
            $(".loader").show();
            var Angolo = $("#Angolo")[0].value;
            var Modulo = $("#Modulo")[0].value;
            var DentiPign = $("#DentiPign")[0].value;
            var DentiCor = $("#DentiCor")[0].value;
            var Senso = $("#Senso")[0].value;
            var PosNeg = $("#PosNeg")[0].value;
            console.log(PosNeg)
            var link = endpointGleason + "AQ=" + Angolo + "&MQ=" + Modulo + "&Z1Q=" + DentiPign + "&Z2Q=" + DentiCor + "&S=" + Senso + "&Pos=" +PosNeg
            console.log(link);
            $.ajax({
                method: "GET",
                url: link,
                success: function (data) {
                    $.notify({ message: data.message }, { type: 'success' });
                    console.log(data)
                    $("#RisultatoTablePignone").empty();
                    $("#RisultatoTableCorona").empty();
                    $("#RisultatoTablePignone").append("<h2>Tabella Pignone</h2>")
                    $("#RisultatoTablePignone").append(data.tablePignone)
                    $("#RisultatoTableCorona").append("<h2>Tabella Corona</h2>")
                    $("#RisultatoTableCorona").append(data.tableCoron)
                },
                error: function (error_data) {
                    $.notify({ message: error_data.message }, { type: 'error' });
                }
            });
            $(".loader").hide();
        });
    $('body').on('click', '#SearchPriorita', function (e) {
        $(".loader").show();
        var start = $("#StartDate")[0].value;
        var end = $("#EndDate")[0].value;
        console.log(endpointPriorita + start + "-" + end);
        window.open(endpointPriorita + start + "-" + end, '_blank').focus();
        $(".loader").hide();

    });
    $('body').on('click', '#SearchCommessa', function (e) {
        $(".loader").show();
        var Anno = $("#Anno")[0].value;
        var OP = $("#OP")[0].value;
        var Num = $("#Num")[0].value;
        console.log(endpointCommessa + Anno + "-" + OP + "-" + Num);
        $.ajax({
            method: "GET",
            url: endpointCommessa + Anno + "-" + OP + "-" + Num,
            success: function (data) {
                setTimeline(data);
            },
            error: function (error_data) {
                console.log("Endpoint GET request error");
            }
        });
    });
        $('body').on('click', '#sendOLtoFornitore', function (e) {
            var btn = this;
            var dv = $(btn).data("value");
            $(btn).hide();
            $("#loading_" + dv).css('display', 'flex')
            $.ajax({
                method: "GET",
                url: "/Acquisti/LavorazioniEsterneDirect?id="+dv,
                success: function (data) {
                    $.notify({ message: data.message }, { type: 'success' });
                    $("#loading_" + dv).hide();
                },
                error: function (error_data) {
                    $.notify({ message: error_data.message }, { type: 'error' });
                }
            });
        });
        $('body').on('click', '#sendOLtoInterno', function (e) {
            var btn = this;
            var dv = $(btn).data("value");
            $(btn).hide();
            $("#loading_" + dv).css('display', 'flex')
            $.ajax({
                method: "GET",
                url: "/Acquisti/LavorazioniEsterneTest?id=" + dv,
                success: function (data) {
                    $.notify({ message: data.message }, { type: 'success' });
                    $("#loading_" + dv).hide();
                },
                error: function (error_data) {
                    $.notify({ message: error_data.message }, { type: 'error' });
                }
            });
        });

    $("#fold").click(function () {
        $("#fold_p").fadeOut(function () {
            $("#fold_p").text(($("#fold_p").text() == 'V. @Costanti.WebAppVersion') ? 'WebAppVersion: @Costanti.WebAppVersion • Build Date @Costanti.WebAppVersionDateBuild • Release Date @Costanti.WebAppVersionDateRelease' : '@Costanti.WebAppVersion.ToString').fadeIn();
        })
    })
    function FileDownload(txt) {
        var f = btoa(txt.replace("\\", "/").replace(" / ", "_"));
        window.location.href = "/Home/FileSearcher/" + f;
        }
    function Infos() {
            Swal.fire({
                title: 'Informazioni di sistema',
                html: 'Globalization.CultureInfo: @System.Globalization.CultureInfo.CurrentUICulture <br> WebAppVersion: @Costanti.WebAppVersion • @Costanti.WebAppVersionDateRelease <br> @DateTime.Now.Year • &copy; EuromaGroup srl ',
                imageUrl: '../Asset/img/logo-nero.png',
                imageWidth: 400,
                imageHeight: 200,
                imageAlt: 'Custom image'
            }
            )
        }
    function downloadFunc(a) {
        var anchor = document.createElement('a');
        anchor.setAttribute('href', a);
        anchor.setAttribute('download', '');
        document.body.appendChild(anchor);
        anchor.click();
        anchor.parentNode.removeChild(anchor);
    }
    function Codifica() {
            document.getElementById('summernote').value = document.getElementById('summernote').value.replace(/</g, '&lt;').replace(/>/g, '&gt;');
        }
    function DownloadFirmaFile(id) {
            $.ajax({
                url: "/AccettazioneUC/ScaricaFile/"+id,
                success: function (data) {
                    var d = id;
                    var link = '@Url.Action("ScaricaFile", "AccettazioneUC", New With {.id = "-1"})';
                    link = link.replace("-1", d);
                    window.location = link;
                    $.notify({ icon: 'fa-solid fa-check', message: "Operazione correttamente eseguita!" }, { type: 'success' });
                    //$.ajax({
                    //    url: "/AccettazioneUC/CreaEmail/" + id,
                    //    success: function (data) {

                    //    }
                    //});
                }
            });
        };
    function DownloadFirmaFileRaw(id) {
            console.log(id);
            $.ajax({
                url: "/AccettazioneUC/ScaricaFileRaw/"+id,
                success: function (data) {
                    var d = id;
                    var link = '@Url.Action("ScaricaFileRaw", "AccettazioneUC", New With {.id = "-1"})';
                    link = link.replace("-1", d);
                    window.location = link;
                    $.notify({ icon: ' fa-solid fa-check', message: "Operazione correttamente eseguita!" }, { type: 'success' });
                    //$.ajax({
                    //    url: "/AccettazioneUC/CreaEmail/" + id,
                    //    success: function (data) {

                    //    }
                    //});
                }
            });
        };
    function DownloadEMail(id) {
                    $.ajax({
                        url: "/AccettazioneUC/ScaricaFile/"+id,
                        success: function (data) {
                            var d = id;
                            var link = '@Url.Action("ScaricaFile", "AccettazioneUC", New With {.id = "-1"})';
                            link = link.replace("-1", d);
                            window.location = link;
                            $.ajax({
                                url: "/AccettazioneUC/CreaEmail/" + id,
                                success: function (result) {
                                    if (result.ok) {
                                        $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                                        window.location.reload();
                                    }
                                    else {
                                        console.log(result);
                                        //$.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                    }
                                },
                                error: function (result) {
                                    console.log(result);
                                    $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                }
                            });
                        }
                    });
    };

    async function AddNota(id) {
        const { value: text } = await Swal.fire({
            input: 'textarea',
            inputLabel: 'Nota per OC (ID: '+id+")",
            inputPlaceholder: 'Inserisci qui la nota aggiuntiva...',
            inputAttributes: {
                'aria-label': 'Inserisci qui la nota aggiuntiva'
            },
            showCancelButton: true,
            confirmButtonText: 'Aggiungi',
            cancelButtonText: 'Cancella',
        })

        if (text) {
            $.ajax({
                url: '/AccettazioneUC/AddNota',
                type: 'POST',
                data: { Id: id, Nota: text },
                dataType: 'json',
                success: function (result) {
                    if (result.ok) {
                        $.notify({ message: result.message }, { type: 'success' });

                    }
                    else {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                },
                error: function (result) {
                    console.log(result);
                    $.notify({ message: result.message }, { type: 'danger' });
                }
            });
        }
        }
        async function AddFile(id) {
            const { value: file } = await Swal.fire({
                title: 'Seleziona File',
                input: 'file',
                inputAttributes: {
                    'accept': '*',
                    'aria-label': 'Carica File per Documento'
                }
            })

            if (file) {
                var myformData = new FormData();
                myformData.append('id', id);
                myformData.append('file', file);
                $.ajax({
                    url: '/AccettazioneUC/AddFile',
                    type: 'POST',
                    processData: false,
                    contentType: false,
                    cache: false,
                    data: myformData,
                    enctype: 'multipart/form-data',
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ message: result.message }, { type: 'success' });
                        }
                        else {
                            console.log(result);
                            $.notify({ message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                });

            }
        }
    function SendToProd(id) {
        Swal.fire({
            title: 'Conferma',
            text: "Sei sicuro di voler inviare questo documento?",
            icon: 'question',
            showCancelButton: true,
            showDenyButton: true,
            denyButtonColor: '#3085d6',
            denyButtonText: 'Invia a Produzione!',
            cancelButtonText: 'Cancella',
            confirmButtonColor: '#26a360',
            cancelButtonColor: '#d33',
        }).then((result) => {
            if (result.isDenied) {
                SendToProd(id);
            }
        })
    }
    function editPosizioneArticolo(id, idArt) {
        $.ajax({
            url: "/Overviews/EditArticoli/" + id,
            success: function (result) {
                if (result.ok) {
                    console.log(result);
                    const mappy = result.slot.map(({ Value, Text }) => ({[Value]:Text}))
                    console.log(mappy);
                    Swal.fire({
                        title: "Sposta articolo",
                        input: 'select',
                        inputOptions: mappy,

                    }).then((result) => {
                        console.log(result);
                        if (result.isConfirmed) {
                            $.ajax({
                                url: "/Overviews/POSTEdit/",
                                data: {
                                    id: idArt,
                                    nuovaPos: result.value
                                },
                                success: function (result) {
                                    if (result.ok) {
                                        $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                                        window.location.reload();
                                    }
                                    else {
                                        console.log(result);
                                        $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                    }
                                },
                                error: function (result) {
                                    console.log(result);
                                    $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                }
                            });
                        }
                        if (result.isDenied) {
                        }
                    })
                }
                else {
                    $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                }
            },
            error: function (result) {
                console.log(result);
                $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
            }
        });
    }
    function visualizzaArticolo(id) {
        $.ajax({
            url: "/Overviews/VisualizzaArticolo/" + id,
            success: function (result) {
                if (result.ok) {
                    Swal.fire({
                        title: result.codart + " - Q.tà: " + result.qtaart,
                        text: result.descart
                    })
                }
                else {
                    $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                }
            },
            error: function (result) {
                console.log(result);
                $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
            }
        });
        }
        
        async function SearchArticoloTablet() {
            const { value: text } = await Swal.fire({
                input: 'text',
                inputLabel: 'Ricerca Articolo',
                inputPlaceholder: 'Inserisci qui il codice...',
                inputAttributes: {
                    'aria-label': 'Inserisci qui il codice'
                },
                showCancelButton: true,
                confirmButtonText: 'Cerca',
                cancelButtonText: 'Cancella'
            })

            if (text) {
                var id = 0;
                $.ajax({
                    url: '/Overviews/RicercaArticolo',
                    type: 'POST',
                    data: { stringa: text },
                    dataType: 'json',
                    success: function (result) {
                        if (result.ok) {
                            id = result.id;
                            console.log(result);
                            Swal.fire({
                                title: 'Articolo ' + result.codart,
                                text: "L'articolo si trova nello scaffale " + result.scaffale + " e nello slot lettera " + result.slot + ".",
                                icon: 'question',
                                showCancelButton: false,
                                showDenyButton: false,
                                confirmButtonText: 'Ok!',
                                confirmButtonColor: '#26a360'
                            })
                        }
                        else {
                            console.log(result);
                            $.notify({ message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                });
            }
        }
           async function SearchArticolo() {
        const { value: text } = await Swal.fire({
            input: 'text',
            inputLabel: 'Ricerca Articolo',
            inputPlaceholder: 'Inserisci qui il codice...',
            inputAttributes: {
                'aria-label': 'Inserisci qui il codice'
            },
            showCancelButton: true,
            confirmButtonText: 'Cerca',
            cancelButtonText: 'Cancella',
        })

        if (text) {
            var id = 0;
            $.ajax({
                url: '/Overviews/RicercaArticolo',
                type: 'POST',
                data: { stringa: text },
                dataType: 'json',
                success: function (result) {
                    if (result.ok) {
                        id = result.id;
                        console.log(result);
                        Swal.fire({
                            title: 'Articolo '+result.codart,
                            text: "L'articolo si trova nello scaffale " + result.scaffale + " e nello slot lettera "+ result.slot + ". Vuoi scaricare interamente questo articolo?",
                            icon: 'question',
                            showCancelButton: true,
                            showDenyButton: true,
                            confirmButtonText: 'Si!',
                            denyButtonText: 'Scarica parzialmente',
                            cancelButtonText: 'No',
                            confirmButtonColor: '#26a360',
                            cancelButtonColor: '#d33'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                console.log(result);
                                $.ajax({
                                    url: "/Overviews/DeleteArticolo/" + id,
                                    success: function (result) {
                                        if (result.ok) {
                                            $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                                            window.location.reload();
                                        }
                                        else {
                                            console.log(result);
                                            $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                        }
                                    },
                                    error: function (result) {
                                        console.log(result);
                                        $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                    }
                                });
                            } else if (result.isDenied) {
                                Swal.fire({
                                    title: "Scarico parziale",
                                    text: "Inserisci la quantità che vuoi scaricare:",
                                    input: 'number',
                                    showCancelButton: true
                                }).then((result) => {
                                    if (result.value) {
                                        console.log(result);
                                        $.ajax({
                                            url: "/Overviews/DeleteArticoloParziale?id=" + id + "&qta="+result.value.toString(),
                                            success: function (result) {
                                                if (result.ok) {
                                                    $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                                                    window.location.reload();
                                                }
                                                else {
                                                    console.log(result);
                                                    $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                                }
                                            },
                                            error: function (result) {
                                                console.log(result);
                                                $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                            }
                                        });
                                    }
                                });

                            }
                        })
                    }
                    else {
                        console.log(result);
                        $.notify({ message: result.message }, { type: 'danger' });
                    }
                },
                error: function (result) {
                    console.log(result);
                    $.notify({ message: result.message }, { type: 'danger' });
                }
            });
        }
    }
    function SendToUTPrompt(id) {
        Swal.fire({
            title: 'Conferma',
            text: "Sei sicuro di voler inviare questo documento?",
            icon: 'question',
            showCancelButton: true,
            showDenyButton: true,
            denyButtonColor: '#3085d6',
            denyButtonText: 'Invia a Produzione!',
            cancelButtonText: 'Cancella',
            confirmButtonColor: '#26a360',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Invia a Uff. Tec!'
        }).then((result) => {
            console.log(result);
            if (result.isConfirmed) {
                SendToUT(id);
            }
            if (result.isDenied) {
                SendToProd(id);
            }
        })
    }
    function DeleteOL(id) {
        Swal.fire({
            title: 'Conferma',
            text: "Sei sicuro di volere eliminare l'OL?",
            icon: 'question',
            showCancelButton: true,
            showDenyButton: false,
            confirmButtonText: 'Si!',
            cancelButtonText: 'No',
            confirmButtonColor: '#26a360',
            cancelButtonColor: '#d33'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Acquisti/DeleteLavorazione/" + id,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                            window.location.reload();
                        }
                        else {
                            console.log(result);
                            $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                    }
                });
            }
        })
    }
    function EndExtProg(id) {
        Swal.fire({
            title: 'Conferma',
            text: "Sei sicuro di voler chiudere l'attività?",
            icon: 'question',
            showCancelButton: true,
            showDenyButton: false,
            confirmButtonText: 'Si!',
            cancelButtonText: 'No',
            confirmButtonColor: '#26a360',
            cancelButtonColor: '#d33'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/ProgettiUT/EndLavorazione/" + id,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                            window.location.reload();
                        }
                        else {
                            console.log(result);
                            $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                    }
                });
            }
        })
    }
    function DeleteArticolo(id) {
        Swal.fire({
            title: 'Conferma',
            text: "Sei sicuro di volere eliminare l'articolo? (Inserisci la quantità da scaricare, altrimenti lascia il campo vuoto per scaricare interamente).",
            icon: 'question',
            showCancelButton: true,
            showDenyButton: false,
            confirmButtonText: 'Si!',
            cancelButtonText: 'No',
            confirmButtonColor: '#26a360',
            cancelButtonColor: '#d33'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Overviews/DeleteArticolo/" + id,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                            window.location.reload();
                        }
                        else {
                            console.log(result);
                            $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                    }
                });
            }
        })
    }
    function DeleteOP(id) {
        Swal.fire({
            title: 'Conferma',
            text: "Sei sicuro di volere eliminare l'ODP dal sistema esterno?",
            icon: 'question',
            showCancelButton: true,
            showDenyButton: false,
            confirmButtonText: 'Si!',
            cancelButtonText: 'No',
            confirmButtonColor: '#26a360',
            cancelButtonColor: '#d33'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/ProgettiUT/DeleteOrdineDiProduzioneEsterno/" + id,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                            $('#mainDataTableProgettiEsterniInAttesa').DataTable().ajax.reload();
                        }
                        else {
                            console.log(result);
                            $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                    }
                });
            }
        })
    }
    function CreateODP(id) {
        Swal.fire({
            title: 'Conferma',
            text: "Sei sicuro di voler inviare ad un esterno questo documento?",
            icon: 'question',
            showCancelButton: true,
            showDenyButton: false,
            confirmButtonText: 'Invia!',
            cancelButtonText: 'Cancella',
            confirmButtonColor: '#26a360',
            cancelButtonColor: '#d33'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/ProgettiUT/CreateODPEsterno/" + id,
                    success: function (result) {
                        if (result.ok) {
                            $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                            $('#mainDataTableProgettiEsterniInAttesa').DataTable().ajax.reload();
                        }
                        else {
                            console.log(result);
                            $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                        }
                    },
                    error: function (result) {
                        console.log(result);
                        $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                    }
                });
            }
        })
    }
        function SendToCommPrompt(id) {
            Swal.fire({
                title: 'Conferma',
                text: "Sei sicuro di voler reinviare questo documento?",
                icon: 'question',
                showCancelButton: true,
                showDenyButton: false,
                confirmButtonText: 'Invia a Commerciale!',
                cancelButtonText: 'Cancella',
                confirmButtonColor: '#26a360',
                cancelButtonColor: '#d33'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/ProgettiUT/SendToComm/" + id,
                        success: function (result) {
                            if (result.ok) {
                                $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                                window.location.reload();
                            }
                            else {
                                console.log(result);
                                $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                            }
                        },
                        error: function (result) {
                            console.log(result);
                            $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                        }
                    });
                }
            })
        }
    function SendToProdPrompt(id) {
        Swal.fire({
            title: 'Conferma',
            text: "Sei sicuro di voler inviare questo documento?",
            icon: 'question',
            showCancelButton: true,
            showDenyButton: false,
            confirmButtonText: 'Invia a Produzione!',
            cancelButtonText: 'Cancella',
            confirmButtonColor: '#26a360',
            cancelButtonColor: '#d33'
        }).then((result) => {
            if (result.isConfirmed) {
                SendToProdFromUt(id);
            }
        })
        }
        function SendDirectToProdPrompt(id) {
            Swal.fire({
                title: 'Conferma',
                text: "Sei sicuro di voler inviare questo documento direttamente alla Produzione per erroneo smistamento?",
                icon: 'question',
                showCancelButton: true,
                showDenyButton: false,
                confirmButtonText: 'Invia a Produzione!',
                cancelButtonText: 'Cancella',
                confirmButtonColor: '#26a360',
                cancelButtonColor: '#d33'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/ProgettiUT/DirectSendToProd/" + id,
                        success: function (result) {
                            if (result.ok) {
                                $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                                window.location.reload();
                            }
                            else {
                                console.log(result);
                                $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                            }
                        },
                        error: function (result) {
                            console.log(result);
                            $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                        }
                    });
                }
            })
        }
        function SendDirectToUTPrompt(id) {
            Swal.fire({
                title: 'Conferma',
                text: "Sei sicuro di voler inviare questo documento direttamente all'Ufficio Tecnico per erroneo smistamento?",
                icon: 'question',
                showCancelButton: true,
                showDenyButton: false,
                confirmButtonText: 'Invia a Tecnico!',
                cancelButtonText: 'Cancella',
                confirmButtonColor: '#26a360',
                cancelButtonColor: '#d33'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/ProgettiProd/DirectSendToUT/" + id,
                        success: function (result) {
                            if (result.ok) {
                                $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                                window.location.reload();
                            }
                            else {
                                console.log(result);
                                $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                            }
                        },
                        error: function (result) {
                            console.log(result);
                            $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                        }
                    });
                }
            })
        }
    function SendToUT(id) {
                            $.ajax({
                                url: "/AccettazioneUC/SendToUT/"+id,
                                success: function (result) {
                                    if (result.ok) {
                                        $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                                        window.location.reload();
                                    }
                                    else {
                                        console.log(result);
                                        $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                    }
                                },
                                error: function (result) {
                                    console.log(result);
                                    $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                                }
                            });
    };
    function SendToProdFromUt(id) {
        $.ajax({
            url: "/ProgettiUT/SendToProd/" + id,
            success: function (result) {
                if (result.ok) {
                    $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                    window.location.reload();
                }
                else {
                    console.log(result);
                    $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                }
            },
            error: function (result) {
                console.log(result);
                $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
            }
        });
    };
    function SendToProd(id) {
        $.ajax({
            url: "/AccettazioneUC/SendToProd/" + id,
            success: function (result) {
                if (result.ok) {
                    $.notify({ icon: ' fa-solid fa-check', message: result.message }, { type: 'success' });
                    window.location.reload();
                }
                else {
                    console.log(result);
                    $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
                }
            },
            error: function (result) {
                console.log(result);
                $.notify({ icon: ' fa-solid fa-xmark', message: result.message }, { type: 'danger' });
            }
        });
    };
    function setTimeline(data) {
        $(".loader").hide();
        $(".jtimeline-events").empty();
        $("#jtimeline-demo").show();
        data.list.forEach(function (o) {
            $(".jtimeline-events").append(
                "<li onClick = swaltime('" + o.Action.slice(0, -1) + "','" + o.Qty + "','" + o.Art.slice(0, -17) + "') class= 'jtimeline-event' data-timestamp='" + o.Timestamp + "' > " + o.Utente + "<br />" + o.DataString + " </li>");
        });
        $('#jtimeline-demo').jTimeline({
            resolution: 20000,
            minimumSpacing: 250,
            step: 500
        });
        }
    function swaltime(Action, Qty, Art) {
        Swal.fire({
            icon: 'info',
            title: 'Dettagli',
            html: '<p>Azione performata:' + Action + ' <br> Quantità:' + Qty + ' <br> Articolo interessato:' + Art +'</p>'
        });
        }
    function setChart(data) {
        $(".loader").hide();
        $("#Ricavi").html( "Ricavi totali nel periodo indicato di " + data.totRev.toString() + " euro");
        $("#Ricavi").show();
        $("#Anticipi").html( "Anticipi totali nel periodo indicato di " + data.anticipi.toString() + " euro");
        $("#Anticipi").show();
        var portfolioInstruments = ["CMT", "DrillMatic","ISA","MPA","Unistand"];
        var absolutPositionValues = [data.tot.CMT.Italia, data.tot.Drill.Italia, data.tot.ISA.Italia, data.tot.MPA.Italia, data.tot.Unistand.Italia];
        var absolutPositionValues1 = [data.tot.CMT.Estero, data.tot.Drill.Estero, data.tot.ISA.Estero, data.tot.MPA.Estero, data.tot.Unistand.Estero];
        $('canvas#barItalia').remove(); // barItalia Canvas
        $('canvas#barEstero').remove(); // barEstero Canvas
        $('.barItaliaContainer').append('<canvas id="barItalia" width="800" height="800" style="max-width: 800px; max-height: 800px; "></canvas>');
        $('.barEsteroContainer').append('<canvas id="barEstero" width="800" height="800" style="max-width: 800px; max-height: 800px; "></canvas>');
        // ------------------------------------------------------- //
        // Bar Fatturato Italia
        // ------------------------------------------------------ //
        var ctxItalia = document.getElementById("barItalia");
        var ChartItalia = new Chart(ctxItalia, {
            type: 'bar',
            data: {
                labels: ["CMT" , "Drillmatic", "ISA", "MPA", "Unistand"],
                datasets: [{
                    label: 'Italia',
                    data: absolutPositionValues,
                    backgroundColor: [
                        'rgba(236, 119, 38, 0.2)',
                        'rgba(222, 222, 222, 0.5)',
                    ],
                    borderColor: [
                        'rgba(236, 119, 38,0.2)',
                        'rgba(222, 222, 222, 0.2)',
                    ],
                    borderWidth: 1
                }]
            }
        });
        // ------------------------------------------------------- //
        // Bar Fatturato Estero
        // ------------------------------------------------------ //
        var ctxEstero = document.getElementById("barEstero");
        var ChartEstero = new Chart(ctxEstero, {
            type: 'bar',
            data: {
                labels: ["CMT", "Drillmatic", "ISA", "MPA", "Unistand"],
                datasets: [
                {
                    label: 'Estero',
                    data: absolutPositionValues1,
                    backgroundColor: [
                        'rgba(222, 13, 44, 0.2)',
                        'rgba(222, 222, 222, 0.5)',
                    ],
                    borderColor: [
                        'rgba(236, 119, 38,0.2)',
                        'rgba(222, 222, 222, 1)',
                    ],
                    borderWidth: 1
                }]
            }
        });
    }

    </script>
</body>

</html>
