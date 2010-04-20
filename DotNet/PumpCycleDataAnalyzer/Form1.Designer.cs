namespace PumpCycleDataAnalyzer
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
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Station", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("H2Number");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Address");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StartDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EndDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StatePlaneXFt");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StatePlaneYFt");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CreateDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CreateBy");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UpdateDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UpdateBy");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("STATION_PUMP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("STATION_CYCLE_DATA");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_Station_OperationsData");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_Station_WetWell");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FK_ENVIRON_SENSOR_STATION");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Station_DepthData");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("STATION_PUMP", 0);
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("STATION_CYCLE_DATA", 0);
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_Station_OperationsData", 0);
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_Station_WetWell", 0);
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FK_ENVIRON_SENSOR_STATION", 0);
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand7 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Station_DepthData", 0);
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement1 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.ChartArea chartArea1 = new Infragistics.UltraChart.Resources.Appearance.ChartArea();
            Infragistics.UltraChart.Resources.Appearance.AxisItem axisItem1 = new Infragistics.UltraChart.Resources.Appearance.AxisItem();
            Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior fontScalingAxisLabelLayoutBehavior1 = new Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior wrapTextAxisLabelLayoutBehavior1 = new Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior staggerAxisLabelLayoutBehavior1 = new Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior rotateAxisLabelLayoutBehavior1 = new Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior fontScalingAxisLabelLayoutBehavior2 = new Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior wrapTextAxisLabelLayoutBehavior2 = new Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior staggerAxisLabelLayoutBehavior2 = new Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior rotateAxisLabelLayoutBehavior2 = new Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.ClipTextAxisLabelLayoutBehavior clipTextAxisLabelLayoutBehavior1 = new Infragistics.UltraChart.Resources.Appearance.ClipTextAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.AxisItem axisItem2 = new Infragistics.UltraChart.Resources.Appearance.AxisItem();
            Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior fontScalingAxisLabelLayoutBehavior3 = new Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior wrapTextAxisLabelLayoutBehavior3 = new Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior staggerAxisLabelLayoutBehavior3 = new Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior rotateAxisLabelLayoutBehavior3 = new Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior fontScalingAxisLabelLayoutBehavior4 = new Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior wrapTextAxisLabelLayoutBehavior4 = new Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior staggerAxisLabelLayoutBehavior4 = new Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior rotateAxisLabelLayoutBehavior4 = new Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.ClipTextAxisLabelLayoutBehavior clipTextAxisLabelLayoutBehavior2 = new Infragistics.UltraChart.Resources.Appearance.ClipTextAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.AxisItem axisItem3 = new Infragistics.UltraChart.Resources.Appearance.AxisItem();
            Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior fontScalingAxisLabelLayoutBehavior5 = new Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior wrapTextAxisLabelLayoutBehavior5 = new Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior staggerAxisLabelLayoutBehavior5 = new Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior rotateAxisLabelLayoutBehavior5 = new Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior fontScalingAxisLabelLayoutBehavior6 = new Infragistics.UltraChart.Resources.Appearance.FontScalingAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior wrapTextAxisLabelLayoutBehavior6 = new Infragistics.UltraChart.Resources.Appearance.WrapTextAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior staggerAxisLabelLayoutBehavior6 = new Infragistics.UltraChart.Resources.Appearance.StaggerAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior rotateAxisLabelLayoutBehavior6 = new Infragistics.UltraChart.Resources.Appearance.RotateAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.ClipTextAxisLabelLayoutBehavior clipTextAxisLabelLayoutBehavior3 = new Infragistics.UltraChart.Resources.Appearance.ClipTextAxisLabelLayoutBehavior();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement2 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement3 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.ChartArea chartArea2 = new Infragistics.UltraChart.Resources.Appearance.ChartArea();
            Infragistics.UltraChart.Resources.Appearance.AxisItem axisItem4 = new Infragistics.UltraChart.Resources.Appearance.AxisItem();
            Infragistics.UltraChart.Resources.Appearance.AxisItem axisItem5 = new Infragistics.UltraChart.Resources.Appearance.AxisItem();
            Infragistics.UltraChart.Resources.Appearance.AxisItem axisItem6 = new Infragistics.UltraChart.Resources.Appearance.AxisItem();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement4 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement5 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.ChartLayerAppearance chartLayerAppearance1 = new Infragistics.UltraChart.Resources.Appearance.ChartLayerAppearance();
            Infragistics.UltraChart.Resources.Appearance.ChartLayerAppearance chartLayerAppearance2 = new Infragistics.UltraChart.Resources.Appearance.ChartLayerAppearance();
            Infragistics.UltraChart.Resources.Appearance.ChartLayerAppearance chartLayerAppearance3 = new Infragistics.UltraChart.Resources.Appearance.ChartLayerAppearance();
            Infragistics.UltraChart.Resources.Appearance.CompositeLegend compositeLegend1 = new Infragistics.UltraChart.Resources.Appearance.CompositeLegend();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeSeries numericTimeSeries1 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeSeries();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement6 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint1 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement7 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint2 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement8 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint3 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement9 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint4 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement10 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint5 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement11 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeSeries numericTimeSeries2 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeSeries();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement12 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint6 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement13 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint7 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement14 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint8 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement15 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint9 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement16 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint10 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement17 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeSeries numericTimeSeries3 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeSeries();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeSeries numericTimeSeries4 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeSeries();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement18 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint11 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement19 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint12 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement20 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint13 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement21 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint14 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement22 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint15 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement23 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeSeries numericTimeSeries5 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeSeries();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement24 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint16 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement25 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint17 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement26 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint numericTimeDataPoint18 = new Infragistics.UltraChart.Resources.Appearance.NumericTimeDataPoint();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement27 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.GradientEffect gradientEffect1 = new Infragistics.UltraChart.Resources.Appearance.GradientEffect();
            Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
            Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand10 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CycleSummary", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CycleSummaryID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PumpID", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CycleCount");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PumpOnDuration");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PumpOffDuration");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CalculatedPumpingRateGPM");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PumpOnPercent");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AverageCycleTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReportedPumpingRateGPM");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand11 = new Infragistics.Win.UltraWinGrid.UltraGridBand("OperationsData", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OperationsDataID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CalendarDateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfDayID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Channel");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MessageTime", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MessageText");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SourceFileName");
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand12 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CycleData", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CycleDataID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CalendarDateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfDayID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationID", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PumpID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CycleChangeTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OnOffState");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SourceFilename");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand13 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DepthData", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DepthDataID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CalendarDateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOfDayID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EnvironSensorID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn75 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationQualifier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn76 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OriginalSampleTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn77 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedSampleTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn78 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RawValue");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn79 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinalValue");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn80 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Units");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn81 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn82 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SourceFilename");
            Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand14 = new Infragistics.Win.UltraWinGrid.UltraGridBand("WetWell", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn83 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WetWellID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn84 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn85 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WetWellVolumeGallons");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn86 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WetWellType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn87 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StartDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn88 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EndDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn89 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CreateDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn90 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CreateBy");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn91 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UpdateDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn92 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UpdateBy");
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand8 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Raw", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CycleNum");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOn");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOff");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PumpRun");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Inflow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pump1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pump2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pump3");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pump4");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Multi");
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand9 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Calculated", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOn");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeOff");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeAvg");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PumpDownTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FillTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InflowRate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PumpRate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Multi");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Flagged");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PumpNum");
            Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab(true);
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton3 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
            Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton4 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.grdStationList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.pumpCycleDS = new PumpCycleDataAnalyzer.PumpCycleDataSet();
            this.chkRetrieveWetWellLevel = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.ultraLabel35 = new Infragistics.Win.Misc.UltraLabel();
            this.txtUniformInterval2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.chkUniformInterval = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ultraChart1 = new Infragistics.Win.UltraWinChart.UltraChart();
            this.btnExportChart = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel32 = new Infragistics.Win.Misc.UltraLabel();
            this.txtAverageInflow = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtAnalysisDuration = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel20 = new Infragistics.Win.Misc.UltraLabel();
            this.btnProcessCycleData = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel33 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel31 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel17 = new Infragistics.Win.Misc.UltraLabel();
            this.txtMultiPumpRunTime = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel16 = new Infragistics.Win.Misc.UltraLabel();
            this.txtStationAverageCycleTime = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel15 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel22 = new Infragistics.Win.Misc.UltraLabel();
            this.cmbEndDateTime = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
            this.ultraCalendarInfo1 = new Infragistics.Win.UltraWinSchedule.UltraCalendarInfo(this.components);
            this.ultraCalendarLook1 = new Infragistics.Win.UltraWinSchedule.UltraCalendarLook(this.components);
            this.cmbBeginDateTime = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPumpSummary = new System.Windows.Forms.TabPage();
            this.ultraGrid5 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cycleSummaryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabMessages = new System.Windows.Forms.TabPage();
            this.ultraGrid6 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.operationsDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabRawCycleData = new System.Windows.Forms.TabPage();
            this.ultraGrid3 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cycleDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabWetWellDepth = new System.Windows.Forms.TabPage();
            this.ultraGrid7 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.tabWetWellInfo = new System.Windows.Forms.TabPage();
            this.ultraGrid8 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraLabel34 = new Infragistics.Win.Misc.UltraLabel();
            this.txtLeadActiveWWVol = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.chkCustomWetwellVolume = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.btnProcessHydraCycle = new Infragistics.Win.Misc.UltraButton();
            this.btnBrowse = new Infragistics.Win.Misc.UltraButton();
            this.ultraGrid2 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnMerge = new Infragistics.Win.Misc.UltraButton();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.txtCycleDetail = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel12 = new Infragistics.Win.Misc.UltraLabel();
            this.btnLoadCycleDetail = new Infragistics.Win.Misc.UltraButton();
            this.txtEndDate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtPSName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnRecalculate = new Infragistics.Win.Misc.UltraButton();
            this.txtWWVolume = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtStartDate = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraDropDownButton1 = new Infragistics.Win.Misc.UltraDropDownButton();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.grpPumpRateSummary = new System.Windows.Forms.GroupBox();
            this.ultraLabel13 = new Infragistics.Win.Misc.UltraLabel();
            this.txtInflow = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtExludedRowCount = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.ultraLabel14 = new Infragistics.Win.Misc.UltraLabel();
            this.txtPumpThreshold = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel9 = new Infragistics.Win.Misc.UltraLabel();
            this.txtPumpRate4 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel8 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.txtPumpRate3 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.txtPumpRate2 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.txtPumpRate1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraLabel10 = new Infragistics.Win.Misc.UltraLabel();
            this.btnExport = new Infragistics.Win.Misc.UltraButton();
            this.txtUniformInterval = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ultraPopupControlContainer1 = new Infragistics.Win.Misc.UltraPopupControlContainer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.ultraLabel23 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel24 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel25 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel26 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel27 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraGrid4 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraLabel28 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel29 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel30 = new Infragistics.Win.Misc.UltraLabel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.stationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ultraCalendarCombo1 = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
            this.ultraCalendarCombo2 = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
            this.stationTableAdapter = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.StationTableAdapter();
            this.cycleDataTableAdapter = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.CycleDataTableAdapter();
            this.operationsDataTableAdapter = new PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.OperationsDataTableAdapter();
            this.ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStationList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pumpCycleDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUniformInterval2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAverageInflow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnalysisDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMultiPumpRunTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStationAverageCycleTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEndDateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBeginDateTime)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPumpSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cycleSummaryBindingSource)).BeginInit();
            this.tabMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operationsDataBindingSource)).BeginInit();
            this.tabRawCycleData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cycleDataBindingSource)).BeginInit();
            this.tabWetWellDepth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid7)).BeginInit();
            this.tabWetWellInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeadActiveWWVol)).BeginInit();
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPSName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWWVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartDate)).BeginInit();
            this.grpPumpRateSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInflow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExludedRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPumpThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPumpRate4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPumpRate3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPumpRate2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPumpRate1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUniformInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCalendarCombo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCalendarCombo2)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.grdStationList);
            this.ultraTabPageControl2.Controls.Add(this.chkRetrieveWetWellLevel);
            this.ultraTabPageControl2.Controls.Add(this.splitter1);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel35);
            this.ultraTabPageControl2.Controls.Add(this.txtUniformInterval2);
            this.ultraTabPageControl2.Controls.Add(this.chkUniformInterval);
            this.ultraTabPageControl2.Controls.Add(this.ultraChart1);
            this.ultraTabPageControl2.Controls.Add(this.btnExportChart);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel32);
            this.ultraTabPageControl2.Controls.Add(this.txtAverageInflow);
            this.ultraTabPageControl2.Controls.Add(this.txtAnalysisDuration);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel20);
            this.ultraTabPageControl2.Controls.Add(this.btnProcessCycleData);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel33);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel31);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel17);
            this.ultraTabPageControl2.Controls.Add(this.txtMultiPumpRunTime);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel16);
            this.ultraTabPageControl2.Controls.Add(this.txtStationAverageCycleTime);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel15);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel22);
            this.ultraTabPageControl2.Controls.Add(this.cmbEndDateTime);
            this.ultraTabPageControl2.Controls.Add(this.cmbBeginDateTime);
            this.ultraTabPageControl2.Controls.Add(this.tabControl2);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(1, 23);
            this.ultraTabPageControl2.Margin = new System.Windows.Forms.Padding(2);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(903, 573);
            // 
            // grdStationList
            // 
            this.grdStationList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdStationList.DataMember = "Station";
            this.grdStationList.DataSource = this.pumpCycleDS;
            appearance25.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdStationList.DisplayLayout.Appearance = appearance25;
            ultraGridColumn1.Header.Caption = "Station ID";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Width = 71;
            ultraGridColumn2.Header.Caption = "H2 Number";
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn2.Width = 86;
            ultraGridColumn3.Header.Caption = "Station";
            ultraGridColumn3.Header.VisiblePosition = 0;
            ultraGridColumn3.Width = 130;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Header.VisiblePosition = 13;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.Header.VisiblePosition = 14;
            ultraGridColumn16.Header.VisiblePosition = 15;
            ultraGridColumn17.Header.VisiblePosition = 16;
            ultraGridColumn18.Header.VisiblePosition = 17;
            ultraGridColumn19.Header.VisiblePosition = 18;
            ultraGridColumn20.Header.VisiblePosition = 19;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
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
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20});
            ultraGridBand1.Expandable = false;
            ultraGridBand1.Override.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.grdStationList.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdStationList.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdStationList.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.grdStationList.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.grdStationList.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.grdStationList.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            this.grdStationList.DisplayLayout.BandsSerializer.Add(ultraGridBand7);
            this.grdStationList.DisplayLayout.InterBandSpacing = 10;
            this.grdStationList.DisplayLayout.MaxColScrollRegions = 1;
            this.grdStationList.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdStationList.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdStationList.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdStationList.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdStationList.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance26.BackColor = System.Drawing.Color.Transparent;
            this.grdStationList.DisplayLayout.Override.CardAreaAppearance = appearance26;
            appearance27.BackColor = System.Drawing.SystemColors.Control;
            appearance27.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdStationList.DisplayLayout.Override.CellAppearance = appearance27;
            this.grdStationList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdStationList.DisplayLayout.Override.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.grdStationList.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.None;
            appearance28.BackColor = System.Drawing.SystemColors.Control;
            appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance28.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdStationList.DisplayLayout.Override.HeaderAppearance = appearance28;
            this.grdStationList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance29.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdStationList.DisplayLayout.Override.RowSelectorAppearance = appearance29;
            this.grdStationList.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdStationList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance30.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance30.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdStationList.DisplayLayout.Override.SelectedRowAppearance = appearance30;
            this.grdStationList.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.grdStationList.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdStationList.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.None;
            this.grdStationList.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdStationList.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
            this.grdStationList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdStationList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdStationList.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdStationList.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdStationList.Location = new System.Drawing.Point(8, 100);
            this.grdStationList.Margin = new System.Windows.Forms.Padding(2);
            this.grdStationList.Name = "grdStationList";
            this.grdStationList.Size = new System.Drawing.Size(196, 334);
            this.grdStationList.TabIndex = 42;
            this.grdStationList.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdStationList_AfterSelectChange);
            // 
            // pumpCycleDS
            // 
            this.pumpCycleDS.DataSetName = "CycleData";
            this.pumpCycleDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // chkRetrieveWetWellLevel
            // 
            this.chkRetrieveWetWellLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRetrieveWetWellLevel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.chkRetrieveWetWellLevel.Checked = true;
            this.chkRetrieveWetWellLevel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRetrieveWetWellLevel.Location = new System.Drawing.Point(539, 10);
            this.chkRetrieveWetWellLevel.Margin = new System.Windows.Forms.Padding(2);
            this.chkRetrieveWetWellLevel.Name = "chkRetrieveWetWellLevel";
            this.chkRetrieveWetWellLevel.Size = new System.Drawing.Size(217, 29);
            this.chkRetrieveWetWellLevel.TabIndex = 44;
            this.chkRetrieveWetWellLevel.Text = "Retrieve Wet Well Depth? (For durations < 3 days only)";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 443);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(903, 2);
            this.splitter1.TabIndex = 38;
            this.splitter1.TabStop = false;
            // 
            // ultraLabel35
            // 
            this.ultraLabel35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ultraLabel35.Location = new System.Drawing.Point(344, 418);
            this.ultraLabel35.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel35.Name = "ultraLabel35";
            this.ultraLabel35.Size = new System.Drawing.Size(114, 19);
            this.ultraLabel35.TabIndex = 35;
            this.ultraLabel35.Text = "Interval (minutes):";
            // 
            // txtUniformInterval2
            // 
            this.txtUniformInterval2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtUniformInterval2.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtUniformInterval2.Location = new System.Drawing.Point(462, 415);
            this.txtUniformInterval2.Margin = new System.Windows.Forms.Padding(2);
            this.txtUniformInterval2.Name = "txtUniformInterval2";
            this.txtUniformInterval2.Size = new System.Drawing.Size(38, 21);
            this.txtUniformInterval2.TabIndex = 34;
            this.txtUniformInterval2.Text = "60";
            // 
            // chkUniformInterval
            // 
            this.chkUniformInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUniformInterval.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.chkUniformInterval.Checked = true;
            this.chkUniformInterval.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUniformInterval.Location = new System.Drawing.Point(344, 397);
            this.chkUniformInterval.Margin = new System.Windows.Forms.Padding(2);
            this.chkUniformInterval.Name = "chkUniformInterval";
            this.chkUniformInterval.Size = new System.Drawing.Size(142, 16);
            this.chkUniformInterval.TabIndex = 33;
            this.chkUniformInterval.Text = "Use Uniform Interval?";
            // 
            //			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
            //			'ChartType' must be persisted ahead of any Axes change made in design time.
            //		
            this.ultraChart1.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.Composite;
            // 
            // ultraChart1
            // 
            this.ultraChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraChart1.Axis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            paintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            paintElement1.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.ultraChart1.Axis.PE = paintElement1;
            this.ultraChart1.Axis.X.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChart1.Axis.X.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.FormatString = "";
            this.ultraChart1.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X.LineThickness = 1;
            this.ultraChart1.Axis.X.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.X.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.X.ScrollScale.Visible = true;
            this.ultraChart1.Axis.X.TickmarkInterval = 50;
            this.ultraChart1.Axis.X.TickmarkIntervalType = Infragistics.UltraChart.Shared.Styles.AxisIntervalType.Hours;
            this.ultraChart1.Axis.X.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.X.Visible = true;
            this.ultraChart1.Axis.X2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChart1.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.FormatString = "";
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X2.Labels.Visible = false;
            this.ultraChart1.Axis.X2.LineThickness = 1;
            this.ultraChart1.Axis.X2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X2.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.X2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X2.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.X2.TickmarkInterval = 50;
            this.ultraChart1.Axis.X2.TickmarkIntervalType = Infragistics.UltraChart.Shared.Styles.AxisIntervalType.Hours;
            this.ultraChart1.Axis.X2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.X2.Visible = false;
            this.ultraChart1.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChart1.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.FormatString = "";
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y.LineThickness = 1;
            this.ultraChart1.Axis.Y.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Y.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Y.TickmarkInterval = 1;
            this.ultraChart1.Axis.Y.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Y.Visible = true;
            this.ultraChart1.Axis.Y2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChart1.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.FormatString = "";
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y2.Labels.Visible = false;
            this.ultraChart1.Axis.Y2.LineThickness = 1;
            this.ultraChart1.Axis.Y2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y2.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Y2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y2.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Y2.TickmarkInterval = 100000;
            this.ultraChart1.Axis.Y2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Y2.Visible = false;
            this.ultraChart1.Axis.Z.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Z.Labels.ItemFormatString = "";
            this.ultraChart1.Axis.Z.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z.Labels.Visible = false;
            this.ultraChart1.Axis.Z.LineThickness = 1;
            this.ultraChart1.Axis.Z.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Z.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Z.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Z.Visible = false;
            this.ultraChart1.Axis.Z2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Z2.Labels.ItemFormatString = "";
            this.ultraChart1.Axis.Z2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z2.Labels.Visible = false;
            this.ultraChart1.Axis.Z2.LineThickness = 1;
            this.ultraChart1.Axis.Z2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z2.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Z2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z2.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Z2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Z2.Visible = false;
            this.ultraChart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ultraChart1.ColorModel.AlphaLevel = ((byte)(150));
            this.ultraChart1.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
            axisItem1.DataType = Infragistics.UltraChart.Shared.Styles.AxisDataType.Time;
            axisItem1.Extent = 45;
            axisItem1.Key = "axisX";
            axisItem1.Labels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            axisItem1.Labels.ItemFormatString = "<DATA_VALUE:M/d h:mm>";
            fontScalingAxisLabelLayoutBehavior1.EnableRollback = false;
            fontScalingAxisLabelLayoutBehavior2.EnableRollback = false;
            fontScalingAxisLabelLayoutBehavior2.MinimumSize = 5.9F;
            axisItem1.Labels.Layout.BehaviorCollection.AddRange(new Infragistics.UltraChart.Resources.Appearance.AxisLabelLayoutBehavior[] {
            fontScalingAxisLabelLayoutBehavior1,
            wrapTextAxisLabelLayoutBehavior1,
            staggerAxisLabelLayoutBehavior1,
            rotateAxisLabelLayoutBehavior1,
            fontScalingAxisLabelLayoutBehavior2,
            wrapTextAxisLabelLayoutBehavior2,
            staggerAxisLabelLayoutBehavior2,
            rotateAxisLabelLayoutBehavior2,
            clipTextAxisLabelLayoutBehavior1});
            axisItem1.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Custom;
            axisItem1.Labels.OrientationAngle = 30;
            axisItem1.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            axisItem1.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            axisItem1.Labels.SeriesLabels.OrientationAngle = 45;
            axisItem1.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            axisItem1.Labels.SeriesLabels.Visible = false;
            axisItem1.Labels.VerticalAlign = System.Drawing.StringAlignment.Far;
            axisItem1.LineThickness = 1;
            axisItem1.MajorGridLines.AlphaLevel = ((byte)(255));
            axisItem1.MajorGridLines.Color = System.Drawing.Color.Gray;
            axisItem1.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem1.MajorGridLines.Visible = true;
            axisItem1.MinorGridLines.AlphaLevel = ((byte)(255));
            axisItem1.MinorGridLines.Color = System.Drawing.Color.LightGray;
            axisItem1.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem1.MinorGridLines.Visible = false;
            axisItem1.OrientationType = Infragistics.UltraChart.Shared.Styles.AxisNumber.X_Axis;
            axisItem1.RangeMax = 39452;
            axisItem1.RangeMin = 39448;
            axisItem1.SetLabelAxisType = Infragistics.UltraChart.Core.Layers.SetLabelAxisType.GroupBySeries;
            axisItem1.TickmarkInterval = 2;
            axisItem2.DataType = Infragistics.UltraChart.Shared.Styles.AxisDataType.Numeric;
            axisItem2.Extent = 40;
            axisItem2.Key = "axisY";
            axisItem2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            axisItem2.Labels.ItemFormatString = "<DATA_VALUE:##>";
            axisItem2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            fontScalingAxisLabelLayoutBehavior3.EnableRollback = false;
            fontScalingAxisLabelLayoutBehavior4.EnableRollback = false;
            fontScalingAxisLabelLayoutBehavior4.MinimumSize = 5.9F;
            axisItem2.Labels.Layout.BehaviorCollection.AddRange(new Infragistics.UltraChart.Resources.Appearance.AxisLabelLayoutBehavior[] {
            fontScalingAxisLabelLayoutBehavior3,
            wrapTextAxisLabelLayoutBehavior3,
            staggerAxisLabelLayoutBehavior3,
            rotateAxisLabelLayoutBehavior3,
            fontScalingAxisLabelLayoutBehavior4,
            wrapTextAxisLabelLayoutBehavior4,
            staggerAxisLabelLayoutBehavior4,
            rotateAxisLabelLayoutBehavior4,
            clipTextAxisLabelLayoutBehavior2});
            axisItem2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Custom;
            axisItem2.Labels.SeriesLabels.FormatString = "Flow GPM";
            axisItem2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            axisItem2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            axisItem2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            axisItem2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            axisItem2.LineThickness = 1;
            axisItem2.MajorGridLines.AlphaLevel = ((byte)(255));
            axisItem2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            axisItem2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem2.MajorGridLines.Visible = true;
            axisItem2.MinorGridLines.AlphaLevel = ((byte)(255));
            axisItem2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            axisItem2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem2.MinorGridLines.Visible = false;
            axisItem2.OrientationType = Infragistics.UltraChart.Shared.Styles.AxisNumber.Y_Axis;
            axisItem2.RangeMax = 600;
            axisItem2.RangeType = Infragistics.UltraChart.Shared.Styles.AxisRangeType.Custom;
            axisItem2.SetLabelAxisType = Infragistics.UltraChart.Core.Layers.SetLabelAxisType.GroupBySeries;
            axisItem2.TickmarkInterval = 100;
            axisItem2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.DataInterval;
            axisItem3.DataType = Infragistics.UltraChart.Shared.Styles.AxisDataType.Numeric;
            axisItem3.Extent = 30;
            axisItem3.Key = "axisY2";
            axisItem3.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            axisItem3.Labels.ItemFormatString = "<DATA_VALUE:##0>";
            fontScalingAxisLabelLayoutBehavior5.EnableRollback = false;
            fontScalingAxisLabelLayoutBehavior6.EnableRollback = false;
            fontScalingAxisLabelLayoutBehavior6.MinimumSize = 5.9F;
            axisItem3.Labels.Layout.BehaviorCollection.AddRange(new Infragistics.UltraChart.Resources.Appearance.AxisLabelLayoutBehavior[] {
            fontScalingAxisLabelLayoutBehavior5,
            wrapTextAxisLabelLayoutBehavior5,
            staggerAxisLabelLayoutBehavior5,
            rotateAxisLabelLayoutBehavior5,
            fontScalingAxisLabelLayoutBehavior6,
            wrapTextAxisLabelLayoutBehavior6,
            staggerAxisLabelLayoutBehavior6,
            rotateAxisLabelLayoutBehavior6,
            clipTextAxisLabelLayoutBehavior3});
            axisItem3.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            axisItem3.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            axisItem3.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            axisItem3.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            axisItem3.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            axisItem3.LineThickness = 1;
            axisItem3.MajorGridLines.AlphaLevel = ((byte)(255));
            axisItem3.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            axisItem3.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem3.MajorGridLines.Visible = true;
            axisItem3.MinorGridLines.AlphaLevel = ((byte)(255));
            axisItem3.MinorGridLines.Color = System.Drawing.Color.LightGray;
            axisItem3.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem3.MinorGridLines.Visible = false;
            axisItem3.OrientationType = Infragistics.UltraChart.Shared.Styles.AxisNumber.Y2_Axis;
            axisItem3.RangeMax = 30;
            axisItem3.RangeType = Infragistics.UltraChart.Shared.Styles.AxisRangeType.Custom;
            axisItem3.SetLabelAxisType = Infragistics.UltraChart.Core.Layers.SetLabelAxisType.GroupBySeries;
            axisItem3.TickmarkInterval = 10;
            axisItem3.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.DataInterval;
            chartArea1.Axes.Add(axisItem1);
            chartArea1.Axes.Add(axisItem2);
            chartArea1.Axes.Add(axisItem3);
            chartArea1.Border.Thickness = 0;
            chartArea1.Bounds = new System.Drawing.Rectangle(2, 30, 98, 65);
            chartArea1.BoundsMeasureType = Infragistics.UltraChart.Shared.Styles.MeasureType.Percentage;
            paintElement2.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            chartArea1.GridPE = paintElement2;
            chartArea1.Key = "area1";
            chartArea1.PE = paintElement3;
            axisItem4.DataType = Infragistics.UltraChart.Shared.Styles.AxisDataType.Time;
            axisItem4.Extent = 0;
            axisItem4.Key = "axisXb";
            axisItem4.Labels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            axisItem4.Labels.ItemFormatString = "";
            axisItem4.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            axisItem4.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            axisItem4.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            axisItem4.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            axisItem4.Labels.VerticalAlign = System.Drawing.StringAlignment.Far;
            axisItem4.LineColor = System.Drawing.Color.Transparent;
            axisItem4.LineThickness = 0;
            axisItem4.MajorGridLines.AlphaLevel = ((byte)(255));
            axisItem4.MajorGridLines.Color = System.Drawing.Color.Gray;
            axisItem4.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem4.MajorGridLines.Visible = true;
            axisItem4.MinorGridLines.AlphaLevel = ((byte)(255));
            axisItem4.MinorGridLines.Color = System.Drawing.Color.LightGray;
            axisItem4.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem4.MinorGridLines.Visible = false;
            axisItem4.OrientationType = Infragistics.UltraChart.Shared.Styles.AxisNumber.X2_Axis;
            axisItem4.SetLabelAxisType = Infragistics.UltraChart.Core.Layers.SetLabelAxisType.GroupBySeries;
            axisItem5.DataType = Infragistics.UltraChart.Shared.Styles.AxisDataType.Numeric;
            axisItem5.Extent = 40;
            axisItem5.Key = "axisYb";
            axisItem5.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            axisItem5.Labels.ItemFormatString = "<DATA_VALUE:0>";
            axisItem5.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            axisItem5.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            axisItem5.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            axisItem5.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            axisItem5.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            axisItem5.LineThickness = 1;
            axisItem5.MajorGridLines.AlphaLevel = ((byte)(255));
            axisItem5.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            axisItem5.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem5.MajorGridLines.Visible = true;
            axisItem5.MinorGridLines.AlphaLevel = ((byte)(255));
            axisItem5.MinorGridLines.Color = System.Drawing.Color.LightGray;
            axisItem5.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem5.MinorGridLines.Visible = false;
            axisItem5.OrientationType = Infragistics.UltraChart.Shared.Styles.AxisNumber.Y_Axis;
            axisItem5.SetLabelAxisType = Infragistics.UltraChart.Core.Layers.SetLabelAxisType.GroupBySeries;
            axisItem5.TickmarkInterval = 1;
            axisItem5.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.DataInterval;
            axisItem6.DataType = Infragistics.UltraChart.Shared.Styles.AxisDataType.Numeric;
            axisItem6.Extent = 30;
            axisItem6.Key = "axisY2b";
            axisItem6.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            axisItem6.Labels.ItemFormatString = "";
            axisItem6.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            axisItem6.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            axisItem6.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            axisItem6.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            axisItem6.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            axisItem6.LineThickness = 1;
            axisItem6.MajorGridLines.AlphaLevel = ((byte)(255));
            axisItem6.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            axisItem6.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem6.MajorGridLines.Visible = true;
            axisItem6.MinorGridLines.AlphaLevel = ((byte)(255));
            axisItem6.MinorGridLines.Color = System.Drawing.Color.LightGray;
            axisItem6.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            axisItem6.MinorGridLines.Visible = false;
            axisItem6.OrientationType = Infragistics.UltraChart.Shared.Styles.AxisNumber.Y2_Axis;
            axisItem6.SetLabelAxisType = Infragistics.UltraChart.Core.Layers.SetLabelAxisType.GroupBySeries;
            chartArea2.Axes.Add(axisItem4);
            chartArea2.Axes.Add(axisItem5);
            chartArea2.Axes.Add(axisItem6);
            chartArea2.Border.Thickness = 0;
            chartArea2.Bounds = new System.Drawing.Rectangle(2, 10, 98, 20);
            chartArea2.BoundsMeasureType = Infragistics.UltraChart.Shared.Styles.MeasureType.Percentage;
            paintElement4.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            chartArea2.GridPE = paintElement4;
            chartArea2.Key = "area2";
            chartArea2.PE = paintElement5;
            this.ultraChart1.CompositeChart.ChartAreas.Add(chartArea1);
            this.ultraChart1.CompositeChart.ChartAreas.Add(chartArea2);
            chartLayerAppearance1.AxisXKey = "axisX";
            chartLayerAppearance1.AxisYKey = "axisY";
            chartLayerAppearance1.ChartAreaKey = "area1";
            chartLayerAppearance1.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.LineChart;
            chartLayerAppearance1.Key = "chartLayer1";
            chartLayerAppearance1.SeriesList = "calculatedFlow|calculatedPumpingRate";
            chartLayerAppearance2.AxisXKey = "axisX";
            chartLayerAppearance2.AxisYKey = "axisY2";
            chartLayerAppearance2.ChartAreaKey = "area1";
            chartLayerAppearance2.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.LineChart;
            chartLayerAppearance2.Key = "chartLayer2";
            chartLayerAppearance2.SeriesList = "wetwellLevel";
            chartLayerAppearance3.AxisXKey = "axisXb";
            chartLayerAppearance3.AxisY2Key = "axisYb";
            chartLayerAppearance3.AxisYKey = "axisYb";
            chartLayerAppearance3.ChartAreaKey = "area2";
            chartLayerAppearance3.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.StepLineChart;
            chartLayerAppearance3.Key = "chartLayer3";
            chartLayerAppearance3.SeriesList = "runningPumpCount";
            this.ultraChart1.CompositeChart.ChartLayers.AddRange(new Infragistics.UltraChart.Resources.Appearance.ChartLayerAppearance[] {
            chartLayerAppearance1,
            chartLayerAppearance2,
            chartLayerAppearance3});
            compositeLegend1.Border.Thickness = 0;
            compositeLegend1.Bounds = new System.Drawing.Rectangle(60, 2, 500, 40);
            compositeLegend1.ChartLayerList = "chartLayer1|chartLayer2|chartLayer3";
            this.ultraChart1.CompositeChart.Legends.Add(compositeLegend1);
            numericTimeSeries1.Data.TimeValueColumn = "";
            numericTimeSeries1.Data.ValueColumn = "";
            numericTimeSeries1.Key = "runningPumpCount";
            numericTimeSeries1.Label = "Running Pumps";
            paintElement6.Fill = System.Drawing.Color.DarkOrange;
            paintElement6.Stroke = System.Drawing.Color.DarkOrange;
            numericTimeSeries1.PEs.AddRange(new Infragistics.UltraChart.Resources.Appearance.PaintElement[] {
            paintElement6});
            paintElement7.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint1.PE = paintElement7;
            numericTimeDataPoint1.TimeValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            numericTimeDataPoint2.NumericValue = 1;
            paintElement8.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint2.PE = paintElement8;
            numericTimeDataPoint2.TimeValue = new System.DateTime(2000, 1, 1, 1, 0, 0, 0);
            numericTimeDataPoint3.NumericValue = 1;
            paintElement9.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint3.PE = paintElement9;
            numericTimeDataPoint3.TimeValue = new System.DateTime(2000, 1, 1, 2, 0, 0, 0);
            paintElement10.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint4.PE = paintElement10;
            numericTimeDataPoint4.TimeValue = new System.DateTime(2000, 1, 1, 3, 0, 0, 0);
            numericTimeDataPoint5.NumericValue = 0.5;
            paintElement11.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint5.PE = paintElement11;
            numericTimeDataPoint5.TimeValue = new System.DateTime(2000, 1, 1, 4, 0, 0, 0);
            numericTimeSeries1.Points.Add(numericTimeDataPoint1);
            numericTimeSeries1.Points.Add(numericTimeDataPoint2);
            numericTimeSeries1.Points.Add(numericTimeDataPoint3);
            numericTimeSeries1.Points.Add(numericTimeDataPoint4);
            numericTimeSeries1.Points.Add(numericTimeDataPoint5);
            numericTimeSeries2.Data.TimeValueColumn = "";
            numericTimeSeries2.Data.ValueColumn = "";
            numericTimeSeries2.Key = "wetwellLevel";
            numericTimeSeries2.Label = "Wet Well Level (inches)";
            paintElement12.Fill = System.Drawing.Color.ForestGreen;
            paintElement12.Stroke = System.Drawing.Color.ForestGreen;
            paintElement12.StrokeOpacity = ((byte)(126));
            paintElement12.StrokeWidth = 2;
            numericTimeSeries2.PEs.AddRange(new Infragistics.UltraChart.Resources.Appearance.PaintElement[] {
            paintElement12});
            paintElement13.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint6.PE = paintElement13;
            numericTimeDataPoint6.TimeValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            numericTimeDataPoint7.NumericValue = 20;
            paintElement14.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint7.PE = paintElement14;
            numericTimeDataPoint7.TimeValue = new System.DateTime(2000, 1, 1, 1, 0, 0, 0);
            numericTimeDataPoint8.NumericValue = 22;
            paintElement15.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint8.PE = paintElement15;
            numericTimeDataPoint8.TimeValue = new System.DateTime(2000, 1, 1, 2, 0, 0, 0);
            numericTimeDataPoint9.NumericValue = 30;
            paintElement16.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint9.PE = paintElement16;
            numericTimeDataPoint9.TimeValue = new System.DateTime(2000, 1, 1, 3, 0, 0, 0);
            numericTimeDataPoint10.NumericValue = 18;
            paintElement17.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint10.PE = paintElement17;
            numericTimeDataPoint10.TimeValue = new System.DateTime(2000, 1, 1, 5, 0, 0, 0);
            numericTimeSeries2.Points.Add(numericTimeDataPoint6);
            numericTimeSeries2.Points.Add(numericTimeDataPoint7);
            numericTimeSeries2.Points.Add(numericTimeDataPoint8);
            numericTimeSeries2.Points.Add(numericTimeDataPoint9);
            numericTimeSeries2.Points.Add(numericTimeDataPoint10);
            numericTimeSeries3.Data.TimeValueColumn = "";
            numericTimeSeries3.Data.ValueColumn = "";
            numericTimeSeries3.Key = "rainfall";
            numericTimeSeries3.Visible = false;
            numericTimeSeries4.Data.TimeValueColumn = "";
            numericTimeSeries4.Data.ValueColumn = "";
            numericTimeSeries4.Key = "calculatedFlow";
            numericTimeSeries4.Label = "Calculated Flow (gpm)";
            paintElement18.Fill = System.Drawing.Color.Blue;
            paintElement18.Stroke = System.Drawing.Color.Blue;
            paintElement18.StrokeWidth = 2;
            numericTimeSeries4.PEs.AddRange(new Infragistics.UltraChart.Resources.Appearance.PaintElement[] {
            paintElement18});
            numericTimeDataPoint11.NumericValue = 100;
            paintElement19.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint11.PE = paintElement19;
            numericTimeDataPoint11.TimeValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            numericTimeDataPoint12.NumericValue = 120;
            paintElement20.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint12.PE = paintElement20;
            numericTimeDataPoint12.TimeValue = new System.DateTime(2000, 1, 1, 1, 0, 0, 0);
            numericTimeDataPoint13.NumericValue = 180;
            paintElement21.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint13.PE = paintElement21;
            numericTimeDataPoint13.TimeValue = new System.DateTime(2000, 1, 1, 2, 0, 0, 0);
            numericTimeDataPoint14.NumericValue = 40;
            paintElement22.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint14.PE = paintElement22;
            numericTimeDataPoint14.TimeValue = new System.DateTime(2000, 1, 1, 4, 0, 0, 0);
            numericTimeDataPoint15.NumericValue = 90;
            paintElement23.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint15.PE = paintElement23;
            numericTimeDataPoint15.TimeValue = new System.DateTime(2000, 1, 1, 5, 0, 0, 0);
            numericTimeSeries4.Points.Add(numericTimeDataPoint11);
            numericTimeSeries4.Points.Add(numericTimeDataPoint12);
            numericTimeSeries4.Points.Add(numericTimeDataPoint13);
            numericTimeSeries4.Points.Add(numericTimeDataPoint14);
            numericTimeSeries4.Points.Add(numericTimeDataPoint15);
            numericTimeSeries5.Data.TimeValueColumn = "";
            numericTimeSeries5.Data.ValueColumn = "";
            numericTimeSeries5.Key = "calculatedPumpingRate";
            numericTimeSeries5.Label = "Calculated Pumping Rate (gpm)";
            paintElement24.Fill = System.Drawing.Color.Red;
            paintElement24.Stroke = System.Drawing.Color.Red;
            paintElement24.StrokeWidth = 2;
            numericTimeSeries5.PEs.AddRange(new Infragistics.UltraChart.Resources.Appearance.PaintElement[] {
            paintElement24});
            numericTimeDataPoint16.NumericValue = 500;
            paintElement25.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint16.PE = paintElement25;
            numericTimeDataPoint16.TimeValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            numericTimeDataPoint17.NumericValue = 550;
            paintElement26.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint17.PE = paintElement26;
            numericTimeDataPoint17.TimeValue = new System.DateTime(2000, 1, 1, 2, 0, 0, 0);
            numericTimeDataPoint18.NumericValue = 450;
            paintElement27.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            numericTimeDataPoint18.PE = paintElement27;
            numericTimeDataPoint18.TimeValue = new System.DateTime(2000, 1, 1, 5, 0, 0, 0);
            numericTimeSeries5.Points.Add(numericTimeDataPoint16);
            numericTimeSeries5.Points.Add(numericTimeDataPoint17);
            numericTimeSeries5.Points.Add(numericTimeDataPoint18);
            this.ultraChart1.CompositeChart.Series.AddRange(new Infragistics.UltraChart.Data.Series.ISeries[] {
            numericTimeSeries1,
            numericTimeSeries2,
            numericTimeSeries3,
            numericTimeSeries4,
            numericTimeSeries5});
            this.ultraChart1.Effects.Effects.Add(gradientEffect1);
            this.ultraChart1.EmptyChartText = "Pump Cycle Display";
            this.ultraChart1.Legend.Location = Infragistics.UltraChart.Shared.Styles.LegendLocation.Bottom;
            this.ultraChart1.Location = new System.Drawing.Point(210, 100);
            this.ultraChart1.Margin = new System.Windows.Forms.Padding(2);
            this.ultraChart1.Name = "ultraChart1";
            this.ultraChart1.Size = new System.Drawing.Size(685, 293);
            this.ultraChart1.TabIndex = 25;
            this.ultraChart1.TitleBottom.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Tooltips.FormatString = "<ITEM_LABEL>: <DATA_VALUE:00.##>";
            this.ultraChart1.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray;
            this.ultraChart1.Scrolling += new Infragistics.UltraChart.Shared.Events.ChartScrollScaleEventHandler(this.ultraChart1_Scrolling);
            this.ultraChart1.Scaling += new Infragistics.UltraChart.Shared.Events.ChartScrollScaleEventHandler(this.ultraChart1_Scaling);
            // 
            // btnExportChart
            // 
            this.btnExportChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportChart.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnExportChart.Location = new System.Drawing.Point(210, 397);
            this.btnExportChart.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportChart.Name = "btnExportChart";
            this.btnExportChart.Size = new System.Drawing.Size(129, 37);
            this.btnExportChart.TabIndex = 32;
            this.btnExportChart.Text = "Export Chart (*.csv)";
            this.btnExportChart.Click += new System.EventHandler(this.btnExportChart_Click);
            // 
            // ultraLabel32
            // 
            this.ultraLabel32.Location = new System.Drawing.Point(210, 81);
            this.ultraLabel32.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel32.Name = "ultraLabel32";
            this.ultraLabel32.Size = new System.Drawing.Size(138, 19);
            this.ultraLabel32.TabIndex = 24;
            this.ultraLabel32.Text = "Chart Cycle Display:";
            // 
            // txtAverageInflow
            // 
            this.txtAverageInflow.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtAverageInflow.Location = new System.Drawing.Point(638, 47);
            this.txtAverageInflow.Margin = new System.Windows.Forms.Padding(2);
            this.txtAverageInflow.Name = "txtAverageInflow";
            this.txtAverageInflow.ReadOnly = true;
            this.txtAverageInflow.Size = new System.Drawing.Size(39, 21);
            this.txtAverageInflow.TabIndex = 28;
            this.txtAverageInflow.Text = "0.0";
            // 
            // txtAnalysisDuration
            // 
            this.txtAnalysisDuration.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtAnalysisDuration.Location = new System.Drawing.Point(146, 56);
            this.txtAnalysisDuration.Margin = new System.Windows.Forms.Padding(2);
            this.txtAnalysisDuration.Name = "txtAnalysisDuration";
            this.txtAnalysisDuration.ReadOnly = true;
            this.txtAnalysisDuration.Size = new System.Drawing.Size(58, 21);
            this.txtAnalysisDuration.TabIndex = 15;
            this.txtAnalysisDuration.Text = "0.0";
            // 
            // ultraLabel20
            // 
            this.ultraLabel20.Location = new System.Drawing.Point(8, 58);
            this.ultraLabel20.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel20.Name = "ultraLabel20";
            this.ultraLabel20.Size = new System.Drawing.Size(134, 19);
            this.ultraLabel20.TabIndex = 14;
            this.ultraLabel20.Text = "Analysis Duration (days):";
            // 
            // btnProcessCycleData
            // 
            this.btnProcessCycleData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcessCycleData.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnProcessCycleData.Location = new System.Drawing.Point(210, 10);
            this.btnProcessCycleData.Margin = new System.Windows.Forms.Padding(2);
            this.btnProcessCycleData.Name = "btnProcessCycleData";
            this.btnProcessCycleData.Size = new System.Drawing.Size(325, 29);
            this.btnProcessCycleData.TabIndex = 9;
            this.btnProcessCycleData.Text = "Process Cycle Data";
            this.btnProcessCycleData.Click += new System.EventHandler(this.btnProcessCycleData_Click);
            // 
            // ultraLabel33
            // 
            this.ultraLabel33.Location = new System.Drawing.Point(553, 46);
            this.ultraLabel33.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel33.Name = "ultraLabel33";
            this.ultraLabel33.Size = new System.Drawing.Size(99, 34);
            this.ultraLabel33.TabIndex = 27;
            this.ultraLabel33.Text = "Average Inflow (gpm):";
            // 
            // ultraLabel31
            // 
            this.ultraLabel31.Location = new System.Drawing.Point(389, 46);
            this.ultraLabel31.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel31.Name = "ultraLabel31";
            this.ultraLabel31.Size = new System.Drawing.Size(116, 34);
            this.ultraLabel31.TabIndex = 20;
            this.ultraLabel31.Text = "Multiple Pumps Running (minutes):";
            // 
            // ultraLabel17
            // 
            this.ultraLabel17.Location = new System.Drawing.Point(8, 35);
            this.ultraLabel17.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel17.Name = "ultraLabel17";
            this.ultraLabel17.Size = new System.Drawing.Size(88, 19);
            this.ultraLabel17.TabIndex = 6;
            this.ultraLabel17.Text = "End Date/Time:";
            // 
            // txtMultiPumpRunTime
            // 
            this.txtMultiPumpRunTime.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtMultiPumpRunTime.Location = new System.Drawing.Point(509, 47);
            this.txtMultiPumpRunTime.Margin = new System.Windows.Forms.Padding(2);
            this.txtMultiPumpRunTime.Name = "txtMultiPumpRunTime";
            this.txtMultiPumpRunTime.ReadOnly = true;
            this.txtMultiPumpRunTime.Size = new System.Drawing.Size(39, 21);
            this.txtMultiPumpRunTime.TabIndex = 21;
            this.txtMultiPumpRunTime.Text = "0.0";
            // 
            // ultraLabel16
            // 
            this.ultraLabel16.Location = new System.Drawing.Point(8, 11);
            this.ultraLabel16.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel16.Name = "ultraLabel16";
            this.ultraLabel16.Size = new System.Drawing.Size(88, 19);
            this.ultraLabel16.TabIndex = 5;
            this.ultraLabel16.Text = "Start Date/Time:";
            // 
            // txtStationAverageCycleTime
            // 
            this.txtStationAverageCycleTime.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtStationAverageCycleTime.Location = new System.Drawing.Point(346, 47);
            this.txtStationAverageCycleTime.Margin = new System.Windows.Forms.Padding(2);
            this.txtStationAverageCycleTime.Name = "txtStationAverageCycleTime";
            this.txtStationAverageCycleTime.ReadOnly = true;
            this.txtStationAverageCycleTime.Size = new System.Drawing.Size(39, 21);
            this.txtStationAverageCycleTime.TabIndex = 19;
            this.txtStationAverageCycleTime.Text = "0.0";
            // 
            // ultraLabel15
            // 
            this.ultraLabel15.Location = new System.Drawing.Point(8, 81);
            this.ultraLabel15.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel15.Name = "ultraLabel15";
            this.ultraLabel15.Size = new System.Drawing.Size(196, 19);
            this.ultraLabel15.TabIndex = 4;
            this.ultraLabel15.Text = "Pump Stations:";
            // 
            // ultraLabel22
            // 
            this.ultraLabel22.Location = new System.Drawing.Point(210, 46);
            this.ultraLabel22.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel22.Name = "ultraLabel22";
            this.ultraLabel22.Size = new System.Drawing.Size(131, 34);
            this.ultraLabel22.TabIndex = 18;
            this.ultraLabel22.Text = "Station Average Cycle Time (minutes):";
            // 
            // cmbEndDateTime
            // 
            this.cmbEndDateTime.BackColor = System.Drawing.SystemColors.Window;
            this.cmbEndDateTime.CalendarInfo = this.ultraCalendarInfo1;
            this.cmbEndDateTime.CalendarLook = this.ultraCalendarLook1;
            this.cmbEndDateTime.DateButtons.Add(dateButton1);
            this.cmbEndDateTime.Format = "M/d/yyyy hh:mm";
            this.cmbEndDateTime.Location = new System.Drawing.Point(100, 33);
            this.cmbEndDateTime.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEndDateTime.Name = "cmbEndDateTime";
            this.cmbEndDateTime.NonAutoSizeHeight = 21;
            this.cmbEndDateTime.Size = new System.Drawing.Size(104, 21);
            this.cmbEndDateTime.TabIndex = 2;
            this.cmbEndDateTime.ValueChanged += new System.EventHandler(this.cmbEndDateTime_ValueChanged);
            // 
            // ultraCalendarInfo1
            // 
            this.ultraCalendarInfo1.DataBindingsForAppointments.BindingContextControl = this;
            this.ultraCalendarInfo1.DataBindingsForOwners.BindingContextControl = this;
            // 
            // ultraCalendarLook1
            // 
            this.ultraCalendarLook1.ViewStyle = Infragistics.Win.UltraWinSchedule.ViewStyle.VisualStudio2005;
            // 
            // cmbBeginDateTime
            // 
            this.cmbBeginDateTime.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBeginDateTime.CalendarInfo = this.ultraCalendarInfo1;
            this.cmbBeginDateTime.CalendarLook = this.ultraCalendarLook1;
            this.cmbBeginDateTime.DateButtons.Add(dateButton2);
            this.cmbBeginDateTime.Format = "M/d/yyyy hh:mm";
            this.cmbBeginDateTime.Location = new System.Drawing.Point(100, 10);
            this.cmbBeginDateTime.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBeginDateTime.Name = "cmbBeginDateTime";
            this.cmbBeginDateTime.NonAutoSizeHeight = 21;
            this.cmbBeginDateTime.Size = new System.Drawing.Size(104, 21);
            this.cmbBeginDateTime.TabIndex = 1;
            this.cmbBeginDateTime.ValueChanged += new System.EventHandler(this.cmbBeginDateTime_ValueChanged);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPumpSummary);
            this.tabControl2.Controls.Add(this.tabMessages);
            this.tabControl2.Controls.Add(this.tabRawCycleData);
            this.tabControl2.Controls.Add(this.tabWetWellDepth);
            this.tabControl2.Controls.Add(this.tabWetWellInfo);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl2.Location = new System.Drawing.Point(0, 445);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(903, 128);
            this.tabControl2.TabIndex = 47;
            // 
            // tabPumpSummary
            // 
            this.tabPumpSummary.Controls.Add(this.ultraGrid5);
            this.tabPumpSummary.Location = new System.Drawing.Point(4, 22);
            this.tabPumpSummary.Margin = new System.Windows.Forms.Padding(2);
            this.tabPumpSummary.Name = "tabPumpSummary";
            this.tabPumpSummary.Padding = new System.Windows.Forms.Padding(2);
            this.tabPumpSummary.Size = new System.Drawing.Size(895, 102);
            this.tabPumpSummary.TabIndex = 2;
            this.tabPumpSummary.Text = "Pump Summary";
            this.tabPumpSummary.UseVisualStyleBackColor = true;
            // 
            // ultraGrid5
            // 
            this.ultraGrid5.DataSource = this.cycleSummaryBindingSource;
            appearance1.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid5.DisplayLayout.Appearance = appearance1;
            ultraGridColumn43.Header.VisiblePosition = 0;
            ultraGridColumn43.Hidden = true;
            ultraGridColumn44.Header.VisiblePosition = 1;
            ultraGridColumn44.Width = 62;
            ultraGridColumn45.Format = "N0";
            ultraGridColumn45.Header.Caption = "Cycle Count";
            ultraGridColumn45.Header.VisiblePosition = 2;
            ultraGridColumn45.Width = 88;
            ultraGridColumn46.Format = "N2";
            ultraGridColumn46.Header.Caption = "Pump On Duration (days)";
            ultraGridColumn46.Header.VisiblePosition = 6;
            ultraGridColumn47.Format = "N2";
            ultraGridColumn47.Header.Caption = "Pump Off Duration (days)";
            ultraGridColumn47.Header.VisiblePosition = 7;
            ultraGridColumn48.Format = "";
            ultraGridColumn48.Header.Caption = "Calculated Pumping Rate (gpm)";
            ultraGridColumn48.Header.VisiblePosition = 8;
            ultraGridColumn48.Hidden = true;
            ultraGridColumn49.Format = "P1";
            ultraGridColumn49.Header.Caption = "Percent of Duration Pump Running";
            ultraGridColumn49.Header.VisiblePosition = 3;
            ultraGridColumn49.Width = 107;
            ultraGridColumn50.Format = "N1";
            ultraGridColumn50.Header.Caption = "Average Cycle Time (minutes)";
            ultraGridColumn50.Header.VisiblePosition = 4;
            ultraGridColumn51.Header.Caption = "Reported Pumping Rate (gpm)";
            ultraGridColumn51.Header.VisiblePosition = 5;
            ultraGridBand10.Columns.AddRange(new object[] {
            ultraGridColumn43,
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49,
            ultraGridColumn50,
            ultraGridColumn51});
            this.ultraGrid5.DisplayLayout.BandsSerializer.Add(ultraGridBand10);
            this.ultraGrid5.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid5.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid5.DisplayLayout.MaxRowScrollRegions = 1;
            this.ultraGrid5.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid5.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid5.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid5.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.ultraGrid5.DisplayLayout.Override.CellAppearance = appearance3;
            this.ultraGrid5.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            appearance4.BackColor = System.Drawing.SystemColors.Control;
            appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid5.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.ultraGrid5.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ultraGrid5.DisplayLayout.Override.RowSelectorAppearance = appearance5;
            this.ultraGrid5.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.ultraGrid5.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.ultraGrid5.DisplayLayout.Override.SelectedRowAppearance = appearance6;
            this.ultraGrid5.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ultraGrid5.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.ultraGrid5.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid5.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid5.Location = new System.Drawing.Point(2, 2);
            this.ultraGrid5.Margin = new System.Windows.Forms.Padding(2);
            this.ultraGrid5.Name = "ultraGrid5";
            this.ultraGrid5.Size = new System.Drawing.Size(891, 98);
            this.ultraGrid5.TabIndex = 13;
            // 
            // cycleSummaryBindingSource
            // 
            this.cycleSummaryBindingSource.DataMember = "CycleSummary";
            this.cycleSummaryBindingSource.DataSource = this.pumpCycleDS;
            // 
            // tabMessages
            // 
            this.tabMessages.Controls.Add(this.ultraGrid6);
            this.tabMessages.Location = new System.Drawing.Point(4, 22);
            this.tabMessages.Margin = new System.Windows.Forms.Padding(2);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Padding = new System.Windows.Forms.Padding(2);
            this.tabMessages.Size = new System.Drawing.Size(895, 102);
            this.tabMessages.TabIndex = 0;
            this.tabMessages.Text = "Operations Messages";
            this.tabMessages.UseVisualStyleBackColor = true;
            // 
            // ultraGrid6
            // 
            this.ultraGrid6.DataSource = this.operationsDataBindingSource;
            appearance7.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance7.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid6.DisplayLayout.Appearance = appearance7;
            ultraGridColumn52.Header.VisiblePosition = 0;
            ultraGridColumn52.Hidden = true;
            ultraGridColumn53.Format = "G";
            ultraGridColumn53.Header.VisiblePosition = 1;
            ultraGridColumn53.Hidden = true;
            ultraGridColumn54.Header.VisiblePosition = 2;
            ultraGridColumn54.Hidden = true;
            ultraGridColumn55.Header.VisiblePosition = 3;
            ultraGridColumn55.Hidden = true;
            ultraGridColumn56.Header.VisiblePosition = 4;
            ultraGridColumn57.Format = "MM/dd/yy hh:mm";
            ultraGridColumn57.Header.VisiblePosition = 5;
            ultraGridColumn58.Header.VisiblePosition = 6;
            ultraGridColumn58.Width = 670;
            ultraGridColumn59.Header.VisiblePosition = 7;
            ultraGridColumn59.Hidden = true;
            ultraGridColumn60.Header.VisiblePosition = 8;
            ultraGridColumn60.Hidden = true;
            ultraGridBand11.Columns.AddRange(new object[] {
            ultraGridColumn52,
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56,
            ultraGridColumn57,
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn60});
            this.ultraGrid6.DisplayLayout.BandsSerializer.Add(ultraGridBand11);
            this.ultraGrid6.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid6.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid6.DisplayLayout.MaxRowScrollRegions = 1;
            appearance8.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid6.DisplayLayout.Override.CardAreaAppearance = appearance8;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.ultraGrid6.DisplayLayout.Override.CellAppearance = appearance9;
            this.ultraGrid6.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            appearance10.BackColor = System.Drawing.SystemColors.Control;
            appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance10.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid6.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ultraGrid6.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ultraGrid6.DisplayLayout.Override.RowSelectorAppearance = appearance11;
            this.ultraGrid6.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.ultraGrid6.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance12.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance12.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.ultraGrid6.DisplayLayout.Override.SelectedRowAppearance = appearance12;
            this.ultraGrid6.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ultraGrid6.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.ultraGrid6.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid6.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid6.Location = new System.Drawing.Point(2, 2);
            this.ultraGrid6.Margin = new System.Windows.Forms.Padding(2);
            this.ultraGrid6.Name = "ultraGrid6";
            this.ultraGrid6.Size = new System.Drawing.Size(891, 98);
            this.ultraGrid6.TabIndex = 17;
            // 
            // operationsDataBindingSource
            // 
            this.operationsDataBindingSource.DataMember = "OperationsData";
            this.operationsDataBindingSource.DataSource = this.pumpCycleDS;
            // 
            // tabRawCycleData
            // 
            this.tabRawCycleData.Controls.Add(this.ultraGrid3);
            this.tabRawCycleData.Location = new System.Drawing.Point(4, 22);
            this.tabRawCycleData.Margin = new System.Windows.Forms.Padding(2);
            this.tabRawCycleData.Name = "tabRawCycleData";
            this.tabRawCycleData.Padding = new System.Windows.Forms.Padding(2);
            this.tabRawCycleData.Size = new System.Drawing.Size(895, 102);
            this.tabRawCycleData.TabIndex = 1;
            this.tabRawCycleData.Text = "Raw Cycle Data";
            this.tabRawCycleData.UseVisualStyleBackColor = true;
            // 
            // ultraGrid3
            // 
            this.ultraGrid3.DataSource = this.cycleDataBindingSource;
            appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid3.DisplayLayout.Appearance = appearance13;
            ultraGridColumn61.Header.VisiblePosition = 0;
            ultraGridColumn61.Hidden = true;
            ultraGridColumn62.Header.VisiblePosition = 1;
            ultraGridColumn62.Hidden = true;
            ultraGridColumn63.Header.VisiblePosition = 2;
            ultraGridColumn63.Hidden = true;
            ultraGridColumn64.Header.VisiblePosition = 3;
            ultraGridColumn65.Header.VisiblePosition = 4;
            ultraGridColumn66.Format = "G";
            ultraGridColumn66.Header.VisiblePosition = 5;
            ultraGridColumn67.Header.VisiblePosition = 6;
            ultraGridColumn68.Header.VisiblePosition = 7;
            ultraGridColumn68.Hidden = true;
            ultraGridColumn69.Header.VisiblePosition = 8;
            ultraGridColumn69.Hidden = true;
            ultraGridBand12.Columns.AddRange(new object[] {
            ultraGridColumn61,
            ultraGridColumn62,
            ultraGridColumn63,
            ultraGridColumn64,
            ultraGridColumn65,
            ultraGridColumn66,
            ultraGridColumn67,
            ultraGridColumn68,
            ultraGridColumn69});
            this.ultraGrid3.DisplayLayout.BandsSerializer.Add(ultraGridBand12);
            this.ultraGrid3.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid3.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid3.DisplayLayout.MaxRowScrollRegions = 1;
            appearance14.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid3.DisplayLayout.Override.CardAreaAppearance = appearance14;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.ultraGrid3.DisplayLayout.Override.CellAppearance = appearance15;
            this.ultraGrid3.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            appearance16.BackColor = System.Drawing.SystemColors.Control;
            appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance16.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid3.DisplayLayout.Override.HeaderAppearance = appearance16;
            this.ultraGrid3.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance17.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ultraGrid3.DisplayLayout.Override.RowSelectorAppearance = appearance17;
            this.ultraGrid3.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            appearance18.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance18.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.ultraGrid3.DisplayLayout.Override.SelectedRowAppearance = appearance18;
            this.ultraGrid3.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ultraGrid3.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.ultraGrid3.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid3.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid3.Location = new System.Drawing.Point(2, 2);
            this.ultraGrid3.Margin = new System.Windows.Forms.Padding(2);
            this.ultraGrid3.Name = "ultraGrid3";
            this.ultraGrid3.Size = new System.Drawing.Size(891, 98);
            this.ultraGrid3.TabIndex = 10;
            // 
            // cycleDataBindingSource
            // 
            this.cycleDataBindingSource.DataMember = "CycleData";
            this.cycleDataBindingSource.DataSource = this.pumpCycleDS;
            // 
            // tabWetWellDepth
            // 
            this.tabWetWellDepth.Controls.Add(this.ultraGrid7);
            this.tabWetWellDepth.Location = new System.Drawing.Point(4, 22);
            this.tabWetWellDepth.Margin = new System.Windows.Forms.Padding(2);
            this.tabWetWellDepth.Name = "tabWetWellDepth";
            this.tabWetWellDepth.Padding = new System.Windows.Forms.Padding(2);
            this.tabWetWellDepth.Size = new System.Drawing.Size(895, 102);
            this.tabWetWellDepth.TabIndex = 3;
            this.tabWetWellDepth.Text = "Wet Well Depth";
            this.tabWetWellDepth.UseVisualStyleBackColor = true;
            // 
            // ultraGrid7
            // 
            this.ultraGrid7.DataMember = "DepthData";
            this.ultraGrid7.DataSource = this.pumpCycleDS;
            appearance67.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid7.DisplayLayout.Appearance = appearance67;
            ultraGridColumn70.Header.VisiblePosition = 0;
            ultraGridColumn71.Header.VisiblePosition = 1;
            ultraGridColumn72.Header.VisiblePosition = 2;
            ultraGridColumn73.Header.VisiblePosition = 3;
            ultraGridColumn74.Header.VisiblePosition = 4;
            ultraGridColumn75.Header.VisiblePosition = 5;
            ultraGridColumn76.Header.VisiblePosition = 6;
            ultraGridColumn77.Header.VisiblePosition = 7;
            ultraGridColumn78.Header.VisiblePosition = 8;
            ultraGridColumn79.Header.VisiblePosition = 9;
            ultraGridColumn80.Header.VisiblePosition = 10;
            ultraGridColumn81.Header.VisiblePosition = 11;
            ultraGridColumn82.Header.VisiblePosition = 12;
            ultraGridBand13.Columns.AddRange(new object[] {
            ultraGridColumn70,
            ultraGridColumn71,
            ultraGridColumn72,
            ultraGridColumn73,
            ultraGridColumn74,
            ultraGridColumn75,
            ultraGridColumn76,
            ultraGridColumn77,
            ultraGridColumn78,
            ultraGridColumn79,
            ultraGridColumn80,
            ultraGridColumn81,
            ultraGridColumn82});
            this.ultraGrid7.DisplayLayout.BandsSerializer.Add(ultraGridBand13);
            this.ultraGrid7.DisplayLayout.InterBandSpacing = 10;
            appearance68.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid7.DisplayLayout.Override.CardAreaAppearance = appearance68;
            appearance69.BackColor = System.Drawing.SystemColors.Control;
            appearance69.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.ultraGrid7.DisplayLayout.Override.CellAppearance = appearance69;
            appearance70.BackColor = System.Drawing.SystemColors.Control;
            appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance70.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid7.DisplayLayout.Override.HeaderAppearance = appearance70;
            appearance71.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ultraGrid7.DisplayLayout.Override.RowSelectorAppearance = appearance71;
            this.ultraGrid7.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.ultraGrid7.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance72.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance72.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.ultraGrid7.DisplayLayout.Override.SelectedRowAppearance = appearance72;
            this.ultraGrid7.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ultraGrid7.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.ultraGrid7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid7.Location = new System.Drawing.Point(2, 2);
            this.ultraGrid7.Margin = new System.Windows.Forms.Padding(2);
            this.ultraGrid7.Name = "ultraGrid7";
            this.ultraGrid7.Size = new System.Drawing.Size(891, 98);
            this.ultraGrid7.TabIndex = 0;
            // 
            // tabWetWellInfo
            // 
            this.tabWetWellInfo.Controls.Add(this.ultraGrid8);
            this.tabWetWellInfo.Controls.Add(this.ultraLabel34);
            this.tabWetWellInfo.Controls.Add(this.txtLeadActiveWWVol);
            this.tabWetWellInfo.Controls.Add(this.chkCustomWetwellVolume);
            this.tabWetWellInfo.Location = new System.Drawing.Point(4, 22);
            this.tabWetWellInfo.Name = "tabWetWellInfo";
            this.tabWetWellInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabWetWellInfo.Size = new System.Drawing.Size(895, 102);
            this.tabWetWellInfo.TabIndex = 4;
            this.tabWetWellInfo.Text = "Wet Well Info";
            this.tabWetWellInfo.UseVisualStyleBackColor = true;
            // 
            // ultraGrid8
            // 
            this.ultraGrid8.DataMember = "WetWell";
            this.ultraGrid8.DataSource = this.pumpCycleDS;
            appearance19.BackColor = System.Drawing.SystemColors.ControlLight;
            appearance19.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid8.DisplayLayout.Appearance = appearance19;
            ultraGridColumn83.Header.VisiblePosition = 0;
            ultraGridColumn84.Header.VisiblePosition = 1;
            ultraGridColumn85.Header.VisiblePosition = 2;
            ultraGridColumn86.Header.VisiblePosition = 3;
            ultraGridColumn87.Header.VisiblePosition = 4;
            ultraGridColumn88.Header.VisiblePosition = 5;
            ultraGridColumn89.Header.VisiblePosition = 6;
            ultraGridColumn90.Header.VisiblePosition = 7;
            ultraGridColumn91.Header.VisiblePosition = 8;
            ultraGridColumn92.Header.VisiblePosition = 9;
            ultraGridBand14.Columns.AddRange(new object[] {
            ultraGridColumn83,
            ultraGridColumn84,
            ultraGridColumn85,
            ultraGridColumn86,
            ultraGridColumn87,
            ultraGridColumn88,
            ultraGridColumn89,
            ultraGridColumn90,
            ultraGridColumn91,
            ultraGridColumn92});
            this.ultraGrid8.DisplayLayout.BandsSerializer.Add(ultraGridBand14);
            this.ultraGrid8.DisplayLayout.InterBandSpacing = 10;
            appearance20.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid8.DisplayLayout.Override.CardAreaAppearance = appearance20;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.ultraGrid8.DisplayLayout.Override.CellAppearance = appearance21;
            appearance22.BackColor = System.Drawing.SystemColors.Control;
            appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance22.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid8.DisplayLayout.Override.HeaderAppearance = appearance22;
            appearance23.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ultraGrid8.DisplayLayout.Override.RowSelectorAppearance = appearance23;
            this.ultraGrid8.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.ultraGrid8.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance24.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance24.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.ultraGrid8.DisplayLayout.Override.SelectedRowAppearance = appearance24;
            this.ultraGrid8.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ultraGrid8.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.ultraGrid8.Dock = System.Windows.Forms.DockStyle.Right;
            this.ultraGrid8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid8.Location = new System.Drawing.Point(159, 3);
            this.ultraGrid8.Margin = new System.Windows.Forms.Padding(2);
            this.ultraGrid8.Name = "ultraGrid8";
            this.ultraGrid8.Size = new System.Drawing.Size(733, 96);
            this.ultraGrid8.TabIndex = 1;
            // 
            // ultraLabel34
            // 
            this.ultraLabel34.Location = new System.Drawing.Point(5, 14);
            this.ultraLabel34.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel34.Name = "ultraLabel34";
            this.ultraLabel34.Size = new System.Drawing.Size(107, 34);
            this.ultraLabel34.TabIndex = 30;
            this.ultraLabel34.Text = "Lead Active Wetwell Volume (gallons):";
            // 
            // txtLeadActiveWWVol
            // 
            this.txtLeadActiveWWVol.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005;
            this.txtLeadActiveWWVol.Location = new System.Drawing.Point(116, 14);
            this.txtLeadActiveWWVol.Margin = new System.Windows.Forms.Padding(2);
            this.txtLeadActiveWWVol.Name = "txtLeadActiveWWVol";
            this.txtLeadActiveWWVol.ReadOnly = true;
            this.txtLeadActiveWWVol.Size = new System.Drawing.Size(39, 21);
            this.txtLeadActiveWWVol.TabIndex = 31;
            this.txtLeadActiveWWVol.Text = "0.0";
            // 
            // chkCustomWetwellVolume
            // 
            this.chkCustomWetwellVolume.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.chkCustomWetwellVolume.Location = new System.Drawing.Point(2, 47);
            this.chkCustomWetwellVolume.Margin = new System.Windows.Forms.Padding(2);
            this.chkCustomWetwellVolume.Name = "chkCustomWetwellVolume";
            this.chkCustomWetwellVolume.Size = new System.Drawing.Size(142, 37);
            this.chkCustomWetwellVolume.TabIndex = 49;
            this.chkCustomWetwellVolume.Text = "Override Default Wetwell Volume?";
            this.chkCustomWetwellVolume.CheckedChanged += new System.EventHandler(this.chkCustomWetwellVolume_CheckedChanged);
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.btnProcessHydraCycle);
            this.ultraTabPageControl1.Controls.Add(this.btnBrowse);
            this.ultraTabPageControl1.Controls.Add(this.ultraGrid2);
            this.ultraTabPageControl1.Controls.Add(this.btnMerge);
            this.ultraTabPageControl1.Controls.Add(this.ultraLabel1);
            this.ultraTabPageControl1.Controls.Add(this.txtCycleDetail);
            this.ultraTabPageControl1.Controls.Add(this.ultraLabel12);
            this.ultraTabPageControl1.Controls.Add(this.btnLoadCycleDetail);
            this.ultraTabPageControl1.Controls.Add(this.txtEndDate);
            this.ultraTabPageControl1.Controls.Add(this.txtPSName);
            this.ultraTabPageControl1.Controls.Add(this.btnRecalculate);
            this.ultraTabPageControl1.Controls.Add(this.txtWWVolume);
            this.ultraTabPageControl1.Controls.Add(this.txtStartDate);
            this.ultraTabPageControl1.Controls.Add(this.ultraDropDownButton1);
            this.ultraTabPageControl1.Controls.Add(this.ultraLabel2);
            this.ultraTabPageControl1.Controls.Add(this.ultraLabel3);
            this.ultraTabPageControl1.Controls.Add(this.grpPumpRateSummary);
            this.ultraTabPageControl1.Controls.Add(this.ultraGrid1);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl1.Margin = new System.Windows.Forms.Padding(2);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(903, 573);
            // 
            // btnProcessHydraCycle
            // 
            this.btnProcessHydraCycle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProcessHydraCycle.Location = new System.Drawing.Point(2, 534);
            this.btnProcessHydraCycle.Margin = new System.Windows.Forms.Padding(2);
            this.btnProcessHydraCycle.Name = "btnProcessHydraCycle";
            this.btnProcessHydraCycle.Size = new System.Drawing.Size(414, 36);
            this.btnProcessHydraCycle.TabIndex = 34;
            this.btnProcessHydraCycle.Text = "Process Cycle Data";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(500, 8);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(56, 20);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // ultraGrid2
            // 
            this.ultraGrid2.DataMember = "Raw";
            this.ultraGrid2.DataSource = this.pumpCycleDS;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGrid2.DisplayLayout.Appearance = appearance31;
            ultraGridColumn21.Header.VisiblePosition = 0;
            ultraGridColumn22.Header.VisiblePosition = 1;
            ultraGridColumn23.Header.VisiblePosition = 2;
            ultraGridColumn24.Header.VisiblePosition = 3;
            ultraGridColumn25.Header.VisiblePosition = 4;
            ultraGridColumn26.Header.VisiblePosition = 5;
            ultraGridColumn27.Header.VisiblePosition = 6;
            ultraGridColumn28.Header.VisiblePosition = 7;
            ultraGridColumn29.Header.VisiblePosition = 8;
            ultraGridColumn30.Header.VisiblePosition = 9;
            ultraGridColumn31.Header.VisiblePosition = 10;
            ultraGridBand8.Columns.AddRange(new object[] {
            ultraGridColumn21,
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31});
            this.ultraGrid2.DisplayLayout.BandsSerializer.Add(ultraGridBand8);
            this.ultraGrid2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGrid2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance32.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid2.DisplayLayout.GroupByBox.Appearance = appearance32;
            appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
            this.ultraGrid2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance34.BackColor2 = System.Drawing.SystemColors.Control;
            appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid2.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
            this.ultraGrid2.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid2.DisplayLayout.MaxRowScrollRegions = 1;
            appearance35.BackColor = System.Drawing.SystemColors.Window;
            appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGrid2.DisplayLayout.Override.ActiveCellAppearance = appearance35;
            appearance36.BackColor = System.Drawing.SystemColors.Highlight;
            appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGrid2.DisplayLayout.Override.ActiveRowAppearance = appearance36;
            this.ultraGrid2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGrid2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance37.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGrid2.DisplayLayout.Override.CardAreaAppearance = appearance37;
            appearance38.BorderColor = System.Drawing.Color.Silver;
            appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGrid2.DisplayLayout.Override.CellAppearance = appearance38;
            this.ultraGrid2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGrid2.DisplayLayout.Override.CellPadding = 0;
            appearance39.BackColor = System.Drawing.SystemColors.Control;
            appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance39.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid2.DisplayLayout.Override.GroupByRowAppearance = appearance39;
            appearance40.TextHAlignAsString = "Left";
            this.ultraGrid2.DisplayLayout.Override.HeaderAppearance = appearance40;
            this.ultraGrid2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGrid2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance41.BackColor = System.Drawing.SystemColors.Window;
            appearance41.BorderColor = System.Drawing.Color.Silver;
            this.ultraGrid2.DisplayLayout.Override.RowAppearance = appearance41;
            this.ultraGrid2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGrid2.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
            this.ultraGrid2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid2.Enabled = false;
            this.ultraGrid2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid2.Location = new System.Drawing.Point(602, 343);
            this.ultraGrid2.Margin = new System.Windows.Forms.Padding(2);
            this.ultraGrid2.Name = "ultraGrid2";
            this.ultraGrid2.Size = new System.Drawing.Size(85, 90);
            this.ultraGrid2.TabIndex = 27;
            this.ultraGrid2.Text = "ultraGrid2";
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(621, 8);
            this.btnMerge.Margin = new System.Windows.Forms.Padding(2);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(93, 20);
            this.btnMerge.TabIndex = 31;
            this.btnMerge.Text = "Merge Multiple...";
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Location = new System.Drawing.Point(2, 32);
            this.ultraLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(146, 19);
            this.ultraLabel1.TabIndex = 6;
            this.ultraLabel1.Text = "Pump Station Name:";
            // 
            // txtCycleDetail
            // 
            this.txtCycleDetail.Location = new System.Drawing.Point(2, 7);
            this.txtCycleDetail.Margin = new System.Windows.Forms.Padding(2);
            this.txtCycleDetail.Name = "txtCycleDetail";
            this.txtCycleDetail.Size = new System.Drawing.Size(493, 21);
            this.txtCycleDetail.TabIndex = 0;
            this.txtCycleDetail.Text = "Select a Pump Cycle Detail file, and click \"Load\" when done...";
            // 
            // ultraLabel12
            // 
            this.ultraLabel12.Location = new System.Drawing.Point(392, 32);
            this.ultraLabel12.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel12.Name = "ultraLabel12";
            this.ultraLabel12.Size = new System.Drawing.Size(92, 19);
            this.ultraLabel12.TabIndex = 30;
            this.ultraLabel12.Text = "End Date:";
            // 
            // btnLoadCycleDetail
            // 
            this.btnLoadCycleDetail.Location = new System.Drawing.Point(560, 8);
            this.btnLoadCycleDetail.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadCycleDetail.Name = "btnLoadCycleDetail";
            this.btnLoadCycleDetail.Size = new System.Drawing.Size(56, 20);
            this.btnLoadCycleDetail.TabIndex = 1;
            this.btnLoadCycleDetail.Text = "Load";
            this.btnLoadCycleDetail.Click += new System.EventHandler(this.btnLoadCycleDetail_Click);
            // 
            // txtEndDate
            // 
            this.txtEndDate.Location = new System.Drawing.Point(390, 50);
            this.txtEndDate.Margin = new System.Windows.Forms.Padding(2);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(105, 21);
            this.txtEndDate.TabIndex = 29;
            // 
            // txtPSName
            // 
            this.txtPSName.Location = new System.Drawing.Point(2, 50);
            this.txtPSName.Margin = new System.Windows.Forms.Padding(2);
            this.txtPSName.Name = "txtPSName";
            this.txtPSName.Size = new System.Drawing.Size(167, 21);
            this.txtPSName.TabIndex = 3;
            // 
            // btnRecalculate
            // 
            this.btnRecalculate.Location = new System.Drawing.Point(500, 50);
            this.btnRecalculate.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecalculate.Name = "btnRecalculate";
            this.btnRecalculate.Size = new System.Drawing.Size(74, 19);
            this.btnRecalculate.TabIndex = 28;
            this.btnRecalculate.Text = "Recalculate";
            this.btnRecalculate.Click += new System.EventHandler(this.btnRecalculate_Click);
            // 
            // txtWWVolume
            // 
            this.txtWWVolume.Location = new System.Drawing.Point(174, 50);
            this.txtWWVolume.Margin = new System.Windows.Forms.Padding(2);
            this.txtWWVolume.Name = "txtWWVolume";
            this.txtWWVolume.Size = new System.Drawing.Size(100, 21);
            this.txtWWVolume.TabIndex = 4;
            // 
            // txtStartDate
            // 
            this.txtStartDate.Location = new System.Drawing.Point(279, 50);
            this.txtStartDate.Margin = new System.Windows.Forms.Padding(2);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(109, 21);
            this.txtStartDate.TabIndex = 5;
            // 
            // ultraDropDownButton1
            // 
            this.ultraDropDownButton1.Location = new System.Drawing.Point(578, 50);
            this.ultraDropDownButton1.Margin = new System.Windows.Forms.Padding(2);
            this.ultraDropDownButton1.Name = "ultraDropDownButton1";
            this.ultraDropDownButton1.Size = new System.Drawing.Size(99, 20);
            this.ultraDropDownButton1.Style = Infragistics.Win.Misc.SplitButtonDisplayStyle.DropDownButtonOnly;
            this.ultraDropDownButton1.TabIndex = 23;
            this.ultraDropDownButton1.Text = "View Raw Data";
            this.ultraDropDownButton1.DroppingDown += new System.ComponentModel.CancelEventHandler(this.ultraDropDownButton1_DroppingDown);
            this.ultraDropDownButton1.ClosedUp += new System.EventHandler(this.ultraDropDownButton1_ClosedUp);
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.Location = new System.Drawing.Point(174, 32);
            this.ultraLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(97, 19);
            this.ultraLabel2.TabIndex = 7;
            this.ultraLabel2.Text = "Wetwell Volume:";
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.Location = new System.Drawing.Point(279, 32);
            this.ultraLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(95, 19);
            this.ultraLabel3.TabIndex = 8;
            this.ultraLabel3.Text = "Start Date:";
            // 
            // grpPumpRateSummary
            // 
            this.grpPumpRateSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPumpRateSummary.Controls.Add(this.ultraLabel13);
            this.grpPumpRateSummary.Controls.Add(this.txtInflow);
            this.grpPumpRateSummary.Controls.Add(this.txtExludedRowCount);
            this.grpPumpRateSummary.Controls.Add(this.trackBar1);
            this.grpPumpRateSummary.Controls.Add(this.ultraLabel14);
            this.grpPumpRateSummary.Controls.Add(this.txtPumpThreshold);
            this.grpPumpRateSummary.Controls.Add(this.ultraLabel7);
            this.grpPumpRateSummary.Controls.Add(this.ultraLabel9);
            this.grpPumpRateSummary.Controls.Add(this.txtPumpRate4);
            this.grpPumpRateSummary.Controls.Add(this.ultraLabel8);
            this.grpPumpRateSummary.Controls.Add(this.ultraLabel6);
            this.grpPumpRateSummary.Controls.Add(this.txtPumpRate3);
            this.grpPumpRateSummary.Controls.Add(this.ultraLabel5);
            this.grpPumpRateSummary.Controls.Add(this.txtPumpRate2);
            this.grpPumpRateSummary.Controls.Add(this.ultraLabel4);
            this.grpPumpRateSummary.Controls.Add(this.txtPumpRate1);
            this.grpPumpRateSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPumpRateSummary.Location = new System.Drawing.Point(2, 75);
            this.grpPumpRateSummary.Margin = new System.Windows.Forms.Padding(2);
            this.grpPumpRateSummary.Name = "grpPumpRateSummary";
            this.grpPumpRateSummary.Padding = new System.Windows.Forms.Padding(2);
            this.grpPumpRateSummary.Size = new System.Drawing.Size(736, 132);
            this.grpPumpRateSummary.TabIndex = 12;
            this.grpPumpRateSummary.TabStop = false;
            this.grpPumpRateSummary.Text = "Average Pump Rate";
            // 
            // ultraLabel13
            // 
            this.ultraLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel13.Location = new System.Drawing.Point(230, 17);
            this.ultraLabel13.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel13.Name = "ultraLabel13";
            this.ultraLabel13.Size = new System.Drawing.Size(51, 19);
            this.ultraLabel13.TabIndex = 24;
            this.ultraLabel13.Text = "Inflow";
            // 
            // txtInflow
            // 
            this.txtInflow.Location = new System.Drawing.Point(230, 35);
            this.txtInflow.Margin = new System.Windows.Forms.Padding(2);
            this.txtInflow.Name = "txtInflow";
            this.txtInflow.ReadOnly = true;
            this.txtInflow.Size = new System.Drawing.Size(51, 21);
            this.txtInflow.TabIndex = 23;
            // 
            // txtExludedRowCount
            // 
            this.txtExludedRowCount.Location = new System.Drawing.Point(98, 106);
            this.txtExludedRowCount.Margin = new System.Windows.Forms.Padding(2);
            this.txtExludedRowCount.Name = "txtExludedRowCount";
            this.txtExludedRowCount.ReadOnly = true;
            this.txtExludedRowCount.Size = new System.Drawing.Size(127, 21);
            this.txtExludedRowCount.TabIndex = 20;
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 20;
            this.trackBar1.Location = new System.Drawing.Point(159, 75);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(2);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(110, 45);
            this.trackBar1.TabIndex = 22;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // ultraLabel14
            // 
            this.ultraLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel14.Location = new System.Drawing.Point(8, 106);
            this.ultraLabel14.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel14.Name = "ultraLabel14";
            this.ultraLabel14.Size = new System.Drawing.Size(128, 19);
            this.ultraLabel14.TabIndex = 21;
            this.ultraLabel14.Text = "Flagged Records:";
            // 
            // txtPumpThreshold
            // 
            this.txtPumpThreshold.Location = new System.Drawing.Point(202, 58);
            this.txtPumpThreshold.Margin = new System.Windows.Forms.Padding(2);
            this.txtPumpThreshold.Name = "txtPumpThreshold";
            this.txtPumpThreshold.Size = new System.Drawing.Size(23, 21);
            this.txtPumpThreshold.TabIndex = 17;
            this.txtPumpThreshold.Text = "10";
            this.txtPumpThreshold.ValueChanged += new System.EventHandler(this.txtPumpThreshold_ValueChanged);
            // 
            // ultraLabel7
            // 
            this.ultraLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel7.Location = new System.Drawing.Point(174, 17);
            this.ultraLabel7.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel7.Name = "ultraLabel7";
            this.ultraLabel7.Size = new System.Drawing.Size(51, 19);
            this.ultraLabel7.TabIndex = 16;
            this.ultraLabel7.Text = "Pump 4:";
            // 
            // ultraLabel9
            // 
            this.ultraLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel9.Location = new System.Drawing.Point(230, 58);
            this.ultraLabel9.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel9.Name = "ultraLabel9";
            this.ultraLabel9.Size = new System.Drawing.Size(153, 19);
            this.ultraLabel9.TabIndex = 19;
            this.ultraLabel9.Text = "% higher/lower than average";
            // 
            // txtPumpRate4
            // 
            this.txtPumpRate4.Location = new System.Drawing.Point(174, 35);
            this.txtPumpRate4.Margin = new System.Windows.Forms.Padding(2);
            this.txtPumpRate4.Name = "txtPumpRate4";
            this.txtPumpRate4.ReadOnly = true;
            this.txtPumpRate4.Size = new System.Drawing.Size(51, 21);
            this.txtPumpRate4.TabIndex = 15;
            // 
            // ultraLabel8
            // 
            this.ultraLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel8.Location = new System.Drawing.Point(8, 58);
            this.ultraLabel8.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel8.Name = "ultraLabel8";
            this.ultraLabel8.Size = new System.Drawing.Size(218, 19);
            this.ultraLabel8.TabIndex = 18;
            this.ultraLabel8.Text = "Flag data as invalid when pumping rate is";
            // 
            // ultraLabel6
            // 
            this.ultraLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel6.Location = new System.Drawing.Point(118, 17);
            this.ultraLabel6.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(51, 19);
            this.ultraLabel6.TabIndex = 14;
            this.ultraLabel6.Text = "Pump 3:";
            // 
            // txtPumpRate3
            // 
            this.txtPumpRate3.Location = new System.Drawing.Point(118, 35);
            this.txtPumpRate3.Margin = new System.Windows.Forms.Padding(2);
            this.txtPumpRate3.Name = "txtPumpRate3";
            this.txtPumpRate3.ReadOnly = true;
            this.txtPumpRate3.Size = new System.Drawing.Size(51, 21);
            this.txtPumpRate3.TabIndex = 13;
            // 
            // ultraLabel5
            // 
            this.ultraLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel5.Location = new System.Drawing.Point(63, 17);
            this.ultraLabel5.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(51, 19);
            this.ultraLabel5.TabIndex = 12;
            this.ultraLabel5.Text = "Pump 2:";
            // 
            // txtPumpRate2
            // 
            this.txtPumpRate2.Location = new System.Drawing.Point(63, 35);
            this.txtPumpRate2.Margin = new System.Windows.Forms.Padding(2);
            this.txtPumpRate2.Name = "txtPumpRate2";
            this.txtPumpRate2.ReadOnly = true;
            this.txtPumpRate2.Size = new System.Drawing.Size(51, 21);
            this.txtPumpRate2.TabIndex = 11;
            // 
            // ultraLabel4
            // 
            this.ultraLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel4.Location = new System.Drawing.Point(8, 17);
            this.ultraLabel4.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(51, 19);
            this.ultraLabel4.TabIndex = 10;
            this.ultraLabel4.Text = "Pump 1:";
            // 
            // txtPumpRate1
            // 
            this.txtPumpRate1.Location = new System.Drawing.Point(8, 35);
            this.txtPumpRate1.Margin = new System.Windows.Forms.Padding(2);
            this.txtPumpRate1.Name = "txtPumpRate1";
            this.txtPumpRate1.ReadOnly = true;
            this.txtPumpRate1.Size = new System.Drawing.Size(51, 21);
            this.txtPumpRate1.TabIndex = 9;
            // 
            // ultraGrid1
            // 
            this.ultraGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGrid1.DataMember = "Calculated";
            this.ultraGrid1.DataSource = this.pumpCycleDS;
            appearance43.BackColor = System.Drawing.SystemColors.Window;
            appearance43.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGrid1.DisplayLayout.Appearance = appearance43;
            ultraGridColumn32.Header.VisiblePosition = 0;
            ultraGridColumn33.Header.VisiblePosition = 1;
            ultraGridColumn34.Header.VisiblePosition = 2;
            ultraGridColumn35.Header.VisiblePosition = 3;
            ultraGridColumn36.Header.VisiblePosition = 4;
            ultraGridColumn37.Header.VisiblePosition = 5;
            ultraGridColumn38.Header.VisiblePosition = 6;
            ultraGridColumn39.Header.VisiblePosition = 7;
            ultraGridColumn40.Header.VisiblePosition = 8;
            ultraGridColumn41.Header.VisiblePosition = 9;
            ultraGridColumn42.Header.VisiblePosition = 10;
            ultraGridBand9.Columns.AddRange(new object[] {
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
            ultraGridColumn42});
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand9);
            this.ultraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance44.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance44.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.GroupByBox.Appearance = appearance44;
            appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance45;
            this.ultraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance46.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance46.BackColor2 = System.Drawing.SystemColors.Control;
            appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance46;
            this.ultraGrid1.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid1.DisplayLayout.MaxRowScrollRegions = 1;
            appearance47.BackColor = System.Drawing.SystemColors.Window;
            appearance47.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance47;
            appearance48.BackColor = System.Drawing.SystemColors.Highlight;
            appearance48.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance48;
            this.ultraGrid1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance49.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance49;
            appearance50.BorderColor = System.Drawing.Color.Silver;
            appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGrid1.DisplayLayout.Override.CellAppearance = appearance50;
            this.ultraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.ultraGrid1.DisplayLayout.Override.CellPadding = 0;
            this.ultraGrid1.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            appearance51.BackColor = System.Drawing.SystemColors.Control;
            appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance51.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance51.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance51;
            appearance52.TextHAlignAsString = "Left";
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance52;
            this.ultraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance53.BackColor = System.Drawing.SystemColors.Window;
            appearance53.BorderColor = System.Drawing.Color.Silver;
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance53;
            this.ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid1.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            appearance54.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance54;
            this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid1.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.ultraGrid1.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.ultraGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid1.Location = new System.Drawing.Point(2, 212);
            this.ultraGrid1.Margin = new System.Windows.Forms.Padding(2);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(736, 317);
            this.ultraGrid1.TabIndex = 26;
            this.ultraGrid1.Text = "ultraGrid1";
            // 
            // ultraLabel10
            // 
            this.ultraLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel10.Location = new System.Drawing.Point(3, 17);
            this.ultraLabel10.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel10.Name = "ultraLabel10";
            this.ultraLabel10.Size = new System.Drawing.Size(132, 19);
            this.ultraLabel10.TabIndex = 14;
            this.ultraLabel10.Text = "Create Uniform Time Series:";
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(140, 17);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(56, 19);
            this.btnExport.TabIndex = 13;
            this.btnExport.Text = "Export...";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtUniformInterval
            // 
            this.txtUniformInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUniformInterval.Location = new System.Drawing.Point(140, 41);
            this.txtUniformInterval.Margin = new System.Windows.Forms.Padding(2);
            this.txtUniformInterval.Name = "txtUniformInterval";
            this.txtUniformInterval.Size = new System.Drawing.Size(31, 21);
            this.txtUniformInterval.TabIndex = 23;
            this.txtUniformInterval.Text = "60";
            // 
            // ultraLabel11
            // 
            this.ultraLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel11.Location = new System.Drawing.Point(3, 41);
            this.ultraLabel11.Margin = new System.Windows.Forms.Padding(2);
            this.ultraLabel11.Name = "ultraLabel11";
            this.ultraLabel11.Size = new System.Drawing.Size(132, 19);
            this.ultraLabel11.TabIndex = 24;
            this.ultraLabel11.Text = "Time Interval (minutes):";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ultraLabel10);
            this.groupBox1.Controls.Add(this.ultraLabel11);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.txtUniformInterval);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(-98, 624);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(896, 63);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Time Series";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Title = "Select a HYDRA Pump Cycle Detail report";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.Title = "Export Uniform Time Series";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 599);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(907, 22);
            this.statusStrip1.TabIndex = 32;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(737, 16);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.tabControl1.Controls.Add(this.ultraTabPageControl1);
            this.tabControl1.Controls.Add(this.ultraTabPageControl2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.tabControl1.Size = new System.Drawing.Size(907, 599);
            this.tabControl1.TabIndex = 33;
            ultraTab1.TabPage = this.ultraTabPageControl2;
            ultraTab1.Text = "NEPTUNE Database Analysis";
            ultraTab2.TabPage = this.ultraTabPageControl1;
            ultraTab2.Text = "HYDRA Text File Analysis";
            this.tabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Margin = new System.Windows.Forms.Padding(2);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(903, 573);
            // 
            // ultraLabel23
            // 
            this.ultraLabel23.Location = new System.Drawing.Point(432, 149);
            this.ultraLabel23.Name = "ultraLabel23";
            this.ultraLabel23.Size = new System.Drawing.Size(134, 23);
            this.ultraLabel23.TabIndex = 14;
            this.ultraLabel23.Text = "Pump 3 Run Time:";
            // 
            // ultraLabel24
            // 
            this.ultraLabel24.Location = new System.Drawing.Point(432, 178);
            this.ultraLabel24.Name = "ultraLabel24";
            this.ultraLabel24.Size = new System.Drawing.Size(134, 23);
            this.ultraLabel24.TabIndex = 8;
            this.ultraLabel24.Text = "Pump 4 Run Time:";
            // 
            // ultraLabel25
            // 
            this.ultraLabel25.Location = new System.Drawing.Point(432, 120);
            this.ultraLabel25.Name = "ultraLabel25";
            this.ultraLabel25.Size = new System.Drawing.Size(134, 23);
            this.ultraLabel25.TabIndex = 13;
            this.ultraLabel25.Text = "Pump 2 Run Time:";
            // 
            // ultraLabel26
            // 
            this.ultraLabel26.Location = new System.Drawing.Point(432, 91);
            this.ultraLabel26.Name = "ultraLabel26";
            this.ultraLabel26.Size = new System.Drawing.Size(134, 23);
            this.ultraLabel26.TabIndex = 12;
            this.ultraLabel26.Text = "Pump 1 Run Time:";
            // 
            // ultraLabel27
            // 
            this.ultraLabel27.Location = new System.Drawing.Point(432, 62);
            this.ultraLabel27.Name = "ultraLabel27";
            this.ultraLabel27.Size = new System.Drawing.Size(134, 23);
            this.ultraLabel27.TabIndex = 11;
            this.ultraLabel27.Text = "Analysis Duration:";
            // 
            // ultraGrid4
            // 
            this.ultraGrid4.DataSource = this.cycleDataBindingSource;
            appearance55.BackColor = System.Drawing.SystemColors.Window;
            appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGrid4.DisplayLayout.Appearance = appearance55;
            this.ultraGrid4.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGrid4.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance56.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid4.DisplayLayout.GroupByBox.Appearance = appearance56;
            appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid4.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
            this.ultraGrid4.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance58.BackColor2 = System.Drawing.SystemColors.Control;
            appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid4.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
            this.ultraGrid4.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid4.DisplayLayout.MaxRowScrollRegions = 1;
            appearance59.BackColor = System.Drawing.SystemColors.Window;
            appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGrid4.DisplayLayout.Override.ActiveCellAppearance = appearance59;
            appearance60.BackColor = System.Drawing.SystemColors.Highlight;
            appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGrid4.DisplayLayout.Override.ActiveRowAppearance = appearance60;
            this.ultraGrid4.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGrid4.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance61.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGrid4.DisplayLayout.Override.CardAreaAppearance = appearance61;
            appearance62.BorderColor = System.Drawing.Color.Silver;
            appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGrid4.DisplayLayout.Override.CellAppearance = appearance62;
            this.ultraGrid4.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGrid4.DisplayLayout.Override.CellPadding = 0;
            appearance63.BackColor = System.Drawing.SystemColors.Control;
            appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance63.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid4.DisplayLayout.Override.GroupByRowAppearance = appearance63;
            appearance64.TextHAlignAsString = "Left";
            this.ultraGrid4.DisplayLayout.Override.HeaderAppearance = appearance64;
            this.ultraGrid4.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGrid4.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance65.BackColor = System.Drawing.SystemColors.Window;
            appearance65.BorderColor = System.Drawing.Color.Silver;
            this.ultraGrid4.DisplayLayout.Override.RowAppearance = appearance65;
            this.ultraGrid4.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.ultraGrid4.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGrid4.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
            this.ultraGrid4.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid4.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid4.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGrid4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGrid4.Location = new System.Drawing.Point(432, 207);
            this.ultraGrid4.Name = "ultraGrid4";
            this.ultraGrid4.Size = new System.Drawing.Size(552, 384);
            this.ultraGrid4.TabIndex = 10;
            this.ultraGrid4.Text = "ultraGrid3";
            // 
            // ultraLabel28
            // 
            this.ultraLabel28.Location = new System.Drawing.Point(275, 134);
            this.ultraLabel28.Name = "ultraLabel28";
            this.ultraLabel28.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel28.TabIndex = 6;
            this.ultraLabel28.Text = "End Date/Time";
            // 
            // ultraLabel29
            // 
            this.ultraLabel29.Location = new System.Drawing.Point(275, 62);
            this.ultraLabel29.Name = "ultraLabel29";
            this.ultraLabel29.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel29.TabIndex = 5;
            this.ultraLabel29.Text = "Start Date/Time";
            // 
            // ultraLabel30
            // 
            this.ultraLabel30.Location = new System.Drawing.Point(10, 62);
            this.ultraLabel30.Name = "ultraLabel30";
            this.ultraLabel30.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel30.TabIndex = 4;
            this.ultraLabel30.Text = "Pump Stations";
            // 
            // listBox1
            // 
            this.listBox1.DataSource = this.stationBindingSource;
            this.listBox1.DisplayMember = "StationName";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(10, 91);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(259, 498);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 3;
            this.listBox1.ValueMember = "StationID";
            // 
            // stationBindingSource
            // 
            this.stationBindingSource.DataMember = "Station";
            this.stationBindingSource.DataSource = this.pumpCycleDS;
            this.stationBindingSource.Sort = "";
            // 
            // ultraCalendarCombo1
            // 
            this.ultraCalendarCombo1.BackColor = System.Drawing.SystemColors.Window;
            this.ultraCalendarCombo1.CalendarInfo = this.ultraCalendarInfo1;
            this.ultraCalendarCombo1.CalendarLook = this.ultraCalendarLook1;
            this.ultraCalendarCombo1.DateButtons.Add(dateButton3);
            this.ultraCalendarCombo1.Location = new System.Drawing.Point(275, 163);
            this.ultraCalendarCombo1.Name = "ultraCalendarCombo1";
            this.ultraCalendarCombo1.NonAutoSizeHeight = 21;
            this.ultraCalendarCombo1.Size = new System.Drawing.Size(138, 21);
            this.ultraCalendarCombo1.TabIndex = 2;
            this.ultraCalendarCombo1.Value = new System.DateTime(2008, 2, 5, 0, 0, 0, 0);
            // 
            // ultraCalendarCombo2
            // 
            this.ultraCalendarCombo2.BackColor = System.Drawing.SystemColors.Window;
            this.ultraCalendarCombo2.CalendarInfo = this.ultraCalendarInfo1;
            this.ultraCalendarCombo2.CalendarLook = this.ultraCalendarLook1;
            this.ultraCalendarCombo2.DateButtons.Add(dateButton4);
            this.ultraCalendarCombo2.Location = new System.Drawing.Point(275, 91);
            this.ultraCalendarCombo2.Name = "ultraCalendarCombo2";
            this.ultraCalendarCombo2.NonAutoSizeHeight = 21;
            this.ultraCalendarCombo2.Size = new System.Drawing.Size(138, 21);
            this.ultraCalendarCombo2.TabIndex = 1;
            this.ultraCalendarCombo2.Value = new System.DateTime(2008, 2, 5, 0, 0, 0, 0);
            // 
            // stationTableAdapter
            // 
            this.stationTableAdapter.ClearBeforeFill = true;
            // 
            // cycleDataTableAdapter
            // 
            this.cycleDataTableAdapter.ClearBeforeFill = true;
            // 
            // operationsDataTableAdapter
            // 
            this.operationsDataTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 621);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Pump Cycle Data Analyzer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ultraTabPageControl2.ResumeLayout(false);
            this.ultraTabPageControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdStationList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pumpCycleDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUniformInterval2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAverageInflow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnalysisDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMultiPumpRunTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStationAverageCycleTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEndDateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBeginDateTime)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPumpSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cycleSummaryBindingSource)).EndInit();
            this.tabMessages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operationsDataBindingSource)).EndInit();
            this.tabRawCycleData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cycleDataBindingSource)).EndInit();
            this.tabWetWellDepth.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid7)).EndInit();
            this.tabWetWellInfo.ResumeLayout(false);
            this.tabWetWellInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeadActiveWWVol)).EndInit();
            this.ultraTabPageControl1.ResumeLayout(false);
            this.ultraTabPageControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCycleDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPSName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWWVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartDate)).EndInit();
            this.grpPumpRateSummary.ResumeLayout(false);
            this.grpPumpRateSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInflow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExludedRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPumpThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPumpRate4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPumpRate3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPumpRate2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPumpRate1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUniformInterval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCalendarCombo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraCalendarCombo2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtCycleDetail;
        private Infragistics.Win.Misc.UltraButton btnLoadCycleDetail;
        private Infragistics.Win.Misc.UltraButton btnBrowse;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPSName;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtWWVolume;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtStartDate;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPumpRate1;
        private System.Windows.Forms.GroupBox grpPumpRateSummary;
        private Infragistics.Win.Misc.UltraLabel ultraLabel8;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPumpThreshold;
        private Infragistics.Win.Misc.UltraLabel ultraLabel7;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPumpRate4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel6;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPumpRate3;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPumpRate2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel9;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtExludedRowCount;
        private Infragistics.Win.Misc.UltraLabel ultraLabel14;
        private System.Windows.Forms.TrackBar trackBar1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel10;
        private Infragistics.Win.Misc.UltraButton btnExport;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtUniformInterval;
        private Infragistics.Win.Misc.UltraLabel ultraLabel11;
        private System.Windows.Forms.GroupBox groupBox1;
        private PumpCycleDataSet pumpCycleDS;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Infragistics.Win.Misc.UltraDropDownButton ultraDropDownButton1;
        private Infragistics.Win.Misc.UltraPopupControlContainer ultraPopupControlContainer1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Infragistics.Win.Misc.UltraButton btnRecalculate;
        private Infragistics.Win.Misc.UltraLabel ultraLabel12;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtEndDate;
        private Infragistics.Win.Misc.UltraButton btnMerge;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel13;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtInflow;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl tabControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel17;
        private Infragistics.Win.Misc.UltraLabel ultraLabel16;
        private Infragistics.Win.Misc.UltraLabel ultraLabel15;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarCombo cmbEndDateTime;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarInfo ultraCalendarInfo1;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarLook ultraCalendarLook1;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarCombo cmbBeginDateTime;
        private System.Windows.Forms.BindingSource stationBindingSource;
        private PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.StationTableAdapter stationTableAdapter;
        private Infragistics.Win.Misc.UltraButton btnProcessCycleData;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid3;
        private System.Windows.Forms.BindingSource cycleDataBindingSource;
        private PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.CycleDataTableAdapter cycleDataTableAdapter;
        private Infragistics.Win.Misc.UltraLabel ultraLabel23;
        private Infragistics.Win.Misc.UltraLabel ultraLabel24;
        private Infragistics.Win.Misc.UltraLabel ultraLabel25;
        private Infragistics.Win.Misc.UltraLabel ultraLabel26;
        private Infragistics.Win.Misc.UltraLabel ultraLabel27;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel28;
        private Infragistics.Win.Misc.UltraLabel ultraLabel29;
        private Infragistics.Win.Misc.UltraLabel ultraLabel30;
        private System.Windows.Forms.ListBox listBox1;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarCombo ultraCalendarCombo1;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarCombo ultraCalendarCombo2;
        private System.Windows.Forms.BindingSource cycleSummaryBindingSource;
        private Infragistics.Win.Misc.UltraLabel ultraLabel20;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtAnalysisDuration;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid6;
        private System.Windows.Forms.BindingSource operationsDataBindingSource;
        private PumpCycleDataAnalyzer.PumpCycleDataSetTableAdapters.OperationsDataTableAdapter operationsDataTableAdapter;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtMultiPumpRunTime;
        private Infragistics.Win.Misc.UltraLabel ultraLabel31;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtStationAverageCycleTime;
        private Infragistics.Win.Misc.UltraLabel ultraLabel22;
        private Infragistics.Win.Misc.UltraLabel ultraLabel32;
        private Infragistics.Win.UltraWinChart.UltraChart ultraChart1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtAverageInflow;
        private Infragistics.Win.Misc.UltraLabel ultraLabel33;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtLeadActiveWWVol;
        private Infragistics.Win.Misc.UltraLabel ultraLabel34;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor chkUniformInterval;
        private Infragistics.Win.Misc.UltraButton btnExportChart;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtUniformInterval2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel35;
        private System.Windows.Forms.Splitter splitter1;
        private Infragistics.Win.Misc.UltraButton btnProcessHydraCycle;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdStationList;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor chkRetrieveWetWellLevel;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabMessages;
        private System.Windows.Forms.TabPage tabRawCycleData;
        private System.Windows.Forms.TabPage tabPumpSummary;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid5;
        private System.Windows.Forms.TabPage tabWetWellDepth;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid7;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor chkCustomWetwellVolume;
        private System.Windows.Forms.TabPage tabWetWellInfo;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid8;

    }
}

