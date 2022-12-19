@Code
    ViewData("Title") = "FatturatoPage"
End Code


<div class="container">
    <div class="loader"></div>
    <div class="row">
        <div class="col-md-6">
            <label for="Angolo">Angolo:</label>
            <input type="text" name="Angolo" id="Angolo" class="form-control" />
        </div>
        <div class="col-md-6">
            <label for="Modulo">Modulo:</label>
            <input type="text" name="Modulo" id="Modulo" class="form-control" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <label for="DentiPign">Nr denti pignone:</label>
            <input type="text" name="DentiPign" id="DentiPign" class="form-control" />
        </div>
        <div class="col-md-6">
            <label for="DentiCor">Nr denti corona:</label>
            <input type="text" name="DentiCor" id="DentiCor" class="form-control" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <label for="Senso">Rotazione pignone:</label>
            <select id="Senso" name="Senso" style="border: 1px solid #ced4da; width: 100%; min-width: 100%; height: 38px;">
                <option value="DX">Destra</option>
                <option value="SX">Sinistra</option>
            </select>
        </div>
        <div class="col-md-6">
            <label for="PosNeg">Quadrante</label>
            <select id="PosNeg" name="PosNeg" style="border: 1px solid #ced4da; width:100%;min-width:100%;height:38px;">
                <option value="piu">Quadrante superiore</option>
                <option value="meno">Quadrante inferiore</option>
            </select>
        </div>
    </div>
    <div class="row mt-5">
        <div class="col-md-3">
            <button class="btn btn-primary" id="CalcolaGleason">Calcola</button>
        </div>
    </div>

    <div class="row" id="RisultatoTable" style="margin-top:2rem;">
        <div class="col-md-6" id="RisultatoTablePignone">

        </div>
        <div class="col-md-6" id="RisultatoTableCorona">

        </div>
    </div>
</div>
