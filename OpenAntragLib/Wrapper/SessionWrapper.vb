Imports System.Web

Public Class SessionWrapper

#Region "Helper"

    Private Shared Function GetFromSession(Of T)(ByVal strKey As String) As T

        If HttpContext.Current.Session IsNot Nothing Then
            Dim obj As Object = HttpContext.Current.Session(strKey)
            If (obj Is Nothing) Then
                Return Nothing
            Else
                Return CType(obj, T)
            End If
        End If

    End Function

    Private Shared Sub SetInSession(Of T)(ByVal strKey As String, ByVal tValue As T)

        If HttpContext.Current.Session IsNot Nothing Then
            If (tValue Is Nothing) Then
                HttpContext.Current.Session.Remove(strKey)
            Else
                HttpContext.Current.Session(strKey) = tValue
            End If
        End If

    End Sub

#End Region

End Class