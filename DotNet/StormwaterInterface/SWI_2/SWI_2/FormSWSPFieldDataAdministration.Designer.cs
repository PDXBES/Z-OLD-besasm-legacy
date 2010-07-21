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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_VIEW_SUBWATERSHED", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("view_id", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("subwatershed_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("view_number");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_SURVEY_PAGE_VIEW");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_SURVEY_PAGE_VIEW", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("view_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("page_number");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("date");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("weather");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_SURVEY_PAGE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_SURVEY_PAGE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_SURVEY_PAGE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_SURVEY_PAGE_EVALUATOR_SURVEY_PAGE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_SURVEY_PAGE", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_SURVEY_PAGE", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_SURVEY_PAGE", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_SURVEY_PAGE_EVALUATOR_SURVEY_PAGE", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("evaluator_id");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand7 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 2);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand8 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 4);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand9 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 6);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            this.tabControlFormSWSPFieldDataAdministration = new System.Windows.Forms.TabControl();
            this.tabPageWatersheds = new System.Windows.Forms.TabPage();
            this.buttonSubwatershedsUpdate = new System.Windows.Forms.Button();
            this.buttonWatershedsUpdate = new System.Windows.Forms.Button();
            this.buttonSubwatershedsDelete = new System.Windows.Forms.Button();
            this.buttonSubwatershedsAdd = new System.Windows.Forms.Button();
            this.buttonWatershedsDelete = new System.Windows.Forms.Button();
            this.buttonWatershedsAdd = new System.Windows.Forms.Button();
            this.dataGridViewSubwatersheds = new System.Windows.Forms.DataGridView();
            this.subwatershed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fKSUBWATERSHEDWATERSHEDBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sANDBOXDataSet = new SWI_2.SANDBOXDataSet();
            this.dataGridViewWatersheds = new System.Windows.Forms.DataGridView();
            this.watershedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelSubwatersheds = new System.Windows.Forms.Label();
            this.labelWatersheds = new System.Windows.Forms.Label();
            this.tabPageEvaluators = new System.Windows.Forms.TabPage();
            this.buttonEvaluatorsUpdate = new System.Windows.Forms.Button();
            this.buttonEvaluatorsDelete = new System.Windows.Forms.Button();
            this.buttonEvaluatorsAdd = new System.Windows.Forms.Button();
            this.dataGridViewEvaluators = new System.Windows.Forms.DataGridView();
            this.initialsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sWSPEVALUATORBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelEvaluators = new System.Windows.Forms.Label();
            this.tabPageTypes = new System.Windows.Forms.TabPage();
            this.buttonShapesUpdate = new System.Windows.Forms.Button();
            this.buttonMaterialsUpdate = new System.Windows.Forms.Button();
            this.buttonCulvertOpeningsDelete = new System.Windows.Forms.Button();
            this.buttonFacingsUpdate = new System.Windows.Forms.Button();
            this.buttonMaterialsDelete = new System.Windows.Forms.Button();
            this.buttonMaterialsAdd = new System.Windows.Forms.Button();
            this.buttonShapesDelete = new System.Windows.Forms.Button();
            this.buttonShapesAdd = new System.Windows.Forms.Button();
            this.buttonFacingsDelete = new System.Windows.Forms.Button();
            this.buttonFacingsAdd = new System.Windows.Forms.Button();
            this.buttonCulvertOpeningsUpdate = new System.Windows.Forms.Button();
            this.buttonCulvertOpeningsAdd = new System.Windows.Forms.Button();
            this.labelMaterials = new System.Windows.Forms.Label();
            this.labelShapes = new System.Windows.Forms.Label();
            this.dataGridViewMaterials = new System.Windows.Forms.DataGridView();
            this.materialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sWSPMATERIALTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewShapes = new System.Windows.Forms.DataGridView();
            this.shapeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sWSPSHAPETYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewFacings = new System.Windows.Forms.DataGridView();
            this.facingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sWSPFACINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewCulvertOpenings = new System.Windows.Forms.DataGridView();
            this.culvertopeningDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sWSPCULVERTOPENINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelFacings = new System.Windows.Forms.Label();
            this.labelCulvertOpenings = new System.Windows.Forms.Label();
            this.tabPageViewsAndSurveys = new System.Windows.Forms.TabPage();
            this.ultraButtonDeleteSelectedView = new Infragistics.Win.Misc.UltraButton();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.fKVIEWSUBWATERSHEDBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.fKSUBWATERSHEDWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewSurveyPages = new System.Windows.Forms.DataGridView();
            this.surveypageidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viewidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagenumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weatherDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fKSURVEYPAGEVIEWBindingSource5 = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxSubwatershed = new System.Windows.Forms.ComboBox();
            this.comboBoxWatershed = new System.Windows.Forms.ComboBox();
            this.buttonDeleteSelectedPage = new System.Windows.Forms.Button();
            this.buttonAddSurveyPage = new System.Windows.Forms.Button();
            this.buttonAddView = new System.Windows.Forms.Button();
            this.labelSubwatershed = new System.Windows.Forms.Label();
            this.labelWatershed = new System.Windows.Forms.Label();
            this.labelViews = new System.Windows.Forms.Label();
            this.fKSURVEYPAGEVIEWBindingSource4 = new System.Windows.Forms.BindingSource(this.components);
            this.fKVIEWSUBWATERSHEDBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.fKSURVEYPAGEVIEWBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fKVIEWSUBWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPVIEWBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPSURVEYPAGEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonOK = new System.Windows.Forms.Button();
            this.sWSP_WATERSHEDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_WATERSHEDTableAdapter();
            this.sWSP_SUBWATERSHEDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SUBWATERSHEDTableAdapter();
            this.sWSP_VIEWTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_VIEWTableAdapter();
            this.sWSP_EVALUATORTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_EVALUATORTableAdapter();
            this.sWSP_CULVERT_OPENING_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_CULVERT_OPENING_TYPETableAdapter();
            this.sWSP_FACING_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_FACING_TYPETableAdapter();
            this.sWSP_SHAPE_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SHAPE_TYPETableAdapter();
            this.sWSP_MATERIAL_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_MATERIAL_TYPETableAdapter();
            this.sWSP_SURVEY_PAGETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGETableAdapter();
            this.fKSURVEYPAGEVIEWBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.fKSURVEYPAGEVIEWBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.fKSURVEYPAGEVIEWBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.fKVIEWSUBWATERSHEDBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.tabControlFormSWSPFieldDataAdministration.SuspendLayout();
            this.tabPageWatersheds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSubwatersheds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWatersheds)).BeginInit();
            this.tabPageEvaluators.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvaluators)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPEVALUATORBindingSource)).BeginInit();
            this.tabPageTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPMATERIALTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShapes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSHAPETYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFacings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCulvertOpenings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTOPENINGTYPEBindingSource)).BeginInit();
            this.tabPageViewsAndSurveys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSurveyPages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPVIEWBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSURVEYPAGEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlFormSWSPFieldDataAdministration
            // 
            this.tabControlFormSWSPFieldDataAdministration.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlFormSWSPFieldDataAdministration.Controls.Add(this.tabPageWatersheds);
            this.tabControlFormSWSPFieldDataAdministration.Controls.Add(this.tabPageEvaluators);
            this.tabControlFormSWSPFieldDataAdministration.Controls.Add(this.tabPageTypes);
            this.tabControlFormSWSPFieldDataAdministration.Controls.Add(this.tabPageViewsAndSurveys);
            this.tabControlFormSWSPFieldDataAdministration.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlFormSWSPFieldDataAdministration.ItemSize = new System.Drawing.Size(25, 100);
            this.tabControlFormSWSPFieldDataAdministration.Location = new System.Drawing.Point(12, 12);
            this.tabControlFormSWSPFieldDataAdministration.Multiline = true;
            this.tabControlFormSWSPFieldDataAdministration.Name = "tabControlFormSWSPFieldDataAdministration";
            this.tabControlFormSWSPFieldDataAdministration.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControlFormSWSPFieldDataAdministration.SelectedIndex = 0;
            this.tabControlFormSWSPFieldDataAdministration.Size = new System.Drawing.Size(703, 385);
            this.tabControlFormSWSPFieldDataAdministration.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlFormSWSPFieldDataAdministration.TabIndex = 0;
            this.tabControlFormSWSPFieldDataAdministration.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPageWatersheds
            // 
            this.tabPageWatersheds.Controls.Add(this.buttonSubwatershedsUpdate);
            this.tabPageWatersheds.Controls.Add(this.buttonWatershedsUpdate);
            this.tabPageWatersheds.Controls.Add(this.buttonSubwatershedsDelete);
            this.tabPageWatersheds.Controls.Add(this.buttonSubwatershedsAdd);
            this.tabPageWatersheds.Controls.Add(this.buttonWatershedsDelete);
            this.tabPageWatersheds.Controls.Add(this.buttonWatershedsAdd);
            this.tabPageWatersheds.Controls.Add(this.dataGridViewSubwatersheds);
            this.tabPageWatersheds.Controls.Add(this.dataGridViewWatersheds);
            this.tabPageWatersheds.Controls.Add(this.labelSubwatersheds);
            this.tabPageWatersheds.Controls.Add(this.labelWatersheds);
            this.tabPageWatersheds.Location = new System.Drawing.Point(104, 4);
            this.tabPageWatersheds.Name = "tabPageWatersheds";
            this.tabPageWatersheds.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWatersheds.Size = new System.Drawing.Size(595, 377);
            this.tabPageWatersheds.TabIndex = 0;
            this.tabPageWatersheds.Text = "Watersheds";
            this.tabPageWatersheds.UseVisualStyleBackColor = true;
            // 
            // buttonSubwatershedsUpdate
            // 
            this.buttonSubwatershedsUpdate.Location = new System.Drawing.Point(390, 330);
            this.buttonSubwatershedsUpdate.Name = "buttonSubwatershedsUpdate";
            this.buttonSubwatershedsUpdate.Size = new System.Drawing.Size(70, 24);
            this.buttonSubwatershedsUpdate.TabIndex = 9;
            this.buttonSubwatershedsUpdate.Text = "Update";
            this.buttonSubwatershedsUpdate.UseVisualStyleBackColor = true;
            this.buttonSubwatershedsUpdate.Click += new System.EventHandler(this.buttonSubwatershedsUpdate_Click);
            // 
            // buttonWatershedsUpdate
            // 
            this.buttonWatershedsUpdate.Location = new System.Drawing.Point(119, 330);
            this.buttonWatershedsUpdate.Name = "buttonWatershedsUpdate";
            this.buttonWatershedsUpdate.Size = new System.Drawing.Size(70, 24);
            this.buttonWatershedsUpdate.TabIndex = 8;
            this.buttonWatershedsUpdate.Text = "Update";
            this.buttonWatershedsUpdate.UseVisualStyleBackColor = true;
            this.buttonWatershedsUpdate.Click += new System.EventHandler(this.buttonWatershedsUpdate_Click);
            // 
            // buttonSubwatershedsDelete
            // 
            this.buttonSubwatershedsDelete.Location = new System.Drawing.Point(466, 330);
            this.buttonSubwatershedsDelete.Name = "buttonSubwatershedsDelete";
            this.buttonSubwatershedsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonSubwatershedsDelete.TabIndex = 7;
            this.buttonSubwatershedsDelete.Text = "Delete";
            this.buttonSubwatershedsDelete.UseVisualStyleBackColor = true;
            this.buttonSubwatershedsDelete.Click += new System.EventHandler(this.buttonSubwatershedsDelete_Click);
            // 
            // buttonSubwatershedsAdd
            // 
            this.buttonSubwatershedsAdd.Location = new System.Drawing.Point(314, 330);
            this.buttonSubwatershedsAdd.Name = "buttonSubwatershedsAdd";
            this.buttonSubwatershedsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonSubwatershedsAdd.TabIndex = 6;
            this.buttonSubwatershedsAdd.Text = "Add";
            this.buttonSubwatershedsAdd.UseVisualStyleBackColor = true;
            this.buttonSubwatershedsAdd.Click += new System.EventHandler(this.buttonSubwatershedsAdd_Click);
            // 
            // buttonWatershedsDelete
            // 
            this.buttonWatershedsDelete.Location = new System.Drawing.Point(195, 330);
            this.buttonWatershedsDelete.Name = "buttonWatershedsDelete";
            this.buttonWatershedsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonWatershedsDelete.TabIndex = 5;
            this.buttonWatershedsDelete.Text = "Delete";
            this.buttonWatershedsDelete.UseVisualStyleBackColor = true;
            this.buttonWatershedsDelete.Click += new System.EventHandler(this.buttonWatershedsDelete_Click);
            // 
            // buttonWatershedsAdd
            // 
            this.buttonWatershedsAdd.Location = new System.Drawing.Point(43, 330);
            this.buttonWatershedsAdd.Name = "buttonWatershedsAdd";
            this.buttonWatershedsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonWatershedsAdd.TabIndex = 4;
            this.buttonWatershedsAdd.Text = "Add";
            this.buttonWatershedsAdd.UseVisualStyleBackColor = true;
            this.buttonWatershedsAdd.Click += new System.EventHandler(this.buttonWatershedsAdd_Click);
            // 
            // dataGridViewSubwatersheds
            // 
            this.dataGridViewSubwatersheds.AllowUserToAddRows = false;
            this.dataGridViewSubwatersheds.AllowUserToDeleteRows = false;
            this.dataGridViewSubwatersheds.AutoGenerateColumns = false;
            this.dataGridViewSubwatersheds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSubwatersheds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.subwatershed,
            this.descriptionDataGridViewTextBoxColumn1});
            this.dataGridViewSubwatersheds.DataSource = this.fKSUBWATERSHEDWATERSHEDBindingSource1;
            this.dataGridViewSubwatersheds.Location = new System.Drawing.Point(288, 40);
            this.dataGridViewSubwatersheds.Name = "dataGridViewSubwatersheds";
            this.dataGridViewSubwatersheds.Size = new System.Drawing.Size(269, 284);
            this.dataGridViewSubwatersheds.TabIndex = 3;
            // 
            // subwatershed
            // 
            this.subwatershed.DataPropertyName = "subwatershed";
            this.subwatershed.HeaderText = "subwatershed";
            this.subwatershed.Name = "subwatershed";
            // 
            // descriptionDataGridViewTextBoxColumn1
            // 
            this.descriptionDataGridViewTextBoxColumn1.DataPropertyName = "description";
            this.descriptionDataGridViewTextBoxColumn1.HeaderText = "description";
            this.descriptionDataGridViewTextBoxColumn1.Name = "descriptionDataGridViewTextBoxColumn1";
            // 
            // fKSUBWATERSHEDWATERSHEDBindingSource1
            // 
            this.fKSUBWATERSHEDWATERSHEDBindingSource1.DataMember = "FK_SUBWATERSHED_WATERSHED";
            this.fKSUBWATERSHEDWATERSHEDBindingSource1.DataSource = this.sWSPWATERSHEDBindingSource;
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
            // dataGridViewWatersheds
            // 
            this.dataGridViewWatersheds.AllowUserToAddRows = false;
            this.dataGridViewWatersheds.AllowUserToDeleteRows = false;
            this.dataGridViewWatersheds.AutoGenerateColumns = false;
            this.dataGridViewWatersheds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWatersheds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.watershedDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
            this.dataGridViewWatersheds.DataSource = this.sWSPWATERSHEDBindingSource;
            this.dataGridViewWatersheds.Location = new System.Drawing.Point(22, 40);
            this.dataGridViewWatersheds.Name = "dataGridViewWatersheds";
            this.dataGridViewWatersheds.Size = new System.Drawing.Size(260, 284);
            this.dataGridViewWatersheds.TabIndex = 2;
            // 
            // watershedDataGridViewTextBoxColumn
            // 
            this.watershedDataGridViewTextBoxColumn.DataPropertyName = "watershed";
            this.watershedDataGridViewTextBoxColumn.HeaderText = "watershed";
            this.watershedDataGridViewTextBoxColumn.Name = "watershedDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // labelSubwatersheds
            // 
            this.labelSubwatersheds.AutoSize = true;
            this.labelSubwatersheds.Location = new System.Drawing.Point(327, 12);
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
            this.tabPageEvaluators.Controls.Add(this.buttonEvaluatorsUpdate);
            this.tabPageEvaluators.Controls.Add(this.buttonEvaluatorsDelete);
            this.tabPageEvaluators.Controls.Add(this.buttonEvaluatorsAdd);
            this.tabPageEvaluators.Controls.Add(this.dataGridViewEvaluators);
            this.tabPageEvaluators.Controls.Add(this.labelEvaluators);
            this.tabPageEvaluators.Location = new System.Drawing.Point(104, 4);
            this.tabPageEvaluators.Name = "tabPageEvaluators";
            this.tabPageEvaluators.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEvaluators.Size = new System.Drawing.Size(595, 377);
            this.tabPageEvaluators.TabIndex = 1;
            this.tabPageEvaluators.Text = "Evaluators";
            this.tabPageEvaluators.UseVisualStyleBackColor = true;
            // 
            // buttonEvaluatorsUpdate
            // 
            this.buttonEvaluatorsUpdate.Location = new System.Drawing.Point(384, 347);
            this.buttonEvaluatorsUpdate.Name = "buttonEvaluatorsUpdate";
            this.buttonEvaluatorsUpdate.Size = new System.Drawing.Size(70, 24);
            this.buttonEvaluatorsUpdate.TabIndex = 10;
            this.buttonEvaluatorsUpdate.Text = "Update";
            this.buttonEvaluatorsUpdate.UseVisualStyleBackColor = true;
            this.buttonEvaluatorsUpdate.Click += new System.EventHandler(this.buttonEvaluatorsUpdate_Click);
            // 
            // buttonEvaluatorsDelete
            // 
            this.buttonEvaluatorsDelete.Location = new System.Drawing.Point(460, 347);
            this.buttonEvaluatorsDelete.Name = "buttonEvaluatorsDelete";
            this.buttonEvaluatorsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonEvaluatorsDelete.TabIndex = 9;
            this.buttonEvaluatorsDelete.Text = "Delete";
            this.buttonEvaluatorsDelete.UseVisualStyleBackColor = true;
            this.buttonEvaluatorsDelete.Click += new System.EventHandler(this.buttonEvaluatorsDelete_Click);
            // 
            // buttonEvaluatorsAdd
            // 
            this.buttonEvaluatorsAdd.Location = new System.Drawing.Point(308, 347);
            this.buttonEvaluatorsAdd.Name = "buttonEvaluatorsAdd";
            this.buttonEvaluatorsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonEvaluatorsAdd.TabIndex = 8;
            this.buttonEvaluatorsAdd.Text = "Add";
            this.buttonEvaluatorsAdd.UseVisualStyleBackColor = true;
            this.buttonEvaluatorsAdd.Click += new System.EventHandler(this.buttonEvaluatorsAdd_Click);
            // 
            // dataGridViewEvaluators
            // 
            this.dataGridViewEvaluators.AllowUserToAddRows = false;
            this.dataGridViewEvaluators.AllowUserToDeleteRows = false;
            this.dataGridViewEvaluators.AutoGenerateColumns = false;
            this.dataGridViewEvaluators.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEvaluators.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.initialsDataGridViewTextBoxColumn,
            this.lastnameDataGridViewTextBoxColumn,
            this.firstnameDataGridViewTextBoxColumn});
            this.dataGridViewEvaluators.DataSource = this.sWSPEVALUATORBindingSource;
            this.dataGridViewEvaluators.Location = new System.Drawing.Point(14, 39);
            this.dataGridViewEvaluators.Name = "dataGridViewEvaluators";
            this.dataGridViewEvaluators.Size = new System.Drawing.Size(516, 302);
            this.dataGridViewEvaluators.TabIndex = 1;
            // 
            // initialsDataGridViewTextBoxColumn
            // 
            this.initialsDataGridViewTextBoxColumn.DataPropertyName = "initials";
            this.initialsDataGridViewTextBoxColumn.HeaderText = "initials";
            this.initialsDataGridViewTextBoxColumn.Name = "initialsDataGridViewTextBoxColumn";
            // 
            // lastnameDataGridViewTextBoxColumn
            // 
            this.lastnameDataGridViewTextBoxColumn.DataPropertyName = "last_name";
            this.lastnameDataGridViewTextBoxColumn.HeaderText = "last_name";
            this.lastnameDataGridViewTextBoxColumn.Name = "lastnameDataGridViewTextBoxColumn";
            // 
            // firstnameDataGridViewTextBoxColumn
            // 
            this.firstnameDataGridViewTextBoxColumn.DataPropertyName = "first_name";
            this.firstnameDataGridViewTextBoxColumn.HeaderText = "first_name";
            this.firstnameDataGridViewTextBoxColumn.Name = "firstnameDataGridViewTextBoxColumn";
            // 
            // sWSPEVALUATORBindingSource
            // 
            this.sWSPEVALUATORBindingSource.DataMember = "SWSP_EVALUATOR";
            this.sWSPEVALUATORBindingSource.DataSource = this.sANDBOXDataSet;
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
            this.tabPageTypes.Controls.Add(this.buttonShapesUpdate);
            this.tabPageTypes.Controls.Add(this.buttonMaterialsUpdate);
            this.tabPageTypes.Controls.Add(this.buttonCulvertOpeningsDelete);
            this.tabPageTypes.Controls.Add(this.buttonFacingsUpdate);
            this.tabPageTypes.Controls.Add(this.buttonMaterialsDelete);
            this.tabPageTypes.Controls.Add(this.buttonMaterialsAdd);
            this.tabPageTypes.Controls.Add(this.buttonShapesDelete);
            this.tabPageTypes.Controls.Add(this.buttonShapesAdd);
            this.tabPageTypes.Controls.Add(this.buttonFacingsDelete);
            this.tabPageTypes.Controls.Add(this.buttonFacingsAdd);
            this.tabPageTypes.Controls.Add(this.buttonCulvertOpeningsUpdate);
            this.tabPageTypes.Controls.Add(this.buttonCulvertOpeningsAdd);
            this.tabPageTypes.Controls.Add(this.labelMaterials);
            this.tabPageTypes.Controls.Add(this.labelShapes);
            this.tabPageTypes.Controls.Add(this.dataGridViewMaterials);
            this.tabPageTypes.Controls.Add(this.dataGridViewShapes);
            this.tabPageTypes.Controls.Add(this.dataGridViewFacings);
            this.tabPageTypes.Controls.Add(this.dataGridViewCulvertOpenings);
            this.tabPageTypes.Controls.Add(this.labelFacings);
            this.tabPageTypes.Controls.Add(this.labelCulvertOpenings);
            this.tabPageTypes.Location = new System.Drawing.Point(104, 4);
            this.tabPageTypes.Name = "tabPageTypes";
            this.tabPageTypes.Size = new System.Drawing.Size(595, 377);
            this.tabPageTypes.TabIndex = 2;
            this.tabPageTypes.Text = "Types";
            this.tabPageTypes.UseVisualStyleBackColor = true;
            // 
            // buttonShapesUpdate
            // 
            this.buttonShapesUpdate.Location = new System.Drawing.Point(127, 349);
            this.buttonShapesUpdate.Name = "buttonShapesUpdate";
            this.buttonShapesUpdate.Size = new System.Drawing.Size(70, 24);
            this.buttonShapesUpdate.TabIndex = 21;
            this.buttonShapesUpdate.Text = "Update";
            this.buttonShapesUpdate.UseVisualStyleBackColor = true;
            this.buttonShapesUpdate.Click += new System.EventHandler(this.buttonShapesUpdate_Click);
            // 
            // buttonMaterialsUpdate
            // 
            this.buttonMaterialsUpdate.Location = new System.Drawing.Point(426, 350);
            this.buttonMaterialsUpdate.Name = "buttonMaterialsUpdate";
            this.buttonMaterialsUpdate.Size = new System.Drawing.Size(70, 24);
            this.buttonMaterialsUpdate.TabIndex = 20;
            this.buttonMaterialsUpdate.Text = "Update";
            this.buttonMaterialsUpdate.UseVisualStyleBackColor = true;
            this.buttonMaterialsUpdate.Click += new System.EventHandler(this.buttonMaterialsUpdate_Click);
            // 
            // buttonCulvertOpeningsDelete
            // 
            this.buttonCulvertOpeningsDelete.Location = new System.Drawing.Point(240, 145);
            this.buttonCulvertOpeningsDelete.Name = "buttonCulvertOpeningsDelete";
            this.buttonCulvertOpeningsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonCulvertOpeningsDelete.TabIndex = 19;
            this.buttonCulvertOpeningsDelete.Text = "Delete";
            this.buttonCulvertOpeningsDelete.UseVisualStyleBackColor = true;
            this.buttonCulvertOpeningsDelete.Click += new System.EventHandler(this.buttonCulvertOpeningsDelete_Click);
            // 
            // buttonFacingsUpdate
            // 
            this.buttonFacingsUpdate.Location = new System.Drawing.Point(426, 145);
            this.buttonFacingsUpdate.Name = "buttonFacingsUpdate";
            this.buttonFacingsUpdate.Size = new System.Drawing.Size(70, 24);
            this.buttonFacingsUpdate.TabIndex = 18;
            this.buttonFacingsUpdate.Text = "Update";
            this.buttonFacingsUpdate.UseVisualStyleBackColor = true;
            this.buttonFacingsUpdate.Click += new System.EventHandler(this.buttonFacingsUpdate_Click);
            // 
            // buttonMaterialsDelete
            // 
            this.buttonMaterialsDelete.Location = new System.Drawing.Point(522, 349);
            this.buttonMaterialsDelete.Name = "buttonMaterialsDelete";
            this.buttonMaterialsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonMaterialsDelete.TabIndex = 17;
            this.buttonMaterialsDelete.Text = "Delete";
            this.buttonMaterialsDelete.UseVisualStyleBackColor = true;
            this.buttonMaterialsDelete.Click += new System.EventHandler(this.buttonMaterialsDelete_Click);
            // 
            // buttonMaterialsAdd
            // 
            this.buttonMaterialsAdd.Location = new System.Drawing.Point(316, 350);
            this.buttonMaterialsAdd.Name = "buttonMaterialsAdd";
            this.buttonMaterialsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonMaterialsAdd.TabIndex = 16;
            this.buttonMaterialsAdd.Text = "Add";
            this.buttonMaterialsAdd.UseVisualStyleBackColor = true;
            this.buttonMaterialsAdd.Click += new System.EventHandler(this.buttonMaterialsAdd_Click);
            // 
            // buttonShapesDelete
            // 
            this.buttonShapesDelete.Location = new System.Drawing.Point(240, 350);
            this.buttonShapesDelete.Name = "buttonShapesDelete";
            this.buttonShapesDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonShapesDelete.TabIndex = 15;
            this.buttonShapesDelete.Text = "Delete";
            this.buttonShapesDelete.UseVisualStyleBackColor = true;
            this.buttonShapesDelete.Click += new System.EventHandler(this.buttonShapesDelete_Click);
            // 
            // buttonShapesAdd
            // 
            this.buttonShapesAdd.Location = new System.Drawing.Point(18, 350);
            this.buttonShapesAdd.Name = "buttonShapesAdd";
            this.buttonShapesAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonShapesAdd.TabIndex = 14;
            this.buttonShapesAdd.Text = "Add";
            this.buttonShapesAdd.UseVisualStyleBackColor = true;
            this.buttonShapesAdd.Click += new System.EventHandler(this.buttonShapesAdd_Click);
            // 
            // buttonFacingsDelete
            // 
            this.buttonFacingsDelete.Location = new System.Drawing.Point(522, 144);
            this.buttonFacingsDelete.Name = "buttonFacingsDelete";
            this.buttonFacingsDelete.Size = new System.Drawing.Size(70, 24);
            this.buttonFacingsDelete.TabIndex = 13;
            this.buttonFacingsDelete.Text = "Delete";
            this.buttonFacingsDelete.UseVisualStyleBackColor = true;
            this.buttonFacingsDelete.Click += new System.EventHandler(this.buttonFacingsDelete_Click);
            // 
            // buttonFacingsAdd
            // 
            this.buttonFacingsAdd.Location = new System.Drawing.Point(316, 145);
            this.buttonFacingsAdd.Name = "buttonFacingsAdd";
            this.buttonFacingsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonFacingsAdd.TabIndex = 12;
            this.buttonFacingsAdd.Text = "Add";
            this.buttonFacingsAdd.UseVisualStyleBackColor = true;
            this.buttonFacingsAdd.Click += new System.EventHandler(this.buttonFacingsAdd_Click);
            // 
            // buttonCulvertOpeningsUpdate
            // 
            this.buttonCulvertOpeningsUpdate.Location = new System.Drawing.Point(127, 145);
            this.buttonCulvertOpeningsUpdate.Name = "buttonCulvertOpeningsUpdate";
            this.buttonCulvertOpeningsUpdate.Size = new System.Drawing.Size(70, 24);
            this.buttonCulvertOpeningsUpdate.TabIndex = 11;
            this.buttonCulvertOpeningsUpdate.Text = "Update";
            this.buttonCulvertOpeningsUpdate.UseVisualStyleBackColor = true;
            this.buttonCulvertOpeningsUpdate.Click += new System.EventHandler(this.buttonCulvertOpeningsUpdate_Click);
            // 
            // buttonCulvertOpeningsAdd
            // 
            this.buttonCulvertOpeningsAdd.Location = new System.Drawing.Point(16, 145);
            this.buttonCulvertOpeningsAdd.Name = "buttonCulvertOpeningsAdd";
            this.buttonCulvertOpeningsAdd.Size = new System.Drawing.Size(70, 24);
            this.buttonCulvertOpeningsAdd.TabIndex = 10;
            this.buttonCulvertOpeningsAdd.Text = "Add";
            this.buttonCulvertOpeningsAdd.UseVisualStyleBackColor = true;
            this.buttonCulvertOpeningsAdd.Click += new System.EventHandler(this.buttonCulvertOpeningsAdd_Click);
            // 
            // labelMaterials
            // 
            this.labelMaterials.AutoSize = true;
            this.labelMaterials.Location = new System.Drawing.Point(315, 192);
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
            // dataGridViewMaterials
            // 
            this.dataGridViewMaterials.AllowUserToAddRows = false;
            this.dataGridViewMaterials.AllowUserToDeleteRows = false;
            this.dataGridViewMaterials.AutoGenerateColumns = false;
            this.dataGridViewMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMaterials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.materialDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn5});
            this.dataGridViewMaterials.DataSource = this.sWSPMATERIALTYPEBindingSource;
            this.dataGridViewMaterials.Location = new System.Drawing.Point(316, 223);
            this.dataGridViewMaterials.Name = "dataGridViewMaterials";
            this.dataGridViewMaterials.Size = new System.Drawing.Size(276, 120);
            this.dataGridViewMaterials.TabIndex = 5;
            // 
            // materialDataGridViewTextBoxColumn
            // 
            this.materialDataGridViewTextBoxColumn.DataPropertyName = "material";
            this.materialDataGridViewTextBoxColumn.HeaderText = "Material";
            this.materialDataGridViewTextBoxColumn.Name = "materialDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn5
            // 
            this.descriptionDataGridViewTextBoxColumn5.DataPropertyName = "description";
            this.descriptionDataGridViewTextBoxColumn5.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn5.Name = "descriptionDataGridViewTextBoxColumn5";
            // 
            // sWSPMATERIALTYPEBindingSource
            // 
            this.sWSPMATERIALTYPEBindingSource.DataMember = "SWSP_MATERIAL_TYPE";
            this.sWSPMATERIALTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // dataGridViewShapes
            // 
            this.dataGridViewShapes.AllowUserToAddRows = false;
            this.dataGridViewShapes.AllowUserToDeleteRows = false;
            this.dataGridViewShapes.AutoGenerateColumns = false;
            this.dataGridViewShapes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShapes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.shapeDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn4});
            this.dataGridViewShapes.DataSource = this.sWSPSHAPETYPEBindingSource;
            this.dataGridViewShapes.Location = new System.Drawing.Point(18, 225);
            this.dataGridViewShapes.Name = "dataGridViewShapes";
            this.dataGridViewShapes.Size = new System.Drawing.Size(292, 119);
            this.dataGridViewShapes.TabIndex = 4;
            // 
            // shapeDataGridViewTextBoxColumn
            // 
            this.shapeDataGridViewTextBoxColumn.DataPropertyName = "shape";
            this.shapeDataGridViewTextBoxColumn.HeaderText = "Shape";
            this.shapeDataGridViewTextBoxColumn.Name = "shapeDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn4
            // 
            this.descriptionDataGridViewTextBoxColumn4.DataPropertyName = "description";
            this.descriptionDataGridViewTextBoxColumn4.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn4.Name = "descriptionDataGridViewTextBoxColumn4";
            // 
            // sWSPSHAPETYPEBindingSource
            // 
            this.sWSPSHAPETYPEBindingSource.DataMember = "SWSP_SHAPE_TYPE";
            this.sWSPSHAPETYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // dataGridViewFacings
            // 
            this.dataGridViewFacings.AllowUserToAddRows = false;
            this.dataGridViewFacings.AllowUserToDeleteRows = false;
            this.dataGridViewFacings.AutoGenerateColumns = false;
            this.dataGridViewFacings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFacings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.facingDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn3});
            this.dataGridViewFacings.DataSource = this.sWSPFACINGTYPEBindingSource;
            this.dataGridViewFacings.Location = new System.Drawing.Point(316, 32);
            this.dataGridViewFacings.Name = "dataGridViewFacings";
            this.dataGridViewFacings.Size = new System.Drawing.Size(276, 106);
            this.dataGridViewFacings.TabIndex = 3;
            // 
            // facingDataGridViewTextBoxColumn
            // 
            this.facingDataGridViewTextBoxColumn.DataPropertyName = "facing";
            this.facingDataGridViewTextBoxColumn.HeaderText = "Facing Type";
            this.facingDataGridViewTextBoxColumn.Name = "facingDataGridViewTextBoxColumn";
            this.facingDataGridViewTextBoxColumn.Width = 90;
            // 
            // descriptionDataGridViewTextBoxColumn3
            // 
            this.descriptionDataGridViewTextBoxColumn3.DataPropertyName = "description";
            this.descriptionDataGridViewTextBoxColumn3.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn3.Name = "descriptionDataGridViewTextBoxColumn3";
            this.descriptionDataGridViewTextBoxColumn3.Width = 90;
            // 
            // sWSPFACINGTYPEBindingSource
            // 
            this.sWSPFACINGTYPEBindingSource.DataMember = "SWSP_FACING_TYPE";
            this.sWSPFACINGTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // dataGridViewCulvertOpenings
            // 
            this.dataGridViewCulvertOpenings.AllowUserToAddRows = false;
            this.dataGridViewCulvertOpenings.AllowUserToDeleteRows = false;
            this.dataGridViewCulvertOpenings.AutoGenerateColumns = false;
            this.dataGridViewCulvertOpenings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCulvertOpenings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.culvertopeningDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn2});
            this.dataGridViewCulvertOpenings.DataSource = this.sWSPCULVERTOPENINGTYPEBindingSource;
            this.dataGridViewCulvertOpenings.Location = new System.Drawing.Point(18, 32);
            this.dataGridViewCulvertOpenings.Name = "dataGridViewCulvertOpenings";
            this.dataGridViewCulvertOpenings.Size = new System.Drawing.Size(292, 107);
            this.dataGridViewCulvertOpenings.TabIndex = 2;
            // 
            // culvertopeningDataGridViewTextBoxColumn
            // 
            this.culvertopeningDataGridViewTextBoxColumn.DataPropertyName = "culvert_opening";
            this.culvertopeningDataGridViewTextBoxColumn.HeaderText = "Opening Type";
            this.culvertopeningDataGridViewTextBoxColumn.Name = "culvertopeningDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn2
            // 
            this.descriptionDataGridViewTextBoxColumn2.DataPropertyName = "description";
            this.descriptionDataGridViewTextBoxColumn2.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn2.Name = "descriptionDataGridViewTextBoxColumn2";
            // 
            // sWSPCULVERTOPENINGTYPEBindingSource
            // 
            this.sWSPCULVERTOPENINGTYPEBindingSource.DataMember = "SWSP_CULVERT_OPENING_TYPE";
            this.sWSPCULVERTOPENINGTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // labelFacings
            // 
            this.labelFacings.AutoSize = true;
            this.labelFacings.Location = new System.Drawing.Point(310, 14);
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
            this.tabPageViewsAndSurveys.Controls.Add(this.ultraButtonDeleteSelectedView);
            this.tabPageViewsAndSurveys.Controls.Add(this.ultraGrid1);
            this.tabPageViewsAndSurveys.Controls.Add(this.dataGridViewSurveyPages);
            this.tabPageViewsAndSurveys.Controls.Add(this.comboBoxSubwatershed);
            this.tabPageViewsAndSurveys.Controls.Add(this.comboBoxWatershed);
            this.tabPageViewsAndSurveys.Controls.Add(this.buttonDeleteSelectedPage);
            this.tabPageViewsAndSurveys.Controls.Add(this.buttonAddSurveyPage);
            this.tabPageViewsAndSurveys.Controls.Add(this.buttonAddView);
            this.tabPageViewsAndSurveys.Controls.Add(this.labelSubwatershed);
            this.tabPageViewsAndSurveys.Controls.Add(this.labelWatershed);
            this.tabPageViewsAndSurveys.Controls.Add(this.labelViews);
            this.tabPageViewsAndSurveys.Location = new System.Drawing.Point(104, 4);
            this.tabPageViewsAndSurveys.Name = "tabPageViewsAndSurveys";
            this.tabPageViewsAndSurveys.Size = new System.Drawing.Size(595, 377);
            this.tabPageViewsAndSurveys.TabIndex = 3;
            this.tabPageViewsAndSurveys.Text = "Views and Surveys";
            this.tabPageViewsAndSurveys.UseVisualStyleBackColor = true;
            // 
            // ultraButtonDeleteSelectedView
            // 
            this.ultraButtonDeleteSelectedView.Location = new System.Drawing.Point(166, 335);
            this.ultraButtonDeleteSelectedView.Name = "ultraButtonDeleteSelectedView";
            this.ultraButtonDeleteSelectedView.Size = new System.Drawing.Size(127, 26);
            this.ultraButtonDeleteSelectedView.TabIndex = 17;
            this.ultraButtonDeleteSelectedView.Text = "Delete Selected View";
            this.ultraButtonDeleteSelectedView.Click += new System.EventHandler(this.ultraButtonDeleteSelectedView_Click);
            // 
            // ultraGrid1
            // 
            this.ultraGrid1.DataSource = this.fKVIEWSUBWATERSHEDBindingSource3;
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 98;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 100;
            ultraGridColumn4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5});
            ultraGridBand1.Expandable = false;
            ultraGridBand1.Indentation = 0;
            ultraGridColumn6.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn6.Header.VisiblePosition = 0;
            ultraGridColumn6.Width = 98;
            ultraGridColumn7.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn7.Header.VisiblePosition = 1;
            ultraGridColumn8.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn8.Header.VisiblePosition = 2;
            ultraGridColumn8.Width = 100;
            ultraGridColumn9.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn9.Header.VisiblePosition = 3;
            ultraGridColumn10.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn10.Header.VisiblePosition = 4;
            ultraGridColumn11.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn11.Header.VisiblePosition = 5;
            ultraGridColumn12.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn12.Header.VisiblePosition = 6;
            ultraGridColumn13.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn13.Header.VisiblePosition = 7;
            ultraGridColumn14.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn14.Header.VisiblePosition = 8;
            ultraGridColumn15.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn15.Header.VisiblePosition = 9;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15});
            ultraGridBand2.Expandable = false;
            ultraGridBand2.Indentation = 0;
            ultraGridColumn16.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn16.Header.VisiblePosition = 0;
            ultraGridColumn16.Width = 79;
            ultraGridColumn17.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn17.Header.VisiblePosition = 1;
            ultraGridColumn18.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn18.Header.VisiblePosition = 2;
            ultraGridColumn18.Width = 100;
            ultraGridColumn19.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn19.Header.VisiblePosition = 3;
            ultraGridColumn20.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn20.Header.VisiblePosition = 4;
            ultraGridColumn21.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn21.Header.VisiblePosition = 5;
            ultraGridColumn22.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn22.Header.VisiblePosition = 6;
            ultraGridColumn23.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn23.Header.VisiblePosition = 7;
            ultraGridColumn24.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn24.Header.VisiblePosition = 8;
            ultraGridColumn25.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn25.Header.VisiblePosition = 9;
            ultraGridColumn26.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn26.Header.VisiblePosition = 10;
            ultraGridColumn27.Header.VisiblePosition = 11;
            ultraGridColumn28.Header.VisiblePosition = 12;
            ultraGridColumn29.Header.VisiblePosition = 13;
            ultraGridColumn30.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn30.Header.VisiblePosition = 14;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30});
            ultraGridColumn31.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn31.Header.VisiblePosition = 0;
            ultraGridColumn31.Width = 79;
            ultraGridColumn32.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn32.Header.VisiblePosition = 1;
            ultraGridColumn33.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn33.Header.VisiblePosition = 2;
            ultraGridColumn33.Width = 100;
            ultraGridColumn34.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn34.Header.VisiblePosition = 3;
            ultraGridColumn35.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn35.Header.VisiblePosition = 4;
            ultraGridColumn36.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn36.Header.VisiblePosition = 5;
            ultraGridColumn37.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn37.Header.VisiblePosition = 6;
            ultraGridColumn38.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn38.Header.VisiblePosition = 7;
            ultraGridColumn39.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn39.Header.VisiblePosition = 8;
            ultraGridColumn40.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn40.Header.VisiblePosition = 9;
            ultraGridColumn41.Header.VisiblePosition = 10;
            ultraGridColumn42.Header.VisiblePosition = 11;
            ultraGridColumn43.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn43.Header.VisiblePosition = 12;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn43});
            ultraGridColumn44.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn44.Header.VisiblePosition = 0;
            ultraGridColumn44.Width = 79;
            ultraGridColumn45.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn45.Header.VisiblePosition = 1;
            ultraGridColumn46.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn46.Header.VisiblePosition = 2;
            ultraGridColumn46.Width = 100;
            ultraGridColumn47.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn47.Header.VisiblePosition = 3;
            ultraGridColumn48.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn48.Header.VisiblePosition = 4;
            ultraGridColumn49.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn49.Header.VisiblePosition = 5;
            ultraGridColumn50.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn50.Header.VisiblePosition = 6;
            ultraGridColumn51.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn51.Header.VisiblePosition = 7;
            ultraGridColumn52.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn52.Header.VisiblePosition = 8;
            ultraGridColumn53.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn53.Header.VisiblePosition = 9;
            ultraGridColumn54.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn54.Header.VisiblePosition = 10;
            ultraGridColumn55.Header.VisiblePosition = 11;
            ultraGridColumn56.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn56.Header.VisiblePosition = 12;
            ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49,
            ultraGridColumn50,
            ultraGridColumn51,
            ultraGridColumn52,
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56});
            ultraGridColumn57.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn57.Header.VisiblePosition = 0;
            ultraGridColumn57.Width = 79;
            ultraGridColumn58.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn58.Header.VisiblePosition = 1;
            ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn57,
            ultraGridColumn58});
            ultraGridColumn59.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn59.Header.VisiblePosition = 0;
            ultraGridColumn59.Width = 60;
            ultraGridColumn60.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn60.Header.VisiblePosition = 1;
            ultraGridColumn61.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn61.Header.VisiblePosition = 2;
            ultraGridColumn61.Width = 100;
            ultraGridColumn62.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn62.Header.VisiblePosition = 3;
            ultraGridBand7.Columns.AddRange(new object[] {
            ultraGridColumn59,
            ultraGridColumn60,
            ultraGridColumn61,
            ultraGridColumn62});
            ultraGridColumn63.Header.VisiblePosition = 0;
            ultraGridColumn64.Header.VisiblePosition = 1;
            ultraGridColumn65.Header.VisiblePosition = 2;
            ultraGridColumn66.Header.VisiblePosition = 3;
            ultraGridBand8.Columns.AddRange(new object[] {
            ultraGridColumn63,
            ultraGridColumn64,
            ultraGridColumn65,
            ultraGridColumn66});
            ultraGridColumn67.Header.VisiblePosition = 0;
            ultraGridColumn68.Header.VisiblePosition = 1;
            ultraGridColumn69.Header.VisiblePosition = 2;
            ultraGridColumn70.Header.VisiblePosition = 3;
            ultraGridBand9.Columns.AddRange(new object[] {
            ultraGridColumn67,
            ultraGridColumn68,
            ultraGridColumn69,
            ultraGridColumn70});
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand7);
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand8);
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand9);
            this.ultraGrid1.DisplayLayout.GroupByBox.Hidden = true;
            this.ultraGrid1.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid1.DisplayLayout.MaxRowScrollRegions = 1;
            this.ultraGrid1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid1.Location = new System.Drawing.Point(12, 72);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(278, 257);
            this.ultraGrid1.TabIndex = 16;
            // 
            // fKVIEWSUBWATERSHEDBindingSource3
            // 
            this.fKVIEWSUBWATERSHEDBindingSource3.DataMember = "FK_VIEW_SUBWATERSHED";
            this.fKVIEWSUBWATERSHEDBindingSource3.DataSource = this.fKSUBWATERSHEDWATERSHEDBindingSource;
            // 
            // fKSUBWATERSHEDWATERSHEDBindingSource
            // 
            this.fKSUBWATERSHEDWATERSHEDBindingSource.DataMember = "FK_SUBWATERSHED_WATERSHED";
            this.fKSUBWATERSHEDWATERSHEDBindingSource.DataSource = this.sWSPWATERSHEDBindingSource;
            // 
            // dataGridViewSurveyPages
            // 
            this.dataGridViewSurveyPages.AutoGenerateColumns = false;
            this.dataGridViewSurveyPages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSurveyPages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.surveypageidDataGridViewTextBoxColumn,
            this.viewidDataGridViewTextBoxColumn,
            this.pagenumberDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.weatherDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.dataGridViewSurveyPages.DataSource = this.fKSURVEYPAGEVIEWBindingSource5;
            this.dataGridViewSurveyPages.Location = new System.Drawing.Point(299, 72);
            this.dataGridViewSurveyPages.Name = "dataGridViewSurveyPages";
            this.dataGridViewSurveyPages.Size = new System.Drawing.Size(278, 257);
            this.dataGridViewSurveyPages.TabIndex = 15;
            // 
            // surveypageidDataGridViewTextBoxColumn
            // 
            this.surveypageidDataGridViewTextBoxColumn.DataPropertyName = "survey_page_id";
            this.surveypageidDataGridViewTextBoxColumn.HeaderText = "survey_page_id";
            this.surveypageidDataGridViewTextBoxColumn.Name = "surveypageidDataGridViewTextBoxColumn";
            this.surveypageidDataGridViewTextBoxColumn.ReadOnly = true;
            this.surveypageidDataGridViewTextBoxColumn.Width = 75;
            // 
            // viewidDataGridViewTextBoxColumn
            // 
            this.viewidDataGridViewTextBoxColumn.DataPropertyName = "view_id";
            this.viewidDataGridViewTextBoxColumn.HeaderText = "view_id";
            this.viewidDataGridViewTextBoxColumn.Name = "viewidDataGridViewTextBoxColumn";
            this.viewidDataGridViewTextBoxColumn.Width = 50;
            // 
            // pagenumberDataGridViewTextBoxColumn
            // 
            this.pagenumberDataGridViewTextBoxColumn.DataPropertyName = "page_number";
            this.pagenumberDataGridViewTextBoxColumn.HeaderText = "page_number";
            this.pagenumberDataGridViewTextBoxColumn.Name = "pagenumberDataGridViewTextBoxColumn";
            this.pagenumberDataGridViewTextBoxColumn.Width = 75;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.Width = 75;
            // 
            // weatherDataGridViewTextBoxColumn
            // 
            this.weatherDataGridViewTextBoxColumn.DataPropertyName = "weather";
            this.weatherDataGridViewTextBoxColumn.HeaderText = "weather";
            this.weatherDataGridViewTextBoxColumn.Name = "weatherDataGridViewTextBoxColumn";
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            // 
            // fKSURVEYPAGEVIEWBindingSource5
            // 
            this.fKSURVEYPAGEVIEWBindingSource5.DataMember = "FK_SURVEY_PAGE_VIEW";
            this.fKSURVEYPAGEVIEWBindingSource5.DataSource = this.fKVIEWSUBWATERSHEDBindingSource3;
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
            // buttonDeleteSelectedPage
            // 
            this.buttonDeleteSelectedPage.Location = new System.Drawing.Point(450, 335);
            this.buttonDeleteSelectedPage.Name = "buttonDeleteSelectedPage";
            this.buttonDeleteSelectedPage.Size = new System.Drawing.Size(127, 26);
            this.buttonDeleteSelectedPage.TabIndex = 8;
            this.buttonDeleteSelectedPage.Text = "Delete Selected Page";
            this.buttonDeleteSelectedPage.UseVisualStyleBackColor = true;
            this.buttonDeleteSelectedPage.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAddSurveyPage
            // 
            this.buttonAddSurveyPage.Location = new System.Drawing.Point(299, 335);
            this.buttonAddSurveyPage.Name = "buttonAddSurveyPage";
            this.buttonAddSurveyPage.Size = new System.Drawing.Size(127, 26);
            this.buttonAddSurveyPage.TabIndex = 7;
            this.buttonAddSurveyPage.Text = "Add survey page...";
            this.buttonAddSurveyPage.UseVisualStyleBackColor = true;
            this.buttonAddSurveyPage.Click += new System.EventHandler(this.buttonAddSurveyPage_Click);
            // 
            // buttonAddView
            // 
            this.buttonAddView.Location = new System.Drawing.Point(12, 335);
            this.buttonAddView.Name = "buttonAddView";
            this.buttonAddView.Size = new System.Drawing.Size(127, 26);
            this.buttonAddView.TabIndex = 6;
            this.buttonAddView.Text = "Add view...";
            this.buttonAddView.UseVisualStyleBackColor = true;
            this.buttonAddView.Click += new System.EventHandler(this.buttonAddView_Click);
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
            // fKSURVEYPAGEVIEWBindingSource4
            // 
            this.fKSURVEYPAGEVIEWBindingSource4.DataMember = "FK_SURVEY_PAGE_VIEW";
            this.fKSURVEYPAGEVIEWBindingSource4.DataSource = this.fKVIEWSUBWATERSHEDBindingSource1;
            // 
            // fKVIEWSUBWATERSHEDBindingSource1
            // 
            this.fKVIEWSUBWATERSHEDBindingSource1.DataMember = "FK_VIEW_SUBWATERSHED";
            this.fKVIEWSUBWATERSHEDBindingSource1.DataSource = this.fKSUBWATERSHEDWATERSHEDBindingSource;
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
            // sWSPVIEWBindingSource
            // 
            this.sWSPVIEWBindingSource.DataMember = "SWSP_VIEW";
            this.sWSPVIEWBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // sWSPSURVEYPAGEBindingSource
            // 
            this.sWSPSURVEYPAGEBindingSource.DataMember = "SWSP_SURVEY_PAGE";
            this.sWSPSURVEYPAGEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(610, 403);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(105, 33);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // sWSP_WATERSHEDTableAdapter
            // 
            this.sWSP_WATERSHEDTableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_SUBWATERSHEDTableAdapter
            // 
            this.sWSP_SUBWATERSHEDTableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_VIEWTableAdapter
            // 
            this.sWSP_VIEWTableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_EVALUATORTableAdapter
            // 
            this.sWSP_EVALUATORTableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_CULVERT_OPENING_TYPETableAdapter
            // 
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_FACING_TYPETableAdapter
            // 
            this.sWSP_FACING_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_SHAPE_TYPETableAdapter
            // 
            this.sWSP_SHAPE_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_MATERIAL_TYPETableAdapter
            // 
            this.sWSP_MATERIAL_TYPETableAdapter.ClearBeforeFill = true;
            // 
            // sWSP_SURVEY_PAGETableAdapter
            // 
            this.sWSP_SURVEY_PAGETableAdapter.ClearBeforeFill = true;
            // 
            // fKSURVEYPAGEVIEWBindingSource1
            // 
            this.fKSURVEYPAGEVIEWBindingSource1.DataMember = "FK_SURVEY_PAGE_VIEW";
            this.fKSURVEYPAGEVIEWBindingSource1.DataSource = this.fKVIEWSUBWATERSHEDBindingSource;
            // 
            // fKSURVEYPAGEVIEWBindingSource2
            // 
            this.fKSURVEYPAGEVIEWBindingSource2.DataMember = "FK_SURVEY_PAGE_VIEW";
            this.fKSURVEYPAGEVIEWBindingSource2.DataSource = this.fKVIEWSUBWATERSHEDBindingSource;
            // 
            // fKSURVEYPAGEVIEWBindingSource3
            // 
            this.fKSURVEYPAGEVIEWBindingSource3.DataMember = "FK_SURVEY_PAGE_VIEW";
            this.fKSURVEYPAGEVIEWBindingSource3.DataSource = this.fKVIEWSUBWATERSHEDBindingSource;
            // 
            // fKVIEWSUBWATERSHEDBindingSource2
            // 
            this.fKVIEWSUBWATERSHEDBindingSource2.DataMember = "FK_VIEW_SUBWATERSHED";
            this.fKVIEWSUBWATERSHEDBindingSource2.DataSource = this.fKSUBWATERSHEDWATERSHEDBindingSource;
            // 
            // FormSWSPFieldDataAdministration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 444);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControlFormSWSPFieldDataAdministration);
            this.Name = "FormSWSPFieldDataAdministration";
            this.Text = "FormSWSPFieldDataAdministration";
            this.Load += new System.EventHandler(this.FormSWSPFieldDataAdministration_Load);
            this.tabControlFormSWSPFieldDataAdministration.ResumeLayout(false);
            this.tabPageWatersheds.ResumeLayout(false);
            this.tabPageWatersheds.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSubwatersheds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWatersheds)).EndInit();
            this.tabPageEvaluators.ResumeLayout(false);
            this.tabPageEvaluators.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvaluators)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPEVALUATORBindingSource)).EndInit();
            this.tabPageTypes.ResumeLayout(false);
            this.tabPageTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPMATERIALTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShapes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSHAPETYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFacings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCulvertOpenings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTOPENINGTYPEBindingSource)).EndInit();
            this.tabPageViewsAndSurveys.ResumeLayout(false);
            this.tabPageViewsAndSurveys.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSurveyPages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPVIEWBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSURVEYPAGEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource2)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.TabControl tabControlFormSWSPFieldDataAdministration;
        private System.Windows.Forms.TabPage tabPageWatersheds;
        private System.Windows.Forms.TabPage tabPageEvaluators;
        private System.Windows.Forms.TabPage tabPageTypes;
        private System.Windows.Forms.TabPage tabPageViewsAndSurveys;
        private System.Windows.Forms.Label labelWatersheds;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelSubwatersheds;
        private System.Windows.Forms.Button buttonSubwatershedsDelete;
        private System.Windows.Forms.Button buttonSubwatershedsAdd;
        private System.Windows.Forms.Button buttonWatershedsDelete;
        private System.Windows.Forms.Button buttonWatershedsAdd;
        private System.Windows.Forms.DataGridView dataGridViewSubwatersheds;
        private System.Windows.Forms.DataGridView dataGridViewWatersheds;
        private System.Windows.Forms.Button buttonEvaluatorsDelete;
        private System.Windows.Forms.Button buttonEvaluatorsAdd;
        private System.Windows.Forms.DataGridView dataGridViewEvaluators;
        private System.Windows.Forms.Label labelEvaluators;
        private System.Windows.Forms.Button buttonMaterialsDelete;
        private System.Windows.Forms.Button buttonMaterialsAdd;
        private System.Windows.Forms.Button buttonShapesDelete;
        private System.Windows.Forms.Button buttonShapesAdd;
        private System.Windows.Forms.Button buttonFacingsDelete;
        private System.Windows.Forms.Button buttonFacingsAdd;
        private System.Windows.Forms.Button buttonCulvertOpeningsUpdate;
        private System.Windows.Forms.Button buttonCulvertOpeningsAdd;
        private System.Windows.Forms.Label labelMaterials;
        private System.Windows.Forms.Label labelShapes;
        private System.Windows.Forms.DataGridView dataGridViewMaterials;
        private System.Windows.Forms.DataGridView dataGridViewShapes;
        private System.Windows.Forms.DataGridView dataGridViewFacings;
        private System.Windows.Forms.DataGridView dataGridViewCulvertOpenings;
        private System.Windows.Forms.Label labelFacings;
        private System.Windows.Forms.Label labelCulvertOpenings;
        private System.Windows.Forms.Label labelSubwatershed;
        private System.Windows.Forms.Label labelWatershed;
        private System.Windows.Forms.Label labelViews;
        private System.Windows.Forms.Button buttonDeleteSelectedPage;
        private System.Windows.Forms.Button buttonAddSurveyPage;
        private System.Windows.Forms.Button buttonAddView;
        private System.Windows.Forms.ComboBox comboBoxSubwatershed;
        private System.Windows.Forms.ComboBox comboBoxWatershed;
        private SANDBOXDataSet sANDBOXDataSet;
        private System.Windows.Forms.BindingSource sWSPWATERSHEDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_WATERSHEDTableAdapter sWSP_WATERSHEDTableAdapter;
        private System.Windows.Forms.BindingSource fKSUBWATERSHEDWATERSHEDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_SUBWATERSHEDTableAdapter sWSP_SUBWATERSHEDTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn watershedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource fKVIEWSUBWATERSHEDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_VIEWTableAdapter sWSP_VIEWTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn subwatershed;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource fKSUBWATERSHEDWATERSHEDBindingSource1;
        private System.Windows.Forms.Button buttonSubwatershedsUpdate;
        private System.Windows.Forms.Button buttonWatershedsUpdate;
        private System.Windows.Forms.BindingSource sWSPEVALUATORBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_EVALUATORTableAdapter sWSP_EVALUATORTableAdapter;
        private System.Windows.Forms.Button buttonEvaluatorsUpdate;
        private System.Windows.Forms.Button buttonShapesUpdate;
        private System.Windows.Forms.Button buttonMaterialsUpdate;
        private System.Windows.Forms.Button buttonCulvertOpeningsDelete;
        private System.Windows.Forms.Button buttonFacingsUpdate;
        private System.Windows.Forms.BindingSource sWSPCULVERTOPENINGTYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_CULVERT_OPENING_TYPETableAdapter sWSP_CULVERT_OPENING_TYPETableAdapter;
        private System.Windows.Forms.BindingSource sWSPFACINGTYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_FACING_TYPETableAdapter sWSP_FACING_TYPETableAdapter;
        private System.Windows.Forms.BindingSource sWSPSHAPETYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_SHAPE_TYPETableAdapter sWSP_SHAPE_TYPETableAdapter;
        private System.Windows.Forms.BindingSource sWSPMATERIALTYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_MATERIAL_TYPETableAdapter sWSP_MATERIAL_TYPETableAdapter;
        private System.Windows.Forms.BindingSource sWSPSURVEYPAGEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGETableAdapter sWSP_SURVEY_PAGETableAdapter;
        private System.Windows.Forms.BindingSource sWSPVIEWBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn initialsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn materialDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn shapeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn facingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn culvertopeningDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn2;
        private System.Windows.Forms.BindingSource fKSURVEYPAGEVIEWBindingSource;
        private System.Windows.Forms.BindingSource fKSURVEYPAGEVIEWBindingSource1;
        private System.Windows.Forms.BindingSource fKSURVEYPAGEVIEWBindingSource2;
        private System.Windows.Forms.BindingSource fKVIEWSUBWATERSHEDBindingSource1;
        private System.Windows.Forms.DataGridView dataGridViewSurveyPages;
        private System.Windows.Forms.BindingSource fKSURVEYPAGEVIEWBindingSource3;
        private System.Windows.Forms.BindingSource fKSURVEYPAGEVIEWBindingSource4;
        private System.Windows.Forms.DataGridViewTextBoxColumn surveypageidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn viewidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pagenumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn weatherDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource fKVIEWSUBWATERSHEDBindingSource2;
        private Infragistics.Win.Misc.UltraButton ultraButtonDeleteSelectedView;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private System.Windows.Forms.BindingSource fKVIEWSUBWATERSHEDBindingSource3;
        private System.Windows.Forms.BindingSource fKSURVEYPAGEVIEWBindingSource5;
    }
}