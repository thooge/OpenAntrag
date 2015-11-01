@Imports OpenAntrag
@ModelType List(Of Feedback)

@Code
    ViewData("Title") = "Feedback"
    Dim intFilterId As Integer = ViewBag.FilterId
End Code

@Section Styles
    @Styles.Render("~/css/feedback")
End Section

@Section Scripts
    @Scripts.Render("~/bundle/feedback")
    @Scripts.Render("~/bundle/markdown")
    <script>
        $(document).ready(function () {
        });
    </script>
End Section

@Section Intro
    <p>Es gibt immer was zu tun, wenn man denn weiß was.<br />
        Lasst uns OpenAntrag noch besser machen...
    </p>
End Section

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span5 box-head">
            <i class="icon-megaphone"></i>
            <h2>Das&nbsp;<br />
                Feedback</h2>
            <br />
            <p>Eine Website wie diese lebt von ihren Nutzern und entwickelt sich weiter durch ihre Nutzer.
                Jeder ist aufgerufen hier seinen Kommentar abzugeben, einen Verbesserungsvorschlag 
                zu machen oder gar einen Fehler zu melden, damit dieser schnellst möglich behoben werden kann.
            </p>
            <p>Alle bisher eingegangenen Feedbacks findest Du hier in chronologischer Reihenfolge und filterbar über folgende Kategorien:</p>
            <div id="fbt-filter">
                <a class="cmd-filter fbt-type-all @(IIf(intFilterId = -1, "selected", ""))" href="/feedback">Alle</a>
                @For Each ft As FeedbackType In GlobalData.FeedbackTypes.Items
                    @<a class="cmd-filter fbt-type-@(ft.ID) @(IIf(intFilterId = ft.ID, "selected", ""))" href="/feedback/@(ft.Key)">@(ft.Name)</a>
                Next
            </div>
        </div>
        <div class="span6 offset1 box">
            <h3>Dein Feedback</h3>
            @Using Html.BeginForm("CreateFeedback", "Feedback", FormMethod.Post, New With {.id = "newfeedback-form", .name = "newfeedback-form"})
                @Html.Partial("_NewFeedbackPartial", New Feedback)                
            End Using
        </div>
    </div>
</div>

@Code
    Dim bolShaded As Boolean = True
End Code

@For Each fb As Feedback In Model
    
    Dim fbType As FeedbackType = (From fbt As FeedbackType In GlobalData.FeedbackTypes.Items
                                  Where fbt.ID = fb.Type
                                  Select fbt).FirstOrDefault
    
    @<a name="@(fb.IdNumber)"></a>
    @<div class="content @(IIf(bolShaded=true, "content-shaded", "")) container-fluid">
        <div class="row-fluid">
            <div class="span12 box feedbackbox">
                <i class="icon-@(fbType.Icon)" style="color: @(fbType.Color)"></i>
                <div class="feedback" id="@fb.Id">
                    <small>@fb.CreatedBy am @fb.CreatedAtFormat</small>
                    <h4>@fb.Title</h4>
                    <div>@fb.MessageHtml</div>

                    <button class="btn btn-small btn-primary" onclick="showFeedbackComment('@fb.Id');" style="margin: 10px 0;">Neuer Kommentar...</button>
                    <div class="newcomment-wrapper" style="display: none;"></div>

                    <div class="comments">
                        @If fb.Comments IsNot Nothing Then                                
                            @For Each fbc As FeedbackComment In fb.Comments
                                fbc.ID_Feedback = fb.Id
                                @Html.Partial("_FeedbackCommentPartial", fbc)
                            Next
                        End If
                    </div>
                </div>            
            </div>
        </div>
    </div> 
    bolShaded = Not bolShaded
Next

<div id="CommentFormWrapper" style="display:none;">
    @Using Html.BeginForm("CreateFeedbackComment", "Feedback", FormMethod.Post, New With {.id = "newcomment-form", .name = "newcomment-form"})
        @Html.Partial("_NewFeedbackCommentPartial", New FeedbackComment())
    End Using
</div>
