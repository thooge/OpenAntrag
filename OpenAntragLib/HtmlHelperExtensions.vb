Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Web.Mvc
Imports System.Web.Mvc.Html

Public Module HtmlHelperExtensions

    <Extension()>
    Public Sub RenderPartialWithData(htmlHelper As HtmlHelper,
                                     partialViewName As String,
                                     model As Object,
                                     viewData As Object)

        Dim viewDataDictionary = New ViewDataDictionary()

        If viewData IsNot Nothing Then
            For Each prop As PropertyDescriptor In TypeDescriptor.GetProperties(viewData)
                Dim val As Object = prop.GetValue(viewData)
                viewDataDictionary(prop.Name) = val
            Next
        End If

        htmlHelper.RenderPartial(partialViewName, model, viewDataDictionary)

    End Sub

End Module
