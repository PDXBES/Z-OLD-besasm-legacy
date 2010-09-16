using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
  /// <summary>
  /// Direct construction selection class
  /// </summary>
	public class DirectConstructionSelection
	{
    /// <summary>
    /// The selected direct construction item
    /// </summary>
		public bool Selected;

    /// <summary>
    /// The name of the direct construction item
    /// </summary>
		public string Name;

    /// <summary>
    /// The factor to be used to figure the direct construction item's cost
    /// when multiplying against a linked item's cost
    /// </summary>
		public double Factor;
	}
}
