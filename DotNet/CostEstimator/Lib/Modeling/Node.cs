using System;

namespace SystemsAnalysis.Modeling
{
  /// <summary>
  /// Summary description for Node.
  /// </summary>
  public class Node
  {
    private string nodeName;
    private double grndElev;
    private string nodeType;
    private string gageID;

    public Node(string NodeName)
    {
      this.nodeName = NodeName;
    }

    internal Node(System.Data.IDataReader mstNodeReader)
    {
      this.nodeName = (string)mstNodeReader["Node"];
      this.nodeType = (string)mstNodeReader["NodeType"];
      this.grndElev = Convert.ToDouble(mstNodeReader["GrndElev"]);
      this.gageID = (string)mstNodeReader["GageID"];
    }

    public Node(DataAccess.ModelDataSet.MdlNodesRow mdlNodesRow)
      :
      this(mdlNodesRow.Node)
    {
      this.nodeName = mdlNodesRow.Node;
      if (!mdlNodesRow.IsNodeTypeNull())
        this.nodeType = mdlNodesRow.NodeType;
      this.grndElev = mdlNodesRow.GrndElev;
      this.gageID = mdlNodesRow.IsGageIDNull() ? "" : mdlNodesRow.GageID;
    }

    public string Name
    {
      get
      {
        return this.nodeName;
      }
    }

    public double GroundElevation
    {
      get
      {
        return this.grndElev;
      }
    }

    public string NodeType
    {
      get
      {
        return this.nodeType;
      }
    }

    public string GageID
    {
      get
      {
        return this.gageID;
      }
    }
  }
}
