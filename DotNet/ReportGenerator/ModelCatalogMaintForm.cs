using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.Modeling.ModelResults;
using SystemsAnalysis.Modeling.ModelUtils.ResultsExtractor;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Utils.Events;
using System.Threading;

namespace SystemsAnalysis.Reporting
{
    public partial class ModelCatalogMaintForm : Form
    {
        Links links;
        Nodes nodes;
        Dscs dscs;

        LinkHydraulics linkHydraulics;
        NodeHydraulics nodeHydraulics;
        DscHydraulics dscHydraulics;

        SystemsAnalysis.Modeling.ModelUtils.ResultsExtractor.XPSWMMResults results;

        public ModelCatalogMaintForm()
        {
            InitializeComponent();

            this.Cursor = Cursors.WaitCursor;
            try
            {
                DataAccess.ModelCatalogDataSetTableAdapters.StormCatalogTableAdapter stormCatalogTA;
                stormCatalogTA = new DataAccess.ModelCatalogDataSetTableAdapters.StormCatalogTableAdapter();
                stormCatalogTA.Fill(modelCatalogDS.StormCatalog);

                DataAccess.ModelCatalogDataSetTableAdapters.ModelScenarioTableAdapter modelScenarioTA;
                modelScenarioTA = new DataAccess.ModelCatalogDataSetTableAdapters.ModelScenarioTableAdapter();
                modelScenarioTA.Fill(modelCatalogDS.ModelScenario);

                DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter modelCatalogTA;
                modelCatalogTA = new DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter();
                modelCatalogTA.Fill(modelCatalogDS.ModelCatalog);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load Model Catalog: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.WaitCursor;
            try
            {
                DataAccess.FlowEstimationDataSetTableAdapters.FlowEstCatchmentsTableAdapter fecTA;
                fecTA = new DataAccess.FlowEstimationDataSetTableAdapters.FlowEstCatchmentsTableAdapter();
                fecTA.Fill(feDS.FlowEstCatchments);

                DataAccess.FlowEstimationDataSetTableAdapters.FECResultsTableAdapter feResultsTA;
                feResultsTA = new DataAccess.FlowEstimationDataSetTableAdapters.FECResultsTableAdapter();
                feResultsTA.Fill(feDS.FECResults);
            }
            catch (System.Data.ConstraintException ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading FEC data: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            cmbSelectScenario.SelectedRow = cmbSelectScenario.Rows[0];
        }

        private void btnUploadModels_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdModelCatalog.Selected.Rows.Count == 0)
                {
                    MessageBox.Show("Please select the model(s) you wish to upload.", "No model selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                foreach (Control c in this.Controls)
                {
                    c.Enabled = false;
                }
                btnAddNewModel.Visible = false;
                btnEditModel.Visible = false;
                btnClose.Visible = false;
                btnCancelUpload.Enabled = true;
                btnCancelUpload.Visible = true;
                ultraProgressBar1.Visible = true;
                ultraProgressBar1.Enabled = true;
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                modelCatalogDS.RejectChanges();
                MessageBox.Show(ex.Message);
                EnableForm();
            }
            finally
            {
                btnAddNewModel.Visible = true;
                btnEditModel.Visible = true;
                btnClose.Visible = true;
                btnCancelUpload.Visible = false;
                this.Cursor = Cursors.Default;
            }

        }
        private void LoadMasterData()
        {
            SAMasterDataSet saMasterDS;
            saMasterDS = new SAMasterDataSet();

            backgroundWorker1.ReportProgress(25, "Loading master links...");

            DataAccess.SAMasterDataSetTableAdapters.MstLinksTableAdapter mstLinksTA;
            mstLinksTA = new DataAccess.SAMasterDataSetTableAdapters.MstLinksTableAdapter();
            mstLinksTA.Fill(saMasterDS.MstLinks);
            backgroundWorker1.ReportProgress(35, "Loading master links...");
            links = new Links(saMasterDS.MstLinks);

            backgroundWorker1.ReportProgress(50, "Loading master nodes...");
            nodes = new Nodes();
            this.nodes.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
            nodes.LoadFromMaster(links);

            backgroundWorker1.ReportProgress(75, "Loading master dsc...");
            dscs = new Dscs();
            this.dscs.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
            dscs.LoadFromMaster(links);

            int selectedScenario;
            selectedScenario = (int)grdModelCatalog.Selected.Rows[0].Cells["ScenarioID"].Value;

            backgroundWorker1.ReportProgress(25, "Loading links results from catalog...");
            DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter lhTA;
            lhTA = new DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter();
            lhTA.FillByScenario(modelCatalogDS.LinkHydraulics, selectedScenario);
            backgroundWorker1.ReportProgress(35, "Creating link hydraulic objects...");
            linkHydraulics = new LinkHydraulics(links, selectedScenario);

            backgroundWorker1.ReportProgress(50, "Loading nodes results from catalog...");
            DataAccess.ModelCatalogDataSetTableAdapters.NodeHydraulicsTableAdapter nhTA;
            nhTA = new DataAccess.ModelCatalogDataSetTableAdapters.NodeHydraulicsTableAdapter();
            nhTA.FillByScenario(modelCatalogDS.NodeHydraulics, selectedScenario);
            backgroundWorker1.ReportProgress(60, "Creating node hydraulic objects...");
            nodeHydraulics = new NodeHydraulics(nodes, selectedScenario);

            backgroundWorker1.ReportProgress(75, "Loading dsc results from catalog...");
            DataAccess.ModelCatalogDataSetTableAdapters.DscHydraulicsTableAdapter dhTA;
            dhTA = new DataAccess.ModelCatalogDataSetTableAdapters.DscHydraulicsTableAdapter();
            dhTA.FillByScenario(modelCatalogDS.DscHydraulics, selectedScenario);
            backgroundWorker1.ReportProgress(85, "Creating dsc hydraulic objects...");
            dscHydraulics = new DscHydraulics(dscs, selectedScenario);
        }
        private void UploadModels()
        {
            int selectedModelCount;
            selectedModelCount = grdModelCatalog.Selected.Rows.Count;
            if (selectedModelCount > 1)
            {
                MessageBox.Show("You may only upload one model at a time.");
                return;
            }

            for (int i = 0; i < selectedModelCount; i++)
            {
                int modelID;
                modelID = (int)grdModelCatalog.Selected.Rows[i].Cells["ModelID"].Value;
                ModelCatalogDataSet.ModelCatalogRow modelRow;
                modelRow = modelCatalogDS.ModelCatalog.FindByModelID(modelID);
                UploadModel(modelRow);
            }
            if (modelCatalogDS.HasErrors)
            {
                string error = "";
                DataRow[] rows;
                rows = modelCatalogDS.ModelCatalog.GetErrors();
                foreach (DataRow row in rows)
                {
                    error = row.RowError;
                    break;
                }
                rows = modelCatalogDS.LinkHydraulics.GetErrors();
                foreach (DataRow row in rows)
                {
                    error = row.RowError;
                    break;
                }
                rows = modelCatalogDS.NodeHydraulics.GetErrors();
                foreach (DataRow row in rows)
                {
                    error = row.RowError;
                    break;
                }
                rows = modelCatalogDS.DscHydraulics.GetErrors();
                foreach (DataRow row in rows)
                {
                    error = row.RowError;
                    break;
                }
                MessageBox.Show("Errors founds in Model Catalog DataSet, upload canceled. First error was: " + error);
                return;
            }

            try
            {
                backgroundWorker1.ReportProgress(20, "Sending catalog updates to database...");
                DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter mcTA;
                mcTA = new DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter();
                mcTA.Update(modelCatalogDS.ModelCatalog);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not commit ModelCatalog: " + ex.Message);
                return;
            }
            try
            {
                backgroundWorker1.ReportProgress(40, "Sending link results updates to database...");
                DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter lhTA;
                lhTA = new DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter();
                lhTA.Update(modelCatalogDS.LinkHydraulics);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not commit LinkHydraulics: " + ex.Message);
                return;
            }
            try
            {
                backgroundWorker1.ReportProgress(60, "Sending node results to database...");
                DataAccess.ModelCatalogDataSetTableAdapters.NodeHydraulicsTableAdapter nhTA;
                nhTA = new DataAccess.ModelCatalogDataSetTableAdapters.NodeHydraulicsTableAdapter();
                nhTA.Update(modelCatalogDS.NodeHydraulics);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not commit NodeHydraulics: " + ex.Message);
                return;
            }
            try
            {
                backgroundWorker1.ReportProgress(80, "Sending dsc results to database...");
                DataAccess.ModelCatalogDataSetTableAdapters.DscHydraulicsTableAdapter dhTA;
                dhTA = new DataAccess.ModelCatalogDataSetTableAdapters.DscHydraulicsTableAdapter();
                dhTA.Update(modelCatalogDS.DscHydraulics);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not commit DscHydraulics: " + ex.Message);
                return;
            }
            modelCatalogDS.NodeHydraulics.GetErrors();
        }

        private void UploadModel(ModelCatalogDataSet.ModelCatalogRow modelRow)
        {
            if (modelRow.IsUploaded == 1)
            {
                if (MessageBox.Show("Model " + modelRow.ModelName + " was already been uploaded on " + modelRow.UploadDate.ToShortDateString() + ". Replace existing model?", "Model already uploaded", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }
            backgroundWorker1.ReportProgress(10, "Reading output file...");
            if (!System.IO.File.Exists(modelRow.ModelOutputFile))
            {
                MessageBox.Show("Output file not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            results = new XPSWMMResults(modelRow.ModelOutputFile);

            backgroundWorker1.ReportProgress(25, "Calculating link results...");
            UploadLinkResults(modelRow);
            backgroundWorker1.ReportProgress(50, "Calculating node results...");
            UploadNodeResults(modelRow);
            backgroundWorker1.ReportProgress(75, "Calculating dsc results...");
            UploadDscResults(modelRow);

            modelRow.UploadDate = System.DateTime.Now;
            modelRow.UploadedBy = System.Environment.UserName;
            modelRow.IsUploaded = 1;
        }
        private void UploadLinkResults(ModelCatalogDataSet.ModelCatalogRow modelRow)
        {

            backgroundWorker1.ReportProgress(ultraProgressBar1.Value + 10, "Retrieving Table E10");
            TableE10DataSet.TableE10DataTable tableE10;
            tableE10 = results.GetTableE10();

            DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter lhTA;
            lhTA = new DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter();

            int count = 0;
            foreach (TableE10DataSet.TableE10Row e10Row in tableE10)
            {
                count++;
                int mLinkID;
                if (!e10Row.CondName.StartsWith("M"))
                {
                    //Not a master link
                    continue;
                }

                mLinkID = Convert.ToInt32(e10Row.CondName.Trim(new char[] { 'M' }));

                Link link = links.FindByMLinkID(mLinkID);
                if (link == null)
                {
                    //Link does not exist in MstLinks
                    continue;
                }

                ModelCatalogDataSet.LinkHydraulicsRow lhRow;
                bool createNewLinkHyraulicRow;
                createNewLinkHyraulicRow = !linkHydraulics.Contains(mLinkID);
                if (createNewLinkHyraulicRow)
                {
                    //Link does not exist in catalog for this scenario, therefore create
                    lhRow = modelCatalogDS.LinkHydraulics.NewLinkHydraulicsRow();
                }
                else
                {
                    //Link already exists in catalog for this scenario, therefore update                                        
                    LinkHydraulic lh = linkHydraulics[mLinkID];
                    lhRow = modelCatalogDS.LinkHydraulics.FindByLinkHydraulicsID(lh.LinkHydraulicsID);
                }
                lhRow.ModelID = modelRow.ModelID;
                lhRow.ScenarioID = modelRow.ScenarioID;
                lhRow.MLinkID = mLinkID;
                lhRow.MaxQ = (decimal)e10Row.MaxQ;
                lhRow.TimeOfMaxQ = e10Row.TimeMaxQ;
                lhRow.MaxV = (decimal)e10Row.MaxV;
                lhRow.TimeOfMaxV = e10Row.TimeMaxV;
                //lhRow.QQRatio = (decimal)e10Row.QqRatio;
                if (link.IsGravityFlow && link.QDesign != 0)
                {
                    lhRow.QQRatio = (decimal)(e10Row.MaxQ / link.QDesign);
                }

                lhRow.MaxUsElev = (decimal)e10Row.MaxUsElev;
                lhRow.MaxDsElev = (decimal)e10Row.MaxDsElev;

                if (createNewLinkHyraulicRow)
                {
                    modelCatalogDS.LinkHydraulics.AddLinkHydraulicsRow(lhRow);
                }
                int quo, rem;
                quo = Math.DivRem(count, 100, out rem);
                if (rem == 0)
                {
                    backgroundWorker1.ReportProgress(ultraProgressBar1.Value + 1, "Created Link Hydraulic result " +
                    count.ToString() + ".");
                }
            }
        }
        private void UploadNodeResults(ModelCatalogDataSet.ModelCatalogRow modelRow)
        {
            backgroundWorker1.ReportProgress(ultraProgressBar1.Value + 10, "Retrieving Table E09");
            TableE09DataSet.TableE09DataTable tableE09;
            tableE09 = results.GetTableE09();
            backgroundWorker1.ReportProgress(ultraProgressBar1.Value + 10, "Retrieving Table E20");
            TableE20DataSet.TableE20DataTable tableE20;
            tableE20 = results.GetTableE20();

            DataAccess.ModelCatalogDataSetTableAdapters.NodeHydraulicsTableAdapter nhTA;
            nhTA = new DataAccess.ModelCatalogDataSetTableAdapters.NodeHydraulicsTableAdapter();

            int count = 0;
            foreach (TableE09DataSet.TableE09Row e09Row in tableE09)
            {
                count++;
                string nodeName;
                nodeName = e09Row.NodeName;
                if (!nodes.Contains(nodeName))
                {
                    //Node does not exist in MstNodes
                    continue;
                }

                TableE20DataSet.TableE20Row e20Row = tableE20.FindByNodeName(nodeName);

                ModelCatalogDataSet.NodeHydraulicsRow nhRow;
                bool createNewNodeHyraulicRow;
                createNewNodeHyraulicRow = !nodeHydraulics.Contains(nodeName);
                if (createNewNodeHyraulicRow)
                {
                    //Node does not exist in catalog for this scenario, therefore create
                    nhRow = modelCatalogDS.NodeHydraulics.NewNodeHydraulicsRow();
                }
                else
                {
                    //Node already exists in catalog for this scenario, therefore update                                        
                    NodeHydraulic nh = nodeHydraulics[nodeName];
                    nhRow = modelCatalogDS.NodeHydraulics.FindByNodeHydraulicsID(nh.NodeHydraulicsID);
                }
                nhRow.NodeName = nodeName;
                nhRow.ModelID = modelRow.ModelID;
                nhRow.ScenarioID = modelRow.ScenarioID;
                nhRow.MaxElevation = (decimal)e09Row.MaxJElev;
                nhRow.TimeOfMaxElev = e09Row.TimeOfMax;
                nhRow.Surcharge = (decimal)e09Row.Surcharge;
                nhRow.Freeboard = (decimal)e09Row.Freeboard;
                nhRow.SurchargeTime = (decimal)e20Row.SurchargeTime;
                nhRow.FloodedTime = (decimal)e20Row.FloodedTime;
                if (createNewNodeHyraulicRow)
                {
                    modelCatalogDS.NodeHydraulics.AddNodeHydraulicsRow(nhRow);
                }
                int quo, rem;
                quo = Math.DivRem(count, 100, out rem);
                if (rem == 0)
                {
                    backgroundWorker1.ReportProgress(ultraProgressBar1.Value + 1, "Created Node Hydraulic result " +
                    count.ToString() + ".");
                }
            }
        }
        private void UploadDscResults(ModelCatalogDataSet.ModelCatalogRow modelRow)
        {
            backgroundWorker1.ReportProgress(ultraProgressBar1.Value + 10, "Retrieving Table E09");
            TableE09DataSet.TableE09DataTable tableE09;
            tableE09 = results.GetTableE09();
            TableE09DataSet.TableE09Row usE09Row, dsE09Row;
            int count = 0;

            DataAccess.ModelCatalogDataSetTableAdapters.DscHydraulicsTableAdapter dhTA;
            dhTA = new DataAccess.ModelCatalogDataSetTableAdapters.DscHydraulicsTableAdapter();

            foreach (Dsc dsc in dscs.Values)
            {
                count++;

                Link link;
                link = links.FindByMLinkID(dsc.ToMLinkSan);
                if (link == null)
                {
                    //A dsc is referencing a link that does not exist in mst_links, data error!
                    continue;
                }
                usE09Row = tableE09.FindByNodeName(link.USNodeName);
                dsE09Row = tableE09.FindByNodeName(link.DSNodeName);
                if (usE09Row == null || dsE09Row == null)
                {
                    //Dsc not part of this model
                    continue;
                }

                ModelCatalogDataSet.DscHydraulicsRow dhRow;
                bool createNewDscHyraulicRow;
                createNewDscHyraulicRow = !dscHydraulics.Contains(dsc.DscID);
                if (createNewDscHyraulicRow)
                {
                    //Link does not exist in catalog for this scenario, therefore create
                    dhRow = modelCatalogDS.DscHydraulics.NewDscHydraulicsRow();
                }
                else
                {
                    //Link already exists in catalog for this scenario, therefore update                                        
                    DscHydraulic dh = dscHydraulics[dsc.DscID];
                    dhRow = modelCatalogDS.DscHydraulics.FindByDscHydraulicsID(dh.DscHydraulicsID);
                }
                dhRow.DscID = dsc.DscID;
                dhRow.ScenarioID = modelRow.ScenarioID;
                dhRow.ModelID = modelRow.ModelID;
                dhRow.HGL = (decimal)(usE09Row.MaxJElev - (dsc.FracToSwrBeg * (usE09Row.MaxJElev - dsE09Row.MaxJElev)));
                dhRow.DeltaHGL = (decimal)dsc.FloodRefElev - dhRow.HGL;
                dhRow.Surcharge = (decimal)((usE09Row.MaxJElev - usE09Row.MaxCrown) - dsc.FracToSwrBeg * ((usE09Row.MaxJElev - usE09Row.MaxCrown) - (dsE09Row.MaxJElev - dsE09Row.MaxCrown)));

                if (createNewDscHyraulicRow)
                {
                    modelCatalogDS.DscHydraulics.AddDscHydraulicsRow(dhRow);
                }
                int quo, rem;
                quo = Math.DivRem(count, 100, out rem);
                if (rem == 0)
                {
                    backgroundWorker1.ReportProgress(ultraProgressBar1.Value + 1, "Created Dsc Hydraulic result " +
                    count.ToString() + ".");
                }

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OnStatusChanged(Utils.Events.StatusChangedArgs e)
        {
            backgroundWorker1.ReportProgress(ultraProgressBar1.Value + 1, e.NewStatus);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadMasterData();
            UploadModels();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ultraProgressBar1.Text = (string)e.UserState;
            ultraProgressBar1.Value = (e.ProgressPercentage > 100 ? 0 : e.ProgressPercentage);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                modelCatalogDS.RejectChanges();
            }
            else
            {
                modelCatalogDS.AcceptChanges();
            }
            EnableForm();

        }

        private void btnCancelUpload_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            modelCatalogDS.RejectChanges();
            EnableForm();
        }

        private void EnableForm()
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }
            btnCancelUpload.Enabled = false;
            btnCancelUpload.Visible = false;
            ultraProgressBar1.Visible = false;
            ultraProgressBar1.Value = 0;
        }

        private void cmbSelectScenario_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            grdModelCatalog.Rows.FilterRow.Activate();
            grdModelCatalog.Rows.FilterRow.Cells["ScenarioID"].Value = (int)cmbSelectScenario.Value;
            grdModelCatalog.Rows.FilterRow.Refresh();
            grdModelCatalog.Rows.FilterRow.ApplyFilters();
            grdModelCatalog.Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.ReloadData);
        }

        private void btnAddNewModel_Click(object sender, EventArgs e)
        {
            AddModelForm addModelForm = new AddModelForm(modelCatalogDS);
            if (addModelForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter mcTA;
                    mcTA = new DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter();
                    mcTA.Update(modelCatalogDS.ModelCatalog);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not update database: " + ex.Message, "Error writing to database");
                }
            }
        }

