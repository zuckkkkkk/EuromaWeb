@ModelType EuromaWeb.OrdineListaViewmodel
@Code
    ViewData("Title") = "Statistica"
    Dim prev1 = ViewBag.Year - 1
    Dim prev2 = ViewBag.Year - 2
End Code
<h1 style="font-weight:700;">Sez. Statistica</h1>
<h3 class="text-center"><a href="@Url.Action("Index", "Ordini", New With {.id = prev1})"><i class="fas fa-chevron-left"></i></a>Fatturato dell'anno @ViewBag.Year <a href="@Url.Action("Index", "Ordini", New With {.id = ViewBag.Year + 1})"><i class="fas fa-chevron-right"></i></a></h3>

<div class="row">
    <div class="col-md-6">
        <canvas id="myChart" width="400" height="200"></canvas>
    </div>
    <div class="col-md-6">
        <h3 class="text-center">Storico Mensile del Valore Tot. degli ultimi 3 anni</h3>
        <canvas id="myChart3" width="400" height="150" style="max-height:400px;"></canvas>
    </div>
</div>
<div class="row">
    <div class="col-md-6">
        <h3 class="text-center">Netto suddiviso per marca</h3>
        <canvas id="myChart2" width="400" height="150" style="max-height:400px;"></canvas>
    </div>
    <div class="col-md-6">
        <h3 class="text-center">Totale Italia/estero</h3>
        <canvas id="myChartStato" width="400" height="150" style="max-height:400px;"></canvas>
    </div>
</div>
@*<h3 class="text-center">Mappamondo</h3>
    <div id="regions_div" style="width: 900px; height: 500px;"></div>*@
@*<div class="row" style="margin-bottom: 5rem!important;">
        <div class="col-md-1">

        </div>
        <div class="col-md-2 text-center">
            <a class="btn btn-primary w-100">
                CMT
            </a>
        </div>
        <div class="col-md-2 text-center">
            <btn class="btn btn-primary w-100">
                Unistand
            </btn>
        </div>
        <div class="col-md-2 text-center">
            <btn class="btn btn-primary w-100">
                DrillMatica
            </btn>
        </div>
        <div class="col-md-2 text-center">
            <btn class="btn btn-primary w-100">
                M.P.A
            </btn>
        </div>
        <div class="col-md-2 text-center">
            <btn class="btn btn-primary w-100">
                ISA
            </btn>
        </div>
    </div>*@
<h3 class="text-center">Dettaglio dell'anno @DateTime.Now.Year</h3>
<table class="table" id="mainDataTable">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.Lista.First.Anno)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Lista.First.Mese)
            </th>
            <th>
                Divisione
            </th>
            <th>
                Totale costo
            </th>
        </tr>
    </thead>

    <tbody>
        @For Each item In Model.Lista
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Anno)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Mese)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Tipo_Ordine)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.tCharge)
                </td>
            </tr>
        Next

    </tbody>

</table>
<div class="row text-center">
    <a href="@Url.Action("Create", "Ordini")" class="btn btn-primary">Aggiungi Ordini</a>
