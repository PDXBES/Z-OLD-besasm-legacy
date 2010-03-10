Public Class ResultsWarehouseManagerForm
    Inherits System.Windows.Forms.Form

    Friend WithEvents frmEditModelCatalog1 As frmEditModelCatalog
    Friend WithEvents StatusBar1 As ProgressStatus

    Private mstNodes As MasterNodes
    Private mstLinks As MasterLinks
    Private mstDSC As MasterDSC

    Private dvDSCHydraulics As DataView
    Private dvNodeHydraulics As DataView
    Private dvLinkHydraulics As DataView

    Private iniFile As iniFile.INIFile
    Private resultsWarehouse As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        iniFile = New INIFile.INIFile("W:\Model_Programs\ResultsWarehouse\ResultsWarehouseManager.ini")
        resultsWarehouse = iniFile.GetINIString("ApplicationSettings", "resultsWarehouse", "")
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ResultsWarehouseConnection As System.Data.OleDb.OleDbConnection
    Friend WithEvents StormCatalogDataAdapter As System.Data.OleDb.OleDbDataAdapter
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents StormCatalogTab As System.Windows.Forms.TabPage
    Friend WithEvents ModelCatalogTab As System.Windows.Forms.TabPage
    Friend WithEvents ModelResultsTab As System.Windows.Forms.TabPage
    Friend WithEvents ModelCatalogDataAdapter As System.Data.OleDb.OleDbDataAdapter
    Friend WithEvents ModelScenarioDataAdapter As System.Data.OleDb.OleDbDataAdapter
    Friend WithEvents ModelScenarioSelectCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents ModelScenarioInsertCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents ModelScenarioUpdateCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents ModelScenarioDeleteCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents MenuItemFile As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemHelp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemHelp_About As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFile_Exit As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu As System.Windows.Forms.MainMenu
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents ModelCatalogDataset1 As ResultsWarehouseManager.ModelCatalogDataset
    Friend WithEvents ModelScenarioDataGrid As System.Windows.Forms.DataGrid
    Friend WithEvents StormCatalogDataGrid As System.Windows.Forms.DataGrid
    Friend WithEvents StormCatalogSelectCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents StormCatalogInsertCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents StormCatalogUpdateCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents StormCatalogDeleteCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents StormDataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents StormCatalogDataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StormCatalogDataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StormCatalogDataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StormCatalogDataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StormCatalogDataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StormCatalogDataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents StormCatalogDataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ModelScenarioDataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents ModelCatalogDataGrid As System.Windows.Forms.DataGrid
    Friend WithEvents ModelCatalogDataGridTableStyle As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents ModelCatalogPanel As System.Windows.Forms.Panel
    Friend WithEvents ModelCatalogDataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ModelCatalogDataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ModelCatalogDataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ModelCatalogDataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ModelCatalogDataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ModelCatalogDataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ModelCatalogDataGridBoolColumn6 As System.Windows.Forms.DataGridBoolColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnEditModelCatalog As System.Windows.Forms.Button
    Friend WithEvents DSCHydraulicsDataAdapter As System.Data.OleDb.OleDbDataAdapter
    Friend WithEvents LinkHydraulicsDataAdapter As System.Data.OleDb.OleDbDataAdapter
    Friend WithEvents NodeHydraulicsDataAdapter As System.Data.OleDb.OleDbDataAdapter
    Friend WithEvents btnGenerateReports As System.Windows.Forms.Button
    Friend WithEvents ModelScenarioGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnAddModel As System.Windows.Forms.Button
    Friend WithEvents btnBatchUpload As System.Windows.Forms.Button
    Friend WithEvents stormView As DataView
    Friend WithEvents OleDbSelectCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbInsertCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbUpdateCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbDeleteCommand1 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbSelectCommand2 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbInsertCommand2 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbUpdateCommand2 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbDeleteCommand2 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbSelectCommand3 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbInsertCommand3 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbUpdateCommand3 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbDeleteCommand3 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbSelectCommand4 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbInsertCommand4 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbUpdateCommand4 As System.Data.OleDb.OleDbCommand
    Friend WithEvents OleDbDeleteCommand4 As System.Data.OleDb.OleDbCommand
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ResultsWarehouseManagerForm))
        Me.ResultsWarehouseConnection = New System.Data.OleDb.OleDbConnection
        Me.StormCatalogDataAdapter = New System.Data.OleDb.OleDbDataAdapter
        Me.StormCatalogDeleteCommand1 = New System.Data.OleDb.OleDbCommand
        Me.StormCatalogInsertCommand1 = New System.Data.OleDb.OleDbCommand
        Me.StormCatalogSelectCommand1 = New System.Data.OleDb.OleDbCommand
        Me.StormCatalogUpdateCommand1 = New System.Data.OleDb.OleDbCommand
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.ModelCatalogTab = New System.Windows.Forms.TabPage
        Me.ModelCatalogPanel = New System.Windows.Forms.Panel
        Me.btnEditModelCatalog = New System.Windows.Forms.Button
        Me.btnGenerateReports = New System.Windows.Forms.Button
        Me.btnBatchUpload = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnAddModel = New System.Windows.Forms.Button
        Me.Splitter3 = New System.Windows.Forms.Splitter
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.ModelCatalogDataGrid = New System.Windows.Forms.DataGrid
        Me.ModelCatalogDataset1 = New ResultsWarehouseManager.ModelCatalogDataset
        Me.ModelCatalogDataGridTableStyle = New System.Windows.Forms.DataGridTableStyle
        Me.ModelCatalogDataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ModelCatalogDataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ModelCatalogDataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ModelCatalogDataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ModelCatalogDataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ModelCatalogDataGridBoolColumn6 = New System.Windows.Forms.DataGridBoolColumn
        Me.ModelCatalogDataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ModelScenarioDataGrid = New System.Windows.Forms.DataGrid
        Me.ModelScenarioDataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        Me.ModelScenarioGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StormCatalogDataGrid = New System.Windows.Forms.DataGrid
        Me.stormView = New System.Data.DataView
        Me.StormDataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        Me.StormCatalogDataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StormCatalogDataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StormCatalogDataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StormCatalogDataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StormCatalogDataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StormCatalogDataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.StormCatalogDataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ModelResultsTab = New System.Windows.Forms.TabPage
        Me.StormCatalogTab = New System.Windows.Forms.TabPage
        Me.DataGrid1 = New System.Windows.Forms.DataGrid
        Me.ModelCatalogDataAdapter = New System.Data.OleDb.OleDbDataAdapter
        Me.OleDbDeleteCommand4 = New System.Data.OleDb.OleDbCommand
        Me.OleDbInsertCommand4 = New System.Data.OleDb.OleDbCommand
        Me.OleDbSelectCommand4 = New System.Data.OleDb.OleDbCommand
        Me.OleDbUpdateCommand4 = New System.Data.OleDb.OleDbCommand
        Me.ModelScenarioDataAdapter = New System.Data.OleDb.OleDbDataAdapter
        Me.ModelScenarioDeleteCommand1 = New System.Data.OleDb.OleDbCommand
        Me.ModelScenarioInsertCommand1 = New System.Data.OleDb.OleDbCommand
        Me.ModelScenarioSelectCommand1 = New System.Data.OleDb.OleDbCommand
        Me.ModelScenarioUpdateCommand1 = New System.Data.OleDb.OleDbCommand
        Me.MainMenu = New System.Windows.Forms.MainMenu
        Me.MenuItemFile = New System.Windows.Forms.MenuItem
        Me.MenuItemFile_Exit = New System.Windows.Forms.MenuItem
        Me.MenuItemHelp = New System.Windows.Forms.MenuItem
        Me.MenuItemHelp_About = New System.Windows.Forms.MenuItem
        Me.DSCHydraulicsDataAdapter = New System.Data.OleDb.OleDbDataAdapter
        Me.OleDbDeleteCommand3 = New System.Data.OleDb.OleDbCommand
        Me.OleDbInsertCommand3 = New System.Data.OleDb.OleDbCommand
        Me.OleDbSelectCommand3 = New System.Data.OleDb.OleDbCommand
        Me.OleDbUpdateCommand3 = New System.Data.OleDb.OleDbCommand
        Me.LinkHydraulicsDataAdapter = New System.Data.OleDb.OleDbDataAdapter
        Me.OleDbDeleteCommand2 = New System.Data.OleDb.OleDbCommand
        Me.OleDbInsertCommand2 = New System.Data.OleDb.OleDbCommand
        Me.OleDbSelectCommand2 = New System.Data.OleDb.OleDbCommand
        Me.OleDbUpdateCommand2 = New System.Data.OleDb.OleDbCommand
        Me.NodeHydraulicsDataAdapter = New System.Data.OleDb.OleDbDataAdapter
        Me.OleDbDeleteCommand1 = New System.Data.OleDb.OleDbCommand
        Me.OleDbInsertCommand1 = New System.Data.OleDb.OleDbCommand
        Me.OleDbSelectCommand1 = New System.Data.OleDb.OleDbCommand
        Me.OleDbUpdateCommand1 = New System.Data.OleDb.OleDbCommand
        Me.TabControl1.SuspendLayout()
        Me.ModelCatalogTab.SuspendLayout()
        Me.ModelCatalogPanel.SuspendLayout()
        CType(Me.ModelCatalogDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModelCatalogDataset1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModelScenarioDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StormCatalogDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stormView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StormCatalogTab.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ResultsWarehouseConnection
        '
        Me.ResultsWarehouseConnection.ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database L" & _
        "ocking Mode=1;Jet OLEDB:Database Password=;Data Source='" & resultsWarehouse & "'" & _
        ";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Pro" & _
        "vider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;E" & _
        "xtended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OL" & _
        "EDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Je" & _
        "t OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Dat" & _
        "abase=False"
        '
        'StormCatalogDataAdapter
        '
        Me.StormCatalogDataAdapter.DeleteCommand = Me.StormCatalogDeleteCommand1
        Me.StormCatalogDataAdapter.InsertCommand = Me.StormCatalogInsertCommand1
        Me.StormCatalogDataAdapter.SelectCommand = Me.StormCatalogSelectCommand1
        Me.StormCatalogDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "StormCatalog", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("description", "description"), New System.Data.Common.DataColumnMapping("duration", "duration"), New System.Data.Common.DataColumnMapping("interfaceFile", "interfaceFile"), New System.Data.Common.DataColumnMapping("recurrenceInterval", "recurrenceInterval"), New System.Data.Common.DataColumnMapping("startDate", "startDate"), New System.Data.Common.DataColumnMapping("stormID", "stormID"), New System.Data.Common.DataColumnMapping("stormName", "stormName"), New System.Data.Common.DataColumnMapping("timeStep", "timeStep")})})
        Me.StormCatalogDataAdapter.UpdateCommand = Me.StormCatalogUpdateCommand1
        '
        'StormCatalogDeleteCommand1
        '
        Me.StormCatalogDeleteCommand1.CommandText = "DELETE FROM StormCatalog WHERE (stormID = ?) AND (description = ? OR ? IS NULL AN" & _
        "D description IS NULL) AND (duration = ? OR ? IS NULL AND duration IS NULL) AND " & _
        "(interfaceFile = ? OR ? IS NULL AND interfaceFile IS NULL) AND (recurrenceInterv" & _
        "al = ? OR ? IS NULL AND recurrenceInterval IS NULL) AND (startDate = ? OR ? IS N" & _
        "ULL AND startDate IS NULL) AND (stormName = ? OR ? IS NULL AND stormName IS NULL" & _
        ") AND (timeStep = ? OR ? IS NULL AND timeStep IS NULL)"
        Me.StormCatalogDeleteCommand1.Connection = Me.ResultsWarehouseConnection
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_stormID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "stormID", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_description", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "description", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_description1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "description", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_duration", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "duration", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_duration1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "duration", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_interfaceFile", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "interfaceFile", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_interfaceFile1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "interfaceFile", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_recurrenceInterval", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "recurrenceInterval", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_recurrenceInterval1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "recurrenceInterval", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_startDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "startDate", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_startDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "startDate", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_stormName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "stormName", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_stormName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "stormName", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeStep", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeStep", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeStep1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeStep", System.Data.DataRowVersion.Original, Nothing))
        '
        'StormCatalogInsertCommand1
        '
        Me.StormCatalogInsertCommand1.CommandText = "INSERT INTO StormCatalog(description, duration, interfaceFile, recurrenceInterval" & _
        ", startDate, stormID, stormName, timeStep) VALUES (?, ?, ?, ?, ?, ?, ?, ?)"
        Me.StormCatalogInsertCommand1.Connection = Me.ResultsWarehouseConnection
        Me.StormCatalogInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("description", System.Data.OleDb.OleDbType.VarWChar, 255, "description"))
        Me.StormCatalogInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("duration", System.Data.OleDb.OleDbType.Integer, 0, "duration"))
        Me.StormCatalogInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("interfaceFile", System.Data.OleDb.OleDbType.VarWChar, 255, "interfaceFile"))
        Me.StormCatalogInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("recurrenceInterval", System.Data.OleDb.OleDbType.Integer, 0, "recurrenceInterval"))
        Me.StormCatalogInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("startDate", System.Data.OleDb.OleDbType.DBDate, 0, "startDate"))
        Me.StormCatalogInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("stormID", System.Data.OleDb.OleDbType.Integer, 0, "stormID"))
        Me.StormCatalogInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("stormName", System.Data.OleDb.OleDbType.VarWChar, 50, "stormName"))
        Me.StormCatalogInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("timeStep", System.Data.OleDb.OleDbType.Integer, 0, "timeStep"))
        '
        'StormCatalogSelectCommand1
        '
        Me.StormCatalogSelectCommand1.CommandText = "SELECT description, duration, interfaceFile, recurrenceInterval, startDate, storm" & _
        "ID, stormName, timeStep FROM StormCatalog"
        Me.StormCatalogSelectCommand1.Connection = Me.ResultsWarehouseConnection
        '
        'StormCatalogUpdateCommand1
        '
        Me.StormCatalogUpdateCommand1.CommandText = "UPDATE StormCatalog SET description = ?, duration = ?, interfaceFile = ?, recurre" & _
        "nceInterval = ?, startDate = ?, stormID = ?, stormName = ?, timeStep = ? WHERE (" & _
        "stormID = ?) AND (description = ? OR ? IS NULL AND description IS NULL) AND (dur" & _
        "ation = ? OR ? IS NULL AND duration IS NULL) AND (interfaceFile = ? OR ? IS NULL" & _
        " AND interfaceFile IS NULL) AND (recurrenceInterval = ? OR ? IS NULL AND recurre" & _
        "nceInterval IS NULL) AND (startDate = ? OR ? IS NULL AND startDate IS NULL) AND " & _
        "(stormName = ? OR ? IS NULL AND stormName IS NULL) AND (timeStep = ? OR ? IS NUL" & _
        "L AND timeStep IS NULL)"
        Me.StormCatalogUpdateCommand1.Connection = Me.ResultsWarehouseConnection
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("description", System.Data.OleDb.OleDbType.VarWChar, 255, "description"))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("duration", System.Data.OleDb.OleDbType.Integer, 0, "duration"))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("interfaceFile", System.Data.OleDb.OleDbType.VarWChar, 255, "interfaceFile"))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("recurrenceInterval", System.Data.OleDb.OleDbType.Integer, 0, "recurrenceInterval"))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("startDate", System.Data.OleDb.OleDbType.DBDate, 0, "startDate"))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("stormID", System.Data.OleDb.OleDbType.Integer, 0, "stormID"))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("stormName", System.Data.OleDb.OleDbType.VarWChar, 50, "stormName"))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("timeStep", System.Data.OleDb.OleDbType.Integer, 0, "timeStep"))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_stormID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "stormID", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_description", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "description", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_description1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "description", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_duration", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "duration", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_duration1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "duration", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_interfaceFile", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "interfaceFile", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_interfaceFile1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "interfaceFile", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_recurrenceInterval", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "recurrenceInterval", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_recurrenceInterval1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "recurrenceInterval", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_startDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "startDate", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_startDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "startDate", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_stormName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "stormName", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_stormName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "stormName", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeStep", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeStep", System.Data.DataRowVersion.Original, Nothing))
        Me.StormCatalogUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeStep1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeStep", System.Data.DataRowVersion.Original, Nothing))
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.ModelCatalogTab)
        Me.TabControl1.Controls.Add(Me.ModelResultsTab)
        Me.TabControl1.Controls.Add(Me.StormCatalogTab)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.HotTrack = True
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(896, 401)
        Me.TabControl1.TabIndex = 0
        '
        'ModelCatalogTab
        '
        Me.ModelCatalogTab.Controls.Add(Me.ModelCatalogPanel)
        Me.ModelCatalogTab.Controls.Add(Me.Splitter1)
        Me.ModelCatalogTab.Controls.Add(Me.Splitter2)
        Me.ModelCatalogTab.Controls.Add(Me.ModelCatalogDataGrid)
        Me.ModelCatalogTab.Controls.Add(Me.ModelScenarioDataGrid)
        Me.ModelCatalogTab.Controls.Add(Me.StormCatalogDataGrid)
        Me.ModelCatalogTab.Location = New System.Drawing.Point(4, 22)
        Me.ModelCatalogTab.Name = "ModelCatalogTab"
        Me.ModelCatalogTab.Size = New System.Drawing.Size(888, 375)
        Me.ModelCatalogTab.TabIndex = 1
        Me.ModelCatalogTab.Text = "Model Catalog"
        '
        'ModelCatalogPanel
        '
        Me.ModelCatalogPanel.Controls.Add(Me.btnEditModelCatalog)
        Me.ModelCatalogPanel.Controls.Add(Me.btnGenerateReports)
        Me.ModelCatalogPanel.Controls.Add(Me.btnBatchUpload)
        Me.ModelCatalogPanel.Controls.Add(Me.Button2)
        Me.ModelCatalogPanel.Controls.Add(Me.btnAddModel)
        Me.ModelCatalogPanel.Controls.Add(Me.Splitter3)
        Me.ModelCatalogPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ModelCatalogPanel.Location = New System.Drawing.Point(259, 232)
        Me.ModelCatalogPanel.Name = "ModelCatalogPanel"
        Me.ModelCatalogPanel.Size = New System.Drawing.Size(629, 76)
        Me.ModelCatalogPanel.TabIndex = 5
        '
        'btnEditModelCatalog
        '
        Me.btnEditModelCatalog.Location = New System.Drawing.Point(112, 8)
        Me.btnEditModelCatalog.Name = "btnEditModelCatalog"
        Me.btnEditModelCatalog.Size = New System.Drawing.Size(80, 32)
        Me.btnEditModelCatalog.TabIndex = 5
        Me.btnEditModelCatalog.Text = "&Edit Model"
        '
        'btnGenerateReports
        '
        Me.btnGenerateReports.Location = New System.Drawing.Point(416, 8)
        Me.btnGenerateReports.Name = "btnGenerateReports"
        Me.btnGenerateReports.Size = New System.Drawing.Size(88, 32)
        Me.btnGenerateReports.TabIndex = 4
        Me.btnGenerateReports.Text = "&Generate Reports"
        '
        'btnBatchUpload
        '
        Me.btnBatchUpload.Location = New System.Drawing.Point(312, 8)
        Me.btnBatchUpload.Name = "btnBatchUpload"
        Me.btnBatchUpload.Size = New System.Drawing.Size(88, 32)
        Me.btnBatchUpload.TabIndex = 3
        Me.btnBatchUpload.Text = "&Batch Upload Models"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(208, 8)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(88, 32)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "&Delete Model"
        '
        'btnAddModel
        '
        Me.btnAddModel.Location = New System.Drawing.Point(8, 8)
        Me.btnAddModel.Name = "btnAddModel"
        Me.btnAddModel.Size = New System.Drawing.Size(88, 32)
        Me.btnAddModel.TabIndex = 1
        Me.btnAddModel.Text = "&Add New Model"
        '
        'Splitter3
        '
        Me.Splitter3.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Location = New System.Drawing.Point(0, 0)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(629, 3)
        Me.Splitter3.TabIndex = 0
        Me.Splitter3.TabStop = False
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Splitter1.Location = New System.Drawing.Point(256, 232)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 76)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Splitter2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter2.Location = New System.Drawing.Point(256, 308)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(632, 3)
        Me.Splitter2.TabIndex = 4
        Me.Splitter2.TabStop = False
        '
        'ModelCatalogDataGrid
        '
        Me.ModelCatalogDataGrid.AllowNavigation = False
        Me.ModelCatalogDataGrid.CaptionText = "Model Catalog"
        Me.ModelCatalogDataGrid.DataMember = "ModelScenario.ModelScenarioModelCatalog"
        Me.ModelCatalogDataGrid.DataSource = Me.ModelCatalogDataset1
        Me.ModelCatalogDataGrid.Dock = System.Windows.Forms.DockStyle.Top
        Me.ModelCatalogDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.ModelCatalogDataGrid.Location = New System.Drawing.Point(256, 0)
        Me.ModelCatalogDataGrid.Name = "ModelCatalogDataGrid"
        Me.ModelCatalogDataGrid.Size = New System.Drawing.Size(632, 232)
        Me.ModelCatalogDataGrid.TabIndex = 6
        Me.ModelCatalogDataGrid.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.ModelCatalogDataGridTableStyle})
        '
        'ModelCatalogDataset1
        '
        Me.ModelCatalogDataset1.DataSetName = "ModelCatalogDataset"
        Me.ModelCatalogDataset1.Locale = New System.Globalization.CultureInfo("en-US")
        '
        'ModelCatalogDataGridTableStyle
        '
        Me.ModelCatalogDataGridTableStyle.DataGrid = Me.ModelCatalogDataGrid
        Me.ModelCatalogDataGridTableStyle.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.ModelCatalogDataGridTextBoxColumn1, Me.ModelCatalogDataGridTextBoxColumn2, Me.ModelCatalogDataGridTextBoxColumn3, Me.ModelCatalogDataGridTextBoxColumn4, Me.ModelCatalogDataGridTextBoxColumn5, Me.ModelCatalogDataGridBoolColumn6, Me.ModelCatalogDataGridTextBoxColumn7})
        Me.ModelCatalogDataGridTableStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.ModelCatalogDataGridTableStyle.MappingName = "ModelCatalog"
        '
        'ModelCatalogDataGridTextBoxColumn1
        '
        Me.ModelCatalogDataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModelCatalogDataGridTextBoxColumn1.Format = ""
        Me.ModelCatalogDataGridTextBoxColumn1.FormatInfo = Nothing
        Me.ModelCatalogDataGridTextBoxColumn1.HeaderText = "ID"
        Me.ModelCatalogDataGridTextBoxColumn1.MappingName = "modelID"
        Me.ModelCatalogDataGridTextBoxColumn1.Width = 50
        '
        'ModelCatalogDataGridTextBoxColumn2
        '
        Me.ModelCatalogDataGridTextBoxColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModelCatalogDataGridTextBoxColumn2.Format = ""
        Me.ModelCatalogDataGridTextBoxColumn2.FormatInfo = Nothing
        Me.ModelCatalogDataGridTextBoxColumn2.HeaderText = "Name"
        Me.ModelCatalogDataGridTextBoxColumn2.MappingName = "modelName"
        Me.ModelCatalogDataGridTextBoxColumn2.Width = 110
        '
        'ModelCatalogDataGridTextBoxColumn3
        '
        Me.ModelCatalogDataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModelCatalogDataGridTextBoxColumn3.Format = ""
        Me.ModelCatalogDataGridTextBoxColumn3.FormatInfo = Nothing
        Me.ModelCatalogDataGridTextBoxColumn3.HeaderText = "Description"
        Me.ModelCatalogDataGridTextBoxColumn3.MappingName = "modelDescription"
        Me.ModelCatalogDataGridTextBoxColumn3.Width = 175
        '
        'ModelCatalogDataGridTextBoxColumn4
        '
        Me.ModelCatalogDataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModelCatalogDataGridTextBoxColumn4.Format = ""
        Me.ModelCatalogDataGridTextBoxColumn4.FormatInfo = Nothing
        Me.ModelCatalogDataGridTextBoxColumn4.HeaderText = "Type"
        Me.ModelCatalogDataGridTextBoxColumn4.MappingName = "modelType"
        Me.ModelCatalogDataGridTextBoxColumn4.Width = 35
        '
        'ModelCatalogDataGridTextBoxColumn5
        '
        Me.ModelCatalogDataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModelCatalogDataGridTextBoxColumn5.Format = ""
        Me.ModelCatalogDataGridTextBoxColumn5.FormatInfo = Nothing
        Me.ModelCatalogDataGridTextBoxColumn5.HeaderText = "Modeler"
        Me.ModelCatalogDataGridTextBoxColumn5.MappingName = "userName"
        Me.ModelCatalogDataGridTextBoxColumn5.Width = 75
        '
        'ModelCatalogDataGridBoolColumn6
        '
        Me.ModelCatalogDataGridBoolColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModelCatalogDataGridBoolColumn6.AllowNull = False
        Me.ModelCatalogDataGridBoolColumn6.FalseValue = "False"
        Me.ModelCatalogDataGridBoolColumn6.HeaderText = "Uploaded?"
        Me.ModelCatalogDataGridBoolColumn6.MappingName = "isUploaded"
        Me.ModelCatalogDataGridBoolColumn6.NullValue = CType(resources.GetObject("ModelCatalogDataGridBoolColumn6.NullValue"), Object)
        Me.ModelCatalogDataGridBoolColumn6.TrueValue = "True"
        Me.ModelCatalogDataGridBoolColumn6.Width = 60
        '
        'ModelCatalogDataGridTextBoxColumn7
        '
        Me.ModelCatalogDataGridTextBoxColumn7.Format = ""
        Me.ModelCatalogDataGridTextBoxColumn7.FormatInfo = Nothing
        Me.ModelCatalogDataGridTextBoxColumn7.HeaderText = "Upload Date"
        Me.ModelCatalogDataGridTextBoxColumn7.MappingName = "uploadDate"
        Me.ModelCatalogDataGridTextBoxColumn7.NullText = ""
        Me.ModelCatalogDataGridTextBoxColumn7.Width = 75
        '
        'ModelScenarioDataGrid
        '
        Me.ModelScenarioDataGrid.AllowNavigation = False
        Me.ModelScenarioDataGrid.CaptionText = "Model Scenario"
        Me.ModelScenarioDataGrid.DataMember = "ModelScenario"
        Me.ModelScenarioDataGrid.DataSource = Me.ModelCatalogDataset1
        Me.ModelScenarioDataGrid.Dock = System.Windows.Forms.DockStyle.Left
        Me.ModelScenarioDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.ModelScenarioDataGrid.Location = New System.Drawing.Point(0, 0)
        Me.ModelScenarioDataGrid.Name = "ModelScenarioDataGrid"
        Me.ModelScenarioDataGrid.ReadOnly = True
        Me.ModelScenarioDataGrid.RowHeaderWidth = 30
        Me.ModelScenarioDataGrid.Size = New System.Drawing.Size(256, 311)
        Me.ModelScenarioDataGrid.TabIndex = 1
        Me.ModelScenarioDataGrid.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.ModelScenarioDataGridTableStyle1})
        '
        'ModelScenarioDataGridTableStyle1
        '
        Me.ModelScenarioDataGridTableStyle1.DataGrid = Me.ModelScenarioDataGrid
        Me.ModelScenarioDataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.ModelScenarioGridTextBoxColumn1})
        Me.ModelScenarioDataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.ModelScenarioDataGridTableStyle1.MappingName = "ModelScenario"
        Me.ModelScenarioDataGridTableStyle1.RowHeaderWidth = 30
        '
        'ModelScenarioGridTextBoxColumn1
        '
        Me.ModelScenarioGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModelScenarioGridTextBoxColumn1.Format = ""
        Me.ModelScenarioGridTextBoxColumn1.FormatInfo = Nothing
        Me.ModelScenarioGridTextBoxColumn1.HeaderText = "Scenario"
        Me.ModelScenarioGridTextBoxColumn1.MappingName = "description"
        Me.ModelScenarioGridTextBoxColumn1.Width = 220
        '
        'StormCatalogDataGrid
        '
        Me.StormCatalogDataGrid.AllowNavigation = False
        Me.StormCatalogDataGrid.CaptionText = "Storm Information"
        Me.StormCatalogDataGrid.DataMember = ""
        Me.StormCatalogDataGrid.DataSource = Me.stormView
        Me.StormCatalogDataGrid.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StormCatalogDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.StormCatalogDataGrid.Location = New System.Drawing.Point(0, 311)
        Me.StormCatalogDataGrid.Name = "StormCatalogDataGrid"
        Me.StormCatalogDataGrid.ReadOnly = True
        Me.StormCatalogDataGrid.RowHeadersVisible = False
        Me.StormCatalogDataGrid.Size = New System.Drawing.Size(888, 64)
        Me.StormCatalogDataGrid.TabIndex = 2
        Me.StormCatalogDataGrid.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.StormDataGridTableStyle1})
        '
        'stormView
        '
        Me.stormView.Table = Me.ModelCatalogDataset1.StormCatalog
        '
        'StormDataGridTableStyle1
        '
        Me.StormDataGridTableStyle1.DataGrid = Me.StormCatalogDataGrid
        Me.StormDataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.StormCatalogDataGridTextBoxColumn1, Me.StormCatalogDataGridTextBoxColumn2, Me.StormCatalogDataGridTextBoxColumn3, Me.StormCatalogDataGridTextBoxColumn4, Me.StormCatalogDataGridTextBoxColumn5, Me.StormCatalogDataGridTextBoxColumn6, Me.StormCatalogDataGridTextBoxColumn7})
        Me.StormDataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.StormDataGridTableStyle1.MappingName = "StormCatalog"
        Me.StormDataGridTableStyle1.RowHeadersVisible = False
        '
        'StormCatalogDataGridTextBoxColumn1
        '
        Me.StormCatalogDataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StormCatalogDataGridTextBoxColumn1.Format = ""
        Me.StormCatalogDataGridTextBoxColumn1.FormatInfo = Nothing
        Me.StormCatalogDataGridTextBoxColumn1.HeaderText = "Storm Name"
        Me.StormCatalogDataGridTextBoxColumn1.MappingName = "stormName"
        Me.StormCatalogDataGridTextBoxColumn1.Width = 150
        '
        'StormCatalogDataGridTextBoxColumn2
        '
        Me.StormCatalogDataGridTextBoxColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StormCatalogDataGridTextBoxColumn2.Format = ""
        Me.StormCatalogDataGridTextBoxColumn2.FormatInfo = Nothing
        Me.StormCatalogDataGridTextBoxColumn2.HeaderText = "Duration"
        Me.StormCatalogDataGridTextBoxColumn2.MappingName = "duration"
        Me.StormCatalogDataGridTextBoxColumn2.Width = 55
        '
        'StormCatalogDataGridTextBoxColumn3
        '
        Me.StormCatalogDataGridTextBoxColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StormCatalogDataGridTextBoxColumn3.Format = ""
        Me.StormCatalogDataGridTextBoxColumn3.FormatInfo = Nothing
        Me.StormCatalogDataGridTextBoxColumn3.HeaderText = "Recurrence"
        Me.StormCatalogDataGridTextBoxColumn3.MappingName = "recurrenceInterval"
        Me.StormCatalogDataGridTextBoxColumn3.Width = 70
        '
        'StormCatalogDataGridTextBoxColumn4
        '
        Me.StormCatalogDataGridTextBoxColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StormCatalogDataGridTextBoxColumn4.Format = ""
        Me.StormCatalogDataGridTextBoxColumn4.FormatInfo = Nothing
        Me.StormCatalogDataGridTextBoxColumn4.HeaderText = "Start Date"
        Me.StormCatalogDataGridTextBoxColumn4.MappingName = "startDate"
        Me.StormCatalogDataGridTextBoxColumn4.Width = 65
        '
        'StormCatalogDataGridTextBoxColumn5
        '
        Me.StormCatalogDataGridTextBoxColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StormCatalogDataGridTextBoxColumn5.Format = ""
        Me.StormCatalogDataGridTextBoxColumn5.FormatInfo = Nothing
        Me.StormCatalogDataGridTextBoxColumn5.HeaderText = "Time Step"
        Me.StormCatalogDataGridTextBoxColumn5.MappingName = "timeStep"
        Me.StormCatalogDataGridTextBoxColumn5.Width = 65
        '
        'StormCatalogDataGridTextBoxColumn6
        '
        Me.StormCatalogDataGridTextBoxColumn6.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StormCatalogDataGridTextBoxColumn6.Format = ""
        Me.StormCatalogDataGridTextBoxColumn6.FormatInfo = Nothing
        Me.StormCatalogDataGridTextBoxColumn6.HeaderText = "Interface File"
        Me.StormCatalogDataGridTextBoxColumn6.MappingName = "interfaceFile"
        Me.StormCatalogDataGridTextBoxColumn6.Width = 275
        '
        'StormCatalogDataGridTextBoxColumn7
        '
        Me.StormCatalogDataGridTextBoxColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.StormCatalogDataGridTextBoxColumn7.Format = ""
        Me.StormCatalogDataGridTextBoxColumn7.FormatInfo = Nothing
        Me.StormCatalogDataGridTextBoxColumn7.HeaderText = "Description"
        Me.StormCatalogDataGridTextBoxColumn7.MappingName = "description"
        Me.StormCatalogDataGridTextBoxColumn7.NullText = ""
        Me.StormCatalogDataGridTextBoxColumn7.Width = 200
        '
        'ModelResultsTab
        '
        Me.ModelResultsTab.Location = New System.Drawing.Point(4, 22)
        Me.ModelResultsTab.Name = "ModelResultsTab"
        Me.ModelResultsTab.Size = New System.Drawing.Size(888, 375)
        Me.ModelResultsTab.TabIndex = 2
        Me.ModelResultsTab.Text = "Model Results"
        '
        'StormCatalogTab
        '
        Me.StormCatalogTab.Controls.Add(Me.DataGrid1)
        Me.StormCatalogTab.Location = New System.Drawing.Point(4, 22)
        Me.StormCatalogTab.Name = "StormCatalogTab"
        Me.StormCatalogTab.Size = New System.Drawing.Size(888, 375)
        Me.StormCatalogTab.TabIndex = 0
        Me.StormCatalogTab.Text = "Storm Catalog"
        '
        'DataGrid1
        '
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGrid1.Location = New System.Drawing.Point(8, 168)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.Size = New System.Drawing.Size(648, 80)
        Me.DataGrid1.TabIndex = 0
        '
        'ModelCatalogDataAdapter
        '
        Me.ModelCatalogDataAdapter.DeleteCommand = Me.OleDbDeleteCommand4
        Me.ModelCatalogDataAdapter.InsertCommand = Me.OleDbInsertCommand4
        Me.ModelCatalogDataAdapter.SelectCommand = Me.OleDbSelectCommand4
        Me.ModelCatalogDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ModelCatalog", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("isUploaded", "isUploaded"), New System.Data.Common.DataColumnMapping("modelDescription", "modelDescription"), New System.Data.Common.DataColumnMapping("modelID", "modelID"), New System.Data.Common.DataColumnMapping("modelName", "modelName"), New System.Data.Common.DataColumnMapping("modelOutputFile", "modelOutputFile"), New System.Data.Common.DataColumnMapping("modelPath", "modelPath"), New System.Data.Common.DataColumnMapping("modelType", "modelType"), New System.Data.Common.DataColumnMapping("scenarioID", "scenarioID"), New System.Data.Common.DataColumnMapping("uploadDate", "uploadDate"), New System.Data.Common.DataColumnMapping("userName", "userName")})})
        Me.ModelCatalogDataAdapter.UpdateCommand = Me.OleDbUpdateCommand4
        '
        'OleDbDeleteCommand4
        '
        Me.OleDbDeleteCommand4.CommandText = "DELETE FROM ModelCatalog WHERE (modelID = ?) AND (isUploaded = ?) AND (modelDescr" & _
        "iption = ? OR ? IS NULL AND modelDescription IS NULL) AND (modelName = ? OR ? IS" & _
        " NULL AND modelName IS NULL) AND (modelOutputFile = ? OR ? IS NULL AND modelOutp" & _
        "utFile IS NULL) AND (modelPath = ? OR ? IS NULL AND modelPath IS NULL) AND (mode" & _
        "lType = ? OR ? IS NULL AND modelType IS NULL) AND (scenarioID = ?) AND (uploadDa" & _
        "te = ? OR ? IS NULL AND uploadDate IS NULL) AND (userName = ? OR ? IS NULL AND u" & _
        "serName IS NULL)"
        Me.OleDbDeleteCommand4.Connection = Me.ResultsWarehouseConnection
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_isUploaded", System.Data.OleDb.OleDbType.Boolean, 2, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "isUploaded", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelDescription", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelDescription", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelDescription1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelDescription", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelName", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelName", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelOutputFile", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelOutputFile", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelOutputFile1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelOutputFile", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelPath", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelPath", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelPath1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelPath", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelType", System.Data.OleDb.OleDbType.VarWChar, 3, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelType", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelType1", System.Data.OleDb.OleDbType.VarWChar, 3, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelType", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "scenarioID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_uploadDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "uploadDate", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_uploadDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "uploadDate", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_userName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "userName", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_userName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "userName", System.Data.DataRowVersion.Original, Nothing))
        '
        'OleDbInsertCommand4
        '
        Me.OleDbInsertCommand4.CommandText = "INSERT INTO ModelCatalog(isUploaded, modelDescription, modelName, modelOutputFile" & _
        ", modelPath, modelType, scenarioID, uploadDate, userName) VALUES (?, ?, ?, ?, ?," & _
        " ?, ?, ?, ?)"
        Me.OleDbInsertCommand4.Connection = Me.ResultsWarehouseConnection
        Me.OleDbInsertCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("isUploaded", System.Data.OleDb.OleDbType.Boolean, 2, "isUploaded"))
        Me.OleDbInsertCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelDescription", System.Data.OleDb.OleDbType.VarWChar, 255, "modelDescription"))
        Me.OleDbInsertCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelName", System.Data.OleDb.OleDbType.VarWChar, 50, "modelName"))
        Me.OleDbInsertCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelOutputFile", System.Data.OleDb.OleDbType.VarWChar, 255, "modelOutputFile"))
        Me.OleDbInsertCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelPath", System.Data.OleDb.OleDbType.VarWChar, 255, "modelPath"))
        Me.OleDbInsertCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelType", System.Data.OleDb.OleDbType.VarWChar, 3, "modelType"))
        Me.OleDbInsertCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"))
        Me.OleDbInsertCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("uploadDate", System.Data.OleDb.OleDbType.DBDate, 0, "uploadDate"))
        Me.OleDbInsertCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("userName", System.Data.OleDb.OleDbType.VarWChar, 50, "userName"))
        '
        'OleDbSelectCommand4
        '
        Me.OleDbSelectCommand4.CommandText = "SELECT isUploaded, modelDescription, modelID, modelName, modelOutputFile, modelPa" & _
        "th, modelType, scenarioID, uploadDate, userName FROM ModelCatalog"
        Me.OleDbSelectCommand4.Connection = Me.ResultsWarehouseConnection
        '
        'OleDbUpdateCommand4
        '
        Me.OleDbUpdateCommand4.CommandText = "UPDATE ModelCatalog SET isUploaded = ?, modelDescription = ?, modelName = ?, mode" & _
        "lOutputFile = ?, modelPath = ?, modelType = ?, scenarioID = ?, uploadDate = ?, u" & _
        "serName = ? WHERE (modelID = ?) AND (isUploaded = ?) AND (modelDescription = ? O" & _
        "R ? IS NULL AND modelDescription IS NULL) AND (modelName = ? OR ? IS NULL AND mo" & _
        "delName IS NULL) AND (modelOutputFile = ? OR ? IS NULL AND modelOutputFile IS NU" & _
        "LL) AND (modelPath = ? OR ? IS NULL AND modelPath IS NULL) AND (modelType = ? OR" & _
        " ? IS NULL AND modelType IS NULL) AND (scenarioID = ?) AND (uploadDate = ? OR ? " & _
        "IS NULL AND uploadDate IS NULL) AND (userName = ? OR ? IS NULL AND userName IS N" & _
        "ULL)"
        Me.OleDbUpdateCommand4.Connection = Me.ResultsWarehouseConnection
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("isUploaded", System.Data.OleDb.OleDbType.Boolean, 2, "isUploaded"))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelDescription", System.Data.OleDb.OleDbType.VarWChar, 255, "modelDescription"))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelName", System.Data.OleDb.OleDbType.VarWChar, 50, "modelName"))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelOutputFile", System.Data.OleDb.OleDbType.VarWChar, 255, "modelOutputFile"))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelPath", System.Data.OleDb.OleDbType.VarWChar, 255, "modelPath"))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelType", System.Data.OleDb.OleDbType.VarWChar, 3, "modelType"))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("uploadDate", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "uploadDate"))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("userName", System.Data.OleDb.OleDbType.VarWChar, 50, "userName"))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_isUploaded", System.Data.OleDb.OleDbType.Boolean, 2, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "isUploaded", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelDescription", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelDescription", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelDescription1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelDescription", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelName", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelName", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelOutputFile", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelOutputFile", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelOutputFile1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelOutputFile", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelPath", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelPath", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelPath1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelPath", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelType", System.Data.OleDb.OleDbType.VarWChar, 3, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelType", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelType1", System.Data.OleDb.OleDbType.VarWChar, 3, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelType", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "scenarioID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_uploadDate", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "uploadDate", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_uploadDate1", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "uploadDate", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_userName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "userName", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand4.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_userName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "userName", System.Data.DataRowVersion.Original, Nothing))
        '
        'ModelScenarioDataAdapter
        '
        Me.ModelScenarioDataAdapter.DeleteCommand = Me.ModelScenarioDeleteCommand1
        Me.ModelScenarioDataAdapter.InsertCommand = Me.ModelScenarioInsertCommand1
        Me.ModelScenarioDataAdapter.SelectCommand = Me.ModelScenarioSelectCommand1
        Me.ModelScenarioDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ModelScenario", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("description", "description"), New System.Data.Common.DataColumnMapping("scenarioID", "scenarioID"), New System.Data.Common.DataColumnMapping("stormID", "stormID")})})
        Me.ModelScenarioDataAdapter.UpdateCommand = Me.ModelScenarioUpdateCommand1
        '
        'ModelScenarioDeleteCommand1
        '
        Me.ModelScenarioDeleteCommand1.CommandText = "DELETE FROM ModelScenario WHERE (scenarioID = ?) AND (description = ? OR ? IS NUL" & _
        "L AND description IS NULL) AND (stormID = ? OR ? IS NULL AND stormID IS NULL)"
        Me.ModelScenarioDeleteCommand1.Connection = Me.ResultsWarehouseConnection
        Me.ModelScenarioDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "scenarioID", System.Data.DataRowVersion.Original, Nothing))
        Me.ModelScenarioDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_description", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "description", System.Data.DataRowVersion.Original, Nothing))
        Me.ModelScenarioDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_description1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "description", System.Data.DataRowVersion.Original, Nothing))
        Me.ModelScenarioDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_stormID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "stormID", System.Data.DataRowVersion.Original, Nothing))
        Me.ModelScenarioDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_stormID1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "stormID", System.Data.DataRowVersion.Original, Nothing))
        '
        'ModelScenarioInsertCommand1
        '
        Me.ModelScenarioInsertCommand1.CommandText = "INSERT INTO ModelScenario(description, scenarioID, stormID) VALUES (?, ?, ?)"
        Me.ModelScenarioInsertCommand1.Connection = Me.ResultsWarehouseConnection
        Me.ModelScenarioInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("description", System.Data.OleDb.OleDbType.VarWChar, 50, "description"))
        Me.ModelScenarioInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"))
        Me.ModelScenarioInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("stormID", System.Data.OleDb.OleDbType.Integer, 0, "stormID"))
        '
        'ModelScenarioSelectCommand1
        '
        Me.ModelScenarioSelectCommand1.CommandText = "SELECT description, scenarioID, stormID FROM ModelScenario"
        Me.ModelScenarioSelectCommand1.Connection = Me.ResultsWarehouseConnection
        '
        'ModelScenarioUpdateCommand1
        '
        Me.ModelScenarioUpdateCommand1.CommandText = "UPDATE ModelScenario SET description = ?, scenarioID = ?, stormID = ? WHERE (scen" & _
        "arioID = ?) AND (description = ? OR ? IS NULL AND description IS NULL) AND (stor" & _
        "mID = ? OR ? IS NULL AND stormID IS NULL)"
        Me.ModelScenarioUpdateCommand1.Connection = Me.ResultsWarehouseConnection
        Me.ModelScenarioUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("description", System.Data.OleDb.OleDbType.VarWChar, 50, "description"))
        Me.ModelScenarioUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"))
        Me.ModelScenarioUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("stormID", System.Data.OleDb.OleDbType.Integer, 0, "stormID"))
        Me.ModelScenarioUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "scenarioID", System.Data.DataRowVersion.Original, Nothing))
        Me.ModelScenarioUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_description", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "description", System.Data.DataRowVersion.Original, Nothing))
        Me.ModelScenarioUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_description1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "description", System.Data.DataRowVersion.Original, Nothing))
        Me.ModelScenarioUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_stormID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "stormID", System.Data.DataRowVersion.Original, Nothing))
        Me.ModelScenarioUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_stormID1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "stormID", System.Data.DataRowVersion.Original, Nothing))
        '
        'MainMenu
        '
        Me.MainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemFile, Me.MenuItemHelp})
        '
        'MenuItemFile
        '
        Me.MenuItemFile.Index = 0
        Me.MenuItemFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemFile_Exit})
        Me.MenuItemFile.Text = "File"
        '
        'MenuItemFile_Exit
        '
        Me.MenuItemFile_Exit.Index = 0
        Me.MenuItemFile_Exit.Text = "Exit"
        '
        'MenuItemHelp
        '
        Me.MenuItemHelp.Index = 1
        Me.MenuItemHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemHelp_About})
        Me.MenuItemHelp.Text = "Help"
        '
        'MenuItemHelp_About
        '
        Me.MenuItemHelp_About.Index = 0
        Me.MenuItemHelp_About.Text = "About"
        '
        'DSCHydraulicsDataAdapter
        '
        Me.DSCHydraulicsDataAdapter.DeleteCommand = Me.OleDbDeleteCommand3
        Me.DSCHydraulicsDataAdapter.InsertCommand = Me.OleDbInsertCommand3
        Me.DSCHydraulicsDataAdapter.SelectCommand = Me.OleDbSelectCommand3
        Me.DSCHydraulicsDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "DSCHydraulics", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("deltaHGL", "deltaHGL"), New System.Data.Common.DataColumnMapping("DSCHydraulicsID", "DSCHydraulicsID"), New System.Data.Common.DataColumnMapping("DSCID", "DSCID"), New System.Data.Common.DataColumnMapping("HGL", "HGL"), New System.Data.Common.DataColumnMapping("modelID", "modelID"), New System.Data.Common.DataColumnMapping("scenarioID", "scenarioID"), New System.Data.Common.DataColumnMapping("surcharge", "surcharge")})})
        Me.DSCHydraulicsDataAdapter.UpdateCommand = Me.OleDbUpdateCommand3
        '
        'OleDbDeleteCommand3
        '
        Me.OleDbDeleteCommand3.CommandText = "DELETE FROM DSCHydraulics WHERE (DSCHydraulicsID = ?) AND (DSCID = ?) AND (HGL = " & _
        "? OR ? IS NULL AND HGL IS NULL) AND (deltaHGL = ? OR ? IS NULL AND deltaHGL IS N" & _
        "ULL) AND (modelID = ? OR ? IS NULL AND modelID IS NULL) AND (scenarioID = ?) AND" & _
        " (surcharge = ? OR ? IS NULL AND surcharge IS NULL)"
        Me.OleDbDeleteCommand3.Connection = Me.ResultsWarehouseConnection
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_DSCHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "DSCHydraulicsID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_DSCID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "DSCID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_HGL", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "HGL", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_HGL1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "HGL", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_deltaHGL", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "deltaHGL", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_deltaHGL1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "deltaHGL", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "scenarioID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surcharge", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surcharge", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surcharge1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surcharge", System.Data.DataRowVersion.Original, Nothing))
        '
        'OleDbInsertCommand3
        '
        Me.OleDbInsertCommand3.CommandText = "INSERT INTO DSCHydraulics(deltaHGL, DSCID, HGL, modelID, scenarioID, surcharge) V" & _
        "ALUES (?, ?, ?, ?, ?, ?)"
        Me.OleDbInsertCommand3.Connection = Me.ResultsWarehouseConnection
        Me.OleDbInsertCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("deltaHGL", System.Data.OleDb.OleDbType.Double, 0, "deltaHGL"))
        Me.OleDbInsertCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("DSCID", System.Data.OleDb.OleDbType.Integer, 0, "DSCID"))
        Me.OleDbInsertCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("HGL", System.Data.OleDb.OleDbType.Double, 0, "HGL"))
        Me.OleDbInsertCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"))
        Me.OleDbInsertCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"))
        Me.OleDbInsertCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("surcharge", System.Data.OleDb.OleDbType.Double, 0, "surcharge"))
        '
        'OleDbSelectCommand3
        '
        Me.OleDbSelectCommand3.CommandText = "SELECT deltaHGL, DSCHydraulicsID, DSCID, HGL, modelID, scenarioID, surcharge FROM" & _
        " DSCHydraulics"
        Me.OleDbSelectCommand3.Connection = Me.ResultsWarehouseConnection
        '
        'OleDbUpdateCommand3
        '
        Me.OleDbUpdateCommand3.CommandText = "UPDATE DSCHydraulics SET deltaHGL = ?, DSCID = ?, HGL = ?, modelID = ?, scenarioI" & _
        "D = ?, surcharge = ? WHERE (DSCHydraulicsID = ?) AND (DSCID = ?) AND (HGL = ? OR" & _
        " ? IS NULL AND HGL IS NULL) AND (deltaHGL = ? OR ? IS NULL AND deltaHGL IS NULL)" & _
        " AND (modelID = ? OR ? IS NULL AND modelID IS NULL) AND (scenarioID = ?) AND (su" & _
        "rcharge = ? OR ? IS NULL AND surcharge IS NULL)"
        Me.OleDbUpdateCommand3.Connection = Me.ResultsWarehouseConnection
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("deltaHGL", System.Data.OleDb.OleDbType.Double, 0, "deltaHGL"))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("DSCID", System.Data.OleDb.OleDbType.Integer, 0, "DSCID"))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("HGL", System.Data.OleDb.OleDbType.Double, 0, "HGL"))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("surcharge", System.Data.OleDb.OleDbType.Double, 0, "surcharge"))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_DSCHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "DSCHydraulicsID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_DSCID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "DSCID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_HGL", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "HGL", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_HGL1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "HGL", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_deltaHGL", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "deltaHGL", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_deltaHGL1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "deltaHGL", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "scenarioID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surcharge", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surcharge", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand3.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surcharge1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surcharge", System.Data.DataRowVersion.Original, Nothing))
        '
        'LinkHydraulicsDataAdapter
        '
        Me.LinkHydraulicsDataAdapter.DeleteCommand = Me.OleDbDeleteCommand2
        Me.LinkHydraulicsDataAdapter.InsertCommand = Me.OleDbInsertCommand2
        Me.LinkHydraulicsDataAdapter.SelectCommand = Me.OleDbSelectCommand2
        Me.LinkHydraulicsDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LinkHydraulics", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("linkHydraulicsID", "linkHydraulicsID"), New System.Data.Common.DataColumnMapping("maxDSElev", "maxDSElev"), New System.Data.Common.DataColumnMapping("maxQ", "maxQ"), New System.Data.Common.DataColumnMapping("maxUSElev", "maxUSElev"), New System.Data.Common.DataColumnMapping("maxV", "maxV"), New System.Data.Common.DataColumnMapping("MLinkID", "MLinkID"), New System.Data.Common.DataColumnMapping("modelID", "modelID"), New System.Data.Common.DataColumnMapping("QqRatio", "QqRatio"), New System.Data.Common.DataColumnMapping("scenarioID", "scenarioID"), New System.Data.Common.DataColumnMapping("timeOfMaxQ", "timeOfMaxQ"), New System.Data.Common.DataColumnMapping("timeOfMaxV", "timeOfMaxV")})})
        Me.LinkHydraulicsDataAdapter.UpdateCommand = Me.OleDbUpdateCommand2
        '
        'OleDbDeleteCommand2
        '
        Me.OleDbDeleteCommand2.CommandText = "DELETE FROM LinkHydraulics WHERE (linkHydraulicsID = ?) AND (MLinkID = ?) AND (Qq" & _
        "Ratio = ? OR ? IS NULL AND QqRatio IS NULL) AND (maxDSElev = ? OR ? IS NULL AND " & _
        "maxDSElev IS NULL) AND (maxQ = ? OR ? IS NULL AND maxQ IS NULL) AND (maxUSElev =" & _
        " ? OR ? IS NULL AND maxUSElev IS NULL) AND (maxV = ? OR ? IS NULL AND maxV IS NU" & _
        "LL) AND (modelID = ? OR ? IS NULL AND modelID IS NULL) AND (scenarioID = ?) AND " & _
        "(timeOfMaxQ = ? OR ? IS NULL AND timeOfMaxQ IS NULL) AND (timeOfMaxV = ? OR ? IS" & _
        " NULL AND timeOfMaxV IS NULL)"
        Me.OleDbDeleteCommand2.Connection = Me.ResultsWarehouseConnection
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_linkHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "linkHydraulicsID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_MLinkID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "MLinkID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_QqRatio", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "QqRatio", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_QqRatio1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "QqRatio", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxDSElev", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxDSElev", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxDSElev1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxDSElev", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxQ", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxQ", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxQ1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxQ", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxUSElev", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxUSElev", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxUSElev1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxUSElev", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxV", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxV", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxV1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxV", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "scenarioID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxQ", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxQ", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxQ1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxQ", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxV", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxV", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxV1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxV", System.Data.DataRowVersion.Original, Nothing))
        '
        'OleDbInsertCommand2
        '
        Me.OleDbInsertCommand2.CommandText = "INSERT INTO LinkHydraulics(maxDSElev, maxQ, maxUSElev, maxV, MLinkID, modelID, Qq" & _
        "Ratio, scenarioID, timeOfMaxQ, timeOfMaxV) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" & _
        ""
        Me.OleDbInsertCommand2.Connection = Me.ResultsWarehouseConnection
        Me.OleDbInsertCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("maxDSElev", System.Data.OleDb.OleDbType.Double, 0, "maxDSElev"))
        Me.OleDbInsertCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("maxQ", System.Data.OleDb.OleDbType.Double, 0, "maxQ"))
        Me.OleDbInsertCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("maxUSElev", System.Data.OleDb.OleDbType.Double, 0, "maxUSElev"))
        Me.OleDbInsertCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("maxV", System.Data.OleDb.OleDbType.Double, 0, "maxV"))
        Me.OleDbInsertCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("MLinkID", System.Data.OleDb.OleDbType.Integer, 0, "MLinkID"))
        Me.OleDbInsertCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"))
        Me.OleDbInsertCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("QqRatio", System.Data.OleDb.OleDbType.Double, 0, "QqRatio"))
        Me.OleDbInsertCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"))
        Me.OleDbInsertCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("timeOfMaxQ", System.Data.OleDb.OleDbType.DBDate, 0, "timeOfMaxQ"))
        Me.OleDbInsertCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("timeOfMaxV", System.Data.OleDb.OleDbType.DBDate, 0, "timeOfMaxV"))
        '
        'OleDbSelectCommand2
        '
        Me.OleDbSelectCommand2.CommandText = "SELECT linkHydraulicsID, maxDSElev, maxQ, maxUSElev, maxV, MLinkID, modelID, QqRa" & _
        "tio, scenarioID, timeOfMaxQ, timeOfMaxV FROM LinkHydraulics"
        Me.OleDbSelectCommand2.Connection = Me.ResultsWarehouseConnection
        '
        'OleDbUpdateCommand2
        '
        Me.OleDbUpdateCommand2.CommandText = "UPDATE LinkHydraulics SET maxDSElev = ?, maxQ = ?, maxUSElev = ?, maxV = ?, MLink" & _
        "ID = ?, modelID = ?, QqRatio = ?, scenarioID = ?, timeOfMaxQ = ?, timeOfMaxV = ?" & _
        " WHERE (linkHydraulicsID = ?) AND (MLinkID = ?) AND (QqRatio = ? OR ? IS NULL AN" & _
        "D QqRatio IS NULL) AND (maxDSElev = ? OR ? IS NULL AND maxDSElev IS NULL) AND (m" & _
        "axQ = ? OR ? IS NULL AND maxQ IS NULL) AND (maxUSElev = ? OR ? IS NULL AND maxUS" & _
        "Elev IS NULL) AND (maxV = ? OR ? IS NULL AND maxV IS NULL) AND (modelID = ? OR ?" & _
        " IS NULL AND modelID IS NULL) AND (scenarioID = ?) AND (timeOfMaxQ = ? OR ? IS N" & _
        "ULL AND timeOfMaxQ IS NULL) AND (timeOfMaxV = ? OR ? IS NULL AND timeOfMaxV IS N" & _
        "ULL)"
        Me.OleDbUpdateCommand2.Connection = Me.ResultsWarehouseConnection
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("maxDSElev", System.Data.OleDb.OleDbType.Double, 0, "maxDSElev"))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("maxQ", System.Data.OleDb.OleDbType.Double, 0, "maxQ"))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("maxUSElev", System.Data.OleDb.OleDbType.Double, 0, "maxUSElev"))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("maxV", System.Data.OleDb.OleDbType.Double, 0, "maxV"))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("MLinkID", System.Data.OleDb.OleDbType.Integer, 0, "MLinkID"))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("QqRatio", System.Data.OleDb.OleDbType.Double, 0, "QqRatio"))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("timeOfMaxQ", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "timeOfMaxQ"))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("timeOfMaxV", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "timeOfMaxV"))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_linkHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "linkHydraulicsID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_MLinkID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "MLinkID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_QqRatio", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "QqRatio", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_QqRatio1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "QqRatio", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxDSElev", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxDSElev", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxDSElev1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxDSElev", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxQ", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxQ", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxQ1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxQ", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxUSElev", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxUSElev", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxUSElev1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxUSElev", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxV", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxV", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxV1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxV", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "scenarioID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxQ", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxQ", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxQ1", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxQ", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxV", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxV", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand2.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxV1", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxV", System.Data.DataRowVersion.Original, Nothing))
        '
        'NodeHydraulicsDataAdapter
        '
        Me.NodeHydraulicsDataAdapter.DeleteCommand = Me.OleDbDeleteCommand1
        Me.NodeHydraulicsDataAdapter.InsertCommand = Me.OleDbInsertCommand1
        Me.NodeHydraulicsDataAdapter.SelectCommand = Me.OleDbSelectCommand1
        Me.NodeHydraulicsDataAdapter.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "NodeHydraulics", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("floodedTime", "floodedTime"), New System.Data.Common.DataColumnMapping("freeboard", "freeboard"), New System.Data.Common.DataColumnMapping("maxElevation", "maxElevation"), New System.Data.Common.DataColumnMapping("modelID", "modelID"), New System.Data.Common.DataColumnMapping("nodeHydraulicsID", "nodeHydraulicsID"), New System.Data.Common.DataColumnMapping("nodeName", "nodeName"), New System.Data.Common.DataColumnMapping("scenarioID", "scenarioID"), New System.Data.Common.DataColumnMapping("surcharge", "surcharge"), New System.Data.Common.DataColumnMapping("surchargeTime", "surchargeTime"), New System.Data.Common.DataColumnMapping("timeOfMaxElev", "timeOfMaxElev")})})
        Me.NodeHydraulicsDataAdapter.UpdateCommand = Me.OleDbUpdateCommand1
        '
        'OleDbDeleteCommand1
        '
        Me.OleDbDeleteCommand1.CommandText = "DELETE FROM NodeHydraulics WHERE (nodeHydraulicsID = ?) AND (floodedTime = ? OR ?" & _
        " IS NULL AND floodedTime IS NULL) AND (freeboard = ? OR ? IS NULL AND freeboard " & _
        "IS NULL) AND (maxElevation = ? OR ? IS NULL AND maxElevation IS NULL) AND (model" & _
        "ID = ?) AND (nodeName = ?) AND (scenarioID = ?) AND (surcharge = ? OR ? IS NULL " & _
        "AND surcharge IS NULL) AND (surchargeTime = ? OR ? IS NULL AND surchargeTime IS " & _
        "NULL) AND (timeOfMaxElev = ? OR ? IS NULL AND timeOfMaxElev IS NULL)"
        Me.OleDbDeleteCommand1.Connection = Me.ResultsWarehouseConnection
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_nodeHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "nodeHydraulicsID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_floodedTime", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "floodedTime", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_floodedTime1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "floodedTime", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_freeboard", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "freeboard", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_freeboard1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "freeboard", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxElevation", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxElevation", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxElevation1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxElevation", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_nodeName", System.Data.OleDb.OleDbType.VarWChar, 6, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "nodeName", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "scenarioID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surcharge", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surcharge", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surcharge1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surcharge", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surchargeTime", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surchargeTime", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surchargeTime1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surchargeTime", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxElev", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxElev", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbDeleteCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxElev1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxElev", System.Data.DataRowVersion.Original, Nothing))
        '
        'OleDbInsertCommand1
        '
        Me.OleDbInsertCommand1.CommandText = "INSERT INTO NodeHydraulics(floodedTime, freeboard, maxElevation, modelID, nodeNam" & _
        "e, scenarioID, surcharge, surchargeTime, timeOfMaxElev) VALUES (?, ?, ?, ?, ?, ?" & _
        ", ?, ?, ?)"
        Me.OleDbInsertCommand1.Connection = Me.ResultsWarehouseConnection
        Me.OleDbInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("floodedTime", System.Data.OleDb.OleDbType.Double, 0, "floodedTime"))
        Me.OleDbInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("freeboard", System.Data.OleDb.OleDbType.Double, 0, "freeboard"))
        Me.OleDbInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("maxElevation", System.Data.OleDb.OleDbType.Double, 0, "maxElevation"))
        Me.OleDbInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"))
        Me.OleDbInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("nodeName", System.Data.OleDb.OleDbType.VarWChar, 6, "nodeName"))
        Me.OleDbInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"))
        Me.OleDbInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("surcharge", System.Data.OleDb.OleDbType.Double, 0, "surcharge"))
        Me.OleDbInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("surchargeTime", System.Data.OleDb.OleDbType.Double, 0, "surchargeTime"))
        Me.OleDbInsertCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("timeOfMaxElev", System.Data.OleDb.OleDbType.DBDate, 0, "timeOfMaxElev"))
        '
        'OleDbSelectCommand1
        '
        Me.OleDbSelectCommand1.CommandText = "SELECT floodedTime, freeboard, maxElevation, modelID, nodeHydraulicsID, nodeName," & _
        " scenarioID, surcharge, surchargeTime, timeOfMaxElev FROM NodeHydraulics"
        Me.OleDbSelectCommand1.Connection = Me.ResultsWarehouseConnection
        '
        'OleDbUpdateCommand1
        '
        Me.OleDbUpdateCommand1.CommandText = "UPDATE NodeHydraulics SET floodedTime = ?, freeboard = ?, maxElevation = ?, model" & _
        "ID = ?, nodeName = ?, scenarioID = ?, surcharge = ?, surchargeTime = ?, timeOfMa" & _
        "xElev = ? WHERE (nodeHydraulicsID = ?) AND (floodedTime = ? OR ? IS NULL AND flo" & _
        "odedTime IS NULL) AND (freeboard = ? OR ? IS NULL AND freeboard IS NULL) AND (ma" & _
        "xElevation = ? OR ? IS NULL AND maxElevation IS NULL) AND (modelID = ?) AND (nod" & _
        "eName = ?) AND (scenarioID = ?) AND (surcharge = ? OR ? IS NULL AND surcharge IS" & _
        " NULL) AND (surchargeTime = ? OR ? IS NULL AND surchargeTime IS NULL) AND (timeO" & _
        "fMaxElev = ? OR ? IS NULL AND timeOfMaxElev IS NULL)"
        Me.OleDbUpdateCommand1.Connection = Me.ResultsWarehouseConnection
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("floodedTime", System.Data.OleDb.OleDbType.Double, 0, "floodedTime"))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("freeboard", System.Data.OleDb.OleDbType.Double, 0, "freeboard"))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("maxElevation", System.Data.OleDb.OleDbType.Double, 0, "maxElevation"))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("nodeName", System.Data.OleDb.OleDbType.VarWChar, 6, "nodeName"))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("surcharge", System.Data.OleDb.OleDbType.Double, 0, "surcharge"))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("surchargeTime", System.Data.OleDb.OleDbType.Double, 0, "surchargeTime"))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("timeOfMaxElev", System.Data.OleDb.OleDbType.DBTimeStamp, 0, "timeOfMaxElev"))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_nodeHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "nodeHydraulicsID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_floodedTime", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "floodedTime", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_floodedTime1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "floodedTime", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_freeboard", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "freeboard", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_freeboard1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "freeboard", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxElevation", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxElevation", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_maxElevation1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "maxElevation", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "modelID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_nodeName", System.Data.OleDb.OleDbType.VarWChar, 6, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "nodeName", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "scenarioID", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surcharge", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surcharge", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surcharge1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surcharge", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surchargeTime", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surchargeTime", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_surchargeTime1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "surchargeTime", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxElev", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxElev", System.Data.DataRowVersion.Original, Nothing))
        Me.OleDbUpdateCommand1.Parameters.Add(New System.Data.OleDb.OleDbParameter("Original_timeOfMaxElev1", System.Data.OleDb.OleDbType.DBTimeStamp, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "timeOfMaxElev", System.Data.DataRowVersion.Original, Nothing))
        '
        'ResultsWarehouseManagerForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(896, 401)
        Me.Controls.Add(Me.TabControl1)
        Me.Menu = Me.MainMenu
        Me.Name = "ResultsWarehouseManagerForm"
        Me.Text = "Results Warehouse Manager"
        Me.TabControl1.ResumeLayout(False)
        Me.ModelCatalogTab.ResumeLayout(False)
        Me.ModelCatalogPanel.ResumeLayout(False)
        CType(Me.ModelCatalogDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModelCatalogDataset1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModelScenarioDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StormCatalogDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stormView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StormCatalogTab.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub MenuItemFile_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFile_Exit.Click
        Me.Dispose()
    End Sub

    Private Sub ResultsWarehouseManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Set the default value for checkbox columns to FALSE, because DBNull will cause data binding to fail
        If ModelCatalogDataset1.ModelCatalog.isUploadedColumn.DefaultValue Is System.DBNull.Value Then
            ModelCatalogDataset1.ModelCatalog.isUploadedColumn.DefaultValue() = False
        End If
        SetupForms()
        mstNodes = New MasterNodes
        mstLinks = New MasterLinks
        'mstDSC = New MasterDSC

    End Sub

    Private Sub SetupForms()
        'Clear the dataset and load data from database
        ModelCatalogDataset1.Clear()
        StormCatalogDataAdapter.Fill(ModelCatalogDataset1)
        ModelScenarioDataAdapter.Fill(ModelCatalogDataset1)
        ModelCatalogDataAdapter.Fill(ModelCatalogDataset1)
        LinkHydraulicsDataAdapter.Fill(ModelCatalogDataset1)
        NodeHydraulicsDataAdapter.Fill(ModelCatalogDataset1)
        DSCHydraulicsDataAdapter.Fill(ModelCatalogDataset1)
        'Initialize the storm display grid to show the storm associated with the selected model scenario
        Dim idxSelectedRow As Integer
        idxSelectedRow = ModelScenarioDataGrid.CurrentRowIndex
        stormView.RowFilter = "stormID=" & ModelCatalogDataset1.ModelScenario.Rows(idxSelectedRow)("stormID")
        StormCatalogDataGrid.DataSource = stormView

        StatusBar1 = New ProgressStatus
        Dim StatusBarPanel1 As StatusBarPanel
        Dim StatusBarPanel2 As StatusBarPanel
        StatusBarPanel1 = New StatusBarPanel
        StatusBarPanel2 = New StatusBarPanel
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 353)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(888, 22)
        Me.StatusBar1.TabIndex = 7
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        StatusBarPanel1.Alignment = HorizontalAlignment.Left
        StatusBarPanel1.Width = 99
        StatusBarPanel1.Text = "Selected Model: '" & ModelCatalogDataGrid.Item(ModelCatalogDataGrid.CurrentRowIndex, 1) & "'"
        '
        'StatusBarPanel2
        '
        StatusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        StatusBarPanel2.Width = 773

        StatusBar1.Panels.Add(StatusBarPanel1)
        StatusBar1.Panels.Add(StatusBarPanel2)
        StatusBar1.ShowPanels = True
        StatusBar1.Dock = DockStyle.Bottom
        StatusBar1.setProgressBar = 1

        Me.Controls.Add(StatusBar1)
        StatusBar1.Show()

    End Sub

    Private Sub ModelScenarioDataGrid_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ModelScenarioDataGrid.CurrentCellChanged
        'Refresh the storm display grid to show the storm associated with the selected model scenario.
        'This code is required since parent tables can not be selected by binding a component to a dataset. Components
        'can only be bound downward to child tables. In order to simulate upward binding (to show parent data) it is 
        'necessary to create a dataview and refresh the row filter based on the selected child row as necesary.
        Dim idxSelectedRow As Integer
        idxSelectedRow = ModelScenarioDataGrid.CurrentRowIndex
        'The followwing works because the ordinal position in the datagrid corresponds to the ordinal position in the dataset.
        'That is, there is a one-to-one relationship between data in the ModelScenarioDataGrid and data in
        'the ModelCatalogDataset.ModelScenarioTable. The index of the datagrid corresponds to the index of the dataset
        stormView.RowFilter = "stormID=" & ModelCatalogDataset1.ModelScenario.Rows(idxSelectedRow)("stormID")
        StormCatalogDataGrid.DataSource = stormView
    End Sub

    Private Sub ModelCatalogDataGrid_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModelCatalogDataGrid.CurrentCellChanged
        StatusBar1.Panels(0).Text = "Selected Model: '" & ModelCatalogDataGrid.Item(ModelCatalogDataGrid.CurrentRowIndex, 1) & "'"

    End Sub

    Public Function getSelectedModelColumn(ByVal idxDataGridColumn As Integer) As Integer
        Dim idxSelectedModel
        Dim selectedModelIDCell As DataGridCell

        idxSelectedModel = ModelCatalogDataGrid.CurrentRowIndex
        getSelectedModelColumn = ModelCatalogDataGrid.Item(idxSelectedModel, idxDataGridColumn)
    End Function

    Public Function updateSelectedModel(ByVal ds As ModelCatalogDataset)
        ds.AcceptChanges()
    End Function

    Private Sub btnEditModelCatalog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditModelCatalog.Click
        Try
            frmEditModelCatalog1 = New frmEditModelCatalog(ModelCatalogDataset1, getSelectedModelColumn(0))
            frmEditModelCatalog1.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            frmEditModelCatalog1 = Nothing
        End Try

    End Sub

    Private Sub btnGenerateReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateReports.Click
        Debug.WriteLine("Selected Scenario ID: " & ModelScenarioDataGrid.CurrentRowIndex)
        Debug.WriteLine("Selected Model ID: " & ModelCatalogDataGrid.CurrentRowIndex)
    End Sub

    Private Sub btnAddModel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddModel.Click
        Try
            frmEditModelCatalog1 = New frmEditModelCatalog(ModelCatalogDataset1)
            frmEditModelCatalog1.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            frmEditModelCatalog1 = Nothing
        End Try
    End Sub

    Private Sub btnBatchUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatchUpload.Click
        batchProcessModels(ModelCatalogDataset1)
    End Sub

    Sub batchProcessModels(ByVal ds As ModelCatalogDataset)
        Dim dv As DataView
        dv = New DataView(ds.ModelCatalog)
        dv.RowFilter = "not isuploaded"
        Dim modelRow As DataRowView
        Dim mdl As ModelResults
        Dim rows As DataRowCollection = ds.ModelCatalog.Rows
        Dim row As DataRow
        For Each row In rows
            If Not row.Item("isuploaded") Then
                Try
                    mdl = New ModelResults(row)
                    processModel(mdl)
                    'For Each r As DataRow In ModelCatalogDataset1.GetChanges().Tables(4).Rows
                    '    For Each c As DataColumn In r.Table.Columns
                    '        Debug.WriteLine(c.ColumnName & "=" & r(c.ColumnName).ToString)
                    '    Next
                    '    Debug.WriteLine("Row State=" & r.RowState.ToString)
                    '    Debug.WriteLine("**********************************")
                    'Next
                    StatusBar1.progressBar.Maximum = 4
                    StatusBar1.progressBar.Minimum = 0
                    StatusBar1.progressBar.Value = 1
                    StatusBar1.progressBar.Visible = True
                    Application.DoEvents()
                    Dim now = System.DateTime.Now
                    Do
                    Loop Until Me.ResultsWarehouseConnection.State = ConnectionState.Closed
                    Dim later = System.DateTime.Now
                    Debug.WriteLine("Sit and spin for " & now.Subtract(later).TotalSeconds & " seconds.")
                    StatusBar1.Panels(0).Text = mdl.modelName & " - Uploading Node data"
                    NodeHydraulicsDataAdapter.Update(ModelCatalogDataset1.NodeHydraulics.GetChanges())
                    StatusBar1.Panels(0).Text = mdl.modelName & " - Uploading Link data"
                    StatusBar1.progressBar.PerformStep()
                    Application.DoEvents()
                    LinkHydraulicsDataAdapter.Update(ModelCatalogDataset1.LinkHydraulics.GetChanges())
                    StatusBar1.Panels(0).Text = mdl.modelName & " - Uploading DSC data"
                    StatusBar1.progressBar.PerformStep()
                    Application.DoEvents()
                    DSCHydraulicsDataAdapter.Update(ModelCatalogDataset1.DSCHydraulics.GetChanges())
                    row.Item("isuploaded") = True
                    row.Item("uploadDate") = System.DateTime.Now.ToString

                    ModelCatalogDataAdapter.Update(ModelCatalogDataset1.ModelCatalog.GetChanges())
                    Application.DoEvents()
                    ModelCatalogDataset1.AcceptChanges()
                Catch ex As System.Data.DBConcurrencyException
                    MsgBox("WTF YO?")
                Catch ex As Exception
                    Debug.WriteLine("Could not process model '" & mdl.modelName & "'.")
                    Debug.WriteLine(ex.ToString)
                    'Debug.Fail(ex.ToString)
                Finally
                    ModelCatalogDataset1.NodeHydraulics.Clear()
                    ModelCatalogDataset1.LinkHydraulics.Clear()
                    ModelCatalogDataset1.DSCHydraulics.Clear()
                    'ModelCatalogDataset1.ModelCatalog.Clear()
                    'ModelCatalogDataAdapter.Fill(ModelCatalogDataset1.ModelCatalog)
                    NodeHydraulicsDataAdapter.Fill(ModelCatalogDataset1.NodeHydraulics)
                    LinkHydraulicsDataAdapter.Fill(ModelCatalogDataset1.LinkHydraulics)
                    DSCHydraulicsDataAdapter.Fill(ModelCatalogDataset1.DSCHydraulics)
                    mdl = Nothing
                End Try
            End If
        Next
    End Sub

    Private Sub processModel(ByVal mdl As ModelResults)
        Dim tableE09 As DataTable
        Dim tableE10 As DataTable
        Dim tableE20 As DataTable
        Dim DSCResults As DataTable

        StatusBar1.progressBar.Visible = True
        StatusBar1.progressBar.Minimum = 1
        StatusBar1.progressBar.Step = 1

        tableE09 = mdl.ResultsDataset.Tables("tableE09")
        tableE10 = mdl.ResultsDataset.Tables("tableE10")
        tableE20 = mdl.ResultsDataset.Tables("tableE20")
        DSCResults = mdl.ResultsDataset.Tables("DSCResults")

        Dim tableE09Rows As DataRowCollection = tableE09.Rows
        Dim tableE09Row As DataRow

        Dim tableE10Rows As DataRowCollection = tableE10.Rows
        Dim tableE10Row As DataRow

        Dim tableE20Rows As DataRowCollection = tableE20.Rows
        Dim tableE20Row As DataRow

        Dim DSCResultsRows As DataRowCollection = DSCResults.Rows
        Dim DSCResultsRow As DataRow

        Dim linkRow As ModelCatalogDataset.LinkHydraulicsRow
        Dim nodeRow As ModelCatalogDataset.NodeHydraulicsRow
        Dim DSCRow As ModelCatalogDataset.DSCHydraulicsRow

        linkRow = ModelCatalogDataset1.LinkHydraulics.NewLinkHydraulicsRow()
        nodeRow = ModelCatalogDataset1.NodeHydraulics.NewNodeHydraulicsRow()
        DSCRow = ModelCatalogDataset1.DSCHydraulics.NewDSCHydraulicsRow()

        Dim nodeNameArray As String()

        Me.dvNodeHydraulics = New DataView(ModelCatalogDataset1.NodeHydraulics)
        dvNodeHydraulics.RowFilter = "scenarioID=" & mdl.scenarioID
        dvNodeHydraulics.Sort = "nodeName"
        Me.dvLinkHydraulics = New DataView(ModelCatalogDataset1.LinkHydraulics)
        dvLinkHydraulics.RowFilter = "scenarioID=" & mdl.scenarioID
        dvLinkHydraulics.Sort = "MLinkID"
        Me.dvDSCHydraulics = New DataView(ModelCatalogDataset1.DSCHydraulics)
        dvDSCHydraulics.RowFilter = "scenarioID=" & mdl.scenarioID
        dvDSCHydraulics.Sort = "DSCID"

        Dim i As Integer = 0
        ' Set Maximum to the total number of files to copy.
        StatusBar1.progressBar.Maximum = tableE09.Rows.Count
        ' Set the initial value of the ProgressBar.
        StatusBar1.progressBar.Value = 1
        StatusBar1.Panels(0).Text = "Processing " & mdl.modelName & " - Nodes"
        For Each tableE09Row In tableE09Rows
            Dim nodeName As String = tableE09Row("NodeName")
            If mstNodes.isMstNode(nodeName) Then
                tableE20Row = tableE20Rows(i)
                If NodeInWarehouse(nodeName) Then                    
                    'existingRow = ModelCatalogDataset1.NodeHydraulics.Select("scenarioID=" & mdl.scenarioID & " AND nodeName='" & nodeRow.nodeName & "'")(0)
                    nodeRow = dvNodeHydraulics(dvNodeHydraulics.Find(nodeName)).Row
                    With nodeRow                        
                        .modelID = mdl.modelID
                        If .nodeName <> tableE09Row("NodeName") Then
                            Throw New Exception("Attempted to changes nodeName during update")
                        End If
                        .maxElevation = tableE09Row("MaxJElev")
                        .timeOfMaxElev = tableE09Row("TimeOfMax")
                        .surcharge = tableE09Row("Surcharge")
                        .freeboard = tableE09Row("Freeboard")
                        .surchargeTime = tableE20Row("SurchargeTime")
                        .floodedTime = tableE20Row("FloodedTime")
                    End With
                Else
                    nodeRow = ModelCatalogDataset1.NodeHydraulics.NewNodeHydraulicsRow()
                    With nodeRow
                        .scenarioID = mdl.scenarioID
                        .modelID = mdl.modelID
                        .nodeName = tableE09Row("NodeName")
                        .maxElevation = tableE09Row("MaxJElev")
                        .timeOfMaxElev = tableE09Row("TimeOfMax")
                        .surcharge = tableE09Row("Surcharge")
                        .freeboard = tableE09Row("Freeboard")
                        .surchargeTime = tableE20Row("SurchargeTime")
                        .floodedTime = tableE20Row("FloodedTime")
                    End With
                    ModelCatalogDataset1.NodeHydraulics.AddNodeHydraulicsRow(nodeRow)
                End If
            End If
            i = i + 1
            StatusBar1.progressBar.PerformStep()
            Application.DoEvents()
        Next

        StatusBar1.progressBar.Maximum = tableE10.Rows.Count
        ' Set the initial value of the ProgressBar.
        StatusBar1.progressBar.Value = 1
        StatusBar1.Panels(0).Text = "Processing " & mdl.modelName & " - Links"
        For Each tableE10Row In tableE10Rows
            Dim MLinkID As Integer
            Dim CondName As String
            CondName = tableE10Row("CondName")
            If mstLinks.isMstLink(CondName) Then
                MLinkID = CInt(CondName.Trim("M"))
                If LinkInWarehouse(MLinkID) Then                    
                    linkRow = dvLinkHydraulics(dvLinkHydraulics.Find(MLinkID)).Row
                    With linkRow                        
                        .modelID = mdl.modelID
                        If .MLinkID <> MLinkID Then
                            Throw New Exception("Attempted to changes MLinkID during update")
                        End If
                        .maxQ = tableE10Row("MaxQ")
                        .timeOfMaxQ = tableE10Row("TimeMaxQ")
                        .maxV = tableE10Row("MaxV")
                        .timeOfMaxV = tableE10Row("TimeMaxV")
                        .QqRatio = tableE10Row("QqRatio")
                        .maxUSElev = tableE10Row("MaxUsElev")
                        .maxDSElev = tableE10Row("MaxDsElev")
                    End With
                Else
                    linkRow = ModelCatalogDataset1.LinkHydraulics.NewLinkHydraulicsRow()
                    With linkRow
                        .scenarioID = mdl.scenarioID
                        .modelID = mdl.modelID
                        .MLinkID = MLinkID
                        .maxQ = tableE10Row("MaxQ")
                        .timeOfMaxQ = tableE10Row("TimeMaxQ")
                        .maxV = tableE10Row("MaxV")
                        .timeOfMaxV = tableE10Row("TimeMaxV")
                        .QqRatio = tableE10Row("QqRatio")
                        .maxUSElev = tableE10Row("MaxUsElev")
                        .maxDSElev = tableE10Row("MaxDsElev")
                    End With
                    ModelCatalogDataset1.LinkHydraulics.AddLinkHydraulicsRow(linkRow)
                End If
            End If
            StatusBar1.progressBar.PerformStep()
            Application.DoEvents()
        Next

        StatusBar1.progressBar.Maximum = DSCResults.Rows.Count
        StatusBar1.progressBar.Value = 1
        StatusBar1.Panels(0).Text = "Processing " & mdl.modelName & " - DSCs"
        Me.Invalidate()
        For Each DSCResultsRow In DSCResultsRows
            Dim DSCID As Integer
            DSCID = DSCResultsRow("ParcelID") * 100 + DSCResultsRow("DivideID")
            If True Then 'mstDSC.isMstDSC(DSCID) Then                
                If DSCInWarehouse(DSCID) Then                    
                    DSCRow = dvDSCHydraulics(dvDSCHydraulics.Find(DSCID)).Row
                    With DSCRow                        
                        If .DSCID <> DSCID Then
                            Throw New Exception("Attempted to changes MLinkID during update")
                        End If
                        .modelID = mdl.modelID
                        .HGL = DSCResultsRow("HGL")
                        .deltaHGL = DSCResultsRow("dHGL")
                        .surcharge = DSCResultsRow("surcharge")
                    End With
                Else
                    DSCRow = ModelCatalogDataset1.DSCHydraulics.NewDSCHydraulicsRow
                    With DSCRow
                        .scenarioID = mdl.scenarioID
                        .DSCID = DSCID
                        .modelID = mdl.modelID
                        .HGL = DSCResultsRow("HGL")
                        .deltaHGL = DSCResultsRow("dHGL")
                        .surcharge = DSCResultsRow("surcharge")
                    End With
                    ModelCatalogDataset1.DSCHydraulics.AddDSCHydraulicsRow(DSCRow)
                End If
            End If
            StatusBar1.progressBar.PerformStep()
            Application.DoEvents()
        Next
        'Dim linksResults as ModelCatalogDataset.        
        StatusBar1.progressBar.Visible = False

    End Sub

    Private Function NodeInWarehouse(ByVal nodeName As String) As Boolean

        Dim matchingRowCount = dvNodeHydraulics.FindRows(nodeName).Length

        If matchingRowCount = 0 Then
            NodeInWarehouse = False
        ElseIf matchingRowCount = 1 Then
            NodeInWarehouse = True
        Else
            Throw New Exception("Multiple results for [nodeName]=" & nodeName & ", " & dvNodeHydraulics.RowFilter)
        End If
    End Function

    Private Function LinkInWarehouse(ByVal MLinkID As Integer) As Boolean
        'Dim matchingRowCount = ModelCatalogDataset1.LinkHydraulics.Select("scenarioID=" & linkRow("scenarioID") & " AND MLinkID=" & linkRow("MLinkID")).Length()
        Dim matchingRowCount = dvLinkHydraulics.FindRows(MLinkID).Length

        If matchingRowCount = 0 Then
            LinkInWarehouse = False
        ElseIf matchingRowCount = 1 Then
            LinkInWarehouse = True
        Else
            Throw New Exception("Multiple results for [MLinkID]=" & MLinkID & ", " & dvLinkHydraulics.RowFilter)
        End If
    End Function

    Private Function DSCInWarehouse(ByVal DSCID As Integer) As Boolean
        'Dim matchingRowCount = ModelCatalogDataset1.DSCHydraulics.Select("scenarioID=" & DSCRow("scenarioID") & " AND DSCID=" & DSCRow("DSCID")).Length()
        Dim matchingRowCount = dvDSCHydraulics.FindRows(DSCID).Length

        If matchingRowCount = 0 Then
            DSCInWarehouse = False
        ElseIf matchingRowCount = 1 Then
            DSCInWarehouse = True
        Else
            Throw New Exception("Multiple results for [DSCID]=" & DSCID & ", " & dvDSCHydraulics.RowFilter)
        End If
    End Function

