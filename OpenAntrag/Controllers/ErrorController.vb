Imports System.Net
Imports System.Web.Mvc

Public Class ErrorController
    Inherits CommonController

    Public Function Index() As ActionResult
        Return InternalServerError()
    End Function

    Public Function InternalServerError() As ActionResult

        Response.TrySkipIisCustomErrors = True
        Response.StatusCode = CInt(HttpStatusCode.InternalServerError)

        Dim cei As CustomErrorInfo
        Dim message As String = "Es ist ein Server-Fehler aufgetreten"
        Dim referrerUrl As String = ""

        If Request.UrlReferrer IsNot Nothing Then
            referrerUrl = Request.UrlReferrer.AbsolutePath
        End If

        Dim ex As Exception = Server.GetLastError()
        If ex IsNot Nothing Then
            cei = New CustomErrorInfo(message,
                                      Request.UrlReferrer.AbsoluteUri,
                                      CustomErrorInfo.OriginEnum.Redirect,
                                      500,
                                      ex,
                                      RouteData.Values("controller").ToString(),
                                      RouteData.Values("action").ToString())

            PushoverManager.Send("InternalServerError", ex.Message)

        Else
            cei = New CustomErrorInfo(message, CustomErrorInfo.OriginEnum.Redirect, 500)
            cei.ReferrerUrl = referrerUrl
        End If

        Return View("Uups", cei)

    End Function

    Public Function NotFound() As ActionResult

        Response.TrySkipIisCustomErrors = True
        Response.StatusCode = CInt(HttpStatusCode.NotFound)

        Dim cei As New CustomErrorInfo("Diese Seite existiert nicht", CustomErrorInfo.OriginEnum.Redirect, 404)
        If Request.UrlReferrer IsNot Nothing Then
            cei.ReferrerUrl = Request.UrlReferrer.AbsoluteUri
        End If

        Return View("Uups", cei)

    End Function

    <Authorize>
    Public Function ErrorLog(id As String) As ActionResult

        Dim el As OpenAntrag.ErrorLog = OpenAntrag.ErrorLogs.GetById(id)

        Return View("ErrorLog", el)

    End Function

End Class