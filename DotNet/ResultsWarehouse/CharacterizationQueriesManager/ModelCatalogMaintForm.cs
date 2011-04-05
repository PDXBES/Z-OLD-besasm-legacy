using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.Modeling.ModelResults;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Utils.Events;
using System.Threading;

namespace SystemsAnalysis.Characterization
{
    public partial class ModelCatalogMaintForm : Form
    {        
        Links links;
        Nodes nodes;
        Dscs dscs;

        LinkHydraulics linkHydraulics;
        NodeHydraulics nodeHydraulics;
        DscHydraulics dscHydraulics;

        XPSWMMResults results;

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
            nodes.Load(links);

            backgroundWorker1.ReportProgress(75, "Loading master dsc...");
            dscs = new Dscs();
            this.dscs.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
            dscs.Load(links);            

            int selectedScenario;
            selectedScenario = (int)grdModelCatalog.Selected.Rows[0].Cells["ScenarioID"].Value;

            backgroundWorker1.ReportProgress(25, "Loading links results from catalog...");
            DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter lhTA;
            lhTA = new DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter();            
            lhTA.FillByScenario(modelCatalogDS.LinkHydraulics,selectedScenario);
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

            for (int i = 0; i < selectedModelCount; i++)
            {               
                int modelID;
                modelID = (int)grdModelCatalog.Selected.Rows[i].Cells["ModelID"].Value;
                ModelCatalogDataSet.ModelCatalogRow modelRow;
                modelRow = modelCatalogDS.ModelCatalog.FindByModelID(modelID);
                UploadModel(modelRow);                
            }
            backgroundWorker1.ReportProgress(20, "Sending catalog updates to database...");
            DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter mcTA;
            mcTA = new DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter();
            mcTA.Update(modelCatalogDS.ModelCatalog);
            backgroundWorker1.ReportProgress(40, "Sending link results updates to database...");
            DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter lhTA;
            lhTA = new DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter();
            lhTA.Update(modelCatalogDS.LinkHydraulics);
            backgroundWorker1.ReportProgress(60, "Sending node results to database...");
            DataAccess.ModelCatalogDataSetTableAdapters.NodeHydraulicsTableAdapter nhTA;
            nhTA = new DataAccess.ModelCatalogDataSetTableAdapters.NodeHydraulicsTableAdapter();
            nhTA.Update(modelCatalogDS.NodeHydraulics);
            backgroundWorker1.ReportProgress(80, "Sending dsc results to database...");
            DataAccess.ModelCatalogDataSetTableAdapters.DscHydraulicsTableAdapter dhTA;
            dhTA = new DataAccess.ModelCatalogDataSetTableAdapters.DscHydraulicsTableAdapter();
            dhTA.Update(modelCatalogDS.DscHydraulics);
        }

        private void UploadModel(ModelCatalogDataSet.ModelCatalogRow modelRow)
        {                                            
            if (modelRow.IsUploaded==1)
            {
                if (MessageBox.Show("Model " + modelRow.ModelName + " was already been uploaded on " + modelRow.UploadDate.ToShortDateString() + ". Replace existing model?", "Model already uploaded", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }
            backgroundWorker1.ReportProgress(10, "Reading output file...");
            results = new XPSWMMResults(modelRow.ModelOutputFile);

            backgroundWorker1.ReportProgress(25, "Calculating link results...");
            UploadLinkResults(modelRow);
            backgroundWorker1.ReportProgress(50, "Calculating node results...");
            UploadNodeResults(modelRow);
            backgroundWorker1.ReportProgress(75, "Calculating dsc results...");
            UploadDscResults(modelRow);

            modelRow.UploadDate = System.DateTime.Now;
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
                mLinkID = Convert.ToInt32(e10Row.CondName.Trim(new char[] { 'M' }));
                if (links.FindByMLinkID(mLinkID) == null)
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
                lhRow.QQRatio = (decimal)e10Row.QqRatio;
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
                    backgroundWorker1.ReportProgress(ultraProgressBar1.Value+1, "Created Node Hydraulic result " +
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
                    backgroundWorker1.ReportProgress(ultraProgressBar1.Value+1,"Created Dsc Hydraulic result " +
                        count.ToString() + ".");
                }
               
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void OnStatusChanged(Utils.Events.StatusChangedArgs e)
        {
            backgroundWorker1.ReportProgress(ultraProgressBar1.Value+1, e.NewStatus);
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


    }
}