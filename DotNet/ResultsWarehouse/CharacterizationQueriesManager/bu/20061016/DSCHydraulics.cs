using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.Characterization
{
	/// <summary>
	/// A strongly-typed collection of DSCHydraulic objects.
	/// </summary>
	public class DSCHydraulics : DictionaryBase
	{
		/// <summary>
		/// Creates an empty DSCHydraulics collection.
		/// </summary>
		public DSCHydraulics()
		{
		}

		/// <summary>
		/// Constructor for a DSCHydraulics collection. This collection will contain a 
		/// subset of the master DSCHydraulics table whose DSCID values exist in the 
		/// provided DSCs collection.
		/// </summary>
		/// <param name="dscs">A DSCs collection whose DSCID values
		/// should be added to this DSCHydraulics collection.</param>
		/// <param name="scenarioID">The Scenario ID of the DSCHydraulics collection.</param>
		/// <param name="modelCatalogDB">The database to read model results from. Typically will
		/// be the Central Data Store for modeling results.</param>
		public DSCHydraulics(DSCs dscs, int scenarioID, string modelCatalogDB)
		{			
			ModelCatalogDataAccess modelCatalogDataAccess = new ModelCatalogDataAccess(modelCatalogDB);
			modelCatalogDataAccess.DSCHydraulicsDataAdapter.SelectCommand.Connection.Open();
			System.Data.OleDb.OleDbDataReader dscHydraulicsReader;
			dscHydraulicsReader = modelCatalogDataAccess.DSCHydraulicsDataAdapter.SelectCommand.ExecuteReader();
			while (dscHydraulicsReader.Read())
			{
				if ( (int)dscHydraulicsReader["scenarioID"] == scenarioID )
				{
					int dscID = (int)dscHydraulicsReader["DSCID"];
					if (dscs.Contains(dscID))
					{
						if (!this.Contains(dscID))
						{
							this.Add(new DSCHydraulic(dscHydraulicsReader));
						}
					}
				}
			}
			dscHydraulicsReader.Close();
			modelCatalogDataAccess.DSCHydraulicsDataAdapter.SelectCommand.Connection.Close();			
		}

		/// <summary>
		/// Returns the number of DSCHydraulic objects in this collection that
		/// have basement backup risk within the specified range. Basement backup
		/// risk is determined using DSCHydraulic.DeltaHGL. Direct subcatchments that 
		/// have a 'False Positive' (determined by DSC.FalseBFRisk) flooding risk 
		/// are excluded from the total.
		/// </summary>
		/// <param name="min">The mininum basement backup risk to count (inclusive). To select
		/// an upper bound only use System.Double.MinValue for this parameter.</param>
		/// <param name="max">The maximum basement backup risk to count (inclusive). To select
		/// an upper bound only use System.Double.MinValue for this parameter.</param>
		/// <returns>The number of DSC objects in this collection within the specified range.</returns>
		public int CountBySewerBackupRisk(double min, double max)
		{
			int count = 0;
			foreach (DSCHydraulic dh in this.Values)
			{
				if (!dh.DSC.FalseBFRisk && Math.Round(dh.DeltaHGL, 4) >= min && Math.Round(dh.DeltaHGL, 4) <= max)
				{
					count++;
				}
				
			}
			return count;
		}

		#region Overriden methods from DictionaryBase
		/// <summary>
		/// Gets the DSCHydraulic object with the specified dscID. 		
		/// </summary>
		public DSCHydraulic this[int dscID]
		{
			get { return (DSCHydraulic)this.Dictionary[dscID]; }			
		}

		/// <summary>
		/// Adds a DSCHydraulic object to this collection.
		/// </summary>
		/// <param name="dsc">The DSCHydraulic object to add to this collection.</param>
		public void Add(DSCHydraulic dsc)
		{
			this.Dictionary.Add(dsc.DSCID, dsc);
		}
		/// <summary>
		/// Removes a DSCHydraulic object from this DSCHydraulics collection.
		/// </summary>
		/// <param name="dscID">The dscID of the DSCHydraulic object to remove 
		/// from this DSCHydraulics collection. </param>
		public void Remove(int dscID)
		{
			this.Dictionary.Remove(dscID);
		}
		/// <summary>
		/// Determines whether this DSCHydraulics collection contains a DSCHydraulic 
		/// object with the specified dscID.
		/// </summary>
		/// <param name="dscID"></param>
		/// <returns>true if the specified dscID was found, otherwise false.</returns>
		public bool Contains(int dscID)
		{
			return this.Dictionary.Contains(dscID);
		}
		/// <summary>
		/// Gets an ICollection containing a collection of dscID integers contained
		/// in the DSCHydraulics collection.
		/// </summary>
		public ICollection Keys
		{
			get { return this.Dictionary.Keys; }
		}
		/// <summary>
		/// Gets an ICollection containing a collection of DSCHydraulic objects
		/// contained in this DSCHydraulics collection. Use this method to enumerate
		/// through the DSCHydraulics collection using the "foreach" enumerator.
		/// </summary>
		public ICollection Values
		{
			get { return this.Dictionary.Values; }
		}
		/// <summary>
		/// Event that occurs when a DSCHydraulic object is added to this collection. Will throw
		/// and exception if the object added is not of type DSCHydraulic.
		/// </summary>
		/// <param name="key">The DSCID of the DSCHydraulic object to be added to the collection.</param>
		/// <param name="value">The DSCHydraulic object to be added to this collection.</param>
		protected override void OnValidate(object key, object value)
		{
			base.OnValidate(key, value);
			if (!(value is DSCHydraulic))
			{
				throw new ArgumentException("DSCHydraulics only supports DSCHydraulic objects.");
			}
		}
		#endregion
	}	
}
