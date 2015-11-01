@Imports OpenAntrag
@ModelType Representation

@Code
    ViewData("Title") = Model.Name & " | Dein Antrag"
End Code

@Section Styles
    @Styles.Render(String.Concat("~/css/representations-", Model.Key))
    <link href="/@(Model.Key)/style-representation" rel="stylesheet" type="text/css" media="screen" />
End Section

@Section Scripts
    @Scripts.Render("~/bundle/representations")
    @Scripts.Render("~/bundle/markdown")
    <script>
        $(document).ready(function () {
            var markdown = new MarkdownDeep.Markdown();
            markdown.ExtraMode = true;
            markdown.SafeMode = false;
            prepareMDDEditor("mdd-editor-newproposal");
        });
        $("#Title").focus();
    </script>
End Section

@Section Intro
    @Html.PartialOrNull("_RepresentationIntro", Model)    
End Section

@Section RepNav
    @Html.PartialOrNull("_NavRepresentation", Model)    
End Section

<div id="newproposal-container" class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span3 box-head">
            <i class="icon-plus-circled"></i>
            <h2>Dein&nbsp;<br />Antrag</h2>
            <br /><br />
            <p>Du hast etwas was Du gerne in diesem Parlament behandelt sehen möchtest? 
                Hier hast Du @IIf(Model.IsActive = False Or Model.IsViewOnly = True, Html.Raw("<strong>bald</strong>"), "") die Gelegenheit dazu...
            </p>
        </div>
        <div id="new-proposal" class="span8 offset1">
            @Using Html.BeginForm("CreateProposal", "Proposal", FormMethod.Post, New With {.id = "newproposal-form", .name = "newproposal-form"})
                @Html.Partial("_NewProposalPartial", New Proposal(Model))
            End Using
        </div>
    </div>
</div>

<div id="processinfo-container" class="content content-representation content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span6">
            <div class="row-fluid">
                <div class="span5">
                    <p style="margin-top: 15px;">
                        Trag ins erste Feld einen 
                        aussagekräftigen Titel ein und beschreib im zweiten Feld deine Idee. 
                        Dabei musst du keinen perfekt ausformulierten Antragstext haben, wir 
                        werden ihn an die Regeln des Parlaments anpassen, wenn uns die Idee 
                        überzeugt.</p>
                    <p>Abschließend musst du nur noch auf <strong>Antrag senden</strong> klicken.</p>
                </div>
                <div class="span6 offset1">
                    <h4>Die Regeln</h4>
                    <ol>
                        <li>Jeder Antrag wird geprüft. Wir behalten uns jedoch vor, nur solche Anträge weiterzuverfolgen, die unserem politischen Selbstverständnis entsprechen.</li>
                        <li>Anträge werden grundsätzlich anonym behandelt, um die persönliche/ideologische Ebene außen vor zu lassen.</li>
                        <li>Die Bearbeitung der Anträge erfolgt transparent auf dieser Seite. Eine Benachrichtigung des Antragsstellers erfolgt nicht.</li>
                        <li>Alle Anträge sind öffentlich und können auch öffentlich kommentiert werden.</li>
                    </ol>
                </div>
  	        </div>
            
            @If Tools.IsAdmin() = True Then
  	        @<div class="row-fluid">
                <div style="background-color: #fff; color: #333; border: 6px solid #b30000; margin: 20px 0px; padding: 10px 20px;" class="span12">
                    <h4>Hinweis für Fraktionen/Abgeordnete</h4>
      	            <p>
                          <strong style="color: #b30000;">Der Antragsprozess ist JEDERZEIT anpassbar!</strong> Dies gilt sowohl für die 
                          Schritte selbst, als auch für die damit verknüpften Folgeschritte.
      	            </p>
                    <p>
                        Soll z.B. ein Schritt entfernt werden, genügt es, ihn als Folgeschritt 
                        aus allen verknüpften Schritten herauszunehmen und er wird in der Übersicht 
                        nicht mehr ausgegeben.
                    </p>
                    <p>
                        Aktuell können Änderungen noch nicht online vorgenommen werden, 
                        aber das ist geplant. Bis dahin schicke Deine Änderungen oder am Besten 
                        den gesamten neuen Prozess via Mail an 
                        <a href="webmaster@openantrag.de"><strong style="color: #b30000;">webmaster@openantrag.de</strong></a>.</p>
                </div>
            </div>                
            End If

        </div>
  
        <div class="span5 offset1">

            <h4>Der Antragsprozess in @Model.Label</h4>
            <ul class="schema-info">
                @For Each ps As ProcessStep In Model.ProcessSteps
                    If ps.IsInactive = False Then
                        @<li>
                            <span style="background-color: @ps.Color;">
                                <span data-id="@ps.ID">@ps.ShortCaption</span>
                            </span>
                            @If ps.NextSteps IsNot Nothing AndAlso ps.NextSteps.Count > 0 Then
                                @<ul>
                                    @For Each ns As ProcessStep In ps.NextSteps
                                        @<li style="background-color: @ns.Color;">
                                            <i style="color: @ps.Color !important;" class="icon-right-dir"></i>
                                            <span data-id="@ns.ID">@ns.ShortCaption</span>
                                        </li>
                                    Next
                                </ul>
                            End If
                        </li>

                    End If
                Next
            </ul>            
        </div>
    </div>
</div>

@Section PreFooter
    @Html.PartialOrNull("_BannerTeaserPartial", Model)
End Section