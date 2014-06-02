// Project: Classes, File: BoringJackingAncillaryCost.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: BoringJackingAncillaryCost
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 18, Size of file: 292 Bytes
// Creation date: 6/11/2008 5:42 PM
// Last modified: 10/12/2010 2:59 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Boring/Jacking cost
  /// </summary>
  class BoringJackingAncillaryCost : AncillaryCost
  {
    private const int MINIMUM_PIPE_DEPTH_REQUIRED_FOR_BORING_JACKING_FT = 30;
    #region Variables
    ConflictPackage _ConflictPackage;
    #endregion

    private static double BORINGJACKINGMULTIPLIER = 2.5;
    private static double MICROTUNNELMULTIPLIER = 3.0;

    #region Constructors
    /// <summary>
    /// Create boring jacking ancillary cost
    /// </summary>
    public BoringJackingAncillaryCost(ConflictPackage conflictPackage)
    {
      _ConflictPackage = conflictPackage;
    } // BoringJackingAncillaryCost()
    #endregion

    #region Properties
    /// <summary>
    /// Name of boring/jacking item, based on diameter, pipe material, depth
    /// </summary>
    /// <returns>String</returns>
    public string Name
    {
      get
      {
        string tunnelType = "Not boring/jacking";
        if (IsBoringJacking)
        {
          if (IsMicroTunnel)
            tunnelType = "Microtunnel";
          else
            tunnelType = "Boring/jacking";
        } // if

        return string.Format("{3} {0:F0} in diam, {1} pipe, {2:F0} ft deep", _ConflictPackage.Diameter,
        _ConflictPackage.PipeMaterial, _ConflictPackage.Depth, tunnelType);
      } // get
    } // Name

    /// <summary>
    /// Cost in dollars of the boring/jacking
    /// </summary>
    /// <returns>float</returns>
    public float Cost
    {
      get
      {
        return (float)(Units * (double)UnitCost);
      } // get
    } // Cost

    /// <summary>
    /// Unit cost per foot of the boring/jacking
    /// </summary>
    /// <returns>float</returns>
    public float UnitCost
    {
      get
      {
        PipeCoster pipeCoster = new PipeCoster();
        pipeCoster.InsideDiameter = _ConflictPackage.Diameter;
        pipeCoster.Material = _ConflictPackage.PipeMaterial;
        pipeCoster.Depth = _ConflictPackage.Depth;

        double multiplier = IsMicroTunnel ? MICROTUNNELMULTIPLIER : BORINGJACKINGMULTIPLIER;
        return (float)((double)pipeCoster.DirectConstructionCost * multiplier);
      } // get
    } // UnitCost

    /// <summary>
    /// Unit name
    /// </summary>
    /// <returns>String</returns>
    public string Unit
    {
      get
      {
        return "ft";
      } // get
    } // UnitCost

    /// <summary>
    /// Is micro tunnel
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsMicroTunnel
    {
      get
      {
        return _ConflictPackage.Length <= 50;
      } // get
    } // isMicroTunnel

    /// <summary>
    /// Length of boring/jacking
    /// </summary>
    /// <returns>Double</returns>
    public float Units
    {
      get
      {
        if (IsBoringJacking)
        {
          bool highPipeDepth = _ConflictPackage.Depth > MINIMUM_PIPE_DEPTH_REQUIRED_FOR_BORING_JACKING_FT;
          bool crossesFreeway = _ConflictPackage.Conflicts.NumFreewayCrossings > 0;

          if (highPipeDepth || crossesFreeway)
          {
            return _ConflictPackage.Length;
          } // if
          else
          {
            // AltPipXP routine can have overlapping crossings, and so
            // multiplying the number of crossings by 50 ft. won't really work,
            // nor would a plain old' 50 ft. if there is a crossing at all.
            // We'll return 50 for now, but we'll have to redesign the conflict 
            // database and detection routine to calculate possible boring length
            return 50;
          } // else
        } // if
        else
          return 0;
      } // get
    } // Units

    /// <summary>
    /// Determines whether pipe needs boring/jacking
    /// </summary>
    /// <returns>True if pipe needs boring/jackng</returns>
    private bool IsBoringJacking
    {
      get
      {
        bool highPipeDepth = _ConflictPackage.Depth > MINIMUM_PIPE_DEPTH_REQUIRED_FOR_BORING_JACKING_FT;
        bool crossesFreeway = _ConflictPackage.Conflicts.NumFreewayCrossings > 0;
        bool crossesRailroad = _ConflictPackage.Conflicts.NumRailroadCrossings > 0;
        bool crossesLightRail = _ConflictPackage.Conflicts.NumLightRailCrossings > 0;
        bool crossesBuilding = _ConflictPackage.Conflicts.IsNearBuilding;
        return highPipeDepth || crossesFreeway || crossesRailroad || crossesLightRail ||
        crossesBuilding;
      } // get
    } // IsBoringJacking

    /// <summary>
    /// Returns reference to self if there is a cost
    /// </summary>
    /// <returns>Ancillary cost (if cost > 0 and conflicts are available) or
    /// null otherwise  </returns>
    public AncillaryCost AncillaryCost
    {
      get
      {
        if (_ConflictPackage.Conflicts != null && Cost > 0)
          return this;
        else
          return null;
      } // get
    } // AncillaryCost
    #endregion
  }
}
