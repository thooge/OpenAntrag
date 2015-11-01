Imports System.ComponentModel.DataAnnotations
Imports System.Web.Script.Serialization
Imports System.ServiceModel.Syndication
Imports System.Web
Imports System.Text

Public Class Proposal
    Inherits RavenModelBase

    Public Property Key_Representation As String

    Private _Representation As Representation
    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property Representation As Representation
        Get
            If _Representation Is Nothing Then
                _Representation = GlobalData.Representations.GetByKey(Me.Key_Representation.ToLower)
            End If
            Return _Representation
        End Get
        Set(value As Representation)
            _Representation = value
        End Set
    End Property

    <Required(ErrorMessage:="Bitte eingeben")>
    Public Property Title As String

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property IsTest() As Boolean

    <Required(ErrorMessage:="Bitte eingeben")>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property Text As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property TextHtml() As String
        Get
            Dim txt As String = HttpUtility.UrlDecode(Me.Text)
            Dim str As String = MarkdownHelper.Markdown(txt).ToHtmlString
            'str = str.CleanHtmlCode()
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
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property TextMarkdown() As String
        Get
            Dim txt As String = HttpUtility.UrlDecode(Me.Text)
            Return txt
        End Get
    End Property

    Public Property TitleUrl As String
    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property FullUrl As String
        Get
            Dim stb As New StringBuilder(Me.Representation.FullUrl)
            If String.IsNullOrEmpty(Me.TitleUrl) = False Then
                stb.Append("/").Append(Me.TitleUrl)
            End If
            Return stb.ToString
        End Get
    End Property

    Public Property UpdatedAt As String

    <ScriptIgnore()>
    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property UpdatedAtFormat As String
        Get
            If String.IsNullOrEmpty(Me.UpdatedAt) = False Then
                Return CType(Me.UpdatedAt, DateTime).ToString("dd.MM.yy HH:mm")
            Else
                Return ""
            End If
        End Get
    End Property

    Private _updatedTimestamp As Integer = 0
    Public Property UpdatedTimestamp As Integer
        Get
            Try
                If _updatedTimestamp = 0 Then
                    _updatedTimestamp = Tools.GetUnixTimestampFromDate(CType(Me.UpdatedAt, DateTime))
                End If
            Catch ex As Exception
            End Try
            Return _updatedTimestamp
        End Get
        Set(value As Integer)
            _updatedTimestamp = value
        End Set
    End Property

    Private _UpdatedBy As String
    <Newtonsoft.Json.JsonIgnore()>
    Public Property UpdatedBy As String
        Get
            Return _UpdatedBy
        End Get
        Set(value As String)
            _UpdatedBy = value
        End Set
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Tags As List(Of String)

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property TagsList As String
        Get
            If Me.Tags IsNot Nothing Then
                Return String.Join(",", Me.Tags)
            Else
                Return ""
            End If
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property HasTags As Boolean
        Get
            Dim b As Boolean
            b = Me.Tags Is Nothing
            If b = False Then b = (Me.Tags.Count = 0)
            If b = False Then b = Me.Tags.Count = 1 AndAlso Me.Tags(0).Length = 0
            Return Not b
        End Get
    End Property

    Public Property ShortUrl As String
    Public Property ExternalUrl As String
    Public Property ExternalShortUrl As String

    Public Property Key_Representative As String
    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property Representative As Representative
        Get
            Dim rv As Representative = Nothing
            If String.IsNullOrEmpty(Me.Key_Representative) = False And Me.Representation.Representatives.Count > 0 Then
                Dim query = From r As Representative In Me.Representation.Representatives
                            Where r.Key = Me.Key_Representative
                            Select r

                If query.Count > 0 Then
                    rv = query.First
                End If
            End If
            Return rv
        End Get
    End Property

    Public Property Key_Committee As String
    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property Committee As Committee
        Get
            Dim cm As Committee = Nothing
            If String.IsNullOrEmpty(Me.Key_Committee) = False And Me.Representation.Committees.Count > 0 Then
                Dim query = From c As Committee In Me.Representation.Committees
                            Where c.Key = Me.Key_Committee
                            Select c

                If query.Count > 0 Then
                    cm = query.First
                End If
            End If
            Return cm
        End Get
    End Property

    Public Property AbuseMessage() As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property AbuseMessageHtml() As String
        Get
            Dim cmt As String = HttpUtility.UrlDecode(Me.AbuseMessage)
            Return MarkdownHelper.Markdown(cmt).ToHtmlString
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property AbuseMessageText() As String
        Get
            Dim msg As String = HttpUtility.UrlDecode(Me.AbuseMessage)
            Return MarkdownHelper.MarkdownText(msg)
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property IsAbuse() As Boolean
        Get
            Return Not String.IsNullOrEmpty(Me.AbuseMessage)
        End Get
    End Property

    Public Property CommentingClosedDate As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property IsCommentingClosed() As Boolean
        Get
            Return Not String.IsNullOrEmpty(Me.CommentingClosedDate)
        End Get
    End Property

    Public Property ID_CurrentProposalStep As String
    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property CurrentProposalStep As ProposalStep
        Get
            Dim ps As ProposalStep = Nothing
            If String.IsNullOrEmpty(Me.ID_CurrentProposalStep) = False And Me.ProposalSteps.Count > 0 Then
                Dim query = From s As ProposalStep In Me.ProposalSteps
                            Where s.Id = Me.ID_CurrentProposalStep
                            Select s

                If query.Count > 0 Then
                    ps = query.First
                End If
            End If
            Return ps
        End Get
    End Property

    <CSVIgnore>
    Public Property ProposalSteps As New List(Of ProposalStep)

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property ProposalStepStack As List(Of ProposalStep)
        Get
            Dim query = From ps As ProposalStep In Me.ProposalSteps
                        Where ps.Id <> Me.ID_CurrentProposalStep
                        Order By ps.Id Descending
                        Select ps

            Return query.ToList
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property ProposalStepList As List(Of ProposalStep)
        Get
            Dim query = From ps As ProposalStep In Me.ProposalSteps
                        Order By ps.Id
                        Select ps

            Return query.ToList
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property ProposalComments As New List(Of ProposalComment)

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property ProposalCommentCountCaption As String
        Get
            Select Case Me.ProposalComments.Count
                Case 0 : Return "keine Kommentare"
                Case 1 : Return "1 Kommentar"
                Case Else : Return String.Concat(Me.ProposalComments.Count, " Kommentare")
            End Select
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public Property ContactInfo() As String = Nothing

    Public Property RatingCount As Integer = 0
    Public Property RatingSum As Integer = 0

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property Rating As Integer
        Get
            If Me.RatingSum > 0 Then
                Return Math.Round(Me.RatingSum / Me.RatingCount, 0, MidpointRounding.AwayFromZero)
            Else
                Return 0
            End If
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property SuccessStoryStatus As Integer = 0

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property ID_SuccessStory As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property HasSuccessStory As Boolean
        Get
            Return (Not String.IsNullOrEmpty(Me.ID_SuccessStory))
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property FeedItem As SyndicationItem
        Get
            Dim stbText As New StringBuilder
            'stbText.Append("<![CDATA[")
            stbText.AppendLine(Me.TextHtml)

            Me.FillProcessSteps()

            Dim stbSteps As New StringBuilder
            For Each ps As ProposalStep In Me.ProposalSteps
                stbSteps.AppendLine(String.Concat(ps.CreatedAtFormat, " - ", ps.ProcessStep.Caption))
            Next

            stbText.AppendLine(stbSteps.ToString)
            stbText.Replace(vbCrLf, "<br>")
            'stbText.Append("]]>")

            Dim si As New SyndicationItem(String.Concat(Me.Title, " (", Me.Representation.Name, ")"),
                                          "",
                                          New Uri(Me.FullUrl),
                                          Me.IdNumber,
                                          Me.CreatedAt)

            si.Content = New CDataSyndicationContent(New TextSyndicationContent(stbText.ToString, TextSyndicationContentKind.Html))

            With si
                .PublishDate = CType(Me.CreatedAt, DateTimeOffset)
            End With

            Return si

        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(representation As Representation)

        Me.CreatedAt = Format(DateTime.Now, "dd.MM.yyyy HH:mm:ss")
        Me.Representation = representation
        Me.Key_Representation = Me.Representation.Key

    End Sub

    Public Sub FillProcessSteps()

        If Me.Representation IsNot Nothing AndAlso Me.Representation.ProcessSteps IsNot Nothing Then
            For Each ps As ProposalStep In Me.ProposalSteps
                Dim query = From p As ProcessStep In Me.Representation.ProcessSteps
                            Where p.ID = ps.ID_ProcessStep
                            Select p

                If query.Count > 0 Then
                    ps.ProcessStep = query.First()
                End If
            Next
        End If

    End Sub

End Class

Public Class ProposalStep
    Inherits RavenModelBase

    Public Property ID_ProcessStep As Integer

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public Property ProcessStep As ProcessStep

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Info As String

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property InfoHtml() As String
        Get
            Dim cmt As String = HttpUtility.UrlDecode(Me.Info)
            Return MarkdownHelper.Markdown(cmt).ToHtmlString
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property InfoText() As String
        Get
            Dim cmt As String = HttpUtility.UrlDecode(Me.Info)
            Return MarkdownHelper.MarkdownText(cmt)
        End Get
    End Property

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property InfoMarkdown() As String
        Get
            Dim txt As String = HttpUtility.UrlDecode(Me.Info)
            Return txt
        End Get
    End Property

    Public Sub New()
        Me.CreatedAt = Format(DateTime.Now, "dd.MM.yyyy HH:mm:ss")
        If HttpContext.Current.User.Identity.IsAuthenticated = True Then
            Me.CreatedBy = HttpContext.Current.User.Identity.Name
        End If
    End Sub

End Class
