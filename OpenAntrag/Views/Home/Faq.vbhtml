@Imports OpenAntrag

@Code
    ViewData("Title") = "FAQ"
End Code

@Section Styles
    @Styles.Render("~/css/faq")
End Section

@Section Scripts
    <script>
        $(document).ready(function () {
            var $toc = $("#toc");
            $(".content-faq").each(function () {
                var $this = $(this),
                    sTitle = $this.find("div > div > h3").text(),
                    $item = $('<li><a href="javascript:go();">' + sTitle + '</a></li>')
                $item.find("a").click(function () {
                    scrollToOffset($this, 500);
                });
                $toc.append($item);
            });
            //--
            $("a.hash").each(function () {
                var sHash = $(this).text();
                $(this).prop("href", "javascript:go();").click(function () {
                    goHash(sHash);
                    return false;
                });
            });
        });
    </script>
End Section

@Section Intro
    <p>Fragen über Fragen ...<br />
        Hier gibts ein paar Antworten</p>
End Section

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span2 box-head">
            <i class="icon-help-circled"></i>
            <h2>Die<br />
                FAQ</h2>
            <br />
        </div>
        <div class="span9 offset1 box">
            <p>FAQ - "Frequently Asked Questions" ist traditionell der Bereich einer Website
                wo häufig gestellte Fragen beantwortet werden. So auch hier. Interessant auch 
                für Fraktionsadministratoren... ;)
            </p>
            <p>Dein drängende Frage wurde noch nicht gestellt? Dann nutze das 
                <a href="/feedback">Feedback</a> und es wird Dir geholfen.</p>

            <ul id="toc" class="tight" style="margin: 20px 0 0 15px"></ul>
        </div>
    </div>
</div>

@Code
    Dim bolShaded As Boolean = True
End Code

@*
    <a name="XXX"></a>
    <div class="content content-faq @IIf(bolshaded = True, "content-shaded", "") container-fluid">
        <div class="row-fluid">
            <div class="span12">
                <small></small>
                <h3></h3>
                <p></p>
            </div>
        </div>
    </div>
    @Code bolShaded = Not bolShaded End Code
*@

<a name="Fraktion"></a>
<div class="content content-faq @IIf(bolshaded = True, "content-shaded", "") container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>für Fraktionen/Einzelabgeordnete</small>
            <h3>Wie bekomme ich für meine <a class="hash">Fraktion</a> eine eigene OpenAntrag-Seite?</h3>
            <p>Stell zunächst sicher, dass noch kein Kollege schneller war. War dies keiner, 
                dann schicke an die Mail-Adresse <a href="mailto:@("fraktion-registrieren@" & Tools.GetRequestDomain)">@("fraktion-registrieren@" & Tools.GetRequestDomain)</a> 
                eine Mail mit folgenden Informationen...
            </p>
            <div>
                <a style="float:right; margin: 10px 0;" class="btn btn-medium btn-primary" href="~/Downloads/OpenAntrag-FraktionRegistrieren.xls">
                    <i class="flaticon-excel2"></i>
                    <strong>Download</strong> Excel-Datei zum einfachen Ausfüllen
                </a>
            </div>
            <h4>...zum Parlament</h4>
            <ul class="tight">
                <li>Name des Parlaments</li>
                <li>Liste aller Ausschüsse (Name und Link, <em>falls vorhanden</em>)</li>
                <li>Bild der Region (JPG-Datei, mind. 2100 Pixel breit; <em>falls vorhanden</em>)</li>
            </ul>
            <h4>...zur Fraktion</h4>
            <ul class="tight">
                <li>Name der Fraktion / des Abgeordneten</li>
                <li>Beschreibung der Fraktion (Fließtext; Historie, Wissenswertes, ...)</li>
                <li>zentrale Mail-Adresse</li>
                <li>Mail-Adresse für Hinweisnachrichten</li>
                <li>zentrale Telefonnummer (<em>falls vorhanden</em>)</li>
                <li>Twitter-Account (<em>falls vorhanden</em>)</li>                
                <li>Logo der Fraktion (PNG-Datei; <em>falls vorhanden</em>)</li>
            </ul>
            <h4>...zu den einzenen Abgeordneten</h4>
            <ul class="tight">
                <li>Vorname, Nachname</li>
                <li>Mail-Adresse</li>
                <li>Twitter-Account (<em>falls vorhanden</em>)</li>
                <li>Telefonnummer (<em>falls gewünscht</em>)</li>
                <li>Foto (JPG-Datei, 100 x 125 Pixel)</li>
            </ul>
            <h4>...zum Antragsprozess</h4>
            <p>
                Beschreibung des Antragsprozesses des Parlaments in einzelnen Schritten 
                mit jeweiligen Verknüpfungen zu den nächsten möglichen Schritten.
            </p>
            <p><strong>Beispiel:</strong></p>
            <pre>