End Class

Public Class MasterNodes
    Private ds As DataSet
    Private da As OleDb.OleDbDataAdapter
    Private dt As DataTable
    Private dv As DataView
    Private SelectCommand As OleDb.OleDbCommand
    Private DataConnection As OleDb.OleDbConnection

    Public Sub New()
        Dim ConnectionString As String
        ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;" & _
                   "Jet OLEDB:Database Password=;Data Source='\\cassio\modeling\AGMaster21\Nodes\mst_nodes_ac.mdb'" & _
                   ";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;" & _
                   "Provider='Microsoft.Jet.OLEDB.4.0';Jet OLEDB:System database=;" & _
                   "Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;" & _
                   "Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;" & _
                   "Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False"

        DataConnection = New OleDb.OleDbConnection(ConnectionString)
        da = New OleDb.OleDbDataAdapter
        SelectCommand = New OleDb.OleDbCommand("Select * From mst_nodes_ac")
        SelectCommand.Connection = DataConnection
        da.SelectCommand = SelectCommand
        ds = New DataSet("mst_nodes")
        dt = New DataTable("mst_nodes")
        ds.Tables.Add(dt)
        da.Fill(ds, dt.TableName)
        dv = New DataView(dt, Nothing, "Node", DataViewRowState.CurrentRows)
    End Sub

    Public Function isMstNode(ByVal NodeName As String) As Boolean
        Dim matchedNodeRows As Integer


        matchedNodeRows = dv.Find(NodeName)
        'matchedNodeRows = dt.Select("Node='" & NodeName & "'")
        If matchedNodeRows = -1 Then
            isMstNode = False
        Else
            isMstNode = True
        End If
    End Function
