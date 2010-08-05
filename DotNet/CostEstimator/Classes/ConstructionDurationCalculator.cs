// Project: Classes, File: ConstructionDurationCalculator.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: ConstructionDurationCalculator
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 21, Size of file: 384 Bytes
// Creation date: 6/12/2008 10:27 AM
// Last modified: 8/20/2008 2:28 PM

using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	public class ConstructionDurationCalculator
	{
		public const int MAINLINE_BUILD_RATE_PER_DAY_CUYD = 140;
    public const double MANHOLE_BUILD_RATE_PER_DAY_FT = 10;
    public const double CROSSING_RATE_PER_DAY_EA = 0.5;
    public const int PAVEMENT_REPAIR_RATE_PER_DAY_FT = 250;

		#region Properties
		/// <summary>
		/// Duration of construction based on build rates of components (mainline,
    /// manhole, pavement, and crossings)
		/// </summary>
		/// <returns>Number of fractional days needed to build pipe</returns>
		static public double ConstructionDurationDays(ConflictPackage conflictPackage)
		{
			PipeCoster pipeCoster = new PipeCoster();

			pipeCoster.InsideDiameter = conflictPackage.Diameter;
			pipeCoster.Depth = conflictPackage.Depth;
			pipeCoster.Material = conflictPackage.PipeMaterial;

			// 1 day per 10 ft vertical of manhole
			double manholeConstructionDurationDays = conflictPackage.Depth /
        MANHOLE_BUILD_RATE_PER_DAY_FT;
			// Mainline at 140 cy per day
			double mainlineConstructionDurationDays = pipeCoster.ExcavationVolume * conflictPackage.Length / MAINLINE_BUILD_RATE_PER_DAY_CUYD;
			// Utility crossings add 0.5 days per conflict
			int numUtilityCrossings =
				conflictPackage.Conflicts.NumFiberOpticCrossings +
				conflictPackage.Conflicts.NumGasCrossings +
				conflictPackage.Conflicts.NumSewerCrossings +
				conflictPackage.Conflicts.NumWaterCrossings;
			double utilityCrossingDurationDays = numUtilityCrossings * CROSSING_RATE_PER_DAY_EA;
			// Pavement repair at 250 feet per day
			double pavementRepairDurationDays = conflictPackage.Length / PAVEMENT_REPAIR_RATE_PER_DAY_FT;

			double numDays = Math.Ceiling(
				manholeConstructionDurationDays +
				mainlineConstructionDurationDays +
				utilityCrossingDurationDays +
				pavementRepairDurationDays);
			if (numDays < 0)
				throw new Exception(string.Format("Check pipe for negative length or depth:\n\n{0} {1}-{2}", conflictPackage.MstLinkID, conflictPackage.USNode, conflictPackage.DSNode));
			return numDays;
		} // ConstructionDuration
		#endregion
	}
}