1. Eingang des Antrags
   > 2   
2. Prüfung des Antrags
   > 3
   > 4
3. Behandlung in der Fraktionssitzung
   > 4
   > 5
4. Keine Übernahme durch die Fraktion
5. Übernahme durch Fraktionsmitglied
   > 6
   > 7
   > 8
6. Antrag in Ausschuss
   > 6
   > 7  
7. Antrag im Parlament
   > 6
   > 8
   > 9   
8. Antrag angenommen
9. Antrag abgelehnt
            </pre>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<a name="webcast-administration"></a>
<div class="content content-faq @IIf(bolshaded = True, "content-shaded", "") container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>für Fraktionen/Einzelabgeordnete</small>
            <h3>Wie administriere ich die Anträge (Webcast)</h3>
            <p>Im folgenden Webcast (14 Minuten) wird die Bearbeitung eines Antrags erläutert:</p>
            <video controls="controls">
                <source src="~/Video/OpenAntrag-Administration.mp4" type="video/mp4">
                <source src="~/Video/OpenAntrag-Administration.webm" type="video/webm">
                <source src="~/Video/OpenAntrag-Administration.ogv" type="video/ogg">
                <div>
                    Uups - hier käme ein Video, wenn Dein Browser HTML 5-Unterstützung 
                    hätte, wie z.B. der aktuelle Firefox oder Chrome
                </div>
            </video>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<a name="informieren"></a>
<div class="content content-faq @IIf(bolshaded = True, "content-shaded", "") container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>für Alle</small>
            <h3>Wie kann ich mich über neue Fraktionen oder Anträge <a class="hash">informieren</a>?</h3>
            <p>OpenAntrag nutzt diverse Kanäle, um Informationen zu verbreiten.</p>

            <h4>RSS-Feed</h4>
            <p>
                OpenAntrag bietet zwei klassische <a href="http://de.wikipedia.org/wiki/RSS">RSS-Feeds</a>. Der allgemeine Feed, der über die Adresse 
                <a href="http://openantrag.de/feed">http://openantrag.de/feed</a> abonniert werden kann, 
                beinhaltet alle eingegangenen Anträge. Jede Parlaments- bzw. Fraktionsseite bietet zudem
                einen eigenen Feed, nur mit den Anträgen die dort eingegangen sind: <strong>http://openantrag.de/&lt;<em>Schlüssel des Parlaments (Fraktion)</em>&gt;/feed</strong>.
            </p>
            <p>
                RSS-Feeds lassen sich mit vielen Programmen verarbeiten und lesen. Ein recht bekanntes Exemplar ist 
                zum Beispiel der Online-Feed-Reader <a href="feedly.com">feedly.com</a>, das aus dem Google Reader hervorgegangen ist. 
                Eine recht gute Übersicht über die verfügbaren Programme bietet <a href="http://www.about-rss.de/">about-rss.de</a>.
            </p>

            <h4>Twitter</h4>
            <p>
                Jeder neue Antrag wird automatisch auf dem Twitter-Konto <a href="https://twitter.com/OpenAntrag">@@OpenAntrag</a>
                veröffentlicht. Daneben gibt es dort auch immer wieder manuelle Tweets, wenn zum Beispiel eine neue 
                Fraktion an dem System teilnimmt. Es lohnt sich also dem Account zu folgen...
            </p>

            <h4>Pushbullet</h4>
            <p>
                Pushbullet ist ein sog. Notification Service (Hinweisdienst) für Smartphones (Andropid, iOS, Windows) und
                Browser (Chrome, Firefox).
            </p>
            <p>
                Mit Einführung der abonnierbaren Pushbullet-Channels gibt es auch einen 
                <a href="http://pushbullet.com/channel?tag=openantrag">OpenAntrag-Channel</a>, 
                über den alle Mitteilungen, neue Anträge, Erfolgsgeschichten und Feedbacks gepostet werden: 
                <a href="http://pushbullet.com/channel?tag=openantrag">http://pushbullet.com/channel?tag=openantrag</a>
            </p>

            <h4>Google+</h4>
            <p>
                Es gibt eine <a href="https://plus.google.com/communities/116610228747484665783">OpenAntrag-Community auf Google+</a>
                auf der neben <strong>neuen Fraktionen</strong> auch <strong>neue Features</strong> angekündigt werden. 
                Sie dient auch zur Informationssammlung von Presseberichten und anderem Wissenswertem über OpenAntrag.
            </p>

            <h4>API</h4>
            <p>
                Neben den oben genannten, recht bequemen Wegen, zur Information, gibt es auch noch die API (Application Programming Interface) 
                für die Techies unter uns: <a href="http://openantrag.de/api">http://openantrag.de/api</a> Mit den verschiedenen Methoden 
                der Schnittstellen lassen sich recht unkompliziert neue Informationsangebote kreieren, wenn man damit umzugehen weiß.
            </p>

        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<a name="Antragsteller"></a>
