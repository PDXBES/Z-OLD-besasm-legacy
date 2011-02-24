using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using SystemsAnalysis;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Characterization
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class CharacterizationForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 
		/*	This object ensures that all COM-wrapped ArcObjects are
			correctly disposed of when the application shuts down. This
			must be the first ArcObject instantiated	*/
		private IAoInitialize m_AoInitialize = new AoInitializeClass();
			
		private DataAccess.AGMasterDataSet agMasterDataSet1;
		private DataAccess.AGMasterDataAccess agMasterDataAccess1;
		private DataAccess.ModelCatalogDataSet modelCatalogDataSet1;
		private DataAccess.ModelCatalogDataAccess modelCatalogDataAccess1;
		
		//Map Control commands
		private SystemsAnalysis.Utils.ArcObjects.MapControl.SelectFeatures selectCommand;
		private SystemsAnalysis.Utils.ArcObjects.MapControl.ClearFeatureSelection clearCommand;
		private SystemsAnalysis.Utils.ArcObjects.MapControl.Pan panCommand;
		private SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomIn zoomInCommand;
		private SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomOut zoomOutCommand;
		
		//Data view used for showing only Models associatted with the selected Scenario
		private DataView modelCatalogView;

		private Model selectedModel;

		private Links mstLinks;
		private Links startLinks;		
		private Links stopLinks;				
				
		private SystemsAnalysis.Characterization.Settings settings;
		private SystemsAnalysis.Characterization.Preferences pref;

		#region Form component declarations
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemFileExit;
		private System.Windows.Forms.Button btnPan;
		private System.Windows.Forms.Button btnZoomOut;
		private System.Windows.Forms.Button btnZoomIn;
		private ESRI.ArcGIS.MapControl.AxMapControl axMapControl;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel pnlCharacterizationView;
		private System.Windows.Forms.TabPage tabMapView;
		private System.Windows.Forms.TabPage tabCharacterizationView;
		private System.Windows.Forms.Panel pnlMapControls;
		private System.Windows.Forms.TabPage tabCatalogSelect;
		private System.Windows.Forms.TabPage tabInteractSelect;
		private System.Windows.Forms.TabPage tabPolySelect;
		private System.Windows.Forms.DataGrid grdModelCatalog;
		private System.Windows.Forms.ComboBox cmbSelectScenario;
		private System.Windows.Forms.Label lblScenario;
		private System.Windows.Forms.Button btnCharacterize;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.ListBox lstStartLinks;
		private System.Windows.Forms.ListBox lstStopLinks;
		private System.Windows.Forms.Label lblStartLinks;
		private System.Windows.Forms.Label lblStopLinks;
		private System.Windows.Forms.Button btnAddModel;
		private System.Windows.Forms.Button btnClearList;
		private System.Windows.Forms.Button btnRemoveStartLink;
		private System.Windows.Forms.Button btnRemoveStopLink;
		private System.Windows.Forms.Button btnPreviewTrace;
		private System.Windows.Forms.Button btnReconcileModel;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.Label lblStudyArea;
		private System.Windows.Forms.TextBox txtStudyAreaName;
		private System.Windows.Forms.Button btnAddStartLink;
		private System.Windows.Forms.Button btnAddStopLink;
		private System.Windows.Forms.Button btnSelectTemplate;
		private System.Windows.Forms.Button btnSelectCharDir;
		private System.Windows.Forms.TabControl tabControlOutputDisplay;
		private System.Windows.Forms.TextBox txtCharTableTemplate;
		private System.Windows.Forms.TextBox txtCharOutputDir;		
		private System.Windows.Forms.Label lblOutputDir;
		private System.Windows.Forms.Label lblCharTemplate;
		private System.Windows.Forms.MenuItem menuItemTools;
		private System.Windows.Forms.TabPage tabStatusView;		
		private System.Windows.Forms.MenuItem menuItemToolsSettings;		
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.TabControl tabControlStudyArea;
		private System.Windows.Forms.RichTextBox statusTextBox;
		private System.Windows.Forms.Panel pnlStatusControl;
		private System.Windows.Forms.Panel pnlModelCatalogControls;
		private System.Windows.Forms.Panel pnlStudyAreaControls;
		private AxSHDocVw.AxWebBrowser axWebBrowser;
		private System.Windows.Forms.MenuItem menuItemToolsClearLog;
		#endregion
			
		/// <summary>
		/// Main user-input form for the characterization tool.
		/// </summary>
		public CharacterizationForm()
		{
			/*	This object ensures that all COM-wrapped ArcObjects are
				correctly disposed of when the application shuts down. */
			m_AoInitialize = new AoInitializeClass();
						
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
				 
			settings.ReadXml("\\\\Cassio\\Modeling\\Model_Programs\\ResultsWarehouse\\CharacterizationQueriesManager\\Default_Settings.xml");			
			
			this.loadMasterData();

			this.selectedModel = this.getSelectedModel();

			startLinks = new Links();
			startLinks.OnAddedLink += 
				new OnAddLinkEventHandler(this.startLinks_OnAddedLink);
			startLinks.OnRemovedLink += 
				new OnRemoveLinkEventHandler(this.startLinks_OnRemovedLink);			
			stopLinks = new Links();
			stopLinks.OnAddedLink += 
				new OnAddLinkEventHandler(this.stopLinks_OnAddedLink);
			stopLinks.OnRemovedLink +=
				new OnRemoveLinkEventHandler(this.stopLinks_OnRemovedLink);
			lstStartLinks.DisplayMember = "MLinkID";
			lstStopLinks.DisplayMember = "MLinkID";																	
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			axMapControl.ClearLayers();			
			axMapControl = null;			
			/*	Ensure that all ArcObjects are disposed of properly.
				Prior to implementing this code, the process would
				either continue running after exit, or crash ungracefully */
			
			ESRI.ArcGIS.Utility.COMSupport.AOUninitialize.Shutdown();

			if(m_AoInitialize != null)
			{
				m_AoInitialize.Shutdown();
			}			
						
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CharacterizationForm));
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.menuItemFileExit = new System.Windows.Forms.MenuItem();
			this.menuItemTools = new System.Windows.Forms.MenuItem();
			this.menuItemToolsClearLog = new System.Windows.Forms.MenuItem();
			this.menuItemToolsSettings = new System.Windows.Forms.MenuItem();
			this.axMapControl = new ESRI.ArcGIS.MapControl.AxMapControl();
			this.btnPan = new System.Windows.Forms.Button();
			this.btnZoomOut = new System.Windows.Forms.Button();
			this.btnZoomIn = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnlCharacterizationView = new System.Windows.Forms.Panel();
			this.tabControlStudyArea = new System.Windows.Forms.TabControl();
			this.tabCatalogSelect = new System.Windows.Forms.TabPage();
			this.grdModelCatalog = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.pnlModelCatalogControls = new System.Windows.Forms.Panel();
			this.btnReconcileModel = new System.Windows.Forms.Button();
			this.btnAddModel = new System.Windows.Forms.Button();
			this.cmbSelectScenario = new System.Windows.Forms.ComboBox();
			this.lblScenario = new System.Windows.Forms.Label();
			this.tabInteractSelect = new System.Windows.Forms.TabPage();
			this.tabPolySelect = new System.Windows.Forms.TabPage();
			this.pnlStudyAreaControls = new System.Windows.Forms.Panel();
			this.btnAddStopLink = new System.Windows.Forms.Button();
			this.btnAddStartLink = new System.Windows.Forms.Button();
			this.lblStudyArea = new System.Windows.Forms.Label();
			this.txtStudyAreaName = new System.Windows.Forms.TextBox();
			this.btnPreviewTrace = new System.Windows.Forms.Button();
			this.btnRemoveStopLink = new System.Windows.Forms.Button();
			this.btnRemoveStartLink = new System.Windows.Forms.Button();
			this.btnClearList = new System.Windows.Forms.Button();
			this.lblStopLinks = new System.Windows.Forms.Label();
			this.lblStartLinks = new System.Windows.Forms.Label();
			this.lstStopLinks = new System.Windows.Forms.ListBox();
			this.lstStartLinks = new System.Windows.Forms.ListBox();
			this.btnCharacterize = new System.Windows.Forms.Button();
			this.tabControlOutputDisplay = new System.Windows.Forms.TabControl();
			this.tabMapView = new System.Windows.Forms.TabPage();
			this.pnlMapControls = new System.Windows.Forms.Panel();
			this.tabStatusView = new System.Windows.Forms.TabPage();
			this.statusTextBox = new System.Windows.Forms.RichTextBox();
			this.pnlStatusControl = new System.Windows.Forms.Panel();
			this.tabCharacterizationView = new System.Windows.Forms.TabPage();
			this.settings = new SystemsAnalysis.Characterization.Settings();
			this.btnSelectCharDir = new System.Windows.Forms.Button();
			this.txtCharOutputDir = new System.Windows.Forms.TextBox();
			this.axWebBrowser = new AxSHDocVw.AxWebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
			this.pnlCharacterizationView.SuspendLayout();
			this.tabControlStudyArea.SuspendLayout();
			this.tabCatalogSelect.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdModelCatalog)).BeginInit();
			this.pnlModelCatalogControls.SuspendLayout();
			this.pnlStudyAreaControls.SuspendLayout();
			this.tabControlOutputDisplay.SuspendLayout();
			this.tabMapView.SuspendLayout();
			this.pnlMapControls.SuspendLayout();
			this.tabStatusView.SuspendLayout();
			this.tabCharacterizationView.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.settings)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItemFile,
																					 this.menuItemTools});
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
			// menuItemTools
			// 
			this.menuItemTools.Index = 1;
			this.menuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.menuItemToolsClearLog,
																						  this.menuItemToolsSettings});
			this.menuItemTools.Text = "Tools";
			// 
			// menuItemToolsClearLog
			// 
			this.menuItemToolsClearLog.Index = 0;
			this.menuItemToolsClearLog.Text = "Clear Status Log";
			this.menuItemToolsClearLog.Click += new System.EventHandler(this.menuItemToolsClearLog_Click);
			// 
			// menuItemToolsSettings
			// 
			this.menuItemToolsSettings.Index = 1;
			this.menuItemToolsSettings.Text = "Settings";
			this.menuItemToolsSettings.Click += new System.EventHandler(this.menuItemToolsSettings_Click);
			// 
			// axMapControl
			// 
			this.axMapControl.ContainingControl = this;
			this.axMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axMapControl.Location = new System.Drawing.Point(0, 0);
			this.axMapControl.Name = "axMapControl";
			this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
			this.axMapControl.Size = new System.Drawing.Size(557, 479);
			this.axMapControl.TabIndex = 1;
			this.axMapControl.OnMouseDown += new ESRI.ArcGIS.MapControl.IMapControlEvents2_OnMouseDownEventHandler(this.axMapControl_OnMouseDown);
			// 
			// btnPan
			// 
			this.btnPan.Location = new System.Drawing.Point(200, 8);
			this.btnPan.Name = "btnPan";
			this.btnPan.TabIndex = 16;
			this.btnPan.Text = "Pan";
			this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
			// 
			// btnZoomOut
			// 
			this.btnZoomOut.Location = new System.Drawing.Point(112, 8);
			this.btnZoomOut.Name = "btnZoomOut";
			this.btnZoomOut.TabIndex = 15;
			this.btnZoomOut.Text = "Zoom Out";
			this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
			// 
			// btnZoomIn
			// 
			this.btnZoomIn.Location = new System.Drawing.Point(8, 8);
			this.btnZoomIn.Name = "btnZoomIn";
			this.btnZoomIn.Size = new System.Drawing.Size(96, 23);
			this.btnZoomIn.TabIndex = 14;
			this.btnZoomIn.Text = "Zoom In";
			this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(376, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 545);
			this.splitter1.TabIndex = 18;
			this.splitter1.TabStop = false;
			// 
			// pnlCharacterizationView
			// 
			this.pnlCharacterizationView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlCharacterizationView.Controls.Add(this.tabControlStudyArea);
			this.pnlCharacterizationView.Controls.Add(this.pnlStudyAreaControls);
			this.pnlCharacterizationView.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlCharacterizationView.Location = new System.Drawing.Point(0, 0);
			this.pnlCharacterizationView.Name = "pnlCharacterizationView";
			this.pnlCharacterizationView.Size = new System.Drawing.Size(376, 545);
			this.pnlCharacterizationView.TabIndex = 19;
			// 
			// tabControlStudyArea
			// 
			this.tabControlStudyArea.Controls.Add(this.tabCatalogSelect);
			this.tabControlStudyArea.Controls.Add(this.tabInteractSelect);
			this.tabControlStudyArea.Controls.Add(this.tabPolySelect);
			this.tabControlStudyArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlStudyArea.Location = new System.Drawing.Point(0, 0);
			this.tabControlStudyArea.Name = "tabControlStudyArea";
			this.tabControlStudyArea.SelectedIndex = 0;
			this.tabControlStudyArea.Size = new System.Drawing.Size(374, 351);
			this.tabControlStudyArea.TabIndex = 21;
			// 
			// tabCatalogSelect
			// 
			this.tabCatalogSelect.Controls.Add(this.grdModelCatalog);
			this.tabCatalogSelect.Controls.Add(this.pnlModelCatalogControls);
			this.tabCatalogSelect.Location = new System.Drawing.Point(4, 22);
			this.tabCatalogSelect.Name = "tabCatalogSelect";
			this.tabCatalogSelect.Size = new System.Drawing.Size(366, 325);
			this.tabCatalogSelect.TabIndex = 0;
			this.tabCatalogSelect.Text = "Model Catalog";
			// 
			// grdModelCatalog
			// 
			this.grdModelCatalog.AllowNavigation = false;
			this.grdModelCatalog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.grdModelCatalog.DataMember = "";
			this.grdModelCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdModelCatalog.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.grdModelCatalog.Location = new System.Drawing.Point(0, 0);
			this.grdModelCatalog.Name = "grdModelCatalog";
			this.grdModelCatalog.ReadOnly = true;
			this.grdModelCatalog.RowHeadersVisible = false;
			this.grdModelCatalog.Size = new System.Drawing.Size(366, 261);
			this.grdModelCatalog.TabIndex = 21;
			this.grdModelCatalog.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																										this.dataGridTableStyle1});
			this.grdModelCatalog.Click += new System.EventHandler(this.grdModelCatalog_Click);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = this.grdModelCatalog;
			this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.dataGridTextBoxColumn1,
																												  this.dataGridTextBoxColumn2,
																												  this.dataGridTextBoxColumn3,
																												  this.dataGridTextBoxColumn4,
																												  this.dataGridTextBoxColumn5,
																												  this.dataGridTextBoxColumn6});
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "ModelCatalog";
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.HeaderText = "ID";
			this.dataGridTextBoxColumn1.MappingName = "modelID";
			this.dataGridTextBoxColumn1.Width = 25;
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.HeaderText = "Model";
			this.dataGridTextBoxColumn2.MappingName = "modelName";
			this.dataGridTextBoxColumn2.Width = 75;
			// 
			// dataGridTextBoxColumn3
			// 
			this.dataGridTextBoxColumn3.Format = "";
			this.dataGridTextBoxColumn3.FormatInfo = null;
			this.dataGridTextBoxColumn3.HeaderText = "Study Area";
			this.dataGridTextBoxColumn3.MappingName = "studyArea";
			this.dataGridTextBoxColumn3.ReadOnly = true;
			this.dataGridTextBoxColumn3.Width = 150;
			// 
			// dataGridTextBoxColumn4
			// 
			this.dataGridTextBoxColumn4.Format = "";
			this.dataGridTextBoxColumn4.FormatInfo = null;
			this.dataGridTextBoxColumn4.HeaderText = "Modeler";
			this.dataGridTextBoxColumn4.MappingName = "userName";
			this.dataGridTextBoxColumn4.Width = 75;
			// 
			// dataGridTextBoxColumn5
			// 
			this.dataGridTextBoxColumn5.Format = "";
			this.dataGridTextBoxColumn5.FormatInfo = null;
			this.dataGridTextBoxColumn5.HeaderText = "Date";
			this.dataGridTextBoxColumn5.MappingName = "uploadDate";
			this.dataGridTextBoxColumn5.Width = 75;
			// 
			// dataGridTextBoxColumn6
			// 
			this.dataGridTextBoxColumn6.Format = "";
			this.dataGridTextBoxColumn6.FormatInfo = null;
			this.dataGridTextBoxColumn6.HeaderText = "Path";
			this.dataGridTextBoxColumn6.MappingName = "modelPath";
			this.dataGridTextBoxColumn6.Width = 300;
			// 
			// pnlModelCatalogControls
			// 
			this.pnlModelCatalogControls.Controls.Add(this.btnReconcileModel);
			this.pnlModelCatalogControls.Controls.Add(this.btnAddModel);
			this.pnlModelCatalogControls.Controls.Add(this.cmbSelectScenario);
			this.pnlModelCatalogControls.Controls.Add(this.lblScenario);
			this.pnlModelCatalogControls.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlModelCatalogControls.Location = new System.Drawing.Point(0, 261);
			this.pnlModelCatalogControls.Name = "pnlModelCatalogControls";
			this.pnlModelCatalogControls.Size = new System.Drawing.Size(366, 64);
			this.pnlModelCatalogControls.TabIndex = 22;
			// 
			// btnReconcileModel
			// 
			this.btnReconcileModel.Location = new System.Drawing.Point(192, 40);
			this.btnReconcileModel.Name = "btnReconcileModel";
			this.btnReconcileModel.Size = new System.Drawing.Size(120, 23);
			this.btnReconcileModel.TabIndex = 21;
			this.btnReconcileModel.Text = "Reconcile Model";
			this.btnReconcileModel.Click += new System.EventHandler(this.btnReconcileModel_Click);
			// 
			// btnAddModel
			// 
			this.btnAddModel.Location = new System.Drawing.Point(64, 40);
			this.btnAddModel.Name = "btnAddModel";
			this.btnAddModel.Size = new System.Drawing.Size(120, 23);
			this.btnAddModel.TabIndex = 20;
			this.btnAddModel.Text = "Add Start/Stop Links";
			this.btnAddModel.Click += new System.EventHandler(this.btnAddModel_Click);
			// 
			// cmbSelectScenario
			// 
			this.cmbSelectScenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSelectScenario.Location = new System.Drawing.Point(64, 8);
			this.cmbSelectScenario.Name = "cmbSelectScenario";
			this.cmbSelectScenario.Size = new System.Drawing.Size(272, 21);
			this.cmbSelectScenario.TabIndex = 18;
			this.cmbSelectScenario.SelectedIndexChanged += new System.EventHandler(this.cmbSelectScenario_SelectedIndexChanged);
			// 
			// lblScenario
			// 
			this.lblScenario.Location = new System.Drawing.Point(8, 8);
			this.lblScenario.Name = "lblScenario";
			this.lblScenario.Size = new System.Drawing.Size(56, 23);
			this.lblScenario.TabIndex = 19;
			this.lblScenario.Text = "Scenario:";
			// 
			// tabInteractSelect
			// 
			this.tabInteractSelect.Location = new System.Drawing.Point(4, 22);
			this.tabInteractSelect.Name = "tabInteractSelect";
			this.tabInteractSelect.Size = new System.Drawing.Size(366, 325);
			this.tabInteractSelect.TabIndex = 1;
			this.tabInteractSelect.Text = "Interactive Select";
			// 
			// tabPolySelect
			// 
			this.tabPolySelect.Location = new System.Drawing.Point(4, 22);
			this.tabPolySelect.Name = "tabPolySelect";
			this.tabPolySelect.Size = new System.Drawing.Size(366, 325);
			this.tabPolySelect.TabIndex = 2;
			this.tabPolySelect.Text = "Polygon Select";
			// 
			// pnlStudyAreaControls
			// 
			this.pnlStudyAreaControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlStudyAreaControls.Controls.Add(this.btnAddStopLink);
			this.pnlStudyAreaControls.Controls.Add(this.btnAddStartLink);
			this.pnlStudyAreaControls.Controls.Add(this.lblStudyArea);
			this.pnlStudyAreaControls.Controls.Add(this.txtStudyAreaName);
			this.pnlStudyAreaControls.Controls.Add(this.btnPreviewTrace);
			this.pnlStudyAreaControls.Controls.Add(this.btnRemoveStopLink);
			this.pnlStudyAreaControls.Controls.Add(this.btnRemoveStartLink);
			this.pnlStudyAreaControls.Controls.Add(this.btnClearList);
			this.pnlStudyAreaControls.Controls.Add(this.lblStopLinks);
			this.pnlStudyAreaControls.Controls.Add(this.lblStartLinks);
			this.pnlStudyAreaControls.Controls.Add(this.lstStopLinks);
			this.pnlStudyAreaControls.Controls.Add(this.lstStartLinks);
			this.pnlStudyAreaControls.Controls.Add(this.btnCharacterize);
			this.pnlStudyAreaControls.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlStudyAreaControls.Location = new System.Drawing.Point(0, 351);
			this.pnlStudyAreaControls.Name = "pnlStudyAreaControls";
			this.pnlStudyAreaControls.Size = new System.Drawing.Size(374, 192);
			this.pnlStudyAreaControls.TabIndex = 22;
			// 
			// btnAddStopLink
			// 
			this.btnAddStopLink.Location = new System.Drawing.Point(224, 152);
			this.btnAddStopLink.Name = "btnAddStopLink";
			this.btnAddStopLink.Size = new System.Drawing.Size(56, 23);
			this.btnAddStopLink.TabIndex = 12;
			this.btnAddStopLink.Text = "Add";
			this.btnAddStopLink.Click += new System.EventHandler(this.btnAddStopLink_Click);
			// 
			// btnAddStartLink
			// 
			this.btnAddStartLink.Location = new System.Drawing.Point(96, 152);
			this.btnAddStartLink.Name = "btnAddStartLink";
			this.btnAddStartLink.Size = new System.Drawing.Size(56, 23);
			this.btnAddStartLink.TabIndex = 11;
			this.btnAddStartLink.Text = "Add";
			this.btnAddStartLink.Click += new System.EventHandler(this.btnAddStartLink_Click);
			// 
			// lblStudyArea
			// 
			this.lblStudyArea.Location = new System.Drawing.Point(24, 16);
			this.lblStudyArea.Name = "lblStudyArea";
			this.lblStudyArea.Size = new System.Drawing.Size(64, 23);
			this.lblStudyArea.TabIndex = 10;
			this.lblStudyArea.Text = "Study Area:";
			// 
			// txtStudyAreaName
			// 
			this.txtStudyAreaName.Location = new System.Drawing.Point(96, 16);
			this.txtStudyAreaName.Name = "txtStudyAreaName";
			this.txtStudyAreaName.Size = new System.Drawing.Size(248, 20);
			this.txtStudyAreaName.TabIndex = 9;
			this.txtStudyAreaName.Text = "";
			// 
			// btnPreviewTrace
			// 
			this.btnPreviewTrace.Location = new System.Drawing.Point(8, 64);
			this.btnPreviewTrace.Name = "btnPreviewTrace";
			this.btnPreviewTrace.Size = new System.Drawing.Size(80, 23);
			this.btnPreviewTrace.TabIndex = 8;
			this.btnPreviewTrace.Text = "Preview";
			this.btnPreviewTrace.Click += new System.EventHandler(this.btnPreviewTrace_Click);
			// 
			// btnRemoveStopLink
			// 
			this.btnRemoveStopLink.Location = new System.Drawing.Point(288, 152);
			this.btnRemoveStopLink.Name = "btnRemoveStopLink";
			this.btnRemoveStopLink.Size = new System.Drawing.Size(56, 23);
			this.btnRemoveStopLink.TabIndex = 7;
			this.btnRemoveStopLink.Text = "Remove";
			this.btnRemoveStopLink.Click += new System.EventHandler(this.btnRemoveStopLink_Click);
			// 
			// btnRemoveStartLink
			// 
			this.btnRemoveStartLink.Location = new System.Drawing.Point(160, 152);
			this.btnRemoveStartLink.Name = "btnRemoveStartLink";
			this.btnRemoveStartLink.Size = new System.Drawing.Size(56, 23);
			this.btnRemoveStartLink.TabIndex = 6;
			this.btnRemoveStartLink.Text = "Remove";
			this.btnRemoveStartLink.Click += new System.EventHandler(this.btnRemoveStartLink_Click);
			// 
			// btnClearList
			// 
			this.btnClearList.Location = new System.Drawing.Point(8, 128);
			this.btnClearList.Name = "btnClearList";
			this.btnClearList.Size = new System.Drawing.Size(80, 23);
			this.btnClearList.TabIndex = 5;
			this.btnClearList.Text = "Clear Links";
			this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
			// 
			// lblStopLinks
			// 
			this.lblStopLinks.Location = new System.Drawing.Point(224, 48);
			this.lblStopLinks.Name = "lblStopLinks";
			this.lblStopLinks.Size = new System.Drawing.Size(100, 16);
			this.lblStopLinks.TabIndex = 4;
			this.lblStopLinks.Text = "Stop Links";
			// 
			// lblStartLinks
			// 
			this.lblStartLinks.Location = new System.Drawing.Point(96, 48);
			this.lblStartLinks.Name = "lblStartLinks";
			this.lblStartLinks.Size = new System.Drawing.Size(100, 16);
			this.lblStartLinks.TabIndex = 3;
			this.lblStartLinks.Text = "Start Links";
			// 
			// lstStopLinks
			// 
			this.lstStopLinks.Location = new System.Drawing.Point(224, 64);
			this.lstStopLinks.Name = "lstStopLinks";
			this.lstStopLinks.Size = new System.Drawing.Size(120, 82);
			this.lstStopLinks.TabIndex = 2;
			// 
			// lstStartLinks
			// 
			this.lstStartLinks.Location = new System.Drawing.Point(96, 64);
			this.lstStartLinks.Name = "lstStartLinks";
			this.lstStartLinks.Size = new System.Drawing.Size(120, 82);
			this.lstStartLinks.TabIndex = 1;
			// 
			// btnCharacterize
			// 
			this.btnCharacterize.Location = new System.Drawing.Point(8, 96);
			this.btnCharacterize.Name = "btnCharacterize";
			this.btnCharacterize.Size = new System.Drawing.Size(80, 23);
			this.btnCharacterize.TabIndex = 0;
			this.btnCharacterize.Text = "Characterize";
			this.btnCharacterize.Click += new System.EventHandler(this.btnCharacterize_Click);
			// 
			// tabControlOutputDisplay
			// 
			this.tabControlOutputDisplay.Controls.Add(this.tabMapView);
			this.tabControlOutputDisplay.Controls.Add(this.tabStatusView);
			this.tabControlOutputDisplay.Controls.Add(this.tabCharacterizationView);
			this.tabControlOutputDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlOutputDisplay.Location = new System.Drawing.Point(379, 0);
			this.tabControlOutputDisplay.Name = "tabControlOutputDisplay";
			this.tabControlOutputDisplay.SelectedIndex = 0;
			this.tabControlOutputDisplay.Size = new System.Drawing.Size(565, 545);
			this.tabControlOutputDisplay.TabIndex = 20;
			// 
			// tabMapView
			// 
			this.tabMapView.Controls.Add(this.axMapControl);
			this.tabMapView.Controls.Add(this.pnlMapControls);
			this.tabMapView.Location = new System.Drawing.Point(4, 22);
			this.tabMapView.Name = "tabMapView";
			this.tabMapView.Size = new System.Drawing.Size(557, 519);
			this.tabMapView.TabIndex = 0;
			this.tabMapView.Text = "Map View";
			// 
			// pnlMapControls
			// 
			this.pnlMapControls.Controls.Add(this.btnZoomIn);
			this.pnlMapControls.Controls.Add(this.btnZoomOut);
			this.pnlMapControls.Controls.Add(this.btnPan);
			this.pnlMapControls.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlMapControls.Location = new System.Drawing.Point(0, 479);
			this.pnlMapControls.Name = "pnlMapControls";
			this.pnlMapControls.Size = new System.Drawing.Size(557, 40);
			this.pnlMapControls.TabIndex = 0;
			// 
			// tabStatusView
			// 
			this.tabStatusView.Controls.Add(this.statusTextBox);
			this.tabStatusView.Controls.Add(this.pnlStatusControl);
			this.tabStatusView.Location = new System.Drawing.Point(4, 22);
			this.tabStatusView.Name = "tabStatusView";
			this.tabStatusView.Size = new System.Drawing.Size(557, 519);
			this.tabStatusView.TabIndex = 0;
			this.tabStatusView.Text = "Status Log";
			// 
			// statusTextBox
			// 
			this.statusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusTextBox.Location = new System.Drawing.Point(0, 0);
			this.statusTextBox.Name = "statusTextBox";
			this.statusTextBox.Size = new System.Drawing.Size(557, 419);
			this.statusTextBox.TabIndex = 1;
			this.statusTextBox.Text = "";
			// 
			// pnlStatusControl
			// 
			this.pnlStatusControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlStatusControl.Location = new System.Drawing.Point(0, 419);
			this.pnlStatusControl.Name = "pnlStatusControl";
			this.pnlStatusControl.Size = new System.Drawing.Size(557, 100);
			this.pnlStatusControl.TabIndex = 0;
			// 
			// tabCharacterizationView
			// 
			this.tabCharacterizationView.Controls.Add(this.axWebBrowser);
			this.tabCharacterizationView.Location = new System.Drawing.Point(4, 22);
			this.tabCharacterizationView.Name = "tabCharacterizationView";
			this.tabCharacterizationView.Size = new System.Drawing.Size(557, 519);
			this.tabCharacterizationView.TabIndex = 1;
			this.tabCharacterizationView.Text = "Characterization View";
			// 
			// settings
			// 
			this.settings.DataSetName = "Settings";
			this.settings.EnforceConstraints = false;
			this.settings.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// btnSelectCharDir
			// 
			this.btnSelectCharDir.Location = new System.Drawing.Point(0, 0);
			this.btnSelectCharDir.Name = "btnSelectCharDir";
			this.btnSelectCharDir.TabIndex = 0;
			// 
			// txtCharOutputDir
			// 
			this.txtCharOutputDir.Location = new System.Drawing.Point(0, 0);
			this.txtCharOutputDir.Name = "txtCharOutputDir";
			this.txtCharOutputDir.TabIndex = 0;
			this.txtCharOutputDir.Text = "";
			// 
			// axWebBrowser
			// 
			this.axWebBrowser.ContainingControl = this;
			this.axWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axWebBrowser.Enabled = true;
			this.axWebBrowser.Location = new System.Drawing.Point(0, 0);
			this.axWebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser.OcxState")));
			this.axWebBrowser.Size = new System.Drawing.Size(557, 519);
			this.axWebBrowser.TabIndex = 0;
			// 
			// CharacterizationForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(944, 545);
			this.Controls.Add(this.tabControlOutputDisplay);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.pnlCharacterizationView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.Name = "CharacterizationForm";
			this.Text = "Characterization Queries Manager";
			this.Load += new System.EventHandler(this.CharacterizationForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
			this.pnlCharacterizationView.ResumeLayout(false);
			this.tabControlStudyArea.ResumeLayout(false);
			this.tabCatalogSelect.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdModelCatalog)).EndInit();
			this.pnlModelCatalogControls.ResumeLayout(false);
			this.pnlStudyAreaControls.ResumeLayout(false);
			this.tabControlOutputDisplay.ResumeLayout(false);
			this.tabMapView.ResumeLayout(false);
			this.pnlMapControls.ResumeLayout(false);
			this.tabStatusView.ResumeLayout(false);
			this.tabCharacterizationView.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.settings)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new CharacterizationForm());			
		}

		private void CharacterizationForm_Load(object sender, System.EventArgs e)
		{
			this.Show();
			this.setupMapControl();
		}

		#region Menu item event handlers
		private void menuItemFileExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();			
		}
		
		#endregion

		#region Model Catalog panel command button actions
		private void btnCharacterize_Click(object sender, System.EventArgs e)
		{				
			string studyArea;			
			string outputFile;
			
			try
			{
				studyArea = this.txtStudyAreaName.Text;				
								
				this.AddInfoStatus("Characterizing study area '" + studyArea + "'.");

				if (studyArea == null || studyArea.Length <= 0)
				{
					throw new Exception("You must enter a Study Area.");						
				}						
			}
			catch (Exception ex)
			{
				this.AddErrorStatus(ex.Message);
				return;
			}

			try
			{
				mstLinks.ClearSubNetwork();			
				this.AddInfoStatus("Performing trace.");
				int traceCount = mstLinks.SelectSubNetwork(this.startLinks, this.stopLinks);	
				if (traceCount <= 0)
				{
					throw new Exception("Error: No links were selected. Please verify start links.");								
				}
				this.AddInfoStatus("Traced " + traceCount + " links.");
			}
			catch (Exception ex)
			{
				this.AddErrorStatus(ex.Message);
				return;

			}
										
			this.AddInfoStatus("Creating characterization engine.");		
			Links charLinks;									
			Characterization.CharacterizationEngine charEngine;		
	
			try
			{
				charLinks = mstLinks.GetSubNetwork();			

				charEngine = new CharacterizationEngine(charLinks, studyArea, 
					settings);
			
				charEngine.StatusChanged +=
					new Utils.Events.OnStatusChangedEventHandler(this.OnStatusChanged);				
				
				charEngine.LoadCharacterizationTables();				
				outputFile = charEngine.Characterize();
			}
			catch (Exception ex)
			{
				this.AddErrorStatus(ex.Message);
				return;
			}

			tabControlOutputDisplay.SelectedTab = tabCharacterizationView;	
					
			object blah = new object();
			axWebBrowser.Navigate(outputFile, ref blah, ref blah, ref blah, ref blah );	
			if (System.IO.File.Exists(outputFile))
			{
				this.AddInfoStatus("Wrote output file: '" + outputFile + "'.");
				this.AddInfoStatus("** Characterization Complete! **");
			}
			else
			{
				this.AddErrorStatus("Characterization incomplete! " + 
					"Could not create output file: '" + outputFile + "'.");
			}								
		}	
	
		private void btnPreviewTrace_Click(object sender, System.EventArgs e)
		{
			mstLinks.ClearSubNetwork();
			mstLinks.SelectSubNetwork(startLinks, stopLinks);	
	
			ESRI.ArcGIS.Carto.IFeatureLayer pFeatLayer;
			pFeatLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)axMapControl.get_Layer(0);

			int[] selectedEdgeIDArray = mstLinks.GetSelectedEdgeIDArray();			
			Utils.ArcObjects.MapControl.MapControlHelper.ZoomToOIDArray(
				axMapControl, pFeatLayer, selectedEdgeIDArray);	
			tabControlOutputDisplay.SelectedTab = tabMapView;
		}

		private void btnClearList_Click(object sender, System.EventArgs e)
		{
			mstLinks.ClearSubNetwork();
			startLinks.Clear();
			lstStartLinks.Items.Clear();
			stopLinks.Clear();						
			lstStopLinks.Items.Clear();
			ESRI.ArcGIS.Carto.IFeatureLayer pFeatLayer;
			pFeatLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)axMapControl.get_Layer(0);
			Utils.ArcObjects.MapControl.MapControlHelper.ClearSelection(
				axMapControl, pFeatLayer);
			
		}
		#endregion
		
		#region Map panel command button actions
				
		private void btnSelectFeatures_Click(object sender, System.EventArgs e)
		{
			axMapControl.CurrentTool = selectCommand;
		}
		private void btnClearSelection_Click(object sender, System.EventArgs e)
		{
			clearCommand.OnClick();
		}
		private void btnZoomIn_Click(object sender, System.EventArgs e)
		{
			axMapControl.CurrentTool = zoomInCommand;
		}
		private void btnZoomOut_Click(object sender, System.EventArgs e)
		{
			axMapControl.CurrentTool = zoomOutCommand;
		}
		private void btnPan_Click(object sender, System.EventArgs e)
		{
			axMapControl.CurrentTool = panCommand;
		}
		#endregion

		private void setupMapControl()
		{
			selectCommand = 
				new SystemsAnalysis.Utils.ArcObjects.MapControl.SelectFeatures();
			selectCommand.OnCreate(axMapControl.Object);
			axMapControl.CurrentTool = selectCommand;
			clearCommand = 
				new SystemsAnalysis.Utils.ArcObjects.MapControl.ClearFeatureSelection();
			clearCommand.OnCreate(axMapControl.Object);
			panCommand = 
				new SystemsAnalysis.Utils.ArcObjects.MapControl.Pan();
			panCommand.OnCreate(axMapControl.Object);
			zoomInCommand = 
				new SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomIn();
			zoomInCommand.OnCreate(axMapControl.Object);
			zoomOutCommand = 
				new SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomOut();
			zoomOutCommand.OnCreate(axMapControl.Object);						

			ESRI.ArcGIS.Carto.IFeatureLayer pFL;
			pFL = (ESRI.ArcGIS.Carto.FeatureLayer)							
				CGIS.MapWorks.EsriUtils.LoadLayerFile(settings.DataSource.FindByName("MstLinks").DataSource_Text, axMapControl);		
			
			mstLinks = new Links(agMasterDataSet1.mst_links, "EX");			
		}
		
		
		private void axMapControl_OnMouseDown(
			object sender, ESRI.ArcGIS.MapControl.IMapControlEvents2_OnMouseDownEvent e)
		{			
		
		}

		private void cmbSelectScenario_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataRowView selectedRowView = (DataRowView)cmbSelectScenario.SelectedItem;
			int selectedScenario = (int)selectedRowView.Row["scenarioID"];
			modelCatalogView.RowFilter = "scenarioID="+selectedScenario;
		}		

		private void btnAddModel_Click(object sender, System.EventArgs e)
		{			
			int selectedModelID = (int)grdModelCatalog[
						grdModelCatalog.CurrentCell.RowNumber, 0];
			DataAccess.ModelCatalogDataSet.ModelCatalogRow selectedModelRow;
			selectedModelRow = modelCatalogDataSet1.ModelCatalog.FindBymodelID(selectedModelID);

			this.AddInfoStatus("Reading start/stop links from model.ini for model: '"
				+ selectedModelRow.modelName + "'.");
			
			try
			{
				selectedModel = new Model(selectedModelRow.modelPath);
			}
			catch
			{
				this.AddErrorStatus(e.ToString());
			}
			
			if (startLinks.Count == 0 & startLinks.Count == 0)
			{
				this.txtStudyAreaName.Text = selectedModelRow.studyArea;
			}
			else
			{
				this.txtStudyAreaName.Text = "";
			}

			try
			{
				foreach (Link l in selectedModel.StartLinks.Values)
				{
					Link startLink;
					startLink = mstLinks.FindByMLinkID(l.MLinkID);
					if (startLink != null)
					{
						this.startLinks.Add(startLink);
					}
					else
					{
						this.AddWarningStatus("Start link missing from master database. MLinkID = " + l.MLinkID);
					}
					//TODO: Log missing start link
				}								
				foreach (Link l in selectedModel.StopLinks.Values)
				{
					Link stopLink;
					stopLink = mstLinks.FindByMLinkID(l.MLinkID);
					if (stopLink != null)
					{
						this.stopLinks.Add(stopLink);
					}
					else
					{
						this.AddWarningStatus(
							"Stop link missing from master database. MLinkID = " + l.MLinkID);
					}
					//TODO: Log missing stop link				
				}
			}
			catch (Exception ex)
			{
				this.startLinks.Clear();
				this.stopLinks.Clear();				
				this.AddErrorStatus(ex.Message);				
			}
		}

		#region Methods to manage start link and stop link list boxes
		private void startLinks_OnAddedLink(object sender, Links.LinkChangedEventArgs e)
		{
			Link addedLink = (Link)e.ChangedLink;
			lstStartLinks.Items.Add(addedLink);
		}
		private void startLinks_OnRemovedLink(object sender, Links.LinkChangedEventArgs e)
		{
			Link removedLink = (Link)e.ChangedLink;
			lstStartLinks.Items.Remove(removedLink);
		}
		private void stopLinks_OnAddedLink(object sender, Links.LinkChangedEventArgs e)
		{
			Link addedLink = (Link)e.ChangedLink;
			lstStopLinks.Items.Add(addedLink);
		}
		private void stopLinks_OnRemovedLink(object sender, Links.LinkChangedEventArgs e)
		{
			Link removedLink = (Link)e.ChangedLink;
			lstStopLinks.Items.Remove(removedLink);
		}
		private void btnRemoveStartLink_Click(object sender, System.EventArgs e)
		{
			Link linkToRemove = (Link)lstStartLinks.SelectedItem;			
			startLinks.Remove(linkToRemove.LinkID);
		}
		private void btnAddStartLink_Click(object sender, System.EventArgs e)
		{
			InputBoxResult result;
			result = InputBox.Show("Enter an MLinkID:", "Add StartLink", "", null, -1, -1);
			int mLinkID;
			if (result.OK)
			{
				try 
				{ 
					mLinkID = Int32.Parse(result.Text); 
					Link l = mstLinks.FindByMLinkID(mLinkID);
					if (l != null)
					{
						startLinks.Add(l);
					}
					else
					{
						MessageBox.Show("Could not find MLinkID in MstLinks.", "MLinkID Not Found");
					}
				}
				catch 
				{ MessageBox.Show("You must enter a valid MLinkID.", "Invalid Input"); }	
			}					
		}
		private void btnRemoveStopLink_Click(object sender, System.EventArgs e)
		{			
			Link linkToRemove = (Link)lstStopLinks.SelectedItem;
			stopLinks.Remove(linkToRemove.LinkID);
		}
		private void btnAddStopLink_Click(object sender, System.EventArgs e)
		{
			InputBoxResult result;
			result = InputBox.Show("Enter an MLinkID:", "Add StopLink", "", null, -1, -1);
			int mLinkID;
			if (result.OK)
			{
				try 
				{ 
					mLinkID = Int32.Parse(result.Text); 
					Link l = mstLinks.FindByMLinkID(mLinkID);
					if (l != null)
					{
						stopLinks.Add(l);
					}
					else
					{
						MessageBox.Show("Could not find MLinkID in MstLinks.", "MLinkID Not Found");
					}
				}
				catch 
				{ MessageBox.Show("You must enter a valid MLinkID.", "Invalid Input"); }	
			}
		
		}
		#endregion

		private void OnStatusChanged(Utils.Events.StatusChangedArgs e)
		{			
			this.AddInfoStatus(e.NewStatus);					
		}

		private void menuItemToolsClearLog_Click(object sender, System.EventArgs e)
		{
			this.ClearStatusText();
		}

		private void btnReconcileModel_Click(object sender, System.EventArgs e)
		{	
			this.tabControlOutputDisplay.SelectedTab = this.tabStatusView;	
			this.AddInfoStatus("Attempting to reconcile model to master data.");
			if (this.selectedModel == null)
			{				
				this.AddErrorStatus("No model selected.");
			}			
			Links modelLinks = this.selectedModel.ModelLinks;
			Nodes modelNodes = this.selectedModel.ModelNodes;
			DSCs modelDSCs = this.selectedModel.ModelDSCs;

			Links reconcileStartLinks = new Links();
			Links reconcileStopLinks = new Links();
			#region Translate model start/stop links to master start/stop links			
			foreach (Link l in selectedModel.StartLinks.Values)
			{
				Link startLink;
				startLink = mstLinks.FindByMLinkID(l.MLinkID);
				if (startLink != null)
				{
					reconcileStartLinks.Add(startLink);
				}
				else
				{
					this.AddWarningStatus("Start link missing from master database. MLinkID = " + l.MLinkID);
				}
				//TODO: Log missing start link
			}								
			foreach (Link l in selectedModel.StopLinks.Values)
			{
				Link stopLink;
				stopLink = mstLinks.FindByMLinkID(l.MLinkID);
				if (stopLink != null)
				{
					reconcileStopLinks.Add(stopLink);
				}
				else
				{
					this.AddWarningStatus(
						"Stop link missing from master database. MLinkID = " + l.MLinkID);
				}
				//TODO: Log missing stop link				
			}
			#endregion

			this.mstLinks.ClearSubNetwork();			
			this.AddInfoStatus("Performing trace.");
			int traceCount = mstLinks.SelectSubNetwork(
				reconcileStartLinks, reconcileStopLinks);
			if (traceCount <= 0)
			{
				throw new Exception("Error: No links were selected. Please verify start links.");								
			}
			this.AddInfoStatus("Traced " + traceCount + " links.");
			Links charLinks = mstLinks.GetSubNetwork();
			Nodes charNodes = new Nodes();
			charNodes.Load(charLinks, settings.DataSource.FindByName("AGMasterDB").DataSource_Text);
			this.AddInfoStatus("Traced " + charNodes.Count + " nodes.");
			DSCs charDSCs = new DSCs();
			charDSCs.Load(charLinks, settings.DataSource.FindByName("AGMasterDB").DataSource_Text);
			this.AddInfoStatus("Traced " + charDSCs.Count + " direct subcatchments.");
			this.AddInfoStatus("*******************************************************");
			this.AddInfoStatus("*		Model		Trace *");
			this.AddInfoStatus("*	Links:	" + modelLinks.Count + "	" + charLinks.Count + " *");
			this.AddInfoStatus("*	Nodes:	" + modelNodes.Count + "	" + charNodes.Count + " *");
			this.AddInfoStatus("*	DSCs:	" + modelDSCs.Count + "	" + charDSCs.Count + " *");
			this.AddInfoStatus("*******************************************************");
			foreach (Link l in modelLinks.Values)
			{
				if (l.MLinkID != 0 && charLinks.FindByMLinkID(l.MLinkID) == null)
				{
					this.AddWarningStatus("MLinkID " + l.MLinkID + " missing from new trace.");
				}
			}
			foreach (Link l in charLinks.Values)
			{
				if (l.MLinkID != 0 && modelLinks.FindByMLinkID(l.MLinkID) == null)
				{
					this.AddWarningStatus("MLinkID " + l.MLinkID + " does not exist in model.");
				}
			}
			foreach (Node n in modelNodes.Values)
			{
				if (n.Name.IndexOf("SPL") == -1 && !charNodes.Contains(n.Name))
				{
					this.AddWarningStatus("Node " + n.Name + " missing from new trace.");
				}
			}
			foreach (Node n in charNodes.Values)
			{
				if (!modelNodes.Contains(n.Name))
				{
					this.AddWarningStatus("Node " + n.Name+ " does not exist in model.");
				}			
			}
			foreach (DSC d in modelDSCs.Values)
			{
				if (!charDSCs.Contains(d.DSCID))
				{
					this.AddWarningStatus("DSC " + d.DSCID + " missing from new trace.");
				}
			}
			foreach (DSC d in charDSCs.Values)
			{
				if (!modelDSCs.Contains(d.DSCID))
				{
					this.AddWarningStatus("DSC " + d.DSCID + " does not exist in model.");
				}
			}
			this.AddInfoStatus("Reconciliation Complete.");
		}
	
		private void grdModelCatalog_Click(object sender, System.EventArgs e)
		{			
			this.selectedModel = this.getSelectedModel();
			return;			
		}

		private Model getSelectedModel()
		{		
			try
			{
				int selectedModelID = (int)grdModelCatalog[
					grdModelCatalog.CurrentCell.RowNumber, 0];
				DataAccess.ModelCatalogDataSet.ModelCatalogRow selectedModelRow;
				selectedModelRow = modelCatalogDataSet1.ModelCatalog.FindBymodelID(selectedModelID);
				if (selectedModel != null && selectedModel.Path != selectedModelRow.modelPath)
				{
					return this.selectedModel;	
				}
				else
				{
					return new Model(selectedModelRow.modelPath);
				}
			}
			catch (Exception ex)
			{
				this.AddErrorStatus(ex.Message);
				return null;
			}
		}

		#region Status Window
		/// <summary>
		/// Adds text to the Status Form display.
		/// </summary>
		/// <param name="status">The string to be added to the display. The newline
		/// character will be automatically appended to the end of this string
		/// if it is not already present.</param>
		public void AddInfoStatus(string status)
		{	
			this.statusTextBox.AppendText(System.DateTime.Now + ": ");
			this.statusTextBox.AppendText(status);
			if (!status.EndsWith("\n"))
			{
				this.statusTextBox.AppendText("\n");
			}
			Application.DoEvents();
			this.statusTextBox.Focus();
			this.statusTextBox.ScrollToCaret();
			this.Refresh();
			Application.DoEvents();
		}
		/// <summary>
		/// Adds warning text to the Status Form display.
		/// </summary>
		/// <param name="status">The warning message to add.</param>
		public void AddWarningStatus(string status)
		{
			this.statusTextBox.SelectionColor = Color.DarkOrange;
			this.AddInfoStatus(status);
			this.statusTextBox.SelectionColor = Color.Black;
		}
		/// <summary>
		/// Adds error text to the Status Form display.
		/// </summary>
		/// <param name="status">The error message to add.</param>
		public void AddErrorStatus(string status)
		{
			this.statusTextBox.SelectionColor = Color.Red;
			this.AddInfoStatus(status);
			this.statusTextBox.SelectionColor = Color.Black;
			this.tabControlOutputDisplay.SelectedTab = this.tabStatusView;
		}
		/// <summary>
		/// Adds text to the Status Form display.
		/// </summary>
		/// <param name="status">The string to be added to the display. The newline
		/// character will be automatically appended to the end of this string
		/// if it is not already present.</param>
		/// <param name="newLine">Specifies whether a line break should be appended
		/// to the end of the status text.</param>
		public void AddInfoStatus(string status, bool newLine)
		{
			if (newLine)
			{
				this.AddInfoStatus(status);
			}
			else
			{
				this.statusTextBox.AppendText(status);
				this.Refresh();
				Application.DoEvents();
			}
			return;
		}			
		/// <summary>
		/// Clears the Status Form display.
		/// </summary>
		public void ClearStatusText()
		{
			this.statusTextBox.Clear();
		}
		#endregion

		private void menuItemToolsSettings_Click(object sender, System.EventArgs e)
		{
			if (pref == null)
			{
				pref = new Preferences("\\\\Cassio\\Modeling\\Model_Programs\\ResultsWarehouse\\CharacterizationQueriesManager\\Default_Settings.xml");						
			}
			if (pref.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.settings = pref.Settings;
				this.loadMasterData();
			}
		}				

		private void loadMasterData()
		{
			this.AddInfoStatus("Loading model catalog data from '" + settings.DataSource.FindByName("ResultsWarehouseDB").DataSource_Text + "'.");
			try
			{
				modelCatalogDataSet1 = new DataAccess.ModelCatalogDataSet();
				modelCatalogDataAccess1 = new DataAccess.ModelCatalogDataAccess(settings.DataSource.FindByName("ResultsWarehouseDB").DataSource_Text);
				modelCatalogDataAccess1.StormCatalogDataAdapter.Fill(modelCatalogDataSet1);
				modelCatalogDataAccess1.ModelScenarioDataAdapter.Fill(modelCatalogDataSet1);
				modelCatalogDataAccess1.ModelCatalogDataAdapter.Fill(modelCatalogDataSet1);
			}
			catch (Exception ex)
			{
				this.AddErrorStatus("Could not load model catalog. Verify location of ResultsWarehouseDB: '"
					+ settings.DataSource.FindByName("ResultsWarehouseDB").DataSource_Text + "'.");
				this.AddErrorStatus(ex.ToString());
			}
			
			this.AddInfoStatus("Preparing master links preview.");
			try
			{
				agMasterDataSet1 = new DataAccess.AGMasterDataSet();
				agMasterDataAccess1 = new DataAccess.AGMasterDataAccess(settings.DataSource.FindByName("AGMasterDB").DataSource_Text);			
				agMasterDataAccess1.MstLinksDataAdapter.Fill(agMasterDataSet1);		
			}
			catch (Exception ex)
			{
				this.AddErrorStatus("Could not load master links preview. Verify location of AGMasterDB: '"
					+ settings.DataSource.FindByName("AGMasterDB").DataSource_Text + "'.");
				this.AddErrorStatus(ex.ToString());
			}

			modelCatalogView = new DataView(modelCatalogDataSet1.ModelCatalog);
			modelCatalogView.RowFilter="scenarioID=1";
			grdModelCatalog.DataSource=modelCatalogView;

			cmbSelectScenario.DataSource=modelCatalogDataSet1.ModelScenario;
			cmbSelectScenario.DisplayMember="description";

			//Object zoomLevel = 0;								
			//axWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_ZOOM,
			//	SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, ref zoomLevel, ref returnValue);	
			object blah = new object();
			axWebBrowser.Navigate(settings.DataSource.FindByName("CharTemplate").DataSource_Text, ref blah, ref blah, ref blah, ref blah );	
			this.AddInfoStatus("Finished loading master data.");			
		}
	}
}
