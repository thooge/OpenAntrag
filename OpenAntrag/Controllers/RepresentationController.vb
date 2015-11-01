Imports System.ServiceModel.Syndication

Public Class RepresentationController
    Inherits DocumentStoreController

#Region "View Actions"

    Public Function Index(keyRepresentation As String) As ActionResult

        'Dim xxx As String = Representations.GetNewApiKey()
        'Stop

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)
        ViewBag.CurrentRepresentation = rep

        Return View(rep)

    End Function

    Public Function Add(keyRepresentation As String) As ActionResult

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)
        ViewBag.CurrentRepresentation = rep

        Return View(rep)

    End Function

    Public Function Journal(keyRepresentation As String, pageNo As Integer) As ActionResult

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)
        ViewBag.CurrentRepresentation = rep

        ViewData("PageNo") = pageNo
        Return View(rep)

    End Function

    Public Function List(keyRepresentation As String) As ActionResult

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)
        ViewBag.CurrentRepresentation = rep

        Return View(rep)

    End Function

    Public Function Banner(keyRepresentation As String) As ActionResult

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)
        ViewBag.CurrentRepresentation = rep

        Return View(rep)

    End Function

    <Authorize()>
    Public Function Settings(keyRepresentation As String) As ActionResult

        If Tools.IsAdmin(keyRepresentation) = True Then
            Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)
            ViewBag.CurrentRepresentation = rep
            Return View(rep)
        Else
            Return RedirectToAction("Index", "Representation", New With {.keyRepresentation = keyRepresentation})
        End If

    End Function

    Public Function Proposal(keyRepresentation As String, titleUrl As String) As ActionResult

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)
        ViewBag.CurrentRepresentation = rep

        Dim model As Proposal = Nothing

        model = Proposals.GetByTitleUrl(rep, titleUrl)
        If model IsNot Nothing Then
            model.FillProcessSteps()

            Return View(model)
        Else
            Throw New HttpException(404, "Diese Seite existiert nicht")
        End If

    End Function

    Public Function SuccessStory(keyRepresentation As String, titleUrl As String) As ActionResult

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)
        ViewBag.CurrentRepresentation = rep

        Dim prop As Proposal = Nothing

        prop = Proposals.GetByTitleUrl(rep, titleUrl)
        If prop IsNot Nothing Then
            prop.FillProcessSteps()

            Dim model As SuccessStory = SuccessStories.GetById(prop.ID_SuccessStory)
            model.Proposal = prop

            Return View(model)
        Else
            Throw New HttpException(404, "Diese Seite existiert nicht")
        End If

    End Function

    Public Function RepresentationStyle(keyRepresentation As String) As ActionResult

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)

        Dim strTemplatePath As String = HttpContext.Server.MapPath("~/Content/style-representation.template.css")
        Dim strTemplate As String = Nothing
        If System.IO.File.Exists(strTemplatePath) = True Then
            strTemplate = System.IO.File.ReadAllText(strTemplatePath)
        End If

        Dim stb As New StringBuilder()

        stb.Append(strTemplate)
        Representations.ReplaceStyleColor(rep, stb)

        Return Content(stb.ToString, "text/css")

    End Function

    Public Function ProposalFeed(keyRepresentation As String) As FeedResult

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)

        Dim lst As List(Of Proposal) = Proposals.GetItemsTopByRepresentation(rep, 25)

        Dim items As IEnumerable(Of SyndicationItem) = Nothing

        If lst IsNot Nothing Then
            items = From prop As Proposal In lst
                    Select prop.FeedItem
        End If

        Dim feed As New SyndicationFeed(String.Concat("OpenAntrag-Feed ", rep.Name),
                                        String.Concat("Alle Bürgeranträge, gestellt an ", rep.GroupName),
                                        New Uri(String.Concat("http://", HttpContext.Request.Url.Authority, "/", rep.Key, "/feed")),
                                        "", DateTime.Now, items) With {.Language = "de-DE"}

        Return New FeedResult(New Rss20FeedFormatter(feed))

    End Function

#End Region

