using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using SystemsAnalysis.Utils.AccessUtils;

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

        private AccessHelper accessHelper;

        public GridModelEngine(int projectID, string projectDescription, string gridDataPath,
            string bmpPath, string mipPath, string osfPath, string outputDirectory)
            : this()
        {
            this.projectID = projectID;
            this.projectDescription = projectDescription;
            this.gridDataPath = gridDataPath;
            this.prfPath = bmpPath;
            this.mipPath = mipPath;
            this.osfPath = osfPath;
            this.outputDirectory = outputDirectory;
        }
        public GridModelEngine()
        {            
            this.gridModelPath = System.AppDomain.CurrentDomain.BaseDirectory + "Waterqual_GIS_v5_0.mdb";
            
            gridDataTableName = ConfigurationManager.AppSettings.Get("gridDataTableName");

            gridModelRuns = new List<GridModelRun>();
            gridModelResults = new List<GridModelResult>();
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

            accessHelper.SQLCreateVIEW("GRID_calc_table", calcTableQuery);
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
                accessHelper.SQLCopyAccessTable(bmpTableName, prfPath, "GRID_" + bmpTableName);
                bmpUnionQuery += "SELECT * FROM GRID_PRF_LIST ";
                bmpSourceCount++;
            }
            if (mipPath != "")
            {
                accessHelper.SQLCopyAccessTable(mipTableName, mipPath, "GRID_" + mipTableName);
                if (bmpSourceCount > 0)
                {
                    bmpUnionQuery += "UNION ";
                }
                bmpUnionQuery += "SELECT * FROM GRID_MIP_LIST ";
                bmpSourceCount++;
            }
            if (osfPath != "")
            {
                accessHelper.SQLCopyAccessTable(osfTableName, osfPath, "GRID_" + osfTableName);
                if (bmpSourceCount > 0)
                {
                    bmpUnionQuery += "UNION ";
                }
                bmpUnionQuery += " SELECT * FROM GRID_OSF_LIST ";
            }
            bmpUnionQuery += ";";

            accessHelper.SQLCreateVIEW("GRID_BMP_LIST_UNION_QRY", bmpUnionQuery);
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
            accessHelper.SQLCreateVIEW("GRID_PRF_LIST", prfListQuery);
        }

        /// <summary>
        /// Execute a GridModelRun collection containing one or more GridModelRun instances.
        /// </summary>
        /// <returns>A GridModelOutput object containing model run metadata</returns>
        public GridModelOutput ExecuteModels()
        {
            accessHelper = new AccessHelper(gridModelPath, /*"Data Source=WS09858\\SQLEXPRESS;Initial Catalog=PortlandHarbor;Integrated Security=True"*/"Data Source=SIRTOBY;Initial Catalog=SANDBOX;Persist Security Info=True;User ID=GIS;Password=Extra$hade");

            GridModelOutput gridModelOutput = new GridModelOutput();
            
            //copy all of the important tables from the access database to the SQL server database
            accessHelper.SQLCopyAccessTable("ZONING_IMP", gridModelPath, "GRID_ZONING_IMP");
            accessHelper.SQLCopyAccessTable("variables", gridModelPath, "GRID_variables");
            accessHelper.SQLCopyAccessTable("FE_SELECTION_SETS", gridModelPath, "GRID_FE_SELECTION_SETS");
            accessHelper.SQLCopyAccessTable("FE_SELECTION_SET_AREAS", gridModelPath, "GRID_FE_SELECTION_SET_AREAS");
            accessHelper.SQLCopyAccessTable("FE_SCENARIOS", gridModelPath, "GRID_FE_SCENARIOS");
            accessHelper.SQLCopyAccessTable("FE_SCENARIO_X_PROCESS", gridModelPath, "GRID_FE_SCENARIO_X_PROCESS");
            accessHelper.SQLCopyAccessTable("FE_PROCESS_GROUP", gridModelPath, "GRID_FE_PROCESS_GROUP");
            accessHelper.SQLCopyAccessTable("FE_PROCESS", gridModelPath, "GRID_FE_PROCESS");
            accessHelper.SQLCopyAccessTable("FE_MODEL_RUN", gridModelPath, "GRID_FE_MODEL_RUN");
            accessHelper.SQLCopyAccessTable("FE_HYETOGRAPHS", gridModelPath, "GRID_FE_HYETOGRAPHS");
            accessHelper.SQLCopyAccessTable("FE_HYETOGRAPH_DATA", gridModelPath, "GRID_FE_HYETOGRAPH_DATA");
            accessHelper.SQLCopyAccessTable("FE_GRID_PROJECTS", gridModelPath, "GRID_FE_GRID_PROJECTS");
            accessHelper.SQLCopyAccessTable("Contaminants", gridModelPath, "GRID_Contaminants");
            accessHelper.SQLCopyAccessTable("BMP_TYPE_TABLE_GENERAL", gridModelPath, "GRID_BMP_TYPE_TABLE_GENERAL");

            GridDataTable = gridDataTableName;
            /////Moved from ExecuteModel
            accessHelper.SQLCopyAccessTable(gridDataTableName, gridDataPath, "GRID_" + gridDataTableName);
            accessHelper.SQLCopyAccessTable(gridModelRuns[0].BMPEffectivenessTable,
                gridModelRuns[0].BMPEffectivenessDB, "GRID_BMP_PERFORMANCE");
            accessHelper.SQLCopyAccessTable(gridModelRuns[0].PollutantLoadingTable,
                gridModelRuns[0].PollutantLoadingDB, "GRID_pollutant_loadings");

            CreatePRFListQuery(gridModelRuns[0].InstreamFacilities, gridModelRuns[0].TimePeriod);
            CreateBMPUnionQuery();
            foreach (GridModelRun gridModelRun in gridModelRuns)
            {
                try
                {
                    this.StatusChanged("Executing " + gridModelRun.ModelDescription);
                    //////
                    ExecuteModel(gridModelRun);
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

                ExportResultsAllTimeSteps(outputFile);
                GridModelResult gridModelResult;
                gridModelResult = new GridModelResult(outputFile, gridModelRun.Area, gridModelRun.SubArea, gridModelRun.ModelDescription, gridModelTimeStep.Comment);
                gridModelResults.Add(gridModelResult);
                Dictionary<string, double> modelResultsSummary;

                modelResultsSummary = accessHelper.SQLExecuteAggregateQueryDoubles("GRID_calc_table_summary");
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
                }
                runCount++;
            }
            return;
        }

        private void ExecuteTimeSteps(GridModelRun gridModelRun)
        {
            foreach (GridProcessGroup gridProcessGroup in gridModelRun.GridProcessGroups.OrderBy(gridProcessGroup => gridProcessGroup.GroupOrder))
            {
                try
                {
                    this.StatusChanged(gridModelRun.ModelDescription + ": Executing process '" + gridProcessGroup.GroupName + "'");/*gridProcess.ProcessName + "'");*/
                    if (gridProcessGroup.GroupName == "GRID_SETUP_RAINMESH" )
                    {
                        accessHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, "@SelectionSetID", gridModelRun.SelectionSetAreaID, "@ProjectID", ProjectID);
                    }
                    else if (gridProcessGroup.GroupName == "GRID_RUNOFF_RAINMESH")
                    {
                        accessHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName, "@SelectionSetID", gridModelRun.SelectionSetAreaID);
                    }
                    else
                    {
                        accessHelper.SQLExecuteActionQuery(gridProcessGroup.GroupName);
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
            if (totalRunCount == 1)
            {
                runDescription = "";
            }
            else
            {
                runDescription = runCount.ToString();
                string totalRunCountToString = totalRunCount.ToString();
                runDescription = runDescription.PadLeft(totalRunCountToString.Length, '0');
            }
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
            accessHelper.SQLWriteKeyValueTable(variableTableName, variableKeyFieldName, name, variableValueFieldName, value);
        }

        private void ExportResultsAllTimeSteps(string outputFileName)
        {
            if (!Directory.Exists(Path.GetFullPath(outputFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputFileName));
            }
            try
            {
                accessHelper.SQLExportTablePortion("GRID_GridResults", outputFileName, AccessHelper.FileType.CSV, "comment", (Path.GetFileNameWithoutExtension(outputFileName)).Remove(0,3));
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                accessHelper.SQLExportTablePortion("GRID_GridResults", outputFileName, AccessHelper.FileType.CSV, "comment", (Path.GetFileNameWithoutExtension(outputFileName)).Remove(0,3));
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
                accessHelper.SQLExportTable("GRID_GridResults", outputFileName, AccessHelper.FileType.CSV);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                accessHelper.SQLExportTable("GRID_GridResults", outputFileName, AccessHelper.FileType.CSV);
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
        #endregion
    }

}
