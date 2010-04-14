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
    public Form1()
    {
      InitializeComponent();

      SystemsAnalysis.DataAccess.StormwaterControlsDataSet scDS;
      scDS = new StormwaterControlsDataSet();

      scDS.InitStormwaterControlDataSet(@"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\Alts\Beech_Essex\BEE_NP-Pipe");

      SystemsAnalysis.DataAccess.AlternativeDataSet altDS;
      altDS = new AlternativeDataSet();

      altDS.InitAlternativeDataSet(@"\\Cassio\systems_planning\8063_CombinedFacPlan\Models\Alts\Beech_Essex\BEE_NP_Base\alternatives\BEE_NP_NP-Pipe");

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

      //workingDS.Tables[0].s
      //workingDS.Tables.Add(new      
    }
  }
}
