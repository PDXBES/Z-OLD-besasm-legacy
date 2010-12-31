using System;

namespace SystemsAnalysis.Tracer
{
    /// <summary>
    /// Interface for all classes that implement graph edge behavior/properties
    /// </summary>
    public interface IGraphEdge : IComparable
    {
        /// <summary>
        /// Upstream node ID
        /// </summary>
        string SourceNode
        {
            get;
        }

        /// <summary>
        /// Downstream node ID
        /// </summary>
        string SinkNode
        {
            get;
        }

        /// <summary>
        /// ID of the edge
        /// </summary>
        int EdgeID
        {
            get;
        }

        /// <summary>
        /// Whether the edge has been selected
        /// </summary>
        bool IsSelected
        {
            get;
            set;
        }

    }
}
