
#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Data;
using System.Data.DataSetExtensions;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.SqlServerCe;
using System.Linq;
using System.Linq.Expressions;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Security.Principal;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer;
using Microsoft.Office;
using Microsoft.VisualBasic;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Utils.AccessUtils;

#endregion

namespace DSCUpdater
{
  public partial class frmMain : Form
  {
    #region Global Variables
    SqlDataAdapter daUpdaterEditor;
    DataTable dtUpdaterEditor;
    DateTime retrofitsStartDate;
    DateTime retrofitsEndDate;
    #endregion

    #region cleanup
    /*
    public static DataTable ToADOTable<T>(
           this IEnumerable<T> varlist, CreateRowDelegate<T> fn)
    {
      DataTable dtReturn = new DataTable();
      // Could add a check to verify that there is an element 0
      T TopRec = varlist.ElementAt(0);

      // Use reflection to get property names, to create table
      // column names

      PropertyInfo[] oProps =
              ((Type)TopRec.GetType()).GetProperties();

      foreach (PropertyInfo pi in oProps)
        dtReturn.Columns.Add(
          pi.Name, pi.PropertyType);


      foreach (T rec in varlist)
      {
        DataRow dr = dtReturn.NewRow();
        foreach (PropertyInfo pi in oProps)
          dr[pi.Name] = pi.GetValue(rec, null);
        dtReturn.Rows.Add(dr);
      }

      return (dtReturn);
    }
    public delegate object[] CreateRowDelegate<T>(T t);
     */
    #endregion

#region Declarations
    ArrayList arrTables;
#endregion

    public frmMain()
    {
      InitializeComponent();
    }

    private int SetProgress
    {
      get
      {
        return this.statusBarMain.Panels["progressBar"].ProgressBarInfo.Value;
      }
      set
      {
        this.statusBarMain.Panels["progressBar"].ProgressBarInfo.Value = value;
      }
    }

    private void SetStatus(string status)
    {
      this.statusBarMain.Panels["status"].Text = status;
    }

    private string ConnectionStringSummary(string connectionString)
    {
      System.Data.Common.DbConnectionStringBuilder csb;
      csb = new System.Data.Common.DbConnectionStringBuilder();

      csb.ConnectionString = connectionString;
      string summary = csb["data source"].ToString();
      return summary;
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
      statusBarMain.Panels["dscEditorConnection"].Text = "Dsc Editor: " + ConnectionStringSummary(Properties.Settings.Default.DscEditorConnectionString);
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
      statusBarMain.Panels["masterDataConnection"].Text = "Master Data: " + ConnectionStringSummary(Properties.Settings.Default.MasterDataConnectionString);
      return;
    }

    private void UpdateFfeDataConnection()
    {
      Microsoft.Data.ConnectionUI.DataConnectionDialog dataConnectionDialog = new
      Microsoft.Data.ConnectionUI.DataConnectionDialog();
      Microsoft.Data.ConnectionUI.DataSource.AddStandardDataSources(dataConnectionDialog);

      //TODO: Detect whether Master Data is SQL or Access (Jet) and set SelectedDataSource accordingly
      string ffeDataConnectionString = Properties.Settings.Default.FfeDataConnectionString;
      dataConnectionDialog.SelectedDataSource = Microsoft.Data.ConnectionUI.DataSource.AccessDataSource;
      dataConnectionDialog.ConnectionString = ffeDataConnectionString;

      if (Microsoft.Data.ConnectionUI.DataConnectionDialog.Show(dataConnectionDialog) != DialogResult.OK)
      {
        return;
      }

      Properties.Settings.Default.SetFfeDataConnectionString = dataConnectionDialog.ConnectionString;
      Properties.Settings.Default.Save();
      statusBarMain.Panels["ffeDataConnection"].Text = "FFE Data: " + ConnectionStringSummary(Properties.Settings.Default.FfeDataConnectionString);
      return;
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

    /// <summary>
    /// Resets the UI to the initial state for beginning the Update process
    /// </summary>
    private void RestartUpdate()
    {
      LoadTab(tabPageControlMain.Tab.Key);
      projectDataSet.DscUpdater.Clear();
      projectDataSet.DscQc.Clear();
      dgvData.Visible = false;
      btnSubmitUpdates.Enabled = false;
      txtFileName.Clear();
    }

    private void DownloadUpdateTemplate()
    {
      string templatePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Template\\UserUpdateTemplate.csv";
      SaveFileDialog sfdMain = new SaveFileDialog();
      sfdMain.Title = "Where do you want to save the UserUpdate file?";
      sfdMain.InitialDirectory = @"C:\";
      sfdMain.FileName = "UserUpdate.csv";
      if (sfdMain.ShowDialog() == DialogResult.OK)
      {
        File.Copy(templatePath, sfdMain.FileName, true);
      }
    }

    /// <summary>
    /// Copies the user instance of the Dsc Update template file to the master location
    /// </summary>
    /// <param name="fileName">The path to the user Dsc Update template file</param>
    private void WriteDscUpdateFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException();
      }

      string dscUpdateFile = Properties.Settings.Default.DscUpdateFile;
      if (File.Exists(dscUpdateFile))
      {
        File.Delete(dscUpdateFile);
      }
      File.Copy(fileName, dscUpdateFile, true);
    }

    private bool PrepareUpdateFile(string fileName)
    {
      WriteDscUpdateFile(fileName);
      LoadMstData();

      int qcCount = 0;
      SetProgress = 0;

      projectDataSet.DscQc.Clear();

      // build the pending update query
      qcCount += QCQueryPendingUpdate(projectDataSet.DscQc);

      SetProgress = 50;
      qcCount += QCQueryDscNotFound(projectDataSet.DscQc);

      SetProgress = 75;
      qcCount += QCQueryRoofIcArea(projectDataSet.DscQc);

      SetProgress = 95;
      qcCount += QCQueryParkIcArea(projectDataSet.DscQc);

      SetProgress = 0;
      return qcCount == 0;
    }

    private int QCQueryPendingUpdate(DataTable qcDt)
    {
      int qcCount = 0;

      var qryPendingUpdate =
        from m in projectDataSet.MstDsc
        join d in projectDataSet.DscUpdater
        on m.DSCID equals d.DscId
        where m.roofAreaNeedsUpdate == true ||
        m.parkAreaNeedsUpdate == true
        select new
        {
          DscID = m.DSCID,
          ErrorCode = (string)DscErrors.PendingUpdate,
          ErrorDescription = d.DscId + " has pending updates"
        };

      foreach (var row in qryPendingUpdate)
      {
        qcDt.Rows.Add(row.DscID, row.ErrorCode, row.ErrorDescription);
        SetProgress++;
        qcCount++;
      }
      return qcCount;
    }

    private int QCQueryDscNotFound(DataTable qcDt)
    {
      int qcCount = 0;
      var qryDscNotFound =
        from d in projectDataSet.DscUpdater
        join m in projectDataSet.MstDsc
        on d.DscId equals m.DSCID
        into dJoin
        from outerJoin in dJoin.DefaultIfEmpty()
        where outerJoin == null
        select new
        {
          DscID = -1,
          ErrorCode = DscErrors.DscNotFound,
          ErrorDescription = d.DscId + " does not exist in master data",
        };

      foreach (var row in qryDscNotFound)
      {
        qcDt.Rows.Add(row.DscID, row.ErrorCode, row.ErrorDescription);
        SetProgress++;
        qcCount++;
      }
      return qcCount;
    }

    private int QCQueryRoofIcArea(DataTable qcDt)
    {
      int qcCount = 0;
      var qryRoofIcAreaGreaterThanNewRoofArea =
        from m in projectDataSet.MstDsc
        join d in projectDataSet.DscUpdater
        on m.DSCID equals d.DscId
        where d.NewRoofDiscoArea + d.NewRoofDrywellArea > d.NewRoofArea
        select new
        {
          DscID = m.DSCID,
          ErrorCode = DscErrors.RoofICGreaterThanRoofArea,
          ErrorDescription = d.NewRoofDiscoArea + " + " + d.NewRoofDrywellArea + " > " + d.NewRoofArea
        };

      foreach (var row in qryRoofIcAreaGreaterThanNewRoofArea)
      {
        qcDt.Rows.Add(row.DscID, row.ErrorCode, row.ErrorDescription);
        SetProgress++;
        qcCount++;
      }
      return qcCount;
    }