        private void ModelCatalogMaintForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAddFecResults_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText())
            {
                MessageBox.Show("Clipboard does contain FEC results.", "Error pasting FEC Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string clipboardText = Clipboard.GetText();

            if (!ValidateFecText(clipboardText))
            {
                return;
            }

            int resultsCount = 0;
            try
            {
                resultsCount = UploadFecText(clipboardText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data from clipboard: " + ex.Message, "Paste error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                DataAccess.FlowEstimationDataSetTableAdapters.FECResultsTableAdapter fecResultsTA;
                fecResultsTA = new DataAccess.FlowEstimationDataSetTableAdapters.FECResultsTableAdapter();
                fecResultsTA.Update(feDS.FECResults);
                MessageBox.Show("Successfully loaded " + resultsCount + " FEC results");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending results to database." + ex.Message, "Error loading database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                feDS.FECResults.RejectChanges();
            }

        }

        private bool ValidateFecText(string fecText)
        {
            int alreadyUploadedCount = 0;

            string[] rows = fecText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string row in rows)
            {
                bool newFecResult = false;
                string[] columns = row.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                string fecName;
                if (columns.Length != 21)
                {
                    MessageBox.Show("Incorrect column count; Expected 21, found " + columns.Length + ".", "Error pasting FEC Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                fecName = columns[0];

                FlowEstimationDataSet.FlowEstCatchmentsRow fecRow;
                fecRow = feDS.FlowEstCatchments.FindByFECName(fecName);
                if (fecRow == null)
                {
                    MessageBox.Show("FE Catchment " + fecName + " does not exist in FE Catchment database", "Error pasting FEC Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //FlowEstimationDataSet.FECResultsRow fecResultsRow = feDS.FECResults.FindByFECName(fecName);                
                FlowEstimationDataSet.FECResultsRow fecResultsRow = feDS.FECResults.FindByFECID(fecRow.FecID);

                newFecResult = fecResultsRow == null;
                if (newFecResult)
                {
                    fecResultsRow = feDS.FECResults.NewFECResultsRow();
                }

                if (!newFecResult)
                {
                    alreadyUploadedCount++;
                    if (fecRow.FecID != fecResultsRow.FecID)
                    {
                        //What if an FE Catchment shares a name with an existing FE Result, but the FecID is different?
                        MessageBox.Show("FE Catchment " + fecName + " was already uploaded but FECID is mismatched. Database constraint violation.", "Error pasting FEC Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                fecResultsRow.FecName = fecName;
                try
                {
                    AssignFecResults(fecResultsRow, columns);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error parsing columns: " + ex.Message, "Error pasting FEC Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            if (alreadyUploadedCount > 0)
            {
                if (MessageBox.Show("Results already exist for " + alreadyUploadedCount + " of " + rows.Length + " FE catchments. Overwrite results?", "FEC Results already uploaded", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    feDS.FECResults.RejectChanges();
                    return false;
                }
            }

            return true;
        }

        private int UploadFecText(string fecText)
        {
            string[] rows = fecText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string row in rows)
            {
                bool newFecResult = false;
                string[] columns = row.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                string fecName;

                fecName = columns[0];

                FlowEstimationDataSet.FlowEstCatchmentsRow fecRow;
                fecRow = feDS.FlowEstCatchments.FindByFECName(fecName);
                if (fecRow == null)
                {
                    MessageBox.Show("FE Catchment " + fecName + " does not exist in FE Catchment database", "Error pasting FEC Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }

                //FlowEstimationDataSet.FECResultsRow fecResultsRow = feDS.FECResults.FindByFECName(fecName);                
                FlowEstimationDataSet.FECResultsRow fecResultsRow = feDS.FECResults.FindByFECID(fecRow.FecID);

                newFecResult = fecResultsRow == null;
                if (newFecResult)
                {
                    fecResultsRow = feDS.FECResults.NewFECResultsRow();
                }

                fecResultsRow.FecName = fecName;
                fecResultsRow.FecID = feDS.FlowEstCatchments.FindByFECName(fecName).FecID;

                fecResultsRow = AssignFecResults(fecResultsRow, columns);

                if (newFecResult)
                {
                    feDS.FECResults.AddFECResultsRow(fecResultsRow);
                }
            }
            return rows.Length;
        }

        private FlowEstimationDataSet.FECResultsRow AssignFecResults(FlowEstimationDataSet.FECResultsRow fecResultsRow, string[] columns)
        {
            fecResultsRow.DesignManualSeweredABF = Convert.ToDecimal(columns[1]);
            fecResultsRow.DesignManualStaticABF = Convert.ToDecimal(columns[2]);
            fecResultsRow.DesignManualFUABF = Convert.ToDecimal(columns[3]);
            fecResultsRow.FUEXABFFactor = Convert.ToDecimal(columns[4]);
            fecResultsRow.FUFUABFFactor = Convert.ToDecimal(columns[5]);
            fecResultsRow.SeweredArea = Convert.ToDecimal(columns[6]);
            fecResultsRow.StaticArea = Convert.ToDecimal(columns[7]);
            fecResultsRow.DevelopmentArea = Convert.ToDecimal(columns[8]);
            fecResultsRow.RedevelopmentArea = Convert.ToDecimal(columns[9]);
            fecResultsRow.NewDevelopmentArea = Convert.ToDecimal(columns[10]);
            fecResultsRow.EXRDIICfs = Convert.ToDecimal(columns[11]);
            fecResultsRow.EXRDIIGpad = Convert.ToDecimal(columns[12]);
            fecResultsRow.FUEXRDIICfs = Convert.ToDecimal(columns[13]);
            fecResultsRow.FUEXRDIIGpad = Convert.ToDecimal(columns[14]);
            fecResultsRow.FUFURDIICfs = Convert.ToDecimal(columns[15]);
            fecResultsRow.FUFURDIIGpad = Convert.ToDecimal(columns[16]);
            fecResultsRow.FUEXRDIIFactor = Convert.ToDecimal(columns[17]);
            fecResultsRow.FUFURDIIFactor = Convert.ToDecimal(columns[18]);
            fecResultsRow.MonitoredABF = Convert.ToDecimal(columns[19]);
            fecResultsRow.MonitoredPeakBF = Convert.ToDecimal(columns[20]);
            fecResultsRow.UploadedBy = Environment.UserName;
            fecResultsRow.UploadDate = DateTime.Now;

            return fecResultsRow;
        }

        private void btnEditModel_Click(object sender, EventArgs e)
        {
            int selectedModelCount;
            selectedModelCount = grdModelCatalog.Selected.Rows.Count;
            if (selectedModelCount > 1)
            {
                MessageBox.Show("You may only upload one model at a time.");
                return;
            }

            int modelID;
            modelID = (int)grdModelCatalog.Selected.Rows[0].Cells["ModelID"].Value;
            ModelCatalogDataSet.ModelCatalogRow modelCatalogRow;
            modelCatalogRow = modelCatalogDS.ModelCatalog.FindByModelID(modelID);

            EditModelForm editModelForm;
            editModelForm = new EditModelForm(modelCatalogDS, modelCatalogRow);
            if (editModelForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter mcTA;
                    mcTA = new DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter();
                    mcTA.Update(modelCatalogDS.ModelCatalog);
                    modelCatalogDS.AcceptChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not update database: " + ex.Message, "Error writing to database");
                }
            }
            else
            {
                modelCatalogDS.RejectChanges();
            }
        }

    }
}