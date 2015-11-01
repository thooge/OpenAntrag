Imports System.Web

Public Class CustomHeaderModule
    Implements IHttpModule

    Public Sub Init(context As HttpApplication) Implements IHttpModule.Init
        AddHandler context.PreSendRequestHeaders, AddressOf OnPreSendRequestHeaders
    End Sub

    Public Sub Dispose() Implements IHttpModule.Dispose
    End Sub

    Private Sub OnPreSendRequestHeaders(sender As Object, e As EventArgs)
        'HttpContext.Current.Response.Headers.Remove("Server");
        ' Or you can set something funny
        HttpContext.Current.Response.Headers.[Set]("Server", "HAL 9000.l")
    End Sub
End Class
