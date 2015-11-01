@Imports OpenAntrag
@ModelType Representation

@Code
    ViewData("Title") = Model.Name & " | Einstellungen"    
End Code

@Section Styles
    @Styles.Render(String.Concat("~/css/representations-", Model.Key))
    <link href="/@(Model.Key)/style-representation" rel="stylesheet" type="text/css" media="screen" />
End Section

@Section Scripts
    <script type="text/javascript">
        $(document).ready(function () {
            revampForm();

            var ePick = $("#representation-color-button"),
                picker = new Picker(ePick[0]);
            ePick.click(function() {
                picker.show();
            });
            picker.on_done = function (colour) {
                ePick.css("background", colour.rgba().toString());
                $("#representation-color").val(colour.hex().toString());
            };
            $("#representation-color").blur(function () {
                var sColor = $(this).val().toUpperCase();
                if (sColor.substring(0, 1) != "#") { sColor = "#" + sColor; }
                ePick.css("background", sColor);
                $(this).val(sColor);
            });
        });
        function revampForm() {
            $(".table-form tr").each(function () {
                var $tr = $(this);
                $tr.click(function () {
                    $tr.find("input, textarea, select").focus(); //.putCursorAtEnd();
                });
                $tr.find("input, textarea, select")
                    .focus(function () {
                        $tr.addClass("focus");
                    })
                    .blur(function () {
                        $tr.removeClass("focus");
                    });
            });
        }
        function newCommitee() {
            var $t = $("#committee-template").clone().prop("id", "");
            $t.removeClass("committee-tmpl").addClass("committee-block");
            $t.insertBefore($("#committee-new")).show();
            revampForm();
        }
        function removeNewCommittee(e) {
            $(e).parents("table").remove();
        }
        function newRepresentative() {
            var $d = $("#representative-template").clone().prop("id", "").addClass("representative-block"),
                bShaded = $(".representative-block").last().hasClass("content-shaded");
            if (!bShaded) { $d.addClass("content-shaded"); }
            $d.removeClass("representative-tmpl").addClass("representative-block");
            $d.insertBefore($("#representative-new").parents("div.content")).show();
            revampForm();
        }
        function removeNewRepresentative(e) {
            $(e).parents("div.content").remove();
        }
        function getXML() {
            var $root = $('<XMLDocument />'),
                $rep = $('<item />'),
                $representatives = $('<representatives />'),
                $committees = $('<committees />'),
                $infotext = $('<infotext />');

            $rep.attr("id", $("#representation-id").val());
            $rep.attr("key", $("#representation-key").val());
            $rep.attr("status", $("#representation-status").val());
            $rep.attr("api-key", $("#representation-api-key").val());
            $rep.attr("label", $("#representation-label").val());
            $rep.attr("color", $("#representation-color").val());
            $rep.attr("name", $("#representation-name").val());
            $rep.attr("name2", $("#representation-name2").val());
            $rep.attr("level", $("#representation-level").val());
            $rep.attr("federal", $("#representation-federal").val());
            $rep.attr("group-type", $("#representation-group-type").val());
            $rep.attr("group-name", $("#representation-group-name").val());
            $rep.attr("link", encodeURIComponent($("#representation-link").val()));
            $rep.attr("phone", $("#representation-phone").val());
            $rep.attr("twitter", $("#representation-twitter").val());
            $rep.attr("mail", $("#representation-mail").val());
            $rep.attr("info-mail", $("#representation-info-mail").val());

            $(".committee-block").each(function () {
                var newCommittee = $('<item />');
                newCommittee.attr("id", $(this).find(".committee-id").val());
                newCommittee.attr("key", $(this).find(".committee-key").val());
                newCommittee.attr("caption", $(this).find(".committee-caption").val());
                newCommittee.attr("name", $(this).find(".committee-name").val());
                newCommittee.attr("url", encodeURIComponent($(this).find(".committee-url").val()));
                $committees.append(newCommittee);
            });
            $rep.append($representatives);

            $(".representative-block").each(function () {
                var newRepresentative = $('<item />');
                newRepresentative.attr("id", $(this).find(".representative-id").val());
                newRepresentative.attr("key", $(this).find(".representative-key").val());
                newRepresentative.attr("name", $(this).find(".representative-name").val());
                newRepresentative.attr("mail", $(this).find(".representative-mail").val());
                newRepresentative.attr("phone", $(this).find(".representative-phone").val());
                newRepresentative.attr("twitter", $(this).find(".representative-twitter").val());
                newRepresentative.attr("party", $(this).find(".representative-party").val());
                $representatives.append(newRepresentative);
            });            
            $rep.append($committees);

            $infotext.text("<![CDATA[" + $(".infotext").val() + "]]>");
            $rep.append($infotext);

            $root.append($rep);

            console.log($root.html());
        }
    </script>
