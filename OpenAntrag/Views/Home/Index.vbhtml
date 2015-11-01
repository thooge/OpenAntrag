@Imports OpenAntrag
@Code
    ViewData("Title") = "Start"
    
    Dim lst As List(Of Representation) = GlobalData.Representations.Items _
        .Where(Function(x) (x.Status And (Representations.StatusConjuction.Active + Representations.StatusConjuction.Ended)) > 0) _
        .OrderBy(Function(x) x.Level) _
        .ThenBy(Function(x) x.Key) _
        .ToList()
    
End Code

@Section Styles
    @Styles.Render("~/css/home")
End Section

@Section Scripts
    <script>
        var sInfoContent;
        $(document).ready(function () {
            sInfoContent = $("#mapInfo").html();
            $("#mapD > a").hover(
                function () {
                    $("#mapInfo").html($(this).find("img").attr("alt") + " (" + $(this).data("count") + ")");
                },
                function () {
                    $("#mapInfo").html(sInfoContent);
                }
            ).click(function () {
                setMapFilter($(this), true);
                scrollToOffset($("#fraktionen"), 500);
            });
            var sCookieMap = $.cookie("OpenAntrag-NavFilter-F");
            if (sCookieMap) {
                setMapFilter($("#" + sCookieMap.replace(/f/, "map")), false);
            }
        });        

        function setMapFilter(jE, doNav) { 
            var sFed = jE.data("federal"),
                sFedName = jE.find("img").attr("alt"),
                sRepCount = jE.data("count"),
                jR = $('<a id="deleteMapFilter" onclick="removeMapFilter(true);showAllMaps();"style="font-size:10px;" href="javascript:go();">Filter entfernen</a>');
            removeMapFilter(doNav);
            jE.css("opacity", "1");
            $("#mapInfo").empty().append(sFedName).append(" (" + sRepCount + ")").append("<br>").append(jR);
            sInfoContent = $("#mapInfo").html();
            //--
            $(".fraktionbox").not(".fs" + sFed).fadeOut("slow");
            $(".fraktionbox.fs" + sFed).fadeIn("slow");
            //--
            $("#pm-container .pm-item").not(".fs" + sFed).fadeOut("slow");
            $("#pm-container .pm-item.fs" + sFed).fadeIn("slow");
            $("#pm-fed").text(" für " + sFedName);
            $("#pm-fed-count").text($("#pm-container .pm-item.fs" + sFed).length);
            //--
            $("#rv-container a").not(".fs" + sFed).fadeOut("slow");
            $("#rv-container a.fs" + sFed).fadeIn("slow");
            $("#rv-fed").text(" in " + sFedName);
            $("#rv-fed-count").text($("#rv-container a.fs" + sFed).length);
            //--
            $.cookie("OpenAntrag-NavFilter-F", "f" + sFed);
            if (doNav === true) {
                dofilterRepresentationSubnav($("#filter-f" + sFed));
            }
        }
        function removeMapFilter(doNav) {
            $("#mapD > a").css("opacity", "");
            $("#mapInfo").html("");
            //--
            if (doNav === true) {
                dofilterRepresentationSubnav($("#filter-g0"));
                dofilterRepresentationSubnav($("#filter-f0"));
            }
        }
        function showAllMaps() {
            $(".fraktionbox").fadeIn("slow");
            //--
            $("#pm-container .pm-item").fadeIn("slow");
            $("#pm-fed").text("");
            $("#pm-fed-count").text($("#pm-container .pm-item").length);
            //--
            $("#rv-container a").fadeIn("slow");
            $("#rv-fed").text("");
            $("#rv-fed-count").text($("#rv-container a").length);
        }
    </script>
End Section

@Section Intro
    <img src="/Images/Content/banner-160x220.png" style="position: absolute; left: -1000px; top: -1000px; width:1px; height: 1px; opacity: 0;" />
    <img src="/Images/Content/banner-275x80.png" style="position: absolute; left: -1000px; top: -1000px; width: 1px; height: 1px; opacity: 0" />

    <p>Für Piraten ist Bürgerbeteiligung nicht nur ein Wort. Wir sind schon in vielen Parlamenten 
        vertreten und geben Dir hier die Möglichkeit, Dein Anliegen dort einzubringen.</p>
End Section

<div class="content container-fluid">
    <div class="row-fluid">
        <div id="intention" class="span2 box-head">
            <i class="icon-lightbulb"></i>
            <h2>Die&nbsp;<br />
                Idee</h2>
            <br />
        </div>
        <div class="span9 offset1">
            <p>Es gibt so wunderbare Ideen von wunderbaren Menschen, die es nie in irgendein Parlament 
            schaffen; wir stellen uns die Frage warum.</p>
            <p>Piraten sind landauf und landab angetreten, um den Menschen zu mehr Mitbestimmungsrecht 
            zu verhelfen. Dazu ist es notwendig, das Ohr ganz nah am Bürger und seinen Ideen zu haben.</p>
            <p>Nun gibt es in unserer repräsentativen Demokratie die Regel, dass nur Parlamentarier Anträge 
            in die Versammlung einbringen können. Aber wer sagt denn, dass wir deswegen nicht zuhören sollten?</p>
            <p>Wir nehmen das Wort <em>Volksvertreter</em> wörtlich und geben Dir mit dieser Website 
            die Möglichkeit, Deine Ideen in Dein Parlament zu bringen.
            </p>
        </div>
    </div>
