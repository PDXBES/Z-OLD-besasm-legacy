using System;
using System.Collections;

namespace SystemsAnalysis.Tracer
{
    /// <summary>
    /// A GraphEdge represents a link between two nodes in the graph (network)
    /// </summary>
    public class GraphEdge : IGraphEdge, IComparable
    {
        private string sourceNode;
        private string sinkNode;
        private int edgeID;
        private bool isSelected;

      /// <summary>
      /// Constructs a GraphEdge
      /// </summary>
      /// <param name="edgeID">An integer identifying the edge</param>
      /// <param name="sourceNode">A string identifying the upstream/source node</param>
      /// <param name="sinkNode">A string identifying the downstream/sink node</param>
        public GraphEdge(int edgeID, string sourceNode, string sinkNode)
        {
            this.edgeID = edgeID;
            this.sourceNode = sourceNode;
            this.sinkNode = sinkNode;
            this.isSelected = false;
        }

        /// <summary>
        /// Returns the string representation of the edge
        /// </summary>
        /// <returns>string of "ID: source-sink"</returns>
        public override string ToString()
        {
            string s = ": " + this.sourceNode + "-" + this.SinkNode;
            s.PadLeft(24 - s.Length + this.EdgeID.ToString().Length);
            s = this.EdgeID.ToString() + s;
            return s;
        }

        /// <summary>
        /// Returns the name of the source node
        /// </summary>
        public string SourceNode
        {
            get
            {
                return this.sourceNode;
            }
        }

        /// <summary>
        /// Returns the name of the sink node
        /// </summary>
        public string SinkNode
        {
            get
            {
                return this.sinkNode;
            }
        }

        /// <summary>
        /// Returns the name of the edge
        /// </summary>
        public int EdgeID
        {
            get
            {
                return this.edgeID;
            }
        }

        /// <summary>
        /// Returns whether the edge has been selected
        /// </summary>
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

        /// <summary>
        /// Returns whether the supplied edge's ID is less than the edge ID, for sorting
        /// </summary>
        /// <param name="obj">A GraphEdge to compare to</param>
        /// <returns>an integer less than 0 if less, 0 if equal, or greater than 0 if higher or an exception if the parameter is not a GraphEdge</returns>
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
