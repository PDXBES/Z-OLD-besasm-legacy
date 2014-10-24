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
    private const int MIN_PIPE_SLOPE_FOR_DIFFICULT_AREA = 10;
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
        StringBuilder reason = new StringBuilder();
        if (_ConflictPackage != null)
        {
          if (!_ConflictPackage.Conflicts.IsInStreet &&
          Math.Abs(_ConflictPackage.Conflicts.SurfaceSlopePct) >= MIN_PIPE_SLOPE_FOR_DIFFICULT_AREA)
            reason.Append("unpaved/steep surface,");
          if (_ConflictPackage.Conflicts.IsHardArea)
            reason.Append("difficult area,");
        } // if

        if (reason.Length > 0)
          return ("Difficult work area (" +
          reason.ToString().Substring(0, reason.Length - 1) + ")");
        else
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
        bool HasConflicts = _ConflictPackage != null && _ConflictPackage.Conflicts != null;
        bool NotInStreetAndSurfaceSlopeIsSteep = !_ConflictPackage.Conflicts.IsInStreet &&
        Math.Abs(_ConflictPackage.Conflicts.SurfaceSlopePct) >= MIN_PIPE_SLOPE_FOR_DIFFICULT_AREA;
        bool ParallelToRailroad = _ConflictPackage.Conflicts.HasRailroadParallel;
        bool ParallelToLTR = _ConflictPackage.Conflicts.HasLightRailParallel;
        if (HasConflicts &&
        (NotInStreetAndSurfaceSlopeIsSteep || _ConflictPackage.Conflicts.IsHardArea ||
        ParallelToRailroad || ParallelToLTR))
          return 0.50f;
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
