using System;
using SystemsAnalysis;
using SystemsAnalysis.Tracer;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.Modeling
{
  /// <summary>
  /// Summary description for Link.
  /// </summary>
  public class Link : IGraphEdge
  {
    private string usNodeName;
    private string dsNodeName;
    private int linkID;
    private int mLinkID = 0;
    private string material;
    private PipeConstructionType pipeConstruction;
    private double diameter;
    private double height;
    private double length;
    private double usIE;
    private double dsIE;
    private string pipeShape = "";
    private string simLinkID = "";
    private bool isSpecLink = false;
    private DateTime installDate;
    private Enumerators.LinkTypes linkType;
    private Enumerators.PipeFlowTypes pipeFlowType;
    private GraphEdge linkEdge;
    private double qDesign = 0;

    /// <summary>
    /// Creates a new link containing only a LinkID.
    /// </summary>
    /// <param name="LinkID">The LinkID to create</param>
    public Link(int LinkID)
    {
      this.linkID = LinkID;
      linkEdge = new GraphEdge(linkID, "", "");
    }
    /// <summary>
    /// Creates a new link with connectivity and a corresponding
    /// Master Link.
    /// </summary>
    /// <param name="LinkID">The LinkID to create</param>
    /// <param name="MLinkID">The MLinkID of the master link corresponding
    /// to this link</param>
    /// <param name="USNodeName">The name of the Upstream Node</param>
    /// <param name="DSNodeName">The name of the Downstream Node</param>
    public Link(int LinkID, int MLinkID, string USNodeName, string DSNodeName)
    {
      this.linkID = LinkID;
      this.mLinkID = MLinkID;
      this.usNodeName = USNodeName;
      this.dsNodeName = DSNodeName;
      linkEdge = new GraphEdge(linkID, USNodeName, DSNodeName);
    }
    /// <summary>
    /// Creates a new link with connectivity and a corresponding
    /// Master Link.
    /// </summary>
    /// <param name="LinkID">The LinkID to create</param>
    /// <param name="MLinkID">The MLinkID of the master link corresponding
    /// to this link</param>
    /// <param name="USNodeName">A Node object representing the Upstream Node</param>
    /// <param name="DSNodeName">A Node object representing the Downstream Node</param>
    public Link(int LinkID, int MLinkID, Node USNode, Node DSNode)
      :
  this(LinkID, MLinkID, USNode.Name, USNode.Name)
    {
    }
    /// <summary>
    /// Creates a new link from the MstLink database.
    /// </summary>
    /// <param name="mstLinksRow">A row from DataAccess.SAMasterDataSet</param>
    public Link(DataAccess.SAMasterDataSet.MstLinksRow mstLinksRow)
    {
      this.linkID = mstLinksRow.ObjectID;
      this.mLinkID = mstLinksRow.MLinkID;
      this.material = mstLinksRow.Material;
      this.usNodeName = mstLinksRow.UsNode;
      this.dsNodeName = mstLinksRow.DsNode;
      this.diameter = mstLinksRow.DiamWidth;
      this.height = mstLinksRow.Height;
      this.usIE = mstLinksRow.UsIE;
      this.dsIE = mstLinksRow.DsIE;
      this.linkType = Enumerators.GetLinkTypeEnum(mstLinksRow.LinkType);
      this.pipeFlowType = Enumerators.GetPipeFlowTypeEnum(mstLinksRow.PipeFlowType);
      this.installDate = mstLinksRow.IsInstDateNull() ? new DateTime() : mstLinksRow.InstDate;
      this.pipeShape = mstLinksRow.IsPipeShapeNull() ? "" : mstLinksRow.PipeShape;
      this.material = mstLinksRow.Material;
      this.qDesign = mstLinksRow.QDes;

      this.length = mstLinksRow.Length;
      linkEdge = new GraphEdge(linkID, USNodeName, DSNodeName);
    }
    /// <summary>
    /// Creates a new link from a MdlLink database.
    /// </summary>
    /// <param name="mdlLinksRow">A row from DataAccess.ModelDataSet</param>
    public Link(DataAccess.ModelDataSet.MdlLinksRow mdlLinksRow)
    {
      this.linkID = mdlLinksRow.LinkID;
      //HACK: In this system, MLinkIDs are required to be unique in a given
      //Links collection.  EMG2.1 models have duplicate MLinkIDs due to special
      //links structures.  We will ignore the MLinkID for special links.  This
      //can be removed when 2.1 models are obsolete.
      if (!mdlLinksRow.IsSpecLink)
      {
        this.mLinkID = mdlLinksRow.MLinkID;
      }
      this.material = mdlLinksRow.Material;
      this.usNodeName = mdlLinksRow.USNode;
      this.dsNodeName = mdlLinksRow.DSNode;
      this.diameter = mdlLinksRow.DiamWidth;
      this.height = mdlLinksRow.Height;
      this.usIE = mdlLinksRow.UsIE;
      this.dsIE = mdlLinksRow.DsIE;
      this.linkType = Enumerators.GetLinkTypeEnum(mdlLinksRow.LinkType);
      this.length = (int)mdlLinksRow.Length;
      this.pipeShape = mdlLinksRow.IsPipeShapeNull() ? "" : mdlLinksRow.PipeShape;
      this.simLinkID = mdlLinksRow.IsSimLinkIDNull() ? "" : mdlLinksRow.SimLinkID;
      this.isSpecLink = mdlLinksRow.IsSpecLink;
      this.installDate = mdlLinksRow.IsInstdateNull() ? new DateTime() : mdlLinksRow.Instdate;
      linkEdge = new GraphEdge(linkID, USNodeName, DSNodeName);
    }


    #region Explicit implementation of IGraphEdge
    string IGraphEdge.SourceNode
    {
      get { return this.usNodeName; }
    }

    string IGraphEdge.SinkNode
    {
      get { return this.dsNodeName; }
    }

    int IGraphEdge.EdgeID
    {
      get { return this.linkID; }
    }

    bool IGraphEdge.IsSelected
    {
      get { return this.linkEdge.IsSelected; }
      set { this.linkEdge.IsSelected = value; }
    }
    #endregion

    /// <summary>
    /// Compares one link to another. Links are considered equal
    /// if they have equal LinkIDs.
    /// </summary>
    /// <param name="obj">The object to compare to this Link</param>
    /// <returns>Returns TRUE is obj is a Link with the same LinkID
    /// as this Link. Returns FALSE if obj is a Link witha different
    /// LinkID. Throws an exception if obj is not a Link.</returns>
    public int CompareTo(object obj)
    {
      if (obj is Link)
      {
        Link compareLink = (Link)obj;

        return this.linkID.CompareTo(compareLink.linkID);
      }
      throw new ArgumentException("object is not a Link");
    }
    /// <summary>
    /// Gets the LinkID of this Link
    /// </summary>
    public int LinkID
    {
      get { return this.linkID; }
    }
    /// <summary>
    /// Gets the MLinkID of this Link, if present.
    /// </summary>
    public int MLinkID
    {
      get { return this.mLinkID; }
    }
    public string SimLinkID
    {
      get { return this.simLinkID; }
    }
    /// <summary>
    /// Gets the Upstream Node name of this Link
    /// </summary>
    public string USNodeName
    {
      get { return this.usNodeName; }
    }
    /// <summary>
    /// Gets the Downstream Node name of this Link
    /// </summary>
    public string DSNodeName
    {
      get { return this.dsNodeName; }
    }
    /// <summary>
    /// Gets the material of the link
    /// </summary>
    public string Material
    {
      get
      {
        return material;
      }
    }
    /// <summary>
    /// Gets the pipe construction method of the linkID
    /// </summary>
    public PipeConstructionType PipeConstruction
    {
      get
      {
        return pipeConstruction;
      }
      set
      {
        pipeConstruction = value;
      }
    }
    /// <summary>
    /// Gets the Diameter of this Link
    /// </summary>
    public double Diameter
    {
      get { return this.diameter; }
    }
    /// <summary>
    /// Returns the height of non-circular pipes. If the pipe is
    /// circular then returns 0
    /// </summary>
    public double Height
    {
      get
      {
        if (this.PipeShape != "CIRC")
        {
          return this.height;
        }
        else
        {
          return 0;
        }
      }
    }

    /// <summary>
    /// Gets the Upstream Invert Elevation of this Link
    /// </summary>
    public double USIE
    {
      get { return this.usIE; }
    }
    /// <summary>
    /// Gets the Downstream Invert Elevation of this Link
    /// </summary>
    public double DSIE
    {
      get { return this.dsIE; }
    }
    /// <summary>
    /// Gets the Shape of this Link
    /// </summary>
    public string PipeShape
    {
      get { return this.pipeShape; }
    }
    /// <summary>
    /// Gets an LinkTypes enumerator representing the LinkType of this Link
    /// </summary>
    public Enumerators.LinkTypes LinkType
    {
      get { return this.linkType; }
    }
    /// <summary>
    /// Gets an PipeFlowTypes enumerator representing the LinkType of this Link
    /// </summary>
    public Enumerators.PipeFlowTypes PipeFlowType
    {
      get { return this.pipeFlowType; }
    }
    /// <summary>
    /// Indicates if flow in this Link is driven by gravity (PipeFlowType != F)
    /// </summary>
    public bool IsGravityFlow
    {
      get { return this.pipeFlowType != Enumerators.PipeFlowTypes.F; }
    }
    /// <summary>
    /// Gets the Length of this Link in feet
    /// </summary>
    public double Length
    {
      get { return this.length; }
    }
    /// <summary>
    /// Returns true is this Link is "special"
    /// </summary>
    public bool IsSpecLink
    {
      get { return this.isSpecLink; }
    }
    /// <summary>
    /// Gets the installation date of this Link
    /// </summary>
    public DateTime InstallDate
    {
      get { return this.installDate; }
    }
    /// <summary>
    /// Returns the manning's peak design flow for this Link
    /// </summary>
    public double QDesign
    {
      get { return this.qDesign; }
    }

  }
}
