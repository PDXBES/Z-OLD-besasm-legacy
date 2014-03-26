using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Xml;
using System.Reflection;
using System.IO;
using Infragistics.UltraChart.Core;

namespace SystemsAnalysis.EMGAATS.CrossSectionEditor
{
  public partial class frmXSectEditor : Form
  {
    private enum ChartAction
    {
      None, SetLeftStation, SetRightStation,
      AddPoint, MovePoint, MovingPoint, DeletePoint
    };
    private ChartAction chartAction = ChartAction.None;
    private IAdvanceAxis XAxis { get; set; }
    private IAdvanceAxis YAxis { get; set; }
    private ProcessedXSectDataSet.PointListRow activePointListRow = null;


    public frmXSectEditor()
    {
      InitializeComponent();
      //chrtXSectDisplay.DataBindings.Clear();      
      chrtXSectDisplay.FillSceneGraph += new Infragistics.UltraChart.Shared.Events.FillSceneGraphEventHandler(chrtXSectDisplay_FillSceneGraph);

    }

    private void frmXSectEditor_Load(object sender, EventArgs e)
    {

    }

    void chrtXSectDisplay_FillSceneGraph(object sender, Infragistics.UltraChart.Shared.Events.FillSceneGraphEventArgs e)
    {
      if (e.SceneGraph.Count > 2)
        chrtXSectDisplay.InvalidDataReceived += new Infragistics.UltraChart.Shared.Events.ChartDataInvalidEventHandler(chrtXSectDisplay_InvalidDataReceived);

      this.XAxis = (IAdvanceAxis)e.Grid["X"];
      this.YAxis = (IAdvanceAxis)e.Grid["Y"];
    }

    void chrtXSectDisplay_InvalidDataReceived(object sender, Infragistics.UltraChart.Shared.Events.ChartDataInvalidEventArgs e)
    {
      e.LabelStyle.FontColor = Color.Blue;
      e.LabelStyle.FontSizeBestFit = false;
      e.LabelStyle.HorizontalAlign = StringAlignment.Center;
      e.LabelStyle.Font = new Font("Verdana", 12);
      e.Text = chrtXSectDisplay.EmptyChartText;
      chrtXSectDisplay.InvalidDataReceived -= new Infragistics.UltraChart.Shared.Events.ChartDataInvalidEventHandler(chrtXSectDisplay_InvalidDataReceived);
    }

    void ValidationCallBacKHandler(object sender, ValidationEventArgs e)
    {
      chrtXSectDisplay.DataSource = null;
      chrtXSectDisplay.EmptyChartText += "\nError: " + e.Message;
    }

    private void bindingSource1_CurrentChanged(object sender, EventArgs e)
    {
      if (bindingSource1.Count > 0)
      {
        ProcessedXSectDataSet.XSectsRow xSectsRow = SelectedXSectsRow;
        LoadXSect(xSectsRow.XSectName);
      }
    }

    private void bindingSource1_BindingComplete(object sender, BindingCompleteEventArgs e)
    {
      if (e.BindingCompleteContext ==
        BindingCompleteContext.DataSourceUpdate && e.Exception == null)
        e.Binding.BindingManagerBase.EndCurrentEdit();
    }

    private void optStationOrder_ValueChanged(object sender, EventArgs e)
    {
      SwapLeftAndRight();
    }

    private void chkReadyForExport_CheckedValueChanged(object sender, EventArgs e)
    {
      if (chkReadyForExport.Checked)
      {
        //bindingSource1.MoveNext();
      }
    }

    private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
    {
      switch (e.Tool.Key)
      {
        case "btnSaveProcessedData":    // ButtonTool
          SaveProcessedData();
          break;

        case "btnSendToMaster":    // ButtonTool
          ExportToMaster();
          break;

        case "btnLoadProcessedData":    // ButtonTool
          LoadProcessedData();
          break;

        case "btnLoadRawXSect":    // ButtonTool
          LoadRawData();
          break;

        case "btnExportCSV":
          ExportCSV();
          break;

        case "btnExit":    // ButtonTool
          Application.Exit();
          break;

        case "btnPrint":    // ButtonTool
          PrintXSects();
          break;

      }

      return;
    }

    private void PrintXSects()
    {

      //if (printPreviewDialog1.ShowDialog() != DialogResult.OK)
      //  return;

      if (printDialog1.ShowDialog() != DialogResult.OK)
        return;

      printDocument1.PrinterSettings = printDialog1.PrinterSettings;
      printDocument1.Print();

      //printPreviewDialog1.Document
    }

