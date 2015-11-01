Imports System.ComponentModel.DataAnnotations
Imports Raven.Client
Imports System.Web.Script.Serialization

Public Class ProposalTags
    Inherits RavenModelBase

    Public Property Items As List(Of ProposalTag)

    Public Sub New()
    End Sub

    Public Shared Function Load() As ProposalTags

        Using ds As IDocumentSession = DataDocumentStore.Session
            Dim entity = ds.Load(Of ProposalTags)("ProposalTags-1")
            If entity IsNot Nothing Then
                Return entity
            End If
        End Using

        Return Nothing

    End Function

    Public Shared Function Tags() As List(Of ProposalTag)

        Using ds As IDocumentSession = DataDocumentStore.Session
            Dim entity = ds.Load(Of ProposalTags)("ProposalTags-1")
            If entity IsNot Nothing Then
                Return entity.Items
            End If
        End Using

        Return Nothing

    End Function

    Public Shared Function TagsOrdered() As List(Of ProposalTag)

        Dim lst As List(Of ProposalTag) = Nothing

        Try
            lst = (From t As ProposalTag In ProposalTags.Tags
                   Order By t.Tag Ascending
                   Select t).ToList

        Catch ex As Exception
        End Try

        Return lst

    End Function

    Public Shared Function TagsList() As List(Of String)

        Dim lst As List(Of String)

        lst = (From t As ProposalTag In ProposalTags.Tags
               Order By t.Tag Ascending
               Select t.Tag).ToList

        Return lst

    End Function

    Public Shared Function GetTag(tag As String) As ProposalTag

        Dim model As ProposalTag = Nothing

        Dim lst As List(Of ProposalTag) = ProposalTags.Tags()

        Dim query = From pt As ProposalTag In lst
                    Where pt.Tag.ToLower = tag.ToLower
                    Select pt

        If query.Count > 0 Then
            model = query.First
        End If

        Return model

    End Function

    Public Shared Function TagCloudItems() As List(Of TagCloudItem)

        Dim lst As New List(Of TagCloudItem)

        Try
            Dim query = From pt As ProposalTag In ProposalTags.TagsOrdered
                    Select New TagCloudItem With {
                        .text = pt.Tag,
                        .weight = pt.ProposalCount,
                        .link = String.Concat("/themen/", pt.Tag.ToLower),
                        .html = New TagCloudHtmlAttribute With {
                            .title = pt.Tag
                        }
                    }

            lst = query.ToList

        Catch ex As Exception
        End Try

        Return lst

    End Function

End Class

Public Class ProposalTag

    Public Property Tag As String
    Public Property Proposals As List(Of String)

    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property ProposalCount As Integer
        Get
            If Me.Proposals Is Nothing Then
                Return 0
            Else
                Return Me.Proposals.Count
            End If
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(tag As String)
        Me.Tag = tag
    End Sub

End Class

Public Class TagCloudItem
    Public Property text As String
    Public Property weight As Integer
    Public Property link As String
    Public Property html As TagCloudHtmlAttribute
End Class

Public Class TagCloudHtmlAttribute
    Public Property title As String
    Public Property [class] As String
End Class
