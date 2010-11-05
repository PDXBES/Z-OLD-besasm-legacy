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

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public partial class FrmGridAnalysis : Form
    {
        GridModelEngine gridModelEngine;
        GridModelOutput gridModelOutput;
        string userID = "";
        string password = "";
        string domain = "";
        string server = "";
        string database = "";
        bool trustedConnection = false;

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
            bool serverIsUsable = false;
            string inputDatabase = "";

            while (!serverIsUsable)
            {
                //get the connection information
                FormServerDatabaseUserIDPasswordDomainEntry child = new FormServerDatabaseUserIDPasswordDomainEntry();
                child.FormClosing += new FormClosingEventHandler(child_FormClosing2);
                this.Enabled = false;
                child.ShowDialog();
                this.Enabled = true;
                
                //test the connection information:
                inputDatabase = "Data Source=" + server + ";Initial Catalog=" + database;
                if (trustedConnection == true)
                {
                    inputDatabase += ";Trusted_Connection=yes";
                }
                else
                {
                    inputDatabase += ";Persist Security Info=True;User ID=" + userID + ";Password=" + password;
                }

                if (domain != "")
                {
                    domain = domain + ".";
                }

                serverIsUsable = AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_GridResults");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_ZONING_IMP");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_variables");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_SELECTION_SETS");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_SELECTION_SET_AREAS");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_SCENARIOS");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_SCENARIO_X_PROCESS");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_PROCESS_GROUP");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_PROCESS");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_MODEL_RUN");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_HYETOGRAPHS");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_HYETOGRAPH_DATA");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_FE_GRID_PROJECTS");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_Contaminants");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_BMP_TYPE_TABLE_GENERAL");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_BMP_PERFORMANCE");
                serverIsUsable = serverIsUsable && AccessHelper.SQLTestDatabase(inputDatabase, domain + "GRID_pollutant_loadings");
            }

            //now that we know the server is usable, copy the tables from the server to the access file:
            //string dataDirectory = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.DataDirectory;
            //string AccessString = dataDirectory + "\\Waterqual_GIS_v5_0.mdb";
            string AccessString = /*"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=*/"C:\\Development\\DotNet\\GridModel\\GridInterface\\Waterqual_GIS_v5_0.mdb";
            string inputDatabaseStringForAccess = "ODBC;"/*DRIVER={sql server}*/+";DSN=" + server + ";DATABASE = " + database + ";Integrated Security=SSPI;";
            AccessHelper.AccessCopySQLTable("GRID_GridResults", inputDatabaseStringForAccess, domain + "GRID_GridResults", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_ZONING_IMP", inputDatabaseStringForAccess, domain + "GRID_ZONING_IMP", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_variables", inputDatabaseStringForAccess, domain + "GRID_variables", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_FE_SELECTION_SETS", inputDatabaseStringForAccess, domain + "GRID_FE_SELECTION_SETS", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_FE_SELECTION_SET_AREAS", inputDatabaseStringForAccess, domain + "GRID_FE_SELECTION_SET_AREAS", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_FE_SCENARIOS", inputDatabaseStringForAccess, domain + "GRID_FE_SCENARIOS", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_FE_SCENARIO_X_PROCESS", inputDatabaseStringForAccess, domain + "GRID_FE_SCENARIO_X_PROCESS", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_FE_PROCESS_GROUP", inputDatabaseStringForAccess, domain + "GRID_FE_PROCESS_GROUP", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_FE_PROCESS", inputDatabaseStringForAccess, domain + "GRID_FE_PROCESS", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_FE_MODEL_RUN", inputDatabaseStringForAccess, domain + "GRID_FE_MODEL_RUN", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_FE_HYETOGRAPHS", inputDatabaseStringForAccess, domain + "GRID_FE_HYETOGRAPHS", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_FE_HYETOGRAPH_DATA", inputDatabaseStringForAccess, domain + "GRID_FE_HYETOGRAPH_DATA", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_FE_GRID_PROJECTS", inputDatabaseStringForAccess, domain + "GRID_FE_GRID_PROJECTS", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_Contaminants", inputDatabaseStringForAccess, domain + "GRID_Contaminants", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_BMP_TYPE_TABLE_GENERAL", inputDatabaseStringForAccess, domain + "GRID_BMP_TYPE_TABLE_GENERAL", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_BMP_PERFORMANCE", inputDatabaseStringForAccess, domain + "GRID_BMP_PERFORMANCE", AccessString);
            AccessHelper.AccessCopySQLTable("GRID_pollutant_loadings", inputDatabaseStringForAccess, domain + "GRID_pollutant_loadings", AccessString);

            //now that the user has probably entered in a server, database, user id, password, and maybe a domain name,
            //the program should try to copy the proper sql server database tables into the Access database.
            //If tables already exist in the database, just write over them.
            //The real problem is that we are currently working with a database that we no longer have create/drop
            //permissions on, so the fastest SQL procedures are no longer available to us.
            AccessHelper.AccessCopyTable("GridResults", "GRID_GridResults", AccessString);
            AccessHelper.AccessCopyTable("ZONING_IMP",  "GRID_ZONING_IMP", AccessString);
            AccessHelper.AccessCopyTable("variables",  "GRID_variables", AccessString);
            AccessHelper.AccessCopyTable("FE_SELECTION_SETS",  "GRID_FE_SELECTION_SETS", AccessString);
            AccessHelper.AccessCopyTable("FE_SELECTION_SET_AREAS",  "GRID_FE_SELECTION_SET_AREAS", AccessString);
            AccessHelper.AccessCopyTable("FE_SCENARIOS",  "GRID_FE_SCENARIOS", AccessString);
            AccessHelper.AccessCopyTable("FE_SCENARIO_X_PROCESS", "GRID_FE_SCENARIO_X_PROCESS", AccessString);
            AccessHelper.AccessCopyTable("FE_PROCESS_GROUP", "GRID_FE_PROCESS_GROUP", AccessString);
            AccessHelper.AccessCopyTable("FE_PROCESS",  "GRID_FE_PROCESS", AccessString);
            AccessHelper.AccessCopyTable("FE_MODEL_RUN",  "GRID_FE_MODEL_RUN", AccessString);
            AccessHelper.AccessCopyTable("FE_HYETOGRAPHS",  "GRID_FE_HYETOGRAPHS", AccessString);
            AccessHelper.AccessCopyTable("FE_HYETOGRAPH_DATA", "GRID_FE_HYETOGRAPH_DATA", AccessString);
            AccessHelper.AccessCopyTable("FE_GRID_PROJECTS", "GRID_FE_GRID_PROJECTS", AccessString);
            AccessHelper.AccessCopyTable("Contaminants", "GRID_Contaminants", AccessString);
            AccessHelper.AccessCopyTable("BMP_TYPE_TABLE_GENERAL", "GRID_BMP_TYPE_TABLE_GENERAL", AccessString);
            AccessHelper.AccessCopyTable("BMP_PERFORMANCE", "GRID_BMP_PERFORMANCE", AccessString);
            AccessHelper.AccessCopyTable("pollutant_loadings", "GRID_pollutant_loadings", AccessString);
            //now copy the linked tables straight into access.  We are doing this because there doesn't seem to be a function
            //that simply copies an SQL table into access.  So this is a sideways way of going about it.

            AccessHelper.AccessDropTable("GRID_GridResults", AccessString);
            AccessHelper.AccessDropTable("GRID_ZONING_IMP", AccessString);
            AccessHelper.AccessDropTable("GRID_variables", AccessString);
            AccessHelper.AccessDropTable("GRID_FE_SELECTION_SETS", AccessString);
            AccessHelper.AccessDropTable("GRID_FE_SELECTION_SET_AREAS", AccessString);
            AccessHelper.AccessDropTable("GRID_FE_SCENARIOS", AccessString);
            AccessHelper.AccessDropTable("GRID_FE_SCENARIO_X_PROCESS", AccessString);
            AccessHelper.AccessDropTable("GRID_FE_PROCESS_GROUP", AccessString);
            AccessHelper.AccessDropTable("GRID_FE_PROCESS", AccessString);
            AccessHelper.AccessDropTable("GRID_FE_MODEL_RUN", AccessString);
            AccessHelper.AccessDropTable("GRID_FE_HYETOGRAPHS", AccessString);
            AccessHelper.AccessDropTable("GRID_FE_HYETOGRAPH_DATA", AccessString);
            AccessHelper.AccessDropTable("GRID_FE_GRID_PROJECTS", AccessString);
            AccessHelper.AccessDropTable("GRID_Contaminants", AccessString);
            AccessHelper.AccessDropTable("GRID_BMP_TYPE_TABLE_GENERAL", AccessString);
            AccessHelper.AccessDropTable("GRID_BMP_PERFORMANCE", AccessString);
            AccessHelper.AccessDropTable("GRID_pollutant_loadings", AccessString);

            try
            {
                gridInterfaceDS.LoadData();
                cmbSelectedProject.SelectedIndex = 0;
                Object[] theObjects = (Object[])AccessHelper.SQLGetListOfServers();
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
                    feModelRunTA.Insert(scenarioID, selectionSetAreaID, hyetographID);
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

            gridModelEngine = new GridModelEngine(projectID, projectDescription,
                txtGridPath.Text, txtPRFPath.Text, txtMIPPath.Text, txtOSFPath.Text, txtOutputDirectory.Text);
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
            Object[] theObjects = (Object[])AccessHelper.SQLGetListOfDatabasesInServer(cboServers.SelectedItem.ToString());
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
            string inputDatabase = "Data Source=SIRTOBY;Initial Catalog=SANDBOX;Integrated Security=SSPI;";//User ID=GIS;Password=Extra$hade";
            string outputDatabase = "Data Source=" + this.cboServers.Text + ";Initial Catalog=" + this.cboDatabases.Text;
            if (trustedConnection == true)
            {
                outputDatabase += ";Trusted_Connection=yes";
            }
            else
            {
                outputDatabase += ";Persist Security Info=True;User ID="+userID+";Password="+password;
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

                    gridModelEngine = new GridModelEngine(projectID, projectDescription,
                txtGridPath.Text, txtPRFPath.Text, txtMIPPath.Text, txtOSFPath.Text, txtOutputDirectory.Text);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_GridResults", inputDatabase, "GRID_GridResults", outputDatabase);
                //"Data Source=SIRTOBY;Initial Catalog=SANDBOX;Integrated Security=SSPI;",//;User ID=GIS;Password=Extra$hade",
                //"GRID_GridResults",
                //"Data Source=" + this.cboServers.Text + ";Initial Catalog=" + this.cboDatabases.Text + ";Trusted_Connection=yes");

                    AccessHelper.SQLCopySQLTable("GIS.GRID_ZONING_IMP", inputDatabase, "GRID_ZONING_IMP", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_variables", inputDatabase, "GRID_variables", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SELECTION_SETS", inputDatabase, "GRID_FE_SELECTION_SETS", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SELECTION_SET_AREAS", inputDatabase, "GRID_FE_SELECTION_SET_AREAS", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SCENARIOS", inputDatabase, "GRID_FE_SCENARIOS", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SCENARIO_X_PROCESS", inputDatabase, "GRID_FE_SCENARIO_X_PROCESS", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_FE_PROCESS_GROUP", inputDatabase, "GRID_FE_PROCESS_GROUP", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_FE_PROCESS", inputDatabase, "GRID_FE_PROCESS", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_FE_MODEL_RUN", inputDatabase, "GRID_FE_MODEL_RUN", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_FE_HYETOGRAPHS", inputDatabase, "GRID_FE_HYETOGRAPHS", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_FE_HYETOGRAPH_DATA", inputDatabase, "GRID_FE_HYETOGRAPH_DATA", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_FE_GRID_PROJECTS", inputDatabase, "GRID_FE_GRID_PROJECTS", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_Contaminants", inputDatabase, "GRID_Contaminants", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_BMP_TYPE_TABLE_GENERAL", inputDatabase, "GRID_BMP_TYPE_TABLE_GENERAL", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_BMP_PERFORMANCE", inputDatabase, "GRID_BMP_PERFORMANCE", outputDatabase);
                    AccessHelper.SQLCopySQLTable("GIS.GRID_pollutant_loadings", inputDatabase, "GRID_pollutant_loadings", outputDatabase);
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

                            gridModelEngine = new GridModelEngine(projectID, projectDescription,
                txtGridPath.Text, txtPRFPath.Text, txtMIPPath.Text, txtOSFPath.Text, txtOutputDirectory.Text);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_GridResults", inputDatabase, "GRID_GridResults", outputDatabase);
                //"Data Source=SIRTOBY;Initial Catalog=SANDBOX;Persist Security Info=True;User ID=GIS;Password=Extra$hade",
                //"GRID_GridResults",
                //"Data Source=" + this.cboServers.Text + ";Initial Catalog=" + this.cboDatabases.Text + ";Trusted_Connection=yes");

                            AccessHelper.SQLCopySQLTable("GIS.GRID_ZONING_IMP", inputDatabase, "GRID_ZONING_IMP", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_variables", inputDatabase, "GRID_variables", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SELECTION_SETS", inputDatabase, "GRID_FE_SELECTION_SETS", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SELECTION_SET_AREAS", inputDatabase, "GRID_FE_SELECTION_SET_AREAS", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SCENARIOS", inputDatabase, "GRID_FE_SCENARIOS", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SCENARIO_X_PROCESS", inputDatabase, "GRID_FE_SCENARIO_X_PROCESS", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_FE_PROCESS_GROUP", inputDatabase, "GRID_FE_PROCESS_GROUP", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_FE_PROCESS", inputDatabase, "GRID_FE_PROCESS", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_FE_MODEL_RUN", inputDatabase, "GRID_FE_MODEL_RUN", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_FE_HYETOGRAPHS", inputDatabase, "GRID_FE_HYETOGRAPHS", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_FE_HYETOGRAPH_DATA", inputDatabase, "GRID_FE_HYETOGRAPH_DATA", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_FE_GRID_PROJECTS", inputDatabase, "GRID_FE_GRID_PROJECTS", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_Contaminants", inputDatabase, "GRID_Contaminants", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_BMP_TYPE_TABLE_GENERAL", inputDatabase, "GRID_BMP_TYPE_TABLE_GENERAL", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_BMP_PERFORMANCE", inputDatabase, "GRID_BMP_PERFORMANCE", outputDatabase);
                            AccessHelper.SQLCopySQLTable("GIS.GRID_pollutant_loadings", inputDatabase, "GRID_pollutant_loadings", outputDatabase);
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
                        AccessHelper.SQLCopySQLTable("GIS.GRID_GridResults", inputDatabase, "GRID_GridResults", outputDatabase);
                //"Data Source=SIRTOBY;Initial Catalog=SANDBOX;Persist Security Info=True;User ID=GIS;Password=Extra$hade",
                //"GRID_GridResults",
                //"Data Source=" + this.cboServers.Text + ";Initial Catalog=" + this.cboDatabases.Text + ";Trusted_Connection=yes");

                        AccessHelper.SQLCopySQLTable("GIS.GRID_ZONING_IMP", inputDatabase, "GRID_ZONING_IMP", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_variables", inputDatabase, "GRID_variables", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SELECTION_SETS", inputDatabase, "GRID_FE_SELECTION_SETS", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SELECTION_SET_AREAS", inputDatabase, "GRID_FE_SELECTION_SET_AREAS", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SCENARIOS", inputDatabase, "GRID_FE_SCENARIOS", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_FE_SCENARIO_X_PROCESS", inputDatabase, "GRID_FE_SCENARIO_X_PROCESS", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_FE_PROCESS_GROUP", inputDatabase, "GRID_FE_PROCESS_GROUP", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_FE_PROCESS", inputDatabase, "GRID_FE_PROCESS", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_FE_MODEL_RUN", inputDatabase, "GRID_FE_MODEL_RUN", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_FE_HYETOGRAPHS", inputDatabase, "GRID_FE_HYETOGRAPHS", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_FE_HYETOGRAPH_DATA", inputDatabase, "GRID_FE_HYETOGRAPH_DATA", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_FE_GRID_PROJECTS", inputDatabase, "GRID_FE_GRID_PROJECTS", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_Contaminants", inputDatabase, "GRID_Contaminants", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_BMP_TYPE_TABLE_GENERAL", inputDatabase, "GRID_BMP_TYPE_TABLE_GENERAL", outputDatabase);
                        if (gridModelEngine != null)
                        {
                            if (gridModelEngine.GridDataTable != null)
                            {
                                AccessHelper.SQLCopySQLTable("GIS.GRID_" + gridModelEngine.GridDataTable, inputDatabase, "GRID_" + gridModelEngine.GridDataTable, outputDatabase);
                            }
                        }
                        AccessHelper.SQLCopySQLTable("GIS.GRID_BMP_PERFORMANCE", inputDatabase, "GRID_BMP_PERFORMANCE", outputDatabase);
                        AccessHelper.SQLCopySQLTable("GIS.GRID_pollutant_loadings", inputDatabase, "GRID_pollutant_loadings", outputDatabase);
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
            userID = ((FormUserIDAndPasswordEntry)sender).UserID;
            password = ((FormUserIDAndPasswordEntry)sender).Password;
            trustedConnection = ((FormUserIDAndPasswordEntry)sender).useTrustedConnection;
        }

        void child_FormClosing2(object sender, FormClosingEventArgs e)
        {
            userID = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).UserID;
            password = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).Password;
            database = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).Database;
            server = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).Server;
            domain = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).Domain;
            trustedConnection = ((FormServerDatabaseUserIDPasswordDomainEntry)sender).useTrustedConnection;
        }
    }
}