</div>
<script>
    var i = '@Html.Raw(ViewBag.OrdiniMese)';
    var labels = JSON.parse(i).map(function (e) {
        return e.mnth;
    });
    var data = JSON.parse(i).map(function (e) {
        return e.tCharge;
    });;
    var a = '@Html.Raw(ViewBag.OrdiniMarca)';
    var labels1 = JSON.parse(a).map(function (e) {
        return e.mnth;
    });
    var data1 = JSON.parse(a).map(function (e) {
        return e.tCharge;
    });;
    var m1 = '@Html.Raw(ViewBag.OrdiniMeseAnnoPrima1)';
    var mdata1 = JSON.parse(m1).map(function (e) {
        return e.tCharge;
    });;
    var m2 = '@Html.Raw(ViewBag.OrdiniMeseAnnoPrima2)';
    var mdata2 = JSON.parse(m2).map(function (e) {
        return e.tCharge;
    });;
    const ctx = document.getElementById('myChart');
    const myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ["Gennaio","Febbraio","Marzo","Aprile","Maggio","Giugno","Luglio","Agosto","Settembre","Ottobre","Novembre","Dicembre"],
            datasets: [{
                label: 'Fatturato',
                data: data,
                backgroundColor: [
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)'
                ],
                borderColor: [
                    'rgba(255, 108, 43, 0)',
                    'rgba(54, 162, 235, 0)',
                    'rgba(255, 206, 86, 0)',
                    'rgba(75, 192, 192, 0)',
                    'rgba(153, 102, 255, 0)',
                    'rgba(255, 159, 64, 0)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
    const ctx2 = document.getElementById('myChart2');
    const myChart2 = new Chart(ctx2, {
        type: 'pie',
        data: {
            labels: ["Accessori e Ricambi", "Materiale CMT", "Materiale Drillmatic", "Materiale ISA", "Materiale MPA","Materiale Unistand","-"],
            datasets: [{
                label: 'Per marca',
                data: data1,
                backgroundColor: [
                    'rgba(255, 108, 43, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderColor: [
                    'rgba(255, 108, 43, 1)',
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
                xAxes: [{
                    gridLines: {
                        display: false
                    }
                }],
                yAxes: [{
                    gridLines: {
                        display: false
                    }
                }]
            }
        }
    });
    const ctx3 = document.getElementById('myChart3');
    const myChart3 = new Chart(ctx3, {
        type: 'bar',
        data: {
            labels: ["Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"],
            datasets: [{
                label: '@ViewBag.Year',
                data: data,
                backgroundColor: [
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)'
                ],
                borderColor: [
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 108, 43, 1)',
                    'rgba(255, 99, 132, 1)'
                ],
                borderWidth: 1
            },
                {
                    label: '@prev1',
                    data: mdata1,
                    backgroundColor: [
                        'rgba(255, 108, 43, .8)',
                        'rgba(255, 108, 43, .8)',
                        'rgba(255, 108, 43, .8)',
                        'rgba(255, 108, 43, .8)',
                        'rgba(255, 108, 43, .8)',
                        'rgba(255, 108, 43, .8)'
                    ],
                    borderColor: [
                        'rgba(255, 108, 43, .8)',
                        'rgba(255, 108, 43, 1)',
                        'rgba(255, 108, 43, 1)',
                        'rgba(255, 108, 43, 1)',
                        'rgba(255, 108, 43, 1)',
                        'rgba(255, 108, 43, 1)'
                    ],
                    borderWidth: 1
                },
                {
                    label: '@prev2',
                    data: mdata2,
                    backgroundColor: [
                        'rgba(255, 108, 43, .6)',
                        'rgba(255, 108, 43, .6)',
                        'rgba(255, 108, 43, .6)',
                        'rgba(255, 108, 43, .6)',
                        'rgba(255, 108, 43, .6)',
                        'rgba(255, 108, 43, .6)'
                    ],
                    borderColor: [
                        'rgba(255, 108, 43, 1)',
                        'rgba(255, 108, 43, 1)',
                        'rgba(255, 108, 43, 1)',
                        'rgba(255, 108, 43, 1)',
                        'rgba(255, 108, 43, 1)',
                        'rgba(255, 108, 43, 1)'
                    ],
                    borderWidth: 1
                }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
    const ctx4 = document.getElementById('myChartStato');
    const myChart4 = new Chart(ctx4, {
        type: 'pie',
        data: {
            labels: ["Italia", "Estero"],
            datasets: [{
                label: 'Per stato',
                data: [@ViewBag.TotITA.ToString.Split(",")(0), @ViewBag.TotEST.ToString.Split(",")(0)],
                backgroundColor: [
                    'rgba(255, 108, 43, 1)',
                    'rgba(54, 162, 235, 1)'
                ],
                borderColor: [
                    'rgba(255, 108, 43, 1)',
                    'rgba(54, 162, 235, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                xAxes: [{
                    gridLines: {
                        display: false
                    }
                }],
                yAxes: [{
                    gridLines: {
                        display: false
                    }
                }]
            }
        }
    });
    google.charts.load('current', { packages: ['corechart'] });
    google.charts.setOnLoadCallback(drawRegionsMap);

    function drawRegionsMap() {
        var array = JSON.parse(JSON.stringify(@Html.Raw(ViewBag.OrdiniPaese)));
        console.log(array);
        var data = google.visualization.arrayToDataTable(["mnth", "tCharge"],array);
        console.log(data);
        var options = {};

        var chart = new google.visualization.GeoChart(document.getElementById('regions_div'));

        chart.draw(data, options);
    }
</script>