// Project: SystemsAnalysis.Modeling, File: PipXP.cs
// Namespace: SystemsAnalysis.Modeling, Class: PipXP
// Path: C:\Development\DotNet\Modeling, Author: Arnel
// Code lines: 1040, Size of file: 22.80 KB
// Creation date: 6/30/2008 11:03 AM
// Last modified: 8/26/2008 11:42 AM

using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.Modeling
{
	public class PipXP : PipeConflict
	{
		#region Variables
    private SortedList<double, double> _parallelWaterLookup = new SortedList<double, double>();
    private SortedList<double, double> _parallelSewerLookup = new SortedList<double, double>();

    // Identifying data
		private int _MstLinkID; // MLINKID
		private string _USNode; // USNODE
		private string _DSNode; // DSNODE
		private int _CompKey; // COMPKEY

    // Location data
    private double _USX; // xa
    private double _USY; // ya
    private double _DSX; // xb
    private double _DSY; // yb
    private double _Azimuth; // Deg2N

		// Water facilities
		private int _NumWaterCrossings; // xWtr
		private int _SmallestWaterCrossingDiameterInches; // xWMinD
		private int _LargestWaterCrossingDiameterInches; // xWMaxD
		private bool _HasWaterParallel; // pWtr
    private double _ParallelWaterLenWithin2ft; // pWtr2
    private double _ParallelWaterLenWithin4ft; // pWtr4
    private double _ParallelWaterLenWithin6ft; // pWtr6
    private double _ParallelWaterLenWithin8ft; // pWtr8
    private double _ParallelWaterLenWithin10ft; // pWtr10
    private double _ParallelWaterLenWithin12ft; // pWtr12
		private int _LargestWaterParallelDiameterInches; // pWtrMaxD
		private int _DistToWaterParallelFeet; // pFt2Wtr

		// Sewer Facilities
		private int _NumSewerCrossings; // xSewer
		private int _SmallestSewerCrossingDiameterInches; // xSwrMinD
		private int _LargestSewerCrossingDiameterInches; // xSwrMaxD
		private bool _HasSewerParallel; // pSewer
    private double _ParallelSewerLenWithin2ft; // pSwr2
    private double _ParallelSewerLenWithin4ft; // pSwr4
    private double _ParallelSewerLenWithin6ft; // pSwr6
    private double _ParallelSewerLenWithin8ft; // pSwr8
    private double _ParallelSewerLenWithin10ft; // pSwr10
		private int _LargestSewerParallelDiameterInches; // pSwrMaxD
		private int _DistToSewerParallelFeet; // pFt2Swr

		// Transportation facilities
		private int _NumStreetCrossings; // xStrt
		private int _NumArterialCrossings; // xArt
		private int _NumMajorArterialCrossings; // xMJArt
		private int _NumFreewayCrossings; // xFrwy
		private bool _IsInStreet; //pStrt
		private Enumerators.StreetTypeKind _StreetType; // pStrtTyp
		private int _DistToStreetCenterlineFeet; // pFt2Strt
		private int _NumStreetsIfUSNodeInIntersection; // uxCLx
		private int _DistUSNodeToIntersectionFeet; // uxFt2CLx
		private int _NumStreetsIfDSNodeInIntersection; // dxCLx
		private int _DistDSNodeToIntersectionFeet; // dxFt2CLx
    private int _TrafficVolVehiclesPerDay; // pTraffic

		// Railroad facilities
		private int _NumRailroadCrossings; // xRail
		private bool _HasRailroadParallel; // pRail
		private int _DistToRailroadParallelFeet; // pFt2Rail
		private int _NumLightRailCrossings; // xLRT
		private bool _HasLightRailParallel; // pLRT
		private int _DistToLightRailParallelFeet; // pFt2LRT

		// Fiber Optic & Gas facilities
		private int _NumFiberOpticCrossings; // xFiber
		private bool _HasFiberOpticParallel; // pFiber
		private int _DistToFiberOpticParallelFeet; // pFt2Fiber
		private int _NumGasCrossings; // xGas
		private bool _HasGasParallel; // pGas
		private int _DistToGasParallel; // pFt2Gas

		// Environmental zones
		private bool _IsInConservationZone; // xEzonC
		private int _LengthInConservationZoneFeet; // xFtEzonC
		private int _AreaConservationZoneSqFt; // xEzAreaC
		private bool _IsInPreservationZone; // xEzonP
		private int _LengthInPreservationZoneFeet; // xFtEzonP
		private int _AreaPreservationZoneSqFt; // xEzAreaP
		private bool _IsNearContaminationSite; // xEcsi
		private int _DistToNearestEcsiFeet; // xFt2Ecsi
    private double _LengthInEcsiFeet; // xEcsiLen
		private int _VolHazardousConflictCuYd; // xEcsiVol
    private bool _IsNearLUST; // xLUST
    private int _DistToNearestLUSTFeet; // xFt2LUST

		// Public safety
		private bool _IsNearSchool; // xSchl
		private int _DistToSchoolFeet; // xFt2Schl
		private bool _IsNearHospital; // xHosp
		private int _DistToHospitalFeet; // xFt2Hosp
		private bool _IsNearPoliceStation; // xPol
		private int _DistToPoliceStationFeet; // xFt2Pol
		private bool _IsNearFireStation; // xFire
		private int _DistToFireStationFeet; // xFt2Fire
		private int _NumEmergencyRouteCrossings; // xEmt
		private bool _IsInEmergencyRoute; // pEmt
		private int _DistToEmergencyRouteCenterlineFeet; // pFt2Emt

		// Other
		private bool _USNodeInMS4; // uxMS4
		private bool _USNodeInUIC; // uxUIC
		private double _USNodeDepth; // uDepth
		private double _DSNodeDepth; // dDepth
		private int _SurfaceSlopePct; // gSlope
		private bool _IsNearBuilding; // xBldg
		private int _DistToBuildingFeet; // xFt2Bldg
		private bool _IsNearHydrant; // xHyd
		private int _DistToHydrant; // xFt2Hyd
		private bool _IsHardArea; // HardArea
		#endregion

		#region Constructors
		public PipXP(DataAccess.ModelDataSet.MdlPipXPRow mdlPipXPRow)
		{
			_MstLinkID = Convert.ToInt32(mdlPipXPRow.MLINKID);
			_USNode = mdlPipXPRow.USNODE;
			_DSNode = mdlPipXPRow.DSNODE;
			_CompKey = Convert.ToInt32(mdlPipXPRow.COMPKEY);
      _USX = mdlPipXPRow.xa;
      _USY = mdlPipXPRow.ya;
      _DSX = mdlPipXPRow.xb;
      _DSY = mdlPipXPRow.yb;
      _Azimuth = mdlPipXPRow.Deg2N;

			_NumWaterCrossings = mdlPipXPRow.xWtr;
			_SmallestWaterCrossingDiameterInches = mdlPipXPRow.xWMinD;
			_LargestWaterCrossingDiameterInches = mdlPipXPRow.xWMaxD;
			_HasWaterParallel = mdlPipXPRow.pWtr >= 1;
      _ParallelWaterLenWithin2ft = mdlPipXPRow.pWtr2;
      _ParallelWaterLenWithin4ft = mdlPipXPRow.pWtr4;
      _ParallelWaterLenWithin6ft = mdlPipXPRow.pWtr6;
      _ParallelWaterLenWithin8ft = mdlPipXPRow.pWtr8;
      _ParallelWaterLenWithin10ft = mdlPipXPRow.pWtr10;
      _ParallelWaterLenWithin12ft = mdlPipXPRow.pWtr12;
			_LargestWaterParallelDiameterInches = mdlPipXPRow.pWtrMaxD;
			_DistToWaterParallelFeet = mdlPipXPRow.pFt2Wtr;

      _parallelWaterLookup.Add(2, _ParallelWaterLenWithin2ft);
      _parallelWaterLookup.Add(4, _ParallelWaterLenWithin4ft);
      _parallelWaterLookup.Add(6, _ParallelWaterLenWithin6ft);
      _parallelWaterLookup.Add(8, _ParallelWaterLenWithin8ft);
      _parallelWaterLookup.Add(10, _ParallelWaterLenWithin10ft);
      _parallelWaterLookup.Add(12, _ParallelWaterLenWithin12ft);

			_NumSewerCrossings = mdlPipXPRow.xSewer;
			_SmallestSewerCrossingDiameterInches = mdlPipXPRow.xSwrMinD;
			_LargestSewerCrossingDiameterInches = mdlPipXPRow.xSwrMaxD;
			_HasSewerParallel = mdlPipXPRow.pSewer >= 1;
      _ParallelSewerLenWithin2ft = mdlPipXPRow.pSwr2;
      _ParallelSewerLenWithin4ft = mdlPipXPRow.pSwr4;
      _ParallelSewerLenWithin6ft = mdlPipXPRow.pSwr6;
      _ParallelSewerLenWithin8ft = mdlPipXPRow.pSwr8;
      _ParallelSewerLenWithin10ft = mdlPipXPRow.pSwr10;
			_LargestSewerParallelDiameterInches = mdlPipXPRow.pSwrMaxD;
			_DistToSewerParallelFeet = mdlPipXPRow.pFt2Swr;

      _parallelSewerLookup.Add(2, _ParallelSewerLenWithin2ft);
      _parallelSewerLookup.Add(4, _ParallelSewerLenWithin4ft);
      _parallelSewerLookup.Add(6, _ParallelSewerLenWithin6ft);
      _parallelSewerLookup.Add(8, _ParallelSewerLenWithin8ft);
      _parallelSewerLookup.Add(10, _ParallelSewerLenWithin10ft);

      _NumStreetCrossings = mdlPipXPRow.xStrt;
			_NumArterialCrossings = mdlPipXPRow.xArt;
			_NumMajorArterialCrossings = mdlPipXPRow.xMJArt;
			_NumFreewayCrossings = mdlPipXPRow.xFrwy;
			_IsInStreet = mdlPipXPRow.pStrt >= 1;
			switch (mdlPipXPRow.pStrtTyp)
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
      } // switch
			_DistToStreetCenterlineFeet = mdlPipXPRow.pFt2Strt;
			_NumStreetsIfUSNodeInIntersection = mdlPipXPRow.uxCLx;
			_DistUSNodeToIntersectionFeet = mdlPipXPRow.uxFt2CLx;
			_NumStreetsIfDSNodeInIntersection = mdlPipXPRow.dxCLx;
			_DistDSNodeToIntersectionFeet = mdlPipXPRow.dxFt2CLx;
			_TrafficVolVehiclesPerDay = mdlPipXPRow.pTraffic;

			_NumRailroadCrossings = mdlPipXPRow.xRail;
			_HasRailroadParallel = mdlPipXPRow.pRail >= 1;
			_DistToRailroadParallelFeet = mdlPipXPRow.pFt2Rail;
			_NumLightRailCrossings = mdlPipXPRow.xLRT;
			_HasLightRailParallel = mdlPipXPRow.pLRT >= 1;
			_DistToLightRailParallelFeet = mdlPipXPRow.pFt2LRT;

			_NumFiberOpticCrossings = mdlPipXPRow.xFiber;
			_HasFiberOpticParallel = mdlPipXPRow.pFiber == 1;
			_DistToFiberOpticParallelFeet = mdlPipXPRow.pFt2Fiber;
			_NumGasCrossings = mdlPipXPRow.xGas;
			_HasGasParallel = mdlPipXPRow.pGas == 1;
			_DistToGasParallel = mdlPipXPRow.pFt2Gas;

			_IsInConservationZone = mdlPipXPRow.xEzonC == 1;
			_LengthInConservationZoneFeet = mdlPipXPRow.xFtEzonC;
			_AreaConservationZoneSqFt = mdlPipXPRow.xEzAreaC;
			_IsInPreservationZone = mdlPipXPRow.xEzonP == 1;
			_LengthInPreservationZoneFeet = mdlPipXPRow.xFtEzonP;
			_AreaPreservationZoneSqFt = mdlPipXPRow.xEzAreaP;
			_IsNearContaminationSite = mdlPipXPRow.xEcsi == 1;
			_DistToNearestEcsiFeet = mdlPipXPRow.xFt2Ecsi;
      _LengthInEcsiFeet = mdlPipXPRow.xEcsiLen;
      _IsNearLUST = mdlPipXPRow.xLUST == 1;
      _DistToNearestLUSTFeet = mdlPipXPRow.xFt2LUST;
			_VolHazardousConflictCuYd = mdlPipXPRow.xEcsiVol;

			_IsNearSchool = mdlPipXPRow.xSchl == 1;
			_DistToSchoolFeet = mdlPipXPRow.xFt2Schl;
			_IsNearHospital = mdlPipXPRow.xHosp == 1;
			_DistToHospitalFeet = mdlPipXPRow.xFt2Hosp;
			_IsNearPoliceStation = mdlPipXPRow.xPol == 1;
			_DistToPoliceStationFeet = mdlPipXPRow.xFt2Pol;
			_IsNearFireStation = mdlPipXPRow.xFire == 1;
			_DistToFireStationFeet = mdlPipXPRow.xFt2Fire;
			_NumEmergencyRouteCrossings = mdlPipXPRow.xEmt;
			_IsInEmergencyRoute = mdlPipXPRow.pEmt == 1;
			_DistToEmergencyRouteCenterlineFeet = mdlPipXPRow.pFt2Emt;

			_USNodeInMS4 = mdlPipXPRow.uxMS4 == 1;
			_USNodeInUIC = mdlPipXPRow.uxUIC == 1;
			_USNodeDepth = mdlPipXPRow.uDepth;
			_DSNodeDepth = mdlPipXPRow.dDepth;
			_SurfaceSlopePct = Convert.ToInt32(mdlPipXPRow.gSlope);
			_IsNearBuilding = mdlPipXPRow.xBldg == 1;
			_DistToBuildingFeet = mdlPipXPRow.xFt2Bldg;
			_IsNearHydrant = mdlPipXPRow.xHyd == 1;
			_DistToHydrant = mdlPipXPRow.xFt2Hyd;
			_IsHardArea = mdlPipXPRow.HardArea == 1;
		} // AltPipXP(altPipXPRow)
		#endregion

		#region Properties
		public int MstLinkID
		{
			get
			{
				return _MstLinkID;
			} // get
		} // MstLinkID

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
    /// Upstream X-coordinate
    /// </summary>
    public double US_XCoord
    {
      get
      {
        return _USX;
      }
    }

    /// <summary>
    /// Upstream Y-coordinate
    /// </summary>
    public double US_YCoord
    {
      get
      {
        return _USY;
      }
    }

    /// <summary>
    /// Downstream X-coordinate
    /// </summary>
    public double DS_XCoord
    {
      get
      {
        return _DSX;
      }
    }

    /// <summary>
    /// Downstream y-coordinate
    /// </summary>
    public double DS_YCoord
    {
      get
      {
        return _DSY;
      }
    }

    /// <summary>
    /// Direction of line as compared to North (0 degrees), considering only direction of line
    /// from 0 to 180 degrees
    /// </summary>
    public double Direction
    {
      get
      {
        return _Azimuth;
      }
    }

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

    public double LengthOfPipeFtNearElementByFt(
      SortedList<double, double> elementDistTable,
      double distFromElement)
    {
      List<double> elementDistTableKeys = new List<double>(elementDistTable.Keys);
      int lookupIndex = elementDistTableKeys.BinarySearch(distFromElement);
      if (lookupIndex > 0)
        return elementDistTable.Values[lookupIndex];
      else
      {
        int nearestLookupIndex = ~lookupIndex;
        if ((nearestLookupIndex >= 0) && (nearestLookupIndex < elementDistTable.Count))
        {
          if (nearestLookupIndex > 0)
          {
            return elementDistTable.Values[nearestLookupIndex];
          }
          else
          {
            return elementDistTable.Values[0];
          }
        }
        else
        {
          return elementDistTable.Values[elementDistTable.Count - 1];
        }
      }
    }

    /// <summary>
    /// Returns the length of pipe considered parallel to a water pipe at less than the given distance
    /// </summary>
    /// <param name="distFromWater">Distance to consider from water pipes</param>
    /// <returns>Length of pipe from nearby parallel water pipes</returns>
    public double LengthOfPipeFtNearWaterByFt(double distFromWater)
    {
      return LengthOfPipeFtNearElementByFt(_parallelWaterLookup, distFromWater);
    }

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
    /// Returns the length of pipe considered parallel to a sewer pipe at less than the given distance
    /// </summary>
    /// <param name="distFromSewer">Distance to consider from sewer pipes</param>
    /// <returns>Length of pipe from nearby parallel sewer pipes</returns>
    public double LengthOfPipeFtNearSewerByFt(double distFromSewer)
    {
      return LengthOfPipeFtNearElementByFt(_parallelSewerLookup, distFromSewer);
    }

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
    /// Length of pipe influenced by ECSI in feet
    /// </summary>
    public double LengthInEcsiFeet
    {
      get
      {
        return _LengthInEcsiFeet;
      }
    }

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
    /// True if pipe is near a LUST (leaking underground storage tank)
    /// </summary>
    public bool IsNearLUST
    {
      get
      {
        return _IsNearLUST;
      }
    }

    /// <summary>
    /// Distance from pipe to nearest LUST
    /// </summary>
    public int DistToNearestLUSTFeet
    {
      get
      {
        return _DistToNearestLUSTFeet;
      }
    }

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
