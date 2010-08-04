﻿using System;
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
using System.Data.Linq;
using System.Linq.Expressions;
using System.Data.Linq.Mapping;

namespace DSCUpdater
{
  public partial class frmMain : Form
  {
    SqlDataAdapter daUpdaterEditor;
    DataTable dtUpdaterEditor;

    char[] splitArray1 = { ',', '\n', '\r' };
    char[] splitArray2 = { ' ' };

    Int64[] SearchList = new long[1];

    //string LineText;
    string tempFileName;

    // Use to populate the grid. 
    string[] DataResult1 = { "", "", "", "", "", "", "", "" };
    string[] Titles = { "RNO", "DSCID", "New Roof Area", "New Roof DISCO IC Area", "New Roof Drywell IC Area" };

    public static class DscErrors
    {
      public const string PendingUpdate = "Dsc Has Pending Update";
      public const string DscNotFound = "Dsc Not Found in Master";
      public const string ParkICGreaterThanParkArea = "Park IC Area > Park Area";
      public const string RoofICGreaterThanRoofArea = "Roof IC Area > Roof Area";
    }


    public frmMain()
    {
      InitializeComponent();

      tempFileName = @"C:\Temp.csv";
      SetStatus("Ready");
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      try
      {
        Cursor = Cursors.WaitCursor;
        ultraStatusBar1.Panels["dscEditorConnection"].Text = "Dsc Editor: " + ConnectionStringSummary(Properties.Settings.Default.DscEditorConnectionString);
        ultraStatusBar1.Panels["masterDataConnection"].Text = "Master Data: " + ConnectionStringSummary(Properties.Settings.Default.MasterDataConnectionString);

        // TODO: This line of code loads data into the 'ProjectDataSet.DSCEDIT' table. You can move, or remove it, as needed.
        projectDataSet.EnforceConstraints = false;
        //this.dSCEDITTableAdapter.Fill(this.ProjectDataSet.DSCEDIT);
        // TODO: This line of code loads data into the 'ProjectDataSet.SESSION' table. You can move, or remove it, as needed.
        this.sESSIONTableAdapter.Fill(this.projectDataSet.SESSION);
        bindingNavigator1.BindingSource = sESSIONBindingSource;
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
      OutlookMail oMail = new OutlookMail();
      oMail.AddToOutbox(toValue, subjectValue, bodyValue);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      RestartUpdate();
    }

    private int SetProgress
    {
      get
      {
        return this.ultraStatusBar1.Panels["progressBar"].ProgressBarInfo.Value;
      }
      set
      {
        this.ultraStatusBar1.Panels["progressBar"].ProgressBarInfo.Value = value;
      }
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
      LoadTab("dscEditorHistory");      
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
        /*
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

    private void btnSubmitUpdaterEditorChanges_Click(object sender, EventArgs e)
    {
      /*
      SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.DscEditorConnectionString);
      SqlCommand sqlCmd = new SqlCommand();
      
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

      UpdateDscTables();
      //BatchUpdateMasterICTables(sqlCmd);
      UpdateTableSession();

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
       * */
    }

    private void btnRevertSession_Click(object sender, EventArgs e)
    {
      //TODO: Verify that entries in dgvUpdaterEditor are only displaying a single session id. The grid must be filtered by session id to use this tool.
      int dgvRowCount = 0;
      dgvRowCount = dgvUpdaterEditor.Rows.Count;
      DialogResult dr = MessageBox.Show(dgvRowCount + " records will be reverted.  Do you wish to continue? (Changes can only be undone by submitting a new update file)", "Confirm Revert Operation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (dr == DialogResult.Yes)
      {
        int maxEditID = 0;
        int editorEditID = 0;
        dgvUpdaterEditor.Rows[0].Selected = true;
        editorEditID = Convert.ToInt32(dgvUpdaterEditor.Selected.Cells[1].Value);
        SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.DscEditorConnectionString);
        sqlCon.Open();
        SqlCommand sqlCmd = new SqlCommand();

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
          /*
          BatchRevertICEdits(sqlCmd);
          AddEditIDCommandParameter(sqlCmd);
          AddEditDateCommandParameter(sqlCmd, sqlCon);
          AddEditedByCommandParameter(sqlCmd, sqlCon);
          UpdateTableSession();

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
           */
        }
      }
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

    private void btnCancel_Click(object sender, MouseEventArgs e)
    {

    }

    private void btnCancelUpdate_Click(object sender, EventArgs e)
    {
      RestartUpdate();
    }

    private void btnExportErrors_Click(object sender, EventArgs e)
    {
      throw new NotImplementedException();
    }

    private void btnUpdateErrorCancel_Click(object sender, EventArgs e)
    {
      RestartUpdate();
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

    private void dgvUpdaterEditor_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
    {
      //TODO: Update text fields to reflect current selected data grid row.
    }

  }

}

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


