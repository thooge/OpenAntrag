Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc

Public Class MailValidAttribute
    Inherits ValidationAttribute
    Implements IClientValidatable

    Private Const strDefaultErrorMessage As String = "Ungültige Mail-Adresse"

    Public Sub New()
        MyBase.New(strDefaultErrorMessage)
    End Sub

    Protected Overrides Function IsValid(ByVal value As Object, context As ValidationContext) As ValidationResult

        Dim strMailAddress As String = CType(value, String)

        If Tools.IsValidMail(strMailAddress) = False Then
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
                                                          .ValidationType = "mailvalid"}
        Return New ModelClientValidationRule() {rule}
    End Function

End Class