    private int QCQueryParkIcArea(DataTable qcDt)
    {
      int qcCount = 0;

      var qryParkIcAreaGreaterThanNewParkArea =
        from m in projectDataSet.MstDsc
        join d in projectDataSet.DscUpdater
        on m.DSCID equals d.DscId
        where d.NewParkDiscoArea + d.NewParkDrywellArea > d.NewParkArea
        select new
        {
          DscID = m.DSCID,
          ErrorCode = DscErrors.ParkICGreaterThanParkArea,
          ErrorDescription = d.NewParkDiscoArea + " + " + d.NewParkDrywellArea + " > " + d.NewParkArea
        };

      foreach (var row in qryParkIcAreaGreaterThanNewParkArea)
      {
        qcDt.Rows.Add(row.DscID, row.ErrorCode, row.ErrorDescription);
        SetProgress++;
        qcCount++;
      }
      return qcCount;
    }

    private void UpdateDscTables()
    {
      /*
       * 1) Obtain a list of DSCIDs that don't already exist in IC table
       * 2) Append new IC records if don't exist already
       * 3) Update new IC records with new data (2 & 3 can be merged
       * */
      int editSessionId = UpdateTableSession();

      foreach (ProjectDataSet.DscUpdaterRow dscUpdaterRow in projectDataSet.DscUpdater)
      {
        int dscId = dscUpdaterRow.DscId;
        //Add any needed DiscoVeg Roof ICs
        UpdateMstDiscoVegRecord(dscId, (int)dscUpdaterRow.NewRoofDiscoArea, (int)dscUpdaterRow.NewParkDiscoArea);

        //Add any needed Drywell Roof ICs
        UpdateMstDrywellRecord(dscId, (int)dscUpdaterRow.NewRoofDrywellArea, (int)dscUpdaterRow.NewParkDrywellArea);

        UpdateMstDscRecord(dscId, (int)dscUpdaterRow.NewRoofArea, (int)dscUpdaterRow.NewParkArea);

        UpdateDscEdit(editSessionId, dscUpdaterRow);
      }
      return;
      #region old sql queries
      //run AppendNewParkDISCORecords queries
      SqlCommand sqlCmd = null;
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
      #endregion
    }

    private void UpdateMstDiscoVegRecord(int dscId, int newRoofDiscoArea, int newParkDiscoArea)
    {
      ProjectDataSet.MstIcDiscoVegRow mstIcDiscoVegRow = projectDataSet.MstIcDiscoVeg.FindBydscIDRoofRParkTimeFrame(dscId, "R", "EX");
      if (mstIcDiscoVegRow == null)
      {
        mstIcDiscoVegRow = projectDataSet.MstIcDiscoVeg.AddMstIcDiscoVegRow(
          dscId, Convert.ToInt32(dscId / 100), Convert.ToInt32(dscId % 100),
          "R", "DDEX", "EX", "EX", null, null,
          0, .7, "DSCUpdater", DateTime.Now.ToString("yyyyMMdd"));
      }
      mstIcDiscoVegRow.SqFt = newRoofDiscoArea;

      //Add any needed DiscoVeg Park ICs
      mstIcDiscoVegRow = projectDataSet.MstIcDiscoVeg.FindBydscIDRoofRParkTimeFrame(dscId, "P", "EX");
      if (mstIcDiscoVegRow == null)
      {
        mstIcDiscoVegRow = projectDataSet.MstIcDiscoVeg.AddMstIcDiscoVegRow(
          dscId, Convert.ToInt32(dscId / 100), Convert.ToInt32(dscId % 100),
          "P", "DDEX", "EX", "EX", null, null,
          0, .7, "DSCUpdater", DateTime.Now.ToString("yyyyMMdd"));
      }
      mstIcDiscoVegRow.SqFt = newParkDiscoArea;
    }

    private void UpdateMstDrywellRecord(int dscId, int newRoofDrywellArea, int newParkDrywellArea)
    {
      ProjectDataSet.MstIcDrywellRow mstIcDrywellRow = projectDataSet.MstIcDrywell.FindBydscIDRoofRParkTimeFrame(dscId, "R", "EX");
      if (mstIcDrywellRow == null)
      {
        mstIcDrywellRow = projectDataSet.MstIcDrywell.AddMstIcDrywellRow(
          dscId, Convert.ToInt32(dscId / 100), Convert.ToInt32(dscId % 100),
          "R", "DDEX", "EX", "EX", null, null,
          0, "DSCUpdater", DateTime.Now.ToString("yyyyMMdd"));
      }
      mstIcDrywellRow.SqFt = newRoofDrywellArea;

      //Add any needed Drywell Park ICs
      mstIcDrywellRow = projectDataSet.MstIcDrywell.FindBydscIDRoofRParkTimeFrame(dscId, "P", "EX");
      if (mstIcDrywellRow == null)
      {
        mstIcDrywellRow = projectDataSet.MstIcDrywell.AddMstIcDrywellRow(
          dscId, Convert.ToInt32(dscId / 100), Convert.ToInt32(dscId % 100),
          "P", "DDEX", "EX", "EX", null, null,
          0, "DSCUpdater", DateTime.Now.ToString("yyyyMMdd"));
      }
      mstIcDrywellRow.SqFt = newParkDrywellArea;
    }

    private void UpdateMstDscRecord(int dscId, int newRoofArea, int newParkArea)
    {
      ProjectDataSet.MstDscRow mstDscRow = projectDataSet.MstDsc.FindByDSCID(dscId);
      if (mstDscRow == null)
      {
        throw new Exception("Dsc " + dscId + " not found in master data.");
      }

      mstDscRow.RfAreaFtEX = newRoofArea;
      mstDscRow.PkAreaFtEX = newParkArea;
      return;
    }

    private void UpdateDscEdit(int editSessionId, ProjectDataSet.DscUpdaterRow dscUpdaterRow)
    {
      int dscId;

      int newRoofArea, newParkArea;
      int oldRoofArea, oldParkArea;

      int newRoofDiscoArea, newParkDiscoArea;
      int newRoofDrywellArea, newParkDrywellArea;

      int oldRoofDiscoArea = 0, oldParkDiscoArea = 0; //TODO: What should these values be?
      int oldRoofDrywellArea = 0, oldParkDrywellArea = 0; //TODO: What should these values be?

      dscId = dscUpdaterRow.DscId;
      ProjectDataSet.MstDscRow mstDscRow = projectDataSet.MstDsc.FindByDSCID(dscId);

      oldRoofArea = mstDscRow.RfAreaFtEX;
      oldParkArea = mstDscRow.PkAreaFtEX;

      newRoofArea = (int)dscUpdaterRow.NewRoofArea;
      newParkArea = (int)dscUpdaterRow.NewParkArea;

      newRoofDiscoArea = (int)dscUpdaterRow.NewRoofDiscoArea;
      newParkDiscoArea = (int)dscUpdaterRow.NewParkDiscoArea;

      newRoofDrywellArea = (int)dscUpdaterRow.NewRoofDrywellArea;
      newParkDrywellArea = (int)dscUpdaterRow.NewParkDrywellArea;

      projectDataSet.DSCEDIT.AddDSCEDITRow(projectDataSet.SESSION.FindByedit_id(editSessionId),
        DateTime.Now, Environment.UserName, dscUpdaterRow.RNo, dscId,
        oldRoofArea, newRoofArea, oldRoofDiscoArea, newRoofDiscoArea, oldRoofDrywellArea, newRoofDrywellArea,
        oldParkArea, newParkArea, oldParkDiscoArea, newParkDiscoArea, oldParkDrywellArea, newParkDrywellArea,
        true);

      return;
    }

    private int UpdateTableSession()
    {
      ProjectDataSet.SESSIONRow sessionRow =
        projectDataSet.SESSION.AddSESSIONRow(DateTime.Now, Environment.UserName);

      return sessionRow.edit_id;
    }

