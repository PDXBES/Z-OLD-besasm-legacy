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
	class DifficultAreaAncillaryFactor : AncillaryFactor
	{
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
						Math.Abs(_ConflictPackage.Conflicts.SurfaceSlopePct) >= 10)
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

		public double Factor
		{
			get
			{
				if ((_ConflictPackage != null && _ConflictPackage.Conflicts != null) && 
					((!_ConflictPackage.Conflicts.IsInStreet &&
					Math.Abs(_ConflictPackage.Conflicts.SurfaceSlopePct) >= 10) ||
					_ConflictPackage.Conflicts.IsHardArea))
					return 0.50;
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
