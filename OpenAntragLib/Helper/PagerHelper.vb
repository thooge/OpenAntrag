Imports System.Runtime.CompilerServices
Imports System.Web.Mvc

Public Module PagerHelper

    <Extension()>
    Public Function Pager(htmlHelper As HtmlHelper,
                          m As PagerModel) As MvcHtmlString

        If m Is Nothing Then
            Return MvcHtmlString.Empty
        End If

        '--- Berechnung ------------------------------------------------------

        Dim pagerLength = (m.PagerWingLength * 2) + 1
        If pagerLength > m.MaxPages Then pagerLength = m.MaxPages

        Dim pivotLeft As Integer = m.PagerWingLength + 1
        Dim pivotRight As Integer = m.MaxPages - m.PagerWingLength

        Dim pageFirst As Integer
        Dim pageLast As Integer

        If m.CurrentPage <= pivotLeft Then
            pageFirst = 1
            pageLast = pagerLength
        ElseIf m.CurrentPage >= pivotRight Then
            pageFirst = (m.MaxPages - pagerLength) + 1
            pageLast = m.MaxPages
        Else
            pageFirst = m.CurrentPage - m.PagerWingLength
            pageLast = m.CurrentPage + m.PagerWingLength
        End If

        'Dim pageLast As Integer = m.TotalSize \ m.PageSize
        'If m.TotalSize Mod m.PageSize > 0 Then
        '    pageLast += 1
        'End If

        '------------------------------------------------------

        Dim ePager As New TagBuilder("ul")

        'Dim eDots As New TagBuilder("span")
        'eDots.SetInnerText("...")

        If m.CurrentPage > m.MaxPages Then
            Dim eInfo As New TagBuilder("span")
            eInfo.SetInnerText("Auf dieser Seite gibt es leider noch nichts zu sehen...")
            ePager.Append(eInfo)
            '--
            'ePager.Append(eDots)
            '--
            Dim ePageOne As New TagBuilder("a")
            ePageOne.Attributes.Add("href", m.PageUrl.Replace("@Page", 1))
            ePageOne.SetInnerText("zurück zum Anfang")
            ePager.Append(ePageOne)
            '--
            Return MvcHtmlString.Create(ePager.ToString(TagRenderMode.Normal))
        End If

        '--- Direction LEFT ------------------------------------------------------

        'First
        Dim eFirstOuter As New TagBuilder("li")
        With eFirstOuter
            Dim e As TagBuilder
            If m.CurrentPage > 1 Then
                e = New TagBuilder("a")
                e.AddCssClass("tt-std")
                e.Attributes.Add("title", "Erste Seite")
                e.Attributes.Add("href", m.PageUrl.Replace("@Page", 1))
            Else
                .AddCssClass("disabled")
                e = New TagBuilder("span")
            End If
            e.Append(<i class="icon-angle-double-left"></i>.ToString)
            .AddCssClass("dir")
            .Append(e)
        End With
        ePager.Append(eFirstOuter)

        'Previous
        Dim ePrevOuter As New TagBuilder("li")
        With ePrevOuter
            Dim e As TagBuilder
            If m.CurrentPage > 1 Then
                e = New TagBuilder("a")
                e.AddCssClass("tt-std")
                e.Attributes.Add("title", "Vorherige Seite")
                e.Attributes.Add("href", m.PageUrl.Replace("@Page", m.CurrentPage - 1))
            Else
                .AddCssClass("disabled")
                e = New TagBuilder("span")
            End If
            e.Append(<i class="icon-angle-left"></i>.ToString)
            .AddCssClass("dir")
            .Append(e)
        End With
        ePager.Append(ePrevOuter)

        '--- Wing ------------------------------------------------------

        'If intPageFirst > 1 Then eWing.Append(eDots)

        For i As Integer = pageFirst To pageLast
            Dim ePageOuter As New TagBuilder("li")
            With ePageOuter
                Dim e As TagBuilder
                If i = m.CurrentPage Then
                    .AddCssClass("active")
                    e = New TagBuilder("span")
                Else
                    e = New TagBuilder("a")
                    e.AddCssClass("tt-std")
                    e.Attributes.Add("title", String.Concat("Seite ", i))
                    e.Attributes.Add("href", m.PageUrl.Replace("@Page", i))
                End If
                e.SetInnerText(i)
                ePageOuter.Append(e)
            End With            
            ePager.Append(ePageOuter)
        Next

        'If intPageLast < m.MaxPages Then eWing.Append(eDots)

        '--- Direction RIGHT ------------------------------------------------------

        'Next
        Dim eNextOuter As New TagBuilder("li")
        With eNextOuter
            Dim e As TagBuilder
            If m.CurrentPage < m.MaxPages Then
                e = New TagBuilder("a")
                e.AddCssClass("tt-std")
                e.Attributes.Add("title", "Nächste Seite")
                e.Attributes.Add("href", m.PageUrl.Replace("@Page", m.CurrentPage + 1))
            Else
                .AddCssClass("disabled")
                e = New TagBuilder("span")
            End If
            e.Append(<i class="icon-angle-right"></i>.ToString)
            .AddCssClass("dir")
            .Append(e)
        End With
        ePager.Append(eNextOuter)

        'Last
        Dim eLastOuter As New TagBuilder("li")
        With eLastOuter
            Dim e As TagBuilder
            If m.CurrentPage < m.MaxPages Then
                e = New TagBuilder("a")
                e.AddCssClass("tt-std")
                e.Attributes.Add("title", "Letzte Seite")
                e.Attributes.Add("href", m.PageUrl.Replace("@Page", m.MaxPages))
            Else
                .AddCssClass("disabled")
                e = New TagBuilder("span")
            End If
            e.Append(<i class="icon-angle-double-right"></i>.ToString)
            .AddCssClass("dir")
            .Append(e)
        End With
        ePager.Append(eLastOuter)

        '------------------------------------------------------

        Dim ePagerWrapper As New TagBuilder("div")
        ePagerWrapper.AddCssClass("pagination")
        ePagerWrapper.Append(ePager)

        Return MvcHtmlString.Create(ePagerWrapper.ToString(TagRenderMode.Normal))

    End Function

End Module
