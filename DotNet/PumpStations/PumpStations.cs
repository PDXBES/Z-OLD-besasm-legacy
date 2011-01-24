using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Modeling
{
    /// <summary>
    /// Strongly typed collection of PumpStations objects.
    /// </summary>
    public class PumpStations : DictionaryBase
    {
        /// <summary>
        /// Creates a collection of PumpStation objects containing all
        /// pump stations in the pump station database.
        /// </summary>
        public PumpStations()
        {
            DataAccess.PumpStationDataSet psDS;
            psDS = new DataAccess.PumpStationDataSet();
            DataAccess.PumpStationDataSetTableAdapters.PumpStationsTableAdapter psTA;
            psTA = new SystemsAnalysis.DataAccess.PumpStationDataSetTableAdapters.PumpStationsTableAdapter();
            DataAccess.PumpStationDataSetTableAdapters.ForceMainsTableAdapter fmTA;
            fmTA = new SystemsAnalysis.DataAccess.PumpStationDataSetTableAdapters.ForceMainsTableAdapter();
            DataAccess.PumpStationDataSetTableAdapters.WetWellsTableAdapter wwTA;
            wwTA = new SystemsAnalysis.DataAccess.PumpStationDataSetTableAdapters.WetWellsTableAdapter();
            DataAccess.PumpStationDataSetTableAdapters.PumpsTableAdapter pumpTA;
            pumpTA = new SystemsAnalysis.DataAccess.PumpStationDataSetTableAdapters.PumpsTableAdapter();
            DataAccess.PumpStationDataSetTableAdapters.PumpScheduleTableAdapter pscheduleTA;
            pscheduleTA = new SystemsAnalysis.DataAccess.PumpStationDataSetTableAdapters.PumpScheduleTableAdapter();

            psTA.Fill(psDS.PumpStations);
            fmTA.Fill(psDS.ForceMains);
            wwTA.Fill(psDS.WetWells);
            pumpTA.Fill(psDS.Pumps);
            pscheduleTA.Fill(psDS.PumpSchedule);

            foreach (PumpStationDataSet.PumpStationsRow row in psDS.PumpStations)
            {
                this.Add(new PumpStation(row));
            }
        }
        /// <summary>
        /// Creates a collection of PumpStation objects containing all
        /// pump stations in the master pump station database
        /// that exist in the provided Nodes collection.
        /// </summary>
        /// <param name="nodes">The Nodes collection that contains the list
        /// pf pump stations to select from the pump station database.</param>
        public PumpStations(Nodes nodes)
        {
            DataAccess.PumpStationDataSet psDS;
            psDS = new DataAccess.PumpStationDataSet();
            DataAccess.PumpStationDataSetTableAdapters.PumpStationsTableAdapter psTA;
            psTA = new SystemsAnalysis.DataAccess.PumpStationDataSetTableAdapters.PumpStationsTableAdapter();
            DataAccess.PumpStationDataSetTableAdapters.ForceMainsTableAdapter fmTA;
            fmTA = new SystemsAnalysis.DataAccess.PumpStationDataSetTableAdapters.ForceMainsTableAdapter();
            DataAccess.PumpStationDataSetTableAdapters.WetWellsTableAdapter wwTA;
            wwTA = new SystemsAnalysis.DataAccess.PumpStationDataSetTableAdapters.WetWellsTableAdapter();
            DataAccess.PumpStationDataSetTableAdapters.PumpsTableAdapter pumpTA;
            pumpTA = new SystemsAnalysis.DataAccess.PumpStationDataSetTableAdapters.PumpsTableAdapter();
            DataAccess.PumpStationDataSetTableAdapters.PumpScheduleTableAdapter pscheduleTA;
            pscheduleTA = new SystemsAnalysis.DataAccess.PumpStationDataSetTableAdapters.PumpScheduleTableAdapter();

            psTA.Fill(psDS.PumpStations);
            fmTA.Fill(psDS.ForceMains);

            wwTA.Fill(psDS.WetWells);
            pumpTA.Fill(psDS.Pumps);
            pscheduleTA.Fill(psDS.PumpSchedule);

            foreach (PumpStationDataSet.PumpStationsRow row in psDS.PumpStations)
            {
                if (nodes.Contains(row.Node))
                {
                    this.Add(new PumpStation(row));
                }
            }
        }

        #region Overriden methods from DictionaryBase
        /// <summary>
        /// Gets the PumpStation object with the specified pump station ID. 		
        /// </summary>
        public PumpStation this[int psID]
        {
            get { return (PumpStation)this.Dictionary[psID]; }
            set { this.Dictionary[psID] = value; }
        }
        /// <summary>
        /// Adds a PumpStation object to this collection.
        /// </summary>
        /// <param name="node">The PumpStation object to add to this collection.</param>
        public void Add(PumpStation ps)
        {
            this.Dictionary.Add(ps.PSID, ps);
        }
        /// <summary>
        /// Removes a PumpStation object from this PumpStations collection.
        /// </summary>
        /// <param name="psName">The name of the PumpStation object to remove 
        /// from this PumpStations collection. </param>
        public void Remove(string psName)
        {
            this.Dictionary.Remove(psName);
        }
        /// <summary>
        /// Determines whether this DiversionStructures collection contains a DiversionStructure 
        /// object with the specified divName.
        /// </summary>
        /// <param name="psName"></param>
        /// <returns>true if the specified divName was found, otherwise false.</returns>
        public bool Contains(string psName)
        {
            return this.Dictionary.Contains(psName);
        }
        /// <summary>
        /// Gets an ICollection containing a collection of DiversionStructureName strings contained
        /// in the Nodes collection.
        /// </summary>
        public ICollection Keys
        {
            get { return this.Dictionary.Keys; }
        }
        /// <summary>
        /// Gets an ICollection containing a collection of PumpStation objects
        /// contained in this PumpStations collection. Use this method to enumerate
        /// through the PumpStations using the "foreach" enumerator.
        /// </summary>
        public ICollection Values
        {
            get { return this.Dictionary.Values; }
        }

        protected override void OnValidate(object key, object value)
        {
            base.OnValidate(key, value);
            if (!(value is PumpStation))
            {
                throw new ArgumentException("PumpStations collection only supports PumpStation objects.");
            }
        }
        #endregion

    }
}