End Class

Public Class MasterLinks
    Private ds As DataSet
    Private da As OleDb.OleDbDataAdapter
    Private dt As DataTable
    Private dv As DataView
    Private SelectCommand As OleDb.OleDbCommand
    Private DataConnection As OleDb.OleDbConnection

    Public Sub New()
        Dim ConnectionString As String
        ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;" & _
                   "Jet OLEDB:Database Password=;Data Source='\\cassio\modeling\AGMaster21\Links\mst_links_ac.mdb'" & _
                   ";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;" & _
                   "Provider='Microsoft.Jet.OLEDB.4.0';Jet OLEDB:System database=;" & _
                   "Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;" & _
                   "Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;" & _
                   "Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False"

        DataConnection = New OleDb.OleDbConnection(ConnectionString)
        da = New OleDb.OleDbDataAdapter
        SelectCommand = New OleDb.OleDbCommand("Select * From mst_links_ac")
        SelectCommand.Connection = DataConnection
        da.SelectCommand = SelectCommand
        ds = New DataSet("mst_links")
        dt = New DataTable("mst_links")
        ds.Tables.Add(dt)
        da.Fill(ds, dt.TableName)
        dv = New DataView(dt, Nothing, "MLinkID", DataViewRowState.CurrentRows)
    End Sub

    Public Function isMstLink(ByVal CondName As String) As Boolean
        Dim MLinkID As Integer
        If CondName.StartsWith("M") Then
            MLinkID = CInt(CondName.Trim("M"))
        Else
            isMstLink = False
            Exit Function
        End If

        Dim matchedLinkRows As Integer

        matchedLinkRows = dv.Find(MLinkID)
        'matchedNodeRows = dt.Select("Node='" & NodeName & "'")
        If matchedLinkRows = -1 Then
            isMstLink = False
        Else
            isMstLink = True
        End If
    End Function
