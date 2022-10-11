@ModelType UserLicenze
<style>
    input[type=checkbox] {
        -moz-appearance: none;
        -webkit-appearance: none;
        -o-appearance: none;
        outline: none;
        content: none;
    }

        input[type=checkbox]:before {
            font-family: "FontAwesome";
            content: "\f00c";
            font-size: 22px;
            border-radius: 0px 16px;
            color: transparent !important;
            display: block;
            width: 32px;
            height: 32px;
            border: 1px solid #ced4da;
            margin-right: 7px;
            max-width: none !important;
            display: flex;
            justify-content: center;
            vertical-align: middle;
        }

        input[type=checkbox]:checked:before {
            color: white !important;
            background-color: #0d6efd;
        }
</style>
@Using Html.BeginForm("ModificaLicenza", "Manage", Nothing, FormMethod.Post, New With {.class = "ModalForm"})
    @Html.AntiForgeryToken()
    @Html.HiddenFor(Function(m) m.Id)
    @Html.ValidationSummary("", New With {.class = "text-danger"})
    @<div class="row">
        <div class="col-md-6">
            @Html.LabelFor(Function(m) m.NomeLicenza, New With {.class = "col-md-2 control-label"})
            @Html.EditorFor(Function(model) model.NomeLicenza, New With {.htmlAttributes = New With {.class = "form-control", .name = "Destinatario"}})
        </div>
        <div class="col-md-6">
            @Html.LabelFor(Function(m) m.CostoLicenza, New With {.class = "col-md-2 control-label"})
            @Html.EditorFor(Function(model) model.CostoLicenza, New With {.htmlAttributes = New With {.class = "form-control"}})
        </div>
    </div>
    @<div class="row">
        <div class="col-md-6">
            @Html.LabelFor(Function(m) m.DataInizio, New With {.id = "StartDate", .class = "col-md-2 control-label"})
            @Html.EditorFor(Function(model) model.DataInizio, New With {.htmlAttributes = New With {.class = "form-control"}})
        </div>
        <div class="col-md-6">
            @Html.LabelFor(Function(m) m.DataRinnovo, New With {.id = "StartDate", .class = "col-md-2 control-label"})
            @Html.EditorFor(Function(model) model.DataRinnovo, New With {.htmlAttributes = New With {.class = "form-control"}})
        </div>
    </div>
    @<div class="row">
        <div class="col-md-6">
            @Html.LabelFor(Function(m) m.TypeLicenza, New With {.class = "col-md-12 control-label"})
            @Html.CheckBoxFor(Function(m) m.TypeLicenza, New With {.htmlAttributes = New With {.class = "form-control"}})
        </div>
        <div class="col-md-6">
            @Html.LabelFor(Function(m) m.QtaLicenze, New With {.class = "col-md-2 control-label"})
            @Html.EditorFor(Function(model) model.QtaLicenze, New With {.htmlAttributes = New With {.class = "form-control"}})
        </div>
    </div>
    @<div class="row">
        <div class="col-md-12">
            @Html.LabelFor(Function(m) m.DescrizioneLicenza, New With {.class = "col-md-2 control-label"})
            @Html.TextAreaFor(Function(model) model.DescrizioneLicenza, htmlAttributes:=New With {.class = "form-control", .rows = "6", .style = "max-width: none; min-width:250px;", .id = "summernote"})
        </div>
    </div>
    @<div class="row">
         <div class="col-md-12">
             <label class="col-md-2 control-label" for="dropzone">Documenti:</label>
                 <div class="dropzone" id="LicenzaFileUpload"></div>
         </div>
    </div>
End Using

<script>
    Dropzone.options.LicenzaFileUpload = {
        url: '/Manage/ModificaLicenza',
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
    $('.dropzone').dropzone();
    var dsC = datepicker("#DataInizio", {
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

            input.value = arr[2] + "/" + arr[1] + "/" + arr[3]
        },
        startDay: 1,
        customDays: ['Dom', 'Lun', 'Mar', 'Mer', 'Gio', 'Ven', 'Sab'],
        customMonths: ['Gennaio', 'Febbraio', 'Marzo', 'Aprile', 'Maggio', 'Giugno', 'Luglio', 'Agosto', 'Settembre', 'Ottobre', 'Novembre', 'Dicembre']
    });
    var esC = datepicker("#DataRinnovo", {
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

            input.value = arr[2] + "/" + arr[1] + "/" + arr[3]
        },
        startDay: 1,
        customDays: ['Dom', 'Lun', 'Mar', 'Mer', 'Gio', 'Ven', 'Sab'],
        customMonths: ['Gennaio', 'Febbraio', 'Marzo', 'Aprile', 'Maggio', 'Giugno', 'Luglio', 'Agosto', 'Settembre', 'Ottobre', 'Novembre', 'Dicembre']
    });

</script>