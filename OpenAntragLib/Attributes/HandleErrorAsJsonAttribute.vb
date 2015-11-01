Imports System.Web.Mvc

Public Class HandleErrorAsJsonAttribute
    Inherits FilterAttribute
    Implements IExceptionFilter

    Public Sub OnException(filterContext As ExceptionContext) Implements IExceptionFilter.OnException

        filterContext.ExceptionHandled = True
        filterContext.Result = New JsonResult() With {
            .Data = New With {
                .success = False,
                .error = filterContext.Exception.Message
            },
            .JsonRequestBehavior = JsonRequestBehavior.AllowGet
        }

    End Sub

End Class
