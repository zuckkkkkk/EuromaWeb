@ModelType EuromaWeb.AccettazioneUC
@Code
    ViewData("Title") = "Create"
End Code


@Using (Html.BeginForm())@*"Create", "AccettazioneUC", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data"}*@
@Html.AntiForgeryToken()
    @<div>
        <p>Info aggiuntive:</p>
    <div class="row mb-3">
        <div class="col-md-6">
            <Label Class="form-check-label" for="ListOT">
                Lista OT (inserire OT di provenienza)
            </Label>
            <input class="form-control" id="ListOT" list="OTList" placeholder="Nessuna OT selezionata...">
            @Html.Raw(ViewBag.ListaOT)
        </div>
          <div class="col-md-6">
            <Label Class="form-check-label" for="checkCosto">
                MPA - Costo Maggiorato
            </Label>
            <input Class="form-check" type="checkbox" value="false" id="checkCosto" style="width:32px!important; height:32px!important;">
        </div>
    </div>
    </div>
@<div class="dropzone col" id="importupload"></div>

End Using

<script>
    $('.dropzone').dropzone();
</script>