using System;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Modeling.ModelResults
{
  /// <summary>
  /// Summary description for DSCHydraulics.
  /// </summary>
  public class DscHydraulic
  {
    private int dscHydraulicsID;
    private int dscid;
    private double hgl;
    private double deltaHGL;
    private double surcharge;
    private Dsc dsc;
    private bool hasDsc;

    internal DscHydraulic(System.Data.IDataReader mstDSCHydraulicsReader)
    {
      this.dscHydraulicsID = (int)mstDSCHydraulicsReader["DSCHydraulicsID"];
      this.dscid = (int)mstDSCHydraulicsReader["DSCID"];
      this.hgl = Convert.ToDouble(mstDSCHydraulicsReader["HGL"]);
      this.deltaHGL = Convert.ToDouble(mstDSCHydraulicsReader["deltaHGL"]);
      this.surcharge = Convert.ToDouble(mstDSCHydraulicsReader["surcharge"]);
    }
    /// <summary>
    /// Returns the DSCID associated with this DSC hydraulic modeling results.
    /// The DSCID is equal to ParcelID * 100 + DivideID.
    /// </summary>
    public int DscID
    {
      get { return this.dscid; }
    }
    public int DscHydraulicsID
    {
      get { return this.dscHydraulicsID; }
    }
    /// <summary>
    /// Returns the peak HGL all the pipe that this DSC is connected to.
    /// </summary>
    public double HGL
    {
      get { return this.hgl; }
    }
    /// <summary>
    /// Returns the difference between the peak HGL and the finished floor elevation
    /// for a given DSC.
    /// </summary>
    public double DeltaHGL
    {
      get { return this.deltaHGL; }
    }
    /// <summary>
    /// Returns the surcharge elevation of pipe connected to this DSC, at the point of
    /// connection.
    /// </summary>
    public double Surcharge
    {
      get { return this.surcharge; }
    }
    /// <summary>
    /// Gets or sets the DSC object associated with this DSC hydraulic results
    /// object. Returns null is this DSC hydraulic result does not have an 
    /// associated DSC object.  
    /// </summary>
    public Dsc Dsc
    {
      get
      {
        if (this.hasDsc)
        {
          return this.dsc;
        }
        else
        {
          return null;
        }
      }
      set
      {
        if (value != null && value.DscID == this.DscID)
        {
          this.hasDsc = true;
          this.dsc = value;
        }
        else
        {
          throw new Exception("Mismatched Dsc added to DscHydraulic");
        }
      }
    }
    /// <summary>
    /// Indicates if this DSCHydraulic object contains a reference to 
    /// an associated DSC object.
    /// </summary>
    public bool HasDsc
    {
      get { return this.hasDsc; }
    }
  }
}