<div class="content content-faq @IIf(bolshaded = True, "content-shaded", "") container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>für Alle</small>
            <h3>Warum kann der <a class="hash">Antragsteller</a> seinen Namen und seine Kontaktdaten nicht angeben?</h3>
            <p>Jeder Piratenabgeordneter wird das schon mal erlebt haben: man bringt einen Antrag ins Parlament ein und er 
                wird von den Etablierten nur abgelehnt, weil er von den Piraten kommt. Man merkt das meist daran, dass das 
                Thema ein oder zwei Jahre später fast wortgleich von einer Fraktion eingebracht wird, die den eigenen zuvor 
                abgelehnt hat.
            </p>
            <p>So, und nun stelle man sich vor, alle Anträge kämen anonymisiert ins Parlament, sprich die Abgeordneten müssten 
                sich nur mit dem Thema auseinandersetzen..!? Ergebnis: Viele gute Dinge würden wesentlich schneller umgesetzt 
                und unser Land hätte weniger Probleme. 
            </p>
            <p>Wir Piraten können nun nicht umhin zuzugeben, dass auch wir der Versuchung erliegen können, die 
                persönliche und/oder ideologische Ebene in unsere Entscheidungen mit einfließen zu lassen. Dieser 
                Erfahrungen haben wir in unserem Pilotprojekt in der Stadtverordnetenfraktion "Linke & Piraten Wiesbaden" 
                bereits gemacht und uns deswegen dazu entschlossen uns auf das eigentliche Thema zu konzentrieren, auch wenn 
                es manchmal schade ist, den Antragssteller nicht direkt kontaktieren zu können, wenn zum Beispiel etwas unklar ist.
                Die Kommentarfunktion schafft da allerdings Abhilfe, denn wenn ein Antragsteller wirklich an der Umsetzung 
                interessiert ist, wird er die Seite erneut besuchen, den Rückfragekommentar lesen und antworten.
            </p>
            <p>Inzwischen ist es jeder Fraktion möglich einzustellen, ob unter dem Antragstext ein Feld zur freiwilligen 
                Angabe von Kontaktinformationen eingblendet wird. Diese Daten werden allerdings nur in die Hinweisnachricht 
                an die Fraktion übernommen und nicht auf der OpenAntrag-Server gespeichert.
            </p>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<a name="Missbrauch"></a>
