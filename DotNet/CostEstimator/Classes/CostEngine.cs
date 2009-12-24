// Project: Classes, File: CostEngine.cs
// Namespace: CostEstimator.Classes, Class: CostEngine
// Path: C:\Development\CostEstimatorV2\Classes, Author: Arnel
// Code lines: 14, Size of file: 194 Bytes
// Creation date: 3/1/2008 2:33 PM
// Last modified: 3/1/2008 3:19 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	public class CostEngine
	{
		#region Variables
		private List<Project> _Projects;
		#endregion

		#region Properties
		/// <summary>
		/// Project count
		/// </summary>
		/// <returns>Int</returns>
		public int ProjectCount
		{
			get
			{
				return _Projects.Count;
			} // get
		} // ProjectCount
		#endregion

		#region Constructors
		/// <summary>
		/// Create cost engine
		/// </summary>
		public CostEngine()
		{
			_Projects = new List<Project>();
		} // CostEngine()
		#endregion

		#region Methods
		/// <summary>
		/// Add project
		/// </summary>
		public Project AddProject()
		{
			Project newProject = new Project();
			_Projects.Add(newProject);

			return newProject;
		} // AddProject()

		/// <summary>
		/// Export
		/// </summary>
		public void Export()
		{
		} // Export()

		/// <summary>
		/// Generate
		/// </summary>
		public void Generate()
		{
		} // Generate()

		/// <summary>
		/// Load cast factor templates
		/// </summary>
		public void LoadCostFactorTemplates()
		{
		} // LoadCastFactorTemplates()

		/// <summary>
		/// Load cost item templates
		/// </summary>
		public void LoadCostItemTemplates()
		{
		} // LoadCostItemTemplates()

		/// <summary>
		/// Load project
		/// </summary>
		public void LoadProject()
		{
		} // LoadProject()

		/// <summary>
		/// Load templates
		/// </summary>
		public void LoadTemplates()
		{
			LoadCostFactorTemplates();
			LoadCostItemTemplates();
		} // LoadTemplates()

		/// <summary>
		/// Save cost factor templates
		/// </summary>
		public void SaveCostFactorTemplates()
		{
		} // SaveCostFactorTemplates()

		/// <summary>
		/// Save cost item templates
		/// </summary>
		public void SaveCostItemTemplates()
		{
		} // SaveCostItemTemplates()

		/// <summary>
		/// Save project
		/// </summary>
		public void SaveProject()
		{
		} // SaveProject()

		/// <summary>
		/// Save templates
		/// </summary>
		public void SaveTemplates()
		{
			SaveCostFactorTemplates();
			SaveCostItemTemplates();
		} // SaveTemplates()
		#endregion
	}
}
