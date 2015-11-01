@Imports OpenAntrag
@Imports ActionMailer.Net
@ModelType ProposalAbuseNotice

@Code
    Layout = Nothing
End Code

Ahoi,

folgender Antrag wurde von einem Website-Benutzer als Missbrauch gemeldet:

Titel: @Html.Raw(Model.Proposal.Title)

Url: @Model.Proposal.FullUrl

Bitte prüfe den Antrag umgehend und schalte den Antrag stumm, sofern sich die Meldung bestätigt!

Begründung:
-----------
@Html.Raw(Model.Notice)