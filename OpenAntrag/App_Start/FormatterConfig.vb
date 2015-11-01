Imports System.Net.Http.Formatting
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization
'Imports WebApiContrib.Formatting.Jsonp

Public Class FormatterConfig

    Public Shared Sub RegisterFormatters(formatters As MediaTypeFormatterCollection)

        '>>> WebApiContrib.Formatting.Jsonp
        '>>> STACKOVERFLOW-EXCEPTION: https://github.com/WebApiContrib/WebApiContrib.Formatting.Jsonp/pull/10

        'Dim jsonFormatter = formatters.JsonFormatter

        'jsonFormatter.SerializerSettings = New JsonSerializerSettings() With {
        '    .ContractResolver = New CamelCasePropertyNamesContractResolver()
        '}

        'Dim jsonpFormatter = New JsonpMediaTypeFormatter(formatters.JsonFormatter)
        'formatters.Insert(0, jsonpFormatter)

    End Sub

End Class
