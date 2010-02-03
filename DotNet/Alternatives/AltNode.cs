using System;

namespace SystemsAnalysis.Modeling.Alternatives
{
	/// <summary>
	/// Summary description for Node.
	/// </summary>
	public class AltNode
	{
    private int altNodeID;
		private string nodeName;
    private string operation;
    private double grndElev;
				
		public AltNode(DataAccess.AlternativeDataSet.AltNodesRow altNodesRow)		
		{
      this.altNodeID = altNodesRow.AltNodeID;
      this.nodeName = altNodesRow.Node;
      this.operation = altNodesRow.Operation;
      this.grndElev = altNodesRow.GrndElev;
		}

    public int AltNodeID
    {
        get { return this.altNodeID; }
    }

		public string Name
		{
			get { return this.nodeName; }		
		}

    public string Operation
    {
        get { return this.operation; }
    }

    public double GroundElevation
    {
      get
      {
        return this.grndElev;
      }
    }
	}
}
