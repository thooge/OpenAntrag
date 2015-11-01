Imports System.ComponentModel.DataAnnotations

Public Class SearchModel

    <Required(ErrorMessage:="Bitte eingeben")>
    Public Property SearchTerms As String

    Public Property Results As List(Of Proposal)

    Public Sub New(searchTerms As String)
        Me.SearchTerms = searchTerms
        Me.Results = New List(Of Proposal)
    End Sub

End Class
