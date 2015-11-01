@Imports OpenAntrag
@ModelType ProposalTag

@Code
    ViewData("Title") = "Themen"

    Dim pageNo As Integer = 0
    If ViewData("PageNo") IsNot Nothing Then
        pageNo = CType(ViewData("PageNo"), Integer)
    End If
    
End Code

@Section Styles
    @Styles.Render("~/css/tags")
End Section

@Section Scripts
    <script>
        $(document).ready(function () {            
            initTagCloud("tag-cloud-container");
            if ($("#tags-header").length > 0) {
                scrollToOffset($("#tags-header"), 500);
            };            
            initReadMore('.proposal-text');
        });
    </script>
End Section

@Section Intro
    <p>Piraten stehen für Themen, für eine Politik in der Sache, über Grenzen hinweg.</p>
End Section

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span4 box-head">
            <i class="icon-tags"></i>
            <h2>Die&nbsp;<br />
                Themen</h2>
            <br />
            <p>Die Bürgeranträge werden von den Fraktionen mit Themenschlagworten versehen. 
                Nach diesen kannst Du hier fraktionsübergreifend filtern.</p>
            @If Model Is Nothing Then
                @<p><strong>Bitte wähle ein Thema...</strong></p>
            End If
        </div>
        <div class="span8 box">
            <div id="tag-cloud-container"></div>
        </div>
    </div>
</div>

@Code
    If Model IsNot Nothing Then

        @<div id="tags-header" class="content content-inverse container-fluid">
            <div class="row-fluid">
                <div class="span12 box-head">
                    <i class="icon-tag"></i>
                    <h2><span style="color:#333;">Thema:</span><br />@Model.Tag</h2>
                </div>
            </div>
        </div>

        Dim bolShaded As Boolean = False
        Dim pager As PagerModel = Nothing
        Dim lst As List(Of Proposal) = Proposals.GetItemsByTag(Model)
        If lst IsNot Nothing AndAlso lst.Count > 0 Then
        
            pager = New PagerModel(pageNo, lst.Count, String.Concat("/tags/", HttpUtility.UrlEncode(Model.Tag), "/@Page"))
            lst.GetPageData(pageNo)
                
            For Each prop In lst
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