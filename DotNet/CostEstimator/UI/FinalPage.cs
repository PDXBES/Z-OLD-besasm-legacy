// Project: UI, File: FinalPage.cs
// Namespace: CostEstimator.UI, Class: FinalPage
// Path: C:\Development\DotNet\CostEstimator\UI, Author: Arnel
// Code lines: 76, Size of file: 1.80 KB
// Creation date: 4/28/2008 1:04 PM
// Last modified: 8/21/2009 10:07 AM

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
	public partial class FinalPage : CostEstimator.UI.ChildFormTemplate
	{
		#region Variables
		private Project _project;
		#endregion

		#region Constructors
		public FinalPage()
		{
			InitializeComponent();
		}

		public FinalPage(Project project)
			: this()
		{
			_project = project;

		}
		#endregion

		#region Methods
		/// <summary>
		/// Prepare report
		/// </summary>
		public void PrepareReport(CostItemFactor estimate)
		{
			ProjectBindingSource.DataSource = _project;
			ReportPipeItemBindingSource.DataSource = _project.ReportPipeItems(estimate);
			ReportGenericItemBindingSource.DataSource = _project.ReportOtherDirectConstructionItems(estimate);
			ReportSummaryItemBindingSource.DataSource = _project.ReportSummaryItems(estimate);
			ReportInflowControlItemBindingSource.DataSource = _project.ReportInflowControlItems(estimate);
			ReportFocusAreaInfoBindingSource.DataSource = new ReportFocusAreaInfo(estimate.Name);
		} // PrepareReport()

		/// <summary>
		/// Exit page
		/// </summary>
		private void ExitPage()
		{
			AppForm.ViewCosts();
		} // ExitPage()

		/// <summary>
		/// Refresh report
		/// </summary>
		private void RefreshReport()
		{
			PrepareReport(lstEstimates.ActiveItem.Value as CostItemFactor);
			reportViewer.RefreshReport();
		} // RefreshReport()
		#endregion

		private void FinalPage_VisibleChanged(object sender, EventArgs e)
		{
			if (AppForm != null && this.Visible == true)
			{
				if (AppForm.toolbarManager.ActiveMdiChildManager != null)
				{
					AppForm.toolbarManager.ActiveMdiChildManager = null;
					AppForm.toolbarManager.Ribbon.Tabs.RemoveAt(AppForm.toolbarManager.Ribbon.Tabs.Count - 1);
				} // if

				AppForm.toolbarManager.ActiveMdiChildManager = this.toolbarManager;
				AppForm.toolbarManager.Ribbon.SelectedTab =
					AppForm.toolbarManager.Ribbon.Tabs[AppForm.toolbarManager.Ribbon.Tabs.Count - 1];
			} // if

			if (lstEstimates.ActiveItem != null)
				RefreshReport();
		}

		private void FinalPage_Load(object sender, EventArgs e)
		{
			SortedList<string, CostItemFactor> sortedEstimateList = 
				new SortedList<string, CostItemFactor>();
			foreach (CostItemFactor estimate in _project.Estimates)
			{
				sortedEstimateList.Add(estimate.Name, estimate);
			} // foreach  (estimate)
			lstEstimates.Items.Clear();
			foreach (KeyValuePair<string, CostItemFactor> kvPair in sortedEstimateList)
			{
				Infragistics.Win.UltraWinListView.UltraListViewItem item = lstEstimates.Items.Add(kvPair.Key, kvPair.Value);
			} // foreach  (kvPair)

			PrepareReport(lstEstimates.Items[0].Value as CostItemFactor);
			lstEstimates.Items[0].Activate();
			lstEstimates.Select();
			reportViewer.RefreshReport();
		}

		private void toolbarManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
		{
			switch (e.Tool.Key)
			{
				case "Exit":    // ButtonTool
					ExitPage();
					break;

			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			ExitPage();
		}

		private void reportViewer_ReportRefresh(object sender, CancelEventArgs e)
		{
			RefreshReport();
		}

		private void lstEstimates_ItemActivated(object sender, Infragistics.Win.UltraWinListView.ItemActivatedEventArgs e)
		{
			RefreshReport();
		}
	}
}

