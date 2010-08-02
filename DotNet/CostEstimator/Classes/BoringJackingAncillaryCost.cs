// Project: Classes, File: BoringJackingAncillaryCost.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: BoringJackingAncillaryCost
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 18, Size of file: 292 Bytes
// Creation date: 6/11/2008 5:42 PM
// Last modified: 11/24/2008 4:57 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	class BoringJackingAncillaryCost : AncillaryCost
	{
		#region Variables
		ConflictPackage _ConflictPackage;
		#endregion

    private static double BORINGJACKINGMULTIPLIER = 2.5;

		#region Constructors
		/// <summary>
		/// Create boring jacking ancillary cost
		/// </summary>
		public BoringJackingAncillaryCost(ConflictPackage conflictPackage)
		{
			_ConflictPackage = conflictPackage;
		} // BoringJackingAncillaryCost()
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
				return string.Format("Boring/jacking {0:F0} in diam, {1} pipe, {2:F0} ft deep", _ConflictPackage.Diameter,
					_ConflictPackage.PipeMaterial, _ConflictPackage.Depth);
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
				PipeCoster pipeCoster = new PipeCoster();
				pipeCoster.InsideDiameter = _ConflictPackage.Diameter;
				pipeCoster.Material = _ConflictPackage.PipeMaterial;
				pipeCoster.Depth = _ConflictPackage.Depth;

        return (decimal)((double)pipeCoster.DirectConstructionCost * BORINGJACKINGMULTIPLIER);
			} // get
		} // UnitCost

		/// <summary>
		/// Unit cost
		/// </summary>
		/// <returns>String</returns>
		public string Unit
		{
			get
			{
				return "ft";
			} // get
		} // UnitCost

		/// <summary>
		/// Units
		/// </summary>
		/// <returns>Double</returns>
		public double Units
		{
			get
			{
				if (IsBoringJacking)
				{
					bool highPipeDepth = _ConflictPackage.Depth > 30;
					bool crossesFreeway = _ConflictPackage.Conflicts.NumFreewayCrossings > 0;
					if (highPipeDepth || crossesFreeway)
					{
						return _ConflictPackage.Length;
					} // if
					else
					{
						// AltPipXP routine can have overlapping crossings, and so
						// multiplying the number of crossings by 50 ft. won't really work,
						// nor would a plain old' 50 ft. if there is a crossing at all.
						// We'll return 50 for now, but we'll have to redesign the conflict 
						// database and detection routine to calculate possible boring length
						return 50;
					} // else
				} // if
				else
					return 0;
			} // get
		} // Units

		private bool IsBoringJacking
		{
			get
			{
				bool highPipeDepth = _ConflictPackage.Depth > 30;
				bool crossesFreeway = _ConflictPackage.Conflicts.NumFreewayCrossings > 0;
				bool crossesRailroad = _ConflictPackage.Conflicts.NumRailroadCrossings > 0;
				bool crossesLightRail = _ConflictPackage.Conflicts.NumLightRailCrossings > 0;
				bool crossesBuilding = _ConflictPackage.Conflicts.IsNearBuilding;
				return highPipeDepth || crossesFreeway || crossesRailroad || crossesLightRail ||
					crossesBuilding;
			} // get
		} // IsBoringJacking

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
