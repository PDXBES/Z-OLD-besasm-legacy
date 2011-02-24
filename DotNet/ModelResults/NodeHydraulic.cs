using System;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Modeling.ModelResults
{
  /// <summary>
  /// Summary description for NodeHydraulics.
  /// </summary>
  public class NodeHydraulic
  {
    private int nodeHydraulicsID;
    private string nodeName;
    private double maxElevation;
    private double surcharge;
    private double freeboard;
    private double surchargeTime;
    private double floodedTime;

    /// <summary>
    /// Constructs a NodeHydraulic object using a ADO.NET data reader. Should only be
    /// called from the NodeHydraulics collection.
    /// </summary>
    /// <param name="mstNodeHydraulicsReader">An ADO.NET data reader containing a DataRow
    /// that matches the node hydraulics schema.</param>
    internal NodeHydraulic(System.Data.IDataReader mstNodeHydraulicsReader)
    {
      this.nodeHydraulicsID = (int)mstNodeHydraulicsReader["nodeHydraulicsID"];
      this.nodeName = (string)mstNodeHydraulicsReader["nodeName"];
      this.maxElevation = Convert.ToDouble(mstNodeHydraulicsReader["maxElevation"]);
      this.surcharge = Convert.ToDouble(mstNodeHydraulicsReader["surcharge"]);
      this.freeboard = Convert.ToDouble(mstNodeHydraulicsReader["freeboard"]);
      this.surchargeTime = Convert.ToDouble(mstNodeHydraulicsReader["surchargeTime"]);
      this.floodedTime = Convert.ToDouble(mstNodeHydraulicsReader["floodedTime"]);
    }

    /// <summary>
    /// The six character "licencse plate" ID of this node, AKA the Hansen ID.
    /// </summary>
    public string NodeName
    {
      get { return this.nodeName; }
    }
    /// <summary>
    /// The ID of the corresponding hydraulic result entry in the Model Catalog
    /// </summary>
    public int NodeHydraulicsID
    {
      get { return this.nodeHydraulicsID; }
    }
    /// <summary>
    /// The maximum water surface elevation at this node.
    /// </summary>
    public double MaxElevation
    {
      get { return this.maxElevation; }
    }
    /// <summary>
    /// The amount of surcharge at this node, measured relative to the crown
    /// of the highest pipe connected to this node.
    /// </summary>
    public double Surcharge
    {
      get { return this.surcharge; }
    }
    /// <summary>
    /// The distance between the maximum water surface elevation and the ground
    /// elvation in feet. If this is 0 or less, then street flooding is occcuring.
    /// </summary>
    public double Freeboard
    {
      get { return this.freeboard; }
    }
    /// <summary>
    /// The amount of time in minutes that pipes connected to this node are 
    /// experiencing surcharging.
    /// </summary>
    public double SurchargeTime
    {
      get { return this.surchargeTime; }
    }
    /// <summary>
    /// The amount of time in minutes that this node is flooding to the street.
    /// </summary>
    public double FloodedTime
    {
      get { return this.floodedTime; }
    }
  }
}
