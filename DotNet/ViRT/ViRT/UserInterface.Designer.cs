namespace ViRT
{
    partial class UserInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            this.tabControl_ToolSelect = new System.Windows.Forms.TabControl();
            this.tabPageVirtualRainfallGenerator = new System.Windows.Forms.TabPage();
            this.buttonSelectByQuartersection = new System.Windows.Forms.Button();
            this.buttonSelectByBasin = new System.Windows.Forms.Button();
            this.panelMapControl = new System.Windows.Forms.Panel();
            this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.buttonPan = new System.Windows.Forms.Button();
            this.labelCombineSuccessful = new System.Windows.Forms.Label();
            this.buttonImportMapInfoLinks = new System.Windows.Forms.Button();
            this.buttonChangeLayer = new System.Windows.Forms.Button();
            this.labelSelectionLayer = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.labelEndDate = new System.Windows.Forms.Label();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnFileBrowser = new System.Windows.Forms.Button();
            this.txtOutputFileDestination = new System.Windows.Forms.TextBox();
            this.labelOutputFileLocation = new System.Windows.Forms.Label();
            this.checkBinaryOutput = new System.Windows.Forms.CheckBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelQuarterSectionList = new System.Windows.Forms.Label();
            this.txtQSBulkInput = new System.Windows.Forms.TextBox();
            this.buttonZoomOut = new System.Windows.Forms.Button();
            this.buttonZoomIn = new System.Windows.Forms.Button();
            this.tabPageMonitoringExtractor = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.eNVIRONSENSORBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelOutputLocation = new System.Windows.Forms.Label();
            this.cbReportZeroRain = new System.Windows.Forms.CheckBox();
            this.nudTimeStep = new System.Windows.Forms.NumericUpDown();
            this.labelTimeStep = new System.Windows.Forms.Label();
            this.dtpEndDay = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDay = new System.Windows.Forms.DateTimePicker();
            this.labelRealRainEndTime = new System.Windows.Forms.Label();
            this.labelRealRainStartTime = new System.Windows.Forms.Label();
            this.dataGridViewRainGages = new System.Windows.Forms.DataGridView();
            this.sTATIONBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbConnection2 = new System.Data.OleDb.OleDbConnection();
            this.qsDS1 = new ViRT.qsDS();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.rainDS = new ViRT.RainDSClass();
            this.rAINSENSORBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialogBasinLayer = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogMapInfoFile = new System.Windows.Forms.OpenFileDialog();
            this.labelTimeRangeToGenerate = new System.Windows.Forms.Label();
            this.environsensoridDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationidDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xdatacategorylocqualcombinedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datacategoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationqualifierDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channelnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channeldescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.engineeringunitspanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.engineeringunitzeroDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.engineeringunitsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hasdepthtoflowDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startdateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enddateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createbyDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatebyDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nEPTUNEDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nEPTUNEDataSet = new ViRT.NEPTUNEDataSet();
            this.stationidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.h2numberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationdescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationtypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startdateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enddateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateplanexftDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateplaneyftDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createbyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatebyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sTATIONTableAdapter = new ViRT.NEPTUNEDataSetTableAdapters.STATIONTableAdapter();
            this.rAIN_SENSORTableAdapter = new ViRT.NEPTUNEDataSetTableAdapters.RAIN_SENSORTableAdapter();
            this.eNVIRON_SENSORTableAdapter = new ViRT.NEPTUNEDataSetTableAdapters.ENVIRON_SENSORTableAdapter();
            this.panelMapControlMonitoring = new System.Windows.Forms.Panel();
            this.axMapControlMonitoring = new ESRI.ArcGIS.Controls.AxMapControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tabControl_ToolSelect.SuspendLayout();
            this.tabPageVirtualRainfallGenerator.SuspendLayout();
            this.panelMapControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            this.tabPageMonitoringExtractor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNVIRONSENSORBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRainGages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sTATIONBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qsDS1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rainDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rAINSENSORBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nEPTUNEDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nEPTUNEDataSet)).BeginInit();
            this.panelMapControlMonitoring.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControlMonitoring)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl_ToolSelect
            // 
            this.tabControl_ToolSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_ToolSelect.Controls.Add(this.tabPageVirtualRainfallGenerator);
            this.tabControl_ToolSelect.Controls.Add(this.tabPageMonitoringExtractor);
            this.tabControl_ToolSelect.Location = new System.Drawing.Point(12, 12);
            this.tabControl_ToolSelect.Name = "tabControl_ToolSelect";
            this.tabControl_ToolSelect.SelectedIndex = 0;
            this.tabControl_ToolSelect.Size = new System.Drawing.Size(694, 572);
            this.tabControl_ToolSelect.TabIndex = 0;
            // 
            // tabPageVirtualRainfallGenerator
            // 
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.labelTimeRangeToGenerate);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.buttonSelectByQuartersection);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.buttonSelectByBasin);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.panelMapControl);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.buttonPan);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.labelCombineSuccessful);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.buttonImportMapInfoLinks);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.buttonChangeLayer);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.labelSelectionLayer);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.textBox2);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.labelEndDate);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.labelStartDate);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.btnProcess);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.btnFileBrowser);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.txtOutputFileDestination);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.labelOutputFileLocation);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.checkBinaryOutput);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.dtpEndDate);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.dtpStartDate);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.labelQuarterSectionList);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.txtQSBulkInput);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.buttonZoomOut);
            this.tabPageVirtualRainfallGenerator.Controls.Add(this.buttonZoomIn);
            this.tabPageVirtualRainfallGenerator.Location = new System.Drawing.Point(4, 22);
            this.tabPageVirtualRainfallGenerator.Name = "tabPageVirtualRainfallGenerator";
            this.tabPageVirtualRainfallGenerator.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVirtualRainfallGenerator.Size = new System.Drawing.Size(686, 546);
            this.tabPageVirtualRainfallGenerator.TabIndex = 0;
            this.tabPageVirtualRainfallGenerator.Text = "Virtual Rainfall Generator";
            this.tabPageVirtualRainfallGenerator.UseVisualStyleBackColor = true;
            // 
            // buttonSelectByQuartersection
            // 
            this.buttonSelectByQuartersection.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelectByQuartersection.Image")));
            this.buttonSelectByQuartersection.Location = new System.Drawing.Point(3, 95);
            this.buttonSelectByQuartersection.Name = "buttonSelectByQuartersection";
            this.buttonSelectByQuartersection.Size = new System.Drawing.Size(40, 40);
            this.buttonSelectByQuartersection.TabIndex = 29;
            this.buttonSelectByQuartersection.UseVisualStyleBackColor = true;
            this.buttonSelectByQuartersection.Click += new System.EventHandler(this.buttonSelectByQuartersection_Click);
            // 
            // buttonSelectByBasin
            // 
            this.buttonSelectByBasin.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelectByBasin.Image")));
            this.buttonSelectByBasin.Location = new System.Drawing.Point(3, 49);
            this.buttonSelectByBasin.Name = "buttonSelectByBasin";
            this.buttonSelectByBasin.Size = new System.Drawing.Size(40, 40);
            this.buttonSelectByBasin.TabIndex = 28;
            this.buttonSelectByBasin.UseVisualStyleBackColor = true;
            this.buttonSelectByBasin.Click += new System.EventHandler(this.buttonSelectByBasin_Click);
            // 
            // panelMapControl
            // 
            this.panelMapControl.Controls.Add(this.axMapControl);
            this.panelMapControl.Location = new System.Drawing.Point(49, 3);
            this.panelMapControl.Name = "panelMapControl";
            this.panelMapControl.Size = new System.Drawing.Size(631, 329);
            this.panelMapControl.TabIndex = 27;
            // 
            // axMapControl
            // 
            this.axMapControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.axMapControl.Location = new System.Drawing.Point(0, 0);
            this.axMapControl.Margin = new System.Windows.Forms.Padding(50);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
            this.axMapControl.Size = new System.Drawing.Size(631, 329);
            this.axMapControl.TabIndex = 4;
            this.axMapControl.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl_OnMouseUp);
            // 
            // buttonPan
            // 
            this.buttonPan.Image = ((System.Drawing.Image)(resources.GetObject("buttonPan.Image")));
            this.buttonPan.Location = new System.Drawing.Point(3, 3);
            this.buttonPan.Name = "buttonPan";
            this.buttonPan.Size = new System.Drawing.Size(40, 40);
            this.buttonPan.TabIndex = 26;
            this.buttonPan.UseVisualStyleBackColor = true;
            this.buttonPan.Click += new System.EventHandler(this.buttonPan_Click);
            // 
            // labelCombineSuccessful
            // 
            this.labelCombineSuccessful.AutoSize = true;
            this.labelCombineSuccessful.Location = new System.Drawing.Point(325, 458);
            this.labelCombineSuccessful.Name = "labelCombineSuccessful";
            this.labelCombineSuccessful.Size = new System.Drawing.Size(0, 13);
            this.labelCombineSuccessful.TabIndex = 25;
            this.labelCombineSuccessful.Visible = false;
            // 
            // buttonImportMapInfoLinks
            // 
            this.buttonImportMapInfoLinks.Location = new System.Drawing.Point(349, 451);
            this.buttonImportMapInfoLinks.Name = "buttonImportMapInfoLinks";
            this.buttonImportMapInfoLinks.Size = new System.Drawing.Size(129, 22);
            this.buttonImportMapInfoLinks.TabIndex = 24;
            this.buttonImportMapInfoLinks.Text = "Import MapInfo Links...";
            this.buttonImportMapInfoLinks.UseVisualStyleBackColor = true;
            this.buttonImportMapInfoLinks.Click += new System.EventHandler(this.buttonImportMapInfoLinks_Click);
            // 
            // buttonChangeLayer
            // 
            this.buttonChangeLayer.Location = new System.Drawing.Point(478, 450);
            this.buttonChangeLayer.Name = "buttonChangeLayer";
            this.buttonChangeLayer.Size = new System.Drawing.Size(92, 23);
            this.buttonChangeLayer.TabIndex = 23;
            this.buttonChangeLayer.Text = "Change layer...";
            this.buttonChangeLayer.UseVisualStyleBackColor = true;
            this.buttonChangeLayer.Click += new System.EventHandler(this.buttonChangeLayer_Click);
            // 
            // labelSelectionLayer
            // 
            this.labelSelectionLayer.AutoSize = true;
            this.labelSelectionLayer.Location = new System.Drawing.Point(346, 435);
            this.labelSelectionLayer.Name = "labelSelectionLayer";
            this.labelSelectionLayer.Size = new System.Drawing.Size(114, 13);
            this.labelSelectionLayer.TabIndex = 22;
            this.labelSelectionLayer.Text = "Basin (Selection) layer:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(349, 474);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(221, 20);
            this.textBox2.TabIndex = 21;
            // 
            // labelEndDate
            // 
            this.labelEndDate.AutoSize = true;
            this.labelEndDate.Location = new System.Drawing.Point(6, 393);
            this.labelEndDate.Name = "labelEndDate";
            this.labelEndDate.Size = new System.Drawing.Size(53, 13);
            this.labelEndDate.TabIndex = 20;
            this.labelEndDate.Text = "End date:";
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Location = new System.Drawing.Point(6, 367);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(56, 13);
            this.labelStartDate.TabIndex = 19;
            this.labelStartDate.Text = "Start date:";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(588, 517);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(92, 22);
            this.btnProcess.TabIndex = 18;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnFileBrowser
            // 
            this.btnFileBrowser.Location = new System.Drawing.Point(478, 495);
            this.btnFileBrowser.Name = "btnFileBrowser";
            this.btnFileBrowser.Size = new System.Drawing.Size(92, 23);
            this.btnFileBrowser.TabIndex = 17;
            this.btnFileBrowser.Text = "File browser...";
            this.btnFileBrowser.UseVisualStyleBackColor = true;
            this.btnFileBrowser.Click += new System.EventHandler(this.btnFileBrowser_Click);
            // 
            // txtOutputFileDestination
            // 
            this.txtOutputFileDestination.Location = new System.Drawing.Point(349, 519);
            this.txtOutputFileDestination.Name = "txtOutputFileDestination";
            this.txtOutputFileDestination.Size = new System.Drawing.Size(221, 20);
            this.txtOutputFileDestination.TabIndex = 16;
            // 
            // labelOutputFileLocation
            // 
            this.labelOutputFileLocation.AutoSize = true;
            this.labelOutputFileLocation.Location = new System.Drawing.Point(346, 500);
            this.labelOutputFileLocation.Name = "labelOutputFileLocation";
            this.labelOutputFileLocation.Size = new System.Drawing.Size(98, 13);
            this.labelOutputFileLocation.TabIndex = 15;
            this.labelOutputFileLocation.Text = "Output file location:";
            // 
            // checkBinaryOutput
            // 
            this.checkBinaryOutput.AutoSize = true;
            this.checkBinaryOutput.Location = new System.Drawing.Point(588, 494);
            this.checkBinaryOutput.Name = "checkBinaryOutput";
            this.checkBinaryOutput.Size = new System.Drawing.Size(88, 17);
            this.checkBinaryOutput.TabIndex = 13;
            this.checkBinaryOutput.Text = "Binary output";
            this.checkBinaryOutput.UseVisualStyleBackColor = true;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(65, 389);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(615, 20);
            this.dtpEndDate.TabIndex = 11;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(65, 363);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(615, 20);
            this.dtpStartDate.TabIndex = 9;
            // 
            // labelQuarterSectionList
            // 
            this.labelQuarterSectionList.AutoSize = true;
            this.labelQuarterSectionList.Location = new System.Drawing.Point(6, 450);
            this.labelQuarterSectionList.Name = "labelQuarterSectionList";
            this.labelQuarterSectionList.Size = new System.Drawing.Size(98, 13);
            this.labelQuarterSectionList.TabIndex = 8;
            this.labelQuarterSectionList.Text = "Quartersection List:";
            // 
            // txtQSBulkInput
            // 
            this.txtQSBulkInput.Location = new System.Drawing.Point(6, 466);
            this.txtQSBulkInput.Multiline = true;
            this.txtQSBulkInput.Name = "txtQSBulkInput";
            this.txtQSBulkInput.Size = new System.Drawing.Size(197, 74);
            this.txtQSBulkInput.TabIndex = 7;
            // 
            // buttonZoomOut
            // 
            this.buttonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomOut.Image")));
            this.buttonZoomOut.Location = new System.Drawing.Point(3, 187);
            this.buttonZoomOut.Name = "buttonZoomOut";
            this.buttonZoomOut.Size = new System.Drawing.Size(40, 40);
            this.buttonZoomOut.TabIndex = 6;
            this.buttonZoomOut.UseVisualStyleBackColor = true;
            this.buttonZoomOut.Click += new System.EventHandler(this.buttonZoomOut_Click);
            // 
            // buttonZoomIn
            // 
            this.buttonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomIn.Image")));
            this.buttonZoomIn.Location = new System.Drawing.Point(3, 141);
            this.buttonZoomIn.Name = "buttonZoomIn";
            this.buttonZoomIn.Size = new System.Drawing.Size(40, 40);
            this.buttonZoomIn.TabIndex = 5;
            this.buttonZoomIn.UseVisualStyleBackColor = true;
            this.buttonZoomIn.Click += new System.EventHandler(this.buttonZoomIn_Click);
            // 
            // tabPageMonitoringExtractor
            // 
            this.tabPageMonitoringExtractor.Controls.Add(this.button1);
            this.tabPageMonitoringExtractor.Controls.Add(this.button2);
            this.tabPageMonitoringExtractor.Controls.Add(this.button3);
            this.tabPageMonitoringExtractor.Controls.Add(this.button4);
            this.tabPageMonitoringExtractor.Controls.Add(this.button5);
            this.tabPageMonitoringExtractor.Controls.Add(this.panelMapControlMonitoring);
            this.tabPageMonitoringExtractor.Controls.Add(this.dataGridView1);
            this.tabPageMonitoringExtractor.Controls.Add(this.btnExportToExcel);
            this.tabPageMonitoringExtractor.Controls.Add(this.textBox1);
            this.tabPageMonitoringExtractor.Controls.Add(this.labelOutputLocation);
            this.tabPageMonitoringExtractor.Controls.Add(this.cbReportZeroRain);
            this.tabPageMonitoringExtractor.Controls.Add(this.nudTimeStep);
            this.tabPageMonitoringExtractor.Controls.Add(this.labelTimeStep);
            this.tabPageMonitoringExtractor.Controls.Add(this.dtpEndDay);
            this.tabPageMonitoringExtractor.Controls.Add(this.dtpStartDay);
            this.tabPageMonitoringExtractor.Controls.Add(this.labelRealRainEndTime);
            this.tabPageMonitoringExtractor.Controls.Add(this.labelRealRainStartTime);
            this.tabPageMonitoringExtractor.Controls.Add(this.dataGridViewRainGages);
            this.tabPageMonitoringExtractor.Location = new System.Drawing.Point(4, 22);
            this.tabPageMonitoringExtractor.Name = "tabPageMonitoringExtractor";
            this.tabPageMonitoringExtractor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMonitoringExtractor.Size = new System.Drawing.Size(686, 546);
            this.tabPageMonitoringExtractor.TabIndex = 1;
            this.tabPageMonitoringExtractor.Text = "Monitoring Extractor";
            this.tabPageMonitoringExtractor.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.environsensoridDataGridViewTextBoxColumn,
            this.stationidDataGridViewTextBoxColumn1,
            this.xdatacategorylocqualcombinedDataGridViewTextBoxColumn,
            this.datacategoryDataGridViewTextBoxColumn,
            this.locationqualifierDataGridViewTextBoxColumn,
            this.channelDataGridViewTextBoxColumn,
            this.channelnameDataGridViewTextBoxColumn,
            this.channeldescriptionDataGridViewTextBoxColumn,
            this.engineeringunitspanDataGridViewTextBoxColumn,
            this.engineeringunitzeroDataGridViewTextBoxColumn,
            this.engineeringunitsDataGridViewTextBoxColumn,
            this.hasdepthtoflowDataGridViewTextBoxColumn,
            this.startdateDataGridViewTextBoxColumn1,
            this.enddateDataGridViewTextBoxColumn1,
            this.createdateDataGridViewTextBoxColumn1,
            this.createbyDataGridViewTextBoxColumn1,
            this.updatedateDataGridViewTextBoxColumn1,
            this.updatebyDataGridViewTextBoxColumn1});
            this.dataGridView1.DataSource = this.eNVIRONSENSORBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(6, 357);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(674, 94);
            this.dataGridView1.TabIndex = 14;
            // 
            // eNVIRONSENSORBindingSource
            // 
            this.eNVIRONSENSORBindingSource.DataMember = "ENVIRON_SENSOR";
            this.eNVIRONSENSORBindingSource.DataSource = this.nEPTUNEDataSetBindingSource;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Location = new System.Drawing.Point(513, 517);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(144, 23);
            this.btnExportToExcel.TabIndex = 13;
            this.btnExportToExcel.Text = "Export to Excel file";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(512, 491);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(168, 20);
            this.textBox1.TabIndex = 11;
            // 
            // labelOutputLocation
            // 
            this.labelOutputLocation.AutoSize = true;
            this.labelOutputLocation.Location = new System.Drawing.Point(509, 475);
            this.labelOutputLocation.Name = "labelOutputLocation";
            this.labelOutputLocation.Size = new System.Drawing.Size(82, 13);
            this.labelOutputLocation.TabIndex = 10;
            this.labelOutputLocation.Text = "Output location:";
            // 
            // cbReportZeroRain
            // 
            this.cbReportZeroRain.AutoSize = true;
            this.cbReportZeroRain.Location = new System.Drawing.Point(512, 455);
            this.cbReportZeroRain.Name = "cbReportZeroRain";
            this.cbReportZeroRain.Size = new System.Drawing.Size(145, 17);
            this.cbReportZeroRain.TabIndex = 9;
            this.cbReportZeroRain.Text = "Report times with no rain.";
            this.cbReportZeroRain.UseVisualStyleBackColor = true;
            // 
            // nudTimeStep
            // 
            this.nudTimeStep.Location = new System.Drawing.Point(110, 520);
            this.nudTimeStep.Name = "nudTimeStep";
            this.nudTimeStep.Size = new System.Drawing.Size(393, 20);
            this.nudTimeStep.TabIndex = 8;
            this.nudTimeStep.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // labelTimeStep
            // 
            this.labelTimeStep.AutoSize = true;
            this.labelTimeStep.Location = new System.Drawing.Point(6, 522);
            this.labelTimeStep.Name = "labelTimeStep";
            this.labelTimeStep.Size = new System.Drawing.Size(98, 13);
            this.labelTimeStep.TabIndex = 7;
            this.labelTimeStep.Text = "Time step, minutes:";
            // 
            // dtpEndDay
            // 
            this.dtpEndDay.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dtpEndDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDay.Location = new System.Drawing.Point(110, 499);
            this.dtpEndDay.Name = "dtpEndDay";
            this.dtpEndDay.Size = new System.Drawing.Size(393, 20);
            this.dtpEndDay.TabIndex = 4;
            // 
            // dtpStartDay
            // 
            this.dtpStartDay.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dtpStartDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDay.Location = new System.Drawing.Point(110, 476);
            this.dtpStartDay.Name = "dtpStartDay";
            this.dtpStartDay.Size = new System.Drawing.Size(393, 20);
            this.dtpStartDay.TabIndex = 3;
            // 
            // labelRealRainEndTime
            // 
            this.labelRealRainEndTime.AutoSize = true;
            this.labelRealRainEndTime.Location = new System.Drawing.Point(53, 503);
            this.labelRealRainEndTime.Name = "labelRealRainEndTime";
            this.labelRealRainEndTime.Size = new System.Drawing.Size(51, 13);
            this.labelRealRainEndTime.TabIndex = 2;
            this.labelRealRainEndTime.Text = "End time:";
            // 
            // labelRealRainStartTime
            // 
            this.labelRealRainStartTime.AutoSize = true;
            this.labelRealRainStartTime.Location = new System.Drawing.Point(50, 480);
            this.labelRealRainStartTime.Name = "labelRealRainStartTime";
            this.labelRealRainStartTime.Size = new System.Drawing.Size(54, 13);
            this.labelRealRainStartTime.TabIndex = 1;
            this.labelRealRainStartTime.Text = "Start time:";
            // 
            // dataGridViewRainGages
            // 
            this.dataGridViewRainGages.AllowUserToAddRows = false;
            this.dataGridViewRainGages.AllowUserToDeleteRows = false;
            this.dataGridViewRainGages.AutoGenerateColumns = false;
            this.dataGridViewRainGages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRainGages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stationidDataGridViewTextBoxColumn,
            this.h2numberDataGridViewTextBoxColumn,
            this.stationnameDataGridViewTextBoxColumn,
            this.locationdescriptionDataGridViewTextBoxColumn,
            this.addressDataGridViewTextBoxColumn,
            this.locationtypeDataGridViewTextBoxColumn,
            this.startdateDataGridViewTextBoxColumn,
            this.enddateDataGridViewTextBoxColumn,
            this.stateplanexftDataGridViewTextBoxColumn,
            this.stateplaneyftDataGridViewTextBoxColumn,
            this.createdateDataGridViewTextBoxColumn,
            this.createbyDataGridViewTextBoxColumn,
            this.updatedateDataGridViewTextBoxColumn,
            this.updatebyDataGridViewTextBoxColumn});
            this.dataGridViewRainGages.DataSource = this.sTATIONBindingSource;
            this.dataGridViewRainGages.Location = new System.Drawing.Point(6, 257);
            this.dataGridViewRainGages.Name = "dataGridViewRainGages";
            this.dataGridViewRainGages.ReadOnly = true;
            this.dataGridViewRainGages.Size = new System.Drawing.Size(674, 94);
            this.dataGridViewRainGages.TabIndex = 0;
            // 
            // sTATIONBindingSource
            // 
            this.sTATIONBindingSource.DataMember = "STATION";
            this.sTATIONBindingSource.DataSource = this.nEPTUNEDataSetBindingSource;
            // 
            // oleDbConnection2
            // 
            this.oleDbConnection2.ConnectionString = resources.GetString("oleDbConnection2.ConnectionString");
            // 
            // qsDS1
            // 
            this.qsDS1.DataSetName = "qsDS";
            this.qsDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "workstation id=BES4712;packet size=4096;integrated security=SSPI;data source=Repo" +
                "rtsio;persist security info=False;initial catalog=NEPTUNE;connection timeout=0";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // rainDS
            // 
            this.rainDS.DataSetName = "RainDSClass";
            this.rainDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rAINSENSORBindingSource
            // 
            this.rAINSENSORBindingSource.DataMember = "RAIN_SENSOR";
            this.rAINSENSORBindingSource.DataSource = this.nEPTUNEDataSetBindingSource;
            // 
            // openFileDialogBasinLayer
            // 
            this.openFileDialogBasinLayer.FileName = "openFileDialog1";
            // 
            // openFileDialogMapInfoFile
            // 
            this.openFileDialogMapInfoFile.FileName = "openFileDialog1";
            // 
            // labelTimeRangeToGenerate
            // 
            this.labelTimeRangeToGenerate.AutoSize = true;
            this.labelTimeRangeToGenerate.Location = new System.Drawing.Point(3, 347);
            this.labelTimeRangeToGenerate.Name = "labelTimeRangeToGenerate";
            this.labelTimeRangeToGenerate.Size = new System.Drawing.Size(120, 13);
            this.labelTimeRangeToGenerate.TabIndex = 30;
            this.labelTimeRangeToGenerate.Text = "Time range to generate:";
            // 
            // environsensoridDataGridViewTextBoxColumn
            // 
            this.environsensoridDataGridViewTextBoxColumn.DataPropertyName = "environ_sensor_id";
            this.environsensoridDataGridViewTextBoxColumn.HeaderText = "environ_sensor_id";
            this.environsensoridDataGridViewTextBoxColumn.Name = "environsensoridDataGridViewTextBoxColumn";
            this.environsensoridDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stationidDataGridViewTextBoxColumn1
            // 
            this.stationidDataGridViewTextBoxColumn1.DataPropertyName = "station_id";
            this.stationidDataGridViewTextBoxColumn1.HeaderText = "station_id";
            this.stationidDataGridViewTextBoxColumn1.Name = "stationidDataGridViewTextBoxColumn1";
            this.stationidDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // xdatacategorylocqualcombinedDataGridViewTextBoxColumn
            // 
            this.xdatacategorylocqualcombinedDataGridViewTextBoxColumn.DataPropertyName = "x_data_category_loc_qual_combined";
            this.xdatacategorylocqualcombinedDataGridViewTextBoxColumn.HeaderText = "x_data_category_loc_qual_combined";
            this.xdatacategorylocqualcombinedDataGridViewTextBoxColumn.Name = "xdatacategorylocqualcombinedDataGridViewTextBoxColumn";
            this.xdatacategorylocqualcombinedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // datacategoryDataGridViewTextBoxColumn
            // 
            this.datacategoryDataGridViewTextBoxColumn.DataPropertyName = "data_category";
            this.datacategoryDataGridViewTextBoxColumn.HeaderText = "data_category";
            this.datacategoryDataGridViewTextBoxColumn.Name = "datacategoryDataGridViewTextBoxColumn";
            this.datacategoryDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // locationqualifierDataGridViewTextBoxColumn
            // 
            this.locationqualifierDataGridViewTextBoxColumn.DataPropertyName = "location_qualifier";
            this.locationqualifierDataGridViewTextBoxColumn.HeaderText = "location_qualifier";
            this.locationqualifierDataGridViewTextBoxColumn.Name = "locationqualifierDataGridViewTextBoxColumn";
            this.locationqualifierDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // channelDataGridViewTextBoxColumn
            // 
            this.channelDataGridViewTextBoxColumn.DataPropertyName = "channel";
            this.channelDataGridViewTextBoxColumn.HeaderText = "channel";
            this.channelDataGridViewTextBoxColumn.Name = "channelDataGridViewTextBoxColumn";
            this.channelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // channelnameDataGridViewTextBoxColumn
            // 
            this.channelnameDataGridViewTextBoxColumn.DataPropertyName = "channel_name";
            this.channelnameDataGridViewTextBoxColumn.HeaderText = "channel_name";
            this.channelnameDataGridViewTextBoxColumn.Name = "channelnameDataGridViewTextBoxColumn";
            this.channelnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // channeldescriptionDataGridViewTextBoxColumn
            // 
            this.channeldescriptionDataGridViewTextBoxColumn.DataPropertyName = "channel_description";
            this.channeldescriptionDataGridViewTextBoxColumn.HeaderText = "channel_description";
            this.channeldescriptionDataGridViewTextBoxColumn.Name = "channeldescriptionDataGridViewTextBoxColumn";
            this.channeldescriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // engineeringunitspanDataGridViewTextBoxColumn
            // 
            this.engineeringunitspanDataGridViewTextBoxColumn.DataPropertyName = "engineering_unit_span";
            this.engineeringunitspanDataGridViewTextBoxColumn.HeaderText = "engineering_unit_span";
            this.engineeringunitspanDataGridViewTextBoxColumn.Name = "engineeringunitspanDataGridViewTextBoxColumn";
            this.engineeringunitspanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // engineeringunitzeroDataGridViewTextBoxColumn
            // 
            this.engineeringunitzeroDataGridViewTextBoxColumn.DataPropertyName = "engineering_unit_zero";
            this.engineeringunitzeroDataGridViewTextBoxColumn.HeaderText = "engineering_unit_zero";
            this.engineeringunitzeroDataGridViewTextBoxColumn.Name = "engineeringunitzeroDataGridViewTextBoxColumn";
            this.engineeringunitzeroDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // engineeringunitsDataGridViewTextBoxColumn
            // 
            this.engineeringunitsDataGridViewTextBoxColumn.DataPropertyName = "engineering_units";
            this.engineeringunitsDataGridViewTextBoxColumn.HeaderText = "engineering_units";
            this.engineeringunitsDataGridViewTextBoxColumn.Name = "engineeringunitsDataGridViewTextBoxColumn";
            this.engineeringunitsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hasdepthtoflowDataGridViewTextBoxColumn
            // 
            this.hasdepthtoflowDataGridViewTextBoxColumn.DataPropertyName = "has_depth_to_flow";
            this.hasdepthtoflowDataGridViewTextBoxColumn.HeaderText = "has_depth_to_flow";
            this.hasdepthtoflowDataGridViewTextBoxColumn.Name = "hasdepthtoflowDataGridViewTextBoxColumn";
            this.hasdepthtoflowDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // startdateDataGridViewTextBoxColumn1
            // 
            this.startdateDataGridViewTextBoxColumn1.DataPropertyName = "start_date";
            this.startdateDataGridViewTextBoxColumn1.HeaderText = "start_date";
            this.startdateDataGridViewTextBoxColumn1.Name = "startdateDataGridViewTextBoxColumn1";
            this.startdateDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // enddateDataGridViewTextBoxColumn1
            // 
            this.enddateDataGridViewTextBoxColumn1.DataPropertyName = "end_date";
            this.enddateDataGridViewTextBoxColumn1.HeaderText = "end_date";
            this.enddateDataGridViewTextBoxColumn1.Name = "enddateDataGridViewTextBoxColumn1";
            this.enddateDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // createdateDataGridViewTextBoxColumn1
            // 
            this.createdateDataGridViewTextBoxColumn1.DataPropertyName = "create_date";
            this.createdateDataGridViewTextBoxColumn1.HeaderText = "create_date";
            this.createdateDataGridViewTextBoxColumn1.Name = "createdateDataGridViewTextBoxColumn1";
            this.createdateDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // createbyDataGridViewTextBoxColumn1
            // 
            this.createbyDataGridViewTextBoxColumn1.DataPropertyName = "create_by";
            this.createbyDataGridViewTextBoxColumn1.HeaderText = "create_by";
            this.createbyDataGridViewTextBoxColumn1.Name = "createbyDataGridViewTextBoxColumn1";
            this.createbyDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // updatedateDataGridViewTextBoxColumn1
            // 
            this.updatedateDataGridViewTextBoxColumn1.DataPropertyName = "update_date";
            this.updatedateDataGridViewTextBoxColumn1.HeaderText = "update_date";
            this.updatedateDataGridViewTextBoxColumn1.Name = "updatedateDataGridViewTextBoxColumn1";
            this.updatedateDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // updatebyDataGridViewTextBoxColumn1
            // 
            this.updatebyDataGridViewTextBoxColumn1.DataPropertyName = "update_by";
            this.updatebyDataGridViewTextBoxColumn1.HeaderText = "update_by";
            this.updatebyDataGridViewTextBoxColumn1.Name = "updatebyDataGridViewTextBoxColumn1";
            this.updatebyDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // nEPTUNEDataSetBindingSource
            // 
            this.nEPTUNEDataSetBindingSource.DataSource = this.nEPTUNEDataSet;
            this.nEPTUNEDataSetBindingSource.Position = 0;
            // 
            // nEPTUNEDataSet
            // 
            this.nEPTUNEDataSet.DataSetName = "NEPTUNEDataSet";
            this.nEPTUNEDataSet.EnforceConstraints = false;
            this.nEPTUNEDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // stationidDataGridViewTextBoxColumn
            // 
            this.stationidDataGridViewTextBoxColumn.DataPropertyName = "station_id";
            this.stationidDataGridViewTextBoxColumn.HeaderText = "station_id";
            this.stationidDataGridViewTextBoxColumn.Name = "stationidDataGridViewTextBoxColumn";
            this.stationidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // h2numberDataGridViewTextBoxColumn
            // 
            this.h2numberDataGridViewTextBoxColumn.DataPropertyName = "h2_number";
            this.h2numberDataGridViewTextBoxColumn.HeaderText = "h2_number";
            this.h2numberDataGridViewTextBoxColumn.Name = "h2numberDataGridViewTextBoxColumn";
            this.h2numberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stationnameDataGridViewTextBoxColumn
            // 
            this.stationnameDataGridViewTextBoxColumn.DataPropertyName = "station_name";
            this.stationnameDataGridViewTextBoxColumn.HeaderText = "station_name";
            this.stationnameDataGridViewTextBoxColumn.Name = "stationnameDataGridViewTextBoxColumn";
            this.stationnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // locationdescriptionDataGridViewTextBoxColumn
            // 
            this.locationdescriptionDataGridViewTextBoxColumn.DataPropertyName = "location_description";
            this.locationdescriptionDataGridViewTextBoxColumn.HeaderText = "location_description";
            this.locationdescriptionDataGridViewTextBoxColumn.Name = "locationdescriptionDataGridViewTextBoxColumn";
            this.locationdescriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // addressDataGridViewTextBoxColumn
            // 
            this.addressDataGridViewTextBoxColumn.DataPropertyName = "address";
            this.addressDataGridViewTextBoxColumn.HeaderText = "address";
            this.addressDataGridViewTextBoxColumn.Name = "addressDataGridViewTextBoxColumn";
            this.addressDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // locationtypeDataGridViewTextBoxColumn
            // 
            this.locationtypeDataGridViewTextBoxColumn.DataPropertyName = "location_type";
            this.locationtypeDataGridViewTextBoxColumn.HeaderText = "location_type";
            this.locationtypeDataGridViewTextBoxColumn.Name = "locationtypeDataGridViewTextBoxColumn";
            this.locationtypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // startdateDataGridViewTextBoxColumn
            // 
            this.startdateDataGridViewTextBoxColumn.DataPropertyName = "start_date";
            this.startdateDataGridViewTextBoxColumn.HeaderText = "start_date";
            this.startdateDataGridViewTextBoxColumn.Name = "startdateDataGridViewTextBoxColumn";
            this.startdateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // enddateDataGridViewTextBoxColumn
            // 
            this.enddateDataGridViewTextBoxColumn.DataPropertyName = "end_date";
            this.enddateDataGridViewTextBoxColumn.HeaderText = "end_date";
            this.enddateDataGridViewTextBoxColumn.Name = "enddateDataGridViewTextBoxColumn";
            this.enddateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateplanexftDataGridViewTextBoxColumn
            // 
            this.stateplanexftDataGridViewTextBoxColumn.DataPropertyName = "state_plane_x_ft";
            this.stateplanexftDataGridViewTextBoxColumn.HeaderText = "state_plane_x_ft";
            this.stateplanexftDataGridViewTextBoxColumn.Name = "stateplanexftDataGridViewTextBoxColumn";
            this.stateplanexftDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateplaneyftDataGridViewTextBoxColumn
            // 
            this.stateplaneyftDataGridViewTextBoxColumn.DataPropertyName = "state_plane_y_ft";
            this.stateplaneyftDataGridViewTextBoxColumn.HeaderText = "state_plane_y_ft";
            this.stateplaneyftDataGridViewTextBoxColumn.Name = "stateplaneyftDataGridViewTextBoxColumn";
            this.stateplaneyftDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdateDataGridViewTextBoxColumn
            // 
            this.createdateDataGridViewTextBoxColumn.DataPropertyName = "create_date";
            this.createdateDataGridViewTextBoxColumn.HeaderText = "create_date";
            this.createdateDataGridViewTextBoxColumn.Name = "createdateDataGridViewTextBoxColumn";
            this.createdateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createbyDataGridViewTextBoxColumn
            // 
            this.createbyDataGridViewTextBoxColumn.DataPropertyName = "create_by";
            this.createbyDataGridViewTextBoxColumn.HeaderText = "create_by";
            this.createbyDataGridViewTextBoxColumn.Name = "createbyDataGridViewTextBoxColumn";
            this.createbyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // updatedateDataGridViewTextBoxColumn
            // 
            this.updatedateDataGridViewTextBoxColumn.DataPropertyName = "update_date";
            this.updatedateDataGridViewTextBoxColumn.HeaderText = "update_date";
            this.updatedateDataGridViewTextBoxColumn.Name = "updatedateDataGridViewTextBoxColumn";
            this.updatedateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // updatebyDataGridViewTextBoxColumn
            // 
            this.updatebyDataGridViewTextBoxColumn.DataPropertyName = "update_by";
            this.updatebyDataGridViewTextBoxColumn.HeaderText = "update_by";
            this.updatebyDataGridViewTextBoxColumn.Name = "updatebyDataGridViewTextBoxColumn";
            this.updatebyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sTATIONTableAdapter
            // 
            this.sTATIONTableAdapter.ClearBeforeFill = true;
            // 
            // rAIN_SENSORTableAdapter
            // 
            this.rAIN_SENSORTableAdapter.ClearBeforeFill = true;
            // 
            // eNVIRON_SENSORTableAdapter
            // 
            this.eNVIRON_SENSORTableAdapter.ClearBeforeFill = true;
            // 
            // panelMapControlMonitoring
            // 
            this.panelMapControlMonitoring.Controls.Add(this.axMapControlMonitoring);
            this.panelMapControlMonitoring.Location = new System.Drawing.Point(49, 3);
            this.panelMapControlMonitoring.Name = "panelMapControlMonitoring";
            this.panelMapControlMonitoring.Size = new System.Drawing.Size(631, 248);
            this.panelMapControlMonitoring.TabIndex = 28;
            // 
            // axMapControlMonitoring
            // 
            this.axMapControlMonitoring.Dock = System.Windows.Forms.DockStyle.Top;
            this.axMapControlMonitoring.Location = new System.Drawing.Point(0, 0);
            this.axMapControlMonitoring.Margin = new System.Windows.Forms.Padding(50);
            this.axMapControlMonitoring.Name = "axMapControlMonitoring";
            this.axMapControlMonitoring.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControlMonitoring.OcxState")));
            this.axMapControlMonitoring.Size = new System.Drawing.Size(631, 248);
            this.axMapControlMonitoring.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(3, 95);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 34;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(3, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 40);
            this.button2.TabIndex = 33;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(3, 3);
            this.button3.Name = "button3";
            this.button3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button3.Size = new System.Drawing.Size(40, 40);
            this.button3.TabIndex = 32;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(3, 187);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 40);
            this.button4.TabIndex = 31;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(3, 141);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 40);
            this.button5.TabIndex = 30;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 596);
            this.Controls.Add(this.tabControl_ToolSelect);
            this.Name = "UserInterface";
            this.Text = "DaisyMae";
            this.Load += new System.EventHandler(this.UserInterface_Load);
            this.Closed += new System.EventHandler(this.UserInterface_Unload);
            this.tabControl_ToolSelect.ResumeLayout(false);
            this.tabPageVirtualRainfallGenerator.ResumeLayout(false);
            this.tabPageVirtualRainfallGenerator.PerformLayout();
            this.panelMapControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            this.tabPageMonitoringExtractor.ResumeLayout(false);
            this.tabPageMonitoringExtractor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eNVIRONSENSORBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRainGages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sTATIONBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qsDS1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rainDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rAINSENSORBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nEPTUNEDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nEPTUNEDataSet)).EndInit();
            this.panelMapControlMonitoring.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControlMonitoring)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private string CombineSuccessful = "";
        private System.Windows.Forms.TabControl tabControl_ToolSelect;
        private System.Windows.Forms.TabPage tabPageVirtualRainfallGenerator;
        private System.Windows.Forms.TabPage tabPageMonitoringExtractor;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
        //private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl;
        private System.Windows.Forms.TextBox txtQSBulkInput;
        private System.Windows.Forms.Button buttonZoomOut;
        private System.Windows.Forms.Button buttonZoomIn;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label labelQuarterSectionList;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label labelOutputFileLocation;
        private System.Windows.Forms.CheckBox checkBinaryOutput;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnFileBrowser;
        private System.Windows.Forms.TextBox txtOutputFileDestination;
        private System.Windows.Forms.DataGridView dataGridViewRainGages;
        private System.Windows.Forms.Label labelEndDate;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.BindingSource sTATIONBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn h2numberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DateTimePicker dtpStartDay;
        private System.Windows.Forms.Label labelRealRainEndTime;
        private System.Windows.Forms.Label labelRealRainStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndDay;
        private System.Windows.Forms.NumericUpDown nudTimeStep;
        private System.Windows.Forms.Label labelTimeStep;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelOutputLocation;
        private System.Windows.Forms.CheckBox cbReportZeroRain;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationdescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationtypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startdateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn enddateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateplanexftDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateplaneyftDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createbyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatebyDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource rAINSENSORBindingSource;
        private ViRT.NEPTUNEDataSetTableAdapters.RAIN_SENSORTableAdapter rAIN_SENSORTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource eNVIRONSENSORBindingSource;
        private ViRT.NEPTUNEDataSetTableAdapters.ENVIRON_SENSORTableAdapter eNVIRON_SENSORTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn environsensoridDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationidDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn xdatacategorylocqualcombinedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datacategoryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationqualifierDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn channeldescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn engineeringunitspanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn engineeringunitzeroDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn engineeringunitsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hasdepthtoflowDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startdateDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn enddateDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdateDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn createbyDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedateDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatebyDataGridViewTextBoxColumn1;
        private System.Windows.Forms.Label labelSelectionLayer;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonChangeLayer;
        private System.Windows.Forms.OpenFileDialog openFileDialogBasinLayer;
        private System.Windows.Forms.Button buttonImportMapInfoLinks;
        private System.Windows.Forms.OpenFileDialog openFileDialogMapInfoFile;
        private System.Windows.Forms.Label labelCombineSuccessful;
        private System.Windows.Forms.Button buttonPan;
        private System.Windows.Forms.Panel panelMapControl;
        private System.Windows.Forms.Button buttonSelectByQuartersection;
        private System.Windows.Forms.Button buttonSelectByBasin;
        private System.Windows.Forms.Label labelTimeRangeToGenerate;
        private System.Windows.Forms.Panel panelMapControlMonitoring;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControlMonitoring;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

