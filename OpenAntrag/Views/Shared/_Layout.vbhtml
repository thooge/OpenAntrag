@Imports OpenAntrag

<!DOCTYPE html>
<html lang="en">
    <head>
        @Html.Partial("AsciiArtPartial")
        <meta charset="utf-8" />
        <title>OpenAntrag - @ViewData("Title")</title>
        
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0">

        <!-- http://realfavicongenerator.net -->
        <link rel="shortcut icon" href="/Images/Logos/favicon.ico">
        <link rel="apple-touch-icon" sizes="57x57" href="/Images/Logos/apple-touch-icon-57x57.png">
        <link rel="apple-touch-icon" sizes="114x114" href="/Images/Logos/apple-touch-icon-114x114.png">
        <link rel="apple-touch-icon" sizes="72x72" href="/Images/Logos/apple-touch-icon-72x72.png">
        <link rel="apple-touch-icon" sizes="144x144" href="/Images/Logos/apple-touch-icon-144x144.png">
        <link rel="apple-touch-icon" sizes="60x60" href="/Images/Logos/apple-touch-icon-60x60.png">
        <link rel="apple-touch-icon" sizes="120x120" href="/Images/Logos/apple-touch-icon-120x120.png">
        <link rel="apple-touch-icon" sizes="76x76" href="/Images/Logos/apple-touch-icon-76x76.png">
        <link rel="apple-touch-icon" sizes="152x152" href="/Images/Logos/apple-touch-icon-152x152.png">
        <link rel="icon" type="image/png" href="/Images/Logos/favicon-196x196.png" sizes="196x196">
        <link rel="icon" type="image/png" href="/Images/Logos/favicon-160x160.png" sizes="160x160">
        <link rel="icon" type="image/png" href="/Images/Logos/favicon-96x96.png" sizes="96x96">
        <link rel="icon" type="image/png" href="/Images/Logos/favicon-16x16.png" sizes="16x16">
        <link rel="icon" type="image/png" href="/Images/Logos/favicon-32x32.png" sizes="32x32">
        <meta name="msapplication-TileColor" content="#ff8800">
        <meta name="msapplication-TileImage" content="/Images/Logos/mstile-144x144.png">
        <meta name="msapplication-config" content="/Images/Logos/browserconfig.xml">

        <link rel="alternate" type="application/rss+xml" title="OpenAntrag-Feed: alle Bürgeranträge" href="http://@(HttpContext.Current.Request.Url.Authority)/feed" />
        @RenderSection("links", required:=False)        

        @Styles.Render("~/css/plugins") 
        <link href="/Fonts/Fontello/css/fontello-openantrag.css" rel="stylesheet"/>
        <link href="/Fonts/Flaticon/flaticon.css" rel="stylesheet" />
        @Styles.Render("~/css/styles-v2")
        <link href="/allrepresentationstyle.css" rel="stylesheet"/>
        @RenderSection("styles", required:=False)

        @Scripts.Render("~/bundle/modernizr")
    </head>

    <body>
        <div id="wrapper">

            <header>
                <div id="topbar">
                    @If Tools.IsAdmin Then
                        @<a href="javascript:go();" onclick="showNewPost();">Neue Meldung</a>
                    End If
                </div>

                <div id="logobar">
                    <div class="left">
                        <a href="/"><img src="/Images/Logos/OALogo50.png" style="padding: 12px" /></a>
                    </div>
                    <div class="main">                    
                        <h1><a href="/">OpenAntrag</a></h1>                    
                        @*<h2><a id="headnav-toggle" href="#">DE<b class="caret" style="border-width:6px;"></b></a></h2>*@
                        <div id="notifications">
    @Code
        Dim lstNF As List(Of Notification) = Notifications.GetItemsPage(-1, 1, 10)
    
        If lstNF.Count > 0 Then
            @<a id="notification-throbber" href="/mitteilungen"><i class="icon-right-open"></i></a>
        End If
    
        @<div id="notification-box">
            @For Each nf As Notification In lstNF
                @<a id="@(nf.Id)" data-time="@(nf.CreatedAt)" href="@(nf.Url)">
                    <small></small>
                    <span>@(nf.Title)</span>
                </a>
            Next
        </div>
    End Code                    
                        </div>
                    </div>
                </div>

                <div id="headnav" style="display: none;">                
                    <div id="country-headnav">
                        <a class="selected" href="http://openantrag.de">Deutschland</a>
                        <a href="http://openantrag.net">Luxemburg</a>
                    </div>                
                </div>

                <div id="intro">
                    <h2>Bürgeranträge<br />Dein Anliegen : Unser Antrag</h2>
                    @RenderSection("Intro", required:=True)
                </div>
            </header>

            <nav>
