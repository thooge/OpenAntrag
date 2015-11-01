@Imports OpenAntrag
@ModelType Representation

@Code
    ViewData("Title") = Model.Name    
End Code

@Section Links
    <link rel="alternate" type="application/rss+xml" title="OpenAntrag-Feed @Model.Name" href="http://@(HttpContext.Current.Request.Url.Authority)/@(Model.Key)/feed" />
End Section

@Section Styles
    @Styles.Render(String.Concat("~/css/representations-", Model.Key))
    <link href="/@(Model.Key)/style-representation" rel="stylesheet" type="text/css" media="screen" />
End Section

@Section Scripts
    @Scripts.Render("~/bundle/representations")
    @Scripts.Render("~/bundle/markdown")
    <script>
        $(document).ready(function () {
        });
    </script>
End Section

@Section Intro
    <img src="/Content/Representations/@(Model.Key)/banner-160x220.png" style="position: absolute; left: -1000px; top: -1000px; width:1px; height: 1px; opacity:0" />
    <img src="/Content/Representations/@(Model.Key)/banner-275x80.png" style="position: absolute; left: -1000px; top: -1000px; width:1px; height: 1px; opacity:0" />
    @Html.PartialOrNull("_RepresentationIntro", Model)
End Section

@Section RepNav
    @Html.PartialOrNull("_NavRepresentation", Model)
End Section

<div class="content content-navigation content-representation content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span7">
            <div class="btn-group btn-group-invers">
                <button class="btn btn-small btn-primary btn-selected" data-area="group-info-area" 
                        onclick="switchButtonGroup(this); return false;">Über @(Model.GroupTypeGen)</button>
                <button class="btn btn-small btn-primary" data-area="representation-info-area" 
                        onclick="switchButtonGroup(this); return false;">Über das Parlament</button>
            </div>
        </div>
        @If (Model.Status And Representations.StatusConjuction.Ended) = 0 Then
            @<div class="span5 right">
                <p>
                    Du hast eine Idee für @(Model.LabelWithArticle)?<br />
                    <a href="@(Model.FullUrl)/neu"><strong>Schreib jetzt Deinen Antrag!</strong></a>
                </p>
            </div>
        End If
    </div>
</div>

<div class="content container-fluid content-representation">
    <div class="row-fluid" style="margin-top: 0;">
        <div class="@(IIf(Model.piratenmandate is Nothing,"span12", "span8"))">
            <div id="group-info-area" style="display:block;">

                <div class="content content-representation container-fluid">
                    <div class="row-fluid">
                        <div id="representation">
                            <h2>@Model.GroupName</h2>
                            <div class="comm">
                                @If String.IsNullOrEmpty(Model.Link) = False Then
                                    @<p>
                                        <i class="icon-globe" style="font-size: 18px;"></i>&nbsp;
                                        <a href="@Model.Link">@Model.Link</a>
                                    </p>
                                End If
                                @If String.IsNullOrEmpty(Model.Mail) = False Then
                                    @<p>
                                        <i class="icon-email"></i>&nbsp;
                                        <a href="mailto:@Model.Mail">@Model.Mail</a>
                                    </p>                
                                End If
                                @If String.IsNullOrEmpty(Model.Phone) = False Then                    
                                    @<p>
                                        <i class="icon-phone"></i>&nbsp;&nbsp;
                                        <a href="tel:@Model.Phone">@Model.Phone</a>
                                    </p>
                                End If
                                @If String.IsNullOrEmpty(Model.Twitter) = False Then
                                    @<p>
                                        <i class="icon-twitter"></i>&nbsp;
                                        <a href="https://twitter.com/@Model.Twitter">@@@Model.Twitter</a>
                                    </p>
                                End If
                            </div>
                            <div style="margin-top: 20px;">
                                @Html.Raw(Model.FraktionInfoHtml)
                            </div>
                        </div>
                    </div>
                </div>

            @If (Model.Status And Representations.StatusConjuction.Ended) = 0 Then    
                @<div class="container-fluid">
                    <div class="row-fluid">
                        @If Model.piratenmandate Is Nothing Then
                            @<div class="span6">
                            @For i As Integer = 0 To Model.Representatives.Count - 1 Step 2                    
                                @Html.Partial("_RepresentativePartial", Model.Representatives(i))
                            Next
                            </div>
                            @<div class="span6">
                            @For i As Integer = 1 To Model.Representatives.Count - 1 Step 2
                                @Html.Partial("_RepresentativePartial", Model.Representatives(i))
                            Next
                            </div>
                        Else
                            @<div class="span12">
                            @For Each r As Representative In Model.Representatives
                                @Html.Partial("_RepresentativePartial", r)
                            Next
                            </div>
                        End If
                    </div>
                </div>    
            End If

            </div>

            <div class="content container-fluid" 
                 id="representation-info-area" style="display:none;">
                <div class="row-fluid">
                    <div>
                        <small>@Model.Federal.Name</small>
                        <h2>@Model.Name</h2>

                        <div style="margin-top: 20px; margin-bottom: 20px;">
                            @Html.Raw(Model.ParlamentInfoHtml)
                        </div>

                        <h5>Ausschüsse</h5>
                        @If Model.HasCommittees = False Then
                            @<p>Dieses Parlament hat keine Ausschüsse</p>                        
                        Else
                            If Model.Committees.Count = 0 Then
                                @<p style="font-style:italic;">Die Ausschüsse wurden noch nicht benannt.</p>
                            Else
                                @<ul class="tight">
                                    @For Each cm As Committee In Model.Committees
                                        @<li>
                                            @If String.IsNullOrEmpty(cm.Url) = False Then
                                                @<a href="@(Server.UrlDecode(cm.Url))">@cm.Name</a>
                                            Else
                                                @cm.Name
                                            End If
                                        </li>
                                    Next                    
                                </ul>
                            End If                            
                        End If

                    </div>
                </div>
            </div>
        </div>

        @If Model.piratenmandate IsNot Nothing Then
            @Html.PartialOrNull("_PiratenmandatePartial", Model.piratenmandate)
        End If

    </div>
</div>

@Section PreFooter
    @Html.PartialOrNull("_BannerTeaserPartial", Model)
End Section