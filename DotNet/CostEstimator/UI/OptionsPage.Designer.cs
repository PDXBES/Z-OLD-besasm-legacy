namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	partial class OptionsPage
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
			this.components = new System.ComponentModel.Container();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint5 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint6 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint4 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint3 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint2 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint1 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			this.ultraGridBagLayoutPanel1 = new Infragistics.Win.Misc.UltraGridBagLayoutPanel();
			this.btnClose = new Infragistics.Win.Misc.UltraButton();
			this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			this.ultraGridBagLayoutManager1 = new Infragistics.Win.Misc.UltraGridBagLayoutManager(this.components);
			this.ultraGridBagLayoutPanel2 = new Infragistics.Win.Misc.UltraGridBagLayoutPanel();
			this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			this.ultraNumericEditor1 = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
			this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
			this.ultraTextEditor1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel1)).BeginInit();
			this.ultraGridBagLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
			this.ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel2)).BeginInit();
			this.ultraGridBagLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditor1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraTextEditor1)).BeginInit();
			this.SuspendLayout();
			// 
			// ultraTabPageControl1
			// 
			this.ultraTabPageControl1.Controls.Add(this.ultraGridBagLayoutPanel2);
			this.ultraTabPageControl1.Location = new System.Drawing.Point(9, 32);
			this.ultraTabPageControl1.Name = "ultraTabPageControl1";
			this.ultraTabPageControl1.Size = new System.Drawing.Size(915, 620);
			// 
			// ultraGridBagLayoutPanel1
			// 
			this.ultraGridBagLayoutPanel1.Controls.Add(this.btnClose);
			this.ultraGridBagLayoutPanel1.Controls.Add(this.ultraTabControl1);
			this.ultraGridBagLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ultraGridBagLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.ultraGridBagLayoutPanel1.Name = "ultraGridBagLayoutPanel1";
			this.ultraGridBagLayoutPanel1.Size = new System.Drawing.Size(933, 692);
			this.ultraGridBagLayoutPanel1.TabIndex = 0;
			// 
			// btnClose
			// 
			gridBagConstraint5.Anchor = Infragistics.Win.Layout.AnchorType.Right;
			gridBagConstraint5.OriginX = 0;
			gridBagConstraint5.OriginY = 1;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.btnClose, gridBagConstraint5);
			this.btnClose.Location = new System.Drawing.Point(858, 669);
			this.btnClose.Name = "btnClose";
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.btnClose, new System.Drawing.Size(75, 23));
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			// 
			// ultraTabControl1
			// 
			this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
			this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
			gridBagConstraint6.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint6.Insets.Bottom = 8;
			gridBagConstraint6.OriginX = 0;
			gridBagConstraint6.OriginY = 0;
			gridBagConstraint6.WeightX = 1F;
			gridBagConstraint6.WeightY = 1F;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.ultraTabControl1, gridBagConstraint6);
			this.ultraTabControl1.Location = new System.Drawing.Point(0, 0);
			this.ultraTabControl1.Name = "ultraTabControl1";
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.ultraTabControl1, new System.Drawing.Size(200, 100));
			this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
			this.ultraTabControl1.Size = new System.Drawing.Size(933, 661);
			this.ultraTabControl1.TabIndex = 0;
			this.ultraTabControl1.TabPageMargins.Bottom = 8;
			this.ultraTabControl1.TabPageMargins.Left = 8;
			this.ultraTabControl1.TabPageMargins.Right = 8;
			this.ultraTabControl1.TabPageMargins.Top = 8;
			ultraTab1.Key = "General";
			ultraTab1.TabPage = this.ultraTabPageControl1;
			ultraTab1.Text = "General";
			this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1});
			this.ultraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007;
			// 
			// ultraTabSharedControlsPage1
			// 
			this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(915, 620);
			// 
			// ultraGridBagLayoutPanel2
			// 
			this.ultraGridBagLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
			this.ultraGridBagLayoutPanel2.Controls.Add(this.ultraTextEditor1);
			this.ultraGridBagLayoutPanel2.Controls.Add(this.ultraLabel2);
			this.ultraGridBagLayoutPanel2.Controls.Add(this.ultraNumericEditor1);
			this.ultraGridBagLayoutPanel2.Controls.Add(this.ultraLabel1);
			this.ultraGridBagLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ultraGridBagLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.ultraGridBagLayoutPanel2.Name = "ultraGridBagLayoutPanel2";
			this.ultraGridBagLayoutPanel2.Size = new System.Drawing.Size(915, 620);
			this.ultraGridBagLayoutPanel2.TabIndex = 0;
			// 
			// ultraLabel1
			// 
			gridBagConstraint4.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint4.OriginX = 0;
			gridBagConstraint4.OriginY = 0;
			this.ultraGridBagLayoutPanel2.SetGridBagConstraint(this.ultraLabel1, gridBagConstraint4);
			this.ultraLabel1.Location = new System.Drawing.Point(282, 277);
			this.ultraLabel1.Name = "ultraLabel1";
			this.ultraGridBagLayoutPanel2.SetPreferredSize(this.ultraLabel1, new System.Drawing.Size(100, 23));
			this.ultraLabel1.Size = new System.Drawing.Size(150, 33);
			this.ultraLabel1.TabIndex = 0;
			this.ultraLabel1.Text = "Default ENR";
			// 
			// ultraNumericEditor1
			// 
			gridBagConstraint3.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint3.Insets.Bottom = 8;
			gridBagConstraint3.OriginX = 1;
			gridBagConstraint3.OriginY = 0;
			this.ultraGridBagLayoutPanel2.SetGridBagConstraint(this.ultraNumericEditor1, gridBagConstraint3);
			this.ultraNumericEditor1.Location = new System.Drawing.Point(432, 277);
			this.ultraNumericEditor1.Name = "ultraNumericEditor1";
			this.ultraGridBagLayoutPanel2.SetPreferredSize(this.ultraNumericEditor1, new System.Drawing.Size(100, 25));
			this.ultraNumericEditor1.PromptChar = ' ';
			this.ultraNumericEditor1.Size = new System.Drawing.Size(200, 25);
			this.ultraNumericEditor1.TabIndex = 1;
			// 
			// ultraLabel2
			// 
			gridBagConstraint2.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint2.OriginX = 0;
			gridBagConstraint2.OriginY = 1;
			this.ultraGridBagLayoutPanel2.SetGridBagConstraint(this.ultraLabel2, gridBagConstraint2);
			this.ultraLabel2.Location = new System.Drawing.Point(282, 310);
			this.ultraLabel2.Name = "ultraLabel2";
			this.ultraGridBagLayoutPanel2.SetPreferredSize(this.ultraLabel2, new System.Drawing.Size(150, 23));
			this.ultraLabel2.Size = new System.Drawing.Size(150, 33);
			this.ultraLabel2.TabIndex = 2;
			this.ultraLabel2.Text = "Default Project Name";
			// 
			// ultraTextEditor1
			// 
			gridBagConstraint1.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint1.Insets.Bottom = 8;
			gridBagConstraint1.OriginX = 1;
			gridBagConstraint1.OriginY = 1;
			this.ultraGridBagLayoutPanel2.SetGridBagConstraint(this.ultraTextEditor1, gridBagConstraint1);
			this.ultraTextEditor1.Location = new System.Drawing.Point(432, 310);
			this.ultraTextEditor1.Name = "ultraTextEditor1";
			this.ultraTextEditor1.NullText = "Enter a default project name; otherwise, \"Project\" will be used";
			appearance1.FontData.ItalicAsString = "True";
			this.ultraTextEditor1.NullTextAppearance = appearance1;
			this.ultraGridBagLayoutPanel2.SetPreferredSize(this.ultraTextEditor1, new System.Drawing.Size(200, 25));
			this.ultraTextEditor1.Size = new System.Drawing.Size(200, 25);
			this.ultraTextEditor1.TabIndex = 3;
			// 
			// OptionsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.ClientSize = new System.Drawing.Size(933, 692);
			this.Controls.Add(this.ultraGridBagLayoutPanel1);
			this.Name = "OptionsPage";
			this.ultraTabPageControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel1)).EndInit();
			this.ultraGridBagLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
			this.ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel2)).EndInit();
			this.ultraGridBagLayoutPanel2.ResumeLayout(false);
			this.ultraGridBagLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditor1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ultraTextEditor1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Infragistics.Win.Misc.UltraGridBagLayoutPanel ultraGridBagLayoutPanel1;
		private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl1;
		private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
		private Infragistics.Win.Misc.UltraButton btnClose;
		private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
		private Infragistics.Win.Misc.UltraGridBagLayoutPanel ultraGridBagLayoutPanel2;
		private Infragistics.Win.Misc.UltraLabel ultraLabel2;
		private Infragistics.Win.UltraWinEditors.UltraNumericEditor ultraNumericEditor1;
		private Infragistics.Win.Misc.UltraLabel ultraLabel1;
		private Infragistics.Win.Misc.UltraGridBagLayoutManager ultraGridBagLayoutManager1;
		private Infragistics.Win.UltraWinEditors.UltraTextEditor ultraTextEditor1;
	}
}
