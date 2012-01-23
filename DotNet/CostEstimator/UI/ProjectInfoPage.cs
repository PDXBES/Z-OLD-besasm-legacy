// Project: UI, File: ProjectInfoPage.cs
// Namespace: CostEstimator.UI, Class: ProjectInfoPage
// Path: C:\Development\DotNet\CostEstimator\UI, Author: Arnel
// Code lines: 35, Size of file: 602 Bytes
// Creation date: 4/28/2008 6:26 AM
// Last modified: 7/10/2008 12:37 PM

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
  public partial class ProjectInfoPage : CostEstimator.UI.ChildFormTemplate
  {
    #region Variables
    private Project _project;
    #endregion

    #region Constructors
    public ProjectInfoPage()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Costs page
    /// </summary>
    /// <param name="project">Project</param>
    public ProjectInfoPage(Project project)
      : this()
    {
      _project = project;
      edtProjectTitle.Text = _project.ProjectInfo.ProjectTitle;
      edtProjectNumber.Text = _project.ProjectInfo.ProjectNumber;
      edtProjectDescription.Text = _project.ProjectInfo.ProjectDescription;
      edtProjectManager.Text = _project.ProjectInfo.ProjectManager;
      edtCostEstimator.Text = _project.ProjectInfo.CostEstimator;
      cmbDate.Value = _project.ProjectInfo.PreparedDate;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Exit page
    /// </summary>
    private void ExitPage()
    {
      AppForm.ViewCosts();
    } // ExitPage()

    /// <summary>
    /// Save project info
    /// </summary>
    private void SaveProjectInfo()
    {
      _project.ProjectInfo.ProjectTitle = edtProjectTitle.Text;
      _project.ProjectInfo.ProjectNumber = edtProjectNumber.Text;
      _project.ProjectInfo.ProjectDescription = edtProjectDescription.Text;
      _project.ProjectInfo.ProjectManager = edtProjectManager.Text;
      _project.ProjectInfo.CostEstimator = edtCostEstimator.Text;
      _project.ProjectInfo.PreparedDate = (DateTime)cmbDate.Value;
    } // SaveProjectInfo()
    #endregion

    private void ProjectInfoPage_VisibleChanged(object sender, EventArgs e)
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
      }
    }

    private void toolbarManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
    {
      switch (e.Tool.Key)
      {
        case "Exit": // ButtonTool
          SaveProjectInfo();
          ExitPage();
          break;
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      SaveProjectInfo();
      ExitPage();
    }
  }
}