</div>

<div class="content content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span6 box-head">
            <i class="icon-road"></i>
            <h2>Das&nbsp;<br />
                Verfahren</h2>
            <br />
            <p>Dreh- und Angelpunkt sind die einzelnen Fraktionen oder Einzelabgeordneten der Piratenpartei. 
                Sie dienen als Vermittler Deines Anliegens.</p>
            <p>Das grundlegende Prinzip ist recht einfach: Du bringst Dein Anliegen über diese Website ein. 
                Anschließend wird es von uns geprüft und zu einem Antrag ausgearbeitet. Dieser wird dann ins 
                Parlament oder einen Ausschuss eingebracht, bzw. eine Anfrage wird gestartet.</p>
            <p>Der Antragsprozess kann sich von Fraktion zu Fraktion ein wenig unterscheiden (je nach Gremium), 
                aber der Status eines Antrags ist immer transparent und nachvollziehbar.</p>
            <br />
        </div>
        <div class="span5 offset1 box">
            <img src="/Images/workflow-schema.png" alt="" />            
        </div>
    </div>
</div>

<div class="content container-fluid">
    <div class="row-fluid">
        <div id="rules" class="span3 box-head">
            <i class="icon-check"></i>
            <h2>Die&nbsp;<br />
                Regeln</h2>
            <br />
        </div>
        <div class="span8 offset1">
            <p>Natürlich braucht es ein paar Regeln, um der Idee eine Form zu geben, aber es sind nicht viele:</p>
            <ol>
                <li>Jeder Antrag wird geprüft. Wir behalten uns jedoch vor, nur solche Anträge weiterzuverfolgen, die unserem politischen Selbstverständnis entsprechen.</li>
                <li>Anträge werden grundsätzlich anonym behandelt, um die persönliche/ideologische Ebene außen vor zu lassen.</li>
                <li>Die Bearbeitung der Anträge erfolgt transparent auf dieser Seite. Eine Benachrichtigung des Antragsstellers erfolgt nicht.</li>
                <li>Alle Anträge sind öffentlich und können auch öffentlich kommentiert werden.</li>
            </ol>
        </div>
    </div>
</div>

<div id="fraktionen" class="content content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span4 box-head">
            <i class="icon-group"></i>
            <h2>Die&nbsp;<br />
                Parlamente</h2>
            <br />
            <p>
                In <strong>OpenAntrag</strong> sind aktuell <strong>@(lst.Count)</strong> Parlamente gelistet, 
                in denen Abgeordnete oder Fraktionen der Piraten Deine Anträge entgegennehmen oder dies getan haben.
            </p>
@*            <p>Wir können bzw. Du kannst in folgenden <strong>@(intActiveCount)&nbsp;Parlamenten</strong> Anträge einbringen, weil wir dort 
                Einzelabgeordnete oder eine Fraktion haben:</p>*@
        </div>
        <div class="span3">
            @Html.Partial("_FederalMap", GlobalData.FederalStates)
        </div>
        <div class="span3 offset1">
            <p>Deine Piratenfraktion oder Du als Einzelabgeordneter bist hier noch nicht vertreten?</p>
            <a class="btn btn-primary btn-small"
                href="javascript:go();" onclick="toggleContentInfo(this, 'fraktion-info'); return false;">Informationen zur Teilnahme</a>
        </div>
    </div>
</div>

<div id="fraktion-info" class="content content-info content-hide container-fluid">
    <div class="row-fluid">
        <div class="span6">
            <p>Dieses System steht allen Piratenabgeordneten oder Fraktionen mit Piratenbeteiligung 
                offen, um die Ideen der Bürger umzusetzen.</p>
            <p>Jedes Parlament bzw. deren Fraktion bzw. Einzelabgeordneter bekommt eine eigene Seite, die über das Schema 
                <span style="display: block; margin: 5px 0;"><strong><em>http://openantrag.de/&lt;Name des Landes, der Stadt oder des Kreises&gt;</em></strong></span>
                erreichbar ist.</p>
            <p>Die Bearbeitung der eingehenden Bürgeranträge findet entweder über diese Seite statt oder 
                Du nutzt die Schnittstellen (API), um <strong>OpenAntrag</strong> in Deine eigene Website einzubinden.</p>
            <p><a href="/schnittstellen" class="btn btn-small btn-inverse">Schnittstellenbeschreibungen</a></p>
        </div>
        <div class="span5 offset1">
            <p>
                Es braucht nicht viel, um Deine Fraktion oder Dich als einzelnen Abgeordneten an 
                <strong>OpenAntrag</strong> anzubinden. Alle Infos findest Du in der FAQ.
            </p>
            <p><a href="http://openantrag.de/faq#Fraktion" class="btn btn-small btn-inverse"><strong>http://openantrag.de/faq#Fraktion</strong></a></p>
        </div>
    </div>
