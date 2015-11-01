@Imports OpenAntrag
@ModelType Feedback

<select id="Type">
    @For Each f As FeedbackType In GlobalData.FeedbackTypes.Items
        @<option value="@f.ID">@f.Name</option>
    Next
</select>
                
@Html.TextBoxFor(Function(m) m.CreatedBy, New With {.placeholder = "Dein Name", .style = "width:95%"})
@Html.ValidationMessageFor(Function(m) m.CreatedBy)

@Html.TextBoxFor(Function(m) m.Title, New With {.placeholder = "Titel", .style = "width:95%"})
@Html.ValidationMessageFor(Function(m) m.Title)

<div id="mdd-editor-newfeedback" class="mdd-editor-container">
    @Html.TextAreaFor(Function(m) m.Message, 5, 0, New With {.placeholder = "Dein Feedback", .style = "width:95%"})
    @Html.ValidationMessageFor(Function(m) m.Message)
</div>

<div style="margin-top: 10px;">
    <button class="btn btn-primary" style="float:left;"
            onclick="createFeedback(); return false;" >Feedback speichern</button>
    <small style="line-height: 14px; display:block; margin-left: 200px;">
        Du kannst Dein Feedback auch jederzeit an <a href="mailto:feedback@openantrag.de">feedback@openantrag.de</a> richten.
    </small>
</div>                
