@Imports OpenAntrag
@ModelType Proposal

<ul class="sublinks">

    @If Model.HasSuccessStory = True Then
        @<li><a href="/@(Model.Key_Representation)/@(Model.TitleUrl)/erfolg"><i class="tt-std icon-light-up"  
                title="Dieser Antrag ist bzw. hat eine Erfolgsgeschichte"
                style="font-size:2rem;font-weight:bold"></i></a></li>
    End If    

    @If Model.IsAbuse = False Then
        @<li><a id="abusenotice-link-@(Model.Id)" href="javascript: go();" onClick="showAbuseNotice('@(Model.Id)');">Missbrauch melden</a></li>
    End If

    @If Model.ProposalComments.Count > 0 Then
        @<li>
            <a href="@(Model.FullUrl)#comments" >@(Model.ProposalCommentCountCaption)</a>
        </li>
    End If

    @If String.IsNullOrEmpty(Model.ShortUrl) = False Then
        @<li>
            <em>Kurzlink: </em>
            <a href="@(Model.ShortUrl)">@(Model.ShortUrl.Replace("http://", ""))</a>
         </li>
    End If

    <li>
        <em>Unterstützung (@Model.RatingCount): </em>
        <div id="rating-@(Model.Id)" class="rating-group" data-rating="@(Model.Rating)">
            <a href="javascript:go();" data-rate="1" 
               onclick="rateProposal('@(Model.Id)', 1)" class="tt-std" 
               title="Guter Antrag (+1)"><i class="icon-star-empty"></i></a>
            <a href="javascript:go();" data-rate="2" 
               onclick="rateProposal('@(Model.Id)', 2)" class="tt-std" 
               title="Sehr guter Antrag (+2)"><i class="icon-star-empty"></i></a>
            <a href="javascript:go();" data-rate="3" 
               onclick="rateProposal('@(Model.Id)', 3)" class="tt-std" 
               title="Superantrag (+3)"><i class="icon-star-empty"></i></a>
            <a href="javascript:go();" data-rate="4" 
               onclick="rateProposal('@(Model.Id)', 4)" class="tt-std" 
               title="Spitzenantrag (+4)"><i class="icon-star-empty"></i></a>
            <a href="javascript:go();" data-rate="5" 
               onclick="rateProposal('@(Model.Id)', 5)" class="tt-std" 
               title="Warum ist mir das nicht eingefallen!? (+5)"><i class="icon-star-empty"></i></a>
        </div>
    </li>

</ul>

<div id="abusenotice-wrapper-@(Model.Id)" style="display:none;">
    <h5>Missbrauch melden</h5>
    <textarea id="abusenotice-@(Model.Id)" placeholder="Begründung"
              style="width: 100%" rows="3"></textarea>
    <button class="btn btn-small btn-primary" 
            onclick="sendAbuseNotice('@(Model.Id)'); return false;">Senden an Fraktion und Administration</button>
</div>
