using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.Utils.SQLHelper;
using SystemsAnalysis.Utils.DataMobility;

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public partial class FrmGridAnalysis : Form
    {    
        GridModelEngine gridModelEngine;
        GridModelOutput gridModelOutput;

        //variables for the main server connection
        string userID = "";
        string password = "";
        string domain = "";
        string server = "";
        string database = "";
        bool trustedConnection = false;
        bool SQLisSource = true;

        //variables for the user selected server connections
        //these variables are used for multiple different connections
        //so they should not be expected to remain static
        string dynamicUserID = "";
        string dynamicPassword = "";
        string dynamicDomain = "";
        string dynamicServer = "";
        string dynamicDatabase = "";
        string dynamicTableName = "";
        string dynamicInputDatabase = "";
        bool dynamicTrustedConnection = false;

        //variables for the archive server connection
        string ArchiveUserID = "";
        string ArchivePassword = "";
        string ArchiveDomain = "";
        string ArchiveServer = "";
        string ArchiveDatabase = "";
        bool ArchiveTrustedConnection = false;


        public FrmGridAnalysis()
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(DoSplash));
            th.Start();
            InitializeComponent();
        }

        private void frmGridAnalysis_Load(object sender, EventArgs e)
        {
            //on loadup, before trying to LoadData, the user should specify what database the 
            //program should be accessing the data from.
            //The user should be told that the data source should have a Gridmodel compatible 
            //table set, and the user should be asked if the tables are listed under a specific
            //domain (in case the program needs to preface the tablenames with a "domainName."
            //qualifier before the actual table names.
            string serverIsUsable = "empty";
            string inputDatabase = "";

            while (serverIsUsable != "")
            {
                //get the connection information
                FormServerDatabaseUserIDPasswordDomainEntry child = new FormServerDatabaseUserIDPasswordDomainEntry();
                child.FormClosing += new FormClosingEventHandler(child_FormClosing2);
                this.Enabled = false;
                child.ShowDialog();
                this.Enabled = true;

                
                //test the connection information:
                inputDatabase = "Server=" + server + ";Database=" + database;
                if (trustedConnection == true)
                {
                    inputDatabase += ";Trusted_Connection=yes;";
                }
                else
                {
                    inputDatabase += ";Persist Security Info=True;User ID=" + userID + ";Password=" + password;
                }

                if (domain != "")
                {
                    domain = domain + ".";
                }

                serverIsUsable = SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_GridResults");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_ZONING_IMP");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_variables");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_SELECTION_SETS");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_SELECTION_SET_AREAS");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_SCENARIOS");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_SCENARIO_X_PROCESS");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_PROCESS_GROUP");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_PROCESS");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_MODEL_RUN");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_HYETOGRAPHS");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_HYETOGRAPH_DATA");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_GRID_PROJECTS");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_Contaminants");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_BMP_TYPE_TABLE_GENERAL");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_BMP_PERFORMANCE");
                serverIsUsable = serverIsUsable + SQLHelper.SQLTestDatabase(inputDatabase, domain + "GRID_pollutant_loadings");
                if (serverIsUsable != "")
                {
                    MessageBox.Show("The server you have selected is not acceptable to use for the GRIDMODEL");
                    MessageBox.Show(serverIsUsable);
                }
                //if the user has selected the access source radio button, then you also have to try to import the 
                //access database.  If the access database import fails, then you need to return to the
                //server selection form
                else if (SQLisSource == false)
                {

                    //user provided path of the source access database
                    System.Windows.Forms.FolderBrowserDialog theFolderBrowserDialog = new FolderBrowserDialog();

                    DialogResult result = theFolderBrowserDialog.ShowDialog();
                    if(result == DialogResult.OK)
                    {
                        string AccessString = theFolderBrowserDialog.SelectedPath;

                        string inputDatabaseStringForAccess = "ODBC;"/*DRIVER={sql server}*/+ ";DSN=" + server + ";DATABASE = " + database;
                        if (trustedConnection == true)
                        {
                            inputDatabaseStringForAccess += ";Trusted_Connection=yes;";
                        }
                        else
                        {
                            inputDatabaseStringForAccess += ";Persist Security Info=True;User ID=" + userID + ";Password=" + password;
                        }
                        string outputDatabaseStringForAccess = "Server=" + server + ";Database=" + database;
                        if (trustedConnection == true)
                        {
                            outputDatabaseStringForAccess += ";Trusted_Connection=yes;";
                        }
                        else
                        {
                            outputDatabaseStringForAccess += ";Persist Security Info=True;User ID=" + userID + ";Password=" + password;
                        }
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("ZONING_IMP", AccessString, "GRID_ZONING_IMP", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("variables", AccessString, "GRID_variables", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("FE_SELECTION_SETS", AccessString, "GRID_FE_SELECTION_SETS", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("FE_SELECTION_SET_AREAS", AccessString, "GRID_FE_SELECTION_SET_AREAS", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("FE_SCENARIOS", AccessString, "GRID_FE_SCENARIOS", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("FE_SCENARIO_X_PROCESS", AccessString, "GRID_FE_SCENARIO_X_PROCESS", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("FE_PROCESS_GROUP", AccessString, "GRID_FE_PROCESS_GROUP", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("FE_PROCESS", AccessString, "GRID_FE_PROCESS", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("FE_MODEL_RUN", AccessString, "GRID_FE_MODEL_RUN", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("FE_HYETOGRAPHS", AccessString, "GRID_FE_HYETOGRAPHS", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("FE_HYETOGRAPH_DATA", AccessString, "GRID_FE_HYETOGRAPH_DATA", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("FE_GRID_PROJECTS", AccessString, "GRID_FE_GRID_PROJECTS", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("Contaminants", AccessString, "GRID_Contaminants", outputDatabaseStringForAccess);
                        serverIsUsable = serverIsUsable + DataMobility.SQLCopyAccessTable("BMP_TYPE_TABLE_GENERAL", AccessString, "GRID_BMP_TYPE_TABLE_GENERAL", outputDatabaseStringForAccess);
                    }
                    else serverIsUsable = serverIsUsable + "Chosen access file is unacceptable";
                }
            }

            try
            {
                gridInterfaceDS.LoadData();
                cmbSelectedProject.SelectedIndex = 0;
                Object[] theObjects = (Object[])SQLHelper.SQLGetListOfServers();
                this.cboServers.Items.AddRange(theObjects);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open Grid database: " + ex.Message, "Error opening Grid database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
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

                FileInfo fi = new FileInfo("GridInterface.exe");
                dateText = fi.CreationTime.Date.ToString("MMMM dd yyyy");
            }

            SplashScreen sp = new SplashScreen(versionText, dateText);
            sp.ShowDialog(waitForClick);
        }

        private void btnDefineModelRun_Click(object sender, EventArgs e)
        {
            if (grdSelectScenario.Selected.Rows.Count != 1)
            {
                MessageBox.Show("You must select a Model Scenario", "No Model Scenario selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (grdSelectionSetAreas.Selected.Rows.Count != 1)
            {
                MessageBox.Show("You must select a Selection Set Area", "No Selection Set Area selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (grdHyetographs.Selected.Rows.Count != 1)
            {
                MessageBox.Show("You must select a Hyetograph", "No Hyetograph selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int scenarioID;
                scenarioID = (int)grdSelectScenario.Selected.Rows[0].Cells["scenario_id"].Value;

                int selectionSetAreaID;
                selectionSetAreaID = (int)grdSelectionSetAreas.Selected.Rows[0].Cells["selection_set_area_id"].Value;

                int hyetographID;
                hyetographID = (int)grdHyetographs.Selected.Rows[0].Cells["hyetograph_id"].Value;

                GridInterfaceDataSetTableAdapters.FEModelRunTableAdapter feModelRunTA;
                feModelRunTA = new GridInterfaceDataSetTableAdapters.FEModelRunTableAdapter();
                if (gridInterfaceDS.ModelRunExists(scenarioID, selectionSetAreaID, hyetographID))
                {
                    MessageBox.Show("This Model Run is already defined", "Model Run Already Defined", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int? NextModelRunID = feModelRunTA.GetNextModelRunID();
                    feModelRunTA.InsertQuery(NextModelRunID, scenarioID, selectionSetAreaID, hyetographID);
                    gridInterfaceDS.RefreshModelRunQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Define Models Runs: " + ex.Message, "Error Defining Model Runs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return;
        }

        private void btnCommitHyetographChanges_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Commit changes to Hyetographs?", "Commit Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }
            try
            {
                GridInterfaceDataSetTableAdapters.FEHyetographsTableAdapter feHyetographsTA;
                feHyetographsTA = new GridInterfaceDataSetTableAdapters.FEHyetographsTableAdapter();
                GridInterfaceDataSetTableAdapters.FEHyetographDataTableAdapter feHyetographDataTA;
                feHyetographDataTA = new GridInterfaceDataSetTableAdapters.FEHyetographDataTableAdapter();
                gridInterfaceDS.FEHyetographData.BeginLoadData();
                gridInterfaceDS.FEHyetographs.BeginLoadData();
                feHyetographsTA.Update(gridInterfaceDS.FEHyetographs);

                feHyetographsTA.Fill(gridInterfaceDS.FEHyetographs);

                feHyetographDataTA.Update(gridInterfaceDS.FEHyetographData);
                feHyetographDataTA.Fill(gridInterfaceDS.FEHyetographData);
                gridInterfaceDS.FEHyetographs.AcceptChanges();
                gridInterfaceDS.FEHyetographData.AcceptChanges();
                gridInterfaceDS.FEHyetographData.EndLoadData();
                gridInterfaceDS.FEHyetographs.EndLoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Commiting Hyetograph Changes: " + ex.Message, "Error Comitting Hyetograph Changes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddNewScenario_Click(object sender, EventArgs e)
        {
            string description;
            description = txtNewScenarioName.Text;
            if (description == "" || description == null)
            {
                return;
            }
            if (MessageBox.Show(
                "Add Scenario '" + description + "'?", "Add New Scenario?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                int projectID;
                projectID = (int)cmbSelectedProject.SelectedValue;
                gridInterfaceDS.FEScenarios.AddFEScenariosRow(
                    gridInterfaceDS.FEGridProjects.FindByproject_id(projectID),
                    "EX", description, true, "", "", "", "");
                txtNewScenarioName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Adding New Scenario: " + ex.Message, "Error Adding New Scenario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeleteScenario_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete Scenario?", "Delete Scenario?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int scenarioID;
                scenarioID = (int)lstDefineScenario.SelectedValue;
                gridInterfaceDS.FEScenarios.FindByscenario_id(scenarioID).Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Deleting Scenario: " + ex.Message, "Error Deleting Scenario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCommitScenarioChanges_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Commit changes to Scenarios?", "Commit Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }
            try
            {
                feScenariosBindingSource.EndEdit();
                GridInterfaceDataSetTableAdapters.FEScenariosTableAdapter feScenariosTA;
                feScenariosTA = new GridInterfaceDataSetTableAdapters.FEScenariosTableAdapter();
                feScenariosTA.Update(gridInterfaceDS.FEScenarios);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update Scenario: " + ex.Message, "Error Updating Scenario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gridInterfaceDS.FEScenarios.RejectChanges();
            }
        }

        private void btnUpdateSelectedProcesses_Click(object sender, EventArgs e)
        {
            if (lstDefineScenario.SelectedItems.Count != 1)
            {
                MessageBox.Show("You must select a Model Scenario", "No Model Scenario selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int scenarioID;
                scenarioID = (int)lstDefineScenario.SelectedValue;
                FrmChooseProcesses frmChooseProcesses = new FrmChooseProcesses(scenarioID);
                frmChooseProcesses.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Updating Processes: " + ex.Message, "Error Updating Processes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadModels_Click(object sender, EventArgs e)
        {
            if (grdModelRuns.Selected.Rows.Count == 0)
            {
                MessageBox.Show("You must select one or more Model Runs", "No Model Runs selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string outputDirectory = txtOutputDirectory.Text;
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            try
            {
                CreateGridModelEngine();
                LoadStoredModelRuns();
                gridModelEngine.populateGridData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Stored Models Runs: " + ex.Message, "Error Loading Stored Model Runs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }
        private void LoadStoredModelRuns()
        {
            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow row in grdModelRuns.Selected.Rows)
            {
                int selectionSetAreaID = (int)row.Cells["selection_set_area_id"].Value;
                int scenarioID = (int)row.Cells["scenario_id"].Value;
                int hyetographID = (int)row.Cells["hyetograph_id"].Value;

                int modelRunID;
                modelRunID = (int)row.Cells["model_run_id"].Value;
                GridInterfaceDataSet.QryModelRunRow modelRunRow;
                modelRunRow = gridInterfaceDS.QryModelRun.FindBymodel_run_id(modelRunID);
                GridModelRun modelRun;
                modelRun = new GridModelRun(modelRunRow);
                gridModelEngine.GridModelRuns.Add(modelRun);

                foreach (GridInterfaceDataSet.FEHyetographDataRow hyetographDataRow
                    in gridInterfaceDS.FEHyetographs.
                    FindByhyetograph_id(hyetographID).GetFE_HYETOGRAPH_DATARows())
                {
                    modelRun.GridModelTimeSteps.Add(
                        new GridModelTimeStep(hyetographDataRow.rainfall,
                            hyetographDataRow.run_order, hyetographDataRow.comment));
                }
                foreach (GridInterfaceDataSet.FEScenarioXProcessRow scenarioXProcessRow
                    in gridInterfaceDS.FEScenarios.
                    FindByscenario_id(scenarioID).GetFEScenarioXProcessRows())
                {
                    string processGroup;
                    processGroup = scenarioXProcessRow.process_group;

                    GridInterfaceDataSet.FEProcessGroupRow processGroupRow;
                    processGroupRow = gridInterfaceDS.FEProcessGroup.FindByprocess_group(processGroup);

                    string processDescription;
                    processDescription = processGroupRow.description;
                    double groupOrder;
                    groupOrder = processGroupRow.group_order;

                    GridProcessGroup gridProcessGroup;
                    gridProcessGroup = new GridProcessGroup(processGroup, groupOrder, processDescription);
                    modelRun.GridProcessGroups.Add(gridProcessGroup);

                    foreach (GridInterfaceDataSet.FEProcessRow processRow in processGroupRow.GetFEProcessRows())
                    {
                        gridProcessGroup.GridProcesses.Add(new GridProcess(processRow.process_name, processRow.critical, processRow.process_order));
                    }
                }
            }
        }

        private void btnClearModels_Click(object sender, EventArgs e)
        {
            CreateGridModelEngine();
        }

        private void btnImportModelMetadata_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "xml";
            ofd.AddExtension = true;
            ofd.ValidateNames = true;
            ofd.Title = "Select Grid Model Run Metadata file to import:";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GridModelEngine));
                StreamReader reader = new StreamReader(ofd.FileName);
                gridModelEngine = (GridModelEngine)serializer.Deserialize(reader);
                gridModelEngineBindingSource.DataSource = gridModelEngine.GridModelRuns;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Importing Model Metadata: " + ex.Message, "Error Importing Model Metadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExportModelMetadata_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "xml";
            sfd.AddExtension = true;
            sfd.ValidateNames = true;
            sfd.Title = "Select Grid Model Run Metadata file to export:";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = sfd.FileName;
            try
            {
                ExportModelMetadata(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Exporting Model Metadata: " + ex.Message, "Error Exporting Model Metadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportModelMetadata(string fileName)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName);
                XmlSerializer serializer = new XmlSerializer(typeof(GridModelEngine));
                serializer.Serialize(writer, gridModelEngine);
            }
            finally
            {
                writer.Close();
            }
        }
        private void btnImportModelResults_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "xml";
            ofd.AddExtension = true;
            ofd.ValidateNames = true;
            ofd.Title = "Select Grid Model Run Results Metadata file to import:";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GridModelOutput));
                StreamReader reader = new StreamReader(ofd.FileName);
                gridModelOutput = (GridModelOutput)serializer.Deserialize(reader);
                gridModelOutputBindingSource.DataSource = gridModelOutput;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Importing Model Results Metadata: " + ex.Message, "Error Importing Model Results Metadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExportModelResults_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "xml";
            sfd.AddExtension = true;
            sfd.ValidateNames = true;
            sfd.Title = "Select Grid Model Output Metadata file to export:";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = sfd.FileName;
            try
            {
                ExportModelOutputMetadata(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Exporting Model Output Metadata: " + ex.Message, "Error Exporting Model Output Metadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportModelOutputMetadata(string fileName)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName);
                XmlSerializer serializer = new XmlSerializer(typeof(GridModelOutput));
                serializer.Serialize(writer, gridModelOutput);
            }
            finally
            {
                writer.Close();
            }
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

            // Programatically add XSL processing instruction to an XML-document:                
            doc.Load(fileName);
            string gridOutputStyleSheet;
            gridOutputStyleSheet = System.Configuration.ConfigurationManager.AppSettings.Get("gridOutputStyleSheet");
            System.Xml.XmlProcessingInstruction pi = doc.CreateProcessingInstruction("xml-stylesheet", "type='text/xsl' href='" + gridOutputStyleSheet + "'");
            doc.InsertAfter(pi, doc.ChildNodes[0]);
            doc.Save(fileName);
        }
        private void btnExportLoadingMetadata_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "xml";
            sfd.AddExtension = true;
            sfd.ValidateNames = true;
            sfd.Title = "Select Grid Model Pollutant Loading Metadata file to export:";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = sfd.FileName;
            try
            {
                ExportPollutantLoadingMetadata(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Exporting Pollutant Loading Metadata: " + ex.Message, "Error Exporting Pollutant Loading Metadata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportPollutantLoadingMetadata(string fileName)
        {
            StreamWriter writer = null;
            List<PollutantLoadingMetadataDataSet> plmdsList;
            plmdsList = gridModelOutput.GetPollutantLoadingMetadataDSList();
            try
            {
                writer = new StreamWriter(fileName);
                writer.WriteLine("<PollutantLoadingMetadataList>");
                foreach (PollutantLoadingMetadataDataSet plmdds in plmdsList)
                {
                    writer.WriteLine("<Scenario>");
                    writer.WriteLine("<ScenarioDescription>" + plmdds.ScenarioDescription + "</ScenarioDescription>");
                    writer.WriteLine(plmdds.GetXml());
                    writer.WriteLine("</Scenario>");
                }
                writer.WriteLine("</PollutantLoadingMetadataList>");
            }
            finally
            {
                writer.Close();
            }
        }

        private void btnExecuteModels_Click(object sender, EventArgs e)
        {
            int projectID;
            if (gridModelEngine == null || gridModelEngine.GridModelRuns.Count == 0)
            {
                MessageBox.Show("No Model Runs Loaded", "Could Not Execute Grid Model", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int modelRunCount;
                modelRunCount = gridModelEngine.GridModelRuns.Count;

                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Choose Output Directory";
                fbd.SelectedPath = gridModelEngine.OutputDirectory;
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                if (MessageBox.Show("Execute " + modelRunCount + " Model Runs (Existing Model Results Will Be Overwritten)?", "Begin Execution?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                gridModelEngine.OutputDirectory = fbd.SelectedPath;

                projectID = (int)cmbSelectedProject.SelectedValue;

                //Execute grid model in background process
                pnlCancelBackgroundWorker.Visible = true;
                this.Refresh();
                backgroundWorker.RunWorkerAsync(gridModelEngine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Creating Grid Engine: " + ex.Message, "Error Creating Grid Engine", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return;
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GridModelEngine gridModelEngine = (GridModelEngine)e.Argument;
            gridModelEngine.StatusChanged += new GridModelEngine.OnStatusChangedEventHandler(OnGridEngineStatusChanged);

            gridModelOutput = gridModelEngine.ExecuteModels();
            gridModelOutputBindingSource.DataSource = gridModelOutput;

            string runDate = System.DateTime.Now.ToString("yyyyMMdd_hhmm");
            ExportModelMetadata(gridModelEngine.OutputDirectory + "\\GridModelMetadata_" + runDate + ".xml");
            ExportModelOutputMetadata(gridModelEngine.OutputDirectory + "\\GridModelOutputMetadata_" + runDate + ".xml");
            ExportPollutantLoadingMetadata(gridModelEngine.OutputDirectory + "\\GridPollutantLoadingMetadata_" + runDate + ".xml");
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bgw = (BackgroundWorker)sender;
            if (bgw.CancellationPending)
            {
                bgw.CancelAsync();
            }

            string status = (string)e.UserState;

            //statusChangedArgs = (GridModelEngine.StatusChangedArgs)e.UserState;
            lblModelRunStatus.Text = status; // statusChangedArgs.NewStatus;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlCancelBackgroundWorker.Visible = false;
            if (e.Cancelled)
            {
                MessageBox.Show("Model Run Canceled - Model Results May Be Invalid",
                            "Grid Model Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error executing Grid Model: " + e.Error.Message,
                            "Error Executing Grid Model", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Successfully executed models!",
                            "Grid Model Executed Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.WorkerSupportsCancellation)
            {
                backgroundWorker.CancelAsync();
            }
        }

        public virtual void OnGridEngineStatusChanged(string status)
        {
            //if (GridEngineStatusChanged != null)
            backgroundWorker.ReportProgress(0, status);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reloadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridInterfaceDS.LoadData();
        }

        private void CreateGridModelEngine()
        {
            DataRowView drv;
            drv = (DataRowView)cmbSelectedProject.SelectedItem;
            int projectID = (int)drv["project_id"];
            string projectDescription = (string)drv["project_description"];

            string inputDatabase = "Server=" + server + ";Database=" + database;
            if (trustedConnection == true)
            {
                inputDatabase += ";Trusted_Connection=yes;";
            }
            else
            {
                inputDatabase += ";Persist Security Info=True;User ID=" + userID + ";Password=" + password;
            }
            gridModelEngine = new GridModelEngine(projectID, projectDescription,
                txtGridPath.Text, txtPRFPath.Text, txtMIPPath.Text, txtOSFPath.Text, txtOutputDirectory.Text, inputDatabase);
            gridModelEngineBindingSource.DataSource = gridModelEngine.GridModelRuns;
        }

        private void btnDeleteStoredModelRuns_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete Stored Model Run(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow row in grdModelRuns.Selected.Rows)
                {
                    int modelRunID = (int)row.Cells["model_run_id"].Value;
                    gridInterfaceDS.FEModelRun.FindBymodel_run_id(modelRunID).Delete();
                }
                GridInterfaceDataSetTableAdapters.FEModelRunTableAdapter modelRunTA;
                modelRunTA = new GridInterfaceDataSetTableAdapters.FEModelRunTableAdapter();
                modelRunTA.Update(gridInterfaceDS.FEModelRun);
                gridInterfaceDS.FEModelRun.AcceptChanges();
                gridInterfaceDS.RefreshModelRunQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Delete Stored Model Run(s):" + ex.Message, "Error Deleting Model Runs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddNewProject_Click(object sender, EventArgs e)
        {
            string description;
            description = txtNewProjectName.Text;
            if (description == "" || description == null)
            {
                return;
            }
            if (MessageBox.Show(
                "Add Project '" + description + "'?", "Add New Project?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                gridInterfaceDS.FEGridProjects.AddFEGridProjectsRow(
                    description, "", "", "", "", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Creating New Project: " + ex.Message, "Error Creating New Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteProject_Click(object sender, EventArgs e)
        {
            string description;
            description = gridInterfaceDS.FEGridProjects.FindByproject_id((int)lstDefineProject.SelectedValue).project_description;
            if (MessageBox.Show(
                "Delete Project '" + description + "'? Note: " +
                "All Selection Sets, Scenarios and Model Runs associated with "
                + "this project will also be deleted.", "Delete Project?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                int projectID;
                projectID = (int)cmbSelectedProject.SelectedValue;
                gridInterfaceDS.FEGridProjects.FindByproject_id(projectID).Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Deleting Project: " + ex.Message, "Error Deleting Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCommitProjectChanges_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Commit Changes to Projects?", "Commit Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }
            try
            {
                feGridProjectsBindingSource.EndEdit();
                GridInterfaceDataSetTableAdapters.FEGridProjectsTableAdapter gridProjectsTA;
                gridProjectsTA = new GridInterfaceDataSetTableAdapters.FEGridProjectsTableAdapter();
                gridProjectsTA.Update(gridInterfaceDS.FEGridProjects);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Update Projects: " + ex.Message, "Error Updating Projects", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gridInterfaceDS.FEScenarios.RejectChanges();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSplash(true);
        }

        private void onlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://twiki.bes-sa.org/bin/view/Development/GRIDModel");
        }

        private void browseToApplicationFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory);
        }

        private void tabPageDefineProjects_Click(object sender, EventArgs e)
        {

        }

        private void cboServers_SelectedValueChanged(object sender, EventArgs e)
        {
            Object[] theObjects = (Object[])SQLHelper.SQLGetListOfDatabasesInServer(cboServers.SelectedItem.ToString());
            this.cboDatabases.Items.Clear();
            this.cboDatabases.Items.AddRange(theObjects);
        }

        private void ultraButtonArchiveInputOutput_Click(object sender, EventArgs e)
        {
            //this should not be in this part of the program.
            //but then again, I haven't been able to get the guys in charge of creating a dedicated
            //database for the gridmodel to actually do that so this is really just a temporary workaround.

            //ask the user for user ID/password for the server/database
            //if they supply no userid/password, assume trusted connection
            
            FormUserIDAndPasswordEntry child = new FormUserIDAndPasswordEntry();
            child.FormClosing += new FormClosingEventHandler(child_FormClosing);
            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;

            //check to see if a connection can be made
            string inputDatabase = "Server=" + server + ";Database="+database;
            if (trustedConnection == true)
            {
                inputDatabase += ";Trusted_Connection=yes;";
            }
            else
            {
                inputDatabase += ";Persist Security Info=True;User ID="+userID+";Password="+password;
            }
            string outputDatabase = "Server=" + this.cboServers.Text + ";Database=" + this.cboDatabases.Text;
            if (ArchiveTrustedConnection == true)
            {
                outputDatabase += ";Trusted_Connection=yes;";
            }
            else
            {
                outputDatabase += ";Persist Security Info=True;User ID="+ArchiveUserID+";Password="+ArchivePassword;
            }
            //check to see if the user has add/delete/update rights to the database
            

            //check to see if there is already a gridModelEngine
            if (gridModelEngine == null)
            {
                //if there is no gridmodel engine, then no model has been run yet,
                //so this means we are only archiving input files
                //create a gridModelEngine and call the gridModelEngine archiveInputTables() method.
                try
                {
                    DataRowView drv = (DataRowView)cmbSelectedProject.SelectedItem;
                    int projectID = (int)drv["project_id"];
                    string projectDescription = (string)drv["project_description"];
                    inputDatabase = "Server=" + server + ";Database=" + database;
                    if (trustedConnection == true)
                    {
                        inputDatabase += ";Trusted_Connection=yes;";
                    }
                    else
                    {
                        inputDatabase += ";Persist Security Info=True;User ID=" + userID + ";Password=" + password;
                    }
                    gridModelEngine = new GridModelEngine(projectID, projectDescription,
                txtGridPath.Text, txtPRFPath.Text, txtMIPPath.Text, txtOSFPath.Text, txtOutputDirectory.Text, inputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_GridResults", inputDatabase, "GRID_GridResults", outputDatabase);
                //"Data Source=SIRTOBY;Initial Catalog=SANDBOX;Integrated Security=SSPI;",//;User ID=GIS;Password=Extra$hade",
                //"GRID_GridResults",
                //"Data Source=" + this.cboServers.Text + ";Initial Catalog=" + this.cboDatabases.Text + ";Trusted_Connection=yes");

                    DataMobility.SQLCopySQLTable("GRID_ZONING_IMP", inputDatabase, "GRID_ZONING_IMP", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_variables", inputDatabase, "GRID_variables", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_FE_SELECTION_SETS", inputDatabase, "GRID_FE_SELECTION_SETS", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_FE_SELECTION_SET_AREAS", inputDatabase, "GRID_FE_SELECTION_SET_AREAS", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_FE_SCENARIOS", inputDatabase, "GRID_FE_SCENARIOS", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_FE_SCENARIO_X_PROCESS", inputDatabase, "GRID_FE_SCENARIO_X_PROCESS", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_FE_PROCESS_GROUP", inputDatabase, "GRID_FE_PROCESS_GROUP", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_FE_PROCESS", inputDatabase, "GRID_FE_PROCESS", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_FE_MODEL_RUN", inputDatabase, "GRID_FE_MODEL_RUN", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_FE_HYETOGRAPHS", inputDatabase, "GRID_FE_HYETOGRAPHS", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_FE_HYETOGRAPH_DATA", inputDatabase, "GRID_FE_HYETOGRAPH_DATA", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_FE_GRID_PROJECTS", inputDatabase, "GRID_FE_GRID_PROJECTS", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_Contaminants", inputDatabase, "GRID_Contaminants", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_BMP_TYPE_TABLE_GENERAL", inputDatabase, "GRID_BMP_TYPE_TABLE_GENERAL", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_BMP_PERFORMANCE", inputDatabase, "GRID_BMP_PERFORMANCE", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_pollutant_loadings", inputDatabase, "GRID_pollutant_loadings", outputDatabase);
                    DataMobility.SQLCopySQLTable("GRID_WshdGrd100FtOpt", inputDatabase, "GRID_WshdGrd100FtOpt", outputDatabase);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not connect to database!");
                }
            }
            else
            {
                //if there is already a gridmodelEngine, check to see if the projectID of the 
                //gridmodelEngine is the same as the projectID of the currently viewed project
                if (gridModelEngine.ProjectID != Int32.Parse(txtProjectID.Text))
                {
                    //we can only archive the input tables, and we must ask the user if that is
                    //what they actually intended to do
                    DialogResult theResponse = MessageBox.Show("Selected project ID does not match the project ID of the gridmodel most recently run.  You will only be able to archive the input tables for this project (ID #" + txtProjectID.Text + ")", "No results available yet for this project ID", MessageBoxButtons.OKCancel);
                    if (theResponse == DialogResult.OK)
                    {
                        //create a new gridmodelEngine and archiveInputTables()
                        try
                        {
                            DataRowView drv = (DataRowView)cmbSelectedProject.SelectedItem;
                            int projectID = (int)drv["project_id"];
                            string projectDescription = (string)drv["project_description"];
                            inputDatabase = "Server=" + server + ";Database=" + database;
                            if (trustedConnection == true)
                            {
                                inputDatabase += ";Trusted_Connection=yes;";
                            }
                            else
                            {
                                inputDatabase += ";Persist Security Info=True;User ID=" + userID + ";Password=" + password;
                            }
                            gridModelEngine = new GridModelEngine(projectID, projectDescription,
                txtGridPath.Text, txtPRFPath.Text, txtMIPPath.Text, txtOSFPath.Text, txtOutputDirectory.Text, inputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_GridResults", inputDatabase, "GRID_GridResults", outputDatabase);
                //"Data Source=SIRTOBY;Initial Catalog=SANDBOX;Persist Security Info=True;User ID=GIS;Password=Extra$hade",
                //"GRID_GridResults",
                //"Data Source=" + this.cboServers.Text + ";Initial Catalog=" + this.cboDatabases.Text + ";Trusted_Connection=yes");

                            DataMobility.SQLCopySQLTable("GRID_ZONING_IMP", inputDatabase, "GRID_ZONING_IMP", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_variables", inputDatabase, "GRID_variables", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_FE_SELECTION_SETS", inputDatabase, "GRID_FE_SELECTION_SETS", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_FE_SELECTION_SET_AREAS", inputDatabase, "GRID_FE_SELECTION_SET_AREAS", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_FE_SCENARIOS", inputDatabase, "GRID_FE_SCENARIOS", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_FE_SCENARIO_X_PROCESS", inputDatabase, "GRID_FE_SCENARIO_X_PROCESS", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_FE_PROCESS_GROUP", inputDatabase, "GRID_FE_PROCESS_GROUP", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_FE_PROCESS", inputDatabase, "GRID_FE_PROCESS", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_FE_MODEL_RUN", inputDatabase, "GRID_FE_MODEL_RUN", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_FE_HYETOGRAPHS", inputDatabase, "GRID_FE_HYETOGRAPHS", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_FE_HYETOGRAPH_DATA", inputDatabase, "GRID_FE_HYETOGRAPH_DATA", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_FE_GRID_PROJECTS", inputDatabase, "GRID_FE_GRID_PROJECTS", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_Contaminants", inputDatabase, "GRID_Contaminants", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_BMP_TYPE_TABLE_GENERAL", inputDatabase, "GRID_BMP_TYPE_TABLE_GENERAL", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_BMP_PERFORMANCE", inputDatabase, "GRID_BMP_PERFORMANCE", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_pollutant_loadings", inputDatabase, "GRID_pollutant_loadings", outputDatabase);
                            DataMobility.SQLCopySQLTable("GRID_WshdGrd100FtOpt", inputDatabase, "GRID_WshdGrd100FtOpt", outputDatabase);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not connect to database!");
                        }
                    }
                }
                //we have trapped for all instances that would require only archiving the input tables,
                //so now we can just archive all of the tables in the current gridmodelEngine
                else
                {
                    //archiveEverything()
                    try
                    {
                        DataMobility.SQLCopySQLTable("GRID_GridResults", inputDatabase, "GRID_GridResults", outputDatabase);
                //"Data Source=SIRTOBY;Initial Catalog=SANDBOX;Persist Security Info=True;User ID=GIS;Password=Extra$hade",
                //"GRID_GridResults",
                //"Data Source=" + this.cboServers.Text + ";Initial Catalog=" + this.cboDatabases.Text + ";Trusted_Connection=yes");

                        DataMobility.SQLCopySQLTable("GRID_ZONING_IMP", inputDatabase, "GRID_ZONING_IMP", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_variables", inputDatabase, "GRID_variables", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_FE_SELECTION_SETS", inputDatabase, "GRID_FE_SELECTION_SETS", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_FE_SELECTION_SET_AREAS", inputDatabase, "GRID_FE_SELECTION_SET_AREAS", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_FE_SCENARIOS", inputDatabase, "GRID_FE_SCENARIOS", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_FE_SCENARIO_X_PROCESS", inputDatabase, "GRID_FE_SCENARIO_X_PROCESS", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_FE_PROCESS_GROUP", inputDatabase, "GRID_FE_PROCESS_GROUP", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_FE_PROCESS", inputDatabase, "GRID_FE_PROCESS", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_FE_MODEL_RUN", inputDatabase, "GRID_FE_MODEL_RUN", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_FE_HYETOGRAPHS", inputDatabase, "GRID_FE_HYETOGRAPHS", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_FE_HYETOGRAPH_DATA", inputDatabase, "GRID_FE_HYETOGRAPH_DATA", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_FE_GRID_PROJECTS", inputDatabase, "GRID_FE_GRID_PROJECTS", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_Contaminants", inputDatabase, "GRID_Contaminants", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_BMP_TYPE_TABLE_GENERAL", inputDatabase, "GRID_BMP_TYPE_TABLE_GENERAL", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_BMP_PERFORMANCE", inputDatabase, "GRID_BMP_PERFORMANCE", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_pollutant_loadings", inputDatabase, "GRID_pollutant_loadings", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_WshdGrd100FtOpt", inputDatabase, "GRID_WshdGrd100FtOpt", outputDatabase);
                        DataMobility.SQLCopySQLTable("GRID_GridCompiledResults", inputDatabase, "GRID_GridCompiledResults", outputDatabase);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not connect to database!");
                    }
                }
            }
        }

        void child_FormClosing(object sender, FormClosingEventArgs e)
        {
            ArchiveUserID = ((FormUserIDAndPasswordEntry)sender).UserID;
            ArchivePassword = ((FormUserIDAndPasswordEntry)sender).Password;
            ArchiveTrustedConnection = ((FormUserIDAndPasswordEntry)sender).useTrustedConnection;
        }

        void child_FormClosing2(object sender, FormClosingEventArgs e)
        {
            userID = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).UserID;
            password = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).Password;
            database = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).Database;
            server = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).Server;
            domain = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).Domain;
            trustedConnection = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).useTrustedConnection;
            SQLisSource = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).SQLisSource;
        }

        void child_FormClosing3(object sender, FormClosingEventArgs e)
        {
            dynamicUserID = ((FormConnectionStringInterface)sender).UserID;
            dynamicPassword = ((FormConnectionStringInterface)sender).Password;
            dynamicDatabase = ((FormConnectionStringInterface)sender).Database;
            dynamicServer = ((FormConnectionStringInterface)sender).Server;
            dynamicDomain = ((FormConnectionStringInterface)sender).Domain;
            dynamicTrustedConnection = ((FormConnectionStringInterface)sender).useTrustedConnection;
        }

        private void buttonPollutantLoadingConnectionStringEdit_Click(object sender, EventArgs e)
        {
            int numberOfMatchingColumns = 0;
            string serverIsUsable = "";
            dynamicInputDatabase = "";

            getConnectionStringFromUser((string)(((DataRowView)feScenariosBindingSource.Current)["pollutant_loading_db"]));

            ((DataRowView)feScenariosBindingSource.Current)["pollutant_loading_db"] = dynamicInputDatabase;
            feScenariosBindingSource.EndEdit();

            //The grid path should connect to a table named GRID_pollutant_loadings
            //later on the table name should be allowed to be user defined
            serverIsUsable = SQLHelper.SQLTestDatabase(dynamicInputDatabase, dynamicDomain + "GRID_pollutant_loadings");

            if (serverIsUsable != "")
            {
                MessageBox.Show("The connections you have selected are not acceptable to use for the GRIDMODEL");
                MessageBox.Show(serverIsUsable);
            }

            //make sure the table also has all of the appropriate columns
            //with all of the appropriate data types.
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_pollutant_loadings", dynamicInputDatabase, "LU_CODE", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_pollutant_loadings", dynamicInputDatabase, "Landuse", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_pollutant_loadings", dynamicInputDatabase, "Constituent", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_pollutant_loadings", dynamicInputDatabase, "VALUE_LOW", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_pollutant_loadings", dynamicInputDatabase, "UNITS", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_pollutant_loadings", dynamicInputDatabase, "Source", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_pollutant_loadings", dynamicInputDatabase, "VALUE_HIGH", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_pollutant_loadings", dynamicInputDatabase, "VALUE", "number");

            //if ScalarQueryResults is not null, then we have a good answer
            if (numberOfMatchingColumns == null)
            {
                MessageBox.Show("Could not test provided table for validity, please verify the source before trying to run the Grid Model");
            }
            else if ((int)numberOfMatchingColumns != 8)
            {
                MessageBox.Show("The table you indicated does not contain the necessary columns, please check the documentation for guidelines on creating a pollutant loading Table");
            }
        }

        private void buttonBMPEffectivenessConnectionStringEdit_Click(object sender, EventArgs e)
        {
            int numberOfMatchingColumns = 0;
            string serverIsUsable = "";
            dynamicInputDatabase = "";

            getConnectionStringFromUser((string)(((DataRowView)feScenariosBindingSource.Current)["bmp_effectiveness_db"]));

            ((DataRowView)feScenariosBindingSource.Current)["bmp_effectiveness_db"] = dynamicInputDatabase;
            feScenariosBindingSource.EndEdit();

            //The grid path should connect to a table named GRID_pollutant_loadings
            //later on the table name should be allowed to be user defined
            serverIsUsable = SQLHelper.SQLTestDatabase(dynamicInputDatabase, dynamicDomain + "GRID_BMP_PERFORMANCE");

            if (serverIsUsable != "")
            {
                MessageBox.Show("The connections you have selected are not acceptable to use for the GRIDMODEL");
                MessageBox.Show(serverIsUsable);
            }

            //make sure the table also has all of the appropriate columns
            //with all of the appropriate data types.
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_BMP_PERFORMANCE", dynamicInputDatabase, "BMP_TYPE_GEN_ID", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_BMP_PERFORMANCE", dynamicInputDatabase, "Constituent", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_BMP_PERFORMANCE", dynamicInputDatabase, "VALUE_LOW", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_BMP_PERFORMANCE", dynamicInputDatabase, "UNITS", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_BMP_PERFORMANCE", dynamicInputDatabase, "Source", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_BMP_PERFORMANCE", dynamicInputDatabase, "VALUE_HIGH", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_BMP_PERFORMANCE", dynamicInputDatabase, "VALUE", "number");

            if (numberOfMatchingColumns == null)
            {
                MessageBox.Show("Could not test provided table for validity, please verify the source before trying to run the Grid Model");
            }
            else if ((int)numberOfMatchingColumns != 7)
            {
                MessageBox.Show("The table you indicated does not contain the necessary columns, please check the documentation for guidelines on creating a pollutant loading Table");
            }
        }

        private void getConnectionStringFromUser(string currentConnectionString)
        {
            FormConnectionStringInterface child = new FormConnectionStringInterface(currentConnectionString);
            child.FormClosing += new FormClosingEventHandler(child_FormClosing3);

            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;

            dynamicInputDatabase = "Server=" + dynamicServer + ";Database=" + dynamicDatabase;
            if (dynamicTrustedConnection == true)
            {
                dynamicInputDatabase += ";Trusted_Connection=yes;";
            }
            else
            {
                dynamicInputDatabase += ";Persist Security Info=True;User ID=" + dynamicUserID + ";Password=" + dynamicPassword;
            }

            if (dynamicDomain != "")
            {
                dynamicDomain = dynamicDomain + ".";
            }
        }

        private void buttonGridPathEdit_Click(object sender, EventArgs e)
        {
            int numberOfMatchingColumns = 0;
            string serverIsUsable = "";
            dynamicInputDatabase = "";

            getConnectionStringFromUser((string)(((DataRowView)feGridProjectsBindingSource.Current)["grid_path"]));

            ((DataRowView)feGridProjectsBindingSource.Current)["grid_path"] = dynamicInputDatabase;
            feGridProjectsBindingSource.EndEdit();
                
            //verify the connection works.  IF the connection does not work,
            //then notify the user that the connection does not work, but the
            //user should still be in charge of deciding whether or not to keep
            //a connection that does not work.  The gridmodel should not be allowed
            //to run if a connection does not work, though.

            //The grid path should connect to a table named GRID_WshdGrd100FtOpt
            //later on the table name should be allowed to be user defined
            serverIsUsable = SQLHelper.SQLTestDatabase(dynamicInputDatabase, dynamicDomain + "GRID_WshdGrd100FtOpt");

            if (serverIsUsable != "")
            {
                MessageBox.Show("The connections you have selected are not acceptable to use for the GRIDMODEL");
                MessageBox.Show(serverIsUsable);
            }

            //make sure the table also has all of the appropriate columns
            //with all of the appropriate data types.
            //there must be 81 matches (there is no chance there could be more than 81
            //matches even if there are more than 81 columns in the input table,
            //and less than 81 matches means the input table isn't ready.)
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "Description", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "Col_Name", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "Row_Name", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "Area_ft2", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "Avg_Slope", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "Public", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "Private", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "VEG_LONcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "VEG_LOFFcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "CPY_LONcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "ROW_CellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "RF_CellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "PKG_CellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "IMP_CellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "WAT_CellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "TRA_LUcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COM_LUcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "IND_LUcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "SFR_LUcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "MFR_LUcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "FOR_LUcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "RUR_LUcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "VAC_LUcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "POS_LUcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "Blnk_LUcellCount", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "VEG_LON_pct", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "VEG_LOFF_pct", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "CPY_LON_pct", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "CPY_LOFF_pct", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "VEG_pct", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "ROW_pct", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "RF_pct", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "PKG_pct", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "IMP_pct", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "TSS", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "WAT_pct", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "CAL_point", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "WS_Code", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "BASIN_Code", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "BRANCH_ID", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "IN_PDX", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "RAINGAGE", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_B", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_C", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_D", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_E", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_F", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_G", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_H", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_I", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_J", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_K", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_L", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_M", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_N", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_O", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_P", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_Q", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_1", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_2", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "COL_3", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "TP", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "TSS", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "ECOLI", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "BOD", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "TP_WET", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "TSS_WET", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "ECOLI_WET", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "BOD_WET", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "TP_DRY", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "TSS_DRY", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "ECOLI_DRY", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "BOD_DRY", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "PbD", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "PbD_DRY", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "PbD_WET", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "ECOLI_REMOVAL", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "Xc", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_WshdGrd100FtOpt", dynamicInputDatabase, "Yc", "number");

            if (numberOfMatchingColumns == null)
            {
                MessageBox.Show("Could not test provided table for validity, please verify the source before trying to run the Grid Model");
            }
            else if ((int)numberOfMatchingColumns != 79)
            {
                MessageBox.Show("The table you indicated does not contain the necessary columns, please check the documentation for guidelines on creating a Grid Table");
            }
        }

        private void buttonPRFPathEdit_Click(object sender, EventArgs e)
        {
            int numberOfMatchingColumns = 0;
            string serverIsUsable = "";
            dynamicInputDatabase = "";

            getConnectionStringFromUser((string)(((DataRowView)feGridProjectsBindingSource.Current)["bmp_path"]));

            ((DataRowView)feGridProjectsBindingSource.Current)["bmp_path"] = dynamicInputDatabase;
            feGridProjectsBindingSource.EndEdit();

            serverIsUsable = SQLHelper.SQLTestDatabase(dynamicInputDatabase, dynamicDomain + "GRID_PDX_BMP_GRID");

            if (serverIsUsable != "")
            {
                MessageBox.Show("The connections you have selected are not acceptable to use for the GRIDMODEL");
                MessageBox.Show(serverIsUsable);
            }

            //make sure the table also has all of the appropriate columns
            //with all of the appropriate data types.
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_BMP_GRID", dynamicInputDatabase, "Description", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_BMP_GRID", dynamicInputDatabase, "Percent_Overlap", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_BMP_GRID", dynamicInputDatabase, "INSTREAM", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_BMP_GRID", dynamicInputDatabase, "BMP_ID", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_BMP_GRID", dynamicInputDatabase, "BMP_Type1", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_BMP_GRID", dynamicInputDatabase, "TimeFrame", "string");

            //if ScalarQueryResults is not null, then we have a good answer
            if (numberOfMatchingColumns == null)
            {
                MessageBox.Show("Could not test provided table for validity, please verify the source before trying to run the Grid Model");
            }
            else if ((int)numberOfMatchingColumns != 6)
            {
                MessageBox.Show("The table you indicated does not contain the necessary columns, please check the documentation for guidelines on creating a BMP Table");
            }
        }

        private void buttonMIPPathEdit_Click(object sender, EventArgs e)
        {
            int numberOfMatchingColumns = 0;
            string serverIsUsable = "";
            dynamicInputDatabase = "";

            getConnectionStringFromUser((string)(((DataRowView)feGridProjectsBindingSource.Current)["mip_path"]));

            ((DataRowView)feGridProjectsBindingSource.Current)["mip_path"] = dynamicInputDatabase;
            feGridProjectsBindingSource.EndEdit();

            serverIsUsable = SQLHelper.SQLTestDatabase(dynamicInputDatabase, dynamicDomain + "GRID_PDX_MIP_GRID");

            if (serverIsUsable != "")
            {
                MessageBox.Show("The connections you have selected are not acceptable to use for the GRIDMODEL");
                MessageBox.Show(serverIsUsable);
            }

            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_MIP_GRID", dynamicInputDatabase, "Description", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_MIP_GRID", dynamicInputDatabase, "Percent_Overlap", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_MIP_GRID", dynamicInputDatabase, "BMP_Type", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_MIP_GRID", dynamicInputDatabase, "Description_2", "string");

            //if ScalarQueryResults is not null, then we have a good answer
            if (numberOfMatchingColumns == null)
            {
                MessageBox.Show("Could not test provided table for validity, please verify the source before trying to run the Grid Model");
            }
            else if ((int)numberOfMatchingColumns != 4)
            {
                MessageBox.Show("The table you indicated does not contain the necessary columns, please check the documentation for guidelines on creating a MIP Table");
            }
        }

        private void buttonOSFPathEdit_Click(object sender, EventArgs e)
        {
            int numberOfMatchingColumns = 0;
            string serverIsUsable = "";
            dynamicInputDatabase = "";

            getConnectionStringFromUser((string)(((DataRowView)feGridProjectsBindingSource.Current)["osf_path"]));

            ((DataRowView)feGridProjectsBindingSource.Current)["osf_path"] = dynamicInputDatabase;
            feGridProjectsBindingSource.EndEdit();

            serverIsUsable = SQLHelper.SQLTestDatabase(dynamicInputDatabase, dynamicDomain + "GRID_PDX_OSF_GRID");

            if (serverIsUsable != "")
            {
                MessageBox.Show("The connections you have selected are not acceptable to use for the GRIDMODEL");
                MessageBox.Show(serverIsUsable);
            }
            //make sure the table also has all of the appropriate columns
            //with all of the appropriate data types.
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_OSF_GRID", dynamicInputDatabase, "Description", "string");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_OSF_GRID", dynamicInputDatabase, "Percent_Overlap", "number");
            numberOfMatchingColumns = numberOfMatchingColumns +
                SQLHelper.SQLTestTableForFuzzyColumn
                ("GRID_PDX_OSF_GRID", dynamicInputDatabase, "FAC_GRP", "string");

            //if ScalarQueryResults is not null, then we have a good answer
            if (numberOfMatchingColumns == null)
            {
                MessageBox.Show("Could not test provided table for validity, please verify the source before trying to run the Grid Model");
            }
            else if ((int)numberOfMatchingColumns != 3)
            {
                MessageBox.Show("The table you indicated does not contain the necessary columns, please check the documentation for guidelines on creating a OSF Table");
            }
        }
    }
}
