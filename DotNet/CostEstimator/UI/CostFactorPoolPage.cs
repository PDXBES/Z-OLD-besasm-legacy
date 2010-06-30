// Project: UI, File: CostFactorPoolPage.cs
// Namespace: CostEstimator.UI, Class: CostFactorPoolPage
// Path: C:\Development\CostEstimatorV2\UI, Author: Arnel
// Code lines: 31, Size of file: 586 Bytes
// Creation date: 3/26/2008 4:42 PM
// Last modified: 3/26/2008 4:45 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.Analysis.CostEstimator.Classes;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	public partial class CostFactorPoolPage : CostEstimator.UI.ChildFormTemplate
	{
		#region Variables
		private Project _project;
		private Infragistics.Win.ValueList _factorTypeList = new Infragistics.Win.ValueList();
		#endregion

		#region Constructors
		private CostFactorPoolPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Create cost factor pool page
		/// </summary>
		/// <param name="project">Project</param>
		public CostFactorPoolPage(Project project)
			: this()
		{
			_project = project;
		}
		#endregion
		
		//
		// Events
		//

		private void CostFactorPoolPage_Load(object sender, EventArgs e)
		{
			if (_project != null)
				dsCostFactorPool.Rows.SetCount(_project.CostFactorPoolCount);

			// Setup cost grid for factors
			foreach (string s in Enum.GetNames(typeof(CostFactorType)))
			{
				CostFactorType itemToAdd = (CostFactorType)Enum.Parse(typeof(CostFactorType), s);
				_factorTypeList.ValueListItems.Add(itemToAdd, s);
			}
			Infragistics.Win.UltraWinGrid.UltraGridBand factorsBand = gridFactors.DisplayLayout.Bands[0];
			factorsBand.Columns["FactorType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			factorsBand.Columns["FactorType"].ValueList = _factorTypeList;
		}

		private void dsCostFactorPool_CellDataRequested(object sender, Infragistics.Win.UltraWinDataSource.CellDataRequestedEventArgs e)
		{
			int factorIndex = e.Row.Index;
			CostFactor factor = _project.FactorFromPoolByIndex(factorIndex);
			switch (e.Column.Key)
			{
				case "ID":
					e.Data = factor.ID;
					break;
				case "Name":
					e.Data = factor.Name;
					break;
				case "FactorValue":
					e.Data = factor.FactorValue;
					break;
				case "FactorType":
					e.Data = factor.FactorType.ToString();
					break;
				case "Comment":
					e.Data = factor.Comment;
					break;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			AppForm.ViewCosts();
		}

		private void CostFactorPoolPage_VisibleChanged(object sender, EventArgs e)
		{
			if (AppForm != null && this.Visible == true)
			{
				if (AppForm.toolbarManager.ActiveMdiChildManager != null)
				{
					AppForm.toolbarManager.ActiveMdiChildManager = null;
					AppForm.toolbarManager.Ribbon.Tabs.RemoveAt(AppForm.toolbarManager.Ribbon.Tabs.Count - 1);
				}

				AppForm.toolbarManager.ActiveMdiChildManager = this.toolbarManager;
				AppForm.toolbarManager.Ribbon.SelectedTab =
					AppForm.toolbarManager.Ribbon.Tabs[AppForm.toolbarManager.Ribbon.Tabs.Count - 1];

				dsCostFactorPool.Rows.SetCount(_project.CostFactorPoolCount);
				dsCostFactorPool.ResetCachedValues();
			}
		}

		private void toolbarManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
		{
			switch (e.Tool.Key)
			{
				case "Exit":    // ButtonTool
					AppForm.ViewCosts();
					break;
			}
		} // CostFactorPoolPage(project)
	}
}

