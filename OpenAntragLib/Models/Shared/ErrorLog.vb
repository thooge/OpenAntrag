Imports System.Web

Public Class ErrorLog
    Inherits RavenModelBase

    Public Sub New()
        Me.CreatedAt = Format(DateTime.Now, "dd.MM.yyyy HH:mm:ss")
        Me.Parameter = New List(Of String)()
    End Sub

    Public Property AbsoluteUri() As String
    Public Property Controller() As String
    Public Property Action() As String
    Public Property RequestType() As String
    Public Property Parameter() As List(Of String)
    Public Property ReferrerUrl() As String
    Public Property AjaxCall() As Boolean
    Public Property Message() As String
    Public Property Occurrences() As List(Of ErrorOccurrence)

    <Raven.Imports.Newtonsoft.Json.JsonIgnore>
    Public ReadOnly Property Url() As String
        Get
            Return String.Concat("http://", Tools.GetRequestDomain(), "/errors/", Me.Id)
        End Get
    End Property
End Class

Public Class ErrorOccurrence
    Inherits ModelBase

    Public Sub New()
        Me.CreatedAt = Format(DateTime.Now, "dd.MM.yyyy HH:mm:ss")
        If HttpContext.Current.Request.IsAuthenticated Then
            CreatedBy = (HttpContext.Current.User.Identity.Name)
        End If
    End Sub

    Public Property UserAgent() As String

End Class
