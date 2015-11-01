String.prototype.endsWith = function(pattern) {
    var d = this.length - pattern.length;
    return d >= 0 && this.lastIndexOf(pattern) === d;
};
/* ----------------------------------------------------------------------------------- */
function go() { void (0); }
/* ----------------------------------------------------------------------------------- */
function scrollToOffset(jE, scrollSpeed, addNavOffset) {
    //jQuery-ScrollTo-Erweiterungsmethode, um das Headroom-bedingte Offset auszugleichen
    var iOffset = 0,
        iNavHeight = $("nav").height();
    
    if ($("nav").hasClass("hrm-not-top") === false) {
        iOffset += iNavHeight;
        console.log(iOffset);
    }
    if (addNavOffset != undefined && addNavOffset === true) {
        iOffset += iNavHeight;
    }
    $.scrollTo(jE.offset().top - iOffset, scrollSpeed);    
}
/* ----------------------------------------------------------------------------------- */
function scrollBottom() {
    $('html, body').animate({ scrollTop: $(document).height() }, 500);
}
/* ----------------------------------------------------------------------------------- */
function goHash(hash) {
    scrollToOffset($("a[name='" + hash + "']"), 500);
}
/* ----------------------------------------------------------------------------------- */
function parseBool(value) {
    return (typeof value === "undefined") ?
           false :
            // trim using jQuery.trim()'s source 
           value.replace(/^\s+|\s+$/g, "").toLowerCase() === "true";
}
/* ----------------------------------------------------------------------------------- */
function validEmail(elementValue) {
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailPattern.test(elementValue);
}
/* ----------------------------------------------------------------------------------- */
function validUrl(elementValue) {
    var urlPattern = /^\b((?:https?:\/\/|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}\/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()[\]{};:'".,<>?«»“”‘’]))$/;
    return urlPattern.test(elementValue);
}
/* ----------------------------------------------------------------------------------- */
function stripHtml(sText) {
    return sText.replace(/(<.*?>)/ig, "");
    // /(<([^>]+)>)/ig
}
/* ----------------------------------------------------------------------------------- */
function pad(number, length) {
    //konvertiert eine Zahl in einen String mit führenden Nullen
    var str = '' + number;
    while (str.length < length) {
        str = '0' + str;
    }
    return str;
}
/* ----------------------------------------------------------------------------------- */
function xmlToString(xmlData) {

    var xmlString;
    
    if (window.ActiveXObject) { //IE
        xmlString = xmlData.xml;
    } else { // code for Mozilla, Firefox, Opera, etc.
        xmlString = (new XMLSerializer()).serializeToString(xmlData);
    }
    return xmlString;
}
/* ----------------------------------------------------------------------------------- */
function datAdd(datValue, daysAdd) {
    var myDate = new Date(datValue);
    myDate.setDate(myDate.getDate() + daysAdd);
    return myDate;
}
/* ----------------------------------------------------------------------------------- */
function datDiff(date1, date2, interval) {
    var second = 1000,
        minute = second * 60,
        hour = minute * 60,
        day = hour * 24,
        week = day * 7;
    date1 = new Date(date1);
    date2 = new Date(date2);
    var timediff = date2 - date1;
    if (isNaN(timediff)) return NaN;
    switch (interval) {
        case "years": return date2.getFullYear() - date1.getFullYear();
        case "months": return (
            (date2.getFullYear() * 12 + date2.getMonth())
            -
            (date1.getFullYear() * 12 + date1.getMonth())
        );
        case "weeks": return Math.floor(timediff / week);
        case "days": return Math.floor(timediff / day);
        case "hours": return Math.floor(timediff / hour);
        case "minutes": return Math.floor(timediff / minute);
        case "seconds": return Math.floor(timediff / second);
        default: return undefined;
    }
}
/* ----------------------------------------------------------------------------------- */
function alertEx(sText, sType, sTitle, fOK) {
    $(document).ready(function () {
        //sType: info, error; question, warning
        
        if (!sType) sType = 'info';
        if (!sTitle) sTitle = 'Hinweis';

        //http://www.littlesparkvt.com/flatstrap/javascript.html#modals
        
        var eModal = $('<div class="modal hide fade"></div>')
            .attr({ id: 'modalEx', title: sTitle }
        );
        eModal.addClass('modal-' + sType);

        var eModalHeader = $('<div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button></div>');
        eModalHeader.append('<h3>' + sTitle + '</h3>');
        eModal.append(eModalHeader);
        
        var eModalBody = $('<div class="modal-body"></div>');
        eModalBody.append('<p>' + sText + '</p>');
        eModal.append(eModalBody);

        var eModalFooter = $('<div class="modal-footer"></div>');
        eModalFooter.append('<button class="btn" data-dismiss="modal" aria-hidden="true">Schließen</button>');
        eModal.append(eModalFooter);

        eModal.modal('show').on('hidden', function () {
            if (typeof fOK == 'function') { fOK(); }
        });
    });
    return false;
}
/* ----------------------------------------------------------------------------------- */
function confirmEx(sText, sTitle, fYes, fNo) {
    $(document).ready(function () {

        var eModal = $('<div class="modal hide fade"></div>')
            .attr({ id: 'modalEx', title: sTitle }
        );

        var eModalHeader = $('<div class="modal-header"></div>');
        eModalHeader.append('<h3>' + sTitle + '</h3>');
        eModal.append(eModalHeader);

        var eModalBody = $('<div class="modal-body"></div>');
        eModalBody.append('<p>' + sText + '</p>');
        eModal.append(eModalBody);

        var eModalFooter = $('<div class="modal-footer"></div>');
        
        var eButtonYes = $('<button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Ja</button>');
        if (typeof fYes == 'function') { eButtonYes.bind('click', fYes); }
        eModalFooter.append(eButtonYes);
        
        var eButtonNo = $('<button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Nein</button>');
        if (typeof fNo == 'function') { eButtonNo.bind('click', fNo); }
        eModalFooter.append(eButtonNo);
        
        eModal.append(eModalFooter);

        eModal.modal('show');
    });
    return false;
}
/* ----------------------------------------------------------------------------------- */
function prettyPrintJson(json) {
    //http://stackoverflow.com/questions/4810841/json-pretty-print-using-javascript
    
    if (typeof json != 'string') {
        json = JSON.stringify(json, undefined, 2);
    }
    json = json.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
    return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
        var cls = 'number';
        if (/^"/.test(match)) {
            if (/:$/.test(match)) {
                cls = 'key';
            } else {
                cls = 'string';
            }
        } else if (/true|false/.test(match)) {
            cls = 'boolean';
        } else if (/null/.test(match)) {
            cls = 'null';
        }
        return '<span class="' + cls + '">' + match + '</span>';
    });
}
/* ----------------------------------------------------------------------------------- */
function prettyPrintXml(xml) {
    var formatted = '';
    var reg = /(>)(<)(\/*)/g;
    xml = xml.replace(reg, '$1\r\n$2$3');
    var pad = 0;
    jQuery.each(xml.split('\r\n'), function (index, node) {
        var indent = 0;
        if (node.match(/.+<\/\w[^>]*>$/)) {
            indent = 0;
        } else if (node.match(/^<\/\w/)) {
            if (pad != 0) {
                pad -= 1;
            }
        } else if (node.match(/^<\w[^>]*[^\/]>.*$/)) {
            indent = 1;
        } else {
            indent = 0;
        }

        var padding = '';
        for (var i = 0; i < pad; i++) {
            padding += ' ';
        }

        formatted += padding + node + '\r\n';
        pad += indent;
    });

    return formatted;
}
/* ----------------------------------------------------------------------------------- */
(function ($) {
    jQuery.fn.putCursorAtEnd = function () {
        return this.each(function () {
            $(this).focus()

            // If this function exists...
            if (this.setSelectionRange) {
                // ... then use it
                // (Doesn't work in IE)

                // Double the length because Opera is inconsistent about whether a carriage return is one character or two. Sigh.
                var len = $(this).val().length * 2;
                this.setSelectionRange(len, len);
            }
            else {
                // ... otherwise replace the contents with itself
                // (Doesn't work in Google Chrome)
                $(this).val($(this).val());
            }

            // Scroll to the bottom, in case we're in a tall textarea
            // (Necessary for Firefox and Google Chrome)
            this.scrollTop = 999999;
        });
    };
})(jQuery);
/* ----------------------------------------------------------------------------------- */
jQuery.fn.reverse = [].reverse;