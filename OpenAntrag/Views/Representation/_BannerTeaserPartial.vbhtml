@Imports OpenAntrag
@ModelType Representation

@If (Model.Status And Representations.StatusConjuction.Ended) = 0 Then
@<div id="banner-container" class="content content-special container-fluid">
    <div class="row-fluid">
        <div class="span12 box">
            <i class="icon-coverflow-empty"></i>
            <span>Du möchtest auf Deiner Website Werbung für Bürgeranträge in Deinem Parlament machen?<br />
                Dann schau Dir die individuellen oder generischen 
                <a href="/@(Model.Key)/banner" style="font-weight:bold;">OpenAntrag-Banner</a> an.</span>
        </div>
    </div>
</div>
End If