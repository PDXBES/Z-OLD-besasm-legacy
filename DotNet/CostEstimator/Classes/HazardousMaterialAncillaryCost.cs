// Project: Classes, File: HazardousMaterialAncillaryCost.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: HazardousMaterialAncillaryCost
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 18, Size of file: 301 Bytes
// Creation date: 6/11/2008 11:20 PM
// Last modified: 6/17/2008 11:52 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Cost for building pipe through a known hazardous material/polluted
  /// underground soil area
  /// </summary>
	class HazardousMaterialAncillaryCost : AncillaryCost
	{
		#region Constants
		private const decimal  UNIT_COST_HAZARDOUS_MATERIAL_HANDLING_PER_CUBIC_YARD = 50;
		private const int HAZARDOUS_MATERIAL_TRENCH_IMPACT_LENGTH_FEET = 50;
		#endregion

		#region Variables
		public ConflictPackage _ConflictPackage;
		#endregion

		#region Constructors
		/// <summary>
		/// Create hazardous material ancillary cost
		/// </summary>
		/// <param name="altPipXP">Alt pip XP</param>
		public HazardousMaterialAncillaryCost(ConflictPackage conflictPackage)
		{
			_ConflictPackage = conflictPackage;
		} // HazardousMaterialAncillaryCost(altPipXP)
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
				return "Hazardous materials";
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
				return UNIT_COST_HAZARDOUS_MATERIAL_HANDLING_PER_CUBIC_YARD;
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
				return "cuyd";
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
				double affectedLength = _ConflictPackage.Conflicts.IsNearContaminationSite ?
					(_ConflictPackage.Length < HAZARDOUS_MATERIAL_TRENCH_IMPACT_LENGTH_FEET ? 
					_ConflictPackage.Length : HAZARDOUS_MATERIAL_TRENCH_IMPACT_LENGTH_FEET) : 0;
				PipeCoster pipeCoster = new PipeCoster();
				pipeCoster.InsideDiameter = _ConflictPackage.Diameter;
				pipeCoster.Material = _ConflictPackage.PipeMaterial;
				pipeCoster.Depth = _ConflictPackage.Depth;

				return (affectedLength / _ConflictPackage.Length) *
					(pipeCoster.ExcavationVolume * _ConflictPackage.Length);
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
