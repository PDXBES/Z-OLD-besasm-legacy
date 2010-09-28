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

        public FrmGridAnalysis()
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(DoSplash));
            th.Start();
            InitializeComponent();
        }

        private void frmGridAnalysis_Load(object sender, EventArgs e)
        {
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
            //database for the gridmodel to actually do that so this is really jsut a temporary workaround.
            string inputDatabase = "Data Source=SIRTOBY;Initial Catalog=SANDBOX;Persist Security Info=True;User ID=GIS;Password=Extra$hade";
            string outputDatabase = "Data Source=" + this.cboServers.Text + ";Initial Catalog=" + this.cboDatabases.Text + ";Trusted_Connection=yes";


            AccessHelper.SQLCopySQLTable("GRID_GridResults",
                "Data Source=SIRTOBY;Initial Catalog=SANDBOX;Persist Security Info=True;User ID=GIS;Password=Extra$hade",
                "GRID_GridResults",
                "Data Source=" + this.cboServers.Text + ";Initial Catalog=" + this.cboDatabases.Text + ";Trusted_Connection=yes");

            AccessHelper.SQLCopySQLTable("GRID_ZONING_IMP", inputDatabase, "GRID_ZONING_IMP", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_variables", inputDatabase, "GRID_variables", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_FE_SELECTION_SETS", inputDatabase, "GRID_FE_SELECTION_SETS", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_FE_SELECTION_SET_AREAS", inputDatabase, "GRID_FE_SELECTION_SET_AREAS", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_FE_SCENARIOS", inputDatabase, "GRID_FE_SCENARIOS", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_FE_SCENARIO_X_PROCESS", inputDatabase, "GRID_FE_SCENARIO_X_PROCESS", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_FE_PROCESS_GROUP", inputDatabase, "GRID_FE_PROCESS_GROUP", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_FE_PROCESS", inputDatabase, "GRID_FE_PROCESS", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_FE_MODEL_RUN", inputDatabase, "GRID_FE_MODEL_RUN", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_FE_HYETOGRAPHS", inputDatabase, "GRID_FE_HYETOGRAPHS", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_FE_HYETOGRAPH_DATA", inputDatabase, "GRID_FE_HYETOGRAPH_DATA", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_FE_GRID_PROJECTS", inputDatabase, "GRID_FE_GRID_PROJECTS", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_Contaminants", inputDatabase, "GRID_Contaminants", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_BMP_TYPE_TABLE_GENERAL", inputDatabase, "GRID_BMP_TYPE_TABLE_GENERAL", outputDatabase);
            if(gridModelEngine != null)
            {
                if (gridModelEngine.GridDataTable != null)
                {
                    AccessHelper.SQLCopySQLTable("GRID_" + gridModelEngine.GridDataTable, inputDatabase, "GRID_" + gridModelEngine.GridDataTable, outputDatabase);
                }
            }
            AccessHelper.SQLCopySQLTable("GRID_BMP_PERFORMANCE", inputDatabase, "GRID_BMP_PERFORMANCE", outputDatabase);
            AccessHelper.SQLCopySQLTable("GRID_pollutant_loadings", inputDatabase, "GRID_pollutant_loadings", outputDatabase);
        }

    }

}
