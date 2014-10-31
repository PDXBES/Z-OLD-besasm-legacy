// Project: Classes, File: DifficultAreaAncillaryFactor.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: DifficultAreaAncillaryFactor
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 18, Size of file: 294 Bytes
// Creation date: 8/26/2008 11:08 AM
// Last modified: 8/26/2008 12:07 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Cost for building a pipe in a difficult area (unpaved and steep, or
  /// historically difficult area [e.g., downtown area])
  /// </summary>
  class DifficultAreaAncillaryFactor : AncillaryFactor
  {
    private ConflictPackage _ConflictPackage;

    /// <summary>
    /// Create difficult area ancillary factor
    /// </summary>
    /// <param name="conflictPackage">Conflict package</param>
    public DifficultAreaAncillaryFactor(ConflictPackage conflictPackage)
    {
      _ConflictPackage = conflictPackage;
    } // DifficultAreaAncillaryFactor(conflictPackage)

    public string Name
    {
      get
      {
        return "Difficult work area";
      } // get
    } // Name

    public CostFactorType FactorType
    {
      get
      {
        return CostFactorType.Additive;
      } // get
    } // FactorType

    public float Factor
    {
      get
      {
        if (_ConflictPackage != null && _ConflictPackage.Conflict != null)
        {
          bool HasConflicts = _ConflictPackage != null && _ConflictPackage.Conflict != null;
          bool ParallelToRailroad = _ConflictPackage.Conflict.NumRailParallels > 0;
          bool ParallelToLTR = _ConflictPackage.Conflict.NumLRTParallels > 0;
          if (HasConflicts &&
            (_ConflictPackage.Conflict.InHardArea ||
            ParallelToRailroad ||
            ParallelToLTR))
            return 0.50f;
          else
            return 0;
        }
        else if (_ConflictPackage != null)
        {
          bool HasConflicts = _ConflictPackage != null && _ConflictPackage.Conflicts != null;
          bool ParallelToRailroad = _ConflictPackage.Conflicts.HasRailroadParallel;
          bool ParallelToLTR = _ConflictPackage.Conflicts.HasLightRailParallel;
          if (HasConflicts &&
            (_ConflictPackage.Conflicts.IsHardArea ||
            ParallelToRailroad ||
            ParallelToLTR))
            return 0.50f;
          else
            return 0;
        }
        else
          return 0;
      } // get
    } // Factor

    /// <summary>
    /// Ancillary factor
    /// </summary>
    /// <returns>Ancillary factor</returns>
    public AncillaryFactor AncillaryFactor
    {
      get
      {
        if (Factor > 0)
          return this;
        else
          return null;
      } // get
    } // AncillaryFactor
  }
}
