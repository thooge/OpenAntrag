Imports System.Web.Mvc
Imports Raven.Imports
Imports System.Text

Public Class ProcessStep

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Key_Representation As String
    Private _Representation As Representation
    <Newtonsoft.Json.JsonIgnore()>
    Public Property Representation As Representation
        Get
            If _Representation Is Nothing Then
                _Representation = GlobalData.Representations.GetByKey(Me.Key_Representation.ToLower)
            End If
            Return _Representation
        End Get
        Set(value As Representation)
            _Representation = value
        End Set
    End Property

    Public Property ID As Integer
    Public Property IsInactive As Boolean = False

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Key_Definition As String

    Public Property Caption As String
    Public Property ShortCaption As String    
    Public Property SuccessStory As Boolean = False

    Public Property Color As String

    <Newtonsoft.Json.JsonIgnore()>
    Public Property Icon As String

    <Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property IconPath As String
        Get
            Return String.Concat("/Images/Icons/", Me.Icon)
        End Get
    End Property

    <Newtonsoft.Json.JsonIgnore()>
    Public Property NextSteps As List(Of ProcessStep)

    Public ReadOnly Property ID_NextSteps As String
        Get
            Dim retVal As String = Nothing
            Try
                Dim query = From ps As ProcessStep In Me.NextSteps
                        Select ps.ID

                If query.Count > 0 Then
                    retVal = String.Join(",", query.ToArray)
                End If

            Catch ex As Exception
            End Try

            Return retVal

        End Get
    End Property

    Public Sub New(rep As Representation)
        _Representation = rep
        Me.Key_Representation = rep.Key
    End Sub

    Public Sub New(rep As Representation, ex As XElement)
        Me.New(rep)
        With Me
            .ID = CType(ex.Attribute("id").Value, Integer)

            If ex.Attribute("inactive") IsNot Nothing Then
                .IsInactive = CType(ex.Attribute("inactive").Value, Boolean)
            End If

            If ex.Attribute("def") IsNot Nothing Then
                .Key_Definition = ex.Attribute("def").Value

                'Definition als Standard setzen
                Dim def = (From d In GlobalData.ProcessStepDefinitions.Items
                           Where d.Key = .Key_Definition
                           Select d).FirstOrDefault

                .Icon = def.Icon
                .Color = def.Color
                .Caption = def.Caption
                .ShortCaption = def.ShortCaption
            Else
                Throw New Exception("Schritt ohne Definition")
            End If

            If ex.Attribute("icon") IsNot Nothing Then
                .Icon = ex.Attribute("icon").Value
            End If
            If ex.Attribute("color") IsNot Nothing Then
                .Color = ex.Attribute("color").Value
            End If
            If ex.Attribute("caption") IsNot Nothing Then
                .Caption = ex.Attribute("caption").Value
            End If
            If ex.Attribute("short-caption") IsNot Nothing Then
                .ShortCaption = ex.Attribute("short-caption").Value
            End If

            If .Icon Is Nothing Or .Color Is Nothing Then
                Throw New Exception("Prozesschritt ohne Icon oder Farbe")
            End If

            If .Caption Is Nothing Or .ShortCaption Is Nothing Then
                Throw New Exception("Prozesschritt ohne Caption")
            End If

            If .Caption.Contains("[") Or .ShortCaption.Contains("[") Then
                Throw New Exception("Prozesschritt mit unaufgelöster Definitionsvariable")
            End If

            If ex.Attribute("success-story") IsNot Nothing Then
                .SuccessStory = ex.Attribute("success-story").Value
            End If

        End With
    End Sub

    Public Function CloneForNextStep() As ProcessStep

        Return New ProcessStep(Me.Representation) With {
            .Key_Representation = Me.Key_Representation,
            .ID = Me.ID,
            .Caption = Me.Caption,
            .ShortCaption = Me.ShortCaption,
            .Color = Me.Color,
            .Icon = Me.Icon,
            .NextSteps = Nothing}

    End Function

    Public Function GetXElement() As XElement

        Dim ex As New XElement("item")
        ex.SetAttributeValue("id", Me.ID.ToString)
        ex.SetAttributeValue("caption", Me.Caption.ToString)
        ex.SetAttributeValue("sort-caption", Me.ShortCaption.ToString)
        ex.SetAttributeValue("icon", Me.Icon.ToString)
        ex.SetAttributeValue("color", Me.Color.ToString)
        If String.IsNullOrEmpty(Me.SuccessStory) = False Then ex.SetAttributeValue("success-story", Me.SuccessStory.ToString)

        Return ex

    End Function

    Public Function GetCaptionHtml() As String

        Dim stb As New StringBuilder(Me.Caption)

        If Me.Caption.ToUpper.Contains("%REPRESENTATIVE%") Then
            Dim eRep As New TagBuilder("select")
            eRep.Attributes.Add("id", "Key_Representative")
            eRep.AddCssClass("nextstep-option")
            eRep.AddCssClass("selectpicker")
            For Each r As Representative In Me.Representation.Representatives
                Dim eOpt As New TagBuilder("option")
                eOpt.Attributes.Add("value", r.Key)
                eOpt.Append(r.Name)
                eRep.Append(eOpt)
            Next
            stb.Replace("%REPRESENTATIVE%",
                        String.Concat("&nbsp;", eRep.ToString(TagRenderMode.Normal), "&nbsp;"))
        End If

        If Me.Caption.ToUpper.Contains("%COMMITTEE%") Then
            Dim eCom As New TagBuilder("select")
            eCom.Attributes.Add("id", "Key_Committee")
            eCom.AddCssClass("nextstep-option")
            eCom.AddCssClass("selectpicker")
            For Each c As Committee In Me.Representation.Committees
                Dim eOpt As New TagBuilder("option")
                eOpt.Attributes.Add("value", c.Key)
                eOpt.Append(c.Name)
                eCom.Append(eOpt)
            Next
            stb.Replace("%COMMITTEE%",
                        String.Concat("&nbsp;", eCom.ToString(TagRenderMode.Normal), "&nbsp;"))
        End If

        Return stb.ToString

    End Function

End Class
