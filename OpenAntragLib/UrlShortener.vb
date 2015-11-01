Imports System.Net
Imports System.Text

Public Class UrlShortener

    Public Shared Function GetShortUrl(strFullUrl As String) As String

        Const strRequestUrl As String = "http://pirat.ly/shortener/api/createplain/{ApiKey}/?{Url}"        

        Dim strRetVal As String = ""

        Dim stbUrl As New StringBuilder(strRequestUrl)
        stbUrl.Replace("{ApiKey}", SettingsWrapper.PiratlyApiKey)
        stbUrl.Replace("{Url}", strFullUrl)

        Try
            Dim req As HttpWebRequest = HttpWebRequest.Create(stbUrl.ToString)
            req.Method = "GET"
            req.ContentType = "text/xml; encoding='utf-8'"
            req.Timeout = 1000

            Dim res As HttpWebResponse = req.GetResponse()
            Dim rd As New IO.StreamReader(res.GetResponseStream())
            strRetVal = rd.ReadToEnd

            If strRetVal.StartsWith("http://pirat.ly") = False Then strRetVal = ""

        Catch ex As Exception
            'TODO: GetShortUrl-Errors
        End Try

        Return strRetVal

    End Function

End Class
