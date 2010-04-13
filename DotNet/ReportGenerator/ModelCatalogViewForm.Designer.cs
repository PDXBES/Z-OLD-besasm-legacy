namespace SystemsAnalysis.Reporting
{
    partial class ModelCatalogViewForm
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ModelScenario", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StormID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelScenario_LinkHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelScenario_NodeHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelScenario_DscHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ModelScenario_LinkHydraulics", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LinkHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MLinkID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxQ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxQ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxV");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxV");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("QQRatio");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxUsElev");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxDsElev");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ModelScenario_NodeHydraulics", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NodeHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NodeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxElevation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxElev");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Surcharge");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Freeboard");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SurchargeTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FloodedTime");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ModelScenario_DscHydraulics", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DscHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DscID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HGL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeltaHGL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Surcharge");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ModelCatalog", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StudyArea");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeFrame");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelPath");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelOutputFile");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Modeler");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsUploaded");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UploadDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UploadedBy");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            this.cmbSelectScenario = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.modelCatalogDS = new SystemsAnalysis.DataAccess.ModelCatalogDataSet();
            this.grdModelCatalog = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblScenario = new System.Windows.Forms.Label();
            this.btnAddModel = new Infragistics.Win.Misc.UltraButton();
            this.btnClose = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSelectScenario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modelCatalogDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdModelCatalog)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSelectScenario
            // 
            this.cmbSelectScenario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbSelectScenario.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cmbSelectScenario.DataMember = "ModelScenario";
            this.cmbSelectScenario.DataSource = this.modelCatalogDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbSelectScenario.DisplayLayout.Appearance = appearance1;
            this.cmbSelectScenario.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 0;
            ultraGridColumn3.Width = 294;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6});
            ultraGridBand1.Header.Enabled = false;
            ultraGridColumn7.Header.VisiblePosition = 0;
            ultraGridColumn8.Header.VisiblePosition = 1;
            ultraGridColumn9.Header.VisiblePosition = 2;
            ultraGridColumn10.Header.VisiblePosition = 3;
            ultraGridColumn11.Header.VisiblePosition = 4;
            ultraGridColumn12.Header.VisiblePosition = 5;
            ultraGridColumn13.Header.VisiblePosition = 6;
            ultraGridColumn14.Header.VisiblePosition = 7;
            ultraGridColumn15.Header.VisiblePosition = 8;
            ultraGridColumn16.Header.VisiblePosition = 9;
            ultraGridColumn17.Header.VisiblePosition = 10;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17});
            ultraGridColumn18.Header.VisiblePosition = 0;
            ultraGridColumn19.Header.VisiblePosition = 1;
            ultraGridColumn20.Header.VisiblePosition = 2;
            ultraGridColumn21.Header.VisiblePosition = 3;
            ultraGridColumn22.Header.VisiblePosition = 4;
            ultraGridColumn23.Header.VisiblePosition = 5;
            ultraGridColumn24.Header.VisiblePosition = 6;
            ultraGridColumn25.Header.VisiblePosition = 7;
            ultraGridColumn26.Header.VisiblePosition = 8;
            ultraGridColumn27.Header.VisiblePosition = 9;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27});
            ultraGridColumn28.Header.VisiblePosition = 0;
            ultraGridColumn29.Header.VisiblePosition = 1;
            ultraGridColumn30.Header.VisiblePosition = 2;
            ultraGridColumn31.Header.VisiblePosition = 3;
            ultraGridColumn32.Header.VisiblePosition = 4;
            ultraGridColumn33.Header.VisiblePosition = 5;
            ultraGridColumn34.Header.VisiblePosition = 6;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34});
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.cmbSelectScenario.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.cmbSelectScenario.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbSelectScenario.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbSelectScenario.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.cmbSelectScenario.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.cmbSelectScenario.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.cmbSelectScenario.DisplayLayout.MaxColScrollRegions = 1;
            this.cmbSelectScenario.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbSelectScenario.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cmbSelectScenario.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.cmbSelectScenario.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.cmbSelectScenario.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.cmbSelectScenario.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.cmbSelectScenario.DisplayLayout.Override.CellAppearance = appearance8;
            this.cmbSelectScenario.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.cmbSelectScenario.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.cmbSelectScenario.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.cmbSelectScenario.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.cmbSelectScenario.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.cmbSelectScenario.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.cmbSelectScenario.DisplayLayout.Override.RowAppearance = appearance11;
            this.cmbSelectScenario.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbSelectScenario.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.cmbSelectScenario.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.cmbSelectScenario.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.cmbSelectScenario.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.cmbSelectScenario.DisplayMember = "Description";
            this.cmbSelectScenario.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.cmbSelectScenario.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSelectScenario.Location = new System.Drawing.Point(141, 272);
            this.cmbSelectScenario.Name = "cmbSelectScenario";
            this.cmbSelectScenario.Size = new System.Drawing.Size(312, 25);
            this.cmbSelectScenario.TabIndex = 27;
            this.cmbSelectScenario.ValueMember = "ScenarioID";
            this.cmbSelectScenario.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.cmbSelectScenario_RowSelected);
            // 
            // modelCatalogDS
            // 
            this.modelCatalogDS.DataSetName = "ModelCatalogDataSet";
            this.modelCatalogDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grdModelCatalog
            // 
            this.grdModelCatalog.DataMember = "ModelCatalog";
            this.grdModelCatalog.DataSource = this.modelCatalogDS;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grdModelCatalog.DisplayLayout.Appearance = appearance13;
            ultraGridColumn35.Header.Caption = "ID";
            ultraGridColumn35.Header.VisiblePosition = 1;
            ultraGridColumn35.Width = 38;
            ultraGridColumn36.Header.VisiblePosition = 0;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.Header.Caption = "Model";
            ultraGridColumn37.Header.VisiblePosition = 2;
            ultraGridColumn38.Header.VisiblePosition = 3;
            ultraGridColumn39.Header.Caption = "Description";
            ultraGridColumn39.Header.VisiblePosition = 4;
            ultraGridColumn39.Hidden = true;
            ultraGridColumn40.Header.VisiblePosition = 5;
            ultraGridColumn40.Hidden = true;
            ultraGridColumn41.Header.VisiblePosition = 6;
            ultraGridColumn41.Hidden = true;
            ultraGridColumn42.Header.Caption = "Path";
            ultraGridColumn42.Header.VisiblePosition = 7;
            ultraGridColumn42.Width = 191;
            ultraGridColumn43.Header.VisiblePosition = 8;
            ultraGridColumn43.Hidden = true;
            ultraGridColumn44.Header.VisiblePosition = 9;
            ultraGridColumn45.Header.Caption = "Is Uploaded?";
            ultraGridColumn45.Header.VisiblePosition = 10;
            ultraGridColumn45.Hidden = true;
            ultraGridColumn46.Header.VisiblePosition = 11;
            ultraGridColumn46.Hidden = true;
            ultraGridColumn47.Header.VisiblePosition = 12;
            ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn35,
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn43,
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47});
            this.grdModelCatalog.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.grdModelCatalog.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdModelCatalog.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.grdModelCatalog.DisplayLayout.GroupByBox.Appearance = appearance14;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdModelCatalog.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
            this.grdModelCatalog.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grdModelCatalog.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
            this.grdModelCatalog.DisplayLayout.MaxColScrollRegions = 1;
            this.grdModelCatalog.DisplayLayout.MaxRowScrollRegions = 1;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdModelCatalog.DisplayLayout.Override.ActiveCellAppearance = appearance17;
            appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdModelCatalog.DisplayLayout.Override.ActiveRowAppearance = appearance18;
            this.grdModelCatalog.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grdModelCatalog.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.grdModelCatalog.DisplayLayout.Override.CardAreaAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.Silver;
            appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grdModelCatalog.DisplayLayout.Override.CellAppearance = appearance20;
            this.grdModelCatalog.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdModelCatalog.DisplayLayout.Override.CellPadding = 0;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.grdModelCatalog.DisplayLayout.Override.GroupByRowAppearance = appearance21;
            appearance22.TextHAlignAsString = "Left";
            this.grdModelCatalog.DisplayLayout.Override.HeaderAppearance = appearance22;
            this.grdModelCatalog.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdModelCatalog.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            appearance23.BorderColor = System.Drawing.Color.Silver;
            this.grdModelCatalog.DisplayLayout.Override.RowAppearance = appearance23;
            this.grdModelCatalog.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdModelCatalog.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdModelCatalog.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grdModelCatalog.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
            this.grdModelCatalog.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdModelCatalog.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdModelCatalog.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdModelCatalog.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdModelCatalog.Location = new System.Drawing.Point(0, 0);
            this.grdModelCatalog.Name = "grdModelCatalog";
            this.grdModelCatalog.Size = new System.Drawing.Size(597, 263);
            this.grdModelCatalog.TabIndex = 26;
            this.grdModelCatalog.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grdModelCatalog_InitializeLayout);
            // 
            // lblScenario
            // 
            this.lblScenario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblScenario.Location = new System.Drawing.Point(53, 272);
            this.lblScenario.Name = "lblScenario";
            this.lblScenario.Size = new System.Drawing.Size(82, 25);
            this.lblScenario.TabIndex = 24;
            this.lblScenario.Text = "Scenario:";
            this.lblScenario.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnAddModel
            // 
            this.btnAddModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddModel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnAddModel.Location = new System.Drawing.Point(141, 303);
            this.btnAddModel.Name = "btnAddModel";
            this.btnAddModel.Size = new System.Drawing.Size(185, 48);
            this.btnAddModel.TabIndex = 29;
            this.btnAddModel.Text = "Import Start/Stop Links From Selected Models";
            this.btnAddModel.Click += new System.EventHandler(this.btnAddModel_Click);
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnClose.Location = new System.Drawing.Point(332, 303);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(121, 48);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ModelCatalogViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 367);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddModel);
            this.Controls.Add(this.cmbSelectScenario);
            this.Controls.Add(this.lblScenario);
            this.Controls.Add(this.grdModelCatalog);
            this.Name = "ModelCatalogViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Model Catalog View";
            ((System.ComponentModel.ISupportInitialize)(this.cmbSelectScenario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modelCatalogDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdModelCatalog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraCombo cmbSelectScenario;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdModelCatalog;
        private System.Windows.Forms.Label lblScenario;
        private SystemsAnalysis.DataAccess.ModelCatalogDataSet modelCatalogDS;
        private Infragistics.Win.Misc.UltraButton btnAddModel;
        private Infragistics.Win.Misc.UltraButton btnClose;
    }
}