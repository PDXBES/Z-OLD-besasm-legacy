// Project: UI, File: Main.cs
// Namespace: CostEstimator, Class: Main
// Path: C:\Development\CostEstimatorV2\UI, Author: Arnel
// Code lines: 151, Size of file: 4.04 KB
// Creation date: 3/7/2008 8:58 AM
// Last modified: 8/18/2009 2:35 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Collections;
using ADOX;
using ADODB;
using Infragistics.Win.UltraWinDataSource;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.AppStyling;
using SystemsAnalysis.Utils.MapInfoUtils;
using SystemsAnalysis.Analysis.CostEstimator.Classes;
using SystemsAnalysis.Modeling;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	public partial class Main : Form
	{
		#region Constants
		private string _LastSelectedModelDirectory;
    private const string COST_ESTIMATE_PROJECT_FILE_EXTENSION = ".cep";
		private const string ACCESS_FILE_EXTENSION = ".mdb";
		#endregion

		#region Variables
		private Project _project = null;
    private Infragistics.Win.ValueList _factorTypeListForGridCosts = new Infragistics.Win.ValueList();
		private Infragistics.Win.ValueList _factorTypeListForGridCostFactorPool = 
			new Infragistics.Win.ValueList();

		private Dictionary<string, ChildFormTemplate> _mainPages = new Dictionary<string, ChildFormTemplate>();

		private System.Threading.Thread splashThread;

		private UserSettings _UserSettings = new UserSettings();

		private Infragistics.Win.UltraWinTabControl.UltraTab lastTab = null;
		#endregion

		#region Constructors
		public Main()
		{
			splashThread = new System.Threading.Thread(new System.Threading.ThreadStart(DoSplash));
			splashThread.Start();

			InitializeComponent();

			System.IO.Stream appStylingStream = (Assembly.GetExecutingAssembly()).
				GetManifestResourceStream("SystemsAnalysis.Analysis.CostEstimator.UI.Resources.CostEstimator.isl");
			if (appStylingStream != null)
				StyleManager.Load(appStylingStream);
		}
		#endregion

		#region Methods

		#region Splash
		/// <summary>
		/// Do splash
		/// </summary>
		private static void DoSplash()
		{
			DoSplash(false);
		} // DoSplash()

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
				dateText = File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString("MMMM dd yyyy");
			} // if
			else
			{
				string version = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.ToString();
				int minorVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Minor;
				int majorVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Major;
				int build = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Build;
				int revision = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Revision;
				versionText = majorVersion + "." + minorVersion + "." + build + "." + revision;

				FileInfo fi = new FileInfo("PWMPDataSystem.exe");
				string date = fi.CreationTime.Date.ToString("MMMM dd yyyy");
				dateText = date;
			} // else

			SplashScreen sp = new SplashScreen(versionText, dateText);
			sp.ShowDialog(waitForClick);
		} // DoSplash(waitForClick)
		#endregion

		#region User Settings
		/// <summary>
		/// Load user configuration
		/// </summary>
		private void LoadUserConfiguration()
		{
			_UserSettings.SettingChanging += UserSettings_SettingChanging;

			// Form Size
			Binding bndSize = new Binding("Size", _UserSettings, "FormSize", true, DataSourceUpdateMode.OnPropertyChanged);
			this.DataBindings.Add(bndSize);

			// Form Location
			Binding bndLocation = new Binding("Location", _UserSettings, "FormLocation", true, DataSourceUpdateMode.OnPropertyChanged);
			DataBindings.Add(bndLocation);

			// Allow ENR CCI Changes
			Binding bndENRCCIChanges = new Binding("Checked", _UserSettings, "AllowENRCCIChange", true, DataSourceUpdateMode.OnPropertyChanged);
			chkAllowENRCCIChange.DataBindings.Add(bndENRCCIChanges);

			ArrayList mruFiles = _UserSettings.MostRecentlyUsedFiles;
			Infragistics.Win.UltraWinToolbars.ListTool mruTool =
				toolbarManager.Tools["Most Recently Used Files"] as Infragistics.Win.UltraWinToolbars.ListTool;
			Infragistics.Win.UltraWinToolbars.ListToolItemsCollection mruList =
				mruTool.ListToolItems;
			for (int i = 0; i < mruFiles.Count; i++)
			{
				string currentFile = (string)mruFiles[i];
				mruList.Add(currentFile, currentFile);
			}

			edtMasterPipXPLocation.Text = _UserSettings.MasterPipXPLocation;
			GenerateManholeCosts = !chkDoNotGenerateManholeCosts.Checked;
		} // LoadUserConfiguration()

		/// <summary>
		/// User settings _ setting changing
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">E</param>
		private void UserSettings_SettingChanging(object sender, SettingChangingEventArgs e)
		{
		} // UserSettings_SettingChanging(sender, e)

		/// <summary>
		/// User settings _ setting saving
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">E</param>
		private void UserSettings_SettingSaving(object sender, CancelEventArgs e)
		{
		} // UserSettings_SettingSaving(sender, e)

		/// <summary>
		/// Generate manhole costs
		/// </summary>
		/// <returns>Bool</returns>
		public bool GenerateManholeCosts { get; set; } // GenerateManholeCosts
		#endregion

		/// <summary>
		/// Set app caption
		/// </summary>
		private void SetAppCaption()
		{
			Text = string.Format("[{1}] - {0}", "Cost Estimator", 
				(_project == null || _project.FileName == "") ? "Untitled" : _project.FileName);
		} // SetAppCaption()

		/// <summary>
		/// Close project
		/// </summary>
		public void CloseProject()
		{
			CloseProject(false);
		} // CloseProject()

		public void CloseProject(bool forceClose)
		{
			bool canceled = false;
			if (_project != null)
			{
				if (!forceClose && _project.IsDirty)
				{
					DialogResult result = MessageBox.Show("Save project?", "Project has changed", MessageBoxButtons.YesNoCancel);
					switch (result)
					{
						case DialogResult.Yes:
							SaveProject();
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							canceled = true;
							break;
					}
				} // if
			}
			if (forceClose || !canceled)
			{
				_project = null;
				ClearPages();
				tabMain.SelectedTab = tabMain.Tabs["Home"];
			}
		}

		private void CreateNewProject()
		{
			CloseProject();
			_project = new Project();
			tabMain.SelectedTab = tabMain.Tabs["Costs"];
			(_mainPages["Costs"] as CostsPage).dsProject.Rows.SetCount(1, true);
		}

		private void SelectModelForAlternative()
		{
			dlgBrowseFolder.Description = "Browse for a model folder";
			if (Directory.Exists(_UserSettings.LastModelDirectory))
				dlgBrowseFolder.SelectedPath = _UserSettings.LastModelDirectory;
			if (dlgBrowseFolder.ShowDialog() == DialogResult.OK)
			{
				// Enumerate the alternatives under the alternatives directory
				string altsDir = dlgBrowseFolder.SelectedPath + @"\alternatives";
				if (Directory.Exists(altsDir))
				{
					lstvwAlternatives.Items.Clear();
					string[] subDirs = Directory.GetDirectories(altsDir);
					foreach (string subDir in subDirs)
					{
						Infragistics.Win.UltraWinListView.UltraListViewItem newItem =
							lstvwAlternatives.Items.Add(subDir, Path.GetFileNameWithoutExtension(subDir));
					} // foreach  (subDir)
					tabMain.SelectedTab = tabMain.Tabs["Select Alternative"];
					EnableRibbon(false);

					_UserSettings.LastModelDirectory = dlgBrowseFolder.SelectedPath;
					_UserSettings.Save();
				}
				else
				{
					MessageBox.Show("No alternatives directory found.");
				} // else
			} // if
		}

		/// <summary>
		/// Select model
		/// </summary>
		private void SelectModel()
		{
			dlgBrowseFolder.Description = "Browse for a model folder";
			if (Directory.Exists(_UserSettings.LastModelDirectory))
				dlgBrowseFolder.SelectedPath = _UserSettings.LastModelDirectory;
			if (dlgBrowseFolder.ShowDialog() == DialogResult.OK)
			{
				DialogResult result = MessageBox.Show("Select subset of model links?", "Select items", MessageBoxButtons.YesNoCancel);
				switch (result)
        {
        	case DialogResult.Yes:
						tabMain.SelectedTab = tabMain.Tabs["Select Estimate Items"];
						(_mainPages["Select Estimate Items"] as SelectEstimateItemsPage).ModelPath = dlgBrowseFolder.SelectedPath;
        		break;
					case DialogResult.No:
						AddModelEstimateToProject();
						EnableRibbon(true);
						break;
					case DialogResult.Cancel:
						break;
        }
				_UserSettings.LastModelDirectory = dlgBrowseFolder.SelectedPath;
				_UserSettings.Save();
			} // if
		} // SelectModel()

		/// <summary>
		/// View progress
		/// </summary>
		public void ViewProgress()
		{
			tabMain.SelectedTab = tabMain.Tabs["Progress"];
		} // ViewProgress()

		/// <summary>
		/// View project info
		/// </summary>
		public void ViewProjectInfo()
		{
			tabMain.SelectedTab = tabMain.Tabs["Project Info"];
		} // ViewProjectInfo()

		/// <summary>
		/// View cost factor pool
		/// </summary>
		public void ViewCostFactorPool()
		{
			tabMain.SelectedTab = tabMain.Tabs["Cost Factor Pool"];
		} // ViewCostFactorPool()

		/// <summary>
		/// View cost item pool
		/// </summary>
		public void ViewCostItemPool()
		{
			tabMain.SelectedTab = tabMain.Tabs["Cost Item Pool"];
		} // ViewCostItemPool()

		/// <summary>
		/// View costs
		/// </summary>
		public void ViewCosts()
		{
			tabMain.SelectedTab = tabMain.Tabs["Costs"];
		} // ViewCosts()

		/// <summary>
		/// View report
		/// </summary>
		public void ViewReport()
		{
			tabMain.SelectedTab = tabMain.Tabs["Final"];
		} // ViewReport()

		private void AddFileToMRUList(string fileName)
		{
			ArrayList mruFiles = _UserSettings.MostRecentlyUsedFiles;
			Infragistics.Win.UltraWinToolbars.ListTool mruTool =
				toolbarManager.Tools["Most Recently Used Files"] as Infragistics.Win.UltraWinToolbars.ListTool;
			Infragistics.Win.UltraWinToolbars.ListToolItemsCollection mruList =
				mruTool.ListToolItems;
			if (mruFiles.Contains(fileName))
				mruFiles.Remove(fileName);
			mruFiles.Insert(0, fileName);
			if (mruFiles.Count > _UserSettings.NumMostRecentlyUsedFiles)
				mruFiles.RemoveAt(mruFiles.Count - 1);

			if (mruList.Contains(fileName))
				mruList.Remove(mruList[fileName]);
			mruList.Insert(0, fileName, fileName);
			if (mruList.Count > _UserSettings.NumMostRecentlyUsedFiles)
				mruList.RemoveAt(mruList.Count - 1);


		}
		/// <summary>
		/// Open project
		/// </summary>
		private void OpenProject()
		{
			dlgOpen.DefaultExt = COST_ESTIMATE_PROJECT_FILE_EXTENSION;
			dlgOpen.Title = "Open Cost Estimate Project File";
			dlgOpen.Filter = "Cost Estimate Project (*.cep)|*.cep|All files (*.*)|*.*";
			dlgOpen.FilterIndex = 0;
			dlgOpen.FileName = "";
			if (dlgOpen.ShowDialog() == DialogResult.OK)
			{
				if (_project != null)
					CloseProject();

				_project = new Project();
				_project.LoadFromFile(dlgOpen.FileName);
				_project.FileName = dlgOpen.FileName;
				SetupProjectForCostGrid();

				string fileName = dlgOpen.FileName;

				AddFileToMRUList(fileName);
				SetAppCaption();
			} // if
		} // OpenProject()

		/// <summary>
		/// Open project
		/// </summary>
		/// <param name="fileName">File name</param>
		private void OpenProject(string fileName)
		{
			if (_project != null)
				CloseProject();

			_project = new Project();
			_project.LoadFromFile(fileName);
			_project.FileName = fileName;
			SetupProjectForCostGrid();

			AddFileToMRUList(fileName);
			SetAppCaption();
		} // OpenProject(fileName)

		/// <summary>
		/// Save project
		/// </summary>
		private void SaveProject()
		{
			if (_project.FileName.Length == 0)
				SaveProjectAs();
			else
			{
				if (!_project.SaveToFile(_project.FileName))
					{
						MessageBox.Show(String.Format("An error occurred during save.  The file {0} may be corrupt.\n\n", dlgSave.FileName));
						return;
					}
				AddFileToMRUList(_project.FileName);
			}
			SetAppCaption();
		} // SaveProject()

		/// <summary>
		/// Save project as
		/// </summary>
		private void SaveProjectAs()
		{
			dlgSave.DefaultExt = COST_ESTIMATE_PROJECT_FILE_EXTENSION;
			dlgSave.Title = "Save Cost Estimate Project File";
			dlgSave.InitialDirectory = _LastSelectedModelDirectory;
			dlgSave.Filter = "Cost Estimate Project (*.cep)|*.cep|All files (*.*)|*.*";
			dlgSave.FilterIndex = 0;
			dlgSave.FileName = "";
			if (dlgSave.ShowDialog() == DialogResult.OK)
			{
				if (!_project.SaveToFile(dlgSave.FileName))
				{
					MessageBox.Show(String.Format(
						"An error occurred during save.  The file {0} may be corrupt.\n\n", 
						dlgSave.FileName));
					return;
				}
				_project.FileName = dlgSave.FileName;
				AddFileToMRUList(_project.FileName);
				SetAppCaption();
			} // if
		} // SaveProjectAs()

		/// <summary>
		/// View global settings
		/// </summary>
		private void ViewGlobalSettings()
		{
			dsGlobalPipeSettings.Rows.SetCount(1);
			dsGlobalPipeSettings.Rows[0].GetChildRows("Params").SetCount(_project.PipeCoster.CosterParameters.Count);
			dsGlobalPipeSettings.Rows[0].GetChildRows("ConcretePipe").SetCount(_project.PipeCoster.PipeCostTable(PipeMaterial.Concrete).Count);
			dsGlobalPipeSettings.Rows[0].GetChildRows("CIPP").SetCount(_project.PipeCoster.PipeCostTable(PipeMaterial.CIPP).Count);
			dsGlobalPipeSettings.Rows[0].GetChildRows("Pipeburst").SetCount(_project.PipeCoster.PipeCostTable(PipeMaterial.Pipeburst).Count);
			dsGlobalPipeSettings.Rows[0].GetChildRows("PVCHDPE").SetCount(_project.PipeCoster.PipeCostTable(PipeMaterial.PVCHDPE).Count);
			dsGlobalInflowControlSettings.Rows.SetCount(_project.InflowControlCoster.CosterParameters.Count);
			tabMain.SelectedTab = tabMain.Tabs["Global Settings"];
		} // ViewGlobalSettings()

		/// <summary>
		/// Batch alternatives
		/// </summary>
		private void BatchAlternatives()
		{
			tabMain.SelectedTab = tabMain.Tabs["Batch"];
		} // BatchAlternatives()

    /// <summary>
		/// Enable ribbon
		/// </summary>
		private void EnableRibbon(bool isEnabled)
		{
			foreach (Infragistics.Win.UltraWinToolbars.ToolBase tool in toolbarManager.Tools)
				tool.SharedProps.Enabled = isEnabled;
		} // EnableRibbon()

		/// <summary>
		/// Setup data set for cost item factor
		/// </summary>
		/// <param name="aCostItemFactor">A cost item factor</param>
		private void SetupDataSetForCostItemFactor(CostItemFactor aCostItemFactor,
			UltraDataRow aCostItemFactorRow, int depth)
		{
			UltraDataRowsCollection costItemRows = aCostItemFactorRow.GetChildRows(
				string.Format("CostItem{0}", depth + 1));

			UltraDataRowsCollection factorsRows = aCostItemFactorRow.GetChildRows(
				string.Format("Factors{0}", depth + 1));

			string costItemFactorLevelName = string.Format("CostItemFactor{0}", depth + 1);
			UltraDataRowsCollection costItemFactorRows;
			List<string> childBandNames = new List<string>();
			foreach (UltraDataBand band in aCostItemFactorRow.Band.ChildBands)
			{
				childBandNames.Add(band.Key);
			}
			if (childBandNames.Contains(costItemFactorLevelName))
				costItemFactorRows = aCostItemFactorRow.GetChildRows(costItemFactorLevelName);
			else
				costItemFactorRows = null;

			costItemRows.SetCount(aCostItemFactor.CostItem != null ? 1 : 0);
			factorsRows.SetCount(aCostItemFactor.CostFactorsCount);

			if (costItemFactorRows != null)
			{
				costItemFactorRows.SetCount(aCostItemFactor.ChildCostItemFactorCount);

				for (int i = 0; i < aCostItemFactor.ChildCostItemFactorCount; i++)
				{
					SetupDataSetForCostItemFactor(aCostItemFactor.ChildCostItemFactor(i), costItemFactorRows[i], depth + 1);
				}
			}
		} // SetupDataSetForCostItemFactor(aCostItemFactor)

		#region PipXP Processing
		/// <summary>
		/// Removes the pipxp_ac.mdb file from the Links directory
		/// </summary>
		/// <param name="modelPath">Model path</param>
		private void CleanupPipXP(string modelPath)
		{
			string pipXPTableFileName = modelPath + Path.DirectorySeparatorChar + "Links" +
				Path.DirectorySeparatorChar + "mdl_pipxp_ac.mdb";
			if (File.Exists(pipXPTableFileName))
			{
				File.Delete(pipXPTableFileName);
			} // if

			using (System.Data.OleDb.OleDbConnection modelConn = 
				new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" +
					"Data Source=" + modelPath + Path.DirectorySeparatorChar + "mdbs" + 
					Path.DirectorySeparatorChar + "DataAccess.mdb"))
			{
				modelConn.Open();
				using (System.Data.OleDb.OleDbCommand deleteCmd = 
					new System.Data.OleDb.OleDbCommand("DROP TABLE mdl_pipxp_ac", modelConn))
        {
					try
					{
						deleteCmd.ExecuteNonQuery();
					} // try
					catch (System.Data.OleDb.OleDbException e)
					{
					}
        }
				// catch
				modelConn.Close();
			}
		} // CleanupPipXP(modelPath)

		/// <summary>
		/// Model has pip XP table
		/// </summary>
		/// <param name="modelPath">Model path</param>
		/// <returns>True if model has a PipXP table in the Links directory</returns>
		private bool ModelHasPipXPTable(string modelPath)
		{
			string pipXPTableFileName = modelPath + Path.DirectorySeparatorChar + "Links" +
				Path.DirectorySeparatorChar + "mdl_pipxp_ac.mdb";
			return File.Exists(pipXPTableFileName);
		} // ModelHasPipXPTable(modelPath)

		/// <summary>
		/// Copy master pip XP data
		/// </summary>
		/// <param name="modelPath">Model path</param>
		private void CopyMasterPipXPData(string modelPath)
		{
			try
			{
				string masterPipXPLocation = _UserSettings.MasterPipXPLocation;
				using (System.Data.OleDb.OleDbConnection sourceConn =
					new System.Data.OleDb.OleDbConnection(String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"{0}\"", 
						masterPipXPLocation)))
				{
					sourceConn.Open();
					string masterPipXPTableName = Path.GetFileNameWithoutExtension(masterPipXPLocation);
					System.Data.OleDb.OleDbCommand copyCmd = new System.Data.OleDb.OleDbCommand("SELECT * INTO [MS Access;DATABASE=" + modelPath + Path.DirectorySeparatorChar + "Links" + Path.DirectorySeparatorChar + "mdl_pipxp_ac.mdb;].[mdl_pipxp_ac] FROM " + "(SELECT " + masterPipXPTableName + ".* " + "FROM " + masterPipXPTableName + " INNER JOIN [MS Access;DATABASE=" + modelPath + Path.DirectorySeparatorChar + "Links" + Path.DirectorySeparatorChar + "mdl_links_ac.mdb].[mdl_links_ac] AS mdl_links " + "ON " + masterPipXPTableName + ".MLinkID = mdl_links.MLinkID)", sourceConn);
					copyCmd.ExecuteNonQuery();
					sourceConn.Close();
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(String.Format("Error occurred while copying master pipxp data:\n\n{0}", e.Message));
			} // catch
		} // CopyMasterPipXPData(modelPath)

		/// <summary>
		/// Link pip XP to data access DB
		/// </summary>
		/// <param name="modelPath">Model path</param>
		private void LinkPipXPToDataAccessDB(string modelPath)
		{
			Catalog dataAccessCatalog = new CatalogClass();
			Connection dataAccessConn = new ConnectionClass();
			dataAccessConn.Open("Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" +
				modelPath + Path.DirectorySeparatorChar + "mdbs" +
				Path.DirectorySeparatorChar + "DataAccess.mdb;" +
				"Jet OLEDB:Engine Type=5", "admin", "", -1);
			dataAccessCatalog.ActiveConnection = dataAccessConn;
			Table pipXPLinkedTable = new Table 
			{ 
				Name = "mdl_pipxp_ac", 
				ParentCatalog = dataAccessCatalog 
			};
			pipXPLinkedTable.Properties["Jet OLEDB:Create Link"].Value = true;
			pipXPLinkedTable.Properties["Jet OLEDB:Link Datasource"].Value =
				modelPath + Path.DirectorySeparatorChar + "Links" +
				Path.DirectorySeparatorChar + "mdl_pipxp_ac.mdb";
			pipXPLinkedTable.Properties["Jet OLEDB:Link Provider String"].Value =
				"MS Access;";
			pipXPLinkedTable.Properties["Jet OLEDB:Remote Table Name"].Value = "mdl_pipxp_ac";
			dataAccessCatalog.Tables.Append(pipXPLinkedTable);
			dataAccessConn.Close();
		} // LinkPipXPToDataAccessDB(modelPath)

		/// <summary>
		/// Create pip XP table
		/// </summary>
		/// <param name="modelPath">Model path</param>
		/// <returns>Bool</returns>
		private bool CreatePipXPTable(string modelPath)
		{
			string pipXPTableFileName = modelPath + Path.DirectorySeparatorChar + "Links" +
				Path.DirectorySeparatorChar + "mdl_pipxp_ac.mdb";

			if (File.Exists(pipXPTableFileName))
				CleanupPipXP(modelPath);

			CatalogClass catalog = new CatalogClass();
			string createDBSpec = String.Format(
				"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Engine Type=5", 
				pipXPTableFileName);
			catalog.Create(createDBSpec);

			CopyMasterPipXPData(modelPath);
			LinkPipXPToDataAccessDB(modelPath);

			return ModelHasPipXPTable(modelPath);
		} // CreatePipXPTable(modelPath)
		#endregion

		#region AltPipXP Processing
		/// <summary>
		/// Removes the alt_pipxp_ac table and associated files, as well as the alt_pipxp_ac
		/// table from alternative_package.mdb.
		/// </summary>
		/// <param name="altInfo">The selected alternative's info</param>
		private void CleanupAltPipXP(SelectedAlternative altInfo)
		{
			string altPipXPTableFileName = altInfo.AlternativePath + Path.DirectorySeparatorChar +
				"alt_pipxp_ac.mdb";
			string qaqcPipXPFileName = altInfo.AlternativePath + Path.DirectorySeparatorChar +
				"QaQc_Pipxp.wor";

			if (File.Exists(altPipXPTableFileName))
			{
				string[] altPipXPFiles = Directory.GetFiles(altInfo.AlternativePath, "alt_pipxp_ac.*");
				foreach (string altPipXPFile in altPipXPFiles)
				{
					File.Delete(altPipXPFile);
				} // foreach  (altPipXPFile)
			} // if

			if (File.Exists(qaqcPipXPFileName))
			{
				File.Delete(qaqcPipXPFileName);
			} // if

			System.Data.OleDb.OleDbConnection altPackageConn = new System.Data.OleDb.OleDbConnection(
				"Provider=Microsoft.Jet.OLEDB.4.0;" +
				"Data Source=" + altInfo.AlternativePath + Path.DirectorySeparatorChar + "alternative_package.mdb");
			altPackageConn.Open();
			System.Data.OleDb.OleDbCommand deleteCmd = new System.Data.OleDb.OleDbCommand(
				"DROP TABLE alt_pipxp_ac", altPackageConn);
			try
			{
				deleteCmd.ExecuteNonQuery();
			}
			catch (System.Data.OleDb.OleDbException e)
			{
			} // catch
			altPackageConn.Close();
		} // CleanupAltPipXP(altInfo)

		/// <summary>
		/// Alternative has alt pip XP table
		/// </summary>
		/// <param name="altInfo">Alt info</param>
		/// <returns>Bool</returns>
		private bool AlternativeHasAltPipXPTable(SelectedAlternative altInfo)
		{
			string altPipXPTableFileName = altInfo.AlternativePath + Path.DirectorySeparatorChar +
				"alt_pipxp_ac.mdb";
			return File.Exists(altPipXPTableFileName);
		} // AlternativeHasAltPipXPTable(altInfo)

		/// <summary>
		/// Copy QA alt pip XP workspace
		/// </summary>
		/// <returns>Bool</returns>
		private bool CopyQAAltPipXPWorkspace(SelectedAlternative altInfo)
		{
			string qaWorkspacePath = Path.GetDirectoryName(Application.ExecutablePath);
			string altPath = altInfo.AlternativePath;
			File.Copy(qaWorkspacePath + Path.DirectorySeparatorChar + "QaQc_PipXP.WOR",
				altPath + Path.DirectorySeparatorChar + "QaQc_PipXP.WOR");
			return true;
		} // CopyQAAltPipXPWorkspace()

		/// <summary>
		/// Create alt pip XP table
		/// </summary>
		/// <returns>Bool</returns>
		private bool CreateAltPipXPTable(SelectedAlternative altInfo)
		{
			string localAppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
				Path.DirectorySeparatorChar + "AltPipXP";
			if (!Directory.Exists(localAppPath))
				Directory.CreateDirectory(localAppPath);

			CopyQAAltPipXPWorkspace(altInfo);

			string altPipXPIniFileName = localAppPath + Path.DirectorySeparatorChar + "AltPipXP.ini";
			bool readyToFireAltPipXP = false;
			try
			{
				TextWriter tw = new StreamWriter(altPipXPIniFileName);
				tw.WriteLine("[Alternative]");
				tw.WriteLine("Path=" + "\"" + altInfo.AlternativePath + "\"");
				tw.Close();
				readyToFireAltPipXP = true;
			}
			catch (Exception)
			{
				MessageBox.Show("Error writing to " + altPipXPIniFileName);
			} // catch (Exception)

			if (readyToFireAltPipXP)
			{
				MapBasicEngine mbe = null;
				try
				{
					mbe = new MapBasicEngine();
					mbe.ExecuteLibrary("AltPipXP.MBX");
				}
				catch (Exception e)
				{
					MessageBox.Show("Error running AltPipXP.MBX\n" + e.Message);
				} // catch (Exception)
				finally
				{
					mbe.CloseMapInfo();
				} // finally
			} // if

			return AlternativeHasAltPipXPTable(altInfo);
		} // CopyAltPipXPTableToAlternativePackage()

		/// <summary>
		/// Copy alt pip XP table to alt package
		/// </summary>
		/// <param name="altInfo">Alt info</param>
		/// <returns>Bool</returns>
		private bool CopyAltPipXPTableToAltPackage(SelectedAlternative altInfo)
		{
			bool copyWasSuccessful = true;
			try
			{
				System.Data.OleDb.OleDbConnection sourceConn = new System.Data.OleDb.OleDbConnection(
					"Provider=Microsoft.Jet.OLEDB.4.0;" +
					"Data Source=\"" + altInfo.AlternativePath + Path.DirectorySeparatorChar + "alt_PipXP_ac.mdb" + "\"");
				sourceConn.Open();
				System.Data.OleDb.OleDbCommand copyCmd = new System.Data.OleDb.OleDbCommand(
					"SELECT * INTO [MS Access;DATABASE=" + altInfo.AlternativePath + Path.DirectorySeparatorChar + "alternative_package.mdb;]" +
					".[alt_pipxp_ac] FROM alt_PipXP_ac", sourceConn);
				copyCmd.ExecuteNonQuery();
				sourceConn.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show("Error:" + e.Message, "Error adding altpipxp table to alternative_package.mdb");
				copyWasSuccessful = false;
			} // catch

			return copyWasSuccessful;
		} // CopyAltPipXPTableToAltPackage(altInfo)
		#endregion

		/// <summary>
		/// Add alternative estimate to project
		/// </summary>
		private void AddAlternativeEstimateToProject()
		{
			if (_project == null)
				_project = new Project();

			string modelPath = dlgBrowseFolder.SelectedPath;
			_LastSelectedModelDirectory = modelPath;
			string selectedAlt = (string)lstvwAlternatives.ActiveItem.Value;
			SelectedAlternative altInfo = new SelectedAlternative(modelPath, selectedAlt);

			if (!AlternativeHasAltPipXPTable(altInfo))
			{
				lblProgress.Text = "Creating pipe conflict table";
				lblProgress.Refresh();
				bool createAltPipXPTableWasSuccessful = CreateAltPipXPTable(altInfo);
				if (!createAltPipXPTableWasSuccessful)
				{
					MessageBox.Show("A problem occurred creating the conflict table.  Please try again.", "Problem creating conflict table");
					return;
				} // if
				
				bool copyAltPipXPTableWasSuccessful = CopyAltPipXPTableToAltPackage(altInfo);
				if (!copyAltPipXPTableWasSuccessful)
				{
					MessageBox.Show("A problem occurred copying the conflict table.  Please try again.", "Problem creating conflict table");
					return;
				} // if
			} // if

			lblProgress.Text = "Loading alternative";
			prgMainProgress.Value = 0;
			tabMain.SelectedTab = tabMain.Tabs["Progress"];
			bkgWorkerLoadAlternative.RunWorkerAsync(altInfo);
		} // AddAlternativeEstimateToProject()

		/// <summary>
		/// Add model estimate to project
		/// </summary>
		public void AddModelEstimateToProject()
		{
			if (_project == null)
				_project = new Project();

			string modelPath = dlgBrowseFolder.SelectedPath;

			if (!ModelHasPipXPTable(modelPath))
			{
				lblProgress.Text = "Creating pipe conflict table";
				lblProgress.Refresh();
				bool createWasSuccessful = CreatePipXPTable(modelPath);
				if (!createWasSuccessful)
				{
					MessageBox.Show("A problem occurred creating the conflict table.  Please try again.", "Problem creating conflict table");
					return;
				} // if
			} // if

			lblProgress.Text = "Loading model";
			prgMainProgress.Value = 0;
			tabMain.SelectedTab = tabMain.Tabs["Progress"];

			LoadModelPackage loadModelPackage = new LoadModelPackage();
			loadModelPackage.ModelPath = modelPath;
			loadModelPackage.LinksToEstimate = null;

			bkgWorkerLoadModel.RunWorkerAsync(loadModelPackage);

		} // AddModelEstimateToProject()

		/// <summary>
		/// Add model estimate to project with item selection
		/// </summary>
		public void AddModelEstimateToProjectWithItemSelection()
		{
			if (_project == null)
				_project = new Project();

			string modelPath = dlgBrowseFolder.SelectedPath;

			if (!ModelHasPipXPTable(modelPath))
			{
				lblProgress.Text = "Creating pipe conflict table";
				lblProgress.Refresh();
				CreatePipXPTable(modelPath);
			} // if

			lblProgress.Text = "Loading model";
			prgMainProgress.Value = 0;
			tabMain.SelectedTab = tabMain.Tabs["Progress"];

			LoadModelPackage loadModelPackage = new LoadModelPackage();
			loadModelPackage.ModelPath = modelPath;

			List<EstimateItemSelection> selectedItems = 
				(_mainPages["Select Estimate Items"] as SelectEstimateItemsPage).ItemSelectionList;
			List<int> selectedMLinkIds = new List<int>();
			foreach (EstimateItemSelection item in selectedItems)
			{
				if (item.Selected)
					selectedMLinkIds.Add((item.Object as Link).MLinkID);
			} // foreach  (item)

			loadModelPackage.LinksToEstimate = selectedMLinkIds;

			bkgWorkerLoadModel.RunWorkerAsync(loadModelPackage);
		} // AddModelEstimateToProjectWithItemSelection()

		/// <summary>
		/// Setup project for cost grid
		/// </summary>
		private void SetupProjectForCostGrid()
		{
			tabMain.SelectedTab = tabMain.Tabs["Costs"];
			(_mainPages["Costs"] as CostsPage).dsProject.Rows.SetCount(1);
			UltraDataRowsCollection estimateRows = (_mainPages["Costs"] as CostsPage).dsProject.Rows[0].GetChildRows("CostItemFactor1");
			estimateRows.SetCount(_project.EstimateCount);

			lblProgress.Text = "Setting up data";
			lblProgress.Refresh();
			int numEstimates = _project.EstimateCount;
			for (int i = 0; i < _project.EstimateCount; i++)
			{
				prgMainProgress.Value = (int)((double)i / (double)numEstimates * 100);
				prgMainProgress.Refresh();
				CostItemFactor estimateCostItemFactor = _project.Estimate(i);
				UltraDataRow estimateCostItemFactorRow = estimateRows[i];
				SetupDataSetForCostItemFactor(estimateCostItemFactor, estimateCostItemFactorRow, 1);
			} // for
			prgMainProgress.Value = 0;

			(_mainPages["Costs"] as CostsPage).dsProject.ResetCachedValues();
			(_mainPages["Costs"] as CostsPage).gridCosts.Refresh();
		} // SetupProjectForCostGrid()

		private void CloseTabAndReturnToProject()
		{
			if (_project != null)
				tabMain.SelectedTab = tabMain.Tabs["Costs"];
			else
				tabMain.SelectedTab = tabMain.Tabs["Home"];
		}

		/// <summary>
		/// Initialize page
		/// </summary>
		/// <param name="pageBaseName">Page base name</param>
		private void InitializePage(string pageBaseName)
		{
			object[] pageConstructorParams = new object[] { _project };

			_mainPages.Add(pageBaseName,
				Activator.CreateInstance(Type.GetType(string.Format(
				"SystemsAnalysis.Analysis.CostEstimator.UI.{0}{1}",
				pageBaseName.Replace(" ", null), "Page")),
				pageConstructorParams) as ChildFormTemplate);
			_mainPages[pageBaseName].Initialize(tabMain.Tabs[pageBaseName].TabPage, this);

			if (pageBaseName == "Costs")
				PassSettingsToCostsPage();
		} // InitializePage(pageBaseName)

		/// <summary>
		/// Clear pages
		/// </summary>
		private void ClearPages()
		{
			foreach (KeyValuePair<string, ChildFormTemplate> kvPair in _mainPages)
				kvPair.Value.Dispose();
			_mainPages.Clear();
		} // ClearPages()

		#endregion

		// Events

		private void Main_Load(object sender, EventArgs e)
		{
			try
			{
				LoadUserConfiguration();

				tabMain.SelectedTab = tabMain.Tabs["Home"];
				tabMain.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
				Infragistics.Win.Office2007ColorTable.ColorScheme = Infragistics.Win.Office2007ColorScheme.Black;
				Infragistics.Win.Office2007ColorTable.CustomBlendColor = Color.SteelBlue;

				// Setup shortcut on EMGAATS menu
				if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
				{
					try
					{
						if (System.Deployment.Application.ApplicationDeployment.CurrentDeployment.IsFirstRun)
						{
							File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) +
								"\\Programs\\BES Systems Analysis\\Cost Estimator.appref-ms",
								Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) +
								"\\EMGAATS\\Cost Estimator.appref-ms", true);
						} // if
					} // try
					catch (System.Deployment.Application.InvalidDeploymentException ex)
					{
					} // catch
					catch (Exception ex)
					{
						MessageBox.Show("Exception:\n" + ex.Message);
					} // catch
				} // if

				// Setup cost grid for factors
				foreach (string s in Enum.GetNames(typeof(CostFactorType)))
				{
					CostFactorType itemToAdd = (CostFactorType)Enum.Parse(typeof(CostFactorType), s);
					_factorTypeListForGridCosts.ValueListItems.Add(itemToAdd, s);
					_factorTypeListForGridCostFactorPool.ValueListItems.Add(itemToAdd, s);
				}
			}
			finally
			{
				if (splashThread.IsAlive)
					splashThread.Abort();
				SetAppCaption();
			} // finally
		}

		private void toolbarManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
		{
			if (tabMain.SelectedTab == tabMain.Tabs["Costs"])
				(_mainPages["Costs"] as CostsPage).UpdateCurrentlyEditingRow();

			switch (e.Tool.Key)
			{
				case "New Project":    // ButtonTool
					CreateNewProject();
					break;

				case "Open Project":    // ButtonTool
					OpenProject();
					break;

				case "Save Project":    // ButtonTool
					SaveProject();
					break;

				case "Close Project":    // ButtonTool
					CloseProject();
					break;

				case "Save Project As":    // ButtonTool
					SaveProjectAs();
					break;

				case "Estimate from Alternative":    // ButtonTool
					SelectModelForAlternative();
					break;

				case "Hide Add Items":    // StateButtonTool
					(_mainPages["Costs"] as CostsPage).HideAddItemsFromCostGrid(
						(e.Tool as Infragistics.Win.UltraWinToolbars.StateButtonTool).Checked);
					break;

				case "Exit Cost Estimator":    // ButtonTool
					Close();
					break;

				case "Change Global Settings":    // ButtonTool
					ViewGlobalSettings();
					break;
				
				case "Estimate from Model":    // ButtonTool
					SelectModel();
					break;

				case "Batch Alternatives":    // ButtonTool
					BatchAlternatives();
					break;

				case "Cleanup AltPipXP":    // ButtonTool
					string selectedAltFolder = "";
					if (dlgBrowseFolder.ShowDialog() == DialogResult.OK)
						selectedAltFolder = dlgBrowseFolder.SelectedPath;
					int lastDirSepIndex = selectedAltFolder.LastIndexOf(Path.DirectorySeparatorChar);
					string modelPath = selectedAltFolder + "\\..\\..";
					string altName = selectedAltFolder.Substring(lastDirSepIndex + 1,
						selectedAltFolder.Length - lastDirSepIndex - 1);
					SelectedAlternative selectedAlt = new SelectedAlternative(modelPath,
						altName);
					CleanupAltPipXP(selectedAlt);
					break;

				case "Most Recently Used Files":    // ListTool
					Infragistics.Win.UltraWinToolbars.ListTool mruListTool = toolbarManager.Tools["Most Recently Used Files"] as
						Infragistics.Win.UltraWinToolbars.ListTool;
					string fileName = mruListTool.SelectedItem.Key.ToString();
					OpenProject(fileName);
					break;

				case "Options":    // ButtonTool
					lastTab = tabMain.SelectedTab;
					tabMain.SelectedTab = tabMain.Tabs["Options"];
					break;
			}
		}

		private void btnOkSelectAlternative_Click(object sender, EventArgs e)
		{
			AddAlternativeEstimateToProject();
			EnableRibbon(true);
		}

		private void lstvwAlternatives_ItemDoubleClick(object sender, Infragistics.Win.UltraWinListView.ItemDoubleClickEventArgs e)
		{
			AddAlternativeEstimateToProject();
			EnableRibbon(true);
		}

		private void bkgWorkerLoadAlternative_DoWork(object sender, DoWorkEventArgs e)
		{
			SelectedAlternative altInfo = (SelectedAlternative)e.Argument;
			BackgroundWorker bw = sender as BackgroundWorker;
			string error = string.Empty;
			bool noError = _project.CreateEstimateFromAlternative(bw, altInfo.ModelPath, altInfo.AlternativeName, out error);
			e.Result = new ErrorInfo(noError, error);
		}

		private void bkgWorkerLoadAlternative_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			prgMainProgress.Value = e.ProgressPercentage;
			prgMainProgress.Refresh();
			if (e.UserState != null)
			{
				lblProgress.Text = (string)e.UserState;
				lblProgress.Refresh();
			}
		}

		private void bkgWorkerLoadAlternative_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			ErrorInfo errorInfo = (ErrorInfo)e.Result;
			if (errorInfo.NoError == false)
			{
				MessageBox.Show("An error occurred while costing the alternative. " +
					"Check the alternative for consistency.\n" + errorInfo.Message);
				CloseProject(true);
			}
			else
				SetupProjectForCostGrid();
		}

		private void btnCancelSelectAlternative_Click(object sender, EventArgs e)
		{
			CloseProject();
			EnableRibbon(true);
		}

		private void dsGlobalPipeSettings_CellDataRequested(object sender, CellDataRequestedEventArgs e)
		{
			int currentRowIndex = e.Row.Index;
			
			switch (e.Row.Band.Key)
			{
				case "PipeCosterSettings":
					e.Data = "Settings";
					break;
				case "Params":
					switch (e.Column.Key)
					{
						case "Name":
							e.Data = _project.PipeCoster.CosterParameters.Keys[currentRowIndex];
							break;
						case "Value":
							e.Data = Convert.ChangeType(_project.PipeCoster.CosterParameters.Values[currentRowIndex].Value,
								Type.GetType(_project.PipeCoster.CosterParameters.Values[currentRowIndex].Type));
							break;
					}
					break;
				case "ConcretePipe":
				case "CIPP":
				case "Pipeburst":
				case "PVCHDPE":
					PipeMaterial material = new PipeMaterial();
					switch (e.Row.Band.Key)
					{
						case "ConcretePipe":
							material = PipeMaterial.Concrete;
							break;
						case "CIPP":
							material = PipeMaterial.CIPP;
							break;
						case "Pipeburst":
							material = PipeMaterial.Pipeburst;
							break;
						case "PVCHDPE":
							material = PipeMaterial.PVCHDPE;
							break;
					}
					SortedList<double, decimal> costTable = _project.PipeCoster.PipeCostTable(material);
					switch (e.Column.Key)
					{
						case "Name":
							e.Data = string.Format("{0:F0} in diam", costTable.Keys[currentRowIndex]);
							break;
						case "Value":
							e.Data = costTable.Values[currentRowIndex];
							break;
					}
					break;
				case "Lateral":
					break;
				case "Manhole":
					SortedList<double, ManholeCosts> manholeCostTable = _project.PipeCoster.ManholeCostTable();
					switch (e.Column.Key)
					{
						case "Name":
							e.Data = string.Format("{0:F0} in diam", manholeCostTable.Keys[currentRowIndex]);
							break;
						case "MinimumCost":
							e.Data = manholeCostTable.Values[currentRowIndex].BaseCost;
							break;
						case "CostPerFtDepthAbove8ft":
							e.Data = manholeCostTable.Values[currentRowIndex].CostPerFtDepthAbove8Ft;
							break;
					}
					break;
				case "FlowDiversion":
					SortedList<double, decimal> flowDiversionCostTable = _project.PipeCoster.FlowDiversionCostTable();
					switch (e.Column.Key)
					{
						case "Name":
							e.Data = string.Format("{0:F0} in diam", flowDiversionCostTable.Keys[currentRowIndex]);
							break;
						case "Value":
							e.Data = flowDiversionCostTable.Values[currentRowIndex];
							break;
					}
					break;
			}
		}

		private void btnCloseGlobalSettings_Click(object sender, EventArgs e)
		{
			tabMain.SelectedTab = tabMain.Tabs["Costs"];
		}

		/// <summary>
		/// Ds global inflow control settings _ cell data requested
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">E</param>
		private void dsGlobalInflowControlSettings_CellDataRequested(object sender, CellDataRequestedEventArgs e)
		{
			int currentRowIndex = e.Row.Index;
			switch (e.Column.Key)
			{
				case "Name":
					e.Data = _project.InflowControlCoster.CosterParameters.Keys[currentRowIndex];
					break;
				case "Value":
					e.Data = Convert.ChangeType(_project.InflowControlCoster.CosterParameters.Values[currentRowIndex].Value,
						Type.GetType(_project.InflowControlCoster.CosterParameters.Values[currentRowIndex].Type));
					break;
			} // switch
		}

		private void tabMain_SelectedTabChanging(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs e)
		{
			switch (e.Tab.Key)
			{
				case "Costs":
				case "Cost Factor Pool":
				case "Cost Item Pool":
				case "Batch":
				case "Project Info":
				case "Final":
				case "Select Estimate Items":
					if (!_mainPages.ContainsKey(e.Tab.Key))
						InitializePage(e.Tab.Key);
					break;
			}
			toolbarManager.RefreshMerge();
		}

		private void bkgWorkerLoadModel_DoWork(object sender, DoWorkEventArgs e)
		{
			LoadModelPackage loadModelPackage = (LoadModelPackage)e.Argument;
			string modelPath = loadModelPackage.ModelPath;
			List<int> linksToEstimate = loadModelPackage.LinksToEstimate;
			BackgroundWorker bw = sender as BackgroundWorker;
			string error = string.Empty;
			bool noError = _project.CreateEstimateFromModel(bw, modelPath, linksToEstimate, 
				GenerateManholeCosts, out error);
			e.Result = new ErrorInfo(noError, error);
		}

		private void bkgWorkerLoadModel_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			prgMainProgress.Value = e.ProgressPercentage;
			prgMainProgress.Refresh();
			if (e.UserState != null)
			{
				lblProgress.Text = (string)e.UserState;
				lblProgress.Refresh();
			}
		}

		private void bkgWorkerLoadModel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			ErrorInfo errorInfo = (ErrorInfo)e.Result;
			if (errorInfo.NoError == false)
			{
				MessageBox.Show("An error occurred while costing the model. " +
					"Check the model for consistency.\n" + errorInfo.Message);
				CloseProject(true);
			}
			else
				SetupProjectForCostGrid();
		}

		/// <summary>
		/// Pass settings to costs page
		/// </summary>
		private void PassSettingsToCostsPage()
		{
			CostsPage costsPage = (CostsPage)_mainPages["Costs"];
			costsPage.Settings = _UserSettings;
		} // PassSettingsToCostsPage()

		private void btnEstimateFromAlternative_Click(object sender, EventArgs e)
		{
			SelectModelForAlternative();
		}

		private void btnOpenEstimate_Click(object sender, EventArgs e)
		{
			OpenProject();
		}

		private void btnEstimateFromModel_Click(object sender, EventArgs e)
		{
			SelectModel();
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool canceled = false;
			if (_project != null)
			{
				if (_project.IsDirty)
				{
					DialogResult result = MessageBox.Show("Save project?", "Project has changed", MessageBoxButtons.YesNoCancel);
					switch (result)
					{
						case DialogResult.Yes:
							SaveProject();
							break;
						case DialogResult.No:
							break;
						case DialogResult.Cancel:
							canceled = true;
							break;
					}
				} // if
			}
			if (!canceled)
			{
				_project = null;
				ClearPages();
				tabMain.SelectedTab = tabMain.Tabs["Home"];
				e.Cancel = false;
				
				_UserSettings.Save();
			}
			else
				e.Cancel = true;
		}

		private void btnOptionsOK_Click(object sender, EventArgs e)
		{
			_UserSettings.MasterPipXPLocation = edtMasterPipXPLocation.Text;
			GenerateManholeCosts = !chkDoNotGenerateManholeCosts.Checked;
			_UserSettings.AllowENRCCIChange = chkAllowENRCCIChange.Checked;
			_UserSettings.Save();
			tabMain.SelectedTab = lastTab;
		}

		private void edtMasterPipXPLocation_EditorButtonClick(object sender, 
			Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
		{
			switch (e.Button.Key)
			{
				case "OpenFile":
					dlgOpen.DefaultExt = ACCESS_FILE_EXTENSION;
					dlgOpen.Title = "Open Access Database File Containing Master Pipe XP data";
					dlgOpen.Filter = "MS Access Database (*.mdb)|*.mdb|All files (*.*)|*.*";
					dlgOpen.FilterIndex = 0;
					dlgOpen.FileName = "";
					if (dlgOpen.ShowDialog() == DialogResult.OK)
						edtMasterPipXPLocation.Text = dlgOpen.FileName;
					break;
			} // switch
		}
	}
}