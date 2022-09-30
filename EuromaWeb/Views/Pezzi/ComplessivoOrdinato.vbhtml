 @Code
    ViewData("Title") = "FatturatoPage"
End Code


<div class="container">
    <div class="loader"></div>
    <div class="row">
        <div class="col-md-12">
            <label for="Startdate">Data inizio:</label>
            <input type="text" name="StartDate" id="StartDate" class="form-control" placeholder="Data inizio" autocomplete="off" />
        </div>

    </div>
    <div class="row">
        <div class="col-md-12">
            <label for="EndDate">Data fine:</label>
            <input type="text" name="EndDate" id="EndDate" class="form-control" placeholder="Data fine" autocomplete="off" />
        </div>
    </div>
    <div class="row mt-5">
        <div class="col-md-4">
            <button class="btn btn-primary" id="SearchComplessivoOrdinato">Calcola</button>
        </div>
    </div>
    <div id="ComplessivoChartContainer">
        <canvas style="display:none;" id="ChartComplessivo" width="800" height="450"></canvas>
    </div>

</div>
<div class="row">
   
</div>

<script>

        var dsC = datepicker("#StartDate", {
            onSelect: instance => {
                // Show which date was selected.
                console.log(instance.dateSelected)
            },
            id: 4,
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
        var esC = datepicker("#EndDate", {
            id: 3,
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

</script>