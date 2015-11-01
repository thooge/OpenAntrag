(function($) {

    // Validierung nur onBlur, nicht onKeyUp
    jQuery.validator.setDefaults({ onkeyup: false });

    // -----------------------------------------------------------------------------------
    jQuery.validator.addMethod("mailvalid", function(value, element, param) {
        return validEmail(value);
    });
    jQuery.validator.unobtrusive.adapters.addBool('mailvalid');
    // -----------------------------------------------------------------------------------
    jQuery.validator.addMethod("urlvalid", function(value, element, param) {
        if (value.length != 0) {
            return validUrl(value);
        } else {
            return true;
        }
    });
    jQuery.validator.unobtrusive.adapters.addBool('urlvalid');
    // -----------------------------------------------------------------------------------
}(jQuery));