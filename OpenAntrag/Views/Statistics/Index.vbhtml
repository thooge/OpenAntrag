@Imports OpenAntrag
@ModelType String
@Code
    ViewData("Title") = "Statistik"
    
    Dim strPart As String = ViewData("StatPartial")
    Dim strPartial As String = Nothing
    
    Dim bolHasPart As Boolean = Not String.IsNullOrEmpty(strPart)    
    If bolHasPart = True Then strPartial = String.Concat("_", strPart, "Partial")
    
    'Dim intRepresentationCount As Integer = (New Representations(Representations.StatusConjuction.Active)).Items.Count
    Dim intRepresentationCount As Integer = GlobalData.Representations.Items _
        .Where(Function(x) x.Status And Representations.StatusConjuction.Active = Representations.StatusConjuction.Active) _
        .Count()
    
    Dim intProposalCount As Integer = Proposals.GetItemsCount()
    Dim intCommentCount As Integer = Proposals.GetItemsCommentCount
    Dim intFeedbackCount As Integer = Feedbacks.GetCount
        
End Code

@Section Styles
    @Styles.Render("~/css/statistics")
End Section

@Section Scripts
    <script src="~/Scripts/Plugins/highcharts/highcharts.js"></script>
    @If bolHasPart = True Then
        @<script>
            $(document).ready(function () {
                init(@(ViewData("StatScroll").ToString.ToLower));
            });
        </script>        
    End If
End Section

@Section Intro
    <p>
        Wo der Glaube versagt, hilft die Statistik<br />
        Die Anträge in Zahlen, Form und Farbe
    </p>
End Section

<div class="content content-home content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <div class="btn-group btn-group-invers">
                <a class="btn btn-small btn-primary @(IIf(strPart = "ProposalCountByRepresentation", "btn-selected", ""))" 
                    href="/statistiken/ProposalCountByRepresentation">Anzahl Anträge je Parlament</a>
                <a class="btn btn-small btn-primary @(IIf(strPart = "RepresentationCountByType", "btn-selected", ""))" 
                    href="/statistiken/RepresentationCountByType">Verteilung Gruppentypen Parlamente</a>
                <a class="btn btn-small btn-primary @(IIf(strPart = "FeedbackCountByType", "btn-selected", ""))" 
                    href="/statistiken/FeedbackCountByType">Verteilung Feedback</a>
            </div>
        </div>
    </div>
</div>

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span5 box-head">
            <i class="icon-chart-line"></i>
            <h2>Die&nbsp;<br />
                Statistiken</h2>
            <br />
        </div>
        <div class="span7 box">
            <p>
                Hier finden Statistik-Fans nützliche Zahlen und Grafiken rund um die 
                <span style="white-space:nowrap"><strong>@(intRepresentationCount)</strong> Parlamente (Fraktionen)</span>,  
                <span style="white-space:nowrap"><strong>@(intProposalCount)</strong> Anträge</span>,
                <span style="white-space:nowrap"><strong>@(intCommentCount)</strong> Antragskommentare</span> und 
                <span style="white-space:nowrap"><strong>@(intFeedbackCount)</strong> Feedback-Beiträge</span>,
            </p>
        </div>
    </div>
</div>

@Code
    If bolHasPart = True Then
        @Html.Partial(strPartial)
    End If    
End Code
