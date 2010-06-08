namespace SystemsAnalysis.Grid.GridAnalysis
{

    public partial class GridInterfaceDataSet
    {
        public bool ModelRunExists(int scenarioID, int selectionSetAreaID, int hyetographID)
        {
            foreach (GridInterfaceDataSet.FEModelRunRow modelRunRow in this.FEModelRun)
            {
                if (modelRunRow.scenario_id == scenarioID &&
                    modelRunRow.selection_set_area_id == selectionSetAreaID &&
                    modelRunRow.hyetograph_id == hyetographID)
                {
                    return true;
                }
            }
            return false;
        }

        internal void LoadData()
        {
            this.Clear();
            GridInterfaceDataSetTableAdapters.FEGridProjectsTableAdapter feGridProjectsTA;
            feGridProjectsTA = new GridInterfaceDataSetTableAdapters.FEGridProjectsTableAdapter();
            feGridProjectsTA.Fill(this.FEGridProjects);

            GridInterfaceDataSetTableAdapters.FESelectionSetAreasTableAdapter feSelectionSetAreasTA;
            feSelectionSetAreasTA = new GridInterfaceDataSetTableAdapters.FESelectionSetAreasTableAdapter();
            feSelectionSetAreasTA.Fill(this.FESelectionSetAreas);

            GridInterfaceDataSetTableAdapters.FEScenariosTableAdapter feScenariosTA;
            feScenariosTA = new GridInterfaceDataSetTableAdapters.FEScenariosTableAdapter();
            feScenariosTA.Fill(this.FEScenarios);

            GridInterfaceDataSetTableAdapters.FEScenarioXProcessTableAdapter feScenarioXProcessTA;
            feScenarioXProcessTA = new GridInterfaceDataSetTableAdapters.FEScenarioXProcessTableAdapter();
            feScenarioXProcessTA.Fill(this.FEScenarioXProcess);

            GridInterfaceDataSetTableAdapters.FEModelRunTableAdapter feModelRunTA;
            feModelRunTA = new GridInterfaceDataSetTableAdapters.FEModelRunTableAdapter();
            feModelRunTA.Fill(this.FEModelRun);

            GridInterfaceDataSetTableAdapters.FEHyetographsTableAdapter feHyetographsTA;
            feHyetographsTA = new GridInterfaceDataSetTableAdapters.FEHyetographsTableAdapter();
            feHyetographsTA.Fill(this.FEHyetographs);

            GridInterfaceDataSetTableAdapters.FEHyetographDataTableAdapter feHyetographDataTA;
            feHyetographDataTA = new GridInterfaceDataSetTableAdapters.FEHyetographDataTableAdapter();
            feHyetographDataTA.Fill(this.FEHyetographData);

            GridInterfaceDataSetTableAdapters.FEProcessGroupTableAdapter feProcessGroupTA;
            feProcessGroupTA = new GridInterfaceDataSetTableAdapters.FEProcessGroupTableAdapter();
            feProcessGroupTA.Fill(this.FEProcessGroup);

            GridInterfaceDataSetTableAdapters.FEProcessTableAdapter feProcessTA;
            feProcessTA = new GridInterfaceDataSetTableAdapters.FEProcessTableAdapter();
            feProcessTA.Fill(this.FEProcess);

            GridInterfaceDataSetTableAdapters.QryModelRunTableAdapter qryModelRunTA;
            qryModelRunTA = new GridInterfaceDataSetTableAdapters.QryModelRunTableAdapter();
            qryModelRunTA.Fill(this.QryModelRun);
            
        }

        internal void RefreshModelRunQuery()
        {
            GridInterfaceDataSetTableAdapters.FEModelRunTableAdapter feModelRunTA;
            feModelRunTA = new GridInterfaceDataSetTableAdapters.FEModelRunTableAdapter();
            feModelRunTA.Fill(this.FEModelRun);

            GridInterfaceDataSetTableAdapters.QryModelRunTableAdapter qryModelRunTA;
            qryModelRunTA = new GridAnalysis.GridInterfaceDataSetTableAdapters.QryModelRunTableAdapter();
            qryModelRunTA.Fill(this.QryModelRun);
        }
    }
}
