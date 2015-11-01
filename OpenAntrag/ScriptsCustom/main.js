
var _scroll = false;
// =============================================
$(document).ready(function () {
    // -----------------------------------------
    moment.lang('de');
    // -----------------------------------------
    doTooltipster($('.tt-std'), '.tooltipster-standard', 'top');
    doTooltipster($('#mainnav .nav-item'), '.tooltipster-mainnav', 'top');
    doTooltipster($('#repnav .nav-item'), '.tooltipster-repnav', 'bottom');
    // -----------------------------------------
    initRatingGroups();
    // -----------------------------------------
    $(window).scroll(function () {
        _scroll = true;
    });
    setInterval(function () {
        if (_scroll) {            
            //-- BACKTOP
            var el = $("#backtop"),
                wTop = $(window).scrollTop(),
                wH = $(window).height(),
                dH = $(document).height(),                
                nLimit = $("header").offset().top + $("header").height(),
                //nLimit = 0,
                p = parseInt(((wTop + wH - nLimit) / dH * 100) + 0.5) / 100;
            (wTop > nLimit) ? el.css("display", "block") : el.css("display", "none");
            el.css("opacity", p);
            _scroll = false;
        }
    }, 50);
    // -----------------------------------------
    initNotifications();
    // -----------------------------------------
    $('textarea:visible').autosize();
    // -----------------------------------------
    $("input.readonly").each(function() {
        var def = $(this).val();
        $(this)[0].addEventListener("input", function () {
                this.value = def;
            }, false);
        $(this).click(function() {
            $(this).focus().select();
        });
    });
    // -----------------------------------------
    var jDSM = $.cookie("OpenAntrag-DisableScrollMenu");
    //alert($(window).height() + ' | ' + $(document).height() + ' = ' + ($(document).height() - $(window).height()));
    //alert($("nav").position().top + $("nav").height());
    if ((jDSM === null || jDSM === 'false') && ($(document).height() - $(window).height()) > ($("nav").position().top + $("nav").height())) {
        var headroom = new Headroom(document.querySelector("nav"), {
            offset: $("nav").position().top + 10,
            tolerance: 0,
            classes: {
                initial: "hrm",
                pinned: "hrm-pinned",
                unpinned: "hrm-unpinned",
                top: "hrm-top",
                notTop: "hrm-not-top"
            },
            onTop: function() {
                $("#navlogo").hide("slide");
            },
            onNotTop: function () {
                $("#navlogo").show( "slide" );
            }
        });
        headroom.init();        
    }
    // -----------------------------------------
});
// =============================================
function initLayoutPage() {
    $(document).ready(function () {
        $('#headnav-toggle').click(function () {
            $("#country-headnav").slideToggle();
        });
        $('#navmenu').click(function () {
            $("#mainsubnav-container").slideToggle(function () {
                if ($("#mainsubnav-container").is(':visible')) {
                    $("#mainnav a.nav-item.nav-icon-mere").each(function() {
                        if ($(this).find("span.nav-text").is(":hidden")) {
                            $(this).clone().removeClass("nav-icon-mere").appendTo("#mainsubnav-container");
                            $(this).fadeOut();
                        }
                    });
                    //--
                    $("#mainnav a.nav-item.nav-icon-only").clone().removeClass("nav-icon-only").appendTo("#mainsubnav-container");
                    $("#mainnav a.nav-item.nav-icon-only").fadeOut();
                } else {
                    $("#mainsubnav-container").empty();
                    //--
                    $("#mainnav a.nav-item.nav-icon-mere").fadeIn();
                    $("#mainnav a.nav-item.nav-icon-only").fadeIn();
                }
            });
            return false;
        });
        $('#subnav-toggle').click(function () {
            $("#mainsubnav-rep-container").slideToggle(function () {
                $('#mainsubnav-rep-list').perfectScrollbar({
                    wheelSpeed: 8
                });
            });
            return false;
        });
        $('#footer-commands a').click(function () {
            toggleFooterMore($(this).attr("rel"));
        });
    });
}
// =============================================
function initReadMore(sE) {
    $(sE).readmore({
        speed: 500,
        maxHeight: 200,
        moreLink: '<button class="btn btn-small btn-readmore">mehr lesen...</button>',
        lessLink: '<button class="btn btn-small btn-readmore">...weniger</button>',
        expandedLinkClass: 'btn-readmore-expanded',
        collapsedLinkClass: 'btn-readmore-collapsed',
        beforeToggle: function (trigger, element, expanded) {
            if (expanded === false) {
                scrollToOffset(element.parents(".content"), 500, true);
            }
        }
    });
}
// -----------------------------------------------------------------------------------
function initNotifications() {
    $(document).ready(function () {
        var j = $("#notification-box"),
            jX = $("#notification-box > a");
        if (j.is(':visible') && jX.length != 0) {
            showNotification(jX, 0);
        }        
    });
}
//---
function showNotification(jX, i) {
    var sTime = jX.eq(i).data("time"),
        jTime = jX.eq(i).find("small"),
        sMoment = moment(sTime, "DD.MM.YYYY HH:mm:ss").calendar();
    jTime.text(sMoment);
    
    jX.eq(i).fadeIn(1000, function () {
        setTimeout(function () {
            jX.eq(i).animate({'top':'-100px'}, 1000, function () {
                $(this).hide().css("top", "");
                i += 1;
                if (i === jX.length) { i = 0; }
                showNotification(jX, i);
            });
        }, 10000);
    });
}
// =============================================
var currentMore = '';
function toggleFooterMore(sID) {
    if (currentMore.length != 0 && currentMore != sID) {
        toggleFooterMore(currentMore);
    }

    var jMore = $("#" + sID),
        docHeight = $(document).height(),
        windowHeight = $(window).height(),
        moreHeight = jMore.height(),
        scrollPos = docHeight - windowHeight;

    jMore.animate({ height: "toggle" }, 500);

    if (moreHeight > window.innerHeight) {
        scrollPos = jMore.offset().top;
    }

    if (currentMore.length === 0) {
        currentMore = sID;
        
        switch (currentMore) {
            case "more-settings":
                var sFirstRep = $("#more-settings-first").val();
                $("#more-settings-placeholder").empty();
                getSettings(sFirstRep, function (settingsHeight) {
                    moreHeight = jMore.height() + settingsHeight;
                    scrollPos += moreHeight;
                    $('html, body').animate({ scrollTop: scrollPos + 'px' }, 500);
                });
            default:
                scrollPos += moreHeight;
                $('html, body').animate({ scrollTop: scrollPos + 'px' }, 500);
        }                
    } else {
        currentMore = "";
    }
}
// -----------------------------------------------------------------------------------
function showSettings(sKey) {
    var jSettings = $("#more-settings-placeholder"),
        docHeight = $(document).height(),
        windowHeight = $(window).height(),
        settingsHeight = jSettings.height(),
        scrollPos = docHeight - windowHeight + settingsHeight;

    $("#more-settings-wrapper").css("width", $("#more-settings-wrapper").width() + "px");

    jSettings.slideUp(function () {
        $('html, body').animate({ scrollTop: scrollPos + 'px' }, 500);
        getSettings(sKey, function () {
            $('html, body').animate({ scrollTop: scrollPos + 'px' }, 500);
        });
    });

    return false;
}
// -----------------------------------------------------------------------------------
function doTooltipster(jE, sTheme, sPosition) {
    if (sTheme === undefined) sTheme = ".tooltipster-standard";
    jE.tooltipster({
        animation: 'fade',
        delay: 200,
        theme: sTheme,
        position: sPosition
    });
}
// -----------------------------------------------------------------------------------
function initRatingGroups() {
    $(".rating-group").each(function () {
        var jR = $(this),
            iRating = jR.data("rating");
        jR.find("a").each(function () {
            var jA = $(this);
            if (jA.data("rate") <= iRating) {
                jA.find("i").removeClass("icon-star-empty").addClass("icon-star");
            }
            jA.hover(
                function () {
                    var iCurrent = jA.data("rate");
                    jR.find("a").each(function () {
                        $(this).css("color", "#333");
                        if ($(this).data("rate") <= iCurrent) {                            
                            $(this).find("i").removeClass("icon-star-empty").addClass("icon-star");
                        } else {
                            $(this).find("i").removeClass("icon-star").addClass("icon-star-empty");
                        }
                    });
                },
                function () {                    
                    jR.find("a").each(function () {
                        $(this).css("color", "");
                        if ($(this).data("rate") <= iRating) {
                            $(this).find("i").removeClass("icon-star-empty").addClass("icon-star");
                        } else {
                            $(this).find("i").removeClass("icon-star").addClass("icon-star-empty");
                        }
                    });
                });            
        });
    });
}
// ------------------------------------------------------------------------------------------
function rateProposal(idProposal, ratingPoints) {

    var sRatesStored = $.jStorage.get("rates");
    
    if (sRatesStored != null && sRatesStored.contains(idProposal)) {
        alertEx("<strong>Du unterstützt diesen Antrag bereits</strong><br>" +
            "<p>Diese Information wird nach den Grundsätzen des Datenschutzes und der Datensparsamkeit " +
            "nicht auf dem Server gespeichert sondern lokal in Deinem Browser (localStorage).</p>" +
            "<p>Aus diesem Grunde sei hiermit an Dich appeliert, einen Antrag nicht bewußt mehrmals " +
            "zu unterstützen, um der Fraktion ein authentisches Bild zu liefern.</p>");
        return;
    }
    
    sRatesStored === null ? sRatesStored = idProposal : sRatesStored += "|" + idProposal;
    
    $.jStorage.set("rates", sRatesStored);
    
    var oProperties = {
        'idProposal': idProposal,
        'iRate': ratingPoints
    };
    postActionResponse({
        controller: "Representation",
        action: "SaveProposalRating",
        properties: oProperties,
        successFunction: function (data) {
            window.location.href = window.location.href;
        }
    });

}
// -----------------------------------------------------------------------------------
function getSettings(sKey, fSuccess) {
    
    getActionResponse({
        controller: "Home",
        action: "GetRepresentationSettingPartial",
        properties: { "key": sKey },
        successFunction: function (data) {
            $("#more-settings-placeholder").empty().append(data);            
            $("#more-settings-placeholder").slideDown(function () {
                fSuccess($("#more-settings-placeholder").height());
            });            
            $("#settings-group").find("button").removeClass("btn-selected");
            $("#settings-" + sKey).addClass("btn-selected");
        }
    });
}
// -----------------------------------------------------------------------------------
function toggleContentInfo(btn, sInfo) {
    var jE = $("#" + sInfo);
    jE.slideToggle(function () {
        scrollToOffset(jE, 500);
    });
}
// -----------------------------------------------------------------------------------
function resetPW() {
    if ($("#resetpw-form").valid()) {
        var oModel = {
            'UserNameReset': $("#UserNameReset").val()
        };
        getActionResponse({
            controller: "Account",
            action: "ResetPassword",
            properties: oModel,
            successFunction: function (data) {
                $("#ResetPasswordSet").slideUp("normal", function () {
                    $("#ResetPasswordSet").empty().append(data).slideDown("normal");
                });
            }
        });
    }
}
// -----------------------------------------------------------------------------------
function changePW(sUserName) {
    if ($("#changepw-form").valid()) {
        var oModel = {
            'UserName': sUserName,
            'OldPassword': $("#OldPassword").val(),
            'NewPassword': $("#NewPassword").val(),
            'ConfirmPassword': $("#ConfirmPassword").val()
        };
        getActionResponse({
            controller: "Account",
            action: "ChangePassword",
            properties: oModel,
            successFunction: function (data) {
                $("#ChangePasswordSet").slideUp("normal", function () {
                    $("#ChangePasswordSet").empty().append(data).slideDown("normal");
                });
            }
        });
    }
}
// -----------------------------------------------------------------------------------
function prepareMDDEditor(sContainer) {
    var $con = $("#" + sContainer),
        $txt = $con.find("textarea"),
        sPreviewId = sContainer + "-preview";

    if ($con.hasClass("mdd-editor-prepared")) {
        return;
    }
    $con.addClass("mdd-editor-prepared");
    
    $txt.data("mdd-preview", sPreviewId);
    setTimeout(function() {
        $txt.focusin(function () {
            var $this = $(this),
                $tb = $con.find(".mdd_toolbar"),
                $sm = $('<small class="mdd_info">Für die Formatierung des Textes wird <a target="_blank" href="http://de.wikipedia.org/wiki/Markdown"><strong>Markdown</strong></a> unterstützt</small>'),
                $pv = $('<div id="' + sPreviewId + '" class="mdd_preview markdown-text"></div>');
            $this.addClass("mdd_editor");
            if ($tb.length === 0) {
                $con.append($sm).append($pv);
                setTimeout(function () {
                    $this.MarkdownDeep({
                        onLoad: function (editor) {
                        }
                    });
                }, 10);
            }
        })
        .focusout(function () {
            //return;
            var $this = $(this),
                $tb = $con.find(".mdd_toolbar"),
                $sm = $con.find(".mdd_info"),
                $pv = $con.find("#" + sPreviewId);
            setTimeout(function () {
                if (!$tb.is(":focus") && !$this.is(":focus")) {
                    $this.removeClass("mdd_editor");
                    $tb.remove();
                    $sm.remove();
                    $pv.remove();
                }
            }, 5000);
        });
    }, 10);
}
// -----------------------------------------------------------------------------------
function initTagCloud(container) {
    var jContainer = $("#" + container);
    getActionResponse({
        controller: "Representation",
        action: "GetTagCloudItems",
        properties: {},
        successFunction: function (data) {
            jContainer.jQCloud(data);
        }
    });
}
// -----------------------------------------------------------------------------------
function filterRepresentationSubnav(e) {

    var sF = dofilterRepresentationSubnav(e);
    
    if(sF.length != 0) {
        var jMap = $("#" + sF.replace(/f/, "map"));
        if (jMap.length > 0) {
            if (typeof setMapFilter === "function") {
                setMapFilter(jMap, false);
            }
        }
    } else {
        if (typeof removeMapFilter === "function") {
            removeMapFilter(false);
        }
    }
}
// -----------------------------------------------------------------------------------
function dofilterRepresentationSubnav(e) {
    var jE = $(e),
        jParent = jE.parent(),
        sG = "",
        sF = "";
    
    if (jE.data("filter-type") === "g") {
        sG = jE.data("filter");
        sF = $("#mainsubnav-rep-filter-federalstate").find(".selected").data("filter");
    } else {
        sF = jE.data("filter");
        sG = $("#mainsubnav-rep-filter-governmentallevel").find(".selected").data("filter");
    }
    
    jParent.find("a.selected").removeClass("selected");
    jE.addClass("selected");

    if (sG.length === 0 && sF.length === 0) {
        $("#no-representations").hide();
        $("#mainsubnav-rep-list a").fadeIn("slow");
    } else {        

        var sFilter = "";
        if (sG.length != 0 && sF.length != 0) {
            sFilter = "." + sG + "." + sF;
        } else {
            if (sG.length != 0) {
                sFilter = "." + sG;
            }
            if (sF.length != 0) {
                sFilter = "." + sF;
            }
        }
        
        $("#mainsubnav-rep-list a").not(sFilter).fadeOut("slow", function () {
            filterShowNoRepresentations();
            $("#mainsubnav-rep-list a" + sFilter).fadeIn("slow", function () {
                filterShowNoRepresentations();
            });
        });        
    }
    
    if (sF.length != 0 && $("#mainsubnav-rep-list a." + sF).length === 0) {
        sF = "";
    }
    $("#mainsubnav-rep-list").scrollTop(0);
    $('#mainsubnav-rep-list').perfectScrollbar('update');
    $.cookie("OpenAntrag-NavFilter-G", sG, { path: '/' });
    $.cookie("OpenAntrag-NavFilter-F", sF, { path: '/' });

    return sF;
}
// -----------------------------------------------------------------------------------
function filterShowNoRepresentations() {
    if ($("#mainsubnav-rep-list a:visible").length === 0) {
        $("#no-representations").show();
    } else {
        $("#no-representations").hide();
    }
}
// -----------------------------------------------------------------------------------
function showAbuseNotice(idProposal) {
    $("#abusenotice-wrapper-" + idProposal).slideToggle();
}
// -----------------------------------------------------------------------------------
function sendAbuseNotice(idProposal) {
    var jAbuseNotice = $("#abusenotice-" + idProposal),
        sAbuseNotice = jAbuseNotice.val();

    if (sAbuseNotice.length === 0) {
        alertEx("Bitte gib eine Begründung an", "warning");
        jAbuseNotice.focus();
        return;
    }

    var oProperties = {
        'idProposal': idProposal,
        'abuseNotice': sAbuseNotice
    };
    getActionResponse({
        controller: "Representation",
        action: "SendProposalAbuseNotice",
        properties: oProperties,
        successFunction: function (data) {
            alertEx("Deine Nachricht wurde versandt.<br><br>Der Antrag wird nun umgehend von der Fraktion und der Administration geprüft und im Fall tatsächlichen Missbrauchs stumm geschaltet.");
            $("#abusenotice-wrapper-" + idProposal).slideToggle(function () {
                $(this).remove();
                $("#abusenotice-link-" + idProposal).remove();
            });
        }
    });

}
// -----------------------------------------------------------------------------------
function switchButtonGroup(e) {
    var $this = $(e),
        $parent = $this.parent(),
        $actual = $parent.find("button.btn-selected");
    
    if ($this.hasClass("btn-selected")) {
        return false;
    } else {
        $actual.removeClass("btn-selected");
        $("#" + $actual.data("area")).fadeOut("fast", function () {
            $this.addClass("btn-selected");
            $("#" + $this.data("area")).fadeIn("fast");
        });
    }
    scrollToOffset($("body"), 500);
    return false;
}
// -----------------------------------------------------------------------------------
function initProposalList() {
    $("#proposallist-table tr").each(function () {
        var jA = $(this).find("td").first().find("a");
        $(this).click(function () {
            window.location.href = jA.attr("href");
        });
    });
    $("table#proposallist-table").tablesorter({
        sortList: [[1, 1]]
    });
    $("table#proposallist-table")
        .wholly()
        .on('wholly.mouseenter', 'td, th', function () {
            $(this).addClass('wholly-highlight');
        })
        .on('wholly.mouseleave', 'td, th', function () {
            $(this).removeClass('wholly-highlight');
        });
}
// -----------------------------------------------------------------------------------
function getProposalListTable(count) {
    var iCount = parseInt(count);
    $("#proposallist-wrapper").css("min-height", $("#proposallist-wrapper").height() + 'px');
    $("table#proposallist-table").fadeOut();
    getActionResponse({
        controller: "Home",
        action: "GetProposalListTable",
        properties: { "iCount": iCount },
        successFunction: function (data) {
            $("#proposallist-wrapper").empty().append(data);
            initProposalList();
            $("#proposallist-wrapper").css("min-height", "");            
        }
    });

}
// -----------------------------------------------------------------------------------
function disableScrollNav() {
    confirmEx(
        "Möchtest Du das Scroll-Menü deaktivieren?<br><br>" +
        "Wenn ja, kannst Du es jederzeit durch Löschen des Cookies '<em>OpenAntrag-DisableScrollMenu</em>' wieder aktivieren.",
        "Scroll-Menü deaktivieren", function() {
            $.cookie("OpenAntrag-DisableScrollMenu", true);
            window.location.reload();
        });
}