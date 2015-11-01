@Imports OpenAntrag
@ModelType FeedbackComment

<div class="comment">
    @If Tools.IsAdmin = True Then
        @<div class="commands">
            <a onclick="deleteFeedbackComment('@Model.ID_Feedback', this); return false;" href="javascript:go();" 
               class="btn btn-small tt-std" title="Kommentar löschen"
               data-timestamp="@Model.Timestamp"
               data-commentedby="@Model.CommentedBy"><i class="icon-trash"></i></a>
        </div>
    End If
    <span class="brace">&nbsp;</span>
    <div class="markdown-text">
        <small>@Model.CommentedBy am @Model.CommentedAtFormat</small>
        <div>@Model.CommentHtml</div>
    </div>
</div>
<br />