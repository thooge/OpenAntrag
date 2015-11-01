Imports ActionMailer.Net.Mvc

Public Class MailController
    Inherits MailerBase

    Public Function ResetPassword(model As ResetPasswordModel) As EmailResult

        Me.To.Add(model.MailAddress)
        Me.From = SettingsWrapper.MailSender
        Me.Subject = "Dein neues Passwort für OpenAntrag"

        Return Email("ResetPasswordEmail", model)

    End Function

    Public Function NewProposal(model As Proposal) As EmailResult

        Dim arrMailAdresses As String() = model.Representation.InfoMail.Split(";")
        For Each s As String In arrMailAdresses
            Me.To.Add(s)
        Next

        If SettingsWrapper.InfoMailCC.Length > 0 Then
            Me.BCC.Add(SettingsWrapper.InfoMailCC)
        End If

        Me.From = SettingsWrapper.MailSender
        Me.Subject = "Ein neuer OpenAntrag ist eingegangen"

        Return Email("NewProposalEmail", model)

    End Function

    Public Function NewProposalComment(model As ProposalComment, prop As Proposal) As EmailResult

        Dim arrMailAdresses As String() = prop.Representation.InfoMail.Split(";")
        For Each s As String In arrMailAdresses
            Me.To.Add(s)
        Next

        If SettingsWrapper.InfoMailCC.Length > 0 Then
            Me.BCC.Add(SettingsWrapper.InfoMailCC)
        End If

        Me.From = SettingsWrapper.MailSender

        Dim strSubject As String = String.Concat("Ein neuer OpenAntrag-Kommentar für '", prop.Title, "' ist eingegangen")
        Me.Subject = strSubject

        ViewData("Subject") = strSubject
        Return Email("NewProposalCommentEmail", model)

    End Function

    Public Function NewProposalAbuseNotice(model As ProposalAbuseNotice) As EmailResult

        Dim arrMailAdresses As String() = model.Proposal.Representation.InfoMail.Split(";")
        For Each s As String In arrMailAdresses
            Me.To.Add(s)
        Next

        If SettingsWrapper.InfoMailCC.Length > 0 Then
            Me.BCC.Add(SettingsWrapper.InfoMailCC)
        End If

        Me.From = SettingsWrapper.MailSender
        Me.Subject = "ACHTUNG: Eine OpenAntrag-Missbrauchsmeldung!"

        Return Email("NewProposalAbuseNoticeEmail", model)

    End Function

End Class