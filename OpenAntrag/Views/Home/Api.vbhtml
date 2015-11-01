@Imports OpenAntrag
@Code
    ViewData("Title") = "API"
End Code

@Section Styles
    @Styles.Render("~/css/api")
End Section

@Section Scripts
    <script>
        $(document).ready(function () {
            buildToc();
        });

        function getApiResult(e, sUrl) {
            var $this = $(e),
                $container = $this.parents(".content-api"),
                dataType = "text",
                result = "";

            if (sUrl.endsWith('json')) { dataType = "json"; }
            //if (sUrl.endsWith('xml')) { dataType = "xml"; }
            $.ajax({
                type: "GET",
                url: sUrl,
                dataType: dataType,
                async: false,
                cache: false
            }).done(function (data) {
                if (sUrl.endsWith('json')) { result = prettyPrintJson(data); }
                //if (sUrl.endsWith('xml')) { result = prettyPrintXml(xmlToString(data)).replace(/\</g, "&lt;").replace(/\>/g, "&gt;"); }
                if (sUrl.endsWith('csv')) { result = data; }

                if ($this.parents(".api-info").next("div.result").length > 0) {
                    $this.parents(".api-info").next("div.result").slideUp(function () {
                        $(this).remove();
                        buildPre();
                    });
                } else { buildPre(); }

                function buildPre() {
                    var $wrap = $('<div class="result"></div>'),
                        $link = $('<h5>' + sUrl + '</h5>'),
                        $pre = $('<pre style="width:91%"></pre>').append(result);
                    $wrap.append($link).append($pre);
                    $this.parents(".api-info").after($wrap);
                    $wrap.slideDown(function () {
                        scrollToOffset($container, 500);
                    });
                }
            }).fail(function (jqXHR, textStatus, err) {
                alertEx(err);
            });
        }
        function postApi(e, sUrl) {
            var $this = $(e),
                $container = $this.parents(".content-api"),
                dto = JSON.parse($container.find("pre").html());
            $.post(sUrl, dto)
                .done(function (data) {
                    console.log(data);
                })
                .fail(function (jqXHR, textStatus, err) {
                    console.log(jqXHR.responseText);
                    alertEx(err);
                });
        }
        function buildToc() {
            var jToc = $("#toc");
            $(".content-api-wrapper").each(function() {
                var $root = $(this),
                    jTocElm = $('<a class="root" href="javascript:go();">' + $root.data("root-caption") + '</a>'),
                    jTocList = $('<ul></ul>');

                jTocElm.click(function () { scrollToOffset($root, 500) });
                jToc.append(jTocElm).append(jTocList);

                $(".content-api-" + $root.data("root")).each(function () {
                    var $method = $(this),
                        jTocSub = $("<li></li>"),
                        jTocSubElm = $('<a class="method" href="javascript:go();">' + $method.data("method") + '</a>');

                    jTocSub.click(function () { scrollToOffset($method, 500) });
                    jTocSub.append(jTocSubElm);
                    jTocList.append(jTocSub);
                    jToc.append(jTocList);
                });
            });
        }
    </script>
End Section

@Section Intro
    <p>Daten müssen fließen, um sie nutzbar zu machen.<br />
        Am besten über eine API wie diese, die die 
        OpenAntrag-Daten öffentlich zugänglich macht.
    </p>