    private void ExportCSV()
    {
      if (saveFileDialog.ShowDialog() != DialogResult.OK)
        return;

      string csvFile = saveFileDialog.FileName;

      StreamWriter writer = new StreamWriter(csvFile);

      writer.WriteLine("XSectName, Station, Description, " +
        "MainChannelRoughness, LeftOverbankRoughness, RightOverbankRoughness, " +
        "LeftOverbankStation, RightOverbankStation, " +
        "LeftOverbankLengthFactor, RightOverbankLengthFactor");
      foreach (ProcessedXSectDataSet.XSectsRow row in processedXSectDS.XSects)
      {
        writer.WriteLine(row.XSectName + "," + row.Station + "," + row.Description + "," +
          row.MainChannelRoughness + "," +
          row.LeftOverbankRoughness + "," + row.RightOverbankRoughness + "," +
          row.LeftOverbankStation + "," + row.RightOverbankStation + "," +
          row.LeftOverbankLengthFactor + "," + row.RightOverbankLengthFactor);
      }
      writer.Close();
    }

    private ProcessedXSectDataSet.XSectsRow SelectedXSectsRow
    {
      get
      {
        if (bindingSource1.Current == null)
          return null;
        else
          return (ProcessedXSectDataSet.XSectsRow)((System.Data.DataRowView)bindingSource1.Current).Row;
      }
    }

    private void SwapLeftAndRight()
    {
      var pointsQuery = from points in processedXSectDS.PointList select points;
      foreach (ProcessedXSectDataSet.PointListRow row in pointsQuery)
      {
        row.Station = row.Station * -1;
      }

      var xSectsQuery = from xsects in processedXSectDS.XSects select xsects;
      foreach (ProcessedXSectDataSet.XSectsRow row in xSectsQuery)
      {
        double temp = row.LeftOverbankStation;
        row.LeftOverbankStation = row.RightOverbankStation * -1;
        row.RightOverbankStation = temp * -1;
      }

      LoadXSect(SelectedXSectsRow.XSectName);
      return;
    }

    private void SaveProcessedData()
    {
      if (saveFileDialog.ShowDialog() != DialogResult.OK)
        return;

      processedXSectDS.WriteXml(saveFileDialog.FileName);
    }

    private void LoadProcessedData()
    {
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;

      chrtXSectDisplay.DataBindings.Clear();
      processedXSectDS.Clear();
      try
      {
        processedXSectDS.ReadXml(openFileDialog.FileName);
        LoadXSect(processedXSectDS.XSects[0].XSectName);
        ConfigureChart();
      }
      catch (Exception ex)
      {
        chrtXSectDisplay.EmptyChartText = "Error loading processed xml file: " + ex.Message;
      }
    }

