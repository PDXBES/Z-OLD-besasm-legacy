using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.SqlServerCe;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer;
using System.Configuration.Assemblies;
using Microsoft.Office;

namespace DSCUpdater
{
  public partial class frmMain : Form
  {
    public int newRoofDrywellArea = 0;
    public int newRoofDISCOICArea = 0;
    public int newParkDrywellICArea = 0;
    public int newParkDISCOICArea = 0;
    public int newParkICArea = 0;
    public int newRoofICArea = 0;
    public int newRoofArea = 0;
    public int newParkArea = 0;
    public int selectedIndex = 0;
    public int impAQCCounter = 0;

    SqlDataAdapter daUpdaterEditor;
    DataTable dtUpdaterEditor;

    char[] splitArray1 = { ',', '\n', '\r' };
    char[] splitArray2 = { ' ' };

    Int64[] SearchList = new long[1];

    string csvDataSource;
    string fileName;
    string FileSize;
    //string LineText;
    string tempFileName;

    // Use to populate the grid. 
    string[] DataResult1 = { "", "", "", "", "", "", "", "" };
    string[] Titles = { "RNO", "DSCID", "New Roof Area", "New Roof DISCO IC Area", "New Roof Drywell IC Area" };

    //Create a dataset  
    DataSet dataset = new DataSet("My Dataset");
    //Create a table
    DataTable datatable = new DataTable("Temp.CSV");

    private enum DscErrors
    {
      PendingUpdate, DscNotFound, ParkICGreaterThanParkArea, RoofICGreaterThanRoofArea
    }

    public frmMain()
    {
      InitializeComponent();

      tempFileName = @"C:\Temp.csv";
      this.ultraStatusBar1.Panels["status"].Text = "Ready";

      //CreateTable();
      dataset.Tables.Add(datatable);
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      try
      {
        ultraStatusBar1.Panels["dscEditorConnection"].Text = "Dsc Editor: " + ConnectionStringSummary(Properties.Settings.Default.DscEditorConnectionString);
        ultraStatusBar1.Panels["masterDataConnection"].Text = "Master Data: " + ConnectionStringSummary(Properties.Settings.Default.MasterDataConnectionString);

        // TODO: This line of code loads data into the 'projectDataSet.DSCEDIT' table. You can move, or remove it, as needed.
        projectDataSet.EnforceConstraints = false;
        //this.dSCEDITTableAdapter.Fill(this.projectDataSet.DSCEDIT);
        // TODO: This line of code loads data into the 'projectDataSet.SESSION' table. You can move, or remove it, as needed.
        this.sESSIONTableAdapter.Fill(this.projectDataSet.SESSION);
        bindingNavigator1.BindingSource = sESSIONBindingSource;

        SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.DscEditorConnectionString);
        sqlCon.Open();
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "DELETE FROM USERUPDATE";
        sqlCmd.Connection = sqlCon;
        sqlCmd.ExecuteNonQuery();
      }
      catch (SqlException sqlException)
      {
        MessageBox.Show(sqlException.Message + "\n" + "Please specify server and database connection parameters on the Database Connection Options tab.", "DSCUpdater: SQL Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
        UpdateDscEditorConnection();
      }
    }

    private string columnNames(DataTable dtSchemaTable, string delimiter)
    {
      string strOut = "";
      if (delimiter.ToLower() == "tab")
      {
        delimiter = "\t";
      }

      for (int i = 0; i < dtSchemaTable.Rows.Count; i++)
      {
        strOut += dtSchemaTable.Rows[i][0].ToString();
        if (i < dtSchemaTable.Rows.Count - 1)
        {
          strOut += delimiter;
        }
      }
      return strOut;
    }

    private void ExportIMPUPDATEToCSV()
    {
      string fileOut = "C:\\temp\\IMPUPDATE.csv";
      SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.DscEditorConnectionString);
      string sqlQuery = "SELECT * FROM IMPUPDATE";
      SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
      sqlCon.Open();

      // Creates a SqlDataReader instance to read data from the table.
      SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();

      // Retrives the schema of the table.
      DataTable dtSchema = sqlDataReader.GetSchemaTable();

      // Creates the CSV file as a stream, using the given encoding.
      StreamWriter sw = new StreamWriter(fileOut, false, Encoding.ASCII);

      // represents a full row
      string strRow;

      // Writes the column headers     
      sw.WriteLine(columnNames(dtSchema, ","));

      // Reads the rows one by one from the SqlDataReader
      // transfers them to a string with the given separator character and
      // writes it to the file.
      while (sqlDataReader.Read())
      {
        strRow = "";
        for (int i = 0; i < sqlDataReader.FieldCount; i++)
        {
          strRow += sqlDataReader.GetInt32(i);
          if (i < sqlDataReader.FieldCount - 1)
          {
            strRow += ",";
          }
        }
        sw.WriteLine(strRow);
      }

      // Closes the text stream and the database connenction.
      sw.Close();
      sqlCon.Close();
    }

    private void CreateTable()
    {
      for (int i = 0; i < 8; i++)
      {
        datatable.Columns.Add(Titles[i]);
      }
    }

    private void ReadData()
    {
      string tempPath = "C:";
      string strCon = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + tempPath + @"\;Extensions=asc,csv,tab,txt";
      OdbcConnection con = new OdbcConnection(strCon);
      OdbcDataAdapter da = new OdbcDataAdapter("Select [RNO],[DSCID],[New Roof Area], [New Roof DISCO IC Area],[New Roof Drywell IC Area], [New Park Area], [New Park DISCO IC Area], [New Park Drywell IC Area] from temp.csv", con);
      DataTable dt1 = new DataTable();
      da.Fill(dt1);

      dgvData.DataSource = dt1;
      dgvData.Columns[0].Width = 75;
      dgvData.Columns[1].Width = 75;
      dgvData.Columns[2].Width = 50;
      dgvData.Columns[3].Width = 50;
      dgvData.Columns[4].Width = 50;
      dgvData.Columns[5].Width = 50;
      dgvData.Columns[6].Width = 50;
      dgvData.Columns[7].Width = 50;
      dgvData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      dgvData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      dgvData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      dgvData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      dgvData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      dgvData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      dgvData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      dgvData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      dgvData.Columns[0].DefaultCellStyle.Format = "T";

      foreach (DataGridViewColumn col in dgvData.Columns)
      {
        col.SortMode = DataGridViewColumnSortMode.NotSortable;
      }
    }

