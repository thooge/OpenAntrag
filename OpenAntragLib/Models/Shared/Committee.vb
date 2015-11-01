Imports Raven.Imports

Public Class Committee

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Key_Representation As String

    <Newtonsoft.Json.JsonIgnore()>
    Public Property ID As Integer

    Public Property Key As String
    Public Property [Name] As String
    Public Property Caption As String
    Public Property Url As String

    Public Sub New()
    End Sub

    Public Sub New(ex As XElement, keyRepresentation As String)
        Me.New()
        With Me
            .Key_Representation = keyRepresentation
            .ID = CType(ex.Attribute("id").Value, Integer)
            .Key = ex.Attribute("key").Value
            .[Name] = ex.Attribute("name").Value
            .Caption = ex.Attribute("caption").Value
            .Url = ex.Attribute("url").Value
        End With
    End Sub

    Public Function GetXElement() As XElement

        Dim ex As New XElement("item")
        ex.SetAttributeValue("id", Me.ID.ToString)
        ex.SetAttributeValue("key", Me.Key.ToString)
        ex.SetAttributeValue("name", Me.[Name].ToString)
        ex.SetAttributeValue("caption", Me.Caption.ToString)
        ex.SetAttributeValue("url", Me.Url.ToString)

        Return ex

    End Function
End Class
