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

namespace SystemsAnalysis.EMGAATS.CrossSectionEditor
{
  public partial class frmXSectEditor : Form
  {
    private bool setLeftStation, setRightStation, validXml = false;

    public frmXSectEditor()
    {
      InitializeComponent();
      chrtXSectDisplay.DataBindings.Clear();
      chrtXSectDisplay.InvalidDataReceived += new Infragistics.UltraChart.Shared.Events.ChartDataInvalidEventHandler(chrtXSectDisplay_InvalidDataReceived);
    }

    void chrtXSectDisplay_InvalidDataReceived(object sender, Infragistics.UltraChart.Shared.Events.ChartDataInvalidEventArgs e)
    {
      e.Text = "Load a valid LandXml-1.2 file";
      e.LabelStyle.FontColor = Color.Honeydew;
      e.LabelStyle.FontSizeBestFit = false;
      e.LabelStyle.HorizontalAlign = StringAlignment.Center;
      e.LabelStyle.Font = new Font("Verdana", 12);
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

    private void ValidateRawXml(string rawXml)
    {
      chrtXSectDisplay.EmptyChartText = "Validating xml...";
      validXml = true;

      Stream xsdStream = System.Reflection.Assembly.
                      GetExecutingAssembly().GetManifestResourceStream("SystemsAnalysis.EMGAATS.CrossSectionEditor.LandXML-1.2.xsd");

      XmlSchema rawSchema;
      rawSchema = XmlSchema.Read(xsdStream, null);

      XmlSchemaSet schemaSet = new XmlSchemaSet();
      schemaSet.Add(rawSchema);

      XmlReaderSettings xmlSettings = new XmlReaderSettings();
      xmlSettings.ValidationType = ValidationType.Schema;

      xmlSettings.Schemas = schemaSet;
      xmlSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBacKHandler);

      XmlReader reader = XmlReader.Create(openFileDialog.FileName, xmlSettings);

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
    }

    private void LoadXSect(string xSectName)
    {
      processedXSectDS.ChartTable.Clear();

      ProcessedXSectDataSet.XSectsRow xSectRow
        = processedXSectDS.XSects.FindByXSectName(xSectName);

      double minElev = Double.MaxValue;
      double maxElev = Double.MinValue;

      foreach (ProcessedXSectDataSet.PointListRow pointRow in xSectRow.GetPointListRows())
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

    void ValidationCallBacKHandler(object sender, ValidationEventArgs e)
    {
      chrtXSectDisplay.EmptyChartText += "\nError: " + e.Message;
      validXml = false;
    }

    private void frmXSectEditor_Load(object sender, EventArgs e)
    {

    }

    private void txtXSectFileBrowseButton_Clicked(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
    {
      chrtXSectDisplay.DataBindings.Clear();
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;

      txtXSectFile.Text = openFileDialog.FileName;

      processedXSectDS.Clear();
      ValidateRawXml(openFileDialog.FileName);
      LoadXSect(processedXSectDS.XSects[0].XSectName);

      ConfigureChart();
    }

    private void bindingSource1_CurrentChanged(object sender, EventArgs e)
    {

      ProcessedXSectDataSet.XSectsRow xSectsRow = SelectedXSectsRow;
      LoadXSect(xSectsRow.XSectName);
    }

    private ProcessedXSectDataSet.XSectsRow SelectedXSectsRow
    {
      get
      {
        return (ProcessedXSectDataSet.XSectsRow)((System.Data.DataRowView)bindingSource1.Current).Row;
      }
    }

    private void chrtXSectDisplay_ChartDataClicked(object sender, Infragistics.UltraChart.Shared.Events.ChartDataEventArgs e)
    {
      ProcessedXSectDataSet.XSectsRow xSectRow = SelectedXSectsRow;
      if (setLeftStation)
      {
        if (e.DataValue < xSectRow.RightOverbankStation)
          xSectRow.LeftOverbankStation = e.DataValue;
        else
          MessageBox.Show("Left station must be on left side of channel.");
      }


      if (setRightStation)
      {
        if (e.DataValue > xSectRow.LeftOverbankStation)
          xSectRow.RightOverbankStation = e.DataValue;
        else
          MessageBox.Show("Right station must be on right side of channel.");
      }

      setLeftStation = false;
      setRightStation = false;
      this.LoadXSect(xSectRow.XSectName);
      Cursor = Cursors.Default;
    }

    private void txtLeftStation_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
    {
      setLeftStation = !setLeftStation;
      setRightStation = false;
      if (!setLeftStation)
      {
        Cursor = Cursors.Default;
        return;
      }
      Cursor = Cursors.Cross;

    }

    private void txtRightStation_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
    {
      setRightStation = !setRightStation;
      setLeftStation = false;
      if (!setRightStation)
      {
        Cursor = Cursors.Default;
        return;
      }
      Cursor = Cursors.Cross;


    }

    private void optStationOrder_ValueChanged(object sender, EventArgs e)
    {
      SwapLeftAndRight();
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

    private void chkReadyForExport_CheckedValueChanged(object sender, EventArgs e)
    {
      if (chkReadyForExport.Checked)
      {
        //bindingSource1.MoveNext();
      }
    }

    private void bindingSource1_BindingComplete(object sender, BindingCompleteEventArgs e)
    {
      if (e.BindingCompleteContext ==
        BindingCompleteContext.DataSourceUpdate && e.Exception == null)

        e.Binding.BindingManagerBase.EndCurrentEdit();
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
      }

      return;
    }

    private void SaveProcessedData()
    {
    }

    private void LoadProcessedData()
    {
    }

    private void ExportToMaster()
    {
    }


  }
}

