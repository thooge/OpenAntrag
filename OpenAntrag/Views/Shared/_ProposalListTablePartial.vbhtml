@Imports OpenAntrag
@ModelType List(Of Proposal)

@Code
    Dim bolShowRep As Boolean = ViewData("ShowRepresentation")
End Code

<table class="table table-hover table-sorted" id="proposallist-table" >
    <thead>
        @Html.Partial("_ProposalRowHeadPartial", New ViewDataDictionary() From {{"ShowRepresentation", bolShowRep}})
    </thead>
    <tbody>
        @For Each prop In Model                    
                Html.RenderPartialWithData("_ProposalRowPartial", prop, New With {.ShowRepresentation = bolShowRep})
            Next
    </tbody>
</table>
