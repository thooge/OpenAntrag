/// <reference path="tools.js" />
// -----------------------------------------------------------------------------------
function getActionResponse(params) {

    document.body.style.cursor = "wait";

    //Standardwerte
    var defaults = {
        controller: "",
        action: "",
        properties: {},
        successFunction: function (s) { alertEx(s); },
        errorFunction: handleActionErrorDialog,
        finallyFunction: function () { },
        errorBoxWrapper: null
    };

    //Standards und Parameter zusammenführen
    var config = $.extend(defaults, params);

    //http://stackoverflow.com/questions/3676883/send-list-array-as-paramater-with-jquery-getjson

    $.getJSON(
        '/' + config.controller + '/' + config.action,
        $.param(config.properties, true),
        function (result) {
            if (result.success) {
                document.body.style.cursor = "default";
                config.successFunction(result.data);
            } else {
                document.body.style.cursor = "default";
                if (result.errorHtml && config.errorBoxWrapper != null) {
                    config.errorBoxWrapper.empty();
                    config.errorBoxWrapper.append(result.errorHtml);
                    config.errorBoxWrapper.show();
                } else {
                    config.errorFunction(result.error);
                }
            }
            config.finallyFunction();
        });
}
// -----------------------------------------------------------------------------------
function postActionResponse(params) {
    document.body.style.cursor = "wait";

    var defaults = {
        controller: "",
        action: "",
        properties: {},
        successFunction: function (s) { alertEx(s); },
        errorFunction: handleActionErrorDialog,
        finallyFunction: function () { },
        errorBoxWrapper: null
    };
    var config = $.extend(defaults, params);
    $.post(
        '/' + config.controller + '/' + config.action,
        $.param(config.properties, true),
        function (result) {
            if (result.success) {
                document.body.style.cursor = "default";
                config.successFunction(result.data);
            } else {
                document.body.style.cursor = "default";
                if (result.errorHtml && config.errorBoxWrapper != null) {
                    config.errorBoxWrapper.empty();
                    config.errorBoxWrapper.append(result.errorHtml);
                    config.errorBoxWrapper.show();
                } else {
                    config.errorFunction(result.error);
                }
            }
            config.finallyFunction();
        }, "json");
}
// -----------------------------------------------------------------------------------
function handleActionErrorDialog(errorText) {
    alertEx(errorText, 'error', 'Fehlerhinweis');
}