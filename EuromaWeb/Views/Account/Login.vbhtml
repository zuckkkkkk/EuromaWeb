@ModelType LoginViewModel
@Code
    ViewBag.Title = "Accedi"
End Code
<style>
    #loginForm {
        position: absolute;
        top: 0 !important;
        right: 0 !important;
        height: 100vh;
        width: 50vw;
        box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
        padding: 20px 30px;
        background-color: white;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    body {
        overflow: hidden;
    }
    .LogoLogin {
        position: absolute;
        top: 0 !important;
        left: 0 !important;
        height: 100vh;
        width: 50vw;
        box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
        padding: 20px 30px;
        background-color: none;
        display: flex;
        align-items: center;
        justify-content: center;
    }
    @@media only screen and (max-width: 768px) {
        .LogoLogin, #particles-js {
            opacity: 0 !important;
        }
        #loginForm{
            left:0!important;
            width: 100vw!important;
        }
    }

    #particles-js {
        position: absolute;
        top: 0;
        left: 0;
        width: 50vw;
        height: 100vh;
        z-index: -1;
        background-color:white;
    }

    #gradient-canvas {
        width: 100%;
        height: 100%;
        --gradient-color-1: #c3e4ff;
        --gradient-color-2: #6ec3f4;
        --gradient-color-3: #eae2ff;
        --gradient-color-4: #b9beff;
    }

    .login {
        background-color: white;
    }
    input[type=checkbox] {
        -moz-appearance: none;
        -webkit-appearance: none;
        -o-appearance: none;
        outline: none;
        content: none;
    }

        input[type=checkbox]:before {
            font-family: "FontAwesome";
            content: "\f00c";
            font-size: 22px;
            border-radius: 0px 16px;
            color: transparent !important;
            display: block;
            width: 32px;
            height: 32px;
            border: 1px solid #ced4da;
            margin-right: 7px;
            max-width:none!important;
            display: flex;
            justify-content: center;
            vertical-align: middle;
        }

        input[type=checkbox]:checked:before {
            color: white !important;
            background-color: #0d6efd;
        }
</style>
<div id="particles-js">
</div>
<div class=" LogoLogin">
    <img src="~/Asset/img/logo-nero.png" height="300 "style="z-index:1000;" />
</div>

<section id="loginForm">
    <div style="width: 60%!important">
        <h1>Login</h1>
        @Using Html.BeginForm("Login", "Account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
            @Html.AntiForgeryToken()
            @<text>
                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                <div class="form-group">
                    @Html.LabelFor(Function(m) m.UserName, New With {.class = "col-md-3 control-label"})
                    <div class="col-md-12">
                        @Html.TextBoxFor(Function(m) m.UserName, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(m) m.UserName, "", New With {.class = "text-danger"})
                    </div>
                </div>
                <div class="form-group">
                    @Html.LabelFor(Function(m) m.Password, New With {.class = "col-md-3 control-label"})
                    <div class="col-md-12">
                        @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger"})
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <div class="col-md-12 mt-2">
                            @Html.CheckBoxFor(Function(m) m.RememberMe)
                            @Html.LabelFor(Function(m) m.RememberMe)
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class=" col-md-12 text-center">
                        <input type="submit" value="Accedi" class="btn btn-primary mt-2" style="width:100%" />
                    </div>
                </div>

            </text>
        End Using
    </div>

</section>
<div id="fold">
    <p id="fold_p" style="position:absolute; bottom: 0; right:16px; margin-bottom:0!important; text-align: center;">V. @Costanti.WebAppVersion</p>
</div>
<script>

</script>
@Section Scripts

    @Scripts.Render("~/bundles/jqueryval")
End Section
<script>
    $(document).ready(function () {
        particlesJS.load('particles-js', '/Content/json/particlesjs-config.json', function () {
            console.log('callback - particles.js config loaded');
        });
    });
</script>