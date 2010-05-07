Imports ResultsWarehouseManager

Public Class frmEditModelCatalog
    Inherits System.Windows.Forms.Form

    Private catalogDataView As DataView
    Private catalogRowView As DataRowView
    Private catalogDataSet As ModelCatalogDataset

    Public Sub New(ByVal ds As ModelCatalogDataset, ByVal ModelID As Integer)
        MyBase.New()
        InitializeComponent()
        catalogDataSet = ds

        catalogDataView = New DataView(ds.ModelCatalog)
        catalogDataView.RowFilter = "modelID=" & ModelID
        catalogRowView = catalogDataView.Item(0)
        BindControls()
        catalogRowView.Row.BeginEdit()
        Me.btnAdd.Visible = False
        Me.btnAdd.Enabled = False
        Me.btnDelete.Visible = True
        Me.btnDelete.Enabled = True
        Me.btnReload.Visible = True
        Me.btnReload.Enabled = True
        Me.btnUpdate.Visible = True
    End Sub

    Public Sub New(ByVal ds As ModelCatalogDataset)
        MyBase.New()
        InitializeComponent()
        catalogDataSet = ds
        catalogDataView = New DataView(ds.ModelCatalog)
        catalogRowView = catalogDataView.AddNew
        catalogRowView.Item("modelID") = findMaxColumnValue(ds.ModelCatalog, ds.ModelCatalog.modelIDColumn) + 1
        catalogDataView.RowFilter = "modelID=" & catalogRowView.Item(0)
        
        BindControls()
        catalogRowView.Row.BeginEdit()
        Me.btnAdd.Visible = True
        Me.btnAdd.Enabled = True
        Me.btnDelete.Visible = False
        Me.btnDelete.Enabled = False
        Me.btnReload.Visible = False
        Me.btnReload.Enabled = False
        Me.btnUpdate.Visible = False
    End Sub