End Section

@Section Intro
    @Html.PartialOrNull("_RepresentationIntro", Model)    
End Section

@Section RepNav
    @Html.PartialOrNull("_NavRepresentation", Model)    
End Section

<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span4 box-head">
            <i class="icon-cog"></i>
            <h2>Die<br />
                Einstellungen</h2>
            <br />
        </div>
        <div class="span7 offset1">
            <p>
                Über die Einstellungen in diesem Bereich kannst Du das Aussehen und 
                Verhalten Deiner OpenAntrag-Seite steuern. @*Änderungen treten, im 
                Gegensatz zu den <strong>Seitendaten</strong> weiter unten, sofort in Kraft.*@
            </p>
        </div>
    </div>
</div>
@Code
    Dim rst As RepresentationSetting = RepresentationSettings.GetByKey(Model.Key)
End Code
<div class="content content-representation content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <table class="table table-form">
                <colgroup><col style="width:450px;" /></colgroup>
                <tbody>
                    <tr>
                        <th>API-Key</th>
                        <td><input type="text" class="readonly" value="@Model.ApiKey" /></td>
                    </tr>
                    <tr>
                        <th>Kontaktmöglichkeit bei neuem Antrag anzeigen?</th>
                        <td>
                            <div class="checkrack">
                                @If rst.HasContactPossibility = True Then
                                    @<input type="checkbox" id="HasContactPossibility" checked="checked">
                                Else
                                    @<input type="checkbox" name="" id="HasContactPossibility">
                                End If
		  	                    <label for="HasContactPossibility"></label>
	  	                    </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <button class="btn btn-primary" onclick="saveRepresentationSetting('@(Model.Key)');">Speichern</button>            
        </div>
    </div>
</div>

@Code Return End Code


<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span4 box-head">
            <i class="icon-edit-1"></i>
            <h2>Die<br />
                Seitendaten</h2>
            <br />
        </div>
        <div class="span7 offset1">
            <p>
                Die Informationen in diesem Abschnitt definieren Deine OpenAntrag-Seite.
            </p>
            <p>
                Du kannst sie nach Deinen Wünschen anpassen, wobei die <strong>Änderungen 
                nicht sofort wirksam</strong> werden. Vielmehr kannst Du am Ende des Bereichs 
                (Punkt 5) die Änderungen als Datenpaket an die Administrations senden.
            </p>
            <p>
                Bestehende <strong>Abgeordnete und Ausschüsse</strong> lassen sich 
                <strong>lediglich deaktivieren</strong>, 
                damit sichergestellt ist, 
                dass Verknüpfungen zu bestehenden Anträgen nicht verloren gehen.
            </p>
        </div>
    </div>
</div>

<div class="content content-edit content-representation container-fluid">
    <div class="row-fluid row-small">
        <div class="span12 box-head">
            <i class="icon-edit-1"></i>
            <h3>1. Parlament</h3>
        </div>
    </div>
</div>
@Code

