@Imports OpenAntrag
@ModelType List(Of Proposal)

@Code
    ViewData("Title") = "Antragsjournal"
    
    Dim itemsShown As Integer = CType(ViewData("ItemsShown"), Integer)
    Dim itemsCount As Integer = CType(ViewData("ItemsCount"), Integer)

    'Dim rps As New Representations(Representations.StatusConjuction.Active)
    Dim lst As List(Of Representation) = GlobalData.Representations.Items _
        .Where(Function(x) (x.Status And Representations.StatusConjuction.Active) > 0) _
        .ToList()

End Code

@Section Styles
    @Styles.Render("~/css/list")
End Section

@Section Scripts
    <script>
        $(document).ready(function () {
            initProposalList();
            $("#inslideCount").inslide({
                moreItems: ['100', '150', '200', '@(itemsCount)'],
                itemClick: function (s) { getProposalListTable(s); }
            });
        });
    </script>
End Section

@Section Intro
    <p>Jeder Bürgerantrag ist ein Zeichen dass Demokratie funktioniert, 
        sofern man denn will und jeder mitwirken kann der möchte.</p>
End Section

<div class="content content-navigation content-home content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span7">
            <div class="btn-group btn-group-invers">
                <a class="btn btn-small btn-primary" href="/journal">Antragsjournal</a>
                <a class="btn btn-small btn-primary btn-selected" href="/liste">Antragsliste</a>
            </div>
        </div>
        <div class="span5 right">
            <p>
                Es ist nicht leicht etwas zu verändern, aber es geht!<br />
                <a href="/erfolge"><strong>Schau Dir unsere Erfolgsgeschichten an...</strong></a>
            </p>
        </div>
    </div>
</div>

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span5 box-head">
            <i class="icon-tasks"></i>
            <h2>Die&nbsp;<br />
                Antragsliste</h2>
        </div>
        <div class="span7 box">
            <p>
                In dieser Liste findest Du die letzten 
                <span id="inslideCount" class="inslide">@(SettingsWrapper.ProposalListCount)</span>
                von <strong>@(itemsCount)</strong> Bürgeranträgen, die auf OpenAntrag in die 
                <strong>@(lst.Count)</strong> teilnehmenden Parlamente (Fraktionen) 
                eingestellt wurden, als sortierbare Liste. 
            </p>
        </div>
    </div>
</div>

<div class="content content-representation container-fluid">
    <div class="row-fluid" id="proposallist-wrapper">
        @Html.Partial("_ProposalListTablePartial", Model, New ViewDataDictionary() From {{"ShowRepresentation", True}})
    </div>
</div>