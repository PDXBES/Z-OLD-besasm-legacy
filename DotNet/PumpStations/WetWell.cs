using System;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.DataAccess;

namespace SystemsAnalysis.Modeling
{
	/// <summary>
	/// Summary description for WetWell.
	/// </summary>
	public class WetWell
	{
        int wetWellID;
		int wetWellNum;
		double d1;
		double d2;
		string wetWellShape;
		double vol;
		string bypass;
		public WetWell(DataAccess.PumpStationDataSet.WetWellsRow row)
		{
            this.wetWellID = row.WetWellID;
            this.wetWellNum = row.WetWellNumber;
            this.d1 = Convert.ToDouble(row.D1OrW1);
            this.d2 = Convert.ToDouble(row.D2OrW2);
            this.vol = Convert.ToDouble(row.Volume);
            this.wetWellShape = row.Outline;
            this.bypass = row.BypassReason;
		}

        public int WetWellID
        {
            get { return this.wetWellID; }
        }
		public int Number
		{
			get { return this.wetWellNum; }
		}
		public double Dimension1
		{
			get { return this.d1; }
		}
		public double Dimension2
		{
			get { return this.d2; }
		}
		public string Shape
		{
			get { return this.wetWellShape; }
		}
		public double Volume
		{
			get { return this.vol; }
		}
		public string ByPass
		{
			get { return this.bypass; }
		}
		
	}
}
