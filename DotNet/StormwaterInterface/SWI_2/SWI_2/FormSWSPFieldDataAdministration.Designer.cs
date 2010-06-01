namespace SWI_2
{
    partial class FormSWSPFieldDataAdministration
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageWatersheds = new System.Windows.Forms.TabPage();
            this.buttonSubwatershedsDelete = new System.Windows.Forms.Button();
            this.buttonSubwatershedsAdd = new System.Windows.Forms.Button();
            this.buttonWatershedsDelete = new System.Windows.Forms.Button();
            this.buttonWatershedsAdd = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelSubwatersheds = new System.Windows.Forms.Label();
            this.labelWatersheds = new System.Windows.Forms.Label();
            this.tabPageEvaluators = new System.Windows.Forms.TabPage();
            this.buttonEvaluatorsDelete = new System.Windows.Forms.Button();
            this.buttonEvaluatorsAdd = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.labelEvaluators = new System.Windows.Forms.Label();
            this.tabPageTypes = new System.Windows.Forms.TabPage();
            this.buttonMaterialsDelete = new System.Windows.Forms.Button();
            this.buttonMaterialsAdd = new System.Windows.Forms.Button();
            this.buttonShapesDelete = new System.Windows.Forms.Button();
            this.buttonShapesAdd = new System.Windows.Forms.Button();
            this.buttonFacingsDelete = new System.Windows.Forms.Button();
            this.buttonFacingsAdd = new System.Windows.Forms.Button();
            this.buttonCulvertOpeningsDelete = new System.Windows.Forms.Button();
            this.buttonCulvertOpeningsAdd = new System.Windows.Forms.Button();
            this.labelMaterials = new System.Windows.Forms.Label();
            this.labelShapes = new System.Windows.Forms.Label();
            this.dataGridView7 = new System.Windows.Forms.DataGridView();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.labelFacings = new System.Windows.Forms.Label();
            this.labelCulvertOpenings = new System.Windows.Forms.Label();
            this.tabPageViewsAndSurveys = new System.Windows.Forms.TabPage();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAddSurveyPage = new System.Windows.Forms.Button();
            this.buttonAddView = new System.Windows.Forms.Button();
            this.dataGridView8 = new System.Windows.Forms.DataGridView();
            this.labelSubwatershed = new System.Windows.Forms.Label();
            this.labelWatershed = new System.Windows.Forms.Label();
            this.labelViews = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.comboBoxWatershed = new System.Windows.Forms.ComboBox();
            this.comboBoxSubwatershed = new System.Windows.Forms.ComboBox();
            this.sANDBOXDataSet = new SWI_2.SANDBOXDataSet();
            this.sWSPWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_WATERSHEDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_WATERSHEDTableAdapter();
            this.fKSUBWATERSHEDWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_SUBWATERSHEDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SUBWATERSHEDTableAdapter();
            this.tabControl1.SuspendLayout();
            this.tabPageWatersheds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPageEvaluators.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPageTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.tabPageViewsAndSurveys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPageWatersheds);
            this.tabControl1.Controls.Add(this.tabPageEvaluators);
            this.tabControl1.Controls.Add(this.tabPageTypes);
            this.tabControl1.Controls.Add(this.tabPageViewsAndSurveys);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(25, 100);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(652, 385);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPageWatersheds
            // 
            this.tabPageWatersheds.Controls.Add(this.buttonSubwatershedsDelete);
            this.tabPageWatersheds.Controls.Add(this.buttonSubwatershedsAdd);
            this.tabPageWatersheds.Controls.Add(this.buttonWatershedsDelete);
            this.tabPageWatersheds.Controls.Add(this.buttonWatershedsAdd);
            this.tabPageWatersheds.Controls.Add(this.dataGridView2);
            this.tabPageWatersheds.Controls.Add(this.dataGridView1);
            this.tabPageWatersheds.Controls.Add(this.labelSubwatersheds);
            this.tabPageWatersheds.Controls.Add(this.labelWatersheds);
            this.tabPageWatersheds.Location = new System.Drawing.Point(104, 4);
            this.tabPageWatersheds.Name = "tabPageWatersheds";
            this.tabPageWatersheds.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWatersheds.Size = new System.Drawing.Size(544, 377);
            this.tabPageWatersheds.TabIndex = 0;
            this.tabPageWatersheds.Text = "Watersheds";
            this.tabPageWatersheds.UseVisualStyleBackColor = true;
            // 
            // buttonSubwatershedsDelete
            // 
            this.buttonSubwatershedsDelete.Location = new System.Drawing.Point(354, 330);
            this.buttonSubwatershedsDelete.Name = "buttonSubwatershedsDelete";
            this.buttonSubwatershedsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonSubwatershedsDelete.TabIndex = 7;
            this.buttonSubwatershedsDelete.Text = "Delete";
            this.buttonSubwatershedsDelete.UseVisualStyleBackColor = true;
            // 
            // buttonSubwatershedsAdd
            // 
            this.buttonSubwatershedsAdd.Location = new System.Drawing.Point(221, 330);
            this.buttonSubwatershedsAdd.Name = "buttonSubwatershedsAdd";
            this.buttonSubwatershedsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonSubwatershedsAdd.TabIndex = 6;
            this.buttonSubwatershedsAdd.Text = "Add";
            this.buttonSubwatershedsAdd.UseVisualStyleBackColor = true;
            // 
            // buttonWatershedsDelete
            // 
            this.buttonWatershedsDelete.Location = new System.Drawing.Point(121, 330);
            this.buttonWatershedsDelete.Name = "buttonWatershedsDelete";
            this.buttonWatershedsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonWatershedsDelete.TabIndex = 5;
            this.buttonWatershedsDelete.Text = "Delete";
            this.buttonWatershedsDelete.UseVisualStyleBackColor = true;
            // 
            // buttonWatershedsAdd
            // 
            this.buttonWatershedsAdd.Location = new System.Drawing.Point(24, 330);
            this.buttonWatershedsAdd.Name = "buttonWatershedsAdd";
            this.buttonWatershedsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonWatershedsAdd.TabIndex = 4;
            this.buttonWatershedsAdd.Text = "Add";
            this.buttonWatershedsAdd.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(221, 40);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(203, 284);
            this.dataGridView2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(169, 284);
            this.dataGridView1.TabIndex = 2;
            // 
            // labelSubwatersheds
            // 
            this.labelSubwatersheds.AutoSize = true;
            this.labelSubwatersheds.Location = new System.Drawing.Point(225, 12);
            this.labelSubwatersheds.Name = "labelSubwatersheds";
            this.labelSubwatersheds.Size = new System.Drawing.Size(80, 13);
            this.labelSubwatersheds.TabIndex = 1;
            this.labelSubwatersheds.Text = "Subwatersheds";
            // 
            // labelWatersheds
            // 
            this.labelWatersheds.AutoSize = true;
            this.labelWatersheds.Location = new System.Drawing.Point(18, 12);
            this.labelWatersheds.Name = "labelWatersheds";
            this.labelWatersheds.Size = new System.Drawing.Size(64, 13);
            this.labelWatersheds.TabIndex = 0;
            this.labelWatersheds.Text = "Watersheds";
            // 
            // tabPageEvaluators
            // 
            this.tabPageEvaluators.Controls.Add(this.buttonEvaluatorsDelete);
            this.tabPageEvaluators.Controls.Add(this.buttonEvaluatorsAdd);
            this.tabPageEvaluators.Controls.Add(this.dataGridView3);
            this.tabPageEvaluators.Controls.Add(this.labelEvaluators);
            this.tabPageEvaluators.Location = new System.Drawing.Point(104, 4);
            this.tabPageEvaluators.Name = "tabPageEvaluators";
            this.tabPageEvaluators.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEvaluators.Size = new System.Drawing.Size(544, 377);
            this.tabPageEvaluators.TabIndex = 1;
            this.tabPageEvaluators.Text = "Evaluators";
            this.tabPageEvaluators.UseVisualStyleBackColor = true;
            // 
            // buttonEvaluatorsDelete
            // 
            this.buttonEvaluatorsDelete.Location = new System.Drawing.Point(460, 347);
            this.buttonEvaluatorsDelete.Name = "buttonEvaluatorsDelete";
            this.buttonEvaluatorsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonEvaluatorsDelete.TabIndex = 9;
            this.buttonEvaluatorsDelete.Text = "Delete";
            this.buttonEvaluatorsDelete.UseVisualStyleBackColor = true;
            // 
            // buttonEvaluatorsAdd
            // 
            this.buttonEvaluatorsAdd.Location = new System.Drawing.Point(327, 347);
            this.buttonEvaluatorsAdd.Name = "buttonEvaluatorsAdd";
            this.buttonEvaluatorsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonEvaluatorsAdd.TabIndex = 8;
            this.buttonEvaluatorsAdd.Text = "Add";
            this.buttonEvaluatorsAdd.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(14, 39);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(516, 302);
            this.dataGridView3.TabIndex = 1;
            // 
            // labelEvaluators
            // 
            this.labelEvaluators.AutoSize = true;
            this.labelEvaluators.Location = new System.Drawing.Point(16, 13);
            this.labelEvaluators.Name = "labelEvaluators";
            this.labelEvaluators.Size = new System.Drawing.Size(57, 13);
            this.labelEvaluators.TabIndex = 0;
            this.labelEvaluators.Text = "Evaluators";
            // 
            // tabPageTypes
            // 
            this.tabPageTypes.Controls.Add(this.buttonMaterialsDelete);
            this.tabPageTypes.Controls.Add(this.buttonMaterialsAdd);
            this.tabPageTypes.Controls.Add(this.buttonShapesDelete);
            this.tabPageTypes.Controls.Add(this.buttonShapesAdd);
            this.tabPageTypes.Controls.Add(this.buttonFacingsDelete);
            this.tabPageTypes.Controls.Add(this.buttonFacingsAdd);
            this.tabPageTypes.Controls.Add(this.buttonCulvertOpeningsDelete);
            this.tabPageTypes.Controls.Add(this.buttonCulvertOpeningsAdd);
            this.tabPageTypes.Controls.Add(this.labelMaterials);
            this.tabPageTypes.Controls.Add(this.labelShapes);
            this.tabPageTypes.Controls.Add(this.dataGridView7);
            this.tabPageTypes.Controls.Add(this.dataGridView6);
            this.tabPageTypes.Controls.Add(this.dataGridView5);
            this.tabPageTypes.Controls.Add(this.dataGridView4);
            this.tabPageTypes.Controls.Add(this.labelFacings);
            this.tabPageTypes.Controls.Add(this.labelCulvertOpenings);
            this.tabPageTypes.Location = new System.Drawing.Point(104, 4);
            this.tabPageTypes.Name = "tabPageTypes";
            this.tabPageTypes.Size = new System.Drawing.Size(544, 377);
            this.tabPageTypes.TabIndex = 2;
            this.tabPageTypes.Text = "Types";
            this.tabPageTypes.UseVisualStyleBackColor = true;
            // 
            // buttonMaterialsDelete
            // 
            this.buttonMaterialsDelete.Location = new System.Drawing.Point(351, 293);
            this.buttonMaterialsDelete.Name = "buttonMaterialsDelete";
            this.buttonMaterialsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonMaterialsDelete.TabIndex = 17;
            this.buttonMaterialsDelete.Text = "Delete";
            this.buttonMaterialsDelete.UseVisualStyleBackColor = true;
            // 
            // buttonMaterialsAdd
            // 
            this.buttonMaterialsAdd.Location = new System.Drawing.Point(258, 293);
            this.buttonMaterialsAdd.Name = "buttonMaterialsAdd";
            this.buttonMaterialsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonMaterialsAdd.TabIndex = 16;
            this.buttonMaterialsAdd.Text = "Add";
            this.buttonMaterialsAdd.UseVisualStyleBackColor = true;
            // 
            // buttonShapesDelete
            // 
            this.buttonShapesDelete.Location = new System.Drawing.Point(119, 293);
            this.buttonShapesDelete.Name = "buttonShapesDelete";
            this.buttonShapesDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonShapesDelete.TabIndex = 15;
            this.buttonShapesDelete.Text = "Delete";
            this.buttonShapesDelete.UseVisualStyleBackColor = true;
            // 
            // buttonShapesAdd
            // 
            this.buttonShapesAdd.Location = new System.Drawing.Point(26, 293);
            this.buttonShapesAdd.Name = "buttonShapesAdd";
            this.buttonShapesAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonShapesAdd.TabIndex = 14;
            this.buttonShapesAdd.Text = "Add";
            this.buttonShapesAdd.UseVisualStyleBackColor = true;
            // 
            // buttonFacingsDelete
            // 
            this.buttonFacingsDelete.Location = new System.Drawing.Point(338, 98);
            this.buttonFacingsDelete.Name = "buttonFacingsDelete";
            this.buttonFacingsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonFacingsDelete.TabIndex = 13;
            this.buttonFacingsDelete.Text = "Delete";
            this.buttonFacingsDelete.UseVisualStyleBackColor = true;
            // 
            // buttonFacingsAdd
            // 
            this.buttonFacingsAdd.Location = new System.Drawing.Point(245, 98);
            this.buttonFacingsAdd.Name = "buttonFacingsAdd";
            this.buttonFacingsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonFacingsAdd.TabIndex = 12;
            this.buttonFacingsAdd.Text = "Add";
            this.buttonFacingsAdd.UseVisualStyleBackColor = true;
            // 
            // buttonCulvertOpeningsDelete
            // 
            this.buttonCulvertOpeningsDelete.Location = new System.Drawing.Point(111, 98);
            this.buttonCulvertOpeningsDelete.Name = "buttonCulvertOpeningsDelete";
            this.buttonCulvertOpeningsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonCulvertOpeningsDelete.TabIndex = 11;
            this.buttonCulvertOpeningsDelete.Text = "Delete";
            this.buttonCulvertOpeningsDelete.UseVisualStyleBackColor = true;
            // 
            // buttonCulvertOpeningsAdd
            // 
            this.buttonCulvertOpeningsAdd.Location = new System.Drawing.Point(18, 98);
            this.buttonCulvertOpeningsAdd.Name = "buttonCulvertOpeningsAdd";
            this.buttonCulvertOpeningsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonCulvertOpeningsAdd.TabIndex = 10;
            this.buttonCulvertOpeningsAdd.Text = "Add";
            this.buttonCulvertOpeningsAdd.UseVisualStyleBackColor = true;
            // 
            // labelMaterials
            // 
            this.labelMaterials.AutoSize = true;
            this.labelMaterials.Location = new System.Drawing.Point(257, 191);
            this.labelMaterials.Name = "labelMaterials";
            this.labelMaterials.Size = new System.Drawing.Size(49, 13);
            this.labelMaterials.TabIndex = 7;
            this.labelMaterials.Text = "Materials";
            // 
            // labelShapes
            // 
            this.labelShapes.AutoSize = true;
            this.labelShapes.Location = new System.Drawing.Point(29, 196);
            this.labelShapes.Name = "labelShapes";
            this.labelShapes.Size = new System.Drawing.Size(43, 13);
            this.labelShapes.TabIndex = 6;
            this.labelShapes.Text = "Shapes";
            // 
            // dataGridView7
            // 
            this.dataGridView7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView7.Location = new System.Drawing.Point(258, 222);
            this.dataGridView7.Name = "dataGridView7";
            this.dataGridView7.Size = new System.Drawing.Size(166, 64);
            this.dataGridView7.TabIndex = 5;
            // 
            // dataGridView6
            // 
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView6.Location = new System.Drawing.Point(26, 225);
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.Size = new System.Drawing.Size(154, 62);
            this.dataGridView6.TabIndex = 4;
            // 
            // dataGridView5
            // 
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Location = new System.Drawing.Point(245, 36);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.Size = new System.Drawing.Size(180, 55);
            this.dataGridView5.TabIndex = 3;
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(18, 32);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(163, 60);
            this.dataGridView4.TabIndex = 2;
            // 
            // labelFacings
            // 
            this.labelFacings.AutoSize = true;
            this.labelFacings.Location = new System.Drawing.Point(239, 13);
            this.labelFacings.Name = "labelFacings";
            this.labelFacings.Size = new System.Drawing.Size(44, 13);
            this.labelFacings.TabIndex = 1;
            this.labelFacings.Text = "Facings";
            // 
            // labelCulvertOpenings
            // 
            this.labelCulvertOpenings.AutoSize = true;
            this.labelCulvertOpenings.Location = new System.Drawing.Point(13, 9);
            this.labelCulvertOpenings.Name = "labelCulvertOpenings";
            this.labelCulvertOpenings.Size = new System.Drawing.Size(88, 13);
            this.labelCulvertOpenings.TabIndex = 0;
            this.labelCulvertOpenings.Text = "Culvert Openings";
            // 
            // tabPageViewsAndSurveys
            // 
            this.tabPageViewsAndSurveys.Controls.Add(this.comboBoxSubwatershed);
            this.tabPageViewsAndSurveys.Controls.Add(this.comboBoxWatershed);
            this.tabPageViewsAndSurveys.Controls.Add(this.buttonDelete);
            this.tabPageViewsAndSurveys.Controls.Add(this.buttonAddSurveyPage);
            this.tabPageViewsAndSurveys.Controls.Add(this.buttonAddView);
            this.tabPageViewsAndSurveys.Controls.Add(this.dataGridView8);
            this.tabPageViewsAndSurveys.Controls.Add(this.labelSubwatershed);
            this.tabPageViewsAndSurveys.Controls.Add(this.labelWatershed);
            this.tabPageViewsAndSurveys.Controls.Add(this.labelViews);
            this.tabPageViewsAndSurveys.Location = new System.Drawing.Point(104, 4);
            this.tabPageViewsAndSurveys.Name = "tabPageViewsAndSurveys";
            this.tabPageViewsAndSurveys.Size = new System.Drawing.Size(544, 377);
            this.tabPageViewsAndSurveys.TabIndex = 3;
            this.tabPageViewsAndSurveys.Text = "Views and Surveys";
            this.tabPageViewsAndSurveys.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(393, 335);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(127, 26);
            this.buttonDelete.TabIndex = 8;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // buttonAddSurveyPage
            // 
            this.buttonAddSurveyPage.Location = new System.Drawing.Point(261, 335);
            this.buttonAddSurveyPage.Name = "buttonAddSurveyPage";
            this.buttonAddSurveyPage.Size = new System.Drawing.Size(126, 26);
            this.buttonAddSurveyPage.TabIndex = 7;
            this.buttonAddSurveyPage.Text = "Add survey page...";
            this.buttonAddSurveyPage.UseVisualStyleBackColor = true;
            this.buttonAddSurveyPage.Click += new System.EventHandler(this.buttonAddSurveyPage_Click);
            // 
            // buttonAddView
            // 
            this.buttonAddView.Location = new System.Drawing.Point(141, 335);
            this.buttonAddView.Name = "buttonAddView";
            this.buttonAddView.Size = new System.Drawing.Size(114, 26);
            this.buttonAddView.TabIndex = 6;
            this.buttonAddView.Text = "Add view...";
            this.buttonAddView.UseVisualStyleBackColor = true;
            this.buttonAddView.Click += new System.EventHandler(this.buttonAddView_Click);
            // 
            // dataGridView8
            // 
            this.dataGridView8.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView8.Location = new System.Drawing.Point(14, 89);
            this.dataGridView8.Name = "dataGridView8";
            this.dataGridView8.Size = new System.Drawing.Size(506, 236);
            this.dataGridView8.TabIndex = 5;
            // 
            // labelSubwatershed
            // 
            this.labelSubwatershed.AutoSize = true;
            this.labelSubwatershed.Location = new System.Drawing.Point(229, 33);
            this.labelSubwatershed.Name = "labelSubwatershed";
            this.labelSubwatershed.Size = new System.Drawing.Size(75, 13);
            this.labelSubwatershed.TabIndex = 2;
            this.labelSubwatershed.Text = "Subwatershed";
            // 
            // labelWatershed
            // 
            this.labelWatershed.AutoSize = true;
            this.labelWatershed.Location = new System.Drawing.Point(9, 33);
            this.labelWatershed.Name = "labelWatershed";
            this.labelWatershed.Size = new System.Drawing.Size(59, 13);
            this.labelWatershed.TabIndex = 1;
            this.labelWatershed.Text = "Watershed";
            // 
            // labelViews
            // 
            this.labelViews.AutoSize = true;
            this.labelViews.Location = new System.Drawing.Point(7, 5);
            this.labelViews.Name = "labelViews";
            this.labelViews.Size = new System.Drawing.Size(35, 13);
            this.labelViews.TabIndex = 0;
            this.labelViews.Text = "Views";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(559, 403);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(105, 33);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(448, 403);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(105, 33);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // comboBoxWatershed
            // 
            this.comboBoxWatershed.DataSource = this.sWSPWATERSHEDBindingSource;
            this.comboBoxWatershed.DisplayMember = "watershed";
            this.comboBoxWatershed.FormattingEnabled = true;
            this.comboBoxWatershed.Location = new System.Drawing.Point(82, 31);
            this.comboBoxWatershed.Name = "comboBoxWatershed";
            this.comboBoxWatershed.Size = new System.Drawing.Size(100, 21);
            this.comboBoxWatershed.TabIndex = 9;
            this.comboBoxWatershed.ValueMember = "watershed_id";
            // 
            // comboBoxSubwatershed
            // 
            this.comboBoxSubwatershed.DataSource = this.fKSUBWATERSHEDWATERSHEDBindingSource;
            this.comboBoxSubwatershed.DisplayMember = "subwatershed";
            this.comboBoxSubwatershed.FormattingEnabled = true;
            this.comboBoxSubwatershed.Location = new System.Drawing.Point(332, 30);
            this.comboBoxSubwatershed.Name = "comboBoxSubwatershed";
            this.comboBoxSubwatershed.Size = new System.Drawing.Size(138, 21);
            this.comboBoxSubwatershed.TabIndex = 10;
            this.comboBoxSubwatershed.ValueMember = "subwatershed_id";
            // 
            // sANDBOXDataSet
            // 
            this.sANDBOXDataSet.DataSetName = "SANDBOXDataSet";
            this.sANDBOXDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sWSPWATERSHEDBindingSource
            // 
            this.sWSPWATERSHEDBindingSource.DataMember = "SWSP_WATERSHED";
            this.sWSPWATERSHEDBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // sWSP_WATERSHEDTableAdapter
            // 
            this.sWSP_WATERSHEDTableAdapter.ClearBeforeFill = true;
            // 
            // fKSUBWATERSHEDWATERSHEDBindingSource
            // 
            this.fKSUBWATERSHEDWATERSHEDBindingSource.DataMember = "FK_SUBWATERSHED_WATERSHED";
            this.fKSUBWATERSHEDWATERSHEDBindingSource.DataSource = this.sWSPWATERSHEDBindingSource;
            // 
            // sWSP_SUBWATERSHEDTableAdapter
            // 
            this.sWSP_SUBWATERSHEDTableAdapter.ClearBeforeFill = true;
            // 
            // FormSWSPFieldDataAdministration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 444);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormSWSPFieldDataAdministration";
            this.Text = "FormSWSPFieldDataAdministration";
            this.Load += new System.EventHandler(this.FormSWSPFieldDataAdministration_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageWatersheds.ResumeLayout(false);
            this.tabPageWatersheds.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPageEvaluators.ResumeLayout(false);
            this.tabPageEvaluators.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPageTypes.ResumeLayout(false);
            this.tabPageTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.tabPageViewsAndSurveys.ResumeLayout(false);
            this.tabPageViewsAndSurveys.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageWatersheds;
        private System.Windows.Forms.TabPage tabPageEvaluators;
        private System.Windows.Forms.TabPage tabPageTypes;
        private System.Windows.Forms.TabPage tabPageViewsAndSurveys;
        private System.Windows.Forms.Label labelWatersheds;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelSubwatersheds;
        private System.Windows.Forms.Button buttonSubwatershedsDelete;
        private System.Windows.Forms.Button buttonSubwatershedsAdd;
        private System.Windows.Forms.Button buttonWatershedsDelete;
        private System.Windows.Forms.Button buttonWatershedsAdd;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonEvaluatorsDelete;
        private System.Windows.Forms.Button buttonEvaluatorsAdd;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label labelEvaluators;
        private System.Windows.Forms.Button buttonMaterialsDelete;
        private System.Windows.Forms.Button buttonMaterialsAdd;
        private System.Windows.Forms.Button buttonShapesDelete;
        private System.Windows.Forms.Button buttonShapesAdd;
        private System.Windows.Forms.Button buttonFacingsDelete;
        private System.Windows.Forms.Button buttonFacingsAdd;
        private System.Windows.Forms.Button buttonCulvertOpeningsDelete;
        private System.Windows.Forms.Button buttonCulvertOpeningsAdd;
        private System.Windows.Forms.Label labelMaterials;
        private System.Windows.Forms.Label labelShapes;
        private System.Windows.Forms.DataGridView dataGridView7;
        private System.Windows.Forms.DataGridView dataGridView6;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label labelFacings;
        private System.Windows.Forms.Label labelCulvertOpenings;
        private System.Windows.Forms.Label labelSubwatershed;
        private System.Windows.Forms.Label labelWatershed;
        private System.Windows.Forms.Label labelViews;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAddSurveyPage;
        private System.Windows.Forms.Button buttonAddView;
        private System.Windows.Forms.DataGridView dataGridView8;
        private System.Windows.Forms.ComboBox comboBoxSubwatershed;
        private System.Windows.Forms.ComboBox comboBoxWatershed;
        private SANDBOXDataSet sANDBOXDataSet;
        private System.Windows.Forms.BindingSource sWSPWATERSHEDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_WATERSHEDTableAdapter sWSP_WATERSHEDTableAdapter;
        private System.Windows.Forms.BindingSource fKSUBWATERSHEDWATERSHEDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_SUBWATERSHEDTableAdapter sWSP_SUBWATERSHEDTableAdapter;
    }
}