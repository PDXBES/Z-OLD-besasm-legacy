namespace SystemsAnalysis.Grid.GridAnalysis
{
    partial class FrmGridAnalysis
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGridAnalysis));
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_GRID_PROJECTSFE_SCENARIOS", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("project_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("time_period");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("include_instream_facilities");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pollutant_loading_db");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pollutant_loading_table");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bmp_effectiveness_db");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bmp_effectiveness_table");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_SCENARIOSFE_SCENARIO_X_PROCESS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_SCENARIOSFE_MODEL_RUN");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_SCENARIOSFE_SCENARIO_X_PROCESS", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_x_process_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_group");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FEScenarioXProcess_FEProcessGroup");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FEScenarioXProcess_FEProcessGroup", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_group_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_group");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("group_order");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FEProcessGroup_FEProcess");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FEProcessGroup_FEProcess", 2);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_group");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_order");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("critical");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_SCENARIOSFE_MODEL_RUN", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("model_run_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_area_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hyetograph_id");
            Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_GRID_PROJECTSFE_SELECTION_SET_AREAS", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_area_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("project_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("area");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_area");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_SELECTION_SET_AREASFE_SELECTION_SETS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_SELECTION_SET_AREASFE_GRID_MODEL_RUN");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand7 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_SELECTION_SET_AREASFE_SELECTION_SETS", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_area_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("col_name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("row_name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("percent_overlap");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand8 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_SELECTION_SET_AREASFE_GRID_MODEL_RUN", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("model_run_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_area_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hyetograph_id");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand9 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FEHyetographs", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hyetograph_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("time_step");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("time_step_units");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("source");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_HYETOGRAPHSFE_HYETOGRAPH_DATA");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_HYDROGRAPHSFE_GRID_MODEL_RUN");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand10 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_HYETOGRAPHSFE_HYETOGRAPH_DATA", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hyetograph_data_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hyetograph_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("rainfall");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("run_order");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand11 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_HYDROGRAPHSFE_GRID_MODEL_RUN", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("model_run_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_area_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hyetograph_id");
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand12 = new Infragistics.Win.UltraWinGrid.UltraGridBand("GridModelRuns", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SelectionSetAreaID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HyetographID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Area");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SubArea");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimePeriod", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ProjectDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HyetographDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InstreamFacilities");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PollutantLoadingDB");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PollutantLoadingTable");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn75 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BMPEffectivenessDB");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn76 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BMPEffectivenessTable");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn77 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("GridModelTimeSteps");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn78 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("GridProcessGroups");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand13 = new Infragistics.Win.UltraWinGrid.UltraGridBand("GridModelTimeSteps", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn79 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rainfall");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn80 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RunOrder");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn81 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand14 = new Infragistics.Win.UltraWinGrid.UltraGridBand("GridProcessGroups", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn82 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("GroupName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn83 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn84 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("GroupOrder");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn85 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("GridProcesses");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand15 = new Infragistics.Win.UltraWinGrid.UltraGridBand("GridProcesses", 2);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn86 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ProcessName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn87 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Critical");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn88 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ProcessOrder");
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand16 = new Infragistics.Win.UltraWinGrid.UltraGridBand("GridModelResults", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn89 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Area");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn90 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SubArea");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn91 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeStepDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn92 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn93 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ResultFile");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn94 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PollutantLoads");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand17 = new Infragistics.Win.UltraWinGrid.UltraGridBand("PollutantLoads", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn95 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn96 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Load");
            Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand18 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FEGridProjects_QryModelRun", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn97 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("time_period");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn98 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn99 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn100 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_area_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn101 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hyetograph_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn102 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hyetograph_description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn103 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("project_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn104 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("model_run_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn105 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("area");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn106 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_area");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn107 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("include_instream_facilities");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn108 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("project_description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn109 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pollutant_loading_db");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn110 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pollutant_loading_table");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn111 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bmp_effectiveness_db");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn112 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bmp_effectiveness_table");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn113 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_GRID_PROJECTSFE_SCENARIOS1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn114 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_GRID_PROJECTSFE_SELECTION_SET_AREAS1");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand19 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_GRID_PROJECTSFE_SCENARIOS1", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn115 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn116 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("project_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn117 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("time_period");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn118 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn119 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("include_instream_facilities");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn120 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pollutant_loading_db");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn121 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pollutant_loading_table");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn122 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bmp_effectiveness_db");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn123 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bmp_effectiveness_table");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn124 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_SCENARIOSFE_SCENARIO_X_PROCESS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn125 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_SCENARIOSFE_MODEL_RUN");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand20 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_SCENARIOSFE_SCENARIO_X_PROCESS", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn126 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_x_process_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn127 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn128 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_group");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn129 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FEScenarioXProcess_FEProcessGroup");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand21 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FEScenarioXProcess_FEProcessGroup", 2);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn130 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_group_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn131 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_group");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn132 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn133 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("group_order");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn134 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FEProcessGroup_FEProcess");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand22 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FEProcessGroup_FEProcess", 3);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn135 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn136 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_group");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn137 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_order");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn138 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn139 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("critical");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand23 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_SCENARIOSFE_MODEL_RUN", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn140 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("model_run_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn141 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn142 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_area_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn143 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hyetograph_id");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand24 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_GRID_PROJECTSFE_SELECTION_SET_AREAS1", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn144 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_area_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn145 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("project_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn146 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("area");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn147 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_area");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn148 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_SELECTION_SET_AREASFE_SELECTION_SETS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn149 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FE_SELECTION_SET_AREASFE_GRID_MODEL_RUN");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand25 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_SELECTION_SET_AREASFE_SELECTION_SETS", 6);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn150 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn151 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_area_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn152 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn153 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("col_name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn154 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("row_name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn155 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("percent_overlap");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand26 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FE_SELECTION_SET_AREASFE_GRID_MODEL_RUN", 6);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn156 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("model_run_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn157 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("scenario_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn158 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("selection_set_area_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn159 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("hyetograph_id");
            Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
            this.txtGridPath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.feGridProjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridInterfaceDS = new SystemsAnalysis.Grid.GridAnalysis.GridInterfaceDataSet();
            this.txtPRFPath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtMIPPath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtOSFPath = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblGridPath = new Infragistics.Win.Misc.UltraLabel();
            this.lblPRFPath = new Infragistics.Win.Misc.UltraLabel();
            this.lblMIPPath = new Infragistics.Win.Misc.UltraLabel();
            this.lblOSFPath = new Infragistics.Win.Misc.UltraLabel();
            this.lblProjectList = new Infragistics.Win.Misc.UltraLabel();
            this.lblSelectionSetAreas = new Infragistics.Win.Misc.UltraLabel();
            this.lblModelRuns = new Infragistics.Win.Misc.UltraLabel();
            this.lblHyetographs = new Infragistics.Win.Misc.UltraLabel();
            this.btnDefineModelRun = new Infragistics.Win.Misc.UltraButton();
            this.btnCommitHyetographChanges = new Infragistics.Win.Misc.UltraButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDefineScenarios = new System.Windows.Forms.TabPage();
            this.txtScenarioID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.feScenariosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblScenarioID = new Infragistics.Win.Misc.UltraLabel();
            this.label5 = new Infragistics.Win.Misc.UltraLabel();
            this.btnDeleteScenario = new Infragistics.Win.Misc.UltraButton();
            this.txtNewScenarioName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.label3 = new Infragistics.Win.Misc.UltraLabel();
            this.label4 = new Infragistics.Win.Misc.UltraLabel();
            this.btnAddNewScenario = new Infragistics.Win.Misc.UltraButton();
            this.chkInstreamFacilities = new System.Windows.Forms.CheckBox();
            this.label2 = new Infragistics.Win.Misc.UltraLabel();
            this.txtTimePeriod = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblTimePeriod = new Infragistics.Win.Misc.UltraLabel();
            this.lstDefineScenario = new System.Windows.Forms.ListBox();
            this.lblTableName = new Infragistics.Win.Misc.UltraLabel();
            this.txtPollutantLoadingTable = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtBMPEffectivenessTable = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblDBPath = new Infragistics.Win.Misc.UltraLabel();
            this.txtPollutantLoadingDB = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblBMPEffectiveness = new Infragistics.Win.Misc.UltraLabel();
            this.txtBMPEffectivenessDB = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblPollutantLoads = new Infragistics.Win.Misc.UltraLabel();
            this.btnUpdateSelectedProcesses = new Infragistics.Win.Misc.UltraButton();
            this.lblModelScenarios = new Infragistics.Win.Misc.UltraLabel();
            this.btnCommitScenarioChanges = new Infragistics.Win.Misc.UltraButton();
            this.tabPageDefineSelectionSets = new System.Windows.Forms.TabPage();
            this.textBox1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.tabPageDefineModelRuns = new System.Windows.Forms.TabPage();
            this.lblSelectScenario = new Infragistics.Win.Misc.UltraLabel();
            this.grdSelectScenario = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.grdSelectionSetAreas = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.feSelectionSetAreasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grdHyetographs = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.tabPageExecuteGrid = new System.Windows.Forms.TabPage();
            this.label1 = new Infragistics.Win.Misc.UltraLabel();
            this.pnlCancelBackgroundWorker = new System.Windows.Forms.Panel();
            this.lblModelRunStatus = new Infragistics.Win.Misc.UltraLabel();
            this.btnCancel = new Infragistics.Win.Misc.UltraButton();
            this.grdModelMetadata = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.gridModelRunsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridModelEngineBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnImportModelMetadata = new Infragistics.Win.Misc.UltraButton();
            this.btnExportModelMetadata = new Infragistics.Win.Misc.UltraButton();
            this.btnClearModels = new Infragistics.Win.Misc.UltraButton();
            this.btnExecuteModels = new Infragistics.Win.Misc.UltraButton();
            this.btnLoadModels = new Infragistics.Win.Misc.UltraButton();
            this.tabPageReviewResults = new System.Windows.Forms.TabPage();
            this.btnExportModelResults = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.txtUserName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.gridModelOutputBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblUserName = new Infragistics.Win.Misc.UltraLabel();
            this.txtRunDate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblRunDate = new Infragistics.Win.Misc.UltraLabel();
            this.btnImportModelResults = new Infragistics.Win.Misc.UltraButton();
            this.grdModelResults = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.gridModelResultsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageDefineProjects = new System.Windows.Forms.TabPage();
            this.ultraButtonArchiveInputOutput = new Infragistics.Win.Misc.UltraButton();
            this.cboDatabases = new System.Windows.Forms.ComboBox();
            this.labelArchiveDatabase = new System.Windows.Forms.Label();
            this.cboServers = new System.Windows.Forms.ComboBox();
            this.labelArchiveServer = new System.Windows.Forms.Label();
            this.txtOutputDirectory = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnCommitProjectChanges = new Infragistics.Win.Misc.UltraButton();
            this.lblNewProjectName = new Infragistics.Win.Misc.UltraLabel();
            this.btnDeleteProject = new Infragistics.Win.Misc.UltraButton();
            this.txtNewProjectName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnAddNewProject = new Infragistics.Win.Misc.UltraButton();
            this.lstDefineProject = new System.Windows.Forms.ListBox();
            this.lblOutputDirectory = new Infragistics.Win.Misc.UltraLabel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnDeleteStoredModelRuns = new Infragistics.Win.Misc.UltraButton();
            this.grdModelRuns = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.qryModelRunBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlTop = new System.Windows.Forms.Panel();
            this.txtProjectID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblProjectID = new Infragistics.Win.Misc.UltraLabel();
            this.cmbSelectedProject = new System.Windows.Forms.ComboBox();
            this.lblProject = new Infragistics.Win.Misc.UltraLabel();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseToApplicationFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.txtGridPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.feGridProjectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInterfaceDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPRFPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMIPPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOSFPath)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageDefineScenarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScenarioID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.feScenariosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewScenarioName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPollutantLoadingTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBMPEffectivenessTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPollutantLoadingDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBMPEffectivenessDB)).BeginInit();
            this.tabPageDefineSelectionSets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            this.tabPageDefineModelRuns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSelectScenario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSelectionSetAreas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.feSelectionSetAreasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdHyetographs)).BeginInit();
            this.tabPageExecuteGrid.SuspendLayout();
            this.pnlCancelBackgroundWorker.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdModelMetadata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridModelRunsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridModelEngineBindingSource)).BeginInit();
            this.tabPageReviewResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridModelOutputBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRunDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdModelResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridModelResultsBindingSource)).BeginInit();
            this.tabPageDefineProjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutputDirectory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewProjectName)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdModelRuns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryModelRunBindingSource)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectID)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtGridPath
            // 
            this.txtGridPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feGridProjectsBindingSource, "grid_path", true));
            this.txtGridPath.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtGridPath.Location = new System.Drawing.Point(368, 25);
            this.txtGridPath.Name = "txtGridPath";
            this.txtGridPath.Size = new System.Drawing.Size(509, 21);
            this.txtGridPath.TabIndex = 4;
            // 
            // feGridProjectsBindingSource
            // 
            this.feGridProjectsBindingSource.DataMember = "FEGridProjects";
            this.feGridProjectsBindingSource.DataSource = this.gridInterfaceDS;
            // 
            // gridInterfaceDS
            // 
            this.gridInterfaceDS.DataSetName = "GridInterfaceDataSet";
            this.gridInterfaceDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtPRFPath
            // 
            this.txtPRFPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feGridProjectsBindingSource, "bmp_path", true));
            this.txtPRFPath.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtPRFPath.Location = new System.Drawing.Point(368, 53);
            this.txtPRFPath.Name = "txtPRFPath";
            this.txtPRFPath.Size = new System.Drawing.Size(509, 21);
            this.txtPRFPath.TabIndex = 5;
            // 
            // txtMIPPath
            // 
            this.txtMIPPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feGridProjectsBindingSource, "mip_path", true));
            this.txtMIPPath.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtMIPPath.Location = new System.Drawing.Point(369, 81);
            this.txtMIPPath.Name = "txtMIPPath";
            this.txtMIPPath.Size = new System.Drawing.Size(508, 21);
            this.txtMIPPath.TabIndex = 6;
            // 
            // txtOSFPath
            // 
            this.txtOSFPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feGridProjectsBindingSource, "osf_path", true));
            this.txtOSFPath.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtOSFPath.Location = new System.Drawing.Point(368, 109);
            this.txtOSFPath.Name = "txtOSFPath";
            this.txtOSFPath.Size = new System.Drawing.Size(509, 21);
            this.txtOSFPath.TabIndex = 7;
            // 
            // lblGridPath
            // 
            this.lblGridPath.AutoSize = true;
            this.lblGridPath.Location = new System.Drawing.Point(278, 29);
            this.lblGridPath.Name = "lblGridPath";
            this.lblGridPath.Size = new System.Drawing.Size(52, 14);
            this.lblGridPath.TabIndex = 8;
            this.lblGridPath.Text = "Grid Path";
            // 
            // lblPRFPath
            // 
            this.lblPRFPath.AutoSize = true;
            this.lblPRFPath.Location = new System.Drawing.Point(278, 56);
            this.lblPRFPath.Name = "lblPRFPath";
            this.lblPRFPath.Size = new System.Drawing.Size(53, 14);
            this.lblPRFPath.TabIndex = 9;
            this.lblPRFPath.Text = "PRF Path";
            // 
            // lblMIPPath
            // 
            this.lblMIPPath.AutoSize = true;
            this.lblMIPPath.Location = new System.Drawing.Point(278, 84);
            this.lblMIPPath.Name = "lblMIPPath";
            this.lblMIPPath.Size = new System.Drawing.Size(51, 14);
            this.lblMIPPath.TabIndex = 10;
            this.lblMIPPath.Text = "MIP Path";
            // 
            // lblOSFPath
            // 
            this.lblOSFPath.AutoSize = true;
            this.lblOSFPath.Location = new System.Drawing.Point(278, 112);
            this.lblOSFPath.Name = "lblOSFPath";
            this.lblOSFPath.Size = new System.Drawing.Size(54, 14);
            this.lblOSFPath.TabIndex = 11;
            this.lblOSFPath.Text = "OSF Path";
            // 
            // lblProjectList
            // 
            this.lblProjectList.AutoSize = true;
            this.lblProjectList.Location = new System.Drawing.Point(7, 3);
            this.lblProjectList.Name = "lblProjectList";
            this.lblProjectList.Size = new System.Drawing.Size(39, 14);
            this.lblProjectList.TabIndex = 12;
            this.lblProjectList.Text = "Project";
            // 
            // lblSelectionSetAreas
            // 
            this.lblSelectionSetAreas.AutoSize = true;
            this.lblSelectionSetAreas.Location = new System.Drawing.Point(7, 3);
            this.lblSelectionSetAreas.Name = "lblSelectionSetAreas";
            this.lblSelectionSetAreas.Size = new System.Drawing.Size(104, 14);
            this.lblSelectionSetAreas.TabIndex = 14;
            this.lblSelectionSetAreas.Text = "Selection Set Areas";
            // 
            // lblModelRuns
            // 
            this.lblModelRuns.AutoSize = true;
            this.lblModelRuns.Location = new System.Drawing.Point(3, 2);
            this.lblModelRuns.Name = "lblModelRuns";
            this.lblModelRuns.Size = new System.Drawing.Size(101, 14);
            this.lblModelRuns.TabIndex = 16;
            this.lblModelRuns.Text = "Stored Model Runs";
            // 
            // lblHyetographs
            // 
            this.lblHyetographs.AutoSize = true;
            this.lblHyetographs.Location = new System.Drawing.Point(723, 8);
            this.lblHyetographs.Name = "lblHyetographs";
            this.lblHyetographs.Size = new System.Drawing.Size(68, 14);
            this.lblHyetographs.TabIndex = 21;
            this.lblHyetographs.Text = "Hyetographs";
            // 
            // btnDefineModelRun
            // 
            this.btnDefineModelRun.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnDefineModelRun.Location = new System.Drawing.Point(192, 242);
            this.btnDefineModelRun.Name = "btnDefineModelRun";
            this.btnDefineModelRun.Size = new System.Drawing.Size(525, 54);
            this.btnDefineModelRun.TabIndex = 2;
            this.btnDefineModelRun.Text = "Define Model Run Using Current Selection Set Area/Model Scenario/Hyetograph";
            this.btnDefineModelRun.Click += new System.EventHandler(this.btnDefineModelRun_Click);
            // 
            // btnCommitHyetographChanges
            // 
            this.btnCommitHyetographChanges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommitHyetographChanges.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnCommitHyetographChanges.Location = new System.Drawing.Point(723, 242);
            this.btnCommitHyetographChanges.Name = "btnCommitHyetographChanges";
            this.btnCommitHyetographChanges.Size = new System.Drawing.Size(311, 54);
            this.btnCommitHyetographChanges.TabIndex = 4;
            this.btnCommitHyetographChanges.Text = "Commit Hyetograph Changes";
            this.btnCommitHyetographChanges.Click += new System.EventHandler(this.btnCommitHyetographChanges_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPageDefineScenarios);
            this.tabControl1.Controls.Add(this.tabPageDefineSelectionSets);
            this.tabControl1.Controls.Add(this.tabPageDefineModelRuns);
            this.tabControl1.Controls.Add(this.tabPageExecuteGrid);
            this.tabControl1.Controls.Add(this.tabPageReviewResults);
            this.tabControl1.Controls.Add(this.tabPageDefineProjects);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1049, 410);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageDefineScenarios
            // 
            this.tabPageDefineScenarios.Controls.Add(this.txtScenarioID);
            this.tabPageDefineScenarios.Controls.Add(this.lblScenarioID);
            this.tabPageDefineScenarios.Controls.Add(this.label5);
            this.tabPageDefineScenarios.Controls.Add(this.btnDeleteScenario);
            this.tabPageDefineScenarios.Controls.Add(this.txtNewScenarioName);
            this.tabPageDefineScenarios.Controls.Add(this.label3);
            this.tabPageDefineScenarios.Controls.Add(this.label4);
            this.tabPageDefineScenarios.Controls.Add(this.btnAddNewScenario);
            this.tabPageDefineScenarios.Controls.Add(this.chkInstreamFacilities);
            this.tabPageDefineScenarios.Controls.Add(this.label2);
            this.tabPageDefineScenarios.Controls.Add(this.txtTimePeriod);
            this.tabPageDefineScenarios.Controls.Add(this.lblTimePeriod);
            this.tabPageDefineScenarios.Controls.Add(this.lstDefineScenario);
            this.tabPageDefineScenarios.Controls.Add(this.lblTableName);
            this.tabPageDefineScenarios.Controls.Add(this.txtPollutantLoadingTable);
            this.tabPageDefineScenarios.Controls.Add(this.txtBMPEffectivenessTable);
            this.tabPageDefineScenarios.Controls.Add(this.lblDBPath);
            this.tabPageDefineScenarios.Controls.Add(this.txtPollutantLoadingDB);
            this.tabPageDefineScenarios.Controls.Add(this.lblBMPEffectiveness);
            this.tabPageDefineScenarios.Controls.Add(this.txtBMPEffectivenessDB);
            this.tabPageDefineScenarios.Controls.Add(this.lblPollutantLoads);
            this.tabPageDefineScenarios.Controls.Add(this.btnUpdateSelectedProcesses);
            this.tabPageDefineScenarios.Controls.Add(this.lblModelScenarios);
            this.tabPageDefineScenarios.Controls.Add(this.btnCommitScenarioChanges);
            this.tabPageDefineScenarios.Location = new System.Drawing.Point(4, 25);
            this.tabPageDefineScenarios.Name = "tabPageDefineScenarios";
            this.tabPageDefineScenarios.Size = new System.Drawing.Size(1041, 381);
            this.tabPageDefineScenarios.TabIndex = 4;
            this.tabPageDefineScenarios.Text = "Define Scenarios";
            this.tabPageDefineScenarios.UseVisualStyleBackColor = true;
            // 
            // txtScenarioID
            // 
            this.txtScenarioID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feScenariosBindingSource, "scenario_id", true));
            this.txtScenarioID.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtScenarioID.Location = new System.Drawing.Point(589, 17);
            this.txtScenarioID.Name = "txtScenarioID";
            this.txtScenarioID.ReadOnly = true;
            this.txtScenarioID.Size = new System.Drawing.Size(117, 21);
            this.txtScenarioID.TabIndex = 56;
            // 
            // feScenariosBindingSource
            // 
            this.feScenariosBindingSource.DataMember = "FE_GRID_PROJECTSFE_SCENARIOS";
            this.feScenariosBindingSource.DataSource = this.feGridProjectsBindingSource;
            // 
            // lblScenarioID
            // 
            this.lblScenarioID.AutoSize = true;
            this.lblScenarioID.Location = new System.Drawing.Point(520, 20);
            this.lblScenarioID.Name = "lblScenarioID";
            this.lblScenarioID.Size = new System.Drawing.Size(63, 14);
            this.lblScenarioID.TabIndex = 57;
            this.lblScenarioID.Text = "Scenario ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 14);
            this.label5.TabIndex = 55;
            this.label5.Text = "New Scenario Name";
            // 
            // btnDeleteScenario
            // 
            this.btnDeleteScenario.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnDeleteScenario.Location = new System.Drawing.Point(99, 210);
            this.btnDeleteScenario.Name = "btnDeleteScenario";
            this.btnDeleteScenario.Size = new System.Drawing.Size(85, 35);
            this.btnDeleteScenario.TabIndex = 2;
            this.btnDeleteScenario.Text = "Delete Scenario";
            this.btnDeleteScenario.Click += new System.EventHandler(this.btnDeleteScenario_Click);
            // 
            // txtNewScenarioName
            // 
            this.txtNewScenarioName.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtNewScenarioName.Location = new System.Drawing.Point(7, 267);
            this.txtNewScenarioName.Name = "txtNewScenarioName";
            this.txtNewScenarioName.Size = new System.Drawing.Size(177, 21);
            this.txtNewScenarioName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 14);
            this.label3.TabIndex = 52;
            this.label3.Text = "Table Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(280, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 14);
            this.label4.TabIndex = 51;
            this.label4.Text = "Database Path";
            // 
            // btnAddNewScenario
            // 
            this.btnAddNewScenario.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnAddNewScenario.Location = new System.Drawing.Point(7, 210);
            this.btnAddNewScenario.Name = "btnAddNewScenario";
            this.btnAddNewScenario.Size = new System.Drawing.Size(85, 35);
            this.btnAddNewScenario.TabIndex = 1;
            this.btnAddNewScenario.Text = "Add New Scenario";
            this.btnAddNewScenario.Click += new System.EventHandler(this.btnAddNewScenario_Click);
            // 
            // chkInstreamFacilities
            // 
            this.chkInstreamFacilities.AutoSize = true;
            this.chkInstreamFacilities.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.feScenariosBindingSource, "include_instream_facilities", true));
            this.chkInstreamFacilities.Location = new System.Drawing.Point(436, 42);
            this.chkInstreamFacilities.Name = "chkInstreamFacilities";
            this.chkInstreamFacilities.Size = new System.Drawing.Size(15, 14);
            this.chkInstreamFacilities.TabIndex = 5;
            this.chkInstreamFacilities.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 14);
            this.label2.TabIndex = 48;
            this.label2.Text = "Instream Facilities?";
            // 
            // txtTimePeriod
            // 
            this.txtTimePeriod.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feScenariosBindingSource, "time_period", true));
            this.txtTimePeriod.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtTimePeriod.Location = new System.Drawing.Point(386, 17);
            this.txtTimePeriod.Name = "txtTimePeriod";
            this.txtTimePeriod.Size = new System.Drawing.Size(117, 21);
            this.txtTimePeriod.TabIndex = 4;
            // 
            // lblTimePeriod
            // 
            this.lblTimePeriod.AutoSize = true;
            this.lblTimePeriod.Location = new System.Drawing.Point(280, 20);
            this.lblTimePeriod.Name = "lblTimePeriod";
            this.lblTimePeriod.Size = new System.Drawing.Size(65, 14);
            this.lblTimePeriod.TabIndex = 46;
            this.lblTimePeriod.Text = "Time Period";
            // 
            // lstDefineScenario
            // 
            this.lstDefineScenario.DataSource = this.feScenariosBindingSource;
            this.lstDefineScenario.DisplayMember = "description";
            this.lstDefineScenario.FormattingEnabled = true;
            this.lstDefineScenario.Location = new System.Drawing.Point(7, 20);
            this.lstDefineScenario.Name = "lstDefineScenario";
            this.lstDefineScenario.Size = new System.Drawing.Size(271, 186);
            this.lstDefineScenario.TabIndex = 0;
            this.lstDefineScenario.ValueMember = "scenario_id";
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(280, 112);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(66, 14);
            this.lblTableName.TabIndex = 43;
            this.lblTableName.Text = "Table Name";
            // 
            // txtPollutantLoadingTable
            // 
            this.txtPollutantLoadingTable.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feScenariosBindingSource, "pollutant_loading_table", true));
            this.txtPollutantLoadingTable.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtPollutantLoadingTable.Location = new System.Drawing.Point(386, 109);
            this.txtPollutantLoadingTable.Name = "txtPollutantLoadingTable";
            this.txtPollutantLoadingTable.Size = new System.Drawing.Size(384, 21);
            this.txtPollutantLoadingTable.TabIndex = 7;
            // 
            // txtBMPEffectivenessTable
            // 
            this.txtBMPEffectivenessTable.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feScenariosBindingSource, "bmp_effectiveness_table", true));
            this.txtBMPEffectivenessTable.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtBMPEffectivenessTable.Location = new System.Drawing.Point(386, 178);
            this.txtBMPEffectivenessTable.Name = "txtBMPEffectivenessTable";
            this.txtBMPEffectivenessTable.Size = new System.Drawing.Size(384, 21);
            this.txtBMPEffectivenessTable.TabIndex = 9;
            // 
            // lblDBPath
            // 
            this.lblDBPath.AutoSize = true;
            this.lblDBPath.Location = new System.Drawing.Point(280, 89);
            this.lblDBPath.Name = "lblDBPath";
            this.lblDBPath.Size = new System.Drawing.Size(79, 14);
            this.lblDBPath.TabIndex = 40;
            this.lblDBPath.Text = "Database Path";
            // 
            // txtPollutantLoadingDB
            // 
            this.txtPollutantLoadingDB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feScenariosBindingSource, "pollutant_loading_db", true));
            this.txtPollutantLoadingDB.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtPollutantLoadingDB.Location = new System.Drawing.Point(386, 86);
            this.txtPollutantLoadingDB.Name = "txtPollutantLoadingDB";
            this.txtPollutantLoadingDB.Size = new System.Drawing.Size(564, 21);
            this.txtPollutantLoadingDB.TabIndex = 6;
            // 
            // lblBMPEffectiveness
            // 
            this.lblBMPEffectiveness.AutoSize = true;
            this.lblBMPEffectiveness.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBMPEffectiveness.Location = new System.Drawing.Point(280, 135);
            this.lblBMPEffectiveness.Name = "lblBMPEffectiveness";
            this.lblBMPEffectiveness.Size = new System.Drawing.Size(103, 14);
            this.lblBMPEffectiveness.TabIndex = 39;
            this.lblBMPEffectiveness.Text = "BMP Effectiveness";
            // 
            // txtBMPEffectivenessDB
            // 
            this.txtBMPEffectivenessDB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feScenariosBindingSource, "bmp_effectiveness_db", true));
            this.txtBMPEffectivenessDB.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtBMPEffectivenessDB.Location = new System.Drawing.Point(386, 155);
            this.txtBMPEffectivenessDB.Name = "txtBMPEffectivenessDB";
            this.txtBMPEffectivenessDB.Size = new System.Drawing.Size(564, 21);
            this.txtBMPEffectivenessDB.TabIndex = 8;
            // 
            // lblPollutantLoads
            // 
            this.lblPollutantLoads.AutoSize = true;
            this.lblPollutantLoads.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPollutantLoads.Location = new System.Drawing.Point(280, 66);
            this.lblPollutantLoads.Name = "lblPollutantLoads";
            this.lblPollutantLoads.Size = new System.Drawing.Size(96, 14);
            this.lblPollutantLoads.TabIndex = 38;
            this.lblPollutantLoads.Text = "Pollutant Loading";
            // 
            // btnUpdateSelectedProcesses
            // 
            this.btnUpdateSelectedProcesses.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnUpdateSelectedProcesses.Location = new System.Drawing.Point(601, 210);
            this.btnUpdateSelectedProcesses.Name = "btnUpdateSelectedProcesses";
            this.btnUpdateSelectedProcesses.Size = new System.Drawing.Size(311, 35);
            this.btnUpdateSelectedProcesses.TabIndex = 11;
            this.btnUpdateSelectedProcesses.Text = "Update Scenario Processes";
            this.btnUpdateSelectedProcesses.Click += new System.EventHandler(this.btnUpdateSelectedProcesses_Click);
            // 
            // lblModelScenarios
            // 
            this.lblModelScenarios.AutoSize = true;
            this.lblModelScenarios.Location = new System.Drawing.Point(7, 3);
            this.lblModelScenarios.Name = "lblModelScenarios";
            this.lblModelScenarios.Size = new System.Drawing.Size(89, 14);
            this.lblModelScenarios.TabIndex = 32;
            this.lblModelScenarios.Text = "Model Scenarios";
            // 
            // btnCommitScenarioChanges
            // 
            this.btnCommitScenarioChanges.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnCommitScenarioChanges.Location = new System.Drawing.Point(282, 210);
            this.btnCommitScenarioChanges.Name = "btnCommitScenarioChanges";
            this.btnCommitScenarioChanges.Size = new System.Drawing.Size(313, 35);
            this.btnCommitScenarioChanges.TabIndex = 10;
            this.btnCommitScenarioChanges.Text = "Commit Changes to Model Scenarios";
            this.btnCommitScenarioChanges.Click += new System.EventHandler(this.btnCommitScenarioChanges_Click);
            // 
            // tabPageDefineSelectionSets
            // 
            this.tabPageDefineSelectionSets.Controls.Add(this.textBox1);
            this.tabPageDefineSelectionSets.Location = new System.Drawing.Point(4, 25);
            this.tabPageDefineSelectionSets.Name = "tabPageDefineSelectionSets";
            this.tabPageDefineSelectionSets.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDefineSelectionSets.Size = new System.Drawing.Size(1041, 381);
            this.tabPageDefineSelectionSets.TabIndex = 5;
            this.tabPageDefineSelectionSets.Text = "Define Selection Sets";
            this.tabPageDefineSelectionSets.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(462, 220);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // tabPageDefineModelRuns
            // 
            this.tabPageDefineModelRuns.Controls.Add(this.lblSelectScenario);
            this.tabPageDefineModelRuns.Controls.Add(this.btnDefineModelRun);
            this.tabPageDefineModelRuns.Controls.Add(this.lblSelectionSetAreas);
            this.tabPageDefineModelRuns.Controls.Add(this.btnCommitHyetographChanges);
            this.tabPageDefineModelRuns.Controls.Add(this.lblHyetographs);
            this.tabPageDefineModelRuns.Controls.Add(this.grdSelectScenario);
            this.tabPageDefineModelRuns.Controls.Add(this.grdSelectionSetAreas);
            this.tabPageDefineModelRuns.Controls.Add(this.grdHyetographs);
            this.tabPageDefineModelRuns.Location = new System.Drawing.Point(4, 25);
            this.tabPageDefineModelRuns.Name = "tabPageDefineModelRuns";
            this.tabPageDefineModelRuns.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDefineModelRuns.Size = new System.Drawing.Size(1041, 381);
            this.tabPageDefineModelRuns.TabIndex = 0;
            this.tabPageDefineModelRuns.Text = "Define Model Runs";
            this.tabPageDefineModelRuns.UseVisualStyleBackColor = true;
            // 
            // lblSelectScenario
            // 
            this.lblSelectScenario.AutoSize = true;
            this.lblSelectScenario.Location = new System.Drawing.Point(192, 3);
            this.lblSelectScenario.Name = "lblSelectScenario";
            this.lblSelectScenario.Size = new System.Drawing.Size(89, 14);
            this.lblSelectScenario.TabIndex = 37;
            this.lblSelectScenario.Text = "Model Scenarios";
            // 
            // grdSelectScenario
            // 
            this.grdSelectScenario.DataSource = this.feScenariosBindingSource;
            appearance27.BackColor = System.Drawing.SystemColors.Window;
            appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grdSelectScenario.DisplayLayout.Appearance = appearance27;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn4.Header.VisiblePosition = 2;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11});
            ultraGridColumn12.Header.VisiblePosition = 0;
            ultraGridColumn13.Header.VisiblePosition = 1;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Header.VisiblePosition = 2;
            ultraGridColumn15.Header.VisiblePosition = 3;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15});
            ultraGridColumn16.Header.VisiblePosition = 0;
            ultraGridColumn17.Header.VisiblePosition = 1;
            ultraGridColumn18.Header.VisiblePosition = 2;
            ultraGridColumn19.Header.VisiblePosition = 3;
            ultraGridColumn20.Header.VisiblePosition = 4;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20});
            ultraGridColumn21.Header.VisiblePosition = 0;
            ultraGridColumn22.Header.VisiblePosition = 1;
            ultraGridColumn23.Header.VisiblePosition = 2;
            ultraGridColumn24.Header.VisiblePosition = 3;
            ultraGridColumn25.Header.VisiblePosition = 4;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn21,
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25});
            ultraGridColumn26.Header.VisiblePosition = 0;
            ultraGridColumn27.Header.VisiblePosition = 1;
            ultraGridColumn28.Header.VisiblePosition = 2;
            ultraGridColumn29.Header.VisiblePosition = 3;
            ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29});
            ultraGridBand5.Hidden = true;
            this.grdSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.grdSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.grdSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.grdSelectScenario.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdSelectScenario.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance42.BorderColor = System.Drawing.SystemColors.Window;
            this.grdSelectScenario.DisplayLayout.GroupByBox.Appearance = appearance42;
            appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdSelectScenario.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
            this.grdSelectScenario.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance44.BackColor2 = System.Drawing.SystemColors.Control;
            appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdSelectScenario.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
            this.grdSelectScenario.DisplayLayout.MaxColScrollRegions = 1;
            this.grdSelectScenario.DisplayLayout.MaxRowScrollRegions = 1;
            appearance45.BackColor = System.Drawing.SystemColors.Window;
            appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdSelectScenario.DisplayLayout.Override.ActiveCellAppearance = appearance45;
            appearance46.BackColor = System.Drawing.SystemColors.Highlight;
            appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdSelectScenario.DisplayLayout.Override.ActiveRowAppearance = appearance46;
            this.grdSelectScenario.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdSelectScenario.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdSelectScenario.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.grdSelectScenario.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdSelectScenario.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance47.BackColor = System.Drawing.SystemColors.Window;
            this.grdSelectScenario.DisplayLayout.Override.CardAreaAppearance = appearance47;
            appearance48.BorderColor = System.Drawing.Color.Silver;
            appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grdSelectScenario.DisplayLayout.Override.CellAppearance = appearance48;
            this.grdSelectScenario.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdSelectScenario.DisplayLayout.Override.CellPadding = 0;
            appearance49.BackColor = System.Drawing.SystemColors.Control;
            appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance49.BorderColor = System.Drawing.SystemColors.Window;
            this.grdSelectScenario.DisplayLayout.Override.GroupByRowAppearance = appearance49;
            appearance74.TextHAlignAsString = "Left";
            this.grdSelectScenario.DisplayLayout.Override.HeaderAppearance = appearance74;
            this.grdSelectScenario.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdSelectScenario.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance75.BackColor = System.Drawing.SystemColors.Window;
            appearance75.BorderColor = System.Drawing.Color.Silver;
            this.grdSelectScenario.DisplayLayout.Override.RowAppearance = appearance75;
            this.grdSelectScenario.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdSelectScenario.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance76.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grdSelectScenario.DisplayLayout.Override.SelectedRowAppearance = appearance76;
            this.grdSelectScenario.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance77.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdSelectScenario.DisplayLayout.Override.TemplateAddRowAppearance = appearance77;
            this.grdSelectScenario.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSelectScenario.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSelectScenario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSelectScenario.Location = new System.Drawing.Point(192, 24);
            this.grdSelectScenario.Name = "grdSelectScenario";
            this.grdSelectScenario.Size = new System.Drawing.Size(525, 212);
            this.grdSelectScenario.TabIndex = 1;
            this.grdSelectScenario.Text = "ultraGrid1";
            // 
            // grdSelectionSetAreas
            // 
            this.grdSelectionSetAreas.DataSource = this.feSelectionSetAreasBindingSource;
            appearance28.BackColor = System.Drawing.SystemColors.Window;
            appearance28.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grdSelectionSetAreas.DisplayLayout.Appearance = appearance28;
            ultraGridColumn30.Header.VisiblePosition = 0;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.Header.VisiblePosition = 1;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.Header.VisiblePosition = 2;
            ultraGridColumn33.Header.VisiblePosition = 3;
            ultraGridColumn34.Header.VisiblePosition = 4;
            ultraGridColumn35.Header.VisiblePosition = 5;
            ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35});
            ultraGridBand6.Expandable = false;
            ultraGridColumn36.Header.VisiblePosition = 0;
            ultraGridColumn37.Header.VisiblePosition = 1;
            ultraGridColumn38.Header.VisiblePosition = 2;
            ultraGridColumn39.Header.VisiblePosition = 3;
            ultraGridColumn40.Header.VisiblePosition = 4;
            ultraGridColumn41.Header.VisiblePosition = 5;
            ultraGridBand7.Columns.AddRange(new object[] {
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41});
            ultraGridColumn42.Header.VisiblePosition = 0;
            ultraGridColumn43.Header.VisiblePosition = 2;
            ultraGridColumn44.Header.VisiblePosition = 1;
            ultraGridColumn45.Header.VisiblePosition = 3;
            ultraGridBand8.Columns.AddRange(new object[] {
            ultraGridColumn42,
            ultraGridColumn43,
            ultraGridColumn44,
            ultraGridColumn45});
            this.grdSelectionSetAreas.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            this.grdSelectionSetAreas.DisplayLayout.BandsSerializer.Add(ultraGridBand7);
            this.grdSelectionSetAreas.DisplayLayout.BandsSerializer.Add(ultraGridBand8);
            this.grdSelectionSetAreas.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdSelectionSetAreas.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.grdSelectionSetAreas.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdSelectionSetAreas.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.grdSelectionSetAreas.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdSelectionSetAreas.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.grdSelectionSetAreas.DisplayLayout.MaxColScrollRegions = 1;
            this.grdSelectionSetAreas.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdSelectionSetAreas.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdSelectionSetAreas.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.grdSelectionSetAreas.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdSelectionSetAreas.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.grdSelectionSetAreas.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grdSelectionSetAreas.DisplayLayout.Override.CellAppearance = appearance8;
            this.grdSelectionSetAreas.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdSelectionSetAreas.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.grdSelectionSetAreas.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.grdSelectionSetAreas.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.grdSelectionSetAreas.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdSelectionSetAreas.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.grdSelectionSetAreas.DisplayLayout.Override.RowAppearance = appearance11;
            this.grdSelectionSetAreas.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdSelectionSetAreas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grdSelectionSetAreas.DisplayLayout.Override.SelectedRowAppearance = appearance25;
            this.grdSelectionSetAreas.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdSelectionSetAreas.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.grdSelectionSetAreas.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSelectionSetAreas.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSelectionSetAreas.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdSelectionSetAreas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSelectionSetAreas.Location = new System.Drawing.Point(3, 24);
            this.grdSelectionSetAreas.Name = "grdSelectionSetAreas";
            this.grdSelectionSetAreas.Size = new System.Drawing.Size(183, 272);
            this.grdSelectionSetAreas.TabIndex = 0;
            this.grdSelectionSetAreas.Text = "grdSelectionSetAreas";
            // 
            // feSelectionSetAreasBindingSource
            // 
            this.feSelectionSetAreasBindingSource.DataMember = "FE_GRID_PROJECTSFE_SELECTION_SET_AREAS";
            this.feSelectionSetAreasBindingSource.DataSource = this.feGridProjectsBindingSource;
            // 
            // grdHyetographs
            // 
            this.grdHyetographs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdHyetographs.DataMember = "FEHyetographs";
            this.grdHyetographs.DataSource = this.gridInterfaceDS;
            appearance26.BackColor = System.Drawing.SystemColors.Window;
            appearance26.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grdHyetographs.DisplayLayout.Appearance = appearance26;
            ultraGridColumn46.Header.VisiblePosition = 0;
            ultraGridColumn47.Header.VisiblePosition = 1;
            ultraGridColumn48.Header.VisiblePosition = 2;
            ultraGridColumn49.Header.VisiblePosition = 3;
            ultraGridColumn50.Header.VisiblePosition = 4;
            ultraGridColumn51.Header.VisiblePosition = 5;
            ultraGridColumn52.Header.VisiblePosition = 6;
            ultraGridBand9.Columns.AddRange(new object[] {
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49,
            ultraGridColumn50,
            ultraGridColumn51,
            ultraGridColumn52});
            ultraGridColumn53.Header.VisiblePosition = 0;
            ultraGridColumn54.Header.VisiblePosition = 1;
            ultraGridColumn55.Header.VisiblePosition = 2;
            ultraGridColumn56.Header.VisiblePosition = 3;
            ultraGridColumn57.Header.VisiblePosition = 4;
            ultraGridBand10.Columns.AddRange(new object[] {
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56,
            ultraGridColumn57});
            ultraGridColumn58.Header.VisiblePosition = 0;
            ultraGridColumn59.Header.VisiblePosition = 1;
            ultraGridColumn60.Header.VisiblePosition = 2;
            ultraGridColumn61.Header.VisiblePosition = 3;
            ultraGridBand11.Columns.AddRange(new object[] {
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn60,
            ultraGridColumn61});
            this.grdHyetographs.DisplayLayout.BandsSerializer.Add(ultraGridBand9);
            this.grdHyetographs.DisplayLayout.BandsSerializer.Add(ultraGridBand10);
            this.grdHyetographs.DisplayLayout.BandsSerializer.Add(ultraGridBand11);
            this.grdHyetographs.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdHyetographs.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance50.BorderColor = System.Drawing.SystemColors.Window;
            this.grdHyetographs.DisplayLayout.GroupByBox.Appearance = appearance50;
            appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdHyetographs.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
            this.grdHyetographs.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance52.BackColor2 = System.Drawing.SystemColors.Control;
            appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdHyetographs.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
            this.grdHyetographs.DisplayLayout.MaxColScrollRegions = 1;
            this.grdHyetographs.DisplayLayout.MaxRowScrollRegions = 1;
            appearance53.BackColor = System.Drawing.SystemColors.Window;
            appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdHyetographs.DisplayLayout.Override.ActiveCellAppearance = appearance53;
            appearance54.BackColor = System.Drawing.SystemColors.Highlight;
            appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdHyetographs.DisplayLayout.Override.ActiveRowAppearance = appearance54;
            this.grdHyetographs.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
            this.grdHyetographs.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.True;
            this.grdHyetographs.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.grdHyetographs.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdHyetographs.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance55.BackColor = System.Drawing.SystemColors.Window;
            this.grdHyetographs.DisplayLayout.Override.CardAreaAppearance = appearance55;
            appearance56.BorderColor = System.Drawing.Color.Silver;
            appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grdHyetographs.DisplayLayout.Override.CellAppearance = appearance56;
            this.grdHyetographs.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdHyetographs.DisplayLayout.Override.CellPadding = 0;
            appearance57.BackColor = System.Drawing.SystemColors.Control;
            appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance57.BorderColor = System.Drawing.SystemColors.Window;
            this.grdHyetographs.DisplayLayout.Override.GroupByRowAppearance = appearance57;
            appearance58.TextHAlignAsString = "Left";
            this.grdHyetographs.DisplayLayout.Override.HeaderAppearance = appearance58;
            this.grdHyetographs.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdHyetographs.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance59.BackColor = System.Drawing.SystemColors.Window;
            appearance59.BorderColor = System.Drawing.Color.Silver;
            this.grdHyetographs.DisplayLayout.Override.RowAppearance = appearance59;
            this.grdHyetographs.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdHyetographs.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.grdHyetographs.DisplayLayout.Override.SelectedRowAppearance = appearance60;
            this.grdHyetographs.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance73.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdHyetographs.DisplayLayout.Override.TemplateAddRowAppearance = appearance73;
            this.grdHyetographs.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdHyetographs.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdHyetographs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdHyetographs.Location = new System.Drawing.Point(723, 24);
            this.grdHyetographs.Name = "grdHyetographs";
            this.grdHyetographs.Size = new System.Drawing.Size(311, 212);
            this.grdHyetographs.TabIndex = 3;
            this.grdHyetographs.Text = "ultraGrid1";
            // 
            // tabPageExecuteGrid
            // 
            this.tabPageExecuteGrid.Controls.Add(this.label1);
            this.tabPageExecuteGrid.Controls.Add(this.pnlCancelBackgroundWorker);
            this.tabPageExecuteGrid.Controls.Add(this.grdModelMetadata);
            this.tabPageExecuteGrid.Controls.Add(this.btnImportModelMetadata);
            this.tabPageExecuteGrid.Controls.Add(this.btnExportModelMetadata);
            this.tabPageExecuteGrid.Controls.Add(this.btnClearModels);
            this.tabPageExecuteGrid.Controls.Add(this.btnExecuteModels);
            this.tabPageExecuteGrid.Controls.Add(this.btnLoadModels);
            this.tabPageExecuteGrid.Location = new System.Drawing.Point(4, 25);
            this.tabPageExecuteGrid.Name = "tabPageExecuteGrid";
            this.tabPageExecuteGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExecuteGrid.Size = new System.Drawing.Size(1041, 381);
            this.tabPageExecuteGrid.TabIndex = 1;
            this.tabPageExecuteGrid.Text = "Execute GRID Model";
            this.tabPageExecuteGrid.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 14);
            this.label1.TabIndex = 34;
            this.label1.Text = "Model Runs";
            // 
            // pnlCancelBackgroundWorker
            // 
            this.pnlCancelBackgroundWorker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCancelBackgroundWorker.Controls.Add(this.lblModelRunStatus);
            this.pnlCancelBackgroundWorker.Controls.Add(this.btnCancel);
            this.pnlCancelBackgroundWorker.Location = new System.Drawing.Point(270, 50);
            this.pnlCancelBackgroundWorker.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCancelBackgroundWorker.Name = "pnlCancelBackgroundWorker";
            this.pnlCancelBackgroundWorker.Size = new System.Drawing.Size(511, 147);
            this.pnlCancelBackgroundWorker.TabIndex = 23;
            this.pnlCancelBackgroundWorker.Visible = false;
            // 
            // lblModelRunStatus
            // 
            this.lblModelRunStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.TextHAlignAsString = "Center";
            appearance1.TextVAlignAsString = "Middle";
            this.lblModelRunStatus.Appearance = appearance1;
            this.lblModelRunStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblModelRunStatus.Location = new System.Drawing.Point(35, 11);
            this.lblModelRunStatus.Margin = new System.Windows.Forms.Padding(2);
            this.lblModelRunStatus.MinimumSize = new System.Drawing.Size(137, 55);
            this.lblModelRunStatus.Name = "lblModelRunStatus";
            this.lblModelRunStatus.Size = new System.Drawing.Size(434, 79);
            this.lblModelRunStatus.TabIndex = 3;
            this.lblModelRunStatus.Text = "Generating Report";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(35, 94);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.MinimumSize = new System.Drawing.Size(137, 37);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(434, 37);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdModelMetadata
            // 
            this.grdModelMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdModelMetadata.DataSource = this.gridModelRunsBindingSource;
            appearance29.BackColor = System.Drawing.SystemColors.Window;
            appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grdModelMetadata.DisplayLayout.Appearance = appearance29;
            ultraGridColumn62.Header.VisiblePosition = 0;
            ultraGridColumn62.Hidden = true;
            ultraGridColumn63.Header.VisiblePosition = 1;
            ultraGridColumn63.Hidden = true;
            ultraGridColumn64.Header.VisiblePosition = 2;
            ultraGridColumn64.Hidden = true;
            ultraGridColumn65.Header.VisiblePosition = 3;
            ultraGridColumn65.Width = 174;
            ultraGridColumn66.Header.VisiblePosition = 4;
            ultraGridColumn66.Width = 98;
            ultraGridColumn67.Header.VisiblePosition = 5;
            ultraGridColumn67.Width = 98;
            ultraGridColumn68.Header.VisiblePosition = 6;
            ultraGridColumn69.Header.VisiblePosition = 8;
            ultraGridColumn70.Header.VisiblePosition = 9;
            ultraGridColumn71.Header.VisiblePosition = 7;
            ultraGridColumn72.Header.VisiblePosition = 10;
            ultraGridColumn73.Header.VisiblePosition = 11;
            ultraGridColumn73.Hidden = true;
            ultraGridColumn74.Header.VisiblePosition = 13;
            ultraGridColumn74.Hidden = true;
            ultraGridColumn75.Header.VisiblePosition = 15;
            ultraGridColumn75.Hidden = true;
            ultraGridColumn76.Header.VisiblePosition = 16;
            ultraGridColumn76.Hidden = true;
            ultraGridColumn77.Header.VisiblePosition = 12;
            ultraGridColumn78.Header.VisiblePosition = 14;
            ultraGridBand12.Columns.AddRange(new object[] {
            ultraGridColumn62,
            ultraGridColumn63,
            ultraGridColumn64,
            ultraGridColumn65,
            ultraGridColumn66,
            ultraGridColumn67,
            ultraGridColumn68,
            ultraGridColumn69,
            ultraGridColumn70,
            ultraGridColumn71,
            ultraGridColumn72,
            ultraGridColumn73,
            ultraGridColumn74,
            ultraGridColumn75,
            ultraGridColumn76,
            ultraGridColumn77,
            ultraGridColumn78});
            ultraGridColumn79.Header.VisiblePosition = 0;
            ultraGridColumn80.Header.VisiblePosition = 1;
            ultraGridColumn81.Header.VisiblePosition = 2;
            ultraGridBand13.Columns.AddRange(new object[] {
            ultraGridColumn79,
            ultraGridColumn80,
            ultraGridColumn81});
            ultraGridColumn82.Header.VisiblePosition = 0;
            ultraGridColumn83.Header.VisiblePosition = 1;
            ultraGridColumn84.Header.VisiblePosition = 2;
            ultraGridColumn85.Header.VisiblePosition = 3;
            ultraGridBand14.Columns.AddRange(new object[] {
            ultraGridColumn82,
            ultraGridColumn83,
            ultraGridColumn84,
            ultraGridColumn85});
            ultraGridColumn86.Header.VisiblePosition = 0;
            ultraGridColumn87.Header.VisiblePosition = 1;
            ultraGridColumn88.Header.VisiblePosition = 2;
            ultraGridBand15.Columns.AddRange(new object[] {
            ultraGridColumn86,
            ultraGridColumn87,
            ultraGridColumn88});
            this.grdModelMetadata.DisplayLayout.BandsSerializer.Add(ultraGridBand12);
            this.grdModelMetadata.DisplayLayout.BandsSerializer.Add(ultraGridBand13);
            this.grdModelMetadata.DisplayLayout.BandsSerializer.Add(ultraGridBand14);
            this.grdModelMetadata.DisplayLayout.BandsSerializer.Add(ultraGridBand15);
            this.grdModelMetadata.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdModelMetadata.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance30.BorderColor = System.Drawing.SystemColors.Window;
            this.grdModelMetadata.DisplayLayout.GroupByBox.Appearance = appearance30;
            appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdModelMetadata.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
            this.grdModelMetadata.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance32.BackColor2 = System.Drawing.SystemColors.Control;
            appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdModelMetadata.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
            this.grdModelMetadata.DisplayLayout.MaxColScrollRegions = 1;
            this.grdModelMetadata.DisplayLayout.MaxRowScrollRegions = 1;
            appearance33.BackColor = System.Drawing.SystemColors.Window;
            appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdModelMetadata.DisplayLayout.Override.ActiveCellAppearance = appearance33;
            appearance34.BackColor = System.Drawing.SystemColors.Highlight;
            appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdModelMetadata.DisplayLayout.Override.ActiveRowAppearance = appearance34;
            this.grdModelMetadata.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdModelMetadata.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance35.BackColor = System.Drawing.SystemColors.Window;
            this.grdModelMetadata.DisplayLayout.Override.CardAreaAppearance = appearance35;
            appearance36.BorderColor = System.Drawing.Color.Silver;
            appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grdModelMetadata.DisplayLayout.Override.CellAppearance = appearance36;
            this.grdModelMetadata.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdModelMetadata.DisplayLayout.Override.CellPadding = 0;
            appearance37.BackColor = System.Drawing.SystemColors.Control;
            appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance37.BorderColor = System.Drawing.SystemColors.Window;
            this.grdModelMetadata.DisplayLayout.Override.GroupByRowAppearance = appearance37;
            appearance38.TextHAlignAsString = "Left";
            this.grdModelMetadata.DisplayLayout.Override.HeaderAppearance = appearance38;
            this.grdModelMetadata.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdModelMetadata.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance39.BackColor = System.Drawing.SystemColors.Window;
            appearance39.BorderColor = System.Drawing.Color.Silver;
            this.grdModelMetadata.DisplayLayout.Override.RowAppearance = appearance39;
            this.grdModelMetadata.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdModelMetadata.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdModelMetadata.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdModelMetadata.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
            this.grdModelMetadata.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdModelMetadata.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdModelMetadata.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grdModelMetadata.Location = new System.Drawing.Point(7, 26);
            this.grdModelMetadata.Name = "grdModelMetadata";
            this.grdModelMetadata.Size = new System.Drawing.Size(1027, 191);
            this.grdModelMetadata.TabIndex = 0;
            // 
            // gridModelRunsBindingSource
            // 
            this.gridModelRunsBindingSource.DataMember = "GridModelRuns";
            this.gridModelRunsBindingSource.DataSource = this.gridModelEngineBindingSource;
            // 
            // gridModelEngineBindingSource
            // 
            this.gridModelEngineBindingSource.DataSource = typeof(SystemsAnalysis.Grid.GridAnalysis.GridModelEngine);
            // 
            // btnImportModelMetadata
            // 
            this.btnImportModelMetadata.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnImportModelMetadata.Location = new System.Drawing.Point(293, 223);
            this.btnImportModelMetadata.Name = "btnImportModelMetadata";
            this.btnImportModelMetadata.Size = new System.Drawing.Size(137, 35);
            this.btnImportModelMetadata.TabIndex = 3;
            this.btnImportModelMetadata.Text = "Import Model Metadata";
            this.btnImportModelMetadata.Click += new System.EventHandler(this.btnImportModelMetadata_Click);
            // 
            // btnExportModelMetadata
            // 
            this.btnExportModelMetadata.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnExportModelMetadata.Location = new System.Drawing.Point(436, 223);
            this.btnExportModelMetadata.Name = "btnExportModelMetadata";
            this.btnExportModelMetadata.Size = new System.Drawing.Size(137, 35);
            this.btnExportModelMetadata.TabIndex = 4;
            this.btnExportModelMetadata.Text = "Export Model Metadata";
            this.btnExportModelMetadata.Click += new System.EventHandler(this.btnExportModelMetadata_Click);
            // 
            // btnClearModels
            // 
            this.btnClearModels.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnClearModels.Location = new System.Drawing.Point(150, 223);
            this.btnClearModels.Name = "btnClearModels";
            this.btnClearModels.Size = new System.Drawing.Size(137, 35);
            this.btnClearModels.TabIndex = 2;
            this.btnClearModels.Text = "Clear Models";
            this.btnClearModels.Click += new System.EventHandler(this.btnClearModels_Click);
            // 
            // btnExecuteModels
            // 
            this.btnExecuteModels.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnExecuteModels.Location = new System.Drawing.Point(579, 223);
            this.btnExecuteModels.Name = "btnExecuteModels";
            this.btnExecuteModels.Size = new System.Drawing.Size(280, 35);
            this.btnExecuteModels.TabIndex = 5;
            this.btnExecuteModels.Text = "Execute Model(s)";
            this.btnExecuteModels.Click += new System.EventHandler(this.btnExecuteModels_Click);
            // 
            // btnLoadModels
            // 
            this.btnLoadModels.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnLoadModels.Location = new System.Drawing.Point(7, 223);
            this.btnLoadModels.Name = "btnLoadModels";
            this.btnLoadModels.Size = new System.Drawing.Size(137, 35);
            this.btnLoadModels.TabIndex = 1;
            this.btnLoadModels.Text = "Load Stored Model(s)";
            this.btnLoadModels.Click += new System.EventHandler(this.btnLoadModels_Click);
            // 
            // tabPageReviewResults
            // 
            this.tabPageReviewResults.Controls.Add(this.btnExportModelResults);
            this.tabPageReviewResults.Controls.Add(this.ultraLabel1);
            this.tabPageReviewResults.Controls.Add(this.txtUserName);
            this.tabPageReviewResults.Controls.Add(this.lblUserName);
            this.tabPageReviewResults.Controls.Add(this.txtRunDate);
            this.tabPageReviewResults.Controls.Add(this.lblRunDate);
            this.tabPageReviewResults.Controls.Add(this.btnImportModelResults);
            this.tabPageReviewResults.Controls.Add(this.grdModelResults);
            this.tabPageReviewResults.Location = new System.Drawing.Point(4, 25);
            this.tabPageReviewResults.Name = "tabPageReviewResults";
            this.tabPageReviewResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReviewResults.Size = new System.Drawing.Size(1041, 381);
            this.tabPageReviewResults.TabIndex = 2;
            this.tabPageReviewResults.Text = "Review Results";
            this.tabPageReviewResults.UseVisualStyleBackColor = true;
            // 
            // btnExportModelResults
            // 
            this.btnExportModelResults.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnExportModelResults.Location = new System.Drawing.Point(151, 250);
            this.btnExportModelResults.Name = "btnExportModelResults";
            this.btnExportModelResults.Size = new System.Drawing.Size(137, 35);
            this.btnExportModelResults.TabIndex = 65;
            this.btnExportModelResults.Text = "Export Model Results";
            this.btnExportModelResults.Click += new System.EventHandler(this.btnExportModelResults_Click);
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.AutoSize = true;
            this.ultraLabel1.Location = new System.Drawing.Point(8, 42);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(76, 14);
            this.ultraLabel1.TabIndex = 64;
            this.ultraLabel1.Text = "Model Results";
            // 
            // txtUserName
            // 
            this.txtUserName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gridModelOutputBindingSource, "UserName", true));
            this.txtUserName.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtUserName.Location = new System.Drawing.Point(279, 12);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(117, 21);
            this.txtUserName.TabIndex = 62;
            // 
            // gridModelOutputBindingSource
            // 
            this.gridModelOutputBindingSource.DataSource = typeof(SystemsAnalysis.Grid.GridAnalysis.GridModelOutput);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(210, 15);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(61, 14);
            this.lblUserName.TabIndex = 63;
            this.lblUserName.Text = "User Name";
            // 
            // txtRunDate
            // 
            this.txtRunDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gridModelOutputBindingSource, "RunDate", true));
            this.txtRunDate.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtRunDate.Location = new System.Drawing.Point(77, 12);
            this.txtRunDate.Name = "txtRunDate";
            this.txtRunDate.ReadOnly = true;
            this.txtRunDate.Size = new System.Drawing.Size(117, 21);
            this.txtRunDate.TabIndex = 60;
            // 
            // lblRunDate
            // 
            this.lblRunDate.AutoSize = true;
            this.lblRunDate.Location = new System.Drawing.Point(8, 15);
            this.lblRunDate.Name = "lblRunDate";
            this.lblRunDate.Size = new System.Drawing.Size(52, 14);
            this.lblRunDate.TabIndex = 61;
            this.lblRunDate.Text = "Run Date";
            // 
            // btnImportModelResults
            // 
            this.btnImportModelResults.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnImportModelResults.Location = new System.Drawing.Point(8, 250);
            this.btnImportModelResults.Name = "btnImportModelResults";
            this.btnImportModelResults.Size = new System.Drawing.Size(137, 35);
            this.btnImportModelResults.TabIndex = 4;
            this.btnImportModelResults.Text = "Import Model Results";
            this.btnImportModelResults.Click += new System.EventHandler(this.btnImportModelResults_Click);
            // 
            // grdModelResults
            // 
            this.grdModelResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdModelResults.DataSource = this.gridModelResultsBindingSource;
            appearance89.BackColor = System.Drawing.SystemColors.Window;
            appearance89.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grdModelResults.DisplayLayout.Appearance = appearance89;
            ultraGridColumn89.Header.VisiblePosition = 0;
            ultraGridColumn89.Width = 88;
            ultraGridColumn90.Header.VisiblePosition = 1;
            ultraGridColumn90.Width = 84;
            ultraGridColumn91.Header.VisiblePosition = 2;
            ultraGridColumn92.Header.VisiblePosition = 3;
            ultraGridColumn93.Header.VisiblePosition = 4;
            ultraGridColumn93.Width = 663;
            ultraGridColumn94.Header.VisiblePosition = 5;
            ultraGridBand16.Columns.AddRange(new object[] {
            ultraGridColumn89,
            ultraGridColumn90,
            ultraGridColumn91,
            ultraGridColumn92,
            ultraGridColumn93,
            ultraGridColumn94});
            ultraGridBand17.CardSettings.Width = 86;
            ultraGridBand17.CardView = true;
            ultraGridColumn95.Header.VisiblePosition = 0;
            ultraGridColumn96.Header.VisiblePosition = 1;
            ultraGridBand17.Columns.AddRange(new object[] {
            ultraGridColumn95,
            ultraGridColumn96});
            ultraGridBand17.UseRowLayout = true;
            this.grdModelResults.DisplayLayout.BandsSerializer.Add(ultraGridBand16);
            this.grdModelResults.DisplayLayout.BandsSerializer.Add(ultraGridBand17);
            this.grdModelResults.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdModelResults.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance90.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance90.BorderColor = System.Drawing.SystemColors.Window;
            this.grdModelResults.DisplayLayout.GroupByBox.Appearance = appearance90;
            appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdModelResults.DisplayLayout.GroupByBox.BandLabelAppearance = appearance91;
            this.grdModelResults.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance92.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance92.BackColor2 = System.Drawing.SystemColors.Control;
            appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance92.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdModelResults.DisplayLayout.GroupByBox.PromptAppearance = appearance92;
            this.grdModelResults.DisplayLayout.MaxColScrollRegions = 1;
            this.grdModelResults.DisplayLayout.MaxRowScrollRegions = 1;
            appearance93.BackColor = System.Drawing.SystemColors.Window;
            appearance93.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdModelResults.DisplayLayout.Override.ActiveCellAppearance = appearance93;
            appearance94.BackColor = System.Drawing.SystemColors.Highlight;
            appearance94.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdModelResults.DisplayLayout.Override.ActiveRowAppearance = appearance94;
            this.grdModelResults.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdModelResults.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdModelResults.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdModelResults.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdModelResults.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdModelResults.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance95.BackColor = System.Drawing.SystemColors.Window;
            this.grdModelResults.DisplayLayout.Override.CardAreaAppearance = appearance95;
            appearance96.BorderColor = System.Drawing.Color.Silver;
            appearance96.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grdModelResults.DisplayLayout.Override.CellAppearance = appearance96;
            this.grdModelResults.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdModelResults.DisplayLayout.Override.CellPadding = 0;
            appearance97.BackColor = System.Drawing.SystemColors.Control;
            appearance97.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance97.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance97.BorderColor = System.Drawing.SystemColors.Window;
            this.grdModelResults.DisplayLayout.Override.GroupByRowAppearance = appearance97;
            appearance98.TextHAlignAsString = "Left";
            this.grdModelResults.DisplayLayout.Override.HeaderAppearance = appearance98;
            this.grdModelResults.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdModelResults.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance99.BackColor = System.Drawing.SystemColors.Window;
            appearance99.BorderColor = System.Drawing.Color.Silver;
            this.grdModelResults.DisplayLayout.Override.RowAppearance = appearance99;
            this.grdModelResults.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdModelResults.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdModelResults.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdModelResults.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed;
            appearance100.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdModelResults.DisplayLayout.Override.TemplateAddRowAppearance = appearance100;
            this.grdModelResults.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdModelResults.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdModelResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grdModelResults.Location = new System.Drawing.Point(7, 59);
            this.grdModelResults.Name = "grdModelResults";
            this.grdModelResults.Size = new System.Drawing.Size(1027, 185);
            this.grdModelResults.TabIndex = 0;
            // 
            // gridModelResultsBindingSource
            // 
            this.gridModelResultsBindingSource.DataMember = "GridModelResults";
            this.gridModelResultsBindingSource.DataSource = this.gridModelOutputBindingSource;
            // 
            // tabPageDefineProjects
            // 
            this.tabPageDefineProjects.Controls.Add(this.ultraButtonArchiveInputOutput);
            this.tabPageDefineProjects.Controls.Add(this.cboDatabases);
            this.tabPageDefineProjects.Controls.Add(this.labelArchiveDatabase);
            this.tabPageDefineProjects.Controls.Add(this.cboServers);
            this.tabPageDefineProjects.Controls.Add(this.labelArchiveServer);
            this.tabPageDefineProjects.Controls.Add(this.txtOutputDirectory);
            this.tabPageDefineProjects.Controls.Add(this.btnCommitProjectChanges);
            this.tabPageDefineProjects.Controls.Add(this.lblNewProjectName);
            this.tabPageDefineProjects.Controls.Add(this.btnDeleteProject);
            this.tabPageDefineProjects.Controls.Add(this.txtNewProjectName);
            this.tabPageDefineProjects.Controls.Add(this.btnAddNewProject);
            this.tabPageDefineProjects.Controls.Add(this.lstDefineProject);
            this.tabPageDefineProjects.Controls.Add(this.lblProjectList);
            this.tabPageDefineProjects.Controls.Add(this.txtGridPath);
            this.tabPageDefineProjects.Controls.Add(this.lblPRFPath);
            this.tabPageDefineProjects.Controls.Add(this.txtPRFPath);
            this.tabPageDefineProjects.Controls.Add(this.lblGridPath);
            this.tabPageDefineProjects.Controls.Add(this.lblOutputDirectory);
            this.tabPageDefineProjects.Controls.Add(this.lblMIPPath);
            this.tabPageDefineProjects.Controls.Add(this.txtMIPPath);
            this.tabPageDefineProjects.Controls.Add(this.txtOSFPath);
            this.tabPageDefineProjects.Controls.Add(this.lblOSFPath);
            this.tabPageDefineProjects.Location = new System.Drawing.Point(4, 25);
            this.tabPageDefineProjects.Name = "tabPageDefineProjects";
            this.tabPageDefineProjects.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDefineProjects.Size = new System.Drawing.Size(1041, 381);
            this.tabPageDefineProjects.TabIndex = 3;
            this.tabPageDefineProjects.Text = "Define Projects";
            this.tabPageDefineProjects.UseVisualStyleBackColor = true;
            this.tabPageDefineProjects.Click += new System.EventHandler(this.tabPageDefineProjects_Click);
            // 
            // ultraButtonArchiveInputOutput
            // 
            this.ultraButtonArchiveInputOutput.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.ultraButtonArchiveInputOutput.Location = new System.Drawing.Point(670, 225);
            this.ultraButtonArchiveInputOutput.Name = "ultraButtonArchiveInputOutput";
            this.ultraButtonArchiveInputOutput.Size = new System.Drawing.Size(207, 35);
            this.ultraButtonArchiveInputOutput.TabIndex = 67;
            this.ultraButtonArchiveInputOutput.Text = "Archive Input/Output";
            this.ultraButtonArchiveInputOutput.Click += new System.EventHandler(this.ultraButtonArchiveInputOutput_Click);
            // 
            // cboDatabases
            // 
            this.cboDatabases.FormattingEnabled = true;
            this.cboDatabases.Location = new System.Drawing.Point(369, 192);
            this.cboDatabases.Name = "cboDatabases";
            this.cboDatabases.Size = new System.Drawing.Size(508, 21);
            this.cboDatabases.TabIndex = 66;
            // 
            // labelArchiveDatabase
            // 
            this.labelArchiveDatabase.AutoSize = true;
            this.labelArchiveDatabase.Location = new System.Drawing.Point(275, 196);
            this.labelArchiveDatabase.Name = "labelArchiveDatabase";
            this.labelArchiveDatabase.Size = new System.Drawing.Size(92, 13);
            this.labelArchiveDatabase.TabIndex = 65;
            this.labelArchiveDatabase.Text = "Archive Database";
            // 
            // cboServers
            // 
            this.cboServers.FormattingEnabled = true;
            this.cboServers.Location = new System.Drawing.Point(369, 164);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(508, 21);
            this.cboServers.TabIndex = 64;
            this.cboServers.SelectionChangeCommitted += new System.EventHandler(this.cboServers_SelectedValueChanged);
            // 
            // labelArchiveServer
            // 
            this.labelArchiveServer.AutoSize = true;
            this.labelArchiveServer.Location = new System.Drawing.Point(275, 170);
            this.labelArchiveServer.Name = "labelArchiveServer";
            this.labelArchiveServer.Size = new System.Drawing.Size(77, 13);
            this.labelArchiveServer.TabIndex = 63;
            this.labelArchiveServer.Text = "Archive Server";
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feGridProjectsBindingSource, "default_output_path", true));
            this.txtOutputDirectory.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtOutputDirectory.Location = new System.Drawing.Point(368, 137);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(509, 21);
            this.txtOutputDirectory.TabIndex = 8;
            // 
            // btnCommitProjectChanges
            // 
            this.btnCommitProjectChanges.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnCommitProjectChanges.Location = new System.Drawing.Point(281, 225);
            this.btnCommitProjectChanges.Name = "btnCommitProjectChanges";
            this.btnCommitProjectChanges.Size = new System.Drawing.Size(313, 35);
            this.btnCommitProjectChanges.TabIndex = 9;
            this.btnCommitProjectChanges.Text = "Commit Changes to Project";
            this.btnCommitProjectChanges.Click += new System.EventHandler(this.btnCommitProjectChanges_Click);
            // 
            // lblNewProjectName
            // 
            this.lblNewProjectName.AutoSize = true;
            this.lblNewProjectName.Location = new System.Drawing.Point(6, 266);
            this.lblNewProjectName.Name = "lblNewProjectName";
            this.lblNewProjectName.Size = new System.Drawing.Size(98, 14);
            this.lblNewProjectName.TabIndex = 59;
            this.lblNewProjectName.Text = "New Project Name";
            // 
            // btnDeleteProject
            // 
            this.btnDeleteProject.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnDeleteProject.Location = new System.Drawing.Point(144, 225);
            this.btnDeleteProject.Name = "btnDeleteProject";
            this.btnDeleteProject.Size = new System.Drawing.Size(128, 35);
            this.btnDeleteProject.TabIndex = 2;
            this.btnDeleteProject.Text = "Delete Project";
            this.btnDeleteProject.Click += new System.EventHandler(this.btnDeleteProject_Click);
            // 
            // txtNewProjectName
            // 
            this.txtNewProjectName.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtNewProjectName.Location = new System.Drawing.Point(6, 282);
            this.txtNewProjectName.Name = "txtNewProjectName";
            this.txtNewProjectName.Size = new System.Drawing.Size(177, 21);
            this.txtNewProjectName.TabIndex = 3;
            // 
            // btnAddNewProject
            // 
            this.btnAddNewProject.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnAddNewProject.Location = new System.Drawing.Point(7, 225);
            this.btnAddNewProject.Name = "btnAddNewProject";
            this.btnAddNewProject.Size = new System.Drawing.Size(131, 35);
            this.btnAddNewProject.TabIndex = 1;
            this.btnAddNewProject.Text = "Add New Project";
            this.btnAddNewProject.Click += new System.EventHandler(this.btnAddNewProject_Click);
            // 
            // lstDefineProject
            // 
            this.lstDefineProject.DataSource = this.feGridProjectsBindingSource;
            this.lstDefineProject.DisplayMember = "project_description";
            this.lstDefineProject.FormattingEnabled = true;
            this.lstDefineProject.Location = new System.Drawing.Point(7, 19);
            this.lstDefineProject.Name = "lstDefineProject";
            this.lstDefineProject.Size = new System.Drawing.Size(265, 199);
            this.lstDefineProject.TabIndex = 0;
            this.lstDefineProject.ValueMember = "project_id";
            // 
            // lblOutputDirectory
            // 
            this.lblOutputDirectory.AutoSize = true;
            this.lblOutputDirectory.Location = new System.Drawing.Point(278, 140);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.Size = new System.Drawing.Size(87, 14);
            this.lblOutputDirectory.TabIndex = 62;
            this.lblOutputDirectory.Text = "Output Directory";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.btnDeleteStoredModelRuns);
            this.pnlBottom.Controls.Add(this.grdModelRuns);
            this.pnlBottom.Controls.Add(this.lblModelRuns);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 493);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1051, 257);
            this.pnlBottom.TabIndex = 31;
            // 
            // btnDeleteStoredModelRuns
            // 
            this.btnDeleteStoredModelRuns.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnDeleteStoredModelRuns.Location = new System.Drawing.Point(7, 215);
            this.btnDeleteStoredModelRuns.Name = "btnDeleteStoredModelRuns";
            this.btnDeleteStoredModelRuns.Size = new System.Drawing.Size(183, 35);
            this.btnDeleteStoredModelRuns.TabIndex = 33;
            this.btnDeleteStoredModelRuns.Text = "Delete Stored Model Run(s)";
            this.btnDeleteStoredModelRuns.Click += new System.EventHandler(this.btnDeleteStoredModelRuns_Click);
            // 
            // grdModelRuns
            // 
            this.grdModelRuns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdModelRuns.DataSource = this.qryModelRunBindingSource;
            appearance61.BackColor = System.Drawing.SystemColors.Window;
            appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grdModelRuns.DisplayLayout.Appearance = appearance61;
            ultraGridColumn97.Header.VisiblePosition = 0;
            ultraGridColumn98.Header.VisiblePosition = 1;
            ultraGridColumn99.Header.VisiblePosition = 2;
            ultraGridColumn99.Hidden = true;
            ultraGridColumn100.Header.VisiblePosition = 3;
            ultraGridColumn100.Hidden = true;
            ultraGridColumn101.Header.VisiblePosition = 4;
            ultraGridColumn101.Hidden = true;
            ultraGridColumn102.Header.VisiblePosition = 5;
            ultraGridColumn103.Header.VisiblePosition = 6;
            ultraGridColumn103.Hidden = true;
            ultraGridColumn104.Header.VisiblePosition = 7;
            ultraGridColumn104.Hidden = true;
            ultraGridColumn105.Header.VisiblePosition = 8;
            ultraGridColumn106.Header.VisiblePosition = 9;
            ultraGridColumn107.Header.VisiblePosition = 10;
            ultraGridColumn108.Header.VisiblePosition = 11;
            ultraGridColumn109.Header.VisiblePosition = 12;
            ultraGridColumn109.Hidden = true;
            ultraGridColumn110.Header.VisiblePosition = 13;
            ultraGridColumn110.Hidden = true;
            ultraGridColumn111.Header.VisiblePosition = 14;
            ultraGridColumn111.Hidden = true;
            ultraGridColumn112.Header.VisiblePosition = 15;
            ultraGridColumn112.Hidden = true;
            ultraGridColumn113.Header.VisiblePosition = 16;
            ultraGridColumn114.Header.VisiblePosition = 17;
            ultraGridBand18.Columns.AddRange(new object[] {
            ultraGridColumn97,
            ultraGridColumn98,
            ultraGridColumn99,
            ultraGridColumn100,
            ultraGridColumn101,
            ultraGridColumn102,
            ultraGridColumn103,
            ultraGridColumn104,
            ultraGridColumn105,
            ultraGridColumn106,
            ultraGridColumn107,
            ultraGridColumn108,
            ultraGridColumn109,
            ultraGridColumn110,
            ultraGridColumn111,
            ultraGridColumn112,
            ultraGridColumn113,
            ultraGridColumn114});
            ultraGridColumn115.Header.VisiblePosition = 1;
            ultraGridColumn116.Header.VisiblePosition = 3;
            ultraGridColumn117.Header.VisiblePosition = 0;
            ultraGridColumn118.Header.VisiblePosition = 2;
            ultraGridColumn119.Header.VisiblePosition = 4;
            ultraGridColumn120.Header.VisiblePosition = 5;
            ultraGridColumn121.Header.VisiblePosition = 8;
            ultraGridColumn122.Header.VisiblePosition = 9;
            ultraGridColumn123.Header.VisiblePosition = 10;
            ultraGridColumn124.Header.VisiblePosition = 6;
            ultraGridColumn125.Header.VisiblePosition = 7;
            ultraGridBand19.Columns.AddRange(new object[] {
            ultraGridColumn115,
            ultraGridColumn116,
            ultraGridColumn117,
            ultraGridColumn118,
            ultraGridColumn119,
            ultraGridColumn120,
            ultraGridColumn121,
            ultraGridColumn122,
            ultraGridColumn123,
            ultraGridColumn124,
            ultraGridColumn125});
            ultraGridColumn126.Header.VisiblePosition = 0;
            ultraGridColumn127.Header.VisiblePosition = 1;
            ultraGridColumn128.Header.VisiblePosition = 2;
            ultraGridColumn129.Header.VisiblePosition = 3;
            ultraGridBand20.Columns.AddRange(new object[] {
            ultraGridColumn126,
            ultraGridColumn127,
            ultraGridColumn128,
            ultraGridColumn129});
            ultraGridColumn130.Header.VisiblePosition = 0;
            ultraGridColumn131.Header.VisiblePosition = 1;
            ultraGridColumn132.Header.VisiblePosition = 2;
            ultraGridColumn133.Header.VisiblePosition = 3;
            ultraGridColumn134.Header.VisiblePosition = 4;
            ultraGridBand21.Columns.AddRange(new object[] {
            ultraGridColumn130,
            ultraGridColumn131,
            ultraGridColumn132,
            ultraGridColumn133,
            ultraGridColumn134});
            ultraGridColumn135.Header.VisiblePosition = 0;
            ultraGridColumn136.Header.VisiblePosition = 1;
            ultraGridColumn137.Header.VisiblePosition = 2;
            ultraGridColumn138.Header.VisiblePosition = 3;
            ultraGridColumn139.Header.VisiblePosition = 4;
            ultraGridBand22.Columns.AddRange(new object[] {
            ultraGridColumn135,
            ultraGridColumn136,
            ultraGridColumn137,
            ultraGridColumn138,
            ultraGridColumn139});
            ultraGridColumn140.Header.VisiblePosition = 0;
            ultraGridColumn141.Header.VisiblePosition = 1;
            ultraGridColumn142.Header.VisiblePosition = 2;
            ultraGridColumn143.Header.VisiblePosition = 3;
            ultraGridBand23.Columns.AddRange(new object[] {
            ultraGridColumn140,
            ultraGridColumn141,
            ultraGridColumn142,
            ultraGridColumn143});
            ultraGridColumn144.Header.VisiblePosition = 0;
            ultraGridColumn145.Header.VisiblePosition = 1;
            ultraGridColumn146.Header.VisiblePosition = 4;
            ultraGridColumn147.Header.VisiblePosition = 5;
            ultraGridColumn148.Header.VisiblePosition = 2;
            ultraGridColumn149.Header.VisiblePosition = 3;
            ultraGridBand24.Columns.AddRange(new object[] {
            ultraGridColumn144,
            ultraGridColumn145,
            ultraGridColumn146,
            ultraGridColumn147,
            ultraGridColumn148,
            ultraGridColumn149});
            ultraGridColumn150.Header.VisiblePosition = 0;
            ultraGridColumn151.Header.VisiblePosition = 1;
            ultraGridColumn152.Header.VisiblePosition = 2;
            ultraGridColumn153.Header.VisiblePosition = 3;
            ultraGridColumn154.Header.VisiblePosition = 4;
            ultraGridColumn155.Header.VisiblePosition = 5;
            ultraGridBand25.Columns.AddRange(new object[] {
            ultraGridColumn150,
            ultraGridColumn151,
            ultraGridColumn152,
            ultraGridColumn153,
            ultraGridColumn154,
            ultraGridColumn155});
            ultraGridColumn156.Header.VisiblePosition = 0;
            ultraGridColumn157.Header.VisiblePosition = 1;
            ultraGridColumn158.Header.VisiblePosition = 2;
            ultraGridColumn159.Header.VisiblePosition = 3;
            ultraGridBand26.Columns.AddRange(new object[] {
            ultraGridColumn156,
            ultraGridColumn157,
            ultraGridColumn158,
            ultraGridColumn159});
            this.grdModelRuns.DisplayLayout.BandsSerializer.Add(ultraGridBand18);
            this.grdModelRuns.DisplayLayout.BandsSerializer.Add(ultraGridBand19);
            this.grdModelRuns.DisplayLayout.BandsSerializer.Add(ultraGridBand20);
            this.grdModelRuns.DisplayLayout.BandsSerializer.Add(ultraGridBand21);
            this.grdModelRuns.DisplayLayout.BandsSerializer.Add(ultraGridBand22);
            this.grdModelRuns.DisplayLayout.BandsSerializer.Add(ultraGridBand23);
            this.grdModelRuns.DisplayLayout.BandsSerializer.Add(ultraGridBand24);
            this.grdModelRuns.DisplayLayout.BandsSerializer.Add(ultraGridBand25);
            this.grdModelRuns.DisplayLayout.BandsSerializer.Add(ultraGridBand26);
            this.grdModelRuns.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdModelRuns.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance62.BorderColor = System.Drawing.SystemColors.Window;
            this.grdModelRuns.DisplayLayout.GroupByBox.Appearance = appearance62;
            appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdModelRuns.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
            this.grdModelRuns.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance64.BackColor2 = System.Drawing.SystemColors.Control;
            appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdModelRuns.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
            this.grdModelRuns.DisplayLayout.MaxColScrollRegions = 1;
            this.grdModelRuns.DisplayLayout.MaxRowScrollRegions = 1;
            appearance65.BackColor = System.Drawing.SystemColors.Window;
            appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdModelRuns.DisplayLayout.Override.ActiveCellAppearance = appearance65;
            appearance66.BackColor = System.Drawing.SystemColors.Highlight;
            appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdModelRuns.DisplayLayout.Override.ActiveRowAppearance = appearance66;
            this.grdModelRuns.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdModelRuns.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdModelRuns.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdModelRuns.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdModelRuns.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance67.BackColor = System.Drawing.SystemColors.Window;
            this.grdModelRuns.DisplayLayout.Override.CardAreaAppearance = appearance67;
            appearance68.BorderColor = System.Drawing.Color.Silver;
            appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grdModelRuns.DisplayLayout.Override.CellAppearance = appearance68;
            this.grdModelRuns.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdModelRuns.DisplayLayout.Override.CellPadding = 0;
            appearance69.BackColor = System.Drawing.SystemColors.Control;
            appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance69.BorderColor = System.Drawing.SystemColors.Window;
            this.grdModelRuns.DisplayLayout.Override.GroupByRowAppearance = appearance69;
            appearance70.TextHAlignAsString = "Left";
            this.grdModelRuns.DisplayLayout.Override.HeaderAppearance = appearance70;
            this.grdModelRuns.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdModelRuns.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance71.BackColor = System.Drawing.SystemColors.Window;
            appearance71.BorderColor = System.Drawing.Color.Silver;
            this.grdModelRuns.DisplayLayout.Override.RowAppearance = appearance71;
            this.grdModelRuns.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdModelRuns.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdModelRuns.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdModelRuns.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
            this.grdModelRuns.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdModelRuns.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdModelRuns.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdModelRuns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdModelRuns.Location = new System.Drawing.Point(3, 18);
            this.grdModelRuns.Name = "grdModelRuns";
            this.grdModelRuns.Size = new System.Drawing.Size(1042, 191);
            this.grdModelRuns.TabIndex = 32;
            this.grdModelRuns.Text = "Model Runs";
            // 
            // qryModelRunBindingSource
            // 
            this.qryModelRunBindingSource.DataMember = "FEGridProjects_QryModelRun";
            this.qryModelRunBindingSource.DataSource = this.feGridProjectsBindingSource;
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.txtProjectID);
            this.pnlTop.Controls.Add(this.lblProjectID);
            this.pnlTop.Controls.Add(this.cmbSelectedProject);
            this.pnlTop.Controls.Add(this.lblProject);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 24);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1051, 54);
            this.pnlTop.TabIndex = 32;
            // 
            // txtProjectID
            // 
            this.txtProjectID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.feGridProjectsBindingSource, "project_id", true));
            this.txtProjectID.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtProjectID.Location = new System.Drawing.Point(343, 19);
            this.txtProjectID.Name = "txtProjectID";
            this.txtProjectID.ReadOnly = true;
            this.txtProjectID.Size = new System.Drawing.Size(117, 21);
            this.txtProjectID.TabIndex = 58;
            // 
            // lblProjectID
            // 
            this.lblProjectID.AutoSize = true;
            this.lblProjectID.Location = new System.Drawing.Point(274, 22);
            this.lblProjectID.Name = "lblProjectID";
            this.lblProjectID.Size = new System.Drawing.Size(54, 14);
            this.lblProjectID.TabIndex = 59;
            this.lblProjectID.Text = "Project ID";
            // 
            // cmbSelectedProject
            // 
            this.cmbSelectedProject.DataSource = this.feGridProjectsBindingSource;
            this.cmbSelectedProject.DisplayMember = "project_description";
            this.cmbSelectedProject.FormattingEnabled = true;
            this.cmbSelectedProject.Location = new System.Drawing.Point(3, 19);
            this.cmbSelectedProject.Name = "cmbSelectedProject";
            this.cmbSelectedProject.Size = new System.Drawing.Size(265, 21);
            this.cmbSelectedProject.TabIndex = 0;
            this.cmbSelectedProject.ValueMember = "FEGridProjects.project_id";
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(3, 3);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(39, 14);
            this.lblProject.TabIndex = 13;
            this.lblProject.Text = "Project";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1051, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadDataToolStripMenuItem,
            this.browseToApplicationFolderToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // reloadDataToolStripMenuItem
            // 
            this.reloadDataToolStripMenuItem.Name = "reloadDataToolStripMenuItem";
            this.reloadDataToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.reloadDataToolStripMenuItem.Text = "Reload Data";
            this.reloadDataToolStripMenuItem.Click += new System.EventHandler(this.reloadDataToolStripMenuItem_Click);
            // 
            // browseToApplicationFolderToolStripMenuItem
            // 
            this.browseToApplicationFolderToolStripMenuItem.Name = "browseToApplicationFolderToolStripMenuItem";
            this.browseToApplicationFolderToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.browseToApplicationFolderToolStripMenuItem.Text = "Browse to Application Folder";
            this.browseToApplicationFolderToolStripMenuItem.Click += new System.EventHandler(this.browseToApplicationFolderToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // onlineHelpToolStripMenuItem
            // 
            this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
            this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.onlineHelpToolStripMenuItem.Text = "Online Help";
            this.onlineHelpToolStripMenuItem.Click += new System.EventHandler(this.onlineHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMiddle.Controls.Add(this.tabControl1);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Location = new System.Drawing.Point(0, 78);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlMiddle.Size = new System.Drawing.Size(1051, 415);
            this.pnlMiddle.TabIndex = 33;
            // 
            // FrmGridAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 750);
            this.Controls.Add(this.pnlMiddle);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlBottom);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmGridAnalysis";
            this.Text = "GRID v5.0";
            this.Load += new System.EventHandler(this.frmGridAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtGridPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.feGridProjectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInterfaceDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPRFPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMIPPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOSFPath)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageDefineScenarios.ResumeLayout(false);
            this.tabPageDefineScenarios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScenarioID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.feScenariosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewScenarioName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPollutantLoadingTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBMPEffectivenessTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPollutantLoadingDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBMPEffectivenessDB)).EndInit();
            this.tabPageDefineSelectionSets.ResumeLayout(false);
            this.tabPageDefineSelectionSets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            this.tabPageDefineModelRuns.ResumeLayout(false);
            this.tabPageDefineModelRuns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSelectScenario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSelectionSetAreas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.feSelectionSetAreasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdHyetographs)).EndInit();
            this.tabPageExecuteGrid.ResumeLayout(false);
            this.tabPageExecuteGrid.PerformLayout();
            this.pnlCancelBackgroundWorker.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdModelMetadata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridModelRunsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridModelEngineBindingSource)).EndInit();
            this.tabPageReviewResults.ResumeLayout(false);
            this.tabPageReviewResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridModelOutputBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRunDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdModelResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridModelResultsBindingSource)).EndInit();
            this.tabPageDefineProjects.ResumeLayout(false);
            this.tabPageDefineProjects.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutputDirectory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewProjectName)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdModelRuns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryModelRunBindingSource)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectID)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlMiddle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private Infragistics.Win.UltraWinEditors.UltraTextEditor txtGridPath;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtGridPath;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPRFPath;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtMIPPath;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtOSFPath;        
        private Infragistics.Win.Misc.UltraLabel lblGridPath;
        private Infragistics.Win.Misc.UltraLabel lblPRFPath;
        private Infragistics.Win.Misc.UltraLabel lblMIPPath;
        private Infragistics.Win.Misc.UltraLabel lblOSFPath;
        private Infragistics.Win.Misc.UltraLabel lblProjectList;
        private Infragistics.Win.Misc.UltraLabel lblSelectionSetAreas;
        private Infragistics.Win.Misc.UltraLabel lblModelRuns;
        private Infragistics.Win.Misc.UltraLabel lblHyetographs;
        private Infragistics.Win.Misc.UltraButton btnDefineModelRun;        
        private Infragistics.Win.Misc.UltraButton btnCommitHyetographChanges;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDefineModelRuns;
        private System.Windows.Forms.TabPage tabPageExecuteGrid;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlTop;
        private Infragistics.Win.Misc.UltraButton btnLoadModels;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdSelectionSetAreas;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdHyetographs;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdModelRuns;
        private Infragistics.Win.Misc.UltraButton btnExecuteModels;
        private Infragistics.Win.Misc.UltraButton btnClearModels;
        private Infragistics.Win.Misc.UltraButton btnExportModelMetadata;
        private Infragistics.Win.Misc.UltraButton btnImportModelMetadata;
        private GridInterfaceDataSet gridInterfaceDS;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Panel pnlCancelBackgroundWorker;
        private Infragistics.Win.Misc.UltraLabel lblModelRunStatus;
        private Infragistics.Win.Misc.UltraButton btnCancel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadDataToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMiddle;
        private System.Windows.Forms.TabPage tabPageReviewResults;        
        private Infragistics.Win.UltraWinGrid.UltraGrid grdModelMetadata;
        private System.Windows.Forms.BindingSource gridModelEngineBindingSource;
        private System.Windows.Forms.BindingSource gridModelRunsBindingSource;
        private Infragistics.Win.Misc.UltraLabel label1;
        private Infragistics.Win.Misc.UltraButton btnDeleteStoredModelRuns;
        private System.Windows.Forms.TabPage tabPageDefineProjects;
        private Infragistics.Win.Misc.UltraLabel lblProject;
        private System.Windows.Forms.ListBox lstDefineProject;
        private System.Windows.Forms.ComboBox cmbSelectedProject;
        private System.Windows.Forms.TabPage tabPageDefineScenarios;
        private Infragistics.Win.Misc.UltraButton btnUpdateSelectedProcesses;
        private Infragistics.Win.Misc.UltraLabel lblModelScenarios;
        private Infragistics.Win.Misc.UltraButton btnCommitScenarioChanges;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPollutantLoadingDB;
        private Infragistics.Win.Misc.UltraLabel lblBMPEffectiveness;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtBMPEffectivenessDB;
        private Infragistics.Win.Misc.UltraLabel lblPollutantLoads;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPollutantLoadingTable;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtBMPEffectivenessTable;
        private System.Windows.Forms.TabPage tabPageDefineSelectionSets;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor textBox1;
        private Infragistics.Win.Misc.UltraLabel lblSelectScenario;
        private System.Windows.Forms.ListBox lstDefineScenario;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtTimePeriod;
        private Infragistics.Win.Misc.UltraLabel lblTimePeriod;
        private System.Windows.Forms.CheckBox chkInstreamFacilities;
        private Infragistics.Win.Misc.UltraLabel label2;
        private Infragistics.Win.Misc.UltraLabel label3;
        private Infragistics.Win.Misc.UltraLabel label4;
        private Infragistics.Win.Misc.UltraButton btnAddNewScenario;
        private Infragistics.Win.Misc.UltraLabel lblTableName;
        private Infragistics.Win.Misc.UltraLabel lblDBPath;
        private Infragistics.Win.Misc.UltraButton btnDeleteScenario;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNewScenarioName;
        private Infragistics.Win.Misc.UltraLabel label5;
        private Infragistics.Win.Misc.UltraButton btnCommitProjectChanges;
        private Infragistics.Win.Misc.UltraLabel lblNewProjectName;
        private Infragistics.Win.Misc.UltraButton btnDeleteProject;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNewProjectName;
        private Infragistics.Win.Misc.UltraButton btnAddNewProject;
        private Infragistics.Win.Misc.UltraLabel lblOutputDirectory;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtOutputDirectory;
        private System.Windows.Forms.BindingSource feGridProjectsBindingSource;
        private System.Windows.Forms.BindingSource feScenariosBindingSource;
        public Infragistics.Win.UltraWinGrid.UltraGrid grdSelectScenario;
        private System.Windows.Forms.BindingSource feSelectionSetAreasBindingSource;
        private System.Windows.Forms.BindingSource qryModelRunBindingSource;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtScenarioID;
        private Infragistics.Win.Misc.UltraLabel lblScenarioID;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtProjectID;
        private Infragistics.Win.Misc.UltraLabel lblProjectID;
        private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdModelResults;
        private System.Windows.Forms.BindingSource gridModelOutputBindingSource;
        private System.Windows.Forms.BindingSource gridModelResultsBindingSource;
        private Infragistics.Win.Misc.UltraButton btnImportModelResults;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtUserName;
        private Infragistics.Win.Misc.UltraLabel lblUserName;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtRunDate;
        private Infragistics.Win.Misc.UltraLabel lblRunDate;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private System.Windows.Forms.ToolStripMenuItem browseToApplicationFolderToolStripMenuItem;
        private Infragistics.Win.Misc.UltraButton btnExportModelResults;
        private System.Windows.Forms.Label labelArchiveServer;
        private System.Windows.Forms.ComboBox cboServers;
        private System.Windows.Forms.ComboBox cboDatabases;
        private System.Windows.Forms.Label labelArchiveDatabase;
        private Infragistics.Win.Misc.UltraButton ultraButtonArchiveInputOutput;
    }
}

