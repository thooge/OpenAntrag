@Imports OpenAntrag
@ModelType Proposal

@Code
    ViewData("Title") = Model.Representation.Name & " | " & Model.Title
End Code

@Section Styles
    @Styles.Render(String.Concat("~/css/proposal-", Model.Representation.Key))
    <link href="/@(Model.Representation.Key)/style-representation" rel="stylesheet" type="text/css" media="screen" />
End Section

@Section Scripts
    @Scripts.Render("~/bundle/representations")
    @Scripts.Render("~/bundle/markdown")
    <script type="text/javascript">
        $(document).ready(function () {
            $('.nextstep-item').equalHeights();
            initTagSelect();
            //--
            $('#createdat-wrapper').datetimepicker({
                language: 'de-DE'
            }).on('changeDate', function (e) {
                saveProposalDate($("#createdat"));
            });
            //--
            $('.proposalstep-date').datetimepicker({
                language: 'de-DE'
            }).on('changeDate', function (e) {
                saveProposalStepDate($(e.target).find("input"));
            });
            var markdown = new MarkdownDeep.Markdown();
            markdown.ExtraMode = true;
            markdown.SafeMode = false;
            prepareMDDEditor("mdd-editor-editproposal");            
        });
    </script>
End Section

@Section Intro
    @Html.PartialOrNull("_RepresentationIntro", Model.Representation)
End Section

@Section RepNav
    @Html.PartialOrNull("_NavRepresentation", Model.Representation)
End Section

@* --- Abuse-Message --- *@
@If Model.IsAbuse = True Then
    @<div class="content alert-error container-fluid">
        <div class="row-fluid">
            Dieser Antrag wurde wegen Missbrauchs stumm geschaltet
            <div class="abuse-message">@Html.Raw(Model.AbuseMessageHtml)</div>
        </div>
    </div>
End If

@* --- Success-Story-Message --- *@
@If Model.CurrentProposalStep.ProcessStep.SuccessStory = True AndAlso Model.SuccessStoryStatus = 0 AndAlso Tools.IsAdmin(Model.Representation.Key) = True Then
    @<div class="content alert-success container-fluid">
        <div class="row-fluid">
            Der Prozessschritt dieses Antrags erlaubt die Anlage einer 'Success-Story' (Erfolgsgeschichte),
            die unter <a href="/erfolge">Erfolge</a> aufgelistet wird.<br />
            <a onclick="$.scrollTo('#success-story-new', 500); return false;" href="#success-story"><strong>Jetzt Erfolgsgeschichte anlegen</strong></a>, wenn der Antrag denn erfolgreich war ;)
        </div>
    </div>
End If

@* --- Antrag --- *@
<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span10 box-head">
            <i class="icon-doc proposal-step-icon" style="color: @(Model.CurrentProposalStep.ProcessStep.Color)" rel="@Model.Id">
