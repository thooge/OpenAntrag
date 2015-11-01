Imports System.Net
Imports System.Web.Http
Imports Raven.Client.Util

Public Class NotificationsApiController
    Inherits ApiController

    Public Function GetTypeList() As IEnumerable(Of KeyValueObject)

        Dim values() As Integer = CType([Enum].GetValues(GetType(NotificationTypes)), Integer())

        Dim query = From v As Integer In values
                    Select New KeyValueObject With {.Key = v,
                                                    .Value = Notifications.GetTypeStringPlural(v)}

        Return query

    End Function

    Public Function GetLast(count As Integer) As IEnumerable(Of Notification)

        Return Notifications.GetItemsPage(-1, 1, count)

    End Function

    Public Function GetLastByType(typeId As Integer,
                                  count As Integer) As IEnumerable(Of Notification)

        Return Notifications.GetItemsPage(typeId, 1, count)

    End Function

End Class