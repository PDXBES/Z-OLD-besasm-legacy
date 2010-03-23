using System;
using System.Data;
using System.Diagnostics;
using System.Collections;

namespace SystemsAnalysis.Tracer
{
    /// <summary>
    /// Summary description for Network.
    /// </summary>
    public class Network : INetwork
    {
        private IGraphEdges edges;
        static int recursionCount = 0;
        /// <summary>
        /// Creates a new, empty Network.
        /// </summary>
        public Network()
        {
            this.edges = new GraphEdges();
        }
        /// <summary>
        /// Creates a new Network consisting of the provided IGraphEdges collection.
        /// </summary>
        /// <param name="edges">An IGraphEdges collection used to build this Network.</param>
        public Network(IGraphEdges edges)
        {
            this.edges = edges;
        }
        /// <summary>
        /// Creates a new Network from an ADO.NET DataTable.
        /// </summary>
        /// <param name="GraphDataTable">The DataTable used to create this Network.</param>
        /// <param name="edgeIDField">The name of the field containing primary 
        /// key for each Edge in the Network. For ArcObjects FeatureClasses it 
        /// is useful is this field is the OID.</param>
        /// <param name="sourceNodeField">The name of the field containing the Source Node 
        /// of the GraphEdges. Also known as the Upstream Node.</param>
        /// <param name="sinkNodeField">The name of the field containing the Sink Node 
        /// of the GraphEdges. Also known as the Downstream Node.</param>
        public Network(DataTable GraphDataTable, string edgeIDField,
        string sourceNodeField, string sinkNodeField)
        {
            edges = new GraphEdges();
            DataRowCollection GraphDataRows = GraphDataTable.Rows;
            //Turn off sorting for performance boost while propogating GraphEdges
            edges.isSorted = false;
            for (int i = 0; i < GraphDataRows.Count; i++)
            {
                DataRow GraphDataRow = GraphDataRows[i];
                int edgeID = (int)GraphDataRow[edgeIDField];
                string sourceNode = GraphDataRow[sourceNodeField] as string;
                string sinkNode = GraphDataRow[sinkNodeField] as string;
                GraphEdge edge;
                edge = new GraphEdge(edgeID, sourceNode, sinkNode);
                edges.Add(edge);
            }
            //Once Network is filled turn on sort to allow quick binary searches 
            edges.isSorted = true;
        }
        /// <summary>
        /// Performs a trace of the Network. GraphEdges that are included in the trace
        /// will be indicated by IGraphEdge.IsSelected = true.
        /// </summary>
        /// <param name="rootEdges">A collection of IGraphEdges that represent
        /// the starting edges of the Network trace.</param>
        /// <param name="stopEdges">A collection of IGraphEdges that represent
        /// the stopping edges of the Network trace.</param>
        /// <returns>The total number of IGraphEdges is the Network that were
        /// selected by the trace.</returns>
        public int SelectSubNetwork(IGraphEdges rootEdges, IGraphEdges stopEdges)
        {
            recursionCount++;
            int edgeCount = 0;
            //Debug.WriteLine("Subnetwork recursion level: " + recursionCount);			

            foreach (IGraphEdge rootEdge in rootEdges)
            {
                if (rootEdge.IsSelected)
                {
                    continue;
                }
                rootEdge.IsSelected = true;
                edgeCount++;
                if (stopEdges != null)
                {
                    if (stopEdges.Contains(rootEdge))
                    {
                        continue;
                    }
                }
                if (this.edges.Contains(rootEdge))
                {
                    //Debug.WriteLine("	Added MLinkID: " + rootEdge.EdgeID);
                    IGraphEdges sourceEdges;
                    sourceEdges = this.edges.FindSourceEdges((IGraphEdge)rootEdge);
                    if (sourceEdges.Count != 0)
                    {
                        //subNetwork.Concat(this.getSubNetwork(sourceEdges, stopEdges));
                        edgeCount += this.SelectSubNetwork(sourceEdges, stopEdges);
                    }
                }
            }
            recursionCount--;
            return edgeCount; // subNetwork;
        }

