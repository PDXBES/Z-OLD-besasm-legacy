using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using SystemsAnalysis;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Characterization
{
    /// <summary>
    /// User interface for generating characterization reports
    /// </summary>
    public partial class CharacterizationForm : Form
    {     
        /*	This object ensures that all COM-wrapped ArcObjects are
            correctly disposed of when the application shuts down. This
            must be the first ArcObject instantiated	*/
        private IAoInitialize m_AoInitialize = new AoInitializeClass();

        private DataAccess.SAMasterDataSet saMasterDS;       

        //Map Control commands
        private SystemsAnalysis.Utils.ArcObjects.MapControl.SelectFeatures selectCommand;
        private SystemsAnalysis.Utils.ArcObjects.MapControl.ClearFeatureSelection clearCommand;
        private SystemsAnalysis.Utils.ArcObjects.MapControl.Pan panCommand;
        private SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomIn zoomInCommand;
        private SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomOut zoomOutCommand;
        
        private Links mstLinks;
        private Links startLinks;
        private Links stopLinks;

        private DataTable reportTable;
        
        private SystemsAnalysis.Characterization.Preferences pref;        

        /// <summary>
        /// Constructor for characterization user interface
        /// </summary>
        public CharacterizationForm()
        {
            /*	This object ensures that all COM-wrapped ArcObjects are
				correctly disposed of when the application shuts down. */
            m_AoInitialize = new AoInitializeClass();
            if (m_AoInitialize.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeArcView) == esriLicenseStatus.esriLicenseAvailable)
            {
                m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeArcView);
            }
            else
            {
                MessageBox.Show("Could not check out ArcInfo license.");
                return;
            }

            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(DoSplash));
            th.Start();

            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            settings.ReadXml(@"\\Cassio\Modeling\Model_Programs\ResultsWarehouse\CharacterizationQueriesManager\Default_Settings.xml");

            LoadMasterData();
            SetupMapControl();

            InitializeLinkDisplayBoxes();
            
            LoadReportList();         
        }

        private void InitializeLinkDisplayBoxes()
        {
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

        private void LoadReportList()
        {
            string[] reportPathList;

            reportPathList = Directory.GetFiles(Application.StartupPath + "\\xml\\", "*.xml", SearchOption.TopDirectoryOnly);

            reportTable = new DataTable("CharacterizationReports");
            reportTable.Columns.Add("ReportName", Type.GetType("System.String"));
            reportTable.Columns.Add("ReportPath", Type.GetType("System.String"));
            for (int i = 0; i < reportPathList.Length; i++)
            {

                System.Xml.XmlTextReader xmlReader;
                xmlReader = new System.Xml.XmlTextReader(reportPathList[i]);
                xmlReader.ReadToFollowing("ReportGenerator");
                string reportName;
                reportName = xmlReader.GetAttribute("reportName");
                reportTable.Rows.Add(reportName, reportPathList[i]);
            }
            cmbReportChooser.DataSource = reportTable;
            cmbReportChooser.ValueMember = "ReportPath";
            cmbReportChooser.DisplayMember = "ReportName";
            cmbReportChooser.SelectedRow = cmbReportChooser.Rows[0];
        }

        /// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new CharacterizationForm());			    
		}

        private static void DoSplash()
        {
            DoSplash(false);
        }

        /// <summary>
        /// Loads the Splash Screen during start-up.
        /// </summary>
        private static void DoSplash(bool waitForClick)
        {
            string versionText = "x.x.x.x";
            string dateText = "1/1/1900";
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                Version v = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                versionText = v.Major + "." + v.Minor + "." + v.Build + "." + v.Revision;
                dateText = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString("MMMM dd yyyy");
            }
            else
            {
                string version = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.ToString();
                int minorVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Minor;
                int majorVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Major;
                int build = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Build;
                int revision = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Revision;

                FileInfo fi = new FileInfo("AlternativesToolkit.exe");
                string date = fi.CreationTime.Date.ToString("MMMM dd yyyy");
            }

            SplashScreen sp = new SplashScreen(versionText, dateText);
            sp.ShowDialog(waitForClick);
        }

		#region Menu item event handlers
		private void menuItemFileExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();			
		}
        private void menuItemToolsClearLog_Click(object sender, System.EventArgs e)
        {
            this.ClearStatusText();
        }
        private void menuItemToolsSettings_Click(object sender, System.EventArgs e)
        {
            if (pref == null)
            {
                pref = new Preferences("\\\\Cassio\\Modeling\\Model_Programs\\ResultsWarehouse\\CharacterizationQueriesManager\\Default_Settings.xml");
            }
            if (pref.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.settings = pref.Settings;
                this.LoadMasterData();
            }
        }
        private void menuItemManageModelCatalog_Click(object sender, EventArgs e)
        {
            ModelCatalogMaintForm modelCatalogMaintForm;
            modelCatalogMaintForm = new ModelCatalogMaintForm();
            modelCatalogMaintForm.ShowDialog();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            DoSplash(true);
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
                saveFileDialog.ShowDialog();
                outputFile = saveFileDialog.FileName;
                charEngine.Characterize((string)cmbReportChooser.Value, outputFile, studyArea);
			}
			catch (Exception ex)
			{
				this.AddErrorStatus(ex.Message);
				return;
			}            
			this.tabControlOutputDisplay.SelectedTab = tabCharacterizationView.Tab;	
					
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
			try
			{
				mstLinks.ClearSubNetwork();
				mstLinks.SelectSubNetwork(startLinks, stopLinks);	
	
				ESRI.ArcGIS.Carto.IFeatureLayer pFeatLayer;
				pFeatLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)axMapControl.get_Layer(0);

				int[] selectedEdgeIDArray = mstLinks.GetSelectedEdgeIDArray();			
				Utils.ArcObjects.MapControl.MapControlHelper.ZoomToOIDArray(
					axMapControl, pFeatLayer, selectedEdgeIDArray);	
				tabControlOutputDisplay.SelectedTab = tabMapView.Tab;
			}
			catch (Exception ex)
			{
				this.AddErrorStatus(ex.ToString());
			}
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

		private void SetupMapControl()
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
		}
			
		private void axMapControl_OnMouseDown(
			object sender, ESRI.ArcGIS.MapControl.IMapControlEvents2_OnMouseDownEvent e)
		{			
		
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

		private void btnReconcileModel_Click(object sender, System.EventArgs e)
		{	
			/*this.tabControlOutputDisplay.SelectedTab = this.tabStatusView.Tab;	
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
			this.AddInfoStatus("Reconciliation Complete.");*/
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
            this.tabControlOutputDisplay.SelectedTab = this.tabStatusView.Tab;
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
		
		private void LoadMasterData()
		{						
			this.AddInfoStatus("Preparing master links preview.");
			try
			{
                saMasterDS = new DataAccess.SAMasterDataSet();

                DataAccess.SAMasterDataSetTableAdapters.MstLinksTableAdapter mstLinksTA;
                mstLinksTA = new SystemsAnalysis.DataAccess.SAMasterDataSetTableAdapters.MstLinksTableAdapter();

                mstLinksTA.Fill(saMasterDS.MstLinks);
                
				//agMasterDataAccess1 = new DataAccess.AGMasterDataAccess(settings.DataSource.FindByName("AGMasterDB").DataSource_Text);			
				//agMasterDataAccess1.MstLinksDataAdapter.Fill(agMasterDataSet1);									
                mstLinks = new Links(saMasterDS.MstLinks);		
			}
			catch (Exception ex)
			{
                this.AddErrorStatus("Could not load master links preview.");
				this.AddErrorStatus(ex.ToString());
			}

            try
            {
                ESRI.ArcGIS.Carto.IFeatureLayer pFL;                

                pFL = (ESRI.ArcGIS.Carto.FeatureLayer)
                    CGIS.MapWorks.EsriUtils.LoadLayerFile(settings.DataSource.FindByName("MstLinks").DataSource_Text, axMapControl);

                //Object zoomLevel = 0;								
                //axWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_ZOOM,
                //	SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, ref zoomLevel, ref returnValue);	
                object blah = new object();
                axWebBrowser.Navigate(settings.DataSource.FindByName("CharTemplate").DataSource_Text, ref blah, ref blah, ref blah, ref blah);
            }
            catch (Exception ex)
            {
                this.AddErrorStatus("Could not display preview map.");
                this.AddErrorStatus(ex.ToString());
            }
			this.AddInfoStatus("Finished loading master data.");			
		}
     
        private void btnImportFromModel_Click(object sender, EventArgs e)
        {
            ModelCatalogViewForm mcForm;
            mcForm = new ModelCatalogViewForm();
            Model[] models;
            if (mcForm.ShowDialog() != DialogResult.OK)
            {                
                return;
            }
            models = mcForm.SelectedModels;
            this.AddInfoStatus("Importing start/stop links from " + models.Length + " models.");

            foreach (Model m in models)
            {
                this.AddInfoStatus("Importing start/stop links from " + m.Path + ".");
                if (startLinks.Count == 0 & stopLinks.Count == 0)
                {
                    this.txtStudyAreaName.Text = mcForm.StudyArea;
                }                

                try
                {
                    foreach (Link l in m.StartLinks.Values)
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
                    foreach (Link l in m.StopLinks.Values)
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
        }

        private void btnImportFromIni_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".ini";
            ofd.CheckFileExists = true;
            ofd.AddExtension = true;
            ofd.Title = "Find a Model.ini file";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            
            SystemsAnalysis.Utils.INIFile iniFile;
            iniFile = new SystemsAnalysis.Utils.INIFile(ofd.FileName);
            if (iniFile.GetINIKeys("RootLinks") != null)
            {
                foreach (string s in iniFile.GetINIKeys("RootLinks"))
                {
                    Link l = mstLinks.FindByMLinkID(Convert.ToInt32(s));
                    if (l != null)
                    {
                        startLinks.Add(l);
                    }
                }
            }
            if (iniFile.GetINIKeys("ForcedStartLinks") != null)
            {
                foreach (string s in iniFile.GetINIKeys("ForcedStartLinks"))
                {
                    Link l = mstLinks.FindByMLinkID(Convert.ToInt32(s));
                    if (l != null)
                    {
                        startLinks.Add(l);
                    }
                }
            }
            if (iniFile.GetINIKeys("StopLinks") != null)
            {
                foreach (string s in iniFile.GetINIKeys("StopLinks"))
                {
                    Link l = mstLinks.FindByMLinkID(Convert.ToInt32(s));
                    if (l != null)
                    {
                        stopLinks.Add(l);
                    }
                }
            }
            if (iniFile.GetINIKeys("ForcedStopLinks") != null)
            {
                foreach (string s in iniFile.GetINIKeys("ForcedStopLinks"))
                {
                    Link l = mstLinks.FindByMLinkID(Convert.ToInt32(s));
                    if (l != null)
                    {
                        stopLinks.Add(l);
                    }
                }
            }
        }     
	}
}
