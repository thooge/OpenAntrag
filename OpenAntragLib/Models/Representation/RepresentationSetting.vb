Imports System.ComponentModel.DataAnnotations
Imports Raven.Client
Imports System.Web

Public Class RepresentationSetting
    Inherits RavenModelBase

    Public Property Key As String
    Public Property HasContactPossibility As Boolean = False

    Public Sub New()
        Me.CreatedAt = Format(DateTime.Now, "dd.MM.yyyy HH:mm:ss")
    End Sub

End Class

Public Class RepresentationSettings

    Public Shared Function GetByKey(key As String) As RepresentationSetting

        Dim model As RepresentationSetting

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = ds.Query(Of RepresentationSetting)() _
                        .Where(Function(s) s.Key = key)

            If query.Count > 0 Then
                model = query.First()
            Else
                model = New RepresentationSetting()
                model.Key = key
                model.CreatedBy = HttpContext.Current.User.Identity.Name
                ds.Store(model)
                ds.SaveChanges()
            End If

        End Using

        Return model

    End Function

End Class