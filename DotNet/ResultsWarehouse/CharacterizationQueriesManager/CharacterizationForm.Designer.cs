namespace SystemsAnalysis.Characterization
{
    partial class CharacterizationForm
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
            m_AoInitialize.Shutdown();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharacterizationForm));
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            this.tabMapView = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.pnlMapControls = new System.Windows.Forms.Panel();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnPan = new System.Windows.Forms.Button();
            this.axMapControl = new ESRI.ArcGIS.MapControl.AxMapControl();
            this.tabCharacterizationView = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.axWebBrowser = new AxSHDocVw.AxWebBrowser();
            this.tabStatusView = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.statusTextBox = new System.Windows.Forms.RichTextBox();
            this.tabModelCatalog = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.btnImportFromIni = new System.Windows.Forms.Button();
            this.btnImportFromModel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstStopLinks = new System.Windows.Forms.ListBox();
            this.btnAddStopLink = new System.Windows.Forms.Button();
            this.btnRemoveStopLink = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstStartLinks = new System.Windows.Forms.ListBox();
            this.btnRemoveStartLink = new System.Windows.Forms.Button();
            this.btnAddStartLink = new System.Windows.Forms.Button();
            this.btnPreviewTrace = new System.Windows.Forms.Button();
            this.btnClearList = new System.Windows.Forms.Button();
            this.tabInteractiveSelect = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.tabPolygonSelect = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.btnReconcileModel = new System.Windows.Forms.Button();
            this.tabControlOutputDisplay = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.tabOutputDisplaySharedControlsPage = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.txtStudyAreaName = new System.Windows.Forms.TextBox();
            this.lblStudyArea = new System.Windows.Forms.Label();
            this.btnCharacterize = new System.Windows.Forms.Button();
            this.btnSelectCharDir = new System.Windows.Forms.Button();
            this.pnlCharacterizationView = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbReportChooser = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlStudyArea = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.tabStudyAreaSharedControlsPage = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemFileExit = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemClearStatusLog = new System.Windows.Forms.MenuItem();
            this.menuItemSettings = new System.Windows.Forms.MenuItem();
            this.menuItemTools = new System.Windows.Forms.MenuItem();
            this.menuItemManageModelCatalog = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.settings = new SystemsAnalysis.Characterization.Settings();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabMapView.SuspendLayout();
            this.pnlMapControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            this.tabCharacterizationView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).BeginInit();
            this.tabStatusView.SuspendLayout();
            this.tabModelCatalog.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlOutputDisplay)).BeginInit();
            this.tabControlOutputDisplay.SuspendLayout();
            this.pnlCharacterizationView.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportChooser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlStudyArea)).BeginInit();
            this.tabControlStudyArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.settings)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMapView
            // 
            this.tabMapView.Controls.Add(this.pnlMapControls);
            this.tabMapView.Controls.Add(this.axMapControl);
            this.tabMapView.Location = new System.Drawing.Point(2, 25);
            this.tabMapView.Name = "tabMapView";
            this.tabMapView.Size = new System.Drawing.Size(480, 463);
            // 
            // pnlMapControls
            // 
            this.pnlMapControls.Controls.Add(this.btnZoomIn);
            this.pnlMapControls.Controls.Add(this.btnZoomOut);
            this.pnlMapControls.Controls.Add(this.btnPan);
            this.pnlMapControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMapControls.Location = new System.Drawing.Point(0, 417);
            this.pnlMapControls.Name = "pnlMapControls";
            this.pnlMapControls.Size = new System.Drawing.Size(480, 46);
            this.pnlMapControls.TabIndex = 3;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Location = new System.Drawing.Point(102, 12);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(115, 27);
            this.btnZoomIn.TabIndex = 14;
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Location = new System.Drawing.Point(223, 12);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(90, 27);
            this.btnZoomOut.TabIndex = 15;
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnPan
            // 
            this.btnPan.Location = new System.Drawing.Point(319, 12);
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(90, 27);
            this.btnPan.TabIndex = 16;
            this.btnPan.Text = "Pan";
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // axMapControl
            // 
            this.axMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl.Location = new System.Drawing.Point(0, 0);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
            this.axMapControl.Size = new System.Drawing.Size(480, 463);
            this.axMapControl.TabIndex = 2;
            // 
            // tabCharacterizationView
            // 
            this.tabCharacterizationView.Controls.Add(this.axWebBrowser);
            this.tabCharacterizationView.Location = new System.Drawing.Point(-10000, -10000);
            this.tabCharacterizationView.Name = "tabCharacterizationView";
            this.tabCharacterizationView.Size = new System.Drawing.Size(480, 463);
            // 
            // axWebBrowser
            // 
            this.axWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWebBrowser.Enabled = true;
            this.axWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.axWebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser.OcxState")));
            this.axWebBrowser.Size = new System.Drawing.Size(480, 463);
            this.axWebBrowser.TabIndex = 1;
            // 
            // tabStatusView
            // 
            this.tabStatusView.Controls.Add(this.statusTextBox);
            this.tabStatusView.Location = new System.Drawing.Point(-10000, -10000);
            this.tabStatusView.Name = "tabStatusView";
            this.tabStatusView.Size = new System.Drawing.Size(480, 463);
            // 
            // statusTextBox
            // 
            this.statusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusTextBox.Location = new System.Drawing.Point(0, 0);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(480, 463);
            this.statusTextBox.TabIndex = 2;
            this.statusTextBox.Text = "";
            // 
            // tabModelCatalog
            // 
            this.tabModelCatalog.Controls.Add(this.btnImportFromIni);
            this.tabModelCatalog.Controls.Add(this.btnImportFromModel);
            this.tabModelCatalog.Controls.Add(this.groupBox2);
            this.tabModelCatalog.Controls.Add(this.groupBox1);
            this.tabModelCatalog.Controls.Add(this.btnPreviewTrace);
            this.tabModelCatalog.Controls.Add(this.btnClearList);
            this.tabModelCatalog.Location = new System.Drawing.Point(2, 25);
            this.tabModelCatalog.Name = "tabModelCatalog";
            this.tabModelCatalog.Size = new System.Drawing.Size(446, 250);
            // 
            // btnImportFromIni
            // 
            this.btnImportFromIni.Location = new System.Drawing.Point(328, 37);
            this.btnImportFromIni.Name = "btnImportFromIni";
            this.btnImportFromIni.Size = new System.Drawing.Size(115, 47);
            this.btnImportFromIni.TabIndex = 16;
            this.btnImportFromIni.Text = "Import From Model.ini";
            this.btnImportFromIni.Click += new System.EventHandler(this.btnImportFromIni_Click);
            // 
            // btnImportFromModel
            // 
            this.btnImportFromModel.Location = new System.Drawing.Point(328, 90);
            this.btnImportFromModel.Name = "btnImportFromModel";
            this.btnImportFromModel.Size = new System.Drawing.Size(115, 47);
            this.btnImportFromModel.TabIndex = 15;
            this.btnImportFromModel.Text = "Import From Model Catalog";
            this.btnImportFromModel.Click += new System.EventHandler(this.btnImportFromModel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.lstStopLinks);
            this.groupBox2.Controls.Add(this.btnAddStopLink);
            this.groupBox2.Controls.Add(this.btnRemoveStopLink);
            this.groupBox2.Location = new System.Drawing.Point(166, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(157, 195);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stop Links";
            // 
            // lstStopLinks
            // 
            this.lstStopLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstStopLinks.ItemHeight = 16;
            this.lstStopLinks.Location = new System.Drawing.Point(6, 18);
            this.lstStopLinks.Name = "lstStopLinks";
            this.lstStopLinks.Size = new System.Drawing.Size(144, 132);
            this.lstStopLinks.TabIndex = 2;
            // 
            // btnAddStopLink
            // 
            this.btnAddStopLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddStopLink.Location = new System.Drawing.Point(6, 160);
            this.btnAddStopLink.Name = "btnAddStopLink";
            this.btnAddStopLink.Size = new System.Drawing.Size(67, 27);
            this.btnAddStopLink.TabIndex = 12;
            this.btnAddStopLink.Text = "Add";
            this.btnAddStopLink.Click += new System.EventHandler(this.btnAddStopLink_Click);
            // 
            // btnRemoveStopLink
            // 
            this.btnRemoveStopLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveStopLink.Location = new System.Drawing.Point(79, 160);
            this.btnRemoveStopLink.Name = "btnRemoveStopLink";
            this.btnRemoveStopLink.Size = new System.Drawing.Size(71, 27);
            this.btnRemoveStopLink.TabIndex = 7;
            this.btnRemoveStopLink.Text = "Remove";
            this.btnRemoveStopLink.Click += new System.EventHandler(this.btnRemoveStopLink_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lstStartLinks);
            this.groupBox1.Controls.Add(this.btnRemoveStartLink);
            this.groupBox1.Controls.Add(this.btnAddStartLink);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 195);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Start Links";
            // 
            // lstStartLinks
            // 
            this.lstStartLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstStartLinks.ItemHeight = 16;
            this.lstStartLinks.Location = new System.Drawing.Point(6, 18);
            this.lstStartLinks.Name = "lstStartLinks";
            this.lstStartLinks.Size = new System.Drawing.Size(144, 132);
            this.lstStartLinks.TabIndex = 1;
            // 
            // btnRemoveStartLink
            // 
            this.btnRemoveStartLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveStartLink.Location = new System.Drawing.Point(79, 160);
            this.btnRemoveStartLink.Name = "btnRemoveStartLink";
            this.btnRemoveStartLink.Size = new System.Drawing.Size(71, 27);
            this.btnRemoveStartLink.TabIndex = 6;
            this.btnRemoveStartLink.Text = "Remove";
            this.btnRemoveStartLink.Click += new System.EventHandler(this.btnRemoveStartLink_Click);
            // 
            // btnAddStartLink
            // 
            this.btnAddStartLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddStartLink.Location = new System.Drawing.Point(6, 160);
            this.btnAddStartLink.Name = "btnAddStartLink";
            this.btnAddStartLink.Size = new System.Drawing.Size(67, 27);
            this.btnAddStartLink.TabIndex = 11;
            this.btnAddStartLink.Text = "Add";
            this.btnAddStartLink.Click += new System.EventHandler(this.btnAddStartLink_Click);
            // 
            // btnPreviewTrace
            // 
            this.btnPreviewTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreviewTrace.Location = new System.Drawing.Point(3, 210);
            this.btnPreviewTrace.Name = "btnPreviewTrace";
            this.btnPreviewTrace.Size = new System.Drawing.Size(320, 37);
            this.btnPreviewTrace.TabIndex = 8;
            this.btnPreviewTrace.Text = "Preview Trace";
            this.btnPreviewTrace.Click += new System.EventHandler(this.btnPreviewTrace_Click);
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(328, 143);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(115, 47);
            this.btnClearList.TabIndex = 5;
            this.btnClearList.Text = "Clear Links";
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // tabInteractiveSelect
            // 
            this.tabInteractiveSelect.Enabled = false;
            this.tabInteractiveSelect.Location = new System.Drawing.Point(-10000, -10000);
            this.tabInteractiveSelect.Name = "tabInteractiveSelect";
            this.tabInteractiveSelect.Size = new System.Drawing.Size(446, 250);
            // 
            // tabPolygonSelect
            // 
            this.tabPolygonSelect.Enabled = false;
            this.tabPolygonSelect.Location = new System.Drawing.Point(-10000, -10000);
            this.tabPolygonSelect.Name = "tabPolygonSelect";
            this.tabPolygonSelect.Size = new System.Drawing.Size(446, 250);
            // 
            // btnReconcileModel
            // 
            this.btnReconcileModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReconcileModel.Location = new System.Drawing.Point(234, 454);
            this.btnReconcileModel.Name = "btnReconcileModel";
            this.btnReconcileModel.Size = new System.Drawing.Size(144, 27);
            this.btnReconcileModel.TabIndex = 21;
            this.btnReconcileModel.Text = "Reconcile Model";
            this.btnReconcileModel.Click += new System.EventHandler(this.btnReconcileModel_Click);
            // 
            // tabControlOutputDisplay
            // 
            this.tabControlOutputDisplay.Controls.Add(this.tabOutputDisplaySharedControlsPage);
            this.tabControlOutputDisplay.Controls.Add(this.tabMapView);
            this.tabControlOutputDisplay.Controls.Add(this.tabStatusView);
            this.tabControlOutputDisplay.Controls.Add(this.tabCharacterizationView);
            this.tabControlOutputDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOutputDisplay.Location = new System.Drawing.Point(0, 0);
            this.tabControlOutputDisplay.Name = "tabControlOutputDisplay";
            this.tabControlOutputDisplay.SharedControlsPage = this.tabOutputDisplaySharedControlsPage;
            this.tabControlOutputDisplay.Size = new System.Drawing.Size(484, 490);
            this.tabControlOutputDisplay.TabIndex = 24;
            ultraTab1.TabPage = this.tabMapView;
            ultraTab1.Text = "Map View";
            ultraTab2.TabPage = this.tabCharacterizationView;
            ultraTab2.Text = "Characterization View";
            ultraTab3.TabPage = this.tabStatusView;
            ultraTab3.Text = "Status Log";
            this.tabControlOutputDisplay.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2,
            ultraTab3});
            // 
            // tabOutputDisplaySharedControlsPage
            // 
            this.tabOutputDisplaySharedControlsPage.Location = new System.Drawing.Point(-10000, -10000);
            this.tabOutputDisplaySharedControlsPage.Name = "tabOutputDisplaySharedControlsPage";
            this.tabOutputDisplaySharedControlsPage.Size = new System.Drawing.Size(480, 463);
            // 
            // txtStudyAreaName
            // 
            this.txtStudyAreaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStudyAreaName.Location = new System.Drawing.Point(9, 33);
            this.txtStudyAreaName.Name = "txtStudyAreaName";
            this.txtStudyAreaName.Size = new System.Drawing.Size(422, 22);
            this.txtStudyAreaName.TabIndex = 9;
            // 
            // lblStudyArea
            // 
            this.lblStudyArea.Location = new System.Drawing.Point(6, 6);
            this.lblStudyArea.Name = "lblStudyArea";
            this.lblStudyArea.Size = new System.Drawing.Size(131, 25);
            this.lblStudyArea.TabIndex = 10;
            this.lblStudyArea.Text = "Study Area Name:";
            // 
            // btnCharacterize
            // 
            this.btnCharacterize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCharacterize.Location = new System.Drawing.Point(54, 442);
            this.btnCharacterize.Name = "btnCharacterize";
            this.btnCharacterize.Size = new System.Drawing.Size(174, 39);
            this.btnCharacterize.TabIndex = 0;
            this.btnCharacterize.Text = "Characterize";
            this.btnCharacterize.Click += new System.EventHandler(this.btnCharacterize_Click);
            // 
            // btnSelectCharDir
            // 
            this.btnSelectCharDir.Location = new System.Drawing.Point(-81, 0);
            this.btnSelectCharDir.Name = "btnSelectCharDir";
            this.btnSelectCharDir.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCharDir.TabIndex = 21;
            // 
            // pnlCharacterizationView
            // 
            this.pnlCharacterizationView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCharacterizationView.Controls.Add(this.splitContainer1);
            this.pnlCharacterizationView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharacterizationView.Location = new System.Drawing.Point(0, 0);
            this.pnlCharacterizationView.Name = "pnlCharacterizationView";
            this.pnlCharacterizationView.Size = new System.Drawing.Size(950, 496);
            this.pnlCharacterizationView.TabIndex = 24;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.cmbReportChooser);
            this.splitContainer1.Panel1.Controls.Add(this.btnReconcileModel);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tabControlStudyArea);
            this.splitContainer1.Panel1.Controls.Add(this.lblStudyArea);
            this.splitContainer1.Panel1.Controls.Add(this.txtStudyAreaName);
            this.splitContainer1.Panel1.Controls.Add(this.btnCharacterize);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlOutputDisplay);
            this.splitContainer1.Size = new System.Drawing.Size(948, 494);
            this.splitContainer1.SplitterDistance = 456;
            this.splitContainer1.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(6, 384);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 25);
            this.label2.TabIndex = 23;
            this.label2.Text = "Choose a report:";
            // 
            // cmbReportChooser
            // 
            this.cmbReportChooser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbReportChooser.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbReportChooser.DisplayLayout.Appearance = appearance1;
            this.cmbReportChooser.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            ultraGridBand1.Header.Enabled = false;
            this.cmbReportChooser.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.cmbReportChooser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cmbReportChooser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbReportChooser.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbReportChooser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.cmbReportChooser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbReportChooser.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.cmbReportChooser.DisplayLayout.MaxColScrollRegions = 1;
            this.cmbReportChooser.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbReportChooser.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmbReportChooser.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.cmbReportChooser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cmbReportChooser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.cmbReportChooser.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cmbReportChooser.DisplayLayout.Override.CellAppearance = appearance8;
            this.cmbReportChooser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cmbReportChooser.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbReportChooser.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.cmbReportChooser.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.cmbReportChooser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cmbReportChooser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.cmbReportChooser.DisplayLayout.Override.RowAppearance = appearance11;
            this.cmbReportChooser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbReportChooser.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.cmbReportChooser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cmbReportChooser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cmbReportChooser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cmbReportChooser.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
            this.cmbReportChooser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReportChooser.Location = new System.Drawing.Point(6, 411);
            this.cmbReportChooser.Name = "cmbReportChooser";
            this.cmbReportChooser.Size = new System.Drawing.Size(422, 25);
            this.cmbReportChooser.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(6, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(443, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Characterize study area using:";
            // 
            // tabControlStudyArea
            // 
            this.tabControlStudyArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlStudyArea.Controls.Add(this.tabStudyAreaSharedControlsPage);
            this.tabControlStudyArea.Controls.Add(this.tabModelCatalog);
            this.tabControlStudyArea.Controls.Add(this.tabInteractiveSelect);
            this.tabControlStudyArea.Controls.Add(this.tabPolygonSelect);
            this.tabControlStudyArea.Location = new System.Drawing.Point(4, 90);
            this.tabControlStudyArea.Name = "tabControlStudyArea";
            this.tabControlStudyArea.SharedControlsPage = this.tabStudyAreaSharedControlsPage;
            this.tabControlStudyArea.Size = new System.Drawing.Size(450, 277);
            this.tabControlStudyArea.TabIndex = 2;
            ultraTab4.TabPage = this.tabModelCatalog;
            ultraTab4.Text = "Network Trace";
            ultraTab5.TabPage = this.tabInteractiveSelect;
            ultraTab5.Text = "Interactive Select";
            ultraTab6.TabPage = this.tabPolygonSelect;
            ultraTab6.Text = "Polygon Select";
            this.tabControlStudyArea.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab4,
            ultraTab5,
            ultraTab6});
            // 
            // tabStudyAreaSharedControlsPage
            // 
            this.tabStudyAreaSharedControlsPage.Location = new System.Drawing.Point(-10000, -10000);
            this.tabStudyAreaSharedControlsPage.Name = "tabStudyAreaSharedControlsPage";
            this.tabStudyAreaSharedControlsPage.Size = new System.Drawing.Size(446, 250);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItem1,
            this.menuItemTools,
            this.menuItemHelp});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFileExit});
            this.menuItemFile.Text = "File";
            // 
            // menuItemFileExit
            // 
            this.menuItemFileExit.Index = 0;
            this.menuItemFileExit.Text = "Exit";
            this.menuItemFileExit.Click += new System.EventHandler(this.menuItemFileExit_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemClearStatusLog,
            this.menuItemSettings});
            this.menuItem1.Text = "Edit";
            // 
            // menuItemClearStatusLog
            // 
            this.menuItemClearStatusLog.Index = 0;
            this.menuItemClearStatusLog.Text = "Clear Status Log";
            this.menuItemClearStatusLog.Click += new System.EventHandler(this.menuItemToolsClearLog_Click);
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Index = 1;
            this.menuItemSettings.Text = "Settings";
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemToolsSettings_Click);
            // 
            // menuItemTools
            // 
            this.menuItemTools.Index = 2;
            this.menuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemManageModelCatalog});
            this.menuItemTools.Text = "Tools";
            // 
            // menuItemManageModelCatalog
            // 
            this.menuItemManageModelCatalog.Index = 0;
            this.menuItemManageModelCatalog.Text = "Manage Model Catalog";
            this.menuItemManageModelCatalog.Click += new System.EventHandler(this.menuItemManageModelCatalog_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 3;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAbout});
            this.menuItemHelp.Text = "Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 0;
            this.menuItemAbout.Text = "About";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // settings
            // 
            this.settings.DataSetName = "Settings";
            this.settings.EnforceConstraints = false;
            this.settings.Locale = new System.Globalization.CultureInfo("en-US");
            this.settings.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.Title = "Save characterization report as...";
            // 
            // CharacterizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 496);
            this.Controls.Add(this.pnlCharacterizationView);
            this.Controls.Add(this.btnSelectCharDir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu;
            this.Name = "CharacterizationForm";
            this.Text = "Characterization Queries Manager";
            this.tabMapView.ResumeLayout(false);
            this.pnlMapControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            this.tabCharacterizationView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).EndInit();
            this.tabStatusView.ResumeLayout(false);
            this.tabModelCatalog.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlOutputDisplay)).EndInit();
            this.tabControlOutputDisplay.ResumeLayout(false);
            this.pnlCharacterizationView.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportChooser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlStudyArea)).EndInit();
            this.tabControlStudyArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.settings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPreviewTrace;
        private System.Windows.Forms.Button btnRemoveStopLink;
        private System.Windows.Forms.Button btnRemoveStartLink;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.TextBox txtStudyAreaName;
        private System.Windows.Forms.Button btnAddStopLink;
        private System.Windows.Forms.Button btnAddStartLink;
        private System.Windows.Forms.Label lblStudyArea;
        private System.Windows.Forms.ListBox lstStopLinks;
        private System.Windows.Forms.ListBox lstStartLinks;
        private System.Windows.Forms.Button btnCharacterize;
        private System.Windows.Forms.Button btnSelectCharDir;
        private Settings settings;
        private System.Windows.Forms.Panel pnlCharacterizationView;
        private System.Windows.Forms.Button btnReconcileModel;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItemFile;
        private System.Windows.Forms.MenuItem menuItemFileExit;
        private System.Windows.Forms.MenuItem menuItemTools;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl tabControlStudyArea;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage tabStudyAreaSharedControlsPage;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabModelCatalog;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabInteractiveSelect;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabPolygonSelect;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage tabOutputDisplaySharedControlsPage;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabMapView;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabStatusView;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabCharacterizationView;
        private System.Windows.Forms.Panel pnlMapControls;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnPan;
        private ESRI.ArcGIS.MapControl.AxMapControl axMapControl;
        private System.Windows.Forms.RichTextBox statusTextBox;
        private AxSHDocVw.AxWebBrowser axWebBrowser;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl tabControlOutputDisplay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImportFromModel;
        private System.Windows.Forms.MenuItem menuItemManageModelCatalog;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemClearStatusLog;
        private System.Windows.Forms.MenuItem menuItemSettings;
        private System.Windows.Forms.MenuItem menuItemHelp;
        private System.Windows.Forms.MenuItem menuItemAbout;
        private System.Windows.Forms.Label label2;
        private Infragistics.Win.UltraWinGrid.UltraCombo cmbReportChooser;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btnImportFromIni;
    }
}