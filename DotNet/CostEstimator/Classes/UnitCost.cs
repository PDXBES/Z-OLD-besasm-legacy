// Project: UI, File: UnitCost.cs
// Namespace: CostEstimator.UI, Class: 
// Path: C:\Development\CostEstimatorV2\UI, Author: Arnel
// Code lines: 19, Size of file: 327 Bytes
// Creation date: 3/13/2008 1:17 PM
// Last modified: 7/1/2008 3:41 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Consolidates a cost and its associated number of units and unit name
  /// </summary>
  public struct UnitCost
  {
    public decimal CostPerUnit;
    public string UnitName;
    public double Units;

    /// <summary>
    /// Create unit cost
    /// </summary>
    /// <param name="units">Units</param>
    /// <param name="unitName">Unit name</param>
    public UnitCost(decimal costPerUnit, string unitName)
      : this(costPerUnit, unitName, 1)
    {
    } // UnitCost(units, unitName)

    /// <summary>
    /// Create unit cost
    /// </summary>
    /// <param name="costPerUnit">Cost per unit</param>
    /// <param name="unitName">Unit name</param>
    /// <param name="units">Units</param>
    public UnitCost(decimal costPerUnit, string unitName, double units)
    {
      CostPerUnit = costPerUnit;
      UnitName = unitName;
      Units = units;
    } // UnitCost(costPerUnit, unitName, units)
  }
}
