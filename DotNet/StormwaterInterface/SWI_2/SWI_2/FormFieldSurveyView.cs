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
        //records the selected row in case we are trying to search for infos
        int selectedRow = 0;

        public FormFieldSurveyView()
        {
            InitializeComponent();
        }

        private void FormFieldSurveyView_Load(object sender, EventArgs e)
        {
            //System.Security.Principal.WindowsIdentity ident = System.Security.Principal.WindowsIdentity.GetCurrent();
           // System.Security.Principal.WindowsPrincipal user = new System.Security.Principal.WindowsPrincipal(ident);
            //string theName = "["+user.Identity.Name+"]";
            //MessageBox.Show(theName);
            //this.sWSP_MESH1TableAdapter.SET_SCHEMA_ISSACG(theName);

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
            //this is to separate sessions.
            //MessageBox.Show((this.sWSP_MESH1TableAdapter.SWI_GET_SPID()).ToString());
            tempGlobal_id = -1000 * (int)((Int16)this.sWSP_MESH1TableAdapter.SWI_GET_SPID());
            //fill the FieldSurveypageview datatable with the data from the MESH1 datatable
            /*foreach(DataRow r in this.sANDBOXDataSet.SWSP_MESH1)
            {
                sANDBOXDataSet.DataTableFieldSurvey.LoadDataRow(r.ItemArray, false);
                sANDBOXDataSet.DataTableFieldSurvey.EndLoadData();
                sANDBOXDataSet.DataTableFieldSurvey.AcceptChanges();
            }*/
            this.sANDBOXDataSet.SWSP_MESH1.CopyToDataTable(sANDBOXDataSet.DataTableFieldSurvey, LoadOption.PreserveChanges);

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
                        //insert an empty photo record into the photo table?
                        //This may be necessary for the datatable records.
                        this.swsP_PHOTOTableAdapter1.InsertQuery(tempGlobal_id, "", "");
                    }

                    if (r.RowState == DataRowState.Modified)
                    {
                        r["action"] = 3;
                        sANDBOXDataSet.DataTableFieldSurvey.RemoveDataTableFieldSurveyRow((SANDBOXDataSet.DataTableFieldSurveyRow)(sANDBOXDataSet.DataTableFieldSurvey.Select("global_id = " + r["global_id"].ToString()))[0]);
                        sANDBOXDataSet.DataTableFieldSurvey.LoadDataRow(r.ItemArray, false);
                        sANDBOXDataSet.DataTableFieldSurvey.EndLoadData();
                        sANDBOXDataSet.DataTableFieldSurvey.AcceptChanges();
                    }
                    
                    
                    /*if (r.RowState == DataRowState.Deleted)
                    {
                        
                    }*/
                }

                //change the action for deleted entries.
                //If the row is intended to be deleted,
                //look in the hash set deletedGlobalID
                //and then in datatablefieldSurvey
                //if the globalID in deletedGlobalID matches
                //the globalID from datatablefieldSurvey,
                //change the action flag in datatablefieldSurvey to 1
                //and save that change in datatablefieldSurvey.
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

                sANDBOXDataSet.DataTableFieldSurveyEditable.Clear();
                sANDBOXDataSet.DataTableFieldSurveyEditable.AcceptChanges();
                foreach (DataRow r in this.sANDBOXDataSet.DataTableFieldSurvey)
                {
                    if ((int)r["view_number"]/*[15]*/ == (int)((DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"]/*[2]*/ &&
                        (int)r["page_number"]/*[16]*/ == Int32.Parse(comboBoxSurveyPage.Text)&&// (int) ((DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)[2] &&
                       (string)r["watershed"]/*[17]*/ == (string)((DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed"]/*[1]*/ &&
                       (string)r["subwatershed"]/*[18]*/ == (string)((DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed"]/*[2]*/ &&
                        (int)((r["action"] is System.DBNull) ? 0 : (int)r["action"]) != 1)
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

        /*private void CheckEvaluatorsAssociatedWithThisSurveyPage()
        {
            //check the objects in the checkedListBox the evaluator_id is in
            //the Survey_Page_Evaluator Dataset for this SurveyPage
            object item;
            try
            {
                for (int index = 0; index < checkedListBoxEvaluators.Items.Count; index++)
                {
                    item = checkedListBoxEvaluators.Items[index];
                    if ((int)this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.IdentifyValidEvaluators((int)comboBoxSurveyPage.SelectedValue, (int)((System.Data.DataRowView)item).Row[0]) != 0)
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
        }*/

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
            //CheckEvaluatorsAssociatedWithThisSurveyPage();
            PopulateLinkInfo();
        }

        private void checkedListBoxEvaluators_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*System.Data.DataRowView item;
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
            }*/
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
                if (r["photo_id"] is System.DBNull) { r["photo_id"] = " "; }
                //if the global_id is negative, then we are adding a record
                int old_global_id = (int)r["global_id"];
                if ((int)r["global_id"] < 0)
                {
                    int globalID = 0;
                    //the transfer of this data should be handled simply to the linkinfo datasource
                    //insert based on the value in the link type column
                    if ((string)r["linktype"] == "Pipe")
                    {
                        
                        //adding a pipe means:
                        //placing a new entry in the globalID table,
                        //taking that value and using it to create a new pipe
                        this.sWSP_GLOBAL_IDTableAdapter.InsertQuery("");
                        //what was the global ID that was just inserted?  The highest value in the GlobalID table.  Since we just inserted to it, there is no chance that it could be null
                        globalID = (int)this.sWSP_GLOBAL_IDTableAdapter.ScalarQuery();
                        //add this record to the SWSP_PIPE table
                        this.sWSP_PIPETableAdapter.InsertQuery(globalID,
                                                            page_id,
                                                            (string)r["us_node"],
                                                            (string)r["ds_node"],
                                                            (double?)((r["us_depth_in"] is System.DBNull) ? null : (double?)r["us_depth_in"]),
                                                            (double?)((r["ds_depth_in"] is System.DBNull) ? null : (double?)r["ds_depth_in"]),
                                                            (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                            (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                            material_id,
                                                            shape_id,
                                                            (string)r["photo_id"],
                                                            (int?)((r["length_ft"] is System.DBNull) ? null :(int?)r["length_ft"]),
                                                            (string)r["node"],
                                                            (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                            culvert_opening_type/*(int?) ((r["culvert_opening"] is System.DBNull) ? null : (int?)r["culvert_opening"])*/);
                    }
                    else if ((string)r["linktype"] == "Ditch")
                    {
                        //adding a ditch means:
                        //placing a new entry in the globalID table,
                        //taking that value and using it to create a new ditch
                        this.sWSP_GLOBAL_IDTableAdapter.InsertQuery("");
                        //what was the global ID that was just inserted?  The highest value in the GlobalID table.  Since we just inserted to it, there is no chance that it could be null
                        globalID = (int)this.sWSP_GLOBAL_IDTableAdapter.ScalarQuery();
                        //add this record to the SWSP_DITCH table
                        this.sWSP_DITCHTableAdapter.InsertQuery(globalID,
                                                            page_id,
                                                            (string)r["node"],
                                                            facing_id,
                                                            (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                            (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                            (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                            material_id,
                                                            (string)r["photo_id"],
                                                            (string)r["ds_node"],
                                                            (string)r["us_node"],
                                                            (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]));
                    }
                    else if ((string)r["linktype"] == "Culvert")
                    {
                        //adding a culvert means:
                        //placing a new entry in the globalID table,
                        //taking that value and using it to create a new culvert
                        this.sWSP_GLOBAL_IDTableAdapter.InsertQuery("");
                        //what was the global ID that was just inserted?  The highest value in the GlobalID table.  Since we just inserted to it, there is no chance that it could be null
                        globalID = (int)this.sWSP_GLOBAL_IDTableAdapter.ScalarQuery();
                        //add this record to the SWSP_CULVERT table
                        this.sWSP_CULVERTTableAdapter.InsertQuery(globalID,
                                                            page_id,
                                                            (string)r["node"],
                                                            facing_id,
                                                            culvert_opening_type,
                                                            shape_id,
                                                            (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                            (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                            (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                            material_id,
                                                            (string)r["photo_id"],
                                                            (string)r["ds_node"],
                                                            (string)r["us_node"],
                                                            (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]),
                                                            (double?)((r["us_depth_in"] is System.DBNull) ? null : (double?)r["us_depth_in"]),
                                                            (double?)((r["ds_depth_in"] is System.DBNull) ? null : (double?)r["ds_depth_in"])
                                                            );
                    }

                    this.swsP_PHOTOTableAdapter1.UpdateQueryNewGlobalID(globalID, old_global_id);
                }
                else if (r["action"] is System.DBNull)
                {
                    //do nothing
                }
                //if the global id is positive, and the action flag is 3, then we are modifying an existing record
                //and must check to see if the existing global id belongs to a different type of conduit.
                //if the existing global id belongs to a different conduit type, then we need to delete the 
                //previous conduit type and insert the new one.
                //There is also the case that perhaps another user has already deleted this pipe. This case is handled
                //by the case when the pipes have not been found in the PIPEID field, then they are deleted
                //from the other tables.  This algorithm pretty much assures that the pipe will be recreated.
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
                            this.sWSP_PIPETableAdapter.InsertQuery(
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
                                                            (string)r["photo_id"],
                                                            (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]),
                                                            (string)r["node"],
                                                            (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                            culvert_opening_type//(int?)((r["culvert_opening"] is System.DBNull) ? null : (int?)r["culvert_opening"])
                                                            
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
                                                            (string)r["photo_id"],
                                                            (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]),
                                                            ((r["node"] is System.DBNull) ? "" : (string)r["node"]),
                                                            (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                            culvert_opening_type,//(int?)((r["culvert_opening"] is System.DBNull) ? null : (int?)r["culvert_opening"]),
                                                            
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
                            this.sWSP_DITCHTableAdapter.InsertQuery(
                                                                (int)r["global_id"],
                                                                page_id,
                                                                ((r["node"] is System.DBNull) ? "" : (string)r["node"]),
                                                                facing_id,
                                                                (double?)((r["dimension3"] is System.DBNull) ? null : (double?)r["dimension3"]),
                                                                (double?)((r["dimension1"] is System.DBNull) ? null : (double?)r["dimension1"]),
                                                                (double?)((r["dimension2"] is System.DBNull) ? null : (double?)r["dimension2"]),
                                                                material_id,
                                                                (string)r["photo_id"],
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
                                                                (string)r["photo_id"],
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
                            this.sWSP_CULVERTTableAdapter.InsertQuery(
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
                                                                (string)r["photo_id"],
                                                                ((r["ds_node"] is System.DBNull) ? "" : (string)r["ds_node"]),
                                                                ((r["us_node"] is System.DBNull) ? "" : (string)r["us_node"]),
                                                                (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]),
                                                                (double?)((r["us_depth_in"] is System.DBNull) ? null : (double?)r["us_depth_in"]),
                                                                (double?)((r["ds_depth_in"] is System.DBNull) ? null : (double?)r["ds_depth_in"])
                                                                
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
                                                                (string)r["photo_id"],
                                                                ((r["ds_node"] is System.DBNull) ? "" : (string)r["ds_node"]),
                                                                ((r["us_node"] is System.DBNull) ? "" : (string)r["us_node"]),
                                                                (int?)((r["length_ft"] is System.DBNull) ? null : (int?)r["length_ft"]),
                                                                (double?)((r["us_depth_in"] is System.DBNull) ? null : (double?)r["us_depth_in"]),
                                                                (double?)((r["ds_depth_in"] is System.DBNull) ? null : (double?)r["ds_depth_in"]),
                                                                
                                                                (int)r["global_id"]);
                        }
                    }
                }

                //update all of the photos that had the previous global_id to the new global_id
                //this. sWSP_DITCHTableAdapter.GetDataByGlobalID((int)r["global_id"]).Count < 1));
                //this.swsP_PHOTOTableAdapter1.UpdateQueryNewGlobalID(;

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
            //record the record that is selected in case we are doing a search
            selectedRow = dataGridViewLinkInfo.CurrentRow.Index;
            dataGridViewLinkInfo.CurrentRow.DataGridView.EndEdit();
            dataGridViewLinkInfo.EndEdit();
            CurrencyManager cm = (CurrencyManager)dataGridViewLinkInfo.BindingContext[dataGridViewLinkInfo.DataSource, dataGridViewLinkInfo.DataMember];
            cm.EndCurrentEdit();

            PopulateLinkInfo();
        }

        void dataGridViewLinkInfo_MouseLeave(object sender, System.EventArgs e)
        {
            //yes this redundancy is really necessary
            /*dataGridViewLinkInfo.CurrentRow.DataGridView.EndEdit();
            dataGridViewLinkInfo.EndEdit();
            CurrencyManager cm = (CurrencyManager)dataGridViewLinkInfo.BindingContext[dataGridViewLinkInfo.DataSource, dataGridViewLinkInfo.DataMember];
            cm.EndCurrentEdit();

            PopulateLinkInfo();*/
        }

        private void FormFieldSurveyView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Environment.HasShutdownStarted)
            {
                //yes this redundancy is really necessary
                try
                {
                    //sometimes the user will not have a row selected when they close out.
                    dataGridViewLinkInfo.CurrentRow.DataGridView.EndEdit();
                }
                catch (Exception ex)
                {
                }
                dataGridViewLinkInfo.EndEdit();
                CurrencyManager cm = (CurrencyManager)dataGridViewLinkInfo.BindingContext[dataGridViewLinkInfo.DataSource, dataGridViewLinkInfo.DataMember];
                cm.EndCurrentEdit();

                PopulateLinkInfo();
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
            //yes this redundancy is really necessary
            try
            {
                //sometimes the user will not have a row selected when they close out.
                dataGridViewLinkInfo.CurrentRow.DataGridView.EndEdit();
            }
            catch (Exception ex)
            {
            }
            dataGridViewLinkInfo.EndEdit();
            CurrencyManager cm = (CurrencyManager)dataGridViewLinkInfo.BindingContext[dataGridViewLinkInfo.DataSource, dataGridViewLinkInfo.DataMember];
            cm.EndCurrentEdit();

            PopulateLinkInfo();
            DialogResult theAnswer = MessageBox.Show("Do you wish to save changes to the database?", "Save before adding map", MessageBoxButtons.YesNo);
            if (theAnswer == DialogResult.No)
            {
                //do nothing
            }
            else if (theAnswer == DialogResult.Yes)
            {
                translateChangesAndSaveToDatabase();
            }

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
            //yes this redundancy is really necessary
            try
            {
                //sometimes the user will not have a row selected when they close out.
                dataGridViewLinkInfo.CurrentRow.DataGridView.EndEdit();
            }
            catch (Exception ex)
            {
            }
            dataGridViewLinkInfo.EndEdit();
            CurrencyManager cm = (CurrencyManager)dataGridViewLinkInfo.BindingContext[dataGridViewLinkInfo.DataSource, dataGridViewLinkInfo.DataMember];
            cm.EndCurrentEdit();

            PopulateLinkInfo();
            DialogResult theAnswer = MessageBox.Show("Do you wish to save changes to the database?", "Save before adding page", MessageBoxButtons.YesNo);
            if (theAnswer == DialogResult.No)
            {
                //do nothing
            }
            else if (theAnswer == DialogResult.Yes)
            {
                translateChangesAndSaveToDatabase();
            }

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

        private void dataGridViewLinkInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)//check whether is it button column or not.....
            {
                int currentrow = this.dataGridViewLinkInfo.CurrentRow.Index;
                //this.dataGridViewLinkInfo.CurrentRow.DataGridView.EndEdit();
                //this.dataGridViewLinkInfo.EndEdit();
                CurrencyManager cm = (CurrencyManager)dataGridViewLinkInfo.BindingContext[dataGridViewLinkInfo.DataSource, dataGridViewLinkInfo.DataMember];
                cm.EndCurrentEdit();
                PopulateLinkInfo();

                this.dataGridViewLinkInfo.CurrentCell = dataGridViewLinkInfo.Rows[currentrow].Cells[0];

                dataGridViewLinkInfo.Rows[currentrow].Selected = true;
                FormPhotos child = new FormPhotos();

                child.GlobalID = (int)((System.Data.DataRowView)dataTableFieldSurveyEditableBindingSource.Current)["global_id"];
                this.Enabled = false;
                child.ShowDialog();
                this.Enabled = true;
            }
        }

        private void buttonSearchForNode_Click(object sender, EventArgs e)
        {
            string theNodeToSearchFor = textBoxSearchForNode.Text.Trim();

            int lastGlobalID = 0;
            int newGlobalID = 0;
            DataRow[] theDataRows = null;
            int page_number = 0;
            int view_number = 0;
            string watershed = "";
            string subwatershed = "";

            try
            {
                page_number = Int32.Parse(comboBoxSurveyPage.Text);
                view_number = (int)((DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)[2];
                watershed = (string)((DataRowView)sWSPWATERSHEDBindingSource.Current)[1];
                subwatershed = (string)((DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)[2];
            }
            catch (Exception ex)
            {
                //something is wrong with the selected data
                page_number = 1;
                view_number = 1;
                watershed = "";
                subwatershed = "";
            }

            foreach(DataGridViewRow R in dataGridViewLinkInfo.Rows)
            {
                R.Selected = false;
            }

            try
            {
                dataGridViewLinkInfo.Rows[selectedRow].Selected = true;
            }
            catch
            {
                //The page has been changed since the last search
            }
            //the global id of the selected record will be the global ID that we begin our search on.
            try
            {
                lastGlobalID = (int)(dataGridViewLinkInfo.Rows[selectedRow].Cells["GlobID"].Value);
            }
            catch (Exception ex)
            {
                lastGlobalID = 0;
            }

            //start with this global id and find the next highest global id that has a matching node
            //if there are no global ids above this one with a matching node, start back at 0 and
            //look up until we reach this global id again
            //if there are no matches at all, then just go back to the current global id
            //MessageBox.Show(lastGlobalID.ToString());
            theDataRows = this.sANDBOXDataSet.DataTableFieldSurvey.Select("(us_node = '" + theNodeToSearchFor + "' or ds_node = '" + theNodeToSearchFor + "') and global_id > " + lastGlobalID.ToString(), "global_id asc");
            try
            {
                newGlobalID = (int)theDataRows[0][0];

                try
                {
                    page_number = (int)theDataRows[0][16];
                    view_number = (int)theDataRows[0][15];
                    watershed = (string)theDataRows[0][17];
                    subwatershed = (string)theDataRows[0][18];
                }
                catch (Exception ex)
                {
                    //something is wrong with the selected data
                    page_number = 1;
                    view_number = 1;
                    watershed = "";
                    subwatershed = "";
                }

                try
                {
                    newGlobalID = (int)theDataRows[0][0];

                    if (watershed != "")
                    {
                        sWSPWATERSHEDBindingSource.MoveFirst();
                        while (watershed != (string)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed"])
                        {
                            sWSPWATERSHEDBindingSource.MoveNext();
                        }
                    }
                    if (subwatershed != "")
                    {
                        fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                        while (subwatershed != (string)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed"])
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                        }
                    }
                    if (view_number != 0)
                    {
                        fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                        while (view_number != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"])
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                        }
                    }
                    if (page_number != 0)
                    {
                        fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                        while (page_number != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["page_number"])
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveNext();
                        }
                    }

                }
                catch
                {
                    //if there is no global ID, then no rows were returned, and there is no match at all in any of the records
                }
            }
            catch
            {
                //if there is no globalID, then no rows were returned, and we need to check all records starting at the bottom
                
                theDataRows = this.sANDBOXDataSet.DataTableFieldSurvey.Select("us_node = '" + theNodeToSearchFor + "' or ds_node = '" + theNodeToSearchFor + "' ", "global_id asc");

                if (theDataRows.Length != 0)
                {
                    newGlobalID = (int)theDataRows[0][0];


                    try
                    {
                        page_number = (int)theDataRows[0][16];
                        view_number = (int)theDataRows[0][15];
                        watershed = (string)theDataRows[0][17];
                        subwatershed = (string)theDataRows[0][18];
                    }
                    catch (Exception ex)
                    {
                        //something is wrong with the selected data
                    }


                    try
                    {
                        if (watershed != "")
                        {
                            sWSPWATERSHEDBindingSource.MoveFirst();
                            while (watershed != (string)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed"])
                            {
                                sWSPWATERSHEDBindingSource.MoveNext();
                            }
                        }
                        if (subwatershed != "")
                        {
                            fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
                            while (subwatershed != (string)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed"])
                            {
                                fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                            }
                        }
                        if (view_number != 0)
                        {
                            fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
                            while (view_number != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_number"])
                            {
                                fKVIEWSUBWATERSHEDBindingSource.MoveNext();
                            }
                        }
                        if (page_number != 0)
                        {
                            fKSURVEYPAGEVIEWBindingSource.MoveFirst();
                            while (page_number != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["page_number"])
                            {
                                fKSURVEYPAGEVIEWBindingSource.MoveNext();
                            }
                        }

                    }
                    catch
                    {
                        //if there is no global ID, then no rows were returned, and there is no match at all in any of the records
                    }
                }
                else
                {
                    MessageBox.Show("Node not found!");
                }

                
            }
            //MessageBox.Show(((int)theDataRows[0][0]).ToString());



            //Now that we have the global ID of the record in question, we need to load up all the related tables
            //with the matching data


            //dataTableFieldSurveyEditableBindingSource.Position = dataTablePosition;
            //MessageBox.Show(dataTableFieldSurveyEditableBindingSource.Position.ToString() + ", " + dataTablePosition.ToString());


            //select top 1 global_id from datatablefieldsurveyeditable where global_id  > lastGlobalID and us_node = theNodeToSearchFor or ds_node = theNodeToSearchFor order by global_id
            //remember that datatablefieldsurveyeditable is not zero-based, there should be negative global_ids in there, so when we hit the highest number with
            //no matches, we have to start at the lowest negative number and then search back up to the starting global_id
            /*if (string.Compare(currentSelected, theNodeToSearchFor) == 0)
            {
                _lastGlobalID = (int)this.sWSP_DITCHTableAdapter.FindFirstGlobalID(textBoxFindNode.Text);
                RefreshInterfaceTables();
                this.Refresh();
                fKDITCHSURVEYPAGEBindingSource.MoveFirst();
                while (_lastGlobalID != (int)((System.Data.DataRowView)fKDITCHSURVEYPAGEBindingSource.Current)["global_id"])
                {
                    fKDITCHSURVEYPAGEBindingSource.MoveNext();
                }
                this.Refresh();
            }*/
        }
    }
}
