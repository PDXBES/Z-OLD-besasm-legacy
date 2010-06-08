using ESRI.ArcGIS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.Utils.Events;

namespace SystemsAnalysis.Reporting
{
  public partial class StatusDisplayControl : UserControl
  {
    delegate void SetTextCallback(string status);
    delegate void SetTextColorCallback(Color color);

    public void AppendTextThreadSafe(string text)
    {
      if (this.statusTextBox.InvokeRequired)
      {
        SetTextCallback d = new SetTextCallback(AppendTextThreadSafe);
        this.Invoke(d, new object[] { text });
      }
      else
      {        
        this.statusTextBox.AppendText(text);
        this.statusTextBox.Focus();
        this.statusTextBox.ScrollToCaret();        
      }
    }
    public void SetColorThreadSafe(Color color)
    {
      if (this.statusTextBox.InvokeRequired)
      {
        SetTextColorCallback d = new SetTextColorCallback(SetColorThreadSafe);
        this.Invoke(d, new object[] { color });
      }
      else
      {
        this.statusTextBox.SelectionColor = color;
      }
    }
    



    public StatusDisplayControl()
    {
      InitializeComponent();
    }

    public void AddInfoStatus(StatusChangedArgs e)
    {
      switch (e.NewStatusType)
      {
        case StatusChangeType.Info:
          AddInfoStatus(e.NewStatus);
          break;
        case StatusChangeType.Warning:
          AddWarningStatus(e.NewStatus);
          break;
        case StatusChangeType.Error:
          AddErrorStatus(e.NewStatus);
          break;
        default:
          AddInfoStatus(e.NewStatus);
          break;
      }
    }
    /// <summary>
    /// Adds text to the Status Form display.
    /// </summary>
    /// <param name="status">The string to be added to the display. The newline
    /// character will be automatically appended to the end of this string
    /// if it is not already present.</param>
    public void AddInfoStatus(string status)
    {
      SetColorThreadSafe(Color.Black);
      AppendTextThreadSafe(System.DateTime.Now + ": ");
      AppendTextThreadSafe(status);
      if (!status.EndsWith("\n"))
      {
        AppendTextThreadSafe("\n");
      }
      //Application.DoEvents();
      //this.Refresh();
      //Application.DoEvents();
    }
    /// <summary>
    /// Adds warning text to the Status Form display.
    /// </summary>
    /// <param name="status">The warning message to add.</param>
    public void AddWarningStatus(string status)
    {
      SetColorThreadSafe(Color.Orange);
      this.AddInfoStatus(status);
    }
    /// <summary>
    /// Adds error text to the Status Form display.
    /// </summary>
    /// <param name="status">The error message to add.</param>
    public void AddErrorStatus(string status)
    {
      SetColorThreadSafe(Color.Red);
      this.AddInfoStatus(status);            
    }
    /// <summary>
    /// Adds text to the Status Form display.
    /// </summary>
    /// <param name="status">The string to be added to the display. The newline
    /// character will be automatically appended to the end of this string
    /// if it is not already present.</param>
    /// <param name="newLine">Specifies whether a line break should be appended
    /// to the end of the status text.</param>
    public void AddInfoStatus(string status, bool newLine)
    {
      if (newLine)
      {
        this.AddInfoStatus(status);
      }
      else
      {
        AppendTextThreadSafe(status);
        this.Refresh();
        //Application.DoEvents();
      }
      return;
    }
    /// <summary>
    /// Clears the Status Form display.
    /// </summary>
    public void ClearStatusText()
    {
      this.statusTextBox.Clear();
    }

    private void StatusDisplayForm_Load(object sender, EventArgs e)
    {

    }

    private void StatusDisplayForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason == CloseReason.UserClosing)
      {
        e.Cancel = true;
        this.Hide();
        return;
      }
    }
  }
}
