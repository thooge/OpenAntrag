// =============================================    


    var handler = null;
    
    // Prepare layout options.
    var options = {
        autoResize: true, // This will auto-update the layout when the browser window is resized.
        container: $('#nf-wrapper'), // Optional, used for some extra CSS styling
        offset: 20 // Optional, the distance between grid items
        //itemWidth: 310 // Optional, the width of a grid item
    };


    //function onScroll(event) {
    //    // Check if we're within 100 pixels of the bottom edge of the broser window.
    //    var winHeight = window.innerHeight ? window.innerHeight : $(window).height(); // iphone fix
    //    var closeToBottom = ($(window).scrollTop() + winHeight > $(document).height() - 100);

    //    if (closeToBottom) {
    //        // Get the first then items from the grid, clone them, and add them to the bottom of the grid.
    //        //var items = $('#nf-list li'),
    //        //    firstTen = items.slice(0, 10);
    //        //$('#nf-list').append(firstTen.clone());

    //        applyGrid();
    //    }
    //};
    
    //$(window).bind('scroll', onScroll);

    applyGrid();

// =============================================
function applyGrid() {
    
    // Destroy the old handler
    if (handler && handler.wookmarkInstance) {
        handler.wookmarkInstance.clear();
    }

    // Create a new layout handler.
    handler = $('#nf-list li');
    handler.wookmark(options);

    handler.each(function () {
        var sTime = $(this).data("time"),
            jTime = $(this).find("small"),
            sMoment = moment(sTime, "DD.MM.YYYY HH:mm:ss").calendar();
        jTime.text(sMoment);
    });

    /*
        // Capture clicks on grid items.
        handler.click(function () {
    
            // Update the layout.
            handler.wookmark(options);
        });
    */

}
// ---------------------------------------------
function getMore() {
    var iPage = parseInt($("#nf-page").val());
    var oProperties = {
        'type': $("#nf-filter-type").val(),
        'page': iPage + 1
    };
    getActionResponse({
        controller: "Notifications",
        action: "Service/GetMoreNotifications",
        properties: oProperties,
        successFunction: function (data) {
            if (data.length != 0) {
                $("#nf-page").val(iPage + 1);
                $('#nf-list').append(data);
                applyGrid();                
            } else {
                $("#nf-more-cmd").hide();
            }
        }
    });

}
