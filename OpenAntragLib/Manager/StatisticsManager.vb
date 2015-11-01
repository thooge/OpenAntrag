Imports Raven.Client

Public Module StatisticsManager

    Public Sub GetProposalCountByRepresentationData(ByRef lstCategories As List(Of String),
                                                    ByRef lstData As List(Of BarColumnDataSDM))

        Dim lst As List(Of ProposalCountByRepresentationRIM)

        Using ds As IDocumentSession = DataDocumentStore.Session
            Dim query = ds.Query(Of ProposalCountByRepresentationRIM)("Statistics/ProposalCountByRepresentation")
            lst = query.ToList()
        End Using

        lst = lst.OrderByDescending(Function(m) m.Count).ToList()

        'Dim rps As New Representations()

        For Each m As ProposalCountByRepresentationRIM In lst
            Dim rep As Representation = (From r As Representation In GlobalData.Representations.Items
                                         Where r.Key = m.Key
                                         Select r).First()

            If rep.Status And Representations.StatusConjuction.Active Then
                lstCategories.Add(rep.Label.BreakWordsHtml(15))

                lstData.Add(New BarColumnDataSDM With {.y = m.Count,
                                                       .color = rep.Color,
                                                       .url = rep.FullUrl})
            End If
        Next

    End Sub

    Public Sub GetRepresentationCountByType(ByRef lstData As List(Of PieSliceDataSDM))

        'Dim rps As New Representations(Representations.StatusConjuction.Active)
        Dim lst As List(Of Representation) = GlobalData.Representations.Items _
            .Where(Function(x) (x.Status And (Representations.StatusConjuction.Active)) > 0) _
            .ToList()

        Dim query = From rp In lst
                    Group rp By rpg = New With {Key .Common = rp.GroupTypeObject.Common,
                                                Key .Color = rp.GroupTypeObject.Color}
                    Into Group
                    Select New PieSliceDataSDM With {
                        .y = Group.Count,
                        .name = rpg.Common,
                        .color = rpg.Color}

        lstData = query.ToList()

    End Sub

    Public Sub GetFeedbackCountByType(ByRef lstData As List(Of PieSliceDataSDM))

        Dim lst As List(Of FeedbackCountByTypeRIM)

        Using ds As IDocumentSession = DataDocumentStore.Session
            Dim query = ds.Query(Of FeedbackCountByTypeRIM)("Statistics/FeedbackCountByType")
            lst = query.ToList()
        End Using

        For Each m As FeedbackCountByTypeRIM In lst
            Dim fbt As FeedbackType = (From f As FeedbackType In GlobalData.FeedbackTypes.Items
                                       Where f.ID = m.Type
                                       Select f).FirstOrDefault()

            lstData.Add(New PieSliceDataSDM() With {.y = m.Count,
                                                    .name = fbt.Name,
                                                    .color = fbt.Color})
        Next

    End Sub

End Module
