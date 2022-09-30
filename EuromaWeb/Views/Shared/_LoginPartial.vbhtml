@Imports Microsoft.AspNet.Identity

@If Request.IsAuthenticated Then


    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With {.id = "logoutForm", .class = "navbar-right", .style = "margin: 0!important;"})
        @Html.AntiForgeryToken()
        @<ul class="nav navbar-nav">
    @*<li>
            <div class="input-group " style="margin-right: 32px;">
                <div class="input-box"> <input type="text" class="form-control" id="NomeDisegno" placeholder="Cerca disegni..."></div>
                <button class="btn btn-primary" type="button" id="BtnDisegni"><svg width="24" height="24" xmlns="http://www.w3.org/2000/svg" fill-rule="evenodd" clip-rule="evenodd" style="fill: white;"><path d="M15.853 16.56c-1.683 1.517-3.911 2.44-6.353 2.44-5.243 0-9.5-4.257-9.5-9.5s4.257-9.5 9.5-9.5 9.5 4.257 9.5 9.5c0 2.442-.923 4.67-2.44 6.353l7.44 7.44-.707.707-7.44-7.44zm-6.353-15.56c4.691 0 8.5 3.809 8.5 8.5s-3.809 8.5-8.5 8.5-8.5-3.809-8.5-8.5 3.809-8.5 8.5-8.5z" /></svg></button>
            </div>
        </li>*@
    <li>
        @Html.ActionLink("Ciao " + User.Identity.GetUserName() + "!", "Index", "Manage", routeValues:=Nothing, htmlAttributes:=New With {.title = "Manage", .class = "nav-link"})
    </li>
    <li><a class="nav-link" href="javascript:document.getElementById('logoutForm').submit()">Disconnetti</a></li>
    <li onclick="Infos();"><a class="nav-link"><img src="~/Content/icons/Impostazioni.svg" width="20" height="20" id="Infos" /></a></li>
</ul>
    End Using
Else
    @<ul class="nav navbar-nav navbar-right">
        <li>@Html.ActionLink("Accedi", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "loginLink"})</li>
    </ul>
End If

