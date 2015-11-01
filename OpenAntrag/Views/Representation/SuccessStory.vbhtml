@Imports OpenAntrag
@ModelType SuccessStory

@Code
    ViewData("Title") = "Erfolgsgeschichte " & Model.Proposal.Representation.Name & " | " & Model.Proposal.Title
End Code

@Section Styles
    @Styles.Render(String.Concat("~/css/proposal-", Model.Proposal.Representation.Key))
    <link href="/@(Model.Proposal.Representation.Key)/style-representation" rel="stylesheet" type="text/css" media="screen" />
End Section

@Section Scripts
    @Scripts.Render("~/bundle/representations")
    @Scripts.Render("~/bundle/markdown")
    <script>
        $(document).ready(function () {            
        });
    </script>
End Section

@Section Intro
    @Html.PartialOrNull("_RepresentationIntro", Model.Proposal.Representation)
End Section

@Section RepNav
    @Html.PartialOrNull("_NavRepresentation", Model.Proposal.Representation)
End Section

<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span10 box-head">
            <i class="icon-light-up"></i>
            <div class="proposal-head">
                <small>@Model.StepDateFormat</small>
                <h4 style="margin:0; line-height: 18px; margin-bottom: 10px;">Die Erfolgsgeschichte des Antrags</h4>
                <h2 style="margin:0">@Model.Title</h2>
            </div>
            <div class="proposal-body">@Html.Raw(Model.TextHtml)</div>
            @Html.Partial("_SuccessStorySublinksPartial", Model)
        </div>
    </div>
</div>

@* --- Antrag --- *@
<div class="content content-representation content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span10 box-head">
            <i class="icon-doc proposal-step-icon" style="color: @(Model.Proposal.CurrentProposalStep.ProcessStep.Color)"></i>
            <div class="proposal-head">
                <small>@Model.Proposal.CreatedAtFormat</small>
                <h2><a style="color: #333" href="@(Model.Proposal.FullUrl)">Der Antrag</a></h2>
            </div>
            <div class="proposal-body">@Html.Raw(Model.Proposal.TextHtml)</div> 
        </div>
    </div>
</div>

@Code
    Dim bolShaded As Boolean = False
End Code

@* --- Schrittliste --- *@
@For Each ps As ProposalStep In Model.Proposal.ProposalStepList
    @<div class="content @(IIf(bolShaded=true, "content-shaded", "")) container-fluid">
        <div class="row-fluid">
            <div class="span12 proposalstep-item">
                <span class="bar" style="background-color: @ps.ProcessStep.Color;">&nbsp;</span>
                <div class="proposalstep-body">
                    <img src="/Images/Icons/@ps.ProcessStep.Icon">
                    <small>@ps.CreatedAtFormat</small>
                    <em>@Proposals.GetProcessStepCaption(ps.ProcessStep.Caption, Model.Proposal, Model.Proposal.Representation)</em>                    
                    <div class="info">@Html.Raw(ps.InfoHtml)</div>
                </div>
            </div>
        </div>
    </div>
    bolShaded = Not bolShaded
Next

@* --- Kommentare --- *@
<a name="comments"></a>
<div id="comment-new" class="content @(IIf(bolShaded=true, "content-shaded", "")) content-representation container-fluid">
    <div class="row-fluid">
        <div class="span12 box-head">
            <i class="icon-comment"></i>
            <h2>Die<br />
                Kommentare</h2>
            <br />
            
            @If Model.Proposal.IsCommentingClosed = False Then
                @<button class="btn btn-small btn-primary" onclick="showProposalComment(this);" style="margin: 10px 0;">Neuer Kommentar...</button>
                
                @If Tools.IsAdmin(Model.Proposal.Representation.Key) = True Then
                    @<a onclick="closeCommenting('@Model.Id'); return false;" href="javascript:go();" style="color: #E80000;"
                        class="tt-std cmd btn btn-small" title="Kommentarfunktion schließen"><i class="icon-lock"></i></a>                    
                End if

                @Using Html.BeginForm("CreateProposalComment", "Proposal", FormMethod.Post, New With {.id = "newcomment-form", .name = "newcomment-form", .style = "display:none;"})
                    @Html.Partial("_NewProposalCommentPartial", New ProposalComment(Model.Proposal))
                End Using
            Else
                @<p style="margin: 5px 10px 15px 0; display: inline-block;">Kommentarfunktion wurde geschlossen am @(CType(Model.Proposal.CommentingClosedDate, DateTime).ToString("dd. MMMM yyyy HH:mm"))</p>
                @If Tools.IsAdmin(Model.Proposal.Representation.Key) = True Then
                    @<a onclick="reopenCommenting('@Model.Id'); return false;" href="javascript:go();" style="color: #028C00;"
                        class="tt-std cmd btn btn-small" title="Kommentarfunktion wieder öffnen"><i class="icon-lock-open"></i></a>                                            
                End If
            End If

            <div id="proposal-comments">
                @For Each pc As ProposalComment In Model.Proposal.ProposalComments
                    pc.ID_Proposal = Model.Id
                    Html.RenderPartial("_ProposalCommentPartial", pc)
                Next
            </div>
        </div>
    </div>
</div>
