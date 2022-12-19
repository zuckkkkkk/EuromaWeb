<style>
    .card {
        border: none !important;
        box-shadow: rgba(0, 0, 0, 0.1) 0px 3px 8px;
        border-radius: 14px;
        padding-bottom: 16px;
    }

    #Titolo_Macchina {
        margin-top: 32px;
    }

    #Titolo_Chart {
        margin-top: 16px !important;
        margin-left: 16px !important;
    }
</style>
<p id="UltimoAggiornamento" style="font-size:11px!important;z-index: 10000;text-align: center; position: fixed; left: 16px; width: 15vw; bottom: 0; color: white; padding: 8px; background-color: black; margin: 0 !important; border-radius: 16px 16px 0px 0px; box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px; ">
</p>
<div class="row mt-3">
    <div class="col-md-3">

    </div>
    <div class="col-md-6">
        <div class="card">
            <div class="row">
                <div class="col-md-12">
                    @Html.DropDownList("listaMacchine", Nothing, htmlAttributes:=New With {.class = "form-select form-select mb-3", .style = "width: 100%!important; max-width:none!important; border:none!important;", .id = "SelectMacchina"})
                </div>
            </div>
            <div class="row p-3">
                <div class="col-md-6">
                    <img src="@ViewBag.fotomacchina" style="max-width:100%;" />
                </div>
                <div class="col-md-6">
                    <h3 id="Titolo_Macchina"></h3>
                    <h5 id="Descrizione_Macchina"></h5>
                    <hr style="width:50%;" />
                    <h5 id="Programma_Macchina_Title" style="margin-bottom:0!important">Programma in corso</h5>
                    <h3 id="Programma_Macchina">Nessuno</h3>
                    <h5 id="Descrizione_Programma_Macchina"></h5>
                    <h5>Status: <span class="badge bg-primary" id="Status_Macchina" style="border-radius:100px;">ACTIVE</span></h5>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-3">

    </div>
</div>
<div class="VisualizzaMacchina">
    <div class="row">
        <div class="col-md-4">
            <div id="ContainerTempi"class="card m-2">
            <div class="row">
                <div class="col-md-10">
                    <h3 id="Titolo_Chart">
                        Modalità Macchina
                    </h3>
                </div>
                <div class="col-md-2" style="display: flex; align-items: center; justify-content: center; margin-top: 8px; margin-right: 0px;">
                    <div class="dropdown">
                        <button style="box-shadow:none!important;"class="btn " type="button" id="dropdownModalitaMacchina" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="fa-2x fa-solid fa-ellipsis-vertical"></i>
                        </button>
                        <ul class="dropdown-menu" aria-labelledby="dropdownModalitaMacchina">
                            <li><a class="dropdown-item" href="@Url.Action("DownloadModalitaMacchina", "Macchine", New With {.id = ViewBag.idMacchina})">Scarica dati</a></li>
                        </ul>
                    </div>
                </div>
            </div>
                <canvas id="ChartTempi"></canvas>
            </div>
        </div>
        <div class="col-md-4">
            <div id="ContainerTempiOperatore" class="card m-2">
                
                <div class="row">
                    <div class="col-md-10">
                        <h3 id="Titolo_Chart">Modalità Operatore</h3>
                    </div>
                    <div class="col-md-2" style="display: flex; align-items: center; justify-content: center; margin-top: 8px; margin-right: 0px;">
                        <div class="dropdown">
                            <button style="box-shadow:none!important;" class="btn " type="button" id="dropdownModalitaOperatore" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="fa-2x fa-solid fa-ellipsis-vertical"></i>
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="dropdownModalitaOperatore">
                                <li><a class="dropdown-item" href="@Url.Action("DownloadModalitaOperatore", "Macchine", New With {.id = ViewBag.idMacchina})">Scarica dati</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <canvas id="ChartTempiOperatore"></canvas>
            </div>
        </div>
        <div class="col-md-4">
            <div id="ContainerStato" class="card m-2">
                <div class="row">
                    <div class="col-md-10">
                        <h3 id="Titolo_Chart">Stato Programmi</h3>
                    </div>
                    <div class="col-md-2" style="display: flex; align-items: center; justify-content: center; margin-top: 8px; margin-right: 0px;">
                        <div class="dropdown">
                            <button style="box-shadow:none!important;" class="btn " type="button" id="dropdownModalitaStato" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="fa-2x fa-solid fa-ellipsis-vertical"></i>
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="dropdownModalitaStato">
                                <li><a class="dropdown-item" href="@Url.Action("DownloadModalitaStato", "Macchine", New With {.id = ViewBag.idMacchina})">Scarica dati</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <canvas id="ChartStato"></canvas>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-6">
            <div class="containerTabella card m-2">
                <h3 id="Titolo_Chart">Storico Programmi</h3>
                <table id="DisegniMacchinaTable" class="table table-striped" style="overflow: scroll!important;">
                    <thead>
                        <tr>
                            <td>
                                Cod. Programma
                            </td>
                            <td>
                                Inizio
                            </td>
                            <td>
                                Fine
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
    </div>
    <div class="col-md-6">
        <div class="col-md-12">
            <div id="ContainerComplessivo" class="card m-2 ">
                <div class="row">
                    <div class="col-md-10">
                        <h3 class="Complessivo"id="Titolo_Chart">Complessivo</h3>
                    </div>
                    <div class="col-md-2" style="display: flex; align-items: center; justify-content: center; margin-top: 8px; margin-right: 0px;">
                        <div class="dropdown">
                            <button style="box-shadow:none!important;" class="btn " type="button" id="dropdownModalitaComplessivo" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="fa-2x fa-solid fa-ellipsis-vertical"></i>
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="dropdownModalitaComplessivo">
                                <li><btn class="dropdown-item" onclick="ChangeComplessivo('1', '@ViewBag.idMacchina')">T. Taglio Compl.</btn></li>
                                <li><btn class="dropdown-item" onclick="ChangeComplessivo('2', '@ViewBag.idMacchina')">T. Mandrino Compl.</btn></li>
                                <li><btn class="dropdown-item" onclick="ChangeComplessivo('3', '@ViewBag.idMacchina')">Operatività Compl.</btn></li>
                                <li><btn class="dropdown-item" onclick="ChangeComplessivo('4', '@ViewBag.idMacchina')">Esecuzione Compl.</btn></li>
                                <li><a class="dropdown-item" href="@Url.Action("DownloadComplessivoDati", "Macchine", New With {.macchina = ViewBag.idMacchina})">Download</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <canvas id="ChartComplessivo"></canvas>
            </div>

        </div>
    </div>
