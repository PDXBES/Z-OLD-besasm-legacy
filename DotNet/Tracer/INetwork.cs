using System;

namespace SystemsAnalysis.Tracer
{
    /// <summary>
    /// Interface for all classes that implement Network behavior
    /// </summary>
    public interface INetwork
    {
        /// <summary>
        /// Selects a portion of the network given a set of root (sink) edges
        /// and a set of stop (source) edges into the Network's "Subnetwork."
        /// </summary>
        /// <param name="rootEdges">A collection of root (sink) edges</param>
        /// <param name="stopEdges">A collection of stop (source) edges</param>
        /// <returns>The number of edges selected</returns>
        int SelectSubNetwork(IGraphEdges rootEdges, IGraphEdges stopEdges);

        /// <summary>
        /// Clears the network's current SubNetwork
        /// </summary>
        void ClearSubNetwork();

        /// <summary>
        /// Retrieves the network's current SubNetwork
        /// </summary>
        /// <returns>A Network object</returns>
        Network GetSubNetwork();

        /// <summary>
        /// Retrieves an array of edges representing the current selection
        /// </summary>
        /// <returns>an array of Edge IDs (ints)</returns>
        int[] GetSelectedEdgeIDArray();

        /// <summary>
        /// Returns the number of edges in the current selection
        /// </summary>
        /// <returns>The number of edges in the current selection</returns>
        int GetSelectedEdgeCount();

    }
}
