Imports System.Web.Mvc
Imports Raven.Client

Public Class DocumentStoreController
    Inherits CommonController

    Public Property DocumentSession() As IDocumentSession
    Public Property AlreadySaved As Boolean = False

    Public Sub StoreAndSave(entity As Object)

        Me.DocumentSession.Store(entity)
        Me.DocumentSession.SaveChanges()
        Me.AlreadySaved = True

    End Sub

    Protected Overrides Sub OnActionExecuting(ByVal filterContext As ActionExecutingContext)

        If filterContext.IsChildAction Then
            Return
        End If

        Me.DocumentSession = DataDocumentStore.Session()

        MyBase.OnActionExecuting(filterContext)

    End Sub

    Protected Overrides Sub OnActionExecuted(ByVal filterContext As ActionExecutedContext)

        If filterContext.IsChildAction Then
            Return
        End If

        If Me.AlreadySaved = True Then
            'es wurde bereits gespeichert; Kennwert zurücksetzen
            Me.AlreadySaved = False
        Else
            If Me.DocumentSession IsNot Nothing AndAlso filterContext.Exception Is Nothing Then
                Me.DocumentSession.SaveChanges()
            End If
        End If

        Me.DocumentSession.Dispose()

        MyBase.OnActionExecuted(filterContext)

    End Sub

End Class