using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  public class Conflict
  {
    public int ID { get; private set; }
    public int GlobalID { get; private set; }
    public string USNodeID { get; private set; }
    public string DSNodeID { get; private set; }
    public int HansenCompkey { get; private set; }

    public int NumWaterCrossings { get; private set; }
    public int WaterCrossingMinDiamWidthIn { get; private set; }
    public int WaterCrossingMaxDiamWidthIn { get; private set; }
    public int NumWaterParallels { get; private set; }
    public int WaterParallelsMaxDiamWidthIn { get; private set; }
    public int DistFtToNearestWaterParallel { get; private set; }

    public int NumSewerCrossings { get; private set; }
    public int SewerCrossingMinDiamWidthIn { get; private set; }
    public int SewerCrossingMaxDiamWidthIn { get; private set; }
    public int NumSewerParallels { get; private set; }
    public int SewerParallelsMaxDiamWidthIn { get; private set; }
    public int DistFtToNearestSewerParallel { get; private set; }

    public int NumStreetCrossings { get; private set; }
    public int NumArterialCrossings { get; private set; }
    public int NumMajorArterialCrossings { get; private set; }
    public int NumFreewayCrossings { get; private set; }
    public int NumStreetParallels { get; private set; }
    public int StreetParallelsType { get; private set; }
    public int DistFtToNearestStreetParallel { get; private set; }
    public int StreetParallelsTrafficCount { get; private set; }
    public int NumStreetsWithUSNodeAtIntersection { get; private set; }
    public int DistToStreetsWithUSNodeAtIntersection { get; private set; }
    public int NumStreetsWithDSNodeAtIntersection { get; private set; }
    public int DistToStreetsWithDSNodeAtIntersection { get; private set; }

    public int NumFiberCrossings { get; private set; }
    public int NumFiberParallels { get; private set; }
    public int DistFtToNearestFiberParallel { get; private set; }

    public int NumGasCrossings { get; private set; }
    public int NumGasParallels { get; private set; }
    public int DistFtToNearestGasParallel { get; private set; }

    public int NumRailCrossings { get; private set; }
    public int NumRailParallels { get; private set; }
    public int DistFtToNearestRailParallel { get; private set; }

    public int NumLRTCrossings { get; private set; }
    public int NumLRTParallels { get; private set; }
    public int DistFtToNearestLRTParallel { get; private set; }

    public int NumEMTCrossings { get; private set; }
    public int NumEMTParallels { get; private set; }
    public int DistFtToNearestEMTParallel { get; private set; }

    public bool InConservationZone { get; private set; }
    public bool InPreservationZone { get; private set; }
    public int LengthFtInsideConservationZone { get; private set; }
    public int LengthFtInsidePreservationZone { get; private set; }

    public bool USNodeInMS4 { get; private set; }
    public bool USNodeInUIC { get; private set; }

    public double USDepth { get; private set; }
    public double DSDepth { get; private set; }
    public double PipeSlope { get; private set; }
    public double GroundSlope { get; private set; }

    public bool NearContaminationSite { get; private set; }
    public int DistFtToContaminationSite { get; private set; }
    public int LengthFtInsideContaminationSite { get; private set; }
    public int VolumeCuFtInsideContaminationSite { get; private set; }

    public bool NearSchool { get; private set; }
    public int DistFtToNearestSchool { get; private set; }

    public bool NearHospital { get; private set; }
    public int DistFtToNearestHospital { get; private set; }

    public bool NearPoliceStation { get; private set; }
    public int DistFtToNearestPoliceStation { get; private set; }

    public bool NearFireStation { get; private set; }
    public int DistFtToNearestFireStation { get; private set; }

    public bool NearBuilding { get; private set; }
    public int DistFtToNearestBuilding { get; private set; }

    public bool NearHydrant { get; private set; }
    public int DistFtToNearestHydrant { get; private set; }

    public bool InHardArea { get; private set; }

    public Conflict(OleDbDataReader reader)
    {
      ID = Convert.ToInt32(reader["ID"]);
      GlobalID = Convert.ToInt32(reader["GLOBALID"]);
      USNodeID = reader["us_node_id"].ToString();
      DSNodeID = reader["ds_node_id"].ToString();
      HansenCompkey = Convert.ToInt32(reader["hansen_compkey"]);

      NumWaterCrossings = Convert.ToInt32(reader["xWtr"]);
      WaterCrossingMinDiamWidthIn = Convert.ToInt32(reader["xWMinD"]);
      WaterCrossingMaxDiamWidthIn = Convert.ToInt32(reader["xWMaxD"]);
      NumWaterParallels = Convert.ToInt32(reader["pWtr"]);
      WaterParallelsMaxDiamWidthIn = Convert.ToInt32(reader["pWtrMaxD"]);
      DistFtToNearestWaterParallel = Convert.ToInt32(reader["pFt2Wtr"]);

      NumSewerCrossings = Convert.ToInt32(reader["xSewer"]);
      SewerCrossingMinDiamWidthIn = Convert.ToInt32(reader["xSwrMinD"]);
      SewerParallelsMaxDiamWidthIn = Convert.ToInt32(reader["xSwrMaxD"]);
      NumSewerParallels = Convert.ToInt32(reader["pSewer"]);
      SewerParallelsMaxDiamWidthIn = Convert.ToInt32(reader["pSwrMaxD"]);
      DistFtToNearestSewerParallel = Convert.ToInt32(reader["pFt2Swr"]);

      NumStreetCrossings = Convert.ToInt32(reader["xStrt"]);
      NumArterialCrossings = Convert.ToInt32(reader["xArt"]);
      NumMajorArterialCrossings = Convert.ToInt32(reader["xMJArt"]);
      NumFreewayCrossings = Convert.ToInt32(reader["xFrwy"]);
      NumStreetParallels = Convert.ToInt32(reader["pStrt"]);
      StreetParallelsType = Convert.ToInt32(reader["pStrtType"]);
      DistFtToNearestStreetParallel = Convert.ToInt32(reader["pFt2Strt"]);
      StreetParallelsTrafficCount = Convert.ToInt32(reader["pTraffic"]);
      NumStreetsWithUSNodeAtIntersection = Convert.ToInt32(reader["uxCLx"]);
      DistToStreetsWithUSNodeAtIntersection = Convert.ToInt32(reader["uxFt2CLx"]);
      NumStreetsWithDSNodeAtIntersection = Convert.ToInt32(reader["dxCLx"]);
      DistToStreetsWithDSNodeAtIntersection = Convert.ToInt32(reader["dxFt2CLx"]);

      NumFiberCrossings = Convert.ToInt32(reader["xFiber"]);
      NumFiberParallels = Convert.ToInt32(reader["pFiber"]);
      DistFtToNearestFiberParallel = Convert.ToInt32(reader["pFt2Fiber"]);

      NumGasCrossings = Convert.ToInt32(reader["xGas"]);
      NumGasParallels = Convert.ToInt32(reader["pGas"]);
      DistFtToNearestGasParallel = Convert.ToInt32(reader["pFt2Gas"]);

      NumRailCrossings = Convert.ToInt32(reader["xRail"]);
      NumRailParallels = Convert.ToInt32(reader["pRail"]);
      DistFtToNearestRailParallel = Convert.ToInt32(reader["pFt2Rail"]);

      NumLRTCrossings = Convert.ToInt32(reader["xLRT"]);
      NumLRTParallels = Convert.ToInt32(reader["pLRT"]);
      DistFtToNearestLRTParallel = Convert.ToInt32(reader["pFt2LRT"]);

      NumEMTCrossings = Convert.ToInt32(reader["xEMT"]);
      NumEMTParallels = Convert.ToInt32(reader["pEMT"]);
      DistFtToNearestEMTParallel = Convert.ToInt32(reader["pFt2EMT"]);

      InConservationZone = Convert.ToInt32(reader["xEzonC"]) != 0;
      InPreservationZone = Convert.ToInt32(reader["xEzonP"]) != 0;
      LengthFtInsideConservationZone = Convert.ToInt32(reader["xEzAreaC"]);
      LengthFtInsidePreservationZone = Convert.ToInt32(reader["xEzAreaP"]);

      USNodeInMS4 = Convert.ToInt32(reader["uxMS4"]) != 0;
      USNodeInUIC = Convert.ToInt32(reader["uxUIC"]) != 0;

      USDepth = Convert.ToDouble(reader["uDepth"]);
      DSDepth = Convert.ToDouble(reader["dDepth"]);
      PipeSlope = Convert.ToDouble(reader["xPipSlope"]);
      GroundSlope = Convert.ToDouble(reader["gSlope"]);

      NearContaminationSite = Convert.ToInt32(reader["xEcsi"]) != 0;
      DistFtToContaminationSite = Convert.ToInt32(reader["xFt2Ecsi"]);
      LengthFtInsideContaminationSite = Convert.ToInt32(reader["xEcsiLen"]);
      VolumeCuFtInsideContaminationSite = Convert.ToInt32(reader["xEcsiVol"]);

      NearSchool = Convert.ToInt32(reader["xSchl"]) != 0;
      DistFtToNearestSchool = Convert.ToInt32(reader["xFt2Schl"]);

      NearHospital = Convert.ToInt32(reader["xHosp"]) != 0;
      DistFtToNearestHospital = Convert.ToInt32(reader["xFt2Hosp"]);

      NearPoliceStation = Convert.ToInt32(reader["xPol"]) != 0;
      DistFtToNearestPoliceStation = Convert.ToInt32(reader["xFt2Pol"]);

      NearFireStation = Convert.ToInt32(reader["xFire"]) != 0;
      DistFtToNearestFireStation = Convert.ToInt32(reader["xFt2Fire"]);

      NearBuilding = Convert.ToInt32(reader["xBldg"]) != 0;
      DistFtToNearestBuilding = Convert.ToInt32(reader["xFt2Bldg"]);

      NearHydrant = Convert.ToInt32(reader["xHyd"]) != 0;
      DistFtToNearestHydrant = Convert.ToInt32(reader["xFt2Hyd"]);

      InHardArea = Convert.ToInt32(reader["HardArea"]) != 0;
    }
  }
}
