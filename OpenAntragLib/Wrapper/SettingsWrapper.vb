Imports System.Web.Configuration.WebConfigurationManager

Public Class SettingsWrapper

#Region "Properties: AppSettings"

    Public Shared ReadOnly Property PushbulletApiUrl() As String
        Get
            Return GetFromSettings(Of String)("PushbulletApiUrl")
        End Get
    End Property

    Public Shared ReadOnly Property PushbulletAccessToken() As String
        Get
            Return GetFromSettings(Of String)("PushbulletAccessToken")
        End Get
    End Property

    Public Shared ReadOnly Property PushoverApiUrl() As String
        Get
            Return GetFromSettings(Of String)("PushoverApiUrl")
        End Get
    End Property

    Public Shared ReadOnly Property PushoverAppToken() As String
        Get
            Return GetFromSettings(Of String)("PushoverAppToken")
        End Get
    End Property

    Public Shared ReadOnly Property PushoverUserKey() As String
        Get
            Return GetFromSettings(Of String)("PushoverUserKey")
        End Get
    End Property

    Public Shared ReadOnly Property TwitterConsumerKey() As String
        Get
            Return GetFromSettings(Of String)("TwitterConsumerKey")
        End Get
    End Property

    Public Shared ReadOnly Property TwitterConsumerSecret() As String
        Get
            Return GetFromSettings(Of String)("TwitterConsumerSecret")
        End Get
    End Property

    Public Shared ReadOnly Property TwitterAccessToken() As String
        Get
            Return GetFromSettings(Of String)("TwitterAccessToken")
        End Get
    End Property

    Public Shared ReadOnly Property TwitterAccessTokenSecret() As String
        Get
            Return GetFromSettings(Of String)("TwitterAccessTokenSecret")
        End Get
    End Property

    Public Shared ReadOnly Property TwitterRequestUrl() As String
        Get
            Return GetFromSettings(Of String)("TwitterRequestUrl")
        End Get
    End Property

    Public Shared ReadOnly Property PiratlyApiKey() As String
        Get
            Return GetFromSettings(Of String)("PiratlyApiKey")
        End Get
    End Property

    Public Shared ReadOnly Property SendMail As Boolean
        Get
            Return GetFromSettings(Of Boolean)("SendMail")
        End Get
    End Property

    Public Shared ReadOnly Property InfoMailCC() As String
        Get
            Return GetFromSettings(Of String)("InfoMailCC")
        End Get
    End Property

    Public Shared ReadOnly Property MailSender() As String
        Get
            Return GetFromSettings(Of String)("MailSender")
        End Get
    End Property

    Public Shared ReadOnly Property SendTweets As Boolean
        Get
            Return GetFromSettings(Of Boolean)("SendTweets")
        End Get
    End Property

    Public Shared ReadOnly Property SendPushoverNotification As Boolean
        Get
            Return GetFromSettings(Of Boolean)("SendPushoverNotification")
        End Get
    End Property

    Public Shared ReadOnly Property DefaultPagerListPageSize() As Integer
        Get
            Return GetFromSettings(Of Integer)("DefaultPagerListPageSize")
        End Get
    End Property

    Public Shared ReadOnly Property DefaultPagerListWingLength() As Integer
        Get
            Return GetFromSettings(Of Integer)("DefaultPagerListWingLength")
        End Get
    End Property

    Public Shared ReadOnly Property RandomKey_AllowCapitalLetters() As Boolean
        Get
            Return GetFromSettings(Of Boolean)("RandomKey_AllowCapitalLetters")
        End Get
    End Property

    Public Shared ReadOnly Property RandomKey_Letters() As String
        Get
            Return GetFromSettings(Of String)("RandomKey_Letters")
        End Get
    End Property

    Public Shared ReadOnly Property RandomKey_Numbers() As String
        Get
            Return GetFromSettings(Of String)("RandomKey_Numbers")
        End Get
    End Property

    Public Shared ReadOnly Property NotificationPageCount() As Integer
        Get
            Return GetFromSettings(Of Integer)("NotificationPageCount")
        End Get
    End Property

    Public Shared ReadOnly Property ProposalListCount() As Integer
        Get
            Return GetFromSettings(Of Integer)("ProposalListCount")
        End Get
    End Property

#End Region

#Region "Properties: Global"

    Public Shared ReadOnly Property GetSetting(ByVal strKey As String) As String
        Get
            Return GetFromSettings(Of String)(strKey)
        End Get
    End Property

#End Region

#Region "Methoden"

    Private Shared Function GetFromSettings(Of T)(ByVal strKey As String) As T

        Dim obj As Object = AppSettings(strKey)
        If (obj Is Nothing) Then
            Return Nothing
        Else
            Return CType(obj, T)
        End If

    End Function

#End Region

End Class
