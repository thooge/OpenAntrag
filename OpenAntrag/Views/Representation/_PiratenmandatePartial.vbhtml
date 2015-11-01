@ModelType OpenAntrag.piratenmandate.Item

<div class="span4 piratenmandate">
    <i class="icon-network"></i>
    <a href="http://kommunalpiraten.de"><img src="/images/logo-kommunalpiraten.png"></a>
    <div>
  	    <strong>@Model.GebietName</strong>
        @If String.IsNullOrEmpty(Model.ParlamentSeats) = False Then
            @<p>@Model.MandateCount von @Model.ParlamentSeats Sitze</p>
        End If
        @If String.IsNullOrEmpty(Model.ParlamentStory) = False Then
            @<p>
                @Model.ParlamentStory
                @If String.IsNullOrEmpty(Model.ParlamentStorySource) = False Then
                    @<br />
                    @<a href="@(Model.ParlamentStorySource)"><small>Quelle...</small></a>
                End If                
            </p>
        End If
        <p>@Html.Raw(Model.FraktionText)</p>
        <div class="links">
            @If String.IsNullOrEmpty(Model.ParlamentRIS) = False Then
                @<a class="btn btn-primary btn-tiny" href="@(Model.ParlamentRIS)">Ratsinformationssystem</a>
            End If
            @If String.IsNullOrEmpty(Model.GebietLocalpirates) = False Then
                @<a class="btn btn-primary btn-tiny" href="@(Model.GebietLocalpirates)">Piraten vor Ort</a>                        
            End If
        </div>
        @If String.IsNullOrEmpty(Model.MapUrl) = False Then
            @<iframe width="100%" height="300" frameborder="0" src="@(Model.MapUrl)"></iframe>
        End If
    </div>
</div>
