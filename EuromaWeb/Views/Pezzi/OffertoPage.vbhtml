@ModelType EuromaWeb.OrdinatoViewModel

<div class="container">
    <div class="loader"></div>
    <div class="row">
        <div class="col-md-12">
            <label for="Startdate">Data inizio:</label>
            @Html.EditorFor(Function(model) model.StartDate, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "Data inizio", .autocomplete = "off"}})
        </div>

    </div>
    <div class="row">
        <div class="col-md-12">
            <label for="EndDate">Data fine:</label>
            @Html.EditorFor(Function(model) model.EndDate, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "Data inizio", .autocomplete = "off"}})
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <label for="Agenti">Agenti:</label>
            @Html.DropDownList("agenti", Nothing, "Non Impostato", htmlAttributes:=New With {.class = "form-control"})
        </div>
        <div class="col-md-6">
            <label for="Agenti">Clienti:</label>
            @Html.DropDownList("clienti", Nothing, "Non Impostato", htmlAttributes:=New With {.class = "form-control"})
        </div>
    </div>
    <div class="row mt-5">
        <div class="col-md-3">
            <button class="btn btn-primary" style="width:90%;" id="SearchOfferto">Scarica dati</button>
        </div>
        @*<div class="col-md-3">
            <button class="btn btn-primary" style="width:90%;" id="SearchClientiOfferto">Scarica Clienti</button>
        </div>*@
    </div>
</div>

<script>
    $('#clienti').selectize({
        sortField: 'text'
    });
    $('#agenti').selectize({
        sortField: 'text'
    });
    var ds = datepicker("#StartDate", {
        onSelect: instance => {
            // Show which date was selected.
            console.log(instance.dateSelected)
        },
        id: 1,
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
    var es = datepicker("#EndDate", {
        id: 2,
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
        startDay: 31,
        customDays: ['Dom', 'Lun', 'Mar', 'Mer', 'Gio', 'Ven', 'Sab'],
        customMonths: ['Gennaio', 'Febbraio', 'Marzo', 'Aprile', 'Maggio', 'Giugno', 'Luglio', 'Agosto', 'Settembre', 'Ottobre', 'Novembre', 'Dicembre']
    });
    $('#agenti').change(function () {
        var value = $('#agenti :selected').val();
        if (value == '') {
            $('#clienti')[0].selectize.enable();
        } else {
            $('#clienti')[0].selectize.disable();
        }

    });
    $('#clienti').change(function () {
        var value = $('#clienti :selected');
        if (value == '') {
            $('#agenti')[0].selectize.enable();
        } else {
            $('#agenti')[0].selectize.disable();
        }
    });
</script>