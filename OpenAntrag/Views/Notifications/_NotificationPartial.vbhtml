@Imports OpenAntrag
@ModelType List(Of Notification)

@Code    
    Dim strUpperText As String
    Dim strText As String
    Dim intDoubleQuote As Integer
    
    For Each nf As Notification In Model
        intDoubleQuote = InStr(nf.Text, ": ")
        If intDoubleQuote > 0 Then
            strUpperText = Left(nf.Text, intDoubleQuote - 1)
            strText = Mid(nf.Text, intDoubleQuote + 1, nf.Text.Length - intDoubleQuote)
        Else
            strUpperText = Nothing
            strText = nf.Text
        End If
    @<li data-time="@(nf.CreatedAt)" style="border-color: @(nf.NotificationTypeColor)">
        <em>@(nf.NotificationTypeString)</em>
        <small></small>
        @If String.IsNullOrEmpty(nf.Url) = True Then
            @<h4>@(nf.Title)</h4>
        Else
            @<a href="@(nf.Url)"><h4>@(nf.Title)</h4></a>
        End If
        @If String.IsNullOrEmpty(strUpperText) = False Then
            @<strong>@(strUpperText)</strong>        
        End If
        <p>@(strText)</p>
    </li>
    Next
End Code
