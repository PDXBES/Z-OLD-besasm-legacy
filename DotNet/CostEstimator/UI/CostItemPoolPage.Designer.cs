namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
	partial class CostItemPoolPage
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
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint1 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CostItems", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Quantity");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UnitCost");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UnitName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comment");
			Infragistics.Win.Layout.GridBagConstraint gridBagConstraint2 = new Infragistics.Win.Layout.GridBagConstraint();
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ID");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Name");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Quantity");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("UnitCost");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("UnitName");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn6 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Comment");
			Infragistics.Win.UltraWinToolbars.RibbonTab ribbonTab1 = new Infragistics.Win.UltraWinToolbars.RibbonTab("Cost Items");
			Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup1 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Cost Items");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			this.ultraGridBagLayoutPanel1 = new Infragistics.Win.Misc.UltraGridBagLayoutPanel();
			this.btnClose = new Infragistics.Win.Misc.UltraButton();
			this.gridCostItems = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.dsCostItemPool = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
			this.toolbarManager = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
			this._ChildFormTemplate_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			this._ChildFormTemplate_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			this._ChildFormTemplate_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel1)).BeginInit();
			this.ultraGridBagLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridCostItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsCostItemPool)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolbarManager)).BeginInit();
			this.SuspendLayout();
			// 
			// ultraGridBagLayoutPanel1
			// 
			this.ultraGridBagLayoutPanel1.Controls.Add(this.btnClose);
			this.ultraGridBagLayoutPanel1.Controls.Add(this.gridCostItems);
			this.ultraGridBagLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ultraGridBagLayoutPanel1.Location = new System.Drawing.Point(4, 155);
			this.ultraGridBagLayoutPanel1.Name = "ultraGridBagLayoutPanel1";
			this.ultraGridBagLayoutPanel1.Size = new System.Drawing.Size(925, 579);
			this.ultraGridBagLayoutPanel1.TabIndex = 0;
			// 
			// btnClose
			// 
			gridBagConstraint1.Anchor = Infragistics.Win.Layout.AnchorType.Right;
			gridBagConstraint1.OriginX = 0;
			gridBagConstraint1.OriginY = 1;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.btnClose, gridBagConstraint1);
			this.btnClose.Location = new System.Drawing.Point(850, 556);
			this.btnClose.Name = "btnClose";
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.btnClose, new System.Drawing.Size(75, 23));
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// gridCostItems
			// 
			this.gridCostItems.DataSource = this.dsCostItemPool;
			this.gridCostItems.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
			ultraGridColumn1.Header.VisiblePosition = 0;
			ultraGridColumn1.Width = 98;
			ultraGridColumn2.Header.VisiblePosition = 1;
			ultraGridColumn2.Width = 178;
			ultraGridColumn3.Header.VisiblePosition = 2;
			ultraGridColumn3.Width = 136;
			ultraGridColumn4.Header.VisiblePosition = 3;
			ultraGridColumn4.Width = 136;
			ultraGridColumn5.Header.VisiblePosition = 4;
			ultraGridColumn5.Width = 178;
			ultraGridColumn6.Header.VisiblePosition = 5;
			ultraGridColumn6.Width = 178;
			ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6});
			this.gridCostItems.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			this.gridCostItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			gridBagConstraint2.Fill = Infragistics.Win.Layout.FillType.Both;
			gridBagConstraint2.Insets.Bottom = 8;
			gridBagConstraint2.OriginX = 0;
			gridBagConstraint2.OriginY = 0;
			gridBagConstraint2.WeightX = 1F;
			gridBagConstraint2.WeightY = 1F;
			this.ultraGridBagLayoutPanel1.SetGridBagConstraint(this.gridCostItems, gridBagConstraint2);
			this.gridCostItems.Location = new System.Drawing.Point(0, 0);
			this.gridCostItems.Name = "gridCostItems";
			this.ultraGridBagLayoutPanel1.SetPreferredSize(this.gridCostItems, new System.Drawing.Size(550, 80));
			this.gridCostItems.Size = new System.Drawing.Size(925, 548);
			this.gridCostItems.TabIndex = 0;
			this.gridCostItems.Text = "ultraGrid1";
			// 
			// dsCostItemPool
			// 
			ultraDataColumn1.DataType = typeof(int);
			ultraDataColumn3.DataType = typeof(double);
			ultraDataColumn4.DataType = typeof(decimal);
			this.dsCostItemPool.Band.Columns.AddRange(new object[] {
            ultraDataColumn1,
            ultraDataColumn2,
            ultraDataColumn3,
            ultraDataColumn4,
            ultraDataColumn5,
            ultraDataColumn6});
			this.dsCostItemPool.Band.Key = "CostItems";
			this.dsCostItemPool.CellDataRequested += new Infragistics.Win.UltraWinDataSource.CellDataRequestedEventHandler(this.dsCostItemPool_CellDataRequested);
			// 
			// toolbarManager
			// 
			this.toolbarManager.DesignerFlags = 1;
			this.toolbarManager.DockWithinContainer = this;
			this.toolbarManager.DockWithinContainerBaseType = typeof(SystemsAnalysis.Analysis.CostEstimator.UI.ChildFormTemplate);
			this.toolbarManager.MdiMergeable = false;
			ribbonTab1.Caption = "Cost Items";
			ribbonGroup1.Caption = "Cost Items";
			ribbonGroup1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool1});
			ribbonTab1.Groups.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonGroup[] {
            ribbonGroup1});
			this.toolbarManager.Ribbon.NonInheritedRibbonTabs.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonTab[] {
            ribbonTab1});
			this.toolbarManager.Ribbon.Visible = true;
			appearance11.Image = global::SystemsAnalysis.Analysis.CostEstimator.UI.Properties.Resources.CloseCostEstimator_small;
			buttonTool2.SharedProps.AppearancesSmall.Appearance = appearance11;
			buttonTool2.SharedProps.Caption = "Exit";
			this.toolbarManager.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool2});
			this.toolbarManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.toolbarManager_ToolClick);
			// 
			// _ChildFormTemplate_Toolbars_Dock_Area_Left
			// 
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.InitialResizeAreaExtent = 4;
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 155);
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.Name = "_ChildFormTemplate_Toolbars_Dock_Area_Left";
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(4, 579);
			this._ChildFormTemplate_Toolbars_Dock_Area_Left.ToolbarsManager = this.toolbarManager;
			// 
			// _ChildFormTemplate_Toolbars_Dock_Area_Right
			// 
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.InitialResizeAreaExtent = 4;
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(929, 155);
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.Name = "_ChildFormTemplate_Toolbars_Dock_Area_Right";
			this._ChildFormTemplate_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(4, 579);
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
			this._ChildFormTemplate_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(933, 155);
			this._ChildFormTemplate_Toolbars_Dock_Area_Top.ToolbarsManager = this.toolbarManager;
			// 
			// _ChildFormTemplate_Toolbars_Dock_Area_Bottom
			// 
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 734);
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.Name = "_ChildFormTemplate_Toolbars_Dock_Area_Bottom";
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(933, 4);
			this._ChildFormTemplate_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.toolbarManager;
			// 
			// CostItemPoolPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.ClientSize = new System.Drawing.Size(933, 738);
			this.Controls.Add(this.ultraGridBagLayoutPanel1);
			this.Controls.Add(this._ChildFormTemplate_Toolbars_Dock_Area_Left);
			this.Controls.Add(this._ChildFormTemplate_Toolbars_Dock_Area_Right);
			this.Controls.Add(this._ChildFormTemplate_Toolbars_Dock_Area_Top);
			this.Controls.Add(this._ChildFormTemplate_Toolbars_Dock_Area_Bottom);
			this.Name = "CostItemPoolPage";
			this.Text = "Cost Item Pool";
			this.Load += new System.EventHandler(this.CostItemPoolPage_Load);
			this.VisibleChanged += new System.EventHandler(this.CostItemPoolPage_VisibleChanged);
			((System.ComponentModel.ISupportInitialize)(this.ultraGridBagLayoutPanel1)).EndInit();
			this.ultraGridBagLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridCostItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsCostItemPool)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolbarManager)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Infragistics.Win.Misc.UltraGridBagLayoutPanel ultraGridBagLayoutPanel1;
		private Infragistics.Win.Misc.UltraButton btnClose;
		private Infragistics.Win.UltraWinGrid.UltraGrid gridCostItems;
		private Infragistics.Win.UltraWinDataSource.UltraDataSource dsCostItemPool;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager toolbarManager;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _ChildFormTemplate_Toolbars_Dock_Area_Left;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _ChildFormTemplate_Toolbars_Dock_Area_Right;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _ChildFormTemplate_Toolbars_Dock_Area_Top;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _ChildFormTemplate_Toolbars_Dock_Area_Bottom;
	}
}
