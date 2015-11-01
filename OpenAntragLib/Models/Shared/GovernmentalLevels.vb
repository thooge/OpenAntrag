Public Class GovernmentalLevels

    Public Property Items As List(Of GovernmentalLevel)

    Public Sub New()
        Dim xh As New XmlHelper()
        Me.Items = xh.GetInstance(Of GovernmentalLevel)()
    End Sub

End Class

Public Class GovernmentalLevel
    Implements IXMLClass

    Public Property ID As Integer
    Public Property [Name] As String

    Public Sub New()
    End Sub

    Public Sub New(ex As XElement)
        Me.New()
        With Me
            .ID = CType(ex.Attribute("id").Value, Integer)
            .Name = ex.Attribute("name").Value
        End With
    End Sub

    Public Function GetXElement(xD As XmlData) As XElement Implements IXMLClass.GetXElement

        Dim xE As New XElement(xD.ElementName)
        xE.SetAttributeValue("id", Me.ID.ToString)
        xE.SetAttributeValue("name", Me.Name.ToString)
        Return xE

    End Function

End Class
