Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Drawing

Public Class Representation
    Implements IXMLClass

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property ID As Integer

    'Private _piratenmandate As piratenmandate.Item
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property piratenmandate As piratenmandate.Item
    '    Get
    '        If _piratenmandate Is Nothing Then
    '            _piratenmandate = (From pm As piratenmandate.Item In GlobalData.piratenmandate.Items
    '                               Where pm.OpenAntragKey = Me.Key
    '                               Select pm).FirstOrDefault()
    '        End If
    '        Return _piratenmandate
    '    End Get
    '    Set(value As piratenmandate.Item)
    '        _piratenmandate = value
    '    End Set
    'End Property

    Public Property Status As Representations.StatusConjuction

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property StatusName() As String
        Get
            Dim s As String = [Enum].GetName(GetType(Representations.StatusConjuction), Me.Status)
            If String.IsNullOrEmpty(s) = False Then
                Return s.ToLower()
            Else
                Return ""
            End If
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property IsActive As Boolean
        Get
            Return (Me.Status And Representations.StatusConjuction.Active) = Representations.StatusConjuction.Active
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property IsViewOnly As Boolean
        Get
            Return (Me.Status And Representations.StatusConjuction.ViewOnly) = Representations.StatusConjuction.ViewOnly
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property IsTest As Boolean

    Public Property Key As String
    Public Property Label As String
    Public Property LabelArticle As String = ""

    Public ReadOnly Property LabelWithArticle As String
        Get
            If String.IsNullOrEmpty(Me.LabelArticle) = False Then
                Return String.Concat(Me.LabelArticle, " ", Me.Label)
            Else
                Return Me.Label
            End If
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property ApiKey As String

    Public Property Color As String

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property ColorRGB As String
        Get
            Dim cColor As Color = ColorTranslator.FromHtml(Me.Color)
            Dim stb As New StringBuilder
            stb.Append(cColor.R.ToString).Append(",")
            stb.Append(cColor.G.ToString).Append(",")
            stb.Append(cColor.B.ToString)
            Return stb.ToString()
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property ColorBright As String
        Get
            Dim cColor As Color = ColorTranslator.FromHtml(Me.Color)
            Dim cBrightColor As Color = cColor.ChangeColorBrightness(0.25)
            Return ColorTranslator.ToHtml(cBrightColor)
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property ColorText As String
        Get
            Dim cColor As Color = ColorTranslator.FromHtml(Me.Color)
            Dim cBrightColor As Color = cColor.ChangeColorBrightness(-0.15)
            Return ColorTranslator.ToHtml(cBrightColor)
        End Get
    End Property

    Public Property [Name] As String
    Public Property [Name2] As String
    Public Property Level As Integer

    Public Property FederalKey As String
    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property Federal As FederalState
    Public ReadOnly Property FederalName As String 'nur für API
        Get
            Return Me.Federal.Name
        End Get
    End Property

    Public Property GroupType As Integer
    Public Property GroupName As String
    Public Property Link As String
    Public Property Twitter As String
    Public Property Phone As String
    Public Property Mail As String
    Public Property InfoMail As String
    Public Property MapUrl As String

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property Representatives As List(Of Representative)

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property HasCommittees As Boolean = True

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property Committees As List(Of Committee)

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property ProcessSteps As List(Of ProcessStep)

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property GroupTypeCaption As String
        Get
            Dim strRetVal As String = ""
            Try
                Dim query = From gt As GroupType In GlobalData.GroupTypes.Items
                            Where gt.ID = Me.GroupType
                            Select gt

                If query.Count > 0 Then
                    strRetVal = query.First.Name
                End If
            Catch ex As Exception

            End Try
            Return strRetVal
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property GroupTypeGen As String
        Get
            Dim strRetVal As String = ""
            Try
                Dim query = From gt As GroupType In GlobalData.GroupTypes.Items
                            Where gt.ID = Me.GroupType
                            Select gt

                If query.Count > 0 Then
                    strRetVal = query.First.NameGen
                End If
            Catch ex As Exception

            End Try
            Return strRetVal
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property GroupTypeObject As GroupType
        Get
            Dim objRetVal As GroupType = Nothing
            Try
                Dim query = From gt As GroupType In GlobalData.GroupTypes.Items
                            Where gt.ID = Me.GroupType
                            Select gt

                If query.Count > 0 Then
                    objRetVal = query.First
                End If
            Catch ex As Exception

            End Try
            Return objRetVal
        End Get
    End Property

    Public ReadOnly Property FullUrl As String
        Get
            Dim stb As New StringBuilder()
            stb.Append("http://").Append(HttpContext.Current.Request.Url.Authority)
            stb.Append("/").Append(Me.Key)
            Return stb.ToString
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property HasLogoImage As Boolean
        Get
            Return (Me.LogoImage IsNot Nothing)
        End Get
    End Property

    Public ReadOnly Property LogoImage As String
        Get
            Dim stbPath As New StringBuilder()
            stbPath.Append("/Content/Representations/")
            stbPath.Append(Me.Key).Append("/")

            Dim di As New DirectoryInfo(HttpContext.Current.Server.MapPath(stbPath.ToString))
            Dim fi As FileInfo() = di.GetFiles("logo.*")
            If fi.Count > 0 Then
                Return String.Concat(stbPath.ToString, fi(0).Name)
            Else : Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property LogoImagePage As String
        Get
            Dim stbPath As New StringBuilder()
            stbPath.Append("/Content/Representations/")
            stbPath.Append(Me.Key).Append("/")

            Dim di As New DirectoryInfo(HttpContext.Current.Server.MapPath(stbPath.ToString))
            Dim fi As FileInfo() = di.GetFiles("logo-page.*")
            If fi.Count > 0 Then
                Return String.Concat(stbPath.ToString, fi(0).Name)
            Else : Return Me.LogoImage
            End If
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property FraktionInfo As HtmlString
        Get
            Dim hs As New HtmlString("")

            Dim stbPath As New StringBuilder()
            stbPath.Append("/Content/Representations/")
            stbPath.Append(Me.Key).Append("/Info/")

            Dim di As New DirectoryInfo(HttpContext.Current.Server.MapPath(stbPath.ToString))
            Dim fi As FileInfo() = di.GetFiles("fraktion.info")
            If fi.Count > 0 Then
                Dim sr As New StreamReader(fi(0).FullName, Encoding.UTF8)
                Dim strContent As String = sr.ReadToEnd()
                sr.Close()
                hs = New HtmlString(strContent)
            End If

            Return hs
        End Get
    End Property

    <CSVIgnore>
    Public ReadOnly Property FraktionInfoHtml() As String
        Get
            Return Me.FraktionInfo.ToHtmlString
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property ParlamentInfo As HtmlString
        Get
            Dim hs As New HtmlString("")

            Dim stbPath As New StringBuilder()
            stbPath.Append("/Content/Representations/")
            stbPath.Append(Me.Key).Append("/Info/")

            Dim di As New DirectoryInfo(HttpContext.Current.Server.MapPath(stbPath.ToString))
            Dim fi As FileInfo() = di.GetFiles("parlament.info")
            If fi.Count > 0 Then
                Dim sr As New StreamReader(fi(0).FullName, Encoding.UTF8)
                Dim strContent As String = sr.ReadToEnd()
                sr.Close()
                hs = New HtmlString(strContent)
            End If

            Return hs
        End Get
    End Property

    <CSVIgnore>
    Public ReadOnly Property ParlamentInfoHtml() As String
        Get
            Return Me.ParlamentInfo.ToHtmlString
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ex As XElement)
        Me.New()
        With Me
            .ID = CType(ex.Attribute("id").Value, Integer)
            .Status = CType(ex.Attribute("status").Value, Integer)
            .IsTest = (CType(ex.Attribute("id").Value, Integer) >= 999)
            .Key = ex.Attribute("key").Value
            .Label = ex.Attribute("label").Value
            If ex.Attribute("label-article") IsNot Nothing Then
                .LabelArticle = ex.Attribute("label-article").Value
            End If
            .ApiKey = ex.Attribute("api-key").Value
            .Color = ex.Attribute("color").Value
            .[Name] = ex.Attribute("name").Value
            .[Name2] = ex.Attribute("name2").Value
            .Level = CType(ex.Attribute("level").Value, Integer)
            .FederalKey = ex.Attribute("federal").Value
            .GroupType = CType(ex.Attribute("group-type").Value, Integer)
            .GroupName = ex.Attribute("group-name").Value
            .Link = ex.Attribute("link").Value
            .Twitter = ex.Attribute("twitter").Value.Replace("@", "")
            .Phone = ex.Attribute("phone").Value
            .Mail = ex.Attribute("mail").Value
            .InfoMail = ex.Attribute("info-mail").Value
            If ex.Attribute("map-url") IsNot Nothing Then
                .MapUrl = ex.Attribute("map-url").Value
            End If

            Me.Representatives = New List(Of Representative)
            For Each xM As XElement In ex.Element("representatives").Elements("item")
                Me.Representatives.Add(New Representative(xM, ex.Attribute("key").Value))
            Next

            Me.Committees = New List(Of Committee)
            If ex.Element("committees").Attribute("has-committees") IsNot Nothing Then
                .HasCommittees = CType(ex.Element("committees").Attribute("has-committees").Value, Boolean)
            End If
            For Each xM As XElement In ex.Element("committees").Elements("item")
                Me.Committees.Add(New Committee(xM, ex.Attribute("key").Value))
            Next

            Me.ProcessSteps = New List(Of ProcessStep)
            For Each xS As XElement In ex.Element("process").Elements("step")
                Me.ProcessSteps.Add(New ProcessStep(Me, xS))
            Next
            '--- 
            For Each xS As XElement In ex.Element("process").Elements("step")
                Dim parentStep As ProcessStep = (From p As ProcessStep In Me.ProcessSteps
                                                 Where p.ID = CType(xS.Attribute("id").Value, Integer)
                                                 Select p).First

                For Each xN As XElement In xS.Element("next").Elements("step")
                    Dim nextStep = (From p As ProcessStep In Me.ProcessSteps
                                    Where p.ID = CType(xN.Attribute("id").Value, Integer)
                                    Select p).First

                    If parentStep.NextSteps Is Nothing Then parentStep.NextSteps = New List(Of ProcessStep)
                    parentStep.NextSteps.Add(nextStep.CloneForNextStep)
                Next
            Next

        End With
    End Sub

    Public Function GetXElement(xD As XmlData) As XElement Implements IXMLClass.GetXElement

        Dim xE As New XElement(xD.ElementName)
        xE.SetAttributeValue("id", Me.ID.ToString)
        xE.SetAttributeValue("status", Me.Status.ToString)
        xE.SetAttributeValue("key", Me.Key.ToString)
        xE.SetAttributeValue("label", Me.Label.ToString)
        If String.IsNullOrEmpty(Me.LabelArticle) = False Then
            xE.SetAttributeValue("label-article", Me.LabelArticle.ToString)
        End If
        xE.SetAttributeValue("api-key", Me.ApiKey.ToString)
        xE.SetAttributeValue("color", Me.Color.ToString)
        xE.SetAttributeValue("name", Me.[Name].ToString)
        xE.SetAttributeValue("level", Me.Level.ToString)
        xE.SetAttributeValue("federal", Me.FederalKey.ToString)
        xE.SetAttributeValue("name2", Me.Name2.ToString)
        xE.SetAttributeValue("group-type", Me.GroupType.ToString)
        xE.SetAttributeValue("group-name", Me.GroupName.ToString)
        xE.SetAttributeValue("link", Me.Link.ToString)
        xE.SetAttributeValue("twitter", Me.Twitter.ToString)
        xE.SetAttributeValue("phone", Me.Phone.ToString)
        xE.SetAttributeValue("mail", Me.Mail.ToString)
        xE.SetAttributeValue("info-mail", Me.InfoMail.ToString)
        If String.IsNullOrEmpty(Me.MapUrl) = False Then
            xE.SetAttributeValue("map-url", Me.MapUrl.ToString)
        End If

        Dim xR As New XElement("representatives")
        For Each m As Representative In Me.Representatives
            xR.Add(m.GetXElement)
        Next
        xE.Add(xR)

        Dim xC As New XElement("committees")
        For Each m As Committee In Me.Committees
            xC.Add(m.GetXElement)
        Next
        xE.Add(xC)

        Dim xP As New XElement("process")
        For Each s As ProcessStep In Me.ProcessSteps
            xP.Add(s.GetXElement)
        Next
        xE.Add(xP)

        Return xE

    End Function

End Class
