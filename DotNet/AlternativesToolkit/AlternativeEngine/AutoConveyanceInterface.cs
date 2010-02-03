using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Odbc;
using SystemsAnalysis.Modeling.ModelResults;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Modeling.ModelUtils.ResultsExtractor;

namespace SystemsAnalysis.ModelConstruction.AlternativesToolkit
{
  public partial class AutoConveyanceInterface : UserControl
  {
    private string baseModel, alternativeName;
    private bool autoConveyanceMode = false;
    private ConveyanceIterationLog conveyanceLog;
    private string conveyanceLogPath;
    private Color defaultGridBackColor;

    public AutoConveyanceInterface()
    {
      InitializeComponent();
      foreach (Control c in this.Controls)
      {
        c.Enabled = false;
      }
      autoConveyanceMode = false;
      this.ultraDropDownButton1.PopupItem = (Infragistics.Win.IPopupItem)this.ultraPopupControlContainer1;
      defaultGridBackColor = grdConveyancePreview.DisplayLayout.Override.RowAppearance.BackColor;
    }

    private void ultraGridColumnChooser1_Click(object sender, EventArgs e)
    {

    }

    public string BaseModel
    {
      get { return this.baseModel; }
      set
      {
        if (value == null) { this.baseModel = ""; }
        else
        {
          this.baseModel = value.EndsWith("\\") ? value : value + "\\";
        }
        conveyanceLogPath = baseModel + "\\sim\\Conveyance_Log.xml";
      }

    }
    public string AlternativeName
    {
      get { return this.alternativeName; }
      set { this.alternativeName = value; }
    }
    public bool AutoConveyanceMode
    {
      get { return this.autoConveyanceMode; }
    }

    public void EnableAutoConveyanceMode()
    {
      foreach (Control c in this.Controls)
      {
        c.Enabled = true;
      }
      this.autoConveyanceMode = true;

      //Update the alternative configuration file 
      conveyanceLog = new ConveyanceIterationLog();
      if (File.Exists(conveyanceLogPath))
      {
        conveyanceLog.ReadXml(conveyanceLogPath);
      }
      else
      {
        conveyanceLog.WriteXml(conveyanceLogPath);
      }
      this.grdIterationLog.DataSource = conveyanceLog;
      this.grdIterationLog.DataMember = "Iteration";

    }

    public void DisableAutoConveyanceMode()
    {
      foreach (Control c in this.Controls)
      {
        c.Enabled = false;
      }
      this.autoConveyanceMode = false;
      conveyanceBuilder = new ConveyanceBuilder();
      grdConveyancePreview.DataSource = conveyanceBuilder;
      ResetConveyanceGrid();
    }

    private void btnBrowseModelResults_Click(object sender, EventArgs e)
    {
      this.openFileDialog1 = new OpenFileDialog();
      this.openFileDialog1.Filter = "*.out|*.out";
      this.openFileDialog1.DefaultExt = "*.out";
      if (Directory.Exists(baseModel + "sim"))
      {
        this.openFileDialog1.InitialDirectory = baseModel + "sim\\";
      }
      else
      {
        this.openFileDialog1.InitialDirectory = baseModel;
      }

      if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        string modelOutputFile;
        modelOutputFile = this.openFileDialog1.FileName;

        this.cmbModelResultsFile.Text = modelOutputFile;
      }
    }

