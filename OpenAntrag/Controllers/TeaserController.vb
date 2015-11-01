Imports System.Web.Mvc

Public Class TeaserController
    Inherits Controller

    Function Index(keyTeaser As String) As ActionResult

        Dim tsr As Teaser = (New Teasers).GetByKey(keyTeaser.ToLower)
        ViewBag.CurrentTeaser = tsr

        If String.IsNullOrEmpty(tsr.KeyRepresenation) = False Then
            Dim rep As Representation = GlobalData.Representations.GetByKey(tsr.KeyRepresenation)
            If rep IsNot Nothing Then
                Return RedirectToActionPermanent("Index", "Representation",
                                                 New With {.keyRepresentation = tsr.KeyRepresenation})
            Else
                Return Nothing
            End If
        Else
            Return View(tsr)
        End If

    End Function

    Public Function TeaserStyle(keyTeaser As String) As ActionResult

        Dim tsr As Teaser = (New Teasers).GetByKey(keyTeaser.ToLower)

        Dim strTemplatePath As String = HttpContext.Server.MapPath("~/Content/style-teaser.template.css")
        Dim strTemplate As String = Nothing
        If System.IO.File.Exists(strTemplatePath) = True Then
            strTemplate = System.IO.File.ReadAllText(strTemplatePath)
        End If

        Dim stb As New StringBuilder()

        stb.Append(strTemplate)
        Teasers.ReplaceStyleColor(tsr, stb)

        Return Content(stb.ToString, "text/css")

    End Function


End Class