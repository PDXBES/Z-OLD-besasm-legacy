using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.DataAccess;

namespace DataAccessTestBench
{
  public partial class Form1 : Form
  {
    SystemsAnalysis.DataAccess.StormwaterControlsDataSet scDS;
    SystemsAnalysis.DataAccess.AlternativeDataSet altDS;

    public Form1()
    {
      InitializeComponent();

      scDS = new StormwaterControlsDataSet();
      //scDS.InitStormwaterControlDataSet(@"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\Alts\Beech_Essex\BEE_NP-Pipe");
      scDS.InitStormwaterControlDataSet(@"C:\Data\Projects\41800023-1 BES_DataManagement\Beech_Essex\BEE_RP");

      altDS = new AlternativeDataSet();
      //altDS.InitAlternativeDataSet(@"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\Alts\Beech_Essex\BEE_NP_Base\alternatives\BEE_NP_NP-Pipe");
      altDS.InitAlternativeDataSet(@"C:\Data\Projects\41800023-1 BES_DataManagement\Beech_Essex\BEE_FU\alternatives\BEE_RP");
    }

    private void button1_Click(object sender, EventArgs e)
    {
      DataSet workingDS = new DataSet();

      workingDS.Tables.Add(scDS.ic_ParkingTargets.Copy());
      workingDS.Tables.Add(scDS.ic_RoofTargets.Copy());
      workingDS.Tables.Add(scDS.ic_StreetTargets.Copy());
      workingDS.Tables.Add(scDS.mdl_SurfSC_ac.Copy());
      workingDS.Tables.Add(scDS.ICNode.Copy());
      workingDS.Tables.Add(scDS.ICTargetPark.Copy());
      workingDS.Tables.Add(scDS.ICTargetRoof.Copy());

      workingDS.Tables.Add(altDS.AltParkingTargets.Copy());
      workingDS.Tables.Add(altDS.AltRoofTargets.Copy());
      workingDS.Tables.Add(altDS.AltStreetTargets.Copy());

      var query =
        from icNode in workingDS.Tables["icNode"].AsEnumerable()
        join icStreetTarget in workingDS.Tables["ic_StreetTargets"].AsEnumerable()
        on icNode.Field<string>("FacNode") equals icStreetTarget.Field<string>("XPSWMM_Name")
        join altStreetTarget in workingDS.Tables["AltStreetTargets"].AsEnumerable()
        on icStreetTarget.Field<int>("icID") equals altStreetTarget.Field<int>("icID")
        join mdlSurfSc in workingDS.Tables["mdl_SurfSc_ac"].AsEnumerable()
        on icStreetTarget.Field<int>("surfSCID") equals mdlSurfSc.Field<int>("surfSCID")
        group mdlSurfSc by altStreetTarget.Field<string>("FocusArea") into focusArea
        orderby focusArea.Key
        select new
        {
          FocusArea = focusArea.Key,
          NetIA = focusArea.Sum(p => p.Field<double>("c_netIMPacres")),          
        };

      textBox1.AppendText("Infiltrate Stormwater\n------------------------------------\n");
      foreach (var row in query)
      {
        textBox1.AppendText("Focus Area " + row.FocusArea + ": " + row.NetIA + "\n");
      }
      
      textBox1.AppendText("Protect and Improve Terrestrial Habitat\n------------------------------------\n");
      var query2 = 
        from icNode in workingDS.Tables["icNode"].AsEnumerable()
        join icStreetTarget in workingDS.Tables["ic_StreetTargets"].AsEnumerable()
        on icNode.Field<string>("FacNode") equals icStreetTarget.Field<string>("XPSWMM_Name")
        join altStreetTarget in workingDS.Tables["AltStreetTargets"].AsEnumerable()
        on icStreetTarget.Field<int>("icID") equals altStreetTarget.Field<int>("icID")
        where icNode.Field<string>("FacType") == "C" && icStreetTarget.Field<int>("Constructed") == 0
        group icNode by altStreetTarget.Field<string>("FocusArea") into focusArea
        orderby focusArea.Key
        select new
        {
          FocusArea = focusArea.Key,
          FacilityVolume = focusArea.Sum(p => p.Field<double>("FacVolCuFt")) / 0.75 / 43560,
        };

      foreach (var row in query2)
      {
        textBox1.AppendText("Focus Area " + row.FocusArea + ": " + row.FacilityVolume + "\n");
      }
    }
  }
}
