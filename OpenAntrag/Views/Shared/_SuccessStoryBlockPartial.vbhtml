@Imports OpenAntrag
@ModelType SuccessStory

@Code
    Dim bolShaded As Boolean = ViewData("Shaded")
End Code

<div data-id="@Model.Id" class="content @(IIf(bolShaded=true, "content-shaded", "")) container-fluid">
    <div class="row-fluid">
        <div class="span12 success-item rid@(Model.Proposal.Representation.ID)">
            <a class="btn btn-tiny" href="@(Model.Proposal.Representation.FullUrl)">
                @(Model.Proposal.Representation.Label)
            </a>
            <small>@(Model.StepDateFormat)</small>
            <h3><a href="@(Model.FullUrl)">@(Model.Title)</a></h3>
            <div class="proposal-body">@Html.Raw(Model.TextHtml)</div>
            @Html.Partial("_SuccessStorySublinksPartial", Model)
        </div>
    </div>
</div>