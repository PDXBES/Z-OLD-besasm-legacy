// Project: UI, File: SelectedAlternative.cs
// Namespace: CostEstimator.UI, Class: 
// Path: C:\Development\CostEstimatorV2\UI, Author: Arnel
// Code lines: 16, Size of file: 307 Bytes
// Creation date: 3/12/2008 11:24 PM
// Last modified: 6/3/2008 3:14 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
#endregion

/// <summary>
/// Cost estimator. UI
/// </summary>
namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	public struct SelectedAlternative
	{
		#region Variables
		private string _ModelPath;
		public string AlternativeName;
		#endregion

		#region Properties
		/// <summary>
		/// Path to parent model of alternative (non-path delimited)
		/// </summary>
		public string ModelPath
		{
			get
      {
      	return _ModelPath;
      } // get

			set
      {
				_ModelPath = value.EndsWith(Path.DirectorySeparatorChar.ToString()) ?
					value.Substring(0, value.LastIndexOf(Path.DirectorySeparatorChar.ToString())) :
					value;
      } // SelectedAlternative
		} // namespace CostEstimator.UI

		/// <summary>
		/// Alternative path
		/// </summary>
		/// <returns>String</returns>
		public string AlternativePath
		{
			get
			{
				return ModelPath + Path.DirectorySeparatorChar + "alternatives" + Path.DirectorySeparatorChar +
					AlternativeName;
			} // get
		} // AlternativePath
		#endregion

		#region Methods
		public SelectedAlternative(string modelPath, string alternativeName)
		{
			_ModelPath = modelPath;
			AlternativeName = alternativeName;
		} // SelectedAlternative
		public override string ToString()
		{
			return AlternativeName;
		}
		#endregion
	}
}
