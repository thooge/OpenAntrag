@Imports OpenAntrag
@ModelType Notification

<div id="newPostDialog" class="modal hide fade">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h3>Neue Meldung</h3>
    </div>
    <div class="modal-body">
        <p>Bitte gib Titel und Text der neuen Meldung an:</p>
        @Using Html.BeginForm("CreateNewPost", "Home", FormMethod.Post, New With {.id = "newpost-form", .name = "newpost-form"})
            @Html.TextBoxFor(Function(m) m.Title, New With {.placeholder = "Titel", .style = "display:block;width: 97%;"})    
            @Html.TextAreaFor(Function(m) m.Text, 5, 0, New With {.placeholder = "Mitteilungstext", .style = "display:block;width: 97%;"})
        End Using
    </div>
    <div class="modal-footer">
        <a href="#" data-dismiss="modal" class="btn">Abbrechen</a>
        <a href="javascript:go();" onclick="saveNewPost();" class="btn btn-primary">Speichern</a>
    </div>
</div>
