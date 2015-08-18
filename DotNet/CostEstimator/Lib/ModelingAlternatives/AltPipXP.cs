// Project: SystemsAnalysis.Modeling.Alternatives, File: AltPipXP.cs
// Namespace: SystemsAnalysis.Modeling.Alternatives, Class: AltPipXP
// Path: C:\Development\DotNet\Alternatives, Author: Arnel
// Code lines: 85, Size of file: 2.57 KB
// Creation date: 6/3/2008 12:15 PM
// Last modified: 8/26/2008 11:41 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis;
using SystemsAnalysis.Types;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;

#endregion

/// <summary>
/// Systems analysis. modeling. alternatives
/// </summary>
namespace SystemsAnalysis.Modeling.Alternatives
{
  public class AltPipXP : PipeConflict
  {
    #region Variables
    // Identifying data
    private int _AltLinkID;
    private string _USNode;
    private string _DSNode;
    private int _MstLinkID;
    private int _CompKey;

    // Water facilities
    private int _NumWaterCrossings;
    private int _SmallestWaterCrossingDiameterInches;
    private int _LargestWaterCrossingDiameterInches;
    private bool _HasWaterParallel;
    private int _LargestWaterParallelDiameterInches;
    private int _DistToWaterParallelFeet;

    // Sewer Facilities
    private int _NumSewerCrossings;
    private int _SmallestSewerCrossingDiameterInches;
    private int _LargestSewerCrossingDiameterInches;
    private bool _HasSewerParallel;
    private int _LargestSewerParallelDiameterInches;
    private int _DistToSewerParallelFeet;

    // Transportation facilities
    private int _NumStreetCrossings;
    private int _NumArterialCrossings;
    private int _NumMajorArterialCrossings;
    private int _NumFreewayCrossings;
    private bool _IsInStreet;
    private Enumerators.StreetTypeKind _StreetType;
    private int _DistToStreetCenterlineFeet;
    private int _NumStreetsIfUSNodeInIntersection;
    private int _DistUSNodeToIntersectionFeet;
    private int _NumStreetsIfDSNodeInIntersection;
    private int _DistDSNodeToIntersectionFeet;
    private int _TrafficVolVehiclesPerDay;

    // Railroad facilities
    private int _NumRailroadCrossings;
    private bool _HasRailroadParallel;
    private int _DistToRailroadParallelFeet;
    private int _NumLightRailCrossings;
    private bool _HasLightRailParallel;
    private int _DistToLightRailParallelFeet;

    // Fiber Optic & Gas facilities
    private int _NumFiberOpticCrossings;
    private bool _HasFiberOpticParallel;
    private int _DistToFiberOpticParallelFeet;
    private int _NumGasCrossings;
    private bool _HasGasParallel;
    private int _DistToGasParallel;

    // Environmental zones
    private bool _IsInConservationZone;
    private int _LengthInConservationZoneFeet;
    private int _AreaConservationZoneSqFt;
    private bool _IsInPreservationZone;
    private int _LengthInPreservationZoneFeet;
    private int _AreaPreservationZoneSqFt;
    private bool _IsNearContaminationSite;
    private int _DistToNearestEcsiFeet;
    private int _VolHazardousConflictCuYd;

    // Public safety
    private bool _IsNearSchool;
    private int _DistToSchoolFeet;
    private bool _IsNearHospital;
    private int _DistToHospitalFeet;
    private bool _IsNearPoliceStation;
    private int _DistToPoliceStationFeet;
    private bool _IsNearFireStation;
    private int _DistToFireStationFeet;
    private int _NumEmergencyRouteCrossings;
    private bool _IsInEmergencyRoute;
    private int _DistToEmergencyRouteCenterlineFeet;

    // Other
    private bool _USNodeInMS4;
    private bool _USNodeInUIC;
    private double _USNodeDepth;
    private double _DSNodeDepth;
    private int _SurfaceSlopePct;
    private bool _IsNearBuilding;
    private int _DistToBuildingFeet;
    private bool _IsNearHydrant;
    private int _DistToHydrant;
    private bool _IsHardArea;
    #endregion

