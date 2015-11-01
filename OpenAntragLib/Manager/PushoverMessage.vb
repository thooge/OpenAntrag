
Imports System.Collections.Specialized

Public Class PushoverMessage

    Public Params As NameValueCollection

    Public Sub New(strUser As String,
                   strTitle As String,
                   strMessage As String)

        Params = New NameValueCollection

        Params.Add("token", SettingsWrapper.PushoverAppToken)
        Params.Add("user", strUser)
        Params.Add("title", strTitle)
        Params.Add("message", strMessage)

    End Sub

    Public Sub New(strUser As String,
                   strTitle As String,
                   strMessage As String,
                   strUrl As String,
                   strUrlTitle As String)

        Me.New(strUser, strTitle, strMessage)

        If String.IsNullOrEmpty(strUrl) = False Then
            Params.Add("url", strUrl)
        End If

        If String.IsNullOrEmpty(strUrlTitle) = False Then
            Params.Add("url_title", strUrlTitle)
        End If

    End Sub

    Public Sub New(strTitle As String,
                   strMessage As String)

        Params = New NameValueCollection

        Params.Add("token", SettingsWrapper.PushoverAppToken)
        Params.Add("user", SettingsWrapper.PushoverUserKey)
        Params.Add("title", strTitle)
        Params.Add("message", strMessage)

    End Sub

    Public Sub New(strTitle As String,
                   strMessage As String,
                   bolHighPriority As Boolean)

        Me.New(strTitle, strMessage)

        If bolHighPriority = True Then
            Params.Add("priority", "1")
        End If

    End Sub

    Public Sub New(strTitle As String,
                   strMessage As String,
                   strUrl As String,
                   strUrlTitle As String)

        Me.New(strTitle, strMessage)

        If String.IsNullOrEmpty(strUrl) = False Then
            Params.Add("url", strUrl)
        End If

        If String.IsNullOrEmpty(strUrlTitle) = False Then
            Params.Add("url_title", strUrlTitle)
        End If

    End Sub

    Public Sub New(strTitle As String,
               strMessage As String,
               strUrl As String,
               strUrlTitle As String,
               bolHighPriority As Boolean)

        Me.New(strTitle, strMessage, strUrl, strUrlTitle)

        If bolHighPriority = True Then
            Params.Add("priority", "1")
        End If

    End Sub

End Class
