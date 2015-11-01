// =============================================    
$(function () {
    var markdown = new MarkdownDeep.Markdown();
    markdown.ExtraMode = true;
    markdown.SafeMode = false;
    prepareMDDEditor("mdd-editor-newfeedback");
    prepareMDDEditor("mdd-editor-newcomment");
    // -----------------------------------------
});
// =============================================
function createFeedback() {
    if ($('#newfeedback-form').valid()) {
        var oProperties = {
            'type': $("#Type").val(),
            'createdby': $("#CreatedBy").val(),
            'title': $("#Title").val(),
            'message': encodeURIComponent($("#Message").val())
        };
        postActionResponse({
            controller: "Feedback",
            action: "Service/CreateNew",
            properties: oProperties,
            successFunction: function (data) {
                window.location.href = window.location.href;
            }
        });
    }
}
// ------------------------------------------------------------------------------------------
function showFeedbackComment(feedbackID) {
    $("#newcomment-form").parent().slideUp(500, function () {
        if ($("#newcommentFeedbackID").val() != feedbackID) {
            $("#newcommentFeedbackID").val(feedbackID);
            $("#CommentedBy").val('');
            $("#Comment").val('');
            $("#" + feedbackID + " .newcomment-wrapper").append($("#newcomment-form")).slideDown(500);
            scrollToOffset($("#" + feedbackID).closest(".content"), 500);
        } else {
            $("#newcommentFeedbackID").val('');
            $("#CommentFormWrapper").append($("#newcomment-form"));
        }                
    });
}
// ---------------------------------------------------------------------------------------
function createFeedbackComment() {
    if ($("#newcomment-form").valid()) {
        var feedbackID = $("#newcommentFeedbackID").val();
        var oProperties = {
            'feedbackID': feedbackID,
            'commentedby': $("#CommentedBy").val(),
            'comment': encodeURIComponent($("#Comment").val())
        };
        postActionResponse({
            controller: "Feedback",
            action: "Service/CreateNewComment",
            properties: oProperties,
            successFunction: function (data) {
                showFeedbackComment(feedbackID);
                var html = $.parseHTML(data),
                    jData = $(html);
                $("#" + feedbackID + " .comments").append(jData);
            }
        });
    }
}
// ------------------------------------------------------------------------------------------
function deleteFeedbackComment(feedbackID, e) {
    var jE = $(e),
        jContainer = jE.closest(".comment"),
        sTimestamp = jE.data("timestamp"),
        sCommentedBy = jE.data("commentedby");
    confirmEx("Möchtest Du diesen Kommentar wirklich löschen?", "Kommentar löschen",
        function () {
            var oProperties = {
                'feedbackID': feedbackID,
                'commentedby': sCommentedBy,
                'timestamp': sTimestamp
            };
            getActionResponse({
                controller: "Feedback",
                action: "Service/DeleteComment",
                properties: oProperties,
                successFunction: function (data) {
                    jContainer.slideUp(function () {
                        $(this).remove();
                    });
                }
            });
        });
}
