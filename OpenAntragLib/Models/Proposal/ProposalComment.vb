Imports System.ComponentModel.DataAnnotations
Imports System.Web.Script.Serialization
Imports System.Web

Public Class ProposalComment

    <Newtonsoft.Json.JsonIgnore()>
    Public Property ID_Proposal As String

    <Required(ErrorMessage:="Bitte eingeben")>
    <Display(Name:="Dein Kommentar")>
    <DataType(DataType.MultilineText)>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property Comment As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property CommentHtml() As String
        Get
            Dim cmt As String = HttpUtility.UrlDecode(Me.Comment)
            Return MarkdownHelper.Markdown(cmt).ToHtmlString
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property CommentText() As String
        Get
            Dim msg As String = HttpUtility.UrlDecode(Me.Comment)
            Return MarkdownHelper.MarkdownText(msg)
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property CommentRaw() As String
        Get
            Dim txt As String = HttpUtility.UrlDecode(Me.Comment)
            Return MarkdownHelper.MarkdownText(txt)
        End Get
    End Property

    <Required(ErrorMessage:="Bitte eingeben")>
    <Display(Name:="Dein Name")>
    Public Property CommentedBy As String

    Public Property CommentedAt As String

    <ScriptIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property CommentedAtFormat As String
        Get
            Return CType(Me.CommentedAt, DateTime).ToString("dd. MMMM yyyy HH:mm")
        End Get
    End Property

    Private _CommentedAtTimestamp As Integer = 0
    Public Property CommentedAtTimestamp As Integer
        Get
            Try
                If _CommentedAtTimestamp = 0 Then
                    _CommentedAtTimestamp = Tools.GetUnixTimestampFromDate(CType(Me.CommentedAt, DateTime))
                End If
            Catch ex As Exception
            End Try
            Return _CommentedAtTimestamp
        End Get
        Set(value As Integer)
            _CommentedAtTimestamp = value
        End Set
    End Property


    Public Sub New()
    End Sub

    Public Sub New(ps As Proposal)

        Me.ID_Proposal = ps.Id

    End Sub

End Class
