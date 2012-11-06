using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace SWI_2
{
    public partial class SWIInputForm : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        private DataTable personalDataTable = new DataTable();

        SANDBOXDataSetTableAdapters.SWSP_MESH_NumberedTableAdapter sWSP_MESH_NUMBEREDTableAdapter = new SANDBOXDataSetTableAdapters.SWSP_MESH_NumberedTableAdapter();
        SANDBOXDataSetTableAdapters.View_PAGES_EVALUATORSTableAdapter view_PAGES_EVALUATORSTableAdapter = new SANDBOXDataSetTableAdapters.View_PAGES_EVALUATORSTableAdapter();
        SANDBOXDataSetTableAdapters.SWSP_PHOTOTableAdapter sWSP_PHOTOTableAdapter = new SANDBOXDataSetTableAdapters.SWSP_PHOTOTableAdapter();

        
        string newTableName = "";
        string newEvaluatorsTableName = "";
        string newPhotoTableName = "";
        string newPagesTableName = "";
        string newViewsTableName = "";
        string myConnectionString = "Data Source=SIRTOBY;Initial Catalog=SWI;Integrated Security=True";

        //Need to keep track of the user's added data
        int global_id_USER = -1;
        int photo_id_USER = -1;
        int page_id_USER = -1;
        int view_id_USER = -1;
        int databaseMaxGlobalID = -1;
        int databaseMaxPhotoID = -1;
        int databaseMaxPageID = -1;

        public SWIInputForm()
        {
            InitializeComponent();
        }

        private void SWIInputForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SHAPE_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_SHAPE_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SHAPE_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_MATERIAL_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_MATERIAL_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_MATERIAL_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_VIEW' table. You can move, or remove it, as needed.
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SUBWATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_WATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_EVALUATOR' table. You can move, or remove it, as needed.
            this.sWSP_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_EVALUATOR);
            this.sWSP_MESH_NUMBEREDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_MESH_Numbered);
            this.view_PAGES_EVALUATORSTableAdapter.Fill(this.sANDBOXDataSet.View_PAGES_EVALUATORS);
            this.sWSP_PHOTOTableAdapter.Fill(this.sANDBOXDataSet.SWSP_PHOTO);
            

            DoImport();
        }

        private void ultraButtonImportData_Click(object sender, EventArgs e)
        {
            DoImport();
        }

        private void DoImport()
        {
            string query = "SELECT MAX(global_id) FROM [SWI].[dbo].SWSP_GLOBAL_ID";
            databaseMaxGlobalID = ExecuteScalar(query, myConnectionString, databaseMaxGlobalID);
            query = "SELECT MAX(photo_id) FROM [SWI].[dbo].SWSP_PHOTO";
            databaseMaxPhotoID = ExecuteScalar(query, myConnectionString, databaseMaxPhotoID);
            query = "SELECT MAX(survey_page_id) FROM [SWI].[dbo].SWSP_SURVEY_PAGE";
            databaseMaxPageID = ExecuteScalar(query, myConnectionString, databaseMaxPageID);

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            /*This function should create a new table that is structurally similar to the
             *mesh view, and then import the data from the mesh view
             */
            //Need to ask what the user id is.  Tables made with this same user id
            //need to be deleted before we create a new table.  This is just the way it is.
            //Should also warn the user that a prior session is active and that continuing
            //will cause those changes to be deleted.
            query = "SELECT CAST(SYSTEM_USER AS varchar(128))";
            string userID = "";
            userID = ExecuteScalar(query, myConnectionString, userID);

            //Still need to get rid of the 'ROSE\' part of the user ID
            userID = userID.Substring(userID.IndexOf('\\')+1);

            newTableName = "Mesh_" + userID.ToString();
            newEvaluatorsTableName = "";
            newPhotoTableName = "";
            newPagesTableName = "";
            newViewsTableName = "";
            
            //There may be more than one table that the user has created and left abandoned.  Should select and delete them all
            query = "DECLARE @tableName  varchar(128) " +
            "DECLARE @cmd varchar(1000) " +
            "DECLARE cmds cursor for " +
            "SELECT 'DROP TABLE [' + Table_Name + ']' " +
            "FROM INFORMATION_SCHEMA.TABLES " +
            "WHERE Table_Name like '" +newTableName+"%' " +

            "open cmds " +
            "while 1=1 " +
            "begin " +
                "fetch cmds into @cmd " +
                "if @@fetch_status != 0 break " +
                "exec(@cmd) " +
            "end " +
            "close cmds " +
            "deallocate cmds ";

            ExecuteNonQuery(query, myConnectionString);

            //Need to ask the server what the sessionID is
            int sessionID = 0;
            query = "SELECT CAST(@@SPID AS int)";
            sessionID = ExecuteScalar(query, myConnectionString, sessionID);

            //Connect the SessionID to create the new table name.
            newTableName = newTableName + "_" + sessionID.ToString();
            newEvaluatorsTableName = newTableName + "_Evaluators";
            newPhotoTableName = newTableName + "_Photos";
            newPagesTableName = newTableName + "_Pages";
            newViewsTableName = newTableName + "_Views";

            //This is the query string for creating the new table.
            query = "CREATE TABLE [dbo].["+newTableName+"]( " +
	                " [global_id] [int] NOT NULL, "+
                    " [us_node] [char](10) NULL, " +
                    " [ds_node] [char](10) NULL, " +
                    " [LinkType] [varchar](7) NOT NULL, " +
                    " [node] [char](10) NULL, " +
                    " [shape_type_id] [int] NOT NULL, " +
                    " [dimension1] [float] NULL, " +
                    " [dimension2] [float] NULL, " +
                    " [dimension3] [float] NULL, " +
                    " [material_type_id] [int] NOT NULL, " +
                    " [culvert_opening_type_id] [int] NULL, " +
                    " [length_ft] [int] NULL, " +
                    " [us_depth_in] [float] NULL, " +
                    " [ds_depth_in] [float] NULL, " +
                    " [view_id] [int] NOT NULL, " +
                    " [survey_page_id] [int] NOT NULL, " +
                    " [watershed_id] [int] NOT NULL, " +
                    " [subwatershed_id] [int] NOT NULL, " +
                    " [action] [int] NULL " +
                    " )";
            ExecuteNonQuery(query, myConnectionString);

            //This is the query string for creating the new evaluators table.
            query = "CREATE TABLE [dbo].[" + newEvaluatorsTableName + "]( " +
                    " [survey_page_id] [int] NOT NULL, " +
                    " [evaluator_id] [int] NOT NULL, " +
                    " [initials] [char](3) NOT NULL, " +
                    " [Present] [int] NOT NULL " +
                    " )";
            ExecuteNonQuery(query, myConnectionString);

            //This is the query string for creating the new photos table.
            query = "CREATE TABLE [dbo].[" + newPhotoTableName + "]( " +
                    " [photo_id] [int] NOT NULL, " +
                    " [global_id] [int] NOT NULL, " +
                    " [location] [char](255) NULL, " +
                    " [comment] [varchar] (max) NULL " +
                    " )";
            ExecuteNonQuery(query, myConnectionString);

            //This is the query string for creating the new pages table.
            query = "CREATE TABLE [dbo].[" + newPagesTableName + "]( " +
                    " [survey_page_id] [int] NOT NULL, " +
                    " [view_id] [int] NOT NULL, " +
                    " [page_number] [int] NOT NULL, " +
                    " [date] [datetime] NULL, " +
                    " [weather] [varchar](50) NULL, " +
                    " [comment] [varchar](max) NULL " +
                    " )";
            ExecuteNonQuery(query, myConnectionString);

            //This is the query string for creating the new views table.
            query = "CREATE TABLE [dbo].[" + newViewsTableName + "]( " +
                    " [view_id] [int] NOT NULL, " +
                    " [subwatershed_id] [int] NOT NULL, " +
                    " [view_number] [int] NOT NULL, " +
                    " [description] [varchar] (100) NULL " +
                    " )";
            ExecuteNonQuery(query, myConnectionString);

            //Copy the Mesh view into the new table.
            query = "INSERT INTO [dbo].[" + newTableName + "]( " +
                    " [global_id], " +
                    " [us_node], " +
                    " [ds_node], " +
                    " [LinkType], " +
                    " [node], " +
                    " [shape_type_id], " +
                    " [dimension1], " +
                    " [dimension2], " +
                    " [dimension3], " +
                    " [material_type_id], " +
                    " [culvert_opening_type_id], " +
                    " [length_ft], " +
                    " [us_depth_in], " +
                    " [ds_depth_in], " +
                    " [view_id], " +
                    " [survey_page_id], " +
                    " [watershed_id], " +
                    " [subwatershed_id]" +
                    " ) SELECT " +
                    " [global_id], " +
                    " [us_node], " +
                    " [ds_node], " +
                    " [LinkType], " +
                    " [node], " +
                    " [shape_type_id], " +
                    " [dimension1], " +
                    " [dimension2], " +
                    " [dimension3], " +
                    " [material_type_id], " +
                    " [culvert_opening_type_id], " +
                    " [length_ft], " +
                    " [us_depth_in], " +
                    " [ds_depth_in], " +
                    " [view_id], " +
                    " [survey_page_id], " +
                    " [watershed_id], " +
                    " [subwatershed_id]" +
                    " FROM dbo.SWSP_MESH_Numbered";
            ExecuteNonQuery(query, myConnectionString);

            //Copy the evaluator view into the new table.
            query = "INSERT INTO [dbo].[" + newEvaluatorsTableName + "]( " +
                    " [survey_page_id] , " +
                    " [evaluator_id] , " +
                    " [initials] , " +
                    " [Present] " +
                    " ) SELECT " +
                    " [survey_page_id] , " +
                    " [evaluator_id] , " +
                    " [initials] , " +
                    " [Present] " +
                    " FROM dbo.View_PAGES_EVALUATORS";
            ExecuteNonQuery(query, myConnectionString);

            //Copy the photos table into the new table.
            query = "INSERT INTO [dbo].[" + newPhotoTableName + "]( " +
                    " [photo_id] , " +
                    " [global_id] , " +
                    " [location] , " +
                    " [comment] " +
                    " ) SELECT " +
                    " [photo_id] , " +
                    " [global_id] , " +
                    " [location] , " +
                    " [comment] " +
                    " FROM dbo.SWSP_PHOTO";
            ExecuteNonQuery(query, myConnectionString);

            //Copy the pages table into the new table.
            query = "INSERT INTO [dbo].[" + newPagesTableName + "]( " +
                    " [survey_page_id] , " +
                    " [view_id] , " +
                    " [page_number] , " +
                    " [date], " +
                    " [weather], " +
                    " [comment] " +
                    " ) SELECT " +
                    " [survey_page_id] , " +
                    " [view_id] , " +
                    " [page_number] , " +
                    " [date], " +
                    " [weather], " +
                    " [comment] " +
                    " FROM dbo.SWSP_SURVEY_PAGE";
            ExecuteNonQuery(query, myConnectionString);

            //Copy the views table into the new table.
            query = "INSERT INTO [dbo].[" + newViewsTableName + "]( " +
                    " [view_id] , " +
                    " [subwatershed_id] , " +
                    " [view_number] , " +
                    " [description] " +
                    " ) SELECT " +
                    " [view_id] , " +
                    " [subwatershed_id] , " +
                    " [view_number] , " +
                    " [description] " +
                    " FROM dbo.SWSP_VIEW";
            ExecuteNonQuery(query, myConnectionString);

            InitializeDataGridView(newTableName);
        }

        private int ExecuteScalar(string query, string myConnectionString, int returnValue)
        {
            using (SqlConnection connection = new SqlConnection(myConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    query, connection);
                connection.Open();
                command.CommandText = query;
                returnValue = (Int32)command.ExecuteScalar();
            }

            return returnValue;
        }

        private string ExecuteScalar(string query, string myConnectionString, string returnValue)
        {
            using (SqlConnection connection = new SqlConnection(myConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    query, connection);
                connection.Open();
                command.CommandText = query;
                returnValue = (String)command.ExecuteScalar();
            }

            return returnValue;
        }

        private int ExecuteNonQuery(string query, string myConnectionString)
        {
            int returnValue = 0;

            using (SqlConnection connection = new SqlConnection(myConnectionString))
            {
                SqlCommand command = new SqlCommand(
                    query, connection);
                connection.Open();
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
            return returnValue;
        }

        private void InitializeDataGridView(string newTableName)
        {
            try
            {
                sANDBOXDataSet.EditablePhotos.Clear();
                sANDBOXDataSet.EditablePAGES_EVALUATORS.Clear();
                sANDBOXDataSet.datatable1.Clear();
                sANDBOXDataSet.EditableSurveyPages.Clear();
                sANDBOXDataSet.EditableViews.Clear();

                this.sANDBOXDataSet.SWSP_VIEW.CopyToDataTable(sANDBOXDataSet.EditableViews, LoadOption.PreserveChanges);
                this.sANDBOXDataSet.SWSP_SURVEY_PAGE.CopyToDataTable(sANDBOXDataSet.EditableSurveyPages, LoadOption.PreserveChanges);
                this.sANDBOXDataSet.SWSP_MESH_Numbered.CopyToDataTable(sANDBOXDataSet.datatable1, LoadOption.PreserveChanges);
                this.sANDBOXDataSet.View_PAGES_EVALUATORS.CopyToDataTable(sANDBOXDataSet.EditablePAGES_EVALUATORS, LoadOption.PreserveChanges);
                this.sANDBOXDataSet.SWSP_PHOTO.CopyToDataTable(sANDBOXDataSet.EditablePhotos, LoadOption.PreserveChanges);
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to connect to database.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                System.Threading.Thread.CurrentThread.Abort();
            }
        }

        private static DataTable GetData(string sqlCommand)
        {
            string connectionString = "Data Source=SIRTOBY;Initial Catalog=SWI;Integrated Security=True";

            SqlConnection northwindConnection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(sqlCommand, northwindConnection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            adapter.Fill(table);

            return table;
        }

        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            this.fKSURVEYPAGEVIEWBindingSource.Position = this.comboBox4.SelectedIndex;
            this.sWSPSURVEYPAGEEditablePAGESEVALUATORSBindingSource.Position = this.comboBox4.SelectedIndex;
        }

        private void datatable1DataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //Global ID should be the result
            int returnValue = -1;
            string query = "INSERT INTO [SWI].[dbo].[SWSP_GLOBAL_ID] (description) OUTPUT inserted.global_id VALUES ('')";
            returnValue = ExecuteScalar(query, myConnectionString, returnValue);

            e.Row.Cells["globid"].Value = returnValue;
            e.Row.Cells["lType"].Value = "Pipe";
            e.Row.Cells["shaptype"].Value = 1;
            e.Row.Cells["mattype"].Value = 1;

            //the following numbers should be pulled directly from the selected datapositions.
            e.Row.Cells["vwid"].Value = ((DataRowView)this.sWSPSUBWATERSHEDEditableViewsBindingSource.Current)["view_id"];
            e.Row.Cells["survpagid"].Value = ((DataRowView)this.editableViewsEditableSurveyPagesBindingSource.Current)["survey_page_id"];
            e.Row.Cells["wtrshdid"].Value = ((DataRowView)this.sWSPWATERSHEDBindingSource.Current)["watershed_id"];
            e.Row.Cells["subwtrshd"].Value = ((DataRowView)this.fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"];
        }

        private void ConduitDefaultValuesNeeded(DataGridViewRow e)
        {
            
        }

        
        private void SWIInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*CurrencyManager cm;

            //if (this.ActiveControl == this.dataGridView1)
            //{
            try
            {
                //sometimes the user will not have a row selected when they close out.
                dataGridView1.EndEdit();/*.CurrentRow.DataGridView.EndEdit();
            }
            catch (Exception ex)
            {
            }

            dataGridView1.EndEdit();

            cm = (CurrencyManager)dataGridView1.BindingContext[dataGridView1.DataSource, dataGridView1.DataMember];
            cm.EndCurrentEdit();

            this.sWSPSURVEYPAGEEditablePAGESEVALUATORSBindingSource.Position = 0;

            if (dataGridView1.BindingContext[this.sWSPSURVEYPAGEEditablePAGESEVALUATORSBindingSource] != null)
            {
                dataGridView1.BindingContext[this.sWSPSURVEYPAGEEditablePAGESEVALUATORSBindingSource].EndCurrentEdit();
            }
            //}

            //if (this.ActiveControl == this.dataGridViewPhotos)
            //{
            try
            {
                //sometimes the user will not have a row selected when they close out.
                if (dataGridViewPhotos.RowCount > 0)
                {
                    dataGridViewPhotos.EndEdit();/*.CurrentRow.DataGridView.EndEdit();
                }
            }
            catch (Exception ex)
            {
            }

            dataGridViewPhotos.EndEdit();

            cm = (CurrencyManager)dataGridViewPhotos.BindingContext[dataGridViewPhotos.DataSource, dataGridViewPhotos.DataMember];
            cm.EndCurrentEdit();

            this.datatable1EditablePhotosBindingSource.Position = 0;

            if (dataGridViewPhotos.BindingContext[this.datatable1EditablePhotosBindingSource] != null)
            {
                dataGridViewPhotos.BindingContext[this.datatable1EditablePhotosBindingSource].EndCurrentEdit();
            }
            //}

            //if (this.ActiveControl == this.dataGridView2)
            //{
                try
                {
                    dataGridView2.EndEdit();/*.CurrentRow.DataGridView.EndEdit();
                }
                catch (Exception ex)
                {
                }

                dataGridView2.EndEdit();

                cm = (CurrencyManager)dataGridView2.BindingContext[dataGridView2.DataSource, dataGridView2.DataMember];
                cm.EndCurrentEdit();
           //}
            */
            if (!Environment.HasShutdownStarted)
            {
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

        private void translateChangesAndSaveToDatabase()
        {
            dataGridView1.EndEdit();
            dataGridView2.EndEdit();
            dataGridViewPhotos.EndEdit();
            this.Cursor = Cursors.WaitCursor;
            string query = "";
            //save all of the dataTables back into the database
            //clear the tables out...
            //Copy the evaluator view into the new table.
            query = "TRUNCATE TABLE [dbo].[" + newTableName + "]";
            ExecuteNonQuery(query, myConnectionString);
            query = "TRUNCATE TABLE [dbo].[" + newPhotoTableName + "]";
            ExecuteNonQuery(query, myConnectionString);
            query = "TRUNCATE TABLE [dbo].[" + newEvaluatorsTableName + "]";
            ExecuteNonQuery(query, myConnectionString);
            query = "TRUNCATE TABLE [dbo].[" + newViewsTableName + "]";
            ExecuteNonQuery(query, myConnectionString);
            query = "TRUNCATE TABLE [dbo].[" + newPagesTableName + "]";
            ExecuteNonQuery(query, myConnectionString);

            //compare the users personal database tables with the 
            //tables of record

            //For the conduits:
            try
            {
                query = "INSERT INTO " + newTableName + " ( [global_id], " +
                        " [us_node], " +
                        " [ds_node], " +
                        " [LinkType], " +
                        " [node], " +
                        " [shape_type_id], " +
                        " [dimension1], " +
                        " [dimension2], " +
                        " [dimension3], " +
                        " [material_type_id], " +
                        " [culvert_opening_type_id], " +
                        " [length_ft], " +
                        " [us_depth_in], " +
                        " [ds_depth_in], " +
                        " [view_id], " +
                        " [survey_page_id], " +
                        " [watershed_id], " +
                        " [subwatershed_id]) VALUES (@A, @B, @C, @D, @E, @F, @G, @H, @I, @J, @K, @L, @M, @N, @O, @P, @Q, @R)";
                using (SqlConnection conn = new SqlConnection(myConnectionString))
                {
                    conn.Open();
                    foreach (DataRow r in this.sANDBOXDataSet.datatable1.Rows)
                    {
                        if (r.RowState != DataRowState.Deleted)
                        {
                            SqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = query;
                            cmd.Parameters.AddWithValue("@A", r["global_id"]);
                            cmd.Parameters.AddWithValue("@B", r["us_node"]);
                            cmd.Parameters.AddWithValue("@C", r["ds_node"]);
                            cmd.Parameters.AddWithValue("@D", r["LinkType"]);
                            cmd.Parameters.AddWithValue("@E", r["node"]);
                            cmd.Parameters.AddWithValue("@F", r["shape_type_id"]);
                            cmd.Parameters.AddWithValue("@G", r["dimension1"]);
                            cmd.Parameters.AddWithValue("@H", r["dimension2"]);
                            cmd.Parameters.AddWithValue("@I", r["dimension3"]);
                            cmd.Parameters.AddWithValue("@J", r["material_type_id"]);
                            cmd.Parameters.AddWithValue("@K", r["culvert_opening_type_id"]);
                            cmd.Parameters.AddWithValue("@L", r["length_ft"]);
                            cmd.Parameters.AddWithValue("@M", r["us_depth_in"]);
                            cmd.Parameters.AddWithValue("@N", r["ds_depth_in"]);
                            cmd.Parameters.AddWithValue("@O", r["view_id"]);
                            cmd.Parameters.AddWithValue("@P", r["survey_page_id"]);
                            cmd.Parameters.AddWithValue("@Q", r["watershed_id"]);
                            cmd.Parameters.AddWithValue("@R", r["subwatershed_id"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Since we cannot save to the main table in the database, I suppose we will
                //have to save to a text file or csv.
                createDBLogs(ex.ToString());
                //Since we failed to transfer the records, we should just stop here
                //and keep all of our changes in our tables.  Get all of the
                //data that the user has input and find some way to keep it so that 
                //it can be checked and possibly restored.
                MessageBox.Show("Cannot save main table:" + ex.ToString());

                //We also have to end this attempt.  So...
                return;
            }

            //For the photos:
            try
            {
                query = "INSERT INTO " + newPhotoTableName + " ( [photo_id], " +
                        " [global_id], " +
                        " [location], " +
                        " [comment]) VALUES (@A, @B, @C, @D)";
                using (SqlConnection conn = new SqlConnection(myConnectionString))
                {
                    conn.Open();
                    foreach (DataRow r in this.sANDBOXDataSet.EditablePhotos.Rows)
                    {
                        if (r.RowState != DataRowState.Deleted)
                        {
                            SqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = query;
                            cmd.Parameters.AddWithValue("@A", r["photo_id"]);
                            cmd.Parameters.AddWithValue("@B", r["global_id"]);
                            cmd.Parameters.AddWithValue("@C", r["location"]);
                            cmd.Parameters.AddWithValue("@D", r["comment"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot save table:" + ex.ToString());
                
                return;
            }

            
            //For the evaluators:
            try
            {
                query = "INSERT INTO " + newEvaluatorsTableName + " ( [survey_page_id], " +
                        " [evaluator_id], " +
                        " [initials], " +
                        " [Present]) VALUES (@A, @B, @C, @D)";
                using (SqlConnection conn = new SqlConnection(myConnectionString))
                {
                    conn.Open();
                    foreach (DataRow r in this.sANDBOXDataSet.EditablePAGES_EVALUATORS.Rows)
                    {
                        if (r.RowState != DataRowState.Deleted)
                        {
                            SqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = query;
                            cmd.Parameters.AddWithValue("@A", r["survey_page_id"]);
                            cmd.Parameters.AddWithValue("@B", r["evaluator_id"]);
                            cmd.Parameters.AddWithValue("@C", r["initials"]);
                            cmd.Parameters.AddWithValue("@D", r["Present"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot save table:" + ex.ToString());
                
                return;
            }

            //For the views:
            try
            {
                query = "INSERT INTO " + newViewsTableName + " ( [view_id], " +
                        " [subwatershed_id], " +
                        " [view_number], " +
                        " [description]) VALUES (@A, @B, @C, @D)";
                using (SqlConnection conn = new SqlConnection(myConnectionString))
                {
                    conn.Open();
                    foreach (DataRow r in this.sANDBOXDataSet.EditableViews.Rows)
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@A", r["view_id"]);
                        cmd.Parameters.AddWithValue("@B", r["subwatershed_id"]);
                        cmd.Parameters.AddWithValue("@C", r["view_number"]);
                        cmd.Parameters.AddWithValue("@D", r["description"]);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot save table:" + ex.ToString());
                
                return;
            }

            //For the pages:
            try
            {
                query = "INSERT INTO " + newPagesTableName + " ( [survey_page_id], " +
                        " [view_id], " +
                        " [page_number], " +
                        " [date], " +
                        " [weather], " +
                        " [comment] ) VALUES (@A, @B, @C, @D, @E, @F)";
                using (SqlConnection conn = new SqlConnection(myConnectionString))
                {
                    conn.Open();
                    foreach (DataRow r in this.sANDBOXDataSet.EditableSurveyPages.Rows)
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@A", r["survey_page_id"]);
                        cmd.Parameters.AddWithValue("@B", r["view_id"]);
                        cmd.Parameters.AddWithValue("@C", r["page_number"]);
                        cmd.Parameters.AddWithValue("@D", r["date"]);
                        cmd.Parameters.AddWithValue("@E", r["weather"]);
                        cmd.Parameters.AddWithValue("@F", r["comment"]);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot save table:" + ex.ToString());
                
                return;
            }

            //then call the translate queries.
            //The translate queries take the data that is in the temporary tables and
            //places them into the permanent tables.
            //Translate old views that have updated descriptions
            //Any add view or page attempt should insert a new page or view into the database
            //as well as adding one to the editable table.
            //The difference is that the add to the official view or page table will simply 
            //add a table id and leave the rest of the columns blank.  These will be updated
            //once the user commits changes.
            try
            {
                query = "UPDATE	[SWI].[dbo].SWSP_VIEW " +
                " SET		[SWI].[dbo].SWSP_VIEW.description = [SWI].[dbo]." + newViewsTableName + ".description, " +
                            " [SWI].[dbo].SWSP_VIEW.subwatershed_id = [SWI].[dbo]." + newViewsTableName + ".subwatershed_id, " +
                            " [SWI].[dbo].SWSP_VIEW.view_number = [SWI].[dbo]." + newViewsTableName + ".view_number " +
                " FROM	[SWI].[dbo]." + newViewsTableName +
                        " INNER JOIN " +
                        " [SWI].[dbo].SWSP_VIEW " +
                        " ON	[SWI].[dbo]." + newViewsTableName + ".view_id = [SWI].[dbo].SWSP_VIEW.view_id ";

                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot update table:" + ex.ToString());
                
                return;
            }

            //Translate pages (modified)
            try
            {
                query = " UPDATE	[SWI].[dbo].SWSP_SURVEY_PAGE " +
                        " SET		[SWI].[dbo].SWSP_SURVEY_PAGE.page_number = [SWI].[dbo]." + newPagesTableName + ".page_number, " +
                                " [SWI].[dbo].SWSP_SURVEY_PAGE.[date] = [SWI].[dbo]." + newPagesTableName + ".[date], " +
                                " [SWI].[dbo].SWSP_SURVEY_PAGE.weather = [SWI].[dbo]." + newPagesTableName + ".weather, " +
                                " [SWI].[dbo].SWSP_SURVEY_PAGE.Comment = [SWI].[dbo]." + newPagesTableName + ".Comment " +
                        " FROM	[SWI].[dbo].SWSP_SURVEY_PAGE " +
                                " INNER JOIN " +
                                " [SWI].[dbo]." + newPagesTableName +
                                " ON	[SWI].[dbo].SWSP_SURVEY_PAGE.survey_page_id = [SWI].[dbo]." + newPagesTableName + ".survey_page_id ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot update table SWSP_SURVEY_PAGE:" + ex.ToString());
                
                return;
            }

            //Global IDs should be added to the global ID table EVERY TIME a new conduit is added.  This
            //means that global IDs should be added when the entry form needs default values.
            //Translate pipes (added)
            //Take the values in the Main mesh table and insert those that are pipes and are not yet
            //in the pipe table.
            try
            {
                query = " INSERT INTO [SWI].[dbo].SWSP_PIPE  " +
                        " (global_id, survey_page_id, us_node, ds_node, us_depth_in, ds_depth_in, inside_diam_in, inside_width_in, material,         shape,        comment, length_ft, node, unobstructed_height_in, pipe_opening) " +
                  " SELECT global_id, survey_page_id, us_node, ds_node, us_depth_in, ds_depth_in, dimension1,     dimension2,      material_type_id, shape_type_id, NULL,   length_ft, node, dimension3,             culvert_opening_type_id " +
                        " FROM [SWI].[dbo]." + newTableName +
                        " WHERE LinkType = 'Pipe' AND global_id not in (SELECT global_id FROM [SWI].[dbo].SWSP_PIPE) ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot insert into table SWSP_PIPE:" + ex.ToString());
                
                return;
            }

            //Translate pipes (deleted)
            //Delete from the pipe table the pipes that the user either transformed to culverts/ditches,
            //or simply deleted.  The problem here is finding out which pipes are newer than
            //the last pull we got from the database.  This can be determined by selecting the max global
            //id from the database when we pull from it.  Any number higher than this is an add from someone else,
            //and should stay, and any number lower than this is a delete, and should be removed.
            try
            {
                query = " DELETE " +
                        " FROM	[SWI].[dbo].SWSP_PIPE " +
                        " WHERE	global_id NOT IN (SELECT global_id FROM [SWI].[dbo]." + newTableName + ") AND global_id <= " + databaseMaxGlobalID.ToString();
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot delete from table SWSP_PIPE:" + ex.ToString());
                
                return;
            }

            //Translate pipes (modified)
            //for the pipes that were already in the pipes table, but have been modified through this program
            try
            {
                query = " UPDATE [SWI].[dbo].SWSP_PIPE  " +
                        " SET  survey_page_id = [SWI].[dbo]." + newTableName + ".survey_page_id, " +
                        "us_node = [SWI].[dbo]." + newTableName + ".us_node, " +
                        "ds_node = [SWI].[dbo]." + newTableName + ".ds_node, " +
                        "us_depth_in = [SWI].[dbo]." + newTableName + ".us_depth_in, " +
                        "ds_depth_in = [SWI].[dbo]." + newTableName + ".ds_depth_in, " +
                        "inside_diam_in = [SWI].[dbo]." + newTableName + ".dimension1, " +
                        "inside_width_in = [SWI].[dbo]." + newTableName + ".dimension2, " +
                        "material = [SWI].[dbo]." + newTableName + ".material_type_id, " +
                        "shape = [SWI].[dbo]." + newTableName + ".shape_type_id, " +
                        "length_ft = [SWI].[dbo]." + newTableName + ".length_ft, " +
                        "node = [SWI].[dbo]." + newTableName + ".node, " +
                        "unobstructed_height_in = [SWI].[dbo]." + newTableName + ".dimension3, " +
                        "pipe_opening = [SWI].[dbo]." + newTableName + ".culvert_opening_type_id " +
                        " FROM [SWI].[dbo]." + newTableName +
                        "       INNER JOIN  [SWI].[dbo].SWSP_PIPE ON [SWI].[dbo].SWSP_PIPE.global_id = [SWI].[dbo]." + newTableName + ".global_id ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot update table SWSP_PIPE:" + ex.ToString());
                
                return;
            }

            //Translate ditches (added)
            //Take the values in the Main mesh table and insert those that are ditches and are not yet
            //in the ditches table.
            try
            {
                query = " INSERT INTO [SWI].[dbo].SWSP_DITCH  " +
                        " (global_id, survey_page_id, us_node, ds_node, node, facing, depth_in,    top_width_in, bottom_width_in, material,         comment, length_ft) " +
                  " SELECT global_id, survey_page_id, us_node, ds_node, node, NULL,   dimension3,  dimension1,   dimension2,      material_type_id, NULL,    length_ft " +
                        " FROM [SWI].[dbo]." + newTableName +
                        " WHERE LinkType = 'Ditch' AND global_id not in (SELECT global_id FROM [SWI].[dbo].SWSP_DITCH) ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot insert into table SWSP_DITCH:" + ex.ToString());
                
                return;
            }

            //Translate ditches (deleted)
            //Delete from the ditches table the pipes that the user either transformed to culverts/pipes,
            //or simply deleted.  
            try
            {
                query = " DELETE " +
                        " FROM	[SWI].[dbo].SWSP_DITCH " +
                        " WHERE	global_id NOT IN (SELECT global_id FROM [SWI].[dbo]." + newTableName + ") AND global_id <= " + databaseMaxGlobalID.ToString();
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot delete from table SWSP_DITCH:" + ex.ToString());
                
                return;
            }

            //Translate ditches (modified)
            //for the ditches that were already in the ditches table, but have been modified through this program
            try
            {
                query = " UPDATE [SWI].[dbo].SWSP_DITCH  " +
                        " SET  survey_page_id = [SWI].[dbo]." + newTableName + ".survey_page_id,  " +
                        " us_node = [SWI].[dbo]." + newTableName + ".us_node,  " +
                        " ds_node = [SWI].[dbo]." + newTableName + ".ds_node, " +
                        " node = [SWI].[dbo]." + newTableName + ".node, " +
                        " depth_in = [SWI].[dbo]." + newTableName + ".dimension3, " +
                        " top_width_in = [SWI].[dbo]." + newTableName + ".dimension1, " +
                        " bottom_width_in = [SWI].[dbo]." + newTableName + ".dimension2, " +
                        " material = [SWI].[dbo]." + newTableName + ".material_type_id, " +
                        " length_ft = [SWI].[dbo]." + newTableName + ".length_ft " +
                        " FROM [SWI].[dbo]." + newTableName +
                        "       INNER JOIN  [SWI].[dbo].SWSP_DITCH ON [SWI].[dbo].SWSP_DITCH.global_id = [SWI].[dbo]." + newTableName + ".global_id ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot update table SWSP_DITCH:" + ex.ToString());
                
                return;
            }

            //Translate culverts (added)
            try
            {
                query = " INSERT INTO [SWI].[dbo].SWSP_CULVERT  " +
                        " (global_id, survey_page_id, us_node, ds_node, node, facing,                  shape,          full_diam_in, full_width_in, unobstructed_height_in, material,         comment, length_ft, us_depth_in, ds_depth_in) " +
                  " SELECT global_id, survey_page_id, us_node, ds_node, node, culvert_opening_type_id, shape_type_id,  dimension1,   dimension2,    dimension3,             material_type_id, NULL,    length_ft, us_depth_in, ds_depth_in " +
                        " FROM [SWI].[dbo]." + newTableName +
                        " WHERE LinkType = 'Culvert' AND global_id not in (SELECT global_id FROM [SWI].[dbo].SWSP_CULVERT) ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot insert into table SWSP_CULVERT:" + ex.ToString());
                
                return;
            }

            //Translate culverts (deleted)
            try
            {
                query = " DELETE " +
                        " FROM	[SWI].[dbo].SWSP_CULVERT " +
                        " WHERE	global_id NOT IN (SELECT global_id FROM [SWI].[dbo]." + newTableName + ") AND global_id <= " + databaseMaxGlobalID.ToString();

                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot delete from table SWSP_CULVERT:" + ex.ToString());
                
                return;
            }

            //Translate culverts (modified)
            try
            {
                query = " UPDATE [SWI].[dbo].SWSP_CULVERT  " +
                        " SET  survey_page_id = [SWI].[dbo]." + newTableName + ".survey_page_id, " +
                        "us_node = [SWI].[dbo]." + newTableName + ".us_node, " +
                        "ds_node = [SWI].[dbo]." + newTableName + ".ds_node, " +
                        "node = [SWI].[dbo]." + newTableName + ".node, " +
                        "culvert_opening = [SWI].[dbo]." + newTableName + ".culvert_opening_type_id, " +
                        "shape = [SWI].[dbo]." + newTableName + ".shape_type_id, " +
                        "full_diam_in = [SWI].[dbo]." + newTableName + ".dimension1, " +
                        "full_width_in = [SWI].[dbo]." + newTableName + ".dimension2, " +
                        "unobstructed_height_in = [SWI].[dbo]." + newTableName + ".dimension3, " +
                        "material = [SWI].[dbo]." + newTableName + ".material_type_id, " +
                        "length_ft = [SWI].[dbo]." + newTableName + ".length_ft, " +
                        "us_depth_in = [SWI].[dbo]." + newTableName + ".us_depth_in, " +
                        "ds_depth_in = [SWI].[dbo]." + newTableName + ".ds_depth_in " +
                        " FROM [SWI].[dbo]." + newTableName +
                        "       INNER JOIN  [SWI].[dbo].SWSP_CULVERT ON [SWI].[dbo].SWSP_CULVERT.global_id = [SWI].[dbo]." + newTableName + ".global_id ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot update table SWSP_CULVERT:" + ex.ToString());
                
                return;
            }

            //Translate the photos (added)
            try
            {
                query = " INSERT INTO [SWI].[dbo].SWSP_PHOTO  " +
                        " (global_id, location, comment) " +
                  " SELECT global_id, location, comment  " +
                        " FROM [SWI].[dbo]." + newPhotoTableName +
                        " WHERE photo_id not in (SELECT photo_id FROM [SWI].[dbo].SWSP_PHOTO) ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot insert into table SWSP_PHOTO:" + ex.ToString());
                
                return;
            }

            //Translate photos (deleted)
            //photos that belong to pipes that have a lower global_id than the databaseMaxGlobalID
            //AND do not have matching global_ids in MESH should be deleted
            try
            {
                query = " DELETE " +
                        " FROM	[SWI].[dbo].SWSP_PHOTO " +
                        " WHERE	photo_id NOT IN (SELECT photo_id FROM [SWI].[dbo]." + newPhotoTableName + ") AND (photo_id <= " + databaseMaxPhotoID.ToString() + " )";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot delete from table SWSP_PHOTO:" + ex.ToString());
                
                return;
            }

            //Translate photos (modified)
            try
            {
                query = " UPDATE [SWI].[dbo].SWSP_PHOTO  " +
                        " SET  global_id = [SWI].[dbo]." + newPhotoTableName + ".global_id ,  " +
                        " location = [SWI].[dbo]." + newPhotoTableName + ".location,  " +
                        " comment = [SWI].[dbo]." + newPhotoTableName + ".comment " +
                        " FROM [SWI].[dbo]." + newPhotoTableName +
                        "       INNER JOIN  [SWI].[dbo].SWSP_PHOTO ON [SWI].[dbo].SWSP_PHOTO.photo_id = [SWI].[dbo]." + newPhotoTableName + ".photo_id ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot update table SWSP_PHOTO:" + ex.ToString());
                
                return;
            }

            //Translate the evaluators (deleted)
            //Cannot simply delete everything and rewrite it because other people might have already added/deleted
            //entire pages in the database.  Have to create unique page ids for new pages and call a maxpageid funciton
            //similar to the maxglobalID function, then you could probably get away with deleting everything below that maxpageID
            //value and re-inserting everything
            try
            {
                query = " DELETE " +
                        " FROM	[SWI].[dbo].[SWSP_SURVEY_PAGE_EVALUATOR] " +
                        " WHERE	survey_page_id <= " + databaseMaxPageID.ToString() + " ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot delete from table SWSP_SURVEY_PAGE_EVALUATOR:" + ex.ToString());
                
                return;
            }

            try
            {
                query = " INSERT INTO [SWI].[dbo].[SWSP_SURVEY_PAGE_EVALUATOR]  " +
                        " (survey_page_id, evaluator_id) " +
                  " SELECT survey_page_id, evaluator_id  " +
                        " FROM [SWI].[dbo]." + newEvaluatorsTableName +
                        " WHERE [SWI].[dbo]." + newEvaluatorsTableName + ".Present <> 0 ";
                ExecuteNonQuery(query, myConnectionString);
            }
            catch (Exception ex)
            {
                createDBLogs(ex.ToString());
                MessageBox.Show("Cannot insert into table SWSP_SURVEY_PAGE_EVALUATOR:" + ex.ToString());
                return;
            }

            DoImport();

            //Give the user an indication that the saves are done.
            this.Cursor = Cursors.Default;
        }

        private void createDBLogs(string theProblem)
        {
            CreateCSVFile((DataTable)this.sANDBOXDataSet.EditablePAGES_EVALUATORS, "C:\\TEMP\\" + this.sANDBOXDataSet.EditablePAGES_EVALUATORS.TableName + "_" + DateTime.Now.ToShortTimeString() + ".xls");
            CreateCSVFile((DataTable)this.sANDBOXDataSet.EditableSurveyPages, "C:\\TEMP\\" + this.sANDBOXDataSet.EditableSurveyPages.TableName + "_" + DateTime.Now.ToShortTimeString() + ".xls");
            CreateCSVFile((DataTable)this.sANDBOXDataSet.EditableViews, "C:\\TEMP\\" + this.sANDBOXDataSet.EditableViews.TableName + "_" + DateTime.Now.ToShortTimeString() + ".xls");
            CreateCSVFile((DataTable)this.sANDBOXDataSet.datatable1, "C:\\TEMP\\" + this.sANDBOXDataSet.datatable1.TableName + "_" + DateTime.Now.ToShortTimeString() + ".xls");
            CreateCSVFile((DataTable)this.sANDBOXDataSet.EditablePhotos, "C:\\TEMP\\" + this.sANDBOXDataSet.EditablePhotos.TableName + "_"+ DateTime.Now.ToShortTimeString()+".xls");

            StreamWriter sw = new StreamWriter("C:\\TEMP\\" + DateTime.Now.ToShortTimeString() + ".txt", false);
            sw.Write(theProblem);

            DoImport();
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            if (dataGridView1.BindingContext[this.sWSPSURVEYPAGEEditablePAGESEVALUATORSBindingSource] != null)
            {
                dataGridView1.BindingContext[this.sWSPSURVEYPAGEEditablePAGESEVALUATORSBindingSource].EndCurrentEdit();
            }
        }

        private void ultraButtonSaveData_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewPhotos_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //Global ID should be the result
            int returnValue = -1;
            string query = "INSERT INTO [SWI].[dbo].[SWSP_PHOTO] (global_id) OUTPUT inserted.photo_id VALUES (" + ((DataRowView)this.editableSurveyPagesdatatable1BindingSource.Current)["global_id"] + ")";
            returnValue = ExecuteScalar(query, myConnectionString, returnValue);

            e.Row.Cells["photoid"].Value = returnValue;
        }

        private void dataGridView2_Leave(object sender, EventArgs e)
        {
            if (dataGridView2.BindingContext[this.dataTable1BindingSource] != null)
            {
                dataGridView2.BindingContext[this.dataTable1BindingSource].EndCurrentEdit();
            }
        }

        private void dataGridViewPhotos_Leave(object sender, EventArgs e)
        {
            if (dataGridViewPhotos.BindingContext[this.datatable1EditablePhotosBindingSource] != null)
            {
                dataGridViewPhotos.BindingContext[this.datatable1EditablePhotosBindingSource].EndCurrentEdit();
            }
        }

        private void textBoxComments_Leave_1(object sender, EventArgs e)
        {
            if (dataGridView2.BindingContext[this.dataTable1BindingSource] != null)
            {
                dataGridView2.BindingContext[this.dataTable1BindingSource].EndCurrentEdit();
            }
        }

        private void reloadDataDiscardChangesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult theAnswer = MessageBox.Show("Do you wish to discard changes to the database?", "Discard Changes", MessageBoxButtons.YesNo);
            if (theAnswer == DialogResult.No)
            {
                //do nothing
            }
            else if (theAnswer == DialogResult.Yes)
            {
                DoImport();
            }
        }

        private void saveChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            translateChangesAndSaveToDatabase();
        }

        private void newPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //get the page the user wants to insert
            FormAddNewSurveyView child = new FormAddNewSurveyView();

            this.Enabled = false;
            child.ShowDialog();
            try
            {
                int myNumber = child.GetNumber();
                int isTaken = 1;

                //if that page number is available, then add
                //the new page number to the database.
                //if it is not available, tell the user.
                //survey_page_id ID should be the result
                string query = "SELECT COUNT(*) FROM [SWI].[dbo].[SWSP_SURVEY_PAGE] WHERE page_number = " + myNumber.ToString() + " AND view_id = " + ((DataRowView)this.sWSPSUBWATERSHEDEditableViewsBindingSource.Current)["view_id"].ToString();
                isTaken = ExecuteScalar(query, myConnectionString, isTaken);

                if (isTaken == 0)
                {
                    int returnValue = -1;
                    query = "INSERT INTO [SWI].[dbo].[SWSP_SURVEY_PAGE] (view_id, page_number, date, weather, comment) OUTPUT inserted.survey_page_id VALUES (" + ((DataRowView)this.sWSPSUBWATERSHEDEditableViewsBindingSource.Current)["view_id"].ToString() + ", " + myNumber.ToString() + ", GETDATE(), null, null)";
                    returnValue = ExecuteScalar(query, myConnectionString, returnValue);

                    //Insert the new page into the users table set.
                    SANDBOXDataSet.EditableSurveyPagesRow row = this.sANDBOXDataSet.EditableSurveyPages.NewEditableSurveyPagesRow();
                    row["survey_page_id"] = returnValue;
                    row["view_id"] = ((DataRowView)this.sWSPSUBWATERSHEDEditableViewsBindingSource.Current)["view_id"];
                    row["page_number"] = myNumber;
                    row["weather"] = "";
                    row["comment"]= "";
                    this.sANDBOXDataSet.EditableSurveyPages.AddEditableSurveyPagesRow(row);

                    newConduitRowNeeded(returnValue, (int)((DataRowView)this.sWSPSUBWATERSHEDEditableViewsBindingSource.Current)["view_id"]);
                    newEvaluatorsDataNeeded(returnValue);

                    this.comboBox4.SelectedValue = returnValue;
                }
                else
                {
                    MessageBox.Show("Page " + myNumber.ToString() + " is already taken for this view!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not insert new page");
            }
            this.Enabled = true;
        }

        private void newConduitRowNeeded(int pageID, int viewID)
        {
            //Global ID should be the result
            int returnValue = -1;
            string query = "INSERT INTO [SWI].[dbo].[SWSP_GLOBAL_ID] (description) OUTPUT inserted.global_id VALUES ('')";
            returnValue = ExecuteScalar(query, myConnectionString, returnValue);

            SANDBOXDataSet.datatable1Row row = this.sANDBOXDataSet.datatable1.Newdatatable1Row();
            row["global_id"] = returnValue;
            row["LinkType"] = "Pipe";
            row["shape_type_id"] = 1;
            row["material_type_id"] = 1;

            //the following numbers should be pulled directly from the selected datapositions.
            row["view_id"] = viewID;
            row["survey_page_id"] = pageID;
            row["watershed_id"] = ((DataRowView)this.sWSPWATERSHEDBindingSource.Current)["watershed_id"];
            row["subwatershed_id"] = ((DataRowView)this.fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"];
            this.sANDBOXDataSet.datatable1.Adddatatable1Row(row);

            newPhotoRowNeeded(returnValue);
        }

        private void newPhotoRowNeeded(int globalID)
        {
            int returnValue = -1;
            string query = "INSERT INTO [SWI].[dbo].[SWSP_PHOTO] (global_id) OUTPUT inserted.photo_id VALUES (" + globalID.ToString() + ")";
            returnValue = ExecuteScalar(query, myConnectionString, returnValue);

            SANDBOXDataSet.EditablePhotosRow row = this.sANDBOXDataSet.EditablePhotos.NewEditablePhotosRow();
            row["photo_id"] = returnValue;
            row["global_id"] = globalID;
            row["location"] = "";
            row["comment"] = "";

            this.sANDBOXDataSet.EditablePhotos.AddEditablePhotosRow(row);
        }

        private void newEvaluatorsDataNeeded(int page_id)
        {
            //For every evaluator that exists in SWSP_EVALUATOR, place a new evaluator in the evaluators table
            foreach (DataRow r in this.sANDBOXDataSet.SWSP_EVALUATOR)
            {
                SANDBOXDataSet.EditablePAGES_EVALUATORSRow row = this.sANDBOXDataSet.EditablePAGES_EVALUATORS.NewEditablePAGES_EVALUATORSRow();
                row["survey_page_id"] = page_id;
                row["evaluator_id"] = r["evaluator_id"];
                row["initials"] = r["initials"];
                row["Present"] = 0;

                this.sANDBOXDataSet.EditablePAGES_EVALUATORS.AddEditablePAGES_EVALUATORSRow(row);
            }
        }

        private void newViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //get the page the user wants to insert
            FormAddNewMap child = new FormAddNewMap();

            this.Enabled = false;
            child.ShowDialog();
            try
            {
                int myNumber = child.GetNumber();
                int isTaken = 1;

                //if that page number is available, then add
                //the new page number to the database.
                //if it is not available, tell the user.
                //survey_page_id ID should be the result
                string query = "SELECT COUNT(*) FROM [SWI].[dbo].[SWSP_VIEW] WHERE view_number = " + myNumber.ToString() + " AND subwatershed_id = " + ((DataRowView)this.sWSPSUBWATERSHEDEditableViewsBindingSource.Current)["subwatershed_id"].ToString();
                isTaken = ExecuteScalar(query, myConnectionString, isTaken);

                if (isTaken == 0)
                {
                    int returnValue = -1;
                    query = "INSERT INTO [SWI].[dbo].[SWSP_VIEW] (subwatershed_id, view_number, description) OUTPUT inserted.view_id VALUES (" + ((DataRowView)this.sWSPSUBWATERSHEDEditableViewsBindingSource.Current)["subwatershed_id"].ToString() + ", " + myNumber.ToString() + ", null)";
                    returnValue = ExecuteScalar(query, myConnectionString, returnValue);

                    //Insert the new page into the users table set.
                    SANDBOXDataSet.EditableViewsRow row = this.sANDBOXDataSet.EditableViews.NewEditableViewsRow();
                    row["view_id"] = returnValue;
                    row["subwatershed_id"] = ((DataRowView)this.sWSPSUBWATERSHEDEditableViewsBindingSource.Current)["subwatershed_id"];
                    row["view_number"] = myNumber;
                    this.sANDBOXDataSet.EditableViews.AddEditableViewsRow(row);
                }
                else
                {
                    MessageBox.Show("View " + myNumber.ToString() + " is already taken for this view!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not insert new view");
            }
            this.Enabled = true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.BindingContext[this.sWSPSURVEYPAGEEditablePAGESEVALUATORSBindingSource] != null)
                {
                    dataGridView1.BindingContext[this.sWSPSURVEYPAGEEditablePAGESEVALUATORSBindingSource].EndCurrentEdit();
                }
            }
            catch (Exception ex)
            {
                //it is possible we are simply starting up the form, or creating new views
                //This is bound to throw this exception quite a lot.
                //Find the flags those actions set and use them here instead.
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.EndEdit();
        }

        public void CreateCSVFile(DataTable dt, string strFilePath)
        {
            try
            {
                // Create the CSV file to which grid data will be exported.
                StreamWriter sw = new StreamWriter(strFilePath, false);
                // First we will write the headers.
                int iColCount = dt.Columns.Count;
                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);

                // Now write all the rows.

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write(dr[i].ToString());
                        }
                        if (i < iColCount - 1)
                        {
                            sw.Write(",");
                        }
                    }

                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
