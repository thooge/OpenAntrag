Imports System.Net
Imports System.Text
Imports System.Web

Public Module PushbulletManager

    'https://docs.pushbullet.com/http/

    Public Sub Send(pb As PushbulletMessage)

        Try
            Using webClient As New WebClient

                Dim strToken As String = SettingsWrapper.PushbulletAccessToken
                webClient.Headers(HttpRequestHeader.Authorization) = String.Format("Bearer {0}", strToken)

                webClient.UploadValues(New Uri(SettingsWrapper.PushbulletApiUrl), pb.Params)
            End Using
        Catch ex As Exception
        End Try

    End Sub

    Public Sub SendNewProposal(ByVal model As Proposal)

        Try
            Dim stbTitle As New StringBuilder("Neuer Antrag in ")
            stbTitle.Append(model.Representation.Label)

            Dim stbMsg As New StringBuilder
            stbMsg.Append(model.Id).Append(": ")
            stbMsg.Append(model.Title)

            Dim pb As New PushbulletMessage(stbTitle.ToString, stbMsg.ToString, model.FullUrl)
            Send(pb)

        Catch ex As Exception
        End Try

    End Sub

    Public Sub SendNewSuccessStory(ByVal model As SuccessStory)

        Try
            Dim stbTitle As New StringBuilder("Neue Erfolgsgeschichte in ")
            stbTitle.Append(model.Proposal.Representation.Label)

            Dim stbMsg As New StringBuilder
            stbMsg.Append(model.Id).Append(": ")
            stbMsg.Append(model.Title)

            Dim pb As New PushbulletMessage(stbTitle.ToString, stbMsg.ToString, model.FullUrl)
            Send(pb)

        Catch ex As Exception
        End Try

    End Sub

    Public Sub SendNewFeedback(ByVal model As Feedback)

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

            Dim pb As New PushbulletMessage(stbTitle.ToString, stbMsg.ToString, stbUrl.ToString)
            Send(pb)

        Catch ex As Exception
        End Try

    End Sub

    Public Sub SendNewPost(ByVal strTitle As String, strText As String)

        Try
            Dim stb As New StringBuilder
            stb.Append(strTitle)
            stb.Append(" - ").Append(strText)

            Dim pb As New PushbulletMessage(strTitle, strText)
            Send(pb)

        Catch ex As Exception
        End Try

    End Sub

End Module
