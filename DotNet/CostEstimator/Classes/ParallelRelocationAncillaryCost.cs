// Project: Classes, File: ParallelRelocationAncillaryCost.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: ParallelRelocationAncillaryCost
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 20, Size of file: 330 Bytes
// Creation date: 6/11/2008 5:57 PM
// Last modified: 6/12/2008 11:23 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Cost for relocating parallel and close proximity utility pipes (close
  /// proximity being horizontally a maximum of third of the average pipe
  /// depth away)
  /// </summary>
	class ParallelRelocationAncillaryCost : AncillaryCost
	{
		#region Variables
		public ConflictPackage _ConflictPackage;
		#endregion

		#region Constructors
		/// <summary>
		/// Create parallel relocation ancillary cost
		/// </summary>
		/// <param name="altPackage">Alt package</param>
		/// <param name="altLink">Alt link</param>
		/// <param name="altPipXP">Alt pip XP</param>
		public ParallelRelocationAncillaryCost(ConflictPackage conflictPackage)
		{
			_ConflictPackage = conflictPackage;
		} // ParallelRelocationAncillaryCost(altPackage, altLink, altPipXP)
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
				bool isInConflict = (_ConflictPackage.Conflicts.DistToWaterParallelFeet <= _ConflictPackage.Depth / 3);
				string waterPipeType = "";
				if (isInConflict)
				{
					if (_ConflictPackage.Conflicts.LargestWaterParallelDiameterInches <= 12)
						waterPipeType = "(<= 12 in)";
					else if (_ConflictPackage.Conflicts.LargestWaterParallelDiameterInches <= 21)
						waterPipeType = "(> 12 in, =< 21 in)";
					else if (_ConflictPackage.Conflicts.LargestWaterParallelDiameterInches <= 33)
						waterPipeType = "(> 21 in, =< 33 in)";
					else
						waterPipeType = "(> 33 in)";
				} // if

				return "Parallel water relocation " + waterPipeType;
			} // get
		} // Name

		/// <summary>
		/// Cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal Cost
		{
			get
			{
				return (decimal)(Units * (double)UnitCost);
			} // get
		} // Cost

		/// <summary>
		/// Unit cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal UnitCost
		{
			get
      {
				bool isInConflict = (_ConflictPackage.Conflicts.DistToWaterParallelFeet <= _ConflictPackage.Depth / 3);
				if (isInConflict)
				{
					if (_ConflictPackage.Conflicts.LargestWaterParallelDiameterInches <= 12)
						return 1000 / 2;
					else if (_ConflictPackage.Conflicts.LargestWaterParallelDiameterInches <= 21)
						return 2500 / 2;
					else if (_ConflictPackage.Conflicts.LargestWaterParallelDiameterInches <= 33)
						return 5000 / 2;
					else
						return 7500 / 2;
				} // if
				else
					return 0;
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
				return "ea";
			} // get
		} // Unit

		/// <summary>
		/// Units
		/// </summary>
		/// <returns>Double</returns>
		public double Units
		{
			get
			{
				return _ConflictPackage.Conflicts.HasWaterParallel ? 1 : 0;
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
