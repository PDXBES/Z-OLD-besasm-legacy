namespace SystemsAnalysis.Grid.GridAnalysis
{
    partial class FrmChooseProcesses
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
            Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("QryProcesses", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("process_group");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("included_in_scenario");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("group_order", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gridInterfaceDS = new SystemsAnalysis.Grid.GridAnalysis.GridInterfaceDataSet();
            this.grdProcesses = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.gridInterfaceDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProcesses)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(137, 298);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(152, 23);
            this.btnSave.TabIndex = 31;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(295, 298);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(152, 23);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gridInterfaceDS
            // 
            this.gridInterfaceDS.DataSetName = "GridInterfaceDataSet";
            this.gridInterfaceDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grdProcesses
            // 
            this.grdProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProcesses.DataMember = "QryProcesses";
            this.grdProcesses.DataSource = this.gridInterfaceDS;
            appearance121.BackColor = System.Drawing.SystemColors.Window;
            appearance121.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grdProcesses.DisplayLayout.Appearance = appearance121;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn3.Header.VisiblePosition = 1;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4});
            ultraGridBand1.Expandable = false;
            this.grdProcesses.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdProcesses.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdProcesses.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance122.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance122.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance122.BorderColor = System.Drawing.SystemColors.Window;
            this.grdProcesses.DisplayLayout.GroupByBox.Appearance = appearance122;
            appearance123.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdProcesses.DisplayLayout.GroupByBox.BandLabelAppearance = appearance123;
            this.grdProcesses.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance124.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance124.BackColor2 = System.Drawing.SystemColors.Control;
            appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdProcesses.DisplayLayout.GroupByBox.PromptAppearance = appearance124;
            this.grdProcesses.DisplayLayout.MaxColScrollRegions = 1;
            this.grdProcesses.DisplayLayout.MaxRowScrollRegions = 1;
            appearance125.BackColor = System.Drawing.SystemColors.Window;
            appearance125.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdProcesses.DisplayLayout.Override.ActiveCellAppearance = appearance125;
            appearance126.BackColor = System.Drawing.SystemColors.Highlight;
            appearance126.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdProcesses.DisplayLayout.Override.ActiveRowAppearance = appearance126;
            this.grdProcesses.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdProcesses.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance127.BackColor = System.Drawing.SystemColors.Window;
            this.grdProcesses.DisplayLayout.Override.CardAreaAppearance = appearance127;
            appearance128.BorderColor = System.Drawing.Color.Silver;
            appearance128.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grdProcesses.DisplayLayout.Override.CellAppearance = appearance128;
            this.grdProcesses.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdProcesses.DisplayLayout.Override.CellPadding = 0;
            appearance129.BackColor = System.Drawing.SystemColors.Control;
            appearance129.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance129.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance129.BorderColor = System.Drawing.SystemColors.Window;
            this.grdProcesses.DisplayLayout.Override.GroupByRowAppearance = appearance129;
            appearance130.TextHAlignAsString = "Left";
            this.grdProcesses.DisplayLayout.Override.HeaderAppearance = appearance130;
            this.grdProcesses.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdProcesses.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance131.BackColor = System.Drawing.SystemColors.Window;
            appearance131.BorderColor = System.Drawing.Color.Silver;
            this.grdProcesses.DisplayLayout.Override.RowAppearance = appearance131;
            this.grdProcesses.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdProcesses.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdProcesses.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance132.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdProcesses.DisplayLayout.Override.TemplateAddRowAppearance = appearance132;
            this.grdProcesses.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdProcesses.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdProcesses.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdProcesses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProcesses.Location = new System.Drawing.Point(12, 12);
            this.grdProcesses.Name = "grdProcesses";
            this.grdProcesses.Size = new System.Drawing.Size(603, 280);
            this.grdProcesses.TabIndex = 33;
            this.grdProcesses.Text = "grdSelectionSetAreas";
            // 
            // FrmChooseProcesses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 333);
            this.ControlBox = false;
            this.Controls.Add(this.grdProcesses);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "FrmChooseProcesses";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Select Grid Processes for Current Scenario";
            this.Load += new System.EventHandler(this.frmChooseProcesses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridInterfaceDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProcesses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GridInterfaceDataSet gridInterfaceDS;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdProcesses;
    }
}