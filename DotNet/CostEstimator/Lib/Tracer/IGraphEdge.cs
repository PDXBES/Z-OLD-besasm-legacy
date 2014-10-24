using System;

namespace SystemsAnalysis.Tracer
{
  /// <summary>
  /// Summary description for IGraphEdge.
  /// </summary>
  public interface IGraphEdge : IComparable
  {
    string SourceNode { get; }

    string SinkNode { get; }

    int EdgeID { get; }

    bool IsSelected { get;
      set; }
  }
}
