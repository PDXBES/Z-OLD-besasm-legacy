using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.Modeling.ModelResults
{
    /// <summary>
    /// A strongly-typed collection of DSCHydraulic objects.
    /// </summary>
    public class DscHydraulics : DictionaryBase
    {
        /// <summary>
        /// Creates an empty DSCHydraulics collection.
        /// </summary>
        public DscHydraulics()
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
        public DscHydraulics(Dscs dscs, int scenarioID)
        {
            DataAccess.ModelCatalogDataSetTableAdapters.DscHydraulicsTableAdapter dschTA;
            dschTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.DscHydraulicsTableAdapter();

            string sql = "SELECT * FROM SP_DscHydraulics WHERE ScenarioID=" + scenarioID;
            string connString = dschTA.Connection.ConnectionString;
            System.Data.SqlClient.SqlDataReader dscHydraulicsReader;
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connString);

            System.Data.SqlClient.SqlCommand selCmd = new System.Data.SqlClient.SqlCommand(sql, conn);
            selCmd.CommandType = System.Data.CommandType.Text;
            conn.Open();
            dscHydraulicsReader = selCmd.ExecuteReader();

            while (dscHydraulicsReader.Read())
            {
                int dscID = (int)dscHydraulicsReader["DSCID"];
                if (dscs.Contains(dscID))
                {
                    if (!this.Contains(dscID))
                    {
                        DscHydraulic dscHydraulic;
                        dscHydraulic = new DscHydraulic(dscHydraulicsReader);
                        this.Add(dscHydraulic);
                        dscHydraulic.Dsc = dscs[dscID];
                    }
                }
            }
            dscHydraulicsReader.Close();
            GC.Collect();
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
            foreach (DscHydraulic dh in this.Values)
            {
                if (!dh.Dsc.FalseBFRisk && Math.Round(dh.DeltaHGL, 4) >= min && Math.Round(dh.DeltaHGL, 4) <= max)
                {
                    count++;
                }

            }
            return count;
        }

        #region Overriden methods from DictionaryBase
        /// <summary>
        /// Gets the DSCHydraulic object with the specified DscID. 		
        /// </summary>
        public DscHydraulic this[int DscID]
        {
            get { return (DscHydraulic)this.Dictionary[DscID]; }
        }

        /// <summary>
        /// Adds a DSCHydraulic object to this collection.
        /// </summary>
        /// <param name="Dsc">The DSCHydraulic object to add to this collection.</param>
        public void Add(DscHydraulic Dsc)
        {
            this.Dictionary.Add(Dsc.DscID, Dsc);
        }
        /// <summary>
        /// Removes a DSCHydraulic object from this DSCHydraulics collection.
        /// </summary>
        /// <param name="DscID">The DscID of the DSCHydraulic object to remove 
        /// from this DSCHydraulics collection. </param>
        public void Remove(int DscID)
        {
            this.Dictionary.Remove(DscID);
        }
        /// <summary>
        /// Determines whether this DSCHydraulics collection contains a DSCHydraulic 
        /// object with the specified DscID.
        /// </summary>
        /// <param name="DscID"></param>
        /// <returns>true if the specified DscID was found, otherwise false.</returns>
        public bool Contains(int DscID)
        {
            return this.Dictionary.Contains(DscID);
        }
        /// <summary>
        /// Gets an ICollection containing a collection of altDscID integers contained
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
            if (!(value is DscHydraulic))
            {
                throw new ArgumentException("DSCHydraulics only supports DSCHydraulic objects.");
            }
        }
        #endregion
    }
}
