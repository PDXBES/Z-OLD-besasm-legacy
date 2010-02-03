namespace SystemsAnalysis.Reporting
{
    partial class CreateReportForm
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
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateReportForm));
					Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
					Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("reportTable1", -1);
					Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReportName");
					Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReportPath");
					Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
					Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
					Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
					Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
					Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
					Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
					Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
					this.tabMapView = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
					this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
					this.pnlMapControls = new System.Windows.Forms.Panel();
					this.btnSelectStopLink = new Infragistics.Win.Misc.UltraButton();
					this.btnSelectStartLink = new Infragistics.Win.Misc.UltraButton();
					this.btnPan = new Infragistics.Win.Misc.UltraButton();
					this.btnZoomOut = new Infragistics.Win.Misc.UltraButton();
					this.btnZoomIn = new Infragistics.Win.Misc.UltraButton();
					this.tabCharacterizationView = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
					this.pnlCancelBackgroundWorker = new System.Windows.Forms.Panel();
					this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
					this.btnCancel = new Infragistics.Win.Misc.UltraButton();
					this.axWebBrowser = new AxSHDocVw.AxWebBrowser();
					this.panel2 = new System.Windows.Forms.Panel();
					this.btnCharacterize = new Infragistics.Win.Misc.UltraButton();
					this.grpBoxReportBase = new Infragistics.Win.Misc.UltraExpandableGroupBox();
					this.grpBoxReportBasePanel = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
					this.reportOptionsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
					this.lblIncludedLibrary = new Infragistics.Win.Misc.UltraLabel();
					this.lblIncludedLibraryName = new Infragistics.Win.Misc.UltraLabel();
					this.lblAuxilaryData = new Infragistics.Win.Misc.UltraLabel();
					this.txtAuxilaryDataValue = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
					this.btnBrowseAuxilaryData = new Infragistics.Win.Misc.UltraButton();
					this.panel1 = new System.Windows.Forms.Panel();
					this.btnImportReport = new Infragistics.Win.Misc.UltraButton();
					this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
					this.cmbReportChooser = new Infragistics.Win.UltraWinGrid.UltraCombo();
					this.reportTable = new System.Data.DataSet();
					this.reportTable1 = new System.Data.DataTable();
					this.dataColumn1 = new System.Data.DataColumn();
					this.dataColumn2 = new System.Data.DataColumn();
					this.tabModelCatalog = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
					this.grpboxStartLinks = new Infragistics.Win.Misc.UltraGroupBox();
					this.panel3 = new System.Windows.Forms.Panel();
					this.btnRemoveStartLink = new Infragistics.Win.Misc.UltraButton();
					this.btnAddStartLink = new Infragistics.Win.Misc.UltraButton();
					this.lstStartLinks = new System.Windows.Forms.ListBox();
					this.grpboxStopLinks = new Infragistics.Win.Misc.UltraGroupBox();
					this.panel4 = new System.Windows.Forms.Panel();
					this.btnRemoveStopLink = new Infragistics.Win.Misc.UltraButton();
					this.btnAddStopLink = new Infragistics.Win.Misc.UltraButton();
					this.lstStopLinks = new System.Windows.Forms.ListBox();
					this.btnPreviewTrace = new Infragistics.Win.Misc.UltraButton();
					this.btnClearList = new Infragistics.Win.Misc.UltraButton();
					this.btnReconcileModel = new System.Windows.Forms.Button();
					this.btnImportFromModel = new Infragistics.Win.Misc.UltraButton();
					this.btnImportFromIni = new Infragistics.Win.Misc.UltraButton();
					this.tabInteractiveSelect = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
					this.tabPolygonSelect = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
					this.tabControlOutputDisplay = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
					this.tabOutputDisplaySharedControlsPage = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
					this.txtStudyAreaName = new System.Windows.Forms.TextBox();
					this.btnSelectCharDir = new System.Windows.Forms.Button();
					this.pnlCharacterizationView = new System.Windows.Forms.Panel();
					this.splitContainer1 = new System.Windows.Forms.SplitContainer();
					this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
					this.lblStudyArea = new Infragistics.Win.Misc.UltraLabel();
					this.tabControlStudyArea = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
					this.tabStudyAreaSharedControlsPage = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
					this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
					this.ofd = new System.Windows.Forms.OpenFileDialog();
					this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
					this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
					this.tabMapView.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
					this.pnlMapControls.SuspendLayout();
					this.tabCharacterizationView.SuspendLayout();
					this.pnlCancelBackgroundWorker.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).BeginInit();
					this.panel2.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.grpBoxReportBase)).BeginInit();
					this.grpBoxReportBase.SuspendLayout();
					this.grpBoxReportBasePanel.SuspendLayout();
					this.reportOptionsLayoutPanel.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.txtAuxilaryDataValue)).BeginInit();
					this.panel1.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.cmbReportChooser)).BeginInit();
					((System.ComponentModel.ISupportInitialize)(this.reportTable)).BeginInit();
					((System.ComponentModel.ISupportInitialize)(this.reportTable1)).BeginInit();
					this.tabModelCatalog.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.grpboxStartLinks)).BeginInit();
					this.grpboxStartLinks.SuspendLayout();
					this.panel3.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.grpboxStopLinks)).BeginInit();
					this.grpboxStopLinks.SuspendLayout();
					this.panel4.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.tabControlOutputDisplay)).BeginInit();
					this.tabControlOutputDisplay.SuspendLayout();
					this.pnlCharacterizationView.SuspendLayout();
					this.splitContainer1.Panel1.SuspendLayout();
					this.splitContainer1.Panel2.SuspendLayout();
					this.splitContainer1.SuspendLayout();
					((System.ComponentModel.ISupportInitialize)(this.tabControlStudyArea)).BeginInit();
					this.tabControlStudyArea.SuspendLayout();
					this.SuspendLayout();
					// 
					// tabMapView
					// 
					this.tabMapView.Controls.Add(this.axMapControl);
					this.tabMapView.Controls.Add(this.pnlMapControls);
					this.tabMapView.Location = new System.Drawing.Point(-10000, -10000);
					this.tabMapView.Margin = new System.Windows.Forms.Padding(2);
					this.tabMapView.Name = "tabMapView";
					this.tabMapView.Size = new System.Drawing.Size(216, 452);
					// 
					// axMapControl
					// 
					this.axMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
					this.axMapControl.Location = new System.Drawing.Point(0, 0);
					this.axMapControl.Margin = new System.Windows.Forms.Padding(2);
					this.axMapControl.Name = "axMapControl";
					this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
					this.axMapControl.Size = new System.Drawing.Size(216, 415);
					this.axMapControl.TabIndex = 4;
					// 
					// pnlMapControls
					// 
					this.pnlMapControls.Controls.Add(this.btnSelectStopLink);
					this.pnlMapControls.Controls.Add(this.btnSelectStartLink);
					this.pnlMapControls.Controls.Add(this.btnPan);
					this.pnlMapControls.Controls.Add(this.btnZoomOut);
					this.pnlMapControls.Controls.Add(this.btnZoomIn);
					this.pnlMapControls.Dock = System.Windows.Forms.DockStyle.Bottom;
					this.pnlMapControls.Location = new System.Drawing.Point(0, 415);
					this.pnlMapControls.Margin = new System.Windows.Forms.Padding(2);
					this.pnlMapControls.Name = "pnlMapControls";
					this.pnlMapControls.Size = new System.Drawing.Size(216, 37);
					this.pnlMapControls.TabIndex = 3;
					// 
					// btnSelectStopLink
					// 
					this.btnSelectStopLink.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
					this.btnSelectStopLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnSelectStopLink.Location = new System.Drawing.Point(218, 7);
					this.btnSelectStopLink.Margin = new System.Windows.Forms.Padding(2);
					this.btnSelectStopLink.Name = "btnSelectStopLink";
					this.btnSelectStopLink.Size = new System.Drawing.Size(68, 22);
					this.btnSelectStopLink.TabIndex = 4;
					this.btnSelectStopLink.Text = "Stop Link";
					this.btnSelectStopLink.Click += new System.EventHandler(this.btnSelectStopLink_Click);
					// 
					// btnSelectStartLink
					// 
					this.btnSelectStartLink.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
					this.btnSelectStartLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnSelectStartLink.Location = new System.Drawing.Point(146, 7);
					this.btnSelectStartLink.Margin = new System.Windows.Forms.Padding(2);
					this.btnSelectStartLink.Name = "btnSelectStartLink";
					this.btnSelectStartLink.Size = new System.Drawing.Size(68, 22);
					this.btnSelectStartLink.TabIndex = 3;
					this.btnSelectStartLink.Text = "Start Link";
					this.btnSelectStartLink.Click += new System.EventHandler(this.btnSelectStartLink_Click);
					// 
					// btnPan
					// 
					this.btnPan.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
					this.btnPan.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnPan.Location = new System.Drawing.Point(74, 7);
					this.btnPan.Margin = new System.Windows.Forms.Padding(2);
					this.btnPan.Name = "btnPan";
					this.btnPan.Size = new System.Drawing.Size(68, 22);
					this.btnPan.TabIndex = 2;
					this.btnPan.Text = "Pan";
					this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
					// 
					// btnZoomOut
					// 
					this.btnZoomOut.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
					this.btnZoomOut.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnZoomOut.Location = new System.Drawing.Point(2, 7);
					this.btnZoomOut.Margin = new System.Windows.Forms.Padding(2);
					this.btnZoomOut.Name = "btnZoomOut";
					this.btnZoomOut.Size = new System.Drawing.Size(68, 22);
					this.btnZoomOut.TabIndex = 1;
					this.btnZoomOut.Text = "Zoom Out";
					this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
					// 
					// btnZoomIn
					// 
					this.btnZoomIn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
					this.btnZoomIn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnZoomIn.Location = new System.Drawing.Point(-70, 7);
					this.btnZoomIn.Margin = new System.Windows.Forms.Padding(2);
					this.btnZoomIn.Name = "btnZoomIn";
					this.btnZoomIn.Size = new System.Drawing.Size(68, 22);
					this.btnZoomIn.TabIndex = 0;
					this.btnZoomIn.Text = "Zoom In";
					this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
					// 
					// tabCharacterizationView
					// 
					this.tabCharacterizationView.Controls.Add(this.pnlCancelBackgroundWorker);
					this.tabCharacterizationView.Controls.Add(this.axWebBrowser);
					this.tabCharacterizationView.Controls.Add(this.panel2);
					this.tabCharacterizationView.Controls.Add(this.grpBoxReportBase);
					this.tabCharacterizationView.Controls.Add(this.panel1);
					this.tabCharacterizationView.Location = new System.Drawing.Point(1, 20);
					this.tabCharacterizationView.Margin = new System.Windows.Forms.Padding(2);
					this.tabCharacterizationView.Name = "tabCharacterizationView";
					this.tabCharacterizationView.Size = new System.Drawing.Size(216, 452);
					// 
					// pnlCancelBackgroundWorker
					// 
					this.pnlCancelBackgroundWorker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.pnlCancelBackgroundWorker.Controls.Add(this.ultraLabel3);
					this.pnlCancelBackgroundWorker.Controls.Add(this.btnCancel);
					this.pnlCancelBackgroundWorker.Location = new System.Drawing.Point(54, 172);
					this.pnlCancelBackgroundWorker.Margin = new System.Windows.Forms.Padding(2);
					this.pnlCancelBackgroundWorker.Name = "pnlCancelBackgroundWorker";
					this.pnlCancelBackgroundWorker.Size = new System.Drawing.Size(108, 116);
					this.pnlCancelBackgroundWorker.TabIndex = 22;
					this.pnlCancelBackgroundWorker.Visible = false;
					// 
					// ultraLabel3
					// 
					this.ultraLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					appearance1.TextHAlignAsString = "Center";
					appearance1.TextVAlignAsString = "Middle";
					this.ultraLabel3.Appearance = appearance1;
					this.ultraLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
					this.ultraLabel3.Location = new System.Drawing.Point(35, 10);
					this.ultraLabel3.Margin = new System.Windows.Forms.Padding(2);
					this.ultraLabel3.MinimumSize = new System.Drawing.Size(137, 55);
					this.ultraLabel3.Name = "ultraLabel3";
					this.ultraLabel3.Size = new System.Drawing.Size(137, 55);
					this.ultraLabel3.TabIndex = 3;
					this.ultraLabel3.Text = "Generating Report";
					// 
					// btnCancel
					// 
					this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
					this.btnCancel.Location = new System.Drawing.Point(35, 70);
					this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
					this.btnCancel.MinimumSize = new System.Drawing.Size(137, 37);
					this.btnCancel.Name = "btnCancel";
					this.btnCancel.Size = new System.Drawing.Size(137, 37);
					this.btnCancel.TabIndex = 2;
					this.btnCancel.Text = "Cancel";
					this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
					// 
					// axWebBrowser
					// 
					this.axWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
					this.axWebBrowser.Enabled = true;
					this.axWebBrowser.Location = new System.Drawing.Point(0, 155);
					this.axWebBrowser.Margin = new System.Windows.Forms.Padding(2);
					this.axWebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser.OcxState")));
					this.axWebBrowser.Size = new System.Drawing.Size(216, 297);
					this.axWebBrowser.TabIndex = 0;
					// 
					// panel2
					// 
					this.panel2.Controls.Add(this.btnCharacterize);
					this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
					this.panel2.Location = new System.Drawing.Point(0, 114);
					this.panel2.Margin = new System.Windows.Forms.Padding(2);
					this.panel2.Name = "panel2";
					this.panel2.Size = new System.Drawing.Size(216, 41);
					this.panel2.TabIndex = 3;
					// 
					// btnCharacterize
					// 
					this.btnCharacterize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.btnCharacterize.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnCharacterize.Location = new System.Drawing.Point(6, 5);
					this.btnCharacterize.Margin = new System.Windows.Forms.Padding(2);
					this.btnCharacterize.Name = "btnCharacterize";
					this.btnCharacterize.Size = new System.Drawing.Size(204, 32);
					this.btnCharacterize.TabIndex = 0;
					this.btnCharacterize.Text = "Generate Report";
					this.btnCharacterize.Click += new System.EventHandler(this.btnCharacterize_Click);
					// 
					// grpBoxReportBase
					// 
					this.grpBoxReportBase.Controls.Add(this.grpBoxReportBasePanel);
					this.grpBoxReportBase.Dock = System.Windows.Forms.DockStyle.Top;
					this.grpBoxReportBase.ExpandedSize = new System.Drawing.Size(216, 86);
					this.grpBoxReportBase.ExpansionIndicator = Infragistics.Win.Misc.GroupBoxExpansionIndicator.Far;
					this.grpBoxReportBase.Location = new System.Drawing.Point(0, 28);
					this.grpBoxReportBase.Margin = new System.Windows.Forms.Padding(2);
					this.grpBoxReportBase.Name = "grpBoxReportBase";
					this.grpBoxReportBase.Size = new System.Drawing.Size(216, 86);
					this.grpBoxReportBase.TabIndex = 0;
					this.grpBoxReportBase.Text = "Report Options";
					this.grpBoxReportBase.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.XP;
					this.grpBoxReportBase.ExpandedStateChanged += new System.EventHandler(this.grpBoxReportBase_ExpandedStateChanged);
					// 
					// grpBoxReportBasePanel
					// 
					this.grpBoxReportBasePanel.AutoSize = true;
					this.grpBoxReportBasePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
					this.grpBoxReportBasePanel.Controls.Add(this.reportOptionsLayoutPanel);
					this.grpBoxReportBasePanel.Dock = System.Windows.Forms.DockStyle.Fill;
					this.grpBoxReportBasePanel.Location = new System.Drawing.Point(3, 19);
					this.grpBoxReportBasePanel.Margin = new System.Windows.Forms.Padding(2);
					this.grpBoxReportBasePanel.Name = "grpBoxReportBasePanel";
					this.grpBoxReportBasePanel.Size = new System.Drawing.Size(210, 64);
					this.grpBoxReportBasePanel.TabIndex = 0;
					// 
					// reportOptionsLayoutPanel
					// 
					this.reportOptionsLayoutPanel.AutoSize = true;
					this.reportOptionsLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
					this.reportOptionsLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
					this.reportOptionsLayoutPanel.ColumnCount = 3;
					this.reportOptionsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
					this.reportOptionsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
					this.reportOptionsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
					this.reportOptionsLayoutPanel.Controls.Add(this.lblIncludedLibrary, 0, 0);
					this.reportOptionsLayoutPanel.Controls.Add(this.lblIncludedLibraryName, 1, 0);
					this.reportOptionsLayoutPanel.Controls.Add(this.lblAuxilaryData, 0, 1);
					this.reportOptionsLayoutPanel.Controls.Add(this.txtAuxilaryDataValue, 1, 1);
					this.reportOptionsLayoutPanel.Controls.Add(this.btnBrowseAuxilaryData, 2, 1);
					this.reportOptionsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
					this.reportOptionsLayoutPanel.Location = new System.Drawing.Point(0, 0);
					this.reportOptionsLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
					this.reportOptionsLayoutPanel.Name = "reportOptionsLayoutPanel";
					this.reportOptionsLayoutPanel.RowCount = 2;
					this.reportOptionsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
					this.reportOptionsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
					this.reportOptionsLayoutPanel.Size = new System.Drawing.Size(210, 46);
					this.reportOptionsLayoutPanel.TabIndex = 0;
					// 
					// lblIncludedLibrary
					// 
					appearance2.FontData.BoldAsString = "True";
					appearance2.TextVAlignAsString = "Middle";
					this.lblIncludedLibrary.Appearance = appearance2;
					this.lblIncludedLibrary.AutoSize = true;
					this.lblIncludedLibrary.Location = new System.Drawing.Point(3, 3);
					this.lblIncludedLibrary.Margin = new System.Windows.Forms.Padding(2);
					this.lblIncludedLibrary.Name = "lblIncludedLibrary";
					this.lblIncludedLibrary.Size = new System.Drawing.Size(78, 14);
					this.lblIncludedLibrary.TabIndex = 4;
					this.lblIncludedLibrary.Text = "Library Name:";
					// 
					// lblIncludedLibraryName
					// 
					appearance3.TextVAlignAsString = "Middle";
					this.lblIncludedLibraryName.Appearance = appearance3;
					this.lblIncludedLibraryName.AutoSize = true;
					this.reportOptionsLayoutPanel.SetColumnSpan(this.lblIncludedLibraryName, 2);
					this.lblIncludedLibraryName.Location = new System.Drawing.Point(86, 3);
					this.lblIncludedLibraryName.Margin = new System.Windows.Forms.Padding(2);
					this.lblIncludedLibraryName.Name = "lblIncludedLibraryName";
					this.lblIncludedLibraryName.Size = new System.Drawing.Size(60, 14);
					this.lblIncludedLibraryName.TabIndex = 5;
					this.lblIncludedLibraryName.Text = "ultraLabel5";
					// 
					// lblAuxilaryData
					// 
					appearance4.TextVAlignAsString = "Middle";
					this.lblAuxilaryData.Appearance = appearance4;
					this.lblAuxilaryData.AutoSize = true;
					this.lblAuxilaryData.Location = new System.Drawing.Point(3, 22);
					this.lblAuxilaryData.Margin = new System.Windows.Forms.Padding(2);
					this.lblAuxilaryData.Name = "lblAuxilaryData";
					this.lblAuxilaryData.Size = new System.Drawing.Size(75, 14);
					this.lblAuxilaryData.TabIndex = 1;
					this.lblAuxilaryData.Text = "Auxilary Data:";
					// 
					// txtAuxilaryDataValue
					// 
					this.txtAuxilaryDataValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
											| System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.txtAuxilaryDataValue.Location = new System.Drawing.Point(86, 22);
					this.txtAuxilaryDataValue.Margin = new System.Windows.Forms.Padding(2);
					this.txtAuxilaryDataValue.Name = "txtAuxilaryDataValue";
					this.txtAuxilaryDataValue.Size = new System.Drawing.Size(5, 21);
					this.txtAuxilaryDataValue.TabIndex = 3;
					this.txtAuxilaryDataValue.Text = "ultraTextEditor1";
					// 
					// btnBrowseAuxilaryData
					// 
					this.btnBrowseAuxilaryData.Location = new System.Drawing.Point(96, 22);
					this.btnBrowseAuxilaryData.Margin = new System.Windows.Forms.Padding(2);
					this.btnBrowseAuxilaryData.Name = "btnBrowseAuxilaryData";
					this.btnBrowseAuxilaryData.Size = new System.Drawing.Size(52, 19);
					this.btnBrowseAuxilaryData.TabIndex = 2;
					this.btnBrowseAuxilaryData.Text = "Browse...";
					this.btnBrowseAuxilaryData.Click += new System.EventHandler(this.btnBrowseAuxilaryData_Click);
					// 
					// panel1
					// 
					this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
					this.panel1.Controls.Add(this.btnImportReport);
					this.panel1.Controls.Add(this.ultraLabel2);
					this.panel1.Controls.Add(this.cmbReportChooser);
					this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
					this.panel1.Location = new System.Drawing.Point(0, 0);
					this.panel1.Margin = new System.Windows.Forms.Padding(2);
					this.panel1.Name = "panel1";
					this.panel1.Size = new System.Drawing.Size(216, 28);
					this.panel1.TabIndex = 1;
					// 
					// btnImportReport
					// 
					this.btnImportReport.Anchor = System.Windows.Forms.AnchorStyles.Right;
					this.btnImportReport.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnImportReport.Location = new System.Drawing.Point(150, 4);
					this.btnImportReport.Margin = new System.Windows.Forms.Padding(2);
					this.btnImportReport.Name = "btnImportReport";
					this.btnImportReport.Size = new System.Drawing.Size(59, 20);
					this.btnImportReport.TabIndex = 1;
					this.btnImportReport.Text = "Import";
					this.btnImportReport.Click += new System.EventHandler(this.btnImportReport_Click);
					// 
					// ultraLabel2
					// 
					appearance5.TextVAlignAsString = "Middle";
					this.ultraLabel2.Appearance = appearance5;
					this.ultraLabel2.Location = new System.Drawing.Point(6, 6);
					this.ultraLabel2.Margin = new System.Windows.Forms.Padding(2);
					this.ultraLabel2.Name = "ultraLabel2";
					this.ultraLabel2.Size = new System.Drawing.Size(92, 19);
					this.ultraLabel2.TabIndex = 19;
					this.ultraLabel2.Text = "Choose a report:";
					// 
					// cmbReportChooser
					// 
					this.cmbReportChooser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.cmbReportChooser.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
					this.cmbReportChooser.DataMember = "reportTable1";
					this.cmbReportChooser.DataSource = this.reportTable;
					appearance6.BackColor = System.Drawing.SystemColors.Window;
					appearance6.BorderColor = System.Drawing.SystemColors.InactiveCaption;
					this.cmbReportChooser.DisplayLayout.Appearance = appearance6;
					this.cmbReportChooser.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
					ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
					ultraGridColumn1.Header.VisiblePosition = 0;
					ultraGridColumn1.Width = 25;
					ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
					ultraGridColumn2.AutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.None;
					ultraGridColumn2.Header.Enabled = false;
					ultraGridColumn2.Header.VisiblePosition = 1;
					ultraGridColumn2.Hidden = true;
					ultraGridColumn2.Width = 14;
					ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2});
					ultraGridBand1.Header.Enabled = false;
					this.cmbReportChooser.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
					this.cmbReportChooser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
					this.cmbReportChooser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
					this.cmbReportChooser.DisplayLayout.ColumnChooserEnabled = Infragistics.Win.DefaultableBoolean.True;
					appearance7.BackColor = System.Drawing.SystemColors.ActiveBorder;
					appearance7.BackColor2 = System.Drawing.SystemColors.ControlDark;
					appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
					appearance7.BorderColor = System.Drawing.SystemColors.Window;
					this.cmbReportChooser.DisplayLayout.GroupByBox.Appearance = appearance7;
					appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
					this.cmbReportChooser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance8;
					this.cmbReportChooser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
					appearance9.BackColor = System.Drawing.SystemColors.ControlLightLight;
					appearance9.BackColor2 = System.Drawing.SystemColors.Control;
					appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
					appearance9.ForeColor = System.Drawing.SystemColors.GrayText;
					this.cmbReportChooser.DisplayLayout.GroupByBox.PromptAppearance = appearance9;
					this.cmbReportChooser.DisplayLayout.MaxColScrollRegions = 1;
					this.cmbReportChooser.DisplayLayout.MaxRowScrollRegions = 1;
					appearance10.BackColor = System.Drawing.SystemColors.Window;
					appearance10.ForeColor = System.Drawing.SystemColors.ControlText;
					this.cmbReportChooser.DisplayLayout.Override.ActiveCellAppearance = appearance10;
					appearance11.BackColor = System.Drawing.SystemColors.Highlight;
					appearance11.ForeColor = System.Drawing.SystemColors.HighlightText;
					this.cmbReportChooser.DisplayLayout.Override.ActiveRowAppearance = appearance11;
					this.cmbReportChooser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
					this.cmbReportChooser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
					appearance12.BackColor = System.Drawing.SystemColors.Window;
					this.cmbReportChooser.DisplayLayout.Override.CardAreaAppearance = appearance12;
					appearance13.BorderColor = System.Drawing.Color.Silver;
					appearance13.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
					this.cmbReportChooser.DisplayLayout.Override.CellAppearance = appearance13;
					this.cmbReportChooser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
					this.cmbReportChooser.DisplayLayout.Override.CellPadding = 0;
					appearance14.BackColor = System.Drawing.SystemColors.Control;
					appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
					appearance14.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
					appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
					appearance14.BorderColor = System.Drawing.SystemColors.Window;
					this.cmbReportChooser.DisplayLayout.Override.GroupByRowAppearance = appearance14;
					appearance15.TextHAlignAsString = "Left";
					this.cmbReportChooser.DisplayLayout.Override.HeaderAppearance = appearance15;
					this.cmbReportChooser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
					this.cmbReportChooser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
					appearance16.BackColor = System.Drawing.SystemColors.Window;
					appearance16.BorderColor = System.Drawing.Color.Silver;
					this.cmbReportChooser.DisplayLayout.Override.RowAppearance = appearance16;
					this.cmbReportChooser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
					appearance17.BackColor = System.Drawing.SystemColors.ControlLight;
					this.cmbReportChooser.DisplayLayout.Override.TemplateAddRowAppearance = appearance17;
					this.cmbReportChooser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
					this.cmbReportChooser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
					this.cmbReportChooser.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
					this.cmbReportChooser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
					this.cmbReportChooser.DisplayMember = "ReportName";
					this.cmbReportChooser.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
					this.cmbReportChooser.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
					this.cmbReportChooser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
					this.cmbReportChooser.LimitToList = true;
					this.cmbReportChooser.Location = new System.Drawing.Point(102, 4);
					this.cmbReportChooser.Margin = new System.Windows.Forms.Padding(2);
					this.cmbReportChooser.Name = "cmbReportChooser";
					this.cmbReportChooser.Size = new System.Drawing.Size(44, 22);
					this.cmbReportChooser.TabIndex = 0;
					this.cmbReportChooser.ValueMember = "ReportPath";
					this.cmbReportChooser.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.cmbReportChooser_RowSelected);
					// 
					// reportTable
					// 
					this.reportTable.DataSetName = "reportTable";
					this.reportTable.Tables.AddRange(new System.Data.DataTable[] {
            this.reportTable1});
					// 
					// reportTable1
					// 
					this.reportTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2});
					this.reportTable1.TableName = "reportTable1";
					// 
					// dataColumn1
					// 
					this.dataColumn1.ColumnName = "ReportName";
					// 
					// dataColumn2
					// 
					this.dataColumn2.ColumnName = "ReportPath";
					// 
					// tabModelCatalog
					// 
					this.tabModelCatalog.Controls.Add(this.grpboxStartLinks);
					this.tabModelCatalog.Controls.Add(this.grpboxStopLinks);
					this.tabModelCatalog.Controls.Add(this.btnPreviewTrace);
					this.tabModelCatalog.Controls.Add(this.btnClearList);
					this.tabModelCatalog.Controls.Add(this.btnReconcileModel);
					this.tabModelCatalog.Controls.Add(this.btnImportFromModel);
					this.tabModelCatalog.Controls.Add(this.btnImportFromIni);
					this.tabModelCatalog.Location = new System.Drawing.Point(1, 20);
					this.tabModelCatalog.Margin = new System.Windows.Forms.Padding(2);
					this.tabModelCatalog.Name = "tabModelCatalog";
					this.tabModelCatalog.Size = new System.Drawing.Size(385, 381);
					// 
					// grpboxStartLinks
					// 
					this.grpboxStartLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
											| System.Windows.Forms.AnchorStyles.Left)));
					this.grpboxStartLinks.Controls.Add(this.panel3);
					this.grpboxStartLinks.Controls.Add(this.lstStartLinks);
					this.grpboxStartLinks.Location = new System.Drawing.Point(5, 8);
					this.grpboxStartLinks.Margin = new System.Windows.Forms.Padding(2);
					this.grpboxStartLinks.Name = "grpboxStartLinks";
					this.grpboxStartLinks.Size = new System.Drawing.Size(136, 257);
					this.grpboxStartLinks.TabIndex = 20;
					this.grpboxStartLinks.Text = "Start Links";
					this.grpboxStartLinks.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
					// 
					// panel3
					// 
					this.panel3.Controls.Add(this.btnRemoveStartLink);
					this.panel3.Controls.Add(this.btnAddStartLink);
					this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
					this.panel3.Location = new System.Drawing.Point(2, 210);
					this.panel3.Margin = new System.Windows.Forms.Padding(2);
					this.panel3.Name = "panel3";
					this.panel3.Size = new System.Drawing.Size(132, 45);
					this.panel3.TabIndex = 1;
					// 
					// btnRemoveStartLink
					// 
					this.btnRemoveStartLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.btnRemoveStartLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnRemoveStartLink.Location = new System.Drawing.Point(68, 7);
					this.btnRemoveStartLink.Margin = new System.Windows.Forms.Padding(2);
					this.btnRemoveStartLink.Name = "btnRemoveStartLink";
					this.btnRemoveStartLink.Size = new System.Drawing.Size(62, 32);
					this.btnRemoveStartLink.TabIndex = 1;
					this.btnRemoveStartLink.Text = "Remove";
					this.btnRemoveStartLink.Click += new System.EventHandler(this.btnRemoveStartLink_Click);
					// 
					// btnAddStartLink
					// 
					this.btnAddStartLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this.btnAddStartLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnAddStartLink.Location = new System.Drawing.Point(4, 7);
					this.btnAddStartLink.Margin = new System.Windows.Forms.Padding(2);
					this.btnAddStartLink.Name = "btnAddStartLink";
					this.btnAddStartLink.Size = new System.Drawing.Size(62, 32);
					this.btnAddStartLink.TabIndex = 0;
					this.btnAddStartLink.Text = "Add";
					this.btnAddStartLink.Click += new System.EventHandler(this.btnAddStartLink_Click);
					// 
					// lstStartLinks
					// 
					this.lstStartLinks.Dock = System.Windows.Forms.DockStyle.Fill;
					this.lstStartLinks.Location = new System.Drawing.Point(2, 18);
					this.lstStartLinks.Margin = new System.Windows.Forms.Padding(2);
					this.lstStartLinks.Name = "lstStartLinks";
					this.lstStartLinks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
					this.lstStartLinks.Size = new System.Drawing.Size(132, 225);
					this.lstStartLinks.TabIndex = 0;
					// 
					// grpboxStopLinks
					// 
					this.grpboxStopLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
											| System.Windows.Forms.AnchorStyles.Left)));
					this.grpboxStopLinks.Controls.Add(this.panel4);
					this.grpboxStopLinks.Controls.Add(this.lstStopLinks);
					this.grpboxStopLinks.Location = new System.Drawing.Point(148, 8);
					this.grpboxStopLinks.Margin = new System.Windows.Forms.Padding(2);
					this.grpboxStopLinks.Name = "grpboxStopLinks";
					this.grpboxStopLinks.Size = new System.Drawing.Size(136, 257);
					this.grpboxStopLinks.TabIndex = 19;
					this.grpboxStopLinks.Text = "Stop Links";
					this.grpboxStopLinks.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
					// 
					// panel4
					// 
					this.panel4.Controls.Add(this.btnRemoveStopLink);
					this.panel4.Controls.Add(this.btnAddStopLink);
					this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
					this.panel4.Location = new System.Drawing.Point(2, 210);
					this.panel4.Margin = new System.Windows.Forms.Padding(2);
					this.panel4.Name = "panel4";
					this.panel4.Size = new System.Drawing.Size(132, 45);
					this.panel4.TabIndex = 1;
					// 
					// btnRemoveStopLink
					// 
					this.btnRemoveStopLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.btnRemoveStopLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnRemoveStopLink.Location = new System.Drawing.Point(66, 7);
					this.btnRemoveStopLink.Margin = new System.Windows.Forms.Padding(2);
					this.btnRemoveStopLink.Name = "btnRemoveStopLink";
					this.btnRemoveStopLink.Size = new System.Drawing.Size(62, 32);
					this.btnRemoveStopLink.TabIndex = 3;
					this.btnRemoveStopLink.Text = "Remove";
					this.btnRemoveStopLink.Click += new System.EventHandler(this.btnRemoveStopLink_Click);
					// 
					// btnAddStopLink
					// 
					this.btnAddStopLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this.btnAddStopLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnAddStopLink.Location = new System.Drawing.Point(2, 7);
					this.btnAddStopLink.Margin = new System.Windows.Forms.Padding(2);
					this.btnAddStopLink.Name = "btnAddStopLink";
					this.btnAddStopLink.Size = new System.Drawing.Size(62, 32);
					this.btnAddStopLink.TabIndex = 2;
					this.btnAddStopLink.Text = "Add";
					this.btnAddStopLink.Click += new System.EventHandler(this.btnAddStopLink_Click);
					// 
					// lstStopLinks
					// 
					this.lstStopLinks.Dock = System.Windows.Forms.DockStyle.Fill;
					this.lstStopLinks.Location = new System.Drawing.Point(2, 18);
					this.lstStopLinks.Margin = new System.Windows.Forms.Padding(6);
					this.lstStopLinks.Name = "lstStopLinks";
					this.lstStopLinks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
					this.lstStopLinks.Size = new System.Drawing.Size(132, 225);
					this.lstStopLinks.TabIndex = 0;
					// 
					// btnPreviewTrace
					// 
					this.btnPreviewTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this.btnPreviewTrace.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnPreviewTrace.Location = new System.Drawing.Point(5, 317);
					this.btnPreviewTrace.Margin = new System.Windows.Forms.Padding(2);
					this.btnPreviewTrace.Name = "btnPreviewTrace";
					this.btnPreviewTrace.Size = new System.Drawing.Size(278, 54);
					this.btnPreviewTrace.TabIndex = 4;
					this.btnPreviewTrace.Text = "Preview Trace";
					this.btnPreviewTrace.Click += new System.EventHandler(this.btnPreviewTrace_Click);
					// 
					// btnClearList
					// 
					this.btnClearList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this.btnClearList.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnClearList.Location = new System.Drawing.Point(202, 274);
					this.btnClearList.Margin = new System.Windows.Forms.Padding(2);
					this.btnClearList.Name = "btnClearList";
					this.btnClearList.Size = new System.Drawing.Size(82, 38);
					this.btnClearList.TabIndex = 7;
					this.btnClearList.Text = "Clear Links";
					this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
					// 
					// btnReconcileModel
					// 
					this.btnReconcileModel.Enabled = false;
					this.btnReconcileModel.Location = new System.Drawing.Point(106, 254);
					this.btnReconcileModel.Margin = new System.Windows.Forms.Padding(2);
					this.btnReconcileModel.Name = "btnReconcileModel";
					this.btnReconcileModel.Size = new System.Drawing.Size(71, 32);
					this.btnReconcileModel.TabIndex = 7;
					this.btnReconcileModel.Text = "Reconcile Model";
					this.btnReconcileModel.Visible = false;
					this.btnReconcileModel.Click += new System.EventHandler(this.btnReconcileModel_Click);
					// 
					// btnImportFromModel
					// 
					this.btnImportFromModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this.btnImportFromModel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnImportFromModel.Location = new System.Drawing.Point(96, 274);
					this.btnImportFromModel.Margin = new System.Windows.Forms.Padding(2);
					this.btnImportFromModel.Name = "btnImportFromModel";
					this.btnImportFromModel.Size = new System.Drawing.Size(94, 38);
					this.btnImportFromModel.TabIndex = 6;
					this.btnImportFromModel.Text = "Import From Model Catalog";
					this.btnImportFromModel.Click += new System.EventHandler(this.btnImportFromModel_Click);
					// 
					// btnImportFromIni
					// 
					this.btnImportFromIni.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
					this.btnImportFromIni.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
					this.btnImportFromIni.Location = new System.Drawing.Point(5, 274);
					this.btnImportFromIni.Margin = new System.Windows.Forms.Padding(2);
					this.btnImportFromIni.Name = "btnImportFromIni";
					this.btnImportFromIni.Size = new System.Drawing.Size(83, 38);
					this.btnImportFromIni.TabIndex = 5;
					this.btnImportFromIni.Text = "Import From Model.ini";
					this.btnImportFromIni.Click += new System.EventHandler(this.btnImportFromIni_Click);
					// 
					// tabInteractiveSelect
					// 
					this.tabInteractiveSelect.Enabled = false;
					this.tabInteractiveSelect.Location = new System.Drawing.Point(-10000, -10000);
					this.tabInteractiveSelect.Margin = new System.Windows.Forms.Padding(2);
					this.tabInteractiveSelect.Name = "tabInteractiveSelect";
					this.tabInteractiveSelect.Size = new System.Drawing.Size(385, 381);
					// 
					// tabPolygonSelect
					// 
					this.tabPolygonSelect.Enabled = false;
					this.tabPolygonSelect.Location = new System.Drawing.Point(-10000, -10000);
					this.tabPolygonSelect.Margin = new System.Windows.Forms.Padding(2);
					this.tabPolygonSelect.Name = "tabPolygonSelect";
					this.tabPolygonSelect.Size = new System.Drawing.Size(385, 381);
					// 
					// tabControlOutputDisplay
					// 
					this.tabControlOutputDisplay.Controls.Add(this.tabOutputDisplaySharedControlsPage);
					this.tabControlOutputDisplay.Controls.Add(this.tabMapView);
					this.tabControlOutputDisplay.Controls.Add(this.tabCharacterizationView);
					this.tabControlOutputDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
					this.tabControlOutputDisplay.Location = new System.Drawing.Point(0, 0);
					this.tabControlOutputDisplay.Margin = new System.Windows.Forms.Padding(2);
					this.tabControlOutputDisplay.Name = "tabControlOutputDisplay";
					this.tabControlOutputDisplay.SharedControlsPage = this.tabOutputDisplaySharedControlsPage;
					this.tabControlOutputDisplay.Size = new System.Drawing.Size(218, 473);
					this.tabControlOutputDisplay.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio;
					this.tabControlOutputDisplay.TabIndex = 0;
					ultraTab1.TabPage = this.tabMapView;
					ultraTab1.Text = "Map View";
					ultraTab2.TabPage = this.tabCharacterizationView;
					ultraTab2.Text = "Report View";
					this.tabControlOutputDisplay.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2});
					// 
					// tabOutputDisplaySharedControlsPage
					// 
					this.tabOutputDisplaySharedControlsPage.Location = new System.Drawing.Point(-10000, -10000);
					this.tabOutputDisplaySharedControlsPage.Margin = new System.Windows.Forms.Padding(2);
					this.tabOutputDisplaySharedControlsPage.Name = "tabOutputDisplaySharedControlsPage";
					this.tabOutputDisplaySharedControlsPage.Size = new System.Drawing.Size(216, 452);
					// 
					// txtStudyAreaName
					// 
					this.txtStudyAreaName.Location = new System.Drawing.Point(7, 27);
					this.txtStudyAreaName.Margin = new System.Windows.Forms.Padding(2);
					this.txtStudyAreaName.Name = "txtStudyAreaName";
					this.txtStudyAreaName.Size = new System.Drawing.Size(281, 20);
					this.txtStudyAreaName.TabIndex = 0;
					// 
					// btnSelectCharDir
					// 
					this.btnSelectCharDir.Location = new System.Drawing.Point(-61, 0);
					this.btnSelectCharDir.Margin = new System.Windows.Forms.Padding(2);
					this.btnSelectCharDir.Name = "btnSelectCharDir";
					this.btnSelectCharDir.Size = new System.Drawing.Size(56, 19);
					this.btnSelectCharDir.TabIndex = 21;
					// 
					// pnlCharacterizationView
					// 
					this.pnlCharacterizationView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
					this.pnlCharacterizationView.Controls.Add(this.splitContainer1);
					this.pnlCharacterizationView.Dock = System.Windows.Forms.DockStyle.Fill;
					this.pnlCharacterizationView.Location = new System.Drawing.Point(0, 0);
					this.pnlCharacterizationView.Margin = new System.Windows.Forms.Padding(2);
					this.pnlCharacterizationView.Name = "pnlCharacterizationView";
					this.pnlCharacterizationView.Size = new System.Drawing.Size(619, 479);
					this.pnlCharacterizationView.TabIndex = 24;
					// 
					// splitContainer1
					// 
					this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
					this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
					this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
					this.splitContainer1.Location = new System.Drawing.Point(0, 0);
					this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
					this.splitContainer1.Name = "splitContainer1";
					// 
					// splitContainer1.Panel1
					// 
					this.splitContainer1.Panel1.Controls.Add(this.ultraLabel1);
					this.splitContainer1.Panel1.Controls.Add(this.lblStudyArea);
					this.splitContainer1.Panel1.Controls.Add(this.tabControlStudyArea);
					this.splitContainer1.Panel1.Controls.Add(this.txtStudyAreaName);
					// 
					// splitContainer1.Panel2
					// 
					this.splitContainer1.Panel2.Controls.Add(this.tabControlOutputDisplay);
					this.splitContainer1.Size = new System.Drawing.Size(617, 477);
					this.splitContainer1.SplitterDistance = 392;
					this.splitContainer1.SplitterWidth = 3;
					this.splitContainer1.TabIndex = 22;
					// 
					// ultraLabel1
					// 
					this.ultraLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
											| System.Windows.Forms.AnchorStyles.Right)));
					this.ultraLabel1.Location = new System.Drawing.Point(7, 50);
					this.ultraLabel1.Margin = new System.Windows.Forms.Padding(2);
					this.ultraLabel1.Name = "ultraLabel1";
					this.ultraLabel1.Size = new System.Drawing.Size(365, 19);
					this.ultraLabel1.TabIndex = 18;
					this.ultraLabel1.Text = "Characterize study area using:";
					// 
					// lblStudyArea
					// 
					this.lblStudyArea.Location = new System.Drawing.Point(7, 3);
					this.lblStudyArea.Margin = new System.Windows.Forms.Padding(2);
					this.lblStudyArea.Name = "lblStudyArea";
					this.lblStudyArea.Size = new System.Drawing.Size(280, 19);
					this.lblStudyArea.TabIndex = 17;
					this.lblStudyArea.Text = "Study Area Name:";
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
					this.tabControlStudyArea.Location = new System.Drawing.Point(1, 73);
					this.tabControlStudyArea.Margin = new System.Windows.Forms.Padding(2);
					this.tabControlStudyArea.Name = "tabControlStudyArea";
					this.tabControlStudyArea.SharedControlsPage = this.tabStudyAreaSharedControlsPage;
					this.tabControlStudyArea.Size = new System.Drawing.Size(387, 402);
					this.tabControlStudyArea.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio;
					this.tabControlStudyArea.TabIndex = 1;
					ultraTab3.TabPage = this.tabModelCatalog;
					ultraTab3.Text = "Network Trace";
					ultraTab4.TabPage = this.tabInteractiveSelect;
					ultraTab4.Text = "Interactive Select";
					ultraTab5.TabPage = this.tabPolygonSelect;
					ultraTab5.Text = "Polygon Select";
					this.tabControlStudyArea.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab3,
            ultraTab4,
            ultraTab5});
					// 
					// tabStudyAreaSharedControlsPage
					// 
					this.tabStudyAreaSharedControlsPage.Location = new System.Drawing.Point(-10000, -10000);
					this.tabStudyAreaSharedControlsPage.Margin = new System.Windows.Forms.Padding(2);
					this.tabStudyAreaSharedControlsPage.Name = "tabStudyAreaSharedControlsPage";
					this.tabStudyAreaSharedControlsPage.Size = new System.Drawing.Size(385, 381);
					// 
					// saveFileDialog
					// 
					this.saveFileDialog.DefaultExt = "xml";
					this.saveFileDialog.RestoreDirectory = true;
					this.saveFileDialog.Title = "Save characterization report as...";
					// 
					// ofd
					// 
					this.ofd.RestoreDirectory = true;
					// 
					// backgroundWorker1
					// 
					this.backgroundWorker1.WorkerReportsProgress = true;
					this.backgroundWorker1.WorkerSupportsCancellation = true;
					this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
					this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
					this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
					// 
					// printPreviewDialog1
					// 
					this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
					this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
					this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
					this.printPreviewDialog1.Enabled = true;
					this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
					this.printPreviewDialog1.Name = "printPreviewDialog1";
					this.printPreviewDialog1.Visible = false;
					// 
					// CreateReportForm
					// 
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.ClientSize = new System.Drawing.Size(619, 479);
					this.Controls.Add(this.pnlCharacterizationView);
					this.Controls.Add(this.btnSelectCharDir);
					this.Margin = new System.Windows.Forms.Padding(2);
					this.Name = "CreateReportForm";
					this.ShowIcon = false;
					this.Text = "Create Report";
					this.Load += new System.EventHandler(this.CreateReportForm_Load);
					this.tabMapView.ResumeLayout(false);
					((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
					this.pnlMapControls.ResumeLayout(false);
					this.tabCharacterizationView.ResumeLayout(false);
					this.pnlCancelBackgroundWorker.ResumeLayout(false);
					((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).EndInit();
					this.panel2.ResumeLayout(false);
					((System.ComponentModel.ISupportInitialize)(this.grpBoxReportBase)).EndInit();
					this.grpBoxReportBase.ResumeLayout(false);
					this.grpBoxReportBase.PerformLayout();
					this.grpBoxReportBasePanel.ResumeLayout(false);
					this.grpBoxReportBasePanel.PerformLayout();
					this.reportOptionsLayoutPanel.ResumeLayout(false);
					this.reportOptionsLayoutPanel.PerformLayout();
					((System.ComponentModel.ISupportInitialize)(this.txtAuxilaryDataValue)).EndInit();
					this.panel1.ResumeLayout(false);
					this.panel1.PerformLayout();
					((System.ComponentModel.ISupportInitialize)(this.cmbReportChooser)).EndInit();
					((System.ComponentModel.ISupportInitialize)(this.reportTable)).EndInit();
					((System.ComponentModel.ISupportInitialize)(this.reportTable1)).EndInit();
					this.tabModelCatalog.ResumeLayout(false);
					((System.ComponentModel.ISupportInitialize)(this.grpboxStartLinks)).EndInit();
					this.grpboxStartLinks.ResumeLayout(false);
					this.panel3.ResumeLayout(false);
					((System.ComponentModel.ISupportInitialize)(this.grpboxStopLinks)).EndInit();
					this.grpboxStopLinks.ResumeLayout(false);
					this.panel4.ResumeLayout(false);
					((System.ComponentModel.ISupportInitialize)(this.tabControlOutputDisplay)).EndInit();
					this.tabControlOutputDisplay.ResumeLayout(false);
					this.pnlCharacterizationView.ResumeLayout(false);
					this.splitContainer1.Panel1.ResumeLayout(false);
					this.splitContainer1.Panel1.PerformLayout();
					this.splitContainer1.Panel2.ResumeLayout(false);
					this.splitContainer1.ResumeLayout(false);
					((System.ComponentModel.ISupportInitialize)(this.tabControlStudyArea)).EndInit();
					this.tabControlStudyArea.ResumeLayout(false);
					this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtStudyAreaName;
        private System.Windows.Forms.ListBox lstStopLinks;
        private System.Windows.Forms.ListBox lstStartLinks;
        private System.Windows.Forms.Button btnSelectCharDir;
        private System.Windows.Forms.Panel pnlCharacterizationView;
        private System.Windows.Forms.Button btnReconcileModel;        
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl tabControlStudyArea;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage tabStudyAreaSharedControlsPage;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabModelCatalog;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabInteractiveSelect;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabPolygonSelect;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage tabOutputDisplaySharedControlsPage;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabMapView;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabCharacterizationView;
        private System.Windows.Forms.Panel pnlMapControls;                
        private AxSHDocVw.AxWebBrowser axWebBrowser;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl tabControlOutputDisplay;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Panel panel1;
        private Infragistics.Win.UltraWinGrid.UltraCombo cmbReportChooser;
        private System.Data.DataSet reportTable;
        private System.Data.DataTable reportTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private Infragistics.Win.Misc.UltraExpandableGroupBox grpBoxReportBase;
        private Infragistics.Win.Misc.UltraExpandableGroupBoxPanel grpBoxReportBasePanel;
        private Infragistics.Win.Misc.UltraButton btnImportFromIni;
        private Infragistics.Win.Misc.UltraButton btnImportFromModel;
        private Infragistics.Win.Misc.UltraButton btnClearList;
        private Infragistics.Win.Misc.UltraButton btnPreviewTrace;
        private Infragistics.Win.Misc.UltraButton btnRemoveStartLink;
        private Infragistics.Win.Misc.UltraButton btnAddStartLink;
        private Infragistics.Win.Misc.UltraButton btnRemoveStopLink;
        private Infragistics.Win.Misc.UltraButton btnAddStopLink;
        private Infragistics.Win.Misc.UltraLabel lblStudyArea;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraButton btnCharacterize;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel reportOptionsLayoutPanel;
        private System.Windows.Forms.OpenFileDialog ofd;
        private Infragistics.Win.Misc.UltraLabel lblAuxilaryData;
        private Infragistics.Win.Misc.UltraButton btnBrowseAuxilaryData;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtAuxilaryDataValue;
        private Infragistics.Win.Misc.UltraLabel lblIncludedLibrary;
        private Infragistics.Win.Misc.UltraLabel lblIncludedLibraryName;
        private Infragistics.Win.Misc.UltraButton btnZoomIn;
        private Infragistics.Win.Misc.UltraButton btnPan;
        private Infragistics.Win.Misc.UltraButton btnZoomOut;
        private Infragistics.Win.Misc.UltraGroupBox grpboxStartLinks;
        private Infragistics.Win.Misc.UltraGroupBox grpboxStopLinks;
        private Infragistics.Win.Misc.UltraButton btnImportReport;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
        private Infragistics.Win.Misc.UltraButton btnSelectStopLink;
        private Infragistics.Win.Misc.UltraButton btnSelectStartLink;
        private System.Windows.Forms.Panel pnlCancelBackgroundWorker;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.Misc.UltraButton btnCancel;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;        
    }
}