Imports ActionMailer.Net.Mvc

Public Module MailManager

    Public Sub SendResetPasswordToUser(model As ResetPasswordModel)

        If SettingsWrapper.SendMail = True Then
            Try
                If Not String.IsNullOrEmpty(model.MailAddress) Then
                    Dim mc As New MailController
                    Dim result As EmailResult = mc.ResetPassword(model)
                    result.Deliver()
                End If
            Catch ex As Exception
                PushoverManager.SendError("SendResetPasswordToUser", ex)
            End Try
        End If

    End Sub

    Public Sub SendNewProposal(model As Proposal)

        If SettingsWrapper.SendMail = True Then
            Try
                If Not String.IsNullOrEmpty(model.Representation.InfoMail) Then
                    Dim mc As New MailController
                    Dim result As EmailResult = mc.NewProposal(model)
                    result.Deliver()
                End If
            Catch ex As Exception
                PushoverManager.SendError("SendNewProposal", ex)
            End Try
        End If

    End Sub

    Public Sub SendNewProposalComment(model As ProposalComment, prop As Proposal)

        If SettingsWrapper.SendMail = True Then
            Try
                If Not String.IsNullOrEmpty(prop.Representation.InfoMail) Then
                    Dim mc As New MailController
                    Dim result As EmailResult = mc.NewProposalComment(model, prop)
                    result.Deliver()
                End If
            Catch ex As Exception
                PushoverManager.SendError("SendNewProposalComment", ex)
            End Try
        End If

    End Sub

    Public Sub SendNewProposalAbuseNotice(model As ProposalAbuseNotice)

        If SettingsWrapper.SendMail = True Then
            Try
                If Not String.IsNullOrEmpty(model.Proposal.Representation.InfoMail) Then
                    Dim mc As New MailController
                    Dim result As EmailResult = mc.NewProposalAbuseNotice(model)
                    result.Deliver()
                End If
            Catch ex As Exception
                PushoverManager.SendError("SendNewProposalAbuseNotice", ex)
            End Try
        End If

    End Sub

End Module