@*                @If Model.HasSuccessStory = True Then
                    @<i class="icon-light-up"></i>
                End If*@
            </i>
            <input id="proposal-id" type="hidden" value="@Model.Id" />
            
            <div class="proposal-head">
                @If Tools.IsAdmin(Model.Representation.Key) = False Then
                    @<small>
                        @Model.CreatedAtFormat
                        <i style="font-size: 1.3em; padding: 5px" class="icon-angle-double-right"></i>
                        <em>@Model.CurrentProposalStep.CreatedAtFormat</em>
                     </small>
                    @<br />
                    @<small>
                        <img src="/Images/Icons/@Model.CurrentProposalStep.ProcessStep.Icon" style="width: 16px;">
                        <em>@Proposals.GetProcessStepCaption(Model.CurrentProposalStep.ProcessStep.Caption, Model, Model.Representation)</em>
                     </small>
                Else
                    @<div id="createdat-wrapper" class="input-append date">
                        <span class="tt-std add-on" title="Datum auswählen"><i data-time-icon="icon-time" data-date-icon="icon-calendar"></i></span>
                        <input id="createdat" type="text" disabled="disabled"
                               data-format="dd.MM.yyyy hh:mm:ss" 
                               value="@Model.CreatedAt" />                        
                    </div>
                    @<a onclick="deleteProposal('@Model.Id'); return false;" href="javascript:go();"
                        class="tt-std cmd btn btn-small" title="Anträg löschen"><i class="icon-trash"></i></a>
                    @If Model.IsAbuse = False Then
                        @<a onclick="showProposalAbuse(); return false;" href="javascript:go();" style="color: #E80000;"
                            class="tt-std cmd btn btn-small" title="Antrag stumm schalten"><i class="icon-block"></i></a>                    
                    Else
                        @<a onclick="removeProposalAbuse('@Model.Id'); return false;" href="javascript:go();" style="color: #028C00;"
                            class="tt-std cmd btn btn-small" title="Antrag wieder freischalten"><i class="icon-block"></i></a>                        
                    End If
                End If
                <h2>@Model.Title</h2>
            </div>

            @If Tools.IsCreatedByAdmin(Model.Representation.Key, Model.CreatedBy) = True OrElse (Model.IsAbuse = True And Tools.IsAdmin(Model.Representation.Key) = True) Then
                @<div id="mdd-editor-editproposal" class="mdd-editor-container" style="margin-top: 20px; margin-left: 80px;">
                    @Html.TextAreaFor(Function(m) m.TextMarkdown, 5, 0, New With {.placeholder = "Dein Antragstext"})
                </div>   
                @<div style="margin-top: 10px; margin-left: 80px;">
                    <button onclick="saveProposalText(); return false;" class="btn btn-small btn-primary">Antragsstext speichern</button>
                </div>
            Else
                If Model.IsAbuse = False Then
                    @<div class="proposal-body">@Html.Raw(Model.TextHtml)</div>    
                    @Html.Partial("_ProposalSublinksPartial", Model)
                End If
            End If
            
        </div>
        <div id="proposal-tags-list" class="span2 proposal-tags" 
             style="@(IIf(Model.HasTags = true,"display:block;", "display:none;"))">
            @Html.Partial("_ProposalTagListPartial", Model)
        </div>
    </div>
</div>

@* --- Antrag stummschalten [EDIT] --- *@
@If Model.IsAbuse = False AndAlso Tools.IsAdmin(Model.Representation.Key) = True Then    
    @<div id="proposal-blockabuse" class="content content-edit container-fluid" style="display:none;">
        <div class="row-fluid">
            <div class="span12 box box-head">
                <i class="icon-block" style="font-size: 48px;"></i>
                <h2>[Antrag stumm schalten]</h2>
                <br>
                <p>Wenn dieser Antrag als Missbrauch gemeldet wurde und/oder Du selbst denkst, dass 
                    dieser Antrag <strong>Denunziationen</strong> oder <strong>persönliche Informationen</strong> enthält, 
                    die nicht veröffentlicht werden dürfen, dann gib in nachfolgendem Textfeld bitte 
                    eine entsprechende Begründung ein und klicke auf '<strong>Anzeige des Antrags blockieren</strong>'.</p>
                <p>Der Antragstext wird danach auf allen Seiten für normale Benutzer 
                    mit Deiner Begründung <strong>ersetzt</strong> und Du hast die Möglichkeit den Antragstext zu <strong>editieren</strong>.</p>
                    <div id="mdd-editor-blockabuse" class="mdd-editor-container mdd-editor-inverse" style="margin-top: 20px;">
                        @Html.TextAreaFor(Function(m) m.AbuseMessage, 5, 0, New With {.placeholder = "Begründung der Blockierung"})
                    </div>
                    <div style="margin-top: 10px;">
                        <button onclick="blockProposalAbuse('@Model.Id'); return false;" 
                            class="btn btn-primary">Anzeige des Antrags blockieren</button>
                    </div>
            </div>
        </div>
    </div>
End If

@Code
    Dim bolShaded As Boolean = True
End Code