    private void LoadUpdaterHistory()
    {
      //MessageBox.Show("LoadUpdaterHistory");     
      this.sessionTableAdapter.Fill(this.projectDataSet.SESSION);
      updaterHistoryBindingNav.BindingSource = sessionBindingSource;
      tabControlMain.Visible = true;
      Cursor.Current = Cursors.Arrow;
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

    //ImpAreaChanges
    private static void SendImpAEmail()
    {
      string toValue = "jrubengb@gmail.com";
      string subjectValue = "Request for Impervious Area Update";
      string bodyValue = "This is an auto-generated email." + "\r\n" +
                "This message is a request for changes to the impervious area coverage." + "\r\n" +
                "The attached table lists parcels by DSCID that are in need of updates in the modeling system.";
      OutlookMail oMail = new OutlookMail();
      oMail.AddToOutboxImpAreaChanges(toValue, subjectValue, bodyValue);
    }

    //AllChanges
    private static void SendRetroAllChangesEmail()
    {
      string toValue = "jrubengb@gmail.com";
      string subjectValue = "Request for DSC and Inflow Control Records Update";
      string bodyValue = "This is an auto-generated email." + "r\n" +
                        "This message is a request for changed to the DSC and Inflow Controls coverages." + "r\n" +
                        "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      OutlookMail oMail = new OutlookMail();
      oMail.AddToOutboxAllRetroChanges(toValue, subjectValue, bodyValue);
    }

    //AssessmentsPotentialChanges
    private static void SendRetroAssessmentPotentialChangesEmail()
    {
      string toValue = "jrubengb@gmail.com";
      string subjectValue = "Request for DSC and Inflow Control Records Update";
      string bodyValue = "This is an auto-generated email." + "r\n" +
                        "This message is a request for changed to the DSC and Inflow Controls coverages." + "r\n" +
                        "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      OutlookMail oMail = new OutlookMail();
      oMail.AddToOutboxAssessmentsPotentialChanges(toValue, subjectValue, bodyValue);
    }

    //AssessmentsConstructedChanges
    private static void SendRetroAssessmentConstructedChangesEmail()
    {
      string toValue = "jrubengb@gmail.com";
      string subjectValue = "Request for DSC and Inflow Control Records Update";
      string bodyValue = "This is an auto-generated email." + "r\n" +
                        "This message is a request for changed to the DSC and Inflow Controls coverages." + "r\n" +
                        "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      OutlookMail oMail = new OutlookMail();
      oMail.AddToOutboxAssessmentConstructedChanges(toValue, subjectValue, bodyValue);
    }

    //AssessmentsChangesOnly
    private static void SendRetroAssessmentsChangesOnlyEmail()
    {
      string toValue = "jrubengb@gmail.com";
      string subjectValue = "Request for DSC Records Update";
      string bodyValue = "This is an auto-generated email." + "r\n" +
                        "This message is a request for changes to DSC coverage." + "r\n" +
                        "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      OutlookMail oMail = new OutlookMail();
      oMail.AddToOutboxAssessmentsChangesOnly(toValue, subjectValue, bodyValue);
    }

    //PotentialConstructedChanges
    private static void SendRetroPotentialConstructedEmail()
    {
      string toValue = "jrubengb@gmail.com";
      string subjectValue = "Request for Inflow Control Records Update";
      string bodyValue = "This is an auto-generated email." + "r\n" +
                        "This message is a request for changes to the Inflow Controls coverage(s)." + "r\n" +
                        "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      OutlookMail oMail = new OutlookMail();
      oMail.AddToOutboxPotentialConstructedChanges(toValue, subjectValue, bodyValue);
    }

    //PotentialChangesOnly
    private static void SendRetroPotentialChangesOnlyEmail()
    {
      string toValue = "jrubengb@gmail.com";
      string subjectValue = "Request for Inflow Control Records Update";
      string bodyValue = "This is an auto-generated email." + "r\n" +
                        "This message is a request for changes to the Inflow Controls coverage(s)." + "r\n" +
                        "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      OutlookMail oMail = new OutlookMail();
      oMail.AddToOutboxPotentialChangesOnly(toValue, subjectValue, bodyValue);
    }

    //ConstructedChangesOnly
    private static void SendRetroConstructedChangesOnlyEmail()
    {
      string toValue = "jrubengb@gmail.com";
      string subjectValue = "Request for Inflow Control Records Update";
      string bodyValue = "This is an auto-generated email." + "r\n" +
                        "This message is a request for changes to the Inflow Controls coverage(s)." + "r\n" +
                        "The attached tables list parcels by DSCID that are in need of updates in the modeling system.";
      OutlookMail oMail = new OutlookMail();
      oMail.AddToOutboxConstructedChangesOnly(toValue, subjectValue, bodyValue);
    }

    //NoChanges
    private static void SendRetroNoChangesEmail()
    {
      string toValue = "jrubengb@gmail.com";
      string subjectValue = "No RETRO Changes Update";
      string bodyValue = "This is an auto-generated email." + "r\n" +
                        "This message is to inform you that no changes were found." + "r\n" +
                        "The attached tables are test tables.";
      OutlookMail oMail = new OutlookMail();
      oMail.AddToOutboxNoChanges(toValue, subjectValue, bodyValue);
    }

    private void LoadMstData()
    {
      ProjectDataSetTableAdapters.DscUpdaterTableAdapter dscUpdaterTA;
      dscUpdaterTA = new ProjectDataSetTableAdapters.DscUpdaterTableAdapter();
      dscUpdaterTA.Fill(projectDataSet.DscUpdater);

      SetStatus("Loading Master Dsc...");
      IEnumerable<int> dscids = from d in projectDataSet.DscUpdater select d.DscId;

      ProjectDataSetTableAdapters.MstDscTableAdapter mstDscTA = new DSCUpdater.ProjectDataSetTableAdapters.MstDscTableAdapter();
      mstDscTA.FillByDscIdList(projectDataSet.MstDsc, dscids);

      ProjectDataSetTableAdapters.MstIcDiscoVegTableAdapter mstIcDiscoVegTA = new DSCUpdater.ProjectDataSetTableAdapters.MstIcDiscoVegTableAdapter();
      mstIcDiscoVegTA.FillByDscIdList(projectDataSet.MstIcDiscoVeg, dscids);

      ProjectDataSetTableAdapters.MstIcDrywellTableAdapter mstIcDrywellTA = new DSCUpdater.ProjectDataSetTableAdapters.MstIcDrywellTableAdapter();
      mstIcDrywellTA.FillByDscIdList(projectDataSet.MstIcDrywell, dscids);

      ProjectDataSetTableAdapters.SESSIONTableAdapter sessionTA = new DSCUpdater.ProjectDataSetTableAdapters.SESSIONTableAdapter();
      sessionTA.Fill(projectDataSet.SESSION);
    }

    private void SaveMstData()
    {
      ProjectDataSet changedProjectDataSet = (ProjectDataSet)projectDataSet.GetChanges();

      ProjectDataSetTableAdapters.DscUpdaterTableAdapter dscUpdaterTA;
      dscUpdaterTA = new ProjectDataSetTableAdapters.DscUpdaterTableAdapter();
      dscUpdaterTA.Update(changedProjectDataSet.DscUpdater);

      SetStatus("Updating Master Dsc...");
      IEnumerable<int> dscids = from d in projectDataSet.DscUpdater select d.DscId;

      ProjectDataSetTableAdapters.MstDscTableAdapter mstDscTA = new DSCUpdater.ProjectDataSetTableAdapters.MstDscTableAdapter();
      mstDscTA.Update(changedProjectDataSet.MstDsc);

      ProjectDataSetTableAdapters.MstIcDiscoVegTableAdapter mstIcDiscoVegTA = new DSCUpdater.ProjectDataSetTableAdapters.MstIcDiscoVegTableAdapter();
      mstIcDiscoVegTA.Update(changedProjectDataSet.MstIcDiscoVeg);

      ProjectDataSetTableAdapters.MstIcDrywellTableAdapter mstIcDrywellTA = new DSCUpdater.ProjectDataSetTableAdapters.MstIcDrywellTableAdapter();
      mstIcDrywellTA.Update(changedProjectDataSet.MstIcDrywell);

      ProjectDataSetTableAdapters.SESSIONTableAdapter sessionTA = new DSCUpdater.ProjectDataSetTableAdapters.SESSIONTableAdapter();
      sessionTA.Update(changedProjectDataSet.SESSION);

      ProjectDataSetTableAdapters.DSCEDITTableAdapter dscEditTA = new DSCUpdater.ProjectDataSetTableAdapters.DSCEDITTableAdapter();
      dscEditTA.Update(changedProjectDataSet.DSCEDIT);

      LoadMstData();
    }

    private static void CheckNewIncomingSiteAssessments()
    {

    }

    private static void CheckNewIncomingIcTargets()
    {

    }

    private static void CheckNewIncomingConstructedICs()
    {

    }

    private void ApplyRetroUpdates()
    {

    }

    public void StoreTableNames()
    {
      DataTable schemaTable;
      arrTables = new ArrayList();
      this.Refresh();

      if (Properties.Settings.Default.FfeDataConnectionString != string.Empty)
      {
        using (OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.FfeDataConnectionString))
        {
          try
          {
            conn.Open();
            schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
              arrTables.Add(schemaTable.Rows[i].ItemArray[2].ToString());
            }
          }
          catch (Exception ex)
          {
            MessageBox.Show(ex.Message, "Connection Error");
          }
        }
      }
    }

    private void RunFfeUpdates()
    {
      OleDbConnection conn;
      OleDbCommand comm;
      OleDbDataAdapter daGeoCodedFfe;
      OleDbDataAdapter daAppend;
      DataSet dsFfe;
      string appendTableName;
      string adapterQueryText;
      
      conn = new OleDbConnection(Properties.Settings.Default.FfeDataConnectionString);
      comm = new OleDbCommand();
      daAppend = new OleDbDataAdapter();
      daGeoCodedFfe = new OleDbDataAdapter();
      dsFfe = new DataSet();
      appendTableName = lstFfeDbTables.SelectedItem.ToString();
      adapterQueryText = "Select * from " + appendTableName;
      comm.Connection = conn;
      
      try
      {
        //open connection to FFE database
        conn.Open();

        //create data table and fill dataset from FFE database append table
        daAppend.SelectCommand = new OleDbCommand(adapterQueryText,conn);
        daAppend.Fill(dsFfe,"Append");

        //create data table and fill dataset from FFE database geocoded_ffe table
        adapterQueryText = "Select * from geocoded_FFE";
        daGeoCodedFfe.SelectCommand = new OleDbCommand(adapterQueryText,conn);
        daGeoCodedFfe.Fill(dsFfe,"GeocodedFfe");

        DataTable dtAppend = dsFfe.Tables["Append"];
        DataTable dtGeoFfe = dsFfe.Tables["GeocodedFfe"];
        
        //select append datatable from FFE dataset into an enumerable Linq collection
        var append = dsFfe.Tables["Append"].AsEnumerable();

        //select geo datatable from FFE dataset into an enumerable Linq collection      
        var geo = dsFfe.Tables["GeocodedFfe"].AsEnumerable();

        var qryGeoApppendCount =
              (from a in append
              join g in geo
              on a.Field<string>("RNO")
              equals g.Field<string>("RNO")
              group g by g.Field<int>("DSCID") into appendGeo
              select new
              {
                DscId = appendGeo.Key,
                DscIdCount = appendGeo.Count()
              })
              .Where(a => a.DscIdCount > 1);

        if (qryGeoApppendCount.Count() > 0)
        {
          throw new Exception("The Append table has duplicates in the Geocoded table. " +
                              "All records from the Append table may have only one corresponding record " +
                              "in the Geocoded table, referenced by DSCID.  Please correct the tables so that " +
                              "the Geocoded table has had unique DSCID values.");
        }

        //append new survey records to the FFE table
        comm.CommandText = "INSERT INTO FFE ( SITEADDR, RNO, " +
                           "SURVEYFFE, NOBSMT, SURVEYDATE, ADDDATE, " +
                           "NOTES ) SELECT " +
                           appendTableName + ".SITEADDR,   " +
                           appendTableName + ".RNO,   " +
                           appendTableName + ".SURVEYFFE,   " +
                           appendTableName + ".NOBSMT,   " +
                           appendTableName + ".SURVEYDATE,   " +
                           appendTableName + ".ADDDATE,   " +
                           appendTableName + ".NOTES FROM  " +
                           appendTableName;
        comm.ExecuteNonQuery();
        
        //update DSCID in FFE table using geocoded_FFE table
        comm.CommandType = CommandType.StoredProcedure;
        comm.CommandText = "Update_DSCID_From_Geocoded_FFE";
        comm.ExecuteNonQuery();

        //update mst_DSC_ac table based on FFE table values
        comm.CommandType = CommandType.StoredProcedure;
        comm.CommandText = "UpdateFFE";
        comm.ExecuteNonQuery();

        //update AddDate values in FFE table for recently added records
        comm.CommandType = CommandType.StoredProcedure;
        comm.CommandText = "UpdateFFEAddDate";
        comm.ExecuteNonQuery();

        //close connection to FFE database
        conn.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "FFE Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    public static void ExportToCSV(DataTable dt, string strFilePath, string fileName)
    {
      var sw = new StreamWriter(strFilePath + fileName, false);

      // Write the headers.
      int iColCount = dt.Columns.Count;
      for (int i = 0; i < iColCount; i++)
      {
        sw.Write(dt.Columns[i]);
        if (i < iColCount - 1) sw.Write(",");
      }

      sw.Write(sw.NewLine);

      // Write rows.
      foreach (DataRow dr in dt.Rows)
      {
        for (int i = 0; i < iColCount; i++)
        {
          if (!Convert.IsDBNull(dr[i]))
          {
            if (dr[i].ToString().StartsWith("0"))
            {
              sw.Write(@"=""" + dr[i] + @"""");
            }
            else
            {
              sw.Write(dr[i].ToString());
            }
          }

          if (i < iColCount - 1) sw.Write(",");
        }
        sw.Write(sw.NewLine);
      }

      sw.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      RestartUpdate();
    }

    private void btnLoadUpdateFile_Click(object sender, EventArgs e)
    {
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

      Cursor = Cursors.WaitCursor;

      btnSubmitUpdates.Enabled = false;
      txtFileName.Text = ofdMain.FileName;

      if (!PrepareUpdateFile(txtFileName.Text))
      {
        LoadTab("tabUpdateFileErrors");
        return;
      }

      Cursor = Cursors.Default;
      btnSubmitUpdates.Enabled = true;
      dgvData.Visible = true;
      SetStatus("Ready");
    }

    private void btnSubmitUpdates_Click(object sender, EventArgs e)
    {

      SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.DscEditorConnectionString);
      sqlCon.Open();
      SqlCommand sqlCmd = new SqlCommand();

      //run SelectPendingImpAreaUpdates      

      Cursor.Current = Cursors.WaitCursor;
      SetStatus("Submitting");

      if (!PrepareUpdateFile(txtFileName.Text))
      {
        LoadTab("tabUpdateFileErrors");
        return;
      }

      try
      {
        //the following are extracted methods based on batch queries:                  
        UpdateDscTables();

        if (projectDataSet.HasErrors)
        {
          projectDataSet.RejectChanges();
          MessageBox.Show("Errors were found in the update tables. No changes commited.");
          return;
        }
        SaveMstData();

        //TODO: What does IMPUPDATE table do? Do I need to write to this table here?
        /*NOTE FROM CODER: IMPUPDATE is a list of DSCs whose Impervious areas need to be updated.
         *The DSCUpdater populates the IMPUPDATE table for each edit session and then exports the SQL table
         *to Excel .csv format and emails to the impervious area maintainer
         *See the ImpAEmail method in the frmMain class and the
         *AddToOutboxImpAreaChanges method in the Outlook class for instances where the exported IMPUPDATE table
         *are attached to the email and sent to the data maintenance person.
        sqlCmd.CommandText = "INSERT INTO IMPUPDATE SELECT dsc_edit_id, dsc_id, new_roof_area_sqft, " +
                             "old_roof_area_sqft, new_park_area_sqft, old_park_area_sqft FROM [DSCEDIT] " +
                             "WHERE ((([DSCEDIT].[new_roof_area_sqft])<>[DSCEDIT].[old_roof_area_sqft]) " +
                             "AND (([DSCEDIT].[edit_id])=@editID)) " +
                             "OR ((([DSCEDIT].[new_park_area_sqft])<>[DSCEDIT].[old_park_area_sqft]) " +
                             "AND (([DSCEDIT].[edit_id])=@editID))";
        sqlCmd.ExecuteNonQuery();

        ExportIMPUPDATEToCSV();
        */
        MessageBox.Show("All updates to the modeling system have completed sucessfully.  To review changes from this edit session, return to the main page, and click on the 'Load Update History' button to load the desired edit session.", "DSCUpdater: Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "DSCUpdater: Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      finally
      {
        SetStatus("Ready");
        SetProgress = 0;
        Cursor.Current = Cursors.WaitCursor;
        RestartUpdate();
      }

      LoadTab("tabUpdaterHistory");

      try
      {
        SendImpAEmail();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Unable to send email - Impervious Area Update Request has not been logged." + ex.Message);
        return;
      }

      return;
    }

    private void btnCancelUpdate_Click(object sender, EventArgs e)
    {
      RestartUpdate();
    }

    private void btnLoadSelectedEditSession_Click(object sender, EventArgs e)
    {
      btnUpdaterHistoryReturn.Visible = true;
      if (dgvUpdaterHistory.SelectedRows.Count > 1)
      {
        MessageBox.Show("Only one Editor History session may be selected at a time.", "DSC Editor History: Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      else if (dgvUpdaterHistory.SelectedRows.Count == 0)
      {
        MessageBox.Show("No Editor History session selected. Please select valid Editor History session.", "DSC Editor History: Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }

      int editId = (int)dgvUpdaterHistory.SelectedRows[0].Cells["edit_id"].Value;
      ProjectDataSetTableAdapters.DSCEDITTableAdapter dscEditTA = new DSCUpdater.ProjectDataSetTableAdapters.DSCEDITTableAdapter();
      dscEditTA.FillByEditId(projectDataSet.DSCEDIT, editId);

      LoadTab("tabUpdaterEditor");
      return;

      SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.DscEditorConnectionString);
      sqlCon.Open();
      SqlCommand sqlCmd = new SqlCommand();

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

    private void btnCloseUpdaterHistory_Click(object sender, EventArgs e)
    {
      RestartUpdate();
    }

    private void btnUpdaterHistoryReturn_Click(object sender, EventArgs e)
    {
      if (projectDataSet.HasChanges())
      {
        if (MessageBox.Show(
          "Abandon Edits?", "Warning: Unsaved Changes",
          MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
          != DialogResult.OK)
        {
          return;
        }
      }
      projectDataSet.RejectChanges();
      LoadTab("tabUpdaterHistory");
    }

    private void btnUpdaterEditorEnter_Click(object sender, EventArgs e)
    {
      int newRoofDrywellArea = 0;
      int newRoofDISCOICArea = 0;
      int newParkDrywellICArea = 0;
      int newParkDISCOICArea = 0;
      int newParkICArea = 0;
      int newRoofICArea = 0;
      int newRoofArea = 0;
      int newParkArea = 0;

      #region NewParkArea

      try
      {
        if (txtNewParkArea.Text != "")
        {
          newParkArea = Convert.ToInt32(txtNewParkArea.Text);
          dgvUpdaterEditor.Selected.Cells[13].Value = Convert.ToInt32(txtNewParkArea.Text);
          dgvUpdaterEditor.Selected.Cells[18].Value = Convert.ToBoolean(true);
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
          dgvUpdaterEditor.Selected.Cells[15].Value = Convert.ToInt32(txtNewParkDISCOICArea.Text);
          dgvUpdaterEditor.Selected.Cells[18].Value = Convert.ToBoolean(true);
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
          dgvUpdaterEditor.Selected.Cells[17].Value = Convert.ToInt32(txtNewParkDrywellICArea.Text);
          dgvUpdaterEditor.Selected.Cells[18].Value = Convert.ToBoolean(true);
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
          dgvUpdaterEditor.Selected.Cells[7].Value = Convert.ToInt32(txtNewRoofArea.Text);
          dgvUpdaterEditor.Selected.Cells[18].Value = Convert.ToBoolean(true);
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
          dgvUpdaterEditor.Selected.Cells[9].Value = Convert.ToInt32(txtNewRoofDISCOICArea.Text);
          dgvUpdaterEditor.Selected.Cells[18].Value = Convert.ToBoolean(true);
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
          dgvUpdaterEditor.Selected.Cells[11].Value = Convert.ToInt32(txtNewRoofDrywellICArea.Text);
          dgvUpdaterEditor.Selected.Cells[18].Value = Convert.ToBoolean(true);
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
      if (dgvUpdaterEditor.Selected.Rows.Count == 0)
      {
        return;
      }

      if (dgvUpdaterEditor.Selected.Rows.Count != 1)
      {
        return;
      }
      txtNewParkArea.Enabled = true;
      txtNewRoofArea.Enabled = true;
      txtNewParkDISCOICArea.Enabled = true;
      txtNewParkDrywellICArea.Enabled = true;
      txtNewRoofDISCOICArea.Enabled = true;
      txtNewRoofDrywellICArea.Enabled = true;

      if (dgvUpdaterEditor.Selected.Rows[0].Cells[4].Value.ToString() != "" && dgvUpdaterEditor.Selected.Rows[0].Cells[18].Value.ToString() == "False")
      {
        //MessageBox.Show("If1");
        rNoLblText = "RNO: Not Available";
        currentParkAreaLblText = "Current park area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[13].Value.ToString();
        currentParkDiscoICAreaLblText = "Current park DISCO IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[15].Value.ToString(); ;
        currentParkDrywellICAreaLblText = "Current park drywell IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[17].Value.ToString();
        currentRoofAreaLblText = "Current roof area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[7].Value.ToString();
        currentRoofDiscoICAreaLblText = "Current roof DISCO IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[9].Value.ToString();
        currentRoofDrywellICAreaLblText = "Current roof drywell IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[11].Value.ToString();
      }

      if (dgvUpdaterEditor.Selected.Rows[0].Cells[4].Value.ToString() != "" && dgvUpdaterEditor.Selected.Rows[0].Cells[18].Value.ToString() == "True")
      {
        //MessageBox.Show("If2");
        rNoLblText = "RNO: " + dgvUpdaterEditor.Selected.Rows[0].Cells[4].Value.ToString();
        currentParkAreaLblText = "Updated park area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[13].Value.ToString();
        currentParkDiscoICAreaLblText = "Updated park DISCO IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[15].Value.ToString(); ;
        currentParkDrywellICAreaLblText = "Updated park drywell IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[17].Value.ToString();
        currentRoofAreaLblText = "Updated roof area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[7].Value.ToString();
        currentRoofDiscoICAreaLblText = "Updated roof DISCO IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[9].Value.ToString();
        currentRoofDrywellICAreaLblText = "Updated roof drywell IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[11].Value.ToString();
      }

      if (dgvUpdaterEditor.Selected.Rows[0].Cells[4].Value.ToString() == "" && dgvUpdaterEditor.Selected.Rows[0].Cells[18].Value.ToString() == "False")
      {
        //MessageBox.Show("If3");
        rNoLblText = "RNO: Not Available";
        currentParkAreaLblText = "Current park area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[13].Value.ToString();
        currentParkDiscoICAreaLblText = "Current park DISCO IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[15].Value.ToString(); ;
        currentParkDrywellICAreaLblText = "Current park drywell IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[17].Value.ToString();
        currentRoofAreaLblText = "Current roof area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[7].Value.ToString();
        currentRoofDiscoICAreaLblText = "Current roof DISCO IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[9].Value.ToString();
        currentRoofDrywellICAreaLblText = "Current roof drywell IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[11].Value.ToString();
      }

      if (dgvUpdaterEditor.Selected.Rows[0].Cells[4].Value.ToString() == "" && dgvUpdaterEditor.Selected.Rows[0].Cells[18].Value.ToString() == "True")
      {
        //MessageBox.Show("If4");
        rNoLblText = "RNO: Not Available";
        currentParkAreaLblText = "Updated park area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[13].Value.ToString();
        currentParkDiscoICAreaLblText = "Updated park DISCO IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[15].Value.ToString(); ;
        currentParkDrywellICAreaLblText = "Updated park drywell IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[17].Value.ToString();
        currentRoofAreaLblText = "Updated roof area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[7].Value.ToString();
        currentRoofDiscoICAreaLblText = "Updated roof DISCO IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[9].Value.ToString();
        currentRoofDrywellICAreaLblText = "Updated roof drywell IC area (sqft): " + dgvUpdaterEditor.Selected.Rows[0].Cells[11].Value.ToString();
      }
      lblSelectedEditorRNO.Text = rNoLblText;
      lblSelectedEditorPkArea.Text = currentParkAreaLblText;
      lblSelectedEditorPkDISCOICArea.Text = currentParkDiscoICAreaLblText;
      lblSelectedEditorPkDrywellICArea.Text = currentParkDrywellICAreaLblText;
      lblSelectedEditorRfArea.Text = currentRoofAreaLblText;
      lblSelectedEditorRfDISCOICArea.Text = currentRoofDiscoICAreaLblText;
      lblSelectedEditorRfDrywellICArea.Text = currentRoofDrywellICAreaLblText;
    }

    private void dgvUpdaterEditor_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
    {
      //TODO: Update text fields to reflect current selected data grid row.
    }

    private void btnViewNewRetroAssessments_Click(object sender, EventArgs e)
    {
      if (Convert.ToString(dgvIncomingRetroChanges.DataSource) != "")
      {
        dgvIncomingRetroChanges.DataSource = null;
      }

      try
      {
        rDS = new RetrofitsDataSet();
        rDS.InitRetroDataSet();
        int newSiteAssessmentsCount;
        DataTable assessment = new DataTable();
        DataView assessmentDataView = new DataView();

        SystemsAnalysis.DataAccess.RetrofitsDataSetTableAdapters.SiteAssessmentTableAdapter assessmentTA;
        assessmentTA = new SystemsAnalysis.DataAccess.RetrofitsDataSetTableAdapters.SiteAssessmentTableAdapter();
        assessmentTA.FilterFillSiteAssessment(rDS.SiteAssessment, clndrRetrofitsChangesStart.Value, clndrRetrofitsChangesEnd.Value);   

        //QRY_USERFRIENDLYTableAdapter qryUserFriendlyTA;
        //qryUserFriendlyTA = new SystemsAnalysis.DataAccess.RetrofitsDataSetTableAdapters.QRY_USERFRIENDLYTableAdapter();
        //qryUserFriendlyTA.FillByDateRange(rDS.QRY_USERFRIENDLY, clndrRetrofitsChanges.SelectionStart, clndrRetrofitsChanges.SelectionEnd);

          IEnumerable<int> qrySiteAssessMentsCount =
            from r in rDS.SiteAssessment
            where r.startDate >= retrofitsStartDate && r.endDate <= retrofitsEndDate
            select r.site_assessment_id;

          newSiteAssessmentsCount = qrySiteAssessMentsCount.Count();

          if (newSiteAssessmentsCount > 0)
          {
            assessment = rDS.SiteAssessment;
            EnumerableRowCollection<DataRow> qrySelectNewSiteAssessmentRecords =
            (from r in assessment.AsEnumerable()
             where r.Field<DateTime>("startDate") >= retrofitsStartDate && r.Field<DateTime>("endDate") <= System.DateTime.Now
             select r);
            
            //assessment = rDS.Tables["SITE_ASSESSMENT"];
            //EnumerableRowCollection<DataRow> qrySelectNewSiteAssessmentRecords =
            //(from r in assessment.AsEnumerable()
            // where r.Field<DateTime>("update_date") >= retrofitsStartDate && r.Field<DateTime>("update_date") <= System.DateTime.Now
            // select r);

            assessmentDataView = qrySelectNewSiteAssessmentRecords.AsDataView();

            DataTable siteAssessmentsDataTable = assessmentDataView.Table.Clone();
            foreach (DataRowView drv in assessmentDataView)
            {
              siteAssessmentsDataTable.ImportRow(drv.Row);
            }

            siteAssessmentsDataTable.Columns[0].ColumnName = "Property Id";
            siteAssessmentsDataTable.Columns[5].ColumnName = "Hansen Id";
            siteAssessmentsDataTable.Columns[6].ColumnName = "Management Type";
            siteAssessmentsDataTable.Columns[7].ColumnName = "Area Type";
            siteAssessmentsDataTable.Columns[8].ColumnName = "Destination Type";
            siteAssessmentsDataTable.Columns[9].ColumnName = "Facility Type";
            siteAssessmentsDataTable.Columns[10].ColumnName = "Update Date";
            siteAssessmentsDataTable.PrimaryKey = null;
            siteAssessmentsDataTable.Columns.Remove("ia_type_id");
            siteAssessmentsDataTable.Columns.Remove("destination_id");
            siteAssessmentsDataTable.Columns.Remove("management_id");
            siteAssessmentsDataTable.Columns.Remove("facility_type_id");
            siteAssessmentsDataTable.Columns.Remove("endDate");
            siteAssessmentsDataTable.Columns.Remove("site_assessment_id");
            siteAssessmentsDataTable.AcceptChanges();
            
            retroBindingSource.DataSource = siteAssessmentsDataTable;
            
            dgvIncomingRetroChanges.DataSource = retroBindingSource;
            dgvIncomingRetroChanges.AutoResizeColumns();
            
            lblNewRetroSiteAssessments.Text = "New Site Assessments: " + Convert.ToString(newSiteAssessmentsCount);
            MessageBox.Show("Current new site assessments list shows changes since " + Convert.ToString(retrofitsStartDate) + ". To change the date range, choose a start date on the calendar and re-click on the 'View New Site Assessments' button.");

            ExportToCSV(siteAssessmentsDataTable, "c:\\temp\\", "SiteAssessments.csv");
            btnExportToTemplate.Visible = true;
            return;
          }

          else
          {
            MessageBox.Show("No new site assessments added to RETRO database since " + retrofitsStartDate + ". Try the using the calendar to find potential ICs within specific time period.", "No Site Assesments within Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
          }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Could not load incoming changes: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnViewNewIcTargets_Click(object sender, EventArgs e)
    {
      if (Convert.ToString(dgvIncomingRetroChanges.DataSource) != "")
      {
        dgvIncomingRetroChanges.DataSource = null;
      }

      try
      {
        rDS = new RetrofitsDataSet();
        rDS.InitRetroDataSet();
        int newIcTargetsCount;
        DataTable opportunity = new DataTable();
        DataView icTargetsDataView = new DataView();

        SystemsAnalysis.DataAccess.RetrofitsDataSetTableAdapters.SiteOpportunityTableAdapter opportunityTA;
        opportunityTA = new SystemsAnalysis.DataAccess.RetrofitsDataSetTableAdapters.SiteOpportunityTableAdapter();
        opportunityTA.FilterFillSiteOpportunity(rDS.SiteOpportunity, clndrRetrofitsChangesStart.Value, clndrRetrofitsChangesEnd.Value);

          IEnumerable<int> qryNewIcTargetsCount =
            from r in rDS.SiteOpportunity
            where r.startDate >= retrofitsStartDate
            && r.endDate <= retrofitsEndDate
            && (r.opportunity_feasibility == 1 || r.opportunity_feasibility == 2)
            select r.site_opportunity_id;

          newIcTargetsCount = qryNewIcTargetsCount.Count();

          if (newIcTargetsCount > 0)
          {
            opportunity = rDS.SiteOpportunity;

            EnumerableRowCollection<DataRow> qrySelectNewIcTargetsRecords =
              (from r in opportunity.AsEnumerable()
               where r.Field<DateTime>("startDate") >= retrofitsStartDate
               && r.Field<DateTime>("endDate") <= retrofitsEndDate
               && (r.Field<int>("opportunity_feasibility") == 1 || r.Field<int>("opportunity_feasibility") == 2)
               select r);

            icTargetsDataView = qrySelectNewIcTargetsRecords.AsDataView();

            DataTable potentialICsDataTable = icTargetsDataView.Table.Clone();
            foreach (DataRowView drv in icTargetsDataView)
            {
              potentialICsDataTable.ImportRow(drv.Row);
            }
            
            potentialICsDataTable.Columns[0].ColumnName = "Property Id";
            potentialICsDataTable.Columns[1].ColumnName = "Imp Area SqFt";
            potentialICsDataTable.Columns[4].ColumnName = "Facility Type";
            potentialICsDataTable.Columns[5].ColumnName = "Management Type";
            potentialICsDataTable.Columns[6].ColumnName = "Modified Date";

            potentialICsDataTable.PrimaryKey = null;
            potentialICsDataTable.Columns.Remove("opportunity_feasibility");
            potentialICsDataTable.Columns.Remove("site_opportunity_id");
            potentialICsDataTable.Columns.Remove("endDate");
            
            potentialICsDataTable.AcceptChanges();

            retroBindingSource.DataSource = potentialICsDataTable;
            dgvIncomingRetroChanges.DataSource = retroBindingSource;
            dgvIncomingRetroChanges.AutoResizeColumns();
            
            lblNewRetroIcTargets.Text = "New IC Targets: " + Convert.ToString(newIcTargetsCount);
            MessageBox.Show("Current new potential ICs list shows changes since previous potential ICs update which occured on " + Convert.ToString(retrofitsStartDate)); 
          
            //TO-DO: join the icTargetsDataView to the SITE table on site_id to get property_id
            //property_id will be returned in the exported "PotentialICs" table which the technician
            //will use as reference for digitizing new IC alt targets

            //TO-DO: also look to join other fields (such as ia_type_id from IMPERVIOUS_AREA_TYPE, management_id
            //from MANAGEMENT, facility_type_id from FACILITY_TYPE, and opportunity_feasibility from OPPORTUNITY_FEASIBILITY)
            //in order to provide actual descriptions of what each field means (e.g., facility_type_id 1 = "Infiltration", management_id 4 = "Rain Garden")
            //Note: definition/description for opportunity_feasibility_id may not be needed since this won't affect the formatted IC alt record-it would only 
            //serve as supplemental information.

            //TO-DO: look at dropping some fields from the exported user file such as: update_by, update_date, opportunity_feasibility, etc.  These fields
            //would only act as supplemental info and are not necessary for digitizing the IC alt record.

            ExportToCSV(potentialICsDataTable, "c:\\temp\\", "PotentialICs.csv");

            return;
          }
          else
          {
            MessageBox.Show("No new potential IC targets added to RETRO database since " + retrofitsStartDate + ". Try the using the calendar to find potential ICs within specific time period.", "No Potential ICs within Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
          }
        }

      catch (Exception ex)
      {
        MessageBox.Show("Could not load incoming changes: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnViewNewConstrFacs_Click(object sender, EventArgs e)
    {
      if (Convert.ToString(dgvIncomingRetroChanges.DataSource) != "")
      {
        dgvIncomingRetroChanges.DataSource = null;
      }

      try
      {
        rDS = new RetrofitsDataSet();
        rDS.InitRetroDataSet();
        int newConstructedICsCount;
        DataTable project = new DataTable();
        DataView projectDataView = new DataView();

        SystemsAnalysis.DataAccess.RetrofitsDataSetTableAdapters.ProjectTableAdapter projectTA;
        projectTA = new SystemsAnalysis.DataAccess.RetrofitsDataSetTableAdapters.ProjectTableAdapter();
        projectTA.FilterFillProject(rDS.Project, clndrRetrofitsChangesStart.Value, clndrRetrofitsChangesEnd.Value);

        IEnumerable<int> qryNewConstructedICsCount =
          from r in rDS.Project
          where r.startDate >= retrofitsStartDate
          && r.endDate <= retrofitsEndDate
          && r.project_status_id == 4
          select r.project_id;

        newConstructedICsCount = qryNewConstructedICsCount.Count();

        if (newConstructedICsCount > 0)
        {
          project = rDS.Project;
          EnumerableRowCollection<DataRow> qrySelectNewConstructedICsRecords =
          (from r in project.AsEnumerable()
           where r.Field<DateTime>("startDate") >= retrofitsStartDate
           && r.Field<DateTime>("endDate") <= retrofitsEndDate
           && r.Field<int>("project_status_id") == 4
           select r);

          projectDataView = qrySelectNewConstructedICsRecords.AsDataView();

          DataTable constructedICsDataTable = projectDataView.Table.Clone();
          foreach (DataRowView drv in projectDataView)
          {
            constructedICsDataTable.ImportRow(drv.Row);
          }

          constructedICsDataTable.Columns[0].ColumnName = "Property ID";
          constructedICsDataTable.Columns[1].ColumnName = "IA Managed-Roof (Sq Ft)";
          constructedICsDataTable.Columns[2].ColumnName = "IA Managed-Park (Sq Ft)";
          constructedICsDataTable.Columns[3].ColumnName = "Facility Size (Sq Ft)";
          constructedICsDataTable.Columns[4].ColumnName = "Storage Vol (Cu Ft)";
          constructedICsDataTable.Columns[6].ColumnName = "Management Type";
          constructedICsDataTable.Columns[7].ColumnName = "Fac Type";
          constructedICsDataTable.Columns[8].ColumnName = "Modified Date";

          constructedICsDataTable.Columns.Remove("project_status_id");
          constructedICsDataTable.Columns.Remove("endDate");
          constructedICsDataTable.PrimaryKey = null;
          constructedICsDataTable.Columns.Remove("project_id");

          constructedICsDataTable.AcceptChanges();
          
          retroBindingSource.DataSource = constructedICsDataTable;
          dgvIncomingRetroChanges.DataSource = retroBindingSource;
          dgvIncomingRetroChanges.AutoResizeColumns();

          lblNewConstrRetroFacs.Text = "New Constructed ICs: " + Convert.ToString(newConstructedICsCount);
          MessageBox.Show("Current new constructed ICs list shows changes since " + Convert.ToString(retrofitsStartDate) + ". To change the date range, choose a start date on the calendar and re-click on the 'View New Constructed Facilities' button.");

          //TO-DO: join the projectDataView to the SITE table on site_id to get property_id
          //property_id will be returned in the exported "ConstructedICs" table which the technician
          //will use as reference for digitizing new IC alt targets

          //TO-DO: also look to join other fields (such as management_id from MANAGEMENT, facility_type_id from FACILITY_TYPE,   
          //in order to provide actual descriptions of what each field means (e.g., facility_type_id 1 = "Infiltration", management_id 4 = "Rain Garden")
          //Note: definition/description for opportunity_feasibility_id may not be needed since this won't affect the formatted IC alt record-it would only 
          //serve as supplemental information.

          //TO-DO: look at dropping some fields from the exported user file such as: name, facility_size, storage_volume, infiltration_test_id, 
          //infiltration_rate, project_status_id, update_by, update_date, opportunity_feasibility, etc.  These fields
          //would only act as supplemental info and are not necessary for digitizing the IC alt record.

          ExportToCSV(constructedICsDataTable, "c:\\temp\\", "ConstructedICs.csv");
          return;
        }

        else
        {
          MessageBox.Show("No new completed projects added to RETRO database since " + retrofitsStartDate + ". Try the using the calendar to find completed projects within specific time period.", "No Completed Projects within Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
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

      try
      {
        rDS = new RetrofitsDataSet();
        rDS.InitRetroDataSet();
        int newSiteAssessmentsCount;
        int newIcTargetsCount;
        int newConstructedICsCount;
        DateTime maxSiteAssessmentsDate;
        DateTime maxIcTargetsDate;
        DateTime maxConstructedICsDate;
        DataTable siteAssessmentsTable = new DataTable();
        DataTable icTargetsTable = new DataTable();
        DataTable constructedICsTable = new DataTable();

        maxSiteAssessmentsDate =
            (from s in projectDataSet.SESSION
             select s.edit_date).Max();

        IEnumerable<int> qrySiteAssessMentsCount =
          from r in rDS.SITE_ASSESSMENT
          where r.update_date >= maxSiteAssessmentsDate && r.update_date <= System.DateTime.Now
          select r.site_assessment_id;

        newSiteAssessmentsCount = qrySiteAssessMentsCount.Count();

        maxIcTargetsDate =
            (from s in projectDataSet.SESSION
             select s.edit_date).Max();

        IEnumerable<int> qryNewIcTargetsCount =
          from r in rDS.SITE_OPPORTUNITY
          where r.update_date >= maxIcTargetsDate && r.update_date <= System.DateTime.Now
          select r.site_opportunity_id;

        newIcTargetsCount = qryNewIcTargetsCount.Count();

        maxConstructedICsDate =
            (from s in projectDataSet.SESSION
             select s.edit_date).Max();

        IEnumerable<int> qryNewConstructedICsCount =
          from r in rDS.PROJECT
          where r.update_date >= maxConstructedICsDate && r.update_date <= System.DateTime.Now
          select r.project_id;

        newConstructedICsCount = qryNewConstructedICsCount.Count();

        if (newSiteAssessmentsCount > 0)
        {
          IEnumerable<DataRow> qrySelectNewSiteAssessmentRecords =
          (from r in rDS.SITE_ASSESSMENT.AsEnumerable()
           where r.update_date >= maxSiteAssessmentsDate && r.update_date <= System.DateTime.Now
           select r.site_assessment_id) as IEnumerable<DataRow>;

          siteAssessmentsTable = qrySelectNewSiteAssessmentRecords.CopyToDataTable<DataRow>();
        }

        if (newIcTargetsCount > 0)
        {
          IEnumerable<DataRow> qrySelectNewIcTargetsRecords =
            (from r in rDS.SITE_OPPORTUNITY.AsEnumerable()
             where r.update_date >= maxIcTargetsDate && r.update_date <= System.DateTime.Now
             select r.site_opportunity_id) as IEnumerable<DataRow>;

          icTargetsTable = qrySelectNewIcTargetsRecords.CopyToDataTable<DataRow>();
        }

        if (newConstructedICsCount > 0)
        {
          IEnumerable<DataRow> qrySelectNewConstructedICsRecords =
          (from r in rDS.PROJECT.AsEnumerable()
           where r.update_date >= maxConstructedICsDate && r.update_date <= System.DateTime.Now
           select r.project_id) as IEnumerable<DataRow>;

          constructedICsTable = qrySelectNewConstructedICsRecords.CopyToDataTable<DataRow>();
        }

        DialogResult result = MessageBox.Show("The following updates will be applied:" + "\r\n" +
        newSiteAssessmentsCount + " new site assessments will be applied to master modeling tables." + "\r\n" +
        newIcTargetsCount + " new potential inflow controls will be added to the IC Alts GIS coverage(s)." + "\r\n" +
        newConstructedICsCount + " new constructed inflow controls will be added to the IC Alt GIS coverage(s)." + "\r\n", "Confirm Update", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

        if (result != DialogResult.OK)
        {
          return;
        }
        //ApplyRetroUpdates();
        int retroChangesType = 0;

        if (newSiteAssessmentsCount > 0 && newIcTargetsCount > 0 && newConstructedICsCount > 0)
        {
          retroChangesType = 1; //AllChanges
        }

        if (newSiteAssessmentsCount > 0 && newIcTargetsCount > 0 && newConstructedICsCount == 0)
        {
          retroChangesType = 2; //AssessmentsPotentialChanges
        }

        if (newSiteAssessmentsCount > 0 && newIcTargetsCount == 0 && newConstructedICsCount > 0)
        {
          retroChangesType = 3; //AssessmentsConstructedChanges
        }

        if (newSiteAssessmentsCount > 0 && newIcTargetsCount == 0 && newConstructedICsCount > 0)
        {
          retroChangesType = 4; //AssessmentsChangesOnly
        }

        if (newSiteAssessmentsCount == 0 && newIcTargetsCount > 0 && newConstructedICsCount > 0)
        {
          retroChangesType = 5; //PotentialConstructedChanges
        }

        if (newSiteAssessmentsCount == 0 && newIcTargetsCount > 0 && newConstructedICsCount == 0)
        {
          retroChangesType = 6; //PotentialChangesOnly
        }

        if (newSiteAssessmentsCount == 0 && newIcTargetsCount == 0 && newConstructedICsCount > 0)
        {
          retroChangesType = 7; //ConstructedChangesOnly
        }

        if (newSiteAssessmentsCount == 0 && newIcTargetsCount == 0 && newConstructedICsCount == 0)
        {
          retroChangesType = 8; //NoChanges
        }

        switch (retroChangesType)
        {
          case 0:
            MessageBox.Show("0. This should not have happened");
            break;
          case 1:
            MessageBox.Show("1. AllChanges.");
            SendRetroAllChangesEmail();
            break;
          case 2:
            MessageBox.Show("2. AssessmentsPotentialChanges.");
            SendRetroAssessmentPotentialChangesEmail();
            break;
          case 3:
            MessageBox.Show("3. AssessmentsConstructedChanges.");
            SendRetroAssessmentConstructedChangesEmail();
            break;
          case 4:
            MessageBox.Show("4. AssessmentsChangesOnly.");
            SendRetroAssessmentsChangesOnlyEmail();
            break;
          case 5:
            MessageBox.Show("5. PotentialConstructedChanges.");
            SendRetroPotentialConstructedEmail();
            break;
          case 6:
            MessageBox.Show("6. PotentialChangesOnly.");
            SendRetroPotentialChangesOnlyEmail();
            break;
          case 7:
            MessageBox.Show("7. ConstructedChangesOnly.");
            SendRetroConstructedChangesOnlyEmail();
            break;
          case 8:
            MessageBox.Show("8. NoChanges.");
            SendRetroNoChangesEmail();
            break;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Could not apply RETRO changes: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnExportErrors_Click(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }

    private void btnUpdateErrorCancel_Click(object sender, EventArgs e)
    {
      RestartUpdate();
    }

    public void frmMain_Load(object sender, EventArgs e)
    {
      SetStatus("Ready");
      try
      {
        Cursor = Cursors.WaitCursor;
        statusBarMain.Panels["dscEditorConnection"].Text = "Dsc Editor: " + ConnectionStringSummary(Properties.Settings.Default.DscEditorConnectionString);
        statusBarMain.Panels["masterDataConnection"].Text = "Master Data: " + ConnectionStringSummary(Properties.Settings.Default.MasterDataConnectionString);

        // TODO: This line of code loads data into the 'ProjectDataSet.DSCEDIT' table. You can move, or remove it, as needed.
        projectDataSet.EnforceConstraints = false;
        //this.dSCEDITTableAdapter.Fill(this.ProjectDataSet.DSCEDIT);
        // TODO: This line of code loads data into the 'ProjectDataSet.SESSION' table. You can move, or remove it, as needed.
        this.sessionTableAdapter.Fill(this.projectDataSet.SESSION);

        //Initialize date range of assessment calendar
        DateTime retrofitsStartDate =
            (from s in projectDataSet.SESSION
             select s.edit_date).Max();
        DateTime retrofitsEndDate = System.DateTime.Now;

        clndrRetrofitsChangesStart.Value = retrofitsStartDate;
        clndrRetrofitsChangesEnd.Value= retrofitsEndDate;

        updaterHistoryBindingNav.BindingSource = sessionBindingSource;
      }
      catch (SqlException sqlException)
      {
        MessageBox.Show(sqlException.Message + "\n" + "Please specify server and database connection parameters on the Database Connection Options tab.", "DSCUpdater: SQL Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
        UpdateDscEditorConnection();
      }
      catch (Exception ex)
      {

      }
      finally
      {
        Cursor = Cursors.Default;
        SetStatus("Ready");
      }
    }

    private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
    {

    }

    private void expBarMain_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
    {
      LoadTab(e.Item.Key);
    }

    private void tabControlMain_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
    {
      this.Text = "DSC Updater";
      if (e.Tab != null)
      {
        this.Text = this.Text + " - " + e.Tab.Text;
      }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string fi;
      fi = @"\\Cassio\asm_apps\Apps\DSCUpdater\DSCUpdaterPublish.htm";
      System.Diagnostics.Process.Start("IEXPLORE.EXE", fi);
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
      //TO-DO: change DSCUpdaterConfig.xml file DB connection settings back to default
    }

    private void toolStripStatusLabel2_Click(object sender, EventArgs e)
    {
      UpdateDscEditorConnection();
    }

    private void toolbarManagerMain_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
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

    private void btnRevertSession_Click(object sender, EventArgs e)
    {
      //BatchRevertICEdits();
    }

    private void btnUpdaterEditorCloseCancel_Click(object sender, EventArgs e)
    {
      RestartUpdate();
    }

    private void clndrRetrofitsChangesStart_ValueChanged(object sender, EventArgs e)
    {
      retrofitsStartDate = clndrRetrofitsChangesStart.Value;
      //MessageBox.Show("Start"+Convert.ToString(clndrRetrofitsChangesStart.Value)); 
    }

    private void clndrRetrofitsChangesEnd_ValueChanged(object sender, EventArgs e)
    {
      retrofitsEndDate = clndrRetrofitsChangesEnd.Value;
      //MessageBox.Show("End"+Convert.ToString(clndrRetrofitsChangesEnd.Value));
    }

    private void btnExportToTemplate_Click(object sender, EventArgs e)
    {

    }

    private void lstFfeDbTables_SelectedIndexChanged(object sender, EventArgs e)
    {
      lblSelectedFfeDbTable.Text = "Selected Table: "+ lstFfeDbTables.SelectedItem.ToString();
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      RestartUpdate();
    }

    private void openViewConnectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      UpdateFfeDataConnection();
    }

    private void loadDataForCurrentConnectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // get tables
      StoreTableNames();
      
      // clear internal list
      lstFfeDbTables.Items.Clear();

      //update the list
      lstFfeDbTables.Items.AddRange(arrTables.ToArray());
    }

    private void btnRunFfeUpdates_Click(object sender, EventArgs e)
    {
      RunFfeUpdates();
    }    
  }
}

public static class DscErrors
{
  public const string PendingUpdate = "Dsc Has Pending Update";
  public const string DscNotFound = "Dsc Not Found in Master";
  public const string ParkICGreaterThanParkArea = "Park IC Area > Park Area";
  public const string RoofICGreaterThanRoofArea = "Roof IC Area > Roof Area";
}


