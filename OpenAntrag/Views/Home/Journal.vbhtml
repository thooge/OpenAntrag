@Imports OpenAntrag
@ModelType List(Of Proposal)

@Code
    ViewData("Title") = "Antragsjournal"

    Dim pageNo As Integer = 0
    If ViewData("PageNo") IsNot Nothing Then
        pageNo = CType(ViewData("PageNo"), Integer)
    End If
    
    Dim itemsCount As Integer = 0
    If ViewData("ItemsCount") IsNot Nothing Then
        itemsCount = CType(ViewData("ItemsCount"), Integer)
    End If

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
            initReadMore('.proposal-text');
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
                <a class="btn btn-small btn-primary btn-selected" href="/journal">Antragsjournal</a>
                <a class="btn btn-small btn-primary" href="/liste">Antragsliste</a>
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
        <div class="span6 box-head">
            <i class="icon-tasks"></i>
            <h2>Das&nbsp;<br />
                Antragsjournal
            <br />
            <br />
            <div id="toc"></div>
        </div>
        <div class="span6 box">
            <p>
                An dieser Stelle findest Du alle <strong>@(itemsCount)</strong> 
                Bürgeranträge, die auf OpenAntrag bislang in die 
                <strong>@(lst.Count)</strong> teilnehmenden Parlamente (Fraktionen) 
                eingestellt wurden, in voller Länge und sortiert nach Eingang. </p>
        </div>
    </div>
</div>

@Code
    Dim bolShaded As Boolean = True
    
    If Model IsNot Nothing Then

        Dim pager As PagerModel = Nothing        
        If itemsCount > 0 Then
        
            pager = New PagerModel(pageNo, itemsCount, String.Concat("/journal/@Page"))
                
            For Each prop In Model
                prop.FillProcessSteps()
                Html.RenderPartialWithData("_ProposalBlockPartial", prop, New With {.Shaded = bolShaded,
                                                                                    .ShowRepresentation = True})
                bolShaded = Not bolShaded
            Next
        End If

        @<div class="content content-proposal @(IIf(bolShaded=true, "content-shaded", "")) container-fluid">
            <div class="row-fluid">
                @Html.Pager(pager)
            </div>
        </div>

    End If
End Code
