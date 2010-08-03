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
        int tempGlobal_id;

        public FormFieldSurveyView()
        {
            InitializeComponent();
        }




        private void FormFieldSurveyView_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_MATERIAL_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_MATERIAL_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_MATERIAL_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SHAPE_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_SHAPE_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SHAPE_TYPE);
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
            tempGlobal_id = 0;
            //fill the FieldSurveypageview datatable with the data from the MESH1 datatable
            foreach(DataRow r in this.sANDBOXDataSet.SWSP_MESH1)
            {
                sANDBOXDataSet.DataTableFieldSurvey.LoadDataRow(r.ItemArray, false);
                sANDBOXDataSet.DataTableFieldSurvey.EndLoadData();
                sANDBOXDataSet.DataTableFieldSurvey.AcceptChanges();
            }

            //every time the datagridview is refreshed, the default values have to be changed
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["watershed"].DefaultValue = comboBoxWatershed.Text;
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["subwatershed"].DefaultValue = comboBoxSubwatershed.Text;
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["view_number"].DefaultValue = int.Parse(comboBoxMapNo.Text);
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["page_number"].DefaultValue = int.Parse(comboBoxSurveyPage.Text);
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["us_node"].DefaultValue = " ";
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["ds_node"].DefaultValue = " ";
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["node"].DefaultValue = " ";
            sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["linktype"].DefaultValue = "Pipe";

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
                //This event will generally follow a page update, but that change will not yet have hit the tables, so
                //the page should be retrieved from the textbox.  Oddly enough, the tables will generally be the ONLY
                //place you can find the correct subwatershed, so get the subwatershed from the tables.
                sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["watershed"].DefaultValue = (string)((DataRowView)sWSPWATERSHEDBindingSource.Current)[1];
                sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["subwatershed"].DefaultValue = (string)((DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)[2];
                sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["view_number"].DefaultValue = (int)((DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)[2];
                sANDBOXDataSet.DataTableFieldSurveyEditable.Columns["page_number"].DefaultValue = Int32.Parse(comboBoxSurveyPage.Text);

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
                        tempGlobal_id--;
                        r["action"] = 2;
                        r["global_id"] = tempGlobal_id;
                        sANDBOXDataSet.DataTableFieldSurvey.LoadDataRow(r.ItemArray, false);
                        sANDBOXDataSet.DataTableFieldSurvey.EndLoadData();
                        sANDBOXDataSet.DataTableFieldSurvey.AcceptChanges();
                    }

                    if (r.RowState == DataRowState.Modified)
                    {
                        r["action"] = 3;
                        sANDBOXDataSet.DataTableFieldSurvey.RemoveDataTableFieldSurveyRow((SANDBOXDataSet.DataTableFieldSurveyRow)(sANDBOXDataSet.DataTableFieldSurvey.Select("global_id = " + r["global_id"].ToString()))[0]);
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
                    if ((int)r.ItemArray[15] == (int)((DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)[2] &&
                        (int)r.ItemArray[16] == Int32.Parse(comboBoxSurveyPage.Text)&&// (int) ((DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)[2] &&
                       (string)r.ItemArray[17] == (string) ((DataRowView)sWSPWATERSHEDBindingSource.Current)[1] &&
                       (string)r.ItemArray[18] == (string)((DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)[2] &&
                        (int)((r.ItemArray[19] is System.DBNull) ? 0 : (int)r.ItemArray[19]) != 1)
                    {
                        sANDBOXDataSet.DataTableFieldSurveyEditable.LoadDataRow(r.ItemArray, false);
                        sANDBOXDataSet.DataTableFieldSurveyEditable.EndLoadData();
                        sANDBOXDataSet.DataTableFieldSurveyEditable.AcceptChanges();
                    }
                }
                dataGridViewLinkInfo.Refresh();

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

        private void comboBoxMapNo_SelectedValueChanged(object sender, System.EventArgs e)
        {
            SurveyPageValueChanged(sender, e);
        }

        private void comboBoxSurveyPage_SelectedValueChanged(object sender, System.EventArgs e)
        {
            SurveyPageValueChanged(sender, e);
        }

        private void comboBoxSurveyPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //SurveyPageValueChanged(sender, e);
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

        private void translateChangesAndSaveToDatabase()
        {
            int watershed_id = 0;
            int subwatershed_id = 0;
            int view_id = 0;
            int page_id = 0;
            int material_id = 0;
            int shape_id = 0;
            int culvert_opening_type = 0;
            //facing will soon be deprecated.  This might need to be removed.
            int facing_id = 3;

            foreach (DataRow r in this.sANDBOXDataSet.DataTableFieldSurvey)
            {
                //get the watershed_id
                watershed_id = (int)((DataRow)sANDBOXDataSet.SWSP_WATERSHED.Select("watershed = '" + (string)r["watershed"] + "'")[0]).ItemArray[0];

                //get the subwatershed_id
                subwatershed_id = (int)((DataRow)sANDBOXDataSet.SWSP_SUBWATERSHED.Select("subwatershed = '" + (string)r["subwatershed"] + "' AND watershed_id = " + watershed_id.ToString())[0]).ItemArray[0];
                //get the view_id
                view_id = (int)((DataRow)sANDBOXDataSet.SWSP_VIEW.Select("view_number = " + ((int)r["view_number"]).ToString() + " AND subwatershed_id = " + subwatershed_id.ToString())[0]).ItemArray[0];
                //get the page_id
                page_id = (int)((DataRow)sANDBOXDataSet.SWSP_SURVEY_PAGE.Select("page_number = " + ((int)r["page_number"]).ToString() + " AND view_id = " + view_id.ToString())[0]).ItemArray[0];
                //get the material_id
                if (r["material"] is System.DBNull) { r["material"] = "UNSPEC"; }
                material_id = (int)((DataRow)sANDBOXDataSet.SWSP_MATERIAL_TYPE.Select("material = '" + (string)r["material"] + "'")[0]).ItemArray[0];
                //get the shape_id
                if (r["shape"] is System.DBNull) { r["shape"] = "UNK"; }
                shape_id = (int)((DataRow)sANDBOXDataSet.SWSP_SHAPE_TYPE.Select("shape = '" + (string)r["shape"] + "'")[0]).ItemArray[0];
                //get the culvert_opening_type_id
                if (r["culvert_opening"] is System.DBNull)
                {
                    culvert_opening_type = (int)((DataRow)sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE.Select("culvert_opening is null")[0]).ItemArray[0];
                }
                else
                {
                    culvert_opening_type = (int)((DataRow)sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE.Select("culvert_opening = '" + (string)r["culvert_opening"] + "'")[0]).ItemArray[0];
                }
                //if the global_id is negative, then we are adding a record
                if ((int)r["global_id"] < 0)
                {
                    
                    //the transfer of this data should be handled simply to the linkinfo datasource
                    //insert based on the value in the link type column
                    if ((string)r["linktype"] == "Pipe")
                    {
                        int globalID = 0;
                        //adding a pipe means:
                        //placing a new entry in the globalID table,
                        //taking that value and using it to create a new pipe
                        this.sWSP_GLOBAL_IDTableAdapter.Insert("");
                        //what was the global ID that was just inserted?  The highest value in the GlobalID table.  Since we just inserted to it, there is no chance that it could be null
                        globalID = (int)this.sWSP_GLOBAL_IDTableAdapter.ScalarQuery();
                        //add this record to the SWSP_PIPE table
                        this.sWSP_PIPETableAdapter.Insert(globalID,
                                                            page_id,
                                                            (string)r["us_node"],
                                                            (string)r["ds_node"],
                                                            (double?)((r["us_depth_in"] is System.DBNull) ? null : (double?)r["us_depth_in"]),
                                                            (double?)((r["ds_depth_in"] is System.DBNull) ? null : (double?)r["ds_depth_in"]),
                                                            (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                            (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                            material_id,
                                                            shape_id,
                                                            "",
                                                            (int?)((r["length_ft"] is System.DBNull) ? null :(int?)r["length_ft"]));
                    }
                    else if ((string)r["linktype"] == "Ditch")
                    {
                        int globalID = 0;
                        //adding a pipe means:
                        //placing a new entry in the globalID table,
                        //taking that value and using it to create a new pipe
                        this.sWSP_GLOBAL_IDTableAdapter.Insert("");
                        //what was the global ID that was just inserted?  The highest value in the GlobalID table.  Since we just inserted to it, there is no chance that it could be null
                        globalID = (int)this.sWSP_GLOBAL_IDTableAdapter.ScalarQuery();
                        //add this record to the SWSP_PIPE table
                        this.sWSP_DITCHTableAdapter.Insert(globalID,
                                                            page_id,
                                                            (string)r["node"],
                                                            facing_id,
                                                            (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                            (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                            (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                            material_id,
                                                            "",
                                                            (string)r["ds_node"],
                                                            (string)r["us_node"],
                                                            (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]));
                    }
                    else if ((string)r["linktype"] == "Culvert")
                    {
                        int globalID = 0;
                        //adding a pipe means:
                        //placing a new entry in the globalID table,
                        //taking that value and using it to create a new pipe
                        this.sWSP_GLOBAL_IDTableAdapter.Insert("");
                        //what was the global ID that was just inserted?  The highest value in the GlobalID table.  Since we just inserted to it, there is no chance that it could be null
                        globalID = (int)this.sWSP_GLOBAL_IDTableAdapter.ScalarQuery();
                        //add this record to the SWSP_PIPE table
                        this.sWSP_CULVERTTableAdapter.Insert(globalID,
                                                            page_id,
                                                            (string)r["node"],
                                                            facing_id,
                                                            culvert_opening_type,
                                                            shape_id,
                                                            (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                            (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                            (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                            material_id,
                                                            "",
                                                            (string)r["ds_node"],
                                                            (string)r["us_node"],
                                                            (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]));
                    }
                }
                else if (r["action"] is System.DBNull)
                {
                    //do nothing
                }
                //if the global id is positive, and the action flag is 3, then we are modifying an existing record
                //and must check to see if the existing global id belongs to a different type of conduit.
                //if the existing global id belongs to a different conduit type, then we need to delete the 
                //previous conduit type and insert the new one.
                else if ((int)r["action"] == 3)
                {
                    //get the material_id
                    if (r["material"] is System.DBNull) { r["material"] = "UNSPEC"; }
                    material_id = (int)((DataRow)sANDBOXDataSet.SWSP_MATERIAL_TYPE.Select("material = '" + (string)r["material"] + "'")[0]).ItemArray[0];
                    //get the shape_id
                    if (r["shape"] is System.DBNull) { r["shape"] = "UNK"; }
                    shape_id = (int)((DataRow)sANDBOXDataSet.SWSP_SHAPE_TYPE.Select("shape = '" + (string)r["shape"] + "'")[0]).ItemArray[0];
                    //get the culvert_opening_type_id
                    if (r["culvert_opening"] is System.DBNull)
                    {
                        culvert_opening_type = (int)((DataRow)sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE.Select("culvert_opening is null")[0]).ItemArray[0];
                    }
                    else
                    {
                        culvert_opening_type = (int)((DataRow)sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE.Select("culvert_opening = '" + (string)r["culvert_opening"] + "'")[0]).ItemArray[0];
                    }

                    if ((string)r["linktype"] == "Pipe")
                    {
                        //check pipe table for this global id
                        //if the global id does not exist in the pipe table, delete from the ditch and culvert table
                        if (this.sWSP_PIPETableAdapter.GetDataByGlobalID((int)r["global_id"]).Count < 1)
                        {
                            this.sWSP_DITCHTableAdapter.DeleteByGlobalID((int)r["global_id"]);
                            this.sWSP_CULVERTTableAdapter.DeleteByGlobalID((int)r["global_id"]);
                            //  then insert to the pipe table
                            this.sWSP_PIPETableAdapter.Insert(
                                                            (int)r["global_id"],
                                                            page_id,
                                                            ((r["us_node"] is System.DBNull) ? "" : (string)r["us_node"]),
                                                            ((r["ds_node"] is System.DBNull) ? "" : (string)r["ds_node"]),
                                                            (double?)((r["us_depth_in"] is System.DBNull) ? null : (double?)r["us_depth_in"]),
                                                            (double?)((r["ds_depth_in"] is System.DBNull) ? null : (double?)r["ds_depth_in"]),
                                                            (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                            (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                            material_id,
                                                            shape_id,
                                                            "",
                                                            (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"])
                                                            );
                        }
                        else{
                            this.sWSP_PIPETableAdapter.UpdateQuery(
                                                            page_id,
                                                            ((r["us_node"] is System.DBNull) ? "" : (string)r["us_node"]),
                                                            ((r["ds_node"] is System.DBNull) ? "" : (string)r["ds_node"]),
                                                            (double?)((r["us_depth_in"] is System.DBNull) ? null : (double?)r["us_depth_in"]),
                                                            (double?)((r["ds_depth_in"] is System.DBNull) ? null : (double?)r["ds_depth_in"]),
                                                            (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                            (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                            material_id,
                                                            shape_id,
                                                            "",
                                                            (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]),
                                                            (int)r["global_id"]);
                        }
                    }
                    else if ((string)r["linktype"] == "Ditch")
                    {
                        //check pipe table for this global id
                        //if the global id does not exist in the pipe table, delete from the ditch and culvert table
                        if (this.sWSP_DITCHTableAdapter.GetDataByGlobalID((int)r["global_id"]).Count < 1)
                        {
                            this.sWSP_PIPETableAdapter.DeleteByGlobalID((int)r["global_id"]);
                            this.sWSP_CULVERTTableAdapter.DeleteByGlobalID((int)r["global_id"]);
                            //  then insert to the pipe table
                            this.sWSP_DITCHTableAdapter.Insert(
                                                                (int)r["global_id"],
                                                                page_id,
                                                                ((r["node"] is System.DBNull) ? "" : (string)r["node"]),
                                                                facing_id,
                                                                (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                                (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                                (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                                material_id,
                                                                "",
                                                                ((r["ds_node"] is System.DBNull) ? "" : (string)r["ds_node"]),
                                                                ((r["us_node"] is System.DBNull) ? "" : (string)r["us_node"]),
                                                                (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"])
                                                                );
                        }
                        else
                        {
                            this.sWSP_DITCHTableAdapter.UpdateQuery(
                                                                page_id,
                                                                ((r["node"] is System.DBNull) ? "" : (string)r["node"]),
                                                                facing_id,
                                                                (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                                (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                                (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                                material_id,
                                                                "",
                                                                ((r["ds_node"] is System.DBNull) ? "" : (string)r["ds_node"]),
                                                                ((r["us_node"] is System.DBNull) ? "" : (string)r["us_node"]),
                                                                (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]),
                                                                (int)r["global_id"]);
                        }
                    }
                    else if ((string)r["linktype"] == "Culvert")
                    {
                        //check pipe table for this global id
                        //if the global id does not exist in the pipe table, delete from the ditch and culvert table
                        if (this.sWSP_CULVERTTableAdapter.GetDataByGlobalID((int)r["global_id"]).Count < 1)
                        {
                            this.sWSP_DITCHTableAdapter.DeleteByGlobalID((int)r["global_id"]);
                            this.sWSP_PIPETableAdapter.DeleteByGlobalID((int)r["global_id"]);
                            //  then insert to the pipe table
                            this.sWSP_CULVERTTableAdapter.Insert(
                                                                (int)r["global_id"],
                                                                page_id,
                                                                ((r["node"] is System.DBNull) ? "" : (string)r["node"]),
                                                                facing_id,
                                                                culvert_opening_type,
                                                                shape_id,
                                                                (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                                (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                                (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                                material_id,
                                                                "",
                                                                ((r["ds_node"] is System.DBNull) ? "" : (string)r["ds_node"]),
                                                                ((r["us_node"] is System.DBNull) ? "" : (string)r["us_node"]),
                                                                (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"])
                                                                );
                        }
                        else
                        {
                            this.sWSP_CULVERTTableAdapter.UpdateQuery(
                                                                page_id,
                                                                ((r["node"] is System.DBNull) ? "" : (string)r["node"]),
                                                                facing_id,
                                                                culvert_opening_type,
                                                                shape_id,
                                                                (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                                (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                                (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                                material_id,
                                                                "",
                                                                ((r["ds_node"] is System.DBNull) ? "" : (string)r["ds_node"]),
                                                                ((r["us_node"] is System.DBNull) ? "" : (string)r["us_node"]),
                                                                (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]),
                                                                (int)r["global_id"]);
                        }
                    }
                }
                else if ((int)r["action"] == 1)
                {
                    if ((string)r["linktype"] == "Pipe")
                    {
                        this.sWSP_PIPETableAdapter.DeleteByGlobalID((int)r["global_id"]);
                    }
                    else if ((string)r["linktype"] == "Culvert")
                    {
                        this.sWSP_CULVERTTableAdapter.DeleteByGlobalID((int)r["global_id"]);
                    }
                    else if ((string)r["linktype"] == "Ditch")
                    {
                        this.sWSP_DITCHTableAdapter.DeleteByGlobalID((int)r["global_id"]);
                    }
                }
            }
            this.fKSURVEYPAGEVIEWBindingSource.EndEdit();
            this.sWSP_SURVEY_PAGETableAdapter.Update(sANDBOXDataSet);
        }
        void dataGridViewLinkInfo_Leave(object sender, System.EventArgs e)
        {
            //yes this redundancy is really necessary
            dataGridViewLinkInfo.CurrentRow.DataGridView.EndEdit();
            dataGridViewLinkInfo.EndEdit();
            CurrencyManager cm = (CurrencyManager)dataGridViewLinkInfo.BindingContext[dataGridViewLinkInfo.DataSource, dataGridViewLinkInfo.DataMember];
            cm.EndCurrentEdit();

            PopulateLinkInfo();
        }

        void dataGridViewLinkInfo_MouseLeave(object sender, System.EventArgs e)
        {

        }

        private void FormFieldSurveyView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Environment.HasShutdownStarted)
            {
                //yes this redundancy is really necessary
                try
                {
                    //sometimes, the user will not have a row selected when they close out.
                    dataGridViewLinkInfo.CurrentRow.DataGridView.EndEdit();
                }
                catch (Exception ex)
                {
                }
                dataGridViewLinkInfo.EndEdit();
                CurrencyManager cm = (CurrencyManager)dataGridViewLinkInfo.BindingContext[dataGridViewLinkInfo.DataSource, dataGridViewLinkInfo.DataMember];
                cm.EndCurrentEdit();

                PopulateLinkInfo();
                //e.Cancel = true;
                DialogResult theAnswer = MessageBox.Show("Do you wish to save changes to the database?", "Save before closing", MessageBoxButtons.YesNoCancel);
                if (theAnswer == DialogResult.No)
                {
                    //do nothing
                }
                else if (theAnswer == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (theAnswer == DialogResult.Yes)
                {
                    translateChangesAndSaveToDatabase();
                }

            }
        }

        private void buttonAddMap_Click(object sender, EventArgs e)
        {
            int CurrentView = 0;
            int CurrentSurveyPage = 0;
            int CurrentWatershed = 0;
            int CurrentSubwatershed = 0;

            //these error traps need to happen in case someone has deleted all the views or survey pages for the subwatershed
            try
            {
                CurrentView = (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"];
            }
            catch (Exception ex)
            {
                CurrentView = 0;
            }
            try
            {
                CurrentSurveyPage = (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["page_number"];
            }
            catch (Exception ex)
            {
                CurrentSurveyPage = 0;
            }
            try
            {
                CurrentWatershed = (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"];
            }
            catch (Exception ex)
            {
                CurrentWatershed = 0;
            }
            try
            {
                CurrentSubwatershed = (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"];
            }
            catch (Exception ex)
            {
                CurrentSubwatershed = 0;
            }

            FormAddView child = new FormAddView();
            this.Enabled = false;
            child.ShowDialog();
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_VIEW' table. You can move, or remove it, as needed.
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            this.Enabled = true;
            if (CurrentWatershed != 0)
            {
                while (CurrentWatershed != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                {
                    sWSPWATERSHEDBindingSource.MoveNext();
                }
            }
            if (CurrentSubwatershed != 0)
            {
                while (CurrentSubwatershed != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                {
                    fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                }
            }
            if (CurrentView != 0)
            {
                while (CurrentView != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"])
                {
                    fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                }
            }
            if (CurrentSurveyPage != 0)
            {
                while (CurrentSurveyPage != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["page_number"])
                {
                    fKSURVEYPAGEVIEWBindingSource.MoveNext();
                }
            }
        }

        private void buttonAddSurveyPage_Click(object sender, EventArgs e)
        {
            int CurrentView = 0;
            int CurrentSurveyPage = 0;
            int CurrentWatershed = 0;
            int CurrentSubwatershed = 0;

            //these error traps need to happen in case someone has deleted all the views or survey pages for the subwatershed
            try
            {
                CurrentView = (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"];
            }
            catch (Exception ex)
            {
                CurrentView = 0;
            }
            try
            {
                CurrentSurveyPage = (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["page_number"];
            }
            catch (Exception ex)
            {
                CurrentSurveyPage = 0;
            }
            try
            {
                CurrentWatershed = (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"];
            }
            catch (Exception ex)
            {
                CurrentWatershed = 0;
            }
            try
            {
                CurrentSubwatershed = (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"];
            }
            catch (Exception ex)
            {
                CurrentSubwatershed = 0;
            }
            FormAddSurvey child = new FormAddSurvey();
            child.MapNo = (int)((System.Data.DataRowView)comboBoxMapNo.SelectedItem)["view_number"];
            child.SurveyPage = (int)((System.Data.DataRowView)comboBoxSurveyPage.SelectedItem)["page_number"];
            this.Enabled = false;
            child.ShowDialog();
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            this.Enabled = true;
            if (CurrentWatershed != 0)
            {
                while (CurrentWatershed != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                {
                    sWSPWATERSHEDBindingSource.MoveNext();
                }
            }
            if (CurrentSubwatershed != 0)
            {
                while (CurrentSubwatershed != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                {
                    fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                }
            }
            if (CurrentView != 0)
            {
                while (CurrentView != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"])
                {
                    fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                }
            }
            if (CurrentSurveyPage != 0)
            {
                while (CurrentSurveyPage != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["page_number"])
                {
                    fKSURVEYPAGEVIEWBindingSource.MoveNext();
                }
            }
        }
    }
}
