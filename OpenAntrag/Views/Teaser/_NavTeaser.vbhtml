@Imports OpenAntrag
@ModelType Teaser

<div class="nav" id="repnav">
    <div class="nav-left">

        <a class="nav-item nav-item-ellipsis" 
           href="/@(Model.TeaserUrl)" title="OpenAntrag für @(Model.Label)">
            <i class="nav-icon icon-check"></i>
            <span class="nav-text">&nbsp;@Model.Label&nbsp;&nbsp;-&nbsp;&nbsp;am @(Model.ElectionDateFormat) hast Du die Wahl</span>
        </a>

    </div>
    <div class="nav-right">
    </div>
</div>

