using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace SystemsAnalysis.Tracer
{
	/// <summary>
	/// Summary description for TracerGUI.
	/// </summary>
	public class TracerGUI : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox cmbFeatureLayer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cmbUpstreamNodeField;
		private System.Windows.Forms.ComboBox cmbDownstreamNodeField;
		private System.Windows.Forms.ListBox lstStartPipes;
		private System.Windows.Forms.ListBox lstStopPipes;
		private System.Windows.Forms.Button btnRemoveStart;
		private System.Windows.Forms.Button btnRemoveStop;
		private System.Windows.Forms.Button btnSaveChanges;
		private System.Windows.Forms.Button btnCancel;
		
		private IFeatureLayer traceFL;
		public string USField;
		public string DSField;
		private IGraphEdges startEdges;
		private IGraphEdges stopEdges;
		private bool hasDirtyTrace;
		private bool hasDirtyNetwork;
		private System.Windows.Forms.CheckBox checkBox1;
		
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TracerGUI()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			this.USField = "";
			this.DSField = "";
			this.hasDirtyTrace = false;
			this.hasDirtyNetwork = false;
			this.startEdges = new GraphEdges();
			this.stopEdges = new GraphEdges();
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
			this.cmbFeatureLayer = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbUpstreamNodeField = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbDownstreamNodeField = new System.Windows.Forms.ComboBox();
			this.lstStartPipes = new System.Windows.Forms.ListBox();
			this.lstStopPipes = new System.Windows.Forms.ListBox();
			this.btnRemoveStart = new System.Windows.Forms.Button();
			this.btnRemoveStop = new System.Windows.Forms.Button();
			this.btnSaveChanges = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// cmbFeatureLayer
			// 
			this.cmbFeatureLayer.Location = new System.Drawing.Point(8, 24);
			this.cmbFeatureLayer.Name = "cmbFeatureLayer";
			this.cmbFeatureLayer.Size = new System.Drawing.Size(264, 21);
			this.cmbFeatureLayer.Sorted = true;
			this.cmbFeatureLayer.TabIndex = 0;
			this.cmbFeatureLayer.SelectedIndexChanged += new System.EventHandler(this.cmbFeatureLayer_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select a Polyline Feature Class:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Select the Upstream Node Field:";
			// 
			// cmbUpstreamNodeField
			// 
			this.cmbUpstreamNodeField.Location = new System.Drawing.Point(8, 64);
			this.cmbUpstreamNodeField.Name = "cmbUpstreamNodeField";
			this.cmbUpstreamNodeField.Size = new System.Drawing.Size(264, 21);
			this.cmbUpstreamNodeField.Sorted = true;
			this.cmbUpstreamNodeField.TabIndex = 2;
			this.cmbUpstreamNodeField.SelectedIndexChanged += new System.EventHandler(this.cmbUpstreamNodeField_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(208, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Select the Downstream Node Field:";
			// 
			// cmbDownstreamNodeField
			// 
			this.cmbDownstreamNodeField.Location = new System.Drawing.Point(8, 104);
			this.cmbDownstreamNodeField.Name = "cmbDownstreamNodeField";
			this.cmbDownstreamNodeField.Size = new System.Drawing.Size(264, 21);
			this.cmbDownstreamNodeField.Sorted = true;
			this.cmbDownstreamNodeField.TabIndex = 4;
			this.cmbDownstreamNodeField.SelectedIndexChanged += new System.EventHandler(this.cmbDownstreamNodeField_SelectedIndexChanged);
			// 
			// lstStartPipes
			// 
			this.lstStartPipes.Location = new System.Drawing.Point(8, 152);
			this.lstStartPipes.Name = "lstStartPipes";
			this.lstStartPipes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstStartPipes.Size = new System.Drawing.Size(128, 82);
			this.lstStartPipes.TabIndex = 6;
			// 
			// lstStopPipes
			// 
			this.lstStopPipes.Location = new System.Drawing.Point(144, 152);
			this.lstStopPipes.Name = "lstStopPipes";
			this.lstStopPipes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstStopPipes.Size = new System.Drawing.Size(128, 82);
			this.lstStopPipes.TabIndex = 7;
			// 
			// btnRemoveStart
			// 
			this.btnRemoveStart.Location = new System.Drawing.Point(40, 240);
			this.btnRemoveStart.Name = "btnRemoveStart";
			this.btnRemoveStart.Size = new System.Drawing.Size(56, 23);
			this.btnRemoveStart.TabIndex = 9;
			this.btnRemoveStart.Text = "Remove";
			this.btnRemoveStart.Click += new System.EventHandler(this.btnRemoveStart_Click);
			// 
			// btnRemoveStop
			// 
			this.btnRemoveStop.Location = new System.Drawing.Point(176, 240);
			this.btnRemoveStop.Name = "btnRemoveStop";
			this.btnRemoveStop.Size = new System.Drawing.Size(56, 23);
			this.btnRemoveStop.TabIndex = 11;
			this.btnRemoveStop.Text = "Remove";
			this.btnRemoveStop.Click += new System.EventHandler(this.btnRemoveStop_Click);
			// 
			// btnSaveChanges
			// 
			this.btnSaveChanges.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSaveChanges.Location = new System.Drawing.Point(112, 272);
			this.btnSaveChanges.Name = "btnSaveChanges";
			this.btnSaveChanges.Size = new System.Drawing.Size(72, 23);
			this.btnSaveChanges.TabIndex = 12;
			this.btnSaveChanges.Text = "OK";
			this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(192, 272);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 13;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 16);
			this.label4.TabIndex = 14;
			this.label4.Text = "Start Pipes";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(144, 136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 15;
			this.label5.Text = "Stop Pipes";
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(8, 272);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.TabIndex = 16;
			this.checkBox1.Text = "Zoom to Trace?";
			// 
			// TracerGUI
			// 
			this.AcceptButton = this.btnSaveChanges;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(280, 309);
			this.ControlBox = false;
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSaveChanges);
			this.Controls.Add(this.btnRemoveStop);
			this.Controls.Add(this.btnRemoveStart);
			this.Controls.Add(this.lstStopPipes);
			this.Controls.Add(this.lstStartPipes);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmbDownstreamNodeField);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbUpstreamNodeField);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbFeatureLayer);
			this.Name = "TracerGUI";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "TracerGUI";
			this.ResumeLayout(false);

		}
		#endregion

		public void SetupForm(IEnumDataset enumDataset, IGraphEdges startEdges, IGraphEdges stopEdges)
		{								
			this.startEdges = new GraphEdges();
			this.stopEdges = new GraphEdges();
			
			//When re-entering the settings form, we do not know for sure if the
			//previously selected FeatureClass still exists in the document. However,
			//if it does it is neccesary to remember it.
			IFeatureLayer flHolder = this.traceFL;			
			
			this.traceFL = null;
			this.hasDirtyNetwork = true;

			this.cmbFeatureLayer.SelectedItem = null;
			this.cmbFeatureLayer.Items.Clear();			
			
			//Add all polyline FeatureClasses in the document to the FeatureClass
			//drop-down list.
			IDataset ds;
			ds = enumDataset.Next();
			while (ds != null)
			{	
				try
				{
					if (ds.Type == esriDatasetType.esriDTFeatureClass)
					{												
						IFeatureLayer fl;					
						fl = (IFeatureLayer)ds;											

						IFeatureClass fc;
						fc = fl.FeatureClass;	
															
						if (fc.ShapeType == esriGeometryType.esriGeometryPolyline)
						{											
							this.cmbFeatureLayer.Items.Add(new FeatureLayerWrapper(fl));						
						}
					}				
				}
				catch
				{
				}
				finally
				{
					ds = enumDataset.Next();
				}
			}

			//If possible, overwrite the combobox selection with the 
			//FeatureLayer that was selected previously
			try
			{				
				foreach (FeatureLayerWrapper flw in this.cmbFeatureLayer.Items)
				{
					if (flw.FL == flHolder) 
					{	
						this.cmbFeatureLayer.SelectedItem = flw;
						foreach (IGraphEdge g in startEdges)
						{
							this.startEdges.Add(new GraphEdge(g.EdgeID, g.SourceNode, g.SinkNode));
							this.lstStartPipes.Items.Add(g.ToString());
						}
						foreach (IGraphEdge g in stopEdges)
						{
							this.stopEdges.Add(new GraphEdge(g.EdgeID, g.SourceNode, g.SinkNode));
							this.lstStopPipes.Items.Add(g.ToString());
						}
						this.traceFL = flHolder;
						this.hasDirtyNetwork = false;
						break;
					}
				}	
				//If a Network was built previously, but the FeatureClass that formed that Network
				//has been removed from the document then throw an exception.
				if (this.traceFL == null) 
				{ throw new Exception("FeatureClass of previous Network no longer exists in document."); }
			}
			catch
			{
				//Select the first item in the list if the list contains any items
				if (this.cmbFeatureLayer.Items.Count > 0)
				{
					this.cmbFeatureLayer.SelectedIndex = 0;
				}
			}
			
			this.lstStartPipes.Items.Clear();
			foreach (GraphEdge g in this.startEdges)
			{
				this.lstStartPipes.Items.Add(g);
			}
			this.lstStopPipes.Items.Clear();
			foreach (GraphEdge g in this.stopEdges)
			{
				this.lstStopPipes.Items.Add(g);
			}	

			//After a new call to setup, the Trace is not dirty until the Start/Stop
			//links are changed.
			this.hasDirtyTrace = false;					
			
			return;
		}

		private void cmbFeatureLayer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.hasDirtyTrace = true;
			this.hasDirtyNetwork = true;

			this.cmbUpstreamNodeField.Items.Clear();
			this.cmbDownstreamNodeField.Items.Clear();

			if (this.cmbFeatureLayer.SelectedItem == null) 
			{ return; }
			
			FeatureLayerWrapper flw = (FeatureLayerWrapper)this.cmbFeatureLayer.SelectedItem;
			IFeatureClass fc = flw.FC;

			//The selected FeatureLayer has changed, so the old start/stop links
			//are no longer valid. If the user clicks "Cancel", the previously
			//selected start/stop links are preserved in TracerExtension.			
			this.startEdges = new GraphEdges();
			this.stopEdges = new GraphEdges();
			this.lstStartPipes.Items.Clear();
			this.lstStopPipes.Items.Clear();

			//Add all fields of the currently selected FeatureLayer to both
			//up- and downstream field selectors. Up- and downstream node
			//fields are required to be strings.
			IFields fields = fc.Fields;
			for (int i = 0; i < fields.FieldCount; i++)
			{
				IField field = fields.get_Field(i);
				if (field.Type == esriFieldType.esriFieldTypeString)
				{
					this.cmbUpstreamNodeField.Items.Add(field.Name);
					this.cmbDownstreamNodeField.Items.Add(field.Name);
				}
			}
			
			if (this.cmbUpstreamNodeField.Items.Contains(this.USField))
			{
				this.cmbUpstreamNodeField.SelectedItem = this.USField;
			}
			//90% of the time, the UpstreamNodeField will be named "USNode"
			else if (this.cmbUpstreamNodeField.Items.Contains("USNode"))
			{
				this.cmbUpstreamNodeField.SelectedItem = "USNode";
			}
			//Probably a better way to do this, but SDE data uses all caps.
			else if (this.cmbUpstreamNodeField.Items.Contains("USNODE"))
			{
				this.cmbUpstreamNodeField.SelectedItem = "USNODE";
			}
			else if (this.cmbUpstreamNodeField.Items.Count > 0)
			{ 
				this.cmbUpstreamNodeField.SelectedIndex = 0;
			}

			if (this.cmbDownstreamNodeField.Items.Contains(this.DSField))
			{
				this.cmbDownstreamNodeField.SelectedItem = this.DSField;
			}
			else if (this.cmbDownstreamNodeField.Items.Contains("DSNode"))
			{
				this.cmbDownstreamNodeField.SelectedItem = "DSNode";
			}
			else if (this.cmbDownstreamNodeField.Items.Contains("DSNODE"))
			{
				this.cmbDownstreamNodeField.SelectedItem = "DSNODE";
			}
			else if (this.cmbDownstreamNodeField.Items.Count > 0)
			{ 
				this.cmbDownstreamNodeField.SelectedIndex = 0;
			}
		}
		
		private void btnSaveChanges_Click(object sender, System.EventArgs e)
		{
			try
			{									
				FeatureLayerWrapper flw = (FeatureLayerWrapper)this.cmbFeatureLayer.SelectedItem;				
				this.traceFL = flw.FL;				
				this.USField = (string)this.cmbUpstreamNodeField.SelectedItem;
				this.DSField = (string)this.cmbDownstreamNodeField.SelectedItem;
				this.DialogResult = DialogResult.OK;				
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not save settings: " + ex.ToString(), "Error saving settings");
				this.hasDirtyNetwork = false;
				this.hasDirtyTrace = false;
				this.DialogResult = DialogResult.Abort;				
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{			
			this.DialogResult = DialogResult.Cancel;		
		}
		
		private void btnRemoveStart_Click(object sender, System.EventArgs e)
		{
			//Remove all items selected in start pipe ListBox from the startEdges collection
			foreach (GraphEdge g in this.lstStartPipes.SelectedItems)
			{
				this.startEdges.Remove(g);				
				this.hasDirtyTrace = true;
			}	
			//Repopulate the start pipe ListBox from the new startEdges collection
			this.lstStartPipes.Items.Clear();
			foreach (GraphEdge g in this.startEdges)
			{
				this.lstStartPipes.Items.Add(g);
			}						
		}

		private void btnRemoveStop_Click(object sender, System.EventArgs e)
		{	
			//Remove all items selected in stop pipe ListBox from the stopEdges collection
			foreach (GraphEdge g in this.lstStopPipes.SelectedItems)
			{
				this.stopEdges.Remove(g);	
				this.hasDirtyTrace = true;
			}	
			//Repopulate the stop pipe ListBox from the new stopEdges collection
			this.lstStopPipes.Items.Clear();
			foreach (GraphEdge g in this.stopEdges)
			{
				this.lstStopPipes.Items.Add(g);				
			}			
		}


		private void cmbUpstreamNodeField_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.lstStartPipes.Items.Clear();
			this.lstStopPipes.Items.Clear();
			foreach (GraphEdge g in this.startEdges)
			{
				this.startEdges.Remove(g);
			}
			foreach (GraphEdge g in this.stopEdges)
			{
				this.stopEdges.Remove(g);
			}	
			this.hasDirtyTrace = true;
			this.hasDirtyNetwork = true;
		}

		private void cmbDownstreamNodeField_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.lstStartPipes.Items.Clear();
			this.lstStopPipes.Items.Clear();
			foreach (GraphEdge g in this.startEdges)
			{
				this.startEdges.Remove(g);
			}
			foreach (GraphEdge g in this.stopEdges)
			{
				this.stopEdges.Remove(g);
			}
			this.hasDirtyTrace = true;
		}
	
		public bool DirtyTrace
		{
			get { return this.hasDirtyTrace; }
		}
		public bool DirtyNetwork
		{
			get { return this.hasDirtyNetwork; }
		}

		public IGraphEdges StartEdges
		{
			get { return this.startEdges; }
		}
		public IGraphEdges StopEdges
		{
			get { return this.stopEdges; }
		}

		public IFeatureLayer TraceFL
		{
			get { return this.traceFL; }
		}
		public bool ZoomToTrace
		{
			get { return this.checkBox1.Checked; }
		}

		/// <summary>
		/// Provides a wrapper around IFeatureClass that allows us to add
		/// IFeatureClass to a ListBox and retrieve a meaningful ToString(),
		/// since ESRI's ToString implementation is shit (ie returns 
		/// "COM__OBJECT" for all IFeatureClass objects.)
		/// </summary>
		protected class FeatureLayerWrapper
		{
			private IFeatureClass fc;
			private IFeatureLayer fl;
			private string description;
			public FeatureLayerWrapper(IFeatureLayer fl)
			{
				this.fl = fl;
				this.fc = fl.FeatureClass;				
				string name = fl.Name;
				int dotLocation = name.LastIndexOf(".");
				if (dotLocation == -1)
				{
					this.description = fl.Name;
				}
				else
				{
					this.description = name.Substring(dotLocation + 1);
				}
			}
			public override string ToString()
			{
				return this.description;
			}
			public IFeatureClass FC
			{
				get { return this.fc; }
			}
			public IFeatureLayer FL
			{
				get { return this.fl; }
			}
		
		}
	}
}
