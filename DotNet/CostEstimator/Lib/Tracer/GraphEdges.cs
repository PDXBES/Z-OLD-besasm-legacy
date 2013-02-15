using System;
using System.Data;
using System.Collections;

namespace SystemsAnalysis.Tracer
{
  /// <summary>
  /// Summary description for Models.
  /// </summary>
  public class GraphEdges : System.Collections.CollectionBase, IGraphEdges
  {
    private bool issorted;
    private ArrayList sinkNodeIndex;

    public GraphEdges()
    {
      issorted = true;
      sinkNodeIndex = new ArrayList();
    }

    public GraphEdges(DataTable GraphDataTable, string edgeIDField, string sourceNodeField, string sinkNodeField)
    {
      // For each row in DataSet, call this.add(new GraphEdge(edgeIDField.Value, etc)
      DataRowCollection rows = GraphDataTable.Rows;
      foreach (DataRow row in rows)
      {
        int edgeID = (int)row[edgeIDField];
        string sourceNode = (string)row[sourceNodeField];
        string sinkNode = (string)row[sinkNodeField];
        IGraphEdge edge = new GraphEdge(edgeID, sourceNode, sinkNode);
        this.Add(edge);
        sinkNodeIndex.Add(edge);
      }
      InnerList.Sort();
      sinkNodeIndex = new ArrayList();
      sinkNodeIndex.Sort(new sinkNodeComparer());
      issorted = true;
    }

    public IGraphEdge this[int index]
    {
      get
      {
        foreach (IGraphEdge edge in List)
        {
          if (edge.EdgeID == index)
          {
            return edge;
          }
        }
        return null;
      }
    }

    public int Add(IGraphEdge value)
    {
      int i = List.Add(value);
      sinkNodeIndex.Add(value);
      if (this.issorted)
      {
        //value.IsSelected = false;
        InnerList.Sort();
        sinkNodeIndex.Sort(new sinkNodeComparer());
      }
      return (i);
    }

    public int IndexOf(IGraphEdge value)
    {
      return (List.IndexOf(value));
    }

    public void Insert(int index, IGraphEdge value)
    {
      List.Insert(index, value);
      sinkNodeIndex.Insert(index, value);
      if (this.issorted)
      {
        InnerList.Sort();
        sinkNodeIndex.Sort(new sinkNodeComparer());
      }
    }

    public void Remove(IGraphEdge value)
    {
      List.Remove(value);
      sinkNodeIndex.Remove(value);
      InnerList.Sort();
      sinkNodeIndex.Sort();
    }


    public bool Contains(IGraphEdge value)
    {
      if (issorted)
      {
        return InnerList.BinarySearch(value) >= 0 ? true : false;
      }
      else
      {
        // If value is not of type GraphEdge, this will return false.
        foreach (IGraphEdge edge in List)
        {
          if (edge.EdgeID == value.EdgeID)
          {
            return true;
          }
        }
        return false;
      }
    }

    //TODO: Bad implementation. IsSorted should be maintaned by
    //the object, not by the user. GraphEdges should sort itself
    //when accessing data, but allow itself to become unsorted when
    //adding or removing data.
    public bool isSorted
    {
      get
      {
        return this.issorted;
      }
      set
      {
        this.issorted = value;
        if (this.issorted)
        {
          InnerList.Sort();
          sinkNodeIndex.Sort(new sinkNodeComparer());
        }
      }
    }


    public IGraphEdges FindSourceEdges(IGraphEdge sinkEdge)
    {
      GraphEdges sourceEdges = new GraphEdges();
      if (this.isSorted)
      {
        int foundIndex = sinkNodeIndex.BinarySearch(
        new GraphEdge(sinkEdge.EdgeID, sinkEdge.SourceNode, sinkEdge.SourceNode),
        new sinkNodeComparer());
        if (foundIndex >= 0)
        {
          int firstIndex = foundIndex;
          bool foundFirstIndex = false;
          IGraphEdge firstSourceEdge;
          while (!foundFirstIndex && firstIndex > 0)
          {
            firstSourceEdge = (IGraphEdge)sinkNodeIndex[firstIndex - 1];
            if (firstSourceEdge.SinkNode == sinkEdge.SourceNode)
            {
              firstIndex--;
            }
            else
            {
              foundFirstIndex = true;
            }
          }
          int lastIndex = foundIndex;
          bool foundLastIndex = false;
          IGraphEdge lastSourceEdge;
          while (!foundLastIndex && lastIndex < sinkNodeIndex.Count - 1)
          {
            lastSourceEdge = (IGraphEdge)sinkNodeIndex[lastIndex + 1];
            if (lastSourceEdge.SinkNode == sinkEdge.SourceNode)
            {
              lastIndex++;
            }
            else
            {
              foundLastIndex = true;
            }
          }
          for (int i = firstIndex; i <= lastIndex; i++)
          {
            sourceEdges.Add((IGraphEdge)sinkNodeIndex[i]);
          }
        }
        return sourceEdges;
      }
      foreach (IGraphEdge edge in this.List)
      {
        if (edge.SinkNode == sinkEdge.SourceNode)
        {
          sourceEdges.Add(edge);
        }
      }
      return sourceEdges;
    }


    protected override void OnInsert(int index, Object value)
    {
      if (!Type.GetType("SystemsAnalysis.Tracer.IGraphEdge")
      .IsAssignableFrom(value.GetType()))
      {
        throw new ArgumentException("value must be of type IGraphEdge.");
      }
    }

    protected override void OnRemove(int index, Object value)
    {
      if (!Type.GetType("SystemsAnalysis.Tracer.IGraphEdge")
      .IsAssignableFrom(value.GetType()))
      {
        throw new ArgumentException("value must be of type IGraphEdge.");
      }
    }
    protected override void OnSet(int index, Object oldValue, Object newValue)
    {
      if (!Type.GetType("SystemsAnalysis.Tracer.IGraphEdge")
      .IsAssignableFrom(newValue.GetType()))
      {
        throw new ArgumentException("value must be of type IGraphEdge.");
      }
    }

    protected override void OnValidate(Object value)
    {
      if (!Type.GetType("SystemsAnalysis.Tracer.IGraphEdge")
      .IsAssignableFrom(value.GetType()))
      {
        throw new ArgumentException("value must be of type IGraphEdge.");
      }
    }


    private class sinkNodeComparer : IComparer
    {
      #region IComparer Members
      public int Compare(object x, object y)
      {
        IGraphEdge edge1 = (IGraphEdge)x;
        IGraphEdge edge2 = (IGraphEdge)y;
        return edge1.SinkNode.CompareTo(edge2.SinkNode);
      }

      #endregion
    }
  }
}
