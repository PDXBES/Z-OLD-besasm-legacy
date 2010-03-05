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
		#region Properties
		/// <summary>
		/// Construction duration
		/// </summary>
		/// <returns>Double</returns>
		static public double ConstructionDurationDays(ConflictPackage conflictPackage)
		{
			PipeCoster pipeCoster = new PipeCoster();

			pipeCoster.InsideDiameter = conflictPackage.Diameter;
			pipeCoster.Depth = conflictPackage.Depth;
			pipeCoster.Material = conflictPackage.PipeMaterial;

			// 1 day per 10 ft vertical of manhole
			double manholeConstructionDurationDays = conflictPackage.Depth / 10;
			// Mainline at 140 cy per day
			double mainlineConstructionDurationDays = pipeCoster.ExcavationVolume * conflictPackage.Length / MAINLINE_BUILD_RATE_PER_DAY_CUYD;
			// Utility crossings add 0.5 days per conflict
			int numUtilityCrossings =
				conflictPackage.Conflicts.NumFiberOpticCrossings +
				conflictPackage.Conflicts.NumGasCrossings +
				conflictPackage.Conflicts.NumSewerCrossings +
				conflictPackage.Conflicts.NumWaterCrossings;
			double utilityCrossingDurationDays = numUtilityCrossings * 0.5;
			// Pavement repair at 250 feet per day
			double pavementRepairDurationDays = conflictPackage.Length / 250;

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
