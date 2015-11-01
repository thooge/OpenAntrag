Public Class FeedbackTypes

    Public Property Items As List(Of FeedbackType)

    Public Sub New()
        Dim xh As New XmlHelper()
        Me.Items = xh.GetInstance(Of FeedbackType)()
    End Sub

End Class

Public Class FeedbackType
    Implements IXMLClass

    Public Property ID As Integer
    Public Property Key As String
    Public Property [Name] As String
    Public Property Icon As String
    Public Property Voting As Boolean
    Public Property Color As String

    Public Sub New()
    End Sub

    Public Sub New(ex As XElement)
        Me.New()
        With Me
            .ID = CType(ex.Attribute("id").Value, Integer)
            .Key = ex.Attribute("key").Value
            .Name = ex.Attribute("name").Value
            .Icon = ex.Attribute("icon").Value
            .Voting = CType(ex.Attribute("voting").Value, Boolean)
            .Color = ex.Attribute("color").Value
        End With
    End Sub

    Public Function GetXElement(xD As XmlData) As XElement Implements IXMLClass.GetXElement

        Dim xE As New XElement(xD.ElementName)
        xE.SetAttributeValue("id", Me.ID.ToString)
        xE.SetAttributeValue("key", Me.Key.ToString)
        xE.SetAttributeValue("name", Me.Name.ToString)
        xE.SetAttributeValue("icon", Me.Icon.ToString)
        xE.SetAttributeValue("voting", Me.Voting.ToString)
        xE.SetAttributeValue("color", Me.Color.ToString)
        Return xE

    End Function

End Class
