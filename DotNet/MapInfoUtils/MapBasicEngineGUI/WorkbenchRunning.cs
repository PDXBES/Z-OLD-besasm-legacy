using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SystemsAnalysis.ModelConstruction.AlternativesBuilder
{
	/// <summary>
	/// Summary description for WorkbenchRunning.
	/// </summary>
	public class WorkbenchRunning : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;

		private bool workbenchRunning;
		private System.Windows.Forms.Button btnCancel;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WorkbenchRunning()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.workbenchRunning = workbenchRunning;
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
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(376, 96);
			this.label1.TabIndex = 0;
			this.label1.Text = @"The Alternative Workbench is running in a MapInfo Window.

When you are done editing your alternative, please save changes via MapInfo and close the MapInfo session.

Alternately, click 'Cancel' to force the Alternative Workbench to close (You will lose all unsaved changes and your alternative may become corrupt).";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(144, 104);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// WorkbenchRunning
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(386, 137);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "WorkbenchRunning";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Alternative Workbench Running";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		


	}
}
