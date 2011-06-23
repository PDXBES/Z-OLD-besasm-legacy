using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using System.IO;

namespace SystemsAnalysis.Reporting
{
    /// <summary>
    /// Form for read-only view of the ModelCatalog DataSet
    /// </summary>
    public partial class ModelCatalogViewForm : Form
    {

        private Model[] selectedModels;
        private string scenario;
        private string studyArea;

        public ModelCatalogViewForm()
        {
            InitializeComponent();

            //this.AddInfoStatus("Loading model catalog data from '" + settings.DataSource.FindByName("ResultsWarehouseDB").DataSource_Text + "'.");
            try
            {
                DataAccess.ModelCatalogDataSetTableAdapters.StormCatalogTableAdapter scTA;
                scTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.StormCatalogTableAdapter();
                DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter mcTA;
                mcTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter();
                DataAccess.ModelCatalogDataSetTableAdapters.ModelScenarioTableAdapter msTA;
                msTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.ModelScenarioTableAdapter();
                scTA.Fill(modelCatalogDS.StormCatalog);
                msTA.Fill(modelCatalogDS.ModelScenario);
                mcTA.Fill(modelCatalogDS.ModelCatalog);

                cmbSelectScenario.SelectedRow = cmbSelectScenario.Rows[0];
            }
            catch (Exception ex)
            {
                /*this.AddErrorStatus("Could not load model catalog. Verify location of ResultsWarehouseDB: '"
                    + settings.DataSource.FindByName("ResultsWarehouseDB").DataSource_Text + "'.");
                this.AddErrorStatus(ex.ToString());*/
            }
        }

        private void grdModelCatalog_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private Model[] ReadSelectedModels()
        {
                Model[] models;
                int selectedModelCount;
                selectedModelCount = grdModelCatalog.Selected.Rows.Count;
                models = new Model[selectedModelCount];

                for (int i = 0; i < selectedModelCount; i++)
                {
                    int selectedModelID;                    
                    selectedModelID = (int)grdModelCatalog.Selected.Rows[i].Cells["ModelID"].Value;                    
                    DataAccess.ModelCatalogDataSet.ModelCatalogRow selectedModelRow;
                    selectedModelRow = modelCatalogDS.ModelCatalog.FindByModelID(selectedModelID);
                    models[i] = new Model(selectedModelRow.ModelPath);                                             
                }
                return models;
        }

        /// <summary>
        /// Gets an array of selected models from the data grid
        /// </summary>
        public Model[] SelectedModels
        {
            get { return this.selectedModels; }
        }
        /// <summary>
        /// Gets the name of the selected scenario
        /// </summary>
        public string Scenario
        {
            get { return this.scenario; }
        }
        /// <summary>
        /// Gets the name of the selected model's study area
        /// </summary>
        public string StudyArea
        {
            get { return this.studyArea; }
        }

        private void cmbSelectScenario_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            grdModelCatalog.Rows.FilterRow.Activate();
            grdModelCatalog.Rows.FilterRow.Cells["ScenarioID"].Value = (int)cmbSelectScenario.Value;
            grdModelCatalog.Rows.FilterRow.Refresh();
            grdModelCatalog.Rows.FilterRow.ApplyFilters();
            grdModelCatalog.Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.ReloadData);

        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                int selectedModelCount;
                selectedModelCount = grdModelCatalog.Selected.Rows.Count;
                if (selectedModelCount == 0)
                {
                    MessageBox.Show("No Model Selected!");
                    return;
                }
                else if (selectedModelCount > 1)
                {
                    if (MessageBox.Show("Multiple models selected... import start/stop links from all selected models?", "Use Multiple Models?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                    studyArea = "Multiple model areas";
                }
                else
                {
                    studyArea = (string)grdModelCatalog.Selected.Rows[0].Cells["StudyArea"].Value;
                }
                selectedModels = this.ReadSelectedModels();
                scenario = (string)cmbSelectScenario.Text;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
    }
}