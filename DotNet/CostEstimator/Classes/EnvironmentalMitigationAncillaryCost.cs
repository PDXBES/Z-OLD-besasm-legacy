// Project: Classes, File: EnvironmentalMitigationAncillaryCost.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: EnvironmentalMitigationAncillaryCost
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 18, Size of file: 312 Bytes
// Creation date: 6/11/2008 11:20 PM
// Last modified: 6/17/2008 11:45 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Cost for building pipe across an environmental zone
  /// </summary>
  class EnvironmentalMitigationAncillaryCost : AncillaryCost
  {
    #region Constants
    private const float UNIT_COST_ENVIRONMENTAL_MITIGATION_PER_ACRE = 150000;
    private const double ENVIRONMENTAL_ZONE_AFFECTED_WIDTH = 50;
    #endregion

    #region Variables
    public ConflictPackage _ConflictPackage;
    #endregion

    #region Constructors
    /// <summary>
    /// Create environmental mitigation ancillary cost
    /// </summary>
    /// <param name="altPipXP">Alt pip XP</param>
    public EnvironmentalMitigationAncillaryCost(ConflictPackage conflictPackage)
    {
      _ConflictPackage = conflictPackage;
    } // EnvironmentalMitigationAncillaryCost(altPipXP)
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
        return "Environmental mitigation";
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
        return (float)((double)UnitCost * Units);
      }
    } // Cost

    /// <summary>
    /// Unit cost
    /// </summary>
    /// <returns>float</returns>
    public float UnitCost
    {
      get
      {
        return UNIT_COST_ENVIRONMENTAL_MITIGATION_PER_ACRE;
      }
    } // UnitCost

    /// <summary>
    /// Unit
    /// </summary>
    /// <returns>String</returns>
    public string Unit
    {
      get
      {
        return "ac";
      }
    } // Unit

    /// <summary>
    /// Units
    /// </summary>
    /// <returns>Double</returns>
    public float Units
    {
      get
      {
        return (float)(
        _ConflictPackage.Conflicts.LengthInConservationZoneFeet * ENVIRONMENTAL_ZONE_AFFECTED_WIDTH +
        _ConflictPackage.Conflicts.LengthInPreservationZoneFeet * ENVIRONMENTAL_ZONE_AFFECTED_WIDTH) /
        Common.SQUARE_FEET_PER_ACRE;
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
