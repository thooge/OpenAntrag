// =============================================
function saveRepresentationSetting(sKey) {
    var oProperties = {
        'key': sKey,
        'hasContactPossibility': $("#HasContactPossibility").is(':checked')
    };
    getActionResponse({
        controller: "Home",
        action: "SaveRepresentationSetting",
        properties: oProperties,
        successFunction: function (data) {
            window.location.href = data;
        }
    });
}
// =============================================
function showNewPost() {
    getActionResponse({
        controller: "Notifications",
        action: "Service/GetNewPostPartial",
        properties: {},
        successFunction: function (data) {
            $("body").append(data);
            $("#newPostDialog").modal().on('hidden', function () {
                $(this).remove();
            });
        }
    });
}
// =============================================
function saveNewPost() {
    var jD = $("#newPostDialog"),
        sTitle = jD.find("#Title").val(),
        sText = jD.find("#Text").val();
    
    if (sTitle.length === 0) { alertEx("Bitte Titel eingeben", "error", "Neue Meldung"); return false; }
    if (sText.length === 0) { alertEx("Bitte Text eingeben", "error", "Neue Meldung"); return false; }

    jD.modal('hide');
    
    var oProperties = {
        'sTitle': sTitle,
        'sText': sText
    };
    getActionResponse({
        controller: "Notifications",
        action: "Service/CreateNewPost",
        properties: oProperties,
        successFunction: function (data) {
            window.location.href = data;
        }
    });
}
// =============================================
