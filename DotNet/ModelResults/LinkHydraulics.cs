using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Modeling.ModelResults
{
  /// <summary>
  /// A strongly-typed collection of LinkHydraulic objects.
  /// </summary>
  public class LinkHydraulics : DictionaryBase
  {
    /// <summary>
    /// Creates an empty LinkHydraulics collection.
    /// </summary>
    public LinkHydraulics()
    {
    }

    /// <summary>
    /// Constructor for a LinksHydraulics collection. This collection will contain a 
    /// subset of the master LinksHydraulics table whose MLinkIDs corresponds to those
    /// in the provided Links collection.
    /// </summary>
    /// <param name="links">A Links collection whose MLinkID values
    /// should be added to the this LinkHydraulics collection.</param>
    /// <param name="scenarioID">The Scenario ID of the LinkHydraulics collection.</param>
    public LinkHydraulics(Links links, int scenarioID)
    {
      DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter linkhTA;
      linkhTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.LinkHydraulicsTableAdapter();
      //We can load quickly using a stored procedure for lists less than 8000 items long
      /*if (links.Count < 8000)
      {
      string linkids;                
      int i = 0;
      foreach (Link l in links.Values)
      {
      linkids += l.MLinkID + ",";
      i++;
      }
      linkids.TrimEnd(new char[] {","});
      string sql = "SELECT SP_LinkHydraulics.* FROM SP_LinkHydraulics INNER JOIN dbo.CsvToInt("+linkids+") LinkIDList WHERE ScenarioID=" + scenarioID + " AND SP_LinkHydraulics.MLinkID=LinkIDList.IntValue";
      }*/

      string sql = "SELECT * FROM SP_LinkHydraulics WHERE ScenarioID=" + scenarioID;
      string connString = linkhTA.Connection.ConnectionString;
      System.Data.SqlClient.SqlDataReader linkHydraulicsReader;
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connString);

      System.Data.SqlClient.SqlCommand selCmd = new System.Data.SqlClient.SqlCommand(sql, conn);
      selCmd.CommandType = System.Data.CommandType.Text;
      conn.Open();
      linkHydraulicsReader = selCmd.ExecuteReader();

      while (linkHydraulicsReader.Read())
      {
        int mlinkID = (int)linkHydraulicsReader["MLinkID"];
        Link link = links.FindByMLinkID(mlinkID);
        if (link != null)
        {
          LinkHydraulic linkHydraulic;
          linkHydraulic = new LinkHydraulic(linkHydraulicsReader);
          this.Add(linkHydraulic);
          linkHydraulic.Link = link;
        }
      }
      linkHydraulicsReader.Close();
      GC.Collect();
    }


    /// <summary>
    /// Returns the number of LinkHydraulic objects in this collection that
    /// have a Max Model Q to Design Q ratio within the specified range and
    /// flow by gravity.
    /// </summary>
    /// <param name="min">The mininum Q/Q ratio to count (inclusive). To select
    /// an upper bound only use System.Double.MinValue for this parameter.</param>
    /// <param name="max">The maximum Q/Q ratio to count (inclusive). To select
    /// a lower bound only use System.Double.Max for this parameter.</param>
    /// <returns>The number of pipe segments within the specifed range.</returns>
    public int CountByQQRatio(double min, double max)
    {
      int count = 0;
      foreach (LinkHydraulic lh in this.Values)
      {
        if (Math.Round(lh.QQRatio, 4) >= min &&
        Math.Round(lh.QQRatio, 4) <= max &&
        lh.Link.IsGravityFlow)
        {
          count++;
        }
      }
      return count;
    }

    /// <summary>
    /// Returns the number of LinkHydraulic objects in this collection that
    /// have a Max Model Q to Design Q ratio within the specified range and a
    /// pipe diameter within the specified range. 
    /// </summary>
    /// <param name="min">The mininum Q/Q ratio to count (inclusive). To select
    /// an upper bound only use System.Double.MinValue for this parameter.</param>
    /// <param name="max">The maximum Q/Q ratio to count (inclusive). To select
    /// a lower bound only use System.Double.Max for this parameter.</param>
    /// <param name="minPipeSize">The minimum pipe diameter to count (inclusive). To select
    /// an upper bound only use System.Double.MinValue for this parameter.</param>
    /// <param name="maxPipeSize">The maximum pipe diameter to count (inclusive). To select
    /// an upper bound only use System.Double.MinValue for this parameter.</param>
    /// <returns>The number of pipe segments within the specifed Q/Q range and
    /// pipe diameter range.</returns>
    public int CountByQQRatio(double min, double max, double minPipeSize, double maxPipeSize)
    {
      int count = 0;
      foreach (LinkHydraulic lh in this.Values)
      {
        if (Math.Round(lh.QQRatio, 4) >= min && Math.Round(lh.QQRatio, 4) <= max &&
        Math.Round(lh.Link.Diameter, 4) >= minPipeSize &&
        Math.Round(lh.Link.Diameter, 4) <= maxPipeSize &&
        lh.Link.IsGravityFlow)
        {
          count++;
        }
      }
      return count;
    }
    /// <summary>
    /// Returns the length of LinkHydraulic objects in feet in this collection that
    /// have a Max Model Q to Design Q ratio within the specified range and a
    /// pipe diameter within the specified range and flow by gravity. 
    /// </summary>
    /// <param name="min">The mininum Q/Q ratio to count (inclusive). To select
    /// an upper bound only use System.Double.MinValue for this parameter.</param>
    /// <param name="max">The maximum Q/Q ratio to count (inclusive). To select
    /// a lower bound only use System.Double.Max for this parameter.</param>
    /// <param name="minPipeSize">The minimum pipe diameter to count (inclusive). To select
    /// an upper bound only use System.Double.MinValue for this parameter.</param>
    /// <param name="maxPipeSize">The maximum pipe diameter to count (inclusive). To select
    /// an upper bound only use System.Double.MinValue for this parameter.</param>
    /// <returns>The length of pipe segments in feet within the specifed Q/Q range and
    /// pipe diameter range.</returns>
    public int CountByQQRatioLength(double min, double max, double minPipeSize, double maxPipeSize)
    {
      double length = 0;
      foreach (LinkHydraulic lh in this.Values)
      {
        if (Math.Round(lh.QQRatio, 4) >= min && Math.Round(lh.QQRatio, 4) <= max &&
        Math.Round(lh.Link.Diameter, 4) >= minPipeSize &&
        Math.Round(lh.Link.Diameter, 4) <= maxPipeSize)
        {
          length += lh.Link.Length;
        }
      }
      return (int)length;
    }

    /// <summary>
    /// Returns the number of LinkHydraulic objects in this collection that
    /// have pipe surcharge within the specified range and flow by gravity.
    /// </summary>
    /// <param name="min">The mininum pipe surcharge value to count (inclusive). To select
    /// an upper bound only use System.Double.MinValue for this parameter.</param>
    /// <param name="max">The maximum pipe surcharge value to count (inclusive). To select
    /// an upper bound only use System.Double.MaxValue for this parameter.</param>
    /// <returns>The number of pipes in this collection within the specified range.</returns>
    public int PipeSurchargeCount(double min, double max)
    {
      int count = 0;
      foreach (LinkHydraulic lh in this.Values)
      {
        double maxSurcharge = lh.MaxSurcharge;
        if (maxSurcharge >= min && maxSurcharge <= max && lh.Link.IsGravityFlow)
        {
          count++;
        }
      }
      return count;
    }

    /// <summary>
    /// Returns the total length of the surcharged pipe segments in this collection
    /// have pipe surcharge within the specified range and flow by gravity.
    /// </summary>
    /// <param name="min">The mininum pipe surcharge value to include (inclusive). To select
    /// an upper bound only use System.Double.MinValue for this parameter.</param>
    /// <param name="max">The maximum pipe surcharge value to include (inclusive). To select
    /// an upper bound only use System.Double.MaxValue for this parameter.</param>
    /// <returns>The total length of the pipe segments in this collection with surcharge
    /// values within the specified range.</returns>
    public int PipeSurchargeLength(double min, double max)
    {
      double length = 0;
      foreach (LinkHydraulic lh in this.Values)
      {
        double diam = lh.Link.Diameter / 12;
        double USIE = lh.Link.USIE;
        double DSIE = lh.Link.DSIE;
        double USSurcharge = Math.Round(lh.MaxUSElev - (USIE + diam), 4);
        double DSSurcharge = Math.Round(lh.MaxDSElev - (DSIE + diam), 4);
        double maxSurcharge =
        USSurcharge > DSSurcharge ? USSurcharge : DSSurcharge;
        if (maxSurcharge >= min && maxSurcharge <= max && lh.Link.IsGravityFlow)
        {
          length += lh.Link.Length;
        }
      }
      return (int)length;
    }
    #region Overriden methods from DictionaryBase
    /// <summary>
    /// Gets the LinkHydraulic object with the specified mLinkID. 		
    /// </summary>
    public LinkHydraulic this[int mLinkID]
    {
      get { return (LinkHydraulic)this.Dictionary[mLinkID]; }
    }
    /// <summary>
    /// Adds a LinkHydraulic object to this collection.
    /// </summary>
    /// <param name="link">The LinkHydraulic object to add to this collection.</param>
    public void Add(LinkHydraulic link)
    {
      this.Dictionary.Add(link.MLinkID, link);
    }
    /// <summary>
    /// Removes a LinkHydraulic object from this LinkHydraulics collection.
    /// </summary>
    /// <param name="mLinkID">The MLinkID of the LinkHydraulic object to remove 
    /// from this LinkHydraulic collection. </param>
    public void Remove(int mLinkID)
    {
      this.Dictionary.Remove(mLinkID);
    }
    /// <summary>
    /// Determines whether this LinkHydraulics collection contains a LinkHydraulic 
    /// object with the specified mLinkID.
    /// </summary>
    /// <param name="mLinkID"></param>
    /// <returns>true if the specified mLinkID was found, otherwise false.</returns>
    public bool Contains(int mLinkID)
    {
      return this.Dictionary.Contains(mLinkID);
    }
    /// <summary>
    /// Gets an ICollection containing a collection of MLinkID integers contained
    /// in the LinkHydraulics collection.
    /// </summary>
    public ICollection Keys
    {
      get { return this.Dictionary.Keys; }
    }
    /// <summary>
    /// Gets an ICollection containing a collection of LinkHydraulic objects
    /// contained in the LinkHydraulics collection. Use this method to enumerate
    /// through the LinkHydraulics collection using the "foreach" enumerator.
    /// </summary>
    public ICollection Values
    {
      get { return this.Dictionary.Values; }
    }
    /// <summary>
    /// Event that occurs when a LinkHydraulic object is added to this collection. Will throw
    /// and exception if the object added is not of type LinkHydraulic.
    /// </summary>
    /// <param name="key">The LinkID of the LinkHydraulic object to be added to the collection.</param>
    /// <param name="value">The LinkHydraulic object to be added to this collection.</param>
    protected override void OnValidate(object key, object value)
    {
      base.OnValidate(key, value);
      if (!(value is LinkHydraulic))
      {
        throw new ArgumentException("LinkHydraulics only supports LinkHydraulic objects.");
      }
    }
    #endregion
  }
}
