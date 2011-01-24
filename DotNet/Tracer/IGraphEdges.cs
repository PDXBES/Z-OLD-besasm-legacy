using System;
using System.Collections;

namespace SystemsAnalysis.Tracer
{
    /// <summary>
    /// Interface for all classes that implement Graph Edge Collection behavior
    /// </summary>
    public interface IGraphEdges
    {
        /// <summary>
        /// Index property for accessing an IGraphEdge directly
        /// </summary>
        /// <param name="index">The index of interest</param>
        /// <returns>The IGraphEdge at the specified index</returns>
        IGraphEdge this[int index]
        {
            get;
        }

        /// <summary>
        /// Adds an IGraphEdge to the collection
        /// </summary>
        /// <param name="value">An IGraphEdge</param>
        /// <returns>The index in the collection of the added IGraphEdge</returns>
        int Add(IGraphEdge value);

        /// <summary>
        /// Removes an IGraphEdge from the collection
        /// </summary>
        /// <param name="value">The IGraphEdge to be removed</param>
        void Remove(IGraphEdge value);

        /// <summary>
        /// Determines whether an IGraphEdge exists within the collection
        /// </summary>
        /// <param name="value">The IGraphEdge </param>
        /// <returns></returns>
        bool Contains(IGraphEdge value);

        /// <summary>
        /// Gets an enumerator for iterating through the collection
        /// </summary>
        /// <returns></returns>
        IEnumerator GetEnumerator();

        /// <summary>
        /// Indicates whether the collection is sorted (may have to be manually called to induce sort)
        /// </summary>
        bool isSorted { get; set; }

        /// <summary>
        /// Finds all the upstream-most edges connected to the IGraphEdge provided
        /// </summary>
        /// <param name="value">The IGraphEdge of interest</param>
        /// <returns>A collection of IGraphEdges</returns>
        IGraphEdges FindSourceEdges(IGraphEdge value);
        int Count { get; }

    }
}
