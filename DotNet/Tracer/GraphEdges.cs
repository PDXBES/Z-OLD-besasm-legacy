using System;
using System.Data;
using System.Collections;

namespace SystemsAnalysis.Tracer
{
    /// <summary>
    /// A Collection of GraphEdge objects.
    /// </summary>
    public class GraphEdges : System.Collections.CollectionBase, IGraphEdges
    {
        private bool issorted;
        private ArrayList sinkNodeIndex;

        /// <summary>
        /// Constructs an empty collection of GraphEdge objects
        /// </summary>
        public GraphEdges()
        {
            issorted = true;
            sinkNodeIndex = new ArrayList();
        }

        /// <summary>
        /// Constructs a collection of GraphEdge objects using a data table and specifications for its fields
        /// </summary>
        /// <param name="GraphDataTable">A DataTable with graph data</param>
        /// <param name="edgeIDField">The field name that contains the edge IDs</param>
        /// <param name="sourceNodeField">The field name that contains the source node IDs</param>
        /// <param name="sinkNodeField">The field name that contains the sink node IDs</param>
        public GraphEdges(DataTable GraphDataTable, string edgeIDField, string sourceNodeField, string sinkNodeField)
        {
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

        /// <summary>
        /// Returns the index-th edge in the collection
        /// </summary>
        /// <param name="index">The index of the edge of interest</param>
        /// <returns>An IGraphEdge object</returns>
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

        /// <summary>
        /// Adds a GraphEdge to the end of the collection
        /// </summary>
        /// <param name="value">The GraphEdge to add</param>
        /// <returns>The index of the GraphEdge within the collection</returns>
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

        /// <summary>
        /// Returns the index of a Graphedge within the collection
        /// </summary>
        /// <param name="value">The GraphEdge of interest</param>
        /// <returns>The index of the GraphEdge within the collection</returns>
        public int IndexOf(IGraphEdge value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>
        /// Inserts a GraphEdge at a specific index of the collection
        /// </summary>
        /// <param name="index">The index at which to insert the GraphEdge</param>
        /// <param name="value">The GraphEdge to insert</param>
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

        /// <summary>
        /// Removes a GraphEdge from the collection
        /// </summary>
        /// <param name="value">The GraphEdge to remove</param>
        public void Remove(IGraphEdge value)
        {
            List.Remove(value);
            sinkNodeIndex.Remove(value);
            InnerList.Sort();
            sinkNodeIndex.Sort();
        }

        /// <summary>
        /// Tests whether a GraphEdge exists in the collection
        /// </summary>
        /// <param name="value">The GraphEdge of interest</param>
        /// <returns>True if the GraphEdge is in the collection</returns>
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
        /// <summary>
        /// Sorts the collection
        /// </summary>
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

        /// <summary>
        /// Traces upstream from a downstream edge to the top of the graph
        /// </summary>
        /// <param name="sinkEdge">The downstream edge of interest</param>
        /// <returns>A collection of edges</returns>
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

        /// <summary>
        /// Tests if the object being inserted into the collection implements IGraphEdge
        /// </summary>
        /// <param name="index">The index at which the IGraphEdge is being inserted</param>
        /// <param name="value">The IGraphEdge being inserted</param>
        protected override void OnInsert(int index, Object value)
        {
            if (!Type.GetType("SystemsAnalysis.Tracer.IGraphEdge")
            .IsAssignableFrom(value.GetType()))
            {
                throw new ArgumentException("value must be of type IGraphEdge.");
            }
        }
        
        /// <summary>
        /// Tests if the object desired to be removed from the collection implements IGraphEdge
        /// </summary>
        /// <param name="index">The index from which the IGraphEdge is being removed</param>
        /// <param name="value">The IGraphEdge being removed</param>
        protected override void OnRemove(int index, Object value)
        {
            if (!Type.GetType("SystemsAnalysis.Tracer.IGraphEdge")
            .IsAssignableFrom(value.GetType()))
            {
                throw new ArgumentException("value must be of type IGraphEdge.");
            }
        }

        /// <summary>
        /// Tests if the object replacing another IGraphEdge object in the collection implements IGraphEdge
        /// </summary>
        /// <param name="index">The index of the IGraphEdge being replaced</param>
        /// <param name="oldValue">The IGraphEdge being replaced</param>
        /// <param name="newValue">The new IGraphEdge</param>
        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            if (!Type.GetType("SystemsAnalysis.Tracer.IGraphEdge")
            .IsAssignableFrom(newValue.GetType()))
            {
                throw new ArgumentException("value must be of type IGraphEdge.");
            }
        }

        /// <summary>
        /// Tests if the object being validated is an IGraphEdge
        /// </summary>
        /// <param name="value">The IGraphEdge being replaced</param>
        protected override void OnValidate(Object value)
        {
            if (!Type.GetType("SystemsAnalysis.Tracer.IGraphEdge")
            .IsAssignableFrom(value.GetType()))
            {
                throw new ArgumentException("value must be of type IGraphEdge.");
            }
        }

        /// <summary>
        /// Compares two IGraphEdge objects
        /// </summary>
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
