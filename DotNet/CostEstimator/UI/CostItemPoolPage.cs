// Project: UI, File: CostItemPoolPage.cs
// Namespace: CostEstimator.UI, Class: CostItemPoolPage
// Path: C:\Development\CostEstimatorV2\UI, Author: Arnel
// Code lines: 40, Size of file: 690 Bytes
// Creation date: 3/27/2008 12:36 PM
// Last modified: 4/1/2008 3:00 PM

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
  public partial class CostItemPoolPage : CostEstimator.UI.ChildFormTemplate
  {
    #region Variables
    Project _project;
    #endregion

    #region Constructors
    private CostItemPoolPage()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Create cost item pool page
    /// </summary>
    /// <param name="project">Project</param>
    public CostItemPoolPage(Project project)
      : this()
    {
      _project = project;
    } // CostItemPoolPage(project)

    #endregion

    #region Methods

    #endregion

    private void CostItemPoolPage_Load(object sender, EventArgs e)
    {
      if (_project != null)
        dsCostItemPool.Rows.SetCount(_project.CostItemPoolCount);
    }

    private void dsCostItemPool_CellDataRequested(object sender, Infragistics.Win.UltraWinDataSource.CellDataRequestedEventArgs e)
    {
      int costItemIndex = e.Row.Index;
      CostItem costItem = _project.CostItemFromPoolByIndex(costItemIndex);
      switch (e.Column.Key)
      {
        case "ID":
          e.Data = costItem.ID;
          break;
        case "Name":
          e.Data = costItem.Name;
          break;
        case "Quantity":
          e.Data = costItem.Quantity;
          break;
        case "UnitCost":
          e.Data = costItem.UnitCost;
          break;
        case "UnitName":
          e.Data = costItem.UnitName;
          break;
        case "Comment":
          e.Data = costItem.Comment;
          break;
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      AppForm.ViewCosts();
    }

    /// <summary>
    /// Cost item pool page _ visible changed
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">E</param>
    private void CostItemPoolPage_VisibleChanged(object sender, EventArgs e)
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

        dsCostItemPool.Rows.SetCount(_project.CostItemPoolCount);
        dsCostItemPool.ResetCachedValues();
      } // if
    }

    private void toolbarManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
    {
      switch (e.Tool.Key)
      {
        case "Exit": // ButtonTool
          AppForm.ViewCosts();
          break;
      }
    } // CostItemPoolPage_VisibleChanged(sender, e)
  }
}

