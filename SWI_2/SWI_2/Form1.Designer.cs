namespace SWI_2
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
            this.buttonAddView = new System.Windows.Forms.Button();
            this.buttonAddSurveyPage = new System.Windows.Forms.Button();
            this.dateTimePickerSurveyDate = new System.Windows.Forms.DateTimePicker();
            this.listBoxWeather = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonUpdateDitch = new System.Windows.Forms.Button();
            this.buttonDitchesViewAddPhotos = new System.Windows.Forms.Button();
            this.comboBoxDitchesFacingDirection = new System.Windows.Forms.ComboBox();
            this.fKDITCHSURVEYPAGEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fKSURVEYPAGEVIEWBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fKVIEWSUBWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fKSUBWATERSHEDWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sANDBOXDataSet = new SWI_2.SANDBOXDataSet();
            this.sWSPFACINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxDitchesMaterial = new System.Windows.Forms.ComboBox();
            this.sWSPMATERIALTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.numericUpDownDitchesTopWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDitchesDepth = new System.Windows.Forms.NumericUpDown();
            this.textBoxDitchesNode = new System.Windows.Forms.TextBox();
            this.labelDitchesNode = new System.Windows.Forms.Label();
            this.labelDitchesFacingDirection = new System.Windows.Forms.Label();
            this.labelDitchesMaterial = new System.Windows.Forms.Label();
            this.labelDitchesBottomWidth = new System.Windows.Forms.Label();
            this.labelDitchesTopWidth = new System.Windows.Forms.Label();
            this.labelDitchesDepth = new System.Windows.Forms.Label();
            this.buttonDitchesAdd = new System.Windows.Forms.Button();
            this.numericUpDownDitchesBottomWidth = new System.Windows.Forms.NumericUpDown();
            this.buttonDitchesDelete = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.nodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.facingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageCulverts = new System.Windows.Forms.TabPage();
            this.buttonUpdateCulvert = new System.Windows.Forms.Button();
            this.buttonCulvertsViewAddPhotos = new System.Windows.Forms.Button();
            this.comboBoxCulvertsShape = new System.Windows.Forms.ComboBox();
            this.sWSPSHAPETYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxCulvertsType = new System.Windows.Forms.ComboBox();
            this.sWSPCULVERTOPENINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxCulvertsMaterial = new System.Windows.Forms.ComboBox();
            this.fKCULVERTSURVEYPAGEBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxCulvertsFacingDirection = new System.Windows.Forms.ComboBox();
            this.textBoxCulvertsNode = new System.Windows.Forms.TextBox();
            this.labelCulvertsNode = new System.Windows.Forms.Label();
            this.labelCulvertsFacingDirection = new System.Windows.Forms.Label();
            this.labelCulvertsMaterial = new System.Windows.Forms.Label();
            this.labelCulvertsUnobstructedHeight = new System.Windows.Forms.Label();
            this.labelCulvertsFullDepth = new System.Windows.Forms.Label();
            this.labelCulvertsShape = new System.Windows.Forms.Label();
            this.labelCulvertsType = new System.Windows.Forms.Label();
            this.buttonCulvertsAdd = new System.Windows.Forms.Button();
            this.numericUpDownCulvertsUnobstructedHeight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCulvertsFullDepth = new System.Windows.Forms.NumericUpDown();
            this.buttonCulvertsDelete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nodeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.facingDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonUpdatePipe = new System.Windows.Forms.Button();
            this.buttonPipesViewAddPhotos = new System.Windows.Forms.Button();
            this.comboBoxPipesShape = new System.Windows.Forms.ComboBox();
            this.fKPIPESURVEYPAGEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxPipesMaterial = new System.Windows.Forms.ComboBox();
            this.labelPipesShape = new System.Windows.Forms.Label();
            this.labelPipesInnerDiameter = new System.Windows.Forms.Label();
            this.numericUpDownPipesInnerDiameter = new System.Windows.Forms.NumericUpDown();
            this.textBoxPipesDSNode = new System.Windows.Forms.TextBox();
            this.textBoxPipesUSNode = new System.Windows.Forms.TextBox();
            this.labelPipesUSNode = new System.Windows.Forms.Label();
            this.labelPipesDSNode = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelPipesDSDepth = new System.Windows.Forms.Label();
            this.labelPipesUSDepth = new System.Windows.Forms.Label();
            this.buttonPipesAdd = new System.Windows.Forms.Button();
            this.numericUpDownPipesDSDepth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPipesUSDepth = new System.Windows.Forms.NumericUpDown();
            this.buttonPipesDelete = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.usnodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dsnodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sWSPPHOTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fKCULVERTFACINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPCULVERTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPPIPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fKCULVERTSURVEYPAGEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelView = new System.Windows.Forms.Label();
            this.labelSurveyPage = new System.Windows.Forms.Label();
            this.labelWatershed = new System.Windows.Forms.Label();
            this.labelSubwatershed = new System.Windows.Forms.Label();
            this.labelSurveyDate = new System.Windows.Forms.Label();
            this.labelWeather = new System.Windows.Forms.Label();
            this.labelEvaluators = new System.Windows.Forms.Label();
            this.labelSearchNode = new System.Windows.Forms.Label();
            this.labelComments = new System.Windows.Forms.Label();
            this.buttonFindNode = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.buttonUpdateDatabase = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sWSPEVALUATORBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxWatershed = new System.Windows.Forms.ComboBox();
            this.comboBoxSubwatershed = new System.Windows.Forms.ComboBox();
            this.sWSP_WATERSHEDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_WATERSHEDTableAdapter();
            this.sWSP_SUBWATERSHEDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SUBWATERSHEDTableAdapter();
            this.sWSPVIEWBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_VIEWTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_VIEWTableAdapter();
            this.comboBoxView = new System.Windows.Forms.ComboBox();
            this.comboBoxSurveyPage = new System.Windows.Forms.ComboBox();
            this.sWSP_SURVEY_PAGETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGETableAdapter();
            this.sWSP_EVALUATORTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_EVALUATORTableAdapter();
            this.sWSPEVALUATORBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPSURVEYPAGEEVALUATORBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPEVALUATORBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGE_EVALUATORTableAdapter();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.sWSP_DITCHTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_DITCHTableAdapter();
            this.sWSPDITCHBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_FACING_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_FACING_TYPETableAdapter();
            this.sWSP_MATERIAL_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_MATERIAL_TYPETableAdapter();
            this.sWSP_PHOTOTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_PHOTOTableAdapter();
            this.sWSP_CULVERTTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_CULVERTTableAdapter();
            this.sWSP_CULVERT_OPENING_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_CULVERT_OPENING_TYPETableAdapter();
            this.sWSP_SHAPE_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SHAPE_TYPETableAdapter();
            this.sWSP_PIPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_PIPETableAdapter();
            this.sWSPCULVERTBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_GLOBAL_IDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_GLOBAL_IDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_GLOBAL_IDTableAdapter();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKDITCHSURVEYPAGEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPMATERIALTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDitchesTopWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDitchesDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDitchesBottomWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPageCulverts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSHAPETYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTOPENINGTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTSURVEYPAGEBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCulvertsUnobstructedHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCulvertsFullDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPESURVEYPAGEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipesInnerDiameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipesDSDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipesUSDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPPHOTOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTFACINGTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPPIPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTSURVEYPAGEBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPEVALUATORBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPVIEWBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPEVALUATORBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSURVEYPAGEEVALUATORBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPEVALUATORBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPDITCHBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSP_GLOBAL_IDBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAddView
            // 
            this.buttonAddView.Location = new System.Drawing.Point(141, 44);
            this.buttonAddView.Name = "buttonAddView";
            this.buttonAddView.Size = new System.Drawing.Size(95, 22);
            this.buttonAddView.TabIndex = 2;
            this.buttonAddView.Text = "Add view...";
            this.buttonAddView.UseVisualStyleBackColor = true;
            this.buttonAddView.Click += new System.EventHandler(this.buttonAddView_Click);
            // 
            // buttonAddSurveyPage
            // 
            this.buttonAddSurveyPage.Location = new System.Drawing.Point(242, 44);
            this.buttonAddSurveyPage.Name = "buttonAddSurveyPage";
            this.buttonAddSurveyPage.Size = new System.Drawing.Size(119, 22);
            this.buttonAddSurveyPage.TabIndex = 3;
            this.buttonAddSurveyPage.Text = "Add survey page...";
            this.buttonAddSurveyPage.UseVisualStyleBackColor = true;
            this.buttonAddSurveyPage.Click += new System.EventHandler(this.buttonAddSurveyPage_Click);
            // 
            // dateTimePickerSurveyDate
            // 
            this.dateTimePickerSurveyDate.Location = new System.Drawing.Point(12, 97);
            this.dateTimePickerSurveyDate.Name = "dateTimePickerSurveyDate";
            this.dateTimePickerSurveyDate.Size = new System.Drawing.Size(141, 20);
            this.dateTimePickerSurveyDate.TabIndex = 6;
            // 
            // listBoxWeather
            // 
            this.listBoxWeather.FormattingEnabled = true;
            this.listBoxWeather.Location = new System.Drawing.Point(171, 100);
            this.listBoxWeather.Name = "listBoxWeather";
            this.listBoxWeather.Size = new System.Drawing.Size(66, 17);
            this.listBoxWeather.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageCulverts);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(8, 132);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(804, 241);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonUpdateDitch);
            this.tabPage1.Controls.Add(this.buttonDitchesViewAddPhotos);
            this.tabPage1.Controls.Add(this.comboBoxDitchesFacingDirection);
            this.tabPage1.Controls.Add(this.comboBoxDitchesMaterial);
            this.tabPage1.Controls.Add(this.numericUpDownDitchesTopWidth);
            this.tabPage1.Controls.Add(this.numericUpDownDitchesDepth);
            this.tabPage1.Controls.Add(this.textBoxDitchesNode);
            this.tabPage1.Controls.Add(this.labelDitchesNode);
            this.tabPage1.Controls.Add(this.labelDitchesFacingDirection);
            this.tabPage1.Controls.Add(this.labelDitchesMaterial);
            this.tabPage1.Controls.Add(this.labelDitchesBottomWidth);
            this.tabPage1.Controls.Add(this.labelDitchesTopWidth);
            this.tabPage1.Controls.Add(this.labelDitchesDepth);
            this.tabPage1.Controls.Add(this.buttonDitchesAdd);
            this.tabPage1.Controls.Add(this.numericUpDownDitchesBottomWidth);
            this.tabPage1.Controls.Add(this.buttonDitchesDelete);
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(796, 215);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ditches";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Entered);
            // 
            // buttonUpdateDitch
            // 
            this.buttonUpdateDitch.Location = new System.Drawing.Point(690, 177);
            this.buttonUpdateDitch.Name = "buttonUpdateDitch";
            this.buttonUpdateDitch.Size = new System.Drawing.Size(97, 31);
            this.buttonUpdateDitch.TabIndex = 65;
            this.buttonUpdateDitch.Text = "Update Ditch";
            this.buttonUpdateDitch.UseVisualStyleBackColor = true;
            this.buttonUpdateDitch.Click += new System.EventHandler(this.buttonUpdateDitch_Click);
            // 
            // buttonDitchesViewAddPhotos
            // 
            this.buttonDitchesViewAddPhotos.Location = new System.Drawing.Point(351, 177);
            this.buttonDitchesViewAddPhotos.Name = "buttonDitchesViewAddPhotos";
            this.buttonDitchesViewAddPhotos.Size = new System.Drawing.Size(220, 32);
            this.buttonDitchesViewAddPhotos.TabIndex = 64;
            this.buttonDitchesViewAddPhotos.Text = "View/Add Photos";
            this.buttonDitchesViewAddPhotos.UseVisualStyleBackColor = true;
            this.buttonDitchesViewAddPhotos.Click += new System.EventHandler(this.buttonDitchesViewAddPhotos_Click);
            // 
            // comboBoxDitchesFacingDirection
            // 
            this.comboBoxDitchesFacingDirection.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.fKDITCHSURVEYPAGEBindingSource, "facing", true));
            this.comboBoxDitchesFacingDirection.DataSource = this.sWSPFACINGTYPEBindingSource;
            this.comboBoxDitchesFacingDirection.DisplayMember = "facing";
            this.comboBoxDitchesFacingDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDitchesFacingDirection.FormattingEnabled = true;
            this.comboBoxDitchesFacingDirection.Location = new System.Drawing.Point(418, 30);
            this.comboBoxDitchesFacingDirection.Name = "comboBoxDitchesFacingDirection";
            this.comboBoxDitchesFacingDirection.Size = new System.Drawing.Size(79, 21);
            this.comboBoxDitchesFacingDirection.TabIndex = 62;
            this.comboBoxDitchesFacingDirection.ValueMember = "facing_type_id";
            // 
            // fKDITCHSURVEYPAGEBindingSource
            // 
            this.fKDITCHSURVEYPAGEBindingSource.DataMember = "FK_DITCH_SURVEY_PAGE";
            this.fKDITCHSURVEYPAGEBindingSource.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            // 
            // fKSURVEYPAGEVIEWBindingSource
            // 
            this.fKSURVEYPAGEVIEWBindingSource.DataMember = "FK_SURVEY_PAGE_VIEW";
            this.fKSURVEYPAGEVIEWBindingSource.DataSource = this.fKVIEWSUBWATERSHEDBindingSource;
            // 
            // fKVIEWSUBWATERSHEDBindingSource
            // 
            this.fKVIEWSUBWATERSHEDBindingSource.DataMember = "FK_VIEW_SUBWATERSHED";
            this.fKVIEWSUBWATERSHEDBindingSource.DataSource = this.fKSUBWATERSHEDWATERSHEDBindingSource;
            // 
            // fKSUBWATERSHEDWATERSHEDBindingSource
            // 
            this.fKSUBWATERSHEDWATERSHEDBindingSource.DataMember = "FK_SUBWATERSHED_WATERSHED";
            this.fKSUBWATERSHEDWATERSHEDBindingSource.DataSource = this.sWSPWATERSHEDBindingSource;
            // 
            // sWSPWATERSHEDBindingSource
            // 
            this.sWSPWATERSHEDBindingSource.DataMember = "SWSP_WATERSHED";
            this.sWSPWATERSHEDBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // sANDBOXDataSet
            // 
            this.sANDBOXDataSet.DataSetName = "SANDBOXDataSet";
            this.sANDBOXDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sWSPFACINGTYPEBindingSource
            // 
            this.sWSPFACINGTYPEBindingSource.DataMember = "SWSP_FACING_TYPE";
            this.sWSPFACINGTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // comboBoxDitchesMaterial
            // 
            this.comboBoxDitchesMaterial.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.fKDITCHSURVEYPAGEBindingSource, "material", true));
            this.comboBoxDitchesMaterial.DataSource = this.sWSPMATERIALTYPEBindingSource;
            this.comboBoxDitchesMaterial.DisplayMember = "material";
            this.comboBoxDitchesMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDitchesMaterial.FormattingEnabled = true;
            this.comboBoxDitchesMaterial.Location = new System.Drawing.Point(698, 93);
            this.comboBoxDitchesMaterial.Name = "comboBoxDitchesMaterial";
            this.comboBoxDitchesMaterial.Size = new System.Drawing.Size(90, 21);
            this.comboBoxDitchesMaterial.TabIndex = 61;
            this.comboBoxDitchesMaterial.ValueMember = "material_type_id";
            // 
            // sWSPMATERIALTYPEBindingSource
            // 
            this.sWSPMATERIALTYPEBindingSource.DataMember = "SWSP_MATERIAL_TYPE";
            this.sWSPMATERIALTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // numericUpDownDitchesTopWidth
            // 
            this.numericUpDownDitchesTopWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "top_width_in", true));
            this.numericUpDownDitchesTopWidth.Location = new System.Drawing.Point(528, 93);
            this.numericUpDownDitchesTopWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownDitchesTopWidth.Name = "numericUpDownDitchesTopWidth";
            this.numericUpDownDitchesTopWidth.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownDitchesTopWidth.TabIndex = 60;
            // 
            // numericUpDownDitchesDepth
            // 
            this.numericUpDownDitchesDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "depth_in", true));
            this.numericUpDownDitchesDepth.Location = new System.Drawing.Point(458, 92);
            this.numericUpDownDitchesDepth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownDitchesDepth.Name = "numericUpDownDitchesDepth";
            this.numericUpDownDitchesDepth.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownDitchesDepth.TabIndex = 59;
            // 
            // textBoxDitchesNode
            // 
            this.textBoxDitchesNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKDITCHSURVEYPAGEBindingSource, "node", true));
            this.textBoxDitchesNode.Location = new System.Drawing.Point(351, 29);
            this.textBoxDitchesNode.Name = "textBoxDitchesNode";
            this.textBoxDitchesNode.Size = new System.Drawing.Size(56, 20);
            this.textBoxDitchesNode.TabIndex = 58;
            // 
            // labelDitchesNode
            // 
            this.labelDitchesNode.AutoSize = true;
            this.labelDitchesNode.Location = new System.Drawing.Point(348, 10);
            this.labelDitchesNode.Name = "labelDitchesNode";
            this.labelDitchesNode.Size = new System.Drawing.Size(33, 13);
            this.labelDitchesNode.TabIndex = 56;
            this.labelDitchesNode.Text = "Node";
            // 
            // labelDitchesFacingDirection
            // 
            this.labelDitchesFacingDirection.AutoSize = true;
            this.labelDitchesFacingDirection.Location = new System.Drawing.Point(415, 10);
            this.labelDitchesFacingDirection.Name = "labelDitchesFacingDirection";
            this.labelDitchesFacingDirection.Size = new System.Drawing.Size(84, 13);
            this.labelDitchesFacingDirection.TabIndex = 55;
            this.labelDitchesFacingDirection.Text = "Facing Direction";
            // 
            // labelDitchesMaterial
            // 
            this.labelDitchesMaterial.AutoSize = true;
            this.labelDitchesMaterial.Location = new System.Drawing.Point(692, 73);
            this.labelDitchesMaterial.Name = "labelDitchesMaterial";
            this.labelDitchesMaterial.Size = new System.Drawing.Size(44, 13);
            this.labelDitchesMaterial.TabIndex = 53;
            this.labelDitchesMaterial.Text = "Material";
            // 
            // labelDitchesBottomWidth
            // 
            this.labelDitchesBottomWidth.AutoSize = true;
            this.labelDitchesBottomWidth.Location = new System.Drawing.Point(601, 73);
            this.labelDitchesBottomWidth.Name = "labelDitchesBottomWidth";
            this.labelDitchesBottomWidth.Size = new System.Drawing.Size(85, 13);
            this.labelDitchesBottomWidth.TabIndex = 51;
            this.labelDitchesBottomWidth.Text = "Bottom width (in)";
            // 
            // labelDitchesTopWidth
            // 
            this.labelDitchesTopWidth.AutoSize = true;
            this.labelDitchesTopWidth.Location = new System.Drawing.Point(525, 73);
            this.labelDitchesTopWidth.Name = "labelDitchesTopWidth";
            this.labelDitchesTopWidth.Size = new System.Drawing.Size(71, 13);
            this.labelDitchesTopWidth.TabIndex = 50;
            this.labelDitchesTopWidth.Text = "Top width (in)";
            // 
            // labelDitchesDepth
            // 
            this.labelDitchesDepth.AutoSize = true;
            this.labelDitchesDepth.Location = new System.Drawing.Point(455, 73);
            this.labelDitchesDepth.Name = "labelDitchesDepth";
            this.labelDitchesDepth.Size = new System.Drawing.Size(53, 13);
            this.labelDitchesDepth.TabIndex = 49;
            this.labelDitchesDepth.Text = "Depth (in)";
            // 
            // buttonDitchesAdd
            // 
            this.buttonDitchesAdd.Location = new System.Drawing.Point(257, 184);
            this.buttonDitchesAdd.Name = "buttonDitchesAdd";
            this.buttonDitchesAdd.Size = new System.Drawing.Size(65, 28);
            this.buttonDitchesAdd.TabIndex = 48;
            this.buttonDitchesAdd.Text = "Add";
            this.buttonDitchesAdd.UseVisualStyleBackColor = true;
            this.buttonDitchesAdd.Click += new System.EventHandler(this.buttonDitchesAdd_Click);
            // 
            // numericUpDownDitchesBottomWidth
            // 
            this.numericUpDownDitchesBottomWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "bottom_width_in", true));
            this.numericUpDownDitchesBottomWidth.Location = new System.Drawing.Point(604, 93);
            this.numericUpDownDitchesBottomWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownDitchesBottomWidth.Name = "numericUpDownDitchesBottomWidth";
            this.numericUpDownDitchesBottomWidth.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownDitchesBottomWidth.TabIndex = 44;
            // 
            // buttonDitchesDelete
            // 
            this.buttonDitchesDelete.Location = new System.Drawing.Point(257, 8);
            this.buttonDitchesDelete.Name = "buttonDitchesDelete";
            this.buttonDitchesDelete.Size = new System.Drawing.Size(65, 28);
            this.buttonDitchesDelete.TabIndex = 41;
            this.buttonDitchesDelete.Text = "Delete";
            this.buttonDitchesDelete.UseVisualStyleBackColor = true;
            this.buttonDitchesDelete.Click += new System.EventHandler(this.buttonDitchesDelete_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nodeDataGridViewTextBoxColumn,
            this.facingDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.fKDITCHSURVEYPAGEBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(3, 8);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(248, 204);
            this.dataGridView2.TabIndex = 40;
            // 
            // nodeDataGridViewTextBoxColumn
            // 
            this.nodeDataGridViewTextBoxColumn.DataPropertyName = "node";
            this.nodeDataGridViewTextBoxColumn.HeaderText = "Node";
            this.nodeDataGridViewTextBoxColumn.Name = "nodeDataGridViewTextBoxColumn";
            this.nodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.nodeDataGridViewTextBoxColumn.Width = 80;
            // 
            // facingDataGridViewTextBoxColumn
            // 
            this.facingDataGridViewTextBoxColumn.DataPropertyName = "facing";
            this.facingDataGridViewTextBoxColumn.HeaderText = "Facing Dir";
            this.facingDataGridViewTextBoxColumn.Name = "facingDataGridViewTextBoxColumn";
            this.facingDataGridViewTextBoxColumn.ReadOnly = true;
            this.facingDataGridViewTextBoxColumn.Width = 80;
            // 
            // tabPageCulverts
            // 
            this.tabPageCulverts.Controls.Add(this.buttonUpdateCulvert);
            this.tabPageCulverts.Controls.Add(this.buttonCulvertsViewAddPhotos);
            this.tabPageCulverts.Controls.Add(this.comboBoxCulvertsShape);
            this.tabPageCulverts.Controls.Add(this.comboBoxCulvertsType);
            this.tabPageCulverts.Controls.Add(this.comboBoxCulvertsMaterial);
            this.tabPageCulverts.Controls.Add(this.comboBoxCulvertsFacingDirection);
            this.tabPageCulverts.Controls.Add(this.textBoxCulvertsNode);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsNode);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsFacingDirection);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsMaterial);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsUnobstructedHeight);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsFullDepth);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsShape);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsType);
            this.tabPageCulverts.Controls.Add(this.buttonCulvertsAdd);
            this.tabPageCulverts.Controls.Add(this.numericUpDownCulvertsUnobstructedHeight);
            this.tabPageCulverts.Controls.Add(this.numericUpDownCulvertsFullDepth);
            this.tabPageCulverts.Controls.Add(this.buttonCulvertsDelete);
            this.tabPageCulverts.Controls.Add(this.dataGridView1);
            this.tabPageCulverts.Location = new System.Drawing.Point(4, 22);
            this.tabPageCulverts.Name = "tabPageCulverts";
            this.tabPageCulverts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCulverts.Size = new System.Drawing.Size(796, 215);
            this.tabPageCulverts.TabIndex = 1;
            this.tabPageCulverts.Text = "Culverts";
            this.tabPageCulverts.UseVisualStyleBackColor = true;
            this.tabPageCulverts.Enter += new System.EventHandler(this.tabPageCulverts_Enter);
            // 
            // buttonUpdateCulvert
            // 
            this.buttonUpdateCulvert.Location = new System.Drawing.Point(693, 177);
            this.buttonUpdateCulvert.Name = "buttonUpdateCulvert";
            this.buttonUpdateCulvert.Size = new System.Drawing.Size(97, 31);
            this.buttonUpdateCulvert.TabIndex = 66;
            this.buttonUpdateCulvert.Text = "Update Culvert";
            this.buttonUpdateCulvert.UseVisualStyleBackColor = true;
            this.buttonUpdateCulvert.Click += new System.EventHandler(this.buttonUpdateCulvert_Click);
            // 
            // buttonCulvertsViewAddPhotos
            // 
            this.buttonCulvertsViewAddPhotos.Location = new System.Drawing.Point(333, 177);
            this.buttonCulvertsViewAddPhotos.Name = "buttonCulvertsViewAddPhotos";
            this.buttonCulvertsViewAddPhotos.Size = new System.Drawing.Size(220, 32);
            this.buttonCulvertsViewAddPhotos.TabIndex = 65;
            this.buttonCulvertsViewAddPhotos.Text = "View/Add Photos";
            this.buttonCulvertsViewAddPhotos.UseVisualStyleBackColor = true;
            this.buttonCulvertsViewAddPhotos.Click += new System.EventHandler(this.buttonCulvertsViewAddPhotos_Click);
            // 
            // comboBoxCulvertsShape
            // 
            this.comboBoxCulvertsShape.DataSource = this.sWSPSHAPETYPEBindingSource;
            this.comboBoxCulvertsShape.DisplayMember = "shape";
            this.comboBoxCulvertsShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCulvertsShape.FormattingEnabled = true;
            this.comboBoxCulvertsShape.Location = new System.Drawing.Point(553, 26);
            this.comboBoxCulvertsShape.Name = "comboBoxCulvertsShape";
            this.comboBoxCulvertsShape.Size = new System.Drawing.Size(44, 21);
            this.comboBoxCulvertsShape.TabIndex = 48;
            // 
            // sWSPSHAPETYPEBindingSource
            // 
            this.sWSPSHAPETYPEBindingSource.DataMember = "SWSP_SHAPE_TYPE";
            this.sWSPSHAPETYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // comboBoxCulvertsType
            // 
            this.comboBoxCulvertsType.DataSource = this.sWSPCULVERTOPENINGTYPEBindingSource;
            this.comboBoxCulvertsType.DisplayMember = "culvert_opening";
            this.comboBoxCulvertsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCulvertsType.FormattingEnabled = true;
            this.comboBoxCulvertsType.Location = new System.Drawing.Point(482, 24);
            this.comboBoxCulvertsType.Name = "comboBoxCulvertsType";
            this.comboBoxCulvertsType.Size = new System.Drawing.Size(52, 21);
            this.comboBoxCulvertsType.TabIndex = 47;
            // 
            // sWSPCULVERTOPENINGTYPEBindingSource
            // 
            this.sWSPCULVERTOPENINGTYPEBindingSource.DataMember = "SWSP_CULVERT_OPENING_TYPE";
            this.sWSPCULVERTOPENINGTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // comboBoxCulvertsMaterial
            // 
            this.comboBoxCulvertsMaterial.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.fKCULVERTSURVEYPAGEBindingSource1, "material", true));
            this.comboBoxCulvertsMaterial.DataSource = this.sWSPMATERIALTYPEBindingSource;
            this.comboBoxCulvertsMaterial.DisplayMember = "material";
            this.comboBoxCulvertsMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCulvertsMaterial.FormattingEnabled = true;
            this.comboBoxCulvertsMaterial.Location = new System.Drawing.Point(701, 80);
            this.comboBoxCulvertsMaterial.Name = "comboBoxCulvertsMaterial";
            this.comboBoxCulvertsMaterial.Size = new System.Drawing.Size(89, 21);
            this.comboBoxCulvertsMaterial.TabIndex = 45;
            this.comboBoxCulvertsMaterial.ValueMember = "material_type_id";
            // 
            // fKCULVERTSURVEYPAGEBindingSource1
            // 
            this.fKCULVERTSURVEYPAGEBindingSource1.DataMember = "FK_CULVERT_SURVEY_PAGE";
            this.fKCULVERTSURVEYPAGEBindingSource1.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            // 
            // comboBoxCulvertsFacingDirection
            // 
            this.comboBoxCulvertsFacingDirection.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.fKCULVERTSURVEYPAGEBindingSource1, "facing", true));
            this.comboBoxCulvertsFacingDirection.DataSource = this.sWSPFACINGTYPEBindingSource;
            this.comboBoxCulvertsFacingDirection.DisplayMember = "facing";
            this.comboBoxCulvertsFacingDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCulvertsFacingDirection.FormattingEnabled = true;
            this.comboBoxCulvertsFacingDirection.Location = new System.Drawing.Point(397, 24);
            this.comboBoxCulvertsFacingDirection.Name = "comboBoxCulvertsFacingDirection";
            this.comboBoxCulvertsFacingDirection.Size = new System.Drawing.Size(72, 21);
            this.comboBoxCulvertsFacingDirection.TabIndex = 44;
            this.comboBoxCulvertsFacingDirection.ValueMember = "facing_type_id";
            // 
            // textBoxCulvertsNode
            // 
            this.textBoxCulvertsNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKCULVERTSURVEYPAGEBindingSource1, "node", true));
            this.textBoxCulvertsNode.Location = new System.Drawing.Point(333, 25);
            this.textBoxCulvertsNode.Name = "textBoxCulvertsNode";
            this.textBoxCulvertsNode.Size = new System.Drawing.Size(56, 20);
            this.textBoxCulvertsNode.TabIndex = 43;
            // 
            // labelCulvertsNode
            // 
            this.labelCulvertsNode.AutoSize = true;
            this.labelCulvertsNode.Location = new System.Drawing.Point(330, 6);
            this.labelCulvertsNode.Name = "labelCulvertsNode";
            this.labelCulvertsNode.Size = new System.Drawing.Size(33, 13);
            this.labelCulvertsNode.TabIndex = 41;
            this.labelCulvertsNode.Text = "Node";
            // 
            // labelCulvertsFacingDirection
            // 
            this.labelCulvertsFacingDirection.AutoSize = true;
            this.labelCulvertsFacingDirection.Location = new System.Drawing.Point(386, 7);
            this.labelCulvertsFacingDirection.Name = "labelCulvertsFacingDirection";
            this.labelCulvertsFacingDirection.Size = new System.Drawing.Size(84, 13);
            this.labelCulvertsFacingDirection.TabIndex = 40;
            this.labelCulvertsFacingDirection.Text = "Facing Direction";
            // 
            // labelCulvertsMaterial
            // 
            this.labelCulvertsMaterial.AutoSize = true;
            this.labelCulvertsMaterial.Location = new System.Drawing.Point(746, 60);
            this.labelCulvertsMaterial.Name = "labelCulvertsMaterial";
            this.labelCulvertsMaterial.Size = new System.Drawing.Size(44, 13);
            this.labelCulvertsMaterial.TabIndex = 38;
            this.labelCulvertsMaterial.Text = "Material";
            // 
            // labelCulvertsUnobstructedHeight
            // 
            this.labelCulvertsUnobstructedHeight.AutoSize = true;
            this.labelCulvertsUnobstructedHeight.Location = new System.Drawing.Point(575, 60);
            this.labelCulvertsUnobstructedHeight.Name = "labelCulvertsUnobstructedHeight";
            this.labelCulvertsUnobstructedHeight.Size = new System.Drawing.Size(120, 13);
            this.labelCulvertsUnobstructedHeight.TabIndex = 37;
            this.labelCulvertsUnobstructedHeight.Text = "Unobstructed height (in)";
            // 
            // labelCulvertsFullDepth
            // 
            this.labelCulvertsFullDepth.AutoSize = true;
            this.labelCulvertsFullDepth.Location = new System.Drawing.Point(503, 60);
            this.labelCulvertsFullDepth.Name = "labelCulvertsFullDepth";
            this.labelCulvertsFullDepth.Size = new System.Drawing.Size(70, 13);
            this.labelCulvertsFullDepth.TabIndex = 36;
            this.labelCulvertsFullDepth.Text = "Full depth (in)";
            // 
            // labelCulvertsShape
            // 
            this.labelCulvertsShape.AutoSize = true;
            this.labelCulvertsShape.Location = new System.Drawing.Point(560, 6);
            this.labelCulvertsShape.Name = "labelCulvertsShape";
            this.labelCulvertsShape.Size = new System.Drawing.Size(38, 13);
            this.labelCulvertsShape.TabIndex = 35;
            this.labelCulvertsShape.Text = "Shape";
            // 
            // labelCulvertsType
            // 
            this.labelCulvertsType.AutoSize = true;
            this.labelCulvertsType.Location = new System.Drawing.Point(503, 7);
            this.labelCulvertsType.Name = "labelCulvertsType";
            this.labelCulvertsType.Size = new System.Drawing.Size(31, 13);
            this.labelCulvertsType.TabIndex = 34;
            this.labelCulvertsType.Text = "Type";
            // 
            // buttonCulvertsAdd
            // 
            this.buttonCulvertsAdd.Location = new System.Drawing.Point(257, 184);
            this.buttonCulvertsAdd.Name = "buttonCulvertsAdd";
            this.buttonCulvertsAdd.Size = new System.Drawing.Size(65, 28);
            this.buttonCulvertsAdd.TabIndex = 30;
            this.buttonCulvertsAdd.Text = "Add";
            this.buttonCulvertsAdd.UseVisualStyleBackColor = true;
            this.buttonCulvertsAdd.Click += new System.EventHandler(this.buttonCulvertsAdd_Click);
            // 
            // numericUpDownCulvertsUnobstructedHeight
            // 
            this.numericUpDownCulvertsUnobstructedHeight.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "unobstructed_height_in", true));
            this.numericUpDownCulvertsUnobstructedHeight.Location = new System.Drawing.Point(648, 80);
            this.numericUpDownCulvertsUnobstructedHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownCulvertsUnobstructedHeight.Name = "numericUpDownCulvertsUnobstructedHeight";
            this.numericUpDownCulvertsUnobstructedHeight.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownCulvertsUnobstructedHeight.TabIndex = 26;
            // 
            // numericUpDownCulvertsFullDepth
            // 
            this.numericUpDownCulvertsFullDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "full_diam_in", true));
            this.numericUpDownCulvertsFullDepth.Location = new System.Drawing.Point(528, 80);
            this.numericUpDownCulvertsFullDepth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownCulvertsFullDepth.Name = "numericUpDownCulvertsFullDepth";
            this.numericUpDownCulvertsFullDepth.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownCulvertsFullDepth.TabIndex = 25;
            // 
            // buttonCulvertsDelete
            // 
            this.buttonCulvertsDelete.Location = new System.Drawing.Point(257, 8);
            this.buttonCulvertsDelete.Name = "buttonCulvertsDelete";
            this.buttonCulvertsDelete.Size = new System.Drawing.Size(65, 28);
            this.buttonCulvertsDelete.TabIndex = 22;
            this.buttonCulvertsDelete.Text = "Delete";
            this.buttonCulvertsDelete.UseVisualStyleBackColor = true;
            this.buttonCulvertsDelete.Click += new System.EventHandler(this.buttonCulvertsDelete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nodeDataGridViewTextBoxColumn1,
            this.facingDataGridViewTextBoxColumn1});
            this.dataGridView1.DataSource = this.fKCULVERTSURVEYPAGEBindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(3, 8);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(248, 204);
            this.dataGridView1.TabIndex = 21;
            // 
            // nodeDataGridViewTextBoxColumn1
            // 
            this.nodeDataGridViewTextBoxColumn1.DataPropertyName = "node";
            this.nodeDataGridViewTextBoxColumn1.HeaderText = "Node";
            this.nodeDataGridViewTextBoxColumn1.Name = "nodeDataGridViewTextBoxColumn1";
            this.nodeDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // facingDataGridViewTextBoxColumn1
            // 
            this.facingDataGridViewTextBoxColumn1.DataPropertyName = "facing";
            this.facingDataGridViewTextBoxColumn1.HeaderText = "Facing Dir";
            this.facingDataGridViewTextBoxColumn1.Name = "facingDataGridViewTextBoxColumn1";
            this.facingDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonUpdatePipe);
            this.tabPage3.Controls.Add(this.buttonPipesViewAddPhotos);
            this.tabPage3.Controls.Add(this.comboBoxPipesShape);
            this.tabPage3.Controls.Add(this.comboBoxPipesMaterial);
            this.tabPage3.Controls.Add(this.labelPipesShape);
            this.tabPage3.Controls.Add(this.labelPipesInnerDiameter);
            this.tabPage3.Controls.Add(this.numericUpDownPipesInnerDiameter);
            this.tabPage3.Controls.Add(this.textBoxPipesDSNode);
            this.tabPage3.Controls.Add(this.textBoxPipesUSNode);
            this.tabPage3.Controls.Add(this.labelPipesUSNode);
            this.tabPage3.Controls.Add(this.labelPipesDSNode);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.labelPipesDSDepth);
            this.tabPage3.Controls.Add(this.labelPipesUSDepth);
            this.tabPage3.Controls.Add(this.buttonPipesAdd);
            this.tabPage3.Controls.Add(this.numericUpDownPipesDSDepth);
            this.tabPage3.Controls.Add(this.numericUpDownPipesUSDepth);
            this.tabPage3.Controls.Add(this.buttonPipesDelete);
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(796, 215);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Pipes";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Enter += new System.EventHandler(this.tabPage3_Enter);
            // 
            // buttonUpdatePipe
            // 
            this.buttonUpdatePipe.Location = new System.Drawing.Point(696, 180);
            this.buttonUpdatePipe.Name = "buttonUpdatePipe";
            this.buttonUpdatePipe.Size = new System.Drawing.Size(97, 31);
            this.buttonUpdatePipe.TabIndex = 72;
            this.buttonUpdatePipe.Text = "Update Pipe";
            this.buttonUpdatePipe.UseVisualStyleBackColor = true;
            this.buttonUpdatePipe.Click += new System.EventHandler(this.buttonUpdatePipe_Click);
            // 
            // buttonPipesViewAddPhotos
            // 
            this.buttonPipesViewAddPhotos.Location = new System.Drawing.Point(334, 180);
            this.buttonPipesViewAddPhotos.Name = "buttonPipesViewAddPhotos";
            this.buttonPipesViewAddPhotos.Size = new System.Drawing.Size(220, 32);
            this.buttonPipesViewAddPhotos.TabIndex = 71;
            this.buttonPipesViewAddPhotos.Text = "View/Add Photos";
            this.buttonPipesViewAddPhotos.UseVisualStyleBackColor = true;
            this.buttonPipesViewAddPhotos.Click += new System.EventHandler(this.buttonPipesViewAddPhotos_Click);
            // 
            // comboBoxPipesShape
            // 
            this.comboBoxPipesShape.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.fKPIPESURVEYPAGEBindingSource, "shape", true));
            this.comboBoxPipesShape.DataSource = this.sWSPSHAPETYPEBindingSource;
            this.comboBoxPipesShape.DisplayMember = "shape";
            this.comboBoxPipesShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPipesShape.FormattingEnabled = true;
            this.comboBoxPipesShape.Location = new System.Drawing.Point(668, 78);
            this.comboBoxPipesShape.Name = "comboBoxPipesShape";
            this.comboBoxPipesShape.Size = new System.Drawing.Size(80, 21);
            this.comboBoxPipesShape.TabIndex = 70;
            this.comboBoxPipesShape.ValueMember = "shape_type_id";
            // 
            // fKPIPESURVEYPAGEBindingSource
            // 
            this.fKPIPESURVEYPAGEBindingSource.DataMember = "FK_PIPE_SURVEY_PAGE";
            this.fKPIPESURVEYPAGEBindingSource.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            // 
            // comboBoxPipesMaterial
            // 
            this.comboBoxPipesMaterial.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.fKPIPESURVEYPAGEBindingSource, "material", true));
            this.comboBoxPipesMaterial.DataSource = this.sWSPMATERIALTYPEBindingSource;
            this.comboBoxPipesMaterial.DisplayMember = "material";
            this.comboBoxPipesMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPipesMaterial.FormattingEnabled = true;
            this.comboBoxPipesMaterial.Location = new System.Drawing.Point(582, 78);
            this.comboBoxPipesMaterial.Name = "comboBoxPipesMaterial";
            this.comboBoxPipesMaterial.Size = new System.Drawing.Size(74, 21);
            this.comboBoxPipesMaterial.TabIndex = 69;
            this.comboBoxPipesMaterial.ValueMember = "material_type_id";
            // 
            // labelPipesShape
            // 
            this.labelPipesShape.AutoSize = true;
            this.labelPipesShape.Location = new System.Drawing.Point(669, 57);
            this.labelPipesShape.Name = "labelPipesShape";
            this.labelPipesShape.Size = new System.Drawing.Size(38, 13);
            this.labelPipesShape.TabIndex = 66;
            this.labelPipesShape.Text = "Shape";
            // 
            // labelPipesInnerDiameter
            // 
            this.labelPipesInnerDiameter.AutoSize = true;
            this.labelPipesInnerDiameter.Location = new System.Drawing.Point(482, 57);
            this.labelPipesInnerDiameter.Name = "labelPipesInnerDiameter";
            this.labelPipesInnerDiameter.Size = new System.Drawing.Size(93, 13);
            this.labelPipesInnerDiameter.TabIndex = 65;
            this.labelPipesInnerDiameter.Text = "Inner Diameter (in)";
            // 
            // numericUpDownPipesInnerDiameter
            // 
            this.numericUpDownPipesInnerDiameter.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "inside_diam_in", true));
            this.numericUpDownPipesInnerDiameter.Location = new System.Drawing.Point(485, 76);
            this.numericUpDownPipesInnerDiameter.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownPipesInnerDiameter.Name = "numericUpDownPipesInnerDiameter";
            this.numericUpDownPipesInnerDiameter.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownPipesInnerDiameter.TabIndex = 64;
            // 
            // textBoxPipesDSNode
            // 
            this.textBoxPipesDSNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKPIPESURVEYPAGEBindingSource, "ds_node", true));
            this.textBoxPipesDSNode.Location = new System.Drawing.Point(401, 28);
            this.textBoxPipesDSNode.Name = "textBoxPipesDSNode";
            this.textBoxPipesDSNode.Size = new System.Drawing.Size(64, 20);
            this.textBoxPipesDSNode.TabIndex = 63;
            // 
            // textBoxPipesUSNode
            // 
            this.textBoxPipesUSNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKPIPESURVEYPAGEBindingSource, "us_node", true));
            this.textBoxPipesUSNode.Location = new System.Drawing.Point(333, 28);
            this.textBoxPipesUSNode.Name = "textBoxPipesUSNode";
            this.textBoxPipesUSNode.Size = new System.Drawing.Size(56, 20);
            this.textBoxPipesUSNode.TabIndex = 62;
            // 
            // labelPipesUSNode
            // 
            this.labelPipesUSNode.AutoSize = true;
            this.labelPipesUSNode.Location = new System.Drawing.Point(330, 12);
            this.labelPipesUSNode.Name = "labelPipesUSNode";
            this.labelPipesUSNode.Size = new System.Drawing.Size(51, 13);
            this.labelPipesUSNode.TabIndex = 60;
            this.labelPipesUSNode.Text = "US Node";
            // 
            // labelPipesDSNode
            // 
            this.labelPipesDSNode.AutoSize = true;
            this.labelPipesDSNode.Location = new System.Drawing.Point(398, 12);
            this.labelPipesDSNode.Name = "labelPipesDSNode";
            this.labelPipesDSNode.Size = new System.Drawing.Size(51, 13);
            this.labelPipesDSNode.TabIndex = 59;
            this.labelPipesDSNode.Text = "DS Node";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(581, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Material";
            // 
            // labelPipesDSDepth
            // 
            this.labelPipesDSDepth.AutoSize = true;
            this.labelPipesDSDepth.Location = new System.Drawing.Point(405, 57);
            this.labelPipesDSDepth.Name = "labelPipesDSDepth";
            this.labelPipesDSDepth.Size = new System.Drawing.Size(71, 13);
            this.labelPipesDSDepth.TabIndex = 56;
            this.labelPipesDSDepth.Text = "DS Depth (in)";
            // 
            // labelPipesUSDepth
            // 
            this.labelPipesUSDepth.AutoSize = true;
            this.labelPipesUSDepth.Location = new System.Drawing.Point(331, 56);
            this.labelPipesUSDepth.Name = "labelPipesUSDepth";
            this.labelPipesUSDepth.Size = new System.Drawing.Size(69, 13);
            this.labelPipesUSDepth.TabIndex = 55;
            this.labelPipesUSDepth.Text = "US depth (in)";
            // 
            // buttonPipesAdd
            // 
            this.buttonPipesAdd.Location = new System.Drawing.Point(257, 184);
            this.buttonPipesAdd.Name = "buttonPipesAdd";
            this.buttonPipesAdd.Size = new System.Drawing.Size(65, 28);
            this.buttonPipesAdd.TabIndex = 52;
            this.buttonPipesAdd.Text = "Add";
            this.buttonPipesAdd.UseVisualStyleBackColor = true;
            this.buttonPipesAdd.Click += new System.EventHandler(this.buttonPipesAdd_Click);
            // 
            // numericUpDownPipesDSDepth
            // 
            this.numericUpDownPipesDSDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "ds_depth_in", true));
            this.numericUpDownPipesDSDepth.Location = new System.Drawing.Point(408, 76);
            this.numericUpDownPipesDSDepth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownPipesDSDepth.Name = "numericUpDownPipesDSDepth";
            this.numericUpDownPipesDSDepth.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownPipesDSDepth.TabIndex = 49;
            // 
            // numericUpDownPipesUSDepth
            // 
            this.numericUpDownPipesUSDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "us_depth_in", true));
            this.numericUpDownPipesUSDepth.Location = new System.Drawing.Point(356, 76);
            this.numericUpDownPipesUSDepth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownPipesUSDepth.Name = "numericUpDownPipesUSDepth";
            this.numericUpDownPipesUSDepth.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownPipesUSDepth.TabIndex = 48;
            // 
            // buttonPipesDelete
            // 
            this.buttonPipesDelete.Location = new System.Drawing.Point(257, 8);
            this.buttonPipesDelete.Name = "buttonPipesDelete";
            this.buttonPipesDelete.Size = new System.Drawing.Size(65, 28);
            this.buttonPipesDelete.TabIndex = 45;
            this.buttonPipesDelete.Text = "Delete";
            this.buttonPipesDelete.UseVisualStyleBackColor = true;
            this.buttonPipesDelete.Click += new System.EventHandler(this.buttonPipesDelete_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.usnodeDataGridViewTextBoxColumn,
            this.dsnodeDataGridViewTextBoxColumn});
            this.dataGridView3.DataSource = this.fKPIPESURVEYPAGEBindingSource;
            this.dataGridView3.Location = new System.Drawing.Point(3, 8);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.Size = new System.Drawing.Size(248, 204);
            this.dataGridView3.TabIndex = 44;
            // 
            // usnodeDataGridViewTextBoxColumn
            // 
            this.usnodeDataGridViewTextBoxColumn.DataPropertyName = "us_node";
            this.usnodeDataGridViewTextBoxColumn.HeaderText = "US Node";
            this.usnodeDataGridViewTextBoxColumn.Name = "usnodeDataGridViewTextBoxColumn";
            this.usnodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dsnodeDataGridViewTextBoxColumn
            // 
            this.dsnodeDataGridViewTextBoxColumn.DataPropertyName = "ds_node";
            this.dsnodeDataGridViewTextBoxColumn.HeaderText = "DS Nnode";
            this.dsnodeDataGridViewTextBoxColumn.Name = "dsnodeDataGridViewTextBoxColumn";
            this.dsnodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sWSPPHOTOBindingSource
            // 
            this.sWSPPHOTOBindingSource.DataMember = "SWSP_PHOTO";
            this.sWSPPHOTOBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // fKCULVERTFACINGTYPEBindingSource
            // 
            this.fKCULVERTFACINGTYPEBindingSource.DataMember = "FK_CULVERT_FACING_TYPE";
            this.fKCULVERTFACINGTYPEBindingSource.DataSource = this.sWSPFACINGTYPEBindingSource;
            // 
            // sWSPCULVERTBindingSource
            // 
            this.sWSPCULVERTBindingSource.DataMember = "SWSP_CULVERT";
            this.sWSPCULVERTBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // sWSPPIPEBindingSource
            // 
            this.sWSPPIPEBindingSource.DataMember = "SWSP_PIPE";
            this.sWSPPIPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // fKCULVERTSURVEYPAGEBindingSource
            // 
            this.fKCULVERTSURVEYPAGEBindingSource.DataMember = "FK_CULVERT_SURVEY_PAGE";
            this.fKCULVERTSURVEYPAGEBindingSource.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(822, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fIleToolStripMenuItem.Text = "FIle";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // labelView
            // 
            this.labelView.AutoSize = true;
            this.labelView.Location = new System.Drawing.Point(9, 27);
            this.labelView.Name = "labelView";
            this.labelView.Size = new System.Drawing.Size(30, 13);
            this.labelView.TabIndex = 11;
            this.labelView.Text = "View";
            // 
            // labelSurveyPage
            // 
            this.labelSurveyPage.AutoSize = true;
            this.labelSurveyPage.Location = new System.Drawing.Point(62, 27);
            this.labelSurveyPage.Name = "labelSurveyPage";
            this.labelSurveyPage.Size = new System.Drawing.Size(68, 13);
            this.labelSurveyPage.TabIndex = 12;
            this.labelSurveyPage.Text = "Survey Page";
            // 
            // labelWatershed
            // 
            this.labelWatershed.AutoSize = true;
            this.labelWatershed.Location = new System.Drawing.Point(591, 27);
            this.labelWatershed.Name = "labelWatershed";
            this.labelWatershed.Size = new System.Drawing.Size(59, 13);
            this.labelWatershed.TabIndex = 13;
            this.labelWatershed.Text = "Watershed";
            // 
            // labelSubwatershed
            // 
            this.labelSubwatershed.AutoSize = true;
            this.labelSubwatershed.Location = new System.Drawing.Point(733, 31);
            this.labelSubwatershed.Name = "labelSubwatershed";
            this.labelSubwatershed.Size = new System.Drawing.Size(75, 13);
            this.labelSubwatershed.TabIndex = 14;
            this.labelSubwatershed.Text = "Subwatershed";
            // 
            // labelSurveyDate
            // 
            this.labelSurveyDate.AutoSize = true;
            this.labelSurveyDate.Location = new System.Drawing.Point(12, 81);
            this.labelSurveyDate.Name = "labelSurveyDate";
            this.labelSurveyDate.Size = new System.Drawing.Size(64, 13);
            this.labelSurveyDate.TabIndex = 15;
            this.labelSurveyDate.Text = "Survey date";
            // 
            // labelWeather
            // 
            this.labelWeather.AutoSize = true;
            this.labelWeather.Location = new System.Drawing.Point(168, 81);
            this.labelWeather.Name = "labelWeather";
            this.labelWeather.Size = new System.Drawing.Size(48, 13);
            this.labelWeather.TabIndex = 16;
            this.labelWeather.Text = "Weather";
            // 
            // labelEvaluators
            // 
            this.labelEvaluators.AutoSize = true;
            this.labelEvaluators.Location = new System.Drawing.Point(260, 81);
            this.labelEvaluators.Name = "labelEvaluators";
            this.labelEvaluators.Size = new System.Drawing.Size(63, 13);
            this.labelEvaluators.TabIndex = 17;
            this.labelEvaluators.Text = "Evaluator(s)";
            // 
            // labelSearchNode
            // 
            this.labelSearchNode.AutoSize = true;
            this.labelSearchNode.Location = new System.Drawing.Point(36, 482);
            this.labelSearchNode.Name = "labelSearchNode";
            this.labelSearchNode.Size = new System.Drawing.Size(33, 13);
            this.labelSearchNode.TabIndex = 41;
            this.labelSearchNode.Text = "Node";
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.Location = new System.Drawing.Point(13, 376);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(56, 13);
            this.labelComments.TabIndex = 40;
            this.labelComments.Text = "Comments";
            // 
            // buttonFindNode
            // 
            this.buttonFindNode.Location = new System.Drawing.Point(225, 476);
            this.buttonFindNode.Name = "buttonFindNode";
            this.buttonFindNode.Size = new System.Drawing.Size(92, 19);
            this.buttonFindNode.TabIndex = 33;
            this.buttonFindNode.Text = "Find";
            this.buttonFindNode.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(78, 476);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(131, 20);
            this.textBox3.TabIndex = 32;
            // 
            // buttonUpdateDatabase
            // 
            this.buttonUpdateDatabase.Location = new System.Drawing.Point(680, 461);
            this.buttonUpdateDatabase.Name = "buttonUpdateDatabase";
            this.buttonUpdateDatabase.Size = new System.Drawing.Size(133, 34);
            this.buttonUpdateDatabase.TabIndex = 31;
            this.buttonUpdateDatabase.Text = "Update Database";
            this.buttonUpdateDatabase.UseVisualStyleBackColor = true;
            this.buttonUpdateDatabase.Click += new System.EventHandler(this.buttonUpdateDatabase_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 392);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(797, 63);
            this.textBox1.TabIndex = 42;
            // 
            // sWSPEVALUATORBindingSource
            // 
            this.sWSPEVALUATORBindingSource.DataMember = "SWSP_EVALUATOR";
            this.sWSPEVALUATORBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // comboBoxWatershed
            // 
            this.comboBoxWatershed.DataSource = this.sWSPWATERSHEDBindingSource;
            this.comboBoxWatershed.DisplayMember = "watershed";
            this.comboBoxWatershed.FormattingEnabled = true;
            this.comboBoxWatershed.Location = new System.Drawing.Point(540, 47);
            this.comboBoxWatershed.Name = "comboBoxWatershed";
            this.comboBoxWatershed.Size = new System.Drawing.Size(109, 21);
            this.comboBoxWatershed.TabIndex = 44;
            this.comboBoxWatershed.ValueMember = "watershed_id";
            // 
            // comboBoxSubwatershed
            // 
            this.comboBoxSubwatershed.DataSource = this.fKSUBWATERSHEDWATERSHEDBindingSource;
            this.comboBoxSubwatershed.DisplayMember = "subwatershed";
            this.comboBoxSubwatershed.FormattingEnabled = true;
            this.comboBoxSubwatershed.Location = new System.Drawing.Point(687, 46);
            this.comboBoxSubwatershed.Name = "comboBoxSubwatershed";
            this.comboBoxSubwatershed.Size = new System.Drawing.Size(120, 21);
            this.comboBoxSubwatershed.TabIndex = 45;
            this.comboBoxSubwatershed.ValueMember = "subwatershed_id";
            // 
            // sWSP_WATERSHEDTableAdapter
            // 
            this.sWSP_WATERSHEDTableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_SUBWATERSHEDTableAdapter
            // 
            this.sWSP_SUBWATERSHEDTableAdapter.ClearBeforeFill = true;
            // 
            // sWSPVIEWBindingSource
            // 
            this.sWSPVIEWBindingSource.DataMember = "SWSP_VIEW";
            this.sWSPVIEWBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // sWSP_VIEWTableAdapter
            // 
            this.sWSP_VIEWTableAdapter.ClearBeforeFill = true;
            // 
            // comboBoxView
            // 
            this.comboBoxView.DataSource = this.fKVIEWSUBWATERSHEDBindingSource;
            this.comboBoxView.DisplayMember = "view_number";
            this.comboBoxView.FormattingEnabled = true;
            this.comboBoxView.Location = new System.Drawing.Point(11, 49);
            this.comboBoxView.Name = "comboBoxView";
            this.comboBoxView.Size = new System.Drawing.Size(43, 21);
            this.comboBoxView.TabIndex = 46;
            this.comboBoxView.ValueMember = "view_id";
            // 
            // comboBoxSurveyPage
            // 
            this.comboBoxSurveyPage.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            this.comboBoxSurveyPage.DisplayMember = "page_number";
            this.comboBoxSurveyPage.FormattingEnabled = true;
            this.comboBoxSurveyPage.Location = new System.Drawing.Point(67, 49);
            this.comboBoxSurveyPage.Name = "comboBoxSurveyPage";
            this.comboBoxSurveyPage.Size = new System.Drawing.Size(62, 21);
            this.comboBoxSurveyPage.TabIndex = 47;
            this.comboBoxSurveyPage.ValueMember = "survey_page_id";
            this.comboBoxSurveyPage.SelectedValueChanged += new System.EventHandler(this.CheckEvaluatorsAssociatedWithThisSurveyPage);
            // 
            // sWSP_SURVEY_PAGETableAdapter
            // 
            this.sWSP_SURVEY_PAGETableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_EVALUATORTableAdapter
            // 
            this.sWSP_EVALUATORTableAdapter.ClearBeforeFill = true;
            // 
            // sWSPEVALUATORBindingSource1
            // 
            this.sWSPEVALUATORBindingSource1.DataMember = "SWSP_EVALUATOR";
            this.sWSPEVALUATORBindingSource1.DataSource = this.sANDBOXDataSet;
            // 
            // sWSPSURVEYPAGEEVALUATORBindingSource
            // 
            this.sWSPSURVEYPAGEEVALUATORBindingSource.DataMember = "SWSP_SURVEY_PAGE_EVALUATOR";
            this.sWSPSURVEYPAGEEVALUATORBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // sWSPEVALUATORBindingSource2
            // 
            this.sWSPEVALUATORBindingSource2.DataMember = "SWSP_EVALUATOR";
            this.sWSPEVALUATORBindingSource2.DataSource = this.sANDBOXDataSet;
            // 
            // sWSP_SURVEY_PAGE_EVALUATORTableAdapter
            // 
            this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.ClearBeforeFill = true;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.DataSource = this.sANDBOXDataSet.SWSP_EVALUATOR;
            this.checkedListBox1.DisplayMember = "Initials";
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(264, 99);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(96, 49);
            this.checkedListBox1.TabIndex = 48;
            this.checkedListBox1.ValueMember = "evaluator_id";
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // sWSP_DITCHTableAdapter
            // 
            this.sWSP_DITCHTableAdapter.ClearBeforeFill = true;
            // 
            // sWSPDITCHBindingSource
            // 
            this.sWSPDITCHBindingSource.DataMember = "SWSP_DITCH";
            this.sWSPDITCHBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // sWSP_FACING_TYPETableAdapter
            // 
            this.sWSP_FACING_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_MATERIAL_TYPETableAdapter
            // 
            this.sWSP_MATERIAL_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_PHOTOTableAdapter
            // 
            this.sWSP_PHOTOTableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_CULVERTTableAdapter
            // 
            this.sWSP_CULVERTTableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_CULVERT_OPENING_TYPETableAdapter
            // 
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_SHAPE_TYPETableAdapter
            // 
            this.sWSP_SHAPE_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_PIPETableAdapter
            // 
            this.sWSP_PIPETableAdapter.ClearBeforeFill = true;
            // 
            // sWSPCULVERTBindingSource1
            // 
            this.sWSPCULVERTBindingSource1.DataMember = "SWSP_CULVERT";
            this.sWSPCULVERTBindingSource1.DataSource = this.sANDBOXDataSet;
            // 
            // sWSP_GLOBAL_IDBindingSource
            // 
            this.sWSP_GLOBAL_IDBindingSource.DataMember = "SWSP_GLOBAL_ID";
            this.sWSP_GLOBAL_IDBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // sWSP_GLOBAL_IDTableAdapter
            // 
            this.sWSP_GLOBAL_IDTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 498);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.comboBoxSurveyPage);
            this.Controls.Add(this.comboBoxView);
            this.Controls.Add(this.comboBoxSubwatershed);
            this.Controls.Add(this.comboBoxWatershed);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelSearchNode);
            this.Controls.Add(this.labelEvaluators);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.labelWeather);
            this.Controls.Add(this.labelSurveyDate);
            this.Controls.Add(this.labelSubwatershed);
            this.Controls.Add(this.labelWatershed);
            this.Controls.Add(this.labelSurveyPage);
            this.Controls.Add(this.labelView);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonFindNode);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.listBoxWeather);
            this.Controls.Add(this.buttonUpdateDatabase);
            this.Controls.Add(this.dateTimePickerSurveyDate);
            this.Controls.Add(this.buttonAddSurveyPage);
            this.Controls.Add(this.buttonAddView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Stormwater Interface";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKDITCHSURVEYPAGEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPMATERIALTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDitchesTopWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDitchesDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDitchesBottomWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPageCulverts.ResumeLayout(false);
            this.tabPageCulverts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSHAPETYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTOPENINGTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTSURVEYPAGEBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCulvertsUnobstructedHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCulvertsFullDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPESURVEYPAGEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipesInnerDiameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipesDSDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipesUSDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPPHOTOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTFACINGTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPPIPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTSURVEYPAGEBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPEVALUATORBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPVIEWBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPEVALUATORBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSURVEYPAGEEVALUATORBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPEVALUATORBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPDITCHBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSP_GLOBAL_IDBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddView;
        private System.Windows.Forms.Button buttonAddSurveyPage;
        private System.Windows.Forms.DateTimePicker dateTimePickerSurveyDate;
        private System.Windows.Forms.ListBox listBoxWeather;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPageCulverts;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.Label labelView;
        private System.Windows.Forms.Label labelSurveyPage;
        private System.Windows.Forms.Label labelWatershed;
        private System.Windows.Forms.Label labelSubwatershed;
        private System.Windows.Forms.Label labelSurveyDate;
        private System.Windows.Forms.Label labelWeather;
        private System.Windows.Forms.Label labelEvaluators;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label labelSearchNode;
        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.Label labelCulvertsMaterial;
        private System.Windows.Forms.Label labelCulvertsUnobstructedHeight;
        private System.Windows.Forms.Label labelCulvertsFullDepth;
        private System.Windows.Forms.Label labelCulvertsShape;
        private System.Windows.Forms.Label labelCulvertsType;
        private System.Windows.Forms.Button buttonFindNode;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button buttonUpdateDatabase;
        private System.Windows.Forms.Button buttonCulvertsAdd;
        private System.Windows.Forms.NumericUpDown numericUpDownCulvertsUnobstructedHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownCulvertsFullDepth;
        private System.Windows.Forms.Button buttonCulvertsDelete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelDitchesMaterial;
        private System.Windows.Forms.Label labelDitchesBottomWidth;
        private System.Windows.Forms.Label labelDitchesTopWidth;
        private System.Windows.Forms.Label labelDitchesDepth;
        private System.Windows.Forms.Button buttonDitchesAdd;
        private System.Windows.Forms.NumericUpDown numericUpDownDitchesBottomWidth;
        private System.Windows.Forms.Button buttonDitchesDelete;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox textBoxCulvertsNode;
        private System.Windows.Forms.Label labelCulvertsNode;
        private System.Windows.Forms.Label labelCulvertsFacingDirection;
        private System.Windows.Forms.TextBox textBoxDitchesNode;
        private System.Windows.Forms.Label labelDitchesNode;
        private System.Windows.Forms.Label labelDitchesFacingDirection;
        private System.Windows.Forms.NumericUpDown numericUpDownDitchesTopWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownDitchesDepth;
        private System.Windows.Forms.TextBox textBoxPipesUSNode;
        private System.Windows.Forms.Label labelPipesUSNode;
        private System.Windows.Forms.Label labelPipesDSNode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPipesDSDepth;
        private System.Windows.Forms.Label labelPipesUSDepth;
        private System.Windows.Forms.Button buttonPipesAdd;
        private System.Windows.Forms.NumericUpDown numericUpDownPipesDSDepth;
        private System.Windows.Forms.NumericUpDown numericUpDownPipesUSDepth;
        private System.Windows.Forms.Button buttonPipesDelete;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TextBox textBoxPipesDSNode;
        private System.Windows.Forms.Label labelPipesInnerDiameter;
        private System.Windows.Forms.NumericUpDown numericUpDownPipesInnerDiameter;
        private System.Windows.Forms.Label labelPipesShape;
        private System.Windows.Forms.ComboBox comboBoxWatershed;
        private System.Windows.Forms.ComboBox comboBoxSubwatershed;
        private SANDBOXDataSet sANDBOXDataSet;
        private System.Windows.Forms.BindingSource sWSPWATERSHEDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_WATERSHEDTableAdapter sWSP_WATERSHEDTableAdapter;
        private System.Windows.Forms.BindingSource fKSUBWATERSHEDWATERSHEDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_SUBWATERSHEDTableAdapter sWSP_SUBWATERSHEDTableAdapter;
        private System.Windows.Forms.BindingSource sWSPVIEWBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_VIEWTableAdapter sWSP_VIEWTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxView;
        private System.Windows.Forms.BindingSource fKVIEWSUBWATERSHEDBindingSource;
        private System.Windows.Forms.ComboBox comboBoxSurveyPage;
        private System.Windows.Forms.BindingSource fKSURVEYPAGEVIEWBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGETableAdapter sWSP_SURVEY_PAGETableAdapter;
        private System.Windows.Forms.BindingSource sWSPEVALUATORBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_EVALUATORTableAdapter sWSP_EVALUATORTableAdapter;
        private System.Windows.Forms.BindingSource sWSPEVALUATORBindingSource1;
        private System.Windows.Forms.BindingSource sWSPEVALUATORBindingSource2;
        private System.Windows.Forms.BindingSource sWSPSURVEYPAGEEVALUATORBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGE_EVALUATORTableAdapter sWSP_SURVEY_PAGE_EVALUATORTableAdapter;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.BindingSource fKDITCHSURVEYPAGEBindingSource;
        private System.Windows.Forms.BindingSource fKCULVERTSURVEYPAGEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_DITCHTableAdapter sWSP_DITCHTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn facingDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sWSPDITCHBindingSource;
        private System.Windows.Forms.BindingSource sWSPFACINGTYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_FACING_TYPETableAdapter sWSP_FACING_TYPETableAdapter;
        private System.Windows.Forms.BindingSource sWSPMATERIALTYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_MATERIAL_TYPETableAdapter sWSP_MATERIAL_TYPETableAdapter;
        private System.Windows.Forms.ComboBox comboBoxDitchesFacingDirection;
        private System.Windows.Forms.ComboBox comboBoxDitchesMaterial;
        private System.Windows.Forms.BindingSource sWSPPHOTOBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_PHOTOTableAdapter sWSP_PHOTOTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxCulvertsMaterial;
        private System.Windows.Forms.ComboBox comboBoxCulvertsFacingDirection;
        private System.Windows.Forms.ComboBox comboBoxPipesShape;
        private System.Windows.Forms.ComboBox comboBoxPipesMaterial;
        private System.Windows.Forms.ComboBox comboBoxCulvertsType;
        private System.Windows.Forms.ComboBox comboBoxCulvertsShape;
        private System.Windows.Forms.BindingSource fKCULVERTFACINGTYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_CULVERTTableAdapter sWSP_CULVERTTableAdapter;
        private System.Windows.Forms.BindingSource sWSPCULVERTOPENINGTYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_CULVERT_OPENING_TYPETableAdapter sWSP_CULVERT_OPENING_TYPETableAdapter;
        private System.Windows.Forms.BindingSource sWSPSHAPETYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_SHAPE_TYPETableAdapter sWSP_SHAPE_TYPETableAdapter;
        private System.Windows.Forms.BindingSource sWSPCULVERTBindingSource;
        private System.Windows.Forms.BindingSource sWSPPIPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_PIPETableAdapter sWSP_PIPETableAdapter;
        private System.Windows.Forms.BindingSource fKCULVERTSURVEYPAGEBindingSource1;
        private System.Windows.Forms.BindingSource sWSPCULVERTBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn facingDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource fKPIPESURVEYPAGEBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn usnodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dsnodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button buttonDitchesViewAddPhotos;
        private System.Windows.Forms.Button buttonCulvertsViewAddPhotos;
        private System.Windows.Forms.Button buttonPipesViewAddPhotos;
        private System.Windows.Forms.BindingSource sWSP_GLOBAL_IDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_GLOBAL_IDTableAdapter sWSP_GLOBAL_IDTableAdapter;
        private System.Windows.Forms.Button buttonUpdateDitch;
        private System.Windows.Forms.Button buttonUpdateCulvert;
        private System.Windows.Forms.Button buttonUpdatePipe;
    }
}

