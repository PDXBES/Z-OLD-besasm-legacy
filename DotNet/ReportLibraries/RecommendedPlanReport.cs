using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.Modeling;
using System.Data.Linq;
using System.IO;
using System.Linq.Expressions;
using System.Linq;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
  public class RecommendedPlanReport : ReportBase
  {
    private StormwaterControlsDataSet scDS;
    private AltCompilerDataSet altCompilerDS;
    private string modelPath;
    private string alternativePath;

    public RecommendedPlanReport()
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
        auxilaryDataDescription.Add("AlternativePath", "Alternative Package (alternative_package.mdb)");
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
      //modelPath = Path.GetDirectoryName(modelPath);
      alternativePath = AuxilaryData["AlternativePath"].Value;
      //alternativePath = Path.GetDirectoryName(alternativePath);
            
      try
      {                
        //Execute queries in StormwaterControls_v12
        //Load ic_target tables into StormwaterControlsDataSet
        scDS = new StormwaterControlsDataSet();
        scDS.InitStormwaterControlDataSet(modelPath);
        scDS.InitAltTargetDataTables(alternativePath);        
      }
      catch (Exception ex)
      {
        throw new Exception("Could not read auxilary data 'ModelPath': " + ex.Message, ex);
      }
      finally
      {
        
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

    public double InfiltrateStormwaterArea(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      var query =
        from icNode in scDS.ICNode
        join icStreetTarget in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTarget.XPSWMM_Name
        join altStreetTarget in scDS.AltStreetTargets
        on icStreetTarget.icID equals altStreetTarget.ICID
        join mdlSurfSc in scDS.mdl_SurfSC_ac
        on icStreetTarget.surfSCID equals mdlSurfSc.SurfSCID        
        where altStreetTarget.FocusArea == focusArea
        group mdlSurfSc by altStreetTarget.FocusArea into grpFocusArea
        orderby grpFocusArea.Key
        select new
        {
          FocusArea = grpFocusArea.Key,
          NetIA = grpFocusArea.Sum(p => p.c_netIMPacres)
        };

      if (query.Count() != 1)
      {
        throw new Exception("InfiltratedArea returned more than one row");
      }

      return query.First().NetIA;
      
    }

    public double ReducePollutantsArea(IDictionary<string, Parameter> parameters)
    {
      return InfiltrateStormwaterArea(parameters);
    }

    public double ProtectAndImproveTerrestrialHabitialArea(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      var query =
        from icNode in scDS.ICNode
        join icStreetTarget in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTarget.XPSWMM_Name
        join altStreetTarget in scDS.AltStreetTargets
        on icStreetTarget.icID equals altStreetTarget.ICID
        where icNode.Factype == "C" && icStreetTarget.constructed == 0 && altStreetTarget.FocusArea == focusArea
        group icNode by altStreetTarget.FocusArea into grpFocusArea
        orderby grpFocusArea.Key
        select new
        {
          FocusArea = grpFocusArea.Key,
          FacilityVolume = grpFocusArea.Sum(p => p.FacVolCuFt) / 0.75 / 43560,
        };

      if (query.Count() != 1)
      {
        throw new Exception("HabitatArea returned more than one row");
      }

      return query.First().FacilityVolume;
    }

    


  }
}
