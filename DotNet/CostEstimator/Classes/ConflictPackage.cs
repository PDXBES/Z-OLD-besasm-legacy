// Project: Classes, File: ConflictPackage.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: ConflictPackage
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 22, Size of file: 480 Bytes
// Creation date: 6/30/2008 4:18 PM
// Last modified: 6/30/2008 5:48 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Modeling.Alternatives;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// A ConflictPackage packages conflict information about an alternative link
  /// and includes references to the AltLink, AltPackage, and PipeConflict (PipXP)
  /// record.
  /// </summary>
  public class ConflictPackage
  {
    #region Variables
    private AlternativePackage _AltPackage = null;
    private AltLink _AltLink = null;

    private Model _Model = null;
    private Link _Link = null;

    private Segment _Segment = null;
    private Conflict _Conflict = null;

    private PipeConflict _PipeConflict = null;
    #endregion

    #region Constructors
    /// <summary>
    /// Create conflict package from an alternative
    /// </summary>
    /// <param name="altPackage">An alternative package</param>
    /// <param name="link">An alternative link within the alternative package</param>
    /// <param name="altPipXP">The link's AltPipXP record</param>
    public ConflictPackage(AlternativePackage altPackage, AltLink altLink, AltPipXP altPipXP)
    {
      _AltPackage = altPackage;
      _AltLink = altLink;
      _PipeConflict = altPipXP;
    } // ConflictPackage(altPackage, link, altPipXP)

    /// <summary>
    /// Create conflict package from a model
    /// </summary>
    /// <param name="model">A model</param>
    /// <param name="link">A link within the model</param>
    /// <param name="pipXP">The link's PipXP record</param>
    public ConflictPackage(Model model, Link link, PipXP pipXP)
    {
      _Model = model;
      _Link = link;
      _PipeConflict = pipXP;
    } // ConflictPackage(model, link, pipXP)

    public ConflictPackage(Segment segment, Conflict conflict, PipXP pipXP)
    {
      _Segment = segment;
      _Conflict = conflict;
      _PipeConflict = pipXP;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Diameter in feet
    /// </summary>
    /// <returns>Double</returns>
    public float Diameter
    {
      get
      {
        return _Conflict == null ? (_AltPackage == null ? (float)_Link.Diameter : (float)_AltLink.Diameter) :
          _Segment.PipeDiamWidth;
      } // get
    } // Diameter

    /// <summary>
    /// Average depth in feet (average of upstream and downstream depths)
    /// </summary>
    /// <returns>Double</returns>
    public float Depth
    {
      get
      {
        return _Conflict == null ? (_AltPackage == null ? (float)_Model.PipeDepth(_Link) :
          (float)_AltPackage.PipeDepth(_AltLink)) : (_Conflict.USDepth + _Conflict.DSDepth) / 2;
      } // get
    } // Depth

    /// <summary>
    /// Length in feet
    /// </summary>
    /// <returns>Double</returns>
    public float Length
    {
      get
      {
        return _Conflict == null ? (_AltPackage == null ? (float)_Link.Length :
          (float)_AltLink.Length) : _Segment.LengthFt;
      } // get
    } // Length

    /// <summary>
    /// Slope in ft/ft
    /// </summary>
    /// <returns>Double</returns>
    public float Slope
    {
      get
      {
        return _Conflict == null ? (_AltPackage == null ? (float)((_Link.USIE - _Link.DSIE) / _Link.Length) :
          (float)((_AltLink.USIE - _AltLink.DSIE) / _AltLink.Length)) : 
          (_Segment.USElevation - _Segment.DSElevation) / _Segment.LengthFt;
      }
    } // Slope

    /// <summary>
    /// Pipe material
    /// </summary>
    /// <returns>Pipe material</returns>
    public PipeMaterial PipeMaterial
    {
      get
      {
        return _Conflict == null ? (_AltPackage == null ? PipeCoster.StringToPipeMaterial(_Link.Material) :
          PipeCoster.StringToPipeMaterial(_AltLink.Material)) : PipeCoster.StringToPipeMaterial(_Segment.Material);
      }
    } // PipeMaterial

    /// <summary>
    /// Upstream node ID
    /// </summary>
    /// <returns>String</returns>
    public string USNode
    {
      get
      {
        return _Conflict == null ? (_AltPackage == null ? _Link.USNodeName : _AltLink.USNodeName) :
          _Segment.USNodeID;
      } // get
    } // USNode

    /// <summary>
    /// Downstream node id
    /// </summary>
    /// <returns>String</returns>
    public string DSNode
    {
      get
      {
        return _Conflict == null ? (_AltPackage == null ? _Link.DSNodeName : _AltLink.DSNodeName) :
          _Segment.DSNodeID;
      } // get
    } // DSNode

    /// <summary>
    /// Master link ID
    /// </summary>
    /// <returns>Int</returns>
    public int MstLinkID
    {
      get
      {
        return _Conflict == null ? (_AltPackage == null ? _Link.MLinkID : 0) : 0;
      } // get
    } // MstLinkID

    /// <summary>
    /// Number of segments
    /// </summary>
    public int NumSegments
    {
      get
      {
        return _Segment == null ? 1 : _Segment.NumCuts;
      }
    }

    public float PipeLength
    {
      get
      {
        return _Segment == null ? Length : _Segment.PipeLength;
      }
    }
    /// <summary>
    /// Conflict information
    /// </summary>
    /// <returns>Pipe conflict record</returns>
    public PipeConflict Conflicts
    {
      get
      {
        return _PipeConflict;
      } // get
    } // Conflict

    public Conflict Conflict
    {
      get
      {
        return _Conflict;
      }
    }
    #endregion
  }
}
