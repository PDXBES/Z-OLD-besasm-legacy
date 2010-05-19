using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SystemsAnalysis.Characterization
{
	/// <summary>
	/// Summary description for Preferences.
	/// </summary>
	public class Preferences : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Panel panel1;

		private string settingsFile;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Preferences(string settingsFile)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			System.Data.DataSet prefDataSet;
			prefDataSet = new System.Data.DataSet("Program Settings");

			this.settingsFile = settingsFile;

			// Read the XML document back in. 
			// Create new FileStream to read schema with.
			System.IO.FileStream fsReadXml = new System.IO.FileStream
				(settingsFile, System.IO.FileMode.Open);
			// Create an XmlTextReader to read the file.
			System.Xml.XmlTextReader xmlReader = 
				new System.Xml.XmlTextReader(fsReadXml);
			// Read the XML document into the DataSet.
			prefDataSet.ReadXml(xmlReader);
			// Close the XmlTextReader
			xmlReader.Close();

			this.dataGrid1.DataSource = prefDataSet;

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
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.FlatMode = true;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 0);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(292, 265);
			this.dataGrid1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 165);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(292, 100);
			this.panel1.TabIndex = 1;
			// 
			// Preferences
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 265);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.dataGrid1);
			this.Name = "Preferences";
			this.Text = "Preferences";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

	
	}
}
