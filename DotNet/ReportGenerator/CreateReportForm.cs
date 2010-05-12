using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Utility;
using SystemsAnalysis;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Modeling.ModelResults;
using SystemsAnalysis.Utils.Events;
using SystemsAnalysis.Reporting;
using SystemsAnalysis.Reporting.ReportLibraries;
using System.Xml;

namespace SystemsAnalysis.Reporting
{
  /// <summary>
  /// User interface for generating characterization reports
  /// </summary>
  public partial class CreateReportForm : Form
  {
    private DataAccess.SAMasterDataSet saMasterDS;

    //Map Control commands
    private SystemsAnalysis.Utils.ArcObjects.MapControl.SelectFeatures selectCommand;
    private SystemsAnalysis.Utils.ArcObjects.MapControl.ClearFeatureSelection clearCommand;
    private SystemsAnalysis.Utils.ArcObjects.MapControl.Pan panCommand;
    private SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomIn zoomInCommand;
    private SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomOut zoomOutCommand;
    private SystemsAnalysis.Tracer.SelectStartLinkTool selectStartLinkTool;
    private SystemsAnalysis.Tracer.SelectStopLinkTool selectStopLinkTool;
    private SystemsAnalysis.Tracer.TracerExtension tracerExtension;

    private Links _mstLinks;
    private Links startLinks;
    private Links stopLinks;

    private CharacterizationEngine charEngine;

    private struct CharacterizationData
    {
      public string studyArea;
      public string reportOutput;
      public string outputFile;
      public string reportTemplate;
      public Dictionary<string, ReportBase.Parameter> auxilaryData;
    }

    /// <summary>
    /// Acccessor property for lazy loaded object _mstLinks
    /// </summary>
    private Links MstLinks
    {
      get
      {
        if (_mstLinks == null)
        {
          InitMstLinks();
        }
        return _mstLinks;
      }
    }
    /// <summary>
    /// Lazy loader for _mstLinks
    /// </summary>
    private void InitMstLinks()
    {
      this.OnStatusChanged(new StatusChangedArgs("Preparing master links preview."));
      try
      {
        saMasterDS = new DataAccess.SAMasterDataSet();

        DataAccess.SAMasterDataSetTableAdapters.MstLinksTableAdapter mstLinksTA;
        mstLinksTA = new SystemsAnalysis.DataAccess.SAMasterDataSetTableAdapters.MstLinksTableAdapter();

        mstLinksTA.Fill(saMasterDS.MstLinks);

        _mstLinks = new Links(saMasterDS.MstLinks);
      }
      catch (Exception ex)
      {
        this.OnStatusChanged(new StatusChangedArgs("Could not load master links preview", StatusChangeType.Error));
        this.OnStatusChanged(new StatusChangedArgs(ex.Message, StatusChangeType.Error));
        _mstLinks = new Links();
      }

      this.OnStatusChanged(new StatusChangedArgs("Finished loading master data.", StatusChangeType.Info));
    }

    /// <summary>
    /// Constructor for characterization user interface
    /// </summary>
    public CreateReportForm()
    {
      //
      // Required for Windows Form Designer support
      //            
      InitializeComponent();
    }

