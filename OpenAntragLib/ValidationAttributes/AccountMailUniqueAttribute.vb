Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc
Imports System.Web.Security

Public Class AccountMailUniqueAttribute
    Inherits ValidationAttribute
    Implements IClientValidatable

    Private Const strDefaultErrorMessage As String = "Dieser Benutzer-Mail-Adresse existiert bereits"

    Public Sub New()
        MyBase.New(strDefaultErrorMessage)
    End Sub

    Protected Overrides Function IsValid(ByVal value As Object, context As ValidationContext) As ValidationResult

        '--ClientValidation: AccountController.IsAccountMailUnique

        Dim strWantedMail As String = CType(value, String)

        Dim strUserName As String = Membership.GetUserNameByEmail(strWantedMail)

        If String.IsNullOrEmpty(strUserName) = False Then
            Return New ValidationResult(FormatErrorMessage(context.DisplayName))
        End If

        Return ValidationResult.Success

    End Function

    Public Overrides Function FormatErrorMessage(name As String) As String
        Return String.Format(ErrorMessageString, name)
    End Function

    Public Function GetClientValidationRules(metadata As System.Web.Mvc.ModelMetadata,
                                             context As System.Web.Mvc.ControllerContext) As IEnumerable(Of ModelClientValidationRule) Implements IClientValidatable.GetClientValidationRules

        Dim rule As New ModelClientValidationRule() With {.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                                                          .ValidationType = "accountmailunique"}
        Return New ModelClientValidationRule() {rule}
    End Function

End Class
