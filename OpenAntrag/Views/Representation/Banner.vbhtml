@Imports OpenAntrag
@ModelType Representation

@Code
    ViewData("Title") = Model.Name & " | Banner"
End Code

@Section Styles
    @Styles.Render(String.Concat("~/css/representations-", Model.Key))
    <link href="/@(Model.Key)/style-representation" rel="stylesheet" type="text/css" media="screen" />
End Section

@Section Scripts
End Section

@Section Intro
    @Html.PartialOrNull("_RepresentationIntro", Model)    
End Section

@Section RepNav
    @Html.PartialOrNull("_NavRepresentation", Model)    
End Section

<div class="content content-representation container-fluid">
    <div class="row-fluid">
        <div class="span3 box-head">
            <i class="icon-coverflow-empty"></i>
            <h2 style="margin:0 90px;">Die<br />
                Banner</h2>
            <br />
        </div>
        <div class="span8 offset1">
            <p>OpenAntrag lebt von den Anträgen der Bürger, die natürlich erstmal 
                wissen müssen, dass es die Seite überhaupt gibt. Da hilft nur 
                Werbung, Werbung, Werbung. Hier ein paar Banner für Web-Seiten...</p>
            <p>Bitte kopiere Dir die Grafiken und setze keine <a href="http://de.wikipedia.org/wiki/Hotlinking">Hotlinks</a>, 
                denn die kosten Bandbreite, die wir besser für Anträge nutzen sollten.</p>
        </div>
    </div>
</div>

<div class="content content-faq content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>individuell</small>
            <h3>160 x 220 Pixel, statisch</h3>
            <br />
            <img src="/Content/Representations/@(Model.Key)/banner-160x220.png" style="float:left; margin-right: 30px;" />

            @If HttpContext.Current.User.IsInRole("admin") Then
            @<div style="position: relative; width: 160px; height: 220px; overflow: hidden;">
                <div style="position: absolute; top: 0; height: 38px; width: 160px; background: none repeat scroll 0px 0px rgb(102, 102, 102); border-top: 7px solid #FF8800;">
                    <img style="height: 30px; padding: 4px 0px 4px 2px;" src="/Images/Logos/OALogo50.png">
                    <span style="font-family: 'PoliticsHeadBold','Helvetica Neue','Arial',Sans-Serif; color: #fff; font-size: 21px; display: inline-block; height: 30px; vertical-align: bottom; margin-left: -1px;">OpenAntrag</span>
                </div>
                <div style="position: absolute; top: 45px; height: 100px; width: 160px; background-color: #000;"></div>
                <img style="position: absolute; top: 45px; height: 100px; right: 0px; max-width: 350px; overflow: hidden; opacity: 0.8;" 
                     src="/Content/Representations/@(Model.Key)/IntroBack/770.jpg">
                <div style="position: absolute; top: 45px; height: 80px; padding: 10px 8px; font-family: 'PoliticsHeadBold','Helvetica Neue','Arial',Sans-Serif; color: #fff; font-size: 19px; opacity: 0.9">
                    <div>Bürgeranträge</div>
                    <div style="margin-top: 10px">Dein Anliegen : Unser Antrag</div>
                </div>
                <div style="position: absolute; top: 145px; height: 75px; width: 160px; background-color: @(Model.Color)">
                    <div style="padding: 6px 8px; font-size: 18px; line-height: 20px; font-family: 'PoliticsHeadBold','Helvetica Neue','Arial',Sans-Serif; -ms-word-wrap: break-word; word-wrap: break-word; color: white;">
                        @(Model.Name)
                    </div>
                </div>
            </div>
            End If            
        </div>
    </div>
</div>

<div class="content content-faq container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>individuell</small>
            <h3>275 x 80 Pixel, statisch</h3>
            <br />
            <img src="/Content/Representations/@(Model.Key)/banner-275x80.png" style="float:left; margin-right: 30px;" />

            @If HttpContext.Current.User.IsInRole("admin") Then
            @<div style="position: relative; width: 275px; height: 80px; overflow: hidden;">
                    <div style="position: absolute; top: 0px; width: 275px; height: 33px; border-top: 7px solid #ff8800; background: none repeat scroll 0px 0px #666;">
                        <img style="height: 25px; padding: 4px 0px 4px 2px;" src="/Images/Logos/OALogo50.png">
                        <div style="font-family: 'PoliticsHeadBold','Helvetica Neue','Arial',Sans-Serif; color: #fff; font-size: 24px; display: inline-block; height: 27px; vertical-align: bottom;">OpenAntrag</div>
                        <img style="position: absolute; top: 0; left: 180px; width: 110px; margin: 0 0 0 0" 
                             src="/Content/Representations/@(Model.Key)/IntroBack/770.jpg">
                    </div>
                    <div style="position: absolute; top: 40px; height: 40px; width: 275px; padding: 10px; line-height: 16px; 
                                background-color: @(Model.Color); 
                                font-size: 18px; font-family: 'PoliticsHeadBold','Helvetica Neue','Arial',Sans-Serif; color: #fff;">
                        @(Model.Name)
                    </div>
                </div>
            End If

        </div>
    </div>
</div>

<div class="content content-faq content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>generisch</small>
            <h3>160 x 220 Pixel, statisch</h3>
            <br />
            <img src="/Images/Content/banner-160x220.png" />
        </div>
    </div>
</div>

<div class="content content-faq container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <small>generisch</small>
            <h3>275 x 80 Pixel, statisch</h3>
            <br />
            <img src="/Images/Content/banner-275x80.png" />
        </div>
    </div>
</div>