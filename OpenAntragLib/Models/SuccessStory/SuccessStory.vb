Imports System.ComponentModel.DataAnnotations
Imports System.Web.Script.Serialization
Imports System.Web
Imports System.Text

Public Class SuccessStory
    Inherits RavenModelBase

    Public Property ID_Proposal As String

    Private _Proposal As Proposal
    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public Property Proposal As Proposal
        Get
            If _Proposal Is Nothing Then
                _Proposal = Proposals.GetById(Me.ID_Proposal)
            End If
            Return _Proposal
        End Get
        Set(value As Proposal)
            _Proposal = value
        End Set
    End Property

    Public Property Title As String

    Public Property StepDate As String

    <ScriptIgnore()>
    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property StepDateFormat As String
        Get
            Return CType(Me.StepDate, DateTime).ToString("dd. MMMM yyyy")
        End Get
    End Property

    Private _StepDateTimestamp As Integer = 0
    Public Property StepDateTimestamp As Integer
        Get
            Try
                If _StepDateTimestamp = 0 Then
                    _StepDateTimestamp = Tools.GetUnixTimestampFromDate(CType(Me.StepDate, DateTime))
                End If
            Catch ex As Exception
            End Try
            Return _StepDateTimestamp
        End Get
        Set(value As Integer)
            _StepDateTimestamp = value
        End Set
    End Property

    <Required(ErrorMessage:="Bitte eingeben")>
    <Newtonsoft.Json.JsonIgnore()>
    Public Property Text As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property TextHtml() As String
        Get
            Dim txt As String = HttpUtility.UrlDecode(Me.Text)
            Dim str As String = MarkdownHelper.Markdown(txt).ToHtmlString
            Return str
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property TextRaw() As String
        Get
            Dim txt As String = HttpUtility.UrlDecode(Me.Text)
            Return MarkdownHelper.MarkdownText(txt)
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property TextMarkdown() As String
        Get
            Dim txt As String = HttpUtility.UrlDecode(Me.Text)
            Return txt
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property FullUrl As String
        Get
            Return String.Concat(Me.Proposal.FullUrl, "/erfolg")
        End Get
    End Property

    Public Property ShortUrl As String

    Public Sub New()

        Me.CreatedAt = Format(DateTime.Now, "dd.MM.yyyy HH:mm:ss")
        If HttpContext.Current.User.Identity.IsAuthenticated = True Then
            Me.CreatedBy = HttpContext.Current.User.Identity.Name
        End If

    End Sub

    Public Sub New(prop As Proposal)

        Me.New()

        Me.ID_Proposal = prop.Id
        Me.Title = prop.Title
        Me.StepDate = prop.CurrentProposalStep.CreatedAt

    End Sub

End Class
