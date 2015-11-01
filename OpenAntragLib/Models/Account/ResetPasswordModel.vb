Imports System.ComponentModel.DataAnnotations

Public Class ResetPasswordModel

    <Required(ErrorMessage:="Bitte eingeben")>
    <Display(Name:="Benutzername")>
    Public Property UserNameReset() As String

    Public Property MailAddress As String

    Public Property NewPassword() As String

End Class
