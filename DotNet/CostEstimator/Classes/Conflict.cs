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

    public Int16 NumWaterCrossings { get; private set; }
    public Int16 WaterCrossingMinDiamWidthIn { get; private set; }
    public Int16 WaterCrossingMaxDiamWidthIn { get; private set; }
    public Int16 NumWaterParallels { get; private set; }
    public Int16 WaterParallelsMaxDiamWidthIn { get; private set; }
    public Int16 DistFtToNearestWaterParallel { get; private set; }

    public Int16 NumSewerCrossings { get; private set; }
    public Int16 SewerCrossingMinDiamWidthIn { get; private set; }
    public Int16 SewerCrossingMaxDiamWidthIn { get; private set; }
    public Int16 NumSewerParallels { get; private set; }
    public Int16 SewerParallelsMaxDiamWidthIn { get; private set; }
    public Int16 DistFtToNearestSewerParallel { get; private set; }

    public Int16 NumStreetCrossings { get; private set; }
    public Int16 NumArterialCrossings { get; private set; }
    public Int16 NumMajorArterialCrossings { get; private set; }
    public Int16 NumFreewayCrossings { get; private set; }
    public Int16 NumStreetParallels { get; private set; }
    public Int16 StreetParallelsType { get; private set; }
    public Int16 DistFtToNearestStreetParallel { get; private set; }
    public Int16 StreetParallelsTrafficCount { get; private set; }
    public Int16 NumStreetsWithUSNodeAtIntersection { get; private set; }
    public Int16 DistToStreetsWithUSNodeAtIntersection { get; private set; }
    public Int16 NumStreetsWithDSNodeAtIntersection { get; private set; }
    public Int16 DistToStreetsWithDSNodeAtIntersection { get; private set; }

    public Int16 NumFiberCrossings { get; private set; }
    public Int16 NumFiberParallels { get; private set; }
    public Int16 DistFtToNearestFiberParallel { get; private set; }

    public Int16 NumGasCrossings { get; private set; }
    public Int16 NumGasParallels { get; private set; }
    public Int16 DistFtToNearestGasParallel { get; private set; }

    public Int16 NumRailCrossings { get; private set; }
    public Int16 NumRailParallels { get; private set; }
    public Int16 DistFtToNearestRailParallel { get; private set; }

    public Int16 NumLRTCrossings { get; private set; }
    public Int16 NumLRTParallels { get; private set; }
    public Int16 DistFtToNearestLRTParallel { get; private set; }

    public Int16 NumEMTCrossings { get; private set; }
    public Int16 NumEMTParallels { get; private set; }
    public Int16 DistFtToNearestEMTParallel { get; private set; }

    public bool InConservationZone { get; private set; }
    public bool InPreservationZone { get; private set; }
    public Int16 LengthFtInsideConservationZone { get; private set; }
    public Int16 LengthFtInsidePreservationZone { get; private set; }
    public Int16 AreaSqFtInsideConservationZone { get; private set; }
    public Int16 AreaSqFtInsidePreservationZone { get; private set; }

    public bool USNodeInMS4 { get; private set; }
    public bool USNodeInUIC { get; private set; }

    public Single USDepth { get; private set; }
    public Single DSDepth { get; private set; }
    public Single PipeSlope { get; private set; }
    public Single GroundSlope { get; private set; }

    public bool NearContaminationSite { get; private set; }
    public Int16 DistFtToContaminationSite { get; private set; }
    public Int16 LengthFtInsideContaminationSite { get; private set; }
    public int VolumeCuFtInsideContaminationSite { get; private set; }

    public bool NearSchool { get; private set; }
    public Int16 DistFtToNearestSchool { get; private set; }

    public bool NearHospital { get; private set; }
    public Int16 DistFtToNearestHospital { get; private set; }

    public bool NearPoliceStation { get; private set; }
    public Int16 DistFtToNearestPoliceStation { get; private set; }

    public bool NearFireStation { get; private set; }
    public Int16 DistFtToNearestFireStation { get; private set; }

    public bool NearBuilding { get; private set; }
    public Int16 DistFtToNearestBuilding { get; private set; }

    public bool NearHydrant { get; private set; }
    public Int16 DistFtToNearestHydrant { get; private set; }

    public bool InHardArea { get; private set; }

    public Conflict(OleDbDataReader reader)
    {
      ID = Convert.ToInt32(reader["ID"] != DBNull.Value ? reader["ID"] : 0);
      GlobalID = Convert.ToInt32(reader["GLOBALID"] != DBNull.Value ? reader["GLOBALID"] : 0);
      USNodeID = reader["us_node_id"] != DBNull.Value ? reader["us_node_id"].ToString() : string.Empty;
      DSNodeID = reader["ds_node_id"] != DBNull.Value ? reader["ds_node_id"].ToString() : string.Empty;
      HansenCompkey = Convert.ToInt32(reader["hansen_compkey"] != DBNull.Value ? reader["hansen_compkey"] : 0);

      NumWaterCrossings = Convert.ToInt16(reader["xWtr"] != DBNull.Value ? reader["xWtr"] : 0);
      WaterCrossingMinDiamWidthIn = Convert.ToInt16(reader["xWMinD"] != DBNull.Value ? reader["xWMinD"] : 0);
      WaterCrossingMaxDiamWidthIn = Convert.ToInt16(reader["xWMaxD"] != DBNull.Value ? reader["xWMaxD"] : 0);
      NumWaterParallels = Convert.ToInt16(reader["pWtr"] != DBNull.Value ? reader["pWtr"] : 0);
      WaterParallelsMaxDiamWidthIn = Convert.ToInt16(reader["pWtrMaxD"] != DBNull.Value ? reader["pWtrMaxD"] : 0);
      DistFtToNearestWaterParallel = Convert.ToInt16(reader["pFt2Wtr"] != DBNull.Value ? reader["pFt2Wtr"] : 0);

      NumSewerCrossings = Convert.ToInt16(reader["xSewer"] != DBNull.Value ? reader["xSewer"] : 0);
      SewerCrossingMinDiamWidthIn = Convert.ToInt16(reader["xSwrMinD"] != DBNull.Value ? reader["xSwrMinD"] : 0);
      SewerParallelsMaxDiamWidthIn = Convert.ToInt16(reader["xSwrMaxD"] != DBNull.Value ? reader["xSwrMaxD"] : 0);
      NumSewerParallels = Convert.ToInt16(reader["pSewer"] != DBNull.Value ? reader["pSewer"] : 0);
      SewerParallelsMaxDiamWidthIn = Convert.ToInt16(reader["pSwrMaxD"] != DBNull.Value ? reader["pSwrMaxD"]: 0);
      DistFtToNearestSewerParallel = Convert.ToInt16(reader["pFt2Swr"] != DBNull.Value ? reader["pFt2Swr"] : 0);

      NumStreetCrossings = Convert.ToInt16(reader["xStrt"] != DBNull.Value ? reader["xStrt"] : 0);
      NumArterialCrossings = Convert.ToInt16(reader["xArt"] != DBNull.Value ? reader["xArt"] : 0);
      NumMajorArterialCrossings = Convert.ToInt16(reader["xMJArt"] != DBNull.Value ? reader["xMJArt"] : 0);
      NumFreewayCrossings = Convert.ToInt16(reader["xFrwy"] != DBNull.Value ? reader["xFrwy"] : 0);
      NumStreetParallels = Convert.ToInt16(reader["pStrt"] != DBNull.Value ? reader["pStrt"] : 0);
      StreetParallelsType = Convert.ToInt16(reader["pStrtTyp"] != DBNull.Value ? reader["pStrtTyp"] : 0);
      DistFtToNearestStreetParallel = Convert.ToInt16(reader["pFt2Strt"] != DBNull.Value ? reader["pFt2Strt"] : 0);
      StreetParallelsTrafficCount = Convert.ToInt16(reader["pTraffic"] != DBNull.Value ? reader["pTraffic"] : 0);
      NumStreetsWithUSNodeAtIntersection = Convert.ToInt16(reader["uxCLx"] != DBNull.Value ? reader["uxCLx"] : 0);
      DistToStreetsWithUSNodeAtIntersection = Convert.ToInt16(reader["uxFt2CLx"] != DBNull.Value ? reader["uxFt2CLx"] : 0);
      NumStreetsWithDSNodeAtIntersection = Convert.ToInt16(reader["dxCLx"] != DBNull.Value ? reader["dxCLx"] : 0);
      DistToStreetsWithDSNodeAtIntersection = Convert.ToInt16(reader["dxFt2CLx"] != DBNull.Value ? reader["dxFt2CLx"] : 0);

      NumFiberCrossings = Convert.ToInt16(reader["xFiber"] != DBNull.Value ? reader["xFiber"] : 0);
      NumFiberParallels = Convert.ToInt16(reader["pFiber"] != DBNull.Value ? reader["pFiber"] : 0);
      DistFtToNearestFiberParallel = Convert.ToInt16(reader["pFt2Fiber"] != DBNull.Value ? reader["pFt2Fiber"] : 0);

      NumGasCrossings = Convert.ToInt16(reader["xGas"] != DBNull.Value ? reader["xGas"] : 0);
      NumGasParallels = Convert.ToInt16(reader["pGas"] != DBNull.Value ? reader["pGas"] : 0);
      DistFtToNearestGasParallel = Convert.ToInt16(reader["pFt2Gas"] != DBNull.Value ? reader["pFt2Gas"] : 0);

      NumRailCrossings = Convert.ToInt16(reader["xRail"] != DBNull.Value ? reader["xRail"] : 0);
      NumRailParallels = Convert.ToInt16(reader["pRail"] != DBNull.Value ? reader["pRail"] : 0);
      DistFtToNearestRailParallel = Convert.ToInt16(reader["pFt2Rail"] != DBNull.Value ? reader["pFt2Rail"] : 0);

      NumLRTCrossings = Convert.ToInt16(reader["xLRT"] != DBNull.Value ? reader["xLRT"] : 0);
      NumLRTParallels = Convert.ToInt16(reader["pLRT"] != DBNull.Value ? reader["pLRT"] : 0);
      DistFtToNearestLRTParallel = Convert.ToInt16(reader["pFt2LRT"] != DBNull.Value ? reader["pFt2LRT"] : 0);

      NumEMTCrossings = Convert.ToInt16(reader["xEMT"] != DBNull.Value ? reader["xEMT"] : 0);
      NumEMTParallels = Convert.ToInt16(reader["pEMT"] != DBNull.Value ? reader["pEMT"] : 0);
      DistFtToNearestEMTParallel = Convert.ToInt16(reader["pFt2EMT"] != DBNull.Value ? reader["pFt2EMT"] : 0);

      InConservationZone = Convert.ToInt16(reader["xEzonC"] != DBNull.Value ? reader["xEzonC"] : 0) != 0;
      InPreservationZone = Convert.ToInt16(reader["xEzonP"] != DBNull.Value ? reader["xEzonP"] : 0) != 0;
      LengthFtInsideConservationZone = Convert.ToInt16(reader["xEzAreaC"] != DBNull.Value ? reader["xEzAreaC"] : 0);
      LengthFtInsidePreservationZone = Convert.ToInt16(reader["xEzAreaP"] != DBNull.Value ? reader["xEzAreaP"] : 0);
      AreaSqFtInsideConservationZone = Convert.ToInt16(reader["xEzAreaC"] != DBNull.Value ? reader["xEzAreaC"] : 0);
      AreaSqFtInsidePreservationZone = Convert.ToInt16(reader["xEzAreaP"] != DBNull.Value ? reader["xEzAreaP"] : 0);


      USNodeInMS4 = Convert.ToInt16(reader["uxMS4"] != DBNull.Value ? reader["uxMS4"] : 0) != 0;
      USNodeInUIC = Convert.ToInt16(reader["uxUIC"] != DBNull.Value ? reader["uxUIC"] : 0) != 0;

      USDepth = Convert.ToSingle(reader["uDepth"] != DBNull.Value ? reader["uDepth"] : 0.0);
      DSDepth = Convert.ToSingle(reader["dDepth"] != DBNull.Value ? reader["dDepth"] : 0.0);
      PipeSlope = Convert.ToSingle(reader["xPipSlope"] != DBNull.Value ? reader["xPipSlope"] : 0.0);
      GroundSlope = Convert.ToSingle(reader["gSlope"] != DBNull.Value ? reader["gSlope"] : 0.0);

      NearContaminationSite = Convert.ToInt16(reader["xEcsi"] != DBNull.Value ? reader["xEcsi"] : 0) != 0;
      DistFtToContaminationSite = Convert.ToInt16(reader["xFt2Ecsi"] != DBNull.Value ? reader["xFt2Ecsi"] : 0);
      LengthFtInsideContaminationSite = Convert.ToInt16(reader["xEcsiLen"] != DBNull.Value ? reader["xEcsiLen"] : 0);
      VolumeCuFtInsideContaminationSite = Convert.ToInt32(reader["xEcsiVol"] != DBNull.Value ? reader["xEcsiVol"] : 0);

      NearSchool = Convert.ToInt16(reader["xSchl"] != DBNull.Value ? reader["xSchl"] : 0) != 0;
      DistFtToNearestSchool = Convert.ToInt16(reader["xFt2Schl"] != DBNull.Value ? reader["xFt2Schl"] : 0);

      NearHospital = Convert.ToInt16(reader["xHosp"] != DBNull.Value ? reader["xHosp"] : 0) != 0;
      DistFtToNearestHospital = Convert.ToInt16(reader["xFt2Hosp"] != DBNull.Value ? reader["xFt2Hosp"] : 0);

      NearPoliceStation = Convert.ToInt16(reader["xPol"] != DBNull.Value ? reader["xPol"] : 0) != 0;
      DistFtToNearestPoliceStation = Convert.ToInt16(reader["xFt2Pol"] != DBNull.Value ? reader["xFt2Pol"] : 0);

      NearFireStation = Convert.ToInt16(reader["xFire"] != DBNull.Value ? reader["xFire"] : 0) != 0;
      DistFtToNearestFireStation = Convert.ToInt16(reader["xFt2Fire"] != DBNull.Value ? reader["xFt2Fire"] : 0);

      NearBuilding = Convert.ToInt16(reader["xBldg"] != DBNull.Value ? reader["xBldg"] : 0) != 0;
      DistFtToNearestBuilding = Convert.ToInt16(reader["xFt2Bldg"] != DBNull.Value ? reader["xFt2Bldg"] : 0);

      NearHydrant = Convert.ToInt16(reader["xHyd"] != DBNull.Value ? reader["xHyd"] : 0) != 0;
      DistFtToNearestHydrant = Convert.ToInt16(reader["xFt2Hyd"] != DBNull.Value ? reader["xFt2Hyd"] : 0);

      InHardArea = Convert.ToInt16(reader["HardArea"] != DBNull.Value ? reader["HardArea"] : 0) != 0;
    }
  }
}