@*                <div class="nav" id="navlogo">
                    <img src="/Images/Logos/OALogo50.png">
                    <a class="caption" href="/">OpenAntrag</a>
                    <a class="close" href="#" onclick="disableScrollNav(); return false;">
                        <i class="icon icon-cancel"></i>
                    </a>
                </div>*@

                <div class="nav" id="mainnav">
                    <div class="nav-left">
                        @Html.Partial("_NavCommonLeftPartial")                        
                    </div>
                    <div class="nav-right">
                        @Html.Partial("_NavCommonRightPartial")
                    </div>
                </div>            

                <div class="subnav" id="mainsubnav">
                    <div id="mainsubnav-container" style="display: none;"></div>
                </div>

                <div class="subnav" id="mainsubnav-rep">
                    @Html.Partial("_SubnavRepresentations")
                </div>

                @RenderSection("RepNav", required:=False)
            </nav>

            <div id="body">
                @RenderBody()
            </div>

        </div>

        @RenderSection("PreFooter", required:=False)

        <div id="more">
            <div id="more-legal" class="content content-info content-hide container-fluid">
                <div class="row-fluid">
                    <div class="span6" style="font-size: 0.9em; line-height: 14px;">
                        <h4>Impressum</h4>

                        <p><strong>Design, Entwicklung und Betrieb</strong></p>
                            <p>Kristof Zerbe<br>
                            Schöne Aussicht 2b<br>
                            65193 Wiesbaden<br>
                            <a href="mailto:kristof@openantrag.net">kristof@openantrag.net</a>
                        </p>
                        <p>Mitglied der Piratenpartei, Landesverband Hessen<br />
                            <a href="https://wiki.piratenpartei.de/Benutzer:Kiko">https://wiki.piratenpartei.de/Benutzer:Kiko</a>
                        </p>

                        <br /><br />
                        <h4>Rechtliche Hinweise</h4>

                        <p><strong>Haftung für Inhalte</strong></p>
                        <p>Die Inhalte dieser Seiten wurden mit größter Sorgfalt erstellt. 
                        Für die Richtigkeit, Vollständigkeit und Aktualität der Dienste 
                        kann jedoch keine Gewähr übernehmen werden.</p>
                        
                        <p>Als Diensteanbieter ist der Betreiber gemäß § 7 Abs.1 TMG für eigene 
                        Inhalte auf diesen Seiten nach den allgemeinen Gesetzen verantwortlich. 
                        Nach §§ 8 bis 10 TMG ist er als Diensteanbieter jedoch nicht 
                        verpflichtet, übermittelte oder gespeicherte fremde Informationen zu 
                        überwachen oder nach Umständen zu forschen, die auf eine rechtswidrige 
                        Tätigkeit hinweisen. Verpflichtungen zur Entfernung oder Sperrung der 
                        Nutzung von Informationen nach den allgemeinen Gesetzen bleiben hiervon 
                        unberührt. Eine diesbezügliche Haftung ist jedoch erst ab dem 
                        Zeitpunkt der Kenntnis einer konkreten Rechtsverletzung möglich. Bei 
                        Bekanntwerden von entsprechenden Rechtsverletzungen werden diese Inhalte 
                        umgehend entfernen.</p>
                    </div>
                    <div class="span6" style="font-size: 0.9em; line-height: 14px;">
                        <p><strong>Haftung für Links</strong></p>
                        <p>Dieses Angebot enthält Links zu externen Webseiten Dritter, auf deren 
                        Inhalte der Betreiber keinen Einfluss hat. Deshalb wird für diese 
                        fremden Inhalte auch keine Gewähr übernommen. Für die Inhalte 
                        der verlinkten Seiten ist stets der jeweilige Anbieter oder Betreiber der 
                        Seiten verantwortlich. Eine permanente inhaltliche Kontrolle der verlinkten 
                        Seiten ist jedoch ohne konkrete Anhaltspunkte einer Rechtsverletzung nicht 
                        zumutbar. Bei Bekanntwerden von Rechtsverletzungen werden derartige Links 
                        umgehend entfernen.</p>

                        <p><strong>Datenschutz</strong></p>
                        <p>Die Nutzung dieser Webseite ist in der Regel ohne Angabe personenbezogener Daten möglich. 
                        Soweit personenbezogene Daten (beispielsweise Name oder eMail-Adressen) erhoben werden, 
                        erfolgt dies stets auf freiwilliger Basis. Diese Daten werden nicht an Dritte weitergegeben.</p>
                        
                        <p>Es findet keine Speicherung von Bewegungsdaten (IP-Adresse, Browser-Kennung, o.ä.) statt.</p>
                        
                        <p>Der Betreiber weist darauf hin, dass die Datenübertragung im Internet Sicherheitslücken aufweisen kann. 
                        Ein lückenloser Schutz der Daten vor dem Zugriff durch Dritte ist nicht möglich.</p>
                        
                        <p>Der Nutzung von im Rahmen der Impressumspflicht veröffentlichten Kontaktdaten 
                        durch Dritte zur Übersendung von nicht ausdrücklich angeforderter 
                        Werbung und Informationsmaterialien wird hiermit ausdrücklich widersprochen. 
                        Die Betreiber der Seiten behält sich ausdrücklich rechtliche Schritte 
                        im Falle der unverlangten Zusendung von Werbeinformationen, etwa durch Spam-Mails, 
                        vor.</p>
                        
                        <p><i>Quelle: <a target="_blank" href="http://www.e-recht24.de/muster-disclaimer.htm">eRecht24 Disclaimer</a></i></p>
                    </div>
                </div>
            </div>

            @If HttpContext.Current.User.Identity.IsAuthenticated = False Then
                @<div id="more-login" class="content content-info content-hide container-fluid">
                    <div class="row-fluid">
                        <div class="span12">
                            <div id="logon-wrapper" style="float:right;">
                                @Using Html.BeginForm("Logon", "Account", FormMethod.Post, New With {.id = "logon-form", .name = "logon-form"})
                                    @Html.Partial("_LogonPartial", New LogonModel)
                                End Using
                            </div>
                            <div id="resetpw-wrapper" style="float: right; display:none;">
                                @Using Html.BeginForm("ResetPassword", "Account", FormMethod.Post, New With {.id = "resetpw-form", .name = "resetpw-form"})
                                    @Html.Partial("_PasswordResetPartial", New ResetPasswordModel)
                                End Using
                            </div>
                        </div>
                    </div>
                </div>
            Else
                @<div id="more-account" class="content content-info content-hide container-fluid">
                    <div class="row-fluid">
                        <div class="span12">
                            <div style="float: right; margin-left: 60px;">
                                <p>Du bist als <strong>@HttpContext.Current.User.Identity.Name</strong> angemeldet</p>
                                @Using Html.BeginForm("Logoff", "Account", FormMethod.Post, New With {.id = "logoff-form", .name = "logoff-form"})
                                    @<input type="submit" class="btn btn-small btn-inverse" value="Abmelden" />
                                End Using
                            </div>
                            <div style="float: right;">
                                @Using Html.BeginForm("ChangePassword", "Account", FormMethod.Post, New With {.id = "changepw-form", .name = "changepw-form"})
                                    @Html.Partial("_PasswordChangePartial", New ChangePasswordModel)
                                End Using
                            </div>
                        </div>
                    </div>
                </div>
            End If
        </div>

        <footer>
            <div class="left">
                <a href="/"><img src="/Images/Logos/OALogo50.png" style="padding: 12px" /></a>
            </div>
            <div class="right"></div>
            <div class="main">
                <div id="footer-commands">
                    <a id="footer-command-legal" style="float:left;" rel="more-legal" href="javascript:go();">
                        <i class="icon-keyboard"></i><br />
                        Impressum & Co.
                    </a>
                    @If HttpContext.Current.User.Identity.IsAuthenticated = False Then
                        @<a id="footer-command-login" style="float:right;" rel="more-login" href="javascript:go();">
                            <i class="icon-key"></i><br />
                            Login
                         </a>
                    Else
                        @<a id="footer-command-account" style="float:right;" rel="more-account" href="javascript:go();">
                            <i class="icon-user"></i><br />
                            [@HttpContext.Current.User.Identity.Name]
                         </a>
                    End If
                </div>
            </div>
        </footer>

        <a id="backtop" href="javascript:go();" onclick="$.scrollTo(0, 500);"><i class="icon-up-circled"></i></a>

        @Scripts.Render("~/bundle/jquery")
        @Scripts.Render("~/bundle/jqueryval")
        @Scripts.Render("~/bundle/plugins-pre")
        @Scripts.Render("~/bundle/plugins")
        @Scripts.Render("~/bundle/main")
        @If Tools.IsAdmin Then 
            @Scripts.Render("~/bundle/admin")
        End If
        @RenderSection("scripts", required:=False)

        <script type="text/javascript">
            initLayoutPage();
        </script>

        @If TempData("ExceptionAlert") IsNot Nothing Then
            Dim exc As CustomException = CType(TempData("ExceptionAlert"), CustomException)
            @<script type="text/javascript">
                 $(document).ready(function () {
                     alertEx("@(exc.Message)", "error", "@(exc.Title)");
                 });
            </script>
        End If

    </body>
</html>
