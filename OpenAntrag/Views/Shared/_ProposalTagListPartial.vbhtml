@Imports OpenAntrag
@ModelType Proposal

@If Model.Tags IsNot Nothing Then
    For Each tag As String In Model.Tags
        If String.IsNullOrEmpty(tag) = False Then
            @<a class="btn" href="/themen/@tag"><i class="icon-tag">&nbsp;&nbsp;</i>@tag</a>
        End If
    Next
End If
