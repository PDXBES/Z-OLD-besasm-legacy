using ESRI.ArcGIS.Utility;

using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Modeling.ModelResults;
using System.Collections;
using System.Collections.Specialized;
using SystemsAnalysis.Utils.Events;
using SystemsAnalysis.DataAccess;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
  public class ModelResultsReport : ReportBase
  {
    private Dictionary<int, LinkHydraulics> _linkResults;
    private Dictionary<int, NodeHydraulics> _nodeResults;
    private Dictionary<int, DscHydraulics> _dscResults;
    private ModelCatalogDataSet _modelCatalogDS;

    private Links links;
    private Nodes nodes;
    private Dscs dscs;

    /// <summary>
    /// Creates a ModelResultsReport from a collections of Links, Nodes and Dscs
    /// </summary>
    /// <param name="links">A Links collection to report on</param>
    /// <param name="nodes">A Nodes collection to report on</param>
    /// <param name="dscs">A Dscs collection to report on</param>
    public ModelResultsReport(Links links, Nodes nodes, Dscs dscs)
    //: base(links, nodes, dscs)
    {
      this.links = links;
      this.nodes = nodes;
      this.dscs = dscs;
    }

    private LinkHydraulics LinkResults(int scenarioID)
    {
      if (this._linkResults == null)
      {
        _linkResults = new Dictionary<int, LinkHydraulics>();
      }
      if (!this._linkResults.ContainsKey(scenarioID))
      {
        this.InitLinkResults(scenarioID);
      }
      return _linkResults[scenarioID];
    }
    private void InitLinkResults(int scenarioID)
    {
      //For convenience unique results objects are created for each scenario 
      //and stored in a list dictionary accessible via the scenarioID. This 
      //allows loading/accessing all model scenarios by simply iterating
      //through the dictionary                
      this.OnStatusChanged(new StatusChangedArgs("Creating link results objects for scenario " + scenarioID + "."));
      _linkResults.Add(scenarioID, new LinkHydraulics(links, scenarioID));
    }
    private NodeHydraulics NodeResults(int scenarioID)
    {
      if (this._nodeResults == null)
      {
        _nodeResults = new Dictionary<int, NodeHydraulics>();
      }
      if (!this._nodeResults.ContainsKey(scenarioID))
      {
        this.InitNodeResults(scenarioID);
      }
      return _nodeResults[scenarioID];
    }
    private void InitNodeResults(int scenarioID)
    {
      //For convenience unique results objects are created for each scenario 
      //and stored in a list dictionary accessible via the scenarioID. This 
      //allows loading/accessing all model scenarios by simply iterating
      //through the dictionary                                    
      this.OnStatusChanged(new StatusChangedArgs("Creating node results objects for scenario " + scenarioID + "'."));
      _nodeResults.Add(scenarioID, new NodeHydraulics(nodes, scenarioID));
    }
    private DscHydraulics DscResults(int scenarioID)
    {
      if (this._dscResults == null)
      {
        _dscResults = new Dictionary<int, DscHydraulics>();
      }
      if (!this._dscResults.ContainsKey(scenarioID))
      {
        this.InitDscResults(scenarioID);
      }
      return _dscResults[scenarioID];
    }
    private void InitDscResults(int scenarioID)
    {
      //For convenience unique results objects are created for each scenario 
      //and stored in a list dictionary accessible via the scenarioID. This 
      //allows loading/accessing all model scenarios by simply iterating
      //through the dictionary                        
      this.OnStatusChanged(new StatusChangedArgs("Creating dsc results objects for scenario " + scenarioID + "'."));
      _dscResults.Add(scenarioID, new DscHydraulics(dscs, scenarioID));
    }

    private ModelCatalogDataSet ModelCatalogDS
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        if (this._modelCatalogDS == null)
        {
          this.InitModelCatalogDS();
        }
        return this._modelCatalogDS;
      }
    }
    private void InitModelCatalogDS()
    {
      this.OnStatusChanged(new StatusChangedArgs("Loading the ModelCatalog DataSet."));
      _modelCatalogDS = new DataAccess.ModelCatalogDataSet();
      DataAccess.ModelCatalogDataSetTableAdapters.StormCatalogTableAdapter scTA;
      scTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.StormCatalogTableAdapter();
      DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter mcTA;
      mcTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter();
      DataAccess.ModelCatalogDataSetTableAdapters.ModelScenarioTableAdapter msTA;
      msTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.ModelScenarioTableAdapter();
      scTA.Fill(_modelCatalogDS.StormCatalog);
      msTA.Fill(_modelCatalogDS.ModelScenario);
      mcTA.Fill(_modelCatalogDS.ModelCatalog);
    }

    public int PipeSurchargeCount(IDictionary<string, Parameter> parameters)
    {
      bool filteredByMinSurchargeDepth;
      double minSurchargeDepth = 0;
      filteredByMinSurchargeDepth = parameters.ContainsKey("MinSurchargeDepth");
      if (filteredByMinSurchargeDepth)
      {
        minSurchargeDepth = parameters["MinSurchargeDepth"].ValueAsDouble;
      }

      bool filteredByMaxSurchargeDepth;
      double maxSurchargeDepth = 0;
      filteredByMaxSurchargeDepth = parameters.ContainsKey("MaxSurchargeDepth");
      if (filteredByMaxSurchargeDepth)
      {
        maxSurchargeDepth = parameters["MaxSurchargeDepth"].ValueAsDouble;
      }

      bool filteredByMinSurchargeTime;
      double minSurchargeTime = 0;
      filteredByMinSurchargeTime = parameters.ContainsKey("MinSurchargeTime");
      if (filteredByMinSurchargeTime)
      {
        minSurchargeTime = parameters["MinSurchargeTime"].ValueAsDouble;
      }

      bool filteredByMaxSurchargeTime;
      double maxSurchargeTime = 0;
      filteredByMaxSurchargeTime = parameters.ContainsKey("MaxSurchargeTime");
      if (filteredByMaxSurchargeTime)
      {
        maxSurchargeTime = parameters["MaxSurchargeTime"].ValueAsDouble;
      }

      int count = 0;
      int scenarioID;
      scenarioID = parameters["ScenarioID"].ValueAsInt;
      LinkHydraulics linkResults = LinkResults(scenarioID);
      foreach (LinkHydraulic lh in linkResults.Values)
      {
        double surchargeDepth;
        double surchargeTime;
        double usSurchargeTime, dsSurchargeTime;
        surchargeDepth = lh.MaxSurcharge;
        NodeHydraulics nodeResults = NodeResults(scenarioID);
        if (nodeResults == null) { continue; }
        usSurchargeTime = nodeResults[lh.Link.USNodeName] == null ? 0 : nodeResults[lh.Link.USNodeName].SurchargeTime;
        dsSurchargeTime = nodeResults[lh.Link.DSNodeName] == null ? 0 : nodeResults[lh.Link.DSNodeName].SurchargeTime;
        surchargeTime = Math.Max(usSurchargeTime, dsSurchargeTime);

        if ((!filteredByMinSurchargeDepth || surchargeDepth >= minSurchargeDepth) &&
        (!filteredByMaxSurchargeDepth || surchargeDepth <= maxSurchargeDepth) &&
        (!filteredByMinSurchargeTime || surchargeTime >= minSurchargeTime) &&
        (!filteredByMaxSurchargeTime || surchargeTime <= maxSurchargeTime) &&
        lh.Link.IsGravityFlow)
        {
          count++;
        }
      }
      return count;
    }
    public double PipeSurchargePercent(IDictionary<string, Parameter> parameters)
    {
      int scenarioID;
      scenarioID = parameters["ScenarioID"].ValueAsInt;
      LinkHydraulics linkResults = LinkResults(scenarioID);

      int count = 0;
      foreach (LinkHydraulic lh in linkResults.Values)
      {
        if (lh.Link.IsGravityFlow)
        {
          count++;
        }
      }
      return (double)PipeSurchargeCount(parameters) / count * 100;
    }
    public double PipeSurchargeLength(IDictionary<string, Parameter> parameters)
    {
      bool filteredByMinSurchargeDepth;
      double minSurchargeDepth = 0;
      filteredByMinSurchargeDepth = parameters.ContainsKey("MinSurchargeDepth");
      if (filteredByMinSurchargeDepth)
      {
        minSurchargeDepth = parameters["MinSurchargeDepth"].ValueAsDouble;
      }

      bool filteredByMaxSurchargeDepth;
      double maxSurchargeDepth = 0;
      filteredByMaxSurchargeDepth = parameters.ContainsKey("MaxSurchargeDepth");
      if (filteredByMaxSurchargeDepth)
      {
        maxSurchargeDepth = parameters["MaxSurchargeDepth"].ValueAsDouble;
      }

      bool filteredByMinSurchargeTime;
      double minSurchargeTime = 0;
      filteredByMinSurchargeTime = parameters.ContainsKey("MinSurchargeTime");
      if (filteredByMinSurchargeTime)
      {
        minSurchargeTime = parameters["MinSurchargeTime"].ValueAsDouble;
      }

      bool filteredByMaxSurchargeTime;
      double maxSurchargeTime = 0;
      filteredByMaxSurchargeTime = parameters.ContainsKey("MaxSurchargeTime");
      if (filteredByMaxSurchargeTime)
      {
        maxSurchargeTime = parameters["MaxSurchargeTime"].ValueAsDouble;
      }

      double length = 0;
      int scenarioID;
      scenarioID = parameters["ScenarioID"].ValueAsInt;
      LinkHydraulics linkResults = LinkResults(scenarioID);
      foreach (LinkHydraulic lh in linkResults.Values)
      {
        double surchargeDepth;
        double surchargeTime;
        double usSurchargeTime, dsSurchargeTime;
        surchargeDepth = lh.MaxSurcharge;
        NodeHydraulics nodeResults = NodeResults(scenarioID);
        if (nodeResults == null) { continue; }
        usSurchargeTime = nodeResults[lh.Link.USNodeName] == null ? 0 : nodeResults[lh.Link.USNodeName].SurchargeTime;
        dsSurchargeTime = nodeResults[lh.Link.DSNodeName] == null ? 0 : nodeResults[lh.Link.DSNodeName].SurchargeTime;
        surchargeTime = Math.Max(usSurchargeTime, dsSurchargeTime);

        if ((!filteredByMinSurchargeDepth || surchargeDepth >= minSurchargeDepth) &&
        (!filteredByMaxSurchargeDepth || surchargeDepth <= maxSurchargeDepth) &&
        (!filteredByMinSurchargeTime || surchargeTime >= minSurchargeTime) &&
        (!filteredByMaxSurchargeTime || surchargeTime <= maxSurchargeTime) &&
        lh.Link.IsGravityFlow)
        {
          length += (lh.Link.Length / 5280.0);
        }
      }
      return length;
    }

    public string PipeSurchargeLengthCountSummary(IDictionary<string, Parameter> parameters)
    {
      return PipeSurchargeLength(parameters).ToString("N2") + " (" + PipeSurchargeCount(parameters).ToString("N0") + ")";
    }
    public double PipeSurchargeLengthPercent(IDictionary<string, Parameter> parameters)
    {
      int scenarioID;
      scenarioID = parameters["ScenarioID"].ValueAsInt;
      LinkHydraulics linkResults = LinkResults(scenarioID);

      double totalLength = 0;
      foreach (LinkHydraulic lh in linkResults.Values)
      {
        totalLength += lh.Link.Length / 5280.0;
      }
      return PipeSurchargeLength(parameters) / totalLength * 100;
    }

    public int QQRatioCount(IDictionary<string, Parameter> parameters)
    {
      bool filteredByMinQQRatio;
      double minQQRatio = 0;
      filteredByMinQQRatio = parameters.ContainsKey("MinQQRatio");
      if (filteredByMinQQRatio)
      {
        minQQRatio = parameters["MinQQRatio"].ValueAsDouble;
      }
      bool filteredByMaxQQRatio;
      double maxQQRatio = 0;
      filteredByMaxQQRatio = parameters.ContainsKey("MaxQQRatio");
      if (filteredByMaxQQRatio)
      {
        maxQQRatio = parameters["MaxQQRatio"].ValueAsDouble;
      }
      bool filteredByMinPipeSize;
      double minPipeSize = 0;
      filteredByMinPipeSize = parameters.ContainsKey("MinPipeSize");
      if (filteredByMinPipeSize)
      {
        minPipeSize = parameters["MinPipeSize"].ValueAsDouble;
      }
      bool filteredByMaxPipeSize;
      double maxPipeSize = 0;
      filteredByMaxPipeSize = parameters.ContainsKey("MaxPipeSize");
      if (filteredByMaxPipeSize)
      {
        maxPipeSize = parameters["MaxPipeSize"].ValueAsDouble;
      }

      int count = 0;
      int scenarioID;
      scenarioID = parameters["ScenarioID"].ValueAsInt;
      LinkHydraulics linkResults = LinkResults(scenarioID);
      foreach (LinkHydraulic lh in linkResults.Values)
      {
        double qqRatio;
        qqRatio = lh.QQRatio;
        double pipeSize;
        pipeSize = lh.Link.Diameter;

        if ((!filteredByMinQQRatio || qqRatio >= minQQRatio) &&
        (!filteredByMaxQQRatio || qqRatio <= maxQQRatio) &&
        (!filteredByMinPipeSize || pipeSize >= minPipeSize) &&
        (!filteredByMaxPipeSize || pipeSize <= maxPipeSize) &&
        lh.Link.IsGravityFlow)
        {
          count++;
        }
      }
      return count;
    }
    public double QQRatioPercent(IDictionary<string, Parameter> parameters)
    {
      int scenarioID;
      scenarioID = parameters["ScenarioID"].ValueAsInt;
      LinkHydraulics linkResults = LinkResults(scenarioID);

      int count = 0;
      foreach (LinkHydraulic lh in linkResults.Values)
      {
        if (lh.Link.IsGravityFlow)
        {
          count++;
        }
      }
      return (double)QQRatioCount(parameters) / count * 100;
    }
    public double QQRatioLength(IDictionary<string, Parameter> parameters)
    {
      bool filteredByMinQQRatio;
      double minQQRatio = 0;
      filteredByMinQQRatio = parameters.ContainsKey("MinQQRatio");
      if (filteredByMinQQRatio)
      {
        minQQRatio = parameters["MinQQRatio"].ValueAsDouble;
      }
      bool filteredByMaxQQRatio;
      double maxQQRatio = 0;
      filteredByMaxQQRatio = parameters.ContainsKey("MaxQQRatio");
      if (filteredByMaxQQRatio)
      {
        maxQQRatio = parameters["MaxQQRatio"].ValueAsDouble;
      }
      bool filteredByMinPipeSize;
      double minPipeSize = 0;
      filteredByMinPipeSize = parameters.ContainsKey("MinPipeSize");
      if (filteredByMinPipeSize)
      {
        minPipeSize = parameters["MinPipeSize"].ValueAsDouble;
      }
      bool filteredByMaxPipeSize;
      double maxPipeSize = 0;
      filteredByMaxPipeSize = parameters.ContainsKey("MaxPipeSize");
      if (filteredByMaxPipeSize)
      {
        maxPipeSize = parameters["MaxPipeSize"].ValueAsDouble;
      }

      double length = 0;
      int scenarioID;
      scenarioID = parameters["ScenarioID"].ValueAsInt;
      LinkHydraulics linkResults = LinkResults(scenarioID);
      foreach (LinkHydraulic lh in linkResults.Values)
      {
        double qqRatio;
        qqRatio = lh.QQRatio;
        double pipeSize;
        pipeSize = lh.Link.Diameter;

        if ((!filteredByMinQQRatio || qqRatio >= minQQRatio) &&
        (!filteredByMaxQQRatio || qqRatio <= maxQQRatio) &&
        (!filteredByMinPipeSize || pipeSize >= minPipeSize) &&
        (!filteredByMaxPipeSize || pipeSize <= maxPipeSize) &&
        lh.Link.IsGravityFlow)
        {
          length += (lh.Link.Length / 5280.0);
        }
      }
      return length;
    }
    public string QQRatioSummary(IDictionary<string, Parameter> parameters)
    {
      return QQRatioPercent(parameters).ToString("N1") + " (" + QQRatioCount(parameters).ToString("N0") + ")";      
    }
    public string QQRatioLengthCountSummary(IDictionary<string, Parameter> parameters)
    {
      return QQRatioLength(parameters).ToString("N2") + " (" + QQRatioCount(parameters).ToString("N0") + ")";
    }

    public int SewerBackupRiskCount(IDictionary<string, Parameter> parameters)
    {
      bool filteredByMinDeltaHGL;
      double minDeltaHGL = 0;
      filteredByMinDeltaHGL = parameters.ContainsKey("MinDeltaHGL");
      if (filteredByMinDeltaHGL)
      {
        minDeltaHGL = parameters["MinDeltaHGL"].ValueAsDouble;
      }

      bool filteredByMaxDeltaHGL;
      double maxDeltaHGL = 0;
      filteredByMaxDeltaHGL = parameters.ContainsKey("MaxDeltaHGL");
      if (filteredByMaxDeltaHGL)
      {
        maxDeltaHGL = parameters["MaxDeltaHGL"].ValueAsDouble;
      }


      int count = 0;
      int scenarioID;
      scenarioID = parameters["ScenarioID"].ValueAsInt;

      DscHydraulics dscHyd = DscResults(scenarioID);
      foreach (DscHydraulic dh in dscHyd.Values)
      {
        double deltaHGL = dh.DeltaHGL;
        if ((!filteredByMinDeltaHGL || deltaHGL >= minDeltaHGL) &&
        (!filteredByMaxDeltaHGL || deltaHGL <= maxDeltaHGL) &&
        dh.Surcharge > 0 &&
        !dh.Dsc.FalseBFRisk &&
        dh.Dsc.Sewerable != Types.Enumerators.Sewerable.Unsewerable)
        {
          count++;
        }
      }
      return count;

      /*DscHydraulics dscHyd;
      bool isFraction = parameters.ContainsKey("IsFraction") && parameters["IsFraction"].ValueAsBool;
      dscHyd = (DscHydraulics)DscResults[parameters["ScenarioID"].ValueAsInt];
      count = dscHyd.CountBySewerBackupRisk(
      parameters["MinValue"].ValueAsDouble,
      parameters["MaxValue"].ValueAsDouble);
      return isFraction
      ? count / dscs.Count * 100
      : count;*/
    }
    public double SewerBackupRiskPercent(IDictionary<string, Parameter> parameters)
    {
      return (double)SewerBackupRiskCount(parameters) / dscs.Count * 100;
    }
    public string SewerBackupRiskSummary(IDictionary<string, Parameter> parameters)
    {
      return SewerBackupRiskCount(parameters).ToString("N0") + " (" + SewerBackupRiskPercent(parameters).ToString("N0") + "%)";
    }

    public int SurfaceFloodingCount(IDictionary<string, Parameter> parameters)
    {
      int count = 0;
      bool filteredByMinFreeboard;
      double minFreeboard = 0;
      filteredByMinFreeboard = parameters.ContainsKey("MinFreeboard");
      if (filteredByMinFreeboard)
      {
        minFreeboard = parameters["MinFreeboard"].ValueAsDouble;
      }

      bool filteredByMaxFreeboard;
      double maxFreeboard = 0;
      filteredByMaxFreeboard = parameters.ContainsKey("MaxFreeboard");
      if (filteredByMaxFreeboard)
      {
        maxFreeboard = parameters["MaxFreeboard"].ValueAsDouble;
      }

      int scenarioID;
      scenarioID = parameters["ScenarioID"].ValueAsInt;

      NodeHydraulics nodeHyd = NodeResults(scenarioID);
      foreach (NodeHydraulic nh in nodeHyd.Values)
      {
        double freeboard = nh.Freeboard;
        if ((!filteredByMinFreeboard || freeboard >= minFreeboard) &&
        (!filteredByMaxFreeboard || freeboard <= maxFreeboard) &&
        nodes[nh.NodeName].NodeType != "FM" &&
        nodes[nh.NodeName].NodeType != "ANG" &&
        nodes[nh.NodeName].NodeType != "BLO" &&
        nodes[nh.NodeName].NodeType != "CSZ" &&
        nodes[nh.NodeName].NodeType != "SGVA" &&
        nodes[nh.NodeName].NodeType != "WYE" &&
        nh.Surcharge > 0)
        {
          count++;
        }
      }
      return count;
    }
    public string SurfaceFloodingSummary(IDictionary<string, Parameter> parameters)
    {
      int count = 0;
      int total = 0;

      count = SurfaceFloodingCount(parameters);
      total = nodes.Count;
      double fraction = 100.0 * count / total;
      string summary;
      summary = count.ToString("N0") + " (" + fraction.ToString("N1") + "%)";
      return summary;
    }

    public string PipeSurchargeTimeSummary(IDictionary<string, Parameter> parameters)
    {
      double length = 0;
      int count = 0;
      double min;
      double max;
      min = parameters["MinValue"].ValueAsDouble;
      max = parameters["MaxValue"].ValueAsDouble;

      NodeHydraulics nodeHyd;
      nodeHyd = (NodeHydraulics)NodeResults(parameters["ScenarioID"].ValueAsInt);

      foreach (Link l in links.Values)
      {
        double usSurchargeTime = 0;
        double dsSurchargeTime = 0;
        if (nodeHyd.Contains(l.USNodeName))
        {
          usSurchargeTime = nodeHyd[l.USNodeName].SurchargeTime;
        }
        if (nodeHyd.Contains(l.DSNodeName))
        {
          dsSurchargeTime = nodeHyd[l.DSNodeName].SurchargeTime;
        }
        if ((usSurchargeTime >= min && usSurchargeTime <= max) ||
        (dsSurchargeTime >= min && dsSurchargeTime <= max))
        {
          if (l.IsGravityFlow)
          {
            length += l.Length;
            count++;
          }
        }
      }
      return (length / 5280.0).ToString("N1") + " (" + count.ToString("N0") + ")";
    }
    public double PipeSurchargeTimeLength(IDictionary<string, Parameter> parameters)
    {
      double length = 0;

      double min;
      double max;
      min = parameters["MinValue"].ValueAsDouble;
      max = parameters["MaxValue"].ValueAsDouble;

      bool isFraction = parameters.ContainsKey("IsFraction") && parameters["IsFraction"].ValueAsBool;

      NodeHydraulics nodeHyd;
      nodeHyd = (NodeHydraulics)NodeResults(parameters["ScenarioID"].ValueAsInt);

      foreach (Link l in links.Values)
      {
        double usSurchargeTime = 0;
        double dsSurchargeTime = 0;
        if (nodeHyd.Contains(l.USNodeName))
        {
          usSurchargeTime = nodeHyd[l.USNodeName].SurchargeTime;
        }
        if (nodeHyd.Contains(l.DSNodeName))
        {
          dsSurchargeTime = nodeHyd[l.DSNodeName].SurchargeTime;
        }
        if ((usSurchargeTime >= min && usSurchargeTime <= max) ||
        (dsSurchargeTime >= min && dsSurchargeTime <= max))
        {
          if (l.IsGravityFlow)
          {
            length += l.Length;
          }
        }
      }
      return isFraction
      ? ((double)length / links.PipeSegmentLength()) * 100
      : length / 5280.0;
    }

  }
}
