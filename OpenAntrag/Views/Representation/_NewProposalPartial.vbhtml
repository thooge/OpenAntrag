@Imports OpenAntrag
@ModelType Proposal

@Code
    Dim rep As Representation = Model.Representation
    Dim rps As RepresentationSetting = RepresentationSettings.GetByKey(rep.Key)
End Code

@If rep.IsViewOnly = False And rep.IsActive = True Then
    @Html.TextBoxFor(Function(m) m.Title, New With {.placeholder = "Titel Deines Antrags"})    
    @Html.ValidationMessageFor(Function(m) m.Title)

    @<div id="mdd-editor-newproposal" class="mdd-editor-container">
        @Html.TextAreaFor(Function(m) m.Text, 5, 0, New With {.placeholder = "Dein Antragstext"})
        @Html.ValidationMessageFor(Function(m) m.Text)
    </div>
Else
    @<p>Diese Antragsseite befindet sich noch im Aufbau und wird, sobald alle notwendigen 
        Informationen zusammengetragen sind und die Fraktion bzw. der Abgeordnete grünes Licht 
        gibt, aktiviert. 
        <br /><strong>Stay tuned...</strong>
        <br /><br />
     </p>
End If

<input id="Key_Representation" type="hidden" value="@Model.Key_Representation" />

<div style="margin-top: -10px">
    @If rps.HasContactPossibility = True Then
        @<small style="display:inline-block; margin-top: 5px; line-height:14px; width:99%">
            Wenn Du möchtest, daß die Fraktion mit Dir Kontakt aufnehmen kann, kannst Du 
            <a href="javascript:do();" onclick="$('#ContactInformation').slideToggle()">
                <strong>hier </strong> die von Dir gewünschten Kontaktinformationen eingeben.</a> 
            Diese werden <strong>nicht</strong> gespeichert, sondern lediglich via Mail an die Fraktion übermittelt!
         </small>
        @<input type="text" value="" placeholder="Kontaktinformationen" style="display:none; margin-top: 5px;"
            name="ContactInformation" id="ContactInformation">
        @<small></small>
    End If
       
    @If rep.IsViewOnly = False And rep.IsActive = True Then
        @<button onclick="createProposal(); return false;" class="btn btn-primary" style="margin-top: 10px;">Antrag senden</button>
    End If    
</div>
