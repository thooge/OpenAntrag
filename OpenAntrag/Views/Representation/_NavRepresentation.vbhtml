@Imports OpenAntrag
@ModelType Representation

<div class="nav" id="repnav">
    <div class="nav-left">

        <a class="nav-item nav-item-ellipsis" 
            href="/@(Model.Key)" title="Startseite @(Model.GroupTypeCaption) / @(Model.Name)">
            <i class="nav-icon icon-home"></i>
            <span class="nav-text">@Model.Label</span>
        </a>

    @If (Model.Status And Representations.StatusConjuction.Ended) = 0 Then        
        @<a class="nav-item nav-icon-mere" 
            href="/@(Model.Key)/neu" title="Stelle Deinen Antrag">
            <i class="nav-icon icon-plus-circled"></i>
            <span class="nav-text">Dein Antrag</span>
        </a>            
    End If

        <a class="nav-item nav-icon-mere" 
            href="/@(Model.Key)/journal" title="Anträge für @(Model.Name)">
            <i class="nav-icon icon-tasks"></i>
            <span class="nav-text">Anträge</span>
        </a>
                        
    </div>

    <div class="nav-right">

        @If Tools.IsAdmin(Model.Key) Then
            @<a class="nav-item nav-icon-mere" 
                href="/@(Model.Key)/einstellungen" title="Einstellungen @(Model.Name)">
                <i class="nav-icon icon-cog"></i>
                <span class="nav-text">Einstellungen</span>
            </a>            
        End If

        <a class="nav-item nav-icon-only" 
            href="/@(Model.Key)/feed" title="RSS-Feed - Anträge für @(Model.Name)">
            <i class="nav-icon icon-rss"></i>
            <span class="nav-text">RSS</span>
        </a>

    </div>

</div>

