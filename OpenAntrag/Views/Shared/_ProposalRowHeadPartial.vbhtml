@Code
    Dim bolShowRep As Boolean = ViewData("ShowRepresentation")
End Code

<tr>
    <th class="tt-std" title="Prozeßschritt" style="min-width: 32px;">
        <i class="icon-direction" style="font-size:1.1rem;"></i>
    </th>
    <th>Datum</th>
    @If bolShowRep = True Then
        @<th>Fraktion</th>
    End If
    <th>Titel</th>
    <th class="tt-std" title="Letzte Änderung" style="min-width: 32px;">
        <i class="icon-edit"></i>
    </th>
    <th class="tt-std" title="Kommentare" style="min-width: 30px;">
        <i class="icon-comment"></i>
    </th>
    <th class="tt-std" title="Unterstützungspunkte" style="min-width: 32px;">
        <i class="icon-star"></i>
    </th>
</tr>