Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Web.Mvc

''' <summary>
''' Controller extension class that adds controller methods
''' to render a partial view and return the result as string.
''' http://blog.janjonas.net/2011-06-18/aspnet-mvc3-controller-extension-method-render-partial-view-string
''' Based on http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
''' </summary>
Public Module ControllerExtensions

    ''' <summary>
    ''' Renders a (partial) view to string.
    ''' </summary>
    ''' <param name="controller">Controller to extend</param>
    ''' <param name="viewName">(Partial) view to render</param>
    ''' <returns>Rendered (partial) view as string</returns>
    <Extension()> _
    Public Function RenderPartialViewToString(controller As Controller, viewName As String) As String
        Return controller.RenderPartialViewToString(viewName, Nothing)
    End Function

    ''' <summary>
    ''' Renders a (partial) view to string.
    ''' </summary>
    ''' <param name="controller">Controller to extend</param>
    ''' <param name="viewName">(Partial) view to render</param>
    ''' <param name="model">Model</param>
    ''' <returns>Rendered (partial) view as string</returns>
    <Extension()> _
    Public Function RenderPartialViewToString(controller As Controller, viewName As String, model As Object) As String

        If String.IsNullOrEmpty(viewName) Then
            viewName = controller.ControllerContext.RouteData.GetRequiredString("action")
        End If

        controller.ViewData.Model = model

        Dim strRetVal As String = ""

        Using sw = New StringWriter()
            Try
                Dim viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName)

                Dim viewContext = New ViewContext(controller.ControllerContext,
                                                  viewResult.View,
                                                  controller.ViewData,
                                                  controller.TempData,
                                                  sw)

                viewResult.View.Render(viewContext, sw)                

                strRetVal = sw.GetStringBuilder().ToString().CleanHtmlCode

            Catch ex As Exception
                'Throw New Exception("Fehler beim Rendern vom " & viewName)

                Dim cei As New CustomErrorInfo With {.ErrorMessage = "Fehler bei der Darstellung von '" & viewName & "'"}
                strRetVal = controller.RenderPartialViewToString("_ErrorBoxPartial", cei)

            End Try
        End Using

        Return strRetVal

    End Function

End Module
