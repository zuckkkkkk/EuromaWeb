@Using (Html.BeginForm("_NuovoUtente", "Account", FormMethod.Post, New With {.class = "ModalForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

        <div class="form-row">
            <div class="form-group col">
                @Html.Label("Nome Utente", htmlAttributes:=New With {.class = "control-label col"})
                <div class="col-md-10">
                    @Html.TextBox("Nome", "", htmlAttributes:=New With {.class = "form-control"})
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col">
                @Html.Label("Email", htmlAttributes:=New With {.class = "control-label col"})
                <div class="col-md-10">
                    @Html.TextBox("Email", "", htmlAttributes:=New With {.class = "form-control"})
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col">
                @Html.Label("Password", htmlAttributes:=New With {.class = "control-label col"})
                <div class="col-md-10">
                    @Html.TextBox("Password", "", htmlAttributes:=New With {.class = "form-control"})
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col">
                @Html.Label("Ruolo Principale", htmlAttributes:=New With {.class = "control-label col"})
                <div class="col-md-10">
                    @Html.DropDownList("Ruolo", Nothing, "Non Impostato", htmlAttributes:=New With {.class = "form-control"})
                </div>
            </div>
        </div>
    </div>  End Using
