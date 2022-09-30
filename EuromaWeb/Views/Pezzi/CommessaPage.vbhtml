@Code
    ViewData("Title") = "Commesse"
End Code


<div class="container">
    <h2 style="margin-top: 1rem; margin-bottom: 1rem;">Commessa</h2>

    <div class="loader"></div>
    <div class="row">
        <div class="col-md-5">
            <input type="text" name="Anno" id="Anno" class="form-control" placeholder="Anno" autocomplete="off" />
        </div>
        <div class="col-md-2">
            <input type="text" name="OP" id="OP" class="form-control" placeholder="OP" autocomplete="off" />
        </div>
        <div class="col-md-5">
            <input type="text" name="Num" id="Num" class="form-control" placeholder="Num" autocomplete="off" />
        </div>
    </div>
    <div class="row mt-2">
        <div class="col-md-3">
            <button class="btn btn-primary" id="SearchCommessa">Scarica dati</button>
        </div>
    </div>
    <div id="jtimeline-demo" class="jtimeline" style="display: none;">
        <ul class="jtimeline-events">
        </ul>
    </div>
</div>
