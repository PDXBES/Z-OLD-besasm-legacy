// Project: Classes, File: AncillaryCost.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: 
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 18, Size of file: 315 Bytes
// Creation date: 6/11/2008 5:39 PM
// Last modified: 6/12/2008 11:27 AM

using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Common interface for all ancillary costs
  /// </summary>
  public interface AncillaryCost
  {
    /// <summary>
    /// Name
    /// </summary>
    /// <returns>String</returns>
    string Name { get; } // Name

    /// <summary>
    /// Cost
    /// </summary>
    /// <returns>Decimal</returns>
    decimal Cost { get; } // Cost

    /// <summary>
    /// Unit cost
    /// </summary>
    /// <returns>Decimal</returns>
    decimal UnitCost { get; } // UnitCost

    /// <summary>
    /// Unit
    /// </summary>
    /// <returns>String</returns>
    string Unit { get; } // Unit

    /// <summary>
    /// Units
    /// </summary>
    /// <returns>Double</returns>
    double Units { get; } // Units

    /// <summary>
    /// Ancillary cost; generally returns a reference to self if there is a
    /// cost, null otherwise
    /// </summary>
    /// <returns>Ancillary cost</returns>
    AncillaryCost AncillaryCost { get; } // AncillaryCost
  }
}
