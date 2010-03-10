// Project: UI, File: EstimateItemSelection.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.UI, Class: EstimateItemSelection
// Path: C:\Development\DotNet\CostEstimator\UI, Author: Arnel
// Code lines: 28, Size of file: 430 Bytes
// Creation date: 7/1/2008 11:17 AM
// Last modified: 7/1/2008 11:28 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	public class EstimateItemSelection
	{
		#region Variables
		private bool _Selected = false;
		private string _Item = "";
		private object _Object = null;
		#endregion

		#region Constructors
		/// <summary>
		/// Create estimate item selection
		/// </summary>
		/// <param name="item">Item</param>
		public EstimateItemSelection(string item)
		{
			_Item = item;
		} // EstimateItemSelection(item)

		/// <summary>
		/// Create estimate item selection
		/// </summary>
		/// <param name="item">Item</param>
		/// <param name="tagObject">Tag object</param>
		public EstimateItemSelection(string item, object tagObject)
		{
			_Item = item;
			_Object = tagObject;
		} // EstimateItemSelection(item, tagObject)
		#endregion

		#region Properties
		/// <summary>
		/// Selected
		/// </summary>
		/// <returns>Bool</returns>
		public bool Selected
		{
			get
			{
				return _Selected;
			} // get

			set
			{
				_Selected = value;
			} // set
		} // Selected

		/// <summary>
		/// Item
		/// </summary>
		/// <returns>String</returns>
		public string Item
		{
			get
			{
				return _Item;
			} // get

			set
			{
				_Item = value;
			} // set
		} // Item

		/// <summary>
		/// Object
		/// </summary>
		/// <returns>Object</returns>
		public object Object
		{
			get
			{
				return _Object;
			} // get

			set
			{
				_Object = value;
			} // set
		} // Object
		#endregion

		#region Methods

		#endregion
	}
}
