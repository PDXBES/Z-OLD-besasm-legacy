using System;
using SystemsAnalysis;
using SystemsAnalysis.Types;
using SystemsAnalysis.DataAccess;

namespace SystemsAnalysis.Modeling.Alternatives
{
  /// <summary>
  /// Summary description for Node.
  /// </summary>
  public class AltDsc
  {
    private int altDscID;
    private int dscID;
    private int linkIDSan;
    private string linkSourceSan;
    private int linkIDStorm;
    private string linkSourceStorm;

    /// <summary>
    /// Creates a AltDsc from an AltDscRow DataRow.
    /// </summary>
    /// <param name="altDscRow">An AltDscRow DataRow from 
    /// SystemsAnalysis.DataAccess.AlternativeDataSet.</param>
    public AltDsc(DataAccess.AlternativeDataSet.AltDscRow altDscRow)
    {
      this.altDscID = altDscRow.AltDscID;
      this.dscID = altDscRow.MdlDscID;
      this.linkIDSan = altDscRow.LinkIDSan;
      this.linkSourceSan = altDscRow.IsLinkSourceSanNull() ? "" : altDscRow.LinkSourceSan;
      this.linkIDStorm = altDscRow.LinkIDStorm;
      this.linkSourceStorm = altDscRow.IsLinkSourceStormNull() ? "" : altDscRow.LinkSourceStorm;
    }

    /// <summary>
    /// Gets the AltDscID of this object.
    /// </summary>
    public int AltDscID
    {
      get { return this.altDscID; }
    }
    /// <summary>
    /// Gets the LinkIDSan of this object.
    /// </summary>
    public int LinkIDSan
    {
      get { return this.linkIDSan; }
      set { this.linkIDSan = value; }
    }
    /// <summary>
    /// Gets the LinkIDStorm of this object.
    /// </summary>
    public int LinkIDStorm
    {
      get { return this.linkIDStorm; }
      set { this.linkIDStorm = value; }
    }
    /// <summary>
    /// Gets the LinkSourceSan of this object.
    /// </summary>
    public string LinkSourceSan
    {
      get { return this.linkSourceSan; }      
    }
    /// <summary>
    /// Gets the LinkSourceStorm of this object.
    /// </summary>
    public string LinkSourceStorm
    {
      get { return this.linkSourceStorm; }
    }

    public int DscId
    {
      get { return this.dscID; }
    }

  }
}
