// Project: Classes, File: ReportFocusAreaInfo.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: ReportFocusAreaInfo
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 25, Size of file: 326 Bytes
// Creation date: 6/12/2008 4:38 PM
// Last modified: 6/12/2008 5:12 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	public class ReportFocusAreaInfo
	{
		string _Name;

		/// <summary>
		/// Create report focus area info
		/// </summary>
		/// <param name="focusArea">Focus area</param>
		public ReportFocusAreaInfo(string focusArea)
		{
			_Name = focusArea;
		} // ReportFocusAreaInfo(focusArea)

		/// <summary>
		/// Name
		/// </summary>
		/// <returns>String</returns>
		public string Name
		{
			get
			{
				return _Name;
			} // get

			set
			{
				_Name = value;
			} // set
		} // Name
	}
}
