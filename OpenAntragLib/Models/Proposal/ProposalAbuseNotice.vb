Public Class ProposalAbuseNotice

    Public Property Proposal As Proposal
    Public Property Notice As String

    Public Sub New(prop As Proposal, notice As String)
        Me.Proposal = prop
        Me.Notice = notice
    End Sub

End Class
