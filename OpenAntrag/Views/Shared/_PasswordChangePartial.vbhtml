@Imports OpenAntrag
@ModelType ChangePasswordModel

<h4>Kennwort ändern</h4>
<div id="ChangePasswordSet">
    <div>
        <label class="inputicon"><i class="icon-key"></i></label>
        @Html.PasswordFor(Function(m) m.OldPassword, New With {.class = "w1", .placeholder = "Altes Kennwort"})
        @Html.ValidationMessageFor(Function(m) m.OldPassword)
    </div>
    <div>
        <label class="inputicon"><i class="icon-key" style="color: red;"></i></label>
        @Html.PasswordFor(Function(m) m.NewPassword, New With {.class = "w1", .placeholder = "Neues Kennwort"})
        @Html.ValidationMessageFor(Function(m) m.NewPassword)
    </div>
    <div>
        <label class="inputicon"><i class="icon-cw" style="color: red;"></i></label>
        @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "w1", .placeholder = "Wiederholung"})
        @Html.ValidationMessageFor(Function(m) m.ConfirmPassword)
    </div>
    <button class="btn btn-small btn-inverse" onclick="changePW('@Model.UserName'); return false;">Kennwort ändern</button>
</div>