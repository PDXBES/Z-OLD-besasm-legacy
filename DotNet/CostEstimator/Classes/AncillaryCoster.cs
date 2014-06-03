// Project: Classes, File: AncillaryCoster.cs
// Namespace: CostEstimator.Classes, Class: AncillaryCoster
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 18, Size of file: 256 Bytes
// Creation date: 5/12/2008 1:08 PM
// Last modified: 1/6/2011 9:33 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;
using SystemsAnalysis.Modeling;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Supplementary costing engine for ancillary costs incurred by positional
  /// qualities such as overlaid utilities, transportation, and special
  /// environmental zones
  /// </summary>
  public class AncillaryCoster
  {
    #region Variables
    private AltPipXP _AltPipXP;
    private AltLink _AltLink;
    private AlternativePackage _AltPackage;

    private PipXP _PipXP;
    private Link _Link;
    private Model _Model;

    private PipeCoster _PipeCoster = new PipeCoster();

    private Segment _Segment;
    private Conflict _Conflict;
    #endregion

    #region Constructors
    /// <summary>
    /// Create ancillary coster for an alternative package
    /// </summary>
    /// <param name="altPackage">The alternative package being processed</param>
    public AncillaryCoster(AlternativePackage altPackage)
    {
      Type = AncillaryCosterType.Alt;
      _AltPackage = altPackage;
      _Model = null;
    } // AncillaryCoster(altPackage)

    /// <summary>
    /// Create ancillary coster for a model
    /// </summary>
    /// <param name="model">The model being processed</param>
    public AncillaryCoster(Model model)
    {
      Type = AncillaryCosterType.Model;
      _Model = model;
      _AltPackage = null;
    } // AncillaryCoster(model)

    public AncillaryCoster(Segment segment, Conflict conflict, int lengthFt, float diamWidthIn, float USIE, float DSIE,
      string material, string USNode, string DSNode)
    {
      Type = AncillaryCosterType.Rehab;
      _Segment = segment;
      _Conflict = conflict;

      DataAccess.ModelDataSet.MdlLinksDataTable tempLinksTable = new DataAccess.ModelDataSet.MdlLinksDataTable();
      DataAccess.ModelDataSet.MdlLinksRow tempLinksRow = tempLinksTable.NewMdlLinksRow();
      tempLinksRow.MLinkID = 1;
      tempLinksRow.LinkID = 1;
      tempLinksRow.IsSpecLink = false;
      tempLinksRow.Height = 0.0;
      tempLinksRow.LinkType = "C";
      tempLinksRow.Length = lengthFt;
      tempLinksRow.DiamWidth = diamWidthIn;
      tempLinksRow.UsIE = USIE;
      tempLinksRow.DsIE = DSIE;
      tempLinksRow.Material = material.ToString();
      tempLinksRow.USNode = USNode;
      tempLinksRow.DSNode = DSNode;
      _Link = new Link(tempLinksRow);

      DataAccess.ModelDataSet.MdlPipXPDataTable tempTable = new DataAccess.ModelDataSet.MdlPipXPDataTable();
      DataAccess.ModelDataSet.MdlPipXPRow tempRow = tempTable.NewMdlPipXPRow();
      tempRow.MLinkID = conflict.ID;
      tempRow.USNode = conflict.USNodeID;
      tempRow.DSNode = conflict.DSNodeID;
      tempRow.COMPKEY = conflict.HansenCompkey;
      tempRow.xWtr = conflict.NumWaterCrossings;
      tempRow.xWMinD = conflict.WaterCrossingMinDiamWidthIn;
      tempRow.xWMaxD = conflict.WaterCrossingMaxDiamWidthIn;
      tempRow.pWtr = conflict.NumWaterParallels;
      tempRow.pWtrMaxD = conflict.WaterParallelsMaxDiamWidthIn;
      tempRow.pFt2Wtr = conflict.DistFtToNearestWaterParallel;
      tempRow.xSewer = conflict.NumSewerCrossings;
      tempRow.xSwrMinD = conflict.SewerCrossingMinDiamWidthIn;
      tempRow.xSwrMaxD = conflict.SewerCrossingMaxDiamWidthIn;
      tempRow.pSewer = conflict.NumSewerParallels;
      tempRow.pSwrMaxD = conflict.SewerParallelsMaxDiamWidthIn;
      tempRow.pFt2Swr = conflict.DistFtToNearestSewerParallel;
      tempRow.xStrt = conflict.NumStreetCrossings;
      tempRow.xArt = conflict.NumArterialCrossings;
      tempRow.xMJArt = conflict.NumMajorArterialCrossings;
      tempRow.xFrwy = conflict.NumFreewayCrossings;
      tempRow.pStrt = conflict.NumStreetParallels;
      tempRow.pStrtTyp = conflict.StreetParallelsType;
      tempRow.pFt2Strt = conflict.DistFtToNearestStreetParallel;
      tempRow.pTraffic = conflict.StreetParallelsTrafficCount;
      tempRow.uxCLx = conflict.NumStreetsWithUSNodeAtIntersection;
      tempRow.uxFt2CLx = conflict.DistToStreetsWithUSNodeAtIntersection;
      tempRow.dxCLx = conflict.NumStreetsWithDSNodeAtIntersection;
      tempRow.dxFt2CLx = conflict.DistToStreetsWithDSNodeAtIntersection;
      tempRow.xFiber = conflict.NumFiberCrossings;
      tempRow.pFiber = conflict.NumFiberParallels;
      tempRow.pFt2Fiber = conflict.DistFtToNearestFiberParallel;
      tempRow.xGas = conflict.NumGasCrossings;
      tempRow.pGas = conflict.NumGasParallels;
      tempRow.pFt2Gas = conflict.DistFtToNearestGasParallel;
      tempRow.xRail = conflict.NumRailCrossings;
      tempRow.pRail = conflict.NumRailParallels;
      tempRow.pFt2Rail = conflict.DistFtToNearestRailParallel;
      tempRow.xLRT = conflict.NumLRTCrossings;
      tempRow.pLRT = conflict.NumLRTParallels;
      tempRow.pFt2LRT = conflict.DistFtToNearestLRTParallel;
      tempRow.xEmt = conflict.NumEMTCrossings;
      tempRow.pEmt = conflict.NumEMTParallels;
      tempRow.pFt2Emt = conflict.DistFtToNearestEMTParallel;
      tempRow.xEzonC = conflict.InConservationZone ? 1 : 0;
      tempRow.xEzonP = conflict.InPreservationZone ? 1 : 0;
      tempRow.xFtEzonC = conflict.LengthFtInsideConservationZone;
      tempRow.xFtEzonP = conflict.LengthFtInsidePreservationZone;
      tempRow.xEzAreaC = conflict.AreaSqFtInsideConservationZone;
      tempRow.xEzAreaP = conflict.AreaSqFtInsidePreservationZone;
      tempRow.uxMS4 = conflict.USNodeInMS4 ? 1 : 0;
      tempRow.uxUIC = conflict.USNodeInUIC ? 1 : 0;
      tempRow.uDepth = conflict.USDepth;
      tempRow.dDepth = conflict.DSDepth;
      tempRow.xPipSlope = conflict.PipeSlope;
      tempRow.gSlope = (Int16)(conflict.GroundSlope * 100);
      tempRow.xEcsi = conflict.NearContaminationSite ? 1 : 0;
      tempRow.xFt2Ecsi = conflict.DistFtToContaminationSite;
      tempRow.xEcsiLen = conflict.LengthFtInsideContaminationSite;
      tempRow.xEcsiVol = conflict.VolumeCuFtInsideContaminationSite;
      tempRow.xSchl = conflict.NearSchool ? 1 : 0;
      tempRow.xFt2Schl = conflict.DistFtToNearestSchool;
      tempRow.xHosp = conflict.NearHospital ? 1 : 0;
      tempRow.xFt2Hosp = conflict.DistFtToNearestHospital;
      tempRow.xPol = conflict.NearPoliceStation ? 1 : 0;
      tempRow.xFt2Pol = conflict.DistFtToNearestPoliceStation;
      tempRow.xFire = conflict.NearFireStation ? 1 : 0;
      tempRow.xFt2Fire = conflict.DistFtToNearestFireStation;
      tempRow.xBldg = conflict.NearBuilding ? 1 : 0;
      tempRow.xFt2Bldg = conflict.DistFtToNearestBuilding;
      tempRow.xHyd = conflict.NearHydrant ? 1 : 0;
      tempRow.xFt2Hyd = conflict.DistFtToNearestHydrant;
      tempRow.HardArea = conflict.InHardArea ? 1 : 0;

      _PipXP = new PipXP(tempRow);
    }
    #endregion

    #region Properties
    // Type of ancillary coster
    public AncillaryCosterType Type { get; private set; }
    /// <summary>
    /// Returns the current AltPipXP record
    /// </summary>
    /// <returns>An AltPipXP record</returns>
    public AltPipXP AltPipXP
    {
      get
      {
        if (_AltPackage != null)
          return _AltPipXP;
        else
          return null;
      } // get

      set
      {
        if (_AltPackage != null)
          _AltPipXP = value;
      } // set
    } // AltPipXP

    /// <summary>
    /// Returns the current AltLink record
    /// </summary>
    /// <returns>An AltLink record</returns>
    public AltLink AltLink
    {
      get
      {
        if (_AltPackage != null)
          return _AltLink;
        else
          return null;
      } // get

      set
      {
        if (_AltPackage != null)
          _AltLink = value;
      } // set
    } // AltLink

    /// <summary>
    /// Returns the current PipXp record
    /// </summary>
    /// <returns>A PipXp record</returns>
    public PipXP PipXP
    {
      get
      {
        if (_Model != null)
          return _PipXP;
        else
          return null;
      } // get

      set
      {
        if (_Model != null)
          _PipXP = value;
      } // set
    } // PipXP

    /// <summary>
    /// Returns the current link being processed
    /// </summary>
    /// <returns>A Link record</returns>
    public Link Link
    {
      get
      {
        if (_Model != null)
          return _Link;
        else
          return null;
      } // get

      set
      {
        if (_Model != null)
          _Link = value;
      } // set
    } // Link

    /// <summary>
    /// Calculates and packages ancillary costs for a given link's conflict package
    /// </summary>
    /// <param name="ancillaryCosts">Reference to list that will contain ancillary costs</param>
    /// <param name="conflictPackage">Conflict package from link that will be used to get ancillary costs</param>
    private static void GetAncillaryCosts(List<AncillaryCost> ancillaryCosts, ConflictPackage conflictPackage)
    {
      ancillaryCosts.Add(new BoringJackingAncillaryCost(conflictPackage).AncillaryCost);
      ancillaryCosts.Add(new TrafficControlAncillaryCost(conflictPackage).AncillaryCost);
      ancillaryCosts.Add(new ParallelRelocationAncillaryCost(conflictPackage).AncillaryCost);
      ancillaryCosts.Add(new CrossingRelocationAncillaryCost(conflictPackage).AncillaryCost);
      ancillaryCosts.Add(new BypassPumpingAncillaryCost(conflictPackage).AncillaryCost);
      ancillaryCosts.Add(new EnvironmentalMitigationAncillaryCost(conflictPackage).AncillaryCost);
      ancillaryCosts.Add(new HazardousMaterialAncillaryCost(conflictPackage).AncillaryCost);

      // Clean up the list (nulls might be there if an ancillary cost comes back null
      for (int i = ancillaryCosts.Count - 1; i >= 0; i--)
      {
        if (ancillaryCosts[i] == null)
          ancillaryCosts.RemoveAt(i);
      }
    }

    /// <summary>
    /// Packages ancillary factors for a given link's conflict package
    /// </summary>
    /// <param name="ancillaryFactors">Reference to list that will contain ancillary factors</param>
    /// <param name="conflictPackage">Conflict package from link that will be used to get ancillary factors</param>
    private static void GetAncillaryFactors(List<AncillaryFactor> ancillaryFactors, ConflictPackage conflictPackage)
    {
      ancillaryFactors.Add(new DifficultAreaAncillaryFactor(conflictPackage).AncillaryFactor);

      for (int i = ancillaryFactors.Count - 1; i >= 0 ; i--)
      {
        if (ancillaryFactors[i] == null)
          ancillaryFactors.RemoveAt(i);
      }
    } // GetAncillaryFactors(ancillaryFactors, conflictPackage)

    /// <summary>
    /// Returns list of ancillary costs from the current alternative package
    /// </summary>
    /// <returns>List of ancillary costs</returns>
    public List<AncillaryCost> AlternativeAncillaryCosts
    {
      get
      {
        List<AncillaryCost> ancillaryCosts = new List<AncillaryCost>();

        ConflictPackage altConflictPackage = new ConflictPackage(_AltPackage, _AltLink, _AltPipXP);
        GetAncillaryCosts(ancillaryCosts, altConflictPackage);

        return ancillaryCosts;
      } // get
    } // AncillaryCosts

    /// <summary>
    /// Returns list of ancillary factors from the current alternative package
    /// </summary>
    /// <returns>List of ancillary factors</returns>
    public List<AncillaryFactor> AlternativeAncillaryFactors
    {
      get
      {
        List<AncillaryFactor> ancillaryFactors = new List<AncillaryFactor>();

        ConflictPackage altConflictPackage = new ConflictPackage(_AltPackage, _AltLink, _AltPipXP);
        GetAncillaryFactors(ancillaryFactors, altConflictPackage);

        return ancillaryFactors;
      }
    } // AncillaryFactors

    /// <summary>
    /// Returns list of ancillary costs from the current model
    /// </summary>
    /// <returns>List of ancillary costs</returns>
    public List<AncillaryCost> ModelAncillaryCosts
    {
      get
      {
        List<AncillaryCost> ancillaryCosts = new List<AncillaryCost>();

        ConflictPackage modelConflictPackage = new ConflictPackage(_Model, _Link, _PipXP);
        GetAncillaryCosts(ancillaryCosts, modelConflictPackage);

        return ancillaryCosts;
      } // get
    } // ModelAncillaryCosts

    /// <summary>
    /// Returns list of ancillary factors from the current model
    /// </summary>
    /// <returns>List of ancillary factors</returns>
    public List<AncillaryFactor> ModelAncillaryFactors
    {
      get
      {
        List<AncillaryFactor> ancillaryFactors = new List<AncillaryFactor>();

        ConflictPackage modelConflictPackage = new ConflictPackage(_Model, _Link, _PipXP);
        GetAncillaryFactors(ancillaryFactors, modelConflictPackage);
        return ancillaryFactors;
      } // get
    } // ModelAncillaryFactors

    /// <summary>
    /// Current alternative conflict package
    /// </summary>
    /// <returns>Conflict package</returns>
    public ConflictPackage CurrentAltConflictPackage
    {
      get
      {
        return new ConflictPackage(_AltPackage, _AltLink, _AltPipXP);
      }
    } // CurrentConflictPackage

    /// <summary>
    /// Current conflict package
    /// </summary>
    /// <returns>Conflict package</returns>
    public ConflictPackage CurrentConflictPackage
    {
      get
      {
        return Type == AncillaryCosterType.Rehab 
          ? new ConflictPackage(_Segment, _Conflict, _PipXP)
          : new ConflictPackage(_Model, _Link, _PipXP);
      }
    } // CurrentConflictPackage

    #endregion
  }
}
