using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Types;
using SystemsAnalysis.Utils.Events;

namespace SystemsAnalysis.Modeling
{
  /// <summary>
  /// A strongly-typed collection of Dsc objects.
  /// </summary>
  public class Dscs : DictionaryBase
  {
    Hashtable toLinkSanList;
    Hashtable toLinkStormList;

    /// <summary>
    /// Creates an empty Dscs collection.
    /// </summary>
    public Dscs()
    {
    }

    /// <summary>
    /// Loads a Dscs collection. This collection will contain a
    /// Dsc object corresponding to each row in the provided mdlDsc table.
    /// </summary>
    /// <param name="mdlDscTable">A SystemsAnalysis.DataAccess.ModelComponents.MdlDscDataTable
    /// that will be used to fill this collection.  </param>
    /// <returns>The number of Dsc object loaded into this collection.</returns>
    /// <remarks>This collection is filled via Load() rather than in the constructor
    /// because it may be a time consuming operation to create a Dscs collection. By
    /// loading the Dscs collection here it is possible for the calling method to listen
    /// for the StatusChanged event to monitor loading progress.  </remarks>
    public int LoadFromModel(string modelPath)
    {
      ModelDataSet.MdlDscDataTable mdlDscDT = new ModelDataSet.MdlDscDataTable();
      DataAccess.ModelDataSetTableAdapters.MdlDscTableAdapter mdlDscAdapter;
      mdlDscAdapter = new DataAccess.ModelDataSetTableAdapters.MdlDscTableAdapter(modelPath);
      mdlDscAdapter.Fill(mdlDscDT);

      foreach (ModelDataSet.MdlDscRow mdlDscRow in mdlDscDT)
      {
        this.Add(new Dsc(mdlDscRow));
          int quo, rem;
        quo = Math.DivRem(this.Count, 1000, out rem);
        if (rem == 0)
        {
          this.OnStatusChanged(new StatusChangedArgs("Created Dsc #" +
          this.Count.ToString() + "."));
        }
      }
      return this.Count;
    }
    public int Load(ModelDataSet.MdlDscDataTable mdlDscTable)
    {
      throw new Exception("This function has been replaced by LoadFromModel(string modelPath)");
    }


    /// <summary>
    /// Loads a Dscs collection. This collection will contain a subset
    /// of master Dsc which is connected to the Links collection through the 
    /// ToMLinkSan or ToMLinkStorm fields.
    /// </summary>
    /// <param name="links">A Links collection whose MLinkIDs exist in
    /// the master Dsc field ToMLinkSan or ToMLinkStorm.  </param>
    /// <returns>The number of Dsc object loaded into this collection.</returns>
    /// <remarks>This collection is filled via Load() rather than in the constructor
    /// because it may be a time consuming operation to create a Dscs collection. By
    /// loading the Dscs collection here it is possible for the calling method to listen
    /// for the StatusChanged event to monitor loading progress.  </remarks>
    public int LoadFromMaster(Links links)
    {
      DataAccess.SAMasterDataSetTableAdapters.MstDscTableAdapter mstDscTA;
      mstDscTA = new DataAccess.SAMasterDataSetTableAdapters.MstDscTableAdapter();

      //Table loaded through direct SQL statement rather than TableAdapter due to performance constraints
      string sql = "SELECT * FROM Mst_Dsc";
      string connString = mstDscTA.Connection.ConnectionString;
      System.Data.SqlClient.SqlDataReader mstDscReader;
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connString);

      System.Data.SqlClient.SqlCommand selCmd = new System.Data.SqlClient.SqlCommand(sql, conn);
      selCmd.CommandType = System.Data.CommandType.Text;
      conn.Open();
      mstDscReader = selCmd.ExecuteReader();