#Region "Service Actions"

    <HandleErrorAsJson()>
    Public Function CreateProposal(keyRepresentation As String,
                                   title As String,
                                   text As String,
                                   contactInfo As String) As JsonResult

        Dim model As Proposal = Proposals.CreateNew(keyRepresentation, title, text, "", contactInfo)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = model.FullUrl}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function SaveProposalText(idProposal As String,
                                     sText As String) As JsonResult

        Dim model As Proposal = Me.DocumentSession.Load(Of Proposal)(idProposal)

        If model Is Nothing Then
            Throw New CustomException("Antrag nicht gefunden")
        End If

        If Tools.IsAdmin(model.Key_Representation) = False Then
            Throw New CustomException("Für diesen Vorgang hast Du keine Berechtigung")
        End If

        model.Text = sText
        StoreAndSave(model)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function SaveProposalRating(idProposal As String,
                                       iRate As Integer) As JsonResult

        Dim model As Proposal = Me.DocumentSession.Load(Of Proposal)(idProposal)

        If model Is Nothing Then
            Throw New CustomException("Antrag nicht gefunden")
        End If

        model.RatingCount += 1
        model.RatingSum += iRate
        StoreAndSave(model)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function SaveProposalDate(idProposal As String,
                                     sDate As String) As JsonResult

        Dim model As Proposal = Me.DocumentSession.Load(Of Proposal)(idProposal)

        If model Is Nothing Then
            Throw New CustomException("Antrag nicht gefunden")
        End If

        If Tools.IsAdmin(model.Key_Representation) = False Then
            Throw New CustomException("Für diesen Vorgang hast Du keine Berechtigung")
        End If

        If IsDate(sDate) = True Then
            model.CreatedAt = sDate
            model.Timestamp = Tools.GetUnixTimestampFromDate(CType(model.CreatedAt, DateTime))
            StoreAndSave(model)
        End If

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function SaveProposalStepDate(idProposal As String,
                                         idStep As Integer,
                                         sDate As String) As JsonResult

        Dim model As Proposal = Me.DocumentSession.Load(Of Proposal)(idProposal)

        If model Is Nothing Then
            Throw New CustomException("Antrag nicht gefunden")
        End If

        If Tools.IsAdmin(model.Key_Representation) = False Then
            Throw New CustomException("Für diesen Vorgang hast Du keine Berechtigung")
        End If

        If IsDate(sDate) = True And idStep > 0 Then

            Dim query = From ps As ProposalStep In model.ProposalSteps
                        Where ps.Id = idStep
                        Select ps

            If query.Count > 0 Then
                Dim ps As ProposalStep = query.First
                ps.CreatedAt = sDate
                ps.Timestamp = Tools.GetUnixTimestampFromDate(CType(model.CreatedAt, DateTime))
                StoreAndSave(model)
            End If
        End If

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function SaveProposalStepText(idProposal As String,
                                         idStep As Integer,
                                         sInfo As String) As JsonResult

        Dim model As Proposal = Me.DocumentSession.Load(Of Proposal)(idProposal)

        If model Is Nothing Then
            Throw New CustomException("Antrag nicht gefunden")
        End If

        If Tools.IsAdmin(model.Key_Representation) = False Then
            Throw New CustomException("Für diesen Vorgang hast Du keine Berechtigung")
        End If

        If idStep > 0 Then

            Dim query = From ps As ProposalStep In model.ProposalSteps
                        Where ps.Id = idStep
                        Select ps

            If query.Count > 0 Then
                Dim ps As ProposalStep = query.First
                ps.Info = sInfo
                StoreAndSave(model)
            End If
        End If

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function DeleteProposal(idProposal As String) As JsonResult

        'geht so nicht wg. unterschiedlicher Session...
        'Dim model As Proposal = Proposals.GetById(idProposal)

        Dim model As Proposal = Me.DocumentSession.Load(Of Proposal)(idProposal)

        If model Is Nothing Then
            Throw New CustomException("Antrag nicht gefunden")
        End If

        If Tools.IsAdmin(model.Key_Representation) = False Then
            Throw New CustomException("Für diesen Vorgang hast Du keine Berechtigung")
        End If

        Me.DocumentSession.Delete(model)
        Me.DocumentSession.SaveChanges()

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = model.Representation.FullUrl}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function DeleteProposalStep(idProposal As String, idStep As Integer) As JsonResult

        'geht so nicht wg. unterschiedlicher Session...
        'Dim model As Proposal = Proposals.GetById(idProposal)

        Dim model As Proposal = Me.DocumentSession.Load(Of Proposal)(idProposal)
        If model Is Nothing Then Throw New CustomException("Antrag nicht gefunden")

        If Tools.IsAdmin(model.Key_Representation) = False Then
            Throw New CustomException("Für diesen Vorgang hast Du keine Berechtigung")
        End If

        Dim psRemove As ProposalStep = (From ps As ProposalStep In model.ProposalSteps
                                        Order By ps.Id
                                        Select ps).Last

        If psRemove Is Nothing OrElse psRemove.Id <> idStep Then
            Throw New CustomException("Antragsschritt nicht gefunden oder er ist nicht der letzte")
        End If

        If psRemove.Id = 1 Then
            Throw New CustomException("Der Eingangsschritt kann nicht gelöscht werden")
        End If

        model.ProposalSteps.Remove(psRemove)

        Dim psLast As ProposalStep = (From ps As ProposalStep In model.ProposalSteps
                                      Order By ps.Id
                                      Select ps).Last

        model.ID_CurrentProposalStep = psLast.Id

        Me.DocumentSession.Store(model)
        Me.DocumentSession.SaveChanges()

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = model.FullUrl}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function CreateProposalComment(idProposal As String,
                                          commentBy As String,
                                          commentText As String) As JsonResult

        Dim model As Proposal = Proposals.GetById(idProposal)

        If model Is Nothing Then
            Throw New Exception("Ein Antrag mit der angegebenen ID wurde nicht gefunden")
        End If

        If model.ProposalComments Is Nothing Then
            model.ProposalComments = New List(Of ProposalComment)
        End If

        Dim pc As New ProposalComment With {.ID_Proposal = idProposal,
                                            .CommentedAt = DateAndTime.Now.ToString,
                                            .CommentedBy = commentBy,
                                            .Comment = commentText.EnsureMarkdown}

        model.ProposalComments.Add(pc)

        Me.StoreAndSave(model)

        MailManager.SendNewProposalComment(pc, model)
        'PushoverManager.SendNewProposalComment(pc, model)
        NotificationManager.StoreNewProposalComment(pc, model)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = Me.RenderPartialViewToString("_ProposalCommentPartial", pc)}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function DeleteProposalComment(proposalId As String,
                                          commentedby As String,
                                          timeStamp As Integer) As JsonResult

        Dim model As Proposal = Proposals.GetById(proposalId)
        If model Is Nothing Then Throw New Exception("Ein Antrag mit der angegebenen ID wurde nicht gefunden")

        Dim rep As Representation = model.Representation

        If Tools.IsAdmin(rep.Key) = False Then
            Throw New CustomException("Dazu fehlt Dir die Berechtigung")
        End If

        Dim query = From c As ProposalComment In model.ProposalComments
                    Where c.CommentedBy.ToLower = commentedby.ToLower And c.CommentedAtTimestamp = timeStamp
                    Select c

        If query.Count > 0 Then
            Dim pc As ProposalComment = query.First
            model.ProposalComments.Remove(pc)
        End If

        Me.StoreAndSave(model)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function GetNextStepHtml(keyRepresentation As String,
                                    idStep As Integer) As JsonResult

        Dim rep As Representation = GlobalData.Representations.GetByKey(keyRepresentation.ToLower)
        Dim ps As ProcessStep = Nothing

        Dim query = From p As ProcessStep In rep.ProcessSteps
                    Where p.ID = idStep
                    Select p

        If query.Count > 0 Then
            ps = query.First
        Else
            Throw New Exception("Der Prozessschritt konnte nicht geladen werden")
        End If

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = Me.RenderPartialViewToString("_NextStepPartial", ps)}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function SaveNextStep(idProposal As String,
                                 keyRepresentation As String,
                                 idStep As Integer,
                                 info As String,
                                 options As String) As JsonResult

        Dim model As Proposal = Proposals.GetById(idProposal)

        If model Is Nothing Then
            Throw New Exception("Ein Antrag mit der angegebenen ID wurde nicht gefunden")
        End If

        Dim keyRepresentative As String = Nothing
        Dim keyCommittee As String = Nothing

        Dim aOptions As String() = options.Split(",")
        For Each s As String In aOptions
            Dim aOption As String() = s.Split("|") 'siehe representation.js > saveNextStep
            Select Case aOption(0) 'siehe ProcessStep.GetCaptionHtml
                Case "Key_Representative" : keyRepresentative = aOption(1)
                Case "Key_Committee" : keyCommittee = aOption(1)
            End Select
        Next

        Proposals.SaveNextStep(model, idStep, info,
                               keyRepresentative,
                               keyCommittee)

        NotificationManager.StoreNextProposalStep(model)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function SendProposalAbuseNotice(idProposal As String,
                                            abuseNotice As String) As JsonResult

        Dim model As Proposal = Proposals.GetById(idProposal)

        If model Is Nothing Then
            Throw New Exception("Ein Antrag mit der angegebenen ID wurde nicht gefunden")
        End If

        Dim pan As New ProposalAbuseNotice(model, abuseNotice)
        MailManager.SendNewProposalAbuseNotice(pan)
        PushoverManager.SendNewProposalAbuseNotice(pan)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function SaveProposalAbuseMessage(idProposal As String,
                                             abuseMessage As String) As JsonResult

        Dim model As Proposal = Proposals.GetById(idProposal)

        If model Is Nothing Then
            Throw New Exception("Ein Antrag mit der angegebenen ID wurde nicht gefunden")
        End If

        model.AbuseMessage = abuseMessage

        Me.StoreAndSave(model)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function SaveCommentingStatus(idProposal As String,
                                         lock As Boolean) As JsonResult

        Dim model As Proposal = Proposals.GetById(idProposal)

        If model Is Nothing Then
            Throw New Exception("Ein Antrag mit der angegebenen ID wurde nicht gefunden")
        End If

        If lock = True Then
            model.CommentingClosedDate = Format(DateTime.Now, "dd.MM.yyyy HH:mm:ss")
        Else
            model.CommentingClosedDate = ""
        End If

        Me.StoreAndSave(model)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function GetProposalTags() As JsonResult

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ProposalTags.TagsOrdered}

        Return jr

    End Function

    <HandleErrorAsJson()>
    Public Function GetTagCloudItems() As JsonResult

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ProposalTags.TagCloudItems}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function SaveProposalTags(idProposal As String,
                                     tagList As String) As JsonResult

        Dim prop As Proposal = Proposals.GetById(idProposal)
        Dim tags As ProposalTags = ProposalTags.Load()

        Try
            Proposals.SaveTags(prop, tags, tagList)

            Me.StoreAndSave(prop)
            Me.StoreAndSave(tags)

        Catch ex As Exception
            Throw ex
        End Try

        'Rückgabe...
        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = Me.RenderPartialViewToString("_ProposalTagListPartial", prop)}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function SetSuccessStoryStatus(idProposal As String,
                                          status As Integer)

        Dim model As Proposal = Proposals.GetById(idProposal)
        model.SuccessStoryStatus = status
        Me.StoreAndSave(model)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

    <HandleErrorAsJson()>
    <Authorize()>
    Public Function CreateSuccessStory(idProposal As String,
                                       title As String,
                                       stepDate As String,
                                       text As String) As JsonResult

        Dim prop As Proposal = Proposals.GetById(idProposal)

        Dim sShortUrl As String = UrlShortener.GetShortUrl(String.Concat(prop.FullUrl, "/erfolg"))

        Dim model As New SuccessStory With {
            .ID_Proposal = idProposal,
            .Title = title,
            .StepDate = stepDate,
            .Text = text,
            .ShortUrl = sShortUrl
        }
        Me.StoreAndSave(model)

        prop.SuccessStoryStatus = 1
        prop.ID_SuccessStory = model.Id
        Me.StoreAndSave(prop)

        PushoverManager.SendNewSuccessStory(model)
        TwitterManager.TweetNewSuccessStory(model)
        NotificationManager.StoreNewSuccessStory(model)
        PushbulletManager.SendNewSuccessStory(model)

        Dim jr As New JsonResult
        jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet
        jr.Data = New With {.success = True, .data = ""}

        Return jr

    End Function

#End Region

End Class