namespace SystemsAnalysis.ModelConstruction.AlternativesToolkit
{
    partial class AlternativeEngineForm
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
            System.IO.Stream configStream = GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.ModelConstruction.AlternativesToolkit.Default_Settings.xml");
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            this.config.WriteXml(this.configPath);	
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
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("History", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Date", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("User");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Action");
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem4 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem5 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem6 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem7 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab(true);
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab11 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab12 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlternativeEngineForm));
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.cmbAlternativeList = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.cmbBaseModel = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.btnBrowseBaseModel = new Infragistics.Win.Misc.UltraButton();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.engineOutput = new SystemsAnalysis.Utils.StatusTextBox.StatusTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
            this.grdAlternativeHistory = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.altConfigFileBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.altConfigFile = new SystemsAnalysis.Modeling.Alternatives.AlternativeConfiguration();
            this.btnCopyAlternative = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
            this.btnDeleteAlternative = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.btnNewAlternative = new Infragistics.Win.Misc.UltraButton();
            this.txtDefaultNodeName = new System.Windows.Forms.TextBox();
            this.btnEditAlternative = new Infragistics.Win.Misc.UltraButton();
            this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.cmbOutputModel = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.btnBrowseOutput = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
            this.btnApplyAlternative = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.btnLoadDebugSettings = new Infragistics.Win.Misc.UltraButton();
            this.lblCurrentConfigFile = new Infragistics.Win.Misc.UltraLabel();
            this.btnRestoreDefault = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.txtDebugMode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ultraTabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.btnEditAutoConveyance = new Infragistics.Win.Misc.UltraButton();
            this.btnGenerateConveyance = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
            this.autoConveyanceInterface1 = new SystemsAnalysis.ModelConstruction.AlternativesToolkit.AutoConveyanceInterface();
            this.btnCancel = new Infragistics.Win.Misc.UltraButton();
            this.alternativeExplorerBar = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.tabControl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.altEngineConfiguration = new SystemsAnalysis.ModelConstruction.AlternativesToolkit.AltEngineConfiguration();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlternativeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBaseModel)).BeginInit();
            this.ultraTabPageControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAlternativeHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.altConfigFileBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.altConfigFile)).BeginInit();
            this.ultraTabPageControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOutputModel)).BeginInit();
            this.ultraTabPageControl4.SuspendLayout();
            this.ultraTabPageControl5.SuspendLayout();
            this.ultraTabPageControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alternativeExplorerBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.ultraTabSharedControlsPage1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.altEngineConfiguration)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraLabel2
            // 
            appearance19.TextVAlignAsString = "Bottom";
            this.ultraLabel2.Appearance = appearance19;
            this.ultraLabel2.Location = new System.Drawing.Point(20, 78);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(159, 17);
            this.ultraLabel2.TabIndex = 31;
            this.ultraLabel2.Text = "Alternative Name:";
            // 
            // cmbAlternativeList
            // 
            this.cmbAlternativeList.DisplayMember = "";
            this.cmbAlternativeList.Location = new System.Drawing.Point(20, 97);
            this.cmbAlternativeList.Name = "cmbAlternativeList";
            this.cmbAlternativeList.Size = new System.Drawing.Size(204, 21);
            this.cmbAlternativeList.SyncWithCurrencyManager = false;
            this.cmbAlternativeList.TabIndex = 2;
            this.cmbAlternativeList.ValueMember = "";
            this.cmbAlternativeList.SelectionChanged += new System.EventHandler(this.cmbAlternativeList_SelectionChanged);
            // 
            // ultraLabel1
            // 
            appearance20.TextVAlignAsString = "Bottom";
            this.ultraLabel1.Appearance = appearance20;
            this.ultraLabel1.Location = new System.Drawing.Point(20, 34);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(73, 17);
            this.ultraLabel1.TabIndex = 26;
            this.ultraLabel1.Text = "Base Model:";
            // 
            // cmbBaseModel
            // 
            this.cmbBaseModel.DisplayMember = "BaseModel_text";
            this.cmbBaseModel.Location = new System.Drawing.Point(20, 52);
            this.cmbBaseModel.Name = "cmbBaseModel";
            this.cmbBaseModel.Size = new System.Drawing.Size(504, 21);
            this.cmbBaseModel.SyncWithCurrencyManager = false;
            this.cmbBaseModel.TabIndex = 0;
            this.cmbBaseModel.ValueMember = "BaseModel_text";
            this.cmbBaseModel.ValueChanged += new System.EventHandler(this.cmbBaseModel_ValueChanged);
            this.cmbBaseModel.SelectionChanged += new System.EventHandler(this.cmbBaseModel_SelectedIndexChanged);
            // 
            // btnBrowseBaseModel
            // 
            this.btnBrowseBaseModel.Location = new System.Drawing.Point(529, 52);
            this.btnBrowseBaseModel.Name = "btnBrowseBaseModel";
            this.btnBrowseBaseModel.Size = new System.Drawing.Size(64, 22);
            this.btnBrowseBaseModel.TabIndex = 1;
            this.btnBrowseBaseModel.Text = "Browse...";
            this.btnBrowseBaseModel.Click += new System.EventHandler(this.btnBrowseBaseModel_Click);
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.engineOutput);
            this.ultraTabPageControl1.Controls.Add(this.panel2);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(-8333, -8667);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(923, 615);
            // 
            // engineOutput
            // 
            this.engineOutput.BackColor = System.Drawing.Color.White;
            this.engineOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.engineOutput.Location = new System.Drawing.Point(0, 44);
            this.engineOutput.Name = "engineOutput";
            this.engineOutput.ReadOnly = true;
            this.engineOutput.Size = new System.Drawing.Size(923, 571);
            this.engineOutput.TabIndex = 0;
            this.engineOutput.Text = "";
            this.engineOutput.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.engineOutput_LinkClicked);
            this.engineOutput.TextChanged += new System.EventHandler(this.engineOutput_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ultraLabel7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(923, 44);
            this.panel2.TabIndex = 2;
            // 
            // ultraLabel7
            // 
            this.ultraLabel7.Font = new System.Drawing.Font("Lucida Console", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel7.Location = new System.Drawing.Point(2, 10);
            this.ultraLabel7.Name = "ultraLabel7";
            this.ultraLabel7.Size = new System.Drawing.Size(426, 27);
            this.ultraLabel7.TabIndex = 22;
            this.ultraLabel7.Text = "Status Log";
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel13);
            this.ultraTabPageControl2.Controls.Add(this.grdAlternativeHistory);
            this.ultraTabPageControl2.Controls.Add(this.btnCopyAlternative);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel12);
            this.ultraTabPageControl2.Controls.Add(this.btnDeleteAlternative);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel3);
            this.ultraTabPageControl2.Controls.Add(this.btnNewAlternative);
            this.ultraTabPageControl2.Controls.Add(this.txtDefaultNodeName);
            this.ultraTabPageControl2.Controls.Add(this.btnEditAlternative);
            this.ultraTabPageControl2.Controls.Add(this.cmbAlternativeList);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel2);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel1);
            this.ultraTabPageControl2.Controls.Add(this.cmbBaseModel);
            this.ultraTabPageControl2.Controls.Add(this.btnBrowseBaseModel);
            this.ultraTabPageControl2.Controls.Add(this.btnCancel);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(0, 0);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(923, 615);
            // 
            // ultraLabel13
            // 
            appearance21.TextVAlignAsString = "Bottom";
            this.ultraLabel13.Appearance = appearance21;
            this.ultraLabel13.Location = new System.Drawing.Point(20, 128);
            this.ultraLabel13.Name = "ultraLabel13";
            this.ultraLabel13.Size = new System.Drawing.Size(159, 18);
            this.ultraLabel13.TabIndex = 43;
            this.ultraLabel13.Text = "History:";
            // 
            // grdAlternativeHistory
            // 
            this.grdAlternativeHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAlternativeHistory.DataMember = "History";
            this.grdAlternativeHistory.DataSource = this.altConfigFileBindingSource;
            appearance22.BackColor = System.Drawing.SystemColors.Window;
            appearance22.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grdAlternativeHistory.DisplayLayout.Appearance = appearance22;
            this.grdAlternativeHistory.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 152;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 345;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3});
            this.grdAlternativeHistory.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdAlternativeHistory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdAlternativeHistory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance23.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance23.BorderColor = System.Drawing.SystemColors.Window;
            this.grdAlternativeHistory.DisplayLayout.GroupByBox.Appearance = appearance23;
            appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdAlternativeHistory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance24;
            this.grdAlternativeHistory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance25.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance25.BackColor2 = System.Drawing.SystemColors.Control;
            appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance25.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdAlternativeHistory.DisplayLayout.GroupByBox.PromptAppearance = appearance25;
            this.grdAlternativeHistory.DisplayLayout.MaxColScrollRegions = 1;
            this.grdAlternativeHistory.DisplayLayout.MaxRowScrollRegions = 1;
            appearance26.BackColor = System.Drawing.SystemColors.Window;
            appearance26.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdAlternativeHistory.DisplayLayout.Override.ActiveCellAppearance = appearance26;
            appearance27.BackColor = System.Drawing.SystemColors.Highlight;
            appearance27.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdAlternativeHistory.DisplayLayout.Override.ActiveRowAppearance = appearance27;
            this.grdAlternativeHistory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdAlternativeHistory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance28.BackColor = System.Drawing.SystemColors.Window;
            this.grdAlternativeHistory.DisplayLayout.Override.CardAreaAppearance = appearance28;
            appearance29.BorderColor = System.Drawing.Color.Silver;
            appearance29.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grdAlternativeHistory.DisplayLayout.Override.CellAppearance = appearance29;
            this.grdAlternativeHistory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdAlternativeHistory.DisplayLayout.Override.CellPadding = 0;
            appearance30.BackColor = System.Drawing.SystemColors.Control;
            appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance30.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance30.BorderColor = System.Drawing.SystemColors.Window;
            this.grdAlternativeHistory.DisplayLayout.Override.GroupByRowAppearance = appearance30;
            appearance31.TextHAlignAsString = "Left";
            this.grdAlternativeHistory.DisplayLayout.Override.HeaderAppearance = appearance31;
            this.grdAlternativeHistory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdAlternativeHistory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance32.BackColor = System.Drawing.SystemColors.Window;
            appearance32.BorderColor = System.Drawing.Color.Silver;
            this.grdAlternativeHistory.DisplayLayout.Override.RowAppearance = appearance32;
            this.grdAlternativeHistory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance33.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdAlternativeHistory.DisplayLayout.Override.TemplateAddRowAppearance = appearance33;
            this.grdAlternativeHistory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdAlternativeHistory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdAlternativeHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdAlternativeHistory.Location = new System.Drawing.Point(20, 151);
            this.grdAlternativeHistory.Name = "grdAlternativeHistory";
            this.grdAlternativeHistory.Size = new System.Drawing.Size(893, 426);
            this.grdAlternativeHistory.TabIndex = 0;
            this.grdAlternativeHistory.Text = "ultraGrid2";
            // 
            // altConfigFileBindingSource
            // 
            this.altConfigFileBindingSource.DataSource = this.altConfigFile;
            this.altConfigFileBindingSource.Position = 0;
            // 
            // altConfigFile
            // 
            this.altConfigFile.DataSetName = "AltConfigFile";
            this.altConfigFile.Locale = new System.Globalization.CultureInfo("");
            this.altConfigFile.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnCopyAlternative
            // 
            this.btnCopyAlternative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyAlternative.Location = new System.Drawing.Point(691, 582);
            this.btnCopyAlternative.Name = "btnCopyAlternative";
            this.btnCopyAlternative.Size = new System.Drawing.Size(75, 25);
            this.btnCopyAlternative.TabIndex = 3;
            this.btnCopyAlternative.Text = "Copy";
            this.btnCopyAlternative.Click += new System.EventHandler(this.btnCopyAlternative_Click);
            // 
            // ultraLabel12
            // 
            appearance34.TextVAlignAsString = "Bottom";
            this.ultraLabel12.Appearance = appearance34;
            this.ultraLabel12.Location = new System.Drawing.Point(229, 78);
            this.ultraLabel12.Name = "ultraLabel12";
            this.ultraLabel12.Size = new System.Drawing.Size(158, 16);
            this.ultraLabel12.TabIndex = 41;
            this.ultraLabel12.Text = "Default Node Name:";
            // 
            // btnDeleteAlternative
            // 
            this.btnDeleteAlternative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAlternative.Location = new System.Drawing.Point(771, 582);
            this.btnDeleteAlternative.Name = "btnDeleteAlternative";
            this.btnDeleteAlternative.Size = new System.Drawing.Size(75, 25);
            this.btnDeleteAlternative.TabIndex = 4;
            this.btnDeleteAlternative.Text = "Delete";
            this.btnDeleteAlternative.Click += new System.EventHandler(this.btnDeleteAlternative_Click);
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.Font = new System.Drawing.Font("Lucida Console", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel3.Location = new System.Drawing.Point(5, 10);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(426, 27);
            this.ultraLabel3.TabIndex = 21;
            this.ultraLabel3.Text = "Manage Alternatives";
            // 
            // btnNewAlternative
            // 
            this.btnNewAlternative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewAlternative.Location = new System.Drawing.Point(531, 582);
            this.btnNewAlternative.Name = "btnNewAlternative";
            this.btnNewAlternative.Size = new System.Drawing.Size(75, 25);
            this.btnNewAlternative.TabIndex = 1;
            this.btnNewAlternative.Text = "Create New";
            this.btnNewAlternative.Click += new System.EventHandler(this.btnNewAlternative_Click);
            // 
            // txtDefaultNodeName
            // 
            this.txtDefaultNodeName.Location = new System.Drawing.Point(229, 97);
            this.txtDefaultNodeName.MaxLength = 3;
            this.txtDefaultNodeName.Name = "txtDefaultNodeName";
            this.txtDefaultNodeName.Size = new System.Drawing.Size(73, 20);
            this.txtDefaultNodeName.TabIndex = 3;
            this.txtDefaultNodeName.Text = "ALT";
            // 
            // btnEditAlternative
            // 
            this.btnEditAlternative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditAlternative.Location = new System.Drawing.Point(611, 582);
            this.btnEditAlternative.Name = "btnEditAlternative";
            this.btnEditAlternative.Size = new System.Drawing.Size(75, 25);
            this.btnEditAlternative.TabIndex = 2;
            this.btnEditAlternative.Text = "Edit Existing";
            this.btnEditAlternative.Click += new System.EventHandler(this.btnEditAlternative_Click);
            // 
            // ultraTabPageControl3
            // 
            this.ultraTabPageControl3.Controls.Add(this.cmbOutputModel);
            this.ultraTabPageControl3.Controls.Add(this.btnBrowseOutput);
            this.ultraTabPageControl3.Controls.Add(this.ultraLabel8);
            this.ultraTabPageControl3.Controls.Add(this.btnApplyAlternative);
            this.ultraTabPageControl3.Controls.Add(this.ultraLabel4);
            this.ultraTabPageControl3.Location = new System.Drawing.Point(-8333, -8667);
            this.ultraTabPageControl3.Name = "ultraTabPageControl3";
            this.ultraTabPageControl3.Size = new System.Drawing.Size(923, 615);
            // 
            // cmbOutputModel
            // 
            this.cmbOutputModel.DisplayMember = "OutputModel_text";
            this.cmbOutputModel.Location = new System.Drawing.Point(20, 144);
            this.cmbOutputModel.Name = "cmbOutputModel";
            this.cmbOutputModel.Size = new System.Drawing.Size(503, 21);
            this.cmbOutputModel.SyncWithCurrencyManager = false;
            this.cmbOutputModel.TabIndex = 0;
            this.cmbOutputModel.ValueMember = "OutputModel_text";
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Location = new System.Drawing.Point(528, 144);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(64, 21);
            this.btnBrowseOutput.TabIndex = 1;
            this.btnBrowseOutput.Text = "Browse...";
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // ultraLabel8
            // 
            appearance35.TextVAlignAsString = "Bottom";
            this.ultraLabel8.Appearance = appearance35;
            this.ultraLabel8.Location = new System.Drawing.Point(20, 123);
            this.ultraLabel8.Name = "ultraLabel8";
            this.ultraLabel8.Size = new System.Drawing.Size(96, 16);
            this.ultraLabel8.TabIndex = 29;
            this.ultraLabel8.Text = "Output Directory:";
            // 
            // btnApplyAlternative
            // 
            this.btnApplyAlternative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyAlternative.Location = new System.Drawing.Point(745, 582);
            this.btnApplyAlternative.Name = "btnApplyAlternative";
            this.btnApplyAlternative.Size = new System.Drawing.Size(99, 24);
            this.btnApplyAlternative.TabIndex = 5;
            this.btnApplyAlternative.Text = "Apply Alternative";
            this.btnApplyAlternative.Click += new System.EventHandler(this.btnApplyAlternative_Click);
            // 
            // ultraLabel4
            // 
            this.ultraLabel4.Font = new System.Drawing.Font("Lucida Console", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel4.Location = new System.Drawing.Point(2, 10);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(421, 27);
            this.ultraLabel4.TabIndex = 22;
            this.ultraLabel4.Text = "Apply Alternative to Model";
            // 
            // ultraTabPageControl4
            // 
            this.ultraTabPageControl4.Controls.Add(this.ultraLabel5);
            this.ultraTabPageControl4.Location = new System.Drawing.Point(-8333, -8667);
            this.ultraTabPageControl4.Name = "ultraTabPageControl4";
            this.ultraTabPageControl4.Size = new System.Drawing.Size(923, 615);
            // 
            // ultraLabel5
            // 
            this.ultraLabel5.Font = new System.Drawing.Font("Lucida Console", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel5.Location = new System.Drawing.Point(5, 10);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(421, 27);
            this.ultraLabel5.TabIndex = 23;
            this.ultraLabel5.Text = "QC Alternative";
            // 
            // ultraTabPageControl5
            // 
            this.ultraTabPageControl5.Controls.Add(this.btnLoadDebugSettings);
            this.ultraTabPageControl5.Controls.Add(this.lblCurrentConfigFile);
            this.ultraTabPageControl5.Controls.Add(this.btnRestoreDefault);
            this.ultraTabPageControl5.Controls.Add(this.ultraLabel6);
            this.ultraTabPageControl5.Controls.Add(this.txtDebugMode);
            this.ultraTabPageControl5.Location = new System.Drawing.Point(-8333, -8667);
            this.ultraTabPageControl5.Name = "ultraTabPageControl5";
            this.ultraTabPageControl5.Size = new System.Drawing.Size(923, 615);
            // 
            // btnLoadDebugSettings
            // 
            this.btnLoadDebugSettings.Location = new System.Drawing.Point(156, 68);
            this.btnLoadDebugSettings.Name = "btnLoadDebugSettings";
            this.btnLoadDebugSettings.Size = new System.Drawing.Size(138, 20);
            this.btnLoadDebugSettings.TabIndex = 2;
            this.btnLoadDebugSettings.Text = "Load Debug Settings";
            this.btnLoadDebugSettings.Click += new System.EventHandler(this.btnLoadDebugSettings_Click);
            // 
            // lblCurrentConfigFile
            // 
            this.lblCurrentConfigFile.Location = new System.Drawing.Point(12, 94);
            this.lblCurrentConfigFile.Name = "lblCurrentConfigFile";
            this.lblCurrentConfigFile.Size = new System.Drawing.Size(611, 65);
            this.lblCurrentConfigFile.TabIndex = 27;
            this.lblCurrentConfigFile.Text = "Current Settings File:";
            // 
            // btnRestoreDefault
            // 
            this.btnRestoreDefault.Location = new System.Drawing.Point(12, 68);
            this.btnRestoreDefault.Name = "btnRestoreDefault";
            this.btnRestoreDefault.Size = new System.Drawing.Size(139, 20);
            this.btnRestoreDefault.TabIndex = 1;
            this.btnRestoreDefault.Text = "Restore Default Settings";
            this.btnRestoreDefault.Click += new System.EventHandler(this.btnRestoreDefault_Click);
            // 
            // ultraLabel6
            // 
            this.ultraLabel6.Font = new System.Drawing.Font("Lucida Console", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel6.Location = new System.Drawing.Point(5, 10);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(421, 27);
            this.ultraLabel6.TabIndex = 23;
            this.ultraLabel6.Text = "Alternative Toolkit Settings";
            // 
            // txtDebugMode
            // 
            this.txtDebugMode.Location = new System.Drawing.Point(12, 42);
            this.txtDebugMode.Name = "txtDebugMode";
            this.txtDebugMode.Size = new System.Drawing.Size(239, 21);
            this.txtDebugMode.TabIndex = 0;
            this.txtDebugMode.Text = "Manually Execute MapBasic Code?";
            this.txtDebugMode.CheckedChanged += new System.EventHandler(this.txtDebugMode_CheckedChanged);
            // 
            // ultraTabPageControl6
            // 
            this.ultraTabPageControl6.Controls.Add(this.btnEditAutoConveyance);
            this.ultraTabPageControl6.Controls.Add(this.btnGenerateConveyance);
            this.ultraTabPageControl6.Controls.Add(this.ultraLabel10);
            this.ultraTabPageControl6.Controls.Add(this.autoConveyanceInterface1);
            this.ultraTabPageControl6.Location = new System.Drawing.Point(-8333, -8667);
            this.ultraTabPageControl6.Name = "ultraTabPageControl6";
            this.ultraTabPageControl6.Size = new System.Drawing.Size(923, 615);
            // 
            // btnEditAutoConveyance
            // 
            this.btnEditAutoConveyance.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEditAutoConveyance.Location = new System.Drawing.Point(229, 97);
            this.btnEditAutoConveyance.Name = "btnEditAutoConveyance";
            this.btnEditAutoConveyance.Size = new System.Drawing.Size(190, 22);
            this.btnEditAutoConveyance.TabIndex = 3;
            this.btnEditAutoConveyance.Text = "Prepare Conveyance Alternative";
            this.btnEditAutoConveyance.Click += new System.EventHandler(this.btnEditAutoConveyance_Click);
            // 
            // btnGenerateConveyance
            // 
            this.btnGenerateConveyance.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnGenerateConveyance.Enabled = false;
            this.btnGenerateConveyance.Location = new System.Drawing.Point(424, 97);
            this.btnGenerateConveyance.Name = "btnGenerateConveyance";
            this.btnGenerateConveyance.Size = new System.Drawing.Size(132, 22);
            this.btnGenerateConveyance.TabIndex = 4;
            this.btnGenerateConveyance.Text = "Generate Alternative";
            this.btnGenerateConveyance.Click += new System.EventHandler(this.btnGenerateConveyance_Click);
            // 
            // ultraLabel10
            // 
            this.ultraLabel10.Font = new System.Drawing.Font("Lucida Console", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel10.Location = new System.Drawing.Point(5, 10);
            this.ultraLabel10.Name = "ultraLabel10";
            this.ultraLabel10.Size = new System.Drawing.Size(426, 27);
            this.ultraLabel10.TabIndex = 7;
            this.ultraLabel10.Text = "Auto-Generate Conveyance Alternative";
            // 
            // autoConveyanceInterface1
            // 
            this.autoConveyanceInterface1.AlternativeName = null;
            this.autoConveyanceInterface1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.autoConveyanceInterface1.BackColor = System.Drawing.Color.Transparent;
            this.autoConveyanceInterface1.BaseModel = "\\";
            this.autoConveyanceInterface1.Location = new System.Drawing.Point(17, 123);
            this.autoConveyanceInterface1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.autoConveyanceInterface1.Name = "autoConveyanceInterface1";
            this.autoConveyanceInterface1.Size = new System.Drawing.Size(898, 492);
            this.autoConveyanceInterface1.TabIndex = 5;
            this.autoConveyanceInterface1.Load += new System.EventHandler(this.autoConveyanceInterface1_Load);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(850, 582);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            // 
            // alternativeExplorerBar
            // 
            this.alternativeExplorerBar.Dock = System.Windows.Forms.DockStyle.Left;
            ultraExplorerBarItem1.Key = "ManageAlternatives";
            ultraExplorerBarItem1.Settings.UseDefaultImage = Infragistics.Win.DefaultableBoolean.False;
            ultraExplorerBarItem1.Text = "Manage Alternatives";
            ultraExplorerBarItem2.Key = "ApplyAlternative";
            ultraExplorerBarItem2.Text = "Apply Alternative";
            ultraExplorerBarItem2.ToolTipText = "Create a new model based on an alternative";
            ultraExplorerBarItem3.Key = "AlternativeQC";
            ultraExplorerBarItem3.Settings.Enabled = Infragistics.Win.DefaultableBoolean.False;
            ultraExplorerBarItem3.Text = "Create QC Report";
            ultraExplorerBarItem3.ToolTipText = "Perform Alternative QAQC";
            ultraExplorerBarItem3.Visible = false;
            ultraExplorerBarItem4.Key = "AutoConveyance";
            ultraExplorerBarItem4.Text = "Conveyance Alternative";
            ultraExplorerBarItem4.ToolTipText = "Automatically Generate a Conveyance Alternative";
            ultraExplorerBarGroup1.Items.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem[] {
            ultraExplorerBarItem1,
            ultraExplorerBarItem2,
            ultraExplorerBarItem3,
            ultraExplorerBarItem4});
            ultraExplorerBarGroup1.Settings.ContainerHeight = 19;
            ultraExplorerBarGroup1.Text = "Tools";
            ultraExplorerBarItem5.Key = "EngineStatus";
            ultraExplorerBarItem5.Text = "Status Log";
            ultraExplorerBarItem6.Checked = true;
            ultraExplorerBarItem6.Key = "ModifySettings";
            ultraExplorerBarItem6.Text = "Settings";
            ultraExplorerBarItem7.Key = "Exit";
            ultraExplorerBarItem7.Text = "Exit";
            ultraExplorerBarGroup2.Items.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem[] {
            ultraExplorerBarItem5,
            ultraExplorerBarItem6,
            ultraExplorerBarItem7});
            ultraExplorerBarGroup2.Text = "Application";
            this.alternativeExplorerBar.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2});
            this.alternativeExplorerBar.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.SmallImagesWithText;
            this.alternativeExplorerBar.ItemSettings.UseDefaultImage = Infragistics.Win.DefaultableBoolean.False;
            this.alternativeExplorerBar.Location = new System.Drawing.Point(0, 24);
            this.alternativeExplorerBar.Name = "alternativeExplorerBar";
            this.alternativeExplorerBar.Size = new System.Drawing.Size(171, 615);
            this.alternativeExplorerBar.TabIndex = 2;
            this.alternativeExplorerBar.ViewStyle = Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarViewStyle.VisualStudio2005;
            this.alternativeExplorerBar.ItemClick += new Infragistics.Win.UltraWinExplorerBar.ItemClickEventHandler(this.alternativeExplorerBar_ItemClick);
            // 
            // tabControl
            // 
            appearance36.BackColor = System.Drawing.SystemColors.Control;
            appearance36.BackColor2 = System.Drawing.SystemColors.Control;
            appearance36.ForeColor = System.Drawing.SystemColors.Control;
            this.tabControl.Appearance = appearance36;
            this.tabControl.Controls.Add(this.ultraTabSharedControlsPage1);
            this.tabControl.Controls.Add(this.ultraTabPageControl2);
            this.tabControl.Controls.Add(this.ultraTabPageControl1);
            this.tabControl.Controls.Add(this.ultraTabPageControl3);
            this.tabControl.Controls.Add(this.ultraTabPageControl4);
            this.tabControl.Controls.Add(this.ultraTabPageControl5);
            this.tabControl.Controls.Add(this.ultraTabPageControl6);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(171, 24);
            this.tabControl.Name = "tabControl";
            this.tabControl.SharedControls.AddRange(new System.Windows.Forms.Control[] {
            this.cmbAlternativeList,
            this.ultraLabel2,
            this.ultraLabel1,
            this.cmbBaseModel,
            this.btnBrowseBaseModel,
            this.btnCancel});
            this.tabControl.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.tabControl.Size = new System.Drawing.Size(923, 615);
            this.tabControl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
            this.tabControl.TabIndex = 0;
            ultraTab7.ExcludedSharedControls.AddRange(new System.Windows.Forms.Control[] {
            this.ultraLabel2,
            this.cmbAlternativeList,
            this.ultraLabel1,
            this.cmbBaseModel,
            this.btnBrowseBaseModel});
            ultraTab7.Key = "EngineStatus";
            ultraTab7.TabPage = this.ultraTabPageControl1;
            ultraTab7.Text = "tab1";
            ultraTab8.Key = "ManageAlternatives";
            ultraTab8.TabPage = this.ultraTabPageControl2;
            ultraTab8.Text = "tab2";
            ultraTab9.Key = "ApplyAlternative";
            ultraTab9.TabPage = this.ultraTabPageControl3;
            ultraTab9.Text = "tab3";
            ultraTab10.Key = "AlternativeQC";
            ultraTab10.TabPage = this.ultraTabPageControl4;
            ultraTab10.Text = "tab4";
            ultraTab11.ExcludedSharedControls.AddRange(new System.Windows.Forms.Control[] {
            this.ultraLabel2,
            this.cmbAlternativeList,
            this.ultraLabel1,
            this.cmbBaseModel,
            this.btnBrowseBaseModel});
            ultraTab11.Key = "ModifySettings";
            ultraTab11.TabPage = this.ultraTabPageControl5;
            ultraTab11.Text = "tab5";
            ultraTab12.Key = "AutoConveyance";
            ultraTab12.TabPage = this.ultraTabPageControl6;
            ultraTab12.Text = "tab6";
            this.tabControl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab7,
            ultraTab8,
            ultraTab9,
            ultraTab10,
            ultraTab11,
            ultraTab12});
            this.tabControl.SelectedTabChanging += new Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventHandler(this.tabControl_SelectedTabChanging);
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Controls.Add(this.cmbAlternativeList);
            this.ultraTabSharedControlsPage1.Controls.Add(this.ultraLabel2);
            this.ultraTabSharedControlsPage1.Controls.Add(this.ultraLabel1);
            this.ultraTabSharedControlsPage1.Controls.Add(this.cmbBaseModel);
            this.ultraTabSharedControlsPage1.Controls.Add(this.btnBrowseBaseModel);
            this.ultraTabSharedControlsPage1.Controls.Add(this.btnCancel);
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(923, 615);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 639);
            this.statusBar.Name = "statusBar";
            ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
            this.statusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[] {
            ultraStatusPanel2});
            this.statusBar.Size = new System.Drawing.Size(1094, 20);
            this.statusBar.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1094, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.redoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.cutToolStripMenuItem.Text = "Cu&t";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.out";
            this.openFileDialog1.Filter = "*.out|*.out";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // altEngineConfiguration
            // 
            this.altEngineConfiguration.DataSetName = "AltEngineConfiguration";
            this.altEngineConfiguration.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AlternativeEngineForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1094, 659);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.alternativeExplorerBar);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AlternativeEngineForm";
            this.Text = "Alternatives Toolkit";
            ((System.ComponentModel.ISupportInitialize)(this.cmbAlternativeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBaseModel)).EndInit();
            this.ultraTabPageControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ultraTabPageControl2.ResumeLayout(false);
            this.ultraTabPageControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAlternativeHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.altConfigFileBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.altConfigFile)).EndInit();
            this.ultraTabPageControl3.ResumeLayout(false);
            this.ultraTabPageControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOutputModel)).EndInit();
            this.ultraTabPageControl4.ResumeLayout(false);
            this.ultraTabPageControl5.ResumeLayout(false);
            this.ultraTabPageControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.alternativeExplorerBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ultraTabSharedControlsPage1.ResumeLayout(false);
            this.ultraTabSharedControlsPage1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.altEngineConfiguration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinStatusBar.UltraStatusBar statusBar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl tabControl;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private Infragistics.Win.Misc.UltraButton btnCopyAlternative;
        private Infragistics.Win.Misc.UltraLabel ultraLabel12;
        private Infragistics.Win.Misc.UltraButton btnDeleteAlternative;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.Misc.UltraButton btnNewAlternative;
        private System.Windows.Forms.TextBox txtDefaultNodeName;
        private Infragistics.Win.Misc.UltraButton btnEditAlternative;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private SystemsAnalysis.Utils.StatusTextBox.StatusTextBox engineOutput;
        private System.Windows.Forms.Panel panel2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel7;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl3;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cmbOutputModel;
        private Infragistics.Win.Misc.UltraButton btnBrowseOutput;
        private Infragistics.Win.Misc.UltraLabel ultraLabel8;
        private Infragistics.Win.Misc.UltraButton btnApplyAlternative;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl5;
        private Infragistics.Win.Misc.UltraButton btnRestoreDefault;
        private Infragistics.Win.Misc.UltraLabel ultraLabel6;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor txtDebugMode;
        
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar alternativeExplorerBar;
        private AltEngineConfiguration altEngineConfiguration;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Infragistics.Win.Misc.UltraLabel lblCurrentConfigFile;
        private Infragistics.Win.Misc.UltraButton btnLoadDebugSettings;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdAlternativeHistory;
        private SystemsAnalysis.Modeling.Alternatives.AlternativeConfiguration altConfigFile;
        private System.Windows.Forms.BindingSource altConfigFileBindingSource;
        private Infragistics.Win.Misc.UltraLabel ultraLabel13;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl6;
        private Infragistics.Win.Misc.UltraButton btnGenerateConveyance;
        private Infragistics.Win.Misc.UltraLabel ultraLabel10;        
        private AutoConveyanceInterface autoConveyanceInterface1;
        private Infragistics.Win.Misc.UltraButton btnEditAutoConveyance;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cmbAlternativeList;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cmbBaseModel;
        private Infragistics.Win.Misc.UltraButton btnBrowseBaseModel;
        private Infragistics.Win.Misc.UltraButton btnCancel;
    }
}