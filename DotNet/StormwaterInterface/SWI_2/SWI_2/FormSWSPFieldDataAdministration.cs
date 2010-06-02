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
            TabPage _TabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _TabBounds = tabControl1.GetTabRect(e.Index);

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
            FormAddView child = new FormAddView();

            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
        }

        private void buttonAddSurveyPage_Click(object sender, EventArgs e)
        {
            FormAddSurvey child = new FormAddSurvey();

            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
        }

        private void FormSWSPFieldDataAdministration_Load(object sender, EventArgs e)
        {
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
            this.sWSPWATERSHEDBindingSource.EndEdit();
            this.sWSP_WATERSHEDTableAdapter.Update(sANDBOXDataSet);
            this.sWSP_WATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_WATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_WATERSHED);
            dataGridView1.Refresh();
        }

        private void buttonWatershedsDelete_Click(object sender, EventArgs e)
        {
            this.sWSP_WATERSHEDTableAdapter.DeleteQuery((int)(((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"]));
            this.sWSP_WATERSHEDTableAdapter.Update(sANDBOXDataSet);
            this.sWSP_WATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_WATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_WATERSHED);
            dataGridView1.Refresh();
        }

        private void buttonSubwatershedsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_SUBWATERSHEDTableAdapter.Insert((int)(((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"]), "New Subwatershed", "");
            this.sWSP_SUBWATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_SUBWATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_SUBWATERSHED);
        }

        private void buttonSubwatershedsUpdate_Click(object sender, EventArgs e)
        {
            this.fKSUBWATERSHEDWATERSHEDBindingSource.EndEdit();
            this.sWSP_SUBWATERSHEDTableAdapter.Update(sANDBOXDataSet);
            this.sWSP_SUBWATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_SUBWATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_SUBWATERSHED);
            dataGridView2.Refresh();
        }

        private void buttonSubwatershedsDelete_Click(object sender, EventArgs e)
        {
            this.sWSP_SUBWATERSHEDTableAdapter.DeleteQuery((int)(((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"]));
            this.sWSP_SUBWATERSHEDTableAdapter.Update(sANDBOXDataSet);
            this.sWSP_SUBWATERSHEDTableAdapter.Fill((SANDBOXDataSet.SWSP_SUBWATERSHEDDataTable)((SANDBOXDataSet)this.sWSPWATERSHEDBindingSource.DataSource).SWSP_SUBWATERSHED);
            dataGridView1.Refresh();
        }

        private void buttonEvaluatorsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_EVALUATORTableAdapter.Insert("", "FirstName", "LastName");
            this.sWSP_EVALUATORTableAdapter.Fill((SANDBOXDataSet.SWSP_EVALUATORDataTable)((SANDBOXDataSet)this.sWSPEVALUATORBindingSource.DataSource).SWSP_EVALUATOR);
        }

        private void buttonEvaluatorsUpdate_Click(object sender, EventArgs e)
        {
            this.sWSPEVALUATORBindingSource.EndEdit();
            this.sWSP_EVALUATORTableAdapter.Update(sANDBOXDataSet);
            this.sWSP_EVALUATORTableAdapter.Fill((SANDBOXDataSet.SWSP_EVALUATORDataTable)((SANDBOXDataSet)this.sWSPEVALUATORBindingSource.DataSource).SWSP_EVALUATOR);
            dataGridView3.Refresh();
        }

        private void buttonEvaluatorsDelete_Click(object sender, EventArgs e)
        {
            this.sWSP_EVALUATORTableAdapter.DeleteQuery((int)(((System.Data.DataRowView)sWSPEVALUATORBindingSource.Current)["evaluator_id"]));
            this.sWSP_EVALUATORTableAdapter.Update(sANDBOXDataSet);
            this.sWSP_EVALUATORTableAdapter.Fill((SANDBOXDataSet.SWSP_EVALUATORDataTable)((SANDBOXDataSet)this.sWSPEVALUATORBindingSource.DataSource).SWSP_EVALUATOR);
            dataGridView3.Refresh();
        }

        private void buttonCulvertOpeningsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Insert("", "New Opening Type");
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_CULVERT_OPENING_TYPEDataTable)((SANDBOXDataSet)this.sWSPCULVERTOPENINGTYPEBindingSource.DataSource).SWSP_CULVERT_OPENING_TYPE);
        }

        private void buttonCulvertOpeningsUpdate_Click(object sender, EventArgs e)
        {
            this.sWSPCULVERTOPENINGTYPEBindingSource.EndEdit();
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Update(sANDBOXDataSet);
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_CULVERT_OPENING_TYPEDataTable)((SANDBOXDataSet)this.sWSPCULVERTOPENINGTYPEBindingSource.DataSource).SWSP_CULVERT_OPENING_TYPE);
            dataGridView4.Refresh();
        }

        private void buttonCulvertOpeningsDelete_Click(object sender, EventArgs e)
        {
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.DeleteQuery((int)((System.Data.DataRowView)sWSPCULVERTOPENINGTYPEBindingSource.Current)["culvert_opening_type_id"]);
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Update(sANDBOXDataSet);
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_CULVERT_OPENING_TYPEDataTable)((SANDBOXDataSet)this.sWSPCULVERTOPENINGTYPEBindingSource.DataSource).SWSP_CULVERT_OPENING_TYPE);
            dataGridView4.Refresh();
        }

        private void buttonFacingsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_FACING_TYPETableAdapter.Insert("", "New Facing Type");
            this.sWSP_FACING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_FACING_TYPEDataTable)((SANDBOXDataSet)this.sWSPFACINGTYPEBindingSource.DataSource).SWSP_FACING_TYPE);
        }

        private void buttonFacingsUpdate_Click(object sender, EventArgs e)
        {
            this.sWSPFACINGTYPEBindingSource.EndEdit();
            this.sWSP_FACING_TYPETableAdapter.Update(sANDBOXDataSet);
            this.sWSP_FACING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_FACING_TYPEDataTable)((SANDBOXDataSet)this.sWSPFACINGTYPEBindingSource.DataSource).SWSP_FACING_TYPE);
            dataGridView5.Refresh();
        }

        private void buttonFacingsDelete_Click(object sender, EventArgs e)
        {
            this.sWSP_FACING_TYPETableAdapter.DeleteQuery((int)((System.Data.DataRowView)sWSPFACINGTYPEBindingSource.Current)["facing_type_id"]);
            this.sWSP_FACING_TYPETableAdapter.Update(sANDBOXDataSet);
            this.sWSP_FACING_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_FACING_TYPEDataTable)((SANDBOXDataSet)this.sWSPFACINGTYPEBindingSource.DataSource).SWSP_FACING_TYPE);
            dataGridView5.Refresh();
        }

        private void buttonShapesAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_SHAPE_TYPETableAdapter.Insert("New Shape", "New Shape Description");
            this.sWSP_SHAPE_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_SHAPE_TYPEDataTable)((SANDBOXDataSet)this.sWSPSHAPETYPEBindingSource.DataSource).SWSP_SHAPE_TYPE);
        }

        private void buttonShapesUpdate_Click(object sender, EventArgs e)
        {
            this.sWSPSHAPETYPEBindingSource.EndEdit();
            this.sWSP_SHAPE_TYPETableAdapter.Update(sANDBOXDataSet);
            this.sWSP_SHAPE_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_SHAPE_TYPEDataTable)((SANDBOXDataSet)this.sWSPSHAPETYPEBindingSource.DataSource).SWSP_SHAPE_TYPE);
            dataGridView6.Refresh();
        }

        private void buttonShapesDelete_Click(object sender, EventArgs e)
        {
            this.sWSP_SHAPE_TYPETableAdapter.DeleteQuery((int)((System.Data.DataRowView)sWSPSHAPETYPEBindingSource.Current)["shape_type_id"]);
            this.sWSP_SHAPE_TYPETableAdapter.Update(sANDBOXDataSet);
            this.sWSP_SHAPE_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_SHAPE_TYPEDataTable)((SANDBOXDataSet)this.sWSPSHAPETYPEBindingSource.DataSource).SWSP_SHAPE_TYPE);
            dataGridView6.Refresh();
        }

        private void buttonMaterialsAdd_Click(object sender, EventArgs e)
        {
            this.sWSP_MATERIAL_TYPETableAdapter.Insert("New Material", "New material description");
            this.sWSP_MATERIAL_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_MATERIAL_TYPEDataTable)((SANDBOXDataSet)this.sWSPMATERIALTYPEBindingSource.DataSource).SWSP_MATERIAL_TYPE);
        }

        private void buttonMaterialsUpdate_Click(object sender, EventArgs e)
        {
            this.sWSPMATERIALTYPEBindingSource.EndEdit();
            this.sWSP_MATERIAL_TYPETableAdapter.Update(sANDBOXDataSet);
            this.sWSP_MATERIAL_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_MATERIAL_TYPEDataTable)((SANDBOXDataSet)this.sWSPMATERIALTYPEBindingSource.DataSource).SWSP_MATERIAL_TYPE);
            dataGridView7.Refresh();
        }

        private void buttonMaterialsDelete_Click(object sender, EventArgs e)
        {
            this.sWSP_MATERIAL_TYPETableAdapter.DeleteQuery((int)((System.Data.DataRowView)sWSPMATERIALTYPEBindingSource.Current)["material_type_id"]);
            this.sWSP_MATERIAL_TYPETableAdapter.Update(sANDBOXDataSet);
            this.sWSP_MATERIAL_TYPETableAdapter.Fill((SANDBOXDataSet.SWSP_MATERIAL_TYPEDataTable)((SANDBOXDataSet)this.sWSPMATERIALTYPEBindingSource.DataSource).SWSP_MATERIAL_TYPE);
            dataGridView7.Refresh();
        }


    }
}
