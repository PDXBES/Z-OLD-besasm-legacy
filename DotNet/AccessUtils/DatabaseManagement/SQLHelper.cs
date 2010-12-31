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
using SMO = Microsoft.SqlServer.Management.Smo;
using System.Security.AccessControl;
using Microsoft.SqlServer.Management.Smo.RegisteredServers;

namespace SystemsAnalysis.Utils.AccessUtils
{
    class SQLHelper
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


    }
}
