using System;
using System.Collections;

namespace SystemsAnalysis.Tracer
{
  /// <summary>
  /// Summary description for IGraphEdges.
  /// </summary>
  public interface IGraphEdges
  {
    IGraphEdge this[int index] { get; }
    int Add(IGraphEdge value);
    void Remove(IGraphEdge value);
    bool Contains(IGraphEdge value);
    IEnumerator GetEnumerator();
    bool isSorted { get; set; }
    IGraphEdges FindSourceEdges(IGraphEdge value);
    int Count { get; }
  }
}
