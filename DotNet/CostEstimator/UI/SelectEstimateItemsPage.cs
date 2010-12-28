// Project: UI, File: SelectEstimateItems.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.UI, Class: SelectEstimateItems
// Path: C:\Development\DotNet\CostEstimator\UI, Author: Arnel
// Code lines: 31, Size of file: 565 Bytes
// Creation date: 7/1/2008 11:01 AM
// Last modified: 7/1/2008 1:00 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Analysis.CostEstimator.Classes;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	public partial class SelectEstimateItemsPage : CostEstimator.UI.ChildFormTemplate
	{
		#region Variables
		private string _ModelPath;
		private Model _Model;
		private SortedList<int, EstimateItemSelection> _SelectionList = new SortedList<int,EstimateItemSelection>();
		#endregion

		#region Constructors
		public SelectEstimateItemsPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Create cost factor pool page
		/// </summary>
		/// <param name="project">Project</param>
		public SelectEstimateItemsPage(Project project)
			: this()
		{
		}
		#endregion

		#region Properties
		/// <summary>
		/// Model path
		/// </summary>
		/// <returns>String</returns>
		public string ModelPath
		{
			get
			{
				return _ModelPath;
			} // get

			set
			{
				_ModelPath = value;
				FillItems();
			} // set
		} // ModelPath
		#endregion

		#region Methods
		/// <summary>
		/// Fill items
		/// </summary>
		public void FillItems()
		{
			_Model = new Model(_ModelPath);
			dsItems.Rows.SetCount(_Model.ModelLinks.Count);
			foreach (Link link in _Model.ModelLinks.Values)
			{
				EstimateItemSelection newSelectItem = new EstimateItemSelection(
					string.Format("{0} {1}-{2} {3} {4} in. diam", link.MLinkID, link.USNodeName, link.DSNodeName, 
					link.Material, link.Diameter), 
					link);
				_SelectionList.Add(link.MLinkID, newSelectItem);
			} // foreach  (link)
		} // FillItems()

		/// <summary>
		/// Item selection list
		/// </summary>
		/// <returns>List</returns>
		public List<EstimateItemSelection> ItemSelectionList
		{
			get
			{
				List<EstimateItemSelection> itemList = new List<EstimateItemSelection>();
				foreach (KeyValuePair<int, EstimateItemSelection> kvPair in _SelectionList)
				{
					if (kvPair.Value.Selected)
						itemList.Add(kvPair.Value);
				} // foreach  (kvPair)

				return itemList;
			}
		} // ItemSelectionList
		#endregion

		//
		// Events
		//

		private void dsItems_CellDataRequested(object sender, Infragistics.Win.UltraWinDataSource.CellDataRequestedEventArgs e)
		{
			EstimateItemSelection currentItem = _SelectionList.Values[e.Row.Index];
			switch (e.Column.Key)
			{
				case "Selected":
					e.Data = currentItem.Selected;
					break;
				case "Item":
					e.Data = currentItem.Item;
					break;
			}
		}

		private void dsItems_CellDataUpdated(object sender, Infragistics.Win.UltraWinDataSource.CellDataUpdatedEventArgs e)
		{
			EstimateItemSelection currentItem = _SelectionList.Values[e.Row.Index];
			switch (e.Column.Key)
			{
				case "Selected":
					currentItem.Selected = (bool)e.NewValue;
					break;
				case "Item":
					currentItem.Item = e.NewValue.ToString();
					break;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			AppForm.ViewProgress();
			AppForm.AddModelEstimateToProjectWithItemSelection();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			AppForm.CloseProject();
		} // SelectEstimateItems(modelPath)

	}
}