End Section

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span4 box-head">
            <i class="icon-network"></i>
            <h2>Die&nbsp;<br />
                Schnittstellen</h2>
            <br />
            <br />
            <div id="toc"></div>
        </div>
        <div class="span7 offset1 box">
            <p>Alle Daten von <strong>OpenAntrag</strong>, bis auf die Kontodaten der Benutzer, 
                sind öffentlich zugänglich. Jeder kann sie maschinell einlesen und weiterverarbeiten</p>
            <p>Um Vandalismus und dergleichen zu verhindern, ist der schreibende Zugriff durch 
                einen Schlüssel (API-Key) gesichert, den die Fraktionen auf Anfrage (kurze Mail) erhalten.</p>
            <p>Alle Dienstmethoden wurden als HTTP-Services (REST) via Web API umgesetzt. Das Ausgabeformat 
                richtet sich nach dem im <code>Request</code> angegebenen <code>Content-Type</code>. 
                Unterstützt werden aktuell folgende Content-Types:
                <ul class="tight">
                    <li>application/json</li>
                    @*<li>application/xml</li>*@
                    <li>text/csv (nur Listen)</li>
                </ul>
            </p>
            <p>
                Alternativ kann auch der Url-Parameter <code>format</code> zur erzwungenen Ausgabe eines 
                bestimmten Formats verwenden werden. Beispiel:
            </p>
            <p>
                http://@(Tools.GetRequestDomain)/api/&nbsp;...&nbsp;<strong>?format=json</strong>
            </p>
            <p>Im Folgenden werden die einzelnen Methoden und ihre Ausgaben dokumentiert. 
                Sollten sich daraus Fragen ergeben, gerne via Mail an 
                <a href="mailto:@("api@" & Tools.GetRequestDomain)">@("api@" & Tools.GetRequestDomain)</a></p>
        </div>
    </div>
</div>

@Code
    Dim bolShaded As Boolean = False
End Code

<div class="content content-inverse container-fluid content-api-wrapper"
    data-root="representation" data-root-caption="Parlament (Fraktion)">
    <div class="row-fluid">
        <div class="span12 box-head">
            <i class="icon-group"></i>
            <h2>Parlament (Fraktion):<br /><span style="color: #333;">Representation</span></h2>
        </div>
    </div>
</div>

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-representation container-fluid" 
    data-method="GetKeyValueList">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/representation/GetKeyValueList</h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/representation/GetKeyValueList</td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Eine Key-Value-Liste aller in <strong>OpenAntrag</strong> vertretenen Parlamente (Fraktionen) 
                        mit Schlüsselwert und Namen
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetKeyValueList?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetKeyValueList?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetKeyValueList?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-representation container-fluid"
    data-method="GetAll">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/representation/GetAll</h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/representation/GetAll</td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Liste aller <code>Representation</code>-Objekte (<strong>Parlamente</strong>).
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetAll?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetAll?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetAll?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code    

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-representation container-fluid"
    data-method="GetByKey">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/representation/GetByKey/<span class="highlight">{key}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/representation/GetByKey/<span class="code">{KEY}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Ein <code>Representation</code>-Objekt (<strong>Parlament</strong>) anhand seines 
                        Schlüsselwertes (<span class="code">KEY</span>).
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetByKey/wiesbaden?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetByKey/wiesbaden?format=xml');">XML</button>*@
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-representation container-fluid"
    data-method="GetRepresentatives">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/representation/GetRepresentatives/<span class="highlight">{key}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/representation/GetRepresentatives/<span class="code">{KEY}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>

                <tr><th>Ausgabe</th>
                    <td>
                        Liste aller <code>Representative</code>-Objekte (<strong>Abgeordnete</strong>) 
                        einer Fraktion anhand des Schlüsselwertes eines Parlamentes (<span class="code">KEY</span>).
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetRepresentatives/wiesbaden?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetRepresentatives/wiesbaden?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetRepresentatives/wiesbaden?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-representation container-fluid"
    data-method="GetCommittees">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/representation/GetCommittees/<span class="highlight">{key}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/representation/GetCommittees/<span class="code">{KEY}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Liste aller <code>Committee</code>-Objekte (<strong>Ausschüsse</strong>) 
                        eines Parlaments (Fraktion) anhand des Schlüsselwertes eines Parlamentes 
                        (<span class="code">KEY</span>).
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetCommittees/wiesbaden?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetCommittees/wiesbaden?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetCommittees/wiesbaden?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-representation container-fluid"
    data-method="GetProcessSteps">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/representation/GetProcessSteps/<span class="highlight">{key}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/representation/GetProcessSteps/<span class="code">{KEY}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Liste aller <code>ProcessStep</code>-Objekte (<strong>Prozessschritte</strong>), 
                        inklusive der Folgeschritte, eines Parlaments (Fraktion) anhand des 
                        Schlüsselwertes eines Parlamentes (<span class=code>Key</span>).
                        <br /><br />
                        In der <code>Caption</code>-Eigenschaft sind Platzhalter wie <em>%REPRESENTATIVE%</em> oder 
                        <em>%COMMITTEE%</em> enthalten, wenn ein Schritt bei der Weitergabe eines Antrags einen 
                        zusätzlichen Wert benötigt.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetProcessSteps/wiesbaden?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetProcessSteps/wiesbaden?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetProcessSteps/wiesbaden?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-representation container-fluid"
    data-method="GetProcessStepById">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/representation/GetProcessStepById/<span class="highlight">{key}</span>/<span class="highlight">{id}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/representation/GetProcessStepById/<span class="code">{KEY}</span>/<span class="code">{ID}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr>
                    <th>Ausgabe</th>
                    <td>
                        Gibt ein <code>ProcessStep</code>-Objekt (<strong>Prozessschritt</strong>)  
                        eines Parlamentes (<span class="code">Key</span>) anhand seiner <span class="code">ID</span> zurück.
                        <br />
                        <br />
                        In der <code>Caption</code>-Eigenschaft sind Platzhalter wie <em>%REPRESENTATIVE%</em> oder 
                        <em>%COMMITTEE%</em> enthalten, wenn ein Schritt bei der Weitergabe eines Antrags einen 
                        zusätzlichen Wert benötigt.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetProcessStepById/wiesbaden/5?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/representation/GetProcessStepById/wiesbaden/5?format=xml');">XML</button>*@
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content content-inverse container-fluid content-api-wrapper"
    data-root="proposal" data-root-caption="Antrag">
    <div class="row-fluid">
        <div class="span12 box-head">
            <i class="icon-doc"></i>
            <h2>Antrag:<br /><span style="color: #333;">Proposal</span></h2>
        </div>
    </div>