    #region Constructors
    /// <summary>
    /// Create alt pip XP
    /// </summary>
    /// <param name="AltPipXPRow">Alt pip XP row</param>
    public AltPipXP(DataAccess.AlternativeDataSet.AltPipXPRow altPipXPRow)
    {
      _AltLinkID = altPipXPRow.AltLinkID;
      _USNode = altPipXPRow.USNode;
      _DSNode = altPipXPRow.DSNode;
      _MstLinkID = altPipXPRow.MstLinkID;
      _CompKey = altPipXPRow.COMPKEY;

      _NumWaterCrossings = altPipXPRow.xWtr;
      _SmallestWaterCrossingDiameterInches = altPipXPRow.xWMinD;
      _LargestWaterCrossingDiameterInches = altPipXPRow.xWMaxD;
      _HasWaterParallel = altPipXPRow.pWtr == 1;
      _LargestWaterParallelDiameterInches = altPipXPRow.pWtrMaxD;
      _DistToWaterParallelFeet = altPipXPRow.pFt2Wtr;

      _NumSewerCrossings = altPipXPRow.xSewer;
      _SmallestSewerCrossingDiameterInches = altPipXPRow.xSwrMinD;
      _LargestSewerCrossingDiameterInches = altPipXPRow.xSwrMaxD;
      _HasSewerParallel = altPipXPRow.pSewer == 1;
      _LargestSewerParallelDiameterInches = altPipXPRow.pSwrMaxD;
      _DistToSewerParallelFeet = altPipXPRow.pFt2Swr;

      _NumStreetCrossings = altPipXPRow.xStrt;
      _NumArterialCrossings = altPipXPRow.xArt;
      _NumMajorArterialCrossings = altPipXPRow.xMJArt;
      _NumFreewayCrossings = altPipXPRow.xFrwy;
      _IsInStreet = altPipXPRow.pStrt == 1;
      switch (altPipXPRow.pStrtTyp)
      {
        case 1110:
        case 1121:
        case 1123:
        case 1200:
        case 1221:
        case 1222:
        case 1223:
          _StreetType = Enumerators.StreetTypeKind.Freeway;
          break;
        case 1300:
          _StreetType = Enumerators.StreetTypeKind.MajorArterial;
          break;
        case 1400:
        case 1450:
          _StreetType = Enumerators.StreetTypeKind.Arterial;
          break;
        case 1500:
        case 1521:
        case 1700:
        case 1740:
        case 1750:
        case 1800:
        case 1950:
        case 5301:
        case 5401:
        case 5501:
          _StreetType = Enumerators.StreetTypeKind.Street;
          break;
        default:
          _StreetType = Enumerators.StreetTypeKind.None;
          break;
      } // switch
      _DistToStreetCenterlineFeet = altPipXPRow.pFt2Strt;
      _NumStreetsIfUSNodeInIntersection = altPipXPRow.uxCLx;
      _DistUSNodeToIntersectionFeet = altPipXPRow.uxFt2CLx;
      _NumStreetsIfDSNodeInIntersection = altPipXPRow.dxCLx;
      _DistDSNodeToIntersectionFeet = altPipXPRow.dxFt2CLx;
      _TrafficVolVehiclesPerDay = altPipXPRow.pTraffic;

      _NumRailroadCrossings = altPipXPRow.xRail;
      _HasRailroadParallel = altPipXPRow.pRail == 1;
      _DistToRailroadParallelFeet = altPipXPRow.pFt2Rail;
      _NumLightRailCrossings = altPipXPRow.xLRT;
      _HasLightRailParallel = altPipXPRow.pLRT == 1;
      _DistToLightRailParallelFeet = altPipXPRow.pFt2LRT;

      _NumFiberOpticCrossings = altPipXPRow.xFiber;
      _HasFiberOpticParallel = altPipXPRow.pFiber == 1;
      _DistToFiberOpticParallelFeet = altPipXPRow.pFt2Fiber;
      _NumGasCrossings = altPipXPRow.xGas;
      _HasGasParallel = altPipXPRow.pGas == 1;
      _DistToGasParallel = altPipXPRow.pFt2Gas;

      _IsInConservationZone = altPipXPRow.xEzonC == 1;
      _LengthInConservationZoneFeet = altPipXPRow.xFtEzonC;
      _AreaConservationZoneSqFt = altPipXPRow.xEzAreaC;
      _IsInPreservationZone = altPipXPRow.xEzonP == 1;
      _LengthInPreservationZoneFeet = altPipXPRow.xFtEzonP;
      _AreaPreservationZoneSqFt = altPipXPRow.xEzAreaP;
      _IsNearContaminationSite = altPipXPRow.xEcsi == 1;
      _DistToNearestEcsiFeet = altPipXPRow.xFt2Ecsi;
      _VolHazardousConflictCuYd = altPipXPRow.xEcsiVol;

      _IsNearSchool = altPipXPRow.xSchl == 1;
      _DistToSchoolFeet = altPipXPRow.xFt2Schl;
      _IsNearHospital = altPipXPRow.xHosp == 1;
      _DistToHospitalFeet = altPipXPRow.xFt2Hosp;
      _IsNearPoliceStation = altPipXPRow.xPol == 1;
      _DistToPoliceStationFeet = altPipXPRow.xFt2Pol;
      _IsNearFireStation = altPipXPRow.xFire == 1;
      _DistToFireStationFeet = altPipXPRow.xFt2Fire;
      _NumEmergencyRouteCrossings = altPipXPRow.xEmt;
      _IsInEmergencyRoute = altPipXPRow.pEmt == 1;
      _DistToEmergencyRouteCenterlineFeet = altPipXPRow.pFt2Emt;

      _USNodeInMS4 = altPipXPRow.uxMS4 == 1;
      _USNodeInUIC = altPipXPRow.uxUIC == 1;
      _USNodeDepth = altPipXPRow.uDepth;
      _DSNodeDepth = altPipXPRow.dDepth;
      _SurfaceSlopePct = altPipXPRow.gSlope;
      _IsNearBuilding = altPipXPRow.xBldg == 1;
      _DistToBuildingFeet = altPipXPRow.xFt2Bldg;
      _IsNearHydrant = altPipXPRow.xHyd == 1;
      _DistToHydrant = altPipXPRow.xFt2Hyd;
      _IsHardArea = altPipXPRow.HardArea == 1;
    } // AltPipXP(altPipXPRow)
    #endregion

