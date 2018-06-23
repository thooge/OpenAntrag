Imports System.Runtime.CompilerServices
Imports MarkdownDeep
Imports System.Web
Imports System.Web.Mvc

'http://blog.dantup.com/2011/03/an-asp-net-mvc-htmlhelper-extension-method-for-markdown-using-markdownsharp

Public Module MarkdownHelper

    ''' <summary>
    ''' Transforms a string of Markdown into HTML.
    ''' </summary>
    ''' <param name="text">The Markdown that should be transformed.</param>
    ''' <returns>The HTML representation of the supplied Markdown.</returns>
    Public Function Markdown(ByVal text As String,
                             Optional ByVal bolSafeMode As Boolean = True) As IHtmlString
        Dim md = New Markdown With {
            .ExtraMode = True,
            .SafeMode = bolSafeMode,
            .NewWindowForExternalLinks = True
        }

        Dim html As String = md.Transform(text)

        Return New MvcHtmlString(html)

    End Function

    ''' <summary>
    ''' Transforms a string of Markdown into Plain Text
    ''' </summary>
    ''' <param name="text"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MarkdownText(text As String) As String

        Dim str As String = Nothing

        Try
            Dim md = New Markdown()
            md.SummaryLength = -1
            str = md.Transform(text)
        Catch ex As Exception
        End Try

        Return str

    End Function

    ''' <summary>
    ''' Transforms a string of Markdown into HTML.
    ''' </summary>
    ''' <param name="helper">HtmlHelper - Not used, but required to make this an extension method.</param>
    ''' <param name="text">The Markdown that should be transformed.</param>
    ''' <returns>The HTML representation of the supplied Markdown.</returns>
    <Extension()> _
    Public Function Markdown(helper As HtmlHelper, text As String) As MvcHtmlString
        Return Markdown(text)
    End Function

End Module
