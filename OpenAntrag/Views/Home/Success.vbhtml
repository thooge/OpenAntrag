@Imports OpenAntrag
@ModelType List(Of SuccessStory)


@Code
    ViewData("Title") = "Erfolgsgeschichten"

    Dim pageNo As Integer = 0
    If ViewData("PageNo") IsNot Nothing Then
        pageNo = CType(ViewData("PageNo"), Integer)
    End If
    
    Dim itemsCount As Integer = 0
    If ViewData("ItemsCount") IsNot Nothing Then
        itemsCount = CType(ViewData("ItemsCount"), Integer)
    End If

End code

@Section Styles
    @Styles.Render("~/css/success")
End Section

@Section Scripts
    <script>
        $(document).ready(function () {            
        });
    </script>
End Section

@Section Intro
    <p>
        Etwas zu verändern ist nicht leicht, aber es geht, wie unsere erfolgreichen Bürgeranträge beweisen.
    </p>
End Section

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span5 box-head">
            <i class="icon-light-up"></i>
            <h2>Die&nbsp;<br />
                Erfolgs<br />
                geschichten
            <br />    
            <br />        
        </div>
        <div class="span7 box">
            <p>
                Diese Seite dient dazu die erfolgreiche Arbeit der an OpenAntrag teilnehmenden 
                Fraktionen/Einzelabgeordneten sichtbar zu machen. Wurde ein Bürgerantrag im 
                Parlament positiv abgestimmt, kann die Fraktion diesen im Anschluss hier einstellen,
                um den Erfolg zu dokumentieren.
            </p>
            <p>Im folgenden findest Du diese <strong>@(itemsCount)</strong> Erfolgsgeschichten.</p>
            @If itemsCount < 10 Then
                @<p style="font-style: italic; font-weight: bold;">
                    <span style="text-decoration: underline;">Hinweis:</span> 
                    Diese Seite ist erst seit Kurzem in OpenAntrag verfügbar. 
                    Die Fraktionen arbeiten daran schnellstmöglich die in den vergangenen Monaten erfolgreich 
                    abgeschlossenen Anträge hier einzustellen.
                </p>
            End If
        </div>
    </div>
</div>

@Code
    Dim bolShaded As Boolean = True
    
    If Model IsNot Nothing Then

        Dim pager As PagerModel = Nothing        
        If itemsCount > 0 Then
        
            pager = New PagerModel(pageNo, itemsCount, String.Concat("/erfolge/@Page"))
                
            For Each suc As SuccessStory In Model
                Html.RenderPartialWithData("_SuccessStoryBlockPartial", suc, New With {.Shaded = bolShaded})
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
