using System;
using System.Collections;
using System.Collections.Generic;
using Access = Microsoft.Office.Interop.Access;
using dao;
using System.Reflection;
//using Microsoft.Office.Interop.Access.Dao;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.Management;
//using SMO = Microsoft.SqlServer.Management.Smo;
using System.Security.AccessControl;
///using Microsoft.SqlServer.Management.Smo.RegisteredServers;
using SystemsAnalysis.Utils.SQLHelper;

namespace SystemsAnalysis.Utils.DataMobility
{
    public class DataMobility
    {
        public static string SQLCopyAccessTable(string AccessTableName, string sourceDatabase, string SQLTableName, string SQLDB)
        {
            string exceptionString = "";
            System.Data.DataTable table = new System.Data.DataTable();
            //dao.TableDef linkTable;
            string linkTableConnection = "Provider=Microsoft.Jet.OleDb.4.0;DATA SOURCE=" + sourceDatabase;
            //linkTable.SourceTableName = tableName;
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();

            string DROPsql = "DROP TABLE " + SQLTableName;
            SqlCommand cmd = new SqlCommand(DROPsql, thisSQLDB);
            try
            {
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                OleDbDataAdapter accDataAdapter = new OleDbDataAdapter("SELECT * FROM " + AccessTableName, linkTableConnection);
                accDataAdapter.Fill(table);
                SqlTableCreator theCreator = new SqlTableCreator(thisSQLDB);
                table.TableName = SQLTableName;

                try
                {
                    theCreator.CreateFromDataTable(table);
                    //Open a connection with destination database;
                    using (SqlConnection connection =
                           new SqlConnection(thisSQLDB.ConnectionString))
                    {
                        connection.Open();

                        //Open bulkcopy connection.
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connection))
                        {
                            //Set destination table name
                            //to table previously created.
                            bulkcopy.DestinationTableName = table.TableName;

                            try
                            {
                                bulkcopy.BulkCopyTimeout = 0;
                                bulkcopy.WriteToServer(table);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                exceptionString = exceptionString + ex.ToString();
                            }

                            connection.Close();
                        }
                    }
                }
                catch (SqlException ae)
                {
                    exceptionString = exceptionString + ae.ToString();
                }
            }
            catch (Exception ex)
            {
                exceptionString = exceptionString + ex.ToString();
            }
            thisSQLDB.Close();
            return exceptionString;
        }

        public static void AccessCopySQLTable(string AccessTableName, string sourceDatabase, string SQLTableName, string outputDatabase)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            //dao.TableDef linkTable;
            string linkTableConnection = outputDatabase;
            //linkTable.SourceTableName = tableName;
            dao._DBEngine dbEng = new dao.DBEngineClass();
            dao.Workspace ws = dbEng.CreateWorkspace("", "admin", "", dao.WorkspaceTypeEnum.dbUseJet);
            dao.Database db = ws.OpenDatabase(outputDatabase, true, false, "");


            //get rid of the table if it already exists in access
            try
            {
                db.Execute("DROP TABLE " + AccessTableName, Type.Missing);
            }
            catch (Exception ex)
            {
                //table doesnt exist
            }
            //create a linked table to the sql server table
            dao.TableDef theTable = db.CreateTableDef(AccessTableName, System.Reflection.Missing.Value, SQLTableName, sourceDatabase);
            theTable.Connect = sourceDatabase;
            theTable.SourceTableName = SQLTableName;
            db.TableDefs.Append(theTable);
            //copy the linked table to a permanent table in access

            db.Close();
            ws.Close();
        }

        /*public string SQLCopyAccessTable(string AccessTableName, string sourceDatabase, string SQLTableName)
        {
            return SQLCopyAccessTable(AccessTableName, sourceDatabase, SQLTableName, CurrentSQLDB.ConnectionString);
        }*/

        public static void SQLCopySQLTable(string SQLInputTableName, string inputDatabase, string SQLOutputTableName, string outputDatabase)
        {
            System.Data.DataTable inputTable = new System.Data.DataTable();
            //System.Data.DataTable outputTable = new System.Data.DataTable();
            SqlConnection outputDatabaseConnection = new SqlConnection(outputDatabase);
            outputDatabaseConnection.Open();

            //remove any existing matching output table from the output database
            string DROPsql = "DROP TABLE " + SQLOutputTableName;
            SqlCommand cmd = new SqlCommand(DROPsql, outputDatabaseConnection);
            try
            {
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Could not drop table
            }
            try
            {
                //copying to access should be ok here.
                SqlDataAdapter SQLInputDataAdapter = new SqlDataAdapter("SELECT * FROM " + SQLInputTableName, inputDatabase);

                SQLInputDataAdapter.Fill(inputTable);
                SqlTableCreator theCreator = new SqlTableCreator(outputDatabaseConnection);
                inputTable.TableName = SQLInputTableName;
                try
                {
                    theCreator.CreateFromDataTable(inputTable, SQLOutputTableName);
                    //Open a connection with destination database;
                    using (SqlConnection connection =
                           new SqlConnection(outputDatabase))
                    {
                        connection.Open();

                        //Open bulkcopy connection.
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connection))
                        {
                            //Set destination table name
                            //to table previously created.
                            bulkcopy.DestinationTableName = SQLOutputTableName;

                            try
                            {
                                bulkcopy.BulkCopyTimeout = 0;
                                bulkcopy.WriteToServer(inputTable);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            connection.Close();
                        }
                    }
                }
                catch (SqlException ae)
                {

                }
            }
            catch (Exception ex)
            {
                //write error message
            }
        }


    }
}
