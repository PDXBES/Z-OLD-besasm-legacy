using System;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Characterization
{
	/// <summary>
	/// Summary description for NodeHydraulics.
	/// </summary>
	public class NodeHydraulic
	{
		private int nodeID;
		private string name;
		private double maxElevation;
		private double surcharge;
		private double freeboard;
		private double surchargeTime;
		private double floodedTime;

		/// <summary>
		/// Constructs a NodeHydraulic object using a ADO.NET data reader. Should only be
		/// called from the NodeHydraulics collection.
		/// </summary>
		/// <param name="mstNodeHydraulicsReader">An ADO.NET data reader containing a DataRow
		/// that matches the node hydraulics schema.</param>
		internal NodeHydraulic(System.Data.OleDb.OleDbDataReader mstNodeHydraulicsReader)
		{
			this.nodeID = (int)mstNodeHydraulicsReader["nodeHydraulicsID"];
			this.name = (string)mstNodeHydraulicsReader["nodeName"];
			this.maxElevation = (double)mstNodeHydraulicsReader["maxElevation"];
			this.surcharge = (double)mstNodeHydraulicsReader["surcharge"];
			this.freeboard = (double)mstNodeHydraulicsReader["freeboard"];
			this.surchargeTime =(double)mstNodeHydraulicsReader["surchargeTime"];
			this.floodedTime = (double)mstNodeHydraulicsReader["floodedTime"];
		}

		/// <summary>
		/// The six character "licencse plate" ID of this node, AKA the Hansen ID.
		/// </summary>
		public string Name
		{
			get { return this.name; }
		}
		/// <summary>
		/// The maximum water surface elevation at this node.
		/// </summary>
		public double MaxElevation
		{
			get { return this.maxElevation; }
		}
		/// <summary>
		/// The amount of surcharge at this node, measured relative to the crown
		/// of the highest pipe connected to this node.
		/// </summary>
		public double Surcharge
		{
			get { return this.surcharge; }
		}
		/// <summary>
		/// The distance between the maximum water surface elevation and the ground
		/// elvation in feet. If this is 0 or less, then street flooding is occcuring.
		/// </summary>
		public double Freeboard
		{
			get { return this.freeboard; }
		}
		/// <summary>
		/// The amount of time in minutes that pipes connected to this node are 
		/// experiencing surcharging.
		/// </summary>
		public double SurchargeTime
		{
			get { return this.surchargeTime; }
		}
		/// <summary>
		/// The amount of time in minutes that this node is flooding to the street.
		/// </summary>
		public double FloodedTime
		{
			get { return this.floodedTime; }
		}											
	}
}
