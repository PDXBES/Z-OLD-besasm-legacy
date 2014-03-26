using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public partial class FrmChooseProcesses : Form
    {
        private int scenarioID;

        public FrmChooseProcesses(int scenarioID)
        {
            InitializeComponent();
            this.scenarioID = scenarioID;
        }

        private void frmChooseProcesses_Load(object sender, EventArgs e)
        {
            GridInterfaceDataSetTableAdapters.FEScenariosTableAdapter feGridScenariosTA;
            feGridScenariosTA = new GridInterfaceDataSetTableAdapters.FEScenariosTableAdapter();
            feGridScenariosTA.Fill(gridInterfaceDS.FEScenarios);

            GridInterfaceDataSetTableAdapters.FEProcessGroupTableAdapter feProcessGroupTA;
            feProcessGroupTA = new GridInterfaceDataSetTableAdapters.FEProcessGroupTableAdapter();
            feProcessGroupTA.Fill(gridInterfaceDS.FEProcessGroup);

            GridInterfaceDataSetTableAdapters.FEScenarioXProcessTableAdapter scenarioXProcessTA;
            scenarioXProcessTA = new GridInterfaceDataSetTableAdapters.FEScenarioXProcessTableAdapter();
            scenarioXProcessTA.FillByScenarioID(gridInterfaceDS.FEScenarioXProcess, scenarioID);

            //GridInterfaceDataSetTableAdapters.QryProcessesTableAdapter qryProcessesTA;
            //qryProcessesTA = new GridInterfaceDataSetTableAdapters.QryProcessesTableAdapter();
            //qryProcessesTA.Fill(gridInterfaceDS.QryProcesses, scenarioID);

            foreach (GridInterfaceDataSet.FEProcessGroupRow processGroupRow in gridInterfaceDS.FEProcessGroup)
            {
                bool includedInScenario = false;
                foreach (GridInterfaceDataSet.FEScenarioXProcessRow scenarioXProcessRow in gridInterfaceDS.FEScenarioXProcess)
                {
                    if (scenarioXProcessRow.process_group == processGroupRow.process_group)
                    {
                        includedInScenario = true;
                        break;
                    }
                }
                gridInterfaceDS.QryProcesses.AddQryProcessesRow(processGroupRow.process_group, processGroupRow.description, includedInScenario, processGroupRow.group_order);
                //processRow.run_group = processRow.included_in_scenario;// == -1 ? true : false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
            try
            {
                SaveProcesses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save changes to process group list: " + ex.Message, "Error saving changes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void SaveProcesses()
        {
            GridInterfaceDataSetTableAdapters.FEScenarioXProcessTableAdapter feScenarioXProcessTA;
            feScenarioXProcessTA = new GridInterfaceDataSetTableAdapters.FEScenarioXProcessTableAdapter();
            //feScenarioXProcessTA.FillByScenarioID(gridInterfaceDS.FEScenarioXProcess, scenarioID);

            foreach (GridInterfaceDataSet.QryProcessesRow processRow in gridInterfaceDS.QryProcesses)
            {
                bool rowProcessed = false;
                DataTable unchangedRows = gridInterfaceDS.FEScenarioXProcess.GetChanges(DataRowState.Unchanged);
                if (unchangedRows != null)
                {
                    foreach (GridInterfaceDataSet.FEScenarioXProcessRow scenarioXProcessRow in unchangedRows.Rows)
                    {
                        //Scenario: Process set to execute, and already exists in list
                        if (scenarioXProcessRow.process_group == processRow.process_group && processRow.included_in_scenario)
                        {
                            rowProcessed = true;
                            break;
                        }
                        //Scenario: Process should NOT execute, and already exists in list
                        else if (scenarioXProcessRow.process_group == processRow.process_group && !processRow.included_in_scenario)
                        {
                            rowProcessed = true;
                            gridInterfaceDS.FEScenarioXProcess.FindByscenario_x_process_id(scenarioXProcessRow.scenario_x_process_id).Delete();
                            //scenarioXProcessRow.Delete();
                            break;
                        }
                    }
                }
                //Scenario: Process set to execute, but does not exist in list
                if (!rowProcessed && processRow.included_in_scenario)
                {
                    gridInterfaceDS.FEScenarioXProcess.AddFEScenarioXProcessRow(
                        gridInterfaceDS.FEScenarios.FindByscenario_id(scenarioID),
                        processRow.process_group);
                }
            }
            feScenarioXProcessTA.Update(gridInterfaceDS.FEScenarioXProcess);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gridInterfaceDS.QryProcesses.RejectChanges();
            this.Close();
        }
    }
}
