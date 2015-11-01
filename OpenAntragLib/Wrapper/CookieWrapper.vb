Imports System.Web

Public Class CookieWrapper

    Public Overloads Shared Function GetCookie(ByVal strName As String) As String

        If Not HttpContext.Current.Request.Cookies(strName) Is Nothing Then
            Return HttpContext.Current.Request.Cookies(strName).Value
        Else
            Return Nothing
        End If

    End Function

    Public Overloads Shared Function GetCookie(ByVal strName As String, ByVal strDefaultValue As String) As String

        If HttpContext.Current.Request.Cookies(strName) Is Nothing Then
            Call SetCookie(strName, strDefaultValue, 1)
        End If

        Return HttpContext.Current.Request.Cookies(strName).Value

    End Function

    Public Shared Sub SetCookie(ByVal strName As String,
                                ByVal strValue As String,
                                Optional ByVal intExpiresInDays As Integer = 1)

        Dim oCookie As New HttpCookie(strName, strValue)
        oCookie.Expires = DateTime.Now.AddDays(1)
        HttpContext.Current.Response.Cookies.Add(oCookie)

    End Sub

    Public Shared Sub RemoveCookie(ByVal strName As String)

        Dim oCookie As HttpCookie = New HttpCookie(strName)
        oCookie.Expires = DateTime.Now.AddDays(-1D)
        HttpContext.Current.Response.Cookies.Add(oCookie)

    End Sub

End Class