<div class="content content-faq @IIf(bolshaded = True, "content-shaded", "") container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>für Alle</small>
            <h3>Was ist "<a class="hash">Missbrauch</a> melden" und was passiert danach?</h3>
            <p>
                Die Plattform ist für jeden und alles offen, soll aber nicht dazu dienen, sie für 
                Denunziationen, Beleidigungen oder Ähnliches zu missbrauchen. 
                Daher gibt es unter jedem Antrag den Link <em>Missbrauch melden</em> und 
                <strong>jeder ist aufgerufen Anträge darüber zu melden, die 
                widerrechtlich sind!</strong>
            </p>
            <p>
                Klickt man auf den Link, so öffnet sich ein Feld zur Angabe der Begründung. 
                Diese Begründung wird an die Fraktion und die Administration geschickt, die den 
                Antrag dann <strong>stumm schalten</strong> kann. Dazu gibt es im Administrationsmodus 
                eines Antrags einen entsprechenden Button, der wiederum eine Begründung einfordert, 
                die anschließend anstelle des ursprünglichen Antragstextes für alle sichtbar gemacht wird.
            </p>
            <p>
                Ist ein Antrag stumm geschaltet, hat die Fraktion die Möglichkeit den Antragstext zu 
                "schwärzen", also einzelne Passagen durch "XXX" oder Ähnliches zu ersetzen, um zum 
                Beispiel persönliche Angaben herauszunehmen. Danach kann sie den Antrag wieder "scharf" 
                schalten, wobei die Fraktionsbegründung gespeichert bleibt.
            </p>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<a name="Projekt"></a>
<div class="content content-faq @IIf(bolshaded = True, "content-shaded", "") container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>für Alle</small>
            <h3>Warum ist OpenAntrag ein privates <a class="hash">Projekt</a>?</h3>
            <p>Bei den Piraten in Hessen ist ein Bürgerportal schon seit fast zwei Jahren immer wieder in der 
                Diskussion, auch weil die Stadtverordentenfraktion "Linke & Piraten Wiesbaden" mit dem Pilotprojekt 
                "Bürgeranträge" zumindest einen Teil in der Umsetzung hatte. Bis auf ein Anforderungsprofil, welches 
                fast so dick wie Tolstois "Krieg und Frieden" wurde, ist aber nicht viel passiert. 
            </p>
            <p>Einige andere hessische Fraktionen wollten nun aber ebenfalls Bürgeranträge anbieten und in das 
                Pilotprojekt einsteigen. Da Portierungen auf unterschiedlichste Web-Systeme resourcenintensiv und 
                mit Reibungsverlusten behaftet sind, wurde die Idee eines bundesweiten, gemeinsamen Portals geboren.
                Da ich (Kristof Zerbe) eher zu Abteilung "Machen statt Labern" gehöre, mein Engagement für die Piratenpartei 
                seit 2009 ungebrochen ist (Kreisvorstand, Landesvorstand, PG's, AG's, et cetera pp) und schlicht gerne moderne 
                Webseiten entwickele, habe ich mich an die Tastatur gesetzt...
            </p>
            <p>Das OpenAntrag einen solchen Zuspruch erhält, habe ich nicht erwartet und mir ist durchaus bewußt, dass es 
                den Abgeordneten und Bürgern ein wenig Vertrauen in meine Person abverlangt, auch wenn ich aktuell die Kosten 
                und die Verantwortung trage. In naher Zukunft möchte ich allerdings das Projekt unter die Ägide der Partei 
                bringen, da es dort meiner Meinung nach hingehört und die Akzeptanz erhöht. Als Gegenleistung erwarte ich 
                dafür: Nichts, denn ich bin Idealist und möchte mit meiner Arbeit für die Piraten dazu beitragen etwas zu 
                verbessern.
            </p>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<a name="Code"></a>
