using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.Grid.GridAnalysis;


namespace SystemsAnalysis.Grid.GridAnalysis
{
    public class GridModelRun
    {        
        private int selectionSetAreaID;
        private int scenarioID;
        private int hyetographID;        
        private string area;
        private string subArea;        
        private string timePeriod;        
        private string scenarioDescription;
        private string projectDescription;
        private string hyetographDescription;
        private bool instreamFacilities;
        private string pollutantLoadingDB;
        private string pollutantLoadingTable;
        private string bmpEffectivenessDB;
        private string bmpEffectivenessTable;
        private List<GridModelTimeStep> gridModelTimeSteps;
        private List<GridProcessGroup> gridProcessGroups;        

        public GridModelRun(GridInterfaceDataSet.QryModelRunRow modelRunRow)
            : this()
        {
            this.selectionSetAreaID = modelRunRow.selection_set_area_id;
            this.scenarioID = modelRunRow.scenario_id;
            this.hyetographID = modelRunRow.hyetograph_id;
            this.selectionSetAreaID = modelRunRow.selection_set_area_id;
            this.scenarioID = modelRunRow.scenario_id;
            this.area = modelRunRow.area;
            this.subArea = modelRunRow.sub_area;            
            this.timePeriod = modelRunRow.time_period;
            this.scenarioDescription = modelRunRow.scenario_description;            
            this.projectDescription = modelRunRow.project_description;
            this.hyetographDescription = modelRunRow.hyetograph_description;
            this.instreamFacilities = modelRunRow.include_instream_facilities;
            this.pollutantLoadingDB = modelRunRow.pollutant_loading_db;
            this.pollutantLoadingTable = modelRunRow.pollutant_loading_table;
            this.bmpEffectivenessDB = modelRunRow.bmp_effectiveness_db;
            this.bmpEffectivenessTable = modelRunRow.bmp_effectiveness_table;
            return;
        }
        public GridModelRun()
        {
            gridModelTimeSteps = new List<GridModelTimeStep>();
            gridProcessGroups = new List<GridProcessGroup>();
        }

        #region Property accessors for Xml serialization
        public int SelectionSetAreaID { get { return selectionSetAreaID; } set { selectionSetAreaID = value; } }
        public int ScenarioID { get { return scenarioID; } set { scenarioID = value; } }
        public int HyetographID { get { return hyetographID; } set { hyetographID = value; } }
        public string Area { get { return area; } set { area = value; } }
        public string SubArea { get { return subArea; } set { subArea = value; } }        
        public string TimePeriod { get { return timePeriod; } set { timePeriod = value; } }        
        public string ProjectDescription { get { return projectDescription; } set { projectDescription = value; } }
        public string HyetographDescription { get { return hyetographDescription; } set { hyetographDescription = value; } }
        public string ScenarioDescription { get { return scenarioDescription; } set { scenarioDescription = value; } }
        public string ModelDescription
        {
            get
            {
                return area + " " + subArea + " " + scenarioDescription;
            }
            set { }
        }
        public bool InstreamFacilities { get { return instreamFacilities; } set { instreamFacilities = value; } }
        public string PollutantLoadingDB { get { return pollutantLoadingDB; } set { pollutantLoadingDB = value; } }
        public string PollutantLoadingTable { get { return pollutantLoadingTable; } set { pollutantLoadingTable = value; } }
        public string BMPEffectivenessDB { get { return bmpEffectivenessDB; } set { bmpEffectivenessDB = value; } }
        public string BMPEffectivenessTable { get { return bmpEffectivenessTable; } set { bmpEffectivenessTable = value; } }
        public List<GridModelTimeStep> GridModelTimeSteps { get { return gridModelTimeSteps; } set { gridModelTimeSteps = value; } }
        public List<GridProcessGroup> GridProcessGroups { get { return gridProcessGroups; } set { gridProcessGroups = value; } }
        #endregion


    }


}
