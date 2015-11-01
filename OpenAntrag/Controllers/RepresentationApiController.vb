Imports System.Net
Imports System.Web.Http

Public Class RepresentationApiController
    Inherits ApiController

    Public Function GetKeyValueList() As IEnumerable(Of KeyValueObject)

        Dim query = (From rep As Representation In GlobalData.Representations.Items
                    Where rep.IsTest = False
                    Order By rep.Name
                    Select New KeyValueObject() With {.Key = rep.Key, .Value = rep.Name}).ToList

        Return query

    End Function

    Public Function GetAll() As IEnumerable(Of Representation)

        Dim query = From r As Representation In GlobalData.Representations.Items
                    Where r.IsTest = False
                    Select r

        Return query

    End Function

    Public Function GetByKey(key As String) As Representation

        Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)

        If rep Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        Return rep

    End Function

    Public Function GetRepresentatives(key As String) As IEnumerable(Of Representative)

        Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)

        If rep Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        Return rep.Representatives

    End Function

    Public Function GetCommittees(key As String) As IEnumerable(Of Committee)

        Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)

        If rep Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        Return rep.Committees

    End Function

    Public Function GetProcessSteps(key As String) As IEnumerable(Of ProcessStep)

        Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)

        If rep Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        Return rep.ProcessSteps

    End Function

    Public Function GetProcessStepById(key As String, id As String) As ProcessStep

        Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)
        Dim query = From ps As ProcessStep In rep.ProcessSteps
                    Where ps.ID = CType(id, Integer)
                    Select ps

        If query.Count > 0 Then
            Return query.First()
        Else
            Return Nothing
        End If

    End Function

End Class
