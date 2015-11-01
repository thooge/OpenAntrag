@Imports OpenAntrag
@ModelType FeedbackComment

@Html.TextBoxFor(Function(m) m.CommentedBy, New With {.placeholder = "Dein Name"})
@Html.ValidationMessageFor(Function(m) m.CommentedBy)

<div id="mdd-editor-newcomment" class="mdd-editor-container">
    @Html.TextAreaFor(Function(m) m.Comment, 5, 0, New With {.placeholder = "Dein Kommentar"})
    @Html.ValidationMessageFor(Function(m) m.Comment)
</div>

<input id="newcommentFeedbackID" type="hidden" value="" />

<div style="margin-top: 10px;">
    <button onclick="createFeedbackComment(); return false;" class="btn btn-small btn-primary">Kommentar speichern</button>
</div>
