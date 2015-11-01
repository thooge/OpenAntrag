Imports Raven.Client
Imports System.Text

Public Class Representations

    Public Enum StatusConjuction
        Inactive = 0
        Active = 1
        ViewOnly = 2
        Ended = 4
    End Enum

    Public Property Items As List(Of Representation)

    Public Sub New()

        Dim xh As New XmlHelper
        Me.Items = xh.GetInstance(Of Representation)()

        For Each rp As Representation In Me.Items

            If String.IsNullOrEmpty(rp.FederalKey) = False Then
                rp.Federal = (From fs As FederalState In GlobalData.FederalStates.Items
                              Where fs.Key.ToUpper = rp.FederalKey.ToUpper
                              Select fs).First

            End If

            rp.piratenmandate = (From pm As piratenmandate.Item In GlobalData.piratenmandate.Items
                                 Where pm.OpenAntragKey = rp.Key
                                 Select pm).FirstOrDefault()

            If rp.piratenmandate IsNot Nothing Then
                rp.piratenmandate.MapUrl = rp.MapUrl
            End If

        Next

    End Sub

    Public Function GetById(id As Integer) As Representation

        Dim rep As Representation = Nothing

        Dim query = From a As Representation In Me.Items
                    Where a.ID = id
                    Select a

        If query.Count > 0 Then
            rep = query.First
        End If

        Return rep

    End Function

    Public Function GetByKey(keyRepresentation As String) As Representation

        Dim rep As Representation = Nothing

        Dim query = From a As Representation In Me.Items
                    Where a.Key = keyRepresentation
                    Select a

        If query.Count > 0 Then
            rep = query.First
        End If

        Return rep

    End Function

    Public Function GetByList(arrKeyList As String()) As List(Of Representation)

        Return (From s In arrKeyList Select GetByKey(s)).ToList()

    End Function

    Public Shared Function GetNewApiKey() As String

        Dim x As New RandomKeyGenerator(42)
        Dim sx As String = x.Generate()
        Return sx

    End Function

    Public Function GetRepresentatives() As List(Of Representative)

        Dim lst As New List(Of Representative)

        For Each rp As Representation In Me.Items.Where(Function(x) (x.Status And Representations.StatusConjuction.Active) > 0)
            lst.AddRange(From rv In rp.Representatives Where rv.HasPortraitImage = True And rv.Party.ToLower.Contains("piratenpartei"))
        Next

        Return lst

    End Function

    Public Shared Sub EnsureRepresentationClone(rep As Representation)

        Dim model As RepresentationClone
        Dim bSave As Boolean = False

        Using ds As IDocumentSession = DataDocumentStore.Session

            Dim query = From m As RepresentationClone In ds.Query(Of RepresentationClone)()
                        Where m.OriginalId = rep.ID
                        Select m

            If query.Count > 0 Then
                model = query.First

                If Not model.Label = rep.Label Then model.Label = rep.Label : bSave = True
                If Not model.ApiKey = rep.ApiKey Then model.ApiKey = rep.ApiKey : bSave = True
                If Not model.Color = rep.Color Then model.Color = rep.Color : bSave = True
                If Not model.Name = rep.Name Then model.Name = rep.Name : bSave = True
                If Not model.Name2 = rep.Name2 Then model.Name2 = rep.Name2 : bSave = True
                If Not model.Level = rep.Level Then model.Level = rep.Level : bSave = True
                If Not model.FederalKey = rep.FederalKey Then model.FederalKey = rep.FederalKey : bSave = True
                If Not model.GroupType = rep.GroupType Then model.GroupType = rep.GroupType : bSave = True
                If Not model.GroupName = rep.GroupName Then model.GroupName = rep.GroupName : bSave = True
                If Not model.Link = rep.Link Then model.Link = rep.Link : bSave = True
                If Not model.Twitter = rep.Twitter Then model.Twitter = rep.Twitter : bSave = True
                If Not model.Phone = rep.Phone Then model.Phone = rep.Phone : bSave = True
                If Not model.Mail = rep.Mail Then model.Mail = rep.Mail : bSave = True
                If Not model.InfoMail = rep.InfoMail Then model.InfoMail = rep.InfoMail : bSave = True
                If Not model.MapUrl = rep.MapUrl Then model.MapUrl = rep.MapUrl : bSave = True

            Else
                bSave = True
                model = New RepresentationClone With {.OriginalId = rep.ID,
                                                      .Key = rep.Key,
                                                      .ApiKey = rep.ApiKey,
                                                      .Color = rep.Color,
                                                      .Name = rep.Name,
                                                      .Name2 = rep.Name2,
                                                      .Level = rep.Level,
                                                      .FederalKey = rep.FederalKey,
                                                      .GroupType = rep.GroupType,
                                                      .GroupName = rep.GroupName,
                                                      .Link = rep.Link,
                                                      .Twitter = rep.Twitter,
                                                      .Mail = rep.Mail,
                                                      .InfoMail = rep.InfoMail,
                                                      .MapUrl = rep.MapUrl}
            End If

            If bSave = True Then
                ds.Store(model)
                ds.SaveChanges()
            End If

        End Using


    End Sub

    Public Shared Sub ReplaceStyleColor(ByRef rep As Representation,
                                        ByVal stb As StringBuilder)

        stb.Replace("[ID]", rep.ID.ToString)
        stb.Replace("[KEY]", rep.Key)
        stb.Replace("[COLOR]", rep.Color)
        stb.Replace("[COLOR-RGB]", rep.ColorRGB)
        stb.Replace("[COLORBRIGHT]", rep.ColorBright)
        stb.Replace("[COLORTEXT]", rep.ColorText)

    End Sub

End Class