    #region Properties
    /// <summary>
    /// Alt link ID
    /// </summary>
    /// <returns>Int</returns>
    public int AltLinkID
    {
      get
      {
        return _AltLinkID;
      } // get
    } // AltLinkID

    /// <summary>
    /// USNode
    /// </summary>
    /// <returns>String</returns>
    public string USNode
    {
      get
      {
        return _USNode;
      } // get
    } // USNode

    /// <summary>
    /// DSNode
    /// </summary>
    /// <returns>String</returns>
    public string DSNode
    {
      get
      {
        return _DSNode;
      } // get
    } // DSNode

    /// <summary>
    /// Mst link ID
    /// </summary>
    /// <returns>Int</returns>
    public int MstLinkID
    {
      get
      {
        return _MstLinkID;
      } // get
    } // MstLinkID

    /// <summary>
    /// Comp key
    /// </summary>
    /// <returns>Int</returns>
    public int CompKey
    {
      get
      {
        return _CompKey;
      } // get
    } // CompKey

    /// <summary>
    /// Num water crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumWaterCrossings
    {
      get
      {
        return _NumWaterCrossings;
      } // get
    } // NumWaterCrossings

    /// <summary>
    /// Smallest water crossing diameter inches
    /// </summary>
    /// <returns>Int</returns>
    public int SmallestWaterCrossingDiameterInches
    {
      get
      {
        return _SmallestWaterCrossingDiameterInches;
      } // get
    } // SmallestWaterCrossingDiameterInches

    /// <summary>
    /// Largest water crossing diameter inches
    /// </summary>
    /// <returns>Int</returns>
    public int LargestWaterCrossingDiameterInches
    {
      get
      {
        return _LargestWaterCrossingDiameterInches;
      } // get
    } // LargestWaterCrossingDiameterInches

    /// <summary>
    /// Has water parallel
    /// </summary>
    /// <returns>Bool</returns>
    public bool HasWaterParallel
    {
      get
      {
        return _HasWaterParallel;
      } // get
    } // HasWaterParallel

    /// <summary>
    /// Largest water parallel diameter inches
    /// </summary>
    /// <returns>Int</returns>
    public int LargestWaterParallelDiameterInches
    {
      get
      {
        return _LargestWaterCrossingDiameterInches;
      } // get
    } // LargestWaterParallelDiameterInches

    /// <summary>
    /// Dist to water parallel feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToWaterParallelFeet
    {
      get
      {
        return _DistToWaterParallelFeet;
      } // get
    } // DistToWaterParallelFeet

