using System;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.DataAccess;

namespace SystemsAnalysis.Characterization
{
	/// <summary>
	/// Summary description for LinkHydraulics.
	/// </summary>
	public class LinkHydraulic
	{		
        private int linkHydraulicsID;
		private int mLinkID;
		private Link link;
		private bool hasLink;
		private double maxQ;
		private double maxV;
		private double qQRatio;		
		private double maxUSElev;
		private double maxDSElev;		

		internal LinkHydraulic(System.Data.IDataReader mstLinkHydraulicsReader)
		{
			this.linkHydraulicsID = (int)mstLinkHydraulicsReader["linkHydraulicsID"];
			this.mLinkID = (int)mstLinkHydraulicsReader["MLinkID"];
			this.maxQ = Convert.ToDouble(mstLinkHydraulicsReader["maxQ"]);
            this.maxV = Convert.ToDouble(mstLinkHydraulicsReader["maxV"]);
            this.qQRatio = Convert.ToDouble(mstLinkHydraulicsReader["qQRatio"]);
            this.maxUSElev = Convert.ToDouble(mstLinkHydraulicsReader["maxUSElev"]);
            this.maxDSElev = Convert.ToDouble(mstLinkHydraulicsReader["maxDSElev"]);			
		}

		/// <summary>
		/// The LinkID of this LinkHydraulic object.
		/// </summary>
		public int LinkHydraulicsID
		{
			get { return this.linkHydraulicsID; }
		}
		/// <summary>
		/// The MLinkID associated with this pipe, or 0 if the pipe is not
		/// represented in the master links database.
		/// </summary>
		public int MLinkID
		{
			get { return this.mLinkID; }
		}
		/// <summary>
		/// The peak modeled flow for this pipe in cfs.
		/// </summary>
		public double MaxQ
		{
			get { return this.maxQ; }
		}
		/// <summary>
		/// The peak modeled velocity for this pipe in fps.
		/// </summary>
		public double MaxV
		{
			get { return this.maxV; }
		}
		/// <summary>
		/// The peak flow to design flow ratio for this pipe.
		/// </summary>
		public double QQRatio
		{
			get { return this.qQRatio; }
		}
		/// <summary>
		/// The maximum modeled water surface elevation at the upstream 
		/// end of the pipe.
		/// </summary>
		public double MaxUSElev
		{
			get { return this.maxUSElev; }
		}
		/// <summary>
		/// The maximum modeled water surface elevation at the downstream 
		/// end of the pipe.
		/// </summary>
		public double MaxDSElev
		{
			get { return this.maxDSElev; }
		}
		/// <summary>
		/// Gets or sets the Link object associated with this LinkHydraulic
		/// object. Returns null if there is no associated Link object.
		/// </summary>
		public Link Link
		{
			get 
			{
				if (this.hasLink)
				{
					return this.link;
				}
				else
				{
					return null;
				}
			}
			set 
			{ 
				if (value != null && value.MLinkID == this.MLinkID)
				{
					this.hasLink = true;
					this.link = value;
				}
				else
				{
					throw new MismatchedLinkException();
				}
			}
		}
		/// <summary>
		/// Indicates if this LinkHydraulic object contains a reference to 
		/// an associated Link object.
		/// </summary>
		public bool HasLink
		{
			get { return this.hasLink; }
		}
	}
}
