namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	partial class SelectAlternativePage
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint4 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint2 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint3 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint5 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint6 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint7 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint8 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint1 = new Infragistics.Win.Layout.GridBagConstraint();
			this.ultraGridBagLayoutPanel1 = new Infragistics.Win.Misc.UltraGridBagLayoutPanel();
			this.pnlButtons = new Infragistics.Win.Misc.UltraGridBagLayoutPanel();
			this.btnOK = new Infragistics.Win.Misc.UltraButton();
			this.btnCancel = new Infragistics.Win.Misc.UltraButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ultraListView1 = new Infragistics.Win.UltraWinListView.UltraListView();
			this.ultraGridBagLayoutPanel2 = new Infragistics.Win.Misc.UltraGridBagLayoutPanel();
			this.ultraListView2 = new Infragistics.Win.UltraWinListView.UltraListView();
			this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
			this.navDirs = new Infragistics.Win.Misc.UltraNavigationBar();
			this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel1)).BeginInit();
			this.ultraGridBagLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).BeginInit();
			this.pnlButtons.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraListView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel2)).BeginInit();
			this.ultraGridBagLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraListView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.navDirs)).BeginInit();
			this.SuspendLayout();
			// 
			// ultraGridBagLayoutPanel1
			// 
			this.ultraGridBagLayoutPanel1.Controls.Add(this.ultraLabel1);
			this.ultraGridBagLayoutPanel1.Controls.Add(this.pnlButtons);
			this.ultraGridBagLayoutPanel1.Controls.Add(this.splitContainer1);
			this.ultraGridBagLayoutPanel1.Controls.Add(this.navDirs);
			this.ultraGridBagLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ultraGridBagLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.ultraGridBagLayoutPanel1.Name = "ultraGridBagLayoutPanel1";
			this.ultraGridBagLayoutPanel1.Size = new System.Drawing.Size(933, 692);
			this.ultraGridBagLayoutPanel1.TabIndex = 0;
			// 
			// pnlButtons
			// 
			this.pnlButtons.Controls.Add(this.btnOK);
			this.pnlButtons.Controls.Add(this.btnCancel);
			gridBagConstraint4.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint4.OriginX = 0;
			gridBagConstraint4.OriginY = 3;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.pnlButtons, gridBagConstraint4);
			this.pnlButtons.Location = new System.Drawing.Point(0, 652);
			this.pnlButtons.Name = "pnlButtons";
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.pnlButtons, new System.Drawing.Size(200, 40));
			this.pnlButtons.Size = new System.Drawing.Size(933, 40);
			this.pnlButtons.TabIndex = 3;
			// 
			// btnOK
			// 
			gridBagConstraint2.Anchor = Infragistics.Win.Layout.AnchorType.Right;
			gridBagConstraint2.Insets.Right = 8;
			gridBagConstraint2.OriginX = 0;
			gridBagConstraint2.OriginY = 0;
			gridBagConstraint2.WeightX = 1F;
			this.pnlButtons.SetGridBagConstraint(this.btnOK, gridBagConstraint2);
			this.btnOK.Location = new System.Drawing.Point(775, 8);
			this.btnOK.Name = "btnOK";
			this.pnlButtons.SetPreferredSize(this.btnOK, new System.Drawing.Size(75, 23));
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			// 
			// btnCancel
			// 
			gridBagConstraint3.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint3.OriginX = 1;
			gridBagConstraint3.OriginY = 0;
			this.pnlButtons.SetGridBagConstraint(this.btnCancel, gridBagConstraint3);
			this.btnCancel.Location = new System.Drawing.Point(858, 8);
			this.btnCancel.Name = "btnCancel";
			this.pnlButtons.SetPreferredSize(this.btnCancel, new System.Drawing.Size(75, 23));
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			// 
			// splitContainer1
			// 
			gridBagConstraint5.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint5.OriginX = 0;
			gridBagConstraint5.OriginY = 2;
			gridBagConstraint5.WeightX = 1F;
			gridBagConstraint5.WeightY = 1F;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.splitContainer1, gridBagConstraint5);
			this.splitContainer1.Location = new System.Drawing.Point(0, 59);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ultraListView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ultraGridBagLayoutPanel2);
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.splitContainer1, new System.Drawing.Size(150, 100));
			this.splitContainer1.Size = new System.Drawing.Size(933, 593);
			this.splitContainer1.SplitterDistance = 311;
			this.splitContainer1.TabIndex = 2;
			// 
			// ultraListView1
			// 
			this.ultraListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ultraListView1.ItemSettings.DefaultImage = global::SystemsAnalysis.Analysis.CostEstimator.UI.Properties.Resources.AlternativeFolder;
			this.ultraListView1.Location = new System.Drawing.Point(0, 0);
			this.ultraListView1.Name = "ultraListView1";
			this.ultraListView1.Size = new System.Drawing.Size(311, 593);
			this.ultraListView1.TabIndex = 0;
			this.ultraListView1.Text = "ultraListView1";
			// 
			// ultraGridBagLayoutPanel2
			// 
			this.ultraGridBagLayoutPanel2.Controls.Add(this.ultraListView2);
			this.ultraGridBagLayoutPanel2.Controls.Add(this.ultraLabel2);
			this.ultraGridBagLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ultraGridBagLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.ultraGridBagLayoutPanel2.Name = "ultraGridBagLayoutPanel2";
			this.ultraGridBagLayoutPanel2.Size = new System.Drawing.Size(618, 593);
			this.ultraGridBagLayoutPanel2.TabIndex = 0;
			// 
			// ultraListView2
			// 
			gridBagConstraint6.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint6.OriginX = 0;
			gridBagConstraint6.OriginY = 1;
			gridBagConstraint6.WeightX = 1F;
			gridBagConstraint6.WeightY = 1F;
			this.ultraGridBagLayoutPanel2.SetGridBagConstraint(this.ultraListView2, gridBagConstraint6);
			this.ultraListView2.ItemSettings.DefaultImage = global::SystemsAnalysis.Analysis.CostEstimator.UI.Properties.Resources.AlternativeFolder;
			this.ultraListView2.Location = new System.Drawing.Point(0, 23);
			this.ultraListView2.Name = "ultraListView2";
			this.ultraGridBagLayoutPanel2.SetPreferredSize(this.ultraListView2, new System.Drawing.Size(121, 97));
			this.ultraListView2.Size = new System.Drawing.Size(618, 570);
			this.ultraListView2.TabIndex = 1;
			this.ultraListView2.Text = "ultraListView2";
			// 
			// ultraLabel2
			// 
			gridBagConstraint7.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint7.OriginX = 0;
			gridBagConstraint7.OriginY = 0;
			this.ultraGridBagLayoutPanel2.SetGridBagConstraint(this.ultraLabel2, gridBagConstraint7);
			this.ultraLabel2.Location = new System.Drawing.Point(0, 0);
			this.ultraLabel2.Name = "ultraLabel2";
			this.ultraGridBagLayoutPanel2.SetPreferredSize(this.ultraLabel2, new System.Drawing.Size(100, 23));
			this.ultraLabel2.Size = new System.Drawing.Size(618, 23);
			this.ultraLabel2.TabIndex = 0;
			this.ultraLabel2.Text = "Available Alternatives";
			// 
			// navDirs
			// 
			gridBagConstraint8.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint8.Insets.Bottom = 8;
			gridBagConstraint8.OriginX = 0;
			gridBagConstraint8.OriginY = 1;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.navDirs, gridBagConstraint8);
			this.navDirs.Location = new System.Drawing.Point(0, 23);
			this.navDirs.Name = "navDirs";
			this.navDirs.NonAutoSizeHeight = 24;
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.navDirs, new System.Drawing.Size(400, 28));
			this.navDirs.Size = new System.Drawing.Size(933, 28);
			this.navDirs.TabIndex = 1;
			this.navDirs.Text = "ultraNavigationBar1";
			// 
			// ultraLabel1
			// 
			gridBagConstraint1.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint1.OriginX = 0;
			gridBagConstraint1.OriginY = 0;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.ultraLabel1, gridBagConstraint1);
			this.ultraLabel1.Location = new System.Drawing.Point(0, 0);
			this.ultraLabel1.Name = "ultraLabel1";
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.ultraLabel1, new System.Drawing.Size(100, 23));
			this.ultraLabel1.Size = new System.Drawing.Size(933, 23);
			this.ultraLabel1.TabIndex = 4;
			this.ultraLabel1.Text = "Available Directories";
			// 
			// SelectAlternativePage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.ClientSize = new System.Drawing.Size(933, 692);
			this.Controls.Add(this.ultraGridBagLayoutPanel1);
			this.Name = "SelectAlternativePage";
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel1)).EndInit();
			this.ultraGridBagLayoutPanel1.ResumeLayout(false);
			this.ultraGridBagLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).EndInit();
			this.pnlButtons.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraListView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel2)).EndInit();
			this.ultraGridBagLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraListView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.navDirs)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Infragistics.Win.Misc.UltraGridBagLayoutPanel ultraGridBagLayoutPanel1;
		private Infragistics.Win.Misc.UltraNavigationBar navDirs;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Infragistics.Win.UltraWinListView.UltraListView ultraListView1;
		private Infragistics.Win.Misc.UltraGridBagLayoutPanel ultraGridBagLayoutPanel2;
		private Infragistics.Win.UltraWinListView.UltraListView ultraListView2;
		private Infragistics.Win.Misc.UltraLabel ultraLabel2;
		private Infragistics.Win.Misc.UltraGridBagLayoutPanel pnlButtons;
		private Infragistics.Win.Misc.UltraButton btnOK;
		private Infragistics.Win.Misc.UltraButton btnCancel;
		private Infragistics.Win.Misc.UltraLabel ultraLabel1;
	}
}
