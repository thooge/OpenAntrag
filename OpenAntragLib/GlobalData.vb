Public Class GlobalData

    Private Shared _Representations As Representations
    Public Shared ReadOnly Property Representations As Representations
        Get
            If GlobalData._Representations Is Nothing Then
                GlobalData._Representations = New Representations()
            End If
            Return _Representations
        End Get
    End Property

    Private Shared _piratenmandate As piratenmandate
    Public Shared ReadOnly Property piratenmandate As piratenmandate
        Get
            If GlobalData._piratenmandate Is Nothing Then
                GlobalData._piratenmandate = New piratenmandate()
            End If
            Return _piratenmandate
        End Get
    End Property

    Private Shared _ProcessStepDefinitions As ProcessStepDefinitions
    Public Shared ReadOnly Property ProcessStepDefinitions As ProcessStepDefinitions
        Get
            If GlobalData._ProcessStepDefinitions Is Nothing Then
                GlobalData._ProcessStepDefinitions = New ProcessStepDefinitions()
            End If
            Return _ProcessStepDefinitions
        End Get
    End Property

    Private Shared _GroupTypes As GroupTypes
    Public Shared ReadOnly Property GroupTypes As GroupTypes
        Get
            If GlobalData._GroupTypes Is Nothing Then
                GlobalData._GroupTypes = New GroupTypes()
            End If
            Return _GroupTypes
        End Get
    End Property

    Private Shared _FeedbackTypes As FeedbackTypes
    Public Shared ReadOnly Property FeedbackTypes As FeedbackTypes
        Get
            If GlobalData._FeedbackTypes Is Nothing Then
                GlobalData._FeedbackTypes = New FeedbackTypes()
            End If
            Return _FeedbackTypes
        End Get
    End Property

    Private Shared _FederalStates As FederalStates
    Public Shared ReadOnly Property FederalStates As FederalStates
        Get
            If GlobalData._FederalStates Is Nothing Then
                GlobalData._FederalStates = New FederalStates()
            End If
            Return _FederalStates
        End Get
    End Property

    Private Shared _GovernmentalLevels As GovernmentalLevels
    Public Shared ReadOnly Property GovernmentalLevels As GovernmentalLevels
        Get
            If GlobalData._GovernmentalLevels Is Nothing Then
                GlobalData._GovernmentalLevels = New GovernmentalLevels()
            End If
            Return _GovernmentalLevels
        End Get
    End Property

End Class
