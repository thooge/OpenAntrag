Imports System.Web.Mvc

Public Class CommonController
    Inherits System.Web.Mvc.Controller

    <HandleErrorAsJson()>
    Public Function GetPartial(namepart As String) As JsonResult

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = Me.RenderPartialViewToString("_" & namepart & "Partial")}
        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function GetPartialModel(namepart As String, model As Object) As JsonResult

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = Me.RenderPartialViewToString("_" & namepart & "Partial", model)}
        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function GetErrorHtml(strErrorMessage As String) As JsonResult

        Dim cei As New CustomErrorInfo With {.ErrorMessage = strErrorMessage}
        Dim strHtml As String = Me.RenderPartialViewToString("_ErrorBoxPartial", cei)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = False, .errorHtml = strHtml}
        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function GetHash(strValue As String) As JsonResult

        Dim strHash As String = Tools.GetMd5(strValue)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = strHash}
        Return jr

    End Function

End Class
