@Imports OpenAntrag
@ModelType List(Of Notification)

@Code
    ViewData("Title") = "Mitteilungen"

    Dim strType As String = ""
    If ViewData("Type") IsNot Nothing Then
        strType = ViewData("Type")
    End If

    Dim intTypeId As Integer = -1
    If ViewData("TypeId") IsNot Nothing Then
        intTypeId = ViewData("TypeId")
    End If

    Dim stbFeedUrl As New StringBuilder("/mitteilungen/feed")
    Dim strFeedType As String = "Alle"
    If intTypeId > -1 Then
        stbFeedUrl.Append("/").Append(strType)
        strFeedType = Notifications.GetTypeStringPlural(intTypeId)
    End If
End Code

@Section Styles
    @Styles.Render("~/css/notifications")
End Section

@Section Scripts
    @Scripts.Render("~/bundle/notifications")
    @Scripts.Render("~/bundle/markdown")
    <script>
        $(document).ready(function () {
        });
    </script>
End Section

@Section Intro
    <p>
        Was passiert hier eigentlich? Was ist wo los?<br />
        Hier findest Du eine Chronologie der Aktivitäten...
    </p>
End Section

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span4 box-head">
            <i class="icon-bell"></i>
            <h2>Die<br />
                Mitteilungen</h2>
            <br />
            <br />
            <p>Abonniere den <a href="@(stbFeedUrl.ToString)">Mitteilungs-Feed (@(strFeedType))</a>, 
                um auf dem Laufenden zu bleiben.</p>
            <div id="toc"></div>
        </div>
        <div class="span7 offset1 box">
            <p>In OpenAntrag gibt es viele verschiedene Aktivtäten, die meist eine Mail an 
                die betreffende Fraktion, einen Tweet auf Twitter oder Ähnliches nach sich ziehen, 
                um zu informieren.
            </p>
            <p>Alle diese Nachrichten und noch ein paar mehr findest Du hier in chronologischer 
                Reihenfolge und filterbar über folgende Kategorien:</p>

            <input id="nf-filter-type" type="hidden" value="@(intTypeId)" />
            <div id="nf-filter">
                <a class="cmd-filter nf-type-all @(IIf(intTypeId = -1,"selected", ""))" href="/mitteilungen">Alle</a>
                <a class="cmd-filter nf-type-0 @(IIf(intTypeId = 0,"selected", ""))" href="/mitteilungen/@(Notifications.GetTypeStringPlural(0).ToLower)">@(Notifications.GetTypeStringPlural(0))</a>
                <a class="cmd-filter nf-type-1 @(IIf(intTypeId = 1,"selected", ""))" href="/mitteilungen/@(Notifications.GetTypeStringPlural(1).ToLower)">@(Notifications.GetTypeStringPlural(1))</a>
                <a class="cmd-filter nf-type-2 @(IIf(intTypeId = 2,"selected", ""))" href="/mitteilungen/@(Notifications.GetTypeStringPlural(2).ToLower)">@(Notifications.GetTypeStringPlural(2))</a>
                <a class="cmd-filter nf-type-5 @(IIf(intTypeId = 5,"selected", ""))" href="/mitteilungen/@(Notifications.GetTypeStringPlural(5).ToLower)">@(Notifications.GetTypeStringPlural(5))</a>
                <a class="cmd-filter nf-type-6 @(IIf(intTypeId = 6,"selected", ""))" href="/mitteilungen/@(Notifications.GetTypeStringPlural(6).ToLower)">@(Notifications.GetTypeStringPlural(6))</a>
                <a class="cmd-filter nf-type-3 @(IIf(intTypeId = 3,"selected", ""))" href="/mitteilungen/@(Notifications.GetTypeStringPlural(3).ToLower)">@(Notifications.GetTypeStringPlural(3))</a>
                <a class="cmd-filter nf-type-4 @(IIf(intTypeId = 4,"selected", ""))" href="/mitteilungen/@(Notifications.GetTypeStringPlural(4).ToLower)">@(Notifications.GetTypeStringPlural(4))</a>                
            </div>

        </div>
    </div>
</div>

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span12" id="nf-wrapper">
            <ul id="nf-list">
                @Html.Partial("_NotificationPartial", Model)
            </ul>
        </div>
    </div>
</div>

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span12" id="nf-more" style="margin-top: -30px;">
            <a id="nf-more-cmd" href="javascript:go();" onclick="getMore();" class="more">Mehr...</a>
            <input id="nf-page" type="hidden" value="1" />
        </div>
    </div>
</div>
