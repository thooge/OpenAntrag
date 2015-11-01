Imports System.ServiceModel.Syndication
Imports System.IO

Public Class HomeController
    Inherits DocumentStoreController

#Region "View Actions"

    Public Function AllRepresentationsStyle() As FileResult

        Dim strTemplatePath As String = HttpContext.Server.MapPath("~/Content/style-allrepresentations.template.css")
        Dim strTemplate As String = Nothing
        If IO.File.Exists(strTemplatePath) = True Then
            strTemplate = IO.File.ReadAllText(strTemplatePath)
        End If

        Dim stb As New StringBuilder()

        For Each rep As Representation In GlobalData.Representations.Items
            stb.Append(strTemplate)
            Representations.ReplaceStyleColor(rep, stb)
        Next

        Dim ba As Byte() = Encoding.UTF8.GetBytes(stb.ToString())
        Dim stm As MemoryStream = New MemoryStream(ba)

        Return File(stm, "text/css", "allrepresentationstyle.css")

    End Function

    Public Function Index() As ActionResult
        Return View()
    End Function

    Public Function Success(pageNo As Integer) As ActionResult

        Dim model As List(Of SuccessStory) = SuccessStories.GetItemsPage(pageNo, SettingsWrapper.DefaultPagerListPageSize)

        ViewData("ItemsCount") = SuccessStories.GetItemsCount()
        ViewData("PageNo") = pageNo

        Return View(model)

    End Function

    Public Function Journal(pageNo As Integer) As ActionResult

        Dim model As List(Of Proposal) = Proposals.GetItemsPage(pageNo, SettingsWrapper.DefaultPagerListPageSize)

        ViewData("ItemsCount") = Proposals.GetItemsCount()
        ViewData("PageNo") = pageNo

        Return View(model)

    End Function

    Public Function List() As ActionResult

        Dim model As List(Of Proposal) = Proposals.GetItemsPage(1, SettingsWrapper.ProposalListCount)

        ViewData("ItemsShown") = SettingsWrapper.ProposalListCount
        ViewData("ItemsCount") = Proposals.GetItemsCount()

        Return View(model)

    End Function

    Public Function Search(searchTerms As String, pageNo As Integer) As ActionResult

        Dim model As SearchModel = Nothing

        If String.IsNullOrEmpty(searchTerms) = False Then
            model = New SearchModel(searchTerms)
            Proposals.SearchItems(model, pageNo, SettingsWrapper.DefaultPagerListPageSize)
        End If

        ViewData("PageNo") = 1

        Return View(model)

    End Function

    Public Function Tags(tag As String, pageNo As Integer) As ActionResult

        Dim model As ProposalTag = Nothing

        If String.IsNullOrEmpty(tag) = False Then
            model = ProposalTags.GetTag(tag)
        End If

        ViewData("PageNo") = pageNo

        Return View(model)

    End Function

    Public Function ProposalAllFeed() As FeedResult

        Dim lst As List(Of Proposal) = Proposals.GetItemsTop(25)

        Dim items As IEnumerable(Of SyndicationItem) = Nothing

        If lst IsNot Nothing Then
            items = From prop As Proposal In lst
                    Select prop.FeedItem
        End If

        Dim feed As New SyndicationFeed("OpenAntrag-Feed",
                                        String.Concat("Aktuelle Bürgeranträge auf ", HttpContext.Request.Url.Authority),
                                        New Uri(String.Concat("http://", HttpContext.Request.Url.Authority, "/feed")),
                                        "", DateTime.Now, items) With {.Language = "de-DE"}

        Return New FeedResult(New Rss20FeedFormatter(feed))

    End Function

    Public Function Api() As ActionResult
        Return View()
    End Function

    Public Function Overview() As ActionResult
        Dim lst As List(Of Representation) = GlobalData.Representations.Items.Where(Function(r) r.Status > 0).ToList()
        Return View(lst)
    End Function

    Public Function Faq() As ActionResult
        Return View()
    End Function

#End Region

#Region "Service Actions"

    <HandleErrorAsJson()>
    Public Function GetProposalListTable(iCount As Integer)

        Dim model As List(Of Proposal) = Proposals.GetItemsPage(1, iCount)

        ViewData("ShowRepresentation") = True
        Return Me.GetPartialModel("ProposalListTable", model)

    End Function

    <Authorize()>
    <HandleErrorAsJson()>
    Public Function SaveRepresentationSetting(key As String,
                                              hasContactPossibility As Boolean) As JsonResult

        Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)
        Dim model As RepresentationSetting = RepresentationSettings.GetByKey(key)

        If model IsNot Nothing Then
            model.HasContactPossibility = hasContactPossibility
            Me.StoreAndSave(model)
        End If

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = rep.FullUrl}

        Return jr

    End Function

#End Region

End Class