End Code
<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <table class="table table-striped table-form">
                <colgroup><col style="width:100px;" /></colgroup>
                <tbody>
                    <tr><th>ID | Key</th>
                        <td style="padding-left: 10px;">
                            <em>@Model.ID</em>&nbsp;|&nbsp;<em>@Model.Key</em>
                            <input type="hidden" id="representation-id" value="@Model.ID" />
                            <input type="hidden" id="representation-key" value="@Model.Key" />
                        </td>
                    </tr>
                    <tr><th>Status</th>
                        <td>
                            <select id="representation-status">
                                <option value="1" @IIf(Model.Status = Representations.StatusConjuction.Active, "selected=""selected""", "")>Aktiv</option>
                                <option value="0" @IIf(Model.Status = Representations.StatusConjuction.Inactive, "selected=""selected""", "")>Inaktiv (nicht in der Übersicht enthalten)</option>                                
                                <option value="2" @IIf(Model.Status = Representations.StatusConjuction.ViewOnly, "selected=""selected""", "")>Nur lesen (keine neuen Anträge)</option>
                                <option value="4" @IIf(Model.Status = Representations.StatusConjuction.Ended, "selected=""selected""", "")>Beendet (Mandat besteht nicht mehr)</option>
                            </select>
                        </td>
                    </tr>
                    <tr><th>Label</th>
                        <td><input type="text" id="representation-label" value="@Model.Label" /></td>
                    </tr>
                    <tr><th>Name</th>
                        <td><input type="text" id="representation-name" value="@Model.Name" /></td>
                    </tr>
                    <tr><th>Name 2</th>
                        <td><input type="text" id="representation-name2" value="@Model.Name2" /></td>
                    </tr>
                    <tr><th>Bundesland</th>
                        <td>
                            <select id="representation-federal">
                                @For Each fs As FederalState In GlobalData.FederalStates.Items
                                    @<option value="@fs.Key" @IIf(fs.Key = Model.FederalKey, "selected=""selected""", "")>@fs.Name</option>
                                Next
                            </select>
                        </td>
                    </tr>
                    <tr><th>Ebene</th>
                        <td>
                            <select id="representation-level">
                                @For Each gl As GovernmentalLevel In GlobalData.GovernmentalLevels.Items
                                    @<option value="@gl.ID" @IIf(gl.ID = Model.Level, "selected=""selected""", "")>@gl.Name</option>
                                Next
                            </select>
                        </td>
                    </tr>
                    <tr><th>Farbe</th>
                        <td>
                            <input id="representation-color" type="text" 
                                class="color-picker representation-color" value="@(Model.Color)" />
                            <div id="representation-color-button" class="btn" 
                                 style="background-color: @(Model.Color); color: #fff;">
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>

<div class="content content-shaded content-representation container-fluid">    
    <div class="row-fluid">
        <div class="span12">
            <h3>Ausschüsse</h3>
            @For Each cm In Model.Committees
                @<table class="table table-striped-reverse table-form committee-block" style="margin-bottom: 20px;" 
                        data-committee-key="@cm.Key">
                    <colgroup><col style="width:100px;" /></colgroup>
                    <tbody>
                        <tr><th>ID | Key</th>
                            <td style="padding-left: 10px;">
                                <em>@cm.ID</em>&nbsp;|&nbsp;<em>@cm.Key</em>
                                <input type="hidden" class="committee-id" value="@cm.ID" />
                                <input type="hidden" class="committee-key" value="@cm.Key" />
                            </td>
                        </tr>
                        <tr><th>Name</th>
                            <td><input type="text" class="committee-name" value="@cm.Name" /></td>
                        </tr>
                        <tr><th>Kurzname</th>
                            <td><input type="text" class="committee-caption" value="@cm.Caption" /></td>
                        </tr>
                        <tr><th>Link</th>
                            <td><input type="text" class="committee-link" value="@cm.Url" /></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="2"><a class="btn btn-text btn-tiny" href="">Deaktivieren</a></td>
                        </tr>
                    </tfoot>
                </table>
            Next
            <button id="committee-new" class="btn btn-small btn-primary" onclick="newCommitee(); return false;">Neuer Ausschuss...</button>
            <table id="committee-template" class="table table-striped-reverse table-form committee-tmpl" style="margin-bottom: 20px; display:none">
                <colgroup><col style="width:100px;" /></colgroup>
                <tbody>
                <tr><th>ID | Key</th><td style="padding-left: 10px;"><em>-</em>&nbsp;|&nbsp;<em>NEU</em>
                    <input type="hidden" class="committee-id" value="-" />
                    <input type="hidden" class="committee-key" value="NEU" /></td></tr>
                <tr><th>Name</th><td><input type="text" class="committee-name" value=""/></td></tr>
                <tr><th>Kurzname</th><td><input type="text" class="committee-caption" value=""/></td></tr>
                <tr><th>Link</th><td><input type="text" class="committee-link" value=""/></td></tr>
                </tbody>
                <tfoot><tr><td colspan="2"><a class="btn btn-text btn-tiny" onclick="removeNewCommittee(this); return false;">Wieder entfernen</a></td></tr></tfoot>
            </table>
        </div>
    </div>
