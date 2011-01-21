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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn369 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn370 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn371 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn372 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_SHAPE_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn373 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_SHAPE_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_SHAPE_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn374 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn375 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn376 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn377 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn378 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn379 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn380 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn381 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn382 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn383 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn384 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn385 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn386 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn387 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn388 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn389 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn390 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn391 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn392 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn393 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_SHAPE_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn394 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn395 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn396 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn397 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn398 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn399 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn400 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn401 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn402 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn403 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn404 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn405 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn406 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn407 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn408 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn409 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 3);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn410 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn411 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn412 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn413 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn414 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn415 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn416 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn417 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn418 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn419 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand7 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn420 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn421 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn422 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn423 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn424 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn425 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn426 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn427 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn428 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn429 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn430 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn431 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn432 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn433 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn434 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn435 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand8 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn436 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn437 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn438 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn439 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand9 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn440 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn441 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn442 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn443 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn444 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn445 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn446 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn447 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn448 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn449 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn450 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn451 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn452 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn453 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand10 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 3);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn454 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn455 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn456 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn457 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand11 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn458 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn459 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn460 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn461 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn462 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn463 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn464 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn465 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn466 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn467 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn468 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn469 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn470 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn471 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn472 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn473 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand12 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 5);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn474 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn475 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn476 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn477 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn478 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn479 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn480 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn481 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_FACING_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn482 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_FACING_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand14 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_FACING_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn483 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn484 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn485 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn486 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn487 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn488 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn489 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn490 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn491 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn492 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn493 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn494 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn495 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn496 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand15 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn497 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn498 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn499 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn500 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand16 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_FACING_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn501 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn502 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn503 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn504 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn505 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn506 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn507 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn508 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn509 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn510 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn511 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn512 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn513 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn514 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn515 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn516 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand17 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 3);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn517 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn518 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn519 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn520 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn521 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn522 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn523 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn524 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn525 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn526 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand19 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn527 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn528 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn529 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn530 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn531 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn532 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn533 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn534 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn535 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn536 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn537 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn538 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn539 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn540 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn541 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn542 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand20 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn543 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn544 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn545 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn546 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand21 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn547 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn548 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn549 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn550 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn551 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn552 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn553 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn554 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn555 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn556 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn557 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn558 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn559 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn560 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand22 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 3);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn561 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn562 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn563 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn564 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand23 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn565 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn566 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn567 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn568 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn569 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn570 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn571 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn572 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn573 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn574 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn575 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn576 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn577 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn578 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn579 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn580 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand24 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 5);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn581 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn582 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn583 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn584 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn585 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn586 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn587 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn588 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_SHAPE_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn589 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_SHAPE_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand26 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_SHAPE_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn590 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn591 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn592 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn593 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn594 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn595 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn596 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn597 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn598 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn599 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn600 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn601 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn602 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn603 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn604 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn605 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand27 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn606 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn607 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn608 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn609 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand28 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_SHAPE_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn610 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn611 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn612 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn613 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn614 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn615 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn616 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn617 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn618 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn619 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn620 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn621 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn622 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn623 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn624 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn625 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand29 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 3);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn626 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn627 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn628 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn629 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn630 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn631 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn632 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn633 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_FACING_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn634 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_FACING_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand31 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_FACING_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn635 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn636 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn637 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn638 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn639 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn640 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn641 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn642 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn643 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn644 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn645 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn646 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn647 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn648 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand32 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn649 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn650 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn651 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn652 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand33 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_FACING_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn653 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn654 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn655 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn656 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn657 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn658 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn659 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn660 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn661 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn662 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn663 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn664 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn665 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn666 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn667 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn668 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand34 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 3);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn669 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn670 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn671 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn672 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn673 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material_type_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn674 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn675 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn676 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PIPE_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn677 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_DITCH_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn678 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_CULVERT_MATERIAL_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand36 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PIPE_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn679 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("pipe_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn680 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn681 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn682 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn683 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn684 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn685 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn686 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn687 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn688 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn689 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn690 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("inside_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn691 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn692 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn693 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn694 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_PIPE");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand37 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_PIPE", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn695 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn696 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn697 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn698 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand38 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_DITCH_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn699 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ditch_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn700 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn701 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn702 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn703 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn704 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("depth_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn705 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("top_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn706 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("bottom_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn707 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn708 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn709 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn710 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn711 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn712 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_DITCH");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand39 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_DITCH", 3);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn713 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn714 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn715 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn716 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand40 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_CULVERT_MATERIAL_TYPE", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn717 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn718 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn719 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("survey_page_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn720 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn721 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("facing");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn722 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("culvert_opening");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn723 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("shape");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn724 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_diam_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn725 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("unobstructed_height_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn726 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("material");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn727 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn728 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("full_width_in");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn729 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("us_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn730 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ds_node");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn731 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("length_ft");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn732 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_PHOTO_CULVERT");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand41 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_PHOTO_CULVERT", 5);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn733 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("photo_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn734 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("global_id");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn735 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn736 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("comment");
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
            this.ultraNumericEditorPipesInnerWidth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.fKPIPESURVEYPAGEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ultraLabelInnerWidth = new Infragistics.Win.Misc.UltraLabel();
            this.ultraComboPipesShape = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.sWSPSHAPETYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ultraComboPipesMaterial = new Infragistics.Win.UltraWinGrid.UltraCombo();
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
            this.textBoxDitchesUSNode = new System.Windows.Forms.TextBox();
            this.fKDITCHSURVEYPAGEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelDitchesUSNode = new System.Windows.Forms.Label();
            this.textBoxDitchesDSNode = new System.Windows.Forms.TextBox();
            this.labelDitchesDSNode = new System.Windows.Forms.Label();
            this.ultraComboDitchesFacingDirection = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.sWSPFACINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ultraComboDitchesMaterial = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.ultraNumericEditorDitchesBottomWidth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.ultraNumericEditorDitchesTopWidth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.ultraNumericEditorDitchesDepth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.buttonUpdateDitch = new System.Windows.Forms.Button();
            this.buttonDitchesViewAddPhotos = new System.Windows.Forms.Button();
            this.textBoxDitchesMeasuredNode = new System.Windows.Forms.TextBox();
            this.labelDitchesMeasuredNode = new System.Windows.Forms.Label();
            this.labelDitchesFacingDirection = new System.Windows.Forms.Label();
            this.labelDitchesMaterial = new System.Windows.Forms.Label();
            this.labelDitchesBottomWidth = new System.Windows.Forms.Label();
            this.labelDitchesTopWidth = new System.Windows.Forms.Label();
            this.labelDitchesDepth = new System.Windows.Forms.Label();
            this.buttonDitchesAdd = new System.Windows.Forms.Button();
            this.buttonDitchesDelete = new System.Windows.Forms.Button();
            this.dataGridViewDitches = new System.Windows.Forms.DataGridView();
            this.us_node = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ds_node = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageCulverts = new System.Windows.Forms.TabPage();
            this.textBoxCulvertsDSNode = new System.Windows.Forms.TextBox();
            this.fKCULVERTSURVEYPAGEBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labelCulvertsDSNode = new System.Windows.Forms.Label();
            this.textBoxCulvertsUSNode = new System.Windows.Forms.TextBox();
            this.labelCulvertsUSNode = new System.Windows.Forms.Label();
            this.ultraComboCulvertsShape = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.ultraComboCulvertsFacingDirection = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.ultraComboCulvertsMaterial = new Infragistics.Win.UltraWinGrid.UltraCombo();
            this.ultraNumericEditorCulvertsUnobstructedHeight = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.ultraNumericEditorCulvertsFullDepth = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.buttonUpdateCulvert = new System.Windows.Forms.Button();
            this.buttonCulvertsViewAddPhotos = new System.Windows.Forms.Button();
            this.comboBoxCulvertsType = new System.Windows.Forms.ComboBox();
            this.sWSPCULVERTOPENINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBoxCulvertsMeasuredNode = new System.Windows.Forms.TextBox();
            this.labelCulvertsMeasuredNode = new System.Windows.Forms.Label();
            this.labelCulvertsFacingDirection = new System.Windows.Forms.Label();
            this.labelCulvertsMaterial = new System.Windows.Forms.Label();
            this.labelCulvertsUnobstructedHeight = new System.Windows.Forms.Label();
            this.labelCulvertsFullDepth = new System.Windows.Forms.Label();
            this.labelCulvertsShape = new System.Windows.Forms.Label();
            this.labelCulvertsType = new System.Windows.Forms.Label();
            this.buttonCulvertsAdd = new System.Windows.Forms.Button();
            this.buttonCulvertsDelete = new System.Windows.Forms.Button();
            this.dataGridViewCulverts = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fKCULVERTFACINGTYPEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMstlinksacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataAdministratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surveyViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.ultraDateTimeEditorSurveyDate = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sWSPGLOBALIDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.checkedListBoxEvaluators = new System.Windows.Forms.CheckedListBox();
            this.sWSPFACINGTYPEBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
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
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesInnerWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPESURVEYPAGEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboPipesShape)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSHAPETYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboPipesMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPMATERIALTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesInnerDiameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesDSDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesUSDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPipes)).BeginInit();
            this.tabPageDitches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKDITCHSURVEYPAGEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboDitchesFacingDirection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboDitchesMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesBottomWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesTopWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDitches)).BeginInit();
            this.tabPageCulverts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTSURVEYPAGEBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboCulvertsShape)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboCulvertsFacingDirection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboCulvertsMaterial)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.ultraDateTimeEditorSurveyDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPGLOBALIDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource1)).BeginInit();
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
            this.tabControlDitchesCulvertsPipes.Size = new System.Drawing.Size(1051, 196);
            this.tabControlDitchesCulvertsPipes.TabIndex = 9;
            this.tabControlDitchesCulvertsPipes.SelectedIndexChanged += new System.EventHandler(this.tabControlDitchesCulvertsPipes_SelectedIndexChanged);
            // 
            // tabPagePipes
            // 
            this.tabPagePipes.Controls.Add(this.ultraNumericEditorPipesInnerWidth);
            this.tabPagePipes.Controls.Add(this.ultraLabelInnerWidth);
            this.tabPagePipes.Controls.Add(this.ultraComboPipesShape);
            this.tabPagePipes.Controls.Add(this.ultraComboPipesMaterial);
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
            this.tabPagePipes.Size = new System.Drawing.Size(1043, 170);
            this.tabPagePipes.TabIndex = 2;
            this.tabPagePipes.Text = "Pipes";
            this.tabPagePipes.UseVisualStyleBackColor = true;
            this.tabPagePipes.Enter += new System.EventHandler(this.tabPagePipes_Enter);
            // 
            // ultraNumericEditorPipesInnerWidth
            // 
            this.ultraNumericEditorPipesInnerWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "inside_width_in", true));
            this.ultraNumericEditorPipesInnerWidth.Location = new System.Drawing.Point(543, 81);
            this.ultraNumericEditorPipesInnerWidth.MaskInput = "{LOC}nnnnnnn.nn";
            this.ultraNumericEditorPipesInnerWidth.Name = "ultraNumericEditorPipesInnerWidth";
            this.ultraNumericEditorPipesInnerWidth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorPipesInnerWidth.Size = new System.Drawing.Size(83, 21);
            this.ultraNumericEditorPipesInnerWidth.TabIndex = 65;
            this.ultraNumericEditorPipesInnerWidth.ValueChanged += new System.EventHandler(this.ultraNumericEditorPipesInnerWidth_ValueChanged);
            // 
            // fKPIPESURVEYPAGEBindingSource
            // 
            this.fKPIPESURVEYPAGEBindingSource.DataMember = "FK_PIPE_SURVEY_PAGE";
            this.fKPIPESURVEYPAGEBindingSource.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            // 
            // ultraLabelInnerWidth
            // 
            this.ultraLabelInnerWidth.Location = new System.Drawing.Point(543, 65);
            this.ultraLabelInnerWidth.Name = "ultraLabelInnerWidth";
            this.ultraLabelInnerWidth.Size = new System.Drawing.Size(85, 16);
            this.ultraLabelInnerWidth.TabIndex = 73;
            this.ultraLabelInnerWidth.Text = "Inner Height (in)";
            // 
            // ultraComboPipesShape
            // 
            this.ultraComboPipesShape.CheckedListSettings.CheckStateMember = "";
            this.ultraComboPipesShape.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "shape", true));
            this.ultraComboPipesShape.DataSource = this.sWSPSHAPETYPEBindingSource;
            appearance64.BackColor = System.Drawing.SystemColors.Window;
            appearance64.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraComboPipesShape.DisplayLayout.Appearance = appearance64;
            ultraGridColumn369.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn369.Header.VisiblePosition = 0;
            ultraGridColumn369.Hidden = true;
            ultraGridColumn370.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn370.Header.VisiblePosition = 1;
            ultraGridColumn371.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn371.Header.VisiblePosition = 2;
            ultraGridColumn371.Hidden = true;
            ultraGridColumn372.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn372.Header.VisiblePosition = 4;
            ultraGridColumn373.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn373.Header.VisiblePosition = 3;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn369,
            ultraGridColumn370,
            ultraGridColumn371,
            ultraGridColumn372,
            ultraGridColumn373});
            ultraGridColumn374.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn374.Header.VisiblePosition = 0;
            ultraGridColumn375.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn375.Header.VisiblePosition = 1;
            ultraGridColumn376.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn376.Header.VisiblePosition = 2;
            ultraGridColumn377.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn377.Header.VisiblePosition = 3;
            ultraGridColumn378.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn378.Header.VisiblePosition = 4;
            ultraGridColumn379.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn379.Header.VisiblePosition = 5;
            ultraGridColumn380.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn380.Header.VisiblePosition = 6;
            ultraGridColumn381.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn381.Header.VisiblePosition = 7;
            ultraGridColumn382.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn382.Header.VisiblePosition = 8;
            ultraGridColumn383.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn383.Header.VisiblePosition = 9;
            ultraGridColumn384.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn384.Header.VisiblePosition = 10;
            ultraGridColumn385.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn385.Header.VisiblePosition = 11;
            ultraGridColumn386.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn386.Header.VisiblePosition = 12;
            ultraGridColumn387.Header.VisiblePosition = 13;
            ultraGridColumn388.Header.VisiblePosition = 14;
            ultraGridColumn389.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn389.Header.VisiblePosition = 15;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn374,
            ultraGridColumn375,
            ultraGridColumn376,
            ultraGridColumn377,
            ultraGridColumn378,
            ultraGridColumn379,
            ultraGridColumn380,
            ultraGridColumn381,
            ultraGridColumn382,
            ultraGridColumn383,
            ultraGridColumn384,
            ultraGridColumn385,
            ultraGridColumn386,
            ultraGridColumn387,
            ultraGridColumn388,
            ultraGridColumn389});
            ultraGridColumn390.Header.VisiblePosition = 0;
            ultraGridColumn391.Header.VisiblePosition = 1;
            ultraGridColumn392.Header.VisiblePosition = 2;
            ultraGridColumn393.Header.VisiblePosition = 3;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn390,
            ultraGridColumn391,
            ultraGridColumn392,
            ultraGridColumn393});
            ultraGridColumn394.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn394.Header.VisiblePosition = 0;
            ultraGridColumn395.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn395.Header.VisiblePosition = 1;
            ultraGridColumn396.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn396.Header.VisiblePosition = 2;
            ultraGridColumn397.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn397.Header.VisiblePosition = 3;
            ultraGridColumn398.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn398.Header.VisiblePosition = 4;
            ultraGridColumn399.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn399.Header.VisiblePosition = 5;
            ultraGridColumn400.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn400.Header.VisiblePosition = 6;
            ultraGridColumn401.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn401.Header.VisiblePosition = 7;
            ultraGridColumn402.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn402.Header.VisiblePosition = 8;
            ultraGridColumn403.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn403.Header.VisiblePosition = 9;
            ultraGridColumn404.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn404.Header.VisiblePosition = 10;
            ultraGridColumn405.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn405.Header.VisiblePosition = 11;
            ultraGridColumn406.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn406.Header.VisiblePosition = 12;
            ultraGridColumn407.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn407.Header.VisiblePosition = 13;
            ultraGridColumn408.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn408.Header.VisiblePosition = 14;
            ultraGridColumn409.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn409.Header.VisiblePosition = 15;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn394,
            ultraGridColumn395,
            ultraGridColumn396,
            ultraGridColumn397,
            ultraGridColumn398,
            ultraGridColumn399,
            ultraGridColumn400,
            ultraGridColumn401,
            ultraGridColumn402,
            ultraGridColumn403,
            ultraGridColumn404,
            ultraGridColumn405,
            ultraGridColumn406,
            ultraGridColumn407,
            ultraGridColumn408,
            ultraGridColumn409});
            ultraGridColumn410.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn410.Header.VisiblePosition = 0;
            ultraGridColumn411.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn411.Header.VisiblePosition = 1;
            ultraGridColumn412.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn412.Header.VisiblePosition = 2;
            ultraGridColumn413.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn413.Header.VisiblePosition = 3;
            ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn410,
            ultraGridColumn411,
            ultraGridColumn412,
            ultraGridColumn413});
            this.ultraComboPipesShape.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraComboPipesShape.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.ultraComboPipesShape.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.ultraComboPipesShape.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.ultraComboPipesShape.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.ultraComboPipesShape.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraComboPipesShape.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance61.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance61.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboPipesShape.DisplayLayout.GroupByBox.Appearance = appearance61;
            appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboPipesShape.DisplayLayout.GroupByBox.BandLabelAppearance = appearance62;
            this.ultraComboPipesShape.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance63.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance63.BackColor2 = System.Drawing.SystemColors.Control;
            appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboPipesShape.DisplayLayout.GroupByBox.PromptAppearance = appearance63;
            this.ultraComboPipesShape.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraComboPipesShape.DisplayLayout.MaxRowScrollRegions = 1;
            appearance72.BackColor = System.Drawing.SystemColors.Window;
            appearance72.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraComboPipesShape.DisplayLayout.Override.ActiveCellAppearance = appearance72;
            appearance67.BackColor = System.Drawing.SystemColors.Highlight;
            appearance67.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraComboPipesShape.DisplayLayout.Override.ActiveRowAppearance = appearance67;
            this.ultraComboPipesShape.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraComboPipesShape.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance66.BackColor = System.Drawing.SystemColors.Window;
            this.ultraComboPipesShape.DisplayLayout.Override.CardAreaAppearance = appearance66;
            appearance65.BorderColor = System.Drawing.Color.Silver;
            appearance65.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraComboPipesShape.DisplayLayout.Override.CellAppearance = appearance65;
            this.ultraComboPipesShape.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraComboPipesShape.DisplayLayout.Override.CellPadding = 0;
            appearance69.BackColor = System.Drawing.SystemColors.Control;
            appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance69.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboPipesShape.DisplayLayout.Override.GroupByRowAppearance = appearance69;
            appearance71.TextHAlignAsString = "Left";
            this.ultraComboPipesShape.DisplayLayout.Override.HeaderAppearance = appearance71;
            this.ultraComboPipesShape.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraComboPipesShape.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance70.BackColor = System.Drawing.SystemColors.Window;
            appearance70.BorderColor = System.Drawing.Color.Silver;
            this.ultraComboPipesShape.DisplayLayout.Override.RowAppearance = appearance70;
            this.ultraComboPipesShape.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance68.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraComboPipesShape.DisplayLayout.Override.TemplateAddRowAppearance = appearance68;
            this.ultraComboPipesShape.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraComboPipesShape.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraComboPipesShape.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraComboPipesShape.DisplayMember = "shape";
            this.ultraComboPipesShape.Location = new System.Drawing.Point(738, 80);
            this.ultraComboPipesShape.Name = "ultraComboPipesShape";
            this.ultraComboPipesShape.Size = new System.Drawing.Size(100, 22);
            this.ultraComboPipesShape.TabIndex = 67;
            this.ultraComboPipesShape.Text = "ultraComboPipesShape";
            this.ultraComboPipesShape.ValueMember = "shape_type_id";
            // 
            // sWSPSHAPETYPEBindingSource
            // 
            this.sWSPSHAPETYPEBindingSource.DataMember = "SWSP_SHAPE_TYPE";
            this.sWSPSHAPETYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // ultraComboPipesMaterial
            // 
            this.ultraComboPipesMaterial.CheckedListSettings.CheckStateMember = "";
            this.ultraComboPipesMaterial.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "material", true));
            this.ultraComboPipesMaterial.DataSource = this.sWSPMATERIALTYPEBindingSource;
            appearance25.BackColor = System.Drawing.SystemColors.Window;
            appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraComboPipesMaterial.DisplayLayout.Appearance = appearance25;
            ultraGridColumn414.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn414.Header.VisiblePosition = 0;
            ultraGridColumn414.Hidden = true;
            ultraGridColumn415.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn415.Header.VisiblePosition = 1;
            ultraGridColumn416.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn416.Header.VisiblePosition = 2;
            ultraGridColumn416.Hidden = true;
            ultraGridColumn417.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn417.Header.VisiblePosition = 5;
            ultraGridColumn418.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn418.Header.VisiblePosition = 4;
            ultraGridColumn419.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn419.Header.VisiblePosition = 3;
            ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn414,
            ultraGridColumn415,
            ultraGridColumn416,
            ultraGridColumn417,
            ultraGridColumn418,
            ultraGridColumn419});
            ultraGridColumn420.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn420.Header.VisiblePosition = 0;
            ultraGridColumn421.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn421.Header.VisiblePosition = 1;
            ultraGridColumn422.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn422.Header.VisiblePosition = 2;
            ultraGridColumn423.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn423.Header.VisiblePosition = 3;
            ultraGridColumn424.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn424.Header.VisiblePosition = 4;
            ultraGridColumn425.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn425.Header.VisiblePosition = 5;
            ultraGridColumn426.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn426.Header.VisiblePosition = 6;
            ultraGridColumn427.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn427.Header.VisiblePosition = 7;
            ultraGridColumn428.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn428.Header.VisiblePosition = 8;
            ultraGridColumn429.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn429.Header.VisiblePosition = 9;
            ultraGridColumn430.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn430.Header.VisiblePosition = 10;
            ultraGridColumn431.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn431.Header.VisiblePosition = 11;
            ultraGridColumn432.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn432.Header.VisiblePosition = 12;
            ultraGridColumn433.Header.VisiblePosition = 13;
            ultraGridColumn434.Header.VisiblePosition = 14;
            ultraGridColumn435.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn435.Header.VisiblePosition = 15;
            ultraGridBand7.Columns.AddRange(new object[] {
            ultraGridColumn420,
            ultraGridColumn421,
            ultraGridColumn422,
            ultraGridColumn423,
            ultraGridColumn424,
            ultraGridColumn425,
            ultraGridColumn426,
            ultraGridColumn427,
            ultraGridColumn428,
            ultraGridColumn429,
            ultraGridColumn430,
            ultraGridColumn431,
            ultraGridColumn432,
            ultraGridColumn433,
            ultraGridColumn434,
            ultraGridColumn435});
            ultraGridColumn436.Header.VisiblePosition = 0;
            ultraGridColumn437.Header.VisiblePosition = 1;
            ultraGridColumn438.Header.VisiblePosition = 2;
            ultraGridColumn439.Header.VisiblePosition = 3;
            ultraGridBand8.Columns.AddRange(new object[] {
            ultraGridColumn436,
            ultraGridColumn437,
            ultraGridColumn438,
            ultraGridColumn439});
            ultraGridColumn440.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn440.Header.VisiblePosition = 0;
            ultraGridColumn441.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn441.Header.VisiblePosition = 1;
            ultraGridColumn442.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn442.Header.VisiblePosition = 2;
            ultraGridColumn443.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn443.Header.VisiblePosition = 3;
            ultraGridColumn444.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn444.Header.VisiblePosition = 4;
            ultraGridColumn445.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn445.Header.VisiblePosition = 5;
            ultraGridColumn446.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn446.Header.VisiblePosition = 6;
            ultraGridColumn447.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn447.Header.VisiblePosition = 7;
            ultraGridColumn448.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn448.Header.VisiblePosition = 8;
            ultraGridColumn449.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn449.Header.VisiblePosition = 9;
            ultraGridColumn450.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn450.Header.VisiblePosition = 10;
            ultraGridColumn451.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn451.Header.VisiblePosition = 11;
            ultraGridColumn452.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn452.Header.VisiblePosition = 12;
            ultraGridColumn453.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn453.Header.VisiblePosition = 13;
            ultraGridBand9.Columns.AddRange(new object[] {
            ultraGridColumn440,
            ultraGridColumn441,
            ultraGridColumn442,
            ultraGridColumn443,
            ultraGridColumn444,
            ultraGridColumn445,
            ultraGridColumn446,
            ultraGridColumn447,
            ultraGridColumn448,
            ultraGridColumn449,
            ultraGridColumn450,
            ultraGridColumn451,
            ultraGridColumn452,
            ultraGridColumn453});
            ultraGridColumn454.Header.VisiblePosition = 0;
            ultraGridColumn455.Header.VisiblePosition = 1;
            ultraGridColumn456.Header.VisiblePosition = 2;
            ultraGridColumn457.Header.VisiblePosition = 3;
            ultraGridBand10.Columns.AddRange(new object[] {
            ultraGridColumn454,
            ultraGridColumn455,
            ultraGridColumn456,
            ultraGridColumn457});
            ultraGridColumn458.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn458.Header.VisiblePosition = 0;
            ultraGridColumn459.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn459.Header.VisiblePosition = 1;
            ultraGridColumn460.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn460.Header.VisiblePosition = 2;
            ultraGridColumn461.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn461.Header.VisiblePosition = 3;
            ultraGridColumn462.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn462.Header.VisiblePosition = 4;
            ultraGridColumn463.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn463.Header.VisiblePosition = 5;
            ultraGridColumn464.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn464.Header.VisiblePosition = 6;
            ultraGridColumn465.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn465.Header.VisiblePosition = 7;
            ultraGridColumn466.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn466.Header.VisiblePosition = 8;
            ultraGridColumn467.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn467.Header.VisiblePosition = 9;
            ultraGridColumn468.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn468.Header.VisiblePosition = 10;
            ultraGridColumn469.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn469.Header.VisiblePosition = 11;
            ultraGridColumn470.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn470.Header.VisiblePosition = 12;
            ultraGridColumn471.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn471.Header.VisiblePosition = 13;
            ultraGridColumn472.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn472.Header.VisiblePosition = 14;
            ultraGridColumn473.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn473.Header.VisiblePosition = 15;
            ultraGridBand11.Columns.AddRange(new object[] {
            ultraGridColumn458,
            ultraGridColumn459,
            ultraGridColumn460,
            ultraGridColumn461,
            ultraGridColumn462,
            ultraGridColumn463,
            ultraGridColumn464,
            ultraGridColumn465,
            ultraGridColumn466,
            ultraGridColumn467,
            ultraGridColumn468,
            ultraGridColumn469,
            ultraGridColumn470,
            ultraGridColumn471,
            ultraGridColumn472,
            ultraGridColumn473});
            ultraGridColumn474.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn474.Header.VisiblePosition = 0;
            ultraGridColumn475.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn475.Header.VisiblePosition = 1;
            ultraGridColumn476.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn476.Header.VisiblePosition = 2;
            ultraGridColumn477.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn477.Header.VisiblePosition = 3;
            ultraGridBand12.Columns.AddRange(new object[] {
            ultraGridColumn474,
            ultraGridColumn475,
            ultraGridColumn476,
            ultraGridColumn477});
            this.ultraComboPipesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            this.ultraComboPipesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand7);
            this.ultraComboPipesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand8);
            this.ultraComboPipesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand9);
            this.ultraComboPipesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand10);
            this.ultraComboPipesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand11);
            this.ultraComboPipesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand12);
            this.ultraComboPipesMaterial.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraComboPipesMaterial.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance19.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance19.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance19.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboPipesMaterial.DisplayLayout.GroupByBox.Appearance = appearance19;
            appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboPipesMaterial.DisplayLayout.GroupByBox.BandLabelAppearance = appearance20;
            this.ultraComboPipesMaterial.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance21.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance21.BackColor2 = System.Drawing.SystemColors.Control;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboPipesMaterial.DisplayLayout.GroupByBox.PromptAppearance = appearance21;
            this.ultraComboPipesMaterial.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraComboPipesMaterial.DisplayLayout.MaxRowScrollRegions = 1;
            appearance33.BackColor = System.Drawing.SystemColors.Window;
            appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraComboPipesMaterial.DisplayLayout.Override.ActiveCellAppearance = appearance33;
            appearance28.BackColor = System.Drawing.SystemColors.Highlight;
            appearance28.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraComboPipesMaterial.DisplayLayout.Override.ActiveRowAppearance = appearance28;
            this.ultraComboPipesMaterial.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraComboPipesMaterial.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance27.BackColor = System.Drawing.SystemColors.Window;
            this.ultraComboPipesMaterial.DisplayLayout.Override.CardAreaAppearance = appearance27;
            appearance26.BorderColor = System.Drawing.Color.Silver;
            appearance26.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraComboPipesMaterial.DisplayLayout.Override.CellAppearance = appearance26;
            this.ultraComboPipesMaterial.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraComboPipesMaterial.DisplayLayout.Override.CellPadding = 0;
            appearance30.BackColor = System.Drawing.SystemColors.Control;
            appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance30.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance30.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboPipesMaterial.DisplayLayout.Override.GroupByRowAppearance = appearance30;
            appearance32.TextHAlignAsString = "Left";
            this.ultraComboPipesMaterial.DisplayLayout.Override.HeaderAppearance = appearance32;
            this.ultraComboPipesMaterial.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraComboPipesMaterial.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            appearance31.BorderColor = System.Drawing.Color.Silver;
            this.ultraComboPipesMaterial.DisplayLayout.Override.RowAppearance = appearance31;
            this.ultraComboPipesMaterial.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance29.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraComboPipesMaterial.DisplayLayout.Override.TemplateAddRowAppearance = appearance29;
            this.ultraComboPipesMaterial.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraComboPipesMaterial.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraComboPipesMaterial.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraComboPipesMaterial.DisplayMember = "material";
            this.ultraComboPipesMaterial.Location = new System.Drawing.Point(632, 81);
            this.ultraComboPipesMaterial.Name = "ultraComboPipesMaterial";
            this.ultraComboPipesMaterial.Size = new System.Drawing.Size(98, 22);
            this.ultraComboPipesMaterial.TabIndex = 666;
            this.ultraComboPipesMaterial.Text = "ultraComboPipesMaterial";
            this.ultraComboPipesMaterial.ValueMember = "material_type_id";
            // 
            // sWSPMATERIALTYPEBindingSource
            // 
            this.sWSPMATERIALTYPEBindingSource.DataMember = "SWSP_MATERIAL_TYPE";
            this.sWSPMATERIALTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // ultraNumericEditorPipesInnerDiameter
            // 
            this.ultraNumericEditorPipesInnerDiameter.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "inside_diam_in", true));
            this.ultraNumericEditorPipesInnerDiameter.Location = new System.Drawing.Point(450, 81);
            this.ultraNumericEditorPipesInnerDiameter.MaskInput = "{LOC}nnnnnnn.nn";
            this.ultraNumericEditorPipesInnerDiameter.Name = "ultraNumericEditorPipesInnerDiameter";
            this.ultraNumericEditorPipesInnerDiameter.Nullable = true;
            this.ultraNumericEditorPipesInnerDiameter.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorPipesInnerDiameter.Size = new System.Drawing.Size(90, 21);
            this.ultraNumericEditorPipesInnerDiameter.TabIndex = 64;
            // 
            // ultraNumericEditorPipesDSDepth
            // 
            this.ultraNumericEditorPipesDSDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "ds_depth_in", true));
            this.ultraNumericEditorPipesDSDepth.Location = new System.Drawing.Point(916, 81);
            this.ultraNumericEditorPipesDSDepth.MaskInput = "{LOC}-nnnn.nn";
            this.ultraNumericEditorPipesDSDepth.Name = "ultraNumericEditorPipesDSDepth";
            this.ultraNumericEditorPipesDSDepth.Nullable = true;
            this.ultraNumericEditorPipesDSDepth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorPipesDSDepth.Size = new System.Drawing.Size(68, 21);
            this.ultraNumericEditorPipesDSDepth.TabIndex = 69;
            // 
            // ultraNumericEditorPipesUSDepth
            // 
            this.ultraNumericEditorPipesUSDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKPIPESURVEYPAGEBindingSource, "us_depth_in", true));
            this.ultraNumericEditorPipesUSDepth.Location = new System.Drawing.Point(844, 81);
            this.ultraNumericEditorPipesUSDepth.MaskInput = "{LOC}-nnnn.nn";
            this.ultraNumericEditorPipesUSDepth.Name = "ultraNumericEditorPipesUSDepth";
            this.ultraNumericEditorPipesUSDepth.Nullable = true;
            this.ultraNumericEditorPipesUSDepth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorPipesUSDepth.Size = new System.Drawing.Size(66, 21);
            this.ultraNumericEditorPipesUSDepth.TabIndex = 68;
            // 
            // buttonUpdatePipe
            // 
            this.buttonUpdatePipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdatePipe.Location = new System.Drawing.Point(938, 139);
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
            this.buttonPipesViewAddPhotos.Location = new System.Drawing.Point(482, 139);
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
            this.labelPipesShape.Location = new System.Drawing.Point(735, 66);
            this.labelPipesShape.Name = "labelPipesShape";
            this.labelPipesShape.Size = new System.Drawing.Size(38, 13);
            this.labelPipesShape.TabIndex = 66;
            this.labelPipesShape.Text = "Shape";
            // 
            // labelPipesInnerDiameter
            // 
            this.labelPipesInnerDiameter.AutoSize = true;
            this.labelPipesInnerDiameter.Location = new System.Drawing.Point(447, 66);
            this.labelPipesInnerDiameter.Name = "labelPipesInnerDiameter";
            this.labelPipesInnerDiameter.Size = new System.Drawing.Size(93, 13);
            this.labelPipesInnerDiameter.TabIndex = 65;
            this.labelPipesInnerDiameter.Text = "Inner Diameter (in)";
            // 
            // textBoxPipesDSNode
            // 
            this.textBoxPipesDSNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKPIPESURVEYPAGEBindingSource, "ds_node", true));
            this.textBoxPipesDSNode.Location = new System.Drawing.Point(351, 82);
            this.textBoxPipesDSNode.Name = "textBoxPipesDSNode";
            this.textBoxPipesDSNode.Size = new System.Drawing.Size(90, 20);
            this.textBoxPipesDSNode.TabIndex = 63;
            this.textBoxPipesDSNode.TextChanged += new System.EventHandler(this.textBoxPipesDSNode_TextChanged);
            this.textBoxPipesDSNode.Enter += new System.EventHandler(this.textBoxPipesDSNode_Enter);
            // 
            // textBoxPipesUSNode
            // 
            this.textBoxPipesUSNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKPIPESURVEYPAGEBindingSource, "us_node", true));
            this.textBoxPipesUSNode.Location = new System.Drawing.Point(257, 82);
            this.textBoxPipesUSNode.Name = "textBoxPipesUSNode";
            this.textBoxPipesUSNode.Size = new System.Drawing.Size(90, 20);
            this.textBoxPipesUSNode.TabIndex = 62;
            this.textBoxPipesUSNode.TextChanged += new System.EventHandler(this.textBoxPipesUSNode_TextChanged);
            this.textBoxPipesUSNode.Enter += new System.EventHandler(this.textBoxPipesUSNode_Enter);
            // 
            // labelPipesUSNode
            // 
            this.labelPipesUSNode.AutoSize = true;
            this.labelPipesUSNode.Location = new System.Drawing.Point(257, 66);
            this.labelPipesUSNode.Name = "labelPipesUSNode";
            this.labelPipesUSNode.Size = new System.Drawing.Size(51, 13);
            this.labelPipesUSNode.TabIndex = 60;
            this.labelPipesUSNode.Text = "US Node";
            // 
            // labelPipesDSNode
            // 
            this.labelPipesDSNode.AutoSize = true;
            this.labelPipesDSNode.Location = new System.Drawing.Point(348, 65);
            this.labelPipesDSNode.Name = "labelPipesDSNode";
            this.labelPipesDSNode.Size = new System.Drawing.Size(51, 13);
            this.labelPipesDSNode.TabIndex = 59;
            this.labelPipesDSNode.Text = "DS Node";
            // 
            // labelMaterial
            // 
            this.labelMaterial.AutoSize = true;
            this.labelMaterial.Location = new System.Drawing.Point(629, 66);
            this.labelMaterial.Name = "labelMaterial";
            this.labelMaterial.Size = new System.Drawing.Size(44, 13);
            this.labelMaterial.TabIndex = 57;
            this.labelMaterial.Text = "Material";
            // 
            // labelPipesDSDepth
            // 
            this.labelPipesDSDepth.AutoSize = true;
            this.labelPipesDSDepth.Location = new System.Drawing.Point(913, 65);
            this.labelPipesDSDepth.Name = "labelPipesDSDepth";
            this.labelPipesDSDepth.Size = new System.Drawing.Size(71, 13);
            this.labelPipesDSDepth.TabIndex = 56;
            this.labelPipesDSDepth.Text = "DS Depth (in)";
            // 
            // labelPipesUSDepth
            // 
            this.labelPipesUSDepth.AutoSize = true;
            this.labelPipesUSDepth.Location = new System.Drawing.Point(841, 65);
            this.labelPipesUSDepth.Name = "labelPipesUSDepth";
            this.labelPipesUSDepth.Size = new System.Drawing.Size(69, 13);
            this.labelPipesUSDepth.TabIndex = 55;
            this.labelPipesUSDepth.Text = "US depth (in)";
            // 
            // buttonPipesAdd
            // 
            this.buttonPipesAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPipesAdd.Location = new System.Drawing.Point(257, 139);
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
            this.tabPageDitches.Controls.Add(this.textBoxDitchesUSNode);
            this.tabPageDitches.Controls.Add(this.labelDitchesUSNode);
            this.tabPageDitches.Controls.Add(this.textBoxDitchesDSNode);
            this.tabPageDitches.Controls.Add(this.labelDitchesDSNode);
            this.tabPageDitches.Controls.Add(this.ultraComboDitchesFacingDirection);
            this.tabPageDitches.Controls.Add(this.ultraComboDitchesMaterial);
            this.tabPageDitches.Controls.Add(this.ultraNumericEditorDitchesBottomWidth);
            this.tabPageDitches.Controls.Add(this.ultraNumericEditorDitchesTopWidth);
            this.tabPageDitches.Controls.Add(this.ultraNumericEditorDitchesDepth);
            this.tabPageDitches.Controls.Add(this.buttonUpdateDitch);
            this.tabPageDitches.Controls.Add(this.buttonDitchesViewAddPhotos);
            this.tabPageDitches.Controls.Add(this.textBoxDitchesMeasuredNode);
            this.tabPageDitches.Controls.Add(this.labelDitchesMeasuredNode);
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
            this.tabPageDitches.Size = new System.Drawing.Size(1043, 170);
            this.tabPageDitches.TabIndex = 0;
            this.tabPageDitches.Text = "Ditches";
            this.tabPageDitches.UseVisualStyleBackColor = true;
            this.tabPageDitches.Click += new System.EventHandler(this.tabPageDitches_Click);
            this.tabPageDitches.Enter += new System.EventHandler(this.tabPageDitches_Entered);
            // 
            // textBoxDitchesUSNode
            // 
            this.textBoxDitchesUSNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKDITCHSURVEYPAGEBindingSource, "us_node", true));
            this.textBoxDitchesUSNode.Location = new System.Drawing.Point(260, 81);
            this.textBoxDitchesUSNode.Name = "textBoxDitchesUSNode";
            this.textBoxDitchesUSNode.Size = new System.Drawing.Size(78, 20);
            this.textBoxDitchesUSNode.TabIndex = 56;
            this.textBoxDitchesUSNode.TextChanged += new System.EventHandler(this.textBoxDitchesUSNode_TextChanged);
            // 
            // fKDITCHSURVEYPAGEBindingSource
            // 
            this.fKDITCHSURVEYPAGEBindingSource.DataMember = "FK_DITCH_SURVEY_PAGE";
            this.fKDITCHSURVEYPAGEBindingSource.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            // 
            // labelDitchesUSNode
            // 
            this.labelDitchesUSNode.AutoSize = true;
            this.labelDitchesUSNode.Location = new System.Drawing.Point(257, 65);
            this.labelDitchesUSNode.Name = "labelDitchesUSNode";
            this.labelDitchesUSNode.Size = new System.Drawing.Size(49, 13);
            this.labelDitchesUSNode.TabIndex = 68;
            this.labelDitchesUSNode.Text = "US node";
            // 
            // textBoxDitchesDSNode
            // 
            this.textBoxDitchesDSNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKDITCHSURVEYPAGEBindingSource, "ds_node", true));
            this.textBoxDitchesDSNode.Location = new System.Drawing.Point(342, 81);
            this.textBoxDitchesDSNode.Name = "textBoxDitchesDSNode";
            this.textBoxDitchesDSNode.Size = new System.Drawing.Size(78, 20);
            this.textBoxDitchesDSNode.TabIndex = 57;
            this.textBoxDitchesDSNode.TextChanged += new System.EventHandler(this.textBoxDitchesDSNode_TextChanged);
            // 
            // labelDitchesDSNode
            // 
            this.labelDitchesDSNode.AutoSize = true;
            this.labelDitchesDSNode.Location = new System.Drawing.Point(339, 65);
            this.labelDitchesDSNode.Name = "labelDitchesDSNode";
            this.labelDitchesDSNode.Size = new System.Drawing.Size(49, 13);
            this.labelDitchesDSNode.TabIndex = 66;
            this.labelDitchesDSNode.Text = "DS node";
            // 
            // ultraComboDitchesFacingDirection
            // 
            this.ultraComboDitchesFacingDirection.CheckedListSettings.CheckStateMember = "";
            this.ultraComboDitchesFacingDirection.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "facing", true));
            this.ultraComboDitchesFacingDirection.DataSource = this.sWSPFACINGTYPEBindingSource;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Appearance = appearance4;
            ultraGridColumn478.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn478.Header.VisiblePosition = 0;
            ultraGridColumn478.Hidden = true;
            ultraGridColumn479.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn479.Header.VisiblePosition = 1;
            ultraGridColumn480.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn480.Header.VisiblePosition = 2;
            ultraGridColumn480.Hidden = true;
            ultraGridColumn481.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn481.Header.VisiblePosition = 4;
            ultraGridColumn482.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn482.Header.VisiblePosition = 3;
            ultraGridBand13.Columns.AddRange(new object[] {
            ultraGridColumn478,
            ultraGridColumn479,
            ultraGridColumn480,
            ultraGridColumn481,
            ultraGridColumn482});
            ultraGridColumn483.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn483.Header.VisiblePosition = 0;
            ultraGridColumn484.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn484.Header.VisiblePosition = 1;
            ultraGridColumn485.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn485.Header.VisiblePosition = 2;
            ultraGridColumn486.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn486.Header.VisiblePosition = 3;
            ultraGridColumn487.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn487.Header.VisiblePosition = 4;
            ultraGridColumn488.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn488.Header.VisiblePosition = 5;
            ultraGridColumn489.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn489.Header.VisiblePosition = 6;
            ultraGridColumn490.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn490.Header.VisiblePosition = 7;
            ultraGridColumn491.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn491.Header.VisiblePosition = 8;
            ultraGridColumn492.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn492.Header.VisiblePosition = 9;
            ultraGridColumn493.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn493.Header.VisiblePosition = 10;
            ultraGridColumn494.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn494.Header.VisiblePosition = 11;
            ultraGridColumn495.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn495.Header.VisiblePosition = 12;
            ultraGridColumn496.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn496.Header.VisiblePosition = 13;
            ultraGridBand14.Columns.AddRange(new object[] {
            ultraGridColumn483,
            ultraGridColumn484,
            ultraGridColumn485,
            ultraGridColumn486,
            ultraGridColumn487,
            ultraGridColumn488,
            ultraGridColumn489,
            ultraGridColumn490,
            ultraGridColumn491,
            ultraGridColumn492,
            ultraGridColumn493,
            ultraGridColumn494,
            ultraGridColumn495,
            ultraGridColumn496});
            ultraGridColumn497.Header.VisiblePosition = 0;
            ultraGridColumn498.Header.VisiblePosition = 1;
            ultraGridColumn499.Header.VisiblePosition = 2;
            ultraGridColumn500.Header.VisiblePosition = 3;
            ultraGridBand15.Columns.AddRange(new object[] {
            ultraGridColumn497,
            ultraGridColumn498,
            ultraGridColumn499,
            ultraGridColumn500});
            ultraGridColumn501.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn501.Header.VisiblePosition = 0;
            ultraGridColumn502.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn502.Header.VisiblePosition = 1;
            ultraGridColumn503.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn503.Header.VisiblePosition = 2;
            ultraGridColumn504.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn504.Header.VisiblePosition = 3;
            ultraGridColumn505.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn505.Header.VisiblePosition = 4;
            ultraGridColumn506.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn506.Header.VisiblePosition = 5;
            ultraGridColumn507.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn507.Header.VisiblePosition = 6;
            ultraGridColumn508.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn508.Header.VisiblePosition = 7;
            ultraGridColumn509.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn509.Header.VisiblePosition = 8;
            ultraGridColumn510.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn510.Header.VisiblePosition = 9;
            ultraGridColumn511.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn511.Header.VisiblePosition = 10;
            ultraGridColumn512.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn512.Header.VisiblePosition = 11;
            ultraGridColumn513.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn513.Header.VisiblePosition = 12;
            ultraGridColumn514.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn514.Header.VisiblePosition = 13;
            ultraGridColumn515.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn515.Header.VisiblePosition = 14;
            ultraGridColumn516.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn516.Header.VisiblePosition = 15;
            ultraGridBand16.Columns.AddRange(new object[] {
            ultraGridColumn501,
            ultraGridColumn502,
            ultraGridColumn503,
            ultraGridColumn504,
            ultraGridColumn505,
            ultraGridColumn506,
            ultraGridColumn507,
            ultraGridColumn508,
            ultraGridColumn509,
            ultraGridColumn510,
            ultraGridColumn511,
            ultraGridColumn512,
            ultraGridColumn513,
            ultraGridColumn514,
            ultraGridColumn515,
            ultraGridColumn516});
            ultraGridColumn517.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn517.Header.VisiblePosition = 0;
            ultraGridColumn518.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn518.Header.VisiblePosition = 1;
            ultraGridColumn519.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn519.Header.VisiblePosition = 2;
            ultraGridColumn520.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn520.Header.VisiblePosition = 3;
            ultraGridBand17.Columns.AddRange(new object[] {
            ultraGridColumn517,
            ultraGridColumn518,
            ultraGridColumn519,
            ultraGridColumn520});
            this.ultraComboDitchesFacingDirection.DisplayLayout.BandsSerializer.Add(ultraGridBand13);
            this.ultraComboDitchesFacingDirection.DisplayLayout.BandsSerializer.Add(ultraGridBand14);
            this.ultraComboDitchesFacingDirection.DisplayLayout.BandsSerializer.Add(ultraGridBand15);
            this.ultraComboDitchesFacingDirection.DisplayLayout.BandsSerializer.Add(ultraGridBand16);
            this.ultraComboDitchesFacingDirection.DisplayLayout.BandsSerializer.Add(ultraGridBand17);
            this.ultraComboDitchesFacingDirection.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraComboDitchesFacingDirection.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboDitchesFacingDirection.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboDitchesFacingDirection.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.ultraComboDitchesFacingDirection.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboDitchesFacingDirection.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.ultraComboDitchesFacingDirection.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraComboDitchesFacingDirection.DisplayLayout.MaxRowScrollRegions = 1;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.ActiveCellAppearance = appearance12;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.CellAppearance = appearance5;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance11.TextHAlignAsString = "Left";
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.RowAppearance = appearance10;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraComboDitchesFacingDirection.DisplayLayout.Override.TemplateAddRowAppearance = appearance8;
            this.ultraComboDitchesFacingDirection.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraComboDitchesFacingDirection.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraComboDitchesFacingDirection.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraComboDitchesFacingDirection.DisplayMember = "facing";
            this.ultraComboDitchesFacingDirection.Location = new System.Drawing.Point(417, 24);
            this.ultraComboDitchesFacingDirection.Name = "ultraComboDitchesFacingDirection";
            this.ultraComboDitchesFacingDirection.Size = new System.Drawing.Size(106, 22);
            this.ultraComboDitchesFacingDirection.TabIndex = 59;
            this.ultraComboDitchesFacingDirection.Text = "ultraComboDitchesFacingDirection";
            this.ultraComboDitchesFacingDirection.ValueMember = "facing_type_id";
            this.ultraComboDitchesFacingDirection.Visible = false;
            // 
            // sWSPFACINGTYPEBindingSource
            // 
            this.sWSPFACINGTYPEBindingSource.DataMember = "SWSP_FACING_TYPE";
            this.sWSPFACINGTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // ultraComboDitchesMaterial
            // 
            this.ultraComboDitchesMaterial.CheckedListSettings.CheckStateMember = "";
            this.ultraComboDitchesMaterial.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "material", true));
            this.ultraComboDitchesMaterial.DataSource = this.sWSPMATERIALTYPEBindingSource;
            appearance49.BackColor = System.Drawing.SystemColors.Window;
            appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraComboDitchesMaterial.DisplayLayout.Appearance = appearance49;
            ultraGridColumn521.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn521.Header.VisiblePosition = 0;
            ultraGridColumn521.Hidden = true;
            ultraGridColumn522.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn522.Header.VisiblePosition = 1;
            ultraGridColumn523.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn523.Header.VisiblePosition = 2;
            ultraGridColumn523.Hidden = true;
            ultraGridColumn524.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn524.Header.VisiblePosition = 5;
            ultraGridColumn525.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn525.Header.VisiblePosition = 4;
            ultraGridColumn526.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn526.Header.VisiblePosition = 3;
            ultraGridBand18.Columns.AddRange(new object[] {
            ultraGridColumn521,
            ultraGridColumn522,
            ultraGridColumn523,
            ultraGridColumn524,
            ultraGridColumn525,
            ultraGridColumn526});
            ultraGridColumn527.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn527.Header.VisiblePosition = 0;
            ultraGridColumn528.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn528.Header.VisiblePosition = 1;
            ultraGridColumn529.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn529.Header.VisiblePosition = 2;
            ultraGridColumn530.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn530.Header.VisiblePosition = 3;
            ultraGridColumn531.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn531.Header.VisiblePosition = 4;
            ultraGridColumn532.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn532.Header.VisiblePosition = 5;
            ultraGridColumn533.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn533.Header.VisiblePosition = 6;
            ultraGridColumn534.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn534.Header.VisiblePosition = 7;
            ultraGridColumn535.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn535.Header.VisiblePosition = 8;
            ultraGridColumn536.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn536.Header.VisiblePosition = 9;
            ultraGridColumn537.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn537.Header.VisiblePosition = 10;
            ultraGridColumn538.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn538.Header.VisiblePosition = 11;
            ultraGridColumn539.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn539.Header.VisiblePosition = 12;
            ultraGridColumn540.Header.VisiblePosition = 13;
            ultraGridColumn541.Header.VisiblePosition = 14;
            ultraGridColumn542.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn542.Header.VisiblePosition = 15;
            ultraGridBand19.Columns.AddRange(new object[] {
            ultraGridColumn527,
            ultraGridColumn528,
            ultraGridColumn529,
            ultraGridColumn530,
            ultraGridColumn531,
            ultraGridColumn532,
            ultraGridColumn533,
            ultraGridColumn534,
            ultraGridColumn535,
            ultraGridColumn536,
            ultraGridColumn537,
            ultraGridColumn538,
            ultraGridColumn539,
            ultraGridColumn540,
            ultraGridColumn541,
            ultraGridColumn542});
            ultraGridColumn543.Header.VisiblePosition = 0;
            ultraGridColumn544.Header.VisiblePosition = 1;
            ultraGridColumn545.Header.VisiblePosition = 2;
            ultraGridColumn546.Header.VisiblePosition = 3;
            ultraGridBand20.Columns.AddRange(new object[] {
            ultraGridColumn543,
            ultraGridColumn544,
            ultraGridColumn545,
            ultraGridColumn546});
            ultraGridColumn547.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn547.Header.VisiblePosition = 0;
            ultraGridColumn548.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn548.Header.VisiblePosition = 1;
            ultraGridColumn549.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn549.Header.VisiblePosition = 2;
            ultraGridColumn550.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn550.Header.VisiblePosition = 3;
            ultraGridColumn551.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn551.Header.VisiblePosition = 4;
            ultraGridColumn552.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn552.Header.VisiblePosition = 5;
            ultraGridColumn553.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn553.Header.VisiblePosition = 6;
            ultraGridColumn554.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn554.Header.VisiblePosition = 7;
            ultraGridColumn555.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn555.Header.VisiblePosition = 8;
            ultraGridColumn556.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn556.Header.VisiblePosition = 9;
            ultraGridColumn557.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn557.Header.VisiblePosition = 10;
            ultraGridColumn558.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn558.Header.VisiblePosition = 11;
            ultraGridColumn559.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn559.Header.VisiblePosition = 12;
            ultraGridColumn560.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn560.Header.VisiblePosition = 13;
            ultraGridBand21.Columns.AddRange(new object[] {
            ultraGridColumn547,
            ultraGridColumn548,
            ultraGridColumn549,
            ultraGridColumn550,
            ultraGridColumn551,
            ultraGridColumn552,
            ultraGridColumn553,
            ultraGridColumn554,
            ultraGridColumn555,
            ultraGridColumn556,
            ultraGridColumn557,
            ultraGridColumn558,
            ultraGridColumn559,
            ultraGridColumn560});
            ultraGridColumn561.Header.VisiblePosition = 0;
            ultraGridColumn562.Header.VisiblePosition = 1;
            ultraGridColumn563.Header.VisiblePosition = 2;
            ultraGridColumn564.Header.VisiblePosition = 3;
            ultraGridBand22.Columns.AddRange(new object[] {
            ultraGridColumn561,
            ultraGridColumn562,
            ultraGridColumn563,
            ultraGridColumn564});
            ultraGridColumn565.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn565.Header.VisiblePosition = 0;
            ultraGridColumn566.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn566.Header.VisiblePosition = 1;
            ultraGridColumn567.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn567.Header.VisiblePosition = 2;
            ultraGridColumn568.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn568.Header.VisiblePosition = 3;
            ultraGridColumn569.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn569.Header.VisiblePosition = 4;
            ultraGridColumn570.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn570.Header.VisiblePosition = 5;
            ultraGridColumn571.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn571.Header.VisiblePosition = 6;
            ultraGridColumn572.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn572.Header.VisiblePosition = 7;
            ultraGridColumn573.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn573.Header.VisiblePosition = 8;
            ultraGridColumn574.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn574.Header.VisiblePosition = 9;
            ultraGridColumn575.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn575.Header.VisiblePosition = 10;
            ultraGridColumn576.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn576.Header.VisiblePosition = 11;
            ultraGridColumn577.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn577.Header.VisiblePosition = 12;
            ultraGridColumn578.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn578.Header.VisiblePosition = 13;
            ultraGridColumn579.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn579.Header.VisiblePosition = 14;
            ultraGridColumn580.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn580.Header.VisiblePosition = 15;
            ultraGridBand23.Columns.AddRange(new object[] {
            ultraGridColumn565,
            ultraGridColumn566,
            ultraGridColumn567,
            ultraGridColumn568,
            ultraGridColumn569,
            ultraGridColumn570,
            ultraGridColumn571,
            ultraGridColumn572,
            ultraGridColumn573,
            ultraGridColumn574,
            ultraGridColumn575,
            ultraGridColumn576,
            ultraGridColumn577,
            ultraGridColumn578,
            ultraGridColumn579,
            ultraGridColumn580});
            ultraGridColumn581.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn581.Header.VisiblePosition = 0;
            ultraGridColumn582.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn582.Header.VisiblePosition = 1;
            ultraGridColumn583.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn583.Header.VisiblePosition = 2;
            ultraGridColumn584.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn584.Header.VisiblePosition = 3;
            ultraGridBand24.Columns.AddRange(new object[] {
            ultraGridColumn581,
            ultraGridColumn582,
            ultraGridColumn583,
            ultraGridColumn584});
            this.ultraComboDitchesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand18);
            this.ultraComboDitchesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand19);
            this.ultraComboDitchesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand20);
            this.ultraComboDitchesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand21);
            this.ultraComboDitchesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand22);
            this.ultraComboDitchesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand23);
            this.ultraComboDitchesMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand24);
            this.ultraComboDitchesMaterial.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraComboDitchesMaterial.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance46.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboDitchesMaterial.DisplayLayout.GroupByBox.Appearance = appearance46;
            appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboDitchesMaterial.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
            this.ultraComboDitchesMaterial.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance48.BackColor2 = System.Drawing.SystemColors.Control;
            appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboDitchesMaterial.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
            this.ultraComboDitchesMaterial.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraComboDitchesMaterial.DisplayLayout.MaxRowScrollRegions = 1;
            appearance57.BackColor = System.Drawing.SystemColors.Window;
            appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.ActiveCellAppearance = appearance57;
            appearance52.BackColor = System.Drawing.SystemColors.Highlight;
            appearance52.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.ActiveRowAppearance = appearance52;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance51.BackColor = System.Drawing.SystemColors.Window;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.CardAreaAppearance = appearance51;
            appearance50.BorderColor = System.Drawing.Color.Silver;
            appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.CellAppearance = appearance50;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.CellPadding = 0;
            appearance54.BackColor = System.Drawing.SystemColors.Control;
            appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance54.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance54.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.GroupByRowAppearance = appearance54;
            appearance56.TextHAlignAsString = "Left";
            this.ultraComboDitchesMaterial.DisplayLayout.Override.HeaderAppearance = appearance56;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance55.BackColor = System.Drawing.SystemColors.Window;
            appearance55.BorderColor = System.Drawing.Color.Silver;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.RowAppearance = appearance55;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance53.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraComboDitchesMaterial.DisplayLayout.Override.TemplateAddRowAppearance = appearance53;
            this.ultraComboDitchesMaterial.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraComboDitchesMaterial.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraComboDitchesMaterial.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraComboDitchesMaterial.DisplayMember = "material";
            this.ultraComboDitchesMaterial.Location = new System.Drawing.Point(846, 79);
            this.ultraComboDitchesMaterial.Name = "ultraComboDitchesMaterial";
            this.ultraComboDitchesMaterial.Size = new System.Drawing.Size(107, 22);
            this.ultraComboDitchesMaterial.TabIndex = 62;
            this.ultraComboDitchesMaterial.Text = "ultraComboDitchesMaterial";
            this.ultraComboDitchesMaterial.ValueMember = "material_type_id";
            // 
            // ultraNumericEditorDitchesBottomWidth
            // 
            this.ultraNumericEditorDitchesBottomWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "bottom_width_in", true));
            this.ultraNumericEditorDitchesBottomWidth.Location = new System.Drawing.Point(620, 80);
            this.ultraNumericEditorDitchesBottomWidth.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorDitchesBottomWidth.Name = "ultraNumericEditorDitchesBottomWidth";
            this.ultraNumericEditorDitchesBottomWidth.Nullable = true;
            this.ultraNumericEditorDitchesBottomWidth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorDitchesBottomWidth.Size = new System.Drawing.Size(107, 21);
            this.ultraNumericEditorDitchesBottomWidth.TabIndex = 60;
            // 
            // ultraNumericEditorDitchesTopWidth
            // 
            this.ultraNumericEditorDitchesTopWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "top_width_in", true));
            this.ultraNumericEditorDitchesTopWidth.Location = new System.Drawing.Point(507, 80);
            this.ultraNumericEditorDitchesTopWidth.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorDitchesTopWidth.Name = "ultraNumericEditorDitchesTopWidth";
            this.ultraNumericEditorDitchesTopWidth.Nullable = true;
            this.ultraNumericEditorDitchesTopWidth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorDitchesTopWidth.Size = new System.Drawing.Size(107, 21);
            this.ultraNumericEditorDitchesTopWidth.TabIndex = 59;
            // 
            // ultraNumericEditorDitchesDepth
            // 
            this.ultraNumericEditorDitchesDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKDITCHSURVEYPAGEBindingSource, "depth_in", true));
            this.ultraNumericEditorDitchesDepth.Location = new System.Drawing.Point(733, 80);
            this.ultraNumericEditorDitchesDepth.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorDitchesDepth.Name = "ultraNumericEditorDitchesDepth";
            this.ultraNumericEditorDitchesDepth.Nullable = true;
            this.ultraNumericEditorDitchesDepth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorDitchesDepth.Size = new System.Drawing.Size(107, 21);
            this.ultraNumericEditorDitchesDepth.TabIndex = 61;
            // 
            // buttonUpdateDitch
            // 
            this.buttonUpdateDitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateDitch.Location = new System.Drawing.Point(940, 139);
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
            this.buttonDitchesViewAddPhotos.Location = new System.Drawing.Point(482, 139);
            this.buttonDitchesViewAddPhotos.Name = "buttonDitchesViewAddPhotos";
            this.buttonDitchesViewAddPhotos.Size = new System.Drawing.Size(220, 28);
            this.buttonDitchesViewAddPhotos.TabIndex = 64;
            this.buttonDitchesViewAddPhotos.Text = "View/Add Photos";
            this.buttonDitchesViewAddPhotos.UseVisualStyleBackColor = true;
            this.buttonDitchesViewAddPhotos.Click += new System.EventHandler(this.buttonDitchesViewAddPhotos_Click);
            // 
            // textBoxDitchesMeasuredNode
            // 
            this.textBoxDitchesMeasuredNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKDITCHSURVEYPAGEBindingSource, "node", true));
            this.textBoxDitchesMeasuredNode.Location = new System.Drawing.Point(426, 81);
            this.textBoxDitchesMeasuredNode.Name = "textBoxDitchesMeasuredNode";
            this.textBoxDitchesMeasuredNode.Size = new System.Drawing.Size(78, 20);
            this.textBoxDitchesMeasuredNode.TabIndex = 58;
            this.textBoxDitchesMeasuredNode.TextChanged += new System.EventHandler(this.textBoxDitchesNode_TextChanged);
            this.textBoxDitchesMeasuredNode.Enter += new System.EventHandler(this.textBoxDitchesNode_Enter);
            // 
            // labelDitchesMeasuredNode
            // 
            this.labelDitchesMeasuredNode.AutoSize = true;
            this.labelDitchesMeasuredNode.Location = new System.Drawing.Point(423, 65);
            this.labelDitchesMeasuredNode.Name = "labelDitchesMeasuredNode";
            this.labelDitchesMeasuredNode.Size = new System.Drawing.Size(81, 13);
            this.labelDitchesMeasuredNode.TabIndex = 56;
            this.labelDitchesMeasuredNode.Text = "Measured node";
            // 
            // labelDitchesFacingDirection
            // 
            this.labelDitchesFacingDirection.AutoSize = true;
            this.labelDitchesFacingDirection.Location = new System.Drawing.Point(414, 8);
            this.labelDitchesFacingDirection.Name = "labelDitchesFacingDirection";
            this.labelDitchesFacingDirection.Size = new System.Drawing.Size(84, 13);
            this.labelDitchesFacingDirection.TabIndex = 55;
            this.labelDitchesFacingDirection.Text = "Facing Direction";
            this.labelDitchesFacingDirection.Visible = false;
            // 
            // labelDitchesMaterial
            // 
            this.labelDitchesMaterial.AutoSize = true;
            this.labelDitchesMaterial.Location = new System.Drawing.Point(843, 65);
            this.labelDitchesMaterial.Name = "labelDitchesMaterial";
            this.labelDitchesMaterial.Size = new System.Drawing.Size(44, 13);
            this.labelDitchesMaterial.TabIndex = 53;
            this.labelDitchesMaterial.Text = "Material";
            // 
            // labelDitchesBottomWidth
            // 
            this.labelDitchesBottomWidth.AutoSize = true;
            this.labelDitchesBottomWidth.Location = new System.Drawing.Point(617, 65);
            this.labelDitchesBottomWidth.Name = "labelDitchesBottomWidth";
            this.labelDitchesBottomWidth.Size = new System.Drawing.Size(85, 13);
            this.labelDitchesBottomWidth.TabIndex = 51;
            this.labelDitchesBottomWidth.Text = "Bottom width (in)";
            // 
            // labelDitchesTopWidth
            // 
            this.labelDitchesTopWidth.AutoSize = true;
            this.labelDitchesTopWidth.Location = new System.Drawing.Point(504, 65);
            this.labelDitchesTopWidth.Name = "labelDitchesTopWidth";
            this.labelDitchesTopWidth.Size = new System.Drawing.Size(71, 13);
            this.labelDitchesTopWidth.TabIndex = 50;
            this.labelDitchesTopWidth.Text = "Top width (in)";
            // 
            // labelDitchesDepth
            // 
            this.labelDitchesDepth.AutoSize = true;
            this.labelDitchesDepth.Location = new System.Drawing.Point(733, 65);
            this.labelDitchesDepth.Name = "labelDitchesDepth";
            this.labelDitchesDepth.Size = new System.Drawing.Size(55, 13);
            this.labelDitchesDepth.TabIndex = 49;
            this.labelDitchesDepth.Text = "Height (in)";
            // 
            // buttonDitchesAdd
            // 
            this.buttonDitchesAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDitchesAdd.Location = new System.Drawing.Point(257, 139);
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
            this.us_node,
            this.ds_node,
            this.nodeDataGridViewTextBoxColumn});
            this.dataGridViewDitches.DataSource = this.fKDITCHSURVEYPAGEBindingSource;
            this.dataGridViewDitches.Location = new System.Drawing.Point(3, 8);
            this.dataGridViewDitches.Name = "dataGridViewDitches";
            this.dataGridViewDitches.ReadOnly = true;
            this.dataGridViewDitches.Size = new System.Drawing.Size(248, 159);
            this.dataGridViewDitches.TabIndex = 40;
            // 
            // us_node
            // 
            this.us_node.DataPropertyName = "us_node";
            this.us_node.HeaderText = "us_node";
            this.us_node.Name = "us_node";
            this.us_node.ReadOnly = true;
            this.us_node.Width = 66;
            // 
            // ds_node
            // 
            this.ds_node.DataPropertyName = "ds_node";
            this.ds_node.HeaderText = "ds_node";
            this.ds_node.Name = "ds_node";
            this.ds_node.ReadOnly = true;
            this.ds_node.Width = 66;
            // 
            // nodeDataGridViewTextBoxColumn
            // 
            this.nodeDataGridViewTextBoxColumn.DataPropertyName = "node";
            this.nodeDataGridViewTextBoxColumn.HeaderText = "Measured";
            this.nodeDataGridViewTextBoxColumn.Name = "nodeDataGridViewTextBoxColumn";
            this.nodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.nodeDataGridViewTextBoxColumn.Width = 66;
            // 
            // tabPageCulverts
            // 
            this.tabPageCulverts.Controls.Add(this.textBoxCulvertsDSNode);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsDSNode);
            this.tabPageCulverts.Controls.Add(this.textBoxCulvertsUSNode);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsUSNode);
            this.tabPageCulverts.Controls.Add(this.ultraComboCulvertsShape);
            this.tabPageCulverts.Controls.Add(this.ultraComboCulvertsFacingDirection);
            this.tabPageCulverts.Controls.Add(this.ultraComboCulvertsMaterial);
            this.tabPageCulverts.Controls.Add(this.ultraNumericEditorCulvertsUnobstructedHeight);
            this.tabPageCulverts.Controls.Add(this.ultraNumericEditorCulvertsFullDepth);
            this.tabPageCulverts.Controls.Add(this.buttonUpdateCulvert);
            this.tabPageCulverts.Controls.Add(this.buttonCulvertsViewAddPhotos);
            this.tabPageCulverts.Controls.Add(this.comboBoxCulvertsType);
            this.tabPageCulverts.Controls.Add(this.textBoxCulvertsMeasuredNode);
            this.tabPageCulverts.Controls.Add(this.labelCulvertsMeasuredNode);
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
            this.tabPageCulverts.Size = new System.Drawing.Size(1043, 170);
            this.tabPageCulverts.TabIndex = 1;
            this.tabPageCulverts.Text = "Culverts";
            this.tabPageCulverts.UseVisualStyleBackColor = true;
            this.tabPageCulverts.Click += new System.EventHandler(this.tabPageCulverts_Click);
            this.tabPageCulverts.Enter += new System.EventHandler(this.tabPageCulverts_Enter);
            // 
            // textBoxCulvertsDSNode
            // 
            this.textBoxCulvertsDSNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKCULVERTSURVEYPAGEBindingSource1, "ds_node", true));
            this.textBoxCulvertsDSNode.Location = new System.Drawing.Point(349, 79);
            this.textBoxCulvertsDSNode.Name = "textBoxCulvertsDSNode";
            this.textBoxCulvertsDSNode.Size = new System.Drawing.Size(78, 20);
            this.textBoxCulvertsDSNode.TabIndex = 42;
            this.textBoxCulvertsDSNode.TextChanged += new System.EventHandler(this.textBoxCulvertsDSNode_TextChanged);
            // 
            // fKCULVERTSURVEYPAGEBindingSource1
            // 
            this.fKCULVERTSURVEYPAGEBindingSource1.DataMember = "FK_CULVERT_SURVEY_PAGE";
            this.fKCULVERTSURVEYPAGEBindingSource1.DataSource = this.fKSURVEYPAGEVIEWBindingSource;
            // 
            // labelCulvertsDSNode
            // 
            this.labelCulvertsDSNode.AutoSize = true;
            this.labelCulvertsDSNode.Location = new System.Drawing.Point(346, 63);
            this.labelCulvertsDSNode.Name = "labelCulvertsDSNode";
            this.labelCulvertsDSNode.Size = new System.Drawing.Size(49, 13);
            this.labelCulvertsDSNode.TabIndex = 69;
            this.labelCulvertsDSNode.Text = "DS node";
            // 
            // textBoxCulvertsUSNode
            // 
            this.textBoxCulvertsUSNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKCULVERTSURVEYPAGEBindingSource1, "us_node", true));
            this.textBoxCulvertsUSNode.Location = new System.Drawing.Point(265, 79);
            this.textBoxCulvertsUSNode.Name = "textBoxCulvertsUSNode";
            this.textBoxCulvertsUSNode.Size = new System.Drawing.Size(78, 20);
            this.textBoxCulvertsUSNode.TabIndex = 41;
            this.textBoxCulvertsUSNode.TextChanged += new System.EventHandler(this.textBoxCulvertsUSNode_TextChanged);
            // 
            // labelCulvertsUSNode
            // 
            this.labelCulvertsUSNode.AutoSize = true;
            this.labelCulvertsUSNode.Location = new System.Drawing.Point(262, 63);
            this.labelCulvertsUSNode.Name = "labelCulvertsUSNode";
            this.labelCulvertsUSNode.Size = new System.Drawing.Size(49, 13);
            this.labelCulvertsUSNode.TabIndex = 67;
            this.labelCulvertsUSNode.Text = "US node";
            // 
            // ultraComboCulvertsShape
            // 
            this.ultraComboCulvertsShape.CheckedListSettings.CheckStateMember = "";
            this.ultraComboCulvertsShape.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "shape", true));
            this.ultraComboCulvertsShape.DataSource = this.sWSPSHAPETYPEBindingSource;
            appearance76.BackColor = System.Drawing.SystemColors.Window;
            appearance76.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraComboCulvertsShape.DisplayLayout.Appearance = appearance76;
            ultraGridColumn585.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn585.Header.VisiblePosition = 0;
            ultraGridColumn585.Hidden = true;
            ultraGridColumn586.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn586.Header.VisiblePosition = 1;
            ultraGridColumn587.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn587.Header.VisiblePosition = 2;
            ultraGridColumn587.Hidden = true;
            ultraGridColumn588.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn588.Header.VisiblePosition = 4;
            ultraGridColumn589.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn589.Header.VisiblePosition = 3;
            ultraGridBand25.Columns.AddRange(new object[] {
            ultraGridColumn585,
            ultraGridColumn586,
            ultraGridColumn587,
            ultraGridColumn588,
            ultraGridColumn589});
            ultraGridColumn590.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn590.Header.VisiblePosition = 0;
            ultraGridColumn591.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn591.Header.VisiblePosition = 1;
            ultraGridColumn592.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn592.Header.VisiblePosition = 2;
            ultraGridColumn593.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn593.Header.VisiblePosition = 3;
            ultraGridColumn594.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn594.Header.VisiblePosition = 4;
            ultraGridColumn595.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn595.Header.VisiblePosition = 5;
            ultraGridColumn596.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn596.Header.VisiblePosition = 6;
            ultraGridColumn597.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn597.Header.VisiblePosition = 7;
            ultraGridColumn598.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn598.Header.VisiblePosition = 8;
            ultraGridColumn599.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn599.Header.VisiblePosition = 9;
            ultraGridColumn600.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn600.Header.VisiblePosition = 10;
            ultraGridColumn601.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn601.Header.VisiblePosition = 11;
            ultraGridColumn602.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn602.Header.VisiblePosition = 12;
            ultraGridColumn603.Header.VisiblePosition = 13;
            ultraGridColumn604.Header.VisiblePosition = 14;
            ultraGridColumn605.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn605.Header.VisiblePosition = 15;
            ultraGridBand26.Columns.AddRange(new object[] {
            ultraGridColumn590,
            ultraGridColumn591,
            ultraGridColumn592,
            ultraGridColumn593,
            ultraGridColumn594,
            ultraGridColumn595,
            ultraGridColumn596,
            ultraGridColumn597,
            ultraGridColumn598,
            ultraGridColumn599,
            ultraGridColumn600,
            ultraGridColumn601,
            ultraGridColumn602,
            ultraGridColumn603,
            ultraGridColumn604,
            ultraGridColumn605});
            ultraGridColumn606.Header.VisiblePosition = 0;
            ultraGridColumn607.Header.VisiblePosition = 1;
            ultraGridColumn608.Header.VisiblePosition = 2;
            ultraGridColumn609.Header.VisiblePosition = 3;
            ultraGridBand27.Columns.AddRange(new object[] {
            ultraGridColumn606,
            ultraGridColumn607,
            ultraGridColumn608,
            ultraGridColumn609});
            ultraGridColumn610.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn610.Header.VisiblePosition = 0;
            ultraGridColumn611.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn611.Header.VisiblePosition = 1;
            ultraGridColumn612.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn612.Header.VisiblePosition = 2;
            ultraGridColumn613.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn613.Header.VisiblePosition = 3;
            ultraGridColumn614.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn614.Header.VisiblePosition = 4;
            ultraGridColumn615.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn615.Header.VisiblePosition = 5;
            ultraGridColumn616.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn616.Header.VisiblePosition = 6;
            ultraGridColumn617.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn617.Header.VisiblePosition = 7;
            ultraGridColumn618.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn618.Header.VisiblePosition = 8;
            ultraGridColumn619.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn619.Header.VisiblePosition = 9;
            ultraGridColumn620.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn620.Header.VisiblePosition = 10;
            ultraGridColumn621.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn621.Header.VisiblePosition = 11;
            ultraGridColumn622.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn622.Header.VisiblePosition = 12;
            ultraGridColumn623.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn623.Header.VisiblePosition = 13;
            ultraGridColumn624.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn624.Header.VisiblePosition = 14;
            ultraGridColumn625.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn625.Header.VisiblePosition = 15;
            ultraGridBand28.Columns.AddRange(new object[] {
            ultraGridColumn610,
            ultraGridColumn611,
            ultraGridColumn612,
            ultraGridColumn613,
            ultraGridColumn614,
            ultraGridColumn615,
            ultraGridColumn616,
            ultraGridColumn617,
            ultraGridColumn618,
            ultraGridColumn619,
            ultraGridColumn620,
            ultraGridColumn621,
            ultraGridColumn622,
            ultraGridColumn623,
            ultraGridColumn624,
            ultraGridColumn625});
            ultraGridColumn626.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn626.Header.VisiblePosition = 0;
            ultraGridColumn627.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn627.Header.VisiblePosition = 1;
            ultraGridColumn628.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn628.Header.VisiblePosition = 2;
            ultraGridColumn629.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn629.Header.VisiblePosition = 3;
            ultraGridBand29.Columns.AddRange(new object[] {
            ultraGridColumn626,
            ultraGridColumn627,
            ultraGridColumn628,
            ultraGridColumn629});
            this.ultraComboCulvertsShape.DisplayLayout.BandsSerializer.Add(ultraGridBand25);
            this.ultraComboCulvertsShape.DisplayLayout.BandsSerializer.Add(ultraGridBand26);
            this.ultraComboCulvertsShape.DisplayLayout.BandsSerializer.Add(ultraGridBand27);
            this.ultraComboCulvertsShape.DisplayLayout.BandsSerializer.Add(ultraGridBand28);
            this.ultraComboCulvertsShape.DisplayLayout.BandsSerializer.Add(ultraGridBand29);
            this.ultraComboCulvertsShape.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraComboCulvertsShape.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance73.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance73.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboCulvertsShape.DisplayLayout.GroupByBox.Appearance = appearance73;
            appearance74.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboCulvertsShape.DisplayLayout.GroupByBox.BandLabelAppearance = appearance74;
            this.ultraComboCulvertsShape.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance75.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance75.BackColor2 = System.Drawing.SystemColors.Control;
            appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboCulvertsShape.DisplayLayout.GroupByBox.PromptAppearance = appearance75;
            this.ultraComboCulvertsShape.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraComboCulvertsShape.DisplayLayout.MaxRowScrollRegions = 1;
            appearance84.BackColor = System.Drawing.SystemColors.Window;
            appearance84.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraComboCulvertsShape.DisplayLayout.Override.ActiveCellAppearance = appearance84;
            appearance79.BackColor = System.Drawing.SystemColors.Highlight;
            appearance79.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraComboCulvertsShape.DisplayLayout.Override.ActiveRowAppearance = appearance79;
            this.ultraComboCulvertsShape.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraComboCulvertsShape.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance78.BackColor = System.Drawing.SystemColors.Window;
            this.ultraComboCulvertsShape.DisplayLayout.Override.CardAreaAppearance = appearance78;
            appearance77.BorderColor = System.Drawing.Color.Silver;
            appearance77.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraComboCulvertsShape.DisplayLayout.Override.CellAppearance = appearance77;
            this.ultraComboCulvertsShape.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraComboCulvertsShape.DisplayLayout.Override.CellPadding = 0;
            appearance81.BackColor = System.Drawing.SystemColors.Control;
            appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance81.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboCulvertsShape.DisplayLayout.Override.GroupByRowAppearance = appearance81;
            appearance83.TextHAlignAsString = "Left";
            this.ultraComboCulvertsShape.DisplayLayout.Override.HeaderAppearance = appearance83;
            this.ultraComboCulvertsShape.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraComboCulvertsShape.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance82.BackColor = System.Drawing.SystemColors.Window;
            appearance82.BorderColor = System.Drawing.Color.Silver;
            this.ultraComboCulvertsShape.DisplayLayout.Override.RowAppearance = appearance82;
            this.ultraComboCulvertsShape.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraComboCulvertsShape.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
            this.ultraComboCulvertsShape.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraComboCulvertsShape.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraComboCulvertsShape.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraComboCulvertsShape.DisplayMember = "shape";
            this.ultraComboCulvertsShape.Location = new System.Drawing.Point(517, 77);
            this.ultraComboCulvertsShape.Name = "ultraComboCulvertsShape";
            this.ultraComboCulvertsShape.Size = new System.Drawing.Size(95, 22);
            this.ultraComboCulvertsShape.TabIndex = 44;
            this.ultraComboCulvertsShape.Text = "ultraComboCulvertsShape";
            this.ultraComboCulvertsShape.ValueMember = "shape_type_id";
            // 
            // ultraComboCulvertsFacingDirection
            // 
            this.ultraComboCulvertsFacingDirection.CheckedListSettings.CheckStateMember = "";
            this.ultraComboCulvertsFacingDirection.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "facing", true));
            this.ultraComboCulvertsFacingDirection.DataSource = this.sWSPFACINGTYPEBindingSource;
            appearance16.BackColor = System.Drawing.SystemColors.Window;
            appearance16.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Appearance = appearance16;
            ultraGridColumn630.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn630.Header.VisiblePosition = 0;
            ultraGridColumn630.Hidden = true;
            ultraGridColumn631.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn631.Header.VisiblePosition = 1;
            ultraGridColumn632.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn632.Header.VisiblePosition = 2;
            ultraGridColumn632.Hidden = true;
            ultraGridColumn633.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn633.Header.VisiblePosition = 4;
            ultraGridColumn634.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn634.Header.VisiblePosition = 3;
            ultraGridBand30.Columns.AddRange(new object[] {
            ultraGridColumn630,
            ultraGridColumn631,
            ultraGridColumn632,
            ultraGridColumn633,
            ultraGridColumn634});
            ultraGridColumn635.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn635.Header.VisiblePosition = 0;
            ultraGridColumn636.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn636.Header.VisiblePosition = 1;
            ultraGridColumn637.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn637.Header.VisiblePosition = 2;
            ultraGridColumn638.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn638.Header.VisiblePosition = 3;
            ultraGridColumn639.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn639.Header.VisiblePosition = 4;
            ultraGridColumn640.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn640.Header.VisiblePosition = 5;
            ultraGridColumn641.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn641.Header.VisiblePosition = 6;
            ultraGridColumn642.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn642.Header.VisiblePosition = 7;
            ultraGridColumn643.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn643.Header.VisiblePosition = 8;
            ultraGridColumn644.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn644.Header.VisiblePosition = 9;
            ultraGridColumn645.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn645.Header.VisiblePosition = 10;
            ultraGridColumn646.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn646.Header.VisiblePosition = 11;
            ultraGridColumn647.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn647.Header.VisiblePosition = 12;
            ultraGridColumn648.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn648.Header.VisiblePosition = 13;
            ultraGridBand31.Columns.AddRange(new object[] {
            ultraGridColumn635,
            ultraGridColumn636,
            ultraGridColumn637,
            ultraGridColumn638,
            ultraGridColumn639,
            ultraGridColumn640,
            ultraGridColumn641,
            ultraGridColumn642,
            ultraGridColumn643,
            ultraGridColumn644,
            ultraGridColumn645,
            ultraGridColumn646,
            ultraGridColumn647,
            ultraGridColumn648});
            ultraGridColumn649.Header.VisiblePosition = 0;
            ultraGridColumn650.Header.VisiblePosition = 1;
            ultraGridColumn651.Header.VisiblePosition = 2;
            ultraGridColumn652.Header.VisiblePosition = 3;
            ultraGridBand32.Columns.AddRange(new object[] {
            ultraGridColumn649,
            ultraGridColumn650,
            ultraGridColumn651,
            ultraGridColumn652});
            ultraGridColumn653.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn653.Header.VisiblePosition = 0;
            ultraGridColumn654.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn654.Header.VisiblePosition = 1;
            ultraGridColumn655.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn655.Header.VisiblePosition = 2;
            ultraGridColumn656.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn656.Header.VisiblePosition = 3;
            ultraGridColumn657.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn657.Header.VisiblePosition = 4;
            ultraGridColumn658.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn658.Header.VisiblePosition = 5;
            ultraGridColumn659.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn659.Header.VisiblePosition = 6;
            ultraGridColumn660.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn660.Header.VisiblePosition = 7;
            ultraGridColumn661.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn661.Header.VisiblePosition = 8;
            ultraGridColumn662.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn662.Header.VisiblePosition = 9;
            ultraGridColumn663.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn663.Header.VisiblePosition = 10;
            ultraGridColumn664.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn664.Header.VisiblePosition = 11;
            ultraGridColumn665.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn665.Header.VisiblePosition = 12;
            ultraGridColumn666.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn666.Header.VisiblePosition = 13;
            ultraGridColumn667.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn667.Header.VisiblePosition = 14;
            ultraGridColumn668.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn668.Header.VisiblePosition = 15;
            ultraGridBand33.Columns.AddRange(new object[] {
            ultraGridColumn653,
            ultraGridColumn654,
            ultraGridColumn655,
            ultraGridColumn656,
            ultraGridColumn657,
            ultraGridColumn658,
            ultraGridColumn659,
            ultraGridColumn660,
            ultraGridColumn661,
            ultraGridColumn662,
            ultraGridColumn663,
            ultraGridColumn664,
            ultraGridColumn665,
            ultraGridColumn666,
            ultraGridColumn667,
            ultraGridColumn668});
            ultraGridColumn669.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn669.Header.VisiblePosition = 0;
            ultraGridColumn670.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn670.Header.VisiblePosition = 1;
            ultraGridColumn671.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn671.Header.VisiblePosition = 2;
            ultraGridColumn672.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn672.Header.VisiblePosition = 3;
            ultraGridBand34.Columns.AddRange(new object[] {
            ultraGridColumn669,
            ultraGridColumn670,
            ultraGridColumn671,
            ultraGridColumn672});
            this.ultraComboCulvertsFacingDirection.DisplayLayout.BandsSerializer.Add(ultraGridBand30);
            this.ultraComboCulvertsFacingDirection.DisplayLayout.BandsSerializer.Add(ultraGridBand31);
            this.ultraComboCulvertsFacingDirection.DisplayLayout.BandsSerializer.Add(ultraGridBand32);
            this.ultraComboCulvertsFacingDirection.DisplayLayout.BandsSerializer.Add(ultraGridBand33);
            this.ultraComboCulvertsFacingDirection.DisplayLayout.BandsSerializer.Add(ultraGridBand34);
            this.ultraComboCulvertsFacingDirection.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance13.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance13.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.GroupByBox.Appearance = appearance13;
            appearance14.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.GroupByBox.BandLabelAppearance = appearance14;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance15.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance15.BackColor2 = System.Drawing.SystemColors.Control;
            appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.GroupByBox.PromptAppearance = appearance15;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.MaxRowScrollRegions = 1;
            appearance60.BackColor = System.Drawing.SystemColors.Window;
            appearance60.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.ActiveCellAppearance = appearance60;
            appearance22.BackColor = System.Drawing.SystemColors.Highlight;
            appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.ActiveRowAppearance = appearance22;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance18.BackColor = System.Drawing.SystemColors.Window;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.CardAreaAppearance = appearance18;
            appearance17.BorderColor = System.Drawing.Color.Silver;
            appearance17.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.CellAppearance = appearance17;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.CellPadding = 0;
            appearance24.BackColor = System.Drawing.SystemColors.Control;
            appearance24.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance24.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance24.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.GroupByRowAppearance = appearance24;
            appearance59.TextHAlignAsString = "Left";
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.HeaderAppearance = appearance59;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance58.BackColor = System.Drawing.SystemColors.Window;
            appearance58.BorderColor = System.Drawing.Color.Silver;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.RowAppearance = appearance58;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance23.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.Override.TemplateAddRowAppearance = appearance23;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraComboCulvertsFacingDirection.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraComboCulvertsFacingDirection.DisplayMember = "facing";
            this.ultraComboCulvertsFacingDirection.Location = new System.Drawing.Point(360, 24);
            this.ultraComboCulvertsFacingDirection.Name = "ultraComboCulvertsFacingDirection";
            this.ultraComboCulvertsFacingDirection.Size = new System.Drawing.Size(89, 22);
            this.ultraComboCulvertsFacingDirection.TabIndex = 44;
            this.ultraComboCulvertsFacingDirection.Text = "ultraComboCulvertsFacingDirection";
            this.ultraComboCulvertsFacingDirection.ValueMember = "facing_type_id";
            this.ultraComboCulvertsFacingDirection.Visible = false;
            // 
            // ultraComboCulvertsMaterial
            // 
            this.ultraComboCulvertsMaterial.CheckedListSettings.CheckStateMember = "";
            this.ultraComboCulvertsMaterial.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "material", true));
            this.ultraComboCulvertsMaterial.DataSource = this.sWSPMATERIALTYPEBindingSource;
            appearance37.BackColor = System.Drawing.SystemColors.Window;
            appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraComboCulvertsMaterial.DisplayLayout.Appearance = appearance37;
            ultraGridColumn673.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn673.Header.VisiblePosition = 0;
            ultraGridColumn673.Hidden = true;
            ultraGridColumn674.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn674.Header.VisiblePosition = 1;
            ultraGridColumn675.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn675.Header.VisiblePosition = 2;
            ultraGridColumn675.Hidden = true;
            ultraGridColumn676.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn676.Header.VisiblePosition = 5;
            ultraGridColumn677.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn677.Header.VisiblePosition = 4;
            ultraGridColumn678.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn678.Header.VisiblePosition = 3;
            ultraGridBand35.Columns.AddRange(new object[] {
            ultraGridColumn673,
            ultraGridColumn674,
            ultraGridColumn675,
            ultraGridColumn676,
            ultraGridColumn677,
            ultraGridColumn678});
            ultraGridColumn679.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn679.Header.VisiblePosition = 0;
            ultraGridColumn680.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn680.Header.VisiblePosition = 1;
            ultraGridColumn681.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn681.Header.VisiblePosition = 2;
            ultraGridColumn682.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn682.Header.VisiblePosition = 3;
            ultraGridColumn683.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn683.Header.VisiblePosition = 4;
            ultraGridColumn684.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn684.Header.VisiblePosition = 5;
            ultraGridColumn685.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn685.Header.VisiblePosition = 6;
            ultraGridColumn686.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn686.Header.VisiblePosition = 7;
            ultraGridColumn687.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn687.Header.VisiblePosition = 8;
            ultraGridColumn688.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn688.Header.VisiblePosition = 9;
            ultraGridColumn689.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn689.Header.VisiblePosition = 10;
            ultraGridColumn690.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn690.Header.VisiblePosition = 11;
            ultraGridColumn691.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn691.Header.VisiblePosition = 12;
            ultraGridColumn692.Header.VisiblePosition = 13;
            ultraGridColumn693.Header.VisiblePosition = 14;
            ultraGridColumn694.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn694.Header.VisiblePosition = 15;
            ultraGridBand36.Columns.AddRange(new object[] {
            ultraGridColumn679,
            ultraGridColumn680,
            ultraGridColumn681,
            ultraGridColumn682,
            ultraGridColumn683,
            ultraGridColumn684,
            ultraGridColumn685,
            ultraGridColumn686,
            ultraGridColumn687,
            ultraGridColumn688,
            ultraGridColumn689,
            ultraGridColumn690,
            ultraGridColumn691,
            ultraGridColumn692,
            ultraGridColumn693,
            ultraGridColumn694});
            ultraGridColumn695.Header.VisiblePosition = 0;
            ultraGridColumn696.Header.VisiblePosition = 1;
            ultraGridColumn697.Header.VisiblePosition = 2;
            ultraGridColumn698.Header.VisiblePosition = 3;
            ultraGridBand37.Columns.AddRange(new object[] {
            ultraGridColumn695,
            ultraGridColumn696,
            ultraGridColumn697,
            ultraGridColumn698});
            ultraGridColumn699.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn699.Header.VisiblePosition = 0;
            ultraGridColumn700.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn700.Header.VisiblePosition = 1;
            ultraGridColumn701.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn701.Header.VisiblePosition = 2;
            ultraGridColumn702.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn702.Header.VisiblePosition = 3;
            ultraGridColumn703.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn703.Header.VisiblePosition = 4;
            ultraGridColumn704.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn704.Header.VisiblePosition = 5;
            ultraGridColumn705.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn705.Header.VisiblePosition = 6;
            ultraGridColumn706.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn706.Header.VisiblePosition = 7;
            ultraGridColumn707.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn707.Header.VisiblePosition = 8;
            ultraGridColumn708.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn708.Header.VisiblePosition = 9;
            ultraGridColumn709.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn709.Header.VisiblePosition = 10;
            ultraGridColumn710.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn710.Header.VisiblePosition = 11;
            ultraGridColumn711.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn711.Header.VisiblePosition = 12;
            ultraGridColumn712.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn712.Header.VisiblePosition = 13;
            ultraGridBand38.Columns.AddRange(new object[] {
            ultraGridColumn699,
            ultraGridColumn700,
            ultraGridColumn701,
            ultraGridColumn702,
            ultraGridColumn703,
            ultraGridColumn704,
            ultraGridColumn705,
            ultraGridColumn706,
            ultraGridColumn707,
            ultraGridColumn708,
            ultraGridColumn709,
            ultraGridColumn710,
            ultraGridColumn711,
            ultraGridColumn712});
            ultraGridColumn713.Header.VisiblePosition = 0;
            ultraGridColumn714.Header.VisiblePosition = 1;
            ultraGridColumn715.Header.VisiblePosition = 2;
            ultraGridColumn716.Header.VisiblePosition = 3;
            ultraGridBand39.Columns.AddRange(new object[] {
            ultraGridColumn713,
            ultraGridColumn714,
            ultraGridColumn715,
            ultraGridColumn716});
            ultraGridColumn717.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn717.Header.VisiblePosition = 0;
            ultraGridColumn718.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn718.Header.VisiblePosition = 1;
            ultraGridColumn719.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn719.Header.VisiblePosition = 2;
            ultraGridColumn720.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn720.Header.VisiblePosition = 3;
            ultraGridColumn721.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn721.Header.VisiblePosition = 4;
            ultraGridColumn722.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn722.Header.VisiblePosition = 5;
            ultraGridColumn723.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn723.Header.VisiblePosition = 6;
            ultraGridColumn724.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn724.Header.VisiblePosition = 7;
            ultraGridColumn725.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn725.Header.VisiblePosition = 8;
            ultraGridColumn726.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn726.Header.VisiblePosition = 9;
            ultraGridColumn727.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn727.Header.VisiblePosition = 10;
            ultraGridColumn728.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn728.Header.VisiblePosition = 11;
            ultraGridColumn729.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn729.Header.VisiblePosition = 12;
            ultraGridColumn730.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn730.Header.VisiblePosition = 13;
            ultraGridColumn731.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn731.Header.VisiblePosition = 14;
            ultraGridColumn732.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn732.Header.VisiblePosition = 15;
            ultraGridBand40.Columns.AddRange(new object[] {
            ultraGridColumn717,
            ultraGridColumn718,
            ultraGridColumn719,
            ultraGridColumn720,
            ultraGridColumn721,
            ultraGridColumn722,
            ultraGridColumn723,
            ultraGridColumn724,
            ultraGridColumn725,
            ultraGridColumn726,
            ultraGridColumn727,
            ultraGridColumn728,
            ultraGridColumn729,
            ultraGridColumn730,
            ultraGridColumn731,
            ultraGridColumn732});
            ultraGridColumn733.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn733.Header.VisiblePosition = 0;
            ultraGridColumn734.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn734.Header.VisiblePosition = 1;
            ultraGridColumn735.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn735.Header.VisiblePosition = 2;
            ultraGridColumn736.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn736.Header.VisiblePosition = 3;
            ultraGridBand41.Columns.AddRange(new object[] {
            ultraGridColumn733,
            ultraGridColumn734,
            ultraGridColumn735,
            ultraGridColumn736});
            this.ultraComboCulvertsMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand35);
            this.ultraComboCulvertsMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand36);
            this.ultraComboCulvertsMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand37);
            this.ultraComboCulvertsMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand38);
            this.ultraComboCulvertsMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand39);
            this.ultraComboCulvertsMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand40);
            this.ultraComboCulvertsMaterial.DisplayLayout.BandsSerializer.Add(ultraGridBand41);
            this.ultraComboCulvertsMaterial.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraComboCulvertsMaterial.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance34.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance34.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboCulvertsMaterial.DisplayLayout.GroupByBox.Appearance = appearance34;
            appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboCulvertsMaterial.DisplayLayout.GroupByBox.BandLabelAppearance = appearance35;
            this.ultraComboCulvertsMaterial.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance36.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance36.BackColor2 = System.Drawing.SystemColors.Control;
            appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance36.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraComboCulvertsMaterial.DisplayLayout.GroupByBox.PromptAppearance = appearance36;
            this.ultraComboCulvertsMaterial.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraComboCulvertsMaterial.DisplayLayout.MaxRowScrollRegions = 1;
            appearance45.BackColor = System.Drawing.SystemColors.Window;
            appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.ActiveCellAppearance = appearance45;
            appearance40.BackColor = System.Drawing.SystemColors.Highlight;
            appearance40.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.ActiveRowAppearance = appearance40;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance39.BackColor = System.Drawing.SystemColors.Window;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.CardAreaAppearance = appearance39;
            appearance38.BorderColor = System.Drawing.Color.Silver;
            appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.CellAppearance = appearance38;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.CellPadding = 0;
            appearance42.BackColor = System.Drawing.SystemColors.Control;
            appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance42.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance42.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.GroupByRowAppearance = appearance42;
            appearance44.TextHAlignAsString = "Left";
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.HeaderAppearance = appearance44;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance43.BackColor = System.Drawing.SystemColors.Window;
            appearance43.BorderColor = System.Drawing.Color.Silver;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.RowAppearance = appearance43;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance41.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraComboCulvertsMaterial.DisplayLayout.Override.TemplateAddRowAppearance = appearance41;
            this.ultraComboCulvertsMaterial.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraComboCulvertsMaterial.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraComboCulvertsMaterial.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraComboCulvertsMaterial.DisplayMember = "material";
            this.ultraComboCulvertsMaterial.Location = new System.Drawing.Point(838, 77);
            this.ultraComboCulvertsMaterial.Name = "ultraComboCulvertsMaterial";
            this.ultraComboCulvertsMaterial.Size = new System.Drawing.Size(100, 22);
            this.ultraComboCulvertsMaterial.TabIndex = 47;
            this.ultraComboCulvertsMaterial.Text = "ultraComboCulvertsMaterial";
            this.ultraComboCulvertsMaterial.ValueMember = "material_type_id";
            // 
            // ultraNumericEditorCulvertsUnobstructedHeight
            // 
            this.ultraNumericEditorCulvertsUnobstructedHeight.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "unobstructed_height_in", true));
            this.ultraNumericEditorCulvertsUnobstructedHeight.Location = new System.Drawing.Point(712, 77);
            this.ultraNumericEditorCulvertsUnobstructedHeight.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorCulvertsUnobstructedHeight.Name = "ultraNumericEditorCulvertsUnobstructedHeight";
            this.ultraNumericEditorCulvertsUnobstructedHeight.Nullable = true;
            this.ultraNumericEditorCulvertsUnobstructedHeight.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorCulvertsUnobstructedHeight.Size = new System.Drawing.Size(120, 21);
            this.ultraNumericEditorCulvertsUnobstructedHeight.TabIndex = 46;
            // 
            // ultraNumericEditorCulvertsFullDepth
            // 
            this.ultraNumericEditorCulvertsFullDepth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKCULVERTSURVEYPAGEBindingSource1, "full_diam_in", true));
            this.ultraNumericEditorCulvertsFullDepth.Location = new System.Drawing.Point(616, 77);
            this.ultraNumericEditorCulvertsFullDepth.MaskInput = "{LOC}-nnnnnnnnnn.nn";
            this.ultraNumericEditorCulvertsFullDepth.Name = "ultraNumericEditorCulvertsFullDepth";
            this.ultraNumericEditorCulvertsFullDepth.Nullable = true;
            this.ultraNumericEditorCulvertsFullDepth.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.ultraNumericEditorCulvertsFullDepth.Size = new System.Drawing.Size(90, 21);
            this.ultraNumericEditorCulvertsFullDepth.TabIndex = 45;
            // 
            // buttonUpdateCulvert
            // 
            this.buttonUpdateCulvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateCulvert.Location = new System.Drawing.Point(940, 139);
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
            this.buttonCulvertsViewAddPhotos.Location = new System.Drawing.Point(482, 139);
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
            this.comboBoxCulvertsType.Location = new System.Drawing.Point(942, 77);
            this.comboBoxCulvertsType.Name = "comboBoxCulvertsType";
            this.comboBoxCulvertsType.Size = new System.Drawing.Size(90, 21);
            this.comboBoxCulvertsType.TabIndex = 48;
            this.comboBoxCulvertsType.ValueMember = "culvert_opening_type_id";
            // 
            // sWSPCULVERTOPENINGTYPEBindingSource
            // 
            this.sWSPCULVERTOPENINGTYPEBindingSource.DataMember = "SWSP_CULVERT_OPENING_TYPE";
            this.sWSPCULVERTOPENINGTYPEBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // textBoxCulvertsMeasuredNode
            // 
            this.textBoxCulvertsMeasuredNode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fKCULVERTSURVEYPAGEBindingSource1, "node", true));
            this.textBoxCulvertsMeasuredNode.Location = new System.Drawing.Point(433, 79);
            this.textBoxCulvertsMeasuredNode.Name = "textBoxCulvertsMeasuredNode";
            this.textBoxCulvertsMeasuredNode.Size = new System.Drawing.Size(78, 20);
            this.textBoxCulvertsMeasuredNode.TabIndex = 43;
            this.textBoxCulvertsMeasuredNode.TextChanged += new System.EventHandler(this.textBoxCulvertsNode_TextChanged);
            this.textBoxCulvertsMeasuredNode.Enter += new System.EventHandler(this.textBoxCulvertsNode_Enter);
            // 
            // labelCulvertsMeasuredNode
            // 
            this.labelCulvertsMeasuredNode.AutoSize = true;
            this.labelCulvertsMeasuredNode.Location = new System.Drawing.Point(430, 63);
            this.labelCulvertsMeasuredNode.Name = "labelCulvertsMeasuredNode";
            this.labelCulvertsMeasuredNode.Size = new System.Drawing.Size(81, 13);
            this.labelCulvertsMeasuredNode.TabIndex = 41;
            this.labelCulvertsMeasuredNode.Text = "Measured node";
            // 
            // labelCulvertsFacingDirection
            // 
            this.labelCulvertsFacingDirection.AutoSize = true;
            this.labelCulvertsFacingDirection.Location = new System.Drawing.Point(357, 8);
            this.labelCulvertsFacingDirection.Name = "labelCulvertsFacingDirection";
            this.labelCulvertsFacingDirection.Size = new System.Drawing.Size(84, 13);
            this.labelCulvertsFacingDirection.TabIndex = 40;
            this.labelCulvertsFacingDirection.Text = "Facing Direction";
            this.labelCulvertsFacingDirection.Visible = false;
            // 
            // labelCulvertsMaterial
            // 
            this.labelCulvertsMaterial.AutoSize = true;
            this.labelCulvertsMaterial.Location = new System.Drawing.Point(835, 61);
            this.labelCulvertsMaterial.Name = "labelCulvertsMaterial";
            this.labelCulvertsMaterial.Size = new System.Drawing.Size(44, 13);
            this.labelCulvertsMaterial.TabIndex = 38;
            this.labelCulvertsMaterial.Text = "Material";
            // 
            // labelCulvertsUnobstructedHeight
            // 
            this.labelCulvertsUnobstructedHeight.AutoSize = true;
            this.labelCulvertsUnobstructedHeight.Location = new System.Drawing.Point(709, 61);
            this.labelCulvertsUnobstructedHeight.Name = "labelCulvertsUnobstructedHeight";
            this.labelCulvertsUnobstructedHeight.Size = new System.Drawing.Size(120, 13);
            this.labelCulvertsUnobstructedHeight.TabIndex = 37;
            this.labelCulvertsUnobstructedHeight.Text = "Unobstructed height (in)";
            // 
            // labelCulvertsFullDepth
            // 
            this.labelCulvertsFullDepth.AutoSize = true;
            this.labelCulvertsFullDepth.Location = new System.Drawing.Point(613, 61);
            this.labelCulvertsFullDepth.Name = "labelCulvertsFullDepth";
            this.labelCulvertsFullDepth.Size = new System.Drawing.Size(70, 13);
            this.labelCulvertsFullDepth.TabIndex = 36;
            this.labelCulvertsFullDepth.Text = "Full depth (in)";
            // 
            // labelCulvertsShape
            // 
            this.labelCulvertsShape.AutoSize = true;
            this.labelCulvertsShape.Location = new System.Drawing.Point(514, 61);
            this.labelCulvertsShape.Name = "labelCulvertsShape";
            this.labelCulvertsShape.Size = new System.Drawing.Size(38, 13);
            this.labelCulvertsShape.TabIndex = 35;
            this.labelCulvertsShape.Text = "Shape";
            // 
            // labelCulvertsType
            // 
            this.labelCulvertsType.AutoSize = true;
            this.labelCulvertsType.Location = new System.Drawing.Point(939, 61);
            this.labelCulvertsType.Name = "labelCulvertsType";
            this.labelCulvertsType.Size = new System.Drawing.Size(40, 13);
            this.labelCulvertsType.TabIndex = 34;
            this.labelCulvertsType.Text = "L, P, O";
            // 
            // buttonCulvertsAdd
            // 
            this.buttonCulvertsAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCulvertsAdd.Location = new System.Drawing.Point(257, 139);
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
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.nodeDataGridViewTextBoxColumn1});
            this.dataGridViewCulverts.DataSource = this.fKCULVERTSURVEYPAGEBindingSource1;
            this.dataGridViewCulverts.Location = new System.Drawing.Point(3, 8);
            this.dataGridViewCulverts.Name = "dataGridViewCulverts";
            this.dataGridViewCulverts.ReadOnly = true;
            this.dataGridViewCulverts.Size = new System.Drawing.Size(248, 159);
            this.dataGridViewCulverts.TabIndex = 21;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "us_node";
            this.dataGridViewTextBoxColumn1.HeaderText = "us_node";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 66;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ds_node";
            this.dataGridViewTextBoxColumn2.HeaderText = "ds_node";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 66;
            // 
            // nodeDataGridViewTextBoxColumn1
            // 
            this.nodeDataGridViewTextBoxColumn1.DataPropertyName = "node";
            this.nodeDataGridViewTextBoxColumn1.HeaderText = "Measured";
            this.nodeDataGridViewTextBoxColumn1.Name = "nodeDataGridViewTextBoxColumn1";
            this.nodeDataGridViewTextBoxColumn1.ReadOnly = true;
            this.nodeDataGridViewTextBoxColumn1.Width = 66;
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
            this.menuStripMainForm.Size = new System.Drawing.Size(1051, 24);
            this.menuStripMainForm.TabIndex = 10;
            this.menuStripMainForm.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportReportToolStripMenuItem,
            this.updateMstlinksacToolStripMenuItem});
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fIleToolStripMenuItem.Text = "FIle";
            // 
            // exportReportToolStripMenuItem
            // 
            this.exportReportToolStripMenuItem.Name = "exportReportToolStripMenuItem";
            this.exportReportToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.exportReportToolStripMenuItem.Text = "Export Report";
            this.exportReportToolStripMenuItem.Click += new System.EventHandler(this.exportReportToolStripMenuItem_Click);
            // 
            // updateMstlinksacToolStripMenuItem
            // 
            this.updateMstlinksacToolStripMenuItem.Name = "updateMstlinksacToolStripMenuItem";
            this.updateMstlinksacToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.updateMstlinksacToolStripMenuItem.Text = "Update mst_links_ac";
            this.updateMstlinksacToolStripMenuItem.Click += new System.EventHandler(this.updateMstlinksacToolStripMenuItem_Click_1);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataAdministratorToolStripMenuItem,
            this.surveyViewToolStripMenuItem});
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
            // surveyViewToolStripMenuItem
            // 
            this.surveyViewToolStripMenuItem.Name = "surveyViewToolStripMenuItem";
            this.surveyViewToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.surveyViewToolStripMenuItem.Text = "Survey View";
            this.surveyViewToolStripMenuItem.Click += new System.EventHandler(this.surveyViewToolStripMenuItem_Click);
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
            this.labelWatershed.Location = new System.Drawing.Point(820, 27);
            this.labelWatershed.Name = "labelWatershed";
            this.labelWatershed.Size = new System.Drawing.Size(59, 13);
            this.labelWatershed.TabIndex = 13;
            this.labelWatershed.Text = "Watershed";
            // 
            // labelSubwatershed
            // 
            this.labelSubwatershed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSubwatershed.AutoSize = true;
            this.labelSubwatershed.Location = new System.Drawing.Point(962, 31);
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
            this.buttonUpdateDatabase.Location = new System.Drawing.Point(906, 471);
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
            this.textBoxComments.Size = new System.Drawing.Size(1026, 118);
            this.textBoxComments.TabIndex = 6;
            // 
            // comboBoxWatershed
            // 
            this.comboBoxWatershed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWatershed.DataSource = this.sWSPWATERSHEDBindingSource;
            this.comboBoxWatershed.DisplayMember = "watershed";
            this.comboBoxWatershed.FormattingEnabled = true;
            this.comboBoxWatershed.Location = new System.Drawing.Point(769, 47);
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
            this.comboBoxSubwatershed.Location = new System.Drawing.Point(916, 46);
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
            // ultraDateTimeEditorSurveyDate
            // 
            this.ultraDateTimeEditorSurveyDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.fKSURVEYPAGEVIEWBindingSource, "date", true));
            this.ultraDateTimeEditorSurveyDate.Location = new System.Drawing.Point(11, 97);
            this.ultraDateTimeEditorSurveyDate.Name = "ultraDateTimeEditorSurveyDate";
            this.ultraDateTimeEditorSurveyDate.Size = new System.Drawing.Size(144, 21);
            this.ultraDateTimeEditorSurveyDate.TabIndex = 103;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 471);
            this.panel1.TabIndex = 104;
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
            // sWSPFACINGTYPEBindingSource1
            // 
            this.sWSPFACINGTYPEBindingSource1.DataMember = "SWSP_FACING_TYPE";
            this.sWSPFACINGTYPEBindingSource1.DataSource = this.sANDBOXDataSet;
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
            this.ClientSize = new System.Drawing.Size(1051, 501);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ultraDateTimeEditorSurveyDate);
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
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesInnerWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKPIPESURVEYPAGEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboPipesShape)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPSHAPETYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboPipesMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPMATERIALTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesInnerDiameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesDSDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorPipesUSDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPipes)).EndInit();
            this.tabPageDitches.ResumeLayout(false);
            this.tabPageDitches.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKDITCHSURVEYPAGEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboDitchesFacingDirection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboDitchesMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesBottomWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesTopWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraNumericEditorDitchesDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDitches)).EndInit();
            this.tabPageCulverts.ResumeLayout(false);
            this.tabPageCulverts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fKCULVERTSURVEYPAGEBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboCulvertsShape)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboCulvertsFacingDirection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboCulvertsMaterial)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.ultraDateTimeEditorSurveyDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPGLOBALIDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPFACINGTYPEBindingSource1)).EndInit();
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
        private System.Windows.Forms.TextBox textBoxCulvertsMeasuredNode;
        private System.Windows.Forms.Label labelCulvertsMeasuredNode;
        private System.Windows.Forms.Label labelCulvertsFacingDirection;
        private System.Windows.Forms.TextBox textBoxDitchesMeasuredNode;
        private System.Windows.Forms.Label labelDitchesMeasuredNode;
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
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraComboPipesMaterial;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraComboCulvertsMaterial;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraComboDitchesMaterial;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraComboDitchesFacingDirection;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraComboCulvertsFacingDirection;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraComboPipesShape;
        private Infragistics.Win.UltraWinGrid.UltraCombo ultraComboCulvertsShape;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor ultraDateTimeEditorSurveyDate;
        private Infragistics.Win.Misc.UltraLabel ultraLabelInnerWidth;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor ultraNumericEditorPipesInnerWidth;
        private System.Windows.Forms.ToolStripMenuItem exportReportToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxCulvertsDSNode;
        private System.Windows.Forms.Label labelCulvertsDSNode;
        private System.Windows.Forms.TextBox textBoxCulvertsUSNode;
        private System.Windows.Forms.Label labelCulvertsUSNode;
        private System.Windows.Forms.TextBox textBoxDitchesUSNode;
        private System.Windows.Forms.Label labelDitchesUSNode;
        private System.Windows.Forms.TextBox textBoxDitchesDSNode;
        private System.Windows.Forms.Label labelDitchesDSNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn us_node;
        private System.Windows.Forms.DataGridViewTextBoxColumn ds_node;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem surveyViewToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource sWSPGLOBALIDBindingSource;
        private System.Windows.Forms.ToolStripMenuItem updateMstlinksacToolStripMenuItem;
    }
}

