Imports System.Web
Imports System.Web.Routing

Public Class RepresentationRouteConstraint
    Implements IRouteConstraint

    Public Function Match(httpContext As HttpContextBase,
                          route As Route,
                          parameterName As String,
                          values As RouteValueDictionary,
                          routeDirection As RouteDirection) As Boolean Implements IRouteConstraint.Match

        Dim intLength As Integer = values.Count - 1

        Dim strController As String = values.Values(intLength - 1)
        Dim strAction As String = values.Values(intLength)

        Dim strParameterName As String = parameterName
        Dim strParameterValue As String = values.Item(parameterName)

        If strParameterName = "keyRepresentation" AndAlso
            String.IsNullOrEmpty(strParameterValue) = False AndAlso
            strController = "Representation" Then

            Dim rep As Representation = GlobalData.Representations.GetByKey(strParameterValue.ToLower)

            Return rep IsNot Nothing

        End If

        Return False

    End Function

End Class
