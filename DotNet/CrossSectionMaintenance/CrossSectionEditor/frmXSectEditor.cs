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
    private bool validXml;

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

    private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
    {

    }

    private void ultraButton1_Click(object sender, EventArgs e)
    {
      chrtXSectDisplay.DataBindings.Clear();
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;

      processedXSectDS.Clear();
      ValidateRawXml(openFileDialog.FileName);
      LoadXSect(0);

      ConfigureChart();

    }

    private void ConfigureChart()
    {
      chrtXSectDisplay.DataSource = processedXSectDS.ChartTable;
      chrtXSectDisplay.ScatterChart.ColumnX = 1;
      chrtXSectDisplay.ScatterChart.ColumnY = 2;
      chrtXSectDisplay.ScatterChart.GroupByColumn = 0;
      chrtXSectDisplay.ScatterChart.UseGroupByColumn = true;
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

        for (int i = 0; i < pointList.Length;  i += 2)
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

      reader.Close();
    }

    private void LoadXSect(int index)
    {
      processedXSectDS.ChartTable.Clear();
      ProcessedXSectDataSet.XSectsRow xSectRow 
        = processedXSectDS.XSects[index];
      
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
    }

    void ValidationCallBacKHandler(object sender, ValidationEventArgs e)
    {
      chrtXSectDisplay.EmptyChartText += "\nError: " + e.Message;
      validXml = false;
    }

    private void frmXSectEditor_Load(object sender, EventArgs e)
    {

    }




  }
}

