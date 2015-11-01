Imports System.IO
Imports System.Web

Public Class XmlData

    Public Property Type As XmlDataType
    Public Property TypeName As String
    Public Property FileName() As String
    Public Property RootElementName() As String
    Public Property ElementName() As String

    Public ReadOnly Property RelativePath() As String
        Get
            Return "~/App_Data/" & Me.FileName
        End Get
    End Property

    Public ReadOnly Property MappedPath() As String
        Get
            Return HttpContext.Current.Server.MapPath(Me.RelativePath)
        End Get
    End Property

End Class

Public Enum XmlDataType 'Namen referenzieren auf den Single-Objekttyp
    Representations = 0
    FeedbackType = 1
    FeedbackStatus = 2
    GovernmentalLevels = 3
    FederalStates = 4
    GroupType = 5
    Teaser = 6
    ProcessStepDefinition = 7
End Enum

Public Class XmlHelper

    Private _XmlInfo As New List(Of XmlData)

    Public Sub New()

        _XmlInfo.Add(New XmlData With {
                     .Type = XmlDataType.Representations,
                     .TypeName = "Representation",
                     .FileName = "Representations.xml",
                     .RootElementName = "representations",
                     .ElementName = "item"})

        _XmlInfo.Add(New XmlData With {
                     .Type = XmlDataType.FeedbackType,
                     .TypeName = "FeedbackType",
                     .FileName = "FeedbackTypes.xml",
                     .RootElementName = "types",
                     .ElementName = "type"})

        _XmlInfo.Add(New XmlData With {
                     .Type = XmlDataType.FeedbackStatus,
                     .TypeName = "FeedbackStatusCode",
                     .FileName = "FeedbackStatusCodes.xml",
                     .RootElementName = "statuscodes",
                     .ElementName = "statuscode"})

        _XmlInfo.Add(New XmlData With {
                     .Type = XmlDataType.GroupType,
                     .TypeName = "GroupType",
                     .FileName = "GroupTypes.xml",
                     .RootElementName = "types",
                     .ElementName = "type"})

        _XmlInfo.Add(New XmlData With {
                     .Type = XmlDataType.GovernmentalLevels,
                     .TypeName = "GovernmentalLevel",
                     .FileName = "GovernmentalLevels.xml",
                     .RootElementName = "levels",
                     .ElementName = "level"})

        _XmlInfo.Add(New XmlData With {
                     .Type = XmlDataType.FederalStates,
                     .TypeName = "FederalState",
                     .FileName = "FederalStates.xml",
                     .RootElementName = "federalstates",
                     .ElementName = "federalstate"})

        _XmlInfo.Add(New XmlData With {
                     .Type = XmlDataType.Teaser,
                     .TypeName = "Teaser",
                     .FileName = "Teaser.xml",
                     .RootElementName = "teaser",
                     .ElementName = "item"})

        _XmlInfo.Add(New XmlData With {
                     .Type = XmlDataType.ProcessStepDefinition,
                     .TypeName = "ProcessStepDefinition",
                     .FileName = "ProcessStepDefinitions.xml",
                     .RootElementName = "steps",
                     .ElementName = "step"})

    End Sub

    Public Function ConvertInstance(Of T As IXMLClass)(lst As List(Of T)) As XElement

        Dim typ As Type = GetType(T)
        Dim strTypename As String = typ.Name

        Dim query = From xd As XmlData In _XmlInfo
                    Where xd.TypeName = strTypename
                    Select xd

        If query.Count > 0 Then
            Dim xD As XmlData = query.First
            Dim xE As New XElement(xd.RootElementName)

            For Each m As T In lst
                Dim xNew As XElement = m.GetXElement(xd)
                xE.Add(xNew)
            Next

            Return xE
        Else
            Return Nothing
        End If

    End Function

    Public Function GetInstance(Of T As IXMLClass)() As List(Of T)

        Dim typ As Type = GetType(T)
        Dim strTypename As String = typ.Name

        Dim query = From xd As XmlData In _XmlInfo
                    Where xd.TypeName = strTypename
                    Select xd

        If query.Count > 0 Then
            Dim xd As XmlData = query.First
            Dim xE As XElement = GetXml(xd.Type)

            Dim lst As New List(Of T)
            Dim querx = From ex In xE.Elements(xd.ElementName)
                        Select ex

            For Each ex In querx
                Dim cT As T = Activator.CreateInstance(typ, {ex})
                lst.Add(cT)
            Next

            Return lst
        Else
            Return Nothing
        End If

    End Function

    Public Function GetXmlDataFromType(ByVal intType As XmlDataType) As XmlData

        Return (From xd As XmlData In _XmlInfo
                Where xd.Type = intType
                Select xd).First()

    End Function

    Public Function GetXml(ByVal intType As XmlDataType) As XElement

        Dim cXmlData As XmlData = GetXmlInfo(intType)
        Return SetupXml(cXmlData)

    End Function

    Public Function GetXmlInfo(ByVal intType As XmlDataType) As XmlData

        Dim cXmlData As XmlData = Nothing

        Dim query = From xd As XmlData In _XmlInfo
                    Where xd.Type = intType
                    Select xd

        If query.Count > 0 Then
            cXmlData = query.First
        End If

        Return cXmlData

    End Function

    Public Function SetupXml(cXmlData As XmlData) As XElement

        Dim xE As XElement = XElement.Load(cXmlData.MappedPath)

        Dim strTemplateFile As String = HttpContext.Current.Server.MapPath("~/App_Data/XmlTemplates/" & cXmlData.FileName)

        If File.Exists(strTemplateFile) = True Then

            Dim xT As XElement = XElement.Load(strTemplateFile)

            Dim bolChanged As Boolean = False
            DoMerge(bolChanged, xE, xT) 'rekursiver Aufruf

            If bolChanged = True Then
                Call BackupXml(cXmlData.Type)
                Call SaveXml(xE, cXmlData.Type)
            End If

        End If

        Return xE

    End Function

    Public Function BackupXml(ByVal intType As XmlDataType) As Boolean

        Dim cXmlData As XmlData = GetXmlInfo(intType)
        Dim strBackupFile As String = HttpContext.Current.Server.MapPath("~/App_Data/XmlBackup/") & cXmlData.FileName

        Try
            Dim fi As New FileInfo(cXmlData.MappedPath)
            If fi.Exists = True Then
                fi.CopyTo(strBackupFile, True)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function SaveXml(ByVal xE As XElement,
                            ByVal intType As XmlDataType) As Boolean

        Dim cXmlData As XmlData = GetXmlInfo(intType)

        If xE.Name.LocalName.ToLower <> cXmlData.RootElementName.ToLower Then
            Throw New Exception("Falsche XML-Struktur")
        End If

        Try
            xE.Save(cXmlData.MappedPath)
            Return True
        Catch ex As Exception
            Throw New Exception("Fehler beim Speichern der XML-Struktur")
        End Try

    End Function

    Public Function GetMaxID(ByVal intType As XmlDataType) As Integer

        Dim intMaxID As Integer = 0
        Dim xE As XElement = GetXml(intType)
        Dim xD As XmlData = GetXmlDataFromType(intType)

        Try
            intMaxID = Aggregate node In xE.Elements(xD.ElementName)
                       Into Max(CType(node.@id, Integer))
        Catch ex As Exception
        End Try

        Return intMaxID

    End Function

