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

    public Form1()
    {
      InitializeComponent();

      scDS = new StormwaterControlsDataSet();

      string testModelPath = @"C:\Data\Projects\41800023-1 BES_DataManagement\Beech_Essex\BEE_RP";
      //string testModelPath = @"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\Alts\Beech_Essex\BEE_NP-Pipe";
      scDS.InitStormwaterControlDataSet(testModelPath);
      
      string testAlternativePath = @"C:\Data\Projects\41800023-1 BES_DataManagement\Beech_Essex\BEE_FU\alternatives\BEE_RP";
      //string testAlternativePath = @"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\Alts\Beech_Essex\BEE_NP_Base\alternatives\BEE_NP_NP-Pipe";
      scDS.InitAltTargetDataTables(testAlternativePath);
    }

    private void button1_Click(object sender, EventArgs e)
    {      
      var query =
        from icNode in scDS.ICNode
        join icStreetTarget in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTarget.XPSWMM_Name
        join altStreetTarget in scDS.AltStreetTargets
        on icStreetTarget.icID equals altStreetTarget.ICID
        join mdlSurfSc in scDS.mdl_SurfSC_ac
        on icStreetTarget.surfSCID equals mdlSurfSc.SurfSCID
        group mdlSurfSc by altStreetTarget.FocusArea into grpFocusArea
        orderby grpFocusArea.Key
        select new
        {
          FocusArea = grpFocusArea.Key,
          NetIA = grpFocusArea.Sum(p => p.c_netIMPacres)
        };

      textBox1.AppendText("Infiltrate Stormwater\n");
      textBox1.AppendText("------------------------------------\n");
      foreach (var row in query)
      {
        textBox1.AppendText("Focus Area " + row.FocusArea + ": " + row.NetIA + "\n");
      }

      textBox1.AppendText("\n");
      textBox1.AppendText("Protect and Improve Terrestrial Habitat\n");
      textBox1.AppendText("------------------------------------\n");
      var query2 = 
        from icNode in scDS.ICNode
        join icStreetTarget in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTarget.XPSWMM_Name
        join altStreetTarget in scDS.AltStreetTargets
        on icStreetTarget.icID equals altStreetTarget.ICID
        where icNode.Factype == "C" && icStreetTarget.constructed == 0
        group icNode by altStreetTarget.FocusArea into grpFocusArea
        orderby grpFocusArea.Key
        select new
        {
          FocusArea = grpFocusArea.Key,
          FacilityVolume = grpFocusArea.Sum(p => p.FacVolCuFt) / 0.75 / 43560,
        };

      foreach (var row in query2)
      {
        textBox1.AppendText("Focus Area " + row.FocusArea + ": " + row.FacilityVolume + "\n");
      }
    }
  }
}