@* --- Schrittliste --- *@
@For Each ps As ProposalStep In Model.ProposalStepList
    @<div class="content @(IIf(bolShaded=true, "content-shaded", "")) container-fluid">
        <div class="row-fluid">
            <div class="span12 proposalstep-item">
                <span class="bar" style="background-color: @ps.ProcessStep.Color;">&nbsp;</span>
                <div class="proposalstep-body">
                    <img src="/Images/Icons/@ps.ProcessStep.Icon">
                    @If Tools.IsAdmin(Model.Representation.Key) = False Then
                            @<small>@ps.CreatedAtFormat</small>
                        Else
                            @<div class="proposalstep-date input-append date">
                                <span class="tt-std add-on" title="Datum auswählen"><i data-time-icon="icon-time" data-date-icon="icon-calendar"></i></span>
                                <input type="text" disabled="disabled"
                                       data-format = "dd.MM.yyyy hh:mm:ss"
                                       data-step-id ="@ps.Id"
                                       value="@ps.CreatedAt" />
                            </div>

                            @If ps.Id > 1 Then
                                @<a onclick="editProposalStep(this, @ps.Id); return false;" href="javascript:go();" 
                                    class="tt-std cmd btn btn-small" title="Antragsschritt bearbeiten"><i class="icon-edit"></i></a>
                            End If

                            @If ps Is Model.ProposalStepList.Last And ps.Id > 1 Then
                                @<a onclick="deleteProposalStep('@Model.Id', @ps.Id); return false;" href="javascript:go();"
                                    class="tt-std cmd btn btn-small" title="Antragsschritt löschen"><i class="icon-trash"></i></a>
                            End If

                        End If                    

                    <em>@Proposals.GetProcessStepCaption(ps.ProcessStep.Caption, Model, Model.Representation)</em>                    
                    <div class="info">@Html.Raw(ps.InfoHtml)</div>
                    <div id="mdd-editor-editstep-@(ps.Id)" class="info-edit mdd-editor-container" style="display:none;">
                        <textarea class="info-edit" style="width: 100%;" rows="5"
                                  placeholder="Informationen über diesen Schritt">@ps.InfoMarkdown</textarea>
                    </div>
                </div>
            </div>
        </div>
    </div>
    bolShaded = Not bolShaded
Next