        /// <summary>
        /// Clears the selected GraphEdge objects from this collection. All GraphEdge
        /// objects in this Network will have IsSelected = false.
        /// </summary>
        public void ClearSubNetwork()
        {
            foreach (IGraphEdge edge in this.edges)
            {
                edge.IsSelected = false;
            }
        }

        /// <summary>
        /// Gets a new Network containing the selected GraphEdges in the current
        /// Network. Note that this does not create new instances of the GraphEdge 
        /// objects in the current Network. The GraphEdge objects in the new 
        /// Network are the same objects in the current Network.
        /// </summary>
        /// <returns>A new Network containing the selected GraphEdges in the current
        /// Network.</returns>
        public Network GetSubNetwork()
        {
            Network subNetwork = new Network();
            subNetwork.edges.isSorted = false;
            foreach (IGraphEdge edge in this.edges)
            {
                if (edge.IsSelected)
                {
                    subNetwork.AddGraphEdge(edge);
                }
            }
            subNetwork.edges.isSorted = true;
            return subNetwork;
        }

        private void AddGraphEdge(IGraphEdge edge)
        {
            this.edges.Add(edge);
            return;
        }

        /// <summary>
        /// Returns an IGraphEdges collection containing the IGraphEdges that
        /// make up this Network.
        /// </summary>
        public IGraphEdges Edges
        {
            get
            {
                return edges;
            }
        }

        /// <summary>
        /// Returns an IGraphEdge with the specified edgeID.
        /// </summary>
        /// <param name="edgeID">The ID of the IGraphEdge to select.</param>
        /// <returns>The IGraphEdge with the specified ID, or null if not found.</returns>
        public IGraphEdge getEdge(int edgeID)
        {
            return edges[edgeID];
        }

        /// <summary>
        /// Returns an integer array containing the IDs of the all
        /// Edge objects in this Network. Useful for selecting feature in ArcObjects via
        /// SelectionSet.AddList().
        /// </summary>
        /// <returns>An array of integers containing the IDs of the all the
        /// Edge objects in this Network.</returns>		
        public int[] GetEdgeIDArray()
        {
            int[] edgeIDArray = new int[edges.Count];
            int i = 0;
            foreach (GraphEdge edge in edges)
            {
                edgeIDArray[i] = edge.EdgeID;
                i++;
            }
            return edgeIDArray;
        }

        /// <summary>
        /// Gets the number of Edge objects in this collection..
        /// </summary>
        /// <returns>The number of Edge objects in this collection.</returns>
        public int GetEdgeCount()
        {
            return edges.Count;
        }
        /// <summary>
        /// Returns an integer array containing the IDs of the currently selected
        /// Edge objects.  Useful for selecting feature in ArcObjects via
        /// SelectionSet.AddList().
        /// </summary>
        /// <returns>An array of integers containing the IDs of the currently
        /// selected Edge objects.</returns>
        public int[] GetSelectedEdgeIDArray()
        {
            int[] edgeIDArray = new int[GetSelectedEdgeCount()];
            int i = 0;
            foreach (IGraphEdge edge in edges)
            {
                if (edge.IsSelected)
                {
                    edgeIDArray[i] = edge.EdgeID;
                    i++;
                }
            }
            return edgeIDArray;
        }
        /// <summary>
        /// Gets the number of currently selected Edge objects.
        /// </summary>
        /// <returns>The number of currently selected Edge objects.</returns>
        public int GetSelectedEdgeCount()
        {
            int i = 0;
            foreach (IGraphEdge edge in edges)
            {
                if (edge.IsSelected)
                {
                    i++;
                }
            }
            return i;
        }

        /// <summary>
        /// Gets an IEnumerator for enumerating the Edges in this Network.
        /// </summary>
        /// <returns>An IEnumerator for enumerating the Edges in this Network.</returns>
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)Edges.GetEnumerator();
        }
    }
}
