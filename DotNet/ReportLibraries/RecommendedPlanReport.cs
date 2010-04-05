using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
  public class RecommendedPlanReport : ReportBase
  {
    private StormwaterControlsDataSet stormwaterControlDS;
    private AccessHelper accessHelper;
    private string modelPath;

    public RecommendedPlanReport(Links links, Nodes nodes, Dscs dscs)
    {
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
      try
      {        
        modelPath = AuxilaryData["ModelPath"].Value;
        modelPath = System.IO.Path.GetDirectoryName(modelPath);        
        accessHelper = new AccessHelper(modelPath + @"mdbs\StormwaterControls_v12.mdb");
        
        //Execute queries in StormwaterControls_v12
        //Load ic_target tables into StormwaterControlsDataSet
        stormwaterControlDS = new StormwaterControlsDataSet();        
        stormwaterControlDS.InitStormwaterControlDataSet(modelPath);        
      }
      catch (Exception ex)
      {
        throw new Exception("Could not read auxilary data 'ModelPath': " + ex.Message, ex);
      }
    }

    public double BSBRCount(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      string useFlag;
      string stormEvent;

      useFlag = parameters["USEFLAG"].Value;
      stormEvent = parameters["STORMEVENT"].Value;
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
