Imports Raven.Client

Public Class Feedbacks

    Public Shared Function GetItems() As List(Of Feedback)

        Dim lst As New List(Of Feedback)

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = ds.Query(Of Feedback)()

            If query.Count > 0 Then
                lst = query.ToList
                lst.Sort(New Comparison(Of Feedback)(Function(x As Feedback, y As Feedback) CType(y.CreatedAt, DateTime).CompareTo(CType(x.CreatedAt, DateTime))))
            End If

        End Using

        Return lst

    End Function

    Public Shared Function GetItemsByType(fb As FeedbackType) As List(Of Feedback)

        Dim lst As New List(Of Feedback)

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = ds.Query(Of Feedback)().Where(Function(f) f.Type = fb.ID)

            If query.Count > 0 Then
                lst = query.ToList
                lst.Sort(New Comparison(Of Feedback)(Function(x As Feedback, y As Feedback) CType(y.CreatedAt, DateTime).CompareTo(CType(x.CreatedAt, DateTime))))
            End If

        End Using

        Return lst

    End Function

    Public Shared Function GetByID(id As String) As Feedback

        Dim fb As Feedback = Nothing

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim entity = ds.Load(Of Feedback)(id)

            If entity IsNot Nothing Then
                fb = entity
            End If

        End Using

        Return fb

    End Function

    Public Shared Function GetCount() As Integer

        Dim intCount As Integer = -1

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim stats As New RavenQueryStatistics
            Dim query = ds.Query(Of Feedback)() _
                        .Statistics(stats) _
                        .Take(0).ToArray()
            intCount = stats.TotalResults

        End Using

        Return intCount

    End Function

End Class
