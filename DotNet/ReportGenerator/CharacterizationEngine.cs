using System;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using SystemsAnalysis;
using SystemsAnalysis.Types;
using SystemsAnalysis.Tracer;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Modeling.ModelResults;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Utils.Events;
using SystemsAnalysis.Reporting.ReportLibraries;
using System.ComponentModel;

namespace SystemsAnalysis.Reporting
{
  /// <summary>
  /// A class to perform characterization queries for collections of Model
  /// components. Characterization operations are assumed to take place against
  /// component collections, not a Model or StartLinks/StopLinks collections.
  /// </summary>
  public class CharacterizationEngine
  {
    private ModelReport _modelReport;
    private ModelResultsReport _modelResultsReport;
    private FecReport _fecReport;
    private UicReport _uicReport;
    private PumpStationReport _psReport;
    private AlternativeReport _alternativeReport;

    private DoWorkEventArgs doWorkEventArgs;
    private BackgroundWorker bw;

    private Links _charLinks;
    private Nodes _charNodes;
    private Dscs _charDscs;

    private string reportTemplate;
    private Dictionary<string, ReportBase> reports;
    private Dictionary<string, ReportBase.ReportInfo> reportInfos;
    private string reportOutput;
    private string studyArea;

    private XmlDocument xmlDoc;

    /// <summary>
    /// Constructor for the CharacterizationEngine.
    /// </summary>
    /// <param name="charLinks">A Links collection to be characterized.</param>					
    public CharacterizationEngine(string reportTemplate)
    {
      this.OnStatusChanged(new StatusChangedArgs("Creating the characterization engine."));
      xmlDoc = new XmlDocument();
      xmlDoc.Load(reportTemplate);

      this.reportTemplate = reportTemplate;

      XmlNodeList xmlNodeList;
      xmlNodeList = xmlDoc.GetElementsByTagName("Function");
      reportInfos = new Dictionary<string, ReportBase.ReportInfo>();
      for (int i = 0; i < xmlNodeList.Count; i++)
      {
        try
        {
          string library;
          library = xmlNodeList[i].Attributes["library"].Value;

          if (reportInfos.ContainsKey(library))
          {
            continue;
          }
          System.Reflection.Assembly a;
          a = System.Reflection.Assembly.GetAssembly(typeof(ReportBase));

          Type t = a.GetType(a.GetName().Name + "." + library);

          if (t.GetNestedType("ReportInfo") == null)
          {
            t = t.BaseType;
          }
          Object o = Activator.CreateInstance(t.GetNestedType("ReportInfo"), new Object[] { library });
          ReportBase.ReportInfo reportInfo = (ReportBase.ReportInfo)o;
          reportInfos.Add(library, reportInfo);
        }
        catch (Exception ex)
        {
          return;
        }
      }
    }

    /// <summary>
    /// Generates an XML characterization report using the parameters specified in the constructor.
    /// </summary>
    /// <returns>The name of the .xml file containing the characterization information.</returns>
    public string Characterize(Links charLinks, string reportOutput, string studyArea, BackgroundWorker bw, DoWorkEventArgs doWorkEventArgs)
    {
      this.doWorkEventArgs = doWorkEventArgs;
      this.bw = bw;

      this.OnStatusChanged(new StatusChangedArgs("Beginning characterization process."));
      try
      {
        this._charLinks = charLinks;
        this.reportOutput = reportOutput;
        this.studyArea = studyArea;
        if (File.Exists(reportOutput))
        {
          File.Delete(reportOutput);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error creating Characterization Engine", ex);
      }

      try
      {
        reports = new Dictionary<string, ReportBase>();

        foreach (KeyValuePair<string, ReportBase.ReportInfo> kvp in reportInfos)
        {
          string library = kvp.Key;
          switch (library)
          {
            case "ModelReport":
              reports.Add(library, ModelReport);
              break;
            case "ModelResultsReport":
              reports.Add(library, ModelResultsReport);
              break;
            case "FecReport":
              reports.Add(library, FecReport);
              break;
            case "PumpStationReport":
              reports.Add(library, PSReport);
              break;
            case "AlternativeReport":
              reports.Add(library, AlternativeReport);
              break;
            case "UicReport":
              reports.Add(library, UicReport);
              break;
            default:
              break;
            //return "Unknown Library";
          }
          if (reportInfos[library].RequiresAuxilaryData)
          {
            reports[library].LoadAuxilaryData(ReportInfos[library].AuxilaryData);
          }
        }
        xmlDoc.GetElementsByTagName("ReportGenerator")[0].Attributes["studyArea"].Value = studyArea;

        expandMultiTables();
        expandMultiRows();
        parseEmbeddedFunctions();        

        xmlDoc.Save(reportOutput);
      }
      catch (Exception ex)
      {
        this.OnStatusChanged(new StatusChangedArgs(ex.Message, StatusChangeType.Error));
      }
      return reportOutput;
    }
    
    public Dictionary<string, ReportBase.ReportInfo> ReportInfos
    {
      get
      {
        return reportInfos;
      }
    }

    #region Accessor classes
    /* These accessors and initalization methods are for characterization classes
* that may not be needed by every characterization report. The characterization
* classes here should always be accessed through their property rather than via the
* object directly.  By accessing the classes this way they are only created if they
* are needed, saving time loading data that is not used */
    private ModelReport ModelReport
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        if (this._modelReport == null)
        {
          this.InitModelReport();
        }
        return this._modelReport;
      }
    }
    private void InitModelReport()
    {
      this.OnStatusChanged(new StatusChangedArgs("Creating Model Report."));
      this._modelReport = new ModelReport(CharLinks, CharNodes, CharDscs);
      this._modelReport.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
    }

