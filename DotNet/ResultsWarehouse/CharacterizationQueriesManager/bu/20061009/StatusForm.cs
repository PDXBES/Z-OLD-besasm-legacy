using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SystemsAnalysis.Characterization
{
	/// <summary>
	/// Summary description for StatusForm.
	/// </summary>
	public class StatusForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox statusTextBox;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Panel panel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// A Windows Form for logging status information and providing real-time
		/// feedback of program status during log execution blocks.
		/// </summary>
		public StatusForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Adds text to the Status Form display.
		/// </summary>
		/// <param name="status">The string to be added to the display. The newline
		/// character will be automatically appended to the end of this string
		/// if it is not already present.</param>
		public void AddInfo(string status)
		{	
			this.statusTextBox.AppendText(System.DateTime.Now + ": ");
			this.statusTextBox.AppendText(status);
			if (!status.EndsWith("\n"))
			{
				this.statusTextBox.AppendText("\n");
			}
			Application.DoEvents();
			this.statusTextBox.Focus();
			this.statusTextBox.ScrollToCaret();
			this.Refresh();
			Application.DoEvents();
		}
		public void AddWarning(string status)
		{
			this.statusTextBox.SelectionColor = Color.Orange;
			this.AddInfo(status);
			this.statusTextBox.SelectionColor = Color.Black;
		}
		public void AddError(string status)
		{
			this.statusTextBox.SelectionColor = Color.Red;
			this.AddInfo(status);
			this.statusTextBox.SelectionColor = Color.Black;
			this.Show();
		}

		/// <summary>
		/// Adds text to the Status Form display.
		/// </summary>
		/// <param name="status">The string to be added to the display. The newline
		/// character will be automatically appended to the end of this string
		/// if it is not already present.</param>
		/// <param name="newLine">Specifies whether a line break should be appended
		/// to the end of the status text.</param>
		public void AddInfo(string status, bool newLine)
		{
			if (newLine)
			{
				this.AddInfo(status);
			}
			else
			{
				this.statusTextBox.AppendText(status);
				this.Refresh();
				Application.DoEvents();
			}
			return;
		}			
		/// <summary>
		/// Clears the Status Form display.
		/// </summary>
		public void ClearText()
		{
			this.statusTextBox.Clear();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatusForm));
			this.statusTextBox = new System.Windows.Forms.RichTextBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusTextBox
			// 
			this.statusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusTextBox.Location = new System.Drawing.Point(0, 0);
			this.statusTextBox.Name = "statusTextBox";
			this.statusTextBox.Size = new System.Drawing.Size(736, 197);
			this.statusTextBox.TabIndex = 0;
			this.statusTextBox.Text = "";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(656, 8);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 197);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(736, 32);
			this.panel1.TabIndex = 3;
			// 
			// StatusForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(736, 229);
			this.ControlBox = false;
			this.Controls.Add(this.statusTextBox);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "StatusForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Characterization Status";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.StatusForm_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void StatusForm_Load(object sender, System.EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}
	}
}
