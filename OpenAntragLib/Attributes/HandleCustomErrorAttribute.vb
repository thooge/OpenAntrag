Imports System.Collections.Generic
Imports System.Web
Imports System.Web.Mvc

Public Class HandleCustomErrorAttribute
    Inherits HandleErrorAttribute

    Public Overrides Sub OnException(filterContext As System.Web.Mvc.ExceptionContext)

        If filterContext.ExceptionHandled OrElse Not filterContext.HttpContext.IsCustomErrorEnabled Then Return
        If New HttpException(Nothing, filterContext.Exception).GetHttpCode() <> 500 Then Return
        If Not ExceptionType.IsInstanceOfType(filterContext.Exception) Then Return

        Dim log As New ErrorLog()

        log.AbsoluteUri = filterContext.HttpContext.Request.Url.AbsoluteUri
        log.Controller = filterContext.RouteData.Values("controller").ToString()
        log.Action = filterContext.RouteData.Values("action").ToString()
        log.RequestType = filterContext.HttpContext.Request.RequestType
        log.Message = filterContext.Exception.Message

        log.AjaxCall = (filterContext.HttpContext.Request.Headers("X-Requested-With") IsNot Nothing AndAlso
                        filterContext.HttpContext.Request.Headers("X-Requested-With") = "XMLHttpRequest")

        Dim userAgent As String = filterContext.HttpContext.Request.UserAgent

        For Each kv As KeyValuePair(Of String, Object) In filterContext.RouteData.Values
            log.Parameter.Add(String.Concat(kv.Key, ": ", kv.Value))
        Next

        If filterContext.HttpContext.Request.UrlReferrer IsNot Nothing Then
            log.ReferrerUrl = filterContext.HttpContext.Request.UrlReferrer.AbsolutePath
        End If

        If log.AjaxCall Then

            'JSON, wenn [HandleCustomErrorAttribute] nicht gesetzt ist

            filterContext.Result = New JsonResult() With {
                .Data = New With {
                    .success = False,
                    .error = filterContext.Exception.Message
                },
                .JsonRequestBehavior = JsonRequestBehavior.AllowGet
            }

        Else

            'STANDARD

            Dim cei As New CustomErrorInfo(log.Message, log.ReferrerUrl, CustomErrorInfo.OriginEnum.Exception, -1, filterContext.Exception, log.Controller, _
                log.Action)

            filterContext.Result = New ViewResult() With {
                .ViewName = Me.View, _
                .MasterName = Master, _
                .ViewData = New ViewDataDictionary(Of HandleErrorInfo)(cei),
                .TempData = filterContext.Controller.TempData _
            }

        End If

        If TypeOf filterContext.Exception Is CustomException = False Then
            ErrorLogs.NewLog(log, userAgent)
        End If

        filterContext.ExceptionHandled = True
        filterContext.HttpContext.Response.Clear()
        filterContext.HttpContext.Response.StatusCode = 500

        filterContext.HttpContext.Response.TrySkipIisCustomErrors = True

    End Sub

    'Public Overrides Sub OnException(filterContext As System.Web.Mvc.ExceptionContext)

    '    If filterContext.ExceptionHandled OrElse Not filterContext.HttpContext.IsCustomErrorEnabled Then Return
    '    If New HttpException(Nothing, filterContext.Exception).GetHttpCode() <> 500 Then Return
    '    If Not ExceptionType.IsInstanceOfType(filterContext.Exception) Then Return

    '    Dim controllerName As String = filterContext.RouteData.Values("controller").ToString()
    '    Dim actionName As String = filterContext.RouteData.Values("action").ToString()
    '    Dim requestType As String = filterContext.HttpContext.Request.RequestType
    '    Dim absoluteUri As String = filterContext.HttpContext.Request.Url.AbsoluteUri
    '    Dim userAgent As String = filterContext.HttpContext.Request.UserAgent

    '    Dim ajaxCall As Boolean = (filterContext.HttpContext.Request.Headers("X-Requested-With") IsNot Nothing AndAlso
    '                               filterContext.HttpContext.Request.Headers("X-Requested-With") = "XMLHttpRequest")

    '    Dim stbValues As New StringBuilder()
    '    stbValues.Append("requestType").Append(": ").Append(String.Concat(requestType, If((ajaxCall = True), " (AJAX)", ""))).Append(vbLf)
    '    stbValues.Append("userAgent").Append(": ").Append(userAgent).Append(vbLf)
    '    For Each kv As KeyValuePair(Of String, Object) In filterContext.RouteData.Values
    '        stbValues.Append(kv.Key).Append(": ").Append(kv.Value.ToString()).Append(vbLf)
    '    Next

    '    Dim referrerUrl As String = ""
    '    If filterContext.HttpContext.Request.UrlReferrer IsNot Nothing Then
    '        referrerUrl = filterContext.HttpContext.Request.UrlReferrer.AbsolutePath
    '    End If

    '    If ajaxCall Then

    '        'JSON, wenn [HandleErrorAsJsonAttribute] nicht gesetzt ist

    '        filterContext.Result = New JsonResult() With {
    '            .Data = New With {
    '                .success = False,
    '                .error = filterContext.Exception.Message
    '            },
    '            .JsonRequestBehavior = JsonRequestBehavior.AllowGet
    '        }
    '    Else

    '        'STANDARD

    '        Dim cei As New CustomErrorInfo(filterContext.Exception.Message,
    '                                       referrerUrl,
    '                                       CustomErrorInfo.OriginEnum.Exception,
    '                                       -1,
    '                                       filterContext.Exception, controllerName,
    '                                       actionName)

    '        filterContext.Result = New ViewResult() With {
    '            .ViewName = Me.View, _
    '            .MasterName = Master, _
    '            .ViewData = New ViewDataDictionary(Of HandleErrorInfo)(cei),
    '            .TempData = filterContext.Controller.TempData _
    '        }
    '    End If

    '    'TODO: Logging

    '    If TypeOf filterContext.Exception Is CustomException = False Then
    '        PushoverManager.Send(
    '            New PushoverMessage(String.Concat("Exception auf ", absoluteUri),
    '                                String.Concat(stbValues, filterContext.Exception.Message),
    '                                referrerUrl, referrerUrl, True))
    '    End If

    '    filterContext.ExceptionHandled = True
    '    filterContext.HttpContext.Response.Clear()
    '    filterContext.HttpContext.Response.StatusCode = 500

    '    filterContext.HttpContext.Response.TrySkipIisCustomErrors = True
    'End Sub

End Class
