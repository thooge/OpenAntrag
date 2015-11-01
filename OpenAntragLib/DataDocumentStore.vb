Imports System
Imports Raven.Client
Imports Raven.Client.Indexes
Imports System.Reflection
Imports Raven.Abstractions.Indexing
Imports System.Threading

Public Class DataDocumentStore

    'http://msdn.microsoft.com/pt-br/magazine/hh547101%28en-us%29.aspx
    'http://ravendb.net/docs/client-api/connecting-to-a-ravendb-datastore

    Private Shared _ds As IDocumentStore

    Public Shared Function Initialize() As IDocumentStore

        _ds = New Document.DocumentStore With {.ConnectionStringName = "RavenDBServer"}

        'Predefined Indexes...
        'http://codeofrob.com/entries/ravendb-image-gallery-project-xiii--understanding-indexes.html

        _ds.Conventions.IdentityPartsSeparator = "-"
        _ds.Initialize()

        IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), _ds)

        Return _ds

    End Function

    Public Shared ReadOnly Property Instance() As IDocumentStore
        Get
            If _ds Is Nothing Then
                Throw New InvalidOperationException("IDocumentStore wurde nicht initialisiert")
            End If
            Return _ds
        End Get
    End Property

    Public Shared ReadOnly Property Session() As IDocumentSession
        Get
            Dim ses As IDocumentSession = DataDocumentStore.Instance.OpenSession
            'If ses Is Nothing Then
            '    Thread.Sleep(10)
            '    ses = DataDocumentStore.Instance.OpenSession
            'End If
            Return ses
        End Get
    End Property

End Class
