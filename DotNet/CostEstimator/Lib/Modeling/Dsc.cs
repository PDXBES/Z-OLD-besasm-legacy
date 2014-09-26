using System;
using SystemsAnalysis;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.Modeling
{
  /// <summary>
  /// Summary description for Node.
  /// </summary>
  public class Dsc
  {
    private int dscID;
    private int toMLinkSan;
    private int toMLinkStorm;
    private string discoClass;
    private double sanSwrCrown;
    private double fracToSwrBeg;
    private double floodRefElev;
    private bool falseBFRisk;
    private Types.Enumerators.ZoningTypes genZoneEX;
    private Types.Enumerators.ZoningTypes genZoneCP;
    private string zoneEX;
    private string zoneCP;
    private bool willDevelop; //INCIA4FB
    private bool isVacant;
    private Enumerators.DevelopmentState developmentState = Enumerators.DevelopmentState.Unspecified;
    private Enumerators.ConnectionAssumption connectionAssumption = Enumerators.ConnectionAssumption.Unspecified;
    private int area;
    private int roofAreaEX;
    private int roofAreaFB;
    private int parkAreaEX;
    private int parkAreaFB;
    private int icRoofEX;
    private int icRoofFB;
    private int icParkEX;
    private int icParkFB;
    private double eicRoofEX;
    private double eicRoofFB;
    private double eicParkEX;
    private double eicParkFB;
    private Enumerators.Sewerable sewerable;

    /// <summary>
    /// Creates a light-weight Dsc object containing only a DscID.
    /// </summary>
    /// <param name="DscID">The ID of the newly created Direct Subcatchment</param>
    public Dsc(int dscID)
    {
      this.dscID = dscID;
    }

    /// <summary>
    /// Creates a Dsc from an master Dsc DataRow.
    /// </summary>
    /// <param name="mstDscRow">A mst_DscRow DataRow from 
    /// SystemsAnalysis.DataAccess.SAMasterDataSet.  </param>
    public Dsc(DataAccess.SAMasterDataSet.MstDscRow mstDscRow)
    {
      throw new Exception("Not implemented");
      this.dscID = mstDscRow.DscID;
      this.toMLinkSan = mstDscRow.ToMLinkSan;
      this.toMLinkStorm = mstDscRow.ToMLinkStorm;
    }
    /// <summary>
    /// Creates a Dsc from a model Dsc DataRow.
    /// </summary>
    /// <param name="mdlDscRow">a MdlDscRow DataRow from
    /// SystemsAnalysis.ModelDataSet.  </param>
    public Dsc(DataAccess.ModelDataSet.MdlDscRow mdlDscRow)
    {
      //TODO: This in an incomplete constructor, since mdl_DirSC structure
      //will changes in EMG2.2.  For characterization purposes, all we need
      //to know about a mdl_DirSC is the list of DscIDs in that model - All
      //other Dsc information will come from mstDsc and DscHydraulics
      this.dscID = mdlDscRow.DscID;
    }
    /// <summary>
    /// Creates a Dsc from an OleDbDataReader object.  This data reader
    /// must contain entries corresponding to the fields in mst_Dsc.  This
    /// constructor will set all relevent Dsc parameters which correspond to 
    /// the public properties of this Object
    /// </summary>
    /// <param name="mstDscReader">An OleDbDataReader obtained from
    /// SystemsAnalysis.DataAccess.AGMasterDataAccess.MstDscDataAdapter.SelectCommand.ExecuteReader()  </param>
    internal Dsc(System.Data.IDataReader mstDscReader)
    {
      //mstDscReader.gets
      this.dscID = (int)mstDscReader["DscID"];
      this.toMLinkSan = (int)mstDscReader["ToMLinkSan"];
      this.toMLinkStorm = (int)mstDscReader["ToMLinkStorm"];
      //TODO: dsc.DiscoClass should be replaced with an enumerated type
      this.discoClass = (string)mstDscReader["DiscoClass"];
      this.sanSwrCrown = Convert.ToDouble(mstDscReader["SanSwrCrown"]);
      this.fracToSwrBeg = Convert.ToDouble(mstDscReader["Frac2SwrBeg"]);
      this.floodRefElev = Convert.ToDouble(mstDscReader["FloodRefElev"]);
      this.falseBFRisk = Convert.ToBoolean(mstDscReader["FalseBFRisk"]);
      this.genZoneEX = Types.Enumerators.GetZoningEnum(
      (string)mstDscReader["GenEX"]);
      this.genZoneCP = Types.Enumerators.GetZoningEnum(
      (string)mstDscReader["GenCP"]);
      this.area = (int)mstDscReader["AreaFt"];
      this.willDevelop = Convert.ToBoolean(mstDscReader["INCIA4FB"]);
      this.isVacant = Convert.ToBoolean(mstDscReader["IsVacant"]);
      this.roofAreaEX = (int)mstDscReader["RfAreaFtEX"];
      this.roofAreaFB = (int)mstDscReader["RfAreaFtFB"];
      this.parkAreaEX = (int)mstDscReader["PkAreaFtEX"];
      this.parkAreaFB = (int)mstDscReader["PkAreaFtFB"];
      this.icRoofEX = (int)mstDscReader["ICFtRoofEX"];
      this.icRoofFB = (int)mstDscReader["ICFtRoofFB"];
      this.icParkEX = (int)mstDscReader["ICFtParkEX"];
      this.icParkFB = (int)mstDscReader["ICFtParkFB"];
      this.eicRoofEX = Convert.ToDouble(mstDscReader["EICFtRoofEX"]);
      this.eicRoofFB = Convert.ToDouble(mstDscReader["EICFtRoofFB"]);
      this.eicParkEX = Convert.ToDouble(mstDscReader["EICFtParkEX"]);
      this.eicParkFB = Convert.ToDouble(mstDscReader["EICFtParkFB"]);
      this.sewerable = (Enumerators.Sewerable)mstDscReader["Sewerable"];
      this.zoneEX = (string)mstDscReader["ZoneEX"];
      this.zoneCP = (string)mstDscReader["ZoneCP"];
    }

    #region Property accessors
    /// <summary>
    /// Gets the DscID of this object.
    /// </summary>
    public int DscID
    {
      get
      {
        return this.dscID;
      }
    }
    /// <summary>
    /// Gets the ToMLinkSan of this object.
    /// </summary>
    public int ToMLinkSan
    {
      get
      {
        return this.toMLinkSan;
      }
    }
    /// <summary>
    /// Gets the ToMLinkStorm of this object.
    /// </summary>
    public int ToMLinkStorm
    {
      get
      {
        return this.toMLinkStorm;
      }
    }
    /// <summary>
    /// Gets a char representing the DiscoClass of this object.
    /// </summary>
    public string DiscoClass
    {
      get
      {
        return this.discoClass;
      }
    }
    /// <summary>
    /// Gets the Sanitary Sewer Crown elevation of this object.
    /// </summary>
    public double SanSwrCrown
    {
      get
      {
        return this.sanSwrCrown;
      }
    }
    /// <summary>
    /// Gets a fraction representing the position of this Dsc along
    /// it's connected sanitary lateral.
    /// </summary>
    public double FracToSwrBeg
    {
      get
      {
        return this.fracToSwrBeg;
      }
    }
    /// <summary>
    /// Gets the flood reference elevation of this object.
    /// </summary>
    public double FloodRefElev
    {
      get
      {
        return this.floodRefElev;
      }
    }
    /// <summary>
    /// "true" if this parcel has potential false positive flooding risk.
    /// </summary>
    public bool FalseBFRisk
    {
      get
      {
        return floodRefElev - 8 <= sanSwrCrown;
      }
    }// this.falseBFRisk; } }
    /// <summary>
    /// Returns the existing conditions general zoning code
    /// </summary>
    public Types.Enumerators.ZoningTypes GenZoneEX
    {
      get
      {
        return this.genZoneEX;
      }
    }
    /// <summary>
    /// Returns the comprehensive plan general zoning code
    /// </summary>
    public Types.Enumerators.ZoningTypes GenZoneCP
    {
      get
      {
        return this.genZoneCP;
      }
    }
    /// <summary>
    /// Returns the existing conditions specific zoning code
    /// </summary>
    public string ZoneEX
    {
      get
      {
        return this.zoneEX;
      }
    }
    /// <summary>
    /// Returns the comprehensive plan specific zoning code
    /// </summary>
    public string ZoneCP
    {
      get
      {
        return this.zoneCP;
      }
    }
    /// <summary>
    /// Indicates whether modeling assumptions indicate this property is likely to develop/redevelop. Also called "INCIA4FB".
    /// </summary>
    public bool WillDevelop
    {
      get
      {
        return this.willDevelop;
      }
    }
    /// <summary>
    /// Returns the vacancy status of the Dsc
    /// </summary>
    public bool IsVacant
    {
      get
      {
        return this.isVacant;
      }
    }
    /// <summary>
    /// Returns the development state of the Dsc. This may be deprecated by ConnectionAssumption
    /// </summary>
    public Enumerators.DevelopmentState DevelopmentState
    {
      get
      {
        if (this.developmentState == Enumerators.DevelopmentState.Unspecified)
        {
          switch (sewerable)
          {
            case Enumerators.Sewerable.Sewered:
              if (WillDevelop)
                this.developmentState = Enumerators.DevelopmentState.Redevelopment;
              else
                this.developmentState = Enumerators.DevelopmentState.Static;
              break;
            case Enumerators.Sewerable.Sewerable:
              this.developmentState = Enumerators.DevelopmentState.NewDevelopment;
              break;
            case Enumerators.Sewerable.Unsewerable:
              this.developmentState = Enumerators.DevelopmentState.Undevelopable;
              break;
          }
        }
        return this.developmentState;
      }
    }
    /// <summary>
    /// Returns the future connection assumption for the Dsc.
    /// </summary>
    public Enumerators.ConnectionAssumption ConnectionAssumption
    {
      get
      {
        switch (sewerable)
        {
          case Enumerators.Sewerable.Sewered:
            if (WillDevelop)
              this.connectionAssumption = Enumerators.ConnectionAssumption.ConnectedFutureRedevelopment;
            else
              this.connectionAssumption = Enumerators.ConnectionAssumption.ConnectedNoChange;
            break;
          case Enumerators.Sewerable.Sewerable:
            this.connectionAssumption = Enumerators.ConnectionAssumption.FutureNewConnection;
            break;
          case Enumerators.Sewerable.Unsewerable:
            this.connectionAssumption = Enumerators.ConnectionAssumption.WillNotConnect;
            break;
        }
        return this.connectionAssumption;
      }
    }

    /// <summary>
    /// Returns the area in sq. ft. of this Dsc
    /// </summary>
    public int Area
    {
      get
      {
        return this.area;
      }
    }
    /// <summary>
    /// Returns the existing condition roof area in sq. ft. of this Dsc
    /// </summary>
    public int RoofAreaEX
    {
      get
      {
        return this.roofAreaEX;
      }
    }
    /// <summary>
    /// Returns the assumed roof area in sq. ft. of this Dsc
    /// </summary>
    public int RoofAreaFB
    {
      get
      {
        return this.roofAreaFB;
      }
    }
    /// <summary>
    /// Returns the existing condition parking lot area in sq. ft. of this Dsc
    /// </summary>
    public int ParkAreaEX
    {
      get
      {
        return this.parkAreaEX;
      }
    }
    /// <summary>
    /// Returns the assumed parking lot area in sq. ft. of this Dsc
    /// </summary>
    public int ParkAreaFB
    {
      get
      {
        return this.parkAreaFB;
      }
    }
    public int ICRoofEX
    {
      get
      {
        return this.icRoofEX;
      }
    }
    public int ICRoofFB
    {
      get
      {
        return this.icRoofFB;
      }
    }
    public int ICParkEX
    {
      get
      {
        return this.icParkEX;
      }
    }
    public int ICParkFB
    {
      get
      {
        return this.icParkFB;
      }
    }
    public double EICRoofEX
    {
      get
      {
        return this.eicRoofEX;
      }
    }
    public double EICRoofFB
    {
      get
      {
        return this.eicRoofFB;
      }
    }
    public double EICParkEX
    {
      get
      {
        return this.eicParkEX;
      }
    }
    public double EICParkFB
    {
      get
      {
        return this.eicParkFB;
      }
    }
    /// <summary>
    /// Indications the connection status of this Dsc. 0 = "No determination" 1 = "Sewered", 2 = "Unsewered", 3 = "Unsewerable", 4 = "Septic"
    /// </summary>
    public Enumerators.Sewerable Sewerable
    {
      //TODO: Sewerable should be an enumerator type
      get
      {
        return this.sewerable;
      }
    }
    #endregion
  }
}
