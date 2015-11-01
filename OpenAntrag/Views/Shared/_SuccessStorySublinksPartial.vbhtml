@Imports OpenAntrag
@ModelType SuccessStory

<ul class="sublinks">
    @If String.IsNullOrEmpty(Model.ShortUrl) = False Then
        @<li>
            <em>Kurzlink: </em>
            <a href="@(Model.ShortUrl)">@(Model.ShortUrl.Replace("http://", ""))</a>
         </li>
    End If
</ul>