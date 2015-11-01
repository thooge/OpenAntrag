@Imports OpenAntrag
@ModelType ErrorLog

@Code
    If Model Is Nothing Then
        ViewData("Title") = "ErrorLog"
    Else
        ViewData("Title") = Model.Id
    End If
End Code

@Section Styles
    @Styles.Render("~/css/error")
End Section

@Section Scripts
    <script>
        $(document).ready(function () {
        });
    </script>l
End Section

@Section Intro
    <p>Fehler sind die Würze, die dem Erfolg sein Aroma geben<br />- Truman Capote</p>
End Section

<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span12 box-head">
            <i class="icon-bell"></i>            

            @If Model Is Nothing Then
                @<h2>Fehler-ID nicht gefunden</h2>
            Else
                @<h2>@(Model.AbsoluteUri)</h2>
            End if
        </div>
    </div>
</div>

@If Model IsNot Nothing Then
@<div class="content container-fluid">
    <div class="row-fluid">
        <div class="span12">
            
            <table class="table table-striped">
                <tbody>
                    <tr>
                        <th style="width:150px;">Controller</th>
                        <td>@(Model.Controller)</td>
                    </tr>
                    <tr>
                        <th>Action</th>
                        <td>@(Model.Action)</td>
                    </tr>
                    <tr>
                        <th>RequestType</th>
                        <td>@(Model.RequestType)</td>
                    </tr>
                    <tr>
                        <th>ReferrerUrl</th>
                        <td>@(Model.ReferrerUrl)</td>
                    </tr>
                    <tr>
                        <th>AjaxCall</th>
                        <td>@(Model.AjaxCall)</td>
                    </tr>
                    <tr>
                        <th>Parameters</th>
                        <td>
                            @For Each pm As String In Model.Parameter
                                @<span>@pm</span>
                                @<br />                                        
                            Next           
                        </td>
                    </tr>
                </tbody>
            </table>                
            <p>@(Model.Message)</p>
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>CreatedAt</th>
                        <th>CreatedBy</th>
                        <th>UserAgent</th>
                    </tr>
                </thead>
                <tbody>
                    @For Each eo As ErrorOccurrence In Model.Occurrences
                        @<tr>
                            <td>@(eo.CreatedAtFormat)</td>
                            <td>@(eo.CreatedBy)</td>
                            <td>@(eo.UserAgent)</td>
                        </tr>                                
                    Next
                </tbody>
            </table>

        </div>
    </div>
</div>
End If