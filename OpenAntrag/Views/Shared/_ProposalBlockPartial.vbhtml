@Imports OpenAntrag
@ModelType Proposal

@Code
    Dim bolShaded As Boolean = ViewData("Shaded")
    Dim bolShowRep As Boolean = ViewData("ShowRepresentation")
End Code

<div data-id="@Model.Id" class="content content-proposal @(IIf(bolShaded=true, "content-shaded", "")) container-fluid">
    <div class="row-fluid">
        <div class="span10 proposal-item rid@(Model.Representation.ID)">
            <a style="background-color: @Model.CurrentProposalStep.ProcessStep.Color;" 
               href="@Model.FullUrl">
                <i class="icon-right-open"></i>
                <div class="proposal-steps">
                    @For Each ps As ProposalStep In Model.ProposalStepStack
                        @<span style="background: @ps.ProcessStep.Color;">&nbsp;</span>                        
                    Next
                </div>
            </a>
            <div class="proposal-body">
                @If bolShowRep = True Then
                    @<a href="/@Model.Representation.Key" class="representation">@Model.Representation.[Name]</a>
                End If
                <img src="/Images/Icons/@Model.CurrentProposalStep.ProcessStep.Icon">
                <em>@Proposals.GetProcessStepCaption(Model.CurrentProposalStep.ProcessStep.Caption, Model, Model.Representation)</em>
                <small>@Model.CreatedAtFormat</small>

                <h3><a href="@Model.FullUrl">@Model.Title</a></h3>
                @If Model.IsAbuse = True Then
                    @<div class="alert alert-error" style="display: inline-block; margin: 5px -10px; padding-left: 10px;">
                        Dieser Antrag wurde wegen Missbrauchs stumm geschaltet
                        <div class="abuse-message">@Html.Raw(Model.AbuseMessageHtml)</div>
                    </div>
                Else
                    @<div class="proposal-text">@Html.Raw(Model.TextHtml)</div>
                End If
                @Html.Partial("_ProposalSublinksPartial", Model)
            </div>
        </div>
        <div class="span2 proposal-tags"
             style="@(IIf(Model.HasTags = true,"display:block;", "display:none;"))">
            @Html.Partial("_ProposalTagListPartial", Model)
        </div>
    </div>
</div>