    private ModelResultsReport ModelResultsReport
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        if (this._modelResultsReport == null)
        {
          this.InitModelResultsReport();
        }
        return this._modelResultsReport;
      }
    }
    private void InitModelResultsReport()
    {
      this.OnStatusChanged(new StatusChangedArgs("Creating Model Results Report."));
      this._modelResultsReport = new ModelResultsReport(CharLinks, CharNodes, CharDscs);
      this._modelResultsReport.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
    }

    private FecReport FecReport
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        if (this._fecReport == null)
        {
          this.InitFecReport();
        }
        return this._fecReport;
      }
    }
    private void InitFecReport()
    {
      this.OnStatusChanged(new StatusChangedArgs("Creating FEC Summary Report."));
      this._fecReport = new FecReport(CharLinks, CharNodes, CharDscs);
      this._fecReport.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
    }

    private UicReport UicReport
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        if (this._uicReport == null)
        {
          this.InitUicReport();
        }
        return this._uicReport;
      }
    }
    private void InitUicReport()
    {
      this.OnStatusChanged(new StatusChangedArgs("Creating UIC Summary Report."));
      this._uicReport = new UicReport(CharLinks, CharNodes);
      this._uicReport.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
    }

    private PumpStationReport PSReport
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        if (this._psReport == null)
        {
          this.InitPSReport();
        }
        return this._psReport;
      }
    }
    private void InitPSReport()
    {
      this.OnStatusChanged(new StatusChangedArgs("Creating PS Summary Report."));
      this._psReport = new PumpStationReport(CharLinks, CharNodes);
      this._psReport.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
    }

    private AlternativeReport AlternativeReport
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        if (this._alternativeReport == null)
        {
          this.InitAlternativeReport();
        }
        return this._alternativeReport;
      }
    }
    private void InitAlternativeReport()
    {
      this.OnStatusChanged(new StatusChangedArgs("Creating Alternative Summary Report."));
      this._alternativeReport = new AlternativeReport();
      this._alternativeReport.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
    }

    private Links CharLinks
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        return this._charLinks;
      }
    }
    private Nodes CharNodes
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        if (this._charNodes == null)
        {
          this.InitCharNodes();
        }
        return this._charNodes;
      }
    }
    private void InitCharNodes()
    {
      this.OnStatusChanged(new StatusChangedArgs("Creating Node objects."));
      this._charNodes = new Nodes();
      this._charNodes.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
      this._charNodes.LoadFromMaster(CharLinks);
    }
    private Dscs CharDscs
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        if (this._charDscs == null)
        {
          this.InitCharDscs();
        }
        return this._charDscs;
      }
    }
    private void InitCharDscs()
    {
      this.OnStatusChanged(new StatusChangedArgs("Creating Dsc objects."));
      this._charDscs = new Dscs();
      this._charDscs.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
      this._charDscs.LoadFromMaster(CharLinks);
    }

    #endregion

    private void parseEmbeddedFunctions()
    {
      System.Xml.XmlNodeList xmlNodeList;

      xmlNodeList = xmlDoc.GetElementsByTagName("Cell");
      int i = 1;
      foreach (XmlNode xmlNode in xmlNodeList)
      {
        if (xmlNode.Attributes["cellType"] == null)
        {
          StatusChanged(new StatusChangedArgs("Cell " + i + " missing 'cellType' attribute", StatusChangeType.Warning));
          continue;
        }
        if (xmlNode.Attributes["cellType"].Value != "FUNCTION")
        {
          continue;
        }
        string functionValue;
        functionValue = this.parseEmbeddedFunction(xmlNode.InnerXml);

        while (xmlNode.ChildNodes.Count != 0)
        {
          xmlNode.RemoveChild(xmlNode.FirstChild);
        }
        xmlNode.Attributes["cellType"].Value = "FUNCTION";
        xmlNode.Attributes.Append(xmlDoc.CreateAttribute("data"));
        xmlNode.Attributes["data"].Value = functionValue;

        i++;
      }
    }
    /// <summary>
    /// Returns the results of the embedded function as a string
    /// </summary>
    /// <param name="xmlFragment">An xml fragment containing the functionName, the library and any parameters</param>
    /// <returns>The results of the embedded function</returns>
    private string parseEmbeddedFunction(string xmlFragment)
    {
      string functionName = "";
      string library = "";
      string formatString = "";
      string suffix = "";
      string defaultValue = "";

      IDictionary<string, ReportBase.Parameter> parameters = new Dictionary<string, ReportBase.Parameter>();

      XmlReader functionReader = XmlReader.Create(new StringReader(xmlFragment));
      //Read raw parameters as objects into ListDictionary rawParams
      while (functionReader.Read())
      {
        if (functionReader.NodeType == System.Xml.XmlNodeType.EndElement)
        {
          continue;
        }
        switch (functionReader.Name)
        {
          case "Function":
            functionName = functionReader.GetAttribute("functionName");
            library = functionReader.GetAttribute("library");
            formatString = functionReader.GetAttribute("format") == null ? String.Empty : functionReader.GetAttribute("format");
            suffix = functionReader.GetAttribute("suffix") == null ? String.Empty : functionReader.GetAttribute("suffix");
            defaultValue = functionReader.GetAttribute("defaultValue") == null ? String.Empty : functionReader.GetAttribute("defaultValue");
            break;
          case "Parameter":
            string parameterName;
            string parameterValue;
            parameterName = functionReader.GetAttribute("parameterName");
            parameterValue = functionReader.GetAttribute("parameterValue");
            parameters.Add(parameterName, new ReportBase.Parameter(parameterName, parameterValue));
            break;
        }
      }
      try
      {
        return reports[library].EvaluateFunction(functionName, parameters, formatString) + suffix;
      }
      catch (Exception ex)
      {
        if (defaultValue != String.Empty)
        {
          return defaultValue;
        }

        //If any of the embedded functions was invalid, had improper parameters,
        //or simply failed, write "PARSE ERROR" and keep move onto the next.								
        this.OnStatusChanged(new StatusChangedArgs("Parse embedded XML function error! " + ex.Message));
        this.OnStatusChanged(new StatusChangedArgs("Function name: '" + functionName + "'. Parameters:"));

        foreach (KeyValuePair<string, ReportBase.Parameter> kvp in parameters)
        {
          ReportBase.Parameter p = kvp.Value;
          this.OnStatusChanged(new StatusChangedArgs("	" + (string)p.Name + "=" + p.Value));
        }
        return "Parse Error";
      }
    }

    private void expandMultiRows()
    {
      System.Xml.XmlNodeList xmlNodeList;
      xmlNodeList = xmlDoc.GetElementsByTagName("Row");

      foreach (XmlNode xmlNode in xmlNodeList)
      {
        try
        {
          if (xmlNode.Attributes["isMultiRow"] == null || xmlNode.Attributes["isMultiRow"].Value != "true")
          {
            continue;
          }

          XmlNode[] multiRowResultsNodes = expandMultiRow(xmlNode);

          foreach (XmlNode newNode in multiRowResultsNodes)
          {
            xmlNode.ParentNode.InsertBefore(newNode, xmlNode);
          }
          xmlNode.ParentNode.RemoveChild(xmlNode);
          expandMultiRows();
          break;
        }
        catch (Exception ex)
        {
          this.OnStatusChanged(new StatusChangedArgs("Error expanding MultiRow!"));
          this.OnStatusChanged(new StatusChangedArgs(ex.Message));
        }
      }
    }
    private XmlNode[] expandMultiRow(XmlNode xmlNode)
    {
      string multiRowKey = xmlNode.Attributes["multiRowKey"].Value;

      IList multiRowKeyList;
      //ArrayList sortedMultiRowKeyList;

      if (xmlNode.Attributes["multiRowFilterName"] == null)
      {
        multiRowKeyList = getMultiRowKeyList(multiRowKey);
      }
      else
      {
        string multiRowFilterName, multiRowFilterValue;
        multiRowFilterName = xmlNode.Attributes["multiRowFilterName"].Value;
        multiRowFilterValue = xmlNode.Attributes["multiRowFilterValue"] == null ? "" : xmlNode.Attributes["multiRowFilterValue"].Value;
        multiRowKeyList = getFilteredMultiRowKeyList(multiRowKey, multiRowFilterName, multiRowFilterValue);
      }
      //sortedMultiRowKeyList = new ArrayList(multiRowKeyList);
      //sortedMultiRowKeyList.Sort();

      xmlNode.Attributes.RemoveAll(); //Remove the isMultiRow and multiRowKey attributes

      XmlNode[] rowXmlNodes = new XmlNode[multiRowKeyList.Count];

      //XmlNode rowXmlNode = xmlNode;
      for (int i = 0; i < multiRowKeyList.Count; i++)
      {
        rowXmlNodes[i] = xmlNode.Clone();
        rowXmlNodes[i].InnerXml = rowXmlNodes[i].InnerXml.Replace("@" + multiRowKey, Convert.ToString(multiRowKeyList[i]));
      }

      return rowXmlNodes;
    }
    private IList getMultiRowKeyList(string multiRowKey)
    {
      int[] multiRowKeyList;
      int i = 0;
      switch (multiRowKey)
      {
        case "FECID":
          return (IList)FecReport.FecIDList();
        case "FocusArea":
          return AlternativeReport.FocusAreaList();
        case "PumpStationID":
          return PSReport.PumpStationIDList;
        case "DiversionID":
        default:
          multiRowKeyList = new int[0];
          return (IList)multiRowKeyList;
      }
    }
    private int[] getFilteredMultiRowKeyList(string multiRowKey, string multiRowFilterName, string multiRowFilterValue)
    {
      int[] multiRowKeyList;
      int i = 0;
      switch (multiRowKey)
      {
        case "WetWellIndex":
          return PSReport.WetWellIndexList(Convert.ToInt16(multiRowFilterValue));
        case "ForceMainIndex":
          return PSReport.ForceMainIndexList(Convert.ToInt16(multiRowFilterValue));
        case "PumpIndex":
          return PSReport.PumpIndexList(Convert.ToInt16(multiRowFilterValue));
        default:
          multiRowKeyList = new int[0];
          return multiRowKeyList;
      }
    }

    private void expandMultiTables()
    {
      System.Xml.XmlNodeList xmlNodeList;
      xmlNodeList = xmlDoc.GetElementsByTagName("CharacterizationTable");

      foreach (XmlNode xmlNode in xmlNodeList)
      {
        try
        {
          if (xmlNode.Attributes["isMultiTable"] == null || xmlNode.Attributes["isMultiTable"].Value != "true")
          {
            continue;
          }

          XmlNode[] multiTableResultsNodes = expandMultiTable(xmlNode);

          char tableID;
          tableID = multiTableResultsNodes.Length == 1 ? ' ' : 'a';
          foreach (XmlNode newNode in multiTableResultsNodes)
          {
            newNode.Attributes["isMultiTable"].RemoveAll();
            newNode.Attributes["multiTableKey"].RemoveAll();
            newNode.Attributes["tableNumber"].Value += tableID++;
            xmlNode.ParentNode.InsertBefore(newNode, xmlNode);
          }
          xmlNode.ParentNode.RemoveChild(xmlNode);
          expandMultiTables();
          break;
        }
        catch (Exception ex)
        {
          this.OnStatusChanged(new StatusChangedArgs("Error expanding MultiTable"));
          this.OnStatusChanged(new StatusChangedArgs(ex.Message));
        }
      }
    }
    private XmlNode[] expandMultiTable(XmlNode xmlNode)
    {
      string multiTableKey = xmlNode.Attributes["multiTableKey"].Value;
      //xmlNode.Attributes.RemoveAll(); //Remove the isMultiRow and multiRowKey attributes
      int[] multiTableKeyList = getMultiTableKeyList(multiTableKey);

      XmlNode[] tableXmlNodes = new XmlNode[multiTableKeyList.Length];

      //XmlNode rowXmlNode = xmlNode;
      for (int i = 0; i < multiTableKeyList.Length; i++)
      {
        tableXmlNodes[i] = xmlNode.Clone();
        tableXmlNodes[i].InnerXml = tableXmlNodes[i].InnerXml.Replace("@" + multiTableKey, Convert.ToString(multiTableKeyList[i]));
      }

      return tableXmlNodes;
    }
    private int[] getMultiTableKeyList(string multiTableKey)
    {
      int[] multiTableKeyList;
      int i = 0;
      switch (multiTableKey)
      {
        case "PumpStationID":
          return PSReport.PumpStationIDList;
        default:
          multiTableKeyList = new int[0];
          return multiTableKeyList;
      }
    }

    public event OnStatusChangedEventHandler StatusChanged;

    /// <summary>
    /// Internally called function that fires the OnStatusChanged event.
    /// </summary>
    /// <param name="e">Parameter that contains the string describing the new state.</param>
    protected virtual void OnStatusChanged(StatusChangedArgs e)
    {
      if (bw != null && bw.CancellationPending == true)
      {
        doWorkEventArgs.Cancel = true;
        throw new Exception("Canceled");
      }
      if (StatusChanged != null)
        StatusChanged(e);
    }

  }

}