    private void btnPreviewConveyance_Click(object sender, EventArgs e)
    {
      XPSWMMResults xpResults;

      //engineOutput.AddStatus("Previewing auto-conveyance alternative: file:" + cmbModelResultsFile.Text, SeverityLevel.Info);
      this.Cursor = Cursors.WaitCursor;
      try
      {
        xpResults = new XPSWMMResults(cmbModelResultsFile.Text);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Could not parse output file: '" + ex.Message + "'. Please verify that you\nhave specified a valid XP-SWMM output file.", "Error Parsing Output", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //engineOutput.AddStatus("Error parsing output file:" + cmbModelResultsFile.Text, SeverityLevel.Error);
        //engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
        this.Cursor = Cursors.Default;
        return;
      }
      try
      {
        ResetConveyanceGrid();
        PreviewConveyance(xpResults);

        btnExportConveyance.Enabled = true;
        //engineOutput.AddStatus("Auto-conveyance preview generated succesfully.");
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error previewing output file: " + ex.Message);
        //engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
        //engineOutput.AddStatus("Could not load data for conveyance alternative. See Status Log for details.", SeverityLevel.Error);                
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }

    public void ResetConveyanceGrid()
    {
      try
      {
        grdConveyancePreview.Rows.FilterRow.Cells["IncludeInAlternative"].Value = null;
        grdConveyancePreview.Rows.FilterRow.ApplyFilters();
        grdConveyancePreview.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
        grdConveyancePreview.DisplayLayout.Override.RowAppearance.BackColor = defaultGridBackColor;
        grdConveyancePreview.Refresh();
      }
      catch
      {
      }
    }

    private void btnExportConveyance_Click(object sender, EventArgs e)
    {
      //string baseModel = "";
      //string alternativeName = "";
      int iterationNum = 0;
      StreamWriter outputStream;

      try
      {
        this.Cursor = Cursors.WaitCursor;
        //engineOutput.AddStatus("Attempting to export conveyance iteration...");

        iterationNum = 1;

        ConveyanceIterationLog.IterationRow iterationRow = conveyanceLog.Iteration.AddIterationRow("", "", "");
        iterationNum = iterationRow.IterationNum;

        saveFileDialog1.Title = "Export conveyance alternative as:";
        saveFileDialog1.DefaultExt = "xpx";
        saveFileDialog1.AddExtension = true;
        saveFileDialog1.Filter = "*.xpx|*.xpx";
        saveFileDialog1.InitialDirectory = baseModel + "sim\\";
        saveFileDialog1.FileName = alternativeName + "iteration_" + iterationNum + ".xpx";

        if (saveFileDialog1.ShowDialog() != DialogResult.OK)
        {
          iterationRow.Delete();
          return;
        }
        string outputXPX = saveFileDialog1.FileName;
        string outputXML = Path.GetDirectoryName(outputXPX) + "\\" + Path.GetFileNameWithoutExtension(outputXPX) + ".xml";
        iterationRow.XPXFile = outputXPX;
        iterationRow.XMLFile = outputXML;
        iterationRow.SWMMOutput = cmbModelResultsFile.Text;

        outputStream = File.CreateText(outputXPX);
        foreach (ConveyanceBuilder.PreviewTableRow row in conveyanceBuilder.PreviewTable)
        {
          if (row.IncludeInAlternative == true)
          {
            outputStream.WriteLine("Data Deep \"" + row.SimLinkID + "\" 0 1 " + Math.Round(row.NewDiameter / 12, 3));
          }
        }

        conveyanceBuilder.WriteXml(outputXML);
        conveyanceLog.WriteXml(conveyanceLogPath);
        outputStream.Close();

        //engineOutput.AddStatus("Succesfully exported iteration " + iterationNum + " at file:'" + outputXPX + "'.");
      }
      catch (Exception ex)
      {
        MessageBox.Show("Export error: " + ex.Message);
        //engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
        //engineOutput.AddStatus("Error exporting XPX File. See status log for details.", SeverityLevel.Error);
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void btnMergeConveyance_Click(object sender, EventArgs e)
    {
      int iterationNum;

      try
      {
        this.Cursor = Cursors.WaitCursor;
        //engineOutput.AddStatus("Attempting to merge conveyance iterations...");

        string alternativePath = baseModel + "alternatives\\" + alternativeName + "\\";

        conveyanceBuilder = new ConveyanceBuilder();
        conveyanceBuilder.PreviewTable.BeginLoadData();
        foreach (ConveyanceIterationLog.IterationRow row in conveyanceLog.Iteration)
        {
          iterationNum = row.IterationNum;
          //engineOutput.AddStatus("Merging iteration " + iterationNum + " at: 'file:" + row.XMLFile + "'.");
          ConveyanceBuilder iteration;
          iteration = new ConveyanceBuilder();
          iteration.ReadXml(row.XMLFile);
          foreach (ConveyanceBuilder.PreviewTableRow builderRow in iteration.PreviewTable)
          {
            ConveyanceBuilder.PreviewTableRow existingRow;
            existingRow = conveyanceBuilder.PreviewTable.FindByMdlLinkID(builderRow.MdlLinkID);
            //If the row does not already exist in the merged alternative then create a new row
            if (existingRow == null)
            {
              existingRow = conveyanceBuilder.PreviewTable.NewPreviewTableRow();
              conveyanceBuilder.PreviewTable.AddPreviewTableRow(existingRow);
            }
            //If the row does already exist in the merged alternative, then only overwrite if it contains more up-to-date info
            else if (builderRow.IncludeInAlternative == false)
            {
              continue;
            }
            //Copy data from iteration row to merged row
            foreach (DataColumn col in iteration.PreviewTable.Columns)
            {
              existingRow[col.ColumnName] = builderRow[col.ColumnName];
            }
          }
          conveyanceBuilder.PreviewTable.AcceptChanges();
        }
        conveyanceBuilder.PreviewTable.EndLoadData();
        ResetConveyanceGrid();
        grdConveyancePreview.DataSource = conveyanceBuilder;
        //engineOutput.AddStatus("Succesfully merged " + iterationNum + " conveyance iterations! Next, click 'Generate Alternative'.");
        MessageBox.Show("Conveyance Iterations successfully merged.  Review the merged results and click 'Generate Alternative' to create new alternative containing the merged results.", "Iterations Merged Successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        //engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
        //engineOutput.AddStatus("Error merging conveyance iterations. See Status Log for details.", SeverityLevel.Error);
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void btnClearIterations_Click(object sender, EventArgs e)
    {
      try
      {
        if (MessageBox.Show("This will delete all conveyance iterations.  Do you wish to continue?", "Clear Conveyance Iterations?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
        {
          return;
        }

        this.Cursor = Cursors.WaitCursor;

        //engineOutput.AddStatus("Attempting to reset conveyance iterations...");
        //Update the alternative configuration file            
        ConveyanceIterationLog conveyanceLog = new ConveyanceIterationLog();
        if (File.Exists(baseModel + "\\sim\\Conveyance_Log.xml"))
        {
          conveyanceLog.ReadXml(baseModel + "\\sim\\Conveyance_Log.xml");
          foreach (ConveyanceIterationLog.IterationRow row in conveyanceLog.Iteration)
          {
            //engineOutput.AddStatus("Deleting iteration " + row.IterationNum + " at 'file:" + row.XMLFile + "'.");
            if (File.Exists(row.XMLFile))
            {
              File.Delete(row.XMLFile);
            }
            if (File.Exists(row.XPXFile))
            {
              File.Delete(row.XPXFile);
            }
          }
          //engineOutput.AddStatus("Deleting iteration log: 'file:" + baseModel + "\\sim\\Conveyance_Log.xml'.");
          File.Delete(baseModel + "\\sim\\Conveyance_Log.xml");
        }
        //engineOutput.AddStatus("Succesfully reset conveyance iterations!");
      }
      catch (Exception ex)
      {
        //engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
        //engineOutput.AddStatus("Error reseting conveyance iterations. See Status Log for details.", SeverityLevel.Error);
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void cmbModelResultsFile_ValueChanged(object sender, EventArgs e)
    {
      btnExportConveyance.Enabled = false;

      conveyanceBuilder = null;
      if (File.Exists(cmbModelResultsFile.Text))
      {
        btnPreviewConveyance.Enabled = true;
      }
    }

    private void PreviewConveyance(XPSWMMResults xpResults)
    {
      TableE01DataSet.TableE01DataTable tableE01;
      TableE09DataSet.TableE09DataTable tableE09;
      TableE10DataSet.TableE10DataTable tableE10;
      TableE20DataSet.TableE20DataTable tableE20;

      tableE01 = xpResults.GetTableE01();
      tableE09 = xpResults.GetTableE09();
      tableE10 = xpResults.GetTableE10();
      tableE20 = xpResults.GetTableE20();

      ModelBuilder.RelinkModel(baseModel);

      SystemsAnalysis.Modeling.Model model;
      model = new Model(baseModel);

      double requiredDiameter;
      double slope;

      TableE01DataSet.TableE01Row linkSummary;
      TableE09DataSet.TableE09Row usResults;
      TableE09DataSet.TableE09Row dsResults;
      TableE10DataSet.TableE10Row linkResults;
      TableE20DataSet.TableE20Row usResults2;
      TableE20DataSet.TableE20Row dsResults2;

      conveyanceBuilder = new ConveyanceBuilder();
      conveyanceBuilder.PreviewTable.BeginLoadData();
      Links links = model.ModelLinks;
      foreach (Link l in links.Values)
      {
        linkSummary = tableE01.FindByCondName(l.SimLinkID);
        usResults = tableE09.FindByNodeName(l.USNodeName);
        dsResults = tableE09.FindByNodeName(l.DSNodeName);
        linkResults = tableE10.FindByCondName(l.SimLinkID);
        usResults2 = tableE20.FindByNodeName(l.USNodeName);
        dsResults2 = tableE20.FindByNodeName(l.DSNodeName);

        ConveyanceBuilder.PreviewTableRow newRow;
        newRow = conveyanceBuilder.PreviewTable.NewPreviewTableRow();

        newRow.MdlLinkID = l.LinkID;
        newRow.SimLinkID = l.SimLinkID;
        newRow.USNode = l.USNodeName;
        newRow.DSNode = l.DSNodeName;
        newRow.PipeShape = l.PipeShape;
        newRow.ExistingDiameter = l.Diameter;
        newRow.DSIE = l.DSIE;
        newRow.USIE = l.USIE;
        newRow.Length = l.Length;
        newRow.LinkType = l.LinkType.ToString();
        newRow.IsSpecLink = l.IsSpecLink;

        slope = (l.USIE - l.DSIE) / l.Length;

        newRow.Slope = slope;

        //If the link portion of the base model link does not exist in the output file then skip
        //and keep going. The auto-conveyance preview is a best-effort alternative
        if (linkResults == null || usResults == null || dsResults == null || linkSummary == null)
        {
          //newRow.QQRatio = 0;
          //newRow.NewDiameter = l.Diameter;
          //newRow.RequiredDiameter = 0;
          newRow.IncludeInAlternative = false;
          conveyanceBuilder.PreviewTable.AddPreviewTableRow(newRow);
          continue;
        }

        double calcedDesignQ;
        calcedDesignQ = ((1.49 / .013) * Math.PI * Math.Pow(linkSummary.Depth / (2.0), 2.0) * Math.Pow(linkSummary.MaxWidth / (2.0 * 2), 2.0 / 3.0) * Math.Pow(slope, 0.5));
        newRow.QQRatio = linkResults.MaxQ / calcedDesignQ;
        newRow.USSurcharge = Math.Max(usResults.MaxJElev - (l.USIE + linkSummary.Depth), 0);
        newRow.DSSurcharge = Math.Max(dsResults.MaxJElev - (l.DSIE + linkSummary.Depth), 0);

        //The pipe requires upsizing...
        if (newRow.QQRatio > (double)qqRatio.Value &&
            (newRow.USSurcharge > 0 || newRow.DSSurcharge > 0) &&
             slope > 0)
        {
          newRow.ModeledDiameter = Math.Round(linkSummary.MaxWidth * 12, 2);
          if (l.PipeShape == "CIRC")
          {
            requiredDiameter = Math.Pow(0.02792 * linkResults.MaxQ / Math.Pow(slope, 0.5), 0.375) * 12;
            requiredDiameter = Math.Round(requiredDiameter, 1);
            newRow.RequiredDiameter = requiredDiameter;

            if (requiredDiameter <= newRow.ModeledDiameter)
            {
              newRow.NewDiameter = newRow.ModeledDiameter;
              newRow.IncludeInAlternative = false;
            }
            else
            {
              newRow.NewDiameter = (ValidPipeDiameter(requiredDiameter));
              newRow.IncludeInAlternative = true;
            }
          }
          else
          {
            newRow.QQRatio = 0;
            newRow.NewDiameter = newRow.ModeledDiameter;
            newRow.RequiredDiameter = 0;
            newRow.IncludeInAlternative = false;
          }

          newRow.MaxQ = linkResults.MaxQ;
          newRow.SurchargeTime = Math.Max(dsResults2.SurchargeTime, usResults2.SurchargeTime);
          newRow.USFreeboard = usResults.Freeboard;
          newRow.DSFreeboard = dsResults.Freeboard;

        }
        //The pipe does not require upsizing
        else
        {
          newRow.ModeledDiameter = Math.Round(linkSummary.MaxWidth * 12, 2);
          if (l.PipeShape != "CIRC" || slope <= 0)
          {
            newRow.QQRatio = 0;
          }


          newRow.MaxQ = linkResults.MaxQ;
          newRow.SurchargeTime = Math.Max(dsResults2.SurchargeTime, usResults2.SurchargeTime);
          newRow.USFreeboard = usResults.Freeboard;
          newRow.DSFreeboard = dsResults.Freeboard;

          newRow.RequiredDiameter = newRow.ModeledDiameter;
          newRow.NewDiameter = newRow.ModeledDiameter;
          newRow.IncludeInAlternative = false;
        }

        conveyanceBuilder.PreviewTable.AddPreviewTableRow(newRow);
      }
      conveyanceBuilder.PreviewTable.EndLoadData();
      grdConveyancePreview.DataSource = conveyanceBuilder;
    }

    public void AutoConveyance(string baseModel, string alternativeName)
    {
      string altLinksDB;

      this.Cursor = Cursors.WaitCursor;

      try
      {
        altLinksDB = baseModel + "alternatives\\" + alternativeName + "\\alternative_package.mdb";
        OdbcConnection conn;
        conn = new OdbcConnection(@"Dsn=MS Access Database;dbq=" + altLinksDB + ";driverid=25;fil=MS Access;maxbuffersize=2048;pagetimeout=5");

        OdbcCommand command = new OdbcCommand();
        command.Connection = conn;
        conn.Open();

        int altLinkID = 1;

        foreach (ConveyanceBuilder.PreviewTableRow r in conveyanceBuilder.PreviewTable)
        {
          if (!r.IncludeInAlternative)
          {
            continue;
          }
          command.CommandText = "Insert Into alt_links_ac (AltLinkID, MdlLinkID," +
              "USNode, DSNode, Operation, LinkType, DiamWidth, Height, USIE, DSIE," +
              "PipeShape, Material, Length, IsSpecLink) " +
              "Values (" + altLinkID + "," + r.MdlLinkID + ",'" + r.USNode +
              "','" + r.DSNode + "','UPD', '" + r.LinkType + "'," + r.NewDiameter + ",0," + r.USIE +
              "," + r.DSIE + ",'CIRC','CSP'," + r.Length + "," + r.IsSpecLink + ")";
          command.ExecuteNonQuery();
          altLinkID++;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        //this.engineOutput.AddStatus("Error generating conveyance alternative!: " + ex.ToString(), SeverityLevel.Error);
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private double ValidPipeDiameter(double requiredDiameter)
    {
      if (requiredDiameter <= 6) return 6;
      if (requiredDiameter <= 8) return 8;
      if (requiredDiameter <= 10) return 10;
      if (requiredDiameter <= 24) return Math.Ceiling(requiredDiameter / 3) * 3;
      return requiredDiameter = Math.Ceiling(requiredDiameter / 6) * 6;
    }

    private void grdIterationLog_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
    {
      try
      {
        ConveyanceIterationLog.IterationRow row;
        string xmlFile;
        xmlFile = (string)grdIterationLog.Selected.Rows[0].Cells["XMLFile"].Text;
        conveyanceBuilder = new ConveyanceBuilder();
        conveyanceBuilder.ReadXml(xmlFile);

        grdConveyancePreview.DataSource = conveyanceBuilder;
        grdConveyancePreview.Rows.FilterRow.Cells["IncludeInAlternative"].Value = true;
        grdConveyancePreview.Rows.FilterRow.ApplyFilters();
        grdConveyancePreview.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
        grdConveyancePreview.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(163, 163, 163);
        btnExportConveyance.Enabled = false;
      }
      catch
      {
      }

    }

    private void btnDeleteIteration_Click(object sender, EventArgs e)
    {
      try
      {
        Infragistics.Win.UltraWinGrid.UltraGridRow gridRow;
        gridRow = grdIterationLog.Selected.Rows[0];
        if (gridRow == null)
        {
          MessageBox.Show("No iteration selected.", "Cannot delete iteration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        if (MessageBox.Show("Delete selected iteration?", "Confirm delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
        {
          return;
        }

        string xmlFile;
        string xpxFile;
        xmlFile = gridRow.Cells["XMLFile"].Text;
        xpxFile = gridRow.Cells["XPXFile"].Text;
        File.Delete(xmlFile);
        File.Delete(xpxFile);
        foreach (ConveyanceIterationLog.IterationRow row in conveyanceLog.Iteration)
        {
          if (row.IterationNum.ToString() == gridRow.Cells["IterationNum"].Text)
          {
            row.Delete();
            conveyanceLog.WriteXml(conveyanceLogPath);
            break;
          }
        }
      }
      catch
      {
      }
    }
  }
}
