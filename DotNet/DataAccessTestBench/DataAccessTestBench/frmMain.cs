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
  public partial class frmMain : Form
  {
    private SystemsAnalysis.DataAccess.StormwaterControlsDataSet scDS;

    private RecommendedPlanReport rpReport;

    public frmMain()
    {
      InitializeComponent();

      //string testModelPath = @"C:\Data\Projects\41800023-1_BES_DataManagement\Beech_Essex\BEE_RP\";
      string testModelPath = @"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\RP\Beech_Essex\BEE_RP\";

      //string testAlternativePath = @"C:\Data\Projects\41800023-1_BES_DataManagement\Beech_Essex\BEE_FU\alternatives\BEE_RP\";
      string testAlternativePath = @"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\RP\Beech_Essex\BEE_FU\alternatives\BEE_RP";

      //string testSwmmOutput = @"C:\Data\Projects\41800023-1_BES_DataManagement\Beech_Essex\BEE_RP\sim\4S6\BEE_RP_4S6.out";
      string testSwmmOutput = @"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\RP\Beech_Essex\BEE_RP\sim\4S6\BEE_RP_4S6.out";

      rpReport = new RecommendedPlanReport();
      Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Add("ModelPath", new ReportBase.Parameter("ModelPath", testModelPath));
      parameters.Add("AlternativePath", new ReportBase.Parameter("AlternativePath", testAlternativePath));
      parameters.Add("SwmmOutputFile", new ReportBase.Parameter("SwmmOutputFile", testSwmmOutput));
      rpReport.LoadAuxilaryData(parameters);

    }

    private void frmMain_Load(object sender, EventArgs e)
    {

    }

    private void btnExecuteQueriesDirect_Click(object sender, EventArgs e)
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
      textBox1.AppendText("------------------------------------\r\n");
      foreach (var row in query)
      {
        textBox1.AppendText("Focus Area " + row.FocusArea + ": " + row.NetIA + "\r\n");
      }

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Protect and Improve Terrestrial Habitat\r\n");
      textBox1.AppendText("------------------------------------\r\n");
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
        textBox1.AppendText("Focus Area " + row.FocusArea + ": " + row.FacilityVolume + "\r\n");
      }
    }

    private void btnUseReportLibrary_Click(object sender, EventArgs e)
    {
      textBox1.Clear();

      textBox1.AppendText("Infiltrate Stormwater Area\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      var query =
        from altStreetTargets in rpReport.scDS.AltStreetTargets
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

        textBox1.AppendText("Focus Area: " + row.FocusArea + "; Infiltrated Area = " + infiltratedArea + "\r\n");
      }

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Protect and Improve Terrestrial Habitat Area\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (var row in query)
      {
        Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", row.FocusArea));
        double infiltratedArea = rpReport.ProtectImproveTerrestrialHabitatArea(parameters);

        textBox1.AppendText("Focus Area: " + row.FocusArea + "; Habitat Area = " + infiltratedArea + "\r\n");
      }

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Roof Planter Storage Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (var row in query)
      {
        Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", row.FocusArea));
        double stormwaterRemovalVol = rpReport.RoofPlanterStorageVolume(parameters);

        textBox1.AppendText("Focus Area: " + row.FocusArea + "; Vol Remove: = " + stormwaterRemovalVol + "\r\n");
      }

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Roof Planter Infiltration Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (var row in query)
      {
        Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", row.FocusArea));
        double stormwaterRemovalVol = rpReport.RoofPlanterInfiltrationVolume(parameters);

        textBox1.AppendText("Focus Area: " + row.FocusArea + "; Vol Remove: = " + stormwaterRemovalVol + "\r\n");
      }

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Roof Bio Storage Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (var row in query)
      {
        Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", row.FocusArea));
        double stormwaterRemovalVol = rpReport.RoofBioStorageVolume(parameters);

        textBox1.AppendText("Focus Area: " + row.FocusArea + "; Vol Remove: = " + stormwaterRemovalVol + "\r\n");
      }

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Roof Bio Infiltration Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (var row in query)
      {
        Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", row.FocusArea));
        double stormwaterRemovalVol = rpReport.RoofBioInfiltrationVolume(parameters);

        textBox1.AppendText("Focus Area: " + row.FocusArea + "; Vol Remove: = " + stormwaterRemovalVol + "\r\n");
      }

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Park Bio Storage Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (var row in query)
      {
        Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", row.FocusArea));
        double stormwaterRemovalVol = rpReport.ParkBioStorageVolume(parameters);

        textBox1.AppendText("Focus Area: " + row.FocusArea + "; Vol Remove: = " + stormwaterRemovalVol + "\r\n");
      }

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Park Bio Infiltration Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (var row in query)
      {
        Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", row.FocusArea));
        double stormwaterRemovalVol = rpReport.ParkBioInfiltrationVolume(parameters);

        textBox1.AppendText("Focus Area: " + row.FocusArea + "; Vol Remove: = " + stormwaterRemovalVol + "\r\n");
      }
    }

    private void btnBSBRTest_Click(object sender, EventArgs e)
    {
      textBox1.Clear();

      #region BSBRCount2yrEX

      textBox1.AppendText("BSBR Count (2-yr EX)\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      Dictionary<string, ReportBase.Parameter> parameters;
      int bsbrCount = 0;
      foreach (string focusArea in rpReport.FocusAreaList())
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("StormEvent", new ReportBase.Parameter("StormEvent", "2"));
        parameters.Add("UseFlag", new ReportBase.Parameter("UseFlag", "14"));
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        bsbrCount = rpReport.BSBRCount(parameters);

        textBox1.AppendText(focusArea + " BSBR Count: " + bsbrCount.ToString() + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();

      parameters.Add("UseFlag", new ReportBase.Parameter("UseFlag", "14"));
      parameters.Add("StormEvent", new ReportBase.Parameter("StormEvent", "2"));
      parameters.Remove("FocusArea");
      bsbrCount = rpReport.BSBRCount(parameters);
      textBox1.AppendText("Total BSBR 2-yr EX: " + bsbrCount.ToString() + "\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      #endregion

      #region BSBRCount5yrEX

      textBox1.AppendText("BSBR Count (5-yr EX)\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      foreach (string focusArea in rpReport.FocusAreaList())
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("StormEvent", new ReportBase.Parameter("StormEvent", "5"));
        parameters.Add("UseFlag", new ReportBase.Parameter("UseFlag", "14"));
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        bsbrCount = rpReport.BSBRCount(parameters);

        textBox1.AppendText(focusArea + " BSBR Count: " + bsbrCount.ToString() + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();

      parameters.Add("UseFlag", new ReportBase.Parameter("UseFlag", "14"));
      parameters.Add("StormEvent", new ReportBase.Parameter("StormEvent", "5"));
      parameters.Remove("FocusArea");
      bsbrCount = rpReport.BSBRCount(parameters);
      textBox1.AppendText("Total BSBR 5-yr EX: " + bsbrCount.ToString() + "\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      #endregion

      #region BSBRCount25yrEX

      textBox1.AppendText("BSBR Count (25-yr EX)\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      foreach (string focusArea in rpReport.FocusAreaList())
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("StormEvent", new ReportBase.Parameter("StormEvent", "25"));
        parameters.Add("UseFlag", new ReportBase.Parameter("UseFlag", "14"));
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        bsbrCount = rpReport.BSBRCount(parameters);

        textBox1.AppendText(focusArea + " BSBR Count: " + bsbrCount.ToString() + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();

      parameters.Add("UseFlag", new ReportBase.Parameter("UseFlag", "14"));
      parameters.Add("StormEvent", new ReportBase.Parameter("StormEvent", "25"));
      parameters.Remove("FocusArea");
      bsbrCount = rpReport.BSBRCount(parameters);
      textBox1.AppendText("Total BSBR 25-yr EX: " + bsbrCount.ToString() + "\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      #endregion

      #region BSBRCount25yrFU

      textBox1.AppendText("BSBR Count (25-yr FU)\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      foreach (string focusArea in rpReport.FocusAreaList())
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("StormEvent", new ReportBase.Parameter("StormEvent", "25"));
        parameters.Add("UseFlag", new ReportBase.Parameter("UseFlag", "15"));
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        bsbrCount = rpReport.BSBRCount(parameters);

        textBox1.AppendText(focusArea + " BSBR Count: " + bsbrCount.ToString() + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();

      parameters.Add("UseFlag", new ReportBase.Parameter("UseFlag", "15"));
      parameters.Add("StormEvent", new ReportBase.Parameter("StormEvent", "25"));
      parameters.Remove("FocusArea");
      bsbrCount = rpReport.BSBRCount(parameters);
      textBox1.AppendText("Total BSBR 25-yr FU: " + bsbrCount.ToString() + "\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      #endregion

      #region BSBRCount25yrRP

      textBox1.AppendText("BSBR Count (25-yr FU RP)\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      foreach (string focusArea in rpReport.FocusAreaList())
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("StormEvent", new ReportBase.Parameter("StormEvent", "25"));
        parameters.Add("UseFlag", new ReportBase.Parameter("UseFlag", "13"));
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        bsbrCount = rpReport.BSBRCount(parameters);

        textBox1.AppendText(focusArea + " BSBR Count: " + bsbrCount.ToString() + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();

      parameters.Add("UseFlag", new ReportBase.Parameter("UseFlag", "13"));
      parameters.Add("StormEvent", new ReportBase.Parameter("StormEvent", "25"));
      parameters.Remove("FocusArea");
      bsbrCount = rpReport.BSBRCount(parameters);
      textBox1.AppendText("Total BSBR 25-yr FU RP: " + bsbrCount.ToString() + "\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      #endregion

      #region BSBRDelta

      textBox1.AppendText("\r\n");
      textBox1.AppendText("BSBR Delta\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      
      int bsbrDelta = 0;
      foreach (string focusArea in rpReport.FocusAreaList())
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("StormEvent1", new ReportBase.Parameter("StormEvent1", "25"));
        parameters.Add("UseFlag1", new ReportBase.Parameter("UseFlag1", "15"));
        parameters.Add("StormEvent2", new ReportBase.Parameter("StormEvent2", "25"));
        parameters.Add("UseFlag2", new ReportBase.Parameter("UseFlag2", "13"));
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        bsbrDelta = rpReport.BSBRDelta(parameters);

        textBox1.AppendText(focusArea + " BSBR Delta: " + bsbrDelta.ToString() + "\r\n");
      }
      parameters.Remove("FocusArea");
      bsbrDelta = rpReport.BSBRDelta(parameters);
      textBox1.AppendText("Total BSBR Delta: " + bsbrDelta.ToString() + "\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      #endregion
    }

    private void btnCloseApplication_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

  }
}
