namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	partial class SelectEstimateItemsPage
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
			Infragistics.Win.UltraWinToolbars.RibbonTab ribbonTab1 = new Infragistics.Win.UltraWinToolbars.RibbonTab("Select Items");
			Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup1 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Select Items");
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint1 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint2 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Selected");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Item");
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint3 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Selected");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Item");
			this.toolbarManager = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
			this.ChildFormTemplate_Fill_Panel = new System.Windows.Forms.Panel();
			this.ultraGridBagLayoutPanel1 = new Infragistics.Win.Misc.UltraGridBagLayoutPanel();
			this.btnOK = new Infragistics.Win.Misc.UltraButton();
			this.btnCancel = new Infragistics.Win.Misc.UltraButton();
			this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.dsItems = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
			this._ChildFormTemplate_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			this._ChildFormTemplate_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			this._ChildFormTemplate_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			((System.ComponentModel.ISupportInitialize)(this.toolbarManager)).BeginInit();
			this.ChildFormTemplate_Fill_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel1)).BeginInit();
			this.ultraGridBagLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsItems)).BeginInit();
			this.SuspendLayout();
			// 
			// toolbarManager
			// 
			this.toolbarManager.DesignerFlags = 1;
			this.toolbarManager.DockWithinContainer = this;
			this.toolbarManager.DockWithinContainerBaseType = typeof(SystemsAnalysis.Analysis.CostEstimator.UI.ChildFormTemplate);
			ribbonTab1.Caption = "Select Items";
			ribbonGroup1.Caption = "Select Items";
			ribbonTab1.Groups.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonGroup[] {
            ribbonGroup1});
			this.toolbarManager.Ribbon.NonInheritedRibbonTabs.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonTab[] {
            ribbonTab1});
			this.toolbarManager.Ribbon.Visible = true;
			this.toolbarManager.ShowFullMenusDelay = 500;
			// 
			// ChildFormTemplate_Fill_Panel
			// 
			this.ChildFormTemplate_Fill_Panel.Controls.Add(this.ultraGridBagLayoutPanel1);
			this.ChildFormTemplate_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
			this.ChildFormTemplate_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChildFormTemplate_Fill_Panel.Location = new System.Drawing.Point(4, 150);
			this.ChildFormTemplate_Fill_Panel.Name = "ChildFormTemplate_Fill_Panel";
			this.ChildFormTemplate_Fill_Panel.Size = new System.Drawing.Size(925, 538);
			this.ChildFormTemplate_Fill_Panel.TabIndex = 0;
			// 
			// ultraGridBagLayoutPanel1
			// 
			this.ultraGridBagLayoutPanel1.Controls.Add(this.btnOK);
			this.ultraGridBagLayoutPanel1.Controls.Add(this.btnCancel);
			this.ultraGridBagLayoutPanel1.Controls.Add(this.ultraGrid1);
			this.ultraGridBagLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ultraGridBagLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.ultraGridBagLayoutPanel1.Name = "ultraGridBagLayoutPanel1";
			this.ultraGridBagLayoutPanel1.Size = new System.Drawing.Size(925, 538);
			this.ultraGridBagLayoutPanel1.TabIndex = 0;
			// 
			// btnOK
			// 
			gridBagConstraint1.Anchor = Infragistics.Win.Layout.AnchorType.Right;
			gridBagConstraint1.Insets.Right = 8;
			gridBagConstraint1.OriginX = 0;
			gridBagConstraint1.OriginY = 1;
			gridBagConstraint1.WeightX = 1F;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.btnOK, gridBagConstraint1);
			this.btnOK.Location = new System.Drawing.Point(767, 515);
			this.btnOK.Name = "btnOK";
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.btnOK, new System.Drawing.Size(75, 23));
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			gridBagConstraint2.Anchor = Infragistics.Win.Layout.AnchorType.Right;
			gridBagConstraint2.OriginX = 1;
			gridBagConstraint2.OriginY = 1;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.btnCancel, gridBagConstraint2);
			this.btnCancel.Location = new System.Drawing.Point(850, 515);
			this.btnCancel.Name = "btnCancel";
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.btnCancel, new System.Drawing.Size(75, 23));
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// ultraGrid1
			// 
			this.ultraGrid1.DataSource = this.dsItems;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			this.ultraGrid1.DisplayLayout.Appearance = appearance11;
			this.ultraGrid1.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
			ultraGridColumn1.Header.VisiblePosition = 0;
			ultraGridColumn2.Header.VisiblePosition = 1;
			ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2});
			this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			this.ultraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			this.ultraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance12.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance12.BorderColor = System.Drawing.SystemColors.Window;
			this.ultraGrid1.DisplayLayout.GroupByBox.Appearance = appearance12;
			appearance13.ForeColor = System.Drawing.SystemColors.GrayText;
			this.ultraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance13;
			this.ultraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			this.ultraGrid1.DisplayLayout.GroupByBox.Hidden = true;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance14.BackColor2 = System.Drawing.SystemColors.Control;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance14.ForeColor = System.Drawing.SystemColors.GrayText;
			this.ultraGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance14;
			this.ultraGrid1.DisplayLayout.MaxColScrollRegions = 1;
			this.ultraGrid1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ultraGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance15;
			appearance16.BackColor = System.Drawing.SystemColors.Highlight;
			appearance16.ForeColor = System.Drawing.SystemColors.HighlightText;
			this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance16;
			this.ultraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			this.ultraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance17;
			appearance18.BorderColor = System.Drawing.Color.Silver;
			appearance18.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			this.ultraGrid1.DisplayLayout.Override.CellAppearance = appearance18;
			this.ultraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			this.ultraGrid1.DisplayLayout.Override.CellPadding = 0;
			appearance19.BackColor = System.Drawing.SystemColors.Control;
			appearance19.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance19.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance19.BorderColor = System.Drawing.SystemColors.Window;
			this.ultraGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance19;
			appearance20.TextHAlignAsString = "Left";
			this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance20;
			this.ultraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			this.ultraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.BorderColor = System.Drawing.Color.Silver;
			this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance21;
			this.ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance22.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ultraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance22;
			this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			this.ultraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			gridBagConstraint3.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint3.Insets.Bottom = 8;
			gridBagConstraint3.OriginX = 0;
			gridBagConstraint3.OriginY = 0;
			gridBagConstraint3.SpanX = 2;
			gridBagConstraint3.WeightX = 1F;
			gridBagConstraint3.WeightY = 1F;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.ultraGrid1, gridBagConstraint3);
			this.ultraGrid1.Location = new System.Drawing.Point(0, 0);
			this.ultraGrid1.Name = "ultraGrid1";
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.ultraGrid1, new System.Drawing.Size(550, 80));
			this.ultraGrid1.Size = new System.Drawing.Size(925, 507);
			this.ultraGrid1.TabIndex = 0;
			this.ultraGrid1.Text = "ultraGrid1";
			// 
			// dsItems
			// 
			ultraDataColumn1.DataType = typeof(bool);
			this.dsItems.Band.Columns.AddRange(new object[] {
            ultraDataColumn1,
            ultraDataColumn2});
			this.dsItems.CellDataRequested += new Infragistics.Win.UltraWinDataSource.CellDataRequestedEventHandler(this.dsItems_CellDataRequested);
			this.dsItems.CellDataUpdated += new Infragistics.Win.UltraWinDataSource.CellDataUpdatedEventHandler(this.dsItems_CellDataUpdated);
			// 
			// _ChildFormTemplate_Toolbars_Dock_Area_Left
			// 
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.InitialResizeAreaExtent = 4;
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 150);
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.Name = "_ChildFormTemplate_Toolbars_Dock_Area_Left";
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(4, 538);
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.ToolbarsManager = this.toolbarManager;
			// 
			// _ChildFormTemplate_Toolbars_Dock_Area_Right
			// 
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.InitialResizeAreaExtent = 4;
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(929, 150);
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.Name = "_ChildFormTemplate_Toolbars_Dock_Area_Right";
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(4, 538);
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.ToolbarsManager = this.toolbarManager;
			// 
			// _ChildFormTemplate_Toolbars_Dock_Area_Top
			// 
			this._ChildFormTemplate_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._ChildFormTemplate_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
			this._ChildFormTemplate_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
			this._ChildFormTemplate_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
			this._ChildFormTemplate_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
			this._ChildFormTemplate_Toolbars_Dock_Area_Top.Name = "_ChildFormTemplate_Toolbars_Dock_Area_Top";
			this._ChildFormTemplate_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(933, 150);
			this._ChildFormTemplate_Toolbars_Dock_Area_Top.ToolbarsManager = this.toolbarManager;
			// 
			// _ChildFormTemplate_Toolbars_Dock_Area_Bottom
			// 
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 688);
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.Name = "_ChildFormTemplate_Toolbars_Dock_Area_Bottom";
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(933, 4);
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.toolbarManager;
			// 
			// SelectEstimateItemsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.ClientSize = new System.Drawing.Size(933, 692);
			this.Controls.Add(this.ChildFormTemplate_Fill_Panel);
			this.Controls.Add(this._ChildFormTemplate_Toolbars_Dock_Area_Left);
			this.Controls.Add(this._ChildFormTemplate_Toolbars_Dock_Area_Right);
			this.Controls.Add(this._ChildFormTemplate_Toolbars_Dock_Area_Top);
			this.Controls.Add(this._ChildFormTemplate_Toolbars_Dock_Area_Bottom);
			this.Name = "SelectEstimateItemsPage";
			((System.ComponentModel.ISupportInitialize)(this.toolbarManager)).EndInit();
			this.ChildFormTemplate_Fill_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel1)).EndInit();
			this.ultraGridBagLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsItems)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager toolbarManager;
		private System.Windows.Forms.Panel ChildFormTemplate_Fill_Panel;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _ChildFormTemplate_Toolbars_Dock_Area_Left;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _ChildFormTemplate_Toolbars_Dock_Area_Right;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _ChildFormTemplate_Toolbars_Dock_Area_Top;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _ChildFormTemplate_Toolbars_Dock_Area_Bottom;
		private Infragistics.Win.UltraWinDataSource.UltraDataSource dsItems;
		private Infragistics.Win.Misc.UltraGridBagLayoutPanel ultraGridBagLayoutPanel1;
		private Infragistics.Win.Misc.UltraButton btnCancel;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
		private Infragistics.Win.Misc.UltraButton btnOK;
	}
}
