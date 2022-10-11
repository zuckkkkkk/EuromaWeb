
@Using (Html.BeginForm("CreateAssociazione", "Manage", Nothing, FormMethod.Post, New With {.class = "ModalForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <div class="form-row">
        <div class="row">
            <div class="col-md-6">
                <label for="idSlot">Utente:</label>
                @Html.DropDownList("IdEsternoUtente", Nothing, "Non Impostato", htmlAttributes:=New With {.class = "form-control"})
            </div>
            <div class="col-md-6">
                <label for="idSlot">Licenza:</label>
                @Html.DropDownList("IdEsternoLicenza", Nothing, "Non Impostato", htmlAttributes:=New With {.class = "form-control"})
            </div>
        </div>
    </div>
    
</div>
End Using
