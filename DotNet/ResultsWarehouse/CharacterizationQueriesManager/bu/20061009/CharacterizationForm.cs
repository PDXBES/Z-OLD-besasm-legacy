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

		private AxSHDocVw.AxWebBrowser axWebBrowser;
	
		private StatusForm statusForm;		
		
		private SystemsAnalysis.Characterization.Settings settings;

		#region Form component declarations
		private System.ComponentModel.Container components = null;		
		private System.Windows.Forms.MainMenu mainMenu1;
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
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tabCatalogSelect;
		private System.Windows.Forms.TabPage tabInteractSelect;
		private System.Windows.Forms.TabPage tabPolySelect;
		private System.Windows.Forms.DataGrid grdModelCatalog;
		private System.Windows.Forms.Panel pnlCharacterizationControls;
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
		private System.Windows.Forms.Panel pnlCharacterizeControl;
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
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button btnSelectTemplate;
		private System.Windows.Forms.Button btnSelectCharDir;
		private System.Windows.Forms.TabControl tabControlOutputDisplay;
		private System.Windows.Forms.TextBox txtCharTableTemplate;
		private System.Windows.Forms.TextBox txtCharOutputDir;
		private System.Windows.Forms.Panel pnlCharDir;
		private System.Windows.Forms.Label lblOutputDir;
		private System.Windows.Forms.Label lblCharTemplate;
		private System.Windows.Forms.MenuItem menuItemTools;
		private System.Windows.Forms.MenuItem menuItemToolsViewLog;
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

			this.statusForm = new StatusForm();
			this.statusForm.Visible = true;
			this.statusForm.AddInfo("Initializing Windows form components.");
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();			
			settings.ReadXml("\\\\Cassio\\Modeling\\Model_Programs\\ResultsWarehouse\\CharacterizationQueriesManager\\Default_Settings.xml");			

			this.statusForm.AddInfo("Loading model catalog data from '" + settings.Files[0].ResultsWarehouseDB + "'.");
			modelCatalogDataSet1 = new DataAccess.ModelCatalogDataSet();
			modelCatalogDataAccess1 = new DataAccess.ModelCatalogDataAccess(settings.Files[0].ResultsWarehouseDB);
			modelCatalogDataAccess1.StormCatalogDataAdapter.Fill(modelCatalogDataSet1);
			modelCatalogDataAccess1.ModelScenarioDataAdapter.Fill(modelCatalogDataSet1);
			modelCatalogDataAccess1.ModelCatalogDataAdapter.Fill(modelCatalogDataSet1);
			
			this.statusForm.AddInfo("Preparing master links preview.");
			agMasterDataSet1 = new DataAccess.AGMasterDataSet();
			agMasterDataAccess1 = new DataAccess.AGMasterDataAccess(settings.Files[0].AGMasterDB);			
			agMasterDataAccess1.MstLinksDataAdapter.Fill(agMasterDataSet1);			

			modelCatalogView = new DataView(modelCatalogDataSet1.ModelCatalog);
			modelCatalogView.RowFilter="scenarioID=1";
			grdModelCatalog.DataSource=modelCatalogView;

			cmbSelectScenario.DataSource=modelCatalogDataSet1.ModelScenario;
			cmbSelectScenario.DisplayMember="description";

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
						
			//txtCharOutputDir.Text = XMLDIR;
			openFileDialog1.InitialDirectory = settings.Files[0].OutputDir;	
			folderBrowserDialog1.SelectedPath = System.IO.Path.GetDirectoryName(settings.Files[0].CharTemplate);
									
			//Object zoomLevel = 0;								
			//axWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_ZOOM,
			//	SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, ref zoomLevel, ref returnValue);	
			axWebBrowser.Navigate(settings.Files[0].CharTemplate);																																																		
						
			this.statusForm.Visible = false;
			this.statusForm.Hide();			
			Application.DoEvents();
			this.statusForm.Hide();						
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.menuItemFileExit = new System.Windows.Forms.MenuItem();
			this.menuItemTools = new System.Windows.Forms.MenuItem();
			this.menuItemToolsViewLog = new System.Windows.Forms.MenuItem();
			this.menuItemToolsClearLog = new System.Windows.Forms.MenuItem();
			this.axMapControl = new ESRI.ArcGIS.MapControl.AxMapControl();
			this.btnPan = new System.Windows.Forms.Button();
			this.btnZoomOut = new System.Windows.Forms.Button();
			this.btnZoomIn = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnlCharacterizationView = new System.Windows.Forms.Panel();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tabCatalogSelect = new System.Windows.Forms.TabPage();
			this.grdModelCatalog = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.pnlCharacterizationControls = new System.Windows.Forms.Panel();
			this.btnReconcileModel = new System.Windows.Forms.Button();
			this.btnAddModel = new System.Windows.Forms.Button();
			this.cmbSelectScenario = new System.Windows.Forms.ComboBox();
			this.lblScenario = new System.Windows.Forms.Label();
			this.tabInteractSelect = new System.Windows.Forms.TabPage();
			this.tabPolySelect = new System.Windows.Forms.TabPage();
			this.pnlCharacterizeControl = new System.Windows.Forms.Panel();
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
			this.tabCharacterizationView = new System.Windows.Forms.TabPage();
			this.axWebBrowser = new AxSHDocVw.AxWebBrowser();
			this.pnlCharDir = new System.Windows.Forms.Panel();
			this.lblCharTemplate = new System.Windows.Forms.Label();
			this.lblOutputDir = new System.Windows.Forms.Label();
			this.btnSelectTemplate = new System.Windows.Forms.Button();
			this.txtCharTableTemplate = new System.Windows.Forms.TextBox();
			this.settings = new SystemsAnalysis.Characterization.Settings();
			this.btnSelectCharDir = new System.Windows.Forms.Button();
			this.txtCharOutputDir = new System.Windows.Forms.TextBox();
			this.tabMapView = new System.Windows.Forms.TabPage();
			this.pnlMapControls = new System.Windows.Forms.Panel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
			this.pnlCharacterizationView.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tabCatalogSelect.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdModelCatalog)).BeginInit();
			this.pnlCharacterizationControls.SuspendLayout();
			this.pnlCharacterizeControl.SuspendLayout();
			this.tabControlOutputDisplay.SuspendLayout();
			this.tabCharacterizationView.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).BeginInit();
			this.pnlCharDir.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.settings)).BeginInit();
			this.tabMapView.SuspendLayout();
			this.pnlMapControls.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
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
																						  this.menuItemToolsViewLog,
																						  this.menuItemToolsClearLog});
			this.menuItemTools.Text = "Tools";
			// 
			// menuItemToolsViewLog
			// 
			this.menuItemToolsViewLog.Index = 0;
			this.menuItemToolsViewLog.Text = "View Status Log";
			this.menuItemToolsViewLog.Click += new System.EventHandler(this.menuItemToolsViewLog_Click);
			// 
			// menuItemToolsClearLog
			// 
			this.menuItemToolsClearLog.Index = 1;
			this.menuItemToolsClearLog.Text = "Clear Status Log";
			this.menuItemToolsClearLog.Click += new System.EventHandler(this.menuItemToolsClearLog_Click);
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
			this.pnlCharacterizationView.Controls.Add(this.tabControl2);
			this.pnlCharacterizationView.Controls.Add(this.pnlCharacterizeControl);
			this.pnlCharacterizationView.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlCharacterizationView.Location = new System.Drawing.Point(0, 0);
			this.pnlCharacterizationView.Name = "pnlCharacterizationView";
			this.pnlCharacterizationView.Size = new System.Drawing.Size(376, 545);
			this.pnlCharacterizationView.TabIndex = 19;
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.tabCatalogSelect);
			this.tabControl2.Controls.Add(this.tabInteractSelect);
			this.tabControl2.Controls.Add(this.tabPolySelect);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.Location = new System.Drawing.Point(0, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(374, 351);
			this.tabControl2.TabIndex = 21;
			// 
			// tabCatalogSelect
			// 
			this.tabCatalogSelect.Controls.Add(this.grdModelCatalog);
			this.tabCatalogSelect.Controls.Add(this.pnlCharacterizationControls);
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
			// pnlCharacterizationControls
			// 
			this.pnlCharacterizationControls.Controls.Add(this.btnReconcileModel);
			this.pnlCharacterizationControls.Controls.Add(this.btnAddModel);
			this.pnlCharacterizationControls.Controls.Add(this.cmbSelectScenario);
			this.pnlCharacterizationControls.Controls.Add(this.lblScenario);
			this.pnlCharacterizationControls.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlCharacterizationControls.Location = new System.Drawing.Point(0, 261);
			this.pnlCharacterizationControls.Name = "pnlCharacterizationControls";
			this.pnlCharacterizationControls.Size = new System.Drawing.Size(366, 64);
			this.pnlCharacterizationControls.TabIndex = 22;
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
			// pnlCharacterizeControl
			// 
			this.pnlCharacterizeControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlCharacterizeControl.Controls.Add(this.btnAddStopLink);
			this.pnlCharacterizeControl.Controls.Add(this.btnAddStartLink);
			this.pnlCharacterizeControl.Controls.Add(this.lblStudyArea);
			this.pnlCharacterizeControl.Controls.Add(this.txtStudyAreaName);
			this.pnlCharacterizeControl.Controls.Add(this.btnPreviewTrace);
			this.pnlCharacterizeControl.Controls.Add(this.btnRemoveStopLink);
			this.pnlCharacterizeControl.Controls.Add(this.btnRemoveStartLink);
			this.pnlCharacterizeControl.Controls.Add(this.btnClearList);
			this.pnlCharacterizeControl.Controls.Add(this.lblStopLinks);
			this.pnlCharacterizeControl.Controls.Add(this.lblStartLinks);
			this.pnlCharacterizeControl.Controls.Add(this.lstStopLinks);
			this.pnlCharacterizeControl.Controls.Add(this.lstStartLinks);
			this.pnlCharacterizeControl.Controls.Add(this.btnCharacterize);
			this.pnlCharacterizeControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlCharacterizeControl.Location = new System.Drawing.Point(0, 351);
			this.pnlCharacterizeControl.Name = "pnlCharacterizeControl";
			this.pnlCharacterizeControl.Size = new System.Drawing.Size(374, 192);
			this.pnlCharacterizeControl.TabIndex = 22;
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
			this.tabControlOutputDisplay.Controls.Add(this.tabCharacterizationView);
			this.tabControlOutputDisplay.Controls.Add(this.tabMapView);
			this.tabControlOutputDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlOutputDisplay.Location = new System.Drawing.Point(379, 0);
			this.tabControlOutputDisplay.Name = "tabControlOutputDisplay";
			this.tabControlOutputDisplay.SelectedIndex = 0;
			this.tabControlOutputDisplay.Size = new System.Drawing.Size(565, 545);
			this.tabControlOutputDisplay.TabIndex = 20;
			// 
			// tabCharacterizationView
			// 
			this.tabCharacterizationView.Controls.Add(this.axWebBrowser);
			this.tabCharacterizationView.Controls.Add(this.pnlCharDir);
			this.tabCharacterizationView.Location = new System.Drawing.Point(4, 22);
			this.tabCharacterizationView.Name = "tabCharacterizationView";
			this.tabCharacterizationView.Size = new System.Drawing.Size(557, 519);
			this.tabCharacterizationView.TabIndex = 1;
			this.tabCharacterizationView.Text = "Characterization Tables";
			// 
			// axWebBrowser
			// 
			this.axWebBrowser.ContainingControl = this;
			this.axWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axWebBrowser.Enabled = true;
			this.axWebBrowser.Location = new System.Drawing.Point(0, 0);
			this.axWebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser.OcxState")));
			this.axWebBrowser.Size = new System.Drawing.Size(557, 399);
			this.axWebBrowser.TabIndex = 4;
			// 
			// pnlCharDir
			// 
			this.pnlCharDir.Controls.Add(this.lblCharTemplate);
			this.pnlCharDir.Controls.Add(this.lblOutputDir);
			this.pnlCharDir.Controls.Add(this.btnSelectTemplate);
			this.pnlCharDir.Controls.Add(this.txtCharTableTemplate);
			this.pnlCharDir.Controls.Add(this.btnSelectCharDir);
			this.pnlCharDir.Controls.Add(this.txtCharOutputDir);
			this.pnlCharDir.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlCharDir.Location = new System.Drawing.Point(0, 399);
			this.pnlCharDir.Name = "pnlCharDir";
			this.pnlCharDir.Size = new System.Drawing.Size(557, 120);
			this.pnlCharDir.TabIndex = 5;
			// 
			// lblCharTemplate
			// 
			this.lblCharTemplate.Location = new System.Drawing.Point(16, 8);
			this.lblCharTemplate.Name = "lblCharTemplate";
			this.lblCharTemplate.Size = new System.Drawing.Size(144, 16);
			this.lblCharTemplate.TabIndex = 5;
			this.lblCharTemplate.Text = "Characterization Template:";
			// 
			// lblOutputDir
			// 
			this.lblOutputDir.Location = new System.Drawing.Point(16, 56);
			this.lblOutputDir.Name = "lblOutputDir";
			this.lblOutputDir.Size = new System.Drawing.Size(100, 16);
			this.lblOutputDir.TabIndex = 4;
			this.lblOutputDir.Text = "Output Directory:";
			// 
			// btnSelectTemplate
			// 
			this.btnSelectTemplate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnSelectTemplate.Location = new System.Drawing.Point(472, 48);
			this.btnSelectTemplate.Name = "btnSelectTemplate";
			this.btnSelectTemplate.TabIndex = 2;
			this.btnSelectTemplate.Text = "Browse";
			this.btnSelectTemplate.Click += new System.EventHandler(this.btnSelectTemplate_Click);
			// 
			// txtCharTableTemplate
			// 
			this.txtCharTableTemplate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.txtCharTableTemplate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.settings, "Files.CharTemplate"));
			this.txtCharTableTemplate.Location = new System.Drawing.Point(16, 24);
			this.txtCharTableTemplate.Name = "txtCharTableTemplate";
			this.txtCharTableTemplate.Size = new System.Drawing.Size(536, 20);
			this.txtCharTableTemplate.TabIndex = 0;
			this.txtCharTableTemplate.Text = "";
			// 
			// settings
			// 
			this.settings.DataSetName = "Settings";
			this.settings.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// btnSelectCharDir
			// 
			this.btnSelectCharDir.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnSelectCharDir.Location = new System.Drawing.Point(472, 96);
			this.btnSelectCharDir.Name = "btnSelectCharDir";
			this.btnSelectCharDir.TabIndex = 3;
			this.btnSelectCharDir.Text = "Browse";
			this.btnSelectCharDir.Click += new System.EventHandler(this.btnSelectCharDir_Click);
			// 
			// txtCharOutputDir
			// 
			this.txtCharOutputDir.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.txtCharOutputDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.settings, "Files.OutputDir"));
			this.txtCharOutputDir.Location = new System.Drawing.Point(16, 72);
			this.txtCharOutputDir.Name = "txtCharOutputDir";
			this.txtCharOutputDir.Size = new System.Drawing.Size(536, 20);
			this.txtCharOutputDir.TabIndex = 1;
			this.txtCharOutputDir.Text = "";
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
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "*.xml";
			this.openFileDialog1.RestoreDirectory = true;
			// 
			// CharacterizationForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(944, 545);
			this.Controls.Add(this.tabControlOutputDisplay);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.pnlCharacterizationView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "CharacterizationForm";
			this.Text = "Characterization Queries Manager";
			this.Load += new System.EventHandler(this.CharacterizationForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
			this.pnlCharacterizationView.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.tabCatalogSelect.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdModelCatalog)).EndInit();
			this.pnlCharacterizationControls.ResumeLayout(false);
			this.pnlCharacterizeControl.ResumeLayout(false);
			this.tabControlOutputDisplay.ResumeLayout(false);
			this.tabCharacterizationView.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).EndInit();
			this.pnlCharDir.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.settings)).EndInit();
			this.tabMapView.ResumeLayout(false);
			this.pnlMapControls.ResumeLayout(false);
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
				outputFile = this.settings.Files[0].OutputDir + "Char_" + studyArea + ".xml";

				this.statusForm.Visible = true;
				this.statusForm.AddInfo("Characterizing study area '" + studyArea + "'.");

				if (studyArea == null || studyArea.Length <= 0)
				{
					throw new Exception("You must enter a Study Area.");						
				}			
				if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(outputFile)))
					//|| outputFile.IndexOfAny(System.IO.Path.InvalidPathChars) >= 0)
				{
					throw new Exception("Could not create output file. Verify output path and study area name are valid: '"
						+ outputFile + "'.");						
				}
			}
			catch (Exception ex)
			{
				this.statusForm.AddError(ex.Message);
				return;
			}

			try
			{
				mstLinks.ClearSubNetwork();			
				this.statusForm.AddInfo("Performing trace.");
				int traceCount = mstLinks.SelectSubNetwork(startLinks, stopLinks);	
				if (traceCount <= 0)
				{
					throw new Exception("Error: No links were selected. Please verify start links.");								
				}
				this.statusForm.AddInfo("Traced " + traceCount + " links.");
			}
			catch (Exception ex)
			{
				this.statusForm.AddError(ex.Message);
				return;

			}
										
			this.statusForm.AddInfo("Creating characterization engine.");		
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
				charEngine.Characterize();
			}
			catch (Exception ex)
			{
				statusForm.AddError(ex.Message);
				return;
			}

			tabControlOutputDisplay.SelectedTab = tabCharacterizationView;			
			axWebBrowser.Navigate(outputFile);
			if (System.IO.File.Exists(outputFile))
			{
				this.statusForm.AddInfo("Wrote output file: '" + outputFile + "'.");
				this.statusForm.AddInfo("** Characterization Complete! **");
			}
			else
			{
				this.statusForm.AddError("Characterization incomplete! " + 
					"Could not create output file: '" + outputFile + "'.");
			}
					
			this.statusForm.Visible = true;		
			this.statusForm.Hide();
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
				CGIS.MapWorks.EsriUtils.LoadLayerFile(settings.FeatureClasses[0].MstLinks, axMapControl);		
			
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

			this.statusForm.AddInfo("Reading start/stop links from model.ini for model: '"
				+ selectedModelRow.modelName + "'.");
			
			try
			{
				selectedModel = new Model(selectedModelRow.modelPath);
			}
			catch
			{
				this.statusForm.AddError(e.ToString());
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
						this.statusForm.AddWarning("Start link missing from master database. MLinkID = " + l.MLinkID);
					}
					//TODO: Log missing start link
				}
				foreach (Link l in selectedModel.ForcedStartLinks.Values)
				{
					Link forcedStartLink;
					forcedStartLink = mstLinks.FindByMLinkID(l.MLinkID);
					if (forcedStartLink != null)
					{
						this.startLinks.Add(forcedStartLink);
					}
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
						this.statusForm.AddWarning(
							"Stop link missing from master database. MLinkID = " + l.MLinkID);
					}
					//TODO: Log missing stop link				
				}
				foreach (Link l in selectedModel.ForcedStopLinks.Values)
				{
					Link forcedStopLink;
					forcedStopLink = mstLinks.FindByMLinkID(l.MLinkID);
					if (forcedStopLink != null)
					{
						this.stopLinks.Add(forcedStopLink);
					}
				}	
			}
			catch (Exception ex)
			{
				this.startLinks.Clear();
				this.stopLinks.Clear();				
				this.statusForm.AddError(ex.Message);				
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

		private void btnSelectTemplate_Click(object sender, System.EventArgs e)
		{				
			this.openFileDialog1.Title = "Select an .xml Characterization Template.";			
			this.openFileDialog1.ShowDialog();
			if (System.IO.File.Exists(this.openFileDialog1.FileName))
			{
				settings.Files[0].CharTemplate = this.openFileDialog1.FileName;
			}
		}

		private void btnSelectCharDir_Click(object sender, System.EventArgs e)
		{			
			this.folderBrowserDialog1.Description = "Select the output directory.";			
			this.folderBrowserDialog1.ShowDialog();
			txtCharOutputDir.Text = this.folderBrowserDialog1.SelectedPath;
			if (System.IO.Directory.Exists(this.folderBrowserDialog1.SelectedPath))
			{				
				settings.Files[0].OutputDir = this.folderBrowserDialog1.SelectedPath;
				if (!settings.Files[0].OutputDir.EndsWith("\\"))
				{
					settings.Files[0].OutputDir += "\\";
				}
			}
		
		}

		private void OnStatusChanged(Utils.Events.StatusChangedArgs e)
		{			
			this.statusForm.AddInfo(e.NewStatus);					
		}

		private void menuItemToolsViewLog_Click(object sender, System.EventArgs e)
		{
			this.statusForm.Show();
		}

		private void menuItemToolsClearLog_Click(object sender, System.EventArgs e)
		{
			this.statusForm.ClearText();
		}

		private void btnReconcileModel_Click(object sender, System.EventArgs e)
		{						
			this.statusForm.AddInfo("Attempting to reconcile model to master data.");
			if (this.selectedModel == null)
			{				
				this.statusForm.AddError("No model selected.");
			}			
			Links modelLinks = this.selectedModel.ModelLinks;
			Nodes modelNodes = this.selectedModel.ModelNodes;
			DSCs modelDSCs = this.selectedModel.ModelDSCs;

			this.mstLinks.ClearSubNetwork();			
			this.statusForm.AddInfo("Performing trace.");
			int traceCount = mstLinks.SelectSubNetwork(
				selectedModel.StartLinks, selectedModel.StopLinks);
			if (traceCount <= 0)
			{
				throw new Exception("Error: No links were selected. Please verify start links.");								
			}
			this.statusForm.AddInfo("Traced " + traceCount + " links.");
			Links charLinks = mstLinks.GetSubNetwork();
			Nodes charNodes = new Nodes();
			charNodes.Load(charLinks, settings.Files[0].AGMasterDB);
			DSCs charDSCs = new DSCs();
			charDSCs.Load(charLinks, settings.Files[0].AGMasterDB);
			this.statusForm.AddInfo("*************************");
			this.statusForm.AddInfo("*		Model		Trace *");
			this.statusForm.AddInfo("* Links: " + modelLinks.Count + "	" + charLinks.Count + " *");
			this.statusForm.AddInfo("* Nodes: " + modelNodes.Count + "	" + charNodes.Count + " *");
			this.statusForm.AddInfo("* DSCs: " + modelDSCs.Count + "	" + charDSCs.Count + " *");
			this.statusForm.AddInfo("*************************");
			foreach (Link l in modelLinks.Values)
			{
				if (charLinks.FindByMLinkID(l.MLinkID) == null)
				{
					this.statusForm.AddWarning("MLinkID " + l.MLinkID + " missing from new trace.");
				}
			}
			foreach (Link l in charLinks.Values)
			{
				if (modelLinks.FindByMLinkID(l.MLinkID) == null)
				{
					this.statusForm.AddWarning("MLinkID " + l.MLinkID + " does not exist in model.");
				}
			}
			foreach (Node n in modelNodes.Values)
			{
				if (!charNodes.Contains(n.Name))
				{
					this.statusForm.AddWarning("Node " + n.Name + " missing from new trace.");
				}
			}
			foreach (Node n in charNodes.Values)
			{
				if (modelNodes.Contains(n.Name))
				{
					this.statusForm.AddWarning("Node " + n.Name+ " does not exist in model.");
				}			
			}
			foreach (DSC d in modelDSCs.Values)
			{
				if (!charDSCs.Contains(d.DSCID))
				{
					this.statusForm.AddWarning("DSC " + d.DSCID + " missing from new trace.");
				}
			}
			foreach (DSC d in charDSCs.Values)
			{
				if (modelDSCs.Contains(d.DSCID))
				{
					this.statusForm.AddWarning("DSC " + d.DSCID + " does not exist in model.");
				}
			}
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
				this.statusForm.AddError(ex.Message);
				return null;
			}
		}
				
	}
}
