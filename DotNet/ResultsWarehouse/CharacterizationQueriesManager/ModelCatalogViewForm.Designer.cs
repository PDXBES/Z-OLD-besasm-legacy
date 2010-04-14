namespace SystemsAnalysis.Characterization
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ModelScenario_ModelCatalog");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ModelScenario_LinkHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ModelScenario_NodeHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ModelScenario_DscHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ModelScenario_ModelCatalog", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StudyArea");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeFrame");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelPath");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelOutputFile");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsUploaded");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UploadDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ModelCatalog_LinkHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ModelCatalog_NodeHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ModelCatalog_DscHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ModelCatalog_LinkHydraulics", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LinkHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MLinkID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxQ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxQ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxV");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxV");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("QQRatio");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxUsElev");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxDsElev");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ModelCatalog_NodeHydraulics", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NodeHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NodeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxElevation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxElev");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Surcharge");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Freeboard");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SurchargeTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FloodedTime");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ModelCatalog_DscHydraulics", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DscHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DscID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HGL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeltaHGL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Surcharge");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ModelScenario_LinkHydraulics", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LinkHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MLinkID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxQ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxQ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxV");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxV");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("QQRatio");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxUsElev");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxDsElev");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand7 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ModelScenario_NodeHydraulics", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NodeHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NodeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxElevation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxElev");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Surcharge");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Freeboard");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SurchargeTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FloodedTime");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand8 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ModelScenario_DscHydraulics", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DscHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DscID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn75 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn76 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HGL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn77 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeltaHGL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn78 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Surcharge");
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand9 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ModelCatalog", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn79 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn80 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn81 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn82 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StudyArea");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn83 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn84 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn85 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeFrame");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn86 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelPath");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn87 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelOutputFile");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn88 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn89 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsUploaded");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn90 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UploadDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn91 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ModelCatalog_LinkHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn92 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ModelCatalog_NodeHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn93 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ModelCatalog_DscHydraulics");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand10 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ModelCatalog_LinkHydraulics", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn94 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LinkHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn95 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn96 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MLinkID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn97 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn98 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxQ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn99 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxQ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn100 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxV");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn101 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxV");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn102 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("QQRatio");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn103 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxUsElev");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn104 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxDsElev");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand11 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ModelCatalog_NodeHydraulics", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn105 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NodeHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn106 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn107 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn108 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NodeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn109 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxElevation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn110 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfMaxElev");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn111 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Surcharge");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn112 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Freeboard");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn113 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SurchargeTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn114 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FloodedTime");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand12 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ModelCatalog_DscHydraulics", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn115 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DscHydraulicsID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn116 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScenarioID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn117 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DscID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn118 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn119 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HGL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn120 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeltaHGL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn121 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Surcharge");
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
            this.btnAddModel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn6.Header.VisiblePosition = 6;
            ultraGridColumn7.Header.VisiblePosition = 4;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7});
            ultraGridBand1.Header.Enabled = false;
            ultraGridColumn8.Header.VisiblePosition = 0;
            ultraGridColumn8.Width = 28;
            ultraGridColumn9.Header.VisiblePosition = 1;
            ultraGridColumn9.Width = 53;
            ultraGridColumn10.Header.VisiblePosition = 2;
            ultraGridColumn10.Width = 62;
            ultraGridColumn11.Header.VisiblePosition = 3;
            ultraGridColumn11.Width = 62;
            ultraGridColumn12.Header.VisiblePosition = 4;
            ultraGridColumn12.Width = 69;
            ultraGridColumn13.Header.VisiblePosition = 5;
            ultraGridColumn13.Width = 62;
            ultraGridColumn14.Header.VisiblePosition = 6;
            ultraGridColumn14.Width = 62;
            ultraGridColumn15.Header.VisiblePosition = 7;
            ultraGridColumn15.Width = 62;
            ultraGridColumn16.Header.VisiblePosition = 8;
            ultraGridColumn16.Width = 66;
            ultraGridColumn17.Header.VisiblePosition = 9;
            ultraGridColumn17.Width = 62;
            ultraGridColumn18.Header.VisiblePosition = 10;
            ultraGridColumn18.Width = 50;
            ultraGridColumn19.Header.VisiblePosition = 11;
            ultraGridColumn19.Width = 54;
            ultraGridColumn20.Header.VisiblePosition = 13;
            ultraGridColumn21.Header.VisiblePosition = 14;
            ultraGridColumn22.Header.VisiblePosition = 12;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22});
            ultraGridColumn23.Header.VisiblePosition = 0;
            ultraGridColumn23.Width = 72;
            ultraGridColumn24.Header.VisiblePosition = 1;
            ultraGridColumn24.Width = 63;
            ultraGridColumn25.Header.VisiblePosition = 2;
            ultraGridColumn25.Width = 48;
            ultraGridColumn26.Header.VisiblePosition = 3;
            ultraGridColumn26.Width = 49;
            ultraGridColumn27.Header.VisiblePosition = 4;
            ultraGridColumn27.Width = 61;
            ultraGridColumn28.Header.VisiblePosition = 5;
            ultraGridColumn28.Width = 69;
            ultraGridColumn29.Header.VisiblePosition = 6;
            ultraGridColumn29.Width = 61;
            ultraGridColumn30.Header.VisiblePosition = 7;
            ultraGridColumn30.Width = 67;
            ultraGridColumn31.Header.VisiblePosition = 8;
            ultraGridColumn31.Width = 61;
            ultraGridColumn32.Header.VisiblePosition = 9;
            ultraGridColumn32.Width = 61;
            ultraGridColumn33.Header.VisiblePosition = 10;
            ultraGridColumn33.Width = 61;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33});
            ultraGridColumn34.Header.VisiblePosition = 0;
            ultraGridColumn34.Width = 78;
            ultraGridColumn35.Header.VisiblePosition = 1;
            ultraGridColumn35.Width = 63;
            ultraGridColumn36.Header.VisiblePosition = 2;
            ultraGridColumn36.Width = 49;
            ultraGridColumn37.Header.VisiblePosition = 3;
            ultraGridColumn37.Width = 75;
            ultraGridColumn38.Header.VisiblePosition = 4;
            ultraGridColumn38.Width = 68;
            ultraGridColumn39.Header.VisiblePosition = 5;
            ultraGridColumn39.Width = 78;
            ultraGridColumn40.Header.VisiblePosition = 6;
            ultraGridColumn40.Width = 60;
            ultraGridColumn41.Header.VisiblePosition = 7;
            ultraGridColumn41.Width = 60;
            ultraGridColumn42.Header.VisiblePosition = 8;
            ultraGridColumn42.Width = 75;
            ultraGridColumn43.Header.VisiblePosition = 9;
            ultraGridColumn43.Width = 67;
            ultraGridBand4.Columns.AddRange(new object[] {
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
            ultraGridColumn44.Header.VisiblePosition = 0;
            ultraGridColumn44.Width = 129;
            ultraGridColumn45.Header.VisiblePosition = 1;
            ultraGridColumn45.Width = 101;
            ultraGridColumn46.Header.VisiblePosition = 2;
            ultraGridColumn46.Width = 70;
            ultraGridColumn47.Header.VisiblePosition = 3;
            ultraGridColumn47.Width = 79;
            ultraGridColumn48.Header.VisiblePosition = 4;
            ultraGridColumn48.Width = 98;
            ultraGridColumn49.Header.VisiblePosition = 5;
            ultraGridColumn49.Width = 98;
            ultraGridColumn50.Header.VisiblePosition = 6;
            ultraGridColumn50.Width = 98;
            ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49,
            ultraGridColumn50});
            ultraGridColumn51.Header.VisiblePosition = 0;
            ultraGridColumn51.Width = 77;
            ultraGridColumn52.Header.VisiblePosition = 1;
            ultraGridColumn52.Width = 65;
            ultraGridColumn53.Header.VisiblePosition = 2;
            ultraGridColumn53.Width = 50;
            ultraGridColumn54.Header.VisiblePosition = 3;
            ultraGridColumn54.Width = 50;
            ultraGridColumn55.Header.VisiblePosition = 4;
            ultraGridColumn55.Width = 62;
            ultraGridColumn56.Header.VisiblePosition = 5;
            ultraGridColumn56.Width = 71;
            ultraGridColumn57.Header.VisiblePosition = 6;
            ultraGridColumn57.Width = 62;
            ultraGridColumn58.Header.VisiblePosition = 7;
            ultraGridColumn58.Width = 69;
            ultraGridColumn59.Header.VisiblePosition = 8;
            ultraGridColumn59.Width = 62;
            ultraGridColumn60.Header.VisiblePosition = 9;
            ultraGridColumn60.Width = 62;
            ultraGridColumn61.Header.VisiblePosition = 10;
            ultraGridColumn61.Width = 62;
            ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn51,
            ultraGridColumn52,
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56,
            ultraGridColumn57,
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn60,
            ultraGridColumn61});
            ultraGridColumn62.Header.VisiblePosition = 0;
            ultraGridColumn62.Width = 80;
            ultraGridColumn63.Header.VisiblePosition = 1;
            ultraGridColumn63.Width = 65;
            ultraGridColumn64.Header.VisiblePosition = 2;
            ultraGridColumn64.Width = 50;
            ultraGridColumn65.Header.VisiblePosition = 3;
            ultraGridColumn65.Width = 77;
            ultraGridColumn66.Header.VisiblePosition = 4;
            ultraGridColumn66.Width = 70;
            ultraGridColumn67.Header.VisiblePosition = 5;
            ultraGridColumn67.Width = 80;
            ultraGridColumn68.Header.VisiblePosition = 6;
            ultraGridColumn68.Width = 62;
            ultraGridColumn69.Header.VisiblePosition = 7;
            ultraGridColumn69.Width = 62;
            ultraGridColumn70.Header.VisiblePosition = 8;
            ultraGridColumn70.Width = 77;
            ultraGridColumn71.Header.VisiblePosition = 9;
            ultraGridColumn71.Width = 69;
            ultraGridBand7.Columns.AddRange(new object[] {
            ultraGridColumn62,
            ultraGridColumn63,
            ultraGridColumn64,
            ultraGridColumn65,
            ultraGridColumn66,
            ultraGridColumn67,
            ultraGridColumn68,
            ultraGridColumn69,
            ultraGridColumn70,
            ultraGridColumn71});
            ultraGridColumn72.Header.VisiblePosition = 0;
            ultraGridColumn72.Width = 132;
            ultraGridColumn73.Header.VisiblePosition = 1;
            ultraGridColumn73.Width = 104;
            ultraGridColumn74.Header.VisiblePosition = 2;
            ultraGridColumn74.Width = 72;
            ultraGridColumn75.Header.VisiblePosition = 3;
            ultraGridColumn75.Width = 81;
            ultraGridColumn76.Header.VisiblePosition = 4;
            ultraGridColumn76.Width = 101;
            ultraGridColumn77.Header.VisiblePosition = 5;
            ultraGridColumn77.Width = 101;
            ultraGridColumn78.Header.VisiblePosition = 6;
            ultraGridColumn78.Width = 101;
            ultraGridBand8.Columns.AddRange(new object[] {
            ultraGridColumn72,
            ultraGridColumn73,
            ultraGridColumn74,
            ultraGridColumn75,
            ultraGridColumn76,
            ultraGridColumn77,
            ultraGridColumn78});
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand7);
            this.cmbSelectScenario.DisplayLayout.BandsSerializer.Add(ultraGridBand8);
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
            this.cmbSelectScenario.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
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
            ultraGridColumn79.Header.Caption = "ID";
            ultraGridColumn79.Header.VisiblePosition = 1;
            ultraGridColumn79.Width = 38;
            ultraGridColumn80.Header.Caption = "Scenario ID";
            ultraGridColumn80.Header.VisiblePosition = 0;
            ultraGridColumn80.Hidden = true;
            ultraGridColumn81.Header.Caption = "Model";
            ultraGridColumn81.Header.VisiblePosition = 2;
            ultraGridColumn82.Header.Caption = "Study Area";
            ultraGridColumn82.Header.VisiblePosition = 3;
            ultraGridColumn83.Header.Caption = "Description";
            ultraGridColumn83.Header.VisiblePosition = 5;
            ultraGridColumn83.Hidden = true;
            ultraGridColumn84.Header.Caption = "Model Type";
            ultraGridColumn84.Header.VisiblePosition = 6;
            ultraGridColumn84.Hidden = true;
            ultraGridColumn85.Header.Caption = "Time Frame";
            ultraGridColumn85.Header.VisiblePosition = 7;
            ultraGridColumn85.Hidden = true;
            ultraGridColumn86.Header.Caption = "Path";
            ultraGridColumn86.Header.VisiblePosition = 8;
            ultraGridColumn86.Width = 191;
            ultraGridColumn87.Header.Caption = "Output File";
            ultraGridColumn87.Header.VisiblePosition = 9;
            ultraGridColumn87.Hidden = true;
            ultraGridColumn88.Header.Caption = "Modeler";
            ultraGridColumn88.Header.VisiblePosition = 4;
            ultraGridColumn89.Header.Caption = "Is Uploaded?";
            ultraGridColumn89.Header.VisiblePosition = 10;
            ultraGridColumn89.Hidden = true;
            ultraGridColumn90.Header.Caption = "Upload Date";
            ultraGridColumn90.Header.VisiblePosition = 11;
            ultraGridColumn90.Hidden = true;
            ultraGridColumn91.Header.VisiblePosition = 13;
            ultraGridColumn92.Header.VisiblePosition = 14;
            ultraGridColumn93.Header.VisiblePosition = 12;
            ultraGridBand9.Columns.AddRange(new object[] {
            ultraGridColumn79,
            ultraGridColumn80,
            ultraGridColumn81,
            ultraGridColumn82,
            ultraGridColumn83,
            ultraGridColumn84,
            ultraGridColumn85,
            ultraGridColumn86,
            ultraGridColumn87,
            ultraGridColumn88,
            ultraGridColumn89,
            ultraGridColumn90,
            ultraGridColumn91,
            ultraGridColumn92,
            ultraGridColumn93});
            ultraGridColumn94.Header.VisiblePosition = 0;
            ultraGridColumn95.Header.VisiblePosition = 1;
            ultraGridColumn96.Header.VisiblePosition = 2;
            ultraGridColumn97.Header.VisiblePosition = 3;
            ultraGridColumn98.Header.VisiblePosition = 4;
            ultraGridColumn99.Header.VisiblePosition = 5;
            ultraGridColumn100.Header.VisiblePosition = 6;
            ultraGridColumn101.Header.VisiblePosition = 7;
            ultraGridColumn102.Header.VisiblePosition = 8;
            ultraGridColumn103.Header.VisiblePosition = 9;
            ultraGridColumn104.Header.VisiblePosition = 10;
            ultraGridBand10.Columns.AddRange(new object[] {
            ultraGridColumn94,
            ultraGridColumn95,
            ultraGridColumn96,
            ultraGridColumn97,
            ultraGridColumn98,
            ultraGridColumn99,
            ultraGridColumn100,
            ultraGridColumn101,
            ultraGridColumn102,
            ultraGridColumn103,
            ultraGridColumn104});
            ultraGridBand10.Hidden = true;
            ultraGridColumn105.Header.VisiblePosition = 0;
            ultraGridColumn106.Header.VisiblePosition = 1;
            ultraGridColumn107.Header.VisiblePosition = 2;
            ultraGridColumn108.Header.VisiblePosition = 3;
            ultraGridColumn109.Header.VisiblePosition = 4;
            ultraGridColumn110.Header.VisiblePosition = 5;
            ultraGridColumn111.Header.VisiblePosition = 6;
            ultraGridColumn112.Header.VisiblePosition = 7;
            ultraGridColumn113.Header.VisiblePosition = 8;
            ultraGridColumn114.Header.VisiblePosition = 9;
            ultraGridBand11.Columns.AddRange(new object[] {
            ultraGridColumn105,
            ultraGridColumn106,
            ultraGridColumn107,
            ultraGridColumn108,
            ultraGridColumn109,
            ultraGridColumn110,
            ultraGridColumn111,
            ultraGridColumn112,
            ultraGridColumn113,
            ultraGridColumn114});
            ultraGridBand11.Hidden = true;
            ultraGridColumn115.Header.VisiblePosition = 0;
            ultraGridColumn116.Header.VisiblePosition = 1;
            ultraGridColumn117.Header.VisiblePosition = 2;
            ultraGridColumn118.Header.VisiblePosition = 3;
            ultraGridColumn119.Header.VisiblePosition = 4;
            ultraGridColumn120.Header.VisiblePosition = 5;
            ultraGridColumn121.Header.VisiblePosition = 6;
            ultraGridBand12.Columns.AddRange(new object[] {
            ultraGridColumn115,
            ultraGridColumn116,
            ultraGridColumn117,
            ultraGridColumn118,
            ultraGridColumn119,
            ultraGridColumn120,
            ultraGridColumn121});
            ultraGridBand12.Hidden = true;
            this.grdModelCatalog.DisplayLayout.BandsSerializer.Add(ultraGridBand9);
            this.grdModelCatalog.DisplayLayout.BandsSerializer.Add(ultraGridBand10);
            this.grdModelCatalog.DisplayLayout.BandsSerializer.Add(ultraGridBand11);
            this.grdModelCatalog.DisplayLayout.BandsSerializer.Add(ultraGridBand12);
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
            this.btnAddModel.Location = new System.Drawing.Point(141, 303);
            this.btnAddModel.Name = "btnAddModel";
            this.btnAddModel.Size = new System.Drawing.Size(185, 52);
            this.btnAddModel.TabIndex = 25;
            this.btnAddModel.Text = "Import Start/Stop Links From Selected Models";
            this.btnAddModel.Click += new System.EventHandler(this.btnAddModel_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(332, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 52);
            this.button1.TabIndex = 28;
            this.button1.Text = "Cancel";
            // 
            // ModelCatalogViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 367);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbSelectScenario);
            this.Controls.Add(this.lblScenario);
            this.Controls.Add(this.btnAddModel);
            this.Controls.Add(this.grdModelCatalog);
            this.Name = "ModelCatalogViewForm";
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
        private System.Windows.Forms.Button btnAddModel;
        private System.Windows.Forms.Button button1;
        private SystemsAnalysis.DataAccess.ModelCatalogDataSet modelCatalogDS;
    }
}