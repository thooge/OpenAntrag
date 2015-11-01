@Imports OpenAntrag
@ModelType ProcessStep

<img src="@Model.IconPath">
<small>@Now.ToString("dd. MMMM yyyy HH:mm")</small>
<em>@Html.Raw(Model.GetCaptionHtml)</em>
<br>
<div id="mdd-editor-nextstep" class="mdd-editor-container mdd-editor-inverse">
    <textarea id="nextstep-info" style="width: 100%;" rows="5"
        placeholder="Informationen über diesen Schritt"></textarea>
</div>
<button class="btn btn-primary"
    onclick="saveNextStep('@Model.Key_Representation', @Model.ID); return false;">Schritt speichern</button>
