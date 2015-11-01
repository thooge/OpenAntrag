Imports System.ComponentModel.DataAnnotations

Public Class LogonModel

    <Required(ErrorMessage:="Bitte eingeben")>
    <Display(Name:="Benutzername")>
    Public Property UserName() As String

    <Required(ErrorMessage:="Bitte eingeben")>
    <DataType(DataType.Password)>
    <Display(Name:="Passwort")>
    Public Property Password() As String

    <Display(Name:="Erinnere Dich an mich")>
    Public Property RememberMe() As Boolean

    Public Property ReturnUrl As String

End Class
