Imports System.Runtime.CompilerServices
Imports System.Web.Mvc
Imports System.Text

Public Module TagBuilderExtension

    <Extension()>
    Public Sub Append(tb As TagBuilder, value As String)

        Dim stb As New StringBuilder(tb.InnerHtml)
        stb.Append(value)
        tb.InnerHtml = stb.ToString

    End Sub

    <Extension()>
    Public Sub Append(tb As TagBuilder, value As TagBuilder)

        Dim stb As New StringBuilder(tb.InnerHtml)
        stb.Append(value.ToString)
        tb.InnerHtml = stb.ToString

    End Sub

End Module
