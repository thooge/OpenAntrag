Imports System.Web.Mvc

Public Class CustomErrorInfo
    Inherits HandleErrorInfo

    Public Enum OriginEnum
        Undefined
        Exception
        Redirect
    End Enum

    Public Sub New()
        MyBase.New(New Exception(), "Error", "Index")

        Me.Origin = OriginEnum.Undefined

    End Sub

    Public Sub New(errorMessage As String,
                   origin As OriginEnum,
                   code As Integer)

        MyBase.New(New Exception(), "Error", "Index")

        Me.ErrorMessage = errorMessage
        Me.Origin = origin
        Me.Code = code

    End Sub

    Public Sub New(errorMessage As String,
                   referrerUrl As String,
                   origin As OriginEnum,
                   code As Integer,
                   exception As Exception,
                   controllerName As String,
                   actionName As String)

        MyBase.New(exception, controllerName, actionName)

        Me.ErrorMessage = errorMessage
        Me.ReferrerUrl = referrerUrl
        Me.Origin = origin
        Me.Code = code

    End Sub

    Public Property ErrorMessage() As String
    Public Property ReferrerUrl() As String
    Public Property Origin() As OriginEnum
    Public Property Code() As Integer

End Class
