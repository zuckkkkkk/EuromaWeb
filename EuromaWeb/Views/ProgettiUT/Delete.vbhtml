@ModelType EuromaWeb.ProgettiUT
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>ProgettiUT</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.OC_Riferimento)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OC_Riferimento)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OperatoreSmistamento)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OperatoreSmistamento)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Operatore)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Operatore)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DataCreazione)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DataCreazione)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DataCompletamento)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DataCompletamento)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Note)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Note)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.File)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.File)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.StatoProgetto)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.StatoProgetto)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Flag_Invio_Materiali)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Flag_Invio_Materiali)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Flag_1)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Flag_1)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Flag_2)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Flag_2)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Flag_3)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Flag_3)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
