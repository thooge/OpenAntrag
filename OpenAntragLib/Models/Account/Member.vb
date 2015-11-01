Imports System.ComponentModel.DataAnnotations
Imports Raven.Client
Imports System.Web.Script.Serialization
Imports System.IO
Imports System.Web.Security

Public Class Member

    '<JsonIgnore()>
    <ScriptIgnore()>
    Public ReadOnly Property User As MembershipUser
        Get
            If String.IsNullOrEmpty(Me.UserName) = False Then
                Return Membership.GetUser(Me.UserName)
            Else
                Return Nothing
            End If
        End Get
    End Property

    <Display(Name:="ID")>
    Public Property Id As String

    <Display(Name:="User-Key")>
    Public Property UserKey As String

    <Display(Name:="Benutzername")>
    Public Property UserName() As String

    <Display(Name:="EMail")>
    Public Property Mail() As String

    <Display(Name:="API-Key")>
    Public Property APIKey As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property HasAPIKey As Boolean
        Get
            Return Not String.IsNullOrEmpty(Me.APIKey)
        End Get
    End Property

    <Display(Name:="Pushover-User-Key")>
    Public Property PushoverUserKey As String

End Class