</div>

@Code
    Dim intMid As Integer = CInt(Math.Round(lst.Count / 2))
    If intMid Mod 2 = 1 Then intMid += 1
    Dim lstOne = lst.Where(Function(r, index) (index + 1) <= intMid)
    Dim lstTwo = lst.Where(Function(r, index) (index + 1) > intMid)
End Code

<div class="content content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span6">
            @For Each r As Representation In lstOne
                Html.RenderPartial("_RepresentationBox", r)
            Next
        </div>
        <div class="span6">
            @For Each r As Representation In lstTwo
                Html.RenderPartial("_RepresentationBox", r)
            Next
        </div> 
    </div>
    <div class="row-fluid">
        <div><a class="btn btn-primary btn-small" href="/overview">Tabellarische Übersicht über alle Parlamente...</a></div>
    </div>  
</div>

@Code
    Dim lstRV As List(Of Representative) = GlobalData.Representations.GetRepresentatives
    lstRV = Tools.RandomizeList(Of Representative)(lstRV)
End Code
<div class="content container-fluid">
    <div class="row-fluid">
        <div id="represenatives" class="span4 box-head">
            <i class="icon-user"></i>
            <h2>Die&nbsp;<br />
                Abgeordneten</h2>
            <br />
        </div>
        <div class="span7 offset1">
            <p>
                Diese <span id="rv-fed-count">@(lstRV.Count)</span> Piraten <span id="rv-fed"></span> 
                und viele viele weitere, sowie die Kollegen anderer Parteien aus den hier 
                vertretenen Fraktionen, setzen sich für Deine Idee und Deinen Antrag ein.</p>
        </div>
    </div>
</div>

<div class="content container-fluid">
    <div class="row-fluid" style="margin-top: 0;">
        <div class="span12" id="rv-container">                    
            @For Each rv As Representative In lstRV
                Dim rp As Representation = GlobalData.Representations.Items.Where(Function(x) x.Key = rv.Key_Representation).FirstOrDefault()
                Dim stbTitle As New StringBuilder
                stbTitle.Append("<strong>").Append(rv.Name).Append("</strong>").Append("<br>")
                stbTitle.Append(rp.Name).Append("<br>")
                stbTitle.Append("<em>").Append(rp.FederalName).Append("</em>")
                                        
                @<a href="@rp.FullUrl" class="tt-std fs@(rp.FederalKey)"
                    title="@(stbTitle.ToString)" 
                    style="background-image: url('@rv.PortraitImage')"></a>                   
            Next           
        </div>
    </div>
</div>

@Code
    Dim lstP = GlobalData.piratenmandate.Items _
                .Where(Function(x) String.IsNullOrEmpty(x.OpenAntragKey) = True and x.MandateCount > 0) _
                .OrderBy(Function(x) x.Bundesland.Name) _
                .ThenBy(Function(x) x.GebietName).ToList()
End Code
<div class="content content-shaded container-fluid">
    <div class="row-fluid">
        <div id="possibilities" class="span4 box-head">
            <i class="icon-rocket"></i>
            <h2>Die&nbsp;<br />
                Möglichkeiten</h2>
            <br />
        </div>
        <div class="span7 offset1">
            <p>
                Auf <a href="http://kommunalpiraten.de">kommunalpiraten.de</a> trägt Michael Büker 
                alle Parlamente zusammen, in denen Mandatsträger der Piratenpartei Deutschland 
                vertreten sind. Viele davon nutzen bereits das Angebot von OpenAntrag. 
                Aktuell gibt es noch <strong><span id="pm-fed-count">@lstP.Count</span> 
                Möglichkeiten <span id="pm-fed"></span></strong>...
            </p>
        </div>
    </div>
</div>

<div class="content content-shaded container-fluid">
    <div class="row-fluid" style="margin-top: 0;">
        <div class="span12" id="pm-container">                    
            @For Each p In lstP
                Dim stbTitle As New StringBuilder
                stbTitle.Append("<strong>").Append(p.GebietType).Append("</strong>").Append(" ").Append("<em>").Append(p.Bundesland.Name).Append("</em>").Append("<br>")
                stbTitle.Append(p.MandateCount).Append(" von ").Append(p.ParlamentSeats).Append(" Sitzen")
                If String.IsNullOrEmpty(p.FraktionName) = False Then
                    stbTitle.Append("<br>")
                    stbTitle.Append(p.FraktionText)
                End If
                    
                @If String.IsNullOrEmpty(p.GebietLocalpirates) = false Then
                    @<a class="tt-std pm-item fs@(p.Bundesland.Key)" 
                        href="@p.GebietLocalpirates" 
                        title="@(stbTitle.ToString)">
                        @p.GebietTypeAndName
                        </a>
                Else
                    @<div class="tt-std pm-item fs@(p.Bundesland.Key)" 
                            title="@(stbTitle.ToString)">
                        @p.GebietTypeAndName
                        </div>
                End If                                
            Next             
        </div>
    </div>
</div>

