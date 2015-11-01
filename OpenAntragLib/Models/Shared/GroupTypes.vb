Public Class GroupTypes

    Public Property Items As List(Of GroupType)

    Public Sub New()
        Dim xh As New XmlHelper()
        Me.Items = xh.GetInstance(Of GroupType)()
    End Sub

End Class

Public Class GroupType
    Implements IXMLClass

    Public Property ID As Integer
    Public Property [Name] As String
    Public Property NameGen As String
    Public Property Common As String
    Public Property Color As String

    Public Sub New()
    End Sub

    Public Sub New(ex As XElement)
        Me.New()
        With Me
            .ID = CType(ex.Attribute("id").Value, Integer)
            .Name = ex.Attribute("name").Value
            .NameGen = ex.Attribute("name-gen").Value
            .Common = ex.Attribute("common").Value
            .Color = ex.Attribute("color").Value
        End With
    End Sub

    Public Function GetXElement(xD As XmlData) As XElement Implements IXMLClass.GetXElement

        Dim xE As New XElement(xD.ElementName)
        xE.SetAttributeValue("id", Me.ID.ToString)
        xE.SetAttributeValue("name", Me.Name.ToString)
        xE.SetAttributeValue("name-gen", Me.NameGen.ToString)
        xE.SetAttributeValue("color", Me.Color.ToString)
        Return xE

    End Function

End Class
