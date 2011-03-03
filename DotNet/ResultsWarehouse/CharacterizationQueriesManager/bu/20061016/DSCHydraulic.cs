using System;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Characterization
{
	/// <summary>
	/// Summary description for DSCHydraulics.
	/// </summary>
	public class DSCHydraulic
	{
		private int dscHydraulicsID;
		private int dscid;
		private double hgl;
		private double deltaHGL;
		private double surcharge;
		private DSC dsc;
		private bool hasDSC;

		internal DSCHydraulic(System.Data.OleDb.OleDbDataReader mstDSCHydraulicsReader)
		{
			this.dscHydraulicsID = (int)mstDSCHydraulicsReader["DSCHydraulicsID"];
			this.dscid = (int)mstDSCHydraulicsReader["DSCID"];
			this.hgl = (double)mstDSCHydraulicsReader["HGL"];
			this.deltaHGL = (double)mstDSCHydraulicsReader["deltaHGL"];
			this.surcharge = (double)mstDSCHydraulicsReader["surcharge"];
		}
		/// <summary>
		/// Returns the DSCID associated with this DSC hydraulic modeling results.
		/// The DSCID is equal to ParcelID * 100 + DivideID.
		/// </summary>
		public int DSCID
		{
			get { return this.dscid; }
		}
		/// <summary>
		/// Returns the peak HGL all the pipe that this DSC is connected to.
		/// </summary>
		public double HGL
		{
			get { return this.hgl; }
		}
		/// <summary>
		/// Returns the difference between the peak HGL and the finished floor elevation
		/// for a given DSC.
		/// </summary>
		public double DeltaHGL
		{
			get { return this.deltaHGL; }
		}
		/// <summary>
		/// Returns the surcharge elevation of pipe connected to this DSC, at the point of
		/// connection.
		/// </summary>
		public double Surcharge
		{ 
			get { return this.surcharge; }
		}
		/// <summary>
		/// Gets or sets the DSC object associated with this DSC hydraulic results
		/// object. Returns null is this DSC hydraulic result does not have an 
		/// associated DSC object.  
		/// </summary>
		public DSC DSC
		{
			get 
			{
				if (this.hasDSC)
				{
					return this.dsc;
				}
				else
				{
					return null;
				}
			}
			set 
			{ 
				if (value != null && value.DSCID == this.DSCID)
				{
					this.hasDSC = true;
					this.dsc = value;
				}
				else
				{
					throw new Exception("Mismatched DSC added to DSCHydraulic");
				}
			}
		}
		/// <summary>
		/// Indicates if this DSCHydraulic object contains a reference to 
		/// an associated DSC object.
		/// </summary>
		public bool HasDSC
		{
			get { return this.hasDSC; }
		}
	}
}
