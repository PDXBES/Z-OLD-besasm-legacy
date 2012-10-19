using System;

namespace SystemsAnalysis.Tracer
{
  /// <summary>
  /// Summary description for INetwork
  /// </summary>
  public interface INetwork
  {
    int SelectSubNetwork(IGraphEdges rootEdges, IGraphEdges stopEdges);

    void ClearSubNetwork();

    Network GetSubNetwork();

    int[] GetSelectedEdgeIDArray();
    int GetSelectedEdgeCount();
  }
}
