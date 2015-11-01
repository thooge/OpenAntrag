@Imports OpenAntrag
@Modeltype CustomErrorInfo

@Code
    ViewData("Title") = "Uups, ein Fehler"
    
    Dim message As String
    
    If Model IsNot Nothing Then
        If Model.Origin = CustomErrorInfo.OriginEnum.Redirect OrElse TypeOf Model.Exception Is CustomException Then
            message = Model.ErrorMessage
        Else
            message = "Es ist ein interner Fehler aufgetreten"
        End If
    Else
        message = "Es ist kein Fehler aufgetreten"
    End If
    
End Code

@Section Styles
    @Styles.Render("~/css/error")
End Section

@Section Intro
    <p>Fehler sind die Würze, die dem Erfolg sein Aroma geben<br />- Truman Capote</p>
End Section


<div class="content container-fluid">
    <div class="row-fluid">
        <div id="intention" class="span12 box-head">
            <i class="icon-attention"></i>
            <h2>Uups,<br />ein Fehler</h2>
            <br />
            <p style="font-size:1.6em;">@message</p>
            @If Model.ReferrerUrl IsNot Nothing Then
                @<p><a href="@Model.ReferrerUrl">zurück zur letzten Seite</a>.</p>
            End If
        </div>
    </div>
</div>
