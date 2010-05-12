namespace StormwaterInterface
{
    partial class FormStormwaterInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStormwaterInterface));
            this.textBoxWatershed = new System.Windows.Forms.TextBox();
            this.evaluatorPageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fieldWorkDBDataSet = new StormwaterInterface.FieldWorkDBDataSet();
            this.labelWatershed = new System.Windows.Forms.Label();
            this.labelSubWatershed = new System.Windows.Forms.Label();
            this.textBoxSubWatershed = new System.Windows.Forms.TextBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.labelEvaluator = new System.Windows.Forms.Label();
            this.textBoxEvaluator = new System.Windows.Forms.TextBox();
            this.labelMapNo = new System.Windows.Forms.Label();
            this.textBoxMapNo = new System.Windows.Forms.TextBox();
            this.labelGPS = new System.Windows.Forms.Label();
            this.textBoxGPS = new System.Windows.Forms.TextBox();
            this.labelWeather = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelCulvertsAndDitchesInformation = new System.Windows.Forms.Label();
            this.labelLocation = new System.Windows.Forms.Label();
            this.labelDitches = new System.Windows.Forms.Label();
            this.labelComments = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lPODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shapeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pipeDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.culvDepthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.culvMaterialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.culvPhotoIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ditchDepthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.widthTopDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.widthBottomDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ditchMaterialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ditchPhotoIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ditchesCulvertsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelCulverts = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonUpdateDatabase = new System.Windows.Forms.Button();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.labelPageID = new System.Windows.Forms.Label();
            this.textBoxPageID = new System.Windows.Forms.TextBox();
            this.buttonAddANode = new System.Windows.Forms.Button();
            this.buttonDeleteSelectedNode = new System.Windows.Forms.Button();
            this.labelPage = new System.Windows.Forms.Label();
            this.textBoxPage = new System.Windows.Forms.TextBox();
            this.ditchesCulvertsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.viewCulvertsDitchesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.view_CulvertsDitchesTableAdapter = new StormwaterInterface.FieldWorkDBDataSetTableAdapters.View_CulvertsDitchesTableAdapter();
            this.ditchesCulvertsTableAdapter = new StormwaterInterface.FieldWorkDBDataSetTableAdapters.DitchesCulvertsTableAdapter();
            this.evaluatorPageTableAdapter = new StormwaterInterface.FieldWorkDBDataSetTableAdapters.EvaluatorPageTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.evaluatorPageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldWorkDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ditchesCulvertsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ditchesCulvertsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCulvertsDitchesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxWatershed
            // 
            this.textBoxWatershed.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.evaluatorPageBindingSource, "Watershed", true));
            this.textBoxWatershed.Location = new System.Drawing.Point(93, 45);
            this.textBoxWatershed.Name = "textBoxWatershed";
            this.textBoxWatershed.Size = new System.Drawing.Size(245, 20);
            this.textBoxWatershed.TabIndex = 1;
            // 
            // evaluatorPageBindingSource
            // 
            this.evaluatorPageBindingSource.DataMember = "EvaluatorPage";
            this.evaluatorPageBindingSource.DataSource = this.fieldWorkDBDataSet;
            // 
            // fieldWorkDBDataSet
            // 
            this.fieldWorkDBDataSet.DataSetName = "FieldWorkDBDataSet";
            this.fieldWorkDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // labelWatershed
            // 
            this.labelWatershed.AutoSize = true;
            this.labelWatershed.Location = new System.Drawing.Point(12, 46);
            this.labelWatershed.Name = "labelWatershed";
            this.labelWatershed.Size = new System.Drawing.Size(62, 13);
            this.labelWatershed.TabIndex = 1;
            this.labelWatershed.Text = "Watershed:";
            // 
            // labelSubWatershed
            // 
            this.labelSubWatershed.AutoSize = true;
            this.labelSubWatershed.Location = new System.Drawing.Point(365, 45);
            this.labelSubWatershed.Name = "labelSubWatershed";
            this.labelSubWatershed.Size = new System.Drawing.Size(78, 13);
            this.labelSubWatershed.TabIndex = 3;
            this.labelSubWatershed.Text = "Subwatershed:";
            // 
            // textBoxSubWatershed
            // 
            this.textBoxSubWatershed.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.evaluatorPageBindingSource, "Subwatershed", true));
            this.textBoxSubWatershed.Location = new System.Drawing.Point(446, 44);
            this.textBoxSubWatershed.Name = "textBoxSubWatershed";
            this.textBoxSubWatershed.Size = new System.Drawing.Size(245, 20);
            this.textBoxSubWatershed.TabIndex = 2;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(12, 72);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(33, 13);
            this.labelDate.TabIndex = 5;
            this.labelDate.Text = "Date:";
            // 
            // textBoxDate
            // 
            this.textBoxDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.evaluatorPageBindingSource, "Date", true));
            this.textBoxDate.Location = new System.Drawing.Point(93, 71);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(157, 20);
            this.textBoxDate.TabIndex = 4;
            // 
            // labelEvaluator
            // 
            this.labelEvaluator.AutoSize = true;
            this.labelEvaluator.Location = new System.Drawing.Point(275, 73);
            this.labelEvaluator.Name = "labelEvaluator";
            this.labelEvaluator.Size = new System.Drawing.Size(55, 13);
            this.labelEvaluator.TabIndex = 7;
            this.labelEvaluator.Text = "Evaluator:";
            // 
            // textBoxEvaluator
            // 
            this.textBoxEvaluator.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.evaluatorPageBindingSource, "Evaluator", true));
            this.textBoxEvaluator.Location = new System.Drawing.Point(356, 72);
            this.textBoxEvaluator.Name = "textBoxEvaluator";
            this.textBoxEvaluator.Size = new System.Drawing.Size(154, 20);
            this.textBoxEvaluator.TabIndex = 5;
            // 
            // labelMapNo
            // 
            this.labelMapNo.AutoSize = true;
            this.labelMapNo.Location = new System.Drawing.Point(562, 74);
            this.labelMapNo.Name = "labelMapNo";
            this.labelMapNo.Size = new System.Drawing.Size(48, 13);
            this.labelMapNo.TabIndex = 9;
            this.labelMapNo.Text = "Map No:";
            // 
            // textBoxMapNo
            // 
            this.textBoxMapNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.evaluatorPageBindingSource, "MapNo", true));
            this.textBoxMapNo.Location = new System.Drawing.Point(643, 73);
            this.textBoxMapNo.Name = "textBoxMapNo";
            this.textBoxMapNo.Size = new System.Drawing.Size(154, 20);
            this.textBoxMapNo.TabIndex = 6;
            // 
            // labelGPS
            // 
            this.labelGPS.AutoSize = true;
            this.labelGPS.Location = new System.Drawing.Point(12, 98);
            this.labelGPS.Name = "labelGPS";
            this.labelGPS.Size = new System.Drawing.Size(32, 13);
            this.labelGPS.TabIndex = 11;
            this.labelGPS.Text = "GPS:";
            // 
            // textBoxGPS
            // 
            this.textBoxGPS.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.evaluatorPageBindingSource, "GPS", true));
            this.textBoxGPS.Location = new System.Drawing.Point(93, 97);
            this.textBoxGPS.Name = "textBoxGPS";
            this.textBoxGPS.Size = new System.Drawing.Size(157, 20);
            this.textBoxGPS.TabIndex = 7;
            // 
            // labelWeather
            // 
            this.labelWeather.AutoSize = true;
            this.labelWeather.Location = new System.Drawing.Point(12, 124);
            this.labelWeather.Name = "labelWeather";
            this.labelWeather.Size = new System.Drawing.Size(51, 13);
            this.labelWeather.TabIndex = 13;
            this.labelWeather.Text = "Weather:";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.evaluatorPageBindingSource, "Weather", true));
            this.textBox1.Location = new System.Drawing.Point(93, 123);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(157, 20);
            this.textBox1.TabIndex = 8;
            // 
            // labelCulvertsAndDitchesInformation
            // 
            this.labelCulvertsAndDitchesInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCulvertsAndDitchesInformation.Location = new System.Drawing.Point(56, 149);
            this.labelCulvertsAndDitchesInformation.Name = "labelCulvertsAndDitchesInformation";
            this.labelCulvertsAndDitchesInformation.Size = new System.Drawing.Size(805, 26);
            this.labelCulvertsAndDitchesInformation.TabIndex = 14;
            this.labelCulvertsAndDitchesInformation.Text = "Culverts && Ditches Information";
            this.labelCulvertsAndDitchesInformation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelLocation
            // 
            this.labelLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLocation.Location = new System.Drawing.Point(56, 162);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(126, 24);
            this.labelLocation.TabIndex = 15;
            this.labelLocation.Text = "Location";
            // 
            // labelDitches
            // 
            this.labelDitches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDitches.Location = new System.Drawing.Point(556, 162);
            this.labelDitches.Name = "labelDitches";
            this.labelDitches.Size = new System.Drawing.Size(305, 24);
            this.labelDitches.TabIndex = 18;
            this.labelDitches.Text = "Ditches";
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.Location = new System.Drawing.Point(12, 411);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(59, 13);
            this.labelComments.TabIndex = 34;
            this.labelComments.Text = "Comments:";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(21, 392);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(243, 13);
            this.labelInfo.TabIndex = 35;
            this.labelInfo.Text = "*(L, P, O: L = Level; P = Perched; O = Obstructed)";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Position,
            this.nodeNoDataGridViewTextBoxColumn,
            this.lPODataGridViewTextBoxColumn,
            this.shapeDataGridViewTextBoxColumn,
            this.pipeDDataGridViewTextBoxColumn,
            this.culvDepthDataGridViewTextBoxColumn,
            this.culvMaterialDataGridViewTextBoxColumn,
            this.culvPhotoIDDataGridViewTextBoxColumn,
            this.ditchDepthDataGridViewTextBoxColumn,
            this.widthTopDataGridViewTextBoxColumn,
            this.widthBottomDataGridViewTextBoxColumn,
            this.ditchMaterialDataGridViewTextBoxColumn,
            this.ditchPhotoIDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.ditchesCulvertsBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(15, 178);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(846, 211);
            this.dataGridView1.TabIndex = 36;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Position
            // 
            this.Position.DataPropertyName = "Position";
            this.Position.HeaderText = "US/DS";
            this.Position.Name = "Position";
            this.Position.Width = 50;
            // 
            // nodeNoDataGridViewTextBoxColumn
            // 
            this.nodeNoDataGridViewTextBoxColumn.DataPropertyName = "Node_No";
            this.nodeNoDataGridViewTextBoxColumn.HeaderText = "Node No.";
            this.nodeNoDataGridViewTextBoxColumn.Name = "nodeNoDataGridViewTextBoxColumn";
            this.nodeNoDataGridViewTextBoxColumn.Width = 75;
            // 
            // lPODataGridViewTextBoxColumn
            // 
            this.lPODataGridViewTextBoxColumn.DataPropertyName = "LPO";
            this.lPODataGridViewTextBoxColumn.HeaderText = "L, P, O*";
            this.lPODataGridViewTextBoxColumn.Name = "lPODataGridViewTextBoxColumn";
            this.lPODataGridViewTextBoxColumn.Width = 25;
            // 
            // shapeDataGridViewTextBoxColumn
            // 
            this.shapeDataGridViewTextBoxColumn.DataPropertyName = "Shape";
            this.shapeDataGridViewTextBoxColumn.HeaderText = "Shape";
            this.shapeDataGridViewTextBoxColumn.Name = "shapeDataGridViewTextBoxColumn";
            this.shapeDataGridViewTextBoxColumn.Width = 60;
            // 
            // pipeDDataGridViewTextBoxColumn
            // 
            this.pipeDDataGridViewTextBoxColumn.DataPropertyName = "Pipe_D";
            this.pipeDDataGridViewTextBoxColumn.HeaderText = "Full Diameter";
            this.pipeDDataGridViewTextBoxColumn.Name = "pipeDDataGridViewTextBoxColumn";
            this.pipeDDataGridViewTextBoxColumn.ToolTipText = "(inches)";
            this.pipeDDataGridViewTextBoxColumn.Width = 70;
            // 
            // culvDepthDataGridViewTextBoxColumn
            // 
            this.culvDepthDataGridViewTextBoxColumn.DataPropertyName = "CulvDepth";
            this.culvDepthDataGridViewTextBoxColumn.HeaderText = "Opening Diameter";
            this.culvDepthDataGridViewTextBoxColumn.Name = "culvDepthDataGridViewTextBoxColumn";
            this.culvDepthDataGridViewTextBoxColumn.ToolTipText = "(inches)";
            this.culvDepthDataGridViewTextBoxColumn.Width = 70;
            // 
            // culvMaterialDataGridViewTextBoxColumn
            // 
            this.culvMaterialDataGridViewTextBoxColumn.DataPropertyName = "CulvMaterial";
            this.culvMaterialDataGridViewTextBoxColumn.HeaderText = "Material";
            this.culvMaterialDataGridViewTextBoxColumn.Name = "culvMaterialDataGridViewTextBoxColumn";
            this.culvMaterialDataGridViewTextBoxColumn.Width = 75;
            // 
            // culvPhotoIDDataGridViewTextBoxColumn
            // 
            this.culvPhotoIDDataGridViewTextBoxColumn.DataPropertyName = "CulvPhoto_ID";
            this.culvPhotoIDDataGridViewTextBoxColumn.HeaderText = "Photo ID";
            this.culvPhotoIDDataGridViewTextBoxColumn.Name = "culvPhotoIDDataGridViewTextBoxColumn";
            this.culvPhotoIDDataGridViewTextBoxColumn.Width = 75;
            // 
            // ditchDepthDataGridViewTextBoxColumn
            // 
            this.ditchDepthDataGridViewTextBoxColumn.DataPropertyName = "DitchDepth";
            this.ditchDepthDataGridViewTextBoxColumn.HeaderText = "Depth (ft)";
            this.ditchDepthDataGridViewTextBoxColumn.Name = "ditchDepthDataGridViewTextBoxColumn";
            this.ditchDepthDataGridViewTextBoxColumn.ToolTipText = "5\' from CVT";
            this.ditchDepthDataGridViewTextBoxColumn.Width = 50;
            // 
            // widthTopDataGridViewTextBoxColumn
            // 
            this.widthTopDataGridViewTextBoxColumn.DataPropertyName = "WidthTop";
            this.widthTopDataGridViewTextBoxColumn.HeaderText = "Width Top";
            this.widthTopDataGridViewTextBoxColumn.Name = "widthTopDataGridViewTextBoxColumn";
            this.widthTopDataGridViewTextBoxColumn.ToolTipText = "(ft)";
            this.widthTopDataGridViewTextBoxColumn.Width = 50;
            // 
            // widthBottomDataGridViewTextBoxColumn
            // 
            this.widthBottomDataGridViewTextBoxColumn.DataPropertyName = "WidthBottom";
            this.widthBottomDataGridViewTextBoxColumn.HeaderText = "Width Bottom";
            this.widthBottomDataGridViewTextBoxColumn.Name = "widthBottomDataGridViewTextBoxColumn";
            this.widthBottomDataGridViewTextBoxColumn.ToolTipText = "(ft)";
            this.widthBottomDataGridViewTextBoxColumn.Width = 50;
            // 
            // ditchMaterialDataGridViewTextBoxColumn
            // 
            this.ditchMaterialDataGridViewTextBoxColumn.DataPropertyName = "DitchMaterial";
            this.ditchMaterialDataGridViewTextBoxColumn.HeaderText = "Material";
            this.ditchMaterialDataGridViewTextBoxColumn.Name = "ditchMaterialDataGridViewTextBoxColumn";
            this.ditchMaterialDataGridViewTextBoxColumn.Width = 75;
            // 
            // ditchPhotoIDDataGridViewTextBoxColumn
            // 
            this.ditchPhotoIDDataGridViewTextBoxColumn.DataPropertyName = "DitchPhoto_ID";
            this.ditchPhotoIDDataGridViewTextBoxColumn.HeaderText = "Photo ID";
            this.ditchPhotoIDDataGridViewTextBoxColumn.Name = "ditchPhotoIDDataGridViewTextBoxColumn";
            this.ditchPhotoIDDataGridViewTextBoxColumn.Width = 75;
            // 
            // ditchesCulvertsBindingSource
            // 
            this.ditchesCulvertsBindingSource.DataMember = "DitchesCulverts";
            this.ditchesCulvertsBindingSource.DataSource = this.fieldWorkDBDataSet;
            this.ditchesCulvertsBindingSource.CurrentChanged += new System.EventHandler(this.ditchesCulvertsBindingSource_CurrentChanged);
            // 
            // labelCulverts
            // 
            this.labelCulverts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCulverts.Location = new System.Drawing.Point(181, 162);
            this.labelCulverts.Name = "labelCulverts";
            this.labelCulverts.Size = new System.Drawing.Size(376, 24);
            this.labelCulverts.TabIndex = 37;
            this.labelCulverts.Text = "Culverts";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.evaluatorPageBindingSource, "Comments", true));
            this.textBox2.Location = new System.Drawing.Point(15, 427);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(846, 36);
            this.textBox2.TabIndex = 38;
            // 
            // buttonUpdateDatabase
            // 
            this.buttonUpdateDatabase.Location = new System.Drawing.Point(730, 469);
            this.buttonUpdateDatabase.Name = "buttonUpdateDatabase";
            this.buttonUpdateDatabase.Size = new System.Drawing.Size(131, 22);
            this.buttonUpdateDatabase.TabIndex = 39;
            this.buttonUpdateDatabase.Text = "Update Database";
            this.buttonUpdateDatabase.UseVisualStyleBackColor = true;
            this.buttonUpdateDatabase.Click += new System.EventHandler(this.buttonUpdateDatabase_Click);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.BindingSource = this.evaluatorPageBindingSource;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.bindingNavigator1.Size = new System.Drawing.Size(873, 25);
            this.bindingNavigator1.TabIndex = 40;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(34, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            this.bindingNavigatorMoveFirstItem.Click += new System.EventHandler(this.bindingNavigatorMoveFirstItem_Click);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            this.bindingNavigatorMovePreviousItem.Click += new System.EventHandler(this.bindingNavigatorMovePreviousItem_Click);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(100, 25);
            this.bindingNavigatorPositionItem.Text = "0";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            this.bindingNavigatorMoveNextItem.Click += new System.EventHandler(this.bindingNavigatorMoveNextItem_Click);
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            this.bindingNavigatorMoveLastItem.Click += new System.EventHandler(this.bindingNavigatorMoveLastItem_Click);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // labelPageID
            // 
            this.labelPageID.AutoSize = true;
            this.labelPageID.Enabled = false;
            this.labelPageID.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.labelPageID.Location = new System.Drawing.Point(562, 105);
            this.labelPageID.Name = "labelPageID";
            this.labelPageID.Size = new System.Drawing.Size(46, 13);
            this.labelPageID.TabIndex = 41;
            this.labelPageID.Text = "PageID:";
            this.labelPageID.Visible = false;
            // 
            // textBoxPageID
            // 
            this.textBoxPageID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.evaluatorPageBindingSource, "EvaluatorPageID", true));
            this.textBoxPageID.Enabled = false;
            this.textBoxPageID.Location = new System.Drawing.Point(643, 102);
            this.textBoxPageID.Name = "textBoxPageID";
            this.textBoxPageID.Size = new System.Drawing.Size(0, 20);
            this.textBoxPageID.TabIndex = 42;
            // 
            // buttonAddANode
            // 
            this.buttonAddANode.Location = new System.Drawing.Point(728, 393);
            this.buttonAddANode.Name = "buttonAddANode";
            this.buttonAddANode.Size = new System.Drawing.Size(133, 30);
            this.buttonAddANode.TabIndex = 43;
            this.buttonAddANode.Text = "Add A Node";
            this.buttonAddANode.UseVisualStyleBackColor = true;
            this.buttonAddANode.Click += new System.EventHandler(this.buttonAddANode_Click);
            // 
            // buttonDeleteSelectedNode
            // 
            this.buttonDeleteSelectedNode.Location = new System.Drawing.Point(544, 393);
            this.buttonDeleteSelectedNode.Name = "buttonDeleteSelectedNode";
            this.buttonDeleteSelectedNode.Size = new System.Drawing.Size(124, 30);
            this.buttonDeleteSelectedNode.TabIndex = 44;
            this.buttonDeleteSelectedNode.Text = "Delete Selected Node";
            this.buttonDeleteSelectedNode.UseVisualStyleBackColor = true;
            this.buttonDeleteSelectedNode.Click += new System.EventHandler(this.buttonDeleteSelectedNode_Click);
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(707, 43);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(35, 13);
            this.labelPage.TabIndex = 45;
            this.labelPage.Text = "Page:";
            // 
            // textBoxPage
            // 
            this.textBoxPage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.evaluatorPageBindingSource, "MapPage", true));
            this.textBoxPage.Location = new System.Drawing.Point(747, 42);
            this.textBoxPage.Name = "textBoxPage";
            this.textBoxPage.Size = new System.Drawing.Size(49, 20);
            this.textBoxPage.TabIndex = 3;
            // 
            // ditchesCulvertsBindingSource1
            // 
            this.ditchesCulvertsBindingSource1.DataMember = "DitchesCulverts";
            this.ditchesCulvertsBindingSource1.DataSource = this.fieldWorkDBDataSet;
            // 
            // viewCulvertsDitchesBindingSource
            // 
            this.viewCulvertsDitchesBindingSource.DataMember = "View_CulvertsDitches";
            this.viewCulvertsDitchesBindingSource.DataSource = this.fieldWorkDBDataSet;
            // 
            // view_CulvertsDitchesTableAdapter
            // 
            this.view_CulvertsDitchesTableAdapter.ClearBeforeFill = true;
            // 
            // ditchesCulvertsTableAdapter
            // 
            this.ditchesCulvertsTableAdapter.ClearBeforeFill = true;
            // 
            // evaluatorPageTableAdapter
            // 
            this.evaluatorPageTableAdapter.ClearBeforeFill = true;
            // 
            // FormStormwaterInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 499);
            this.Controls.Add(this.textBoxPage);
            this.Controls.Add(this.labelPage);
            this.Controls.Add(this.buttonDeleteSelectedNode);
            this.Controls.Add(this.buttonAddANode);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.buttonUpdateDatabase);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.labelDitches);
            this.Controls.Add(this.labelWeather);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelGPS);
            this.Controls.Add(this.textBoxGPS);
            this.Controls.Add(this.labelMapNo);
            this.Controls.Add(this.textBoxMapNo);
            this.Controls.Add(this.labelEvaluator);
            this.Controls.Add(this.textBoxEvaluator);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.labelSubWatershed);
            this.Controls.Add(this.textBoxSubWatershed);
            this.Controls.Add(this.labelWatershed);
            this.Controls.Add(this.textBoxWatershed);
            this.Controls.Add(this.labelCulverts);
            this.Controls.Add(this.labelLocation);
            this.Controls.Add(this.labelCulvertsAndDitchesInformation);
            this.Controls.Add(this.textBoxPageID);
            this.Controls.Add(this.labelPageID);
            this.Name = "FormStormwaterInterface";
            this.Text = "Stormwater Interface";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.evaluatorPageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldWorkDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ditchesCulvertsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ditchesCulvertsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCulvertsDitchesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxWatershed;
        private System.Windows.Forms.Label labelWatershed;
        private System.Windows.Forms.Label labelSubWatershed;
        private System.Windows.Forms.TextBox textBoxSubWatershed;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Label labelEvaluator;
        private System.Windows.Forms.TextBox textBoxEvaluator;
        private System.Windows.Forms.Label labelMapNo;
        private System.Windows.Forms.TextBox textBoxMapNo;
        private System.Windows.Forms.Label labelGPS;
        private System.Windows.Forms.TextBox textBoxGPS;
        private System.Windows.Forms.Label labelWeather;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelCulvertsAndDitchesInformation;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Label labelDitches;
        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private FieldWorkDBDataSet fieldWorkDBDataSet;
        private System.Windows.Forms.BindingSource viewCulvertsDitchesBindingSource;
        private StormwaterInterface.FieldWorkDBDataSetTableAdapters.View_CulvertsDitchesTableAdapter view_CulvertsDitchesTableAdapter;
        private System.Windows.Forms.Label labelCulverts;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.BindingSource ditchesCulvertsBindingSource;
        private StormwaterInterface.FieldWorkDBDataSetTableAdapters.DitchesCulvertsTableAdapter ditchesCulvertsTableAdapter;
        private System.Windows.Forms.Button buttonUpdateDatabase;
        private System.Windows.Forms.BindingSource ditchesCulvertsBindingSource1;
        private System.Windows.Forms.BindingSource evaluatorPageBindingSource;
        private StormwaterInterface.FieldWorkDBDataSetTableAdapters.EvaluatorPageTableAdapter evaluatorPageTableAdapter;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Label labelPageID;
        private System.Windows.Forms.TextBox textBoxPageID;
        private System.Windows.Forms.Button buttonAddANode;
        private System.Windows.Forms.Button buttonDeleteSelectedNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lPODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shapeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pipeDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn culvDepthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn culvMaterialDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn culvPhotoIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ditchDepthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn widthTopDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn widthBottomDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ditchMaterialDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ditchPhotoIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.TextBox textBoxPage;
    }
}

