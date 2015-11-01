Imports System.ServiceModel.Syndication
Imports System.Text
Imports System.Web
Imports System.Web.Mvc
Imports System.Xml

'http://damieng.com/blog/2010/04/26/creating-rss-feeds-in-asp-net-mvc

Public Class FeedResult
    Inherits ActionResult

    Public Property ContentEncoding() As Encoding
    Public Property ContentType() As String

    Private ReadOnly m_feed As SyndicationFeedFormatter
    Public ReadOnly Property Feed() As SyndicationFeedFormatter
        Get
            Return m_feed
        End Get
    End Property

    Public Sub New(feed As SyndicationFeedFormatter)
        Me.m_feed = feed
    End Sub

    Public Overrides Sub ExecuteResult(context As ControllerContext)

        If context Is Nothing Then
            Throw New ArgumentNullException("context")
        End If

        Dim response As HttpResponseBase = context.HttpContext.Response
        response.ContentType = If(Not String.IsNullOrEmpty(ContentType), ContentType, "application/rss+xml")

        If ContentEncoding IsNot Nothing Then
            response.ContentEncoding = ContentEncoding
        End If

        If m_feed IsNot Nothing Then
            Using xmlWriter = New XmlTextWriter(response.Output)
                xmlWriter.Formatting = Formatting.Indented
                m_feed.WriteTo(xmlWriter)
            End Using
        End If

    End Sub

End Class
