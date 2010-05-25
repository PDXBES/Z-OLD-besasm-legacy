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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_PIPE' table. You can move, or remove it, as needed.
            this.sWSP_PIPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_PIPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SHAPE_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_SHAPE_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SHAPE_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_CULVERT' table. You can move, or remove it, as needed.
            this.sWSP_CULVERTTableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_PHOTO' table. You can move, or remove it, as needed.
            this.sWSP_PHOTOTableAdapter.Fill(this.sANDBOXDataSet.SWSP_PHOTO);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_MATERIAL_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_MATERIAL_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_MATERIAL_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_FACING_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_FACING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_FACING_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_DITCH' table. You can move, or remove it, as needed.
            this.sWSP_DITCHTableAdapter.Fill(this.sANDBOXDataSet.SWSP_DITCH);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE_EVALUATOR' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE_EVALUATOR);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_EVALUATOR' table. You can move, or remove it, as needed.
            this.sWSP_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_EVALUATOR);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_VIEW' table. You can move, or remove it, as needed.
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SUBWATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_WATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);

        }

        /*private void CheckEveryOther_Click(object sender, System.EventArgs e)
        {
            // Cycle through every item and check every other.

            // Set flag to true to know when this code is being executed. Used in the ItemCheck
            // event handler.
            insideCheckEveryOther = true;

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                // For every other item in the list, set as checked.
                if ((i % 2) == 0)
                {
                    // But for each other item that is to be checked, set as being in an
                    // indeterminate checked state.
                    if ((i % 4) == 0)
                        checkedListBox1.SetItemCheckState(i, CheckState.Indeterminate);
                    else
                        checkedListBox1.SetItemChecked(i, true);
                }
            }

            insideCheckEveryOther = false;
        }*/

        private void CheckEvaluatorsAssociatedWithThisSurveyPage(object sender, System.EventArgs e)
        {
            //check the objects in the checkedListBox the evaluator_id is in
            //the Survey_Page_Evaluator Dataset for this SurveyPage
            object item;
            try
            {

                for (int index = 0; index < checkedListBox1.Items.Count; index++)
                {
                    item = checkedListBox1.Items[index];
                    if (this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.IdentifyValidEvaluators((int)comboBoxSurveyPage.SelectedValue, (int)((System.Data.DataRowView)item).Row[0]) != 0)
                    {
                        checkedListBox1.SetItemChecked(index, true);
                    }
                    else
                    {
                        checkedListBox1.SetItemChecked(index, false);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataRowView item;
            //check the index of the selected items.
            //Update Survey_Page_Evaluator according to which ones are selected
            
            //Delete all the associated evaluators with this page
            this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.DeleteQuery((int)comboBoxSurveyPage.SelectedValue);
            //refill the evaulator/page associations
            try
            {
                for (int index = 0; index < checkedListBox1.Items.Count; index++)
                {
                    item = (System.Data.DataRowView)checkedListBox1.Items[index];
                    if (checkedListBox1.CheckedIndices.Contains(index))
                    {
                        this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.InsertQuery((int)comboBoxSurveyPage.SelectedValue, (int)((System.Data.DataRowView)item).Row[0]);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void checkFacingDirectionAssociatedWithThisDitch(object sender, System.EventArgs e)
        {
            //MessageBox.Show(((int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["facing"]).ToString());
            try
            {
                
                //comboBoxDitchesFacingDirection.SelectedValue = (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["facing"];
                
                //comboBoxDitchesFacingDirection.SelectedIndex = comboBoxDitchesFacingDirection.Items.IndexOf(((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["facing"]); 
            }
            catch (Exception ex)
            {
            }
        }

        private void comboBoxDitchesFacingDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*System.Data.DataRowView item;
            //refill the evaulator/page associations
            MessageBox.Show(((int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["facing"]).ToString());
            try
            {
                comboBoxDitchesFacingDirection.SelectedValue = (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["facing"];
            }
            catch (Exception ex)
            {
            }*/
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            this.sWSP_PHOTOTableAdapter.GetDataByGlobalID((int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"]);
        }
    }
}
