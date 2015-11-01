// =============================================    
$(function () {
    // -----------------------------------------
});
// =============================================
// ------------------------------------------------------------------------------------------
function createProposal() {
    if ($('#newproposal-form').valid()) {
        var sContactInfo = "";
        if ($("#ContactInformation").length > 0) {
             sContactInfo = $("#ContactInformation").val();
        }        
        var oProperties = {
            'keyRepresentation': $("#Key_Representation").val(),
            'title': $("#Title").val(),
            'text': encodeURIComponent($("#Text").val()),
            'contactInfo': sContactInfo
        };
        postActionResponse({
            controller: "Representation",
            action: "CreateProposal",
            properties: oProperties,
            successFunction: function (data) {
                window.location.href = data;
            }
        });
    }
}
// ------------------------------------------------------------------------------------------
function saveProposalText() {
    var idProposal = $("#proposal-id").val(),
        sText = encodeURIComponent($("#TextMarkdown").val());
    
    var oProperties = {
        'idProposal': idProposal,
        'sText': sText
    };
    postActionResponse({
        controller: "Representation",
        action: "SaveProposalText",
        properties: oProperties,
        successFunction: function (data) {
            window.location.href = window.location.href;
        }
    });
}
// ------------------------------------------------------------------------------------------
function saveProposalDate(e) {
    var idProposal = $("#proposal-id").val(),
        jE = $(e),
        sNewDate = jE.val();

    var oProperties = {
        'idProposal': idProposal,
        'sDate': sNewDate
    };
    getActionResponse({
        controller: "Representation",
        action: "SaveProposalDate",
        properties: oProperties,
        successFunction: function (data) {
        }
    });
}
// ------------------------------------------------------------------------------------------
function saveProposalStepDate(e) {
    var idProposal = $("#proposal-id").val(),
        jE = $(e),
        idStep = jE.data("step-id"),
        sNewDate = jE.val();

    var oProperties = {
        'idProposal': idProposal,
        'idStep': idStep,
        'sDate': sNewDate
    };
    getActionResponse({
        controller: "Representation",
        action: "SaveProposalStepDate",
        properties: oProperties,
        successFunction: function (data) {
        }
    });
}
// ------------------------------------------------------------------------------------------
function deleteProposal(idProposal) {
    var sMsg =
        "<strong>Möchtest Du diesen Antrag wirklich löschen?</strong>" +
        "<br><br>" +
        "(Komplette Anträge zu löschen sollte nur geschehen, wenn er aus " +
        "Versehen oder zu Testzwecken angelegt wurde)";
    
    confirmEx(sMsg, "Antrag löschen",
        function () {
            var oProperties = {
                'idProposal': idProposal,
            };
            getActionResponse({
                controller: "Representation",
                action: "DeleteProposal",
                properties: oProperties,
                successFunction: function (data) {
                    window.location.href = data;                    
                }
            });
        });
}
// ------------------------------------------------------------------------------------------
function deleteProposalStep(idProposal, idStep) {
    var sMsg =
        "<strong>Möchtest Du diesen Antragsschritt wirklich löschen?</strong>";

    confirmEx(sMsg, "Antragsschritt löschen",
        function () {
            var oProperties = {
                'idProposal': idProposal,
                'idStep': idStep,
            };
            getActionResponse({
                controller: "Representation",
                action: "DeleteProposalStep",
                properties: oProperties,
                successFunction: function (data) {
                    window.location.href = data;
                }
            });
        });
}
// ------------------------------------------------------------------------------------------
function editProposalStep(e, idStep) {
    var $this = $(e),
        $body = $this.parents(".proposalstep-body"),
        $editor = $body.find(".mdd-editor-container");
    
    $this.tooltipster('update', 'Antragsschritt speichern')
        .find("i")
            .removeClass("icon-edit").addClass("icon-floppy")
            .css("color", "#E80000");
    
    $body.find(".info").slideUp(function () {
        $editor.slideDown(function () {
            $editor.find("textarea").autosize();
            prepareMDDEditor($editor.attr("id"));
        });
    });

    $this.attr('onclick', '').unbind('click').click(function () {
        var idProposal = $("#proposal-id").val(),
            sInfo = $editor.find("textarea").val();

        var oProperties = {
            'idProposal': idProposal,
            'idStep': idStep,
            'sInfo': sInfo
        };
        getActionResponse({
            controller: "Representation",
            action: "SaveProposalStepText",
            properties: oProperties,
            successFunction: function (data) {
                window.location.href = window.location.href;
            }
        });        
     });
}
// ------------------------------------------------------------------------------------------
function showProposalAbuse() {
    $("#proposal-blockabuse").slideToggle(function () {
        prepareMDDEditor("mdd-editor-blockabuse");
        scrollToOffset($("#proposal-blockabuse"), 500);
    });
}
// ------------------------------------------------------------------------------------------
function blockProposalAbuse(idProposal) {
    var sAbuseMessage = encodeURIComponent($("#AbuseMessage").val());
    if (sAbuseMessage.length === 0) {
        alertEx("Bitte gib eine Begründung für die Blockierung des Antrags an", "warning");
        $("#AbuseMessage").focus();
        return;
    }
    var oProperties = {
        'idProposal': idProposal,
        'abuseMessage': sAbuseMessage
    };
    getActionResponse({
        controller: "Representation",
        action: "SaveProposalAbuseMessage",
        properties: oProperties,
        successFunction: function (data) {
            window.location.reload(true);
        }
    });
}
// ------------------------------------------------------------------------------------------
function removeProposalAbuse(idProposal) {
    var sMsg = "Möchtest Du diesen Antrag wirklich wieder freischalten?";    
    confirmEx(sMsg, "Antrag freischalten?",
        function () {
            var oProperties = {
                'idProposal': idProposal,
                'abuseMessage': null
            };
            getActionResponse({
                controller: "Representation",
                action: "SaveProposalAbuseMessage",
                properties: oProperties,
                successFunction: function (data) {
                    window.location.reload(true);
                }
            });
        });
}
// ------------------------------------------------------------------------------------------
function closeCommenting(idProposal) {
    var sMsg = "Möchtest Du die Kommentarfunktion für diesen Antrag wirklich schließen?";
    confirmEx(sMsg, "Kommentarfunktion schließen?",
        function () {
            var oProperties = {
                'idProposal': idProposal,
                'lock': true
            };
            getActionResponse({
                controller: "Representation",
                action: "SaveCommentingStatus",
                properties: oProperties,
                successFunction: function (data) {
                    window.location.reload(true);
                }
            });
        });
}
// ------------------------------------------------------------------------------------------
function reopenCommenting(idProposal) {
    var sMsg = "Möchtest Du die Kommentarfunktion für diesen Antrag wirklich wieder öffnen?";
    confirmEx(sMsg, "Kommentarfunktion wieder öffnen?",
        function () {
            var oProperties = {
                'idProposal': idProposal,
                'lock': false
            };
            getActionResponse({
                controller: "Representation",
                action: "SaveCommentingStatus",
                properties: oProperties,
                successFunction: function (data) {
                    window.location.reload(true);
                }
            });
        });
}
// ------------------------------------------------------------------------------------------
function showProposalComment(e) {

    $(e).slideUp(function () {
        $('#newcomment-form').slideDown(function () {
            var markdown = new MarkdownDeep.Markdown();
            markdown.ExtraMode = true;
            markdown.SafeMode = false;
            prepareMDDEditor("mdd-editor-newproposalcomment");
            scrollToOffset($("#comment-new"), 500);
        });
    }).remove();
}
// ------------------------------------------------------------------------------------------
function createProposalComment() {
    if ($('#newcomment-form').valid()) {
        var oProperties = {
            'idProposal': $("#ProposalID").val(),
            'commentBy': $("#CommentedBy").val(),
            'commentText': encodeURIComponent($("#Comment").val())
        };
        postActionResponse({
            controller: "Representation",
            action: "CreateProposalComment",
            properties: oProperties,
            successFunction: function (data) {
                $('#newcomment-form').slideUp(function () {
                    $("#proposal-comments").append(data);
                }).remove();
                scrollBottom();
            }
        });

    }
}
// ------------------------------------------------------------------------------------------
function deleteProposalComment(proposalID, e) {
    var jE = $(e),
        jContainer = jE.closest(".comment"),
        sTimestamp = jE.data("timestamp"),
        sCommentedBy = jE.data("commentedby");
    confirmEx("Möchtest Du diesen Kommentar wirklich löschen?", "Kommentar löschen",
        function () {
            var oProperties = {
                'proposalID': proposalID,
                'commentedby': sCommentedBy,
                'timestamp': sTimestamp
            };
            getActionResponse({
                controller: "Representation",
                action: "DeleteProposalComment",
                properties: oProperties,
                successFunction: function (data) {
                    jContainer.slideUp(function () {
                        $(this).remove();
                    });
                }
            });
        });
}
// ------------------------------------------------------------------------------------------
function setNextStep(e) {
    var jE = $(e),
        jBar = $("#nextstep-bar"),
        jBody = $("#nextstep-body"),
        sRepKey = jE.data("repid"),
        sStepId = jE.data("stepid"),
        sColor = jE.data("color");

    var oProperties = {
        'keyRepresentation': sRepKey,
        'idStep': sStepId
    };
    getActionResponse({
        controller: "Representation",
        action: "GetNextStepHtml",
        properties: oProperties,
        successFunction: function (data) {
            jBar.css("background-color", sColor);
            jBody.html(data);

            $('.selectpicker').selectpicker({
                style: 'select-inline'
            });
            
            var markdown = new MarkdownDeep.Markdown();
            markdown.ExtraMode = true;
            markdown.SafeMode = false;
            prepareMDDEditor("mdd-editor-nextstep");

            $("#nextstep-hint").remove();
            jBar.slideDown();
            jBody.slideDown();
            scrollToOffset($("#proposaledit-container"), 500);
        }
    });    
}
// ------------------------------------------------------------------------------------------
function saveNextStep(keyRepresentation, idStep) {
    var idProposal = $("i.icon-doc").attr("rel"),
        sInfo = encodeURIComponent($("#nextstep-info").val()),
        aOptions = [];
    
    if (sInfo.length === 0) {
        alertEx("Bitte gib eine Information über diesen Schritt an", "warning");
        $("#nextstep-info").focus();
        return;
    }

    $("select.nextstep-option").each(function () {
        var sOption = $(this).attr("id") + "|" + $(this).val();
        aOptions.push(sOption);
    });

    //return;

    var oProperties = {
        'idProposal': idProposal,
        'keyRepresentation': keyRepresentation,
        'idStep': idStep,
        'info': sInfo,
        'options': aOptions.toString()
    };
    postActionResponse({
        controller: "Representation",
        action: "SaveNextStep",
        properties: oProperties,
        successFunction: function (data) {
            window.location.reload(true);
        }
    });
}
// ------------------------------------------------------------------------------------------
function initTagSelect() {
    var idProposal = $("i.icon-doc").attr("rel");
    getActionResponse({
        controller: "Representation",
        action: "GetProposalTags",
        properties: {},
        successFunction: function (data) {
            $('#proposal-tags-edit').selectize({
                delimiter: ',',
                persist: false,
                openOnFocus: true,
                valueField: 'Tag',
                labelField: 'Tag',
                searchField: 'Tag',
                options: data,
                create: true,
                //function (input) {
                //    console.log('create: ' + input);
                //    return { Tag: input };
                //},
                onChange: function (value) {
                    saveProposalTags(idProposal, value);
                }
            });
        }
    });
}
// ------------------------------------------------------------------------------------------
function saveProposalTags(idProposal, tagList) {
    var oProperties = {
        'idProposal': idProposal,
        'tagList': tagList
    };
    getActionResponse({
        controller: "Representation",
        action: "SaveProposalTags",
        properties: oProperties,
        successFunction: function (data) {
            $("#proposal-tags-list").slideUp(function () {
                $(this).empty().append(data).slideDown();
            });
        }
    });
}
// ------------------------------------------------------------------------------------------
function setSuccessStoryStatus(bSuccess) {
    if (bSuccess) {
        scrollToOffset($('#success-story-new'), 500);
        $("#success-story-add").slideToggle(function () {
            prepareMDDEditor("mdd-editor-newsuccessstory");
        });
    } else {
        var oProperties = {
            'idProposal': $("#ID_Proposal").val(),
            'status': -1
        };
        postActionResponse({
            controller: "Representation",
            action: "SetSuccessStoryStatus",
            properties: oProperties,
            successFunction: function (data) {
                window.location.href = data;
            }
        });
    }
}
// ------------------------------------------------------------------------------------------
function createSuccessStory() {
    if ($('#newsuccessstory-form').valid()) {
        var oProperties = {
            'idProposal': $("#ID_Proposal").val(),
            'title': $("#Title").val(),
            'stepDate': $("#StepDate").val(),
            'text': encodeURIComponent($("#Text").val())
        };
        postActionResponse({
            controller: "Representation",
            action: "CreateSuccessStory",
            properties: oProperties,
            successFunction: function (data) {
                window.location.href = data;
            }
        });
    }
}
// ------------------------------------------------------------------------------------------