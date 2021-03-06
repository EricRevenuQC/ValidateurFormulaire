﻿@ModelType ValidationFormulaire.LoginModel

@Code
    ViewData("Title") = "Se connecter"
End Code

<hgroup class="title">
    <h1>@ViewData("Title").</h1>
</hgroup>

<section id="loginForm">
<h2>Utilisez un compte local pour vous connecter.</h2>
@Using Html.BeginForm(New With { .ReturnUrl = ViewData("ReturnUrl") })
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(true)

    @<fieldset>
        <legend>Formulaire de connexion</legend>
        <ol>
            <li>
                @Html.LabelFor(Function(m) m.UserName)
                @Html.TextBoxFor(Function(m) m.UserName)
                @Html.ValidationMessageFor(Function(m) m.UserName)
            </li>
            <li>
                @Html.LabelFor(Function(m) m.Password)
                @Html.PasswordFor(Function(m) m.Password)
                @Html.ValidationMessageFor(Function(m) m.Password)
            </li>
            <li>
                @Html.CheckBoxFor(Function(m) m.RememberMe)
                @Html.LabelFor(Function(m) m.RememberMe, New With { .Class = "checkbox" })
            </li>
        </ol>
        <input type="submit" value="Se connecter" />
    </fieldset>
    @<p>
        @Html.ActionLink("S'inscrire", "Register") si vous n’avez pas de compte.
    </p>
End Using
</section>

<section class="social" id="socialLoginForm">
    <h2>Utilisez un autre service pour vous connecter.</h2>
    @Html.Action("ExternalLoginsList", New With {.ReturnUrl = ViewData("ReturnUrl")})
</section>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
