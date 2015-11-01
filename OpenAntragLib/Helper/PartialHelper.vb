Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Web.Mvc
Imports System.Web.Mvc.Html
Imports System.Web.WebPages
Imports System.IO
Imports System.Globalization
Imports System.Runtime.CompilerServices

Public Module PartialExtensions

    'http://aspnet.codeplex.com/workitem/8872
    'https://github.com/q42jaap/PartialMagic.Mvc/blob/master/PartialMagic.Mvc/PartialExtensions.cs

    ' copied from HtmlHelper.FindPartialView because it's original is internal
    Public Function FindPartialView(viewContext As ViewContext,
                                    partialViewName As String,
                                    viewEngineCollection As ViewEngineCollection) As IView

        Dim result As ViewEngineResult = viewEngineCollection.FindPartialView(viewContext, partialViewName)

        If result.View IsNot Nothing Then
            Return result.View
        End If

        Dim builder As New StringBuilder()

        For Each str As String In result.SearchedLocations
            builder.AppendLine()
            builder.Append(str)
        Next

        Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "The partial view '{0}' was not found or no view engine supports the searched locations. The following locations were searched:{1}", New Object() {partialViewName, builder}))

    End Function

    ' copied from HtmlHelper.RenderPartialInternal (thanks Red Gate!)
    ' modified the logics to OrNull functionality
    Public Sub RenderPartialInternalOrNull(htmlHelper As HtmlHelper,
                                           partialViewName As String,
                                           viewData As ViewDataDictionary,
                                           model As Object,
                                           writer As TextWriter,
                                           viewEngineCollection As ViewEngineCollection)

        If String.IsNullOrEmpty(partialViewName) Then
            Throw New ArgumentNullException("partialViewName")
        End If
        Dim dictionary As New ViewDataDictionary(If(viewData, htmlHelper.ViewData))

        ' we explicitly set the model here so we don't get the current Model as fallback.
        dictionary.Model = model

        Dim viewContext As New ViewContext(htmlHelper.ViewContext, htmlHelper.ViewContext.View, dictionary, htmlHelper.ViewContext.TempData, writer)
        FindPartialView(viewContext, partialViewName, viewEngineCollection).Render(viewContext, writer)

    End Sub

    ''' <summary>
    ''' Renders the specified partial view as an HTML-encoded string (even if the model is null).
    ''' This is a safer method than the normal HtmlHelper.Partial() because that one will use the current Model as a fallback when the given model is null.
    ''' </summary>
    ''' <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    ''' <param name="partialViewName">The name of the partial view to render.</param>
    ''' <param name="model">The model for the partial view, may be null</param>
    ''' <param name="viewData">A new dictionary or null (in which case the current view data is used as a fallback)</param>
    ''' <returns></returns>
    <Extension> _
    Public Function PartialOrNull(htmlHelper As HtmlHelper, partialViewName As String, model As Object, Optional viewData As ViewDataDictionary = Nothing) As MvcHtmlString
        Using writer As New StringWriter(CultureInfo.CurrentCulture)
            RenderPartialInternalOrNull(htmlHelper, partialViewName, viewData, model, writer, ViewEngines.Engines)
            Return MvcHtmlString.Create(writer.ToString())
        End Using
    End Function

    ''' <summary>
    ''' Renders the specified partial view as an HTML-encoded string unless the model is null.
    ''' Note that the partial is not exectued when the model is null.
    ''' </summary>
    ''' <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    ''' <param name="partialViewName">The name of the partial view to render.</param>
    ''' <param name="model">The model for the partial view, may be null</param>
    ''' <param name="viewData">A new dictionary or null (in which case the current view data is used as a fallback)</param>
    ''' <returns>The partial view that is rendered as an HTML-encoded string or null if the model is null.</returns>
    <Extension()>
    Public Function PartialOrDiscard(htmlHelper As HtmlHelper, partialViewName As String, model As Object, Optional viewData As ViewDataDictionary = Nothing) As MvcHtmlString
        If model Is Nothing Then
            Return Nothing
        End If
        Return htmlHelper.[Partial](partialViewName, model, viewData)
    End Function

    ''' <summary>
    ''' Renders the specified partial view as an HTML-encoded string unless the model is null.
    ''' The given wrapper is used to wrap around the output of the partial result, it uses the Razor @item to place the partial output within the wrapper's template.
    ''' Note the partial and the wrapper are not exectued when the model is null.
    ''' </summary>
    ''' <remarks>
    ''' Note that the partial is rendered before wrapper is executed (should there be side-effects in either of them).
    ''' </remarks>
    ''' <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    ''' <param name="partialViewName">The name of the partial view to render.</param>
    ''' <param name="model">The model for the partial view, may be null</param>
    ''' <param name="wrapper">This wrapper is excuted when the model is not null, use @item to render the output of the partial</param>
    ''' <param name="viewData">A new dictionary or null (in which case the current view data is used as a fallback)</param>
    ''' <returns>The partial view that is rendered as an HTML-encoded string or null if the model is null.</returns>
    <Extension()>
    Public Function PartialOrDiscard(htmlHelper As HtmlHelper,
                                     partialViewName As String,
                                     model As Object,
                                     wrapper As Func(Of MvcHtmlString, HelperResult),
                                     Optional viewData As ViewDataDictionary = Nothing) As HelperResult

        If model Is Nothing Then
            Return Nothing
        End If

        'krze: 'Function' geändert zu 'Sub' !?
        Return New HelperResult(Sub(writer)
                                    Dim partialResult = htmlHelper.[Partial](partialViewName, model, viewData)
                                    wrapper(partialResult).WriteTo(writer)
                                End Sub)
    End Function

    ''' <summary>
    ''' Renders the specified partial view as an HTML-encoded string unless the model is null or empty.
    ''' Note that the partial is not exectued when the model is null or empty.
    ''' </summary>
    ''' <remarks>
    ''' The enumerable passed into the model is checked with Any() to see whether it is empty.
    ''' In case of a LINQ query this may cause the enumerable to be called twice (once for the Any() check and once propably inside the partial).
    ''' </remarks>
    ''' <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    ''' <param name="partialViewName">The name of the partial view to render.</param>
    ''' <param name="model">The model for the partial view, may be null or an empty enumerable</param>
    ''' <param name="viewData">A new dictionary or null (in which case the current view data is used as a fallback)</param>
    ''' <returns>The partial view that is rendered as an HTML-encoded string or null if the model is null or empty.</returns>
    <Extension()>
    Public Function PartialOrDiscardIfEmpty(htmlHelper As HtmlHelper, partialViewName As String, model As IEnumerable(Of Object), Optional viewData As ViewDataDictionary = Nothing) As MvcHtmlString
        If model Is Nothing OrElse Not model.Any() Then
            Return Nothing
        End If
        Return htmlHelper.[Partial](partialViewName, model, viewData)
    End Function

    ''' <summary>
    ''' Renders the specified partial view as an HTML-encoded string unless the model is null or empty.<br/>
    ''' The given wrapper is used to wrap around the output of the partial result, it uses the Razor @item to place the partial output within the wrapper's template.
    ''' Note the partial and the wrapper are not exectued when the model is null or empty.
    ''' </summary>
    ''' <remarks>
    ''' The enumerable passed into the model is checked with Any() to see whether it is empty.
    ''' In case of a LINQ query this may cause the enumerable to be called twice (once for the Any() check and once propably inside the partial).
    ''' Note that the partial is rendered before wrapper is executed (should there be side-effects in either of them).
    ''' </remarks>
    ''' <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    ''' <param name="partialViewName">The name of the partial view to render.</param>
    ''' <param name="model">The model for the partial view, may be null or an empty enumerable</param>
    ''' <param name="wrapper">This wrapper is excuted when the model is not null or empty, use @item to render the output of the partial</param>
    ''' <param name="viewData">A new dictionary or null (in which case the current view data is used as a fallback)</param>
    ''' <returns>The partial view that is rendered as an HTML-encoded string or null if the model is null or empty.</returns>
    <Extension()>
    Public Function PartialOrDiscardIfEmpty(htmlHelper As HtmlHelper,
                                            partialViewName As String,
                                            model As IEnumerable(Of Object),
                                            wrapper As Func(Of MvcHtmlString, HelperResult),
                                            Optional viewData As ViewDataDictionary = Nothing) As HelperResult

        If model Is Nothing OrElse Not model.Any() Then
            Return Nothing
        End If

        'krze: 'Function' geändert zu 'Sub' !?
        Return New HelperResult(Sub(writer)
                                    Dim partialResult = htmlHelper.[Partial](partialViewName, model, viewData)
                                    wrapper(partialResult).WriteTo(writer)
                                End Sub)
    End Function

End Module
