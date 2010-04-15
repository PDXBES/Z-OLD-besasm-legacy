using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Reporting.ReportLibraries;
using SystemsAnalysis.Modeling.ModelUtils.ResultsExtractor;
  
namespace DataAccessTestBench
{
  public partial class Form1 : Form
  {
    private SystemsAnalysis.DataAccess.StormwaterControlsDataSet scDS;
    
    private RecommendedPlanReport rpReport;

    public Form1()
    {
      InitializeComponent();

      string testModelPath = @"C:\Data\Projects\41800023-1 BES_DataManagement\Beech_Essex\BEE_RP\";
      //string testModelPath = @"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\Alts\Beech_Essex\BEE_NP-Pipe\";

      string testAlternativePath = @"C:\Data\Projects\41800023-1 BES_DataManagement\Beech_Essex\BEE_FU\alternatives\BEE_RP\";
      //string testAlternativePath = @"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\Alts\Beech_Essex\BEE_NP_Base\alternatives\BEE_NP_NP-Pipe";

      string testSwmmOutput = @"C:\Data\Projects\41800023-1 BES_DataManagement\Beech_Essex\BEE_FU\sim\25\BEE_FU_25.out";
           
      scDS = new StormwaterControlsDataSet();
      scDS.InitStormwaterControlDataSet(testModelPath);
      scDS.InitAltTargetDataTables(testAlternativePath);
      rpReport = new RecommendedPlanReport();
      Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Add("ModelPath", new ReportBase.Parameter("ModelPath", testModelPath));
      parameters.Add("AlternativePath", new ReportBase.Parameter("AlternativePath", testAlternativePath));
      rpReport.LoadAuxilaryData(parameters);

      XPSWMMResults xpSwmmResults = new XPSWMMResults(testSwmmOutput);
      scDS.Tables.Add(xpSwmmResults.GetTableE18());
      
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

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {
      textBox1.Clear();

      textBox1.AppendText("Infiltrate Stormwater\n");
      textBox1.AppendText("------------------------------------\n");

      var query =
        from altStreetTargets in scDS.AltStreetTargets
        group altStreetTargets by altStreetTargets.FocusArea into grpFocusArea
        orderby grpFocusArea.Key
        select new
        {
          FocusArea = grpFocusArea.Key
        };

      foreach (var row in query)
      {
        Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", row.FocusArea));
        double infiltratedArea = rpReport.InfiltrateStormwaterArea(parameters);

        textBox1.AppendText("Focus Area: " + row.FocusArea + "; Infiltrated Area = " + infiltratedArea + "\n");        
      }

      textBox1.AppendText("\n");
      textBox1.AppendText("Protect and Improve Terrestrial Habitat\n");
      textBox1.AppendText("------------------------------------\n");
      foreach (var row in query)
      {
        Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", row.FocusArea));
        double infiltratedArea = rpReport.ProtectAndImproveTerrestrialHabitialArea(parameters);

        textBox1.AppendText("Focus Area: " + row.FocusArea + "; Infiltrated Area = " + infiltratedArea + "\n");        
      }
    }
  }
}
