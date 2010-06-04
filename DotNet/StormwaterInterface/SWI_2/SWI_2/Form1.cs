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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonUpdateDatabase_Click(object sender, EventArgs e)
        {
            this.fKSURVEYPAGEVIEWBindingSource.EndEdit();
            this.sWSP_SURVEY_PAGETableAdapter.Update(sANDBOXDataSet);
        }

        private void buttonAddView_Click(object sender, EventArgs e)
        {
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
            child.MapNo = (int)comboBoxView.SelectedValue;
            this.Enabled = false;
            child.ShowDialog();
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            this.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_GLOBAL_ID' table. You can move, or remove it, as needed.
            this.sWSP_GLOBAL_IDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_GLOBAL_ID);
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

            try
            {
                dataGridViewDitches.Rows[0].Selected = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void CheckEvaluatorsAssociatedWithThisSurveyPage(object sender, System.EventArgs e)
        {
            //check the objects in the checkedListBox the evaluator_id is in
            //the Survey_Page_Evaluator Dataset for this SurveyPage
            object item;
            try
            {

                for (int index = 0; index < checkedListBoxEvaluators.Items.Count; index++)
                {
                    item = checkedListBoxEvaluators.Items[index];
                    if (this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.IdentifyValidEvaluators((int)comboBoxSurveyPage.SelectedValue, (int)((System.Data.DataRowView)item).Row[0]) != 0)
                    {
                        checkedListBoxEvaluators.SetItemChecked(index, true);
                    }
                    else
                    {
                        checkedListBoxEvaluators.SetItemChecked(index, false);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void checkedListBoxEvaluators_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Data.DataRowView item;
            //check the index of the selected items.
            //Update Survey_Page_Evaluator according to which ones are selected
            
            //Delete all the associated evaluators with this page
            this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.DeleteQuery((int)comboBoxSurveyPage.SelectedValue);
            //refill the evaulator/page associations
            try
            {
                for (int index = 0; index < checkedListBoxEvaluators.Items.Count; index++)
                {
                    item = (System.Data.DataRowView)checkedListBoxEvaluators.Items[index];
                    if (checkedListBoxEvaluators.CheckedIndices.Contains(index))
                    {
                        this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.InsertQuery((int)comboBoxSurveyPage.SelectedValue, (int)((System.Data.DataRowView)item).Row[0]);
                    }
                }

                this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.Update(sANDBOXDataSet);
                this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.Fill((SANDBOXDataSet.SWSP_SURVEY_PAGE_EVALUATORDataTable)((SANDBOXDataSet)this.sWSPSURVEYPAGEEVALUATORBindingSource.DataSource).SWSP_SURVEY_PAGE_EVALUATOR);
                checkedListBoxEvaluators.Refresh();
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

        private void tabPage1_Entered(object sender, EventArgs e)
        {
            try
            {
                foreach (object rowObject in dataGridViewDitches.Rows)
                {
                    ((DataGridViewRow)rowObject).Selected = false;
                }
                dataGridViewDitches.Rows[0].Selected = true;
                fKDITCHSURVEYPAGEBindingSource.MoveFirst();
                dataGridViewDitches.Refresh();
                this.sWSP_PHOTOTableAdapter.FillByGlobalID((SANDBOXDataSet.SWSP_PHOTODataTable)((SANDBOXDataSet)this.sWSPPHOTOBindingSource.DataSource).SWSP_PHOTO, (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"]);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                this.sWSP_PHOTOTableAdapter.FillByGlobalID((SANDBOXDataSet.SWSP_PHOTODataTable)((SANDBOXDataSet)this.sWSPPHOTOBindingSource.DataSource).SWSP_PHOTO, 0);
            }
        }

        private void tabPageCulverts_Enter(object sender, EventArgs e)
        {
            try
            {
            foreach (object rowObject in dataGridViewCulverts.Rows)
            {
                ((DataGridViewRow)rowObject).Selected = false;
            }
            dataGridViewCulverts.Rows[0].Selected = true;
            fKCULVERTSURVEYPAGEBindingSource.MoveFirst();
            dataGridViewCulverts.Refresh();


            }
            catch (Exception ex)
            {
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            try
            {
            foreach (object rowObject in dataGridViewPipes.Rows)
            {
                ((DataGridViewRow)rowObject).Selected = false;
            }
            dataGridViewPipes.Rows[0].Selected = true;
            fKPIPESURVEYPAGEBindingSource.MoveFirst();
            dataGridViewPipes.Refresh();
}
            catch (Exception ex)
            {
             }
        }

        private void buttonDitchesDelete_Click(object sender, EventArgs e)
        {
            this.sWSP_DITCHTableAdapter.DeleteQuery(((int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["ditch_id"]));
            this.sWSP_DITCHTableAdapter.Update(sANDBOXDataSet);
            this.sWSP_DITCHTableAdapter.Fill((SANDBOXDataSet.SWSP_DITCHDataTable)((SANDBOXDataSet)this.sWSPDITCHBindingSource.DataSource).SWSP_DITCH);
            dataGridViewDitches.Refresh();
        }

        private void buttonDitchesAdd_Click(object sender, EventArgs e)
        {
            int globalID = 0;
            //adding a ditch means:
            //placing a new entry in the globalID table,
            //taking that value and using it to create a new ditch
            this.sWSP_GLOBAL_IDTableAdapter.Insert("");
            //what was the global ID that was just inserted?  The highest value in the GlobalID table.  Since we just inserted to it, there is no chance that it could be null
            globalID = (int)this.sWSP_GLOBAL_IDTableAdapter.ScalarQuery();
            this.sWSP_DITCHTableAdapter.Insert(globalID,
                                                ((int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"]),
                                                "NewNode",
                                                1,
                                                1,
                                                1,
                                                1,
                                                1,
                                                "");
            this.sWSP_DITCHTableAdapter.Fill((SANDBOXDataSet.SWSP_DITCHDataTable)((SANDBOXDataSet)this.sWSPDITCHBindingSource.DataSource).SWSP_DITCH);

            fKDITCHSURVEYPAGEBindingSource.MoveLast();
            dataGridViewDitches.Refresh();
        }

        private void buttonPipesViewAddPhotos_Click(object sender, EventArgs e)
        {
            FormPhotos child = new FormPhotos();

            child.GlobalID = (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"];
            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
        }

        private void buttonCulvertsViewAddPhotos_Click(object sender, EventArgs e)
        {
            FormPhotos child = new FormPhotos();

            child.GlobalID = (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"];
            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
        }

        private void buttonDitchesViewAddPhotos_Click(object sender, EventArgs e)
        {
            FormPhotos child = new FormPhotos();

            child.GlobalID = (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"];
            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
        }

        private void buttonUpdateDitch_Click(object sender, EventArgs e)
        {
            int currentSelected = (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"];
            this.fKDITCHSURVEYPAGEBindingSource.EndEdit();
            this.sWSP_DITCHTableAdapter.Update(sANDBOXDataSet);
            this.sWSP_DITCHTableAdapter.Fill((SANDBOXDataSet.SWSP_DITCHDataTable)((SANDBOXDataSet)this.sWSPDITCHBindingSource.DataSource).SWSP_DITCH);
            while (currentSelected != (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"])
            {
                fKPIPESURVEYPAGEBindingSource.MoveNext();
            }
            dataGridViewDitches.Refresh();
        }

        private void buttonCulvertsAdd_Click(object sender, EventArgs e)
        {
            int globalID = 0;
            //adding a ditch means:
            //placing a new entry in the globalID table,
            //taking that value and using it to create a new ditch
            this.sWSP_GLOBAL_IDTableAdapter.Insert("");
            //what was the global ID that was just inserted?  The highest value in the GlobalID table.  Since we just inserted to it, there is no chance that it could be null
            globalID = (int)this.sWSP_GLOBAL_IDTableAdapter.ScalarQuery();
            this.sWSP_CULVERTTableAdapter.Insert(globalID,
                                                ((int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"]),
                                                "NewNode",
                                                1,
                                                1,
                                                1,
                                                1,
                                                1,
                                                1,
                                                "");
            this.sWSP_CULVERTTableAdapter.Fill((SANDBOXDataSet.SWSP_CULVERTDataTable)((SANDBOXDataSet)this.sWSPCULVERTBindingSource.DataSource).SWSP_CULVERT);

            fKCULVERTSURVEYPAGEBindingSource1.MoveLast();
            dataGridViewCulverts.Refresh();
        }

        private void buttonCulvertsDelete_Click(object sender, EventArgs e)
        {
            this.sWSP_CULVERTTableAdapter.DeleteQuery(((int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["culvert_id"]));
            this.sWSP_CULVERTTableAdapter.Update(sANDBOXDataSet);
            this.sWSP_CULVERTTableAdapter.Fill((SANDBOXDataSet.SWSP_CULVERTDataTable)((SANDBOXDataSet)this.sWSPCULVERTBindingSource.DataSource).SWSP_CULVERT);
            dataGridViewCulverts.Refresh();
        }

        private void buttonUpdateCulvert_Click(object sender, EventArgs e)
        {
            int currentSelected = (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"];
            this.fKCULVERTSURVEYPAGEBindingSource1.EndEdit();
            this.sWSP_CULVERTTableAdapter.Update(sANDBOXDataSet);
            this.sWSP_CULVERTTableAdapter.Fill((SANDBOXDataSet.SWSP_CULVERTDataTable)((SANDBOXDataSet)this.sWSPCULVERTBindingSource.DataSource).SWSP_CULVERT);
            while (currentSelected != (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"])
            {
                fKCULVERTSURVEYPAGEBindingSource1.MoveNext();
            }
            dataGridViewCulverts.Refresh();
        }

        private void buttonPipesAdd_Click(object sender, EventArgs e)
        {
            int globalID = 0;
            //adding a pipe means:
            //placing a new entry in the globalID table,
            //taking that value and using it to create a new pipe
            this.sWSP_GLOBAL_IDTableAdapter.Insert("");
            //what was the global ID that was just inserted?  The highest value in the GlobalID table.  Since we just inserted to it, there is no chance that it could be null
            globalID = (int)this.sWSP_GLOBAL_IDTableAdapter.ScalarQuery();
            this.sWSP_PIPETableAdapter.Insert(globalID,
                                                ((int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"]),
                                                "NewNode1",
                                                "NewNode2",
                                                1,
                                                1,
                                                1,
                                                1,
                                                1,
                                                "");
            this.sWSP_PIPETableAdapter.Fill((SANDBOXDataSet.SWSP_PIPEDataTable)((SANDBOXDataSet)this.sWSPPIPEBindingSource.DataSource).SWSP_PIPE);

            fKPIPESURVEYPAGEBindingSource.MoveLast();
            dataGridViewPipes.Refresh();
        }

        private void buttonPipesDelete_Click(object sender, EventArgs e)
        {
            this.sWSP_PIPETableAdapter.DeleteQuery(((int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["pipe_id"]));
            this.sWSP_PIPETableAdapter.Update(sANDBOXDataSet);
            this.sWSP_PIPETableAdapter.Fill((SANDBOXDataSet.SWSP_PIPEDataTable)((SANDBOXDataSet)this.sWSPPIPEBindingSource.DataSource).SWSP_PIPE);
            dataGridViewPipes.Refresh();
        }

        private void buttonUpdatePipe_Click(object sender, EventArgs e)
        {
            int currentSelected = (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"];
            this.fKPIPESURVEYPAGEBindingSource.EndEdit();
            this.sWSP_PIPETableAdapter.Update(sANDBOXDataSet);
            this.sWSP_PIPETableAdapter.Fill((SANDBOXDataSet.SWSP_PIPEDataTable)((SANDBOXDataSet)this.sWSPPIPEBindingSource.DataSource).SWSP_PIPE);
            while (currentSelected != (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"])
            {
                fKPIPESURVEYPAGEBindingSource.MoveNext();
            }
            dataGridViewPipes.Refresh();
        }

        private void dataAdministratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSWSPFieldDataAdministration child = new FormSWSPFieldDataAdministration();

            //child.GlobalID = (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"];
            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
        }
    }
}
