@ModelType EuromaWeb.DBViewModel

<div id="jstree_demo_div">
    <ul>
        <li>
            @Model.Codice
            <ul>
                @For Each art In Model.ListaArt
                    @<li>
                         <p style="color:@art.Colore!important;">@art.Codice [@art.Tipoart]</p>
                        <ul>
                            @For Each bart In art.ListaArt
                                @<li>
                                     <p style="color:@bart.Colore!important;">@bart.Codice [@bart.Tipoart]</p>
                                    <ul>
                                        @For Each cart In bart.ListaArt
                                            @cart.Codice @<h4 style="color:@cart.Colore!important;">[@cart.Tipoart]</h4>
                                        Next
                                    </ul>
                                </li>
                            Next
                        </ul>
                    </li>
                Next
            </ul>
        </li>
    </ul>
</div>

<script>
    $('#jstree_demo_div').jstree({
        "core": {
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
</script>