    /// <summary>
    /// Num sewer crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumSewerCrossings
    {
      get
      {
        return _NumSewerCrossings;
      } // get
    } // NumSewerCrossings

    /// <summary>
    /// Smallest sewer crossing diameter inches
    /// </summary>
    /// <returns>Int</returns>
    public int SmallestSewerCrossingDiameterInches
    {
      get
      {
        return _SmallestSewerCrossingDiameterInches;
      } // get
    } // SmallestSewerCrossingDiameterInches

    /// <summary>
    /// Largest sewer crossing diameter inches
    /// </summary>
    /// <returns>Int</returns>
    public int LargestSewerCrossingDiameterInches
    {
      get
      {
        return _LargestSewerCrossingDiameterInches;
      } // get
    } // LargestSewerCrossingDiameterInches

    /// <summary>
    /// Has sewer parallel
    /// </summary>
    /// <returns>Bool</returns>
    public bool HasSewerParallel
    {
      get
      {
        return _HasSewerParallel;
      } // get
    } // HasSewerParallel

    /// <summary>
    /// Largest sewer parallel diameter inches
    /// </summary>
    /// <returns>Int</returns>
    public int LargestSewerParallelDiameterInches
    {
      get
      {
        return _LargestSewerParallelDiameterInches;
      } // get
    } // LargestSewerParallelDiameterInches

    /// <summary>
    /// Dist to sewer parallel feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToSewerParallelFeet
    {
      get
      {
        return _DistToSewerParallelFeet;
      } // get
    } // DistToSewerParallelFeet

    /// <summary>
    /// Num street crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumStreetCrossings
    {
      get
      {
        return _NumStreetCrossings;
      } // get
    } // NumStreetCrossings

    /// <summary>
    /// Num arterial crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumArterialCrossings
    {
      get
      {
        return _NumArterialCrossings;
      } // get
    } // NumArterialCrossings

    /// <summary>
    /// Num major arterial crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumMajorArterialCrossings
    {
      get
      {
        return _NumMajorArterialCrossings;
      } // get
    } // NumMajorArterialCrossings

    /// <summary>
    /// Num freeway crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumFreewayCrossings
    {
      get
      {
        return _NumFreewayCrossings;
      } // get
    } // NumFreewayCrossings

    /// <summary>
    /// Is in street
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsInStreet
    {
      get
      {
        return _IsInStreet;
      } // get
    } // IsInStreet

    /// <summary>
    /// Street type
    /// </summary>
    /// <returns>Street type</returns>
    public Enumerators.StreetTypeKind StreetType
    {
      get
      {
        return _StreetType;
      } // get
    } // StreetType

    /// <summary>
    /// Dist to street centerline feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToStreetCenterlineFeet
    {
      get
      {
        return _DistToStreetCenterlineFeet;
      } // get
    } // DistToStreetCenterlineFeet

    /// <summary>
    /// Num streets if us node in intersection
    /// </summary>
    /// <returns>Int</returns>
    public int NumStreetsIfUsNodeInIntersection
    {
      get
      {
        return _NumStreetsIfUSNodeInIntersection;
      } // get
    } // NumStreetsIfUsNodeInIntersection

    /// <summary>
    /// Dist US node to intersection feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistUSNodeToIntersectionFeet
    {
      get
      {
        return _DistUSNodeToIntersectionFeet;
      } // get
    } // DistUSNodeToIntersectionFeet

    /// <summary>
    /// Num streets if DS node in intersection
    /// </summary>
    /// <returns>Int</returns>
    public int NumStreetsIfDSNodeInIntersection
    {
      get
      {
        return _NumStreetsIfDSNodeInIntersection;
      } // get
    } // NumStreetsIfDSNodeInIntersection

    /// <summary>
    /// Dist DS node to intersection feet
    /// </summary>
    /// <returns>Indexer</returns>
    public int DistDSNodeToIntersectionFeet
    {
      get
      {
        return _DistDSNodeToIntersectionFeet;
      } // get
    } // DistDSNodeToIntersectionFeet

    /// <summary>
    /// Num railroad crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumRailroadCrossings
    {
      get
      {
        return _NumRailroadCrossings;
      } // get
    } // NumRailroadCrossings

    /// <summary>
    /// Has railroad parallel
    /// </summary>
    /// <returns>Bool</returns>
    public bool HasRailroadParallel
    {
      get
      {
        return _HasRailroadParallel;
      } // get
    } // HasRailroadParallel