</div>
    <Button type="button" id="Change_Date" onclick="ChangeDate()" Class="btn btn-primary w-auto">
        <i id="CalendayDays" class="fa-solid fa-calendar-days"></i>
       </Button>
<Button type="button" id="Hide_Navbar" onclick="ToggleNavbar()" Class="btn btn-primary w-auto">
    <i id="ToggleEye"class="fa-solid fa-eye"></i>
</Button>

<script defer>
    function ChangeDate() {
        Swal.fire({
            title: 'Seleziona date',
            html: `<input type="text" id="DataInizio" class="swal2-input" placeholder="Data inizio" autocomplete="off">
                    <input type="text" id="DataFine" class="swal2-input" placeholder="Data fine" autocomplete="off">`,
            confirmButtonText: 'Modifica ricerca',
            onReady: creaDatePicker(),
            focusConfirm: true,
            preConfirm: () => {
                const datainizio = Swal.getPopup().querySelector('#DataInizio').value
                const datafine = Swal.getPopup().querySelector('#DataFine').value
                if (!datainizio || !datafine) {
                    Swal.showValidationMessage(`Per favore, compila entrambi i campi data`)
                }
                return { datafine: datafine, datainizio: datainizio }
            }
        }).then((result) => {
            console.log(result);
         $.ajax({
        url: '/Macchine/DetailsPost?id=@ViewBag.idMacchina&datainizio='+result.value.datainizio+"&datafine="+result.value.datafine ,
        type: 'POST',
        success: function (result) {
            if (result.ok) {
                console.log(result);
                $('#Titolo_Macchina').text(result.data.CodMacchina);
                $('#Descrizione_Macchina').text(result.data.DescMacchina);
                $('#Programma_Macchina').text(result.data.ActualProgram);
                $('#Descrizione_Programma_Macchina').text(result.data.ActualProgramDesc);
                $('#Status_Macchina').text(result.data.ActualState);
                $('#UltimoAggiornamento').text("Ultimo aggiornamento: " + result.data.LastUpdate + "\n" + "Dati del periodo: " + result.data.DataInizio + "-" + result.data.DataFine);
                $('#DisegniMacchinaTable tr').remove();
                result.data.ListaDisegni.forEach(function (disegno) {
                    $('#DisegniMacchinaTable').append('<tr><td>' + disegno.CodDisegno + '</td><td>' + disegno.FirstStart + '</td><td>' + disegno.LastStart + '</td></tr>')
                });
                var DisegniMacchina = $("#DisegniMacchinaTable").DataTable();
                var kmkLabel = Object.keys(result.data.DicitonaryMacchina)
                var kmkData = Object.values(result.data.DicitonaryMacchina)
                $("#ChartTempi").remove();
                $('#ContainerTempi').append('<canvas id="ChartTempi"></canvas>');
                window.ChartTempi = new Chart(document.getElementById("ChartTempi"), {
                    type: 'pie',
                    data: {
                        labels: kmkLabel,
                        datasets: [
                            {
                                label: "# Tempo",
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
                var kmkLabelOperatore = Object.keys(result.data.DictionaryOperatore)
                var kmkDataOperatore = Object.values(result.data.DictionaryOperatore)
                $("#ChartTempiOperatore").remove();
                $('#ContainerTempiOperatore').append('<canvas id="ChartTempiOperatore"></canvas>');
                window.ChartTempiOperatore = new Chart(document.getElementById("ChartTempiOperatore"), {
                    type: 'pie',
                    data: {
                        labels: kmkLabelOperatore,
                        datasets: [
                            {
                                label: "# Tempo",
                                backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                                data: kmkDataOperatore
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
                var kmkLabelStato = Object.keys(result.data.DictionaryStato)
                var kmkDataStato = Object.values(result.data.DictionaryStato)
                $("#ChartStato").slice(1).remove();
                $('#ContainerStato').append('<canvas id="ChartStato"></canvas>');
                window.ChartTempiOperatore = new Chart(document.getElementById("ChartStato"), {
                    type: 'pie',
                    data: {
                        labels: kmkLabelStato,
                        datasets: [
                            {
                                label: "# Tempo",
                                backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                                data: kmkDataStato
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
                var kmkLabelComplessivo = Object.keys(result.data.TempoComplessivo)
                var kmkDataComplessivo = Object.values(result.data.TempoComplessivo)
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
        })
    }
    function creaDatePicker() {
        setTimeout(function () {
            var ds = datepicker("#DataInizio", {
                onSelect: instance => {
                    // Show which date was selected.
                    console.log(instance.dateSelected)
                },
                formatter: (input, date, instance) => {
                    // This will display the date as `1/1/2019`.
                    var arr = date.toString().split(" ");
                    switch (arr[1]) {
                        case 'Jan':
                            arr[1] = "01"
                            break;
                        case 'Feb':
                            arr[1] = "02"
                            break;
                        case 'Mar':
                            arr[1] = "03"
                            break;
                        case 'Apr':
                            arr[1] = "04"
                            break;
                        case 'May':
                            arr[1] = "05"
                            break;
                        case 'Jun':
                            arr[1] = "06"
                            break;
                        case 'Jul':
                            arr[1] = "07"
                            break;
                        case 'Aug':
                            arr[1] = "08"
                            break;
                        case 'Sep':
                            arr[1] = "09"
                            break;
                        case 'Oct':
                            arr[1] = "10"
                            break;
                        case 'Nov':
                            arr[1] = "11"
                            break;
                        case 'Dec':
                            arr[1] = "12"
                            break;

                        default:
                            console.log(`Sorry, we are out of ${expr}.`);
                    }

                    input.value = arr[3] + "" + arr[1] + "" + arr[2]
                },
                startDay: 1,
                customDays: ['Dom', 'Lun', 'Mar', 'Mer', 'Gio', 'Ven', 'Sab'],
                customMonths: ['Gennaio', 'Febbraio', 'Marzo', 'Aprile', 'Maggio', 'Giugno', 'Luglio', 'Agosto', 'Settembre', 'Ottobre', 'Novembre', 'Dicembre']
            });
            var es = datepicker("#DataFine", {
                formatter: (input, date, instance) => {
                    // This will display the date as `1/1/2019`.
                    var arr = date.toString().split(" ");
                    switch (arr[1]) {
                        case 'Jan':
                            arr[1] = "01"
                            break;
                        case 'Feb':
                            arr[1] = "02"
                            break;
                        case 'Mar':
                            arr[1] = "03"
                            break;
                        case 'Apr':
                            arr[1] = "04"
                            break;
                        case 'May':
                            arr[1] = "05"
                            break;
                        case 'Jun':
                            arr[1] = "06"
                            break;
                        case 'Jul':
                            arr[1] = "07"
                            break;
                        case 'Aug':
                            arr[1] = "08"
                            break;
                        case 'Sep':
                            arr[1] = "09"
                            break;
                        case 'Oct':
                            arr[1] = "10"
                            break;
                        case 'Nov':
                            arr[1] = "11"
                            break;
                        case 'Dec':
                            arr[1] = "12"
                            break;

                        default:
                            console.log(`Sorry, we are out of ${expr}.`);
                    }

                    input.value = arr[3] + "" + arr[1] + "" + arr[2]
                },
                startDay: 1,
                customDays: ['Dom', 'Lun', 'Mar', 'Mer', 'Gio', 'Ven', 'Sab'],
                customMonths: ['Gennaio', 'Febbraio', 'Marzo', 'Aprile', 'Maggio', 'Giugno', 'Luglio', 'Agosto', 'Settembre', 'Ottobre', 'Novembre', 'Dicembre']
            });
        }, 500)
    }
    $("#SelectMacchina option[value='@ViewBag.idMacchina']").attr('selected', 'selected')
    $("#SelectMacchina").change(function () {
        document.location = "/Macchine/Index?id=" + this.value;
    });
    $.ajax({
        url: '/Macchine/DetailsPost?id=@ViewBag.idMacchina' ,
        type: 'POST',
        success: function (result) {
            if (result.ok) {
                console.log(result);
                $('#Titolo_Macchina').text(result.data.CodMacchina);
                $('#Descrizione_Macchina').text(result.data.DescMacchina);
                $('#Programma_Macchina').text(result.data.ActualProgram);
                $('#Descrizione_Programma_Macchina').text(result.data.ActualProgramDesc);
                $('#Status_Macchina').text(result.data.ActualState);
                $('#UltimoAggiornamento').text("Ultimo aggiornamento: " + result.data.LastUpdate + "\n" + "Dati del periodo: "+ result.data.DataInizio + "-"+result.data.DataFine);
                result.data.ListaDisegni.forEach(function (disegno) {
                    $('#DisegniMacchinaTable').append('<tr><td>' + disegno.CodDisegno + '</td><td>' + disegno.FirstStart + '</td><td>' + disegno.LastStart + '</td></tr>')
                });
                var DisegniMacchina = $("#DisegniMacchinaTable").DataTable();
                var kmkLabel = Object.keys(result.data.DicitonaryMacchina)
                var kmkData = Object.values(result.data.DicitonaryMacchina)
                $("#ChartTempi").remove();
                $('#ContainerTempi').append('<canvas id="ChartTempi"></canvas>');
                window.ChartTempi = new Chart(document.getElementById("ChartTempi"), {
                    type: 'pie',
                    data: {
                        labels: kmkLabel,
                        datasets: [
                            {
                                label: "# Tempo",
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
                var kmkLabelOperatore = Object.keys(result.data.DictionaryOperatore)
                var kmkDataOperatore = Object.values(result.data.DictionaryOperatore)
                $("#ChartTempiOperatore").remove();
                $('#ContainerTempiOperatore').append('<canvas id="ChartTempiOperatore"></canvas>');
                window.ChartTempiOperatore = new Chart(document.getElementById("ChartTempiOperatore"), {
                    type: 'pie',
                    data: {
                        labels: kmkLabelOperatore,
                        datasets: [
                            {
                                label: "# Tempo",
                                backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                                data: kmkDataOperatore
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
                var kmkLabelStato = Object.keys(result.data.DictionaryStato)
                var kmkDataStato = Object.values(result.data.DictionaryStato)
                $("#ChartStato").remove();
                $('#ContainerStato').append('<canvas id="ChartStato"></canvas>');
                window.ChartTempiOperatore = new Chart(document.getElementById("ChartStato"), {
                    type: 'pie',
                    data: {
                        labels: kmkLabelStato,
                        datasets: [
                            {
                                label: "# Tempo",
                                backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                                data: kmkDataStato
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
                var kmkLabelComplessivo = Object.keys(result.data.TempoComplessivo)
                var kmkDataComplessivo = Object.values(result.data.TempoComplessivo)
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

</script>