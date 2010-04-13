using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.Modeling;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Linq;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
  public class RecommendedPlanReport : ReportBase
  {
    private StormwaterControlsDataSet stormwaterControlDS;
    private AltCompilerDataSet altCompilerDS;
    private string modelPath;

    public RecommendedPlanReport(Links links, Nodes nodes, Dscs dscs)
    {
      DataAccess.AltCompilerDataSetTableAdapters.SP_ALT_BSBRTableAdapter altBSBRTA;
      altBSBRTA = new SystemsAnalysis.DataAccess.AltCompilerDataSetTableAdapters.SP_ALT_BSBRTableAdapter();
      //TODO: alt_BSBR table adapter should probably require UseFlag (and possibly Storm or other fields)
      //as query parameters to save memory and reduce load times from SirToby
      //altBSBRTA.Fill(altCompilerDS.SP_ALT_BSBR);
    }

    public new class ReportInfo : ReportBase.ReportInfo
    {
      private Dictionary<string, Parameter> auxilaryData;
      private Dictionary<string, string> auxilaryDataDescription;
      public ReportInfo(string reportName)
        : base(reportName)
      {
        SetAuxilaryDataDescription();
        auxilaryData = new Dictionary<string, Parameter>();
      }
      public override Dictionary<string, string> AuxilaryDataDescription
      {
        get
        {
          return auxilaryDataDescription;
        }
      }

      private void SetAuxilaryDataDescription()
      {
        auxilaryDataDescription = new Dictionary<string, string>();
        auxilaryDataDescription.Add("ModelPath", "Model Directory (Model.ini)");
      }
      public override bool RequiresAuxilaryData
      {
        get
        {
          return true;
        }
      }
      public override Dictionary<string, Parameter> AuxilaryData
      {
        get
        {
          return auxilaryData;
        }
        set
        {
          auxilaryData = value;
        }
      }
    }

    public override void LoadAuxilaryData(Dictionary<string, Parameter> AuxilaryData)
    {
      modelPath = AuxilaryData["ModelPath"].Value;
      modelPath = System.IO.Path.GetDirectoryName(modelPath);

      string stormwaterControlsDB = modelPath + @"\mdbs\StormwaterControls_v12.mdb";
      //accessHelper = new AccessHelper(stormwaterControlsDB);
      try
      {                
        //Execute queries in StormwaterControls_v12
        //Load ic_target tables into StormwaterControlsDataSet
        stormwaterControlDS = new StormwaterControlsDataSet();
        stormwaterControlDS.InitStormwaterControlDataSet(modelPath);
        IQueryable q = (IQueryable)stormwaterControlDS;
      }
      catch (Exception ex)
      {
        throw new Exception("Could not read auxilary data 'ModelPath': " + ex.Message, ex);
      }
      finally
      {
        //accessHelper.Dispose();
      }
    }

    public double BSBRCount(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      string useFlag;
      string stormEvent;

      useFlag = parameters["UseFlag"].Value;
      stormEvent = parameters["StormEvent"].Value;
      focusArea = parameters["FocusArea"].Value;

      return 0;
    }

    public double VolumeStormwaterInfiltrated(IDictionary<string, Parameter> parameters)
    {
      string focusArea;

      focusArea = parameters["FocusArea"].Value;

      return 0;
    }

    public double InfiltratedArea(IDictionary<string, Parameter> parameters)
    {
      string focusArea;

      focusArea = parameters["FocusArea"].Value;

      return 0;
    }


  }
}
