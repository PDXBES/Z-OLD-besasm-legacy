// Project: UI, File: CostsPage.cs
// Namespace: CostEstimator.UI, Class: CostsPage
// Path: C:\Development\CostEstimatorV2\UI, Author: Arnel
// Code lines: 29, Size of file: 486 Bytes
// Creation date: 3/21/2008 4:00 AM
// Last modified: 9/30/2010 7:26 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using SystemsAnalysis.Analysis.CostEstimator.Classes;
using Infragistics.Win.UltraWinDataSource;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTree;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
  public partial class CostsPage : CostEstimator.UI.ChildFormTemplate
  {
    #region Variables
    private Project _project;
    private Infragistics.Win.ValueList _factorTypeListForGridCosts = new Infragistics.Win.ValueList();
    private Infragistics.Win.ValueList _factorTypeListForGridCostFactorPool =
    new Infragistics.Win.ValueList();
    private bool settingUpProject;

    private List<string> _directConstructionItems = new List<string>();
    private List<DirectConstructionSelection> _directConstructionSelection = new List<DirectConstructionSelection>();
    private UserSettings _Settings;

    public CostItemFactor _currentDirectConstructionCIF;
    #endregion

    #region Constructors
    /// <summary>
    /// Create costs page
    /// </summary>
    private CostsPage()
    {
      InitializeComponent();
    } // CostsPage()

    /// <summary>
    /// Create costs page
    /// </summary>
    /// <param name="project">Project</param>
    public CostsPage(Project project)
      : this()
    {
      Project = project;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Project
    /// </summary>
    /// <returns>Project</returns>
    public Project Project
    {
      get
      {
        return _project;
      } // get
      set
      {
        _project = value;
        dsProject.ResetCachedValues();
        settingUpProject = true;
        gridCosts.Refresh();

        lblENR.Text = string.Format("/ Base ENR: {0}", _project.PipeCoster.BaseENR);
        trackENR.MinValue = _project.PipeCoster.BaseENR.Value / 4;
        trackENR.MaxValue = _project.PipeCoster.BaseENR.Value * 4;
        trackENR.MidpointSettings.Value = _project.PipeCoster.BaseENR.Value;
        trackENR.Value = _project.ENR;
        calENRDate.Value = new DateTime(_project.ProjectInfo.ENR.Year,
        _project.ProjectInfo.ENR.Month, 1);
        edtENR.Value = _project.ENR;
        _project.ProjectInfo.Clean();

        SetupProjectDataSource();
        settingUpProject = false;
        dsProject.ResetCachedValues();
      } // set
    } // Project

    /// <summary>
    /// Settings
    /// </summary>
    /// <returns>User settings</returns>
    public UserSettings Settings
    {
      get
      {
        return _Settings;
      } // get
      set
      {
        _Settings = value;
      } // set
    } // Settings
    #endregion

    #region Methods
    /// <summary>
    /// Sets up the local data source for the loaded project
    /// </summary>
    private void SetupProjectDataSource()
    {
      System.Diagnostics.Debug.WriteLine("SetupProjectDataSource");
      dsProject.Rows.SetCount(1);
      UltraDataRowsCollection estimateRows = dsProject.Rows[0].GetChildRows("CostItemFactor1");
      estimateRows.SetCount(_project.EstimateCount);

      for (int i = 0; i < _project.EstimateCount; i++)
      {
        CostItemFactor estimateCostItemFactor = _project.Estimate(i);
        UltraDataRow estimateCostItemFactorRow = estimateRows[i];
        SetupDataSetForCostItemFactor(estimateCostItemFactor, estimateCostItemFactorRow, 1);
      }
    }

    /// <summary>
    /// Initialize
    /// </summary>
    /// <param name="parentControl">Parent control</param>
    /// <param name="mainForm">Main form</param>
    public override void Initialize(Control parentControl, Main mainForm)
    {
      System.Diagnostics.Debug.WriteLine("Initialize");
      base.Initialize(parentControl, mainForm);
      //toolbarManager.MdiParentManager = AppForm.toolbarManager;
    } // Initialize(parentControl, mainForm)

    /// <summary>
    /// Process depth of row
    /// </summary>
    /// <param name="bandName">Band name</param>
    /// <returns>String</returns>
    private int ProcessDepthOfRow(string bandName)
    {
      System.Diagnostics.Debug.WriteLine("ProcessDepthOfRow");
      string numbers = "01234567890";
      char[] numberChars = numbers.ToCharArray();
      int depthIndex = bandName.LastIndexOfAny(numberChars);
      return Convert.ToInt32(bandName.Substring(depthIndex));
    } // ProcessDepthOfRow(bandName)

    /// <summary>
    /// Depth of row
    /// </summary>
    /// <param name="row">Row</param>
    /// <returns>Integer representing the number of levels of depth from the top of the costing hierarchy</returns>
    private int DepthOfRow(UltraDataRow row)
    {
      return ProcessDepthOfRow(row.Band.Key);
    } // DepthOfRow(row)

    /// <summary>
    /// Depth of row
    /// </summary>
    /// <param name="row">Row</param>
    /// <returns>Integer representing the number of levels of depth from the top of the costing hierarchy</returns>
    private int DepthOfRow(UltraGridRow row)
    {
      return ProcessDepthOfRow(row.Band.Key);
    } // DepthOfRow(row)

    /// <summary>
    /// Process band of row
    /// </summary>
    /// <param name="rowName">Row name</param>
    /// <returns>String</returns>
    private string ProcessBandNameOfRow(string bandName)
    {
      System.Diagnostics.Debug.WriteLine("ProcessBandNameOfRow");
      string numbers = "01234567890";
      char[] numberChars = numbers.ToCharArray();
      int depthIndex = bandName.LastIndexOfAny(numberChars);

      return bandName.Substring(0, depthIndex);
    } // ProcessBandOfRow(rowName)

    /// <summary>
    /// Band of row
    /// </summary>
    /// <param name="row">Row</param>
    /// <returns>Int</returns>
    private string BandOfRow(UltraDataRow row)
    {
      return ProcessBandNameOfRow(row.Band.Key);
    } // BandOfRow(row)

    /// <summary>
    /// Band of row
    /// </summary>
    /// <param name="row">Row</param>
    /// <returns>String</returns>
    private string BandOfRow(UltraGridRow row)
    {
      return ProcessBandNameOfRow(row.Band.Key);
    } // BandOfRow(row)

    /// <summary>
    /// Estimate of row
    /// </summary>
    /// <param name="row">Row</param>
    /// <returns>Cost item factor</returns>
    private CostItemFactor EstimateOfRow(UltraDataRow row)
    {
      if (row == null)
        return null;

      int depth = DepthOfRow(row);

      if (depth == 0)
        return null;
      else
      {
        UltraDataRow currentRow = row;
        if (BandOfRow(row) != "CostItemFactor")
          currentRow = row.ParentRow;

        List<int> lineage = new List<int>();
        while (currentRow != null)
        {
          lineage.Insert(0, currentRow.Index);
          currentRow = currentRow.ParentRow;
        } // while

        return _project.Estimate(lineage[1]);
      } // else
    } // EstimateOfRow(row)

    /// <summary>
    /// Estimate of row
    /// </summary>
    /// <param name="row">Row</param>
    /// <returns>Cost item factor</returns>
    private CostItemFactor EstimateOfRow(UltraGridRow row)
    {
      if (row == null)
        return null;

      UltraDataRow dataRow = row.ListObject as UltraDataRow;

      return EstimateOfRow(dataRow);
    } // EstimateOfRow(row)

    /// <summary>
    /// Get cost item factor
    /// </summary>
    /// <param name="column">Column</param>
    /// <param name="row">Row</param>
    /// <returns>Cost item factor</returns>
    private CostItemFactor GetCostItemFactor(UltraDataRow row)
    {
      System.Diagnostics.Debug.WriteLine("GetCostItemFactor");
      if (row == null)
        return null;

      int depth = DepthOfRow(row);

      if (depth == 0)
        return _project.Root;
      else
      {
        UltraDataRow currentRow = row;
        if (BandOfRow(row) != "CostItemFactor")
          currentRow = row.ParentRow;

        List<int> lineage = new List<int>();
        while (currentRow != null)
        {
          lineage.Insert(0, currentRow.Index);
          currentRow = currentRow.ParentRow;
        }

        CostItemFactor currentCostItemFactor = _project.Estimate(lineage[1]);

        for (int i = 2; i < lineage.Count; i++)
        {
          if (currentCostItemFactor.ChildCostItemFactorCount > lineage[i])
            currentCostItemFactor = currentCostItemFactor.ChildCostItemFactor(lineage[i]);
          else
            // This happens when entering a new non-estimate level cost item factor
            break;
        }
        return currentCostItemFactor;
      }
    } // GetCostItemFactor(column, row)

    /// <summary>
    /// Gets the Cost Item Factor object from a grid row
    /// </summary>
    /// <param name="row">The grid row of the presumed cost item factor</param>
    /// <returns>The corresponding CostItemFactor object</returns>
    private CostItemFactor GetCostItemFactor(UltraGridRow row)
    {
      System.Diagnostics.Debug.WriteLine("GetCostItemFactor");
      if (row == null)
        return null;

      UltraDataRow dataRow = row.ListObject as UltraDataRow;

      return GetCostItemFactor(dataRow);
    } // GetCostItemFactor(column, row)

    /// <summary>
    /// Get cost item
    /// </summary>
    /// <param name="row">Row</param>
    /// <returns>Cost item</returns>
    private CostItem GetCostItem(UltraDataRow row)
    {
      System.Diagnostics.Debug.WriteLine("GetCostItem");
      string bandName = BandOfRow(row);
      if (bandName == "CostItem")
      {
        CostItemFactor currentCostItemFactor = GetCostItemFactor(row);
        return (currentCostItemFactor != null) ? currentCostItemFactor.CostItem : null;
      }
      else
        return null;
    } // GetCostItem(row)

    /// <summary>
    /// Gets the Cost Item object from a grid row
    /// </summary>
    /// <param name="row">The grid row of the presumed cost item</param>
    /// <returns>The corresponding CostItem object</returns>
    private CostItem GetCostItem(UltraGridRow row)
    {
      System.Diagnostics.Debug.WriteLine("GetCostItem");
      UltraDataRow dataRow = row.ListObject as UltraDataRow;
      return GetCostItem(dataRow);
    } // GetCostItem(row)

    /// <summary>
    /// Get cost factor
    /// </summary>
    /// <param name="row">Row</param>
    /// <returns>Cost factor</returns>
    private CostFactor GetCostFactor(UltraDataRow row)
    {
      System.Diagnostics.Debug.WriteLine("GetCostFactor");
      string bandName = BandOfRow(row);
      if (bandName == "Factors")
      {
        CostItemFactor currentCostItemFactor = GetCostItemFactor(row);
        return (currentCostItemFactor != null && currentCostItemFactor.CostFactorsCount > 0) ?
        currentCostItemFactor.CostFactor(row.Index) : null;
      }
      else
        return null;
    } // GetCostFactor(row)

    /// <summary>
    /// Gets the Cost Factor object from a grid row
    /// </summary>
    /// <param name="row">The grid row of the presumed cost factor</param>
    /// <returns>The corresponding CostFactor object</returns>
    private CostFactor GetCostFactor(UltraGridRow row)
    {
      System.Diagnostics.Debug.WriteLine("GetCostFactor");
      UltraDataRow dataRow = row.ListObject as UltraDataRow;
      return GetCostFactor(dataRow);
    } // GetCostFactor(row)

    /// <summary>
    /// Setup data set for cost item factor
    /// </summary>
    /// <param name="aCostItemFactor">A cost item factor</param>
    private void SetupDataSetForCostItemFactor(CostItemFactor aCostItemFactor,
    UltraDataRow aCostItemFactorRow, int depth)
    {
      System.Diagnostics.Debug.WriteLine("SetupDataSetForCostItemFactor(CostItemFactor, UltraDataRow, int)");
      UltraDataRowsCollection costItemRows = aCostItemFactorRow.GetChildRows(
      string.Format("CostItem{0}", depth + 1));

      UltraDataRowsCollection factorsRows = aCostItemFactorRow.GetChildRows(
      string.Format("Factors{0}", depth + 1));

      string costItemFactorLevelName = string.Format("CostItemFactor{0}", depth + 1);
      UltraDataRowsCollection costItemFactorRows;
      List<string> childBandNames = new List<string>();
      foreach (UltraDataBand band in aCostItemFactorRow.Band.ChildBands)
      {
        childBandNames.Add(band.Key);
      }
      if (childBandNames.Contains(costItemFactorLevelName))
        costItemFactorRows = aCostItemFactorRow.GetChildRows(costItemFactorLevelName);
      else
        costItemFactorRows = null;

      costItemRows.SetCount(aCostItemFactor.CostItem != null ? 1 : 0);
      factorsRows.SetCount(aCostItemFactor.CostFactorsCount);

      if (costItemFactorRows != null)
      {
        costItemFactorRows.SetCount(aCostItemFactor.ChildCostItemFactorCount);

        for (int i = 0; i < aCostItemFactor.ChildCostItemFactorCount; i++)
        {
          SetupDataSetForCostItemFactor(aCostItemFactor.ChildCostItemFactor(i), costItemFactorRows[i], depth + 1);
        }
      }
    } // SetupDataSetForCostItemFactor(aCostItemFactor)

    /// <summary>
    /// Hides or shows the Add Items row from the grid
    /// </summary>
    /// <param name="hidden">true to hide the add (new) item rows, false to show</param>
    public void HideAddItemsFromCostGrid(bool hidden)
    {
      System.Diagnostics.Debug.WriteLine("HideAddItemsFromCostGrid");
      if (hidden)
        gridCosts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
      else
        gridCosts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
    }

    /// <summary>
    /// Handle detach from pool
    /// </summary>
    private void HandleDetachFromPool()
    {
      System.Diagnostics.Debug.WriteLine("HandleDetachFromPool()");
      UltraGridRow currentRow = gridCosts.ActiveRow;
      string bandName = BandOfRow(currentRow);
      if (bandName == "CostItem" || bandName == "Factors")
      {
        switch (bandName)
        {
          case "CostItem":

            break;
          case "Factors":
            string factorName = Convert.ToString(currentRow.GetCellValue(currentRow.Band.Columns["Name"]));
            CostFactor factor = _project.FactorFromPool(factorName);
            CostFactor newFactor = _project.CopyFactor(factor);

            CostItemFactor currentCostItemFactor = GetCostItemFactor(currentRow);
            int indexOfFactor = currentCostItemFactor.IndexOfFactor(factor);
            currentCostItemFactor.DeleteFactor(factor);

            // Place the item at the same location in the list order
            if (indexOfFactor > currentCostItemFactor.CostFactorsCount)
              currentCostItemFactor.AddFactor(newFactor);
            else
              currentCostItemFactor.InsertFactor(indexOfFactor, newFactor);
            break;
        }

        dsProject.ResetCachedValues();
        gridCosts.Refresh();
      } // if
    } // HandleDetachFromPool()

    /// <summary>
    /// Read optional direct construction items
    /// </summary>
    private void ReadDirectConstructionItems()
    {
      System.Diagnostics.Debug.WriteLine("ReadDirectConstructionItems");
      XmlReaderSettings readerFileSettings = new XmlReaderSettings { IgnoreWhitespace = true,
      ValidationType = ValidationType.Schema,
      ConformanceLevel = ConformanceLevel.Document };
      string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
      XmlReader directConstructionItemsFile = XmlReader.Create(
      String.Format(@"{0}\DirectConstructionItems.xml", appPath), readerFileSettings);

      using (XmlReader loadFile = XmlReader.Create(directConstructionItemsFile, readerFileSettings))
      {
        XPathDocument projectDoc = new XPathDocument(loadFile, XmlSpace.None);
        XPathNavigator projectNav = projectDoc.CreateNavigator();

        XPathNodeIterator xpathIter = projectNav.Select("/DirectConstructionItems/ItemList/*");
        _directConstructionItems.Clear();
        while (xpathIter.MoveNext())
        {
          _directConstructionItems.Add(xpathIter.Current.Value);
        } // while
      } // using (loadFile)

      //lstDirectConstructionItems.Items.Clear();
      //foreach (string item in _directConstructionItems)
      //  lstDirectConstructionItems.Items.Add(item, item);

      _directConstructionSelection.Clear();
      foreach (string item in _directConstructionItems)
      {
        DirectConstructionSelection dcItem = new DirectConstructionSelection { Selected = false,
        Name = item,
        Factor = 0.0f };
        _directConstructionSelection.Add(dcItem);
      } // foreach  (item)
      dsOtherDirectConstructionFactors.Rows.SetCount(_directConstructionItems.Count);
    } // ReadOptionalDirectConstructionItems()

    /// <summary>
    /// Fill select cost item factor tree
    /// </summary>
    /// <param name="estimateIndex">Estimate index</param>
    private void FillSelectCostItemFactorTree(UltraTreeNode parentNode, CostItemFactor currentCIF, ref int nodeID)
    {
      System.Diagnostics.Debug.WriteLine("FillSelectCostItemFactorTree()");
      foreach (CostItemFactor cif in currentCIF.ChildCostItemFactors)
      {
        nodeID++;
        System.Diagnostics.Debug.WriteLine(string.Format("{0} {1}", nodeID, cif.Name));
        UltraTreeNode addedNode = parentNode.Nodes.Add(nodeID.ToString());
        addedNode.Text = string.Format("{0} ({1:C0})", cif.Name, cif.Cost);
        addedNode.Tag = cif;
        if (cif.HasChildren)
        {
          FillSelectCostItemFactorTree(addedNode, cif, ref nodeID);
        }
      }
    } // FillSelectCostItemFactorTree(estimateIndex)

    /// <summary>
    /// Sets up the grid to have user select a cost item factor
    /// </summary>
    private void SetupSelectCostItemFactorTree()
    {
      System.Diagnostics.Debug.WriteLine("SetupSelectCostItemFactorTree()");
      treeCostItemFactor.Nodes.Clear();
      CostItemFactor currentEstimate = EstimateOfRow(gridCosts.ActiveRow);
      if (currentEstimate != null)
      {
        UltraTreeNode rootNode = treeCostItemFactor.Nodes.Add(0.ToString());
        rootNode.Text = string.Format("{0} ({1:C0})", currentEstimate.Name, currentEstimate.Cost);
        rootNode.Tag = currentEstimate;
        int currentNodeID = 0;
        FillSelectCostItemFactorTree(rootNode, currentEstimate, ref currentNodeID);
        rootNode.ExpandAll();
        tabCosts.SelectedTab = tabCosts.Tabs["SelectCostItemFactor"];
      }
      else
        MessageBox.Show("Could not figure out the estimate you're interested in.  Select a row inside an estimate and try again.");
    }

    /// <summary>
    /// Update the cells of the row currently being edited
    /// </summary>
    public void UpdateCurrentlyEditingRow()
    {
      if (gridCosts.ActiveRow != null)
        gridCosts.ActiveRow.Update();
    }

    public void WritePipeCosts(string exportFile)
    {
      try
      {
        System.Diagnostics.Debug.WriteLine("ExportPipeCosts: Hiding grid");
        gridCosts.Hide();
        try
        {
          using (StreamWriter pipeCostsStream = new StreamWriter(exportFile))
          {
            List<ReportPipeItem> pipeItems = _project.ReportPipeItems();
            pipeCostsStream.WriteLine("MLinkID,USNode,DSNode," +
            "DirectConstructionCost,TotalConstructionCost,PipelineBuildDuration");
            foreach (ReportPipeItem item in pipeItems)
            {
              string[] itemNameItems = item.Name.Split(new char[] { ' ', '-' }, StringSplitOptions.None);
              try
              {
                pipeCostsStream.WriteLine(string.Format("{0},{1},{2},{3:F0},{5:F0},{4}",
                itemNameItems[0], itemNameItems[1], itemNameItems[2],
                item.DirectConstructionCost,
                item.ConstructionDuration,
                item.TotalConstructionCost));
              } // try
              catch (Exception e)
              {
                MessageBox.Show(String.Format("{0}\n\n{1}",
                string.Format("Problem writing item {0}", item.Name), e.Message));
              } // catch
            }
            // foreach  (item)
            pipeCostsStream.Close();
          }
        }
        finally
        {
          System.Diagnostics.Debug.WriteLine("ExportPipeCosts: Showing grid");
          gridCosts.Show();
        } // finally
      }
      catch (Exception e)
      {
        MessageBox.Show("A problem occurred while writing the file:\n\n" + e.Message);
      } // catch
    }
    /// <summary>
    /// Export pipe costs
    /// </summary>
    private void ExportPipeCosts()
    {
      dlgSave.Title = "Save pipe costs as text file";
      dlgSave.DefaultExt = "csv";
      if (dlgSave.ShowDialog() != DialogResult.OK)
        return;
      string exportFile = dlgSave.FileName;

      WritePipeCosts(exportFile);
    } // ExportPipeCosts()

    public void WriteDetailedCosts(string exportFile)
    {
      // Assemble pipe costs
      List<CostItemFactor> pipeList = _project.PipeItems();
      Dictionary<CostItemFactor, CostItemFactor> pipeCIFs = new Dictionary<CostItemFactor, CostItemFactor>();
      Dictionary<CostItemFactor, CostItemFactor> lateralCIFs = new Dictionary<CostItemFactor, CostItemFactor>();
      Dictionary<CostItemFactor, CostItemFactor> manholeCIFs = new Dictionary<CostItemFactor, CostItemFactor>();
      Dictionary<CostItemFactor, List<CostItemFactor>> ancillaryCIFs = new Dictionary<CostItemFactor, List<CostItemFactor>>();
      foreach (CostItemFactor item in pipeList)
      {
        if (item.ReportItemType == ReportItemType.Pipe)
        {
          List<CostItemFactor> subItems = item.ChildCostItemFactors;
          CostItemFactor pipeCIF = null;
          CostItemFactor manholeCIF = null;
          CostItemFactor lateralCIF = null;
          List<CostItemFactor> ancillaryCIFList = new List<CostItemFactor>();
          foreach (CostItemFactor subItem in subItems)
          {
            if (subItem.Name.StartsWith("Pipe"))
            {
              pipeCIF = subItem;
              pipeCIFs.Add(item, pipeCIF);

              List<CostItemFactor> pipeSubItems = pipeCIF.ChildCostItemFactors;
              foreach (CostItemFactor pipeSubItem in pipeSubItems)
              {
                if (pipeSubItem.Name.StartsWith("Lateral"))
                {
                  lateralCIF = pipeSubItem;
                  lateralCIFs.Add(item, lateralCIF);
                }
              }
            } // if
            else
              if (subItem.Name.StartsWith("Manhole"))
              {
                manholeCIF = subItem;
                manholeCIFs.Add(item, manholeCIF);
              } // if
              else
              {
                ancillaryCIFList.Add(subItem);
              } // else
          } // foreach  (subItem)
          if (ancillaryCIFList.Count > 0)
            ancillaryCIFs.Add(item, ancillaryCIFList);
          else
            ancillaryCIFs.Add(item, new List<CostItemFactor>());
        } // if
      } // foreach  (item)

      // Do the export
      System.Diagnostics.Debug.WriteLine("ExportDetailedCosts: Hiding grid");
      gridCosts.Hide();
      try
      {
        using (StreamWriter pipeCostsStream = new StreamWriter(exportFile))
        {
          foreach (CostItemFactor item in pipeList)
          {
            char[] separators = { ' ', '-' };
            string[] tokens = item.Name.Split(separators);
            string MLinkID = "", USNode = "", DSNode = "";
            if (tokens.Length >= 3)
            {
              MLinkID = tokens[0];
              USNode = tokens[1];
              DSNode = tokens[2];
            } // if

            pipeCostsStream.WriteLine(string.Format("\"Link\",{0},{1},{2},{3:#},{4:#}",
            MLinkID, USNode, DSNode, item.Cost, item.Factor));
            if (pipeCIFs[item] != null)
              pipeCostsStream.WriteLine(string.Format("\"Pipe\",{0},{1},{2},{3:#}",
              MLinkID, USNode, DSNode, pipeCIFs[item].Cost));
            if (lateralCIFs.ContainsKey(item) && lateralCIFs[item] != null)
              pipeCostsStream.WriteLine(string.Format("\"Lateral\",{0},{1},{2},{3:#}",
              MLinkID, USNode, DSNode, lateralCIFs[item].Cost));
            if (manholeCIFs.ContainsKey(item) && (manholeCIFs[item] != null))
              pipeCostsStream.WriteLine(string.Format("\"Manhole\",{0},{1},{2},{3:#}",
              MLinkID, USNode, DSNode, manholeCIFs[item].Cost));
            if (ancillaryCIFs.ContainsKey(item) && (ancillaryCIFs[item].Count > 0))
            {
              foreach (CostItemFactor ancillaryCIF in ancillaryCIFs[item])
              {
                string ancillaryName = string.Empty;
                if (ancillaryCIF.Name.StartsWith("Boring/jacking"))
                  ancillaryName = "Boring/jacking";
                else
                  if (ancillaryCIF.Name.StartsWith("Microtunneling"))
                    ancillaryName = "Microtunnel";
                  else
                    ancillaryName = ancillaryCIF.Name;
                pipeCostsStream.WriteLine(string.Format("\"{4}\",{0},{1},{2},{3:#}",
                MLinkID, USNode, DSNode, ancillaryCIF.Cost,
                ancillaryName));
              } // foreach  (ancillaryCIF)
            } // if
          } // foreach  (item)
        } // using (pipeCostsStream)
      } // try
      finally
      {
        System.Diagnostics.Debug.WriteLine("ExportDetailedCosts: Showing grid");
        gridCosts.Show();
      } // finally
    }
    /// <summary>
    /// Export detailed costs
    /// </summary>
    private void ExportDetailedCosts()
    {
      dlgSave.Title = "Save pipe costs as text file";
      dlgSave.DefaultExt = "csv";
      if (dlgSave.ShowDialog() != DialogResult.OK)
        return;
      string exportFile = dlgSave.FileName;

      WriteDetailedCosts(exportFile);
    } // ExportDetailedCosts()
    #endregion

    private void dsProject_CellDataRequested(object sender, CellDataRequestedEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine(string.Format("dsProject_CellDataRequested: {0} {1}",
      sender.GetType().Name, e.Column.Key));
      CostItemFactor currentCostItemFactor = GetCostItemFactor(e.Row);
      if (currentCostItemFactor != null && BandOfRow(e.Row) == "CostItemFactor")
      {
        System.Diagnostics.Debug.WriteLine("CostItemFactor");
        switch (e.Column.Key)
        {
          case "ID":
            e.Data = currentCostItemFactor.ID;
            break;
          case "Name":
            e.Data = currentCostItemFactor.Name;
            break;
          case "Cost":
            e.Data = currentCostItemFactor.Cost;
            break;
          case "Quantity":
            e.Data = currentCostItemFactor.Quantity;
            break;
          case "Factor":
            e.Data = currentCostItemFactor.Factor;
            break;
          case "PrefactoredCost":
            e.Data = currentCostItemFactor.PrefactoredCost;
            break;
          case "Comment":
            e.Data = currentCostItemFactor.Comment;
            break;
        }
      }

      CostItem currentCostItem = GetCostItem(e.Row);

      if (currentCostItem != null && BandOfRow(e.Row) == "CostItem")
      {
        System.Diagnostics.Debug.WriteLine("CostItem");
        switch (e.Column.Key)
        {
          case "ID":
            e.Data = currentCostItem.ID;
            break;
          case "Name":
            e.Data = currentCostItem.Name;
            break;
          case "Quantity":
            e.Data = currentCostItem.Quantity;
            break;
          case "UnitCost":
            e.Data = currentCostItem.UnitCost;
            break;
          case "UnitName":
            e.Data = currentCostItem.UnitName;
            break;
          case "Comment":
            e.Data = currentCostItem.Comment;
            break;
        } // switch
      } // if

      CostFactor currentCostFactor = GetCostFactor(e.Row);

      if (currentCostFactor != null && BandOfRow(e.Row) == "Factors")
      {
        System.Diagnostics.Debug.WriteLine("Factors");
        switch (e.Column.Key)
        {
          case "ID":
            e.Data = currentCostFactor.ID;
            break;
          case "Name":
            e.Data = currentCostFactor.Name;
            break;
          case "FactorValue":
            e.Data = currentCostFactor.FactorValue;
            break;
          case "FactorType":
            e.Data = currentCostFactor.FactorType.ToString();
            break;
          case "Cost":
            e.Data = currentCostItemFactor.CostFactorValue(currentCostItemFactor.CostFactorIndex(currentCostFactor));
            break;
          case "Comment":
            e.Data = currentCostFactor.Comment;
            break;
        } // switch
      } // if
    }

    private void dsProject_CellDataUpdating(object sender, CellDataUpdatingEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("dsProject_CellDataUpdating");
      string bandName = BandOfRow(e.Row);
      int id = Convert.ToInt32(e.Row.GetCellValue("ID", true));
      switch (bandName)
      {
        case "CostItem":
          switch (e.Column.Key)
          {
            case "Name":
              if (_project.CostItemFromPool(e.NewValue.ToString()) != null)
              {
                MessageBox.Show("The name for this cost item already exists. Name was modified.");
                int trialNum = 1;
                string trialName = e.NewValue.ToString() + trialNum.ToString();
                while (_project.CostItemFromPool(trialName) != null)
                {
                  trialNum++;
                  trialName = e.NewValue.ToString() + trialNum.ToString();
                } // while
                e.NewValue = trialName;
                _project.CostItemFromPool(id).Name = trialName;
                return;
              } // if
              break;
          } // switch
          break;
        case "Factors":
          switch (e.Column.Key)
          {
            case "Name":
              if (_project.FactorFromPool(e.NewValue.ToString()) != null)
              {
                MessageBox.Show("The name for this cost factor already exists. Name was modified.");
                int trialNum = 1;
                string trialName = e.NewValue.ToString() + trialNum.ToString();
                while (_project.CostItemFromPool(trialName) != null)
                {
                  trialNum++;
                  trialName = e.NewValue.ToString() + trialNum.ToString();
                } // while
                e.NewValue = trialName;
                _project.FactorFromPool(id).Name = trialName;
                return;
              } // if
              break;
          } // switch
          break;
      } // switch
    }

    private void dsProject_CellDataUpdated(object sender, CellDataUpdatedEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("dsProject_CellDataUpdated");
      string bandName = BandOfRow(e.Row);
      int id = Convert.ToInt32(e.Row.GetCellValue("ID", true));
      switch (bandName)
      {
        case "CostItemFactor":
          switch (e.Column.Key)
          {
            case "Name":
              _project.CostItemFactorFromPool(id).Name = e.NewValue == null ?
              string.Empty : Convert.ToString(e.NewValue);
              break;
            case "Quantity":
              _project.CostItemFactorFromPool(id).Quantity = (float)Convert.ToDouble(e.NewValue);
              break;
            case "Comment":
              _project.CostItemFactorFromPool(id).Comment = e.NewValue == null ?
              string.Empty : Convert.ToString(e.NewValue);
              break;
          }
          break;
        case "CostItem":
          switch (e.Column.Key)
          {
            case "Name":
              _project.CostItemFromPool(id).Name = e.NewValue == null ?
              string.Empty : Convert.ToString(e.NewValue);
              break;
            case "Quantity":
              _project.CostItemFromPool(id).Quantity = (float)Convert.ToDouble(e.NewValue);
              break;
            case "UnitCost":
              _project.CostItemFromPool(id).UnitCost = (float)Convert.ToDecimal(e.NewValue);
              break;
            case "UnitName":
              _project.CostItemFromPool(id).UnitName = e.NewValue == null ?
              string.Empty : Convert.ToString(e.NewValue);
              break;
            case "Comment":
              _project.CostItemFromPool(id).Comment = e.NewValue == null ?
              string.Empty : Convert.ToString(e.NewValue);
              break;
          }
          break;
        case "Factors":
          switch (e.Column.Key)
          {
            case "Name":
              _project.FactorFromPool(id).Name = e.NewValue == null ?
              string.Empty : Convert.ToString(e.NewValue);
              break;
            case "FactorValue":
              _project.FactorFromPool(id).FactorValue = (float)Convert.ToDouble(e.NewValue);
              break;
            case "FactorType":
              _project.FactorFromPool(id).FactorType = (CostFactorType)Enum.Parse(typeof(CostFactorType), e.NewValue.ToString());
              break;
            case "Comment":
              _project.FactorFromPool(id).Comment = e.NewValue == null ?
              string.Empty : Convert.ToString(e.NewValue);
              break;
          }
          break;
      }
    }

    private void dsProject_RowAdded(object sender, RowAddedEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("dsProject_RowAdded");
      string bandName = BandOfRow(e.Row);
      CostItemFactor parentCostItemFactor = null;
      int depth = DepthOfRow(e.Row);
      if (depth > 1)
        parentCostItemFactor = GetCostItemFactor(e.Row);
      else
        if (bandName == "CostItemFactor" && e.Row.Index >= _project.EstimateCount)
          parentCostItemFactor = _project.Root;

      // This should never happen
      if (parentCostItemFactor == null)
        return;

      switch (bandName)
      {
        case "CostItemFactor":
          CostItemFactor newCostItemFactor = new CostItemFactor("New item",
          null, null, 1);
          if (depth == 1)
            _project.AddEstimate(newCostItemFactor);
          else
          {
            int index = parentCostItemFactor.AddCostItemFactor(newCostItemFactor);
            if (index == -1)
              throw new Exception("Cannot add CostItemFactor");
            _project.AddCostItemFactorToPool(newCostItemFactor);
          }
          e.Row["ID"] = newCostItemFactor.ID;
          e.Row["Name"] = newCostItemFactor.Name;
          e.Row["Quantity"] = newCostItemFactor.Quantity;

          newCostItemFactor.ReportItemType = ReportItemType.Generic;
          ReportGenericItem reportItem = new ReportGenericItem();
          newCostItemFactor.Data = reportItem;
          reportItem.Name = newCostItemFactor.Name;
          reportItem.Quantity = newCostItemFactor.Quantity;
          reportItem.UnitName = "ea";
          reportItem.UnitPrice = newCostItemFactor.Cost;
          reportItem.Group = "General";
          reportItem.Item = newCostItemFactor;
          reportItem.Comment = string.Empty;

          break;
        case "CostItem":
          CostItem newCostItem = new CostItem("New item", 1, 0f, "ea");
          parentCostItemFactor.CostItem = newCostItem;
          _project.AddCostItemToPool(newCostItem);
          e.Row["ID"] = newCostItem.ID;
          e.Row["Name"] = newCostItem.Name;
          e.Row["Quantity"] = newCostItem.Quantity;
          e.Row["UnitCost"] = newCostItem.UnitCost;
          e.Row["UnitName"] = newCostItem.UnitName;
          break;
        case "Factors":
          CostFactor newCostFactor = new CostFactor("New factor", 1, CostFactorType.Simple);
          parentCostItemFactor.AddFactor(newCostFactor);
          _project.AddFactorToPool(newCostFactor);
          e.Row["ID"] = newCostFactor.ID;
          e.Row["Name"] = newCostFactor.Name;
          e.Row["FactorValue"] = newCostFactor.FactorValue;
          e.Row["FactorType"] = newCostFactor.FactorType;
          break;
      }
    }

    private void dsProject_RowCancelEdit(object sender, RowCancelEditEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("dsProject_RowCancelEdit()");
      string bandName = BandOfRow(e.Row);
      int depth = DepthOfRow(e.Row);
      switch (bandName)
      {
        case "CostItemFactor":
          if (depth == 1)
            _project.RevertEstimate();
          else
            _project.RevertCostItemFactorPool();
          break;
        case "CostItem":
          _project.RevertCostItemPool();
          break;
        case "Factors":
          _project.RevertFactorPool();
          break;
      }
    }

    private void dsProject_RowDeleted(object sender, RowDeletedEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("dsProject_RowDeleted()");
      dsProject.ResetCachedValues();
      //gridCosts.Refresh();
    }

    private void dsProject_RowDeleting(object sender, RowDeletingEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("dsProject_RowDeleting");
      string bandName = BandOfRow(e.Row);
      int depth = DepthOfRow(e.Row);
      CostItemFactor costItemFactor = GetCostItemFactor(e.Row.ParentRow);
      switch (bandName)
      {
        case "CostItemFactor":
          if (depth == 1)
            _project.DeleteEstimate(Convert.ToInt32(e.Row.GetCellValue("ID", true)));
          else
            costItemFactor.DeleteCostItemFactor(Convert.ToInt32(e.Row.GetCellValue("ID", true)));
          break;
        case "CostItem":
          costItemFactor.CostItem = null;
          break;
        case "Factors":
          {
            costItemFactor.DeleteFactor(Convert.ToInt32(e.Row.GetCellValue("ID", true)));
          }
          break;
      }
    }

    private static void CompileReportGenericData(CostItemFactor costItemFactor)
    {
      System.Diagnostics.Debug.WriteLine("CompileReportGenericData");
      ReportGenericItem reportItem = costItemFactor.Data as ReportGenericItem;
      reportItem.Name = costItemFactor.Name;
      reportItem.Quantity = costItemFactor.Quantity;
      reportItem.UnitName = costItemFactor.CostItem != null ?
      costItemFactor.CostItem.UnitName : "ea";
      reportItem.UnitPrice = costItemFactor.CostItem != null ?
        (float)((double)costItemFactor.CostItem.UnitCost *
        costItemFactor.Factor) : costItemFactor.Cost;
      reportItem.Group = "General";
      reportItem.Item = costItemFactor;
      reportItem.Comment = costItemFactor.Comment ?? string.Empty;
    }

    private static void CompileReportInflowControlData(CostItemFactor costItemFactor)
    {
      System.Diagnostics.Debug.WriteLine("CompileReportInflowControlData");
      ReportInflowControlItem reportItem = costItemFactor.Data as ReportInflowControlItem;
      reportItem.Name = costItemFactor.Name;
      reportItem.Comment = costItemFactor.Comment ?? string.Empty;
      reportItem.Cost = costItemFactor.Cost;
    }

    private static void CompileReportPipeData(CostItemFactor costItemFactor)
    {
      System.Diagnostics.Debug.WriteLine("CompileReportPipeData");
      ReportPipeItem reportItem = costItemFactor.Data as ReportPipeItem;
      reportItem.Name = costItemFactor.Name;
      reportItem.Comment = costItemFactor.Comment ?? string.Empty;
    }

    private void dsProject_RowEndEdit(object sender, RowEndEditEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("dsProject_RowEndEdit()");
      string bandName = BandOfRow(e.Row);
      CostItemFactor costItemFactor = null;
      if (bandName == "CostItemFactor")
        costItemFactor = GetCostItemFactor(e.Row);
      else
        costItemFactor = GetCostItemFactor(e.Row.ParentRow);

      switch (bandName)
      {
        case "CostItemFactor":
          switch (costItemFactor.ReportItemType)
          {
            case ReportItemType.Generic:
              CompileReportGenericData(costItemFactor);
              break;
            case ReportItemType.InflowControl:
              CompileReportInflowControlData(costItemFactor);
              break;
            case ReportItemType.Pipe:
              CompileReportPipeData(costItemFactor);
              break;
          }
          break;
        case "CostItem":
        case "Factors":
          {
            CostItemFactor currentCIF = GetCostItemFactor(e.Row);
            switch (currentCIF.ReportItemType)
            {
              case ReportItemType.Generic:
                CompileReportGenericData(currentCIF);
                break;
              case ReportItemType.InflowControl:
                CompileReportInflowControlData(currentCIF);
                break;
              case ReportItemType.Pipe:
                CompileReportPipeData(currentCIF);
                break;
            }
          } // block
          break;
      }

      dsProject.ResetCachedValues();
      gridCosts.Refresh();
    }

    private void CostsPage_Load(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("CostsPage_Load");
      // Setup cost grid for factors
      foreach (string s in Enum.GetNames(typeof(CostFactorType)))
      {
        CostFactorType itemToAdd = (CostFactorType)Enum.Parse(typeof(CostFactorType), s);
        _factorTypeListForGridCosts.ValueListItems.Add(itemToAdd, s);
        _factorTypeListForGridCostFactorPool.ValueListItems.Add(itemToAdd, s);
      }

      foreach (UltraGridBand band in gridCosts.DisplayLayout.Bands)
        if (band.Key.StartsWith("Factor"))
        {
          band.Columns["FactorType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
          band.Columns["FactorType"].ValueList = _factorTypeListForGridCosts;
        }

      tabCosts.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
      ReadDirectConstructionItems();
    }

    private void trackENR_ValueChanged(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("trackENR_ValueChanged()");
      if (!settingUpProject)
      {
        edtENR.Text = trackENR.Value.ToString();
        _project.FactorFromPool("ENR").FactorValue = trackENR.MidpointSettings.Value == null ? 1 :
          (float)trackENR.Value / (float)((int)trackENR.MidpointSettings.Value);
        ENR newENRValue = new ENR(trackENR.Value, _project.ProjectInfo.ENR.Month,
        _project.ProjectInfo.ENR.Year);
        _project.ProjectInfo.ENR = newENRValue;
        dsProject.ResetCachedValues();
        gridCosts.Refresh();
      }
    }

    private void edtENR_ValueChanged(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("edtENR_ValueChanged()");
      if (!settingUpProject)
      {
        int testValue = (int)edtENR.Value;
        if (testValue < trackENR.MinValue || testValue > trackENR.MaxValue)
        {
          lblENR.Text = string.Format("out of range ({1}-{2})/ Base ENR: {0}",
          _project.PipeCoster.BaseENR, trackENR.MinValue, trackENR.MaxValue);
        }
        else
        {
          trackENR.Value = testValue;
          lblENR.Text = string.Format("/ Base ENR: {0}", _project.PipeCoster.BaseENR);
          dsProject.ResetCachedValues();
          gridCosts.Refresh();
        }
      }
    }

    private void toolbarManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("toolbarManager_ToolClick()");
      UpdateCurrentlyEditingRow();

      switch (e.Tool.Key)
      {
        case "Detach from pool": // ButtonTool
          HandleDetachFromPool();
          break;

        case "Link to item": // ButtonTool
          // Place code here
          break;

        case "Direct Construction Items": // PopupControlContainerTool
          // Place code here
          break;

        case "Add Cost Item Factor": // ButtonTool
          // Place code here
          break;

        case "Link Cost Item Factor": // ButtonTool
          SetupSelectCostItemFactorTree();
          break;

        case "Hide Add Items": // ButtonTool
          HideAddItemsFromCostGrid(
          (e.Tool as Infragistics.Win.UltraWinToolbars.StateButtonTool).Checked);
          break;

        case "Add Factor": // ButtonTool
          // Place code here
          break;

        case "Add Cost Item": // ButtonTool
          // Place code here
          break;

        case "Delete Cost Item Factor": // ButtonTool
          // Place code here
          break;

        case "Delete Factor": // ButtonTool
          // Place code here
          break;

        case "Delete Cost Item": // ButtonTool
          // Place code here
          break;

        case "Cost Factor Pool": // ButtonTool
          AppForm.ViewCostFactorPool();
          break;

        case "Cost Item Pool": // ButtonTool
          AppForm.ViewCostItemPool();
          break;

        case "Edit Info":
          AppForm.ViewProjectInfo();
          break;

        case "Print":
          AppForm.ViewReport();
          break;

        case "Add Other Direct Construction": // ButtonTool
          {
            if (gridCosts.ActiveRow == null)
            {
              MessageBox.Show("Click on an item within a focus area first before adding an other direct construction item");
              return;
            }
            _currentDirectConstructionCIF = EstimateOfRow(gridCosts.ActiveRow);
            if (_currentDirectConstructionCIF == null)
            {
              MessageBox.Show("Click on an item within a focus area first before adding an other direct construction item");
              return;
            }

            CostItemFactor pipeDCCIF = _currentDirectConstructionCIF.ChildCostItemFactor("Pipe direct construction");
            CostItemFactor icDCCIF = _currentDirectConstructionCIF.ChildCostItemFactor("Inflow control direct construction");
            ReadDirectConstructionItems();

            cmbApplyTo.Items.Clear();
            cmbApplyTo.Items.Add(pipeDCCIF,
            string.Format("Direct pipe construction costs for {0} ({1:C0})",
            _currentDirectConstructionCIF.Name, pipeDCCIF.Cost));
            cmbApplyTo.Items.Add(icDCCIF,
            string.Format("Direct inflow construction costs for {0} ({1:C0})",
            _currentDirectConstructionCIF.Name, icDCCIF.Cost));
            cmbApplyTo.SelectedItem = cmbApplyTo.Items[0];

            tabCosts.SelectedTab = tabCosts.Tabs["OtherDirectConstruction"];
          }
          break;

        case "Export Pipe Costs": // ButtonTool
          ExportPipeCosts();
          break;

        case "Export Detailed Costs":
          ExportDetailedCosts();
          break;
      }
    }

    private void CostsPage_VisibleChanged(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("CostsPage_VisibleChanged()");
      if (AppForm != null && Visible == true)
      {
        if (AppForm.toolbarManager.ActiveMdiChildManager != null)
        {
          AppForm.toolbarManager.ActiveMdiChildManager = null;
          AppForm.toolbarManager.Ribbon.Tabs.RemoveAt(AppForm.toolbarManager.Ribbon.Tabs.Count - 1);
        } // if
        AppForm.toolbarManager.ActiveMdiChildManager = this.toolbarManager;
        AppForm.toolbarManager.Ribbon.SelectedTab =
        AppForm.toolbarManager.Ribbon.Tabs[AppForm.toolbarManager.Ribbon.Tabs.Count - 1];
      } // if

      if (dsProject.Rows.Count == 0)
        SetupProjectDataSource();

      if (Visible && _Settings != null)
      {
        edtENR.Enabled = _Settings.AllowENRCCIChange;
        edtENR.ReadOnly = !_Settings.AllowENRCCIChange;
      } // if
    }

    private void dsOtherDirectConstructionFactors_CellDataRequested(object sender, CellDataRequestedEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("dsOtherDirectConstructionFactors_CellDataUpdated()");
      if (_currentDirectConstructionCIF != null)
      {
        int currentRow = e.Row.Index;
        switch (e.Column.Key)
        {
          case "Selected":
            e.Data = _directConstructionSelection[currentRow].Selected;
            break;
          case "Item":
            e.Data = _directConstructionSelection[currentRow].Name;
            break;
          case "Factor":
            e.Data = _directConstructionSelection[currentRow].Factor;
            break;
          case "Cost":
            CostItemFactor pipeDCCIF = cmbApplyTo.SelectedItem.DataValue as CostItemFactor;
            e.Data = (double)pipeDCCIF.Cost * _directConstructionSelection[currentRow].Factor;
            break;
        }
      }
    }

    private void dsOtherDirectConstructionFactors_CellDataUpdated(object sender, CellDataUpdatedEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("dsOtherDirectConstructionFactors_CellDataUpdated");
      if (_currentDirectConstructionCIF != null)
      {
        int currentRow = e.Row.Index;
        switch (e.Column.Key)
        {
          case "Selected":
            _directConstructionSelection[currentRow].Selected = Convert.ToBoolean(e.NewValue);
            break;
          case "Item":
            _directConstructionSelection[currentRow].Name = Convert.ToString(e.NewValue);
            break;
          case "Factor":
            _directConstructionSelection[currentRow].Factor = (float)Convert.ToDouble(e.NewValue);
            break;
        }
      } // if
    }

    private void dsOtherDirectConstructionFactors_RowEndEdit(object sender, RowEndEditEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("dsOtherDirectConstructionFactors_RowEndEdit()");
      dsOtherDirectConstructionFactors.ResetCachedValues();
      gridOtherDirectConstruction.Refresh();
    }

    private void btnOKOtherDirectConstruction_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("btnOKSelectCostItemFactor_Click()");
      CostItemFactor estimateCostItemFactor = _currentDirectConstructionCIF;
      CostItemFactor directCostItemFactor = cmbApplyTo.SelectedItem.DataValue as CostItemFactor;
      CostItemFactor otherDCCostItemFactor = estimateCostItemFactor.ChildCostItemFactor("Other direct construction");

      CostItemFactor detailOtherDCCostItemFactor = null;
      string otherDCCIFName = String.Format("Other {0}", directCostItemFactor.Name);
      if (otherDCCostItemFactor.ChildCostItemFactor(otherDCCIFName) == null)
      {
        CostItemFactor newOtherDCCostItemFactor = new CostItemFactor(otherDCCIFName);
        _project.AddCostItemFactorToPool(newOtherDCCostItemFactor);
        otherDCCostItemFactor.AddCostItemFactor(newOtherDCCostItemFactor);
        newOtherDCCostItemFactor.AddAsParent(otherDCCostItemFactor);
      } // if
      detailOtherDCCostItemFactor = otherDCCostItemFactor.ChildCostItemFactor(otherDCCIFName);

      if (!detailOtherDCCostItemFactor.IsAParentOf(directCostItemFactor))
        detailOtherDCCostItemFactor.AddCostItemFactor(directCostItemFactor);

      // Add the selected cost factors
      foreach (DirectConstructionSelection item in _directConstructionSelection)
      {
        if (item.Selected)
        {
          string itemFactorName = item.Name;
          CostFactor otherDCCIFFactor = 
            new CostFactor(itemFactorName, item.Factor, CostFactorType.IndirectSimple);
          _project.AddFactorToPool(otherDCCIFFactor);
          if (detailOtherDCCostItemFactor.CostFactorIndex(otherDCCIFFactor) == -1)
            detailOtherDCCostItemFactor.AddFactor(otherDCCIFFactor);
        } // if
      } // foreach  (item)

      // Reset the datasource
      UltraDataRowsCollection estimateRows = dsProject.Rows[0].GetChildRows("CostItemFactor1");
      UltraDataRow estimateRow = null;
      foreach (UltraDataRow row in estimateRows)
      {
        if (row.GetCellValue("Name", true).ToString() == estimateCostItemFactor.Name)
        {
          estimateRow = row;
          break;
        }
      } // foreach  (row)
      if (estimateRow != null)
      {
        UltraDataRowsCollection estimateDCRows = estimateRow.GetChildRows("CostItemFactor2");
        UltraDataRow estimateOtherDCRow = null;
        foreach (UltraDataRow row in estimateDCRows)
        {
          if (row.GetCellValue("Name", true).ToString() == "Other direct construction")
          {
            estimateOtherDCRow = row;
            break;
          } // if
        }

        if (estimateOtherDCRow != null)
        {
          estimateOtherDCRow.GetChildRows("CostItemFactor3").SetCount(otherDCCostItemFactor.ChildCostItemFactorCount);
          estimateOtherDCRow.GetChildRows("Factors3").SetCount(otherDCCostItemFactor.CostFactorsCount);

          UltraDataRowsCollection otherDCRows = estimateOtherDCRow.GetChildRows("CostItemFactor3");
          for (int i = 0; i < otherDCRows.Count; i++)
          {
            UltraDataRow row = otherDCRows[i];
            CostItemFactor childCIF = otherDCCostItemFactor.ChildCostItemFactor(i);
            row.GetChildRows("CostItemFactor4").SetCount(childCIF.ChildCostItemFactorCount);
            row.GetChildRows("Factors4").SetCount(childCIF.CostFactorsCount);
          }
        }
      } // if

      dsProject.ResetCachedValues();
      gridCosts.Refresh();

      tabCosts.SelectedTab = tabCosts.Tabs["Costs"];
    }

    private void btnCancelOtherDirectConstruction_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("btnCancelOtherDirectConstruction_Click()");
      tabCosts.SelectedTab = tabCosts.Tabs["Costs"];
    }

    private void btnOKSelectCostItemFactor_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("btnOKSelectCostItemFactor_Click()");
      if (treeCostItemFactor.SelectedNodes.Count == 0)
        return;

      CostItemFactor selectedCostItemFactor = treeCostItemFactor.SelectedNodes[0].Tag as CostItemFactor;

      CostItemFactor currentCostItemFactor = GetCostItemFactor(gridCosts.ActiveRow);
      if (!currentCostItemFactor.IsAParentOf(selectedCostItemFactor))
      {
        UltraDataRow currentDataRow = gridCosts.ActiveRow.ListObject as UltraDataRow;
        string currentDataRowBand = BandOfRow(currentDataRow);

        DialogResult replaceCurrentCIF;
        if (currentDataRowBand.StartsWith("CostItemFactor"))
        {
          replaceCurrentCIF = MessageBox.Show("Replace the current cost item factor?", "Replace", MessageBoxButtons.YesNoCancel);

          currentDataRow = null;
          switch (replaceCurrentCIF)
          {
            case DialogResult.Yes:
              currentDataRow = gridCosts.ActiveRow.ListObject as UltraDataRow;
              int currentDepth = DepthOfRow(currentDataRow);
              string currentBand = string.Format("CostItemFactor{0}", currentDepth);
              int numCurrentBandRows = currentDataRow.ParentRow.GetChildRows(currentBand).Count;
              UltraDataRow parentRow = currentDataRow.ParentRow;
              parentRow.GetChildRows(currentBand).Remove(currentDataRow, true);
              parentRow.GetChildRows(currentBand).SetCount(numCurrentBandRows - 1);
              currentDataRow = parentRow;
              currentCostItemFactor = GetCostItemFactor(currentDataRow);
              break;
            case DialogResult.No:
              break;
            case DialogResult.Cancel:
              return;
          }
        }

        currentCostItemFactor.AddCostItemFactor(selectedCostItemFactor);

        if (currentDataRow == null)
          currentDataRow = gridCosts.ActiveRow.ListObject as UltraDataRow;

        UltraDataRow parentDataRow;
        if (currentDataRowBand.StartsWith("CostItemFactor"))
          parentDataRow = currentDataRow;
        else
          parentDataRow = currentDataRow.ParentRow;

        int parentBandDepth = DepthOfRow(parentDataRow);
        UltraDataRowsCollection cifRowsOfParent = parentDataRow.GetChildRows(string.Format("CostItemFactor{0}", parentBandDepth + 1));
        cifRowsOfParent.SetCount(cifRowsOfParent.Count + 1);

        dsProject.ResetCachedValues();
        gridCosts.Refresh();
      }
      else
      {
        MessageBox.Show(string.Format("Current cost item factor {0}-{1} is a " +
        "parent of the selected cost item factor {2}-{3} and cannot be added.",
        currentCostItemFactor.ID, currentCostItemFactor.Name,
        selectedCostItemFactor.ID, selectedCostItemFactor.Name), "Cannot assign selected cost item factor");
      }

      tabCosts.SelectedTab = tabCosts.Tabs["Costs"];
    }

    private void btnCancelSelectCostItemFactor_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("btnCancelSelectCostItemFactor_Click()");
      tabCosts.SelectedTab = tabCosts.Tabs["Costs"];
    }

    private void cmbApplyTo_ValueChanged(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("cmbApplyTo_ValueChanged");
      dsOtherDirectConstructionFactors.ResetCachedValues();
      gridOtherDirectConstruction.Refresh();
    }

    private void gridCosts_AfterRowExpanded(object sender, RowEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("gridCosts_AfterRowExpanded()");
      string bandName = BandOfRow(e.Row);
      int depth = DepthOfRow(e.Row);
      switch (bandName)
      {
        case "CostItemFactor":
          UltraGridBand itemBand =
          gridCosts.DisplayLayout.Bands[String.Format("CostItem{0}", depth + 1)];
          CostItemFactor currentCIF = GetCostItemFactor(e.Row);
          if (itemBand != null)
            if (currentCIF.CostItem != null)
              itemBand.Override.AllowAddNew = AllowAddNew.No;
            else
              itemBand.Override.AllowAddNew = AllowAddNew.Default;
          break;
      }
    }

    private void gridCosts_AfterRowInsert(object sender, RowEventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("gridCosts_AfterRowInsert()");
      string bandName = BandOfRow(e.Row);
      int depth = DepthOfRow(e.Row);
      switch (bandName)
      {
        case "CostItem":
          UltraGridBand itemBand =
          gridCosts.DisplayLayout.Bands[String.Format("CostItem{0}", depth)];
          CostItemFactor currentCIF = GetCostItemFactor(e.Row);
          if (itemBand != null)
            if (currentCIF.CostItem != null)
              itemBand.Override.AllowAddNew = AllowAddNew.No;
            else
              itemBand.Override.AllowAddNew = AllowAddNew.Default;
          break;
      }
    }

    private void calENRDate_ValueChanged(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("calENRDate_ValueChanged()");
      DateTime newDateTime = (DateTime)calENRDate.Value;
      ENR newENRValue = new ENR(trackENR.Value, newDateTime.Month,
      newDateTime.Year);
      _project.ProjectInfo.ENR = newENRValue;
    }
  }
}

