using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWI_2
{
    public partial class FormSWSPFieldDataAdministration : Form
    {
        private int _CurrentSurveyPage;
        private int _CurrentView;
        private int _lastGlobalID;
        private int _CurrentWatershed;
        private int _CurrentSubwatershed;

        public int CurrentSurveyPage
        {
            get { return _CurrentSurveyPage; }
            set { _CurrentSurveyPage = value; }
        }

        public int CurrentView
        {
            get { return _CurrentView; }
            set { _CurrentView = value; }
        }

        public int CurrentWatershed
        {
            get { return _CurrentWatershed; }
            set { _CurrentWatershed = value; }
        }

        public int CurrentSubwatershed
        {
            get { return _CurrentSubwatershed; }
            set { _CurrentSubwatershed = value; }
        }

        public FormSWSPFieldDataAdministration()
        {
            InitializeComponent();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _TextBrush;
            Brush _FillBrush;

            // Get the item from the collection.
            TabPage _TabPage = tabControlFormSWSPFieldDataAdministration.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _TabBounds = tabControlFormSWSPFieldDataAdministration.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                // Draw a different background color, and don't paint a focus rectangle.
                _TextBrush = new SolidBrush(Color.Black);
                _FillBrush = new SolidBrush(this.BackColor);
                g.FillRectangle((Brush)_FillBrush /*Brushes.LightGray*/, e.Bounds);
            }
            else
            {
                _TextBrush = new System.Drawing.SolidBrush(Color.LightGray/*e.ForeColor*/);
                e.DrawBackground();
            }

            // Use our own font. Because we CAN.
            Font _TabFont = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _StringFlags = new StringFormat();
            _StringFlags.Alignment = StringAlignment.Center;
            _StringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_TabPage.Text, _TabFont, _TextBrush,
                         _TabBounds, new StringFormat(_StringFlags));
        }
        private void buttonUpdateDatabase_Click(object sender, EventArgs e)
        {
            FormSWSPFieldDataAdministration child = new FormSWSPFieldDataAdministration();

            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
        }

        private void buttonAddView_Click(object sender, EventArgs e)
        {
            //CurrentWatershed
            //CurrentSubwatershed
            FormAddView child = new FormAddView();

            this.Enabled = false;
            child.ShowDialog();
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_VIEW' table. You can move, or remove it, as needed.
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            this.Enabled = true;
        }

        private void buttonAddSurveyPage_Click(object sender, EventArgs e)
        {
            FormAddSurvey child = new FormAddSurvey();

            this.Enabled = false;
            child.ShowDialog();
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_VIEW' table. You can move, or remove it, as needed.
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            this.Enabled = true;
        }

        private void FormSWSPFieldDataAdministration_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_MATERIAL_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_MATERIAL_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_MATERIAL_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SHAPE_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_SHAPE_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SHAPE_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_FACING_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_FACING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_FACING_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_EVALUATOR' table. You can move, or remove it, as needed.
            this.sWSP_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_EVALUATOR);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_VIEW' table. You can move, or remove it, as needed.
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SUBWATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_WATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonWatershedsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_WATERSHEDTableAdapter.Insert("New Watershed", "");
            this.sWSP_WATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_WATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_WATERSHED);
        }

        private void buttonWatershedsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSPWATERSHEDBindingSource.EndEdit();
                this.sWSP_WATERSHEDTableAdapter.Update(sANDBOXDataSet);
                this.sWSP_WATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_WATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_WATERSHED);
                dataGridViewWatersheds.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update may not have been performed due to improper data input");
            }
        }

        private void buttonWatershedsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you wish to delete Watershed " + (string)(((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed"]) + " and ALL of its related records?", "Really bad idea", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                DialogResult dr2 = MessageBox.Show("It is a bad idea to delete Watershed " + (string)(((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed"]) + " and ALL of its related records!  Are you SURE you want to do this?", "Really bad idea", MessageBoxButtons.YesNo);

                if (dr2 == DialogResult.Yes)
                {
                    try
                    {
                        this.sWSP_WATERSHEDTableAdapter.DeleteQuery((int)(((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"]));
                        this.sWSP_WATERSHEDTableAdapter.Update(sANDBOXDataSet);
                        this.sWSP_WATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_WATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_WATERSHED);
                        dataGridViewWatersheds.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("You cannot delete that watershed at this time.  Try deleting associated subwatersheds.");
                    }
                }
            }
        }

        private void buttonSubwatershedsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_SUBWATERSHEDTableAdapter.Insert((int)(((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"]), "New Subwatershed", "");
            this.sWSP_SUBWATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_SUBWATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_SUBWATERSHED);
        }

        private void buttonSubwatershedsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.fKSUBWATERSHEDWATERSHEDBindingSource1.EndEdit();
                this.sWSP_SUBWATERSHEDTableAdapter.Update(sANDBOXDataSet);
                this.sWSP_SUBWATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_SUBWATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_SUBWATERSHED);
                dataGridViewSubwatersheds.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update may not have been completed due to improper data input");
            }
        }

        private void buttonSubwatershedsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you wish to delete Subwatershed " + (string)(((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource1.Current)["subwatershed"]) + " and ALL of its related records?", "Really bad idea", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                DialogResult dr2 = MessageBox.Show("It is a bad idea to delete Subwatershed " + (string)(((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource1.Current)["subwatershed"]) + " and ALL of its related records!  Are you SURE you want to do this?", "Really bad idea", MessageBoxButtons.YesNo);

                if (dr2 == DialogResult.Yes)
                {
                    try
                    {
                        this.sWSP_SUBWATERSHEDTableAdapter.DeleteQuery((int)(((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource1.Current)["subwatershed_id"]));
                        this.sWSP_SUBWATERSHEDTableAdapter.Update(sANDBOXDataSet);
                        this.sWSP_SUBWATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_SUBWATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_SUBWATERSHED);
                        dataGridViewWatersheds.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("You cannot delete that subwatershed at this time.  Try deleting associated maps.");
                    }
                }
            }
        }

        private void buttonEvaluatorsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_EVALUATORTableAdapter.Insert("", "LastName", "FirstName");
            this.sWSP_EVALUATORTableAdapter.Fill((SANDBOXDataSet.SWSP_EVALUATORDataTable)((SANDBOXDataSet)this.sWSPEVALUATORBindingSource.DataSource).SWSP_EVALUATOR);
        }

        private void buttonEvaluatorsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSPEVALUATORBindingSource.EndEdit();
                this.sWSP_EVALUATORTableAdapter.Update(sANDBOXDataSet);
                this.sWSP_EVALUATORTableAdapter.Fill((SANDBOXDataSet.SWSP_EVALUATORDataTable)((SANDBOXDataSet)this.sWSPEVALUATORBindingSource.DataSource).SWSP_EVALUATOR);
                dataGridViewEvaluators.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update may not have been completed due to improper data format");
            }
        }

        private void buttonEvaluatorsDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSP_EVALUATORTableAdapter.DeleteQuery((int)(((System.Data.DataRowView)sWSPEVALUATORBindingSource.Current)["evaluator_id"]));
                this.sWSP_EVALUATORTableAdapter.Update(sANDBOXDataSet);
                this.sWSP_EVALUATORTableAdapter.Fill((SANDBOXDataSet.SWSP_EVALUATORDataTable)((SANDBOXDataSet)this.sWSPEVALUATORBindingSource.DataSource).SWSP_EVALUATOR);
                dataGridViewEvaluators.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You may not have selected an evaluator to delete");
            }
        }

        private void buttonCulvertOpeningsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Insert("", "New Opening Type");
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_CULVERT_OPENING_TYPEDataTable)((SANDBOXDataSet)this.sWSPCULVERTOPENINGTYPEBindingSource.DataSource).SWSP_CULVERT_OPENING_TYPE);
        }

        private void buttonCulvertOpeningsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSPCULVERTOPENINGTYPEBindingSource.EndEdit();
                this.sWSP_CULVERT_OPENING_TYPETableAdapter.Update(sANDBOXDataSet);
                this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_CULVERT_OPENING_TYPEDataTable)((SANDBOXDataSet)this.sWSPCULVERTOPENINGTYPEBindingSource.DataSource).SWSP_CULVERT_OPENING_TYPE);
                dataGridViewCulvertOpenings.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update may not have been completed due to improper data format");
            }
        }

        private void buttonCulvertOpeningsDelete_Click(object sender, EventArgs e)
        {
                try
                {
                    this.sWSP_CULVERT_OPENING_TYPETableAdapter.DeleteQuery((int)((System.Data.DataRowView)sWSPCULVERTOPENINGTYPEBindingSource.Current)["culvert_opening_type_id"]);
                    this.sWSP_CULVERT_OPENING_TYPETableAdapter.Update(sANDBOXDataSet);
                    this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_CULVERT_OPENING_TYPEDataTable)((SANDBOXDataSet)this.sWSPCULVERTOPENINGTYPEBindingSource.DataSource).SWSP_CULVERT_OPENING_TYPE);
                    dataGridViewCulvertOpenings.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("You may not have selected an opening type to delete");
                }
        }

        private void buttonFacingsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_FACING_TYPETableAdapter.Insert("", "New Facing Type");
            this.sWSP_FACING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_FACING_TYPEDataTable)((SANDBOXDataSet)this.sWSPFACINGTYPEBindingSource.DataSource).SWSP_FACING_TYPE);
        }

        private void buttonFacingsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSPFACINGTYPEBindingSource.EndEdit();
                this.sWSP_FACING_TYPETableAdapter.Update(sANDBOXDataSet);
                this.sWSP_FACING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_FACING_TYPEDataTable)((SANDBOXDataSet)this.sWSPFACINGTYPEBindingSource.DataSource).SWSP_FACING_TYPE);
                dataGridViewFacings.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update may not have been completed due to improper data format");
            }
        }

        private void buttonFacingsDelete_Click(object sender, EventArgs e)
        {
                try
                {
                    this.sWSP_FACING_TYPETableAdapter.DeleteQuery((int)((System.Data.DataRowView)sWSPFACINGTYPEBindingSource.Current)["facing_type_id"]);
                    this.sWSP_FACING_TYPETableAdapter.Update(sANDBOXDataSet);
                    this.sWSP_FACING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_FACING_TYPEDataTable)((SANDBOXDataSet)this.sWSPFACINGTYPEBindingSource.DataSource).SWSP_FACING_TYPE);
                    dataGridViewFacings.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("You may not have selected an opening type to delete");
                }
        }

        private void buttonShapesAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_SHAPE_TYPETableAdapter.Insert("NewShp", "New Shape Description");
            this.sWSP_SHAPE_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_SHAPE_TYPEDataTable)((SANDBOXDataSet)this.sWSPSHAPETYPEBindingSource.DataSource).SWSP_SHAPE_TYPE);
        }

        private void buttonShapesUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSPSHAPETYPEBindingSource.EndEdit();
                this.sWSP_SHAPE_TYPETableAdapter.Update(sANDBOXDataSet);
                this.sWSP_SHAPE_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_SHAPE_TYPEDataTable)((SANDBOXDataSet)this.sWSPSHAPETYPEBindingSource.DataSource).SWSP_SHAPE_TYPE);
                dataGridViewShapes.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update may not have been completed due to improper data format");
            }
        }

        private void buttonShapesDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSP_SHAPE_TYPETableAdapter.DeleteQuery((int)((System.Data.DataRowView)sWSPSHAPETYPEBindingSource.Current)["shape_type_id"]);
                this.sWSP_SHAPE_TYPETableAdapter.Update(sANDBOXDataSet);
                this.sWSP_SHAPE_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_SHAPE_TYPEDataTable)((SANDBOXDataSet)this.sWSPSHAPETYPEBindingSource.DataSource).SWSP_SHAPE_TYPE);
                dataGridViewShapes.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You might not have selected a shape to delete!");
            }
        }

        private void buttonMaterialsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_MATERIAL_TYPETableAdapter.Insert("0NewMa", "New material description");
            this.sWSP_MATERIAL_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_MATERIAL_TYPEDataTable)((SANDBOXDataSet)this.sWSPMATERIALTYPEBindingSource.DataSource).SWSP_MATERIAL_TYPE);
        }

        private void buttonMaterialsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSPMATERIALTYPEBindingSource.EndEdit();
                this.sWSP_MATERIAL_TYPETableAdapter.Update(sANDBOXDataSet);
                this.sWSP_MATERIAL_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_MATERIAL_TYPEDataTable)((SANDBOXDataSet)this.sWSPMATERIALTYPEBindingSource.DataSource).SWSP_MATERIAL_TYPE);
                dataGridViewMaterials.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update may not have been completed due to improper data format");
            }
        }

        private void buttonMaterialsDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSP_MATERIAL_TYPETableAdapter.DeleteQuery((int)((System.Data.DataRowView)sWSPMATERIALTYPEBindingSource.Current)["material_type_id"]);
                this.sWSP_MATERIAL_TYPETableAdapter.Update(sANDBOXDataSet);
                this.sWSP_MATERIAL_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_MATERIAL_TYPEDataTable)((SANDBOXDataSet)this.sWSPMATERIALTYPEBindingSource.DataSource).SWSP_MATERIAL_TYPE);
                dataGridViewMaterials.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You may not have selected a material to delete");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSP_SURVEY_PAGETableAdapter.DeleteQuery((int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource5.Current)["survey_page_id"]);
                this.sWSP_SURVEY_PAGETableAdapter.Update(sANDBOXDataSet);
                this.sWSP_SURVEY_PAGETableAdapter.Fill((SANDBOXDataSet.SWSP_SURVEY_PAGEDataTable)((SANDBOXDataSet)this.sWSPSURVEYPAGEBindingSource.DataSource).SWSP_SURVEY_PAGE);
                dataGridViewSurveyPages.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please delete all of the associated culverts, ditches or pipes before trying to delete this survey page");
            }
        }

        private void ultraButtonDeleteSelectedView_Click(object sender, EventArgs e)
        {
            try
            {
                this.sWSP_VIEWTableAdapter.DeleteQuery((int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource3.Current)["view_id"]);
                this.sWSP_VIEWTableAdapter.Update(sANDBOXDataSet);
                this.sWSP_VIEWTableAdapter.Fill((SANDBOXDataSet.SWSP_VIEWDataTable)((SANDBOXDataSet)this.sWSPVIEWBindingSource.DataSource).SWSP_VIEW);
                ultraGrid1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please remove all of the associated survey pages before deleting this view");
            }
        }
    }
}
