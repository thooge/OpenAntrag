@Imports OpenAntrag
@Imports ActionMailer.Net

@ModelType ResetPasswordModel
    
@Code
    Layout = Nothing
End Code

Ahoi,

Du hast ein neues Passwort für Dein Konto '@Model.UserNameReset' angefordert. Hier ist es:

      @Model.NewPassword

Du kannst Dich mit diesem nun wieder bei OpenAntrag anmelden.