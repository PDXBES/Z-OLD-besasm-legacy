Imports ResultsWarehouseManager

Public Class ModelResults

    Inherits System.Object

    'From model catalog table
    Public modelID As Integer
    Public scenarioID As Integer
    Public modelName As String
    Public modelDescription As String
    Public modelType As String
    Public modelPath As String
    Public modelOutputFile As String
    Public userName As String
    Public isUploaded As Boolean
    Public uploadedDate As Date

    Public modelResultsTable As String

    Public ResultsDataset As DataSet

    Private ModelConnectionString As OleDb.OleDbConnection

    Private tableE09Adapter As OleDb.OleDbDataAdapter
    Private tableE10Adapter As OleDb.OleDbDataAdapter
    Private tableE20Adapter As OleDb.OleDbDataAdapter
    Private DSCResultsAdapter As OleDb.OleDbDataAdapter
    Private tableE09AdapterSelectCommand As OleDb.OleDbCommand
    Private tableE10AdapterSelectCommand As OleDb.OleDbCommand
    Private tableE20AdapterSelectCommand As OleDb.OleDbCommand
    Private DSCResultsAdapterSelectCommand As OleDb.OleDbCommand

    Private tableE09 As DataTable
    Private tableE10 As DataTable
    Private tableE20 As DataTable
    Private DSCResults As DataTable

    Private validated As Boolean

    Public Sub New()
        MyBase.new()
    End Sub

    Public Sub New(ByVal modelRow As ModelCatalogDataset.ModelCatalogRow)
        Me.modelID = modelRow.modelID
        Me.scenarioID = modelRow.scenarioID
        Me.modelName = modelRow.modelName
        Me.modelDescription = modelRow.modelDescription
        Me.modelType = modelRow.modelType
        Me.modelPath = modelRow.modelPath
        Me.modelOutputFile = modelRow.modelOutputFile
        Me.userName = modelRow.userName
        Me.isUploaded = modelRow.isUploaded

        Me.modelResultsTable = Me.modelPath & "\mdbs\ModelResults.mdb"

        ModelConnectionString = New OleDb.OleDbConnection( _
            "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;" & _
            "Jet OLEDB:Database Password=;Data Source='" & Me.modelResultsTable & "'" & _
            ";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;" & _
            "Provider='Microsoft.Jet.OLEDB.4.0';Jet OLEDB:System database=;" & _
            "Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;" & _
            "Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;" & _
            "Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False")

        tableE09Adapter = New OleDb.OleDbDataAdapter
        tableE10Adapter = New OleDb.OleDbDataAdapter
        tableE20Adapter = New OleDb.OleDbDataAdapter
        DSCResultsAdapter = New OleDb.OleDbDataAdapter

        tableE09AdapterSelectCommand = New OleDb.OleDbCommand("Select * From tableE09")
        tableE10AdapterSelectCommand = New OleDb.OleDbCommand("Select * From tableE10")
        tableE20AdapterSelectCommand = New OleDb.OleDbCommand("Select * From tableE20")
        DSCResultsAdapterSelectCommand = New OleDb.OleDbCommand("Select * From _ComputeHGL")

        tableE09AdapterSelectCommand.Connection = ModelConnectionString
        tableE10AdapterSelectCommand.Connection = ModelConnectionString
        tableE20AdapterSelectCommand.Connection = ModelConnectionString
        DSCResultsAdapterSelectCommand.Connection = ModelConnectionString

        tableE09Adapter.SelectCommand = tableE09AdapterSelectCommand
        tableE10Adapter.SelectCommand = tableE10AdapterSelectCommand
        tableE20Adapter.SelectCommand = tableE20AdapterSelectCommand
        DSCResultsAdapter.SelectCommand = DSCResultsAdapterSelectCommand

        ResultsDataset = New DataSet("ModelResults")
        ResultsDataset.ReadXmlSchema("\\cassio\modeling\Model_Programs\ResultsWarehouse\ResultsWarehouseManager\ModelResultsSchema.xsd")

        tableE09 = New DataTable("tableE09")
        tableE10 = New DataTable("tableE10")
        tableE20 = New DataTable("tableE20")
        DSCResults = New DataTable("DSCResults")

        'ResultsDataset.Tables.Add(tableE09)
        'ResultsDataset.Tables.Add(tableE10)
        'ResultsDataset.Tables.Add(tableE20)
        'ResultsDataset.Tables.Add(DSCResults)
        Try
            tableE09Adapter.Fill(ResultsDataset, "tableE09")
            tableE10Adapter.Fill(ResultsDataset, "tableE10")
            tableE20Adapter.Fill(ResultsDataset, "tableE20")
            DSCResultsAdapter.Fill(ResultsDataset, "DSCResults")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Function processLinks()

    End Function

    Private Function processNodes()

    End Function

    Private Function processDSC()

    End Function

    Function processResults()
        'ProcessLinks
        'ProcessNodes
        'ProcessDSC
    End Function

    Function validateResults() As DataSet

    End Function

    Private Function validateLinks()

    End Function

    Private Function validateNodes()

    End Function

    Private Function validateDSC()

    End Function

    Public ReadOnly Property isValidated() As Boolean
        Get
            isValidated = Me.validated
        End Get
    End Property
End Class
