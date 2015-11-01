Public Class AccountController
    Inherits CommonController

    <HttpPost()>
    Public Function Logon(ByVal model As LogonModel) As ActionResult

        Dim rp As Representation = Nothing
        Dim exc As CustomException = Nothing

        If ModelState.IsValid Then
            If Membership.ValidateUser(model.UserName, model.Password) Then

                FormsAuthentication.SetAuthCookie(model.UserName, True)

                Dim mu As MembershipUser = Membership.GetUser(model.UserName)
                Members.EnsureMember(mu)

                Dim ur As String() = Roles.GetRolesForUser(mu.UserName)

                For Each r As Representation In GlobalData.Representations.Items
                    If ur.Contains(r.Key) = True Then
                        rp = r
                        Exit For
                    End If
                Next
            Else
                exc = New CustomException("Der Benutzername oder das Passwort sind nicht korrekt.", "Login-Fehler")
            End If
        End If

        If exc IsNot Nothing Then
            TempData("ExceptionAlert") = exc
        End If

        If rp IsNot Nothing Then
            Return RedirectToAction("Index", "Representation", New With {.keyRepresentation = rp.Key})
        Else
            Return RedirectToAction("Index", "Home")
        End If

    End Function

    Public Function Logoff() As ActionResult

        FormsAuthentication.SignOut()

        Dim strReturnUrl As String = Nothing
        If HttpContext.Request.Params("returnUrl") IsNot Nothing Then
            strReturnUrl = HttpContext.Request.Params("returnUrl")
        End If

        If Url.IsLocalUrl(strReturnUrl.ToLocalUrl) Then
            Return Redirect(strReturnUrl)
        Else
            Return RedirectToAction("Index", "Home")
        End If

    End Function

    <HandleErrorAsJson()>
    Public Function ResetPassword(ByVal model As ResetPasswordModel) As JsonResult

        'wg. Artem-ResetPassword-Bug
        Throw New Exception("Diese Funktion steht aktuell nicht zur Verfügung")


        'If model.UserNameReset Is Nothing OrElse model.UserNameReset.Length = 0 Then
        '    Throw New Exception("Der Benutzername ist leer")
        'End If

        'Dim usr As MembershipUser = Membership.GetUser(model.UserNameReset, True)
        'If usr Is Nothing Then
        '    Throw New Exception("Dieser Benutzername existiert nicht")
        'End If

        'model.MailAddress = usr.Email
        'model.NewPassword = usr.ResetPassword()
        ''Dim strReset As String = usr.ResetPassword() 'hässliches Passwort
        ''model.NewPassword = Membership.GeneratePassword(10, 0) 'nettes Passwort
        ''usr.ChangePassword(strReset, model.NewPassword)

        ''GEHT NICHT: Dim bolVal As Boolean = Membership.ValidateUser(model.UserNameReset, model.NewPassword)

        'MailManager.SendResetPasswordToUser(model)

        'Dim jr As New JsonResult
        'jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        'jr.Data = New With {.success = True, .data = Me.RenderPartialViewToString("_PasswordResetSuccessPartial")}

        'Return jr

    End Function

    <Authorize()>
    <HandleErrorAsJson()>
    Public Function ChangePassword(ByVal model As ChangePasswordModel) As JsonResult

        If User.Identity.Name <> model.UserName Then
            Throw New Exception("Diese Methode steht nur dem angemeldeten Benutzer zur Verfügung")
        End If

        If ModelState.IsValid = False Then
            Throw New Exception("Ungültige Kennwortdaten")
        End If

        Dim bolSuccess As Boolean

        Try
            Dim usr As MembershipUser = Membership.GetUser(User.Identity.Name, True)
            bolSuccess = usr.ChangePassword(model.OldPassword, model.NewPassword)
        Catch ex As Exception
            bolSuccess = False
        End Try

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        If bolSuccess = True Then
            jr.Data = New With {.success = True, .data = Me.RenderPartialViewToString("_PasswordChangeSuccessPartial")}
        Else
            Throw New Exception("Das Passwort wurde nicht geändert. Bitte überprüfe Deine Eingaben.")
        End If

        Return jr

    End Function

End Class