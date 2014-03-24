using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.Utils.SQLHelper;
using SystemsAnalysis.Utils.DataMobility;

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public class GridModelEngine
    {
        private int projectID;
        private string projectDescription;
        private string gridModelPath;
        private string gridDataTableName;
        private string gridDataPath, prfPath, mipPath, osfPath;
        private string outputDirectory;
        private List<GridModelRun> gridModelRuns;
        private List<GridModelResult> gridModelResults;
        public string GridDataTable = null;
        private string SQLDatabaseConnectionString;
        private bool isAccessBased;

        private AccessHelper accessHelper;

        public GridModelEngine(int projectID, string projectDescription, string gridDataPath,
            string bmpPath, string mipPath, string osfPath, string outputDirectory, string SQLDatabaseConnectionString, bool isAccessBased)
            : this(SQLDatabaseConnectionString)
        {
            this.projectID = projectID;
            this.projectDescription = projectDescription;
            this.gridDataPath = gridDataPath;
            this.prfPath = bmpPath;
            this.mipPath = mipPath;
            this.osfPath = osfPath;
            this.outputDirectory = outputDirectory;
            this.SQLDatabaseConnectionString = SQLDatabaseConnectionString;
            this.isAccessBased = isAccessBased;
            accessHelper = new AccessHelper();//gridModelPath, SQLDatabaseConnectionString);

            
        }
        //These constructors should not be connecting to the base access database to get the grid table
        //the grid table is really too large to be placing in the base access database, it should just be a linked table.
        public GridModelEngine(string SQLDatabaseConnectionString)
        {            
            /// *waterqualReference* this.gridModelPath = System.AppDomain.CurrentDomain.BaseDirectory + "Waterqual_GIS_v5_0.mdb";
            
            gridDataTableName = ConfigurationManager.AppSettings.Get("gridDataTableName");

            gridModelRuns = new List<GridModelRun>();
            gridModelResults = new List<GridModelResult>();
            accessHelper = new AccessHelper();//gridModelPath, SQLDatabaseConnectionString/*"Data Source=BESDBDEV1;Initial Catalog=SANDBOX;"*/);

        }
        //These constructors should not be connecting to the base access database to get the grid table
        //the grid table is really too large to be placing in the base access database, it should just be a linked table.
        public GridModelEngine()
        {
            ///*waterqualReference* this.gridModelPath = System.AppDomain.CurrentDomain.BaseDirectory + "Waterqual_GIS_v5_0.mdb";

            gridDataTableName = ConfigurationManager.AppSettings.Get("gridDataTableName");

            gridModelRuns = new List<GridModelRun>();
            gridModelResults = new List<GridModelResult>();
            accessHelper = new AccessHelper();
        }

        public void populateGridData()
        {
            //This function checks to see if the data in question resides on the current server
            //or if the Gridmodel must import the data from another
            //The input server connection and table name will be tested against the current gridmodels
            //server connection(SQLDatabaseConnectionString) and the default table name
            //if they both match up, then skip the copy/import, but if they do not both match up, then
            //perform the copy/import.

            //will really need to know what kind of connection it is by the string
            if ((string.Compare(gridModelRuns[0].BMPEffectivenessDB, SQLDatabaseConnectionString) != 0) || (string.Compare("GRID_BMP_PERFORMANCE", gridModelRuns[0].BMPEffectivenessTable) != 0))
            {
                if (gridModelRuns[0].BMPEffectivenessDB.IndexOf('\\')>=0)
                {
                    DataMobility.SQLCopyAccessTable(gridModelRuns[0].BMPEffectivenessTable, gridModelRuns[0].BMPEffectivenessDB, "GRID_BMP_PERFORMANCE", SQLDatabaseConnectionString);
                }
                else
                {
                    DataMobility.SQLCopySQLTable(gridModelRuns[0].BMPEffectivenessTable,
                        gridModelRuns[0].BMPEffectivenessDB, "GRID_BMP_PERFORMANCE", SQLDatabaseConnectionString);
                }
            }
            if ((string.Compare(gridModelRuns[0].PollutantLoadingDB, SQLDatabaseConnectionString) != 0) || (string.Compare("GRID_pollutant_loadings", gridModelRuns[0].PollutantLoadingTable) != 0))
            {
                if (gridModelRuns[0].PollutantLoadingDB.IndexOf('\\') >= 0)
                {
                    DataMobility.SQLCopyAccessTable(gridModelRuns[0].PollutantLoadingTable, gridModelRuns[0].PollutantLoadingDB, "GRID_pollutant_loadings", SQLDatabaseConnectionString);
                }
                else
                {
                    DataMobility.SQLCopySQLTable(gridModelRuns[0].PollutantLoadingTable,
                        gridModelRuns[0].PollutantLoadingDB, "GRID_pollutant_loadings", SQLDatabaseConnectionString);
                }
            }
            if ((string.Compare(gridDataPath, SQLDatabaseConnectionString) != 0) || (string.Compare(gridDataTableName, "GRID_WshdGrd100FtOpt") != 0))
            {
                if (gridDataPath.IndexOf('\\') >= 0)
                {
                    DataMobility.SQLCopyAccessTable(gridDataTableName, gridDataPath, "GRID_WshdGrd100FtOpt", SQLDatabaseConnectionString);
                }
                else
                {
                    DataMobility.SQLCopySQLTable(gridDataTableName, gridDataPath, "GRID_WshdGrd100FtOpt", SQLDatabaseConnectionString);
                }
            }
        }

        #region Accessors for Xml serialization
        public int ProjectID { get { return projectID; } set { projectID = value; } }
        public string ProjectDescription { get { return projectDescription; } set { projectDescription = value; } }
        public string UserName { get { return System.Windows.Forms.SystemInformation.UserName; } set { ; } }
        public string DateCreated { get { return DateTime.Now.ToString(); } set { ; } }
        public string GridDataPath { get { return gridDataPath; } set { gridDataPath = value; } }
        public string PRFPath { get { return prfPath; } set { prfPath = value; } }
        public string MIPPath { get { return mipPath; } set { mipPath = value; } }
        public string OSFPath { get { return osfPath; } set { osfPath = value; } }
        public string OutputDirectory { get { return outputDirectory; } set { outputDirectory = value; } }
        public List<GridModelRun> GridModelRuns { get { return gridModelRuns; } set { gridModelRuns = value; } }
        #endregion

        private void CreateCalcTableQuery(int selectionSetAreaID)
        {
            string calcTableQuery;
            calcTableQuery = "SELECT GRID_WshdGrd100FtOpt.*, " +
                "GRID_FE_SELECTION_SETS.percent_overlap as grid_overlap, " +
                "GRID_GridIntermediateResults.Description AS IntermediateResultsDescription, " +
                "GRID_GridIntermediateResults.IMP_pct, GRID_GridIntermediateResults.COL_B, " +
                "GRID_GridIntermediateResults.COL_C, GRID_GridIntermediateResults.COL_D, " +
                "GRID_GridIntermediateResults.COL_E, GRID_GridIntermediateResults.COL_F, " +
                "GRID_GridIntermediateResults.COL_G, GRID_GridIntermediateResults.COL_H, " +
                "GRID_GridIntermediateResults.COL_I, GRID_GridIntermediateResults.COL_J, " +
                "GRID_GridIntermediateResults.COL_K, GRID_GridIntermediateResults.COL_L, " +
                "GRID_GridIntermediateResults.COL_M, GRID_GridIntermediateResults.COL_N, " +
                "GRID_GridIntermediateResults.COL_O, GRID_GridIntermediateResults.COL_P, GRID_GridIntermediateResults.COL_Q, " +
                "GRID_GridResults.Description AS GridResultsDescription, " +
                "GRID_GridResults.COL_1, GRID_GridResults.COL_2, GRID_GridResults.COL_3, " +
                "GRID_GridResults.TP, GRID_GridResults.TP_DRY, GRID_GridResults.TP_WET, " +
                "GRID_GridResults.TSS, GRID_GridResults.TSS_DRY, GRID_GridResults.TSS_WET, " +
                "GRID_GridResults.BOD, GRID_GridResults.BOD_DRY, GRID_GridResults.BOD_WET," +
                "GRID_GridResults.ECOLI, GRID_GridResults.ECOLI_DRY, GRID_GridResults.ECOLI_WET, " +
                "GRID_GridResults.PbD, GRID_GridResults.PbD_DRY, GRID_GridResults.PbD_WET " +
                "FROM ((GRID_WshdGrd100FtOpt INNER JOIN GRID_FE_SELECTION_SETS " +
                "ON GRID_WshdGrd100FtOpt.Description = GRID_FE_SELECTION_SETS.description) " +
                "LEFT JOIN GRID_GridResults " +
                "ON GRID_FE_SELECTION_SETS.description = GRID_GridResults.Description) " +
                "LEFT JOIN GRID_GridIntermediateResults " +
                "ON GRID_WshdGrd100FtOpt.Description = GRID_GridIntermediateResults.Description " +
                "WHERE (((GRID_FE_SELECTION_SETS.selection_set_area_id)= " + selectionSetAreaID + "));";

            SQLHelper.SQLCreateVIEW("GRID_calc_table", calcTableQuery, SQLDatabaseConnectionString);
            return;
        }
        private void CreateBMPUnionQuery()
        {
            string bmpTableName = ConfigurationManager.AppSettings.Get("bmpTableName");
            string mipTableName = ConfigurationManager.AppSettings.Get("mipTableName");
            string osfTableName = ConfigurationManager.AppSettings.Get("osfTableName");

            string bmpUnionQuery = "";
            int bmpSourceCount = 0;
            if (prfPath != "")
            {
                if (prfPath.IndexOf('\\') >= 0)
                {
                    DataMobility.SQLCopyAccessTable("PDX_BMP_GRID", prfPath, "GRID_PDX_BMP_GRID", SQLDatabaseConnectionString);
                }
                else
                {
                    DataMobility.SQLCopySQLTable(bmpTableName, prfPath, "GRID_PDX_BMP_GRID", SQLDatabaseConnectionString);
                }
                bmpUnionQuery += "SELECT * FROM GRID_PRF_LIST ";
                bmpSourceCount++;
            }
            if (mipPath != "")
            {
                if (mipPath.IndexOf('\\') >= 0)
                {
                    DataMobility.SQLCopyAccessTable("PDX_MIP_GRID", mipPath, "GRID_PDX_MIP_GRID", SQLDatabaseConnectionString);
                }
                else
                {
                    DataMobility.SQLCopySQLTable(mipTableName, mipPath, "GRID_PDX_MIP_GRID", SQLDatabaseConnectionString);
                }
                if (bmpSourceCount > 0)
                {
                    bmpUnionQuery += "UNION ";
                }
                bmpUnionQuery += "SELECT * FROM GRID_MIP_LIST ";
                bmpSourceCount++;
            }
            if (osfPath != "")
            {
                if (osfPath.IndexOf('\\') >= 0)
                {
                    DataMobility.SQLCopyAccessTable("PDX_OSF_GRID", osfPath, "GRID_PDX_OSF_GRID", SQLDatabaseConnectionString);
                }
                else
                {
                    DataMobility.SQLCopySQLTable(osfTableName, osfPath, "GRID_PDX_OSF_GRID", SQLDatabaseConnectionString);
                }
                if (bmpSourceCount > 0)
                {
                    bmpUnionQuery += "UNION ";
                }
                bmpUnionQuery += " SELECT * FROM GRID_OSF_LIST ";
            }
            bmpUnionQuery += ";";

            SQLHelper.SQLCreateVIEW("GRID_BMP_LIST_UNION_QRY", bmpUnionQuery, SQLDatabaseConnectionString);
            return;
        }
        private void CreatePRFListQuery(bool inStream, string timePeriod)
        {
            string prfListQuery;
            int specialConditions;
            specialConditions = (!inStream ? 1 : 0) + (timePeriod == "EX" ? 1 : 0);
            prfListQuery = "SELECT GRID_PDX_BMP_GRID.Description, GRID_BMP_TABLES.BMP_TYPE, " +
                "GRID_BMP_TYPE_TABLE_GENERAL.BMP_TYPE_GEN_ID, GRID_BMP_TYPE_TABLE_GENERAL.BMP_TYPE_DESCRIPTION, " +
                "GRID_PDX_BMP_GRID.Percent_Overlap " +
                "FROM GRID_BMP_TYPE_TABLE_GENERAL INNER JOIN " +
                "(GRID_PDX_BMP_GRID INNER JOIN GRID_BMP_TABLES ON GRID_PDX_BMP_GRID.BMP_Type1 = GRID_BMP_TABLES.BMP_TYPE) " +
                "ON GRID_BMP_TYPE_TABLE_GENERAL.BMP_TYPE_GEN_ID = GRID_BMP_TABLES.BMP_TYPE_GEN_ID " +
                "WHERE (GRID_BMP_TABLES.SA_SOURCE='PRF') ";

            if (specialConditions > 0)
            {
                prfListQuery += "AND ";
            }
            if (!inStream)
            {
                prfListQuery += "((GRID_PDX_BMP_GRID.INSTREAM)=0) ";
            }

            //Should this simply assume that the timePeriod is EX considering that
            //the only case the specialconditions can be greater than one is when
            //timePeriod is EX?
            if (specialConditions > 1)
            {
                prfListQuery += "AND ";
            }
            if (timePeriod == "EX")
            {
                prfListQuery += "((GRID_PDX_BMP_GRID.TimeFrame)= 'EX');";
            }
            try
            {
                SQLHelper.SQLCreateVIEW("GRID_PRF_LIST", prfListQuery, SQLDatabaseConnectionString);
                if (timePeriod == "EX")
                {
                    SQLHelper.SQLCreateVIEW("GRID_PRF_LIST_EX", prfListQuery, SQLDatabaseConnectionString);
                }
                if (timePeriod == "FU")
                {
                    SQLHelper.SQLCreateVIEW("GRID_PRF_LIST_FU", prfListQuery, SQLDatabaseConnectionString);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Execute a GridModelRun collection containing one or more GridModelRun instances.
        /// </summary>
        /// <returns>A GridModelOutput object containing model run metadata</returns>
        public GridModelOutput ExecuteModels()
        {
            
            GridModelOutput gridModelOutput = new GridModelOutput();
            
            //copy all of the important tables from the access database to the SQL server database
           
            SQLHelper.SQLExecuteActionQuery("GRID_ClearCompiledResults", SQLDatabaseConnectionString);
            foreach (GridModelRun gridModelRun in gridModelRuns)
            {
                bool boolContainsBMP = false;
                foreach (GridProcessGroup gridProcessGroup in gridModelRun.GridProcessGroups.OrderBy(gridProcessGroup => gridProcessGroup.GroupOrder))
                {
                    if (gridProcessGroup.GroupName.Contains("GRID_BMP") && boolContainsBMP == false)
                    {
                        CreatePRFListQuery(gridModelRun.InstreamFacilities, gridModelRun.TimePeriod);
                        CreateBMPUnionQuery();
                        if (string.Compare(gridModelRuns[0].BMPEffectivenessDB, SQLDatabaseConnectionString) != 0)
                        {
                            if (gridModelRuns[0].BMPEffectivenessDB.IndexOf('\\') >= 0)
                            {
                                DataMobility.SQLCopyAccessTable("BMP_TYPE_TABLE_GENERAL", gridModelRuns[0].BMPEffectivenessDB, "GRID_BMP_TYPE_TABLE_GENERAL", SQLDatabaseConnectionString);
                                DataMobility.SQLCopyAccessTable("BMP_TABLES", gridModelRuns[0].BMPEffectivenessDB, "GRID_BMP_TABLES", SQLDatabaseConnectionString);
                            }
                            else
                            {
                                DataMobility.SQLCopySQLTable("GRID_BMP_TABLES",
                                    gridModelRuns[0].BMPEffectivenessDB, "GRID_BMP_TABLES", SQLDatabaseConnectionString);
                                DataMobility.SQLCopySQLTable("GRID_BMP_TABLES",
                                    gridModelRuns[0].BMPEffectivenessDB, "GRID_BMP_TABLES", SQLDatabaseConnectionString);
                            }
                        }
                        boolContainsBMP = true;
                    }
                }

                try
                {
                    this.StatusChanged("Executing " + gridModelRun.ModelDescription);
                    
                    ExecuteModel(gridModelRun);
                    SQLHelper.SQLExecuteActionQuery("GRID_CompileResults", SQLDatabaseConnectionString);
                    gridModelOutput.AddPollutantLoadingMetadataDS(gridModelRun.ScenarioDescription);
                }
                catch (Exception ex)
                {
                    string msg = "Error executing model '" + gridModelRun.ModelDescription + "': " + ex.Message;
                    throw new Exception(msg);
                }
            }
            gridModelOutput.GridModelResults = gridModelResults;
            accessHelper.Dispose();
            return gridModelOutput;
        }
        private void ExecuteModel(GridModelRun gridModelRun)
        {            
            //runcount may also be a SQL stored procedure loop
            int runCount = 1;
            int totalRunCount = gridModelRun.GridModelTimeSteps.Count;

            ExecuteTimeSteps(gridModelRun);
            //this could be part of the sql stored procedure loop
            
            foreach (GridModelTimeStep gridModelTimeStep in gridModelRun.GridModelTimeSteps)
            {
                this.StatusChanged(gridModelRun.ModelDescription + ": " + "Exporting results");

                string runDescription = GetFormattedRunCount(runCount, totalRunCount);
                string outputFile = outputDirectory + "\\" + gridModelRun.Area + "_" + gridModelRun.SubArea + "\\" + gridModelRun.ScenarioDescription + "\\" + runDescription + "_" + gridModelTimeStep.Comment + ".csv";

                ExportResultsAllTimeSteps(outputFile, totalRunCount);
                GridModelResult gridModelResult;
                gridModelResult = new GridModelResult(outputFile, gridModelRun.Area, gridModelRun.SubArea, gridModelRun.ModelDescription, gridModelTimeStep.Comment);
                gridModelResults.Add(gridModelResult);
                Dictionary<string, double> modelResultsSummary;

                /*modelResultsSummary = SQLHelper.SQLExecuteAggregateQueryDoubles("GRID_calc_table_summary", SQLDatabaseConnectionString);
                foreach (KeyValuePair<string, double> kvp in modelResultsSummary)
                {
                    try
                    {
                        gridModelResult.PollutantLoads.Add(new PollutantLoad(kvp.Key, (double)kvp.Value));
                    }
                    catch (Exception e)
                    {
                        gridModelResult.PollutantLoads.Add(new PollutantLoad(kvp.Key, (double)0));
                    }
                }*/
                runCount++;
            }
            return;
        }

        private void ExecuteTimeSteps(GridModelRun gridModelRun)
        {
            //Determine if the model is based on mapped impervious or zoning impervious.
            int ImperviousMethod = 0;
            foreach (GridProcessGroup gridProcessGroup in gridModelRun.GridProcessGroups)
            {
                if (gridProcessGroup.GroupName == "GRID_MAPPED_IA")
                {
                    ImperviousMethod = 1;
                }
                else if (gridProcessGroup.GroupName == "GRID_ZONING_IA")
                {
                    ImperviousMethod = 2;
                }
            }
            foreach (GridProcessGroup gridProcessGroup in gridModelRun.GridProcessGroups.OrderBy(gridProcessGroup => gridProcessGroup.GroupOrder))
            {
                try
                {
                    this.StatusChanged(gridModelRun.ModelDescription + ": Executing process '" + gridProcessGroup.GroupName + "'");/*gridProcess.ProcessName + "'");*/
                    if (gridProcessGroup.GroupName == "GRID_SETUP" )
                    {
                        SQLHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, "@SelectionSetID", gridModelRun.SelectionSetAreaID, "@ProjectID", ProjectID, "@ScenarioID", gridModelRun.ScenarioID, SQLDatabaseConnectionString);
                    }
                    else if (gridProcessGroup.GroupName == "GRID_RUNOFF")
                    {
                        SQLHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, "@SelectionSetID", gridModelRun.SelectionSetAreaID, SQLDatabaseConnectionString);
                    }
                    else if (gridProcessGroup.GroupName == "GRID_COMMON")
                    {
                        SQLHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, "@SelectionSetID", gridModelRun.SelectionSetAreaID, SQLDatabaseConnectionString);
                    }
                    else if (gridProcessGroup.GroupName == "GRID_LOAD_BOD")
                    {
                        SQLHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, "@SelectionSetID", gridModelRun.SelectionSetAreaID, "@ImperviousType", ImperviousMethod, SQLDatabaseConnectionString);
                    }
                    else if (gridProcessGroup.GroupName == "GRID_LOAD_ECOLI")
                    {
                        SQLHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, "@SelectionSetID", gridModelRun.SelectionSetAreaID, "@ImperviousType", ImperviousMethod, SQLDatabaseConnectionString);
                    }
                    else if (gridProcessGroup.GroupName == "GRID_LOAD_PbD")
                    {
                        SQLHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, "@SelectionSetID", gridModelRun.SelectionSetAreaID, "@ImperviousType", ImperviousMethod, SQLDatabaseConnectionString);
                    }
                    else if (gridProcessGroup.GroupName == "GRID_LOAD_TP")
                    {
                        SQLHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, "@SelectionSetID", gridModelRun.SelectionSetAreaID, "@ImperviousType", ImperviousMethod, SQLDatabaseConnectionString);
                    }
                    else if (gridProcessGroup.GroupName == "GRID_LOAD_TSS")
                    {
                        SQLHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, "@SelectionSetID", gridModelRun.SelectionSetAreaID, "@ImperviousType", ImperviousMethod, SQLDatabaseConnectionString);
                    }
                    else
                    {
                        SQLHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, SQLDatabaseConnectionString);
                    }
                }
                catch (Exception ex)
                {
                    /*if (gridProcess.Critical)
                    {
                        throw new Exception("Unable to execute critical Grid Query '" + gridProcess.ProcessName + "': " + ex.Message);
                    }*/
                }
            }

            return;
        }

        private static string GetFormattedRunCount(int runCount, int totalRunCount)
        {
            string runDescription;

            runDescription = runCount.ToString();
            string totalRunCountToString = totalRunCount.ToString();
            runDescription = runDescription.PadLeft(totalRunCountToString.Length, '0');
        
            return runDescription;
        }

        private void SetGridVariable(string name, double value)
        {
            string variableTableName;
            variableTableName = ConfigurationManager.AppSettings.Get("variableTableName");
            string variableKeyFieldName;
            variableKeyFieldName = ConfigurationManager.AppSettings.Get("variableKeyFieldName");
            string variableValueFieldName;
            variableValueFieldName = ConfigurationManager.AppSettings.Get("variableValueFieldName");
            SQLHelper.SQLWriteKeyValueTable(variableTableName, variableKeyFieldName, name, variableValueFieldName, value, SQLDatabaseConnectionString);
        }

        private void ExportResultsAllTimeSteps(string outputFileName, int totalRunCount)
        {
            string totalRunCountToString = totalRunCount.ToString();
            if (!Directory.Exists(Path.GetFullPath(outputFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputFileName));
            }
            try
            {
                SQLHelper.SQLExportTablePortion("GRID_GridResults", outputFileName, "comment", (Path.GetFileNameWithoutExtension(outputFileName)).Remove(0, totalRunCountToString.Length+1), SQLDatabaseConnectionString);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                SQLHelper.SQLExportTablePortion("GRID_GridResults", outputFileName, "comment", (Path.GetFileNameWithoutExtension(outputFileName)).Remove(0, totalRunCountToString.Length+1), SQLDatabaseConnectionString);
            }
            return;
        }

        private void ExportResults(string outputFileName)
        {
            if (!Directory.Exists(Path.GetFullPath(outputFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputFileName));
            }
            try
            {
                SQLHelper.SQLExportTable("GRID_GridResults", outputFileName, SQLDatabaseConnectionString);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                SQLHelper.SQLExportTable("GRID_GridResults", outputFileName, SQLDatabaseConnectionString);
            }
            return;
        }

        #region Report status to UI
        /// <summary>
        /// Event handler for reporting status changes.
        /// </summary>
        public delegate void OnStatusChangedEventHandler(string status);

        public event OnStatusChangedEventHandler StatusChanged;

        /// <summary>
        /// Arguments for the OnStatusChanged event.
        /// </summary>
        public class StatusChangedArgs : EventArgs
        {
            private string status;

            /// <summary>
            /// Sets the status reported by the OnStatusChanged event.
            /// </summary>
            /// <param name="status">The status message</param>
            public StatusChangedArgs(string status)
            {
                this.status = status;
            }

            /// <summary>
            /// Returns the new status message after
            /// a OnStatusChanged event.
            /// </summary>
            public string NewStatus
            {
                get { return this.status; }
            }

        }

        ///<summary>
        /// This function should populate empty database with new queries.
        /// Its kind of necessary since people who want to make their own
        /// grid model shouldnt have to be weighted down by inserting the actual
        /// queries and views.  It is however unfortunate that I should
        /// have to make this thing since it is so long.  Isn't there some way
        /// I can just stick all the queries in a text file and have that file
        /// be read and placed in the server as the queries?
        /// </summary>
        private void heresYourFreakinQueries()
        {

        }
        #endregion
    }

}
