Imports System.Net
Imports System.Web.Http
Imports System.Net.Http

Public Class ProposalApiController
    Inherits ApiController

    Public Function GetCount(key As String) As Integer

        Select Case key.ToUpper
            Case "ALL"
                Return Proposals.GetItemsCount()
            Case Else
                Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)
                'Dim lst As List(Of Proposal) = Proposals.GetByRepresentation(rep)
                Dim intCount As Integer = Proposals.GetItemsCountByRepresentation(rep)
                Return intCount
        End Select

    End Function

    Public Function GetTop(key As String,
                           count As Integer) As IEnumerable(Of Proposal)

        Dim lst As List(Of Proposal)

        Select Case key.ToUpper
            Case "ALL"
                lst = Proposals.GetItemsTop(count)
            Case Else
                Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)
                If rep Is Nothing Then Throw New HttpResponseException(HttpStatusCode.NotFound)

                lst = Proposals.GetItemsTopByRepresentation(rep, count)
        End Select

        For Each ps As Proposal In lst
            ps.FillProcessSteps()
        Next

        Return lst

    End Function

    Public Function GetPage(key As String,
                            pageNo As Integer,
                            pageCount As Integer) As IEnumerable(Of Proposal)

        Dim lst As List(Of Proposal)

        Select Case key.ToUpper
            Case "ALL"
                lst = Proposals.GetItemsPage(pageNo, pageCount)
            Case Else
                Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)
                If rep Is Nothing Then Throw New HttpResponseException(HttpStatusCode.NotFound)

                lst = Proposals.GetItemsPageByRepresentation(rep, pageNo, pageCount)
        End Select

        For Each ps As Proposal In lst
            ps.FillProcessSteps()
        Next

        Return lst

    End Function

    Public Function GetByTag(key As String,
                             tag As String) As IEnumerable(Of Proposal)

        Dim proptag As ProposalTag = ProposalTags.GetTag(tag)

        If proptag Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        Dim lst As List(Of Proposal)

        Select Case key.ToUpper
            Case "ALL"
                lst = Proposals.GetItemsByTag(proptag)
            Case Else
                Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)
                If rep Is Nothing Then Throw New HttpResponseException(HttpStatusCode.NotFound)

                lst = Proposals.GetItemsByTagAndRepresentation(rep, proptag)
        End Select

        For Each ps As Proposal In lst
            ps.FillProcessSteps()
        Next

        Return lst

    End Function

    Public Function GetById(id As String) As Proposal

        Dim prop As Proposal = Proposals.GetById(id)

        If prop Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        Return prop

    End Function

    Public Function GetByTitleUrl(key As String,
                                  titleUrl As String) As Proposal

        Dim rep As Representation = GlobalData.Representations.GetByKey(key.ToLower)
        If rep Is Nothing Then Throw New HttpResponseException(HttpStatusCode.NotFound)

        Dim prop As Proposal = Proposals.GetByTitleUrl(rep, titleUrl)

        If prop Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        Return prop

    End Function

    Public Function GetComments(id As String) As IEnumerable(Of ProposalComment)

        Dim prop As Proposal = Proposals.GetById(id)

        If prop Is Nothing Then
            Throw New HttpResponseException(HttpStatusCode.NotFound)
        End If

        Return prop.ProposalComments

    End Function

    Public Function GetTags() As IEnumerable(Of String)

        Return ProposalTags.TagsList()

    End Function

    Public Function PostNew(<FromBody()> dto As ProposalDTO) As HttpResponseMessage

        Dim rep As Representation = GlobalData.Representations.GetByKey(dto.Key_Representation.ToLower)

        If rep Is Nothing Then
            Throw New HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Falscher Representation-Key"))
        End If

        If rep.ApiKey.Equals(dto.ApiKey) = False Then
            Throw New HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Falscher Api-Key"))
        End If

        Dim prop As Proposal = Proposals.CreateNew(dto.Key_Representation,
                                                   dto.Title,
                                                   dto.Text,
                                                   dto.TagList,
                                                   Nothing)

        Dim response = Request.CreateResponse(Of Proposal)(HttpStatusCode.Created, prop)

        'Dim uri As String = prop.FullUrl
        'response.Headers.Location = New Uri(uri)

        Return response

    End Function

    Public Function PostNextStep(<FromBody()> dto As ProposalNextStepDTO) As HttpResponseMessage

        Dim rep As Representation = GlobalData.Representations.GetByKey(dto.Key_Representation.ToLower)

        If rep Is Nothing Then
            Throw New HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Falscher Representation-Key"))
        End If

        Dim prop As Proposal = Proposals.GetById(dto.ID_Proposal)

        If prop Is Nothing Then
            Throw New HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Falsche Proposal-ID"))
        End If

        Proposals.SaveNextStep(prop,
                               dto.ID_ProcessStep,
                               dto.InfoText,
                               dto.Key_Representative,
                               dto.Key_Committee)


        Dim response = Request.CreateResponse(Of Proposal)(HttpStatusCode.Created, prop)

        Return response

    End Function

End Class
