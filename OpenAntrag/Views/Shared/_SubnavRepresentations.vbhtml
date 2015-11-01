@Imports OpenAntrag

@Code

    'Dim rps As New Representations(Representations.StatusConjuction.Active + Representations.StatusConjuction.Ended)
    'Dim lstRepresentations As List(Of Representation) = rps.Items.OrderBy(Function(x) x.Level).ThenBy(Function(x) x.Key).ToList

    Dim lst As List(Of Representation) = GlobalData.Representations.Items _
        .Where(Function(x) (x.Status And (Representations.StatusConjuction.Active + Representations.StatusConjuction.Ended)) > 0) _
        .OrderBy(Function(x) x.Level) _
        .ThenBy(Function(x) x.Key) _
        .ToList()

    Dim gls As New GovernmentalLevels
    
    Dim gCookie As String = Tools.GetCookie("OpenAntrag-NavFilter-G", True)
    Dim fCookie As String = Tools.GetCookie("OpenAntrag-NavFilter-F", true)

End Code

<div id="mainsubnav-rep-container" style="display: none;">

    <div id="mainsubnav-rep-filter-federalstate" class="mainsubnav-rep-filter"
         style="margin-top: 10px">
        <a id="filter-f0"
           href="javascript:go();" onclick="filterRepresentationSubnav(this); return false;"
           data-filter-type="f" data-filter="" class="@(IIf(fCookie.IsNullOrEmpty, "selected", ""))">Alle</a>
        @For Each f As FederalState In GlobalData.FederalStates.Items
            @<a id="filter-f@(f.Key)"
                href="javascript:go();" onclick="filterRepresentationSubnav(this); return false;"
                class="@(IIf(fCookie.EndsWith(f.Key), "selected", ""))"
                data-filter-type="f" data-filter="f@(f.Key)">@f.Name</a>
        Next
    </div>

    <div id="mainsubnav-rep-filter-governmentallevel" class="mainsubnav-rep-filter"
        style="margin-top: 5px">
        <a id="filter-g0"
           href="javascript:go();" onclick="filterRepresentationSubnav(this); return false;"
           data-filter-type="g" data-filter="" class="@(IIf(gCookie.IsNullOrEmpty, "selected", ""))">Alle</a>
        @For Each g As GovernmentalLevel In gls.Items
            @<a id="filter-g@(g.ID)" 
                href="javascript:go();" onclick="filterRepresentationSubnav(this); return false;"
                class="@(IIf(gCookie.EndsWith(g.ID), "selected", ""))"
                data-filter-type="g" data-filter="g@(g.ID)">@g.Name</a>
        Next
    </div>

    <div id="mainsubnav-rep-list" style="margin-bottom: 15px">
        @For Each r As Representation In lst
        @Code
            Dim bDisplayNone As Boolean = False
            If gCookie.IsNullOrEmpty = False AndAlso String.Concat("g", r.Level) <> gCookie OrElse fCookie.IsNullOrEmpty = False AndAlso String.Concat("f", r.FederalKey) <> fCookie Then
                bDisplayNone = True
            End If
        End Code
            @<a href="/@(r.Key)" class="rbox rid@(r.ID) g@(r.Level) f@(r.FederalKey) @(r.StatusName)" 
                style="@(IIf(bDisplayNone, "display:none;", ""))">@(r.[Name2])</a>
        Next
        <div id="no-representations" style="display: none; float: left; padding: 5px 0;  color: rgb(255, 255, 255);">
            Hier können wir Dir leider nicht weiterhelfen, aber wir arbeiten dran ;)
        </div>
    </div>

</div>
