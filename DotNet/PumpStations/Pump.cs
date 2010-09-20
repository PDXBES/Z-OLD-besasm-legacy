using System;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.DataAccess;

namespace SystemsAnalysis.Modeling
{
	/// <summary>
	/// Summary description for Pump.
	/// </summary>
	public class Pump
	{
        int pumpID;
		int pumpNumber;
		double motorSpeed;
		double horsePower;
		double capacity;

		public Pump(DataAccess.PumpStationDataSet.PumpsRow row)
		{
            this.pumpID = row.PumpID;
            this.motorSpeed = Convert.ToDouble(row.MotorSpeed);
            this.horsePower = Convert.ToDouble(row.HorsePower);
            this.capacity = Convert.ToDouble(row.Capacity);
            this.pumpNumber = row.PumpNumber;
		}

        public int PumpID
        {
            get { return this.pumpID; }
        }
		public int PumpNumber
		{
			get { return this.pumpNumber; }
		}
		public double MotorSpeed
		{
			get { return this.motorSpeed; }
		}
		public double HorsePower
		{
			get { return this.horsePower; }
		}
		public double Capacity
		{
			get { return this.capacity; }
		}
	}
}
