using System;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.DataAccess;

namespace SystemsAnalysis.Modeling
{
	/// <summary>
	/// Summary description for ForceMain.
	/// </summary>
	public class ForceMain
	{
        int forceMainID;
		double length;
		double staticHead;
		int mLinkID;

		public ForceMain(DataAccess.PumpStationDataSet.ForceMainsRow row)
		{
            this.forceMainID = row.ForceMainID;
            this.staticHead = Convert.ToDouble(row.StaticHead);
            this.length = Convert.ToDouble(row.Length);
            this.mLinkID = row.MLinkID;		
		}

        public int ForceMainID
        {
            get { return this.forceMainID; }
        }
		public double StaticHead
		{
			get { return this.staticHead; }
		}
		public double Length
		{
			get { return this.length; }
		}
		public int MLinkID
		{
			get { return this.mLinkID; }
		}

	}
}
