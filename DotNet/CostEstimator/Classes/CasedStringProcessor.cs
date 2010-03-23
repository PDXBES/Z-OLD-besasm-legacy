// Project: Classes, File: CasedStringProcessor.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: CasedStringProcessor
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 14, Size of file: 284 Bytes
// Creation date: 6/13/2008 1:45 PM
// Last modified: 6/13/2008 1:46 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	public class CasedStringProcessor
	{
		/// <summary>
		/// Split cased string
		/// </summary>
		/// <param name="casedString">Cased string</param>
		/// <returns>String</returns>
		public static string SplitCasedString(string casedString)
		{
			return Regex.Replace(casedString, "([A-Z]+)", " $1", RegexOptions.Compiled).Trim();
		} // SplitCasedString(casedString)
	}
}
