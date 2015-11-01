Imports System.Runtime.CompilerServices

Public Module GeneralExtensions

    <Extension()>
    Public Sub GetPageData(Of T)(ByRef lst As List(Of T),
                                 ByVal pageNo As Integer)
        lst = lst.Skip((pageNo - 1) * SettingsWrapper.DefaultPagerListPageSize).Take(SettingsWrapper.DefaultPagerListPageSize).ToList()
    End Sub

End Module
