Imports System.IO
Imports Microsoft.VisualBasic.CompilerServices
Imports System.Web
Imports System.Text

Public Class piratenmandate

    Public Property Items As New List(Of Item)

    Public Sub New()

        Dim xmlFile As String = HttpContext.Current.Server.MapPath("~/App_Data/piratenmandate.xml")

        If File.Exists(xmlFile) = True Then
            Dim xT As XElement = XElement.Load(xmlFile)

            For Each xB In xT.Elements("bundesland")
                Dim cB As New Item.BundeslandInfo With {.Name = xB.Attribute("name"),
                                                        .GS = xB.Attribute("gs"),
                                                        .Localpirates = xB.Attribute("localpirates")}

                cB.Key = (From f As FederalState In GlobalData.FederalStates.Items
                          Where f.Name = cB.Name
                          Select f.Key).FirstOrDefault()

                For Each xG In xB.Elements("gebiet")
                    ProcessItem(Nothing, xG, cB)
                Next
            Next

        End If

    End Sub

    Public Sub ProcessItem(ByVal cParent As piratenmandate.Item,
                           ByVal xG As XElement,
                           ByVal cB As Item.BundeslandInfo)

        Dim cG As New Item With {.Bundesland = cB,
                                 .GebietName = xG.Attribute("name"),
                                 .GebietGS = xG.Attribute("gs"),
                                 .GebietType = xG.Attribute("type"),
                                 .GebietLocalpirates = xG.Attribute("localpirates")}

        'Behandlung von Ausnahmen in Bezug auf Type und Name
        Select Case cG.GebietType.ToLower()
            Case "stadtbezirk", "ortsteil"
                If cParent IsNot Nothing AndAlso
                    String.IsNullOrEmpty(cParent.GebietName) = False AndAlso
                    cG.GebietName.StartsWith(cParent.GebietName) = False Then

                    cG.GebietName.Prepend(cParent.GebietName, " ")
                End If
        End Select

        If xG.Elements("parlament").Any() Then
            Dim xP As XElement = xG.Elements("parlament").First() 'DTD = *, aber es gibt immer nur ein Parlament            
            cG.ParlamentName = xP.Attribute("name")
            cG.ParlamentSeats = xP.Attribute("seats")
            cG.ParlamentRIS = xP.Attribute("ris")

            If xP.Elements("fraktion").Any() Then
                Dim xF As XElement = xP.Elements("fraktion").First()
                cG.FraktionName = xF.Attribute("name")
                cG.FraktionType = xF.Attribute("type")
                cG.FraktionUrl = xF.Attribute("url")

                If xF.Elements("partner").Any() Then
                    cG.FraktionPartner = New List(Of Item.PartnerInfo)
                    For Each xR As XElement In xF.Elements("partner")
                        Dim cR As New Item.PartnerInfo With {.Name = xR.Attribute("name"),
                                                             .Partei = xR.Attribute("partei"),
                                                             .Num = xR.Attribute("num")}
                        cG.FraktionPartner.Add(cR)
                    Next
                End If
            End If

            If xP.Elements("mandat").Any() Then
                cG.MandateCount = xP.Elements("mandat").Count()
            End If

            If xP.Elements("story").Any() Then
                Dim xS As XElement = xP.Elements("story").First()
                cG.ParlamentStory = xS.Value
                cG.ParlamentStorySource = xS.Attribute("source")
            End If

            If xP.Elements("feed").Any() Then
                Dim xD As XElement = xP.Elements("feed").First()
                cG.ParlamentFeedUrl = xD.Attribute("url")
            End If

            If xP.Elements("oa").Any() Then
                Dim xO As XElement = xP.Elements("oa").First()
                Dim strUrl = xO.Attribute("url")
                If String.IsNullOrEmpty(strUrl) = False Then
                    Dim arr As String() = Split(strUrl, "/")
                    If arr.Length >= 4 Then cG.OpenAntragKey = arr(3)
                End If

            End If

        End If

        Me.Items.Add(cG)

        If xG.Elements("gebiet").Any() Then
            For Each xC In xG.Elements("gebiet")
                ProcessItem(cG, xC, cB) 'rekursiver Aufruf
            Next
        End If

    End Sub

    Public Class Item

        Public Property Bundesland As BundeslandInfo

        Public Property GebietName As String
        Public Property GebietType As String

        Public ReadOnly Property GebietTypeAndName As String
            Get
                Dim lstNoType As New List(Of String) From {
                    "kreis",
                    "landkreis",
                    "landschaftsverband",
                    "regionalverband",
                    "kommunalverband besonderer art"
                }

                If lstNoType.Contains(Me.GebietType.ToLower()) = True Or
                   Me.GebietName.ToLower.Contains("ortsamtsbereich") = True Then
                    Return Me.GebietName
                Else
                    Return String.Concat(Me.GebietType, " ", Me.GebietName)
                End If

            End Get
        End Property

        Public Property GebietGS As String
        Public Property GebietLocalpirates As String

        Public Property ParlamentName As String
        Public Property ParlamentSeats As String
        Public Property ParlamentRIS As String
        Public Property ParlamentStory As String
        Public Property ParlamentStorySource As String
        Public Property ParlamentFeedUrl As String

        Public Property FraktionName As String
        Public Property FraktionType As String
        Public Property FraktionUrl As String
        Public Property FraktionPartner As List(Of PartnerInfo)

        Public ReadOnly Property FraktionText As String
            Get
                Dim stb As New StringBuilder
                If Me.FraktionType IsNot Nothing Then
                    Select Case Me.FraktionType.ToLower
                        Case "piraten"
                            stb.Append("Piratenfraktion")
                        Case "gemeinsam"
                            stb.Append("Gemeinsame Gruppe/Fraktion <strong>").Append(FraktionName).Append("</strong> mit ")
                            Dim spi As New StringBuilder
                            For Each pi As PartnerInfo In Me.FraktionPartner
                                spi.AppendWithSeperator(pi.NameParteiNum, " und ")
                            Next
                            stb.Append(spi.ToString())
                        Case Else
                            'nichts zu tun
                    End Select
                End If
                Return stb.ToString()
            End Get
        End Property

        Public Property MandateCount As Integer

        Public Property OpenAntragKey As String

        Public Property MapUrl As String

        Public Class BundeslandInfo
            Public Property Key As String
            Public Property [Name] As String
            Public Property GS As String
            Public Property Localpirates As String
        End Class

        Public Class PartnerInfo
            Public Property [Name] As String
            Public Property Partei As String
            Public Property Num As String

            Public ReadOnly Property NameParteiNum As String
                Get
                    Dim stb As New StringBuilder
                    If String.IsNullOrEmpty(Me.Name) = False Then
                        stb.Append(Me.Name)
                    Else
                        stb.Append(Me.Partei)
                    End If
                    stb.Append(" (").Append(Me.Num).Append(")")
                    Return stb.ToString()
                End Get
            End Property
        End Class
    End Class

End Class