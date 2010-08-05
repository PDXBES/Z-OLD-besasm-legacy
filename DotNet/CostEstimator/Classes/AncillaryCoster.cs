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
	/// Supplementary costing engine for ancillary costs incurred by positional
  /// qualities such as overlaid utilities, transportation, and special
  /// environmental zones
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
		/// Create ancillary coster for an alternative package
		/// </summary>
		/// <param name="altPackage">The alternative package being processed</param>
		public AncillaryCoster(AlternativePackage altPackage)
		{
			_AltPackage = altPackage;
			_Model = null;
		} // AncillaryCoster(altPackage)

		/// <summary>
		/// Create ancillary coster for a model
		/// </summary>
		/// <param name="model">The model being processed</param>
		public AncillaryCoster(Model model)
		{
			_Model = model;
			_AltPackage = null;
		} // AncillaryCoster(model)
		#endregion

		#region Properties
		/// <summary>
		/// Returns the current AltPipXP record
		/// </summary>
		/// <returns>An AltPipXP record</returns>
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
		/// Returns the current AltLink record
		/// </summary>
		/// <returns>An AltLink record</returns>
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
		/// Returns the current PipXp record
		/// </summary>
		/// <returns>A PipXp record</returns>
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
		/// Returns the current link being processed
		/// </summary>
		/// <returns>A Link record</returns>
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

    /// <summary>
    /// Calculates and packages ancillary costs for a given link's conflict package
    /// </summary>
    /// <param name="ancillaryCosts">Reference to list that will contain ancillary costs</param>
    /// <param name="conflictPackage">Conflict package from link that will be used to get ancillary costs</param>
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
		/// Packages ancillary factors for a given link's conflict package
		/// </summary>
		/// <param name="ancillaryFactors">Reference to list that will contain ancillary factors</param>
		/// <param name="conflictPackage">Conflict package from link that will be used to get ancillary factors</param>
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
		/// Returns list of ancillary costs from the current alternative package
		/// </summary>
		/// <returns>List of ancillary costs</returns>
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
		/// Returns list of ancillary factors from the current alternative package
		/// </summary>
		/// <returns>List of ancillary factors</returns>
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
		/// Returns list of ancillary costs from the current model
		/// </summary>
		/// <returns>List of ancillary costs</returns>
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
		/// Returns list of ancillary factors from the current model
		/// </summary>
		/// <returns>List of ancillary factors</returns>
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
