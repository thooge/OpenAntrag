Public Class FederalStates

    Public Property Items As List(Of FederalState)

    Public Sub New()
        Dim xh As New XmlHelper()
        Me.Items = xh.GetInstance(Of FederalState)()
    End Sub

End Class

Public Class FederalState
    Implements IXMLClass

    Public Property Key As String
    Public Property [Name] As String

    Public Sub New()
    End Sub

    Public Sub New(ex As XElement)
        Me.New()
        With Me
            .Key = ex.Attribute("key").Value
            .Name = ex.Attribute("name").Value
        End With
    End Sub

    Public Function GetXElement(xD As XmlData) As XElement Implements IXMLClass.GetXElement

        Dim xE As New XElement(xD.ElementName)
        xE.SetAttributeValue("key", Me.Key.ToString)
        xE.SetAttributeValue("name", Me.Name.ToString)
        Return xE

    End Function

End Class