@* --- Success-Story --- *@
@If Model.CurrentProposalStep.ProcessStep.SuccessStory = True AndAlso Model.SuccessStoryStatus = 0 AndAlso Tools.IsAdmin(Model.Representation.Key) = True Then
@<a name="success-story" id="success-story"></a>
@<div id="success-story-new" class="content content-info container-fluid">
    <div class="row-fluid">
        <div class="span4 box-head">
            <i class="icon-light-up"></i>
            <h2>Die<br />
                Erfolgs -<br/>
                geschichte</h2>
            <br />
        </div>
        <div class="span8">
            <h4>Tue Gutes und rede darüber.</h4>
            <p>
                Aufgrund der unterschiedlichen Prozesse, ist nicht immer klar, wann ein Antrag 
                erfolgreich war oder nicht. Es sollte auch keine Software darüber entscheiden. 
                Deswegen gibt es die <strong>Erfolgsgeschichten</strong>, in die durchgebrachte 
                Anträge manuell mit einem kleinen Infotext eingestellt werden können.
            </p>
            <button class="btn btn-small btn-primary btn-represenation" 
                    onclick="setSuccessStoryStatus(true); return false;">JA, Erfolgsgeschichte anlegen...</button>
            <button class="btn btn-small btn-primary btn-inverse"
                    onclick="setSuccessStoryStatus(false); return false;">Nein, dieser Antrag war kein Erfolg :(</button>
        </div>
    </div>
</div> 
@<div id="success-story-add" class="content content-edit container-fluid" style="display:none;">
    <div class="row-fluid">
        @Using Html.BeginForm("CreateSuccessStory", "SuccessStory", FormMethod.Post, New With {.id = "newsuccessstory-form", .name = "newsuccessstory-form"})
            @Html.Partial("_NewSuccessStoryPartial", New SuccessStory(Model))
        End Using
    </div>
</div>
End If

@If Model.CurrentProposalStep.ProcessStep.NextSteps IsNot Nothing AndAlso Model.CurrentProposalStep.ProcessStep.NextSteps.Count > 0 Then

    @* --- Nächste Schritte --- *@
    @<div class="content @(IIf(bolShaded=true, "content-shaded", "")) container-fluid">
        <div class="row-fluid">
            <div class="span12 box box-head">
                <i class="icon-direction"></i>
                <h2>Die<br>
                    nächsten Schritte</h2>
                <br>
                <p>Einer der folgende Schritte ist nun als nächster Schritt für den Antrag möglich:</p>
                <div class="row-fluid" style="margin-top:20px;">
@Code
    Dim intNextColWidth As Integer = Math.Abs(12 / Model.CurrentProposalStep.ProcessStep.NextSteps.Count)
    If intNextColWidth > 3 then intNextColWidth = 3
    For Each ps As ProcessStep In Model.CurrentProposalStep.ProcessStep.NextSteps
        @If Tools.IsAdmin(Model.Representation.Key) = True Then
            @<a class="nextstep-item span@(intNextColWidth) box arrow-down" 
                style="background-color:@(ps.Color)" 
                href="javascript:go();"
                data-repid="@Model.Representation.Key"
                data-stepid="@ps.ID"
                data-color="@ps.Color"              
                onclick="setNextStep(this); return false;">
                @ps.ShortCaption
            </a>
        Else
            @<div class="nextstep-item span@(intNextColWidth) box" style="background-color:@(ps.Color)">
                @ps.ShortCaption
            </div>                
        End If
    Next
End Code        
                </div>
            </div>
        </div>
    </div>
    
    @If Tools.IsAdmin(Model.Representation.Key) = True Then
        
        @* --- Nächster Schritt [EDIT] --- *@
        @<div id="proposaledit-container" class="content content-edit container-fluid">
            <div class="row-fluid">
                <div class="span12 box box-head">
                    <i class="icon-edit" style="font-size: 48px;"></i>
                    <h2>[Bearbeitung]</h2>
                    <br>

                    <h3>Nächster Schritt</h3>
                    <div class="proposalstep-item">
                        <p id="nextstep-hint">Bitte wähle oben den nächsten Schritt aus...</p>
                        <span id="nextstep-bar" style="display:none; border: 2px solid #fff;" class="bar">&nbsp;</span>                        
                        <div id="nextstep-body" class="proposalstep-body" style="display:none;">
                        </div>
                    </div>

                </div>
            </div>
        </div>

        @* --- Themen [EDIT] --- *@
        @<div id="proposal-tag-edit" class="content content-edit container-fluid">
            <div class="row-fluid">
                <div class="span12 box box-head">
                    <h3 style="margin-top: 0;">Zugeordnete Themen</h3>
                    <input type="text" id="proposal-tags-edit" value="@Model.TagsList" style="width: 100%">
                </div>
            </div>
        </div>
    End If    
    
    bolShaded = Not bolShaded    
End If

@* --- Kommentare --- *@
<a name="comments"></a>
<div id="comment-new" class="content @(IIf(bolShaded=true, "content-shaded", "")) content-representation container-fluid">
    <div class="row-fluid">
        <div class="span12 box-head">
            <i class="icon-comment"></i>
            <h2>Die<br />
                Kommentare</h2>
            <br />
            
            @If Model.IsCommentingClosed = False Then
                @<button class="btn btn-small btn-primary" onclick="showProposalComment(this);" style="margin: 10px 0;">Neuer Kommentar...</button>
                
                @If Tools.IsAdmin(Model.Representation.Key) = True Then
                    @<a onclick="closeCommenting('@Model.Id'); return false;" href="javascript:go();" style="color: #E80000;"
                        class="tt-std cmd btn btn-small" title="Kommentarfunktion schließen"><i class="icon-lock"></i></a>                    
                End if

                @Using Html.BeginForm("CreateProposalComment", "Proposal", FormMethod.Post, New With {.id = "newcomment-form", .name = "newcomment-form", .style = "display:none;"})
                    @Html.Partial("_NewProposalCommentPartial", New ProposalComment(Model))
                End Using
            Else
                @<p style="margin: 5px 10px 15px 0; display: inline-block;">Kommentarfunktion wurde geschlossen am @(CType(Model.CommentingClosedDate, DateTime).ToString("dd. MMMM yyyy HH:mm"))</p>
                @If Tools.IsAdmin(Model.Representation.Key) = True Then                    
                    @<a onclick="reopenCommenting('@Model.Id'); return false;" href="javascript:go();" style="color: #028C00;"
                        class="tt-std cmd btn btn-small" title="Kommentarfunktion wieder öffnen"><i class="icon-lock-open"></i></a>                                            
                End If
            End If

            <div id="proposal-comments">
                @For Each pc As ProposalComment In Model.ProposalComments
                    pc.ID_Proposal = Model.Id
                    Html.RenderPartial("_ProposalCommentPartial", pc)
                Next
            </div>
        </div>
    </div>
</div>

@Section PreFooter
    @Html.PartialOrNull("_BannerTeaserPartial", Model.Representation)
End Section