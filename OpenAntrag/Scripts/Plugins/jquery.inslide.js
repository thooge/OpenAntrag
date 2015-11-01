/*
 * Inslide
 *
 * Copyright (c) 2014 Kristof Zerbe
 * 
  * Uses the same license as jQuery, see:
 * http://docs.jquery.com/License
 *
 * @version 1.0
 */
(function ($) {

    $.fn.inslide = function (options) {

        if (!this.length) { return this; }

        var settings = $.extend({
            //defaults
            moreItems: {},
            itemClick: function (s) {
                console.log(s);
            }
        }, options);
        
        return this.each(function () {

            var $e = $(this),
                curValue = $e.html(),
                $link = $('<a class="inslide-current" href="#"></a>'),
                $wrap = $('<span class="inslide-more"></span>'),
                $item = $('<a href="#"></a>');

            $e.empty();

            $link.html(curValue).click(function () {
                $e.find('.inslide-more').toggle(); return false;
            }).appendTo($e);

            $item.clone()
                .addClass("inslide-selected")
                .html(curValue)
                .click(function () {
                    $e.find('.inslide-more').toggle(); return false;
                })
                .appendTo($wrap);

            $.each(settings.moreItems, function (index, value) {
                $item.clone()
                    .html(value)
                    .click(function () { ItemClick(value); return false; })
                    .appendTo($wrap);
            });

            $wrap.appendTo($e);
            
            function ItemClick(value) {
                $e.find(".inslide-current").html(value);
                $e.find(".inslide-selected")
                    .removeClass("inslide-selected")
                    .unbind()
                    .click(function () { ItemClick($(this).html()); return false; });
                
                $e.find('.inslide-more a').each(function () {
                    if ($(this).html() === value) {
                        $(this).addClass("inslide-selected");
                    }
                });
                $e.find('.inslide-more').toggle(); 
                settings.itemClick(value);
            }

        });

    };

})(jQuery);