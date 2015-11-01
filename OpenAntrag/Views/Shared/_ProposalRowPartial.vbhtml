@Imports OpenAntrag
@ModelType Proposal

@Code    
    Dim bolShowRep As Boolean = ViewData("ShowRepresentation")
    
    Model.FillProcessSteps()
End Code

<tr data-id="@(Model.Id)">
    <td class="process-marker" style="background-color: @(Model.CurrentProposalStep.ProcessStep.Color);">
        <span style="display:none">@(Model.CurrentProposalStep.ID_ProcessStep)</span>
        <a href="@Model.FullUrl"><i class="icon-right-open"></i></a>
    </td>
    <td>
        <span style="display:none">@Model.Timestamp</span>
        <div>@(CType(Model.CreatedAt, DateTime).ToString("dd.MM.yy hh:mm"))</div>
        <small style="color: @(Model.CurrentProposalStep.ProcessStep.Color);">@(Model.CurrentProposalStep.ProcessStep.ShortCaption)</small>
    </td>
    @If bolShowRep = True Then
        @<td>
            <a href="/@Model.Representation.Key" class="representation" style="color:@(Model.Representation.ColorText)">
                @Model.Representation.Label
             </a>
         </td>
    End If
    <td>
        <a href="@Model.FullUrl" style="color:#333;">
            @Model.Title
        </a>
    </td>
    <td>
        <span style="display:none">@Model.UpdatedTimestamp</span>
        @(Model.UpdatedAtFormat)
    </td>
    <td>@(Model.ProposalComments.Count)</td>
    <td>@(Model.Rating)</td>                
</tr>
