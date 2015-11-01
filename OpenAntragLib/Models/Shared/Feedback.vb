Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc
Imports System.Web

Public Class Feedback
    Inherits RavenModelBase

#Region "Constructors"

    Public Sub New()
        Me.CreatedAt = DateTime.Now.ToString
    End Sub

#End Region

#Region "Properties"

    <Display(Name:="Typ")>
    Public Property Type As Integer

    <Display(Name:="Status")>
    Public Property Status As Integer

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property TypeObject As FeedbackType
        Get
            Dim oRetVal As FeedbackType = Nothing
            Dim query = From f As FeedbackType In GlobalData.FeedbackTypes.Items Where f.ID = Me.Type Select f
            If query.Count > 0 Then oRetVal = query.First
            Return oRetVal
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property StatusObject As FeedbackStatusCode
        Get
            Dim oRetVal As FeedbackStatusCode = Nothing
            Dim fbs As New FeedbackStatusCodes
            Dim query = From f As FeedbackStatusCode In fbs.Items Where f.ID = Me.Type Select f
            If query.Count > 0 Then oRetVal = query.First
            Return oRetVal
        End Get
    End Property

    <Required(ErrorMessage:="Bitte eingeben")>
    <Display(Name:="Titel")>
    Public Property Title As String

    <Required(ErrorMessage:="Bitte eingeben")>
    <Display(Name:="Nachricht")>
    <DataType(DataType.MultilineText)>
    Public Property Message As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property MessageHtml() As MvcHtmlString
        Get
            Dim msg As String = HttpUtility.UrlDecode(Me.Message)
            Return MarkdownHelper.Markdown(msg)
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property MessageText() As String
        Get
            Dim msg As String = HttpUtility.UrlDecode(Me.Message)
            Return MarkdownHelper.MarkdownText(msg)
        End Get
    End Property

    <Required(ErrorMessage:="Bitte eingeben")>
    <Display(Name:="Erstellt von")>
    Public Shadows Property CreatedBy As String

    <Display(Name:="Zustimmung")>
    Public Property Likes As Integer

    <Display(Name:="Ablehnung")>
    Public Property Dislikes As Integer

    Public Property Comments As List(Of FeedbackComment)

#End Region

End Class
