Imports System.ComponentModel.DataAnnotations
Imports System.Web
Imports System.Web.Security

Public Class ChangePasswordModel

    Private _User As MembershipUser

    Public Sub New()
        If String.IsNullOrEmpty(_UserName) = True Then
            _User = Membership.GetUser(HttpContext.Current.User.Identity.Name)
        End If
    End Sub

    Private _UserName As String
    Public Property UserName() As String
        Get
            Return _User.UserName
        End Get
        Set(value As String)
            _User = Membership.GetUser(value)
        End Set
    End Property

    <Required(ErrorMessage:="Bitte eingeben")>
    <DataType(DataType.Password)>
    <Display(Name:="Aktuelles Passwort")>
    Public Property OldPassword() As String

    <Required(ErrorMessage:="Bitte eingeben")>
    <StringLength(100, ErrorMessage:="Das {0} muss mindestens {2} Zeichen lang sein.", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="Neues Passwort")>
    Public Property NewPassword() As String

    <Required(ErrorMessage:="Bitte eingeben")>
    <DataType(DataType.Password)>
    <Display(Name:="Passwortbestätigung")>
    <Compare("NewPassword", ErrorMessage:="Das Passwort und die Bestätigung passen stimmen nicht überein.")>
    Public Property ConfirmPassword() As String

End Class

