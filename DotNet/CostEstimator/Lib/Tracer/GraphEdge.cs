using System;
using System.Collections;

namespace SystemsAnalysis.Tracer
{
  /// <summary>
  /// Summary description for GraphEdges.
  /// </summary>
  public class GraphEdge : IGraphEdge, IComparable
  {
    private string sourceNode;
    private string sinkNode;
    private int edgeID;
    private bool isSelected;

    public GraphEdge(int edgeID, string sourceNode, string sinkNode)
    {
      //
      // TODO: Add constructor logic here
      //
      this.edgeID = edgeID;
      this.sourceNode = sourceNode;
      this.sinkNode = sinkNode;
      this.isSelected = false;
    }

    public override string ToString()
    {
      string s = ": " + this.sourceNode + "-" + this.SinkNode;
      s.PadLeft(24 - s.Length + this.EdgeID.ToString().Length);
      s = this.EdgeID.ToString() + s;
      return s;
    }
    public string SourceNode
    {
      get
      {
        return this.sourceNode;
      }
    }

    public string SinkNode
    {
      get
      {
        return this.sinkNode;
      }
    }

    public int EdgeID
    {
      get
      {
        return this.edgeID;
      }
    }

    public bool IsSelected
    {
      get
      {
        return this.isSelected;
      }
      set
      {
        this.isSelected = value;
      }
    }

    public int CompareTo(object obj)
    {
      if (obj is GraphEdge)
      {
        GraphEdge compareEdge = (GraphEdge)obj;

        return this.edgeID.CompareTo(compareEdge.edgeID);
      }
      throw new ArgumentException("object is not a GraphEdge");
    }
  }
}