      while (mstDscReader.Read())
      {
        int toMLinkSan = (int)mstDscReader["ToMLinkSan"];
        int toMLinkStorm = (int)mstDscReader["ToMLinkStorm"];
        string genEx = (string)mstDscReader["GenEx"];
        string genCP = (string)mstDscReader["GenCP"];

        if (((toMLinkSan != 0 && links.FindByMLinkID(toMLinkSan) != null) ||
        (toMLinkStorm != 0 && links.FindByMLinkID(toMLinkStorm) != null)) &&
        (genEx.Trim() != "" || genCP.Trim() != ""))
        {
          if (!this.Contains((int)mstDscReader["DscID"]))
          {
            this.Add(new Dsc(mstDscReader));

              int quo, rem;
            quo = Math.DivRem(this.Count, 1000, out rem);
            if (rem == 0)
            {
              this.OnStatusChanged(new StatusChangedArgs("Created Dsc #" +
              this.Count.ToString() + "."));
            }
          }
        }
      }
      mstDscReader.Close();
      GC.Collect();
      return this.Count;
    }

    /// <summary>
    /// Deprecated function that has been replaced by LoadFromMaster.
    /// </summary>
    /// <param name="links"></param>
    /// <returns></returns>
    public int Load(Links links)
    {
      throw new Exception("This function has been replaced by LoadFromMaster(Links links)");
    }

    /// <summary>
    /// Selects a Dscs collection that connects to the provided Links collection.
    /// </summary>
    /// <param name="links">A Links collection whose connected Dscs should be returned</param>
    /// <returns>A Dscs collection containing the Dsc object connected to the provided Links collection</returns>
    public Dscs Select(Links links)
    {
      Dscs selectedDscs = new Dscs();
      foreach (Dsc d in this.Values)
      {
        Link l = links.FindByMLinkID(d.ToMLinkSan);
        if (l == null)
        {
          l = links.FindByMLinkID(d.ToMLinkStorm);
        }
        if (l == null)
        {
          continue;
        }
        selectedDscs.Add(d);
      }
      return selectedDscs;
    }
    /// <summary>
    /// Returns the number of Dsc objects in this collection that
    /// have a given zoning.
    /// </summary>
    /// <param name="zoning">The SystemsAnalysis.Types.Enumerators.ZoningTypes
    /// enumerator to count.  </param>
    /// <param name="timeFrame">The SystemsAnalysis.Types.Enumerators.TimeFrames
    /// that should be counted. If TimeFrames.EX then zoning is taken from Dsc.GenEX.
    /// If TimesFrames.FU then zoning is taken from Dsc.GenCP.  </param>
    /// <returns>The number of Dsc objects that match the specified zoning.</returns>
    public int CountByZoning(Enumerators.ZoningTypes zoning, Enumerators.TimeFrames timeFrame)
    {
      int count = 0;

      foreach (Dsc dsc in this.Values)
      {
        if (zoning ==
        (Enumerators.TimeFrames.EX == timeFrame ? dsc.GenZoneEX : dsc.GenZoneCP))
        {
          count++;
        }
      }

      return count;
    }
    /// <summary>
    /// Returns the number of Dsc objects in  this collection that match the sewerable flag.
    /// </summary>
    /// <param name="sewerable">The sewerable value to count</param>
    /// <returns>The number of Dsc objects matches the specified sewerable flag</returns>
    public int CountBySewerable(Enumerators.Sewerable sewerable)
    {
      int count = 0;
      foreach (Dsc dsc in this.Values)
      {
        if (dsc.Sewerable == sewerable)
        {
          count++;
        }
      }
      return count;
    }
    /// <summary>
    /// Returns the area in acres of Dsc objects in this collection that
    /// have a given zoning.
    /// </summary>
    /// <param name="zoning">The SystemsAnalysis.Types.Enumerators.ZoningTypes
    /// enumerator to count.  </param>
    /// <param name="timeFrame">The SystemsAnalysis.Types.Enumerators.TimeFrames
    /// that should be counted; If TimeFrames.EX then zoning is taken from Dsc.GenEX.
    /// If TimesFrames.FU then zoning is taken from Dsc.GenCP.  </param>
    /// <returns>The area in acres of Dsc objects that match the specified zoning</returns>
    public int AreaByZoning(Enumerators.ZoningTypes zoning, Enumerators.TimeFrames timeFrame)
    {
      double area = 0;

      foreach (Dsc dsc in this.Values)
      {
        if (zoning ==
        (Enumerators.TimeFrames.EX == timeFrame ? dsc.GenZoneEX : dsc.GenZoneCP))
        {
          area += dsc.Area;
        }
      }

      return Convert.ToInt32(area) / 43560;
    }

