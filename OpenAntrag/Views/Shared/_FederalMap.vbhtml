@Imports OpenAntrag

@ModelType FederalStates

<div id="mapD" class="representation-map">

@Code
    'Dim rps As New Representations(Representations.StatusConjuction.Active)

    Dim lst As List(Of Representation) = GlobalData.Representations.Items _
        .Where(Function(x) (x.Status And (Representations.StatusConjuction.Active)) > 0) _
        .ToList()
    
    For Each m In Model.Items
    
        Dim query = From rp As Representation In lst
                    Where rp.FederalKey.ToUpper = m.Key.ToUpper And rp.Status > 0
                    Select rp
                    
        Dim stat As String = "active"
        If query.Count = 0 Then stat = "inactive"
        
        @<a id="map@(m.Key)" href="javascript:go();" class="@(stat)"
            data-federal="@(m.Key)" data-count="@(query.count)">
            <img src="/Images/Map/200-@(m.Key)-@(stat).png" alt="@(m.Name)" />
        </a>

    Next
End Code
    <div id="mapInfo">&nbsp;</div>
</div>
