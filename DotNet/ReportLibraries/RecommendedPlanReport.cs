﻿using System;
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
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      var query =
        from tableE18 in scDS.TableE18
        join icNode in scDS.ICNode
        on tableE18.NodeName equals icNode.FacNode
        join icStreetTargets in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTargets.XPSWMM_Name
        join altStreetTargets in scDS.AltStreetTargets
        on icStreetTargets.icID equals altStreetTargets.ICID
        where altStreetTargets.FocusArea == focusArea
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

      var query =
        from tableE19 in scDS.TableE19
        join icNode in scDS.ICNode
        on tableE19.NodeName equals icNode.OutfallNode
        join icStreetTargets in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTargets.XPSWMM_Name
        join altStreetTargets in scDS.AltStreetTargets
        on icStreetTargets.icID equals altStreetTargets.ICID
        where altStreetTargets.FocusArea == focusArea
        group tableE19 by altStreetTargets.FocusArea into grpFocusArea
        orderby grpFocusArea.Key
        select new
        {
          FocusArea = grpFocusArea.Key,
          InfiltrationVolume = grpFocusArea.Sum(p => p.InfiltrationVolumeCuFt)
        };

      if (query.Count() > 1)
      {
        throw new Exception("StormwaterVol returned more than one row");
      }
      else if (query.Count() == 0)
      {
        return 0;
      }

      return query.First().InfiltrationVolume;
    }

    public double RoofPlanterStorageVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;
      //Roof Planter Storage - Grouped By Node Name
      var qryRoofTargetsPlantGroupByFacNodeName =
        from tableE18 in scDS.TableE18
        join icNode in scDS.ICNode
        on tableE18.NodeName equals icNode.FacNode
        join altRoofTargets in scDS.AltRoofTargets
        on icNode.RefNode equals altRoofTargets.NGToRoof
        where (icNode.Factype == "P" && altRoofTargets.BuildModelIC && altRoofTargets.Constructed == 0)
        group tableE18 by new { altRoofTargets.FocusArea, tableE18.NodeName } into grpNodeName
        select new
        {
          FocusArea = grpNodeName.Key.FocusArea,
          NodeName = grpNodeName.Key.NodeName
        };

      //Roof Planter Storage - Filtered and grouped by Focus Area
      var qryRoofTargetsPlant =
        from tableE18 in scDS.TableE18
        join query1 in qryRoofTargetsPlantGroupByFacNodeName
        on tableE18.NodeName equals query1.NodeName
        where query1.FocusArea == focusArea
        group tableE18 by query1.FocusArea into grpFocusArea
        select new
        {
          FocusArea = grpFocusArea.Key,
          StorageVolume = grpFocusArea.Sum(p => p.StorageVolumeCuFt)
        };

      if (qryRoofTargetsPlant.Count() > 1)
      {
        throw new Exception("RoofPlanterStorageVol returned more than one row");
      }

      double roofTargetsPlant = 0;
      if (qryRoofTargetsPlant.Count() != 0)
      {
        roofTargetsPlant = qryRoofTargetsPlant.First().StorageVolume;
      }

      return roofTargetsPlant;
    }
    public double RoofPlanterInfiltrationVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;
      //Roof Planter Storage - Grouped By Node Name
      var qryRoofTargetsPlantGroupByFacNodeName =
        from tableE19 in scDS.TableE19
        join icNode in scDS.ICNode
        on tableE19.NodeName equals icNode.OutfallNode
        join altRoofTargets in scDS.AltRoofTargets
        on icNode.RefNode equals altRoofTargets.NGToRoof
        where (icNode.Factype == "P" && altRoofTargets.BuildModelIC && altRoofTargets.Constructed == 0)
        group tableE19 by new { altRoofTargets.FocusArea, tableE19.NodeName } into grpNodeName
        select new
        {
          FocusArea = grpNodeName.Key.FocusArea,
          NodeName = grpNodeName.Key.NodeName
        };

      var qryRoofTargetsPlant =
        from tableE19 in scDS.TableE19
        join query1 in qryRoofTargetsPlantGroupByFacNodeName
        on tableE19.NodeName equals query1.NodeName
        where query1.FocusArea == focusArea
        group tableE19 by query1.FocusArea into grpFocusArea
        select new
        {
          FocusArea = grpFocusArea.Key,
          InfiltrationVolume = grpFocusArea.Sum(p => p.InfiltrationVolumeCuFt)
        };

      double roofTargetsPlant = 0;
      if (qryRoofTargetsPlant.Count() != 0)
      {
        roofTargetsPlant = qryRoofTargetsPlant.First().InfiltrationVolume;
      }
      return roofTargetsPlant;
    }

    public double RoofBioStorageVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      //Parking Bioinfiltration Storage - Grouped by Node Name
      var qryParkTargetsBioGroupByFacNodeName =
        from tableE18 in scDS.TableE18
        join icNode in scDS.ICNode
        on tableE18.NodeName equals icNode.FacNode
        join altParkingTargets in scDS.AltParkingTargets
        on icNode.RefNode equals altParkingTargets.NGTo
        where icNode.Factype == "B" && altParkingTargets.BuildModelIC && altParkingTargets.Constructed == 0
        group tableE18 by new { altParkingTargets.FocusArea, tableE18.NodeName } into grpNodeName
        select new
        {
          FocusArea = grpNodeName.Key.FocusArea,
          NodeName = grpNodeName.Key.NodeName
        };

      //Roof Bioinfiltration Storage - Grouped by Node Name
      var qryRoofTargetsBioGroupByFacNodeName =
        from tableE18 in scDS.TableE18
        join icNode in scDS.ICNode
        on tableE18.NodeName equals icNode.OutfallNode
        join altRoofTargets in scDS.AltRoofTargets
        on icNode.RefNode equals altRoofTargets.NGToRoof
        where icNode.Factype == "B" && altRoofTargets.BuildModelIC && altRoofTargets.Constructed == 0
        group tableE18 by new { altRoofTargets.FocusArea, tableE18.NodeName } into grpNodeName
        select new
        {
          FocusArea = grpNodeName.Key.FocusArea,
          NodeName = grpNodeName.Key.NodeName
        };

      //Roof Bioinfiltration Storage - Filtered and grouped by Focus Area
      var qryRoofTargetsBio =
        from roof in qryRoofTargetsBioGroupByFacNodeName
        join park in qryParkTargetsBioGroupByFacNodeName
        on roof.NodeName equals park.NodeName into parkJoin        
        from parkSub in parkJoin.DefaultIfEmpty()        
        join tableE18 in scDS.TableE18
        on roof.NodeName equals tableE18.NodeName into e18Join
        from e18Sub in e18Join.DefaultIfEmpty()
        where roof.FocusArea == focusArea && parkSub == null
        group e18Sub by roof.FocusArea into grpFocusArea
        select new
        {
          FocusArea = grpFocusArea.Key,
          StorageVolume = grpFocusArea.Sum(p => p.StorageVolumeCuFt)

        };

      if (qryRoofTargetsBio.Count() > 1)
      {
        throw new Exception("RoofBioStorageVol returned more than one row");
      }

      double roofTargetsBio = 0;
      if (qryRoofTargetsBio.Count() != 0)
      {
        roofTargetsBio = qryRoofTargetsBio.First().StorageVolume;
      }

      return roofTargetsBio;
    }
    public double RoofBioInfiltrationVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      //Parking Bioinfiltration Infiltration - Grouped by Node Name
      var qryParkTargetsBioGroupByOutfallName =
        from tableE19 in scDS.TableE19
        join icNode in scDS.ICNode
        on tableE19.NodeName equals icNode.OutfallNode
        join altParkingTargets in scDS.AltParkingTargets
        on icNode.RefNode equals altParkingTargets.NGTo
        where icNode.Factype == "B" && altParkingTargets.BuildModelIC && altParkingTargets.Constructed == 0
        group tableE19 by new { altParkingTargets.FocusArea, tableE19.NodeName } into grpNodeName
        select new
        {
          FocusArea = grpNodeName.Key.FocusArea,
          NodeName = grpNodeName.Key.NodeName
        };

      //Roof Bioinfiltration Infiltration - Grouped by Node Name
      var qryRoofTargetsBioGroupByOutfallName =
        from tableE19 in scDS.TableE19
        join icNode in scDS.ICNode
        on tableE19.NodeName equals icNode.OutfallNode
        join altRoofTargets in scDS.AltRoofTargets
        on icNode.RefNode equals altRoofTargets.NGToRoof
        where icNode.Factype == "B" && altRoofTargets.BuildModelIC && altRoofTargets.Constructed == 0
        group tableE19 by new { altRoofTargets.FocusArea, tableE19.NodeName } into grpNodeName
        select new
        {
          FocusArea = grpNodeName.Key.FocusArea,
          NodeName = grpNodeName.Key.NodeName
        };

      //Roof Bioinfiltration Infiltration - Filtered and grouped by Focus Area
      var qryRoofTargetsBio =
        from roof in qryRoofTargetsBioGroupByOutfallName
        join park in qryParkTargetsBioGroupByOutfallName
        on roof.NodeName equals park.NodeName into parkJoin
        from parkSub in parkJoin.DefaultIfEmpty()
        join tableE19 in scDS.TableE19
        on roof.NodeName equals tableE19.NodeName into E19Join
        from E19Sub in E19Join.DefaultIfEmpty()
        where roof.FocusArea == focusArea && parkSub == null
        group E19Sub by roof.FocusArea into grpFocusArea
        select new
        {
          FocusArea = grpFocusArea.Key,
          InfiltrationVolume = grpFocusArea.Sum(p => p.InfiltrationVolumeCuFt)

        };

      if (qryRoofTargetsBio.Count() > 1)
      {
        throw new Exception("RoofBioInfiltrationVol returned more than one row");
      }

      double roofTargetsBio = 0;
      if (qryRoofTargetsBio.Count() != 0)
      {
        roofTargetsBio = qryRoofTargetsBio.First().InfiltrationVolume;
      }

      return roofTargetsBio;
    }

    public double ParkPlanterStorageVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;
      //Roof Planter Storage - Grouped By Node Name
      var qryRoofTargetsPlantGroupByFacNodeName =
        from tableE18 in scDS.TableE18
        join icNode in scDS.ICNode
        on tableE18.NodeName equals icNode.FacNode
        join altRoofTargets in scDS.AltRoofTargets
        on icNode.RefNode equals altRoofTargets.NGToRoof
        where (icNode.Factype == "P" && altRoofTargets.BuildModelIC && altRoofTargets.Constructed == 0)
        group tableE18 by new { altRoofTargets.FocusArea, tableE18.NodeName } into grpNodeName
        select new
        {
          FocusArea = grpNodeName.Key.FocusArea,
          NodeName = grpNodeName.Key.NodeName
        };

      //Roof Planter Storage - Filtered and grouped by Focus Area
      var qryRoofTargetsPlant =
        from tableE18 in scDS.TableE18
        join query1 in qryRoofTargetsPlantGroupByFacNodeName
        on tableE18.NodeName equals query1.NodeName
        where query1.FocusArea == focusArea
        group tableE18 by query1.FocusArea into grpFocusArea
        select new
        {
          FocusArea = grpFocusArea.Key,
          StorageVolume = grpFocusArea.Sum(p => p.StorageVolumeCuFt)
        };

      if (qryRoofTargetsPlant.Count() > 1)
      {
        throw new Exception("RoofPlanterStorageVol returned more than one row");
      }

      double roofTargetsPlant = 0;
      if (qryRoofTargetsPlant.Count() != 0)
      {
        roofTargetsPlant = qryRoofTargetsPlant.First().StorageVolume;
      }

      return roofTargetsPlant;
    }
    public double ParkPlanterInfiltrationVolume(IDictionary<string, Parameter> parameters)
    {
      return 0;
    }

    public double ParkBioStorageVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      focusArea = parameters["FocusArea"].Value;

      //Parking Bioinfiltration Storage - Grouped by Node Name
      var qryParkTargetsBioGroupByFacNodeName =
        from tableE18 in scDS.TableE18
        join icNode in scDS.ICNode
        on tableE18.NodeName equals icNode.FacNode
        join altParkingTargets in scDS.AltParkingTargets
        on icNode.RefNode equals altParkingTargets.NGTo
        where icNode.Factype == "B" && altParkingTargets.BuildModelIC && altParkingTargets.Constructed == 0
        group tableE18 by new { altParkingTargets.FocusArea, tableE18.NodeName } into grpNodeName
        select new
        {
          FocusArea = grpNodeName.Key.FocusArea,
          NodeName = grpNodeName.Key.NodeName
        };

      //Roof Bioinfiltration Storage - Grouped by Node Name
      var qryRoofTargetsBioGroupByFacNodeName =
        from tableE18 in scDS.TableE18
        join icNode in scDS.ICNode
        on tableE18.NodeName equals icNode.OutfallNode
        join altRoofTargets in scDS.AltRoofTargets
        on icNode.RefNode equals altRoofTargets.NGToRoof
        where icNode.FacNode == "B" && altRoofTargets.BuildModelIC && altRoofTargets.Constructed == 0
        group tableE18 by new { altRoofTargets.FocusArea, tableE18.NodeName } into grpNodeName
        select new
        {
          FocusArea = grpNodeName.Key.FocusArea,
          NodeName = grpNodeName.Key.NodeName
        };

      //Roof Bioinfiltration Storage - Filtered and grouped by Focus Area
      var qryRoofTargetsBio =
        from query1 in qryParkTargetsBioGroupByFacNodeName
        join query2 in qryRoofTargetsBioGroupByFacNodeName
        on query1.NodeName equals query2.NodeName
        join tableE18 in scDS.TableE18
        on query2.NodeName equals tableE18.NodeName
        where query2.FocusArea == focusArea
        group tableE18 by query1.FocusArea into grpFocusArea
        select new
        {
          FocusArea = grpFocusArea.Key,
          StorageVolume = grpFocusArea.Sum(p => p.StorageVolumeCuFt)
        };

      if (qryRoofTargetsBio.Count() > 1)
      {
        throw new Exception("RoofPlanterInfiltrationVol returned more than one row");
      }

      double roofTargetsBio = 0;
      if (qryRoofTargetsBio.Count() != 0)
      {
        roofTargetsBio = qryRoofTargetsBio.First().StorageVolume;
      }

      return roofTargetsBio;
    }
    public double ParkBioInfiltrationVolume(IDictionary<string, Parameter> parameters)
    {
      return 0;
    }
    
    public double StormwaterRemovalVol(IDictionary<string, Parameter> parameters)
    {
      double volStreetStorage, volStreetInfiltration;
      double volRoofPlantStorage, volRoofPlantInfiltration;
      double volRoofBioStorage, volRoofBioInfiltration;

      double volParkPlantStorage, volParkPlantInfiltration;
      double volParkBioStorage, volParkBioInfiltration;

      volStreetStorage = StormwaterRemovalVolStreetStorage(parameters);
      volStreetInfiltration = StormwaterRemovalVolStreetInfiltration(parameters);

      volRoofPlantStorage = RoofPlanterStorageVolume(parameters);
      volRoofPlantInfiltration = RoofPlanterInfiltrationVolume(parameters);

      volRoofBioStorage = RoofBioStorageVolume(parameters);
      volRoofBioInfiltration = RoofBioInfiltrationVolume(parameters);

      volParkPlantStorage = ParkPlanterStorageVolume(parameters);
      volParkPlantInfiltration = ParkPlanterInfiltrationVolume(parameters);

      volParkBioStorage = ParkBioStorageVolume(parameters);
      volParkBioInfiltration = ParkBioInfiltrationVolume(parameters);

      return volStreetStorage + volStreetInfiltration +
        volRoofPlantStorage + volRoofPlantInfiltration +
        volRoofBioStorage + volRoofBioInfiltration +
        volParkPlantStorage + volParkPlantInfiltration +
        volParkBioStorage + volParkBioInfiltration;

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
