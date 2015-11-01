Imports System.ComponentModel.DataAnnotations
Imports System.Web
Imports System.Web.Mvc

Public Class FeedbackComment

#Region "Constructors"

    Public Sub New()

        If HttpContext.Current.User.Identity.IsAuthenticated = True Then
            Me.CommentedBy = HttpContext.Current.User.Identity.Name
        End If

        Me.CommentedAt = DateTime.Now.ToString
    End Sub

    Public Sub New(feedbackID As String)
        Me.New()
        Me.ID_Feedback = feedbackID
    End Sub

#End Region

#Region "Properties"

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public Property ID_Feedback As String

    <Required(ErrorMessage:="Bitte eingeben")>
    <Display(Name:="Dein Kommentar")>
    <DataType(DataType.MultilineText)>
    Public Property Comment As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property CommentHtml() As MvcHtmlString
        Get
            Dim cmt As String = HttpUtility.UrlDecode(Me.Comment)
            Return MarkdownHelper.Markdown(cmt)
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property CommentText() As String
        Get
            Dim msg As String = HttpUtility.UrlDecode(Me.Comment)
            Return MarkdownHelper.MarkdownText(msg)
        End Get
    End Property

    <Required(ErrorMessage:="Bitte eingeben")>
    <Display(Name:="Erstellt von")>
    Public Property CommentedBy As String

    Public Property CommentedByAdmin As Boolean

    <Display(Name:="Erstellt am")>
    Public Property CommentedAt As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property CommentedAtFormat As String
        Get
            Return CType(Me.CommentedAt, DateTime).ToString("dd. MMMM yyyy HH:mm")
        End Get
    End Property

    Private _timeStamp As Integer = 0
    Public Property Timestamp As Integer
        Get
            Try
                If _timeStamp = 0 Then
                    _timeStamp = Tools.GetUnixTimestampFromDate(CType(Me.CommentedAt, DateTime))
                End If
            Catch ex As Exception
            End Try
            Return _timeStamp
        End Get
        Set(value As Integer)
            _timeStamp = value
        End Set
    End Property

#End Region

End Class