    private void ExportToMaster()
    {
      try
      {
        MstXSectionDataSet mstXSectDS = new MstXSectionDataSet();
        int xSectCount, xSectDataCount;
        xSectCount = UpdatedMstXSect(mstXSectDS);
        xSectDataCount = UpdateMstXSectData(mstXSectDS);
        MessageBox.Show("Succesfully sent " + xSectCount +
          " cross-sections containing " + xSectDataCount +
          " points to master database.", "Successfully sent data to master",
          MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error sending data to master: " +
          ex.Message, "Error sending data to master!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      return;
    }

    private int UpdateMstXSectData(MstXSectionDataSet mstXSectDS)
    {
      int i = 0;
      MstXSectionDataSetTableAdapters.mst_xsectionsTableAdapter mstXSectTA =
        new SystemsAnalysis.EMGAATS.CrossSectionEditor.MstXSectionDataSetTableAdapters.mst_xsectionsTableAdapter();
      MstXSectionDataSetTableAdapters.mst_xsection_dataTableAdapter mstXSectDataTA =
        new SystemsAnalysis.EMGAATS.CrossSectionEditor.MstXSectionDataSetTableAdapters.mst_xsection_dataTableAdapter();

      foreach (ProcessedXSectDataSet.XSectsRow procRow in processedXSectDS.XSects.Where(p => p.ReadyForExport))
      {
        int mlinkID = Int32.Parse(procRow.XSectName);
        MstXSectionDataSet.mst_xsectionsRow mstXSectRow =
          mstXSectDS.mst_xsections.FindByMLinkId(mlinkID);

        int j = 1;
        foreach (ProcessedXSectDataSet.PointListRow pointRow in procRow.GetPointListRows().OrderBy(p => p.Station))
        {
          MstXSectionDataSet.mst_xsection_dataRow mstXSectDataRow =
            mstXSectDS.mst_xsection_data.Newmst_xsection_dataRow();

          mstXSectDataRow.elevation = Math.Round(pointRow.Elevation, 2);
          mstXSectDataRow.station = Math.Round(pointRow.Station, 2);
          mstXSectDataRow.index = j++;
          mstXSectDataRow.mst_xsections_id = mstXSectRow.mst_xsections_id;
          mstXSectDataRow.SetParentRow(mstXSectRow);
          mstXSectDS.mst_xsection_data.Addmst_xsection_dataRow(mstXSectDataRow);
          i++;
        }
      }
      mstXSectDataTA.Update(mstXSectDS.mst_xsection_data);
      return i;
    }

    private int UpdatedMstXSect(MstXSectionDataSet mstXSectDS)
    {
      int i = 0;
      MstXSectionDataSetTableAdapters.mst_xsectionsTableAdapter mstXSectTA =
        new SystemsAnalysis.EMGAATS.CrossSectionEditor.MstXSectionDataSetTableAdapters.mst_xsectionsTableAdapter();
      MstXSectionDataSetTableAdapters.mst_xsection_dataTableAdapter mstXSectDataTA =
        new SystemsAnalysis.EMGAATS.CrossSectionEditor.MstXSectionDataSetTableAdapters.mst_xsection_dataTableAdapter();

      mstXSectTA.Fill(mstXSectDS.mst_xsections);
      mstXSectDataTA.Fill(mstXSectDS.mst_xsection_data);


      foreach (ProcessedXSectDataSet.XSectsRow procRow in processedXSectDS.XSects.Where(p => p.ReadyForExport))
      {
        int mlinkID = Int32.Parse(procRow.XSectName);

        MstXSectionDataSet.mst_xsectionsRow mstXSectRow =
          mstXSectDS.mst_xsections.FindByMLinkId(mlinkID);
        if (mstXSectRow != null)
          mstXSectRow.Delete();

        mstXSectRow = mstXSectDS.mst_xsections.Newmst_xsectionsRow();

        mstXSectRow.mlinkid = mlinkID;
        mstXSectRow.roughness_main = procRow.MainChannelRoughness;
        mstXSectRow.roughness_left = procRow.LeftOverbankRoughness;
        mstXSectRow.roughness_right = procRow.RightOverbankRoughness;
        mstXSectRow.station_left = Math.Round(procRow.LeftOverbankStation, 2);
        mstXSectRow.station_right = Math.Round(procRow.RightOverbankStation, 2);
        mstXSectRow.length_factor_left = procRow.LeftOverbankLengthFactor;
        mstXSectRow.length_factor_right = procRow.RightOverbankLengthFactor;

        mstXSectDS.mst_xsections.Addmst_xsectionsRow(mstXSectRow);
        i++;
      }

      mstXSectTA.Update(mstXSectDS.mst_xsections);
      mstXSectDS.mst_xsection_data.Clear();
      mstXSectTA.Fill(mstXSectDS.mst_xsections); //Need to fill again to get autonumbered primary keys
      return i;
    }

    private void LoadRawData()
    {
      chrtXSectDisplay.DataBindings.Clear();
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;

      processedXSectDS.Clear();
      if (!ValidateRawXml(openFileDialog.FileName))
        return;

      if (!LoadRawXml(openFileDialog.FileName))
        return;

      LoadXSect(processedXSectDS.XSects[0].XSectName);

      ConfigureChart();
    }

    private void ConfigureChart()
    {
      chrtXSectDisplay.DataSource = processedXSectDS.ChartTable;
      chrtXSectDisplay.ScatterChart.ColumnX = 1;
      chrtXSectDisplay.ScatterChart.ColumnY = 2;
      chrtXSectDisplay.ScatterChart.GroupByColumn = 0;
      chrtXSectDisplay.ScatterChart.UseGroupByColumn = true;

      this.bindingSource1.CurrentChanged += new System.EventHandler(this.bindingSource1_CurrentChanged);
    }

    private bool ValidateRawXml(string rawXml)
    {
      chrtXSectDisplay.EmptyChartText = "Validating xml...";

      Stream xsdStream = System.Reflection.Assembly.
                      GetExecutingAssembly().GetManifestResourceStream("SystemsAnalysis.EMGAATS.CrossSectionEditor.LandXML-1.2.xsd");

      XmlSchema rawSchema;
      rawSchema = XmlSchema.Read(xsdStream, null);

      XmlSchemaSet schemaSet = new XmlSchemaSet();
      schemaSet.Add(rawSchema);

      XmlReaderSettings settings = new XmlReaderSettings();
      settings.Schemas.Add(schemaSet);
      settings.ValidationType = ValidationType.Schema;
      settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;

      XmlReader reader = XmlReader.Create(rawXml, settings);

      try
      {
        XmlDocument doc = new XmlDocument();
        doc.Load(reader);
        if (doc.DocumentElement.Name != "LandXML")
          return false;

        while (reader.Read())
        {
        }
      }
      catch
      {
        chrtXSectDisplay.EmptyChartText = "Error reading LandXML File";
        return false;
      }
      finally
      {
        reader.Close();
      }
      return true;
    }

    private bool LoadRawXml(string rawXml)
    {
      chrtXSectDisplay.EmptyChartText = "Loading xml...";
      XmlReader reader = XmlReader.Create(openFileDialog.FileName);
      bool loaded = false;

      //landXmlDoc.Schemas.Add(
      while (reader.ReadToFollowing("CrossSect"))
      {
        if (reader.Name != "CrossSect")
          continue;

        ProcessedXSectDataSet.XSectsRow xSectsRow =
            processedXSectDS.XSects.NewXSectsRow();

        xSectsRow.Description = "";
        xSectsRow.LongName = reader["name"];
        xSectsRow.Station = Double.Parse(reader["sta"]);

        reader.ReadToFollowing("CrossSectSurf");
        xSectsRow.XSectName = reader["name"];
        xSectsRow.Description = reader["desc"];
        reader.ReadToFollowing("PntList2D");
        reader.Read();
        string[] pointList = reader.Value.Split(' ');

        xSectsRow.LeftOverbankStation = Double.MaxValue;
        xSectsRow.RightOverbankStation = Double.MinValue;
        processedXSectDS.XSects.AddXSectsRow(xSectsRow);

        for (int i = 0; i < pointList.Length; i += 2)
        {
          loaded = true;
          ProcessedXSectDataSet.PointListRow pointListRow =
            processedXSectDS.PointList.NewPointListRow();

          pointListRow.Station = Double.Parse(pointList[i]);
          pointListRow.Elevation = Double.Parse(pointList[i + 1]);
          if (xSectsRow.LeftOverbankStation > pointListRow.Station)
            xSectsRow.LeftOverbankStation = pointListRow.Station;
          if (xSectsRow.RightOverbankStation < pointListRow.Station)
            xSectsRow.RightOverbankStation = pointListRow.Station;

          pointListRow.SetParentRow(xSectsRow);
          processedXSectDS.PointList.AddPointListRow(pointListRow);
        }

      }

      this.bindingSource1.CurrentChanged += new System.EventHandler(this.bindingSource1_CurrentChanged);
      reader.Close();
      return loaded;
    }

    private void LoadXSect(string xSectName)
    {
      processedXSectDS.ChartTable.Clear();

      ProcessedXSectDataSet.XSectsRow xSectRow
        = processedXSectDS.XSects.FindByXSectName(xSectName);

      double minElev = Double.MaxValue;
      double maxElev = Double.MinValue;

      foreach (ProcessedXSectDataSet.PointListRow pointRow in xSectRow.GetPointListRows().OrderBy(p => p.Station))
      {
        processedXSectDS.ChartTable.AddChartTableRow("Cross-Section", pointRow.Station, pointRow.Elevation);

        if (minElev > pointRow.Elevation)
          minElev = pointRow.Elevation;
        if (maxElev < pointRow.Elevation)
          maxElev = pointRow.Elevation;
      }

      processedXSectDS.ChartTable.AddChartTableRow("Left Overbank Station", xSectRow.LeftOverbankStation, minElev);
      processedXSectDS.ChartTable.AddChartTableRow("Left Overbank Station", xSectRow.LeftOverbankStation, maxElev);
      processedXSectDS.ChartTable.AddChartTableRow("Right Overbank Station", xSectRow.RightOverbankStation, minElev);
      processedXSectDS.ChartTable.AddChartTableRow("Right Overbank Station", xSectRow.RightOverbankStation, maxElev);

      chrtXSectDisplay.TitleTop.Text = "Cross-Section " + xSectName + " (Longitudinal Station: " + xSectRow.LongName + ")";
    }

    private void bindingNavigatorToggleExport_Click(object sender, EventArgs e)
    {
      var q = from proc in processedXSectDS.XSects select proc.ReadyForExport;
      foreach (ProcessedXSectDataSet.XSectsRow row in processedXSectDS.XSects)
      {
        row.ReadyForExport = !row.ReadyForExport;
      }
    }

    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      int x = this.chrtXSectDisplay.Location.X;
      int y = this.chrtXSectDisplay.Location.Y;

      int width = chrtXSectDisplay.Width;
      int height = chrtXSectDisplay.Height;

      Rectangle bounds = new Rectangle(0, 0, width, height);

      Bitmap img = new Bitmap(width, height);
      chrtXSectDisplay.DrawToBitmap(img, bounds);

      Point p = new Point(0, 0);
      e.Graphics.DrawImage(img, e.MarginBounds.X, e.MarginBounds.Y, e.MarginBounds.Width, (int)(e.MarginBounds.Height * 0.7));

      Stream notesStream = System.Reflection.Assembly.
                      GetExecutingAssembly().GetManifestResourceStream("SystemsAnalysis.EMGAATS.CrossSectionEditor.Resources.notes.png");

      e.Graphics.DrawImage(new Bitmap(notesStream),
        e.MarginBounds.X,
        (int)(e.MarginBounds.Height * 0.7) + 100,
        e.MarginBounds.Width,
        (int)(e.MarginBounds.Height * 0.3) - 30);

      e.Graphics.DrawString(openFileDialog.FileName,
        new Font("Verdana", 10), Brushes.Black,
        new Point(e.MarginBounds.X, e.MarginBounds.Height + 130));


      if (bindingSource1.Position == bindingSource1.Count - 1)
        e.HasMorePages = false;
      else
        e.HasMorePages = true;

      bindingSource1.Position++;
    }

    private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
      bindingSource1.Position = 0;
    }

