@Imports OpenAntrag
@Imports ActionMailer.Net

@ModelType Proposal

@Code
    Layout = Nothing
End Code

Ahoi,

es ist ein neuer OpenAntrag eingegangen, der Deine Aufmerksamkeit benötigt:

Titel: @Html.Raw(Model.Title)

Url: @Model.FullUrl

Text:
-------------------------------------------------------
@Html.Raw(Model.TextRaw)
-------------------------------------------------------

@If String.IsNullOrEmpty(Model.ContactInfo) = False Then
    @Model.ContactInfo
End If