// Project: Classes, File: AncillaryCoster.cs
// Namespace: CostEstimator.Classes, Class: AncillaryCoster
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 18, Size of file: 256 Bytes
// Creation date: 5/12/2008 1:08 PM
// Last modified: 8/26/2008 11:16 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;
using SystemsAnalysis.Modeling;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	/// <summary>
	/// Ancillary coster
	/// </summary>
	public class AncillaryCoster
	{
		#region Variables
		private AltPipXP _AltPipXP;
		private AltLink _AltLink;
		private AlternativePackage _AltPackage;

		private PipXP _PipXP;
		private Link _Link;
		private Model _Model;

		private PipeCoster _PipeCoster = new PipeCoster();
		#endregion

		#region Constructors
		/// <summary>
		/// Create ancillary coster
		/// </summary>
		/// <param name="altPackage">Alt package</param>
		public AncillaryCoster(AlternativePackage altPackage)
		{
			_AltPackage = altPackage;
			_Model = null;
		} // AncillaryCoster(altPackage)

		/// <summary>
		/// Create ancillary coster
		/// </summary>
		/// <param name="model">Model</param>
		public AncillaryCoster(Model model)
		{
			_Model = model;
			_AltPackage = null;
		} // AncillaryCoster(model)
		#endregion

		#region Properties
		/// <summary>
		/// Alt pip XP
		/// </summary>
		/// <returns>Alt pip XP</returns>
		public AltPipXP AltPipXP
		{
			get
			{
				if (_AltPackage != null)
					return _AltPipXP;
				else
					return null;
			} // get

			set
			{
				if (_AltPackage != null)
					_AltPipXP = value;
			} // set
		} // AltPipXP

		/// <summary>
		/// Alt link
		/// </summary>
		/// <returns>Alt link</returns>
		public AltLink AltLink
		{
			get
			{
				if (_AltPackage != null)
					return _AltLink;
				else
					return null;
			} // get

			set
			{
				if (_AltPackage != null)
					_AltLink = value;
			} // set
		} // AltLink

		/// <summary>
		/// Pip XP
		/// </summary>
		/// <returns>Pip XP</returns>
		public PipXP PipXP
		{
			get
			{
				if (_Model != null)
					return _PipXP;
				else
					return null;
			} // get

			set
			{
				if (_Model != null)
					_PipXP = value;
			} // set
		} // PipXP

		/// <summary>
		/// Link
		/// </summary>
		/// <returns>Link</returns>
		public Link Link
		{
			get
			{
				if (_Model != null)
					return _Link;
				else
					return null;
			} // get

			set
			{
				if (_Model != null)
					_Link = value;
			} // set
		} // Link

		private static void GetAncillaryCosts(List<AncillaryCost> ancillaryCosts, ConflictPackage conflictPackage)
		{
			ancillaryCosts.Add(new BoringJackingAncillaryCost(conflictPackage).AncillaryCost);
			ancillaryCosts.Add(new TrafficControlAncillaryCost(conflictPackage).AncillaryCost);
			ancillaryCosts.Add(new ParallelRelocationAncillaryCost(conflictPackage).AncillaryCost);
			ancillaryCosts.Add(new CrossingRelocationAncillaryCost(conflictPackage).AncillaryCost);
			ancillaryCosts.Add(new BypassPumpingAncillaryCost(conflictPackage).AncillaryCost);
			ancillaryCosts.Add(new EnvironmentalMitigationAncillaryCost(conflictPackage).AncillaryCost);
			ancillaryCosts.Add(new HazardousMaterialAncillaryCost(conflictPackage).AncillaryCost);

			// Clean up the list (nulls might be there if an ancillary cost comes back null
			for (int i = ancillaryCosts.Count - 1; i >= 0; i--)
			{
				if (ancillaryCosts[i] == null)
					ancillaryCosts.RemoveAt(i);
			}
		}

		/// <summary>
		/// Get ancillary factors
		/// </summary>
		/// <param name="ancillaryFactors">Ancillary factors</param>
		/// <param name="conflictPackage">Conflict package</param>
		private static void GetAncillaryFactors(List<AncillaryFactor> ancillaryFactors, ConflictPackage conflictPackage)
		{
			ancillaryFactors.Add(new DifficultAreaAncillaryFactor(conflictPackage).AncillaryFactor);

			for (int i = ancillaryFactors.Count - 1; i >= 0 ; i--)
      {
      	if (ancillaryFactors[i] == null)
					ancillaryFactors.RemoveAt(i);
      }
		} // GetAncillaryFactors(ancillaryFactors, conflictPackage)

		/// <summary>
		/// Ancillary costs
		/// </summary>
		/// <returns>List</returns>
		public List<AncillaryCost> AlternativeAncillaryCosts
		{
			get
			{
				List<AncillaryCost> ancillaryCosts = new List<AncillaryCost>();

				ConflictPackage altConflictPackage = new ConflictPackage(_AltPackage, _AltLink, _AltPipXP);
				GetAncillaryCosts(ancillaryCosts, altConflictPackage);

				return ancillaryCosts;
			} // get
		} // AncillaryCosts

		/// <summary>
		/// Ancillary factors
		/// </summary>
		/// <returns>List</returns>
		public List<AncillaryFactor> AlternativeAncillaryFactors
		{
			get
			{
				List<AncillaryFactor> ancillaryFactors = new List<AncillaryFactor>();

				ConflictPackage altConflictPackage = new ConflictPackage(_AltPackage, _AltLink, _AltPipXP);
				GetAncillaryFactors(ancillaryFactors, altConflictPackage);

				return ancillaryFactors;
			}
		} // AncillaryFactors

		/// <summary>
		/// Model ancillary costs
		/// </summary>
		/// <returns>List</returns>
		public List<AncillaryCost> ModelAncillaryCosts
		{
			get
			{
				List<AncillaryCost> ancillaryCosts = new List<AncillaryCost>();

				ConflictPackage modelConflictPackage = new ConflictPackage(_Model, _Link, _PipXP);
				GetAncillaryCosts(ancillaryCosts, modelConflictPackage);

				return ancillaryCosts;
			} // get
		} // ModelAncillaryCosts

		/// <summary>
		/// Model ancillary factors
		/// </summary>
		/// <returns>List</returns>
		public List<AncillaryFactor> ModelAncillaryFactors
		{
			get
      {
				List<AncillaryFactor> ancillaryFactors = new List<AncillaryFactor>();

				ConflictPackage modelConflictPackage = new ConflictPackage(_Model, _Link, _PipXP);
				GetAncillaryFactors(ancillaryFactors, modelConflictPackage);
      	return ancillaryFactors;
      } // get
		} // ModelAncillaryFactors
		#endregion
	}
}
