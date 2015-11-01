@Imports OpenAntrag
@ModelType Representation

@Code
    ViewData("Title") = Model.Name & " | Journal"

    Dim pageNo As Integer = 0
    If ViewData("PageNo") IsNot Nothing Then
        pageNo = CType(ViewData("PageNo"), Integer)
    End If
End Code

@Section Styles
    @Styles.Render(String.Concat("~/css/representations-", Model.Key))
    <link href="/@(Model.Key)/style-representation" rel="stylesheet" type="text/css" media="screen" />
End Section

@Section Scripts
    @Scripts.Render("~/bundle/representations")
    <script>
        $(document).ready(function () {
            initReadMore('.proposal-text');
        });
    </script>
End Section

@Section Intro
    @Html.PartialOrNull("_RepresentationIntro", Model)    
End Section

@Section RepNav
    @Html.PartialOrNull("_NavRepresentation", Model)    
End Section

@Code
    Dim bolShaded As Boolean = True
    Dim intCount As Integer = Proposals.GetItemsCountByRepresentation(Model)
    Dim lst As List(Of Proposal) = Proposals.GetItemsPageByRepresentation(Model, pageNo, SettingsWrapper.DefaultPagerListPageSize)
    Dim pager As PagerModel = New PagerModel(pageNo, intCount, String.Concat("/", Model.Key, "/journal/@Page"))
End Code

<div class="content content-navigation content-representation content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span7">
            <div class="btn-group btn-group-invers">
                <a class="btn btn-small btn-primary btn-selected" href="/@(Model.Key)/journal">Antragsjournal</a>
                <a class="btn btn-small btn-primary" href="/@(Model.Key)/liste">Antragsliste</a>
            </div>
        </div>
        @If (Model.Status And Representations.StatusConjuction.Ended) = 0 Then
            @<div class="span5 right">
                <p>
                    Du hast eine Idee für @(Model.LabelWithArticle)?<br />
                    <a href="@(Model.FullUrl)/neu"><strong>Schreib jetzt Deinen Antrag!</strong></a>
                </p>
            </div>
        End If
    </div>
</div>

<div id="journal-container" class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span6 box box-head">
            <i class="icon-tasks"></i>
            <h2>Das&nbsp;<br />
                Antragsjournal
                @If pager.MaxPages > 1 Then
                    @<span class="page-number">_@pageNo</span>
                End If
            </h2>
        </div>
        <div class="span6">
            <p>
                Nachfolgend findest Du alle <strong>@(intCount)</strong> eingangenen 
                Bürgeranträge für @(Model.LabelWithArticle)
                in voller Länge und sortiert nach Eingang.
            </p>
        </div>
    </div>
</div>

@Code
    If lst IsNot Nothing AndAlso intCount > 0 Then
                
        'lst.GetPageData(pageNo)
                
        For Each prop In lst
            prop.FillProcessSteps()
            Html.RenderPartialWithData("_ProposalBlockPartial", prop, New With {.Shaded = bolShaded,
                                                                                .ShowRepresentation = False})
            bolShaded = Not bolShaded
        Next
    Else
        @<div class="content container-fluid">
            <div class="row-fluid">
                <div class="span12 box">
                    <p>Es wurden bislang noch keine Anträge eingestellt. <strong>Sei der Erste...!</strong></p>
                </div>
            </div>
         </div>
    End If
End Code

@If pager.MaxPages > 1 Then
    @<div class="content content-proposal @(IIf(bolShaded=true, "content-shaded", "")) container-fluid">
        <div class="row-fluid">
            @Html.Pager(pager)
        </div>
    </div>   
End If

@Section PreFooter
    @Html.PartialOrNull("_BannerTeaserPartial", Model)
End Section