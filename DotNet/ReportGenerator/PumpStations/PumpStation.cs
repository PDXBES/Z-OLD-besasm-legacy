using System;
using System.Collections;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.DataAccess;

namespace SystemsAnalysis.Modeling
{
	/// <summary>
	/// Class for storing diversion structure information.
	/// </summary>
	public class PumpStation
	{
        private int psID;
		private string name;
		private string nodeName;
		private int firmCapacity;
		private int fullCapacity;
		private string address;
		private string type;
		private ArrayList forcemains;
		private ArrayList wetwells;
		private ArrayList pumps;

		public PumpStation(DataAccess.PumpStationDataSet.PumpStationsRow psRow)
		{
            this.psID = psRow.PumpStationID;
            this.name = psRow.Name;
            this.nodeName = psRow.Node;
            this.firmCapacity = psRow.FirmCapacity;
            this.fullCapacity = psRow.FullCapacity;
            this.address = psRow.Address;
            this.type = psRow.Type;
			this.forcemains = new ArrayList();
			this.wetwells = new ArrayList();
			this.pumps = new ArrayList();
			foreach (DataAccess.PumpStationDataSet.ForceMainsRow forcemainRow in psRow.GetForceMainsRows())
			{
				try 
				{
					this.forcemains.Add(new ForceMain(forcemainRow));
				}
				catch(Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e.ToString());
				}
			}
			foreach (DataAccess.PumpStationDataSet.WetWellsRow wetwellRow in psRow.GetWetWellsRows())
			{
				try
				{
					this.wetwells.Add(new WetWell(wetwellRow));
				}
				catch(Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e.ToString());
				}
			}
			foreach (DataAccess.PumpStationDataSet.PumpsRow pumpRow in psRow.GetPumpDataRows())
			{
				try 
				{
					this.pumps.Add(new Pump(pumpRow));
				}
				catch(Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e.ToString());
				}
			}
		}

        public int PSID
        {
            get { return this.psID; }
        }


		public string Name
		{
			get { return this.name; }
		}

		public string NodeName
		{
			get { return this.nodeName; }
		}
		public int FirmCapacity
		{
			get { return this.firmCapacity; }
		}
		public int FullCapacity
		{
			get { return this.fullCapacity; }
		}
		public string Address
		{
			get { return this.address; }
		}
		public string Type
		{
			get { return this.type; }
		}


		public ArrayList ForceMains
		{
			get { return this.forcemains; }
		}
		public ArrayList WetWells
		{
			get { return this.wetwells; }
		}
		public ArrayList Pumps
		{
			get { return this.pumps; }
		}


	}
}
