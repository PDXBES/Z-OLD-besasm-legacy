// Project: Classes, File: ConflictPackage.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: ConflictPackage
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 22, Size of file: 480 Bytes
// Creation date: 6/30/2008 4:18 PM
// Last modified: 6/30/2008 5:48 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Modeling.Alternatives;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	public class ConflictPackage
	{
		#region Variables
		private AlternativePackage _AltPackage = null;
		private AltLink _AltLink = null;

		private Model _Model = null;
		private Link _Link = null;

		private PipeConflict _PipeConflict = null;
		#endregion

		#region Constructors
		/// <summary>
		/// Create conflict package
		/// </summary>
		/// <param name="altPackage">Alt package</param>
		/// <param name="link">Link</param>
		/// <param name="altPipXP">Alt pip XP</param>
		public ConflictPackage(AlternativePackage altPackage, AltLink altLink, AltPipXP altPipXP)
		{
			_AltPackage = altPackage;
			_AltLink = altLink;
			_PipeConflict = altPipXP;
		} // ConflictPackage(altPackage, link, altPipXP)

		/// <summary>
		/// Create conflict package
		/// </summary>
		/// <param name="model">Model</param>
		/// <param name="link">Link</param>
		/// <param name="pipXP">Pip XP</param>
		public ConflictPackage(Model model, Link link, PipXP pipXP)
		{
			_Model = model;
			_Link = link;
			_PipeConflict = pipXP;
		} // ConflictPackage(model, link, pipXP)
		#endregion

		#region Properties
		/// <summary>
		/// Diameter
		/// </summary>
		/// <returns>Double</returns>
		public double Diameter
		{
			get
			{
				return _AltPackage == null ? _Link.Diameter : _AltLink.Diameter;
			} // get
		} // Diameter

		/// <summary>
		/// Depth
		/// </summary>
		/// <returns>Double</returns>
		public double Depth
		{
			get
			{
				return _AltPackage == null ? _Model.PipeDepth(_Link) :
					_AltPackage.PipeDepth(_AltLink);
			} // get
		} // Depth

		/// <summary>
		/// Length
		/// </summary>
		/// <returns>Double</returns>
		public double Length
		{
			get
			{
				return _AltPackage == null ? _Link.Length :
					_AltLink.Length;
			} // get
		} // Length

		/// <summary>
		/// Slope
		/// </summary>
		/// <returns>Double</returns>
		public double Slope
		{
			get
			{
				return _AltPackage == null ? (_Link.USIE - _Link.DSIE) / _Link.Length :
					(_AltLink.USIE - _AltLink.DSIE) / _AltLink.Length;
			}
		} // Slope
		/// <summary>
		/// Pipe material
		/// </summary>
		/// <returns>Pipe material</returns>
		public PipeMaterial PipeMaterial
		{
			get
			{
				return _AltPackage == null ? PipeCoster.StringToPipeMaterial(_Link.Material) :
					PipeCoster.StringToPipeMaterial(_AltLink.Material);
			}
		} // PipeMaterial

		/// <summary>
		/// USNode
		/// </summary>
		/// <returns>String</returns>
		public string USNode
		{
			get
			{
				return _AltPackage == null ? _Link.USNodeName : _AltLink.USNodeName;
			} // get
		} // USNode

		/// <summary>
		/// DSNode
		/// </summary>
		/// <returns>String</returns>
		public string DSNode
		{
			get
			{
				return _AltPackage == null ? _Link.DSNodeName : _AltLink.DSNodeName;
			} // get
		} // DSNode

		/// <summary>
		/// Mst link ID
		/// </summary>
		/// <returns>Int</returns>
		public int MstLinkID
		{
			get
			{
				return _AltPackage == null ? _Link.MLinkID : 0;
			} // get
		} // MstLinkID

		/// <summary>
		/// Conflict
		/// </summary>
		/// <returns>Pipe conflict</returns>
		public PipeConflict Conflicts
		{
			get
			{
				return _PipeConflict;
			} // get
		} // Conflict
		#endregion
	}
}
