using System;
using System.Collections;
using System.Collections.Generic;
//using Access = Microsoft.Office.Interop.Access;
//using dao;
using System.Reflection;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.Management;
using SMO = Microsoft.SqlServer.Management.Smo;
using System.Security.AccessControl;
using Microsoft.SqlServer.Management.Smo.RegisteredServers;


namespace SystemsAnalysis.Utils.SQLHelper
{
    public class SQLHelper
    {

        

        //get all available SQL Servers    
        public static Object[] SQLGetListOfServers()
        {
            List<Object> theList = new List<object>();

            try
            {
                /*System.Data.DataTable dt = SMO.SmoApplication.EnumAvailableSqlServers(false);

                if (dt.Rows.Count > 0)
                {
                    // Load server names into combo box
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        theList.Add(dr["Name"]);
                    }
                }*/

                //Registry for local
                try
                {
                    Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                    String[] instances = (String[])rk.GetValue("InstalledInstances");
                    if (instances.Length > 0)
                    {
                        foreach (String element in instances)
                        {
                            String name = "";
                            //only add if it doesn't exist
                            if (element == "MSSQLSERVER")
                                name = System.Environment.MachineName;
                            else
                                name = System.Environment.MachineName + @"\" + element;

                            if (theList.Contains(name) == false)
                                theList.Add(name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //no local servers
                }

                //network servers
                /*try
                {
                    List<ServerInstance> networkServers = new List<ServerInstance>();

                    using (DataTable dataSources = sSqlDataSourceEnumerator.Instance.GetDataSources())
                    {
                        foreach (DataRow dataSource in dataSources.Rows)
                        {
                            networkServers.Add(new ServerInstance(
                                (string)dataSource["ServerName"],
                                Convert.IsDBNull(dataSource["InstanceName"]) ? string.Empty :
                                (string)dataSource["InstanceName"],
                                Convert.IsDBNull(dataSource["Version"]) ?
                                "0.0" : (string)dataSource["Version"]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    //no network servers
                }*/

                // Registered Servers
                try
                {
                    RegisteredServer[] rsvrs = SMO.SqlServerRegistrations.EnumRegisteredServers();

                    foreach (RegisteredServer rs in rsvrs)
                    {
                        String name = "";

                        name = rs.ServerInstance.Replace(".", System.Environment.MachineName)
                                                .Replace("(local)", System.Environment.MachineName)
                                                .Replace("localhost", System.Environment.MachineName);
                        //only add if it doesn't exist
                        if (theList.Contains(name) == false && name.Length > 0)
                            theList.Add(name);
                    }
                }
                catch (Exception ex)
                {
                    //no registered servers
                }

            }
            catch (SMO.SmoException ex)
            {
                //DisplayErrorMessage(ex);
            }
            catch (Exception ex)
            {
                //DisplayErrorMessage(ex);
            }
            Object[] theObjects;
            if (theList.Count > 0)
            {
                theObjects = new Object[theList.Count];
                for (int i = 0; i < theList.Count; i++)
                {
                    theObjects[i] = theList[i];
                }
                return theObjects;
            }
            else
            {
                return null;
            }

        }

        //get all available databases in an SQLServer    
        public static Object[] SQLGetListOfDatabasesInServer(string theServer)
        {
            List<Object> theList = new List<object>();

            try
            {
                SMO.Server svr = new Microsoft.SqlServer.Management.Smo.Server(theServer);
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("DatabaseName");

                foreach (Microsoft.SqlServer.Management.Smo.Database db in svr.Databases)
                {
                    System.Data.DataRow dr = dt.NewRow();
                    dr["DatabaseName"] = db.Name;
                    dt.Rows.Add(dr);
                }

                if (dt.Rows.Count > 0)
                {
                    // Load server names into combo box
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        theList.Add(dr["DatabaseName"]);
                    }
                }
            }
            catch (SMO.SmoException ex)
            {
                //DisplayErrorMessage(ex);
            }
            catch (Exception ex)
            {
                //DisplayErrorMessage(ex);
            }
            Object[] theObjects;
            if (theList.Count > 0)
            {
                theObjects = new Object[theList.Count];
                for (int i = 0; i < theList.Count; i++)
                {
                    theObjects[i] = theList[i];
                }
                return theObjects;
            }
            else
            {
                return null;
            }

        }

        public static string SQLTestDatabase(string databaseName, string tableName)
        {
            string connectionWorks = "";

            System.Data.DataTable inputTable = new System.Data.DataTable();
            //System.Data.DataTable outputTable = new System.Data.DataTable();
            SqlConnection outputDatabaseConnection = new SqlConnection(databaseName);
            try
            {
                outputDatabaseConnection.Open();
                //remove any existing matching output table from the output database
                string SELECTsql = "SELECT TOP 1 * FROM " + tableName;
                SqlCommand cmd = new SqlCommand(SELECTsql, outputDatabaseConnection);

                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                connectionWorks = "";
            }
            catch (SqlException ae)
            {
                //Could not select from table
                connectionWorks = ae.ToString();
            }

            return connectionWorks;
        }

        //methods for translating and archiving SQL server databases/tables
        //this method is important for archiving the sql server database.  Without this method, the 
        //user can only archive to the C: SQL server default location.
        public void ArchiveTable(string tablename, string archiveLocation)
        {
            string tempPath = Directory.CreateDirectory(archiveLocation).FullName;

            //set permissions
            SelectQuery sQuery = new SelectQuery("Win32_Group", "Domain='" + System.Environment.UserDomainName.ToString() + "'");
            try
            {
                DirectoryInfo myDirectoryInfo = new DirectoryInfo(archiveLocation);
                DirectorySecurity myDirectorySecurity = myDirectoryInfo.GetAccessControl();
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(sQuery);
                foreach (ManagementObject mObject in mSearcher.Get())
                {
                    string User = System.Environment.UserDomainName + "\\" + mObject["Name"];
                    if (User.StartsWith(System.Environment.MachineName + "\\SQL"))
                        myDirectorySecurity.AddAccessRule(new FileSystemAccessRule(User, FileSystemRights.FullControl, AccessControlType.Allow));
                }
                myDirectoryInfo.SetAccessControl(myDirectorySecurity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void SQLDeleteVIEW(string queryName, string SQLDB)
        {
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            string DROPsql = "DROP VIEW " + queryName;
            SqlCommand cmd = new SqlCommand(DROPsql, thisSQLDB);
            try
            {
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Could not drop table
            }
            thisSQLDB.Close();
            return;
        }

        public static void SQLCreateVIEW(string queryName, string queryText, string SQLDB)
        {
            SQLDeleteVIEW(queryName, SQLDB);
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            string CREATEsql = "CREATE VIEW " + queryName + " AS  " + queryText;
            SqlCommand cmd = new SqlCommand(CREATEsql, thisSQLDB);
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Handle this
            }
            thisSQLDB.Close();
            return;
        }

        public static void SQLDeletePROCEDURE(string queryName, string SQLDB)
        {
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            string DROPsql = "DROP PROCEDURE " + queryName;
            SqlCommand cmd = new SqlCommand(DROPsql, thisSQLDB);
            try
            {
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Could not drop table
            }
            thisSQLDB.Close();
            return;
        }

        public static void SQLCreatePROCEDURE(string queryName, string queryText, string SQLDB)
        {
            SQLDeletePROCEDURE(queryName, SQLDB);
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            string CREATEsql = "CREATE PROCEDURE " + queryName + " AS  BEGIN " + queryText + " END";
            SqlCommand cmd = new SqlCommand(CREATEsql, thisSQLDB);
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Handle this
            }
            thisSQLDB.Close();
            return;
        }

        public static void SQLExecuteActionQuery(string queryName, string SQLDB)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 rowsAffected;
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            cmd.CommandText = queryName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = thisSQLDB;

            cmd.CommandTimeout = 0;
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Error message here
            }
            thisSQLDB.Close();
        }

        public static void SQLExecuteActionQuery(string queryName, string parameterName, int parameter, string SQLDB)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 rowsAffected;
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            cmd.CommandText = queryName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = thisSQLDB;

            cmd.Parameters.Add(new SqlParameter(parameterName, OleDbType.Integer)).Value = parameter;

            /*string EXECUTEsql = queryName;
            SqlCommand cmd = new SqlCommand(EXECUTEsql, CurrentSQLDB);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;*/
            try
            {
                cmd.CommandTimeout = 0;
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Could not drop table
            }
            thisSQLDB.Close();
        }

