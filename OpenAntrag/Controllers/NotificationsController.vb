Imports System.ServiceModel.Syndication

Public Class NotificationsController
    Inherits DocumentStoreController

#Region "View Actions"

    Public Function Index(type As String) As ActionResult

        Dim typeID As Integer = Notifications.GetTypeId(type)

        Dim lst As List(Of Notification) = Notifications.GetItemsPage(typeID, 1, SettingsWrapper.NotificationPageCount)

        ViewData("Type") = type
        ViewData("TypeId") = typeID
        Return View(lst)

    End Function

    Public Function Feed(type As String) As FeedResult

        Dim typeID As Integer = Notifications.GetTypeId(type)

        Dim lst As List(Of Notification) = Notifications.GetItemsPage(typeID, 1, SettingsWrapper.NotificationPageCount)

        Dim items As IEnumerable(Of SyndicationItem) = Nothing

        If lst IsNot Nothing Then
            items = From nf As Notification In lst
                    Select nf.FeedItem
        End If

        Dim strFeedType As String = "Alle"
        If typeID > -1 Then
            strFeedType = Notifications.GetTypeStringPlural(typeID)
        End If

        Dim oFeed As New SyndicationFeed(String.Concat("OpenAntrag-Mitteilungen (", strFeedType, ")"),
                                        String.Concat("Alle Mitteilungen ", HttpContext.Request.Url.Authority),
                                        New Uri(String.Concat("http://", HttpContext.Request.Url.Authority, "/mitteilungen/feed")),
                                        "", DateTime.Now, items) With {.Language = "de-DE"}

        Return New FeedResult(New Rss20FeedFormatter(oFeed))

    End Function

#End Region

#Region "Service Actions"

    <Authorize()>
    <HandleErrorAsJson()>
    Public Function GetNewPostPartial() As JsonResult
        Return Me.GetPartialModel("NewPost", (New Notification))
    End Function

    <Authorize()>
    <HandleErrorAsJson()>
    Public Function CreateNewPost(sTitle As String, sText As String) As JsonResult

        NotificationManager.StoreNewPost(sTitle, sText)
        TwitterManager.TweetNewPost(sTitle, sText)
        PushbulletManager.SendNewPost(sTitle, sText)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = "/mitteilungen"}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function GetMoreNotifications(type As Integer,
                                         page As Integer) As JsonResult

        Dim lst As List(Of Notification) = Notifications.GetItemsPage(type, page, SettingsWrapper.NotificationPageCount)

        Dim strHtml As String = ""
        If lst.Count > 0 Then
            strHtml = Me.RenderPartialViewToString("_NotificationPartial", lst)
        End If

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = strHtml}

        Return jr

    End Function

#End Region

End Class