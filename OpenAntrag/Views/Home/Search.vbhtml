@Imports OpenAntrag
@ModelType SearchModel

@Code
    ViewData("Title") = "Suche"

    Dim pageNo As Integer = 0
    If ViewData("PageNo") IsNot Nothing Then
        pageNo = CType(ViewData("PageNo"), Integer)
    End If
    
End Code

@Section Styles
    @Styles.Render("~/css/search")
End Section

@Section Scripts
    <script>
        $(document).ready(function () {
            if ($("#search-header").length > 0) {
                scrollToOffset($("#search-header"), 500);
            };
            initReadMore('.proposal-text');
        });
    </script>l
End Section

@Section Intro
    <p>Jede Idee ist wertvoll, wenn man denn von ihr hört oder liest.<br />
        Wer suchet, der findet.</p>
End Section

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span3 box-head">
            <i class="icon-search"></i>
            <h2>Die<br />
                Suche</h2>
            <br />
            <br />
            <div id="toc"></div>
        </div>
        <div class="span8 offset1 box">
            <p>Täglich das Rad neu zu erfinden ist verschwendete Zeit. 
                Vielleicht hat schon jemand anderes eine Idee gehabt, 
                die man übernehmen kann. Hier kannst Du mit freigewählten 
                Begriffen danach suchen. Gesucht wird im Titel und im Text des Antrags</p>

            @Using Html.BeginForm("Search", "Home", FormMethod.Post, New With {.id = "search-form", .name = "search-form"})
                
                @<div style="margin-top: 15px;">
                    @Html.TextBoxFor(Function(m) m.SearchTerms, New With {.placeholder = "Suchbegriffe"})
                    @Html.ValidationMessageFor(Function(m) m.SearchTerms)
                </div>

                @<div style="margin-top:15px;">
                    <button type="submit" class="btn btn-primary">Suche starten</button>
                </div>
            End Using
        </div>
    </div>
</div>

@Code
    If Model IsNot Nothing Then
        Dim strResultString As String
        Select Case Model.Results.Count
            Case 0 : strResultString = "keine Ergebnisse"
            Case 1 : strResultString = "1 Ergebnis"
            Case Else : strResultString = String.Concat(Model.Results.Count, " Ergebnisse")
        End Select

        @<div id="search-header" class="content content-inverse container-fluid">
            <div class="row-fluid">
                <div class="span12 box-head">
                    <i class="icon-bullseye"></i>
                    <h2><span style="color: #333;">Suche:</span><br />@(strResultString)</h2>
                </div>
            </div>
        </div>
    
        Dim bolShaded As Boolean = False

        For Each prop In Model.Results
            prop.FillProcessSteps()
            Html.RenderPartialWithData("_ProposalBlockPartial", prop, New With {.Shaded = bolShaded,
                                                                                .ShowRepresentation = True})
            bolShaded = Not bolShaded
        Next

    End If
End Code
