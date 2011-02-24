// Project: Classes, File: CrossingRelocationAncillaryCost.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: CrossingRelocationAncillaryCost
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 32, Size of file: 548 Bytes
// Creation date: 6/11/2008 11:12 PM
// Last modified: 6/12/2008 10:58 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Cost for utility crossings
  /// </summary>
	class CrossingRelocationAncillaryCost : AncillaryCost
	{
    private const int COST_PER_CROSSING = 5000;

		#region Variables
		private ConflictPackage _ConflictPackage;
		#endregion

		#region Constructors
		/// <summary>
		/// Create crossing relocation ancillary cost
		/// </summary>
		/// <param name="altPipXP">Alt pip XP</param>
		public CrossingRelocationAncillaryCost(ConflictPackage conflictPackage)
		{
			_ConflictPackage = conflictPackage;
		} // CrossingRelocationAncillaryCost(altPipXP)
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
				return "Crossing relocation";
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
      	return (decimal)((double)UnitCost * Units);
      }
		} // Cost

		/// <summary>
		/// Unit cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal UnitCost
		{
			get
      {
				return COST_PER_CROSSING;
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
				return "x-ings";
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
				return _ConflictPackage.Conflicts.NumWaterCrossings +
					_ConflictPackage.Conflicts.NumFiberOpticCrossings +
					_ConflictPackage.Conflicts.NumGasCrossings +
					_ConflictPackage.Conflicts.NumSewerCrossings;
			} // set
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
      }
		} // AncillaryCost
		#endregion

	}
}
