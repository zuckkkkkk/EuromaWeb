@ModelType List(Of EuromaWeb.ArticoliMagazzino)

<table class="table table-striped" id="mainDataTableArticoliMagazzino" style="width:100%">
    <thead>
        <tr>
            <th>
                Codice Articolo
            </th>
            <th>
                Quantità
            </th>
            <th>
                Descrizione
            </th>
            <th style="text-align:right;">
                Azioni
            </th>
        </tr>
    </thead>
    <tbody>
        @For Each m In Model
            @<tr>
                <td>
                    @m.codArticolo
                </td>
                <td>
                    @m.qta
                </td>
                <td>
                    @m.noteArticolo
                </td>
                <td style="text-align:right; width: 40%!important;">
                    <btn class="btn btn-primary" onclick="editPosizioneArticolo(@ViewBag.idMagazzino,@m.Id )"><i class="fa-solid fa-arrows"></i></btn>
                    <a href="/Overviews/EditArticolo/@m.Id" class="btn btn-success"><i class="fa-solid fa-edit"></i></a>
                    <btn class="btn btn-success" onclick="visualizzaArticolo(@m.Id)"><i class="fa-solid fa-file"></i></btn>
                    <a Target="_blank" href="/Pezzi/StampaEtichetta?id=@m.codArticolo&fromMagazzino=True" class="btn btn-info"><i class="fa-solid fa-print"></i></a>
                    <btn class="btn btn-danger" onclick="DeleteArticolo(@m.Id)"><i class="fa-solid fa-trash"></i></btn>
                </td>
            </tr>
        Next
    </tbody>
</table>
<script>
 
    $("#mainDataTableArticoliMagazzino").DataTable({
        dom: 'Blfrtip',
        buttons: [
                    {
                        extend: 'excel',
                        text:'Excel',
                        filename: 'Lista Articoli in magazzino •  @DateTime.Now • EuromaGroup',
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
        "columns": [
            { "width": "20%" },
            { "width": "20%" },
            { "width": "20%" },
            { "width": "40%" }
        ],
        lengthMenu: [[5, 10, 15, 20, 30, 50, 75, 100, -1], [5, 10, 15, 20, 30, 50, 75, 100, "Tutti"]],
        pageLength: 10,
        language: {
            "decimal": ",",
            "emptyTable": "Nessun Dato Disponibile",
            "info": "Visualizzazione da _START_ a _END_ di _TOTAL_ Articoli",
            "infoEmpty": "Visualizzazione da 0 a 0 di 0 Articoli",
            "infoFiltered": "(Filtrati su _MAX_ articoli Totali)",
            "infoPostFix": "",
            "thousands": ".",
            "lengthMenu": "Mostra _MENU_",
            "loadingRecords": "Caricamento...",
            "processing": '<i class="fa fa-circle-notch fa-spin fa-3x fa-fw" style="color: red;"></i><span class="sr-only">Caricamento...</span> ',
            "search": "<i class='fas fa-search'></i>",
            "zeroRecords": "Nessuna Articolo",
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
        }
    });
</script>