        public static void SQLExecuteActionQuery(string queryName, string parameterName, int parameter, string parameterName2, int parameter2, string SQLDB)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 rowsAffected;
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            cmd.CommandText = queryName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = thisSQLDB;

            cmd.Parameters.Add(new SqlParameter(parameterName, OleDbType.Integer)).Value = parameter;
            cmd.Parameters.Add(new SqlParameter(parameterName2, OleDbType.Integer)).Value = parameter2;

            /*string EXECUTEsql = queryName;
            SqlCommand cmd = new SqlCommand(EXECUTEsql, CurrentSQLDB);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;*/
            try
            {
                cmd.CommandTimeout = 0;
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Could not drop table
            }
            thisSQLDB.Close();
        }

        static public void SQLExportTablePortion(string tableName, string strFilePath, string columnName, string parameter, string SQLDB)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //dao.TableDef linkTable;
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM " + tableName + " WHERE " + columnName + " = '" + parameter + "'", thisSQLDB);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Fill(dt);

                // Create the CSV file to which grid data will be exported.
                StreamWriter sw = new StreamWriter(strFilePath, false);
                // First we will write the headers.
                //DataTable dt = m_dsProducts.Tables[0];
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
                foreach (System.Data.DataRow dr in dt.Rows)
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
                //error message
            }
            thisSQLDB.Close();
        }

        static public void SQLExportTable(string tableName, string strFilePath, string SQLDB)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //dao.TableDef linkTable;
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM " + tableName, thisSQLDB);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Fill(dt);

                // Create the CSV file to which grid data will be exported.
                StreamWriter sw = new StreamWriter(strFilePath, false);
                // First we will write the headers.
                //DataTable dt = m_dsProducts.Tables[0];
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
                foreach (System.Data.DataRow dr in dt.Rows)
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
                //error message
            }
            thisSQLDB.Close();
        }

        static public void SQLWriteKeyValueTable(string tableName, string keyField, string key, string valueField, object value, string SQLDB)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM " + tableName, thisSQLDB);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                //sqlDataAdapter.InsertCommand = sqlCommandBuilder.GetInsertCommand();
                sqlDataAdapter.UpdateCommand = sqlCommandBuilder.GetUpdateCommand();
                //sqlDataAdapter.DeleteCommand = sqlCommandBuilder.GetDeleteCommand();
                sqlDataAdapter.Fill(dt);

                Dictionary<string, double> aggregateQueryResults;
                aggregateQueryResults = new Dictionary<string, double>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][keyField] = value;
                    dt.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
            }
            thisSQLDB.Close();
            return;
        }

        public static Dictionary<string, double> SQLExecuteAggregateQueryDoubles(string queryName, string SQLDB)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlConnection thisSQLDB = new SqlConnection(SQLDB);
            thisSQLDB.Open();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM " + queryName, thisSQLDB);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.Fill(dt);

                Dictionary<string, double> aggregateQueryResults;
                aggregateQueryResults = new Dictionary<string, double>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dt.Rows[0][i]))
                    {
                        aggregateQueryResults.Add(dt.Columns[i].ColumnName, (double)dt.Rows[0][i]);
                    }
                    else
                    {
                        aggregateQueryResults.Add(dt.Columns[i].ColumnName, (double)0);
                    }

                }
                return aggregateQueryResults;
            }
            catch (Exception ex)
            {
            }
            thisSQLDB.Close();
            return null;
        }
    }
}