</div>
@Code bolShaded = False End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-proposal container-fluid"
    data-method="GetCount">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/proposal/<span class="highlight2">{Key}</span>/GetCount</h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/proposal/<span class="code">{KEY}</span>/GetCount</td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr>
                    <th>Ausgabe</th>
                    <td>
                        Anzahl der in OpenAntrag erfassten Anträge.
                        <br />
                        <br />
                        Der <span class="code">KEY</span> bezeichnet entweder den Schlüsselwert eines 
                        Parlaments (Fraktion) oder den festen Wert <em>ALL</em>, um über alle Äntrage zu gehen.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/all/GetCount?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/all/GetCount?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/all/GetCount?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-proposal container-fluid"
    data-method="GetTop">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/proposal/<span class="highlight2">{Key}</span>/GetTop/<span class="highlight">{count}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/proposal/<span class="code">{KEY}</span>/GetTop/<span class="code">{COUNT}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Liste der letzten eingegangenen <code>Proposal</code>-Objekte (<strong>Anträge</strong>), 
                        mit dem Parameter <span class="code">COUNT</span> zur Angabe der Anzahl.
                        <br /><br />
                        Der <span class="code">KEY</span> bezeichnet entweder den Schlüsselwert eines 
                        Parlaments (Fraktion) oder den festen Wert <em>ALL</em>, um über alle Äntrage zu gehen.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/all/GetTop/5?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/all/GetTop/5?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/all/GetTop/5?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-proposal container-fluid"
    data-method="GetPage">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/proposal/<span class="highlight2">{Key}</span>/GetPage/<span class="highlight">{pageNo}</span>/<span class="highlight">{pageCount}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/proposal/<span class="code">{KEY}</span>/GetPage/<span class="code">{PAGENO}</span>/<span class="code">{PAGECOUNT}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Seitenweise Liste der <code>Proposal</code>-Objekte (<strong>Anträge</strong>), 
                        mit den Parametern <span class="code">PAGENO</span> zur Angabe der Seitenzahl 
                        und <span class="code">PAGECOUNT</span>, der Anzahl der Anträge pro Seite. 
                        Die Zählung der Seiten beginnt bei 1.
                        <br />
                        <br />
                        Der <span class="code">KEY</span> bezeichnet entweder den Schlüsselwert eines 
                        Parlaments (Fraktion) oder den festen Wert <em>ALL</em>, um über alle Äntrage zu gehen.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/wiesbaden/GetPage/1/3?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/wiesbaden/GetPage/1/3?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/wiesbaden/GetPage/1/3?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-proposal container-fluid"
    data-method="GetByTag">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/proposal/<span class="highlight2">{Key}</span>/GetByTag/<span class="highlight">{tag}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/proposal/<span class="code">{KEY}</span>/GetByTag/<span class="code">{TAG}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Liste aller <code>Proposal</code>-Objekte (<strong>Anträge</strong>), 
                        die mit einem Thema (<span class="code">TAG</span>) gekennzeichnet wurden.
                        <br />
                        <br />
                        Der <span class="code">KEY</span> bezeichnet entweder den Schlüsselwert eines 
                        Parlaments (Fraktion) oder den festen Wert <em>ALL</em>, um über alle Äntrage zu gehen.
                        <br /><br />
                        Das Thema (<span class="code">TAG</span>) muss Url-kodiert sein.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/wiesbaden/GetByTag/opendata?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/wiesbaden/GetByTag/opendata?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/wiesbaden/GetByTag/opendata?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-proposal container-fluid"
    data-method="GetByTitleUrl">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/proposal/GetByTitleUrl/<span class="highlight2">{Key}</span>/<span class="highlight">{titleurl}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/proposal/GetByTitleUrl/<span class="code">{KEY}</span>/<span class="code">{TITLEURL}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Gibt ein <code>Proposal</code>-Objekt (<strong>Antrag</strong>), 
                        anhand seiner suchmaschinenfreundlichen Url zurück.
                        <br />
                        <br />
                        Der <span class="code">KEY</span> bezeichnet entweder den Schlüsselwert eines 
                        Parlaments (Fraktion) oder den festen Wert <em>ALL</em>, um über alle Äntrage zu gehen.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/wiesbaden/GetByTitleUrl/open-data?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/wiesbaden/GetByTitleUrl/open-data?format=xml');">XML</button>*@
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-proposal container-fluid"
    data-method="GetById">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/proposal/GetById/<span class="highlight">{id}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/proposal/GetById/<span class="code">{ID}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Gibt ein <code>Proposal</code>-Objekt (<strong>Antrag</strong>), 
                        anhand seiner systemweit eindeutigen ID zurück.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/GetById/proposals-66?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/GetById/proposals-66?format=xml');">XML</button>*@
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-proposal container-fluid"
    data-method="GetComments">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/proposal/GetComments/<span class="highlight">{id}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/proposal/GetComments/<span class="code">{ID}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Liste aller <code>ProposalComment</code>-Objekte (<strong>Kommentare zu einem Antrag</strong>) 
                        eines Antrags anhand der eindeutigen ID des Antrags.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/GetComments/proposals-66?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/GetComments/proposals-66?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/GetComments/proposals-66?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-proposal container-fluid"
    data-method="GetTags">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/proposal/GetTags</h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/proposal/GetTags</td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Liste aller bereits verfügbaren Tags (<strong>Themen</strong>).                        
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/GetTags?format=json');">JSON</button>
@*                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/GetTags?format=xml');">XML</button>*@
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/proposal/GetTags?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-proposal container-fluid"
    data-method="PostNew">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/proposal/PostNew</h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/proposal/PostNew</td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>POST</td>
                </tr>
                <tr><th>Übergabe</th>
                    <td>
                        <code>ProposalDTO</code>-Objekt mit folgenden Eigenschaften:
                        <br /><br />
                        <table class="table table-bordered table-striped">
                            <tr>
                                <th>Api-Key</th>
                                <td>Api-Schlüssel der Fraktion</td>
                            </tr>
                            <tr>
                                <th>Key_Representation</th>
                                <td>Schlüsselwert des Parlaments (Fraktion)</td>
                            </tr>
                            <tr>
                                <th>Title</th>
                                <td>Title des neuen Antrags</td>
                            </tr>
                            <tr>
                                <th>Text</th>
                                <td>Text des neuen Antrags in <strong>Markdown-Syntax</strong></td>
                            </tr>
                            <tr>
                                <th>TagList</th>
                                <td>Komma-separierte Liste der dem Antrag zuzuweisenden Themen</td>
                            </tr>
                        </table>
                        Beispiel:
                        <br />
                        <pre>
{
  "ApiKey" : "abcdefghijklmnopqrstuvwxyz",
  "Key_Representation": "wiesbaden",
  "Title": "Dies ist ein Test...",
  "Text": "Dies ist ein Test",
  "TagList": "Transparenz,Bildung"
}
                        </pre>
                    </td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Neu erzeugtes <code>Proposal</code>-Objekt (<strong>Antrag</strong>) mit allen Eigenschaften. 
                    </td>
                </tr>
            </table>
            <br />
            @If Tools.IsAdmin = True Then
                @<button class="btn btn-small btn-primary"
                    onclick="postApi(this, '/api/proposal/PostNew');">TEST</button>                
            End If            
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-proposal container-fluid"
    data-method="PostNextStep">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/proposal/PostNextStep</h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/proposal/PostNextStep</td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>POST</td>
                </tr>
                <tr>
                    <th>Hinweis</th>
                    <td>
                        Ein <code>Proposal</code>-Objekt (<strong>Antrag</strong>) enthält eine Auflistung von 
                        <code>ProposalStep</code>-Objekten (<strong>Antragsschritt</strong>). Der aktuell gültige 
                        Antragsschritt aus der Liste kann über die Eigenschaft <span class="code">ID_CurrentProposalStep</span>
                        ermittelt werden. Dieser Wert verweist auf die fortlaufende <span class="code">Id</span> der Antragsschritte.
                        <br />
                        <br />
                        Ein Antragsschritt ist die Ausformulierung eines Prozessschrittes. In einem <code>ProposalStep</code>-Objekt
                        wird dieses über das untergeordnete <code>ProcessStep</code>-Objekt zur Verfügung gestellt.
                        <br />
                        <br />
                        Die nächsten möglichen Schritte lassen sich über die <code>ProcessStep</code>-Eigenschaft 
                        <span class="code">ID_NextSteps</span> als komma-separierte ID-Liste abrufen. Ein komplettes 
                        <code>ProcessStep</code>-Objekt kann <strong>/api/representation/GetProcessSteps</strong> abgerufen werden.
                        <br />
                        <br />
                        In einigen Prozessschritten sind Variablen enthalten, die kennzeichnen, dass weitere Daten zu diesem 
                        Schritt benötigt werden. Dies wären aktuell:
                        <table class="table table-bordered table-striped" style="margin: 10px 0">
                            <tr>
                                <th>%REPRESENTATIVE%</th>
                                <td>
                                    Abgeordneter<br />
                                    <em>Auswahlliste über <strong>/api/representation/GetRepresentatives</strong></em>
                                </td>
                            </tr>
                            <tr>
                                <th>%COMMITTEE%</th>
                                <td>
                                    Ausschuss<br />
                                    <em>Auswahlliste über <strong>/api/representation/GetCommittees</strong></em>
                                </td>
                            </tr>
                        </table>
                        Benötigt ein Antragsschritt eine der Variablen, muss der entsprechende Wert im 
                        zu übergebenden Data Transfer Object gefüllt sein.
                        <br /><br />
                    </td>
                </tr>
                <tr>
                    <th>Übergabe</th>
                    <td>
                        <code>ProposalNextStepDTO</code>-Objekt mit folgenden Eigenschaften:
                        <br />
                        <br />
                        <table class="table table-bordered table-striped">
                            <tr>
                                <th>Api-Key</th>
                                <td>Api-Schlüssel der Fraktion</td>
                            </tr>
                            <tr>
                                <th>Key_Representation</th>
                                <td>Schlüsselwert des Parlaments (Fraktion)</td>
                            </tr>
                            <tr>
                                <th>ID_Proposal</th>
                                <td>ID des Antrags</td>
                            </tr>
                            <tr>
                                <th>ID_ProcessStep</th>
                                <td>ID des einzustellenden Prozessschrittes</td>
                            </tr>
                            <tr>
                                <th>InfoText</th>
                                <td>Informationstext in <strong>Markdown-Syntax</strong></td>
                            </tr>
                            <tr>
                                <th>Key_Representative</th>
                                <td>Schlüsselwert des Abgeordneten, wenn der Prozesschritt dies benötigt</td>
                            </tr>
                            <tr>
                                <th>Key_Committee</th>
                                <td>Schlüsselwert des Ausschusses, wenn der Prozesschritt dies benötigt</td>
                            </tr>
                        </table>
                        Beispiel:
                        <br />
                        <pre>
{
  "ApiKey" : "abcdefghijklmnopqrstuvwxyz",
  "Key_Representation": "wiesbaden",
  "ID_Proposal" : "proposals-66",
  "ID_ProcessStep" : "10",
  "InfoText" : "Antrag wurde einstimmig angenommen",
  "Key_Representative" : null,
  "Key_Committee" : null
}
                        </pre>
                    </td>
                </tr>
                <tr>
                    <th>Ausgabe</th>
                    <td>
                        Neu erzeugtes <code>ProposalStep</code>-Objekt (<strong>Antragsschritt</strong>) mit allen Eigenschaften. 
                    </td>
                </tr>
            </table>
            <br />
            @If Tools.IsAdmin = True Then
                @<button class="btn btn-small btn-primary"
                    onclick="postApi(this, '/api/proposal/PostNextStep');">TEST</button>                
            End If
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content content-inverse container-fluid content-api-wrapper"
    data-root="notifications" data-root-caption="Mitteilungen">
    <div class="row-fluid">
        <div class="span12 box-head">
            <i class="icon-bell"></i>
            <h2>Mitteilungen:<br /><span style="color: #333;">Notifications</span></h2>
        </div>
    </div>
