Imports Raven.Client
Imports System.Text
Imports System.Web

Public Enum NotificationTypes
    Post = 0
    NewProposal = 1
    NewProposalComment = 2
    NewFeedback = 3
    NewFeedbackComment = 4
    NextProposalStep = 5
    SuccessStories = 6
End Enum

Public Module NotificationManager

    Public Sub StoreNewPost(strTitle As String, strText As String)

        Try
            Dim nf As New Notification(NotificationTypes.Post,
                                       Nothing,
                                       strTitle,
                                       strText,
                                       Nothing)
            StoreNotification(nf)
        Catch ex As Exception
        End Try

    End Sub

    Public Sub StoreNewProposal(ByVal model As Proposal)

        If model.IsTest = False Then
            Try
                Dim stbTitle As New StringBuilder
                stbTitle.Append(model.Representation.Label).Append(": ")
                stbTitle.Append("Neuer Antrag ")
                stbTitle.Append("'").Append(model.Title).Append("'")

                Dim stbText As New StringBuilder
                stbText.Append(model.TextRaw.CutEllipsis(100))

                Dim nf As New Notification(NotificationTypes.NewProposal,
                                           model.Representation,
                                           stbTitle.ToString,
                                           stbText.ToString,
                                           model.FullUrl)
                StoreNotification(nf)
            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub StoreNewProposalComment(model As ProposalComment, prop As Proposal)

        If prop.IsTest = False Then
            Try
                Dim stbTitle As New StringBuilder
                stbTitle.Append(prop.Representation.Label).Append(": ")
                stbTitle.Append("Antragskommentar von ")
                stbTitle.Append("'").Append(model.CommentedBy).Append("'")

                Dim stbText As New StringBuilder
                stbText.Append(prop.Title).Append(": ")
                stbText.Append(model.CommentText.CutEllipsis(50))

                Dim stbUrl As New StringBuilder()
                stbUrl.Append(prop.FullUrl)
                stbUrl.Append("#comments")

                Dim nf As New Notification(NotificationTypes.NewProposalComment,
                                           prop.Representation,
                                           stbTitle.ToString,
                                           stbText.ToString,
                                           stbUrl.ToString)
                StoreNotification(nf)
            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub StoreNextProposalStep(ByVal prop As Proposal)

        If prop.IsTest = False Then
            Try
                prop.FillProcessSteps()
                Dim ps As ProposalStep = prop.CurrentProposalStep

                Dim stbTitle As New StringBuilder()
                stbTitle.Append(ps.ProcessStep.Representation.Label).Append(": ")
                stbTitle.Append(ps.ProcessStep.ShortCaption)

                Dim stbText As New StringBuilder
                stbText.Append(prop.Title).Append(": ")
                stbText.Append(ps.InfoText.CutEllipsis(100))

                Dim nf As New Notification(NotificationTypes.NextProposalStep,
                                           ps.ProcessStep.Representation,
                                           stbTitle.ToString,
                                           stbText.ToString,
                                           prop.FullUrl.ToString)
                StoreNotification(nf)
            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub StoreNewFeedback(ByVal feedback As Feedback)

        Try
            Dim stbTitle As New StringBuilder()
            stbTitle.Append("Neues Feedback ")
            stbTitle.Append("(").Append(feedback.TypeObject.Name).Append(")")
            stbTitle.Append(" von ")
            stbTitle.Append("'").Append(feedback.CreatedBy).Append("'")

            Dim stbText As New StringBuilder
            stbText.Append(feedback.Title).Append(": ")
            stbText.Append(feedback.MessageText.CutEllipsis(100))

            Dim stbUrl As New StringBuilder()
            stbUrl.Append("http://").Append(HttpContext.Current.Request.Url.Authority)
            stbUrl.Append("/feedback")
            stbUrl.Append("#").Append(feedback.IdNumber)

            Dim nf As New Notification(NotificationTypes.NewFeedback,
                                       Nothing,
                                       stbTitle.ToString,
                                       stbText.ToString,
                                       stbUrl.ToString)
            StoreNotification(nf)
        Catch ex As Exception
        End Try

    End Sub

    Public Sub StoreNewFeedbackComment(ByVal feedback As Feedback, comment As FeedbackComment)

        Try
            Dim stbTitle As New StringBuilder()
            stbTitle.Append("Feedback-Kommentar von ")
            stbTitle.Append("'").Append(comment.CommentedBy).Append("'")

            Dim stbText As New StringBuilder
            stbText.Append(feedback.Title).Append(": ")
            stbText.Append(comment.CommentText.CutEllipsis(50))

            Dim stbUrl As New StringBuilder()
            stbUrl.Append("http://").Append(HttpContext.Current.Request.Url.Authority)
            stbUrl.Append("/feedback")
            stbUrl.Append("#").Append(feedback.IdNumber)

            Dim nf As New Notification(NotificationTypes.NewFeedbackComment,
                                       Nothing,
                                       stbTitle.ToString,
                                       stbText.ToString,
                                       stbUrl.ToString)
            StoreNotification(nf)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub StoreNotification(nf As Notification)

        Using ds As IDocumentSession = DataDocumentStore.Session()
            ds.Store(nf)
            ds.SaveChanges()
        End Using
    End Sub

    Public Sub StoreNewSuccessStory(ByVal model As SuccessStory)

        If model.Proposal.IsTest = False Then
            Try
                Dim stbTitle As New StringBuilder                
                stbTitle.Append("Erfolgsgeschichte in ")
                stbTitle.Append(model.Proposal.Representation.Label).Append(": ")
                stbTitle.Append("'").Append(model.Title).Append("'")

                Dim stbText As New StringBuilder
                stbText.Append(model.TextRaw.CutEllipsis(100))

                Dim nf As New Notification(NotificationTypes.SuccessStories,
                                           model.Proposal.Representation,
                                           stbTitle.ToString,
                                           stbText.ToString,
                                           model.FullUrl)
                StoreNotification(nf)
            Catch ex As Exception
            End Try
        End If

    End Sub

End Module
