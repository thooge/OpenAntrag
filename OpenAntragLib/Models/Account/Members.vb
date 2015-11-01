Imports Raven.Client
Imports System.Web
Imports System.Web.Security

Public Class Members

#Region "Shared Methods"

    Public Shared Function GetMembers() As List(Of Member)

        Dim lst As List(Of Member)

        Using ds As IDocumentSession = DataDocumentStore.Session()

            Dim query = From m As Member In ds.Query(Of Member)()
                        Select m

            'http://stackoverflow.com/questions/10048943/proper-way-to-retrieve-more-than-128-documents-with-ravendb

            lst = query.Take(1024).ToList()

        End Using

        Return lst

    End Function

    Public Shared Function GetCurrentMember() As Member

        Dim model As New Member

        If HttpContext.Current.User.Identity.IsAuthenticated = True Then
            Using ds As IDocumentSession = DataDocumentStore.Session()

                Dim query = From m As Member In ds.Query(Of Member)()
                            Where m.UserName = HttpContext.Current.User.Identity.Name
                            Select m

                If query.Count > 0 Then
                    model = query.First
                End If

            End Using
        End If

        Return model

    End Function

    Public Shared Function GetMember(ByVal strUserName As String) As Member

        Dim model As Member = Nothing

        Using ds As IDocumentSession = DataDocumentStore.Session()

            Dim query = From m As Member In ds.Query(Of Member)()
                        Where m.UserName = strUserName
                        Select m

            If query.Count > 0 Then
                model = query.First
            End If

        End Using

        Return model

    End Function

    Public Shared Function GetMemberByUserKey(ByVal strUserKey As String) As Member

        Dim model As Member = Nothing

        Using ds As IDocumentSession = DataDocumentStore.Session()

            Dim query = From m As Member In ds.Query(Of Member)()
                        Where m.UserKey = strUserKey
                        Select m

            If query.Count > 0 Then
                model = query.First
            End If

        End Using

        Return model

    End Function

    Public Shared Function GetMemberByApiKey(ByVal strApiKey As String) As Member

        Dim model As Member = Nothing

        Using ds As IDocumentSession = DataDocumentStore.Session()

            Dim query = From m As Member In ds.Query(Of Member)()
                        Where m.APIKey = strApiKey
                        Select m

            If query.Count > 0 Then
                model = query.First
            End If

        End Using

        Return model

    End Function

    Public Shared Function EnsureMember(mu As MembershipUser) As Member

        Dim mb As Member = GetMember(mu.UserName)

        If mb Is Nothing Then
            mb = New Member With {.UserKey = mu.ProviderUserKey.ToString,
                                  .UserName = mu.UserName,
                                  .Mail = mu.Email}
            SaveMember(mb)
        End If

        Return mb

    End Function

    Public Shared Sub SaveMember(model As Member)

        Using ds As IDocumentSession = DataDocumentStore.Session()
            ds.Store(model)
            ds.SaveChanges()
        End Using

    End Sub

#End Region

End Class