</div>

<div class="content content-edit content-representation container-fluid">
    <div class="row-fluid row-small">
        <div class="span12 box-head">
            <i class="icon-edit-1"></i>
            <h3>2. Fraktion / Einzelabgeordneter</h3>
        </div>
    </div>
</div>
<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <table class="table table-striped table-form">
                <colgroup><col style="width:100px;" /></colgroup>
                <tbody>
                    <tr><th>Gruppenname</th>
                        <td><input type="text" id="representation-group-name" value="@Model.GroupName" /></td>
                    </tr>
                    <tr><th>Gruppentyp</th>
                        <td>
                            <select id="representation-group-type">
                                @For Each gt As GroupType In GlobalData.GroupTypes.Items
                                    @<option value="@gt.ID" @IIf(gt.ID = Model.GroupType, "selected=""selected""", "")>@gt.Name</option>                                    
                                Next
                            </select>
                        </td>
                    </tr>
                    <tr><th>Mail</th>
                        <td><input type="text" id="representation-mail" value="@Model.Mail" /></td>
                    </tr>
                    <tr><th>Info-Mail</th>
                        <td><input type="text" id="representation-info-mail" value="@Model.InfoMail" /></td>
                    </tr>
                    <tr><th>Website</th>
                        <td><input type="text" id="representation-link" value="@Model.Link" /></td>
                    </tr>
                    <tr><th>Telefon</th>
                        <td><input type="text" id="representation-phone" value="@Model.Phone" /></td>
                    </tr>
                    <tr><th>Twitter</th>
                        <td><input type="text" id="representation-twitter" value="@Model.Twitter" /></td>
                    </tr>
                    <tr><th style="vertical-align: top; padding-top: 10px">Infotext Fraktion (HTML)</th>
                        <td>
                            <textarea class="infotext" rows="6">@Model.FraktionInfoHtml</textarea>
                        </td>
                    </tr>
                    <tr><th style="vertical-align: top; padding-top: 10px">Infotext Parlament (HTML)</th>
                        <td>
                            <textarea class="infotext" rows="6">@Model.ParlamentInfoHtml</textarea>
                        </td>
                    </tr>
                    <tr><th>API-Key</th>
                        <td>
                            <span>@Model.ApiKey</span>
                            <input type="hidden" id="representation-api-key" value="@Model.ApiKey" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>

<div class="content content-edit content-representation container-fluid">
    <div class="row-fluid row-small">
        <div class="span12 box-head">
            <i class="icon-edit-1"></i>
            <h3>3. Abgeordnete</h3>
        </div>
    </div>
</div>
@Code
    Dim bolShaded As Boolean = False
