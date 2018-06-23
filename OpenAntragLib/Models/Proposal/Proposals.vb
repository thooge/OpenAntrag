Imports Raven.Client
Imports System.Text
Imports System.Web

Public Class Proposals

    Public Shared Function SearchFor(strTerms As String) As List(Of Proposal)

        '... http://stackoverflow.com/questions/4314545/ravendb-full-text-search
        'http://ravendb.net/docs/2.0/client-api/querying/static-indexes/searching
        'http://ravendb.net/docs/client-api/querying/static-indexes/configuring-index-options                

        Dim lst As New List(Of Proposal)

        Using ds As IDocumentSession = DataDocumentStore.Session

            lst = ds.Query(Of Proposal)().Search(Function(x) x.Text, strTerms).ToList()

        End Using

        Return lst

    End Function

    Public Shared Function GetByRepresentation(rep As Representation) As List(Of Proposal)

        Dim lst As New List(Of Proposal)

        Using ds As IDocumentSession = DataDocumentStore.Session

            'max. 128 / 1024! > http://ravendb.net/docs/2.0/intro/safe-by-default
            '                   http://ravendb.net/kb/31/my-10-tips-and-tricks-with-ravendb
            '                   https://groups.google.com/forum/#!msg/ravendb/UcLmIajTTq4/Lpke9qd13woJ

            Dim query = ds.Query(Of Proposal)() _
                        .OrderByDescending(Function(e) e.Timestamp) _
                        .Where(Function(e) e.Key_Representation = rep.Key)

            If query.Count > 0 Then
                lst = query.ToList
            End If

        End Using

        Return lst

    End Function

    Public Shared Function GetItemsTop(count As Integer) As List(Of Proposal)

        Dim lst As New List(Of Proposal)

        Using ds As IDocumentSession = DataDocumentStore.Session

            'TODO: Order & Take
            'http://stackoverflow.com/questions/8931289/ravendb-orderbydescending-and-take-incorrect-results
            'http://ravendb.net/docs/client-api/querying/stale-indexes
            'http://ravendb.net/docs/2.0/client-api/querying/query-and-lucene-query
            'Dim stats As New Raven.Client.RavenQueryStatistics
            'Dim query = ds.Query(Of Proposal)() _
            '            .Statistics(stats) _
            '            .Customize(Function(x) x.WaitForNonStaleResultsAsOfNow()) _
            '            .OrderByDescending(Function(e) e.CreatedAt) _
            '            .Take(count)

            Dim query = ds.Query(Of Proposal)() _
                        .Where(Function(e) e.IsTest = False) _
                        .OrderByDescending(Function(e) e.Timestamp) _
                        .Take(count)

            If query.Count > 0 Then
                lst = query.ToList
                'lst = lst.Take(count).ToList()
            End If

        End Using

        Return lst

    End Function

    Public Shared Function GetItemsTopByRepresentation(rep As Representation,
                                                       count As Integer) As List(Of Proposal)

        Dim lst As New List(Of Proposal)

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = ds.Query(Of Proposal)() _
                        .Where(Function(e) e.Key_Representation = rep.Key) _
                        .OrderByDescending(Function(e) e.Timestamp) _
                        .Take(count)

            If query.Count > 0 Then
                lst = query.ToList
                'lst = lst.Take(count).ToList()
            End If

        End Using

        Return lst


    End Function

    Public Shared Function GetItemsCount() As Integer

        Dim intCount As Integer = -1

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim stats As New RavenQueryStatistics
            Dim query = ds.Query(Of Proposal)() _
                        .Statistics(stats) _
                        .Where(Function(e) e.IsTest = False) _
                        .Take(0).ToArray()
            intCount = stats.TotalResults

        End Using

        Return intCount

    End Function

    Public Shared Function GetItemsCountByRepresentation(rep As Representation) As Integer

        Dim intCount As Integer = -1

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim stats As New RavenQueryStatistics
            Dim query = ds.Query(Of Proposal)() _
                        .Statistics(stats) _
                        .Where(Function(e) e.Key_Representation = rep.Key) _
                        .Take(0).ToArray()
            'AndAlso e.IsTest = False 
            intCount = stats.TotalResults

        End Using

        Return intCount

    End Function

    Public Shared Function GetItemsPage(pageNo As Integer,
                                        pageCount As Integer) As List(Of Proposal)

        Dim lst As New List(Of Proposal)

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = ds.Query(Of Proposal)() _
                        .Where(Function(e) e.IsTest = False) _
                        .OrderByDescending(Function(e) e.Timestamp) _
                        .Skip((pageNo - 1) * pageCount).Take(pageCount)

            If query.Count > 0 Then
                lst = query.ToList
            End If

        End Using

        Return lst

    End Function

    'TODO: http://daniellang.net/joining-documents-in-ravendb-2-0/
    '      http://ravendb.net/docs/2.0/client-api/querying/handling-document-relationships
    '      http://www.w3enterprises.com/articles/beginner-guide-to-ravendb.aspx
    'Public Shared Function GetItemsPageByFederalState(federal As String,
    '                                                  pageNo As Integer,
    '                                                  pageCount As Integer) As List(Of Proposal)

    '    Dim lst As New List(Of Proposal)

    '    Using ds As IDocumentSession = DataDocumentStore.Session

    '        Dim query = ds.Query(Of Proposal)() _
    '                    .Where(Function(e) e.IsTest = False) _
    '                    .OrderByDescending(Function(e) e.Timestamp) _
    '                    .Skip((pageNo - 1) * pageCount).Take(pageCount)

    '        If query.Count > 0 Then
    '            lst = query.ToList
    '        End If

    '    End Using

    '    Return lst

    'End Function

    Public Shared Function GetItemsPageByRepresentation(rep As Representation,
                                                        pageNo As Integer,
                                                        pageCount As Integer) As List(Of Proposal)

        Dim lst As New List(Of Proposal)

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = ds.Query(Of Proposal)() _
                        .Where(Function(e) e.Key_Representation = rep.Key) _
                        .OrderByDescending(Function(e) e.Timestamp) _
                        .Skip((pageNo - 1) * pageCount).Take(pageCount)

            If query.Count > 0 Then
                lst = query.ToList
                'lst = lst.ToList.Skip((pageNo - 1) * pageCount).Take(pageCount).ToList()
            End If

        End Using

        Return lst

    End Function

    Public Shared Function GetItemsByTag(tag As ProposalTag) As List(Of Proposal)

        Dim lst As New List(Of Proposal)

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = ds.Query(Of Proposal)() _
                        .Where(Function(x) x.Tags.Contains(tag.Tag) = True AndAlso x.IsTest = False) _
                        .OrderByDescending(Function(e) e.Timestamp)

            If query.Count > 0 Then
                lst = query.ToList
                'lst.Sort(New Comparison(Of Proposal)(Function(x As Proposal, y As Proposal) CType(y.CreatedAt, DateTime).CompareTo(CType(x.CreatedAt, DateTime))))
            End If

        End Using

        Return lst

    End Function

    Public Shared Function GetItemsByTagAndRepresentation(rep As Representation,
                                                          tag As ProposalTag) As List(Of Proposal)

        Dim lst As New List(Of Proposal)

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = ds.Query(Of Proposal)() _
                        .Where(Function(x) x.Key_Representation = rep.Key AndAlso x.Tags.Contains(tag.Tag)) _
                        .OrderByDescending(Function(e) e.Timestamp)

            If query.Count > 0 Then
                lst = query.ToList
                'lst.Sort(New Comparison(Of Proposal)(Function(x As Proposal, y As Proposal) CType(y.CreatedAt, DateTime).CompareTo(CType(x.CreatedAt, DateTime))))
            End If

        End Using

        Return lst

    End Function

    Public Shared Function GetItemsCommentCount() As Integer

        Dim intRetVal As Integer = -1
        Dim lst As New List(Of Proposal)

        Using ds As IDocumentSession = DataDocumentStore.Session
            Dim query = ds.Query(Of Proposal)("ProposalsWithComments")

            intRetVal += query.ToList().Sum(Function(p) p.ProposalComments.Count)

        End Using

        Return intRetVal

    End Function

    Public Shared Function GetById(id As String) As Proposal

        Dim prop As Proposal = Nothing

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim entity = ds.Load(Of Proposal)(id)

            If entity IsNot Nothing Then
                prop = entity
            End If

        End Using

        Return prop

    End Function

    Public Shared Function GetByTitleUrl(rep As Representation,
                                         titleUrl As String) As Proposal

        Dim prop As Proposal = Nothing

        Dim lst As List(Of Proposal) = GetByRepresentation(rep)

        'Dim query = From p As Proposal In lst
        '            Where p.TitleUrl = titleUrl
        '            Select p

        'If query.Count > 0 Then
        '    prop = query.First
        'End If

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = ds.Query(Of Proposal)()
            query = query.Where(Function(e) e.Key_Representation = rep.Key)
            query = query.Where(Function(e) e.TitleUrl = titleUrl)

            If query.Count > 0 Then
                prop = query.First
            End If

        End Using

        Return prop

    End Function

    Public Shared Sub SearchItems(ByRef srm As SearchModel,
                                  pageNo As Integer,
                                  pageCount As Integer)

        Try
            Using ds As IDocumentSession = DataDocumentStore.Session

                Dim query = ds.Query(Of Proposal)("FulltextByTextAndTitle") _
                            .Search(Function(m) m.Text, srm.SearchTerms, options:=SearchOptions.Or) _
                            .Search(Function(m) m.Title, srm.SearchTerms, options:=SearchOptions.Or)

                If query.Count > 0 Then
                    srm.Results = query.ToList()
                End If

            End Using

        Catch ex As Exception
        End Try

    End Sub

    Public Shared Function TitleUrlExists(rep As Representation,
                                          titleUrl As String) As Boolean

        Dim bolRetVal As Boolean = False

        Dim lst As List(Of Proposal) = GetByRepresentation(rep)

        Dim query = From p As Proposal In lst
                    Where p.TitleUrl = titleUrl
                    Select p

        If query.Count > 0 Then
            bolRetVal = True
        End If

        Return bolRetVal

    End Function

    Public Shared Function GetProcessStepCaption(caption As String,
                                                 p As Proposal,
                                                 m As Representation) As String

        Dim stb As New StringBuilder(caption)

        If caption.Contains("%COMMITTEE%") = True Then
            If String.IsNullOrEmpty(p.Key_Committee) = False Then
                Dim query = From c As Committee In m.Committees
                            Where c.Key = p.Key_Committee
                            Select c

                If query.Count > 0 Then
                    stb.Replace("%COMMITTEE%", query.First.Name)
                End If
            End If
        End If

        If caption.Contains("%REPRESENTATIVE%") = True Then
            If String.IsNullOrEmpty(p.Key_Representative) = False Then
                Dim query = From r As Representative In m.Representatives
                            Where r.Key = p.Key_Representative
                            Select r

                If query.Count > 0 Then
                    stb.Replace("%REPRESENTATIVE%", query.First.Name)
                End If
            End If
        End If

        Return stb.ToString

    End Function

    Public Shared Function CreateNew(keyRepresentation As String,
                                     title As String,
                                     text As String,
                                     tagList As String,
                                     contactInfo As String) As Proposal

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)

        Dim model As New Proposal(rep) With {
            .Title = title.StripSpecialCharsForTitle,
            .Text = text,
            .ContactInfo = contactInfo
        }

        If title.StartsWith("TEST:") Or rep.IsTest = True Then
            model.IsTest = True
        End If

        Dim stbUrl As New StringBuilder(Tools.MakeReadableUrl(model.Title))
        If Proposals.TitleUrlExists(rep, stbUrl.ToString) = True Then
            stbUrl.Append("-").Append(Format(DateTime.Now, "yyMMddHHmm"))
        End If
        model.TitleUrl = stbUrl.ToString

        If model.IsTest = False Then
            model.ShortUrl = UrlShortener.GetShortUrl(model.FullUrl)
        End If

        Dim ps As New ProposalStep
        ps.Id = "1"
        ps.ID_ProcessStep = 1
        'ps.Info = "Eingang des Antrags"

        model.ProposalSteps.Add(ps)

        model.ID_CurrentProposalStep = ps.Id

        If HttpContext.Current.User.Identity.IsAuthenticated = True Then
            model.CreatedBy = HttpContext.Current.User.Identity.Name
        End If

        Dim newID As String
        Using ds As IDocumentSession = DataDocumentStore.Session()
            ds.Store(model)
            newID = model.Id

            Dim tags As ProposalTags = ProposalTags.Load()
            SaveTags(model, tags, tagList)
            ds.Store(tags)

            ds.SaveChanges()
        End Using

        MailManager.SendNewProposal(model)
        PushoverManager.SendNewProposal(model)
        TwitterManager.TweetNewProposal(model)
        NotificationManager.StoreNewProposal(model)
        PushbulletManager.SendNewProposal(model)

        Return model

    End Function

    Public Shared Sub SaveTags(ByRef prop As Proposal,
                               ByRef tags As ProposalTags,
                               ByVal tagList As String)

        If prop.Tags Is Nothing Then prop.Tags = New List(Of String)

        Dim lstTagsNew As List(Of String) = tagList.Split(",").ToList
        Dim lstTagsOld As List(Of String) = prop.Tags

        'Tag in Proposal speichern
        prop.Tags = lstTagsNew

        'Proposal in Tag speichern (Add/Delete)
        Dim lstAddTags As IEnumerable(Of String) = lstTagsNew.Except(lstTagsOld)
        Dim lstDeleteTags As IEnumerable(Of String) = lstTagsOld.Except(lstTagsNew)

        '(Add)
        For Each s As String In lstAddTags
            If String.IsNullOrEmpty(s) = False Then
                Dim query = From tag As ProposalTag In tags.Items
                            Where tag.Tag = s
                            Select tag

                If query.Count > 0 Then
                    Dim tag As ProposalTag = query.First
                    If prop.IsTest = False Then
                        If tag.Proposals Is Nothing Then tag.Proposals = New List(Of String)
                        tag.Proposals.Add(prop.Id)
                    End If
                Else
                    Dim newTag As New ProposalTag(s)
                    newTag.Proposals = New List(Of String)
                    newTag.Proposals.Add(prop.Id)
                    tags.Items.Add(newTag)
                End If
            End If
        Next

        '(Delete)
        For Each s As String In lstDeleteTags
            If String.IsNullOrEmpty(s) = False Then
                Dim query = From tag As ProposalTag In tags.Items
                            Where tag.Tag = s
                            Select tag

                If query.Count > 0 Then
                    Dim tag As ProposalTag = query.First
                    tag.Proposals.Remove(prop.Id)
                    If tag.Proposals.Count = 0 Then
                        tags.Items.Remove(tag)
                    End If
                End If
            End If
        Next

    End Sub

    Public Shared Sub SaveNextStep(ByRef prop As Proposal,
                                   idStep As Integer,
                                   info As String,
                                   keyRepresentative As String,
                                   keyCommittee As String)

        Dim ps As New ProposalStep
        ps.Id = CType(prop.ID_CurrentProposalStep, Integer) + 1
        ps.ID_ProcessStep = idStep
        ps.Info = info.EnsureMarkdown

        prop.ProposalSteps.Add(ps)
        prop.ID_CurrentProposalStep = ps.Id

        If keyRepresentative IsNot Nothing Then
            prop.Key_Representative = keyRepresentative
        End If

        If keyCommittee IsNot Nothing Then
            prop.Key_Committee = keyCommittee
        End If

        prop.UpdatedAt = Format(DateTime.Now, "dd.MM.yyyy HH:mm:ss")
        If HttpContext.Current.User.Identity.IsAuthenticated = True Then
            prop.UpdatedBy = HttpContext.Current.User.Identity.Name
        End If

        Using ds As IDocumentSession = DataDocumentStore.Session()
            ds.Store(prop)
            ds.SaveChanges()
        End Using

    End Sub

End Class
