Imports System.ComponentModel.DataAnnotations
Imports System.Web.Script.Serialization

Public Class ModelBase
    Implements IModelBase

    <Display(Name:="Erstellt am")>
    Public Property CreatedAt As String Implements IModelBase.CreatedAt

    <Display(Name:="Erstellt am")>
    <ScriptIgnore()>
    <Raven.Imports.Newtonsoft.Json.JsonIgnore()>
    <Newtonsoft.Json.JsonIgnore()>
    Public ReadOnly Property CreatedAtFormat As String Implements IModelBase.CreatedAtFormat
        Get
            Return CType(Me.CreatedAt, DateTime).ToString("dd. MMMM yyyy HH:mm")
        End Get
    End Property

    Private _createdBy As String
    <Display(Name:="Erstellt von")>
    <Newtonsoft.Json.JsonIgnore()>
    Public Property CreatedBy As String Implements IModelBase.CreatedBy
        Get
            Return _createdBy
        End Get
        Set(value As String)
            _createdBy = value
        End Set
    End Property

    Private _timeStamp As Integer = 0
    Public Property Timestamp As Integer Implements IModelBase.Timestamp
        Get
            Try
                If _timeStamp = 0 Then
                    _timeStamp = Tools.GetUnixTimestampFromDate(CType(Me.CreatedAt, DateTime))
                End If
            Catch ex As Exception
            End Try
            Return _timeStamp
        End Get
        Set(value As Integer)
            _timeStamp = value
        End Set
    End Property

End Class
