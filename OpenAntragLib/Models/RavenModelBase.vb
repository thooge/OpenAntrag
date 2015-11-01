Imports System.ComponentModel.DataAnnotations

Public Class RavenModelBase
    Inherits ModelBase
    Implements IRavenModelBase

    <Display(Name:="ID")>
    Public Property Id As String Implements IRavenModelBase.Id

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property IdNumber As Integer Implements IRavenModelBase.IdNumber
        Get
            If Me.Id IsNot Nothing AndAlso Me.Id.Length > 0 Then
                'Dim strClassTypeName As String = Me.GetType().Name.ToLower
                'Return CType(Me.Id.ToLower.Replace(strClassTypeName & "s-", ""), Integer)
                Dim arr As String() = Split(Me.Id, "-")
                Return arr(arr.Length - 1)
            Else
                Return 0
            End If
        End Get
    End Property

End Class
