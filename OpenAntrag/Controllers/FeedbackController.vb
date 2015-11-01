Public Class FeedbackController
    Inherits DocumentStoreController

#Region "View Actions"

    Public Function Index(key As String) As ActionResult

        Dim lst As List(Of Feedback)
        Dim intFilterId As Integer = -1

        If String.IsNullOrEmpty(key) = True Then
            lst = Feedbacks.GetItems
        Else
            Dim fb As FeedbackType = (From ft As FeedbackType In GlobalData.FeedbackTypes.Items
                                     Where ft.Key = key
                                     Select ft).FirstOrDefault

            lst = Feedbacks.GetItemsByType(fb)
            intFilterId = fb.ID
        End If

        ViewBag.FilterId = intFilterId

        Return View(lst)

    End Function

#End Region

#Region "Service Actions"

    <HandleErrorAsJson()>
    Public Function CreateNew(type As Integer,
                              createdby As String,
                              title As String,
                              message As String) As JsonResult

        Dim fb As New Feedback With {
            .Type = type,
            .CreatedBy = createdby,
            .Title = title,
            .Message = message,
            .Status = 0,
            .Likes = 1,
            .Dislikes = 0
        }

        Me.StoreAndSave(fb)

        PushoverManager.SendNewFeedback(fb)
        NotificationManager.StoreNewFeedback(fb)
        PushbulletManager.SendNewFeedback(fb)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function Delete(id As String) As JsonResult

        'AKTUELL NICHT

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function CreateNewComment(feedbackID As String,
                                     commentedby As String,
                                     comment As String) As JsonResult

        Dim fb As Feedback = Feedbacks.GetByID(feedbackID)

        Dim fbc As New FeedbackComment With {
            .CommentedBy = commentedby,
            .CommentedByAdmin = Tools.IsAdmin(),
            .Comment = comment,
            .ID_Feedback = fb.Id
        }

        If fb.Comments Is Nothing Then
            fb.Comments = New List(Of FeedbackComment)
        End If

        fb.Comments.Add(fbc)

        Me.StoreAndSave(fb)

        PushoverManager.SendNewFeedbackComment(fb, fbc)
        NotificationManager.StoreNewFeedbackComment(fb, fbc)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = Me.RenderPartialViewToString("_FeedbackCommentPartial", fbc)}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function DeleteComment(feedbackId As String,
                                  commentedby As String,
                                  timeStamp As Integer) As JsonResult

        If Tools.IsAdmin() = False Then
            'If HttpContext.User.IsInRole("admin") = False Then
            Throw New CustomException("Dazu fehlt Dir die Berechtigung")
        End If

        Dim fb As Feedback = Feedbacks.GetByID(feedbackId)

        Dim query = From c As FeedbackComment In fb.Comments
                    Where c.CommentedBy.ToLower = commentedby.ToLower And c.Timestamp = timeStamp
                    Select c

        If query.Count > 0 Then
            Dim fbc As FeedbackComment = query.First
            fb.Comments.Remove(fbc)
        End If

        Me.StoreAndSave(fb)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function Vote(id As String, pro As Boolean) As JsonResult

        Dim fb As Feedback = Feedbacks.GetByID(id)

        If pro = True Then
            fb.Likes += 1
        Else
            fb.Dislikes += 1
        End If

        Me.StoreAndSave(fb)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = fb}

        Return jr

    End Function

#End Region

End Class
