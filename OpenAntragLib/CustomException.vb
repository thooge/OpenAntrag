Imports System.Runtime.Serialization

<Serializable>
Public Class CustomException
    Inherits Exception

    Public Property Title As String = "Fehlerhinweis"

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(ByVal message As String, ByVal title As String)
        Me.New(message)
        Me.Title = title
    End Sub

    Public Sub New(format As String, ParamArray args As Object())
        MyBase.New(String.Format(format, args))
    End Sub

    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Public Sub New(format As String, innerException As Exception, ParamArray args As Object())
        MyBase.New(String.Format(format, args), innerException)
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class
