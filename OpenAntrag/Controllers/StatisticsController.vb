Imports Raven.Client

Public Class StatisticsController
    Inherits CommonController

    Public Function Index(ByVal statPart As String) As ActionResult

        If String.IsNullOrEmpty(statPart) = True Then
            statPart = "ProposalCountByRepresentation"
            ViewData("StatScroll") = False
        Else
            ViewData("StatScroll") = True
        End If

        ViewData("StatPartial") = statPart

        Return View()

    End Function

End Class
