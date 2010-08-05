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
		/// Split a pascal-cased string to a space-separated string
		/// </summary>
		/// <param name="casedString">Pascal-cased string</param>
		/// <returns>String with words separated by spaces</returns>
		public static string SplitCasedString(string casedString)
		{
      // The search term ([A-Z]+) finds any occurrence of a capital letter or a
      // group of capital letters.  The replacement term [space]$1 then replaces
      // these occurrences with a space and the found occurrence, effectively
      // inserting spaces before each group of capital letters.  Trim() then
      // chops off the initial space if the first character is a capital.
			return Regex.Replace(casedString, "([A-Z]+)", " $1", RegexOptions.Compiled).Trim();
		} // SplitCasedString(casedString)
	}
}
