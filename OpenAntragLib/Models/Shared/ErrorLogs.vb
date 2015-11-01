Imports Raven.Client

Public Class ErrorLogs

    Public Shared Sub NewLog(log As ErrorLog, userAgent As String)

        Dim el As ErrorLog = Nothing
        Dim isNewLog As Boolean = False

        Using ds As IDocumentSession = DataDocumentStore.Session
            'el = ds.Query(Of ErrorLog)() _
            '    .Where(Function(x) x.AbsoluteUri = log.AbsoluteUri AndAlso x.Message = log.Message).FirstOrDefault()

            el = ds.Query(Of ErrorLog)() _
                .FirstOrDefault(Function(x) x.AbsoluteUri = log.AbsoluteUri AndAlso x.Message = log.Message)

            If el Is Nothing Then
                el = log
                el.Occurrences = New List(Of ErrorOccurrence)()
                ds.Store(el)
                isNewLog = True
            End If

            el.Occurrences.Add(New ErrorOccurrence() With {
                .UserAgent = userAgent
            })
            ds.SaveChanges()
        End Using

        Dim pm As PushoverMessage

        If isNewLog = True Then
            pm = New PushoverMessage(
                String.Concat("Exception auf ", el.AbsoluteUri),
                String.Concat((String.Join(vbLf, el.Parameter)), vbLf, el.Message),
                el.Url, el.Url,
                True)
        Else
            pm = New PushoverMessage(
                String.Concat("Exception ", el.IdNumber, " auf ", el.AbsoluteUri),
                String.Concat("Wiederholter Fehler ", el.Occurrences.Count),
                el.Url, el.Url,
                True)
        End If

        PushoverManager.Send(pm)

    End Sub

    ''' <summary>
    ''' Lädt einen Log-Eintrag anhand seiner ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Shared Function GetById(id As String) As ErrorLog

        Dim el As ErrorLog = Nothing

        Using session As IDocumentSession = DataDocumentStore.Session
            el = session.Load(Of ErrorLog)(id)
        End Using

        Return el
    End Function

    ''' <summary>
    ''' Gibt eine Liste aller Log-Einträge zurück
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetList() As List(Of ErrorLog)

        Dim lst As New List(Of ErrorLog)()

        Using session As IDocumentSession = DataDocumentStore.Session
            lst = session.Query(Of ErrorLog)().OrderByDescending(Function(x) x.Timestamp).ToList()
        End Using

        Return lst

    End Function

End Class

