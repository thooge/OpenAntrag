Public Class ProcessStepDefinitions

    Public Property Items As List(Of ProcessStepDefinition)

    Public Sub New()
        Dim xh As New XmlHelper()
        Me.Items = xh.GetInstance(Of ProcessStepDefinition)()
    End Sub

End Class

Public Class ProcessStepDefinition
    Implements IXMLClass

    Public Property Key As String
    Public Property Icon As String
    Public Property Color As String
    Public Property Caption As String
    Public Property ShortCaption As String

    Public Sub New()
    End Sub

    Public Sub New(ex As XElement)
        Me.New()
        With Me
            .Key = ex.Attribute("key").Value
            .Icon = ex.Attribute("icon").Value
            .Color = ex.Attribute("color").Value
            .Caption = ex.Attribute("caption").Value
            .ShortCaption = ex.Attribute("short-caption").Value
        End With
    End Sub

    Public Function GetXElement(xD As XmlData) As XElement Implements IXMLClass.GetXElement

        Dim xE As New XElement(xD.ElementName)
        xE.SetAttributeValue("key", Me.Key.ToString)
        xE.SetAttributeValue("icon", Me.Icon.ToString)
        xE.SetAttributeValue("color", Me.Color.ToString)
        xE.SetAttributeValue("caption", Me.Caption.ToString)
        xE.SetAttributeValue("short-caption", Me.ShortCaption.ToString)
        Return xE

    End Function

End Class
