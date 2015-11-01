@ModelType OpenAntrag.Representative

<div class="representative @Model.Party">
    <img src="@Model.PortraitImage" alt="@Model.Key" />
    <div>
        <h5>@Model.Name</h5>
        <span>@Model.Party</span>
        <div class="comm">
            @If String.IsNullOrEmpty(Model.Mail) = False Then
                @<a href="mailto:@Model.Mail">
                    <i class="icon-email">@Model.Mail</i>
                </a>
            End If
            @If String.IsNullOrEmpty(Model.Phone) = False Then
                @<a href="tel:@Model.Phone">
                    <i class="icon-phone">&nbsp;@(Model.Phone)</i>
                    </a>
            End If
            @If String.IsNullOrEmpty(Model.Twitter) = False Then
                @<a href="https://twitter.com/@(Model.Twitter)">
                    <i class="icon-twitter">@@@(Model.Twitter)</i>
                </a>
            End If
        </div>
    </div>
</div>