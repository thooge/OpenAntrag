Imports System.ServiceModel.Syndication

Public Class CDataSyndicationContent
    Inherits TextSyndicationContent

    Public Sub New(content As TextSyndicationContent)
        MyBase.New(content)
    End Sub

    Protected Overrides Sub WriteContentsTo(writer As System.Xml.XmlWriter)
        writer.WriteCData(Text)
    End Sub

End Class