End Class

Public Class MasterDSC
    Private ds As DataSet
    Private da As OleDb.OleDbDataAdapter
    Private dt As DataTable
    Private dv As DataView
    Private SelectCommand As OleDb.OleDbCommand
    Private DataConnection As OleDb.OleDbConnection

    Public Sub New()
        Dim ConnectionString As String
        ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;" & _
                   "Jet OLEDB:Database Password=;Data Source='\\cassio\modeling\AGMaster21\Parcels_Divides\mst_DSC_ac.mdb'" & _
                   ";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;" & _
                   "Provider='Microsoft.Jet.OLEDB.4.0';Jet OLEDB:System database=;" & _
                   "Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;" & _
                   "Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;" & _
                   "Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False"

        DataConnection = New OleDb.OleDbConnection(ConnectionString)
        da = New OleDb.OleDbDataAdapter
        SelectCommand = New OleDb.OleDbCommand("Select * From mstDSC_ac")
        SelectCommand.Connection = DataConnection
        da.SelectCommand = SelectCommand
        ds = New DataSet("mstDSC")
        dt = New DataTable("mstDSC")
        ds.Tables.Add(dt)
        da.Fill(ds, dt.TableName)
        dv = New DataView(dt, Nothing, "DSCID", DataViewRowState.CurrentRows)
    End Sub

    Public Function isMstDSC(ByVal DSCID As Integer) As Boolean
        Dim matchedLinkRows As Integer

        matchedLinkRows = dv.Find(DSCID)
        If matchedLinkRows = -1 Then
            isMstDSC = False
        Else
            isMstDSC = True
        End If
    End Function
End Class

Public Class ProgressStatus : Inherits StatusBar
    Public progressBar As New ProgressBar
    Private _progressBar As Integer = -1


    Sub New()
        progressBar.Hide()

        Me.Controls.Add(progressBar)
    End Sub


    Public Property setProgressBar() As Integer
        Get
            Return _progressBar
        End Get
        Set(ByVal Value As Integer)
            _progressBar = Value
            Me.Panels(_progressBar).Style = StatusBarPanelStyle.OwnerDraw
        End Set
    End Property


    Private Sub Reposition(ByVal sender As Object, _
         ByVal sbdevent As System.Windows.Forms.StatusBarDrawItemEventArgs) _
         Handles MyBase.DrawItem
        progressBar.Location = New Point(sbdevent.Bounds.X, _
           sbdevent.Bounds.Y)
        progressBar.Size = New Size(sbdevent.Bounds.Width, _
           sbdevent.Bounds.Height)
        progressBar.Show()
    End Sub
End Class