    /// <summary>
    /// Dist to railroad parallel feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToRailroadParallelFeet
    {
      get
      {
        return _DistToRailroadParallelFeet;
      } // get
    } // DistToRailroadParallelFeet

    /// <summary>
    /// Num light rail crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumLightRailCrossings
    {
      get
      {
        return _NumLightRailCrossings;
      } // get
    } // NumLightRailCrossings

    /// <summary>
    /// Has light rail parallel
    /// </summary>
    /// <returns>Bool</returns>
    public bool HasLightRailParallel
    {
      get
      {
        return _HasLightRailParallel;
      } // get
    } // HasLightRailParallel

    /// <summary>
    /// Dist to light rail parallel feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToLightRailParallelFeet
    {
      get
      {
        return _DistToLightRailParallelFeet;
      } // get
    } // DistToLightRailParallelFeet

    /// <summary>
    /// Num fiber optic crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumFiberOpticCrossings
    {
      get
      {
        return _NumFiberOpticCrossings;
      } // get
    } // NumFiberOpticCrossings

    /// <summary>
    /// Has fiber optic parallel
    /// </summary>
    /// <returns>Bool</returns>
    public bool HasFiberOpticParallel
    {
      get
      {
        return _HasFiberOpticParallel;
      } // get
    } // HasFiberOpticParallel

    /// <summary>
    /// Dist to fiber optic parallel feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToFiberOpticParallelFeet
    {
      get
      {
        return _DistToFiberOpticParallelFeet;
      } // get
    } // DistToFiberOpticParallelFeet

    /// <summary>
    /// Num gas crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumGasCrossings
    {
      get
      {
        return _NumGasCrossings;
      } // get
    } // NumGasCrossings

    /// <summary>
    /// Has gas parallel
    /// </summary>
    /// <returns>Bool</returns>
    public bool HasGasParallel
    {
      get
      {
        return _HasGasParallel;
      } // get
    } // HasGasParallel

    /// <summary>
    /// Dist to gas parallel
    /// </summary>
    /// <returns>Int</returns>
    public int DistToGasParallel
    {
      get
      {
        return _DistToGasParallel;
      } // get
    } // DistToGasParallel

    /// <summary>
    /// Is in conservation zone
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsInConservationZone
    {
      get
      {
        return _IsInConservationZone;
      } // get
    } // IsInConservationZone

    /// <summary>
    /// Length in conservation zone feet
    /// </summary>
    /// <returns>Int</returns>
    public int LengthInConservationZoneFeet
    {
      get
      {
        return _LengthInConservationZoneFeet;
      } // get
    } // LengthInConservationZoneFeet

    /// <summary>
    /// Is in preservation zone
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsInPreservationZone
    {
      get
      {
        return _IsInPreservationZone;
      } // get
    } // IsInPreservationZone

    /// <summary>
    /// Length in preservation zone feet
    /// </summary>
    /// <returns>Int</returns>
    public int LengthInPreservationZoneFeet
    {
      get
      {
        return _LengthInPreservationZoneFeet;
      } // get
    } // LengthInPreservationZoneFeet

    /// <summary>
    /// Area conservation zone sq ft
    /// </summary>
    /// <returns>Int</returns>
    public int AreaConservationZoneSqFt
    {
      get
      {
        return _AreaConservationZoneSqFt;
      } // get
    } // AreaConservationZoneSqFt

    /// <summary>
    /// Area preservation zone sq ft
    /// </summary>
    /// <returns>Int</returns>
    public int AreaPreservationZoneSqFt
    {
      get
      {
        return _AreaPreservationZoneSqFt;
      } // get
    } // AreaPreservationZoneSqFt

    /// <summary>
    /// Is near contamination site
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsNearContaminationSite
    {
      get
      {
        return _IsNearContaminationSite;
      } // get
    } // IsNearContaminationSite

    /// <summary>
    /// Dist to nearest ecsi feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToNearestEcsiFeet
    {
      get
      {
        return _DistToNearestEcsiFeet;
      } // get
    } // DistToNearestEcsiFeet

    /// <summary>
    /// Vol hazardous materials cu yd
    /// </summary>
    /// <returns>Int</returns>
    public int VolHazardousMaterialsCuYd
    {
      get
      {
        return _VolHazardousConflictCuYd;
      } // get
    } // VolHazardousMaterialsCuYd

