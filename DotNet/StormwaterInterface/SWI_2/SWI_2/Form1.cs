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
        private int _CurrentSurveyPage;
        private int _CurrentView;
        private int _lastGlobalID;
        private enum _lastSearchVar { ditch, culvert, pipe };
        private _lastSearchVar _lastSearch; 

        public FormMain()
        {
            InitializeComponent();
        }

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

        private void buttonUpdateDatabase_Click(object sender, EventArgs e)
        {
            this.fKSURVEYPAGEVIEWBindingSource.EndEdit();
            this.sWSP_SURVEY_PAGETableAdapter.Update(sANDBOXDataSet);
        }

        private void buttonAddView_Click(object sender, EventArgs e)
        {
            CurrentView = (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"];
            CurrentSurveyPage = (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["page_number"];

            FormAddView child = new FormAddView();
            child.MyParentForm = this;
            this.Enabled = false;
            child.ShowDialog();
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_VIEW' table. You can move, or remove it, as needed.
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            this.Enabled = true;
            while (this.CurrentView       != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"])
            {
                fKVIEWSUBWATERSHEDBindingSource.MoveNext();
            }
            while (this.CurrentSurveyPage != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["page_number"])
            {
                fKSURVEYPAGEVIEWBindingSource.MoveNext();
            }
        }

        private void buttonAddSurveyPage_Click(object sender, EventArgs e)
        {
            CurrentView = (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"];
            CurrentSurveyPage = (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["page_number"];

            FormAddSurvey child = new FormAddSurvey();
            child.MapNo = (int)((System.Data.DataRowView)comboBoxView.SelectedItem)["view_number"];
            child.SurveyPage = (int)((System.Data.DataRowView)comboBoxSurveyPage.SelectedItem)["page_number"];
            child.MyParentForm = this;
            this.Enabled = false;
            child.ShowDialog();
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            this.Enabled = true;
            while (this.CurrentView != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"])
            {
                fKVIEWSUBWATERSHEDBindingSource.MoveNext();
            }
            while (this.CurrentSurveyPage != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["page_number"])
            {
                fKSURVEYPAGEVIEWBindingSource.MoveNext();
            }
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

            _lastGlobalID = 0;
            _lastSearch = _lastSearchVar.culvert; 

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

        private void tabPageDitches_Entered(object sender, EventArgs e)
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
            fKCULVERTSURVEYPAGEBindingSource1.MoveFirst();
            dataGridViewCulverts.Refresh();
            }
            catch (Exception ex)
            {
            }
        }

        private void tabPagePipes_Enter(object sender, EventArgs e)
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
                                                "",
                                                3,
                                                null,
                                                null,
                                                null,
                                                10,
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
                fKDITCHSURVEYPAGEBindingSource.MoveNext();
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
                                                "",
                                                3,
                                                4,
                                                4,
                                                null,
                                                null,
                                                10,
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
                                                "",
                                                "",
                                                null,
                                                null,
                                                null,
                                                10,
                                                4,
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

        private void buttonFindNode_Click(object sender, EventArgs e)
        {
            //if the currently selected record does not match the search node string, then there is no effective last search
            //if the currently selected record does match the search node string, then record the globalID and the table that
            //we are in.

            //if there is no effective last search, then that means that there is a possibility of no match.
            //In this case, just search through all the tables in the order of ditch, culvert, pipe.
            //if there is no match at all, then just output a message box saying there is no match.
            int lastGlobalIDSaver = _lastGlobalID;
            _lastSearchVar lastSearchSaver = _lastSearch;
            string currentSelected = "";
            string currentSelected1 = "";
            string currentSelected2 = "";

            //first check to see if there is an effective last search:
            //the tab being viewed will determine what selected record and table needs to be looked at:
            if (tabControlDitchesCulvertsPipes.SelectedTab == tabControlDitchesCulvertsPipes.TabPages["tabpageDitches"])
            {
                //Get the selected row from the ditches table.  Don't worry if the user has selected more than one row,
                //because that situation would mean that at least something was messed with since the last search.
                try
                {
                    currentSelected = ((string)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["node"]).Trim();
                    _lastGlobalID = (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"];
                }
                catch (Exception ex)
                {
                    //there was no currently selected datarow object.
                }

                if (string.Compare(currentSelected, textBoxFindNode.Text) == 0)
                {
                    //we have already searched for this node and found a match.  This means we must
                    //try to find a new match, or at least return to the current match
                    //first, search the rest of the ditches table
                    //SELECT TOP 1 FROM DITCH  WHERE global_id > _lastGlobalID AND node like textboxFindNode.text ORDER BY global_id
                    if((this.sWSP_DITCHTableAdapter.FindNextGlobalID(_lastGlobalID, currentSelected)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageDitches");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_DITCHTableAdapter.FindNextGlobalID(_lastGlobalID, currentSelected);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKDITCHSURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID!= (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKDITCHSURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                        
                    }
                    //second search the culverts table
                    else if((this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(currentSelected)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageCulverts");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(currentSelected);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKCULVERTSURVEYPAGEBindingSource1.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"])
                        {
                            fKCULVERTSURVEYPAGEBindingSource1.MoveNext();
                        }
                        this.Refresh();
                    }

                    //third search the pipes table
                    else if((this.sWSP_PIPETableAdapter.FindFirstGlobalID(currentSelected)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPagePipes");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_PIPETableAdapter.FindFirstGlobalID(currentSelected);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKPIPESURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKPIPESURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                    }

                    //fourth search all of the ditches table (if there is only one match we will end up right back where we started).
                    else if ((this.sWSP_DITCHTableAdapter.FindFirstGlobalID(currentSelected)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageDitches");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_DITCHTableAdapter.FindFirstGlobalID(currentSelected);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKDITCHSURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKDITCHSURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                    }
                }
                else
                {
                    //since the strings didn't match, we can't assume that there will ever be a match, so just search all the tables starting with the lowest global_id in ditches
                    //stop after we have looked at them all, and just return nothing (MessageBox.Show("No matches found");)
                    if ((this.sWSP_DITCHTableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageDitches");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_DITCHTableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKDITCHSURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKDITCHSURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                    }
                    //second search the culverts table
                    else if ((this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageCulverts");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKCULVERTSURVEYPAGEBindingSource1.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"])
                        {
                            fKCULVERTSURVEYPAGEBindingSource1.MoveNext();
                        }
                        this.Refresh();
                    }
                    //third search the pipes table
                    else if ((this.sWSP_PIPETableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPagePipes");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_PIPETableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKPIPESURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKPIPESURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No match found");
                    }
                }
            }
            else if (tabControlDitchesCulvertsPipes.SelectedTab == tabControlDitchesCulvertsPipes.TabPages["tabpageCulverts"])
            {
                //Get the selected row from the ditches table.  Don't worry if the user has selected more than one row,
                //because that situation would mean that at least something was messed with since the last search.
                try
                {
                    currentSelected = ((string)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["node"]).Trim();
                    _lastGlobalID = (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"];
                }
                catch (Exception ex)
                {
                    //there was no currently selected datarow object.
                }

                if (string.Compare(currentSelected, textBoxFindNode.Text) == 0)
                {
                    //we have already searched for this node and found a match.  This means we must
                    //try to find a new match, or at least return to the current match
                    //first, search the rest of the ditches table
                    //SELECT TOP 1 FROM DITCH  WHERE global_id > _lastGlobalID AND node like textboxFindNode.text ORDER BY global_id
                    if ((this.sWSP_CULVERTTableAdapter.FindNextGlobalID(_lastGlobalID, currentSelected)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageCulverts");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_CULVERTTableAdapter.FindNextGlobalID(_lastGlobalID, currentSelected);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKCULVERTSURVEYPAGEBindingSource1.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"])
                        {
                            fKCULVERTSURVEYPAGEBindingSource1.MoveNext();
                        }
                        this.Refresh();

                    }

                    //second search the pipes table
                    else if ((this.sWSP_PIPETableAdapter.FindFirstGlobalID(currentSelected)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPagePipes");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_PIPETableAdapter.FindFirstGlobalID(currentSelected);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKPIPESURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKPIPESURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                    }

                    //third search all of the ditches table (if there is only one match we will end up right back where we started).
                    else if ((this.sWSP_DITCHTableAdapter.FindFirstGlobalID(currentSelected)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageDitches");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_DITCHTableAdapter.FindFirstGlobalID(currentSelected);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKDITCHSURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKDITCHSURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();

                    }
                    //fourth search the culverts table
                    else if ((this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(currentSelected)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageCulverts");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(currentSelected);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKCULVERTSURVEYPAGEBindingSource1.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"])
                        {
                            fKCULVERTSURVEYPAGEBindingSource1.MoveNext();
                        }
                        this.Refresh();
                    }
                }
                else
                {
                    //since the strings didn't match, we can't assume that there will ever be a match, so just search all the tables starting with the lowest global_id in ditches
                    //stop after we have looked at them all, and just return nothing (MessageBox.Show("No matches found");)
                    if ((this.sWSP_DITCHTableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageDitches");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_DITCHTableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKDITCHSURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKDITCHSURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                    }
                    //second search the culverts table
                    else if ((this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageCulverts");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKCULVERTSURVEYPAGEBindingSource1.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"])
                        {
                            fKCULVERTSURVEYPAGEBindingSource1.MoveNext();
                        }
                        this.Refresh();
                    }
                    //third search the pipes table
                    else if ((this.sWSP_PIPETableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPagePipes");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_PIPETableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKPIPESURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKPIPESURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No match found");
                    }
                }
            }
            else if (tabControlDitchesCulvertsPipes.SelectedTab == tabControlDitchesCulvertsPipes.TabPages["tabpagePipes"])
            {
                //Get the selected row from the ditches table.  Don't worry if the user has selected more than one row,
                //because that situation would mean that at least something was messed with since the last search.
                try
                {
                    currentSelected1 = ((string)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["us_node"]).Trim();
                    currentSelected2 = ((string)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["ds_node"]).Trim();
                    _lastGlobalID = (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"];
                }
                catch (Exception ex)
                {
                    //there was no currently selected datarow object.
                }

                if (string.Compare(currentSelected1, textBoxFindNode.Text) == 0 || string.Compare(currentSelected2, textBoxFindNode.Text) == 0)
                {
                    //we have already searched for this node and found a match.  This means we must
                    //try to find a new match, or at least return to the current match
                    //first, search the rest of the ditches table
                    //SELECT TOP 1 FROM DITCH  WHERE global_id > _lastGlobalID AND node like textboxFindNode.text ORDER BY global_id
                    if ((this.sWSP_PIPETableAdapter.FindNextGlobalID(_lastGlobalID, textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPagePipes");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_PIPETableAdapter.FindNextGlobalID(_lastGlobalID, textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKPIPESURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKPIPESURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();

                    }
                    //second search all of the ditches table (if there is only one match we will end up right back where we started).
                    else if ((this.sWSP_DITCHTableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageDitches");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_DITCHTableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKDITCHSURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKDITCHSURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();

                    }
                    //third search the culverts table
                    else if ((this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageCulverts");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKCULVERTSURVEYPAGEBindingSource1.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"])
                        {
                            fKCULVERTSURVEYPAGEBindingSource1.MoveNext();
                        }
                        this.Refresh();
                    }

                    //fourth search the pipes table
                    else if ((this.sWSP_PIPETableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPagePipes");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_PIPETableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKPIPESURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKPIPESURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                    }

                    
                }
                else
                {
                    //since the strings didn't match, we can't assume that there will ever be a match, so just search all the tables starting with the lowest global_id in ditches
                    //stop after we have looked at them all, and just return nothing (MessageBox.Show("No matches found");)
                    if ((this.sWSP_DITCHTableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageDitches");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_DITCHTableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKDITCHSURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKDITCHSURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                    }
                    //second search the culverts table
                    else if ((this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPageCulverts");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_CULVERTTableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByCulvertID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKCULVERTSURVEYPAGEBindingSource1.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKCULVERTSURVEYPAGEBindingSource1.Current)["global_id"])
                        {
                            fKCULVERTSURVEYPAGEBindingSource1.MoveNext();
                        }
                        this.Refresh();
                    }
                    //third search the pipes table
                    else if ((this.sWSP_PIPETableAdapter.FindFirstGlobalID(textBoxFindNode.Text)).HasValue)
                    {
                        tabControlDitchesCulvertsPipes.SelectTab("tabPagePipes");
                        //we have found a match and we don't need to look anywhere else
                        _lastGlobalID = (int)this.sWSP_PIPETableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while ((int)(this.relationalIDsTableAdapter.GetDataByPipeID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                        this.Refresh();
                        fKPIPESURVEYPAGEBindingSource.MoveFirst();
                        while (_lastGlobalID != (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"])
                        {
                            fKPIPESURVEYPAGEBindingSource.MoveNext();
                        }
                        this.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No match found");
                    }
                }
            }
        }

        private void textBoxDitchesNode_TextChanged(object sender, EventArgs e)
        {
            if (textBoxDitchesNode.Text.Length > 10)
            {
                textBoxDitchesNode.Text = textBoxDitchesNode.Text.Substring(0, 10);
            }
            textBoxDitchesNode.Text = textBoxDitchesNode.Text.Trim();
        }

        private void textBoxDitchesNode_Enter(object sender, EventArgs e)
        {
            textBoxDitchesNode.Text = textBoxDitchesNode.Text.Trim();
        }

        private void textBoxCulvertsNode_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCulvertsNode.Text.Length > 10)
            {
                textBoxCulvertsNode.Text = textBoxCulvertsNode.Text.Substring(0, 10);
            }
            textBoxCulvertsNode.Text = textBoxCulvertsNode.Text.Trim();
        }

        private void textBoxCulvertsNode_Enter(object sender, EventArgs e)
        {
            textBoxCulvertsNode.Text = textBoxCulvertsNode.Text.Trim();
        }

        private void textBoxPipesDSNode_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPipesDSNode.Text.Length > 10)
            {
                textBoxPipesDSNode.Text = textBoxPipesDSNode.Text.Substring(0, 10);
            }
            textBoxPipesDSNode.Text = textBoxPipesDSNode.Text.Trim();
        }

        private void textBoxPipesUSNode_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPipesUSNode.Text.Length > 10)
            {
                textBoxPipesUSNode.Text = textBoxPipesUSNode.Text.Substring(0, 10);
            }
            textBoxPipesUSNode.Text = textBoxPipesUSNode.Text.Trim();
        }

        private void textBoxPipesDSNode_Enter(object sender, EventArgs e)
        {
            textBoxPipesDSNode.Text = textBoxPipesDSNode.Text.Trim();
        }

        private void textBoxPipesUSNode_Enter(object sender, EventArgs e)
        {
            textBoxPipesUSNode.Text = textBoxPipesUSNode.Text.Trim();
        }

        private void comboBoxCulvertsShape_SelectedIndexChanged(object sender, EventArgs e)
        {}

        private void comboBoxCulvertsMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxDitchesMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void tabPageCulverts_Click(object sender, EventArgs e)
        {
        }

        private void tabPageDitches_Click(object sender, EventArgs e)
        {
        }

        private void tabControlDitchesCulvertsPipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int lastGlobalIDSaver = _lastGlobalID;
            _lastSearchVar lastSearchSaver = _lastSearch;
            string currentSelected = "";
            string currentSelected1 = "";
            string currentSelected2 = "";

            if (tabControlDitchesCulvertsPipes.SelectedTab == tabControlDitchesCulvertsPipes.TabPages["tabpageDitches"])
            {
                //Get the selected row from the ditches table.  Don't worry if the user has selected more than one row,
                //because that situation would mean that at least something was messed with since the last search.
                try
                {
                    currentSelected = ((string)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["node"]).Trim();
                    _lastGlobalID = (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"];

                    fKDITCHSURVEYPAGEBindingSource.MoveFirst();
                    while (_lastGlobalID != (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"])
                    {
                        fKDITCHSURVEYPAGEBindingSource.MoveNext();
                    }
                    this.Refresh();

                    
                }
                catch (Exception ex)
                {
                    //there was no currently selected datarow object.
                }

            }
        }
    }
}
