@Imports OpenAntrag
@ModelType SuccessStory

<input id="ID_Proposal" type="hidden" value="@Model.ID_Proposal" />
<input id="Title" type="hidden" value="@Model.Title" />
<input id="StepDate" type="hidden" value="@Model.StepDate" />

<p>Schreib einen kurzen Text, in dem Du den Antrag und seinen Werdegang kurz vorstellst:</p>

<div id="mdd-editor-newsuccessstory" class="mdd-editor-container mdd-editor-inverse">
    @Html.TextAreaFor(Function(m) m.Text, 5, 0, New With {.placeholder = "Infotext zum erfolgreichen Antrag"})
    @Html.ValidationMessageFor(Function(m) m.Text)
</div>

<button onclick="createSuccessStory(); return false;" class="btn btn-primary" style="margin-top: 10px;">Erfolgsgeschichte speichern</button>