Imports Raven.Client

Public Class Notifications

    Public Shared Function GetItemsPage(type As Integer,
                                        pageNo As Integer,
                                        pageCount As Integer) As List(Of Notification)

        Dim lst As New List(Of Notification)

        Using ds As IDocumentSession = DataDocumentStore.Session

            If type = -1 Then

                Dim query = ds.Query(Of Notification)() _
                            .OrderByDescending(Function(e) e.Timestamp) _                            
                            .Skip((pageNo - 1) * pageCount).Take(pageCount)

                If query.Count > 0 Then lst = query.ToList
            Else
                Dim query = ds.Query(Of Notification)() _
                            .OrderByDescending(Function(e) e.Timestamp) _
                            .Where(Function(n) n.NotificationType = type) _
                            .Skip((pageNo - 1) * pageCount).Take(pageCount)

                If query.Count > 0 Then lst = query.ToList

            End If

        End Using

        Return lst

    End Function

    Public Shared Function GetTypeId(strType As String) As Integer
        Select Case strType.ToLower
            Case "meldungen" : Return NotificationTypes.Post
            Case "antragseingänge" : Return NotificationTypes.NewProposal
            Case "antragskommentare" : Return NotificationTypes.NewProposalComment
            Case "feedback" : Return NotificationTypes.NewFeedback
            Case "feedback-kommentare" : Return NotificationTypes.NewFeedbackComment
            Case "antragsschritte" : Return NotificationTypes.NextProposalStep
            Case "erfolge" : Return NotificationTypes.SuccessStories
            Case Else : Return -1
        End Select
    End Function

    Public Shared Function GetTypeStringSingular(intType As Integer) As String
        Select Case intType
            Case NotificationTypes.Post : Return "Meldung"
            Case NotificationTypes.NewProposal : Return "Antragseingang"
            Case NotificationTypes.NewProposalComment : Return "Antragskommentar"
            Case NotificationTypes.NewFeedback : Return "Feedback"
            Case NotificationTypes.NewFeedbackComment : Return "Feedback-Kommentar"
            Case NotificationTypes.NextProposalStep : Return "Antragsschritt"
            Case NotificationTypes.SuccessStories : Return "Erfolg"
            Case Else : Return ""
        End Select
    End Function

    Public Shared Function GetTypeStringPlural(intType As Integer) As String
        Select Case intType
            Case NotificationTypes.Post : Return "Meldungen"
            Case NotificationTypes.NewProposal : Return "Antragseingänge"
            Case NotificationTypes.NewProposalComment : Return "Antragskommentare"
            Case NotificationTypes.NewFeedback : Return "Feedback"
            Case NotificationTypes.NewFeedbackComment : Return "Feedback-Kommentare"
            Case NotificationTypes.NextProposalStep : Return "Antragsschritte"
            Case NotificationTypes.SuccessStories : Return "Erfolge"
            Case Else : Return ""
        End Select
    End Function

    Public Shared Function GetTypeColor(intType As Integer) As String
        Select Case intType
            Case NotificationTypes.Post : Return "#B80000"
            Case NotificationTypes.NewProposal : Return "#E47900"
            Case NotificationTypes.NewProposalComment : Return "#CB7311"
            Case NotificationTypes.NewFeedback : Return "#3498DB"
            Case NotificationTypes.NewFeedbackComment : Return "#2D76AA"
            Case NotificationTypes.NextProposalStep : Return "#666666"
            Case NotificationTypes.SuccessStories : Return "#1C8E31"
            Case Else : Return ""
        End Select
    End Function

End Class