</div>

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-notifications container-fluid" 
    data-method="GetKeyValueList">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/notifications/GetTypeList</h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/notifications/GetTypeList</td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Eine Key-Value-Liste aller in <strong>OpenAntrag</strong> verwendeten Mitteilungstypen
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/notifications/GetTypeList?format=json');">JSON</button>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/notifications/GetTypeList?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-notifications container-fluid" 
    data-method="GetLast">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/notifications/GetLast/<span class="highlight">{count}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/notifications/GetLast/<span class="code">{count}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Liste der letzten <code>Notification</code>-Objekte (<strong>Mitteilungen</strong>), 
                        mit dem Parameter <span class="code">COUNT</span> zur Angabe der Anzahl.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/notifications/GetLast/10?format=json');">JSON</button>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/notifications/GetLast/10?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code

<div class="content @(IIf(bolShaded=true, "content-shaded", "")) content-api content-api-notifications container-fluid" 
    data-method="GetLastByType">
    <div class="row-fluid">
        <div class="span12">
            <h3>/api/notifications/GetLastByType/<span class="highlight">{typeId}</span>/<span class="highlight">{count}</span></h3>
            <table class="api-info">
                <tr>
                    <th>Url</th>
                    <td>http://@(HttpContext.Current.Request.Url.Authority)/api/notifications/GetLast/<span class="code">{typeId}</span>/<span class="code">{count}</span></td>
                </tr>
                <tr>
                    <th>HTTP-Methode</th>
                    <td>GET</td>
                </tr>
                <tr><th>Ausgabe</th>
                    <td>
                        Liste der letzten <code>Notification</code>-Objekte (<strong>Mitteilungen</strong>) 
                        eines bestimmten Typs (<span class="code">TYPEID</span>), 
                        mit dem Parameter <span class="code">COUNT</span> zur Angabe der Anzahl.
                    </td>
                </tr>
                <tr>
                    <th>Beispielergebnisse:</th>
                    <td>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/notifications/GetLastByType/0/10?format=json');">JSON</button>
                        <button class="btn btn-small btn-primary"
                            onclick="getApiResult(this, '/api/notifications/GetLastByType/0/10?format=csv');">CSV</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
@Code bolShaded = Not bolShaded End Code
