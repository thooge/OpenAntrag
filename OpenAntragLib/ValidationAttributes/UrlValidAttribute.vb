Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc

Public Class UrlValidAttribute
    Inherits ValidationAttribute
    Implements IClientValidatable

    Private Const strDefaultErrorMessage As String = "Ungültige Url"

    Public Sub New()
        MyBase.New(strDefaultErrorMessage)
    End Sub

    Protected Overrides Function IsValid(ByVal value As Object,
                                         context As ValidationContext) As ValidationResult

        Dim strUrl As String = CType(value, String)

        If String.IsNullOrEmpty(strUrl) = False AndAlso Tools.IsValidUrl(strUrl) = False Then
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
                                                          .ValidationType = "urlvalid"}
        Return New ModelClientValidationRule() {rule}
    End Function

End Class
