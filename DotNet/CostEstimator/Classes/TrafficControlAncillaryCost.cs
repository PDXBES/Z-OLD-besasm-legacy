// Project: Classes, File: TrafficControlAncillaryCost.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: TrafficControlAncillaryCost
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 20, Size of file: 298 Bytes
// Creation date: 6/11/2008 5:47 PM
// Last modified: 6/12/2008 1:57 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;
using SystemsAnalysis.Types;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Traffic control costs, varying by traffic usage (arterials, freeways, 
  /// normal residential streets)
  /// </summary>
  /// <returns>Ancillary cost</returns>
  class TrafficControlAncillaryCost : AncillaryCost
  {
    #region Constants
    private const int TRAFFIC_CONTROL_STREET_COST_PER_DAY = 500;
    private const int TRAFFIC_CONTROL_ARTERIAL_COST_PER_DAY = 1000;
    private const int TRAFFIC_CONTROL_MAJOR_ARTERIAL_COST_PER_DAY = 3000;
    private const int TRAFFIC_CONTROL_FREEWAY_COST_PER_DAY = 0;
    #endregion

    #region Variables
    public ConflictPackage _ConflictPackage;
    private PipeCoster _coster;
    private ConstructionDurationCalculator _constructionDurationCalculator;
    private bool _isLiner;
    #endregion

    #region Constructors
    /// <summary>
    /// Create traffic control ancillary cost
    /// </summary>
    public TrafficControlAncillaryCost(ConflictPackage conflictPackage, PipeCoster coster,
      ConstructionDurationCalculator constructionDurationCalculator, bool isLiner = false)
    {
      _ConflictPackage = conflictPackage;
      _coster = coster;
      _constructionDurationCalculator = constructionDurationCalculator;
      _isLiner = false;
    } // TrafficControlAncillaryCost()
    #endregion

    #region Properties
    /// <summary>
    /// Name
    /// </summary>
    /// <returns>String</returns>
    public string Name
    {
      get
      {
        string streetKind = "";
        switch (_ConflictPackage.Conflicts.StreetType)
        {
          case Enumerators.StreetTypeKind.Street:
            streetKind = "Street";
            break;
          case Enumerators.StreetTypeKind.Arterial:
            streetKind = "Arterial";
            break;
          case Enumerators.StreetTypeKind.MajorArterial:
            streetKind = "Major arterial";
            break;
          case Enumerators.StreetTypeKind.Freeway:
            streetKind = "Freeway";
            break;
        } // switch
        return "Traffic control - " + streetKind;
      } // get
    } // Name

    /// <summary>
    /// Cost
    /// </summary>
    /// <returns>float</returns>
    public float Cost
    {
      get
      {
        return (float)(Units * (double)UnitCost);
      } // get
    } // Cost

    /// <summary>
    /// Unit cost
    /// </summary>
    /// <returns>float</returns>
    public float UnitCost
    {
      get
      {
        float unitCostPerDayOfTrafficControl = 0;
        switch (_ConflictPackage.Conflicts.StreetType)
        {
          case Enumerators.StreetTypeKind.Street:
            unitCostPerDayOfTrafficControl = TRAFFIC_CONTROL_STREET_COST_PER_DAY;
            break;
          case Enumerators.StreetTypeKind.Arterial:
            unitCostPerDayOfTrafficControl = TRAFFIC_CONTROL_ARTERIAL_COST_PER_DAY;
            break;
          case Enumerators.StreetTypeKind.MajorArterial:
            unitCostPerDayOfTrafficControl = TRAFFIC_CONTROL_MAJOR_ARTERIAL_COST_PER_DAY;
            break;
          case Enumerators.StreetTypeKind.Freeway:
            unitCostPerDayOfTrafficControl = TRAFFIC_CONTROL_FREEWAY_COST_PER_DAY;
            break;
        } // switch
        return unitCostPerDayOfTrafficControl;
      } // get
    } // UnitCost

    /// <summary>
    /// Unit
    /// </summary>
    /// <returns>String</returns>
    public string Unit
    {
      get
      {
        return "man-days";
      } // get
    } // Unit

    /// <summary>
    /// Units
    /// </summary>
    /// <returns>Double</returns>
    public float Units
    {
      get
      {
        // Base cost is two people for two streets "intersecting" at the segment. More is charged
        // if there are additional streets intersecting
        int baseStreets = 2;
        int numAdditionalStreets =
          (_ConflictPackage.Conflicts.NumStreetsIfUsNodeInIntersection > baseStreets ?
                    _ConflictPackage.Conflicts.NumStreetsIfUsNodeInIntersection :
                    _ConflictPackage.Conflicts.NumStreetsIfDSNodeInIntersection > baseStreets ?
                    _ConflictPackage.Conflicts.NumStreetsIfDSNodeInIntersection : baseStreets) - baseStreets;
        return (_isLiner ? 3 : _constructionDurationCalculator.ConstructionDurationDays(_ConflictPackage, _coster)) *
          (((float)numAdditionalStreets + (float)baseStreets) / (float)baseStreets);
      } // get
    } // Units

    /// <summary>
    /// Ancillary cost
    /// </summary>
    /// <returns>Ancillary cost</returns>
    public AncillaryCost AncillaryCost
    {
      get
      {
        if (_ConflictPackage.Conflicts != null && Cost > 0)
          return this;
        else
          return null;
      } // get
    } // AncillaryCost
    #endregion
  }
}
