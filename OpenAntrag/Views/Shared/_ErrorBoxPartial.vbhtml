@Imports OpenAntrag
@ModelType CustomErrorInfo

<div class="">    
    
    @Model.ErrorMessage
    
    @If Model.ReferrerUrl IsNot Nothing Then
        @<br />
        @<span>zurück zur <a href="@Model.ReferrerUrl">letzten Seite</a></span>
    End If

</div>