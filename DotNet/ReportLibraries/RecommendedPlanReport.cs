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
    private string swmmOutputFile;

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
        auxilaryDataDescription.Add("SwmmOutputFile", "Swmm Output File (*.out)");
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
      swmmOutputFile = AuxilaryData["SwmmOutputFile"].Value;
      
            
      try
      {                
        //Execute queries in StormwaterControls_v12
        //Load ic_target tables into StormwaterControlsDataSet
        scDS = new StormwaterControlsDataSet();
        scDS.InitStormwaterControlDataSet(modelPath);
        scDS.InitAltTargetDataTables(alternativePath);
        scDS.InitResultsDataTables(swmmOutputFile);
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


    public double StormwaterRemovalVolStreetStorage(IDictionary<string, Parameter> parameters)
    {
      var query =
        from tableE18 in scDS.TableE18
        join icNode in scDS.ICNode
        on tableE18.NodeName equals icNode.FacNode
        join icStreetTargets in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTargets.XPSWMM_Name
        join altStreetTargets in scDS.AltStreetTargets
        on icStreetTargets.icID equals altStreetTargets.ICID        
        group tableE18 by altStreetTargets.FocusArea into grpFocusArea
        orderby grpFocusArea.Key
        select new
        {
          FocusArea = grpFocusArea.Key,
          StreetStorage = grpFocusArea.Sum(p => p.StorageVolumeCuFt)
        };

      if (query.Count() > 1)
      {
        throw new Exception("StormwaterVol returned more than one row");
      }
      else if (query.Count() == 0)
      {
        return 0;
      }

      return query.First().StreetStorage;
    }

    public double StormwaterRemovalVolStreetInfiltration(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      return 0;
    }

    public double StormwaterRemovalVolRoofStorage(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      return 0;
    }

    public double StormwaterRemovalVolRoofInfiltration(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      return 0;
    }

    public double StormwaterRemovalVolParkStorage(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      return 0;
    }

    public double StormwaterRemovalVolParkInfiltration(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      return 0;
    }


    public double StormwaterRemovalVol(IDictionary<string, Parameter> parameters)
    {
      double volStreetStorage, volStreetInfiltration;
      double volRoofStorage, volRoofInfiltration;
      double volParkStorage, volParkInfiltration;

      volStreetStorage = StormwaterRemovalVolStreetStorage(parameters);
      volStreetInfiltration = StormwaterRemovalVolStreetInfiltration(parameters);

      volRoofStorage = StormwaterRemovalVolRoofStorage(parameters);
      volRoofInfiltration = StormwaterRemovalVolRoofInfiltration(parameters);

      volParkStorage = StormwaterRemovalVolParkStorage(parameters);
      volParkInfiltration = StormwaterRemovalVolParkInfiltration(parameters);

      return volStreetStorage + volStreetInfiltration + 
        volRoofStorage + volRoofInfiltration + 
        volParkStorage + volParkInfiltration;
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

      if (query.Count() > 1)
      {
        throw new Exception("InfiltratedArea returned more than one row");
      }
      else if (query.Count() == 0)
      {
        return 0;
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

      if (query.Count() > 1)
      {
        throw new Exception("HabitatArea returned more than one row");
      }
      else if (query.Count() == 0)
      {
        return 0;
      }

      return query.First().FacilityVolume;
    }

    


  }
}