    /// <summary>
    /// Returns the area in acres of all Dsc objects in this collection.
    /// </summary>
    /// <returns>Total area in acres of all Dsc objects in this collection</returns>
    public int Area()
    {
      double area = 0;

      foreach (Dsc dsc in this.Values)
      {
        area += dsc.Area;
      }
      return Convert.ToInt32(area) / 43560;
    }
    /// <summary>
    /// Returns the area in acres of all Dsc objects in this collection that match the sewerable flag.
    /// </summary>
    /// <param name="sewerable">The Sewerable enumerated type to count</param>
    /// <returns>The area in this Dsc collection that matches the sewerable flag</returns>
    public int AreaBySewerable(Enumerators.Sewerable sewerable)
    {
      double area = 0;

      foreach (Dsc dsc in this.Values)
      {
        if (dsc.Sewerable == sewerable)
        {
          area += dsc.Area;
        }
      }
      return Convert.ToInt32(area) / 43560;
    }

    /// <summary>
    /// Return the number of Dsc object within this collection that have a
    /// roof disconnection rate within the range specified by the min and
    /// max parameters. Roof disconnection fraction is calculated as follows:
    /// discoFraction = (dsc.EICParkEX + dsc.EICRoofEX) /(dsc.ParkAreaEX + dsc.RoofAreaEX)
    /// </summary>
    /// <param name="min">The mininum disconnection fraction value to count (inclusive). 
    /// To select an upper bound only use System.Double.MinValue for this parameter.  </param>
    /// <param name="max">The maximum disconnection fraction value to count (inclusive). 
    /// To select an upper bound only use System.Double.MinValue for this parameter.  </param>
    /// <returns>The total number of Dsc objects with a disconnection fraction within
    /// the specified range.  </returns>
    public int RoofDiscoCount(double min, double max)
    {
      throw new Exception("Deprecated - Use ModelReport.RoofDiscoCount");
    }

    /// <summary>
    /// Counts the number of Dsc objects in this collection with the
    /// specified disconnection class.
    /// </summary>
    /// <param name="discoClass">The disconnection class to count.</param>
    /// <returns>The number of Dsc objects in this collection with the
    /// specified disconnection class.  </returns>
    public int CountByDiscoClass(string discoClass)
    {
      throw new Exception("Deprecated - Use ModelReport.DiscoClassCount");
      //TODO: DiscoClass should be made an enumerated type.            
    }

    /// <summary>
    /// Return the number of Dsc object of a specified zoning within this 
    /// collection that have a roof disconnection rate within the range 
    /// specified by the min and max parameters. Roof disconnection fraction 
    /// is calculated as follows:
    /// discoFraction = (dsc.EICParkEX + dsc.EICRoofEX) /(dsc.ParkAreaEX + dsc.RoofAreaEX)
    /// </summary>
    /// <param name="zoning"></param>
    /// <param name="timeFrame"></param>
    public int RoofDiscoCountByZoning(Enumerators.ZoningTypes zoning, Enumerators.TimeFrames timeFrame)
    {
      int count = 0;
      foreach (Dsc dsc in this.Values)
      {
        //TODO: dsc.DiscoClass should be replaced with an enumerated type
        if (zoning ==
        (Enumerators.TimeFrames.EX == timeFrame ? dsc.GenZoneEX : dsc.GenZoneCP))
        {
          double discoFraction;
          discoFraction = timeFrame == Enumerators.TimeFrames.EX
          ? (dsc.EICParkEX + dsc.EICRoofEX) /
          (dsc.ParkAreaEX + dsc.RoofAreaEX + System.Double.Epsilon)
          : (dsc.EICParkFB + dsc.EICRoofFB) /
          (dsc.ParkAreaFB + dsc.RoofAreaFB + System.Double.Epsilon);
          if (discoFraction > 0 && dsc.DiscoClass != "S")
          {
            count++;
          }
        }
      }
      return count;
    }