End Code
@For Each rp In Model.Representatives
    @<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-representation container-fluid representative-block">
        <div class="row-fluid">
            <div class="span12">
                <h3>@rp.Name</h3>
                <table class="table table-striped-reverse table-form">
                    <colgroup><col style="width:100px;" /></colgroup>
                    <tbody>
                        <tr><th>ID | Key</th>
                            <td style="padding-left: 10px;">
                                <em>@rp.ID</em>&nbsp;|&nbsp;<em>@rp.Key</em>
                                <input type="hidden" class="representative-id" value="@rp.ID" />
                                <input type="hidden" class="representative-key" value="@rp.Key" />
                            </td>
                        </tr>
                        <tr><th>Name</th>
                            <td><input type="text" class="representative-name" value="@rp.Name" /></td>
                        </tr>
                        <tr><th>Mail</th>
                            <td><input type="text" class="representative-mail" value="@rp.Mail" /></td>
                        </tr>
                        <tr><th>Telefon</th>
                            <td><input type="text" class="representative-phone" value="@rp.Phone" /></td>
                        </tr>
                        <tr><th>Twitter</th>
                            <td><input type="text" class="representative-twitter" value="@rp.Twitter" /></td>
                        </tr>
                        <tr><th>Partei</th>
                            <td><input type="text" class="representative-party" value="@rp.Party" /></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="2"><a class="btn btn-text btn-tiny" href="">Deaktivieren</a></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </div>
    bolShaded = Not bolShaded
Next

<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <button id="representative-new" class="btn btn-small btn-primary" onclick="newRepresentative(); return false;">Neuer Abgeordneter...</button>
        </div>
    </div>
</div>
<div class="content content-representation container-fluid representative-tmpl" id="representative-template" style="display:none;">
    <div class="row-fluid"><div class="span12">
    <h3 class="representative">NEU</h3>
    <table class="table table-striped-reverse table-form">
    <colgroup><col style="width:100px;" /></colgroup>
    <tbody>
    <tr><th>ID | Key</th><td style="padding-left: 10px;"><em>-</em>&nbsp;|&nbsp;<em>NEU</em>
        <input type="hidden" class="representative-id" value="-" />
        <input type="hidden" class="representative-key" value="NEU" /></td></tr>
    <tr><th>Name</th><td><input type="text" class="representative-name" value=""/></td></tr>
    <tr><th>Mail</th><td><input type="text" class="representative-mail" value=""/></td></tr>
    <tr><th>Twitter</th><td><input type="text" class="representative-twitter" value=""/></td></tr>
    <tr><th>Telefon</th><td><input type="text" class="representative-phone" value=""/></td></tr>
    <tr><th>Partei</th><td><input type="text" class="representative-party" value=""/></td></tr>
    </tbody>
    <tfoot><tr><td colspan="2"><a class="btn btn-text btn-tiny" onclick="removeNewRepresentative(this); return false;">Wieder entfernen</a></td></tr></tfoot>
    </table>
    </div></div>
</div>

<div class="content content-edit content-representation container-fluid">
    <div class="row-fluid row-small">
        <div class="span12 box-head">
            <i class="icon-edit-1"></i>
            <h3>4. Antragsprozess</h3>
        </div>
    </div>
</div>
<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <p>
                Der Antragsprozess ist hier aktuell noch nicht änderbar. 
                Wenn Du neue Schritte in Deinen Prozess aufnehmen willst, 
                schreib bitte ein Mail an 
                <a href="webmaster@openantrag.de">webmaster@openantrag.de</a>.
            </p>
        </div>
    </div>
</div>
<div class="content content-edit content-representation container-fluid">
    <div class="row-fluid row-small">
        <div class="span12 box-head">
            <i class="icon-edit-1"></i>
            <h3>5. Speichern & Senden</h3>
        </div>
    </div>
</div>
<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <p>
                Die Änderungen an den Seitendaten treten nicht sofort in Kraft, sondern werden 
                als Datenpaket an die Administration verschickt, wo sie zunächst geprüft und 
                dann eingestellt werden.
            </p>
            <p>
                <button class="btn btn-primary" onclick="getXML(); return false;">Speichern und Senden</button>
            </p>
        </div>
    </div>
</div>
