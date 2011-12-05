using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SystemsAnalysis.Utils.AccessUtils;

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public class GridModelOutput
    {
        private DateTime runDate;
        private string userName;
        private List<GridModelResult> gridModelResults;
        private List<PollutantLoadingMetadataDataSet> pollutantLoadingMetadataDataSets;

        public GridModelOutput()
        {
            runDate = System.DateTime.Now;
            userName = System.Environment.UserName;
            pollutantLoadingMetadataDataSets = new List<PollutantLoadingMetadataDataSet>();
        }

        //Pass directory or list of files containing .csv results (will retrive this list from modelResultsXml)
        //Iterate through .csv results list
        //  On first pass, create new results table and import first .csv file
        //  Add record to results table indicating where results came from 
        //      - Which area/subarea, which statistics where used - AKA ModelRun Metadata
        //  On subsequent passes, append .csv file to results table
        //  
        //  Once all results are loaded into a results table, we can group by model metadata fields
        //  to summarize pollutant loading for each model run - This is the output the user is interested in

        public string RunDate
        {
            get { return runDate.ToShortDateString(); }
            set { runDate = DateTime.Parse(value); }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public void ExportToDatabase(string modelResults, string outputDatabase, string outputTableName)
        {
            //Get grid table name using: gridDataTableName = ConfigurationManager.AppSettings.Get("gridDataTableName");
            string gridTableName = ConfigurationManager.AppSettings.Get("gridDataTableName");
            
            //assume modelResults is the path to the text file that contains the modelresults

            //we need to translate the text file into an access table for mapinfo

            //Use AccessHelper for all interfacing with .mdb files
            //Use AccessHelper to connect to the database
            AccessHelper accessHelper = new AccessHelper(outputDatabase);
            accessHelper.ImportTable(modelResults, outputTableName, AccessHelper.FileType.CSV);
            accessHelper.Dispose();

            //We have an outputTableName, but there doesn't seem to be an accessHelper
            //method for creating a table within the database.

            /*dao.TableDef linkTable;
            Object missingParam = System.Reflection.Missing.Value;
            linkTable = this.accessApp.CurrentDb().CreateTableDef(linkName, missingParam, missingParam, missingParam);
            linkTable.Connect = ";DATABASE=" + sourceDatabase;
            linkTable.SourceTableName = tableName;
            accessApp.CurrentDb().TableDefs.Append(linkTable);  */
            
           	//validate the output database (access database)
           	//validate the output table (access table)
        }

        public void LoadResults(string outputDatabase, string outputTableName)
        {
            AccessHelper ah;
            ah = new AccessHelper(outputDatabase);
            //The "csvfile" entry of GridModelResultList should be the place where we find all of the 

            GridModelResult gmr = gridModelResults[0];
            ah.ImportTable(gmr.ResultFile, outputTableName, AccessHelper.FileType.CSV);
            ah.AddField(outputTableName, "Area", AccessHelper.FieldType.STRING);
            ah.AddField(outputTableName, "SubArea", AccessHelper.FieldType.STRING);
            ah.AddField(outputTableName, "TimeStepDescription", AccessHelper.FieldType.STRING);
            ah.UpdateField(outputTableName, "Area", gmr.Area);
            ah.UpdateField(outputTableName, "SubArea", gmr.SubArea);
            ah.UpdateField(outputTableName, "TimeStepDescription", gmr.TimeStepDescription);

            for (int i = 1; i < gridModelResults.Count; i++)
            {
                gmr = gridModelResults[i];

                ah.ImportTable(gmr.ResultFile, "temp_table", AccessHelper.FileType.CSV);
                ah.AddField("temp_table", "Area", AccessHelper.FieldType.STRING);
                ah.AddField("temp_table", "SubArea", AccessHelper.FieldType.STRING);
                ah.AddField("temp_table", "TimeStepDescription", AccessHelper.FieldType.STRING);
                ah.UpdateField("temp_table", "Area", gmr.Area);
                ah.UpdateField("temp_table", "SubArea", gmr.SubArea);
                ah.UpdateField("temp_table", "TimeStepDescription", gmr.TimeStepDescription);

                if(ah.SchemasMatch(outputTableName, "temp_table"))
                {
                    ah.AppendTable("temp_table", outputTableName);
                }
                else
                {
                    throw new Exception("Schema of " + gmr.ResultFile + " does not match schema of " + outputTableName);
                }
                ah.DeleteTable("temp_table");
            }

            ah.Dispose();
        }

        public void GeocodeOutput()
        {            
            //Geocode(
        }

        public List<GridModelResult> GridModelResults
        {
            get { return this.gridModelResults; }
            set { this.gridModelResults = value; }
        }

        public void AddPollutantLoadingMetadataDS(string scenarioDescription)
        {
            PollutantLoadingMetadataDataSet plmdDS;
            plmdDS = new PollutantLoadingMetadataDataSet();
            PollutantLoadingMetadataDataSetTableAdapters.BmpPerformanceTableAdapter bmpTA;
            bmpTA = new PollutantLoadingMetadataDataSetTableAdapters.BmpPerformanceTableAdapter();
            PollutantLoadingMetadataDataSetTableAdapters.PollutantLoadingsTableAdapter pollutantLoadTA;
            pollutantLoadTA = new PollutantLoadingMetadataDataSetTableAdapters.PollutantLoadingsTableAdapter();
            try
            {
                bmpTA.Fill(plmdDS.BmpPerformance);
            }
            catch (Exception ex)
            {
                //put in a placeholder for bmp info, as this model contains none.
            }
            try
            {
                pollutantLoadTA.Fill(plmdDS.PollutantLoadings);
            }
            catch (Exception ex)
            {
                //put in a placeholder for pollutant loadings, as this model contains none.
            }
            plmdDS.ScenarioDescription = scenarioDescription;
            this.pollutantLoadingMetadataDataSets.Add(plmdDS);
        }
        public List<PollutantLoadingMetadataDataSet> GetPollutantLoadingMetadataDSList()
        {
            return this.pollutantLoadingMetadataDataSets;
        }
    }
}