    public void InitializeForm()
    {
      Cursor = Cursors.WaitCursor;
      try
      {
        LoadReportList();
        LoadReport(Convert.ToString(cmbReportChooser.Value));
        SetupMapControl();

        InitializeLinkDisplayBoxes();
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }

    private void LoadReportList()
    {
      string[] reportPathList;

      reportPathList = Directory.GetFiles(Application.StartupPath + "\\xml\\", "*.xml", SearchOption.TopDirectoryOnly);

      for (int i = 0; i < reportPathList.Length; i++)
      {
        AddReportToList(reportPathList[i]);
      }
      cmbReportChooser.SelectedRow = cmbReportChooser.Rows[cmbReportChooser.Rows.Count - 1];
      cmbReportChooser.DisplayLayout.Bands[0].Columns[1].Hidden = true;
    }
    private void AddReportToList(string reportPath)
    {
      System.Xml.XmlTextReader xmlReader;
      xmlReader = new System.Xml.XmlTextReader(reportPath);
      xmlReader.ReadToFollowing("ReportGenerator");
      string reportName;
      reportName = xmlReader.GetAttribute("reportName");
      DataRow row;
      row = reportTable.Tables[0].NewRow();
      row["ReportName"] = reportName;
      row["ReportPath"] = reportPath;
      reportTable.Tables[0].Rows.Add(row);
    }

    private void SetupMapControl()
    {
      selectCommand =
          new SystemsAnalysis.Utils.ArcObjects.MapControl.SelectFeatures();
      selectCommand.OnCreate(axMapControl.Object);
      axMapControl.CurrentTool = selectCommand;
      clearCommand =
          new SystemsAnalysis.Utils.ArcObjects.MapControl.ClearFeatureSelection();
      clearCommand.OnCreate(axMapControl.Object);
      panCommand =
          new SystemsAnalysis.Utils.ArcObjects.MapControl.Pan();
      panCommand.OnCreate(axMapControl.Object);
      zoomInCommand =
          new SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomIn();
      zoomInCommand.OnCreate(axMapControl.Object);
      zoomOutCommand =
          new SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomOut();
      zoomOutCommand.OnCreate(axMapControl.Object);
      selectStartLinkTool =
          new SystemsAnalysis.Tracer.SelectStartLinkTool();
      selectStartLinkTool.OnCreate(axMapControl.Object);
      selectStopLinkTool =
          new SystemsAnalysis.Tracer.SelectStopLinkTool();
      selectStopLinkTool.OnCreate(axMapControl.Object);
      tracerExtension = new SystemsAnalysis.Tracer.TracerExtension();

      this.selectStopLinkTool.MouseDown += new SystemsAnalysis.Tracer.OnMouseDownEventHandler(CallTracerExtension);
      this.selectStartLinkTool.MouseDown += new SystemsAnalysis.Tracer.OnMouseDownEventHandler(CallTracerExtension);

      try
      {
        IFeatureLayer pFL;
        pFL = (FeatureLayer)LoadLayerFile(Application.StartupPath + "\\lyr\\sewer_basins.lyr", axMapControl);
        pFL.Selectable = false;

        ESRI.ArcGIS.Geometry.Point point;
        point = new ESRI.ArcGIS.Geometry.PointClass();
        point.X = 7643708.753;
        point.Y = 681634.261;
        axMapControl.CenterAt(point);
        axMapControl.MapScale = 10000.0;

        //LoadLayerFile(Application.StartupPath + "\\lyr\\cgis_streets.lyr", axMapControl);                                
        pFL = (FeatureLayer)LoadLayerFile(Application.StartupPath + "\\lyr\\mst_links.lyr", axMapControl);
        tracerExtension.SetupTracer(MstLinks.LinkNetwork, pFL, "USNode", "DSNode", axMapControl.Map);
        pFL.Selectable = true;

        pFL = (FeatureLayer)LoadLayerFile(Application.StartupPath + "\\lyr\\mst_nodes.lyr", axMapControl);
        pFL.Selectable = false;
        tabControlOutputDisplay.SelectedTab = tabMapView.Tab;
      }
      catch (Exception ex)
      {
        this.OnStatusChanged(new StatusChangedArgs("Could not display preview map.", StatusChangeType.Error));
        this.OnStatusChanged(new StatusChangedArgs(ex.Message, StatusChangeType.Error));
      }
    }
    private void InitializeLinkDisplayBoxes()
    {
      startLinks = new Links();
      startLinks.OnAddedLink +=
          new OnAddLinkEventHandler(this.startLinks_OnAddedLink);
      startLinks.OnRemovedLink +=
          new OnRemoveLinkEventHandler(this.startLinks_OnRemovedLink);
      stopLinks = new Links();
      stopLinks.OnAddedLink +=
          new OnAddLinkEventHandler(this.stopLinks_OnAddedLink);
      stopLinks.OnRemovedLink +=
          new OnRemoveLinkEventHandler(this.stopLinks_OnRemovedLink);
      lstStartLinks.DisplayMember = "MLinkID";
      lstStopLinks.DisplayMember = "MLinkID";
    }

    private void LoadReport(string report)
    {
      if (File.Exists(report))
      {
        try
        {
          object blah = new object();
          axWebBrowser.Navigate(report, ref blah, ref blah, ref blah, ref blah);
          this.tabControlOutputDisplay.SelectedTab = tabCharacterizationView.Tab;
        }
        catch (Exception ex)
        {
          this.OnStatusChanged(new StatusChangedArgs(ex.Message, StatusChangeType.Error));
        }
      }
    }

    private void btnCharacterize_Click(object sender, System.EventArgs e)
    {
      string outputFile;
      string studyArea;
      if (backgroundWorker1.IsBusy)
      {
        if (MessageBox.Show("Report Generator is busy... Cancel?", "Cancel Report Generator?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
          return;
        }
        backgroundWorker1.CancelAsync();
        return;
      }
      try
      {
        studyArea = this.txtStudyAreaName.Text;

        if (studyArea == null || studyArea.Length <= 0)
        {
          MessageBox.Show("You must enter a Study Area Name.", "No Study Area Name Specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        this.OnStatusChanged(new StatusChangedArgs("Characterizing study area '" + studyArea + "'."));
        saveFileDialog.DefaultExt = "xml";
        if (studyArea.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) == -1)
        {
          saveFileDialog.FileName = studyArea + ".xml";
        }
        saveFileDialog.Filter = "*.xml|*.xml";
        if (saveFileDialog.ShowDialog() != DialogResult.OK)
        {
          return;
        }
        outputFile = saveFileDialog.FileName;
      }
      catch (Exception ex)
      {
        this.OnStatusChanged(new StatusChangedArgs(ex.Message, StatusChangeType.Error));
        return;
      }

      try
      {
        ExecuteCharacterization(outputFile, studyArea, true);
      }
      catch (Exception ex)
      {
        this.OnStatusChanged(new StatusChangedArgs(ex.Message, StatusChangeType.Error));
        return;
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }

    private void ExecuteCharacterization(string outputFile, string studyArea, bool asynch)
    {
      CharacterizationData charData = new CharacterizationData();

      charData.studyArea = studyArea;
      charData.outputFile = outputFile;
      charData.reportTemplate = (string)cmbReportChooser.Value;
      charData.auxilaryData = new Dictionary<string, ReportBase.Parameter>();
      foreach (ReportBase.ReportInfo reportInfo in charEngine.ReportInfos.Values)
      {
        if (!reportInfo.RequiresAuxilaryData)
        {
          continue;
        }
        string library = reportInfo.ReportName;

        foreach (string auxilaryDataName in reportInfo.AuxilaryDataDescription.Keys)
        {
          Control c;
          c = reportOptionsLayoutPanel.Controls[library + "." + auxilaryDataName];
          if (c == null)
          {
            continue;
          }
          charData.auxilaryData.Add(auxilaryDataName, new ReportBase.Parameter(auxilaryDataName, c.Text));
        }
      }

      if (asynch)
      {
        pnlCancelBackgroundWorker.Visible = true;
        backgroundWorker1.RunWorkerAsync(charData);
      }
      else
      {
        executeSync(charData);
      }
      return;
    }

    private void executeSync(CharacterizationData charData)
    {
      string studyArea = charData.studyArea;
      string outputFile = charData.outputFile;
      string reportTemplate = charData.reportTemplate;
      Links charLinks;

      try
      {
        this.OnStatusChanged(new StatusChangedArgs("Performing trace."));

        charLinks = MstLinks.Trace(this.startLinks, this.stopLinks);
        if (charLinks == null || charLinks.Count <= 0)
        {
          throw new Exception("Error: No links were selected. Please verify start links.");
        }

        this.OnStatusChanged(new StatusChangedArgs("Traced " + charLinks.Count + " links."));

        this.OnStatusChanged(new StatusChangedArgs("Creating characterization engine."));

        charEngine = new CharacterizationEngine(reportTemplate);
        charEngine.StatusChanged += new Utils.Events.OnStatusChangedEventHandler(OnStatusChanged);

        foreach (ReportBase.ReportInfo reportInfo in charEngine.ReportInfos.Values)
        {
          if (!reportInfo.RequiresAuxilaryData)
          {
            continue;
          }
          string library = reportInfo.ReportName;
          foreach (string auxilaryDataName in reportInfo.AuxilaryDataDescription.Keys)
          {
            reportInfo.AuxilaryData.Add(auxilaryDataName, charData.auxilaryData[auxilaryDataName]);
          }
        }
        charEngine.Characterize(charLinks, outputFile, studyArea, null, null);
        writeMetaData(outputFile, charLinks.Count);

      }
      catch (Exception ex)
      {
        this.OnStatusChanged(new StatusChangedArgs("Batch execute failed", StatusChangeType.Error));
        throw new Exception("Batch execute failed" + ex.Message, ex);
      }
    }

    private void writeMetaData(string outputFile, int linkCount)
    {
      XmlDocument xmlMetaData = new XmlDocument();
      
      string metaData;
      metaData = @"<GeneratedBy>" + System.Environment.UserName + "</GeneratedBy>";
      metaData += "<Date>" + System.DateTime.Now.ToString("MM/dd/yy hh:mm tt") + "</Date>";
      metaData += @"<StudyAreaSummary><StartLinks>";
      foreach (Link l in startLinks.Values)
      {
        metaData += @"<StartLink>" + l.MLinkID + "</StartLink>";
      }
      metaData += @"</StartLinks><StopLinks>";
      foreach (Link l in stopLinks.Values)
      {
        metaData += @"<StopLink>" + l.MLinkID + "</StopLink>";
      }
      metaData += @"</StopLinks><LinkCount>" + linkCount + "</LinkCount></StudyAreaSummary>";

      metaData += @"<AuxilaryData>";
      foreach (ReportBase.ReportInfo ri in charEngine.ReportInfos.Values)
      {
        metaData += "<ReportLibrary>";
        metaData += @"<ReportName>" + ri.ReportName + "</ReportName>";
        foreach (ReportBase.Parameter parameter in ri.AuxilaryData.Values)
        {
          metaData += "<Parameter>";
          metaData += "<ParameterName>" + parameter.Name + "</ParameterName>";
          metaData += "<ParameterValue>" + parameter.Value + "</ParameterValue>";
          metaData += "</Parameter>";
        }        
        metaData += "</ReportLibrary>";
      }
      metaData += @"</AuxilaryData>";
     
      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.Load(outputFile);                 
      XmlNode root = xmlDoc.DocumentElement;
      //Create a new node.
      XmlElement elem = xmlDoc.CreateElement("MetaData");
      elem.InnerXml = metaData;
      //Add the node to the document.
      root.AppendChild(elem);            
      xmlDoc.Save(outputFile);
      
    }

    private void btnPreviewTrace_Click(object sender, System.EventArgs e)
    {
      TraceNetwork();
    }

    private void TraceNetwork()
    {
      try
      {
        MstLinks.ClearSubNetwork();
        MstLinks.SelectSubNetwork(startLinks, stopLinks);

        IFeatureLayer pFeatLayer;
        pFeatLayer = (IFeatureLayer)axMapControl.get_Layer(1);

        int[] selectedEdgeIDArray = MstLinks.GetSelectedEdgeIDArray();
        Utils.ArcObjects.MapControl.MapControlHelper.ZoomToOIDArray(
            axMapControl, pFeatLayer, selectedEdgeIDArray);
        tabControlOutputDisplay.SelectedTab = tabMapView.Tab;
        this.OnStatusChanged(new StatusChangedArgs("Succesfully previewed Network Trace. Found " + selectedEdgeIDArray.Length + " links.", StatusChangeType.Info));
      }
      catch (Exception ex)
      {
        this.OnStatusChanged(new StatusChangedArgs(ex.Message, StatusChangeType.Error));
      }
    }

    private void btnClearList_Click(object sender, System.EventArgs e)
    {
      MstLinks.ClearSubNetwork();
      startLinks.Clear();
      stopLinks.Clear();
      IFeatureLayer pFeatLayer;
      pFeatLayer = (IFeatureLayer)axMapControl.get_Layer(0);
      Utils.ArcObjects.MapControl.MapControlHelper.ClearSelection(
        axMapControl, pFeatLayer);

    }

    #region Map panel command button actions

    private void btnSelectFeatures_Click(object sender, System.EventArgs e)
    {
      axMapControl.CurrentTool = selectCommand;
    }
    private void btnClearSelection_Click(object sender, System.EventArgs e)
    {
      clearCommand.OnClick();
    }
    private void btnZoomIn_Click(object sender, System.EventArgs e)
    {
      axMapControl.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
      axMapControl.CurrentTool = zoomInCommand;
    }
    private void btnZoomOut_Click(object sender, System.EventArgs e)
    {
      axMapControl.MousePointer = esriControlsMousePointer.esriPointerZoomOut;
      axMapControl.CurrentTool = zoomOutCommand;
    }
    private void btnPan_Click(object sender, System.EventArgs e)
    {
      axMapControl.MousePointer = esriControlsMousePointer.esriPointerPan;
      axMapControl.CurrentTool = panCommand;
    }
    private void btnSelectStartLink_Click(object sender, EventArgs e)
    {
      axMapControl.CurrentTool = selectStartLinkTool;
    }

    private void btnSelectStopLink_Click(object sender, EventArgs e)
    {
      axMapControl.CurrentTool = selectStopLinkTool;
    }
    private void CallTracerExtension(object sender, SystemsAnalysis.Tracer.ClickEventArgs e)
    {
      try
      {
        SystemsAnalysis.Tracer.IGraphEdge edge;
        edge = tracerExtension.SelectLinkExternal(sender, e);
        if (sender.GetType().ToString() == "SystemsAnalysis.Tracer.SelectStartLinkTool")
        {
          Link l = MstLinks[edge.EdgeID];
          startLinks.Add(l);
        }
        else if (sender.GetType().ToString() == "SystemsAnalysis.Tracer.SelectStopLinkTool")
        {
          Link l = MstLinks[edge.EdgeID];
          stopLinks.Add(l);
        }
        TraceNetwork();
      }
      catch (Exception ex)
      {
        OnStatusChanged(new StatusChangedArgs("Could not select link: " + ex.Message));
      }

    }
    #endregion

    #region Methods to manage start link and stop link list boxes
    private void startLinks_OnAddedLink(object sender, Links.LinkChangedEventArgs e)
    {
      Link addedLink = (Link)e.ChangedLink;
      lstStartLinks.Items.Add(addedLink);
    }
    private void startLinks_OnRemovedLink(object sender, Links.LinkChangedEventArgs e)
    {
      Link removedLink = (Link)e.ChangedLink;
      lstStartLinks.Items.Remove(removedLink);
    }
    private void stopLinks_OnAddedLink(object sender, Links.LinkChangedEventArgs e)
    {
      Link addedLink = (Link)e.ChangedLink;
      lstStopLinks.Items.Add(addedLink);
    }
    private void stopLinks_OnRemovedLink(object sender, Links.LinkChangedEventArgs e)
    {
      Link removedLink = (Link)e.ChangedLink;
      lstStopLinks.Items.Remove(removedLink);
    }
    private void btnRemoveStartLink_Click(object sender, System.EventArgs e)
    {
      while (lstStartLinks.SelectedItems.Count != 0)
      {
        Link linkToRemove = (Link)lstStartLinks.SelectedItems[0];
        startLinks.Remove(linkToRemove.LinkID);
      }
    }
    private void btnAddStartLink_Click(object sender, System.EventArgs e)
    {
      InputBoxResult result;
      result = InputBox.Show("Enter an MLinkID:", "Add StartLink", "", null, -1, -1);
      int mLinkID;
      if (result.OK)
      {
        try
        {
          mLinkID = Int32.Parse(result.Text);
          Link l = MstLinks.FindByMLinkID(mLinkID);
          if (l != null)
          {
            startLinks.Add(l);
          }
          else
          {
            MessageBox.Show("Could not find MLinkID in mstLinks.", "MLinkID Not Found");
          }
        }
        catch
        { MessageBox.Show("You must enter a valid MLinkID.", "Invalid Input"); }
      }
    }
    private void btnRemoveStopLink_Click(object sender, System.EventArgs e)
    {
      //Link linkToRemove = (Link)lstStopLinks.SelectedItem;
      while (lstStopLinks.SelectedItems.Count != 0)
      {
        Link linkToRemove = (Link)lstStopLinks.SelectedItems[0];
        stopLinks.Remove(linkToRemove.LinkID);
      }
    }
    private void btnAddStopLink_Click(object sender, System.EventArgs e)
    {
      InputBoxResult result;
      result = InputBox.Show("Enter an MLinkID:", "Add StopLink", "", null, -1, -1);
      int mLinkID;
      if (result.OK)
      {
        try
        {
          mLinkID = Int32.Parse(result.Text);
          Link l = MstLinks.FindByMLinkID(mLinkID);
          if (l != null)
          {
            stopLinks.Add(l);
          }
          else
          {
            MessageBox.Show("Could not find MLinkID in mstLinks.", "MLinkID Not Found");
          }
        }
        catch
        { MessageBox.Show("You must enter a valid MLinkID.", "Invalid Input"); }
      }

    }
    #endregion

    private void btnReconcileModel_Click(object sender, System.EventArgs e)
    {
      /*this.tabControlOutputDisplay.SelectedTab = this.tabStatusView.Tab;	
      this.OnStatusChanged(new StatusChangedArgs("Attempting to reconcile model to master data.");
      if (this.selectedModel == null)
      {				                
       *    this.OnStatusChanged(new StatusChangedArgs("No model selected", StatusChangeType.Error);
      }			
      Links modelLinks = this.selectedModel.ModelLinks;
      Nodes modelNodes = this.selectedModel.ModelNodes;
      DSCs modelDSCs = this.selectedModel.ModelDSCs;

      Links reconcileStartLinks = new Links();
      Links reconcileStopLinks = new Links();
      #region Translate model start/stop links to master start/stop links			
      foreach (Link l in selectedModel.StartLinks.Values)
      {
          Link startLink;
          startLink = mstLinks.FindByMLinkID(l.MLinkID);
          if (startLink != null)
          {
              reconcileStartLinks.Add(startLink);
          }
          else
          {                    
       *    this.OnStatusChanged(new StatusChangedArgs("Start link missing from master database. MLinkID = " + l.MLinkID, StatusChangeType.Warning);
          }
          //TODO: Log missing start link
      }								
      foreach (Link l in selectedModel.StopLinks.Values)
      {
          Link stopLink;
          stopLink = mstLinks.FindByMLinkID(l.MLinkID);
          if (stopLink != null)
          {
              reconcileStopLinks.Add(stopLink);
          }
          else
          {
              //this.AddWarningStatus(
                  "Stop link missing from master database. MLinkID = " + l.MLinkID);
          }
          //TODO: Log missing stop link				
      }
      #endregion

      this.mstLinks.ClearSubNetwork();			
      this.OnStatusChanged(new StatusChangedArgs("Performing trace.");
      int traceCount = mstLinks.SelectSubNetwork(
          reconcileStartLinks, reconcileStopLinks);
      if (traceCount <= 0)
      {
          throw new Exception("Error: No links were selected. Please verify start links.");								
      }
      this.OnStatusChanged(new StatusChangedArgs("Traced " + traceCount + " links.");
      Links charLinks = mstLinks.GetSubNetwork();
      Nodes charNodes = new Nodes();
      charNodes.Load(charLinks, settings.DataSource.FindByName("AGMasterDB").DataSource_Text);
      this.OnStatusChanged(new StatusChangedArgs("Traced " + charNodes.Count + " nodes.");
      DSCs charDSCs = new DSCs();
      charDSCs.Load(charLinks, settings.DataSource.FindByName("AGMasterDB").DataSource_Text);
      this.OnStatusChanged(new StatusChangedArgs("Traced " + charDSCs.Count + " direct subcatchments.");
      this.OnStatusChanged(new StatusChangedArgs("*******************************************************");
      this.OnStatusChanged(new StatusChangedArgs("*		Model		Trace *");
      this.OnStatusChanged(new StatusChangedArgs("*	Links:	" + modelLinks.Count + "	" + charLinks.Count + " *");
      this.OnStatusChanged(new StatusChangedArgs("*	Nodes:	" + modelNodes.Count + "	" + charNodes.Count + " *");
      this.OnStatusChanged(new StatusChangedArgs("*	DSCs:	" + modelDSCs.Count + "	" + charDSCs.Count + " *");
      this.OnStatusChanged(new StatusChangedArgs("*******************************************************");
      foreach (Link l in modelLinks.Values)
      {
          if (l.MLinkID != 0 && charLinks.FindByMLinkID(l.MLinkID) == null)
          {
              //this.AddWarningStatus("MLinkID " + l.MLinkID + " missing from new trace.");
          }
      }
      foreach (Link l in charLinks.Values)
      {
          if (l.MLinkID != 0 && modelLinks.FindByMLinkID(l.MLinkID) == null)
          {
              //this.AddWarningStatus("MLinkID " + l.MLinkID + " does not exist in model.");
          }
      }
      foreach (Node n in modelNodes.Values)
      {
          if (n.Name.IndexOf("SPL") == -1 && !charNodes.Contains(n.Name))
          {
              //this.AddWarningStatus("Node " + n.Name + " missing from new trace.");
          }
      }
      foreach (Node n in charNodes.Values)
      {
          if (!modelNodes.Contains(n.Name))
          {
              //this.AddWarningStatus("Node " + n.Name+ " does not exist in model.");
          }			
      }
      foreach (DSC d in modelDSCs.Values)
      {
          if (!charDSCs.Contains(d.DSCID))
          {
              //this.AddWarningStatus("DSC " + d.DSCID + " missing from new trace.");
          }
      }
      foreach (DSC d in charDSCs.Values)
      {
          if (!modelDSCs.Contains(d.DSCID))
          {
              //this.AddWarningStatus("DSC " + d.DSCID + " does not exist in model.");
          }
      }
      this.OnStatusChanged(new StatusChangedArgs("Reconciliation Complete.");*/
    }

    private void btnImportFromModel_Click(object sender, EventArgs e)
    {
      ModelCatalogViewForm mcForm;
      mcForm = new ModelCatalogViewForm();
      Model[] models;
      if (mcForm.ShowDialog() != DialogResult.OK)
      {
        return;
      }
      models = mcForm.SelectedModels;
      this.OnStatusChanged(new StatusChangedArgs("Importing start/stop links from " + models.Length + " models."));

      foreach (Model m in models)
      {
        this.OnStatusChanged(new StatusChangedArgs("Importing start/stop links from " + m.Path + "."));
        if (startLinks.Count == 0 & stopLinks.Count == 0)
        {
          this.txtStudyAreaName.Text = mcForm.StudyArea;
        }

        try
        {
          Cursor = Cursors.WaitCursor;
          foreach (Link l in m.StartLinks.Values)
          {
            Link startLink;
            startLink = MstLinks.FindByMLinkID(l.MLinkID);
            if (startLink != null)
            {
              this.startLinks.Add(startLink);
            }
            else
            {
              this.OnStatusChanged(new StatusChangedArgs("Start link missing from master database. MLinkID = " + l.MLinkID, StatusChangeType.Warning));
            }
            //TODO: Log missing start link
          }
          foreach (Link l in m.StopLinks.Values)
          {
            Link stopLink;
            stopLink = MstLinks.FindByMLinkID(l.MLinkID);
            if (stopLink != null)
            {
              this.stopLinks.Add(stopLink);
            }
            else
            {
              this.OnStatusChanged(new StatusChangedArgs("Stop link missing from master database. MLinkID = " + l.MLinkID, StatusChangeType.Warning));
            }
            //TODO: Log missing stop link				
          }
        }
        catch (Exception ex)
        {
          this.startLinks.Clear();
          this.stopLinks.Clear();
          this.OnStatusChanged(new StatusChangedArgs(ex.Message, StatusChangeType.Error));
        }
        finally
        {
          Cursor = Cursors.Default;
        }
      }
    }
    private void btnImportFromIni_Click(object sender, EventArgs e)
    {
      ofd.DefaultExt = ".ini";
      ofd.FileName = "Model.ini";
      ofd.Filter = "Model.ini|*.ini";
      ofd.CheckFileExists = true;
      ofd.AddExtension = true;
      ofd.Title = "Find a Model.ini file";
      if (ofd.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      string iniFileName = ofd.FileName;
      string studyAreaName = ReadTraceLinksFromIniFile(iniFileName);

      if (txtStudyAreaName.Text == null || txtStudyAreaName.Text == "")
      {
        txtStudyAreaName.Text = studyAreaName;
      }
    }
    private string ReadTraceLinksFromIniFile(string iniFileName)
    {
      SystemsAnalysis.Utils.INIFile iniFile;
      iniFile = new SystemsAnalysis.Utils.INIFile(iniFileName);
      startLinks.Clear();
      stopLinks.Clear();
      if (iniFile.GetINIKeys("RootLinks") != null)
      {
        foreach (string s in iniFile.GetINIKeys("RootLinks"))
        {
          Link l = MstLinks.FindByMLinkID(Convert.ToInt32(s));
          if (l != null)
          {
            startLinks.Add(l);
          }
        }
      }
      if (iniFile.GetINIKeys("ForcedStartLinks") != null)
      {
        foreach (string s in iniFile.GetINIKeys("ForcedStartLinks"))
        {
          Link l = MstLinks.FindByMLinkID(Convert.ToInt32(s));
          if (l != null)
          {
            startLinks.Add(l);
          }
        }
      }
      if (iniFile.GetINIKeys("StopLinks") != null)
      {
        foreach (string s in iniFile.GetINIKeys("StopLinks"))
        {
          Link l = MstLinks.FindByMLinkID(Convert.ToInt32(s));
          if (l != null)
          {
            stopLinks.Add(l);
          }
        }
      }
      if (iniFile.GetINIKeys("ForcedStopLinks") != null)
      {
        foreach (string s in iniFile.GetINIKeys("ForcedStopLinks"))
        {
          Link l = MstLinks.FindByMLinkID(Convert.ToInt32(s));
          if (l != null)
          {
            stopLinks.Add(l);
          }
        }
      }

      string[] pathChunks;
      pathChunks = iniFileName.Split(new char[] { '\\' });

      string studyAreaName;
      studyAreaName = pathChunks[pathChunks.Length - 2];
      return studyAreaName;
    }

    private void cmbReportChooser_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
    {
      charEngine = new CharacterizationEngine((string)cmbReportChooser.Value);

      reportOptionsLayoutPanel.RowCount = charEngine.ReportInfos.Count;
      reportOptionsLayoutPanel.Controls.Clear();

      BuildAuxilaryDataControls(charEngine.ReportInfos);
      ResizeReportOptionsPanel();
      LoadReport(Convert.ToString(cmbReportChooser.Value));
    }

    private void BuildAuxilaryDataControls(Dictionary<string, ReportBase.ReportInfo> reportInfos)
    {
      int labelWidth = 0;
      int browseWidth = 0;
      int i = 0;
      foreach (KeyValuePair<string, ReportLibraries.ReportBase.ReportInfo> kvp in reportInfos)
      {
        lblIncludedLibrary = new Infragistics.Win.Misc.UltraLabel();
        lblIncludedLibrary.Text = "Included Library:";
        lblIncludedLibrary.AutoSize = true;
        lblIncludedLibrary.Font = new Font(lblIncludedLibrary.Font, FontStyle.Bold);
        labelWidth = Math.Max(labelWidth, lblIncludedLibrary.Width);
        reportOptionsLayoutPanel.Controls.Add(lblIncludedLibrary, 0, i);
        lblIncludedLibraryName = new Infragistics.Win.Misc.UltraLabel();
        lblIncludedLibraryName.AutoSize = true;
        lblIncludedLibraryName.Text = kvp.Key;
        reportOptionsLayoutPanel.Controls.Add(lblIncludedLibraryName, 1, i);
        reportOptionsLayoutPanel.SetColumnSpan(lblIncludedLibraryName, 2);
        if (kvp.Value.RequiresAuxilaryData)
        {
          grpBoxReportBase.Expanded = true;
          foreach (KeyValuePair<string, string> kvp2 in kvp.Value.AuxilaryDataDescription)
          {
            i++;
            lblAuxilaryData = new Infragistics.Win.Misc.UltraLabel();
            lblAuxilaryData.Text = kvp2.Key;
            //lblAuxilaryData.AutoSize = true;
            lblAuxilaryData.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            lblAuxilaryData.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            labelWidth = Math.Max(labelWidth, lblAuxilaryData.Width);
            reportOptionsLayoutPanel.Controls.Add(lblAuxilaryData, 0, i);
            txtAuxilaryDataValue = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            txtAuxilaryDataValue.Text = kvp2.Value;
            txtAuxilaryDataValue.AutoSize = true;
            txtAuxilaryDataValue.Name = kvp.Key + "." + kvp2.Key;
            txtAuxilaryDataValue.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            reportOptionsLayoutPanel.Controls.Add(txtAuxilaryDataValue, 1, i);
            btnBrowseAuxilaryData = new Infragistics.Win.Misc.UltraButton();
            btnBrowseAuxilaryData.Text = "Browse...";
            btnBrowseAuxilaryData.AutoSize = true;
            btnBrowseAuxilaryData.Click += new EventHandler(browseButton_Click);
            btnBrowseAuxilaryData.Name = kvp.Key + "." + kvp2.Key;
            btnBrowseAuxilaryData.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            lblAuxilaryData.Height = btnBrowseAuxilaryData.Height;
            browseWidth = Math.Max(browseWidth, btnBrowseAuxilaryData.Width);
            reportOptionsLayoutPanel.Controls.Add(btnBrowseAuxilaryData, 2, i);
          }
        }
        i++;
      }
      reportOptionsLayoutPanel.ColumnStyles[2].Width = browseWidth;
      reportOptionsLayoutPanel.ColumnStyles[0].Width = labelWidth;
    }
    private void browseButton_Click(object sender, EventArgs e)
    {
      Infragistics.Win.Misc.UltraButton browseButton;
      Infragistics.Win.UltraWinEditors.UltraTextEditor textEditor;

      browseButton = (Infragistics.Win.Misc.UltraButton)sender;
      textEditor = (Infragistics.Win.UltraWinEditors.UltraTextEditor)reportOptionsLayoutPanel.Controls[browseButton.Name];

      ofd.Title = "Set Auxilary Data: " + browseButton.Name;
      ofd.FileName = textEditor.Text;
      ofd.Filter = "*.*|*.*";

      if (ofd.ShowDialog() != DialogResult.OK)
      {
        return;
      }
      textEditor.Text = ofd.FileName;
    }

    /// <summary>
    /// Event that occurs when CharacterizationEngine reports that it's status has changed.
    /// </summary>
    public event OnStatusChangedEventHandler StatusChanged;

    /// <summary>
    /// Internally called function that fires the OnStatusChanged event.
    /// </summary>
    /// <param name="e">Parameter that contains the string describing the new state.</param>
    public virtual void OnStatusChanged(StatusChangedArgs e)
    {
      if (StatusChanged != null)
        StatusChanged(e);
    }
    public virtual void OnBackgroundStatusChanged(StatusChangedArgs e)
    {
      if (StatusChanged != null)
        backgroundWorker1.ReportProgress(0, e);
    }

    private void grpBoxReportBase_ExpandedStateChanged(object sender, EventArgs e)
    {
      ResizeReportOptionsPanel();
    }
    private void ResizeReportOptionsPanel()
    {
      if (grpBoxReportBase.Expanded)
      {
        grpBoxReportBase.Height = reportOptionsLayoutPanel.PreferredSize.Height + 23;
      }
    }

    private void btnImportReport_Click(object sender, EventArgs e)
    {
      ofd.DefaultExt = "xml";
      ofd.Title = "Select a custom report (*.xml)";
      ofd.Filter = "*.xml|*.xml";
      if (ofd.ShowDialog() != DialogResult.OK)
      {
        return;
      }
      AddReportToList(ofd.FileName);
      cmbReportChooser.SelectedRow = cmbReportChooser.Rows[cmbReportChooser.Rows.Count - 1];
      LoadReport(ofd.FileName);
    }
    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      CharacterizationData charData = (CharacterizationData)e.Argument;
      string studyArea = charData.studyArea;
      string outputFile = charData.outputFile;
      string reportTemplate = charData.reportTemplate;
      Links charLinks;

      try
      {
        backgroundWorker1.ReportProgress(0, new StatusChangedArgs("Performing trace."));

        charLinks = MstLinks.Trace(this.startLinks, this.stopLinks);
        if (charLinks == null)
        {
          throw new Exception("Error: No links were selected. Please verify start links.");
        }
        backgroundWorker1.ReportProgress(0, new StatusChangedArgs("Traced " + charLinks.Count + " links."));

        backgroundWorker1.ReportProgress(0, new StatusChangedArgs("Creating characterization engine."));

        charEngine = new CharacterizationEngine(reportTemplate);

        charEngine.StatusChanged += new Utils.Events.OnStatusChangedEventHandler(OnBackgroundStatusChanged);

        foreach (ReportBase.ReportInfo reportInfo in charEngine.ReportInfos.Values)
        {
          if (!reportInfo.RequiresAuxilaryData)
          {
            continue;
          }
          string library = reportInfo.ReportName;
          foreach (string auxilaryDataName in reportInfo.AuxilaryDataDescription.Keys)
          {
            reportInfo.AuxilaryData.Add(auxilaryDataName, charData.auxilaryData[auxilaryDataName]);
          }
        }
        charEngine.Characterize(charLinks, outputFile, studyArea, backgroundWorker1, e);
        writeMetaData(outputFile, charLinks.Count);
        e.Result = outputFile;
      }
      catch (Exception ex)
      {
        if (!backgroundWorker1.CancellationPending)
        {
          backgroundWorker1.ReportProgress(0, new StatusChangedArgs(ex.Message));
        }
      }
      return;
    }
    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.OnStatusChanged((StatusChangedArgs)e.UserState);
    }
    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Cancelled || e.Error != null)
      {
        this.OnStatusChanged(new StatusChangedArgs("Generate Report Canceled."));
      }
      else
      {
        string outputFile = (string)e.Result;
        //object blah = new object();
        LoadReport(outputFile);
        //axWebBrowser.Navigate(outputFile, ref blah, ref blah, ref blah, ref blah );	
        if (System.IO.File.Exists(outputFile))
        {
          this.OnStatusChanged(new StatusChangedArgs("Wrote output file: '" + outputFile + "'."));
          this.OnStatusChanged(new StatusChangedArgs("** Characterization Complete! **"));
        }
        else
        {
          string error = "Characterization incomplete! " + "Could not create output file: '" + outputFile + "'.";
          this.OnStatusChanged(new StatusChangedArgs(error, StatusChangeType.Error));
        }
      }
      pnlCancelBackgroundWorker.Visible = false;
    }
    void backgroundWorkerCancel1_CancelRequested(object sender, CancelEventArgs e)
    {
      if (backgroundWorker1.WorkerSupportsCancellation)
      {
        backgroundWorker1.CancelAsync();
      }
    }

    private void CreateReportForm_Load(object sender, EventArgs e)
    {
      splitContainer1.SplitterDistance = btnPreviewTrace.Width + 22;
    }

    /// <summary>
    /// Loads a layerfile into the supplied ESRI map control at index 0 and returns a reference to the layer
    /// </summary>
    /// <param name="fullPath">Path to the layerfile</param>
    /// <param name="mapControl">An ESRI map control object</param>
    /// <returns>An ESRI ILayer object</returns>
    public ILayer LoadLayerFile(string fullPath, AxMapControl mapControl)
    {
      if (System.IO.File.Exists(fullPath) == false)
        return null;

      try
      {
        mapControl.AddLayerFromFile(fullPath, 0);
        this.OnStatusChanged(new StatusChangedArgs("Added layer to MapControl: '" + System.IO.Path.GetFileName(fullPath) + "'"));
        ILayer layer = mapControl.get_Layer(0);

        return layer;
      }
      catch (Exception ex)
      {
        this.OnStatusChanged(new StatusChangedArgs("Error loading layer: " + System.IO.Path.GetFileName(fullPath), StatusChangeType.Warning));
        throw ex;
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (backgroundWorker1.WorkerSupportsCancellation)
      {
        backgroundWorker1.CancelAsync();
      }
    }

    private void btnBrowseAuxilaryData_Click(object sender, EventArgs e)
    {

    }

    private void btnGenerateBatchReports_Click(object sender, EventArgs e)
    {
      ofd.DefaultExt = ".txt";
      ofd.Filter = "*|*";
      ofd.Title = "Find a Batch File list";
      if (ofd.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      string batchFileList = ofd.FileName;
      StreamReader tr = new StreamReader(batchFileList);

      List<string> modelList = new List<string>();
      string outputFolder;
      string studyArea;

      outputFolder = Path.GetDirectoryName(batchFileList);

      while (!tr.EndOfStream)
      {
        modelList.Add(tr.ReadLine());
      }
      tr.Close();

      StreamWriter sw = new StreamWriter(Path.Combine(outputFolder, "_auto_generate_report.txt"));

      foreach (string modelName in modelList)
      {
        sw.WriteLine("Writing report for " + modelName);

        try
        {
          studyArea = ReadTraceLinksFromIniFile(modelName);
          ExecuteCharacterization(Path.Combine(outputFolder, studyArea + ".xml"), studyArea, false);
        }
        catch (Exception ex)
        {
          sw.WriteLine("Failed: " + ex.Message);
        }        
      }
      sw.Close();
    }

  }
}
