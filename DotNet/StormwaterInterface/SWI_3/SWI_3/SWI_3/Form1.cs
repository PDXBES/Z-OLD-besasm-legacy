using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWI_3
{
    

    public partial class Form1 : Form
    {
        //I'm not sure this is the best way to do this...
        string myConnectionString = "Data Source=SIRTOBY;Initial Catalog=SWI;Integrated Security=True";
        bool hasLoaded = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            resetDataTables();

            hasLoaded = true;
        }

        private void reloadData()
        {
            hasLoaded = false;

            int xview_id = (int)((DataRowView)this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Current)["view_id"];
            int xsurvey_page_id = (int)((DataRowView)this.mESHVIEWSMESHPAGESBindingSource.Current)["survey_page_id"];
            int xwatershed_id = (int)((DataRowView)this.mESHWATERSHEDBindingSource.Current)["watershed_id"];
            int xsubwatershed_id = (int)((DataRowView)this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Current)["subwatershed_id"];

            this.sWIDataSet.Clear();

            resetDataTables();

            hasLoaded = true;

            

            this.mESHWATERSHEDBindingSource.Position = this.mESHWATERSHEDBindingSource.Find("watershed_id", xwatershed_id);
            //this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Position = this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Find("subwatershed_id", xsubwatershed_id);
            PropertyDescriptorCollection pdc = this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.CurrencyManager.GetItemProperties();
            int pos = this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Find(pdc["subwatershed_id"], xsubwatershed_id);
            if (pos >= 0) this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Position = pos;
            //this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Position = this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Find("view_id", xview_id);
            pdc = this.mESHSUBWATERSHEDMESHVIEWSBindingSource.CurrencyManager.GetItemProperties();
            pos = this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Find(pdc["view_id"], xview_id);
            if (pos >= 0) this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Position = pos;
            //this.mESHVIEWSMESHPAGESBindingSource.Position = this.mESHVIEWSMESHPAGESBindingSource.Find("survey_page_id", xsurvey_page_id);
            pdc = this.mESHVIEWSMESHPAGESBindingSource.CurrencyManager.GetItemProperties();
            pos = this.mESHVIEWSMESHPAGESBindingSource.Find(pdc["survey_page_id"], xsurvey_page_id);
            if (pos >= 0) this.mESHVIEWSMESHPAGESBindingSource.Position = pos;
        }

        private void reloadDataAtViewAndPage(int ViewID, int PageNumber)
        {
            hasLoaded = false;

            int xview_id = (int)((DataRowView)this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Current)["view_id"];
            int xsurvey_page_id = (int)((DataRowView)this.mESHVIEWSMESHPAGESBindingSource.Current)["survey_page_id"];
            int xwatershed_id = (int)((DataRowView)this.mESHWATERSHEDBindingSource.Current)["watershed_id"];
            int xsubwatershed_id = (int)((DataRowView)this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Current)["subwatershed_id"];

            this.sWIDataSet.Clear();

            resetDataTables();

            hasLoaded = true;

            this.mESHWATERSHEDBindingSource.Position = this.mESHWATERSHEDBindingSource.Find("watershed_id", xwatershed_id);
            //this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Position = this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Find("subwatershed_id", xsubwatershed_id);
            PropertyDescriptorCollection pdc = this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.CurrencyManager.GetItemProperties();
            int pos = this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Find(pdc["subwatershed_id"], xsubwatershed_id);
            if (pos >= 0) this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Position = pos;
            //this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Position = this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Find("view_id", xview_id);
            pdc = this.mESHSUBWATERSHEDMESHVIEWSBindingSource.CurrencyManager.GetItemProperties();
            pos = this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Find(pdc["view_id"], ViewID);
            if (pos >= 0) this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Position = pos;
            //this.mESHVIEWSMESHPAGESBindingSource.Position = this.mESHVIEWSMESHPAGESBindingSource.Find("survey_page_id", xsurvey_page_id);
            pdc = this.mESHVIEWSMESHPAGESBindingSource.CurrencyManager.GetItemProperties();
            pos = this.mESHVIEWSMESHPAGESBindingSource.Find(pdc["page_number"], PageNumber);
            if (pos >= 0) this.mESHVIEWSMESHPAGESBindingSource.Position = pos;
        }

        private void dataGridViewConveyanceObjects_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            int view_id = (int)((DataRowView)this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Current)["view_id"];
            int survey_page_id = (int)((DataRowView)this.mESHVIEWSMESHPAGESBindingSource.Current)["survey_page_id"];
            int watershed_id = (int)((DataRowView)this.mESHWATERSHEDBindingSource.Current)["watershed_id"];
            int subwatershed_id = (int)((DataRowView)this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Current)["subwatershed_id"];

            e.Row.Cells["LinkType"].Value = "Pipe";
            e.Row.Cells["shape_type_id"].Value = 1;
            e.Row.Cells["material_type_id"].Value = 1;
            e.Row.Cells["view_id"].Value =view_id;
            e.Row.Cells["survey_page_id"].Value = survey_page_id;
            e.Row.Cells["watershed_id"].Value = watershed_id;
            e.Row.Cells["subwatershed_id"].Value = subwatershed_id;

            //dataGridViewConveyanceObjects.Rows.Add(e.Row);
        }

        private void AcceptPipeChanges()
        {
            if (hasLoaded == true)
            {
                try
                {
                    this.Validate();

                    this.mESHVIEWSMESHPAGESBindingSource.EndEdit();
                    this.mESH_PAGESTableAdapter.Update(this.sWIDataSet.MESH_PAGES);

                    this.mESHPAGESMESHBindingSource.EndEdit();
                    this.mESHTableAdapter.Update(this.sWIDataSet.MESH);

                    this.mESHMESHPHOTOSBindingSource.EndEdit();
                    this.mESH_PHOTOSTableAdapter.Update(this.sWIDataSet.MESH_PHOTOS);

                    this.mESHPAGESMESHPAGEEVALUATORSBindingSource.EndEdit();
                    this.mESH_PAGE_EVALUATORSTableAdapter.Update(this.sWIDataSet.MESH_PAGE_EVALUATORS);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something's wrong");
                }
            }
        }

        private void AcceptPipeChangesAndReload()
        {
            if (hasLoaded == true)
            {
                try
                {
                    this.Validate();

                    this.mESHVIEWSMESHPAGESBindingSource.EndEdit();
                    this.mESH_PAGESTableAdapter.Update(this.sWIDataSet.MESH_PAGES);

                    this.mESHPAGESMESHBindingSource.EndEdit();
                    this.mESHTableAdapter.Update(this.sWIDataSet.MESH);

                    this.mESHMESHPHOTOSBindingSource.EndEdit();
                    this.mESH_PHOTOSTableAdapter.Update(this.sWIDataSet.MESH_PHOTOS);

                    this.mESHPAGESMESHPAGEEVALUATORSBindingSource.EndEdit();
                    this.mESH_PAGE_EVALUATORSTableAdapter.Update(this.sWIDataSet.MESH_PAGE_EVALUATORS);

                    reloadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something's wrong");
                }
            }
        }

        private void saveChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcceptPipeChangesAndReload();
            //saveChangesToEverything();
        }

        private void saveChangesToEverything()
        {
            //call all of the procedures that update/delete/add records to the tables
            //that this interface is capable of updating.
            saveChangesToPages();
            saveChangesToEvaluators();
            saveChangesToConduits();
            saveChangesToPhotos();
            //it is possible that we will need to create calls to update mapview, update subwatershed,
            //and updatewatershed, but it doesn't look like that is necessary just yet.
        }

        private void saveChangesToPages()
        {
            //the change to the pages table may include new pages, deleted pages, and updated pages

            //a new page requires a new set of evaluators.  It may be that a previous function has already taken care of this?
            
            //a new page must be associated with the watershed/subwatershed/mapviewAQ 

            //a page may have had the weather conditions and date changed.

            //a deleted page requires the associated evaluators to be deleted.
        }

        private void saveChangesToEvaluators()
        {
            //if an evaluator was present for the evaluation, then present should equal 1, otherwise, 0
            //This is the only change that this function should enact


        }

        private void saveChangesToConduits()
        {
            //this procedure will take care of updates, deletes, and additions to the conduit table


        }

        private void saveChangesToPhotos()
        {
            //this procedure will take care of updates, deletes, and additions to the photos table

        }

        

        private int ExecuteScalar(string query, string myConnectionString, int returnValue)
        {
            using (SqlConnection connection = new SqlConnection(myConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    query, connection);
                connection.Open();
                command.CommandText = query;
                try
                {
                    returnValue = (Int32)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    //return value is null, so return -1 instead
                    returnValue = -1;
                }
            }

            return returnValue;
        }

        private void ToolStripViewGoButton_Click(object sender, EventArgs e)
        {
            //Create a new page, be sure to populate this page
            //use the current view and watershed/subwatershed
            int view_id = (int)((DataRowView)this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Current)["view_id"];

            addNewPage(Int32.Parse(this.ToolStripNUDPage.Text), view_id);
        }

        private void addNewPage(int pageNumber, int view_id)
        {
            //This procedure adds a new page to the database.
            int returnValue = 0;
            int watershed_id = (int)((DataRowView)this.mESHWATERSHEDBindingSource.Current)["watershed_id"];
            int subwatershed_id = (int)((DataRowView)this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Current)["subwatershed_id"]; 

            //Check to see if this pagenumber already exists for this view_id
            //if it already exists, throw an error and exit
            string theSQL = "SELECT COUNT(*) FROM MESH_PAGES WHERE page_number = " + pageNumber.ToString() + " AND view_id = " + view_id.ToString();
            returnValue = ExecuteScalar(theSQL, myConnectionString, returnValue);

            if (returnValue != 0)
            {
                MessageBox.Show("View number " + pageNumber.ToString() + " already exists for this view!");
                return;
            }
            else
            {
                //inform the database that we will need a new page_id
                //if we can't get a new page_id, throw an error and exit
                try
                {
                    theSQL = "INSERT INTO [SWI].[dbo].MESH_PAGES (view_id, page_number, date, weather, comment) OUTPUT inserted.survey_page_id VALUES (" +
                        view_id.ToString() + ", " + pageNumber.ToString() + ", NULL, NULL, NULL)";
                    returnValue = ExecuteScalar(theSQL, myConnectionString, returnValue);

                    //New evaluators:
                    //We will need the previously entered pages ID:
                    if (returnValue > 0)
                    {
                        theSQL = "INSERT INTO [SWI].[dbo].MESH_PAGE_EVALUATORS (survey_page_id, evaluator_id, initials, present) " +
                            "SELECT " + returnValue.ToString() + ", evaluator_id, initials, 0 FROM [SWI].[dbo].MESH_EVALUATORS";
                        ExecuteScalar(theSQL, myConnectionString, returnValue);
                    }

                    AcceptPipeChanges();
                    reloadDataAtViewAndPage(view_id, pageNumber);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not properly add page to database");
                }
            }
        }

        private void addNewView(int viewNumber)
        {
            //This procedure adds a new page to the database.
            int returnValue = 0;
            int view_id = 0;
            int watershed_id = (int)((DataRowView)this.mESHWATERSHEDBindingSource.Current)["watershed_id"];
            int subwatershed_id = (int)((DataRowView)this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Current)["subwatershed_id"];

            //Check to see if this pagenumber already exists for this view_id
            //if it already exists, throw an error and exit
            string theSQL = "SELECT COUNT(*) FROM MESH_VIEWS WHERE view_number = " + viewNumber.ToString() + " AND subwatershed_id = " + subwatershed_id.ToString();
            returnValue = ExecuteScalar(theSQL, myConnectionString, returnValue);

            if (returnValue != 0)
            {
                MessageBox.Show("Map number " + viewNumber.ToString() + " already exists for this subwatershed!");
                return;
            }
            else
            {
                //inform the database that we will need a new page_id
                //if we can't get a new page_id, throw an error and exit
                try
                {
                    theSQL = "INSERT INTO [SWI].[dbo].MESH_VIEWS (subwatershed_id, view_number, description) OUTPUT inserted.view_id VALUES (" +
                        subwatershed_id.ToString() + ", " + viewNumber.ToString() + ", NULL)";
                    returnValue = ExecuteScalar(theSQL, myConnectionString, returnValue);
                    view_id = returnValue;

                    theSQL = "INSERT INTO [SWI].[dbo].MESH_PAGES (view_id, page_number, date, weather, comment) OUTPUT inserted.survey_page_id VALUES (" +
                        returnValue.ToString() + ", 1 , NULL, NULL, NULL)";
                    returnValue = ExecuteScalar(theSQL, myConnectionString, returnValue);

                    //We will need the previously entered pages ID:
                    if (returnValue > 0)
                    {
                        theSQL = "INSERT INTO [SWI].[dbo].MESH_PAGE_EVALUATORS (survey_page_id, evaluator_id, initials, present) " +
                            "SELECT " + returnValue.ToString() + ", evaluator_id, initials, 0 FROM [SWI].[dbo].MESH_EVALUATORS";
                        ExecuteScalar(theSQL, myConnectionString, returnValue);
                    }

                    AcceptPipeChanges();
                    reloadDataAtViewAndPage(view_id, 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not properly add map to database");
                }
            }
        }

        private void stopAndSmellTheRosesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //This is really just a spacer between adding views and adding pages.
            //It can stay for now
        }

        private void ToolStripMapGoButton_Click(object sender, EventArgs e)
        {
            //Create a new view, be sure to add a page and populate it
            //use the current watershed/subwatershed

            int view_id = (int)((DataRowView)this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Current)["view_id"];

            addNewView(Int32.Parse(this.ToolStripNUDView.Text));
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            DataRow newRow = this.sWIDataSet.MESH.NewMESHRow();

            int view_id = (int)((DataRowView)this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Current)["view_id"];
            int survey_page_id = (int)((DataRowView)this.mESHVIEWSMESHPAGESBindingSource.Current)["survey_page_id"];
            int watershed_id = (int)((DataRowView)this.mESHWATERSHEDBindingSource.Current)["watershed_id"];
            int subwatershed_id = (int)((DataRowView)this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.Current)["subwatershed_id"];

            newRow["LinkType"] = "Pipe";
            newRow["shape_type_id"] = 1;
            newRow["material_type_id"] = 1;
            newRow["view_id"] = view_id;
            newRow["survey_page_id"] = survey_page_id;
            newRow["watershed_id"] = watershed_id;
            newRow["subwatershed_id"] = subwatershed_id;

            this.sWIDataSet.MESH.Rows.Add(newRow);
        }

        private void dataGridViewConveyanceObjects_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Exception Handled! (Not really, but debugging can happen now at least)
            MessageBox.Show("Exception: " + e.ToString());
        }

        private void resetDataTables()
        {
            // TODO: This line of code loads data into the 'sWIDataSet.MESH_CULVERT_OPENING_TYPE' table. You can move, or remove it, as needed.
            this.mESH_CULVERT_OPENING_TYPETableAdapter.Fill(this.sWIDataSet.MESH_CULVERT_OPENING_TYPE);
            // TODO: This line of code loads data into the 'sWIDataSet.MESH_MATERIAL_TYPE' table. You can move, or remove it, as needed.
            this.mESH_MATERIAL_TYPETableAdapter.Fill(this.sWIDataSet.MESH_MATERIAL_TYPE);
            // TODO: This line of code loads data into the 'sWIDataSet.MESH_SHAPE_TYPE' table. You can move, or remove it, as needed.
            this.mESH_SHAPE_TYPETableAdapter.Fill(this.sWIDataSet.MESH_SHAPE_TYPE);
            // TODO: This line of code loads data into the 'sWIDataSet.MESH_PAGE_EVALUATORS' table. You can move, or remove it, as needed.
            this.mESH_PAGE_EVALUATORSTableAdapter.Fill(this.sWIDataSet.MESH_PAGE_EVALUATORS);
            // TODO: This line of code loads data into the 'sWIDataSet.MESH' table. You can move, or remove it, as needed.
            this.mESHTableAdapter.Fill(this.sWIDataSet.MESH);
            // TODO: This line of code loads data into the 'sWIDataSet.MESH_PHOTOS' table. You can move, or remove it, as needed.
            this.mESH_PHOTOSTableAdapter.Fill(this.sWIDataSet.MESH_PHOTOS);
            // TODO: This line of code loads data into the 'sWIDataSet.MESH_PAGES' table. You can move, or remove it, as needed.
            this.mESH_PAGESTableAdapter.Fill(this.sWIDataSet.MESH_PAGES);
            // TODO: This line of code loads data into the 'sWIDataSet.MESH_VIEWS' table. You can move, or remove it, as needed.
            this.mESH_VIEWSTableAdapter.Fill(this.sWIDataSet.MESH_VIEWS);
            // TODO: This line of code loads data into the 'sWIDataSet.MESH_SUBWATERSHED' table. You can move, or remove it, as needed.
            this.mESH_SUBWATERSHEDTableAdapter.Fill(this.sWIDataSet.MESH_SUBWATERSHED);
            // TODO: This line of code loads data into the 'sWIDataSet.MESH_WATERSHED' table. You can move, or remove it, as needed.
            this.mESH_WATERSHEDTableAdapter.Fill(this.sWIDataSet.MESH_WATERSHED);
        }

        private void dataGridViewConveyanceObjects_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if the cell that has changed is a Link Type cell, and it has been changed to a ditch, then
            //change the shape to a TR31
            try
            {
                if (dataGridViewConveyanceObjects.Columns[e.ColumnIndex].Name.CompareTo("LinkType") == 0)
                {
                    if (((string)(dataGridViewConveyanceObjects[e.ColumnIndex, e.RowIndex].Value)).CompareTo("Ditch") == 0)
                    {
                        dataGridViewConveyanceObjects["shape_type_id", e.RowIndex].Value = 6;
                    }
                }
            }
            catch (Exception ex)
            {
                //string was null
            }
        }
    }
}
