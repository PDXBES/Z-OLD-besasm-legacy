using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
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
    }

    private void LoadRPReport()
    {
      string testModelPath = txtTestModelPath.Text;
      string testAlternativePath = txtTestAltPath.Text;
      string testSwmmOutput = txtSwmmOutputPath.Text;
      rpReport = new RecommendedPlanReport();
      Dictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Add("ModelPath", new ReportBase.Parameter("ModelPath", testModelPath));
      parameters.Add("AlternativePath", new ReportBase.Parameter("AlternativePath", testAlternativePath));
      parameters.Add("SwmmOutputFile", new ReportBase.Parameter("SwmmOutputFile", testSwmmOutput));
      try
      {
        rpReport.LoadAuxilaryData(parameters);
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message);
      }
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      txtTestModelPath.Text = @"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\RP\Beech_Essex\BEE_RP\";
      txtTestAltPath.Text = @"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\RP\Beech_Essex\BEE_FU\alternatives\BEE_RP\";
      txtSwmmOutputPath.Text = @"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\RP\Beech_Essex\BEE_RP\sim\4S6\BEE_RP_4S6.out";
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

      #region InfiltrateStormwaterArea
      
      Dictionary<string, ReportBase.Parameter> parameters;
      double infiltratedArea = 0;
      textBox1.AppendText("Infiltrate Stormwater Area\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      List<string> focusAreaList = (List<string>)rpReport.FocusAreaList();
      //TODO: Need to add alt_links to this union query
      var query =
        (
          from altStreetTargets in rpReport.scDS.AltStreetTargets
          select altStreetTargets.FocusArea
        ).Union
        (
          from altRoofTargets in rpReport.scDS.AltRoofTargets
          select altRoofTargets.FocusArea
        ).Union
        (
          from altParkingTargets in rpReport.scDS.AltParkingTargets
          select altParkingTargets.FocusArea
        ).OrderBy(s => s);

      foreach (string focusArea in focusAreaList)
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        infiltratedArea = rpReport.InfiltrateStormwaterArea(parameters);

        textBox1.AppendText("Focus Area: " + focusArea + "; Infiltrated Area = " + Math.Round(infiltratedArea,1).ToString() + " acres" + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Remove("FocusArea");
      infiltratedArea = rpReport.InfiltrateStormwaterArea(parameters);
      textBox1.AppendText("Total Infiltrated Stormwater Area: " + Math.Round(infiltratedArea,1).ToString() + " acres" + "\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      
      #endregion

      #region ProtectImproveHabitat
      
      textBox1.AppendText("\r\n");
      textBox1.AppendText("Protect and Improve Terrestrial Habitat Area\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (string focusArea in focusAreaList)
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        infiltratedArea = rpReport.ProtectImproveTerrestrialHabitatArea(parameters);

        textBox1.AppendText("Focus Area: " + focusArea + "; Habitat Area = " + (Math.Round(infiltratedArea,2)).ToString() + " acres" + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Remove("FocusArea");
      infiltratedArea = rpReport.ProtectImproveTerrestrialHabitatArea(parameters);
      textBox1.AppendText("Total Protected/Improved Habitat Area: " + (Math.Round(infiltratedArea,2)).ToString() + " acres" + "\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      
      #endregion

      #region ParkBioStorageVolume

      double stormwaterRemovalVol = 0;
      textBox1.AppendText("\r\n");
      textBox1.AppendText("Park Bio Storage Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (string focusArea in focusAreaList)
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        stormwaterRemovalVol = rpReport.ParkBioStorageVolume(parameters);

        textBox1.AppendText("Focus Area: " + focusArea + "; Vol Remove: = " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Remove("FocusArea");
      stormwaterRemovalVol = rpReport.ParkBioStorageVolume(parameters);
      textBox1.AppendText("Total Park Bio Storage Vol Removed: " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");

      #endregion

      #region ParkBioInfiltrationVolume

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Park Bio Infiltration Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (string focusArea in focusAreaList)
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea",focusArea));
        stormwaterRemovalVol = rpReport.ParkBioInfiltrationVolume(parameters);

        textBox1.AppendText("Focus Area: " + focusArea + "; Vol Remove: = " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Remove("FocusArea");
      stormwaterRemovalVol = rpReport.ParkBioInfiltrationVolume(parameters);
      textBox1.AppendText("Total Park Bio Infiltration Vol Removed: " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");

      #endregion
      
      #region RoofPlanterStorageVolume
      
      textBox1.AppendText("\r\n");
      textBox1.AppendText("Roof Planter Storage Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (string focusArea in focusAreaList)
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        stormwaterRemovalVol = rpReport.RoofPlanterStorageVolume(parameters);

        textBox1.AppendText("Focus Area: " + focusArea + "; Vol Remove: = " + (Math.Round(stormwaterRemovalVol)).ToString() + " gallons" + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Remove("FocusArea");
      stormwaterRemovalVol = rpReport.RoofPlanterStorageVolume(parameters);
      textBox1.AppendText("Total Roof Planter Storage Volume Removed: " + (Math.Round(stormwaterRemovalVol)).ToString() + " gallons" + "\r\n");
      
      #endregion

      #region RoofPlanterInfiltrationVolume

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Roof Planter Infiltration Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (string focusArea in focusAreaList)
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        stormwaterRemovalVol = rpReport.RoofPlanterInfiltrationVolume(parameters);
        textBox1.AppendText("Focus Area: " + focusArea + "; Vol Remove: = " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");
      }
      parameters = new Dictionary<string,ReportBase.Parameter>();
      parameters.Remove("FocusArea");
      stormwaterRemovalVol = rpReport.RoofPlanterInfiltrationVolume(parameters);
      textBox1.AppendText("Total Roof Planter Infiltration Volume: " + Math.Round(stormwaterRemovalVol).ToString()+ " gallons" + "\r\n");
      
      #endregion

      #region RoofBioStorageVolume

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Roof Bio Storage Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (string focusArea in focusAreaList)
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        stormwaterRemovalVol = rpReport.RoofBioStorageVolume(parameters);

        textBox1.AppendText("Focus Area: " + focusArea + "; Vol Remove: = " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Remove("FocusArea");
      stormwaterRemovalVol = rpReport.RoofBioStorageVolume(parameters);
      textBox1.AppendText("Total Roof Bio Storage Volume: " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");

      #endregion

      #region RoofBioInfiltrationVolume

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Roof Bio Infiltration Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (string focusArea in focusAreaList)
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        stormwaterRemovalVol = rpReport.RoofBioInfiltrationVolume(parameters);
        textBox1.AppendText("Focus Area: " + focusArea + "; Vol Remove: = " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Remove("FocusArea");
      stormwaterRemovalVol = rpReport.RoofBioInfiltrationVolume(parameters);
      textBox1.AppendText("Total Roof Bio Infiltration Volume: " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");

      #endregion

      #region StreetStorageVolume

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Street Storage Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (string focusArea in focusAreaList)
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        stormwaterRemovalVol = rpReport.StormwaterRemovalVolStreetStorage(parameters);

        textBox1.AppendText("Focus Area: " + focusArea + "; Vol Remove: = " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Remove("FocusArea");
      stormwaterRemovalVol = rpReport.StormwaterRemovalVolStreetStorage(parameters);
      textBox1.AppendText("Total Street Storage Volume: " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");

      #endregion

      #region StreetInfiltrationVolume

      textBox1.AppendText("\r\n");
      textBox1.AppendText("Street Infiltration Volume\r\n");
      textBox1.AppendText("------------------------------------\r\n");
      foreach (string focusArea in focusAreaList)
      {
        parameters = new Dictionary<string, ReportBase.Parameter>();
        parameters.Add("FocusArea", new ReportBase.Parameter("FocusArea", focusArea));
        stormwaterRemovalVol = rpReport.StormwaterRemovalVolStreetInfiltration(parameters);

        textBox1.AppendText("Focus Area: " + focusArea + "; Vol Remove: = " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");
      }
      parameters = new Dictionary<string, ReportBase.Parameter>();
      parameters.Remove("FocusArea");
      stormwaterRemovalVol = rpReport.StormwaterRemovalVolStreetInfiltration(parameters);
      textBox1.AppendText("Total Street Infiltration Volume: " + Math.Round(stormwaterRemovalVol).ToString() + " gallons" + "\r\n");

      #endregion

    }

    private void btnBSBRTest_Click(object sender, EventArgs e)
    {
      textBox1.Clear();

      #region BSBRCount2yrEX

      textBox1.AppendText("BSBR Count (2-yr EX)\r\n");
      textBox1.AppendText("------------------------------------\r\n");

      Dictionary<string, ReportBase.Parameter> parameters;
      int bsbrCount = 0;
      if (rpReport == null)
      {
        MessageBox.Show("Error: Load RPReport before executing BSBR queries");
        return;
      }
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

    private void btnLoadE18Data_Click(object sender, EventArgs e)
    {
      dgvTableE18.DataSource = rpReport.scDS.TableE18;
    }

    private void btnLoadE19Data_Click(object sender, EventArgs e)
    {
      dgvTableE19.DataSource = rpReport.scDS.TableE19;
    }

    private void btnChooseTestModelPath_Click(object sender, EventArgs e)
    {
      if (ofdModelDataSource.ShowDialog() == DialogResult.OK)
      {
        string fullModelPath = ofdModelDataSource.FileName;
        txtTestModelPath.Text = Path.GetDirectoryName(fullModelPath) + "\\";
        string testModelPath = txtTestModelPath.Text;
      }
    }

    private void btnChooseTestAltPath_Click(object sender, EventArgs e)
    {
      if (ofdModelDataSource.ShowDialog() == DialogResult.OK)
      {
        string fullAltPath = ofdModelDataSource.FileName;
        txtTestAltPath.Text = Path.GetDirectoryName(fullAltPath) + "\\";
        string testAlternativePath = txtTestAltPath.Text;  
      } 
    }

    private void btnChooseTestSwmmOutput_Click(object sender, EventArgs e)
    {
      if (ofdModelDataSource.ShowDialog() == DialogResult.OK)
      {
        string fullOutputPath = ofdModelDataSource.FileName;
        txtSwmmOutputPath.Text = fullOutputPath;
        string testSwmmOutput = fullOutputPath;
      }
    }

    private void btnLoadRpReport_Click(object sender, EventArgs e)
    {
      LoadRPReport();
    }

    private void btnClearTextBox_Click(object sender, EventArgs e)
    {
      textBox1.Clear();
    }

  }
}
