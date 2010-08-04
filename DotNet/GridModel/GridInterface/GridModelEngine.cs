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
            calcTableQuery = "SELECT WshdGrd100FtOpt.*, " +
                "FE_SELECTION_SETS.percent_overlap as grid_overlap, " +
                "GridIntermediateResults.Description AS IntermediateResultsDescription, " +
                "GridIntermediateResults.IMP_pct, GridIntermediateResults.COL_B, " +
                "GridIntermediateResults.COL_C, GridIntermediateResults.COL_D, " +
                "GridIntermediateResults.COL_E, GridIntermediateResults.COL_F, " +
                "GridIntermediateResults.COL_G, GridIntermediateResults.COL_H, " +
                "GridIntermediateResults.COL_I, GridIntermediateResults.COL_J, " +
                "GridIntermediateResults.COL_K, GridIntermediateResults.COL_L, " +
                "GridIntermediateResults.COL_M, GridIntermediateResults.COL_N, " +
                "GridIntermediateResults.COL_O, GridIntermediateResults.COL_P, GridIntermediateResults.COL_Q, " +
                "GridResults.Description AS GridResultsDescription, " +
                "GridResults.COL_1, GridResults.COL_2, GridResults.COL_3, " +
                "GridResults.TP, GridResults.TP_DRY, GridResults.TP_WET, " +
                "GridResults.TSS, GridResults.TSS_DRY, GridResults.TSS_WET, " +
                "GridResults.BOD, GridResults.BOD_DRY, GridResults.BOD_WET," +
                "GridResults.ECOLI, GridResults.ECOLI_DRY, GridResults.ECOLI_WET, " +
                "GridResults.PbD, GridResults.PbD_DRY, GridResults.PbD_WET " +
                "FROM ((WshdGrd100FtOpt INNER JOIN FE_SELECTION_SETS " +
                "ON WshdGrd100FtOpt.Description = FE_SELECTION_SETS.description) " +
                "LEFT JOIN GridResults " +
                "ON FE_SELECTION_SETS.description = GridResults.Description) " +
                "LEFT JOIN GridIntermediateResults " +
                "ON WshdGrd100FtOpt.Description = GridIntermediateResults.Description " +
                "WHERE (((FE_SELECTION_SETS.selection_set_area_id)= " + selectionSetAreaID + "));";

            accessHelper.CreateQuery("calc_table", calcTableQuery);
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
                accessHelper.LinkTable(bmpTableName, prfPath);
                bmpUnionQuery += "SELECT * FROM PRF_LIST ";
                bmpSourceCount++;
            }
            if (mipPath != "")
            {
                accessHelper.LinkTable(mipTableName, mipPath);
                if (bmpSourceCount > 0)
                {
                    bmpUnionQuery += "UNION ";
                }
                bmpUnionQuery += "SELECT * FROM MIP_LIST ";
                bmpSourceCount++;
            }
            if (osfPath != "")
            {
                accessHelper.LinkTable(osfTableName, osfPath);
                if (bmpSourceCount > 0)
                {
                    bmpUnionQuery += "UNION ";
                }
                bmpUnionQuery += " SELECT * FROM OSF_LIST ";
            }
            bmpUnionQuery += ";";

            accessHelper.CreateQuery("BMP_LIST_UNION_QRY", bmpUnionQuery);
            return;
        }
        private void CreatePRFListQuery(bool inStream, string timePeriod)
        {
            string prfListQuery;
            int specialConditions;
            specialConditions = (!inStream ? 1 : 0) + (timePeriod == "EX" ? 1 : 0);
            prfListQuery = "SELECT PDX_BMP_GRID.Description, BMP_TABLES.BMP_TYPE, " +
                "BMP_TYPE_TABLE_GENERAL.BMP_TYPE_GEN_ID, BMP_TYPE_TABLE_GENERAL.BMP_TYPE_DESCRIPTION, " +
                "PDX_BMP_GRID.Percent_Overlap " +
                "FROM BMP_TYPE_TABLE_GENERAL INNER JOIN " +
                "(PDX_BMP_GRID INNER JOIN BMP_TABLES ON PDX_BMP_GRID.BMP_Type1 = BMP_TABLES.BMP_TYPE) " +
                "ON BMP_TYPE_TABLE_GENERAL.BMP_TYPE_GEN_ID = BMP_TABLES.BMP_TYPE_GEN_ID " +
                "WHERE (BMP_TABLES.SA_SOURCE=\"PRF\") ";

            if (specialConditions > 0)
            {
                prfListQuery += "AND ";
            }
            if (!inStream)
            {
                prfListQuery += "((PDX_BMP_GRID.INSTREAM)=False) ";
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
                prfListQuery += "((PDX_BMP_GRID.TimeFrame)=\"EX\");";
            }
            accessHelper.CreateQuery("PRF_LIST", prfListQuery);
        }

        /// <summary>
        /// Execute a GridModelRun collection containing one or more GridModelRun instances.
        /// </summary>
        /// <returns>A GridModelOutput object containing model run metadata</returns>
        public GridModelOutput ExecuteModels()
        {
            accessHelper = new AccessHelper(gridModelPath);

            GridModelOutput gridModelOutput = new GridModelOutput();            
            foreach (GridModelRun gridModelRun in gridModelRuns)
            {
                try
                {
                    this.StatusChanged("Executing " + gridModelRun.ModelDescription);
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
            accessHelper.LinkTable(gridDataTableName, gridDataPath);
            accessHelper.LinkTable(gridModelRun.BMPEffectivenessTable,
                gridModelRun.BMPEffectivenessDB, "BMP_PERFORMANCE");
            accessHelper.LinkTable(gridModelRun.PollutantLoadingTable,
                gridModelRun.PollutantLoadingDB, "pollutant_loadings");

            CreatePRFListQuery(gridModelRun.InstreamFacilities, gridModelRun.TimePeriod);
            CreateBMPUnionQuery();
            CreateCalcTableQuery(gridModelRun.SelectionSetAreaID);

            double rainfall;

            int runCount = 1;
            int totalRunCount = gridModelRun.GridModelTimeSteps.Count;

            foreach (GridModelTimeStep gridModelTimeStep in gridModelRun.GridModelTimeSteps)
            {
                rainfall = gridModelTimeStep.Rainfall;
                ExecuteTimeStep(gridModelRun, rainfall);

                this.StatusChanged(gridModelRun.ModelDescription + ": " + "Exporting results");

                string runDescription = GetFormattedRunCount(runCount, totalRunCount);
                string outputFile = outputDirectory + "\\" + gridModelRun.Area + "_" + gridModelRun.SubArea + "\\" + gridModelRun.ScenarioDescription + "\\" + runDescription + "_" + gridModelTimeStep.Comment + ".csv";

                ExportResults(outputFile);
                GridModelResult gridModelResult;
                gridModelResult = new GridModelResult(outputFile, gridModelRun.Area, gridModelRun.SubArea, gridModelRun.ModelDescription, gridModelTimeStep.Comment);
                gridModelResults.Add(gridModelResult);
                Dictionary<string, double> modelResultsSummary;

                modelResultsSummary = accessHelper.ExecuteAggregateQueryDoubles("calc_table_summary");
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
        private void ExecuteTimeStep(GridModelRun gridModelRun, double rainfall)
        {
            SetRainfall(rainfall);

            foreach (GridProcessGroup gridProcessGroup in gridModelRun.GridProcessGroups.OrderBy(gridProcessGroup => gridProcessGroup.GroupOrder))
            {
                foreach (GridProcess gridProcess in gridProcessGroup.GridProcesses.OrderBy(gridProcess => gridProcess.ProcessOrder))
                {
                    try
                    {
                        this.StatusChanged(gridModelRun.ModelDescription + ": Executing process '" + gridProcess.ProcessName + "'");
                        accessHelper.ExecuteActionQuery(gridProcess.ProcessName);
                    }
                    catch (Exception ex)
                    {
                        if (gridProcess.Critical)
                        {
                            throw new Exception("Unable to execute critical Grid Query '" + gridProcess.ProcessName + "': " + ex.Message);
                        }
                    }
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
            accessHelper.WriteKeyValueTable(variableTableName, variableKeyFieldName, name, variableValueFieldName, value);
        }

        private void SetRainfall(double rainfall)
        {
            SetGridVariable("Precip", rainfall);
        }

        private void ExportResults(string outputFileName)
        {
            if (!Directory.Exists(Path.GetFullPath(outputFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputFileName));
            }
            try
            {
                accessHelper.ExportTable("GridResults", outputFileName, AccessHelper.FileType.CSV);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                accessHelper.ExportTable("GridResults", outputFileName, AccessHelper.FileType.CSV);
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
