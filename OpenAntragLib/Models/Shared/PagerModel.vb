Public Class PagerModel

    Public Property CurrentPage As Integer
    Public Property PageSize As Integer
    Public Property TotalSize As Integer    
    Public Property PagerWingLength As Integer
    Public Property PageUrl As String

    Public Sub New(currentPage As Integer,
                   pageSize As Integer,
                   totalSize As Integer,
                   pagerWingLength As Integer,
                   pageUrl As Integer)
        Me.CurrentPage = currentPage
        Me.PageSize = pageSize
        Me.TotalSize = totalSize
        Me.PagerWingLength = pagerWingLength
        Me.PageUrl = pageUrl
    End Sub

    Public Sub New(currentPage As Integer,
                   totalSize As Integer,
                   pageUrl As String)
        Me.CurrentPage = currentPage
        Me.PageSize = SettingsWrapper.DefaultPagerListPageSize
        Me.TotalSize = totalSize
        Me.PagerWingLength = SettingsWrapper.DefaultPagerListWingLength
        Me.PageUrl = pageUrl
    End Sub

    Public ReadOnly Property MaxPages As Integer
        Get
            Dim intPages = Me.TotalSize \ Me.PageSize

            If Me.TotalSize Mod Me.PageSize > 0 Then
                intPages += 1
            End If

            Return intPages

        End Get
    End Property

End Class
