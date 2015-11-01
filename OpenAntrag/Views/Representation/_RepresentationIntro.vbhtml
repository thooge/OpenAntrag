@Imports OpenAntrag
@ModelType Representation

<h3>@Model.Name</h3>

@If Model.LogoImagePage IsNot Nothing Then
    @<img src="@Model.LogoImagePage" alt="@Model.GroupName" />    
Else
    @<p class="group">@Model.GroupName</p>
End If
<small class="fotoby"></small>