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
    public partial class FormFieldSurveyView : Form
    {
        public HashSet<int> deletedGlobalID;

        public FormFieldSurveyView()
        {
            InitializeComponent();
        }

        private void FormFieldSurveyView_Load(object sender, EventArgs e)
        {
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
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_MESH1' table. You can move, or remove it, as needed.
            this.sWSP_MESH1TableAdapter.Fill(this.sANDBOXDataSet.SWSP_MESH1);
            deletedGlobalID = new HashSet<int>();
            //fill the FieldSurveypageview datatable with the data from the MESH1 datatable
            foreach(DataRow r in this.sANDBOXDataSet.SWSP_MESH1)
            {
                sANDBOXDataSet.DataTableFieldSurvey.LoadDataRow(r.ItemArray, false);
                sANDBOXDataSet.DataTableFieldSurvey.EndLoadData();
                sANDBOXDataSet.DataTableFieldSurvey.AcceptChanges();
            }

            //every time the datagridview is refreshed, the default values have to be changed
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["watershed"].DefaultValue = ultraComboWatershed.Text;
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["subwatershed"].DefaultValue = ultraComboSubwatershed.Text;
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["view_number"].DefaultValue = int.Parse(ultraComboMapNo.Text);
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["page_number"].DefaultValue = int.Parse(comboBoxSurveyPage.Text);

            PopulateLinkInfo();
        }

        //this function should be called whenever you want to refresh the information in dataGridViewLinkInfo.
        //The actual data in dataGridViewLinkInfo should no longer be associated with the table in the database.
        //The data should be queried from the database, then inserted into the table and given no association with the database.
        //When the user adds more rows to the tables, they are really just adding more rows to the internal datatable.
        //If there have been changes to the internal datatable when it is time to close out, prompt the user to save,
        //and then check the internal table against the saved tables.  This will more closely match the user's experience
        //with save files.

        //upon loading this version of the interface, just dump all the information from the 
        //MESH1 datatable into the fieldwork datatable. 
        private void PopulateLinkInfo()
        {
            try
            {
                //every time the datagridview is refreshed, the default values have to be changed
                sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["watershed"].DefaultValue = ultraComboWatershed.Text;
                sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["subwatershed"].DefaultValue = ultraComboSubwatershed.Text;
                sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["view_number"].DefaultValue = int.Parse(ultraComboMapNo.Text);
                sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["page_number"].DefaultValue = int.Parse(comboBoxSurveyPage.Text);

                //1:All changes to DataTableFieldSurveyEditable should be committed to DataTableFieldSurvey
                //  in a different procedure, if a user deleted a row, it would have already been taken out of
                //  this table and placed back into DataTableFieldSurvey with an action flag of 1(delete)
                //  other action flags: (2)add (3)modify
                //2:once the changes in the editable version have been committed, then empty the editable
                //  version and reload
                //3:Remember that the page number, view number, watershed and subwatershed need to be
                //  placed in the row as well
                foreach (DataRow r in this.sANDBOXDataSet.DataTableFieldSurveyEditable)
                {
                    if (r.RowState == DataRowState.Added)
                    {
                        r["action"] = 2;
                        sANDBOXDataSet.DataTableFieldSurvey.LoadDataRow(r.ItemArray, false);
                        sANDBOXDataSet.DataTableFieldSurvey.EndLoadData();
                        sANDBOXDataSet.DataTableFieldSurvey.AcceptChanges();
                    }

                    if (r.RowState == DataRowState.Modified)
                    {
                        r["action"] = 3;
                        sANDBOXDataSet.DataTableFieldSurvey.LoadDataRow(r.ItemArray, false);
                        sANDBOXDataSet.DataTableFieldSurvey.EndLoadData();
                        sANDBOXDataSet.DataTableFieldSurvey.AcceptChanges();
                    }
                    //this is not the place for this logic.  This should be moved to someplace
                    //where it wont occur n times per rowAlter
                    if (r.RowState == DataRowState.Deleted)
                    {
                        foreach (int globalID in deletedGlobalID)
                        {
                            foreach (DataRow DR in sANDBOXDataSet.DataTableFieldSurvey)
                            {
                                if (globalID == (int)DR["global_id"])
                                {
                                    DR["action"] = 1;
                                    sANDBOXDataSet.DataTableFieldSurvey.AcceptChanges();
                                }
                            }
                        }
                    }
                }
                sANDBOXDataSet.DataTableFieldSurveyEditable.Clear();
                sANDBOXDataSet.DataTableFieldSurveyEditable.AcceptChanges();
                foreach (DataRow r in this.sANDBOXDataSet.DataTableFieldSurvey)
                {
                    if ((int)r.ItemArray[15] == Int32.Parse(ultraComboMapNo.Text) &&
                       (int)r.ItemArray[16] == Int32.Parse(comboBoxSurveyPage.Text) &&
                       (string)r.ItemArray[17] == ultraComboWatershed.Text &&
                       (string)r.ItemArray[18] == ultraComboSubwatershed.Text)
                    {
                        sANDBOXDataSet.DataTableFieldSurveyEditable.LoadDataRow(r.ItemArray, false);
                        sANDBOXDataSet.DataTableFieldSurveyEditable.EndLoadData();
                        sANDBOXDataSet.DataTableFieldSurveyEditable.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        void dataGridViewLinkInfo_RowsRemoved(object sender, System.Windows.Forms.DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                foreach (DataRow r in this.sANDBOXDataSet.DataTableFieldSurveyEditable)
                {
                    if (r.RowState == DataRowState.Deleted)
                    {
                        deletedGlobalID.Add((int)r["global_id", DataRowVersion.Original]);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CheckEvaluatorsAssociatedWithThisSurveyPage()
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

        private void SurveyPageValueChanged(object sender, System.EventArgs e)
        {
            CheckEvaluatorsAssociatedWithThisSurveyPage();
            PopulateLinkInfo();
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
    }
}
