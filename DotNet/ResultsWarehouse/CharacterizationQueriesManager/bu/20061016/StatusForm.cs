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
	public class StatusTextBox : System.Windows.Forms.RichTextBox
	{
		private System.Windows.Forms.RichTextBox statusTextBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>

		/// <summary>
		/// A Windows Form for logging status information and providing real-time
		/// feedback of program status during log execution blocks.
		/// </summary>
		public StatusTextBox()
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

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatusForm));
			this.statusTextBox = new System.Windows.Forms.RichTextBox();

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

		private void StatusForm_Load(object sender, System.EventArgs e)
		{
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}
	}
}
