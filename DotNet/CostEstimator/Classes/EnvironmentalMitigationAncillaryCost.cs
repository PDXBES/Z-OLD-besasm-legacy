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
	class EnvironmentalMitigationAncillaryCost : AncillaryCost
	{
		#region Constants
		private const decimal UNIT_COST_ENVIRONMENTAL_MITIGATION_PER_ACRE = 100000;
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
		public double Units
		{
			get
			{
				return (double)(_ConflictPackage.Conflicts.AreaConservationZoneSqFt + 
					_ConflictPackage.Conflicts.AreaPreservationZoneSqFt) / 43560;
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