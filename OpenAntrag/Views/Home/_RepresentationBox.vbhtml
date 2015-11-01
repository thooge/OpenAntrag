@Imports OpenAntrag
@ModelType Representation

<div class="fraktionbox fs@(Model.FederalKey) rid@(Model.ID)">
    <a href="/@Model.Key" class="rbar"><i class="icon-right-open"></i></a>
    <div>
        <small>@Model.Federal.Name</small>
        <a class="rep-name @Model.StatusName" href="/@Model.Key">
            <h4>@Model.[Name]</h4>
        </a>
        <em class="group-name @Model.StatusName">@Model.GroupName</em>
        @If String.IsNullOrEmpty(Model.Link) = False Then
            @<a href="@Model.Link">@Model.Link</a>
        End If
        @If String.IsNullOrEmpty(Model.LogoImage) = False Then
            @<a href="/@Model.Key"><img src="@Model.LogoImage"></a>
        End If
    </div>
</div>
