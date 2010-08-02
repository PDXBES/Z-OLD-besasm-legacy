using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Characterization
{
	/// <summary>
	///  A strongly-typed collection of NodeHydraulic objects.
	/// </summary>
	public class NodeHydraulics : DictionaryBase 
	{
		/// <summary>
		/// Creates an empty NodeHydraulics collection.
		/// </summary>
		public NodeHydraulics()
		{
		}
		/// <summary>
		/// Constructor for a NodeHydraulics collection. This collection will contain a 
		/// subset of the master NodeHydraulics table which exist as the up- or downstream 
		/// nodes in the provided Links collection.
		/// </summary>		
		/// <param name="nodes">A Links collection whose up- and downstream
		/// nodes should be added to the this Nodes collection.</param>
		/// <param name="scenarioID">The Scenario ID of the NodeHydraulics collection.</param>
		/// <param name="modelCatalogDB">The database to read model results from. Typically will
		/// be the Central Data Store for modeling results.</param>
		public NodeHydraulics(Nodes nodes, int scenarioID, string modelCatalogDB)
		{			
			ModelCatalogDataAccess modelCatalogDataAccess = new ModelCatalogDataAccess(modelCatalogDB);
			modelCatalogDataAccess.NodeHydraulicsDataAdapter.SelectCommand.Connection.Open();
			System.Data.OleDb.OleDbDataReader nodeHydraulicsReader;
			nodeHydraulicsReader = modelCatalogDataAccess.NodeHydraulicsDataAdapter.SelectCommand.ExecuteReader();
			while (nodeHydraulicsReader.Read())
			{
				if ( (int)nodeHydraulicsReader["scenarioID"] == scenarioID )
				{
					string nodeName = (string)nodeHydraulicsReader["nodeName"];
					if (nodes.Contains(nodeName))
					{
						if (!this.Contains(nodeName))
						{
							NodeHydraulic node = new NodeHydraulic(nodeHydraulicsReader);
							this.Add(node);
						}
					}
				}
			}
			nodeHydraulicsReader.Close();
			modelCatalogDataAccess.NodeHydraulicsDataAdapter.SelectCommand.Connection.Close();
		}

		#region Overriden methods from DictionaryBase
		/// <summary>
		/// Gets the NodeHydraulic object with the specified nodeName. 		
		/// </summary>
		public NodeHydraulic this[string nodeName]
		{
			get { return (NodeHydraulic)this.Dictionary[nodeName]; }			
		}
		/// <summary>
		/// Adds a NodeHydraulic object to this collection.
		/// </summary>
		/// <param name="node">The NodeHydraulic object to add to this collection.</param>
		public void Add(NodeHydraulic node)
		{
			this.Dictionary.Add(node.Name, node);
		}
		/// <summary>
		/// Removes a NodeHydraulic object from this NodeHydraulics collection.
		/// </summary>
		/// <param name="nodeName">The nodeName of the NodeHydraulic object to remove 
		/// from this NodeHydraulics collection. </param>
		public void Remove(string nodeName)
		{
			this.Dictionary.Remove(nodeName);
		}
		/// <summary>
		/// Determines whether this NodeHydraulics collection contains a NodeHydraulic 
		/// object with the specified nodeName.
		/// </summary>
		/// <param name="nodeName"></param>
		/// <returns>true if the specified nodeName was found, otherwise false.</returns>
		public bool Contains(string nodeName)
		{
			return this.Dictionary.Contains(nodeName);
		}
		/// <summary>
		/// Gets an ICollection containing a collection of NodeName strings contained
		/// in the NodeHydraulics collection.
		/// </summary>
		public ICollection Keys
		{
			get { return this.Dictionary.Keys; }
		}
		/// <summary>
		/// Gets an ICollection containing a collection of NodeHydraulic objects
		/// contained in this NodeHydraulics collection. Use this method to enumerate
		/// through the NodeHydraulics collection using the "foreach" enumerator.
		/// </summary>
		public ICollection Values
		{
			get { return this.Dictionary.Values; }
		}
		/// <summary>
		/// Event that occurs when a NodeHydraulic object is added to this collection. Will throw
		/// and exception if the object added is not of type NodeHydraulic.
		/// </summary>
		/// <param name="key">The node name of the NodeHydraulic object to be added to the collection.</param>
		/// <param name="value">The DSCHydraulic object to be added to this collection.</param>
		protected override void OnValidate(object key, object value)
		{
			base.OnValidate(key, value);
			if (!(value is NodeHydraulic))
			{
				throw new ArgumentException("NodeHydraulics only supports NodeHydraulic objects.");
			}
		}
		#endregion

	}
}
