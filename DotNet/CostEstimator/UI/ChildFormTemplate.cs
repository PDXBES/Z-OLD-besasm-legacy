// Project: UI, File: ChildFormTemplate.cs
// Namespace: PWMPDataSystem, Class: ChildFormTemplate
// Path: C:\Development\PWMPDataSystem\UI, Author: Arnel
// Code lines: 22, Size of file: 426 Bytes
// Creation date: 11/2/2007 8:11 PM
// Last modified: 11/21/2007 1:06 PM
// Generated with Commenter by abi.exDream.com

#region Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
  /// <summary>
  /// Child form template
  /// </summary>
  /// <returns>Form</returns>
  public partial class ChildFormTemplate : Form
  {
    private Main _AppForm;

    public ChildFormTemplate()
      : base()
    {
      SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.BackColor = Color.Transparent;

      InitializeComponent();
    } // ChildFormTemplate()

    /// <summary>
    /// Initialize
    /// </summary>
    /// <param name="parentControl">Parent control</param>
    /// <param name="mainForm">Main form</param>
    public virtual void Initialize(Control parentControl, Main mainForm)
    {
      SetParent(parentControl);
      SetAppForm(mainForm);
    } // Initialize(parentControl, mainForm)

    /// <summary>
    /// Set parent
    /// </summary>
    /// <param name="parentControl">Parent control</param>
    public void SetParent(Control parentControl)
    {
      TopLevel = false;
      parentControl.Controls.Add(this);
      Dock = DockStyle.Fill;
      Show();
    } // class ChildFormTemplate

    /// <summary>
    /// Set app form
    /// </summary>
    /// <param name="mainForm">Main form</param>
    public void SetAppForm(Main mainForm)
    {
      _AppForm = mainForm;
    } // SetAppForm(mainForm)

    /// <summary>
    /// App form
    /// </summary>
    /// <returns>Main</returns>
    public Main AppForm
    {
      get
      {
        return _AppForm;
      } // get
    } // AppForm

  }
}
