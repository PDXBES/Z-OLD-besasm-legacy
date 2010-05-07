using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.ModelConstruction;
using System.Data.Linq;
using System.IO;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Linq;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
  public class RecommendedPlanReport : AlternativeReport //ReportBase
  {
    const double SQ_FT_PER_ACRE = 43560;
    const double GAL_PER_FT_3 = 7.48;
    const double CREDIT_PER_GALLON_STORMWATER_REMOVAL = 3;

    private StormwaterControlsDataSet scDS;
    public AltCompilerDataSet altCompilerDS;
    private string modelPath;
    private string alternativePath;
    private string swmmOutputFile;
    private AlternativeReport alternativeReport;

    public RecommendedPlanReport()
    {
      altCompilerDS = new AltCompilerDataSet();
      try
      {
        altCompilerDS.InitAltCompilerDataSet();
      }
      catch (Exception ex)
      {
      }
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
        //auxilaryDataDescription.Add("AlternativePath", "Alternative Package (alternative_package.mdb)");
        auxilaryDataDescription.Add("SwmmOutputFile", "Swmm Output File (.out)");
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
      if (modelPath.ToLower().EndsWith("model.ini"))
      {
        modelPath = Path.GetDirectoryName(modelPath);
      }

      ModelConfigurationDataSet modelConfigDS = new ModelConfigurationDataSet(Path.GetFullPath(modelPath));
      ModelConfigurationDataSet.AlternativeRow[] altRow = modelConfigDS.GetIncludedAlternatives();
      alternativePath = altRow[0].BaseModel + "alternatives\\" + altRow[0].Name;

      if (!AuxilaryData.ContainsKey("AlternativePath"))
      {
        AuxilaryData.Add("AlternativePath", new Parameter("AlternativePath", alternativePath));
      }
      base.LoadAuxilaryData(AuxilaryData);
      //alternativePath = AuxilaryData["AlternativePath"].Value;
      //alternativeReport = new AlternativeReport();
      //alternativeReport.LoadAuxilaryData(AuxilaryData);

      swmmOutputFile = AuxilaryData["SwmmOutputFile"].Value;

      try
      {
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

    public int BSBRCount(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      IList<string> focusAreaList = (IList<string>)FocusAreaList();
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";
      if (!focusAreaList.Contains(focusArea))
      {
        return 0;
      }

      int useFlag;
      string stormEvent;
      useFlag = parameters["UseFlag"].ValueAsInt;
      stormEvent = parameters["StormEvent"].Value;

      //TO-DO: continue implementation of BsbrCount Linq queries

      var qryCountBsbr =
        from tableSpRpBsbr in altCompilerDS.SP_RP_BSBR
        join faList in focusAreaList
        on tableSpRpBsbr.focus_area equals faList
        where (tableSpRpBsbr.useflag == useFlag)
          && (tableSpRpBsbr.storm == stormEvent)          
        group tableSpRpBsbr by tableSpRpBsbr.focus_area into gFocusArea
        orderby gFocusArea.Key
        select new
        {
          FocusArea = gFocusArea.Key,
          BsbrCount = gFocusArea.Count()
        };

      int bsbrCount = qryCountBsbr.Where(
        p => !filteredByFocusArea
          || p.FocusArea == focusArea).Sum(p => p.BsbrCount); //qryCountBsbr.;// qryCountBsbr.BsbrCount;

      return bsbrCount;
    }
    public int BSBRDelta(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

      int useFlag1;
      string stormEvent1;
      useFlag1 = parameters["UseFlag1"].ValueAsInt;
      stormEvent1 = parameters["StormEvent1"].Value;

      int useFlag2;
      string stormEvent2;
      useFlag2 = parameters["UseFlag2"].ValueAsInt;
      stormEvent2 = parameters["StormEvent2"].Value;

      int bsbrCount1;
      IDictionary<string, Parameter> parameters1 = new Dictionary<string, Parameter>();
      if (filteredByFocusArea) { parameters1.Add("FocusArea", parameters["FocusArea"]); }
      parameters1.Add("UseFlag", parameters["UseFlag1"]);
      parameters1.Add("StormEvent", parameters["StormEvent1"]);
      bsbrCount1 = BSBRCount(parameters1);

      int bsbrCount2;
      IDictionary<string, Parameter> parameters2 = new Dictionary<string, Parameter>();
      if (filteredByFocusArea) { parameters2.Add("FocusArea", parameters["FocusArea"]); }
      parameters2.Add("UseFlag", parameters["UseFlag2"]);
      parameters2.Add("StormEvent", parameters["StormEvent2"]);
      bsbrCount2 = BSBRCount(parameters2);

      return bsbrCount1 - bsbrCount2;
    }


    public double StormwaterRemovalVolStreetStorage(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

      var query =
        from tableE18 in scDS.TableE18
        join icNode in scDS.ICNode
        on tableE18.NodeName equals icNode.FacNode
        join icStreetTargets in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTargets.XPSWMM_Name
        join altStreetTargets in scDS.AltStreetTargets
        on icStreetTargets.icID equals altStreetTargets.ICID
        where !filteredByFocusArea || altStreetTargets.FocusArea == focusArea
        group tableE18 by altStreetTargets.FocusArea into gFocusArea
        orderby gFocusArea.Key
        select new
        {
          FocusArea = gFocusArea.Key,
          StreetStorage = gFocusArea.Sum(p => p.StorageVolumeCuFt) * GAL_PER_FT_3
        };

      if (query.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("StormwaterVol returned more than one row");
      }
      else if (query.Count() == 0)
      {
        return 0;
      }

      return query.Sum(p => p.StreetStorage);
    }
    public double StormwaterRemovalVolStreetInfiltration(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

      var query =
        from tableE19 in scDS.TableE19
        join icNode in scDS.ICNode
        on tableE19.NodeName equals icNode.OutfallNode
        join icStreetTargets in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTargets.XPSWMM_Name
        join altStreetTargets in scDS.AltStreetTargets
        on icStreetTargets.icID equals altStreetTargets.ICID
        where !filteredByFocusArea || altStreetTargets.FocusArea == focusArea
        group tableE19 by altStreetTargets.FocusArea into gFocusArea
        orderby gFocusArea.Key
        select new
        {
          FocusArea = gFocusArea.Key,
          InfiltrationVolume = gFocusArea.Sum(p => p.InfiltrationVolumeCuFt) * GAL_PER_FT_3
        };

      if (query.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("StormwaterVol returned more than one row");
      }
      else if (query.Count() == 0)
      {
        return 0;
      }

      return query.Sum(p => p.InfiltrationVolume);
    }

    public double RoofPlanterStorageVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

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
        where !filteredByFocusArea || query1.FocusArea == focusArea
        group tableE18 by query1.FocusArea into gFocusArea
        select new
        {
          FocusArea = gFocusArea.Key,
          StorageVolume = gFocusArea.Sum(p => p.StorageVolumeCuFt) * GAL_PER_FT_3
        };

      if (qryRoofTargetsPlant.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("RoofPlanterStorageVol returned more than one row");
      }

      double roofTargetsPlant = 0;
      if (qryRoofTargetsPlant.Count() != 0)
      {
        roofTargetsPlant = qryRoofTargetsPlant.Sum(p => p.StorageVolume);
      }

      return roofTargetsPlant;
    }
    public double RoofPlanterInfiltrationVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

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
        where !filteredByFocusArea || query1.FocusArea == focusArea
        group tableE19 by query1.FocusArea into gFocusArea
        select new
        {
          FocusArea = gFocusArea.Key,
          InfiltrationVolume = gFocusArea.Sum(p => p.InfiltrationVolumeCuFt) * GAL_PER_FT_3
        };

      double roofTargetsPlant = 0;
      if (qryRoofTargetsPlant.Count() != 0)
      {
        roofTargetsPlant = qryRoofTargetsPlant.Sum(p => p.InfiltrationVolume);
      }
      return roofTargetsPlant;
    }
    public double RoofBioStorageVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

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
        on tableE18.NodeName equals icNode.FacNode
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
        from parkSub in parkJoin.DefaultIfEmpty() //use to create LINQ left outer join
        join tableE18 in scDS.TableE18
        on roof.NodeName equals tableE18.NodeName into e18Join
        from e18Sub in e18Join.DefaultIfEmpty() //use to create LINQ left outer join
        where (!filteredByFocusArea || roof.FocusArea == focusArea) && parkSub == null
        group e18Sub by roof.FocusArea into gFocusArea
        select new
        {
          FocusArea = gFocusArea.Key,
          StorageVolume = gFocusArea.Sum(p => p.StorageVolumeCuFt) * GAL_PER_FT_3

        };

      if (qryRoofTargetsBio.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("RoofBioStorageVol returned more than one row");
      }

      double roofTargetsBio = 0;
      if (qryRoofTargetsBio.Count() != 0)
      {
        roofTargetsBio = qryRoofTargetsBio.Sum(p => p.StorageVolume);
      }

      return roofTargetsBio;
    }
    public double RoofBioInfiltrationVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

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
        where (!filteredByFocusArea || roof.FocusArea == focusArea) && parkSub == null
        group E19Sub by roof.FocusArea into gFocusArea
        select new
        {
          FocusArea = gFocusArea.Key,
          InfiltrationVolume = gFocusArea.Sum(p => p.InfiltrationVolumeCuFt) * GAL_PER_FT_3

        };

      if (qryRoofTargetsBio.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("RoofBioInfiltrationVol returned more than one row");
      }

      double roofTargetsBio = 0;
      if (qryRoofTargetsBio.Count() != 0)
      {
        roofTargetsBio = qryRoofTargetsBio.Sum(p => p.InfiltrationVolume);
      }

      return roofTargetsBio;
    }

    public double ParkBioStorageVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

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

      //Roof Bioinfiltration Storage - Filtered and grouped by Focus Area
      var qryParkTargetsBio =
        from query1 in qryParkTargetsBioGroupByFacNodeName
        join tableE18 in scDS.TableE18
        on query1.NodeName equals tableE18.NodeName
        where !filteredByFocusArea || query1.FocusArea == focusArea
        group tableE18 by query1.FocusArea into gFocusArea
        select new
        {
          FocusArea = gFocusArea.Key,
          StorageVolume = gFocusArea.Sum(p => p.StorageVolumeCuFt) * GAL_PER_FT_3
        };

      if (qryParkTargetsBio.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("ParkBioStorageVol returned more than one row");
      }

      double parkTargetsBio = 0;
      if (qryParkTargetsBio.Count() != 0)
      {
        parkTargetsBio = qryParkTargetsBio.Sum(p => p.StorageVolume);
      }

      return parkTargetsBio;
    }
    public double ParkBioInfiltrationVolume(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

      //Parking Bioinfiltration Storage - Grouped by Node Name
      var qryParkTargetsBioGroupByFacNodeName =
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

      //Roof Bioinfiltration Storage - Filtered and grouped by Focus Area
      var qryParkTargetsBio =
        from query1 in qryParkTargetsBioGroupByFacNodeName
        join tableE19 in scDS.TableE19
        on query1.NodeName equals tableE19.NodeName
        where !filteredByFocusArea || query1.FocusArea == focusArea
        group tableE19 by query1.FocusArea into gFocusArea
        select new
        {
          FocusArea = gFocusArea.Key,
          InfiltrationVolume = gFocusArea.Sum(p => p.InfiltrationVolumeCuFt) * GAL_PER_FT_3
        };

      if (qryParkTargetsBio.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("ParkBioStorageVol returned more than one row");
      }

      double parkTargetsBio = 0;
      if (qryParkTargetsBio.Count() != 0)
      {
        parkTargetsBio = qryParkTargetsBio.Sum(p => p.InfiltrationVolume);
      }

      return parkTargetsBio;
    }

    public double StormwaterRemovalVol(IDictionary<string, Parameter> parameters)
    {
      double volStreetStorage, volStreetInfiltration;
      double volRoofPlantStorage, volRoofPlantInfiltration;
      double volRoofBioStorage, volRoofBioInfiltration;

      double volParkBioStorage, volParkBioInfiltration;

      volStreetStorage = StormwaterRemovalVolStreetStorage(parameters);
      volStreetInfiltration = StormwaterRemovalVolStreetInfiltration(parameters);

      volRoofPlantStorage = RoofPlanterStorageVolume(parameters);
      volRoofPlantInfiltration = RoofPlanterInfiltrationVolume(parameters);

      volRoofBioStorage = RoofBioStorageVolume(parameters);
      volRoofBioInfiltration = RoofBioInfiltrationVolume(parameters);

      volParkBioStorage = ParkBioStorageVolume(parameters);
      volParkBioInfiltration = ParkBioInfiltrationVolume(parameters);

      return volStreetStorage + volStreetInfiltration +
        volRoofPlantStorage + volRoofPlantInfiltration +
        volRoofBioStorage + volRoofBioInfiltration +
        volParkBioStorage + volParkBioInfiltration;

    }

    public double InfiltrateStormwaterArea(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

      var qryInfiltrationAreaStreet =
        from icNode in scDS.ICNode
        join icStreetTarget in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTarget.XPSWMM_Name
        join altStreetTarget in scDS.AltStreetTargets
        on icStreetTarget.icID equals altStreetTarget.ICID
        join mdlSurfSc in scDS.mdl_SurfSC_ac
        on icStreetTarget.surfSCID equals mdlSurfSc.SurfSCID
        where !filteredByFocusArea || altStreetTarget.FocusArea == focusArea
        group mdlSurfSc by altStreetTarget.FocusArea into gFocusArea
        orderby gFocusArea.Key
        select new
        {
          FocusArea = gFocusArea.Key,
          NetIA = gFocusArea.Sum(p => p.c_netIMPacres)
        };

      double infiltrationAreaStreet = 0;
      if (qryInfiltrationAreaStreet.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("InfiltrationStreetArea returned more than one row");
      }
      else if (qryInfiltrationAreaStreet.Count() != 0)
      {
        infiltrationAreaStreet = qryInfiltrationAreaStreet.Sum(p => p.NetIA);
      }

      var qryInfiltrationAreaRoof =
        from mdlDsc in scDS.mdl_dirsc_ac
        join icRoofTargets in scDS.ic_RoofTargets
        on mdlDsc.DSCID equals icRoofTargets.dscID
        join altRoofTargets in scDS.AltRoofTargets
        on icRoofTargets.icID equals altRoofTargets.ICID
        join mdlRoofTargets in scDS._mdl_roofTargets
        on icRoofTargets.dscID equals mdlRoofTargets.dscID
        where icRoofTargets.constructed == 0 &&
          icRoofTargets.buildModelIC &&
          (!filteredByFocusArea || altRoofTargets.FocusArea == focusArea)
        group mdlRoofTargets by altRoofTargets.FocusArea into gFocusArea
        select new
        {
          FocusArea = gFocusArea.Key,
          InfiltrationAreaRoof = gFocusArea.Sum(p =>
            Math.Max(0.0, (double)p.SqFt_Bioret) + Math.Max(0.0, (double)p.SqFt_Drywell) + Math.Max(0.0, (double)p.SqFt_Eco)
            + Math.Max(0.0, (double)p.SqFt_Plntr) + Math.Max(0.0, (double)p.SqFt_Veg)) / SQ_FT_PER_ACRE
        };

      double infiltrationAreaRoof = 0;
      if (qryInfiltrationAreaRoof.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("InfiltrationRoofArea returned more than one row");
      }
      else if (qryInfiltrationAreaRoof.Count() != 0)
      {
        infiltrationAreaRoof = qryInfiltrationAreaRoof.Sum(p => p.InfiltrationAreaRoof);
      }

      var qryInfiltrationAreaPark =
        from mdlDsc in scDS.mdl_dirsc_ac
        join icParkTargets in scDS.ic_ParkingTargets
        on mdlDsc.DSCID equals icParkTargets.dscID
        join altParkTargets in scDS.AltParkingTargets
        on icParkTargets.icID equals altParkTargets.ICID
        join mdlParkTargets in scDS._mdl_ParkingTargets
        on icParkTargets.dscID equals mdlParkTargets.dscID
        where icParkTargets.constructed == 0 &&
          icParkTargets.buildModelIC &&
          (!filteredByFocusArea || altParkTargets.FocusArea == focusArea)
        group mdlParkTargets by altParkTargets.FocusArea into gFocusArea
        select new
        {
          FocusArea = gFocusArea.Key,
          InfiltrationAreaPark = gFocusArea.Sum(p => Math.Max(0.0, (double)p.SqFt_Bioret)) / SQ_FT_PER_ACRE
        };

      double infiltrationAreaPark = 0;
      if (qryInfiltrationAreaPark.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("InfiltrationParkArea returned more than one row");
      }
      else if (qryInfiltrationAreaPark.Count() != 0)
      {
        infiltrationAreaPark = qryInfiltrationAreaPark.Sum(p => p.InfiltrationAreaPark);
      }

      return infiltrationAreaStreet + infiltrationAreaRoof + infiltrationAreaPark;

    }

    public double ReducePollutantsArea(IDictionary<string, Parameter> parameters)
    {
      return InfiltrateStormwaterArea(parameters);
    }

    public double ProtectImproveTerrestrialHabitatArea(IDictionary<string, Parameter> parameters)
    {
      string focusArea;
      bool filteredByFocusArea;
      filteredByFocusArea = parameters.Keys.Contains("FocusArea");
      focusArea = filteredByFocusArea ? parameters["FocusArea"].Value : "";

      double streetArea = 0, roofArea = 0, parkArea = 0;

      var qryStreetArea =
        from icNode in scDS.ICNode
        join icStreetTarget in scDS.ic_StreetTargets
        on icNode.FacNode equals icStreetTarget.XPSWMM_Name
        join altStreetTarget in scDS.AltStreetTargets
        on icStreetTarget.icID equals altStreetTarget.ICID
        where icNode.Factype == "C" && icStreetTarget.constructed == 0 &&
          (!filteredByFocusArea || altStreetTarget.FocusArea == focusArea)
        group icNode by altStreetTarget.FocusArea into gFocusArea
        orderby gFocusArea.Key
        select new
        {
          FocusArea = gFocusArea.Key,
          FacilityVolume = gFocusArea.Sum(p => p.FacVolCuFt) / 0.75 / SQ_FT_PER_ACRE,
        };

      if (qryStreetArea.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("HabitatStreetArea returned more than one row");
      }
      else if (qryStreetArea.Count() != 0)
      {
        streetArea = qryStreetArea.Sum(p => p.FacilityVolume);
      }

      var qryRoofArea =
        from mdlDsc in scDS.mdl_dirsc_ac
        join icRoofTargets in scDS.ic_RoofTargets
        on mdlDsc.DSCID equals icRoofTargets.dscID
        join altRoofTargets in scDS.AltRoofTargets
        on icRoofTargets.icID equals altRoofTargets.ICID
        join mdlRoofTargets in scDS._mdl_roofTargets
        on icRoofTargets.dscID equals mdlRoofTargets.dscID
        where icRoofTargets.constructed == 0 && icRoofTargets.buildModelIC &&
          (!filteredByFocusArea || altRoofTargets.FocusArea == focusArea)
        group mdlRoofTargets by altRoofTargets.FocusArea into grpFocusArea
        select new
        {
          FocusArea = grpFocusArea.Key,
          FacilityVolume = grpFocusArea.Sum(p =>
            Math.Max(0.0, (double)p.SqFt_Bioret) * 0.83 * 0.09 +
            Math.Max(0.0, (double)p.SqFt_Plntr) * 0.06 +
            Math.Max(0.0, (double)p.SqFt_Eco)) / SQ_FT_PER_ACRE
        };

      if (qryRoofArea.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("HabitatRoofArea returned more than one row");
      }
      else if (qryRoofArea.Count() != 0)
      {
        roofArea = qryRoofArea.Sum(p => p.FacilityVolume);
      }

      var qryParkArea =
        from mdlDsc in scDS.mdl_dirsc_ac
        join icParkTargets in scDS.ic_ParkingTargets
        on mdlDsc.DSCID equals icParkTargets.dscID
        join altParkTargets in scDS.AltParkingTargets
        on icParkTargets.icID equals altParkTargets.ICID
        join mdlParkTargets in scDS._mdl_ParkingTargets
        on icParkTargets.dscID equals mdlParkTargets.dscID
        where icParkTargets.constructed == 0 && icParkTargets.buildModelIC &&
          (!filteredByFocusArea || altParkTargets.FocusArea == focusArea)
        group mdlParkTargets by altParkTargets.FocusArea into grpFocusArea
        select new
        {
          FocusArea = grpFocusArea.Key,
          FacilityVolume = grpFocusArea.Sum(p =>
            Math.Max(0.0, (double)p.SqFt_Bioret) * 0.83 * 0.09) / SQ_FT_PER_ACRE
        };

      if (qryParkArea.Count() > 1 && filteredByFocusArea)
      {
        throw new Exception("HabitatParkArea returned more than one row");
      }
      else if (qryParkArea.Count() != 0)
      {
        parkArea = qryParkArea.Sum(p => p.FacilityVolume);
      }

      return streetArea + roofArea + parkArea;
    }
    public double ProtectImproveAquaticHabitatArea(IDictionary<string, Parameter> parameters)
    {
      return 0;
    }

    public double CreditRemovingVolumeTunnel(IDictionary<string, Parameter> parameters)
    {
      return StormwaterRemovalVol(parameters) * CREDIT_PER_GALLON_STORMWATER_REMOVAL;
    }

  }
}
