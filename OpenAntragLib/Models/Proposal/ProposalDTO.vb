Public Class ProposalDTO

    Public Property ApiKey As String
    Public Property Key_Representation As String
    Public Property Title As String
    Public Property Text As String
    Public Property TagList As String

End Class

Public Class ProposalNextStepDTO

    Public Property ApiKey As String
    Public Property Key_Representation As String
    Public Property ID_Proposal As String
    Public Property ID_ProcessStep As String
    Public Property InfoText As String
    Public Property Key_Representative As String
    Public Property Key_Committee As String

End Class
