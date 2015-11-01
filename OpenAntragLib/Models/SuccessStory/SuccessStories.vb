Imports Raven.Client

Public Class SuccessStories

    Public Shared Function GetItemsCount() As Integer

        Dim intCount As Integer = -1

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim stats As New RavenQueryStatistics
            Dim query = ds.Query(Of SuccessStory)() _
                        .Statistics(stats) _
                        .Take(0).ToArray()
            intCount = stats.TotalResults

        End Using

        Return intCount

    End Function

    Public Shared Function GetItemsPage(pageNo As Integer,
                                        pageCount As Integer) As List(Of SuccessStory)

        Dim lst As New List(Of SuccessStory)

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = ds.Query(Of SuccessStory)() _
                        .OrderByDescending(Function(e) e.StepDateTimestamp) _
                        .Skip((pageNo - 1) * pageCount).Take(pageCount)

            If query.Count > 0 Then
                lst = query.ToList
            End If

        End Using

        Return lst.Where(Function(x) x.Proposal.Representation.IsTest = False).ToList

    End Function

    Public Shared Function GetById(id As String) As SuccessStory

        Dim model As SuccessStory = Nothing

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim entity = ds.Load(Of SuccessStory)(id)

            If entity IsNot Nothing Then
                model = entity
            End If

        End Using

        Return model

    End Function

End Class
