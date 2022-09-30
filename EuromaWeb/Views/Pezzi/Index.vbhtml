@ModelType List(Of PezzoViewModel)
@Code
    ViewData("Title") = "Analisi Articoli"
End Code
<div class="container">
    <h2 style="margin-top: 1rem; margin-bottom: 1rem;">Articoli</h2>

    @*<div class="row text-center"><a href="@Url.Action("InventarioPerMagazzino", "Home")" class="btn btn-primary">Scarica Dati</a></div>*@
    <h2 id="loader" class="loading text-center">
        <img src="~/Content/svg/loading-gif-icon-11 (1).gif" alt="Loading" width="250" />
    </h2>
    <div class="main-table" style="display: none;box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px; padding: 15px; border-radius: 15px; ">
        <table id="mainDataTable" class="stripe" style="display: none; width: 100%;">
            <thead>
                <tr>
                    @If User.IsInRole("Commerciale_Admin") Or User.IsInRole("Admin") Then
                       @<td>
                        </td>
                    End If
                    
                    <td>
                        Articolo
                    </td>
                    <td>
                        Descrizione
                    </td>
                    <td>

                    </td>
                </tr>
            </thead>
            <tbody>
                @For Each e In Model
                    @<tr>
                @If User.IsInRole("Commerciale_Admin") Or User.IsInRole("Admin") Then
                    @<td Class="dt-control">
                        </td>
                End If
                        
                        <td>
                            @e.CodArt.ToString
                        </td>
                        <td>
                            @e.Descrizione
                        </td>
                <td>
                    <a href="/Pezzi/StampaEtichetta?id=@e.CodArt.tostring" target="_blank">Stampa etichetta</a>
                </td>
                    </tr>
                Next
            </tbody>
        </table>
    </div>
</div>


@*<div class="container" style="display: flex; align-content: center; justify-content:center;">
        <img src="~/Asset/img/logo-nero.png"/>
    </div>*@
