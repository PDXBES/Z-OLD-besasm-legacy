namespace SWI_2
{
    partial class FormMain
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
            Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SWSP_SHAPE_TYPE", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_SHAPE_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_SHAPE_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_SHAPE_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_SHAPE_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 3);
            Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SWSP_MATERIAL_TYPE", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand7 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand8 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand9 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand10 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn75 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn76 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn77 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn78 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand11 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 3);
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand12 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 5);
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand13 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SWSP_FACING_TYPE", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn79 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn80 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn81 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn82 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_FACING_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn83 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_FACING_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand14 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_FACING_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn84 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn85 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn86 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn87 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn88 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn89 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn90 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn91 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn92 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn93 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn94 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn95 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand15 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_FACING_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn96 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn97 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn98 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn99 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn100 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn101 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn102 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn103 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn104 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn105 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn106 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand16 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn107 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn108 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn109 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn110 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand17 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 3);
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand18 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SWSP_MATERIAL_TYPE", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn111 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn112 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn113 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn114 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn115 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn116 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand19 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn117 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn118 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn119 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn120 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn121 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn122 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn123 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn124 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn125 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn126 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn127 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn128 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand20 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn129 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn130 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn131 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn132 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn133 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn134 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn135 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn136 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn137 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn138 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn139 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand21 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn140 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn141 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn142 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn143 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn144 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn145 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn146 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn147 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn148 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn149 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn150 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn151 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand22 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn152 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn153 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn154 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn155 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand23 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 3);
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand24 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 5);
            Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand25 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SWSP_SHAPE_TYPE", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn156 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn157 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn158 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn159 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_SHAPE_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn160 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_SHAPE_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand26 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_SHAPE_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn161 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn162 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn163 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn164 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn165 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn166 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn167 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn168 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn169 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn170 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn171 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn172 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand27 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_SHAPE_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn173 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn174 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn175 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn176 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn177 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn178 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn179 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn180 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn181 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn182 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn183 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn184 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand28 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn185 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn186 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn187 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn188 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand29 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 3);
            Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand30 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SWSP_FACING_TYPE", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn189 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn190 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn191 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn192 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_FACING_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn193 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_FACING_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand31 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_FACING_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn194 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn195 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn196 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn197 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn198 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn199 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn200 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn201 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn202 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn203 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn204 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn205 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand32 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_FACING_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn206 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn207 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn208 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn209 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn210 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn211 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn212 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn213 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn214 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn215 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn216 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand33 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn217 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn218 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn219 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn220 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand34 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 3);
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand35 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SWSP_MATERIAL_TYPE", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn221 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn222 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn223 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn224 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn225 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn226 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand36 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn227 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn228 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn229 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn230 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn231 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn232 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn233 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn234 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn235 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn236 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn237 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn238 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand37 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn239 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn240 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn241 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn242 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn243 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn244 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn245 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn246 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn247 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn248 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn249 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand38 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn250 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn251 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn252 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn253 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn254 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn255 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn256 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn257 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn258 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn259 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn260 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn261 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand39 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn262 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn263 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn264 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn265 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand40 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 3);
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand41 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 5);
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
            this.buttonAddView = new System.Windows.Forms.Button();
            this.buttonAddSurveyPage = new System.Windows.Forms.Button();
            this.fKSURVEYPAGEVIEWBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fKVIEWSUBWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fKSUBWATERSHEDWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sANDBOXDataSet = new SWI_2.SANDBOXDataSet();
            this.tabControlDitchesCulvertsPipes = new System.Windows.Forms.TabControl();
            this.tabPagePipes = new System.Windows.Forms.TabPage();
            this.ultraCombo6 = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.fKPIPESURVEYPAGEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPSHAPETYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ultraCombo1 = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.sWSPMATERIALTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ultraNumericEditorPipesInnerDiameter = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.ultraNumericEditorPipesDSDepth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.ultraNumericEditorPipesUSDepth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.buttonUpdatePipe = new System.Windows.Forms.Button();
            this.buttonPipesViewAddPhotos = new System.Windows.Forms.Button();
            this.labelPipesShape = new System.Windows.Forms.Label();
            this.labelPipesInnerDiameter = new System.Windows.Forms.Label();
            this.textBoxPipesDSNode = new System.Windows.Forms.TextBox();
            this.textBoxPipesUSNode = new System.Windows.Forms.TextBox();
            this.labelPipesUSNode = new System.Windows.Forms.Label();
            this.labelPipesDSNode = new System.Windows.Forms.Label();
            this.labelMaterial = new System.Windows.Forms.Label();
            this.labelPipesDSDepth = new System.Windows.Forms.Label();
            this.labelPipesUSDepth = new System.Windows.Forms.Label();
            this.buttonPipesAdd = new System.Windows.Forms.Button();
            this.buttonPipesDelete = new System.Windows.Forms.Button();
            this.dataGridViewPipes = new System.Windows.Forms.DataGridView();
            this.usnodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dsnodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageDitches = new System.Windows.Forms.TabPage();
            this.ultraCombo4 = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.fKDITCHSURVEYPAGEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPFACINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ultraCombo3 = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.ultraNumericEditorDitchesBottomWidth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.ultraNumericEditorDitchesTopWidth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.ultraNumericEditorDitchesDepth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.buttonUpdateDitch = new System.Windows.Forms.Button();
            this.buttonDitchesViewAddPhotos = new System.Windows.Forms.Button();
            this.textBoxDitchesNode = new System.Windows.Forms.TextBox();
            this.labelDitchesNode = new System.Windows.Forms.Label();
            this.labelDitchesFacingDirection = new System.Windows.Forms.Label();
            this.labelDitchesMaterial = new System.Windows.Forms.Label();
            this.labelDitchesBottomWidth = new System.Windows.Forms.Label();
            this.labelDitchesTopWidth = new System.Windows.Forms.Label();
            this.labelDitchesDepth = new System.Windows.Forms.Label();
            this.buttonDitchesAdd = new System.Windows.Forms.Button();
            this.buttonDitchesDelete = new System.Windows.Forms.Button();
            this.dataGridViewDitches = new System.Windows.Forms.DataGridView();
            this.facing = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.sWSPFACINGTYPEBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.nodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.facingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageCulverts = new System.Windows.Forms.TabPage();
            this.ultraCombo7 = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.fKCULVERTSURVEYPAGEBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ultraCombo5 = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.ultraCombo2 = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.ultraNumericEditorCulvertsUnobstructedHeight = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.ultraNumericEditorCulvertsFullDepth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.buttonUpdateCulvert = new System.Windows.Forms.Button();
            this.buttonCulvertsViewAddPhotos = new System.Windows.Forms.Button();
            this.comboBoxCulvertsType = new System.Windows.Forms.ComboBox();
            this.sWSPCULVERTOPENINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBoxCulvertsNode = new System.Windows.Forms.TextBox();
            this.labelCulvertsNode = new System.Windows.Forms.Label();
            this.labelCulvertsFacingDirection = new System.Windows.Forms.Label();
            this.labelCulvertsMaterial = new System.Windows.Forms.Label();
            this.labelCulvertsUnobstructedHeight = new System.Windows.Forms.Label();
            this.labelCulvertsFullDepth = new System.Windows.Forms.Label();
            this.labelCulvertsShape = new System.Windows.Forms.Label();
            this.labelCulvertsType = new System.Windows.Forms.Label();
            this.buttonCulvertsAdd = new System.Windows.Forms.Button();
            this.buttonCulvertsDelete = new System.Windows.Forms.Button();
            this.dataGridViewCulverts = new System.Windows.Forms.DataGridView();
            this.nodeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.facingDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.fKCULVERTFACINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataAdministratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.textBoxFindNode = new System.Windows.Forms.TextBox();
            this.buttonUpdateDatabase = new System.Windows.Forms.Button();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.comboBoxWatershed = new System.Windows.Forms.ComboBox();
            this.comboBoxSubwatershed = new System.Windows.Forms.ComboBox();
            this.comboBoxView = new System.Windows.Forms.ComboBox();
            this.comboBoxSurveyPage = new System.Windows.Forms.ComboBox();
            this.textBoxWeather = new System.Windows.Forms.TextBox();
            this.fKCULVERTFACINGTYPEBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.fKPIPEMATERIALTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fKPIPEMATERIALTYPEBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.fKPIPEMATERIALTYPEBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.ultraDateTimeEditor1 = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.checkedListBoxEvaluators = new System.Windows.Forms.CheckedListBox();
            this.sWSPPHOTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPCULVERTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPPIPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPEVALUATORBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_WATERSHEDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_WATERSHEDTableAdapter();
            this.sWSP_SUBWATERSHEDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SUBWATERSHEDTableAdapter();
            this.sWSPVIEWBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_VIEWTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_VIEWTableAdapter();
            this.sWSP_SURVEY_PAGETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGETableAdapter();
            this.sWSP_EVALUATORTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_EVALUATORTableAdapter();
            this.sWSPEVALUATORBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPSURVEYPAGEEVALUATORBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSPEVALUATORBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGE_EVALUATORTableAdapter();
            this.sWSP_DITCHTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_DITCHTableAdapter();
            this.sWSPDITCHBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_FACING_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_FACING_TYPETableAdapter();
            this.sWSP_MATERIAL_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_MATERIAL_TYPETableAdapter();
            this.relationalIDsTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.RelationalIDsTableAdapter();
            this.sWSP_PHOTOTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_PHOTOTableAdapter();
            this.sWSP_CULVERTTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_CULVERTTableAdapter();
            this.sWSP_CULVERT_OPENING_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_CULVERT_OPENING_TYPETableAdapter();
            this.sWSP_SHAPE_TYPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SHAPE_TYPETableAdapter();
            this.sWSP_PIPETableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_PIPETableAdapter();
            this.sWSPCULVERTBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_GLOBAL_IDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_GLOBAL_IDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_GLOBAL_IDTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).BeginInit();
            this.tabControlDitchesCulvertsPipes.SuspendLayout();
            this.tabPagePipes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPESURVEYPAGEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSHAPETYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPMATERIALTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesInnerDiameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesDSDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesUSDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPipes)).BeginInit();
            this.tabPageDitches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKDITCHSURVEYPAGEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesBottomWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesTopWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDitches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource1)).BeginInit();
            this.tabPageCulverts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTSURVEYPAGEBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorCulvertsUnobstructedHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorCulvertsFullDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTOPENINGTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCulverts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTFACINGTYPEBindingSource)).BeginInit();
            this.menuStripMainForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTFACINGTYPEBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPEMATERIALTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPEMATERIALTYPEBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPEMATERIALTYPEBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDateTimeEditor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPPHOTOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPPIPEBindingSource)).BeginInit();
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
            this.buttonAddView.Text = "Add map...";
            this.buttonAddView.UseVisualStyleBackColor = true;
            this.buttonAddView.Click += new System.EventHandler(this.buttonAddView_Click);
            // 
            // buttonAddSurveyPage
            // 
            this.buttonAddSurveyPage.Location = new System.Drawing.Point(242, 44);
            this.buttonAddSurveyPage.Name = "buttonAddSurveyPage";
            this.buttonAddSurveyPage.Size = new System.Drawing.Size(119, 22);
            this.buttonAddSurveyPage.TabIndex = 100;
            this.buttonAddSurveyPage.Text = "Add survey page...";
            this.buttonAddSurveyPage.UseVisualStyleBackColor = true;
            this.buttonAddSurveyPage.Click += new System.EventHandler(this.buttonAddSurveyPage_Click);
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
            // tabControlDitchesCulvertsPipes
            // 
            this.tabControlDitchesCulvertsPipes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlDitchesCulvertsPipes.Controls.Add(this.tabPagePipes);
            this.tabControlDitchesCulvertsPipes.Controls.Add(this.tabPageDitches);
            this.tabControlDitchesCulvertsPipes.Controls.Add(this.tabPageCulverts);
            this.tabControlDitchesCulvertsPipes.Location = new System.Drawing.Point(0, 132);
            this.tabControlDitchesCulvertsPipes.Name = "tabControlDitchesCulvertsPipes";
            this.tabControlDitchesCulvertsPipes.SelectedIndex = 0;
            this.tabControlDitchesCulvertsPipes.Size = new System.Drawing.Size(992, 196);
            this.tabControlDitchesCulvertsPipes.TabIndex = 9;
            this.tabControlDitchesCulvertsPipes.SelectedIndexChanged += new System.EventHandler(this.tabControlDitchesCulvertsPipes_SelectedIndexChanged);
            // 
            // tabPagePipes
            // 
            this.tabPagePipes.Controls.Add(this.ultraCombo6);
            this.tabPagePipes.Controls.Add(this.ultraCombo1);
            this.tabPagePipes.Controls.Add(this.ultraNumericEditorPipesInnerDiameter);
            this.tabPagePipes.Controls.Add(this.ultraNumericEditorPipesDSDepth);
            this.tabPagePipes.Controls.Add(this.ultraNumericEditorPipesUSDepth);
            this.tabPagePipes.Controls.Add(this.buttonUpdatePipe);
            this.tabPagePipes.Controls.Add(this.buttonPipesViewAddPhotos);
            this.tabPagePipes.Controls.Add(this.labelPipesShape);
            this.tabPagePipes.Controls.Add(this.labelPipesInnerDiameter);
            this.tabPagePipes.Controls.Add(this.textBoxPipesDSNode);
            this.tabPagePipes.Controls.Add(this.textBoxPipesUSNode);
            this.tabPagePipes.Controls.Add(this.labelPipesUSNode);
            this.tabPagePipes.Controls.Add(this.labelPipesDSNode);
            this.tabPagePipes.Controls.Add(this.labelMaterial);
            this.tabPagePipes.Controls.Add(this.labelPipesDSDepth);
            this.tabPagePipes.Controls.Add(this.labelPipesUSDepth);
            this.tabPagePipes.Controls.Add(this.buttonPipesAdd);
            this.tabPagePipes.Controls.Add(this.buttonPipesDelete);
            this.tabPagePipes.Controls.Add(this.dataGridViewPipes);
            this.tabPagePipes.Location = new System.Drawing.Point(4, 22);
            this.tabPagePipes.Name = "tabPagePipes";
            this.tabPagePipes.Size = new System.Drawing.Size(984, 170);
            this.tabPagePipes.TabIndex = 2;
            this.tabPagePipes.Text = "Pipes";
            this.tabPagePipes.UseVisualStyleBackColor = true;
            this.tabPagePipes.Enter += new System.EventHandler(this.tabPagePipes_Enter);
            // 
            // ultraCombo6
            // 
            this.ultraCombo6.CheckedListSettings.CheckStateMember = "";
            this.ultraCombo6.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "shape", true));
            this.ultraCombo6.DataSource = this.sWSPSHAPETYPEBindingSource;
            appearance64.BackColor = System.Drawing.SystemColors.Window;
            appearance64.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraCombo6.DisplayLayout.Appearance = appearance64;
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
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
            ultraGridColumn6.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn6.Header.VisiblePosition = 0;
            ultraGridColumn7.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn7.Header.VisiblePosition = 1;
            ultraGridColumn8.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn8.Header.VisiblePosition = 2;
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
            ultraGridColumn16.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn16.Header.VisiblePosition = 10;
            ultraGridColumn17.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn17.Header.VisiblePosition = 11;
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
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17});
            ultraGridColumn18.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn18.Header.VisiblePosition = 0;
            ultraGridColumn19.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn19.Header.VisiblePosition = 1;
            ultraGridColumn20.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn20.Header.VisiblePosition = 2;
            ultraGridColumn21.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn21.Header.VisiblePosition = 3;
            ultraGridColumn22.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn22.Header.VisiblePosition = 4;
            ultraGridColumn23.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn23.Header.VisiblePosition = 5;
            ultraGridColumn24.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn24.Header.VisiblePosition = 6;
            ultraGridColumn25.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn25.Header.VisiblePosition = 7;
            ultraGridColumn26.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn26.Header.VisiblePosition = 8;
            ultraGridColumn27.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn27.Header.VisiblePosition = 9;
            ultraGridColumn28.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn28.Header.VisiblePosition = 10;
            ultraGridColumn29.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn29.Header.VisiblePosition = 11;
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
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29});
            ultraGridColumn30.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn30.Header.VisiblePosition = 0;
            ultraGridColumn31.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn31.Header.VisiblePosition = 1;
            ultraGridColumn32.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn32.Header.VisiblePosition = 2;
            ultraGridColumn33.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn33.Header.VisiblePosition = 3;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33});
            this.ultraCombo6.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraCombo6.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.ultraCombo6.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.ultraCombo6.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.ultraCombo6.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.ultraCombo6.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraCombo6.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance61.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance61.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo6.DisplayLayout.GroupByBox.Appearance = appearance61;
            appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo6.DisplayLayout.GroupByBox.BandLabelAppearance = appearance62;
            this.ultraCombo6.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance63.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance63.BackColor2 = System.Drawing.SystemColors.Control;
            appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo6.DisplayLayout.GroupByBox.PromptAppearance = appearance63;
            this.ultraCombo6.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraCombo6.DisplayLayout.MaxRowScrollRegions = 1;
            appearance72.BackColor = System.Drawing.SystemColors.Window;
            appearance72.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraCombo6.DisplayLayout.Override.ActiveCellAppearance = appearance72;
            appearance67.BackColor = System.Drawing.SystemColors.Highlight;
            appearance67.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraCombo6.DisplayLayout.Override.ActiveRowAppearance = appearance67;
            this.ultraCombo6.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraCombo6.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance66.BackColor = System.Drawing.SystemColors.Window;
            this.ultraCombo6.DisplayLayout.Override.CardAreaAppearance = appearance66;
            appearance65.BorderColor = System.Drawing.Color.Silver;
            appearance65.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraCombo6.DisplayLayout.Override.CellAppearance = appearance65;
            this.ultraCombo6.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraCombo6.DisplayLayout.Override.CellPadding = 0;
            appearance69.BackColor = System.Drawing.SystemColors.Control;
            appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance69.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo6.DisplayLayout.Override.GroupByRowAppearance = appearance69;
            appearance71.TextHAlignAsString = "Left";
            this.ultraCombo6.DisplayLayout.Override.HeaderAppearance = appearance71;
            this.ultraCombo6.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraCombo6.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance70.BackColor = System.Drawing.SystemColors.Window;
            appearance70.BorderColor = System.Drawing.Color.Silver;
            this.ultraCombo6.DisplayLayout.Override.RowAppearance = appearance70;
            this.ultraCombo6.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance68.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraCombo6.DisplayLayout.Override.TemplateAddRowAppearance = appearance68;
            this.ultraCombo6.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraCombo6.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraCombo6.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraCombo6.DisplayMember = "shape";
            this.ultraCombo6.Location = new System.Drawing.Point(881, 80);
            this.ultraCombo6.Name = "ultraCombo6";
            this.ultraCombo6.Size = new System.Drawing.Size(100, 22);
            this.ultraCombo6.TabIndex = 68;
            this.ultraCombo6.Text = "ultraCombo6";
            this.ultraCombo6.ValueMember = "shape_type_id";
            // 
            // fKPIPESURVEYPAGEBindingSource
            // 
            this.fKPIPESURVEYPAGEBindingSource.DataMember = "FK_PIPE_SURVEY_PAGE";
            this.fKPIPESURVEYPAGEBindingSource.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            // 
            // sWSPSHAPETYPEBindingSource
            // 
            this.sWSPSHAPETYPEBindingSource.DataMember = "SWSP_SHAPE_TYPE";
            this.sWSPSHAPETYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // ultraCombo1
            // 
            this.ultraCombo1.CheckedListSettings.CheckStateMember = "";
            this.ultraCombo1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "material", true));
            this.ultraCombo1.DataSource = this.sWSPMATERIALTYPEBindingSource;
            appearance25.BackColor = System.Drawing.SystemColors.Window;
            appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraCombo1.DisplayLayout.Appearance = appearance25;
            ultraGridColumn34.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn34.Header.VisiblePosition = 0;
            ultraGridColumn34.Hidden = true;
            ultraGridColumn35.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn35.Header.VisiblePosition = 1;
            ultraGridColumn36.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn36.Header.VisiblePosition = 2;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn37.Header.VisiblePosition = 3;
            ultraGridColumn38.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn38.Header.VisiblePosition = 4;
            ultraGridColumn39.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn39.Header.VisiblePosition = 5;
            ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39});
            ultraGridColumn40.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn40.Header.VisiblePosition = 0;
            ultraGridColumn41.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn41.Header.VisiblePosition = 1;
            ultraGridColumn42.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn42.Header.VisiblePosition = 2;
            ultraGridColumn43.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn43.Header.VisiblePosition = 3;
            ultraGridColumn44.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn44.Header.VisiblePosition = 4;
            ultraGridColumn45.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn45.Header.VisiblePosition = 5;
            ultraGridColumn46.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn46.Header.VisiblePosition = 6;
            ultraGridColumn47.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn47.Header.VisiblePosition = 7;
            ultraGridColumn48.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn48.Header.VisiblePosition = 8;
            ultraGridColumn49.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn49.Header.VisiblePosition = 9;
            ultraGridColumn50.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn50.Header.VisiblePosition = 10;
            ultraGridColumn51.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn51.Header.VisiblePosition = 11;
            ultraGridBand7.Columns.AddRange(new object[] {
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn43,
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49,
            ultraGridColumn50,
            ultraGridColumn51});
            ultraGridColumn52.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn52.Header.VisiblePosition = 0;
            ultraGridColumn53.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn53.Header.VisiblePosition = 1;
            ultraGridColumn54.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn54.Header.VisiblePosition = 2;
            ultraGridColumn55.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn55.Header.VisiblePosition = 3;
            ultraGridColumn56.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn56.Header.VisiblePosition = 4;
            ultraGridColumn57.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn57.Header.VisiblePosition = 5;
            ultraGridColumn58.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn58.Header.VisiblePosition = 6;
            ultraGridColumn59.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn59.Header.VisiblePosition = 7;
            ultraGridColumn60.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn60.Header.VisiblePosition = 8;
            ultraGridColumn61.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn61.Header.VisiblePosition = 9;
            ultraGridColumn62.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn62.Header.VisiblePosition = 10;
            ultraGridBand8.Columns.AddRange(new object[] {
            ultraGridColumn52,
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56,
            ultraGridColumn57,
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn60,
            ultraGridColumn61,
            ultraGridColumn62});
            ultraGridColumn63.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn63.Header.VisiblePosition = 0;
            ultraGridColumn64.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn64.Header.VisiblePosition = 1;
            ultraGridColumn65.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn65.Header.VisiblePosition = 2;
            ultraGridColumn66.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn66.Header.VisiblePosition = 3;
            ultraGridColumn67.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn67.Header.VisiblePosition = 4;
            ultraGridColumn68.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn68.Header.VisiblePosition = 5;
            ultraGridColumn69.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn69.Header.VisiblePosition = 6;
            ultraGridColumn70.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn70.Header.VisiblePosition = 7;
            ultraGridColumn71.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn71.Header.VisiblePosition = 8;
            ultraGridColumn72.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn72.Header.VisiblePosition = 9;
            ultraGridColumn73.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn73.Header.VisiblePosition = 10;
            ultraGridColumn74.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn74.Header.VisiblePosition = 11;
            ultraGridBand9.Columns.AddRange(new object[] {
            ultraGridColumn63,
            ultraGridColumn64,
            ultraGridColumn65,
            ultraGridColumn66,
            ultraGridColumn67,
            ultraGridColumn68,
            ultraGridColumn69,
            ultraGridColumn70,
            ultraGridColumn71,
            ultraGridColumn72,
            ultraGridColumn73,
            ultraGridColumn74});
            ultraGridColumn75.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn75.Header.VisiblePosition = 0;
            ultraGridColumn76.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn76.Header.VisiblePosition = 1;
            ultraGridColumn77.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn77.Header.VisiblePosition = 2;
            ultraGridColumn78.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn78.Header.VisiblePosition = 3;
            ultraGridBand10.Columns.AddRange(new object[] {
            ultraGridColumn75,
            ultraGridColumn76,
            ultraGridColumn77,
            ultraGridColumn78});
            this.ultraCombo1.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            this.ultraCombo1.DisplayLayout.BandsSerializer.Add(ultraGridBand7);
            this.ultraCombo1.DisplayLayout.BandsSerializer.Add(ultraGridBand8);
            this.ultraCombo1.DisplayLayout.BandsSerializer.Add(ultraGridBand9);
            this.ultraCombo1.DisplayLayout.BandsSerializer.Add(ultraGridBand10);
            this.ultraCombo1.DisplayLayout.BandsSerializer.Add(ultraGridBand11);
            this.ultraCombo1.DisplayLayout.BandsSerializer.Add(ultraGridBand12);
            this.ultraCombo1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraCombo1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance19.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance19.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance19.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo1.DisplayLayout.GroupByBox.Appearance = appearance19;
            appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance20;
            this.ultraCombo1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance21.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance21.BackColor2 = System.Drawing.SystemColors.Control;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo1.DisplayLayout.GroupByBox.PromptAppearance = appearance21;
            this.ultraCombo1.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraCombo1.DisplayLayout.MaxRowScrollRegions = 1;
            appearance33.BackColor = System.Drawing.SystemColors.Window;
            appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraCombo1.DisplayLayout.Override.ActiveCellAppearance = appearance33;
            appearance28.BackColor = System.Drawing.SystemColors.Highlight;
            appearance28.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraCombo1.DisplayLayout.Override.ActiveRowAppearance = appearance28;
            this.ultraCombo1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraCombo1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance27.BackColor = System.Drawing.SystemColors.Window;
            this.ultraCombo1.DisplayLayout.Override.CardAreaAppearance = appearance27;
            appearance26.BorderColor = System.Drawing.Color.Silver;
            appearance26.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraCombo1.DisplayLayout.Override.CellAppearance = appearance26;
            this.ultraCombo1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraCombo1.DisplayLayout.Override.CellPadding = 0;
            appearance30.BackColor = System.Drawing.SystemColors.Control;
            appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance30.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance30.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo1.DisplayLayout.Override.GroupByRowAppearance = appearance30;
            appearance32.TextHAlignAsString = "Left";
            this.ultraCombo1.DisplayLayout.Override.HeaderAppearance = appearance32;
            this.ultraCombo1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraCombo1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            appearance31.BorderColor = System.Drawing.Color.Silver;
            this.ultraCombo1.DisplayLayout.Override.RowAppearance = appearance31;
            this.ultraCombo1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance29.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraCombo1.DisplayLayout.Override.TemplateAddRowAppearance = appearance29;
            this.ultraCombo1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraCombo1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraCombo1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraCombo1.DisplayMember = "material";
            this.ultraCombo1.Location = new System.Drawing.Point(775, 81);
            this.ultraCombo1.Name = "ultraCombo1";
            this.ultraCombo1.Size = new System.Drawing.Size(98, 22);
            this.ultraCombo1.TabIndex = 67;
            this.ultraCombo1.Text = "ultraCombo1";
            this.ultraCombo1.ValueMember = "material_type_id";
            // 
            // sWSPMATERIALTYPEBindingSource
            // 
            this.sWSPMATERIALTYPEBindingSource.DataMember = "SWSP_MATERIAL_TYPE";
            this.sWSPMATERIALTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // ultraNumericEditorPipesInnerDiameter
            // 
            this.ultraNumericEditorPipesInnerDiameter.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "inside_diam_in", true));
            this.ultraNumericEditorPipesInnerDiameter.Location = new System.Drawing.Point(679, 81);
            this.ultraNumericEditorPipesInnerDiameter.MaskInput = "{LOC}nnnnnnn.nn";
            this.ultraNumericEditorPipesInnerDiameter.Name = "ultraNumericEditorPipesInnerDiameter";
            this.ultraNumericEditorPipesInnerDiameter.Nullable = true;
            this.ultraNumericEditorPipesInnerDiameter.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorPipesInnerDiameter.Size = new System.Drawing.Size(90, 21);
            this.ultraNumericEditorPipesInnerDiameter.TabIndex = 66;
            // 
            // ultraNumericEditorPipesDSDepth
            // 
            this.ultraNumericEditorPipesDSDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "ds_depth_in", true));
            this.ultraNumericEditorPipesDSDepth.Location = new System.Drawing.Point(583, 81);
            this.ultraNumericEditorPipesDSDepth.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorPipesDSDepth.Name = "ultraNumericEditorPipesDSDepth";
            this.ultraNumericEditorPipesDSDepth.Nullable = true;
            this.ultraNumericEditorPipesDSDepth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorPipesDSDepth.Size = new System.Drawing.Size(90, 21);
            this.ultraNumericEditorPipesDSDepth.TabIndex = 65;
            // 
            // ultraNumericEditorPipesUSDepth
            // 
            this.ultraNumericEditorPipesUSDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "us_depth_in", true));
            this.ultraNumericEditorPipesUSDepth.Location = new System.Drawing.Point(487, 81);
            this.ultraNumericEditorPipesUSDepth.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorPipesUSDepth.Name = "ultraNumericEditorPipesUSDepth";
            this.ultraNumericEditorPipesUSDepth.Nullable = true;
            this.ultraNumericEditorPipesUSDepth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorPipesUSDepth.Size = new System.Drawing.Size(90, 21);
            this.ultraNumericEditorPipesUSDepth.TabIndex = 64;
            // 
            // buttonUpdatePipe
            // 
            this.buttonUpdatePipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdatePipe.Location = new System.Drawing.Point(881, 139);
            this.buttonUpdatePipe.Name = "buttonUpdatePipe";
            this.buttonUpdatePipe.Size = new System.Drawing.Size(97, 28);
            this.buttonUpdatePipe.TabIndex = 72;
            this.buttonUpdatePipe.Text = "Update Pipe";
            this.buttonUpdatePipe.UseVisualStyleBackColor = true;
            this.buttonUpdatePipe.Click += new System.EventHandler(this.buttonUpdatePipe_Click);
            // 
            // buttonPipesViewAddPhotos
            // 
            this.buttonPipesViewAddPhotos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPipesViewAddPhotos.Location = new System.Drawing.Point(487, 139);
            this.buttonPipesViewAddPhotos.Name = "buttonPipesViewAddPhotos";
            this.buttonPipesViewAddPhotos.Size = new System.Drawing.Size(220, 28);
            this.buttonPipesViewAddPhotos.TabIndex = 71;
            this.buttonPipesViewAddPhotos.Text = "View/Add Photos";
            this.buttonPipesViewAddPhotos.UseVisualStyleBackColor = true;
            this.buttonPipesViewAddPhotos.Click += new System.EventHandler(this.buttonPipesViewAddPhotos_Click);
            // 
            // labelPipesShape
            // 
            this.labelPipesShape.AutoSize = true;
            this.labelPipesShape.Location = new System.Drawing.Point(878, 66);
            this.labelPipesShape.Name = "labelPipesShape";
            this.labelPipesShape.Size = new System.Drawing.Size(38, 13);
            this.labelPipesShape.TabIndex = 66;
            this.labelPipesShape.Text = "Shape";
            // 
            // labelPipesInnerDiameter
            // 
            this.labelPipesInnerDiameter.AutoSize = true;
            this.labelPipesInnerDiameter.Location = new System.Drawing.Point(676, 66);
            this.labelPipesInnerDiameter.Name = "labelPipesInnerDiameter";
            this.labelPipesInnerDiameter.Size = new System.Drawing.Size(93, 13);
            this.labelPipesInnerDiameter.TabIndex = 65;
            this.labelPipesInnerDiameter.Text = "Inner Diameter (in)";
            // 
            // textBoxPipesDSNode
            // 
            this.textBoxPipesDSNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKPIPESURVEYPAGEBindingSource, "ds_node", true));
            this.textBoxPipesDSNode.Location = new System.Drawing.Point(391, 82);
            this.textBoxPipesDSNode.Name = "textBoxPipesDSNode";
            this.textBoxPipesDSNode.Size = new System.Drawing.Size(90, 20);
            this.textBoxPipesDSNode.TabIndex = 63;
            this.textBoxPipesDSNode.TextChanged += new System.EventHandler(this.textBoxPipesDSNode_TextChanged);
            this.textBoxPipesDSNode.Enter += new System.EventHandler(this.textBoxPipesDSNode_Enter);
            // 
            // textBoxPipesUSNode
            // 
            this.textBoxPipesUSNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKPIPESURVEYPAGEBindingSource, "us_node", true));
            this.textBoxPipesUSNode.Location = new System.Drawing.Point(295, 82);
            this.textBoxPipesUSNode.Name = "textBoxPipesUSNode";
            this.textBoxPipesUSNode.Size = new System.Drawing.Size(90, 20);
            this.textBoxPipesUSNode.TabIndex = 62;
            this.textBoxPipesUSNode.TextChanged += new System.EventHandler(this.textBoxPipesUSNode_TextChanged);
            this.textBoxPipesUSNode.Enter += new System.EventHandler(this.textBoxPipesUSNode_Enter);
            // 
            // labelPipesUSNode
            // 
            this.labelPipesUSNode.AutoSize = true;
            this.labelPipesUSNode.Location = new System.Drawing.Point(292, 66);
            this.labelPipesUSNode.Name = "labelPipesUSNode";
            this.labelPipesUSNode.Size = new System.Drawing.Size(51, 13);
            this.labelPipesUSNode.TabIndex = 60;
            this.labelPipesUSNode.Text = "US Node";
            // 
            // labelPipesDSNode
            // 
            this.labelPipesDSNode.AutoSize = true;
            this.labelPipesDSNode.Location = new System.Drawing.Point(388, 65);
            this.labelPipesDSNode.Name = "labelPipesDSNode";
            this.labelPipesDSNode.Size = new System.Drawing.Size(51, 13);
            this.labelPipesDSNode.TabIndex = 59;
            this.labelPipesDSNode.Text = "DS Node";
            // 
            // labelMaterial
            // 
            this.labelMaterial.AutoSize = true;
            this.labelMaterial.Location = new System.Drawing.Point(772, 66);
            this.labelMaterial.Name = "labelMaterial";
            this.labelMaterial.Size = new System.Drawing.Size(44, 13);
            this.labelMaterial.TabIndex = 57;
            this.labelMaterial.Text = "Material";
            // 
            // labelPipesDSDepth
            // 
            this.labelPipesDSDepth.AutoSize = true;
            this.labelPipesDSDepth.Location = new System.Drawing.Point(580, 65);
            this.labelPipesDSDepth.Name = "labelPipesDSDepth";
            this.labelPipesDSDepth.Size = new System.Drawing.Size(71, 13);
            this.labelPipesDSDepth.TabIndex = 56;
            this.labelPipesDSDepth.Text = "DS Depth (in)";
            // 
            // labelPipesUSDepth
            // 
            this.labelPipesUSDepth.AutoSize = true;
            this.labelPipesUSDepth.Location = new System.Drawing.Point(484, 65);
            this.labelPipesUSDepth.Name = "labelPipesUSDepth";
            this.labelPipesUSDepth.Size = new System.Drawing.Size(69, 13);
            this.labelPipesUSDepth.TabIndex = 55;
            this.labelPipesUSDepth.Text = "US depth (in)";
            // 
            // buttonPipesAdd
            // 
            this.buttonPipesAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPipesAdd.Location = new System.Drawing.Point(260, 139);
            this.buttonPipesAdd.Name = "buttonPipesAdd";
            this.buttonPipesAdd.Size = new System.Drawing.Size(65, 28);
            this.buttonPipesAdd.TabIndex = 52;
            this.buttonPipesAdd.Text = "Add";
            this.buttonPipesAdd.UseVisualStyleBackColor = true;
            this.buttonPipesAdd.Click += new System.EventHandler(this.buttonPipesAdd_Click);
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
            // dataGridViewPipes
            // 
            this.dataGridViewPipes.AllowUserToAddRows = false;
            this.dataGridViewPipes.AllowUserToDeleteRows = false;
            this.dataGridViewPipes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewPipes.AutoGenerateColumns = false;
            this.dataGridViewPipes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPipes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.usnodeDataGridViewTextBoxColumn,
            this.dsnodeDataGridViewTextBoxColumn});
            this.dataGridViewPipes.DataSource = this.fKPIPESURVEYPAGEBindingSource;
            this.dataGridViewPipes.Location = new System.Drawing.Point(3, 8);
            this.dataGridViewPipes.Name = "dataGridViewPipes";
            this.dataGridViewPipes.ReadOnly = true;
            this.dataGridViewPipes.Size = new System.Drawing.Size(248, 159);
            this.dataGridViewPipes.TabIndex = 44;
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
            this.dsnodeDataGridViewTextBoxColumn.HeaderText = "DS Node";
            this.dsnodeDataGridViewTextBoxColumn.Name = "dsnodeDataGridViewTextBoxColumn";
            this.dsnodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tabPageDitches
            // 
            this.tabPageDitches.Controls.Add(this.ultraCombo4);
            this.tabPageDitches.Controls.Add(this.ultraCombo3);
            this.tabPageDitches.Controls.Add(this.ultraNumericEditorDitchesBottomWidth);
            this.tabPageDitches.Controls.Add(this.ultraNumericEditorDitchesTopWidth);
            this.tabPageDitches.Controls.Add(this.ultraNumericEditorDitchesDepth);
            this.tabPageDitches.Controls.Add(this.buttonUpdateDitch);
            this.tabPageDitches.Controls.Add(this.buttonDitchesViewAddPhotos);
            this.tabPageDitches.Controls.Add(this.textBoxDitchesNode);
            this.tabPageDitches.Controls.Add(this.labelDitchesNode);
            this.tabPageDitches.Controls.Add(this.labelDitchesFacingDirection);
            this.tabPageDitches.Controls.Add(this.labelDitchesMaterial);
            this.tabPageDitches.Controls.Add(this.labelDitchesBottomWidth);
            this.tabPageDitches.Controls.Add(this.labelDitchesTopWidth);
            this.tabPageDitches.Controls.Add(this.labelDitchesDepth);
            this.tabPageDitches.Controls.Add(this.buttonDitchesAdd);
            this.tabPageDitches.Controls.Add(this.buttonDitchesDelete);
            this.tabPageDitches.Controls.Add(this.dataGridViewDitches);
            this.tabPageDitches.Location = new System.Drawing.Point(4, 22);
            this.tabPageDitches.Name = "tabPageDitches";
            this.tabPageDitches.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDitches.Size = new System.Drawing.Size(984, 170);
            this.tabPageDitches.TabIndex = 0;
            this.tabPageDitches.Text = "Ditches";
            this.tabPageDitches.UseVisualStyleBackColor = true;
            this.tabPageDitches.Click += new System.EventHandler(this.tabPageDitches_Click);
            this.tabPageDitches.Enter += new System.EventHandler(this.tabPageDitches_Entered);
            // 
            // ultraCombo4
            // 
            this.ultraCombo4.CheckedListSettings.CheckStateMember = "";
            this.ultraCombo4.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "facing", true));
            this.ultraCombo4.DataSource = this.sWSPFACINGTYPEBindingSource;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraCombo4.DisplayLayout.Appearance = appearance4;
            ultraGridColumn79.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn79.Header.VisiblePosition = 0;
            ultraGridColumn79.Hidden = true;
            ultraGridColumn80.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn80.Header.VisiblePosition = 1;
            ultraGridColumn81.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn81.Header.VisiblePosition = 2;
            ultraGridColumn81.Hidden = true;
            ultraGridColumn82.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn82.Header.VisiblePosition = 3;
            ultraGridColumn83.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn83.Header.VisiblePosition = 4;
            ultraGridBand13.Columns.AddRange(new object[] {
            ultraGridColumn79,
            ultraGridColumn80,
            ultraGridColumn81,
            ultraGridColumn82,
            ultraGridColumn83});
            ultraGridColumn84.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn84.Header.VisiblePosition = 0;
            ultraGridColumn85.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn85.Header.VisiblePosition = 1;
            ultraGridColumn86.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn86.Header.VisiblePosition = 2;
            ultraGridColumn87.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn87.Header.VisiblePosition = 3;
            ultraGridColumn88.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn88.Header.VisiblePosition = 4;
            ultraGridColumn89.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn89.Header.VisiblePosition = 5;
            ultraGridColumn90.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn90.Header.VisiblePosition = 6;
            ultraGridColumn91.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn91.Header.VisiblePosition = 7;
            ultraGridColumn92.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn92.Header.VisiblePosition = 8;
            ultraGridColumn93.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn93.Header.VisiblePosition = 9;
            ultraGridColumn94.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn94.Header.VisiblePosition = 10;
            ultraGridColumn95.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn95.Header.VisiblePosition = 11;
            ultraGridBand14.Columns.AddRange(new object[] {
            ultraGridColumn84,
            ultraGridColumn85,
            ultraGridColumn86,
            ultraGridColumn87,
            ultraGridColumn88,
            ultraGridColumn89,
            ultraGridColumn90,
            ultraGridColumn91,
            ultraGridColumn92,
            ultraGridColumn93,
            ultraGridColumn94,
            ultraGridColumn95});
            ultraGridColumn96.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn96.Header.VisiblePosition = 0;
            ultraGridColumn97.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn97.Header.VisiblePosition = 1;
            ultraGridColumn98.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn98.Header.VisiblePosition = 2;
            ultraGridColumn99.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn99.Header.VisiblePosition = 3;
            ultraGridColumn100.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn100.Header.VisiblePosition = 4;
            ultraGridColumn101.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn101.Header.VisiblePosition = 5;
            ultraGridColumn102.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn102.Header.VisiblePosition = 6;
            ultraGridColumn103.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn103.Header.VisiblePosition = 7;
            ultraGridColumn104.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn104.Header.VisiblePosition = 8;
            ultraGridColumn105.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn105.Header.VisiblePosition = 9;
            ultraGridColumn106.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn106.Header.VisiblePosition = 10;
            ultraGridBand15.Columns.AddRange(new object[] {
            ultraGridColumn96,
            ultraGridColumn97,
            ultraGridColumn98,
            ultraGridColumn99,
            ultraGridColumn100,
            ultraGridColumn101,
            ultraGridColumn102,
            ultraGridColumn103,
            ultraGridColumn104,
            ultraGridColumn105,
            ultraGridColumn106});
            ultraGridColumn107.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn107.Header.VisiblePosition = 0;
            ultraGridColumn108.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn108.Header.VisiblePosition = 1;
            ultraGridColumn109.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn109.Header.VisiblePosition = 2;
            ultraGridColumn110.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn110.Header.VisiblePosition = 3;
            ultraGridBand16.Columns.AddRange(new object[] {
            ultraGridColumn107,
            ultraGridColumn108,
            ultraGridColumn109,
            ultraGridColumn110});
            this.ultraCombo4.DisplayLayout.BandsSerializer.Add(ultraGridBand13);
            this.ultraCombo4.DisplayLayout.BandsSerializer.Add(ultraGridBand14);
            this.ultraCombo4.DisplayLayout.BandsSerializer.Add(ultraGridBand15);
            this.ultraCombo4.DisplayLayout.BandsSerializer.Add(ultraGridBand16);
            this.ultraCombo4.DisplayLayout.BandsSerializer.Add(ultraGridBand17);
            this.ultraCombo4.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraCombo4.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo4.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo4.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.ultraCombo4.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo4.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.ultraCombo4.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraCombo4.DisplayLayout.MaxRowScrollRegions = 1;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraCombo4.DisplayLayout.Override.ActiveCellAppearance = appearance12;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraCombo4.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.ultraCombo4.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraCombo4.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.ultraCombo4.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraCombo4.DisplayLayout.Override.CellAppearance = appearance5;
            this.ultraCombo4.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraCombo4.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo4.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance11.TextHAlignAsString = "Left";
            this.ultraCombo4.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.ultraCombo4.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraCombo4.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.ultraCombo4.DisplayLayout.Override.RowAppearance = appearance10;
            this.ultraCombo4.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraCombo4.DisplayLayout.Override.TemplateAddRowAppearance = appearance8;
            this.ultraCombo4.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraCombo4.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraCombo4.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraCombo4.DisplayMember = "facing";
            this.ultraCombo4.Location = new System.Drawing.Point(417, 84);
            this.ultraCombo4.Name = "ultraCombo4";
            this.ultraCombo4.Size = new System.Drawing.Size(106, 22);
            this.ultraCombo4.TabIndex = 59;
            this.ultraCombo4.Text = "ultraCombo4";
            this.ultraCombo4.ValueMember = "facing_type_id";
            // 
            // fKDITCHSURVEYPAGEBindingSource
            // 
            this.fKDITCHSURVEYPAGEBindingSource.DataMember = "FK_DITCH_SURVEY_PAGE";
            this.fKDITCHSURVEYPAGEBindingSource.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            // 
            // sWSPFACINGTYPEBindingSource
            // 
            this.sWSPFACINGTYPEBindingSource.DataMember = "SWSP_FACING_TYPE";
            this.sWSPFACINGTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // ultraCombo3
            // 
            this.ultraCombo3.CheckedListSettings.CheckStateMember = "";
            this.ultraCombo3.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "material", true));
            this.ultraCombo3.DataSource = this.sWSPMATERIALTYPEBindingSource;
            appearance49.BackColor = System.Drawing.SystemColors.Window;
            appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraCombo3.DisplayLayout.Appearance = appearance49;
            ultraGridColumn111.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn111.Header.VisiblePosition = 0;
            ultraGridColumn111.Hidden = true;
            ultraGridColumn112.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn112.Header.VisiblePosition = 1;
            ultraGridColumn113.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn113.Header.VisiblePosition = 2;
            ultraGridColumn113.Hidden = true;
            ultraGridColumn114.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn114.Header.VisiblePosition = 3;
            ultraGridColumn115.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn115.Header.VisiblePosition = 4;
            ultraGridColumn116.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn116.Header.VisiblePosition = 5;
            ultraGridBand18.Columns.AddRange(new object[] {
            ultraGridColumn111,
            ultraGridColumn112,
            ultraGridColumn113,
            ultraGridColumn114,
            ultraGridColumn115,
            ultraGridColumn116});
            ultraGridColumn117.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn117.Header.VisiblePosition = 0;
            ultraGridColumn118.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn118.Header.VisiblePosition = 1;
            ultraGridColumn119.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn119.Header.VisiblePosition = 2;
            ultraGridColumn120.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn120.Header.VisiblePosition = 3;
            ultraGridColumn121.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn121.Header.VisiblePosition = 4;
            ultraGridColumn122.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn122.Header.VisiblePosition = 5;
            ultraGridColumn123.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn123.Header.VisiblePosition = 6;
            ultraGridColumn124.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn124.Header.VisiblePosition = 7;
            ultraGridColumn125.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn125.Header.VisiblePosition = 8;
            ultraGridColumn126.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn126.Header.VisiblePosition = 9;
            ultraGridColumn127.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn127.Header.VisiblePosition = 10;
            ultraGridColumn128.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn128.Header.VisiblePosition = 11;
            ultraGridBand19.Columns.AddRange(new object[] {
            ultraGridColumn117,
            ultraGridColumn118,
            ultraGridColumn119,
            ultraGridColumn120,
            ultraGridColumn121,
            ultraGridColumn122,
            ultraGridColumn123,
            ultraGridColumn124,
            ultraGridColumn125,
            ultraGridColumn126,
            ultraGridColumn127,
            ultraGridColumn128});
            ultraGridColumn129.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn129.Header.VisiblePosition = 0;
            ultraGridColumn130.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn130.Header.VisiblePosition = 1;
            ultraGridColumn131.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn131.Header.VisiblePosition = 2;
            ultraGridColumn132.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn132.Header.VisiblePosition = 3;
            ultraGridColumn133.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn133.Header.VisiblePosition = 4;
            ultraGridColumn134.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn134.Header.VisiblePosition = 5;
            ultraGridColumn135.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn135.Header.VisiblePosition = 6;
            ultraGridColumn136.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn136.Header.VisiblePosition = 7;
            ultraGridColumn137.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn137.Header.VisiblePosition = 8;
            ultraGridColumn138.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn138.Header.VisiblePosition = 9;
            ultraGridColumn139.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn139.Header.VisiblePosition = 10;
            ultraGridBand20.Columns.AddRange(new object[] {
            ultraGridColumn129,
            ultraGridColumn130,
            ultraGridColumn131,
            ultraGridColumn132,
            ultraGridColumn133,
            ultraGridColumn134,
            ultraGridColumn135,
            ultraGridColumn136,
            ultraGridColumn137,
            ultraGridColumn138,
            ultraGridColumn139});
            ultraGridColumn140.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn140.Header.VisiblePosition = 0;
            ultraGridColumn141.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn141.Header.VisiblePosition = 1;
            ultraGridColumn142.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn142.Header.VisiblePosition = 2;
            ultraGridColumn143.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn143.Header.VisiblePosition = 3;
            ultraGridColumn144.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn144.Header.VisiblePosition = 4;
            ultraGridColumn145.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn145.Header.VisiblePosition = 5;
            ultraGridColumn146.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn146.Header.VisiblePosition = 6;
            ultraGridColumn147.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn147.Header.VisiblePosition = 7;
            ultraGridColumn148.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn148.Header.VisiblePosition = 8;
            ultraGridColumn149.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn149.Header.VisiblePosition = 9;
            ultraGridColumn150.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn150.Header.VisiblePosition = 10;
            ultraGridColumn151.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn151.Header.VisiblePosition = 11;
            ultraGridBand21.Columns.AddRange(new object[] {
            ultraGridColumn140,
            ultraGridColumn141,
            ultraGridColumn142,
            ultraGridColumn143,
            ultraGridColumn144,
            ultraGridColumn145,
            ultraGridColumn146,
            ultraGridColumn147,
            ultraGridColumn148,
            ultraGridColumn149,
            ultraGridColumn150,
            ultraGridColumn151});
            ultraGridColumn152.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn152.Header.VisiblePosition = 0;
            ultraGridColumn153.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn153.Header.VisiblePosition = 1;
            ultraGridColumn154.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn154.Header.VisiblePosition = 2;
            ultraGridColumn155.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn155.Header.VisiblePosition = 3;
            ultraGridBand22.Columns.AddRange(new object[] {
            ultraGridColumn152,
            ultraGridColumn153,
            ultraGridColumn154,
            ultraGridColumn155});
            this.ultraCombo3.DisplayLayout.BandsSerializer.Add(ultraGridBand18);
            this.ultraCombo3.DisplayLayout.BandsSerializer.Add(ultraGridBand19);
            this.ultraCombo3.DisplayLayout.BandsSerializer.Add(ultraGridBand20);
            this.ultraCombo3.DisplayLayout.BandsSerializer.Add(ultraGridBand21);
            this.ultraCombo3.DisplayLayout.BandsSerializer.Add(ultraGridBand22);
            this.ultraCombo3.DisplayLayout.BandsSerializer.Add(ultraGridBand23);
            this.ultraCombo3.DisplayLayout.BandsSerializer.Add(ultraGridBand24);
            this.ultraCombo3.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraCombo3.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance46.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo3.DisplayLayout.GroupByBox.Appearance = appearance46;
            appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo3.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
            this.ultraCombo3.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance48.BackColor2 = System.Drawing.SystemColors.Control;
            appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo3.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
            this.ultraCombo3.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraCombo3.DisplayLayout.MaxRowScrollRegions = 1;
            appearance57.BackColor = System.Drawing.SystemColors.Window;
            appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraCombo3.DisplayLayout.Override.ActiveCellAppearance = appearance57;
            appearance52.BackColor = System.Drawing.SystemColors.Highlight;
            appearance52.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraCombo3.DisplayLayout.Override.ActiveRowAppearance = appearance52;
            this.ultraCombo3.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraCombo3.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance51.BackColor = System.Drawing.SystemColors.Window;
            this.ultraCombo3.DisplayLayout.Override.CardAreaAppearance = appearance51;
            appearance50.BorderColor = System.Drawing.Color.Silver;
            appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraCombo3.DisplayLayout.Override.CellAppearance = appearance50;
            this.ultraCombo3.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraCombo3.DisplayLayout.Override.CellPadding = 0;
            appearance54.BackColor = System.Drawing.SystemColors.Control;
            appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance54.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance54.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo3.DisplayLayout.Override.GroupByRowAppearance = appearance54;
            appearance56.TextHAlignAsString = "Left";
            this.ultraCombo3.DisplayLayout.Override.HeaderAppearance = appearance56;
            this.ultraCombo3.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraCombo3.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance55.BackColor = System.Drawing.SystemColors.Window;
            appearance55.BorderColor = System.Drawing.Color.Silver;
            this.ultraCombo3.DisplayLayout.Override.RowAppearance = appearance55;
            this.ultraCombo3.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance53.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraCombo3.DisplayLayout.Override.TemplateAddRowAppearance = appearance53;
            this.ultraCombo3.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraCombo3.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraCombo3.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraCombo3.DisplayMember = "material";
            this.ultraCombo3.Location = new System.Drawing.Point(869, 84);
            this.ultraCombo3.Name = "ultraCombo3";
            this.ultraCombo3.Size = new System.Drawing.Size(107, 22);
            this.ultraCombo3.TabIndex = 63;
            this.ultraCombo3.Text = "ultraCombo3";
            this.ultraCombo3.ValueMember = "material_type_id";
            // 
            // ultraNumericEditorDitchesBottomWidth
            // 
            this.ultraNumericEditorDitchesBottomWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "bottom_width_in", true));
            this.ultraNumericEditorDitchesBottomWidth.Location = new System.Drawing.Point(756, 84);
            this.ultraNumericEditorDitchesBottomWidth.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorDitchesBottomWidth.Name = "ultraNumericEditorDitchesBottomWidth";
            this.ultraNumericEditorDitchesBottomWidth.Nullable = true;
            this.ultraNumericEditorDitchesBottomWidth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorDitchesBottomWidth.Size = new System.Drawing.Size(107, 21);
            this.ultraNumericEditorDitchesBottomWidth.TabIndex = 62;
            // 
            // ultraNumericEditorDitchesTopWidth
            // 
            this.ultraNumericEditorDitchesTopWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "top_width_in", true));
            this.ultraNumericEditorDitchesTopWidth.Location = new System.Drawing.Point(643, 84);
            this.ultraNumericEditorDitchesTopWidth.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorDitchesTopWidth.Name = "ultraNumericEditorDitchesTopWidth";
            this.ultraNumericEditorDitchesTopWidth.Nullable = true;
            this.ultraNumericEditorDitchesTopWidth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorDitchesTopWidth.Size = new System.Drawing.Size(107, 21);
            this.ultraNumericEditorDitchesTopWidth.TabIndex = 61;
            // 
            // ultraNumericEditorDitchesDepth
            // 
            this.ultraNumericEditorDitchesDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "depth_in", true));
            this.ultraNumericEditorDitchesDepth.Location = new System.Drawing.Point(530, 83);
            this.ultraNumericEditorDitchesDepth.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorDitchesDepth.Name = "ultraNumericEditorDitchesDepth";
            this.ultraNumericEditorDitchesDepth.Nullable = true;
            this.ultraNumericEditorDitchesDepth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorDitchesDepth.Size = new System.Drawing.Size(107, 21);
            this.ultraNumericEditorDitchesDepth.TabIndex = 60;
            // 
            // buttonUpdateDitch
            // 
            this.buttonUpdateDitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateDitch.Location = new System.Drawing.Point(879, 136);
            this.buttonUpdateDitch.Name = "buttonUpdateDitch";
            this.buttonUpdateDitch.Size = new System.Drawing.Size(97, 28);
            this.buttonUpdateDitch.TabIndex = 65;
            this.buttonUpdateDitch.Text = "Update Ditch";
            this.buttonUpdateDitch.UseVisualStyleBackColor = true;
            this.buttonUpdateDitch.Click += new System.EventHandler(this.buttonUpdateDitch_Click);
            // 
            // buttonDitchesViewAddPhotos
            // 
            this.buttonDitchesViewAddPhotos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDitchesViewAddPhotos.Location = new System.Drawing.Point(491, 136);
            this.buttonDitchesViewAddPhotos.Name = "buttonDitchesViewAddPhotos";
            this.buttonDitchesViewAddPhotos.Size = new System.Drawing.Size(220, 28);
            this.buttonDitchesViewAddPhotos.TabIndex = 64;
            this.buttonDitchesViewAddPhotos.Text = "View/Add Photos";
            this.buttonDitchesViewAddPhotos.UseVisualStyleBackColor = true;
            this.buttonDitchesViewAddPhotos.Click += new System.EventHandler(this.buttonDitchesViewAddPhotos_Click);
            // 
            // textBoxDitchesNode
            // 
            this.textBoxDitchesNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKDITCHSURVEYPAGEBindingSource, "node", true));
            this.textBoxDitchesNode.Location = new System.Drawing.Point(304, 84);
            this.textBoxDitchesNode.Name = "textBoxDitchesNode";
            this.textBoxDitchesNode.Size = new System.Drawing.Size(107, 20);
            this.textBoxDitchesNode.TabIndex = 58;
            this.textBoxDitchesNode.TextChanged += new System.EventHandler(this.textBoxDitchesNode_TextChanged);
            this.textBoxDitchesNode.Enter += new System.EventHandler(this.textBoxDitchesNode_Enter);
            // 
            // labelDitchesNode
            // 
            this.labelDitchesNode.AutoSize = true;
            this.labelDitchesNode.Location = new System.Drawing.Point(301, 67);
            this.labelDitchesNode.Name = "labelDitchesNode";
            this.labelDitchesNode.Size = new System.Drawing.Size(33, 13);
            this.labelDitchesNode.TabIndex = 56;
            this.labelDitchesNode.Text = "Node";
            // 
            // labelDitchesFacingDirection
            // 
            this.labelDitchesFacingDirection.AutoSize = true;
            this.labelDitchesFacingDirection.Location = new System.Drawing.Point(414, 67);
            this.labelDitchesFacingDirection.Name = "labelDitchesFacingDirection";
            this.labelDitchesFacingDirection.Size = new System.Drawing.Size(84, 13);
            this.labelDitchesFacingDirection.TabIndex = 55;
            this.labelDitchesFacingDirection.Text = "Facing Direction";
            // 
            // labelDitchesMaterial
            // 
            this.labelDitchesMaterial.AutoSize = true;
            this.labelDitchesMaterial.Location = new System.Drawing.Point(866, 68);
            this.labelDitchesMaterial.Name = "labelDitchesMaterial";
            this.labelDitchesMaterial.Size = new System.Drawing.Size(44, 13);
            this.labelDitchesMaterial.TabIndex = 53;
            this.labelDitchesMaterial.Text = "Material";
            // 
            // labelDitchesBottomWidth
            // 
            this.labelDitchesBottomWidth.AutoSize = true;
            this.labelDitchesBottomWidth.Location = new System.Drawing.Point(753, 68);
            this.labelDitchesBottomWidth.Name = "labelDitchesBottomWidth";
            this.labelDitchesBottomWidth.Size = new System.Drawing.Size(85, 13);
            this.labelDitchesBottomWidth.TabIndex = 51;
            this.labelDitchesBottomWidth.Text = "Bottom width (in)";
            // 
            // labelDitchesTopWidth
            // 
            this.labelDitchesTopWidth.AutoSize = true;
            this.labelDitchesTopWidth.Location = new System.Drawing.Point(640, 68);
            this.labelDitchesTopWidth.Name = "labelDitchesTopWidth";
            this.labelDitchesTopWidth.Size = new System.Drawing.Size(71, 13);
            this.labelDitchesTopWidth.TabIndex = 50;
            this.labelDitchesTopWidth.Text = "Top width (in)";
            // 
            // labelDitchesDepth
            // 
            this.labelDitchesDepth.AutoSize = true;
            this.labelDitchesDepth.Location = new System.Drawing.Point(527, 67);
            this.labelDitchesDepth.Name = "labelDitchesDepth";
            this.labelDitchesDepth.Size = new System.Drawing.Size(53, 13);
            this.labelDitchesDepth.TabIndex = 49;
            this.labelDitchesDepth.Text = "Depth (in)";
            // 
            // buttonDitchesAdd
            // 
            this.buttonDitchesAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDitchesAdd.Location = new System.Drawing.Point(257, 136);
            this.buttonDitchesAdd.Name = "buttonDitchesAdd";
            this.buttonDitchesAdd.Size = new System.Drawing.Size(65, 28);
            this.buttonDitchesAdd.TabIndex = 48;
            this.buttonDitchesAdd.Text = "Add";
            this.buttonDitchesAdd.UseVisualStyleBackColor = true;
            this.buttonDitchesAdd.Click += new System.EventHandler(this.buttonDitchesAdd_Click);
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
            // dataGridViewDitches
            // 
            this.dataGridViewDitches.AllowUserToAddRows = false;
            this.dataGridViewDitches.AllowUserToDeleteRows = false;
            this.dataGridViewDitches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewDitches.AutoGenerateColumns = false;
            this.dataGridViewDitches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDitches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.facing,
            this.nodeDataGridViewTextBoxColumn,
            this.facingDataGridViewTextBoxColumn});
            this.dataGridViewDitches.DataSource = this.fKDITCHSURVEYPAGEBindingSource;
            this.dataGridViewDitches.Location = new System.Drawing.Point(3, 8);
            this.dataGridViewDitches.Name = "dataGridViewDitches";
            this.dataGridViewDitches.ReadOnly = true;
            this.dataGridViewDitches.Size = new System.Drawing.Size(248, 156);
            this.dataGridViewDitches.TabIndex = 40;
            // 
            // facing
            // 
            this.facing.DataPropertyName = "facing";
            this.facing.DataSource = this.sWSPFACINGTYPEBindingSource1;
            this.facing.DisplayMember = "facing";
            this.facing.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.facing.HeaderText = "facing";
            this.facing.Name = "facing";
            this.facing.ReadOnly = true;
            this.facing.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.facing.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.facing.ValueMember = "facing_type_id";
            // 
            // sWSPFACINGTYPEBindingSource1
            // 
            this.sWSPFACINGTYPEBindingSource1.DataMember = "SWSP_FACING_TYPE";
            this.sWSPFACINGTYPEBindingSource1.DataSource = this.sANDBOXDataSet;
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
            this.facingDataGridViewTextBoxColumn.Visible = false;
            this.facingDataGridViewTextBoxColumn.Width = 80;
            // 
            // tabPageCulverts
            // 
            this.tabPageCulverts.Controls.Add(this.ultraCombo7);
            this.tabPageCulverts.Controls.Add(this.ultraCombo5);
            this.tabPageCulverts.Controls.Add(this.ultraCombo2);
            this.tabPageCulverts.Controls.Add(this.ultraNumericEditorCulvertsUnobstructedHeight);
            this.tabPageCulverts.Controls.Add(this.ultraNumericEditorCulvertsFullDepth);
            this.tabPageCulverts.Controls.Add(this.buttonUpdateCulvert);
            this.tabPageCulverts.Controls.Add(this.buttonCulvertsViewAddPhotos);
            this.tabPageCulverts.Controls.Add(this.comboBoxCulvertsType);
            this.tabPageCulverts.Controls.Add(this.textBoxCulvertsNode);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsNode);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsFacingDirection);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsMaterial);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsUnobstructedHeight);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsFullDepth);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsShape);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsType);
            this.tabPageCulverts.Controls.Add(this.buttonCulvertsAdd);
            this.tabPageCulverts.Controls.Add(this.buttonCulvertsDelete);
            this.tabPageCulverts.Controls.Add(this.dataGridViewCulverts);
            this.tabPageCulverts.Location = new System.Drawing.Point(4, 22);
            this.tabPageCulverts.Name = "tabPageCulverts";
            this.tabPageCulverts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCulverts.Size = new System.Drawing.Size(984, 170);
            this.tabPageCulverts.TabIndex = 1;
            this.tabPageCulverts.Text = "Culverts";
            this.tabPageCulverts.UseVisualStyleBackColor = true;
            this.tabPageCulverts.Click += new System.EventHandler(this.tabPageCulverts_Click);
            this.tabPageCulverts.Enter += new System.EventHandler(this.tabPageCulverts_Enter);
            // 
            // ultraCombo7
            // 
            this.ultraCombo7.CheckedListSettings.CheckStateMember = "";
            this.ultraCombo7.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "shape", true));
            this.ultraCombo7.DataSource = this.sWSPSHAPETYPEBindingSource;
            appearance76.BackColor = System.Drawing.SystemColors.Window;
            appearance76.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraCombo7.DisplayLayout.Appearance = appearance76;
            ultraGridColumn156.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn156.Header.VisiblePosition = 0;
            ultraGridColumn156.Hidden = true;
            ultraGridColumn157.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn157.Header.VisiblePosition = 1;
            ultraGridColumn158.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn158.Header.VisiblePosition = 2;
            ultraGridColumn158.Hidden = true;
            ultraGridColumn159.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn159.Header.VisiblePosition = 3;
            ultraGridColumn160.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn160.Header.VisiblePosition = 4;
            ultraGridBand25.Columns.AddRange(new object[] {
            ultraGridColumn156,
            ultraGridColumn157,
            ultraGridColumn158,
            ultraGridColumn159,
            ultraGridColumn160});
            ultraGridColumn161.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn161.Header.VisiblePosition = 0;
            ultraGridColumn162.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn162.Header.VisiblePosition = 1;
            ultraGridColumn163.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn163.Header.VisiblePosition = 2;
            ultraGridColumn164.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn164.Header.VisiblePosition = 3;
            ultraGridColumn165.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn165.Header.VisiblePosition = 4;
            ultraGridColumn166.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn166.Header.VisiblePosition = 5;
            ultraGridColumn167.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn167.Header.VisiblePosition = 6;
            ultraGridColumn168.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn168.Header.VisiblePosition = 7;
            ultraGridColumn169.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn169.Header.VisiblePosition = 8;
            ultraGridColumn170.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn170.Header.VisiblePosition = 9;
            ultraGridColumn171.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn171.Header.VisiblePosition = 10;
            ultraGridColumn172.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn172.Header.VisiblePosition = 11;
            ultraGridBand26.Columns.AddRange(new object[] {
            ultraGridColumn161,
            ultraGridColumn162,
            ultraGridColumn163,
            ultraGridColumn164,
            ultraGridColumn165,
            ultraGridColumn166,
            ultraGridColumn167,
            ultraGridColumn168,
            ultraGridColumn169,
            ultraGridColumn170,
            ultraGridColumn171,
            ultraGridColumn172});
            ultraGridColumn173.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn173.Header.VisiblePosition = 0;
            ultraGridColumn174.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn174.Header.VisiblePosition = 1;
            ultraGridColumn175.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn175.Header.VisiblePosition = 2;
            ultraGridColumn176.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn176.Header.VisiblePosition = 3;
            ultraGridColumn177.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn177.Header.VisiblePosition = 4;
            ultraGridColumn178.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn178.Header.VisiblePosition = 5;
            ultraGridColumn179.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn179.Header.VisiblePosition = 6;
            ultraGridColumn180.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn180.Header.VisiblePosition = 7;
            ultraGridColumn181.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn181.Header.VisiblePosition = 8;
            ultraGridColumn182.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn182.Header.VisiblePosition = 9;
            ultraGridColumn183.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn183.Header.VisiblePosition = 10;
            ultraGridColumn184.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn184.Header.VisiblePosition = 11;
            ultraGridBand27.Columns.AddRange(new object[] {
            ultraGridColumn173,
            ultraGridColumn174,
            ultraGridColumn175,
            ultraGridColumn176,
            ultraGridColumn177,
            ultraGridColumn178,
            ultraGridColumn179,
            ultraGridColumn180,
            ultraGridColumn181,
            ultraGridColumn182,
            ultraGridColumn183,
            ultraGridColumn184});
            ultraGridColumn185.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn185.Header.VisiblePosition = 0;
            ultraGridColumn186.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn186.Header.VisiblePosition = 1;
            ultraGridColumn187.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn187.Header.VisiblePosition = 2;
            ultraGridColumn188.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn188.Header.VisiblePosition = 3;
            ultraGridBand28.Columns.AddRange(new object[] {
            ultraGridColumn185,
            ultraGridColumn186,
            ultraGridColumn187,
            ultraGridColumn188});
            this.ultraCombo7.DisplayLayout.BandsSerializer.Add(ultraGridBand25);
            this.ultraCombo7.DisplayLayout.BandsSerializer.Add(ultraGridBand26);
            this.ultraCombo7.DisplayLayout.BandsSerializer.Add(ultraGridBand27);
            this.ultraCombo7.DisplayLayout.BandsSerializer.Add(ultraGridBand28);
            this.ultraCombo7.DisplayLayout.BandsSerializer.Add(ultraGridBand29);
            this.ultraCombo7.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraCombo7.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance73.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance73.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo7.DisplayLayout.GroupByBox.Appearance = appearance73;
            appearance74.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo7.DisplayLayout.GroupByBox.BandLabelAppearance = appearance74;
            this.ultraCombo7.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance75.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance75.BackColor2 = System.Drawing.SystemColors.Control;
            appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo7.DisplayLayout.GroupByBox.PromptAppearance = appearance75;
            this.ultraCombo7.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraCombo7.DisplayLayout.MaxRowScrollRegions = 1;
            appearance84.BackColor = System.Drawing.SystemColors.Window;
            appearance84.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraCombo7.DisplayLayout.Override.ActiveCellAppearance = appearance84;
            appearance79.BackColor = System.Drawing.SystemColors.Highlight;
            appearance79.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraCombo7.DisplayLayout.Override.ActiveRowAppearance = appearance79;
            this.ultraCombo7.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraCombo7.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance78.BackColor = System.Drawing.SystemColors.Window;
            this.ultraCombo7.DisplayLayout.Override.CardAreaAppearance = appearance78;
            appearance77.BorderColor = System.Drawing.Color.Silver;
            appearance77.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraCombo7.DisplayLayout.Override.CellAppearance = appearance77;
            this.ultraCombo7.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraCombo7.DisplayLayout.Override.CellPadding = 0;
            appearance81.BackColor = System.Drawing.SystemColors.Control;
            appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance81.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo7.DisplayLayout.Override.GroupByRowAppearance = appearance81;
            appearance83.TextHAlignAsString = "Left";
            this.ultraCombo7.DisplayLayout.Override.HeaderAppearance = appearance83;
            this.ultraCombo7.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraCombo7.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance82.BackColor = System.Drawing.SystemColors.Window;
            appearance82.BorderColor = System.Drawing.Color.Silver;
            this.ultraCombo7.DisplayLayout.Override.RowAppearance = appearance82;
            this.ultraCombo7.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraCombo7.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
            this.ultraCombo7.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraCombo7.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraCombo7.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraCombo7.DisplayMember = "shape";
            this.ultraCombo7.Location = new System.Drawing.Point(881, 82);
            this.ultraCombo7.Name = "ultraCombo7";
            this.ultraCombo7.Size = new System.Drawing.Size(95, 22);
            this.ultraCombo7.TabIndex = 49;
            this.ultraCombo7.Text = "ultraCombo7";
            this.ultraCombo7.ValueMember = "shape_type_id";
            // 
            // fKCULVERTSURVEYPAGEBindingSource1
            // 
            this.fKCULVERTSURVEYPAGEBindingSource1.DataMember = "FK_CULVERT_SURVEY_PAGE";
            this.fKCULVERTSURVEYPAGEBindingSource1.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            // 
            // ultraCombo5
            // 
            this.ultraCombo5.CheckedListSettings.CheckStateMember = "";
            this.ultraCombo5.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "facing", true));
            this.ultraCombo5.DataSource = this.sWSPFACINGTYPEBindingSource;
            appearance16.BackColor = System.Drawing.SystemColors.Window;
            appearance16.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraCombo5.DisplayLayout.Appearance = appearance16;
            ultraGridColumn189.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn189.Header.VisiblePosition = 0;
            ultraGridColumn189.Hidden = true;
            ultraGridColumn190.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn190.Header.VisiblePosition = 1;
            ultraGridColumn191.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn191.Header.VisiblePosition = 2;
            ultraGridColumn191.Hidden = true;
            ultraGridColumn192.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn192.Header.VisiblePosition = 3;
            ultraGridColumn193.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn193.Header.VisiblePosition = 4;
            ultraGridBand30.Columns.AddRange(new object[] {
            ultraGridColumn189,
            ultraGridColumn190,
            ultraGridColumn191,
            ultraGridColumn192,
            ultraGridColumn193});
            ultraGridColumn194.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn194.Header.VisiblePosition = 0;
            ultraGridColumn195.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn195.Header.VisiblePosition = 1;
            ultraGridColumn196.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn196.Header.VisiblePosition = 2;
            ultraGridColumn197.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn197.Header.VisiblePosition = 3;
            ultraGridColumn198.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn198.Header.VisiblePosition = 4;
            ultraGridColumn199.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn199.Header.VisiblePosition = 5;
            ultraGridColumn200.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn200.Header.VisiblePosition = 6;
            ultraGridColumn201.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn201.Header.VisiblePosition = 7;
            ultraGridColumn202.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn202.Header.VisiblePosition = 8;
            ultraGridColumn203.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn203.Header.VisiblePosition = 9;
            ultraGridColumn204.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn204.Header.VisiblePosition = 10;
            ultraGridColumn205.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn205.Header.VisiblePosition = 11;
            ultraGridBand31.Columns.AddRange(new object[] {
            ultraGridColumn194,
            ultraGridColumn195,
            ultraGridColumn196,
            ultraGridColumn197,
            ultraGridColumn198,
            ultraGridColumn199,
            ultraGridColumn200,
            ultraGridColumn201,
            ultraGridColumn202,
            ultraGridColumn203,
            ultraGridColumn204,
            ultraGridColumn205});
            ultraGridColumn206.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn206.Header.VisiblePosition = 0;
            ultraGridColumn207.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn207.Header.VisiblePosition = 1;
            ultraGridColumn208.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn208.Header.VisiblePosition = 2;
            ultraGridColumn209.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn209.Header.VisiblePosition = 3;
            ultraGridColumn210.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn210.Header.VisiblePosition = 4;
            ultraGridColumn211.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn211.Header.VisiblePosition = 5;
            ultraGridColumn212.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn212.Header.VisiblePosition = 6;
            ultraGridColumn213.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn213.Header.VisiblePosition = 7;
            ultraGridColumn214.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn214.Header.VisiblePosition = 8;
            ultraGridColumn215.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn215.Header.VisiblePosition = 9;
            ultraGridColumn216.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn216.Header.VisiblePosition = 10;
            ultraGridBand32.Columns.AddRange(new object[] {
            ultraGridColumn206,
            ultraGridColumn207,
            ultraGridColumn208,
            ultraGridColumn209,
            ultraGridColumn210,
            ultraGridColumn211,
            ultraGridColumn212,
            ultraGridColumn213,
            ultraGridColumn214,
            ultraGridColumn215,
            ultraGridColumn216});
            ultraGridColumn217.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn217.Header.VisiblePosition = 0;
            ultraGridColumn218.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn218.Header.VisiblePosition = 1;
            ultraGridColumn219.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn219.Header.VisiblePosition = 2;
            ultraGridColumn220.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn220.Header.VisiblePosition = 3;
            ultraGridBand33.Columns.AddRange(new object[] {
            ultraGridColumn217,
            ultraGridColumn218,
            ultraGridColumn219,
            ultraGridColumn220});
            this.ultraCombo5.DisplayLayout.BandsSerializer.Add(ultraGridBand30);
            this.ultraCombo5.DisplayLayout.BandsSerializer.Add(ultraGridBand31);
            this.ultraCombo5.DisplayLayout.BandsSerializer.Add(ultraGridBand32);
            this.ultraCombo5.DisplayLayout.BandsSerializer.Add(ultraGridBand33);
            this.ultraCombo5.DisplayLayout.BandsSerializer.Add(ultraGridBand34);
            this.ultraCombo5.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraCombo5.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance13.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance13.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo5.DisplayLayout.GroupByBox.Appearance = appearance13;
            appearance14.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo5.DisplayLayout.GroupByBox.BandLabelAppearance = appearance14;
            this.ultraCombo5.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance15.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance15.BackColor2 = System.Drawing.SystemColors.Control;
            appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo5.DisplayLayout.GroupByBox.PromptAppearance = appearance15;
            this.ultraCombo5.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraCombo5.DisplayLayout.MaxRowScrollRegions = 1;
            appearance60.BackColor = System.Drawing.SystemColors.Window;
            appearance60.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraCombo5.DisplayLayout.Override.ActiveCellAppearance = appearance60;
            appearance22.BackColor = System.Drawing.SystemColors.Highlight;
            appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraCombo5.DisplayLayout.Override.ActiveRowAppearance = appearance22;
            this.ultraCombo5.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraCombo5.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance18.BackColor = System.Drawing.SystemColors.Window;
            this.ultraCombo5.DisplayLayout.Override.CardAreaAppearance = appearance18;
            appearance17.BorderColor = System.Drawing.Color.Silver;
            appearance17.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraCombo5.DisplayLayout.Override.CellAppearance = appearance17;
            this.ultraCombo5.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraCombo5.DisplayLayout.Override.CellPadding = 0;
            appearance24.BackColor = System.Drawing.SystemColors.Control;
            appearance24.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance24.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance24.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo5.DisplayLayout.Override.GroupByRowAppearance = appearance24;
            appearance59.TextHAlignAsString = "Left";
            this.ultraCombo5.DisplayLayout.Override.HeaderAppearance = appearance59;
            this.ultraCombo5.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraCombo5.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance58.BackColor = System.Drawing.SystemColors.Window;
            appearance58.BorderColor = System.Drawing.Color.Silver;
            this.ultraCombo5.DisplayLayout.Override.RowAppearance = appearance58;
            this.ultraCombo5.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance23.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraCombo5.DisplayLayout.Override.TemplateAddRowAppearance = appearance23;
            this.ultraCombo5.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraCombo5.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraCombo5.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraCombo5.DisplayMember = "facing";
            this.ultraCombo5.Location = new System.Drawing.Point(361, 82);
            this.ultraCombo5.Name = "ultraCombo5";
            this.ultraCombo5.Size = new System.Drawing.Size(89, 22);
            this.ultraCombo5.TabIndex = 44;
            this.ultraCombo5.Text = "ultraCombo5";
            this.ultraCombo5.ValueMember = "facing_type_id";
            // 
            // ultraCombo2
            // 
            this.ultraCombo2.CheckedListSettings.CheckStateMember = "";
            this.ultraCombo2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "material", true));
            this.ultraCombo2.DataSource = this.sWSPMATERIALTYPEBindingSource;
            appearance37.BackColor = System.Drawing.SystemColors.Window;
            appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraCombo2.DisplayLayout.Appearance = appearance37;
            ultraGridColumn221.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn221.Header.VisiblePosition = 0;
            ultraGridColumn221.Hidden = true;
            ultraGridColumn222.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn222.Header.VisiblePosition = 1;
            ultraGridColumn223.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn223.Header.VisiblePosition = 2;
            ultraGridColumn223.Hidden = true;
            ultraGridColumn224.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn224.Header.VisiblePosition = 3;
            ultraGridColumn225.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn225.Header.VisiblePosition = 4;
            ultraGridColumn226.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn226.Header.VisiblePosition = 5;
            ultraGridBand35.Columns.AddRange(new object[] {
            ultraGridColumn221,
            ultraGridColumn222,
            ultraGridColumn223,
            ultraGridColumn224,
            ultraGridColumn225,
            ultraGridColumn226});
            ultraGridColumn227.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn227.Header.VisiblePosition = 0;
            ultraGridColumn228.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn228.Header.VisiblePosition = 1;
            ultraGridColumn229.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn229.Header.VisiblePosition = 2;
            ultraGridColumn230.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn230.Header.VisiblePosition = 3;
            ultraGridColumn231.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn231.Header.VisiblePosition = 4;
            ultraGridColumn232.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn232.Header.VisiblePosition = 5;
            ultraGridColumn233.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn233.Header.VisiblePosition = 6;
            ultraGridColumn234.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn234.Header.VisiblePosition = 7;
            ultraGridColumn235.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn235.Header.VisiblePosition = 8;
            ultraGridColumn236.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn236.Header.VisiblePosition = 9;
            ultraGridColumn237.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn237.Header.VisiblePosition = 10;
            ultraGridColumn238.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn238.Header.VisiblePosition = 11;
            ultraGridBand36.Columns.AddRange(new object[] {
            ultraGridColumn227,
            ultraGridColumn228,
            ultraGridColumn229,
            ultraGridColumn230,
            ultraGridColumn231,
            ultraGridColumn232,
            ultraGridColumn233,
            ultraGridColumn234,
            ultraGridColumn235,
            ultraGridColumn236,
            ultraGridColumn237,
            ultraGridColumn238});
            ultraGridColumn239.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn239.Header.VisiblePosition = 0;
            ultraGridColumn240.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn240.Header.VisiblePosition = 1;
            ultraGridColumn241.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn241.Header.VisiblePosition = 2;
            ultraGridColumn242.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn242.Header.VisiblePosition = 3;
            ultraGridColumn243.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn243.Header.VisiblePosition = 4;
            ultraGridColumn244.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn244.Header.VisiblePosition = 5;
            ultraGridColumn245.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn245.Header.VisiblePosition = 6;
            ultraGridColumn246.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn246.Header.VisiblePosition = 7;
            ultraGridColumn247.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn247.Header.VisiblePosition = 8;
            ultraGridColumn248.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn248.Header.VisiblePosition = 9;
            ultraGridColumn249.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn249.Header.VisiblePosition = 10;
            ultraGridBand37.Columns.AddRange(new object[] {
            ultraGridColumn239,
            ultraGridColumn240,
            ultraGridColumn241,
            ultraGridColumn242,
            ultraGridColumn243,
            ultraGridColumn244,
            ultraGridColumn245,
            ultraGridColumn246,
            ultraGridColumn247,
            ultraGridColumn248,
            ultraGridColumn249});
            ultraGridColumn250.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn250.Header.VisiblePosition = 0;
            ultraGridColumn251.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn251.Header.VisiblePosition = 1;
            ultraGridColumn252.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn252.Header.VisiblePosition = 2;
            ultraGridColumn253.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn253.Header.VisiblePosition = 3;
            ultraGridColumn254.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn254.Header.VisiblePosition = 4;
            ultraGridColumn255.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn255.Header.VisiblePosition = 5;
            ultraGridColumn256.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn256.Header.VisiblePosition = 6;
            ultraGridColumn257.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn257.Header.VisiblePosition = 7;
            ultraGridColumn258.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn258.Header.VisiblePosition = 8;
            ultraGridColumn259.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn259.Header.VisiblePosition = 9;
            ultraGridColumn260.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn260.Header.VisiblePosition = 10;
            ultraGridColumn261.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn261.Header.VisiblePosition = 11;
            ultraGridBand38.Columns.AddRange(new object[] {
            ultraGridColumn250,
            ultraGridColumn251,
            ultraGridColumn252,
            ultraGridColumn253,
            ultraGridColumn254,
            ultraGridColumn255,
            ultraGridColumn256,
            ultraGridColumn257,
            ultraGridColumn258,
            ultraGridColumn259,
            ultraGridColumn260,
            ultraGridColumn261});
            ultraGridColumn262.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn262.Header.VisiblePosition = 0;
            ultraGridColumn263.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn263.Header.VisiblePosition = 1;
            ultraGridColumn264.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn264.Header.VisiblePosition = 2;
            ultraGridColumn265.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn265.Header.VisiblePosition = 3;
            ultraGridBand39.Columns.AddRange(new object[] {
            ultraGridColumn262,
            ultraGridColumn263,
            ultraGridColumn264,
            ultraGridColumn265});
            this.ultraCombo2.DisplayLayout.BandsSerializer.Add(ultraGridBand35);
            this.ultraCombo2.DisplayLayout.BandsSerializer.Add(ultraGridBand36);
            this.ultraCombo2.DisplayLayout.BandsSerializer.Add(ultraGridBand37);
            this.ultraCombo2.DisplayLayout.BandsSerializer.Add(ultraGridBand38);
            this.ultraCombo2.DisplayLayout.BandsSerializer.Add(ultraGridBand39);
            this.ultraCombo2.DisplayLayout.BandsSerializer.Add(ultraGridBand40);
            this.ultraCombo2.DisplayLayout.BandsSerializer.Add(ultraGridBand41);
            this.ultraCombo2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraCombo2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance34.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance34.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo2.DisplayLayout.GroupByBox.Appearance = appearance34;
            appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance35;
            this.ultraCombo2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance36.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance36.BackColor2 = System.Drawing.SystemColors.Control;
            appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance36.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraCombo2.DisplayLayout.GroupByBox.PromptAppearance = appearance36;
            this.ultraCombo2.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraCombo2.DisplayLayout.MaxRowScrollRegions = 1;
            appearance45.BackColor = System.Drawing.SystemColors.Window;
            appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraCombo2.DisplayLayout.Override.ActiveCellAppearance = appearance45;
            appearance40.BackColor = System.Drawing.SystemColors.Highlight;
            appearance40.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraCombo2.DisplayLayout.Override.ActiveRowAppearance = appearance40;
            this.ultraCombo2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraCombo2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance39.BackColor = System.Drawing.SystemColors.Window;
            this.ultraCombo2.DisplayLayout.Override.CardAreaAppearance = appearance39;
            appearance38.BorderColor = System.Drawing.Color.Silver;
            appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraCombo2.DisplayLayout.Override.CellAppearance = appearance38;
            this.ultraCombo2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraCombo2.DisplayLayout.Override.CellPadding = 0;
            appearance42.BackColor = System.Drawing.SystemColors.Control;
            appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance42.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance42.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraCombo2.DisplayLayout.Override.GroupByRowAppearance = appearance42;
            appearance44.TextHAlignAsString = "Left";
            this.ultraCombo2.DisplayLayout.Override.HeaderAppearance = appearance44;
            this.ultraCombo2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraCombo2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance43.BackColor = System.Drawing.SystemColors.Window;
            appearance43.BorderColor = System.Drawing.Color.Silver;
            this.ultraCombo2.DisplayLayout.Override.RowAppearance = appearance43;
            this.ultraCombo2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance41.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraCombo2.DisplayLayout.Override.TemplateAddRowAppearance = appearance41;
            this.ultraCombo2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraCombo2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraCombo2.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraCombo2.DisplayMember = "material";
            this.ultraCombo2.Location = new System.Drawing.Point(775, 81);
            this.ultraCombo2.Name = "ultraCombo2";
            this.ultraCombo2.Size = new System.Drawing.Size(100, 22);
            this.ultraCombo2.TabIndex = 48;
            this.ultraCombo2.Text = "ultraCombo2";
            this.ultraCombo2.ValueMember = "material_type_id";
            // 
            // ultraNumericEditorCulvertsUnobstructedHeight
            // 
            this.ultraNumericEditorCulvertsUnobstructedHeight.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "unobstructed_height_in", true));
            this.ultraNumericEditorCulvertsUnobstructedHeight.Location = new System.Drawing.Point(649, 82);
            this.ultraNumericEditorCulvertsUnobstructedHeight.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorCulvertsUnobstructedHeight.Name = "ultraNumericEditorCulvertsUnobstructedHeight";
            this.ultraNumericEditorCulvertsUnobstructedHeight.Nullable = true;
            this.ultraNumericEditorCulvertsUnobstructedHeight.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorCulvertsUnobstructedHeight.Size = new System.Drawing.Size(120, 21);
            this.ultraNumericEditorCulvertsUnobstructedHeight.TabIndex = 47;
            // 
            // ultraNumericEditorCulvertsFullDepth
            // 
            this.ultraNumericEditorCulvertsFullDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "full_diam_in", true));
            this.ultraNumericEditorCulvertsFullDepth.Location = new System.Drawing.Point(553, 81);
            this.ultraNumericEditorCulvertsFullDepth.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorCulvertsFullDepth.Name = "ultraNumericEditorCulvertsFullDepth";
            this.ultraNumericEditorCulvertsFullDepth.Nullable = true;
            this.ultraNumericEditorCulvertsFullDepth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorCulvertsFullDepth.Size = new System.Drawing.Size(90, 21);
            this.ultraNumericEditorCulvertsFullDepth.TabIndex = 46;
            // 
            // buttonUpdateCulvert
            // 
            this.buttonUpdateCulvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateCulvert.Location = new System.Drawing.Point(887, 142);
            this.buttonUpdateCulvert.Name = "buttonUpdateCulvert";
            this.buttonUpdateCulvert.Size = new System.Drawing.Size(97, 28);
            this.buttonUpdateCulvert.TabIndex = 66;
            this.buttonUpdateCulvert.Text = "Update Culvert";
            this.buttonUpdateCulvert.UseVisualStyleBackColor = true;
            this.buttonUpdateCulvert.Click += new System.EventHandler(this.buttonUpdateCulvert_Click);
            // 
            // buttonCulvertsViewAddPhotos
            // 
            this.buttonCulvertsViewAddPhotos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCulvertsViewAddPhotos.Location = new System.Drawing.Point(499, 142);
            this.buttonCulvertsViewAddPhotos.Name = "buttonCulvertsViewAddPhotos";
            this.buttonCulvertsViewAddPhotos.Size = new System.Drawing.Size(220, 28);
            this.buttonCulvertsViewAddPhotos.TabIndex = 65;
            this.buttonCulvertsViewAddPhotos.Text = "View/Add Photos";
            this.buttonCulvertsViewAddPhotos.UseVisualStyleBackColor = true;
            this.buttonCulvertsViewAddPhotos.Click += new System.EventHandler(this.buttonCulvertsViewAddPhotos_Click);
            // 
            // comboBoxCulvertsType
            // 
            this.comboBoxCulvertsType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.fKCULVERTSURVEYPAGEBindingSource1, "culvert_opening", true));
            this.comboBoxCulvertsType.DataSource = this.sWSPCULVERTOPENINGTYPEBindingSource;
            this.comboBoxCulvertsType.DisplayMember = "culvert_opening";
            this.comboBoxCulvertsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCulvertsType.FormattingEnabled = true;
            this.comboBoxCulvertsType.Location = new System.Drawing.Point(457, 80);
            this.comboBoxCulvertsType.Name = "comboBoxCulvertsType";
            this.comboBoxCulvertsType.Size = new System.Drawing.Size(90, 21);
            this.comboBoxCulvertsType.TabIndex = 45;
            this.comboBoxCulvertsType.ValueMember = "culvert_opening_type_id";
            // 
            // sWSPCULVERTOPENINGTYPEBindingSource
            // 
            this.sWSPCULVERTOPENINGTYPEBindingSource.DataMember = "SWSP_CULVERT_OPENING_TYPE";
            this.sWSPCULVERTOPENINGTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // textBoxCulvertsNode
            // 
            this.textBoxCulvertsNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKCULVERTSURVEYPAGEBindingSource1, "node", true));
            this.textBoxCulvertsNode.Location = new System.Drawing.Point(266, 81);
            this.textBoxCulvertsNode.Name = "textBoxCulvertsNode";
            this.textBoxCulvertsNode.Size = new System.Drawing.Size(90, 20);
            this.textBoxCulvertsNode.TabIndex = 43;
            this.textBoxCulvertsNode.TextChanged += new System.EventHandler(this.textBoxCulvertsNode_TextChanged);
            this.textBoxCulvertsNode.Enter += new System.EventHandler(this.textBoxCulvertsNode_Enter);
            // 
            // labelCulvertsNode
            // 
            this.labelCulvertsNode.AutoSize = true;
            this.labelCulvertsNode.Location = new System.Drawing.Point(263, 65);
            this.labelCulvertsNode.Name = "labelCulvertsNode";
            this.labelCulvertsNode.Size = new System.Drawing.Size(33, 13);
            this.labelCulvertsNode.TabIndex = 41;
            this.labelCulvertsNode.Text = "Node";
            // 
            // labelCulvertsFacingDirection
            // 
            this.labelCulvertsFacingDirection.AutoSize = true;
            this.labelCulvertsFacingDirection.Location = new System.Drawing.Point(358, 65);
            this.labelCulvertsFacingDirection.Name = "labelCulvertsFacingDirection";
            this.labelCulvertsFacingDirection.Size = new System.Drawing.Size(84, 13);
            this.labelCulvertsFacingDirection.TabIndex = 40;
            this.labelCulvertsFacingDirection.Text = "Facing Direction";
            // 
            // labelCulvertsMaterial
            // 
            this.labelCulvertsMaterial.AutoSize = true;
            this.labelCulvertsMaterial.Location = new System.Drawing.Point(772, 66);
            this.labelCulvertsMaterial.Name = "labelCulvertsMaterial";
            this.labelCulvertsMaterial.Size = new System.Drawing.Size(44, 13);
            this.labelCulvertsMaterial.TabIndex = 38;
            this.labelCulvertsMaterial.Text = "Material";
            // 
            // labelCulvertsUnobstructedHeight
            // 
            this.labelCulvertsUnobstructedHeight.AutoSize = true;
            this.labelCulvertsUnobstructedHeight.Location = new System.Drawing.Point(646, 66);
            this.labelCulvertsUnobstructedHeight.Name = "labelCulvertsUnobstructedHeight";
            this.labelCulvertsUnobstructedHeight.Size = new System.Drawing.Size(120, 13);
            this.labelCulvertsUnobstructedHeight.TabIndex = 37;
            this.labelCulvertsUnobstructedHeight.Text = "Unobstructed height (in)";
            // 
            // labelCulvertsFullDepth
            // 
            this.labelCulvertsFullDepth.AutoSize = true;
            this.labelCulvertsFullDepth.Location = new System.Drawing.Point(550, 66);
            this.labelCulvertsFullDepth.Name = "labelCulvertsFullDepth";
            this.labelCulvertsFullDepth.Size = new System.Drawing.Size(70, 13);
            this.labelCulvertsFullDepth.TabIndex = 36;
            this.labelCulvertsFullDepth.Text = "Full depth (in)";
            // 
            // labelCulvertsShape
            // 
            this.labelCulvertsShape.AutoSize = true;
            this.labelCulvertsShape.Location = new System.Drawing.Point(878, 66);
            this.labelCulvertsShape.Name = "labelCulvertsShape";
            this.labelCulvertsShape.Size = new System.Drawing.Size(38, 13);
            this.labelCulvertsShape.TabIndex = 35;
            this.labelCulvertsShape.Text = "Shape";
            // 
            // labelCulvertsType
            // 
            this.labelCulvertsType.AutoSize = true;
            this.labelCulvertsType.Location = new System.Drawing.Point(454, 64);
            this.labelCulvertsType.Name = "labelCulvertsType";
            this.labelCulvertsType.Size = new System.Drawing.Size(31, 13);
            this.labelCulvertsType.TabIndex = 34;
            this.labelCulvertsType.Text = "Type";
            // 
            // buttonCulvertsAdd
            // 
            this.buttonCulvertsAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCulvertsAdd.Location = new System.Drawing.Point(257, 142);
            this.buttonCulvertsAdd.Name = "buttonCulvertsAdd";
            this.buttonCulvertsAdd.Size = new System.Drawing.Size(65, 28);
            this.buttonCulvertsAdd.TabIndex = 30;
            this.buttonCulvertsAdd.Text = "Add";
            this.buttonCulvertsAdd.UseVisualStyleBackColor = true;
            this.buttonCulvertsAdd.Click += new System.EventHandler(this.buttonCulvertsAdd_Click);
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
            // dataGridViewCulverts
            // 
            this.dataGridViewCulverts.AllowUserToAddRows = false;
            this.dataGridViewCulverts.AllowUserToDeleteRows = false;
            this.dataGridViewCulverts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewCulverts.AutoGenerateColumns = false;
            this.dataGridViewCulverts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCulverts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nodeDataGridViewTextBoxColumn1,
            this.facingDataGridViewTextBoxColumn1,
            this.dataGridViewComboBoxColumn1});
            this.dataGridViewCulverts.DataSource = this.fKCULVERTSURVEYPAGEBindingSource1;
            this.dataGridViewCulverts.Location = new System.Drawing.Point(3, 8);
            this.dataGridViewCulverts.Name = "dataGridViewCulverts";
            this.dataGridViewCulverts.ReadOnly = true;
            this.dataGridViewCulverts.Size = new System.Drawing.Size(248, 159);
            this.dataGridViewCulverts.TabIndex = 21;
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
            this.facingDataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.DataPropertyName = "facing";
            this.dataGridViewComboBoxColumn1.DataSource = this.sWSPFACINGTYPEBindingSource1;
            this.dataGridViewComboBoxColumn1.DisplayMember = "facing";
            this.dataGridViewComboBoxColumn1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.dataGridViewComboBoxColumn1.HeaderText = "facing";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.ReadOnly = true;
            this.dataGridViewComboBoxColumn1.ValueMember = "facing_type_id";
            // 
            // fKCULVERTFACINGTYPEBindingSource
            // 
            this.fKCULVERTFACINGTYPEBindingSource.DataMember = "FK_CULVERT_FACING_TYPE";
            this.fKCULVERTFACINGTYPEBindingSource.DataSource = this.sWSPFACINGTYPEBindingSource;
            // 
            // menuStripMainForm
            // 
            this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainForm.Name = "menuStripMainForm";
            this.menuStripMainForm.Size = new System.Drawing.Size(992, 24);
            this.menuStripMainForm.TabIndex = 10;
            this.menuStripMainForm.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fIleToolStripMenuItem.Text = "FIle";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataAdministratorToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // dataAdministratorToolStripMenuItem
            // 
            this.dataAdministratorToolStripMenuItem.Name = "dataAdministratorToolStripMenuItem";
            this.dataAdministratorToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.dataAdministratorToolStripMenuItem.Text = "Data Administrator";
            this.dataAdministratorToolStripMenuItem.Click += new System.EventHandler(this.dataAdministratorToolStripMenuItem_Click);
            // 
            // labelView
            // 
            this.labelView.AutoSize = true;
            this.labelView.Location = new System.Drawing.Point(9, 27);
            this.labelView.Name = "labelView";
            this.labelView.Size = new System.Drawing.Size(28, 13);
            this.labelView.TabIndex = 11;
            this.labelView.Text = "Map";
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
            this.labelWatershed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWatershed.AutoSize = true;
            this.labelWatershed.Location = new System.Drawing.Point(761, 27);
            this.labelWatershed.Name = "labelWatershed";
            this.labelWatershed.Size = new System.Drawing.Size(59, 13);
            this.labelWatershed.TabIndex = 13;
            this.labelWatershed.Text = "Watershed";
            // 
            // labelSubwatershed
            // 
            this.labelSubwatershed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSubwatershed.AutoSize = true;
            this.labelSubwatershed.Location = new System.Drawing.Point(903, 31);
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
            this.labelSearchNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSearchNode.AutoSize = true;
            this.labelSearchNode.Location = new System.Drawing.Point(33, 484);
            this.labelSearchNode.Name = "labelSearchNode";
            this.labelSearchNode.Size = new System.Drawing.Size(33, 13);
            this.labelSearchNode.TabIndex = 41;
            this.labelSearchNode.Text = "Node";
            // 
            // labelComments
            // 
            this.labelComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelComments.AutoSize = true;
            this.labelComments.Location = new System.Drawing.Point(10, 331);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(56, 13);
            this.labelComments.TabIndex = 40;
            this.labelComments.Text = "Comments";
            // 
            // buttonFindNode
            // 
            this.buttonFindNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonFindNode.Location = new System.Drawing.Point(222, 478);
            this.buttonFindNode.Name = "buttonFindNode";
            this.buttonFindNode.Size = new System.Drawing.Size(92, 19);
            this.buttonFindNode.TabIndex = 33;
            this.buttonFindNode.Text = "Find";
            this.buttonFindNode.UseVisualStyleBackColor = true;
            this.buttonFindNode.Click += new System.EventHandler(this.buttonFindNode_Click);
            // 
            // textBoxFindNode
            // 
            this.textBoxFindNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFindNode.Location = new System.Drawing.Point(75, 478);
            this.textBoxFindNode.Name = "textBoxFindNode";
            this.textBoxFindNode.Size = new System.Drawing.Size(131, 20);
            this.textBoxFindNode.TabIndex = 32;
            // 
            // buttonUpdateDatabase
            // 
            this.buttonUpdateDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateDatabase.Location = new System.Drawing.Point(847, 471);
            this.buttonUpdateDatabase.Name = "buttonUpdateDatabase";
            this.buttonUpdateDatabase.Size = new System.Drawing.Size(133, 26);
            this.buttonUpdateDatabase.TabIndex = 31;
            this.buttonUpdateDatabase.Text = "Update Database";
            this.buttonUpdateDatabase.UseVisualStyleBackColor = true;
            this.buttonUpdateDatabase.Click += new System.EventHandler(this.buttonUpdateDatabase_Click);
            // 
            // textBoxComments
            // 
            this.textBoxComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComments.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKSURVEYPAGEVIEWBindingSource, "Comment", true));
            this.textBoxComments.Location = new System.Drawing.Point(12, 347);
            this.textBoxComments.Multiline = true;
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxComments.Size = new System.Drawing.Size(967, 118);
            this.textBoxComments.TabIndex = 6;
            // 
            // comboBoxWatershed
            // 
            this.comboBoxWatershed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWatershed.DataSource = this.sWSPWATERSHEDBindingSource;
            this.comboBoxWatershed.DisplayMember = "watershed";
            this.comboBoxWatershed.FormattingEnabled = true;
            this.comboBoxWatershed.Location = new System.Drawing.Point(710, 47);
            this.comboBoxWatershed.Name = "comboBoxWatershed";
            this.comboBoxWatershed.Size = new System.Drawing.Size(109, 21);
            this.comboBoxWatershed.TabIndex = 101;
            this.comboBoxWatershed.ValueMember = "watershed_id";
            // 
            // comboBoxSubwatershed
            // 
            this.comboBoxSubwatershed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSubwatershed.DataSource = this.fKSUBWATERSHEDWATERSHEDBindingSource;
            this.comboBoxSubwatershed.DisplayMember = "subwatershed";
            this.comboBoxSubwatershed.FormattingEnabled = true;
            this.comboBoxSubwatershed.Location = new System.Drawing.Point(857, 46);
            this.comboBoxSubwatershed.Name = "comboBoxSubwatershed";
            this.comboBoxSubwatershed.Size = new System.Drawing.Size(120, 21);
            this.comboBoxSubwatershed.TabIndex = 102;
            this.comboBoxSubwatershed.ValueMember = "subwatershed_id";
            // 
            // comboBoxView
            // 
            this.comboBoxView.DataSource = this.fKVIEWSUBWATERSHEDBindingSource;
            this.comboBoxView.DisplayMember = "view_number";
            this.comboBoxView.FormattingEnabled = true;
            this.comboBoxView.Location = new System.Drawing.Point(11, 49);
            this.comboBoxView.Name = "comboBoxView";
            this.comboBoxView.Size = new System.Drawing.Size(43, 21);
            this.comboBoxView.TabIndex = 1;
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
            this.comboBoxSurveyPage.TabIndex = 2;
            this.comboBoxSurveyPage.ValueMember = "survey_page_id";
            this.comboBoxSurveyPage.SelectedValueChanged += new System.EventHandler(this.CheckEvaluatorsAssociatedWithThisSurveyPage);
            // 
            // textBoxWeather
            // 
            this.textBoxWeather.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKSURVEYPAGEVIEWBindingSource, "weather", true));
            this.textBoxWeather.Location = new System.Drawing.Point(170, 99);
            this.textBoxWeather.Name = "textBoxWeather";
            this.textBoxWeather.Size = new System.Drawing.Size(72, 20);
            this.textBoxWeather.TabIndex = 4;
            // 
            // fKCULVERTFACINGTYPEBindingSource1
            // 
            this.fKCULVERTFACINGTYPEBindingSource1.DataMember = "FK_CULVERT_FACING_TYPE";
            this.fKCULVERTFACINGTYPEBindingSource1.DataSource = this.sWSPFACINGTYPEBindingSource;
            // 
            // fKPIPEMATERIALTYPEBindingSource
            // 
            this.fKPIPEMATERIALTYPEBindingSource.DataMember = "FK_PIPE_MATERIAL_TYPE";
            this.fKPIPEMATERIALTYPEBindingSource.DataSource = this.sWSPMATERIALTYPEBindingSource;
            // 
            // fKPIPEMATERIALTYPEBindingSource1
            // 
            this.fKPIPEMATERIALTYPEBindingSource1.DataMember = "FK_PIPE_MATERIAL_TYPE";
            this.fKPIPEMATERIALTYPEBindingSource1.DataSource = this.sWSPMATERIALTYPEBindingSource;
            // 
            // fKPIPEMATERIALTYPEBindingSource2
            // 
            this.fKPIPEMATERIALTYPEBindingSource2.DataMember = "FK_PIPE_MATERIAL_TYPE";
            this.fKPIPEMATERIALTYPEBindingSource2.DataSource = this.sWSPMATERIALTYPEBindingSource;
            // 
            // ultraDateTimeEditor1
            // 
            this.ultraDateTimeEditor1.DataBindings.Add(new System.Windows.Forms.Binding("DateTime", this.fKSURVEYPAGEVIEWBindingSource, "date", true));
            this.ultraDateTimeEditor1.Location = new System.Drawing.Point(11, 97);
            this.ultraDateTimeEditor1.Name = "ultraDateTimeEditor1";
            this.ultraDateTimeEditor1.Size = new System.Drawing.Size(144, 21);
            this.ultraDateTimeEditor1.TabIndex = 103;
            // 
            // checkedListBoxEvaluators
            // 
            this.checkedListBoxEvaluators.DataSource = this.sANDBOXDataSet.SWSP_EVALUATOR;
            this.checkedListBoxEvaluators.DisplayMember = "Initials";
            this.checkedListBoxEvaluators.FormattingEnabled = true;
            this.checkedListBoxEvaluators.Location = new System.Drawing.Point(264, 99);
            this.checkedListBoxEvaluators.Name = "checkedListBoxEvaluators";
            this.checkedListBoxEvaluators.Size = new System.Drawing.Size(96, 49);
            this.checkedListBoxEvaluators.TabIndex = 5;
            this.checkedListBoxEvaluators.ValueMember = "evaluator_id";
            this.checkedListBoxEvaluators.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxEvaluators_SelectedIndexChanged);
            // 
            // sWSPPHOTOBindingSource
            // 
            this.sWSPPHOTOBindingSource.DataMember = "SWSP_PHOTO";
            this.sWSPPHOTOBindingSource.DataSource = this.sANDBOXDataSet;
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
            // sWSPEVALUATORBindingSource
            // 
            this.sWSPEVALUATORBindingSource.DataMember = "SWSP_EVALUATOR";
            this.sWSPEVALUATORBindingSource.DataSource = this.sANDBOXDataSet;
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
            // relationalIDsTableAdapter
            // 
            this.relationalIDsTableAdapter.ClearBeforeFill = true;
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 501);
            this.Controls.Add(this.ultraDateTimeEditor1);
            this.Controls.Add(this.textBoxWeather);
            this.Controls.Add(this.checkedListBoxEvaluators);
            this.Controls.Add(this.comboBoxSurveyPage);
            this.Controls.Add(this.comboBoxView);
            this.Controls.Add(this.comboBoxSubwatershed);
            this.Controls.Add(this.comboBoxWatershed);
            this.Controls.Add(this.labelSearchNode);
            this.Controls.Add(this.labelEvaluators);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.labelWeather);
            this.Controls.Add(this.labelSurveyDate);
            this.Controls.Add(this.labelSubwatershed);
            this.Controls.Add(this.labelWatershed);
            this.Controls.Add(this.labelSurveyPage);
            this.Controls.Add(this.labelView);
            this.Controls.Add(this.tabControlDitchesCulvertsPipes);
            this.Controls.Add(this.buttonFindNode);
            this.Controls.Add(this.textBoxFindNode);
            this.Controls.Add(this.buttonUpdateDatabase);
            this.Controls.Add(this.buttonAddSurveyPage);
            this.Controls.Add(this.buttonAddView);
            this.Controls.Add(this.menuStripMainForm);
            this.Controls.Add(this.textBoxComments);
            this.MainMenuStrip = this.menuStripMainForm;
            this.Name = "FormMain";
            this.Text = "Stormwater Interface";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fKSURVEYPAGEVIEWBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKVIEWSUBWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).EndInit();
            this.tabControlDitchesCulvertsPipes.ResumeLayout(false);
            this.tabPagePipes.ResumeLayout(false);
            this.tabPagePipes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPESURVEYPAGEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSHAPETYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPMATERIALTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesInnerDiameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesDSDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesUSDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPipes)).EndInit();
            this.tabPageDitches.ResumeLayout(false);
            this.tabPageDitches.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKDITCHSURVEYPAGEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesBottomWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesTopWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDitches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource1)).EndInit();
            this.tabPageCulverts.ResumeLayout(false);
            this.tabPageCulverts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTSURVEYPAGEBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCombo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorCulvertsUnobstructedHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorCulvertsFullDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTOPENINGTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCulverts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTFACINGTYPEBindingSource)).EndInit();
            this.menuStripMainForm.ResumeLayout(false);
            this.menuStripMainForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTFACINGTYPEBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPEMATERIALTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPEMATERIALTYPEBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPEMATERIALTYPEBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDateTimeEditor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPPHOTOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPCULVERTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPPIPEBindingSource)).EndInit();
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
        private System.Windows.Forms.TabControl tabControlDitchesCulvertsPipes;
        private System.Windows.Forms.TabPage tabPageDitches;
        private System.Windows.Forms.TabPage tabPageCulverts;
        private System.Windows.Forms.MenuStrip menuStripMainForm;
        private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.Label labelView;
        private System.Windows.Forms.Label labelSurveyPage;
        private System.Windows.Forms.Label labelWatershed;
        private System.Windows.Forms.Label labelSubwatershed;
        private System.Windows.Forms.Label labelSurveyDate;
        private System.Windows.Forms.Label labelWeather;
        private System.Windows.Forms.Label labelEvaluators;
        private System.Windows.Forms.TabPage tabPagePipes;
        private System.Windows.Forms.Label labelSearchNode;
        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.Label labelCulvertsMaterial;
        private System.Windows.Forms.Label labelCulvertsUnobstructedHeight;
        private System.Windows.Forms.Label labelCulvertsFullDepth;
        private System.Windows.Forms.Label labelCulvertsShape;
        private System.Windows.Forms.Label labelCulvertsType;
        private System.Windows.Forms.Button buttonFindNode;
        private System.Windows.Forms.TextBox textBoxFindNode;
        private System.Windows.Forms.Button buttonUpdateDatabase;
        private System.Windows.Forms.Button buttonCulvertsAdd;
        private System.Windows.Forms.Button buttonCulvertsDelete;
        private System.Windows.Forms.DataGridView dataGridViewCulverts;
        private System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.Label labelDitchesMaterial;
        private System.Windows.Forms.Label labelDitchesBottomWidth;
        private System.Windows.Forms.Label labelDitchesTopWidth;
        private System.Windows.Forms.Label labelDitchesDepth;
        private System.Windows.Forms.Button buttonDitchesAdd;
        private System.Windows.Forms.Button buttonDitchesDelete;
        private System.Windows.Forms.DataGridView dataGridViewDitches;
        private System.Windows.Forms.TextBox textBoxCulvertsNode;
        private System.Windows.Forms.Label labelCulvertsNode;
        private System.Windows.Forms.Label labelCulvertsFacingDirection;
        private System.Windows.Forms.TextBox textBoxDitchesNode;
        private System.Windows.Forms.Label labelDitchesNode;
        private System.Windows.Forms.Label labelDitchesFacingDirection;
        private System.Windows.Forms.TextBox textBoxPipesUSNode;
        private System.Windows.Forms.Label labelPipesUSNode;
        private System.Windows.Forms.Label labelPipesDSNode;
        private System.Windows.Forms.Label labelMaterial;
        private System.Windows.Forms.Label labelPipesDSDepth;
        private System.Windows.Forms.Label labelPipesUSDepth;
        private System.Windows.Forms.Button buttonPipesAdd;
        private System.Windows.Forms.Button buttonPipesDelete;
        private System.Windows.Forms.DataGridView dataGridViewPipes;
        private System.Windows.Forms.TextBox textBoxPipesDSNode;
        private System.Windows.Forms.Label labelPipesInnerDiameter;
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
        private System.Windows.Forms.CheckedListBox checkedListBoxEvaluators;
        private System.Windows.Forms.BindingSource fKDITCHSURVEYPAGEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_DITCHTableAdapter sWSP_DITCHTableAdapter;
        private System.Windows.Forms.BindingSource sWSPDITCHBindingSource;
        private System.Windows.Forms.BindingSource sWSPFACINGTYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_FACING_TYPETableAdapter sWSP_FACING_TYPETableAdapter;
        private System.Windows.Forms.BindingSource sWSPMATERIALTYPEBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_MATERIAL_TYPETableAdapter sWSP_MATERIAL_TYPETableAdapter;
        private SWI_2.SANDBOXDataSetTableAdapters.RelationalIDsTableAdapter relationalIDsTableAdapter;
        private System.Windows.Forms.BindingSource sWSPPHOTOBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_PHOTOTableAdapter sWSP_PHOTOTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxCulvertsType;
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
        private System.Windows.Forms.BindingSource fKPIPESURVEYPAGEBindingSource;
        private System.Windows.Forms.Button buttonDitchesViewAddPhotos;
        private System.Windows.Forms.Button buttonCulvertsViewAddPhotos;
        private System.Windows.Forms.Button buttonPipesViewAddPhotos;
        private System.Windows.Forms.BindingSource sWSP_GLOBAL_IDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_GLOBAL_IDTableAdapter sWSP_GLOBAL_IDTableAdapter;
        private System.Windows.Forms.Button buttonUpdateDitch;
        private System.Windows.Forms.Button buttonUpdateCulvert;
        private System.Windows.Forms.Button buttonUpdatePipe;
        private System.Windows.Forms.TextBox textBoxWeather;
        private System.Windows.Forms.BindingSource sWSPFACINGTYPEBindingSource1;
        private System.Windows.Forms.BindingSource fKCULVERTFACINGTYPEBindingSource1;
        private System.Windows.Forms.ToolStripMenuItem dataAdministratorToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn facingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn facing;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn facingDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn usnodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dsnodeDataGridViewTextBoxColumn;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor ultraNumericEditorDitchesDepth;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor ultraNumericEditorDitchesBottomWidth;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor ultraNumericEditorDitchesTopWidth;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor ultraNumericEditorCulvertsUnobstructedHeight;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor ultraNumericEditorCulvertsFullDepth;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor ultraNumericEditorPipesUSDepth;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor ultraNumericEditorPipesInnerDiameter;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor ultraNumericEditorPipesDSDepth;
        private System.Windows.Forms.BindingSource fKPIPEMATERIALTYPEBindingSource;
        private System.Windows.Forms.BindingSource fKPIPEMATERIALTYPEBindingSource1;
        private System.Windows.Forms.BindingSource fKPIPEMATERIALTYPEBindingSource2;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraCombo1;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraCombo2;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraCombo3;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraCombo4;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraCombo5;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraCombo6;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraCombo7;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor ultraDateTimeEditor1;
    }
}

