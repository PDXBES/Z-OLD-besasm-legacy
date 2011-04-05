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
		private System.Windows.Forms.TabPage tabStatusView;
		private System.Windows.Forms.RichTextBox statusTextBox;
		private System.Windows.Forms.Panel pnlStatusControl;
		private System.Windows.Forms.MenuItem menuItemToolSettings;
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
			
			this.AddInfoStatus("Loading model catalog data from '" + settings.DataSource.FindByName("ResultsWarehouseDB").DataSource_Text + "'.");
			modelCatalogDataSet1 = new DataAccess.ModelCatalogDataSet();
			modelCatalogDataAccess1 = new DataAccess.ModelCatalogDataAccess(settings.DataSource.FindByName("ResultsWarehouseDB").DataSource_Text);
			modelCatalogDataAccess1.StormCatalogDataAdapter.Fill(modelCatalogDataSet1);
			modelCatalogDataAccess1.ModelScenarioDataAdapter.Fill(modelCatalogDataSet1);
			modelCatalogDataAccess1.ModelCatalogDataAdapter.Fill(modelCatalogDataSet1);
			
			this.AddInfoStatus("Preparing master links preview.");
			agMasterDataSet1 = new DataAccess.AGMasterDataSet();
			agMasterDataAccess1 = new DataAccess.AGMasterDataAccess(settings.DataSource.FindByName("AGMasterDB").DataSource_Text);			
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
			openFileDialog1.InitialDirectory = settings.DataSource.FindByName("OutputDirectory").DataSource_Text;	
			folderBrowserDialog1.SelectedPath = settings.DataSource.FindByName("CharTemplate").DataSource_Text;
									
			//Object zoomLevel = 0;								
			//axWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_ZOOM,
			//	SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, ref zoomLevel, ref returnValue);	
			object blah = new object();
			//axWebBrowser.Navigate(settings.DataSource.FindByName("CharTemplate").DataSource_Text, ref blah, ref blah, ref blah, ref blah );									
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
			this.tabStatusView = new System.Windows.Forms.TabPage();
			this.statusTextBox = new System.Windows.Forms.RichTextBox();
			this.pnlStatusControl = new System.Windows.Forms.Panel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.menuItemToolSettings = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdModelCatalog)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settings)).BeginInit();
			this.SuspendLayout();
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = -1;
			this.menuItemFile.Text = "";
			// 
			// menuItemFileExit
			// 
			this.menuItemFileExit.Index = -1;
			this.menuItemFileExit.Text = "";
			// 
			// menuItemTools
			// 
			this.menuItemTools.Index = -1;
			this.menuItemTools.Text = "";
			// 
			// menuItemToolsClearLog
			// 
			this.menuItemToolsClearLog.Index = -1;
			this.menuItemToolsClearLog.Text = "";
			// 
			// axMapControl
			// 
			this.axMapControl.ContainingControl = this;
			this.axMapControl.Location = new System.Drawing.Point(0, 0);
			this.axMapControl.Name = "axMapControl";
			this.axMapControl.TabIndex = 0;
			// 
			// btnPan
			// 
			this.btnPan.Location = new System.Drawing.Point(0, 0);
			this.btnPan.Name = "btnPan";
			this.btnPan.TabIndex = 0;
			// 
			// btnZoomOut
			// 
			this.btnZoomOut.Location = new System.Drawing.Point(0, 0);
			this.btnZoomOut.Name = "btnZoomOut";
			this.btnZoomOut.TabIndex = 0;
			// 
			// btnZoomIn
			// 
			this.btnZoomIn.Location = new System.Drawing.Point(0, 0);
			this.btnZoomIn.Name = "btnZoomIn";
			this.btnZoomIn.TabIndex = 0;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(0, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 545);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// pnlCharacterizationView
			// 
			this.pnlCharacterizationView.Location = new System.Drawing.Point(0, 0);
			this.pnlCharacterizationView.Name = "pnlCharacterizationView";
			this.pnlCharacterizationView.TabIndex = 2;
			// 
			// tabControl2
			// 
			this.tabControl2.Location = new System.Drawing.Point(0, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.TabIndex = 0;
			// 
			// tabCatalogSelect
			// 
			this.tabCatalogSelect.Location = new System.Drawing.Point(0, 0);
			this.tabCatalogSelect.Name = "tabCatalogSelect";
			this.tabCatalogSelect.TabIndex = 0;
			// 
			// grdModelCatalog
			// 
			this.grdModelCatalog.DataMember = "";
			this.grdModelCatalog.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.grdModelCatalog.Location = new System.Drawing.Point(0, 0);
			this.grdModelCatalog.Name = "grdModelCatalog";
			this.grdModelCatalog.TabIndex = 0;
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = null;
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "";
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.MappingName = "";
			this.dataGridTextBoxColumn1.Width = -1;
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.MappingName = "";
			this.dataGridTextBoxColumn2.Width = -1;
			// 
			// dataGridTextBoxColumn3
			// 
			this.dataGridTextBoxColumn3.Format = "";
			this.dataGridTextBoxColumn3.FormatInfo = null;
			this.dataGridTextBoxColumn3.MappingName = "";
			this.dataGridTextBoxColumn3.Width = -1;
			// 
			// dataGridTextBoxColumn4
			// 
			this.dataGridTextBoxColumn4.Format = "";
			this.dataGridTextBoxColumn4.FormatInfo = null;
			this.dataGridTextBoxColumn4.MappingName = "";
			this.dataGridTextBoxColumn4.Width = -1;
			// 
			// dataGridTextBoxColumn5
			// 
			this.dataGridTextBoxColumn5.Format = "";
			this.dataGridTextBoxColumn5.FormatInfo = null;
			this.dataGridTextBoxColumn5.MappingName = "";
			this.dataGridTextBoxColumn5.Width = -1;
			// 
			// dataGridTextBoxColumn6
			// 
			this.dataGridTextBoxColumn6.Format = "";
			this.dataGridTextBoxColumn6.FormatInfo = null;
			this.dataGridTextBoxColumn6.MappingName = "";
			this.dataGridTextBoxColumn6.Width = -1;
			// 
			// pnlCharacterizationControls
			// 
			this.pnlCharacterizationControls.Location = new System.Drawing.Point(0, 0);
			this.pnlCharacterizationControls.Name = "pnlCharacterizationControls";
			this.pnlCharacterizationControls.TabIndex = 0;
			// 
			// btnReconcileModel
			// 
			this.btnReconcileModel.Location = new System.Drawing.Point(0, 0);
			this.btnReconcileModel.Name = "btnReconcileModel";
			this.btnReconcileModel.TabIndex = 0;
			// 
			// btnAddModel
			// 
			this.btnAddModel.Location = new System.Drawing.Point(0, 0);
			this.btnAddModel.Name = "btnAddModel";
			this.btnAddModel.TabIndex = 0;
			// 
			// cmbSelectScenario
			// 
			this.cmbSelectScenario.Location = new System.Drawing.Point(0, 0);
			this.cmbSelectScenario.Name = "cmbSelectScenario";
			this.cmbSelectScenario.TabIndex = 0;
			// 
			// lblScenario
			// 
			this.lblScenario.Location = new System.Drawing.Point(0, 0);
			this.lblScenario.Name = "lblScenario";
			this.lblScenario.TabIndex = 0;
			// 
			// tabInteractSelect
			// 
			this.tabInteractSelect.Location = new System.Drawing.Point(0, 0);
			this.tabInteractSelect.Name = "tabInteractSelect";
			this.tabInteractSelect.TabIndex = 0;
			// 
			// tabPolySelect
			// 
			this.tabPolySelect.Location = new System.Drawing.Point(0, 0);
			this.tabPolySelect.Name = "tabPolySelect";
			this.tabPolySelect.TabIndex = 0;
			// 
			// pnlCharacterizeControl
			// 
			this.pnlCharacterizeControl.Location = new System.Drawing.Point(0, 0);
			this.pnlCharacterizeControl.Name = "pnlCharacterizeControl";
			this.pnlCharacterizeControl.TabIndex = 0;
			// 
			// btnAddStopLink
			// 
			this.btnAddStopLink.Location = new System.Drawing.Point(0, 0);
			this.btnAddStopLink.Name = "btnAddStopLink";
			this.btnAddStopLink.TabIndex = 0;
			// 
			// btnAddStartLink
			// 
			this.btnAddStartLink.Location = new System.Drawing.Point(0, 0);
			this.btnAddStartLink.Name = "btnAddStartLink";
			this.btnAddStartLink.TabIndex = 0;
			// 
			// lblStudyArea
			// 
			this.lblStudyArea.Location = new System.Drawing.Point(0, 0);
			this.lblStudyArea.Name = "lblStudyArea";
			this.lblStudyArea.TabIndex = 0;
			// 
			// txtStudyAreaName
			// 
			this.txtStudyAreaName.Location = new System.Drawing.Point(0, 0);
			this.txtStudyAreaName.Name = "txtStudyAreaName";
			this.txtStudyAreaName.TabIndex = 0;
			this.txtStudyAreaName.Text = "";
			// 
			// btnPreviewTrace
			// 
			this.btnPreviewTrace.Location = new System.Drawing.Point(0, 0);
			this.btnPreviewTrace.Name = "btnPreviewTrace";
			this.btnPreviewTrace.TabIndex = 0;
			// 
			// btnRemoveStopLink
			// 
			this.btnRemoveStopLink.Location = new System.Drawing.Point(0, 0);
			this.btnRemoveStopLink.Name = "btnRemoveStopLink";
			this.btnRemoveStopLink.TabIndex = 0;
			// 
			// btnRemoveStartLink
			// 
			this.btnRemoveStartLink.Location = new System.Drawing.Point(0, 0);
			this.btnRemoveStartLink.Name = "btnRemoveStartLink";
			this.btnRemoveStartLink.TabIndex = 0;
			// 
			// btnClearList
			// 
			this.btnClearList.Location = new System.Drawing.Point(0, 0);
			this.btnClearList.Name = "btnClearList";
			this.btnClearList.TabIndex = 0;
			// 
			// lblStopLinks
			// 
			this.lblStopLinks.Location = new System.Drawing.Point(0, 0);
			this.lblStopLinks.Name = "lblStopLinks";
			this.lblStopLinks.TabIndex = 0;
			// 
			// lblStartLinks
			// 
			this.lblStartLinks.Location = new System.Drawing.Point(0, 0);
			this.lblStartLinks.Name = "lblStartLinks";
			this.lblStartLinks.TabIndex = 0;
			// 
			// lstStopLinks
			// 
			this.lstStopLinks.Location = new System.Drawing.Point(0, 0);
			this.lstStopLinks.Name = "lstStopLinks";
			this.lstStopLinks.TabIndex = 0;
			// 
			// lstStartLinks
			// 
			this.lstStartLinks.Location = new System.Drawing.Point(0, 0);
			this.lstStartLinks.Name = "lstStartLinks";
			this.lstStartLinks.TabIndex = 0;
			// 
			// btnCharacterize
			// 
			this.btnCharacterize.Location = new System.Drawing.Point(0, 0);
			this.btnCharacterize.Name = "btnCharacterize";
			this.btnCharacterize.TabIndex = 0;
			// 
			// tabControlOutputDisplay
			// 
			this.tabControlOutputDisplay.Location = new System.Drawing.Point(0, 0);
			this.tabControlOutputDisplay.Name = "tabControlOutputDisplay";
			this.tabControlOutputDisplay.SelectedIndex = 0;
			this.tabControlOutputDisplay.TabIndex = 0;
			// 
			// tabCharacterizationView
			// 
			this.tabCharacterizationView.Location = new System.Drawing.Point(0, 0);
			this.tabCharacterizationView.Name = "tabCharacterizationView";
			this.tabCharacterizationView.TabIndex = 0;
			// 
			// axWebBrowser
			// 
			this.axWebBrowser.ContainingControl = this;
			this.axWebBrowser.Enabled = true;
			this.axWebBrowser.Location = new System.Drawing.Point(0, 0);
			this.axWebBrowser.TabIndex = 0;
			// 
			// pnlCharDir
			// 
			this.pnlCharDir.Location = new System.Drawing.Point(0, 0);
			this.pnlCharDir.Name = "pnlCharDir";
			this.pnlCharDir.TabIndex = 0;
			// 
			// lblCharTemplate
			// 
			this.lblCharTemplate.Location = new System.Drawing.Point(0, 0);
			this.lblCharTemplate.Name = "lblCharTemplate";
			this.lblCharTemplate.TabIndex = 0;
			// 
			// lblOutputDir
			// 
			this.lblOutputDir.Location = new System.Drawing.Point(0, 0);
			this.lblOutputDir.Name = "lblOutputDir";
			this.lblOutputDir.TabIndex = 0;
			// 
			// btnSelectTemplate
			// 
			this.btnSelectTemplate.Location = new System.Drawing.Point(0, 0);
			this.btnSelectTemplate.Name = "btnSelectTemplate";
			this.btnSelectTemplate.TabIndex = 0;
			// 
			// txtCharTableTemplate
			// 
			this.txtCharTableTemplate.Location = new System.Drawing.Point(0, 0);
			this.txtCharTableTemplate.Name = "txtCharTableTemplate";
			this.txtCharTableTemplate.TabIndex = 0;
			this.txtCharTableTemplate.Text = "";
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
			// tabMapView
			// 
			this.tabMapView.Location = new System.Drawing.Point(0, 0);
			this.tabMapView.Name = "tabMapView";
			this.tabMapView.TabIndex = 0;
			// 
			// pnlMapControls
			// 
			this.pnlMapControls.Location = new System.Drawing.Point(0, 0);
			this.pnlMapControls.Name = "pnlMapControls";
			this.pnlMapControls.TabIndex = 0;
			// 
			// tabStatusView
			// 
			this.tabStatusView.Location = new System.Drawing.Point(0, 0);
			this.tabStatusView.Name = "tabStatusView";
			this.tabStatusView.TabIndex = 0;
			// 
			// statusTextBox
			// 
			this.statusTextBox.Location = new System.Drawing.Point(0, 0);
			this.statusTextBox.Name = "statusTextBox";
			this.statusTextBox.TabIndex = 0;
			this.statusTextBox.Text = "";
			// 
			// pnlStatusControl
			// 
			this.pnlStatusControl.Location = new System.Drawing.Point(0, 0);
			this.pnlStatusControl.Name = "pnlStatusControl";
			this.pnlStatusControl.TabIndex = 0;
			// 
			// menuItemToolSettings
			// 
			this.menuItemToolSettings.Index = -1;
			this.menuItemToolSettings.Text = "";
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
			((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdModelCatalog)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settings)).EndInit();
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
				outputFile = settings.DataSource.Rows.Find("OutputDir").ToString() + "Char_" + studyArea + ".xml";
				
				this.AddInfoStatus("Characterizing study area '" + studyArea + "'.");

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
				charEngine.Characterize();
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
				CGIS.MapWorks.EsriUtils.LoadLayerFile(settings.DataSource.Rows.Find("MstLinks").ToString(), axMapControl);		
			
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

		private void btnSelectTemplate_Click(object sender, System.EventArgs e)
		{				
			this.openFileDialog1.Title = "Select an .xml Characterization Template.";			
			this.openFileDialog1.ShowDialog();
			if (System.IO.File.Exists(this.openFileDialog1.FileName))
			{
				//settings.Files[0].CharTemplate = this.openFileDialog1.FileName;
			}
		}

		private void btnSelectCharDir_Click(object sender, System.EventArgs e)
		{			
			this.folderBrowserDialog1.Description = "Select the output directory.";			
			this.folderBrowserDialog1.ShowDialog();
			txtCharOutputDir.Text = this.folderBrowserDialog1.SelectedPath;
			if (System.IO.Directory.Exists(this.folderBrowserDialog1.SelectedPath))
			{				
				//settings.DataSource.Rows.Find("OutputDir").ToString() = this.folderBrowserDialog1.SelectedPath;
				if (!settings.DataSource.Rows.Find("OutputDir").ToString().EndsWith("\\"))
				{
					//settings.Files[0].OutputDir += "\\";
				}
			}
		
		}

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
			charNodes.Load(charLinks, settings.DataSource.Rows.Find("AGMasterDB").ToString());
			this.AddInfoStatus("Traced " + charNodes.Count + " nodes.");
			DSCs charDSCs = new DSCs();
			charDSCs.Load(charLinks, settings.DataSource.Rows.Find("AGMasterDB").ToString());
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

		private void menuItemToolSettings_Click(object sender, System.EventArgs e)
		{
			Preferences pref = new Preferences("\\\\Cassio\\Modeling\\Model_Programs\\ResultsWarehouse\\CharacterizationQueriesManager\\Default_Settings.xml");
			pref.Show();
		}
				
	}
}