<div class="content content-faq @IIf(bolshaded = True, "content-shaded", "") container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>für Alle</small>
            <h3>Wie wurde OpenAntrag umgesetzt, ist der <a class="hash">Code</a> verfügbar und wo kann man ihn herunterladen?</h3>
            <p>OpenAntrag ist ein ASP.NET MVC-Projekt und läuft auf einem Internet Information Server (IIS) 7.5 unter 
                Windows Server 2008 R2.<br />Die Entscheidung das .NET-Framework einzusetzen hat einen simplen Hintergrund: 
                der Entwickler beherrscht es am besten ;)
            </p>
            <p>Die wichtigsten Standards und Tools:
                <ul class="tight">
                    <li>HTML5 und CSS3</li>
                    <li><a href="http://www.asp.net/mvc">ASP.NET MVC 5</a></li>
                    <li><a href="http://www.asp.net/web-api">ASP.NET Web API 2.1</a></li>                    
                    <li><a href="http://ravendb.net/">RavenDB</a> - NoSQL-Datenbank</li>
                    <li><a href="http://flatstrap.org/">Flatstrap/Bootstrap</a> - Layout/Design-Unterstützung</li>
                    <li><a href="http//modernizr.com">Modernizr</a> - HTML5-Fallback-Unterstützung</li>
                    <li><a href="http://jquery.com">jQuery 2.x</a> - Javascript-Framework</li>
                    <li><a href="http://fontello.com">Fontello</a> - Icon-Font-Generator</li>
                </ul>
            </p>
            <p>In OpenAntrag findet keinerlei Logging oder Tracking statt. Die IIS-Standardlogs sind ausgeschaltet. 
                So werden auch keinerlei externe Services eingebunden, die einem Logging unterliegen könnten.</p>
            <p>Das Projekt wurde auf <a href="https://github.com/kristofzerbe/OpenAntrag">https://github.com/kristofzerbe/OpenAntrag</a> veröffentlicht.</p>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<a name="Antragstext"></a>
<div class="content content-faq @IIf(bolshaded = True, "content-shaded", "") container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>für Administratoren</small>
            <h3>Kann ich den <a class="hash">Antragstext</a> im Nachhinein verändern?</h3>
            <p>Klare Antwort: Jain. Stellt ein Bürger einen Antrag ein, wäre es natürlich fatal, 
                wenn diese "Willensbekundung" manipuliert werden könnte.</p>
            <p>Bist Du allerdings als Administrator angemeldet und stellst selbst einen Antrag ein, 
                um zum Beispiel ältere Anträge nachzupflegen, wird Dein Benutzername im Antrag 
                vermerkt, um Dich in die Lage zu versetzen Deinen eigenen Text auch später noch 
                ändern zu können.
            </p>
            <p>Weiterhin haben die Fraktionen die Möglichkeit Anträge zum Beispiel aus 
                Datenschutzgründen stummzuschalten, wenn jemand schützenswerte Informationen 
                in seinen Antrag geschrieben hat, und den Antragstext dann entsprechend zu 
                verändern.
            </p>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<a name="Banner"></a>
<div class="content content-faq @IIf(bolshaded = True, "content-shaded", "") container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>für Webmaster</small>
            <img src="~/Images/Content/drupal-banner.png" style="float: right; margin: 0 0 20px 20px;" />
            <h3>Ich würde gerne ein OpenAntrag-<a class="hash">Banner</a> auf meine Seite stellen. Gibt es eins?</h3>            
            <p>Ja. <a href="http://wiki.piratenpartei.de/Benutzer:Nowrap">Ralf Praschak</a> hat ein dynamisches Banner gebaut, dass über die API 
                aktuelle Zahlen zieht und einblendet:
                <br />
                <ul><li><a href="http://jsfiddle.net/nowrap/KZDdt/">Fiddle des OpenAntrag-banners (160x213)</a></li></ul>                
            </p>
            <p>Daraus hat Ralf auch ein Drupal 6-Modul entwickelt:
                <br />
                <ul><li><a href="https://github.com/Piratenpartei-Drupal6/pp_openantrag_banner">pp_openantrag_banner auf GitHub</a></li></ul>
            </p>
            <br />
            <p>Individuelle, statische Banner findet man auf den jeweiligen Seiten der Parlamente (Fraktionen) unter:</p>
            <p>
                <strong>http://openantrag.de/&lt;<em>Schlüssel des Parlaments (Fraktion)</em>&gt;/banner</strong>
                <br />
                also z.B.: <a href="http://openantrag.de/wiesbaden/banner">http://openantrag.de/wiesbaden/banner</a>
            </p>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code
