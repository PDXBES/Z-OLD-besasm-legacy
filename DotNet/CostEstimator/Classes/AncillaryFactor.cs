// Project: Classes, File: AncillaryFactor.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: 
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 16, Size of file: 223 Bytes
// Creation date: 6/27/2008 5:12 PM
// Last modified: 8/26/2008 11:17 AM

using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	public interface AncillaryFactor
	{
		/// <summary>
		/// Name
		/// </summary>
		/// <returns>String</returns>
		string Name
		{
			get;
		} // Name

		/// <summary>
		/// Factor type
		/// </summary>
		/// <returns>Cost factor type</returns>
		CostFactorType FactorType
		{
			get;
		} // FactorType

		/// <summary>
		/// Factor
		/// </summary>
		/// <returns>Double</returns>
		double Factor
		{
			get;
		} // Factor

		/// <summary>
		/// Ancillary factor
		/// </summary>
		/// <returns>Ancillary factor</returns>
		AncillaryFactor AncillaryFactor
		{
			get;
		} // AncillaryFactor
	}
}