// Project: Classes, File: ParallelRelocationAncillaryCost.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: ParallelRelocationAncillaryCost
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 20, Size of file: 330 Bytes
// Creation date: 6/11/2008 5:57 PM
// Last modified: 6/12/2008 11:23 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Cost for relocating parallel and close proximity utility pipes (close
  /// proximity being horizontally a maximum of third of the average pipe
  /// depth away)
  /// </summary>
  class ParallelRelocationAncillaryCost : AncillaryCost
  {
    #region Variables
    public ConflictPackage _ConflictPackage;
    #endregion

    #region Constructors
    /// <summary>
    /// Create parallel relocation ancillary cost
    /// </summary>
    /// <param name="altPackage">Alt package</param>
    /// <param name="altLink">Alt link</param>
    /// <param name="altPipXP">Alt pip XP</param>
    public ParallelRelocationAncillaryCost(ConflictPackage conflictPackage)
    {
      _ConflictPackage = conflictPackage;
    } // ParallelRelocationAncillaryCost(altPackage, altLink, altPipXP)
    #endregion

    #region Properties
    /// <summary>
    /// Name
    /// </summary>
    /// <returns>String</returns>
    public string Name
    {
      get
      {
        bool isInConflict;
        try
        {
          if (_ConflictPackage.Conflict != null)
            isInConflict = _ConflictPackage.Conflict.NumWaterParallels > 0;
          else
            isInConflict = (_ConflictPackage.Conflicts.HasWaterParallel);
        }
        catch (ArgumentException)
        {
          return string.Empty;
        }

        string waterPipeType;
        if (isInConflict)
          if (_ConflictPackage.Conflict != null)
            waterPipeType = string.Format("({0:F0} in)", _ConflictPackage.Conflict.WaterParallelsMaxDiamWidthIn);
          else
            waterPipeType = string.Format("({0:F0} in)", _ConflictPackage.Conflicts.LargestWaterParallelDiameterInches);
        else
          waterPipeType = string.Empty; // if

        return "Parallel water relocation " + waterPipeType;
      } // get
    } // Name

    /// <summary>
    /// Cost
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
    /// Unit cost
    /// </summary>
    /// <returns>float</returns>
    public float UnitCost
    {
      get
      {
        bool isInConflict;
        try
        {
          if (_ConflictPackage.Conflict != null)
            isInConflict = _ConflictPackage.Conflict.NumWaterParallels > 0;
          else
            isInConflict = _ConflictPackage.Conflicts.HasWaterParallel;
        }
        catch (ArgumentException e)
        {
          return 0;
        }

        if (isInConflict)
        {
          float diam;
          if (_ConflictPackage.Conflict != null)
            diam = _ConflictPackage.Conflict.WaterParallelsMaxDiamWidthIn;
          else
            diam = _ConflictPackage.Conflicts.LargestWaterParallelDiameterInches;
          return 7.9126f * diam + 74.093f;
        } // if
        else
          return 0.0f;
      }
    } // UnitCost

    /// <summary>
    /// Unit
    /// </summary>
    /// <returns>String</returns>
    public string Unit
    {
      get
      {
        return "ea";
      } // get
    } // Unit

    /// <summary>
    /// Units
    /// </summary>
    /// <returns>Double</returns>
    public float Units
    {
      get
      {
        try
        {
          if (_ConflictPackage.Conflict != null)
            return _ConflictPackage.Conflict.NumWaterParallels > 0 ? 1 : 0;
          else
            return _ConflictPackage.Conflicts.HasWaterParallel ? 1 : 0;
        }
        catch (ArgumentException)
        {
          return 0;
        }
      } // get
    } // Units

    /// <summary>
    /// Ancillary cost
    /// </summary>
    /// <returns>Ancillary cost</returns>
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
