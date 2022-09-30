@ModelType EuromaWeb.DBViewModel

<h1>DB Val. "@Model.Codice"</h1>
<div id="jstree_demo_div">
    <ul>
        <li>
            @Model.Codice
            <ul>
                @For Each art In Model.ListaArt
                    @<li>
                        @art.Codice [@art.Tipoart]
                        <ul>
                            @For Each bart In art.ListaArt
                                @<li>
                                    @bart.Codice [@bart.Tipoart]
                                    <ul>
                                        @For Each cart In bart.ListaArt
                                            @cart.Codice @<h4>[@cart.Tipoart]</h4>
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
