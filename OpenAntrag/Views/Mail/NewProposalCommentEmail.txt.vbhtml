@Imports OpenAntrag
@Imports ActionMailer.Net

@ModelType ProposalComment

@Code
    Layout = Nothing
End Code

Ahoi,

@Html.Raw(ViewData("Subject")):

@Html.Raw(Model.CommentedBy) um @Model.CommentedAtFormat

@Html.Raw(Model.CommentText)