#Region " Windows Form Designer generated code "

    Private Sub New()
        MyBase.New()

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
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents lblisUploaded As System.Windows.Forms.Label
    Friend WithEvents lblmodelDescription As System.Windows.Forms.Label
    Friend WithEvents lblmodelID As System.Windows.Forms.Label
    Friend WithEvents lblmodelName As System.Windows.Forms.Label
    Friend WithEvents lblmodelOutputFile As System.Windows.Forms.Label
    Friend WithEvents lblmodelPath As System.Windows.Forms.Label
    Friend WithEvents editisUploaded As System.Windows.Forms.CheckBox
    Friend WithEvents editmodelDescription As System.Windows.Forms.TextBox
    Friend WithEvents editmodelID As System.Windows.Forms.TextBox
    Friend WithEvents editmodelName As System.Windows.Forms.TextBox
    Friend WithEvents editmodelOutputFile As System.Windows.Forms.TextBox
    Friend WithEvents editmodelPath As System.Windows.Forms.TextBox
    Friend WithEvents lblmodelType As System.Windows.Forms.Label
    Friend WithEvents lblscenarioID As System.Windows.Forms.Label
    Friend WithEvents lbluploadDate As System.Windows.Forms.Label
    Friend WithEvents lbluserName As System.Windows.Forms.Label
    Friend WithEvents editmodelType As System.Windows.Forms.TextBox
    Friend WithEvents editscenarioID As System.Windows.Forms.TextBox
    Friend WithEvents edituploadDate As System.Windows.Forms.TextBox
    Friend WithEvents edituserName As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnReload As System.Windows.Forms.Button
    Friend WithEvents lblScenario As System.Windows.Forms.Label
    Friend WithEvents cmbScenario As System.Windows.Forms.ComboBox

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.lblisUploaded = New System.Windows.Forms.Label
        Me.lblmodelDescription = New System.Windows.Forms.Label
        Me.lblmodelID = New System.Windows.Forms.Label
        Me.lblmodelName = New System.Windows.Forms.Label
        Me.lblmodelOutputFile = New System.Windows.Forms.Label
        Me.lblmodelPath = New System.Windows.Forms.Label
        Me.editisUploaded = New System.Windows.Forms.CheckBox
        Me.editmodelDescription = New System.Windows.Forms.TextBox
        Me.editmodelID = New System.Windows.Forms.TextBox
        Me.editmodelName = New System.Windows.Forms.TextBox
        Me.editmodelOutputFile = New System.Windows.Forms.TextBox
        Me.editmodelPath = New System.Windows.Forms.TextBox
        Me.lblmodelType = New System.Windows.Forms.Label
        Me.lblscenarioID = New System.Windows.Forms.Label
        Me.lbluploadDate = New System.Windows.Forms.Label
        Me.lbluserName = New System.Windows.Forms.Label
        Me.editmodelType = New System.Windows.Forms.TextBox
        Me.editscenarioID = New System.Windows.Forms.TextBox
        Me.edituploadDate = New System.Windows.Forms.TextBox
        Me.edituserName = New System.Windows.Forms.TextBox
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnReload = New System.Windows.Forms.Button
        Me.lblScenario = New System.Windows.Forms.Label
        Me.cmbScenario = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(8, 360)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(96, 23)
        Me.btnUpdate.TabIndex = 9
        Me.btnUpdate.Text = "&Save Changes"
        '
        'lblisUploaded
        '
        Me.lblisUploaded.Location = New System.Drawing.Point(416, 56)
        Me.lblisUploaded.Name = "lblisUploaded"
        Me.lblisUploaded.TabIndex = 2
        Me.lblisUploaded.Text = "Uploaded?"
        '
        'lblmodelDescription
        '
        Me.lblmodelDescription.Location = New System.Drawing.Point(8, 96)
        Me.lblmodelDescription.Name = "lblmodelDescription"
        Me.lblmodelDescription.Size = New System.Drawing.Size(64, 23)
        Me.lblmodelDescription.TabIndex = 3
        Me.lblmodelDescription.Text = "Description"
        '
        'lblmodelID
        '
        Me.lblmodelID.Location = New System.Drawing.Point(416, 8)
        Me.lblmodelID.Name = "lblmodelID"
        Me.lblmodelID.Size = New System.Drawing.Size(56, 23)
        Me.lblmodelID.TabIndex = 4
        Me.lblmodelID.Text = "Model ID"
        '
        'lblmodelName
        '
        Me.lblmodelName.Location = New System.Drawing.Point(8, 8)
        Me.lblmodelName.Name = "lblmodelName"
        Me.lblmodelName.Size = New System.Drawing.Size(72, 23)
        Me.lblmodelName.TabIndex = 5
        Me.lblmodelName.Text = "Model Name"
        '
        'lblmodelOutputFile
        '
        Me.lblmodelOutputFile.Location = New System.Drawing.Point(16, 280)
        Me.lblmodelOutputFile.Name = "lblmodelOutputFile"
        Me.lblmodelOutputFile.Size = New System.Drawing.Size(64, 23)
        Me.lblmodelOutputFile.TabIndex = 6
        Me.lblmodelOutputFile.Text = "Output File"
        '
        'lblmodelPath
        '
        Me.lblmodelPath.Location = New System.Drawing.Point(16, 248)
        Me.lblmodelPath.Name = "lblmodelPath"
        Me.lblmodelPath.Size = New System.Drawing.Size(64, 23)
        Me.lblmodelPath.TabIndex = 7
        Me.lblmodelPath.Text = "Model Path"
        '
        'editisUploaded
        '
        Me.editisUploaded.Location = New System.Drawing.Point(528, 56)
        Me.editisUploaded.Name = "editisUploaded"
        Me.editisUploaded.Size = New System.Drawing.Size(16, 24)
        Me.editisUploaded.TabIndex = 8
        Me.editisUploaded.TabStop = False
        '
        'editmodelDescription
        '
        Me.editmodelDescription.Location = New System.Drawing.Point(80, 96)
        Me.editmodelDescription.Name = "editmodelDescription"
        Me.editmodelDescription.Size = New System.Drawing.Size(312, 20)
        Me.editmodelDescription.TabIndex = 3
        Me.editmodelDescription.Text = ""
        '
        'editmodelID
        '
        Me.editmodelID.Location = New System.Drawing.Point(488, 8)
        Me.editmodelID.Name = "editmodelID"
        Me.editmodelID.Size = New System.Drawing.Size(56, 20)
        Me.editmodelID.TabIndex = 10
        Me.editmodelID.TabStop = False
        Me.editmodelID.Text = ""
        '
        'editmodelName
        '
        Me.editmodelName.Location = New System.Drawing.Point(96, 8)
        Me.editmodelName.Name = "editmodelName"
        Me.editmodelName.Size = New System.Drawing.Size(288, 20)
        Me.editmodelName.TabIndex = 1
        Me.editmodelName.Text = ""
        '
        'editmodelOutputFile
        '
        Me.editmodelOutputFile.Location = New System.Drawing.Point(80, 280)
        Me.editmodelOutputFile.Name = "editmodelOutputFile"
        Me.editmodelOutputFile.Size = New System.Drawing.Size(456, 20)
        Me.editmodelOutputFile.TabIndex = 8
        Me.editmodelOutputFile.Text = ""
        '
        'editmodelPath
        '
        Me.editmodelPath.Location = New System.Drawing.Point(80, 248)
        Me.editmodelPath.Name = "editmodelPath"
        Me.editmodelPath.Size = New System.Drawing.Size(456, 20)
        Me.editmodelPath.TabIndex = 7
        Me.editmodelPath.Text = ""
        '
        'lblmodelType
        '
        Me.lblmodelType.Location = New System.Drawing.Point(8, 128)
        Me.lblmodelType.Name = "lblmodelType"
        Me.lblmodelType.Size = New System.Drawing.Size(64, 23)
        Me.lblmodelType.TabIndex = 14
        Me.lblmodelType.Text = "Model Type"
        '
        'lblscenarioID
        '
        Me.lblscenarioID.Location = New System.Drawing.Point(416, 32)
        Me.lblscenarioID.Name = "lblscenarioID"
        Me.lblscenarioID.Size = New System.Drawing.Size(64, 23)
        Me.lblscenarioID.TabIndex = 15
        Me.lblscenarioID.Text = "Scenario ID"
        '
        'lbluploadDate
        '
        Me.lbluploadDate.Location = New System.Drawing.Point(8, 192)
        Me.lbluploadDate.Name = "lbluploadDate"
        Me.lbluploadDate.Size = New System.Drawing.Size(72, 23)
        Me.lbluploadDate.TabIndex = 17
        Me.lbluploadDate.Text = "Upload Date"
        '
        'lbluserName
        '
        Me.lbluserName.Location = New System.Drawing.Point(8, 160)
        Me.lbluserName.Name = "lbluserName"
        Me.lbluserName.Size = New System.Drawing.Size(48, 23)
        Me.lbluserName.TabIndex = 18
        Me.lbluserName.Text = "Modeler"
        '
        'editmodelType
        '
        Me.editmodelType.Location = New System.Drawing.Point(80, 128)
        Me.editmodelType.Name = "editmodelType"
        Me.editmodelType.TabIndex = 4
        Me.editmodelType.Text = ""
        '
        'editscenarioID
        '
        Me.editscenarioID.Location = New System.Drawing.Point(488, 32)
        Me.editscenarioID.Name = "editscenarioID"
        Me.editscenarioID.Size = New System.Drawing.Size(56, 20)
        Me.editscenarioID.TabIndex = 20
        Me.editscenarioID.TabStop = False
        Me.editscenarioID.Text = ""
        '
        'edituploadDate
        '
        Me.edituploadDate.Location = New System.Drawing.Point(80, 192)
        Me.edituploadDate.Name = "edituploadDate"
        Me.edituploadDate.TabIndex = 6
        Me.edituploadDate.Text = ""
        '
        'edituserName
        '
        Me.edituserName.Location = New System.Drawing.Point(80, 160)
        Me.edituserName.Name = "edituserName"
        Me.edituserName.TabIndex = 5
        Me.edituserName.Text = ""
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(8, 328)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(96, 23)
        Me.btnAdd.TabIndex = 9
        Me.btnAdd.Text = "&Add"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(232, 360)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(96, 23)
        Me.btnDelete.TabIndex = 11
        Me.btnDelete.Text = "&Delete Model"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(344, 360)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 23)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "&Exit"
        '
        'btnReload
        '
        Me.btnReload.Location = New System.Drawing.Point(120, 360)
        Me.btnReload.Name = "btnReload"
        Me.btnReload.Size = New System.Drawing.Size(94, 23)
        Me.btnReload.TabIndex = 10
        Me.btnReload.Text = "&Undo Changes"
        '
        'lblScenario
        '
        Me.lblScenario.Location = New System.Drawing.Point(8, 32)
        Me.lblScenario.Name = "lblScenario"
        Me.lblScenario.Size = New System.Drawing.Size(88, 23)
        Me.lblScenario.TabIndex = 27
        Me.lblScenario.Text = "Model Scenario"
        '
        'cmbScenario
        '
        Me.cmbScenario.Location = New System.Drawing.Point(96, 32)
        Me.cmbScenario.Name = "cmbScenario"
        Me.cmbScenario.Size = New System.Drawing.Size(288, 21)
        Me.cmbScenario.TabIndex = 2
        '
        'frmEditModelCatalog
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(552, 397)
        Me.Controls.Add(Me.cmbScenario)
        Me.Controls.Add(Me.lblScenario)
        Me.Controls.Add(Me.btnReload)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.lblisUploaded)
        Me.Controls.Add(Me.lblmodelDescription)
        Me.Controls.Add(Me.lblmodelID)
        Me.Controls.Add(Me.lblmodelName)
        Me.Controls.Add(Me.lblmodelOutputFile)
        Me.Controls.Add(Me.lblmodelPath)
        Me.Controls.Add(Me.editisUploaded)
        Me.Controls.Add(Me.editmodelDescription)
        Me.Controls.Add(Me.editmodelID)
        Me.Controls.Add(Me.editmodelName)
        Me.Controls.Add(Me.editmodelOutputFile)
        Me.Controls.Add(Me.editmodelPath)
        Me.Controls.Add(Me.lblmodelType)
        Me.Controls.Add(Me.lblscenarioID)
        Me.Controls.Add(Me.lbluploadDate)
        Me.Controls.Add(Me.lbluserName)
        Me.Controls.Add(Me.editmodelType)
        Me.Controls.Add(Me.editscenarioID)
        Me.Controls.Add(Me.edituploadDate)
        Me.Controls.Add(Me.edituserName)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCancel)
        Me.Name = "frmEditModelCatalog"
        Me.Text = "Edit Model Catalog Entry"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        catalogRowView.Row.CancelEdit()
        Me.Close()
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim reallyDelete As Boolean
        If MsgBox("Are you sure you want to delete this model catalog entry?", MsgBoxStyle.OKCancel) = MsgBoxResult.OK Then reallyDelete = True
        If reallyDelete Then
            Try
                catalogRowView.Delete()
            Catch ex As Exception
                Throw New Exception("Could not delete model catalog entry.", ex)
            End Try
        End If

    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            catalogRowView.EndEdit()
            'Clear out the current edits
            'Me.BindingContext(objModelCatalogDataset, "ModelCatalog").EndCurrentEdit()
            'Me.BindingContext(objModelCatalogDataset, "ModelCatalog").AddNew()
        Catch eEndEdit As System.Exception
            System.Windows.Forms.MessageBox.Show(eEndEdit.Message)
        End Try

    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            'Attempt to update the datasource.
            Me.UpdateCatalogEntry()
        Catch eUpdate As System.Exception
            'Add your error handling code here.
            'Display error message, if any.
            System.Windows.Forms.MessageBox.Show(eUpdate.Message)
        End Try

    End Sub
    Private Sub btnReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReload.Click
        Try
            'Attempt to load the dataset.
            If catalogRowView.Row.HasVersion(DataRowVersion.Proposed) Then
                catalogRowView.Row.RejectChanges()
            End If
        Catch eLoad As System.Exception
            'Add your error handling code here.
            'Display error message, if any.
            System.Windows.Forms.MessageBox.Show(eLoad.Message)
        End Try

    End Sub
    Public Sub UpdateCatalogEntry()
        
        'Create a new dataset to hold the changes that have been made to the main dataset.
        Dim reallyUpdate As Boolean
        If MsgBox("Are you sure you want to overwrite this model catalog entry?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then reallyUpdate = True
        If reallyUpdate Then
            If catalogRowView.Row.HasVersion(DataRowVersion.Proposed) Then
                Try
                    catalogRowView.Row.AcceptChanges()
                Catch ex As Exception
                    Throw New Exception("Could not validate model catalog changes.", ex)
                End Try
            Else
                MsgBox("No changes were specified.")
            End If
        End If
    End Sub

    Private Sub BindControls()
        editmodelDescription.DataBindings.Add("Text", Me.catalogDataView, "modelDescription")
        editisUploaded.DataBindings.Add("Checked", Me.catalogDataView, "isUploaded")
        editmodelID.DataBindings.Add("Text", Me.catalogDataView, "modelID")
        editmodelName.DataBindings.Add("Text", Me.catalogDataView, "modelName")
        editmodelOutputFile.DataBindings.Add("Text", Me.catalogDataView, "modelOutputFile")
        editmodelPath.DataBindings.Add("Text", Me.catalogDataView, "modelPath")
        editmodelType.DataBindings.Add("Text", Me.catalogDataView, "modelType")
        editscenarioID.DataBindings.Add("Text", Me.catalogDataView, "scenarioID")
        edituploadDate.DataBindings.Add("Text", Me.catalogDataView, "uploadDate")
        edituserName.DataBindings.Add("Text", Me.catalogDataView, "userName")
        'catalogdataview.
        cmbScenario.DataSource = catalogDataSet
        cmbScenario.DisplayMember = "ModelScenario.description"
        cmbScenario.ValueMember = "ModelScenario.scenarioID"
        cmbScenario.DataBindings.Add("SelectedValue", Me.catalogDataView, "scenarioID")

        'MsgBox("yar matey!")

    End Sub

End Class
