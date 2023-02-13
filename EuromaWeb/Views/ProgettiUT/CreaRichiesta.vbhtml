@ModelType EuromaWeb.ArticoliErrati

@Using (Html.BeginForm("CreaRichiesta", "ProgettiUT", Nothing, FormMethod.Post, New With {.class = "ModalForm"}))
    @Html.AntiForgeryToken()
    @<div class="form-horizontal">
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        <div class="row">
            <div class="col-md-12">
                @Html.LabelFor(Function(model) model.Cod_OP, htmlAttributes:=New With {.class = "control-label col-md-2"})
                @Html.EditorFor(Function(model) model.Cod_OP, New With {.htmlAttributes = New With {.class = "form-control", .style = "width: 100%;"}})
                @Html.ValidationMessageFor(Function(model) model.Cod_OP, "", New With {.class = "text-danger"})
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                @Html.LabelFor(Function(model) model.Cod_OC, htmlAttributes:=New With {.class = "control-label col-md-2"})
                @Html.EditorFor(Function(model) model.Cod_OC, New With {.htmlAttributes = New With {.class = "form-control", .style = "width: 100%;"}})
                @Html.ValidationMessageFor(Function(model) model.Cod_OC, "", New With {.class = "text-danger"})
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                @Html.LabelFor(Function(model) model.Articolo, htmlAttributes:=New With {.class = "control-label col-md-2"})
                @Html.EditorFor(Function(model) model.Articolo, New With {.htmlAttributes = New With {.class = "form-control", .style = "width: 100%;"}})
                @Html.ValidationMessageFor(Function(model) model.Articolo, "", New With {.class = "text-danger"})
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                @Html.LabelFor(Function(model) model.Note_Produzione, htmlAttributes:=New With {.class = "control-label col-md-2"})
                @Html.TextAreaFor(Function(model) model.Note_Produzione, htmlAttributes:=New With {.rows = "5", .class = "form-control", .style = "max-width: none;"})
                @Html.ValidationMessageFor(Function(model) model.Note_Produzione, "", New With {.class = "text-danger"})
            </div>
        </div>

    </div>
End Using
<script>
    $("input[type=text][name=Cod_OP]").change(function () {
        var n = $("input[type=text][name=Cod_OP]").val();
        console.log(n.toString().length);
        if (n.toString().length == 19) {
            console.log("Ci entra");
            $.ajax({
                url: "/ProgettiUT/CheckOP?id=" + n.toString(),
                method: 'get',
                success: function (result) {
                    console.log(result);
                    if (result.ok) {
                        $.notify({ icon: 'fas fa-check', message: result.messaggio }, { type: 'success' });
                        $('#Cod_OC').val(result.Cod_OC);
                        $('#Articolo').val(result.Articolo);
                    }
                    else {
                        $.notify({ icon: 'fas fa-exclamation-triangle', message: result.messaggio }, { type: 'danger' });
                    }
                },
                error: function (result) {
                    $.notify({ icon: 'fas fa-exclamation-triangle', message: result.message }, { type: 'danger' });
                }
            });
        }
    });

</script>