    private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

    private void chrtXSectDisplay_ChartDataClicked(object sender, Infragistics.UltraChart.Shared.Events.ChartDataEventArgs e)
    {
      ProcessedXSectDataSet.XSectsRow xSectRow = SelectedXSectsRow;
      switch (chartAction)
      {
        case ChartAction.SetLeftStation:
          if (e.DataValue < xSectRow.RightOverbankStation)
            xSectRow.LeftOverbankStation = e.DataValue;
          else
            MessageBox.Show("Left station must be on left side of channel.");
          chartAction = ChartAction.None;
          break;
        case ChartAction.SetRightStation:
          if (e.DataValue > xSectRow.LeftOverbankStation)
            xSectRow.RightOverbankStation = e.DataValue;
          else
            MessageBox.Show("Right station must be on right side of channel.");
          chartAction = ChartAction.None;
          break;
        case ChartAction.AddPoint:
          MessageBox.Show("A point already exits at this location.");
          chartAction = ChartAction.None;
          break;
        case ChartAction.DeletePoint:
          foreach (ProcessedXSectDataSet.PointListRow row in xSectRow.GetPointListRows())
          {
            if (row.Station == e.DataValue)
            {
              row.Delete();
              break;
            }
          }
          break;
        case ChartAction.MovePoint:
          foreach (ProcessedXSectDataSet.PointListRow row in xSectRow.GetPointListRows())
          {
            if (row.Station == e.DataValue)
            {
              activePointListRow = row;
              break;
            }
          }
          break;
      }


      this.LoadXSect(xSectRow.XSectName);
      Cursor = Cursors.Default;
    }