    private static void AddEditDateCommandParameter(SqlCommand sqlCmd, SqlConnection sqlCon)
    {
      //add editDate parameter to SQL command sqlCmd
      DateTime editDate = DateTime.Now;
      SqlParameter pEditDate = sqlCmd.CreateParameter();
      pEditDate.ParameterName = "@editDate";
      pEditDate.SqlDbType = SqlDbType.DateTime;
      pEditDate.Value = editDate;
      pEditDate.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pEditDate);
      sqlCmd.CommandTimeout = 300;
      sqlCmd.Connection = sqlCon;
      if (sqlCon.State == ConnectionState.Closed)
      {
        sqlCon.Open();
      }
    }

    private static void AddEditedByCommandParameter(SqlCommand sqlCmd, SqlConnection sqlCon)
    {
      //add editedBy parameter to SQL command cmd
      string editedBy;
      editedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
      SqlParameter pEditedBy = sqlCmd.CreateParameter();
      pEditedBy.ParameterName = "@editedBy";
      pEditedBy.SqlDbType = SqlDbType.VarChar;
      pEditedBy.Size = 50;
      pEditedBy.Value = editedBy;
      pEditedBy.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pEditedBy);
      sqlCmd.CommandTimeout = 300;
      sqlCmd.Connection = sqlCon;
      if (sqlCon.State == ConnectionState.Closed)
      {
        sqlCon.Open();
      }
    }

    private void AddEditIDCommandParameter(SqlCommand sqlCmd)
    {
      //add editID parameter to SQL command   
      int editID;
      sqlCmd.CommandText = "SELECT Max(edit_id) FROM SESSION";
      if (sqlCmd.ExecuteScalar() is DBNull)
      {
        editID = 1;
        SqlParameter pEditID = sqlCmd.CreateParameter();
        pEditID.ParameterName = "@editID";
        pEditID.SqlDbType = SqlDbType.Int;
        pEditID.Value = editID;
        pEditID.Direction = ParameterDirection.Input;
        sqlCmd.Parameters.Add(pEditID);
      }
      else
      {
        editID = Convert.ToInt32(sqlCmd.ExecuteScalar());
        SqlParameter pEditID = sqlCmd.CreateParameter();
        pEditID.ParameterName = "@editID";
        pEditID.SqlDbType = SqlDbType.Int;
        pEditID.Value = editID + 1;
        pEditID.Direction = ParameterDirection.Input;
        sqlCmd.Parameters.Add(pEditID);
      }
    }

    public void AddSESSIONEditIDCommandParameter(SqlCommand sqlCmd)
    {
      int sessionEditID = 0;
      SqlParameter pSessionEditID = sqlCmd.CreateParameter();
      pSessionEditID.ParameterName = "@sessionEditID";
      pSessionEditID.SqlDbType = SqlDbType.Int;
      pSessionEditID.Value = Convert.ToInt32(dgvUpdaterHistory.SelectedCells[0].Value);
      sessionEditID = Convert.ToInt32(pSessionEditID.Value);
      pSessionEditID.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pSessionEditID);
    }

    public void AddEditorEditIDCommandParameter(SqlCommand sqlCmd)
    {
      int editorEditID = 0;
      SqlParameter pEditorEditID = sqlCmd.CreateParameter();
      dgvUpdaterEditor.Rows[0].Selected = true;
      editorEditID = Convert.ToInt32(dgvUpdaterEditor.SelectedCells[1].Value);
      pEditorEditID.ParameterName = "@editorEditID";
      pEditorEditID.SqlDbType = SqlDbType.Int;
      pEditorEditID.Value = editorEditID;
      pEditorEditID.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pEditorEditID);
    }

    private void AddNewParkAreaCommandParameter(SqlCommand sqlCmd, SqlConnection sqlCon)
    {
      //add newParkArea parameter to SQL command cmd
      SqlParameter pNewParkArea = sqlCmd.CreateParameter();
      pNewParkArea.ParameterName = "@newParkArea";
      pNewParkArea.SqlDbType = SqlDbType.Int;
      pNewParkArea.Value = newParkArea;
      pNewParkArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pNewParkArea);
      //sqlCmd.CommandTimeout = 300;
      //sqlCmd.Connection = sqlCon;
      //sqlCon.Open();
    }

    private void AddNewParkDISCOAreaCommandParameter(SqlCommand sqlCmd, SqlConnection sqlCon)
    {
      //add newParkDISCOArea parameter to SQL command cmd
      SqlParameter pNewParkDISCOArea = sqlCmd.CreateParameter();
      pNewParkDISCOArea.ParameterName = "@newParkDISCOArea";
      pNewParkDISCOArea.SqlDbType = SqlDbType.Int;
      pNewParkDISCOArea.Value = newParkDISCOICArea;
      pNewParkDISCOArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pNewParkDISCOArea);
      //sqlCmd.CommandTimeout = 300;
      //sqlCmd.Connection = sqlCon;
      //sqlCon.Open();
    }

    private void AddNewParkAreaDrywellCommandParameter(SqlCommand sqlCmd, SqlConnection sqlCon)
    {
      //add newParkDrywellArea parameter to SQL command cmd
      SqlParameter pNewParkDrywellArea = sqlCmd.CreateParameter();
      pNewParkDrywellArea.ParameterName = "@newParkDrywellArea";
      pNewParkDrywellArea.SqlDbType = SqlDbType.Int;
      pNewParkDrywellArea.Value = newParkDrywellICArea;
      pNewParkDrywellArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pNewParkDrywellArea);
      //sqlCmd.CommandTimeout = 300;
      //sqlCmd.Connection = sqlCon;
      //sqlCon.Open();
    }

    private void AddNewRoofAreaCommandParameter(SqlCommand sqlCmd, SqlConnection sqlCon)
    {
      //add newRoofArea parameter to SQL command cmd
      SqlParameter pNewRoofArea = sqlCmd.CreateParameter();
      pNewRoofArea.ParameterName = "@newRoofkArea";
      pNewRoofArea.SqlDbType = SqlDbType.Int;
      pNewRoofArea.Value = newRoofArea;
      pNewRoofArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pNewRoofArea);
      //sqlCmd.CommandTimeout = 300;
      //sqlCmd.Connection = sqlCon;
      //sqlCon.Open();
    }

    private void AddNewRoofDISCOAreaCommandParameter(SqlCommand sqlCmd, SqlConnection sqlCon)
    {
      //add newRoofDISCOArea parameter to SQL command cmd
      SqlParameter pNewRoofDISCOArea = sqlCmd.CreateParameter();
      pNewRoofDISCOArea.ParameterName = "@newRoofDISCOArea";
      pNewRoofDISCOArea.SqlDbType = SqlDbType.Int;
      pNewRoofDISCOArea.Value = newRoofDISCOICArea;
      pNewRoofDISCOArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pNewRoofDISCOArea);
      //sqlCmd.CommandTimeout = 300;
      //sqlCmd.Connection = sqlCon;
      //sqlCon.Open();
    }

    private void AddNewRoofAreaDrywellCommandParameter(SqlCommand sqlCmd, SqlConnection sqlCon)
    {
      //add newRoofkDrywellArea parameter to SQL command sqlCmd
      SqlParameter pNewRoofDrywellArea = sqlCmd.CreateParameter();
      pNewRoofDrywellArea.ParameterName = "@newRoofDrywellArea";
      pNewRoofDrywellArea.SqlDbType = SqlDbType.Int;
      pNewRoofDrywellArea.Value = newRoofDrywellArea;
      pNewRoofDrywellArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pNewRoofDrywellArea);
      //sqlCmd.CommandTimeout = 300;
      //sqlCmd.Connection = sqlCon;
      //sqlCon.Open();
    }

    private void AddUpdaterEditorParkAreaCommandParameter(SqlCommand sqlCmd)
    {
      //add updaterEditorParkArea parameter to SQL command sqlCmd
      SqlParameter pUpdaterEditorParkArea = sqlCmd.CreateParameter();
      pUpdaterEditorParkArea.ParameterName = "@updaterEditorParkArea";
      pUpdaterEditorParkArea.SqlDbType = SqlDbType.Int;
      pUpdaterEditorParkArea.Value = Convert.ToInt32(txtNewParkArea.Text);
      pUpdaterEditorParkArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pUpdaterEditorParkArea);
    }

    private void AddUpdaterEditorParkDiscoICAreaCommandParameter(SqlCommand sqlCmd)
    {
      //add updaterEditorParkArea parameter to SQL command sqlCmd
      SqlParameter pUpdaterEditorParkDiscoICArea = sqlCmd.CreateParameter();
      pUpdaterEditorParkDiscoICArea.ParameterName = "@updaterEditorParkDiscoICArea";
      pUpdaterEditorParkDiscoICArea.SqlDbType = SqlDbType.Int;
      pUpdaterEditorParkDiscoICArea.Value = Convert.ToInt32(txtNewParkDISCOICArea.Text);
      pUpdaterEditorParkDiscoICArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pUpdaterEditorParkDiscoICArea);
    }

    private void AddUpdaterEditorParkDrywellICAreaCommandParameter(SqlCommand sqlCmd)
    {
      //add updaterEditorParkArea parameter to SQL command sqlCmd
      SqlParameter pUpdaterEditorParkDrywellICArea = sqlCmd.CreateParameter();
      pUpdaterEditorParkDrywellICArea.ParameterName = "@updaterEditorParkDrywellICArea";
      pUpdaterEditorParkDrywellICArea.SqlDbType = SqlDbType.Int;
      pUpdaterEditorParkDrywellICArea.Value = Convert.ToInt32(txtNewParkDrywellICArea.Text);
      pUpdaterEditorParkDrywellICArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pUpdaterEditorParkDrywellICArea);
    }

    private void AddUpdaterEditorRoofAreaCommandParameter(SqlCommand sqlCmd)
    {
      //add updaterEditorParkArea parameter to SQL command sqlCmd
      SqlParameter pUpdaterEditorRoofArea = sqlCmd.CreateParameter();
      pUpdaterEditorRoofArea.ParameterName = "@updaterEditorRoofArea";
      pUpdaterEditorRoofArea.SqlDbType = SqlDbType.Int;
      pUpdaterEditorRoofArea.Value = Convert.ToInt32(txtNewRoofArea.Text);
      pUpdaterEditorRoofArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pUpdaterEditorRoofArea);
    }

    private void AddUpdaterEditorRoofDiscoICAreaCommandParameter(SqlCommand sqlCmd)
    {
      //add updaterEditorParkArea parameter to SQL command sqlCmd
      SqlParameter pUpdaterEditorRoofDiscoIC = sqlCmd.CreateParameter();
      pUpdaterEditorRoofDiscoIC.ParameterName = "@updaterEditorRoofDiscoICArea";
      pUpdaterEditorRoofDiscoIC.SqlDbType = SqlDbType.Int;
      pUpdaterEditorRoofDiscoIC.Value = Convert.ToInt32(txtNewRoofDISCOICArea.Text);
      pUpdaterEditorRoofDiscoIC.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pUpdaterEditorRoofDiscoIC);
    }

    private void AddUpdaterEditorRoofDrywellICAreaCommandParameter(SqlCommand sqlCmd)
    {
      //add updaterEditorParkArea parameter to SQL command sqlCmd
      SqlParameter pUpdaterEditorRoofDrywellICArea = sqlCmd.CreateParameter();
      pUpdaterEditorRoofDrywellICArea.ParameterName = "@updaterEditorRoofDrywellICArea";
      pUpdaterEditorRoofDrywellICArea.SqlDbType = SqlDbType.Int;
      pUpdaterEditorRoofDrywellICArea.Value = Convert.ToInt32(txtNewRoofDrywellICArea.Text);
      pUpdaterEditorRoofDrywellICArea.Direction = ParameterDirection.Input;
      sqlCmd.Parameters.Add(pUpdaterEditorRoofDrywellICArea);
    }

    private static void BatchDSCEDITAPPENDQueries(SqlCommand sqlCmd)
    {
      //run Append2DSCEDITAPPEND query-appends user-defined file into DSCEDITAPPEND sql table                          
      sqlCmd.CommandText = "INSERT INTO DSCEDITAPPEND (dsc_edit_id, edit_id, edit_date, edited_by, rno, dsc_id, " +
                           "old_roof_area_sqft, new_roof_area_sqft, old_roof_disco_ic_area_sqft, " +
                           "new_roof_disco_ic_area_sqft, old_park_area_sqft, new_park_area_sqft, " +
                           "old_park_disco_ic_area_sqft, new_park_disco_ic_area_sqft, " +
                           "old_park_drywell_ic_area_sqft, old_roof_drywell_ic_area_sqft, " +
                           "new_roof_drywell_ic_area_sqft, new_park_drywell_ic_area_sqft) " +
                           "SELECT 0 AS dsc_edit_id, @editID AS edit_id, @editDate AS edit_date, @editedBy AS edited_by, " +
                           "mst_DSC_ac.RNO AS rno, USERUPDATE.dsc_id AS dsc_id, " +
                           "mst_DSC_ac.RfAreaFtEX AS old_roof_area_sqft, 0 AS new_roof_area_sqft, " +
                           "0 AS old_roof_disco_ic_area_sqft, 0 AS new_roof_disco_ic_area_sqft, " +
                           "mst_DSC_ac.PkAreaFtEX AS old_park_area_sqft, 0 AS new_park_area_sqft, " +
                           "0 AS old_park_disco_ic_area_sqft, 0 AS new_park_disco_ic_area_sqft, " +
                           "0 AS old_park_drywell_ic_area_sqft, 0 AS old_roof_drywell_ic_area_sqft, " +
                           "0 AS new_roof_drywell_ic_area_sqft, 0 AS new_park_drywell_ic_area_sqft " +
                           "FROM USERUPDATE INNER JOIN mst_DSC_ac ON USERUPDATE.dsc_id=mst_DSC_ac.DSCID";
      sqlCmd.ExecuteNonQuery();

      //run UpdateOldImperviousArea query to update the old_park_area_sqft and old_roof_area_sqft fields in the DSCEDITAPPEND table
      sqlCmd.CommandText = "UPDATE DSCEDITAPPEND SET old_park_area_sqft = mst_DSC_ac.PkAreaFtEX, " +
                        "old_roof_area_sqft = mst_DSC_ac.RfAreaFtEX " +
                        "FROM DSCEDITAPPEND INNER JOIN mst_DSC_ac " +
                        "ON DSCEDITAPPEND.dsc_id = mst_DSC_ac.dscID";
      sqlCmd.ExecuteNonQuery();

      //run UpdateOldParkDISCOICArea query
      sqlCmd.CommandText = "UPDATE DSCEDITAPPEND SET " +
                        "old_park_disco_ic_area_sqft = mst_ic_DiscoVeg_ac.SqFt " +
                        "FROM DSCEDITAPPEND INNER JOIN mst_ic_DiscoVeg_ac " +
                        "ON DSCEDITAPPEND.dsc_id = mst_ic_DiscoVeg_ac.dscID " +
                        "WHERE (((mst_ic_DiscoVeg_ac.RoofRPark)=N'P') " +
                        "AND ((mst_ic_DiscoVeg_ac.TimeFrame)=N'EX') " +
                        "AND ((mst_ic_DiscoVeg_ac.assumekey)=N'DDEX')) " +
                        "OR (((mst_ic_DiscoVeg_ac.assumekey)=N'SE01'))";
      sqlCmd.ExecuteNonQuery();

      //run UpdateOldParkDrywellArea query
      sqlCmd.CommandText = "UPDATE DSCEDITAPPEND SET " +
                        "old_park_disco_ic_area_sqft = mst_ic_Drywell_ac.SqFt " +
                        "FROM DSCEDITAPPEND INNER JOIN mst_ic_Drywell_ac " +
                        "ON DSCEDITAPPEND.dsc_id = mst_ic_Drywell_ac.dscID " +
                        "WHERE (((mst_ic_Drywell_ac.RoofRPark)=N'P') " +
                        "AND ((mst_ic_Drywell_ac.TimeFrame)=N'EX') " +
                        "AND ((mst_ic_Drywell_ac.assumeKey)=N'DWPR')) " +
                        "OR (((mst_ic_Drywell_ac.assumeKey)=N'LE80')) " +
                        "OR (((mst_ic_Drywell_ac.assumeKey)=N'SE01')) " +
                        "OR (((mst_ic_Drywell_ac.assumeKey)=N'EX01')) " +
                        "OR (((mst_ic_Drywell_ac.assumeKey)=N'INSU'))";
      sqlCmd.ExecuteNonQuery();

      //run UpdateOldRoofDISCOICArea query
      sqlCmd.CommandText = "UPDATE DSCEDITAPPEND SET " +
                        "old_roof_disco_ic_area_sqft = mst_ic_DiscoVeg_ac.SqFt " +
                        "FROM DSCEDITAPPEND INNER JOIN mst_ic_DiscoVeg_ac " +
                        "ON DSCEDITAPPEND.dsc_id = mst_ic_DiscoVeg_ac.dscID " +
                        "WHERE (((mst_ic_DiscoVeg_ac.RoofRPark)=N'R') " +
                        "AND ((mst_ic_DiscoVeg_ac.TimeFrame)=N'EX') " +
                        "AND ((mst_ic_DiscoVeg_ac.assumekey)=N'DDEX')) " +
                        "OR (((mst_ic_DiscoVeg_ac.assumekey)=N'SE01'))";
      sqlCmd.ExecuteNonQuery();

      //run UpdateOldRoofDrywellIC Area query
      sqlCmd.CommandText = "UPDATE DSCEDITAPPEND SET " +
                        "old_roof_drywell_ic_area_sqft = mst_ic_Drywell_ac.SqFt " +
                        "FROM DSCEDITAPPEND INNER JOIN mst_ic_Drywell_ac " +
                        "ON DSCEDITAPPEND.dsc_id = mst_ic_Drywell_ac.dscID " +
                        "WHERE  (((mst_ic_Drywell_ac.RoofRPark)=N'R') " +
                        "AND ((mst_ic_Drywell_ac.TimeFrame)=N'EX') " +
                        "AND ((mst_ic_Drywell_ac.assumeKey)=N'DWPR')) " +
                        "OR (((mst_ic_Drywell_ac.assumeKey)=N'LE80')) " +
                        "OR (((mst_ic_Drywell_ac.assumeKey)=N'SE01')) " +
                        "OR (((mst_ic_Drywell_ac.assumeKey)=N'EX01')) " +
                        "OR (((mst_ic_Drywell_ac.assumeKey)=N'INSU'))";
      sqlCmd.ExecuteNonQuery();

      //run UpdateDSCEDITAPPENDfromUSERUPDATE query
      sqlCmd.CommandText = "UPDATE DSCEDITAPPEND SET " +
                        "new_roof_area_sqft = USERUPDATE.new_roof_area_sqft, " +
                        "new_roof_disco_ic_area_sqft=USERUPDATE.new_roof_disco_ic_area_sqft, " +
                        "new_roof_drywell_ic_area_sqft=USERUPDATE.new_roof_drywell_ic_area_sqft, " +
                        "new_park_area_sqft=USERUPDATE.new_park_area_sqft, " +
                        "new_park_disco_ic_area_sqft=USERUPDATE.new_park_disco_ic_area_sqft, " +
                        "new_park_drywell_ic_area_sqft=USERUPDATE.new_park_drywell_ic_area_sqft " +
                        "FROM DSCEDITAPPEND INNER JOIN USERUPDATE " +
                        "ON DSCEDITAPPEND.dsc_id=USERUPDATE.dsc_id";
      sqlCmd.ExecuteNonQuery();
    }

    private static void BatchNewICRecords(SqlCommand sqlCmd)
    {
      //run AppendNewParkDISCORecords queries
      sqlCmd.CommandText = "CREATE TABLE FIRSTLIST " +
                           "([dsc_id] [int])";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO FIRSTLIST " +
                           "SELECT DSCEDITAPPEND.dsc_id " +
                           "FROM DSCEDITAPPEND " +
                           "LEFT JOIN mst_ic_DiscoVeg_ac " +
                           "ON DSCEDITAPPEND.dsc_id = mst_ic_DiscoVeg_ac.dscID " +
                           "GROUP BY DSCEDITAPPEND.dsc_id";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "CREATE TABLE SECONDLIST " +
                           "([dsc_id] [int])";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO SECONDLIST " +
                           "SELECT DSCEDITAPPEND.dsc_id " +
                           "FROM DSCEDITAPPEND " +
                           "INNER JOIN mst_ic_DiscoVeg_ac " +
                           "ON DSCEDITAPPEND.dsc_id = mst_ic_DiscoVeg_ac.dscID " +
                           "GROUP BY DSCEDITAPPEND.dsc_id, mst_ic_DiscoVeg_ac.RoofRPark " +
                           "HAVING (((mst_ic_DiscoVeg_ac.RoofRPark)='P'))";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "CREATE TABLE THIRDLIST " +
                           "([dsc_id] [int], [RoofRPark] [varchar] (1), [assume_key] [varchar] (2))";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO THIRDLIST ([dsc_id]) " +
                           "SELECT FIRSTLIST.dsc_id " +
                           "FROM FIRSTLIST " +
                           "LEFT JOIN SECONDLIST " +
                           "ON FIRSTLIST.dsc_id=SECONDLIST.dsc_id " +
                           "WHERE (((SECONDLIST.dsc_id) Is Null))";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "UPDATE THIRDLIST " +
                           "SET RoofRPark = 'P',assume_key='EX'";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO mst_ic_DiscoVeg_ac (dscID, RoofRPark, assumeKey) " +
                           "SELECT [THIRDLIST].[dsc_id], [THIRDLIST].[RoofRPark],[THIRDLIST].[assume_key] " +
                           "FROM THIRDLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE FIRSTLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE SECONDLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE THIRDLIST";
      sqlCmd.ExecuteNonQuery();

      //sqlCmd.CommandText = "INSERT INTO mst_ic_DiscoVeg_ac (dscID) " +
      //                     "SELECT DSCEDITAPPEND.dsc_id, DSCEDITAPPEND.edit_date " +
      //                     "FROM DSCEDITAPPEND " +
      //                     "LEFT JOIN mst_ic_DiscoVeg_ac " +
      //                     "ON DSCEDITAPPEND.dsc_id = mst_ic_DiscoVeg_ac.dscID " +
      //                     "WHERE (mst_ic_DiscoVeg_ac.dscID IS NULL) " +
      //                     "AND (DSCEDITAPPEND.new_park_disco_ic_area_sqft <> 0)";
      //sqlCmd.ExecuteNonQuery();

      //run UpdateNewParkDISCORecords query
      sqlCmd.CommandText = "UPDATE mst_ic_DiscoVeg_ac " +
                           "SET ParcelID = LEFT(mst_ic_DiscoVeg_ac.dscID, 6), " +
                           "DivideID = RIGHT(mst_ic_DiscoVeg_ac.dscID, 1), " +
                           "assumekey = N'DDEX', TimeFrame = N'EX', " +
                           "ApplyAreaTF = N'EX', ValidFromDate = @editDate, " +
                           "ValidToDate = N'', SqFt = DSCEDITAPPEND.new_park_disco_ic_area_sqft, " +
                           "Effectiveness = 0.7, Comment = N'DSCEditor' " +
                           "FROM mst_ic_DiscoVeg_ac " +
                           "INNER JOIN DSCEDITAPPEND " +
                           "ON ((mst_ic_DiscoVeg_ac.dscID = DSCEDITAPPEND.dsc_id) " +
                           "AND (mst_ic_DiscoVeg_ac.RoofRPark = 'P')) " +
                           "WHERE (DSCEDITAPPEND.new_park_disco_ic_area_sqft<>0)";
      sqlCmd.ExecuteNonQuery();

      //run AppendNewParkDrywellRecords query
      sqlCmd.CommandText = "CREATE TABLE FIRSTLIST " +
                           "([dsc_id] [int])";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO FIRSTLIST " +
                           "SELECT DSCEDITAPPEND.dsc_id " +
                           "FROM DSCEDITAPPEND " +
                           "LEFT JOIN mst_ic_Drywell_ac " +
                           "ON DSCEDITAPPEND.dsc_id = mst_ic_Drywell_ac.dscID " +
                           "GROUP BY DSCEDITAPPEND.dsc_id";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "CREATE TABLE SECONDLIST " +
                           "([dsc_id] [int])";
      sqlCmd.ExecuteNonQuery();

      sqlCmd.CommandText = "INSERT INTO SECONDLIST " +
                           "SELECT DSCEDITAPPEND.dsc_id " +
                           "FROM DSCEDITAPPEND " +
                           "INNER JOIN mst_ic_Drywell_ac " +
                           "ON DSCEDITAPPEND.dsc_id = mst_ic_Drywell_ac.dscID " +
                           "GROUP BY DSCEDITAPPEND.dsc_id, mst_ic_Drywell_ac.RoofRPark " +
                           "HAVING (((mst_ic_Drywell_ac.RoofRPark)='P'))";
      sqlCmd.ExecuteNonQuery();

      sqlCmd.CommandText = "CREATE TABLE THIRDLIST " +
                           "([dsc_id] [int], [RoofRPark] [varchar] (1),[assume_key] [varchar] (2))";
      sqlCmd.ExecuteNonQuery();

      sqlCmd.CommandText = "INSERT INTO THIRDLIST ([dsc_id]) " +
                           "SELECT FIRSTLIST.dsc_id " +
                           "FROM FIRSTLIST " +
                           "LEFT JOIN SECONDLIST " +
                           "ON FIRSTLIST.dsc_id=SECONDLIST.dsc_id " +
                           "WHERE (((SECONDLIST.dsc_id) Is Null))";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "UPDATE THIRDLIST " +
                           "SET RoofRPark = 'P', assume_key = 'EX'";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO mst_ic_Drywell_ac (dscID, RoofRPark, assumeKey) " +
                           "SELECT [THIRDLIST].[dsc_id], [THIRDLIST].[RoofRPark], [THIRDLIST].[assume_key]" +
                           "FROM THIRDLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE FIRSTLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE SECONDLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE THIRDLIST";
      sqlCmd.ExecuteNonQuery();

      //sqlCmd.CommandText = "INSERT INTO mst_ic_Drywell_ac (dscID) " +
      //                     "SELECT DSCEDITAPPEND.dsc_id " +
      //                     "FROM DSCEDITAPPEND  " +
      //                     "LEFT OUTER JOIN mst_ic_Drywell_ac " +
      //                     "ON DSCEDITAPPEND.dsc_id = mst_ic_Drywell_ac.dscID " +
      //                     "WHERE ((mst_ic_Drywell_ac.dscID IS NULL) " +
      //                     "AND (DSCEDITAPPEND.new_park_drywell_ic_area_sqft <> 0))";
      //sqlCmd.ExecuteNonQuery();

      //run UpdateNewParkDrywellRecords query
      sqlCmd.CommandText = "UPDATE mst_ic_Drywell_ac " +
                           "SET ParcelID = Left(mst_ic_Drywell_ac.dscID, 6), " +
                           "DivideID = Right(mst_ic_Drywell_ac.dscID, 1), " +
                           "assumeKey = N'INSU', TimeFrame = N'EX', " +
                           "ApplyAreaTF = N'EX', ValidFromDate = @editDate, " +
                           "ValidToDate = N'', SqFt = DSCEDITAPPEND.new_park_drywell_ic_area_sqft, " +
                           "Comment = N'DSCEditor', AppendDate = @editDate " +
                           "FROM mst_ic_Drywell_ac " +
                           "INNER JOIN DSCEDITAPPEND " +
                           "ON ((mst_ic_Drywell_ac.dscID = DSCEDITAPPEND.dsc_id) " +
                           "AND (mst_ic_Drywell_ac.RoofRPark='P')) " +
                           "WHERE (DSCEDITAPPEND.new_park_drywell_ic_area_sqft <> 0)";
      sqlCmd.ExecuteNonQuery();

      //AppendNewRoofDISCORecords
      //run queries that will identify which DSCs are in the mst_ic_DiscoVeg_ac table with Parking controls
      //then update the table to include any Roof controls
      sqlCmd.CommandText = "CREATE TABLE FIRSTLIST " +
                           "([dsc_id] [int])";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO FIRSTLIST " +
                           "SELECT DSCEDITAPPEND.dsc_id " +
                           "FROM DSCEDITAPPEND " +
                           "LEFT JOIN mst_ic_DiscoVeg_ac " +
                           "ON DSCEDITAPPEND.dsc_id = mst_ic_DiscoVeg_ac.dscID " +
                           "GROUP BY DSCEDITAPPEND.dsc_id";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "CREATE TABLE SECONDLIST " +
                           "([dsc_id] [int])";
      sqlCmd.ExecuteNonQuery();

      sqlCmd.CommandText = "INSERT INTO SECONDLIST " +
                           "SELECT DSCEDITAPPEND.dsc_id " +
                           "FROM DSCEDITAPPEND " +
                           "INNER JOIN mst_ic_DiscoVeg_ac " +
                           "ON DSCEDITAPPEND.dsc_id = mst_ic_DiscoVeg_ac.dscID " +
                           "GROUP BY DSCEDITAPPEND.dsc_id, mst_ic_DiscoVeg_ac.RoofRPark " +
                           "HAVING (((mst_ic_DiscoVeg_ac.RoofRPark)='R'))";
      sqlCmd.ExecuteNonQuery();

      sqlCmd.CommandText = "CREATE TABLE THIRDLIST " +
                           "([dsc_id] [int], [RoofRPark] [varchar] (1), [assume_key] [varchar] (2))";
      sqlCmd.ExecuteNonQuery();

      sqlCmd.CommandText = "INSERT INTO THIRDLIST ([dsc_id]) " +
                           "SELECT FIRSTLIST.dsc_id " +
                           "FROM FIRSTLIST " +
                           "LEFT JOIN SECONDLIST " +
                           "ON FIRSTLIST.dsc_id=SECONDLIST.dsc_id " +
                           "WHERE (((SECONDLIST.dsc_id) Is Null))";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "UPDATE THIRDLIST " +
                           "SET RoofRPark = 'R', assume_key = 'EX'";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO mst_ic_DiscoVeg_ac (dscID, RoofRPark, assumeKey) " +
                           "SELECT [THIRDLIST].[dsc_id], [THIRDLIST].[RoofRPark], [THIRDLIST].[assume_key] " +
                           "FROM THIRDLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE FIRSTLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE SECONDLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE THIRDLIST";
      sqlCmd.ExecuteNonQuery();

      //AppendNewRoofDrywellRecords
      //run queries that will identify which DSCs are in the mst_ic_Drywell_ac table with Parking controls
      //then update the table to include any Roof controls
      sqlCmd.CommandText = "CREATE TABLE FIRSTLIST " +
                           "([dsc_id] [int])";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO FIRSTLIST " +
                           "SELECT DSCEDITAPPEND.dsc_id " +
                           "FROM DSCEDITAPPEND " +
                           "LEFT JOIN mst_ic_Drywell_ac " +
                           "ON DSCEDITAPPEND.dsc_id = mst_ic_Drywell_ac.dscID " +
                           "GROUP BY DSCEDITAPPEND.dsc_id";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "CREATE TABLE SECONDLIST " +
                           "([dsc_id] [int])";
      sqlCmd.ExecuteNonQuery();

      sqlCmd.CommandText = "INSERT INTO SECONDLIST " +
                           "SELECT DSCEDITAPPEND.dsc_id " +
                           "FROM DSCEDITAPPEND " +
                           "INNER JOIN mst_ic_Drywell_ac " +
                           "ON DSCEDITAPPEND.dsc_id = mst_ic_Drywell_ac.dscID " +
                           "GROUP BY DSCEDITAPPEND.dsc_id, mst_ic_Drywell_ac.RoofRPark " +
                           "HAVING (((mst_ic_Drywell_ac.RoofRPark)='R'))";
      sqlCmd.ExecuteNonQuery();

      sqlCmd.CommandText = "CREATE TABLE THIRDLIST " +
                           "([dsc_id] [int], [RoofRPark] [varchar] (1), [assume_key] [varchar] (2))";
      sqlCmd.ExecuteNonQuery();

      sqlCmd.CommandText = "INSERT INTO THIRDLIST ([dsc_id]) " +
                           "SELECT FIRSTLIST.dsc_id " +
                           "FROM FIRSTLIST " +
                           "LEFT JOIN SECONDLIST " +
                           "ON FIRSTLIST.dsc_id=SECONDLIST.dsc_id " +
                           "WHERE (((SECONDLIST.dsc_id) Is Null))";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "UPDATE THIRDLIST " +
                           "SET RoofRPark = 'R', assume_key = 'EX'";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO mst_ic_Drywell_ac (dscID, RoofRPark, assumeKey) " +
                           "SELECT [THIRDLIST].[dsc_id], [THIRDLIST].[RoofRPark], [THIRDLIST].[assume_key] " +
                           "FROM THIRDLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE FIRSTLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE SECONDLIST";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DROP TABLE THIRDLIST";
      sqlCmd.ExecuteNonQuery();

      //run UpdateNewRoofDISCORecords query
      sqlCmd.CommandText = "UPDATE mst_ic_DiscoVeg_ac SET " +
                        "ParcelID = Left(mst_ic_DiscoVeg_ac.dscID, 6), " +
                        "DivideID = Right(mst_ic_DiscoVeg_ac.dscID, 1), " +
                        "assumekey = N'DDEX', TimeFrame = N'EX', " +
                        "ApplyAreaTF = N'EX', ValidFromDate = @editDate, " +
                        "ValidToDate = N'', SqFt = DSCEDITAPPEND.new_roof_disco_ic_area_sqft, " +
                        "Effectiveness = 0.7, Comment = N'DSCEditor', " +
                        "AppendDate = @editDate FROM mst_ic_DiscoVeg_ac " +
                        "INNER JOIN DSCEDITAPPEND ON " +
                        "(mst_ic_DiscoVeg_ac.dscID = DSCEDITAPPEND.dsc_id " +
                        "AND mst_ic_DiscoVeg_ac.RoofRPark = 'R') " +
                        "WHERE (DSCEDITAPPEND.new_roof_disco_ic_area_sqft <> 0)";
      sqlCmd.ExecuteNonQuery();

      //run UpdateNewRoofDrywellRecords query
      sqlCmd.CommandText = "UPDATE mst_ic_Drywell_ac SET " +
                        "ParcelID = Left(mst_ic_Drywell_ac.dscID, 6), " +
                        "DivideID = Right(mst_ic_Drywell_ac.dscID, 1), " +
                        "assumeKey = N'INSU', TimeFrame = N'EX', " +
                        "ApplyAreaTF = N'EX', ValidFromDate = @editDate, " +
                        "ValidToDate = N'', SqFt = DSCEDITAPPEND.new_roof_drywell_ic_area_sqft, " +
                        "Comment = N'DSCEditor', AppendDate = @editDate FROM " +
                        "mst_ic_Drywell_ac INNER JOIN DSCEDITAPPEND ON " +
                        "(mst_ic_Drywell_ac.dscID = DSCEDITAPPEND.dsc_id " +
                        "AND mst_ic_Drywell_ac.RoofRPark = 'R') " +
                        "WHERE (DSCEDITAPPEND.new_roof_drywell_ic_area_sqft <> 0)";
      sqlCmd.ExecuteNonQuery();
    }

    private static void BatchUpdateMasterICTables(SqlCommand sqlCmd)
    {
      //run UpdateCurrentParkDISCORecords query
      sqlCmd.CommandText = "UPDATE mst_ic_DiscoVeg_ac SET " +
                        "SqFt = DSCEDITAPPEND.new_park_disco_ic_area_sqft " +
                        "FROM mst_ic_DiscoVeg_ac INNER JOIN DSCEDITAPPEND " +
                        "ON mst_ic_DiscoVeg_ac.dscID = DSCEDITAPPEND.dsc_id " +
                        "WHERE (((mst_ic_DiscoVeg_ac.RoofRPark)=N'P') " +
                        "AND ((mst_ic_DiscoVeg_ac.TimeFrame)=N'EX') " +
                        "AND ((mst_ic_DiscoVeg_ac.assumekey)=N'DDEX')) " +
                        "OR (((mst_ic_DiscoVeg_ac.assumekey)=N'SE01'))";
      sqlCmd.ExecuteNonQuery();

      //run UpdateCurrentParkDrywell query
      sqlCmd.CommandText = "UPDATE mst_ic_Drywell_ac SET " +
                        "SqFt = DSCEDITAPPEND.new_park_drywell_ic_area_sqft " +
                        "FROM mst_ic_Drywell_ac INNER JOIN DSCEDITAPPEND ON " +
                        "mst_ic_Drywell_ac.dscID = DSCEDITAPPEND.dsc_id WHERE " +
                        "(((mst_ic_Drywell_ac.RoofRPark)=N'P') AND " +
                        "((mst_ic_Drywell_ac.TimeFrame)=N'EX') AND " +
                        "((mst_ic_Drywell_ac.assumeKey)=N'DWPR')) OR " +
                        "(((mst_ic_Drywell_ac.assumeKey)=N'LE80')) OR " +
                        "(((mst_ic_Drywell_ac.assumeKey)=N'SE01')) OR " +
                        "(((mst_ic_Drywell_ac.assumeKey)=N'EX01')) OR " +
                        "(((mst_ic_Drywell_ac.assumeKey)=N'INSU'))";
      sqlCmd.ExecuteNonQuery();

      //run UpdateCurrentRoofDISCORecords query
      sqlCmd.CommandText = "UPDATE mst_ic_DiscoVeg_ac SET " +
                        "SqFt = DSCEDITAPPEND.new_roof_disco_ic_area_sqft FROM " +
                        "mst_ic_DiscoVeg_ac INNER JOIN DSCEDITAPPEND ON " +
                        "mst_ic_DiscoVeg_ac.dscID = DSCEDITAPPEND.dsc_id WHERE " +
                        "(((mst_ic_DiscoVeg_ac.RoofRPark)=N'R') AND " +
                        "((mst_ic_DiscoVeg_ac.TimeFrame)=N'EX') AND " +
                        "((mst_ic_DiscoVeg_ac.assumekey)=N'DDEX')) OR " +
                        "(((mst_ic_DiscoVeg_ac.assumekey)=N'SE01'))";
      sqlCmd.ExecuteNonQuery();

      //run UpdateCurrentRoofDrywellRecords query
      sqlCmd.CommandText = "UPDATE mst_ic_Drywell_ac SET " +
                        "SqFt = DSCEDITAPPEND.new_roof_drywell_ic_area_sqft FROM " +
                        "mst_ic_Drywell_ac INNER JOIN DSCEDITAPPEND ON " +
                        "mst_ic_Drywell_ac.dscID = DSCEDITAPPEND.dsc_id WHERE " +
                        "(((mst_ic_Drywell_ac.RoofRPark)=N'R') AND " +
                        "((mst_ic_Drywell_ac.TimeFrame)=N'EX') AND " +
                        "((mst_ic_Drywell_ac.assumeKey)=N'DWPR')) OR " +
                        "(((mst_ic_Drywell_ac.assumeKey)=N'LE80')) OR " +
                        "(((mst_ic_Drywell_ac.assumeKey)=N'SE01')) OR " +
                        "(((mst_ic_Drywell_ac.assumeKey)=N'EX01')) OR " +
                        "(((mst_ic_Drywell_ac.assumeKey)=N'INSU'))";
      sqlCmd.ExecuteNonQuery();
    }

    private static void BatchUpdateDSCAreas(SqlCommand sqlCmd)
    {
      //run UpdateMasterParkArea query
      sqlCmd.CommandText = "UPDATE mst_DSC_ac SET " +
                        "surveyedPkAreaSqft = DSCEDITAPPEND.new_park_area_sqft, " +
                        "parkAreaNeedsUpdate = 1 FROM mst_DSC_ac INNER JOIN DSCEDITAPPEND " +
                        "ON mst_DSC_ac.DSCID = DSCEDITAPPEND.dsc_id " +
                        "AND mst_DSC_ac.PkAreaFtEX <> DSCEDITAPPEND.new_park_area_sqft";
      sqlCmd.ExecuteNonQuery();

      //run UpdateMasterRoofArea query
      sqlCmd.CommandText = "UPDATE mst_DSC_ac SET " +
                        "surveyedRfAreaSqFt = DSCEDITAPPEND.new_roof_area_sqft, " +
                        "roofAreaNeedsUpdate = 1 FROM mst_DSC_ac INNER JOIN DSCEDITAPPEND " +
                        "ON mst_DSC_ac.DSCID = DSCEDITAPPEND.dsc_id AND " +
                        "mst_DSC_ac.RfAreaFtEX <> DSCEDITAPPEND.new_roof_area_sqft";
      sqlCmd.ExecuteNonQuery();
    }

    private static void BatchRevertICEdits(SqlCommand sqlCmd)
    {
      //Update roof DISCO records in mst_DiscoVeg_ac to old value
      //These records would have already existed at the time of the previous edit session
      sqlCmd.CommandText = "UPDATE [mst_ic_DiscoVeg_ac] SET " +
                        "SqFt = [DSCEDITAPPEND].[old_park_disco_ic_area_sqft] " +
                        "FROM [mst_ic_DiscoVeg_ac] INNER JOIN [DSCEDITAPPEND] " +
                        "ON [mst_ic_DiscoVeg_ac].[dscID] = [DSCEDITAPPEND].[dsc_id] " +
                        "WHERE (([mst_ic_DiscoVeg_ac].[RoofRPark]=N'R') " +
                        "AND ([mst_ic_DiscoVeg_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_DiscoVeg_ac].[assumekey]=N'DDEX')) " +
                        "OR (([mst_ic_DiscoVeg_ac].[RoofRPark]=N'R') " +
                        "AND ([mst_ic_DiscoVeg_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_DiscoVeg_ac].[assumekey]=N'SE01'))";
      sqlCmd.ExecuteNonQuery();

      //Update park DISCO records in mst_DiscoVeg_ac to old value
      //These records would have already existed at the time of the previous edit session
      sqlCmd.CommandText = "UPDATE [mst_ic_DiscoVeg_ac] SET " +
                        "SqFt = [DSCEDITAPPEND].[old_park_disco_ic_area_sqft] " +
                        "FROM [mst_ic_DiscoVeg_ac] INNER JOIN [DSCEDITAPPEND] " +
                        "ON [mst_ic_DiscoVeg_ac].[dscID] = [DSCEDITAPPEND].[dsc_id] " +
                        "WHERE (([mst_ic_DiscoVeg_ac].[RoofRPark]=N'P') " +
                        "AND ([mst_ic_DiscoVeg_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_DiscoVeg_ac].[assumekey]=N'DDEX')) " +
                        "OR (([mst_ic_DiscoVeg_ac].[RoofRPark]=N'P') " +
                        "AND ([mst_ic_DiscoVeg_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_DiscoVeg_ac].[assumekey]=N'SE01'))";
      sqlCmd.ExecuteNonQuery();

      //Update roof Drywell records in mst_Drywell_ac to old value
      //These records would have already existed at the time of the previous edit session
      sqlCmd.CommandText = "UPDATE [mst_ic_Drywell_ac] SET " +
                        "SqFt = [DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft] " +
                        "FROM [mst_ic_Drywell_ac] INNER JOIN [DSCEDITAPPEND] " +
                        "ON [mst_ic_Drywell_ac].[dscID] = [DSCEDITAPPEND].[dsc_id] " +
                        "WHERE (([mst_ic_Drywell_ac].[RoofRPark]=N'R') " +
                        "AND ([mst_ic_Drywell_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_Drywell_ac].[assumekey]=N'LE80')) " +
                        "OR (([mst_ic_Drywell_ac].[RoofRPark]=N'R') " +
                        "AND ([mst_ic_Drywell_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_Drywell_ac].[assumekey]=N'SE01')) " +
                        "OR (([mst_ic_Drywell_ac].[RoofRPark]=N'R') " +
                        "AND ([mst_ic_Drywell_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_Drywell_ac].[assumekey]=N'EX01')) " +
                        "OR (([mst_ic_Drywell_ac].[RoofRPark]=N'R') " +
                        "AND ([mst_ic_Drywell_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_Drywell_ac].[assumekey]=N'INSU'))";
      sqlCmd.ExecuteNonQuery();

      //Update park Drywell records in mst_Drywell_ac to old value
      //These records would have already existed at the time of the previous edit session
      sqlCmd.CommandText = "UPDATE [mst_ic_Drywell_ac] SET " +
                        "SqFt = [DSCEDITAPPEND].[old_park_drywell_ic_area_sqft] " +
                        "FROM [mst_ic_Drywell_ac] INNER JOIN [DSCEDITAPPEND] " +
                        "ON [mst_ic_Drywell_ac].[dscID] = [DSCEDITAPPEND].[dsc_id] " +
                        "WHERE (([mst_ic_Drywell_ac].[RoofRPark]=N'P') " +
                        "AND ([mst_ic_Drywell_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_Drywell_ac].[assumekey]=N'LE80')) " +
                        "OR (([mst_ic_Drywell_ac].[RoofRPark]=N'P') " +
                        "AND ([mst_ic_Drywell_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_Drywell_ac].[assumekey]=N'SE01')) " +
                        "OR (([mst_ic_Drywell_ac].[RoofRPark]=N'P') " +
                        "AND ([mst_ic_Drywell_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_Drywell_ac].[assumekey]=N'EX01')) " +
                        "OR (([mst_ic_Drywell_ac].[RoofRPark]=N'P') " +
                        "AND ([mst_ic_Drywell_ac].[TimeFrame]=N'EX') " +
                        "AND ([mst_ic_Drywell_ac].[assumekey]=N'INSU'))";
      sqlCmd.ExecuteNonQuery();

      //delete roof DISCO records from mst_DiscoVeg_ac
      //these records would have been appended at the time of the previous edit session
      sqlCmd.CommandText = "DELETE " +
                           "FROM [mst_ic_DiscoVeg_ac] " +
                           "WHERE EXISTS " +
                           "   (SELECT * " +
                           "   FROM [DSCEDITAPPEND] " +
                           "   WHERE (([mst_ic_DiscoVeg_ac].[dscID]=[DSCEDITAPPEND].[dsc_id]) AND " +
                           "   (([DSCEDITAPPEND].[new_roof_disco_ic_area_sqft])>0) AND " +
                           "   (([DSCEDITAPPEND].[old_roof_disco_ic_area_sqft])=0) AND " +
                           "   (([mst_ic_DiscoVeg_ac].[RoofRPark])='R') AND " +
                           "   (([mst_ic_DiscoVeg_ac].[assumekey])='DDEX') AND " +
                           "   (([mst_ic_DiscoVeg_ac].[TimeFrame])='EX')))";
      sqlCmd.ExecuteNonQuery();

      //delete park DISCO records from mst_DiscoVeg_ac
      //these records would have been appended at the time of the previous edit session
      sqlCmd.CommandText = "DELETE " +
                           "FROM [mst_ic_DiscoVeg_ac] " +
                           "WHERE EXISTS " +
                           "   (SELECT * " +
                           "   FROM [DSCEDITAPPEND] " +
                           "   WHERE (([mst_ic_DiscoVeg_ac].[dscID]=[DSCEDITAPPEND].[dsc_id]) AND " +
                           "   (([DSCEDITAPPEND].[new_roof_disco_ic_area_sqft])>0) AND " +
                           "   (([DSCEDITAPPEND].[old_roof_disco_ic_area_sqft])=0) AND " +
                           "   (([mst_ic_DiscoVeg_ac].[RoofRPark])='P') AND " +
                           "   (([mst_ic_DiscoVeg_ac].[assumekey])='DDEX') AND " +
                           "   (([mst_ic_DiscoVeg_ac].[TimeFrame])='EX')))";
      sqlCmd.ExecuteNonQuery();

      //delete roof Drywell records from mst_Drywell_ac
      //these records would have been appended at the time of the previous edit session
      sqlCmd.CommandText = "DELETE " +
                           "FROM [mst_ic_Drywell_ac] " +
                           "WHERE EXISTS " +
                           "   (SELECT * " +
                           "   FROM [DSCEDITAPPEND] " +
                           "   WHERE (([mst_ic_Drywell_ac].[dscID]=[DSCEDITAPPEND].[dsc_id]) AND " +
                           "   (([DSCEDITAPPEND].[new_roof_drywell_ic_area_sqft])>0) AND " +
                           "   (([DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft])=0) AND " +
                           "   (([mst_ic_Drywell_ac].[RoofRPark])='R') AND " +
                           "   (([mst_ic_Drywell_ac].[assumekey])='SE01') AND " +
                           "   (([mst_ic_Drywell_ac].[TimeFrame])='EX')) OR " +
                           "   (([mst_ic_Drywell_ac].[dscID]=[DSCEDITAPPEND].[dsc_id]) AND " +
                           "   (([DSCEDITAPPEND].[new_roof_drywell_ic_area_sqft])>0) AND " +
                           "   (([DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft])=0) AND " +
                           "   (([mst_ic_Drywell_ac].[RoofRPark])='R') AND " +
                           "   (([mst_ic_Drywell_ac].[assumekey])='EX01') AND " +
                           "   (([mst_ic_Drywell_ac].[TimeFrame])='EX')) OR " +
                           "   (([mst_ic_Drywell_ac].[dscID]=[DSCEDITAPPEND].[dsc_id]) AND " +
                           "   (([DSCEDITAPPEND].[new_roof_drywell_ic_area_sqft])>0) AND " +
                           "   (([DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft])=0) AND " +
                           "   (([mst_ic_Drywell_ac].[RoofRPark])='R') AND " +
                           "   (([mst_ic_Drywell_ac].[assumekey])='LE80') AND " +
                           "   (([mst_ic_Drywell_ac].[TimeFrame])='EX')) OR " +
                           "   (([mst_ic_Drywell_ac].[dscID]=[DSCEDITAPPEND].[dsc_id]) AND " +
                           "   (([DSCEDITAPPEND].[new_roof_drywell_ic_area_sqft])>0) AND " +
                           "   (([DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft])=0) AND " +
                           "   (([mst_ic_Drywell_ac].[RoofRPark])='R') AND " +
                           "   (([mst_ic_Drywell_ac].[assumekey])='INSU') AND " +
                           "   (([mst_ic_Drywell_ac].[TimeFrame])='EX')))";
      sqlCmd.ExecuteNonQuery();

      //delete park Drywell records from mst_Drywell_ac
      //these records would have been appended at the time of the previous edit session
      sqlCmd.CommandText = "DELETE " +
                           "FROM [mst_ic_Drywell_ac] " +
                           "WHERE EXISTS " +
                           "   (SELECT * " +
                           "   FROM [DSCEDITAPPEND] " +
                           "   WHERE (([mst_ic_Drywell_ac].[dscID]=[DSCEDITAPPEND].[dsc_id]) AND " +
                           "   (([DSCEDITAPPEND].[new_roof_drywell_ic_area_sqft])>0) AND " +
                           "   (([DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft])=0) AND " +
                           "   (([mst_ic_Drywell_ac].[RoofRPark])='P') AND " +
                           "   (([mst_ic_Drywell_ac].[assumekey])='SE01') AND " +
                           "   (([mst_ic_Drywell_ac].[TimeFrame])='EX')) OR " +
                           "   (([mst_ic_Drywell_ac].[dscID]=[DSCEDITAPPEND].[dsc_id]) AND " +
                           "   (([DSCEDITAPPEND].[new_roof_drywell_ic_area_sqft])>0) AND " +
                           "   (([DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft])=0) AND " +
                           "   (([mst_ic_Drywell_ac].[RoofRPark])='P') AND " +
                           "   (([mst_ic_Drywell_ac].[assumekey])='EX01') AND " +
                           "   (([mst_ic_Drywell_ac].[TimeFrame])='EX')) OR " +
                           "   (([mst_ic_Drywell_ac].[dscID]=[DSCEDITAPPEND].[dsc_id]) AND " +
                           "   (([DSCEDITAPPEND].[new_roof_drywell_ic_area_sqft])>0) AND " +
                           "   (([DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft])=0) AND " +
                           "   (([mst_ic_Drywell_ac].[RoofRPark])='P') AND " +
                           "   (([mst_ic_Drywell_ac].[assumekey])='LE80') AND " +
                           "   (([mst_ic_Drywell_ac].[TimeFrame])='EX')) OR " +
                           "   (([mst_ic_Drywell_ac].[dscID]=[DSCEDITAPPEND].[dsc_id]) AND " +
                           "   (([DSCEDITAPPEND].[new_roof_drywell_ic_area_sqft])>0) AND " +
                           "   (([DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft])=0) AND " +
                           "   (([mst_ic_Drywell_ac].[RoofRPark])='P') AND " +
                           "   (([mst_ic_Drywell_ac].[assumekey])='INSU') AND " +
                           "   (([mst_ic_Drywell_ac].[TimeFrame])='EX')))";
      sqlCmd.ExecuteNonQuery();
    }

    private static void BatchSESSION(SqlCommand sqlCmd)
    {
      //insert new record into SESSION signifying the edit event
      sqlCmd.CommandText = "SET IDENTITY_INSERT [SESSION] ON " +
                           "INSERT [SESSION] (edit_id,edit_date,edited_by) VALUES " +
                           "(@editID, @editDate, @editedBy)";
      sqlCmd.ExecuteNonQuery();
    }

    private static void BatchDSCEDIT(SqlCommand sqlCmd)
    {
      //run Append2DSCEDIT query                          
      sqlCmd.CommandText = "INSERT INTO DSCEDIT (edit_id, " +
                           "edit_date, edited_by, rno, dsc_id, old_roof_area_sqft, " +
                           "new_roof_area_sqft, old_roof_disco_ic_area_sqft, " +
                           "new_roof_disco_ic_area_sqft, old_roof_drywell_ic_area_sqft, " +
                           "new_roof_drywell_ic_area_sqft, old_park_area_sqft, " +
                           "new_park_area_sqft, old_park_disco_ic_area_sqft, " +
                           "new_park_disco_ic_area_sqft, old_park_drywell_ic_area_sqft, " +
                           "new_park_drywell_ic_area_sqft) SELECT " +
                           "DSCEDITAPPEND.edit_id AS edit_id, " +
                           "DSCEDITAPPEND.edit_date AS edit_date, " +
                           "DSCEDITAPPEND.edited_by AS edited_by, " +
                           "DSCEDITAPPEND.RNO AS rno, " +
                           "DSCEDITAPPEND.dsc_id AS dsc_id, " +
                           "DSCEDITAPPEND.old_roof_area_sqft AS old_roof_area_sqft, " +
                           "DSCEDITAPPEND.new_roof_area_sqft AS new_roof_area_sqft, " +
                           "DSCEDITAPPEND.old_roof_disco_ic_area_sqft AS old_roof_disco_ic_area_sqft, " +
                           "DSCEDITAPPEND.new_roof_disco_ic_area_sqft AS new_roof_disco_ic_area_sqft, " +
                           "DSCEDITAPPEND.old_roof_drywell_ic_area_sqft AS old_roof_drywell_ic_area_sqft, " +
                           "DSCEDITAPPEND.new_roof_drywell_ic_area_sqft AS new_roof_drywell_ic_area_sqft, " +
                           "DSCEDITAPPEND.old_park_area_sqft AS old_park_area_sqft, " +
                           "DSCEDITAPPEND.new_park_area_sqft AS new_park_area_sqft, " +
                           "DSCEDITAPPEND.old_park_disco_ic_area_sqft AS old_park_disco_ic_area_sqft, " +
                           "DSCEDITAPPEND.new_park_disco_ic_area_sqft AS new_park_disco_ic_area_sqft, " +
                           "DSCEDITAPPEND.old_park_drywell_ic_area_sqft AS old_park_drywell_ic_area_sqft, " +
                           "DSCEDITAPPEND.new_park_drywell_ic_area_sqft AS new_park_drywell_ic_area_sqft " +
                           "FROM DSCEDITAPPEND";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "UPDATE DSCEDIT SET updater_editor_value_changed = 'False'";
      sqlCmd.ExecuteNonQuery();

    }

    private static void BatchDeleteFromSQlTables(SqlCommand sqlCmd)
    {
      //run DeleteDSCEDITAPPEND Table query
      sqlCmd.CommandText = "DELETE FROM DSCEDITAPPEND";
      sqlCmd.ExecuteNonQuery();

      //run DeleteUserUpdate Table query
      sqlCmd.CommandText = "DELETE FROM USERUPDATE";
      sqlCmd.ExecuteNonQuery();
    }

    private static void BatchCheckNewRetroSiteAssessments(SqlCommand sqlCmd)
    {

    }

    private static void BatchCheckNewRetroIcTargets(SqlCommand sqlCmd)
    {

    }

    private static void BatchCheckNewContructedRetroFacs(SqlCommand sqlCmd)
    {

    }

    private static void SendImpAEmail()
    {
      string toValue = "jrubengb@gmail.com";
      string subjectValue = "Request for Impervious Area Update";
      string bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the impervious area coverage." + "\r\n" +
                "The attached table lists parcels by DSCID that are in need of updates in the modeling system.";
      CSharp.OutlookMail oMail = new CSharp.OutlookMail();
      oMail.AddToOutbox(toValue, subjectValue, bodyValue);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      impAQCCounter = 0;
      SetStatus("Ready");
      SetProgress(0);
      txtFileName.Text = "";
      NavigateTo(tabPageControlMain.Tab.Key);
    }

    private void SetProgress(int progress)
    {
      this.ultraStatusBar1.Panels["progressBar"].ProgressBarInfo.Value = progress;

    }

    private int SetAndIncrementProgress(int progress)
    {
      SetProgress(progress);
      return ++this.ultraStatusBar1.Panels["progress"].ProgressBarInfo.Value;


    }
    private void SetStatus(string status)
    {
      this.ultraStatusBar1.Panels["status"].Text = status;
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void btnCloseEditorHistory_Click(object sender, EventArgs e)
    {

    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
      //TO-DO: change DSCUpdaterConfig.xml file DB connection settings back to default
    }

    private void btnDSCQC_Click(object sender, EventArgs e)
    {
    }

    private void btnParkICQC_Click(object sender, EventArgs e)
    {

    }

    private void btnRoofICQC_Click(object sender, EventArgs e)
    {

    }

    private void btnImpAQC_Click(object sender, EventArgs e)
    {
      impAQCCounter = impAQCCounter + 1;

    }

    private void btnUpdaterHistoryReturn_Click(object sender, EventArgs e)
    {
      if (txtNewParkArea.Text != "" || txtNewRoofArea.Text != "" || txtNewRoofDrywellICArea.Text != "" || txtNewRoofDISCOICArea.Text != "")
      {
        if (MessageBox.Show("Abandon editing without saving?", "DSC Updater", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
        {

        }
      }
      else
      {

      }
      this.sESSIONTableAdapter.Fill(this.projectDataSet.SESSION);
      bindingNavigator1.BindingSource = sESSIONBindingSource;
    }

    private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (File.Exists(tempFileName))
      {
        File.Delete(tempFileName);
      }
    }

    private void dgvUpdaterEditor_SelectionChanged(object sender, EventArgs e)
    {
      txtNewRoofArea.Text = "";
      txtNewRoofDISCOICArea.Text = "";
      txtNewRoofDrywellICArea.Text = "";
      txtNewParkArea.Text = "";
      txtNewParkDISCOICArea.Text = "";
      txtNewParkDrywellICArea.Text = "";

      string rNoLblText = "";
      string currentParkAreaLblText = "";
      string currentParkDiscoICAreaLblText = "";
      string currentParkDrywellICAreaLblText = "";
      string currentRoofAreaLblText = "";
      string currentRoofDiscoICAreaLblText = "";
      string currentRoofDrywellICAreaLblText = "";

      //MessageBox.Show(dgvUpdaterEditor.SelectedRows.Count.ToString());
      if (dgvUpdaterEditor.SelectedRows.Count == 0)
      {
        return;
      }

      if (dgvUpdaterEditor.SelectedRows.Count == 1)
      {
        txtNewParkArea.Enabled = true;
        txtNewRoofArea.Enabled = true;
        txtNewParkDISCOICArea.Enabled = true;
        txtNewParkDrywellICArea.Enabled = true;
        txtNewRoofDISCOICArea.Enabled = true;
        txtNewRoofDrywellICArea.Enabled = true;

        if (dgvUpdaterEditor.SelectedRows[0].Cells[4].Value.ToString() != "" && dgvUpdaterEditor.SelectedRows[0].Cells[18].Value.ToString() == "False")
        {
          //MessageBox.Show("If1");
          rNoLblText = "RNO: Not Available";
          currentParkAreaLblText = "Current park area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[13].Value.ToString();
          currentParkDiscoICAreaLblText = "Current park DISCO IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[15].Value.ToString(); ;
          currentParkDrywellICAreaLblText = "Current park drywell IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[17].Value.ToString();
          currentRoofAreaLblText = "Current roof area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[7].Value.ToString();
          currentRoofDiscoICAreaLblText = "Current roof DISCO IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[9].Value.ToString();
          currentRoofDrywellICAreaLblText = "Current roof drywell IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[11].Value.ToString();
        }

        if (dgvUpdaterEditor.SelectedRows[0].Cells[4].Value.ToString() != "" && dgvUpdaterEditor.SelectedRows[0].Cells[18].Value.ToString() == "True")
        {
          //MessageBox.Show("If2");
          rNoLblText = "RNO: " + dgvUpdaterEditor.SelectedRows[0].Cells[4].Value.ToString();
          currentParkAreaLblText = "Updated park area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[13].Value.ToString();
          currentParkDiscoICAreaLblText = "Updated park DISCO IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[15].Value.ToString(); ;
          currentParkDrywellICAreaLblText = "Updated park drywell IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[17].Value.ToString();
          currentRoofAreaLblText = "Updated roof area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[7].Value.ToString();
          currentRoofDiscoICAreaLblText = "Updated roof DISCO IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[9].Value.ToString();
          currentRoofDrywellICAreaLblText = "Updated roof drywell IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[11].Value.ToString();
        }

        if (dgvUpdaterEditor.SelectedRows[0].Cells[4].Value.ToString() == "" && dgvUpdaterEditor.SelectedRows[0].Cells[18].Value.ToString() == "False")
        {
          //MessageBox.Show("If3");
          rNoLblText = "RNO: Not Available";
          currentParkAreaLblText = "Current park area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[13].Value.ToString();
          currentParkDiscoICAreaLblText = "Current park DISCO IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[15].Value.ToString(); ;
          currentParkDrywellICAreaLblText = "Current park drywell IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[17].Value.ToString();
          currentRoofAreaLblText = "Current roof area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[7].Value.ToString();
          currentRoofDiscoICAreaLblText = "Current roof DISCO IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[9].Value.ToString();
          currentRoofDrywellICAreaLblText = "Current roof drywell IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[11].Value.ToString();
        }

        if (dgvUpdaterEditor.SelectedRows[0].Cells[4].Value.ToString() == "" && dgvUpdaterEditor.SelectedRows[0].Cells[18].Value.ToString() == "True")
        {
          //MessageBox.Show("If4");
          rNoLblText = "RNO: Not Available";
          currentParkAreaLblText = "Updated park area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[13].Value.ToString();
          currentParkDiscoICAreaLblText = "Updated park DISCO IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[15].Value.ToString(); ;
          currentParkDrywellICAreaLblText = "Updated park drywell IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[17].Value.ToString();
          currentRoofAreaLblText = "Updated roof area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[7].Value.ToString();
          currentRoofDiscoICAreaLblText = "Updated roof DISCO IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[9].Value.ToString();
          currentRoofDrywellICAreaLblText = "Updated roof drywell IC area (sqft): " + dgvUpdaterEditor.SelectedRows[0].Cells[11].Value.ToString();
        }
        lblSelectedEditorRNO.Text = rNoLblText;
        lblSelectedEditorPkArea.Text = currentParkAreaLblText;
        lblSelectedEditorPkDISCOICArea.Text = currentParkDiscoICAreaLblText;
        lblSelectedEditorPkDrywellICArea.Text = currentParkDrywellICAreaLblText;
        lblSelectedEditorRfArea.Text = currentRoofAreaLblText;
        lblSelectedEditorRfDISCOICArea.Text = currentRoofDiscoICAreaLblText;
        lblSelectedEditorRfDrywellICArea.Text = currentRoofDrywellICAreaLblText;
      }
    }

    private void btnCloseOptions_Click(object sender, EventArgs e)
    {

    }

    private void DownloadUpdateTemplate()
    {
      string templatePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Template\\UserUpdate.csv";
      SaveFileDialog sfdMain = new SaveFileDialog();
      sfdMain.Title = "Where do you want to save the UserUpdate file?";
      sfdMain.InitialDirectory = @"C:\";
      sfdMain.FileName = "UserUpdate.csv";
      if (sfdMain.ShowDialog() == DialogResult.OK)
      {
        File.Copy(templatePath, sfdMain.FileName, true);
      }
    }

    private void ApplyUpdates()
    {

    }

    private void CheckRetroUpdates()
    {

      //RunIncomingRetroChangesReport();
      //TO-DO: Implement RunIncomingRetroChangesReport();
    }

    private void ApplyRetroUpdates()
    {

    }

    private void LoadUpdaterHistory()
    {
      //MessageBox.Show("LoadUpdaterHistory");     
      this.sESSIONTableAdapter.Fill(this.projectDataSet.SESSION);
      bindingNavigator1.BindingSource = sESSIONBindingSource;
      tabControlMain.Visible = true;
      Cursor.Current = Cursors.Arrow;
    }

    private void RevertSessionChanges()
    {
      //tabControlMain.TabPages.Remove(tabMain);
      //tabControlMain.TabPages.Remove(tabLoadedUpdateReview);
      //tabControlMain.TabPages.Remove(tabUpdaterEditor);
      //tabControlMain.TabPages.Remove(tabMissingDSC);
      //tabControlMain.TabPages.Remove(tabIncorrectParkICArea);
      //tabControlMain.TabPages.Remove(tabIncorrectRoofICArea);
      //tabControlMain.TabPages.Remove(tabPendingImpAChanges);
      //tabControlMain.TabPages.Remove(tabDBConnOptions);
      //tabControlMain.TabPages.Remove(tabDownloadTemplate);       
      //tabControlMain.Visible = true;
      //MessageBox.Show("RevertSessionChanges");
    }

    private void btnSubmitUpdates_Click(object sender, EventArgs e)
    {
      Cursor.Current = Cursors.WaitCursor;
      SetStatus("Submitting");

      SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.DscEditorConnectionString);
      sqlCon.Open();
      SqlCommand sqlCmd = new SqlCommand();
      sqlCmd.CommandText = "DELETE FROM USERUPDATE";
      sqlCmd.Connection = sqlCon;
      sqlCmd.ExecuteNonQuery();

      string filepath = "c:\\";
      string str = "SELECT * FROM temp.csv";
      string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";" + "Extended Properties='text;FMT=Delimited(,);HDR=Yes'";
      OleDbDataAdapter daUserUpdate = new OleDbDataAdapter(str, strCon);
      DataTable dtUserUpdate = new DataTable();
      daUserUpdate.Fill(dtUserUpdate);
      SqlBulkCopy bulkcopy = new SqlBulkCopy(sqlCon);
      bulkcopy.DestinationTableName = "USERUPDATE";
      SqlBulkCopyColumnMapping colmap1 = new SqlBulkCopyColumnMapping(0, "rno");
      SqlBulkCopyColumnMapping colmap2 = new SqlBulkCopyColumnMapping(1, "dsc_id");
      SqlBulkCopyColumnMapping colmap3 = new SqlBulkCopyColumnMapping(2, "new_roof_area_sqft");
      SqlBulkCopyColumnMapping colmap4 = new SqlBulkCopyColumnMapping(3, "new_roof_disco_ic_area_sqft");
      SqlBulkCopyColumnMapping colmap5 = new SqlBulkCopyColumnMapping(4, "new_roof_drywell_ic_area_sqft");
      SqlBulkCopyColumnMapping colmap6 = new SqlBulkCopyColumnMapping(5, "new_park_area_sqft");
      SqlBulkCopyColumnMapping colmap7 = new SqlBulkCopyColumnMapping(6, "new_park_disco_ic_area_sqft");
      SqlBulkCopyColumnMapping colmap8 = new SqlBulkCopyColumnMapping(7, "new_park_drywell_ic_area_sqft");
      bulkcopy.ColumnMappings.Add(colmap1);
      bulkcopy.ColumnMappings.Add(colmap2);
      bulkcopy.ColumnMappings.Add(colmap3);
      bulkcopy.ColumnMappings.Add(colmap4);
      bulkcopy.ColumnMappings.Add(colmap5);
      bulkcopy.ColumnMappings.Add(colmap6);
      bulkcopy.ColumnMappings.Add(colmap7);
      bulkcopy.ColumnMappings.Add(colmap8);
      bulkcopy.WriteToServer(dtUserUpdate);
      bulkcopy.Close();
      sqlCon.Close();

      

      //create qcCOunter variable that is used to increment the number of QC checks that fail to pass
      int qcCounter;
      qcCounter = PerformDscQc();

      //run SelectPendingImpAreaUpdates      

      if (qcCounter > 0)
      {
        MessageBox.Show("One or more errors/warnings were encountered during the check of " +
                        "the user input table.  Please review the tab(s) and correct " +
                        "the corresponding data in the user input table.", "DSCUpdater: Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      try
      {
        //the following are extracted methods that add SQL command parameters
        AddEditDateCommandParameter(sqlCmd, sqlCon);
        AddEditedByCommandParameter(sqlCmd, sqlCon);
        AddNewParkAreaCommandParameter(sqlCmd, sqlCon);
        AddNewParkAreaDrywellCommandParameter(sqlCmd, sqlCon);
        AddNewParkDISCOAreaCommandParameter(sqlCmd, sqlCon);
        AddNewRoofAreaCommandParameter(sqlCmd, sqlCon);
        AddNewRoofAreaDrywellCommandParameter(sqlCmd, sqlCon);
        AddNewRoofDISCOAreaCommandParameter(sqlCmd, sqlCon);
        AddEditIDCommandParameter(sqlCmd);

        //the following are extracted methods based on batch queries:          
        BatchDSCEDITAPPENDQueries(sqlCmd);
        BatchNewICRecords(sqlCmd);
        BatchUpdateMasterICTables(sqlCmd);
        BatchUpdateDSCAreas(sqlCmd);
        BatchDSCEDIT(sqlCmd);
        BatchSESSION(sqlCmd);

        sqlCmd.CommandText = "INSERT INTO IMPUPDATE SELECT dsc_edit_id, dsc_id, new_roof_area_sqft, " +
                             "old_roof_area_sqft, new_park_area_sqft, old_park_area_sqft FROM [DSCEDIT] " +
                             "WHERE ((([DSCEDIT].[new_roof_area_sqft])<>[DSCEDIT].[old_roof_area_sqft]) " +
                             "AND (([DSCEDIT].[edit_id])=@editID)) " +
                             "OR ((([DSCEDIT].[new_park_area_sqft])<>[DSCEDIT].[old_park_area_sqft]) " +
                             "AND (([DSCEDIT].[edit_id])=@editID))";
        sqlCmd.ExecuteNonQuery();

        ExportIMPUPDATEToCSV();

        sqlCmd.CommandText = "DELETE FROM IMPUPDATE";
        sqlCmd.ExecuteNonQuery();

        BatchDeleteFromSQlTables(sqlCmd);

        //close the SQL connection
        sqlCon.Close();

        //HACK: there must be a better way to accomlish this...
        //btnSubmitUpdates.Visible = false;

        btnCancel.Text = "Return";
        MessageBox.Show("All updates to the modeling system have completed sucessfully.  To review changes from this edit session, return to the main page, and click on the 'Load Update History' button to load the desired edit session.", "DSCUpdater: Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

        try
        {
          SendImpAEmail();
        }
        catch (Exception ex)
        {
          MessageBox.Show("Unable to send email - Your changes have not been committed" + ex.Message);
          return;
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "DSCUpdater: Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      SetStatus("Ready");
      SetProgress(0);
      Cursor.Current = Cursors.WaitCursor;
      qcCounter = 0;
    }

    private int PerformDscQc()
    {
      int qcCount = 0;

      string strSQLImpAQC = "SELECT mst_DSC_ac.DSCID AS [DSC ID], " +
                     "mst_DSC_ac.surveyedRfAreaSqFt AS [Surveyed Roof Area], " +
                     "mst_DSC_ac.surveyedPkAreaSqft AS [Surveyed Park Area] " +
                     "FROM mst_DSC_ac INNER JOIN USERUPDATE ON " +
                     "mst_DSC_ac.DSCID = USERUPDATE.dsc_id " +
                     "WHERE (((mst_DSC_ac.roofAreaNeedsUpdate)='True')) " +
                     "OR (((mst_DSC_ac.parkAreaNeedsUpdate)='True'))";

      SqlDataAdapter sqlDAImpAQC = new SqlDataAdapter(strSQLImpAQC, Properties.Settings.Default.DscEditorConnectionString);
      SqlCommandBuilder sqlCBImpAQC = new SqlCommandBuilder(sqlDAImpAQC);
      DataTable dtImpAQC = new DataTable();
      sqlDAImpAQC.Fill(dtImpAQC);
      //MessageBox.Show("ImpAQC = " + Convert.ToString(dtImpAQC.Rows.Count));
      dtImpAQC.Locale = System.Globalization.CultureInfo.InvariantCulture;


      //run SelectUSERUPDATENotInDSC query
      string strSQLDSCQC =
        "SELECT USERUPDATE.dsc_id as 'DSC ID', " +
        DscErrors.PendingUpdate + " as 'ErrorCode' " +
        "null as 'ErrorDescription'" +
        "FROM mst_DSC_ac RIGHT OUTER JOIN USERUPDATE " +
        "ON mst_DSC_ac.DSCID = USERUPDATE.dsc_id " +
        "WHERE (mst_DSC_ac.DSCID IS NULL)";

      SqlDataAdapter sqlDADSCQC = new SqlDataAdapter(strSQLDSCQC, Properties.Settings.Default.DscEditorConnectionString);
      SqlCommandBuilder sqlCBDSCQC = new SqlCommandBuilder(sqlDADSCQC);
      DataTable dtDSCQC = new DataTable();
      sqlDADSCQC.Fill(dtDSCQC);
      dtDSCQC.Locale = System.Globalization.CultureInfo.InvariantCulture;

      //run SelectParkICAreaGreaterThanNewParkArea
      string strSQLParkQC =
        "SELECT USERUPDATE.dsc_id, " +
        DscErrors.ParkICGreaterThanParkArea + " as 'ErrorCode' " +
        "USERUPDATE.new_park_disco_ic_area_sqft & '+' & " +
        "USERUPDATE.new_park_drywell_ic_area_sqft & '>' & " +
        "USERUPDATE.new_park_area_sqft as 'ErrorDescription' " +
        "FROM USERUPDATE " +
        "WHERE ([new_park_disco_ic_area_sqft]+[new_park_drywell_ic_area_sqft])>[new_park_area_sqft]";

      SqlDataAdapter sqlDAParkQC = new SqlDataAdapter(strSQLParkQC, Properties.Settings.Default.DscEditorConnectionString);
      SqlCommandBuilder sqlCBParkQC = new SqlCommandBuilder(sqlDAParkQC);
      DataTable dtParkQC = new DataTable();

      //fill Park QC data adapter
      sqlDAParkQC.Fill(dtParkQC);
      dtParkQC.Locale = System.Globalization.CultureInfo.InvariantCulture;

      //run SelectRoofICAreaGreaterThanNewRoofArea
      string strSQLRoofQC =
        "SELECT USERUPDATE.dsc_id, " +
        DscErrors.RoofICGreaterThanRoofArea + " as 'ErrorCode' " +
        "USERUPDATE.new_roof_disco_ic_area_sqft & '+' &" +
        "USERUPDATE.new_roof_drywell_ic_area_sqft & '>' &" +
        "USERUPDATE.new_roof_area_sqft as 'ErrorDescription' " +
        "FROM USERUPDATE WHERE " +
        "([new_roof_disco_ic_area_sqft]+[new_roof_drywell_ic_area_sqft])>[new_roof_area_sqft]";


      SqlDataAdapter sqlDARoofQC = new SqlDataAdapter(strSQLRoofQC, Properties.Settings.Default.DscEditorConnectionString);
      SqlCommandBuilder sqlCBRoofQC = new SqlCommandBuilder(sqlDARoofQC);
      DataTable dtRoofQC = new DataTable();
      sqlDARoofQC.Fill(dtRoofQC);
      dtRoofQC.Locale = System.Globalization.CultureInfo.InvariantCulture;

      return qcCount;
    }

    private void btnLoadSelectedEditSession_Click(object sender, EventArgs e)
    {
      btnUpdaterHistoryReturn.Visible = true;
      if (dgvUpdaterHistory.SelectedRows.Count > 1)
      {
        MessageBox.Show("Only one Editor History session may be selected at a time.", "DSC Editor History: Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      else if (dgvUpdaterHistory.SelectedRows.Count == 0)
      {
        MessageBox.Show("No Editor History session selected. Please select valid Editor History session.", "DSC Editor History: Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.DscEditorConnectionString);
        sqlCon.Open();
        SqlCommand sqlCmd = new SqlCommand();
        AddSESSIONEditIDCommandParameter(sqlCmd);
        sqlCmd.CommandText = "SELECT dsc_edit_id, edit_id, edit_date, " +
                             "edited_by, rno, dsc_id, old_roof_area_sqft, new_roof_area_sqft, " +
                             "old_roof_disco_ic_area_sqft, new_roof_disco_ic_area_sqft, " +
                             "old_roof_drywell_ic_area_sqft, new_roof_drywell_ic_area_sqft, " +
                             "old_park_area_sqft, new_park_area_sqft, old_park_disco_ic_area_sqft, " +
                             "new_park_disco_ic_area_sqft, " +
                             "old_park_drywell_ic_area_sqft, new_park_drywell_ic_area_sqft, " +
                             "updater_editor_value_changed " +
                             "FROM DSCEDIT WHERE (DSCEDIT.edit_id = @sessionEditID)";

        sqlCmd.Connection = sqlCon;
        daUpdaterEditor = new SqlDataAdapter(sqlCmd);
        //this should be called dtUpdaterEditor
        dtUpdaterEditor = new DataTable();
        //MessageBox.Show(dtUpdaterEditor.Rows.Count.ToString());
        daUpdaterEditor.Fill(dtUpdaterEditor);
        dtUpdaterEditor.Locale = System.Globalization.CultureInfo.InvariantCulture;
        BindingSource bsSQL = new BindingSource();
        bsSQL.DataSource = dtUpdaterEditor;
        bindingNavigator2.BindingSource = bsSQL;
        dgvUpdaterEditor.DataSource = bsSQL;

        //begin tab control
        txtNewRoofArea.Enabled = false;
        txtNewParkArea.Enabled = false;
        txtNewParkDISCOICArea.Enabled = false;
        txtNewParkDrywellICArea.Enabled = false;
        txtNewRoofDISCOICArea.Enabled = false;
        txtNewRoofDrywellICArea.Enabled = false;
        btnRevertSession.Enabled = true;
        btnUpdaterEditorEnter.Enabled = true;
        btnSubmitUpdaterEditorChanges.Enabled = true;
        btnUpdaterEditorClear.Enabled = true;
        btnUpdaterEditorCloseCancel.Text = "Cancel";
        //MessageBox.Show(dgvUpdaterEditor.RowCount.ToString());
      }
    }

    private void btnLoadUpdateFile_Click(object sender, EventArgs e)
    {
      /// <summary>
      /// Open TEMP.CSV as ODBC database file
      /// Access data using SQL 'Select' command
      /// Move data from DATABASE to DataTable and assign to DataGridView object
      /// Make each column UNSORTABLE to stop user messing with data!!!
      /// </summary>

      SetStatus("Loading");

      //Open file dialog handling
      ofdMain.DefaultExt = "*.csv";
      ofdMain.FileName = "*.csv";
      ofdMain.Filter = "csv files|*.csv|txt files|*.txt";
      DialogResult result = ofdMain.ShowDialog();

      //Check to make sure the OFD dialog result is "OK"
      if (result != DialogResult.OK)
      {
        return;
      }
      txtFileName.Text = ofdMain.FileName;

      // Clear datagrid contents
      dgvData.SelectAll();
      dgvData.ClearSelection();
      // Set file name
      fileName = txtFileName.Text;
      csvDataSource = fileName;
      if (File.Exists(tempFileName))
      {
        File.Delete(tempFileName);
      }

      StreamReader sr = new StreamReader(fileName);
      StreamWriter sw = new StreamWriter(tempFileName);

      // Read & dump header
      string junk = sr.ReadLine();

      // Read file into string       
      string FileData = sr.ReadToEnd();

      FileSize = FileData.Length.ToString("N");
      FileSize = FileSize.Substring(0, FileSize.IndexOf("."));
      //SetStatus(= "Loading" + FileSize + "  bytes. Please wait a moment.";

      sw.WriteLine("RNO,DSCID,New Roof Area,New Roof DISCO IC Area,New Roof Drywell IC Area, New Park Area, New Park DISCO IC Area, New Park Drywell IC Area");
      //sw.WriteLine(LineText);
      sw.Write(FileData);
      sr.Close();
      sw.Close();
      ReadData();
      dgvData.Update();
      dgvData.Columns[0].HeaderText = "RNO";
      dgvData.Columns[1].HeaderText = "DSCID";
      dgvData.Columns[2].HeaderText = "New Roof Area";
      dgvData.Columns[3].HeaderText = "New Roof DISCO IC Area";
      dgvData.Columns[4].HeaderText = "New Roof Drywell IC Area";
      dgvData.Columns[5].HeaderText = "New Park Area";
      dgvData.Columns[6].HeaderText = "New Park DISCO IC Area";
      dgvData.Columns[7].HeaderText = "New Park Drywell IC Area";

      SetProgress(0);

      NavigateTo(tabPageControlLoadedUpdateReview.Tab.Key);

      SetStatus("Ready");
    }

    private void NavigateTo(string tabKey)
    {
      try
      {
        Cursor = Cursors.WaitCursor;
        tabControlMain.SelectedTab = tabControlMain.Tabs[tabKey];
      }
      catch
      {
        MessageBox.Show("Could not navigate to tab '" + tabKey + "'.");
        tabControlMain.SelectedTab = tabControlMain.Tabs[0];
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }

    private void btnUpdaterEditorEnter_Click(object sender, EventArgs e)
    {
      #region NewParkArea

      try
      {
        if (txtNewParkArea.Text != "")
        {
          newParkArea = Convert.ToInt32(txtNewParkArea.Text);
          dgvUpdaterEditor.SelectedCells[13].Value = Convert.ToInt32(txtNewParkArea.Text);
          dgvUpdaterEditor.SelectedCells[18].Value = Convert.ToBoolean(true);
        }
        else
        {
          newParkArea = 0;
        }
        if (newParkArea < (newParkDISCOICArea + newParkDrywellICArea))
        {
          MessageBox.Show("The sum of the parking IC areas can not be greater than the total parking area.  Please revise your DISCO and/or drywell IC areas.", "Excessive Park IC Area(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          txtNewParkDISCOICArea.Text = "";
          txtNewParkDrywellICArea.Text = "";
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "DSCUpdater: Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtNewParkArea.Text = "";
      }
      #endregion

      #region NewParkDISCOICArea
      try
      {
        if (txtNewParkDISCOICArea.Text != "")
        {
          newParkDISCOICArea = Convert.ToInt32(txtNewParkDISCOICArea.Text);
          dgvUpdaterEditor.SelectedCells[15].Value = Convert.ToInt32(txtNewParkDISCOICArea.Text);
          dgvUpdaterEditor.SelectedCells[18].Value = Convert.ToBoolean(true);
        }
        else
        {
          newParkDISCOICArea = 0;
        }
        if (newParkArea < (newParkDISCOICArea + newParkDrywellICArea))
        {
          MessageBox.Show("The sum of the parking IC areas can not be greater than the total parking area.  Please revise your DISCO and/or drywell IC areas.", "Excessive Park IC Area(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          txtNewParkDISCOICArea.Text = "";
        }
      }

      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "DSCUpdater: Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtNewParkDISCOICArea.Text = "";
      }
      #endregion

      #region NewParkDrywellICArea

      try
      {
        if (txtNewParkDrywellICArea.Text != "")
        {
          newParkDrywellICArea = Convert.ToInt32(txtNewParkDrywellICArea.Text);
          dgvUpdaterEditor.SelectedCells[17].Value = Convert.ToInt32(txtNewParkDrywellICArea.Text);
          dgvUpdaterEditor.SelectedCells[18].Value = Convert.ToBoolean(true);
        }
        else
        {
          newParkDrywellICArea = 0;
        }
        if (newParkArea < (newParkDISCOICArea + newParkDrywellICArea))
        {
          MessageBox.Show("The sum of the park IC areas can not be greater than the total park area.  Please revise your DISCO and/or drywell IC areas.", "Excessive Park IC Area(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          txtNewParkDrywellICArea.Text = "";
        }
      }

      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "DSCUpdater: Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtNewRoofDrywellICArea.Text = "";
      }

      #endregion

      #region NewRoofArea

      try
      {
        if (txtNewRoofArea.Text != "")
        {
          newRoofArea = Convert.ToInt32(txtNewRoofArea.Text);
          dgvUpdaterEditor.SelectedCells[7].Value = Convert.ToInt32(txtNewRoofArea.Text);
          dgvUpdaterEditor.SelectedCells[18].Value = Convert.ToBoolean(true);
        }
        else
        {
          newRoofArea = 0;
        }
        if (newRoofArea < (newRoofDISCOICArea + newRoofDrywellArea))
        {
          MessageBox.Show("The sum of the roof IC areas can not be greater than the total roof area.  Please revise your DISCO and/or drywell IC areas.", "Excessive Roof IC Area(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          txtNewRoofDISCOICArea.Text = "";
          txtNewRoofDrywellICArea.Text = "";
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "DSCUpdater: Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtNewRoofArea.Text = "";
      }

      #endregion

      #region NewRoofDISCOICArea

      try
      {
        if (txtNewRoofDISCOICArea.Text != "")
        {
          newRoofDISCOICArea = Convert.ToInt32(txtNewRoofDISCOICArea.Text);
          dgvUpdaterEditor.SelectedCells[9].Value = Convert.ToInt32(txtNewRoofDISCOICArea.Text);
          dgvUpdaterEditor.SelectedCells[18].Value = Convert.ToBoolean(true);
        }
        else
        {
          newRoofDISCOICArea = 0;
        }
        if (newRoofArea < (newRoofDISCOICArea + newRoofDrywellArea))
        {
          MessageBox.Show("The sum of the roof IC areas can not be greater than the total roof area.  Please revise your DISCO and/or drywell IC areas.", "Excessive Roof IC Area(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          txtNewRoofDISCOICArea.Text = "";
        }
      }

      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "DSCUpdater: Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtNewRoofDISCOICArea.Text = "";
      }

      #endregion

      #region NewRoofDrywellICArea

      try
      {
        if (txtNewRoofDrywellICArea.Text != "")
        {
          newRoofDrywellArea = Convert.ToInt32(txtNewRoofDrywellICArea.Text);
          dgvUpdaterEditor.SelectedCells[11].Value = Convert.ToInt32(txtNewRoofDrywellICArea.Text);
          dgvUpdaterEditor.SelectedCells[18].Value = Convert.ToBoolean(true);
        }
        else
        {
          newRoofDrywellArea = 0;
        }
        if (newRoofArea < (newRoofDISCOICArea + newRoofDrywellArea))
        {
          MessageBox.Show("The sum of the roof IC areas can not be greater than the total roof area.  Please revise your DISCO and/or drywell IC areas.", "Excessive Roof IC Area(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          txtNewRoofDrywellICArea.Text = "";
        }
      }

      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "DSCUpdater: Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtNewRoofDrywellICArea.Text = "";
      }

      #endregion

      txtNewParkArea.Text = "";
      txtNewParkDISCOICArea.Text = "";
      txtNewParkDrywellICArea.Text = "";
      txtNewRoofArea.Text = "";
      txtNewRoofDISCOICArea.Text = "";
      txtNewRoofDrywellICArea.Text = "";
    }

    private void btnUpdaterEditorClear_Click(object sender, EventArgs e)
    {
      txtNewParkDISCOICArea.Text = "";
      txtNewParkDrywellICArea.Text = "";
      txtNewParkArea.Text = "";

      txtNewRoofDISCOICArea.Text = "";
      txtNewRoofDrywellICArea.Text = "";
      txtNewRoofArea.Text = "";

      newParkDISCOICArea = 0;
      newParkDrywellICArea = 0;
      newParkArea = 0;

      newRoofDISCOICArea = 0;
      newRoofDrywellArea = 0;
      newRoofArea = 0;
    }

    private void btnSubmitUpdaterEditorChanges_Click(object sender, EventArgs e)
    {
      SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.DscEditorConnectionString);
      sqlCon.Open();
      SqlCommand sqlCmd = new SqlCommand();
      sqlCmd.CommandText = "DELETE FROM DSCEDITAPPEND";
      sqlCmd.Connection = sqlCon;
      sqlCmd.ExecuteNonQuery();

      SqlBulkCopy bulkcopy = new SqlBulkCopy(sqlCon);
      bulkcopy.DestinationTableName = "DSCEDITAPPEND";
      bulkcopy.WriteToServer(dtUpdaterEditor);
      bulkcopy.Close();
      sqlCon.Close();

      AddEditDateCommandParameter(sqlCmd, sqlCon);
      AddEditedByCommandParameter(sqlCmd, sqlCon);
      AddEditIDCommandParameter(sqlCmd);

      sqlCmd.CommandText = "UPDATE DSCEDITAPPEND SET edit_id=@editID,edit_date=@editDate, " +
                           "edited_by=@editedBy";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "UPDATE [DSCEDITAPPEND] SET old_roof_area_sqft = DSCEDIT.new_roof_area_sqft, " +
                           "old_roof_disco_ic_area_sqft = DSCEDIT.new_roof_disco_ic_area_sqft, " +
                           "old_roof_drywell_ic_area_sqft = DSCEDIT.new_roof_drywell_ic_area_sqft, " +
                           "old_park_area_sqft = DSCEDIT.new_park_area_sqft, " +
                           "old_park_disco_ic_area_sqft = DSCEDIT.new_park_disco_ic_area_sqft, " +
                           "old_park_drywell_ic_area_sqft = DSCEDIT.new_park_drywell_ic_area_sqft " +
                           "FROM [DSCEDITAPPEND] INNER JOIN [DSCEDIT] ON " +
                           "[DSCEDITAPPEND].[dsc_edit_id] = [DSCEDIT].[dsc_edit_id]" +
                           "WHERE ((DSCEDIT.new_roof_area_sqft<>DSCEDITAPPEND.new_roof_area_sqft)) " +
                           "OR ((DSCEDIT.new_roof_disco_ic_area_sqft<>DSCEDITAPPEND.new_roof_disco_ic_area_sqft)) " +
                           "OR ((DSCEDIT.new_roof_drywell_ic_area_sqft<>DSCEDITAPPEND.new_roof_drywell_ic_area_sqft)) " +
                           "OR ((DSCEDIT.new_park_area_sqft<>DSCEDITAPPEND.new_park_area_sqft)) " +
                           "OR ((DSCEDIT.new_park_disco_ic_area_sqft<>DSCEDITAPPEND.new_park_disco_ic_area_sqft)) " +
                           "OR ((DSCEDIT.new_park_drywell_ic_area_sqft<>DSCEDITAPPEND.new_park_drywell_ic_area_sqft))";
      sqlCmd.ExecuteNonQuery();

      BatchNewICRecords(sqlCmd);
      BatchUpdateMasterICTables(sqlCmd);
      BatchUpdateDSCAreas(sqlCmd);
      BatchSESSION(sqlCmd);

      sqlCmd.CommandText = "UPDATE DSCEDITAPPEND SET dsc_edit_id=0";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO DSCEDIT (edit_id, " +
                           "edit_date, edited_by, rno, dsc_id, old_roof_area_sqft, " +
                           "new_roof_area_sqft, old_roof_disco_ic_area_sqft, " +
                           "new_roof_disco_ic_area_sqft, old_roof_drywell_ic_area_sqft, " +
                           "new_roof_drywell_ic_area_sqft, old_park_area_sqft, " +
                           "new_park_area_sqft, old_park_disco_ic_area_sqft, " +
                           "new_park_disco_ic_area_sqft, old_park_drywell_ic_area_sqft, " +
                           "new_park_drywell_ic_area_sqft) " +
                           "SELECT " +
                           "DSCEDITAPPEND.edit_id AS edit_id, " +
                           "DSCEDITAPPEND.edit_date AS edit_date, " +
                           "DSCEDITAPPEND.edited_by AS edited_by, " +
                           "DSCEDITAPPEND.RNO AS rno, " +
                           "DSCEDITAPPEND.dsc_id AS dsc_id, " +
                           "DSCEDITAPPEND.old_roof_area_sqft AS old_roof_area_sqft, " +
                           "DSCEDITAPPEND.new_roof_area_sqft AS new_roof_area_sqft, " +
                           "DSCEDITAPPEND.old_roof_disco_ic_area_sqft AS old_roof_disco_ic_area_sqft, " +
                           "DSCEDITAPPEND.new_roof_disco_ic_area_sqft AS new_roof_disco_ic_area_sqft, " +
                           "DSCEDITAPPEND.old_roof_drywell_ic_area_sqft AS old_roof_drywell_ic_area_sqft, " +
                           "DSCEDITAPPEND.new_roof_drywell_ic_area_sqft AS new_roof_drywell_ic_area_sqft, " +
                           "DSCEDITAPPEND.old_park_area_sqft AS old_park_area_sqft, " +
                           "DSCEDITAPPEND.new_park_area_sqft AS new_park_area_sqft, " +
                           "DSCEDITAPPEND.old_park_disco_ic_area_sqft AS old_park_disco_ic_area_sqft, " +
                           "DSCEDITAPPEND.new_park_disco_ic_area_sqft AS new_park_disco_ic_area_sqft, " +
                           "DSCEDITAPPEND.old_park_drywell_ic_area_sqft AS old_park_drywell_ic_area_sqft, " +
                           "DSCEDITAPPEND.new_park_drywell_ic_area_sqft AS new_park_drywell_ic_area_sqft " +
                           "FROM DSCEDITAPPEND";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "UPDATE DSCEDIT " +
                           "SET updater_editor_value_changed = 'False'";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DELETE FROM DSCEDITAPPEND";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "DELETE FROM IMPUPDATE";
      sqlCmd.ExecuteNonQuery();
      sqlCmd.CommandText = "INSERT INTO IMPUPDATE SELECT dsc_edit_id, dsc_id, new_roof_area_sqft, " +
                           "old_roof_area_sqft, new_park_area_sqft, old_park_area_sqft FROM [DSCEDIT] " +
                           "WHERE ((([DSCEDIT].[new_roof_area_sqft])<>[DSCEDIT].[old_roof_area_sqft]) " +
                           "AND (([DSCEDIT].[edit_id])=@editID)) " +
                           "OR ((([DSCEDIT].[new_park_area_sqft])<>[DSCEDIT].[old_park_area_sqft]) " +
                           "AND (([DSCEDIT].[edit_id])=@editID))";
      sqlCmd.ExecuteNonQuery();

      ExportIMPUPDATEToCSV();

      sqlCmd.CommandText = "DELETE FROM IMPUPDATE";
      sqlCmd.ExecuteNonQuery();

      sqlCon.Close();
      btnUpdaterEditorCloseCancel.Text = "Return";
      btnUpdaterHistoryReturn.Visible = false;
      MessageBox.Show("Changes submitted.  Internal email will now be sent from Outlook", "DSCEditor: Changes Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
      btnSubmitUpdaterEditorChanges.Enabled = false;
      SendImpAEmail();
    }

    private void btnRevertSession_Click(object sender, EventArgs e)
    {
      int dgvRowCount = 0;
      dgvRowCount = dgvUpdaterEditor.RowCount;
      DialogResult dr = MessageBox.Show(dgvRowCount + " records will be reverted.  Do you wish to continue? (Changes can only be undone by submitting a new update file)", "Confirm Revert Operation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (dr == DialogResult.Yes)
      {
        int maxEditID = 0;
        int editorEditID = 0;
        dgvUpdaterEditor.Rows[0].Selected = true;
        editorEditID = Convert.ToInt32(dgvUpdaterEditor.SelectedCells[1].Value);
        SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.DscEditorConnectionString);
        sqlCon.Open();
        SqlCommand sqlCmd = new SqlCommand();

        AddEditorEditIDCommandParameter(sqlCmd);

        sqlCmd.CommandText = "DELETE FROM DSCEDITAPPEND";
        sqlCmd.Connection = sqlCon;
        sqlCmd.ExecuteNonQuery();
        sqlCmd.CommandText = "SELECT dsc_edit_id, edit_id,edit_date, " +
                             "edited_by, rno, dsc_id, old_roof_area_sqft, new_roof_area_sqft, " +
                             "old_roof_disco_ic_area_sqft, new_roof_disco_ic_area_sqft, " +
                             "old_roof_drywell_ic_area_sqft, new_roof_drywell_ic_area_sqft, " +
                             "old_park_area_sqft, new_park_area_sqft, old_park_disco_ic_area_sqft " +
                             "new_park_disco_ic_area_sqft, old_park_disco_ic_area_sqft, " +
                             "old_park_drywell_ic_area_sqft, new_park_drywell_ic_area_sqft, " +
                             "updater_editor_value_changed " +
                             "FROM DSCEDIT WHERE (DSCEDIT.edit_id = @editorEditID)";
        daUpdaterEditor = new SqlDataAdapter(sqlCmd);
        dtUpdaterEditor = new DataTable();
        daUpdaterEditor.Fill(dtUpdaterEditor);
        dtUpdaterEditor.Locale = System.Globalization.CultureInfo.InvariantCulture;

        SqlBulkCopy bulkcopy = new SqlBulkCopy(sqlCon);
        bulkcopy.DestinationTableName = "DSCEDITAPPEND";
        bulkcopy.WriteToServer(dtUpdaterEditor);
        bulkcopy.Close();
        sqlCon.Close();

        sqlCon.Open();
        sqlCmd.CommandText = "SELECT Max(DSCEDIT.edit_id) FROM DSCEDIT " +
                             "INNER JOIN DSCEDITAPPEND ON DSCEDIT.dsc_id = DSCEDITAPPEND.dsc_id";
        maxEditID = Convert.ToInt32(sqlCmd.ExecuteScalar());
        SqlParameter pMaxEditID = sqlCmd.CreateParameter();
        pMaxEditID.ParameterName = "@maxEditID";
        pMaxEditID.SqlDbType = SqlDbType.Int;
        pMaxEditID.Value = maxEditID;
        pMaxEditID.Direction = ParameterDirection.Input;
        sqlCmd.Parameters.Add(pMaxEditID);

        if (maxEditID > editorEditID)
        {
          MessageBox.Show("There is at least one record that has been edited in a subsequent edit session.  Try reverting from a later edit session.", "DSCUpdater: Updater Editor", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        else
        {
          BatchRevertICEdits(sqlCmd);
          AddEditIDCommandParameter(sqlCmd);
          AddEditDateCommandParameter(sqlCmd, sqlCon);
          AddEditedByCommandParameter(sqlCmd, sqlCon);
          BatchSESSION(sqlCmd);

          sqlCmd.CommandText = "UPDATE DSCEDITAPPEND " +
                               "SET DSCEDITAPPEND.edit_id = @editID, " +
                               "DSCEDITAPPEND.edit_date = @editDate, " +
                               "DSCEDITAPPEND.edited_by = @editedBy";
          sqlCmd.ExecuteNonQuery();

          sqlCmd.CommandText = "INSERT INTO DSCEDIT " +
                               "(edit_id, edit_date, edited_by, rno, dsc_id, " +
                               "old_roof_area_sqft, new_roof_area_sqft, " +
                               "old_roof_disco_ic_area_sqft, new_roof_disco_ic_area_sqft, " +
                               "old_roof_drywell_ic_area_sqft, new_roof_drywell_ic_area_sqft, " +
                               "old_park_area_sqft, new_park_area_sqft, " +
                               "old_park_disco_ic_area_sqft, new_park_disco_ic_area_sqft, " +
                               "old_park_drywell_ic_area_sqft, updater_editor_value_changed) " +
                               "SELECT DSCEDITAPPEND.edit_id, " +
                               "DSCEDITAPPEND.edit_date, " +
                               "DSCEDITAPPEND.edited_by, " +
                               "DSCEDITAPPEND.rno, " +
                               "DSCEDITAPPEND.dsc_id, " +
                               "DSCEDITAPPEND.old_roof_area_sqft, " +
                               "DSCEDITAPPEND.new_roof_area_sqft, " +
                               "DSCEDITAPPEND.old_roof_disco_ic_area_sqft, " +
                               "DSCEDITAPPEND.new_roof_disco_ic_area_sqft, " +
                               "DSCEDITAPPEND.old_roof_drywell_ic_area_sqft, " +
                               "DSCEDITAPPEND.new_roof_drywell_ic_area_sqft, " +
                               "DSCEDITAPPEND.old_park_area_sqft, " +
                               "DSCEDITAPPEND.new_park_area_sqft, " +
                               "DSCEDITAPPEND.old_park_disco_ic_area_sqft, " +
                               "DSCEDITAPPEND.new_park_disco_ic_area_sqft, " +
                               "DSCEDITAPPEND.old_park_drywell_ic_area_sqft, " +
                               "DSCEDITAPPEND.updater_editor_value_changed " +
                               "FROM DSCEDITAPPEND";
          sqlCmd.ExecuteNonQuery();

          sqlCmd.CommandText = "DELETE FROM IMPUPDATE";
          sqlCmd.ExecuteNonQuery();

          sqlCmd.CommandText = "INSERT INTO IMPUPDATE (dsc_edit_id, dsc_id, old_roof_area_sqft, " +
                               "new_roof_area_sqft, old_park_area_sqft, new_park_area_sqft) " +
                               "SELECT DSCEDITAPPEND.dsc_edit_id,  DSCEDITAPPEND.dsc_id, DSCEDITAPPEND.new_roof_area_sqft, " +
                               "DSCEDITAPPEND.old_roof_area_sqft, DSCEDITAPPEND.new_park_area_sqft, " +
                               "DSCEDITAPPEND.old_park_area_sqft FROM DSCEDITAPPEND WHERE " +
                               "((old_roof_area_sqft<>new_roof_area_sqft) OR " +
                               "(old_park_area_sqft<>new_park_area_sqft))";
          sqlCmd.ExecuteNonQuery();

          ExportIMPUPDATEToCSV();

          sqlCmd.CommandText = "DELETE FROM IMPUPDATE";
          sqlCmd.ExecuteNonQuery();

          //update mst_DSC_ac records to previous impervious area values (original values from previous edit session)
          sqlCmd.CommandText = "UPDATE [mst_DSC_ac] " +
                               "SET [mst_DSC_ac].[RfAreaFtEX] = [DSCEDITAPPEND].[old_roof_area_sqft], " +
                               "[mst_DSC_ac].[PkAreaFtEX] = [DSCEDITAPPEND].[old_park_area_sqft] " +
                               "FROM [mst_DSC_ac] " +
                               "INNER JOIN [DSCEDITAPPEND] " +
                               "ON [mst_DSC_ac].[DSCID] = [DSCEDITAPPEND].[dsc_id]";
          sqlCmd.ExecuteNonQuery();

          //update mst_DSC_ac records to previous IC values (original values from previous edit session)
          sqlCmd.CommandText = "UPDATE [mst_DSC_ac] " +
                               "SET [mst_DSC_ac].[ICFtRoofEX] = " +
                               "([DSCEDITAPPEND].[old_roof_disco_ic_area_sqft] + " +
                               "[DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft]), " +
                               "[mst_DSC_ac].[EICFtRoofEX] = " +
                               "(([DSCEDITAPPEND].[old_roof_disco_ic_area_sqft]*0.7)+ " +
                               "[DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft]), " +
                               "[mst_DSC_ac].[ICFtParkEX] = " +
                               "([DSCEDITAPPEND].[old_park_disco_ic_area_sqft]+ " +
                               "[DSCEDITAPPEND].[old_park_drywell_ic_area_sqft]), " +
                               "[mst_DSC_ac].[EICFtParkEX] = " +
                               "(([DSCEDITAPPEND].[old_park_disco_ic_area_sqft]*0.7) + " +
                               "[DSCEDITAPPEND].[old_roof_drywell_ic_area_sqft]) " +
                               "FROM [mst_DSC_ac] INNER JOIN [DSCEDITAPPEND] " +
                               "ON [mst_DSC_ac].[DSCID] = [DSCEDITAPPEND].[dsc_id] " +
                               "WHERE (([new_roof_disco_ic_area_sqft]<>[old_roof_disco_ic_area_sqft]) " +
                               "AND ([new_roof_disco_ic_area_sqft]<>[old_roof_disco_ic_area_sqft]) " +
                               "AND ([new_park_disco_ic_area_sqft]<>[old_park_disco_ic_area_sqft]) " +
                               "AND ([new_park_disco_ic_area_sqft]<>[old_park_disco_ic_area_sqft])) " +
                               "OR (([new_roof_drywell_ic_area_sqft]<>[old_roof_drywell_ic_area_sqft]) " +
                               "AND ([new_roof_drywell_ic_area_sqft]<>[old_roof_drywell_ic_area_sqft]) " +
                               "AND ([new_park_drywell_ic_area_sqft]<>[old_park_drywell_ic_area_sqft]) " +
                               "AND ([new_park_drywell_ic_area_sqft]<>[old_park_drywell_ic_area_sqft]))";
          sqlCmd.ExecuteNonQuery();
          sqlCmd.CommandText = "DELETE FROM DSCEDITAPPEND";
          sqlCmd.ExecuteNonQuery();

          MessageBox.Show("Edit session reverted.", "DSCUpdater: Updater Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
          btnRevertSession.Enabled = false;
          btnUpdaterEditorEnter.Enabled = false;
          btnSubmitUpdaterEditorChanges.Enabled = false;
          btnUpdaterEditorClear.Enabled = false;
          btnUpdaterEditorCloseCancel.Text = "Close Editor";
          dtUpdaterEditor.Clear();
          dgvUpdaterEditor.Refresh();
          SendImpAEmail();
        }
      }
    }

    private void btnUpdaterEditorCloseCancel_Click(object sender, EventArgs e)
    {
      tabControlMain.Visible = false;
    }

    private void btnCancelDownload_Click(object sender, EventArgs e)
    {

    }

    private void btnCancelUpdate_Click(object sender, EventArgs e)
    {

    }

    private void downloadUpdateTemplateToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void loadUpdateFileToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void loadUpdaterHistoryToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void btnCloseUpdaterHistory_Click(object sender, EventArgs e)
    {

    }

    private void checkRETROUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CheckRetroUpdates();
    }

    private void applyRETROUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ApplyRetroUpdates();
    }

    private void btnViewNewRetroAssessments_Click(object sender, EventArgs e)
    {
      try
      {
        ((DataView)dgvIncomingRetroChanges.DataSource).Table.Clear();
        //TO-DO: add code to populate the DataGridView showing the incoming new site assessments
      }
      catch (Exception ex)
      {
        MessageBox.Show("Could not load incoming changes: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

    }

    private void btnViewNewIcTargets_Click(object sender, EventArgs e)
    {
      try
      {
        ((DataView)dgvIncomingRetroChanges.DataSource).Table.Clear();
        //TO-DO: add code to populate the DataGridView showing incoming new IC targets
      }

      catch (Exception ex)
      {
        MessageBox.Show("Could not load incoming changes: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

    }

    private void btnViewNewConstrFacs_Click(object sender, EventArgs e)
    {
      try
      {
        ((DataView)dgvIncomingRetroChanges.DataSource).Table.Clear();
        //TO-DO: add code to populate the DataGridView showing incoming new constructed IC facilities
      }

      catch (Exception ex)
      {
        MessageBox.Show("Could Not Load Incoming Changes: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnApplyRetroUpdates_Click(object sender, EventArgs e)
    {
      //TO-DO: create parameter to count the number of incoming new site assessments from RETRO DB
      //Need to think about everything that comprises a new assessment (i.e., updated impervious area, ic area, etc.)
      //Parameter will be passed to the message box confirming the updates will be applied
      int countOfNewSiteAssessments = 0;

      //TO-DO: create parameter to count the number of incoming new ic targets from RETRO DB
      //Parameter will be passed to the message box confirming the updates will be applied
      int countOfNewPotentialICs = 0;

      //TO-DO: create parameter to count the number of constrcuted IC from the RETRO DB
      //Parameter will be passed to the message box confirming the updates will be applied
      int countOfNewCOnstructedICs = 0;

      MessageBox.Show("The following updates will be applied:" + "\r\n" +
        countOfNewSiteAssessments + " new site assessments will be applied to master modeling tables." + "\r\n" +
        countOfNewPotentialICs + " new potential inflow controls will be added to the IC Alts GIS coverage(s)." + "\r\n" +
        countOfNewSiteAssessments + " new constructed inflow controls will be added to the IC Alt GIS coverage(s)." + "\r\n", "Confirm Update", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
    }

    private void toolStripStatusLabel2_Click(object sender, EventArgs e)
    {
      UpdateDscEditorConnection();
    }

    private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
    {

    }

    private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
    {
      switch (e.Tool.SharedProps.Category)
      {
        case "loadTab":
          LoadTab(e.Tool.Key);
          return;
        default:
          break;
      }
      switch (e.Tool.Key)
      {
        case "changeDscEditorConnection":    // ButtonTool
          UpdateDscEditorConnection();
          break;
        case "changeMasterDataConnection":    // ButtonTool
          UpdateMasterDataConnection();
          break;
        case "downloadTemplate":    // ButtonTool
          DownloadUpdateTemplate();
          break;
        case "about":
          string fi;
          fi = @"\\Cassio\asm_apps\Apps\DSCUpdater\DSCUpdaterPublish.htm";
          System.Diagnostics.Process.Start("IEXPLORE.EXE", fi);
          return;
        case "exit":
          Application.Exit();
          return;
        default:
          MessageBox.Show("Tool '" + e.Tool.Key + "' not implemented");
          return;
      }
    }

    private void UpdateDscEditorConnection()
    {
      Microsoft.Data.ConnectionUI.DataConnectionDialog dataConnectionDialog = new
      Microsoft.Data.ConnectionUI.DataConnectionDialog();

      Microsoft.Data.ConnectionUI.DataSource.AddStandardDataSources(dataConnectionDialog);

      string dscEditorConnectionString = Properties.Settings.Default.DscEditorConnectionString;
      dataConnectionDialog.SelectedDataSource = Microsoft.Data.ConnectionUI.DataSource.SqlDataSource;
      dataConnectionDialog.SelectedDataProvider = Microsoft.Data.ConnectionUI.DataProvider.SqlDataProvider;

      dataConnectionDialog.ConnectionString = dscEditorConnectionString;

      if (Microsoft.Data.ConnectionUI.DataConnectionDialog.Show(dataConnectionDialog) != DialogResult.OK)
      {
        return;
      }

      Properties.Settings.Default.SetDscEditorConnectionString = dataConnectionDialog.ConnectionString;
      Properties.Settings.Default.Save();
      ultraStatusBar1.Panels["dscEditorConnection"].Text = "Dsc Editor: " + ConnectionStringSummary(Properties.Settings.Default.DscEditorConnectionString);
      return;
    }

    private void UpdateMasterDataConnection()
    {
      Microsoft.Data.ConnectionUI.DataConnectionDialog dataConnectionDialog = new
      Microsoft.Data.ConnectionUI.DataConnectionDialog();

      Microsoft.Data.ConnectionUI.DataSource.AddStandardDataSources(dataConnectionDialog);

      //TODO: Detect whether Master Data is SQL or Access (Jet) and set SelectedDataSource accordingly
      string masterDataConnectionString = Properties.Settings.Default.MasterDataConnectionString;
      dataConnectionDialog.SelectedDataSource = Microsoft.Data.ConnectionUI.DataSource.AccessDataSource;
      //dataConnectionDialog.SelectedDataProvider = Microsoft.Data.ConnectionUI.DataProvider.OdbcDataProvider;

      dataConnectionDialog.ConnectionString = masterDataConnectionString;

      if (Microsoft.Data.ConnectionUI.DataConnectionDialog.Show(dataConnectionDialog) != DialogResult.OK)
      {
        return;
      }

      Properties.Settings.Default.SetMasterDataConnectionString = dataConnectionDialog.ConnectionString;
      Properties.Settings.Default.Save();
      ultraStatusBar1.Panels["masterDataConnection"].Text = "Master Data: " + ConnectionStringSummary(Properties.Settings.Default.MasterDataConnectionString);
      return;
    }

    private string ConnectionStringSummary(string connectionString)
    {
      System.Data.Common.DbConnectionStringBuilder csb;
      csb = new System.Data.Common.DbConnectionStringBuilder();

      csb.ConnectionString = connectionString;
      string summary = csb["data source"].ToString();
      return summary;
    }

    private void expBarMain_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
    {
      LoadTab(e.Item.Key);
    }

    private void LoadTab(string tabKey)
    {
      try
      {
        Cursor = Cursors.WaitCursor;
        this.tabControlMain.SelectedTab = this.tabControlMain.Tabs[tabKey];
      }
      catch
      {
        MessageBox.Show("Tab '" + tabKey + "' not found.");
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }

    private void tabControlMain_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
    {
      this.Text = "DSC Updater";
      if (e.Tab != null)
      {
        this.Text = this.Text + " - " + e.Tab.Text;
      }
    }

    private void btnRevertSession_Click_1(object sender, EventArgs e)
    {

    }

    private void btnCancel_Click(object sender, MouseEventArgs e)
    {

    }



  }
}

namespace CSharp
{
  public class OutlookMail
  {
    private Microsoft.Office.Interop.Outlook.Application oApp;
    private Microsoft.Office.Interop.Outlook._NameSpace oNameSpace;
    private Microsoft.Office.Interop.Outlook.MAPIFolder oOutboxFolder;

    public OutlookMail()
    {
      oApp = new Microsoft.Office.Interop.Outlook.Application();
      oNameSpace = oApp.GetNamespace("MAPI");
      oNameSpace.Logon(null, null, true, true);
      oOutboxFolder = oNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderOutbox);
    }

    public void AddToOutbox(string toValue, string subjectValue, string bodyValue)
    {
      Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
      String sSource = "C:\\temp\\IMPUPDATE.csv";
      String sDisplayName = "IMPUPDATE";
      int iPosition = 1;
      int iAttachType = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
      Microsoft.Office.Interop.Outlook.Attachment oAttach = oMailItem.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);
      toValue = "jrubengb@gmail.com";
      subjectValue = "Request for Impervious Area Update";
      bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the impervious area coverage." + "\r\n" +
                "The attached table lists parcels by DSCID that are in need of updates in the modeling system.";
      oMailItem.To = toValue;
      oMailItem.Subject = subjectValue;
      oMailItem.Body = bodyValue;
      oMailItem.SaveSentMessageFolder = oOutboxFolder;
      oMailItem.Send();
    }
  }

}



