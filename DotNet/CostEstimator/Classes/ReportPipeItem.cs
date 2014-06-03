// Project: Classes, File: ReportPipeItem.cs
// Namespace: CostEstimator.Classes, Class: ReportPipeItem
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 25, Size of file: 527 Bytes
// Creation date: 4/29/2008 12:31 PM
// Last modified: 1/6/2011 9:18 AM

#region Using directives
using System;
using System.Xml;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// A pipe reporting item
  /// </summary>
  public class ReportPipeItem : ReportItem
  {
    #region Variables
    private double _Length;
    #endregion

    #region Properties
    /// <summary>
    /// Name
    /// </summary>
    /// <returns>String</returns>
    public string Name { get; // get
      set; // set
    } // Name

    /// <summary>
    /// ID
    /// </summary>
    /// <returns>Int</returns>
    public int ID { get; // get
      set; // set
    } // ID

    /// <summary>
    /// Material type
    /// </summary>
    /// <returns>String</returns>
    public string MaterialType { get; // get
      set; // set
    } // MaterialType

    /// <summary>
    /// Diameter in
    /// </summary>
    /// <returns>Double</returns>
    public double DiameterIn { get; // get
      set; // set
    } // DiameterIn

    /// <summary>
    /// Depth ft
    /// </summary>
    /// <returns>Double</returns>
    public double DepthFt { get; // get
      set; // set
    } // DepthFt

    /// <summary>
    /// Length
    /// </summary>
    /// <returns>Double</returns>
    public double Length
    {
      get
      {
        return _Length;
      } // get

      set
      {
        _Length = value < 1 ? 1 : value;
      } // set
    } // Length

    /// <summary>
    /// Excavation vol cu yd
    /// </summary>
    /// <returns>Double</returns>
    public double ExcavationVolCuYd { get; // get
      set; // set
    } // ExcavationVolCuYd

    /// <summary>
    /// Manhole cost
    /// </summary>
    /// <returns>float</returns>
    public float ManholeCost
    {
      get
      {
        return (float)(double)(Manhole != null ? Manhole.Cost * PipeAndManhole.Factor : 0.0);
      } // get
    } // ManholeCost

    /// <summary>
    /// Direct construction cost per ft
    /// </summary>
    /// <returns>float</returns>
    public float DirectConstructionCostPerFt
    {
      get
      {
        return Manhole != null ?
        (float)((double)(PipeAndManhole.ChildCost - Manhole.Cost ) / _Length * PipeAndManhole.Factor) :
        (float)((double)(PipeAndManhole.ChildCost) / _Length * PipeAndManhole.Factor);
      } // get
    } // DirectConstructionCostPerFt

    /// <summary>
    /// Total construction cost
    /// </summary>
    /// <returns>float</returns>
    public float TotalConstructionCost
    {
      get
      {
        CostItemFactor estimate = PipeAndManhole.ParentCostItemFactor.ParentCostItemFactor;
        double factor = estimate.Factor;
        return (float)((double)DirectConstructionCost * factor);
      }
    } // TotalConstructionCost

    /// <summary>
    /// Manhole
    /// </summary>
    /// <returns>Cost item factor</returns>
    public CostItemFactor Manhole { get; // get
      set; // set
    } // Manhole

    /// <summary>
    /// Pipe
    /// </summary>
    /// <returns>Cost item factor</returns>
    public CostItemFactor Pipe { get; // get
      set; // set
    } // Pipe

    /// <summary>
    /// Pipe and manhole
    /// </summary>
    /// <returns>Cost item factor</returns>
    public CostItemFactor PipeAndManhole { get; // get
      set; // set
    } // PipeAndManhole

    /// <summary>
    /// Direct construction pipe cost
    /// </summary>
    /// <returns>float</returns>
    public float DirectConstructionCost
    {
      get
      {
        return (float)(_Length * (double)DirectConstructionCostPerFt) +
          ManholeCost;
      } // get
    } // DirectConstructionPipeCost

    /// <summary>
    /// Comment
    /// </summary>
    /// <returns>String</returns>
    public string Comment { get; // get
      set; // set
    } // Comment

    /// <summary>
    /// Construction duration
    /// </summary>
    /// <returns>String</returns>
    public double ConstructionDuration { get;
      set; } // ConstructionDuration
    #endregion

    #region Methods
    /// <summary>
    /// Write XML
    /// </summary>
    /// <param name="xw">Xw</param>
    public void WriteXML(XmlWriter xw)
    {
      xw.WriteStartElement("id");
      xw.WriteValue(ID);
      xw.WriteEndElement();

      xw.WriteStartElement("name");
      xw.WriteValue(Name);
      xw.WriteEndElement();

      xw.WriteStartElement("materialType");
      xw.WriteValue(MaterialType);
      xw.WriteEndElement();

      xw.WriteStartElement("diameterIn");
      xw.WriteValue(DiameterIn);
      xw.WriteEndElement();

      xw.WriteStartElement("depthFt");
      xw.WriteValue(DepthFt);
      xw.WriteEndElement();

      xw.WriteStartElement("length");
      xw.WriteValue(_Length);
      xw.WriteEndElement();

      xw.WriteStartElement("excavationVolCuYd");
      xw.WriteValue(ExcavationVolCuYd);
      xw.WriteEndElement();

      xw.WriteStartElement("comment");
      xw.WriteValue(Comment ?? string.Empty);
      xw.WriteEndElement();

      xw.WriteStartElement("manholeID");
      xw.WriteValue(Manhole.ID);
      xw.WriteEndElement();

      xw.WriteStartElement("pipeID");
      xw.WriteValue(Pipe.ID);
      xw.WriteEndElement();

      xw.WriteStartElement("pipeAndManholeID");
      xw.WriteValue(PipeAndManhole.ID);
      xw.WriteEndElement();

      xw.WriteStartElement("constructionDuration");
      xw.WriteValue(ConstructionDuration);
      xw.WriteEndElement();
    } // WriteXML()
    #endregion
  }
}
