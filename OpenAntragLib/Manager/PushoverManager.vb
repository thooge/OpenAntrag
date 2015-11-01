Imports System.Text
Imports System.Web
Imports System.Net

Public Module PushoverManager

#Region "Owner Messages"

    Public Sub Send(title As String, msg As String)
        Dim pm As New PushoverMessage(title, msg)
        Send(pm)
    End Sub

    Public Sub Send(pm As PushoverMessage)

        If SettingsWrapper.SendPushoverNotification = True Then
            Try
                Using webClient = New WebClient
                    webClient.UploadValues(New Uri(SettingsWrapper.PushoverApiUrl), pm.Params)
                End Using
            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub SendError(ByVal methodName As String, ByVal exc As Exception)

        If SettingsWrapper.SendPushoverNotification = True Then
            Try
                Dim stbTitle As New StringBuilder("ERROR in ")
                stbTitle.Append(methodName)

                Dim stbMsg As New StringBuilder
                stbMsg.Append(exc.Message)

                Dim poMessage As New PushoverMessage(stbTitle.ToString, stbMsg.ToString)
                Send(poMessage)

            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub SendNewProposal(ByVal model As Proposal)

        If SettingsWrapper.SendPushoverNotification = True Then
            Try
                Dim stbTitle As New StringBuilder("Neuer Antrag in ")
                stbTitle.Append(model.Representation.Label)

                Dim stbMsg As New StringBuilder
                stbMsg.Append(model.Id).Append(": ")
                stbMsg.Append(model.Title)

                Dim poMessage As New PushoverMessage(stbTitle.ToString, stbMsg.ToString, model.FullUrl, "")
                Send(poMessage)

            Catch ex As Exception
            End Try
        End If

    End Sub

    'Public Sub SendNewProposalComment(model As ProposalComment, prop As Proposal)

    '    If SettingsWrapper.SendPushoverNotification = True Then
    '        Try
    '            Dim stbTitle As New StringBuilder("Antragskommentar in ")
    '            stbTitle.Append(prop.Representation.Key)

    '            Dim stbMsg As New StringBuilder
    '            stbMsg.Append(prop.Title).Append(" : ")
    '            stbMsg.Append(Left(model.CommentText, 50)).Append("...")

    '            Dim poMessage As New PushoverMessage(stbTitle.ToString, stbMsg.ToString, prop.FullUrl, "")
    '            Send(poMessage)

    '        Catch ex As Exception
    '        End Try
    '    End If

    'End Sub

    Public Sub SendNewProposalAbuseNotice(model As ProposalAbuseNotice)

        If SettingsWrapper.SendPushoverNotification = True Then
            Try
                Dim stbTitle As New StringBuilder("Missbrauchsmeldung für ")
                stbTitle.Append(model.Proposal.Title)

                Dim stbMsg As New StringBuilder
                stbMsg.Append(Left(model.Notice, 50)).Append("...")

                Dim poMessage As New PushoverMessage(stbTitle.ToString, stbMsg.ToString, model.Proposal.FullUrl, "", True)
                Send(poMessage)

            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub SendNewSuccessStory(ByVal model As SuccessStory)

        If SettingsWrapper.SendPushoverNotification = True Then
            Try
                Dim stbTitle As New StringBuilder("Neue Erfolgsgeschichte in ")
                stbTitle.Append(model.Proposal.Representation.Label)

                Dim stbMsg As New StringBuilder
                stbMsg.Append(model.Id).Append(": ")
                stbMsg.Append(model.Title)

                Dim poMessage As New PushoverMessage(stbTitle.ToString, stbMsg.ToString,
                                                     model.FullUrl,
                                                     "")
                Send(poMessage)

            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub SendNewFeedback(ByVal model As Feedback)

        If SettingsWrapper.SendPushoverNotification = True Then
            Try
                Dim stbTitle As New StringBuilder()
                stbTitle.Append("Neues Feedback ")
                stbTitle.Append("[").Append(model.TypeObject.Name).Append("]")
                stbTitle.Append(" von ")
                stbTitle.Append(model.CreatedBy)

                Dim stbMsg As New StringBuilder
                stbMsg.Append(model.Title).Append(" : ").Append(model.MessageText)

                Dim stbUrl As New StringBuilder()
                stbUrl.Append("http://").Append(HttpContext.Current.Request.Url.Authority)
                stbUrl.Append("/feedback")

                Dim poMessage As New PushoverMessage(stbTitle.ToString, stbMsg.ToString, True, stbUrl.ToString)
                Send(poMessage)

            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub SendNewFeedbackComment(ByVal feedback As Feedback, comment As FeedbackComment)

        If SettingsWrapper.SendPushoverNotification = True Then
            Try
                Dim stbTitle As New StringBuilder()
                stbTitle.Append("Feedback-Kommentar auf '")
                stbTitle.Append(feedback.Title).Append("'")

                Dim stbMsg As New StringBuilder
                stbMsg.Append(comment.CommentText)

                Dim poMessage As New PushoverMessage(stbTitle.ToString, stbMsg.ToString, True)
                Send(poMessage)

            Catch ex As Exception
            End Try
        End If

    End Sub

#End Region

End Module
