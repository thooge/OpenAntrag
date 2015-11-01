Imports System.Collections.Specialized

Public Class PushbulletMessage

    Public Params As NameValueCollection

    Public Sub New(strTitle As String,
                   strBody As String)

        Params = New NameValueCollection

        Params.Add("type", "note")
        Params.Add("title", strTitle)
        Params.Add("body", strBody)
        Params.Add("channel_tag", "openantrag")

    End Sub

    Public Sub New(strTitle As String,
                   strBody As String,
                   strUrl As String)

        Params = New NameValueCollection

        Params.Add("type", "link")
        Params.Add("url", strUrl)
        Params.Add("title", strTitle)
        Params.Add("body", strBody)
        Params.Add("channel_tag", "openantrag")

    End Sub

End Class
