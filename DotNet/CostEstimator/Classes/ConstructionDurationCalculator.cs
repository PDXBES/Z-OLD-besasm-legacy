// Project: Classes, File: ConstructionDurationCalculator.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: ConstructionDurationCalculator
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 21, Size of file: 384 Bytes
// Creation date: 6/12/2008 10:27 AM
// Last modified: 10/12/2010 3:04 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  public class ConstructionDurationCalculator
  {
    public const int MAINLINE_BUILD_RATE_PER_DAY_CUYD = 140;
    public const double MANHOLE_BUILD_RATE_PER_DAY_FT = 10;
    public const double CROSSING_RATE_PER_DAY_EA = 0.5;
    public const int PAVEMENT_REPAIR_RATE_PER_DAY_FT = 250;
    public const double BOREJACK_DEPTH_FT = 30;
    public const double MICROTUNNEL_BUILD_RATE_PER_DAY_FT = 75;
    public const double BOREJACK_SLOWERDIAMETER_IN = 30;
    public const double BOREJACK_FAST_BUILD_RATE_PER_DAY_FT = 125;
    public const double BOREJACK_SLOW_BUILD_RATE_PER_DAY_FT = 75;
    private PipeCoster _coster = null;
    #region Properties
    /// <summary>
    /// Duration of construction based on build rates of components (mainline,
    /// manhole, pavement, and crossings)
    /// </summary>
    /// <returns>Number of fractional days needed to build pipe</returns>
    public float ConstructionDurationDays(ConflictPackage conflictPackage, PipeCoster coster)
    {
      _coster = coster;

      _coster.InsideDiameter = conflictPackage.Diameter;
      _coster.Depth = conflictPackage.Depth;
      _coster.Material = conflictPackage.PipeMaterial;

      float numDays = 0;

      if (conflictPackage.Depth > BOREJACK_DEPTH_FT)
      {
        BoringJackingAncillaryCost testBoring = new BoringJackingAncillaryCost(conflictPackage, _coster);
        numDays = testBoring.IsMicroTunnel ?
          (float)Math.Ceiling(conflictPackage.Length / MICROTUNNEL_BUILD_RATE_PER_DAY_FT) :
          conflictPackage.Diameter <= BOREJACK_SLOWERDIAMETER_IN ?
          (float)Math.Ceiling(conflictPackage.Length / BOREJACK_FAST_BUILD_RATE_PER_DAY_FT) :
          (float)Math.Ceiling(conflictPackage.Length / BOREJACK_SLOW_BUILD_RATE_PER_DAY_FT);
      } // if
      else
      {
        // 1 day per 10 ft vertical of manhole
        double manholeConstructionDurationDays = conflictPackage.Depth /
        MANHOLE_BUILD_RATE_PER_DAY_FT;
        // Mainline at 140 cy per day
        double mainlineConstructionDurationDays = _coster.ExcavationVolume * conflictPackage.Length / MAINLINE_BUILD_RATE_PER_DAY_CUYD;
        // Utility crossings add 0.5 days per conflict
        int numUtilityCrossings =
        (conflictPackage.Conflicts == null) ? 0 :
        conflictPackage.Conflicts.NumFiberOpticCrossings +
        conflictPackage.Conflicts.NumGasCrossings +
        conflictPackage.Conflicts.NumSewerCrossings +
        conflictPackage.Conflicts.NumWaterCrossings;
        double utilityCrossingDurationDays = numUtilityCrossings * CROSSING_RATE_PER_DAY_EA;
        // Pavement repair at 250 feet per day
        double pavementRepairDurationDays = conflictPackage.Length / PAVEMENT_REPAIR_RATE_PER_DAY_FT;

        numDays = (float)Math.Ceiling(
          manholeConstructionDurationDays +
          mainlineConstructionDurationDays +
          utilityCrossingDurationDays +
          pavementRepairDurationDays);
      } // else

      if (numDays < 0)
        throw new Exception(string.Format("Check pipe for negative length or depth:\n\n{0} {1}-{2}", conflictPackage.MstLinkID, conflictPackage.USNode, conflictPackage.DSNode));
      return numDays;
    } // ConstructionDuration
    #endregion
  }
}