    /// <summary>
    /// Counts all Dsc object in this collection are potential false positives
    /// for basement flooding risk (Dsc.FalseBFRisk == true).
    /// </summary>
    /// <returns>The number of false postive Dsc object in this collection.</returns>
    public int CountByFalseBFRisk()
    {
      int count = 0;
      foreach (Dsc dsc in this.Values)
      {
        if (dsc.FalseBFRisk)
        {
          count++;
        }
      }
      return count;
    }

    public double DesignManualBaseFlow(Enumerators.TimeFrames timeFrame)
    {
      double dmBF = 0;
      SAMasterDataSet.MstZoningToGenDataTable zoningToGenDT;
      zoningToGenDT = new SAMasterDataSet.MstZoningToGenDataTable();
      DataAccess.SAMasterDataSetTableAdapters.MstZoningToGenTableAdapter zoningToGenTA;
      zoningToGenTA = new DataAccess.SAMasterDataSetTableAdapters.MstZoningToGenTableAdapter();
      zoningToGenTA.Fill(zoningToGenDT);
      foreach (Dsc d in this.Values)
      {
        if (timeFrame == Enumerators.TimeFrames.EX && d.Sewerable == Enumerators.Sewerable.Sewered)
        {
          dmBF += (double)zoningToGenDT.FindByZone(d.ZoneEX).SanBFPerNetAcre * d.Area / 43560.0;
        }
        else
          if (timeFrame == Enumerators.TimeFrames.FU &&
          (d.Sewerable == Enumerators.Sewerable.Sewered ||
          d.Sewerable == Enumerators.Sewerable.Sewerable ||
          d.Sewerable == Enumerators.Sewerable.Septic))
          {
            dmBF += (double)zoningToGenDT.FindByZone(d.ZoneCP).SanBFPerNetAcre * d.Area / 43560.0;
          }
      }
      return dmBF;
    }

    #region Overrided methods from DictionaryBase
    /// <summary>
    /// Gets the Dsc object with the specified dscID. 		
    /// </summary>
    public Dsc this[int dscID]
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        return (Dsc)this.Dictionary[dscID];
      }
      set
      {
        this.Dictionary[dscID] = value;
      }
    }
    /// <summary>
    /// Adds a Dsc object to this collection.
    /// </summary>
    /// <param name="dsc">The Dsc object to add to this collection.</param>
    public void Add(Dsc dsc)
    {
      this.Dictionary.Add(dsc.DscID, dsc);
    }
    /// <summary>
    /// Removes a Dsc object from this Dscs collection.
    /// </summary>
    /// <param name="dscID">The dscID of the Dsc object to remove 
    /// from this Dscs collection.   </param>
    public void Remove(int dscID)
    {
      this.Dictionary.Remove(dscID);
    }
    /// <summary>
    /// Determines whether this Dscs collection contains a Dsc 
    /// object with the specified dscID.
    /// </summary>
    /// <param name="dscID"></param>
    /// <returns>true if the specified dscID was found, otherwise false.</returns>
    public bool Contains(int key)
    {
      return this.Dictionary.Contains(key);
    }
    /// <summary>
    /// Gets an ICollection containing a collection of dscID integers contained
    /// in the Dscs collection.
    /// </summary>
    public ICollection Keys
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        return this.Dictionary.Keys;
      }
    }
    /// <summary>
    /// Gets an ICollection containing a collection of Dsc objects
    /// contained in this Dscs collection. Use this method to enumerate
    /// through the Dscs collection using the "foreach" enumerator.
    /// </summary>
    public ICollection Values
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        return this.Dictionary.Values;
      }
    }

    protected override void OnValidate(object key, object value)
    {
      base.OnValidate(key, value);
      if (!(value is Dsc))
      {
        throw new ArgumentException("Dscs collection only supports Dsc objects.");
      }
    }
    #endregion

    /// <summary>
    /// Event that occurs when Dscs collection reports that it's status has changed.
    /// </summary>
    public event OnStatusChangedEventHandler StatusChanged;

    /// <summary>
    /// Internally called function that fires the OnStatusChanged event.
    /// </summary>
    /// <param name="e">Parameter that contains the string describing the new state.</param>
    protected virtual void OnStatusChanged(StatusChangedArgs e)
    {
      if (StatusChanged != null)
        StatusChanged(e);
    }
  }
}