#Region "Helper Methods"

    Private Shared Sub DoMerge(ByRef bolChanged As Boolean, xE As XElement, xT As XElement)

        For Each node In xE.Elements

            'Attribute
            If XmlHelper.MergeTemplateAttributes(node, xT.Elements(node.Name).First) = True Then bolChanged = True
            'Elemente
            If XmlHelper.MergeTemplateElements(node, xT.Elements(node.Name).First) = True Then bolChanged = True

            If node.HasElements = True Then
                DoMerge(bolChanged, node, xT.Elements(node.Name).First)
            End If

        Next

    End Sub

    Private Shared Function MergeTemplateAttributes(ByVal xE As XElement, ByVal xTemplate As XElement) As Boolean

        Dim bolChanged As Boolean = False

        Dim CurrentAttributeNames = From attr In xE.Attributes Select attr.Name
        For Each xAttr In xTemplate.Attributes
            If Not CurrentAttributeNames.Contains(xAttr.Name) Then
                xE.Add(xAttr)
                bolChanged = True
            End If
        Next

        Dim TemplateAttributeNames = From attr In xTemplate.Attributes Select attr.Name
        For Each xAttr In xE.Attributes
            If Not TemplateAttributeNames.Contains(xAttr.Name) Then
                xAttr.Remove()
                bolChanged = True
            End If
        Next

        Return bolChanged

    End Function

    Private Shared Function MergeTemplateElements(ByVal xE As XElement, ByVal xTemplate As XElement) As Boolean

        Dim bolChanged As Boolean = False

        Dim CurrentElementNames = From elm In xE.Elements Select elm.Name
        For Each xElm In xTemplate.Elements
            If Not CurrentElementNames.Contains(xElm.Name) Then
                xE.Add(xElm)
                bolChanged = True
            End If
        Next

        Dim TemplateElementNames = From elm In xTemplate.Elements Select elm.Name
        For Each xElm In xE.Elements
            If Not TemplateElementNames.Contains(xElm.Name) Then
                xElm.Remove()
                bolChanged = True
            End If
        Next

        Return bolChanged

    End Function

#End Region

End Class
