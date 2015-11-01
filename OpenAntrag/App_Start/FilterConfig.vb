Imports System.Web
Imports System.Web.Mvc

Public Class FilterConfig

    Public Shared Sub RegisterGlobalFilters(ByVal filters As GlobalFilterCollection)

        filters.Add(New HandleCustomErrorAttribute With {.View = "Uups"})

    End Sub

End Class