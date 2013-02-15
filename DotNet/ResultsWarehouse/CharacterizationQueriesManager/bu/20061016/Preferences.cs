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
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button btnSaveSettings;
		private System.Windows.Forms.Button btnLoadSettings;

		private string settingsFile;
		private SystemsAnalysis.Characterization.Settings prefDataSet;
		private System.Windows.Forms.Button btnApplySettings;
		private System.Windows.Forms.Button btnCancel;

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
			SystemsAnalysis.Characterization.Settings prefDataSet;
			this.prefDataSet = new SystemsAnalysis.Characterization.Settings();

			this.settingsFile = settingsFile;
			this.Text = "Preferences - " + this.settingsFile;

			// Read the XML document back in. 
			// Create new FileStream to read schema with.
			System.IO.FileStream fsReadXml = new System.IO.FileStream
				(settingsFile, System.IO.FileMode.Open);
			// Create an XmlTextReader to read the file.
			System.Xml.XmlTextReader xmlReader = 
				new System.Xml.XmlTextReader(fsReadXml);
			// Read the XML document into the DataSet.
			this.prefDataSet.ReadXml(xmlReader);
			// Close the XmlTextReader
			xmlReader.Close();

			this.dataGrid1.DataSource = this.prefDataSet.DataSource;			

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
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnApplySettings = new System.Windows.Forms.Button();
			this.btnLoadSettings = new System.Windows.Forms.Button();
			this.btnSaveSettings = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.AllowNavigation = false;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.FlatMode = true;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 0);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ParentRowsVisible = false;
			this.dataGrid1.Size = new System.Drawing.Size(800, 265);
			this.dataGrid1.TabIndex = 0;
			this.dataGrid1.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								  this.dataGridTableStyle1});
			this.dataGrid1.Navigate += new System.Windows.Forms.NavigateEventHandler(this.dataGrid1_Navigate);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = this.dataGrid1;
			this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.dataGridTextBoxColumn1,
																												  this.dataGridTextBoxColumn2,
																												  this.dataGridTextBoxColumn3});
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "DataSource";
			this.dataGridTableStyle1.RowHeadersVisible = false;
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.HeaderText = "Property";
			this.dataGridTextBoxColumn1.MappingName = "Name";
			this.dataGridTextBoxColumn1.Width = 125;
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.HeaderText = "Type";
			this.dataGridTextBoxColumn2.MappingName = "Type";
			this.dataGridTextBoxColumn2.Width = 75;
			// 
			// dataGridTextBoxColumn3
			// 
			this.dataGridTextBoxColumn3.Format = "";
			this.dataGridTextBoxColumn3.FormatInfo = null;
			this.dataGridTextBoxColumn3.HeaderText = "Settings";
			this.dataGridTextBoxColumn3.MappingName = "DataSource_Text";
			this.dataGridTextBoxColumn3.Width = 600;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnApplySettings);
			this.panel1.Controls.Add(this.btnLoadSettings);
			this.panel1.Controls.Add(this.btnSaveSettings);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 225);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(800, 40);
			this.panel1.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(472, 8);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnApplySettings
			// 
			this.btnApplySettings.Location = new System.Drawing.Point(392, 8);
			this.btnApplySettings.Name = "btnApplySettings";
			this.btnApplySettings.TabIndex = 2;
			this.btnApplySettings.Text = "Apply";
			this.btnApplySettings.Click += new System.EventHandler(this.btnApplySettings_Click);
			// 
			// btnLoadSettings
			// 
			this.btnLoadSettings.Location = new System.Drawing.Point(720, 8);
			this.btnLoadSettings.Name = "btnLoadSettings";
			this.btnLoadSettings.Size = new System.Drawing.Size(72, 23);
			this.btnLoadSettings.TabIndex = 1;
			this.btnLoadSettings.Text = "Load";
			this.btnLoadSettings.Click += new System.EventHandler(this.btnLoadSettings_Click);
			// 
			// btnSaveSettings
			// 
			this.btnSaveSettings.Location = new System.Drawing.Point(640, 8);
			this.btnSaveSettings.Name = "btnSaveSettings";
			this.btnSaveSettings.Size = new System.Drawing.Size(72, 23);
			this.btnSaveSettings.TabIndex = 0;
			this.btnSaveSettings.Text = "Save";
			this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "xml";
			this.openFileDialog1.Filter = "XML File (*.xml)|*.xml|All Files (*.*)|*.*";
			this.openFileDialog1.InitialDirectory = "\\\\Cassio\\Modeling\\Model_Programs\\ResultsWarehouse\\CharacterizationQueriesManager";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "xml";
			this.saveFileDialog1.Filter = "XML File (*.xml)|*.xml|All Files (*.*)|*.*";
			this.saveFileDialog1.InitialDirectory = "\\\\Cassio\\Modeling\\Model_Programs\\ResultsWarehouse\\CharacterizationQueriesManager";
			this.saveFileDialog1.Title = "Save preferences as XML";
			// 
			// Preferences
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(800, 265);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.dataGrid1);
			this.Name = "Preferences";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Preferences";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnSaveSettings_Click(object sender, System.EventArgs e)
		{			
		
			if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{																						  
				settingsFile = this.saveFileDialog1.FileName;
				this.prefDataSet.WriteXml(settingsFile);
			}
			this.Text = "Preferences - " + this.settingsFile;
								
		}

		private void btnLoadSettings_Click(object sender, System.EventArgs e)
		{			
			if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				settingsFile = this.openFileDialog1.FileName;

				// Read the XML document back in. 
				// Create new FileStream to read schema with.
				System.IO.FileStream fsReadXml = new System.IO.FileStream
					(settingsFile, System.IO.FileMode.Open);
				// Create an XmlTextReader to read the file.
				System.Xml.XmlTextReader xmlReader = 
					new System.Xml.XmlTextReader(fsReadXml);
				// Read the XML document into the DataSet.
				prefDataSet.Clear();
				prefDataSet.ReadXml(xmlReader);
				// Close the XmlTextReader
				xmlReader.Close();
			}
			this.Text = "Preferences - " + this.settingsFile;
		}

		private void btnApplySettings_Click(object sender, System.EventArgs e)
		{
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void dataGrid1_Navigate(object sender, System.Windows.Forms.NavigateEventArgs ne)
		{
		
		}

		public Settings Settings
		{
			get { return this.prefDataSet; }
		}

	
	
	}
}
