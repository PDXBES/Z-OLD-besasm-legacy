using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using ESRI.ArcGIS.Controls;

namespace SystemsAnalysis.Reporting
{
    public partial class EditModelForm : Form
    {
        //private ModelCatalogDataSet modelCatalogDS;
        private ModelCatalogDataSet.ModelCatalogRow modelCatalogRow;
        public EditModelForm(ModelCatalogDataSet modelCatalogDS, ModelCatalogDataSet.ModelCatalogRow modelCatalogRow)
        {
            InitializeComponent();
            try
            {
                this.modelCatalogDS = modelCatalogDS;
                this.modelCatalogRow = modelCatalogRow;
                cmbScenario.DataSource = modelCatalogDS;
                cmbScenario.DataMember = "ModelScenario";
                cmbScenario.DisplayMember = "Description";
                cmbScenario.ValueMember = "ScenarioID";
                txtModelDescription.Text = modelCatalogRow.ModelDescription;
                txtModelName.Text = modelCatalogRow.ModelName;
                txtOutputFilePath.Text = modelCatalogRow.ModelOutputFile;
                txtModelPath.Text = modelCatalogRow.ModelPath;
                txtStudyArea.Text = modelCatalogRow.StudyArea;
                txtModeler.Text = modelCatalogRow.Modeler;
                cmbScenario.Value = modelCatalogRow.ScenarioID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not create new model entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        private void EditModelForm_Load(object sender, EventArgs e)
        {
                        
        }

        private void btnBrowseModel_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFileDialog1.FileName;
                txtModelPath.Text = Path.GetDirectoryName(fullPath) + "\\";
                txtStudyArea.Text = Path.GetDirectoryName(fullPath).Remove(0, Path.GetDirectoryName(fullPath).LastIndexOf("\\") + 1);
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = Path.GetFullPath(txtModelPath.Text);
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFileDialog2.FileName;
                txtOutputFilePath.Text = fullPath;
                txtModelName.Text = Path.GetFileNameWithoutExtension(fullPath);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            return;
        }

        private void btnCommitChanges_Click(object sender, EventArgs e)
        {            
            modelCatalogRow.ModelDescription = txtModelDescription.Text;
            modelCatalogRow.ModelName = txtModelName.Text;
            modelCatalogRow.ModelOutputFile = txtOutputFilePath.Text;
            modelCatalogRow.ModelPath = txtModelPath.Text;
            modelCatalogRow.ModelType = optModelType.Value.ToString();
            modelCatalogRow.StudyArea = txtStudyArea.Text;
            modelCatalogRow.UploadedBy = System.DBNull.Value.ToString();
            modelCatalogRow.IsUploaded = 0;
            modelCatalogRow.TimeFrame = "";
            //modelRow.UploadDate = System.DBNull.Value.ToString();
            modelCatalogRow.Modeler = txtModeler.Text;
            modelCatalogRow.ScenarioID = Convert.ToInt32(cmbScenario.Value);
            this.DialogResult = DialogResult.OK;
            return;
        }

    }
}