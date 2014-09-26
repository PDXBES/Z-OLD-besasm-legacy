using System.Windows.Forms.Design;
using System.Windows.Forms;
using System;

namespace SWI_3
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapsAndViewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripNUDPage = new SWI_3.Form1.ToolStripNumberControl();
            this.ToolStripViewGoButton = new System.Windows.Forms.ToolStripButton();
            this.stopAndSmellTheRosesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripNUDView = new SWI_3.Form1.ToolStripNumberControl();
            this.ToolStripMapGoButton = new System.Windows.Forms.ToolStripButton();
            this.labelWatershed = new System.Windows.Forms.Label();
            this.labelSubwatershed = new System.Windows.Forms.Label();
            this.labelMap = new System.Windows.Forms.Label();
            this.labelView = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelWeather = new System.Windows.Forms.Label();
            this.dataGridViewSurveyors = new System.Windows.Forms.DataGridView();
            this.surveypageidDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.evaluatoridDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.initialsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.presentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mESHPAGESMESHPAGEEVALUATORSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mESHVIEWSMESHPAGESBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mESHSUBWATERSHEDMESHVIEWSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mESHWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWIDataSet = new SWI_3.SWIDataSet();
            this.labelSurveyers = new System.Windows.Forms.Label();
            this.dataGridViewConveyanceObjects = new System.Windows.Forms.DataGridView();
            this.mESHSHAPETYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mESHPAGESMESHBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mESHMATERIALTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mESHCULVERTOPENINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewPhotos = new System.Windows.Forms.DataGridView();
            this.photoidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.globalidDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mESHMESHPHOTOSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelConveyanceObjects = new System.Windows.Forms.Label();
            this.labelPhotos = new System.Windows.Forms.Label();
            this.labelComments = new System.Windows.Forms.Label();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.mESH_WATERSHEDTableAdapter = new SWI_3.SWIDataSetTableAdapters.MESH_WATERSHEDTableAdapter();
            this.mESH_SUBWATERSHEDTableAdapter = new SWI_3.SWIDataSetTableAdapters.MESH_SUBWATERSHEDTableAdapter();
            this.mESH_VIEWSTableAdapter = new SWI_3.SWIDataSetTableAdapters.MESH_VIEWSTableAdapter();
            this.mESH_PAGESTableAdapter = new SWI_3.SWIDataSetTableAdapters.MESH_PAGESTableAdapter();
            this.mESHTableAdapter = new SWI_3.SWIDataSetTableAdapters.MESHTableAdapter();
            this.mESH_PHOTOSTableAdapter = new SWI_3.SWIDataSetTableAdapters.MESH_PHOTOSTableAdapter();
            this.textBoxWeather = new System.Windows.Forms.TextBox();
            this.mESH_PAGE_EVALUATORSTableAdapter = new SWI_3.SWIDataSetTableAdapters.MESH_PAGE_EVALUATORSTableAdapter();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.mESH_SHAPE_TYPETableAdapter = new SWI_3.SWIDataSetTableAdapters.MESH_SHAPE_TYPETableAdapter();
            this.mESH_MATERIAL_TYPETableAdapter = new SWI_3.SWIDataSetTableAdapters.MESH_MATERIAL_TYPETableAdapter();
            this.mESH_CULVERT_OPENING_TYPETableAdapter = new SWI_3.SWIDataSetTableAdapters.MESH_CULVERT_OPENING_TYPETableAdapter();
            this.mESHSHAPETYPEMESHBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mESHSHAPETYPEMESHBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.mESHSHAPETYPEMESHBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.global_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usnodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dsnodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shape_type_id = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dimension1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dimension2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dimension3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.material_type_id = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.culvert_opening_type_id = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lengthftDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usdepthinDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dsdepthinDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.view_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.survey_page_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.watershed_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subwatershed_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSurveyors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHPAGESMESHPAGEEVALUATORSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHVIEWSMESHPAGESBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHSUBWATERSHEDMESHVIEWSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWIDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConveyanceObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHSHAPETYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHPAGESMESHBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHMATERIALTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHCULVERTOPENINGTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPhotos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHMESHPHOTOSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHSHAPETYPEMESHBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHSHAPETYPEMESHBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHSHAPETYPEMESHBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mapsAndViewsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(993, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveChangesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveChangesToolStripMenuItem
            // 
            this.saveChangesToolStripMenuItem.Name = "saveChangesToolStripMenuItem";
            this.saveChangesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveChangesToolStripMenuItem.Text = "Save Changes";
            this.saveChangesToolStripMenuItem.Click += new System.EventHandler(this.saveChangesToolStripMenuItem_Click);
            // 
            // mapsAndViewsToolStripMenuItem
            // 
            this.mapsAndViewsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewViewToolStripMenuItem,
            this.stopAndSmellTheRosesToolStripMenuItem,
            this.addNewMapToolStripMenuItem});
            this.mapsAndViewsToolStripMenuItem.Name = "mapsAndViewsToolStripMenuItem";
            this.mapsAndViewsToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.mapsAndViewsToolStripMenuItem.Text = "Maps and Views";
            // 
            // addNewViewToolStripMenuItem
            // 
            this.addNewViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripNUDPage,
            this.ToolStripViewGoButton});
            this.addNewViewToolStripMenuItem.Name = "addNewViewToolStripMenuItem";
            this.addNewViewToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.addNewViewToolStripMenuItem.Text = "Add New View";
            // 
            // ToolStripNUDPage
            // 
            this.ToolStripNUDPage.Name = "ToolStripNUDPage";
            this.ToolStripNUDPage.Size = new System.Drawing.Size(41, 21);
            this.ToolStripNUDPage.Text = "0";
            // 
            // ToolStripViewGoButton
            // 
            this.ToolStripViewGoButton.Name = "ToolStripViewGoButton";
            this.ToolStripViewGoButton.Size = new System.Drawing.Size(30, 17);
            this.ToolStripViewGoButton.Text = "GO!";
            this.ToolStripViewGoButton.Click += new System.EventHandler(this.ToolStripViewGoButton_Click);
            // 
            // stopAndSmellTheRosesToolStripMenuItem
            // 
            this.stopAndSmellTheRosesToolStripMenuItem.Name = "stopAndSmellTheRosesToolStripMenuItem";
            this.stopAndSmellTheRosesToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.stopAndSmellTheRosesToolStripMenuItem.Text = "Stop and Smell the Roses";
            this.stopAndSmellTheRosesToolStripMenuItem.Click += new System.EventHandler(this.stopAndSmellTheRosesToolStripMenuItem_Click);
            // 
            // addNewMapToolStripMenuItem
            // 
            this.addNewMapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripNUDView,
            this.ToolStripMapGoButton});
            this.addNewMapToolStripMenuItem.Name = "addNewMapToolStripMenuItem";
            this.addNewMapToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.addNewMapToolStripMenuItem.Text = "Add New Map";
            // 
            // ToolStripNUDView
            // 
            this.ToolStripNUDView.Name = "ToolStripNUDView";
            this.ToolStripNUDView.Size = new System.Drawing.Size(41, 21);
            this.ToolStripNUDView.Text = "0";
            // 
            // ToolStripMapGoButton
            // 
            this.ToolStripMapGoButton.Name = "ToolStripMapGoButton";
            this.ToolStripMapGoButton.Size = new System.Drawing.Size(30, 17);
            this.ToolStripMapGoButton.Text = "GO!";
            this.ToolStripMapGoButton.Click += new System.EventHandler(this.ToolStripMapGoButton_Click);
            // 
            // labelWatershed
            // 
            this.labelWatershed.AutoSize = true;
            this.labelWatershed.Location = new System.Drawing.Point(12, 37);
            this.labelWatershed.Name = "labelWatershed";
            this.labelWatershed.Size = new System.Drawing.Size(59, 13);
            this.labelWatershed.TabIndex = 1;
            this.labelWatershed.Text = "Watershed";
            // 
            // labelSubwatershed
            // 
            this.labelSubwatershed.AutoSize = true;
            this.labelSubwatershed.Location = new System.Drawing.Point(157, 37);
            this.labelSubwatershed.Name = "labelSubwatershed";
            this.labelSubwatershed.Size = new System.Drawing.Size(75, 13);
            this.labelSubwatershed.TabIndex = 2;
            this.labelSubwatershed.Text = "Subwatershed";
            // 
            // labelMap
            // 
            this.labelMap.AutoSize = true;
            this.labelMap.Location = new System.Drawing.Point(302, 37);
            this.labelMap.Name = "labelMap";
            this.labelMap.Size = new System.Drawing.Size(28, 13);
            this.labelMap.TabIndex = 3;
            this.labelMap.Text = "Map";
            // 
            // labelView
            // 
            this.labelView.AutoSize = true;
            this.labelView.Location = new System.Drawing.Point(447, 37);
            this.labelView.Name = "labelView";
            this.labelView.Size = new System.Drawing.Size(30, 13);
            this.labelView.TabIndex = 4;
            this.labelView.Text = "View";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(12, 93);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(30, 13);
            this.labelDate.TabIndex = 5;
            this.labelDate.Text = "Date";
            // 
            // labelWeather
            // 
            this.labelWeather.AutoSize = true;
            this.labelWeather.Location = new System.Drawing.Point(157, 93);
            this.labelWeather.Name = "labelWeather";
            this.labelWeather.Size = new System.Drawing.Size(48, 13);
            this.labelWeather.TabIndex = 6;
            this.labelWeather.Text = "Weather";
            // 
            // dataGridViewSurveyors
            // 
            this.dataGridViewSurveyors.AllowUserToAddRows = false;
            this.dataGridViewSurveyors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSurveyors.AutoGenerateColumns = false;
            this.dataGridViewSurveyors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSurveyors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.surveypageidDataGridViewTextBoxColumn1,
            this.evaluatoridDataGridViewTextBoxColumn,
            this.initialsDataGridViewTextBoxColumn,
            this.presentDataGridViewTextBoxColumn});
            this.dataGridViewSurveyors.DataSource = this.mESHPAGESMESHPAGEEVALUATORSBindingSource;
            this.dataGridViewSurveyors.Location = new System.Drawing.Point(794, 54);
            this.dataGridViewSurveyors.Name = "dataGridViewSurveyors";
            this.dataGridViewSurveyors.RowHeadersWidth = 4;
            this.dataGridViewSurveyors.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewSurveyors.RowTemplate.Height = 18;
            this.dataGridViewSurveyors.Size = new System.Drawing.Size(188, 155);
            this.dataGridViewSurveyors.TabIndex = 7;
            // 
            // surveypageidDataGridViewTextBoxColumn1
            // 
            this.surveypageidDataGridViewTextBoxColumn1.DataPropertyName = "survey_page_id";
            this.surveypageidDataGridViewTextBoxColumn1.HeaderText = "survey_page_id";
            this.surveypageidDataGridViewTextBoxColumn1.Name = "surveypageidDataGridViewTextBoxColumn1";
            this.surveypageidDataGridViewTextBoxColumn1.Visible = false;
            // 
            // evaluatoridDataGridViewTextBoxColumn
            // 
            this.evaluatoridDataGridViewTextBoxColumn.DataPropertyName = "evaluator_id";
            this.evaluatoridDataGridViewTextBoxColumn.HeaderText = "evaluator_id";
            this.evaluatoridDataGridViewTextBoxColumn.Name = "evaluatoridDataGridViewTextBoxColumn";
            this.evaluatoridDataGridViewTextBoxColumn.Visible = false;
            // 
            // initialsDataGridViewTextBoxColumn
            // 
            this.initialsDataGridViewTextBoxColumn.DataPropertyName = "initials";
            this.initialsDataGridViewTextBoxColumn.HeaderText = "initials";
            this.initialsDataGridViewTextBoxColumn.Name = "initialsDataGridViewTextBoxColumn";
            this.initialsDataGridViewTextBoxColumn.ReadOnly = true;
            this.initialsDataGridViewTextBoxColumn.Width = 75;
            // 
            // presentDataGridViewTextBoxColumn
            // 
            this.presentDataGridViewTextBoxColumn.DataPropertyName = "present";
            this.presentDataGridViewTextBoxColumn.FalseValue = "0";
            this.presentDataGridViewTextBoxColumn.HeaderText = "present";
            this.presentDataGridViewTextBoxColumn.Name = "presentDataGridViewTextBoxColumn";
            this.presentDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.presentDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.presentDataGridViewTextBoxColumn.TrueValue = "1";
            this.presentDataGridViewTextBoxColumn.Width = 75;
            // 
            // mESHPAGESMESHPAGEEVALUATORSBindingSource
            // 
            this.mESHPAGESMESHPAGEEVALUATORSBindingSource.DataMember = "MESH_PAGES_MESH_PAGE_EVALUATORS";
            this.mESHPAGESMESHPAGEEVALUATORSBindingSource.DataSource = this.mESHVIEWSMESHPAGESBindingSource;
            this.mESHPAGESMESHPAGEEVALUATORSBindingSource.Sort = "initials";
            // 
            // mESHVIEWSMESHPAGESBindingSource
            // 
            this.mESHVIEWSMESHPAGESBindingSource.DataMember = "MESH_VIEWS_MESH_PAGES";
            this.mESHVIEWSMESHPAGESBindingSource.DataSource = this.mESHSUBWATERSHEDMESHVIEWSBindingSource;
            this.mESHVIEWSMESHPAGESBindingSource.Sort = "page_number";
            // 
            // mESHSUBWATERSHEDMESHVIEWSBindingSource
            // 
            this.mESHSUBWATERSHEDMESHVIEWSBindingSource.DataMember = "MESH_SUBWATERSHED_MESH_VIEWS";
            this.mESHSUBWATERSHEDMESHVIEWSBindingSource.DataSource = this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource;
            this.mESHSUBWATERSHEDMESHVIEWSBindingSource.Sort = "view_number";
            // 
            // mESHWATERSHEDMESHSUBWATERSHEDBindingSource
            // 
            this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.DataMember = "MESH_WATERSHED_MESH_SUBWATERSHED";
            this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource.DataSource = this.mESHWATERSHEDBindingSource;
            // 
            // mESHWATERSHEDBindingSource
            // 
            this.mESHWATERSHEDBindingSource.DataMember = "MESH_WATERSHED";
            this.mESHWATERSHEDBindingSource.DataSource = this.sWIDataSet;
            // 
            // sWIDataSet
            // 
            this.sWIDataSet.DataSetName = "SWIDataSet";
            this.sWIDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // labelSurveyers
            // 
            this.labelSurveyers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSurveyers.AutoSize = true;
            this.labelSurveyers.Location = new System.Drawing.Point(791, 37);
            this.labelSurveyers.Name = "labelSurveyers";
            this.labelSurveyers.Size = new System.Drawing.Size(54, 13);
            this.labelSurveyers.TabIndex = 8;
            this.labelSurveyers.Text = "Surveyors";
            // 
            // dataGridViewConveyanceObjects
            // 
            this.dataGridViewConveyanceObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewConveyanceObjects.AutoGenerateColumns = false;
            this.dataGridViewConveyanceObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConveyanceObjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.global_id,
            this.usnodeDataGridViewTextBoxColumn,
            this.dsnodeDataGridViewTextBoxColumn,
            this.LinkType,
            this.nodeDataGridViewTextBoxColumn,
            this.shape_type_id,
            this.dimension1DataGridViewTextBoxColumn,
            this.dimension2DataGridViewTextBoxColumn,
            this.dimension3DataGridViewTextBoxColumn,
            this.material_type_id,
            this.culvert_opening_type_id,
            this.lengthftDataGridViewTextBoxColumn,
            this.usdepthinDataGridViewTextBoxColumn,
            this.dsdepthinDataGridViewTextBoxColumn,
            this.view_id,
            this.survey_page_id,
            this.watershed_id,
            this.subwatershed_id,
            this.actionDataGridViewTextBoxColumn});
            this.dataGridViewConveyanceObjects.DataSource = this.mESHPAGESMESHBindingSource;
            this.dataGridViewConveyanceObjects.Location = new System.Drawing.Point(15, 215);
            this.dataGridViewConveyanceObjects.Name = "dataGridViewConveyanceObjects";
            this.dataGridViewConveyanceObjects.RowHeadersWidth = 20;
            this.dataGridViewConveyanceObjects.Size = new System.Drawing.Size(967, 120);
            this.dataGridViewConveyanceObjects.TabIndex = 9;
            this.dataGridViewConveyanceObjects.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewConveyanceObjects_CellValueChanged);
            this.dataGridViewConveyanceObjects.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewConveyanceObjects_DataError);
            this.dataGridViewConveyanceObjects.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewConveyanceObjects_DefaultValuesNeeded);
            // 
            // mESHSHAPETYPEBindingSource
            // 
            this.mESHSHAPETYPEBindingSource.DataMember = "MESH_SHAPE_TYPE";
            this.mESHSHAPETYPEBindingSource.DataSource = this.sWIDataSet;
            // 
            // mESHPAGESMESHBindingSource
            // 
            this.mESHPAGESMESHBindingSource.AllowNew = true;
            this.mESHPAGESMESHBindingSource.DataMember = "MESH_PAGES_MESH";
            this.mESHPAGESMESHBindingSource.DataSource = this.mESHVIEWSMESHPAGESBindingSource;
            // 
            // mESHMATERIALTYPEBindingSource
            // 
            this.mESHMATERIALTYPEBindingSource.DataMember = "MESH_MATERIAL_TYPE";
            this.mESHMATERIALTYPEBindingSource.DataSource = this.sWIDataSet;
            // 
            // mESHCULVERTOPENINGTYPEBindingSource
            // 
            this.mESHCULVERTOPENINGTYPEBindingSource.DataMember = "MESH_CULVERT_OPENING_TYPE";
            this.mESHCULVERTOPENINGTYPEBindingSource.DataSource = this.sWIDataSet;
            // 
            // dataGridViewPhotos
            // 
            this.dataGridViewPhotos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPhotos.AutoGenerateColumns = false;
            this.dataGridViewPhotos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPhotos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.photoidDataGridViewTextBoxColumn,
            this.globalidDataGridViewTextBoxColumn1,
            this.locationDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.dataGridViewPhotos.DataSource = this.mESHMESHPHOTOSBindingSource;
            this.dataGridViewPhotos.Location = new System.Drawing.Point(15, 354);
            this.dataGridViewPhotos.Name = "dataGridViewPhotos";
            this.dataGridViewPhotos.RowHeadersWidth = 20;
            this.dataGridViewPhotos.Size = new System.Drawing.Size(967, 87);
            this.dataGridViewPhotos.TabIndex = 10;
            // 
            // photoidDataGridViewTextBoxColumn
            // 
            this.photoidDataGridViewTextBoxColumn.DataPropertyName = "photo_id";
            this.photoidDataGridViewTextBoxColumn.HeaderText = "photo_id";
            this.photoidDataGridViewTextBoxColumn.Name = "photoidDataGridViewTextBoxColumn";
            this.photoidDataGridViewTextBoxColumn.Visible = false;
            // 
            // globalidDataGridViewTextBoxColumn1
            // 
            this.globalidDataGridViewTextBoxColumn1.DataPropertyName = "global_id";
            this.globalidDataGridViewTextBoxColumn1.HeaderText = "global_id";
            this.globalidDataGridViewTextBoxColumn1.Name = "globalidDataGridViewTextBoxColumn1";
            this.globalidDataGridViewTextBoxColumn1.Visible = false;
            // 
            // locationDataGridViewTextBoxColumn
            // 
            this.locationDataGridViewTextBoxColumn.DataPropertyName = "location";
            this.locationDataGridViewTextBoxColumn.HeaderText = "location";
            this.locationDataGridViewTextBoxColumn.Name = "locationDataGridViewTextBoxColumn";
            this.locationDataGridViewTextBoxColumn.Width = 350;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.Width = 500;
            // 
            // mESHMESHPHOTOSBindingSource
            // 
            this.mESHMESHPHOTOSBindingSource.AllowNew = true;
            this.mESHMESHPHOTOSBindingSource.DataMember = "MESH_MESH_PHOTOS";
            this.mESHMESHPHOTOSBindingSource.DataSource = this.mESHPAGESMESHBindingSource;
            // 
            // labelConveyanceObjects
            // 
            this.labelConveyanceObjects.AutoSize = true;
            this.labelConveyanceObjects.Location = new System.Drawing.Point(17, 199);
            this.labelConveyanceObjects.Name = "labelConveyanceObjects";
            this.labelConveyanceObjects.Size = new System.Drawing.Size(106, 13);
            this.labelConveyanceObjects.TabIndex = 11;
            this.labelConveyanceObjects.Text = "Conveyance Objects";
            // 
            // labelPhotos
            // 
            this.labelPhotos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPhotos.AutoSize = true;
            this.labelPhotos.Location = new System.Drawing.Point(12, 338);
            this.labelPhotos.Name = "labelPhotos";
            this.labelPhotos.Size = new System.Drawing.Size(40, 13);
            this.labelPhotos.TabIndex = 12;
            this.labelPhotos.Text = "Photos";
            // 
            // labelComments
            // 
            this.labelComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelComments.AutoSize = true;
            this.labelComments.Location = new System.Drawing.Point(12, 454);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(56, 13);
            this.labelComments.TabIndex = 13;
            this.labelComments.Text = "Comments";
            // 
            // textBoxComments
            // 
            this.textBoxComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComments.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mESHVIEWSMESHPAGESBindingSource, "comment", true));
            this.textBoxComments.Location = new System.Drawing.Point(15, 471);
            this.textBoxComments.Multiline = true;
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.Size = new System.Drawing.Size(967, 66);
            this.textBoxComments.TabIndex = 14;
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.mESHWATERSHEDBindingSource;
            this.comboBox1.DisplayMember = "watershed";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 54);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.ValueMember = "watershed_id";
            // 
            // comboBox2
            // 
            this.comboBox2.DataSource = this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource;
            this.comboBox2.DisplayMember = "subwatershed";
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(160, 54);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 16;
            this.comboBox2.ValueMember = "subwatershed_id";
            // 
            // comboBox3
            // 
            this.comboBox3.DataSource = this.mESHSUBWATERSHEDMESHVIEWSBindingSource;
            this.comboBox3.DisplayMember = "view_number";
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(305, 54);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 17;
            this.comboBox3.ValueMember = "view_id";
            // 
            // comboBox4
            // 
            this.comboBox4.DataSource = this.mESHVIEWSMESHPAGESBindingSource;
            this.comboBox4.DisplayMember = "page_number";
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(450, 54);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(121, 21);
            this.comboBox4.TabIndex = 18;
            this.comboBox4.ValueMember = "survey_page_id";
            // 
            // mESH_WATERSHEDTableAdapter
            // 
            this.mESH_WATERSHEDTableAdapter.ClearBeforeFill = true;
            // 
            // mESH_SUBWATERSHEDTableAdapter
            // 
            this.mESH_SUBWATERSHEDTableAdapter.ClearBeforeFill = true;
            // 
            // mESH_VIEWSTableAdapter
            // 
            this.mESH_VIEWSTableAdapter.ClearBeforeFill = true;
            // 
            // mESH_PAGESTableAdapter
            // 
            this.mESH_PAGESTableAdapter.ClearBeforeFill = true;
            // 
            // mESHTableAdapter
            // 
            this.mESHTableAdapter.ClearBeforeFill = true;
            // 
            // mESH_PHOTOSTableAdapter
            // 
            this.mESH_PHOTOSTableAdapter.ClearBeforeFill = true;
            // 
            // textBoxWeather
            // 
            this.textBoxWeather.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mESHVIEWSMESHPAGESBindingSource, "weather", true));
            this.textBoxWeather.Location = new System.Drawing.Point(160, 109);
            this.textBoxWeather.Name = "textBoxWeather";
            this.textBoxWeather.Size = new System.Drawing.Size(121, 20);
            this.textBoxWeather.TabIndex = 20;
            // 
            // mESH_PAGE_EVALUATORSTableAdapter
            // 
            this.mESH_PAGE_EVALUATORSTableAdapter.ClearBeforeFill = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.mESHVIEWSMESHPAGESBindingSource, "date", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "d"));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(15, 109);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 20);
            this.dateTimePicker1.TabIndex = 21;
            // 
            // mESH_SHAPE_TYPETableAdapter
            // 
            this.mESH_SHAPE_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // mESH_MATERIAL_TYPETableAdapter
            // 
            this.mESH_MATERIAL_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // mESH_CULVERT_OPENING_TYPETableAdapter
            // 
            this.mESH_CULVERT_OPENING_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // mESHSHAPETYPEMESHBindingSource
            // 
            this.mESHSHAPETYPEMESHBindingSource.DataMember = "MESH_SHAPE_TYPE_MESH";
            this.mESHSHAPETYPEMESHBindingSource.DataSource = this.mESHSHAPETYPEBindingSource;
            // 
            // mESHSHAPETYPEMESHBindingSource1
            // 
            this.mESHSHAPETYPEMESHBindingSource1.DataMember = "MESH_SHAPE_TYPE_MESH";
            this.mESHSHAPETYPEMESHBindingSource1.DataSource = this.mESHSHAPETYPEBindingSource;
            // 
            // mESHSHAPETYPEMESHBindingSource2
            // 
            this.mESHSHAPETYPEMESHBindingSource2.DataMember = "MESH_SHAPE_TYPE_MESH";
            this.mESHSHAPETYPEMESHBindingSource2.DataSource = this.mESHSHAPETYPEBindingSource;
            // 
            // global_id
            // 
            this.global_id.DataPropertyName = "global_id";
            this.global_id.HeaderText = "global_id";
            this.global_id.Name = "global_id";
            this.global_id.Visible = false;
            this.global_id.Width = 75;
            // 
            // usnodeDataGridViewTextBoxColumn
            // 
            this.usnodeDataGridViewTextBoxColumn.DataPropertyName = "us_node";
            this.usnodeDataGridViewTextBoxColumn.HeaderText = "US Node";
            this.usnodeDataGridViewTextBoxColumn.Name = "usnodeDataGridViewTextBoxColumn";
            this.usnodeDataGridViewTextBoxColumn.Width = 75;
            // 
            // dsnodeDataGridViewTextBoxColumn
            // 
            this.dsnodeDataGridViewTextBoxColumn.DataPropertyName = "ds_node";
            this.dsnodeDataGridViewTextBoxColumn.HeaderText = "DS Node";
            this.dsnodeDataGridViewTextBoxColumn.Name = "dsnodeDataGridViewTextBoxColumn";
            this.dsnodeDataGridViewTextBoxColumn.Width = 75;
            // 
            // LinkType
            // 
            this.LinkType.DataPropertyName = "LinkType";
            this.LinkType.HeaderText = "Link Type";
            this.LinkType.Items.AddRange(new object[] {
            "Pipe",
            "Culvert",
            "Ditch"});
            this.LinkType.Name = "LinkType";
            this.LinkType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LinkType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.LinkType.Width = 75;
            // 
            // nodeDataGridViewTextBoxColumn
            // 
            this.nodeDataGridViewTextBoxColumn.DataPropertyName = "node";
            this.nodeDataGridViewTextBoxColumn.HeaderText = "Meas. Location";
            this.nodeDataGridViewTextBoxColumn.Name = "nodeDataGridViewTextBoxColumn";
            this.nodeDataGridViewTextBoxColumn.Width = 75;
            // 
            // shape_type_id
            // 
            this.shape_type_id.DataPropertyName = "shape_type_id";
            this.shape_type_id.DataSource = this.mESHSHAPETYPEBindingSource;
            this.shape_type_id.DisplayMember = "shape";
            this.shape_type_id.HeaderText = "Shape";
            this.shape_type_id.Name = "shape_type_id";
            this.shape_type_id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.shape_type_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.shape_type_id.ValueMember = "shape_type_id";
            this.shape_type_id.Width = 75;
            // 
            // dimension1DataGridViewTextBoxColumn
            // 
            this.dimension1DataGridViewTextBoxColumn.DataPropertyName = "dimension1";
            this.dimension1DataGridViewTextBoxColumn.HeaderText = "Pipe Diam or Height/ Top(in) [Opening Depth]";
            this.dimension1DataGridViewTextBoxColumn.Name = "dimension1DataGridViewTextBoxColumn";
            this.dimension1DataGridViewTextBoxColumn.Width = 110;
            // 
            // dimension2DataGridViewTextBoxColumn
            // 
            this.dimension2DataGridViewTextBoxColumn.DataPropertyName = "dimension2";
            this.dimension2DataGridViewTextBoxColumn.HeaderText = "Pipe Width/ Bottom (in)";
            this.dimension2DataGridViewTextBoxColumn.Name = "dimension2DataGridViewTextBoxColumn";
            // 
            // dimension3DataGridViewTextBoxColumn
            // 
            this.dimension3DataGridViewTextBoxColumn.DataPropertyName = "dimension3";
            this.dimension3DataGridViewTextBoxColumn.HeaderText = "Unobstructed Height (in)";
            this.dimension3DataGridViewTextBoxColumn.Name = "dimension3DataGridViewTextBoxColumn";
            this.dimension3DataGridViewTextBoxColumn.Width = 75;
            // 
            // material_type_id
            // 
            this.material_type_id.DataPropertyName = "material_type_id";
            this.material_type_id.DataSource = this.mESHMATERIALTYPEBindingSource;
            this.material_type_id.DisplayMember = "material";
            this.material_type_id.HeaderText = "Material";
            this.material_type_id.Name = "material_type_id";
            this.material_type_id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.material_type_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.material_type_id.ValueMember = "material_type_id";
            this.material_type_id.Width = 75;
            // 
            // culvert_opening_type_id
            // 
            this.culvert_opening_type_id.DataPropertyName = "culvert_opening_type_id";
            this.culvert_opening_type_id.DataSource = this.mESHCULVERTOPENINGTYPEBindingSource;
            this.culvert_opening_type_id.DisplayMember = "culvert_opening";
            this.culvert_opening_type_id.HeaderText = "L, P, O";
            this.culvert_opening_type_id.Name = "culvert_opening_type_id";
            this.culvert_opening_type_id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.culvert_opening_type_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.culvert_opening_type_id.ValueMember = "culvert_opening_type_id";
            this.culvert_opening_type_id.Width = 50;
            // 
            // lengthftDataGridViewTextBoxColumn
            // 
            this.lengthftDataGridViewTextBoxColumn.DataPropertyName = "length_ft";
            this.lengthftDataGridViewTextBoxColumn.HeaderText = "Length (ft)";
            this.lengthftDataGridViewTextBoxColumn.Name = "lengthftDataGridViewTextBoxColumn";
            this.lengthftDataGridViewTextBoxColumn.Width = 50;
            // 
            // usdepthinDataGridViewTextBoxColumn
            // 
            this.usdepthinDataGridViewTextBoxColumn.DataPropertyName = "us_depth_in";
            this.usdepthinDataGridViewTextBoxColumn.HeaderText = "Depth in (in)";
            this.usdepthinDataGridViewTextBoxColumn.Name = "usdepthinDataGridViewTextBoxColumn";
            this.usdepthinDataGridViewTextBoxColumn.Width = 50;
            // 
            // dsdepthinDataGridViewTextBoxColumn
            // 
            this.dsdepthinDataGridViewTextBoxColumn.DataPropertyName = "ds_depth_in";
            this.dsdepthinDataGridViewTextBoxColumn.HeaderText = "Depth out (in)";
            this.dsdepthinDataGridViewTextBoxColumn.Name = "dsdepthinDataGridViewTextBoxColumn";
            this.dsdepthinDataGridViewTextBoxColumn.Width = 50;
            // 
            // view_id
            // 
            this.view_id.DataPropertyName = "view_id";
            this.view_id.HeaderText = "view_id";
            this.view_id.Name = "view_id";
            this.view_id.Visible = false;
            this.view_id.Width = 75;
            // 
            // survey_page_id
            // 
            this.survey_page_id.DataPropertyName = "survey_page_id";
            this.survey_page_id.HeaderText = "survey_page_id";
            this.survey_page_id.Name = "survey_page_id";
            this.survey_page_id.Visible = false;
            this.survey_page_id.Width = 75;
            // 
            // watershed_id
            // 
            this.watershed_id.DataPropertyName = "watershed_id";
            this.watershed_id.HeaderText = "watershed_id";
            this.watershed_id.Name = "watershed_id";
            this.watershed_id.Visible = false;
            this.watershed_id.Width = 75;
            // 
            // subwatershed_id
            // 
            this.subwatershed_id.DataPropertyName = "subwatershed_id";
            this.subwatershed_id.HeaderText = "subwatershed_id";
            this.subwatershed_id.Name = "subwatershed_id";
            this.subwatershed_id.Visible = false;
            this.subwatershed_id.Width = 75;
            // 
            // actionDataGridViewTextBoxColumn
            // 
            this.actionDataGridViewTextBoxColumn.DataPropertyName = "action";
            this.actionDataGridViewTextBoxColumn.HeaderText = "action";
            this.actionDataGridViewTextBoxColumn.Name = "actionDataGridViewTextBoxColumn";
            this.actionDataGridViewTextBoxColumn.Visible = false;
            this.actionDataGridViewTextBoxColumn.Width = 75;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 549);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBoxWeather);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBoxComments);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.labelPhotos);
            this.Controls.Add(this.labelConveyanceObjects);
            this.Controls.Add(this.dataGridViewPhotos);
            this.Controls.Add(this.dataGridViewConveyanceObjects);
            this.Controls.Add(this.labelSurveyers);
            this.Controls.Add(this.dataGridViewSurveyors);
            this.Controls.Add(this.labelWeather);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelView);
            this.Controls.Add(this.labelMap);
            this.Controls.Add(this.labelSubwatershed);
            this.Controls.Add(this.labelWatershed);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SWI Input Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSurveyors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHPAGESMESHPAGEEVALUATORSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHVIEWSMESHPAGESBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHSUBWATERSHEDMESHVIEWSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHWATERSHEDMESHSUBWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWIDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConveyanceObjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHSHAPETYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHPAGESMESHBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHMATERIALTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHCULVERTOPENINGTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPhotos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHMESHPHOTOSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHSHAPETYPEMESHBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHSHAPETYPEMESHBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESHSHAPETYPEMESHBindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripNumberControl : ToolStripControlHost
    {
        public ToolStripNumberControl()
            : base(new NumericUpDown())
        {

        }

        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            ((NumericUpDown)control).ValueChanged += new EventHandler(OnValueChanged);
        }

        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            ((NumericUpDown)control).ValueChanged -= new EventHandler(OnValueChanged);
        }

        public event EventHandler ValueChanged;

        public Control NumericUpDownControl
        {
            get { return Control as NumericUpDown; }
        }

        public void OnValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }
    }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapsAndViewsToolStripMenuItem;
        private System.Windows.Forms.Label labelWatershed;
        private System.Windows.Forms.Label labelSubwatershed;
        private System.Windows.Forms.Label labelMap;
        private System.Windows.Forms.Label labelView;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelWeather;
        private System.Windows.Forms.DataGridView dataGridViewSurveyors;
        private System.Windows.Forms.Label labelSurveyers;
        private System.Windows.Forms.DataGridView dataGridViewConveyanceObjects;
        private System.Windows.Forms.DataGridView dataGridViewPhotos;
        private System.Windows.Forms.Label labelConveyanceObjects;
        private System.Windows.Forms.Label labelPhotos;
        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private SWIDataSet sWIDataSet;
        private System.Windows.Forms.BindingSource mESHWATERSHEDBindingSource;
        private SWIDataSetTableAdapters.MESH_WATERSHEDTableAdapter mESH_WATERSHEDTableAdapter;
        private System.Windows.Forms.BindingSource mESHWATERSHEDMESHSUBWATERSHEDBindingSource;
        private SWIDataSetTableAdapters.MESH_SUBWATERSHEDTableAdapter mESH_SUBWATERSHEDTableAdapter;
        private System.Windows.Forms.BindingSource mESHSUBWATERSHEDMESHVIEWSBindingSource;
        private SWIDataSetTableAdapters.MESH_VIEWSTableAdapter mESH_VIEWSTableAdapter;
        private System.Windows.Forms.BindingSource mESHVIEWSMESHPAGESBindingSource;
        private SWIDataSetTableAdapters.MESH_PAGESTableAdapter mESH_PAGESTableAdapter;
        private System.Windows.Forms.BindingSource mESHPAGESMESHBindingSource;
        private SWIDataSetTableAdapters.MESHTableAdapter mESHTableAdapter;
        private System.Windows.Forms.BindingSource mESHMESHPHOTOSBindingSource;
        private SWIDataSetTableAdapters.MESH_PHOTOSTableAdapter mESH_PHOTOSTableAdapter;
        private System.Windows.Forms.TextBox textBoxWeather;
        private System.Windows.Forms.BindingSource mESHPAGESMESHPAGEEVALUATORSBindingSource;
        private SWIDataSetTableAdapters.MESH_PAGE_EVALUATORSTableAdapter mESH_PAGE_EVALUATORSTableAdapter;
        private System.Windows.Forms.ToolStripMenuItem saveChangesToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn surveypageidDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn evaluatoridDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn initialsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn presentDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem addNewViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAndSmellTheRosesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewMapToolStripMenuItem;
        private ToolStripNumberControl ToolStripNUDView;
        private ToolStripNumberControl ToolStripNUDPage;
        ToolStripButton ToolStripViewGoButton;
        ToolStripButton ToolStripMapGoButton;
        private DataGridViewTextBoxColumn photoidDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn globalidDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn locationDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private DateTimePicker dateTimePicker1;
        private BindingSource mESHSHAPETYPEBindingSource;
        private SWIDataSetTableAdapters.MESH_SHAPE_TYPETableAdapter mESH_SHAPE_TYPETableAdapter;
        private BindingSource mESHMATERIALTYPEBindingSource;
        private SWIDataSetTableAdapters.MESH_MATERIAL_TYPETableAdapter mESH_MATERIAL_TYPETableAdapter;
        private BindingSource mESHCULVERTOPENINGTYPEBindingSource;
        private SWIDataSetTableAdapters.MESH_CULVERT_OPENING_TYPETableAdapter mESH_CULVERT_OPENING_TYPETableAdapter;
        private BindingSource mESHSHAPETYPEMESHBindingSource;
        private BindingSource mESHSHAPETYPEMESHBindingSource1;
        private BindingSource mESHSHAPETYPEMESHBindingSource2;
        private DataGridViewTextBoxColumn global_id;
        private DataGridViewTextBoxColumn usnodeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dsnodeDataGridViewTextBoxColumn;
        private DataGridViewComboBoxColumn LinkType;
        private DataGridViewTextBoxColumn nodeDataGridViewTextBoxColumn;
        private DataGridViewComboBoxColumn shape_type_id;
        private DataGridViewTextBoxColumn dimension1DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dimension2DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dimension3DataGridViewTextBoxColumn;
        private DataGridViewComboBoxColumn material_type_id;
        private DataGridViewComboBoxColumn culvert_opening_type_id;
        private DataGridViewTextBoxColumn lengthftDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn usdepthinDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dsdepthinDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn view_id;
        private DataGridViewTextBoxColumn survey_page_id;
        private DataGridViewTextBoxColumn watershed_id;
        private DataGridViewTextBoxColumn subwatershed_id;
        private DataGridViewTextBoxColumn actionDataGridViewTextBoxColumn;
    }
}

