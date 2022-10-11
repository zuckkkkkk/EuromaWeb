@ModelType EuromaWeb.DBViewModel

<h1>DB Val. "@Model.Codice"</h1>
<div id="jstree_demo_div">
    <ul>
        <li>
            @Model.Codice - [C/O: @Model.CostoContoLavoro] - [Lav.Int.Mdo: @Model.CostoInterno_Mdo] - [Lav.Int.Macch: @Model.CostoInterno_Macchina] - [Mat.: @Model.CostoMateriali]
            <ul>
                @For Each art In Model.ListaArt
                    @If Not art.Tipoart = "BUL" Or Not art.Tipoart = "ACQ" Or Not art.Tipoart = "ACM" Then
                        @<li>
                            @art.Codice [@art.Tipoart] - [C/O: @art.CostoContoLavoro] - [Lav.Int.Mdo: @art.CostoInterno_Mdo] - [Lav.Int.Macch: @art.CostoInterno_Macchina] - [Mat.: @art.CostoMateriali]
                            <ul>
                                @For Each bart In art.ListaArt
                                    @If Not bart.Tipoart = "BUL" Or Not bart.Tipoart = "ACQ" Then
                                        @<li>
                                            @bart.Codice [@bart.Tipoart] - [C/O: @bart.CostoContoLavoro] - [Lav.Int.Mdo: @bart.CostoInterno_Mdo] - [Lav.Int.Macch: @bart.CostoInterno_Macchina] - [Mat.: @bart.CostoMateriali]
                                             <ul>
                                                 @For Each cart In bart.ListaArt
                                                     @If Not cart.Tipoart = "BUL" Or Not cart.Tipoart = "ACQ" Then
                                                         @cart.Codice @<h4>[@cart.Tipoart] - [C/O: @cart.CostoContoLavoro] - [Lav.Int.Mdo: @cart.CostoInterno_Mdo] - [Lav.Int.Macch: @cart.CostoInterno_Macchina] - [Mat.: @cart.CostoMateriali]</h4>
                                                     End If
                                                 Next
                                             </ul>
                                        </li>
                                    End If

                                Next
                            </ul>
                        </li>
                    End If

                Next
            </ul>
        </li>
    </ul>
</div>