    private void chrtXSectDisplay_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.XAxis == null || this.YAxis == null)
        return;


      double xValue = (double)this.XAxis.MapInverse(e.X);
      double yValue = (double)this.YAxis.MapInverse(e.Y);
      ProcessedXSectDataSet.XSectsRow xSectRow = SelectedXSectsRow;

      switch (chartAction)
      {
        case ChartAction.AddPoint:
          processedXSectDS.PointList.AddPointListRow(xSectRow, xValue, yValue);
          LoadXSect(xSectRow.XSectName);
          break;
        case ChartAction.MovingPoint:
          break;
        default:
          //chartAction = ChartAction.None;
          break;
      }
    }

    private void txtLeftStation_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
    {
      chartAction = ChartAction.SetLeftStation;
    }

    private void txtRightStation_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
    {
      chartAction = ChartAction.SetRightStation;
    }

    private void btnSetLeftOverbank_Click(object sender, EventArgs e)
    {
      chartAction = ChartAction.SetLeftStation;
    }

    private void btnSetRightOverbank_Click(object sender, EventArgs e)
    {
      chartAction = ChartAction.SetRightStation;
    }

    private void btnMovePoint_Click(object sender, EventArgs e)
    {
      chartAction = ChartAction.MovePoint;
    }

    private void btnAddPoint_Click(object sender, EventArgs e)
    {
      chartAction = ChartAction.AddPoint;
    }

    private void btnDeletePoint_Click(object sender, EventArgs e)
    {
      chartAction = ChartAction.DeletePoint;
    }

    private void chrtXSectDisplay_MouseMove(object sender, MouseEventArgs e)
    {

      ProcessedXSectDataSet.XSectsRow xSectRow = SelectedXSectsRow;

      if (this.chartAction != ChartAction.MovePoint || activePointListRow == null)
        return;

      double xValue = (double)this.XAxis.MapInverse(e.X);
      double yValue = (double)this.YAxis.MapInverse(e.Y);

      activePointListRow.Station = xValue;
      activePointListRow.Elevation = yValue;

      LoadXSect(activePointListRow.XSectName);

    }

    private void chrtXSectDisplay_MouseUp(object sender, MouseEventArgs e)
    {
      activePointListRow = null;
    }

    private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
    {
      DialogResult result = MessageBox.Show(
        "Copy selected item? Select 'yes' to copy existing or 'no' to create an empty cross-section.",
        "Copy Existing Cross-Section?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

      if (result == DialogResult.Cancel)
        return;

      try
      {

        ProcessedXSectDataSet.XSectsRow newRow;
        newRow = this.processedXSectDS.XSects.NewXSectsRow();
        
        newRow.Description = "New Cross-Section";
        newRow = processedXSectDS.XSects.NewXSectsRow();
        if (result == DialogResult.Yes)
        {

          newRow.XSectName = SelectedXSectsRow.XSectName + "_copy";
          newRow.LongName = SelectedXSectsRow.LongName;
          newRow.Station = SelectedXSectsRow.Station;
          newRow.ReadyForExport = false;
          processedXSectDS.XSects.AddXSectsRow(newRow);

          ProcessedXSectDataSet.PointListRow[] points = SelectedXSectsRow.GetPointListRows();
          foreach (ProcessedXSectDataSet.PointListRow point in points)
          {
            ProcessedXSectDataSet.PointListRow newPoint = processedXSectDS.PointList.NewPointListRow(point.Station, point.Elevation);
            newPoint.Station = point.Station;
            newPoint.Elevation = point.Elevation;

            newPoint.SetParentRow(newRow);
            processedXSectDS.PointList.AddPointListRow(newPoint);
          }
        }
        else
        {
          newRow.XSectName = "_new";
          newRow.LongName = "";
          newRow.Station = 0.0;
          newRow.ReadyForExport = false;
          processedXSectDS.XSects.AddXSectsRow(newRow);
          ProcessedXSectDataSet.PointListRow newPoint = processedXSectDS.PointList.NewPointListRow(-5.0, 5.0);          
          newPoint.SetParentRow(newRow);
          processedXSectDS.PointList.AddPointListRow(newPoint);

          newPoint = processedXSectDS.PointList.NewPointListRow(-1.0, 0.0);          
          newPoint.SetParentRow(newRow);
          processedXSectDS.PointList.AddPointListRow(newPoint);

          newPoint = processedXSectDS.PointList.NewPointListRow(1.0, 0.0);
          newPoint.SetParentRow(newRow);
          processedXSectDS.PointList.AddPointListRow(newPoint);

          newPoint = processedXSectDS.PointList.NewPointListRow(5.0, 5.0);
          newPoint.SetParentRow(newRow);
          processedXSectDS.PointList.AddPointListRow(newPoint);

        }

        

      }
      catch (Exception ex)
      {
        MessageBox.Show("Error creating new cross-section: " + ex.Message);
      }

    }
   
    private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
    {
      DialogResult result = MessageBox.Show(
        "Delete select cross-section?", "Delete Selected?",
        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

      if (result != DialogResult.OK)
        return;

      SelectedXSectsRow.Delete();

    }

  }
}

