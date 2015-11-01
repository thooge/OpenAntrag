Imports System.Net.Http.Formatting
Imports System.Net.Http.Headers
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Reflection

Public Class CSVMediaTypeFormatter
    Inherits MediaTypeFormatter

    Public Sub New()

        SupportedMediaTypes.Add(New MediaTypeHeaderValue("text/csv"))

    End Sub

    Public Sub New(mediaTypeMapping As MediaTypeMapping)

        Me.New()

        MediaTypeMappings.Add(mediaTypeMapping)

    End Sub

    Public Sub New(mtm As IEnumerable(Of MediaTypeMapping))

        Me.New()

        For Each m As MediaTypeMapping In mtm
            MediaTypeMappings.Add(m)
        Next

    End Sub

    Public Overrides Function CanWriteType(type As Type) As Boolean

        If type Is Nothing Then
            Throw New ArgumentNullException("type")
        End If

        Return Me.IsTypeOfIEnumerable(type)

    End Function

    Private Function IsTypeOfIEnumerable(type As Type) As Boolean

        For Each interfaceType As Type In type.GetInterfaces()

            If interfaceType = GetType(IEnumerable) Then
                Return True
            End If
        Next

        Return False

    End Function

    Public Overrides Function CanReadType(type As Type) As Boolean
        Return False
    End Function

    Public Overrides Function WriteToStreamAsync(type As Type,
                                                    value As Object,
                                                    stream As Stream,
                                                    content As HttpContent,
                                                    transportContext As TransportContext) As Task

        writeStream(type, value, stream, content)

        Dim tcs = New TaskCompletionSource(Of Integer)()
        tcs.SetResult(0)
        Return tcs.Task

    End Function

    Private Sub writeStream(type As Type,
                            value As Object,
                            stream As Stream,
                            content As HttpContent)

        'NOTE: We have check the type inside CanWriteType method
        'If request comes this far, the type is IEnumerable. We are safe.

        Dim _stringWriter As New StringWriter()

        Dim bHeaders As Boolean
        Dim lstHeaders As New List(Of String)

        Try
            For Each obj In DirectCast(value, IEnumerable(Of Object))

                Dim oType As Type = obj.GetType()
                Dim lstValues As New List(Of Object)

                For Each oProp As PropertyInfo In oType.GetProperties()

                    Dim attrIgnore As CSVIgnore() = DirectCast(oProp.GetCustomAttributes(GetType(CSVIgnore), True), CSVIgnore())
                    If attrIgnore.Count = 0 Then

                        If bHeaders = False Then
                            lstHeaders.Add(oProp.Name)
                        End If

                        lstValues.Add(New With {.Value = oProp.GetValue(obj)})
                    End If
                Next

                If bHeaders = False Then
                    _stringWriter.WriteLine(String.Join(";", lstHeaders.ToArray))
                    bHeaders = True
                End If

                'Dim vals = obj.GetType().GetProperties().Select(Function(x) New With {.Value = x.GetValue(obj)})

                Dim _valueLine As String = String.Empty

                For Each val As Object In lstValues

                    If val.Value IsNot Nothing Then

                        Dim _val = val.Value.ToString()

                        'Check if the value contans a comma and place it in quotes if so
                        If _val.Contains(";") Then
                            _val = String.Concat("""", _val, """")
                        End If

                        'Replace any \r or \n special characters from a new line with a space
                        If _val.Contains(vbCr) Then
                            _val = _val.Replace(vbCr, " ")
                        End If
                        If _val.Contains(vbLf) Then
                            _val = _val.Replace(vbLf, " ")
                        End If

                        _valueLine = String.Concat(_valueLine, _val, ";")
                    Else
                        _valueLine = String.Concat(_valueLine, String.Empty, ";")
                    End If
                Next

                _stringWriter.WriteLine(_valueLine.TrimEnd(";"c))
            Next
        Catch ex As Exception
        End Try

        Dim streamWriter = New StreamWriter(stream, System.Text.Encoding.UTF8)
        streamWriter.Write(_stringWriter.ToString())
    End Sub

End Class
