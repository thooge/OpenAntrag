@Imports OpenAntrag
@ModelType List(Of Representation)

@Code
    ViewData("Title") = "Übersicht"
End Code

@Section Styles
    @Styles.Render("~/css/api")
End Section

@Section Intro
    <p>Mit jedem in OpenAntrag vertretenen Parlament bringen wir Piraten die Bürger ein Stück naher heran an die Politik</p>
End Section

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <h3>Parlamente/Fraktionen</h3>
            <table class="responsive table table-bordered table-striped table-hover">
                <thead>
                    <tr>
                        <th class="responsive-pin" style="width: 20px;">ID</th>                        
                        <th class="responsive-pin" >Parlament</th>
                        <th style="width: 100px;">Land</th>
                        <th>Fraktion</th>
                        <th>Mail</th>
                        <th style="width: 100px;">Telefon</th>
                    </tr>
                </thead>
                <tbody>
                    @For Each rp As Representation In Model
                        Representations.EnsureRepresentationClone(rp)
                        @<tr class="rid@(rp.ID) @(rp.StatusName)">
                            <td class="responsive-pin">@(rp.ID)</td>
                            <td class="responsive-pin"><a href="@(rp.FullUrl)">@(rp.Name2)</a></td>
                            <td>@(rp.Federal.Name)</td>
                            <td>@(rp.GroupName)</td>
                            <td>@(rp.Mail)</td>
                            <td>@(rp.Phone)</td>
                         </tr>
                    Next
                </tbody>
            </table>
        </div>
    </div>
</div>

@If HttpContext.Current.User.Identity.IsAuthenticated = True Then
@<div class="content content-shaded container-fluid">
    <div class="row-fluid">
        <div class="span12">
            <h3>Mail-Liste</h3>
            <p>
                @For Each rp As Representation In Model
                    @(rp.InfoMail & "; ") 
                Next
            </p>
        </div>
    </div>
</div>
End If
