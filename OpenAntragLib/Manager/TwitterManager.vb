Imports LinqToTwitter
Imports System.Text

Public Class TwitterWrapper

    Private ReadOnly _Context As TwitterContext
    Public ReadOnly Property Context As TwitterContext
        Get
            Return _Context
        End Get
    End Property

    Public Sub New()

        Dim auth As New LinqToTwitter.SingleUserAuthorizer With {
                .Credentials = New InMemoryCredentials With {
                .ConsumerKey = SettingsWrapper.TwitterConsumerKey,
                .ConsumerSecret = SettingsWrapper.TwitterConsumerSecret,
                .AccessToken = SettingsWrapper.TwitterAccessTokenSecret,
                .OAuthToken = SettingsWrapper.TwitterAccessToken}}

        _Context = New TwitterContext(auth)

    End Sub

End Class

Public Module TwitterManager

    Public Sub TweetNewProposal(ByVal model As Proposal)

        If SettingsWrapper.SendTweets = True And model.IsTest = False Then
            Try
                Dim stb As New StringBuilder
                If String.IsNullOrEmpty(model.Representation.Twitter) = False Then
                    stb.Append("@").Append(model.Representation.Twitter).Append(" ")
                End If
                stb.Append("Neuer Antrag für ")
                stb.Append("#").Append(model.Representation.Key)
                stb.Append(" : ").Append(model.Title)
                stb.Append(" - ").Append(model.ShortUrl)

                Dim tw As New TwitterWrapper
                tw.Context.UpdateStatus(stb.ToString)

            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub TweetNewPost(ByVal strTitle As String, strText As String)

        If SettingsWrapper.SendTweets = True Then
            Try
                Dim stb As New StringBuilder
                stb.Append(strTitle)
                stb.Append(" - ").Append(strText)

                Dim tw As New TwitterWrapper
                tw.Context.UpdateStatus(stb.CutEllipsis(140))

            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub TweetNewSuccessStory(ByVal model As SuccessStory)

        If SettingsWrapper.SendTweets = True And model.Proposal.IsTest = False Then
            Try
                Dim stb As New StringBuilder
                stb.Append("Erfolgsgeschichte in ")
                stb.Append("#").Append(model.Proposal.Representation.Key)
                stb.Append(" : ").Append(model.Title)
                stb.Append(" - ").Append(model.ShortUrl)

                Dim tw As New TwitterWrapper
                tw.Context.UpdateStatus(stb.ToString)

            Catch ex As Exception
            End Try
        End If

    End Sub

End Module
