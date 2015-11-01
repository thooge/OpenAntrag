Imports System.ComponentModel.DataAnnotations
Imports System.ServiceModel.Syndication
Imports System.Text
Imports System.Web

Public Class Notification
    Inherits RavenModelBase

#Region "Constructors"

    Public Sub New()
        Me.CreatedAt = DateTime.Now.ToString
    End Sub

    Public Sub New(ByVal intType As NotificationTypes,
                   ByVal rep As Representation,
                   ByVal strTitle As String,
                   ByVal strText As String)
        Me.New()
        Me.NotificationType = intType
        Me.Representation = rep
        Me.Title = strTitle
        Me.Text = strText
    End Sub

    Public Sub New(ByVal intType As NotificationTypes,
                   ByVal rep As Representation,
                   ByVal strTitle As String,
                   ByVal strText As String,
                   ByVal strurl As String)
        Me.New(intType, rep, strTitle, strText)
        Me.Url = strurl
    End Sub

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property FeedItem As SyndicationItem
        Get

            Dim si As New SyndicationItem(String.Concat("[", Me.NotificationTypeString, "] ", Me.Title),
                                          "",
                                          New Uri(Me.FullUrl),
                                          Me.IdNumber,
                                          Me.CreatedAt)

            si.Content = New CDataSyndicationContent(New TextSyndicationContent(Me.Text, TextSyndicationContentKind.Html))

            With si
                .PublishDate = CType(Me.CreatedAt, DateTimeOffset)
            End With

            Return si

        End Get
    End Property

#End Region

#Region "Properties"

    Public Property NotificationType As Integer

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property NotificationTypeString() As String
        Get
            Return Notifications.GetTypeStringSingular(Me.NotificationType)
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property NotificationTypeColor() As String
        Get
            Return Notifications.GetTypeColor(Me.NotificationType)
        End Get
    End Property

    Private _RepresentationKey As String
    Public Property RepresentationKey As String
        Get
            If String.IsNullOrEmpty(_RepresentationKey) AndAlso Not IsNothing(Me.Representation) Then
                _RepresentationKey = Me.Representation.Key
            End If
            Return _RepresentationKey
        End Get
        Set(value As String)
            _RepresentationKey = value
        End Set
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public Property Representation As Representation

    <Display(Name:="Titel")>
    Public Property Title As String

    <Display(Name:="Text")>
    <DataType(DataType.MultilineText)>
    Public Property Text As String

    <Newtonsoft.Json.JsonIgnore()>
    <Display(Name:="Url")>
    Public Property Url As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>    
    Public ReadOnly Property FullUrl As String
        Get
            If String.IsNullOrEmpty(Me.Url) = False And Tools.IsUri(Me.Url) = True Then
                Return Me.Url
            Else
                Dim stb As New StringBuilder()
                stb.Append("http://").Append(HttpContext.Current.Request.Url.Authority)
                stb.Append("/notifications")
                Return stb.ToString
            End If
        End Get
    End Property

#End Region

End Class
