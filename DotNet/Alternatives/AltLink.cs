using System;
using SystemsAnalysis;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.Modeling.Alternatives
{
  /// <summary>
  /// Summary description for Link.
  /// </summary>
  public class AltLink
  {
    private int altLinkID;
    private string usNodeName;
    private string dsNodeName;
    private int linkID;
    private Enumerators.AlternativeOperation operation;
    private double length;
    private string material;
    private double usIE;
    private double dsIE;
    private double diameter;
    private double height;
    private string focusArea;

    /// <summary>
    /// Creates a new link from the MstLink database.
    /// </summary>
    /// <param name="mstLinksRow">A row from DataAccess.SAMasterDataSet</param>
    public AltLink(DataAccess.AlternativeDataSet.AltLinksRow altLinksRow)
    {
      this.altLinkID = altLinksRow.AltLinkID;
      this.usNodeName = altLinksRow.USNode;
      this.dsNodeName = altLinksRow.DSNode;
      this.linkID = altLinksRow.MdlLinkID;
      this.operation = Types.Enumerators.GetAlternativeOperationEnum(altLinksRow.Operation);
      this.length = altLinksRow.Length;
      this.material = altLinksRow.IsMaterialNull() ? "" : altLinksRow.Material;
      this.usIE = altLinksRow.USIE;
      this.dsIE = altLinksRow.DSIE;
      this.diameter = altLinksRow.DiamWidth;
      this.focusArea = altLinksRow.IsFocusAreaNull() ? "" : altLinksRow.FocusArea;
    }

    /// <summary>
    /// Gets the AltLinkID of this AltLink
    /// </summary>
    public int AltLinkID
    {
      get { return this.altLinkID; }
    }
    /// <summary>
    /// Gets the LinkID of this AltLink, if present.
    /// </summary>
    public int LinkID
    {
      get { return this.linkID; }
    }
    /// <summary>
    /// Gets the Upstream Node name of this AltLink
    /// </summary>
    public string USNodeName
    {
      get { return this.usNodeName; }
    }
    /// <summary>
    /// Gets the Downstream Node name of this AltLink
    /// </summary>
    public string DSNodeName
    {
      get { return this.dsNodeName; }
    }
    /// <summary>
    /// Gets the Operation of this AltLink
    /// </summary>
    public Enumerators.AlternativeOperation Operation
    {
      get { return this.operation; }
    }

    /// <summary>
    /// Gets the length of this AltLink
    /// </summary>
    public double Length
    {
      get
      {
        return this.length;
      }
    }

    public string Material
    {
      get
      {
        return this.material;
      }
    }

    public double USIE
    {
      get
      {
        return this.usIE;
      }
    }

    public double DSIE
    {
      get
      {
        return this.dsIE;
      }
    }

    public double Diameter
    {
      get
      {
        return this.diameter;
      }
    }

    public string FocusArea
    {
      get
      {
        return focusArea;
      }
    }
  }
}
