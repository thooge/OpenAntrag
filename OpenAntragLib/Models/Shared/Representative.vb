Imports System.IO
Imports System.Text
Imports System.Web

Public Class Representative

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Key_Representation As String

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public Property ID As Integer

    Public Property Key As String
    Public Property [Name] As String
    Public Property Party As String
    Public Property Mail As String
    Public Property Twitter As String
    Public Property Phone() As String

    Public ReadOnly Property PortraitImage() As String
        Get
            Dim stbPath As New StringBuilder()
            stbPath.Append("/Content/Representations/")
            stbPath.Append(Me.Key_Representation).Append("/Portraits/")

            Dim di As New DirectoryInfo(HttpContext.Current.Server.MapPath(stbPath.ToString))
            Dim fi As FileInfo() = di.GetFiles(Me.Key & ".*")
            If fi.Count > 0 Then
                Return String.Concat(stbPath.ToString, fi(0).Name)
            Else
                Return "/Images/dummy-portrait-invers.png"
            End If
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property HasPortraitImage() As Boolean
        Get
            Return (Me.PortraitImage <> "/Images/dummy-portrait-invers.png")
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    <CSVIgnore>
    Public ReadOnly Property Info As HtmlString
        Get
            Dim hs As New HtmlString("")

            Dim stbPath As New StringBuilder()
            stbPath.Append("/Content/Representations/")
            stbPath.Append(Me.Key_Representation).Append("/Info/")

            Dim di As New DirectoryInfo(HttpContext.Current.Server.MapPath(stbPath.ToString))
            Dim fi As FileInfo() = di.GetFiles(Me.Key & ".info")
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
    Public ReadOnly Property InfoHtml() As String
        Get
            Return Me.Info.ToHtmlString
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ex As XElement, keyRepresentation As String)
        Me.New()
        With Me
            .Key_Representation = keyRepresentation
            .ID = CType(ex.Attribute("id").Value, Integer)
            .Key = ex.Attribute("key").Value
            .[Name] = ex.Attribute("name").Value
            .Party = ex.Attribute("party").Value
            .Mail = ex.Attribute("mail").Value
            .Twitter = ex.Attribute("twitter").Value.Replace("@", "")
            .Phone = ex.Attribute("phone").Value
        End With
    End Sub

    Public Function GetXElement() As XElement

        Dim ex As New XElement("item")
        ex.SetAttributeValue("id", Me.ID.ToString)
        ex.SetAttributeValue("key", Me.Key.ToString)
        ex.SetAttributeValue("name", Me.[Name].ToString)
        ex.SetAttributeValue("party", Me.Party.ToString)
        ex.SetAttributeValue("mail", Me.Mail.ToString)
        ex.SetAttributeValue("phone", Me.Phone.ToString)

        Return ex

    End Function

End Class

