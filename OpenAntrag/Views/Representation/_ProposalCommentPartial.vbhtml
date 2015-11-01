@Imports OpenAntrag
@ModelType ProposalComment

<div class="comment">
    @If Tools.IsAdmin = True Then
        @<div class="commands">
            <a onclick="deleteProposalComment('@Model.ID_Proposal', this); return false;" href="javascript:go();" 
               class="btn btn-small tt-std" title="Kommentar löschen"
               data-timestamp="@Model.CommentedAtTimestamp"
               data-commentedby="@Model.CommentedBy"><i class="icon-trash"></i></a>
        </div>
    End If
    <span class="brace">&nbsp;</span>
    <div class="markdown-text">
        <small>@Model.CommentedAtFormat</small>
        <h4>@Model.CommentedBy</h4>
        @Html.Raw(Model.CommentHtml)
    </div>
</div>
<br />
