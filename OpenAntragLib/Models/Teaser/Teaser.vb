Imports System.Drawing
Imports System.Text
Imports System.Web
Imports System.IO

Public Class Teaser
    Implements IXMLClass

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property ID As Integer

    Public Property Key As String
    Public Property Label As String
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

    Public Property Mail As String
    Public Property Link As String
    Public Property Twitter As String

    Public Property ElectionDate As String

    Public Property KeyRepresenation As String

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property ElectionDateFormat As String
        Get
            If String.IsNullOrEmpty(Me.ElectionDate) = False Then
                Return CType(Me.ElectionDate, DateTime).ToString("dd. MMMM yyyy")
            Else
                Return ""
            End If
        End Get
    End Property

    Public Property TeaserUrl As String

    Public ReadOnly Property FullUrl As String
        Get
            Dim stb As New StringBuilder()
            stb.Append("http://").Append(HttpContext.Current.Request.Url.Authority)
            stb.Append("/").Append(TeaserUrl)
            Return stb.ToString
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property FraktionInfo As HtmlString
        Get
            Dim hs As New HtmlString("")

            Dim stbPath As New StringBuilder()
            stbPath.Append("/Content/Teaser/")
            stbPath.Append(Me.Key).Append("/Info/")

            Dim di As New DirectoryInfo(HttpContext.Current.Server.MapPath(stbPath.ToString))
            Dim fi As FileInfo() = di.GetFiles("teaser.info")
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

    Public Sub New()
    End Sub

    Public Sub New(ex As XElement)
        Me.New()
        With Me
            .ID = CType(ex.Attribute("id").Value, Integer)
            .Key = ex.Attribute("key").Value
            .Label = ex.Attribute("label").Value
            .Color = ex.Attribute("color").Value
            .[Name] = ex.Attribute("name").Value
            .[Name2] = ex.Attribute("name2").Value
            .Level = CType(ex.Attribute("level").Value, Integer)
            .FederalKey = ex.Attribute("federal").Value
            .Mail = ex.Attribute("mail").Value
            .Link = ex.Attribute("link").Value
            .Twitter = ex.Attribute("twitter").Value

            .TeaserUrl = ex.Attribute("teaser-url").Value
            .ElectionDate = ex.Attribute("election-date").Value

            .KeyRepresenation = ex.Attribute("key-represenation").Value
        End With
    End Sub

    Public Function GetXElement(xD As XmlData) As XElement Implements IXMLClass.GetXElement

        Dim xE As New XElement(xD.ElementName)
        xE.SetAttributeValue("id", Me.ID.ToString)
        xE.SetAttributeValue("key", Me.Key.ToString)
        xE.SetAttributeValue("label", Me.Label.ToString)
        xE.SetAttributeValue("color", Me.Color.ToString)
        xE.SetAttributeValue("name", Me.[Name].ToString)
        xE.SetAttributeValue("level", Me.Level.ToString)
        xE.SetAttributeValue("federal", Me.FederalKey.ToString)
        xE.SetAttributeValue("name2", Me.Name2.ToString)
        xE.SetAttributeValue("mail", Me.Mail.ToString)
        xE.SetAttributeValue("link", Me.Link.ToString)
        xE.SetAttributeValue("twitter", Me.Twitter.ToString)

        xE.SetAttributeValue("teaser-url", Me.TeaserUrl.ToString)
        xE.SetAttributeValue("election-date", Me.ElectionDate.ToString)

        xE.SetAttributeValue("key-represenation", Me.KeyRepresenation)

        Return xE

    End Function
End Class
