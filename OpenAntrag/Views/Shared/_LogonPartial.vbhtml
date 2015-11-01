@Imports OpenAntrag
@ModelType LogonModel

<h4>Login</h4>
<div>
    <label class="inputicon"><i class="icon-user"></i></label>
    @Html.TextBoxFor(Function(m) m.UserName, New With {.class = "w1", .placeholder = "Benutzername"})
    @Html.ValidationMessageFor(Function(m) m.UserName)
</div>
<div>
    <label class="inputicon"><i class="icon-key"></i></label>
    @Html.PasswordFor(Function(m) m.Password, New With {.class = "w1", .placeholder = "Kennwort"})
    @Html.ValidationMessageFor(Function(m) m.Password)
</div>
<input type="submit" class="btn btn-small btn-inverse" value="Anmelden" />

<a style="display: block; float: right;"
    href="javascript:go();"
    onclick="$('#more-login').height($('#more-login').innerHeight());$('#logon-wrapper').slideUp(function() { $('#resetpw-wrapper').slideDown(function() {scrollBottom(); } ); });">Kennwort vergessen?</a>
