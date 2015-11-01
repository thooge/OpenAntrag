@Imports OpenAntrag
@ModelType ResetPasswordModel

<h4>Kennwort vergessen?</h4>
<div id="ResetPasswordSet" style="max-width: 238px;">

    <p>Diese Funtkion steht aktuell nicht zur Verfügung. 
        Bitte wende Dich via Mail an: <a href="mailto:@("webmaster@" & Tools.GetRequestDomain)">@("webmaster@" & Tools.GetRequestDomain)</a></p>
@*  
    <div>
        <label class="inputicon"><i class="icon-user"></i></label>
        @Html.TextBoxFor(Function(m) m.UserNameReset, New With {.class = "w1", .placeholder = "Benutzername"})
        @Html.ValidationMessageFor(Function(m) m.UserNameReset)
    </div>
    <button class="btn btn-small btn-inverse" onclick="resetPW(); return false;">Neues Kennwort anfordern</button>
*@
</div>

<a style="display: block; margin-top: 10px;"
    href="javascript:go();"
    onclick="$('#more-login').css('height', '');$('#resetpw-wrapper').slideUp(function() { $('#logon-wrapper').slideDown(function() {scrollBottom(); }); });">...zurück zum Login</a>
