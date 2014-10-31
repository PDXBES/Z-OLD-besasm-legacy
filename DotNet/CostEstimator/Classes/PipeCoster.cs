// Project: Classes, File: PipeCoster.cs
// Namespace: CostEstimator.Classes, Class: PipeCoster
// Path: C:\Development\CostEstimatorV2\Classes, Author: Arnel
// Code lines: 37, Size of file: 917 Bytes
// Creation date: 3/2/2008 8:55 PM
// Last modified: 6/25/2010 1:03 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  public enum PipeMaterial
  {
    Concrete,
    PVCHDPE,
    CIPP,
    Pipeburst
  } // enum PipeMaterial

  public struct ManholeCosts
  {
    public float BaseCost;
    public float CostPerFtDepthAbove8Ft;
    public float RimFrameCost;

    /// <summary>
    /// Create manhole costs
    /// </summary>
    /// <param name="baseCost">Base cost</param>
    /// <param name="costPerFtDepthAbove8Ft">Cost per ft depth above 8 ft</param>
    public ManholeCosts(float baseCost, float costPerFtDepthAbove8Ft,
    float rimFrameCost)
    {
      BaseCost = baseCost;
      CostPerFtDepthAbove8Ft = costPerFtDepthAbove8Ft;
      RimFrameCost = rimFrameCost;
    } // ManholeCosts(baseCost, costPerFtDepthAbove8Ft)
  } // ManholeCosts

  public struct ManholeDepthFactorDiameterRange
  {
    public double MinDiameter;
    public double MaxDiameter;
    public SortedList<double, ManholeDepthFactor> ManholeDepthFactors;

    /// <summary>
    /// Create manhole depth factor diameter range
    /// </summary>
    /// <param name="minDiameter">Min diameter</param>
    /// <param name="maxDiameter">Max diameter</param>
    public ManholeDepthFactorDiameterRange(double minDiameter, double maxDiameter)
    {
      MinDiameter = minDiameter;
      MaxDiameter = maxDiameter;
      ManholeDepthFactors = new SortedList<double, ManholeDepthFactor>();
    } // ManholeDepthFactorDiameterRange(minDiameter, maxDiameter)
  } // ManholeDepthFactorDiameterRange

  public struct ManholeDepthFactor
  {
    public double MinDepth;
    public double MaxDepth;
    public double Factor;

    /// <summary>
    /// Create manhole depth factor
    /// </summary>
    /// <param name="minDiameter">Min diameter</param>
    /// <param name="maxDiameter">Max diameter</param>
    /// <param name="minDepth">Min depth</param>
    /// <param name="maxDepth">Max depth</param>
    /// <param name="factor">Factor</param>
    public ManholeDepthFactor(double minDepth, double maxDepth, double factor)
    {
      MinDepth = minDepth;
      MaxDepth = maxDepth;
      Factor = factor;
    } // ManholeDepthFactor(minDiameter, maxDiameter, minDepth)
  } // ManholeDepthFactor

  /// <summary>
  /// An Engineering News Record Construction Cost Index
  /// </summary>
  [Serializable]
  public struct ENR
  {
    public int Value;
    public int Month;
    public int Year;

    /// <summary>
    /// Create ENR
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="month">Month</param>
    /// <param name="year">Year</param>
    public ENR(int value, int month, int year)
    {
      Value = value;
      DateTime date = new DateTime();
      try
      {
        date = new DateTime(year, month, 1);
      }
      catch (ArgumentOutOfRangeException e)
      {
        throw e;
      } // catch

      Month = date.Month;
      Year = date.Year;
    } // ENR(value, month, year)

    public override string ToString()
    {
      string monthAsString = "";
      switch (Month)
      {
        case 1:
          monthAsString = "Jan";
          break;
        case 2:
          monthAsString = "Feb";
          break;
        case 3:
          monthAsString = "Mar";
          break;
        case 4:
          monthAsString = "Apr";
          break;
        case 5:
          monthAsString = "May";
          break;
        case 6:
          monthAsString = "Jun";
          break;
        case 7:
          monthAsString = "Jul";
          break;
        case 8:
          monthAsString = "Aug";
          break;
        case 9:
          monthAsString = "Sep";
          break;
        case 10:
          monthAsString = "Oct";
          break;
        case 11:
          monthAsString = "Nov";
          break;
        case 12:
          monthAsString = "Dec";
          break;
      }
      return string.Format("{2} ({0} {1})", monthAsString, Year, Value);
    }
  } // ENR

  /// <summary>
  /// Develops costs for a pipe given a diameter, material, and depth
  /// </summary>
  public class PipeCoster
  {
    #region Constants
        private const int DEFAULT_AC_WIDTH = 4;
        private const int MAX_TRENCH_SHORING_DEPTH = 18;

#endregion
    #region Variables
    private float _SawcuttingACPavementPerSqFt;
    private float _AsphaltRemovalPerSqYd;
    private float _TruckHaulExcavationSpoilsPerCuYd;
    private float _TrenchShoringPerSqFt;
    private float _AsphaltTrenchPatchBaseCoursePerCuYd;
    private float _AsphaltTrenchPathPerSqYd;
    private float _PipeZoneBackfillLessThanEqualTo12InDiamPerCuYd;
    private float _PipeZoneBackfillGreater12InDiamPerCuYd;
    private float _FillAbovePipeZoneLessThanEqualTo12InDiamPerCuYd;
    private float _FillAbovePipeZoneGreater12InDiamPerCuYd;
    private float _SpoilsFactor;

    private Dictionary<PipeMaterial, SortedList<float, float>> _InsideDiameterToOutsideDiameterTable =
      new Dictionary<PipeMaterial, SortedList<float, float>>();
    private SortedList<float, float> _InsideDiameterToManholeDiameterTable =
      new SortedList<float, float>();
    private SortedList<float, float> _OutsideDiameterToTrenchWidthTable =
      new SortedList<float, float>();
    private SortedList<float, float> _DepthToTrenchExcavationCostPerCuYd =
      new SortedList<float, float>();
    private Dictionary<PipeMaterial, SortedList<float, float>> _PipeCostTable =
      new Dictionary<PipeMaterial, SortedList<float, float>>();
    private float _DefaultLateralCostPerFt;
    private SortedList<float, SortedList<float, float>> _LateralCostTable =
      new SortedList<float, SortedList<float, float>>();
    private SortedList<float, ManholeCosts> _ManholeCostsTable =
      new SortedList<float, ManholeCosts>();
    private SortedList<float, ManholeDepthFactorDiameterRange> _ManholeDepthFactorsTable =
      new SortedList<float, ManholeDepthFactorDiameterRange>();
    private SortedList<float, float> _TVCleaningTable = new SortedList<float, float>();

    private float _LateralLessThanEqualTo24InDiamCost;
    private float _LateralGreaterThan24InDiamCost;
    private float _TvCleanLessThanEqualTo24InDiamCost;
    private float _TvCleanGreaterThan24InDiamCost;
    private float _PipeburstLateralCost;
    private float _PipeburstTVCleanLessThanEqualTo24InDiamCost;


    private SortedList<float, float> _FlowDiversionCostsTable =
    new SortedList<float, float>();
    private SortedList<float, float> _CIPPCostTable =
    new SortedList<float, float>();
    private SortedList<float, float> _PipeburstCostTable =
    new SortedList<float, float>();

    private ENR _BaseENR;
    private ENR _CurrentENR;

    private PipeMaterial _Material;
    private float _InsideDiameter;
    private float _Depth;
    private Dictionary<string, string> _OutsideTableMessages = new Dictionary<string, string>();

    public SortedList<string, CosterParameter> CosterParameters = new SortedList<string, CosterParameter>();
    public Dictionary<string, UnitCost> DirectConstructionCostItems = new Dictionary<string, UnitCost>();

    #endregion

    #region Constructors
    /// <summary>
    /// Create pipe coster
    /// </summary>
    public PipeCoster()
    {
      LoadReference();
    } // PipeCoster()
    #endregion

    #region Properties
    /// <summary>
    /// Material
    /// </summary>
    /// <returns>Pipe material</returns>
    public PipeMaterial Material
    {
      get
      {
        return _Material;
      } // get
      set
      {
        _Material = value;
      } // set
    } // Material

    /// <summary>
    /// Inside diameter
    /// </summary>
    /// <returns>Double</returns>
    public float InsideDiameter
    {
      get
      {
        return _InsideDiameter;
      } // get
      set
      {
        _InsideDiameter = value;
      } // set
    } // InsideDiameter

    /// <summary>
    /// Depth
    /// </summary>
    /// <returns>Double</returns>
    public float Depth
    {
      get
      {
        return _Depth;
      } // get
      set
      {
        _Depth = value;
      } // set
    } // Depth

    /// <summary>
    /// Outside diameter
    /// </summary>
    /// <returns>Double</returns>
    public float OutsideDiameter
    {
      get
      {
        bool outsideTable;
        return OutsideDiameterFromInsideDiameter(Material, InsideDiameter, out outsideTable);
      } // get
    } // OutsideDiameter

    /// <summary>
    /// Manhole diameter
    /// </summary>
    /// <returns>Double</returns>
    public float ManholeDiameter
    {
      get
      {
        // function of InsideDiameter
        bool outsideTable;
        return ManholeDiameterFromInsideDiameter(InsideDiameter, out outsideTable);
      } // get
    } // ManholeDiameter

    /// <summary>
    /// Trench base width (ft)
    /// </summary>
    /// <returns>Double</returns>
    public float TrenchBaseWidth
    {
      get
      {
        bool outsideTable;
        return TrenchWidthFromOutsideDiameter(OutsideDiameter, out outsideTable);
      } // get
    } // TrenchBaseWidth

    /// <summary>
    /// Asphal removal width (ft)
    /// </summary>
    /// <returns>Double</returns>
    public float AsphaltRemovalWidth
    {
      get
      {
        return TrenchBaseWidth + 1;
      } // get
    } // AsphaltRemovalWidth

    /// <summary>
    /// Asphalt removal surface area (sq ft)
    /// </summary>
    /// <returns>Double</returns>
    public float AsphaltRemovalSurfaceArea
    {
      get
      {
        return AsphaltRemovalWidth * 1;
      } // get
    } // AsphaltRemovalSurfaceArea

    /// <summary>
    /// Asphalt patch surface area per foot of pipe (sq ft)
    /// </summary>
    /// <returns>Double</returns>
    public float AsphaltPatchSurfaceArea
    {
      get
      {
        return AsphaltRemovalSurfaceArea;
      } // get
    } // AsphaltPatchSurfaceArea

    /// <summary>
    /// Excavation volume per foot of pipe (cu yd)
    /// </summary>
    /// <returns>Double</returns>
    public float ExcavationVolume
    {
      get
      {
        return _Depth * TrenchBaseWidth / 27;
      } // get
    } // ExcavationVolume

    /// <summary>
    /// Spoils volume per foot of pipe (cu yd)
    /// </summary>
    /// <returns>Double</returns>
    public float SpoilsVolume
    {
      get
      {
        return ExcavationVolume * _SpoilsFactor;
      } // get
    } // SpoilsVolume

    /// <summary>
    /// Pipe zone depth (ft)
    /// </summary>
    /// <returns>Double</returns>
    public float PipeZoneDepth
    {
      get
      {
        return (float)(0.5 + 1 + OutsideDiameter / 12);
      } // get
    } // PipeZoneDepth

    /// <summary>
    /// Pipe volume per foot of pipe (ft)
    /// </summary>
    /// <returns>Double</returns>
    public float PipeVolume
    {
      get
      {
        return (float)(Math.Pow(OutsideDiameter / 12, 2) * Math.PI / 4 / 27);
      } // get
    } // PipeVolume

    /// <summary>
    /// Shoring per foot of pipe (sq ft)
    /// </summary>
    /// <returns>Double</returns>
    public float Shoring
    {
      get
      {
        return Depth;
      } // get
    } // Shoring

    /// <summary>
    /// Pipe zone volume per foot of pipe (cu yd)
    /// </summary>
    /// <returns>Double</returns>
    public float PipeZoneVolume
    {
      get
      {
        return (TrenchBaseWidth * PipeZoneDepth) / 27 - PipeVolume;
      } // get
    } // PipeZoneVolume

    /// <summary>
    /// Asphalt base volume per foot of pipe (cu yd)
    /// </summary>
    /// <returns>Double</returns>
    public float AsphaltBaseVolume
    {
      get
      {
        // 8 in. thickness
        return (AsphaltRemovalWidth * 2 / 3) / 27;
      } // get
    } // AsphaltBaseVolume

    /// <summary>
    /// Above zone volume per foot of pipe (cu yd)
    /// </summary>
    /// <returns>Double</returns>
    public float AboveZoneVolume
    {
      get
      {
        return ExcavationVolume - PipeVolume - PipeZoneVolume - AsphaltBaseVolume;
      } // get
    } // AboveZoneVolume

    /// <summary>
    /// Direct construction cost
    /// </summary>
    /// <returns>float</returns>
    public float DirectConstructionCost
    {
      get
      {
        AssignDirectConstructionCostItems();

        float directCost = 0.0f;

        foreach (KeyValuePair<string, UnitCost> kvpair in DirectConstructionCostItems)
        {
          UnitCost item = kvpair.Value;
          directCost += item.CostPerUnit * (float)item.Units;
        }

        return directCost;
      } // get
    } // DirectConstructionCost

    /// <summary>
    /// Base ENR
    /// </summary>
    /// <returns>Int</returns>
    public ENR BaseENR
    {
      get
      {
        return _BaseENR;
      } // get
    } // BaseENR

    /// <summary>
    /// Current ENR
    /// </summary>
    /// <returns>Int</returns>
    public ENR CurrentENR
    {
      get
      {
        return _CurrentENR;
      } // get
    } // CurrentENR

    /// <summary>
    /// Item
    /// </summary>
    /// <param name="index">Index</param>
    /// <returns>Object</returns>
    public object this[string index]
    {
      get
      {
        return CosterParameters[index];
      } // get
      /// <summary>
      /// Item
      /// </summary>
      /// <param name="index">Index</param>
      /// <returns>Object</returns>
    } // Item (index)
    #endregion

    #region Methods
    /// <summary>
    /// String to pipe material
    /// </summary>
    /// <param name="materialName">Material name</param>
    /// <returns>Pipe material</returns>
    public static PipeMaterial StringToPipeMaterial(string materialName)
    {
      switch (materialName.ToUpper())
      {
        case "PVC":
        case "HDPE":
          return PipeMaterial.PVCHDPE;
        case "PIPEBUR":
        case "PIPEBURST":
        case "PBURST":
          return PipeMaterial.Pipeburst;
        case "CIPP":
          return PipeMaterial.CIPP;
        default:
          return PipeMaterial.Concrete;
      } // switch
    } // StringToPipeMaterial(materialName)

    /// <summary>
    /// Initialize coster parameters
    /// </summary>
    public void InitializeCosterParameters()
    {
      CosterParameter param;
      param = new CosterParameter("Sawcutting AC pavement per sq ft", "System.float", _SawcuttingACPavementPerSqFt);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Asphalt removal per sq yd", "System.float", _AsphaltRemovalPerSqYd);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Truck haul excavation spoils per cu yd", "System.float", _TruckHaulExcavationSpoilsPerCuYd);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Trench shoring per sq ft", "System.float", _TrenchShoringPerSqFt);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Asphalt trench patch base course per cu yd", "System.float", _AsphaltTrenchPatchBaseCoursePerCuYd);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Asphalt trench patch per sq yd", "System.float", _AsphaltTrenchPathPerSqYd);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Pipe zone backfill less than or equal to 12 in diam per cu yd", "System.float", _PipeZoneBackfillLessThanEqualTo12InDiamPerCuYd);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Pipe zone backfill greater than 12 in diam per cu yd", "System.float", _PipeZoneBackfillGreater12InDiamPerCuYd);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Fill above pipe zone less than or equal to 12 in diam per cu yd", "System.float", _FillAbovePipeZoneLessThanEqualTo12InDiamPerCuYd);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Fill above pipe zone greater than 12 in diam per cu yd", "System.float", _FillAbovePipeZoneGreater12InDiamPerCuYd);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Lateral less than or equal to 24 in diam", "System.float", _LateralLessThanEqualTo24InDiamCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Lateral greater than 24 in diam", "System.float", _LateralGreaterThan24InDiamCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("TV cleaning less than or equal to 24 in diam", "System.float", _TvCleanLessThanEqualTo24InDiamCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("TV cleaning greater than 24 in diam", "System.float", _TvCleanGreaterThan24InDiamCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Pipeburst lateral", "System.float", _PipeburstLateralCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Pipeburst TV cleaning less than or equal to 24 in diam", "System.float", _PipeburstTVCleanLessThanEqualTo24InDiamCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Base ENR", "System.float", _BaseENR);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Current ENR", "System.float", _CurrentENR);
      CosterParameters.Add(param.Name, param);
    } // InitializeCosterParameters()

    /// <summary>
    /// Load pipe cost reference
    /// </summary>
    /// <returns>Bool</returns>
    public bool LoadReference()
    {
      XmlReaderSettings xmlSettings = new XmlReaderSettings();
      xmlSettings.IgnoreWhitespace = true;
      xmlSettings.ValidationType = ValidationType.Schema;
      xmlSettings.ConformanceLevel = ConformanceLevel.Document;

      string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
      XmlReader pipeCostRef = XmlReader.Create(appPath + @"\PipeCostReference.xml", xmlSettings);

      XPathDocument pipeCostRefDoc = new XPathDocument(pipeCostRef, XmlSpace.None);
      XPathNavigator pipeCostRefNav = pipeCostRefDoc.CreateNavigator();

      string xpathQuery = "/PipeCostOptions/TrenchedConstants/*";

      XPathNodeIterator xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        switch (xpathIter.Current.Name)
        {
          case "sawCuttingACPavementPerSqFt":
            _SawcuttingACPavementPerSqFt = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "asphaltRemovalPerSqYd":
            _AsphaltRemovalPerSqYd = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "truckHaulExcavationSpoilsPerCuYd":
            _TruckHaulExcavationSpoilsPerCuYd = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "trenchShoringPerSqFt":
            _TrenchShoringPerSqFt = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "asphaltTrenchPatchBaseCoursePerCuYd":
            _AsphaltTrenchPatchBaseCoursePerCuYd = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "asphaltTrenchPatchPerSqYd":
            _AsphaltTrenchPathPerSqYd = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "pipeZoneBackfillLessThanEqualTo12InDiamPerCuYd":
            _PipeZoneBackfillLessThanEqualTo12InDiamPerCuYd = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "pipeZoneBackfillGreaterThan12InDiamPerCuYd":
            _PipeZoneBackfillGreater12InDiamPerCuYd = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "fillAbovePipeZoneLessThanEqualTo12InDiamPerCuYd":
            _FillAbovePipeZoneLessThanEqualTo12InDiamPerCuYd = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "fillAbovePipeZoneGreaterThan12InDiamPerCuYd":
            _FillAbovePipeZoneGreater12InDiamPerCuYd = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "spoilsFactor":
            _SpoilsFactor = (float)xpathIter.Current.ValueAsDouble;
            break;
        } // switch
      } // while

      xpathQuery = "/PipeCostOptions/InsideDiameterToOutsideDiameterTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        float insideDiameterIn = 0;
        PipeMaterial material = new PipeMaterial();
        float outsideDiameterIn = 0;

        while (childIter.MoveNext())
        {
          switch (childIter.Current.Name)
          {
            case "insideDiameterIn":
              insideDiameterIn = (float)childIter.Current.ValueAsDouble;
              break;
            case "material":
              material = (PipeMaterial)Enum.Parse(typeof(PipeMaterial), childIter.Current.Value, true);
              break;
            case "outsideDiameterIn":
              outsideDiameterIn = (float)childIter.Current.ValueAsDouble;
              break;
          } // switch
        } // while

        if (!_InsideDiameterToOutsideDiameterTable.ContainsKey(material))
          _InsideDiameterToOutsideDiameterTable.Add(material, new SortedList<float, float>());
        _InsideDiameterToOutsideDiameterTable[material].Add(insideDiameterIn, outsideDiameterIn);
      } // while

      xpathQuery = "/PipeCostOptions/InsideDiameterToManholeDiameterTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        float insideDiameterIn = 0;
        float manholeDiameterIn = 0;

        while (childIter.MoveNext())
        {
          switch (childIter.Current.Name)
          {
            case "insideDiameterIn":
              insideDiameterIn = (float)childIter.Current.ValueAsDouble;
              break;
            case "manholeDiameterIn":
              manholeDiameterIn = (float)childIter.Current.ValueAsDouble;
              break;
          } // switch
        } // while
        _InsideDiameterToManholeDiameterTable.Add(insideDiameterIn, manholeDiameterIn);
      } // while

      xpathQuery = "/PipeCostOptions/TrenchBaseWidthTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        float outsideDiameterIn = 0;
        float widthFt = 0;

        while (childIter.MoveNext())
        {
          switch (childIter.Current.Name)
          {
            case "outsideDiameterIn":
              outsideDiameterIn = (float)childIter.Current.ValueAsDouble;
              break;
            case "widthFt":
              widthFt = (float)childIter.Current.ValueAsDouble;
              break;
          } // switch
        } // while

        _OutsideDiameterToTrenchWidthTable.Add(outsideDiameterIn, widthFt);
      } // while

      xpathQuery = "/PipeCostOptions/DepthToTrenchExcavationCostTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        float depthFt = 0;
        float costPerCuYd = 0;

        while (childIter.MoveNext())
        {
          switch (childIter.Current.Name)
          {
            case "depthFt":
              depthFt = (float)childIter.Current.ValueAsDouble;
              break;
            case "costPerCuYd":
              costPerCuYd = (float)childIter.Current.ValueAsDouble;
              break;
          } // switch
        } // while

        _DepthToTrenchExcavationCostPerCuYd.Add(depthFt, costPerCuYd);
      }

      xpathQuery = "/PipeCostOptions/PipeCostTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        float diameterIn = 0;
        PipeMaterial material = new PipeMaterial();
        float costPerFt = 0;

        while (childIter.MoveNext())
        {
          switch (childIter.Current.Name)
          {
            case "diameterIn":
              diameterIn = (float)childIter.Current.ValueAsDouble;
              break;
            case "material":
              material = (PipeMaterial)Enum.Parse(typeof(PipeMaterial), childIter.Current.Value, true);
              break;
            case "costPerFt":
              costPerFt = (float)childIter.Current.ValueAsDouble;
              break;
          } // switch
        } // while
        if (!_PipeCostTable.ContainsKey(material))
          _PipeCostTable.Add(material, new SortedList<float, float>());
        _PipeCostTable[material].Add(diameterIn, costPerFt);
      } // while

      xpathQuery = "/PipeCostOptions/LateralCostTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        if (xpathIter.Current.Name == "DefaultLateralCostPerFt")
        {
          _DefaultLateralCostPerFt = (float)xpathIter.Current.ValueAsDouble;
          continue;
        } // if

        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        float insideDiameterIn = 0;
        float depthFt = 0;
        float costPerFt = 0;

        while (childIter.MoveNext())
        {
          switch (childIter.Current.Name)
          {
            case "insideDiameterIn":
              insideDiameterIn = (float)childIter.Current.ValueAsDouble;
              break;
            case "depthFt":
              depthFt = (float)childIter.Current.ValueAsDouble;
              break;
            case "costPerFt":
              costPerFt = (float)childIter.Current.ValueAsDouble;
              break;
          } // switch
        } // while

        if (!_LateralCostTable.ContainsKey(insideDiameterIn))
          _LateralCostTable.Add(insideDiameterIn, new SortedList<float, float>());
        _LateralCostTable[insideDiameterIn].Add(depthFt, costPerFt);
      } // while

      xpathQuery = "/PipeCostOptions/ManholeCostTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        float manholeDiameterIn = 0;
        float baseCost = 0;
        float costPerFtDepthAbove8Ft = 0;
        float rimFrameCost = 0;

        while (childIter.MoveNext())
        {
          switch (childIter.Current.Name)
          {
            case "manholeDiameterIn":
              manholeDiameterIn = (float)childIter.Current.ValueAsDouble;
              break;
            case "baseCost":
              baseCost = (float)childIter.Current.ValueAsDouble;
              break;
            case "costPerFtDepthAbove8Ft":
              costPerFtDepthAbove8Ft = (float)childIter.Current.ValueAsDouble;
              break;
            case "rimFrameCost":
              rimFrameCost = (float)childIter.Current.ValueAsDouble;
              break;
          } // switch
        } // while

        _ManholeCostsTable.Add(manholeDiameterIn, new ManholeCosts(baseCost, costPerFtDepthAbove8Ft, rimFrameCost));
      } // while

      xpathQuery = "/PipeCostOptions/ManholeCostDepthFactorTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        string manholeMinDiamAsString = xpathIter.Current.GetAttribute("MinDiam", string.Empty);
        float manholeMinDiam = manholeMinDiamAsString == string.Empty ?
        0 : (float)Convert.ToDouble(manholeMinDiamAsString);

        string manholeMaxDiamAsString = xpathIter.Current.GetAttribute("MaxDiam", string.Empty);
        float manholeMaxDiam = manholeMaxDiamAsString == string.Empty ?
        float.MaxValue : (float)Convert.ToDouble(manholeMaxDiamAsString);


        _ManholeDepthFactorsTable.Add(manholeMinDiam, new ManholeDepthFactorDiameterRange(manholeMinDiam, manholeMaxDiam));
        ManholeDepthFactorDiameterRange currentDiameterRange = _ManholeDepthFactorsTable[manholeMinDiam];

        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        while (childIter.MoveNext())
        {
          float manholeMinDepth;
          float manholeMaxDepth;
          float factor;
          switch (childIter.Current.Name)
          {
            case "factor":
              string manholeMinDepthAsString = childIter.Current.GetAttribute("MinDepth", string.Empty);
              manholeMinDepth = manholeMinDepthAsString == string.Empty ?
              0 : (float)Convert.ToDouble(manholeMinDepthAsString);
              string manholeMaxDepthAsString = childIter.Current.GetAttribute("MaxDepth", string.Empty);
              manholeMaxDepth = manholeMaxDepthAsString == string.Empty ?
              float.MaxValue : (float)Convert.ToDouble(manholeMaxDepthAsString);
              factor = (float)childIter.Current.ValueAsDouble;

              currentDiameterRange.ManholeDepthFactors.Add(manholeMinDepth,
              new ManholeDepthFactor(manholeMinDepth, manholeMaxDepth, factor));
              break;
          }
        }

        xpathQuery = "/PipeCostOptions/TVCleaningTable/*";
        xpathIter = pipeCostRefNav.Select(xpathQuery);
        while (xpathIter.MoveNext())
        {
          string diamAsString = xpathIter.Current.GetAttribute("Diam", string.Empty);
          float diam = diamAsString == string.Empty ? 0 : (float)Convert.ToDouble(diamAsString);
          float cost = (float)xpathIter.Current.ValueAsDouble;

          _TVCleaningTable.Add(diam, cost);
        }
      } // while

      xpathQuery = "/PipeCostOptions/TrenchlessCIPPConstants/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        switch (xpathIter.Current.Name)
        {
          case "lateralLessThanEqualTo24InDiamCost":
            _LateralLessThanEqualTo24InDiamCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "lateralGreaterThan24InDiamCost":
            _LateralGreaterThan24InDiamCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "tvCleanLessThanEqualTo24InDiamCost":
            _TvCleanLessThanEqualTo24InDiamCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "tvCleanGreaterThan24InDiamCost":
            _TvCleanGreaterThan24InDiamCost = (float)xpathIter.Current.ValueAsDouble;
            break;
        } // switch
      } // while

      xpathQuery = "/PipeCostOptions/TrenchlessPipeburstConstants/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        switch (xpathIter.Current.Name)
        {
          case "lateralCost":
            _PipeburstLateralCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "tvCleanLessThanEqualTo24InDiamCost":
            _PipeburstTVCleanLessThanEqualTo24InDiamCost = (float)xpathIter.Current.ValueAsDouble;
            break;
        } // switch
      } // while

      xpathQuery = "/PipeCostOptions/TrenchlessFlowDiversionCostTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        float insideDiameterIn = 0;
        float costPerFt = 0;

        while (childIter.MoveNext())
        {
          switch (childIter.Current.Name)
          {
            case "insideDiameterIn":
              insideDiameterIn = (float)childIter.Current.ValueAsDouble;
              break;
            case "costPerFt":
              costPerFt = (float)childIter.Current.ValueAsDouble;
              break;
          } // switch
        } // while

        _FlowDiversionCostsTable.Add(insideDiameterIn, costPerFt);
      } // while

      xpathQuery = "/PipeCostOptions/TrenchlessCIPPCostTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        float insideDiameterIn = 0;
        float costPerFt = 0;

        while (childIter.MoveNext())
        {
          switch (childIter.Current.Name)
          {
            case "insideDiameterIn":
              insideDiameterIn = (float)childIter.Current.ValueAsDouble;
              break;
            case "costPerFt":
              costPerFt = (float)childIter.Current.ValueAsDouble;
              break;
          } // switch
        } // while

        _CIPPCostTable.Add(insideDiameterIn, costPerFt);
      } // while

      xpathQuery = "/PipeCostOptions/TrenchlessPipeburstCostTable/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      while (xpathIter.MoveNext())
      {
        XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
        float insideDiameterIn = 0;
        float costPerFt = 0;

        while (childIter.MoveNext())
        {
          switch (childIter.Current.Name)
          {
            case "insideDiameterIn":
              insideDiameterIn = (float)childIter.Current.ValueAsDouble;
              break;
            case "costPerFt":
              costPerFt = (float)childIter.Current.ValueAsDouble;
              break;
          } // switch
        } // while

        _PipeburstCostTable.Add(insideDiameterIn, costPerFt);
      } // while

      xpathQuery = "/PipeCostOptions/BaseENR/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      int enrValue = 0;
      int enrMonth = 1;
      int enrYear = 0;
      while (xpathIter.MoveNext())
      {
        switch (xpathIter.Current.Name)
        {
          case "value":
            enrValue = xpathIter.Current.ValueAsInt;
            break;
          case "month":
            enrMonth = xpathIter.Current.ValueAsInt;
            break;
          case "year":
            enrYear = xpathIter.Current.ValueAsInt;
            break;
        } // switch
      }
      _BaseENR = new ENR(enrValue, enrMonth, enrYear);

      xpathQuery = "/PipeCostOptions/CurrentENR/*";
      xpathIter = pipeCostRefNav.Select(xpathQuery);
      enrValue = 0;
      enrMonth = 1;
      enrYear = 0;
      while (xpathIter.MoveNext())
      {
        switch (xpathIter.Current.Name)
        {
          case "value":
            enrValue = xpathIter.Current.ValueAsInt;
            break;
          case "month":
            enrMonth = xpathIter.Current.ValueAsInt;
            break;
          case "year":
            enrYear = xpathIter.Current.ValueAsInt;
            break;
        } // switch
      }
      _CurrentENR = new ENR(enrValue, enrMonth, enrYear);

      InitializeCosterParameters();

      return true;
    } // LoadReference()

    /// <summary>
    /// Outside diameter from inside diameter
    /// </summary>
    /// <param name="insideDiameter">Inside diameter</param>
    /// <returns>Double</returns>
    public float OutsideDiameterFromInsideDiameter(PipeMaterial material, double insideDiameter, out bool outsideTable)
    {
      if (!_InsideDiameterToOutsideDiameterTable.ContainsKey(material))
      {
        outsideTable = true;
        return -1;
      } // if
      else
        if (insideDiameter < _InsideDiameterToOutsideDiameterTable[material].Keys[0])
        {
          outsideTable = true;
          return _InsideDiameterToOutsideDiameterTable[material].Values[0];
        } // if
        else
          if (insideDiameter > _InsideDiameterToOutsideDiameterTable[material].Keys[_InsideDiameterToOutsideDiameterTable[material].Count - 1])
          {
            outsideTable = true;
            return _InsideDiameterToOutsideDiameterTable[material].Values[_InsideDiameterToOutsideDiameterTable[material].Count - 1];
          } // if
          else
          {
            int foundIndex = -1;
            for (int i = 0; i < _InsideDiameterToOutsideDiameterTable[material].Count; i++)
            {
              if (_InsideDiameterToOutsideDiameterTable[material].Keys[i] >= insideDiameter)
              {
                foundIndex = i;
                break;
              } // if
            } // for
            System.Diagnostics.Debug.Assert(foundIndex != -1);

            if (foundIndex > -1)
            {
              outsideTable = false;
              return _InsideDiameterToOutsideDiameterTable[material].Values[foundIndex];
            } // if
            else
            {
              // should never be reached; provided for compiler syntax check
              outsideTable = true;
              return -1;
            } // else
          } // else
    } // OutsideDiameterFromInsideDiameter(material, insideDiameter, outsideTable)

    /// <summary>
    /// Manhole diameter from inside diameter
    /// </summary>
    /// <param name="insideDiameter">Inside diameter</param>
    /// <param name="outsideTable">Outside table</param>
    /// <returns>Double</returns>
    public float ManholeDiameterFromInsideDiameter(double insideDiameter, out bool outsideTable)
    {
      if (insideDiameter < _InsideDiameterToManholeDiameterTable.Keys[0])
      {
        outsideTable = true;
        return _InsideDiameterToManholeDiameterTable.Values[0];
      } // if
      else
        if (insideDiameter > _InsideDiameterToManholeDiameterTable.Keys[_InsideDiameterToManholeDiameterTable.Count - 1])
        {
          outsideTable = true;
          return _InsideDiameterToManholeDiameterTable.Values[_InsideDiameterToManholeDiameterTable.Count - 1];
        } // if
        else
        {
          int foundIndex = -1;
          for (int i = 0; i < _InsideDiameterToManholeDiameterTable.Count; i++)
          {
            if (_InsideDiameterToManholeDiameterTable.Keys[i] >= insideDiameter)
            {
              foundIndex = i;
              break;
            } // if
          } // for
          System.Diagnostics.Debug.Assert(foundIndex != -1);

          if (foundIndex > -1)
          {
            outsideTable = false;
            return _InsideDiameterToManholeDiameterTable.Values[foundIndex];
          } // if
          else
          {
            // should never be reached; provided for compiler syntax check
            outsideTable = true;
            return -1;
          } // else
        } // else
    } // ManholeDiameterFromInsideDiameter(insideDiameter, outsideTable)

    /// <summary>
    /// Trench width from outside diameter
    /// </summary>
    /// <param name="outsideDiameter">Outside diameter</param>
    /// <param name="outsideTable">Outside table</param>
    /// <returns>Double</returns>
    public float TrenchWidthFromOutsideDiameter(double outsideDiameter, out bool outsideTable)
    {
      if (outsideDiameter < _OutsideDiameterToTrenchWidthTable.Keys[0])
      {
        outsideTable = true;
        return _OutsideDiameterToTrenchWidthTable.Values[0];
      } // if
      else
        if (outsideDiameter > _OutsideDiameterToTrenchWidthTable.Keys[_OutsideDiameterToTrenchWidthTable.Count - 1])
        {
          outsideTable = true;
          return _OutsideDiameterToTrenchWidthTable.Values[_OutsideDiameterToTrenchWidthTable.Count - 1];
        } // if
        else
        {
          int foundIndex = -1;
          for (int i = 0; i < _OutsideDiameterToTrenchWidthTable.Count; i++)
          {
            if (_OutsideDiameterToTrenchWidthTable.Keys[i] >= outsideDiameter)
            {
              foundIndex = i;
              break;
            } // if
          } // for
          System.Diagnostics.Debug.Assert(foundIndex != -1);

          if (foundIndex > -1)
          {
            outsideTable = false;
            return _OutsideDiameterToTrenchWidthTable.Values[foundIndex];
          } // if
          else
          {
            // should never be reached; provided for compiler syntax check
            outsideTable = true;
            return -1;
          } // else
        } // else
    } // TrenchWidthFromOutsideDiameter(outsideDiameter, outsideTable)

    public float TVCleaningCost(double diameter, out bool outsideTable)
    {
      if (diameter < _TVCleaningTable.Keys[0])
      {
        outsideTable = true;
        return _TVCleaningTable.Values[0];
      }
      else if (diameter > _TVCleaningTable.Keys[_TVCleaningTable.Count - 1])
      {
        outsideTable = true;
        return _TVCleaningTable.Values[_TVCleaningTable.Count - 1];
      }
      else
      {
        int foundIndex = -1;
        for (int i = 0; i < _TVCleaningTable.Count; i++)
        {
          if (_TVCleaningTable.Keys[i] >= diameter)
          {
            foundIndex = i;
            break;
          }
        }

        if (foundIndex > -1)
        {
          outsideTable = false;
          return _TVCleaningTable.Values[foundIndex];
        }
        else
        {
          outsideTable = true;
          return -1;
        }
      }
    }

    /// <summary>
    /// Trench excavation cost per cu yd from pipe depth
    /// </summary>
    /// <param name="depth">Depth in ft</param>
    /// <returns>float</returns>
    public float TrenchExcavationCostPerCuYdFromDepth(double depth)
    {
      if (depth < _DepthToTrenchExcavationCostPerCuYd.Keys[0])
      {
        return _DepthToTrenchExcavationCostPerCuYd.Values[0];
      } // if
      else
        if (depth > _DepthToTrenchExcavationCostPerCuYd.Keys[_DepthToTrenchExcavationCostPerCuYd.Count - 1])
        {
          return _DepthToTrenchExcavationCostPerCuYd.Values[_DepthToTrenchExcavationCostPerCuYd.Count - 1];
        } // if
        else
        {
          int foundIndex = -1;
          for (int i = 0; i < _DepthToTrenchExcavationCostPerCuYd.Count; i++)
          {
            if (_DepthToTrenchExcavationCostPerCuYd.Keys[i] >= depth)
            {
              foundIndex = i;
              break;
            } // if
          }
          System.Diagnostics.Debug.Assert(foundIndex != -1);

          if (foundIndex > -1)
          {
            return _DepthToTrenchExcavationCostPerCuYd.Values[foundIndex];
          } // if
          else
          {
            return 0.0f;
          } // else
        } // else
    } // TrenchExcavationPerCuYdFromDepth(diameter)

    /// <summary>
    /// Pipe cost
    /// </summary>
    /// <param name="material">Material</param>
    /// <param name="diameter">Diameter in ft</param>
    /// <param name="outsideTable">Returns true if the supplied material and diameter falls out of the cost table</param>
    /// <returns>float</returns>
    public float PipeCost(PipeMaterial material, double diameter, out bool outsideTable)
    {
      if (!_PipeCostTable.ContainsKey(material))
      {
        outsideTable = true;
        return -1;
      } // if
      else
        if (diameter < _PipeCostTable[material].Keys[0])
        {
          outsideTable = true;
          return _PipeCostTable[material].Values[0];
        } // if
        else
          if (diameter > _PipeCostTable[material].Keys[_PipeCostTable[material].Count - 1])
          {
            outsideTable = true;
            return _PipeCostTable[material].Values[_PipeCostTable[material].Count - 1];
          } // if
          else
          {
            int foundIndex = -1;
            for (int i = 0; i < _PipeCostTable[material].Count; i++)
            {
              if (_PipeCostTable[material].Keys[i] >= diameter)
              {
                foundIndex = i;
                break;
              } // if
            } // for
            System.Diagnostics.Debug.Assert(foundIndex != -1);

            if (foundIndex > -1)
            {
              outsideTable = false;
              return _PipeCostTable[material].Values[foundIndex];
            } // if
            else
            {
              // should never be reached; provided for compiler syntax check
              outsideTable = true;
              return -1;
            } // else
          } // else
    } // PipeCost(material, diameter, outsideTable)

    /// <summary>
    /// Lateral cost
    /// </summary>
    /// <param name="InsideDiameter">Inside diameter</param>
    /// <param name="depth">Depth</param>
    /// <param name="outsideTable">Outside table</param>
    /// <returns>float</returns>
    public float LateralCost(double insideDiameter, double depth, out bool outsideTable)
    {
      int diameterIndex = -1;
      // find the inside diameter first
      if (insideDiameter < _LateralCostTable.Keys[0])
      {
        outsideTable = true;
        diameterIndex = 0;
      } // if
      else
        if ((insideDiameter > _LateralCostTable.Keys[_LateralCostTable.Count - 1]))
        {
          outsideTable = true;
          diameterIndex = _LateralCostTable.Count - 1;
        } // if
        else
        {
          for (int i = 0; i < _LateralCostTable.Count; i++)
          {
            if (_LateralCostTable.Keys[i] >= insideDiameter)
            {
              diameterIndex = i;
              break;
            } // if
          } // for
          System.Diagnostics.Debug.Assert(diameterIndex != -1);
          outsideTable = false;
        } // else

      if (depth < _LateralCostTable.Values[diameterIndex].Keys[0])
      {
        outsideTable = true;
        return _LateralCostTable.Values[diameterIndex].Values[0];
      } // if
      else
        if (depth > _LateralCostTable.Values[diameterIndex].Keys[_LateralCostTable.Values[diameterIndex].Count - 1])
        {
          outsideTable = true;
          return _LateralCostTable.Values[diameterIndex].Values[_LateralCostTable.Values[diameterIndex].Count - 1];
        } // if
        else
        {
          int foundIndex = -1;
          for (int i = 0; i < _LateralCostTable.Values[diameterIndex].Count; i++)
          {
            if (_LateralCostTable.Values[diameterIndex].Keys[i] >= depth)
            {
              foundIndex = i;
              break;
            } // if
          } // for
          System.Diagnostics.Debug.Assert(foundIndex != -1);

          if (foundIndex > -1)
          {
            return _LateralCostTable.Values[diameterIndex].Values[foundIndex];
          } // if
          else
          {
            // should never be reached; provided for compiler syntax check
            outsideTable = true;
            return -1;
          } // else
        } // else
    } // LateralCost(insideDiameter, depth, outsideTable)

    /// <summary>
    /// Manhole cost
    /// </summary>
    /// <param name="pipeDiameter">Pipe diameter</param>
    /// <param name="depth">Depth</param>
    /// <param name="outsideTable">Outside table</param>
    /// <returns>float</returns>
    public float ManholeCost(double pipeDiameter, double depth, out bool outsideTable)
    {
      // CH2M Hill Formulas convert the diameter to feet first in this formula
      // for some reason
      double pipeTotalWallThickness = (pipeDiameter / 12 + 1) * 2;
      double manholeDiameterSafetySpacer = 12;
      double requiredManholeSize = pipeTotalWallThickness + pipeDiameter + manholeDiameterSafetySpacer;
      double finalManholeSize = Math.Ceiling(requiredManholeSize / 12) * 12;

      int diameterIndex;
      float baseCost;
      float incrementalCostAbove8FtDepth;
      float rimFrameCost;
      if (finalManholeSize < _ManholeCostsTable.Keys[0])
      {
        outsideTable = true;
        diameterIndex = 0;
      } // if
      else
        if (finalManholeSize > _ManholeCostsTable.Keys[_ManholeCostsTable.Count - 1])
        {
          outsideTable = true;
          diameterIndex = _ManholeCostsTable.Count - 1;
        } // if
        else
        {
          int foundIndex = -1;
          for (int i = 0; i < _ManholeCostsTable.Count; i++)
          {
            if (_ManholeCostsTable.Keys[i] >= finalManholeSize)
            {
              foundIndex = i;
              break;
            } // if
          } // for
          outsideTable = false;
          System.Diagnostics.Debug.Assert(foundIndex != -1);
          diameterIndex = foundIndex;
        } // else

      baseCost = _ManholeCostsTable.Values[diameterIndex].BaseCost;
      incrementalCostAbove8FtDepth = _ManholeCostsTable.Values[diameterIndex].CostPerFtDepthAbove8Ft;
      rimFrameCost = _ManholeCostsTable.Values[diameterIndex].RimFrameCost;

      return (float)((double)(baseCost +
      (float)((double)incrementalCostAbove8FtDepth * ((depth - 8) > 0 ? depth - 8 : 0)) +
      rimFrameCost) * ManholeCostDepthFactor(finalManholeSize, depth));
    } // ManholeCost(diameter, depth, outsideTable)

    /// <summary>
    /// Flow diversion cost
    /// </summary>
    /// <param name="diameter">Diameter</param>
    /// <returns>float</returns>
    public float FlowDiversionCost(double insideDiameter, out bool outsideTable)
    {
      if (insideDiameter < _FlowDiversionCostsTable.Keys[0])
      {
        outsideTable = true;
        return _FlowDiversionCostsTable.Values[0];
      } // if
      else
        if (insideDiameter > _FlowDiversionCostsTable.Keys[_FlowDiversionCostsTable.Count - 1])
        {
          outsideTable = true;
          return _FlowDiversionCostsTable.Values[_FlowDiversionCostsTable.Count - 1];
        } // if
        else
        {
          int foundIndex = -1;
          for (int i = 0; i < _FlowDiversionCostsTable.Count; i++)
          {
            if (_FlowDiversionCostsTable.Keys[i] >= insideDiameter)
            {
              foundIndex = i;
              break;
            } // if
          } // for
          System.Diagnostics.Debug.Assert(foundIndex != -1);

          if (foundIndex > -1)
          {
            outsideTable = false;
            return _FlowDiversionCostsTable.Values[foundIndex];
          } // if
          else
          {
            // should never be reached; provided for compiler syntax check
            outsideTable = true;
            return -1;
          } // else
        } // else
    } // FlowDiversionCost(insideDiameter, outsideTable)

    /// <summary>
    /// CIPP pipe cost
    /// </summary>
    /// <param name="insideDiameter">Inside diameter</param>
    /// <param name="outsideTable">Outside table</param>
    /// <returns>float</returns>
    public float CIPPPipeCost(double insideDiameter, out bool outsideTable)
    {
      outsideTable = false;
      double cost = insideDiameter < 0 ? 0 : 1.1406 * Math.Pow(insideDiameter, 1.4882);
      int currentENR = CurrentENR.Value;
      int baseENR = BaseENR.Value;
      double costWithENRBackedOut = cost * ((double)baseENR / (double)currentENR);
      return (float)costWithENRBackedOut;
    } // CIPPPipeCost(insideDiameter, outsideTable)

    /// <summary>
    /// Pipeburst pipe cost
    /// </summary>
    /// <param name="insideDiamter">Inside diamter</param>
    /// <param name="outsideTable">Outside table</param>
    /// <returns>float</returns>
    public float PipeburstPipeCost(double insideDiameter, out bool outsideTable)
    {
      if (insideDiameter < _PipeburstCostTable.Keys[0])
      {
        outsideTable = true;
        return _PipeburstCostTable.Values[0];
      } // if
      else
        if (insideDiameter > _PipeburstCostTable.Keys[_PipeburstCostTable.Count - 1])
        {
          outsideTable = true;
          return _PipeburstCostTable.Values[_PipeburstCostTable.Count - 1];
        } // if
        else
        {
          int foundIndex = -1;
          for (int i = 0; i < _PipeburstCostTable.Count; i++)
          {
            if (_PipeburstCostTable.Keys[i] >= insideDiameter)
            {
              foundIndex = i;
              break;
            } // if
          } // for
          System.Diagnostics.Debug.Assert(foundIndex != -1);

          if (foundIndex > -1)
          {
            outsideTable = false;
            return _PipeburstCostTable.Values[foundIndex];
          } // if
          else
          {
            // should never be reached; provided for compiler syntax check
            outsideTable = true;
            return -1;
          } // else
        } // else
    } // PipeburstPipeCost(insideDiameter, outsideTable)

    /// <summary>
    /// Min _ inside diameter to outside diameter table
    /// </summary>
    /// <param name="material">Material</param>
    /// <returns>Double</returns>
    public float Min_InsideDiameterToOutsideDiameterTable(PipeMaterial material)
    {
      return _InsideDiameterToOutsideDiameterTable[material].Keys[0];
    } // Min_InsideDiameterToOutsideDiameterTable(material)

    /// <summary>
    /// Max _ inside diameter to outside diameter table
    /// </summary>
    /// <param name="material">Material</param>
    /// <returns>Double</returns>
    public float Max_InsideDiameterToOutsideDiameterTable(PipeMaterial material)
    {
      return _InsideDiameterToOutsideDiameterTable[material].Keys[_InsideDiameterToOutsideDiameterTable[material].Count - 1];
    } // Max_InsideDiameterToOutsideDiameterTable(material)

    /// <summary>
    /// Min _ inside diamter to manhole diameter table
    /// </summary>
    /// <returns>Double</returns>
    public float Min_InsideDiameterToManholeDiameterTable()
    {
      return _InsideDiameterToManholeDiameterTable.Keys[0];
    } // Min_InsideDiameterToManholeDiameterTable()

    /// <summary>
    /// Max _ inside diameter to manhole diameter table
    /// </summary>
    /// <returns>Double</returns>
    public float Max_InsideDiameterToManholeDiameterTable()
    {
      return _InsideDiameterToManholeDiameterTable.Keys[_InsideDiameterToManholeDiameterTable.Count - 1];
    } // Max_InsideDiameterToManholeDiameterTable()

    /// <summary>
    /// Min _ outside diameter to trench width table
    /// </summary>
    /// <returns>Double</returns>
    public float Min_OutsideDiameterToTrenchWidthTable()
    {
      return _OutsideDiameterToTrenchWidthTable.Keys[0];
    } // Min_OutsideDiameterToTrenchWidthTable()

    /// <summary>
    /// Max _ outside diameter to trench width table
    /// </summary>
    /// <returns>Double</returns>
    public float Max_OutsideDiameterToTrenchWidthTable()
    {
      return _OutsideDiameterToTrenchWidthTable.Keys[_OutsideDiameterToTrenchWidthTable.Count - 1];
    } // Max_OutsideDiameterToTrenchWidthTable()

    /// <summary>
    /// Min _ pipe cost table
    /// </summary>
    /// <param name="material">Material</param>
    /// <returns>Double</returns>
    public float Min_PipeCostTable(PipeMaterial material)
    {
      return _PipeCostTable[material].Keys[0];
    } // Min_PipeCostTable(material)

    /// <summary>
    /// Max _ pipe cost table
    /// </summary>
    /// <param name="material">Material</param>
    /// <returns>Double</returns>
    public float Max_PipeCostTable(PipeMaterial material)
    {
      return _PipeCostTable[material].Keys[_PipeCostTable[material].Count - 1];
    } // Max_PipeCostTable(material)

    /// <summary>
    /// Min diameter _ lateral cost table
    /// </summary>
    /// <returns>Double</returns>
    public float MinDiameter_LateralCostTable()
    {
      return _LateralCostTable.Keys[0];
    } // MinDiameter_LateralCostTable()

    /// <summary>
    /// Max diameter _ lateral cost table
    /// </summary>
    /// <returns>Double</returns>
    public float MaxDiameter_LateralCostTable()
    {
      return _LateralCostTable.Keys[_LateralCostTable.Count - 1];
    } // MaxDiameter_LateralCostTable()

    /// <summary>
    /// Min depth _ lateral cost table
    /// </summary>
    /// <returns>Double</returns>
    public float MinDepth_LateralCostTable()
    {
      float minDepth_LateralCostTable = float.MinValue;
      foreach (KeyValuePair<float, SortedList<float, float>> kvPair in _LateralCostTable)
      {
        minDepth_LateralCostTable = (float)Math.Min(minDepth_LateralCostTable, kvPair.Value.Keys[0]);
      } // foreach  (kvPair)
      return minDepth_LateralCostTable;
    } // MinDepth_LateralCostTable()

    /// <summary>
    /// Max depth _ lateral cost table
    /// </summary>
    /// <returns>Double</returns>
    public float MaxDepth_LateralCostTable()
    {
      float maxDepth_LateralCostTable = float.MaxValue;
      foreach (KeyValuePair<float, SortedList<float, float>> kvPair in _LateralCostTable)
      {
        maxDepth_LateralCostTable = (float)Math.Max(maxDepth_LateralCostTable, kvPair.Value.Keys[kvPair.Value.Count - 1]);
      } // foreach  (kvPair)
      return maxDepth_LateralCostTable;
    } // MaxDepth_LateralCostTable()

    /// <summary>
    /// Min _ manhole costs table
    /// </summary>
    /// <returns>Double</returns>
    public float Min_ManholeCostsTable()
    {
      return _ManholeCostsTable.Keys[0];
    } // Min_ManholeCostsTable()

    /// <summary>
    /// Max _ manhole costs table
    /// </summary>
    /// <returns>Double</returns>
    public float Max_ManholeCostsTable()
    {
      return _ManholeCostsTable.Keys[_ManholeCostsTable.Count - 1];
    } // Max_ManholeCostsTable()

    /// <summary>
    /// Min _ flow diversion costs table
    /// </summary>
    /// <returns>Double</returns>
    public float Min_FlowDiversionCostsTable()
    {
      return _FlowDiversionCostsTable.Keys[0];
    } // Min_FlowDiversionCostsTable()

    /// <summary>
    /// Max _ flow diversion costs table
    /// </summary>
    /// <returns>Double</returns>
    public float Max_FlowDiversionCostsTable()
    {
      return _FlowDiversionCostsTable.Keys[_FlowDiversionCostsTable.Count - 1];
    } // Max_FlowDiversionCostsTable()

    /// <summary>
    /// Min _CIPP cost table
    /// </summary>
    /// <returns>Double</returns>
    public float Min_CIPPCostTable()
    {
      return _CIPPCostTable.Keys[0];
    } // Min_CIPPCostTable()

    /// <summary>
    /// Max _CIPP cost table
    /// </summary>
    /// <returns>Double</returns>
    public double Max_CIPPCostTable()
    {
      return _CIPPCostTable.Keys[_CIPPCostTable.Count - 1];
    } // Max_CIPPCostTable()

    /// <summary>
    /// Min _ pipeburst cost table
    /// </summary>
    /// <returns>Double</returns>
    public double Min_PipeburstCostTable()
    {
      return _PipeburstCostTable.Keys[0];
    } // Min_PipeburstCostTable()

    /// <summary>
    /// Max _ pipeburst cost table
    /// </summary>
    /// <returns>Double</returns>
    public double Max_PipeburstCostTable()
    {
      return _PipeburstCostTable.Keys[_PipeburstCostTable.Count - 1];
    } // Max_PipeburstCostTable()

    /// <summary>
    /// Manhole cost depth factor
    /// </summary>
    /// <param name="depth">Depth</param>
    /// <param name="diameter">Diameter</param>
    /// <returns>Double</returns>
    public double ManholeCostDepthFactor(double manholeDiameter, double manholeDepth)
    {
      for (int i = 0; i < _ManholeDepthFactorsTable.Count; i++)
      {
        double minDiameter = _ManholeDepthFactorsTable.Values[i].MinDiameter;
        double maxDiameter = _ManholeDepthFactorsTable.Values[i].MaxDiameter;
        if (minDiameter < manholeDiameter && manholeDiameter <= maxDiameter)
        {
          for (int j = 0; j < _ManholeDepthFactorsTable.Values[i].ManholeDepthFactors.Count; j++)
          {
            ManholeDepthFactor currentDepthFactor = _ManholeDepthFactorsTable.Values[i].ManholeDepthFactors.Values[j];
            double minDepth = currentDepthFactor.MinDepth;
            double maxDepth = currentDepthFactor.MaxDepth;
            if (minDepth < manholeDepth && manholeDepth <= maxDepth)
              return currentDepthFactor.Factor;
          }
        }
      }
      return 1.0;
    } // ManholeCostDepthFactor(depth, diameter)

    /// <summary>
    /// Assign direct construction cost items
    /// </summary>
    public void AssignDirectConstructionCostItems()
    {
      bool outsidePipeCostTable;
      bool outsideCIPPCostTable;
      bool outsidePipeburstCostTable;
      bool outsideTVCleaningTable;

      DirectConstructionCostItems.Clear();

      switch (_Material)
      {
        case PipeMaterial.Concrete:
        case PipeMaterial.PVCHDPE:
          double baseCostMultiplierForDepth = 1;

          if (_InsideDiameter < 78)
          {
            if (_Depth < 18)
              baseCostMultiplierForDepth = 1.0;
            else
              if (_Depth >= 18 && _Depth < 24)
                baseCostMultiplierForDepth = 1.1;
              else
                if (_Depth >= 24)
                  baseCostMultiplierForDepth = 1.2;
          } // if
          else
          {
            if (_Depth < 18)
              baseCostMultiplierForDepth = 1.0;
            else
              if (_Depth >= 18 && _Depth < 20)
                baseCostMultiplierForDepth = 1.1;
              else
                if (_Depth >= 20 && _Depth < 24)
                  baseCostMultiplierForDepth = 1.2;
                else
                  if (_Depth >= 24)
                    baseCostMultiplierForDepth = 1.25;
          } // else

          _OutsideTableMessages.Clear();

          DirectConstructionCostItems.Add("Sawcutting AC pavement",
            new UnitCost((float)((double)_SawcuttingACPavementPerSqFt), "sqft", Math.Max(DEFAULT_AC_WIDTH, TrenchBaseWidth)));
          DirectConstructionCostItems.Add(string.Format("Asphalt removal {0:F0} in inside diam", _InsideDiameter),
            new UnitCost((float)((double)_AsphaltRemovalPerSqYd), "sqyd", AsphaltRemovalWidth / 9));
          DirectConstructionCostItems.Add(string.Format("Trench excavation {0:F0} ft deep, {1:F0} in inside diam", _Depth, _InsideDiameter),
            new UnitCost((float)((double)TrenchExcavationCostPerCuYdFromDepth(_Depth)), "cuyd", ExcavationVolume));
          DirectConstructionCostItems.Add(string.Format("Truck haul excavation spoils {0:F0} ft deep, {1:F0} in inside diam", _Depth, _InsideDiameter),
            new UnitCost((float)((double)_TruckHaulExcavationSpoilsPerCuYd), "cuyd", SpoilsVolume));

          if (_Depth < MAX_TRENCH_SHORING_DEPTH)
            DirectConstructionCostItems.Add(string.Format("Trench shoring {0:F0} ft deep", _Depth),
              new UnitCost((float)((double)_TrenchShoringPerSqFt), "sqft",  _Depth * 2));
          DirectConstructionCostItems.Add(string.Format("Pipe cost {0:F0} in inside diam", _InsideDiameter),
            new UnitCost((float)((double)PipeCost(_Material, _InsideDiameter, out outsidePipeCostTable) * baseCostMultiplierForDepth), "ft"));
          if (outsidePipeCostTable)
            _OutsideTableMessages.Add("Pipe Cost", "Outside pipe cost table");
          DirectConstructionCostItems.Add(string.Format("Pipe zone backfill {0:F0} ft deep, {1:F0} in inside diam", _Depth, _InsideDiameter),
            new UnitCost((float)((double)(_InsideDiameter <= 12 ?
            _PipeZoneBackfillLessThanEqualTo12InDiamPerCuYd :
            _PipeZoneBackfillGreater12InDiamPerCuYd)), "cuyd", PipeZoneVolume));
          DirectConstructionCostItems.Add(string.Format("Above zone fill {0:F0} ft deep, {1:F0} in inside diam", _Depth, _InsideDiameter),
            new UnitCost((float)((double)(_InsideDiameter <= 12 ?
            _FillAbovePipeZoneLessThanEqualTo12InDiamPerCuYd :
            _FillAbovePipeZoneGreater12InDiamPerCuYd)), "cuyd", AboveZoneVolume));
          DirectConstructionCostItems.Add("Lateral", new UnitCost(_DefaultLateralCostPerFt, "ft", 1));
          DirectConstructionCostItems.Add(string.Format("Asphalt trench patch base course {0:F0} in inside diam", _InsideDiameter),
            new UnitCost((float)((double)_AsphaltTrenchPatchBaseCoursePerCuYd), "cuyd", AsphaltBaseVolume));
          DirectConstructionCostItems.Add(string.Format("Asphalt trench patch {0:F0} in inside diam", _InsideDiameter),
            new UnitCost((float)((double)_AsphaltTrenchPathPerSqYd), "sqyd", AsphaltPatchSurfaceArea / 9));
          break;
        case PipeMaterial.CIPP:
          _OutsideTableMessages.Clear();

          DirectConstructionCostItems.Add(string.Format("Pipe cost - liner {0:F0} in inside diam", _InsideDiameter),
          new UnitCost((float)((double)CIPPPipeCost(_InsideDiameter, out outsideCIPPCostTable)), "ft"));
          if (outsideCIPPCostTable)
            _OutsideTableMessages.Add("CIPP Cost", "Outside CIPP cost table");

          DirectConstructionCostItems.Add("Lateral", new UnitCost(_DefaultLateralCostPerFt, "ft", 1));

          DirectConstructionCostItems.Add(string.Format("TV cleaning", _InsideDiameter),
            new UnitCost((float)((double)TVCleaningCost(_InsideDiameter, out outsideTVCleaningTable)), "ft"));
          if (outsideTVCleaningTable)
            _OutsideTableMessages.Add("TV Cleaning Cost", "Outside tv cleaning cost table");
          break;
        case PipeMaterial.Pipeburst:
          _OutsideTableMessages.Clear();

          DirectConstructionCostItems.Add(string.Format("Pipe cost - pipeburst {0:F0} in inside diam", _InsideDiameter),
            new UnitCost((float)((double)PipeburstPipeCost(_InsideDiameter, out outsidePipeburstCostTable)), "ft"));
          if (outsidePipeburstCostTable)
            _OutsideTableMessages.Add("Pipeburst Cost", "Outside pipeburst cost table");
          DirectConstructionCostItems.Add("Lateral (pipeburst)",
            new UnitCost((float)((double)_PipeburstLateralCost), "ea"));
          DirectConstructionCostItems.Add(string.Format("TV cleaning {0:F0} in inside diameter", _InsideDiameter),
            new UnitCost((float)((double)(_PipeburstTVCleanLessThanEqualTo24InDiamCost)), "ft"));
          break;
      } // switch

    } // AssignDirectConstructionCostItems()

    /// <summary>
    /// Create direct construction cost items
    /// </summary>
    /// <param name="parent">Parent</param>
    public void CreateDirectConstructionCostItems(Project project, CostItemFactor parent)
    {
      AssignDirectConstructionCostItems();

      foreach (KeyValuePair<string, UnitCost> kvpair in DirectConstructionCostItems)
      {
        CostItem costItemToAssign;
        if (project.PoolHasCostItem(kvpair.Key))
          costItemToAssign = project.CostItemFromPool(kvpair.Key);
        else
        {
          costItemToAssign = new CostItem(kvpair.Key, 1, kvpair.Value.CostPerUnit, kvpair.Value.UnitName);
          project.AddCostItemToPool(costItemToAssign);
        } // else

        CostItemFactor newCostItemFactor = new CostItemFactor(kvpair.Key, costItemToAssign, null);
        newCostItemFactor.Quantity = kvpair.Value.Units;
        parent.AddCostItemFactor(newCostItemFactor);
        project.AddCostItemFactorToPool(newCostItemFactor);
      } // foreach  (kvpair)
    } // CreateDirectConstructionCostItems(project, parent)

    /// <summary>
    /// Pipe cost table
    /// </summary>
    /// <param name="material">Material</param>
    /// <returns>Sorted list</returns>
    public SortedList<float, float> PipeCostTable(PipeMaterial material)
    {
      switch (material)
      {
        case PipeMaterial.Concrete:
        case PipeMaterial.PVCHDPE:
          return _PipeCostTable[material];
        case PipeMaterial.CIPP:
          return _CIPPCostTable;
        case PipeMaterial.Pipeburst:
          return _PipeburstCostTable;
      } // switch
      return null;
    } // PipeCostTable(material)

    /// <summary>
    /// Manhole cost table
    /// </summary>
    /// <returns>Sorted list</returns>
    public SortedList<float, ManholeCosts> ManholeCostTable()
    {
      return _ManholeCostsTable;
    } // ManholeCostTable()

    /// <summary>
    /// Flow diversion costs table
    /// </summary>
    /// <returns>Sorted list</returns>
    public SortedList<float, float> FlowDiversionCostTable()
    {
      return _FlowDiversionCostsTable;
    } // FlowDiversionCostTable()
    #endregion
  } // class PipeCoster
}
