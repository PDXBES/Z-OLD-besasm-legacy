// Project: Classes, File: CosterParameter.cs
// Namespace: CostEstimator.Classes, Class: 
// Path: C:\Development\CostEstimatorV2\Classes, Author: Arnel
// Code lines: 21, Size of file: 390 Bytes
// Creation date: 3/17/2008 1:06 AM
// Last modified: 3/17/2008 1:10 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	public struct CosterParameter
	{
		public string Name;
		public string Type;
		public object Value;

		/// <summary>
		/// Create coster parameter
		/// </summary>
		/// <param name="name">Name</param>
		/// <param name="type">Type</param>
		/// <param name="value">Value</param>
		public CosterParameter(string name, string type, object value)
		{
			Name = name;
			Type = type;
			Value = value;
		} // CosterParameter(name, type, value)
	}
}
