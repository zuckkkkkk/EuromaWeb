@ModelType List(Of ClienteViewModel)
@Code
    ViewData("Title") = "Home Page"
End Code
<div class="container" style="margin-top:2rem;">
    <h1 style="font-weight:700;">Clienti</h1>
    <div class="main-table">
        <table id="mainDataTable" class="stripe">
            <thead>
                <tr>
                    <td>
                    </td>
                    <td>
                        Nome
                    </td>
                    <td>
                        Ragg. Due
                    </td>
                    <td>
                        Regione
                    </td>
                    <td>
                        Provincia
                    </td>
                    <td>
                        Città
                    </td>
                    <td>
                        Email
                    </td>
                    <td>
                        Telefono
                    </td>
                    <td>
                        Fax
                    </td>
                    <td>
                        Divisione
                    </td>
                    <td>
                        Codice Agente
                    </td>
                </tr>
            </thead>
            <tbody>
                @For Each e In Model
                    @<tr>
    <td>
        @if e.HaFatturato Then
            @<p>✔️</p>
        Else
            @<p>✖️</p>
        End If
    </td>
    <td>
        @e.Nome1.ToString
    </td>
    <td>
        @e.RaggruppamentoDue
    </td>
    <td>
        @e.Regione
    </td>
    <td>
        @e.Provincia
    </td>
    <td>
        @e.Citta
    </td>
    <td>
        @e.Email
    </td>
    <td>
        @e.Tel
    </td>
    <td>
        @e.Fax
    </td>
    <td>
        @e.Divisione
    </td>
    <td>
        @e.Agente
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
