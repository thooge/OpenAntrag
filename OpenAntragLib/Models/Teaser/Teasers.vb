Imports System.Text

Public Class Teasers

    Public Property Items As List(Of Teaser)

    Public Sub New()
        Dim xh As New XmlHelper
        Me.Items = xh.GetInstance(Of Teaser)()

        Dim fed As New FederalStates

        For Each ts As Teaser In Me.Items
            If String.IsNullOrEmpty(ts.FederalKey) = False Then
                ts.Federal = (From fs As FederalState In fed.Items
                              Where fs.Key.ToUpper = ts.FederalKey.ToUpper
                              Select fs).First
            End If
        Next

    End Sub

    Public Function GetById(id As Integer) As Teaser

        Dim rep As Teaser = Nothing

        Dim query = From a As Teaser In Me.Items
                    Where a.ID = id
                    Select a

        If query.Count > 0 Then
            rep = query.First
        End If

        Return rep

    End Function

    Public Function GetByKey(keyTeaser As String) As Teaser

        Dim rep As Teaser = Nothing

        Dim query = From a As Teaser In Me.Items
                    Where a.Key = keyTeaser
                    Select a

        If query.Count > 0 Then
            rep = query.First
        End If

        Return rep

    End Function

    Public Shared Sub ReplaceStyleColor(ByRef rep As Teaser,
                                        ByVal stb As StringBuilder)

        stb.Replace("[ID]", rep.ID.ToString)
        stb.Replace("[KEY]", rep.Key)
        stb.Replace("[COLOR]", rep.Color)
        stb.Replace("[COLOR-RGB]", rep.ColorRGB)
        stb.Replace("[COLORBRIGHT]", rep.ColorBright)
        stb.Replace("[COLORTEXT]", rep.ColorText)

    End Sub

End Class