    /// <summary>
    /// Is near school
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsNearSchool
    {
      get
      {
        return _IsNearSchool;
      } // get
    } // IsNearSchool

    /// <summary>
    /// Dist to school feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToSchoolFeet
    {
      get
      {
        return _DistToSchoolFeet;
      } // get
    } // DistToSchoolFeet

    /// <summary>
    /// Is near hospital
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsNearHospital
    {
      get
      {
        return _IsNearHospital;
      } // get
    } // IsNearHospital

    /// <summary>
    /// Dist to hospital feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToHospitalFeet
    {
      get
      {
        return _DistToHospitalFeet;
      } // get
    } // DistToHospitalFeet

    /// <summary>
    /// Is near police station
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsNearPoliceStation
    {
      get
      {
        return _IsNearPoliceStation;
      } // get
    } // IsNearPoliceStation

    /// <summary>
    /// Dist to police station feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToPoliceStationFeet
    {
      get
      {
        return _DistToPoliceStationFeet;
      } // get
    } // DistToPoliceStationFeet

    /// <summary>
    /// Is near fire station
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsNearFireStation
    {
      get
      {
        return _IsNearFireStation;
      } // get
    } // IsNearFireStation
    /// <summary>
    /// Dist to fire station feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToFireStationFeet
    {
      get
      {
        return _DistToFireStationFeet;
      } // get
    } // DistToFireStationFeet

    /// <summary>
    /// Num emergency route crossings
    /// </summary>
    /// <returns>Int</returns>
    public int NumEmergencyRouteCrossings
    {
      get
      {
        return _NumEmergencyRouteCrossings;
      } // get
    } // NumEmergencyRouteCrossings

    /// <summary>
    /// Is in emergency route
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsInEmergencyRoute
    {
      get
      {
        return _IsInEmergencyRoute;
      } // get
    } // IsInEmergencyRoute

    /// <summary>
    /// Dist to emergency route centerline feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToEmergencyRouteCenterlineFeet
    {
      get
      {
        return _DistToEmergencyRouteCenterlineFeet;
      } // get
    } // DistToEmergencyRouteCenterlineFeet

    /// <summary>
    /// USNode in MS4
    /// </summary>
    /// <returns>Bool</returns>
    public bool USNodeInMS4
    {
      get
      {
        return _USNodeInMS4;
      } // get
    } // USNodeInMS4

    /// <summary>
    /// USNode in UIC
    /// </summary>
    /// <returns>Bool</returns>
    public bool USNodeInUIC
    {
      get
      {
        return _USNodeInUIC;
      } // get
    } // USNodeInUIC

    /// <summary>
    /// USNode depth
    /// </summary>
    /// <returns>Int</returns>
    public double USNodeDepth
    {
      get
      {
        return _USNodeDepth;
      } // get
    } // USNodeDepth

    /// <summary>
    /// DSNode depth
    /// </summary>
    /// <returns>Int</returns>
    public double DSNodeDepth
    {
      get
      {
        return _DSNodeDepth;
      } // get
    } // DSNodeDepth

    /// <summary>
    /// Surface slope pct
    /// </summary>
    /// <returns>Int</returns>
    public int SurfaceSlopePct
    {
      get
      {
        return _SurfaceSlopePct;
      } // get
    } // SurfaceSlopePct

    /// <summary>
    /// Is near building
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsNearBuilding
    {
      get
      {
        return _IsNearBuilding;
      }
    } // IsNearBuilding

    /// <summary>
    /// Dist to building feet
    /// </summary>
    /// <returns>Int</returns>
    public int DistToBuildingFeet
    {
      get
      {
        return _DistToBuildingFeet;
      } // get
    } // DistToBuildingFeet

    /// <summary>
    /// Is near hydrant
    /// </summary>
    /// <returns>Int</returns>
    public bool IsNearHydrant
    {
      get
      {
        return _IsNearHydrant;
      } // get
    } // IsNearHydrant

    /// <summary>
    /// Dist to hydrant
    /// </summary>
    /// <returns>Int</returns>
    public int DistToHydrant
    {
      get
      {
        return _DistToHydrant;
      } // get
    } // DistToHydrant

    /// <summary>
    /// Is hard area
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsHardArea
    {
      get
      {
        return _IsHardArea;
      } // get
    } // IsHardArea
    #endregion
  }
}

