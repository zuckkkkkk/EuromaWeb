@ModelType EuromaWeb.DisegnoMPAViewModel

<div>
    <dl Class="dl-horizontal">
        <div class="row">
            <div class="col-md-6">
                @Html.DisplayNameFor(Function(model) model.Cod_art)
                @Html.EditorFor(Function(model) model.Cod_art, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
            </div>
            <div class="col-md-6">
                @Html.DisplayNameFor(Function(model) model.Campo1)
                @Html.EditorFor(Function(model) model.Campo1, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                @Html.DisplayNameFor(Function(model) model.Campo2)
                @Html.EditorFor(Function(model) model.Campo2, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
            </div>
            <div class="col-md-6">
                @Html.DisplayNameFor(Function(model) model.Campo3)
                @Html.EditorFor(Function(model) model.Campo3, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                @Html.DisplayNameFor(Function(model) model.Campo4)
                @Html.EditorFor(Function(model) model.Campo4, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
            </div>
            <div class="col-md-6">
                @Html.DisplayNameFor(Function(model) model.Desc_Alnus)
                @Html.EditorFor(Function(model) model.Desc_Alnus, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
            </div>
        </div>

    </dl>
</div>
@If ViewBag.exist Then
    @Html.Action("ListDownloadMPA", "Home", New With {.id = Model.Id})
    @*@<div Class="text-center">
        <a Class="btn btn-primary" href="@Url.Action("DownloadMPA", "Home", New With {.id = Model.Id})" target="_blank">
            <i Class="fa-solid fa-download"></i>
        </a>
    </div>*@
End If
