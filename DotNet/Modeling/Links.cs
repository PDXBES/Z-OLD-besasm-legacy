using System;
using System.Collections;
using System.Data;
using SystemsAnalysis;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Tracer;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.Modeling
{
  public delegate void OnAddLinkEventHandler(object sender, Links.LinkChangedEventArgs e);
  public delegate void OnRemoveLinkEventHandler(object sender, Links.LinkChangedEventArgs e);

  /// <summary>
  /// A strongly-typed collection of Link objects.
  /// </summary>
  public class Links : DictionaryBase, INetwork
  {
    //This class implements INetwork by wrapping an instance of Network
    private Network _linkNetwork;

    /* This class maintains hash tables/sorted lists of important fields 
    * including MLinkID, USNode and DSNode.  This allows retrieval of Link objects
    * by these fields to occur rapidly.  The Links class will maintain the integrity
    * of these lists as Link objects are added and removed from the collection.
    */
    private Hashtable mLinkList;
    private ArrayList usNodeList;
    private ArrayList dsNodeList;

    /// <summary>
    /// Creates an empty Links collection
    /// </summary>
    public Links()
    {
    }

    protected override void OnClear()
    {
      foreach (Link l in this.Values)
      {
        OnRemovedLink(l, new LinkChangedEventArgs(l));
      }
      base.OnClear();
    }

    public Network LinkNetwork
    {
      get
      {
        if (this._linkNetwork == null)
        {
          this.InitLinkNetwork();
        }
        return this._linkNetwork;
      }
    }

    /// <summary>
    /// Creates a Links collection containing all entries in a MstLinkDataTable.
    /// </summary>
    /// <param name="mstLinksTable">A MstLinkDataTable loaded with data from SAMaster database using SystemsAnalysis.DataAccess</param>
    public Links(SAMasterDataSet.MstLinksDataTable mstLinksTable)
    {
      //if (TimeFrame == "EX")
      //{
      foreach (SAMasterDataSet.MstLinksRow mstLinkRow in mstLinksTable)
      {
        if (mstLinkRow.TimeFrame == "EX" || mstLinkRow.TimeFrame == "CE"
        || mstLinkRow.TimeFrame == "AE")
        {
          this.Add(new Link(mstLinkRow));
        }
      }
    }

    /// <summary>
    /// Creates a Links collection containing all entries in a MdlLinksDataTable.
    /// </summary>
    /// <param name="mdlLinksTable">A MdlLinkDataTable loaded with data from traced model database using SystemsAnalysis.DataAccess</param>
    public Links(string modelPath)
    {
      ModelDataSet.MdlLinksDataTable mdlLinksDT = new ModelDataSet.MdlLinksDataTable();
      DataAccess.ModelDataSetTableAdapters.MdlLinksTableAdapter mdlLinksAdapter;
      mdlLinksAdapter = new DataAccess.ModelDataSetTableAdapters.MdlLinksTableAdapter(modelPath);
      mdlLinksAdapter.Fill(mdlLinksDT);

      foreach (ModelDataSet.MdlLinksRow mdlLinkRow in mdlLinksDT)
      {
        this.Add(new Link(mdlLinkRow));
      }
    }
    public Links(ModelDataSet.MdlLinksDataTable mdlLinksTable)
    {
      throw new Exception("This constructor has been replaced by new Links(string modelPath)");
    }

    /// <summary>
    /// Returns a Link object contained within the Links collection, or null if not found.
    /// </summary>
    /// <param name="mLinkID">An MLinkID integer that may/may not exist in Links.</param>
    /// <returns>A Link with a corresponding MLinkID, or null if not found.</returns>
    public Link FindByMLinkID(int mLinkID)
    {
      if (this.mLinkList == null)
      {
        mLinkList = new Hashtable();
        foreach (Link l in this.Values)
        {
          if (l.MLinkID != 0 && !mLinkList.ContainsKey(l.MLinkID))
          {
            mLinkList.Add(l.MLinkID, l);
          }
        }
      }
      return (Link)mLinkList[mLinkID];
    }
    /// <summary>
    /// Indicates if the Links collections contains a Link with a given upstream
    /// node name.		
    /// </summary>
    /// <param name="nodeName">A node name string to find in the Links collection.</param>
    /// <returns>true if the provided Node is referenced as an upstream node in
    /// this collection, otherwise false.</returns>
    public bool ContainsUSNode(string nodeName)
    {
      if (this.usNodeList == null)
      {
        usNodeList = new ArrayList();
        foreach (Link l in this.Values)
        {
          usNodeList.Add(l.USNodeName);
        }
        usNodeList.Sort();
      }
      return usNodeList.BinarySearch(nodeName) >= 0;
    }
    /// <summary>
    /// Indicates if the Links collections contains a Link with a given downstream
    /// node name.		
    /// </summary>
    /// <param name="nodeName">A node name string to find in the Links collection.</param>
    /// <returns>true if the provided Node is referenced as an downstream node in
    /// this collection, otherwise false.</returns>
    public bool ContainsDSNode(string nodeName)
    {
      if (this.dsNodeList == null)
      {
        dsNodeList = new ArrayList();
        foreach (Link l in this.Values)
        {
          dsNodeList.Add(l.DSNodeName);
        }
        dsNodeList.Sort();
      }
      return dsNodeList.BinarySearch(nodeName) >= 0;
    }

    /// <summary>
    /// Gets the number of gravity flow Link objects in this Links collection with
    /// the specified SystemsAnalysis.Types.Enumerators.LinkTypes.
    /// </summary>
    /// <param name="LinkType">The LinkType to count.</param>
    /// <returns>The number of pipe segments in this collection with the
    /// given LinkType.</returns>
    public int PipeSegmentCount(Enumerators.LinkTypes linkType)
    {
      int count = 0;
      foreach (Link l in this.Values)
      {
        if (l.LinkType == linkType && l.IsGravityFlow)
        {
          count++;
        }
      }
      return count;
    }
    /// <summary>
    /// Gets the number of gravity flow Link objects in this Links collection
    /// </summary>        
    /// <returns>The number of gravity flow pipe segments in this collection</returns>
    public int PipeSegmentCount()
    {
      int count = 0;
      foreach (Link l in this.Values)
      {
        if (l.IsGravityFlow)
        {
          count++;
        }
      }
      return count;
    }
    /// <summary>
    /// Gets the total length in feet of all gravity flow Link objects in this Links collection
    /// with the specified SystemsAnalysis.Types.Enumerators.LinkTypes.
    /// </summary>
    /// <param name="LinkType">The LinkType to count.</param>
    /// <returns>The total length of pipe segments in this collection with the
    /// given LinkType in feet.</returns>
    public int PipeSegmentLength(Enumerators.LinkTypes linkType)
    {
      double length = 0;
      foreach (Link l in this.Values)
      {
        if (l.LinkType == linkType)
        {
          if (l.IsGravityFlow) length += l.Length;
        }
      }
      return (int)length;
    }
    /// <summary>
    /// Gets the total length in feet of all gravity flow Link objects in this Links collection.
    /// </summary>		
    /// <returns>The total length of pipe segments in this collection in feet.</returns>
    public int PipeSegmentLength()
    {
      double length = 0;
      foreach (Link l in this.Values)
      {
        if (l.IsGravityFlow) length += l.Length;
      }
      return (int)length;
    }
    /// <summary>
    /// Returns the smallest pipe diameter found in this collection.
    /// </summary>        
    /// <returns>The smallest diameter in this collection</returns>
    public int GetMinPipeDiam()
    {
      double minDiam = System.Int32.MaxValue;
      foreach (Link l in this.Values)
      {
        if (l.Diameter < minDiam && l.IsGravityFlow)
        {
          minDiam = l.Diameter;
        }
      }
      return (int)minDiam;
    }
    /// <summary>
    /// Returns the smallest pipe diameter with a given LinkType found
    /// in this collection.
    /// </summary>
    /// <param name="LinkType">The LinkType to filter by</param>
    /// <returns>The smallest diameter with the specified LinkType</returns>
    public int GetMinPipeDiam(Enumerators.LinkTypes linkType)
    {
      double minDiam = System.Int32.MaxValue;
      foreach (Link l in this.Values)
      {
        if (l.Diameter < minDiam && l.LinkType == linkType && l.IsGravityFlow)
        {
          minDiam = l.Diameter;
        }
      }
      return (int)minDiam;
    }
    /// <summary>
    /// Returns the largest pipe diameter in this collection.
    /// </summary>        
    /// <returns>The largest diameter in this collection</returns>
    public int GetMaxPipeDiam()
    {
      double maxDiam = 0;
      foreach (Link l in this.Values)
      {
        if (l.Diameter > maxDiam && l.IsGravityFlow)
        {
          maxDiam = l.Diameter;
        }
      }
      return (int)maxDiam;
    }
    /// <summary>
    /// Returns the largest pipe diameter with a given LinkType found
    /// in this collection.
    /// </summary>
    /// <param name="LinkType">The LinkType to filter by.</param>
    /// <returns>The largest diameter.</returns>
    public int GetMaxPipeDiam(Enumerators.LinkTypes linkType)
    {
      double maxDiam = 0;
      foreach (Link l in this.Values)
      {
        if (l.Diameter > maxDiam && l.LinkType == linkType && l.IsGravityFlow)
        {
          maxDiam = l.Diameter;
        }
      }
      return (int)maxDiam;
    }
    /// <summary>
    /// Returns the installation year of the oldest installed pipe in this collection. Note
    /// that unitialized install dates are excluded (InstDate = DateTime.MinValue)
    /// </summary>        
    /// <returns>The install year of the oldest installed pipe in the collection</returns>
    public int GetOldestPipeInstallYear()
    {
      int oldestYear = int.MaxValue;
      foreach (Link l in this.Values)
      {
        if (l.InstallDate == DateTime.MinValue) { continue; }
        if (l.InstallDate.Year != 1 && l.InstallDate.Year < oldestYear)
        {
          oldestYear = l.InstallDate.Year;
        }
      }
      return oldestYear;
    }
    /// <summary>
    /// Returns the installation year of the newest installed pipe in this collection. Note
    /// that unitialized install dates are excluded (InstDate = DateTime.MinValue)
    /// </summary>        
    /// <returns>The largest diameter.</returns>
    public int GetNewestPipeInstallYear()
    {
      int newestYear = int.MinValue;
      foreach (Link l in this.Values)
      {
        if (l.InstallDate == DateTime.MinValue) { continue; }
        if (l.InstallDate.Year != 1 && l.InstallDate.Year > newestYear)
        {
          newestYear = l.InstallDate.Year;
        }
      }
      return newestYear;
    }

    #region Overriden methods from DictionaryBase
    /// <summary>
    /// Gets the Link object with the specified linkID. 		
    /// </summary>
    public Link this[int linkID]
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get { return (Link)this.Dictionary[linkID]; }
      set { this.Dictionary[linkID] = value; }
    }
    /// <summary>
    /// Adds a Link object to this collection.
    /// </summary>
    /// <param name="link">The Link object to add to this collection.</param>
    public void Add(Link link)
    {
      //Dispose linkNetwork since it will no longer be valid.
      //We will build it again next time it is needed.
      this._linkNetwork = null;
      //Dispose sorted mLink list since it will no longer be valid.
      //We will build it again next time it is needed.
      this.mLinkList = null;
      this.dsNodeList = null;
      this.usNodeList = null;
      if (!this.Contains(link.LinkID))
      {
        this.Dictionary.Add(link.LinkID, link);
        this.AddedLink(new LinkChangedEventArgs(link));
      }
    }
    /// <summary>
    /// Removes a LinkHydraulic object from this LinkHydraulics collection.
    /// </summary>
    /// <param name="linkID">The MLinkID of the LinkHydraulic object to remove 
    /// from this LinkHydraulic collection. </param>
    public void Remove(int linkID)
    {
      //Dispose linkNetwork since it will no longer be valid.
      //We will build it again next time it is needed.
      _linkNetwork = null;
      //Dispose sorted mLink list since it will no longer be valid.
      //We will build it again next time it is needed.
      this.mLinkList = null;
      this.dsNodeList = null;
      this.usNodeList = null;
      if (this.Contains(linkID))
      {
        this.RemovedLink(new LinkChangedEventArgs(this[linkID]));
        this.Dictionary.Remove(linkID);
      }
    }
    /// <summary>
    /// Determines whether this LinkHydraulics collection contains a LinkHydraulic 
    /// object with the specified linkID.
    /// </summary>
    /// <param name="linkID"></param>
    /// <returns>true if the specified linkID was found, otherwise false.</returns>
    public bool Contains(int linkID)
    {
      return this.Dictionary.Contains(linkID);
    }
    /// <summary>
    /// Gets an ICollection containing a collection of LinkID integers contained
    /// in the Links collection.
    /// </summary>
    public ICollection Keys
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get { return this.Dictionary.Keys; }
    }
    /// <summary>
    /// Gets an ICollection containing a collection of Link objects
    /// contained in the Links collection. Use this method to enumerate
    /// through the Links collection using the "foreach" enumerator.
    /// </summary>
    public ICollection Values
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get { return this.Dictionary.Values; }
    }

    protected override void OnValidate(object key, object value)
    {
      base.OnValidate(key, value);
      if (!(value is Link))
      {
        throw new ArgumentException("Links only supports Link objects.");
      }
    }
    #endregion

    #region Implementation of INetwork
    /// <summary>
    /// Marks all Link objects in the current Links collection as unvisited.
    /// </summary>
    public void ClearSubNetwork()
    {
      //if (linkNetwork != null)
      //{
      this.LinkNetwork.ClearSubNetwork();
      //}            
    }

    /// <summary>
    /// Explicit implementation of INetwork.GetSubNetwork().
    /// Gets a Network representing those Links selected during the
    /// most recent call to "SelectSubNetwork(Links rootLinks, Links stopLinks)".
    /// </summary>
    /// <returns>A Network which may be null if no subnetwork was selected.</returns>
    Network INetwork.GetSubNetwork()
    {
      //if (linkNetwork == null) { return null; }
      return LinkNetwork.GetSubNetwork();
    }

    /// <summary>
    /// Gets a Links collection representing those Links selected during the
    /// most recent call to "SelectSubNetwork(Links rootLinks, Links stopLinks)".
    /// </summary>
    /// <returns>A Links collection which may be empty if no subnetwork was selected.</returns>
    public Links GetSubNetwork()
    {
      INetwork thisNetwork = (INetwork)this;
      Network subNetwork = thisNetwork.GetSubNetwork();
      Links subLinks = new Links();
      if (subNetwork == null) { return subLinks; }
      foreach (IGraphEdge ge in subNetwork)
      {
        subLinks.Add(this[ge.EdgeID]);
      }
      return subLinks;
    }
    /// <summary>
    /// Explicit implementation of INetwork.SelectSubNetwork
    /// </summary>
    /// <param name="rootEdges">An IGraphEdges collection containing the starting
    /// IGraphEdge objects to trace.</param>
    /// <param name="stopEdges">An IGraphEdges collection containing the terminal
    /// IGraphEdge objects to trace.</param>
    /// <returns>The number of IGraphEdge objects that were traced.</returns>
    int INetwork.SelectSubNetwork(IGraphEdges rootEdges, IGraphEdges stopEdges)
    {
      return this.LinkNetwork.SelectSubNetwork(rootEdges, stopEdges);
    }
    /// <summary>
    /// Performs a trace of this Links collection. The results of the trace
    /// can be retrieved via Links.GetSubNetwork().
    /// </summary>
    /// <param name="rootLinks">A Links collection containing the starting
    /// Link objects trace.</param>
    /// <param name="stopLinks">A Links collection containing the terminal
    /// Link objects to trace.</param>
    /// <returns>The number of Link objects that were traced.</returns>
    public int SelectSubNetwork(Links rootLinks, Links stopLinks)
    {
      GraphEdges rootEdges = new GraphEdges();
      rootEdges.isSorted = false;
      GraphEdges stopEdges = new GraphEdges();
      stopEdges.isSorted = false;
      foreach (Link l in rootLinks.Values)
      { rootEdges.Add((IGraphEdge)l); }
      foreach (Link l in stopLinks.Values)
      { stopEdges.Add((IGraphEdge)l); }
      rootEdges.isSorted = true;
      stopEdges.isSorted = true;

      return ((INetwork)this).SelectSubNetwork(rootEdges, stopEdges);
    }

    /// <summary>
    /// Returns the number of Link objects in the Links collection that were
    /// selected by the most recent trace.
    /// </summary>
    /// <returns>The number of Link objects in the Links collection that were
    /// selected by the most recent trace.</returns>
    public int GetSelectedEdgeCount()
    {
      //if (linkNetwork == null) { return 0; }
      return LinkNetwork.GetSelectedEdgeCount();
    }
    /// <summary>
    /// Gets an integer array of LinkIDs that were selected by the most
    /// recent call to SelectSubNetwork.  This is useful for selecting
    /// features from an ArcObjects IFeatureClass using 
    /// SelectionSet.AddList(...)
    /// </summary>
    /// <returns>An integer array of LinkIDs selected by the most 
    /// recent trace</returns>
    public int[] GetSelectedEdgeIDArray()
    {
      //if (linkNetwork == null) { return null; }
      return LinkNetwork.GetSelectedEdgeIDArray();
    }
    #endregion

    /// <summary>
    /// Performs a trace of the Links collection using the provided root/stop Links collections.
    /// </summary>
    /// <param name="rootLinks">A Links collection containing the Link objects at wchich to begin tracing</param>
    /// <param name="stopLinks">A Links collection containing the Link objects at which to stop tracing</param>
    /// <returns>A Links collections representing the traced, or null if no Links where traced</returns>
    public Links Trace(Links rootLinks, Links stopLinks)
    {
      this.ClearSubNetwork();
      int i;
      i = this.SelectSubNetwork(rootLinks, stopLinks);
      return this.GetSubNetwork();
    }

    private void InitLinkNetwork()
    {
      this._linkNetwork = new Network();
      this._linkNetwork.Edges.isSorted = false;
      foreach (Link l in this.Values)
      {
        this._linkNetwork.Edges.Add(l);
      }
      this._linkNetwork.Edges.isSorted = true;
      return;
    }

    #region Add/Remove link event functions
    public event OnAddLinkEventHandler OnAddedLink;
    public event OnRemoveLinkEventHandler OnRemovedLink;

    // Invoke the AddLinkEventHandler event; called whenever link added
    protected virtual void AddedLink(Links.LinkChangedEventArgs e)
    {
      if (OnAddedLink != null)
        OnAddedLink(this, e);
    }
    // Invoke the AddLinkEventHandler event; called whenever link added
    protected virtual void RemovedLink(Links.LinkChangedEventArgs e)
    {
      if (OnRemovedLink != null)
        OnRemovedLink(this, e);
    }

    public class LinkChangedEventArgs : EventArgs
    {
      Link link;
      public LinkChangedEventArgs(Link link)
      {
        this.link = link;
      }

      public Link ChangedLink
      {
        get { return this.link; }
      }
    }    //end of class FireEventArgs

    #endregion

  }
}
