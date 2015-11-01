@Imports OpenAntrag
@ModelType Representation

@Code
    ViewData("Title") = Model.Name & " | Liste"
End Code

@Section Styles
    @Styles.Render(String.Concat("~/css/representations-", Model.Key))
    <link href="/@(Model.Key)/style-representation" rel="stylesheet" type="text/css" media="screen" />
End Section

@Section Scripts
    @Scripts.Render("~/bundle/representations")
    <script>
        $(document).ready(function () {
            $("#proposallist-table tr").each(function () {
                var jA = $(this).find("td").first().find("a");
                $(this).click(function () {
                    window.location.href = jA.attr("href");
                });
            });
            $("table#proposallist-table").tablesorter({
                sortList: [[1, 1]]
            });
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
    Dim lst As List(Of Proposal) = Proposals.GetByRepresentation(Model)
    'Dim lst As List(Of Proposal) = Proposals.GetItemsPageByRepresentation(Model, 1, SettingsWrapper.DefaultPagerListPageSize)    
End Code

<div class="content content-navigation content-representation content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span7">
            <div class="btn-group btn-group-invers">
                <a class="btn btn-small btn-primary" href="/@(Model.Key)/journal">Antragsjournal</a>
                <a class="btn btn-small btn-primary btn-selected" href="/@(Model.Key)/liste">Antragsliste</a>
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

<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span6 box box-head" style="position: relative;">
            <i class="icon-tasks"></i>
            <h2>Die&nbsp;<br />
                Antragsliste</h2>
        </div>
        <div class="span6">
            <p>
                Nachfolgend findest Du alle <strong>@(lst.Count)</strong> eingangenen 
                Bürgeranträge für @(Model.LabelWithArticle)
                als sortierbare Liste.
            </p>
        </div>
    </div>
</div>

<div class="content content-representation container-fluid">
    <div class="row-fluid" id="proposallist-wrapper">
    @If lst IsNot Nothing AndAlso lst.Count > 0 Then        
        @Html.Partial("_ProposalListTablePartial", lst, New ViewDataDictionary() From {{"ShowRepresentation", False}})
    Else
        @<div class="span12 box">
            <p>Es wurden bislang noch keine Anträge eingestellt. <strong>Sei der Erste...!</strong></p>
        </div>
    End If
    </div>
</div>

@Section PreFooter
    @Html.PartialOrNull("_BannerTeaserPartial", Model)
End Section