using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using SystemsAnalysis.Utils.StatusTextBox;

namespace SystemsAnalysis.Utils.StatusTextBox
{
    /// <summary>
    /// Defines the severity of the message being added to the StatusTextBox.
    /// Info: Displays in Black on one line, prepended with the date
    /// Update: Displays in Black, with no line break
    /// Warning: Displays in Orange on one line, prepended with the date
    /// Error: Displays in Red on one line, prepended with the date
    /// </summary>
    public enum SeverityLevel
    {
        Info,
        Update,
        Attention,
        Warning,
        Error
    }
    /// <summary>
    /// Provides a text box for outputing status information. Provides functionality
    /// of the System.Windows.Forms.RichTextBox with some additional methods to
    /// control the output based on the severity of the message.
    /// </summary>
    public class StatusTextBox : System.Windows.Forms.RichTextBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Creates a new instance of the StatusTextBox
        /// </summary>
        public StatusTextBox()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitComponent call

        }

        private string currentStatus;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        /// <summary>
        /// Adds text to the StatusTextBox display.
        /// </summary>
        /// <param name="status">The string to be added to the display. The newline
        /// character will be automatically appended to the end of this string
        /// if it is not already present.</param>
        public void AddStatus(string status)
        {
            this.currentStatus = status;
            this.AppendText(System.DateTime.Now + ": ");
            this.AppendText(status);
            if (!status.EndsWith("\n"))
            {
                this.AppendText("\n");
            }
            Application.DoEvents();
            this.Focus();
            this.ScrollToCaret();
            this.Refresh();
            Application.DoEvents();
        }

        /// <summary>
        /// Adds text to the StatusTextBox display.
        /// </summary>
        /// <param name="status">The string to be added to the display. The newline
        /// character will be automatically appended to the end of this string
        /// if it is not already present, unless the severity is SeverityLevel.Update</param>
        /// <param name="severity">The SeverityLevel of the status message</param>
        public void AddStatus(string status, SeverityLevel severity)
        {
            switch (severity)
            {
                case SeverityLevel.Info:
                    this.SelectionColor = Color.Black;
                    this.AddStatus(status);
                    break;
                case SeverityLevel.Attention:
                    this.SelectionColor = Color.DarkGreen;
                    this.AddStatus(status);
                    break;
                case SeverityLevel.Warning:
                    this.SelectionColor = Color.DarkOrange;
                    this.AddStatus(status);
                    break;
                case SeverityLevel.Error:
                    this.SelectionColor = Color.Red;
                    this.AddStatus(status);
                    this.Show();
                    break;
            }
            this.SelectionColor = Color.Black;
        }

        /// <summary>
        /// Adds text to the Status Form display.
        /// </summary>
        /// <param name="status">The text to add to the StatusTextBox. Continues
        /// the previous line.</param>		
        public void AddUpdate(string status)
        {
            this.currentStatus += status;
            if (this.Text.EndsWith("\n"))
            {
                this.AppendText(System.DateTime.Now + ": ");
            }
            this.AppendText(status);
            this.Refresh();
            Application.DoEvents();
            return;
        }
        /// <summary>
        /// Adds text to the Status Form display.
        /// </summary>
        /// <param name="status">The text to add to the StatusTextBox. Continues
        /// the previous line.</param>		
        public void AddUpdate(string status, SeverityLevel severity)
        {
            switch (severity)
            {
                case SeverityLevel.Info:
                    this.SelectionColor = Color.Black;
                    this.AddUpdate(status);
                    break;
                case SeverityLevel.Attention:
                    this.SelectionColor = Color.DarkGreen;
                    this.AddUpdate(status);
                    break;
                case SeverityLevel.Warning:
                    this.SelectionColor = Color.DarkOrange;
                    this.AddUpdate(status);
                    break;
                case SeverityLevel.Error:
                    this.SelectionColor = Color.Red;
                    this.AddUpdate(status);
                    this.Show();
                    break;
            }
            return;
        }
        /// <summary>
        /// Clears the StatusTextBox display.
        /// </summary>
        public void ClearStatusText()
        {
            this.currentStatus = "";
            this.Clear();
        }

        public void ExportStatusText(string path)
        {
            this.SaveFile(path, RichTextBoxStreamType.UnicodePlainText);
        }

        public string CurrentStatus
        {
            get { return this.currentStatus; }
        }



    }
}
