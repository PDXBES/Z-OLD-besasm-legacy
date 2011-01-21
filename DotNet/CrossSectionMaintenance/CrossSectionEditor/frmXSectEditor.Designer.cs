namespace SystemsAnalysis.EMGAATS.CrossSectionEditor
{
    partial class frmXSectEditor
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
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement1 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.GradientEffect gradientEffect1 = new Infragistics.UltraChart.Resources.Appearance.GradientEffect();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("commitCrossSections");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("firstCrossSection");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("previousCrossSection");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("nextCrossSection");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("lastCrossSection");
            Infragistics.Win.UltraWinToolbars.RibbonTab ribbonTab1 = new Infragistics.Win.UltraWinToolbars.RibbonTab("ribbon1");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup1 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("ribbonGroup1");
            Infragistics.Win.UltraWinToolbars.RibbonTab ribbonTab2 = new Infragistics.Win.UltraWinToolbars.RibbonTab("ribbon2");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup2 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("ribbonGroup1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("commitCrossSections");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("previousCrossSection");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("nextCrossSection");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("lastCrossSection");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("firstCrossSection");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXSectEditor));
            this.printForm1 = new Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(this.components);
            this.ultraChart1 = new Infragistics.Win.UltraWinChart.UltraChart();
            this.optStationOrder = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.lblStationOrder = new Infragistics.Win.Misc.UltraLabel();
            this.frmXSectEditor_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this._frmXSectEditor_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._frmXSectEditor_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._frmXSectEditor_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._frmXSectEditor_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
            this.appStylistRuntime1 = new Infragistics.Win.AppStyling.Runtime.AppStylistRuntime(this.components);
            this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this.txtXSectFile = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblLeftOverbankStation = new Infragistics.Win.Misc.UltraLabel();
            this.lblRightOverbankStation = new Infragistics.Win.Misc.UltraLabel();
            this.lblLeftRoughness = new Infragistics.Win.Misc.UltraLabel();
            this.lblRightRoughness = new Infragistics.Win.Misc.UltraLabel();
            this.lblMainRoughness = new Infragistics.Win.Misc.UltraLabel();
            this.pnlXSectSettings = new Infragistics.Win.Misc.UltraPanel();
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optStationOrder)).BeginInit();
            this.frmXSectEditor_Fill_Panel.ClientArea.SuspendLayout();
            this.frmXSectEditor_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXSectFile)).BeginInit();
            this.pnlXSectSettings.ClientArea.SuspendLayout();
            this.pnlXSectSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // printForm1
            // 
            this.printForm1.DocumentName = "document";
            this.printForm1.Form = this;
            this.printForm1.PrintAction = System.Drawing.Printing.PrintAction.PrintToPrinter;
            this.printForm1.PrinterSettings = ((System.Drawing.Printing.PrinterSettings)(resources.GetObject("printForm1.PrinterSettings")));
            this.printForm1.PrintFileName = null;
            // 
            //			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
            //			'ChartType' must be persisted ahead of any Axes change made in design time.
            //		
            this.ultraChart1.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.StepLineChart;
            // 
            // ultraChart1
            // 
            this.ultraChart1.Axis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            paintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            paintElement1.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.ultraChart1.Axis.PE = paintElement1;
            this.ultraChart1.Axis.X.Extent = 35;
            this.ultraChart1.Axis.X.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.X.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
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
            this.ultraChart1.Axis.X.Margin.Far.Value = 0.48780487804878048;
            this.ultraChart1.Axis.X.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.X.TickmarkInterval = 50;
            this.ultraChart1.Axis.X.TickmarkIntervalType = Infragistics.UltraChart.Shared.Styles.AxisIntervalType.Hours;
            this.ultraChart1.Axis.X.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.X.Visible = true;
            this.ultraChart1.Axis.X2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL:MM-dd-yy>";
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
            this.ultraChart1.Axis.Y.Extent = 35;
            this.ultraChart1.Axis.Y.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChart1.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
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
            this.ultraChart1.Axis.Y2.TickmarkInterval = 40000;
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
            this.ultraChart1.ColorModel.ColorBegin = System.Drawing.Color.Pink;
            this.ultraChart1.ColorModel.ColorEnd = System.Drawing.Color.DarkRed;
            this.ultraChart1.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
            this.ultraChart1.Effects.Effects.Add(gradientEffect1);
            this.ultraChart1.Legend.Location = Infragistics.UltraChart.Shared.Styles.LegendLocation.Top;
            this.ultraChart1.Legend.Visible = true;
            this.ultraChart1.Location = new System.Drawing.Point(84, 48);
            this.ultraChart1.Name = "ultraChart1";
            this.ultraChart1.Size = new System.Drawing.Size(400, 300);
            this.ultraChart1.TabIndex = 0;
            this.ultraChart1.TitleBottom.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ultraChart1.TitleBottom.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.TitleBottom.Text = "Station (ft)";
            this.ultraChart1.TitleLeft.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ultraChart1.TitleLeft.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.TitleLeft.Text = "Elevation (ft)";
            this.ultraChart1.TitleLeft.Visible = true;
            this.ultraChart1.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray;
            this.ultraChart1.Tooltips.TooltipControl = null;
            // 
            // optStationOrder
            // 
            this.optStationOrder.CheckedIndex = 0;
            valueListItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            valueListItem1.DataValue = "firstStationOnLeft";
            valueListItem1.DisplayText = "Left";
            valueListItem2.DataValue = "firstStationOnRight";
            valueListItem2.DisplayText = "Right";
            this.optStationOrder.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.optStationOrder.Location = new System.Drawing.Point(283, 415);
            this.optStationOrder.Name = "optStationOrder";
            this.optStationOrder.Size = new System.Drawing.Size(99, 20);
            this.optStationOrder.TabIndex = 1;
            this.optStationOrder.Text = "Left";
            // 
            // lblStationOrder
            // 
            this.lblStationOrder.AutoSize = true;
            this.lblStationOrder.Location = new System.Drawing.Point(197, 418);
            this.lblStationOrder.Name = "lblStationOrder";
            this.lblStationOrder.Size = new System.Drawing.Size(86, 14);
            this.lblStationOrder.TabIndex = 2;
            this.lblStationOrder.Text = "First Station On:";
            // 
            // frmXSectEditor_Fill_Panel
            // 
            // 
            // frmXSectEditor_Fill_Panel.ClientArea
            // 
            this.frmXSectEditor_Fill_Panel.ClientArea.Controls.Add(this.pnlXSectSettings);
            this.frmXSectEditor_Fill_Panel.ClientArea.Controls.Add(this.txtXSectFile);
            this.frmXSectEditor_Fill_Panel.ClientArea.Controls.Add(this.ultraButton1);
            this.frmXSectEditor_Fill_Panel.ClientArea.Controls.Add(this.lblStationOrder);
            this.frmXSectEditor_Fill_Panel.ClientArea.Controls.Add(this.optStationOrder);
            this.frmXSectEditor_Fill_Panel.ClientArea.Controls.Add(this.ultraChart1);
            this.frmXSectEditor_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frmXSectEditor_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmXSectEditor_Fill_Panel.Location = new System.Drawing.Point(4, 134);
            this.frmXSectEditor_Fill_Panel.Name = "frmXSectEditor_Fill_Panel";
            this.frmXSectEditor_Fill_Panel.Size = new System.Drawing.Size(756, 535);
            this.frmXSectEditor_Fill_Panel.TabIndex = 0;
            // 
            // _frmXSectEditor_Toolbars_Dock_Area_Left
            // 
            this._frmXSectEditor_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmXSectEditor_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmXSectEditor_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._frmXSectEditor_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmXSectEditor_Toolbars_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frmXSectEditor_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 134);
            this._frmXSectEditor_Toolbars_Dock_Area_Left.Name = "_frmXSectEditor_Toolbars_Dock_Area_Left";
            this._frmXSectEditor_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(4, 535);
            this._frmXSectEditor_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _frmXSectEditor_Toolbars_Dock_Area_Right
            // 
            this._frmXSectEditor_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmXSectEditor_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmXSectEditor_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._frmXSectEditor_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmXSectEditor_Toolbars_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frmXSectEditor_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(760, 134);
            this._frmXSectEditor_Toolbars_Dock_Area_Right.Name = "_frmXSectEditor_Toolbars_Dock_Area_Right";
            this._frmXSectEditor_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(4, 535);
            this._frmXSectEditor_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _frmXSectEditor_Toolbars_Dock_Area_Top
            // 
            this._frmXSectEditor_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmXSectEditor_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmXSectEditor_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._frmXSectEditor_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmXSectEditor_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frmXSectEditor_Toolbars_Dock_Area_Top.Name = "_frmXSectEditor_Toolbars_Dock_Area_Top";
            this._frmXSectEditor_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(764, 134);
            this._frmXSectEditor_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _frmXSectEditor_Toolbars_Dock_Area_Bottom
            // 
            this._frmXSectEditor_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmXSectEditor_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmXSectEditor_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._frmXSectEditor_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmXSectEditor_Toolbars_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frmXSectEditor_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 669);
            this._frmXSectEditor_Toolbars_Dock_Area_Bottom.Name = "_frmXSectEditor_Toolbars_Dock_Area_Bottom";
            this._frmXSectEditor_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(764, 4);
            this._frmXSectEditor_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Cross-Section File|*.xml";
            this.openFileDialog.Title = "Cross-Section File";
            // 
            // ultraButton1
            // 
            this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2010Button;
            this.ultraButton1.Location = new System.Drawing.Point(70, 406);
            this.ultraButton1.Name = "ultraButton1";
            this.ultraButton1.Size = new System.Drawing.Size(100, 51);
            this.ultraButton1.TabIndex = 3;
            this.ultraButton1.Text = "Select Cross-Section File";
            this.ultraButton1.Click += new System.EventHandler(this.ultraButton1_Click);
            // 
            // ultraToolbarsManager1
            // 
            this.ultraToolbarsManager1.DesignerFlags = 1;
            this.ultraToolbarsManager1.DockWithinContainer = this;
            this.ultraToolbarsManager1.DockWithinContainerBaseType = typeof(System.Windows.Forms.Form);
            this.ultraToolbarsManager1.MiniToolbar.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool1,
            buttonTool5,
            buttonTool2,
            buttonTool3,
            buttonTool4});
            ribbonTab1.Caption = "ribbon1";
            ribbonGroup1.Caption = "ribbonGroup1";
            ribbonTab1.Groups.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonGroup[] {
            ribbonGroup1});
            ribbonTab2.Caption = "ribbon2";
            ribbonGroup2.Caption = "ribbonGroup1";
            ribbonTab2.Groups.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonGroup[] {
            ribbonGroup2});
            this.ultraToolbarsManager1.Ribbon.NonInheritedRibbonTabs.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonTab[] {
            ribbonTab1,
            ribbonTab2});
            this.ultraToolbarsManager1.Ribbon.Visible = true;
            this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2010;
            buttonTool6.SharedPropsInternal.Caption = "Commit Cross Sections";
            buttonTool7.SharedPropsInternal.Caption = "Previous Cross-Section";
            buttonTool8.SharedPropsInternal.Caption = "Next Cross-Section";
            buttonTool9.SharedPropsInternal.Caption = "Last Cross-Section";
            buttonTool10.SharedPropsInternal.Caption = "First Cross-Section";
            this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool6,
            buttonTool7,
            buttonTool8,
            buttonTool9,
            buttonTool10});
            this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager1_ToolClick);
            // 
            // txtXSectFile
            // 
            this.txtXSectFile.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2010;
            this.txtXSectFile.Location = new System.Drawing.Point(70, 463);
            this.txtXSectFile.Name = "txtXSectFile";
            this.txtXSectFile.NullText = "(no file selected)";
            this.txtXSectFile.ReadOnly = true;
            this.txtXSectFile.ShowOverflowIndicator = true;
            this.txtXSectFile.Size = new System.Drawing.Size(289, 21);
            this.txtXSectFile.TabIndex = 4;
            // 
            // lblLeftOverbankStation
            // 
            this.lblLeftOverbankStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblLeftOverbankStation.Location = new System.Drawing.Point(3, 72);
            this.lblLeftOverbankStation.Name = "lblLeftOverbankStation";
            this.lblLeftOverbankStation.Size = new System.Drawing.Size(153, 23);
            this.lblLeftOverbankStation.TabIndex = 5;
            this.lblLeftOverbankStation.Text = "Left Overbank Station:";
            // 
            // lblRightOverbankStation
            // 
            this.lblRightOverbankStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblRightOverbankStation.Location = new System.Drawing.Point(3, 95);
            this.lblRightOverbankStation.Name = "lblRightOverbankStation";
            this.lblRightOverbankStation.Size = new System.Drawing.Size(153, 23);
            this.lblRightOverbankStation.TabIndex = 6;
            this.lblRightOverbankStation.Text = "Right Overbank Station:";
            // 
            // lblLeftRoughness
            // 
            this.lblLeftRoughness.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblLeftRoughness.Location = new System.Drawing.Point(3, 26);
            this.lblLeftRoughness.Name = "lblLeftRoughness";
            this.lblLeftRoughness.Size = new System.Drawing.Size(153, 23);
            this.lblLeftRoughness.TabIndex = 7;
            this.lblLeftRoughness.Text = "Left Overbank Roughness:";
            // 
            // lblRightRoughness
            // 
            this.lblRightRoughness.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblRightRoughness.Location = new System.Drawing.Point(3, 49);
            this.lblRightRoughness.Name = "lblRightRoughness";
            this.lblRightRoughness.Size = new System.Drawing.Size(153, 23);
            this.lblRightRoughness.TabIndex = 8;
            this.lblRightRoughness.Text = "Right Overbank Roughness:";
            // 
            // lblMainRoughness
            // 
            this.lblMainRoughness.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblMainRoughness.Location = new System.Drawing.Point(3, 3);
            this.lblMainRoughness.Name = "lblMainRoughness";
            this.lblMainRoughness.Size = new System.Drawing.Size(153, 23);
            this.lblMainRoughness.TabIndex = 9;
            this.lblMainRoughness.Text = "Main Channel Roughness:";
            // 
            // pnlXSectSettings
            // 
            // 
            // pnlXSectSettings.ClientArea
            // 
            this.pnlXSectSettings.ClientArea.Controls.Add(this.lblMainRoughness);
            this.pnlXSectSettings.ClientArea.Controls.Add(this.lblLeftOverbankStation);
            this.pnlXSectSettings.ClientArea.Controls.Add(this.lblRightRoughness);
            this.pnlXSectSettings.ClientArea.Controls.Add(this.lblRightOverbankStation);
            this.pnlXSectSettings.ClientArea.Controls.Add(this.lblLeftRoughness);
            this.pnlXSectSettings.Location = new System.Drawing.Point(490, 48);
            this.pnlXSectSettings.Name = "pnlXSectSettings";
            this.pnlXSectSettings.Size = new System.Drawing.Size(212, 267);
            this.pnlXSectSettings.TabIndex = 10;
            // 
            // frmXSectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 673);
            this.Controls.Add(this.frmXSectEditor_Fill_Panel);
            this.Controls.Add(this._frmXSectEditor_Toolbars_Dock_Area_Left);
            this.Controls.Add(this._frmXSectEditor_Toolbars_Dock_Area_Right);
            this.Controls.Add(this._frmXSectEditor_Toolbars_Dock_Area_Top);
            this.Controls.Add(this._frmXSectEditor_Toolbars_Dock_Area_Bottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmXSectEditor";
            this.Text = "EMGAATS Cross-Section Editor";
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optStationOrder)).EndInit();
            this.frmXSectEditor_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frmXSectEditor_Fill_Panel.ClientArea.PerformLayout();
            this.frmXSectEditor_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtXSectFile)).EndInit();
            this.pnlXSectSettings.ClientArea.ResumeLayout(false);
            this.pnlXSectSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.Printing.PrintForm printForm1;
        private Infragistics.Win.UltraWinChart.UltraChart ultraChart1;
        private Infragistics.Win.UltraWinEditors.UltraOptionSet optStationOrder;
        private Infragistics.Win.Misc.UltraLabel lblStationOrder;
        private Infragistics.Win.Misc.UltraPanel frmXSectEditor_Fill_Panel;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmXSectEditor_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager1;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmXSectEditor_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmXSectEditor_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmXSectEditor_Toolbars_Dock_Area_Bottom;
        private Infragistics.Win.Misc.UltraButton ultraButton1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Infragistics.Win.AppStyling.Runtime.AppStylistRuntime appStylistRuntime1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtXSectFile;
        private Infragistics.Win.Misc.UltraLabel lblMainRoughness;
        private Infragistics.Win.Misc.UltraLabel lblRightRoughness;
        private Infragistics.Win.Misc.UltraLabel lblLeftRoughness;
        private Infragistics.Win.Misc.UltraLabel lblRightOverbankStation;
        private Infragistics.Win.Misc.UltraLabel lblLeftOverbankStation;
        private Infragistics.Win.Misc.UltraPanel pnlXSectSettings;
    }
}

