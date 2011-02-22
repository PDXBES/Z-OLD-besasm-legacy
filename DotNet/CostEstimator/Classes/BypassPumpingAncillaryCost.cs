// Project: Classes, File: BypassPumpingAncillaryCost.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: BypassPumpingAncillaryCost
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 30, Size of file: 510 Bytes
// Creation date: 6/11/2008 11:16 PM
// Last modified: 6/12/2008 2:27 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Cost for bypass pumping during construction
  /// </summary>
	class BypassPumpingAncillaryCost : AncillaryCost
	{
    private const int ASSUMED_BYPASS_FLOW_FRACTION_OF_PIPE_CAPACITY = 5;

		#region Variables
		ConflictPackage _ConflictPackage;
		#endregion

		#region Constructors
		/// <summary>
		/// Create bypass pumping ancillary cost
		/// </summary>
		/// <param name="altLink">Alt link</param>
		public BypassPumpingAncillaryCost(ConflictPackage conflictPackage)
		{
			_ConflictPackage = conflictPackage;
		} // BypassPumpingAncillaryCost(altLink)
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
				string bypassFlowType;
				if (BypassFlow <= 3)
					bypassFlowType = "(<= 3 cfs)";
				else if (BypassFlow <= 7)
					bypassFlowType = "(> 3 cfs, <= 7 cfs)";
				else if (BypassFlow <= 15)
					bypassFlowType = "(> 7 cfs, <= 15 cfs)";
				else bypassFlowType = "(> 15 cfs)"; ;

				return "Bypass pumping " + bypassFlowType;
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
				// Assume for sanitary sewers: half the full flow capacity
				// For combined sewers: 1/5 the full flow capacity
				// <3 cfs: $1000 per day
				// >3 <=7 cfs: $2500 per day
				// >7 <=15 cfs: $4000 per day
				// >15 cfs: $7000 per day

				if (BypassFlow <= 3)
					return 500;
				else if (BypassFlow <= 7)
					return 1000;
				else if (BypassFlow <= 15)
					return 2000;
				else return 3000;
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
      	return "days";
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
      	return ConstructionDurationCalculator.ConstructionDurationDays(_ConflictPackage);
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

		/// <summary>
		/// Pipe capacity
		/// </summary>
		/// <returns>Double</returns>
		private double PipeCapacity
		{
			get
			{
				double pipeCapacity = 0.464 / 0.013 * Math.Pow(_ConflictPackage.Diameter / 12, 8 / 3) *
					Math.Sqrt(_ConflictPackage.Slope);
				return pipeCapacity;
			} // get
		} // PipeCapacity

		/// <summary>
		/// Bypass flow
		/// </summary>
		/// <returns>Double</returns>
		private double BypassFlow
		{
			get
			{
				return PipeCapacity / ASSUMED_BYPASS_FLOW_FRACTION_OF_PIPE_CAPACITY;
			} // get
		} // BypassFlow
		#endregion
	}
}
