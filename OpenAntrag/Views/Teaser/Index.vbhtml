@Imports OpenAntrag
@ModelType Teaser

@Code
    ViewData("Title") = Model.Name
    
    'Dim rps As New Representations(Representations.StatusConjuction.Active)
    Dim lst As List(Of Representation) = GlobalData.Representations.Items _
        .Where(Function(x) (x.Status And Representations.StatusConjuction.Active) > 0) _
        .ToList()
    
End Code

@Section Styles
    @Styles.Render(String.Concat("~/css/teaser-", Model.Key))
    <link href="/@(Model.TeaserUrl)/style-teaser" rel="stylesheet" type="text/css" media="screen" />
End Section

@Section Scripts
    @Scripts.Render("~/bundle/teaser")
    @Scripts.Render("~/bundle/markdown")
    <script>
        $(document).ready(function () {
        });
    </script>
End Section

@Section Intro
    @Html.PartialOrNull("_TeaserIntro", Model)
End Section

@Section RepNav
    @Html.PartialOrNull("_NavTeaser", Model)
End Section

<div class="content content-teaser container-fluid">
    <div class="row-fluid">
        <div class="span8">
            <h2>OpenAntrag für @Model.Label</h2>
            <p>
                Du wohnst in <strong>@(Model.Label)</strong> und hast eine Idee für eine Verbesserung, 
                ein Anliegen, welches Du gerne als Antrag im Parlament eingebracht haben möchtest? 
            </p>
            <p style="font-weight: bold;">Mit einem Mandatsträger der Piratenpartei kein Problem!</p>
            <p>
                Diese Plattform bietet den Bürgern inzwischen in <strong>@(lst.Count)&nbsp;Parlamenten</strong> 
                die Möglichkeit Anträge einzustellen, weil wir dort Parlamentarier haben, die ihr Anliegen übernehmen. 
            </p>
            <p>    
                Nur noch nicht in <strong>@(Model.Label) ... 
                aber das kannst Du am @(Model.ElectionDateFormat) ändern!</strong>
            </p>
        </div>
        <div class="span3 offset1" style="padding-top: 50px;">
            @If String.IsNullOrEmpty(Model.Link) = False Then
                @<p style="font-size: 1.1em; margin-bottom: 10px;">
                    <i class="icon-globe" style="font-size: 18px;"></i>&nbsp;
                    <a href="@Model.Link">@Model.Link</a>
                </p>
            End If
            @If String.IsNullOrEmpty(Model.Mail) = False Then
                @<p style="font-size: 1.1em; margin-bottom: 10px;">
                    <i class="icon-email"></i>&nbsp;
                    <a href="mailto:@Model.Mail">@Model.Mail</a>
                </p>                
            End If
            @If String.IsNullOrEmpty(Model.Twitter) = False Then
                @<p style="font-size: 1.1em; margin-bottom: 10px;">
                    <i class="icon-twitter"></i>&nbsp;
                    <a href="https://twitter.com/@Model.Twitter">@Model.Twitter</a>
                </p>
            End If
        </div>
    </div>
</div>

<div class="content content-teaser content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span12">
            @Model.Info
        </div>
    </div>
</div>
